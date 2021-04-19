Namespace Csv.Mapping

    ''' <summary>
    ''' Defines a mapping between a column in a csv file and an article property in 
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Mapping

        Public Enum OnEmptyColumnOption As Integer
            Unknown = 0
            NullEntry = 1
            SkipField = 2
        End Enum

        ''' <summary>
        ''' Inserts all available headers into the mapping.
        ''' Used once when a new csv file is opened and the header row is read. 
        ''' As all upcoming cvs data is values only this routine can be used to evaluate the index of the mapped csv column.
        ''' </summary>
        ''' <param name="headers">list of headers as read from csv file</param>
        ''' <remarks></remarks>
        Friend MustOverride Sub SetupHeaders(ByVal headers As String())

        ''' <summary>
        ''' Inserts a single line of values read from csv file and expectes a single value extracted based on mapping configuration.
        ''' </summary>
        ''' <param name="csvRow">all values of a single row read from csv file</param>
        ''' <returns>the single value evaluated by the mapping</returns>
        ''' <remarks></remarks>
        Friend MustOverride Function GetValue(ByVal csvRow() As String) As MappingValue

        Friend MustOverride Property EmptyColumnOption As OnEmptyColumnOption

        Friend MustOverride Property ShowInArticleSelector As Boolean

    End Class
End Namespace