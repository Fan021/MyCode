
'Version 1.2.0.0 Build 2015_10_15
'				Second build
'added interface which is compatible with Foba DP10

'Version 1.1.0.0 Build 2015_05_17
'				First build

Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Enum enumLaserType
    DP10GS = 0
    LF100
End Enum
Public Interface ILaserBase
    ReadOnly Property ReadyToWrite As Boolean
    ReadOnly Property Status As Alltec_StatusCode
    ReadOnly Property LastResponse As String
    ReadOnly Property StatusDescription As String
    Sub ResetLastResponse()
    Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Function Start() As Boolean
    Function StartReady() As Boolean
    Function GetStatus() As Boolean
    Function GetStatusReady(ByVal mTemplateName As String) As Boolean
    Function GetVar() As Boolean
    Function GetVarReady() As Boolean
    Function GetGetTemplate() As Boolean
    Function GetGetTemplateReady() As Boolean
    Function SetAnyCommand(ByVal cmd As String) As Boolean
    Function SetAnyCommandReady(ByVal cmd As String) As Boolean
    Function SetAndGetTemplate(ByVal name As String) As Boolean
    Function SetTemplateReady(ByVal name As String) As Boolean
    Sub Dispose()
End Interface

Public Class Laser
    Implements ILaser
    Protected _iLaser As ILaserBase
    Protected _DeviceType As DeviceType
    Public ReadOnly Property ReadyToWrite As Boolean Implements ILaser.ReadyToWrite
        Get
            If IsNothing(_iLaser) Then Return False
            Return _iLaser.ReadyToWrite
        End Get
    End Property
    Public ReadOnly Property Status As Alltec_StatusCode Implements ILaser.Status
        Get
            If IsNothing(_iLaser) Then Return Alltec_StatusCode.NotReady
            Return _iLaser.Status
        End Get
    End Property

    Public ReadOnly Property LastResponse As String Implements ILaser.LastResponse
        Get
            If IsNothing(_iLaser) Then Return ""
            Return _iLaser.LastResponse
        End Get
    End Property

    Public ReadOnly Property StatusDescription As String Implements ILaser.StatusDescription
        Get
            If IsNothing(_iLaser) Then Return ""
            Return _iLaser.StatusDescription
        End Get
    End Property


    Public Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements ILaser.Init
        If mType = LasDeviceType.DP10GS_LAN Then
            _iLaser = New AlltecFoba_Laser_DP10GS()
            _DeviceType = DeviceType.LAN
        ElseIf mType = LasDeviceType.LF100_LAN Then
            _iLaser = New AlltecFoba_Laser_LF100()
            _DeviceType = DeviceType.LAN
        Else
            Return False
        End If
        If IsNothing(_iLaser) Then Return False
        If Not _iLaser.Init(_DeviceType, mConfig, MyStation, _AppSettings, MyLanguage) Then Return False
        Return True
    End Function

    Public Sub ResetLastResponse() Implements ILaser.ResetLastResponse
        If IsNothing(_iLaser) Then Return
        _iLaser.ResetLastResponse()
    End Sub

    Public Function Start() As Boolean Implements ILaser.Start
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.Start
    End Function

    Public Function StartReady() As Boolean Implements ILaser.StartReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.StartReady()
    End Function

    Public Function GetStatus() As Boolean Implements ILaser.GetStatus
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetStatus
    End Function

    Public Function GetStatusReady(ByVal mTemplateName As String) As Boolean Implements ILaser.GetStatusReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetStatusReady(mTemplateName)
    End Function


    Public Function GetVar() As Boolean Implements ILaser.GetVar
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetVar
    End Function

    Public Function GetVarReady() As Boolean Implements ILaser.GetVarReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetVarReady()
    End Function


    Public Function GetGetTemplate() As Boolean Implements ILaser.GetGetTemplate
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetGetTemplate
    End Function

    Public Function GetGetTemplateReady() As Boolean Implements ILaser.GetGetTemplateReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.GetGetTemplateReady()
    End Function

    Public Function SetAndGetTemplate(ByVal name As String) As Boolean Implements ILaser.SetAndGetTemplate
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.SetAndGetTemplate(name)
    End Function

    Public Function SetAnyCommand(ByVal cmd As String) As Boolean Implements ILaser.SetAnyCommand
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.SetAnyCommand(cmd)
    End Function

    Public Function SetAnyCommandReady(ByVal cmd As String) As Boolean Implements ILaser.SetAnyCommandReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.SetAnyCommandReady(cmd)
    End Function

    Public Function SetTemplateReady(ByVal name As String) As Boolean Implements ILaser.SetTemplateReady
        If IsNothing(_iLaser) Then Return False
        Return _iLaser.SetTemplateReady(name)
    End Function

    Public Sub Dispose() Implements ILaser.Dispose
        If IsNothing(_iLaser) Then Return
        _iLaser.Dispose()
    End Sub
