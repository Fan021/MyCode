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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Name = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_Type = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Parameter = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
        Me.HmiLabel_OneTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_OneTime = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Mid.SuspendLayout()
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_OneTime.SuspendLayout()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_OneTime, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Name, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Type, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Mid, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_OneTime, 1, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 4
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
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
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 333)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 27)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiComboBox_Type
        '
        Me.HmiComboBox_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Type.Location = New System.Drawing.Point(93, 3)
        Me.HmiComboBox_Type.Name = "HmiComboBox_Type"
        Me.HmiComboBox_Type.Size = New System.Drawing.Size(144, 33)
        Me.HmiComboBox_Type.TabIndex = 12
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Mid, 3)
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.MachineListView_Parameter, 0, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(1, 79)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(298, 252)
        Me.TableLayoutPanel_Mid.TabIndex = 13
        '
        'MachineListView_Parameter
        '
        Me.MachineListView_Parameter.AllowUserToAddRows = False
        Me.MachineListView_Parameter.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.MachineListView_Parameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Parameter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Parameter.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Parameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Parameter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Parameter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.MachineListView_Parameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Parameter.EnableHeadersVisualStyles = False
        Me.MachineListView_Parameter.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Parameter.Location = New System.Drawing.Point(0, 0)
        Me.MachineListView_Parameter.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Parameter.Name = "MachineListView_Parameter"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MachineListView_Parameter.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.MachineListView_Parameter.RowHeadersVisible = False
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.MachineListView_Parameter.RowTemplate.Height = 23
        Me.MachineListView_Parameter.Size = New System.Drawing.Size(298, 252)
        Me.MachineListView_Parameter.TabIndex = 15
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
        '
        'HmiLabel_OneTime
        '
        Me.HmiLabel_OneTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_OneTime.Location = New System.Drawing.Point(1, 40)
        Me.HmiLabel_OneTime.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_OneTime.Name = "HmiLabel_OneTime"
        Me.HmiLabel_OneTime.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_OneTime.TabIndex = 14
        '
        'TableLayoutPanel_OneTime
        '
        Me.TableLayoutPanel_OneTime.ColumnCount = 2
        Me.TableLayoutPanel_OneTime.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_OneTime.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_OneTime.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_OneTime.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_OneTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_OneTime.Location = New System.Drawing.Point(91, 40)
        Me.TableLayoutPanel_OneTime.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_OneTime.Name = "TableLayoutPanel_OneTime"
        Me.TableLayoutPanel_OneTime.RowCount = 1
        Me.TableLayoutPanel_OneTime.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_OneTime.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_OneTime.Size = New System.Drawing.Size(148, 37)
        Me.TableLayoutPanel_OneTime.TabIndex = 15
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
        Me.RadioButton_Y.Text = "RadioButton1"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
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
        Me.RadioButton_N.Text = "RadioButton2"
        Me.RadioButton_N.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_OneTime.ResumeLayout(False)
        Me.TableLayoutPanel_OneTime.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Name As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiComboBox_Type As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_Parameter As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents HmiLabel_OneTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_OneTime As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
End Class
