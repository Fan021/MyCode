Namespace Base
    Public Class ArticleChangingEventArgs
        Inherits EventArgs

        Private _cancelPluginReset As Boolean
        Private _cancel As Boolean
        Private ReadOnly _currentArticle As IArticleConfigurationSet
        Private ReadOnly _newArticle As IArticleConfigurationSet
        Private _success As Boolean

        ''' <summary>
        ''' Initializes a new instance of ArticleChangingEventArgs with properties CancelPluginReset and Cancel both set to false.
        ''' The currentArticle property is set to nothing and success is set to true.
        ''' </summary>
        ''' <param name="newArticle">IArticleConfigurationSet representing the article that will be active after changing the article.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal newArticle As IArticleConfigurationSet)

            MyBase.New()
            _newArticle = newArticle

            CancelPluginReset = False
            Cancel = False
            Success = True
        End Sub

        ''' <summary>
        ''' Initializes a new instance of ArticleChangingEventArgs with properties CancelPluginReset and Cancel both set to false.
        ''' Success property is set to true.
        ''' </summary>
        ''' <param name="currentArticle">IArticleConfigurationSet representing the current article that is still active but about to change.</param>
        ''' <param name="newArticle">IArticleConfigurationSet representing the article that will be active after changing the article.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal currentArticle As IArticleConfigurationSet, ByVal newArticle As IArticleConfigurationSet)

            Me.New(newArticle)
            _currentArticle = currentArticle

        End Sub

        ''' <summary>
        ''' Gets or sets a value indicating whether plug-ins should be reset on ongoing article change.
        ''' </summary>
        ''' <value>true if plug-ins will be rese; otherwise, false.</value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CancelPluginReset() As Boolean

            Get
                Return _cancelPluginReset
            End Get
            Set(ByVal value As Boolean)
                _cancelPluginReset = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether the event should be canceled.
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if the event should be canceled; otherwise, false.</returns>
        ''' <remarks></remarks>
        Public Property Cancel() As Boolean
            Get
                Return _cancel
            End Get
            Set(ByVal value As Boolean)
                _cancel = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the current article that is still active but about to change.
        ''' </summary>
        ''' <value></value>
        ''' <returns>An ArticleConfigurationSet representing the active article.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentArticle() As IArticleConfigurationSet
            Get
                Return _currentArticle
            End Get
        End Property

        ''' <summary>
        ''' Gets the new article that will get active when this event is not canceled. 
        ''' </summary>
        ''' <value></value>
        ''' <returns>An ArticleConfigurationSet representing the new article.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property NewArticle() As IArticleConfigurationSet
            Get
                Return _newArticle
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the value indicating if article change should be aborted.
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if abort is requested; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Property Success() As Boolean
            Get
                Return _success
            End Get
            Set(ByVal value As Boolean)
                _success = value
            End Set
        End Property
    End Class
End Namespace

