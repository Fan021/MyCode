Namespace Ept.Mapping

    ''' <summary>
    ''' References a property in xml file containing a material number including index. This mapp the raw index only.
    ''' <example>
    ''' Mapped field contains:  1234567890123-00
    ''' This mapping returns:   00
    ''' </example>
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XmlNodeMaterialRevisionMapping : Inherits XmlNodeMapping

        Public Const MaterialAndRevisionDelimiter As Char = "-"c

        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeMaterialRevisionMapping"/>
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="nodeName">Case sensitive name of the node as defined in xml property node</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nodeName As String)
            Me.New(nodeName, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeMaterialRevisionMapping" />.
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
            Dim completeNumber As String = MyBase.GetValue(xmlPropertyValues).Value

            If Not completeNumber.Contains(MaterialAndRevisionDelimiter) Then
                Throw New ArgumentException(String.Format("The value '{0}' is not a valid material number with revision.", completeNumber))
            End If

            Dim extractedString As String = completeNumber.Split(MaterialAndRevisionDelimiter)(1)
            Return New MappedValue(StringModifier.Modify(extractedString, ModificationStrategy))

        End Function

    End Class

End Namespace