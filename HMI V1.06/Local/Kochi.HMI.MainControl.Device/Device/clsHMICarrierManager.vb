Public MustInherit Class clsHMICarrierManager
    Inherits clsHMIDeviceBase
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function ResetCarrierID(ByVal strCarrierID As String, ByRef strResult As String) As Boolean
    Public MustOverride Function CheckRepeat(ByVal strCarrierID As String, ByRef strResult As String) As Integer
    Public MustOverride Function UpdateCarrier(ByVal strCarrierID As String, ByRef strResult As String) As Boolean
    Public MustOverride Function UpdateCarrier(ByVal strCarrierID As String, ByVal strStation As String, ByRef strResult As String) As Boolean
    Public MustOverride Function GetCarrierStation(ByVal strCarrierID As String) As String
End Class
