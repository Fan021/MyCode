<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm_Bosh
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm_Bosh))
        Me.StatusForm = New System.Windows.Forms.StatusStrip()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuLanguage = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuChangePassword = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowWtDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowCounterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowScheduleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowMaintainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowStationOverViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowProductionViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowErrorCodeViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tssLblKostal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MainLogger = New System.Windows.Forms.ListBox()
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
        Me.SkinEngine1 = New Sunisoft.IrisSkin.SkinEngine(CType(Me, System.ComponentModel.Component))
        Me.timCycle = New System.Windows.Forms.Timer(Me.components)
        Me.lblLine = New System.Windows.Forms.Label()
        Me.picAutomobilElectric = New System.Windows.Forms.PictureBox()
        Me.gbArticle = New System.Windows.Forms.GroupBox()
        Me.Main_Title = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.btnArticle = New System.Windows.Forms.Button()
        Me.CBArticle = New System.Windows.Forms.ComboBox()
        Me.DG_Article = New System.Windows.Forms.DataGridView()
        Me.DG_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DG_Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lstMatchBox = New System.Windows.Forms.ListBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblRefPart = New System.Windows.Forms.Label()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.ListView_StationData = New System.Windows.Forms.ListView()
        Me.StationColumnHeader = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.grpCounter = New System.Windows.Forms.GroupBox()
        Me.Main_Count_Right = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.lbltotal = New System.Windows.Forms.Label()
        Me.Button_Reset = New System.Windows.Forms.Button()
        Me.Label_Pass = New System.Windows.Forms.Label()
        Me.lblPass = New System.Windows.Forms.Label()
        Me.Label_Total = New System.Windows.Forms.Label()
        Me.Label_Fail = New System.Windows.Forms.Label()
        Me.lblfail = New System.Windows.Forms.Label()
        Me.grpPicture = New System.Windows.Forms.GroupBox()
        Me.TabControlStations = New System.Windows.Forms.TabControl()
        Me.picBoxMain = New System.Windows.Forms.PictureBox()
        Me.ManLayout = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Main_Mid = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Main_Count = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.gbActualSerialNumber_01 = New System.Windows.Forms.GroupBox()
        Me.lblActualSerialNumber_01 = New System.Windows.Forms.Label()
        Me.MainMenu.SuspendLayout()
        CType(Me.picAutomobilElectric, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbArticle.SuspendLayout()
        Me.Main_Title.SuspendLayout()
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStatus.SuspendLayout()
        Me.grpCounter.SuspendLayout()
        Me.Main_Count_Right.SuspendLayout()
        Me.grpPicture.SuspendLayout()
        CType(Me.picBoxMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ManLayout.SuspendLayout()
        Me.Main_Mid.SuspendLayout()
        Me.Main_Count.SuspendLayout()
        Me.gbActualSerialNumber_01.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusForm
        '
        Me.StatusForm.BackColor = System.Drawing.Color.Silver
        Me.StatusForm.Location = New System.Drawing.Point(0, 899)
        Me.StatusForm.Name = "StatusForm"
        Me.StatusForm.Size = New System.Drawing.Size(1276, 22)
        Me.StatusForm.TabIndex = 0
        Me.StatusForm.Text = "StatusStrip1"
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem, Me.ShowWtDataToolStripMenuItem, Me.ShowCounterToolStripMenuItem, Me.ShowScheduleToolStripMenuItem, Me.ShowMaintainToolStripMenuItem, Me.ShowStationOverViewToolStripMenuItem, Me.ShowProductionViewToolStripMenuItem, Me.ShowErrorCodeViewToolStripMenuItem})
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
        'ShowCounterToolStripMenuItem
        '
        Me.ShowCounterToolStripMenuItem.Name = "ShowCounterToolStripMenuItem"
        Me.ShowCounterToolStripMenuItem.Size = New System.Drawing.Size(97, 21)
        Me.ShowCounterToolStripMenuItem.Text = "ShowCounter"
        '
        'ShowScheduleToolStripMenuItem
        '
        Me.ShowScheduleToolStripMenuItem.Name = "ShowScheduleToolStripMenuItem"
        Me.ShowScheduleToolStripMenuItem.Size = New System.Drawing.Size(103, 21)
        Me.ShowScheduleToolStripMenuItem.Text = "ShowSchedule"
        '
        'ShowMaintainToolStripMenuItem
        '
        Me.ShowMaintainToolStripMenuItem.Name = "ShowMaintainToolStripMenuItem"
        Me.ShowMaintainToolStripMenuItem.Size = New System.Drawing.Size(101, 21)
        Me.ShowMaintainToolStripMenuItem.Text = "ShowMaintain"
        '
        'ShowStationOverViewToolStripMenuItem
        '
        Me.ShowStationOverViewToolStripMenuItem.Name = "ShowStationOverViewToolStripMenuItem"
        Me.ShowStationOverViewToolStripMenuItem.Size = New System.Drawing.Size(146, 21)
        Me.ShowStationOverViewToolStripMenuItem.Text = "ShowStationOverView"
        '
        'ShowProductionViewToolStripMenuItem
        '
        Me.ShowProductionViewToolStripMenuItem.Name = "ShowProductionViewToolStripMenuItem"
        Me.ShowProductionViewToolStripMenuItem.Size = New System.Drawing.Size(141, 21)
        Me.ShowProductionViewToolStripMenuItem.Text = "ShowProductionView"
        '
        'ShowErrorCodeViewToolStripMenuItem
        '
        Me.ShowErrorCodeViewToolStripMenuItem.Name = "ShowErrorCodeViewToolStripMenuItem"
        Me.ShowErrorCodeViewToolStripMenuItem.Size = New System.Drawing.Size(139, 21)
        Me.ShowErrorCodeViewToolStripMenuItem.Text = "ShowErrorCodeView"
        '
        'tssLblKostal
        '
        Me.tssLblKostal.Image = Global.Kostal.Las.UserInterface.My.Resources.Resources.logo_screen_145px2
        Me.tssLblKostal.Name = "tssLblKostal"
        Me.tssLblKostal.Size = New System.Drawing.Size(70, 17)
        Me.tssLblKostal.Text = "KOSTAL"
        '
        'MainLogger
        '
        Me.MainLogger.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MainLogger.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLogger.FormattingEnabled = True
        Me.MainLogger.ItemHeight = 12
        Me.MainLogger.Location = New System.Drawing.Point(3, 750)
        Me.MainLogger.Name = "MainLogger"
        Me.MainLogger.Size = New System.Drawing.Size(1270, 121)
        Me.MainLogger.TabIndex = 23
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
        'SkinEngine1
        '
        Me.SkinEngine1.SerialNumber = ""
        Me.SkinEngine1.SkinFile = Nothing
        '
        'timCycle
        '
        Me.timCycle.Interval = 2000
        '
        'lblLine
        '
        Me.lblLine.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblLine.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblLine.Location = New System.Drawing.Point(3, 73)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(1270, 2)
        Me.lblLine.TabIndex = 16
        Me.lblLine.Text = "Label5"
        '
        'picAutomobilElectric
        '
        Me.picAutomobilElectric.BackColor = System.Drawing.Color.White
        Me.picAutomobilElectric.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.automation_controller
        Me.picAutomobilElectric.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picAutomobilElectric.Dock = System.Windows.Forms.DockStyle.Left
        Me.picAutomobilElectric.Location = New System.Drawing.Point(3, 29)
        Me.picAutomobilElectric.Name = "picAutomobilElectric"
        Me.picAutomobilElectric.Size = New System.Drawing.Size(281, 17)
        Me.picAutomobilElectric.TabIndex = 15
        Me.picAutomobilElectric.TabStop = False
        '
        'gbArticle
        '
        Me.gbArticle.Controls.Add(Me.Main_Title)
        Me.gbArticle.Controls.Add(Me.lstMatchBox)
        Me.gbArticle.Controls.Add(Me.lblMessage)
        Me.gbArticle.Controls.Add(Me.lblRefPart)
        Me.gbArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbArticle.Location = New System.Drawing.Point(3, 78)
        Me.gbArticle.Name = "gbArticle"
        Me.gbArticle.Size = New System.Drawing.Size(1270, 289)
        Me.gbArticle.TabIndex = 18
        Me.gbArticle.TabStop = False
        Me.gbArticle.Text = "Article"
        '
        'Main_Title
        '
        Me.Main_Title.ColumnCount = 2
        Me.Main_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.Main_Title.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.Main_Title.Controls.Add(Me.btnArticle, 1, 0)
        Me.Main_Title.Controls.Add(Me.CBArticle, 0, 0)
        Me.Main_Title.Controls.Add(Me.DG_Article, 0, 1)
        Me.Main_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Title.Location = New System.Drawing.Point(3, 17)
        Me.Main_Title.Name = "Main_Title"
        Me.Main_Title.RowCount = 2
        Me.Main_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.Main_Title.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.Main_Title.Size = New System.Drawing.Size(1264, 269)
        Me.Main_Title.TabIndex = 67
        '
        'btnArticle
        '
        Me.btnArticle.BackColor = System.Drawing.Color.White
        Me.btnArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnArticle.Location = New System.Drawing.Point(1137, 0)
        Me.btnArticle.Margin = New System.Windows.Forms.Padding(0)
        Me.btnArticle.Name = "btnArticle"
        Me.btnArticle.Size = New System.Drawing.Size(127, 26)
        Me.btnArticle.TabIndex = 4
        Me.btnArticle.Text = "btnArticle"
        Me.btnArticle.UseVisualStyleBackColor = False
        '
        'CBArticle
        '
        Me.CBArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CBArticle.FormattingEnabled = True
        Me.CBArticle.Location = New System.Drawing.Point(3, 3)
        Me.CBArticle.MaxDropDownItems = 20
        Me.CBArticle.Name = "CBArticle"
        Me.CBArticle.Size = New System.Drawing.Size(1131, 20)
        Me.CBArticle.TabIndex = 3
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
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Article.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DG_Article.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DG_Name, Me.DG_Value})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Article.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Article.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DG_Article.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DG_Article.Location = New System.Drawing.Point(3, 29)
        Me.DG_Article.Name = "DG_Article"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Article.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Article.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DG_Article.RowTemplate.Height = 23
        Me.DG_Article.Size = New System.Drawing.Size(1131, 237)
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
        'lstMatchBox
        '
        Me.lstMatchBox.FormattingEnabled = True
        Me.lstMatchBox.ItemHeight = 12
        Me.lstMatchBox.Location = New System.Drawing.Point(6, 37)
        Me.lstMatchBox.Name = "lstMatchBox"
        Me.lstMatchBox.Size = New System.Drawing.Size(160, 172)
        Me.lstMatchBox.TabIndex = 66
        Me.lstMatchBox.Visible = False
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Calibri", 50.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(6, 154)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(1127, 102)
        Me.lblMessage.TabIndex = 65
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRefPart
        '
        Me.lblRefPart.Font = New System.Drawing.Font("Calibri", 50.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefPart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRefPart.Location = New System.Drawing.Point(3, 40)
        Me.lblRefPart.Name = "lblRefPart"
        Me.lblRefPart.Size = New System.Drawing.Size(1261, 257)
        Me.lblRefPart.TabIndex = 65
        Me.lblRefPart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpStatus
        '
        Me.grpStatus.Controls.Add(Me.ListView_StationData)
        Me.grpStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpStatus.Location = New System.Drawing.Point(3, 3)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Size = New System.Drawing.Size(580, 306)
        Me.grpStatus.TabIndex = 19
        Me.grpStatus.TabStop = False
        '
        'ListView_StationData
        '
        Me.ListView_StationData.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.StationColumnHeader})
        Me.ListView_StationData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_StationData.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListView_StationData.FullRowSelect = True
        Me.ListView_StationData.GridLines = True
        Me.ListView_StationData.Location = New System.Drawing.Point(3, 17)
        Me.ListView_StationData.Name = "ListView_StationData"
        Me.ListView_StationData.Size = New System.Drawing.Size(574, 286)
        Me.ListView_StationData.TabIndex = 0
        Me.ListView_StationData.UseCompatibleStateImageBehavior = False
        Me.ListView_StationData.View = System.Windows.Forms.View.List
        '
        'grpCounter
        '
        Me.grpCounter.Controls.Add(Me.Main_Count_Right)
        Me.grpCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpCounter.Location = New System.Drawing.Point(589, 3)
        Me.grpCounter.Name = "grpCounter"
        Me.grpCounter.Size = New System.Drawing.Size(684, 59)
        Me.grpCounter.TabIndex = 25
        Me.grpCounter.TabStop = False
        Me.grpCounter.Text = "grpCounter"
        '
        'Main_Count_Right
        '
        Me.Main_Count_Right.ColumnCount = 7
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.389671!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.77934!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.389671!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.77934!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.389671!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.77934!))
        Me.Main_Count_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.49296!))
        Me.Main_Count_Right.Controls.Add(Me.lbltotal, 1, 0)
        Me.Main_Count_Right.Controls.Add(Me.Button_Reset, 6, 0)
        Me.Main_Count_Right.Controls.Add(Me.Label_Pass, 2, 0)
        Me.Main_Count_Right.Controls.Add(Me.lblPass, 3, 0)
        Me.Main_Count_Right.Controls.Add(Me.Label_Total, 0, 0)
        Me.Main_Count_Right.Controls.Add(Me.Label_Fail, 4, 0)
        Me.Main_Count_Right.Controls.Add(Me.lblfail, 5, 0)
        Me.Main_Count_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Count_Right.Location = New System.Drawing.Point(3, 17)
        Me.Main_Count_Right.Name = "Main_Count_Right"
        Me.Main_Count_Right.RowCount = 1
        Me.Main_Count_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Main_Count_Right.Size = New System.Drawing.Size(678, 39)
        Me.Main_Count_Right.TabIndex = 28
        '
        'lbltotal
        '
        Me.lbltotal.BackColor = System.Drawing.Color.White
        Me.lbltotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbltotal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbltotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbltotal.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lbltotal.Location = New System.Drawing.Point(66, 3)
        Me.lbltotal.Margin = New System.Windows.Forms.Padding(3)
        Me.lbltotal.Name = "lbltotal"
        Me.lbltotal.Size = New System.Drawing.Size(121, 33)
        Me.lbltotal.TabIndex = 0
        Me.lbltotal.Text = "0"
        Me.lbltotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button_Reset
        '
        Me.Button_Reset.BackColor = System.Drawing.Color.Transparent
        Me.Button_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Reset.Location = New System.Drawing.Point(573, 3)
        Me.Button_Reset.Name = "Button_Reset"
        Me.Button_Reset.Size = New System.Drawing.Size(102, 33)
        Me.Button_Reset.TabIndex = 27
        Me.Button_Reset.Text = "Reset"
        Me.Button_Reset.UseVisualStyleBackColor = False
        '
        'Label_Pass
        '
        Me.Label_Pass.AutoSize = True
        Me.Label_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Pass.Location = New System.Drawing.Point(193, 0)
        Me.Label_Pass.Name = "Label_Pass"
        Me.Label_Pass.Size = New System.Drawing.Size(57, 39)
        Me.Label_Pass.TabIndex = 25
        Me.Label_Pass.Text = "Pass:"
        Me.Label_Pass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPass
        '
        Me.lblPass.BackColor = System.Drawing.Color.White
        Me.lblPass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblPass.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPass.Location = New System.Drawing.Point(256, 3)
        Me.lblPass.Margin = New System.Windows.Forms.Padding(3)
        Me.lblPass.Name = "lblPass"
        Me.lblPass.Size = New System.Drawing.Size(121, 33)
        Me.lblPass.TabIndex = 0
        Me.lblPass.Text = "0"
        Me.lblPass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Total
        '
        Me.Label_Total.AutoSize = True
        Me.Label_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Total.Location = New System.Drawing.Point(3, 0)
        Me.Label_Total.Name = "Label_Total"
        Me.Label_Total.Size = New System.Drawing.Size(57, 39)
        Me.Label_Total.TabIndex = 2
        Me.Label_Total.Text = "Total:"
        Me.Label_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Fail
        '
        Me.Label_Fail.AutoSize = True
        Me.Label_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Fail.Location = New System.Drawing.Point(383, 0)
        Me.Label_Fail.Name = "Label_Fail"
        Me.Label_Fail.Size = New System.Drawing.Size(57, 39)
        Me.Label_Fail.TabIndex = 26
        Me.Label_Fail.Text = "Fail:"
        Me.Label_Fail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblfail
        '
        Me.lblfail.BackColor = System.Drawing.Color.White
        Me.lblfail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblfail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblfail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblfail.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfail.ForeColor = System.Drawing.Color.Red
        Me.lblfail.Location = New System.Drawing.Point(446, 3)
        Me.lblfail.Margin = New System.Windows.Forms.Padding(3)
        Me.lblfail.Name = "lblfail"
        Me.lblfail.Size = New System.Drawing.Size(121, 33)
        Me.lblfail.TabIndex = 1
        Me.lblfail.Text = "0"
        Me.lblfail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpPicture
        '
        Me.grpPicture.BackColor = System.Drawing.Color.Transparent
        Me.grpPicture.Controls.Add(Me.TabControlStations)
        Me.grpPicture.Controls.Add(Me.picBoxMain)
        Me.grpPicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpPicture.Location = New System.Drawing.Point(589, 3)
        Me.grpPicture.Name = "grpPicture"
        Me.grpPicture.Padding = New System.Windows.Forms.Padding(2)
        Me.grpPicture.Size = New System.Drawing.Size(684, 306)
        Me.grpPicture.TabIndex = 23
        Me.grpPicture.TabStop = False
        Me.grpPicture.Text = "GroupBox1"
        '
        'TabControlStations
        '
        Me.TabControlStations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlStations.Location = New System.Drawing.Point(2, 16)
        Me.TabControlStations.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControlStations.Name = "TabControlStations"
        Me.TabControlStations.Padding = New System.Drawing.Point(0, 0)
        Me.TabControlStations.SelectedIndex = 0
        Me.TabControlStations.Size = New System.Drawing.Size(680, 288)
        Me.TabControlStations.TabIndex = 29
        Me.TabControlStations.Visible = False
        '
        'picBoxMain
        '
        Me.picBoxMain.BackColor = System.Drawing.Color.White
        Me.picBoxMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picBoxMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picBoxMain.Location = New System.Drawing.Point(2, 16)
        Me.picBoxMain.Name = "picBoxMain"
        Me.picBoxMain.Size = New System.Drawing.Size(680, 288)
        Me.picBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picBoxMain.TabIndex = 20
        Me.picBoxMain.TabStop = False
        '
        'ManLayout
        '
        Me.ManLayout.BackColor = System.Drawing.Color.White
        Me.ManLayout.ColumnCount = 1
        Me.ManLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.ManLayout.Controls.Add(Me.picAutomobilElectric, 0, 1)
        Me.ManLayout.Controls.Add(Me.MainLogger, 0, 6)
        Me.ManLayout.Controls.Add(Me.lblLine, 0, 2)
        Me.ManLayout.Controls.Add(Me.gbArticle, 0, 3)
        Me.ManLayout.Controls.Add(Me.Main_Mid, 0, 4)
        Me.ManLayout.Controls.Add(Me.Main_Count, 0, 5)
        Me.ManLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ManLayout.Location = New System.Drawing.Point(0, 25)
        Me.ManLayout.Name = "ManLayout"
        Me.ManLayout.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ManLayout.RowCount = 7
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.061224!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.061224!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.69388!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.7347!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.653061!))
        Me.ManLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.79592!))
        Me.ManLayout.Size = New System.Drawing.Size(1276, 874)
        Me.ManLayout.TabIndex = 25
        '
        'Main_Mid
        '
        Me.Main_Mid.ColumnCount = 2
        Me.Main_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        Me.Main_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.0!))
        Me.Main_Mid.Controls.Add(Me.grpPicture, 1, 0)
        Me.Main_Mid.Controls.Add(Me.grpStatus, 0, 0)
        Me.Main_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Mid.Location = New System.Drawing.Point(0, 370)
        Me.Main_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.Main_Mid.Name = "Main_Mid"
        Me.Main_Mid.RowCount = 1
        Me.Main_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Main_Mid.Size = New System.Drawing.Size(1276, 312)
        Me.Main_Mid.TabIndex = 19
        '
        'Main_Count
        '
        Me.Main_Count.ColumnCount = 2
        Me.Main_Count.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        Me.Main_Count.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.0!))
        Me.Main_Count.Controls.Add(Me.gbActualSerialNumber_01, 0, 0)
        Me.Main_Count.Controls.Add(Me.grpCounter, 1, 0)
        Me.Main_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Count.Location = New System.Drawing.Point(0, 682)
        Me.Main_Count.Margin = New System.Windows.Forms.Padding(0)
        Me.Main_Count.Name = "Main_Count"
        Me.Main_Count.RowCount = 1
        Me.Main_Count.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.Main_Count.Size = New System.Drawing.Size(1276, 65)
        Me.Main_Count.TabIndex = 20
        '
        'gbActualSerialNumber_01
        '
        Me.gbActualSerialNumber_01.Controls.Add(Me.lblActualSerialNumber_01)
        Me.gbActualSerialNumber_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbActualSerialNumber_01.Location = New System.Drawing.Point(3, 3)
        Me.gbActualSerialNumber_01.Name = "gbActualSerialNumber_01"
        Me.gbActualSerialNumber_01.Size = New System.Drawing.Size(580, 59)
        Me.gbActualSerialNumber_01.TabIndex = 64
        Me.gbActualSerialNumber_01.TabStop = False
        Me.gbActualSerialNumber_01.Text = "gbActualSerialNumber_01"
        '
        'lblActualSerialNumber_01
        '
        Me.lblActualSerialNumber_01.BackColor = System.Drawing.Color.White
        Me.lblActualSerialNumber_01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActualSerialNumber_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblActualSerialNumber_01.Font = New System.Drawing.Font("Calibri", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblActualSerialNumber_01.Location = New System.Drawing.Point(3, 17)
        Me.lblActualSerialNumber_01.Margin = New System.Windows.Forms.Padding(3)
        Me.lblActualSerialNumber_01.Name = "lblActualSerialNumber_01"
        Me.lblActualSerialNumber_01.Size = New System.Drawing.Size(574, 39)
        Me.lblActualSerialNumber_01.TabIndex = 63
        Me.lblActualSerialNumber_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MainForm_Bosh
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(70, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1276, 921)
        Me.Controls.Add(Me.ManLayout)
        Me.Controls.Add(Me.MainMenu)
        Me.Controls.Add(Me.StatusForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm_Bosh"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "frmMain"
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        CType(Me.picAutomobilElectric, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbArticle.ResumeLayout(False)
        Me.Main_Title.ResumeLayout(False)
        CType(Me.DG_Article, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStatus.ResumeLayout(False)
        Me.grpCounter.ResumeLayout(False)
        Me.Main_Count_Right.ResumeLayout(False)
        Me.Main_Count_Right.PerformLayout()
        Me.grpPicture.ResumeLayout(False)
        CType(Me.picBoxMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ManLayout.ResumeLayout(False)
        Me.Main_Mid.ResumeLayout(False)
        Me.Main_Count.ResumeLayout(False)
        Me.gbActualSerialNumber_01.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents StatusForm As System.Windows.Forms.StatusStrip
    Public WithEvents MainMenu As System.Windows.Forms.MenuStrip
    Public WithEvents MainLogger As System.Windows.Forms.ListBox
    Public WithEvents tssLblKostal As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem12 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem13 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem14 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem16 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem17 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ToolStripMenuItem18 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MenuLanguage As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MenuChangePassword As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MenuExit As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ShowWtDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ShowCounterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents ShowScheduleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents SkinEngine1 As Sunisoft.IrisSkin.SkinEngine
    Public WithEvents timCycle As System.Windows.Forms.Timer
    Public WithEvents ShowMaintainToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowStationOverViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowErrorCodeViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowProductionViewToolStripMenuItem As ToolStripMenuItem
    Public WithEvents lblLine As Label
    Public WithEvents picAutomobilElectric As PictureBox
    Public WithEvents gbArticle As GroupBox
    Public WithEvents lstMatchBox As ListBox
    Public WithEvents DG_Article As DataGridView
    Public WithEvents DG_Name As DataGridViewTextBoxColumn
    Public WithEvents DG_Value As DataGridViewTextBoxColumn
    Public WithEvents lblMessage As Label
    Public WithEvents lblRefPart As Label
    Public WithEvents grpStatus As GroupBox
    Friend WithEvents ListView_StationData As ListView
    Friend WithEvents StationColumnHeader As ColumnHeader
    Public WithEvents grpCounter As GroupBox
    Public WithEvents Button_Reset As Button
    Public WithEvents Label_Fail As Label
    Public WithEvents Label_Pass As Label
    Public WithEvents Label_Total As Label
    Public WithEvents lbltotal As Label
    Public WithEvents lblfail As Label
    Public WithEvents lblPass As Label
    Public WithEvents grpPicture As GroupBox
    Public WithEvents TabControlStations As TabControl
    Public WithEvents picBoxMain As PictureBox
    Friend WithEvents ManLayout As Base.HMITableLayoutPanel
    Friend WithEvents Main_Mid As Base.HMITableLayoutPanel
    Friend WithEvents Main_Count As Base.HMITableLayoutPanel
    Public WithEvents gbActualSerialNumber_01 As GroupBox
    Public WithEvents lblActualSerialNumber_01 As Label
    Friend WithEvents Main_Title As Base.HMITableLayoutPanel
    Public WithEvents btnArticle As Button
    Public WithEvents CBArticle As ComboBox
    Friend WithEvents Main_Count_Right As Base.HMITableLayoutPanel
End Class
