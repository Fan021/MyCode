Namespace Base
    Public Class ArticleChangedEventArgs
        Inherits EventArgs

        Private ReadOnly _article As IArticleConfigurationSet
        Private _success As Boolean = True

        ''' <summary>
        ''' Initializes a new instance of the ArticleChangedEventArgs class with Success property set to true.
        ''' </summary>
        ''' <param name="article">The article that is active now, represented by IArticleConfigurationSet</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal article As IArticleConfigurationSet)

            _article = article

        End Sub

        ''' <summary>
        ''' Gets the value representing the article that is now active.
        ''' </summary>
        ''' <value></value>
        ''' <returns>An ArticleConfigurationSet representing the active article.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Article() As IArticleConfigurationSet
            Get
                Return _article
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the value representing wether the event was handled successfully
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if no failures occured; otherwise, false</returns>
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