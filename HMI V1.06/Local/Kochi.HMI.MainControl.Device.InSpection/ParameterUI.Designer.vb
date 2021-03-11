<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParameterUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParameterUI))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TabControl_Parameter = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Vision = New System.Windows.Forms.TableLayoutPanel()
        Me.PostToolBar = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add_Vision = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del_Vision = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator_Station = New System.Windows.Forms.ToolStripSeparator()
        Me.TableLayoutPanel_Sensor = New System.Windows.Forms.TableLayoutPanel()
        Me.Sensor = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add_Sensor = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del_Sensor = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MachineListView_Vision = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.MachineListView_Sensor = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.Pandel_Body.SuspendLayout()
        Me.TabControl_Parameter.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel_Vision.SuspendLayout()
        Me.PostToolBar.SuspendLayout()
        Me.TableLayoutPanel_Sensor.SuspendLayout()
        Me.Sensor.SuspendLayout()
        CType(Me.MachineListView_Vision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MachineListView_Sensor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TabControl_Parameter)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(453, 413)
        Me.Pandel_Body.TabIndex = 0
        '
        'TabControl_Parameter
        '
        Me.TabControl_Parameter.Controls.Add(Me.TabPage1)
        Me.TabControl_Parameter.Controls.Add(Me.TabPage2)
        Me.TabControl_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Parameter.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Parameter.Name = "TabControl_Parameter"
        Me.TabControl_Parameter.SelectedIndex = 0
        Me.TabControl_Parameter.Size = New System.Drawing.Size(453, 413)
        Me.TabControl_Parameter.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_Vision)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(445, 387)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Vision"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel_Sensor)
        Me.TabPage2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(445, 387)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Sensor"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Vision
        '
        Me.TableLayoutPanel_Vision.ColumnCount = 1
        Me.TableLayoutPanel_Vision.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Vision.Controls.Add(Me.MachineListView_Vision, 0, 1)
        Me.TableLayoutPanel_Vision.Controls.Add(Me.PostToolBar, 0, 0)
        Me.TableLayoutPanel_Vision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Vision.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Vision.Name = "TableLayoutPanel_Vision"
        Me.TableLayoutPanel_Vision.RowCount = 2
        Me.TableLayoutPanel_Vision.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Vision.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel_Vision.Size = New System.Drawing.Size(439, 381)
        Me.TableLayoutPanel_Vision.TabIndex = 0
        '
        'PostToolBar
        '
        Me.PostToolBar.BackColor = System.Drawing.SystemColors.Control
        Me.PostToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PostToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add_Vision, Me.PostTest_Del_Vision, Me.ToolStripSeparator_Station})
        Me.PostToolBar.Location = New System.Drawing.Point(0, 0)
        Me.PostToolBar.Name = "PostToolBar"
        Me.PostToolBar.Size = New System.Drawing.Size(439, 22)
        Me.PostToolBar.TabIndex = 11
        '
        'PostTest_Add_Vision
        '
        Me.PostTest_Add_Vision.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add_Vision.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add_Vision.Image = CType(resources.GetObject("PostTest_Add_Vision.Image"), System.Drawing.Image)
        Me.PostTest_Add_Vision.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add_Vision.Name = "PostTest_Add_Vision"
        Me.PostTest_Add_Vision.Size = New System.Drawing.Size(23, 19)
        Me.PostTest_Add_Vision.ToolTipText = "add a new command"
        '
        'PostTest_Del_Vision
        '
        Me.PostTest_Del_Vision.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del_Vision.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del_Vision.Image = CType(resources.GetObject("PostTest_Del_Vision.Image"), System.Drawing.Image)
        Me.PostTest_Del_Vision.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del_Vision.Name = "PostTest_Del_Vision"
        Me.PostTest_Del_Vision.Size = New System.Drawing.Size(23, 19)
        Me.PostTest_Del_Vision.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator_Station
        '
        Me.ToolStripSeparator_Station.Name = "ToolStripSeparator_Station"
        Me.ToolStripSeparator_Station.Size = New System.Drawing.Size(6, 22)
        '
        'TableLayoutPanel_Sensor
        '
        Me.TableLayoutPanel_Sensor.ColumnCount = 1
        Me.TableLayoutPanel_Sensor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Sensor.Controls.Add(Me.MachineListView_Sensor, 0, 1)
        Me.TableLayoutPanel_Sensor.Controls.Add(Me.Sensor, 0, 0)
        Me.TableLayoutPanel_Sensor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Sensor.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Sensor.Name = "TableLayoutPanel_Sensor"
        Me.TableLayoutPanel_Sensor.RowCount = 2
        Me.TableLayoutPanel_Sensor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Sensor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel_Sensor.Size = New System.Drawing.Size(439, 381)
        Me.TableLayoutPanel_Sensor.TabIndex = 1
        '
        'Sensor
        '
        Me.Sensor.BackColor = System.Drawing.SystemColors.Control
        Me.Sensor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Sensor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add_Sensor, Me.PostTest_Del_Sensor, Me.ToolStripSeparator1})
        Me.Sensor.Location = New System.Drawing.Point(0, 0)
        Me.Sensor.Name = "Sensor"
        Me.Sensor.Size = New System.Drawing.Size(439, 22)
        Me.Sensor.TabIndex = 11
        '
        'PostTest_Add_Sensor
        '
        Me.PostTest_Add_Sensor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add_Sensor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add_Sensor.Image = CType(resources.GetObject("PostTest_Add_Sensor.Image"), System.Drawing.Image)
        Me.PostTest_Add_Sensor.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add_Sensor.Name = "PostTest_Add_Sensor"
        Me.PostTest_Add_Sensor.Size = New System.Drawing.Size(23, 19)
        Me.PostTest_Add_Sensor.ToolTipText = "add a new command"
        '
        'PostTest_Del_Sensor
        '
        Me.PostTest_Del_Sensor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del_Sensor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del_Sensor.Image = CType(resources.GetObject("PostTest_Del_Sensor.Image"), System.Drawing.Image)
        Me.PostTest_Del_Sensor.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del_Sensor.Name = "PostTest_Del_Sensor"
        Me.PostTest_Del_Sensor.Size = New System.Drawing.Size(23, 19)
        Me.PostTest_Del_Sensor.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 22)
        '
        'MachineListView_Vision
        '
        Me.MachineListView_Vision.AllowUserToAddRows = False
        Me.MachineListView_Vision.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Vision.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Vision.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Vision.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Vision.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Vision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Vision.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Vision.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Vision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Vision.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Vision.EnableHeadersVisualStyles = False
        Me.MachineListView_Vision.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Vision.Location = New System.Drawing.Point(0, 22)
        Me.MachineListView_Vision.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Vision.Name = "MachineListView_Vision"
        Me.MachineListView_Vision.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Vision.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Vision.RowTemplate.Height = 23
        Me.MachineListView_Vision.Size = New System.Drawing.Size(439, 359)
        Me.MachineListView_Vision.TabIndex = 14
        '
        'MachineListView_Sensor
        '
        Me.MachineListView_Sensor.AllowUserToAddRows = False
        Me.MachineListView_Sensor.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Sensor.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.MachineListView_Sensor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Sensor.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Sensor.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Sensor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Sensor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Sensor.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.MachineListView_Sensor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Sensor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Sensor.EnableHeadersVisualStyles = False
        Me.MachineListView_Sensor.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Sensor.Location = New System.Drawing.Point(0, 22)
        Me.MachineListView_Sensor.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Sensor.Name = "MachineListView_Sensor"
        Me.MachineListView_Sensor.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Sensor.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.MachineListView_Sensor.RowTemplate.Height = 23
        Me.MachineListView_Sensor.Size = New System.Drawing.Size(439, 359)
        Me.MachineListView_Sensor.TabIndex = 14
        '
        'ParameterUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 413)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ParameterUI"
        Me.Text = "ParameterUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TabControl_Parameter.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel_Vision.ResumeLayout(False)
        Me.TableLayoutPanel_Vision.PerformLayout()
        Me.PostToolBar.ResumeLayout(False)
        Me.PostToolBar.PerformLayout()
        Me.TableLayoutPanel_Sensor.ResumeLayout(False)
        Me.TableLayoutPanel_Sensor.PerformLayout()
        Me.Sensor.ResumeLayout(False)
        Me.Sensor.PerformLayout()
        CType(Me.MachineListView_Vision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MachineListView_Sensor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TabControl_Parameter As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Vision As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PostToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add_Vision As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del_Vision As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator_Station As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TableLayoutPanel_Sensor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Sensor As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add_Sensor As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del_Sensor As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MachineListView_Vision As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents MachineListView_Sensor As Kochi.HMI.MainControl.UI.MachineListView
End Class
