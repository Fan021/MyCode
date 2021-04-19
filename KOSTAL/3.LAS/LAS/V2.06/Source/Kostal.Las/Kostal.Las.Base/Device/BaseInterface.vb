Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Printing

Public Class StateObject
    ' Client socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 256
    ' Receive buffer.
    Public buffer(BufferSize) As Byte
    ' Received data string.
    Public sb As String
End Class

Public Class TCPIPClient
    Public client As Socket
    Protected remoteEP As IPEndPoint
    ' ManualResetEvent instances signal completion.
    Protected connectDone As New ManualResetEvent(False)
    Protected sendDone As New ManualResetEvent(False)
    Protected receiveDone As New ManualResetEvent(False)
    Public response As String = String.Empty
    Protected isTestRusult As Boolean = False
    Protected ar As IAsyncResult
    Public Function Init(ByVal mIP As String, ByVal mPort As String) As Boolean
        Dim isTestRusult As Boolean = False
        Try
            Dim ipAddress As IPAddress = ipAddress.Parse(mIP)
            remoteEP = New IPEndPoint(ipAddress, CInt(mPort))
            client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            connectDone.Reset()
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
            Thread.Sleep(50)
            isTestRusult = connectDone.WaitOne(40, False)
            If Not isTestRusult Then Throw New Exception("SR610 FAIL")
        Catch ex As Exception
            Throw New Exception("Function:Init " + ex.Message.ToString)
            Return False
        End Try
        Return isTestRusult
    End Function

    Protected Sub ConnectCallback(ByVal ar As IAsyncResult)
        Try
            Me.ar = ar
            Dim client As Socket = CType(ar.AsyncState, Socket)
            If Not client.Connected Then Return
            client.EndConnect(ar)
            connectDone.Set()
            Receive(client)
        Catch ex As Exception
            Throw New Exception("Function:ConnectCallback " + ex.Message.ToString)
        End Try
    End Sub

    Protected Sub Receive(ByVal client As Socket)
        Try
            Dim state As New StateObject
            state.workSocket = client
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)

        Catch ex As Exception
            Throw New Exception("Function:Receive " + ex.Message.ToString)
        End Try
    End Sub

    Protected Sub ReceiveCallback(ByVal ar As IAsyncResult)
        Try
            Dim state As StateObject = CType(ar.AsyncState, StateObject)
            Dim client As Socket = state.workSocket
            Dim bytesRead As Integer = client.EndReceive(ar)

            If bytesRead > 0 Then
                state.sb = Encoding.ASCII.GetString(state.buffer, 0, bytesRead)
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
                response += state.sb
            End If
        Catch ex As Exception
            response = ""
            '  Dim a As String = ex.Message
            ' Throw New Exception("Function:ReceiveCallback " + ex.Message.ToString)
        End Try

    End Sub

    Public Overloads Function Send(ByVal mStr As String) As Boolean
        Try
            Dim byteData() As Byte
            sendDone.Reset()
            '    response = ""
            byteData = Encoding.ASCII.GetBytes(mStr)
            client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
            isTestRusult = sendDone.WaitOne(20, False)
            If Not isTestRusult Then Return False
            Return True
        Catch ex As Exception
            Throw New Exception("Function:Send " + ex.Message.ToString)
            Return False
        End Try
    End Function
    Public Overloads Function Send(ByVal byteData() As Byte) As Boolean
        Try
            sendDone.Reset()
            ' response = ""
            client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
            isTestRusult = sendDone.WaitOne(20, False)
            If Not isTestRusult Then Return False
            Return True
        Catch ex As Exception
            Throw New Exception("Function:Send " + ex.Message.ToString)
            Return False
        End Try
    End Function

    Public Function Read() As String
        Return response
    End Function


    Protected Sub SendCallback(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer = client.EndSend(ar)
            sendDone.Set()
        Catch ex As Exception
            Throw New Exception("Function:SendCallback " + ex.Message.ToString)
        End Try
    End Sub

    Public Function Quit() As Boolean
        Try
            If client IsNot Nothing Then
                client.Disconnect(True)
                client.Close()
            End If
        Catch ex As Exception
            Throw New Exception("Function:Quit " + ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

End Class

Public Enum InterfaceType
    LAN = 0
    RS232
End Enum

Public Enum enumDevice_ErrorCodes
    DEVICE_ERROR_WINDOWS_ERROR = -99
    DEVICE_ERROR_NO_ERROR = 0
    DEVICE_ERROR_PORT_IS_ALWAYS_OPEN
    DEVICE_ERROR_PORT_CANNOT_OPEN
    DEVICE_ERROR_INTERFACE_IS_NOT_MSCOMM
    DEVICE_ERROR_FILE_NOT_FOUND
    DEVICE_ERROR_NO_RESPONSE
    DEVICE_ERROR_INVALID_IP_ADDRESS
    DEVICE_ERROR_INVALID_IP_PORT
    DEVICE_ERROR_INVALID_CONFIG
    DEVICE_ERROR_TIMEOUT
    DEVICE_ERROR_STX
    DEVICE_ERROR_EXT
    DEVICE_ERROR_EXCEPTION
End Enum

Public Interface IInterface
    ReadOnly Property Running As Boolean
    ReadOnly Property ReceivedString As String
    Property ScanResult As String
    Property ReadTimeout As Integer
    Property Status As enumDevice_ErrorCodes
    Property StatusDescription As String
    Function Interface_Init(ByVal InterfaceConfig As InterfaceConfig, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Function Interface_Connect() As Boolean
    Function Interface_Transmit(ByVal Content As String) As Boolean
    Function Interface_Receive() As Boolean
    Function Interface_Receive(ByVal iTimeOut As Integer, Optional ByVal STX As String = "", Optional ByVal EXT As String = "") As Boolean
    Sub Interface_Abort()
    Sub Interface_Close()
    Sub Send(ByVal Content As String)
    Sub SendRead(ByVal Content As String, ByVal iTimeOut As Integer)
    Sub SendReadScan(ByVal TrigOn As String, ByVal TrigOff As String, ByVal iTimeOut As Integer)
    Sub SendCommand(ByVal Content As String, ByVal mReceiveCmd As String, ByVal iTimeOut As Integer)
    Sub SendCommandAndRead(ByVal Content As String, ByVal mReceiveCmd As String, ByVal iTimeOut As Integer)
    Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
End Interface

Public Class InterfaceConfig
    Protected _IP As String
    Protected _Port As Integer
    Protected _RS232Port As String
    Protected _BaudRate As Integer = 9600
    Protected _DataBits As Integer = 8
    Protected _Parity As IO.Ports.Parity = IO.Ports.Parity.None
    Protected _StopBits As IO.Ports.StopBits = IO.Ports.StopBits.One
    Protected _Handshake As IO.Ports.Handshake = IO.Ports.Handshake.None
    Protected _ReceivedBytesThreshold As Integer = 1
    Protected _DataFrameSTX As String = Chr(&H2)
    Protected _DataFrameEXT As String = Chr(&H3)
    Protected _TrigOn As String = String.Empty
    Protected _TrigOff As String = String.Empty
    Protected _Beep As String = String.Empty
    Protected _RemoveString As String = String.Empty
    Protected _EventDataReceived As Boolean = False



    Public Property TrigOn As String
        Get
            Return _TrigOn
        End Get
        Set(ByVal value As String)
            _TrigOn = value
        End Set
    End Property

    Public Property TrigOff As String
        Get
            Return _TrigOff
        End Get
        Set(ByVal value As String)
            _TrigOff = value
        End Set
    End Property

    Public Property Beep As String
        Get
            Return _Beep
        End Get
        Set(ByVal value As String)
            _Beep = value
        End Set
    End Property

    Public Property IP As String
        Get
            Return _IP
        End Get
        Set(ByVal value As String)
            _IP = value
        End Set
    End Property

    Public Property Port As Integer
        Get
            Return _Port
        End Get
        Set(ByVal value As Integer)
            _Port = value
        End Set
    End Property

    Public Property RS232Port As String
        Get
            Return _RS232Port
        End Get
        Set(ByVal value As String)
            _RS232Port = value
        End Set
    End Property

    Public Property BaudRate As Integer
        Get
            Return _BaudRate
        End Get
        Set(ByVal value As Integer)
            _BaudRate = value
        End Set
    End Property

    Public Property DataBits As Integer
        Get
            Return _DataBits
        End Get
        Set(ByVal value As Integer)
            _DataBits = value
        End Set
    End Property


    Public Property Parity As IO.Ports.Parity
        Get
            Return _Parity
        End Get
        Set(ByVal value As IO.Ports.Parity)
            _Parity = value
        End Set
    End Property

    Public Property StopBits As IO.Ports.StopBits
        Get
            Return _StopBits
        End Get
        Set(ByVal value As IO.Ports.StopBits)
            _StopBits = value
        End Set
    End Property

    Public Property Handshake As IO.Ports.Handshake
        Get
            Return _Handshake
        End Get
        Set(ByVal value As IO.Ports.Handshake)
            _Handshake = value
        End Set
    End Property

    Public Property ReceivedBytesThreshold As Integer
        Get
            Return _ReceivedBytesThreshold
        End Get
        Set(ByVal value As Integer)
            _ReceivedBytesThreshold = value
        End Set
    End Property


    Public Property DataFrameSTX As String
        Get
            Return _DataFrameSTX
        End Get
        Set(ByVal value As String)
            _DataFrameSTX = value
        End Set
    End Property

    Public Property DataFrameEXT As String
        Get
            Return _DataFrameEXT
        End Get
        Set(ByVal value As String)
            _DataFrameEXT = value
        End Set
    End Property

    Public Property RemoveString As String
        Get
            Return _RemoveString
        End Get
        Set(ByVal value As String)
            _RemoveString = value
        End Set
    End Property

    Public Property EventDataReceived As Boolean
        Get
            Return _EventDataReceived
        End Get
        Set(ByVal value As Boolean)
            _EventDataReceived = value
        End Set
    End Property

End Class
Public MustInherit Class BaseInterface
    Implements IInterface
    Protected _Running As Boolean
    Protected _ScanResult As String
    Protected _Status As enumDevice_ErrorCodes
    Protected _StatusDescription As String
    Protected _InterfaceConfig As InterfaceConfig
    Protected _ReceivedString As String = ""
    Protected _SendReadPassed As Boolean
    Protected _InterfaceSync As New Object
    Protected _ReadTimeout As Integer = 3000
    Public Const CON_sERROR As String = "ERROR"
    Public Const CON_sTIMEOUT As String = "TIMEOUT"
    Public Const CON_sNULL As String = ""
    Protected Const CON_Delimiter As String = ";"
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station

    Protected TimerCB As New TimerCallback(AddressOf _TimerCB)
    Protected _Timer As New System.Threading.Timer(TimerCB, Nothing, Timeout.Infinite, Timeout.Infinite)
    Protected _TimerTick As Boolean
    Public Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String) Implements IInterface.DataReceived

    Protected Delegate Function DelegateSendData(ByVal Content As String) As Boolean
    Protected Delegate Function DelegateReadData(ByVal Content As String) As Boolean
    Protected Delegate Function DelegateSendAndReadData(ByVal Content As String) As Boolean
    Protected Delegate Function DelegateSendAndReadDataScan(ByVal TrigOn As String, ByVal TrigOFf As String) As Boolean
    Protected Delegate Function DelegateSendCommandAndReadData(ByVal Content As String, ByVal mCmd As String) As Boolean
    Protected pSendRoutine As New DelegateSendAndReadData(AddressOf _SendData)
    Protected pSendReadRoutine As New DelegateSendAndReadData(AddressOf _SendAndReadData)
    Protected pSendReadScanRoutine As New DelegateSendAndReadDataScan(AddressOf _SendAndReadScanData)
    Protected pSendCommandRoutine As New DelegateSendCommandAndReadData(AddressOf _SendCommand)
    Protected pSendCommandReadRoutine As New DelegateSendCommandAndReadData(AddressOf _SendCommandAndReadData)
    Protected pSendCallBack As AsyncCallback = New AsyncCallback(AddressOf SendCallBack)
    Protected pSendReadCallBack As AsyncCallback = New AsyncCallback(AddressOf SendReadCallBack)
    Protected pSendReadScanCallBack As AsyncCallback = New AsyncCallback(AddressOf SendReadScanCallBack)
    Protected pSendCommandCallBack As AsyncCallback = New AsyncCallback(AddressOf SendCommandCallBack)
    Protected pSendCommandReadCallBack As AsyncCallback = New AsyncCallback(AddressOf SendCommandReadCallBack)


    ReadOnly Property ReceivedString As String Implements IInterface.ReceivedString
        Get
            Return _ReceivedString
        End Get
    End Property

    ReadOnly Property Running As Boolean Implements IInterface.Running
        Get
            Return _Running
        End Get
    End Property

    Property ScanResult As String Implements IInterface.ScanResult

        Get
            Return _ScanResult
        End Get
        Set(ByVal value As String)
            _ScanResult = value
        End Set
    End Property

    Property ReadTimeout As Integer Implements IInterface.ReadTimeout
        Get
            Return _ReadTimeout
        End Get
        Set(ByVal value As Integer)
            _ReadTimeout = value
        End Set
    End Property

    Public Property Status As enumDevice_ErrorCodes Implements IInterface.Status
        Get
            Return _Status
        End Get
        Set(ByVal value As enumDevice_ErrorCodes)
            _Status = value
        End Set
    End Property

    Public Property StatusDescription As String Implements IInterface.StatusDescription
        Get
            Return _StatusDescription
        End Get
        Set(ByVal value As String)
            _StatusDescription = value
        End Set
    End Property

    Public Overridable Sub Send(ByVal Content As String) Implements IInterface.Send

        SyncLock _InterfaceSync
            If _Running Then
                _TimerTick = True
            End If
            Do While _Running
                Thread.Sleep(10)
            Loop
            Thread.Sleep(10)
            _TimerTick = False
            _Running = True
            Clear()
            pSendRoutine.BeginInvoke(Content, pSendCallBack, Nothing)
        End SyncLock
    End Sub


    Public Overridable Sub SendRead(ByVal Content As String, ByVal iTimeOut As Integer) Implements IInterface.SendRead

        SyncLock _InterfaceSync
            If _Running Then
                _TimerTick = True
            End If
            Do While _Running
                Thread.Sleep(10)
            Loop
            Thread.Sleep(10)
            _TimerTick = False
            _Running = True
            _ReadTimeout = iTimeOut
            Clear()
            pSendReadRoutine.BeginInvoke(Content, pSendReadCallBack, Nothing)
        End SyncLock
    End Sub


    Public Overridable Sub SendReadScan(ByVal TringOn As String, ByVal TringOff As String, ByVal iTimeOut As Integer) Implements IInterface.SendReadScan

        SyncLock _InterfaceSync
            If _Running Then
                _TimerTick = True
            End If
            Do While _Running
                Thread.Sleep(10)
            Loop
            Thread.Sleep(10)
            _TimerTick = False
            _Running = True
            _ReadTimeout = iTimeOut
            Clear()
            pSendReadScanRoutine.BeginInvoke(TringOn, TringOff, pSendReadScanCallBack, Nothing)
        End SyncLock
    End Sub

    Public Overridable Sub SendCommand(ByVal Content As String, ByVal mReceiveCmd As String, ByVal iTimeOut As Integer) Implements IInterface.SendCommand

        SyncLock _InterfaceSync
            If _Running Then
                _TimerTick = True
            End If
            Do While _Running
                Thread.Sleep(10)
            Loop
            Thread.Sleep(10)
            _TimerTick = False
            _Running = True
            _ReadTimeout = iTimeOut
            Clear()
            pSendCommandRoutine.BeginInvoke(Content, mReceiveCmd, pSendCommandCallBack, Nothing)
        End SyncLock
    End Sub

    Public Overridable Sub SendCommandAndRead(ByVal Content As String, ByVal mReceiveCmd As String, ByVal iTimeOut As Integer) Implements IInterface.SendCommandAndRead

        SyncLock _InterfaceSync
            If _Running Then
                _TimerTick = True
            End If
            Do While _Running
                Thread.Sleep(10)
            Loop
            Thread.Sleep(10)
            _TimerTick = False
            _Running = True
            _ReadTimeout = iTimeOut
            Clear()
            pSendCommandReadRoutine.BeginInvoke(Content, mReceiveCmd, pSendCommandReadCallBack, Nothing)
        End SyncLock
    End Sub

    Protected Function _SendData(ByVal Content As String) As Boolean
        If Not Interface_Connect() Then Return False
        If Not Interface_Transmit(Content) Then Return False
        Return True
    End Function
    Protected Function _SendAndReadData(ByVal Content As String) As Boolean
        If Not Interface_Connect() Then Return False
        If Not Interface_Transmit(Content) Then Return False
        If Not Interface_Receive() Then Return False
        Return True
    End Function

    Protected Function _SendAndReadScanData(ByVal TrigOn As String, ByVal TrigOff As String) As Boolean
        If Not Interface_Connect() Then Return False
        If Not Interface_Transmit(TrigOn) Then Return False
        If Not Interface_Receive() Then Return False
        If Not Interface_Transmit(TrigOff) Then Return False
        Return True
    End Function

    Protected Function _SendCommand(ByVal Content As String, ByVal mReceiveCmd As String) As Boolean
        If Not Interface_Connect() Then Return False
        If Not Interface_Transmit(Content) Then Return False
        If Not Interface_ReceiveCmd(mReceiveCmd) Then Return False
        Return True
    End Function

    Protected Function _SendCommandAndReadData(ByVal Content As String, ByVal mReceiveCmd As String) As Boolean
        If Not Interface_Connect() Then Return False
        If Not Interface_Transmit(Content) Then Return False
        If Not Interface_ReceiveCmd(mReceiveCmd) Then Return False
        If Not Interface_Receive() Then Return False
        Return True
    End Function

    Protected Sub SendCallBack(ByVal Result As IAsyncResult)
        _SendReadPassed = pSendRoutine.EndInvoke(Result)
        If _ReceivedString = "" Then _SendReadPassed = False
        If _ReceivedString.Contains(CON_sERROR) Then _SendReadPassed = False
        If _ReceivedString.Contains(CON_sTIMEOUT) Then _SendReadPassed = False
        _ScanResult = ""
        Interface_Abort()
        _Running = False
    End Sub

    Protected Sub SendReadCallBack(ByVal Result As IAsyncResult)
        _SendReadPassed = pSendReadRoutine.EndInvoke(Result)
        _ScanResult = _ReceivedString

        If _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_TIMEOUT Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_STX Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_EXT Then
            _SendReadPassed = False
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, _StatusDescription)
        Else
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, "")
        End If
        Interface_Abort()

        _Running = False
    End Sub

    Protected Sub SendReadScanCallBack(ByVal Result As IAsyncResult)
        _SendReadPassed = pSendReadScanRoutine.EndInvoke(Result)
        _ScanResult = _ReceivedString

        If _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_TIMEOUT Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_STX Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_EXT Then
            _SendReadPassed = False
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, _StatusDescription)
        Else
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, "")
        End If
        Interface_Abort()

        _Running = False
    End Sub

    Protected Sub SendCommandCallBack(ByVal Result As IAsyncResult)
        _SendReadPassed = pSendCommandRoutine.EndInvoke(Result)
        Interface_Abort()
        _Running = False
    End Sub

    Protected Sub SendCommandReadCallBack(ByVal Result As IAsyncResult)
        _SendReadPassed = pSendCommandReadRoutine.EndInvoke(Result)
        _ScanResult = _ReceivedString
        If _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_TIMEOUT Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_STX Or _Status = enumDevice_ErrorCodes.DEVICE_ERROR_EXT Then
            _SendReadPassed = False
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, _StatusDescription)
        Else
            RaiseEvent DataReceived(_SendReadPassed, _ScanResult, "")
        End If

        Interface_Abort()
        _Running = False
    End Sub

    Protected Sub Clear()
        _SendReadPassed = False
        _ReceivedString = ""
        _ScanResult = ""
        _Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
        _StatusDescription = ""
    End Sub

    Protected Sub _TimerCB(ByVal state As Object)
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        If _InterfaceConfig.RemoveString <> "" Then
            _ReceivedString = _ReceivedString.Replace(_InterfaceConfig.RemoveString, "")
        End If
        If _ReceivedString = "" Then
            _ReceivedString = CON_sTIMEOUT
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_TIMEOUT
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR1)
            _TimerTick = True
            Return
        Else
            If _InterfaceConfig.DataFrameSTX <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) < 0 Then
                _Status = enumDevice_ErrorCodes.DEVICE_ERROR_STX
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR2, Encoding.ASCII.GetBytes(_InterfaceConfig.DataFrameSTX)(0).ToString, Encoding.ASCII.GetBytes(_ReceivedString)(0).ToString, _ReceivedString)
                _TimerTick = True
                Return
            End If
            If _InterfaceConfig.DataFrameEXT <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) < 0 Then
                _Status = enumDevice_ErrorCodes.DEVICE_ERROR_EXT
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR3, Encoding.ASCII.GetBytes(_InterfaceConfig.DataFrameEXT)(0).ToString, Encoding.ASCII.GetBytes(_ReceivedString)(_ReceivedString.Length - 1).ToString, _ReceivedString)
                _TimerTick = True
                Return
            End If
        End If
        _ReceivedString = CON_sTIMEOUT
        _Status = enumDevice_ErrorCodes.DEVICE_ERROR_TIMEOUT
        _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR1)
        _TimerTick = True
    End Sub

    Protected Sub EventDataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        RaiseEvent DataReceived(Pass, Result, ErrorMsg)
    End Sub

    Public MustOverride Sub Interface_Abort() Implements IInterface.Interface_Abort
    Public MustOverride Sub Interface_Close() Implements IInterface.Interface_Close
    Public MustOverride Function Interface_Connect() As Boolean Implements IInterface.Interface_Connect
    Public MustOverride Function Interface_Init(ByVal InterfaceConfig As InterfaceConfig, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IInterface.Interface_Init
    Public MustOverride Function Interface_Receive() As Boolean Implements IInterface.Interface_Receive
    Public MustOverride Function Interface_Receive(ByVal iTimeOut As Integer, Optional ByVal STX As String = "", Optional ByVal EXT As String = "") As Boolean Implements IInterface.Interface_Receive
    Public MustOverride Function Interface_ReceiveCmd(ByVal mCmd As String) As Boolean
    Public MustOverride Function Interface_Transmit(ByVal Content As String) As Boolean Implements IInterface.Interface_Transmit


End Class

Public Class RS232Interface
    Inherits BaseInterface
    Protected WithEvents _Interface As New System.IO.Ports.SerialPort

    Public Overrides Sub Interface_Abort()
    End Sub

    Public Overrides Sub Interface_Close()
        On Error Resume Next
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        _TimerTick = False
        _Interface.Close()
        _Interface = Nothing
    End Sub

    Public Overrides Function Interface_Connect() As Boolean

        Try
            If Not _Interface.IsOpen Then
                _Interface.Open()
            End If
            Return _Interface.IsOpen

        Catch ex As Exception
            _StatusDescription = ex.Message
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            Return False

        End Try
    End Function

    Public Overrides Function Interface_Init(ByVal InterfaceConfig As InterfaceConfig, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        _InterfaceConfig = InterfaceConfig
        _Interface.PortName = _InterfaceConfig.RS232Port
        _Interface.BaudRate = _InterfaceConfig.BaudRate
        _Interface.DataBits = _InterfaceConfig.DataBits
        _Interface.Parity = _InterfaceConfig.Parity
        _Interface.StopBits = _InterfaceConfig.StopBits
        _Interface.Handshake = _InterfaceConfig.Handshake
        _Interface.ReceivedBytesThreshold = _InterfaceConfig.ReceivedBytesThreshold
        _TimerTick = False
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation

        Try
            If _InterfaceConfig.EventDataReceived Then AddHandler _Interface.DataReceived, AddressOf _Interface_DataReceived
            _Interface.Open()
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
            _StatusDescription = _Status.ToString
            Return _Interface.IsOpen
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_PORT_CANNOT_OPEN
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR4, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_Receive() As Boolean
        Try
            Dim NumberOfBytes As Integer, _Receive() As Char, l As Integer
            _TimerTick = False
            _Timer.Change(_ReadTimeout, Timeout.Infinite)
            Do While Not _TimerTick
                Application.DoEvents()
                NumberOfBytes = _Interface.BytesToRead
                If NumberOfBytes > 0 Then

                    ReDim _Receive(NumberOfBytes - 1)

                    _Interface.Read(_Receive, 0, NumberOfBytes)

                    For l = _Receive.GetLowerBound(0) To _Receive.GetUpperBound(0)
                        _ReceivedString = _ReceivedString & _Receive(l)
                    Next
                End If

                If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT = "" Then
                    If _ReceivedString <> "" Then
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT = "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX), _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) - _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) - Len(_InterfaceConfig.DataFrameSTX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If
            Loop

            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_Receive(ByVal iTimeOut As Integer, Optional ByVal STX As String = "", Optional ByVal EXT As String = "") As Boolean
        Try
            Dim NumberOfBytes As Integer, _Receive() As Char, l As Integer
            _TimerTick = False
            _Timer.Change(iTimeOut, Timeout.Infinite)
            Do While Not _TimerTick
                Application.DoEvents()
                NumberOfBytes = _Interface.BytesToRead
                If NumberOfBytes > 0 Then

                    ReDim _Receive(NumberOfBytes - 1)

                    _Interface.Read(_Receive, 0, NumberOfBytes)

                    For l = _Receive.GetLowerBound(0) To _Receive.GetUpperBound(0)
                        _ReceivedString = _ReceivedString & _Receive(l)
                    Next
                End If

                If STX = "" And EXT = "" Then
                    If _ReceivedString <> "" Then
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX = "" And EXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(EXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(EXT))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX <> "" And EXT = "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(STX) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(STX) + Len(STX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX <> "" And EXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(STX) >= 0 And _ReceivedString.IndexOf(EXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(STX) + Len(STX), _ReceivedString.IndexOf(EXT) - _ReceivedString.IndexOf(STX) - Len(STX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If
            Loop

            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_ReceiveCmd(ByVal mCmd As String) As Boolean
        Try
            Dim NumberOfBytes As Integer, _Receive() As Char, l As Integer
            _TimerTick = False
            _Timer.Change(_ReadTimeout, Timeout.Infinite)
            Do While Not _TimerTick
                Application.DoEvents()
                NumberOfBytes = _Interface.BytesToRead
                If NumberOfBytes > 0 Then

                    ReDim _Receive(NumberOfBytes - 1)

                    _Interface.Read(_Receive, 0, NumberOfBytes)

                    For l = _Receive.GetLowerBound(0) To _Receive.GetUpperBound(0)
                        _ReceivedString = _ReceivedString & _Receive(l)
                    Next
                End If

                If _ReceivedString.IndexOf(mCmd) >= 0 Then
                    _ReceivedString = _ReceivedString.Replace(mCmd, "")
                    EventDataReceived(True, "Revecive Command:" + mCmd, "")
                    _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                    _TimerTick = False
                    Return True
                End If
            Loop

            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Private Sub _Interface_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Try
            Dim _Receive() As Char
            _Running = True

            ReDim _Receive(_Interface.BytesToRead - 1)
            _Interface.Read(_Receive, 0, _Interface.BytesToRead)
            For l = _Receive.GetLowerBound(0) To _Receive.GetUpperBound(0)
                _ReceivedString = _ReceivedString & _Receive(l)
            Next

            If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT = "" Then
                If _ReceivedString <> "" Then
                    _ScanResult = _ReceivedString
                    _ReceivedString = ""
                    EventDataReceived(True, _ScanResult, "")
                    _Running = False
                    Return
                End If
            End If

            If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT <> "" Then
                If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                    _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT))
                    _ScanResult = _ReceivedString
                    _ReceivedString = ""
                    EventDataReceived(True, _ScanResult, "")
                    _Running = False
                    Return
                End If
            End If

            If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT = "" Then
                If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 Then
                    _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX))
                    _ScanResult = _ReceivedString
                    _ReceivedString = ""
                    EventDataReceived(True, _ScanResult, "")
                    _Running = False
                    Return
                End If
            End If

            If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT <> "" Then
                If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                    _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX), _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) - _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) - Len(_InterfaceConfig.DataFrameSTX))
                    _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                    _ScanResult = _ReceivedString
                    _ReceivedString = ""
                    EventDataReceived(True, _ScanResult, "")
                    _Running = False
                    Return
                End If
            End If

            If _ReceivedString = "" Then
                EventDataReceived(False, _ReceivedString, "Result is Null")
                _ScanResult = ""
                _Running = False
                Return
            End If

            If _InterfaceConfig.DataFrameSTX <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) < 0 Then
                EventDataReceived(False, _ReceivedString, "Receive DataFrame not Find STX: Chr(" + Encoding.ASCII.GetBytes(_InterfaceConfig.DataFrameSTX)(0).ToString + ")" + "  Data STX Chr(" + Encoding.ASCII.GetBytes(_ReceivedString)(0).ToString + ")")
                _ScanResult = ""
                _Running = False
                Return
            End If

            If _InterfaceConfig.DataFrameEXT <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) < 0 Then
                EventDataReceived(False, _ReceivedString, "Receive DataFrame not Find EXT: Chr(" + Encoding.ASCII.GetBytes(_InterfaceConfig.DataFrameEXT)(0).ToString + ")" + "  Data EXT Chr(" + Encoding.ASCII.GetBytes(_ReceivedString)(_ReceivedString.Length - 1).ToString + ")")
                _ScanResult = ""
                _Running = False
                Return
            End If

            EventDataReceived(False, _ReceivedString, "Not Find Error.")
            _ScanResult = ""
            _Running = False
            Return
        Catch ex As Exception
            _ReceivedString = ""
            _ScanResult = ex.Message
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR6, _Status.ToString, ex.Message)
            EventDataReceived(False, _ScanResult, _StatusDescription)
            _Running = False
        End Try
    End Sub

    Public Overrides Function Interface_Transmit(ByVal Content As String) As Boolean
        Dim strText As String = ""
        Try
            If Not _Interface.IsOpen Then
                _Status = enumDevice_ErrorCodes.DEVICE_ERROR_PORT_CANNOT_OPEN
                _StatusDescription = _Status.ToString
                Return False
            End If
            strText = Content
            _Interface.DiscardInBuffer()
            _Interface.DiscardOutBuffer()
            If strText <> "" Then _Interface.Write(strText)
            Return True

        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR7, _Status.ToString, ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class


