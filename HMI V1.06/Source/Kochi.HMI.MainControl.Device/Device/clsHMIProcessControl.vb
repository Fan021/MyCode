Public MustInherit Class clsHMIProcessControl
    Inherits clsHMIDeviceBase
    MustOverride Property Running As Boolean
    MustOverride ReadOnly Property Enable As Boolean
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean


    Public MustOverride Function Start(ByVal strSFC As String, ByRef strResult As String) As Integer
    Public MustOverride Function CreateSFC(ByVal strSFC As String, ByRef strResult As String) As Integer
    Public MustOverride Function Complete(ByVal strSFC As String, ByRef strResult As String) As Boolean
    Public MustOverride Function logNonConformance(ByVal strSFC As String, ByRef strResult As String) As Boolean

End Class
