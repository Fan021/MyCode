<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WeightUI
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WeightUI))
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Top = New System.Windows.Forms.Panel()
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Top = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_AutoWeight = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_AutoWeight = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_AutoWeightNo = New System.Windows.Forms.Label()
        Me.Label3_AutoWeight = New System.Windows.Forms.Label()
        Me.CheckBox_AutoWeight = New System.Windows.Forms.CheckBox()
        Me.GroupBox_Weight = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Weight = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Weight = New System.Windows.Forms.Label()
        Me.GroupBox_Cup = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Cup = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Cup = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Function = New System.Windows.Forms.TableLayoutPanel()
        Me.Label_Active = New System.Windows.Forms.Label()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Mid_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Mid_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Mid_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Weight_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Data = New System.Windows.Forms.Button()
        Me.Button_Back = New System.Windows.Forms.Button()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.HmiTextBox_AutoWeightNo = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiSensor_Active = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiButtonWithIndicate_Start = New HMIButtonWithIndicate(Me.components)
        Me.Panel_Indicate_Cup = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTableLayoutPanel_Report = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_LastWeight = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_CPK = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_CP = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Std = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_MinWeight = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Weight2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_MaxWeight = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_InCycle = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_Result = New System.Windows.Forms.Label()
        Me.Label_PerCycle2 = New System.Windows.Forms.Label()
        Me.Label_MaxWeight = New System.Windows.Forms.Label()
        Me.Label_InCycle = New System.Windows.Forms.Label()
        Me.Label_Weight2 = New System.Windows.Forms.Label()
        Me.Label_MinWeight = New System.Windows.Forms.Label()
        Me.Label_Std = New System.Windows.Forms.Label()
        Me.Label_CP = New System.Windows.Forms.Label()
        Me.Label_CPK = New System.Windows.Forms.Label()
        Me.Label_Name = New System.Windows.Forms.Label()
        Me.HmiTextBox_PerCycle2 = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_LastWeight = New System.Windows.Forms.Label()
        Me.Panel_Result = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiTableLayoutPanel_Parameter = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_DispensingPause = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_DispensingTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PerCycle = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Pause = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PreShotTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_PreShot = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Min = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Label_DispensingPause = New System.Windows.Forms.Label()
        Me.Label_Max = New System.Windows.Forms.Label()
        Me.Label_PreShot = New System.Windows.Forms.Label()
        Me.Label_PreShotTime = New System.Windows.Forms.Label()
        Me.Label_Pause = New System.Windows.Forms.Label()
        Me.Label_PerCycle = New System.Windows.Forms.Label()
        Me.Label_Min = New System.Windows.Forms.Label()
        Me.Label_DispensingTime = New System.Windows.Forms.Label()
        Me.Label_Title = New System.Windows.Forms.Label()
        Me.HmiTextBox_Max = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Top.SuspendLayout()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body_Top.SuspendLayout()
        Me.GroupBox_AutoWeight.SuspendLayout()
        Me.TableLayoutPanel_AutoWeight.SuspendLayout()
        Me.GroupBox_Weight.SuspendLayout()
        Me.TableLayoutPanel_Weight.SuspendLayout()
        Me.GroupBox_Cup.SuspendLayout()
        Me.TableLayoutPanel_Cup.SuspendLayout()
        Me.TableLayoutPanel_Function.SuspendLayout()
        Me.TableLayoutPanel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Mid_Right.SuspendLayout()
        Me.TableLayoutPanel_Mid_Left.SuspendLayout()
        Me.TableLayoutPanel_Mid_Mid.SuspendLayout()
        CType(Me.Chart_Weight_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.Panel_Body.SuspendLayout()
        Me.HmiTableLayoutPanel_Report.SuspendLayout()
        Me.HmiTableLayoutPanel_Parameter.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Top, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(1)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(615, 498)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Panel_Top
        '
        Me.Panel_Top.Controls.Add(Me.Panel_UI)
        Me.Panel_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Top.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Top.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Top.Name = "Panel_Top"
        Me.Panel_Top.Size = New System.Drawing.Size(615, 448)
        Me.Panel_Top.TabIndex = 0
        '
        'Panel_UI
        '
        Me.Panel_UI.Controls.Add(Me.TableLayoutPanel_Body_Top)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(615, 448)
        Me.Panel_UI.TabIndex = 0
        '
        'TableLayoutPanel_Body_Top
        '
        Me.TableLayoutPanel_Body_Top.ColumnCount = 3
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Top.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.GroupBox_AutoWeight, 2, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.GroupBox_Weight, 1, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.GroupBox_Cup, 0, 0)
        Me.TableLayoutPanel_Body_Top.Controls.Add(Me.TableLayoutPanel_Mid, 0, 1)
        Me.TableLayoutPanel_Body_Top.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Top.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Top.Name = "TableLayoutPanel_Body_Top"
        Me.TableLayoutPanel_Body_Top.RowCount = 2
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Top.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Top.Size = New System.Drawing.Size(615, 448)
        Me.TableLayoutPanel_Body_Top.TabIndex = 0
        '
        'GroupBox_AutoWeight
        '
        Me.GroupBox_AutoWeight.Controls.Add(Me.TableLayoutPanel_AutoWeight)
        Me.GroupBox_AutoWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_AutoWeight.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_AutoWeight.Location = New System.Drawing.Point(413, 3)
        Me.GroupBox_AutoWeight.Name = "GroupBox_AutoWeight"
        Me.GroupBox_AutoWeight.Size = New System.Drawing.Size(199, 83)
        Me.GroupBox_AutoWeight.TabIndex = 2
        Me.GroupBox_AutoWeight.TabStop = False
        Me.GroupBox_AutoWeight.Text = "GroupBox3"
        '
        'TableLayoutPanel_AutoWeight
        '
        Me.TableLayoutPanel_AutoWeight.ColumnCount = 2
        Me.TableLayoutPanel_AutoWeight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.02073!))
        Me.TableLayoutPanel_AutoWeight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.97927!))
        Me.TableLayoutPanel_AutoWeight.Controls.Add(Me.Label_AutoWeightNo, 0, 1)
        Me.TableLayoutPanel_AutoWeight.Controls.Add(Me.Label3_AutoWeight, 0, 0)
        Me.TableLayoutPanel_AutoWeight.Controls.Add(Me.CheckBox_AutoWeight, 1, 0)
        Me.TableLayoutPanel_AutoWeight.Controls.Add(Me.HmiTextBox_AutoWeightNo, 1, 1)
        Me.TableLayoutPanel_AutoWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_AutoWeight.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_AutoWeight.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_AutoWeight.Name = "TableLayoutPanel_AutoWeight"
        Me.TableLayoutPanel_AutoWeight.RowCount = 2
        Me.TableLayoutPanel_AutoWeight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_AutoWeight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_AutoWeight.Size = New System.Drawing.Size(193, 60)
        Me.TableLayoutPanel_AutoWeight.TabIndex = 0
        '
        'Label_AutoWeightNo
        '
        Me.Label_AutoWeightNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_AutoWeightNo.Location = New System.Drawing.Point(3, 33)
        Me.Label_AutoWeightNo.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_AutoWeightNo.Name = "Label_AutoWeightNo"
        Me.Label_AutoWeightNo.Size = New System.Drawing.Size(133, 24)
        Me.Label_AutoWeightNo.TabIndex = 2
        Me.Label_AutoWeightNo.Text = "Label4"
        Me.Label_AutoWeightNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3_AutoWeight
        '
        Me.Label3_AutoWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3_AutoWeight.Location = New System.Drawing.Point(3, 3)
        Me.Label3_AutoWeight.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3_AutoWeight.Name = "Label3_AutoWeight"
        Me.Label3_AutoWeight.Size = New System.Drawing.Size(133, 24)
        Me.Label3_AutoWeight.TabIndex = 1
        Me.Label3_AutoWeight.Text = "Label3"
        Me.Label3_AutoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CheckBox_AutoWeight
        '
        Me.CheckBox_AutoWeight.AutoSize = True
        Me.CheckBox_AutoWeight.Checked = True
        Me.CheckBox_AutoWeight.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox_AutoWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckBox_AutoWeight.Location = New System.Drawing.Point(142, 3)
        Me.CheckBox_AutoWeight.Name = "CheckBox_AutoWeight"
        Me.CheckBox_AutoWeight.Size = New System.Drawing.Size(48, 24)
        Me.CheckBox_AutoWeight.TabIndex = 3
        Me.CheckBox_AutoWeight.UseVisualStyleBackColor = True
        '
        'GroupBox_Weight
        '
        Me.GroupBox_Weight.Controls.Add(Me.TableLayoutPanel_Weight)
        Me.GroupBox_Weight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Weight.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Weight.Location = New System.Drawing.Point(208, 3)
        Me.GroupBox_Weight.Name = "GroupBox_Weight"
        Me.GroupBox_Weight.Size = New System.Drawing.Size(199, 83)
        Me.GroupBox_Weight.TabIndex = 1
        Me.GroupBox_Weight.TabStop = False
        Me.GroupBox_Weight.Text = "GroupBox2"
        '
        'TableLayoutPanel_Weight
        '
        Me.TableLayoutPanel_Weight.ColumnCount = 2
        Me.TableLayoutPanel_Weight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.1282!))
        Me.TableLayoutPanel_Weight.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.8718!))
        Me.TableLayoutPanel_Weight.Controls.Add(Me.Label_Weight, 0, 0)
        Me.TableLayoutPanel_Weight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Weight.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_Weight.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Weight.Name = "TableLayoutPanel_Weight"
        Me.TableLayoutPanel_Weight.RowCount = 2
        Me.TableLayoutPanel_Weight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Weight.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Weight.Size = New System.Drawing.Size(193, 60)
        Me.TableLayoutPanel_Weight.TabIndex = 1
        '
        'Label_Weight
        '
        Me.TableLayoutPanel_Weight.SetColumnSpan(Me.Label_Weight, 2)
        Me.Label_Weight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Weight.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Weight.Location = New System.Drawing.Point(3, 3)
        Me.Label_Weight.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Weight.Name = "Label_Weight"
        Me.Label_Weight.Size = New System.Drawing.Size(187, 24)
        Me.Label_Weight.TabIndex = 1
        Me.Label_Weight.Text = "0.000"
        Me.Label_Weight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox_Cup
        '
        Me.GroupBox_Cup.Controls.Add(Me.TableLayoutPanel_Cup)
        Me.GroupBox_Cup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Cup.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Cup.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Cup.Name = "GroupBox_Cup"
        Me.GroupBox_Cup.Size = New System.Drawing.Size(199, 83)
        Me.GroupBox_Cup.TabIndex = 0
        Me.GroupBox_Cup.TabStop = False
        Me.GroupBox_Cup.Text = "GroupBox1"
        '
        'TableLayoutPanel_Cup
        '
        Me.TableLayoutPanel_Cup.ColumnCount = 2
        Me.TableLayoutPanel_Cup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.09845!))
        Me.TableLayoutPanel_Cup.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.90156!))
        Me.TableLayoutPanel_Cup.Controls.Add(Me.Label_Cup, 1, 0)
        Me.TableLayoutPanel_Cup.Controls.Add(Me.TableLayoutPanel_Function, 0, 1)
        Me.TableLayoutPanel_Cup.Controls.Add(Me.Panel_Indicate_Cup, 0, 0)
        Me.TableLayoutPanel_Cup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Cup.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_Cup.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Cup.Name = "TableLayoutPanel_Cup"
        Me.TableLayoutPanel_Cup.RowCount = 2
        Me.TableLayoutPanel_Cup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Cup.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Cup.Size = New System.Drawing.Size(193, 60)
        Me.TableLayoutPanel_Cup.TabIndex = 1
        '
        'Label_Cup
        '
        Me.Label_Cup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Cup.Location = New System.Drawing.Point(36, 3)
        Me.Label_Cup.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Cup.Name = "Label_Cup"
        Me.Label_Cup.Size = New System.Drawing.Size(154, 24)
        Me.Label_Cup.TabIndex = 0
        Me.Label_Cup.Text = "Label1"
        Me.Label_Cup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel_Function
        '
        Me.TableLayoutPanel_Function.ColumnCount = 3
        Me.TableLayoutPanel_Cup.SetColumnSpan(Me.TableLayoutPanel_Function, 2)
        Me.TableLayoutPanel_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.70177!))
        Me.TableLayoutPanel_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.54367!))
        Me.TableLayoutPanel_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.75456!))
        Me.TableLayoutPanel_Function.Controls.Add(Me.Label_Active, 0, 0)
        Me.TableLayoutPanel_Function.Controls.Add(Me.HmiSensor_Active, 0, 0)
        Me.TableLayoutPanel_Function.Controls.Add(Me.HmiButtonWithIndicate_Start, 2, 0)
        Me.TableLayoutPanel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Function.Location = New System.Drawing.Point(0, 30)
        Me.TableLayoutPanel_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Function.Name = "TableLayoutPanel_Function"
        Me.TableLayoutPanel_Function.RowCount = 1
        Me.TableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Function.Size = New System.Drawing.Size(193, 30)
        Me.TableLayoutPanel_Function.TabIndex = 4
        '
        'Label_Active
        '
        Me.Label_Active.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Active.Location = New System.Drawing.Point(37, 3)
        Me.Label_Active.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Active.Name = "Label_Active"
        Me.Label_Active.Size = New System.Drawing.Size(52, 24)
        Me.Label_Active.TabIndex = 7
        Me.Label_Active.Text = "Label1"
        Me.Label_Active.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 3
        Me.TableLayoutPanel_Body_Top.SetColumnSpan(Me.TableLayoutPanel_Mid, 3)
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Mid_Right, 2, 0)
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Mid_Left, 0, 0)
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Mid_Mid, 1, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 89)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(615, 359)
        Me.TableLayoutPanel_Mid.TabIndex = 3
        '
        'TableLayoutPanel_Mid_Right
        '
        Me.TableLayoutPanel_Mid_Right.ColumnCount = 1
        Me.TableLayoutPanel_Mid_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid_Right.Controls.Add(Me.HmiTableLayoutPanel_Report, 0, 1)
        Me.TableLayoutPanel_Mid_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid_Right.Location = New System.Drawing.Point(460, 0)
        Me.TableLayoutPanel_Mid_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid_Right.Name = "TableLayoutPanel_Mid_Right"
        Me.TableLayoutPanel_Mid_Right.RowCount = 3
        Me.TableLayoutPanel_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Right.Size = New System.Drawing.Size(155, 359)
        Me.TableLayoutPanel_Mid_Right.TabIndex = 2
        '
        'TableLayoutPanel_Mid_Left
        '
        Me.TableLayoutPanel_Mid_Left.ColumnCount = 1
        Me.TableLayoutPanel_Mid_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid_Left.Controls.Add(Me.HmiTableLayoutPanel_Parameter, 0, 1)
        Me.TableLayoutPanel_Mid_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid_Left.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Mid_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid_Left.Name = "TableLayoutPanel_Mid_Left"
        Me.TableLayoutPanel_Mid_Left.RowCount = 3
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Left.Size = New System.Drawing.Size(184, 359)
        Me.TableLayoutPanel_Mid_Left.TabIndex = 1
        '
        'TableLayoutPanel_Mid_Mid
        '
        Me.TableLayoutPanel_Mid_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Mid_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid_Mid.Controls.Add(Me.Chart_Weight_Value, 0, 1)
        Me.TableLayoutPanel_Mid_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid_Mid.Location = New System.Drawing.Point(184, 0)
        Me.TableLayoutPanel_Mid_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid_Mid.Name = "TableLayoutPanel_Mid_Mid"
        Me.TableLayoutPanel_Mid_Mid.RowCount = 3
        Me.TableLayoutPanel_Mid_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Mid_Mid.Size = New System.Drawing.Size(276, 359)
        Me.TableLayoutPanel_Mid_Mid.TabIndex = 0
        '
        'Chart_Weight_Value
        '
        ChartArea1.Name = "ChartArea1"
        Me.Chart_Weight_Value.ChartAreas.Add(ChartArea1)
        Me.Chart_Weight_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Enabled = False
        Legend1.Name = "Legend1"
        Me.Chart_Weight_Value.Legends.Add(Legend1)
        Me.Chart_Weight_Value.Location = New System.Drawing.Point(3, 38)
        Me.Chart_Weight_Value.Name = "Chart_Weight_Value"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart_Weight_Value.Series.Add(Series1)
        Me.Chart_Weight_Value.Size = New System.Drawing.Size(270, 281)
        Me.Chart_Weight_Value.TabIndex = 0
        Me.Chart_Weight_Value.Text = "Chart1"
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 4
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Data, 2, 0)
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Back, 3, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 448)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(615, 50)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 1
        '
        'Button_Data
        '
        Me.Button_Data.BackgroundImage = CType(resources.GetObject("Button_Data.BackgroundImage"), System.Drawing.Image)
        Me.Button_Data.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Data.FlatAppearance.BorderSize = 0
        Me.Button_Data.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Data.Location = New System.Drawing.Point(495, 3)
        Me.Button_Data.Name = "Button_Data"
        Me.Button_Data.Size = New System.Drawing.Size(55, 44)
        Me.Button_Data.TabIndex = 0
        Me.Button_Data.UseVisualStyleBackColor = True
        '
        'Button_Back
        '
        Me.Button_Back.BackgroundImage = CType(resources.GetObject("Button_Back.BackgroundImage"), System.Drawing.Image)
        Me.Button_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button_Back.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Back.FlatAppearance.BorderSize = 0
        Me.Button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button_Back.Location = New System.Drawing.Point(556, 3)
        Me.Button_Back.Name = "Button_Back"
        Me.Button_Back.Size = New System.Drawing.Size(56, 44)
        Me.Button_Back.TabIndex = 1
        Me.Button_Back.UseVisualStyleBackColor = True
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(615, 498)
        Me.Panel_Body.TabIndex = 0
        '
        'HmiTextBox_AutoWeightNo
        '
        Me.HmiTextBox_AutoWeightNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AutoWeightNo.Location = New System.Drawing.Point(142, 33)
        Me.HmiTextBox_AutoWeightNo.Name = "HmiTextBox_AutoWeightNo"
        Me.HmiTextBox_AutoWeightNo.Number = 0
        Me.HmiTextBox_AutoWeightNo.Size = New System.Drawing.Size(48, 24)
        Me.HmiTextBox_AutoWeightNo.TabIndex = 4
        Me.HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
        Me.HmiTextBox_AutoWeightNo.ValueType = GetType(String)
        '
        'HmiSensor_Active
        '
        Me.HmiSensor_Active.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Active.Location = New System.Drawing.Point(3, 5)
        Me.HmiSensor_Active.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.HmiSensor_Active.Name = "HmiSensor_Active"
        Me.HmiSensor_Active.Size = New System.Drawing.Size(28, 20)
        Me.HmiSensor_Active.TabIndex = 6
        '
        'HmiButtonWithIndicate_Start
        '
        Me.HmiButtonWithIndicate_Start.BackColor = System.Drawing.Color.Transparent
        Me.HmiButtonWithIndicate_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_Start.Location = New System.Drawing.Point(95, 3)
        Me.HmiButtonWithIndicate_Start.Name = "HmiButtonWithIndicate_Start"
        Me.HmiButtonWithIndicate_Start.Size = New System.Drawing.Size(95, 24)
        Me.HmiButtonWithIndicate_Start.TabIndex = 8
        Me.HmiButtonWithIndicate_Start.Text = "HmiButtonWithIndicate1"
        Me.HmiButtonWithIndicate_Start.UseVisualStyleBackColor = True
        '
        'Panel_Indicate_Cup
        '
        Me.Panel_Indicate_Cup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Indicate_Cup.Location = New System.Drawing.Point(3, 4)
        Me.Panel_Indicate_Cup.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel_Indicate_Cup.Name = "Panel_Indicate_Cup"
        Me.Panel_Indicate_Cup.Size = New System.Drawing.Size(27, 22)
        Me.Panel_Indicate_Cup.TabIndex = 5
        '
        'HmiTableLayoutPanel_Report
        '
        Me.HmiTableLayoutPanel_Report.ColumnCount = 3
        Me.TableLayoutPanel_Mid_Right.SetColumnSpan(Me.HmiTableLayoutPanel_Report, 3)
        Me.HmiTableLayoutPanel_Report.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.HmiTableLayoutPanel_Report.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.HmiTableLayoutPanel_Report.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_LastWeight, 1, 1)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_CPK, 1, 9)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_CP, 1, 8)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_Std, 1, 7)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_MinWeight, 1, 6)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_Weight2, 1, 5)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_MaxWeight, 1, 4)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_InCycle, 1, 3)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_Result, 0, 10)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_PerCycle2, 0, 2)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_MaxWeight, 0, 4)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_InCycle, 0, 3)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_Weight2, 0, 5)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_MinWeight, 0, 6)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_Std, 0, 7)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_CP, 0, 8)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_CPK, 0, 9)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_Name, 0, 0)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.HmiTextBox_PerCycle2, 1, 2)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Label_LastWeight, 0, 1)
        Me.HmiTableLayoutPanel_Report.Controls.Add(Me.Panel_Result, 1, 10)
        Me.HmiTableLayoutPanel_Report.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Report.Location = New System.Drawing.Point(1, 36)
        Me.HmiTableLayoutPanel_Report.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Report.Name = "HmiTableLayoutPanel_Report"
        Me.HmiTableLayoutPanel_Report.RowCount = 11
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090908!))
        Me.HmiTableLayoutPanel_Report.Size = New System.Drawing.Size(153, 285)
        Me.HmiTableLayoutPanel_Report.TabIndex = 1
        '
        'HmiTextBox_LastWeight
        '
        Me.HmiTextBox_LastWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_LastWeight.Location = New System.Drawing.Point(82, 28)
        Me.HmiTextBox_LastWeight.Name = "HmiTextBox_LastWeight"
        Me.HmiTextBox_LastWeight.Number = 0
        Me.HmiTextBox_LastWeight.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_LastWeight.TabIndex = 25
        Me.HmiTextBox_LastWeight.TextBoxReadOnly = False
        Me.HmiTextBox_LastWeight.ValueType = GetType(String)
        '
        'HmiTextBox_CPK
        '
        Me.HmiTextBox_CPK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_CPK.Location = New System.Drawing.Point(82, 228)
        Me.HmiTextBox_CPK.Name = "HmiTextBox_CPK"
        Me.HmiTextBox_CPK.Number = 0
        Me.HmiTextBox_CPK.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_CPK.TabIndex = 22
        Me.HmiTextBox_CPK.TextBoxReadOnly = False
        Me.HmiTextBox_CPK.ValueType = GetType(String)
        '
        'HmiTextBox_CP
        '
        Me.HmiTextBox_CP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_CP.Location = New System.Drawing.Point(82, 203)
        Me.HmiTextBox_CP.Name = "HmiTextBox_CP"
        Me.HmiTextBox_CP.Number = 0
        Me.HmiTextBox_CP.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_CP.TabIndex = 21
        Me.HmiTextBox_CP.TextBoxReadOnly = False
        Me.HmiTextBox_CP.ValueType = GetType(String)
        '
        'HmiTextBox_Std
        '
        Me.HmiTextBox_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Std.Location = New System.Drawing.Point(82, 178)
        Me.HmiTextBox_Std.Name = "HmiTextBox_Std"
        Me.HmiTextBox_Std.Number = 0
        Me.HmiTextBox_Std.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_Std.TabIndex = 20
        Me.HmiTextBox_Std.TextBoxReadOnly = False
        Me.HmiTextBox_Std.ValueType = GetType(String)
        '
        'HmiTextBox_MinWeight
        '
        Me.HmiTextBox_MinWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MinWeight.Location = New System.Drawing.Point(82, 153)
        Me.HmiTextBox_MinWeight.Name = "HmiTextBox_MinWeight"
        Me.HmiTextBox_MinWeight.Number = 0
        Me.HmiTextBox_MinWeight.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_MinWeight.TabIndex = 19
        Me.HmiTextBox_MinWeight.TextBoxReadOnly = False
        Me.HmiTextBox_MinWeight.ValueType = GetType(String)
        '
        'HmiTextBox_Weight2
        '
        Me.HmiTextBox_Weight2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Weight2.Location = New System.Drawing.Point(82, 128)
        Me.HmiTextBox_Weight2.Name = "HmiTextBox_Weight2"
        Me.HmiTextBox_Weight2.Number = 0
        Me.HmiTextBox_Weight2.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_Weight2.TabIndex = 18
        Me.HmiTextBox_Weight2.TextBoxReadOnly = False
        Me.HmiTextBox_Weight2.ValueType = GetType(String)
        '
        'HmiTextBox_MaxWeight
        '
        Me.HmiTextBox_MaxWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_MaxWeight.Location = New System.Drawing.Point(82, 103)
        Me.HmiTextBox_MaxWeight.Name = "HmiTextBox_MaxWeight"
        Me.HmiTextBox_MaxWeight.Number = 0
        Me.HmiTextBox_MaxWeight.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_MaxWeight.TabIndex = 17
        Me.HmiTextBox_MaxWeight.TextBoxReadOnly = False
        Me.HmiTextBox_MaxWeight.ValueType = GetType(String)
        '
        'HmiTextBox_InCycle
        '
        Me.HmiTextBox_InCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_InCycle.Location = New System.Drawing.Point(82, 78)
        Me.HmiTextBox_InCycle.Name = "HmiTextBox_InCycle"
        Me.HmiTextBox_InCycle.Number = 0
        Me.HmiTextBox_InCycle.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_InCycle.TabIndex = 16
        Me.HmiTextBox_InCycle.TextBoxReadOnly = False
        Me.HmiTextBox_InCycle.ValueType = GetType(String)
        '
        'Label_Result
        '
        Me.Label_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Result.Location = New System.Drawing.Point(3, 253)
        Me.Label_Result.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Result.Name = "Label_Result"
        Me.Label_Result.Size = New System.Drawing.Size(73, 29)
        Me.Label_Result.TabIndex = 12
        Me.Label_Result.Text = "Label24"
        Me.Label_Result.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PerCycle2
        '
        Me.Label_PerCycle2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PerCycle2.Location = New System.Drawing.Point(3, 53)
        Me.Label_PerCycle2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_PerCycle2.Name = "Label_PerCycle2"
        Me.Label_PerCycle2.Size = New System.Drawing.Size(73, 19)
        Me.Label_PerCycle2.TabIndex = 10
        Me.Label_PerCycle2.Text = "Label22"
        Me.Label_PerCycle2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_MaxWeight
        '
        Me.Label_MaxWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_MaxWeight.Location = New System.Drawing.Point(3, 103)
        Me.Label_MaxWeight.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_MaxWeight.Name = "Label_MaxWeight"
        Me.Label_MaxWeight.Size = New System.Drawing.Size(73, 19)
        Me.Label_MaxWeight.TabIndex = 9
        Me.Label_MaxWeight.Text = "Label21"
        Me.Label_MaxWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_InCycle
        '
        Me.Label_InCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_InCycle.Location = New System.Drawing.Point(3, 78)
        Me.Label_InCycle.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_InCycle.Name = "Label_InCycle"
        Me.Label_InCycle.Size = New System.Drawing.Size(73, 19)
        Me.Label_InCycle.TabIndex = 8
        Me.Label_InCycle.Text = "Label20"
        Me.Label_InCycle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Weight2
        '
        Me.Label_Weight2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Weight2.Location = New System.Drawing.Point(3, 128)
        Me.Label_Weight2.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Weight2.Name = "Label_Weight2"
        Me.Label_Weight2.Size = New System.Drawing.Size(73, 19)
        Me.Label_Weight2.TabIndex = 7
        Me.Label_Weight2.Text = "Label19"
        Me.Label_Weight2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_MinWeight
        '
        Me.Label_MinWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_MinWeight.Location = New System.Drawing.Point(3, 153)
        Me.Label_MinWeight.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_MinWeight.Name = "Label_MinWeight"
        Me.Label_MinWeight.Size = New System.Drawing.Size(73, 19)
        Me.Label_MinWeight.TabIndex = 6
        Me.Label_MinWeight.Text = "Label18"
        Me.Label_MinWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Std
        '
        Me.Label_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Std.Location = New System.Drawing.Point(3, 178)
        Me.Label_Std.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Std.Name = "Label_Std"
        Me.Label_Std.Size = New System.Drawing.Size(73, 19)
        Me.Label_Std.TabIndex = 5
        Me.Label_Std.Text = "Label17"
        Me.Label_Std.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_CP
        '
        Me.Label_CP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_CP.Location = New System.Drawing.Point(3, 203)
        Me.Label_CP.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_CP.Name = "Label_CP"
        Me.Label_CP.Size = New System.Drawing.Size(73, 19)
        Me.Label_CP.TabIndex = 4
        Me.Label_CP.Text = "Label16"
        Me.Label_CP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_CPK
        '
        Me.Label_CPK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_CPK.Location = New System.Drawing.Point(3, 228)
        Me.Label_CPK.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_CPK.Name = "Label_CPK"
        Me.Label_CPK.Size = New System.Drawing.Size(73, 19)
        Me.Label_CPK.TabIndex = 3
        Me.Label_CPK.Text = "Label15"
        Me.Label_CPK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Name
        '
        Me.Label_Name.BackColor = System.Drawing.Color.White
        Me.HmiTableLayoutPanel_Report.SetColumnSpan(Me.Label_Name, 2)
        Me.Label_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Name.Font = New System.Drawing.Font("Calibri", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label_Name.Location = New System.Drawing.Point(1, 1)
        Me.Label_Name.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Name.Name = "Label_Name"
        Me.Label_Name.Size = New System.Drawing.Size(129, 23)
        Me.Label_Name.TabIndex = 2
        Me.Label_Name.Text = "Mixture  1:1"
        Me.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'HmiTextBox_PerCycle2
        '
        Me.HmiTextBox_PerCycle2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PerCycle2.Location = New System.Drawing.Point(82, 53)
        Me.HmiTextBox_PerCycle2.Name = "HmiTextBox_PerCycle2"
        Me.HmiTextBox_PerCycle2.Number = 0
        Me.HmiTextBox_PerCycle2.Size = New System.Drawing.Size(46, 19)
        Me.HmiTextBox_PerCycle2.TabIndex = 13
        Me.HmiTextBox_PerCycle2.TextBoxReadOnly = False
        Me.HmiTextBox_PerCycle2.ValueType = GetType(String)
        '
        'Label_LastWeight
        '
        Me.Label_LastWeight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_LastWeight.Location = New System.Drawing.Point(3, 28)
        Me.Label_LastWeight.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_LastWeight.Name = "Label_LastWeight"
        Me.Label_LastWeight.Size = New System.Drawing.Size(73, 19)
        Me.Label_LastWeight.TabIndex = 24
        Me.Label_LastWeight.Text = "Label1"
        Me.Label_LastWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_Result
        '
        Me.Panel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Result.Location = New System.Drawing.Point(82, 253)
        Me.Panel_Result.Name = "Panel_Result"
        Me.Panel_Result.Size = New System.Drawing.Size(46, 29)
        Me.Panel_Result.TabIndex = 26
        '
        'HmiTableLayoutPanel_Parameter
        '
        Me.HmiTableLayoutPanel_Parameter.ColumnCount = 2
        Me.HmiTableLayoutPanel_Parameter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.HmiTableLayoutPanel_Parameter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_DispensingPause, 1, 8)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_DispensingTime, 1, 7)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_PerCycle, 1, 6)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_Pause, 1, 5)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_PreShotTime, 1, 4)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_PreShot, 1, 3)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_Min, 1, 2)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_DispensingPause, 0, 8)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_Max, 0, 1)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_PreShot, 0, 3)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_PreShotTime, 0, 4)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_Pause, 0, 5)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_PerCycle, 0, 6)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_Min, 0, 2)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_DispensingTime, 0, 7)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.Label_Title, 0, 0)
        Me.HmiTableLayoutPanel_Parameter.Controls.Add(Me.HmiTextBox_Max, 1, 1)
        Me.HmiTableLayoutPanel_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Parameter.Location = New System.Drawing.Point(1, 36)
        Me.HmiTableLayoutPanel_Parameter.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Parameter.Name = "HmiTableLayoutPanel_Parameter"
        Me.HmiTableLayoutPanel_Parameter.RowCount = 9
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.HmiTableLayoutPanel_Parameter.Size = New System.Drawing.Size(182, 285)
        Me.HmiTableLayoutPanel_Parameter.TabIndex = 0
        '
        'HmiTextBox_DispensingPause
        '
        Me.HmiTextBox_DispensingPause.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_DispensingPause.Location = New System.Drawing.Point(130, 251)
        Me.HmiTextBox_DispensingPause.Name = "HmiTextBox_DispensingPause"
        Me.HmiTextBox_DispensingPause.Number = 0
        Me.HmiTextBox_DispensingPause.Size = New System.Drawing.Size(49, 31)
        Me.HmiTextBox_DispensingPause.TabIndex = 17
        Me.HmiTextBox_DispensingPause.TextBoxReadOnly = False
        Me.HmiTextBox_DispensingPause.ValueType = GetType(String)
        '
        'HmiTextBox_DispensingTime
        '
        Me.HmiTextBox_DispensingTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_DispensingTime.Location = New System.Drawing.Point(130, 220)
        Me.HmiTextBox_DispensingTime.Name = "HmiTextBox_DispensingTime"
        Me.HmiTextBox_DispensingTime.Number = 0
        Me.HmiTextBox_DispensingTime.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_DispensingTime.TabIndex = 16
        Me.HmiTextBox_DispensingTime.TextBoxReadOnly = False
        Me.HmiTextBox_DispensingTime.ValueType = GetType(String)
        '
        'HmiTextBox_PerCycle
        '
        Me.HmiTextBox_PerCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PerCycle.Location = New System.Drawing.Point(130, 189)
        Me.HmiTextBox_PerCycle.Name = "HmiTextBox_PerCycle"
        Me.HmiTextBox_PerCycle.Number = 0
        Me.HmiTextBox_PerCycle.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_PerCycle.TabIndex = 15
        Me.HmiTextBox_PerCycle.TextBoxReadOnly = False
        Me.HmiTextBox_PerCycle.ValueType = GetType(String)
        '
        'HmiTextBox_Pause
        '
        Me.HmiTextBox_Pause.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Pause.Location = New System.Drawing.Point(130, 158)
        Me.HmiTextBox_Pause.Name = "HmiTextBox_Pause"
        Me.HmiTextBox_Pause.Number = 0
        Me.HmiTextBox_Pause.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_Pause.TabIndex = 14
        Me.HmiTextBox_Pause.TextBoxReadOnly = False
        Me.HmiTextBox_Pause.ValueType = GetType(String)
        '
        'HmiTextBox_PreShotTime
        '
        Me.HmiTextBox_PreShotTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PreShotTime.Location = New System.Drawing.Point(130, 127)
        Me.HmiTextBox_PreShotTime.Name = "HmiTextBox_PreShotTime"
        Me.HmiTextBox_PreShotTime.Number = 0
        Me.HmiTextBox_PreShotTime.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_PreShotTime.TabIndex = 13
        Me.HmiTextBox_PreShotTime.TextBoxReadOnly = False
        Me.HmiTextBox_PreShotTime.ValueType = GetType(String)
        '
        'HmiTextBox_PreShot
        '
        Me.HmiTextBox_PreShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PreShot.Location = New System.Drawing.Point(130, 96)
        Me.HmiTextBox_PreShot.Name = "HmiTextBox_PreShot"
        Me.HmiTextBox_PreShot.Number = 0
        Me.HmiTextBox_PreShot.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_PreShot.TabIndex = 12
        Me.HmiTextBox_PreShot.TextBoxReadOnly = False
        Me.HmiTextBox_PreShot.ValueType = GetType(String)
        '
        'HmiTextBox_Min
        '
        Me.HmiTextBox_Min.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Min.Location = New System.Drawing.Point(130, 65)
        Me.HmiTextBox_Min.Name = "HmiTextBox_Min"
        Me.HmiTextBox_Min.Number = 0
        Me.HmiTextBox_Min.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_Min.TabIndex = 11
        Me.HmiTextBox_Min.TextBoxReadOnly = False
        Me.HmiTextBox_Min.ValueType = GetType(String)
        '
        'Label_DispensingPause
        '
        Me.Label_DispensingPause.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_DispensingPause.Location = New System.Drawing.Point(3, 251)
        Me.Label_DispensingPause.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_DispensingPause.Name = "Label_DispensingPause"
        Me.Label_DispensingPause.Size = New System.Drawing.Size(121, 31)
        Me.Label_DispensingPause.TabIndex = 9
        Me.Label_DispensingPause.Text = "Label13"
        Me.Label_DispensingPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Max
        '
        Me.Label_Max.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Max.Location = New System.Drawing.Point(3, 34)
        Me.Label_Max.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Max.Name = "Label_Max"
        Me.Label_Max.Size = New System.Drawing.Size(121, 25)
        Me.Label_Max.TabIndex = 8
        Me.Label_Max.Text = "Label12"
        Me.Label_Max.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PreShot
        '
        Me.Label_PreShot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PreShot.Location = New System.Drawing.Point(3, 96)
        Me.Label_PreShot.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_PreShot.Name = "Label_PreShot"
        Me.Label_PreShot.Size = New System.Drawing.Size(121, 25)
        Me.Label_PreShot.TabIndex = 7
        Me.Label_PreShot.Text = "Label11"
        Me.Label_PreShot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PreShotTime
        '
        Me.Label_PreShotTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PreShotTime.Location = New System.Drawing.Point(3, 127)
        Me.Label_PreShotTime.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_PreShotTime.Name = "Label_PreShotTime"
        Me.Label_PreShotTime.Size = New System.Drawing.Size(121, 25)
        Me.Label_PreShotTime.TabIndex = 6
        Me.Label_PreShotTime.Text = "Label10"
        Me.Label_PreShotTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Pause
        '
        Me.Label_Pause.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Pause.Location = New System.Drawing.Point(3, 158)
        Me.Label_Pause.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Pause.Name = "Label_Pause"
        Me.Label_Pause.Size = New System.Drawing.Size(121, 25)
        Me.Label_Pause.TabIndex = 5
        Me.Label_Pause.Text = "Label9"
        Me.Label_Pause.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_PerCycle
        '
        Me.Label_PerCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_PerCycle.Location = New System.Drawing.Point(1, 189)
        Me.Label_PerCycle.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.Label_PerCycle.Name = "Label_PerCycle"
        Me.Label_PerCycle.Size = New System.Drawing.Size(125, 25)
        Me.Label_PerCycle.TabIndex = 4
        Me.Label_PerCycle.Text = "Label8"
        Me.Label_PerCycle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Min
        '
        Me.Label_Min.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Min.Location = New System.Drawing.Point(3, 65)
        Me.Label_Min.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_Min.Name = "Label_Min"
        Me.Label_Min.Size = New System.Drawing.Size(121, 25)
        Me.Label_Min.TabIndex = 3
        Me.Label_Min.Text = "Label7"
        Me.Label_Min.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_DispensingTime
        '
        Me.Label_DispensingTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_DispensingTime.Location = New System.Drawing.Point(3, 220)
        Me.Label_DispensingTime.Margin = New System.Windows.Forms.Padding(3)
        Me.Label_DispensingTime.Name = "Label_DispensingTime"
        Me.Label_DispensingTime.Size = New System.Drawing.Size(121, 25)
        Me.Label_DispensingTime.TabIndex = 2
        Me.Label_DispensingTime.Text = "Label6"
        Me.Label_DispensingTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label_Title
        '
        Me.Label_Title.BackColor = System.Drawing.Color.White
        Me.HmiTableLayoutPanel_Parameter.SetColumnSpan(Me.Label_Title, 2)
        Me.Label_Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Title.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.Label_Title.Location = New System.Drawing.Point(1, 1)
        Me.Label_Title.Margin = New System.Windows.Forms.Padding(1)
        Me.Label_Title.Name = "Label_Title"
        Me.Label_Title.Size = New System.Drawing.Size(180, 29)
        Me.Label_Title.TabIndex = 1
        Me.Label_Title.Text = "A 3.000g +B 3.000g =6.000g"
        Me.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'HmiTextBox_Max
        '
        Me.HmiTextBox_Max.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Max.Location = New System.Drawing.Point(130, 34)
        Me.HmiTextBox_Max.Name = "HmiTextBox_Max"
        Me.HmiTextBox_Max.Number = 0
        Me.HmiTextBox_Max.Size = New System.Drawing.Size(49, 25)
        Me.HmiTextBox_Max.TabIndex = 10
        Me.HmiTextBox_Max.TextBoxReadOnly = False
        Me.HmiTextBox_Max.ValueType = GetType(String)
        '
        'WeightUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(615, 498)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "WeightUI"
        Me.Text = "WeightUI"
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Top.ResumeLayout(False)
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Top.ResumeLayout(False)
        Me.GroupBox_AutoWeight.ResumeLayout(False)
        Me.TableLayoutPanel_AutoWeight.ResumeLayout(False)
        Me.TableLayoutPanel_AutoWeight.PerformLayout()
        Me.GroupBox_Weight.ResumeLayout(False)
        Me.TableLayoutPanel_Weight.ResumeLayout(False)
        Me.GroupBox_Cup.ResumeLayout(False)
        Me.TableLayoutPanel_Cup.ResumeLayout(False)
        Me.TableLayoutPanel_Function.ResumeLayout(False)
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Mid_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Mid_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Mid_Mid.ResumeLayout(False)
        CType(Me.Chart_Weight_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.Panel_Body.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Report.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Parameter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Top As System.Windows.Forms.Panel
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents Panel_UI As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Top As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_AutoWeight As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_AutoWeight As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Weight As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Weight As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Cup As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Cup As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid_Right As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Report As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTableLayoutPanel_Parameter As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label_Cup As System.Windows.Forms.Label
    Friend WithEvents Label_AutoWeightNo As System.Windows.Forms.Label
    Friend WithEvents Label3_AutoWeight As System.Windows.Forms.Label
    Friend WithEvents CheckBox_AutoWeight As System.Windows.Forms.CheckBox
    Friend WithEvents HmiTextBox_AutoWeightNo As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_Weight As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_CPK As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_CP As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Std As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MinWeight As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Weight2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_MaxWeight As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_InCycle As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_Result As System.Windows.Forms.Label
    Friend WithEvents Label_PerCycle2 As System.Windows.Forms.Label
    Friend WithEvents Label_MaxWeight As System.Windows.Forms.Label
    Friend WithEvents Label_InCycle As System.Windows.Forms.Label
    Friend WithEvents Label_Weight2 As System.Windows.Forms.Label
    Friend WithEvents Label_MinWeight As System.Windows.Forms.Label
    Friend WithEvents Label_Std As System.Windows.Forms.Label
    Friend WithEvents Label_CP As System.Windows.Forms.Label
    Friend WithEvents Label_CPK As System.Windows.Forms.Label
    Friend WithEvents Label_Name As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_PerCycle2 As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_DispensingPause As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_DispensingTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PerCycle As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Pause As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PreShotTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PreShot As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Min As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_DispensingPause As System.Windows.Forms.Label
    Friend WithEvents Label_Max As System.Windows.Forms.Label
    Friend WithEvents Label_PreShot As System.Windows.Forms.Label
    Friend WithEvents Label_PreShotTime As System.Windows.Forms.Label
    Friend WithEvents Label_Pause As System.Windows.Forms.Label
    Friend WithEvents Label_PerCycle As System.Windows.Forms.Label
    Friend WithEvents Label_Min As System.Windows.Forms.Label
    Friend WithEvents Label_DispensingTime As System.Windows.Forms.Label
    Friend WithEvents Label_Title As System.Windows.Forms.Label
    Friend WithEvents HmiTextBox_Max As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_LastWeight As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents Label_LastWeight As System.Windows.Forms.Label
    Friend WithEvents Chart_Weight_Value As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Data As System.Windows.Forms.Button
    Friend WithEvents Button_Back As System.Windows.Forms.Button
    Friend WithEvents Panel_Result As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents TableLayoutPanel_Function As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel_Indicate_Cup As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents Label_Active As System.Windows.Forms.Label
    Friend WithEvents HmiSensor_Active As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiButtonWithIndicate_Start As HMIButtonWithIndicate
End Class
