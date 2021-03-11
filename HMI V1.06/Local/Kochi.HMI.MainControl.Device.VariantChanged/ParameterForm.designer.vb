<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParameterForm
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
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.Label_AdsLength = New System.Windows.Forms.Label()
        Me.Label_AdsType = New System.Windows.Forms.Label()
        Me.Label_AdsName = New System.Windows.Forms.Label()
        Me.Label_VariantName = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VariantChange_Y = New System.Windows.Forms.RadioButton()
        Me.VariantChange_N = New System.Windows.Forms.RadioButton()
        Me.Label_VariantChange = New System.Windows.Forms.Label()
        Me.TextBox_Text = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Reserve = New System.Windows.Forms.TableLayoutPanel()
        Me.ReadOnly_Y = New System.Windows.Forms.RadioButton()
        Me.ReadOnly_N = New System.Windows.Forms.RadioButton()
        Me.Label_ID = New System.Windows.Forms.Label()
        Me.TextBox_ID = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label_ReadOnly = New System.Windows.Forms.Label()
        Me.Label_Text = New System.Windows.Forms.Label()
        Me.HmiComboBox_VariantName = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiTextBox_AdsName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiComboBox_AdsType = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiTextBox_AdsLength = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel_Reserve.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.06358!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.93642!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_AdsLength, 0, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_AdsType, 0, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_AdsName, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_VariantName, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel1, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_VariantChange, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_Text, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Reserve, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ID, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 1, 8)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ReadOnly, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Text, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_VariantName, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_AdsName, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_AdsType, 1, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTextBox_AdsLength, 1, 7)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 9
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(533, 424)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'Label_AdsLength
        '
        Me.Label_AdsLength.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_AdsLength.AutoSize = True
        Me.Label_AdsLength.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_AdsLength.Location = New System.Drawing.Point(52, 285)
        Me.Label_AdsLength.Name = "Label_AdsLength"
        Me.Label_AdsLength.Size = New System.Drawing.Size(125, 29)
        Me.Label_AdsLength.TabIndex = 32
        Me.Label_AdsLength.Text = "AdsLength:"
        Me.Label_AdsLength.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_AdsType
        '
        Me.Label_AdsType.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_AdsType.AutoSize = True
        Me.Label_AdsType.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_AdsType.Location = New System.Drawing.Point(62, 245)
        Me.Label_AdsType.Name = "Label_AdsType"
        Me.Label_AdsType.Size = New System.Drawing.Size(105, 29)
        Me.Label_AdsType.TabIndex = 28
        Me.Label_AdsType.Text = "AdsType:"
        Me.Label_AdsType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_AdsName
        '
        Me.Label_AdsName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_AdsName.AutoSize = True
        Me.Label_AdsName.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_AdsName.Location = New System.Drawing.Point(55, 205)
        Me.Label_AdsName.Name = "Label_AdsName"
        Me.Label_AdsName.Size = New System.Drawing.Size(118, 29)
        Me.Label_AdsName.TabIndex = 27
        Me.Label_AdsName.Text = "AdsName:"
        Me.Label_AdsName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_VariantName
        '
        Me.Label_VariantName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_VariantName.AutoSize = True
        Me.Label_VariantName.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_VariantName.Location = New System.Drawing.Point(38, 165)
        Me.Label_VariantName.Name = "Label_VariantName"
        Me.Label_VariantName.Size = New System.Drawing.Size(153, 29)
        Me.Label_VariantName.TabIndex = 26
        Me.Label_VariantName.Text = "VariantName:"
        Me.Label_VariantName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VariantChange_Y, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VariantChange_N, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(230, 121)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(302, 38)
        Me.TableLayoutPanel1.TabIndex = 25
        '
        'VariantChange_Y
        '
        Me.VariantChange_Y.AutoSize = True
        Me.VariantChange_Y.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.VariantChange_Y.Location = New System.Drawing.Point(3, 3)
        Me.VariantChange_Y.Name = "VariantChange_Y"
        Me.VariantChange_Y.Size = New System.Drawing.Size(43, 32)
        Me.VariantChange_Y.TabIndex = 0
        Me.VariantChange_Y.TabStop = True
        Me.VariantChange_Y.Text = "Y"
        Me.VariantChange_Y.UseVisualStyleBackColor = True
        '
        'VariantChange_N
        '
        Me.VariantChange_N.AutoSize = True
        Me.VariantChange_N.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.VariantChange_N.Location = New System.Drawing.Point(154, 3)
        Me.VariantChange_N.Name = "VariantChange_N"
        Me.VariantChange_N.Size = New System.Drawing.Size(47, 32)
        Me.VariantChange_N.TabIndex = 1
        Me.VariantChange_N.TabStop = True
        Me.VariantChange_N.Text = "N"
        Me.VariantChange_N.UseVisualStyleBackColor = True
        '
        'Label_VariantChange
        '
        Me.Label_VariantChange.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_VariantChange.AutoSize = True
        Me.Label_VariantChange.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_VariantChange.Location = New System.Drawing.Point(31, 125)
        Me.Label_VariantChange.Name = "Label_VariantChange"
        Me.Label_VariantChange.Size = New System.Drawing.Size(167, 29)
        Me.Label_VariantChange.TabIndex = 24
        Me.Label_VariantChange.Text = "VariantChange:"
        Me.Label_VariantChange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_Text
        '
        Me.TextBox_Text.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Text.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_Text.Location = New System.Drawing.Point(232, 43)
        Me.TextBox_Text.Name = "TextBox_Text"
        Me.TextBox_Text.Size = New System.Drawing.Size(298, 37)
        Me.TextBox_Text.TabIndex = 23
        '
        'TableLayoutPanel_Reserve
        '
        Me.TableLayoutPanel_Reserve.ColumnCount = 2
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.ReadOnly_Y, 0, 0)
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.ReadOnly_N, 1, 0)
        Me.TableLayoutPanel_Reserve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Reserve.Location = New System.Drawing.Point(230, 81)
        Me.TableLayoutPanel_Reserve.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Reserve.Name = "TableLayoutPanel_Reserve"
        Me.TableLayoutPanel_Reserve.RowCount = 1
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Size = New System.Drawing.Size(302, 38)
        Me.TableLayoutPanel_Reserve.TabIndex = 19
        '
        'ReadOnly_Y
        '
        Me.ReadOnly_Y.AutoSize = True
        Me.ReadOnly_Y.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.ReadOnly_Y.Location = New System.Drawing.Point(3, 3)
        Me.ReadOnly_Y.Name = "ReadOnly_Y"
        Me.ReadOnly_Y.Size = New System.Drawing.Size(43, 32)
        Me.ReadOnly_Y.TabIndex = 0
        Me.ReadOnly_Y.TabStop = True
        Me.ReadOnly_Y.Text = "Y"
        Me.ReadOnly_Y.UseVisualStyleBackColor = True
        '
        'ReadOnly_N
        '
        Me.ReadOnly_N.AutoSize = True
        Me.ReadOnly_N.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.ReadOnly_N.Location = New System.Drawing.Point(154, 3)
        Me.ReadOnly_N.Name = "ReadOnly_N"
        Me.ReadOnly_N.Size = New System.Drawing.Size(47, 32)
        Me.ReadOnly_N.TabIndex = 1
        Me.ReadOnly_N.TabStop = True
        Me.ReadOnly_N.Text = "N"
        Me.ReadOnly_N.UseVisualStyleBackColor = True
        '
        'Label_ID
        '
        Me.Label_ID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ID.AutoSize = True
        Me.Label_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ID.Location = New System.Drawing.Point(89, 5)
        Me.Label_ID.Name = "Label_ID"
        Me.Label_ID.Size = New System.Drawing.Size(51, 29)
        Me.Label_ID.TabIndex = 9
        Me.Label_ID.Text = "  ID:"
        Me.Label_ID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_ID
        '
        Me.TextBox_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_ID.Location = New System.Drawing.Point(232, 3)
        Me.TextBox_ID.Name = "TextBox_ID"
        Me.TextBox_ID.ReadOnly = True
        Me.TextBox_ID.Size = New System.Drawing.Size(298, 37)
        Me.TextBox_ID.TabIndex = 8
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Cancel, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Save, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(232, 323)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(298, 98)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 11
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Cancel.Location = New System.Drawing.Point(157, 8)
        Me.Button_Cancel.Margin = New System.Windows.Forms.Padding(8)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(133, 115)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Save.Location = New System.Drawing.Point(8, 8)
        Me.Button_Save.Margin = New System.Windows.Forms.Padding(8)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(133, 115)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Label_ReadOnly
        '
        Me.Label_ReadOnly.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ReadOnly.AutoSize = True
        Me.Label_ReadOnly.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ReadOnly.Location = New System.Drawing.Point(56, 85)
        Me.Label_ReadOnly.Name = "Label_ReadOnly"
        Me.Label_ReadOnly.Size = New System.Drawing.Size(117, 29)
        Me.Label_ReadOnly.TabIndex = 18
        Me.Label_ReadOnly.Text = "ReadOnly:"
        Me.Label_ReadOnly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Text
        '
        Me.Label_Text.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Text.AutoSize = True
        Me.Label_Text.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Text.Location = New System.Drawing.Point(84, 45)
        Me.Label_Text.Name = "Label_Text"
        Me.Label_Text.Size = New System.Drawing.Size(61, 29)
        Me.Label_Text.TabIndex = 22
        Me.Label_Text.Text = "Text:"
        Me.Label_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiComboBox_VariantName
        '
        Me.HmiComboBox_VariantName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_VariantName.Location = New System.Drawing.Point(232, 163)
        Me.HmiComboBox_VariantName.Name = "HmiComboBox_VariantName"
        Me.HmiComboBox_VariantName.Size = New System.Drawing.Size(298, 34)
        Me.HmiComboBox_VariantName.TabIndex = 29
        '
        'HmiTextBox_AdsName
        '
        Me.HmiTextBox_AdsName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AdsName.Location = New System.Drawing.Point(232, 203)
        Me.HmiTextBox_AdsName.Name = "HmiTextBox_AdsName"
        Me.HmiTextBox_AdsName.Number = 0
        Me.HmiTextBox_AdsName.Size = New System.Drawing.Size(298, 34)
        Me.HmiTextBox_AdsName.TabIndex = 30
        Me.HmiTextBox_AdsName.TextBoxReadOnly = False
        Me.HmiTextBox_AdsName.ValueType = GetType(String)
        '
        'HmiComboBox_AdsType
        '
        Me.HmiComboBox_AdsType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_AdsType.Location = New System.Drawing.Point(232, 243)
        Me.HmiComboBox_AdsType.Name = "HmiComboBox_AdsType"
        Me.HmiComboBox_AdsType.Size = New System.Drawing.Size(298, 34)
        Me.HmiComboBox_AdsType.TabIndex = 31
        '
        'HmiTextBox_AdsLength
        '
        Me.HmiTextBox_AdsLength.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AdsLength.Location = New System.Drawing.Point(232, 283)
        Me.HmiTextBox_AdsLength.Name = "HmiTextBox_AdsLength"
        Me.HmiTextBox_AdsLength.Number = 0
        Me.HmiTextBox_AdsLength.Size = New System.Drawing.Size(298, 34)
        Me.HmiTextBox_AdsLength.TabIndex = 33
        Me.HmiTextBox_AdsLength.TextBoxReadOnly = False
        Me.HmiTextBox_AdsLength.ValueType = GetType(String)
        '
        'ParameterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 424)
        Me.ControlBox = False
        Me.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Name = "ParameterForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ParameterForm"
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel_Reserve.ResumeLayout(False)
        Me.TableLayoutPanel_Reserve.PerformLayout()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VariantChange_Y As System.Windows.Forms.RadioButton
    Friend WithEvents VariantChange_N As System.Windows.Forms.RadioButton
    Friend WithEvents Label_VariantChange As System.Windows.Forms.Label
    Friend WithEvents TextBox_Text As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Reserve As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ReadOnly_Y As System.Windows.Forms.RadioButton
    Friend WithEvents ReadOnly_N As System.Windows.Forms.RadioButton
    Friend WithEvents Label_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_ID As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Label_ReadOnly As System.Windows.Forms.Label
    Friend WithEvents Label_Text As System.Windows.Forms.Label
    Friend WithEvents Label_AdsType As System.Windows.Forms.Label
    Friend WithEvents Label_AdsName As System.Windows.Forms.Label
    Friend WithEvents Label_VariantName As System.Windows.Forms.Label
    Friend WithEvents HmiComboBox_VariantName As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_AdsName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiComboBox_AdsType As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents Label_AdsLength As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_AdsLength As Kochi.HMI.MainControl.UI.HMITextBox
End Class
