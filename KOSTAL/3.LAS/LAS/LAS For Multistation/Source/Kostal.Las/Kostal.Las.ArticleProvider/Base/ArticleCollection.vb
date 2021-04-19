Namespace Base
    ''' <summary>
    ''' Container for articles used by testman framework
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArticleCollection
        Implements IEnumerable(Of ArticleConfigurationSet)

        Private ReadOnly _dictionary As Dictionary(Of String, ArticleConfigurationSet)


        Public Sub New()
            _dictionary = New Dictionary(Of String, ArticleConfigurationSet)
        End Sub

        Public Function AddRange(ByVal items As IEnumerable(Of ArticleConfigurationSet), ByRef invalidArticleCount As Integer) As Integer
            Try
                Dim c As Integer = 0
                invalidArticleCount = 0

                For Each item As ArticleConfigurationSet In items
                    c += 1
                    If item.IsValid = False Then
                        invalidArticleCount += 1
                    End If

                    Add(item)
                Next
                Return c
            Catch ex As Exception
                Throw New Exception("Unvalid IEnumerable(Of ArticleConfigurationSet)", ex)
            End Try

        End Function

        Public Sub Add(ByVal article As ArticleConfigurationSet)
            Try
                _dictionary.Add(article.Key, article)
            Catch ex As Exception
                Throw New Exception("Undefined article", ex)
            End Try
        End Sub

        ''' <summary>
        ''' Checks if a article with provided key is registered in this collection
        ''' </summary>
        ''' <param name="key">article key as string</param>
        ''' <returns>true if article is existing; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function IsArticleExisting(ByVal key As String) As Boolean
            Try
                Return _dictionary.ContainsKey(key)
            Catch ex As Exception
                Throw New Exception("Undefined article key.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Returns the instance of an article that is represented by a key
        ''' </summary>
        ''' <param name="key">key of article to be returned</param>
        ''' <returns>the article as ArticleConfigurationSet</returns>
        ''' <remarks></remarks>
        Public Function GetArticle(ByVal key As String) As ArticleConfigurationSet
            Try
                Return _dictionary.Item(key)
            Catch ex As Exception
                Throw New Exception("Undefined article key.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Removes article registered by provided key from this collection
        ''' </summary>
        ''' <param name="key">key of article to be removed as string.</param>
        ''' <remarks></remarks>
        Public Sub Remove(ByVal key As String)
            Try
                _dictionary.Remove(key)
            Catch ex As Exception
                Throw New Exception("Undefined article key.", ex)
            End Try
        End Sub

        ''' <summary>
        ''' Removes all currently registered articles from this collection.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub RemoveAll()
            _dictionary.Clear()
        End Sub



        Public Function GetInfo(ByVal key As String) As Object
            Try
                Dim value As ArticleConfigurationSet = Nothing
                _dictionary.TryGetValue(key, value)
                Return value
            Catch ex As Exception
                Throw New Exception("Undefined article key.", ex)
            End Try
        End Function

        Public Function GetInfo(Of T As Class)(ByVal key As String) As T
            Dim dict As Object = GetInfo(key)
            Try
                Return TryCast(dict, T)
            Catch ex As InvalidCastException
                Throw New Exception(String.Format("Error fetching key '{0}' from ArticleCollection.", key), ex)
                Return Nothing
            End Try
        End Function

        Public Function GetInfos() As IEnumerable(Of ArticleConfigurationSet)
            Return _dictionary.Values.ToArray
        End Function

        Public Sub SetInfo(ByVal key As String, ByVal data As Object)
            Try
                Dim article As ArticleConfigurationSet = CType(data, ArticleConfigurationSet)
                _dictionary.Add(key, article)
            Catch ex As Exception
                Throw New Exception(String.Format("Duplicate or unvalid article key: {0}.", key), ex)
            End Try
        End Sub

        Public Sub SetInfo(ByVal key As String, ByVal articleInfo As ArticleConfigurationSet)
            Try
                _dictionary.Add(key, articleInfo)
            Catch ex As Exception
                Throw New Exception(String.Format("Duplicate or unvalid article key: {0}.", key), ex)
            End Try
        End Sub

        'Public Sub SetArticle(ByVal article As ArticleConfigurationSet)
        '  Add(article)
        'End Sub

        Public Function GetEnumerator() As IEnumerator(Of ArticleConfigurationSet) Implements IEnumerable(Of ArticleConfigurationSet).GetEnumerator
            Return _dictionary.Values.GetEnumerator
        End Function

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function


        Public Overrides Function ToString() As String
            Return String.Format("Articlecollection containing {0} items.", Count)
        End Function

    End Class

End Namespace


