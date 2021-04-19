
Public Interface IViewModelBase
    Inherits System.ComponentModel.INotifyPropertyChanged

    Property DisplayName As String

    ReadOnly Property UiDispatcher As System.Windows.Threading.Dispatcher

    ReadOnly Property Localizer As Kostal.Globalization.Localizer

End Interface