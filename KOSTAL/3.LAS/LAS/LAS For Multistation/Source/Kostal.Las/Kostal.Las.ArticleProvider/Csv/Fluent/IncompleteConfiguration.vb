Imports Kostal.Las.ArticleProvider.Csv.Mapping
Imports Kostal.Las.ArticleProvider.Base
Namespace Csv
    ''' <summary>
    ''' Represents a temporary step in creating a <see cref="ReaderConfiguration"/>.
    ''' At least file and keycolumn have to be configured yet. Additional steps of fluent configuration are required to complete the configuration.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IncompleteConfiguration

        Public Const TestmanFlowFilePropertyName As String = "IniFile"

        Private ReadOnly _config As ReaderConfiguration

        ''' <summary>
        ''' Creates a new instance of <see cref="IncompleteConfiguration"/>
        ''' </summary>
        ''' <param name="config">a incomplete configuration where only key column and file reference is setup</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal config As ReaderConfiguration)
            _config = config
        End Sub

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="articleProperty">the article attribute that is mapped</param>
        ''' <param name="mapping">The <see cref="Mapping"/> used to assign a value</param>
        ''' <returns>a still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetArticleProperty(ByVal articleProperty As ArticleAttribute, ByVal mapping As Mapping.Mapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(articleProperty.ToString, mapping))
            Return Me

        End Function

        Public Function SetTestmanFlowFile(ByVal mapping As Mapping.Mapping) As IncompleteConfiguration

            If _config.Files.Contains(mapping) Then
                Throw New ArgumentException(String.Format("Duplicate file mapping definition '{0}'.", mapping.ToString))
            Else
                _config.Files.Add(mapping)
            End If

            Return Me

        End Function

        ' ''' <summary>
        ' ''' Adds a mapping for a <see cref="KostalArticleAttributes"/> used when configuring <see cref="ArticleConfigurationItem"/> 
        ' ''' </summary>
        ' ''' <param name="articleProperty">the article attribute that is mapped</param>
        ' ''' <param name="mapping">The <see cref="Mapping.Mapping"/> used to assign a value</param>
        ' ''' <returns>a still incomplete configuration</returns>
        ' ''' <remarks></remarks>
        Public Function SetKostalArticleProperty(ByVal articleProperty As ArticleAttribute, ByVal mapping As Mapping.Mapping) As IncompleteConfiguration

            If articleProperty = ArticleAttribute.IniFile Then

                If _config.Files.Contains(mapping) Then
                    Throw New ArgumentException(String.Format("Duplicate file mapping definition '{0}'.", mapping.ToString))
                End If

                _config.Files.Add(mapping)

            Else
                _config.Properties.Add(New ConfiguredMapping(articleProperty.ToString, mapping))
            End If

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">the custom field to be created.</param>
        ''' <param name="mapping">The <see cref="ConstantValueMapping"/> used to assign a value</param>
        ''' <returns>a still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConstantValueMapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))
            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">the custom field to be created.</param>
        ''' <param name="mapping">The <see cref="ConditionalMappingAnd"/> used to assign a value</param>
        ''' <returns>a still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConditionalMappingAnd) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">the custom field to be created.</param>
        ''' <param name="mapping">The <see cref="ConditionalMappingOr"/> used to assign a value</param>
        ''' <returns>a still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConditionalMappingOr) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">the custom field to be created.</param>
        ''' <param name="mapping">The <see cref="CsvColumnMapping"/> used to assign a value</param>
        ''' <returns>a still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As CsvColumnMapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Sets the configuration to throw away / ignore all columns that are not explicitly assigned by one of the mappings.
        ''' This is the last step when setting up configuration. The created <see cref="ReaderConfiguration"/> can be assigned to the <see cref="CsvArticleProvider.Configuration"/>
        ''' </summary>
        ''' <returns>a completly configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function ThrowAdditionalColumnsAway() As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseAdditionalColumnsAsCustomEntries = False
            Return _config
        End Function

        ''' <summary>
        ''' Sets the configuration to map all columns that are not explicitly assigned by one of the mappings as .
        ''' This is the last step when setting up configuration. The created <see cref="ReaderConfiguration"/> can be assigned to the <see cref="CsvArticleProvider.Configuration"/>
        ''' </summary>
        ''' <returns>a completly configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function UseAdditionalColumnsAsCustomEntries() As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseAdditionalColumnsAsCustomEntries = True
            Return _config
        End Function

        ''' <summary>
        ''' Sets the configuration to map all columns that are not explicitly assigned by one of the mappings as .
        ''' This is the last step when setting up configuration. The created <see cref="ReaderConfiguration"/> can be assigned to the <see cref="CsvArticleProvider.Configuration"/>
        ''' </summary>
        ''' <returns>a completly configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function UseAdditionalColumnsAsCustomEntries(ByVal overwrite As Boolean) As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseAdditionalColumnsAsCustomEntries = True
            _config.OverwriteExistingCustomEntries = overwrite
            Return _config
        End Function

        Public Property EmptyColumnOption As Mapping.Mapping.OnEmptyColumnOption
            Get
                Return _config.EmptyColumnOption
            End Get
            Set(ByVal value As Mapping.Mapping.OnEmptyColumnOption)
                _config.EmptyColumnOption = value
            End Set
        End Property

    End Class
End Namespace