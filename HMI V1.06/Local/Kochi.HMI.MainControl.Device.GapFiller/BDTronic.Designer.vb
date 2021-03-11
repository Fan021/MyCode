<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BDTronic
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
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_PPS = New System.Windows.Forms.GroupBox()
        Me.GroupBox_B2000 = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel1 = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiButtonWithIndicate_PPSHS_OPMode3 = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_PPSHS_OPMode2 = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_PPSHS_OPMode1 = New HMIButtonWithIndicate(Me.components)
        Me.HmiLabel8 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel4 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel3 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel1 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_PPS_Active = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTextBox_PPSactOP_Mode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSactUser_Level = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel5 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel6 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PPSFillingLevelP1 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSFillingLevelP2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSSupplyPressureP1 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSSupplyPressureP2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel7 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel9 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PPSPressureP1Outlet = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSPressureP2Outlet = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel10 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PPSOP_Mode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel11 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel12 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_PPSscanProcessReadyMESA = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_PPSscanProcessReadyMESB = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiButtonWithIndicate_PPSHS_MESokA = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_PPSHS_MESokB = New HMIButtonWithIndicate(Me.components)
        Me.HmiLabel13 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel14 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel15 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel16 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel17 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel18 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PPSstrPartNoA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrVolumeA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrExpiryDateA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrBatchNoA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrSupplierNoA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrPackagingNoA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel19 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel20 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel21 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel22 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel23 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel24 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PPSstrPartNoB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrSupplierNoB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrVolumeB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrPackagingNoB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrExpiryDateB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PPSstrBatchNoB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_MES_StatusA = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTableLayoutPanel2 = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiButtonWithIndicate_B2000HS_OPMode3 = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_B2000HS_OPMode2 = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_B2000HS_OPMode1 = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_B2000Material_OK = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_B2000Cleaning = New HMIButtonWithIndicate(Me.components)
        Me.HmiLabel25 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel26 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel27 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel28 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_B2000Ready = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel29 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel30 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel32 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel33 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000actOP_Mode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_B2000requestPostiton = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel34 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000FillingLevel1 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButtonWithIndicate_B2000Start = New HMIButtonWithIndicate(Me.components)
        Me.HmiButtonWithIndicate_B2000Filling = New HMIButtonWithIndicate(Me.components)
        Me.HmiLabel41 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_B2000Busy = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_B2000System_OK = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_B2000ProcessCycle_OK = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_B2000ProcessCycle_NOK = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiSensor_B2000Handshake_active = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel31 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel49 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000actRecipe = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_B2000actUserLevel = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel50 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000FillingLevel2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_B2000Position = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel37 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000OP_Mode = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel38 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_B2000RecipeNumber = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_MES_StatusB = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_PPS.SuspendLayout()
        Me.GroupBox_B2000.SuspendLayout()
        Me.HmiTableLayoutPanel1.SuspendLayout()
        Me.HmiTableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_UI
        '
        Me.Panel_UI.BackColor = System.Drawing.Color.White
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
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_PPS, 1, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_B2000, 1, 2)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 3
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(615, 498)
        Me.TableLayoutPanel_Body.TabIndex = 2
        '
        'GroupBox_PPS
        '
        Me.GroupBox_PPS.Controls.Add(Me.HmiTableLayoutPanel1)
        Me.GroupBox_PPS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_PPS.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_PPS.Location = New System.Drawing.Point(1, 1)
        Me.GroupBox_PPS.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox_PPS.Name = "GroupBox_PPS"
        Me.GroupBox_PPS.Size = New System.Drawing.Size(613, 271)
        Me.GroupBox_PPS.TabIndex = 1
        Me.GroupBox_PPS.TabStop = False
        Me.GroupBox_PPS.Text = "GroupBox1"
        '
        'GroupBox_B2000
        '
        Me.GroupBox_B2000.Controls.Add(Me.HmiTableLayoutPanel2)
        Me.GroupBox_B2000.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_B2000.Location = New System.Drawing.Point(3, 276)
        Me.GroupBox_B2000.Name = "GroupBox_B2000"
        Me.GroupBox_B2000.Size = New System.Drawing.Size(609, 219)
        Me.GroupBox_B2000.TabIndex = 2
        Me.GroupBox_B2000.TabStop = False
        Me.GroupBox_B2000.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel1
        '
        Me.HmiTableLayoutPanel1.ColumnCount = 8
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_MES_StatusB, 6, 9)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_PPSHS_OPMode3, 6, 8)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_PPSHS_OPMode2, 4, 8)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_PPSHS_OPMode1, 2, 8)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel8, 0, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel4, 4, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel3, 2, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel1, 0, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiSensor_PPS_Active, 1, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSactOP_Mode, 3, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSactUser_Level, 5, 0)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel2, 2, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel5, 4, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel6, 6, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSFillingLevelP1, 1, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSFillingLevelP2, 3, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSSupplyPressureP1, 5, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSSupplyPressureP2, 7, 1)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel7, 0, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel9, 2, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSPressureP1Outlet, 1, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSPressureP2Outlet, 3, 2)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel10, 0, 8)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSOP_Mode, 1, 8)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel11, 0, 3)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel12, 2, 3)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiSensor_PPSscanProcessReadyMESA, 1, 3)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiSensor_PPSscanProcessReadyMESB, 3, 3)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_PPSHS_MESokA, 0, 9)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiButtonWithIndicate_PPSHS_MESokB, 2, 9)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel13, 0, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel14, 2, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel15, 4, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel16, 6, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel17, 0, 5)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel18, 2, 5)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrPartNoA, 1, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrVolumeA, 3, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrExpiryDateA, 5, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrBatchNoA, 7, 4)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrSupplierNoA, 1, 5)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrPackagingNoA, 3, 5)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel19, 0, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel20, 2, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel21, 4, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel22, 6, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel23, 0, 7)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiLabel24, 2, 7)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrPartNoB, 1, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrSupplierNoB, 1, 7)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrVolumeB, 3, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrPackagingNoB, 3, 7)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrExpiryDateB, 5, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_PPSstrBatchNoB, 7, 6)
        Me.HmiTableLayoutPanel1.Controls.Add(Me.HmiTextBox_MES_StatusA, 4, 9)
        Me.HmiTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel1.Location = New System.Drawing.Point(3, 20)
        Me.HmiTableLayoutPanel1.Name = "HmiTableLayoutPanel1"
        Me.HmiTableLayoutPanel1.RowCount = 11
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.HmiTableLayoutPanel1.Size = New System.Drawing.Size(607, 248)
        Me.HmiTableLayoutPanel1.TabIndex = 0
        '
        'HmiButtonWithIndicate_PPSHS_OPMode3
        '
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Location = New System.Drawing.Point(454, 161)
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Name = "HmiButtonWithIndicate_PPSHS_OPMode3"
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Size = New System.Drawing.Size(89, 18)
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.TabIndex = 158
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_PPSHS_OPMode3.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_PPSHS_OPMode2
        '
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Location = New System.Drawing.Point(303, 161)
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Name = "HmiButtonWithIndicate_PPSHS_OPMode2"
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Size = New System.Drawing.Size(89, 18)
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.TabIndex = 157
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_PPSHS_OPMode2.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_PPSHS_OPMode1
        '
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Location = New System.Drawing.Point(152, 161)
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Name = "HmiButtonWithIndicate_PPSHS_OPMode1"
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Size = New System.Drawing.Size(89, 18)
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.TabIndex = 156
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_PPSHS_OPMode1.UseVisualStyleBackColor = True
        '
        'HmiLabel8
        '
        Me.HmiLabel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel8.Location = New System.Drawing.Point(1, 21)
        Me.HmiLabel8.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel8.Name = "HmiLabel8"
        Me.HmiLabel8.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel8.TabIndex = 59
        '
        'HmiLabel4
        '
        Me.HmiLabel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel4.Location = New System.Drawing.Point(303, 1)
        Me.HmiLabel4.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel4.Name = "HmiLabel4"
        Me.HmiLabel4.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel4.TabIndex = 55
        '
        'HmiLabel3
        '
        Me.HmiLabel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel3.Location = New System.Drawing.Point(152, 1)
        Me.HmiLabel3.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel3.Name = "HmiLabel3"
        Me.HmiLabel3.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel3.TabIndex = 54
        '
        'HmiLabel1
        '
        Me.HmiLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel1.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel1.Name = "HmiLabel1"
        Me.HmiLabel1.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel1.TabIndex = 52
        '
        'HmiSensor_PPS_Active
        '
        Me.HmiSensor_PPS_Active.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_PPS_Active.Location = New System.Drawing.Point(94, 3)
        Me.HmiSensor_PPS_Active.Name = "HmiSensor_PPS_Active"
        Me.HmiSensor_PPS_Active.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_PPS_Active.TabIndex = 110
        '
        'HmiTextBox_PPSactOP_Mode
        '
        Me.HmiTextBox_PPSactOP_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSactOP_Mode.Location = New System.Drawing.Point(243, 1)
        Me.HmiTextBox_PPSactOP_Mode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSactOP_Mode.Name = "HmiTextBox_PPSactOP_Mode"
        Me.HmiTextBox_PPSactOP_Mode.Number = 0
        Me.HmiTextBox_PPSactOP_Mode.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSactOP_Mode.TabIndex = 111
        Me.HmiTextBox_PPSactOP_Mode.TextBoxReadOnly = False
        Me.HmiTextBox_PPSactOP_Mode.ValueType = GetType(String)
        '
        'HmiTextBox_PPSactUser_Level
        '
        Me.HmiTextBox_PPSactUser_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSactUser_Level.Location = New System.Drawing.Point(394, 1)
        Me.HmiTextBox_PPSactUser_Level.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSactUser_Level.Name = "HmiTextBox_PPSactUser_Level"
        Me.HmiTextBox_PPSactUser_Level.Number = 0
        Me.HmiTextBox_PPSactUser_Level.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSactUser_Level.TabIndex = 112
        Me.HmiTextBox_PPSactUser_Level.TextBoxReadOnly = False
        Me.HmiTextBox_PPSactUser_Level.ValueType = GetType(String)
        '
        'HmiLabel2
        '
        Me.HmiLabel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel2.Location = New System.Drawing.Point(152, 21)
        Me.HmiLabel2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel2.Name = "HmiLabel2"
        Me.HmiLabel2.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel2.TabIndex = 113
        '
        'HmiLabel5
        '
        Me.HmiLabel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel5.Location = New System.Drawing.Point(303, 21)
        Me.HmiLabel5.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel5.Name = "HmiLabel5"
        Me.HmiLabel5.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel5.TabIndex = 114
        '
        'HmiLabel6
        '
        Me.HmiLabel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel6.Location = New System.Drawing.Point(454, 21)
        Me.HmiLabel6.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel6.Name = "HmiLabel6"
        Me.HmiLabel6.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel6.TabIndex = 115
        '
        'HmiTextBox_PPSFillingLevelP1
        '
        Me.HmiTextBox_PPSFillingLevelP1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSFillingLevelP1.Location = New System.Drawing.Point(92, 21)
        Me.HmiTextBox_PPSFillingLevelP1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSFillingLevelP1.Name = "HmiTextBox_PPSFillingLevelP1"
        Me.HmiTextBox_PPSFillingLevelP1.Number = 0
        Me.HmiTextBox_PPSFillingLevelP1.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSFillingLevelP1.TabIndex = 116
        Me.HmiTextBox_PPSFillingLevelP1.TextBoxReadOnly = False
        Me.HmiTextBox_PPSFillingLevelP1.ValueType = GetType(String)
        '
        'HmiTextBox_PPSFillingLevelP2
        '
        Me.HmiTextBox_PPSFillingLevelP2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSFillingLevelP2.Location = New System.Drawing.Point(243, 21)
        Me.HmiTextBox_PPSFillingLevelP2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSFillingLevelP2.Name = "HmiTextBox_PPSFillingLevelP2"
        Me.HmiTextBox_PPSFillingLevelP2.Number = 0
        Me.HmiTextBox_PPSFillingLevelP2.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSFillingLevelP2.TabIndex = 117
        Me.HmiTextBox_PPSFillingLevelP2.TextBoxReadOnly = False
        Me.HmiTextBox_PPSFillingLevelP2.ValueType = GetType(String)
        '
        'HmiTextBox_PPSSupplyPressureP1
        '
        Me.HmiTextBox_PPSSupplyPressureP1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSSupplyPressureP1.Location = New System.Drawing.Point(394, 21)
        Me.HmiTextBox_PPSSupplyPressureP1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSSupplyPressureP1.Name = "HmiTextBox_PPSSupplyPressureP1"
        Me.HmiTextBox_PPSSupplyPressureP1.Number = 0
        Me.HmiTextBox_PPSSupplyPressureP1.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSSupplyPressureP1.TabIndex = 118
        Me.HmiTextBox_PPSSupplyPressureP1.TextBoxReadOnly = False
        Me.HmiTextBox_PPSSupplyPressureP1.ValueType = GetType(String)
        '
        'HmiTextBox_PPSSupplyPressureP2
        '
        Me.HmiTextBox_PPSSupplyPressureP2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSSupplyPressureP2.Location = New System.Drawing.Point(545, 21)
        Me.HmiTextBox_PPSSupplyPressureP2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSSupplyPressureP2.Name = "HmiTextBox_PPSSupplyPressureP2"
        Me.HmiTextBox_PPSSupplyPressureP2.Number = 0
        Me.HmiTextBox_PPSSupplyPressureP2.Size = New System.Drawing.Size(61, 18)
        Me.HmiTextBox_PPSSupplyPressureP2.TabIndex = 119
        Me.HmiTextBox_PPSSupplyPressureP2.TextBoxReadOnly = False
        Me.HmiTextBox_PPSSupplyPressureP2.ValueType = GetType(String)
        '
        'HmiLabel7
        '
        Me.HmiLabel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel7.Location = New System.Drawing.Point(1, 41)
        Me.HmiLabel7.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel7.Name = "HmiLabel7"
        Me.HmiLabel7.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel7.TabIndex = 120
        '
        'HmiLabel9
        '
        Me.HmiLabel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel9.Location = New System.Drawing.Point(152, 41)
        Me.HmiLabel9.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel9.Name = "HmiLabel9"
        Me.HmiLabel9.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel9.TabIndex = 121
        '
        'HmiTextBox_PPSPressureP1Outlet
        '
        Me.HmiTextBox_PPSPressureP1Outlet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSPressureP1Outlet.Location = New System.Drawing.Point(92, 41)
        Me.HmiTextBox_PPSPressureP1Outlet.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSPressureP1Outlet.Name = "HmiTextBox_PPSPressureP1Outlet"
        Me.HmiTextBox_PPSPressureP1Outlet.Number = 0
        Me.HmiTextBox_PPSPressureP1Outlet.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSPressureP1Outlet.TabIndex = 122
        Me.HmiTextBox_PPSPressureP1Outlet.TextBoxReadOnly = False
        Me.HmiTextBox_PPSPressureP1Outlet.ValueType = GetType(String)
        '
        'HmiTextBox_PPSPressureP2Outlet
        '
        Me.HmiTextBox_PPSPressureP2Outlet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSPressureP2Outlet.Location = New System.Drawing.Point(243, 41)
        Me.HmiTextBox_PPSPressureP2Outlet.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSPressureP2Outlet.Name = "HmiTextBox_PPSPressureP2Outlet"
        Me.HmiTextBox_PPSPressureP2Outlet.Number = 0
        Me.HmiTextBox_PPSPressureP2Outlet.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSPressureP2Outlet.TabIndex = 123
        Me.HmiTextBox_PPSPressureP2Outlet.TextBoxReadOnly = False
        Me.HmiTextBox_PPSPressureP2Outlet.ValueType = GetType(String)
        '
        'HmiLabel10
        '
        Me.HmiLabel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel10.Location = New System.Drawing.Point(1, 161)
        Me.HmiLabel10.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel10.Name = "HmiLabel10"
        Me.HmiLabel10.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel10.TabIndex = 124
        '
        'HmiTextBox_PPSOP_Mode
        '
        Me.HmiTextBox_PPSOP_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSOP_Mode.Location = New System.Drawing.Point(92, 161)
        Me.HmiTextBox_PPSOP_Mode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSOP_Mode.Name = "HmiTextBox_PPSOP_Mode"
        Me.HmiTextBox_PPSOP_Mode.Number = 0
        Me.HmiTextBox_PPSOP_Mode.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSOP_Mode.TabIndex = 125
        Me.HmiTextBox_PPSOP_Mode.TextBoxReadOnly = False
        Me.HmiTextBox_PPSOP_Mode.ValueType = GetType(String)
        '
        'HmiLabel11
        '
        Me.HmiLabel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel11.Location = New System.Drawing.Point(1, 61)
        Me.HmiLabel11.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel11.Name = "HmiLabel11"
        Me.HmiLabel11.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel11.TabIndex = 126
        '
        'HmiLabel12
        '
        Me.HmiLabel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel12.Location = New System.Drawing.Point(152, 61)
        Me.HmiLabel12.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel12.Name = "HmiLabel12"
        Me.HmiLabel12.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel12.TabIndex = 127
        '
        'HmiSensor_PPSscanProcessReadyMESA
        '
        Me.HmiSensor_PPSscanProcessReadyMESA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_PPSscanProcessReadyMESA.Location = New System.Drawing.Point(94, 63)
        Me.HmiSensor_PPSscanProcessReadyMESA.Name = "HmiSensor_PPSscanProcessReadyMESA"
        Me.HmiSensor_PPSscanProcessReadyMESA.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_PPSscanProcessReadyMESA.TabIndex = 128
        '
        'HmiSensor_PPSscanProcessReadyMESB
        '
        Me.HmiSensor_PPSscanProcessReadyMESB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_PPSscanProcessReadyMESB.Location = New System.Drawing.Point(245, 63)
        Me.HmiSensor_PPSscanProcessReadyMESB.Name = "HmiSensor_PPSscanProcessReadyMESB"
        Me.HmiSensor_PPSscanProcessReadyMESB.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_PPSscanProcessReadyMESB.TabIndex = 129
        '
        'HmiButtonWithIndicate_PPSHS_MESokA
        '
        Me.HmiButtonWithIndicate_PPSHS_MESokA.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Location = New System.Drawing.Point(1, 181)
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Name = "HmiButtonWithIndicate_PPSHS_MESokA"
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Size = New System.Drawing.Size(89, 18)
        Me.HmiButtonWithIndicate_PPSHS_MESokA.TabIndex = 130
        Me.HmiButtonWithIndicate_PPSHS_MESokA.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_PPSHS_MESokA.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_PPSHS_MESokB
        '
        Me.HmiButtonWithIndicate_PPSHS_MESokB.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Location = New System.Drawing.Point(152, 181)
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Name = "HmiButtonWithIndicate_PPSHS_MESokB"
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Size = New System.Drawing.Size(89, 18)
        Me.HmiButtonWithIndicate_PPSHS_MESokB.TabIndex = 131
        Me.HmiButtonWithIndicate_PPSHS_MESokB.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_PPSHS_MESokB.UseVisualStyleBackColor = True
        '
        'HmiLabel13
        '
        Me.HmiLabel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel13.Location = New System.Drawing.Point(1, 81)
        Me.HmiLabel13.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel13.Name = "HmiLabel13"
        Me.HmiLabel13.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel13.TabIndex = 132
        '
        'HmiLabel14
        '
        Me.HmiLabel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel14.Location = New System.Drawing.Point(152, 81)
        Me.HmiLabel14.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel14.Name = "HmiLabel14"
        Me.HmiLabel14.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel14.TabIndex = 133
        '
        'HmiLabel15
        '
        Me.HmiLabel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel15.Location = New System.Drawing.Point(303, 81)
        Me.HmiLabel15.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel15.Name = "HmiLabel15"
        Me.HmiLabel15.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel15.TabIndex = 134
        '
        'HmiLabel16
        '
        Me.HmiLabel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel16.Location = New System.Drawing.Point(454, 81)
        Me.HmiLabel16.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel16.Name = "HmiLabel16"
        Me.HmiLabel16.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel16.TabIndex = 135
        '
        'HmiLabel17
        '
        Me.HmiLabel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel17.Location = New System.Drawing.Point(1, 101)
        Me.HmiLabel17.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel17.Name = "HmiLabel17"
        Me.HmiLabel17.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel17.TabIndex = 136
        '
        'HmiLabel18
        '
        Me.HmiLabel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel18.Location = New System.Drawing.Point(152, 101)
        Me.HmiLabel18.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel18.Name = "HmiLabel18"
        Me.HmiLabel18.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel18.TabIndex = 137
        '
        'HmiTextBox_PPSstrPartNoA
        '
        Me.HmiTextBox_PPSstrPartNoA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrPartNoA.Location = New System.Drawing.Point(92, 81)
        Me.HmiTextBox_PPSstrPartNoA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrPartNoA.Name = "HmiTextBox_PPSstrPartNoA"
        Me.HmiTextBox_PPSstrPartNoA.Number = 0
        Me.HmiTextBox_PPSstrPartNoA.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrPartNoA.TabIndex = 138
        Me.HmiTextBox_PPSstrPartNoA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrPartNoA.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrVolumeA
        '
        Me.HmiTextBox_PPSstrVolumeA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrVolumeA.Location = New System.Drawing.Point(243, 81)
        Me.HmiTextBox_PPSstrVolumeA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrVolumeA.Name = "HmiTextBox_PPSstrVolumeA"
        Me.HmiTextBox_PPSstrVolumeA.Number = 0
        Me.HmiTextBox_PPSstrVolumeA.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrVolumeA.TabIndex = 139
        Me.HmiTextBox_PPSstrVolumeA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrVolumeA.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrExpiryDateA
        '
        Me.HmiTextBox_PPSstrExpiryDateA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrExpiryDateA.Location = New System.Drawing.Point(394, 81)
        Me.HmiTextBox_PPSstrExpiryDateA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrExpiryDateA.Name = "HmiTextBox_PPSstrExpiryDateA"
        Me.HmiTextBox_PPSstrExpiryDateA.Number = 0
        Me.HmiTextBox_PPSstrExpiryDateA.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrExpiryDateA.TabIndex = 140
        Me.HmiTextBox_PPSstrExpiryDateA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrExpiryDateA.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrBatchNoA
        '
        Me.HmiTextBox_PPSstrBatchNoA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrBatchNoA.Location = New System.Drawing.Point(545, 81)
        Me.HmiTextBox_PPSstrBatchNoA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrBatchNoA.Name = "HmiTextBox_PPSstrBatchNoA"
        Me.HmiTextBox_PPSstrBatchNoA.Number = 0
        Me.HmiTextBox_PPSstrBatchNoA.Size = New System.Drawing.Size(61, 18)
        Me.HmiTextBox_PPSstrBatchNoA.TabIndex = 141
        Me.HmiTextBox_PPSstrBatchNoA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrBatchNoA.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrSupplierNoA
        '
        Me.HmiTextBox_PPSstrSupplierNoA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrSupplierNoA.Location = New System.Drawing.Point(92, 101)
        Me.HmiTextBox_PPSstrSupplierNoA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrSupplierNoA.Name = "HmiTextBox_PPSstrSupplierNoA"
        Me.HmiTextBox_PPSstrSupplierNoA.Number = 0
        Me.HmiTextBox_PPSstrSupplierNoA.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrSupplierNoA.TabIndex = 142
        Me.HmiTextBox_PPSstrSupplierNoA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrSupplierNoA.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrPackagingNoA
        '
        Me.HmiTextBox_PPSstrPackagingNoA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrPackagingNoA.Location = New System.Drawing.Point(243, 101)
        Me.HmiTextBox_PPSstrPackagingNoA.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrPackagingNoA.Name = "HmiTextBox_PPSstrPackagingNoA"
        Me.HmiTextBox_PPSstrPackagingNoA.Number = 0
        Me.HmiTextBox_PPSstrPackagingNoA.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrPackagingNoA.TabIndex = 143
        Me.HmiTextBox_PPSstrPackagingNoA.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrPackagingNoA.ValueType = GetType(String)
        '
        'HmiLabel19
        '
        Me.HmiLabel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel19.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel19.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel19.Name = "HmiLabel19"
        Me.HmiLabel19.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel19.TabIndex = 144
        '
        'HmiLabel20
        '
        Me.HmiLabel20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel20.Location = New System.Drawing.Point(152, 121)
        Me.HmiLabel20.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel20.Name = "HmiLabel20"
        Me.HmiLabel20.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel20.TabIndex = 145
        '
        'HmiLabel21
        '
        Me.HmiLabel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel21.Location = New System.Drawing.Point(303, 121)
        Me.HmiLabel21.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel21.Name = "HmiLabel21"
        Me.HmiLabel21.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel21.TabIndex = 146
        '
        'HmiLabel22
        '
        Me.HmiLabel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel22.Location = New System.Drawing.Point(454, 121)
        Me.HmiLabel22.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel22.Name = "HmiLabel22"
        Me.HmiLabel22.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel22.TabIndex = 147
        '
        'HmiLabel23
        '
        Me.HmiLabel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel23.Location = New System.Drawing.Point(1, 141)
        Me.HmiLabel23.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel23.Name = "HmiLabel23"
        Me.HmiLabel23.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel23.TabIndex = 148
        '
        'HmiLabel24
        '
        Me.HmiLabel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel24.Location = New System.Drawing.Point(152, 141)
        Me.HmiLabel24.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel24.Name = "HmiLabel24"
        Me.HmiLabel24.Size = New System.Drawing.Size(89, 18)
        Me.HmiLabel24.TabIndex = 149
        '
        'HmiTextBox_PPSstrPartNoB
        '
        Me.HmiTextBox_PPSstrPartNoB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrPartNoB.Location = New System.Drawing.Point(92, 121)
        Me.HmiTextBox_PPSstrPartNoB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrPartNoB.Name = "HmiTextBox_PPSstrPartNoB"
        Me.HmiTextBox_PPSstrPartNoB.Number = 0
        Me.HmiTextBox_PPSstrPartNoB.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrPartNoB.TabIndex = 150
        Me.HmiTextBox_PPSstrPartNoB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrPartNoB.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrSupplierNoB
        '
        Me.HmiTextBox_PPSstrSupplierNoB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrSupplierNoB.Location = New System.Drawing.Point(92, 141)
        Me.HmiTextBox_PPSstrSupplierNoB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrSupplierNoB.Name = "HmiTextBox_PPSstrSupplierNoB"
        Me.HmiTextBox_PPSstrSupplierNoB.Number = 0
        Me.HmiTextBox_PPSstrSupplierNoB.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrSupplierNoB.TabIndex = 151
        Me.HmiTextBox_PPSstrSupplierNoB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrSupplierNoB.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrVolumeB
        '
        Me.HmiTextBox_PPSstrVolumeB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrVolumeB.Location = New System.Drawing.Point(243, 121)
        Me.HmiTextBox_PPSstrVolumeB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrVolumeB.Name = "HmiTextBox_PPSstrVolumeB"
        Me.HmiTextBox_PPSstrVolumeB.Number = 0
        Me.HmiTextBox_PPSstrVolumeB.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrVolumeB.TabIndex = 152
        Me.HmiTextBox_PPSstrVolumeB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrVolumeB.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrPackagingNoB
        '
        Me.HmiTextBox_PPSstrPackagingNoB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrPackagingNoB.Location = New System.Drawing.Point(243, 141)
        Me.HmiTextBox_PPSstrPackagingNoB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrPackagingNoB.Name = "HmiTextBox_PPSstrPackagingNoB"
        Me.HmiTextBox_PPSstrPackagingNoB.Number = 0
        Me.HmiTextBox_PPSstrPackagingNoB.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrPackagingNoB.TabIndex = 153
        Me.HmiTextBox_PPSstrPackagingNoB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrPackagingNoB.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrExpiryDateB
        '
        Me.HmiTextBox_PPSstrExpiryDateB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrExpiryDateB.Location = New System.Drawing.Point(394, 121)
        Me.HmiTextBox_PPSstrExpiryDateB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrExpiryDateB.Name = "HmiTextBox_PPSstrExpiryDateB"
        Me.HmiTextBox_PPSstrExpiryDateB.Number = 0
        Me.HmiTextBox_PPSstrExpiryDateB.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_PPSstrExpiryDateB.TabIndex = 154
        Me.HmiTextBox_PPSstrExpiryDateB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrExpiryDateB.ValueType = GetType(String)
        '
        'HmiTextBox_PPSstrBatchNoB
        '
        Me.HmiTextBox_PPSstrBatchNoB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PPSstrBatchNoB.Location = New System.Drawing.Point(545, 121)
        Me.HmiTextBox_PPSstrBatchNoB.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_PPSstrBatchNoB.Name = "HmiTextBox_PPSstrBatchNoB"
        Me.HmiTextBox_PPSstrBatchNoB.Number = 0
        Me.HmiTextBox_PPSstrBatchNoB.Size = New System.Drawing.Size(61, 18)
        Me.HmiTextBox_PPSstrBatchNoB.TabIndex = 155
        Me.HmiTextBox_PPSstrBatchNoB.TextBoxReadOnly = False
        Me.HmiTextBox_PPSstrBatchNoB.ValueType = GetType(String)
        '
        'HmiTextBox_MES_StatusA
        '
        Me.HmiTableLayoutPanel1.SetColumnSpan(Me.HmiTextBox_MES_StatusA, 2)
        Me.HmiTextBox_MES_StatusA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MES_StatusA.Location = New System.Drawing.Point(305, 184)
        Me.HmiTextBox_MES_StatusA.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.HmiTextBox_MES_StatusA.Name = "HmiTextBox_MES_StatusA"
        Me.HmiTextBox_MES_StatusA.Number = 0
        Me.HmiTextBox_MES_StatusA.Size = New System.Drawing.Size(145, 12)
        Me.HmiTextBox_MES_StatusA.TabIndex = 159
        Me.HmiTextBox_MES_StatusA.TextBoxReadOnly = False
        Me.HmiTextBox_MES_StatusA.ValueType = GetType(String)
        '
        'HmiTableLayoutPanel2
        '
        Me.HmiTableLayoutPanel2.ColumnCount = 8
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000HS_OPMode3, 4, 7)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000HS_OPMode2, 2, 7)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000HS_OPMode1, 0, 7)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000Material_OK, 6, 5)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000Cleaning, 4, 5)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel25, 0, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel26, 4, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel27, 2, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel28, 0, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000Ready, 1, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel29, 2, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel30, 4, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel32, 0, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel33, 2, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000actOP_Mode, 1, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000requestPostiton, 3, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel34, 0, 3)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000FillingLevel1, 1, 3)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000Start, 0, 5)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiButtonWithIndicate_B2000Filling, 2, 5)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel41, 0, 6)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000Busy, 3, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000System_OK, 5, 0)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000ProcessCycle_OK, 1, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000ProcessCycle_NOK, 3, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiSensor_B2000Handshake_active, 5, 1)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel31, 4, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel49, 6, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000actRecipe, 5, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000actUserLevel, 7, 2)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel50, 2, 3)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000FillingLevel2, 3, 3)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000Position, 1, 6)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel37, 2, 6)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000OP_Mode, 3, 6)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiLabel38, 4, 6)
        Me.HmiTableLayoutPanel2.Controls.Add(Me.HmiTextBox_B2000RecipeNumber, 5, 6)
        Me.HmiTableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel2.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel2.Name = "HmiTableLayoutPanel2"
        Me.HmiTableLayoutPanel2.RowCount = 10
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.HmiTableLayoutPanel2.Size = New System.Drawing.Size(603, 199)
        Me.HmiTableLayoutPanel2.TabIndex = 1
        '
        'HmiButtonWithIndicate_B2000HS_OPMode3
        '
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Location = New System.Drawing.Point(301, 141)
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Name = "HmiButtonWithIndicate_B2000HS_OPMode3"
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.TabIndex = 177
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000HS_OPMode3.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_B2000HS_OPMode2
        '
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Location = New System.Drawing.Point(151, 141)
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Name = "HmiButtonWithIndicate_B2000HS_OPMode2"
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.TabIndex = 176
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000HS_OPMode2.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_B2000HS_OPMode1
        '
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Location = New System.Drawing.Point(1, 141)
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Name = "HmiButtonWithIndicate_B2000HS_OPMode1"
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.TabIndex = 175
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000HS_OPMode1.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_B2000Material_OK
        '
        Me.HmiButtonWithIndicate_B2000Material_OK.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000Material_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000Material_OK.Location = New System.Drawing.Point(451, 101)
        Me.HmiButtonWithIndicate_B2000Material_OK.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000Material_OK.Name = "HmiButtonWithIndicate_B2000Material_OK"
        Me.HmiButtonWithIndicate_B2000Material_OK.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000Material_OK.TabIndex = 169
        Me.HmiButtonWithIndicate_B2000Material_OK.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000Material_OK.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_B2000Cleaning
        '
        Me.HmiButtonWithIndicate_B2000Cleaning.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000Cleaning.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000Cleaning.Location = New System.Drawing.Point(301, 101)
        Me.HmiButtonWithIndicate_B2000Cleaning.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000Cleaning.Name = "HmiButtonWithIndicate_B2000Cleaning"
        Me.HmiButtonWithIndicate_B2000Cleaning.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000Cleaning.TabIndex = 168
        Me.HmiButtonWithIndicate_B2000Cleaning.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000Cleaning.UseVisualStyleBackColor = True
        '
        'HmiLabel25
        '
        Me.HmiLabel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel25.Location = New System.Drawing.Point(1, 21)
        Me.HmiLabel25.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel25.Name = "HmiLabel25"
        Me.HmiLabel25.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel25.TabIndex = 59
        '
        'HmiLabel26
        '
        Me.HmiLabel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel26.Location = New System.Drawing.Point(301, 1)
        Me.HmiLabel26.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel26.Name = "HmiLabel26"
        Me.HmiLabel26.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel26.TabIndex = 55
        '
        'HmiLabel27
        '
        Me.HmiLabel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel27.Location = New System.Drawing.Point(151, 1)
        Me.HmiLabel27.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel27.Name = "HmiLabel27"
        Me.HmiLabel27.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel27.TabIndex = 54
        '
        'HmiLabel28
        '
        Me.HmiLabel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel28.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel28.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel28.Name = "HmiLabel28"
        Me.HmiLabel28.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel28.TabIndex = 52
        '
        'HmiSensor_B2000Ready
        '
        Me.HmiSensor_B2000Ready.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000Ready.Location = New System.Drawing.Point(93, 3)
        Me.HmiSensor_B2000Ready.Name = "HmiSensor_B2000Ready"
        Me.HmiSensor_B2000Ready.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000Ready.TabIndex = 110
        '
        'HmiLabel29
        '
        Me.HmiLabel29.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel29.Location = New System.Drawing.Point(151, 21)
        Me.HmiLabel29.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel29.Name = "HmiLabel29"
        Me.HmiLabel29.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel29.TabIndex = 113
        '
        'HmiLabel30
        '
        Me.HmiLabel30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel30.Location = New System.Drawing.Point(301, 21)
        Me.HmiLabel30.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel30.Name = "HmiLabel30"
        Me.HmiLabel30.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel30.TabIndex = 114
        '
        'HmiLabel32
        '
        Me.HmiLabel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel32.Location = New System.Drawing.Point(1, 41)
        Me.HmiLabel32.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel32.Name = "HmiLabel32"
        Me.HmiLabel32.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel32.TabIndex = 120
        '
        'HmiLabel33
        '
        Me.HmiLabel33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel33.Location = New System.Drawing.Point(151, 41)
        Me.HmiLabel33.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel33.Name = "HmiLabel33"
        Me.HmiLabel33.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel33.TabIndex = 121
        '
        'HmiTextBox_B2000actOP_Mode
        '
        Me.HmiTextBox_B2000actOP_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000actOP_Mode.Location = New System.Drawing.Point(91, 41)
        Me.HmiTextBox_B2000actOP_Mode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000actOP_Mode.Name = "HmiTextBox_B2000actOP_Mode"
        Me.HmiTextBox_B2000actOP_Mode.Number = 0
        Me.HmiTextBox_B2000actOP_Mode.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000actOP_Mode.TabIndex = 122
        Me.HmiTextBox_B2000actOP_Mode.TextBoxReadOnly = False
        Me.HmiTextBox_B2000actOP_Mode.ValueType = GetType(String)
        '
        'HmiTextBox_B2000requestPostiton
        '
        Me.HmiTextBox_B2000requestPostiton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000requestPostiton.Location = New System.Drawing.Point(241, 41)
        Me.HmiTextBox_B2000requestPostiton.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000requestPostiton.Name = "HmiTextBox_B2000requestPostiton"
        Me.HmiTextBox_B2000requestPostiton.Number = 0
        Me.HmiTextBox_B2000requestPostiton.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000requestPostiton.TabIndex = 123
        Me.HmiTextBox_B2000requestPostiton.TextBoxReadOnly = False
        Me.HmiTextBox_B2000requestPostiton.ValueType = GetType(String)
        '
        'HmiLabel34
        '
        Me.HmiLabel34.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel34.Location = New System.Drawing.Point(1, 61)
        Me.HmiLabel34.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel34.Name = "HmiLabel34"
        Me.HmiLabel34.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel34.TabIndex = 124
        '
        'HmiTextBox_B2000FillingLevel1
        '
        Me.HmiTextBox_B2000FillingLevel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000FillingLevel1.Location = New System.Drawing.Point(91, 61)
        Me.HmiTextBox_B2000FillingLevel1.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000FillingLevel1.Name = "HmiTextBox_B2000FillingLevel1"
        Me.HmiTextBox_B2000FillingLevel1.Number = 0
        Me.HmiTextBox_B2000FillingLevel1.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000FillingLevel1.TabIndex = 125
        Me.HmiTextBox_B2000FillingLevel1.TextBoxReadOnly = False
        Me.HmiTextBox_B2000FillingLevel1.ValueType = GetType(String)
        '
        'HmiButtonWithIndicate_B2000Start
        '
        Me.HmiButtonWithIndicate_B2000Start.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000Start.Location = New System.Drawing.Point(1, 101)
        Me.HmiButtonWithIndicate_B2000Start.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000Start.Name = "HmiButtonWithIndicate_B2000Start"
        Me.HmiButtonWithIndicate_B2000Start.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000Start.TabIndex = 130
        Me.HmiButtonWithIndicate_B2000Start.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000Start.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_B2000Filling
        '
        Me.HmiButtonWithIndicate_B2000Filling.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_B2000Filling.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_B2000Filling.Location = New System.Drawing.Point(151, 101)
        Me.HmiButtonWithIndicate_B2000Filling.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButtonWithIndicate_B2000Filling.Name = "HmiButtonWithIndicate_B2000Filling"
        Me.HmiButtonWithIndicate_B2000Filling.Size = New System.Drawing.Size(88, 18)
        Me.HmiButtonWithIndicate_B2000Filling.TabIndex = 131
        Me.HmiButtonWithIndicate_B2000Filling.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_B2000Filling.UseVisualStyleBackColor = True
        '
        'HmiLabel41
        '
        Me.HmiLabel41.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel41.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel41.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel41.Name = "HmiLabel41"
        Me.HmiLabel41.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel41.TabIndex = 136
        '
        'HmiSensor_B2000Busy
        '
        Me.HmiSensor_B2000Busy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000Busy.Location = New System.Drawing.Point(243, 3)
        Me.HmiSensor_B2000Busy.Name = "HmiSensor_B2000Busy"
        Me.HmiSensor_B2000Busy.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000Busy.TabIndex = 156
        '
        'HmiSensor_B2000System_OK
        '
        Me.HmiSensor_B2000System_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000System_OK.Location = New System.Drawing.Point(393, 3)
        Me.HmiSensor_B2000System_OK.Name = "HmiSensor_B2000System_OK"
        Me.HmiSensor_B2000System_OK.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000System_OK.TabIndex = 157
        '
        'HmiSensor_B2000ProcessCycle_OK
        '
        Me.HmiSensor_B2000ProcessCycle_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000ProcessCycle_OK.Location = New System.Drawing.Point(93, 23)
        Me.HmiSensor_B2000ProcessCycle_OK.Name = "HmiSensor_B2000ProcessCycle_OK"
        Me.HmiSensor_B2000ProcessCycle_OK.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000ProcessCycle_OK.TabIndex = 159
        '
        'HmiSensor_B2000ProcessCycle_NOK
        '
        Me.HmiSensor_B2000ProcessCycle_NOK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000ProcessCycle_NOK.Location = New System.Drawing.Point(243, 23)
        Me.HmiSensor_B2000ProcessCycle_NOK.Name = "HmiSensor_B2000ProcessCycle_NOK"
        Me.HmiSensor_B2000ProcessCycle_NOK.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000ProcessCycle_NOK.TabIndex = 160
        '
        'HmiSensor_B2000Handshake_active
        '
        Me.HmiSensor_B2000Handshake_active.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_B2000Handshake_active.Location = New System.Drawing.Point(393, 23)
        Me.HmiSensor_B2000Handshake_active.Name = "HmiSensor_B2000Handshake_active"
        Me.HmiSensor_B2000Handshake_active.Size = New System.Drawing.Size(54, 14)
        Me.HmiSensor_B2000Handshake_active.TabIndex = 161
        '
        'HmiLabel31
        '
        Me.HmiLabel31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel31.Location = New System.Drawing.Point(301, 41)
        Me.HmiLabel31.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel31.Name = "HmiLabel31"
        Me.HmiLabel31.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel31.TabIndex = 162
        '
        'HmiLabel49
        '
        Me.HmiLabel49.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel49.Location = New System.Drawing.Point(451, 41)
        Me.HmiLabel49.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel49.Name = "HmiLabel49"
        Me.HmiLabel49.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel49.TabIndex = 163
        '
        'HmiTextBox_B2000actRecipe
        '
        Me.HmiTextBox_B2000actRecipe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000actRecipe.Location = New System.Drawing.Point(391, 41)
        Me.HmiTextBox_B2000actRecipe.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000actRecipe.Name = "HmiTextBox_B2000actRecipe"
        Me.HmiTextBox_B2000actRecipe.Number = 0
        Me.HmiTextBox_B2000actRecipe.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000actRecipe.TabIndex = 164
        Me.HmiTextBox_B2000actRecipe.TextBoxReadOnly = False
        Me.HmiTextBox_B2000actRecipe.ValueType = GetType(String)
        '
        'HmiTextBox_B2000actUserLevel
        '
        Me.HmiTextBox_B2000actUserLevel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000actUserLevel.Location = New System.Drawing.Point(541, 41)
        Me.HmiTextBox_B2000actUserLevel.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000actUserLevel.Name = "HmiTextBox_B2000actUserLevel"
        Me.HmiTextBox_B2000actUserLevel.Number = 0
        Me.HmiTextBox_B2000actUserLevel.Size = New System.Drawing.Size(61, 18)
        Me.HmiTextBox_B2000actUserLevel.TabIndex = 165
        Me.HmiTextBox_B2000actUserLevel.TextBoxReadOnly = False
        Me.HmiTextBox_B2000actUserLevel.ValueType = GetType(String)
        '
        'HmiLabel50
        '
        Me.HmiLabel50.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel50.Location = New System.Drawing.Point(151, 61)
        Me.HmiLabel50.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel50.Name = "HmiLabel50"
        Me.HmiLabel50.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel50.TabIndex = 166
        '
        'HmiTextBox_B2000FillingLevel2
        '
        Me.HmiTextBox_B2000FillingLevel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000FillingLevel2.Location = New System.Drawing.Point(241, 61)
        Me.HmiTextBox_B2000FillingLevel2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000FillingLevel2.Name = "HmiTextBox_B2000FillingLevel2"
        Me.HmiTextBox_B2000FillingLevel2.Number = 0
        Me.HmiTextBox_B2000FillingLevel2.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000FillingLevel2.TabIndex = 167
        Me.HmiTextBox_B2000FillingLevel2.TextBoxReadOnly = False
        Me.HmiTextBox_B2000FillingLevel2.ValueType = GetType(String)
        '
        'HmiTextBox_B2000Position
        '
        Me.HmiTextBox_B2000Position.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000Position.Location = New System.Drawing.Point(91, 121)
        Me.HmiTextBox_B2000Position.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000Position.Name = "HmiTextBox_B2000Position"
        Me.HmiTextBox_B2000Position.Number = 0
        Me.HmiTextBox_B2000Position.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000Position.TabIndex = 170
        Me.HmiTextBox_B2000Position.TextBoxReadOnly = False
        Me.HmiTextBox_B2000Position.ValueType = GetType(String)
        '
        'HmiLabel37
        '
        Me.HmiLabel37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel37.Location = New System.Drawing.Point(151, 121)
        Me.HmiLabel37.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel37.Name = "HmiLabel37"
        Me.HmiLabel37.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel37.TabIndex = 171
        '
        'HmiTextBox_B2000OP_Mode
        '
        Me.HmiTextBox_B2000OP_Mode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000OP_Mode.Location = New System.Drawing.Point(241, 121)
        Me.HmiTextBox_B2000OP_Mode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000OP_Mode.Name = "HmiTextBox_B2000OP_Mode"
        Me.HmiTextBox_B2000OP_Mode.Number = 0
        Me.HmiTextBox_B2000OP_Mode.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000OP_Mode.TabIndex = 172
        Me.HmiTextBox_B2000OP_Mode.TextBoxReadOnly = False
        Me.HmiTextBox_B2000OP_Mode.ValueType = GetType(String)
        '
        'HmiLabel38
        '
        Me.HmiLabel38.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel38.Location = New System.Drawing.Point(301, 121)
        Me.HmiLabel38.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel38.Name = "HmiLabel38"
        Me.HmiLabel38.Size = New System.Drawing.Size(88, 18)
        Me.HmiLabel38.TabIndex = 173
        '
        'HmiTextBox_B2000RecipeNumber
        '
        Me.HmiTextBox_B2000RecipeNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_B2000RecipeNumber.Location = New System.Drawing.Point(391, 121)
        Me.HmiTextBox_B2000RecipeNumber.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTextBox_B2000RecipeNumber.Name = "HmiTextBox_B2000RecipeNumber"
        Me.HmiTextBox_B2000RecipeNumber.Number = 0
        Me.HmiTextBox_B2000RecipeNumber.Size = New System.Drawing.Size(58, 18)
        Me.HmiTextBox_B2000RecipeNumber.TabIndex = 174
        Me.HmiTextBox_B2000RecipeNumber.TextBoxReadOnly = False
        Me.HmiTextBox_B2000RecipeNumber.ValueType = GetType(String)
        '
        'HmiTextBox_MES_StatusB
        '
        Me.HmiTableLayoutPanel1.SetColumnSpan(Me.HmiTextBox_MES_StatusB, 2)
        Me.HmiTextBox_MES_StatusB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MES_StatusB.Location = New System.Drawing.Point(456, 185)
        Me.HmiTextBox_MES_StatusB.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.HmiTextBox_MES_StatusB.Name = "HmiTextBox_MES_StatusB"
        Me.HmiTextBox_MES_StatusB.Number = 0
        Me.HmiTextBox_MES_StatusB.Size = New System.Drawing.Size(148, 10)
        Me.HmiTextBox_MES_StatusB.TabIndex = 160
        Me.HmiTextBox_MES_StatusB.TextBoxReadOnly = False
        Me.HmiTextBox_MES_StatusB.ValueType = GetType(String)
        '
        'BDTronic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 498)
        Me.Controls.Add(Me.Panel_UI)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "BDTronic"
        Me.Text = "Parameter"
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_PPS.ResumeLayout(False)
        Me.GroupBox_B2000.ResumeLayout(False)
        Me.HmiTableLayoutPanel1.ResumeLayout(False)
        Me.HmiTableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_PPS As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel1 As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel8 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel4 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel3 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel1 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_PPS_Active As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiTextBox_PPSactOP_Mode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSactUser_Level As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel5 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel6 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PPSFillingLevelP1 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSFillingLevelP2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSSupplyPressureP1 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSSupplyPressureP2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel7 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel9 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PPSPressureP1Outlet As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSPressureP2Outlet As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel10 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PPSOP_Mode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel11 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel12 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_PPSscanProcessReadyMESA As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_PPSscanProcessReadyMESB As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiButtonWithIndicate_PPSHS_MESokA As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_PPSHS_MESokB As HMIButtonWithIndicate
    Friend WithEvents HmiLabel13 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel14 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel15 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel16 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel17 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel18 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PPSstrPartNoA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrVolumeA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrExpiryDateA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrBatchNoA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrSupplierNoA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrPackagingNoA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel19 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel20 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel21 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel22 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel23 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel24 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PPSstrPartNoB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrSupplierNoB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrVolumeB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrPackagingNoB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrExpiryDateB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PPSstrBatchNoB As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents GroupBox_B2000 As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel2 As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel25 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel26 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel27 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel28 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_B2000Ready As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel29 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel30 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel32 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel33 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000actOP_Mode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_B2000requestPostiton As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel34 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000FillingLevel1 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_B2000Start As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_B2000Filling As HMIButtonWithIndicate
    Friend WithEvents HmiLabel41 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_B2000Busy As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_B2000System_OK As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_B2000ProcessCycle_OK As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_B2000ProcessCycle_NOK As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiSensor_B2000Handshake_active As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel31 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel49 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000actRecipe As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_B2000actUserLevel As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_B2000Cleaning As HMIButtonWithIndicate
    Friend WithEvents HmiLabel50 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000FillingLevel2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_B2000Material_OK As HMIButtonWithIndicate
    Friend WithEvents HmiTextBox_B2000Position As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel37 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000OP_Mode As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel38 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_B2000RecipeNumber As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_PPSHS_OPMode3 As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_PPSHS_OPMode2 As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_PPSHS_OPMode1 As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_B2000HS_OPMode3 As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_B2000HS_OPMode2 As HMIButtonWithIndicate
    Friend WithEvents HmiButtonWithIndicate_B2000HS_OPMode1 As HMIButtonWithIndicate
    Friend WithEvents HmiTextBox_MES_StatusA As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MES_StatusB As Kochi.HMI.MainControl.UI.HMITextBox
End Class
