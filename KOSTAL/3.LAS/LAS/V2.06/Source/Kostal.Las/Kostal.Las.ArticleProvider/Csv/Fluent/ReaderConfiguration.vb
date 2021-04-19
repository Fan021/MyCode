Imports Kostal.Las.ArticleProvider.Csv.Mapping
Namespace Csv
    ''' <summary>
    ''' Complete configuration defining how a specified csv file will be parsed into 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ReaderConfiguration

        Private ReadOnly _filename As String
        Private ReadOnly _properties As List(Of Mapping.ConfiguredMapping)
        Private ReadOnly _files As New List(Of Mapping.Mapping)
        Private _keyColumn As Mapping.KeyColumnMapping
        Private _useAdditionalColumnsAsCustomEntries As Boolean
        Private _overwriteExistingCustomEntries As Boolean
        Private _emptyColumnOption As Mapping.Mapping.OnEmptyColumnOption = Mapping.Mapping.OnEmptyColumnOption.NullEntry

        ''' <summary>
        ''' Creates a new instance of <see cref="ReaderConfiguration"/>.
        ''' </summary>
        ''' <param name="filename">the csv file to parse</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal filename As String)
            _filename = filename

            _properties = New List(Of Mapping.ConfiguredMapping)
            _files = New List(Of Mapping.Mapping)
        End Sub

        ''' <summary>
        ''' Name of csv file to parse.
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
        Public ReadOnly Property Properties() As List(Of ConfiguredMapping)
            Get
                Return _properties
            End Get
        End Property

        ''' <summary>
        ''' List of all files that are mapped.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Files() As List(Of Mapping.Mapping)
            Get
                Return _files
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
            If KeyColumn.Reference = ArticleKeyReference.SpecificColumn Then
                list.Add(KeyColumn.Mapping)
            End If

            Return list

        End Function

        ''' <summary>
        ''' Checks the configuration of completeness.
        ''' If a parameter required for a  is missing an exception is raised
        ''' </summary>
        ''' <remarks></remarks>
        ''' <exception cref="ArgumentException">Raised if one of the required parameters describing a is missing.</exception>
        Public Sub ValidateBasicParameters()

            '  Dim requiredValues As String() = {ArticleAttribute.ArticleNumber.ToString}
            Dim requiredValues As String() = {}
            For Each requiredValue In requiredValues
                Dim localValue As String = requiredValue
                If (From item In Properties Where item.Name = localValue.ToString).Count = 0 Then
                    Throw New ArgumentException(String.Format("Missing required configuration parameter '{0}'. Please provide static or dynamic mapping.", requiredValue.ToString))
                End If
            Next

            '  If Files.Count = 0 Then
            ' Throw New ArgumentException("Configuration must have at least one flow file configured.")
            '  End If

        End Sub

        ''' <summary>
        ''' Mapping used as key when creating a new 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property KeyColumn() As KeyColumnMapping
            Get
                Return _keyColumn
            End Get
            Set(ByVal value As KeyColumnMapping)
                _keyColumn = value
            End Set
        End Property

        ''' <summary>
        ''' Indicates wehter columns in the csv file that are not explicitly 
        ''' mapped to a property of
        ''' </summary>
        ''' <value>true is mapped, false if thrown away</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property UseAdditionalColumnsAsCustomEntries() As Boolean
            Get
                Return _useAdditionalColumnsAsCustomEntries
            End Get
            Set(ByVal value As Boolean)
                _useAdditionalColumnsAsCustomEntries = value
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

        Public Property EmptyColumnOption() As Mapping.Mapping.OnEmptyColumnOption
            Get
                Return _emptyColumnOption
            End Get
            Set(ByVal value As Mapping.Mapping.OnEmptyColumnOption)
                _emptyColumnOption = value
            End Set
        End Property

    End Class
End Namespace
