<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenProductionForm
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_Data = New System.Windows.Forms.TabControl()
        Me.TabPage_Data = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.TabPage_Analysis = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid_Analysis = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Production = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.HmiDataViewPage_Analysis = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.HmiDataView_Analysis = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_CarrierId = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_CarrierId = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDateTime_Start = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiDateTime_End = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiButton_Export = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Station = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Result = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.ContextMenuStrip_Function = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogcsv = New System.Windows.Forms.SaveFileDialog()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabControl_Data.SuspendLayout()
        Me.TabPage_Data.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Analysis.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Analysis.SuspendLayout()
        CType(Me.Chart_Production, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HmiDataView_Analysis, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_Data, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabControl_Data
        '
        Me.TabControl_Data.Controls.Add(Me.TabPage_Data)
        Me.TabControl_Data.Controls.Add(Me.TabPage_Analysis)
        Me.TabControl_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Data.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_Data.Location = New System.Drawing.Point(0, 150)
        Me.TabControl_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Data.Name = "TabControl_Data"
        Me.TabControl_Data.SelectedIndex = 0
        Me.TabControl_Data.Size = New System.Drawing.Size(467, 380)
        Me.TabControl_Data.TabIndex = 5
        '
        'TabPage_Data
        '
        Me.TabPage_Data.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.TabPage_Data.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Data.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Data.Name = "TabPage_Data"
        Me.TabPage_Data.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Data.Size = New System.Drawing.Size(459, 348)
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
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(453, 342)
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(453, 314)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 314)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(453, 28)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'TabPage_Analysis
        '
        Me.TabPage_Analysis.Controls.Add(Me.TableLayoutPanel_Body_Mid_Analysis)
        Me.TabPage_Analysis.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Analysis.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Analysis.Name = "TabPage_Analysis"
        Me.TabPage_Analysis.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Analysis.Size = New System.Drawing.Size(459, 348)
        Me.TabPage_Analysis.TabIndex = 1
        Me.TabPage_Analysis.Text = "Analysis"
        Me.TabPage_Analysis.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid_Analysis
        '
        Me.TableLayoutPanel_Body_Mid_Analysis.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Analysis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.Controls.Add(Me.Chart_Production, 0, 2)
        Me.TableLayoutPanel_Body_Mid_Analysis.Controls.Add(Me.HmiDataViewPage_Analysis, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Analysis.Controls.Add(Me.HmiDataView_Analysis, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Analysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Analysis.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid_Analysis.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Analysis.Name = "TableLayoutPanel_Body_Mid_Analysis"
        Me.TableLayoutPanel_Body_Mid_Analysis.RowCount = 3
        Me.TableLayoutPanel_Body_Mid_Analysis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.Size = New System.Drawing.Size(453, 342)
        Me.TableLayoutPanel_Body_Mid_Analysis.TabIndex = 0
        '
        'Chart_Production
        '
        ChartArea1.AxisX.IsMarginVisible = False
        ChartArea1.Name = "ChartArea_Alarm"
        Me.Chart_Production.ChartAreas.Add(ChartArea1)
        Me.Chart_Production.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart_Production.Legends.Add(Legend1)
        Me.Chart_Production.Location = New System.Drawing.Point(3, 207)
        Me.Chart_Production.Name = "Chart_Production"
        Me.Chart_Production.Size = New System.Drawing.Size(447, 132)
        Me.Chart_Production.TabIndex = 4
        Me.Chart_Production.Text = "Production"
        '
        'HmiDataViewPage_Analysis
        '
        Me.HmiDataViewPage_Analysis.Button_DownEnable = False
        Me.HmiDataViewPage_Analysis.Button_DownLastEnable = False
        Me.HmiDataViewPage_Analysis.Button_GoEnable = False
        Me.HmiDataViewPage_Analysis.Button_UpEnable = False
        Me.HmiDataViewPage_Analysis.Button_UpFirstEnable = False
        Me.HmiDataViewPage_Analysis.CurrentPage = 0
        Me.HmiDataViewPage_Analysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataViewPage_Analysis.Location = New System.Drawing.Point(0, 177)
        Me.HmiDataViewPage_Analysis.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Analysis.Name = "HmiDataViewPage_Analysis"
        Me.HmiDataViewPage_Analysis.Size = New System.Drawing.Size(453, 27)
        Me.HmiDataViewPage_Analysis.TabIndex = 2
        Me.HmiDataViewPage_Analysis.TotallPage = 0
        Me.HmiDataViewPage_Analysis.TotalRecord = 0
        '
        'HmiDataView_Analysis
        '
        Me.HmiDataView_Analysis.AllowUserToAddRows = False
        Me.HmiDataView_Analysis.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Analysis.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.HmiDataView_Analysis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Analysis.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Analysis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Analysis.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Analysis.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Analysis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HmiDataView_Analysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Analysis.EnableHeadersVisualStyles = False
        Me.HmiDataView_Analysis.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Analysis.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Analysis.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataView_Analysis.Name = "HmiDataView_Analysis"
        Me.HmiDataView_Analysis.ReadOnly = True
        Me.HmiDataView_Analysis.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Analysis.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.HmiDataView_Analysis.RowTemplate.Height = 40
        Me.HmiDataView_Analysis.RowTemplate.ReadOnly = True
        Me.HmiDataView_Analysis.Size = New System.Drawing.Size(453, 177)
        Me.HmiDataView_Analysis.TabIndex = 1
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
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_CarrierId, 3, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_CarrierId, 2, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Station, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_SFC, 0, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_SFC, 1, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Variant, 2, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Station, 1, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Result, 4, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Result, 5, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Variant, 3, 1)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 3
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 118)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiComboBox_CarrierId
        '
        Me.HmiComboBox_CarrierId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CarrierId.Location = New System.Drawing.Point(181, 81)
        Me.HmiComboBox_CarrierId.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_CarrierId.Name = "HmiComboBox_CarrierId"
        Me.HmiComboBox_CarrierId.Size = New System.Drawing.Size(91, 34)
        Me.HmiComboBox_CarrierId.TabIndex = 19
        '
        'HmiLabel_CarrierId
        '
        Me.HmiLabel_CarrierId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CarrierId.Location = New System.Drawing.Point(136, 81)
        Me.HmiLabel_CarrierId.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_CarrierId.Name = "HmiLabel_CarrierId"
        Me.HmiLabel_CarrierId.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_CarrierId.TabIndex = 18
        '
        'HmiLabel_Station
        '
        Me.HmiLabel_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Station.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Station.Name = "HmiLabel_Station"
        Me.HmiLabel_Station.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Station.TabIndex = 13
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
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(45, 81)
        Me.HmiTextBox_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(91, 34)
        Me.HmiTextBox_SFC.TabIndex = 3
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
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
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(136, 42)
        Me.HmiLabel_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiComboBox_Station
        '
        Me.HmiComboBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Station.Location = New System.Drawing.Point(45, 42)
        Me.HmiComboBox_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Station.Name = "HmiComboBox_Station"
        Me.HmiComboBox_Station.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Station.TabIndex = 14
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
        Me.HmiComboBox_Result.Location = New System.Drawing.Point(332, 42)
        Me.HmiComboBox_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Result.Name = "HmiComboBox_Result"
        Me.HmiComboBox_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiComboBox_Result.TabIndex = 16
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(181, 42)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(91, 33)
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
        'ChildrenProductionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenProductionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ProductionForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_Data.ResumeLayout(False)
        Me.TabPage_Data.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Analysis.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Analysis.ResumeLayout(False)
        CType(Me.Chart_Production, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HmiDataView_Analysis, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Export As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TabControl_Data As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Data As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents TabPage_Analysis As System.Windows.Forms.TabPage
    Friend WithEvents HmiLabel_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Station As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Result As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Result As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Body_Mid_Analysis As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataViewPage_Analysis As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiDataView_Analysis As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents Chart_Production As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents HmiComboBox_CarrierId As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_CarrierId As Kochi.HMI.MainControl.UI.HMILabel
End Class
