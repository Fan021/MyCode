Namespace Ept.Mapping

    ''' <summary>
    ''' References a column in csv file containing a date like ValidFrom or ValidTo.
    ''' <example>
    ''' Mapped field contains:  1998.12.31 13:42
    ''' This mapping returns:   31.12.1998 13:42:00
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XmlNodeDateMapping
        Inherits XmlNodeMapping

        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeDateMapping"/>
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="nodeName">Case sensitive name of the node as defined in xml property node</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nodeName As String)
            Me.New(nodeName, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeDateMapping" />.
        ''' </summary>
        ''' <param name="nodeName">Case sensitive name of the node as defined in xml property node</param>
        ''' <param name="modificationStrategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from xml file</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nodeName As String, ByVal modificationStrategy As StringModification)
            MyBase.New(nodeName, modificationStrategy)
        End Sub

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

            Const dateStringFormat As String = "dd.MM.yyyy hh:mm:ss"
            Const expectedDatePattern As String = "^([0-9]{2}).([0-9]{2}).([0-9]{4})\s([0-9]{2}):([0-9]{2}):([0-9]{2})$"

            Dim csvDateString As String = MyBase.GetValue(xmlPropertyValues).Value

            Dim parsedDateString As String = Date.Parse(csvDateString).ToString(dateStringFormat)

            If Not Text.RegularExpressions.Regex.IsMatch(parsedDateString, expectedDatePattern) Then
                Throw New ArgumentException(String.Format("The value '{0}' is not a valid date. It couldn't be parsed to '{1}'. Matched by pattern '{2}'.", csvDateString, dateStringFormat, expectedDatePattern))
            End If

            Return New MappedValue(StringModifier.Modify(parsedDateString, ModificationStrategy))

        End Function

    End Class

End Namespace