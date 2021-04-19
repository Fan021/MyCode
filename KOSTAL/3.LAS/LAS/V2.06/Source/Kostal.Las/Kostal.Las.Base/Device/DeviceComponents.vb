
Public Interface IDevice
    Inherits IDisposable
End Interface

Public Enum DeviceType
    LAN = 0
    RS232 = 1
    USB = 3
End Enum

Public Enum LasDeviceType
    Keyence_LAN = 0
    Keyence_RS232
    DataLogic_210N_LAN
    DataLogic_210_LAN
    DataLogic_210_RS232
    Sympol_MS447_RS232
    PSD_IT_4820_RS232
    PD9530_RS232

    Zebra_LAN = 20
    Zebra_RS232
    Zebra_ZT610_LAN
    Zebra_ZT610_RS232
    Zebra_MACH2_LAN
    Zebra_MACH2_RS232
    Zebra_MACH4_LAN
    Zebra_MACH4_RS232

    DP10GS_LAN = 30
    LF100_LAN
    MDX1000_LAN
    AF20_USB

    FR01_LAN = 35
    FR02_LAN = 36

    Epson_TM_T88V_LAN = 40
    Epson_TM_T88V_RS232
    Epson_TM_T88V_USB

    Schedule = 50
    Reference
    NewPart
    Article
    QGW
    QGW_ASSM
    QGW_Finish
    Manual
    ShowPic
    Counter
    Packman
    ReTest
    ManualReTest
    SN
    UserDefine
    UpdateReference
    ManualScanner
    CAQ
    SaveProduction
    PLCAlarm
    MulitPrinter
    ForCam
    UserDefineQGW
    MES
    NULL
    NONE
End Enum

Public Interface IScanner
    Inherits IDevice
    ReadOnly Property Running As Boolean
    Property ScanResult As String
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property InterfaceConfig As InterfaceConfig
    ReadOnly Property InterfaceDevice As IInterface
    ReadOnly Property StatusDescription As String
    Function TrigON() As Boolean
    Function TrigOFF() As Boolean
    Function Beep() As Boolean

    Function SendAndReadCommand(ByVal iTimeOut As Integer, ByVal strSendCommand As String, ByVal strReadCommnd As String) As Boolean
    Function SendCommand(ByVal strSendCommand As String) As Boolean
    Function Scan(ByVal iTimeOut As Integer, ByVal strTrigOn As String, ByVal strTrigOff As String) As String
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
End Interface

Public Interface IPrinter
    Inherits IDevice
    Property ClearMaskFile As Boolean
    ReadOnly Property Running As Boolean
    ReadOnly Property PrintMode() As enumZebra_PrintModes
    ReadOnly Property Status() As enumZebra_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Function GetLabelStatus() As Boolean
    Function Calibration() As Boolean
    Function ChangePrintModeTo(ByVal printMode As enumZebra_PrintModes) As Boolean
    Function SendData(ByVal Fields() As String, ByVal MaskPath As String, ByVal MaskFile As String, ByVal MaskNameWithOutExtension As String) As Boolean
End Interface

Public Interface ILaser
    Inherits IDevice
    ReadOnly Property ReadyToWrite As Boolean
    ReadOnly Property Status As Alltec_StatusCode
    ReadOnly Property LastResponse As String
    ReadOnly Property StatusDescription As String
    Sub ResetLastResponse()
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
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

End Interface

Public Interface IFailPrinter
    Inherits IDevice
    ReadOnly Property Running As Boolean
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function Printer(ByVal Lines As Collection) As Boolean
    Function Cut() As Boolean
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
End Interface

Public Interface IPackmanDevice
    Inherits IDevice
    ReadOnly Property Running As Boolean
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
End Interface

Public Interface IFlash
    ReadOnly Property FlasherName As String
    ReadOnly Property Port() As Int32
    ReadOnly Property Status() As FR_StatusCode
    ReadOnly Property StatusDescription() As String
    ReadOnly Property IsError() As Boolean
    ReadOnly Property IsBusy() As Boolean

    ReadOnly Property LastResponse() As String

    Function SendRead(ByVal Fileds As String()) As Boolean
    Sub ResetLastResponse()
    Function Init(ByVal mType As LasDeviceType, ByVal mConfig As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
    Sub Dispose()
End Interface
Public Class DeviceComponents

End Class
