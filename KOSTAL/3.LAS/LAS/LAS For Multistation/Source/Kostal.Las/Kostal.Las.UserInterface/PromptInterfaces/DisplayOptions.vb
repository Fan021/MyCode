Namespace Prompts

    ''' <summary>
    ''' Display options for the prompt
    ''' </summary>
    <Flags>
    Public Enum DisplayOptions

        ''' <summary>
        ''' The are no special display options
        ''' </summary>
        None = 0

        ''' <summary>
        ''' The prompt does a windows beep, if set fresh
        ''' </summary>
        ''' <remarks></remarks>
        WithBeep = 1

        ''' <summary>
        ''' The prompt is only indicated at the station
        ''' </summary>
        ''' <remarks>
        ''' UserRepsones are not allowed to add
        ''' </remarks>
        Local = 2

        ''' <summary>
        ''' The prompt asks for an input
        ''' </summary>
        Input = 4
    End Enum

End Namespace