<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Parameter
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
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel1 = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiButtonWithIndicate_TimeFilling = New HMIButtonWithIndicate()
        Me.HmiTextBox_TimeFilling = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_TimeFilling = New System.Windows.Forms.Label()
        Me.Label_Clean = New System.Windows.Forms.Label()
        Me.Label_BlindShort = New System.Windows.Forms.Label()
        Me.HmiSensor_BlindShort = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_Clean = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiButtonWithIndicate_BlindShort = New HMIButtonWithIndicate()
        Me.HmiButtonWithIndicate_Clean = New HMIButtonWithIndicate()
        Me.GroupBox_BlindShot = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Body_BlindShot = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.Label_PLC_BlindTime = New System.Windows.Forms.Label()
        Me.HmiTextBox_BlindNo = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_BlindTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_PLC_BlindNo = New System.Windows.Forms.Label()
        Me.Label_BlindNo = New System.Windows.Forms.Label()
        Me.Label_BlindTime = New System.Windows.Forms.Label()
        Me.Label_PotLife = New System.Windows.Forms.Label()
        Me.HmiTextBox_PotLife = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Function.SuspendLayout()
        Me.HmiTableLayoutPanel1.SuspendLayout()
        Me.GroupBox_BlindShot.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_BlindShot.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(615, 498)
        Me.Panel_UI.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 3
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Function, 0, 2)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_BlindShot, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 4
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(615, 498)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.HmiTableLayoutPanel1)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Function.Location = New System.Drawing.Point(1, 149)
        Me.GroupBox_Function.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(305, 122)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel1
        '
        Me.HmiTableLayoutPanel1.ColumnCount = 3
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_TimeFilling, 2, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_TimeFilling, 1, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.Label_TimeFilling, 0, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.Label_Clean, 0, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.Label_BlindShort, 0, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiSensor_BlindShort, 1, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiSensor_Clean, 1, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_BlindShort, 2, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_Clean, 2, 1)
        Me.HmiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel1.Location = New System.Drawing.Point(3, 20)
        Me.HmiTableLayoutPanel1.Name = "HmiTableLayoutPanel1"
        Me.HmiTableLayoutPanel1.RowCount = 4
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel1.Size = New System.Drawing.Size(299, 99)
        Me.HmiTableLayoutPanel1.TabIndex = 0
        '
        'HmiButtonWithIndicate_TimeFilling
        '
        Me.HmiButtonWithIndicate_TimeFilling.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_TimeFilling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_TimeFilling.Location = New System.Drawing.Point(211, 51)
        Me.HmiButtonWithIndicate_TimeFilling.Name = "HmiButtonWithIndicate_TimeFilling"
        Me.HmiButtonWithIndicate_TimeFilling.Size = New System.Drawing.Size(85, 18)
        Me.HmiButtonWithIndicate_TimeFilling.TabIndex = 15
        Me.HmiButtonWithIndicate_TimeFilling.Text = "HmiButtonWithIndicate2"
        Me.HmiButtonWithIndicate_TimeFilling.UseVisualStyleBackColor = True
        '
        'HmiTextBox_TimeFilling
        '
        Me.HmiTextBox_TimeFilling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TimeFilling.Location = New System.Drawing.Point(122, 51)
        Me.HmiTextBox_TimeFilling.Name = "HmiTextBox_TimeFilling"
        Me.HmiTextBox_TimeFilling.Number = 0
        Me.HmiTextBox_TimeFilling.Size = New System.Drawing.Size(83, 18)
        Me.HmiTextBox_TimeFilling.TabIndex = 14
        Me.HmiTextBox_TimeFilling.TextBoxReadOnly = False
        Me.HmiTextBox_TimeFilling.ValueType = GetType(String)
        '
        'Label_TimeFilling
        '
        Me.Label_TimeFilling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_TimeFilling.Location = New System.Drawing.Point(1, 49)
        Me.Label_TimeFilling.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_TimeFilling.Name = "Label_TimeFilling"
        Me.Label_TimeFilling.Size = New System.Drawing.Size(117, 22)
        Me.Label_TimeFilling.TabIndex = 8
        Me.Label_TimeFilling.Text = "Label5"
        Me.Label_TimeFilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Clean
        '
        Me.Label_Clean.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Clean.Location = New System.Drawing.Point(1, 25)
        Me.Label_Clean.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Clean.Name = "Label_Clean"
        Me.Label_Clean.Size = New System.Drawing.Size(117, 22)
        Me.Label_Clean.TabIndex = 3
        Me.Label_Clean.Text = "Label5"
        Me.Label_Clean.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_BlindShort
        '
        Me.Label_BlindShort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BlindShort.Location = New System.Drawing.Point(1, 1)
        Me.Label_BlindShort.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_BlindShort.Name = "Label_BlindShort"
        Me.Label_BlindShort.Size = New System.Drawing.Size(117, 22)
        Me.Label_BlindShort.TabIndex = 0
        Me.Label_BlindShort.Text = "Label2"
        Me.Label_BlindShort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HmiSensor_BlindShort
        '
        Me.HmiSensor_BlindShort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_BlindShort.Location = New System.Drawing.Point(122, 3)
        Me.HmiSensor_BlindShort.Name = "HmiSensor_BlindShort"
        Me.HmiSensor_BlindShort.Size = New System.Drawing.Size(83, 18)
        Me.HmiSensor_BlindShort.TabIndex = 4
        '
        'HmiSensor_Clean
        '
        Me.HmiSensor_Clean.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Clean.Location = New System.Drawing.Point(122, 27)
        Me.HmiSensor_Clean.Name = "HmiSensor_Clean"
        Me.HmiSensor_Clean.Size = New System.Drawing.Size(83, 18)
        Me.HmiSensor_Clean.TabIndex = 5
        '
        'HmiButtonWithIndicate_BlindShort
        '
        Me.HmiButtonWithIndicate_BlindShort.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_BlindShort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_BlindShort.Location = New System.Drawing.Point(211, 3)
        Me.HmiButtonWithIndicate_BlindShort.Name = "HmiButtonWithIndicate_BlindShort"
        Me.HmiButtonWithIndicate_BlindShort.Size = New System.Drawing.Size(85, 18)
        Me.HmiButtonWithIndicate_BlindShort.TabIndex = 6
        Me.HmiButtonWithIndicate_BlindShort.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_BlindShort.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_Clean
        '
        Me.HmiButtonWithIndicate_Clean.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_Clean.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_Clean.Location = New System.Drawing.Point(211, 27)
        Me.HmiButtonWithIndicate_Clean.Name = "HmiButtonWithIndicate_Clean"
        Me.HmiButtonWithIndicate_Clean.Size = New System.Drawing.Size(85, 18)
        Me.HmiButtonWithIndicate_Clean.TabIndex = 7
        Me.HmiButtonWithIndicate_Clean.Text = "HmiButtonWithIndicate2"
        Me.HmiButtonWithIndicate_Clean.UseVisualStyleBackColor = True
        '
        'GroupBox_BlindShot
        '
        Me.GroupBox_BlindShot.Controls.Add(Me.HmiTableLayoutPanel_Body_BlindShot)
        Me.GroupBox_BlindShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_BlindShot.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_BlindShot.Location = New System.Drawing.Point(1, 25)
        Me.GroupBox_BlindShot.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox_BlindShot.Name = "GroupBox_BlindShot"
        Me.GroupBox_BlindShot.Size = New System.Drawing.Size(305, 122)
        Me.GroupBox_BlindShot.TabIndex = 0
        Me.GroupBox_BlindShot.TabStop = False
        Me.GroupBox_BlindShot.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Body_BlindShot
        '
        Me.HmiTableLayoutPanel_Body_BlindShot.ColumnCount = 3
        Me.HmiTableLayoutPanel_Body_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.Label_PLC_BlindTime, 2, 1)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.HmiTextBox_BlindNo, 1, 2)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.HmiTextBox_BlindTime, 1, 1)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.Label_PLC_BlindNo, 2, 2)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.Label_BlindNo, 0, 2)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.Label_BlindTime, 0, 1)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.Label_PotLife, 0, 0)
        Me.HmiTableLayoutPanel_Body_BlindShot.Controls.Add(Me.HmiTextBox_PotLife, 1, 0)
        Me.HmiTableLayoutPanel_Body_BlindShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_BlindShot.Location = New System.Drawing.Point(3, 20)
        Me.HmiTableLayoutPanel_Body_BlindShot.Name = "HmiTableLayoutPanel_Body_BlindShot"
        Me.HmiTableLayoutPanel_Body_BlindShot.RowCount = 4
        Me.HmiTableLayoutPanel_Body_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_BlindShot.Size = New System.Drawing.Size(299, 99)
        Me.HmiTableLayoutPanel_Body_BlindShot.TabIndex = 0
        '
        'Label_PLC_BlindTime
        '
        Me.Label_PLC_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PLC_BlindTime.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PLC_BlindTime.Location = New System.Drawing.Point(209, 25)
        Me.Label_PLC_BlindTime.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PLC_BlindTime.Name = "Label_PLC_BlindTime"
        Me.Label_PLC_BlindTime.Size = New System.Drawing.Size(89, 22)
        Me.Label_PLC_BlindTime.TabIndex = 15
        Me.Label_PLC_BlindTime.Text = "0.00"
        Me.Label_PLC_BlindTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_BlindNo
        '
        Me.HmiTextBox_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_BlindNo.Location = New System.Drawing.Point(122, 51)
        Me.HmiTextBox_BlindNo.Name = "HmiTextBox_BlindNo"
        Me.HmiTextBox_BlindNo.Number = 0
        Me.HmiTextBox_BlindNo.Size = New System.Drawing.Size(83, 18)
        Me.HmiTextBox_BlindNo.TabIndex = 13
        Me.HmiTextBox_BlindNo.TextBoxReadOnly = False
        Me.HmiTextBox_BlindNo.ValueType = GetType(String)
        '
        'HmiTextBox_BlindTime
        '
        Me.HmiTextBox_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_BlindTime.Location = New System.Drawing.Point(122, 27)
        Me.HmiTextBox_BlindTime.Name = "HmiTextBox_BlindTime"
        Me.HmiTextBox_BlindTime.Number = 0
        Me.HmiTextBox_BlindTime.Size = New System.Drawing.Size(83, 18)
        Me.HmiTextBox_BlindTime.TabIndex = 11
        Me.HmiTextBox_BlindTime.TextBoxReadOnly = False
        Me.HmiTextBox_BlindTime.ValueType = GetType(String)
        '
        'Label_PLC_BlindNo
        '
        Me.Label_PLC_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PLC_BlindNo.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_PLC_BlindNo.Location = New System.Drawing.Point(209, 49)
        Me.Label_PLC_BlindNo.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PLC_BlindNo.Name = "Label_PLC_BlindNo"
        Me.Label_PLC_BlindNo.Size = New System.Drawing.Size(89, 22)
        Me.Label_PLC_BlindNo.TabIndex = 8
        Me.Label_PLC_BlindNo.Text = "[ 0 ]"
        Me.Label_PLC_BlindNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_BlindNo
        '
        Me.Label_BlindNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BlindNo.Location = New System.Drawing.Point(1, 49)
        Me.Label_BlindNo.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_BlindNo.Name = "Label_BlindNo"
        Me.Label_BlindNo.Size = New System.Drawing.Size(117, 22)
        Me.Label_BlindNo.TabIndex = 6
        Me.Label_BlindNo.Text = "Label8"
        Me.Label_BlindNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_BlindTime
        '
        Me.Label_BlindTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_BlindTime.Location = New System.Drawing.Point(1, 25)
        Me.Label_BlindTime.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_BlindTime.Name = "Label_BlindTime"
        Me.Label_BlindTime.Size = New System.Drawing.Size(117, 22)
        Me.Label_BlindTime.TabIndex = 3
        Me.Label_BlindTime.Text = "Label5"
        Me.Label_BlindTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PotLife
        '
        Me.Label_PotLife.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PotLife.Location = New System.Drawing.Point(1, 1)
        Me.Label_PotLife.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_PotLife.Name = "Label_PotLife"
        Me.Label_PotLife.Size = New System.Drawing.Size(117, 22)
        Me.Label_PotLife.TabIndex = 0
        Me.Label_PotLife.Text = "Label2"
        Me.Label_PotLife.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HmiTextBox_PotLife
        '
        Me.HmiTextBox_PotLife.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PotLife.Location = New System.Drawing.Point(122, 3)
        Me.HmiTextBox_PotLife.Name = "HmiTextBox_PotLife"
        Me.HmiTextBox_PotLife.Number = 0
        Me.HmiTextBox_PotLife.Size = New System.Drawing.Size(83, 18)
        Me.HmiTextBox_PotLife.TabIndex = 9
        Me.HmiTextBox_PotLife.TextBoxReadOnly = False
        Me.HmiTextBox_PotLife.ValueType = GetType(String)
        '
        'Parameter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 498)
        Me.Controls.Add(Me.Panel_UI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Parameter"
        Me.Text = "Parameter"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Function.ResumeLayout(False)
        Me.HmiTableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox_BlindShot.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_BlindShot.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_BlindShot As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Body_BlindShot As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_PLC_BlindTime As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_BlindNo As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_BlindTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_PLC_BlindNo As System.Windows.Forms.Label
    Friend WithEvents Label_BlindNo As System.Windows.Forms.Label
    Friend WithEvents Label_BlindTime As System.Windows.Forms.Label
    Friend WithEvents Label_PotLife As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_PotLife As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel1 As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_Clean As System.Windows.Forms.Label
    Friend WithEvents Label_BlindShort As System.Windows.Forms.Label
    Friend WithEvents HmiSensor_BlindShort As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_Clean As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiButtonWithIndicate_BlindShort As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_Clean As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_TimeFilling As HMIButtonWithIndicate
    Friend WithEvents HmiTextBox_TimeFilling As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_TimeFilling As System.Windows.Forms.Label
End Class
