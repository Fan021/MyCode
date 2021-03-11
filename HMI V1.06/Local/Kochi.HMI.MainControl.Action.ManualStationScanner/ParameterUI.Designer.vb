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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TabControl_Parameter = New System.Windows.Forms.TabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Device_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Device_Value = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.TableLayoutPanel_ErrorCode_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_ErrorCode_Value = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.Pandel_Body.SuspendLayout()
        Me.TabControl_Parameter.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel_Device_Body.SuspendLayout()
        CType(Me.MachineListView_Device_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_ErrorCode_Body.SuspendLayout()
        CType(Me.MachineListView_ErrorCode_Value, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel_Device_Body)
        Me.TabPage2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(445, 387)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "Device"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_ErrorCode_Body)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(445, 387)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "ErrorCode"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Device_Body
        '
        Me.TableLayoutPanel_Device_Body.ColumnCount = 3
        Me.TableLayoutPanel_Device_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Device_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Device_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Device_Body.Controls.Add(Me.MachineListView_Device_Value, 1, 1)
        Me.TableLayoutPanel_Device_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Device_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Device_Body.Name = "TableLayoutPanel_Device_Body"
        Me.TableLayoutPanel_Device_Body.RowCount = 3
        Me.TableLayoutPanel_Device_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Device_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Device_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Device_Body.Size = New System.Drawing.Size(439, 381)
        Me.TableLayoutPanel_Device_Body.TabIndex = 1
        '
        'MachineListView_Device_Value
        '
        Me.MachineListView_Device_Value.AllowUserToAddRows = False
        Me.MachineListView_Device_Value.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Device_Value.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.MachineListView_Device_Value.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Device_Value.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Device_Value.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Device_Value.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Device_Value.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Device_Value.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.MachineListView_Device_Value.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Device_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Device_Value.EnableHeadersVisualStyles = False
        Me.MachineListView_Device_Value.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Device_Value.Location = New System.Drawing.Point(24, 22)
        Me.MachineListView_Device_Value.Name = "MachineListView_Device_Value"
        Me.MachineListView_Device_Value.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Device_Value.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.MachineListView_Device_Value.RowTemplate.Height = 180
        Me.MachineListView_Device_Value.Size = New System.Drawing.Size(389, 336)
        Me.MachineListView_Device_Value.TabIndex = 1
        '
        'TableLayoutPanel_ErrorCode_Body
        '
        Me.TableLayoutPanel_ErrorCode_Body.ColumnCount = 3
        Me.TableLayoutPanel_ErrorCode_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_ErrorCode_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_ErrorCode_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_ErrorCode_Body.Controls.Add(Me.MachineListView_ErrorCode_Value, 1, 1)
        Me.TableLayoutPanel_ErrorCode_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_ErrorCode_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_ErrorCode_Body.Name = "TableLayoutPanel_ErrorCode_Body"
        Me.TableLayoutPanel_ErrorCode_Body.RowCount = 3
        Me.TableLayoutPanel_ErrorCode_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_ErrorCode_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_ErrorCode_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_ErrorCode_Body.Size = New System.Drawing.Size(439, 381)
        Me.TableLayoutPanel_ErrorCode_Body.TabIndex = 2
        '
        'MachineListView_ErrorCode_Value
        '
        Me.MachineListView_ErrorCode_Value.AllowUserToAddRows = False
        Me.MachineListView_ErrorCode_Value.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_ErrorCode_Value.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_ErrorCode_Value.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_ErrorCode_Value.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_ErrorCode_Value.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_ErrorCode_Value.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_ErrorCode_Value.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_ErrorCode_Value.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_ErrorCode_Value.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_ErrorCode_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_ErrorCode_Value.EnableHeadersVisualStyles = False
        Me.MachineListView_ErrorCode_Value.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_ErrorCode_Value.Location = New System.Drawing.Point(24, 22)
        Me.MachineListView_ErrorCode_Value.Name = "MachineListView_ErrorCode_Value"
        Me.MachineListView_ErrorCode_Value.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_ErrorCode_Value.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_ErrorCode_Value.RowTemplate.Height = 180
        Me.MachineListView_ErrorCode_Value.Size = New System.Drawing.Size(389, 336)
        Me.MachineListView_ErrorCode_Value.TabIndex = 1
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
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel_Device_Body.ResumeLayout(False)
        CType(Me.MachineListView_Device_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_ErrorCode_Body.ResumeLayout(False)
        CType(Me.MachineListView_ErrorCode_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TabControl_Parameter As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_ErrorCode_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_ErrorCode_Value As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Device_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_Device_Value As Kochi.HMI.MainControl.UI.MachineListView
End Class
