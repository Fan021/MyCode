Namespace Ept.Fluent

    ''' <summary>
    ''' Used to create a new <see cref="ReaderConfiguration"/> using the fluent definition approach
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigurationFactory

        ''' <summary>
        ''' Assigns name and path of the xml-formatted EPT output file containing the article definitions
        ''' </summary>
        ''' <param name="file">Path and name of xml output file to use</param>
        ''' <returns>Incomplete configuration containing a file already</returns>
        ''' <remarks></remarks>
        Public Shared Function ConfigureUsingFile(ByVal file As String) As IncompleteConfiguration
            Return New IncompleteConfiguration(New ReaderConfiguration(file))
        End Function

    End Class

End Namespace