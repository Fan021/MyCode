<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOLock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IOLock))
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Variant = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Variant_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Lock = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.ToolStrip_Lock = New System.Windows.Forms.ToolStrip()
        Me.ListView_Add = New System.Windows.Forms.ToolStripButton()
        Me.ListView_Del = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ListView_Up = New System.Windows.Forms.ToolStripButton()
        Me.ListView_Down = New System.Windows.Forms.ToolStripButton()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body_Variant.SuspendLayout()
        Me.TableLayoutPanel_Body_Variant_Mid.SuspendLayout()
        CType(Me.MachineListView_Lock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip_Lock.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.TableLayoutPanel_Body_Variant)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(533, 348)
        Me.Panel_UI.TabIndex = 0
        '
        'TableLayoutPanel_Body_Variant
        '
        Me.TableLayoutPanel_Body_Variant.ColumnCount = 1
        Me.TableLayoutPanel_Body_Variant.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Variant.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Variant.Controls.Add(Me.TableLayoutPanel_Body_Variant_Mid, 0, 0)
        Me.TableLayoutPanel_Body_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Variant.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Variant.Name = "TableLayoutPanel_Body_Variant"
        Me.TableLayoutPanel_Body_Variant.RowCount = 1
        Me.TableLayoutPanel_Body_Variant.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Variant.Size = New System.Drawing.Size(533, 348)
        Me.TableLayoutPanel_Body_Variant.TabIndex = 3
        '
        'TableLayoutPanel_Body_Variant_Mid
        '
        Me.TableLayoutPanel_Body_Variant_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Variant_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.Controls.Add(Me.MachineListView_Lock, 0, 1)
        Me.TableLayoutPanel_Body_Variant_Mid.Controls.Add(Me.ToolStrip_Lock, 0, 0)
        Me.TableLayoutPanel_Body_Variant_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Variant_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Variant_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Variant_Mid.Name = "TableLayoutPanel_Body_Variant_Mid"
        Me.TableLayoutPanel_Body_Variant_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Variant_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.Size = New System.Drawing.Size(533, 348)
        Me.TableLayoutPanel_Body_Variant_Mid.TabIndex = 0
        '
        'MachineListView_Lock
        '
        Me.MachineListView_Lock.AllowUserToAddRows = False
        Me.MachineListView_Lock.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Lock.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Lock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Lock.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Lock.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Lock.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Lock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Lock.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Lock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Lock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Lock.EnableHeadersVisualStyles = False
        Me.MachineListView_Lock.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Lock.Location = New System.Drawing.Point(0, 34)
        Me.MachineListView_Lock.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Lock.Name = "MachineListView_Lock"
        Me.MachineListView_Lock.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Lock.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Lock.RowTemplate.Height = 23
        Me.MachineListView_Lock.Size = New System.Drawing.Size(533, 314)
        Me.MachineListView_Lock.TabIndex = 12
        '
        'ToolStrip_Lock
        '
        Me.ToolStrip_Lock.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip_Lock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip_Lock.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListView_Add, Me.ListView_Del, Me.ToolStripSeparator1, Me.ListView_Up, Me.ListView_Down})
        Me.ToolStrip_Lock.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Lock.Name = "ToolStrip_Lock"
        Me.ToolStrip_Lock.Size = New System.Drawing.Size(533, 34)
        Me.ToolStrip_Lock.TabIndex = 11
        '
        'ListView_Add
        '
        Me.ListView_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Add.Image = CType(resources.GetObject("ListView_Add.Image"), System.Drawing.Image)
        Me.ListView_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Add.Name = "ListView_Add"
        Me.ListView_Add.Size = New System.Drawing.Size(23, 31)
        Me.ListView_Add.ToolTipText = "add a new command"
        '
        'ListView_Del
        '
        Me.ListView_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Del.Image = CType(resources.GetObject("ListView_Del.Image"), System.Drawing.Image)
        Me.ListView_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Del.Name = "ListView_Del"
        Me.ListView_Del.Size = New System.Drawing.Size(23, 31)
        Me.ListView_Del.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 34)
        '
        'ListView_Up
        '
        Me.ListView_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Up.Image = CType(resources.GetObject("ListView_Up.Image"), System.Drawing.Image)
        Me.ListView_Up.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Up.Name = "ListView_Up"
        Me.ListView_Up.Size = New System.Drawing.Size(23, 31)
        Me.ListView_Up.ToolTipText = "move up"
        '
        'ListView_Down
        '
        Me.ListView_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Down.Image = CType(resources.GetObject("ListView_Down.Image"), System.Drawing.Image)
        Me.ListView_Down.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Down.Name = "ListView_Down"
        Me.ListView_Down.Size = New System.Drawing.Size(23, 31)
        Me.ListView_Down.ToolTipText = "move down"
        '
        'IOLock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 348)
        Me.Controls.Add(Me.Panel_UI)
        Me.Name = "IOLock"
        Me.Text = "IOLock"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant_Mid.PerformLayout()
        CType(Me.MachineListView_Lock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip_Lock.ResumeLayout(False)
        Me.ToolStrip_Lock.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Variant As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Variant_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_Lock As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents ToolStrip_Lock As System.Windows.Forms.ToolStrip
    Friend WithEvents ListView_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ListView_Up As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView_Down As System.Windows.Forms.ToolStripButton
End Class
