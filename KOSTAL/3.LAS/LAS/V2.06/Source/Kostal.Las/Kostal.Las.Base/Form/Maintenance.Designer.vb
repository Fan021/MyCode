<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Maintenance
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Maintenance))
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.设备维护 = New System.Windows.Forms.GroupBox()
        Me.DG_Maintain = New System.Windows.Forms.DataGridView()
        Me.Station = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Component = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HintUpLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlarmUpLimit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HintMessage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AlarmMessage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reset = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.Panel_Body.SuspendLayout()
        Me.设备维护.SuspendLayout()
        CType(Me.DG_Maintain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.设备维护)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(865, 359)
        Me.Panel_Body.TabIndex = 0
        '
        '设备维护
        '
        Me.设备维护.Controls.Add(Me.DG_Maintain)
        Me.设备维护.Controls.Add(Me.ToolStrip1)
        Me.设备维护.Location = New System.Drawing.Point(7, 6)
        Me.设备维护.Name = "设备维护"
        Me.设备维护.Size = New System.Drawing.Size(850, 346)
        Me.设备维护.TabIndex = 2
        Me.设备维护.TabStop = False
        Me.设备维护.Text = "LAS Maintenance"
        '
        'DG_Maintain
        '
        Me.DG_Maintain.AllowUserToAddRows = False
        Me.DG_Maintain.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Maintain.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DG_Maintain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DG_Maintain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Station, Me.Component, Me.HintUpLimit, Me.AlarmUpLimit, Me.HintMessage, Me.AlarmMessage, Me.Count, Me.Reset})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DG_Maintain.DefaultCellStyle = DataGridViewCellStyle2
        Me.DG_Maintain.Location = New System.Drawing.Point(6, 45)
        Me.DG_Maintain.Name = "DG_Maintain"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DG_Maintain.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DG_Maintain.RowTemplate.Height = 23
        Me.DG_Maintain.Size = New System.Drawing.Size(845, 301)
        Me.DG_Maintain.TabIndex = 2
        '
        'Station
        '
        Me.Station.HeaderText = "Station"
        Me.Station.Name = "Station"
        '
        'Component
        '
        Me.Component.HeaderText = "Component"
        Me.Component.Name = "Component"
        '
        'HintUpLimit
        '
        Me.HintUpLimit.HeaderText = "HintUpLimit"
        Me.HintUpLimit.Name = "HintUpLimit"
        '
        'AlarmUpLimit
        '
        Me.AlarmUpLimit.HeaderText = "AlarmUpLimit"
        Me.AlarmUpLimit.Name = "AlarmUpLimit"
        '
        'HintMessage
        '
        Me.HintMessage.HeaderText = "HintMessage"
        Me.HintMessage.Name = "HintMessage"
        '
        'AlarmMessage
        '
        Me.AlarmMessage.HeaderText = "AlarmMessage"
        Me.AlarmMessage.Name = "AlarmMessage"
        '
        'Count
        '
        Me.Count.HeaderText = "Count"
        Me.Count.Name = "Count"
        Me.Count.ReadOnly = True
        '
        'Reset
        '
        Me.Reset.HeaderText = "Reset"
        Me.Reset.Name = "Reset"
        Me.Reset.Text = "Reset"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 17)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(844, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'Maintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(865, 359)
        Me.Controls.Add(Me.Panel_Body)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Maintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LAS Maintenance"
        Me.Panel_Body.ResumeLayout(False)
        Me.设备维护.ResumeLayout(False)
        Me.设备维护.PerformLayout()
        CType(Me.DG_Maintain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel_Body As Windows.Forms.Panel
    Friend WithEvents 设备维护 As Windows.Forms.GroupBox
    Friend WithEvents DG_Maintain As Windows.Forms.DataGridView
    Friend WithEvents Station As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Component As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HintUpLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlarmUpLimit As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HintMessage As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AlarmMessage As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Count As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Reset As Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents ToolStrip1 As Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As Windows.Forms.ToolStripButton
End Class
