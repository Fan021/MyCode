Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO.Ports
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("KeyenceScanner_Com", "Scanner")>
Public Class clsScanner
    Inherits clsHMIAutoScanner
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Protected _Interface As New System.IO.Ports.SerialPort
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
            _ReadTimeout = CInt(lListInitParameter(5))
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
            Return Transmit(strTrigOff)
        End SyncLock
    End Function

    Public Overrides Function TrigOn() As Boolean
        SyncLock _Object
            strScanResult = ""
            _Interface.DiscardInBuffer()
            _Interface.DiscardOutBuffer()
            Return Transmit(strTrigOn)
        End SyncLock
    End Function

    Public Function StartReceive() As Boolean
        SyncLock _Object
            cThread = New Thread(AddressOf ReceiveEx)
            cThread.IsBackground = True
            cThread.Start()

            Return True
        End SyncLock
    End Function

    Public Overrides Function StopReceive() As Boolean
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
            Try
                _Interface = New SerialPort
                _Interface.PortName = lListInitParameter(0)
                _Interface.BaudRate = Integer.Parse(lListInitParameter(1))
                Select Case lListInitParameter(2).ToUpper
                    Case "N"
                        _Interface.Parity = Parity.None
                    Case "E"
                        _Interface.Parity = Parity.Even
                    Case "O"
                        _Interface.Parity = Parity.Odd
                    Case Else
                        _Interface.Parity = Parity.None
                End Select

                _Interface.DataBits = Integer.Parse(lListInitParameter(3))
                Select Case lListInitParameter(4).ToUpper
                    Case "1"
                        _Interface.StopBits = StopBits.One
                    Case "2"
                        _Interface.StopBits = StopBits.Two
                    Case Else
                        _Interface.StopBits = StopBits.One
                End Select

                _Interface.Open()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function Transmit(ByVal Content As String) As Boolean
        SyncLock _Object
            Dim Buffer(0) As Byte, strText As String
            Try
                strText = Content
                If strText <> "" Then
                    _Interface.Write(Content)
                    ' Buffer = Encoding.ASCII.GetBytes(strText)
                    '  If Not IsNothing(_Interface) Then
                    '_Interface.Write(Buffer, Buffer.GetLength(0), SocketFlags.None)
                    'End If
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
                If _Interface.ReadBufferSize <= 0 Then
                    Continue Do
                End If
                _NowReceived = _Interface.ReadExisting()
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
                If _Interface.ReadBufferSize <= 0 Then
                    Continue Do
                End If
                _NowReceived = _Interface.ReadExisting
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
                strTimeOutError = cLanguageManager.GetUserTextLine("KeyenceScanner_Com", "9", enumDataFrame.STX.ToString, _ReceivedString)
            End If
            If _ReceivedString.IndexOf(DataFrameEXT) < 0 Then
                strTimeOutError = cLanguageManager.GetUserTextLine("KeyenceScanner_Com", "10", enumDataFrame.EXT.ToString, _ReceivedString)
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