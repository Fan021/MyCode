<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenScrewForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component List.
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_Screw = New System.Windows.Forms.TabControl()
        Me.TabPage_Data = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.TabPage_Torque = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid_Torque = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Torque_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Torque_Step = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Rate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Rate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Fail = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Fail = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Cpk = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Cpk = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Cp = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Cp = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Std = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Std = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_UpLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_UpLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_LowLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_LowLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Torque_MaxValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_MaxValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_AvgValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_AvgValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Pass = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Pass = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_MinValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_MinValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Torque_Total = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Torque_Total = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Torque_Step = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Torque_Step3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Torque_Step2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Torque_Step1 = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel_Body_Torque_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Torque_Rate = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart_Torque_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage_Angle = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid_Angle = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Angle_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.TableLayoutPanel_Angle_Step = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Angle_Step3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Angle_Step2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Angle_Step1 = New System.Windows.Forms.RadioButton()
        Me.HmiLabel_Angle_Step = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Rate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Rate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Fail = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Fail = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Cpk = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Cpk = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Cp = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Cp = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Std = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Std = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_UpLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_UpLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_LowLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_LowLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Angle_MaxValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_MaxValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_AvgValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_AvgValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Pass = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Pass = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_MinValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_MinValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Angle_Total = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Angle_Total = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body_Angle_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Angle_Rate = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart_Angle_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_Seq = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Program = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Result = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Program = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_AST = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_AST = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Device = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Device = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PartNumber = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_PartNumber = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDateTime_Start = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiDateTime_End = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiButton_Export = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Station = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Seq = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.ContextMenuStrip_Function = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogcsv = New System.Windows.Forms.SaveFileDialog()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabControl_Screw.SuspendLayout()
        Me.TabPage_Data.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Torque.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Torque.SuspendLayout()
        Me.TableLayoutPanel_Body_Torque_Head.SuspendLayout()
        Me.TableLayoutPanel_Torque_Step.SuspendLayout()
        Me.TableLayoutPanel_Body_Torque_Bottom.SuspendLayout()
        CType(Me.Chart_Torque_Rate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart_Torque_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Angle.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Angle.SuspendLayout()
        Me.TableLayoutPanel_Body_Angle_Head.SuspendLayout()
        Me.TableLayoutPanel_Angle_Step.SuspendLayout()
        Me.TableLayoutPanel_Body_Angle_Bottom.SuspendLayout()
        CType(Me.Chart_Angle_Rate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart_Angle_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Search.SuspendLayout()
        Me.TableLayoutPanel_Body_Head.SuspendLayout()
        Me.ContextMenuStrip_Function.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_Screw, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 189.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabControl_Screw
        '
        Me.TabControl_Screw.Controls.Add(Me.TabPage_Data)
        Me.TabControl_Screw.Controls.Add(Me.TabPage_Torque)
        Me.TabControl_Screw.Controls.Add(Me.TabPage_Angle)
        Me.TabControl_Screw.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Screw.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_Screw.Location = New System.Drawing.Point(0, 189)
        Me.TabControl_Screw.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Screw.Name = "TabControl_Screw"
        Me.TabControl_Screw.SelectedIndex = 0
        Me.TabControl_Screw.Size = New System.Drawing.Size(467, 341)
        Me.TabControl_Screw.TabIndex = 5
        '
        'TabPage_Data
        '
        Me.TabPage_Data.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.TabPage_Data.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Data.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Data.Name = "TabPage_Data"
        Me.TabPage_Data.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Data.Size = New System.Drawing.Size(459, 309)
        Me.TabPage_Data.TabIndex = 0
        Me.TabPage_Data.Text = "Data"
        Me.TabPage_Data.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(453, 303)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 1
        '
        'HmiDataView_Data
        '
        Me.HmiDataView_Data.AllowUserToAddRows = False
        Me.HmiDataView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Data.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.HmiDataView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HmiDataView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Data.EnableHeadersVisualStyles = False
        Me.HmiDataView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Data.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataView_Data.Name = "HmiDataView_Data"
        Me.HmiDataView_Data.ReadOnly = True
        Me.HmiDataView_Data.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.HmiDataView_Data.RowTemplate.Height = 40
        Me.HmiDataView_Data.RowTemplate.ReadOnly = True
        Me.HmiDataView_Data.Size = New System.Drawing.Size(453, 266)
        Me.HmiDataView_Data.TabIndex = 0
        '
        'HmiDataViewPage_Data
        '
        Me.HmiDataViewPage_Data.Button_DownEnable = False
        Me.HmiDataViewPage_Data.Button_DownLastEnable = False
        Me.HmiDataViewPage_Data.Button_GoEnable = False
        Me.HmiDataViewPage_Data.Button_UpEnable = False
        Me.HmiDataViewPage_Data.Button_UpFirstEnable = False
        Me.HmiDataViewPage_Data.CurrentPage = 0
        Me.HmiDataViewPage_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 266)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(453, 37)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'TabPage_Torque
        '
        Me.TabPage_Torque.Controls.Add(Me.TableLayoutPanel_Body_Mid_Torque)
        Me.TabPage_Torque.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Torque.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Torque.Name = "TabPage_Torque"
        Me.TabPage_Torque.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Torque.Size = New System.Drawing.Size(459, 309)
        Me.TabPage_Torque.TabIndex = 1
        Me.TabPage_Torque.Text = "Torque"
        Me.TabPage_Torque.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid_Torque
        '
        Me.TableLayoutPanel_Body_Mid_Torque.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Torque.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Torque.Controls.Add(Me.TableLayoutPanel_Body_Torque_Head, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Torque.Controls.Add(Me.TableLayoutPanel_Body_Torque_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Torque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Torque.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid_Torque.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Torque.Name = "TableLayoutPanel_Body_Mid_Torque"
        Me.TableLayoutPanel_Body_Mid_Torque.RowCount = 2
        Me.TableLayoutPanel_Body_Mid_Torque.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 273.0!))
        Me.TableLayoutPanel_Body_Mid_Torque.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid_Torque.Size = New System.Drawing.Size(453, 303)
        Me.TableLayoutPanel_Body_Mid_Torque.TabIndex = 0
        '
        'TableLayoutPanel_Body_Torque_Head
        '
        Me.TableLayoutPanel_Body_Torque_Head.ColumnCount = 4
        Me.TableLayoutPanel_Body_Torque_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Torque_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Torque_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Step, 0, 0)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Rate, 3, 7)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Rate, 2, 7)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Fail, 1, 7)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Fail, 0, 7)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Cpk, 3, 5)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Cpk, 2, 5)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Cp, 1, 5)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Cp, 0, 5)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Std, 1, 4)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Std, 0, 4)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_UpLimit, 3, 3)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_UpLimit, 2, 3)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_LowLimit, 1, 3)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_LowLimit, 0, 3)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_MaxValue, 0, 1)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_MaxValue, 1, 1)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_AvgValue, 0, 2)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_AvgValue, 1, 2)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Pass, 2, 6)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Pass, 3, 6)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_MinValue, 2, 1)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_MinValue, 3, 1)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiLabel_Torque_Total, 0, 6)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.HmiTextBox_Torque_Total, 1, 6)
        Me.TableLayoutPanel_Body_Torque_Head.Controls.Add(Me.TableLayoutPanel_Torque_Step, 1, 0)
        Me.TableLayoutPanel_Body_Torque_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Torque_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Torque_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Torque_Head.Name = "TableLayoutPanel_Body_Torque_Head"
        Me.TableLayoutPanel_Body_Torque_Head.RowCount = 8
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Head.Size = New System.Drawing.Size(453, 273)
        Me.TableLayoutPanel_Body_Torque_Head.TabIndex = 1
        '
        'HmiLabel_Torque_Step
        '
        Me.HmiLabel_Torque_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Step.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Torque_Step.Name = "HmiLabel_Torque_Step"
        Me.HmiLabel_Torque_Step.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Step.TabIndex = 37
        '
        'HmiTextBox_Torque_Rate
        '
        Me.HmiTextBox_Torque_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Rate.Location = New System.Drawing.Point(363, 143)
        Me.HmiTextBox_Torque_Rate.Name = "HmiTextBox_Torque_Rate"
        Me.HmiTextBox_Torque_Rate.Number = 0
        Me.HmiTextBox_Torque_Rate.Size = New System.Drawing.Size(87, 127)
        Me.HmiTextBox_Torque_Rate.TabIndex = 24
        Me.HmiTextBox_Torque_Rate.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Rate.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Rate
        '
        Me.HmiLabel_Torque_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Rate.Location = New System.Drawing.Point(228, 143)
        Me.HmiLabel_Torque_Rate.Name = "HmiLabel_Torque_Rate"
        Me.HmiLabel_Torque_Rate.Size = New System.Drawing.Size(129, 127)
        Me.HmiLabel_Torque_Rate.TabIndex = 23
        '
        'HmiTextBox_Torque_Fail
        '
        Me.HmiTextBox_Torque_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Fail.Location = New System.Drawing.Point(138, 143)
        Me.HmiTextBox_Torque_Fail.Name = "HmiTextBox_Torque_Fail"
        Me.HmiTextBox_Torque_Fail.Number = 0
        Me.HmiTextBox_Torque_Fail.Size = New System.Drawing.Size(84, 127)
        Me.HmiTextBox_Torque_Fail.TabIndex = 22
        Me.HmiTextBox_Torque_Fail.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Fail.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Fail
        '
        Me.HmiLabel_Torque_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Fail.Location = New System.Drawing.Point(3, 143)
        Me.HmiLabel_Torque_Fail.Name = "HmiLabel_Torque_Fail"
        Me.HmiLabel_Torque_Fail.Size = New System.Drawing.Size(129, 127)
        Me.HmiLabel_Torque_Fail.TabIndex = 21
        '
        'HmiTextBox_Torque_Cpk
        '
        Me.HmiTextBox_Torque_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Cpk.Location = New System.Drawing.Point(363, 103)
        Me.HmiTextBox_Torque_Cpk.Name = "HmiTextBox_Torque_Cpk"
        Me.HmiTextBox_Torque_Cpk.Number = 0
        Me.HmiTextBox_Torque_Cpk.Size = New System.Drawing.Size(87, 14)
        Me.HmiTextBox_Torque_Cpk.TabIndex = 20
        Me.HmiTextBox_Torque_Cpk.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Cpk.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Cpk
        '
        Me.HmiLabel_Torque_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Cpk.Location = New System.Drawing.Point(228, 103)
        Me.HmiLabel_Torque_Cpk.Name = "HmiLabel_Torque_Cpk"
        Me.HmiLabel_Torque_Cpk.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Cpk.TabIndex = 19
        '
        'HmiTextBox_Torque_Cp
        '
        Me.HmiTextBox_Torque_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Cp.Location = New System.Drawing.Point(138, 103)
        Me.HmiTextBox_Torque_Cp.Name = "HmiTextBox_Torque_Cp"
        Me.HmiTextBox_Torque_Cp.Number = 0
        Me.HmiTextBox_Torque_Cp.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_Cp.TabIndex = 18
        Me.HmiTextBox_Torque_Cp.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Cp.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Cp
        '
        Me.HmiLabel_Torque_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Cp.Location = New System.Drawing.Point(3, 103)
        Me.HmiLabel_Torque_Cp.Name = "HmiLabel_Torque_Cp"
        Me.HmiLabel_Torque_Cp.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Cp.TabIndex = 17
        '
        'HmiTextBox_Torque_Std
        '
        Me.HmiTextBox_Torque_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Std.Location = New System.Drawing.Point(138, 83)
        Me.HmiTextBox_Torque_Std.Name = "HmiTextBox_Torque_Std"
        Me.HmiTextBox_Torque_Std.Number = 0
        Me.HmiTextBox_Torque_Std.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_Std.TabIndex = 14
        Me.HmiTextBox_Torque_Std.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Std.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Std
        '
        Me.HmiLabel_Torque_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Std.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_Torque_Std.Name = "HmiLabel_Torque_Std"
        Me.HmiLabel_Torque_Std.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Std.TabIndex = 13
        '
        'HmiTextBox_Torque_UpLimit
        '
        Me.HmiTextBox_Torque_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_UpLimit.Location = New System.Drawing.Point(363, 63)
        Me.HmiTextBox_Torque_UpLimit.Name = "HmiTextBox_Torque_UpLimit"
        Me.HmiTextBox_Torque_UpLimit.Number = 0
        Me.HmiTextBox_Torque_UpLimit.Size = New System.Drawing.Size(87, 14)
        Me.HmiTextBox_Torque_UpLimit.TabIndex = 11
        Me.HmiTextBox_Torque_UpLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_UpLimit.ValueType = GetType(String)
        '
        'HmiLabel_Torque_UpLimit
        '
        Me.HmiLabel_Torque_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_UpLimit.Location = New System.Drawing.Point(228, 63)
        Me.HmiLabel_Torque_UpLimit.Name = "HmiLabel_Torque_UpLimit"
        Me.HmiLabel_Torque_UpLimit.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_UpLimit.TabIndex = 10
        '
        'HmiTextBox_Torque_LowLimit
        '
        Me.HmiTextBox_Torque_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_LowLimit.Location = New System.Drawing.Point(138, 63)
        Me.HmiTextBox_Torque_LowLimit.Name = "HmiTextBox_Torque_LowLimit"
        Me.HmiTextBox_Torque_LowLimit.Number = 0
        Me.HmiTextBox_Torque_LowLimit.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_LowLimit.TabIndex = 9
        Me.HmiTextBox_Torque_LowLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_LowLimit.ValueType = GetType(String)
        '
        'HmiLabel_Torque_LowLimit
        '
        Me.HmiLabel_Torque_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_LowLimit.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_Torque_LowLimit.Name = "HmiLabel_Torque_LowLimit"
        Me.HmiLabel_Torque_LowLimit.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_LowLimit.TabIndex = 8
        '
        'HmiLabel_Torque_MaxValue
        '
        Me.HmiLabel_Torque_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_MaxValue.Location = New System.Drawing.Point(3, 23)
        Me.HmiLabel_Torque_MaxValue.Name = "HmiLabel_Torque_MaxValue"
        Me.HmiLabel_Torque_MaxValue.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_MaxValue.TabIndex = 0
        '
        'HmiTextBox_Torque_MaxValue
        '
        Me.HmiTextBox_Torque_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_MaxValue.Location = New System.Drawing.Point(138, 23)
        Me.HmiTextBox_Torque_MaxValue.Name = "HmiTextBox_Torque_MaxValue"
        Me.HmiTextBox_Torque_MaxValue.Number = 0
        Me.HmiTextBox_Torque_MaxValue.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_MaxValue.TabIndex = 1
        Me.HmiTextBox_Torque_MaxValue.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_MaxValue.ValueType = GetType(String)
        '
        'HmiLabel_Torque_AvgValue
        '
        Me.HmiLabel_Torque_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_AvgValue.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_Torque_AvgValue.Name = "HmiLabel_Torque_AvgValue"
        Me.HmiLabel_Torque_AvgValue.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_AvgValue.TabIndex = 2
        '
        'HmiTextBox_Torque_AvgValue
        '
        Me.HmiTextBox_Torque_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_AvgValue.Location = New System.Drawing.Point(138, 43)
        Me.HmiTextBox_Torque_AvgValue.Name = "HmiTextBox_Torque_AvgValue"
        Me.HmiTextBox_Torque_AvgValue.Number = 0
        Me.HmiTextBox_Torque_AvgValue.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_AvgValue.TabIndex = 3
        Me.HmiTextBox_Torque_AvgValue.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_AvgValue.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Pass
        '
        Me.HmiLabel_Torque_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Pass.Location = New System.Drawing.Point(228, 123)
        Me.HmiLabel_Torque_Pass.Name = "HmiLabel_Torque_Pass"
        Me.HmiLabel_Torque_Pass.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Pass.TabIndex = 31
        '
        'HmiTextBox_Torque_Pass
        '
        Me.HmiTextBox_Torque_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Pass.Location = New System.Drawing.Point(363, 123)
        Me.HmiTextBox_Torque_Pass.Name = "HmiTextBox_Torque_Pass"
        Me.HmiTextBox_Torque_Pass.Number = 0
        Me.HmiTextBox_Torque_Pass.Size = New System.Drawing.Size(87, 14)
        Me.HmiTextBox_Torque_Pass.TabIndex = 32
        Me.HmiTextBox_Torque_Pass.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Pass.ValueType = GetType(String)
        '
        'HmiLabel_Torque_MinValue
        '
        Me.HmiLabel_Torque_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_MinValue.Location = New System.Drawing.Point(228, 23)
        Me.HmiLabel_Torque_MinValue.Name = "HmiLabel_Torque_MinValue"
        Me.HmiLabel_Torque_MinValue.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_MinValue.TabIndex = 33
        '
        'HmiTextBox_Torque_MinValue
        '
        Me.HmiTextBox_Torque_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_MinValue.Location = New System.Drawing.Point(363, 23)
        Me.HmiTextBox_Torque_MinValue.Name = "HmiTextBox_Torque_MinValue"
        Me.HmiTextBox_Torque_MinValue.Number = 0
        Me.HmiTextBox_Torque_MinValue.Size = New System.Drawing.Size(87, 14)
        Me.HmiTextBox_Torque_MinValue.TabIndex = 34
        Me.HmiTextBox_Torque_MinValue.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_MinValue.ValueType = GetType(String)
        '
        'HmiLabel_Torque_Total
        '
        Me.HmiLabel_Torque_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Torque_Total.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_Torque_Total.Name = "HmiLabel_Torque_Total"
        Me.HmiLabel_Torque_Total.Size = New System.Drawing.Size(129, 14)
        Me.HmiLabel_Torque_Total.TabIndex = 35
        '
        'HmiTextBox_Torque_Total
        '
        Me.HmiTextBox_Torque_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Torque_Total.Location = New System.Drawing.Point(138, 123)
        Me.HmiTextBox_Torque_Total.Name = "HmiTextBox_Torque_Total"
        Me.HmiTextBox_Torque_Total.Number = 0
        Me.HmiTextBox_Torque_Total.Size = New System.Drawing.Size(84, 14)
        Me.HmiTextBox_Torque_Total.TabIndex = 36
        Me.HmiTextBox_Torque_Total.TextBoxReadOnly = False
        Me.HmiTextBox_Torque_Total.ValueType = GetType(String)
        '
        'TableLayoutPanel_Torque_Step
        '
        Me.TableLayoutPanel_Torque_Step.ColumnCount = 3
        Me.TableLayoutPanel_Body_Torque_Head.SetColumnSpan(Me.TableLayoutPanel_Torque_Step, 2)
        Me.TableLayoutPanel_Torque_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Torque_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Torque_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Torque_Step.Controls.Add(Me.RadioButton_Torque_Step3, 2, 0)
        Me.TableLayoutPanel_Torque_Step.Controls.Add(Me.RadioButton_Torque_Step2, 1, 0)
        Me.TableLayoutPanel_Torque_Step.Controls.Add(Me.RadioButton_Torque_Step1, 0, 0)
        Me.TableLayoutPanel_Torque_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Torque_Step.Location = New System.Drawing.Point(135, 0)
        Me.TableLayoutPanel_Torque_Step.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Torque_Step.Name = "TableLayoutPanel_Torque_Step"
        Me.TableLayoutPanel_Torque_Step.RowCount = 1
        Me.TableLayoutPanel_Torque_Step.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Torque_Step.Size = New System.Drawing.Size(225, 20)
        Me.TableLayoutPanel_Torque_Step.TabIndex = 38
        '
        'RadioButton_Torque_Step3
        '
        Me.RadioButton_Torque_Step3.AutoSize = True
        Me.RadioButton_Torque_Step3.Checked = True
        Me.RadioButton_Torque_Step3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Torque_Step3.Location = New System.Drawing.Point(153, 3)
        Me.RadioButton_Torque_Step3.Name = "RadioButton_Torque_Step3"
        Me.RadioButton_Torque_Step3.Size = New System.Drawing.Size(69, 14)
        Me.RadioButton_Torque_Step3.TabIndex = 2
        Me.RadioButton_Torque_Step3.TabStop = True
        Me.RadioButton_Torque_Step3.Text = "RadioButton1"
        Me.RadioButton_Torque_Step3.UseVisualStyleBackColor = True
        '
        'RadioButton_Torque_Step2
        '
        Me.RadioButton_Torque_Step2.AutoSize = True
        Me.RadioButton_Torque_Step2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Torque_Step2.Location = New System.Drawing.Point(78, 3)
        Me.RadioButton_Torque_Step2.Name = "RadioButton_Torque_Step2"
        Me.RadioButton_Torque_Step2.Size = New System.Drawing.Size(69, 14)
        Me.RadioButton_Torque_Step2.TabIndex = 1
        Me.RadioButton_Torque_Step2.Text = "RadioButton1"
        Me.RadioButton_Torque_Step2.UseVisualStyleBackColor = True
        '
        'RadioButton_Torque_Step1
        '
        Me.RadioButton_Torque_Step1.AutoSize = True
        Me.RadioButton_Torque_Step1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Torque_Step1.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Torque_Step1.Name = "RadioButton_Torque_Step1"
        Me.RadioButton_Torque_Step1.Size = New System.Drawing.Size(69, 14)
        Me.RadioButton_Torque_Step1.TabIndex = 0
        Me.RadioButton_Torque_Step1.Text = "RadioButton1"
        Me.RadioButton_Torque_Step1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Torque_Bottom
        '
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.Controls.Add(Me.Chart_Torque_Rate, 0, 0)
        Me.TableLayoutPanel_Body_Torque_Bottom.Controls.Add(Me.Chart_Torque_Value, 0, 0)
        Me.TableLayoutPanel_Body_Torque_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Torque_Bottom.Location = New System.Drawing.Point(0, 273)
        Me.TableLayoutPanel_Body_Torque_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Torque_Bottom.Name = "TableLayoutPanel_Body_Torque_Bottom"
        Me.TableLayoutPanel_Body_Torque_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Torque_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Torque_Bottom.Size = New System.Drawing.Size(453, 30)
        Me.TableLayoutPanel_Body_Torque_Bottom.TabIndex = 2
        '
        'Chart_Torque_Rate
        '
        ChartArea1.AxisX.IsMarginVisible = False
        ChartArea1.Name = "ChartArea_Alarm"
        Me.Chart_Torque_Rate.ChartAreas.Add(ChartArea1)
        Me.Chart_Torque_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart_Torque_Rate.Legends.Add(Legend1)
        Me.Chart_Torque_Rate.Location = New System.Drawing.Point(229, 3)
        Me.Chart_Torque_Rate.Name = "Chart_Torque_Rate"
        Me.Chart_Torque_Rate.Size = New System.Drawing.Size(221, 24)
        Me.Chart_Torque_Rate.TabIndex = 5
        Me.Chart_Torque_Rate.Text = "Top Ten Alarm"
        '
        'Chart_Torque_Value
        '
        ChartArea2.AxisX.IsMarginVisible = False
        ChartArea2.Name = "ChartArea_Alarm"
        Me.Chart_Torque_Value.ChartAreas.Add(ChartArea2)
        Me.Chart_Torque_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Name = "Legend1"
        Me.Chart_Torque_Value.Legends.Add(Legend2)
        Me.Chart_Torque_Value.Location = New System.Drawing.Point(3, 3)
        Me.Chart_Torque_Value.Name = "Chart_Torque_Value"
        Me.Chart_Torque_Value.Size = New System.Drawing.Size(220, 24)
        Me.Chart_Torque_Value.TabIndex = 4
        Me.Chart_Torque_Value.Text = "Top Ten Alarm"
        '
        'TabPage_Angle
        '
        Me.TabPage_Angle.Controls.Add(Me.TableLayoutPanel_Body_Mid_Angle)
        Me.TabPage_Angle.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Angle.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Angle.Name = "TabPage_Angle"
        Me.TabPage_Angle.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Angle.Size = New System.Drawing.Size(459, 309)
        Me.TabPage_Angle.TabIndex = 1
        Me.TabPage_Angle.Text = "Angle"
        Me.TabPage_Angle.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid_Angle
        '
        Me.TableLayoutPanel_Body_Mid_Angle.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Angle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Angle.Controls.Add(Me.TableLayoutPanel_Body_Angle_Head, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Angle.Controls.Add(Me.TableLayoutPanel_Body_Angle_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Angle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Angle.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid_Angle.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Angle.Name = "TableLayoutPanel_Body_Mid_Angle"
        Me.TableLayoutPanel_Body_Mid_Angle.RowCount = 2
        Me.TableLayoutPanel_Body_Mid_Angle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 273.0!))
        Me.TableLayoutPanel_Body_Mid_Angle.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid_Angle.Size = New System.Drawing.Size(186, 68)
        Me.TableLayoutPanel_Body_Mid_Angle.TabIndex = 0
        '
        'TableLayoutPanel_Body_Angle_Head
        '
        Me.TableLayoutPanel_Body_Angle_Head.ColumnCount = 4
        Me.TableLayoutPanel_Body_Angle_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Angle_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Angle_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.TableLayoutPanel_Angle_Step, 1, 0)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Step, 0, 0)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Rate, 3, 7)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Rate, 2, 7)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Fail, 1, 7)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Fail, 0, 7)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Cpk, 3, 5)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Cpk, 2, 5)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Cp, 1, 5)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Cp, 0, 5)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Std, 1, 4)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Std, 0, 4)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_UpLimit, 3, 3)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_UpLimit, 2, 3)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_LowLimit, 1, 3)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_LowLimit, 0, 3)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_MaxValue, 0, 1)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_MaxValue, 1, 1)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_AvgValue, 0, 2)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_AvgValue, 1, 2)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Pass, 2, 6)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Pass, 3, 6)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_MinValue, 2, 1)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_MinValue, 3, 1)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiLabel_Angle_Total, 0, 6)
        Me.TableLayoutPanel_Body_Angle_Head.Controls.Add(Me.HmiTextBox_Angle_Total, 1, 6)
        Me.TableLayoutPanel_Body_Angle_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Angle_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Angle_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Angle_Head.Name = "TableLayoutPanel_Body_Angle_Head"
        Me.TableLayoutPanel_Body_Angle_Head.RowCount = 8
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Head.Size = New System.Drawing.Size(186, 273)
        Me.TableLayoutPanel_Body_Angle_Head.TabIndex = 1
        '
        'TableLayoutPanel_Angle_Step
        '
        Me.TableLayoutPanel_Angle_Step.ColumnCount = 3
        Me.TableLayoutPanel_Body_Angle_Head.SetColumnSpan(Me.TableLayoutPanel_Angle_Step, 2)
        Me.TableLayoutPanel_Angle_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Angle_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Angle_Step.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Angle_Step.Controls.Add(Me.RadioButton_Angle_Step3, 2, 0)
        Me.TableLayoutPanel_Angle_Step.Controls.Add(Me.RadioButton_Angle_Step2, 1, 0)
        Me.TableLayoutPanel_Angle_Step.Controls.Add(Me.RadioButton_Angle_Step1, 0, 0)
        Me.TableLayoutPanel_Angle_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Angle_Step.Location = New System.Drawing.Point(55, 0)
        Me.TableLayoutPanel_Angle_Step.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Angle_Step.Name = "TableLayoutPanel_Angle_Step"
        Me.TableLayoutPanel_Angle_Step.RowCount = 1
        Me.TableLayoutPanel_Angle_Step.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Angle_Step.Size = New System.Drawing.Size(92, 20)
        Me.TableLayoutPanel_Angle_Step.TabIndex = 39
        '
        'RadioButton_Angle_Step3
        '
        Me.RadioButton_Angle_Step3.AutoSize = True
        Me.RadioButton_Angle_Step3.Checked = True
        Me.RadioButton_Angle_Step3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Angle_Step3.Location = New System.Drawing.Point(63, 3)
        Me.RadioButton_Angle_Step3.Name = "RadioButton_Angle_Step3"
        Me.RadioButton_Angle_Step3.Size = New System.Drawing.Size(26, 14)
        Me.RadioButton_Angle_Step3.TabIndex = 2
        Me.RadioButton_Angle_Step3.TabStop = True
        Me.RadioButton_Angle_Step3.Text = "RadioButton1"
        Me.RadioButton_Angle_Step3.UseVisualStyleBackColor = True
        '
        'RadioButton_Angle_Step2
        '
        Me.RadioButton_Angle_Step2.AutoSize = True
        Me.RadioButton_Angle_Step2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Angle_Step2.Location = New System.Drawing.Point(33, 3)
        Me.RadioButton_Angle_Step2.Name = "RadioButton_Angle_Step2"
        Me.RadioButton_Angle_Step2.Size = New System.Drawing.Size(24, 14)
        Me.RadioButton_Angle_Step2.TabIndex = 1
        Me.RadioButton_Angle_Step2.Text = "RadioButton1"
        Me.RadioButton_Angle_Step2.UseVisualStyleBackColor = True
        '
        'RadioButton_Angle_Step1
        '
        Me.RadioButton_Angle_Step1.AutoSize = True
        Me.RadioButton_Angle_Step1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Angle_Step1.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Angle_Step1.Name = "RadioButton_Angle_Step1"
        Me.RadioButton_Angle_Step1.Size = New System.Drawing.Size(24, 14)
        Me.RadioButton_Angle_Step1.TabIndex = 0
        Me.RadioButton_Angle_Step1.Text = "RadioButton1"
        Me.RadioButton_Angle_Step1.UseVisualStyleBackColor = True
        '
        'HmiLabel_Angle_Step
        '
        Me.HmiLabel_Angle_Step.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Step.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Angle_Step.Name = "HmiLabel_Angle_Step"
        Me.HmiLabel_Angle_Step.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Step.TabIndex = 38
        '
        'HmiTextBox_Angle_Rate
        '
        Me.HmiTextBox_Angle_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Rate.Location = New System.Drawing.Point(150, 143)
        Me.HmiTextBox_Angle_Rate.Name = "HmiTextBox_Angle_Rate"
        Me.HmiTextBox_Angle_Rate.Number = 0
        Me.HmiTextBox_Angle_Rate.Size = New System.Drawing.Size(33, 127)
        Me.HmiTextBox_Angle_Rate.TabIndex = 24
        Me.HmiTextBox_Angle_Rate.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Rate.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Rate
        '
        Me.HmiLabel_Angle_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Rate.Location = New System.Drawing.Point(95, 143)
        Me.HmiLabel_Angle_Rate.Name = "HmiLabel_Angle_Rate"
        Me.HmiLabel_Angle_Rate.Size = New System.Drawing.Size(49, 127)
        Me.HmiLabel_Angle_Rate.TabIndex = 23
        '
        'HmiTextBox_Angle_Fail
        '
        Me.HmiTextBox_Angle_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Fail.Location = New System.Drawing.Point(58, 143)
        Me.HmiTextBox_Angle_Fail.Name = "HmiTextBox_Angle_Fail"
        Me.HmiTextBox_Angle_Fail.Number = 0
        Me.HmiTextBox_Angle_Fail.Size = New System.Drawing.Size(31, 127)
        Me.HmiTextBox_Angle_Fail.TabIndex = 22
        Me.HmiTextBox_Angle_Fail.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Fail.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Fail
        '
        Me.HmiLabel_Angle_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Fail.Location = New System.Drawing.Point(3, 143)
        Me.HmiLabel_Angle_Fail.Name = "HmiLabel_Angle_Fail"
        Me.HmiLabel_Angle_Fail.Size = New System.Drawing.Size(49, 127)
        Me.HmiLabel_Angle_Fail.TabIndex = 21
        '
        'HmiTextBox_Angle_Cpk
        '
        Me.HmiTextBox_Angle_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Cpk.Location = New System.Drawing.Point(150, 103)
        Me.HmiTextBox_Angle_Cpk.Name = "HmiTextBox_Angle_Cpk"
        Me.HmiTextBox_Angle_Cpk.Number = 0
        Me.HmiTextBox_Angle_Cpk.Size = New System.Drawing.Size(33, 14)
        Me.HmiTextBox_Angle_Cpk.TabIndex = 20
        Me.HmiTextBox_Angle_Cpk.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Cpk.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Cpk
        '
        Me.HmiLabel_Angle_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Cpk.Location = New System.Drawing.Point(95, 103)
        Me.HmiLabel_Angle_Cpk.Name = "HmiLabel_Angle_Cpk"
        Me.HmiLabel_Angle_Cpk.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Cpk.TabIndex = 19
        '
        'HmiTextBox_Angle_Cp
        '
        Me.HmiTextBox_Angle_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Cp.Location = New System.Drawing.Point(58, 103)
        Me.HmiTextBox_Angle_Cp.Name = "HmiTextBox_Angle_Cp"
        Me.HmiTextBox_Angle_Cp.Number = 0
        Me.HmiTextBox_Angle_Cp.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_Cp.TabIndex = 18
        Me.HmiTextBox_Angle_Cp.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Cp.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Cp
        '
        Me.HmiLabel_Angle_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Cp.Location = New System.Drawing.Point(3, 103)
        Me.HmiLabel_Angle_Cp.Name = "HmiLabel_Angle_Cp"
        Me.HmiLabel_Angle_Cp.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Cp.TabIndex = 17
        '
        'HmiTextBox_Angle_Std
        '
        Me.HmiTextBox_Angle_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Std.Location = New System.Drawing.Point(58, 83)
        Me.HmiTextBox_Angle_Std.Name = "HmiTextBox_Angle_Std"
        Me.HmiTextBox_Angle_Std.Number = 0
        Me.HmiTextBox_Angle_Std.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_Std.TabIndex = 14
        Me.HmiTextBox_Angle_Std.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Std.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Std
        '
        Me.HmiLabel_Angle_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Std.Location = New System.Drawing.Point(3, 83)
        Me.HmiLabel_Angle_Std.Name = "HmiLabel_Angle_Std"
        Me.HmiLabel_Angle_Std.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Std.TabIndex = 13
        '
        'HmiTextBox_Angle_UpLimit
        '
        Me.HmiTextBox_Angle_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_UpLimit.Location = New System.Drawing.Point(150, 63)
        Me.HmiTextBox_Angle_UpLimit.Name = "HmiTextBox_Angle_UpLimit"
        Me.HmiTextBox_Angle_UpLimit.Number = 0
        Me.HmiTextBox_Angle_UpLimit.Size = New System.Drawing.Size(33, 14)
        Me.HmiTextBox_Angle_UpLimit.TabIndex = 11
        Me.HmiTextBox_Angle_UpLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_UpLimit.ValueType = GetType(String)
        '
        'HmiLabel_Angle_UpLimit
        '
        Me.HmiLabel_Angle_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_UpLimit.Location = New System.Drawing.Point(95, 63)
        Me.HmiLabel_Angle_UpLimit.Name = "HmiLabel_Angle_UpLimit"
        Me.HmiLabel_Angle_UpLimit.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_UpLimit.TabIndex = 10
        '
        'HmiTextBox_Angle_LowLimit
        '
        Me.HmiTextBox_Angle_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_LowLimit.Location = New System.Drawing.Point(58, 63)
        Me.HmiTextBox_Angle_LowLimit.Name = "HmiTextBox_Angle_LowLimit"
        Me.HmiTextBox_Angle_LowLimit.Number = 0
        Me.HmiTextBox_Angle_LowLimit.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_LowLimit.TabIndex = 9
        Me.HmiTextBox_Angle_LowLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_LowLimit.ValueType = GetType(String)
        '
        'HmiLabel_Angle_LowLimit
        '
        Me.HmiLabel_Angle_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_LowLimit.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_Angle_LowLimit.Name = "HmiLabel_Angle_LowLimit"
        Me.HmiLabel_Angle_LowLimit.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_LowLimit.TabIndex = 8
        '
        'HmiLabel_Angle_MaxValue
        '
        Me.HmiLabel_Angle_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_MaxValue.Location = New System.Drawing.Point(3, 23)
        Me.HmiLabel_Angle_MaxValue.Name = "HmiLabel_Angle_MaxValue"
        Me.HmiLabel_Angle_MaxValue.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_MaxValue.TabIndex = 0
        '
        'HmiTextBox_Angle_MaxValue
        '
        Me.HmiTextBox_Angle_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_MaxValue.Location = New System.Drawing.Point(58, 23)
        Me.HmiTextBox_Angle_MaxValue.Name = "HmiTextBox_Angle_MaxValue"
        Me.HmiTextBox_Angle_MaxValue.Number = 0
        Me.HmiTextBox_Angle_MaxValue.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_MaxValue.TabIndex = 1
        Me.HmiTextBox_Angle_MaxValue.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_MaxValue.ValueType = GetType(String)
        '
        'HmiLabel_Angle_AvgValue
        '
        Me.HmiLabel_Angle_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_AvgValue.Location = New System.Drawing.Point(3, 43)
        Me.HmiLabel_Angle_AvgValue.Name = "HmiLabel_Angle_AvgValue"
        Me.HmiLabel_Angle_AvgValue.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_AvgValue.TabIndex = 2
        '
        'HmiTextBox_Angle_AvgValue
        '
        Me.HmiTextBox_Angle_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_AvgValue.Location = New System.Drawing.Point(58, 43)
        Me.HmiTextBox_Angle_AvgValue.Name = "HmiTextBox_Angle_AvgValue"
        Me.HmiTextBox_Angle_AvgValue.Number = 0
        Me.HmiTextBox_Angle_AvgValue.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_AvgValue.TabIndex = 3
        Me.HmiTextBox_Angle_AvgValue.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_AvgValue.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Pass
        '
        Me.HmiLabel_Angle_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Pass.Location = New System.Drawing.Point(95, 123)
        Me.HmiLabel_Angle_Pass.Name = "HmiLabel_Angle_Pass"
        Me.HmiLabel_Angle_Pass.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Pass.TabIndex = 31
        '
        'HmiTextBox_Angle_Pass
        '
        Me.HmiTextBox_Angle_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Pass.Location = New System.Drawing.Point(150, 123)
        Me.HmiTextBox_Angle_Pass.Name = "HmiTextBox_Angle_Pass"
        Me.HmiTextBox_Angle_Pass.Number = 0
        Me.HmiTextBox_Angle_Pass.Size = New System.Drawing.Size(33, 14)
        Me.HmiTextBox_Angle_Pass.TabIndex = 32
        Me.HmiTextBox_Angle_Pass.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Pass.ValueType = GetType(String)
        '
        'HmiLabel_Angle_MinValue
        '
        Me.HmiLabel_Angle_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_MinValue.Location = New System.Drawing.Point(95, 23)
        Me.HmiLabel_Angle_MinValue.Name = "HmiLabel_Angle_MinValue"
        Me.HmiLabel_Angle_MinValue.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_MinValue.TabIndex = 33
        '
        'HmiTextBox_Angle_MinValue
        '
        Me.HmiTextBox_Angle_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_MinValue.Location = New System.Drawing.Point(150, 23)
        Me.HmiTextBox_Angle_MinValue.Name = "HmiTextBox_Angle_MinValue"
        Me.HmiTextBox_Angle_MinValue.Number = 0
        Me.HmiTextBox_Angle_MinValue.Size = New System.Drawing.Size(33, 14)
        Me.HmiTextBox_Angle_MinValue.TabIndex = 34
        Me.HmiTextBox_Angle_MinValue.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_MinValue.ValueType = GetType(String)
        '
        'HmiLabel_Angle_Total
        '
        Me.HmiLabel_Angle_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Angle_Total.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_Angle_Total.Name = "HmiLabel_Angle_Total"
        Me.HmiLabel_Angle_Total.Size = New System.Drawing.Size(49, 14)
        Me.HmiLabel_Angle_Total.TabIndex = 35
        '
        'HmiTextBox_Angle_Total
        '
        Me.HmiTextBox_Angle_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Angle_Total.Location = New System.Drawing.Point(58, 123)
        Me.HmiTextBox_Angle_Total.Name = "HmiTextBox_Angle_Total"
        Me.HmiTextBox_Angle_Total.Number = 0
        Me.HmiTextBox_Angle_Total.Size = New System.Drawing.Size(31, 14)
        Me.HmiTextBox_Angle_Total.TabIndex = 36
        Me.HmiTextBox_Angle_Total.TextBoxReadOnly = False
        Me.HmiTextBox_Angle_Total.ValueType = GetType(String)
        '
        'TableLayoutPanel_Body_Angle_Bottom
        '
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.Controls.Add(Me.Chart_Angle_Rate, 0, 0)
        Me.TableLayoutPanel_Body_Angle_Bottom.Controls.Add(Me.Chart_Angle_Value, 0, 0)
        Me.TableLayoutPanel_Body_Angle_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Angle_Bottom.Location = New System.Drawing.Point(0, 273)
        Me.TableLayoutPanel_Body_Angle_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Angle_Bottom.Name = "TableLayoutPanel_Body_Angle_Bottom"
        Me.TableLayoutPanel_Body_Angle_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Angle_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Angle_Bottom.Size = New System.Drawing.Size(186, 30)
        Me.TableLayoutPanel_Body_Angle_Bottom.TabIndex = 2
        '
        'Chart_Angle_Rate
        '
        ChartArea3.AxisX.IsMarginVisible = False
        ChartArea3.Name = "ChartArea_Alarm"
        Me.Chart_Angle_Rate.ChartAreas.Add(ChartArea3)
        Me.Chart_Angle_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Legend3.Name = "Legend1"
        Me.Chart_Angle_Rate.Legends.Add(Legend3)
        Me.Chart_Angle_Rate.Location = New System.Drawing.Point(96, 3)
        Me.Chart_Angle_Rate.Name = "Chart_Angle_Rate"
        Me.Chart_Angle_Rate.Size = New System.Drawing.Size(87, 24)
        Me.Chart_Angle_Rate.TabIndex = 6
        Me.Chart_Angle_Rate.Text = "Top Ten Alarm"
        '
        'Chart_Angle_Value
        '
        ChartArea4.AxisX.IsMarginVisible = False
        ChartArea4.Name = "ChartArea_Alarm"
        Me.Chart_Angle_Value.ChartAreas.Add(ChartArea4)
        Me.Chart_Angle_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend4.Name = "Legend1"
        Me.Chart_Angle_Value.Legends.Add(Legend4)
        Me.Chart_Angle_Value.Location = New System.Drawing.Point(3, 3)
        Me.Chart_Angle_Value.Name = "Chart_Angle_Value"
        Me.Chart_Angle_Value.Size = New System.Drawing.Size(87, 24)
        Me.Chart_Angle_Value.TabIndex = 5
        Me.Chart_Angle_Value.Text = "Top Ten Alarm"
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(461, 183)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 7
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0006!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0012!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.0006!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0012!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33213!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33213!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33213!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Seq, 5, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Program, 5, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Result, 5, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Result, 4, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Program, 4, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_AST, 3, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_AST, 2, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Device, 1, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Device, 0, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_PartNumber, 3, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_PartNumber, 2, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_SFC, 0, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Variant, 3, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Variant, 2, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Station, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Station, 1, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Seq, 4, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_SFC, 1, 2)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 4
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 157)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiComboBox_Seq
        '
        Me.HmiComboBox_Seq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Seq.Location = New System.Drawing.Point(332, 81)
        Me.HmiComboBox_Seq.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Seq.Name = "HmiComboBox_Seq"
        Me.HmiComboBox_Seq.Size = New System.Drawing.Size(60, 33)
        Me.HmiComboBox_Seq.TabIndex = 38
        '
        'HmiComboBox_Program
        '
        Me.HmiComboBox_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Program.Location = New System.Drawing.Point(332, 120)
        Me.HmiComboBox_Program.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Program.Name = "HmiComboBox_Program"
        Me.HmiComboBox_Program.Size = New System.Drawing.Size(60, 34)
        Me.HmiComboBox_Program.TabIndex = 37
        '
        'HmiComboBox_Result
        '
        Me.HmiComboBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Result.Location = New System.Drawing.Point(332, 42)
        Me.HmiComboBox_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Result.Name = "HmiComboBox_Result"
        Me.HmiComboBox_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiComboBox_Result.TabIndex = 33
        '
        'HmiLabel_Result
        '
        Me.HmiLabel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result.Location = New System.Drawing.Point(272, 42)
        Me.HmiLabel_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Result.Name = "HmiLabel_Result"
        Me.HmiLabel_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiLabel_Result.TabIndex = 32
        '
        'HmiLabel_Program
        '
        Me.HmiLabel_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Program.Location = New System.Drawing.Point(272, 120)
        Me.HmiLabel_Program.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Program.Name = "HmiLabel_Program"
        Me.HmiLabel_Program.Size = New System.Drawing.Size(60, 34)
        Me.HmiLabel_Program.TabIndex = 30
        '
        'HmiComboBox_AST
        '
        Me.HmiComboBox_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_AST.Location = New System.Drawing.Point(181, 120)
        Me.HmiComboBox_AST.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_AST.Name = "HmiComboBox_AST"
        Me.HmiComboBox_AST.Size = New System.Drawing.Size(91, 34)
        Me.HmiComboBox_AST.TabIndex = 29
        '
        'HmiLabel_AST
        '
        Me.HmiLabel_AST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AST.Location = New System.Drawing.Point(136, 120)
        Me.HmiLabel_AST.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_AST.Name = "HmiLabel_AST"
        Me.HmiLabel_AST.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_AST.TabIndex = 28
        '
        'HmiComboBox_Device
        '
        Me.HmiComboBox_Device.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Device.Location = New System.Drawing.Point(45, 120)
        Me.HmiComboBox_Device.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Device.Name = "HmiComboBox_Device"
        Me.HmiComboBox_Device.Size = New System.Drawing.Size(91, 34)
        Me.HmiComboBox_Device.TabIndex = 27
        '
        'HmiLabel_Device
        '
        Me.HmiLabel_Device.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Device.Location = New System.Drawing.Point(0, 120)
        Me.HmiLabel_Device.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Device.Name = "HmiLabel_Device"
        Me.HmiLabel_Device.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_Device.TabIndex = 26
        '
        'HmiTextBox_PartNumber
        '
        Me.HmiTextBox_PartNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PartNumber.Location = New System.Drawing.Point(181, 81)
        Me.HmiTextBox_PartNumber.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_PartNumber.Name = "HmiTextBox_PartNumber"
        Me.HmiTextBox_PartNumber.Number = 0
        Me.HmiTextBox_PartNumber.Size = New System.Drawing.Size(91, 33)
        Me.HmiTextBox_PartNumber.TabIndex = 19
        Me.HmiTextBox_PartNumber.TextBoxReadOnly = False
        Me.HmiTextBox_PartNumber.ValueType = GetType(String)
        '
        'HmiLabel_PartNumber
        '
        Me.HmiLabel_PartNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PartNumber.Location = New System.Drawing.Point(136, 81)
        Me.HmiLabel_PartNumber.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_PartNumber.Name = "HmiLabel_PartNumber"
        Me.HmiLabel_PartNumber.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_PartNumber.TabIndex = 18
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(0, 81)
        Me.HmiLabel_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_SFC.TabIndex = 16
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(181, 42)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Variant.TabIndex = 14
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(136, 42)
        Me.HmiLabel_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Variant.TabIndex = 13
        '
        'HmiLabel_Station
        '
        Me.HmiLabel_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Station.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Station.Name = "HmiLabel_Station"
        Me.HmiLabel_Station.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Station.TabIndex = 8
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(332, 3)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(60, 33)
        Me.HmiButton_Cancel.TabIndex = 7
        '
        'HmiLabel_StartDate
        '
        Me.HmiLabel_StartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StartDate.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_StartDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_StartDate.Name = "HmiLabel_StartDate"
        Me.HmiLabel_StartDate.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_StartDate.TabIndex = 0
        '
        'HmiLabel_EndDate
        '
        Me.HmiLabel_EndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_EndDate.Location = New System.Drawing.Point(136, 3)
        Me.HmiLabel_EndDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_EndDate.Name = "HmiLabel_EndDate"
        Me.HmiLabel_EndDate.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_EndDate.TabIndex = 2
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(272, 3)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(60, 33)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiDateTime_Start
        '
        Me.HmiDateTime_Start.DateTimeToString = ""
        Me.HmiDateTime_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_Start.Location = New System.Drawing.Point(45, 3)
        Me.HmiDateTime_Start.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_Start.Name = "HmiDateTime_Start"
        Me.HmiDateTime_Start.Size = New System.Drawing.Size(91, 33)
        Me.HmiDateTime_Start.TabIndex = 6
        '
        'HmiDateTime_End
        '
        Me.HmiDateTime_End.DateTimeToString = ""
        Me.HmiDateTime_End.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_End.Location = New System.Drawing.Point(181, 3)
        Me.HmiDateTime_End.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_End.Name = "HmiDateTime_End"
        Me.HmiDateTime_End.Size = New System.Drawing.Size(91, 33)
        Me.HmiDateTime_End.TabIndex = 9
        '
        'HmiButton_Export
        '
        Me.HmiButton_Export.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Export.Location = New System.Drawing.Point(392, 3)
        Me.HmiButton_Export.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Export.MarginHeight = 6
        Me.HmiButton_Export.Name = "HmiButton_Export"
        Me.HmiButton_Export.Size = New System.Drawing.Size(63, 33)
        Me.HmiButton_Export.TabIndex = 10
        '
        'HmiComboBox_Station
        '
        Me.HmiComboBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Station.Location = New System.Drawing.Point(45, 42)
        Me.HmiComboBox_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Station.Name = "HmiComboBox_Station"
        Me.HmiComboBox_Station.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Station.TabIndex = 11
        '
        'HmiLabel_Seq
        '
        Me.HmiLabel_Seq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Seq.Location = New System.Drawing.Point(272, 81)
        Me.HmiLabel_Seq.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Seq.Name = "HmiLabel_Seq"
        Me.HmiLabel_Seq.Size = New System.Drawing.Size(60, 33)
        Me.HmiLabel_Seq.TabIndex = 34
        '
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(45, 81)
        Me.HmiTextBox_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(91, 33)
        Me.HmiTextBox_SFC.TabIndex = 36
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'ContextMenuStrip_Function
        '
        Me.ContextMenuStrip_Function.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.ContextMenuStrip_Function.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Delete})
        Me.ContextMenuStrip_Function.Name = "ContextMenuStrip_Function"
        Me.ContextMenuStrip_Function.Size = New System.Drawing.Size(152, 38)
        '
        'ToolStripMenuItem_Delete
        '
        Me.ToolStripMenuItem_Delete.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem_Delete.Name = "ToolStripMenuItem_Delete"
        Me.ToolStripMenuItem_Delete.Size = New System.Drawing.Size(151, 34)
        Me.ToolStripMenuItem_Delete.Text = "Delete"
        '
        'ChildrenScrewForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenScrewForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScrewForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_Screw.ResumeLayout(False)
        Me.TabPage_Data.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Torque.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Torque.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Torque_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Torque_Step.ResumeLayout(False)
        Me.TableLayoutPanel_Torque_Step.PerformLayout()
        Me.TableLayoutPanel_Body_Torque_Bottom.ResumeLayout(False)
        CType(Me.Chart_Torque_Rate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart_Torque_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Angle.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Angle.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Angle_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Angle_Step.ResumeLayout(False)
        Me.TableLayoutPanel_Angle_Step.PerformLayout()
        Me.TableLayoutPanel_Body_Angle_Bottom.ResumeLayout(False)
        CType(Me.Chart_Angle_Rate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart_Angle_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.ContextMenuStrip_Function.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_StartDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_EndDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiDateTime_Start As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Export As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents HmiComboBox_Station As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Result As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Result As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Program As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_AST As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_AST As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Device As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Device As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PartNumber As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_PartNumber As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Seq As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TabControl_Screw As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Data As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents TabPage_Torque As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid_Torque As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Torque_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_Torque_Rate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Rate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Fail As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Fail As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Cpk As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Cpk As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Cp As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Cp As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Std As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Std As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_UpLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_UpLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_LowLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_LowLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Torque_MaxValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_MaxValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_AvgValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_AvgValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Pass As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Pass As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_MinValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_MinValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Torque_Total As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Torque_Total As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Torque_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabPage_Angle As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid_Angle As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Angle_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_Angle_Rate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Rate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Fail As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Fail As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Cpk As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Cpk As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Cp As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Cp As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Std As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Std As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_UpLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_UpLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_LowLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_LowLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Angle_MaxValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_MaxValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_AvgValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_AvgValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Pass As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Pass As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_MinValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_MinValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Angle_Total As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Angle_Total As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Angle_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Chart_Torque_Rate As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart_Torque_Value As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart_Angle_Rate As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart_Angle_Value As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents HmiComboBox_Seq As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Program As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Torque_Step As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Torque_Step As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Torque_Step3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Torque_Step2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Torque_Step1 As System.Windows.Forms.RadioButton
    Friend WithEvents TableLayoutPanel_Angle_Step As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_Angle_Step3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Angle_Step2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Angle_Step1 As System.Windows.Forms.RadioButton
    Friend WithEvents HmiLabel_Angle_Step As Kochi.HMI.MainControl.UI.HMILabel

End Class
