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
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_FailurePicture = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_FailuerText = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Position = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Position = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_Picture = New Kochi.HMI.MainControl.UI.HMITextBoxWithButton()
        Me.HmiButton_Choose = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Description = New Kochi.HMI.MainControl.UI.HMITextBoxWithButton()
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
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Description, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_Choose, 3, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Picture, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_FailurePicture, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_FailuerText, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Position, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Position, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 3)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 4
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_FailurePicture
        '
        Me.HmiLabel_FailurePicture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_FailurePicture.Location = New System.Drawing.Point(1, 79)
        Me.HmiLabel_FailurePicture.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_FailurePicture.Name = "HmiLabel_FailurePicture"
        Me.HmiLabel_FailurePicture.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_FailurePicture.TabIndex = 13
        '
        'HmiLabel_FailuerText
        '
        Me.HmiLabel_FailuerText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_FailuerText.Location = New System.Drawing.Point(1, 40)
        Me.HmiLabel_FailuerText.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_FailuerText.Name = "HmiLabel_FailuerText"
        Me.HmiLabel_FailuerText.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_FailuerText.TabIndex = 12
        '
        'HmiLabel_Position
        '
        Me.HmiLabel_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Position.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Position.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Position.Name = "HmiLabel_Position"
        Me.HmiLabel_Position.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_Position.TabIndex = 4
        '
        'HmiComboBox_Position
        '
        Me.HmiComboBox_Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Position.Location = New System.Drawing.Point(91, 1)
        Me.HmiComboBox_Position.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Position.Name = "HmiComboBox_Position"
        Me.HmiComboBox_Position.Size = New System.Drawing.Size(118, 37)
        Me.HmiComboBox_Position.TabIndex = 6
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 118)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 320)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiTextBox_Picture
        '
        Me.HmiTextBox_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Picture.Location = New System.Drawing.Point(93, 81)
        Me.HmiTextBox_Picture.Name = "HmiTextBox_Picture"
        Me.HmiTextBox_Picture.Size = New System.Drawing.Size(114, 33)
        Me.HmiTextBox_Picture.TabIndex = 20
        '
        'HmiButton_Choose
        '
        Me.HmiButton_Choose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Choose.Location = New System.Drawing.Point(213, 81)
        Me.HmiButton_Choose.MarginHeight = 6
        Me.HmiButton_Choose.Name = "HmiButton_Choose"
        Me.HmiButton_Choose.Size = New System.Drawing.Size(84, 33)
        Me.HmiButton_Choose.TabIndex = 29
        '
        'HmiTextBox_Description
        '
        Me.HmiTextBox_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Description.Location = New System.Drawing.Point(93, 42)
        Me.HmiTextBox_Description.Name = "HmiTextBox_Description"
        Me.HmiTextBox_Description.Size = New System.Drawing.Size(114, 33)
        Me.HmiTextBox_Description.TabIndex = 30
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
    Friend WithEvents HmiLabel_Position As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_Position As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiLabel_FailurePicture As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_FailuerText As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiTextBox_Picture As Kochi.HMI.MainControl.UI.HMITextBoxWithButton
    Friend WithEvents HmiButton_Choose As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents HmiTextBox_Description As Kochi.HMI.MainControl.UI.HMITextBoxWithButton
End Class
