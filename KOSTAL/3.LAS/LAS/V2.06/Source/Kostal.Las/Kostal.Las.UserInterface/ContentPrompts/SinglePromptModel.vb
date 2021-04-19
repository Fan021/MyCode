
Public Class SinglePromptModel
    Inherits Kostal.Windows.Presentation.ViewModelBase

    Private ReadOnly _prompt As Prompts.IPrompt
    Private ReadOnly _collectionResponseCommands As New ObjectModel.ObservableCollection(Of MenuItemModel)

    Public Sub New(message As Prompts.IPrompt)
        _prompt = message

        AddHandler _prompt.PropertyChanged, AddressOf UserPrompt_PropertyChanged

        StationText = _prompt.StationText
        DateTimeText = _prompt.RaisedTimeText '_prompt.Raised.ToString("T")
        MessageText = _prompt.PromptText
        TypeOfPrompt = _prompt.TypeOfPrompt
        ErrorCode = _prompt.ErrorCode

        IsVisibile = False

        If (message.DisplayOption And Prompts.DisplayOptions.Input) = Prompts.DisplayOptions.Input Then
            Dim menuitemmodelResponse As New MenuItemModel("OK", New Action(Sub()
                                                                                _prompt.SetResponse(Me.ResponseText)
                                                                            End Sub))
            _collectionResponseCommands.Add(menuitemmodelResponse)

            For Each response As Prompts.IResponse In _prompt.PossibleResponses
                Dim cachedResponse As Prompts.IResponse = response
                _collectionComboboxItems.Add(cachedResponse.Text)
            Next

            MessageRowSpan = 1
            ComboboxVisible = _collectionComboboxItems.Any()
            TextboxVisible = Not ComboboxVisible
            Return
        End If

        For Each response As Prompts.IResponse In _prompt.PossibleResponses
            Dim cachedResponse As Prompts.IResponse = response
            Dim menuitemmodelResponse As New MenuItemModel(cachedResponse.Text,
                                                           New Action(Of Object)(
                                                           Sub(o As Object) menuitemactionResponse_Click(o)), cachedResponse)
            _collectionResponseCommands.Add(menuitemmodelResponse)
            MessageRowSpan = 4
            ComboboxVisible = False
            TextboxVisible = False
        Next
    End Sub

    Private Sub UserPrompt_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)

        StationText = _prompt.StationText
        DateTimeText = _prompt.RaisedTimeText
        MessageText = _prompt.PromptText
        TypeOfPrompt = _prompt.TypeOfPrompt
        ErrorCode = _prompt.ErrorCode

    End Sub


    Private Sub menuitemactionResponse_Click(o As Object)
        Dim responseClicked As Prompts.IResponse = TryCast(o, Prompts.IResponse)
        If responseClicked Is Nothing Then Return
        _prompt.SetResponse(responseClicked.Id)
    End Sub

    Private Sub SetColors(typeOfPrompt As Prompts.PromptTypes)
        Select Case typeOfPrompt
            Case Prompts.PromptTypes.Alarm
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptAlarmLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptAlarmTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptAlarmElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptAlarmControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptAlarmSymbolColor
                Return
            Case Prompts.PromptTypes.Problem
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptProblemLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptProblemTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptProblemElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptProblemControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptProblemSymbolColor
                Return
            Case Prompts.PromptTypes.Warning
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptWarningLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptWarningTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptWarningElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptWarningControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptWarningSymbolColor
                Return
            Case Prompts.PromptTypes.Information
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptInformationLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptInformationTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptInformationElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptInformationControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptInformationSymbolColor
                Return
            Case Prompts.PromptTypes.Question
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptQuestionLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptQuestionTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptQuestionElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptQuestionControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptQuestionSymbolColor
                Return
            Case Prompts.PromptTypes.None
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptNoneLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptNoneTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptNoneElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptNoneControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptNoneSymbolColor
                Return
            Case Else
                ControlLabelForeGroundColor = Helpers.ColorHelper.PromptNoneLabelColor
                ControlValueForeGroundColor = Helpers.ColorHelper.PromptNoneTextColor
                ElementsBackGroundColor = Helpers.ColorHelper.PromptNoneElementsBackground
                ControlBackgroundColor = Helpers.ColorHelper.PromptNoneControlBackground
                IconForeGroundColor = Helpers.ColorHelper.PromptNoneSymbolColor
        End Select

    End Sub

    Protected Overrides Sub Finalize()

        RemoveHandler _prompt.PropertyChanged, AddressOf UserPrompt_PropertyChanged

        MyBase.Finalize()

    End Sub

