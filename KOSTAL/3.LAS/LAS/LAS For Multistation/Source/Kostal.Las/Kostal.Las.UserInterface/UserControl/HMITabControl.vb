Imports System.Windows.Forms
Public Class HMITabControl
    Inherits TabControl
    Dim Timer_Add As New Timer
    Dim Timer_Del As New Timer
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = 78 Then
            Dim ch(256) As Byte
            System.Runtime.InteropServices.Marshal.Copy(m.LParam, ch, 0, 255)
            Dim i As Integer = 0
            For i = 0 To 255
                If ch(i) = 253 Then
                    Exit For
                End If
            Next
            If ch(i + 7) = 1 Then
                Timer_Add.Enabled = True
            End If
            If ch(i + 7) = 255 Then
                Timer_Del.Enabled = True
            End If
        End If

    End Sub
    Private Sub Timer_Add_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer_Add.Enabled = False
        If Me.SelectedIndex < Me.TabPages.Count - 1 Then Me.SelectedIndex = Me.SelectedIndex + 1
    End Sub
    Private Sub Timer_Del_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer_Del.Enabled = False
        If Me.SelectedIndex > 0 Then Me.SelectedIndex = Me.SelectedIndex - 1
    End Sub
End Class
