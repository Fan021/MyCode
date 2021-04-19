Imports System.Windows.Forms
Imports Kostal.Las.Base

Public Class ArticleSelectView
    Private cLanguageManager As Language
    Public Event ArticleChanging(sender As Object, e As LasViewEventArgs)

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            cLanguageManager = CType(Devices(Language.Name), Language)
            InitLanugage()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Public Sub InitLanugage()
        lblMessage.Text = cLanguageManager.Read("ArticleSelectView", "lblMessage")
        btnOK.Text = cLanguageManager.Read("ArticleSelectView", "btnOK")
        btnCancel.Text = cLanguageManager.Read("ArticleSelectView", "btnCancel")
        For Each c As DataGridViewColumn In DG_Article.Columns
            c.HeaderText = cLanguageManager.Read("ArticleSelectView", c.Name)
        Next
    End Sub
    Private Sub ArticleSelectView_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBArticle.SizeChanged
        Try
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = CBArticle.Height + 6
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(2).Height = CBArticle.Height + 10
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ArticleSelectView_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Me.CBArticle.Height = panSelect.Height
        '  Me.CBArticle.Width = panSelect.Width - 2.1 * btnCancel.Width

        'Me.CBArticle.Font.Size = panSelect.Height / 45
    End Sub

    Private Sub CBArticle_TextChanged(sender As Object, e As EventArgs) Handles CBArticle.TextChanged

        '    Me.CBArticle.Width = panSelect.Width - 2.1 * btnCancel.Width

    End Sub

    Private Sub CBArticle_VisibleChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        lstMatchBox.Hide()
        RaiseEvent ArticleChanging(Me, New LasViewEventArgs With {.IsMakeSure = True})

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        lstMatchBox.Hide()
        'Me.CBArticle.Text = ""
        RaiseEvent ArticleChanging(Me, New LasViewEventArgs With {.IsMakeSure = False})

    End Sub

    Private Sub lstMatchBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMatchBox.SelectedIndexChanged
        CBArticle.Text = lstMatchBox.SelectedItem.ToString
        lstMatchBox.Hide()
    End Sub

    Private Sub CBArticle_KeyDown(sender As Object, e As KeyEventArgs) Handles CBArticle.KeyDown
        lstMatchBox.Width() = CBArticle.Width
        lstMatchBox.Items.Clear()
        lstMatchBox.Font = CBArticle.Font
        For i = 0 To CBArticle.Items.Count - 1
            If CBArticle.Items(i).IndexOf(CBArticle.Text) >= 0 Then
                lstMatchBox.Items.Add(CBArticle.Items(i).ToString)
            End If
        Next
        lstMatchBox.Show()
    End Sub
End Class


'Public Class ArticleViewEventArgs
'    Inherits EventArgs

'    Private _IsMakeSure As Boolean

'    Public Property IsMakeSure As Boolean
'        Get
'            Return _IsMakeSure
'        End Get
'        Set(value As Boolean)
'            _IsMakeSure = value
'        End Set
'    End Property


'End Class