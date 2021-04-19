Namespace Csv.Mapping

    ''' <summary>
    ''' References a column in csv file containing a date like ValidFrom or ValidTo.
    ''' <example>
    ''' Mapped field contains:  1998.12.31 13:42
    ''' This mapping returns:   31.12.1998 13:42:00
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvColumnDateMapping
        Inherits CsvColumnMapping

        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnDateMapping"/>
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String)
            Me.New(columnHeader, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnDateMapping" />.
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

            Const dateStringFormat As String = "dd.MM.yyyy hh:mm:ss"
            Const expectedDatePattern As String = "^([0-9]{2}).([0-9]{2}).([0-9]{4})\s([0-9]{2}):([0-9]{2}):([0-9]{2})$"

            Dim csvDateString As String = MyBase.GetValue(csvRow).Value

            Dim parsedDateString As String = Date.Parse(csvDateString).ToString(dateStringFormat)

            If Not Text.RegularExpressions.Regex.IsMatch(parsedDateString, expectedDatePattern) Then
                Throw New ArgumentException(String.Format("The value '{0}' is not a valid date. It couldn't be parsed to '{1}'. Matched by pattern '{2}'.", csvDateString, dateStringFormat, expectedDatePattern))
            End If

            Return New MappingValue(StringModifier.Modify(parsedDateString, ModificationStrategy))

        End Function

    End Class
End Namespace