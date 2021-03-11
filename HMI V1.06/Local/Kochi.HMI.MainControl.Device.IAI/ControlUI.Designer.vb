<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ControlUI
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Pandel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Top_Left = New System.Windows.Forms.Panel()
        Me.Panel_Body_Top_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.Panel_Body_Bottom_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Left_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Ready = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_STPEnable = New Kochi.HMI.MainControl.UI.HMIButtonWithIndicate(Me.components)
        Me.HmiButton_MotorEnable = New Kochi.HMI.MainControl.UI.HMIButtonWithIndicate(Me.components)
        Me.HmiSensor_Ready = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Name = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Move = New Kochi.HMI.MainControl.UI.HMIPassFailButton(Me.components)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiPassFailButton1 = New Kochi.HMI.MainControl.UI.HMIPassFailButton(Me.components)
        Me.HmiPassFailButton3 = New Kochi.HMI.MainControl.UI.HMIPassFailButton(Me.components)
        Me.HmiButton_Save = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Pro = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiButton_Variant = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Pro = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiTextBox_Name = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiDataView_Point = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiLabel_Alarm = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Alarm = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.Pandel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.Panel_Body_Top_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
        Me.Panel_Body_Bottom_Left.SuspendLayout()
        Me.TableLayoutPanel_Left_Bottom.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.SuspendLayout()
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pandel_Body
        '
        Me.Pandel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Pandel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pandel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Pandel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Pandel_Body.Name = "Pandel_Body"
        Me.Pandel_Body.Size = New System.Drawing.Size(623, 530)
        Me.Pandel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Top, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(623, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 2
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body_Top_Left, 0, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body_Top_Right, 1, 0)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(0, 26)
        Me.TableLayoutPanel_Body_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(623, 132)
        Me.TableLayoutPanel_Body_Top.TabIndex = 0
        '
        'Panel_Body_Top_Left
        '
        Me.Panel_Body_Top_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Top_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Top_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Top_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Top_Left.Name = "Panel_Body_Top_Left"
        Me.Panel_Body_Top_Left.Size = New System.Drawing.Size(292, 132)
        Me.Panel_Body_Top_Left.TabIndex = 1
        '
        'Panel_Body_Top_Right
        '
        Me.Panel_Body_Top_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Top_Right.Location = New System.Drawing.Point(292, 0)
        Me.Panel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Top_Right.Name = "Panel_Body_Top_Right"
        Me.Panel_Body_Top_Right.Size = New System.Drawing.Size(331, 132)
        Me.Panel_Body_Top_Right.TabIndex = 2
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body_Bottom_Left, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 158)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(623, 372)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Panel_Body_Bottom_Right
        '
        Me.Panel_Body_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Right.Location = New System.Drawing.Point(292, 0)
        Me.Panel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Right.Name = "Panel_Body_Bottom_Right"
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(331, 372)
        Me.Panel_Body_Bottom_Right.TabIndex = 3
        '
        'Panel_Body_Bottom_Left
        '
        Me.Panel_Body_Bottom_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Left.Controls.Add(Me.TableLayoutPanel_Left_Bottom)
        Me.Panel_Body_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body_Bottom_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Left.Name = "Panel_Body_Bottom_Left"
        Me.Panel_Body_Bottom_Left.Size = New System.Drawing.Size(292, 372)
        Me.Panel_Body_Bottom_Left.TabIndex = 0
        '
        'TableLayoutPanel_Left_Bottom
        '
        Me.TableLayoutPanel_Left_Bottom.ColumnCount = 3
        Me.TableLayoutPanel_Left_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Left_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Left_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Left_Bottom.Controls.Add(Me.HmiDataView_Point, 0, 0)
        Me.TableLayoutPanel_Left_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Left_Bottom.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Left_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Left_Bottom.Name = "TableLayoutPanel_Left_Bottom"
        Me.TableLayoutPanel_Left_Bottom.RowCount = 2
        Me.TableLayoutPanel_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Left_Bottom.Size = New System.Drawing.Size(290, 370)
        Me.TableLayoutPanel_Left_Bottom.TabIndex = 0
        '
        'TableLayoutPanel_Body_Top_Right
        '
        Me.TableLayoutPanel_Body_Top_Right.ColumnCount = 6
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Alarm, 1, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Alarm, 0, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Ready, 0, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_STPEnable, 0, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_MotorEnable, 0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Ready, 1, 2)
        Me.TableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Top_Right.Name = "TableLayoutPanel_Body_Top_Right"
        Me.TableLayoutPanel_Body_Top_Right.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(329, 130)
        Me.TableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiLabel_Ready
        '
        Me.HmiLabel_Ready.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Ready.Location = New System.Drawing.Point(3, 67)
        Me.HmiLabel_Ready.Name = "HmiLabel_Ready"
        Me.HmiLabel_Ready.Size = New System.Drawing.Size(48, 26)
        Me.HmiLabel_Ready.TabIndex = 23
        '
        'HmiButton_STPEnable
        '
        Me.HmiButton_STPEnable.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiButton_STPEnable, 3)
        Me.HmiButton_STPEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_STPEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_STPEnable.Location = New System.Drawing.Point(3, 35)
        Me.HmiButton_STPEnable.Name = "HmiButton_STPEnable"
        Me.HmiButton_STPEnable.Size = New System.Drawing.Size(156, 26)
        Me.HmiButton_STPEnable.TabIndex = 22
        Me.HmiButton_STPEnable.Text = "LinearMotor Enable"
        Me.HmiButton_STPEnable.UseVisualStyleBackColor = True
        '
        'HmiButton_MotorEnable
        '
        Me.HmiButton_MotorEnable.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiButton_MotorEnable, 3)
        Me.HmiButton_MotorEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_MotorEnable.Location = New System.Drawing.Point(3, 3)
        Me.HmiButton_MotorEnable.Name = "HmiButton_MotorEnable"
        Me.HmiButton_MotorEnable.Size = New System.Drawing.Size(156, 26)
        Me.HmiButton_MotorEnable.TabIndex = 21
        Me.HmiButton_MotorEnable.Text = "MotorEnable"
        Me.HmiButton_MotorEnable.UseVisualStyleBackColor = True
        '
        'HmiSensor_Ready
        '
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiSensor_Ready, 2)
        Me.HmiSensor_Ready.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Ready.Location = New System.Drawing.Point(57, 67)
        Me.HmiSensor_Ready.Name = "HmiSensor_Ready"
        Me.HmiSensor_Ready.Size = New System.Drawing.Size(102, 26)
        Me.HmiSensor_Ready.TabIndex = 24
        '
        'HmiTableLayoutPanel_Body_Top_Right
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.71428!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Name, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Modify, 2, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Move, 3, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Bottom_Right_Axis, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Save, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Pro, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_Variant, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Variant, 3, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_Pro, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Name, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 6
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.44444!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(329, 370)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiLabel_Name
        '
        Me.HmiLabel_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Name.Location = New System.Drawing.Point(3, 44)
        Me.HmiLabel_Name.Name = "HmiLabel_Name"
        Me.HmiLabel_Name.Size = New System.Drawing.Size(52, 35)
        Me.HmiLabel_Name.TabIndex = 71
        '
        'HmiButton_Modify
        '
        Me.HmiButton_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Modify.Location = New System.Drawing.Point(117, 83)
        Me.HmiButton_Modify.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Modify.MarginHeight = 6
        Me.HmiButton_Modify.Name = "HmiButton_Modify"
        Me.HmiButton_Modify.Size = New System.Drawing.Size(56, 39)
        Me.HmiButton_Modify.TabIndex = 70
        '
        'HmiButton_Move
        '
        Me.HmiButton_Move.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Move.Location = New System.Drawing.Point(177, 85)
        Me.HmiButton_Move.Name = "HmiButton_Move"
        Me.HmiButton_Move.Size = New System.Drawing.Size(52, 35)
        Me.HmiButton_Move.TabIndex = 67
        Me.HmiButton_Move.Text = "HmiPassFailButton5"
        Me.HmiButton_Move.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Bottom_Right_Axis
        '
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnCount = 2
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TableLayoutPanel_Body_Bottom_Right_Axis, 6)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.HmiPassFailButton1, 0, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.HmiPassFailButton3, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Location = New System.Drawing.Point(1, 165)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Name = "TableLayoutPanel_Body_Bottom_Right_Axis"
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Size = New System.Drawing.Size(327, 39)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.TabIndex = 65
        '
        'HmiPassFailButton1
        '
        Me.HmiPassFailButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiPassFailButton1.Location = New System.Drawing.Point(6, 6)
        Me.HmiPassFailButton1.Margin = New System.Windows.Forms.Padding(6)
        Me.HmiPassFailButton1.Name = "HmiPassFailButton1"
        Me.HmiPassFailButton1.Size = New System.Drawing.Size(151, 27)
        Me.HmiPassFailButton1.TabIndex = 0
        Me.HmiPassFailButton1.Text = "HmiPassFailButton1"
        Me.HmiPassFailButton1.UseVisualStyleBackColor = True
        '
        'HmiPassFailButton3
        '
        Me.HmiPassFailButton3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiPassFailButton3.Location = New System.Drawing.Point(169, 6)
        Me.HmiPassFailButton3.Margin = New System.Windows.Forms.Padding(6)
        Me.HmiPassFailButton3.Name = "HmiPassFailButton3"
        Me.HmiPassFailButton3.Size = New System.Drawing.Size(152, 27)
        Me.HmiPassFailButton3.TabIndex = 2
        Me.HmiPassFailButton3.Text = "HmiPassFailButton3"
        Me.HmiPassFailButton3.UseVisualStyleBackColor = True
        '
        'HmiButton_Save
        '
        Me.HmiButton_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Save.Location = New System.Drawing.Point(59, 124)
        Me.HmiButton_Save.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Save.MarginHeight = 6
        Me.HmiButton_Save.Name = "HmiButton_Save"
        Me.HmiButton_Save.Size = New System.Drawing.Size(56, 39)
        Me.HmiButton_Save.TabIndex = 30
        '
        'HmiLabel_Pro
        '
        Me.HmiLabel_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Pro.Location = New System.Drawing.Point(3, 85)
        Me.HmiLabel_Pro.Name = "HmiLabel_Pro"
        Me.HmiLabel_Pro.Size = New System.Drawing.Size(52, 35)
        Me.HmiLabel_Pro.TabIndex = 21
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(52, 35)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiComboBox_Variant
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiComboBox_Variant, 2)
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(61, 1)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(110, 39)
        Me.HmiComboBox_Variant.TabIndex = 12
        '
        'HmiButton_Variant
        '
        Me.HmiButton_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Variant.Location = New System.Drawing.Point(175, 1)
        Me.HmiButton_Variant.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Variant.MarginHeight = 6
        Me.HmiButton_Variant.Name = "HmiButton_Variant"
        Me.HmiButton_Variant.Size = New System.Drawing.Size(56, 39)
        Me.HmiButton_Variant.TabIndex = 13
        '
        'HmiComboBox_Pro
        '
        Me.HmiComboBox_Pro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Pro.Location = New System.Drawing.Point(61, 85)
        Me.HmiComboBox_Pro.Name = "HmiComboBox_Pro"
        Me.HmiComboBox_Pro.Size = New System.Drawing.Size(52, 35)
        Me.HmiComboBox_Pro.TabIndex = 69
        '
        'HmiTextBox_Name
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_Name, 2)
        Me.HmiTextBox_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Name.Location = New System.Drawing.Point(61, 44)
        Me.HmiTextBox_Name.Name = "HmiTextBox_Name"
        Me.HmiTextBox_Name.Number = 0
        Me.HmiTextBox_Name.Size = New System.Drawing.Size(110, 35)
        Me.HmiTextBox_Name.TabIndex = 72
        Me.HmiTextBox_Name.TextBoxReadOnly = False
        Me.HmiTextBox_Name.ValueType = GetType(String)
        '
        'HmiDataView_Point
        '
        Me.HmiDataView_Point.AllowUserToAddRows = False
        Me.HmiDataView_Point.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Point.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.HmiDataView_Point.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Point.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Point.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Point.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.HmiDataView_Point.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableLayoutPanel_Left_Bottom.SetColumnSpan(Me.HmiDataView_Point, 3)
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HmiDataView_Point.DefaultCellStyle = DataGridViewCellStyle3
        Me.HmiDataView_Point.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Point.EnableHeadersVisualStyles = False
        Me.HmiDataView_Point.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Point.Location = New System.Drawing.Point(3, 3)
        Me.HmiDataView_Point.Name = "HmiDataView_Point"
        Me.HmiDataView_Point.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_Point.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.HmiDataView_Point.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Point.RowTemplate.Height = 40
        Me.HmiDataView_Point.RowTemplate.ReadOnly = True
        Me.HmiDataView_Point.Size = New System.Drawing.Size(284, 327)
        Me.HmiDataView_Point.TabIndex = 1
        '
        'HmiLabel_Alarm
        '
        Me.HmiLabel_Alarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Alarm.Location = New System.Drawing.Point(3, 99)
        Me.HmiLabel_Alarm.Name = "HmiLabel_Alarm"
        Me.HmiLabel_Alarm.Size = New System.Drawing.Size(48, 28)
        Me.HmiLabel_Alarm.TabIndex = 25
        '
        'HmiSensor_Alarm
        '
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiSensor_Alarm, 2)
        Me.HmiSensor_Alarm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Alarm.Location = New System.Drawing.Point(57, 99)
        Me.HmiSensor_Alarm.Name = "HmiSensor_Alarm"
        Me.HmiSensor_Alarm.Size = New System.Drawing.Size(102, 28)
        Me.HmiSensor_Alarm.TabIndex = 26
        '
        'ControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Pandel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ControlUI"
        Me.Text = "ControlUI"
        Me.Pandel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.Panel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
        Me.Panel_Body_Bottom_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Left_Bottom.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ResumeLayout(False)
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pandel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiButton_Variant As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Pro As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body_Bottom_Right_Axis As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiPassFailButton1 As Kochi.HMI.MainControl.UI.HMIPassFailButton
    Friend WithEvents HmiButton_Move As Kochi.HMI.MainControl.UI.HMIPassFailButton
    Friend WithEvents HmiComboBox_Pro As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body_Top_Right As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiButton_MotorEnable As Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
    Friend WithEvents Panel_Body_Top_Left As System.Windows.Forms.Panel
    Friend WithEvents HmiButton_STPEnable As Kochi.HMI.MainControl.UI.HMIButtonWithIndicate
    Friend WithEvents HmiPassFailButton3 As Kochi.HMI.MainControl.UI.HMIPassFailButton
    Friend WithEvents Panel_Body_Bottom_Left As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Left_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Point As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiButton_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Save As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Ready As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Ready As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_Name As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Name As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiSensor_Alarm As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_Alarm As Kochi.HMI.MainControl.UI.HMILabel
End Class
