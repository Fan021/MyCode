Imports Kostal.Las.Base

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm_Mul
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm_Mul))
        Me.swTimer = New System.Windows.Forms.Timer(Me.components)
        Me.timCycle = New System.Windows.Forms.Timer(Me.components)
        Me.QRKostal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.KostalInc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblLc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LCStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblReference = New System.Windows.Forms.ToolStripStatusLabel()
        Me.RefStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblCAQ = New System.Windows.Forms.ToolStripStatusLabel()
        Me.CAQ = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tslblPLC = New System.Windows.Forms.ToolStripStatusLabel()
        Me.PLC = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusForm = New System.Windows.Forms.StatusStrip()
        Me.ToolVersion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.panCounter = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_head_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.lblSnMessage = New System.Windows.Forms.Label()
        Me.lblActualSerialNumber_01 = New System.Windows.Forms.Label()
        Me.lblCurrentArticle = New System.Windows.Forms.Label()
        Me.btnCurrentSchedule = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnCurrentArticle = New System.Windows.Forms.Button()
        Me.lblCurrentSchedule = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.TableLayoutPanel_Head_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.picKostal = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Mid_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTableLayoutPanel_Infor = New Kostal.Las.Base.HMITableLayoutPanel()
        Me.Label_Level_Infor = New System.Windows.Forms.Label()
        Me.Label_User_Infor = New System.Windows.Forms.Label()
        Me.Label_Custorm_Infor = New System.Windows.Forms.Label()
        Me.Label_SW_Infor = New System.Windows.Forms.Label()
        Me.Label_HW_Infor = New System.Windows.Forms.Label()
        Me.Label_Index_Infor = New System.Windows.Forms.Label()
        Me.Label_Level = New System.Windows.Forms.Label()
        Me.Label_User = New System.Windows.Forms.Label()
        Me.Label_Customer = New System.Windows.Forms.Label()
        Me.Label_SW = New System.Windows.Forms.Label()
        Me.Label_HW = New System.Windows.Forms.Label()
        Me.Label_Index = New System.Windows.Forms.Label()
        Me.Label_Name_Infor = New System.Windows.Forms.Label()
        Me.Label_Name = New System.Windows.Forms.Label()
        Me.Label_Article_Infor = New System.Windows.Forms.Label()
        Me.Label_Article = New System.Windows.Forms.Label()
        Me.btnShortCut = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnDebug = New System.Windows.Forms.Button()
        Me.btnStation = New System.Windows.Forms.Button()
        Me.btnSystem = New System.Windows.Forms.Button()
        Me.TableLayoutPanel_Mid_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.panMessage = New System.Windows.Forms.Panel()
        Me.panMain = New System.Windows.Forms.Panel()
        Me.StatusForm.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Head.SuspendLayout()
        Me.TableLayoutPanel_head_Right.SuspendLayout()
        Me.TableLayoutPanel_Head_Left.SuspendLayout()
        CType(Me.picKostal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Mid_Left.SuspendLayout()
        Me.HmiTableLayoutPanel_Infor.SuspendLayout()
        Me.TableLayoutPanel_Mid_Right.SuspendLayout()
        Me.SuspendLayout()
        '
        'timCycle
        '
        Me.timCycle.Interval = 2000
        '
        'QRKostal
        '
        Me.QRKostal.AutoSize = False
        Me.QRKostal.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.QR_Kostal
        Me.QRKostal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.QRKostal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.QRKostal.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.QRKostal.Name = "QRKostal"
        Me.QRKostal.Size = New System.Drawing.Size(50, 44)
        Me.QRKostal.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'KostalInc
        '
        Me.KostalInc.AutoSize = False
        Me.KostalInc.Name = "KostalInc"
        Me.KostalInc.Size = New System.Drawing.Size(30, 44)
        '
        'tslblLc
        '
        Me.tslblLc.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslblLc.Name = "tslblLc"
        Me.tslblLc.Size = New System.Drawing.Size(83, 44)
        Me.tslblLc.Text = "Linecontrol"
        '
        'LCStatus
        '
        Me.LCStatus.AutoSize = False
        Me.LCStatus.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.green
        Me.LCStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.LCStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.LCStatus.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.LCStatus.Name = "LCStatus"
        Me.LCStatus.Size = New System.Drawing.Size(50, 44)
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.AutoSize = False
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(40, 44)
        Me.ToolStripStatusLabel3.Text = "       "
        '
        'tslblReference
        '
        Me.tslblReference.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslblReference.Name = "tslblReference"
        Me.tslblReference.Size = New System.Drawing.Size(76, 44)
        Me.tslblReference.Text = "Reference"
        '
        'RefStatus
        '
        Me.RefStatus.AutoSize = False
        Me.RefStatus.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.green
        Me.RefStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RefStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.RefStatus.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.RefStatus.Name = "RefStatus"
        Me.RefStatus.Size = New System.Drawing.Size(50, 44)
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.AutoSize = False
        Me.ToolStripStatusLabel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(49, 44)
        '
        'tslblCAQ
        '
        Me.tslblCAQ.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.tslblCAQ.Name = "tslblCAQ"
        Me.tslblCAQ.Size = New System.Drawing.Size(38, 44)
        Me.tslblCAQ.Text = "CAQ"
        '
        'CAQ
        '
        Me.CAQ.AutoSize = False
        Me.CAQ.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.gray
        Me.CAQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CAQ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CAQ.Name = "CAQ"
        Me.CAQ.Size = New System.Drawing.Size(49, 44)
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.AutoSize = False
        Me.ToolStripStatusLabel7.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(49, 44)
        '
        'tslblPLC
        '
        Me.tslblPLC.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tslblPLC.Name = "tslblPLC"
        Me.tslblPLC.Size = New System.Drawing.Size(33, 44)
        Me.tslblPLC.Text = "PLC"
        '
        'PLC
        '
        Me.PLC.AutoSize = False
        Me.PLC.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.gray
        Me.PLC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PLC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PLC.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PLC.Name = "PLC"
        Me.PLC.Size = New System.Drawing.Size(50, 44)
        Me.PLC.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'StatusForm
        '
        Me.StatusForm.AutoSize = False
        Me.StatusForm.BackColor = System.Drawing.Color.WhiteSmoke
        Me.StatusForm.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QRKostal, Me.KostalInc, Me.tslblLc, Me.LCStatus, Me.ToolStripStatusLabel3, Me.tslblReference, Me.RefStatus, Me.ToolStripStatusLabel5, Me.tslblCAQ, Me.CAQ, Me.ToolStripStatusLabel7, Me.tslblPLC, Me.PLC, Me.ToolVersion})
        Me.StatusForm.Location = New System.Drawing.Point(0, 764)
        Me.StatusForm.Name = "StatusForm"
        Me.StatusForm.Size = New System.Drawing.Size(924, 49)
        Me.StatusForm.SizingGrip = False
        Me.StatusForm.Stretch = False
        Me.StatusForm.TabIndex = 1
        Me.StatusForm.Text = "StatusStrip1"
        '
        'ToolVersion
        '
        Me.ToolVersion.Font = New System.Drawing.Font("Calibri", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.ToolVersion.Name = "ToolVersion"
        Me.ToolVersion.Size = New System.Drawing.Size(151, 44)
        Me.ToolVersion.Text = "ToolStripStatusLabel1"
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.panCounter, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Mid, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(924, 764)
        Me.TableLayoutPanel_Body.TabIndex = 7
        '
        'panCounter
        '
        Me.panCounter.BackColor = System.Drawing.Color.White
        Me.panCounter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panCounter.Location = New System.Drawing.Point(0, 709)
        Me.panCounter.Margin = New System.Windows.Forms.Padding(0)
        Me.panCounter.Name = "panCounter"
        Me.panCounter.Size = New System.Drawing.Size(924, 55)
        Me.panCounter.TabIndex = 3
        '
        'TableLayoutPanel_Head
        '
        Me.TableLayoutPanel_Head.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Head.ColumnCount = 2
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Head.Controls.Add(Me.TableLayoutPanel_head_Right, 0, 0)
        Me.TableLayoutPanel_Head.Controls.Add(Me.TableLayoutPanel_Head_Left, 1, 0)
        Me.TableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head.Name = "TableLayoutPanel_Head"
        Me.TableLayoutPanel_Head.RowCount = 1
        Me.TableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Head.Size = New System.Drawing.Size(924, 114)
        Me.TableLayoutPanel_Head.TabIndex = 0
        '
        'TableLayoutPanel_head_Right
        '
        Me.TableLayoutPanel_head_Right.ColumnCount = 3
        Me.TableLayoutPanel_head_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_head_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_head_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_head_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.btnAuto, 0, 1)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.lblSnMessage, 1, 2)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.lblActualSerialNumber_01, 2, 2)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.lblCurrentArticle, 2, 0)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.btnCurrentSchedule, 1, 1)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.btnClear, 0, 2)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.btnCurrentArticle, 1, 0)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.lblCurrentSchedule, 2, 1)
        Me.TableLayoutPanel_head_Right.Controls.Add(Me.btnStart, 0, 0)
        Me.TableLayoutPanel_head_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_head_Right.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_head_Right.Name = "TableLayoutPanel_head_Right"
        Me.TableLayoutPanel_head_Right.Padding = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel_head_Right.RowCount = 3
        Me.TableLayoutPanel_head_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.26072!))
        Me.TableLayoutPanel_head_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.26072!))
        Me.TableLayoutPanel_head_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.47856!))
        Me.TableLayoutPanel_head_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_head_Right.Size = New System.Drawing.Size(733, 108)
        Me.TableLayoutPanel_head_Right.TabIndex = 1
        '
        'btnAuto
        '
        Me.btnAuto.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnAuto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnAuto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAuto.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnAuto.Location = New System.Drawing.Point(3, 37)
        Me.btnAuto.Margin = New System.Windows.Forms.Padding(1)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(241, 32)
        Me.btnAuto.TabIndex = 18
        Me.btnAuto.Text = "自动"
        Me.btnAuto.UseVisualStyleBackColor = False
        '
        'lblSnMessage
        '
        Me.lblSnMessage.AutoSize = True
        Me.lblSnMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSnMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSnMessage.Font = New System.Drawing.Font("Consolas", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblSnMessage.ForeColor = System.Drawing.Color.Black
        Me.lblSnMessage.Location = New System.Drawing.Point(247, 72)
        Me.lblSnMessage.Margin = New System.Windows.Forms.Padding(2)
        Me.lblSnMessage.Name = "lblSnMessage"
        Me.lblSnMessage.Size = New System.Drawing.Size(239, 32)
        Me.lblSnMessage.TabIndex = 17
        Me.lblSnMessage.Text = "当前序列号"
        Me.lblSnMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblActualSerialNumber_01
        '
        Me.lblActualSerialNumber_01.AutoSize = True
        Me.lblActualSerialNumber_01.BackColor = System.Drawing.Color.White
        Me.lblActualSerialNumber_01.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActualSerialNumber_01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblActualSerialNumber_01.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblActualSerialNumber_01.ForeColor = System.Drawing.Color.Blue
        Me.lblActualSerialNumber_01.Location = New System.Drawing.Point(490, 72)
        Me.lblActualSerialNumber_01.Margin = New System.Windows.Forms.Padding(2)
        Me.lblActualSerialNumber_01.Name = "lblActualSerialNumber_01"
        Me.lblActualSerialNumber_01.Size = New System.Drawing.Size(239, 32)
        Me.lblActualSerialNumber_01.TabIndex = 16
        Me.lblActualSerialNumber_01.Text = "---"
        Me.lblActualSerialNumber_01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCurrentArticle
        '
        Me.lblCurrentArticle.AutoSize = True
        Me.lblCurrentArticle.BackColor = System.Drawing.Color.White
        Me.lblCurrentArticle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentArticle.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblCurrentArticle.ForeColor = System.Drawing.Color.Blue
        Me.lblCurrentArticle.Location = New System.Drawing.Point(490, 4)
        Me.lblCurrentArticle.Margin = New System.Windows.Forms.Padding(2)
        Me.lblCurrentArticle.Name = "lblCurrentArticle"
        Me.lblCurrentArticle.Size = New System.Drawing.Size(239, 30)
        Me.lblCurrentArticle.TabIndex = 14
        Me.lblCurrentArticle.Text = "---"
        Me.lblCurrentArticle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCurrentSchedule
        '
        Me.btnCurrentSchedule.BackColor = System.Drawing.SystemColors.Control
        Me.btnCurrentSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCurrentSchedule.Font = New System.Drawing.Font("Consolas", 13.0!, System.Drawing.FontStyle.Bold)
        Me.btnCurrentSchedule.Location = New System.Drawing.Point(246, 37)
        Me.btnCurrentSchedule.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCurrentSchedule.Name = "btnCurrentSchedule"
        Me.btnCurrentSchedule.Size = New System.Drawing.Size(241, 32)
        Me.btnCurrentSchedule.TabIndex = 13
        Me.btnCurrentSchedule.Text = "测试模式"
        Me.btnCurrentSchedule.UseVisualStyleBackColor = False
        '
        'btnClear
        '
        Me.btnClear.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnClear.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnClear.Location = New System.Drawing.Point(3, 71)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(241, 34)
        Me.btnClear.TabIndex = 11
        Me.btnClear.Text = "清盘"
        Me.btnClear.UseVisualStyleBackColor = False
        '
        'btnCurrentArticle
        '
        Me.btnCurrentArticle.BackColor = System.Drawing.SystemColors.Control
        Me.btnCurrentArticle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCurrentArticle.Font = New System.Drawing.Font("Consolas", 13.0!, System.Drawing.FontStyle.Bold)
        Me.btnCurrentArticle.Location = New System.Drawing.Point(246, 3)
        Me.btnCurrentArticle.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCurrentArticle.Name = "btnCurrentArticle"
        Me.btnCurrentArticle.Size = New System.Drawing.Size(241, 32)
        Me.btnCurrentArticle.TabIndex = 2
        Me.btnCurrentArticle.Text = "当前变种"
        Me.btnCurrentArticle.UseVisualStyleBackColor = False
        '
        'lblCurrentSchedule
        '
        Me.lblCurrentSchedule.AutoSize = True
        Me.lblCurrentSchedule.BackColor = System.Drawing.Color.White
        Me.lblCurrentSchedule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCurrentSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentSchedule.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblCurrentSchedule.ForeColor = System.Drawing.Color.Blue
        Me.lblCurrentSchedule.Location = New System.Drawing.Point(490, 38)
        Me.lblCurrentSchedule.Margin = New System.Windows.Forms.Padding(2)
        Me.lblCurrentSchedule.Name = "lblCurrentSchedule"
        Me.lblCurrentSchedule.Size = New System.Drawing.Size(239, 30)
        Me.lblCurrentSchedule.TabIndex = 5
        Me.lblCurrentSchedule.Text = "---"
        Me.lblCurrentSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnStart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStart.Font = New System.Drawing.Font("Consolas", 15.0!, System.Drawing.FontStyle.Bold)
        Me.btnStart.Location = New System.Drawing.Point(3, 3)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(1)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(241, 32)
        Me.btnStart.TabIndex = 0
        Me.btnStart.Text = "开始"
        Me.btnStart.UseVisualStyleBackColor = False
        '
        'TableLayoutPanel_Head_Left
        '
        Me.TableLayoutPanel_Head_Left.ColumnCount = 1
        Me.TableLayoutPanel_Head_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Head_Left.Controls.Add(Me.lblDate, 0, 1)
        Me.TableLayoutPanel_Head_Left.Controls.Add(Me.lblTime, 0, 2)
        Me.TableLayoutPanel_Head_Left.Controls.Add(Me.picKostal, 0, 0)
        Me.TableLayoutPanel_Head_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Left.Location = New System.Drawing.Point(739, 0)
        Me.TableLayoutPanel_Head_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Head_Left.Name = "TableLayoutPanel_Head_Left"
        Me.TableLayoutPanel_Head_Left.RowCount = 3
        Me.TableLayoutPanel_Head_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Head_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Head_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Head_Left.Size = New System.Drawing.Size(185, 114)
        Me.TableLayoutPanel_Head_Left.TabIndex = 0
        '
        'lblDate
        '
        Me.lblDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDate.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDate.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblDate.Location = New System.Drawing.Point(0, 79)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(185, 17)
        Me.lblDate.TabIndex = 19
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTime
        '
        Me.lblTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTime.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTime.Location = New System.Drawing.Point(0, 96)
        Me.lblTime.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(185, 18)
        Me.lblTime.TabIndex = 18
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picKostal
        '
        Me.picKostal.BackColor = System.Drawing.SystemColors.Control
        Me.picKostal.BackgroundImage = Global.Kostal.Las.UserInterface.My.Resources.Resources.Kostal
        Me.picKostal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picKostal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picKostal.Location = New System.Drawing.Point(0, 0)
        Me.picKostal.Margin = New System.Windows.Forms.Padding(0)
        Me.picKostal.Name = "picKostal"
        Me.picKostal.Size = New System.Drawing.Size(185, 79)
        Me.picKostal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picKostal.TabIndex = 16
        Me.picKostal.TabStop = False
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 2
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Mid_Left, 0, 0)
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Mid_Right, 1, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 114)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(924, 595)
        Me.TableLayoutPanel_Mid.TabIndex = 1
        '
        'TableLayoutPanel_Mid_Left
        '
        Me.TableLayoutPanel_Mid_Left.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Mid_Left.ColumnCount = 1
        Me.TableLayoutPanel_Mid_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.HmiTableLayoutPanel_Infor, 0, 7)
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.btnShortCut, 0, 4)
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.btnLogin, 0, 3)
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.btnDebug, 0, 2)
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.btnStation, 0, 1)
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.btnSystem, 0, 0)
        Me.TableLayoutPanel_Mid_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid_Left.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Mid_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid_Left.Name = "TableLayoutPanel_Mid_Left"
        Me.TableLayoutPanel_Mid_Left.RowCount = 8
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel_Mid_Left.Size = New System.Drawing.Size(184, 595)
        Me.TableLayoutPanel_Mid_Left.TabIndex = 6
        '
        'HmiTableLayoutPanel_Infor
        '
        Me.HmiTableLayoutPanel_Infor.ColumnCount = 2
        Me.HmiTableLayoutPanel_Infor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Infor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Level_Infor, 1, 7)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_User_Infor, 1, 6)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Custorm_Infor, 1, 5)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_SW_Infor, 1, 4)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_HW_Infor, 1, 3)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Index_Infor, 1, 2)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Level, 0, 7)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_User, 0, 6)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Customer, 0, 5)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_SW, 0, 4)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_HW, 0, 3)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Index, 0, 2)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Name_Infor, 1, 1)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Name, 0, 1)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Article_Infor, 1, 0)
        Me.HmiTableLayoutPanel_Infor.Controls.Add(Me.Label_Article, 0, 0)
        Me.HmiTableLayoutPanel_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Infor.Location = New System.Drawing.Point(3, 416)
        Me.HmiTableLayoutPanel_Infor.Name = "HmiTableLayoutPanel_Infor"
        Me.HmiTableLayoutPanel_Infor.RowCount = 8
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Infor.Size = New System.Drawing.Size(178, 176)
        Me.HmiTableLayoutPanel_Infor.TabIndex = 14
        '
        'Label_Level_Infor
        '
        Me.Label_Level_Infor.AutoSize = True
        Me.Label_Level_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Level_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Level_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Level_Infor.Location = New System.Drawing.Point(92, 157)
        Me.Label_Level_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Level_Infor.Name = "Label_Level_Infor"
        Me.Label_Level_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_Level_Infor.TabIndex = 15
        Me.Label_Level_Infor.Text = "--"
        Me.Label_Level_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_User_Infor
        '
        Me.Label_User_Infor.AutoSize = True
        Me.Label_User_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_User_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_User_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_User_Infor.Location = New System.Drawing.Point(92, 135)
        Me.Label_User_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_User_Infor.Name = "Label_User_Infor"
        Me.Label_User_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_User_Infor.TabIndex = 14
        Me.Label_User_Infor.Text = "--"
        Me.Label_User_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Custorm_Infor
        '
        Me.Label_Custorm_Infor.AutoSize = True
        Me.Label_Custorm_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Custorm_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Custorm_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Custorm_Infor.Location = New System.Drawing.Point(92, 113)
        Me.Label_Custorm_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Custorm_Infor.Name = "Label_Custorm_Infor"
        Me.Label_Custorm_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_Custorm_Infor.TabIndex = 13
        Me.Label_Custorm_Infor.Text = "--"
        Me.Label_Custorm_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_SW_Infor
        '
        Me.Label_SW_Infor.AutoSize = True
        Me.Label_SW_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_SW_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_SW_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_SW_Infor.Location = New System.Drawing.Point(92, 91)
        Me.Label_SW_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_SW_Infor.Name = "Label_SW_Infor"
        Me.Label_SW_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_SW_Infor.TabIndex = 12
        Me.Label_SW_Infor.Text = "--"
        Me.Label_SW_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_HW_Infor
        '
        Me.Label_HW_Infor.AutoSize = True
        Me.Label_HW_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_HW_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_HW_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_HW_Infor.Location = New System.Drawing.Point(92, 69)
        Me.Label_HW_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_HW_Infor.Name = "Label_HW_Infor"
        Me.Label_HW_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_HW_Infor.TabIndex = 11
        Me.Label_HW_Infor.Text = "--"
        Me.Label_HW_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Index_Infor
        '
        Me.Label_Index_Infor.AutoSize = True
        Me.Label_Index_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Index_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Index_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Index_Infor.Location = New System.Drawing.Point(92, 47)
        Me.Label_Index_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Index_Infor.Name = "Label_Index_Infor"
        Me.Label_Index_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_Index_Infor.TabIndex = 10
        Me.Label_Index_Infor.Text = "--"
        Me.Label_Index_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Level
        '
        Me.Label_Level.AutoSize = True
        Me.Label_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Level.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Level.Location = New System.Drawing.Point(3, 157)
        Me.Label_Level.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Level.Name = "Label_Level"
        Me.Label_Level.Size = New System.Drawing.Size(83, 16)
        Me.Label_Level.TabIndex = 9
        Me.Label_Level.Text = "等级:"
        Me.Label_Level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_User
        '
        Me.Label_User.AutoSize = True
        Me.Label_User.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_User.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_User.Location = New System.Drawing.Point(3, 135)
        Me.Label_User.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_User.Name = "Label_User"
        Me.Label_User.Size = New System.Drawing.Size(83, 16)
        Me.Label_User.TabIndex = 8
        Me.Label_User.Text = "用户:"
        Me.Label_User.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Customer
        '
        Me.Label_Customer.AutoSize = True
        Me.Label_Customer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Customer.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Customer.Location = New System.Drawing.Point(3, 113)
        Me.Label_Customer.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Customer.Name = "Label_Customer"
        Me.Label_Customer.Size = New System.Drawing.Size(83, 16)
        Me.Label_Customer.TabIndex = 7
        Me.Label_Customer.Text = "客户号:"
        Me.Label_Customer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_SW
        '
        Me.Label_SW.AutoSize = True
        Me.Label_SW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_SW.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_SW.Location = New System.Drawing.Point(3, 91)
        Me.Label_SW.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_SW.Name = "Label_SW"
        Me.Label_SW.Size = New System.Drawing.Size(83, 16)
        Me.Label_SW.TabIndex = 6
        Me.Label_SW.Text = "软件版本:"
        Me.Label_SW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_HW
        '
        Me.Label_HW.AutoSize = True
        Me.Label_HW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_HW.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_HW.Location = New System.Drawing.Point(3, 69)
        Me.Label_HW.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_HW.Name = "Label_HW"
        Me.Label_HW.Size = New System.Drawing.Size(83, 16)
        Me.Label_HW.TabIndex = 5
        Me.Label_HW.Text = "硬件版本:"
        Me.Label_HW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Index
        '
        Me.Label_Index.AutoSize = True
        Me.Label_Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Index.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Index.Location = New System.Drawing.Point(3, 47)
        Me.Label_Index.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Index.Name = "Label_Index"
        Me.Label_Index.Size = New System.Drawing.Size(83, 16)
        Me.Label_Index.TabIndex = 4
        Me.Label_Index.Text = "版本号:"
        Me.Label_Index.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Name_Infor
        '
        Me.Label_Name_Infor.AutoSize = True
        Me.Label_Name_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Name_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Name_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Name_Infor.Location = New System.Drawing.Point(92, 25)
        Me.Label_Name_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Name_Infor.Name = "Label_Name_Infor"
        Me.Label_Name_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_Name_Infor.TabIndex = 3
        Me.Label_Name_Infor.Text = "--"
        Me.Label_Name_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Name
        '
        Me.Label_Name.AutoSize = True
        Me.Label_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Name.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Name.Location = New System.Drawing.Point(3, 25)
        Me.Label_Name.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Name.Name = "Label_Name"
        Me.Label_Name.Size = New System.Drawing.Size(83, 16)
        Me.Label_Name.TabIndex = 2
        Me.Label_Name.Text = "名称:"
        Me.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Article_Infor
        '
        Me.Label_Article_Infor.AutoSize = True
        Me.Label_Article_Infor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label_Article_Infor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Article_Infor.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Article_Infor.Location = New System.Drawing.Point(92, 3)
        Me.Label_Article_Infor.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Article_Infor.Name = "Label_Article_Infor"
        Me.Label_Article_Infor.Size = New System.Drawing.Size(83, 16)
        Me.Label_Article_Infor.TabIndex = 1
        Me.Label_Article_Infor.Text = "--"
        Me.Label_Article_Infor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Article
        '
        Me.Label_Article.AutoSize = True
        Me.Label_Article.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Article.Font = New System.Drawing.Font("Calibri", 8.0!)
        Me.Label_Article.Location = New System.Drawing.Point(3, 3)
        Me.Label_Article.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Article.Name = "Label_Article"
        Me.Label_Article.Size = New System.Drawing.Size(83, 16)
        Me.Label_Article.TabIndex = 0
        Me.Label_Article.Text = "变种:"
        Me.Label_Article.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnShortCut
        '
        Me.btnShortCut.BackColor = System.Drawing.Color.White
        Me.btnShortCut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnShortCut.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShortCut.Location = New System.Drawing.Point(3, 239)
        Me.btnShortCut.Name = "btnShortCut"
        Me.btnShortCut.Size = New System.Drawing.Size(178, 53)
        Me.btnShortCut.TabIndex = 12
        Me.btnShortCut.Text = "快捷"
        Me.btnShortCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnShortCut.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.White
        Me.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLogin.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.Location = New System.Drawing.Point(3, 180)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(178, 53)
        Me.btnLogin.TabIndex = 11
        Me.btnLogin.Text = "登陆"
        Me.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnDebug
        '
        Me.btnDebug.BackColor = System.Drawing.Color.White
        Me.btnDebug.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDebug.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDebug.Location = New System.Drawing.Point(3, 121)
        Me.btnDebug.Name = "btnDebug"
        Me.btnDebug.Size = New System.Drawing.Size(178, 53)
        Me.btnDebug.TabIndex = 10
        Me.btnDebug.Text = "调试"
        Me.btnDebug.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDebug.UseVisualStyleBackColor = True
        '
        'btnStation
        '
        Me.btnStation.BackColor = System.Drawing.Color.White
        Me.btnStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnStation.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStation.Location = New System.Drawing.Point(3, 62)
        Me.btnStation.Name = "btnStation"
        Me.btnStation.Size = New System.Drawing.Size(178, 53)
        Me.btnStation.TabIndex = 8
        Me.btnStation.Text = "工站"
        Me.btnStation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnStation.UseVisualStyleBackColor = True
        '
        'btnSystem
        '
        Me.btnSystem.BackColor = System.Drawing.Color.White
        Me.btnSystem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSystem.Font = New System.Drawing.Font("Consolas", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSystem.Location = New System.Drawing.Point(3, 3)
        Me.btnSystem.Name = "btnSystem"
        Me.btnSystem.Size = New System.Drawing.Size(178, 53)
        Me.btnSystem.TabIndex = 3
        Me.btnSystem.Text = "系统"
        Me.btnSystem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSystem.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Mid_Right
        '
        Me.TableLayoutPanel_Mid_Right.ColumnCount = 1
        Me.TableLayoutPanel_Mid_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid_Right.Controls.Add(Me.panMessage, 0, 1)
        Me.TableLayoutPanel_Mid_Right.Controls.Add(Me.panMain, 0, 0)
        Me.TableLayoutPanel_Mid_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid_Right.Location = New System.Drawing.Point(184, 0)
        Me.TableLayoutPanel_Mid_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid_Right.Name = "TableLayoutPanel_Mid_Right"
        Me.TableLayoutPanel_Mid_Right.RowCount = 2
        Me.TableLayoutPanel_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Mid_Right.Size = New System.Drawing.Size(740, 595)
        Me.TableLayoutPanel_Mid_Right.TabIndex = 7
        '
        'panMessage
        '
        Me.panMessage.BackColor = System.Drawing.Color.White
        Me.panMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panMessage.Location = New System.Drawing.Point(0, 476)
        Me.panMessage.Margin = New System.Windows.Forms.Padding(0)
        Me.panMessage.Name = "panMessage"
        Me.panMessage.Size = New System.Drawing.Size(740, 119)
        Me.panMessage.TabIndex = 4
        '
        'panMain
        '
        Me.panMain.BackColor = System.Drawing.Color.White
        Me.panMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panMain.Location = New System.Drawing.Point(0, 0)
        Me.panMain.Margin = New System.Windows.Forms.Padding(0)
        Me.panMain.Name = "panMain"
        Me.panMain.Size = New System.Drawing.Size(740, 476)
        Me.panMain.TabIndex = 3
        '
        'MainForm_Mul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 813)
        Me.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Controls.Add(Me.StatusForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainForm_Mul"
        Me.Text = "LAS of Table SRC"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusForm.ResumeLayout(False)
        Me.StatusForm.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Head.ResumeLayout(False)
        Me.TableLayoutPanel_head_Right.ResumeLayout(False)
        Me.TableLayoutPanel_head_Right.PerformLayout()
        Me.TableLayoutPanel_Head_Left.ResumeLayout(False)
        CType(Me.picKostal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Mid_Left.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Infor.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Infor.PerformLayout()
        Me.TableLayoutPanel_Mid_Right.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents swTimer As Timer
    Public WithEvents timCycle As Timer
    Public WithEvents QRKostal As ToolStripStatusLabel
    Public WithEvents KostalInc As ToolStripStatusLabel
    Public WithEvents tslblLc As ToolStripStatusLabel
    Public WithEvents LCStatus As ToolStripStatusLabel
    Public WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Public WithEvents tslblReference As ToolStripStatusLabel
    Public WithEvents RefStatus As ToolStripStatusLabel
    Public WithEvents ToolStripStatusLabel5 As ToolStripStatusLabel
    Public WithEvents tslblCAQ As ToolStripStatusLabel
    Public WithEvents CAQ As ToolStripStatusLabel
    Public WithEvents ToolStripStatusLabel7 As ToolStripStatusLabel
    Public WithEvents tslblPLC As ToolStripStatusLabel
    Public WithEvents PLC As ToolStripStatusLabel
    Public WithEvents StatusForm As StatusStrip
    Public WithEvents TableLayoutPanel_Body As TableLayoutPanel
    Public WithEvents panCounter As Panel
    Public WithEvents TableLayoutPanel_Head As TableLayoutPanel
    Public WithEvents TableLayoutPanel_head_Right As TableLayoutPanel
    Public WithEvents btnAuto As Button
    Public WithEvents lblSnMessage As Label
    Public WithEvents lblActualSerialNumber_01 As Label
    Public WithEvents lblCurrentArticle As Label
    Public WithEvents btnCurrentSchedule As Button
    Public WithEvents btnClear As Button
    Public WithEvents btnCurrentArticle As Button
    Public WithEvents lblCurrentSchedule As Label
    Public WithEvents btnStart As Button
    Public WithEvents TableLayoutPanel_Head_Left As TableLayoutPanel
    Public WithEvents lblDate As Label
    Public WithEvents lblTime As Label
    Public WithEvents picKostal As PictureBox
    Public WithEvents TableLayoutPanel_Mid As TableLayoutPanel
    Public WithEvents TableLayoutPanel_Mid_Left As TableLayoutPanel
    Public WithEvents HmiTableLayoutPanel_Infor As HMITableLayoutPanel
    Public WithEvents Label_Level_Infor As Label
    Public WithEvents Label_User_Infor As Label
    Public WithEvents Label_Custorm_Infor As Label
    Public WithEvents Label_SW_Infor As Label
    Public WithEvents Label_HW_Infor As Label
    Public WithEvents Label_Index_Infor As Label
    Public WithEvents Label_Level As Label
    Public WithEvents Label_User As Label
    Public WithEvents Label_Customer As Label
    Public WithEvents Label_SW As Label
    Public WithEvents Label_HW As Label
    Public WithEvents Label_Index As Label
    Public WithEvents Label_Name_Infor As Label
    Public WithEvents Label_Name As Label
    Public WithEvents Label_Article_Infor As Label
    Public WithEvents Label_Article As Label
    Public WithEvents btnShortCut As Button
    Public WithEvents btnLogin As Button
    Public WithEvents btnDebug As Button
    Public WithEvents btnStation As Button
    Public WithEvents btnSystem As Button
    Public WithEvents TableLayoutPanel_Mid_Right As TableLayoutPanel
    Public WithEvents panMessage As Panel
    Public WithEvents panMain As Panel
    Public WithEvents ToolVersion As ToolStripStatusLabel
End Class
