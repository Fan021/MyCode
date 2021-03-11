<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ActionUI
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
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_XYZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_XYZ = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_X = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Y = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Z = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_XYZ = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.OpenFileDialogTpy = New System.Windows.Forms.OpenFileDialog()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 271)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 4
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_XYZ, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_XYZ, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_X, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_X, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Y, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Y, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Z, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Z, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiButton_XYZ, 3, 0)
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 271)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_XYZ
        '
        Me.HmiLabel_XYZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_XYZ.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_XYZ.Name = "HmiLabel_XYZ"
        Me.HmiLabel_XYZ.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_XYZ.TabIndex = 5
        '
        'HmiComboBox_XYZ
        '
        Me.HmiComboBox_XYZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_XYZ.Location = New System.Drawing.Point(78, 3)
        Me.HmiComboBox_XYZ.Name = "HmiComboBox_XYZ"
        Me.HmiComboBox_XYZ.Size = New System.Drawing.Size(69, 33)
        Me.HmiComboBox_XYZ.TabIndex = 10
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_X.TabIndex = 11
        '
        'HmiTextBox_X
        '
        Me.HmiTextBox_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_X.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_X.Name = "HmiTextBox_X"
        Me.HmiTextBox_X.Number = 0
        Me.HmiTextBox_X.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_X.TabIndex = 12
        Me.HmiTextBox_X.TextBoxReadOnly = False
        Me.HmiTextBox_X.ValueType = GetType(String)
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Y.TabIndex = 15
        '
        'HmiTextBox_Y
        '
        Me.HmiTextBox_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Y.Location = New System.Drawing.Point(78, 81)
        Me.HmiTextBox_Y.Name = "HmiTextBox_Y"
        Me.HmiTextBox_Y.Number = 0
        Me.HmiTextBox_Y.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_Y.TabIndex = 16
        Me.HmiTextBox_Y.TextBoxReadOnly = False
        Me.HmiTextBox_Y.ValueType = GetType(String)
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Z.TabIndex = 19
        '
        'HmiTextBox_Z
        '
        Me.HmiTextBox_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Z.Location = New System.Drawing.Point(78, 120)
        Me.HmiTextBox_Z.Name = "HmiTextBox_Z"
        Me.HmiTextBox_Z.Number = 0
        Me.HmiTextBox_Z.Size = New System.Drawing.Size(69, 33)
        Me.HmiTextBox_Z.TabIndex = 20
        Me.HmiTextBox_Z.TextBoxReadOnly = False
        Me.HmiTextBox_Z.ValueType = GetType(String)
        '
        'HmiButton_XYZ
        '
        Me.HmiButton_XYZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_XYZ.Location = New System.Drawing.Point(228, 3)
        Me.HmiButton_XYZ.MarginHeight = 6
        Me.HmiButton_XYZ.Name = "HmiButton_XYZ"
        Me.HmiButton_XYZ.Size = New System.Drawing.Size(69, 33)
        Me.HmiButton_XYZ.TabIndex = 24
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
        '
        'ActionUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 271)
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
    Friend WithEvents HmiLabel_XYZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_XYZ As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_X As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Y As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Z As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_XYZ As Kochi.HMI.MainControl.UI.HMIButton
End Class
