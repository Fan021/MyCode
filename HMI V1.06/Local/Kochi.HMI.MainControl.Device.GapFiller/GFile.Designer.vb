<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GFile
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GFile))
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog_Path = New System.Windows.Forms.SaveFileDialog()
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTableLayoutPanel_Body_Bottom_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiButton_Stop = New HMIButtonWithIndicate()
        Me.HmiButton_Start = New HMIButtonWithIndicate()
        Me.HmiButtonWithIndicate_RWrite = New HMIButtonWithIndicate()
        Me.HmiButtonWithIndicate_RRead = New HMIButtonWithIndicate()
        Me.HmiLabel_GFile = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_NC_ErrorCode = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_MotorEnable = New HMIButtonWithIndicate()
        Me.HmiTextBox_Reset = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Reset = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_HS_Confirm = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTableLayoutPanel = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiButton_Build = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_Release = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiButton_Load = New Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton()
        Me.HmiLabel2_Z = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel2_Y = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel2_X = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Label2_Y = New System.Windows.Forms.Label()
        Me.Label2_Z = New System.Windows.Forms.Label()
        Me.Label2_X = New System.Windows.Forms.Label()
        Me.HmiTextBox_HS_Confirm = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_HS_Confirm = New HMIButtonWithIndicate()
        Me.HmiButton_Reset = New HMIButtonWithIndicate()
        Me.HmiTextBox_NCErrorCode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_GFile = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TabControl_GFile = New System.Windows.Forms.TabControl()
        Me.TabPage1_GFile = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_Body_Bottom_Left = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.HmiLabel_MFunction = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Body_Bottom_Left_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_New = New System.Windows.Forms.Button()
        Me.Button_Open = New System.Windows.Forms.Button()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RichTextBox_GCode = New System.Windows.Forms.RichTextBox()
        Me.TextBox_Read = New System.Windows.Forms.RichTextBox()
        Me.TabPage2_GFile = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_GFile = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel()
        Me.TableLayoutPanel_GFileLink2Right = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiButton_LoadVariant = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_GFileLink2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_GFileLinkChoose2 = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_GFileLinkModify2 = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_GFileName2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiDataView_GFile = New Kochi.HMI.MainControl.UI.HMIDataView()
        Me.HmiDataView_GFile2 = New Kochi.HMI.MainControl.UI.HMIDataView()
        Me.TableLayoutPanel_GFileLink1Right = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_Name1 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Name1 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_GFileLink1 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_GFileLinkChoose1 = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_GFileLinkModify1 = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_GFileName1 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SuspendLayout()
        Me.HmiTableLayoutPanel.SuspendLayout()
        Me.TabControl_GFile.SuspendLayout()
        Me.TabPage1_GFile.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Bottom_Left.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom_Left_Top.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TabPage2_GFile.SuspendLayout()
        Me.HmiTableLayoutPanel_GFile.SuspendLayout()
        Me.TableLayoutPanel_GFileLink2Right.SuspendLayout()
        CType(Me.HmiDataView_GFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HmiDataView_GFile2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_GFileLink1Right.SuspendLayout()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(615, 498)
        Me.TableLayoutPanel_Body.TabIndex = 4
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.HmiTableLayoutPanel_Body_Bottom_Right, 1, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.TabControl_GFile, 0, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(1, 1)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(613, 496)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 0
        '
        'HmiTableLayoutPanel_Body_Bottom_Right
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnCount = 6
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButton_Stop, 4, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButton_Start, 2, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButtonWithIndicate_RWrite, 3, 7)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButtonWithIndicate_RRead, 3, 6)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel_GFile, 2, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel_NC_ErrorCode, 0, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButton_MotorEnable, 0, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiTextBox_Reset, 2, 5)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel_Reset, 0, 5)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel_HS_Confirm, 0, 4)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel, 0, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel2_Z, 4, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel2_Y, 2, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiLabel2_X, 0, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.Label2_Y, 3, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.Label2_Z, 5, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.Label2_X, 1, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiTextBox_HS_Confirm, 2, 4)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButton_HS_Confirm, 3, 4)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiButton_Reset, 3, 5)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiTextBox_NCErrorCode, 1, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Controls.Add(Me.HmiTextBox_GFile, 3, 2)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Location = New System.Drawing.Point(306, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Name = "HmiTableLayoutPanel_Body_Bottom_Right"
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowCount = 17
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.040816!))
        Me.HmiTableLayoutPanel_Body_Bottom_Right.Size = New System.Drawing.Size(307, 496)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.TabIndex = 1
        '
        'HmiButton_Stop
        '
        Me.HmiButton_Stop.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButton_Stop, 2)
        Me.HmiButton_Stop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Stop.Location = New System.Drawing.Point(207, 3)
        Me.HmiButton_Stop.Name = "HmiButton_Stop"
        Me.HmiButton_Stop.Size = New System.Drawing.Size(97, 24)
        Me.HmiButton_Stop.TabIndex = 58
        Me.HmiButton_Stop.Text = "Stop"
        Me.HmiButton_Stop.UseVisualStyleBackColor = True
        '
        'HmiButton_Start
        '
        Me.HmiButton_Start.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButton_Start, 2)
        Me.HmiButton_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Start.Location = New System.Drawing.Point(105, 3)
        Me.HmiButton_Start.Name = "HmiButton_Start"
        Me.HmiButton_Start.Size = New System.Drawing.Size(96, 24)
        Me.HmiButton_Start.TabIndex = 57
        Me.HmiButton_Start.Text = "Start"
        Me.HmiButton_Start.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_RWrite
        '
        Me.HmiButtonWithIndicate_RWrite.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButtonWithIndicate_RWrite, 2)
        Me.HmiButtonWithIndicate_RWrite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_RWrite.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButtonWithIndicate_RWrite.Location = New System.Drawing.Point(156, 213)
        Me.HmiButtonWithIndicate_RWrite.Name = "HmiButtonWithIndicate_RWrite"
        Me.HmiButtonWithIndicate_RWrite.Size = New System.Drawing.Size(96, 24)
        Me.HmiButtonWithIndicate_RWrite.TabIndex = 56
        Me.HmiButtonWithIndicate_RWrite.Text = "RWrite"
        Me.HmiButtonWithIndicate_RWrite.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_RRead
        '
        Me.HmiButtonWithIndicate_RRead.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButtonWithIndicate_RRead, 2)
        Me.HmiButtonWithIndicate_RRead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_RRead.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButtonWithIndicate_RRead.Location = New System.Drawing.Point(156, 183)
        Me.HmiButtonWithIndicate_RRead.Name = "HmiButtonWithIndicate_RRead"
        Me.HmiButtonWithIndicate_RRead.Size = New System.Drawing.Size(96, 24)
        Me.HmiButtonWithIndicate_RRead.TabIndex = 55
        Me.HmiButtonWithIndicate_RRead.Text = "RRead"
        Me.HmiButtonWithIndicate_RRead.UseVisualStyleBackColor = True
        '
        'HmiLabel_GFile
        '
        Me.HmiLabel_GFile.BackColor = System.Drawing.Color.White
        Me.HmiLabel_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_GFile.Location = New System.Drawing.Point(103, 61)
        Me.HmiLabel_GFile.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_GFile.Name = "HmiLabel_GFile"
        Me.HmiLabel_GFile.Size = New System.Drawing.Size(49, 28)
        Me.HmiLabel_GFile.TabIndex = 53
        '
        'HmiLabel_NC_ErrorCode
        '
        Me.HmiLabel_NC_ErrorCode.BackColor = System.Drawing.Color.White
        Me.HmiLabel_NC_ErrorCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_NC_ErrorCode.Location = New System.Drawing.Point(1, 61)
        Me.HmiLabel_NC_ErrorCode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_NC_ErrorCode.Name = "HmiLabel_NC_ErrorCode"
        Me.HmiLabel_NC_ErrorCode.Size = New System.Drawing.Size(49, 28)
        Me.HmiLabel_NC_ErrorCode.TabIndex = 51
        '
        'HmiButton_MotorEnable
        '
        Me.HmiButton_MotorEnable.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButton_MotorEnable, 2)
        Me.HmiButton_MotorEnable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.HmiButton_MotorEnable.Location = New System.Drawing.Point(3, 3)
        Me.HmiButton_MotorEnable.Name = "HmiButton_MotorEnable"
        Me.HmiButton_MotorEnable.Size = New System.Drawing.Size(96, 24)
        Me.HmiButton_MotorEnable.TabIndex = 48
        Me.HmiButton_MotorEnable.Text = "MotorEnable"
        Me.HmiButton_MotorEnable.UseVisualStyleBackColor = True
        '
        'HmiTextBox_Reset
        '
        Me.HmiTextBox_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Reset.Location = New System.Drawing.Point(105, 153)
        Me.HmiTextBox_Reset.Name = "HmiTextBox_Reset"
        Me.HmiTextBox_Reset.Number = 0
        Me.HmiTextBox_Reset.Size = New System.Drawing.Size(45, 24)
        Me.HmiTextBox_Reset.TabIndex = 46
        Me.HmiTextBox_Reset.TextBoxReadOnly = False
        Me.HmiTextBox_Reset.ValueType = GetType(String)
        '
        'HmiLabel_Reset
        '
        Me.HmiLabel_Reset.BackColor = System.Drawing.Color.White
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiLabel_Reset, 2)
        Me.HmiLabel_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Reset.Location = New System.Drawing.Point(1, 151)
        Me.HmiLabel_Reset.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Reset.Name = "HmiLabel_Reset"
        Me.HmiLabel_Reset.Size = New System.Drawing.Size(100, 28)
        Me.HmiLabel_Reset.TabIndex = 45
        '
        'HmiLabel_HS_Confirm
        '
        Me.HmiLabel_HS_Confirm.BackColor = System.Drawing.Color.White
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiLabel_HS_Confirm, 2)
        Me.HmiLabel_HS_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_HS_Confirm.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel_HS_Confirm.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_HS_Confirm.Name = "HmiLabel_HS_Confirm"
        Me.HmiLabel_HS_Confirm.Size = New System.Drawing.Size(100, 28)
        Me.HmiLabel_HS_Confirm.TabIndex = 42
        '
        'HmiTableLayoutPanel
        '
        Me.HmiTableLayoutPanel.BackColor = System.Drawing.Color.White
        Me.HmiTableLayoutPanel.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiTableLayoutPanel, 6)
        Me.HmiTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999001!))
        Me.HmiTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.66734!))
        Me.HmiTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.66734!))
        Me.HmiTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.66734!))
        Me.HmiTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999001!))
        Me.HmiTableLayoutPanel.Controls.Add(Me.HmiButton_Build, 1, 0)
        Me.HmiTableLayoutPanel.Controls.Add(Me.HmiButton_Release, 2, 0)
        Me.HmiTableLayoutPanel.Controls.Add(Me.HmiButton_Load, 3, 0)
        Me.HmiTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel.Location = New System.Drawing.Point(1, 91)
        Me.HmiTableLayoutPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel.Name = "HmiTableLayoutPanel"
        Me.HmiTableLayoutPanel.RowCount = 1
        Me.HmiTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel.Size = New System.Drawing.Size(305, 28)
        Me.HmiTableLayoutPanel.TabIndex = 40
        '
        'HmiButton_Build
        '
        Me.HmiButton_Build.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Build.Location = New System.Drawing.Point(33, 3)
        Me.HmiButton_Build.Name = "HmiButton_Build"
        Me.HmiButton_Build.Size = New System.Drawing.Size(75, 22)
        Me.HmiButton_Build.TabIndex = 41
        Me.HmiButton_Build.UseVisualStyleBackColor = True
        '
        'HmiButton_Release
        '
        Me.HmiButton_Release.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Release.Location = New System.Drawing.Point(114, 3)
        Me.HmiButton_Release.Name = "HmiButton_Release"
        Me.HmiButton_Release.Size = New System.Drawing.Size(75, 22)
        Me.HmiButton_Release.TabIndex = 42
        Me.HmiButton_Release.UseVisualStyleBackColor = True
        '
        'HmiButton_Load
        '
        Me.HmiButton_Load.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Load.Location = New System.Drawing.Point(195, 3)
        Me.HmiButton_Load.Name = "HmiButton_Load"
        Me.HmiButton_Load.Size = New System.Drawing.Size(75, 22)
        Me.HmiButton_Load.TabIndex = 43
        Me.HmiButton_Load.UseVisualStyleBackColor = True
        '
        'HmiLabel2_Z
        '
        Me.HmiLabel2_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel2_Z.Location = New System.Drawing.Point(207, 33)
        Me.HmiLabel2_Z.Name = "HmiLabel2_Z"
        Me.HmiLabel2_Z.Size = New System.Drawing.Size(45, 24)
        Me.HmiLabel2_Z.TabIndex = 12
        '
        'HmiLabel2_Y
        '
        Me.HmiLabel2_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel2_Y.Location = New System.Drawing.Point(105, 33)
        Me.HmiLabel2_Y.Name = "HmiLabel2_Y"
        Me.HmiLabel2_Y.Size = New System.Drawing.Size(45, 24)
        Me.HmiLabel2_Y.TabIndex = 11
        '
        'HmiLabel2_X
        '
        Me.HmiLabel2_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel2_X.Location = New System.Drawing.Point(3, 33)
        Me.HmiLabel2_X.Name = "HmiLabel2_X"
        Me.HmiLabel2_X.Size = New System.Drawing.Size(45, 24)
        Me.HmiLabel2_X.TabIndex = 9
        '
        'Label2_Y
        '
        Me.Label2_Y.AutoSize = True
        Me.Label2_Y.BackColor = System.Drawing.Color.LightGray
        Me.Label2_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2_Y.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label2_Y.ForeColor = System.Drawing.Color.Blue
        Me.Label2_Y.Location = New System.Drawing.Point(158, 35)
        Me.Label2_Y.Margin = New System.Windows.Forms.Padding(5)
        Me.Label2_Y.Name = "Label2_Y"
        Me.Label2_Y.Size = New System.Drawing.Size(41, 20)
        Me.Label2_Y.TabIndex = 7
        Me.Label2_Y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2_Z
        '
        Me.Label2_Z.AutoSize = True
        Me.Label2_Z.BackColor = System.Drawing.Color.LightGray
        Me.Label2_Z.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2_Z.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2_Z.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label2_Z.ForeColor = System.Drawing.Color.Blue
        Me.Label2_Z.Location = New System.Drawing.Point(260, 35)
        Me.Label2_Z.Margin = New System.Windows.Forms.Padding(5)
        Me.Label2_Z.Name = "Label2_Z"
        Me.Label2_Z.Size = New System.Drawing.Size(42, 20)
        Me.Label2_Z.TabIndex = 6
        Me.Label2_Z.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2_X
        '
        Me.Label2_X.AutoSize = True
        Me.Label2_X.BackColor = System.Drawing.Color.LightGray
        Me.Label2_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2_X.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2_X.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Label2_X.ForeColor = System.Drawing.Color.Blue
        Me.Label2_X.Location = New System.Drawing.Point(56, 35)
        Me.Label2_X.Margin = New System.Windows.Forms.Padding(5)
        Me.Label2_X.Name = "Label2_X"
        Me.Label2_X.Size = New System.Drawing.Size(41, 20)
        Me.Label2_X.TabIndex = 5
        Me.Label2_X.Text = "1000.00"
        Me.Label2_X.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_HS_Confirm
        '
        Me.HmiTextBox_HS_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_HS_Confirm.Location = New System.Drawing.Point(105, 123)
        Me.HmiTextBox_HS_Confirm.Name = "HmiTextBox_HS_Confirm"
        Me.HmiTextBox_HS_Confirm.Number = 0
        Me.HmiTextBox_HS_Confirm.Size = New System.Drawing.Size(45, 24)
        Me.HmiTextBox_HS_Confirm.TabIndex = 43
        Me.HmiTextBox_HS_Confirm.TextBoxReadOnly = False
        Me.HmiTextBox_HS_Confirm.ValueType = GetType(String)
        '
        'HmiButton_HS_Confirm
        '
        Me.HmiButton_HS_Confirm.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButton_HS_Confirm, 2)
        Me.HmiButton_HS_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_HS_Confirm.Location = New System.Drawing.Point(156, 123)
        Me.HmiButton_HS_Confirm.Name = "HmiButton_HS_Confirm"
        Me.HmiButton_HS_Confirm.Size = New System.Drawing.Size(96, 24)
        Me.HmiButton_HS_Confirm.TabIndex = 49
        Me.HmiButton_HS_Confirm.UseVisualStyleBackColor = True
        '
        'HmiButton_Reset
        '
        Me.HmiButton_Reset.BackColor = System.Drawing.SystemColors.Control
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiButton_Reset, 2)
        Me.HmiButton_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Reset.Location = New System.Drawing.Point(156, 153)
        Me.HmiButton_Reset.Name = "HmiButton_Reset"
        Me.HmiButton_Reset.Size = New System.Drawing.Size(96, 24)
        Me.HmiButton_Reset.TabIndex = 50
        Me.HmiButton_Reset.UseVisualStyleBackColor = True
        '
        'HmiTextBox_NCErrorCode
        '
        Me.HmiTextBox_NCErrorCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_NCErrorCode.Location = New System.Drawing.Point(54, 63)
        Me.HmiTextBox_NCErrorCode.Name = "HmiTextBox_NCErrorCode"
        Me.HmiTextBox_NCErrorCode.Number = 0
        Me.HmiTextBox_NCErrorCode.Size = New System.Drawing.Size(45, 24)
        Me.HmiTextBox_NCErrorCode.TabIndex = 52
        Me.HmiTextBox_NCErrorCode.TextBoxReadOnly = False
        Me.HmiTextBox_NCErrorCode.ValueType = GetType(String)
        '
        'HmiTextBox_GFile
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Right.SetColumnSpan(Me.HmiTextBox_GFile, 3)
        Me.HmiTextBox_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_GFile.Location = New System.Drawing.Point(156, 63)
        Me.HmiTextBox_GFile.Name = "HmiTextBox_GFile"
        Me.HmiTextBox_GFile.Number = 0
        Me.HmiTextBox_GFile.Size = New System.Drawing.Size(148, 24)
        Me.HmiTextBox_GFile.TabIndex = 54
        Me.HmiTextBox_GFile.TextBoxReadOnly = False
        Me.HmiTextBox_GFile.ValueType = GetType(String)
        '
        'TabControl_GFile
        '
        Me.TabControl_GFile.Controls.Add(Me.TabPage1_GFile)
        Me.TabControl_GFile.Controls.Add(Me.TabPage2_GFile)
        Me.TabControl_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_GFile.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_GFile.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_GFile.Name = "TabControl_GFile"
        Me.TabControl_GFile.SelectedIndex = 0
        Me.TabControl_GFile.Size = New System.Drawing.Size(306, 496)
        Me.TabControl_GFile.TabIndex = 2
        '
        'TabPage1_GFile
        '
        Me.TabPage1_GFile.Controls.Add(Me.HmiTableLayoutPanel_Body_Bottom_Left)
        Me.TabPage1_GFile.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1_GFile.Name = "TabPage1_GFile"
        Me.TabPage1_GFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1_GFile.Size = New System.Drawing.Size(298, 470)
        Me.TabPage1_GFile.TabIndex = 0
        Me.TabPage1_GFile.Text = "TabPage1"
        Me.TabPage1_GFile.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_Body_Bottom_Left
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Left.ColumnCount = 2
        Me.HmiTableLayoutPanel_Body_Bottom_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Controls.Add(Me.HmiLabel_MFunction, 1, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Controls.Add(Me.TableLayoutPanel_Body_Bottom_Left_Top, 0, 0)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Controls.Add(Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion, 1, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Name = "HmiTableLayoutPanel_Body_Bottom_Left"
        Me.HmiTableLayoutPanel_Body_Bottom_Left.RowCount = 2
        Me.HmiTableLayoutPanel_Body_Bottom_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left.Size = New System.Drawing.Size(292, 464)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.TabIndex = 1
        '
        'HmiLabel_MFunction
        '
        Me.HmiLabel_MFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_MFunction.Location = New System.Drawing.Point(236, 3)
        Me.HmiLabel_MFunction.Margin = New System.Windows.Forms.Padding(3)
        Me.HmiLabel_MFunction.Name = "HmiLabel_MFunction"
        Me.HmiLabel_MFunction.Size = New System.Drawing.Size(53, 31)
        Me.HmiLabel_MFunction.TabIndex = 2
        Me.HmiLabel_MFunction.Text = "Label2"
        Me.HmiLabel_MFunction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel_Body_Bottom_Left_Top
        '
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnCount = 8
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Controls.Add(Me.Button_New, 2, 0)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Controls.Add(Me.Button_Open, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Controls.Add(Me.Button_Save, 1, 0)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Name = "TableLayoutPanel_Body_Bottom_Left_Top"
        Me.TableLayoutPanel_Body_Bottom_Left_Top.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom_Left_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom_Left_Top.Size = New System.Drawing.Size(233, 37)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.TabIndex = 3
        '
        'Button_New
        '
        Me.Button_New.BackgroundImage = CType(resources.GetObject("Button_New.BackgroundImage"), System.Drawing.Image)
        Me.Button_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_New.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_New.FlatAppearance.BorderSize = 0
        Me.Button_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_New.Location = New System.Drawing.Point(82, 3)
        Me.Button_New.Name = "Button_New"
        Me.Button_New.Size = New System.Drawing.Size(28, 31)
        Me.Button_New.TabIndex = 45
        Me.Button_New.UseVisualStyleBackColor = True
        '
        'Button_Open
        '
        Me.Button_Open.BackgroundImage = CType(resources.GetObject("Button_Open.BackgroundImage"), System.Drawing.Image)
        Me.Button_Open.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_Open.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Open.FlatAppearance.BorderSize = 0
        Me.Button_Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Open.Location = New System.Drawing.Point(48, 3)
        Me.Button_Open.Name = "Button_Open"
        Me.Button_Open.Size = New System.Drawing.Size(28, 31)
        Me.Button_Open.TabIndex = 44
        Me.Button_Open.UseVisualStyleBackColor = True
        '
        'Button_Save
        '
        Me.Button_Save.BackgroundImage = CType(resources.GetObject("Button_Save.BackgroundImage"), System.Drawing.Image)
        Me.Button_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.FlatAppearance.BorderSize = 0
        Me.Button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Save.Location = New System.Drawing.Point(17, 3)
        Me.Button_Save.Margin = New System.Windows.Forms.Padding(6, 3, 3, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(25, 31)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_Body_Bottom_Left_MFucntion
        '
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.ColumnCount = 1
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Location = New System.Drawing.Point(234, 38)
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Name = "HmiTableLayoutPanel_Body_Bottom_Left_MFucntion"
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowCount = 1
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.Size = New System.Drawing.Size(57, 425)
        Me.HmiTableLayoutPanel_Body_Bottom_Left_MFucntion.TabIndex = 4
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RichTextBox_GCode, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBox_Read, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 40)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(227, 421)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'RichTextBox_GCode
        '
        Me.RichTextBox_GCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox_GCode.Font = New System.Drawing.Font("Calibri", 9.0!)
        Me.RichTextBox_GCode.Location = New System.Drawing.Point(2, 2)
        Me.RichTextBox_GCode.Margin = New System.Windows.Forms.Padding(2)
        Me.RichTextBox_GCode.Name = "RichTextBox_GCode"
        Me.RichTextBox_GCode.Size = New System.Drawing.Size(223, 353)
        Me.RichTextBox_GCode.TabIndex = 2
        Me.RichTextBox_GCode.Text = ""
        Me.RichTextBox_GCode.WordWrap = False
        '
        'TextBox_Read
        '
        Me.TextBox_Read.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox_Read.Location = New System.Drawing.Point(3, 360)
        Me.TextBox_Read.Name = "TextBox_Read"
        Me.TextBox_Read.Size = New System.Drawing.Size(221, 58)
        Me.TextBox_Read.TabIndex = 3
        Me.TextBox_Read.Text = ""
        Me.TextBox_Read.WordWrap = False
        '
        'TabPage2_GFile
        '
        Me.TabPage2_GFile.Controls.Add(Me.HmiTableLayoutPanel_GFile)
        Me.TabPage2_GFile.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2_GFile.Name = "TabPage2_GFile"
        Me.TabPage2_GFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2_GFile.Size = New System.Drawing.Size(298, 470)
        Me.TabPage2_GFile.TabIndex = 1
        Me.TabPage2_GFile.Text = "TabPage2"
        Me.TabPage2_GFile.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_GFile
        '
        Me.HmiTableLayoutPanel_GFile.ColumnCount = 2
        Me.HmiTableLayoutPanel_GFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.0!))
        Me.HmiTableLayoutPanel_GFile.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.HmiTableLayoutPanel_GFile.Controls.Add(Me.TableLayoutPanel_GFileLink2Right, 1, 1)
        Me.HmiTableLayoutPanel_GFile.Controls.Add(Me.HmiDataView_GFile, 0, 0)
        Me.HmiTableLayoutPanel_GFile.Controls.Add(Me.HmiDataView_GFile2, 0, 1)
        Me.HmiTableLayoutPanel_GFile.Controls.Add(Me.TableLayoutPanel_GFileLink1Right, 1, 0)
        Me.HmiTableLayoutPanel_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_GFile.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_GFile.Name = "HmiTableLayoutPanel_GFile"
        Me.HmiTableLayoutPanel_GFile.RowCount = 2
        Me.HmiTableLayoutPanel_GFile.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_GFile.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_GFile.Size = New System.Drawing.Size(292, 464)
        Me.HmiTableLayoutPanel_GFile.TabIndex = 0
        '
        'TableLayoutPanel_GFileLink2Right
        '
        Me.TableLayoutPanel_GFileLink2Right.ColumnCount = 1
        Me.TableLayoutPanel_GFileLink2Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiButton_LoadVariant, 0, 2)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiTextBox_GFileLink2, 0, 4)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiButton_GFileLinkChoose2, 0, 5)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiButton_GFileLinkModify2, 0, 6)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiLabel_GFileName2, 0, 3)
        Me.TableLayoutPanel_GFileLink2Right.Controls.Add(Me.HmiComboBox_Variant, 0, 1)
        Me.TableLayoutPanel_GFileLink2Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_GFileLink2Right.Location = New System.Drawing.Point(222, 235)
        Me.TableLayoutPanel_GFileLink2Right.Name = "TableLayoutPanel_GFileLink2Right"
        Me.TableLayoutPanel_GFileLink2Right.RowCount = 8
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink2Right.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_GFileLink2Right.Size = New System.Drawing.Size(67, 226)
        Me.TableLayoutPanel_GFileLink2Right.TabIndex = 4
        '
        'HmiButton_LoadVariant
        '
        Me.HmiButton_LoadVariant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_LoadVariant.Location = New System.Drawing.Point(3, 43)
        Me.HmiButton_LoadVariant.MarginHeight = 6
        Me.HmiButton_LoadVariant.Name = "HmiButton_LoadVariant"
        Me.HmiButton_LoadVariant.Size = New System.Drawing.Size(61, 14)
        Me.HmiButton_LoadVariant.TabIndex = 6
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(61, 14)
        Me.HmiLabel_Variant.TabIndex = 4
        '
        'HmiTextBox_GFileLink2
        '
        Me.HmiTextBox_GFileLink2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_GFileLink2.Location = New System.Drawing.Point(3, 83)
        Me.HmiTextBox_GFileLink2.Name = "HmiTextBox_GFileLink2"
        Me.HmiTextBox_GFileLink2.Number = 0
        Me.HmiTextBox_GFileLink2.Size = New System.Drawing.Size(61, 14)
        Me.HmiTextBox_GFileLink2.TabIndex = 0
        Me.HmiTextBox_GFileLink2.TextBoxReadOnly = False
        Me.HmiTextBox_GFileLink2.ValueType = GetType(String)
        '
        'HmiButton_GFileLinkChoose2
        '
        Me.HmiButton_GFileLinkChoose2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_GFileLinkChoose2.Location = New System.Drawing.Point(3, 103)
        Me.HmiButton_GFileLinkChoose2.MarginHeight = 6
        Me.HmiButton_GFileLinkChoose2.Name = "HmiButton_GFileLinkChoose2"
        Me.HmiButton_GFileLinkChoose2.Size = New System.Drawing.Size(61, 14)
        Me.HmiButton_GFileLinkChoose2.TabIndex = 1
        '
        'HmiButton_GFileLinkModify2
        '
        Me.HmiButton_GFileLinkModify2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_GFileLinkModify2.Location = New System.Drawing.Point(3, 123)
        Me.HmiButton_GFileLinkModify2.MarginHeight = 6
        Me.HmiButton_GFileLinkModify2.Name = "HmiButton_GFileLinkModify2"
        Me.HmiButton_GFileLinkModify2.Size = New System.Drawing.Size(61, 14)
        Me.HmiButton_GFileLinkModify2.TabIndex = 2
        '
        'HmiLabel_GFileName2
        '
        Me.HmiLabel_GFileName2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_GFileName2.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_GFileName2.Name = "HmiLabel_GFileName2"
        Me.HmiLabel_GFileName2.Size = New System.Drawing.Size(61, 14)
        Me.HmiLabel_GFileName2.TabIndex = 3
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(3, 23)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(61, 14)
        Me.HmiComboBox_Variant.TabIndex = 5
        '
        'HmiDataView_GFile
        '
        Me.HmiDataView_GFile.AllowUserToAddRows = False
        Me.HmiDataView_GFile.AllowUserToDeleteRows = False
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_GFile.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle11
        Me.HmiDataView_GFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_GFile.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_GFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_GFile.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle12.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_GFile.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.HmiDataView_GFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle13.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HmiDataView_GFile.DefaultCellStyle = DataGridViewCellStyle13
        Me.HmiDataView_GFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_GFile.EnableHeadersVisualStyles = False
        Me.HmiDataView_GFile.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_GFile.Location = New System.Drawing.Point(3, 3)
        Me.HmiDataView_GFile.Name = "HmiDataView_GFile"
        Me.HmiDataView_GFile.ReadOnly = True
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle14.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_GFile.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.HmiDataView_GFile.RowHeadersVisible = False
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_GFile.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.HmiDataView_GFile.RowTemplate.Height = 40
        Me.HmiDataView_GFile.RowTemplate.ReadOnly = True
        Me.HmiDataView_GFile.Size = New System.Drawing.Size(213, 226)
        Me.HmiDataView_GFile.TabIndex = 3
        '
        'HmiDataView_GFile2
        '
        Me.HmiDataView_GFile2.AllowUserToAddRows = False
        Me.HmiDataView_GFile2.AllowUserToDeleteRows = False
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_GFile2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle16
        Me.HmiDataView_GFile2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_GFile2.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_GFile2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_GFile2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle17.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_GFile2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle17
        Me.HmiDataView_GFile2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HmiDataView_GFile2.DefaultCellStyle = DataGridViewCellStyle18
        Me.HmiDataView_GFile2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_GFile2.EnableHeadersVisualStyles = False
        Me.HmiDataView_GFile2.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_GFile2.Location = New System.Drawing.Point(3, 242)
        Me.HmiDataView_GFile2.Margin = New System.Windows.Forms.Padding(3, 10, 3, 3)
        Me.HmiDataView_GFile2.Name = "HmiDataView_GFile2"
        Me.HmiDataView_GFile2.ReadOnly = True
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("SimSun", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_GFile2.RowHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.HmiDataView_GFile2.RowHeadersVisible = False
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_GFile2.RowsDefaultCellStyle = DataGridViewCellStyle20
        Me.HmiDataView_GFile2.RowTemplate.Height = 40
        Me.HmiDataView_GFile2.RowTemplate.ReadOnly = True
        Me.HmiDataView_GFile2.Size = New System.Drawing.Size(213, 219)
        Me.HmiDataView_GFile2.TabIndex = 2
        '
        'TableLayoutPanel_GFileLink1Right
        '
        Me.TableLayoutPanel_GFileLink1Right.ColumnCount = 1
        Me.TableLayoutPanel_GFileLink1Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiTextBox_Name1, 0, 1)
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiLabel_Name1, 0, 0)
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiTextBox_GFileLink1, 0, 3)
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiButton_GFileLinkChoose1, 0, 4)
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiButton_GFileLinkModify1, 0, 5)
        Me.TableLayoutPanel_GFileLink1Right.Controls.Add(Me.HmiLabel_GFileName1, 0, 2)
        Me.TableLayoutPanel_GFileLink1Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_GFileLink1Right.Location = New System.Drawing.Point(222, 3)
        Me.TableLayoutPanel_GFileLink1Right.Name = "TableLayoutPanel_GFileLink1Right"
        Me.TableLayoutPanel_GFileLink1Right.RowCount = 7
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_GFileLink1Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_GFileLink1Right.Size = New System.Drawing.Size(67, 226)
        Me.TableLayoutPanel_GFileLink1Right.TabIndex = 1
        '
        'HmiTextBox_Name1
        '
        Me.HmiTextBox_Name1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Name1.Location = New System.Drawing.Point(3, 23)
        Me.HmiTextBox_Name1.Name = "HmiTextBox_Name1"
        Me.HmiTextBox_Name1.Number = 0
        Me.HmiTextBox_Name1.Size = New System.Drawing.Size(61, 14)
        Me.HmiTextBox_Name1.TabIndex = 5
        Me.HmiTextBox_Name1.TextBoxReadOnly = False
        Me.HmiTextBox_Name1.ValueType = GetType(String)
        '
        'HmiLabel_Name1
        '
        Me.HmiLabel_Name1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Name1.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Name1.Name = "HmiLabel_Name1"
        Me.HmiLabel_Name1.Size = New System.Drawing.Size(61, 14)
        Me.HmiLabel_Name1.TabIndex = 4
        '
        'HmiTextBox_GFileLink1
        '
        Me.HmiTextBox_GFileLink1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_GFileLink1.Location = New System.Drawing.Point(3, 63)
        Me.HmiTextBox_GFileLink1.Name = "HmiTextBox_GFileLink1"
        Me.HmiTextBox_GFileLink1.Number = 0
        Me.HmiTextBox_GFileLink1.Size = New System.Drawing.Size(61, 14)
        Me.HmiTextBox_GFileLink1.TabIndex = 0
        Me.HmiTextBox_GFileLink1.TextBoxReadOnly = False
        Me.HmiTextBox_GFileLink1.ValueType = GetType(String)
        '
        'HmiButton_GFileLinkChoose1
        '
        Me.HmiButton_GFileLinkChoose1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_GFileLinkChoose1.Location = New System.Drawing.Point(3, 83)
        Me.HmiButton_GFileLinkChoose1.MarginHeight = 6
        Me.HmiButton_GFileLinkChoose1.Name = "HmiButton_GFileLinkChoose1"
        Me.HmiButton_GFileLinkChoose1.Size = New System.Drawing.Size(61, 14)
        Me.HmiButton_GFileLinkChoose1.TabIndex = 1
        '
        'HmiButton_GFileLinkModify1
        '
        Me.HmiButton_GFileLinkModify1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_GFileLinkModify1.Location = New System.Drawing.Point(3, 103)
        Me.HmiButton_GFileLinkModify1.MarginHeight = 6
        Me.HmiButton_GFileLinkModify1.Name = "HmiButton_GFileLinkModify1"
        Me.HmiButton_GFileLinkModify1.Size = New System.Drawing.Size(61, 14)
        Me.HmiButton_GFileLinkModify1.TabIndex = 2
        '
        'HmiLabel_GFileName1
        '
        Me.HmiLabel_GFileName1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_GFileName1.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_GFileName1.Name = "HmiLabel_GFileName1"
        Me.HmiLabel_GFileName1.Size = New System.Drawing.Size(61, 14)
        Me.HmiLabel_GFileName1.TabIndex = 3
        '
        'GFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 498)
        Me.Controls.Add(Me.Panel_UI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "GFile"
        Me.Text = "GFile"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Right.PerformLayout()
        Me.HmiTableLayoutPanel.ResumeLayout(False)
        Me.TabControl_GFile.ResumeLayout(False)
        Me.TabPage1_GFile.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Bottom_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom_Left_Top.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TabPage2_GFile.ResumeLayout(False)
        Me.HmiTableLayoutPanel_GFile.ResumeLayout(False)
        Me.TableLayoutPanel_GFileLink2Right.ResumeLayout(False)
        CType(Me.HmiDataView_GFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HmiDataView_GFile2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_GFileLink1Right.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog_Path As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Body_Bottom_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiButton_MotorEnable As HMIButtonWithIndicate
    Friend WithEvents HmiTextBox_Reset As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Reset As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_HS_Confirm As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTableLayoutPanel As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel2_Z As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel2_Y As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel2_X As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents Label2_Y As System.Windows.Forms.Label
    Friend WithEvents Label2_Z As System.Windows.Forms.Label
    Friend WithEvents Label2_X As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_HS_Confirm As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_HS_Confirm As HMIButtonWithIndicate
    Friend WithEvents HmiButton_Reset As HMIButtonWithIndicate
    Friend WithEvents HmiButton_Build As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_Release As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents HmiButton_Load As Kochi.HMI.MainControl.Device.GapFiller.GapFillerButton
    Friend WithEvents TabControl_GFile As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1_GFile As System.Windows.Forms.TabPage
    Friend WithEvents HmiTableLayoutPanel_Body_Bottom_Left As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_MFunction As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel_Body_Bottom_Left_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_New As System.Windows.Forms.Button
    Friend WithEvents Button_Open As System.Windows.Forms.Button
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents HmiTableLayoutPanel_Body_Bottom_Left_MFucntion As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TabPage2_GFile As System.Windows.Forms.TabPage
    Friend WithEvents HmiTableLayoutPanel_GFile As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel_GFileLink1Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_GFileLink1 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_GFileLinkChoose1 As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_GFileLinkModify1 As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents TableLayoutPanel_GFileLink2Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_GFileLink2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_GFileLinkChoose2 As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_GFileLinkModify2 As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiDataView_GFile As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataView_GFile2 As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_GFileName2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_Name1 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Name1 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_GFileName1 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_LoadVariant As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_GFile As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_NC_ErrorCode As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_NCErrorCode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_GFile As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_RWrite As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_RRead As HMIButtonWithIndicate
    Friend WithEvents HmiButton_Stop As HMIButtonWithIndicate
    Friend WithEvents HmiButton_Start As HMIButtonWithIndicate
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RichTextBox_GCode As System.Windows.Forms.RichTextBox
    Friend WithEvents TextBox_Read As System.Windows.Forms.RichTextBox
End Class
