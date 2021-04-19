Namespace Prompts

    ''' <summary>
    ''' Interface for user prompt
    ''' </summary>
    Public Interface IPrompt
        Inherits ComponentModel.INotifyPropertyChanged

        ''' <summary>
        ''' Gets the type of the prompt.
        ''' </summary>
        ''' <value>
        ''' The type of the prompt.
        ''' </value>
        Property TypeOfPrompt As PromptTypes

        ''' <summary>
        ''' Gets the display options.
        ''' </summary>
        ''' <value>
        ''' The display options.
        ''' </value>
        ReadOnly Property DisplayOption As DisplayOptions

        ''' <summary>
        ''' Gets the datetime of creation.
        ''' </summary>
        ''' <value>
        ''' The datetime of creation.
        ''' </value>
        ReadOnly Property Raised As System.DateTime

        ''' <summary>
        ''' Gets the datetime of creation.
        ''' </summary>
        ''' <value>
        ''' The datetime of creation.
        ''' </value>
        Property RaisedTimeText As System.String

        ''' <summary>
        ''' Gets the datetime of creation.
        ''' </summary>
        ''' <value>
        ''' The datetime of creation.
        ''' </value>
        Property ErrorCode As System.String

        ''' <summary>
        ''' Gets the station text.
        ''' </summary>
        ''' <value>
        ''' The station text.
        ''' </value>
        Property StationText As String
        'ReadOnly Property StationText As String  'wang65

        ''' <summary>
        ''' Gets the prompt text.
        ''' </summary>
        ''' <value>
        ''' The prompt text.
        ''' </value>
        Property PromptText As String

        ''' <summary>
        ''' Gets the possible responses.
        ''' </summary>
        ''' <value>
        ''' The possible responses.
        ''' </value>
        ReadOnly Property PossibleResponses As IReadOnlyList(Of IResponse)

        ''' <summary>
        ''' Gets the digital output channel ids.
        ''' </summary>
        ''' <value>
        ''' The digital output channel ids.
        ''' </value>
        ReadOnly Property DigitalOutputChannelIds As IReadOnlyList(Of String)

        ''' <summary>
        ''' Gets the time in milliseconds for the on state in blink Mode.
        ''' </summary>
        ''' <value>
        ''' The time for on in blink mode.
        ''' </value>
        ReadOnly Property BlinkTimeOn As Integer

        ''' <summary>
        ''' Gets the time in milliseconds for the off state in blink Mode.
        ''' </summary>
        ''' <value>
        ''' The time for off in blink mode.
        ''' </value>
        ReadOnly Property BlinkTimeOff As Integer

        ''' <summary>
        ''' Gets the id of the response.
        ''' </summary>
        ''' <value>
        ''' The response id.
        ''' </value>
        ReadOnly Property ResponseId As String

        ''' <summary>
        ''' Gets <c>True</c> if a time out occur when wait for the response.
        ''' </summary>
        ''' <value>
        ''' The boolean value if the time out occur
        ''' </value>
        ReadOnly Property IsTimedOut As Boolean

        ''' <summary>
        ''' Sets the response id for this prompt.
        ''' </summary>
        ''' <param name="responseId">The response id.</param>
        ''' <returns>True, if succesful otherwise false</returns>
        ''' <remarks>Can only be called once, until the repsonse is cleared.</remarks>
        Function SetResponse(responseId As String) As Boolean

        ''' <summary>
        ''' Clears the response id for this prompt.
        ''' </summary>
        Sub ClearResponse()

        ''' <summary>
        ''' Waits for response on this prompt for a maximum time (in milliseconds).
        ''' Blocks the current thread until the response or the timeout occur.
        ''' </summary>
        ''' <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
        ''' <param name="clearMessageWhenTimedOut">If set to <c>true</c> the message is cleared after the timeout occur.</param>
        ''' <returns>
        ''' The id of the reponse
        ''' </returns>
        Function WaitForResponse(millisecondsTimeout As Integer, clearMessageWhenTimedOut As Boolean) As String

    End Interface

End Namespace