Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("KeyenceScanner", "Scanner")>
Public Class clsScanner
    Inherits clsHMIAutoScanner

    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Protected _Interface As Socket
    Protected _IpEndPoint As IPEndPoint
    Protected _IpAddress As IPAddress
    Protected strTrigOn As String
    Protected strTrigOff As String
    Private cThread As Thread
    Protected DataFrameSTX As String = Chr(enumDataFrame.STX)
    Protected DataFrameEXT As String = Chr(enumDataFrame.EXT)
    Protected TimerCB As New TimerCallback(AddressOf _TimerCB)
    Protected _Timer As New System.Threading.Timer(TimerCB, Nothing, Timeout.Infinite, Timeout.Infinite)
    Protected _TimerTick As Boolean
    Protected _ReadTimeout As Integer
    Protected _ReceivedString As String
    Protected bScanEnd As Boolean
    Protected bTimeOut As Boolean
    Protected isRunning As Boolean = False
    Protected strScanResult As String = String.Empty
    Protected m_eventas As EventWaitHandle
    Protected strTimeOutError As String = String.Empty

    Public Overrides Property Running As Boolean
        Set(ByVal value As Boolean)
            isRunning = value
        End Set
        Get
            Return isRunning
        End Get
    End Property

    Public Property ScanResult As String
        Set(ByVal value As String)
            strScanResult = value
        End Set
        Get
            Return strScanResult
        End Get
    End Property

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        CreateInitUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        CreateControlUI(cLocalElement, cSystemElement)
        strTrigOn = DataFrameSTX + enumKeyence_ScannerCmds.LON.ToString + DataFrameEXT
        strTrigOff = DataFrameSTX + enumKeyence_ScannerCmds.LOFF.ToString + DataFrameEXT
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        m_eventas = New EventWaitHandle(True, EventResetMode.ManualReset)
        If Not Connect() Then Return False
        Close()
        Return True
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iControlUI) Then
                iControlUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then
            RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function


    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then
            RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iInitUI.Quit(cLocalElement, cSystemElement)
        End If
        iInitUI = New InitUI
        AddHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        MyBase.CreateShortcutUI(cLocalElement, cSystemElement)
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function Scan(ByVal dTimeOut As Double, ByRef strResult As String, ByRef strErrorMessage As String) As Boolean
        SyncLock _Object

            strResult = ""
            bScanEnd = False
            bTimeOut = False
            isRunning = True
            _ReceivedString = ""
            strScanResult = ""
            _ReadTimeout = CInt(lListInitParameter(2))
            If lListInitParameter(3).ToUpper = "FALSE" Then Return True
            If Not Connect() Then Return False
            If Not TrigOn() Then Return False
            _TimerTick = False
            cThread = New Thread(AddressOf Receive)
            cThread.IsBackground = True
            cThread.Start()
            Do While Not bScanEnd
                System.Threading.Thread.Sleep(10)
            Loop
            If Not TrigOff() Then Return False
            Close()
            If Not bTimeOut Then
                strResult = _ReceivedString
                Return True
            Else
                strErrorMessage = strTimeOutError
                strResult = _ReceivedString
                Return False
            End If
        End SyncLock
    End Function

    Public Overrides Function TrigOff() As Boolean
        SyncLock _Object
            If lListInitParameter(3).ToUpper = "FALSE" Then Return True
            Return Transmit(strTrigOff)
        End SyncLock
    End Function

    Public Overrides Function TrigOn() As Boolean
        SyncLock _Object
            strScanResult = ""
            If lListInitParameter(3).ToUpper = "FALSE" Then Return True
            Return Transmit(strTrigOn)
        End SyncLock
    End Function

    Public Function StartReceive() As Boolean
        SyncLock _Object
            If lListInitParameter(3).ToUpper = "FALSE" Then Return True
            cThread = New Thread(AddressOf ReceiveEx)
            cThread.IsBackground = True
            cThread.Start()

            Return True
        End SyncLock
    End Function

    Public Overrides Function StopReceive() As Boolean
        If lListInitParameter(3).ToUpper = "FALSE" Then Return True
        _TimerTick = True
        Dim iCnt As Integer = 100
        Do While iCnt > 0
            If IsNothing(cThread) Then
                Exit Do
            End If
            If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                Exit Do
            End If
            iCnt = iCnt - 1
            System.Threading.Thread.Sleep(1)
        Loop
        If Not IsNothing(cThread) Then cThread.Abort()
        bScanEnd = True
        Return True
    End Function

    Public Function Connect() As Boolean
        SyncLock _Object
            Dim _Address() As String
            Dim _ByteAddress(3) As Byte, l As Integer
            If lListInitParameter(3).ToUpper = "FALSE" Then Return True
            Try
                _Address = Split(lListInitParameter(0), ".")

                For l = _Address.GetLowerBound(0) To _Address.GetUpperBound(0)
                    _ByteAddress(l) = CType(_Address(l), Byte)
                Next
                m_eventas.Reset()
                _IpAddress = Nothing
                _IpAddress = New IPAddress(_ByteAddress)

                _IpEndPoint = Nothing
                _IpEndPoint = New IPEndPoint(_IpAddress, lListInitParameter(1))

                _Interface = Nothing
                _Interface = New Socket(_IpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
                ' _Interface.Connect(_IpEndPoint)
                _Interface.BeginConnect(_IpEndPoint, New AsyncCallback(AddressOf ConnectCallBack), _Interface)
                m_eventas.WaitOne(2000, False)
                Dim isConnected As Boolean = _Interface.Connected
                If Not isConnected Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "7", lListInitParameter(0), lListInitParameter(1)), enumExceptionType.Alarm)
                End If
                Return isConnected
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub ConnectCallBack(ByVal asyncresult As IAsyncResult)
        Try
            Dim sok As Socket = CType(asyncresult.AsyncState, Socket)
            sok.EndConnect(asyncresult)

        Catch ex As Exception

        End Try
        m_eventas.Set()
    End Sub

    Public Function Transmit(ByVal Content As String) As Boolean
        SyncLock _Object
            Dim Buffer(0) As Byte, strText As String
            Try
                strText = Content
                If strText <> "" Then
                    Buffer = Encoding.ASCII.GetBytes(strText)
                    If Not IsNothing(_Interface) Then
                        _Interface.Send(Buffer, Buffer.GetLength(0), SocketFlags.None)
                    End If
                End If
                Return True
            Catch ex As Exception
                Close()
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Sub ReceiveEx()
        Try
            Dim Buffer(0) As Byte, _NowReceived As String = ""
            _TimerTick = False

            Do While Not _TimerTick
                Application.DoEvents()
                Thread.Sleep(10)
                If _Interface.Available <= 0 Then
                    Continue Do
                End If
                _Interface.Receive(Buffer)
                _NowReceived = Encoding.ASCII.GetString(Buffer)
                _ReceivedString = _ReceivedString + _NowReceived
                If _ReceivedString <> "" AndAlso _ReceivedString.IndexOf(DataFrameSTX) >= 0 AndAlso _ReceivedString.IndexOf(DataFrameEXT) >= 0 Then
                    strScanResult = _ReceivedString.Substring(_ReceivedString.IndexOf(DataFrameSTX) + Len(DataFrameSTX), _ReceivedString.IndexOf(DataFrameEXT) - _ReceivedString.IndexOf(DataFrameSTX) - Len(DataFrameSTX))
                    _TimerTick = False
                    Return
                End If
            Loop
            _TimerTick = False
        Catch ex As Exception
            _TimerTick = False
            Close()
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
        End Try
    End Sub

    Public Sub Receive()
        Try
            Dim Buffer(0) As Byte, _NowReceived As String = ""
            _TimerTick = False
            _Timer.Change(_ReadTimeout, Timeout.Infinite)

            Do While Not _TimerTick
                Application.DoEvents()
                Thread.Sleep(10)
                If _Interface.Available <= 0 Then
                    Continue Do
                End If
                _Interface.Receive(Buffer)
                _NowReceived = Encoding.ASCII.GetString(Buffer)
                _ReceivedString = _ReceivedString + _NowReceived
                strScanResult = strScanResult + _NowReceived
                If _ReceivedString <> "" AndAlso _ReceivedString.IndexOf(DataFrameSTX) >= 0 AndAlso _ReceivedString.IndexOf(DataFrameEXT) >= 0 Then
                    _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(DataFrameSTX) + Len(DataFrameSTX), _ReceivedString.IndexOf(DataFrameEXT) - _ReceivedString.IndexOf(DataFrameSTX) - Len(DataFrameSTX))
                    _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                    _TimerTick = False
                    bScanEnd = True
                    Return
                End If
            Loop
            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False
        Catch ex As Exception
            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False
            Close()
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
        End Try
    End Sub

    Protected Sub _TimerCB(ByVal state As Object)
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        _TimerTick = True
        Dim iCnt As Integer = 100
        Do While iCnt > 0
            If IsNothing(cThread) Then
                Exit Do
            End If
            If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                Exit Do
            End If
            iCnt = iCnt - 1
            System.Threading.Thread.Sleep(1)
        Loop
        If Not IsNothing(cThread) Then cThread.Abort()
        If _ReceivedString <> "" Then
            If _ReceivedString.IndexOf(DataFrameSTX) < 0 Then
                strTimeOutError = cLanguageManager.GetUserTextLine("KeyenceScanner", "5", enumDataFrame.STX.ToString, _ReceivedString)
            End If
            If _ReceivedString.IndexOf(DataFrameEXT) < 0 Then
                strTimeOutError = cLanguageManager.GetUserTextLine("KeyenceScanner", "6", enumDataFrame.EXT.ToString, _ReceivedString)
            End If
        Else
            strTimeOutError = "Time Out"
        End If
        bScanEnd = True
        bTimeOut = True
    End Sub
    Public Sub Close()
        SyncLock _Object
            Try
                If Not IsNothing(_Interface) Then
                    _Interface.Shutdown(SocketShutdown.Both)
                    _Interface.Disconnect(False)
                    _Interface.Close()
                    _Interface = Nothing
                End If
            Catch ex As Exception
            End Try

        End SyncLock
    End Sub


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


End Class

Public Enum enumDataFrame
    STX = &H2
    EXT = &H3
End Enum

Public Enum enumKeyence_ScannerCmds
    LON = 1
    LOFF = 2
    BCLR
    SSET
    RESET
End Enum