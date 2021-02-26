
Public Interface IDevice
    Inherits IDisposable
End Interface


Public Enum DeviceType
    DataLogic_210N_LAN
    DataLogic_210N_RS232
End Enum

Public Interface IScanner
    Inherits IDevice
    ReadOnly Property Running As Boolean
    Property ScanResult As String
    ReadOnly Property Status As enumDevice_ErrorCodes
    ReadOnly Property StatusDescription As String
    Function TrigON() As Boolean
    Function TrigOFF() As Boolean
    Function Beep() As Boolean
    Function Scan(ByVal iTimeOut As Integer) As String
    Sub ContinueScan()
    Function Init(ByVal mConfig As InterfaceConfig, ByVal MySettings As Settings) As Boolean
    Event DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
End Interface

