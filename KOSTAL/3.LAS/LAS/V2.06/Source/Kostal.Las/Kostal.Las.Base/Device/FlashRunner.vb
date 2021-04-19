Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Sockets
Imports Kostal.Las.Base

#Region "SMH_FlashRunner"

Public Enum enumFlasher_ErrorCodes
    ERROR_WINDOWS_ERROR = -99
    ERROR_NO_ERROR = 0
    ERROR_PORT_IS_ALWAYS_OPEN
    ERROR_PORT_CANNOT_OPEN
    ERROR_INTERFACE_IS_NOT_MSCOMM
    ERROR_FILE_NOT_FOUND
    ERROR_NO_RESPONSE
    ERROR_INVALID_IP_ADDRESS
    ERROR_INVALID_IP_PORT
End Enum

Public Enum FR_StatusCode
    LaserStatusStandby = 11
    LaserStatusReady = 10
    InConfigurationMode = 9
    InCheckIpMode = 8
    InConnectionToTransmitInterface = 7
    InConnectionToReceiveInterface = 6
    InPingMode = 5
    InReceiveMode = 4
    InSendStartMode = 3
    InConnectMode = 2
    IsNotConnected = 1
    Ready = 0
    InvalidIpAddress = -1
    ConnectionToReceiveInterfaceNotPossible = -2
    ConnectionToTransmitInterfaceNotPossible = -3
    DeviceNotAvailable = -4
    InvalidResponse = -5
    InvalidStepNumber = -6
    NotReady = -7
    InvalidConfig = -8
    WindowsError = -99
End Enum


Public Interface IFlashBase
    ReadOnly Property FlasherName As String
    ReadOnly Property Port() As Int32
    ReadOnly Property Status() As FR_StatusCode
    ReadOnly Property StatusDescription() As String
    ReadOnly Property IsError() As Boolean
    ReadOnly Property IsBusy() As Boolean

    ReadOnly Property LastResponse() As String

    Function SendRead(ByVal Fileds As String()) As Boolean
    Sub ResetLastResponse()
    Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Sub Dispose()
End Interface

Public Class Flash
    Implements IFlash
    Protected _iFlash As IFlashBase
    Protected _DeviceType As DeviceType
    Public ReadOnly Property FlasherName As String Implements IFlash.FlasherName
        Get
            If IsNothing(_iFlash) Then Return ""
            Return _iFlash.FlasherName
        End Get
    End Property

    Public ReadOnly Property IsBusy As Boolean Implements IFlash.IsBusy
        Get
            If IsNothing(_iFlash) Then Return True
            Return _iFlash.IsBusy
        End Get
    End Property

    Public ReadOnly Property IsError As Boolean Implements IFlash.IsError
        Get
            If IsNothing(_iFlash) Then Return True
            Return _iFlash.IsError
        End Get
    End Property

    Public ReadOnly Property LastResponse As String Implements IFlash.LastResponse
        Get
            If IsNothing(_iFlash) Then Return ""
            Return _iFlash.LastResponse
        End Get
    End Property

    Public ReadOnly Property Port As Integer Implements IFlash.Port
        Get
            If IsNothing(_iFlash) Then Return 0
            Return _iFlash.Port
        End Get
    End Property

    Public ReadOnly Property Status As FR_StatusCode Implements IFlash.Status
        Get
            If IsNothing(_iFlash) Then Return FR_StatusCode.WindowsError
            Return _iFlash.Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String Implements IFlash.StatusDescription
        Get
            If IsNothing(_iFlash) Then Return ""
            Return _iFlash.StatusDescription
        End Get
    End Property

    Public Sub Dispose() Implements IFlash.Dispose
        If IsNothing(_iFlash) Then Return
        _iFlash.Dispose()
    End Sub

    Public Function Init(mType As LasDeviceType, mConfig As String, MyStation As Station, AppSettings As Settings, MyLanguage As Language) As Boolean Implements IFlash.Init
        If mType = LasDeviceType.FR01_LAN Then
            _iFlash = New SMH_FlashRunner()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.FR02_LAN Then
            _iFlash = New SMH_Mulit_FlashRunner()
            _DeviceType = DeviceType.LAN
            Else
            Return False
        End If
        If IsNothing(_iFlash) Then Return False
        If Not _iFlash.Init(_DeviceType, mConfig, MyStation, AppSettings, MyLanguage) Then Return False
        Return True

    End Function

    Public Function SendRead(ByVal Fileds As String()) As Boolean Implements IFlash.SendRead
        If IsNothing(_iFlash) Then Return False
        _iFlash.SendRead(Fileds)
        Return True
    End Function

    Public Sub ResetLastResponse() Implements IFlash.ResetLastResponse
        If IsNothing(_iFlash) Then Return
        _iFlash.ResetLastResponse()
    End Sub
