Imports Kostal.Las.ArticleProvider.EPT.Mapping

Namespace Ept.Fluent


    ''' <summary>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ReaderConfiguration

        Private ReadOnly _filename As String
        Private ReadOnly _properties As List(Of ConfiguredMapping)
        Private ReadOnly _files As List(Of Mapping.Mapping)
        Private ReadOnly _rowElements As List(Of Mapping.Mapping)
        Private _useUnmappedPropertiesAsCustomEntries As Boolean
        Private _overwriteExistingCustomEntries As Boolean
        Private _emptyEptPropertyOption As Mapping.Mapping.OnEmptyEptPropertyOption = Mapping.Mapping.OnEmptyEptPropertyOption.NullEntry

        ''' <summary>
        ''' Creates a new instance of <see cref="ReaderConfiguration"/>.
        ''' </summary>
        ''' <param name="filename">The xml file to parse</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal filename As String)
            _filename = filename

            _properties = New List(Of ConfiguredMapping)
            _files = New List(Of Mapping.Mapping)
            _rowElements = New List(Of Mapping.Mapping)
        End Sub

        ''' <summary>
        ''' Name of xml file
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Filename() As String
            Get
                Return _filename
            End Get
        End Property

        ''' <summary>
        ''' Dictionary of all properties that are mapped.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Properties As List(Of ConfiguredMapping)
            Get
                Return _properties
            End Get
        End Property

        ''' <summary>
        ''' List of all sequence files that are mapped.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Files As List(Of Mapping.Mapping)
            Get
                Return _files
            End Get
        End Property

        Public ReadOnly Property RowElements As List(Of Mapping.Mapping)
            Get
                Return _rowElements
            End Get
        End Property

        ''' <summary>
        ''' All mappings used within this configuration
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUsedMappings() As IEnumerable(Of Mapping.Mapping)
            Dim list As New List(Of Mapping.Mapping)

            list.AddRange((From item In Properties Select item.Mapping).ToList)
            list.AddRange(_files)

            Return list

        End Function

        ''' <summary>
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub ValidateBasicParameters()

            Dim requiredValues As List(Of String) = New List(Of String)(4)
            requiredValues.Add(Base.ArticleAttribute.ArticleNumber.ToString())
            requiredValues.Add(Base.ArticleAttribute.ArticleIndex.ToString())
            requiredValues.Add(Base.ArticleAttribute.ArticleName.ToString())
            requiredValues.Add(Base.ArticleAttribute.ArticleInfo.ToString())

            For Each rowElement As XmlNodeVariantRowMapping In RowElements.OfType(Of XmlNodeVariantRowMapping)
                requiredValues.Remove(rowElement.ArticleProperty)
            Next

            ' For Each requiredValue As String In requiredValues
            'Dim localValue As String = requiredValue
            'If (From item In Properties Where item.Name = localValue.ToString).Count = 0 Then
            ' Throw New ArgumentException(String.Format("Missing required configuration parameter '{0}'. Please provide static or dynamic mapping.", requiredValue.ToString))
            ' End If
            '  Next

        End Sub

        ''' <summary>
        ''' </summary>
        ''' <value>TRUE if mapped, otherwise FALSE</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UseUnmappedPropertiesAsCustomEntries() As Boolean
            Get
                Return _useUnmappedPropertiesAsCustomEntries
            End Get
            Set(ByVal value As Boolean)
                _useUnmappedPropertiesAsCustomEntries = value
            End Set
        End Property

        Public Property OverwriteExistingCustomEntries() As Boolean
            Get
                Return _overwriteExistingCustomEntries
            End Get
            Set(ByVal value As Boolean)
                _overwriteExistingCustomEntries = value
            End Set
        End Property

        Public Property EmptyEptPropertyOption() As Mapping.Mapping.OnEmptyEptPropertyOption
            Get
                Return _emptyEptPropertyOption
            End Get
            Set(ByVal value As Mapping.Mapping.OnEmptyEptPropertyOption)
                _emptyEptPropertyOption = value
            End Set
        End Property

    End Class

End Namespace