Public Class ParameterEvent
    Inherits EventArgs
    Private lListParameter As List(Of String)
    Public ReadOnly Property ListParameter As List(Of String)
        Get
            Return lListParameter
        End Get
    End Property

    Sub New(ByVal lListParameter As List(Of String))
        Me.lListParameter = lListParameter
    End Sub
End Class
