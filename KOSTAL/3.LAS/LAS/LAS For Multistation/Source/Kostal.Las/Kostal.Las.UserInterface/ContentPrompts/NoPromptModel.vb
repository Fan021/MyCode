Public Class NoPromptModel
    Inherits Kostal.Windows.Presentation.ViewModelBase

    ReadOnly Property MessageText As String
        Get
            Return "" 'Me.Localizer.GetLocalizedString("No Message")
        End Get
    End Property

    ReadOnly Property Commands As ObjectModel.ObservableCollection(Of MenuItemModel)
        Get
            Return Nothing
        End Get
    End Property

End Class