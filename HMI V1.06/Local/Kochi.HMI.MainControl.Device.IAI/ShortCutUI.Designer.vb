<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ShortCutUI
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
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiSensor_Ready = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel_Ready = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiPassFailButton3 = New Kochi.HMI.MainControl.UI.HMIPassFailButton(Me.components)
        Me.HmiPassFailButton1 = New Kochi.HMI.MainControl.UI.HMIPassFailButton(Me.components)
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_STPEnable = New Kochi.HMI.MainControl.UI.HMIButtonWithIndicate(Me.components)
        Me.HmiButton_MotorEnable = New Kochi.HMI.MainControl.UI.HMIButtonWithIndicate(Me.components)
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Pro = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Alarm = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Alarm = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.BackColor = System.Drawing.Color.White
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(467, 504)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Panel_Body_Bottom_Right
        '
        Me.Panel_Body_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Right.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Right.Name = "Panel_Body_Bottom_Right"
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(467, 504)
        Me.Panel_Body_Bottom_Right.TabIndex = 3
        '
        'HmiTableLayoutPanel_Body_Top_Right
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Alarm, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Alarm, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Ready, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Ready, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiPassFailButton3, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiPassFailButton1, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_STPEnable, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_MotorEnable, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Y, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Pro, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(465, 502)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiSensor_Ready
        '
        Me.HmiSensor_Ready.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Ready.Location = New System.Drawing.Point(80, 83)
        Me.HmiSensor_Ready.Name = "HmiSensor_Ready"
        Me.HmiSensor_Ready.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Ready.TabIndex = 41
        '
        'HmiLabel_Ready
        '
        Me.HmiLabel_Ready.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Ready.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_Ready.Name = "HmiLabel_Ready"
        Me.HmiLabel_Ready.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Ready.TabIndex = 40
        '
        'HmiPassFailButton3
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiPassFailButton3, 2)
        Me.HmiPassFailButton3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiPassFailButton3.Location = New System.Drawing.Point(160, 166)
        Me.HmiPassFailButton3.Margin = New System.Windows.Forms.Padding(6)
        Me.HmiPassFailButton3.Name = "HmiPassFailButton3"
        Me.HmiPassFailButton3.Size = New System.Drawing.Size(142, 28)
        Me.HmiPassFailButton3.TabIndex = 38
        Me.HmiPassFailButton3.Text = "HmiPassFailButton3"
        Me.HmiPassFailButton3.UseVisualStyleBackColor = True
        '
        'HmiPassFailButton1
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiPassFailButton1, 2)
        Me.HmiPassFailButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiPassFailButton1.Location = New System.Drawing.Point(6, 166)
        Me.HmiPassFailButton1.Margin = New System.Windows.Forms.Padding(6)
        Me.HmiPassFailButton1.Name = "HmiPassFailButton1"
        Me.HmiPassFailButton1.Size = New System.Drawing.Size(142, 28)
        Me.HmiPassFailButton1.TabIndex = 37
        Me.HmiPassFailButton1.Text = "HmiPassFailButton1"
        Me.HmiPassFailButton1.UseVisualStyleBackColor = True
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Pro.TabIndex = 36
        '
        'HmiButton_STPEnable
        '
        Me.HmiButton_STPEnable.BackColor = System.Drawing.Color.Transparent
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiButton_STPEnable, 2)
        Me.HmiButton_STPEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_STPEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_STPEnable.Location = New System.Drawing.Point(3, 43)
        Me.HmiButton_STPEnable.Name = "HmiButton_STPEnable"
        Me.HmiButton_STPEnable.Size = New System.Drawing.Size(148, 34)
        Me.HmiButton_STPEnable.TabIndex = 35
        Me.HmiButton_STPEnable.Text = "LinearMotor Enable"
        Me.HmiButton_STPEnable.UseVisualStyleBackColor = True
        '
        'HmiButton_MotorEnable
        '
        Me.HmiButton_MotorEnable.BackColor = System.Drawing.Color.Transparent
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiButton_MotorEnable, 2)
        Me.HmiButton_MotorEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_MotorEnable.Location = New System.Drawing.Point(3, 3)
        Me.HmiButton_MotorEnable.Name = "HmiButton_MotorEnable"
        Me.HmiButton_MotorEnable.Size = New System.Drawing.Size(148, 34)
        Me.HmiButton_MotorEnable.TabIndex = 34
        Me.HmiButton_MotorEnable.Text = "MotorEnable"
        Me.HmiButton_MotorEnable.UseVisualStyleBackColor = True
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(157, 43)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Y.TabIndex = 33
        '
        'HmiTextBox_Pro
        '
        Me.HmiTextBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pro.Location = New System.Drawing.Point(80, 123)
        Me.HmiTextBox_Pro.Name = "HmiTextBox_Pro"
        Me.HmiTextBox_Pro.Number = 0
        Me.HmiTextBox_Pro.Size = New System.Drawing.Size(71, 34)
        Me.HmiTextBox_Pro.TabIndex = 39
        Me.HmiTextBox_Pro.TextBoxReadOnly = False
        Me.HmiTextBox_Pro.ValueType = GetType(String)
        '
        'HmiLabel_Alarm
        '
        Me.HmiLabel_Alarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Alarm.Location = New System.Drawing.Point(157, 83)
        Me.HmiLabel_Alarm.Name = "HmiLabel_Alarm"
        Me.HmiLabel_Alarm.Size = New System.Drawing.Size(71, 34)
        Me.HmiLabel_Alarm.TabIndex = 42
        '
        'HmiSensor_Alarm
        '
        Me.HmiSensor_Alarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Alarm.Location = New System.Drawing.Point(234, 83)
        Me.HmiSensor_Alarm.Name = "HmiSensor_Alarm"
        Me.HmiSensor_Alarm.Size = New System.Drawing.Size(71, 34)
        Me.HmiSensor_Alarm.TabIndex = 43
        '
        'ShortCutUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShortCutUI"
        Me.Text = "ShortCutUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_MotorEnable As Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
    Friend WithEvents HmiButton_STPEnable As Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiPassFailButton1 As Kochi.HMI.MainControl.UI.HMIPassFailButton
    Friend WithEvents HmiPassFailButton3 As Kochi.HMI.MainControl.UI.HMIPassFailButton
    Friend WithEvents HmiTextBox_Pro As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Ready As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Ready As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_Alarm As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Alarm As Kochi.HMI.MainControl.UI.HMISensor
End Class
