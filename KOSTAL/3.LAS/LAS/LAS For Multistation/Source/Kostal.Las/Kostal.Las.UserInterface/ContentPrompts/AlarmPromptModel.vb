Public Class AlarmPromptModel
    Inherits Kostal.Windows.Presentation.ViewModelBase

    Private ReadOnly _resetCommand As Windows.Presentation.RelayCommand
    Private ReadOnly _messageText As String
    Private ReadOnly _problemText As String = System.String.Empty
    Private ReadOnly _datetimeText As String = System.String.Empty
    Private ReadOnly _elementsBackGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneElementsBackground
    Private ReadOnly _controlBackGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneControlBackground
    Private ReadOnly _controlLabelForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.StationLabelAndValueLabelColor
    Private ReadOnly _controlValueForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.StationLabelAndValueValueColor
    Private ReadOnly _iconForeGroundColor As System.Windows.Media.Color = Helpers.ColorHelper.PromptNoneSymbolColor


    Sub New(message As String, problemText As String, resetCommand As Windows.Presentation.RelayCommand, createdDateTime As System.DateTime)
        _messageText = message
        _problemText = problemText

        _resetCommand = resetCommand
        _datetimeText = createdDateTime.ToString("T")

        _controlLabelForeGroundColor = Helpers.ColorHelper.PromptAlarmLabelColor
        _controlValueForeGroundColor = Helpers.ColorHelper.PromptAlarmTextColor
        _elementsBackGroundColor = Helpers.ColorHelper.PromptAlarmElementsBackground
        _controlBackGroundColor = Helpers.ColorHelper.PromptAlarmControlBackground
        _iconForeGroundColor = Helpers.ColorHelper.PromptAlarmSymbolColor
    End Sub

    Public ReadOnly Property ResetCommand As Windows.Presentation.RelayCommand
        Get
            Return _resetCommand
        End Get

    End Property

    Public ReadOnly Property MessageText As String
        Get
            Return _messageText ' "Hardware-Alarm: Emergency-Stop-Button"
        End Get
    End Property


    Public ReadOnly Property DateTimeText As String
        Get
            Return _datetimeText
        End Get
    End Property


    Public ReadOnly Property ProblemText As String
        Get
            Return _problemText
        End Get
    End Property

    Public ReadOnly Property ElementsBackGroundColor As System.Windows.Media.Color
        Get
            Return _elementsBackGroundColor
        End Get
    End Property

    Public ReadOnly Property ControlBackgroundColor As System.Windows.Media.Color
        Get
            Return _controlBackGroundColor
        End Get
    End Property

    Public ReadOnly Property ControlLabelForeGroundColor As System.Windows.Media.Color
        Get
            Return _controlLabelForeGroundColor
        End Get
    End Property


    Public ReadOnly Property ControlValueForeGroundColor As System.Windows.Media.Color
        Get
            Return _controlValueForeGroundColor
        End Get
    End Property

    Public ReadOnly Property IconForeGroundColor As System.Windows.Media.Color
        Get
            Return _iconForeGroundColor
        End Get
    End Property

End Class