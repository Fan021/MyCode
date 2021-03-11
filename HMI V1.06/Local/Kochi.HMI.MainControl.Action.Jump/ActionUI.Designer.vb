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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Action = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBoxWithButton_B = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.HmiLabel_B = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Condition = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_A = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBoxWithButton_A = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.HmiComboBox_Condition = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Action = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
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
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Action, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBoxWithButton_B, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_B, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Condition, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_A, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBoxWithButton_A, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Condition, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Action, 1, 3)
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_Action
        '
        Me.HmiLabel_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Action.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_Action.Name = "HmiLabel_Action"
        Me.HmiLabel_Action.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_Action.TabIndex = 18
        '
        'HmiTextBoxWithButton_B
        '
        Me.HmiTextBoxWithButton_B.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBoxWithButton_B.Location = New System.Drawing.Point(93, 81)
        Me.HmiTextBoxWithButton_B.Name = "HmiTextBoxWithButton_B"
        Me.HmiTextBoxWithButton_B.Size = New System.Drawing.Size(114, 33)
        Me.HmiTextBoxWithButton_B.TabIndex = 17
        '
        'HmiLabel_B
        '
        Me.HmiLabel_B.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_B.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_B.Name = "HmiLabel_B"
        Me.HmiLabel_B.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_B.TabIndex = 16
        '
        'HmiLabel_Condition
        '
        Me.HmiLabel_Condition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Condition.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Condition.Name = "HmiLabel_Condition"
        Me.HmiLabel_Condition.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_Condition.TabIndex = 14
        '
        'HmiLabel_A
        '
        Me.HmiLabel_A.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_A.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_A.Name = "HmiLabel_A"
        Me.HmiLabel_A.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_A.TabIndex = 12
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 157)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 320)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiTextBoxWithButton_A
        '
        Me.HmiTextBoxWithButton_A.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBoxWithButton_A.Location = New System.Drawing.Point(93, 3)
        Me.HmiTextBoxWithButton_A.Name = "HmiTextBoxWithButton_A"
        Me.HmiTextBoxWithButton_A.Size = New System.Drawing.Size(114, 33)
        Me.HmiTextBoxWithButton_A.TabIndex = 13
        '
        'HmiComboBox_Condition
        '
        Me.HmiComboBox_Condition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Condition.Location = New System.Drawing.Point(93, 42)
        Me.HmiComboBox_Condition.Name = "HmiComboBox_Condition"
        Me.HmiComboBox_Condition.Size = New System.Drawing.Size(114, 33)
        Me.HmiComboBox_Condition.TabIndex = 15
        '
        'HmiComboBox_Action
        '
        Me.HmiComboBox_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Action.Location = New System.Drawing.Point(93, 120)
        Me.HmiComboBox_Action.Name = "HmiComboBox_Action"
        Me.HmiComboBox_Action.Size = New System.Drawing.Size(114, 33)
        Me.HmiComboBox_Action.TabIndex = 19
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiLabel_A As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBoxWithButton_B As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Friend WithEvents HmiLabel_B As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Condition As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBoxWithButton_A As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Friend WithEvents HmiComboBox_Condition As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Action As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Action As Kochi.HMI.MainControl.UI.HMIComboBox
End Class
