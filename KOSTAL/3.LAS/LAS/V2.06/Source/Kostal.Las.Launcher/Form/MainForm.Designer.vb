<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.StatusForm = New System.Windows.Forms.StatusStrip()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuChangePassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowWtDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowCounterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowScheduleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tssLblKostal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.picKostal = New System.Windows.Forms.PictureBox()
        Me.picAutomobilElectric = New System.Windows.Forms.PictureBox()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.MainLogger = New System.Windows.Forms.ListBox()
        Me.MainBox = New System.Windows.Forms.GroupBox()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.TabControlStations = New System.Windows.Forms.TabControl()
        Me.picArticle = New System.Windows.Forms.PictureBox()
        Me.gbActualSerialNumber_01 = New System.Windows.Forms.GroupBox()
        Me.lblActualSerialNumber_01 = New System.Windows.Forms.Label()
        Me.grpCounter = New System.Windows.Forms.GroupBox()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Label_Fail = New System.Windows.Forms.Label()
        Me.Label_Pass = New System.Windows.Forms.Label()
        Me.Label_Total = New System.Windows.Forms.Label()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.lblfail = New System.Windows.Forms.Label()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.lblStatusName_16 = New System.Windows.Forms.Label()
        Me.lblStatusName_15 = New System.Windows.Forms.Label()
        Me.lblStatusName_14 = New System.Windows.Forms.Label()
        Me.lblStatusName_13 = New System.Windows.Forms.Label()
        Me.lblStatusName_12 = New System.Windows.Forms.Label()
        Me.lblStatusName_11 = New System.Windows.Forms.Label()
        Me.lblStatusName_10 = New System.Windows.Forms.Label()
        Me.lblStatusName_09 = New System.Windows.Forms.Label()
        Me.lblStatusName_08 = New System.Windows.Forms.Label()
        Me.lblStatusName_07 = New System.Windows.Forms.Label()
        Me.lblStatusName_06 = New System.Windows.Forms.Label()
        Me.lblStatusName_05 = New System.Windows.Forms.Label()
        Me.lblStatusName_04 = New System.Windows.Forms.Label()
        Me.lblStatusName_03 = New System.Windows.Forms.Label()
        Me.lblStatusName_02 = New System.Windows.Forms.Label()
        Me.lblStatusName_01 = New System.Windows.Forms.Label()
        Me.gbArticle = New System.Windows.Forms.GroupBox()
        Me.lstMatchBox = New System.Windows.Forms.ListBox()
        Me.DG_Article = New System.Windows.Forms.DataGridView()
        Me.DG_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnArticle = New System.Windows.Forms.Button()
        Me.CBArticle = New System.Windows.Forms.ComboBox()
        Me.lblRefPart = New System.Windows.Forms.Label()
        Me.timCycle = New System.Windows.Forms.Timer(Me.components)
        Me.SkinEngine1 = New Sunisoft.IrisSkin.SkinEngine(CType(Me, System.ComponentModel.Component))
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenu.SuspendLayout()
        CType(Me.picKostal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAutomobilElectric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainBox.SuspendLayout()
        Me.grpPicture.SuspendLayout()
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbActualSerialNumber_01.SuspendLayout()
        Me.grpCounter.SuspendLayout()
        Me.grpStatus.SuspendLayout()
        Me.gbArticle.SuspendLayout()
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusForm
        '
        Me.StatusForm.BackColor = System.Drawing.Color.Silver
        Me.StatusForm.Location = New System.Drawing.Point(0, 913)
        Me.StatusForm.Name = "StatusForm"
        Me.StatusForm.Size = New System.Drawing.Size(1276, 22)
        Me.StatusForm.TabIndex = 0
        Me.StatusForm.Text = "StatusStrip1"
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ShowWtDataToolStripMenuItem, Me.ShowCounterToolStripMenuItem1, Me.ShowScheduleToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(1276, 25)
        Me.MainMenu.TabIndex = 3
        Me.MainMenu.Text = "MainMenu"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuLanguage, Me.MenuChangePassword, Me.MenuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(39, 21)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'MenuLanguage
        '
        Me.MenuLanguage.Name = "MenuLanguage"
        Me.MenuLanguage.Size = New System.Drawing.Size(176, 22)
        Me.MenuLanguage.Text = "Language"
        '
        'MenuChangePassword
        '
        Me.MenuChangePassword.Name = "MenuChangePassword"
        Me.MenuChangePassword.Size = New System.Drawing.Size(176, 22)
        Me.MenuChangePassword.Text = "ChangePassword"
        '
        'MenuExit
        '
        Me.MenuExit.Name = "MenuExit"
        Me.MenuExit.Size = New System.Drawing.Size(176, 22)
        Me.MenuExit.Text = "Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(47, 21)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ShowWtDataToolStripMenuItem
        '
        Me.ShowWtDataToolStripMenuItem.Name = "ShowWtDataToolStripMenuItem"
        Me.ShowWtDataToolStripMenuItem.Size = New System.Drawing.Size(94, 21)
        Me.ShowWtDataToolStripMenuItem.Text = "ShowWtData"
        '
        'ShowCounterToolStripMenuItem1
        '
        Me.ShowCounterToolStripMenuItem1.Name = "ShowCounterToolStripMenuItem1"
        Me.ShowCounterToolStripMenuItem1.Size = New System.Drawing.Size(97, 21)
        Me.ShowCounterToolStripMenuItem1.Text = "ShowCounter"
        '
        'ShowScheduleToolStripMenuItem
        '
        Me.ShowScheduleToolStripMenuItem.Name = "ShowScheduleToolStripMenuItem"
        Me.ShowScheduleToolStripMenuItem.Size = New System.Drawing.Size(103, 21)
        Me.ShowScheduleToolStripMenuItem.Text = "ShowSchedule"
        '
        'tssLblKostal
        '
        Me.tssLblKostal.Image = Global.Kostal.Las.My.Resources.Resources.logo_screen_145px2
        Me.tssLblKostal.Name = "tssLblKostal"
        Me.tssLblKostal.Size = New System.Drawing.Size(70, 17)
        Me.tssLblKostal.Text = "KOSTAL"
        '
        'picKostal
        '
        Me.picKostal.BackColor = System.Drawing.Color.Transparent
        Me.picKostal.BackgroundImage = Global.Kostal.Las.My.Resources.Resources.logo_screen_145px2
        Me.picKostal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picKostal.Location = New System.Drawing.Point(1059, 20)
        Me.picKostal.Name = "picKostal"
        Me.picKostal.Size = New System.Drawing.Size(202, 50)
        Me.picKostal.TabIndex = 14
        Me.picKostal.TabStop = False
        '
        'picAutomobilElectric
        '
        Me.picAutomobilElectric.BackColor = System.Drawing.Color.White
        Me.picAutomobilElectric.BackgroundImage = Global.Kostal.Las.My.Resources.Resources.automatic_controller
        Me.picAutomobilElectric.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAutomobilElectric.Location = New System.Drawing.Point(10, 46)
        Me.picAutomobilElectric.Name = "picAutomobilElectric"
        Me.picAutomobilElectric.Size = New System.Drawing.Size(281, 24)
        Me.picAutomobilElectric.TabIndex = 15
        Me.picAutomobilElectric.TabStop = False
        '
        'lblLine
        '
        Me.lblLine.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.Location = New System.Drawing.Point(6, 95)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(1253, 2)
        Me.lblLine.TabIndex = 16
        Me.lblLine.Text = "Label5"
        '
        'MainLogger
        '
        Me.MainLogger.FormattingEnabled = True
        Me.MainLogger.ItemHeight = 12
        Me.MainLogger.Location = New System.Drawing.Point(0, 805)
        Me.MainLogger.Name = "MainLogger"
        Me.MainLogger.Size = New System.Drawing.Size(1270, 112)
        Me.MainLogger.TabIndex = 23
        '
        'MainBox
        '
        Me.MainBox.BackColor = System.Drawing.Color.White
        Me.MainBox.Controls.Add(Me.grpPicture)
        Me.MainBox.Controls.Add(Me.gbActualSerialNumber_01)
        Me.MainBox.Controls.Add(Me.grpCounter)
        Me.MainBox.Controls.Add(Me.grpStatus)
        Me.MainBox.Controls.Add(Me.gbArticle)
        Me.MainBox.Controls.Add(Me.picAutomobilElectric)
        Me.MainBox.Controls.Add(Me.lblLine)
        Me.MainBox.Controls.Add(Me.picKostal)
        Me.MainBox.Controls.Add(Me.lblRefPart)
        Me.MainBox.Location = New System.Drawing.Point(0, 25)
        Me.MainBox.Name = "MainBox"
        Me.MainBox.Size = New System.Drawing.Size(1270, 817)
        Me.MainBox.TabIndex = 24
        Me.MainBox.TabStop = False
        Me.MainBox.Text = "MainBox"
        '
        'grpPicture
        '
        Me.grpPicture.BackColor = System.Drawing.Color.Transparent
        Me.grpPicture.Controls.Add(Me.TabControlStations)
        Me.grpPicture.Controls.Add(Me.picArticle)
        Me.grpPicture.Location = New System.Drawing.Point(540, 375)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Padding = New System.Windows.Forms.Padding(2)
        Me.grpPicture.Size = New System.Drawing.Size(724, 315)
        Me.grpPicture.TabIndex = 23
        Me.grpPicture.TabStop = False
        Me.grpPicture.Text = "GroupBox1"
        '
        'TabControlStations
        '
        Me.TabControlStations.Location = New System.Drawing.Point(0, 14)
        Me.TabControlStations.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControlStations.Name = "TabControlStations"
        Me.TabControlStations.Padding = New System.Drawing.Point(0, 0)
        Me.TabControlStations.SelectedIndex = 0
        Me.TabControlStations.Size = New System.Drawing.Size(724, 300)
        Me.TabControlStations.TabIndex = 29
        Me.TabControlStations.Visible = False
        '
        'picArticle
        '
        Me.picArticle.BackColor = System.Drawing.Color.Transparent
        Me.picArticle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.picArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picArticle.Location = New System.Drawing.Point(2, 16)
        Me.picArticle.Name = "picArticle"
        Me.picArticle.Size = New System.Drawing.Size(720, 297)
        Me.picArticle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picArticle.TabIndex = 20
        Me.picArticle.TabStop = False
        '
        'gbActualSerialNumber_01
        '
        Me.gbActualSerialNumber_01.Controls.Add(Me.lblActualSerialNumber_01)
        Me.gbActualSerialNumber_01.Location = New System.Drawing.Point(7, 706)
        Me.gbActualSerialNumber_01.Name = "gbActualSerialNumber_01"
        Me.gbActualSerialNumber_01.Size = New System.Drawing.Size(519, 55)
        Me.gbActualSerialNumber_01.TabIndex = 64
        Me.gbActualSerialNumber_01.TabStop = False
        Me.gbActualSerialNumber_01.Text = "gbActualSerialNumber_01"
        '
        'lblActualSerialNumber_01
        '
        Me.lblActualSerialNumber_01.BackColor = System.Drawing.Color.White
        Me.lblActualSerialNumber_01.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActualSerialNumber_01.Location = New System.Drawing.Point(55, 15)
        Me.lblActualSerialNumber_01.Name = "lblActualSerialNumber_01"
        Me.lblActualSerialNumber_01.Size = New System.Drawing.Size(370, 33)
        Me.lblActualSerialNumber_01.TabIndex = 63
        Me.lblActualSerialNumber_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpCounter
        '
        Me.grpCounter.Controls.Add(Me.Button_Reset)
        Me.grpCounter.Controls.Add(Me.Label_Fail)
        Me.grpCounter.Controls.Add(Me.Label_Pass)
        Me.grpCounter.Controls.Add(Me.Label_Total)
        Me.grpCounter.Controls.Add(Me.lbltotal)
        Me.grpCounter.Controls.Add(Me.lblfail)
        Me.grpCounter.Controls.Add(Me.lblPass)
        Me.grpCounter.Location = New System.Drawing.Point(540, 707)
        Me.grpCounter.Name = "grpCounter"
        Me.grpCounter.Size = New System.Drawing.Size(721, 55)
        Me.grpCounter.TabIndex = 25
        Me.grpCounter.TabStop = False
        Me.grpCounter.Text = "grpCounter"
        '
        'Button_Reset
        '
        Me.Button_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Button_Reset.Location = New System.Drawing.Point(638, 14)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(75, 33)
        Me.Button_Reset.TabIndex = 27
        Me.Button_Reset.Text = "Reset"
        Me.Button_Reset.UseVisualStyleBackColor = False
        '
        'Label_Fail
        '
        Me.Label_Fail.AutoSize = True
        Me.Label_Fail.Location = New System.Drawing.Point(432, 27)
        Me.Label_Fail.Name = "Label_Fail"
        Me.Label_Fail.Size = New System.Drawing.Size(35, 12)
        Me.Label_Fail.TabIndex = 26
        Me.Label_Fail.Text = "Fail:"
        '
        'Label_Pass
        '
        Me.Label_Pass.AutoSize = True
        Me.Label_Pass.Location = New System.Drawing.Point(226, 27)
        Me.Label_Pass.Name = "Label_Pass"
        Me.Label_Pass.Size = New System.Drawing.Size(35, 12)
        Me.Label_Pass.TabIndex = 25
        Me.Label_Pass.Text = "Pass:"
        '
        'Label_Total
        '
        Me.Label_Total.AutoSize = True
        Me.Label_Total.Location = New System.Drawing.Point(23, 27)
        Me.Label_Total.Name = "Label_Total"
        Me.Label_Total.Size = New System.Drawing.Size(41, 12)
        Me.Label_Total.TabIndex = 2
        Me.Label_Total.Text = "Total:"
        '
        'lbltotal
        '
        Me.lbltotal.BackColor = System.Drawing.Color.White
        Me.lbltotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltotal.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lbltotal.Location = New System.Drawing.Point(70, 14)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(137, 33)
        Me.lbltotal.TabIndex = 0
        Me.lbltotal.Text = "0"
        Me.lbltotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfail
        '
        Me.lblfail.BackColor = System.Drawing.Color.White
        Me.lblfail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblfail.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfail.ForeColor = System.Drawing.Color.Red
        Me.lblfail.Location = New System.Drawing.Point(473, 14)
        Me.lblfail.Name = "lblfail"
        Me.lblfail.Size = New System.Drawing.Size(137, 33)
        Me.lblfail.TabIndex = 1
        Me.lblfail.Text = "0"
        Me.lblfail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPass
        '
        Me.lblPass.BackColor = System.Drawing.Color.White
        Me.lblPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPass.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPass.Location = New System.Drawing.Point(273, 14)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(137, 33)
        Me.lblPass.TabIndex = 0
        Me.lblPass.Text = "0"
        Me.lblPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpStatus
        '
        Me.grpStatus.Controls.Add(Me.lblStatusName_16)
        Me.grpStatus.Controls.Add(Me.lblStatusName_15)
        Me.grpStatus.Controls.Add(Me.lblStatusName_14)
        Me.grpStatus.Controls.Add(Me.lblStatusName_13)
        Me.grpStatus.Controls.Add(Me.lblStatusName_12)
        Me.grpStatus.Controls.Add(Me.lblStatusName_11)
        Me.grpStatus.Controls.Add(Me.lblStatusName_10)
        Me.grpStatus.Controls.Add(Me.lblStatusName_09)
        Me.grpStatus.Controls.Add(Me.lblStatusName_08)
        Me.grpStatus.Controls.Add(Me.lblStatusName_07)
        Me.grpStatus.Controls.Add(Me.lblStatusName_06)
        Me.grpStatus.Controls.Add(Me.lblStatusName_05)
        Me.grpStatus.Controls.Add(Me.lblStatusName_04)
        Me.grpStatus.Controls.Add(Me.lblStatusName_03)
        Me.grpStatus.Controls.Add(Me.lblStatusName_02)
        Me.grpStatus.Controls.Add(Me.lblStatusName_01)
        Me.grpStatus.Location = New System.Drawing.Point(10, 375)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Size = New System.Drawing.Size(519, 315)
        Me.grpStatus.TabIndex = 19
        Me.grpStatus.TabStop = False
        '
        'lblStatusName_16
        '
        Me.lblStatusName_16.BackColor = System.Drawing.Color.White
        Me.lblStatusName_16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_16.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_16.Location = New System.Drawing.Point(8, 285)
        Me.lblStatusName_16.Name = "lblStatusName_16"
        Me.lblStatusName_16.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_16.TabIndex = 15
        Me.lblStatusName_16.Tag = "16"
        Me.lblStatusName_16.Text = "lblStatusName_16"
        Me.lblStatusName_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_15
        '
        Me.lblStatusName_15.BackColor = System.Drawing.Color.White
        Me.lblStatusName_15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_15.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_15.Location = New System.Drawing.Point(8, 267)
        Me.lblStatusName_15.Name = "lblStatusName_15"
        Me.lblStatusName_15.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_15.TabIndex = 14
        Me.lblStatusName_15.Tag = "15"
        Me.lblStatusName_15.Text = "lblStatusName_15"
        Me.lblStatusName_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_14
        '
        Me.lblStatusName_14.BackColor = System.Drawing.Color.White
        Me.lblStatusName_14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_14.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_14.Location = New System.Drawing.Point(8, 249)
        Me.lblStatusName_14.Name = "lblStatusName_14"
        Me.lblStatusName_14.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_14.TabIndex = 13
        Me.lblStatusName_14.Tag = "14"
        Me.lblStatusName_14.Text = "lblStatusName_14"
        Me.lblStatusName_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_13
        '
        Me.lblStatusName_13.BackColor = System.Drawing.Color.White
        Me.lblStatusName_13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_13.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_13.Location = New System.Drawing.Point(8, 231)
        Me.lblStatusName_13.Name = "lblStatusName_13"
        Me.lblStatusName_13.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_13.TabIndex = 12
        Me.lblStatusName_13.Tag = "13"
        Me.lblStatusName_13.Text = "lblStatusName_13"
        Me.lblStatusName_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_12
        '
        Me.lblStatusName_12.BackColor = System.Drawing.Color.White
        Me.lblStatusName_12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_12.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_12.Location = New System.Drawing.Point(8, 213)
        Me.lblStatusName_12.Name = "lblStatusName_12"
        Me.lblStatusName_12.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_12.TabIndex = 11
        Me.lblStatusName_12.Tag = "12"
        Me.lblStatusName_12.Text = "lblStatusName_12"
        Me.lblStatusName_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_11
        '
        Me.lblStatusName_11.BackColor = System.Drawing.Color.White
        Me.lblStatusName_11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_11.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_11.Location = New System.Drawing.Point(8, 195)
        Me.lblStatusName_11.Name = "lblStatusName_11"
        Me.lblStatusName_11.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_11.TabIndex = 10
        Me.lblStatusName_11.Tag = "11"
        Me.lblStatusName_11.Text = "lblStatusName_11"
        Me.lblStatusName_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_10
        '
        Me.lblStatusName_10.BackColor = System.Drawing.Color.White
        Me.lblStatusName_10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_10.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_10.Location = New System.Drawing.Point(8, 177)
        Me.lblStatusName_10.Name = "lblStatusName_10"
        Me.lblStatusName_10.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_10.TabIndex = 9
        Me.lblStatusName_10.Tag = "10"
        Me.lblStatusName_10.Text = "lblStatusName_10"
        Me.lblStatusName_10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_09
        '
        Me.lblStatusName_09.BackColor = System.Drawing.Color.White
        Me.lblStatusName_09.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_09.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_09.Location = New System.Drawing.Point(8, 159)
        Me.lblStatusName_09.Name = "lblStatusName_09"
        Me.lblStatusName_09.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_09.TabIndex = 8
        Me.lblStatusName_09.Tag = "09"
        Me.lblStatusName_09.Text = "lblStatusName_09"
        Me.lblStatusName_09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_08
        '
        Me.lblStatusName_08.BackColor = System.Drawing.Color.White
        Me.lblStatusName_08.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_08.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_08.Location = New System.Drawing.Point(8, 141)
        Me.lblStatusName_08.Name = "lblStatusName_08"
        Me.lblStatusName_08.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_08.TabIndex = 7
        Me.lblStatusName_08.Tag = "08"
        Me.lblStatusName_08.Text = "lblStatusName_08"
        Me.lblStatusName_08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_07
        '
        Me.lblStatusName_07.BackColor = System.Drawing.Color.White
        Me.lblStatusName_07.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_07.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_07.Location = New System.Drawing.Point(8, 123)
        Me.lblStatusName_07.Name = "lblStatusName_07"
        Me.lblStatusName_07.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_07.TabIndex = 6
        Me.lblStatusName_07.Tag = "07"
        Me.lblStatusName_07.Text = "lblStatusName_07"
        Me.lblStatusName_07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_06
        '
        Me.lblStatusName_06.BackColor = System.Drawing.Color.White
        Me.lblStatusName_06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_06.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_06.Location = New System.Drawing.Point(8, 105)
        Me.lblStatusName_06.Name = "lblStatusName_06"
        Me.lblStatusName_06.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_06.TabIndex = 5
        Me.lblStatusName_06.Tag = "06"
        Me.lblStatusName_06.Text = "lblStatusName_06"
        Me.lblStatusName_06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_05
        '
        Me.lblStatusName_05.BackColor = System.Drawing.Color.White
        Me.lblStatusName_05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_05.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_05.Location = New System.Drawing.Point(8, 87)
        Me.lblStatusName_05.Name = "lblStatusName_05"
        Me.lblStatusName_05.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_05.TabIndex = 4
        Me.lblStatusName_05.Tag = "05"
        Me.lblStatusName_05.Text = "lblStatusName_05"
        Me.lblStatusName_05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_04
        '
        Me.lblStatusName_04.BackColor = System.Drawing.Color.White
        Me.lblStatusName_04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_04.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_04.Location = New System.Drawing.Point(8, 69)
        Me.lblStatusName_04.Name = "lblStatusName_04"
        Me.lblStatusName_04.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_04.TabIndex = 3
        Me.lblStatusName_04.Tag = "04"
        Me.lblStatusName_04.Text = "lblStatusName_04"
        Me.lblStatusName_04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_03
        '
        Me.lblStatusName_03.BackColor = System.Drawing.Color.White
        Me.lblStatusName_03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_03.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_03.Location = New System.Drawing.Point(8, 51)
        Me.lblStatusName_03.Name = "lblStatusName_03"
        Me.lblStatusName_03.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_03.TabIndex = 2
        Me.lblStatusName_03.Tag = "03"
        Me.lblStatusName_03.Text = "lblStatusName_03"
        Me.lblStatusName_03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_02
        '
        Me.lblStatusName_02.BackColor = System.Drawing.Color.White
        Me.lblStatusName_02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_02.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_02.Location = New System.Drawing.Point(8, 33)
        Me.lblStatusName_02.Name = "lblStatusName_02"
        Me.lblStatusName_02.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_02.TabIndex = 1
        Me.lblStatusName_02.Tag = "02"
        Me.lblStatusName_02.Text = "lblStatusName_02"
        Me.lblStatusName_02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatusName_01
        '
        Me.lblStatusName_01.BackColor = System.Drawing.Color.White
        Me.lblStatusName_01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblStatusName_01.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusName_01.Location = New System.Drawing.Point(8, 15)
        Me.lblStatusName_01.Name = "lblStatusName_01"
        Me.lblStatusName_01.Size = New System.Drawing.Size(500, 19)
        Me.lblStatusName_01.TabIndex = 0
        Me.lblStatusName_01.Tag = "01"
        Me.lblStatusName_01.Text = "lblStatusName_01"
        Me.lblStatusName_01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gbArticle
        '
        Me.gbArticle.Controls.Add(Me.lstMatchBox)
        Me.gbArticle.Controls.Add(Me.DG_Article)
        Me.gbArticle.Controls.Add(Me.btnArticle)
        Me.gbArticle.Controls.Add(Me.CBArticle)
        Me.gbArticle.Location = New System.Drawing.Point(10, 100)
        Me.gbArticle.Name = "gbArticle"
        Me.gbArticle.Size = New System.Drawing.Size(1249, 269)
        Me.gbArticle.TabIndex = 18
        Me.gbArticle.TabStop = False
        Me.gbArticle.Text = "Article"
        '
        'lstMatchBox
        '
        Me.lstMatchBox.FormattingEnabled = True
        Me.lstMatchBox.ItemHeight = 12
        Me.lstMatchBox.Location = New System.Drawing.Point(6, 40)
        Me.lstMatchBox.Name = "lstMatchBox"
        Me.lstMatchBox.Size = New System.Drawing.Size(205, 148)
        Me.lstMatchBox.TabIndex = 66
        Me.lstMatchBox.Visible = False
        '
        'DG_Article
        '
        Me.DG_Article.AllowUserToAddRows = False
        Me.DG_Article.AllowUserToDeleteRows = False
        Me.DG_Article.AllowUserToResizeColumns = False
        Me.DG_Article.AllowUserToResizeRows = False
        Me.DG_Article.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DG_Article.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Article.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_Article.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_Name, Me.DG_Value})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Article.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Article.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Article.Location = New System.Drawing.Point(6, 42)
        Me.DG_Article.Name = "DG_Article"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Article.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Article.RowTemplate.Height = 23
        Me.DG_Article.Size = New System.Drawing.Size(1127, 221)
        Me.DG_Article.TabIndex = 2
        '
        'DG_Name
        '
        Me.DG_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DG_Name.HeaderText = "Name"
        Me.DG_Name.Name = "DG_Name"
        Me.DG_Name.ReadOnly = True
        Me.DG_Name.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DG_Value
        '
        Me.DG_Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DG_Value.HeaderText = "Value"
        Me.DG_Value.Name = "DG_Value"
        Me.DG_Value.ReadOnly = True
        Me.DG_Value.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'btnArticle
        '
        Me.btnArticle.BackColor = System.Drawing.Color.White
        Me.btnArticle.Location = New System.Drawing.Point(1139, 16)
        Me.btnArticle.Name = "btnArticle"
        Me.btnArticle.Size = New System.Drawing.Size(104, 25)
        Me.btnArticle.TabIndex = 1
        Me.btnArticle.Text = "btnArticle"
        Me.btnArticle.UseVisualStyleBackColor = False
        '
        'CBArticle
        '
        Me.CBArticle.FormattingEnabled = True
        Me.CBArticle.Location = New System.Drawing.Point(6, 18)
        Me.CBArticle.MaxDropDownItems = 20
        Me.CBArticle.Name = "CBArticle"
        Me.CBArticle.Size = New System.Drawing.Size(1127, 20)
        Me.CBArticle.TabIndex = 0
        '
        'lblRefPart
        '
        Me.lblRefPart.Font = New System.Drawing.Font("Arial", 50.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefPart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefPart.Location = New System.Drawing.Point(16, 141)
        Me.lblRefPart.Name = "lblRefPart"
        Me.lblRefPart.Size = New System.Drawing.Size(1127, 222)
        Me.lblRefPart.TabIndex = 65
        Me.lblRefPart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'timCycle
        '
        Me.timCycle.Interval = 1000
        '
        'SkinEngine1
        '
        Me.SkinEngine1.SerialNumber = ""
        Me.SkinEngine1.SkinFile = Nothing
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2, Me.ToolStripMenuItem3, Me.ToolStripMenuItem4})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(39, 21)
        Me.ToolStripMenuItem1.Text = "File"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem2.Text = "Language"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem3.Text = "ChangePassword"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem4.Text = "Exit"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem6})
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(47, 21)
        Me.ToolStripMenuItem5.Text = "Help"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(111, 22)
        Me.ToolStripMenuItem6.Text = "About"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(94, 21)
        Me.ToolStripMenuItem7.Text = "ShowWtData"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(97, 21)
        Me.ToolStripMenuItem8.Text = "ShowCounter"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(184, 21)
        Me.ToolStripMenuItem9.Text = "ScheduleToolStripMenuItem"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem11, Me.ToolStripMenuItem12, Me.ToolStripMenuItem13})
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(39, 21)
        Me.ToolStripMenuItem10.Text = "File"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem11.Text = "Language"
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem12.Text = "ChangePassword"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem13.Text = "Exit"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem15})
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(47, 21)
        Me.ToolStripMenuItem14.Text = "Help"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(111, 22)
        Me.ToolStripMenuItem15.Text = "About"
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(94, 21)
        Me.ToolStripMenuItem16.Text = "ShowWtData"
        '
        'ToolStripMenuItem17
        '
        Me.ToolStripMenuItem17.Name = "ToolStripMenuItem17"
        Me.ToolStripMenuItem17.Size = New System.Drawing.Size(97, 21)
        Me.ToolStripMenuItem17.Text = "ShowCounter"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(184, 21)
        Me.ToolStripMenuItem18.Text = "ScheduleToolStripMenuItem"
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1276, 935)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.MainLogger)
        Me.Controls.Add(Me.StatusForm)
        Me.Controls.Add(Me.MainBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMain"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        CType(Me.picKostal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAutomobilElectric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainBox.ResumeLayout(False)
        Me.grpPicture.ResumeLayout(False)
        CType(Me.picArticle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbActualSerialNumber_01.ResumeLayout(False)
        Me.grpCounter.ResumeLayout(False)
        Me.grpCounter.PerformLayout()
        Me.grpStatus.ResumeLayout(False)
        Me.gbArticle.ResumeLayout(False)
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusForm As System.Windows.Forms.StatusStrip
    Friend WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents picKostal As System.Windows.Forms.PictureBox
    Friend WithEvents picAutomobilElectric As System.Windows.Forms.PictureBox
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents MainLogger As System.Windows.Forms.ListBox
    Friend WithEvents MainBox As System.Windows.Forms.GroupBox
    Friend WithEvents timCycle As System.Windows.Forms.Timer
    Friend WithEvents gbArticle As System.Windows.Forms.GroupBox
    Friend WithEvents btnArticle As System.Windows.Forms.Button
    Friend WithEvents CBArticle As System.Windows.Forms.ComboBox
    Friend WithEvents DG_Article As System.Windows.Forms.DataGridView
    Friend WithEvents DG_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DG_Value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grpStatus As System.Windows.Forms.GroupBox
    Friend WithEvents lblStatusName_01 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_04 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_03 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_02 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_09 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_08 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_07 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_06 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_05 As System.Windows.Forms.Label
    Friend WithEvents picArticle As System.Windows.Forms.PictureBox
    Friend WithEvents grpPicture As System.Windows.Forms.GroupBox
    Friend WithEvents lbltotal As System.Windows.Forms.Label
    Friend WithEvents grpCounter As System.Windows.Forms.GroupBox
    Friend WithEvents lblStatusName_13 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_12 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_11 As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_10 As System.Windows.Forms.Label
    Friend WithEvents TabControlStations As System.Windows.Forms.TabControl
    Friend WithEvents lblStatusName_14 As System.Windows.Forms.Label
    Friend WithEvents lblActualSerialNumber_01 As System.Windows.Forms.Label
    Friend WithEvents lblRefPart As System.Windows.Forms.Label
    Friend WithEvents lblStatusName_15 As System.Windows.Forms.Label
    Friend WithEvents lblPass As System.Windows.Forms.Label
    Friend WithEvents lstMatchBox As System.Windows.Forms.ListBox
    Friend WithEvents lblStatusName_16 As System.Windows.Forms.Label
    Friend WithEvents tssLblKostal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblfail As System.Windows.Forms.Label
    Friend WithEvents gbActualSerialNumber_01 As System.Windows.Forms.GroupBox
    Friend WithEvents SkinEngine1 As Sunisoft.IrisSkin.SkinEngine
    Friend WithEvents Button_Reset As System.Windows.Forms.Button
    Friend WithEvents Label_Fail As System.Windows.Forms.Label
    Friend WithEvents Label_Pass As System.Windows.Forms.Label
    Friend WithEvents Label_Total As System.Windows.Forms.Label
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuLanguage As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuChangePassword As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowWtDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowCounterToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowScheduleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