Public Class TCPInterface
    Inherits BaseInterface
    Protected _Interface As Socket
    Protected _IpEndPoint As IPEndPoint
    Protected _IpAddress As IPAddress
    Protected _IpValid As Boolean
    Public Overrides Sub Interface_Abort()
        On Error Resume Next
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        _TimerTick = False
        _Interface.Shutdown(SocketShutdown.Both)
        _Interface.Disconnect(False)
        _Interface.Close()
        _Interface = Nothing
    End Sub

    Public Overrides Sub Interface_Close()
        Interface_Abort()
    End Sub

    Public Overrides Function Interface_Connect() As Boolean
        Dim _Address() As String
        Dim _ByteAddress(3) As Byte, l As Integer

        Try
            _Address = Split(_InterfaceConfig.IP, ".")

            For l = _Address.GetLowerBound(0) To _Address.GetUpperBound(0)
                _ByteAddress(l) = CType(_Address(l), Byte)
            Next

            _IpAddress = Nothing
            _IpAddress = New IPAddress(_ByteAddress)

            _IpEndPoint = Nothing
            _IpEndPoint = New IPEndPoint(_IpAddress, _InterfaceConfig.Port)


            _Interface = Nothing
            _Interface = New Socket(_IpEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            _Interface.Connect(_IpEndPoint)
            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR8, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_Init(ByVal InterfaceConfig As InterfaceConfig, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        _InterfaceConfig = InterfaceConfig
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        _Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
        _StatusDescription = _Status.ToString
        Return _CheckIp()
    End Function

    Protected Function _CheckIp() As Boolean
        Dim _Address() As String
        Dim _ByteAddress(3) As Byte, l As Integer

        _IpValid = False

        If _InterfaceConfig.Port = 0 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_IP_PORT
            _StatusDescription = _Status.ToString
            Return False
        End If

        _Address = Split(_InterfaceConfig.IP, ".")
        If _Address.GetUpperBound(0) <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_IP_ADDRESS
            _StatusDescription = _Status.ToString
        Else
            For l = _Address.GetLowerBound(0) To _Address.GetUpperBound(0)
                Try
                    _ByteAddress(l) = CType(_Address(l), Byte)
                Catch ex As Exception
                    _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
                    _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR9, _Status.ToString, ex.Message)
                    Return False
                End Try
            Next
        End If

        Try
            _IpAddress = Nothing
            _IpAddress = New IPAddress(_ByteAddress)

            _IpEndPoint = Nothing
            _IpEndPoint = New IPEndPoint(_IpAddress, _InterfaceConfig.Port)
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR
            _StatusDescription = Status.ToString
            _IpValid = True

        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR9, _Status.ToString, ex.Message)

        End Try

        Return _IpValid
    End Function

    Public Overrides Function Interface_Receive() As Boolean
        Try
            Dim Buffer(0) As Byte, _NowReceived As String = ""
            _TimerTick = False
            _Timer.Change(_ReadTimeout, Timeout.Infinite)

            Do While Not _TimerTick

                Application.DoEvents()
                If _Interface.Available <= 0 Then
                    Continue Do
                End If
                _Interface.Receive(Buffer)
                _NowReceived = Encoding.ASCII.GetString(Buffer)
                _ReceivedString = _ReceivedString + _NowReceived

                If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT = "" Then
                    If _ReceivedString <> "" Then
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT = "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If
                If _InterfaceConfig.DataFrameSTX <> "" And _InterfaceConfig.DataFrameEXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) >= 0 And _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) + Len(_InterfaceConfig.DataFrameSTX), _ReceivedString.IndexOf(_InterfaceConfig.DataFrameEXT) - _ReceivedString.IndexOf(_InterfaceConfig.DataFrameSTX) - Len(_InterfaceConfig.DataFrameSTX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If
            Loop
            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_Receive(ByVal iTimeOut As Integer, Optional ByVal STX As String = "", Optional ByVal EXT As String = "") As Boolean
        Try
            Dim Buffer(0) As Byte, _NowReceived As String = ""
            _TimerTick = False
            _Timer.Change(iTimeOut, Timeout.Infinite)

            Do While Not _TimerTick
                Application.DoEvents()
                If _Interface.Available <= 0 Then
                    Continue Do
                End If
                _Interface.Receive(Buffer)
                _NowReceived = Encoding.ASCII.GetString(Buffer)
                _ReceivedString = _ReceivedString + _NowReceived

                If _InterfaceConfig.DataFrameSTX = "" And _InterfaceConfig.DataFrameEXT = "" Then
                    If _ReceivedString <> "" Then
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX = "" And EXT = "" Then
                    If _ReceivedString <> "" Then
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX = "" And EXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(EXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(0, _ReceivedString.IndexOf(EXT))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX <> "" And EXT = "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(STX) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(STX) + Len(STX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If

                If STX <> "" And EXT <> "" Then
                    If _ReceivedString <> "" And _ReceivedString.IndexOf(STX) >= 0 And _ReceivedString.IndexOf(EXT) >= 0 Then
                        _ReceivedString = _ReceivedString.Substring(_ReceivedString.IndexOf(STX) + Len(STX), _ReceivedString.IndexOf(EXT) - _ReceivedString.IndexOf(STX) - Len(STX))
                        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                        _TimerTick = False
                        Return True
                    End If
                End If
            Loop

            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_ReceiveCmd(ByVal mCmd As String) As Boolean
        Try
            Dim Buffer(0) As Byte, _NowReceived As String = ""
            _TimerTick = False
            _Timer.Change(_ReadTimeout, Timeout.Infinite)

            Do While Not _TimerTick

                Application.DoEvents()
                If _Interface.Available <= 0 Then
                    Continue Do
                End If
                _Interface.Receive(Buffer)
                _NowReceived = Encoding.ASCII.GetString(Buffer)
                _ReceivedString = _ReceivedString + _NowReceived

                If _ReceivedString.IndexOf(mCmd) >= 0 Then
                    _ReceivedString = _ReceivedString.Replace(mCmd, "")
                    _Timer.Change(Timeout.Infinite, Timeout.Infinite)
                    _TimerTick = False
                    Return True
                End If
            Loop

            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            _TimerTick = False

            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR5, _Status.ToString, ex.Message)
            Return False
        End Try
    End Function

    Public Overrides Function Interface_Transmit(ByVal Content As String) As Boolean
        Dim Buffer(0) As Byte, strText As String

        Try
            strText = Content
            If strText <> "" Then
                Buffer = Encoding.ASCII.GetBytes(strText)
                _Interface.Send(Buffer, Buffer.GetLength(0), SocketFlags.None)
            End If
            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR7, _Status.ToString, ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class

Public Class USBInterface_Printer
    Inherits BaseInterface
    Protected _USBContent As String
    Protected WithEvents _Interface As New PrintDocument

    Public Overrides Sub Interface_Abort()
        On Error Resume Next
        _Timer.Change(Timeout.Infinite, Timeout.Infinite)
        _TimerTick = False
    End Sub

    Public Overrides Sub Interface_Close()
        Interface_Abort()
    End Sub

    Public Overrides Function Interface_Connect() As Boolean
        Return True
    End Function

    Public Overrides Function Interface_Init(ByVal InterfaceConfig As InterfaceConfig, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        Return True
    End Function


    Public Overrides Function Interface_Receive() As Boolean
        Return True
    End Function
    Public Overrides Function Interface_Receive(ByVal iTimeOut As Integer, Optional ByVal STX As String = "", Optional ByVal EXT As String = "") As Boolean
        Return False
    End Function

    Public Overrides Function Interface_ReceiveCmd(ByVal mCmd As String) As Boolean
        Return True
    End Function

    Public Overrides Function Interface_Transmit(ByVal Content As String) As Boolean
        Try
            _USBContent = Content
            _Interface.Print()
            Return True
        Catch ex As Exception
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_WINDOWS_ERROR
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_DEVICE_ERROR7, _Status.ToString, ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub _FailPrinter_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _Interface.PrintPage
        Dim My_Font As New Font("Calibri", 8)
        Try
            Try
                e.Graphics.DrawString(_USBContent, My_Font, Brushes.Black, CSng(e.MarginBounds.Left), CSng(e.MarginBounds.Top))
                _Running = False
            Catch ex As Exception

            End Try
        Catch ex As Exception
        End Try
    End Sub

End Class




