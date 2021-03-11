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
        Me.HmiTextBox_TimeOut = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_TimeOut = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Include = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Include = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Path2 = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Path2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Path2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Type2 = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Type2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Type = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Path = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Path = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PLCAds = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_PLCAds = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Path = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Type = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.FolderBrowserDialog_Path = New System.Windows.Forms.FolderBrowserDialog()
        Me.HmiLabel_ProgramCheck = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Program = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButtonProgram_N = New System.Windows.Forms.RadioButton()
        Me.RadioButtonProgram_Y = New System.Windows.Forms.RadioButton()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Program.SuspendLayout()
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
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Program, 1, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ProgramCheck, 0, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_TimeOut, 1, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_TimeOut, 0, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Include, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Include, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_Path2, 2, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Path2, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Path2, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Type2, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Type2, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Type, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Path, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Path, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_PLCAds, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PLCAds, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_Path, 2, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Type, 1, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 9
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_TimeOut
        '
        Me.HmiTextBox_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TimeOut.Location = New System.Drawing.Point(93, 123)
        Me.HmiTextBox_TimeOut.Name = "HmiTextBox_TimeOut"
        Me.HmiTextBox_TimeOut.Number = 0
        Me.HmiTextBox_TimeOut.Size = New System.Drawing.Size(130, 14)
        Me.HmiTextBox_TimeOut.TabIndex = 18
        Me.HmiTextBox_TimeOut.TextBoxReadOnly = False
        Me.HmiTextBox_TimeOut.ValueType = GetType(String)
        '
        'HmiLabel_TimeOut
        '
        Me.HmiLabel_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TimeOut.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_TimeOut.Name = "HmiLabel_TimeOut"
        Me.HmiLabel_TimeOut.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_TimeOut.TabIndex = 17
        '
        'HmiTextBox_Include
        '
        Me.HmiTextBox_Include.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Include.Location = New System.Drawing.Point(93, 103)
        Me.HmiTextBox_Include.Name = "HmiTextBox_Include"
        Me.HmiTextBox_Include.Number = 0
        Me.HmiTextBox_Include.Size = New System.Drawing.Size(130, 14)
        Me.HmiTextBox_Include.TabIndex = 16
        Me.HmiTextBox_Include.TextBoxReadOnly = False
        Me.HmiTextBox_Include.ValueType = GetType(String)
        '
        'HmiLabel_Include
        '
        Me.HmiLabel_Include.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Include.Location = New System.Drawing.Point(3, 103)
        Me.HmiLabel_Include.Name = "HmiLabel_Include"
        Me.HmiLabel_Include.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_Include.TabIndex = 15
        '
        'HmiButton_Path2
        '
        Me.HmiButton_Path2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Path2.Location = New System.Drawing.Point(229, 83)
        Me.HmiButton_Path2.MarginHeight = 6
        Me.HmiButton_Path2.Name = "HmiButton_Path2"
        Me.HmiButton_Path2.Size = New System.Drawing.Size(71, 14)
        Me.HmiButton_Path2.TabIndex = 14
        '
        'HmiTextBox_Path2
        '
        Me.HmiTextBox_Path2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Path2.Location = New System.Drawing.Point(93, 83)
        Me.HmiTextBox_Path2.Name = "HmiTextBox_Path2"
        Me.HmiTextBox_Path2.Number = 0
        Me.HmiTextBox_Path2.Size = New System.Drawing.Size(130, 14)
        Me.HmiTextBox_Path2.TabIndex = 13
        Me.HmiTextBox_Path2.TextBoxReadOnly = False
        Me.HmiTextBox_Path2.ValueType = GetType(String)
        '
        'HmiLabel_Path2
        '
        Me.HmiLabel_Path2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Path2.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_Path2.Name = "HmiLabel_Path2"
        Me.HmiLabel_Path2.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_Path2.TabIndex = 12
        '
        'HmiComboBox_Type2
        '
        Me.HmiComboBox_Type2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Type2.Location = New System.Drawing.Point(93, 43)
        Me.HmiComboBox_Type2.Name = "HmiComboBox_Type2"
        Me.HmiComboBox_Type2.Size = New System.Drawing.Size(130, 14)
        Me.HmiComboBox_Type2.TabIndex = 11
        '
        'HmiLabel_Type2
        '
        Me.HmiLabel_Type2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Type2.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_Type2.Name = "HmiLabel_Type2"
        Me.HmiLabel_Type2.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_Type2.TabIndex = 10
        '
        'HmiLabel_Type
        '
        Me.HmiLabel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Type.Location = New System.Drawing.Point(3, 23)
        Me.HmiLabel_Type.Name = "HmiLabel_Type"
        Me.HmiLabel_Type.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_Type.TabIndex = 8
        '
        'HmiTextBox_Path
        '
        Me.HmiTextBox_Path.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Path.Location = New System.Drawing.Point(93, 63)
        Me.HmiTextBox_Path.Name = "HmiTextBox_Path"
        Me.HmiTextBox_Path.Number = 0
        Me.HmiTextBox_Path.Size = New System.Drawing.Size(130, 14)
        Me.HmiTextBox_Path.TabIndex = 6
        Me.HmiTextBox_Path.TextBoxReadOnly = False
        Me.HmiTextBox_Path.ValueType = GetType(String)
        '
        'HmiLabel_Path
        '
        Me.HmiLabel_Path.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Path.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_Path.Name = "HmiLabel_Path"
        Me.HmiLabel_Path.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_Path.TabIndex = 5
        '
        'HmiTextBox_PLCAds
        '
        Me.HmiTextBox_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PLCAds.Location = New System.Drawing.Point(93, 3)
        Me.HmiTextBox_PLCAds.Name = "HmiTextBox_PLCAds"
        Me.HmiTextBox_PLCAds.Number = 0
        Me.HmiTextBox_PLCAds.Size = New System.Drawing.Size(130, 14)
        Me.HmiTextBox_PLCAds.TabIndex = 1
        Me.HmiTextBox_PLCAds.TextBoxReadOnly = False
        Me.HmiTextBox_PLCAds.ValueType = GetType(String)
        '
        'HmiLabel_PLCAds
        '
        Me.HmiLabel_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PLCAds.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_PLCAds.Name = "HmiLabel_PLCAds"
        Me.HmiLabel_PLCAds.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_PLCAds.TabIndex = 4
        '
        'HmiButton_Path
        '
        Me.HmiButton_Path.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Path.Location = New System.Drawing.Point(229, 63)
        Me.HmiButton_Path.MarginHeight = 6
        Me.HmiButton_Path.Name = "HmiButton_Path"
        Me.HmiButton_Path.Size = New System.Drawing.Size(71, 14)
        Me.HmiButton_Path.TabIndex = 7
        '
        'HmiComboBox_Type
        '
        Me.HmiComboBox_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Type.Location = New System.Drawing.Point(93, 23)
        Me.HmiComboBox_Type.Name = "HmiComboBox_Type"
        Me.HmiComboBox_Type.Size = New System.Drawing.Size(130, 14)
        Me.HmiComboBox_Type.TabIndex = 9
        '
        'HmiLabel_ProgramCheck
        '
        Me.HmiLabel_ProgramCheck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ProgramCheck.Location = New System.Drawing.Point(3, 143)
        Me.HmiLabel_ProgramCheck.Name = "HmiLabel_ProgramCheck"
        Me.HmiLabel_ProgramCheck.Size = New System.Drawing.Size(84, 14)
        Me.HmiLabel_ProgramCheck.TabIndex = 19
        '
        'TableLayoutPanel_Program
        '
        Me.TableLayoutPanel_Program.ColumnCount = 2
        Me.TableLayoutPanel_Program.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Program.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Program.Controls.Add(Me.RadioButtonProgram_N, 1, 0)
        Me.TableLayoutPanel_Program.Controls.Add(Me.RadioButtonProgram_Y, 0, 0)
        Me.TableLayoutPanel_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Program.Location = New System.Drawing.Point(93, 143)
        Me.TableLayoutPanel_Program.Name = "TableLayoutPanel_Program"
        Me.TableLayoutPanel_Program.RowCount = 1
        Me.TableLayoutPanel_Program.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Program.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Program.Size = New System.Drawing.Size(130, 14)
        Me.TableLayoutPanel_Program.TabIndex = 36
        '
        'RadioButtonProgram_N
        '
        Me.RadioButtonProgram_N.AutoSize = True
        Me.RadioButtonProgram_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButtonProgram_N.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButtonProgram_N.Location = New System.Drawing.Point(68, 3)
        Me.RadioButtonProgram_N.Name = "RadioButtonProgram_N"
        Me.RadioButtonProgram_N.Size = New System.Drawing.Size(59, 8)
        Me.RadioButtonProgram_N.TabIndex = 1
        Me.RadioButtonProgram_N.Text = "N"
        Me.RadioButtonProgram_N.UseVisualStyleBackColor = True
        '
        'RadioButtonProgram_Y
        '
        Me.RadioButtonProgram_Y.AutoSize = True
        Me.RadioButtonProgram_Y.Checked = True
        Me.RadioButtonProgram_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButtonProgram_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButtonProgram_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButtonProgram_Y.Name = "RadioButtonProgram_Y"
        Me.RadioButtonProgram_Y.Size = New System.Drawing.Size(59, 8)
        Me.RadioButtonProgram_Y.TabIndex = 0
        Me.RadioButtonProgram_Y.TabStop = True
        Me.RadioButtonProgram_Y.Text = "Y"
        Me.RadioButtonProgram_Y.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel_Program.ResumeLayout(False)
        Me.TableLayoutPanel_Program.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents HmiTextBox_PLCAds As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_PLCAds As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Path As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Path As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents FolderBrowserDialog_Path As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents HmiLabel_Type As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Path As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Type As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_Include As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Include As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Path2 As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Path2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Path2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Type2 As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Type2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_TimeOut As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_TimeOut As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_ProgramCheck As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Program As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButtonProgram_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonProgram_Y As System.Windows.Forms.RadioButton
End Class
