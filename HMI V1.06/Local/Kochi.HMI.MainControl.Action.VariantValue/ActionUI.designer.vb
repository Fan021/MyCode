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
        Me.HmiLabel_Element = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Type = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Element = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Type = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
        Me.HmiLabel_ADSName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ADSName = New Kochi.HMI.MainControl.UI.HMITextBox()
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
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ADSName, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Element, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Type, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Element, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Type, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ADSName, 1, 2)
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
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 361)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_Element
        '
        Me.HmiLabel_Element.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Element.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Element.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Element.Name = "HmiLabel_Element"
        Me.HmiLabel_Element.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_Element.TabIndex = 4
        '
        'HmiLabel_Type
        '
        Me.HmiLabel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Type.Location = New System.Drawing.Point(1, 40)
        Me.HmiLabel_Type.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Type.Name = "HmiLabel_Type"
        Me.HmiLabel_Type.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_Type.TabIndex = 5
        '
        'HmiComboBox_Element
        '
        Me.HmiComboBox_Element.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Element.Location = New System.Drawing.Point(91, 1)
        Me.HmiComboBox_Element.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Element.Name = "HmiComboBox_Element"
        Me.HmiComboBox_Element.Size = New System.Drawing.Size(148, 37)
        Me.HmiComboBox_Element.TabIndex = 6
        '
        'HmiComboBox_Type
        '
        Me.HmiComboBox_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Type.Location = New System.Drawing.Point(91, 40)
        Me.HmiComboBox_Type.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Type.Name = "HmiComboBox_Type"
        Me.HmiComboBox_Type.Size = New System.Drawing.Size(148, 37)
        Me.HmiComboBox_Type.TabIndex = 10
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
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 281)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
        '
        'HmiLabel_ADSName
        '
        Me.HmiLabel_ADSName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ADSName.Location = New System.Drawing.Point(1, 79)
        Me.HmiLabel_ADSName.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_ADSName.Name = "HmiLabel_ADSName"
        Me.HmiLabel_ADSName.Size = New System.Drawing.Size(88, 37)
        Me.HmiLabel_ADSName.TabIndex = 12
        '
        'HmiTextBox_ADSName
        '
        Me.HmiTextBox_ADSName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ADSName.Location = New System.Drawing.Point(93, 81)
        Me.HmiTextBox_ADSName.Name = "HmiTextBox_ADSName"
        Me.HmiTextBox_ADSName.Number = 0
        Me.HmiTextBox_ADSName.Size = New System.Drawing.Size(144, 33)
        Me.HmiTextBox_ADSName.TabIndex = 13
        Me.HmiTextBox_ADSName.TextBoxReadOnly = False
        Me.HmiTextBox_ADSName.ValueType = GetType(String)
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
    Friend WithEvents HmiLabel_Element As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Type As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_Element As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Type As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_ADSName As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ADSName As Kochi.HMI.MainControl.UI.HMITextBox
End Class