End Class



Public Enum Alltec_StatusCode
    LaserTemplateName = 12
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
    WindowsError = -99
End Enum

Public Class AlltecFoba_Laser_DP10GS
    Implements ILaserBase

    Protected Enum enumRequestActionType
        DEVICE_IDLE = 0
        REQUEST_TEMPLATE_CHANGE = 1
        REQUEST_VARIABLE_REPLACE = 2
        REQUEST_SEND_ANY_CMD = 3
    End Enum

    Protected IsDisposed As Boolean

    Protected _LaserType As enumLaserType = enumLaserType.LF100
    Protected _Status As Alltec_StatusCode
    Protected _StatusDescription As String

    Protected _IP As String
    Protected _IpAddress As IPAddress
    Protected _IpValid As Boolean

    Protected _Interface As Socket
    Protected _Port As Int32
    Protected _EndPoint As IPEndPoint

    Protected _IsConnected As Boolean
    Protected _LastResponse As String

    Protected _Buffer(255) As Byte
    Protected _NowReceived As String

    Protected _StepNextNumber As Long
    Protected _StepCurrentNumber As Long
    Protected _Toggle As Boolean

    Protected _Address_Home As Long
    Protected _Address_SetTemplate As Long
    Protected _Address_SetAnyCmd As Long

    Protected _currentActionType As enumRequestActionType
    'Private _SetVariableRequest As Boolean
    'Private _SetTemplateRequest As Boolean
    'Private _SetAnyCmdRequest As Boolean

    Protected _Error As Boolean
    Protected _GetPing_Successful As Boolean
    Protected _MilliSecondsTimerOut As Integer

    Protected Delegate Sub DelegateMain()
    Protected pMain As New DelegateMain(AddressOf Main)
    Protected pMainCallBack As New AsyncCallback(AddressOf MainCallBack)
    Protected _MainResult As IAsyncResult
    Protected _StopThread As Boolean
    Protected _GetTempCount As Integer
    'Private _CommAborted As Boolean

    Public Event Aborted(ByVal Message As String)
    Public Event Finish()

    Protected _cmdAnyCommand As String = ""
    Protected _cmdTemplateName As String = ""
    Protected _cmdVariableName As String = ""
    Protected _cmdVariableValue As String = ""

    Protected Const CON_VARIABLE_NAME As String = "Barcode"
    'LF100
    Public Const _Delimiter As Char = ";"c
    Public Const CMD_SETJOB As String = "SetJob"
    Public Const CMD_GETJOB As String = "GetJob"
    Public Const CMD_SETVARS As String = "SetVars"
    Public Const CMD_GETVARS As String = "GetVars"
    Public Const CMD_GETSTATUS As String = "GetStatus"
    Public Const CMD_ACK As String = "0000"
    Public Const CMD_START As String = "Start"
    'DP10GS
    Public Const _Colon As Char = ":"c
    Public Const _FileSuffix As String = ".mjb"
    Public Const CMD_DP10GS_LOADJOB As String = "LoadJob"   'format as --> Loadjob:<TemplateNmae>.mjb;
    Public Const CMD_DP10GS_GETJOBNAME As String = "GetJobName"
    Public Const CMD_DP10GS_ACK As String = "1"
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station

