Imports System.Collections.ObjectModel
Imports System.Text
Namespace Base
    ''' <summary>
    ''' The complete configuration of a single article 
    ''' </summary>
    ''' <remarks>If it is required to build ArticleConfigurationSet manually use the Las.Framework.Base.ArticleBuilder class.</remarks>
    Public Class ArticleConfigurationSet
        Implements IComparable(Of ArticleConfigurationSet)
        Implements IArticleConfigurationSet

        Private _hibernateId As Integer
        Private _description As String
        Private _configurationItems As IList(Of ArticleConfigurationItem)
        Private ReadOnly _configurationFiles As IList(Of ArticleConfigurationFile)

        ''' <summary>
        ''' Initializes a new instance of ArticleConfigurationSet
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal key As String)
            _configurationItems = New List(Of ArticleConfigurationItem)
            _configurationFiles = New List(Of ArticleConfigurationFile)

            ConfigurationItems.Add(New ArticleConfigurationItem(ArticleAttribute.ID.ToString, key, True, Me))

        End Sub


        ''' <summary>
        ''' Gets a value representing the unique identifier of this configuration set.
        ''' </summary>
        ''' <value></value>
        ''' <returns>key as string</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Key() As String Implements IArticleConfigurationSet.Key
            Get

                Return Item(ArticleAttribute.ID).Value

            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the value describing this ArticleConfigurationSet
        ''' </summary>
        ''' <value>description as string</value>
        ''' <returns>description as string</returns>
        ''' <remarks></remarks>
        Public Property Description() As String Implements IArticleConfigurationSet.Description
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                If _description = value Then
                    Return
                End If
                _description = value
            End Set
        End Property

        ''' <summary>
        ''' Gets a value indicating wether the article is enabled or not
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if enabled; otherwise, false</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Enabled() As Boolean
            Get
                Return CBool(Me.Item(ArticleAttribute.Enabled).Value)
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating the start of validity for this article
        ''' </summary>
        ''' <value></value>
        ''' <returns>the dateTime when this article gets active.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date
            Get
                Try
                    Dim valid As Date = Nothing
                    If DateTime.TryParse(Me(ArticleAttribute.ValidFrom).Value, valid) Then
                        Return valid
                    End If
                Catch ex As Exception
                    Return Nothing
                End Try
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating the end of lifetime for this article
        ''' </summary>
        ''' <value></value>
        ''' <returns>dateTime when this article gets invalid</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date
            Get
                Try
                    Dim valid As Date = Nothing
                    If DateTime.TryParse(Me(ArticleAttribute.ValidTo).Value, valid) Then
                        Return valid
                    End If
                Catch ex As Exception
                    Return Nothing
                End Try
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Returns a value indicating wether this article is valid or not
        ''' </summary>
        ''' <value></value>
        ''' <returns>true if article is valid; otherwise, false.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsValid() As Boolean
            Get
                Dim time As Date = Date.Now

                If Not (Me.ValidFrom = Nothing OrElse Date.Compare(Me.ValidFrom, time) < 0) Then
                    Return False
                End If


                If Not (Me.ValidTo = Nothing OrElse Date.Compare(Me.ValidTo, time) > 0) Then
                    Return False
                End If

                Return True
            End Get
        End Property

        ''' <summary>
        ''' Returns a collection of all configuration items attached to this ArticleConfigurationSet
        ''' </summary>
        ''' <value></value>
        ''' <returns>list of attached ArticleConfigurationItem objects</returns>
        ''' <remarks></remarks>
        Friend Property ConfigurationItems() As IList(Of ArticleConfigurationItem)
            Get
                Return _configurationItems
            End Get
            Set(ByVal value As IList(Of ArticleConfigurationItem))
                _configurationItems = value
            End Set
        End Property

        ''' <summary>
        ''' Returns a readonly collection of all flow files attached to this ArticleConfigurationSet
        ''' </summary>
        ''' <value></value>
        ''' <returns>readonly collection of all attached ArticleConfigurationFile objects.</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ConfigurationFiles() As ReadOnlyCollection(Of ArticleConfigurationFile)
            Get
                Return New ReadOnlyCollection(Of ArticleConfigurationFile)(_configurationFiles)
            End Get
        End Property

        ''' <summary>
        ''' Adds an additional flow file to this ArticleConfigurationSet
        ''' </summary>
        ''' <param name="articleConfigurationFile">flow file represented by ArticleConfigurationFile</param>
        ''' <remarks></remarks>
        Friend Sub AddConfigurationFile(ByVal articleConfigurationFile As ArticleConfigurationFile)
            _configurationFiles.Add(articleConfigurationFile)
        End Sub

        ''' <summary>
        ''' Checks if this article configuration contains a certain tag
        ''' </summary>
        ''' <param name="keyToBeChecked">name of the tag to be checked</param>
        ''' <returns>true if tag exists; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function ContainsItem(ByVal keyToBeChecked As String) As Boolean
            Return (From configurationItem In _configurationItems Where configurationItem.Key = keyToBeChecked).Any()
        End Function



        ''' <summary>
        ''' Returns a readonly collection of all tags currently attached to this article.
        ''' </summary>
        ''' <value></value>
        ''' <returns>readonly collection containing attached ArticleConfigurationItem</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As ReadOnlyCollection(Of ArticleConfigurationItem)
            Get
                Return New ReadOnlyCollection(Of ArticleConfigurationItem)(_configurationItems.ToList())
            End Get
        End Property


        ''' <summary>
        ''' Returns a tag identified by a certain key.
        ''' This will return tags identified by InfoItems as well as custom tags.
        ''' </summary>
        ''' <param name="keyToBeChecked">key identifying the tag to be returned</param>
        ''' <value></value>
        ''' <returns>the tag as ArticleConfigurationItem; nothing if tag doesn't exist.</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal keyToBeChecked As String) As ArticleConfigurationItem
            Get
                Try
                    Dim onlyItemWithKey As ArticleConfigurationItem = _configurationItems.SingleOrDefault(Function(element) element.Key = keyToBeChecked)
                    If onlyItemWithKey Is Nothing Then
                        Return HandleGetItemProblems(keyToBeChecked)
                    End If
                    Return onlyItemWithKey

                Catch ex As Exception
                    Return HandleGetItemProblems(keyToBeChecked)
                End Try
            End Get
        End Property

        Private Function HandleGetItemProblems(ByVal keyToBeChecked As String) As ArticleConfigurationItem

            If keyToBeChecked = ArticleAttribute.ValidFrom.ToString Then
                Dim configItem As New ArticleConfigurationItem(keyToBeChecked, "ARTICLE_DEFAULT_VALIDFROM_DATE", True, Me)
                Me._configurationItems.Add(configItem)
                Return configItem
            ElseIf keyToBeChecked = ArticleAttribute.ValidTo.ToString Then
                Dim configItem As New ArticleConfigurationItem(keyToBeChecked, "ARTICLE_DEFAULT_VALIDTO_DATE", True, Me)
                Me._configurationItems.Add(configItem)
                Return configItem
            ElseIf keyToBeChecked = ArticleAttribute.ID.ToString Then
                Return Nothing
            ElseIf keyToBeChecked = ArticleAttribute.Enabled.ToString Then
                Dim configItem As New ArticleConfigurationItem(keyToBeChecked, True, True, Me)
                Return configItem
            Else
                Dim configItem As New ArticleConfigurationItem(keyToBeChecked, "CORE_ARTICLE_UNDEFINED_VALUE", True, Me)
                Return configItem
            End If
        End Function


        Public Function CompareTo(ByVal other As ArticleConfigurationSet) As Integer Implements System.IComparable(Of ArticleConfigurationSet).CompareTo
            Return System.String.Compare(Me.Key, other.Key, System.StringComparison.Ordinal)
        End Function

        Public Shared Operator =(ByVal article1 As ArticleConfigurationSet, ByVal article2 As ArticleConfigurationSet) As Boolean
            Return article1.Key = article2.Key
        End Operator

        Public Shared Operator <>(ByVal article1 As ArticleConfigurationSet, ByVal article2 As ArticleConfigurationSet) As Boolean
            Return article1.Key <> article2.Key
        End Operator

        Public Shared Operator <(ByVal article1 As ArticleConfigurationSet, ByVal article2 As ArticleConfigurationSet) As Boolean
            Return article1.Key < article2.Key
        End Operator

        Public Shared Operator >(ByVal article1 As ArticleConfigurationSet, ByVal article2 As ArticleConfigurationSet) As Boolean
            Return article1.Key > article2.Key
        End Operator

        ''' <summary>
        ''' Returns the value of a standard tag
        ''' </summary>
        ''' <param name="articleAttribute">the tag to be returned as InfoItem</param>
        ''' <returns>value as String; nothing if tag doesn't exist.</returns>
        ''' <remarks></remarks>
        Public Function GetInfo(ByVal articleAttribute As ArticleAttribute) As String Implements IArticleConfigurationSet.GetInfo

            Return Me.Item(articleAttribute).Value

        End Function

        ''' <summary>
        ''' Returns the value of a tag identified by string id
        ''' </summary>
        ''' <param name="articleAttributeName">the tag to be returned as id string</param>
        ''' <returns>value as String; nothing if tag doesn't exist.</returns>
        ''' <remarks></remarks>
        Public Function GetInfo(ByVal articleAttributeName As String) As String Implements IArticleConfigurationSet.GetInfo

            Return Me.Item(articleAttributeName).Value

        End Function

        Public Overrides Function ToString() As String
            Dim builder As New StringBuilder("Article with key '", 256)
            builder.Append(Key)
            builder.Append("'")
            If Not String.IsNullOrEmpty(Me.Item(ArticleAttribute.ArticleName).Value) Then
                builder.Append(" and Name '")
                builder.Append(Me.Item(ArticleAttribute.ArticleName).Value)
                builder.Append("'")
            End If
            If Not IsNothing(Description) Then
                builder.Append(" (")
                builder.Append(Me.Description)
                builder.Append(")")
            End If
            Return builder.ToString
        End Function

        ''' <summary>
        ''' Checks if this article configuration contains a certain tag
        ''' </summary>
        ''' <param name="articleAttribute">name of the tag to be checked as enumeration value of InfoItem</param>
        ''' <returns>true if tag exists; otherwise, false</returns>
        ''' <remarks></remarks>
        Public Function ContainsItem(ByVal articleAttribute As ArticleAttribute) As Boolean
            Return ContainsItem(articleAttribute.ToString)
        End Function

        ''' <summary>
        ''' Returns a standard tag identified by a InfoItem value.
        ''' </summary>
        ''' <param name="articleAttribute">InfoItem value identifying the tag to be returned</param>
        ''' <value></value>
        ''' <returns>the tag as ArticleConfigurationItem; nothing if tag doesn't exist.</returns>
        ''' <remarks></remarks>
        Default Public ReadOnly Property Item(ByVal articleAttribute As ArticleAttribute) As ArticleConfigurationItem
            Get
                Return Item(articleAttribute.ToString)
            End Get
        End Property

    End Class
End Namespace


