Namespace Csv.Mapping

    ''' <summary>
    ''' Strategy how a string is parsed and outputted
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum StringModification
        ''' <summary>
        ''' The string is used without any modification.
        ''' </summary>
        ''' <remarks></remarks>
        Unmodified
        ''' <summary>
        ''' All whitespaces at start and end of the string are removed.
        ''' </summary>
        ''' <remarks></remarks>
        Trim
        ''' <summary>
        ''' All whitespaces at start, end and within the string are removed.
        ''' </summary>
        ''' <remarks></remarks>
        RemoveWhitespaces
    End Enum


End Namespace