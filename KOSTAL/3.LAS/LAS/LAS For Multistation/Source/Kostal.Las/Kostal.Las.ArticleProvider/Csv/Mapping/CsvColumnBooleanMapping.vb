Namespace Csv.Mapping

    ''' <summary>
    ''' References a column in csv file containing a data representing a <see cref="Boolean"/>
    ''' <example>
    ''' Mapped field can contain:  +,-,true,false,yes,no,...
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CsvColumnBooleanMapping
        Inherits CsvColumnMapping

        Private ReadOnly _trueDescription As BooleanValueDescription
        Private ReadOnly _falseDescription As BooleanValueDescription
        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="CsvColumnBooleanMapping"/>
        ''' </summary>
        ''' <param name="columnHeader">name of the column as defined in csv header line. This is case sensitive</param>
        ''' <param name="trueDescription">A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> true</param>
        ''' <param name="falseDescription">A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> false</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String, ByVal trueDescription As BooleanValueDescription, ByVal falseDescription As BooleanValueDescription)
            MyBase.New(columnHeader)
            _trueDescription = trueDescription
            _falseDescription = falseDescription
        End Sub

        ''' <summary>
        ''' A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> true
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TrueDescription() As BooleanValueDescription
            Get
                Return _trueDescription
            End Get
        End Property

        ''' <summary>
        ''' A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> false
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FalseDescription() As BooleanValueDescription
            Get
                Return _falseDescription
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

            Dim extractedValue = MyBase.GetValue(csvRow).Value

            Return New MappingValue(BooleanValueMatcher.GetValue(extractedValue, TrueDescription, FalseDescription).ToString())

        End Function

    End Class
End Namespace