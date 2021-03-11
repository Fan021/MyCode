Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device
Public MustInherit Class clsHMIGapFiller
    Inherits clsHMIDeviceBase
    Public MustOverride Property LastProgram As String

    Public MustOverride Property PPSstrPartNoA As String
    Public MustOverride Property PPSstrVolumeA As String
    Public MustOverride Property PPSstrExpiryDateA As String
    Public MustOverride Property PPSstrBatchNoA As String
    Public MustOverride Property PPSstrSupplierNoA As String
    Public MustOverride Property PPSstrPackagingNoA As String

    Public MustOverride Property PPSstrPartNoB As String
    Public MustOverride Property PPSstrVolumeB As String
    Public MustOverride Property PPSstrExpiryDateB As String
    Public MustOverride Property PPSstrBatchNoB As String
    Public MustOverride Property PPSstrSupplierNoB As String
    Public MustOverride Property PPSstrPackagingNoB As String

    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
End Class