#Region "Properties"

    Public ReadOnly Property LaserType As enumLaserType
        Get
            Return _LaserType
        End Get
    End Property

    Public ReadOnly Property TemplateName As String
        Get
            Return _cmdTemplateName
        End Get
    End Property

    Public ReadOnly Property VariableValue As String
        Get
            Return _cmdVariableValue
        End Get
    End Property

    Public ReadOnly Property ReadyToWrite As Boolean Implements ILaserBase.ReadyToWrite
        Get
            Return _currentActionType = enumRequestActionType.DEVICE_IDLE
        End Get
    End Property

    Public ReadOnly Property Port() As Int32
        Get
            Return _Port
        End Get
    End Property

    Public ReadOnly Property Status() As Alltec_StatusCode Implements ILaserBase.Status
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String Implements ILaserBase.StatusDescription
        Get
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property IsError() As Boolean
        Get
            Return _Error
        End Get
    End Property

    Public ReadOnly Property CurrentActionType() As String
        Get
            Return _currentActionType.ToString
        End Get
    End Property

    Public ReadOnly Property LastResponse() As String Implements ILaserBase.LastResponse
        Get
            Return _LastResponse
        End Get
    End Property

    Public Property DeviceInterface() As Socket
        Set(ByVal value As Socket)
            _Interface = value
        End Set
        Get
            Return _Interface
        End Get
    End Property

