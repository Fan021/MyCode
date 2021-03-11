Imports Kochi.HMI.MainControl.Device

Public MustInherit Class clsHMIPrinter
    Inherits clsHMIDeviceBase
    MustOverride Property Running As Boolean
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function LoadPrintFile(ByVal strFilePath As String) As Boolean
    Public MustOverride Function LoadFormatFile(ByVal strFilePath As String) As Boolean
    Public MustOverride Function SetField(ByVal strName As String, ByVal strFildStart As String, ByVal strFildEnd As String, ByVal strValue As String) As Boolean
    Public MustOverride Function PrintLabel(ByVal strName As String) As Boolean

End Class
