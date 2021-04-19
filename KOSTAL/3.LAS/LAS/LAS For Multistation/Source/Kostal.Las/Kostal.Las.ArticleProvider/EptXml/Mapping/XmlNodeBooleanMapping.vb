Namespace Ept.Mapping

    ''' <summary>
    ''' References a column in csv file containing a data representing a <see cref="Boolean"/>
    ''' <example>
    ''' Mapped field can contain:  +,-,true,false,yes,no,...
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XmlNodeBooleanMapping
        Inherits XmlNodeMapping

        Private ReadOnly _trueDescription As BooleanValueDescription
        Private ReadOnly _falseDescription As BooleanValueDescription
        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeBooleanMapping"/>
        ''' </summary>
        ''' <param name="nodeName">Case sensitive name of the node as defined in xml property node</param>
        ''' <param name="trueDescription">A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> true</param>
        ''' <param name="falseDescription">A description of the <see cref="System.String"/> representing a <see cref="Boolean"/> false</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nodeName As String, ByVal trueDescription As BooleanValueDescription, ByVal falseDescription As BooleanValueDescription)
            MyBase.New(nodeName)

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

        Friend Overrides Property EmptyEptPropertyOption() As OnEmptyEptPropertyOption
            Get
                Return _emptyEptPropertyOption
            End Get
            Set(ByVal value As OnEmptyEptPropertyOption)
                _emptyEptPropertyOption = value
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

        Friend Overrides Function GetValue(ByVal xmlPropertyValues As IEnumerable(Of String)) As MappedValue

            Dim extractedValue As String = MyBase.GetValue(xmlPropertyValues).Value

            Return New MappedValue(BooleanValueMatcher.GetValue(extractedValue, TrueDescription, FalseDescription).ToString())

        End Function

    End Class
End Namespace