#End Region

    Public Sub New()
        _LaserType = enumLaserType.DP10GS
    End Sub

    Public Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean Implements ILaserBase.Init
        _currentActionType = enumRequestActionType.DEVICE_IDLE
        If mConfig.Split(CChar(",")).Length <> 2 Then
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        _Port = CInt(mConfig.Split(CChar(","))(1))
        _IP = mConfig.Split(CChar(","))(0)
        _MilliSecondsTimerOut = 10000
        _StepCurrentNumber = 0
        _StepNextNumber = 0
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        _Error = False
        _StopThread = True
        AdressInit()
        _CheckIp()
        If Status <> Alltec_StatusCode.Ready Then Return False
        Return True
    End Function

    Public Sub Dispose() Implements ILaserBase.Dispose
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next

        _StopThread = True
        _Interface.Shutdown(SocketShutdown.Both)
        _Interface.Disconnect(False)
        _Interface.Close()
        _Interface = Nothing

        pMain.EndInvoke(_MainResult)

        IsDisposed = True
        MyBase.Finalize()
    End Sub

    Public Overridable Sub Main()
        Do
            System.Threading.Thread.Sleep(10)
            If _StopThread Then Exit Do

            _Toggle = _StepCurrentNumber <> _StepNextNumber
            _StepCurrentNumber = _StepNextNumber
            Select Case _StepCurrentNumber

                Case 0 : a_SoftwareHome()
                    'set and get Variables
                Case 1 : a_GetPing()
                Case 2 : a_Connect()
                Case 3 : a_SendVariable()
                Case 4 : a_ReceivePositiveResponse()
                Case 5 : a_SendCommand(CMD_GETVARS & _Colon)
                Case 6 : a_ReceiveResponse()
                Case 7 : a_Ready()
                    'set and get specific Template
                Case 10 : a_GetPing()
                Case 11 : a_Connect()
                Case 12 : a_SetTemplate()
                Case 13 : a_ReceivePositiveResponse()
                Case 14 : a_SendCommand(CMD_DP10GS_GETJOBNAME & _Colon)
                Case 15 : a_TempleReceiveResponse()
                Case 16 : a_Ready()
                    'set any command
                Case 20 : a_GetPing()
                Case 21 : a_Connect()
                Case 22 : a_SetAnyCommand()
                Case 23 : a_ReceivePositiveResponse()
                Case 24 : a_Ready()

                Case Else
                    _Status = Alltec_StatusCode.InvalidStepNumber
                    _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR1, _Status.ToString, _StepCurrentNumber.ToString)

                    Abort()
            End Select
        Loop Until _StopThread
    End Sub

    Protected Sub MainCallBack(ByVal Response As IAsyncResult)

        pMain.EndInvoke(_MainResult)
        On Error Resume Next
        _Interface.Shutdown(SocketShutdown.Both)
        _Interface.Disconnect(False)
        _Interface.Close()
        _StopThread = False
        _currentActionType = enumRequestActionType.DEVICE_IDLE
        On Error GoTo 0

    End Sub

    Protected Sub AdressInit()
        _Address_Home = 0
        _Address_SetTemplate = 10
        _Address_SetAnyCmd = 20
        _StepNextNumber = 0
        _StepCurrentNumber = -1
    End Sub

    Public Sub Reset()
        Abort()
        _Error = False
    End Sub

    Public Sub ResetLastResponse() Implements ILaserBase.ResetLastResponse
        _LastResponse = ""
    End Sub

    Public Overridable Function GetStatus() As Boolean Implements ILaserBase.GetStatus
        SetAnyCommand(CMD_DP10GS_GETJOBNAME & _Colon)
        Return True
    End Function

    Public Overridable Function Start() As Boolean Implements ILaserBase.Start
        SetAnyCommand(CMD_START & _Colon)
        Return True
    End Function

    Public Overridable Function GetVar() As Boolean Implements ILaserBase.GetVar
        SetAnyCommand(CMD_GETVARS & _Colon)
        Return True
    End Function

    Public Overridable Function GetGetTemplate() As Boolean Implements ILaserBase.GetGetTemplate
        SetAnyCommand(CMD_DP10GS_GETJOBNAME & _Colon)
        Return True
    End Function

    Public Overridable Function GetStatusReady(ByVal mTemplateName As String) As Boolean Implements ILaserBase.GetStatusReady
        If Not _LastResponse.Contains(mTemplateName) Then
            _Status = Alltec_StatusCode.NotReady
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If
        Return True
    End Function

    Public Overridable Function GetStartReady() As Boolean Implements ILaserBase.StartReady
        If _LastResponse = "" Then
            _Status = Alltec_StatusCode.InvalidResponse
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If
        Return True
    End Function

    Public Overridable Function GetVarReady() As Boolean Implements ILaserBase.GetVarReady
        If _LastResponse = "" Then
            _Status = Alltec_StatusCode.InvalidResponse
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If
        Return True
    End Function

    Public Overridable Function GetGetTemplateReady() As Boolean Implements ILaserBase.GetGetTemplateReady
        If _LastResponse = "" Then
            _Status = Alltec_StatusCode.InvalidResponse
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If
        Return True
    End Function

    Public Function SetAndGetVariable(ByVal name As String, ByVal value As String) As Boolean
        If _currentActionType <> enumRequestActionType.DEVICE_IDLE Then
            Return False
        End If
        If _IpValid Then
            _cmdVariableName = name.Trim
            _cmdVariableValue = value.Trim
            _currentActionType = enumRequestActionType.REQUEST_VARIABLE_REPLACE
            _StopThread = False
            _Error = False
            _StepNextNumber = _Address_Home
            _MainResult = pMain.BeginInvoke(pMainCallBack, pMain)
            Return True
        Else
            _currentActionType = enumRequestActionType.DEVICE_IDLE
            Return False
        End If
    End Function

    Public Function SetAndGetTemplate(ByVal name As String) As Boolean Implements ILaserBase.SetAndGetTemplate
        If _currentActionType <> enumRequestActionType.DEVICE_IDLE Then
            Return False
        End If
        _GetTempCount = 0
        If _IpValid Then
            _cmdTemplateName = name.Trim
            _currentActionType = enumRequestActionType.REQUEST_TEMPLATE_CHANGE
            _StopThread = False
            _Error = False
            _StepNextNumber = _Address_Home
            _MainResult = pMain.BeginInvoke(pMainCallBack, pMain)
            Return True
        Else
            _currentActionType = enumRequestActionType.DEVICE_IDLE
            Return False
        End If
    End Function

    Public Function SetAndGetTemplateReady(ByVal name As String) As Boolean Implements ILaserBase.SetTemplateReady
        If Not _LastResponse.Contains(name) Then
            _Status = Alltec_StatusCode.LaserTemplateName
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If
        Return True
    End Function

    Public Function SetAnyCommand(ByVal cmd As String) As Boolean Implements ILaserBase.SetAnyCommand
        If _currentActionType <> enumRequestActionType.DEVICE_IDLE Then
            Return False
        End If
        If _IpValid Then
            _cmdAnyCommand = cmd.Trim
            _currentActionType = enumRequestActionType.REQUEST_SEND_ANY_CMD
            _StopThread = False
            _Error = False
            _StepNextNumber = _Address_Home
            _MainResult = pMain.BeginInvoke(pMainCallBack, pMain)
            Return True
        Else
            _currentActionType = enumRequestActionType.DEVICE_IDLE
            Return False
        End If
    End Function

    Public Function SetAnyCommandReady(ByVal cmd As String) As Boolean Implements ILaserBase.SetAnyCommandReady
        If _Status <> Alltec_StatusCode.Ready Then
            _Status = Alltec_StatusCode.InvalidResponse
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, LastResponse)
            Return False
        End If

        Return True
    End Function

    Protected Sub a_SoftwareHome()

        If Not _IpValid Then
            '
        ElseIf _Error Then
            '
        ElseIf _currentActionType = enumRequestActionType.REQUEST_VARIABLE_REPLACE Then
            _Error = False
            _NowReceived = ""
            _LastResponse = ""

            ReDim _Buffer(255)
            IncrementStepNumber()

            _Status = Alltec_StatusCode.Ready
            _StatusDescription = _Status.ToString

        ElseIf _currentActionType = enumRequestActionType.REQUEST_TEMPLATE_CHANGE Then
            _Error = False
            _NowReceived = ""
            _LastResponse = ""

            ReDim _Buffer(255)
            _StepNextNumber = _Address_SetTemplate

            _Status = Alltec_StatusCode.Ready
            _StatusDescription = _Status.ToString

        ElseIf _currentActionType = enumRequestActionType.REQUEST_SEND_ANY_CMD Then
            _Error = False
            _NowReceived = ""
            _LastResponse = ""

            ReDim _Buffer(255)
            _StepNextNumber = _Address_SetAnyCmd

            _Status = Alltec_StatusCode.Ready
            _StatusDescription = _Status.ToString

        End If

    End Sub

    Protected Sub _CheckIp()
        Dim _Address() As String
        Dim _ByteAddress(3) As Byte, l As Integer

        _IpValid = False
        _Status = Alltec_StatusCode.InCheckIpMode
        _StatusDescription = _Status.ToString

        _Address = Split(_IP, ".")
        If _Address.GetUpperBound(0) <> 3 Then
            _Status = Alltec_StatusCode.InvalidIpAddress
            _StatusDescription = _Status.ToString
            _Error = True
        Else
            For l = _Address.GetLowerBound(0) To _Address.GetUpperBound(0)
                Try
                    _ByteAddress(l) = CType(_Address(l), Byte)
                Catch ex As Exception
                    _Status = Alltec_StatusCode.WindowsError
                    _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR3, _Status.ToString, ex.Message)
                    _Error = True
                    Exit For
                End Try
            Next
        End If

        If Not _Error Then
            _IpAddress = Nothing
            _IpAddress = New IPAddress(_ByteAddress)

            _EndPoint = Nothing
            _EndPoint = New IPEndPoint(_IpAddress, _Port)
            _Status = Alltec_StatusCode.Ready

            _IpValid = True
        End If

    End Sub

    Protected Sub a_GetPing()
        _Status = Alltec_StatusCode.InPingMode
        _StatusDescription = _Status.ToString

        Try
            If Not My.Computer.Network.Ping(_IP) Then
                _GetPing_Successful = False
                _Status = Alltec_StatusCode.DeviceNotAvailable
                _StatusDescription = _Status.ToString
                Abort()
            Else
                _GetPing_Successful = True
                IncrementStepNumber()
            End If
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try
    End Sub


    Protected Sub a_Connect()
        _Status = Alltec_StatusCode.InConnectionToReceiveInterface
        _StatusDescription = _Status.ToString

        Try
            _Interface = Nothing
            _Interface = New Socket(_EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            _Interface.SendTimeout = _MilliSecondsTimerOut
            _Interface.ReceiveTimeout = _MilliSecondsTimerOut
            _Interface.Connect(_EndPoint)

            If Not _Interface.Connected And Not _Error Then
                _Status = Alltec_StatusCode.ConnectionToReceiveInterfaceNotPossible
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR4, _Status.ToString, Port.ToString)
                Abort()
            ElseIf _Interface.Connected And Not _Error Then
                IncrementStepNumber()
            End If
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try
    End Sub

    Protected Sub a_SendCommand(ByVal cmd As String)

        _Status = Alltec_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Dim _Buffer() As Byte = Encoding.UTF8.GetBytes(cmd & vbCrLf)

        Try
            _Interface.Send(_Buffer, 0, _Buffer.GetLength(0), SocketFlags.None)
            _NowReceived = ""
            IncrementStepNumber()
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try

    End Sub

    Protected Sub a_SetAnyCommand()

        _Status = Alltec_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Dim _Buffer() As Byte = Encoding.UTF8.GetBytes(_cmdAnyCommand & vbCrLf)

        Try
            _Interface.Send(_Buffer, 0, _Buffer.GetLength(0), SocketFlags.None)
            _NowReceived = ""
            IncrementStepNumber()
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try

    End Sub

    Protected Sub a_SendVariable()

        _Status = Alltec_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Dim cmd As String = CMD_SETVARS & _Delimiter & _cmdVariableName & _Delimiter & _
                _cmdVariableValue & _Delimiter & vbCrLf
        Dim _Buffer() As Byte = Encoding.UTF8.GetBytes(cmd)

        Try
            _Interface.Send(_Buffer, 0, _Buffer.GetLength(0), SocketFlags.None)
            _NowReceived = ""
            IncrementStepNumber()
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try

    End Sub

    Protected Overridable Sub a_SetTemplate()

        _Status = Alltec_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Dim cmd As String
        cmd = CMD_DP10GS_LOADJOB & _Colon & _cmdTemplateName & _FileSuffix & vbCrLf
        Dim _Buffer() As Byte = Encoding.UTF8.GetBytes(cmd)

        Try
            _Interface.Send(_Buffer, 0, _Buffer.GetLength(0), SocketFlags.None)
            _NowReceived = ""
            IncrementStepNumber()
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            Abort()
        End Try

    End Sub


    Protected Overridable Sub a_TempleReceiveResponse()
        Dim Results() As String

        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString
        Try
            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then Exit Sub
            _LastResponse = Results(0)

            Results = Split(_LastResponse, _Colon)
            If Results.GetUpperBound(0) < 3 Or _GetTempCount > 300 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Status.ToString
                Abort()
            ElseIf Results(2) = _cmdTemplateName
                IncrementStepNumber()
            Else
                _StepNextNumber = 14
            End If
            _GetTempCount = _GetTempCount + 1

        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Overridable Sub a_ReceiveResponse()
        Dim Results() As String

        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString

        Try
            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then
                _LastResponse = "Error"
                Exit Sub
            End If
            _LastResponse = Results(0)

            Results = Split(_LastResponse, _Colon)
            If Results.GetUpperBound(0) < 1 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Status.ToString
                Abort()
            Else
                IncrementStepNumber()
            End If

        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Overridable Sub a_ReceivePositiveResponse()
        Dim Results() As String
        Dim _Response As String = ""
        Dim _Ack As String
        Dim _Spilter As String


        _Spilter = _Colon
        _Ack = CMD_DP10GS_ACK
      
        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString

        Try
            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then
                _LastResponse = "Error"
                Exit Sub
            End If
            _Response = Results(0)
            _LastResponse = _NowReceived
            Results = Split(_Response, _Spilter)

            If Results(1) = _Ack Then
                IncrementStepNumber()
            Else
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            End If

        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Sub Abort()
        _Error = True
        _StopThread = True

        On Error Resume Next

        _Interface.Shutdown(SocketShutdown.Both)
        _Interface.Disconnect(False)
        _Interface.Close()
        '  _LastResponse = ""

        RaiseEvent Aborted(_StatusDescription)
        On Error GoTo 0

        _StepNextNumber = _Address_Home

    End Sub

    Protected Sub a_Ready()
        _Error = False
        _StopThread = True
        _Status = Alltec_StatusCode.Ready
        _StatusDescription = _Status.ToString
        RaiseEvent Finish()
        _StepNextNumber = _Address_Home
    End Sub

    Protected Sub IncrementStepNumber()
        _StepNextNumber = _StepCurrentNumber + 1
    End Sub
