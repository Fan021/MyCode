Imports System.ComponentModel
Public Class ArticleUI
    Implements IArticleUI


    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IArticleUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IArticleUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IArticleUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IArticleUI.DataList
        Get
            Return DG_Article
        End Get
    End Property

    Public Function AddRow(ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String) As Boolean
        If DG_Article.Rows.Count >= 20 Then
            DG_Article.Rows.RemoveAt(DG_Article.Rows.Count - 1)
        End If
        DG_Article.Rows.Add()
        DG_Article.Rows(DG_Article.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Article.Rows(DG_Article.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Article.Rows(DG_Article.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Article.Rows(DG_Article.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Article.Sort(DG_Article.Columns("Time"), ListSortDirection.Descending)
        Return True
    End Function

    Public Function AddColumns() As Boolean
        DG_Article.Columns.Clear()
        DG_Article.Columns.Add("Article", "Article")
        DG_Article.Columns.Add("Customer", "Customer")
        DG_Article.Columns.Add("ProductFamily", "ProductFamily")
        DG_Article.Columns.Add("Time", "Time")
        DG_Article.Columns("Article").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DG_Article.Columns("Customer").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DG_Article.Columns("ProductFamily").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DG_Article.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

        Return True
    End Function

End Class