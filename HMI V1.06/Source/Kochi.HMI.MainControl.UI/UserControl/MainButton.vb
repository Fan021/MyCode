Imports System.Windows.Forms
Imports System.Drawing

Public Class MainButton
    Public ReadOnly Property MainButton As Button
        Get
            Return Button_Name
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
        Button_Name.Text = strText
        Button_Name.Name = strName
        Return True
    End Function


    Private Sub Panel_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Bottom.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Bottom), 1, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Bottom), 1, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Bottom), 1, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Bottom), 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Button_Name_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Button_Name.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Button).ClientRectangle,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button), 2, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button), 2, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button), 2, ButtonBorderStyle.Solid,
                          ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button), 2, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Button_Name_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseEnter
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
    End Sub

    Private Sub Button_Name_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseLeave
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Level)
    End Sub

End Class

