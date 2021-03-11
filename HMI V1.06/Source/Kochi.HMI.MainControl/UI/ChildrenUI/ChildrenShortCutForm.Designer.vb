Imports Kochi.HMI.MainControl.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenShortCutForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChildrenShortCutForm))
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_Devices = New System.Windows.Forms.TabControl()
        Me.ShortCut = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_ShortCut = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_ShortCut_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.ListView_Info = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.ListView_Data = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.PostToolBar = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator_Station = New System.Windows.Forms.ToolStripSeparator()
        Me.TableLayoutPanel_Body_ShortCut_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add_Info = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del_Info = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Up_Info = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Down_Info = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabControl_Devices.SuspendLayout()
        Me.ShortCut.SuspendLayout()
        Me.TableLayoutPanel_Body_ShortCut.SuspendLayout()
        Me.TableLayoutPanel_Body_ShortCut_Mid.SuspendLayout()
        CType(Me.ListView_Info, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PostToolBar.SuspendLayout()
        Me.TableLayoutPanel_Body_ShortCut_Right.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.Color.Transparent
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_Devices, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabControl_Devices
        '
        Me.TabControl_Devices.Controls.Add(Me.ShortCut)
        Me.TabControl_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Devices.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_Devices.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Devices.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Devices.Name = "TabControl_Devices"
        Me.TabControl_Devices.SelectedIndex = 0
        Me.TabControl_Devices.Size = New System.Drawing.Size(467, 530)
        Me.TabControl_Devices.TabIndex = 0
        '
        'ShortCut
        '
        Me.ShortCut.Controls.Add(Me.TableLayoutPanel_Body_ShortCut)
        Me.ShortCut.Location = New System.Drawing.Point(4, 28)
        Me.ShortCut.Name = "ShortCut"
        Me.ShortCut.Size = New System.Drawing.Size(459, 498)
        Me.ShortCut.TabIndex = 0
        Me.ShortCut.Text = "ShortCut"
        Me.ShortCut.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_ShortCut
        '
        Me.TableLayoutPanel_Body_ShortCut.ColumnCount = 2
        Me.TableLayoutPanel_Body_ShortCut.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Body_ShortCut.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_ShortCut.Controls.Add(Me.TableLayoutPanel_Body_ShortCut_Mid, 0, 1)
        Me.TableLayoutPanel_Body_ShortCut.Controls.Add(Me.TableLayoutPanel_Body_ShortCut_Right, 1, 1)
        Me.TableLayoutPanel_Body_ShortCut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_ShortCut.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_ShortCut.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_ShortCut.Name = "TableLayoutPanel_Body_ShortCut"
        Me.TableLayoutPanel_Body_ShortCut.RowCount = 2
        Me.TableLayoutPanel_Body_ShortCut.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body_ShortCut.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body_ShortCut.Size = New System.Drawing.Size(459, 498)
        Me.TableLayoutPanel_Body_ShortCut.TabIndex = 0
        '
        'TableLayoutPanel_Body_ShortCut_Mid
        '
        Me.TableLayoutPanel_Body_ShortCut_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_ShortCut_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.Controls.Add(Me.ToolStrip1, 0, 2)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Controls.Add(Me.ListView_Info, 0, 3)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Controls.Add(Me.ListView_Data, 0, 1)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Controls.Add(Me.PostToolBar, 0, 0)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_ShortCut_Mid.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_ShortCut_Mid.Name = "TableLayoutPanel_Body_ShortCut_Mid"
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowCount = 4
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_ShortCut_Mid.Size = New System.Drawing.Size(275, 474)
        Me.TableLayoutPanel_Body_ShortCut_Mid.TabIndex = 0
        '
        'ListView_Info
        '
        Me.ListView_Info.AllowUserToAddRows = False
        Me.ListView_Info.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListView_Info.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.ListView_Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ListView_Info.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ListView_Info.BackgroundColor = System.Drawing.Color.White
        Me.ListView_Info.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ListView_Info.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.ListView_Info.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.ListView_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ListView_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Info.EnableHeadersVisualStyles = False
        Me.ListView_Info.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ListView_Info.Location = New System.Drawing.Point(0, 264)
        Me.ListView_Info.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView_Info.Name = "ListView_Info"
        Me.ListView_Info.RowHeadersVisible = False
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListView_Info.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.ListView_Info.RowTemplate.Height = 23
        Me.ListView_Info.Size = New System.Drawing.Size(275, 210)
        Me.ListView_Info.TabIndex = 14
        '
        'ListView_Data
        '
        Me.ListView_Data.AllowUserToAddRows = False
        Me.ListView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.ListView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ListView_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ListView_Data.BackgroundColor = System.Drawing.Color.White
        Me.ListView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ListView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.ListView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.ListView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ListView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView_Data.EnableHeadersVisualStyles = False
        Me.ListView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.ListView_Data.Location = New System.Drawing.Point(0, 28)
        Me.ListView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.ListView_Data.Name = "ListView_Data"
        Me.ListView_Data.RowHeadersVisible = False
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ListView_Data.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.ListView_Data.RowTemplate.Height = 23
        Me.ListView_Data.Size = New System.Drawing.Size(275, 208)
        Me.ListView_Data.TabIndex = 12
        '
        'PostToolBar
        '
        Me.PostToolBar.BackColor = System.Drawing.SystemColors.Control
        Me.PostToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PostToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add, Me.PostTest_Del, Me.ToolStripSeparator_Station})
        Me.PostToolBar.Location = New System.Drawing.Point(0, 0)
        Me.PostToolBar.Name = "PostToolBar"
        Me.PostToolBar.Size = New System.Drawing.Size(275, 28)
        Me.PostToolBar.TabIndex = 11
        '
        'PostTest_Add
        '
        Me.PostTest_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add.Image = CType(resources.GetObject("PostTest_Add.Image"), System.Drawing.Image)
        Me.PostTest_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add.Name = "PostTest_Add"
        Me.PostTest_Add.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Add.ToolTipText = "add a new command"
        '
        'PostTest_Del
        '
        Me.PostTest_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del.Image = CType(resources.GetObject("PostTest_Del.Image"), System.Drawing.Image)
        Me.PostTest_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del.Name = "PostTest_Del"
        Me.PostTest_Del.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Del.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator_Station
        '
        Me.ToolStripSeparator_Station.Name = "ToolStripSeparator_Station"
        Me.ToolStripSeparator_Station.Size = New System.Drawing.Size(6, 28)
        '
        'TableLayoutPanel_Body_ShortCut_Right
        '
        Me.TableLayoutPanel_Body_ShortCut_Right.ColumnCount = 1
        Me.TableLayoutPanel_Body_ShortCut_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_ShortCut_Right.Controls.Add(Me.TableLayoutPanel_Right, 0, 0)
        Me.TableLayoutPanel_Body_ShortCut_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_ShortCut_Right.Location = New System.Drawing.Point(278, 27)
        Me.TableLayoutPanel_Body_ShortCut_Right.Name = "TableLayoutPanel_Body_ShortCut_Right"
        Me.TableLayoutPanel_Body_ShortCut_Right.RowCount = 2
        Me.TableLayoutPanel_Body_ShortCut_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body_ShortCut_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel_Body_ShortCut_Right.Size = New System.Drawing.Size(178, 468)
        Me.TableLayoutPanel_Body_ShortCut_Right.TabIndex = 1
        '
        'TableLayoutPanel_Right
        '
        Me.TableLayoutPanel_Right.ColumnCount = 1
        Me.TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Right.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Right.Name = "TableLayoutPanel_Right"
        Me.TableLayoutPanel_Right.RowCount = 1
        Me.TableLayoutPanel_Body_ShortCut_Right.SetRowSpan(Me.TableLayoutPanel_Right, 2)
        Me.TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Right.Size = New System.Drawing.Size(178, 468)
        Me.TableLayoutPanel_Right.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add_Info, Me.PostTest_Del_Info, Me.PostTest_Up_Info, Me.PostTest_Down_Info, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 236)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(275, 28)
        Me.ToolStrip1.TabIndex = 15
        '
        'PostTest_Add_Info
        '
        Me.PostTest_Add_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add_Info.Image = CType(resources.GetObject("PostTest_Add_Info.Image"), System.Drawing.Image)
        Me.PostTest_Add_Info.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add_Info.Name = "PostTest_Add_Info"
        Me.PostTest_Add_Info.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Add_Info.ToolTipText = "add a new command"
        '
        'PostTest_Del_Info
        '
        Me.PostTest_Del_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del_Info.Image = CType(resources.GetObject("PostTest_Del_Info.Image"), System.Drawing.Image)
        Me.PostTest_Del_Info.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del_Info.Name = "PostTest_Del_Info"
        Me.PostTest_Del_Info.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Del_Info.ToolTipText = "delete selected command"
        '
        'PostTest_Up_Info
        '
        Me.PostTest_Up_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Up_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Up_Info.Image = CType(resources.GetObject("PostTest_Up_Info.Image"), System.Drawing.Image)
        Me.PostTest_Up_Info.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Up_Info.Name = "PostTest_Up_Info"
        Me.PostTest_Up_Info.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Up_Info.ToolTipText = "move up"
        '
        'PostTest_Down_Info
        '
        Me.PostTest_Down_Info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Down_Info.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Down_Info.Image = CType(resources.GetObject("PostTest_Down_Info.Image"), System.Drawing.Image)
        Me.PostTest_Down_Info.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Down_Info.Name = "PostTest_Down_Info"
        Me.PostTest_Down_Info.Size = New System.Drawing.Size(23, 25)
        Me.PostTest_Down_Info.ToolTipText = "move down"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 28)
        '
        'ChildrenShortCutForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenShortCutForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenShortCutForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_Devices.ResumeLayout(False)
        Me.ShortCut.ResumeLayout(False)
        Me.TableLayoutPanel_Body_ShortCut.ResumeLayout(False)
        Me.TableLayoutPanel_Body_ShortCut_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_ShortCut_Mid.PerformLayout()
        CType(Me.ListView_Info, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PostToolBar.ResumeLayout(False)
        Me.PostToolBar.PerformLayout()
        Me.TableLayoutPanel_Body_ShortCut_Right.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl_Devices As System.Windows.Forms.TabControl
    Friend WithEvents TableLayoutPanel_Body_ShortCut As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_ShortCut_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PostToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator_Station As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ListView_Data As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents ShortCut As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_ShortCut_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ListView_Info As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Up_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Down_Info As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
