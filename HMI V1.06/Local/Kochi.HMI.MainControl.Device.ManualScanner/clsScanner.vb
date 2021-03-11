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

<clsHMIDeviceNameAttribute("ManualScanner", "Scanner")>
Public Class clsScanner
    Inherits clsHMIManualScanner

    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Protected WithEvents _Interface As New System.IO.Ports.SerialPort
    Protected TimerCB As New TimerCallback(AddressOf _TimerCB)
    Protected _Timer As New System.Threading.Timer(TimerCB, Nothing, Timeout.Infinite, Timeout.Infinite)
    Protected _ReceivedString As String = ""
    Protected bTimeOut As Boolean
    Protected bActive As Boolean = False
    Protected bScanEnd As Boolean = False
    Public Overrides Property isActive As Boolean
        Get
            Return bActive
        End Get
        Set(ByVal value As Boolean)
            If Not value Then
                _ReceivedString = ""
            End If
            bActive = value
        End Set
    End Property

    Public Overrides Property ScanResult As String
        Get
            Return _ReceivedString
        End Get
        Set(ByVal value As String)
            _ReceivedString = value
        End Set
    End Property


    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        CreateInitUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        CreateControlUI(cLocalElement, cSystemElement)
        If Not Connect() Then Return False
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
            Close()
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
                AddHandler _Interface.DataReceived, AddressOf Receive
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Sub Receive(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Try
            SyncLock _Object
                Dim NumberOfBytes As Integer, _Receive() As Char, l As Integer
                _ReceivedString = ""
                bActive = False
                System.Threading.Thread.Sleep(100)
                NumberOfBytes = _Interface.BytesToRead
                If NumberOfBytes > 0 Then
                    ReDim _Receive(NumberOfBytes - 1)
                    _Interface.Read(_Receive, 0, NumberOfBytes)
                    For l = _Receive.GetLowerBound(0) To _Receive.GetUpperBound(0)
                        If Not bTimeOut Then
                            '   _Timer.Change(1000, Timeout.Infinite)
                            bTimeOut = True
                        End If
                        _ReceivedString = _ReceivedString & _Receive(l)
                    Next
                End If
                If _ReceivedString <> "" Then
                    If _ReceivedString.IndexOf(vbCrLf) > 0 Then
                        _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(vbCrLf))
                        '  _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        bTimeOut = False
                        bActive = True
                    End If
                    Return
                End If
            End SyncLock
        Catch ex As Exception
            '  _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            bTimeOut = False
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
        End Try
    End Sub

    Protected Sub _TimerCB(ByVal state As Object)
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        If _ReceivedString <> "" Then
            If _ReceivedString.IndexOf(vbCrLf) < 0 Then
                _ReceivedString = cLanguageManager.GetUserTextLine("ManualScanner", "8", "CrLf", _ReceivedString)
            End If
        End If
        bTimeOut = False
    End Sub

    Public Sub Close()
        SyncLock _Object
            Try
                If Not IsNothing(_Interface) Then
                    RemoveHandler _Interface.DataReceived, AddressOf Receive
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

    Public Overrides Function StopReceive() As Boolean
        Return True
    End Function
End Class

Public Enum enumDataFrame
    STX = &H2
    EXT = &H3
End Enum