End Class



Public Class SMH_Mulit_FlashRunner
    Inherits SMH_FlashRunner
    Public Overrides Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        If mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = FR_StatusCode.InvalidConfig
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        _Port = CInt(mConfig.Split(CChar(","))(1))
        _IP = mConfig.Split(CChar(","))(0)
        _PortName = "LAN"
        _FlasherName = "FR01LAN"

        _MilliSecondsTimerOut = CInt(mConfig.Split(CChar(","))(2))
        _StepCurrentNumber = 0
        _StepNextNumber = 0

        _Flasher = New clsMuliteFlashRunner(MyStation, _AppSettings, MyLanguage)

        _Error = False
        _StopThread = True

        AdressInit()
        _CheckIp()
        If _Error Then Return False
        Return True
    End Function

End Class
Public Class SMH_FlashRunner
    Implements IFlashBase

    Friend IsDisposed As Boolean

    Friend _FlasherName As String

    Friend _Status As FR_StatusCode
    Friend _StatusDescription As String

    Friend _IP As String
    Friend _IpAddress As IPAddress
    Friend _IpValid As Boolean

    Friend _StepNextNumber As Long
    Friend _StepCurrentNumber As Long
    Friend _Toggle As Boolean

    Friend _Address_Home As Long

    Friend _Port As Int32
    Friend _EndPoint As IPEndPoint

    Friend _IsConnected As Boolean
    Friend _LastResponse As String
    Friend _NowReceived As String

    Friend Delegate Sub DelegateMain(ByVal Fileds As String())
    Friend pMain As New DelegateMain(AddressOf Main)
    Friend pMainCallBack As New AsyncCallback(AddressOf MainCallBack)
    Friend _MainResult As IAsyncResult
    Friend _StopThread As Boolean
    Friend _IsBusy As Boolean

    Friend _Error As Boolean
    Friend _GetPing_Successful As Boolean
    Friend _MilliSecondsTimerOut As Integer

    Friend _Flasher As IFlashRunner

    Public Event Aborted(ByVal Message As String)
    Public Event Finish()

    Public Const _Delimiter As Char = ";"c
    Public Const ACK As String = ">"

    Friend _PortName As String = ""

#Region ""

    Public ReadOnly Property FlasherName As String Implements IFlashBase.FlasherName
        Get
            Return _FlasherName
        End Get
    End Property

    Public ReadOnly Property Port() As Int32 Implements IFlashBase.Port
        Get
            Return _Port
        End Get
    End Property

    Public ReadOnly Property Status() As FR_StatusCode Implements IFlashBase.Status
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements IFlashBase.StatusDescription
        Get
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property IsError() As Boolean Implements IFlashBase.IsError
        Get
            Return _Error
        End Get
    End Property

    Public ReadOnly Property IsBusy() As Boolean Implements IFlashBase.IsBusy
        Get
            Return _IsBusy
        End Get
    End Property

    Public ReadOnly Property LastResponse() As String Implements IFlashBase.LastResponse
        Get
            Return _LastResponse
        End Get
    End Property

