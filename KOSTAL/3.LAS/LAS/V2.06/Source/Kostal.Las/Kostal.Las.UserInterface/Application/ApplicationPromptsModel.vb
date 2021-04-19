Imports System.ComponentModel

Public Class ApplicationPromptsModel
    Inherits Kostal.Windows.Presentation.ViewModelBase

    'Private ReadOnly _modelApplication As ApplicationModel
    'Private ReadOnly _menuitemmodelApplicationPrompts As MenuItemModel
    Private ReadOnly _collectionPromptModels As New ObjectModel.ObservableCollection(Of SinglePromptModel)
    Private ReadOnly _controllerStationPrompts As Prompts.IStationPromptsController
    Private _currentPromptViewModel As Kostal.Windows.Presentation.IViewModelBase = New NoPromptModel

    'Private ReadOnly _commandAlarm As Kostal.Windows.Presentation.RelayCommand
    'Private ReadOnly _commandAlarmReset As Kostal.Windows.Presentation.RelayCommand
    Private ReadOnly _action As Action(Of Object)

    Public Sub New(ByVal ApplicationAlarm As Prompts.IStationPromptsController)
        '_modelApplication = modelApplication
        '_menuitemmodelApplicationPrompts = menuitemmodelApplicationPrompts
        'If _modelApplication Is Nothing Then Return

        'AddHandler for AlarmContainer
        _controllerStationPrompts = ApplicationAlarm
        AddHandler _controllerStationPrompts.PropertyChanged, AddressOf TestApplication_Alarm_PropertyChanged
        TestApplication_Alarm_PropertyChanged(Nothing, New PropertyChangedEventArgs(Member.Of(Function() _controllerStationPrompts.PromptType)))



    End Sub

    'Private Sub Exec(s As Object)
    '    If _testApplication.Prompts.Current Is Nothing Then Return
    '    Dim response As Prompts.IResponse = TryCast(s, Prompts.IResponse)
    '    If response Is Nothing Then Return
    '    _testApplication.Prompts.Current.SetResponse(response.Id)
    'End Sub

    Private Sub TestApplication_Alarm_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        If e.PropertyName <> Member.Of(Function() _controllerStationPrompts.PromptType) Then Return
        'If Me.UiDispatcher.TryRunInDispatcherAsync(Sub() TestApplication_Alarm_PropertyChanged(sender, e)) Then Return

        If _controllerStationPrompts.PromptType = Prompts.PromptTypes.None Then
            If _controllerStationPrompts.Current Is Nothing Then
                _currentPromptViewModel = New NoPromptModel
                OnPropertyChanged(Member.Of(Function() Me.Current))
            Else
                _currentPromptViewModel = New SinglePromptModel(_controllerStationPrompts.Current)
                OnPropertyChanged(Member.Of(Function() Me.Current))
            End If
        Else
            _currentPromptViewModel = New SinglePromptModel(_controllerStationPrompts.Current)
            OnPropertyChanged(Member.Of(Function() Me.Current))
            '_currentPromptViewModel = New AlarmPromptModel("Emergency-Stop-Button", "Hardware-Alarm", New Kostal.Windows.Presentation.RelayCommand(New Action(Sub() _controllerApplicationAlarm.Reset())), System.DateTime.Now)
            'OnPropertyChanged(Member.Of(Function() Me.Current))
        End If
    End Sub


#Region "Properties (and their private members)"

    Private _visibility As System.Windows.Visibility = System.Windows.Visibility.Hidden
    Property Visibility As System.Windows.Visibility
        Get
            Return _visibility
        End Get
        Private Set(value As System.Windows.Visibility)
            If _visibility = value Then Return
            _visibility = value
            OnPropertyChanged()
        End Set
    End Property

    Private _stationText As String = System.String.Empty

    Property StationText As String
        Get
            Return _stationText
        End Get
        Private Set(value As String)
            If _stationText = value Then Return
            _stationText = value
            OnPropertyChanged()
        End Set
    End Property

    Private _messageText As String = System.String.Empty

    Property MessageText As String
        Get
            Return _messageText
        End Get
        Private Set(value As String)
            If _messageText = value Then Return
            _messageText = value
            OnPropertyChanged()
        End Set
    End Property


    Private _datetimeText As String = System.String.Empty

    Property DateTimeText As String
        Get
            Return _datetimeText
        End Get
        Private Set(value As String)
            If _datetimeText = value Then Return
            _datetimeText = value
            OnPropertyChanged()
        End Set
    End Property

    Private _commands As New ObjectModel.ObservableCollection(Of MenuItemModel)
    ReadOnly Property Commands As ObjectModel.ObservableCollection(Of MenuItemModel)
        Get
            Return _commands
        End Get
    End Property

#End Region

#Region "ContextMenu"

    'Public ReadOnly Property MenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
    '    Get
    '        Return _menuitemmodelApplicationPrompts.MenuItems
    '    End Get
    'End Property

#End Region

    Public ReadOnly Property Current As Kostal.Windows.Presentation.IViewModelBase
        Get
            Return _currentPromptViewModel
        End Get
    End Property

    ReadOnly Property PromptModels As ObjectModel.ObservableCollection(Of SinglePromptModel)
        Get
            Return _collectionPromptModels
        End Get
    End Property

End Class