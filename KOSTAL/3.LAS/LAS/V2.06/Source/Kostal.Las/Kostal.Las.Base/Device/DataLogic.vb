Imports System.Text

Public Class DataLogic_210
    Inherits Keyence_Scanner

    Public Overrides Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        If mConfig.Split(CChar(",")).Length <> 2 And mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_CONFIG
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        If mType = DeviceType.LAN Then
            _InterfaceConfig.IP = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.Port = Integer.Parse(mConfig.Split(CChar(","))(1))
            _Interface = New TCPInterface
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.BaudRate = Integer.Parse(mConfig.Split(CChar(","))(1))
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _Interface = New RS232Interface
        End If

        If mConfig.Split(CChar(",")).Length >= 3 Then '此时从ini读取触发命令
            _ConfigFile = mConfig.Split(CChar(","))(2)
            GetConfigCommand()
        Else
            _InterfaceConfig.DataFrameSTX = Chr(enumDataFrame.STX)
            _InterfaceConfig.DataFrameEXT = Chr(enumDataFrame.EXT)
            _InterfaceConfig.TrigOn = Encoding.ASCII.GetString(New Byte() {&H2, &H13, &H10, &H52, &H3})
            _InterfaceConfig.TrigOff = ""
            _InterfaceConfig.Beep = ""
        End If
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

End Class

Public Class DataLogic_210N
    Inherits DataLogic_210
    Public Overrides Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        If mConfig.Split(CChar(",")).Length <> 2 And mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_CONFIG
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        If mType = DeviceType.LAN Then
            _InterfaceConfig.IP = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.Port = Integer.Parse(mConfig.Split(CChar(","))(1))
            _Interface = New TCPInterface
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.BaudRate = Integer.Parse(mConfig.Split(CChar(","))(1))
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _Interface = New RS232Interface
        End If

        If mConfig.Split(CChar(",")).Length >= 3 Then '此时从ini读取触发命令
            _ConfigFile = mConfig.Split(CChar(","))(2)
            GetConfigCommand()
        Else
            _InterfaceConfig.DataFrameSTX = Chr(enumDataFrame.STX)
            _InterfaceConfig.DataFrameEXT = Chr(enumDataFrame.EXT)
            _InterfaceConfig.TrigOn = "T"
            _InterfaceConfig.TrigOff = "S"
            _InterfaceConfig.Beep = ""
        End If
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

End Class


Public Class PD9530
    Inherits DataLogic_210
    Public Overrides Function Init(ByVal mType As DeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        If mConfig.Split(CChar(",")).Length <> 2 And mConfig.Split(CChar(",")).Length <> 3 Then
            _Status = enumDevice_ErrorCodes.DEVICE_ERROR_INVALID_CONFIG
            _StatusDescription = "Config Fail. " + mConfig
            Return False
        End If
        If mType = DeviceType.LAN Then
        End If
        If mType = DeviceType.RS232 Then
            _InterfaceConfig.RS232Port = mConfig.Split(CChar(","))(0)
            _InterfaceConfig.BaudRate = Integer.Parse(mConfig.Split(CChar(","))(1))
            _InterfaceConfig.Parity = IO.Ports.Parity.None
            _InterfaceConfig.DataBits = 8
            _InterfaceConfig.StopBits = IO.Ports.StopBits.One
            _InterfaceConfig.EventDataReceived = True
            _Interface = New RS232Interface
        End If

        If mConfig.Split(CChar(",")).Length >= 3 Then '此时从ini读取触发命令
            _ConfigFile = mConfig.Split(CChar(","))(2)
            GetConfigCommand()
        Else
            _InterfaceConfig.DataFrameSTX = ""
            _InterfaceConfig.DataFrameEXT = Chr(&HD)
            _InterfaceConfig.TrigOn = ""
            _InterfaceConfig.TrigOff = ""
            _InterfaceConfig.Beep = Encoding.ASCII.GetString(New Byte() {&H42, &H45, &H4C})
        End If
        AddHandler _Interface.DataReceived, AddressOf InterfaceDataReceived
        If Not _Interface.Interface_Init(_InterfaceConfig, _i, AppSettings, _Language) Then Return False
        If Not _Interface.Interface_Connect() Then Return False
        _Interface.Interface_Abort()
        Return True
    End Function

End Class
