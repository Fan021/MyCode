
Public Class UserPrompt
    Inherits NotifyingObject
    Implements Prompts.IPrompt

    Private _raised As System.DateTime = System.DateTime.Now
    Private _raisedTimeText As System.String = System.String.Empty
    Private _stationText As System.String = System.String.Empty
    Private _promptType As Prompts.PromptTypes
    Private _promptText As System.String = System.String.Empty
    Private _ErrorCode As System.String = System.String.Empty
    Private ReadOnly _displayOption As Prompts.DisplayOptions
    Private ReadOnly _possibleResponses As IReadOnlyList(Of Prompts.IResponse) = {}
    Private ReadOnly _digitalOutputChannelIds As IReadOnlyList(Of String)
    Private ReadOnly _blinkTimeOn As Integer = 0
    Private ReadOnly _blinkTimeOff As Integer = 0
    Private _responseIdLockObject As New Object
    Private _responseId As String = Nothing
    Private _clearMessageWhenTimedOut As Boolean
    Private _isTimedOut As Boolean
    Private _autoreseteventResult As Threading.AutoResetEvent

    Const MESSAGE_RAISED_ON As String = "Message raised on "

    Friend Sub New(stationText As String, promptType As Prompts.PromptTypes, displayOption As Prompts.DisplayOptions, messageText As String, possibleResponses As IReadOnlyList(Of Prompts.IResponse), digitalOutputChannelIds As IReadOnlyList(Of String), blinkTimeOn As Integer, blinkTimeOff As Integer, Optional ErrorCode As String = "", Optional RaisedTime As String = "")
        _stationText = stationText
        _promptType = promptType
        _displayOption = displayOption
        _raisedTimeText = MESSAGE_RAISED_ON & RaisedTime
        _ErrorCode = ErrorCode
        'ResetRaised()
        'RaisedTimeText = Raised.ToString("T")
        'Dim l As New Globalization.Localizer(True)
        '_promptText = l.GetLocalizedString(messageText)
        '_promptText = (New Globalization.Localizer(3)).GetLocalizedString(messageText)
        _promptText = (New Globalization.Localizer(3)).GetLocalizedString(messageText)

        If possibleResponses IsNot Nothing Then _possibleResponses = possibleResponses

        If (_displayOption And Prompts.DisplayOptions.Local) = Prompts.DisplayOptions.Local Then
            If _possibleResponses.Count > 0 Then Throw New System.ApplicationException("Using a local prompt with responses is not allowed")
            If (_displayOption And Prompts.DisplayOptions.Input) = Prompts.DisplayOptions.Input Then
                Throw New System.ApplicationException("Using a local prompt with input or list is not allowed")
            End If
        End If

        'Prevent empty and duplicate channel ids
        Dim listDigitalOutputChannelIds As New List(Of String)
        For Each digitalOutputChannelId As String In digitalOutputChannelIds
            If Not digitalOutputChannelId.IsNullOrWhiteSpace AndAlso Not listDigitalOutputChannelIds.Contains(digitalOutputChannelId) Then listDigitalOutputChannelIds.Add(digitalOutputChannelId)
        Next
        _digitalOutputChannelIds = listDigitalOutputChannelIds.ToArray()

        If blinkTimeOn > 0 AndAlso blinkTimeOff > 0 Then
            _blinkTimeOn = blinkTimeOn
            _blinkTimeOff = blinkTimeOff
        End If
    End Sub

    Public ReadOnly Property Raised As Date Implements Prompts.IPrompt.Raised
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _raised
        End Get
    End Property

    Friend Sub ResetRaised()
        _raised = System.DateTime.Now
        OnPropertyChanged(Member.Of(Function() Me.Raised))
    End Sub

    Public Property StationText As String Implements Prompts.IPrompt.StationText
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _stationText
        End Get
        Set(value As String)
            If _stationText = value Then Return
            _stationText = value
            OnPropertyChanged(Member.Of(Function() Me.StationText))
        End Set
    End Property

    Public Property RaisedTimeText As String Implements Prompts.IPrompt.RaisedTimeText
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _raisedTimeText
        End Get
        Set(value As String)
            If _raisedTimeText = MESSAGE_RAISED_ON & value Then Return
            _raisedTimeText = MESSAGE_RAISED_ON & value
            OnPropertyChanged(Member.Of(Function() Me.RaisedTimeText))
        End Set
    End Property

    Public Property ErrorCode As String Implements Prompts.IPrompt.ErrorCode
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _ErrorCode
        End Get
        Set(value As String)
            If _ErrorCode = value Then Return
            _ErrorCode = value
            OnPropertyChanged(Member.Of(Function() Me.ErrorCode))
        End Set
    End Property

    Public Property PromptType As Prompts.PromptTypes Implements Prompts.IPrompt.TypeOfPrompt
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _promptType
        End Get
        Set(value As Prompts.PromptTypes)
            If _promptType = value Then Return
            _promptType = value
            OnPropertyChanged(Member.Of(Function() Me.PromptType))
        End Set
    End Property

    Public ReadOnly Property DisplayOption As Prompts.DisplayOptions Implements Prompts.IPrompt.DisplayOption
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _displayOption
        End Get
    End Property

    Public Property PromptText As String Implements Prompts.IPrompt.PromptText
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _promptText
        End Get
        Set(value As String)
            If _promptText = value Then Return
            _promptText = value
            OnPropertyChanged(Member.Of(Function() Me.PromptText))
        End Set
    End Property

    Public ReadOnly Property PossibleResponses As IReadOnlyList(Of Prompts.IResponse) Implements Prompts.IPrompt.PossibleResponses
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _possibleResponses
        End Get
    End Property

    Public ReadOnly Property DigitalOutputChannelIds As IReadOnlyList(Of String) Implements Prompts.IPrompt.DigitalOutputChannelIds
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _digitalOutputChannelIds
        End Get
    End Property

    Public ReadOnly Property BlinkTimeOff As Integer Implements Prompts.IPrompt.BlinkTimeOff
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _blinkTimeOff
        End Get
    End Property

    Public ReadOnly Property BlinkTimeOn As Integer Implements Prompts.IPrompt.BlinkTimeOn
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _blinkTimeOn
        End Get
    End Property

    ''' <summary>
    ''' Gets the id of the response.
    ''' </summary>
    ''' <value>
    ''' The response id.
    ''' </value>
    Public ReadOnly Property ResponseId As String Implements Prompts.IPrompt.ResponseId
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _responseId
        End Get
    End Property

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub SetResponseId(newResponseId As String)
        If String.Equals(_responseId, newResponseId) Then Return
        'NLog.LogManager.GetCurrentClassLogger().Info("Setting '{0}' as response for Prompt {1}", newResponseId, Me)
        _responseId = newResponseId
        OnPropertyChanged(Member.Of(Function() Me.ResponseId))
    End Sub

    ''' <summary>
    ''' Gets a value indicating whether ClearMessageWhenTimedOut was set when WaitForResponse is called.
    ''' </summary>
    Friend ReadOnly Property ClearMessageWhenTimedOut As Boolean
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _clearMessageWhenTimedOut
        End Get
    End Property

    ''' <summary>
    ''' Gets <c>True</c> if a time out occur when WaitForResponse is called.
    ''' </summary>
    ''' <value>
    ''' The boolean value if the time out occur
    ''' </value>
    Public ReadOnly Property IsTimedOut As Boolean Implements Prompts.IPrompt.IsTimedOut
        <System.Diagnostics.DebuggerStepThrough()>
        Get
            Return _isTimedOut
        End Get
    End Property

    ''' <summary>
    ''' Clears the response id for this prompt.
    ''' </summary>
    Public Sub ClearResponse() Implements Prompts.IPrompt.ClearResponse
        SyncLock _responseIdLockObject

            ' Store the new one
            SetResponseId(Nothing)

            ' If there is "someone" waiting, signal that wait is finished 
            If _autoreseteventResult IsNot Nothing Then _autoreseteventResult.Set()
        End SyncLock
    End Sub

    Public Function SetResponse(ByVal newResponseId As String) As Boolean Implements Prompts.IPrompt.SetResponse
        SyncLock _responseIdLockObject
            ' If there is already set a _responseId -> return False
            If Not _responseId.IsNullOrEmpty() Then Return False

            ' Store the new one
            SetResponseId(newResponseId)

            ' If there is "someone" waiting, signal that wait is finished 
            If _autoreseteventResult IsNot Nothing Then _autoreseteventResult.Set()
            Return True
        End SyncLock
    End Function

    ''' <summary>
    ''' Waits for response on this prompt for a maximum time (in milliseconds).
    ''' Blocks the current thread until the response or the timeout occur.
    ''' </summary>
    ''' <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
    ''' <param name="clearMessageWhenTimedOut">If set to <c>true</c> the message is cleared after  the timeout occur.</param>
    ''' <returns>
    ''' The id of the reponse
    ''' </returns>
    Public Function WaitForResponse(millisecondsTimeout As Integer, clearMessageWhenTimedOut As Boolean) As String Implements Prompts.IPrompt.WaitForResponse
        If _possibleResponses.Count < 1 _
           AndAlso (_displayOption And Prompts.DisplayOptions.Input) <> Prompts.DisplayOptions.Input Then
            SetResponse("")
            Return Me.ResponseId
        End If

        _autoreseteventResult = New Threading.AutoResetEvent(False)
        _clearMessageWhenTimedOut = clearMessageWhenTimedOut
        _isTimedOut = False
        If Not _autoreseteventResult.WaitOne(millisecondsTimeout) Then
            ' Timeout occur
            _autoreseteventResult.Close()
            _autoreseteventResult = Nothing
            _isTimedOut = True
            SetResponse("")
        End If
        Return Me.ResponseId
    End Function

    Public Overrides Function ToString() As String
        Return MyBase.ToString() + " - Raised = " + _raised.ToString("s") + " - StationText = '" + _stationText + "' - PromptType = '" + _promptType.ToString + "' - DisplayType = '" + _displayOption.ToString + "' - PromptText = '" + _promptText + "'"
    End Function

End Class