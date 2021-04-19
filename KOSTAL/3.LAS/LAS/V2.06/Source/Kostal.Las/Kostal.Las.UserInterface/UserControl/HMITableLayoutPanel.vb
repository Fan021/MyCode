Imports System.Drawing
Imports System.Windows.Forms
Public Class HMITableLayoutPanel
    Inherits System.Windows.Forms.TableLayoutPanel

    Sub Ini()
        Me.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic).SetValue(Me, True, Nothing)
    End Sub
    Protected Overrides Sub OnCellPaint(ByVal e As System.Windows.Forms.TableLayoutCellPaintEventArgs)
        MyBase.OnCellPaint(e)
        Dim pp = New Pen(System.Drawing.SystemColors.GradientInactiveCaption)
        e.Graphics.DrawRectangle(pp, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.X + Me.Width - 1, e.CellBounds.Y + Me.Height - 1)

    End Sub

End Class
