Namespace Ept.Mapping

    ''' <summary>
    ''' References a column of a csv file
    ''' </summary>
    ''' <remarks></remarks>
    Public Class XmlNodeMapping : Inherits Mapping

        Private ReadOnly _nodeName As String
        Private _nodeNameIndex As Integer
        Private _modificationStrategy As StringModification
        Private _emptyEptPropertyOption As OnEmptyEptPropertyOption = OnEmptyEptPropertyOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeMapping" />.
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <param name="nodeName">Case sensitive name of the node as defined in xml property node</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal nodeName As String)
            Me.New(nodeName, StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="XmlNodeMapping" />.
        ''' </summary>
        ''' <param name="columnHeader">Case sensitive name of the node as defined in xml property node</param>
        ''' <param name="modificationStrategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from xml file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal columnHeader As String, ByVal modificationStrategy As StringModification)
            _nodeName = columnHeader
            _modificationStrategy = modificationStrategy
        End Sub

        ''' <summary>
        ''' The strategy applied to the <see cref="System.String"/> after it has been extracted from xml file.
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
        ''' The <see cref="System.String"/> representing the node name of the mapped node in xml file
        ''' </summary>
        ''' <returns>name of mapped column</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property NodeName() As String
            Get
                Return _nodeName
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
            Return New MappedValue(StringModifier.Modify(xmlPropertyValues(_nodeNameIndex), ModificationStrategy))
        End Function

        Friend Overrides Sub SetupPropertyNames(ByVal xmlPropertyValues As IEnumerable(Of String))

            If Not xmlPropertyValues.Contains(NodeName) Then
                Throw New ArgumentException("Node name {0} is not present in the loaded xml file.", NodeName)
            End If

            _nodeNameIndex = Array.IndexOf(xmlPropertyValues.ToArray, NodeName)

        End Sub

    End Class

End Namespace