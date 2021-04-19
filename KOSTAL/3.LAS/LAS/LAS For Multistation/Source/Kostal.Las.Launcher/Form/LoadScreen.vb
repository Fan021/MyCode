Imports Kostal.Las.Base
Imports Kostal.Las.ArticleProvider
Public Class LoadScreen
    Private _xmlHandler As New XmlHandler

    Private Sub LoadScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sResult As String
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = CInt((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2))
        sResult = _xmlHandler.GetSectionInformation(Main.AppSettings.ApplicationFolder, Main.AppSettings.RootIniName, "Environment", "Screen")

        If IsNumeric(sResult) Then
            Me.Left = (CInt(sResult) * My.Computer.Screen.WorkingArea.Width) + CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
        Else
            Me.Left = CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
        End If

        Me.PleaseWait.Text = "The " & My.Application.Info.AssemblyName & " is loading..."
        Me.Timer1.Interval = 50
        Me.Timer1.Enabled = True
    End Sub

    Private Sub ArticleCounter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.ProgressBar1.Value < Me.ProgressBar1.Maximum Then
            Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            Me.ProgressBar1.Value = Me.ProgressBar1.Minimum
        End If
    End Sub
End Class