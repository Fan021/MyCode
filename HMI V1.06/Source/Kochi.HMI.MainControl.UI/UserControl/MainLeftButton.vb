Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Public Class MainLeftButton
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

    Public Property FunctionEnable
        Set(ByVal value)
            If value Then
                Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Level)
                Button_Name.Enabled = True
            Else
                Button_Name.BackColor = Color.LightGray
                Button_Name.Enabled = False
                Button_Name.BackColor = Color.LightGray
            End If
        End Set
        Get
            Return Button_Name.Enabled
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean
        Button_Name.Text = strText
        Button_Name.Name = strName
        Panel_Indicate.Name = strName
        Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        Return True
    End Function



    Public Function SetIndicateBackColor(ByVal strName As String) As Boolean
        If Panel_Indicate.Name = strName Then
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Active)
        Else
            Panel_Indicate.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOE_MainButton_Indicate_Inactive)
        End If
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

    Private Sub Panel_Indicate_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Indicate.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid,
                         ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Indicate), 2, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Button_Name_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseEnter
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
    End Sub

    Private Sub Button_Name_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Name.MouseLeave
        Button_Name.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Level)
    End Sub

    Private Sub MainLeftButton_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Dim iSize As Double = 0.6
        If File.Exists(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".png") Then
            Dim t As Image = Image.FromFile(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".png")
            Button_Name.Image = New Bitmap(t, Button_Name.Height * iSize, Button_Name.Height * iSize)
        End If
        If File.Exists(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".ico") Then
            Dim t As Image = Image.FromFile(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".ico")
            Button_Name.Image = New Bitmap(t, Button_Name.Height * iSize, Button_Name.Height * iSize)
        End If
        If File.Exists(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".jpg") Then
            Dim t As Image = Image.FromFile(My.Application.Info.DirectoryPath + "\Resources\" + Button_Name.Name + ".jpg")
            Button_Name.Image = New Bitmap(t, Button_Name.Height * iSize, Button_Name.Height * iSize)
        End If
    End Sub
End Class
