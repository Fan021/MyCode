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
        Me.HmiLabel_CellControl = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Method = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_CellControl = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Method = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Bottom = New System.Windows.Forms.TableLayoutPanel()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(300, 391)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_CellControl, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Method, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_CellControl, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Method, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Bottom, 0, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(300, 391)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_CellControl
        '
        Me.HmiLabel_CellControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CellControl.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_CellControl.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_CellControl.Name = "HmiLabel_CellControl"
        Me.HmiLabel_CellControl.Size = New System.Drawing.Size(88, 40)
        Me.HmiLabel_CellControl.TabIndex = 4
        '
        'HmiLabel_Method
        '
        Me.HmiLabel_Method.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Method.Location = New System.Drawing.Point(1, 43)
        Me.HmiLabel_Method.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Method.Name = "HmiLabel_Method"
        Me.HmiLabel_Method.Size = New System.Drawing.Size(88, 40)
        Me.HmiLabel_Method.TabIndex = 5
        '
        'HmiComboBox_CellControl
        '
        Me.HmiComboBox_CellControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CellControl.Location = New System.Drawing.Point(91, 1)
        Me.HmiComboBox_CellControl.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_CellControl.Name = "HmiComboBox_CellControl"
        Me.HmiComboBox_CellControl.Size = New System.Drawing.Size(148, 40)
        Me.HmiComboBox_CellControl.TabIndex = 6
        '
        'HmiComboBox_Method
        '
        Me.HmiComboBox_Method.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Method.Location = New System.Drawing.Point(91, 43)
        Me.HmiComboBox_Method.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Method.Name = "HmiComboBox_Method"
        Me.HmiComboBox_Method.Size = New System.Drawing.Size(148, 40)
        Me.HmiComboBox_Method.TabIndex = 10
        '
        'TableLayoutPanel_Bottom
        '
        Me.TableLayoutPanel_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body.SetColumnSpan(Me.TableLayoutPanel_Bottom, 3)
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Bottom.Location = New System.Drawing.Point(1, 85)
        Me.TableLayoutPanel_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Bottom.Name = "TableLayoutPanel_Bottom"
        Me.TableLayoutPanel_Bottom.RowCount = 1
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Bottom.Size = New System.Drawing.Size(298, 305)
        Me.TableLayoutPanel_Bottom.TabIndex = 11
        '
        'OpenFileDialogTpy
        '
        Me.OpenFileDialogTpy.FileName = "*.tpy"
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
    Friend WithEvents HmiLabel_CellControl As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Method As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents OpenFileDialogTpy As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_CellControl As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Method As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Bottom As System.Windows.Forms.TableLayoutPanel
End Class
