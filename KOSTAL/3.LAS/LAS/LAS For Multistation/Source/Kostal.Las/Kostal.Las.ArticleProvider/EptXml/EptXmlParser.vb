Imports System.IO
Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Ept.Mapping
Imports System.Xml.XPath
Imports Kostal.Las.ArticleProvider.Ept.Fluent
Namespace Ept
    ''' <summary>
    ''' Uses the inserted <see cref="ReaderConfiguration"/> object to parse a xml-formatted EPT output file into a <see cref="ArticleCollection"/> known by Testman Framework
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EptXmlParser

        Public Const EptNodeName As String = "Ept"
        Public Const RowNodeName As String = "Row"
        Public Const ColumnNodeName As String = "Column"
        Public Const ColumnNodeNameAttributeName As String = "name"
        Public Const RowNodeVariantAttributeName As String = "variant"

        Private ReadOnly _config As ReaderConfiguration

        ''' <summary>
        ''' Creates a new instance of <see cref="EptXmlParser"/> 
        ''' </summary>
        ''' <param name="configuration">The configuration to be used</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal configuration As ReaderConfiguration)
            _config = configuration
        End Sub

        ''' <summary>
        ''' Starts the parsing process based on the configuration inserted in constructor.
        ''' This routine opens the xml-formatted EPT output file, reads the content and creates an according <see cref="ArticleCollection"/>.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Read() As ArticleCollection

            Dim collection As New ArticleCollection
            Dim xmlEptOutputDoc As XPathDocument
            Dim xmlEptArticles As New List(Of EptXmlArticle)

            ' check the xml-formatted EPT output file
            If Not File.Exists(_config.Filename) Then
                Throw New FileNotFoundException("XML-formatted EPT output file not found. Please check the configuration.", _config.Filename)
            Else
                xmlEptOutputDoc = New XPathDocument(_config.Filename)
            End If

            ' check the xml after loading
            If xmlEptOutputDoc Is Nothing Then
                Throw New NullReferenceException(String.Format("Failed loading <{0}>. Please check the content.", _config.Filename))
            End If

            ' create the navigator and check the "Ept" node
            Dim xmlNavigator As XPathNavigator = xmlEptOutputDoc.CreateNavigator()
            xmlNavigator.MoveToRoot()

            If (xmlNavigator Is Nothing) OrElse (Not xmlNavigator.MoveToFirstChild()) OrElse (Not xmlNavigator.Name.Equals(EptNodeName)) Then
                Throw New Exception(String.Format("Expected XML node <{0}> not found. Please check the content format of <{1}>.", EptNodeName, _config.Filename))
            End If

            ' get all variants with all properties
            xmlEptArticles.Clear()
            While xmlNavigator.MoveToFollowing(RowNodeName, String.Empty)
                Dim article As New EptXmlArticle(xmlNavigator.GetAttribute(RowNodeVariantAttributeName, String.Empty))
                Dim propertyNavigator As XPathNavigator = xmlNavigator.CreateNavigator

                While propertyNavigator.MoveToFollowing(ColumnNodeName, String.Empty)
                    Dim propertyName As String = propertyNavigator.GetAttribute(ColumnNodeNameAttributeName, String.Empty)
                    Dim propertyValue As String = propertyNavigator.Value

                    article.AddProperty(New EptXmlArticleProperty(propertyName, propertyValue))
                End While

                xmlEptArticles.Add(article)
            End While

            ' do parsing
            For Each article As EptXmlArticle In xmlEptArticles

                If _config.UseUnmappedPropertiesAsCustomEntries Then
                    CreateCustomEntryProperties(article.PropertyNames.ToArray, _config.OverwriteExistingCustomEntries)
                End If

                For Each item As Mapping.Mapping In _config.GetUsedMappings
                    item.SetupPropertyNames(article.PropertyNames.ToArray)
                Next item

                Dim builder As New ArticleBuilder(article.Id)

                ' Row variants attributes
                For Each item As XmlNodeVariantRowMapping In _config.RowElements.OfType(Of XmlNodeVariantRowMapping)
                    Dim currentResult As MappedValue = item.GetRowValue(article.Id)
                    ' Check if the property is a 'normal' ArticleAttribute, but not all TestmanProperties defined in the xsd are 'normal' ArticleAttribute
                    If [Enum].IsDefined(GetType(ArticleAttribute), item.ArticleProperty) Then
                        builder.AddTag(CType([Enum].Parse(GetType(ArticleAttribute), item.ArticleProperty), ArticleAttribute), currentResult.Value)
                    Else
                        ' so add not defined as Custom Tags
                        builder.AddCustomTag(item.ArticleProperty.Replace(" "c, "_"c), currentResult.Value, item.ShowInArticleSelector, True)
                    End If
                Next item

                ' Get all properties but IniFiles
                For Each item As ConfiguredMapping In _config.Properties

                    Dim currentResult As MappedValue = item.Mapping.GetValue(article.PropertyValues.ToArray)
                    If currentResult.AddToConfiguration = True Then

                        ' consider EmptyColumnOption
                        Dim emptyColumnOption As Mapping.Mapping.OnEmptyEptPropertyOption
                        If item.Mapping.EmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.Unknown Then
                            ' use global EmptyColumnOption
                            emptyColumnOption = _config.EmptyEptPropertyOption
                        Else
                            ' use property's EmptyColumnOption
                            emptyColumnOption = item.Mapping.EmptyEptPropertyOption
                        End If

                        ' add field if not empty and not skipped
                        If Not ((emptyColumnOption = Mapping.Mapping.OnEmptyEptPropertyOption.Skip) AndAlso String.IsNullOrEmpty(currentResult.Value)) Then
                            If [Enum].IsDefined(GetType(ArticleAttribute), item.Name) Then
                                builder.AddTag(CType([Enum].Parse(GetType(ArticleAttribute), item.Name), ArticleAttribute), currentResult.Value)
                            Else
                                builder.AddCustomTag(item.Name.Replace(" "c, "_"c), currentResult.Value, item.Mapping.ShowInArticleSelector, item.Overwrite)
                            End If
                        End If

                    End If
                Next item

                ' Get IniFiles
                For Each item As Mapping.Mapping In _config.Files
                    Dim currentResult As MappedValue = item.GetValue(article.PropertyValues.ToArray)
                    If currentResult.AddToConfiguration Then
                        If (item.EmptyEptPropertyOption <> Mapping.Mapping.OnEmptyEptPropertyOption.Skip) OrElse (Not String.IsNullOrEmpty(currentResult.Value)) Then
                            builder.AddFile(currentResult.Value)
                        End If
                    End If
                Next item




                collection.Add(builder.Article)

            Next article


            Return collection

        End Function

        ''' <summary>
        ''' Extends the current <see cref="ReaderConfiguration"/> to handle the unused columns as custom entries.
        ''' For every unused header an additional <see cref="XmlNodeMapping"/> is created. The column name is used as the name of the custom tag.
        ''' </summary>
        ''' <param name="headers">the headers available in the csv file</param>
        ''' <remarks></remarks>
        Protected Sub CreateCustomEntryProperties(ByVal headers As String(), ByVal overwrite As Boolean)

            Dim unused As IEnumerable(Of String) = GetUnmappedProperties(headers, _config.GetUsedMappings)

            If unused.Count > 0 Then
                For Each item As String In unused
                    Dim mapping As New XmlNodeMapping(item)
                    _config.Properties.Add(New ConfiguredMapping(item, mapping, overwrite))
                Next
            End If

        End Sub

        ''' <summary>
        ''' Checks the list of headers against the list of available mappings to get all headers that are not used within any mapping.
        ''' </summary>
        ''' <param name="xmlPropertyNames">list of property names to be checked</param>
        ''' <param name="existingMappings">list of available mappings</param>
        ''' <returns>all items that occured in <paramref name="xmlPropertyNames"/> but was not used in <paramref name="existingMappings"/>.</returns>
        ''' <remarks></remarks>
        Protected Function GetUnmappedProperties(ByVal xmlPropertyNames As IEnumerable(Of String), ByVal existingMappings As IEnumerable(Of Mapping.Mapping)) As IEnumerable(Of String)

            Dim unmappedProperties As IEnumerable(Of String) = From propertyMapping _
                                                        In existingMappings _
                                                        Where TypeOf (propertyMapping) Is XmlNodeMapping _
                                                        Select CType(propertyMapping, XmlNodeMapping).NodeName


            Return From header In xmlPropertyNames Where Not unmappedProperties.Contains(header)

        End Function

    End Class
End Namespace