#End Region

    Public Overridable Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements IFlashBase.Init
        If mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = FR_StatusCode.InvalidConfig
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        _Port = CInt(mConfig.Split(CChar(","))(1))
        _IP = mConfig.Split(CChar(","))(0)
        _PortName = "LAN"
        _FlasherName = "FR01LAN"

        _MilliSecondsTimerOut = CInt(mConfig.Split(CChar(","))(2))
        _StepCurrentNumber = 0
        _StepNextNumber = 0

        _Flasher = New clsFlashRunner(MyStation, _AppSettings, MyLanguage)

        _Error = False
        _StopThread = True

        AdressInit()
        _CheckIp()
        If _Error Then Return False
        Return True
    End Function


#Region "IDisposable Support"

    Public Sub Dispose() Implements IFlashBase.Dispose
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next

        _StopThread = True
        If _IsConnected Then
            _IsConnected = Not _Flasher.CloseDevice()
        End If

        pMain.EndInvoke(_MainResult)

        IsDisposed = True
        MyBase.Finalize()
    End Sub
#End Region

    Public Function SendRead(ByVal Fileds As String()) As Boolean Implements IFlashBase.SendRead

        If _IsBusy Then Return False
        If _IpValid Then
            _StopThread = False
            _Error = False
            _IsBusy = True
            _StepNextNumber = _Address_Home
            _MainResult = pMain.BeginInvoke(Fileds, pMainCallBack, pMain)
            Return True
        Else
            _IsBusy = False
            Return False
        End If

    End Function

    Friend Sub Main(ByVal Fileds As String())
        Try


            Do
                System.Threading.Thread.Sleep(10)
                If _StopThread Then Exit Do

                _Toggle = _StepCurrentNumber <> _StepNextNumber
                _StepCurrentNumber = _StepNextNumber
                Select Case _StepCurrentNumber

                    Case 0 : a_SoftwareHome()
                    'set cmds
                    Case 1 : a_GetPing()
                    Case 2 : a_Connect()
                    Case 3
                        For i = 0 To Fileds.Count - 1
                            a_SetAnyCommand(Fileds(i))
                        Next

                    Case 4 : a_ReceiveResponse()
                    Case 5 : a_Ready()

                    Case Else
                        _Status = FR_StatusCode.InvalidStepNumber
                        _StatusDescription = _Status.ToString & _Delimiter & "STEPNUMBER:" & CStr(_StepCurrentNumber)
                        Abort()
                End Select
            Loop Until _StopThread
        Catch ex As Exception

            Abort()
        End Try
    End Sub


    Friend Sub MainCallBack(ByVal Response As IAsyncResult)

        pMain.EndInvoke(_MainResult)
        On Error Resume Next

        If _IsConnected Then
            _IsConnected = Not _Flasher.CloseDevice()
        End If

        _IsBusy = False
        _StopThread = False
        On Error GoTo 0

    End Sub

    Friend Sub AdressInit()

        _Address_Home = 0
        _StepNextNumber = 0
        _StepCurrentNumber = -1

    End Sub

    Public Sub Reset()
        Abort()
        _Error = False
    End Sub

    Public Sub ResetLastResponse() Implements IFlashBase.ResetLastResponse
        _LastResponse = ""
    End Sub

    Private Sub a_SoftwareHome()

        If Not _IpValid Then
            '
        ElseIf _Error Then
            '
        Else
            _Error = False
            IncrementStepNumber()

            _Status = FR_StatusCode.Ready
            _StatusDescription = _Status.ToString
        End If
    End Sub

    Friend Sub _CheckIp()
        Dim _Address() As String
        Dim _ByteAddress(3) As Byte, l As Integer

        _IpValid = False
        _Status = FR_StatusCode.InCheckIpMode
        _StatusDescription = _Status.ToString

        _Address = Split(_IP, ".")
        If _Address.GetUpperBound(0) <> 3 Then
            _Status = FR_StatusCode.InvalidIpAddress
            _StatusDescription = _Status.ToString
            _Error = True
        Else
            For l = _Address.GetLowerBound(0) To _Address.GetUpperBound(0)
                Try
                    _ByteAddress(l) = CType(_Address(l), Byte)
                Catch ex As Exception
                    _Status = FR_StatusCode.WindowsError
                    _StatusDescription = "_CheckIp" & _Delimiter & _Status.ToString & _Delimiter & ex.Message
                    _Error = True
                    Exit For
                End Try
            Next
        End If

        If Not My.Computer.Network.Ping(_IP) Then
            _GetPing_Successful = False
            _Status = FR_StatusCode.DeviceNotAvailable
            _StatusDescription = "_CheckIp" & _Delimiter & _IP & _Delimiter & "Fail"
            _Error = True
        End If

        If Not _Error Then
            '_IpAddress = Nothing
            '_IpAddress = New IPAddress(_ByteAddress)

            '_EndPoint = Nothing
            '_EndPoint = New IPEndPoint(_IpAddress, _Port)
            _Status = FR_StatusCode.Ready

            _IpValid = True
        End If

    End Sub

    Friend Sub a_GetPing()
        _Status = FR_StatusCode.InPingMode
        _StatusDescription = _Status.ToString

        Try
            If Not My.Computer.Network.Ping(_IP) Then
                _GetPing_Successful = False
                _Status = FR_StatusCode.DeviceNotAvailable
                _StatusDescription = "Ping" & _Delimiter & _IP & _Delimiter & "Fail"
                Abort()
            Else
                _GetPing_Successful = True
                IncrementStepNumber()
            End If
        Catch ex As Exception
            _Status = FR_StatusCode.WindowsError
            _StatusDescription = _Status.ToString & _Delimiter & ex.Message
            Abort()
        End Try
    End Sub

    Friend Sub a_Connect()
        _Status = FR_StatusCode.InConnectionToReceiveInterface
        _StatusDescription = _Status.ToString

        Try
            If Not _Flasher.OpenDevice(_PortName, _IP & ":" & _Port) And Not _Error Then
                _Status = FR_StatusCode.ConnectionToReceiveInterfaceNotPossible
                _StatusDescription = _Status.ToString & _Delimiter & "Port:" & CStr(_Port)
                Abort()
            ElseIf Not _Error Then
                _IsConnected = True
                IncrementStepNumber()
            End If
        Catch ex As Exception
            _Status = FR_StatusCode.WindowsError
            _StatusDescription = _Status.ToString & _Delimiter & ex.Message
            Abort()
        End Try
    End Sub

    Friend Sub a_SetAnyCommand(ByVal cmd As String)

        _Status = FR_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Try
            _NowReceived = ""
            If Not _Flasher.SendCmd(cmd) And Not _Error Then
                _Status = FR_StatusCode.ConnectionToTransmitInterfaceNotPossible
                _StatusDescription = _Status.ToString & _Delimiter & "IP:" & _IP & _Delimiter & "Port:" & CStr(_Port)
                Abort()

            ElseIf Not _Error Then
                IncrementStepNumber()
            End If

        Catch ex As Exception
            _Status = FR_StatusCode.WindowsError
            _StatusDescription = _Status.ToString & _Delimiter & ex.Message
            Abort()
        End Try

    End Sub

    Friend Sub a_ReceiveResponse()

        _Status = FR_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString

        Try
            If Not _Flasher.GetAns(_NowReceived) Then

                _Status = FR_StatusCode.ConnectionToReceiveInterfaceNotPossible
                _StatusDescription = _Status.ToString & _Delimiter & "IP:" & _IP & _Delimiter & "Port:" & CStr(_Port)
                Abort()

            End If

            _LastResponse = _NowReceived
            _NowReceived = ""

            If _LastResponse = "" Then
                _Status = FR_StatusCode.InvalidResponse
                _StatusDescription = _Status.ToString
                Abort()
            Else
                IncrementStepNumber()
            End If

        Catch ex As Exception
            If Not _Error Then
                _Status = FR_StatusCode.WindowsError
                _StatusDescription = _Status.ToString & _Delimiter & ex.Message
            End If
            Abort()
            Return
        End Try

    End Sub

    Friend Sub Abort()
        _Error = True
        _StopThread = True

        On Error Resume Next
        If _IsConnected Then
            _IsConnected = Not _Flasher.CloseDevice()
        End If
        _NowReceived = ""
        _LastResponse = ""

        RaiseEvent Aborted(_StatusDescription)
        On Error GoTo 0

        _StepNextNumber = _Address_Home

    End Sub

    Friend Sub a_Ready()

        _Error = False
        _StopThread = True
        _Status = FR_StatusCode.Ready
        _StatusDescription = _Status.ToString
        RaiseEvent Finish()
        _StepNextNumber = _Address_Home

    End Sub

    Friend Sub IncrementStepNumber()
        _StepNextNumber = _StepCurrentNumber + 1
    End Sub


