<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenGapFillerForm
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_GapFiller = New System.Windows.Forms.TabControl()
        Me.TabPage_Data = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.TabPage_Analysis = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid_Gapfiller = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Gapfiller_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_Gapfiller_Rate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Rate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Fail = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Fail = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Cpk = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Cpk = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Cp = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Cp = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Std = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Std = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_UpLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_UpLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_LowLimit = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_LowLimit = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Gapfiller_MaxValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_MaxValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_AvgValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_AvgValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Pass = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Pass = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_MinValue = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_MinValue = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Gapfiller_Total = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Gapfiller_Total = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body_Gapfiller_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Gapfiller_Value = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_Result = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Shot = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Shot = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Component = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDateTime_Start = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiDateTime_End = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiButton_Export = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Component = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.ContextMenuStrip_Function = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogcsv = New System.Windows.Forms.SaveFileDialog()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TabControl_GapFiller.SuspendLayout()
        Me.TabPage_Data.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Analysis.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Gapfiller.SuspendLayout()
        Me.TableLayoutPanel_Body_Gapfiller_Head.SuspendLayout()
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.SuspendLayout()
        CType(Me.Chart_Gapfiller_Value, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_GapFiller, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TabControl_GapFiller
        '
        Me.TabControl_GapFiller.Controls.Add(Me.TabPage_Data)
        Me.TabControl_GapFiller.Controls.Add(Me.TabPage_Analysis)
        Me.TabControl_GapFiller.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_GapFiller.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_GapFiller.Location = New System.Drawing.Point(0, 111)
        Me.TabControl_GapFiller.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_GapFiller.Name = "TabControl_GapFiller"
        Me.TabControl_GapFiller.SelectedIndex = 0
        Me.TabControl_GapFiller.Size = New System.Drawing.Size(467, 419)
        Me.TabControl_GapFiller.TabIndex = 5
        '
        'TabPage_Data
        '
        Me.TabPage_Data.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.TabPage_Data.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Data.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Data.Name = "TabPage_Data"
        Me.TabPage_Data.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Data.Size = New System.Drawing.Size(459, 387)
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
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(453, 381)
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(453, 335)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 335)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(453, 46)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'TabPage_Analysis
        '
        Me.TabPage_Analysis.Controls.Add(Me.TableLayoutPanel_Body_Mid_Gapfiller)
        Me.TabPage_Analysis.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Analysis.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Analysis.Name = "TabPage_Analysis"
        Me.TabPage_Analysis.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Analysis.Size = New System.Drawing.Size(459, 387)
        Me.TabPage_Analysis.TabIndex = 1
        Me.TabPage_Analysis.Text = "Analysis"
        Me.TabPage_Analysis.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid_Gapfiller
        '
        Me.TableLayoutPanel_Body_Mid_Gapfiller.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Gapfiller.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Controls.Add(Me.TableLayoutPanel_Body_Gapfiller_Head, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Controls.Add(Me.TableLayoutPanel_Body_Gapfiller_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Name = "TableLayoutPanel_Body_Mid_Gapfiller"
        Me.TableLayoutPanel_Body_Mid_Gapfiller.RowCount = 2
        Me.TableLayoutPanel_Body_Mid_Gapfiller.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 273.0!))
        Me.TableLayoutPanel_Body_Mid_Gapfiller.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid_Gapfiller.Size = New System.Drawing.Size(453, 381)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.TabIndex = 0
        '
        'TableLayoutPanel_Body_Gapfiller_Head
        '
        Me.TableLayoutPanel_Body_Gapfiller_Head.ColumnCount = 4
        Me.TableLayoutPanel_Body_Gapfiller_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Rate, 3, 6)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Rate, 2, 6)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Fail, 1, 6)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Fail, 0, 6)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Cpk, 3, 4)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Cpk, 2, 4)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Cp, 1, 4)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Cp, 0, 4)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Std, 1, 3)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Std, 0, 3)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_UpLimit, 3, 2)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_UpLimit, 2, 2)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_LowLimit, 1, 2)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_LowLimit, 0, 2)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_MaxValue, 0, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_MaxValue, 1, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_AvgValue, 0, 1)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_AvgValue, 1, 1)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Pass, 2, 5)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Pass, 3, 5)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_MinValue, 2, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_MinValue, 3, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiLabel_Gapfiller_Total, 0, 5)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Controls.Add(Me.HmiTextBox_Gapfiller_Total, 1, 5)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Gapfiller_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Gapfiller_Head.Name = "TableLayoutPanel_Body_Gapfiller_Head"
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowCount = 7
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Head.Size = New System.Drawing.Size(453, 273)
        Me.TableLayoutPanel_Body_Gapfiller_Head.TabIndex = 1
        '
        'HmiTextBox_Gapfiller_Rate
        '
        Me.HmiTextBox_Gapfiller_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Rate.Location = New System.Drawing.Point(364, 239)
        Me.HmiTextBox_Gapfiller_Rate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Gapfiller_Rate.Name = "HmiTextBox_Gapfiller_Rate"
        Me.HmiTextBox_Gapfiller_Rate.Number = 0
        Me.HmiTextBox_Gapfiller_Rate.Size = New System.Drawing.Size(85, 29)
        Me.HmiTextBox_Gapfiller_Rate.TabIndex = 24
        Me.HmiTextBox_Gapfiller_Rate.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Rate.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Rate
        '
        Me.HmiLabel_Gapfiller_Rate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Rate.Location = New System.Drawing.Point(228, 237)
        Me.HmiLabel_Gapfiller_Rate.Name = "HmiLabel_Gapfiller_Rate"
        Me.HmiLabel_Gapfiller_Rate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_Rate.TabIndex = 23
        '
        'HmiTextBox_Gapfiller_Fail
        '
        Me.HmiTextBox_Gapfiller_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Fail.Location = New System.Drawing.Point(139, 239)
        Me.HmiTextBox_Gapfiller_Fail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Gapfiller_Fail.Name = "HmiTextBox_Gapfiller_Fail"
        Me.HmiTextBox_Gapfiller_Fail.Number = 0
        Me.HmiTextBox_Gapfiller_Fail.Size = New System.Drawing.Size(82, 29)
        Me.HmiTextBox_Gapfiller_Fail.TabIndex = 22
        Me.HmiTextBox_Gapfiller_Fail.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Fail.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Fail
        '
        Me.HmiLabel_Gapfiller_Fail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Fail.Location = New System.Drawing.Point(4, 239)
        Me.HmiLabel_Gapfiller_Fail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiLabel_Gapfiller_Fail.Name = "HmiLabel_Gapfiller_Fail"
        Me.HmiLabel_Gapfiller_Fail.Size = New System.Drawing.Size(127, 29)
        Me.HmiLabel_Gapfiller_Fail.TabIndex = 21
        '
        'HmiTextBox_Gapfiller_Cpk
        '
        Me.HmiTextBox_Gapfiller_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Cpk.Location = New System.Drawing.Point(363, 159)
        Me.HmiTextBox_Gapfiller_Cpk.Name = "HmiTextBox_Gapfiller_Cpk"
        Me.HmiTextBox_Gapfiller_Cpk.Number = 0
        Me.HmiTextBox_Gapfiller_Cpk.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_Gapfiller_Cpk.TabIndex = 20
        Me.HmiTextBox_Gapfiller_Cpk.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Cpk.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Cpk
        '
        Me.HmiLabel_Gapfiller_Cpk.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Cpk.Location = New System.Drawing.Point(228, 159)
        Me.HmiLabel_Gapfiller_Cpk.Name = "HmiLabel_Gapfiller_Cpk"
        Me.HmiLabel_Gapfiller_Cpk.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_Cpk.TabIndex = 19
        '
        'HmiTextBox_Gapfiller_Cp
        '
        Me.HmiTextBox_Gapfiller_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Cp.Location = New System.Drawing.Point(138, 159)
        Me.HmiTextBox_Gapfiller_Cp.Name = "HmiTextBox_Gapfiller_Cp"
        Me.HmiTextBox_Gapfiller_Cp.Number = 0
        Me.HmiTextBox_Gapfiller_Cp.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_Gapfiller_Cp.TabIndex = 18
        Me.HmiTextBox_Gapfiller_Cp.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Cp.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Cp
        '
        Me.HmiLabel_Gapfiller_Cp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Cp.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_Gapfiller_Cp.Name = "HmiLabel_Gapfiller_Cp"
        Me.HmiLabel_Gapfiller_Cp.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_Cp.TabIndex = 17
        '
        'HmiTextBox_Gapfiller_Std
        '
        Me.HmiTextBox_Gapfiller_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Std.Location = New System.Drawing.Point(138, 120)
        Me.HmiTextBox_Gapfiller_Std.Name = "HmiTextBox_Gapfiller_Std"
        Me.HmiTextBox_Gapfiller_Std.Number = 0
        Me.HmiTextBox_Gapfiller_Std.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_Gapfiller_Std.TabIndex = 14
        Me.HmiTextBox_Gapfiller_Std.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Std.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Std
        '
        Me.HmiLabel_Gapfiller_Std.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Std.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_Gapfiller_Std.Name = "HmiLabel_Gapfiller_Std"
        Me.HmiLabel_Gapfiller_Std.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_Std.TabIndex = 13
        '
        'HmiTextBox_Gapfiller_UpLimit
        '
        Me.HmiTextBox_Gapfiller_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_UpLimit.Location = New System.Drawing.Point(363, 81)
        Me.HmiTextBox_Gapfiller_UpLimit.Name = "HmiTextBox_Gapfiller_UpLimit"
        Me.HmiTextBox_Gapfiller_UpLimit.Number = 0
        Me.HmiTextBox_Gapfiller_UpLimit.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_Gapfiller_UpLimit.TabIndex = 11
        Me.HmiTextBox_Gapfiller_UpLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_UpLimit.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_UpLimit
        '
        Me.HmiLabel_Gapfiller_UpLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_UpLimit.Location = New System.Drawing.Point(228, 81)
        Me.HmiLabel_Gapfiller_UpLimit.Name = "HmiLabel_Gapfiller_UpLimit"
        Me.HmiLabel_Gapfiller_UpLimit.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_UpLimit.TabIndex = 10
        '
        'HmiTextBox_Gapfiller_LowLimit
        '
        Me.HmiTextBox_Gapfiller_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_LowLimit.Location = New System.Drawing.Point(138, 81)
        Me.HmiTextBox_Gapfiller_LowLimit.Name = "HmiTextBox_Gapfiller_LowLimit"
        Me.HmiTextBox_Gapfiller_LowLimit.Number = 0
        Me.HmiTextBox_Gapfiller_LowLimit.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_Gapfiller_LowLimit.TabIndex = 9
        Me.HmiTextBox_Gapfiller_LowLimit.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_LowLimit.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_LowLimit
        '
        Me.HmiLabel_Gapfiller_LowLimit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_LowLimit.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Gapfiller_LowLimit.Name = "HmiLabel_Gapfiller_LowLimit"
        Me.HmiLabel_Gapfiller_LowLimit.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_LowLimit.TabIndex = 8
        '
        'HmiLabel_Gapfiller_MaxValue
        '
        Me.HmiLabel_Gapfiller_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_MaxValue.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Gapfiller_MaxValue.Name = "HmiLabel_Gapfiller_MaxValue"
        Me.HmiLabel_Gapfiller_MaxValue.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_MaxValue.TabIndex = 0
        '
        'HmiTextBox_Gapfiller_MaxValue
        '
        Me.HmiTextBox_Gapfiller_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_MaxValue.Location = New System.Drawing.Point(138, 3)
        Me.HmiTextBox_Gapfiller_MaxValue.Name = "HmiTextBox_Gapfiller_MaxValue"
        Me.HmiTextBox_Gapfiller_MaxValue.Number = 0
        Me.HmiTextBox_Gapfiller_MaxValue.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_Gapfiller_MaxValue.TabIndex = 1
        Me.HmiTextBox_Gapfiller_MaxValue.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_MaxValue.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_AvgValue
        '
        Me.HmiLabel_Gapfiller_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_AvgValue.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Gapfiller_AvgValue.Name = "HmiLabel_Gapfiller_AvgValue"
        Me.HmiLabel_Gapfiller_AvgValue.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_AvgValue.TabIndex = 2
        '
        'HmiTextBox_Gapfiller_AvgValue
        '
        Me.HmiTextBox_Gapfiller_AvgValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_AvgValue.Location = New System.Drawing.Point(138, 42)
        Me.HmiTextBox_Gapfiller_AvgValue.Name = "HmiTextBox_Gapfiller_AvgValue"
        Me.HmiTextBox_Gapfiller_AvgValue.Number = 0
        Me.HmiTextBox_Gapfiller_AvgValue.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_Gapfiller_AvgValue.TabIndex = 3
        Me.HmiTextBox_Gapfiller_AvgValue.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_AvgValue.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Pass
        '
        Me.HmiLabel_Gapfiller_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Pass.Location = New System.Drawing.Point(229, 200)
        Me.HmiLabel_Gapfiller_Pass.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiLabel_Gapfiller_Pass.Name = "HmiLabel_Gapfiller_Pass"
        Me.HmiLabel_Gapfiller_Pass.Size = New System.Drawing.Size(127, 29)
        Me.HmiLabel_Gapfiller_Pass.TabIndex = 31
        '
        'HmiTextBox_Gapfiller_Pass
        '
        Me.HmiTextBox_Gapfiller_Pass.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Pass.Location = New System.Drawing.Point(364, 200)
        Me.HmiTextBox_Gapfiller_Pass.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Gapfiller_Pass.Name = "HmiTextBox_Gapfiller_Pass"
        Me.HmiTextBox_Gapfiller_Pass.Number = 0
        Me.HmiTextBox_Gapfiller_Pass.Size = New System.Drawing.Size(85, 29)
        Me.HmiTextBox_Gapfiller_Pass.TabIndex = 32
        Me.HmiTextBox_Gapfiller_Pass.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Pass.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_MinValue
        '
        Me.HmiLabel_Gapfiller_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_MinValue.Location = New System.Drawing.Point(228, 3)
        Me.HmiLabel_Gapfiller_MinValue.Name = "HmiLabel_Gapfiller_MinValue"
        Me.HmiLabel_Gapfiller_MinValue.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_Gapfiller_MinValue.TabIndex = 33
        '
        'HmiTextBox_Gapfiller_MinValue
        '
        Me.HmiTextBox_Gapfiller_MinValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_MinValue.Location = New System.Drawing.Point(363, 3)
        Me.HmiTextBox_Gapfiller_MinValue.Name = "HmiTextBox_Gapfiller_MinValue"
        Me.HmiTextBox_Gapfiller_MinValue.Number = 0
        Me.HmiTextBox_Gapfiller_MinValue.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_Gapfiller_MinValue.TabIndex = 34
        Me.HmiTextBox_Gapfiller_MinValue.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_MinValue.ValueType = GetType(String)
        '
        'HmiLabel_Gapfiller_Total
        '
        Me.HmiLabel_Gapfiller_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Gapfiller_Total.Location = New System.Drawing.Point(4, 200)
        Me.HmiLabel_Gapfiller_Total.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiLabel_Gapfiller_Total.Name = "HmiLabel_Gapfiller_Total"
        Me.HmiLabel_Gapfiller_Total.Size = New System.Drawing.Size(127, 29)
        Me.HmiLabel_Gapfiller_Total.TabIndex = 35
        '
        'HmiTextBox_Gapfiller_Total
        '
        Me.HmiTextBox_Gapfiller_Total.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Gapfiller_Total.Location = New System.Drawing.Point(139, 200)
        Me.HmiTextBox_Gapfiller_Total.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Gapfiller_Total.Name = "HmiTextBox_Gapfiller_Total"
        Me.HmiTextBox_Gapfiller_Total.Number = 0
        Me.HmiTextBox_Gapfiller_Total.Size = New System.Drawing.Size(82, 29)
        Me.HmiTextBox_Gapfiller_Total.TabIndex = 36
        Me.HmiTextBox_Gapfiller_Total.TextBoxReadOnly = False
        Me.HmiTextBox_Gapfiller_Total.ValueType = GetType(String)
        '
        'TableLayoutPanel_Body_Gapfiller_Bottom
        '
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Controls.Add(Me.Chart_Gapfiller_Value, 0, 0)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Location = New System.Drawing.Point(0, 273)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Name = "TableLayoutPanel_Body_Gapfiller_Bottom"
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.Size = New System.Drawing.Size(453, 108)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.TabIndex = 2
        '
        'Chart_Gapfiller_Value
        '
        ChartArea2.AxisX.IsMarginVisible = False
        ChartArea2.Name = "ChartArea_Alarm"
        Me.Chart_Gapfiller_Value.ChartAreas.Add(ChartArea2)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.SetColumnSpan(Me.Chart_Gapfiller_Value, 2)
        Me.Chart_Gapfiller_Value.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Name = "Legend1"
        Me.Chart_Gapfiller_Value.Legends.Add(Legend2)
        Me.Chart_Gapfiller_Value.Location = New System.Drawing.Point(3, 3)
        Me.Chart_Gapfiller_Value.Name = "Chart_Gapfiller_Value"
        Me.Chart_Gapfiller_Value.Size = New System.Drawing.Size(447, 102)
        Me.Chart_Gapfiller_Value.TabIndex = 4
        Me.Chart_Gapfiller_Value.Text = "Top Ten Alarm"
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(461, 105)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 7
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.00001!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.99952!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.00001!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00119!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999759!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999759!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999759!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Result, 5, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Result, 4, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Shot, 3, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Shot, 2, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Component, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Component, 1, 1)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(20)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 2
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 79)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiComboBox_Result
        '
        Me.HmiComboBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Result.Location = New System.Drawing.Point(362, 42)
        Me.HmiComboBox_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Result.Name = "HmiComboBox_Result"
        Me.HmiComboBox_Result.Size = New System.Drawing.Size(45, 34)
        Me.HmiComboBox_Result.TabIndex = 33
        '
        'HmiLabel_Result
        '
        Me.HmiLabel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result.Location = New System.Drawing.Point(317, 42)
        Me.HmiLabel_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Result.Name = "HmiLabel_Result"
        Me.HmiLabel_Result.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_Result.TabIndex = 32
        '
        'HmiComboBox_Shot
        '
        Me.HmiComboBox_Shot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Shot.Location = New System.Drawing.Point(226, 42)
        Me.HmiComboBox_Shot.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Shot.Name = "HmiComboBox_Shot"
        Me.HmiComboBox_Shot.Size = New System.Drawing.Size(91, 34)
        Me.HmiComboBox_Shot.TabIndex = 14
        '
        'HmiLabel_Shot
        '
        Me.HmiLabel_Shot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Shot.Location = New System.Drawing.Point(158, 42)
        Me.HmiLabel_Shot.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Shot.Name = "HmiLabel_Shot"
        Me.HmiLabel_Shot.Size = New System.Drawing.Size(68, 34)
        Me.HmiLabel_Shot.TabIndex = 13
        '
        'HmiLabel_Component
        '
        Me.HmiLabel_Component.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Component.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Component.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Component.Name = "HmiLabel_Component"
        Me.HmiLabel_Component.Size = New System.Drawing.Size(68, 34)
        Me.HmiLabel_Component.TabIndex = 8
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(362, 3)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(45, 33)
        Me.HmiButton_Cancel.TabIndex = 7
        '
        'HmiLabel_StartDate
        '
        Me.HmiLabel_StartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StartDate.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_StartDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_StartDate.Name = "HmiLabel_StartDate"
        Me.HmiLabel_StartDate.Size = New System.Drawing.Size(68, 33)
        Me.HmiLabel_StartDate.TabIndex = 0
        '
        'HmiLabel_EndDate
        '
        Me.HmiLabel_EndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_EndDate.Location = New System.Drawing.Point(158, 3)
        Me.HmiLabel_EndDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_EndDate.Name = "HmiLabel_EndDate"
        Me.HmiLabel_EndDate.Size = New System.Drawing.Size(68, 33)
        Me.HmiLabel_EndDate.TabIndex = 2
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(317, 3)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(45, 33)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiDateTime_Start
        '
        Me.HmiDateTime_Start.DateTimeToString = ""
        Me.HmiDateTime_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_Start.Location = New System.Drawing.Point(68, 3)
        Me.HmiDateTime_Start.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_Start.Name = "HmiDateTime_Start"
        Me.HmiDateTime_Start.Size = New System.Drawing.Size(90, 33)
        Me.HmiDateTime_Start.TabIndex = 6
        '
        'HmiDateTime_End
        '
        Me.HmiDateTime_End.DateTimeToString = ""
        Me.HmiDateTime_End.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_End.Location = New System.Drawing.Point(226, 3)
        Me.HmiDateTime_End.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_End.Name = "HmiDateTime_End"
        Me.HmiDateTime_End.Size = New System.Drawing.Size(91, 33)
        Me.HmiDateTime_End.TabIndex = 9
        '
        'HmiButton_Export
        '
        Me.HmiButton_Export.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Export.Location = New System.Drawing.Point(407, 3)
        Me.HmiButton_Export.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Export.MarginHeight = 6
        Me.HmiButton_Export.Name = "HmiButton_Export"
        Me.HmiButton_Export.Size = New System.Drawing.Size(48, 33)
        Me.HmiButton_Export.TabIndex = 10
        '
        'HmiComboBox_Component
        '
        Me.HmiComboBox_Component.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Component.Location = New System.Drawing.Point(68, 42)
        Me.HmiComboBox_Component.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Component.Name = "HmiComboBox_Component"
        Me.HmiComboBox_Component.Size = New System.Drawing.Size(90, 34)
        Me.HmiComboBox_Component.TabIndex = 11
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
        'ChildrenGapFillerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenGapFillerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScrewForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_GapFiller.ResumeLayout(False)
        Me.TabPage_Data.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Analysis.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Gapfiller.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Gapfiller_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Gapfiller_Bottom.ResumeLayout(False)
        CType(Me.Chart_Gapfiller_Value, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents HmiLabel_Component As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Export As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents HmiComboBox_Component As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Result As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Result As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Shot As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Shot As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TabControl_GapFiller As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Data As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents TabPage_Analysis As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid_Gapfiller As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Gapfiller_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Gapfiller_Rate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_Cpk As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_Cpk As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_Cp As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_Cp As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_Std As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_Std As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_UpLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_UpLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_LowLimit As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_LowLimit As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Gapfiller_MaxValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_MaxValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_AvgValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_AvgValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_MinValue As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_MinValue As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Gapfiller_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Chart_Gapfiller_Value As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents HmiTextBox_Gapfiller_Rate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Gapfiller_Fail As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_Fail As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Gapfiller_Pass As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_Pass As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Gapfiller_Total As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Gapfiller_Total As Kochi.HMI.MainControl.UI.HMITextBox

End Class
