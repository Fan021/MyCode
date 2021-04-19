Imports System.Drawing
Imports System.Windows.Forms
Public Class HMIDateTime
    Private cFormChangeSizeCfg As New clsChangeSizeCfg
    Private cSizeChangeSizeCfg As New clsChangeSizeCfg
    Private calander As New HMIDateTimePick

    Public Property DateTimeToString As String
        Get
            Return TextBoxValue.Text
        End Get
        Set(ByVal value As String)
            TextBoxValue.Text = value
        End Set
    End Property

    Private Sub Button_Picture_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Picture.Click
        calander.Visible = True
        If TextBoxValue.Text = "" Then
            TextBoxValue.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        End If
        calander.MonthCalendar.SelectionStart = DateTime.Parse(TextBoxValue.Text)
        calander.MonthCalendar.SelectionEnd = DateTime.Parse(TextBoxValue.Text)
        calander.ComboBox_Hour.Text = DateTime.Parse(TextBoxValue.Text).Hour.ToString("D2")
        calander.ComboBox_Minute.Text = DateTime.Parse(TextBoxValue.Text).Minute.ToString("D2")
        calander.ComboBox_Second.Text = DateTime.Parse(TextBoxValue.Text).Second.ToString("D2")
        Me.BringToFront()
        calander.BringToFront()
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TextBoxValue.Text = calander.MonthCalendar.SelectionStart.ToString("yyyy-MM-dd") + " " + calander.ComboBox_Hour.Text + ":" + calander.ComboBox_Minute.Text + ":" + calander.ComboBox_Second.Text
        calander.Visible = False
        calander.SendToBack()
    End Sub

    Private Sub HMIEdit_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_Body.Resize
        cFormChangeSizeCfg.newH = Me.Panel_Body.Width
        If cFormChangeSizeCfg.newH <> cFormChangeSizeCfg.oldH And cFormChangeSizeCfg.oldH > 1 And cFormChangeSizeCfg.newH > 1 Then
            Panel_Date.Width = Panel_Body.Width - 6
            Panel_Date.Height = TextBoxValue.Height + 6 + 7
            Panel_Date.Location = New System.Drawing.Point(3, (Panel_Body.Height - Panel_Date.Height) / 2)
            TextBoxValue.Width = Panel_Date.Width
            TextBoxValue.Location = New System.Drawing.Point(0, (Panel_Date.Height - TextBoxValue.Height) / 2)
        End If
        cFormChangeSizeCfg.oldH = cFormChangeSizeCfg.newH
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxValue.SizeChanged
        cSizeChangeSizeCfg.newH = Me.TextBoxValue.Height
        If cSizeChangeSizeCfg.newH <> cSizeChangeSizeCfg.oldH And cSizeChangeSizeCfg.oldH > 1 And cSizeChangeSizeCfg.newH > 1 Then
            Panel_Date.Width = Panel_Body.Width - 6
            Panel_Date.Height = TextBoxValue.Height + 6 + 7
            Panel_Date.Location = New System.Drawing.Point(3, (Panel_Body.Height - Panel_Date.Height) / 2)
            TextBoxValue.Width = Panel_Date.Width
            TextBoxValue.Location = New System.Drawing.Point(0, (Panel_Date.Height - TextBoxValue.Height) / 2)
        End If
        cSizeChangeSizeCfg.oldH = cSizeChangeSizeCfg.newH
    End Sub

    Private Sub HMIDateTime_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxValue.Name = Name
        calander.Margin = New Padding(0)
        calander.Name = "calander"
        calander.TabStop = False
        calander.Visible = False
        ParentForm.Controls.Add(calander)
        Dim pos As Point = ParentForm.PointToClient(Me.PointToScreen(Point.Empty))
        calander.Left = pos.X
        calander.Top = pos.Y + Panel_Date.Height
        AddHandler calander.Button_OK.Click, AddressOf Button_Click
    End Sub
End Class
