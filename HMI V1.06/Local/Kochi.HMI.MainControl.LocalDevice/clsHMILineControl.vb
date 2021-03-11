Imports Kochi.HMI.MainControl.Device

Public MustInherit Class clsHMILineControl
    Inherits clsHMIDeviceBase
    MustOverride Property Running As Boolean
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function Start(ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String, ByRef iResultErrorId As Integer) As Boolean
    Public MustOverride Function Complete(ByVal strSN As String, ByVal strVariant As String, ByVal bResult As Boolean, ByRef strResult As String) As Boolean
    Public MustOverride Function AddChild(ByVal iIndex As Integer, ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String) As Boolean
End Class
