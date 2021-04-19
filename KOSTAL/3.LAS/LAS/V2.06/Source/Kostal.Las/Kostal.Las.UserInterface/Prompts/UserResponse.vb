Namespace Prompts

    ''' <summary>
    ''' Class for user responses used by prompts
    ''' </summary>
    Public Class UserResponse
        Implements IResponse

        Private ReadOnly _id As String
        Private ReadOnly _text As String
        Private ReadOnly _listChannelIds As New List(Of String)

        ''' <summary>
        ''' Create a new UserResponse
        ''' </summary>
        ''' <param name="id">The identifier of the User Response</param>
        ''' <param name="text">The shown text of the UserResponse</param>
        Public Sub New(id As String, text As String)
            _id = id
            _text = (New Globalization.Localizer(False)).GetLocalizedString(text)
        End Sub

        ''' <summary>
        ''' Create a new UserResponse
        ''' </summary>
        ''' <param name="id">The identifier of the User Response</param>
        ''' <param name="text">The shown text of the UserResponse</param>
        ''' <param name="channelId">The name of the (digital input) channel. It is also possible to add more channels names separated by a comma.</param>
        Public Sub New(id As String, text As String, channelId As String)
            _id = id
            _text = (New Globalization.Localizer(False)).GetLocalizedString(text)
            For Each singleChannelId As String In channelId.Split(","c)
                If Not System.String.IsNullOrWhiteSpace(singleChannelId) AndAlso Not _listChannelIds.Contains(singleChannelId.Trim()) Then _listChannelIds.Add(singleChannelId.Trim())
            Next
        End Sub

        ''' <summary>
        ''' Create a new UserResponse
        ''' </summary>
        ''' <param name="id">The identifier of the User Response</param>
        ''' <param name="text">The shown text of the UserResponse</param>
        ''' <param name="channelIds">The list of names of the (digital input) channel(s).</param>
        Public Sub New(id As String, text As String, channelIds As IEnumerable(Of String))
            _id = id
            _text = (New Globalization.Localizer(False)).GetLocalizedString(text)
            For Each singleChannelId As String In channelIds
                If Not System.String.IsNullOrWhiteSpace(singleChannelId) AndAlso Not _listChannelIds.Contains(singleChannelId.Trim()) Then _listChannelIds.Add(singleChannelId.Trim())
            Next
        End Sub

        ''' <summary>
        ''' The identifier of the user response
        ''' </summary>
        ''' <returns>The identifier</returns>
        Public ReadOnly Property Id As String Implements IResponse.Id
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _id
            End Get
        End Property

        ''' <summary>
        ''' The shown text of the user response.
        ''' </summary>
        ''' <returns>The text, if empty the id is returned.</returns>
        Public ReadOnly Property Text As String Implements IResponse.Text
            Get
                If System.String.IsNullOrWhiteSpace(_text) Then Return Id
                Return _text
            End Get
        End Property

        ''' <summary>
        ''' The list of the channels names
        ''' </summary>
        ''' <returns>A string array with the names of the channels.</returns>
        Public ReadOnly Property ChannelIds As String() Implements IResponse.DigitalInputChannelIds
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _listChannelIds.ToArray()
            End Get
        End Property

        ''' <summary>Returns a string that represents the current object.</summary>
        ''' <returns>A string that represents the current object.</returns>
        Public Overrides Function ToString() As String
            Return MyBase.ToString() + String.Format("{0}, Text: {1}, Channelnames: {2}", Id, Text, String.Join(",", ChannelIds))
        End Function
    End Class

End Namespace