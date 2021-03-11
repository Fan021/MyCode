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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_NotInqueue2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_NotInqueue2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_NotInqueue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_NotInqueue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Password = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_UserName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Address = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Password = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_UserName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Address = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Operation = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Operation = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ResourceId = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ResourceId = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_NCCode = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_NCRetryCode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_NCRetryCode = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_NCCode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_NCCode = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Pandel_Body.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.HmiTableLayoutPanel_NCCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TabControl1)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(303, 308)
        Me.Pandel_Body.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(303, 308)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_Body)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(295, 282)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.AutoSize = True
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_NotInqueue2, 1, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_NotInqueue2, 0, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_NotInqueue, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_NotInqueue, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Password, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_UserName, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Address, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Password, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_UserName, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Address, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_Operation, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_Operation, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_ResourceId, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiLabel_ResourceId, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 8
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(289, 276)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'HmiTextBox_NotInqueue2
        '
        Me.HmiTextBox_NotInqueue2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_NotInqueue2.Location = New System.Drawing.Point(104, 237)
        Me.HmiTextBox_NotInqueue2.Name = "HmiTextBox_NotInqueue2"
        Me.HmiTextBox_NotInqueue2.Number = 0
        Me.HmiTextBox_NotInqueue2.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_NotInqueue2.TabIndex = 16
        Me.HmiTextBox_NotInqueue2.TextBoxReadOnly = False
        Me.HmiTextBox_NotInqueue2.ValueType = GetType(String)
        '
        'HmiLabel_NotInqueue2
        '
        Me.HmiLabel_NotInqueue2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_NotInqueue2.Location = New System.Drawing.Point(3, 237)
        Me.HmiLabel_NotInqueue2.Name = "HmiLabel_NotInqueue2"
        Me.HmiLabel_NotInqueue2.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_NotInqueue2.TabIndex = 15
        '
        'HmiTextBox_NotInqueue
        '
        Me.HmiTextBox_NotInqueue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_NotInqueue.Location = New System.Drawing.Point(104, 198)
        Me.HmiTextBox_NotInqueue.Name = "HmiTextBox_NotInqueue"
        Me.HmiTextBox_NotInqueue.Number = 0
        Me.HmiTextBox_NotInqueue.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_NotInqueue.TabIndex = 14
        Me.HmiTextBox_NotInqueue.TextBoxReadOnly = False
        Me.HmiTextBox_NotInqueue.ValueType = GetType(String)
        '
        'HmiLabel_NotInqueue
        '
        Me.HmiLabel_NotInqueue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_NotInqueue.Location = New System.Drawing.Point(3, 198)
        Me.HmiLabel_NotInqueue.Name = "HmiLabel_NotInqueue"
        Me.HmiLabel_NotInqueue.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_NotInqueue.TabIndex = 13
        '
        'HmiTextBox_Password
        '
        Me.HmiTextBox_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Password.Location = New System.Drawing.Point(104, 159)
        Me.HmiTextBox_Password.Name = "HmiTextBox_Password"
        Me.HmiTextBox_Password.Number = 0
        Me.HmiTextBox_Password.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_Password.TabIndex = 12
        Me.HmiTextBox_Password.TextBoxReadOnly = False
        Me.HmiTextBox_Password.ValueType = GetType(String)
        '
        'HmiTextBox_UserName
        '
        Me.HmiTextBox_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_UserName.Location = New System.Drawing.Point(104, 120)
        Me.HmiTextBox_UserName.Name = "HmiTextBox_UserName"
        Me.HmiTextBox_UserName.Number = 0
        Me.HmiTextBox_UserName.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_UserName.TabIndex = 11
        Me.HmiTextBox_UserName.TextBoxReadOnly = False
        Me.HmiTextBox_UserName.ValueType = GetType(String)
        '
        'HmiTextBox_Address
        '
        Me.HmiTextBox_Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Address.Location = New System.Drawing.Point(104, 81)
        Me.HmiTextBox_Address.Name = "HmiTextBox_Address"
        Me.HmiTextBox_Address.Number = 0
        Me.HmiTextBox_Address.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_Address.TabIndex = 10
        Me.HmiTextBox_Address.TextBoxReadOnly = False
        Me.HmiTextBox_Address.ValueType = GetType(String)
        '
        'HmiLabel_Password
        '
        Me.HmiLabel_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Password.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_Password.Name = "HmiLabel_Password"
        Me.HmiLabel_Password.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_Password.TabIndex = 9
        '
        'HmiLabel_UserName
        '
        Me.HmiLabel_UserName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_UserName.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_UserName.Name = "HmiLabel_UserName"
        Me.HmiLabel_UserName.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_UserName.TabIndex = 8
        '
        'HmiLabel_Address
        '
        Me.HmiLabel_Address.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Address.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Address.Name = "HmiLabel_Address"
        Me.HmiLabel_Address.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_Address.TabIndex = 7
        '
        'HmiTextBox_Operation
        '
        Me.HmiTextBox_Operation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Operation.Location = New System.Drawing.Point(104, 42)
        Me.HmiTextBox_Operation.Name = "HmiTextBox_Operation"
        Me.HmiTextBox_Operation.Number = 0
        Me.HmiTextBox_Operation.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_Operation.TabIndex = 6
        Me.HmiTextBox_Operation.TextBoxReadOnly = False
        Me.HmiTextBox_Operation.ValueType = GetType(String)
        '
        'HmiLabel_Operation
        '
        Me.HmiLabel_Operation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Operation.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Operation.Name = "HmiLabel_Operation"
        Me.HmiLabel_Operation.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_Operation.TabIndex = 5
        '
        'HmiTextBox_ResourceId
        '
        Me.HmiTextBox_ResourceId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ResourceId.Location = New System.Drawing.Point(104, 3)
        Me.HmiTextBox_ResourceId.Name = "HmiTextBox_ResourceId"
        Me.HmiTextBox_ResourceId.Number = 0
        Me.HmiTextBox_ResourceId.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_ResourceId.TabIndex = 1
        Me.HmiTextBox_ResourceId.TextBoxReadOnly = False
        Me.HmiTextBox_ResourceId.ValueType = GetType(String)
        '
        'HmiLabel_ResourceId
        '
        Me.HmiLabel_ResourceId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ResourceId.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_ResourceId.Name = "HmiLabel_ResourceId"
        Me.HmiLabel_ResourceId.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_ResourceId.TabIndex = 4
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.HmiTableLayoutPanel_NCCode)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(295, 282)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_NCCode
        '
        Me.HmiTableLayoutPanel_NCCode.AutoSize = True
        Me.HmiTableLayoutPanel_NCCode.ColumnCount = 3
        Me.HmiTableLayoutPanel_NCCode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.HmiTableLayoutPanel_NCCode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_NCCode.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel_NCCode.Controls.Add(Me.HmiTextBox_NCRetryCode, 1, 1)
        Me.HmiTableLayoutPanel_NCCode.Controls.Add(Me.HmiLabel_NCRetryCode, 0, 1)
        Me.HmiTableLayoutPanel_NCCode.Controls.Add(Me.HmiTextBox_NCCode, 1, 0)
        Me.HmiTableLayoutPanel_NCCode.Controls.Add(Me.HmiLabel_NCCode, 0, 0)
        Me.HmiTableLayoutPanel_NCCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_NCCode.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_NCCode.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_NCCode.Name = "HmiTableLayoutPanel_NCCode"
        Me.HmiTableLayoutPanel_NCCode.RowCount = 3
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_NCCode.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_NCCode.Size = New System.Drawing.Size(289, 276)
        Me.HmiTableLayoutPanel_NCCode.TabIndex = 3
        '
        'HmiTextBox_NCRetryCode
        '
        Me.HmiTextBox_NCRetryCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_NCRetryCode.Location = New System.Drawing.Point(104, 42)
        Me.HmiTextBox_NCRetryCode.Name = "HmiTextBox_NCRetryCode"
        Me.HmiTextBox_NCRetryCode.Number = 0
        Me.HmiTextBox_NCRetryCode.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_NCRetryCode.TabIndex = 6
        Me.HmiTextBox_NCRetryCode.TextBoxReadOnly = False
        Me.HmiTextBox_NCRetryCode.ValueType = GetType(String)
        '
        'HmiLabel_NCRetryCode
        '
        Me.HmiLabel_NCRetryCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_NCRetryCode.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_NCRetryCode.Name = "HmiLabel_NCRetryCode"
        Me.HmiLabel_NCRetryCode.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_NCRetryCode.TabIndex = 5
        '
        'HmiTextBox_NCCode
        '
        Me.HmiTextBox_NCCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_NCCode.Location = New System.Drawing.Point(104, 3)
        Me.HmiTextBox_NCCode.Name = "HmiTextBox_NCCode"
        Me.HmiTextBox_NCCode.Number = 0
        Me.HmiTextBox_NCCode.Size = New System.Drawing.Size(138, 33)
        Me.HmiTextBox_NCCode.TabIndex = 1
        Me.HmiTextBox_NCCode.TextBoxReadOnly = False
        Me.HmiTextBox_NCCode.ValueType = GetType(String)
        '
        'HmiLabel_NCCode
        '
        Me.HmiLabel_NCCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_NCCode.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_NCCode.Name = "HmiLabel_NCCode"
        Me.HmiLabel_NCCode.Size = New System.Drawing.Size(95, 33)
        Me.HmiLabel_NCCode.TabIndex = 4
        '
        'InitUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 308)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InitUI"
        Me.Text = "Form1"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.HmiTableLayoutPanel_NCCode.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_NotInqueue2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_NotInqueue2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_NotInqueue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_NotInqueue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Password As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_UserName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Address As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Password As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_UserName As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Address As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Operation As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Operation As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ResourceId As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ResourceId As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents HmiTableLayoutPanel_NCCode As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_NCRetryCode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_NCRetryCode As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_NCCode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_NCCode As Kochi.HMI.MainControl.UI.HMILabel
End Class
