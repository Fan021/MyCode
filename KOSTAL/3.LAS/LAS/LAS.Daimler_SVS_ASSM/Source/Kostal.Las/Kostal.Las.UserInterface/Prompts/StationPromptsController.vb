
Imports Kostal.Las.Base
'Imports Kostal.Testman.Framework.Runtime

Namespace Prompts

    Public Class StationPromptsController
        Inherits NotifyingObject
        Implements IStationPromptsController

        Private _state As Alarm.AlarmStates = Alarm.AlarmStates.NoAlarm
        Protected _funcForReset As Func(Of Boolean) = AddressOf ResetInternal

        Private ReadOnly _runtimeparametersStation As TestStationParameters
        Private ReadOnly _setCurrentLockObject As Object = New Object
        Private _currentPrompt As Prompts.IPrompt
        Private _promptType As Prompts.PromptTypes = PromptTypes.None

        Private _LastUserprompt As UserPrompt

        Private _LastErrorMessage As New structErrorMessageSet

        Private _PcResetError As Boolean = False

        Private iCounter As Integer = 0

        Private TimerReset As New Timer
        'Public Sub New()
        '    _runtimeparametersStation = Nothing
        'End Sub

        Public ReadOnly Property PcResetError As Boolean
            Get
                Return _PcResetError
            End Get
        End Property

        Public Property PromptType As PromptTypes Implements IStationPromptsController.PromptType
            Get
                Return _promptType
            End Get
            Set(value As PromptTypes)
                If value = _promptType Then Return
                _promptType = value
                OnPropertyChanged(Member.Of(Function() Me.PromptType))
            End Set

        End Property

        Public Sub New(runtimeparametersStation As TestStationParameters)
            _runtimeparametersStation = runtimeparametersStation
        End Sub

        Public ReadOnly Property State As Alarm.AlarmStates Implements IStationPromptsController.State
            Get
                Return _state
            End Get
        End Property


        Public ReadOnly Property Current As Prompts.IPrompt Implements Prompts.IStationPromptsController.Current
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _currentPrompt
            End Get
        End Property

        ''' <summary>
        ''' Resets this alarm status.
        ''' </summary>
        ''' <returns>True, if successful, otherwise false.</returns>
        Public Function Reset() As Boolean Implements Prompts.IStationPromptsController.Reset
            Return _funcForReset.Invoke()
        End Function

        Protected Function ResetInternal() As Boolean
            If _state = Alarm.AlarmStates.PhysicalAlarm Then Return False
            If _state = Alarm.AlarmStates.NoAlarm Then Return True
            ChangeAlarm(Alarm.AlarmStates.NoAlarm)
            Return True
        End Function

        Protected Overridable Sub ChangeAlarm(newState As Alarm.AlarmStates)
            If _state = newState Then Return
            _state = newState
            OnPropertyChanged(Member.Of(Function() Me.State))
        End Sub

        Private Sub SetCurrent(newPrompt As Prompts.IPrompt)

            If _currentPrompt Is newPrompt Then Return

            SyncLock _setCurrentLockObject
                'OutputDebugStringConditionalDebugWithThreadId("SetCurrent A '{0}'", newPrompt)

                ' Is the currentPrompt set?
                If _currentPrompt IsNot Nothing Then
                    'OutputDebugStringConditionalDebugWithThreadId("SetCurrent C")
                    RemoveHandler _currentPrompt.PropertyChanged, AddressOf CurrentUserPrompt_PropertyChanged

                    ' If there is a current prompt set -> set the Response on the possibly waiting currentPrompt
                    _currentPrompt.SetResponse(String.Empty)
                End If

                _currentPrompt = newPrompt
                If _currentPrompt IsNot Nothing Then
                    Dim userPrompt As UserPrompt = TryCast(_currentPrompt, UserPrompt)
                    If userPrompt IsNot Nothing Then userPrompt.ResetRaised()

                    AddHandler _currentPrompt.PropertyChanged, AddressOf CurrentUserPrompt_PropertyChanged
                    OnPropertyChanged(Member.Of(Function() Me.Current))

                    'Play beep
                    If (userPrompt.DisplayOption And Prompts.DisplayOptions.WithBeep) = Prompts.DisplayOptions.WithBeep Then System.Media.SystemSounds.Beep.Play()
                    PromptType = userPrompt.PromptType
                Else
                    OnPropertyChanged(Member.Of(Function() Me.Current))
                    PromptType = PromptTypes.None
                End If
            End SyncLock
        End Sub

        Public Function Create(promptText As String) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Create
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, Prompts.PromptTypes.Information, Prompts.DisplayOptions.None, promptText, New Prompts.UserResponse() {}, New String() {}, 0, 0)
            Return newUserprompt
        End Function

        Public Function Create(promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse)) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Create
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, Prompts.PromptTypes.Information, Prompts.DisplayOptions.None, promptText, possibleResponses, New String() {}, 0, 0)
            Return newUserprompt
        End Function

        Public Function Create(promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Create
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, New Prompts.UserResponse() {}, New String() {}, 0, 0)
            Return newUserprompt
        End Function

        Public Function Create(promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse)) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Create
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, possibleResponses, New String() {}, 0, 0)
            Return newUserprompt
        End Function

        Public Function Create(promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse), digitaloutputChannelIds As IReadOnlyList(Of String), blinkTimeOn As Integer, blinkTimeOff As Integer) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Create
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, possibleResponses, digitaloutputChannelIds, blinkTimeOn, blinkTimeOff)
            Return newUserprompt
        End Function

        Public Function [Set](promptText As String) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, Prompts.PromptTypes.Information, Prompts.DisplayOptions.None, promptText, New Prompts.UserResponse() {}, New String() {}, 0, 0)
            SetCurrent(newUserprompt)
            Return newUserprompt
        End Function

        Public Function [Set](promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse)) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, Prompts.PromptTypes.Information, Prompts.DisplayOptions.None, promptText, possibleResponses, New String() {}, 0, 0)
            SetCurrent(newUserprompt)
            Return newUserprompt
        End Function

        Public Function [Set](promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, New Prompts.UserResponse() {}, New String() {}, 0, 0)
            SetCurrent(newUserprompt)
            Return newUserprompt
        End Function

        Public Function [Set](promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse)) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, possibleResponses, New String() {}, 0, 0)
            SetCurrent(newUserprompt)
            Return newUserprompt
        End Function

        Public Function [Set](promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of Prompts.UserResponse), digitaloutputChannelIds As IReadOnlyList(Of String), blinkTimeOn As Integer, blinkTimeOff As Integer) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            Dim newUserprompt As New UserPrompt(_runtimeparametersStation.StationText, promptType, displayOption, promptText, possibleResponses, digitaloutputChannelIds, blinkTimeOn, blinkTimeOff)
            SetCurrent(newUserprompt)
            Return newUserprompt
        End Function

        Public Function [Set](userPromptParam As Prompts.IPrompt) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set
            SetCurrent(userPromptParam)
            Return userPromptParam
        End Function


        'Public Function [Set](ByVal ErrorMessageSet As structErrorMessageSet, possibleResponses As IReadOnlyList(Of Prompts.UserResponse)) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set

        '    Dim displayOption As Prompts.DisplayOptions = DisplayOptions.None
        '    Dim promptType As Prompts.PromptTypes = PromptTypes.None
        '    Dim newUserprompt As UserPrompt
        '    Dim messageTest As String = String.Empty

        '    messageTest = String.Format("[EC{0}]", ErrorMessageSet.iErrorCode.ToString("000"))
        '    messageTest += ErrorMessageSet.strErrorMessage

        '    If _currentPrompt IsNot Nothing Then
        '        _currentPrompt.PromptText = messageTest
        '        _currentPrompt.StationText = ErrorMessageSet.strErrorSource
        '        _currentPrompt.RaisedTimeText = ErrorMessageSet.strRaisedTime
        '    End If

        '    If ErrorMessageSet = _LastErrorMessage Then
        '        Return Nothing
        '    End If

        '    Select Case ErrorMessageSet.strErrorType
        '        Case enumPLC_ERROR_TYPE.MasterError.ToString
        '            promptType = PromptTypes.Problem
        '        Case enumPLC_ERROR_TYPE.Error.ToString
        '            promptType = PromptTypes.Alarm
        '        Case enumPLC_ERROR_TYPE.Message.ToString
        '            promptType = PromptTypes.Information
        '        Case enumPLC_ERROR_TYPE.MasterMessage.ToString
        '            promptType = PromptTypes.Warning
        '        Case Else
        '            promptType = PromptTypes.None
        '    End Select

        '    If promptType = PromptTypes.None Then
        '        newUserprompt = Nothing
        '        SetCurrent(Nothing)
        '        _LastErrorMessage = New structErrorMessageSet
        '    Else

        '        newUserprompt = New UserPrompt(ErrorMessageSet.strErrorSource, promptType, displayOption, messageTest, possibleResponses, New String() {}, 0, 0)
        '        SetCurrent(newUserprompt)
        '        _LastErrorMessage = ErrorMessageSet
        '    End If

        '    Return newUserprompt
        'End Function


        Public Function [Set](ByVal ErrorMessageSet As structErrorMessageSet, possibleResponses As IReadOnlyList(Of Prompts.UserResponse), Optional ByVal RefreshPrompt As Boolean = True) As Prompts.IPrompt Implements Prompts.IStationPromptsController.Set

            Dim displayOption As Prompts.DisplayOptions = DisplayOptions.None
            Dim promptType As Prompts.PromptTypes = PromptTypes.None
            Dim errorCode As String = String.Empty

            If _PcResetError Then
                iCounter = iCounter + 1
                If iCounter >= 20 Then _PcResetError = False
            Else
                iCounter = 0
            End If

            'If ErrorMessageSet = _LastErrorMessage Then Return Nothing

            If RefreshPrompt Then SetCurrent(Nothing)

            Select Case ErrorMessageSet.strErrorType
                Case enumHMI_ERROR_TYPE.MasterError.ToString
                    promptType = PromptTypes.Problem
                Case enumHMI_ERROR_TYPE.Error.ToString
                    promptType = PromptTypes.Alarm
                Case enumHMI_ERROR_TYPE.Tips.ToString
                    promptType = PromptTypes.Information
                Case enumHMI_ERROR_TYPE.MasterMessage.ToString, enumHMI_ERROR_TYPE.Message.ToString
                    promptType = PromptTypes.Warning
                Case Else
                    promptType = PromptTypes.None
            End Select

            errorCode = String.Format("EC{0}", ErrorMessageSet.iErrorCode.ToString("000"))
            If errorCode = "EC000" Then errorCode = ""

            If _LastUserprompt Is Nothing Or _currentPrompt Is Nothing Then
                _LastUserprompt = New UserPrompt(ErrorMessageSet.strErrorSource,
                                                 promptType,
                                                 displayOption,
                                                 ErrorMessageSet.strErrorMessage,
                                                 possibleResponses,
                                                 New String() {}, 0, 0,
                                                 errorCode,
                                                 ErrorMessageSet.strRaisedTime)

                SetCurrent(_LastUserprompt)
            ElseIf _currentPrompt IsNot Nothing Then
                _currentPrompt.PromptText = ErrorMessageSet.strErrorMessage
                _currentPrompt.ErrorCode = errorCode
                _currentPrompt.StationText = ErrorMessageSet.strErrorSource
                _currentPrompt.RaisedTimeText = ErrorMessageSet.strRaisedTime
                _currentPrompt.TypeOfPrompt = promptType
            End If

            _LastErrorMessage = ErrorMessageSet

            Return _LastUserprompt
        End Function
        ''added wang
        'Public Function [Set](StationText As String, promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, promptText As String) As Prompts.IPrompt
        '    Dim newUserprompt As New UserPrompt(StationText, promptType, displayOption, promptText, New Kostal.Testman.Framework.Base.Prompts.UserResponse() {}, New String() {}, 0, 0)
        '    SetCurrent(newUserprompt)
        '    Return newUserprompt
        'End Function

        Private Sub CurrentUserPrompt_PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case Member.Of((Function() _currentPrompt.ResponseId))
                    Dim prompt As UserPrompt = TryCast(sender, UserPrompt)
                    If prompt Is Nothing Then Return

                    ' If the ResponseId is set with a value the prompt can be cleared
                    If Not prompt.ResponseId.IsNullOrEmpty() Then
                        SetCurrent(Nothing)
                        'RemoveHandler Me.PropertyChanged, AddressOf UserPrompt_PropertyChanged
                    ElseIf prompt.IsTimedOut AndAlso prompt.ClearMessageWhenTimedOut Then
                        ' If a timeout occur and the ClearMessageWhenTimedOut is set
                        SetCurrent(Nothing)
                    End If

                    _LastUserprompt = Nothing
                    _LastErrorMessage = New structErrorMessageSet

                    _PcResetError = True

                Case Else

            End Select
        End Sub

        ''' <summary>
        ''' Removes the current message for this station.
        ''' </summary>
        Public Sub Clear() Implements Prompts.IStationPromptsController.Clear
            SetCurrent(Nothing)
        End Sub

    End Class


    Public Class TestStationParameters
        Inherits NotifyingObject

        Private _StationText As String

        Public Sub New()
            _StationText = "Sample Test"
        End Sub

        Public Property StationText As String
            Get
                Return _StationText
            End Get
            Set(value As String)
                _StationText = value
                OnPropertyChanged()
            End Set
        End Property

    End Class

End Namespace