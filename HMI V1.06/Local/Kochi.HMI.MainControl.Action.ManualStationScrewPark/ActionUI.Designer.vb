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
        Me.HmiLabel_PKP = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_CleanTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_ScrewPark = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_CleanTime = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
        Me.HmiLabel_Count = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Count = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 391)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Count, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Count, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PKP, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_CleanTime, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_ScrewPark, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_CleanTime, 1, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 4
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 391)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_PKP
        '
        Me.HmiLabel_PKP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PKP.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_PKP.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_PKP.Name = "HmiLabel_PKP"
        Me.HmiLabel_PKP.Size = New System.Drawing.Size(88, 40)
        Me.HmiLabel_PKP.TabIndex = 4
        '
        'HmiLabel_CleanTime
        '
        Me.HmiLabel_CleanTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CleanTime.Location = New System.Drawing.Point(1, 85)
        Me.HmiLabel_CleanTime.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_CleanTime.Name = "HmiLabel_CleanTime"
        Me.HmiLabel_CleanTime.Size = New System.Drawing.Size(88, 40)
        Me.HmiLabel_CleanTime.TabIndex = 5
        '
        'HmiComboBox_ScrewPark
        '
        Me.HmiComboBox_ScrewPark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ScrewPark.Location = New System.Drawing.Point(91, 1)
        Me.HmiComboBox_ScrewPark.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_ScrewPark.Name = "HmiComboBox_ScrewPark"
        Me.HmiComboBox_ScrewPark.Size = New System.Drawing.Size(118, 40)
        Me.HmiComboBox_ScrewPark.TabIndex = 6
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 127)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 304)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'HmiTextBox_CleanTime
        '
        Me.HmiTextBox_CleanTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_CleanTime.Location = New System.Drawing.Point(93, 87)
        Me.HmiTextBox_CleanTime.Name = "HmiTextBox_CleanTime"
        Me.HmiTextBox_CleanTime.Size = New System.Drawing.Size(114, 36)
        Me.HmiTextBox_CleanTime.TabIndex = 12
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
        '
        'HmiLabel_Count
        '
        Me.HmiLabel_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Count.Location = New System.Drawing.Point(1, 43)
        Me.HmiLabel_Count.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Count.Name = "HmiLabel_Count"
        Me.HmiLabel_Count.Size = New System.Drawing.Size(88, 40)
        Me.HmiLabel_Count.TabIndex = 13
        '
        'HmiTextBox_Count
        '
        Me.HmiTextBox_Count.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Count.Location = New System.Drawing.Point(93, 45)
        Me.HmiTextBox_Count.Name = "HmiTextBox_Count"
        Me.HmiTextBox_Count.Size = New System.Drawing.Size(114, 36)
        Me.HmiTextBox_Count.TabIndex = 14
        '
        'ActionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 391)
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
    Friend WithEvents HmiLabel_PKP As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_CleanTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_ScrewPark As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_CleanTime As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Friend WithEvents HmiTextBox_Count As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Friend WithEvents HmiLabel_Count As Kochi.HMI.MainControl.UI.HMILabel
End Class
