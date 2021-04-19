Imports System.Windows.Forms
Imports System.Drawing

Public Class TextBoxEx
    Inherits TextBox
    Public Sub New()
        MyBase.New()
        Me.BackColor = Drawing.Color.White
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        ' If m.Msg = &HF Or m.Msg = &H133 Then
        '    System.Windows.Forms.ControlPaint.DrawBorder(Me.CreateGraphics(), New System.Drawing.Rectangle(0, 0, Me.Width, Me.Height), System.Drawing.SystemColors.ControlDark, ButtonBorderStyle.Solid)
        ' End If
    End Sub
End Class
