
Namespace Ept.Mapping

    ''' <summary>
    ''' References a column of a csv file
    ''' </summary>
    ''' <remarks></remarks>
    <System.Diagnostics.DebuggerDisplay("XmlNodeVariantRowMapping: ArticlePropertyName: {ArticleProperty},  Index: {Index}, Delimiter: {Delimiter}")>
    Public Class XmlNodeVariantRowMapping
        Inherits Mapping

        Private ReadOnly _articleProperty As String
        Private ReadOnly _delimiter As String
        Private ReadOnly _index As Integer
        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeVariantRowMapping" />.
        ''' </summary>
        ''' <param name="articlePropertyName"></param>
        ''' <param name="delimiter"></param>
        ''' <param name="index"></param>
        ''' <remarks></remarks>
        Public Sub New(articlePropertyName As String, delimiter As String, index As Integer)
            _articleProperty = articlePropertyName
            _delimiter = delimiter
            _index = index
        End Sub

        ''' <summary>
        ''' The <see cref="System.String"/> representing the article property name
        ''' </summary>
        ''' <returns>name of mapped column</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ArticleProperty As String
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _articleProperty
            End Get
        End Property

        Public ReadOnly Property Delimiter As String
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _delimiter
            End Get
        End Property

        Public ReadOnly Property Index As Integer
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _index
            End Get
        End Property

        Friend Overrides Property EmptyEptPropertyOption() As OnEmptyEptPropertyOption
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _emptyEptPropertyOption
            End Get
            Set(ByVal value As OnEmptyEptPropertyOption)
                _emptyEptPropertyOption = value
            End Set
        End Property

        Friend Overrides Property ShowInArticleSelector() As Boolean
            <System.Diagnostics.DebuggerStepThrough()>
            Get
                Return _showInArticleSelector
            End Get
            Set(ByVal value As Boolean)
                _showInArticleSelector = value
            End Set
        End Property

        Friend Function GetRowValue(articleKey As String) As MappedValue
            Dim parts As String() = articleKey.Split(New String() {_delimiter}, StringSplitOptions.None)
            If _index < parts.Count() Then
                Return New MappedValue(parts(_index))
            End If
            Throw New ArgumentOutOfRangeException("Index", String.Format("Index {0} of TestmanRowElementsMapping for article parameter '{1}' is out of range for '{2}' split with delimiter '{3}'", _index, ArticleProperty, articleKey, _delimiter))
        End Function

        Friend Overrides Function GetValue(ByVal xmlPropertyValues As IEnumerable(Of String)) As MappedValue
            Return Nothing
        End Function

        Friend Overrides Sub SetupPropertyNames(ByVal xmlPropertyValues As IEnumerable(Of String))

        End Sub

    End Class

End Namespace