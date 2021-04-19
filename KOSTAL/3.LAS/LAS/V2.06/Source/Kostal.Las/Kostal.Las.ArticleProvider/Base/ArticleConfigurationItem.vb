Namespace Base
    ''' <summary>
    ''' A configuration item that can be used for article configuration
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArticleConfigurationItem

        Private _hibernateId As Integer
        Private ReadOnly _key As String
        Private ReadOnly _value As String
        Private ReadOnly _show As Boolean
        Private ReadOnly _parent As ArticleConfigurationSet

        ''' <summary>
        ''' Initializes a new instance of the ArticleConfigurationItem class.
        ''' </summary>
        ''' <param name="key">identifier for this item.</param>
        ''' <param name="value">the value of this item.</param>
        ''' <param name="show">true if value should be shown in article selector.</param>
        ''' <param name="parent">reference to the parent article.</param>
        Public Sub New(ByVal key As String, ByVal value As String, ByVal show As Boolean, ByVal parent As ArticleConfigurationSet)
            _key = key
            _value = value
            _show = show
            _parent = parent
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the ArticleConfigurationItem class.
        ''' All properties of this class are still null.
        ''' </summary>
        Friend Sub New()
        End Sub

        ''' <summary>
        ''' Gets a value representing the identifier of this ArticleConfigurationItem
        ''' </summary>
        ''' <value></value>
        ''' <returns>key as string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Key() As String
            Get
                Return _key
            End Get
        End Property

        ''' <summary>
        ''' Gets a value representing the value of this item
        ''' </summary>
        ''' <value></value>
        ''' <returns>value as string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Value() As String
            Get
                Return _value
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating wether this item will be shown in UI or not
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if it is shown; otherwise, false</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Show() As Boolean
            Get
                Return _show
            End Get
        End Property

        ''' <summary>
        ''' Gets the value representing the article this configuration element is attached to.
        ''' </summary>
        ''' <value>Parent article as ArticleConfigurationSet</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Parent() As ArticleConfigurationSet
            Get
                Return _parent
            End Get
        End Property

    End Class
End Namespace


