Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenMachineForm
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TabControl_Data = New System.Windows.Forms.TabControl()
        Me.TabPage_Data = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.TabPage_Analysis = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Mid_Analysis = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Analysis_Head = New HMITableLayoutPanel()
        Me.HmiTextBox_AlarmRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_AlarmRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_AlarmTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_AlarmTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WaitingTotalRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_WaitingTotalRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WaitingTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_WaitingTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WorkTotalRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_WorkTotalRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WorkTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_WorkTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ManualRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ManualRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ManualTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ManualTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_AutoRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_AutoRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_AutoTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_AutoTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PowerOnRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_TotalTime = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_TotalTime = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_PowerOn = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_PowerOn = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_PowerOnRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_WaitingRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WaitingRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_WorkRate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_WorkRate = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TableLayoutPanel_Body_Analysis_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Chart_Work = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart_PowerOn = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_Action = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDateTime_Start = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiDateTime_End = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiButton_Export = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_Action = New Kochi.HMI.MainControl.UI.HMIComboBox()
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
        Me.TableLayoutPanel_Body_Analysis_Head.SuspendLayout()
        Me.TableLayoutPanel_Body_Analysis_Bottom.SuspendLayout()
        CType(Me.Chart_Work, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart_PowerOn, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
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
        Me.TabControl_Data.Location = New System.Drawing.Point(0, 111)
        Me.TabControl_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Data.Name = "TabControl_Data"
        Me.TabControl_Data.SelectedIndex = 0
        Me.TabControl_Data.Size = New System.Drawing.Size(467, 419)
        Me.TabControl_Data.TabIndex = 5
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
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(453, 381)
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(453, 350)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 350)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(453, 31)
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
        Me.TabPage_Analysis.Size = New System.Drawing.Size(459, 387)
        Me.TabPage_Analysis.TabIndex = 1
        Me.TabPage_Analysis.Text = "Analysis"
        Me.TabPage_Analysis.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Mid_Analysis
        '
        Me.TableLayoutPanel_Body_Mid_Analysis.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid_Analysis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.Controls.Add(Me.TableLayoutPanel_Body_Analysis_Head, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Analysis.Controls.Add(Me.TableLayoutPanel_Body_Analysis_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Analysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Analysis.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Mid_Analysis.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Analysis.Name = "TableLayoutPanel_Body_Mid_Analysis"
        Me.TableLayoutPanel_Body_Mid_Analysis.RowCount = 2
        Me.TableLayoutPanel_Body_Mid_Analysis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 351.0!))
        Me.TableLayoutPanel_Body_Mid_Analysis.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid_Analysis.Size = New System.Drawing.Size(453, 381)
        Me.TableLayoutPanel_Body_Mid_Analysis.TabIndex = 0
        '
        'TableLayoutPanel_Body_Analysis_Head
        '
        Me.TableLayoutPanel_Body_Analysis_Head.ColumnCount = 4
        Me.TableLayoutPanel_Body_Analysis_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_AlarmRate, 3, 8)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_AlarmRate, 2, 8)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_AlarmTime, 1, 8)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_AlarmTime, 0, 8)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WaitingTotalRate, 3, 6)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WaitingTotalRate, 2, 6)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WaitingTime, 1, 6)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WaitingTime, 0, 6)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WorkTotalRate, 3, 4)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WorkTotalRate, 2, 4)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WorkTime, 1, 4)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WorkTime, 0, 4)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_ManualRate, 3, 3)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_ManualRate, 2, 3)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_ManualTime, 1, 3)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_ManualTime, 0, 3)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_AutoRate, 3, 2)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_AutoRate, 2, 2)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_AutoTime, 1, 2)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_AutoTime, 0, 2)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_PowerOnRate, 3, 1)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_TotalTime, 0, 0)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_TotalTime, 1, 0)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_PowerOn, 0, 1)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_PowerOn, 1, 1)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_PowerOnRate, 2, 1)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WaitingRate, 2, 7)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WaitingRate, 3, 7)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiLabel_WorkRate, 2, 5)
        Me.TableLayoutPanel_Body_Analysis_Head.Controls.Add(Me.HmiTextBox_WorkRate, 3, 5)
        Me.TableLayoutPanel_Body_Analysis_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Analysis_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Analysis_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Analysis_Head.Name = "TableLayoutPanel_Body_Analysis_Head"
        Me.TableLayoutPanel_Body_Analysis_Head.RowCount = 9
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Analysis_Head.Size = New System.Drawing.Size(453, 351)
        Me.TableLayoutPanel_Body_Analysis_Head.TabIndex = 0
        '
        'HmiTextBox_AlarmRate
        '
        Me.HmiTextBox_AlarmRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AlarmRate.Location = New System.Drawing.Point(363, 315)
        Me.HmiTextBox_AlarmRate.Name = "HmiTextBox_AlarmRate"
        Me.HmiTextBox_AlarmRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_AlarmRate.TabIndex = 28
        '
        'HmiLabel_AlarmRate
        '
        Me.HmiLabel_AlarmRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AlarmRate.Location = New System.Drawing.Point(228, 315)
        Me.HmiLabel_AlarmRate.Name = "HmiLabel_AlarmRate"
        Me.HmiLabel_AlarmRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_AlarmRate.TabIndex = 27
        '
        'HmiTextBox_AlarmTime
        '
        Me.HmiTextBox_AlarmTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AlarmTime.Location = New System.Drawing.Point(138, 315)
        Me.HmiTextBox_AlarmTime.Name = "HmiTextBox_AlarmTime"
        Me.HmiTextBox_AlarmTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_AlarmTime.TabIndex = 26
        '
        'HmiLabel_AlarmTime
        '
        Me.HmiLabel_AlarmTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AlarmTime.Location = New System.Drawing.Point(3, 315)
        Me.HmiLabel_AlarmTime.Name = "HmiLabel_AlarmTime"
        Me.HmiLabel_AlarmTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_AlarmTime.TabIndex = 25
        '
        'HmiTextBox_WaitingTotalRate
        '
        Me.HmiTextBox_WaitingTotalRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WaitingTotalRate.Location = New System.Drawing.Point(363, 237)
        Me.HmiTextBox_WaitingTotalRate.Name = "HmiTextBox_WaitingTotalRate"
        Me.HmiTextBox_WaitingTotalRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_WaitingTotalRate.TabIndex = 24
        '
        'HmiLabel_WaitingTotalRate
        '
        Me.HmiLabel_WaitingTotalRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WaitingTotalRate.Location = New System.Drawing.Point(228, 237)
        Me.HmiLabel_WaitingTotalRate.Name = "HmiLabel_WaitingTotalRate"
        Me.HmiLabel_WaitingTotalRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WaitingTotalRate.TabIndex = 23
        '
        'HmiTextBox_WaitingTime
        '
        Me.HmiTextBox_WaitingTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WaitingTime.Location = New System.Drawing.Point(138, 237)
        Me.HmiTextBox_WaitingTime.Name = "HmiTextBox_WaitingTime"
        Me.HmiTextBox_WaitingTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_WaitingTime.TabIndex = 22
        '
        'HmiLabel_WaitingTime
        '
        Me.HmiLabel_WaitingTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WaitingTime.Location = New System.Drawing.Point(3, 237)
        Me.HmiLabel_WaitingTime.Name = "HmiLabel_WaitingTime"
        Me.HmiLabel_WaitingTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WaitingTime.TabIndex = 21
        '
        'HmiTextBox_WorkTotalRate
        '
        Me.HmiTextBox_WorkTotalRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WorkTotalRate.Location = New System.Drawing.Point(363, 159)
        Me.HmiTextBox_WorkTotalRate.Name = "HmiTextBox_WorkTotalRate"
        Me.HmiTextBox_WorkTotalRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_WorkTotalRate.TabIndex = 20
        '
        'HmiLabel_WorkTotalRate
        '
        Me.HmiLabel_WorkTotalRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WorkTotalRate.Location = New System.Drawing.Point(228, 159)
        Me.HmiLabel_WorkTotalRate.Name = "HmiLabel_WorkTotalRate"
        Me.HmiLabel_WorkTotalRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WorkTotalRate.TabIndex = 19
        '
        'HmiTextBox_WorkTime
        '
        Me.HmiTextBox_WorkTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WorkTime.Location = New System.Drawing.Point(138, 159)
        Me.HmiTextBox_WorkTime.Name = "HmiTextBox_WorkTime"
        Me.HmiTextBox_WorkTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_WorkTime.TabIndex = 18
        '
        'HmiLabel_WorkTime
        '
        Me.HmiLabel_WorkTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WorkTime.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_WorkTime.Name = "HmiLabel_WorkTime"
        Me.HmiLabel_WorkTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WorkTime.TabIndex = 17
        '
        'HmiTextBox_ManualRate
        '
        Me.HmiTextBox_ManualRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ManualRate.Location = New System.Drawing.Point(363, 120)
        Me.HmiTextBox_ManualRate.Name = "HmiTextBox_ManualRate"
        Me.HmiTextBox_ManualRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_ManualRate.TabIndex = 16
        '
        'HmiLabel_ManualRate
        '
        Me.HmiLabel_ManualRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ManualRate.Location = New System.Drawing.Point(228, 120)
        Me.HmiLabel_ManualRate.Name = "HmiLabel_ManualRate"
        Me.HmiLabel_ManualRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_ManualRate.TabIndex = 15
        '
        'HmiTextBox_ManualTime
        '
        Me.HmiTextBox_ManualTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ManualTime.Location = New System.Drawing.Point(138, 120)
        Me.HmiTextBox_ManualTime.Name = "HmiTextBox_ManualTime"
        Me.HmiTextBox_ManualTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_ManualTime.TabIndex = 14
        '
        'HmiLabel_ManualTime
        '
        Me.HmiLabel_ManualTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ManualTime.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_ManualTime.Name = "HmiLabel_ManualTime"
        Me.HmiLabel_ManualTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_ManualTime.TabIndex = 13
        '
        'HmiTextBox_AutoRate
        '
        Me.HmiTextBox_AutoRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AutoRate.Location = New System.Drawing.Point(363, 81)
        Me.HmiTextBox_AutoRate.Name = "HmiTextBox_AutoRate"
        Me.HmiTextBox_AutoRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_AutoRate.TabIndex = 11
        '
        'HmiLabel_AutoRate
        '
        Me.HmiLabel_AutoRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AutoRate.Location = New System.Drawing.Point(228, 81)
        Me.HmiLabel_AutoRate.Name = "HmiLabel_AutoRate"
        Me.HmiLabel_AutoRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_AutoRate.TabIndex = 10
        '
        'HmiTextBox_AutoTime
        '
        Me.HmiTextBox_AutoTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_AutoTime.Location = New System.Drawing.Point(138, 81)
        Me.HmiTextBox_AutoTime.Name = "HmiTextBox_AutoTime"
        Me.HmiTextBox_AutoTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_AutoTime.TabIndex = 9
        '
        'HmiLabel_AutoTime
        '
        Me.HmiLabel_AutoTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_AutoTime.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_AutoTime.Name = "HmiLabel_AutoTime"
        Me.HmiLabel_AutoTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_AutoTime.TabIndex = 8
        '
        'HmiTextBox_PowerOnRate
        '
        Me.HmiTextBox_PowerOnRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PowerOnRate.Location = New System.Drawing.Point(363, 42)
        Me.HmiTextBox_PowerOnRate.Name = "HmiTextBox_PowerOnRate"
        Me.HmiTextBox_PowerOnRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_PowerOnRate.TabIndex = 7
        '
        'HmiLabel_TotalTime
        '
        Me.HmiLabel_TotalTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_TotalTime.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_TotalTime.Name = "HmiLabel_TotalTime"
        Me.HmiLabel_TotalTime.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_TotalTime.TabIndex = 0
        '
        'HmiTextBox_TotalTime
        '
        Me.HmiTextBox_TotalTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_TotalTime.Location = New System.Drawing.Point(138, 3)
        Me.HmiTextBox_TotalTime.Name = "HmiTextBox_TotalTime"
        Me.HmiTextBox_TotalTime.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_TotalTime.TabIndex = 1
        '
        'HmiLabel_PowerOn
        '
        Me.HmiLabel_PowerOn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PowerOn.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_PowerOn.Name = "HmiLabel_PowerOn"
        Me.HmiLabel_PowerOn.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_PowerOn.TabIndex = 2
        '
        'HmiTextBox_PowerOn
        '
        Me.HmiTextBox_PowerOn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_PowerOn.Location = New System.Drawing.Point(138, 42)
        Me.HmiTextBox_PowerOn.Name = "HmiTextBox_PowerOn"
        Me.HmiTextBox_PowerOn.Size = New System.Drawing.Size(84, 33)
        Me.HmiTextBox_PowerOn.TabIndex = 3
        '
        'HmiLabel_PowerOnRate
        '
        Me.HmiLabel_PowerOnRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_PowerOnRate.Location = New System.Drawing.Point(228, 42)
        Me.HmiLabel_PowerOnRate.Name = "HmiLabel_PowerOnRate"
        Me.HmiLabel_PowerOnRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_PowerOnRate.TabIndex = 4
        '
        'HmiLabel_WaitingRate
        '
        Me.HmiLabel_WaitingRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WaitingRate.Location = New System.Drawing.Point(228, 276)
        Me.HmiLabel_WaitingRate.Name = "HmiLabel_WaitingRate"
        Me.HmiLabel_WaitingRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WaitingRate.TabIndex = 29
        '
        'HmiTextBox_WaitingRate
        '
        Me.HmiTextBox_WaitingRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WaitingRate.Location = New System.Drawing.Point(363, 276)
        Me.HmiTextBox_WaitingRate.Name = "HmiTextBox_WaitingRate"
        Me.HmiTextBox_WaitingRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_WaitingRate.TabIndex = 30
        '
        'HmiLabel_WorkRate
        '
        Me.HmiLabel_WorkRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_WorkRate.Location = New System.Drawing.Point(228, 198)
        Me.HmiLabel_WorkRate.Name = "HmiLabel_WorkRate"
        Me.HmiLabel_WorkRate.Size = New System.Drawing.Size(129, 33)
        Me.HmiLabel_WorkRate.TabIndex = 31
        '
        'HmiTextBox_WorkRate
        '
        Me.HmiTextBox_WorkRate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_WorkRate.Location = New System.Drawing.Point(363, 198)
        Me.HmiTextBox_WorkRate.Name = "HmiTextBox_WorkRate"
        Me.HmiTextBox_WorkRate.Size = New System.Drawing.Size(87, 33)
        Me.HmiTextBox_WorkRate.TabIndex = 32
        '
        'TableLayoutPanel_Body_Analysis_Bottom
        '
        Me.TableLayoutPanel_Body_Analysis_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Analysis_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Analysis_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Analysis_Bottom.Controls.Add(Me.Chart_Work, 0, 0)
        Me.TableLayoutPanel_Body_Analysis_Bottom.Controls.Add(Me.Chart_PowerOn, 0, 0)
        Me.TableLayoutPanel_Body_Analysis_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Analysis_Bottom.Location = New System.Drawing.Point(3, 354)
        Me.TableLayoutPanel_Body_Analysis_Bottom.Name = "TableLayoutPanel_Body_Analysis_Bottom"
        Me.TableLayoutPanel_Body_Analysis_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Analysis_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Analysis_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Analysis_Bottom.Size = New System.Drawing.Size(447, 100)
        Me.TableLayoutPanel_Body_Analysis_Bottom.TabIndex = 1
        '
        'Chart_Work
        '
        ChartArea1.AxisX.IsMarginVisible = False
        ChartArea1.Name = "ChartArea_Alarm"
        Me.Chart_Work.ChartAreas.Add(ChartArea1)
        Me.Chart_Work.Dock = System.Windows.Forms.DockStyle.Fill
        Legend1.Name = "Legend1"
        Me.Chart_Work.Legends.Add(Legend1)
        Me.Chart_Work.Location = New System.Drawing.Point(226, 3)
        Me.Chart_Work.Name = "Chart_Work"
        Me.Chart_Work.Size = New System.Drawing.Size(218, 94)
        Me.Chart_Work.TabIndex = 5
        Me.Chart_Work.Text = "Top Ten Alarm"
        '
        'Chart_PowerOn
        '
        ChartArea2.AxisX.IsMarginVisible = False
        ChartArea2.Name = "ChartArea_Alarm"
        Me.Chart_PowerOn.ChartAreas.Add(ChartArea2)
        Me.Chart_PowerOn.Dock = System.Windows.Forms.DockStyle.Fill
        Legend2.Name = "Legend1"
        Me.Chart_PowerOn.Legends.Add(Legend2)
        Me.Chart_PowerOn.Location = New System.Drawing.Point(3, 3)
        Me.Chart_PowerOn.Name = "Chart_PowerOn"
        Me.Chart_PowerOn.Size = New System.Drawing.Size(217, 94)
        Me.Chart_PowerOn.TabIndex = 4
        Me.Chart_PowerOn.Text = "Top Ten Alarm"
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
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.001!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.002!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.001!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.002!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33133!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Action, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Action, 1, 1)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 2
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 79)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiLabel_Action
        '
        Me.HmiLabel_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Action.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Action.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Action.Name = "HmiLabel_Action"
        Me.HmiLabel_Action.Size = New System.Drawing.Size(45, 34)
        Me.HmiLabel_Action.TabIndex = 8
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
        'HmiComboBox_Action
        '
        Me.HmiComboBox_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Action.Location = New System.Drawing.Point(45, 42)
        Me.HmiComboBox_Action.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Action.Name = "HmiComboBox_Action"
        Me.HmiComboBox_Action.Size = New System.Drawing.Size(91, 34)
        Me.HmiComboBox_Action.TabIndex = 11
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
        'MachineForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "MachineForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MachineForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TabControl_Data.ResumeLayout(False)
        Me.TabPage_Data.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Analysis.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Analysis.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Analysis_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Analysis_Bottom.ResumeLayout(False)
        CType(Me.Chart_Work, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart_PowerOn, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents HmiLabel_Action As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Export As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TabControl_Data As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Data As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents TabPage_Analysis As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Mid_Analysis As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiComboBox_Action As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents TableLayoutPanel_Body_Analysis_Head As HMITableLayoutPanel
    Friend WithEvents HmiLabel_TotalTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_TotalTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_PowerOnRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_PowerOn As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_PowerOn As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_PowerOnRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_AlarmRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AlarmRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_AlarmTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AlarmTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WaitingTotalRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_WaitingTotalRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WaitingTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_WaitingTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WorkTotalRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_WorkTotalRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WorkTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_WorkTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ManualRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ManualRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ManualTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ManualTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_AutoRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AutoRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_AutoTime As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_AutoTime As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body_Analysis_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Chart_PowerOn As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart_Work As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents HmiLabel_WaitingRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WaitingRate As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_WorkRate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_WorkRate As Kochi.HMI.MainControl.UI.HMITextBox
End Class
