Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProductionDataView
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ProductionDataView))
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_Data = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kostal.Las.Base.HMIDataView()
        Me.HmiDataViewPage_Data = New Kostal.Las.Base.HMIDataViewPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Statistics = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Products_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel_Statistics_Right = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Statistics_ByDay = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Statistics_ByShift = New System.Windows.Forms.RadioButton()
        Me.RowMergeView_Statistics = New Kostal.Las.Base.RowMergeView(Me.components)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_WT = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_WT_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_WT_ByDay = New System.Windows.Forms.RadioButton()
        Me.RadioButton_WT_ByShift = New System.Windows.Forms.RadioButton()
        Me.RowMergeView_WT = New Kostal.Las.Base.RowMergeView(Me.components)
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_ErrorStation = New Kostal.Las.Base.HMIComboBox()
        Me.HmiLabel_ErrorStation = New Kostal.Las.Base.HMILabel()
        Me.HmiTextBox_TestStep = New Kostal.Las.Base.HMITextBox()
        Me.HmiLabel_TestStep = New Kostal.Las.Base.HMILabel()
        Me.HmiComboBox_CarrierId = New Kostal.Las.Base.HMIComboBox()
        Me.HmiLabel_CarrierId = New Kostal.Las.Base.HMILabel()
        Me.HmiLabel_SFC = New Kostal.Las.Base.HMILabel()
        Me.HmiButton_Cancel = New Kostal.Las.Base.HMIButton()
        Me.HmiLabel_StartDate = New Kostal.Las.Base.HMILabel()
        Me.HmiLabel_EndDate = New Kostal.Las.Base.HMILabel()
        Me.HmiTextBox_SFC = New Kostal.Las.Base.HMITextBox()
        Me.HmiButton_Search = New Kostal.Las.Base.HMIButton()
        Me.HmiButton_Export = New Kostal.Las.Base.HMIButton()
        Me.HmiLabel_Variant = New Kostal.Las.Base.HMILabel()
        Me.HmiLabel_Result = New Kostal.Las.Base.HMILabel()
        Me.HmiComboBox_Result = New Kostal.Las.Base.HMIComboBox()
        Me.HmiComboBox_Variant = New Kostal.Las.Base.HMIComboBox()
        Me.ContextMenuStrip_Function = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogcsv = New System.Windows.Forms.SaveFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabControl_Data.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TableLayoutPanel_Statistics.SuspendLayout()
        CType(Me.Chart_Products_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel_Statistics_Right.SuspendLayout()
        CType(Me.RowMergeView_Statistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TableLayoutPanel_WT.SuspendLayout()
        CType(Me.Chart_WT_Value, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.RowMergeView_WT, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Panel_Body.Size = New System.Drawing.Size(467, 331)
        Me.Panel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_Data, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 331)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabControl_Data
        '
        Me.TabControl_Data.Controls.Add(Me.TabPage1)
        Me.TabControl_Data.Controls.Add(Me.TabPage2)
        Me.TabControl_Data.Controls.Add(Me.TabPage3)
        Me.TabControl_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Data.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_Data.Location = New System.Drawing.Point(0, 150)
        Me.TabControl_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Data.Name = "TabControl_Data"
        Me.TabControl_Data.SelectedIndex = 0
        Me.TabControl_Data.Size = New System.Drawing.Size(467, 181)
        Me.TabControl_Data.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage1.Location = New System.Drawing.Point(4, 28)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(459, 149)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(453, 143)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 1
        '
        'HmiDataView_Data
        '
        Me.HmiDataView_Data.AllowUserToAddRows = False
        Me.HmiDataView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Data.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HmiDataView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Data.EnableHeadersVisualStyles = False
        Me.HmiDataView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Data.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataView_Data.Name = "HmiDataView_Data"
        Me.HmiDataView_Data.ReadOnly = True
        Me.HmiDataView_Data.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.HmiDataView_Data.RowTemplate.Height = 40
        Me.HmiDataView_Data.RowTemplate.ReadOnly = True
        Me.HmiDataView_Data.Size = New System.Drawing.Size(453, 131)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 131)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(453, 12)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel_Statistics)
        Me.TabPage2.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(459, 149)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Statistics
        '
        Me.TableLayoutPanel_Statistics.ColumnCount = 2
        Me.TableLayoutPanel_Statistics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.61148!))
        Me.TableLayoutPanel_Statistics.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.388521!))
        Me.TableLayoutPanel_Statistics.Controls.Add(Me.Chart_Products_Value, 0, 1)
        Me.TableLayoutPanel_Statistics.Controls.Add(Me.TableLayoutPanel_Statistics_Right, 1, 0)
        Me.TableLayoutPanel_Statistics.Controls.Add(Me.RowMergeView_Statistics, 0, 0)
        Me.TableLayoutPanel_Statistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Statistics.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Statistics.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Statistics.Name = "TableLayoutPanel_Statistics"
        Me.TableLayoutPanel_Statistics.RowCount = 2
        Me.TableLayoutPanel_Statistics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Statistics.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Statistics.Size = New System.Drawing.Size(453, 143)
        Me.TableLayoutPanel_Statistics.TabIndex = 0
        '
        'Chart_Products_Value
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart_Products_Value.ChartAreas.Add(ChartArea3)
        Me.Chart_Products_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend3.Enabled = False
        Legend3.Name = "Legend1"
        Me.Chart_Products_Value.Legends.Add(Legend3)
        Me.Chart_Products_Value.Location = New System.Drawing.Point(3, 88)
        Me.Chart_Products_Value.Name = "Chart_Products_Value"
        Series3.ChartArea = "ChartArea1"
        Series3.Legend = "Legend1"
        Series3.Name = "Series1"
        Me.Chart_Products_Value.Series.Add(Series3)
        Me.Chart_Products_Value.Size = New System.Drawing.Size(409, 52)
        Me.Chart_Products_Value.TabIndex = 2
        Me.Chart_Products_Value.Text = "Chart1"
        '
        'TableLayoutPanel_Statistics_Right
        '
        Me.TableLayoutPanel_Statistics_Right.ColumnCount = 1
        Me.TableLayoutPanel_Statistics_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Statistics_Right.Controls.Add(Me.RadioButton_Statistics_ByDay, 0, 0)
        Me.TableLayoutPanel_Statistics_Right.Controls.Add(Me.RadioButton_Statistics_ByShift, 0, 1)
        Me.TableLayoutPanel_Statistics_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Statistics_Right.Location = New System.Drawing.Point(415, 0)
        Me.TableLayoutPanel_Statistics_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Statistics_Right.Name = "TableLayoutPanel_Statistics_Right"
        Me.TableLayoutPanel_Statistics_Right.RowCount = 3
        Me.TableLayoutPanel_Statistics_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Statistics_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Statistics_Right.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Statistics_Right.Size = New System.Drawing.Size(38, 85)
        Me.TableLayoutPanel_Statistics_Right.TabIndex = 0
        '
        'RadioButton_Statistics_ByDay
        '
        Me.RadioButton_Statistics_ByDay.AutoSize = True
        Me.RadioButton_Statistics_ByDay.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Statistics_ByDay.Name = "RadioButton_Statistics_ByDay"
        Me.RadioButton_Statistics_ByDay.Size = New System.Drawing.Size(32, 23)
        Me.RadioButton_Statistics_ByDay.TabIndex = 0
        Me.RadioButton_Statistics_ByDay.TabStop = True
        Me.RadioButton_Statistics_ByDay.Text = "ByDay"
        Me.RadioButton_Statistics_ByDay.UseVisualStyleBackColor = True
        '
        'RadioButton_Statistics_ByShift
        '
        Me.RadioButton_Statistics_ByShift.AutoSize = True
        Me.RadioButton_Statistics_ByShift.Location = New System.Drawing.Point(3, 42)
        Me.RadioButton_Statistics_ByShift.Name = "RadioButton_Statistics_ByShift"
        Me.RadioButton_Statistics_ByShift.Size = New System.Drawing.Size(32, 23)
        Me.RadioButton_Statistics_ByShift.TabIndex = 1
        Me.RadioButton_Statistics_ByShift.TabStop = True
        Me.RadioButton_Statistics_ByShift.Text = "ByShift"
        Me.RadioButton_Statistics_ByShift.UseVisualStyleBackColor = True
        '
        'RowMergeView_Statistics
        '
        Me.RowMergeView_Statistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RowMergeView_Statistics.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RowMergeView_Statistics.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.RowMergeView_Statistics.Location = New System.Drawing.Point(3, 3)
        Me.RowMergeView_Statistics.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control
        Me.RowMergeView_Statistics.MergeColumnNames = CType(resources.GetObject("RowMergeView_Statistics.MergeColumnNames"), System.Collections.Generic.List(Of String))
        Me.RowMergeView_Statistics.Name = "RowMergeView_Statistics"
        Me.RowMergeView_Statistics.RowTemplate.Height = 23
        Me.RowMergeView_Statistics.Size = New System.Drawing.Size(409, 79)
        Me.RowMergeView_Statistics.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TableLayoutPanel_WT)
        Me.TabPage3.Location = New System.Drawing.Point(4, 28)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(459, 149)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_WT
        '
        Me.TableLayoutPanel_WT.ColumnCount = 2
        Me.TableLayoutPanel_WT.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.61148!))
        Me.TableLayoutPanel_WT.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.388521!))
        Me.TableLayoutPanel_WT.Controls.Add(Me.Chart_WT_Value, 0, 1)
        Me.TableLayoutPanel_WT.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel_WT.Controls.Add(Me.RowMergeView_WT, 0, 0)
        Me.TableLayoutPanel_WT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_WT.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_WT.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_WT.Name = "TableLayoutPanel_WT"
        Me.TableLayoutPanel_WT.RowCount = 2
        Me.TableLayoutPanel_WT.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_WT.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_WT.Size = New System.Drawing.Size(459, 149)
        Me.TableLayoutPanel_WT.TabIndex = 1
        '
        'Chart_WT_Value
        '
        ChartArea4.Name = "ChartArea1"
        Me.Chart_WT_Value.ChartAreas.Add(ChartArea4)
        Me.Chart_WT_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend4.Enabled = False
        Legend4.Name = "Legend1"
        Me.Chart_WT_Value.Legends.Add(Legend4)
        Me.Chart_WT_Value.Location = New System.Drawing.Point(3, 92)
        Me.Chart_WT_Value.Name = "Chart_WT_Value"
        Series4.ChartArea = "ChartArea1"
        Series4.Legend = "Legend1"
        Series4.Name = "Series1"
        Me.Chart_WT_Value.Series.Add(Series4)
        Me.Chart_WT_Value.Size = New System.Drawing.Size(414, 54)
        Me.Chart_WT_Value.TabIndex = 2
        Me.Chart_WT_Value.Text = "Chart1"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.RadioButton_WT_ByDay, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.RadioButton_WT_ByShift, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(420, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(39, 89)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'RadioButton_WT_ByDay
        '
        Me.RadioButton_WT_ByDay.AutoSize = True
        Me.RadioButton_WT_ByDay.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_WT_ByDay.Name = "RadioButton_WT_ByDay"
        Me.RadioButton_WT_ByDay.Size = New System.Drawing.Size(33, 23)
        Me.RadioButton_WT_ByDay.TabIndex = 0
        Me.RadioButton_WT_ByDay.TabStop = True
        Me.RadioButton_WT_ByDay.Text = "ByDay"
        Me.RadioButton_WT_ByDay.UseVisualStyleBackColor = True
        '
        'RadioButton_WT_ByShift
        '
        Me.RadioButton_WT_ByShift.AutoSize = True
        Me.RadioButton_WT_ByShift.Location = New System.Drawing.Point(3, 42)
        Me.RadioButton_WT_ByShift.Name = "RadioButton_WT_ByShift"
        Me.RadioButton_WT_ByShift.Size = New System.Drawing.Size(33, 23)
        Me.RadioButton_WT_ByShift.TabIndex = 1
        Me.RadioButton_WT_ByShift.TabStop = True
        Me.RadioButton_WT_ByShift.Text = "ByShift"
        Me.RadioButton_WT_ByShift.UseVisualStyleBackColor = True
        '
        'RowMergeView_WT
        '
        Me.RowMergeView_WT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.RowMergeView_WT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RowMergeView_WT.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.RowMergeView_WT.Location = New System.Drawing.Point(3, 3)
        Me.RowMergeView_WT.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control
        Me.RowMergeView_WT.MergeColumnNames = CType(resources.GetObject("RowMergeView_WT.MergeColumnNames"), System.Collections.Generic.List(Of String))
        Me.RowMergeView_WT.Name = "RowMergeView_WT"
        Me.RowMergeView_WT.RowTemplate.Height = 23
        Me.RowMergeView_WT.Size = New System.Drawing.Size(414, 83)
        Me.RowMergeView_WT.TabIndex = 1
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(461, 144)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 7
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00115!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0023!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00115!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0023!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33154!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33154!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33002!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_ErrorStation, 5, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_ErrorStation, 4, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_TestStep, 3, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_TestStep, 2, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_CarrierId, 3, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_CarrierId, 2, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_SFC, 0, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_SFC, 1, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Variant, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Result, 4, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Result, 5, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Variant, 1, 1)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 3
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 118)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiComboBox_ErrorStation
        '
        Me.HmiComboBox_ErrorStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ErrorStation.Location = New System.Drawing.Point(333, 81)
        Me.HmiComboBox_ErrorStation.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.HmiComboBox_ErrorStation.Name = "HmiComboBox_ErrorStation"
        Me.HmiComboBox_ErrorStation.Size = New System.Drawing.Size(56, 34)
        Me.HmiComboBox_ErrorStation.TabIndex = 26
        '
        'HmiLabel_ErrorStation
        '
        Me.HmiLabel_ErrorStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ErrorStation.Location = New System.Drawing.Point(272, 81)
        Me.HmiLabel_ErrorStation.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_ErrorStation.Name = "HmiLabel_ErrorStation"
        Me.HmiLabel_ErrorStation.Size = New System.Drawing.Size(60, 34)
        Me.HmiLabel_ErrorStation.TabIndex = 25
        '
        'HmiTextBox_TestStep
        '
        Me.HmiTextBox_TestStep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TestStep.Location = New System.Drawing.Point(184, 81)
        Me.HmiTextBox_TestStep.Name = "HmiTextBox_TestStep"
        Me.HmiTextBox_TestStep.Number = 0
        Me.HmiTextBox_TestStep.Size = New System.Drawing.Size(85, 34)
        Me.HmiTextBox_TestStep.TabIndex = 24
        Me.HmiTextBox_TestStep.TextBoxReadOnly = False
        Me.HmiTextBox_TestStep.ValueType = GetType(String)
        '
        'HmiLabel_TestStep
        '
        Me.HmiLabel_TestStep.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TestStep.Location = New System.Drawing.Point(136, 81)
        Me.HmiLabel_TestStep.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_TestStep.Name = "HmiLabel_TestStep"
        Me.HmiLabel_TestStep.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_TestStep.TabIndex = 23
        '
        'HmiComboBox_CarrierId
        '
        Me.HmiComboBox_CarrierId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CarrierId.Location = New System.Drawing.Point(184, 40)
        Me.HmiComboBox_CarrierId.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.HmiComboBox_CarrierId.Name = "HmiComboBox_CarrierId"
        Me.HmiComboBox_CarrierId.Size = New System.Drawing.Size(85, 35)
        Me.HmiComboBox_CarrierId.TabIndex = 19
        '
        'HmiLabel_CarrierId
        '
        Me.HmiLabel_CarrierId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CarrierId.Location = New System.Drawing.Point(136, 42)
        Me.HmiLabel_CarrierId.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_CarrierId.Name = "HmiLabel_CarrierId"
        Me.HmiLabel_CarrierId.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_CarrierId.TabIndex = 18
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(0, 81)
        Me.HmiLabel_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_SFC.TabIndex = 8
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(335, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(54, 33)
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
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(48, 81)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(85, 34)
        Me.HmiTextBox_SFC.TabIndex = 3
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(275, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(54, 33)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiButton_Export
        '
        Me.HmiButton_Export.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Export.Location = New System.Drawing.Point(395, 3)
        Me.HmiButton_Export.MarginHeight = 6
        Me.HmiButton_Export.Name = "HmiButton_Export"
        Me.HmiButton_Export.Size = New System.Drawing.Size(57, 33)
        Me.HmiButton_Export.TabIndex = 10
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiLabel_Result
        '
        Me.HmiLabel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result.Location = New System.Drawing.Point(272, 42)
        Me.HmiLabel_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Result.Name = "HmiLabel_Result"
        Me.HmiLabel_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiLabel_Result.TabIndex = 15
        '
        'HmiComboBox_Result
        '
        Me.HmiComboBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Result.Location = New System.Drawing.Point(335, 40)
        Me.HmiComboBox_Result.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.HmiComboBox_Result.Name = "HmiComboBox_Result"
        Me.HmiComboBox_Result.Size = New System.Drawing.Size(54, 35)
        Me.HmiComboBox_Result.TabIndex = 16
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(48, 40)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(85, 35)
        Me.HmiComboBox_Variant.TabIndex = 17
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
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'ProductionDataView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 331)
        Me.Controls.Add(Me.Panel_Body)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ProductionDataView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ProductionForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_Data.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TableLayoutPanel_Statistics.ResumeLayout(False)
        CType(Me.Chart_Products_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel_Statistics_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Statistics_Right.PerformLayout()
        CType(Me.RowMergeView_Statistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TableLayoutPanel_WT.ResumeLayout(False)
        CType(Me.Chart_WT_Value, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.RowMergeView_WT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.ContextMenuStrip_Function.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_StartDate As HMILabel
    Friend WithEvents HmiLabel_EndDate As HMILabel
    Friend WithEvents HmiButton_Search As HMIButton
    Friend WithEvents HmiButton_Cancel As HMIButton
    Friend WithEvents HmiLabel_SFC As HMILabel
    Friend WithEvents HmiTextBox_SFC As HMITextBox
    Friend WithEvents HmiButton_Export As HMIButton
    Friend WithEvents HmiLabel_Variant As HMILabel
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TabControl_Data As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As HMIDataView
    Friend WithEvents HmiDataViewPage_Data As HMIDataViewPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents HmiLabel_Result As HMILabel
    Friend WithEvents HmiComboBox_Result As HMIComboBox
    Friend WithEvents HmiComboBox_Variant As HMIComboBox
    Friend WithEvents HmiComboBox_CarrierId As HMIComboBox
    Friend WithEvents HmiLabel_CarrierId As HMILabel
    Friend WithEvents HmiTextBox_TestStep As HMITextBox
    Friend WithEvents HmiLabel_TestStep As HMILabel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TableLayoutPanel_Statistics As TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Statistics_Right As TableLayoutPanel
    Friend WithEvents RadioButton_Statistics_ByDay As RadioButton
    Friend WithEvents RadioButton_Statistics_ByShift As RadioButton
    Friend WithEvents RowMergeView_Statistics As Base.RowMergeView
    Friend WithEvents Chart_Products_Value As DataVisualization.Charting.Chart
    Friend WithEvents Timer1 As Timer
    Friend WithEvents TableLayoutPanel_WT As TableLayoutPanel
    Friend WithEvents Chart_WT_Value As DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents RadioButton_WT_ByDay As RadioButton
    Friend WithEvents RadioButton_WT_ByShift As RadioButton
    Friend WithEvents RowMergeView_WT As Base.RowMergeView
    Friend WithEvents HmiComboBox_ErrorStation As HMIComboBox
    Friend WithEvents HmiLabel_ErrorStation As HMILabel
End Class
