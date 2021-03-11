<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActionUI
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ActionUI))
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Name = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Picture = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.PostToolBar = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator_Station = New System.Windows.Forms.ToolStripSeparator()
        Me.TableLayoutPanel_Body_Mid_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiButton_Position = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Position = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Position = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Picture = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Picture = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Picture = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Bottom.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.MachineListView_Picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PostToolBar.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 361)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Name, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Top, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'HmiLabel_Name
        '
        Me.HmiLabel_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Name.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Name.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Name.Name = "HmiLabel_Name"
        Me.HmiLabel_Name.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_Name.TabIndex = 5
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 0)
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 40)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 320)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 2
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.MachineListView_Picture, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.PostToolBar, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_Right, 1, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(298, 320)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'MachineListView_Picture
        '
        Me.MachineListView_Picture.AllowUserToAddRows = False
        Me.MachineListView_Picture.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Picture.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Picture.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Picture.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Picture.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Picture.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Picture.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Picture.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Picture.EnableHeadersVisualStyles = False
        Me.MachineListView_Picture.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Picture.Location = New System.Drawing.Point(0, 32)
        Me.MachineListView_Picture.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Picture.Name = "MachineListView_Picture"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MachineListView_Picture.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Picture.RowHeadersVisible = False
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Picture.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.MachineListView_Picture.RowTemplate.Height = 23
        Me.MachineListView_Picture.Size = New System.Drawing.Size(238, 288)
        Me.MachineListView_Picture.TabIndex = 13
        '
        'PostToolBar
        '
        Me.PostToolBar.BackColor = System.Drawing.SystemColors.Control
        Me.PostToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PostToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add, Me.PostTest_Del, Me.ToolStripSeparator_Station})
        Me.PostToolBar.Location = New System.Drawing.Point(0, 0)
        Me.PostToolBar.Name = "PostToolBar"
        Me.PostToolBar.Size = New System.Drawing.Size(238, 32)
        Me.PostToolBar.TabIndex = 10
        '
        'PostTest_Add
        '
        Me.PostTest_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add.Image = CType(resources.GetObject("PostTest_Add.Image"), System.Drawing.Image)
        Me.PostTest_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add.Name = "PostTest_Add"
        Me.PostTest_Add.Size = New System.Drawing.Size(23, 29)
        Me.PostTest_Add.ToolTipText = "add a new command"
        '
        'PostTest_Del
        '
        Me.PostTest_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del.Image = CType(resources.GetObject("PostTest_Del.Image"), System.Drawing.Image)
        Me.PostTest_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del.Name = "PostTest_Del"
        Me.PostTest_Del.Size = New System.Drawing.Size(23, 29)
        Me.PostTest_Del.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator_Station
        '
        Me.ToolStripSeparator_Station.Name = "ToolStripSeparator_Station"
        Me.ToolStripSeparator_Station.Size = New System.Drawing.Size(6, 32)
        '
        'TableLayoutPanel_Body_Mid_Right
        '
        Me.TableLayoutPanel_Body_Mid_Right.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiButton_Position, 0, 6)
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiTextBox_Position, 0, 5)
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiLabel_Position, 0, 4)
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiLabel_Picture, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiTextBox_Picture, 0, 2)
        Me.TableLayoutPanel_Body_Mid_Right.Controls.Add(Me.HmiButton_Picture, 0, 3)
        Me.TableLayoutPanel_Body_Mid_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Right.Location = New System.Drawing.Point(238, 0)
        Me.TableLayoutPanel_Body_Mid_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Right.Name = "TableLayoutPanel_Body_Mid_Right"
        Me.TableLayoutPanel_Body_Mid_Right.RowCount = 8
        Me.TableLayoutPanel_Body_Mid.SetRowSpan(Me.TableLayoutPanel_Body_Mid_Right, 2)
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523807!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.523809!))
        Me.TableLayoutPanel_Body_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Mid_Right.Size = New System.Drawing.Size(60, 320)
        Me.TableLayoutPanel_Body_Mid_Right.TabIndex = 0
        '
        'HmiButton_Position
        '
        Me.HmiButton_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Position.Location = New System.Drawing.Point(3, 183)
        Me.HmiButton_Position.MarginHeight = 6
        Me.HmiButton_Position.Name = "HmiButton_Position"
        Me.HmiButton_Position.Size = New System.Drawing.Size(54, 24)
        Me.HmiButton_Position.TabIndex = 5
        '
        'HmiTextBox_Position
        '
        Me.HmiTextBox_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Position.Location = New System.Drawing.Point(3, 153)
        Me.HmiTextBox_Position.Name = "HmiTextBox_Position"
        Me.HmiTextBox_Position.Number = 0
        Me.HmiTextBox_Position.Size = New System.Drawing.Size(54, 24)
        Me.HmiTextBox_Position.TabIndex = 4
        Me.HmiTextBox_Position.TextBoxReadOnly = False
        Me.HmiTextBox_Position.ValueType = GetType(String)
        '
        'HmiLabel_Position
        '
        Me.HmiLabel_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Position.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel_Position.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Position.Name = "HmiLabel_Position"
        Me.HmiLabel_Position.Size = New System.Drawing.Size(58, 28)
        Me.HmiLabel_Position.TabIndex = 1
        '
        'HmiLabel_Picture
        '
        Me.HmiLabel_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Picture.Location = New System.Drawing.Point(1, 31)
        Me.HmiLabel_Picture.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Picture.Name = "HmiLabel_Picture"
        Me.HmiLabel_Picture.Size = New System.Drawing.Size(58, 28)
        Me.HmiLabel_Picture.TabIndex = 0
        '
        'HmiTextBox_Picture
        '
        Me.HmiTextBox_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Picture.Location = New System.Drawing.Point(3, 63)
        Me.HmiTextBox_Picture.Name = "HmiTextBox_Picture"
        Me.HmiTextBox_Picture.Number = 0
        Me.HmiTextBox_Picture.Size = New System.Drawing.Size(54, 24)
        Me.HmiTextBox_Picture.TabIndex = 2
        Me.HmiTextBox_Picture.TextBoxReadOnly = False
        Me.HmiTextBox_Picture.ValueType = GetType(String)
        '
        'HmiButton_Picture
        '
        Me.HmiButton_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Picture.Location = New System.Drawing.Point(3, 93)
        Me.HmiButton_Picture.MarginHeight = 6
        Me.HmiButton_Picture.Name = "HmiButton_Picture"
        Me.HmiButton_Picture.Size = New System.Drawing.Size(54, 24)
        Me.HmiButton_Picture.TabIndex = 3
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 2
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(91, 1)
        Me.TableLayoutPanel_Body_Top.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(148, 37)
        Me.TableLayoutPanel_Body_Top.TabIndex = 12
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_N.Location = New System.Drawing.Point(77, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(68, 31)
        Me.RadioButton_N.TabIndex = 1
        Me.RadioButton_N.TabStop = True
        Me.RadioButton_N.Text = "N"
        Me.RadioButton_N.UseVisualStyleBackColor = True
        '
        'RadioButton_Y
        '
        Me.RadioButton_Y.AutoSize = True
        Me.RadioButton_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Y.Name = "RadioButton_Y"
        Me.RadioButton_Y.Size = New System.Drawing.Size(68, 31)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'ActionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 361)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ActionUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Bottom.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.PerformLayout()
        CType(Me.MachineListView_Picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PostToolBar.ResumeLayout(False)
        Me.PostToolBar.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Name As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Mid_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PostToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator_Station As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MachineListView_Picture As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents HmiButton_Position As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Position As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Position As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Picture As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Picture As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Picture As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
End Class
