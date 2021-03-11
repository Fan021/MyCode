Public MustInherit Class clsHMIKDX
    Inherits clsHMIDeviceBase
    MustOverride Property Running As Boolean
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Sub SetInfo(ByVal ArticleNumber As String, ByVal ArticleIndex As Integer, ByVal SerialNum As String)
    Public MustOverride Sub AddTeststep(ByVal TestStepNumber As String, ByVal StepName As String, ByVal Unit As String, ByVal LowLimit As Double, ByVal UpLimit As Double, ByVal value As Double, ByVal io As Boolean, ByVal StepDuration As Double, ByVal RetryCounter As Integer, ByVal FailureText As String)
    Public MustOverride Sub DataSave()
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Class
