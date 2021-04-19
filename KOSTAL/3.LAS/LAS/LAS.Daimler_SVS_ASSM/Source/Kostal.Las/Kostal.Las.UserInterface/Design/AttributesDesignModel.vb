Namespace Design

    Public Class AttributesDesignModel
        Inherits Global.Kostal.Windows.Presentation.ViewModelBase

        Public Class AttributeDesignModel

        End Class

        Private ReadOnly _modelDesign As DesignModel
        Private ReadOnly _collectionItems As New ObjectModel.ObservableCollection(Of String)

        Public Sub New(modelDesign As DesignModel)
            _modelDesign = modelDesign
        End Sub

        Public Sub Add(id As String, caption As String, width As String)

        End Sub


    End Class

End Namespace