#Region "Properties (and their private members)"

    Private _isVisibile As Boolean = False
    Public Property IsVisibile As Boolean
        Get
            Return _isVisibile
        End Get
        Private Set(value As Boolean)
            If _isVisibile = value Then Return
            _isVisibile = value
            OnPropertyChanged()
        End Set
    End Property

    Private _stationText As String = System.String.Empty

    Public Property StationText As String
        Get
            Return _stationText
        End Get
        Private Set(value As String)
            If _stationText = value Then Return
            _stationText = value
            OnPropertyChanged()
        End Set
    End Property


    Private _errorCode As String = System.String.Empty

    Public Property ErrorCode As String
        Get
            Return _errorCode
        End Get
        Private Set(value As String)
            If _errorCode = value Then Return
            _errorCode = value
            OnPropertyChanged()
        End Set
    End Property

    Private _messageText As String = System.String.Empty

    Public Property MessageText As String
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


    Public ReadOnly Property Commands As ObjectModel.ObservableCollection(Of MenuItemModel)
        Get
            Return _collectionResponseCommands
        End Get
    End Property

    Private _iconForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneSymbolColor
    Public Property IconForeGroundColor As System.Windows.Media.Color
        Get
            Return _iconForeGroundColor
        End Get
        Set(value As System.Windows.Media.Color)
            _iconForeGroundColor = value
            OnPropertyChanged()
        End Set
    End Property

    Private _promptType As Prompts.PromptTypes
    Public Property TypeOfPrompt As Prompts.PromptTypes
        Get
            Return _promptType
        End Get
        Set(value As Prompts.PromptTypes)
            _promptType = value
            SetColors(value)
            OnPropertyChanged()
        End Set
    End Property

    Private _elementsBackGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneElementsBackground
    Public Property ElementsBackGroundColor As System.Windows.Media.Color
        Get
            Return _elementsBackGroundColor
        End Get
        Set(value As System.Windows.Media.Color)
            _elementsBackGroundColor = value
            OnPropertyChanged()
        End Set
    End Property

    Private _controlBackGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneControlBackground
    Public Property ControlBackgroundColor As System.Windows.Media.Color
        Get
            Return _controlBackGroundColor
        End Get
        Set(value As System.Windows.Media.Color)
            _controlBackGroundColor = value
            OnPropertyChanged()
        End Set
    End Property

    Private _controlLabelForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.StationLabelAndValueLabelColor
    Public Property ControlLabelForeGroundColor As System.Windows.Media.Color
        Get
            Return _controlLabelForeGroundColor
        End Get
        Set(value As System.Windows.Media.Color)
            _controlLabelForeGroundColor = value
            OnPropertyChanged()
        End Set
    End Property


    Private _controlValueForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.StationLabelAndValueValueColor
    Public Property ControlValueForeGroundColor As System.Windows.Media.Color
        Get
            Return _controlValueForeGroundColor
        End Get
        Set(value As System.Windows.Media.Color)
            _controlValueForeGroundColor = value
            OnPropertyChanged()
        End Set
    End Property

#End Region

#Region "InputBox"

    Public Property ComboboxVisible As Boolean = False
    Public Property TextboxVisible As Boolean = False

    Public Property MessageRowSpan As Integer = 4

    Private ReadOnly _collectionComboboxItems As New ObjectModel.ObservableCollection(Of String)

    Public ReadOnly Property ComboboxItems As ObjectModel.ObservableCollection(Of String)
        Get
            Return _collectionComboboxItems
        End Get
    End Property

    Public Property ResponseText As String

#End Region

    '#Region "ContextMenu"

    '    Public ReadOnly Property MenuItems As ReadOnlyObservableCollectionEx(Of MenuItemModel)
    '        Get
    '            Return _menuitemmodelApplicationMessages.MenuItems
    '        End Get
    '    End Property

    '#End Region

End Class