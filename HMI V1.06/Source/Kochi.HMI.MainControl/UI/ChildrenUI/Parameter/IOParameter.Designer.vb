<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOParameter
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
        Me.TabControl_IO = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_UserLevel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Output = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Input = New System.Windows.Forms.RadioButton()
        Me.Label_Type = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Reserve = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.Label_Reserve = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Type = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Tap = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Toggle = New System.Windows.Forms.RadioButton()
        Me.Label_Trigger = New System.Windows.Forms.Label()
        Me.TextBox_NameA2 = New System.Windows.Forms.TextBox()
        Me.Label_NameA2 = New System.Windows.Forms.Label()
        Me.Label_ID = New System.Windows.Forms.Label()
        Me.TextBox_ID = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Cancel = New System.Windows.Forms.Button()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.Label_NameA = New System.Windows.Forms.Label()
        Me.TextBox_NameA = New System.Windows.Forms.TextBox()
        Me.HmiComboBox_Level = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabControl_IO.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel_Reserve.SuspendLayout()
        Me.TableLayoutPanel_Type.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_IO
        '
        Me.TabControl_IO.Controls.Add(Me.TabPage1)
        Me.TabControl_IO.Controls.Add(Me.TabPage2)
        Me.TabControl_IO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_IO.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_IO.Name = "TabControl_IO"
        Me.TabControl_IO.SelectedIndex = 0
        Me.TabControl_IO.Size = New System.Drawing.Size(533, 422)
        Me.TabControl_IO.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_Body)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(525, 396)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.06358!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.93642!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_UserLevel, 0, 6)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel1, 1, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Type, 0, 3)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Reserve, 1, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Reserve, 0, 5)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Type, 1, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_Trigger, 0, 4)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA2, 1, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA2, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_ID, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 1, 7)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Label_NameA, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TextBox_NameA, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiComboBox_Level, 1, 6)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 8
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(519, 390)
        Me.TableLayoutPanel_Body.TabIndex = 1
        '
        'Label_UserLevel
        '
        Me.Label_UserLevel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_UserLevel.AutoSize = True
        Me.Label_UserLevel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_UserLevel.Location = New System.Drawing.Point(76, 245)
        Me.Label_UserLevel.Name = "Label_UserLevel"
        Me.Label_UserLevel.Size = New System.Drawing.Size(71, 29)
        Me.Label_UserLevel.TabIndex = 30
        Me.Label_UserLevel.Text = "Level:"
        Me.Label_UserLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RadioButton_Output, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RadioButton_Input, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(224, 121)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel1.TabIndex = 29
        '
        'RadioButton_Output
        '
        Me.RadioButton_Output.AutoSize = True
        Me.RadioButton_Output.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Output.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_Output.Name = "RadioButton_Output"
        Me.RadioButton_Output.Size = New System.Drawing.Size(102, 32)
        Me.RadioButton_Output.TabIndex = 1
        Me.RadioButton_Output.TabStop = True
        Me.RadioButton_Output.Text = "Output"
        Me.RadioButton_Output.UseVisualStyleBackColor = True
        '
        'RadioButton_Input
        '
        Me.RadioButton_Input.AutoSize = True
        Me.RadioButton_Input.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Input.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Input.Name = "RadioButton_Input"
        Me.RadioButton_Input.Size = New System.Drawing.Size(84, 32)
        Me.RadioButton_Input.TabIndex = 0
        Me.RadioButton_Input.TabStop = True
        Me.RadioButton_Input.Text = "Input"
        Me.RadioButton_Input.UseVisualStyleBackColor = True
        '
        'Label_Type
        '
        Me.Label_Type.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Type.AutoSize = True
        Me.Label_Type.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Type.Location = New System.Drawing.Point(78, 125)
        Me.Label_Type.Name = "Label_Type"
        Me.Label_Type.Size = New System.Drawing.Size(67, 29)
        Me.Label_Type.TabIndex = 28
        Me.Label_Type.Text = "Type:"
        Me.Label_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Reserve
        '
        Me.TableLayoutPanel_Reserve.ColumnCount = 2
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Reserve.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Reserve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Reserve.Location = New System.Drawing.Point(224, 201)
        Me.TableLayoutPanel_Reserve.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Reserve.Name = "TableLayoutPanel_Reserve"
        Me.TableLayoutPanel_Reserve.RowCount = 1
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Reserve.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel_Reserve.TabIndex = 27
        '
        'RadioButton_Y
        '
        Me.RadioButton_Y.AutoSize = True
        Me.RadioButton_Y.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Y.Name = "RadioButton_Y"
        Me.RadioButton_Y.Size = New System.Drawing.Size(43, 32)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_N.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(47, 32)
        Me.RadioButton_N.TabIndex = 1
        Me.RadioButton_N.TabStop = True
        Me.RadioButton_N.Text = "N"
        Me.RadioButton_N.UseVisualStyleBackColor = True
        '
        'Label_Reserve
        '
        Me.Label_Reserve.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Reserve.AutoSize = True
        Me.Label_Reserve.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Reserve.Location = New System.Drawing.Point(61, 205)
        Me.Label_Reserve.Name = "Label_Reserve"
        Me.Label_Reserve.Size = New System.Drawing.Size(100, 29)
        Me.Label_Reserve.TabIndex = 26
        Me.Label_Reserve.Text = "Reserve:"
        Me.Label_Reserve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Type
        '
        Me.TableLayoutPanel_Type.ColumnCount = 2
        Me.TableLayoutPanel_Type.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.Controls.Add(Me.RadioButton_Tap, 1, 0)
        Me.TableLayoutPanel_Type.Controls.Add(Me.RadioButton_Toggle, 0, 0)
        Me.TableLayoutPanel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Type.Location = New System.Drawing.Point(224, 161)
        Me.TableLayoutPanel_Type.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Type.Name = "TableLayoutPanel_Type"
        Me.TableLayoutPanel_Type.RowCount = 1
        Me.TableLayoutPanel_Type.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Type.Size = New System.Drawing.Size(294, 38)
        Me.TableLayoutPanel_Type.TabIndex = 25
        '
        'RadioButton_Tap
        '
        Me.RadioButton_Tap.AutoSize = True
        Me.RadioButton_Tap.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Tap.Location = New System.Drawing.Point(150, 3)
        Me.RadioButton_Tap.Name = "RadioButton_Tap"
        Me.RadioButton_Tap.Size = New System.Drawing.Size(66, 32)
        Me.RadioButton_Tap.TabIndex = 1
        Me.RadioButton_Tap.TabStop = True
        Me.RadioButton_Tap.Text = "Tap"
        Me.RadioButton_Tap.UseVisualStyleBackColor = True
        '
        'RadioButton_Toggle
        '
        Me.RadioButton_Toggle.AutoSize = True
        Me.RadioButton_Toggle.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.RadioButton_Toggle.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Toggle.Name = "RadioButton_Toggle"
        Me.RadioButton_Toggle.Size = New System.Drawing.Size(94, 32)
        Me.RadioButton_Toggle.TabIndex = 0
        Me.RadioButton_Toggle.TabStop = True
        Me.RadioButton_Toggle.Text = "Toggle"
        Me.RadioButton_Toggle.UseVisualStyleBackColor = True
        '
        'Label_Trigger
        '
        Me.Label_Trigger.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_Trigger.AutoSize = True
        Me.Label_Trigger.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_Trigger.Location = New System.Drawing.Point(67, 165)
        Me.Label_Trigger.Name = "Label_Trigger"
        Me.Label_Trigger.Size = New System.Drawing.Size(89, 29)
        Me.Label_Trigger.TabIndex = 24
        Me.Label_Trigger.Text = "Trigger:"
        Me.Label_Trigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_NameA2
        '
        Me.TextBox_NameA2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameA2.Location = New System.Drawing.Point(226, 83)
        Me.TextBox_NameA2.Name = "TextBox_NameA2"
        Me.TextBox_NameA2.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameA2.TabIndex = 23
        '
        'Label_NameA2
        '
        Me.Label_NameA2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA2.AutoSize = True
        Me.Label_NameA2.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA2.Location = New System.Drawing.Point(73, 85)
        Me.Label_NameA2.Name = "Label_NameA2"
        Me.Label_NameA2.Size = New System.Drawing.Size(76, 29)
        Me.Label_NameA2.TabIndex = 22
        Me.Label_NameA2.Text = "TextA:"
        Me.Label_NameA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_ID
        '
        Me.Label_ID.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_ID.AutoSize = True
        Me.Label_ID.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_ID.Location = New System.Drawing.Point(86, 5)
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
        Me.TextBox_ID.Location = New System.Drawing.Point(226, 3)
        Me.TextBox_ID.Name = "TextBox_ID"
        Me.TextBox_ID.ReadOnly = True
        Me.TextBox_ID.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_ID.TabIndex = 8
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Cancel, 1, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Save, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(226, 283)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(290, 104)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 11
        '
        'Button_Cancel
        '
        Me.Button_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Cancel.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Cancel.Location = New System.Drawing.Point(148, 3)
        Me.Button_Cancel.Name = "Button_Cancel"
        Me.Button_Cancel.Size = New System.Drawing.Size(139, 98)
        Me.Button_Cancel.TabIndex = 1
        Me.Button_Cancel.Text = "Cancel"
        Me.Button_Cancel.UseVisualStyleBackColor = True
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Save.Location = New System.Drawing.Point(3, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(139, 98)
        Me.Button_Save.TabIndex = 2
        Me.Button_Save.Text = "Button1"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'Label_NameA
        '
        Me.Label_NameA.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label_NameA.AutoSize = True
        Me.Label_NameA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.Label_NameA.Location = New System.Drawing.Point(73, 45)
        Me.Label_NameA.Name = "Label_NameA"
        Me.Label_NameA.Size = New System.Drawing.Size(76, 29)
        Me.Label_NameA.TabIndex = 12
        Me.Label_NameA.Text = "TextA:"
        Me.Label_NameA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBox_NameA
        '
        Me.TextBox_NameA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_NameA.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox_NameA.Location = New System.Drawing.Point(226, 43)
        Me.TextBox_NameA.Name = "TextBox_NameA"
        Me.TextBox_NameA.Size = New System.Drawing.Size(290, 37)
        Me.TextBox_NameA.TabIndex = 13
        '
        'HmiComboBox_Level
        '
        Me.HmiComboBox_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Level.Location = New System.Drawing.Point(223, 240)
        Me.HmiComboBox_Level.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiComboBox_Level.Name = "HmiComboBox_Level"
        Me.HmiComboBox_Level.Size = New System.Drawing.Size(296, 40)
        Me.HmiComboBox_Level.TabIndex = 31
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(525, 396)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'IOParameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(533, 422)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl_IO)
        Me.Name = "IOParameter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "IOParameter"
        Me.TabControl_IO.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel_Reserve.ResumeLayout(False)
        Me.TableLayoutPanel_Reserve.PerformLayout()
        Me.TableLayoutPanel_Type.ResumeLayout(False)
        Me.TableLayoutPanel_Type.PerformLayout()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl_IO As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Output As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Input As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Type As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Reserve As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Reserve As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Type As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Tap As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Toggle As System.Windows.Forms.RadioButton
    Friend WithEvents Label_Trigger As System.Windows.Forms.Label
    Friend WithEvents TextBox_NameA2 As System.Windows.Forms.TextBox
    Friend WithEvents Label_NameA2 As System.Windows.Forms.Label
    Friend WithEvents Label_ID As System.Windows.Forms.Label
    Friend WithEvents TextBox_ID As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Cancel As System.Windows.Forms.Button
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents Label_NameA As System.Windows.Forms.Label
    Friend WithEvents TextBox_NameA As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label_UserLevel As System.Windows.Forms.Label
    Friend WithEvents HmiComboBox_Level As Kochi.HMI.MainControl.UI.HMIComboBox
End Class
