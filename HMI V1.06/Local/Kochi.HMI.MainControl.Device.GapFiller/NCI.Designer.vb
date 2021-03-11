<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NCI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NCI))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body1_Top_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonXAdd = New HMIButtonWithIndicate()
        Me.ButtonXDec = New HMIButtonWithIndicate()
        Me.ButtonYDec = New HMIButtonWithIndicate()
        Me.ButtonYAdd = New HMIButtonWithIndicate()
        Me.ButtonZAdd = New HMIButtonWithIndicate()
        Me.ButtonZDec = New HMIButtonWithIndicate()
        Me.Panel_Body1_Top_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiLabel_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Step = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Speed = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label_Y = New System.Windows.Forms.Label()
        Me.Label_Z = New System.Windows.Forms.Label()
        Me.Label_X = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.TrackBar_Speed = New System.Windows.Forms.TrackBar()
        Me.HmiTextBox_Speed = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HmiButton_MotorEnable = New HMIButtonWithIndicate()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Body1_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiButton_Save = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_MoveZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveZ = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveX = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_MoveX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_MoveY = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Teach = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiTextBox_ActualZ = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Automatic = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_ActualY = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_MAX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_ActualX = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_Automatic = New System.Windows.Forms.Label()
        Me.Label_ActualY = New System.Windows.Forms.Label()
        Me.Label_ActualZ = New System.Windows.Forms.Label()
        Me.Label_ActualX = New System.Windows.Forms.Label()
        Me.Label_MAX = New System.Windows.Forms.Label()
        Me.PictureBox_Needle = New System.Windows.Forms.PictureBox()
        Me.Label_Needle = New System.Windows.Forms.Label()
        Me.HmiTextBox_Needle = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body_Bottom_Right_Axis = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.InputIO6 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.InputIO3 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.InputIO5 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.InputIO2 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.InputIO4 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.InputIO1 = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.TableLayoutPanel_Body_Bottom_Right_Button = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiButton_Release = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_Needle = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_AutoRefer = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_Filling = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_Move = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.Panel_Body1_Bottom_Left = New System.Windows.Forms.Panel()
        Me.HmiDataView_Point = New Kochi.HMI.MainControl.UI.HMIDataView()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.Panel_Body1_Top_Left.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Left.SuspendLayout()
        Me.Panel_Body1_Top_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Top_Right.SuspendLayout()
        CType(Me.TrackBar_Speed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body1_Bottom_Right.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.SuspendLayout()
        CType(Me.PictureBox_Needle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom_Right_Button.SuspendLayout()
        Me.Panel_Body1_Bottom_Left.SuspendLayout()
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(615, 498)
        Me.TableLayoutPanel_Body.TabIndex = 3
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 2
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body1_Top_Left, 0, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.Panel_Body1_Top_Right, 1, 0)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(0, 24)
        Me.TableLayoutPanel_Body_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(615, 124)
        Me.TableLayoutPanel_Body_Top.TabIndex = 0
        '
        'Panel_Body1_Top_Left
        '
        Me.Panel_Body1_Top_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body1_Top_Left.Controls.Add(Me.TableLayoutPanel_Body_Top_Left)
        Me.Panel_Body1_Top_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body1_Top_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body1_Top_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body1_Top_Left.Name = "Panel_Body1_Top_Left"
        Me.Panel_Body1_Top_Left.Size = New System.Drawing.Size(289, 124)
        Me.Panel_Body1_Top_Left.TabIndex = 1
        '
        'TableLayoutPanel_Body_Top_Left
        '
        Me.TableLayoutPanel_Body_Top_Left.ColumnCount = 6
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonXAdd, 3, 1)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonXDec, 1, 1)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonYDec, 2, 0)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonYAdd, 2, 2)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonZAdd, 4, 2)
        Me.TableLayoutPanel_Body_Top_Left.Controls.Add(Me.ButtonZDec, 4, 0)
        Me.TableLayoutPanel_Body_Top_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Left.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Left.Name = "TableLayoutPanel_Body_Top_Left"
        Me.TableLayoutPanel_Body_Top_Left.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Top_Left.Size = New System.Drawing.Size(287, 122)
        Me.TableLayoutPanel_Body_Top_Left.TabIndex = 1
        '
        'ButtonXAdd
        '
        Me.ButtonXAdd.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonXAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonXAdd.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ButtonXAdd.Location = New System.Drawing.Point(146, 33)
        Me.ButtonXAdd.Name = "ButtonXAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonXAdd, 2)
        Me.ButtonXAdd.Size = New System.Drawing.Size(49, 54)
        Me.ButtonXAdd.TabIndex = 6
        Me.ButtonXAdd.Text = "X+"
        Me.ButtonXAdd.UseVisualStyleBackColor = True
        '
        'ButtonXDec
        '
        Me.ButtonXDec.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonXDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonXDec.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonXDec.Location = New System.Drawing.Point(36, 33)
        Me.ButtonXDec.Name = "ButtonXDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonXDec, 2)
        Me.ButtonXDec.Size = New System.Drawing.Size(49, 54)
        Me.ButtonXDec.TabIndex = 7
        Me.ButtonXDec.Text = "X-"
        Me.ButtonXDec.UseVisualStyleBackColor = True
        '
        'ButtonYDec
        '
        Me.ButtonYDec.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonYDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonYDec.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonYDec.Location = New System.Drawing.Point(91, 3)
        Me.ButtonYDec.Name = "ButtonYDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonYDec, 2)
        Me.ButtonYDec.Size = New System.Drawing.Size(49, 54)
        Me.ButtonYDec.TabIndex = 8
        Me.ButtonYDec.Text = "Y-"
        Me.ButtonYDec.UseVisualStyleBackColor = True
        '
        'ButtonYAdd
        '
        Me.ButtonYAdd.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonYAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonYAdd.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonYAdd.Location = New System.Drawing.Point(91, 63)
        Me.ButtonYAdd.Name = "ButtonYAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonYAdd, 2)
        Me.ButtonYAdd.Size = New System.Drawing.Size(49, 56)
        Me.ButtonYAdd.TabIndex = 9
        Me.ButtonYAdd.Text = "Y+"
        Me.ButtonYAdd.UseVisualStyleBackColor = True
        '
        'ButtonZAdd
        '
        Me.ButtonZAdd.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonZAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonZAdd.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonZAdd.Location = New System.Drawing.Point(201, 63)
        Me.ButtonZAdd.Name = "ButtonZAdd"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonZAdd, 2)
        Me.ButtonZAdd.Size = New System.Drawing.Size(49, 56)
        Me.ButtonZAdd.TabIndex = 10
        Me.ButtonZAdd.Text = "Z+"
        Me.ButtonZAdd.UseVisualStyleBackColor = True
        '
        'ButtonZDec
        '
        Me.ButtonZDec.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonZDec.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ButtonZDec.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.ButtonZDec.Location = New System.Drawing.Point(201, 3)
        Me.ButtonZDec.Name = "ButtonZDec"
        Me.TableLayoutPanel_Body_Top_Left.SetRowSpan(Me.ButtonZDec, 2)
        Me.ButtonZDec.Size = New System.Drawing.Size(49, 54)
        Me.ButtonZDec.TabIndex = 11
        Me.ButtonZDec.Text = "Z-"
        Me.ButtonZDec.UseVisualStyleBackColor = True
        '
        'Panel_Body1_Top_Right
        '
        Me.Panel_Body1_Top_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body1_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Top_Right)
        Me.Panel_Body1_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body1_Top_Right.Location = New System.Drawing.Point(289, 0)
        Me.Panel_Body1_Top_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body1_Top_Right.Name = "Panel_Body1_Top_Right"
        Me.Panel_Body1_Top_Right.Size = New System.Drawing.Size(326, 124)
        Me.Panel_Body1_Top_Right.TabIndex = 2
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
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Z, 4, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Y, 2, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Step, 0, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_X, 0, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Speed, 0, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Y, 3, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_Z, 5, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label_X, 1, 1)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton1, 1, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton2, 2, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton3, 3, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.RadioButton4, 4, 2)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.TrackBar_Speed, 1, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Speed, 4, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.Label1, 5, 3)
        Me.TableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_MotorEnable, 0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Top_Right.Name = "TableLayoutPanel_Body_Top_Right"
        Me.TableLayoutPanel_Body_Top_Right.RowCount = 4
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(324, 122)
        Me.TableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiLabel_Z
        '
        Me.HmiLabel_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Z.Location = New System.Drawing.Point(215, 33)
        Me.HmiLabel_Z.Name = "HmiLabel_Z"
        Me.HmiLabel_Z.Size = New System.Drawing.Size(47, 24)
        Me.HmiLabel_Z.TabIndex = 12
        '
        'HmiLabel_Y
        '
        Me.HmiLabel_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Y.Location = New System.Drawing.Point(109, 33)
        Me.HmiLabel_Y.Name = "HmiLabel_Y"
        Me.HmiLabel_Y.Size = New System.Drawing.Size(47, 24)
        Me.HmiLabel_Y.TabIndex = 11
        '
        'HmiLabel_Step
        '
        Me.HmiLabel_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Step.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_Step.Name = "HmiLabel_Step"
        Me.HmiLabel_Step.Size = New System.Drawing.Size(47, 24)
        Me.HmiLabel_Step.TabIndex = 10
        '
        'HmiLabel_X
        '
        Me.HmiLabel_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_X.Location = New System.Drawing.Point(3, 33)
        Me.HmiLabel_X.Name = "HmiLabel_X"
        Me.HmiLabel_X.Size = New System.Drawing.Size(47, 24)
        Me.HmiLabel_X.TabIndex = 9
        '
        'HmiLabel_Speed
        '
        Me.HmiLabel_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Speed.Location = New System.Drawing.Point(3, 93)
        Me.HmiLabel_Speed.Name = "HmiLabel_Speed"
        Me.HmiLabel_Speed.Size = New System.Drawing.Size(47, 26)
        Me.HmiLabel_Speed.TabIndex = 8
        '
        'Label_Y
        '
        Me.Label_Y.AutoSize = True
        Me.Label_Y.BackColor = System.Drawing.Color.LightGray
        Me.Label_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Y.ForeColor = System.Drawing.Color.Blue
        Me.Label_Y.Location = New System.Drawing.Point(164, 35)
        Me.Label_Y.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Y.Name = "Label_Y"
        Me.Label_Y.Size = New System.Drawing.Size(43, 20)
        Me.Label_Y.TabIndex = 7
        Me.Label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_Z
        '
        Me.Label_Z.AutoSize = True
        Me.Label_Z.BackColor = System.Drawing.Color.LightGray
        Me.Label_Z.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Z.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_Z.ForeColor = System.Drawing.Color.Blue
        Me.Label_Z.Location = New System.Drawing.Point(270, 35)
        Me.Label_Z.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_Z.Name = "Label_Z"
        Me.Label_Z.Size = New System.Drawing.Size(49, 20)
        Me.Label_Z.TabIndex = 6
        Me.Label_Z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label_X
        '
        Me.Label_X.AutoSize = True
        Me.Label_X.BackColor = System.Drawing.Color.LightGray
        Me.Label_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_X.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label_X.ForeColor = System.Drawing.Color.Blue
        Me.Label_X.Location = New System.Drawing.Point(58, 35)
        Me.Label_X.Margin = New System.Windows.Forms.Padding(5)
        Me.Label_X.Name = "Label_X"
        Me.Label_X.Size = New System.Drawing.Size(43, 20)
        Me.Label_X.TabIndex = 5
        Me.Label_X.Text = "1000.00"
        Me.Label_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RadioButton1.Location = New System.Drawing.Point(56, 61)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(49, 28)
        Me.RadioButton1.TabIndex = 13
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "0.1"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton2.Location = New System.Drawing.Point(109, 61)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(49, 28)
        Me.RadioButton2.TabIndex = 14
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "1.00"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton3.Location = New System.Drawing.Point(160, 61)
        Me.RadioButton3.Margin = New System.Windows.Forms.Padding(1)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(51, 28)
        Me.RadioButton3.TabIndex = 15
        Me.RadioButton3.TabStop = True
        Me.RadioButton3.Text = "10.00"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.RadioButton4, 2)
        Me.RadioButton4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.RadioButton4.Location = New System.Drawing.Point(215, 61)
        Me.RadioButton4.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(108, 28)
        Me.RadioButton4.TabIndex = 16
        Me.RadioButton4.TabStop = True
        Me.RadioButton4.Text = "Continue"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'TrackBar_Speed
        '
        Me.TrackBar_Speed.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TrackBar_Speed, 3)
        Me.TrackBar_Speed.Location = New System.Drawing.Point(56, 91)
        Me.TrackBar_Speed.Margin = New System.Windows.Forms.Padding(3, 1, 1, 1)
        Me.TrackBar_Speed.Maximum = 100
        Me.TrackBar_Speed.Name = "TrackBar_Speed"
        Me.TrackBar_Speed.Size = New System.Drawing.Size(155, 30)
        Me.TrackBar_Speed.TabIndex = 17
        '
        'HmiTextBox_Speed
        '
        Me.HmiTextBox_Speed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Speed.Location = New System.Drawing.Point(213, 91)
        Me.HmiTextBox_Speed.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_Speed.Name = "HmiTextBox_Speed"
        Me.HmiTextBox_Speed.Number = 0
        Me.HmiTextBox_Speed.Size = New System.Drawing.Size(51, 30)
        Me.HmiTextBox_Speed.TabIndex = 18
        Me.HmiTextBox_Speed.TextBoxReadOnly = False
        Me.HmiTextBox_Speed.ValueType = GetType(String)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label1.Location = New System.Drawing.Point(266, 91)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 30)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "%"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'HmiButton_MotorEnable
        '
        Me.HmiButton_MotorEnable.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiButton_MotorEnable, 2)
        Me.HmiButton_MotorEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_MotorEnable.Location = New System.Drawing.Point(3, 3)
        Me.HmiButton_MotorEnable.Name = "HmiButton_MotorEnable"
        Me.HmiButton_MotorEnable.Size = New System.Drawing.Size(100, 24)
        Me.HmiButton_MotorEnable.TabIndex = 20
        Me.HmiButton_MotorEnable.Text = "MotorEnable"
        Me.HmiButton_MotorEnable.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body1_Bottom_Right, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Panel_Body1_Bottom_Left, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 148)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(615, 350)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Panel_Body1_Bottom_Right
        '
        Me.Panel_Body1_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body1_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body1_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body1_Bottom_Right.Location = New System.Drawing.Point(289, 0)
        Me.Panel_Body1_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body1_Bottom_Right.Name = "Panel_Body1_Bottom_Right"
        Me.Panel_Body1_Bottom_Right.Size = New System.Drawing.Size(326, 350)
        Me.Panel_Body1_Bottom_Right.TabIndex = 3
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Save, 2, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveZ, 5, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveZ, 4, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveY, 3, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveX, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_MoveX, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_MoveY, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Teach, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Bottom_Right_Needdle, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Bottom_Right_Axis, 0, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.TableLayoutPanel_Body_Bottom_Right_Button, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_Move, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 10
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(324, 348)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiButton_Save
        '
        Me.HmiButton_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Save.Location = New System.Drawing.Point(107, 36)
        Me.HmiButton_Save.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.HmiButton_Save.MarginHeight = 6
        Me.HmiButton_Save.Name = "HmiButton_Save"
        Me.HmiButton_Save.Size = New System.Drawing.Size(51, 30)
        Me.HmiButton_Save.TabIndex = 30
        '
        'HmiTextBox_MoveZ
        '
        Me.HmiTextBox_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveZ.Location = New System.Drawing.Point(268, 3)
        Me.HmiTextBox_MoveZ.Name = "HmiTextBox_MoveZ"
        Me.HmiTextBox_MoveZ.Number = 0
        Me.HmiTextBox_MoveZ.Size = New System.Drawing.Size(53, 28)
        Me.HmiTextBox_MoveZ.TabIndex = 28
        Me.HmiTextBox_MoveZ.TextBoxReadOnly = False
        Me.HmiTextBox_MoveZ.ValueType = GetType(String)
        '
        'HmiLabel_MoveZ
        '
        Me.HmiLabel_MoveZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveZ.Location = New System.Drawing.Point(215, 3)
        Me.HmiLabel_MoveZ.Name = "HmiLabel_MoveZ"
        Me.HmiLabel_MoveZ.Size = New System.Drawing.Size(47, 28)
        Me.HmiLabel_MoveZ.TabIndex = 27
        '
        'HmiTextBox_MoveY
        '
        Me.HmiTextBox_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveY.Location = New System.Drawing.Point(162, 3)
        Me.HmiTextBox_MoveY.Name = "HmiTextBox_MoveY"
        Me.HmiTextBox_MoveY.Number = 0
        Me.HmiTextBox_MoveY.Size = New System.Drawing.Size(47, 28)
        Me.HmiTextBox_MoveY.TabIndex = 26
        Me.HmiTextBox_MoveY.TextBoxReadOnly = False
        Me.HmiTextBox_MoveY.ValueType = GetType(String)
        '
        'HmiLabel_MoveX
        '
        Me.HmiLabel_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveX.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_MoveX.Name = "HmiLabel_MoveX"
        Me.HmiLabel_MoveX.Size = New System.Drawing.Size(47, 28)
        Me.HmiLabel_MoveX.TabIndex = 10
        '
        'HmiTextBox_MoveX
        '
        Me.HmiTextBox_MoveX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MoveX.Location = New System.Drawing.Point(56, 3)
        Me.HmiTextBox_MoveX.Name = "HmiTextBox_MoveX"
        Me.HmiTextBox_MoveX.Number = 0
        Me.HmiTextBox_MoveX.Size = New System.Drawing.Size(47, 28)
        Me.HmiTextBox_MoveX.TabIndex = 14
        Me.HmiTextBox_MoveX.TextBoxReadOnly = False
        Me.HmiTextBox_MoveX.ValueType = GetType(String)
        '
        'HmiLabel_MoveY
        '
        Me.HmiLabel_MoveY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MoveY.Location = New System.Drawing.Point(109, 3)
        Me.HmiLabel_MoveY.Name = "HmiLabel_MoveY"
        Me.HmiLabel_MoveY.Size = New System.Drawing.Size(47, 28)
        Me.HmiLabel_MoveY.TabIndex = 15
        '
        'HmiButton_Teach
        '
        Me.HmiButton_Teach.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Teach.Location = New System.Drawing.Point(54, 36)
        Me.HmiButton_Teach.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.HmiButton_Teach.MarginHeight = 6
        Me.HmiButton_Teach.Name = "HmiButton_Teach"
        Me.HmiButton_Teach.Size = New System.Drawing.Size(51, 30)
        Me.HmiButton_Teach.TabIndex = 16
        '
        'TableLayoutPanel_Body_Bottom_Right_Needdle
        '
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TableLayoutPanel_Body_Bottom_Right_Needdle, 6)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_ActualZ, 4, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_Automatic, 1, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_ActualY, 4, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_MAX, 1, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_ActualX, 4, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_Automatic, 0, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_ActualY, 3, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_ActualZ, 3, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_ActualX, 3, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_MAX, 0, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.PictureBox_Needle, 2, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.Label_Needle, 0, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Controls.Add(Me.HmiTextBox_Needle, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Location = New System.Drawing.Point(1, 103)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Name = "TableLayoutPanel_Body_Bottom_Right_Needdle"
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.RowCount = 3
        Me.HmiTableLayoutPanel_Body_Top_Right.SetRowSpan(Me.TableLayoutPanel_Body_Bottom_Right_Needdle, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.Size = New System.Drawing.Size(322, 66)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.TabIndex = 31
        '
        'HmiTextBox_ActualZ
        '
        Me.HmiTextBox_ActualZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ActualZ.Location = New System.Drawing.Point(276, 47)
        Me.HmiTextBox_ActualZ.Name = "HmiTextBox_ActualZ"
        Me.HmiTextBox_ActualZ.Number = 0
        Me.HmiTextBox_ActualZ.Size = New System.Drawing.Size(43, 16)
        Me.HmiTextBox_ActualZ.TabIndex = 14
        Me.HmiTextBox_ActualZ.TextBoxReadOnly = False
        Me.HmiTextBox_ActualZ.ValueType = GetType(String)
        '
        'HmiTextBox_Automatic
        '
        Me.HmiTextBox_Automatic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Automatic.Location = New System.Drawing.Point(93, 47)
        Me.HmiTextBox_Automatic.Name = "HmiTextBox_Automatic"
        Me.HmiTextBox_Automatic.Number = 0
        Me.HmiTextBox_Automatic.Size = New System.Drawing.Size(42, 16)
        Me.HmiTextBox_Automatic.TabIndex = 13
        Me.HmiTextBox_Automatic.TextBoxReadOnly = False
        Me.HmiTextBox_Automatic.ValueType = GetType(String)
        '
        'HmiTextBox_ActualY
        '
        Me.HmiTextBox_ActualY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ActualY.Location = New System.Drawing.Point(276, 25)
        Me.HmiTextBox_ActualY.Name = "HmiTextBox_ActualY"
        Me.HmiTextBox_ActualY.Number = 0
        Me.HmiTextBox_ActualY.Size = New System.Drawing.Size(43, 16)
        Me.HmiTextBox_ActualY.TabIndex = 12
        Me.HmiTextBox_ActualY.TextBoxReadOnly = False
        Me.HmiTextBox_ActualY.ValueType = GetType(String)
        '
        'HmiTextBox_MAX
        '
        Me.HmiTextBox_MAX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MAX.Location = New System.Drawing.Point(93, 25)
        Me.HmiTextBox_MAX.Name = "HmiTextBox_MAX"
        Me.HmiTextBox_MAX.Number = 0
        Me.HmiTextBox_MAX.Size = New System.Drawing.Size(42, 16)
        Me.HmiTextBox_MAX.TabIndex = 11
        Me.HmiTextBox_MAX.TextBoxReadOnly = False
        Me.HmiTextBox_MAX.ValueType = GetType(String)
        '
        'HmiTextBox_ActualX
        '
        Me.HmiTextBox_ActualX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ActualX.Location = New System.Drawing.Point(276, 3)
        Me.HmiTextBox_ActualX.Name = "HmiTextBox_ActualX"
        Me.HmiTextBox_ActualX.Number = 0
        Me.HmiTextBox_ActualX.Size = New System.Drawing.Size(43, 16)
        Me.HmiTextBox_ActualX.TabIndex = 10
        Me.HmiTextBox_ActualX.TextBoxReadOnly = False
        Me.HmiTextBox_ActualX.ValueType = GetType(String)
        '
        'Label_Automatic
        '
        Me.Label_Automatic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Automatic.Location = New System.Drawing.Point(3, 47)
        Me.Label_Automatic.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Automatic.Name = "Label_Automatic"
        Me.Label_Automatic.Size = New System.Drawing.Size(84, 16)
        Me.Label_Automatic.TabIndex = 8
        Me.Label_Automatic.Text = "Label7"
        Me.Label_Automatic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_ActualY
        '
        Me.Label_ActualY.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_ActualY.Location = New System.Drawing.Point(186, 25)
        Me.Label_ActualY.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_ActualY.Name = "Label_ActualY"
        Me.Label_ActualY.Size = New System.Drawing.Size(84, 16)
        Me.Label_ActualY.TabIndex = 7
        Me.Label_ActualY.Text = "Label6"
        Me.Label_ActualY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_ActualZ
        '
        Me.Label_ActualZ.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_ActualZ.Location = New System.Drawing.Point(186, 47)
        Me.Label_ActualZ.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_ActualZ.Name = "Label_ActualZ"
        Me.Label_ActualZ.Size = New System.Drawing.Size(84, 16)
        Me.Label_ActualZ.TabIndex = 6
        Me.Label_ActualZ.Text = "Label4"
        Me.Label_ActualZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_ActualX
        '
        Me.Label_ActualX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_ActualX.Location = New System.Drawing.Point(186, 3)
        Me.Label_ActualX.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_ActualX.Name = "Label_ActualX"
        Me.Label_ActualX.Size = New System.Drawing.Size(84, 16)
        Me.Label_ActualX.TabIndex = 5
        Me.Label_ActualX.Text = "Label3"
        Me.Label_ActualX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_MAX
        '
        Me.Label_MAX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_MAX.Location = New System.Drawing.Point(3, 25)
        Me.Label_MAX.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_MAX.Name = "Label_MAX"
        Me.Label_MAX.Size = New System.Drawing.Size(84, 16)
        Me.Label_MAX.TabIndex = 4
        Me.Label_MAX.Text = "Label5"
        Me.Label_MAX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'PictureBox_Needle
        '
        Me.PictureBox_Needle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_Needle.Image = CType(resources.GetObject("PictureBox_Needle.Image"), System.Drawing.Image)
        Me.PictureBox_Needle.Location = New System.Drawing.Point(141, 3)
        Me.PictureBox_Needle.Name = "PictureBox_Needle"
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.SetRowSpan(Me.PictureBox_Needle, 3)
        Me.PictureBox_Needle.Size = New System.Drawing.Size(39, 60)
        Me.PictureBox_Needle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_Needle.TabIndex = 0
        Me.PictureBox_Needle.TabStop = False
        '
        'Label_Needle
        '
        Me.Label_Needle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Needle.Location = New System.Drawing.Point(3, 3)
        Me.Label_Needle.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Needle.Name = "Label_Needle"
        Me.Label_Needle.Size = New System.Drawing.Size(84, 16)
        Me.Label_Needle.TabIndex = 1
        Me.Label_Needle.Text = "Label2"
        Me.Label_Needle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HmiTextBox_Needle
        '
        Me.HmiTextBox_Needle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Needle.Location = New System.Drawing.Point(93, 3)
        Me.HmiTextBox_Needle.Name = "HmiTextBox_Needle"
        Me.HmiTextBox_Needle.Number = 0
        Me.HmiTextBox_Needle.Size = New System.Drawing.Size(42, 16)
        Me.HmiTextBox_Needle.TabIndex = 9
        Me.HmiTextBox_Needle.TextBoxReadOnly = False
        Me.HmiTextBox_Needle.ValueType = GetType(String)
        '
        'TableLayoutPanel_Body_Bottom_Right_Axis
        '
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnCount = 2
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TableLayoutPanel_Body_Bottom_Right_Axis, 6)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO6, 1, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO3, 0, 2)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO5, 1, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO2, 0, 1)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO4, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Controls.Add(Me.InputIO1, 0, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Location = New System.Drawing.Point(1, 205)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Name = "TableLayoutPanel_Body_Bottom_Right_Axis"
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowCount = 3
        Me.HmiTableLayoutPanel_Body_Top_Right.SetRowSpan(Me.TableLayoutPanel_Body_Bottom_Right_Axis, 3)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.Size = New System.Drawing.Size(322, 100)
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.TabIndex = 35
        '
        'InputIO6
        '
        Me.InputIO6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO6.Location = New System.Drawing.Point(167, 72)
        Me.InputIO6.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO6.Name = "InputIO6"
        Me.InputIO6.Size = New System.Drawing.Size(149, 22)
        Me.InputIO6.TabIndex = 11
        Me.InputIO6.UseVisualStyleBackColor = True
        '
        'InputIO3
        '
        Me.InputIO3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO3.Location = New System.Drawing.Point(6, 72)
        Me.InputIO3.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO3.Name = "InputIO3"
        Me.InputIO3.Size = New System.Drawing.Size(149, 22)
        Me.InputIO3.TabIndex = 10
        Me.InputIO3.UseVisualStyleBackColor = True
        '
        'InputIO5
        '
        Me.InputIO5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO5.Location = New System.Drawing.Point(167, 39)
        Me.InputIO5.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO5.Name = "InputIO5"
        Me.InputIO5.Size = New System.Drawing.Size(149, 21)
        Me.InputIO5.TabIndex = 9
        Me.InputIO5.UseVisualStyleBackColor = True
        '
        'InputIO2
        '
        Me.InputIO2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO2.Location = New System.Drawing.Point(6, 39)
        Me.InputIO2.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO2.Name = "InputIO2"
        Me.InputIO2.Size = New System.Drawing.Size(149, 21)
        Me.InputIO2.TabIndex = 8
        Me.InputIO2.UseVisualStyleBackColor = True
        '
        'InputIO4
        '
        Me.InputIO4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO4.Location = New System.Drawing.Point(167, 6)
        Me.InputIO4.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO4.Name = "InputIO4"
        Me.InputIO4.Size = New System.Drawing.Size(149, 21)
        Me.InputIO4.TabIndex = 7
        Me.InputIO4.UseVisualStyleBackColor = True
        '
        'InputIO1
        '
        Me.InputIO1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InputIO1.Location = New System.Drawing.Point(6, 6)
        Me.InputIO1.Margin = New System.Windows.Forms.Padding(6)
        Me.InputIO1.Name = "InputIO1"
        Me.InputIO1.Size = New System.Drawing.Size(149, 21)
        Me.InputIO1.TabIndex = 6
        Me.InputIO1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Bottom_Right_Button
        '
        Me.TableLayoutPanel_Body_Bottom_Right_Button.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.TableLayoutPanel_Body_Bottom_Right_Button, 6)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Controls.Add(Me.HmiButton_Release, 0, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Controls.Add(Me.HmiButton_Needle, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Controls.Add(Me.HmiButton_AutoRefer, 2, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Controls.Add(Me.HmiButton_Filling, 3, 0)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Location = New System.Drawing.Point(1, 171)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Name = "TableLayoutPanel_Body_Bottom_Right_Button"
        Me.TableLayoutPanel_Body_Bottom_Right_Button.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom_Right_Button.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom_Right_Button.Size = New System.Drawing.Size(322, 32)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.TabIndex = 39
        '
        'HmiButton_Release
        '
        Me.HmiButton_Release.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Release.Location = New System.Drawing.Point(3, 3)
        Me.HmiButton_Release.Name = "HmiButton_Release"
        Me.HmiButton_Release.Size = New System.Drawing.Size(74, 26)
        Me.HmiButton_Release.TabIndex = 44
        Me.HmiButton_Release.UseVisualStyleBackColor = True
        '
        'HmiButton_Needle
        '
        Me.HmiButton_Needle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Needle.Location = New System.Drawing.Point(81, 3)
        Me.HmiButton_Needle.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.HmiButton_Needle.Name = "HmiButton_Needle"
        Me.HmiButton_Needle.Size = New System.Drawing.Size(78, 26)
        Me.HmiButton_Needle.TabIndex = 41
        Me.HmiButton_Needle.UseVisualStyleBackColor = True
        '
        'HmiButton_AutoRefer
        '
        Me.HmiButton_AutoRefer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_AutoRefer.Location = New System.Drawing.Point(161, 3)
        Me.HmiButton_AutoRefer.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.HmiButton_AutoRefer.Name = "HmiButton_AutoRefer"
        Me.HmiButton_AutoRefer.Size = New System.Drawing.Size(78, 26)
        Me.HmiButton_AutoRefer.TabIndex = 42
        Me.HmiButton_AutoRefer.UseVisualStyleBackColor = True
        '
        'HmiButton_Filling
        '
        Me.HmiButton_Filling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Filling.Location = New System.Drawing.Point(241, 3)
        Me.HmiButton_Filling.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.HmiButton_Filling.Name = "HmiButton_Filling"
        Me.HmiButton_Filling.Size = New System.Drawing.Size(78, 26)
        Me.HmiButton_Filling.TabIndex = 43
        Me.HmiButton_Filling.UseVisualStyleBackColor = True
        '
        'HmiButton_Move
        '
        Me.HmiButton_Move.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Move.Location = New System.Drawing.Point(162, 37)
        Me.HmiButton_Move.Name = "HmiButton_Move"
        Me.HmiButton_Move.Size = New System.Drawing.Size(47, 28)
        Me.HmiButton_Move.TabIndex = 40
        Me.HmiButton_Move.UseVisualStyleBackColor = True
        '
        'Panel_Body1_Bottom_Left
        '
        Me.Panel_Body1_Bottom_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body1_Bottom_Left.Controls.Add(Me.HmiDataView_Point)
        Me.Panel_Body1_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body1_Bottom_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body1_Bottom_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body1_Bottom_Left.Name = "Panel_Body1_Bottom_Left"
        Me.Panel_Body1_Bottom_Left.Size = New System.Drawing.Size(289, 350)
        Me.Panel_Body1_Bottom_Left.TabIndex = 0
        '
        'HmiDataView_Point
        '
        Me.HmiDataView_Point.AllowUserToAddRows = False
        Me.HmiDataView_Point.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Point.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Point.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Point.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Point.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Point.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Point.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.HmiDataView_Point.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
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
        Me.HmiDataView_Point.Location = New System.Drawing.Point(0, 0)
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
        Me.HmiDataView_Point.Size = New System.Drawing.Size(287, 348)
        Me.HmiDataView_Point.TabIndex = 0
        '
        'NCI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 498)
        Me.Controls.Add(Me.Panel_UI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "NCI"
        Me.Text = "NCI"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.Panel_Body1_Top_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Left.ResumeLayout(False)
        Me.Panel_Body1_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top_Right.PerformLayout()
        CType(Me.TrackBar_Speed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body1_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom_Right_Needdle.ResumeLayout(False)
        CType(Me.PictureBox_Needle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Body_Bottom_Right_Axis.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom_Right_Button.ResumeLayout(False)
        Me.Panel_Body1_Bottom_Left.ResumeLayout(False)
        CType(Me.HmiDataView_Point, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body1_Top_Left As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonXAdd As HMIButtonWithIndicate
    Friend WithEvents ButtonXDec As HMIButtonWithIndicate
    Friend WithEvents ButtonYDec As HMIButtonWithIndicate
    Friend WithEvents ButtonYAdd As HMIButtonWithIndicate
    Friend WithEvents ButtonZAdd As HMIButtonWithIndicate
    Friend WithEvents ButtonZDec As HMIButtonWithIndicate
    Friend WithEvents Panel_Body1_Top_Right As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Step As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Speed As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label_Y As System.Windows.Forms.Label
    Friend WithEvents Label_Z As System.Windows.Forms.Label
    Friend WithEvents Label_X As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents TrackBar_Speed As System.Windows.Forms.TrackBar
    Friend WithEvents HmiTextBox_Speed As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents HmiButton_MotorEnable As HMIButtonWithIndicate
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Body1_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiButton_Save As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_MoveZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveZ As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveX As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_MoveX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_MoveY As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Teach As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents TableLayoutPanel_Body_Bottom_Right_Needdle As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_ActualZ As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Automatic As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_ActualY As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MAX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_ActualX As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_Automatic As System.Windows.Forms.Label
    Friend WithEvents Label_ActualY As System.Windows.Forms.Label
    Friend WithEvents Label_ActualZ As System.Windows.Forms.Label
    Friend WithEvents Label_ActualX As System.Windows.Forms.Label
    Friend WithEvents Label_MAX As System.Windows.Forms.Label
    Friend WithEvents PictureBox_Needle As System.Windows.Forms.PictureBox
    Friend WithEvents Label_Needle As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_Needle As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Bottom_Right_Axis As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom_Right_Button As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents Panel_Body1_Bottom_Left As System.Windows.Forms.Panel
    Friend WithEvents HmiDataView_Point As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiButton_Move As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_Needle As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_AutoRefer As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_Filling As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO1 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO6 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO3 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO5 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO2 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents InputIO4 As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_Release As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
End Class
