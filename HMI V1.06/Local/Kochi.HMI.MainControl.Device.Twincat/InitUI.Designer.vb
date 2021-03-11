Imports Kochi.HMI.MainControl.UI
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitUI
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
        Me.HmiTextBox_AmsNet = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_AmsPort = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_AmsNet = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_AmsPort = New Kochi.HMI.MainControl.UI.HMILabel()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_AmsNet, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_AmsPort, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_AmsNet, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_AmsPort, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_AmsNet
        '
        Me.HmiTextBox_AmsNet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AmsNet.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_AmsNet.Name = "HmiTextBox_AmsNet"
        Me.HmiTextBox_AmsNet.Number = 0
        Me.HmiTextBox_AmsNet.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_AmsNet.TabIndex = 0
        Me.HmiTextBox_AmsNet.TextBoxReadOnly = False
        Me.HmiTextBox_AmsNet.ValueType = GetType(String)
        '
        'HmiTextBox_AmsPort
        '
        Me.HmiTextBox_AmsPort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AmsPort.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_AmsPort.Name = "HmiTextBox_AmsPort"
        Me.HmiTextBox_AmsPort.Number = 0
        Me.HmiTextBox_AmsPort.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_AmsPort.TabIndex = 1
        Me.HmiTextBox_AmsPort.TextBoxReadOnly = False
        Me.HmiTextBox_AmsPort.ValueType = GetType(String)
        '
        'HmiLabel_AmsNet
        '
        Me.HmiLabel_AmsNet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AmsNet.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_AmsNet.Name = "HmiLabel_AmsNet"
        Me.HmiLabel_AmsNet.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_AmsNet.TabIndex = 3
        '
        'HmiLabel_AmsPort
        '
        Me.HmiLabel_AmsPort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AmsPort.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_AmsPort.Name = "HmiLabel_AmsPort"
        Me.HmiLabel_AmsPort.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_AmsPort.TabIndex = 4
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents HmiTextBox_AmsPort As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AmsPort As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_AmsNet As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AmsNet As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
End Class
