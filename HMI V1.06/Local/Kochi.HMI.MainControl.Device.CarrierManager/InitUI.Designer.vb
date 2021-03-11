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
        Me.HmiTextBox_Password = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_UserName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Address = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Password = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_UserName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Address = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Operation = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Operation = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_PLCAds = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PLCAds = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Enable = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
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
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 267)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Enable, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_PLCAds, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_PLCAds, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Password, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_UserName, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Address, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Password, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_UserName, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Address, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Operation, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Operation, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 7
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(303, 267)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'HmiTextBox_Password
        '
        Me.HmiTextBox_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Password.Location = New System.Drawing.Point(109, 171)
        Me.HmiTextBox_Password.Name = "HmiTextBox_Password"
        Me.HmiTextBox_Password.Number = 0
        Me.HmiTextBox_Password.Size = New System.Drawing.Size(145, 36)
        Me.HmiTextBox_Password.TabIndex = 12
        Me.HmiTextBox_Password.TextBoxReadOnly = False
        Me.HmiTextBox_Password.ValueType = GetType(String)
        '
        'HmiTextBox_UserName
        '
        Me.HmiTextBox_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_UserName.Location = New System.Drawing.Point(109, 129)
        Me.HmiTextBox_UserName.Name = "HmiTextBox_UserName"
        Me.HmiTextBox_UserName.Number = 0
        Me.HmiTextBox_UserName.Size = New System.Drawing.Size(145, 36)
        Me.HmiTextBox_UserName.TabIndex = 11
        Me.HmiTextBox_UserName.TextBoxReadOnly = False
        Me.HmiTextBox_UserName.ValueType = GetType(String)
        '
        'HmiTextBox_Address
        '
        Me.HmiTextBox_Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Address.Location = New System.Drawing.Point(109, 87)
        Me.HmiTextBox_Address.Name = "HmiTextBox_Address"
        Me.HmiTextBox_Address.Number = 0
        Me.HmiTextBox_Address.Size = New System.Drawing.Size(145, 36)
        Me.HmiTextBox_Address.TabIndex = 10
        Me.HmiTextBox_Address.TextBoxReadOnly = False
        Me.HmiTextBox_Address.ValueType = GetType(String)
        '
        'HmiLabel_Password
        '
        Me.HmiLabel_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Password.Location = New System.Drawing.Point(3, 171)
        Me.HmiLabel_Password.Name = "HmiLabel_Password"
        Me.HmiLabel_Password.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_Password.TabIndex = 9
        '
        'HmiLabel_UserName
        '
        Me.HmiLabel_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_UserName.Location = New System.Drawing.Point(3, 129)
        Me.HmiLabel_UserName.Name = "HmiLabel_UserName"
        Me.HmiLabel_UserName.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_UserName.TabIndex = 8
        '
        'HmiLabel_Address
        '
        Me.HmiLabel_Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Address.Location = New System.Drawing.Point(3, 87)
        Me.HmiLabel_Address.Name = "HmiLabel_Address"
        Me.HmiLabel_Address.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_Address.TabIndex = 7
        '
        'HmiTextBox_Operation
        '
        Me.HmiTextBox_Operation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Operation.Location = New System.Drawing.Point(109, 45)
        Me.HmiTextBox_Operation.Name = "HmiTextBox_Operation"
        Me.HmiTextBox_Operation.Number = 0
        Me.HmiTextBox_Operation.Size = New System.Drawing.Size(145, 36)
        Me.HmiTextBox_Operation.TabIndex = 6
        Me.HmiTextBox_Operation.TextBoxReadOnly = False
        Me.HmiTextBox_Operation.ValueType = GetType(String)
        '
        'HmiLabel_Operation
        '
        Me.HmiLabel_Operation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Operation.Location = New System.Drawing.Point(3, 45)
        Me.HmiLabel_Operation.Name = "HmiLabel_Operation"
        Me.HmiLabel_Operation.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_Operation.TabIndex = 5
        '
        'HmiLabel_PLCAds
        '
        Me.HmiLabel_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PLCAds.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_PLCAds.Name = "HmiLabel_PLCAds"
        Me.HmiLabel_PLCAds.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_PLCAds.TabIndex = 13
        '
        'HmiTextBox_PLCAds
        '
        Me.HmiTextBox_PLCAds.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PLCAds.Location = New System.Drawing.Point(109, 3)
        Me.HmiTextBox_PLCAds.Name = "HmiTextBox_PLCAds"
        Me.HmiTextBox_PLCAds.Number = 0
        Me.HmiTextBox_PLCAds.Size = New System.Drawing.Size(145, 36)
        Me.HmiTextBox_PLCAds.TabIndex = 14
        Me.HmiTextBox_PLCAds.TextBoxReadOnly = False
        Me.HmiTextBox_PLCAds.ValueType = GetType(String)
        '
        'HmiLabel_Enable
        '
        Me.HmiLabel_Enable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Enable.Location = New System.Drawing.Point(3, 213)
        Me.HmiLabel_Enable.Name = "HmiLabel_Enable"
        Me.HmiLabel_Enable.Size = New System.Drawing.Size(100, 36)
        Me.HmiLabel_Enable.TabIndex = 15
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(109, 213)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(145, 36)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 29
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_N.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton_N.Location = New System.Drawing.Point(75, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(67, 30)
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
        Me.RadioButton_Y.Size = New System.Drawing.Size(66, 30)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 267)
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
    Friend WithEvents HmiLabel_Operation As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Operation As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Password As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_UserName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Address As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Password As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_UserName As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Address As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_PLCAds As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PLCAds As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Enable As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
End Class
