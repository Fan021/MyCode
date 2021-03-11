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
        Me.HmiLabel_TimeOut = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_TimeOut = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Port = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Port = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_IP = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_IP = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.HmiLabel_Enable = New Kochi.HMI.MainControl.UI.HMILabel()
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
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Enable, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_TimeOut, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_TimeOut, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Port, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Port, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_IP, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_IP, 0, 0)
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 246)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiLabel_TimeOut
        '
        Me.HmiLabel_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TimeOut.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_TimeOut.Name = "HmiLabel_TimeOut"
        Me.HmiLabel_TimeOut.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_TimeOut.TabIndex = 8
        '
        'HmiTextBox_TimeOut
        '
        Me.HmiTextBox_TimeOut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TimeOut.Location = New System.Drawing.Point(93, 81)
        Me.HmiTextBox_TimeOut.Name = "HmiTextBox_TimeOut"
        Me.HmiTextBox_TimeOut.Number = 0
        Me.HmiTextBox_TimeOut.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_TimeOut.TabIndex = 7
        Me.HmiTextBox_TimeOut.TextBoxReadOnly = False
        Me.HmiTextBox_TimeOut.ValueType = GetType(String)
        '
        'HmiTextBox_Port
        '
        Me.HmiTextBox_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Port.Location = New System.Drawing.Point(93, 42)
        Me.HmiTextBox_Port.Name = "HmiTextBox_Port"
        Me.HmiTextBox_Port.Number = 0
        Me.HmiTextBox_Port.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Port.TabIndex = 6
        Me.HmiTextBox_Port.TextBoxReadOnly = False
        Me.HmiTextBox_Port.ValueType = GetType(String)
        '
        'HmiLabel_Port
        '
        Me.HmiLabel_Port.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Port.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Port.Name = "HmiLabel_Port"
        Me.HmiLabel_Port.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_Port.TabIndex = 5
        '
        'HmiTextBox_IP
        '
        Me.HmiTextBox_IP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_IP.Location = New System.Drawing.Point(93, 3)
        Me.HmiTextBox_IP.Name = "HmiTextBox_IP"
        Me.HmiTextBox_IP.Number = 0
        Me.HmiTextBox_IP.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_IP.TabIndex = 1
        Me.HmiTextBox_IP.TextBoxReadOnly = False
        Me.HmiTextBox_IP.ValueType = GetType(String)
        '
        'HmiLabel_IP
        '
        Me.HmiLabel_IP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_IP.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_IP.Name = "HmiLabel_IP"
        Me.HmiLabel_IP.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_IP.TabIndex = 4
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(93, 120)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(145, 33)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 29
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
        Me.HmiLabel_Enable.Size = New System.Drawing.Size(84, 33)
        Me.HmiLabel_Enable.TabIndex = 30
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
    Friend WithEvents HmiTextBox_IP As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_IP As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Port As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Port As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_TimeOut As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_TimeOut As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Enable As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
End Class
