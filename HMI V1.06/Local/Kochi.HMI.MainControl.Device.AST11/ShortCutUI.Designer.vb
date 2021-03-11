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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTableLayoutPanel_Body_Top = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.InputIO1 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.OutputIO1 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.InputIO2 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO3 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO4 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO5 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.InputIO6 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.OutputIO2 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO3 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO4 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO5 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO6 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.OutputIO7 = New Kochi.HMI.MainControl.UI.OutputIO()
        Me.HmiTableLayoutPanel_Body_Top_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_Pro = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTableLayoutPanel_Body_Bottom_Top = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_ProgramNo = New System.Windows.Forms.Label()
        Me.Label_CycleTime = New System.Windows.Forms.Label()
        Me.Label_State = New System.Windows.Forms.Label()
        Me.HmiLabel_SystemState = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_ProgramNo = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_State = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_CycleTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_SystemState = New System.Windows.Forms.Label()
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Label_Step4_Angle = New System.Windows.Forms.Label()
        Me.Label_Step3_Angle = New System.Windows.Forms.Label()
        Me.Label_Step2_Angle = New System.Windows.Forms.Label()
        Me.Label_Step1_Angle = New System.Windows.Forms.Label()
        Me.Label_Step4_Torque = New System.Windows.Forms.Label()
        Me.Label_Step3_Torque = New System.Windows.Forms.Label()
        Me.Label_Step2_Torque = New System.Windows.Forms.Label()
        Me.Label_Step1_Torque = New System.Windows.Forms.Label()
        Me.Label_Step4_Step = New System.Windows.Forms.Label()
        Me.Label_Step3_Step = New System.Windows.Forms.Label()
        Me.Label_Step2_Step = New System.Windows.Forms.Label()
        Me.Label_Step1_Step = New System.Windows.Forms.Label()
        Me.HmiLabel_Step4 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step3 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step1 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Angle = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Torque = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.InputIO7 = New Kochi.HMI.MainControl.UI.InputIO()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Bottom.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Bottom_Top.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.HmiTableLayoutPanel_Body_Top, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(498, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'HmiTableLayoutPanel_Body_Top
        '
        Me.HmiTableLayoutPanel_Body_Top.ColumnCount = 3
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO7, 0, 6)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO1, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO1, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO2, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO3, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO4, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO5, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.InputIO6, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO2, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO3, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO4, 2, 3)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO5, 2, 4)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO6, 2, 5)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.OutputIO7, 2, 6)
        Me.HmiTableLayoutPanel_Body_Top.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Bottom, 0, 7)
        Me.HmiTableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Body_Top.Name = "HmiTableLayoutPanel_Body_Top"
        Me.HmiTableLayoutPanel_Body_Top.RowCount = 8
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.HmiTableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top.Size = New System.Drawing.Size(492, 259)
        Me.HmiTableLayoutPanel_Body_Top.TabIndex = 0
        '
        'InputIO1
        '
        Me.InputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO1.Location = New System.Drawing.Point(3, 3)
        Me.InputIO1.Name = "InputIO1"
        Me.InputIO1.Size = New System.Drawing.Size(190, 26)
        Me.InputIO1.TabIndex = 0
        '
        'OutputIO1
        '
        Me.OutputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO1.Location = New System.Drawing.Point(297, 3)
        Me.OutputIO1.Name = "OutputIO1"
        Me.OutputIO1.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO1.TabIndex = 1
        '
        'InputIO2
        '
        Me.InputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO2.Location = New System.Drawing.Point(3, 35)
        Me.InputIO2.Name = "InputIO2"
        Me.InputIO2.Size = New System.Drawing.Size(190, 26)
        Me.InputIO2.TabIndex = 2
        '
        'InputIO3
        '
        Me.InputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO3.Location = New System.Drawing.Point(3, 67)
        Me.InputIO3.Name = "InputIO3"
        Me.InputIO3.Size = New System.Drawing.Size(190, 26)
        Me.InputIO3.TabIndex = 3
        '
        'InputIO4
        '
        Me.InputIO4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO4.Location = New System.Drawing.Point(3, 99)
        Me.InputIO4.Name = "InputIO4"
        Me.InputIO4.Size = New System.Drawing.Size(190, 26)
        Me.InputIO4.TabIndex = 4
        '
        'InputIO5
        '
        Me.InputIO5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO5.Location = New System.Drawing.Point(3, 131)
        Me.InputIO5.Name = "InputIO5"
        Me.InputIO5.Size = New System.Drawing.Size(190, 26)
        Me.InputIO5.TabIndex = 5
        '
        'InputIO6
        '
        Me.InputIO6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO6.Location = New System.Drawing.Point(3, 163)
        Me.InputIO6.Name = "InputIO6"
        Me.InputIO6.Size = New System.Drawing.Size(190, 26)
        Me.InputIO6.TabIndex = 6
        '
        'OutputIO2
        '
        Me.OutputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO2.Location = New System.Drawing.Point(297, 35)
        Me.OutputIO2.Name = "OutputIO2"
        Me.OutputIO2.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO2.TabIndex = 7
        '
        'OutputIO3
        '
        Me.OutputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO3.Location = New System.Drawing.Point(297, 67)
        Me.OutputIO3.Name = "OutputIO3"
        Me.OutputIO3.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO3.TabIndex = 8
        '
        'OutputIO4
        '
        Me.OutputIO4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO4.Location = New System.Drawing.Point(297, 99)
        Me.OutputIO4.Name = "OutputIO4"
        Me.OutputIO4.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO4.TabIndex = 9
        '
        'OutputIO5
        '
        Me.OutputIO5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO5.Location = New System.Drawing.Point(297, 131)
        Me.OutputIO5.Name = "OutputIO5"
        Me.OutputIO5.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO5.TabIndex = 10
        '
        'OutputIO6
        '
        Me.OutputIO6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO6.Location = New System.Drawing.Point(297, 163)
        Me.OutputIO6.Name = "OutputIO6"
        Me.OutputIO6.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO6.TabIndex = 11
        '
        'OutputIO7
        '
        Me.OutputIO7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OutputIO7.Location = New System.Drawing.Point(297, 195)
        Me.OutputIO7.Name = "OutputIO7"
        Me.OutputIO7.Size = New System.Drawing.Size(192, 26)
        Me.OutputIO7.TabIndex = 12
        '
        'HmiTableLayoutPanel_Body_Top_Bottom
        '
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Top.SetColumnSpan(Me.HmiTableLayoutPanel_Body_Top_Bottom, 3)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Controls.Add(Me.HmiTextBox_Pro, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Location = New System.Drawing.Point(1, 225)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Name = "HmiTableLayoutPanel_Body_Top_Bottom"
        Me.HmiTableLayoutPanel_Body_Top_Bottom.RowCount = 1
        Me.HmiTableLayoutPanel_Body_Top_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Body_Top_Bottom.Size = New System.Drawing.Size(490, 33)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.TabIndex = 13
        '
        'HmiTextBox_Pro
        '
        Me.HmiTextBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pro.Location = New System.Drawing.Point(198, 3)
        Me.HmiTextBox_Pro.Name = "HmiTextBox_Pro"
        Me.HmiTextBox_Pro.Size = New System.Drawing.Size(92, 27)
        Me.HmiTextBox_Pro.TabIndex = 2
        Me.HmiTextBox_Pro.TextBoxReadOnly = False
        Me.HmiTextBox_Pro.ValueType = GetType(String)
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 1
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.HmiTableLayoutPanel_Body_Bottom_Top, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.HmiTableLayoutPanel_Body_Bottom_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 265)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 2
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(498, 265)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'HmiTableLayoutPanel_Body_Bottom_Top
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ColumnCount = 4
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.Label_ProgramNo, 1, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.Label_CycleTime, 1, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.Label_State, 1, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.HmiLabel_SystemState, 0, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.HmiLabel_ProgramNo, 0, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.HmiLabel_State, 0, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.HmiLabel_CycleTime, 0, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Controls.Add(Me.Label_SystemState, 1, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Name = "HmiTableLayoutPanel_Body_Bottom_Top"
        Me.HmiTableLayoutPanel_Body_Bottom_Top.RowCount = 4
        Me.HmiTableLayoutPanel_Body_Bottom_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Top.Size = New System.Drawing.Size(492, 126)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.TabIndex = 0
        '
        'Label_ProgramNo
        '
        Me.Label_ProgramNo.AutoSize = True
        Me.Label_ProgramNo.BackColor = System.Drawing.Color.LightGray
        Me.Label_ProgramNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_ProgramNo.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_ProgramNo.Location = New System.Drawing.Point(128, 36)
        Me.Label_ProgramNo.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_ProgramNo.Name = "Label_ProgramNo"
        Me.Label_ProgramNo.Size = New System.Drawing.Size(113, 21)
        Me.Label_ProgramNo.TabIndex = 7
        Me.Label_ProgramNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_CycleTime
        '
        Me.Label_CycleTime.AutoSize = True
        Me.Label_CycleTime.BackColor = System.Drawing.Color.LightGray
        Me.Label_CycleTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_CycleTime.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_CycleTime.Location = New System.Drawing.Point(128, 98)
        Me.Label_CycleTime.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_CycleTime.Name = "Label_CycleTime"
        Me.Label_CycleTime.Size = New System.Drawing.Size(113, 23)
        Me.Label_CycleTime.TabIndex = 6
        Me.Label_CycleTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_State
        '
        Me.Label_State.AutoSize = True
        Me.Label_State.BackColor = System.Drawing.Color.LightGray
        Me.Label_State.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_State.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_State.Location = New System.Drawing.Point(128, 67)
        Me.Label_State.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_State.Name = "Label_State"
        Me.Label_State.Size = New System.Drawing.Size(113, 21)
        Me.Label_State.TabIndex = 5
        Me.Label_State.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_SystemState
        '
        Me.HmiLabel_SystemState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SystemState.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_SystemState.Name = "HmiLabel_SystemState"
        Me.HmiLabel_SystemState.Size = New System.Drawing.Size(117, 25)
        Me.HmiLabel_SystemState.TabIndex = 0
        '
        'HmiLabel_ProgramNo
        '
        Me.HmiLabel_ProgramNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ProgramNo.Location = New System.Drawing.Point(3, 34)
        Me.HmiLabel_ProgramNo.Name = "HmiLabel_ProgramNo"
        Me.HmiLabel_ProgramNo.Size = New System.Drawing.Size(117, 25)
        Me.HmiLabel_ProgramNo.TabIndex = 1
        '
        'HmiLabel_State
        '
        Me.HmiLabel_State.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_State.Location = New System.Drawing.Point(3, 65)
        Me.HmiLabel_State.Name = "HmiLabel_State"
        Me.HmiLabel_State.Size = New System.Drawing.Size(117, 25)
        Me.HmiLabel_State.TabIndex = 2
        '
        'HmiLabel_CycleTime
        '
        Me.HmiLabel_CycleTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CycleTime.Location = New System.Drawing.Point(3, 96)
        Me.HmiLabel_CycleTime.Name = "HmiLabel_CycleTime"
        Me.HmiLabel_CycleTime.Size = New System.Drawing.Size(117, 27)
        Me.HmiLabel_CycleTime.TabIndex = 3
        '
        'Label_SystemState
        '
        Me.Label_SystemState.AutoSize = True
        Me.Label_SystemState.BackColor = System.Drawing.Color.LightGray
        Me.Label_SystemState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_SystemState.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_SystemState.Location = New System.Drawing.Point(128, 5)
        Me.Label_SystemState.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_SystemState.Name = "Label_SystemState"
        Me.Label_SystemState.Size = New System.Drawing.Size(113, 21)
        Me.Label_SystemState.TabIndex = 4
        Me.Label_SystemState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTableLayoutPanel_Body_Bottom_Bottom
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step4_Angle, 4, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step3_Angle, 3, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step2_Angle, 2, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step1_Angle, 1, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step4_Torque, 4, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step3_Torque, 3, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step2_Torque, 2, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step1_Torque, 1, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step4_Step, 4, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step3_Step, 3, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step2_Step, 2, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.Label_Step1_Step, 1, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Step4, 4, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Step3, 3, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Step2, 2, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Step1, 1, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Angle, 0, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Torque, 0, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Controls.Add(Me.HmiLabel_Step, 0, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Location = New System.Drawing.Point(3, 135)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Name = "HmiTableLayoutPanel_Body_Bottom_Bottom"
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.RowCount = 4
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.Size = New System.Drawing.Size(492, 127)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.TabIndex = 1
        '
        'Label_Step4_Angle
        '
        Me.Label_Step4_Angle.AutoSize = True
        Me.Label_Step4_Angle.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step4_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step4_Angle.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step4_Angle.Location = New System.Drawing.Point(397, 98)
        Me.Label_Step4_Angle.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step4_Angle.Name = "Label_Step4_Angle"
        Me.Label_Step4_Angle.Size = New System.Drawing.Size(90, 24)
        Me.Label_Step4_Angle.TabIndex = 20
        Me.Label_Step4_Angle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step3_Angle
        '
        Me.Label_Step3_Angle.AutoSize = True
        Me.Label_Step3_Angle.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step3_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step3_Angle.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step3_Angle.Location = New System.Drawing.Point(299, 98)
        Me.Label_Step3_Angle.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step3_Angle.Name = "Label_Step3_Angle"
        Me.Label_Step3_Angle.Size = New System.Drawing.Size(88, 24)
        Me.Label_Step3_Angle.TabIndex = 19
        Me.Label_Step3_Angle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step2_Angle
        '
        Me.Label_Step2_Angle.AutoSize = True
        Me.Label_Step2_Angle.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step2_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step2_Angle.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step2_Angle.Location = New System.Drawing.Point(201, 98)
        Me.Label_Step2_Angle.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step2_Angle.Name = "Label_Step2_Angle"
        Me.Label_Step2_Angle.Size = New System.Drawing.Size(88, 24)
        Me.Label_Step2_Angle.TabIndex = 18
        Me.Label_Step2_Angle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step1_Angle
        '
        Me.Label_Step1_Angle.AutoSize = True
        Me.Label_Step1_Angle.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step1_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step1_Angle.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step1_Angle.Location = New System.Drawing.Point(103, 98)
        Me.Label_Step1_Angle.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step1_Angle.Name = "Label_Step1_Angle"
        Me.Label_Step1_Angle.Size = New System.Drawing.Size(88, 24)
        Me.Label_Step1_Angle.TabIndex = 17
        Me.Label_Step1_Angle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step4_Torque
        '
        Me.Label_Step4_Torque.AutoSize = True
        Me.Label_Step4_Torque.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step4_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step4_Torque.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step4_Torque.Location = New System.Drawing.Point(397, 67)
        Me.Label_Step4_Torque.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step4_Torque.Name = "Label_Step4_Torque"
        Me.Label_Step4_Torque.Size = New System.Drawing.Size(90, 21)
        Me.Label_Step4_Torque.TabIndex = 16
        Me.Label_Step4_Torque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step3_Torque
        '
        Me.Label_Step3_Torque.AutoSize = True
        Me.Label_Step3_Torque.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step3_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step3_Torque.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step3_Torque.Location = New System.Drawing.Point(299, 67)
        Me.Label_Step3_Torque.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step3_Torque.Name = "Label_Step3_Torque"
        Me.Label_Step3_Torque.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step3_Torque.TabIndex = 15
        Me.Label_Step3_Torque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step2_Torque
        '
        Me.Label_Step2_Torque.AutoSize = True
        Me.Label_Step2_Torque.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step2_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step2_Torque.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step2_Torque.Location = New System.Drawing.Point(201, 67)
        Me.Label_Step2_Torque.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step2_Torque.Name = "Label_Step2_Torque"
        Me.Label_Step2_Torque.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step2_Torque.TabIndex = 14
        Me.Label_Step2_Torque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step1_Torque
        '
        Me.Label_Step1_Torque.AutoSize = True
        Me.Label_Step1_Torque.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step1_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step1_Torque.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step1_Torque.Location = New System.Drawing.Point(103, 67)
        Me.Label_Step1_Torque.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step1_Torque.Name = "Label_Step1_Torque"
        Me.Label_Step1_Torque.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step1_Torque.TabIndex = 13
        Me.Label_Step1_Torque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step4_Step
        '
        Me.Label_Step4_Step.AutoSize = True
        Me.Label_Step4_Step.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step4_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step4_Step.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step4_Step.Location = New System.Drawing.Point(397, 36)
        Me.Label_Step4_Step.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step4_Step.Name = "Label_Step4_Step"
        Me.Label_Step4_Step.Size = New System.Drawing.Size(90, 21)
        Me.Label_Step4_Step.TabIndex = 12
        Me.Label_Step4_Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step3_Step
        '
        Me.Label_Step3_Step.AutoSize = True
        Me.Label_Step3_Step.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step3_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step3_Step.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step3_Step.Location = New System.Drawing.Point(299, 36)
        Me.Label_Step3_Step.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step3_Step.Name = "Label_Step3_Step"
        Me.Label_Step3_Step.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step3_Step.TabIndex = 11
        Me.Label_Step3_Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step2_Step
        '
        Me.Label_Step2_Step.AutoSize = True
        Me.Label_Step2_Step.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step2_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step2_Step.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step2_Step.Location = New System.Drawing.Point(201, 36)
        Me.Label_Step2_Step.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step2_Step.Name = "Label_Step2_Step"
        Me.Label_Step2_Step.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step2_Step.TabIndex = 10
        Me.Label_Step2_Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Step1_Step
        '
        Me.Label_Step1_Step.AutoSize = True
        Me.Label_Step1_Step.BackColor = System.Drawing.Color.LightGray
        Me.Label_Step1_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Step1_Step.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Step1_Step.Location = New System.Drawing.Point(103, 36)
        Me.Label_Step1_Step.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Step1_Step.Name = "Label_Step1_Step"
        Me.Label_Step1_Step.Size = New System.Drawing.Size(88, 21)
        Me.Label_Step1_Step.TabIndex = 9
        Me.Label_Step1_Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiLabel_Step4
        '
        Me.HmiLabel_Step4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step4.Location = New System.Drawing.Point(395, 3)
        Me.HmiLabel_Step4.Name = "HmiLabel_Step4"
        Me.HmiLabel_Step4.Size = New System.Drawing.Size(94, 25)
        Me.HmiLabel_Step4.TabIndex = 7
        '
        'HmiLabel_Step3
        '
        Me.HmiLabel_Step3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step3.Location = New System.Drawing.Point(297, 3)
        Me.HmiLabel_Step3.Name = "HmiLabel_Step3"
        Me.HmiLabel_Step3.Size = New System.Drawing.Size(92, 25)
        Me.HmiLabel_Step3.TabIndex = 6
        '
        'HmiLabel_Step2
        '
        Me.HmiLabel_Step2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step2.Location = New System.Drawing.Point(199, 3)
        Me.HmiLabel_Step2.Name = "HmiLabel_Step2"
        Me.HmiLabel_Step2.Size = New System.Drawing.Size(92, 25)
        Me.HmiLabel_Step2.TabIndex = 5
        '
        'HmiLabel_Step1
        '
        Me.HmiLabel_Step1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step1.Location = New System.Drawing.Point(101, 3)
        Me.HmiLabel_Step1.Name = "HmiLabel_Step1"
        Me.HmiLabel_Step1.Size = New System.Drawing.Size(92, 25)
        Me.HmiLabel_Step1.TabIndex = 4
        '
        'HmiLabel_Angle
        '
        Me.HmiLabel_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle.Location = New System.Drawing.Point(3, 96)
        Me.HmiLabel_Angle.Name = "HmiLabel_Angle"
        Me.HmiLabel_Angle.Size = New System.Drawing.Size(92, 28)
        Me.HmiLabel_Angle.TabIndex = 2
        '
        'HmiLabel_Torque
        '
        Me.HmiLabel_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque.Location = New System.Drawing.Point(3, 65)
        Me.HmiLabel_Torque.Name = "HmiLabel_Torque"
        Me.HmiLabel_Torque.Size = New System.Drawing.Size(92, 25)
        Me.HmiLabel_Torque.TabIndex = 1
        '
        'HmiLabel_Step
        '
        Me.HmiLabel_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step.Location = New System.Drawing.Point(3, 34)
        Me.HmiLabel_Step.Name = "HmiLabel_Step"
        Me.HmiLabel_Step.Size = New System.Drawing.Size(92, 25)
        Me.HmiLabel_Step.TabIndex = 0
        '
        'InputIO7
        '
        Me.InputIO7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO7.Location = New System.Drawing.Point(3, 195)
        Me.InputIO7.Name = "InputIO7"
        Me.InputIO7.Size = New System.Drawing.Size(190, 26)
        Me.InputIO7.TabIndex = 14
        '
        'ShortCutUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ShortCutUI"
        Me.Text = "ShortCutUI"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Bottom.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Top.PerformLayout()
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Bottom.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Body_Top As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents InputIO1 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents OutputIO1 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents InputIO2 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO3 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO4 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO5 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents InputIO6 As Kochi.HMI.MainControl.UI.InputIO
    Friend WithEvents OutputIO2 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO3 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO4 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO5 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO6 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents OutputIO7 As Kochi.HMI.MainControl.UI.OutputIO
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_Pro As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Body_Bottom_Top As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_SystemState As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_ProgramNo As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_State As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_CycleTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_SystemState As System.Windows.Forms.Label
    Friend WithEvents Label_ProgramNo As System.Windows.Forms.Label
    Friend WithEvents Label_CycleTime As System.Windows.Forms.Label
    Friend WithEvents Label_State As System.Windows.Forms.Label
    Friend WithEvents HmiTableLayoutPanel_Body_Bottom_Bottom As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Label_Step4_Angle As System.Windows.Forms.Label
    Friend WithEvents Label_Step3_Angle As System.Windows.Forms.Label
    Friend WithEvents Label_Step2_Angle As System.Windows.Forms.Label
    Friend WithEvents Label_Step1_Angle As System.Windows.Forms.Label
    Friend WithEvents Label_Step4_Torque As System.Windows.Forms.Label
    Friend WithEvents Label_Step3_Torque As System.Windows.Forms.Label
    Friend WithEvents Label_Step2_Torque As System.Windows.Forms.Label
    Friend WithEvents Label_Step1_Torque As System.Windows.Forms.Label
    Friend WithEvents Label_Step4_Step As System.Windows.Forms.Label
    Friend WithEvents Label_Step3_Step As System.Windows.Forms.Label
    Friend WithEvents Label_Step2_Step As System.Windows.Forms.Label
    Friend WithEvents Label_Step1_Step As System.Windows.Forms.Label
    Friend WithEvents HmiLabel_Step4 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step3 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step1 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Angle As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Torque As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents InputIO7 As Kochi.HMI.MainControl.UI.InputIO
End Class
