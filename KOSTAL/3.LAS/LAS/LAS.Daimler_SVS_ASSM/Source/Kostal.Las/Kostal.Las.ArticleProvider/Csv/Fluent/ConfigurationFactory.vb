Namespace Csv
    ''' <summary>
    ''' Used to create a new <see cref="ReaderConfiguration"/> using the fluent definition aproach.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigurationFactory

        ''' <summary>
        ''' Assigns name and path of the csv file containing the article definitions
        ''' </summary>
        ''' <param name="csvFile">name and path of csv file to use</param>
        ''' <returns>a incomplete configuration containing a file already</returns>
        ''' <remarks></remarks>
        Public Shared Function ConfigureUsingFile(ByVal csvFile As String) As CsvFileConfiguration
            Return New CsvFileConfiguration(New ReaderConfiguration(csvFile))
        End Function

    End Class
End Namespace