Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Csv
Imports Kostal.Las.ArticleProvider.Ept
Imports System.Windows.Forms

#Region "ArticleReader"


Public Class ArticleReader
    Inherits ReaderBase
    Protected enumCsvAppendType As enumCsvAppendType
    Protected _ListArticleAndMappingFile As New Dictionary(Of ArticleAndMappingFile, ArticleCollection)

    Public Sub New _
      (ByVal MyParent As Station,
       ByVal _AppSettings As Settings,
       ByVal MyLanguage As Language
      )
        MyBase.New(MyParent, _AppSettings, MyLanguage)
        _mSelection = "Article"
        _ClassName = "ArticleReader"
    End Sub

    Public Overrides Function Init() As Boolean
        For Each mArticleAndMappingFile In AppSettings.ArticleAndMappingFile
            If mArticleAndMappingFile.ArticleFile.ToUpper.IndexOf(".CSV") >= 0 Then
                Dim prov As New Csv
                prov.ReadFile = AppSettings.VarFolder + mArticleAndMappingFile.ArticleFile
                prov.MappingFile = AppSettings.VarFolder + mArticleAndMappingFile.MappingFile
                Dim articles As ArticleCollection = prov.GetArticles
                _ListArticleAndMappingFile.Add(mArticleAndMappingFile, articles)
            End If
            If mArticleAndMappingFile.ArticleFile.ToUpper.IndexOf(".XML") >= 0 Then
                Dim prov As New EptXmlArticleProvider
                prov.ReadFile = AppSettings.VarFolder + mArticleAndMappingFile.ArticleFile
                prov.MappingFile = AppSettings.VarFolder + mArticleAndMappingFile.MappingFile
                Dim articles As ArticleCollection = prov.GetArticles
                _ListArticleAndMappingFile.Add(mArticleAndMappingFile, articles)
            End If
        Next

        If Not Init_Elements() Then Return False

        If enumCsvAppendType = Base.enumCsvAppendType.Append Or enumCsvAppendType = Base.enumCsvAppendType.NONE Then
            For Each articleelement As ArticleCollection In _ListArticleAndMappingFile.Values
                For Each articlevalue As ArticleConfigurationSet In articleelement
                    If _ArticleListElement.ContainsKey(articlevalue.Key) Then
                        Throw New Exception(articlevalue.Key + " have exist")
                    End If
                    If articlevalue.Enabled Then
                        _ArticleListElement.Add(articlevalue.Key, New ArticleListElement(articlevalue.Key, articlevalue.Key, Long.Parse(_ArticleListElement.Count.ToString)))
                    End If
                Next
            Next
        End If

        If enumCsvAppendType = Base.enumCsvAppendType.Merge Then
            For Each articlevalue As ArticleConfigurationSet In _ListArticleAndMappingFile(_ListArticleAndMappingFile.Keys(0))
                If _ArticleListElement.ContainsKey(articlevalue.Key) Then
                    Throw New Exception(articlevalue.Key + " have exist")
                End If
                If articlevalue.Enabled Then
                    _ArticleListElement.Add(articlevalue.Key, New ArticleListElement(articlevalue.Key, articlevalue.Key, Long.Parse(_ArticleListElement.Count.ToString)))
                End If
            Next
        End If
        Return True
    End Function


    Protected Overrides Function Init_Elements() As Boolean
        If Not Check_CsvAppendType() Then Return False
        If Not Check_Elements() Then Return False
        Dim basicNames As List(Of String) = GetFullArticleAttributteList()

        If enumCsvAppendType = Base.enumCsvAppendType.Append Or enumCsvAppendType = Base.enumCsvAppendType.NONE Then
            For Each element As ArticleConfigurationItem In _ListArticleAndMappingFile(_ListArticleAndMappingFile.Keys(0)).ElementAt(0).Items
                If element.Key = ArticleAttribute.ID.ToString Then
                    If basicNames.Contains(element.Key) Then basicNames.Remove(element.Key)
                    AddKey(element.Key, enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE)
                Else
                    If basicNames.Contains(element.Key) Then basicNames.Remove(element.Key)
                    AddKey(element.Key, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML)
                End If
            Next

        Else
            For Each articleCollection As ArticleCollection In _ListArticleAndMappingFile.Values
                For Each element As ArticleConfigurationItem In articleCollection.ElementAt(0).Items
                    If element.Key = ArticleAttribute.ID.ToString Then
                        If basicNames.Contains(element.Key) Then basicNames.Remove(element.Key)
                        AddKey(element.Key, enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE)
                    Else
                        If basicNames.Contains(element.Key) Then basicNames.Remove(element.Key)
                        AddKey(element.Key, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML)
                    End If
                Next
            Next
        End If

        '=====================================================================================================
        AddKey(KostalArticleKeys.KEY_SERIAL_NUMBER, enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL, , , False)
        If basicNames.Contains(KostalArticleKeys.KEY_SERIAL_NUMBER) Then basicNames.Remove(KostalArticleKeys.KEY_SERIAL_NUMBER)
        ' AddKey(KostalArticleKeys.KEY_ALTERNATE_SCHEDULE_ACTIV, enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL, , , False)
        ' If basicNames.Contains(KostalArticleKeys.KEY_ALTERNATE_SCHEDULE_ACTIV) Then basicNames.Remove(KostalArticleKeys.KEY_ALTERNATE_SCHEDULE_ACTIV)

        For Each name As String In basicNames
            If name = ArticleAttribute.ArticleNumber.ToString Or name = ArticleAttribute.ArticleIndex.ToString Or name = ArticleAttribute.ArticleFamily.ToString Or name = ArticleAttribute.CustomerNumber.ToString Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_BASE_ELEMENT_CHECK, name), "ArticleReader.Check_Elements")
            End If
            AddKey(name, enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL, , , False)
        Next



        basicNames.Clear()
        basicNames = Nothing
        Return True
    End Function

    Protected Function Check_Elements() As Boolean
        Dim basicNames As New List(Of String)

        If enumCsvAppendType = Base.enumCsvAppendType.Append Or enumCsvAppendType = Base.enumCsvAppendType.NONE Then
            Return True
        End If

        For Each articleCollection As ArticleCollection In _ListArticleAndMappingFile.Values
            For Each element As ArticleConfigurationItem In articleCollection.ElementAt(0).Items
                If element.Key = ArticleAttribute.ID.ToString Then
                    If Not basicNames.Contains(element.Key) Then
                        basicNames.Add(element.Key)
                    End If
                    Continue For
                End If
                If basicNames.Contains(element.Key) Then
                    _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLEREAD_ELEMENT_CHECK, element.Key), "ArticleReader.Check_Elements")
                End If
                basicNames.Add(element.Key)
            Next
        Next
        Return True
    End Function

    Protected Function Check_CsvAppendType() As Boolean
        Dim basicNames As New List(Of String)

        If _ListArticleAndMappingFile.Count > 1 Then

            For Each articleCollection As ArticleCollection In _ListArticleAndMappingFile.Values
                For Each element As ArticleConfigurationItem In _ListArticleAndMappingFile(_ListArticleAndMappingFile.Keys(0)).ElementAt(0).Items
                    If Not basicNames.Contains(element.Key) Then
                        basicNames.Add(element.Key)
                    End If
                Next

                For Each element As ArticleConfigurationItem In articleCollection.ElementAt(0).Items
                    If basicNames.Contains(element.Key) Then
                        basicNames.Remove(element.Key)
                    End If
                Next

                If basicNames.Count > 0 Then
                    enumCsvAppendType = Base.enumCsvAppendType.Merge
                    Return True
                End If
            Next
            enumCsvAppendType = Base.enumCsvAppendType.Append
            Return True
        End If
        enumCsvAppendType = Base.enumCsvAppendType.NONE
        Return True
    End Function

    Protected Overrides Function GetFullArticleAttributteList() As List(Of String)
        Dim basicArticleKeys As New List(Of String)
        For Each name As String In KostalArticleKeys.GetUserDefinedKeys
            basicArticleKeys.Add(name)
        Next
        Return basicArticleKeys
    End Function

    Public Overrides Function Get_Elements(ByVal ID As String) As Dictionary(Of String, ArticleElement)
        Dim _Element As ArticleElement, Found As Boolean
        Dim article As ArticleConfigurationSet
        _Elements(KostalArticleKeys.KEY_ID).Data = ID

        For Each _Element In _Elements.Values
            If _Element.Source <> enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML Then
                Continue For
            End If
            Found = False
            For Each articleCollection As ArticleCollection In _ListArticleAndMappingFile.Values
                If articleCollection.IsArticleExisting(ID) Then
                    article = articleCollection.GetArticle(ID)
                    For Each element As ArticleConfigurationItem In article.Items
                        If element.Key = _Element.Key Then
                            _Elements(_Element.Key).Data = element.Value
                            Found = True
                            Continue For
                        End If
                    Next
                End If
                If Found Then
                    Continue For
                End If
            Next
            If Not Found Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_GETELEMENT, "FAIL", _Element.Key, _Element.Source.ToString), _ClassName + ".Get_Elements")
                _Element.Data = ""
                Return Nothing
            End If
        Next
        Return _Elements
    End Function

    Public Overrides Function Get_Article_ID_FromIndicatedName(ByVal IndicatedName As String) As String
        For Each _Element In _ArticleListElement.Values
            If _Element.IndicatedName = IndicatedName Then
                Return _Element.ID
            End If
        Next
        _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_GETINDICATE, "FAIL"), _ClassName + ".Get_Article_ID_FromIndicatedName")
        Return String.Empty
    End Function

End Class

#End Region





