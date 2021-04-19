Namespace Csv.Mapping

    ''' <summary>
    ''' References a column in csv file containing a material number including index.
    ''' This mapping only maps the index/revision without the article number in front.
    ''' <example>
    ''' Mapped field contains:  1234567890123-00
    ''' This mapping returns:   00
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvColumnArticleIndexMapping
        Inherits CsvColumnMapping

        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnArticleIndexMapping"/>
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String)
            Me.New(columnHeader, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnArticleIndexMapping" />.
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <param name="modificationStrategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from csv file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String, ByVal modificationStrategy As StringModification)
            MyBase.New(columnHeader, modificationStrategy)
        End Sub

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
            Dim completeNumber As String = MyBase.GetValue(csvRow).Value

            If Not completeNumber.Contains("-"c) Then
                Throw New ArgumentException(String.Format("The value '{0}' is not a valid material number with revision (index).", completeNumber))
            End If

            Dim extractedString As String = completeNumber.Split("-"c)(1)
            Return New MappingValue(StringModifier.Modify(extractedString, ModificationStrategy))

        End Function

    End Class
End Namespace