End Class


Public Class AlltecFoba_Laser_LF100
    Inherits AlltecFoba_Laser_DP10GS

    Public Sub New()
        MyBase.New()
        _LaserType = enumLaserType.LF100
    End Sub


    Public Overrides Sub Main()
        Do
            System.Threading.Thread.Sleep(10)
            If _StopThread Then Exit Do

            _Toggle = _StepCurrentNumber <> _StepNextNumber
            _StepCurrentNumber = _StepNextNumber
            Select Case _StepCurrentNumber

                Case 0 : a_SoftwareHome()
                    'set and get Variables
                Case 1 : a_GetPing()
                Case 2 : a_Connect()
                Case 3 : a_SendVariable()
                Case 4 : a_ReceivePositiveResponse()
                Case 5 : a_SendCommand(CMD_GETVARS & _Delimiter)
                Case 6 : a_ReceiveResponse()
                Case 7 : a_Ready()
                    'set and get specific Template
                Case 10 : a_GetPing()
                Case 11 : a_Connect()
                Case 12 : a_SetTemplate()
                Case 13 : a_ReceivePositiveResponse()
                Case 14 : a_SendCommand(CMD_GETJOB & _Delimiter)
                Case 15 : a_TempleReceiveResponse()
                Case 16 : a_Ready()
                    'set any command
                Case 20 : a_GetPing()
                Case 21 : a_Connect()
                Case 22 : a_SetAnyCommand()
                Case 23 : a_ReceivePositiveResponse()
                Case 24 : a_Ready()

                Case Else
                    _Status = Alltec_StatusCode.InvalidStepNumber
                    _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR1, _Status.ToString, _StepCurrentNumber.ToString)
                    Abort()
            End Select
        Loop Until _StopThread
    End Sub

    Public Overrides Function GetStatus() As Boolean
        SetAnyCommand(CMD_GETSTATUS & _Delimiter)
        Return True
    End Function


    Public Overrides Function GetVar() As Boolean
        SetAnyCommand(CMD_GETVARS & _Delimiter)
        Return True
    End Function

    Public Overrides Function Start() As Boolean
        SetAnyCommand(CMD_START & _Delimiter)
        Return True
    End Function

    Public Overrides Function GetGetTemplate() As Boolean
        SetAnyCommand(CMD_GETJOB & _Delimiter)
        Return True
    End Function

    Public Overrides Function GetStatusReady(ByVal mTemplateName As String) As Boolean
        If Not LastResponse.Contains(Alltec_StatusCode.LaserStatusReady.ToString) And Not LastResponse.Contains(Alltec_StatusCode.LaserStatusStandby.ToString) Then
            _Status = Alltec_StatusCode.NotReady
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, _LastResponse)
            Return False
        End If
        Return True
    End Function


    Protected Overrides Sub a_ReceiveResponse()
        Dim Results() As String

        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString

        Try
            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then
                _LastResponse = "Error"
                Exit Sub
            End If

            _LastResponse = Results(0)

            Results = Split(_LastResponse, _Delimiter)
            If Results.GetUpperBound(0) < 2 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            ElseIf Results(0) = "" Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            ElseIf Results(1).Length < 4 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            Else
                IncrementStepNumber()
            End If

              
        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message.ToString)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Overrides Sub a_TempleReceiveResponse()
        Dim Results() As String

        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString
        Try

            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then
                _LastResponse = "Error"
                Exit Sub
            End If

            _LastResponse = Results(0)

            Results = Split(_LastResponse, _Delimiter)
            If Results.GetUpperBound(0) < 3 Or _GetTempCount > 300 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            ElseIf Results(0) = "" Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
                Abort()
            ElseIf Results(1).Length < 4 Then
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Status.ToString
                Abort()
            ElseIf Results(2) = _cmdTemplateName Then
                IncrementStepNumber()
            Else
                _StepNextNumber = 14
            End If

            _GetTempCount = _GetTempCount + 1


        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Overrides Sub a_ReceivePositiveResponse()
        Dim Results() As String
        '  Dim _Response As String = ""
        Dim _Ack As String
        Dim _Spilter As String


        _Spilter = _Delimiter
        _Ack = CMD_ACK

        _Status = Alltec_StatusCode.InReceiveMode
        _StatusDescription = _Status.ToString

        Try
            _Interface.Receive(_Buffer)
            _NowReceived = _NowReceived & Encoding.ASCII.GetString(_Buffer)
            Results = Split(_NowReceived, vbCrLf)
            If Results.GetUpperBound(0) < 1 Then
                _LastResponse = "Error"
                Exit Sub
            End If

            _LastResponse = _NowReceived

            Results = Split(_LastResponse, _Spilter)

            If Results(1) = _Ack Then
                IncrementStepNumber()
            Else
                _Status = Alltec_StatusCode.InvalidResponse
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, _LastResponse)
                Abort()
            End If

        Catch ex As Exception
            If Not _Error Then
                _Status = Alltec_StatusCode.WindowsError
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, ex.Message)
            End If
            Abort()
            Return
        End Try

    End Sub

    Protected Overrides Sub a_SetTemplate()

        _Status = Alltec_StatusCode.InSendStartMode
        _StatusDescription = _Status.ToString

        Dim cmd As String
        cmd = CMD_SETJOB & _Delimiter & _cmdTemplateName & _Delimiter & vbCrLf
        Dim _Buffer() As Byte = Encoding.UTF8.GetBytes(cmd)

        Try
            _Interface.Send(_Buffer, 0, _Buffer.GetLength(0), SocketFlags.None)
            _NowReceived = ""
            '   Threading.Thread.Sleep(1000)
            IncrementStepNumber()
        Catch ex As Exception
            _Status = Alltec_StatusCode.WindowsError
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LASER_ERROR2, _Status.ToString, "")
            Abort()
        End Try

    End Sub
End Class
