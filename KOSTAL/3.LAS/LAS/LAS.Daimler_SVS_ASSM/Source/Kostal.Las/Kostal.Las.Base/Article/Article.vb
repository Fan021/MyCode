
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

Public Class Article
    Inherits ArticleBase
    Public Const Name As String = "_mAppArticle"
    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language, Optional ByVal ComboBox As ComboBox = Nothing)
        MyBase.New(MyStation, _AppSettings, MyLanguage, New ArticleReader(MyStation, _AppSettings, MyLanguage), ComboBox)
        _ClassName = "Article"
    End Sub

End Class
