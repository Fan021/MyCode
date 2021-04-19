Namespace Csv.Mapping

    ''' <summary>
    ''' References a column of a csv file
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvColumnMapping
        Inherits Mapping

        Private ReadOnly _columnHeader As String
        Private _columnHeaderIndex As Integer
        Private _modificationStrategy As StringModification
        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnMapping" />.
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String)
            Me.New(columnHeader, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnMapping" />.
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <param name="modificationStrategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from csv file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String, ByVal modificationStrategy As StringModification)
            _columnHeader = columnHeader
            _modificationStrategy = modificationStrategy
        End Sub

        ''' <summary>
        ''' The strategy applied to the <see cref="System.String"/> after it has been extracted from csv.
        ''' This only works when using <see cref="ArticleKeyReference.FirstColumn"/>. Otherwise the strategy of the inserted <see cref="Mapping"/> is used.
        ''' </summary>
        ''' <value>the new strategy</value>
        ''' <returns>current strategy</returns>
        ''' <remarks></remarks>
        Public Property ModificationStrategy() As StringModification
            Get
                Return _modificationStrategy
            End Get
            Set(ByVal value As StringModification)
                _modificationStrategy = value
            End Set
        End Property

        ''' <summary>
        ''' The <see cref="System.String"/> representing the column header of the mapped column in csv file
        ''' </summary>
        ''' <returns>name of mapped column</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ColumnHeader() As String
            Get
                Return _columnHeader
            End Get
        End Property

        Friend Overrides Property EmptyColumnOption() As OnEmptyColumnOption
            Get
                Return _emptyColumnOption
            End Get
            Set(ByVal value As OnEmptyColumnOption)
                _emptyColumnOption = value
            End Set
        End Property

        Friend Overrides Property ShowInArticleSelector() As Boolean
            Get
                Return _showInArticleSelector
            End Get
            Set(ByVal value As Boolean)
                _showInArticleSelector = value
            End Set
        End Property

        Friend Overrides Function GetValue(ByVal csvRow() As String) As MappingValue
            Return New MappingValue(StringModifier.Modify(csvRow(_columnHeaderIndex), ModificationStrategy))
        End Function

        Friend Overrides Sub SetupHeaders(ByVal headers() As String)

            If Not headers.Contains(ColumnHeader) Then
                Throw New ArgumentException("Column header {0} is not present in current file.", ColumnHeader)
            End If

            _columnHeaderIndex = Array.IndexOf(headers, ColumnHeader)

        End Sub


    End Class
End Namespace