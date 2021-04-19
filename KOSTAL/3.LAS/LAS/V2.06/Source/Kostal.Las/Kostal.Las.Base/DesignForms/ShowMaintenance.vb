Imports System.Drawing

Public Class ShowMaintenance
    Private AppSettings As New Settings
    Private _Language As Language
    Private Sub ShowMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Function Init(ByVal MyParent As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        AppSettings = _AppSettings
        _Language = MyLanguage
        TextBox_Msg.Font = New Font("Calibri", 18)
        Button_Confirm.Font = New Font("Calibri", 18)
        Button_Confirm.Text = _Language.Read("ShowMaintenance", "Button_Confirm")
        Return True
    End Function

    Private Sub Button_Confirm_Click(sender As Object, e As EventArgs) Handles Button_Confirm.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class