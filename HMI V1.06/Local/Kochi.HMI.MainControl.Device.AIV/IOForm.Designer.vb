<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component List.
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Top_input = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Body_Top = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Lero = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Normal = New System.Windows.Forms.RadioButton()
        Me.HmiLabel_Mode = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Delay = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_AIV = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Delay = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_Delay = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_AIV = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Top_input.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top.SuspendLayout()
        Me.TableLayoutPanel_Head_Detail.SuspendLayout()
        Me.TableLayoutPanel_Delay.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Top_input, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(498, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'GroupBox_Top_input
        '
        Me.GroupBox_Top_input.Controls.Add(Me.HmiTableLayoutPanel_Body_Top)
        Me.GroupBox_Top_input.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Top_input.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Top_input.Name = "GroupBox_Top_input"
        Me.GroupBox_Top_input.Size = New System.Drawing.Size(492, 524)
        Me.GroupBox_Top_input.TabIndex = 2
        Me.GroupBox_Top_input.TabStop = False
        Me.GroupBox_Top_input.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Body_Top
        '
        Me.HmiTableLayoutPanel_Body_Top.ColumnCount = 2
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.TableLayoutPanel1, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.TableLayoutPanel_Delay, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.HmiLabel_AIV, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.HmiLabel_Delay, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.HmiLabel_Mode, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel_Body_Top.Name = "HmiTableLayoutPanel_Body_Top"
        Me.HmiTableLayoutPanel_Body_Top.RowCount = 4
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.HmiTableLayoutPanel_Body_Top.Size = New System.Drawing.Size(486, 504)
        Me.HmiTableLayoutPanel_Body_Top.TabIndex = 1
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Lero, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Normal, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(197, 3)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(286, 44)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 30
        '
        'RadioButton_Lero
        '
        Me.RadioButton_Lero.AutoSize = True
        Me.RadioButton_Lero.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Lero.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_Lero.Location = New System.Drawing.Point(146, 3)
        Me.RadioButton_Lero.Name = "RadioButton_Lero"
        Me.RadioButton_Lero.Size = New System.Drawing.Size(137, 38)
        Me.RadioButton_Lero.TabIndex = 1
        Me.RadioButton_Lero.Text = "Lero"
        Me.RadioButton_Lero.UseVisualStyleBackColor = True
        '
        'RadioButton_Normal
        '
        Me.RadioButton_Normal.AutoSize = True
        Me.RadioButton_Normal.Checked = True
        Me.RadioButton_Normal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Normal.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_Normal.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Normal.Name = "RadioButton_Normal"
        Me.RadioButton_Normal.Size = New System.Drawing.Size(137, 38)
        Me.RadioButton_Normal.TabIndex = 0
        Me.RadioButton_Normal.TabStop = True
        Me.RadioButton_Normal.Text = "Normal"
        Me.RadioButton_Normal.UseVisualStyleBackColor = True
        '
        'HmiLabel_Mode
        '
        Me.HmiLabel_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Mode.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Mode.Name = "HmiLabel_Mode"
        Me.HmiLabel_Mode.Size = New System.Drawing.Size(188, 44)
        Me.HmiLabel_Mode.TabIndex = 0
        '
        'HmiLabel_Delay
        '
        Me.HmiLabel_Delay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Delay.Location = New System.Drawing.Point(3, 53)
        Me.HmiLabel_Delay.Name = "HmiLabel_Delay"
        Me.HmiLabel_Delay.Size = New System.Drawing.Size(188, 44)
        Me.HmiLabel_Delay.TabIndex = 31
        '
        'HmiLabel_AIV
        '
        Me.HmiLabel_AIV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AIV.Location = New System.Drawing.Point(3, 103)
        Me.HmiLabel_AIV.Name = "HmiLabel_AIV"
        Me.HmiLabel_AIV.Size = New System.Drawing.Size(188, 44)
        Me.HmiLabel_AIV.TabIndex = 32
        '
        'TableLayoutPanel_Delay
        '
        Me.TableLayoutPanel_Delay.ColumnCount = 2
        Me.TableLayoutPanel_Delay.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Delay.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Delay.Controls.Add(Me.HmiTextBox_Delay, 0, 0)
        Me.TableLayoutPanel_Delay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Delay.Location = New System.Drawing.Point(197, 53)
        Me.TableLayoutPanel_Delay.Name = "TableLayoutPanel_Delay"
        Me.TableLayoutPanel_Delay.RowCount = 1
        Me.TableLayoutPanel_Delay.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Delay.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Delay.Size = New System.Drawing.Size(286, 44)
        Me.TableLayoutPanel_Delay.TabIndex = 33
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.HmiTextBox_AIV, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(197, 103)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(286, 44)
        Me.TableLayoutPanel1.TabIndex = 34
        '
        'HmiTextBox_Delay
        '
        Me.HmiTextBox_Delay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Delay.Location = New System.Drawing.Point(3, 3)
        Me.HmiTextBox_Delay.Name = "HmiTextBox_Delay"
        Me.HmiTextBox_Delay.Number = 0
        Me.HmiTextBox_Delay.Size = New System.Drawing.Size(137, 38)
        Me.HmiTextBox_Delay.TabIndex = 0
        Me.HmiTextBox_Delay.TextBoxReadOnly = False
        Me.HmiTextBox_Delay.ValueType = GetType(String)
        '
        'HmiTextBox_AIV
        '
        Me.HmiTextBox_AIV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AIV.Location = New System.Drawing.Point(3, 3)
        Me.HmiTextBox_AIV.Name = "HmiTextBox_AIV"
        Me.HmiTextBox_AIV.Number = 0
        Me.HmiTextBox_AIV.Size = New System.Drawing.Size(137, 38)
        Me.HmiTextBox_AIV.TabIndex = 2
        Me.HmiTextBox_AIV.TextBoxReadOnly = False
        Me.HmiTextBox_AIV.ValueType = GetType(String)
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IOForm"
        Me.Text = "IOForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Top_input.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.PerformLayout()
        Me.TableLayoutPanel_Delay.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Top_input As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Body_Top As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Mode As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Lero As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Normal As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_AIV As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Delay As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_Delay As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AIV As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Delay As Kochi.HMI.MainControl.UI.HMILabel
End Class
