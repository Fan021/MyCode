Imports System.Windows.Forms
Imports System.Drawing
Public Class ExceptionForm

    Private Sub ExceptionMsg_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Screen.AllScreens.Length > 1 Then
            Me.Location = New Point(0, 0)
            Me.Show()
            Me.Location = New Point(CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)), CInt((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2)))
        Else
            Me.Location = New Point(CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2)), CInt((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2)))
        End If
    End Sub

End Class