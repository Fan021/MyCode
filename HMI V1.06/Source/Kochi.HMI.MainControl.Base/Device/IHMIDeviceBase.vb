Imports System.Windows.Forms
Public Interface IHMIDeviceBase
    Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Function CreateInitUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function CreateControlUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface



Public MustInherit Class clsHMIDeviceBase
    Implements IHMIDeviceBase
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Protected iInitUI As IInitUI
    Protected iControlUI As IControlUI

    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Public MustOverride Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IHMIDeviceBase.Init
    Public MustOverride Function CreateInitUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateInitUI
    Public MustOverride Function CreateControlUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.CreateControlUI
    Public MustOverride Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.Run
    Public MustOverride Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIDeviceBase.Quit
   
    Public ReadOnly Property InitUI As IInitUI
        Get
            Return iInitUI
        End Get
    End Property

    Public ReadOnly Property ControlUI As IControlUI
        Get
            Return iControlUI
        End Get
    End Property

    Public Sub Parameter_ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
        RaiseEvent ParameterChanged(Me, e)
    End Sub

    Public Function Clone(ByRef SrcList As List(Of String), ByRef TarList As List(Of String)) As Boolean
        Try
            TarList.Clear()
            For Each element As String In SrcList
                TarList.Add(element)
            Next
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function
End Class


Public Interface IDeviceUI
    ReadOnly Property UI As Panel
    Function SetParameter(ByVal lListParameter As List(Of String)) As Boolean
    Function CheckParameter(ByVal lListParameter As List(Of String)) As Boolean
    Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface

Public Interface IInitUI
    Inherits IDeviceUI
End Interface

Public Interface IControlUI
    Inherits IDeviceUI
End Interface