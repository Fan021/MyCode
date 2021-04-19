Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Ept.Mapping

Namespace Ept.Fluent


    ''' <summary>
    ''' Represents a temporary step in creating a <see cref="ReaderConfiguration"/>.
    ''' At least file and keycolumn have to be configured yet. Additional steps of fluent configuration are required to complete the configuration.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class IncompleteConfiguration

        Public Const TestmanFlowFilePropertyName As String = "IniFile"
        Public Const TestmanRowElementPropertyName As String = "RowElement"
        Private ReadOnly _config As ReaderConfiguration

        ''' <summary>
        ''' Creates a new instance of <see cref="IncompleteConfiguration"/>
        ''' </summary>
        ''' <param name="config">Incomplete configuration where only file reference is set</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal config As ReaderConfiguration)
            _config = config
        End Sub

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="articleProperty">Mapped article attribute</param>
        ''' <param name="mapping">The <see cref="Mapping"/> used to assign a value</param>
        ''' <returns>Still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomProperty(ByVal articleProperty As ArticleAttribute, ByVal mapping As Mapping.Mapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(articleProperty.ToString, mapping))

            Return Me

        End Function

        ' ''' <summary>
        ' ''' Adds a mapping for a <see cref="KostalArticleAttributes"/> used when configuring <see cref="ArticleConfigurationItem"/> 
        ' ''' </summary>
        ' ''' <param name="articleProperty">Mapped article attribute</param>
        ' ''' <param name="mapping">The <see cref="Mapping"/> used to assign a value</param>
        ' ''' <returns>Still incomplete configuration</returns>
        ' ''' <remarks></remarks>
        Public Function SetTestmanProperty(ByVal articleProperty As ArticleAttribute, ByVal mapping As Mapping.Mapping) As IncompleteConfiguration

            If articleProperty = ArticleAttribute.IniFile Then

                If _config.Files.Contains(mapping) Then
                    Throw New ArgumentException(String.Format("Multiple file mapping definition '{0}'. Exactly one definition is allowed.", mapping.ToString))
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
        ''' <param name="customEntryName">Custom field to be created</param>
        ''' <param name="mapping">The <see cref="Mapping"/> used to assign a value</param>
        ''' <returns>Still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConstantValueMapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">Custom field to be created</param>
        ''' <param name="mapping">The <see cref="ConditionalMappingAnd"/> used to assign a value</param>
        ''' <returns>Still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConditionalMappingAnd) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">Custom field to be created</param>
        ''' <param name="mapping">The <see cref="ConditionalMappingOr"/> used to assign a value</param>
        ''' <returns>Still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As ConditionalMappingOr) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

            Return Me

        End Function

        ''' <summary>
        ''' Adds a mapping for a <see cref="ArticleAttribute"/> used when configuring <see cref="ArticleConfigurationItem"/>
        ''' </summary>
        ''' <param name="customEntryName">Custom field to be created</param>
        ''' <param name="mapping">The <see cref="xmlnodeMapping"/> used to assign a value</param>
        ''' <returns>Still incomplete configuration</returns>
        ''' <remarks></remarks>
        Public Function SetCustomField(ByVal customEntryName As String, ByVal mapping As XmlNodeMapping) As IncompleteConfiguration

            _config.Properties.Add(New ConfiguredMapping(customEntryName, mapping))

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


        Public Function SetTestmanRowElement(mapping As Mapping.Mapping) As IncompleteConfiguration

            If _config.RowElements.Contains(mapping) Then
                Throw New ArgumentException(String.Format("Duplicate RowElement definition '{0}'.", mapping.ToString))
            Else
                _config.RowElements.Add(mapping)
            End If

            Return Me

        End Function

        ''' <summary>
        ''' Sets the configuration to ignore all columns that are not explicitly assigned in the mappings. This is the last step when setting up configuration.
        ''' </summary>
        ''' <returns>Completely configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function IgnoreAdditionalColumns() As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseUnmappedPropertiesAsCustomEntries = False
            Return _config
        End Function

        ''' <summary>
        ''' Sets the configuration to map all columns that are not explicitly assigned in the mappings as . This is the last step when setting up configuration.
        ''' </summary>
        ''' <returns>Completly configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function UseAdditionalColumnsAsCustomEntries() As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseUnmappedPropertiesAsCustomEntries = True
            Return _config
        End Function

        ''' <summary>
        ''' Sets the configuration to map all columns that are not explicitly assigned in the mappings as . This is the last step when setting up configuration.
        ''' </summary>
        ''' <returns>Completly configured and ready to use <see cref="ReaderConfiguration"/></returns>
        ''' <remarks></remarks>
        Public Function UseAdditionalColumnsAsCustomEntries(ByVal overwrite As Boolean) As ReaderConfiguration
            _config.ValidateBasicParameters()
            _config.UseUnmappedPropertiesAsCustomEntries = True
            _config.OverwriteExistingCustomEntries = overwrite
            Return _config
        End Function

        Public Property EmptyEptPropertyOption As Mapping.Mapping.OnEmptyEptPropertyOption
            Get
                Return _config.EmptyEptPropertyOption
            End Get
            Set(ByVal value As Mapping.Mapping.OnEmptyEptPropertyOption)
                _config.EmptyEptPropertyOption = value
            End Set
        End Property

    End Class

End Namespace