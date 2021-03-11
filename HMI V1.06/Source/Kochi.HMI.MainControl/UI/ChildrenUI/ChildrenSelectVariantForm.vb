Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device
Imports System.IO
Public Class ChildrenSelectVariantForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorMessageManager As clsErrorMessageManager
    Private cVariantManager As clsVariantManager
    Private cChangePage As clsChangePage
    Private cDeviceManager As clsDeviceManager
    Private strButtonName As String
    Private cHMIPLC As clsHMIPLC
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cProcessStart As clsProcessStart
    Private cSystemManager As clsSystemManager
    Private TableLayoutPanel_Tab As New HMITableLayoutPanel
    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            InitForm()
            InitControlText()
            If Not cLocalElement.ContainsKey(enumUIName.ChildrenSelectVariantForm.ToString) Then cLocalElement.Add(enumUIName.ChildrenSelectVariantForm.ToString, Me)
            If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.TouchKeyBoard) = "TRUE" Then
                System.Threading.Thread.Sleep(50)
                cProcessStart = New clsProcessStart
                System.Threading.Thread.Sleep(50)
                cProcessStart.Start("C:\Windows\System32", "osk.exe")
            End If
            Timer1.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenSelectVariantForm.ToString))
            Return False
        End Try
    End Function


    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        '  TableLayoutPanel_Body_Mid.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormRight)
        TopLevel = False

        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            ComboBox_Variant.Items.Add(element.Variant)
        Next
        Return True
    End Function
    Public Function InitControlText() As Boolean
        HmiButton_Confirm.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSelectVariantForm.ToString, "HmiButton_Confirm")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSelectVariantForm.ToString, "HmiButton_Cancel")
        HmiLabel_Title.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenSelectVariantForm.ToString, "HmiLabel_Title")
        HmiLabel_Title.Label.Font = New System.Drawing.Font("Calibri", 14.0!)
        HmiLabel_Title.Label.TextAlign = ContentAlignment.MiddleLeft
        HmiButton_Confirm.Button.Enabled = False

        AddHandler ComboBox_Variant.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Confirm.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf Button_Click
        AddHandler ComboBox_Variant.KeyUp, AddressOf ComboBox_KeyUp
        AddHandler lstVariant.Click, AddressOf lstVariant_Click
        AddHandler ComboBox_Variant.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        ComboBox_Variant.Text = cVariantManager.CurrentVariantCfg.Variant
        Return True
    End Function


    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body_Mid.RowStyles(0).Height = ComboBox_Variant.Height + 6 + 6
            TableLayoutPanel_Body_Mid.RowStyles(1).Height = ComboBox_Variant.Height + 6 + 6
            TableLayoutPanel_Body_Mid.RowStyles(2).Height = ComboBox_Variant.Height + 6 + 6
            CreateMessage()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenSelectVariantForm.ToString))
        End Try
    End Sub

    Private Sub Panel_Mid_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Mid.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                    System.Drawing.Color.LightGray, 1, ButtonBorderStyle.Solid,
                    System.Drawing.Color.LightGray, 1, ButtonBorderStyle.Solid,
                    System.Drawing.Color.LightGray, 1, ButtonBorderStyle.Solid,
                    System.Drawing.Color.LightGray, 1, ButtonBorderStyle.Solid)

    End Sub

    Private Sub ComboBox_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_ComBox.Resize
        ComboBox_Variant.Width = Panel_ComBox.Width - 6
        ComboBox_Variant.Location = New System.Drawing.Point(3, 3)
        lstVariant.Width = Panel_ComBox.Width - 6
        lstVariant.Location = New System.Drawing.Point(3, ComboBox_Variant.Height + 3)
        TableLayoutPanel_Tab.Width = Panel_ComBox.Width - 6
        TableLayoutPanel_Tab.Height = Panel_ComBox.Height - ComboBox_Variant.Height
    End Sub

    Private Sub ComboBox_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        CreateList()
    End Sub
    Private Sub CreateList()
        Panel_ComBox.Controls.Clear()
        Panel_ComBox.Controls.Add(ComboBox_Variant)
        Panel_ComBox.Controls.Add(lstVariant)
        ComboBox_Variant.Width = Panel_ComBox.Width - 6
        ComboBox_Variant.Location = New System.Drawing.Point(3, 3)
        lstVariant.Width = Panel_ComBox.Width - 6
        lstVariant.Height = Panel_ComBox.Height - ComboBox_Variant.Height
        lstVariant.Location = New System.Drawing.Point(3, ComboBox_Variant.Height + 3)
        lstVariant.Visible = True
        lstVariant.Items.Clear()
        lstVariant.Font = ComboBox_Variant.Font
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            If element.Variant.IndexOf(ComboBox_Variant.Text) >= 0 Then
                lstVariant.Items.Add(element.Variant)
            End If
        Next
        ComboBox_Variant.Focus()
        ComboBox_Variant.Select(ComboBox_Variant.Text.Length, 0)
    End Sub
    Private Sub CreateMessage()
        ComboBox_Variant.Focus()
        lstVariant.Visible = False
        lstVariant.Items.Clear()
        Dim TableLayoutPanel_Tab As New HMITableLayoutPanel
        TableLayoutPanel_Tab.ColumnCount = 1
        TableLayoutPanel_Tab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Tab.Dock = System.Windows.Forms.DockStyle.None
        TableLayoutPanel_Tab.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab.Name = "TableLayoutPanel_Tab"
        TableLayoutPanel_Tab.Font = ComboBox_Variant.Font
        TableLayoutPanel_Tab.RowCount = 2
        TableLayoutPanel_Tab.Dock = DockStyle.None
        TableLayoutPanel_Tab.Font = New System.Drawing.Font("Calibri", 12.0!)
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40))
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60))
        ' TableLayoutPanel_Tab.BackColor = Color.Yellow

        Dim cHMITextBox As New TextBox
        cHMITextBox.Dock = System.Windows.Forms.DockStyle.Fill
        cHMITextBox.Location = New System.Drawing.Point(0, 0)
        cHMITextBox.Margin = New System.Windows.Forms.Padding(0)
        cHMITextBox.Name = "HMITextBox"
        cHMITextBox.Font = New System.Drawing.Font(ComboBox_Variant.Font.Name, ComboBox_Variant.Font.Size * 0.6)
        cHMITextBox.Padding = New System.Windows.Forms.Padding(0)
        cHMITextBox.Multiline = True
        cHMITextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        TableLayoutPanel_Tab.Controls.Add(cHMITextBox, 0, 0)

        Dim cPictureBox As New PictureBox
        cPictureBox.Dock = System.Windows.Forms.DockStyle.Fill
        cPictureBox.Location = New System.Drawing.Point(0, 0)
        cPictureBox.Margin = New System.Windows.Forms.Padding(0)
        cPictureBox.Name = "PictureBox"
        cPictureBox.Font = ComboBox_Variant.Font
        cPictureBox.Padding = New System.Windows.Forms.Padding(0)
        cPictureBox.SizeMode = PictureBoxSizeMode.Zoom
        TableLayoutPanel_Tab.Controls.Add(cPictureBox, 0, 1)
        Panel_ComBox.Controls.Clear()
        Panel_ComBox.Controls.Add(ComboBox_Variant)
        Panel_ComBox.Controls.Add(TableLayoutPanel_Tab)

        ComboBox_Variant.Width = Panel_ComBox.Width - 6
        ComboBox_Variant.Location = New System.Drawing.Point(3, 3)
        TableLayoutPanel_Tab.Width = Panel_ComBox.Width - 6
        TableLayoutPanel_Tab.Height = Panel_ComBox.Height - ComboBox_Variant.Height
        TableLayoutPanel_Tab.Location = New System.Drawing.Point(3, ComboBox_Variant.Height + 3)
        Dim cVariantCfg As clsVariantCfg = cVariantManager.GetVariantCfgFromVariant(ComboBox_Variant.Text)
        If Not IsNothing(cVariantCfg) Then
            If cLanguageManager.SecondLanguageActive Then
                cHMITextBox.Text = cVariantCfg.Description2
                If cHMITextBox.Text = "" Then
                    cHMITextBox.Text = cVariantCfg.Description
                End If
            Else
                cHMITextBox.Text = cVariantCfg.Description
            End If
            If File.Exists(cVariantCfg.PicturePath) Then
                cPictureBox.Image = Image.FromFile(cVariantCfg.PicturePath)
            End If
        End If
        ComboBox_Variant.Focus()
        ComboBox_Variant.Select(ComboBox_Variant.Text.Length, 0)
    End Sub

    Private Sub lstVariant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If lstVariant.SelectedIndex < 0 Then
            lstVariant.Visible = False
            Return
        End If
        ComboBox_Variant.Text = lstVariant.Items.Item(lstVariant.SelectedIndex).ToString
        lstVariant.Visible = False

        CreateMessage()
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ComboBox_Variant.Text <> "" Then
            HmiButton_Confirm.Button.Enabled = True
        End If
        CreateMessage()
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Confirm"
                If Not cVariantManager.HasVariant(ComboBox_Variant.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenSelectVariantForm.ToString, "1", ComboBox_Variant.Text), enumExceptionType.Alarm, enumUIName.ChildrenSelectVariantForm.ToString))
                    Return
                End If
                HmiButton_Confirm.Enabled = False
                cVariantManager.ChangeVariant(ComboBox_Variant.Text)
                cHMIPLC = cDeviceManager.GetPLCDevice
                cChangePage.BackPage()

            Case "HmiButton_Cancel"
                If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                    cChangePage.BackPage()
                End If
        End Select
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenSelectVariantForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenSelectVariantForm.ToString)
        If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.TouchKeyBoard) = "TRUE" Then
            If Not IsNothing(cProcessStart) Then
                cProcessStart.Stop()
                cProcessStart.Start(cSystemManager.Settings.ExeFolder, "KillProcess.bat", False)
            End If

        End If
        Me.Dispose()
        Return True
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        ComboBox_Variant.Focus()
    End Sub
End Class