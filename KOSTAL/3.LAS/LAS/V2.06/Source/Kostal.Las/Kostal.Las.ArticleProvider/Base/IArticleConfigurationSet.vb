Namespace Base
    Public Interface IArticleConfigurationSet

        ReadOnly Property Key() As String
        Property Description() As String
        Function GetInfo(ByVal item As ArticleAttribute) As String
        Function GetInfo(ByVal item As String) As String
    End Interface
End Namespace


