Public MustInherit Class clsHMIPLC
    Inherits clsHMIDeviceBase
    Public MustOverride Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

    Public MustOverride Overrides Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

    Public MustOverride Function AddAdsVariable(ByVal strVariableName As String) As Boolean
    Public MustOverride Function ReadAny(ByVal strName As String, ByVal Type As Type, Optional ByVal args() As Integer = Nothing) As Object
    Public MustOverride Function WriteAny(ByVal strName As String, ByVal oValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean

End Class


