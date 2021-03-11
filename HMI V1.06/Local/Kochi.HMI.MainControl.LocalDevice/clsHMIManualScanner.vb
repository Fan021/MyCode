Public MustInherit Class clsHMIManualScanner
    Inherits clsHMIScanner
    MustOverride Property isActive As Boolean
    MustOverride Property ScanResult As String
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Class
Public Enum enumScannerMethod
    ScanHousing = 1
    ScanHousingAndWriteAndCheckSN
    ScanHousingAndWriteSN
    ScanHousingAndCheckSN
    ScanPCBA
    ScanChoker
    ScanPFCPCB
    ScanDCPCB
    ScanDCPCB_AID
    ScanDCFilterPCB
    ScanSystemPCB
    ScanHU
    ScanSFC
    ScanOther
End Enum