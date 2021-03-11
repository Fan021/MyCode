<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddForm))
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Sensor = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Type_Value = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.Sensor = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Sensor.SuspendLayout()
        CType(Me.MachineListView_Type_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Sensor.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.TableLayoutPanel_Sensor)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(491, 409)
        Me.Panel_UI.TabIndex = 0
        '
        'TableLayoutPanel_Sensor
        '
        Me.TableLayoutPanel_Sensor.ColumnCount = 1
        Me.TableLayoutPanel_Sensor.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Sensor.Controls.Add(Me.MachineListView_Type_Value, 0, 1)
        Me.TableLayoutPanel_Sensor.Controls.Add(Me.Sensor, 0, 0)
        Me.TableLayoutPanel_Sensor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Sensor.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Sensor.Name = "TableLayoutPanel_Sensor"
        Me.TableLayoutPanel_Sensor.RowCount = 2
        Me.TableLayoutPanel_Sensor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Sensor.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel_Sensor.Size = New System.Drawing.Size(491, 409)
        Me.TableLayoutPanel_Sensor.TabIndex = 2
        '
        'MachineListView_Type_Value
        '
        Me.MachineListView_Type_Value.AllowUserToAddRows = False
        Me.MachineListView_Type_Value.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Type_Value.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Type_Value.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Type_Value.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Type_Value.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Type_Value.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Type_Value.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Type_Value.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Type_Value.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Type_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Type_Value.EnableHeadersVisualStyles = False
        Me.MachineListView_Type_Value.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Type_Value.Location = New System.Drawing.Point(0, 24)
        Me.MachineListView_Type_Value.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Type_Value.Name = "MachineListView_Type_Value"
        Me.MachineListView_Type_Value.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Type_Value.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Type_Value.RowTemplate.Height = 23
        Me.MachineListView_Type_Value.Size = New System.Drawing.Size(491, 385)
        Me.MachineListView_Type_Value.TabIndex = 14
        '
        'Sensor
        '
        Me.Sensor.BackColor = System.Drawing.SystemColors.Control
        Me.Sensor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Sensor.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add, Me.PostTest_Del, Me.ToolStripSeparator1})
        Me.Sensor.Location = New System.Drawing.Point(0, 0)
        Me.Sensor.Name = "Sensor"
        Me.Sensor.Size = New System.Drawing.Size(491, 24)
        Me.Sensor.TabIndex = 11
        '
        'PostTest_Add
        '
        Me.PostTest_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add.Image = CType(resources.GetObject("PostTest_Add.Image"), System.Drawing.Image)
        Me.PostTest_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add.Name = "PostTest_Add"
        Me.PostTest_Add.Size = New System.Drawing.Size(23, 21)
        Me.PostTest_Add.ToolTipText = "add a new command"
        '
        'PostTest_Del
        '
        Me.PostTest_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del.Image = CType(resources.GetObject("PostTest_Del.Image"), System.Drawing.Image)
        Me.PostTest_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del.Name = "PostTest_Del"
        Me.PostTest_Del.Size = New System.Drawing.Size(23, 21)
        Me.PostTest_Del.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 24)
        '
        'AddForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 409)
        Me.Controls.Add(Me.Panel_UI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "AddForm"
        Me.Text = "AddForm"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Sensor.ResumeLayout(False)
        Me.TableLayoutPanel_Sensor.PerformLayout()
        CType(Me.MachineListView_Type_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Sensor.ResumeLayout(False)
        Me.Sensor.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Sensor As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_Type_Value As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents Sensor As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
