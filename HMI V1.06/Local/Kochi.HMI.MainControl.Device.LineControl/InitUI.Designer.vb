<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.HmiLabel_Enable = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Path = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_TraceID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_TraceID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Section = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Section = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Ini = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Ini = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Head_Detail.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 246)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Enable, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_Path, 3, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_TraceID, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_TraceID, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Section, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Section, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Ini, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Ini, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 5
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(78, 120)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(145, 33)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 28
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_N.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_N.Location = New System.Drawing.Point(75, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(67, 27)
        Me.RadioButton_N.TabIndex = 1
        Me.RadioButton_N.Text = "N"
        Me.RadioButton_N.UseVisualStyleBackColor = True
        '
        'RadioButton_Y
        '
        Me.RadioButton_Y.AutoSize = True
        Me.RadioButton_Y.Checked = True
        Me.RadioButton_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Y.Name = "RadioButton_Y"
        Me.RadioButton_Y.Size = New System.Drawing.Size(66, 27)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'HmiLabel_Enable
        '
        Me.HmiLabel_Enable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Enable.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_Enable.Name = "HmiLabel_Enable"
        Me.HmiLabel_Enable.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Enable.TabIndex = 12
        '
        'HmiButton_Path
        '
        Me.HmiButton_Path.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Path.Location = New System.Drawing.Point(229, 3)
        Me.HmiButton_Path.MarginHeight = 6
        Me.HmiButton_Path.Name = "HmiButton_Path"
        Me.HmiButton_Path.Size = New System.Drawing.Size(71, 33)
        Me.HmiButton_Path.TabIndex = 11
        '
        'HmiTextBox_TraceID
        '
        Me.HmiTextBox_TraceID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TraceID.Location = New System.Drawing.Point(78, 81)
        Me.HmiTextBox_TraceID.Name = "HmiTextBox_TraceID"
        Me.HmiTextBox_TraceID.Number = 0
        Me.HmiTextBox_TraceID.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_TraceID.TabIndex = 10
        Me.HmiTextBox_TraceID.TextBoxReadOnly = False
        Me.HmiTextBox_TraceID.ValueType = GetType(String)
        '
        'HmiLabel_TraceID
        '
        Me.HmiLabel_TraceID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TraceID.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_TraceID.Name = "HmiLabel_TraceID"
        Me.HmiLabel_TraceID.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_TraceID.TabIndex = 7
        '
        'HmiTextBox_Section
        '
        Me.HmiTextBox_Section.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Section.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_Section.Name = "HmiTextBox_Section"
        Me.HmiTextBox_Section.Number = 0
        Me.HmiTextBox_Section.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Section.TabIndex = 6
        Me.HmiTextBox_Section.TextBoxReadOnly = False
        Me.HmiTextBox_Section.ValueType = GetType(String)
        '
        'HmiLabel_Section
        '
        Me.HmiLabel_Section.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Section.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Section.Name = "HmiLabel_Section"
        Me.HmiLabel_Section.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Section.TabIndex = 5
        '
        'HmiTextBox_Ini
        '
        Me.HmiTextBox_Ini.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Ini.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_Ini.Name = "HmiTextBox_Ini"
        Me.HmiTextBox_Ini.Number = 0
        Me.HmiTextBox_Ini.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Ini.TabIndex = 1
        Me.HmiTextBox_Ini.TextBoxReadOnly = False
        Me.HmiTextBox_Ini.ValueType = GetType(String)
        '
        'HmiLabel_Ini
        '
        Me.HmiLabel_Ini.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Ini.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Ini.Name = "HmiLabel_Ini"
        Me.HmiLabel_Ini.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Ini.TabIndex = 4
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 246)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InitUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.Pandel_Body.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_Ini As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Ini As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Section As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Section As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_TraceID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_TraceID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Path As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiLabel_Enable As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
End Class
