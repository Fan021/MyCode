Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv
    ''' <summary>
    ''' Represents a temporary step in creating a <see cref="ReaderConfiguration"/>.
    ''' Only the file in configured yet. Additional steps of fluent configuration are required to complete the configuration.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvFileConfiguration

        Private ReadOnly _config As ReaderConfiguration

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvFileConfiguration"/>
        ''' </summary>
        ''' <param name="config">the config to extend already containing a configuration for csv file.</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal config As ReaderConfiguration)
            _config = config
        End Sub

        ''' <summary>
        ''' Sets the configuration to use the first column of the csv file as key column.
        ''' This requires that the referenced column contains unique values in every row.
        ''' </summary>
        ''' <returns>A still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function UsingFirstColumnAsKey() As IncompleteConfiguration
            _config.KeyColumn = New KeyColumnMapping
            Return New IncompleteConfiguration(_config)
        End Function

        ''' <summary>
        ''' Sets the configuration to use the first column of the csv file as key column.
        ''' This requires that the referenced column contains unique values in every row.
        ''' </summary>
        ''' <param name="strategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from csv file.</param>
        ''' <returns>A still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function UsingFirstColumnAsKey(ByVal strategy As StringModification) As IncompleteConfiguration
            _config.KeyColumn = New KeyColumnMapping(strategy)
            Return New IncompleteConfiguration(_config)
        End Function


        ''' <summary>
        ''' Sets the configuration to use a specific column of the csv file assigned by a <see cref="Mapping"/> as key column.
        ''' This requires that the referenced column contains unique values in every row.
        ''' </summary>
        ''' <returns>A still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function UsingSpecificKeyColumn(ByVal mapping As Mapping.Mapping) As IncompleteConfiguration
            _config.KeyColumn = New KeyColumnMapping(mapping)
            Return New IncompleteConfiguration(_config)
        End Function

    End Class
End Namespace