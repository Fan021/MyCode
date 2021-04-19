Imports Kostal.Las.Base
Imports Kostal.Las.UserInterface
Public Class LanguageSelectView
    Implements IViewDefine

    Public Event LanguageChanging(sender As Object, e As LasViewEventArgs)

    Public ReadOnly Property GetPannel As Panel Implements IViewDefine.GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property

    Private Sub LanguageSelectView_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub

    Private Sub LanguageSelectView_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'Me.CBLanguage.Height = panSelect.Height
        '  Me.CBLanguage.Width = panSelect.Width - 2.1 * btnCancel.Width

        'Me.CBLanguage.Font.Size = panSelect.Height / 45
    End Sub

    Private Sub CBLanguage_TextChanged(sender As Object, e As EventArgs)

        ' Me.CBLanguage.Width = panSelect.Width - 2.1 * btnCancel.Width

    End Sub

    Private Sub CBLanguage_VisibleChanged(sender As Object, e As EventArgs)

    End Sub
    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBLanguage.SizeChanged
        Try
            TableLayoutPanel1.RowStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(1).Height = CBLanguage.Height + 6
            TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
            TableLayoutPanel1.RowStyles(2).Height = CBLanguage.Height + 6
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        RaiseEvent LanguageChanging(Me, New LasViewEventArgs With {.IsMakeSure = True})

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        'Me.CBLanguage.Text = ""
        RaiseEvent LanguageChanging(Me, New LasViewEventArgs With {.IsMakeSure = False})

    End Sub

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.Dispose()
        Return True
    End Function
End Class


'Public Class LanguageViewEventArgs
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