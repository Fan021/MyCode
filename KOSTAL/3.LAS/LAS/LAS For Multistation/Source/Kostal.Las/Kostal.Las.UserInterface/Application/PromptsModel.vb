Public Class PromptsModel
    Inherits Global.Kostal.Windows.Presentation.ViewModelBase

    Private ReadOnly _applicationModel As ApplicationPromptsModel

    Public Sub New(ByVal applicationModel As ApplicationPromptsModel)
        MyBase.New()
        _applicationModel = applicationModel
    End Sub

    Public ReadOnly Property ApplicationPromptsModel As ApplicationPromptsModel
        Get
            Return _applicationModel
        End Get
    End Property

End Class