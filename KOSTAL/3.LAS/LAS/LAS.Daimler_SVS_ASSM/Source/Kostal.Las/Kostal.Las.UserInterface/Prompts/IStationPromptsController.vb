
Imports Kostal.Las.Base

Namespace Prompts

    Public Interface IStationPromptsController
        Inherits ComponentModel.INotifyPropertyChanged

        ReadOnly Property Current As IPrompt

        Function Create(promptText As String) As IPrompt

        Function Create(promptText As String, possibleResponses As IReadOnlyList(Of UserResponse)) As IPrompt

        Function Create(promptType As PromptTypes, displayType As DisplayOptions, promptText As String) As IPrompt

        Function Create(promptType As PromptTypes, displayType As DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of UserResponse)) As IPrompt

        Function Create(promptType As PromptTypes, displayType As DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of UserResponse), digitaloutputChannelIds As IReadOnlyList(Of String), blinkTimeOn As Integer, blinkTimeOff As Integer) As IPrompt

        Function [Set](prompt As IPrompt) As IPrompt

        Function [Set](promptText As String) As IPrompt

        Function [Set](promptText As String, possibleResponses As IReadOnlyList(Of UserResponse)) As IPrompt

        Function [Set](promptType As PromptTypes, displayType As DisplayOptions, promptText As String) As IPrompt

        Function [Set](promptType As PromptTypes, displayType As DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of UserResponse)) As IPrompt

        Function [Set](promptType As PromptTypes, displayType As DisplayOptions, promptText As String, possibleResponses As IReadOnlyList(Of UserResponse), digitaloutputChannelIds As IReadOnlyList(Of String), blinkTimeOn As Integer, blinkTimeOff As Integer) As IPrompt

        Function [Set](ErrorMessage As structErrorMessageSet, possibleResponses As IReadOnlyList(Of UserResponse), Optional ByVal RefreshPrompt As Boolean = True) As IPrompt

        Sub Clear()

        ReadOnly Property State As Alarm.AlarmStates

        Property PromptType As PromptTypes

        Function Reset() As Boolean

    End Interface

End Namespace