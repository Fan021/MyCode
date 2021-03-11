Imports System.Windows.Forms
Imports System.Drawing

Public Class MainFunctionButton
    Private bOldIndicateValue As Boolean = False
    Private iCnt As Integer = 0
    Private bLastValue As Boolean = True

    Public Property FunctionEnable
        Set(ByVal value)
            If bLastValue <> value Then
                If value Then
                    Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Level)
                    Button_Name.Enabled = True
                Else
                    Button_Name.BackColor = Color.LightGray
                    Button_Name.Enabled = False
                End If
            End If
            bLastValue = value
        End Set
        Get
            Return Button_Name.Enabled
        End Get
    End Property

    Public ReadOnly Property MainButton As Button
        Get
            Return Button_Name
        End Get
    End Property


    Public ReadOnly Property MainIndicate As Panel
        Get
            Return Panel_Indicate
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
        Button_Name.Text = strText
        Button_Name.Name = strName
        Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        Return True
    End Function

    Public Function SetIndicateBackColor(ByVal bValue As Boolean) As Boolean
        If bOldIndicateValue = bValue And iCnt = 1 Then Return True
        If bValue Then
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
        iCnt = 1
        bOldIndicateValue = bValue
        Return True
    End Function

    Private Sub Button_Name_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseEnter
        Button_Name.FlatAppearance.MouseOverBackColor = Button_Name.BackColor
    End Sub

    Private Sub Button_Name_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button_Name.MouseDown
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Button_Name.FlatAppearance.MouseDownBackColor = Button_Name.BackColor
    End Sub

    Private Sub Button_Name_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button_Name.MouseUp
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Level)
        Button_Name.FlatAppearance.MouseDownBackColor = Button_Name.BackColor
    End Sub

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

    Private Sub Panel_Indicate_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Indicate.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid)
    End Sub


End Class
