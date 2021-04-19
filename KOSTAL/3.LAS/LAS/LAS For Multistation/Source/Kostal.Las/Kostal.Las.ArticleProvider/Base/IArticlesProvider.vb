Namespace Base
    ''' <summary>
    ''' Represents an ArticleProvider.
    ''' An ArticleProvider is class that can load articles from a source and provides them within an ArticleCollection 
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IArticlesProvider

        ''' <summary>
        ''' Gets the available articles in a ArticleCollection.
        ''' </summary>
        ''' <returns>Available articles as ArticleCollection.</returns>
        ''' <remarks></remarks>
        Property ReadFile As String
        Property MappingFile As String
        Function GetArticles() As ArticleCollection

    End Interface
End Namespace