End Class

#End Region

#Region "clsFlashRunner"


Public Interface IFlashRunner
    Function OpenDevice(ByVal sPort As String, ByVal sCommSettings As String) As Boolean
    Function CloseDevice() As Boolean
    Function SendCmd(ByVal cmd As String) As Boolean
    Function GetAns(ByRef answer As String) As Boolean

End Interface
Public Class clsFlashRunner
    Implements IFlashRunner
    Private _hFlasher As IntPtr

    Public Delegate Function FR_FileTransferProgressProc(ByVal file_size As UInteger, ByVal file_pos As UInteger) As UInteger
    Public Delegate Function FR_ExeCommandProgressProc(ByVal progress As UInteger) As UInteger

    Public Enum FR_ANSWER_TYPE As Byte
        FR_ANSWER_ACK = &H3E
        FR_ANSWER_NACK = &H21
        FR_ANSWER_TOUT = 0
    End Enum

    <DllImport("FR_COMM.dll", EntryPoint:="FR_OpenCommunicationA")>
    Public Shared Function FR_OpenCommunication(ByVal port As String, ByVal comSettings As String) As IntPtr
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_CloseCommunicationA")>
    Public Shared Function FR_CloseCommunication(ByVal hComm As IntPtr) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_SendCommandA")>
    Public Shared Function FR_SendCommand(ByVal hComm As IntPtr, ByVal cmd As String) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_GetAnswerA")>
    Public Shared Function FR_GetAnswer(ByVal hComm As IntPtr, ByVal answer As IntPtr, ByVal maxlen As UInteger, ByVal timeout_ms As UInteger) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_ExeCommandA")>
    Public Shared Function FR_ExeCommand(ByVal hComm As IntPtr,
                                         ByVal cmd As String,
                                         ByVal answer As IntPtr,
                                         ByVal maxlen As UInteger,
                                         ByVal timeout_ms As UInteger,
                                         ByRef type As FR_ANSWER_TYPE) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_SendFileA")>
    Public Shared Function FR_SendFile(ByVal hComm As IntPtr,
                                         ByVal protocol As String,
                                         ByVal srcfilename As String,
                                         ByVal dstpath As String,
                                         ByVal progress As FR_FileTransferProgressProc
                                         ) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_GetFileA")>
    Public Shared Function FR_GetFile(ByVal hComm As IntPtr,
                                         ByVal protocol As String,
                                         ByVal srcfilename As String,
                                         ByVal dstpath As String,
                                         ByVal progress As FR_FileTransferProgressProc
                                         ) As UInteger
    End Function

    <DllImport("FR_COMM.dll", EntryPoint:="FR_ExeCommand_CBA")>
    Public Shared Function FR_ExeCommand_CB(ByVal hComm As IntPtr,
                                         ByVal cmd As String,
                                         ByVal answer As IntPtr,
                                         ByVal maxlen As UInteger,
                                         ByVal timeout_ms As UInteger,
                                         ByRef type As FR_ANSWER_TYPE,
                                         ByVal progress As FR_ExeCommandProgressProc) As UInteger
    End Function

    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language)

    End Sub
    Public Function OpenDevice(ByVal sPort As String, ByVal sCommSettings As String) As Boolean Implements IFlashRunner.OpenDevice
        _hFlasher = FR_OpenCommunication(sPort, sCommSettings)

        Return _hFlasher <> IntPtr.Zero
    End Function

    Public Function CloseDevice() As Boolean Implements IFlashRunner.CloseDevice
        Dim iResult As Integer = -999

        If _hFlasher <> IntPtr.Zero Then
            iResult = CInt(FR_CloseCommunication(_hFlasher))
        End If
        Return iResult = 0
    End Function

    Public Function SendCmd(ByVal cmd As String) As Boolean Implements IFlashRunner.SendCmd
        If _hFlasher = IntPtr.Zero Then Return False

        Dim nRet As Integer = CInt(FR_SendCommand(_hFlasher, cmd))

        Return nRet = 0
    End Function

    Public Function GetAns(ByRef answer As String) As Boolean Implements IFlashRunner.GetAns
        If _hFlasher = IntPtr.Zero Then Return False
        Dim buffer As IntPtr = Marshal.AllocHGlobal(1000)

        Dim nRet As Integer = CInt(FR_GetAnswer(_hFlasher, buffer, 1000, 10000))

        If nRet = 0 Then
            answer = Marshal.PtrToStringAnsi(buffer)

        End If
        Marshal.FreeHGlobal(buffer)

        Return nRet = 0
    End Function

    Public Function SendFile(ByVal sFileName As String, ByVal sDstPath As String, ByVal progress As FR_FileTransferProgressProc) As Boolean
        If _hFlasher = IntPtr.Zero Then Return False

        Dim nRet As UInteger = FR_SendFile(_hFlasher, "YMODEM", sFileName, sDstPath, progress)

        Return nRet = 0
    End Function

    Public Function ExeCommand(ByVal cmd As String, ByRef answer As String, ByVal timeouts As UInteger, ByRef type As FR_ANSWER_TYPE, ByVal progress As FR_ExeCommandProgressProc) As Boolean
        Dim nRet As UInteger = 0
        If _hFlasher = IntPtr.Zero Then Return False

        Dim buffer As IntPtr = Marshal.AllocHGlobal(2000)

        nRet = FR_ExeCommand_CB(_hFlasher, cmd, buffer, 2000, timeouts, type, progress)

        If nRet = 0 Then
            answer = Marshal.PtrToStringAnsi(buffer)

        End If
        Marshal.FreeHGlobal(buffer)
        Return nRet = 0
    End Function

