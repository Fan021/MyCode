Namespace Base
    ''' <summary>
    ''' Builder to create an ArticleConfigurationSet using simple add routines.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArticleBuilder

        Private ReadOnly _article As ArticleConfigurationSet


        ''' <summary>
        ''' Initializes a new instance of the <see cref="ArticleBuilder"/> class.
        ''' </summary>
        ''' <param name="key">identifier for new article.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal key As String)
            _article = New ArticleConfigurationSet(key)
        End Sub

        ''' <summary>
        ''' Adds a new flowFile to the article.
        ''' </summary>
        ''' <param name="filename">name and path of the flowFile to be added.</param>
        ''' <returns>true if file was added; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function AddFile(ByVal filename As String) As Boolean

            If (From item In _article.ConfigurationFiles Where item.FileName = filename).Any Then
                Return False
            End If

            _article.AddConfigurationFile(New ArticleConfigurationFile(_article, filename))
            Return True
        End Function

        ''' <summary>
        ''' Adds a tag to the article. 
        ''' A tag is a value that is attached to a certain enumeration value of InfoItem enumeration.
        ''' This routine will not overwrite any existing value.
        ''' </summary>
        ''' <param name="type">the name of tag as InfoItem</param>
        ''' <param name="value">the value to be attached to this tag</param>
        ''' <returns>true if value could be added; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function AddTag(ByVal type As ArticleAttribute, ByVal value As String) As Boolean
            Return AddTag(type, value, False)
        End Function

        ''' <summary>
        ''' Adds a tag to the article. 
        ''' A tag is a value that is attached to a certain enumeration value of InfoItem enumeration.
        ''' </summary>
        ''' <param name="type">the name of tag as InfoItem</param>
        ''' <param name="value">the value to be attached to this tag</param>
        ''' <param name="overwrite">if true existing values will be overwritten, if false existing values will be kept.</param>
        ''' <returns>true if value could be added; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function AddTag(ByVal type As ArticleAttribute, ByVal value As String, ByVal overwrite As Boolean) As Boolean

            If _article.ContainsItem(type.ToString) Then
                If overwrite = False Then
                    Return False
                Else
                    _article.ConfigurationItems.Remove((From item In _article.ConfigurationItems Where item.Key = type.ToString).First)
                End If
            End If



            _article.ConfigurationItems.Add(New ArticleConfigurationItem(type.ToString, value, True, _article))
            Return True

        End Function

        ''' <summary>
        ''' Adds a user-specific or custom tag to the article. This tag is a key-value combination where the key must be unique over all tags (InfoItems and custom tags).
        ''' This routine will not overwrite any existing value.
        ''' </summary>
        ''' <param name="key">name of the tag as string</param>
        ''' <param name="value">value to be attached to this tag</param>
        ''' <param name="isShown">if true it will be listed in UI, if false it will be hidden.</param>
        ''' <returns>true if value could be added; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function AddCustomTag(ByVal key As String, ByVal value As String, ByVal isShown As Boolean) As Boolean
            Return AddCustomTag(key, value, isShown, False)
        End Function

        ''' <summary>
        ''' Adds a user-specific or custom tag to the article. This tag is a key-value combination where the key must be unique over all tags (InfoItems and custom tags).
        ''' </summary>
        ''' <param name="key">name of the tag as string</param>
        ''' <param name="value">value to be attached to this tag</param>
        ''' <param name="isShown">if true it will be listed in UI, if false it will be hidden.</param>
        ''' <param name="overwrite">if true existing values will be overwritten, if false existing values will be kept.</param>
        ''' <returns>true if value could be added; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function AddCustomTag(ByVal key As String, ByVal value As String, ByVal isShown As Boolean, ByVal overwrite As Boolean) As Boolean
            Try

                If _article.ContainsItem(key) Then
                    If overwrite = False Then
                        '    _logger.Warn("Duplicate custom key '{0}' within article '{1}'. Second occurence of this key is ignored.", key, _article.Key)
                        Return False
                    Else
                        _article.ConfigurationItems.Remove((From item In _article.ConfigurationItems Where item.Key = key).First)
                    End If
                End If

                _article.ConfigurationItems.Add(New ArticleConfigurationItem(key, value, isShown, _article))
                Return True

            Catch ex As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Gets a value representing the article build by this builder.
        ''' </summary>
        ''' <value></value>
        ''' <returns>the build article as ArticleConfigurationSet</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Article() As ArticleConfigurationSet
            Get
                If IsNothing(_article.Key) Then
                    Throw New ArgumentException("Can´t generate ArticleConfigurationSet without a key.")
                End If
                If _article.Key = "" Then
                    Throw New ArgumentException("Can´t generate ArticleConfigurationSet without a key.")
                End If
                Return _article
            End Get
        End Property

    End Class
End Namespace

