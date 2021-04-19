Namespace Csv.Mapping

    ''' <summary>
    ''' References the column of a csv file that will be used as  of 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KeyColumnMapping
        Inherits Mapping


        Private ReadOnly _reference As ArticleKeyReference
        Private ReadOnly _mapping As Mapping
        Private _modificationStrategy As StringModification
        Private _emptyColumnOption As OnEmptyColumnOption = OnEmptyColumnOption.Unknown
        Private _showInArticleSelector As Boolean = False

        ''' <summary>
        ''' Creates a new instance of <see cref="KeyColumnMapping" /> which uses the first column (<see cref="ArticleKeyReference.FirstColumn"/>) of the csv file
        ''' The <see cref="StringModification.Unmodified"/> strategy is used for <see cref="StringModification"/>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            Me.New(StringModification.Unmodified)
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="KeyColumnMapping" /> which uses the first column (<see cref="ArticleKeyReference.FirstColumn"/>) of the csv file
        ''' </summary>
        ''' <param name="modificationStrategy">The strategy applied to the <see cref="System.String"/> after it has been extracted from csv file.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal modificationStrategy As StringModification)
            _reference = ArticleKeyReference.FirstColumn
            Me.ModificationStrategy = modificationStrategy
        End Sub

        ''' <summary>
        ''' Creates a new instance of <see cref="KeyColumnMapping" /> which uses a custom (<see cref="Mapping"/>) to get a value from csv file.
        ''' </summary>
        ''' <param name="mapping">the <see cref="Mapping"/> used to fetch the key of</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal mapping As Mapping)
            _reference = ArticleKeyReference.SpecificColumn
            _mapping = mapping
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
        ''' The type of this reference
        ''' </summary>
        ''' <returns>the type of this <see cref="KeyColumnMapping"/></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Reference() As ArticleKeyReference
            Get
                Return _reference
            End Get
        End Property

        ''' <summary>
        ''' The mapping assigned to the <see cref="KeyColumnMapping"/>
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Mapping() As Mapping
            Get
                Return _mapping
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
            If Reference = ArticleKeyReference.FirstColumn Then
                Return New MappingValue(StringModifier.Modify(csvRow(0), ModificationStrategy))
            Else
                Return Mapping.GetValue(csvRow)
            End If

        End Function

        Friend Overrides Sub SetupHeaders(ByVal headers() As String)

            If Reference = ArticleKeyReference.FirstColumn Then
                Mapping.SetupHeaders(headers)
            End If

        End Sub

    End Class
End Namespace