End Class


Public Class clsMuliteFlashRunner
    Implements IFlashRunner
    Protected _InterfaceConfig As New InterfaceConfig
    Public _Interface As TCPIPClient
    Public MyStation As Station
    Public _AppSettings As Settings
    Public MyLanguage As Language
    Public iNumber As Integer = 0

    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language)
        Me.MyStation = MyStation
        Me._AppSettings = _AppSettings
        Me.MyLanguage = MyLanguage
    End Sub
    Public Function CloseDevice() As Boolean Implements IFlashRunner.CloseDevice
        Return _Interface.Quit
    End Function

    Public Function GetAns(ByRef answer As String) As Boolean Implements IFlashRunner.GetAns
        Try
            Dim iCnt As Integer = 100
            Dim strReceive As String = ""
            While iCnt > 0
                strReceive = _Interface.Read()
                Dim str1 As String = strReceive
                If strReceive.IndexOf("!") >= 0 Then
                    answer = strReceive
                    Return False
                End If
                Dim str2 As String = strReceive.Replace(SMH_FlashRunner.ACK, "")
                If iNumber = str1.Length - str2.Length Then
                    answer = strReceive
                    Return True
                End If
                System.Threading.Thread.Sleep(100)
            End While
            answer = strReceive
            Return False
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Function OpenDevice(sPort As String, sCommSettings As String) As Boolean Implements IFlashRunner.OpenDevice
        _InterfaceConfig.IP = sCommSettings.Split(":")(0)
        _InterfaceConfig.Port = Integer.Parse(sCommSettings.Split(":")(1))
        _Interface = New TCPIPClient
        _Interface.Init(_InterfaceConfig.IP, _InterfaceConfig.Port)
        _Interface.response = ""
        iNumber = 0
        Return True
    End Function

    Public Function SendCmd(cmd As String) As Boolean Implements IFlashRunner.SendCmd
        Try
            iNumber = iNumber + 1
            _Interface.Send(cmd + vbCrLf)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function
End Class



#End Region
