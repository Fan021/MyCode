Imports System.Windows.Forms
Imports System.Drawing
Imports Kochi.HMI.MainControl.UI

Public Class HMIPassFailButton
    Inherits Button
    Private eGapFillerButtonValue As enumGapFillerButtonValue

    Private bOldIndicateValue As Boolean = False

    Public Function SetIndicateColor(ByVal bValue As Boolean) As Boolean
        If bOldIndicateValue = bValue Then Return True
        If bValue Then
            BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            BackColor = System.Drawing.SystemColors.Control
        End If
        bOldIndicateValue = bValue
        Return True
    End Function

    Public Function SetIndicateColor(ByVal bPass As Boolean, ByVal bFail As Boolean) As Boolean
        If bPass Then
            eGapFillerButtonValue = enumGapFillerButtonValue.PASS
        End If
        If bFail Then
            eGapFillerButtonValue = enumGapFillerButtonValue.FAIL
        End If
        If Not bPass And Not bFail Then
            eGapFillerButtonValue = enumGapFillerButtonValue.NONE
        End If
        Refresh()
        Return True
    End Function

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        If bOldIndicateValue Then
            BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            BackColor = System.Drawing.SystemColors.Control
        End If
        FlatAppearance.MouseOverBackColor = BackColor
        FlatAppearance.MouseDownBackColor = BackColor
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        If bOldIndicateValue Then
            BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            BackColor = Color.Transparent
        End If
        FlatAppearance.MouseOverBackColor = BackColor
        FlatAppearance.MouseDownBackColor = BackColor
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(pevent)
        Dim brush As System.Drawing.Brush
        Select Case eGapFillerButtonValue
            Case enumGapFillerButtonValue.NONE
                brush = New SolidBrush(ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive))
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
                ' pevent.Graphics.DrawRectangle(New Pen(New SolidBrush(Color.Black)), Me.Width - Single.Parse(Me.Width / 6) - 3, 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, Me.Height - Single.Parse(Me.Height / 4) - 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
                '  pevent.Graphics.DrawRectangle(New Pen(New SolidBrush(Color.Black)), Me.Width - Single.Parse(Me.Width / 6) - 3, Me.Height - Single.Parse(Me.Height / 4) - 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
            Case enumGapFillerButtonValue.PASS
                brush = New SolidBrush(ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active))
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
                brush = New SolidBrush(ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive))
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, Me.Height - Single.Parse(Me.Height / 4) - 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
            Case enumGapFillerButtonValue.FAIL
                brush = New SolidBrush(ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive))
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
                brush = New SolidBrush(Color.Red)
                pevent.Graphics.FillRectangle(brush, Me.Width - Single.Parse(Me.Width / 6) - 3, Me.Height - Single.Parse(Me.Height / 4) - 3, Single.Parse(Me.Width / 6), Single.Parse(Me.Height / 4))
        End Select
    End Sub


End Class

Public Enum enumGapFillerButtonValue
    NONE
    PASS
    FAIL
End Enum
