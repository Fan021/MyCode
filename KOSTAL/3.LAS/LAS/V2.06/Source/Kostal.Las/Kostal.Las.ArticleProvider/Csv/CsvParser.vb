Imports System.IO
Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv
    ''' <summary>
    ''' Uses the inserted <see cref="ReaderConfiguration"/> object to parse a csv file into a <see cref="ArticleCollection"/> usable by Testman Framework.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvParser

        Private ReadOnly _config As ReaderConfiguration

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvParser"/> 
        ''' </summary>
        ''' <param name="configuration">The configuration to be used by the parser</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal configuration As ReaderConfiguration)
            _config = configuration
        End Sub

        ''' <summary>
        ''' Starts the parsing process based on the configuration inserted in constructor.
        ''' This routine opens the csv file, reads the content and creates the <see cref="ArticleCollection"/>
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Read() As ArticleCollection

            Dim collection As New ArticleCollection()

            Using textReader As New StreamReader(_config.Filename, True)

                Using reader As New LumenWorks.Framework.IO.Csv.CsvReader(textReader, True, ";"c)

                    reader.DefaultParseErrorAction = LumenWorks.Framework.IO.Csv.ParseErrorAction.ThrowException

                    If _config.UseAdditionalColumnsAsCustomEntries Then
                        CreateCustomEntryColumns(reader.GetFieldHeaders, _config.OverwriteExistingCustomEntries)
                    End If

                    For Each item As Mapping.Mapping In _config.GetUsedMappings
                        item.SetupHeaders(reader.GetFieldHeaders)
                    Next item

                    While reader.ReadNextRecord

                        Dim values(reader.GetFieldHeaders.Count - 1) As String
                        reader.CopyCurrentRecordTo(values)

                        Dim keyResult = _config.KeyColumn.GetValue(values)
                        If keyResult.AddToConfiguration = False Then Throw New ArgumentException("'AddToConfiguration' returned FALSE for key column.")

                        Dim builder As New ArticleBuilder(keyResult.Value)

                        ' Get all properties but IniFiles
                        For Each item In _config.Properties

                            Dim currentResult = item.Mapping.GetValue(values)
                            If currentResult.AddToConfiguration = True Then

                                ' consider EmptyColumnOption
                                Dim emptyColumnOption As Mapping.Mapping.OnEmptyColumnOption
                                If item.Mapping.EmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.Unknown Then
                                    ' use global EmptyColumnOption
                                    emptyColumnOption = _config.EmptyColumnOption
                                Else
                                    ' use property's EmptyColumnOption
                                    emptyColumnOption = item.Mapping.EmptyColumnOption
                                End If

                                ' add field if not empty and not skipped
                                If Not ((emptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.SkipField) AndAlso String.IsNullOrEmpty(currentResult.Value)) Then
                                    If [Enum].IsDefined(GetType(ArticleAttribute), item.Name) Then
                                        builder.AddTag(CType([Enum].Parse(GetType(ArticleAttribute), item.Name), ArticleAttribute), currentResult.Value)
                                    Else
                                        builder.AddCustomTag(item.Name.Replace(" "c, "_"c), currentResult.Value, item.Mapping.ShowInArticleSelector, item.Overwrite)
                                    End If
                                End If

                            End If
                        Next item

                        ' Get IniFiles
                        For Each item In _config.Files
                            Dim currentResult = item.GetValue(values)
                            If currentResult.AddToConfiguration = True Then
                                If item.EmptyColumnOption <> Mapping.Mapping.OnEmptyColumnOption.SkipField Then
                                    builder.AddFile(currentResult.Value)
                                End If
                            End If
                        Next item

                        collection.Add(builder.Article)
                    End While

                End Using
            End Using

            Return collection

        End Function

        ''' <summary>
        ''' Extends the current <see cref="ReaderConfiguration"/> to handle the unused columns as custom entries.
        ''' For every unused header an additional <see cref="CsvColumnMapping"/> is created. The column name is used as the name of the custom tag.
        ''' </summary>
        ''' <param name="headers">the headers available in the csv file</param>
        ''' <remarks></remarks>
        Protected Sub CreateCustomEntryColumns(ByVal headers As String(), ByVal overwrite As Boolean)

            Dim unused = GetUnusedHeaders(headers, _config.GetUsedMappings)

            If unused.Count > 0 Then
                For Each item In unused
                    Dim mapping As New CsvColumnMapping(item)
                    _config.Properties.Add(New ConfiguredMapping(item, mapping, overwrite))
                Next
            End If

        End Sub

        ''' <summary>
        ''' Checks the list of headers against the list of available mappings to get all headers that are not used within any mapping.
        ''' </summary>
        ''' <param name="headers">list of headers to be checked</param>
        ''' <param name="existingMappings">list of available mappings</param>
        ''' <returns>all items that occured in <paramref name="headers"/> but was not used in <paramref name="existingMappings"/>.</returns>
        ''' <remarks></remarks>
        Protected Function GetUnusedHeaders(ByVal headers As String(), ByVal existingMappings As IEnumerable(Of Mapping.Mapping)) As IEnumerable(Of String)

            Dim usedHeaders As IEnumerable(Of String) = From headerMapping _
                                                        In existingMappings _
                                                        Where TypeOf (headerMapping) Is CsvColumnMapping _
                                                        Select CType(headerMapping, CsvColumnMapping).ColumnHeader


            Return From header In headers Where Not usedHeaders.Contains(header)

        End Function

    End Class
End Namespace
