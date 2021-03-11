Public Interface IHMIActionBase
    Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean
    Function CreateUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface

Public MustInherit Class clsHMIActionBase
    Implements IHMIActionBase
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected lListParameter As New List(Of String)
    Protected iActionUI As IActionUI

    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Public MustOverride Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IHMIActionBase.Init
    Public MustOverride Function CreateActionUI(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIActionBase.CreateUI
    Public MustOverride Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIActionBase.Run
    Public MustOverride Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IHMIActionBase.Quit

    Public ReadOnly Property ActionUI As IActionUI
        Get
            Return iActionUI
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

Public Interface IActionUI
    ReadOnly Property UI As Panel
    Function SetParameter(ByVal lListParameter As List(Of String)) As Boolean
    Function CheckParameter(ByVal lListParameter As List(Of String)) As Boolean
    Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Run(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
End Interface


