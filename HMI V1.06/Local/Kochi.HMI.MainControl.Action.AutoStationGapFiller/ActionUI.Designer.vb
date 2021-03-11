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
        Me.HmiLabel_GapFiller = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_File = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_GapFiller = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_GFile = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_GFile = New Kochi.HMI.MainControl.UI.HMIButton()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_GapFiller, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_File, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_GapFiller, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_GFile, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_GFile, 2, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_GapFiller
        '
        Me.HmiLabel_GapFiller.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_GapFiller.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_GapFiller.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_GapFiller.Name = "HmiLabel_GapFiller"
        Me.HmiLabel_GapFiller.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_GapFiller.TabIndex = 4
        '
        'HmiLabel_File
        '
        Me.HmiLabel_File.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_File.Location = New System.Drawing.Point(1, 40)
        Me.HmiLabel_File.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_File.Name = "HmiLabel_File"
        Me.HmiLabel_File.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_File.TabIndex = 5
        '
        'HmiComboBox_GapFiller
        '
        Me.HmiComboBox_GapFiller.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_GapFiller.Location = New System.Drawing.Point(91, 1)
        Me.HmiComboBox_GapFiller.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_GapFiller.Name = "HmiComboBox_GapFiller"
        Me.HmiComboBox_GapFiller.Size = New System.Drawing.Size(118, 37)
        Me.HmiComboBox_GapFiller.TabIndex = 6
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 79)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 281)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiTextBox_GFile
        '
        Me.HmiTextBox_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_GFile.Location = New System.Drawing.Point(93, 42)
        Me.HmiTextBox_GFile.Name = "HmiTextBox_GFile"
        Me.HmiTextBox_GFile.Number = 0
        Me.HmiTextBox_GFile.Size = New System.Drawing.Size(114, 33)
        Me.HmiTextBox_GFile.TabIndex = 12
        Me.HmiTextBox_GFile.TextBoxReadOnly = False
        Me.HmiTextBox_GFile.ValueType = GetType(String)
        '
        'HmiButton_GFile
        '
        Me.HmiButton_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_GFile.Location = New System.Drawing.Point(211, 40)
        Me.HmiButton_GFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_GFile.MarginHeight = 6
        Me.HmiButton_GFile.Name = "HmiButton_GFile"
        Me.HmiButton_GFile.Size = New System.Drawing.Size(88, 37)
        Me.HmiButton_GFile.TabIndex = 13
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
    Friend WithEvents HmiLabel_GapFiller As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_File As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_GapFiller As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_GFile As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_GFile As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
End Class
