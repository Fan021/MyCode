Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.Diagnostics

Public Class PSD_IT_4820
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
            _InterfaceConfig.DataFrameSTX = Chr(enumDataFrame.STX)
            _InterfaceConfig.DataFrameEXT = Chr(enumDataFrame.EXT)
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
