<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOForm
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_ResetStation = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Data_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Data_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Station = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Function = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiComboBox_Function_Result = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Function_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTableLayoutPanel_Data = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiTextBox_Function_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Result = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.HmiTableLayoutPanel_Body_Top_Right.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.HmiTableLayoutPanel_Mid.SuspendLayout()
        Me.GroupBox_Search.SuspendLayout()
        Me.HmiTableLayoutPanel_Head.SuspendLayout()
        Me.GroupBox_Function.SuspendLayout()
        Me.HmiTableLayoutPanel_Function.SuspendLayout()
        Me.HmiTableLayoutPanel_Data.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TabControl1)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 574)
        Me.Panel_Body.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(498, 574)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(490, 548)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_Body_Top_Right
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Result, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Result, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SFC, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_SFC, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_ResetStation, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Name = "HmiTableLayoutPanel_Body_Top_Right"
        Me.HmiTableLayoutPanel_Body_Top_Right.RowCount = 9
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(484, 542)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 1
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(3, 46)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(90, 37)
        Me.HmiLabel_SFC.TabIndex = 15
        '
        'HmiTextBox_SFC
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_SFC, 2)
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(99, 46)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(186, 37)
        Me.HmiTextBox_SFC.TabIndex = 18
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'HmiButton_ResetStation
        '
        Me.HmiButton_ResetStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_ResetStation.Location = New System.Drawing.Point(291, 46)
        Me.HmiButton_ResetStation.MarginHeight = 6
        Me.HmiButton_ResetStation.Name = "HmiButton_ResetStation"
        Me.HmiButton_ResetStation.Size = New System.Drawing.Size(90, 37)
        Me.HmiButton_ResetStation.TabIndex = 29
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.HmiTableLayoutPanel_Mid)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(490, 548)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'HmiTableLayoutPanel_Mid
        '
        Me.HmiTableLayoutPanel_Mid.ColumnCount = 2
        Me.HmiTableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.HmiTableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.GroupBox_Function, 1, 1)
        Me.HmiTableLayoutPanel_Mid.Controls.Add(Me.HmiTableLayoutPanel_Data, 0, 1)
        Me.HmiTableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Mid.Location = New System.Drawing.Point(3, 3)
        Me.HmiTableLayoutPanel_Mid.Name = "HmiTableLayoutPanel_Mid"
        Me.HmiTableLayoutPanel_Mid.RowCount = 2
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Mid.Size = New System.Drawing.Size(484, 542)
        Me.HmiTableLayoutPanel_Mid.TabIndex = 0
        '
        'GroupBox_Search
        '
        Me.HmiTableLayoutPanel_Mid.SetColumnSpan(Me.GroupBox_Search, 2)
        Me.GroupBox_Search.Controls.Add(Me.HmiTableLayoutPanel_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(478, 144)
        Me.GroupBox_Search.TabIndex = 0
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Head
        '
        Me.HmiTableLayoutPanel_Head.ColumnCount = 6
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_Station, 2, 1)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiTextBox_Data_SFC, 1, 1)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_Data_SFC, 0, 1)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiComboBox_Station, 3, 1)
        Me.HmiTableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Head.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Head.Name = "HmiTableLayoutPanel_Head"
        Me.HmiTableLayoutPanel_Head.RowCount = 2
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Head.Size = New System.Drawing.Size(472, 125)
        Me.HmiTableLayoutPanel_Head.TabIndex = 0
        '
        'HmiLabel_Station
        '
        Me.HmiLabel_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Station.Location = New System.Drawing.Point(164, 65)
        Me.HmiLabel_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Station.Name = "HmiLabel_Station"
        Me.HmiLabel_Station.Size = New System.Drawing.Size(70, 57)
        Me.HmiLabel_Station.TabIndex = 15
        '
        'HmiTextBox_Data_SFC
        '
        Me.HmiTextBox_Data_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Data_SFC.Location = New System.Drawing.Point(70, 65)
        Me.HmiTextBox_Data_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Data_SFC.Name = "HmiTextBox_Data_SFC"
        Me.HmiTextBox_Data_SFC.Number = 0
        Me.HmiTextBox_Data_SFC.Size = New System.Drawing.Size(94, 57)
        Me.HmiTextBox_Data_SFC.TabIndex = 14
        Me.HmiTextBox_Data_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_Data_SFC.ValueType = GetType(String)
        '
        'HmiLabel_Data_SFC
        '
        Me.HmiLabel_Data_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Data_SFC.Location = New System.Drawing.Point(0, 65)
        Me.HmiLabel_Data_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Data_SFC.Name = "HmiLabel_Data_SFC"
        Me.HmiLabel_Data_SFC.Size = New System.Drawing.Size(70, 57)
        Me.HmiLabel_Data_SFC.TabIndex = 13
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(399, 1)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(72, 60)
        Me.HmiButton_Cancel.TabIndex = 12
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(329, 1)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(68, 60)
        Me.HmiButton_Search.TabIndex = 11
        '
        'HmiLabel_EndDate
        '
        Me.HmiLabel_EndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_EndDate.Location = New System.Drawing.Point(164, 3)
        Me.HmiLabel_EndDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_EndDate.Name = "HmiLabel_EndDate"
        Me.HmiLabel_EndDate.Size = New System.Drawing.Size(70, 56)
        Me.HmiLabel_EndDate.TabIndex = 8
        '
        'HmiLabel_StartDate
        '
        Me.HmiLabel_StartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StartDate.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_StartDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_StartDate.Name = "HmiLabel_StartDate"
        Me.HmiLabel_StartDate.Size = New System.Drawing.Size(70, 56)
        Me.HmiLabel_StartDate.TabIndex = 1
        '
        'HmiComboBox_Station
        '
        Me.HmiComboBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Station.Location = New System.Drawing.Point(237, 65)
        Me.HmiComboBox_Station.Name = "HmiComboBox_Station"
        Me.HmiComboBox_Station.Size = New System.Drawing.Size(88, 57)
        Me.HmiComboBox_Station.TabIndex = 16
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.HmiTableLayoutPanel_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Location = New System.Drawing.Point(390, 153)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(91, 386)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Function
        '
        Me.HmiTableLayoutPanel_Function.ColumnCount = 1
        Me.HmiTableLayoutPanel_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiComboBox_Function_Result, 0, 5)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_Result, 0, 4)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_SFC, 0, 3)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_SFC, 0, 2)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_ID, 0, 1)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Modify, 0, 6)
        Me.HmiTableLayoutPanel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Function.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Function.Name = "HmiTableLayoutPanel_Function"
        Me.HmiTableLayoutPanel_Function.RowCount = 8
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Function.Size = New System.Drawing.Size(85, 367)
        Me.HmiTableLayoutPanel_Function.TabIndex = 0
        '
        'HmiComboBox_Function_Result
        '
        Me.HmiComboBox_Function_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_Result.Location = New System.Drawing.Point(3, 153)
        Me.HmiComboBox_Function_Result.Name = "HmiComboBox_Function_Result"
        Me.HmiComboBox_Function_Result.Size = New System.Drawing.Size(79, 24)
        Me.HmiComboBox_Function_Result.TabIndex = 19
        '
        'HmiLabel_Function_Result
        '
        Me.HmiLabel_Function_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Result.Location = New System.Drawing.Point(0, 123)
        Me.HmiLabel_Function_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_Result.Name = "HmiLabel_Function_Result"
        Me.HmiLabel_Function_Result.Size = New System.Drawing.Size(85, 24)
        Me.HmiLabel_Function_Result.TabIndex = 18
        '
        'HmiTextBox_Function_SFC
        '
        Me.HmiTextBox_Function_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_SFC.Location = New System.Drawing.Point(0, 93)
        Me.HmiTextBox_Function_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_SFC.Name = "HmiTextBox_Function_SFC"
        Me.HmiTextBox_Function_SFC.Number = 0
        Me.HmiTextBox_Function_SFC.Size = New System.Drawing.Size(85, 24)
        Me.HmiTextBox_Function_SFC.TabIndex = 17
        Me.HmiTextBox_Function_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_Function_SFC.ValueType = GetType(String)
        '
        'HmiLabel_Function_SFC
        '
        Me.HmiLabel_Function_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_SFC.Location = New System.Drawing.Point(0, 63)
        Me.HmiLabel_Function_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_SFC.Name = "HmiLabel_Function_SFC"
        Me.HmiLabel_Function_SFC.Size = New System.Drawing.Size(85, 24)
        Me.HmiLabel_Function_SFC.TabIndex = 16
        '
        'HmiButton_Modify
        '
        Me.HmiButton_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Modify.Location = New System.Drawing.Point(3, 183)
        Me.HmiButton_Modify.MarginHeight = 6
        Me.HmiButton_Modify.Name = "HmiButton_Modify"
        Me.HmiButton_Modify.Size = New System.Drawing.Size(79, 24)
        Me.HmiButton_Modify.TabIndex = 21
        '
        'HmiTableLayoutPanel_Data
        '
        Me.HmiTableLayoutPanel_Data.ColumnCount = 1
        Me.HmiTableLayoutPanel_Data.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Data.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.HmiTableLayoutPanel_Data.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.HmiTableLayoutPanel_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Data.Location = New System.Drawing.Point(3, 153)
        Me.HmiTableLayoutPanel_Data.Name = "HmiTableLayoutPanel_Data"
        Me.HmiTableLayoutPanel_Data.RowCount = 2
        Me.HmiTableLayoutPanel_Data.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.HmiTableLayoutPanel_Data.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Data.Size = New System.Drawing.Size(381, 386)
        Me.HmiTableLayoutPanel_Data.TabIndex = 2
        '
        'HmiDataViewPage_Data
        '
        Me.HmiDataViewPage_Data.Button_DownEnable = False
        Me.HmiDataViewPage_Data.Button_DownLastEnable = False
        Me.HmiDataViewPage_Data.Button_GoEnable = False
        Me.HmiDataViewPage_Data.Button_UpEnable = False
        Me.HmiDataViewPage_Data.Button_UpFirstEnable = False
        Me.HmiDataViewPage_Data.CurrentPage = 0
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 347)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(373, 39)
        Me.HmiDataViewPage_Data.TabIndex = 0
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
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
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.HmiDataView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.HmiDataView_Data.DefaultCellStyle = DataGridViewCellStyle3
        Me.HmiDataView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Data.EnableHeadersVisualStyles = False
        Me.HmiDataView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Data.Location = New System.Drawing.Point(3, 3)
        Me.HmiDataView_Data.Name = "HmiDataView_Data"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_Data.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.HmiDataView_Data.RowHeadersVisible = False
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Data.RowTemplate.Height = 40
        Me.HmiDataView_Data.Size = New System.Drawing.Size(375, 341)
        Me.HmiDataView_Data.TabIndex = 1
        '
        'HmiTextBox_Function_ID
        '
        Me.HmiTextBox_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ID.Location = New System.Drawing.Point(0, 33)
        Me.HmiTextBox_Function_ID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_ID.Name = "HmiTextBox_Function_ID"
        Me.HmiTextBox_Function_ID.Number = 0
        Me.HmiTextBox_Function_ID.Size = New System.Drawing.Size(85, 24)
        Me.HmiTextBox_Function_ID.TabIndex = 15
        Me.HmiTextBox_Function_ID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_ID.ValueType = GetType(String)
        '
        'HmiLabel_Result
        '
        Me.HmiLabel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result.Location = New System.Drawing.Point(3, 89)
        Me.HmiLabel_Result.Name = "HmiLabel_Result"
        Me.HmiLabel_Result.Size = New System.Drawing.Size(90, 37)
        Me.HmiLabel_Result.TabIndex = 30
        '
        'HmiTextBox_Result
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_Result, 2)
        Me.HmiTextBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Result.Location = New System.Drawing.Point(99, 89)
        Me.HmiTextBox_Result.Name = "HmiTextBox_Result"
        Me.HmiTextBox_Result.Number = 0
        Me.HmiTextBox_Result.Size = New System.Drawing.Size(186, 37)
        Me.HmiTextBox_Result.TabIndex = 31
        Me.HmiTextBox_Result.TextBoxReadOnly = False
        Me.HmiTextBox_Result.ValueType = GetType(String)
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 574)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IOForm"
        Me.Text = "IOForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Body_Top_Right.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Mid.ResumeLayout(False)
        Me.GroupBox_Search.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Head.ResumeLayout(False)
        Me.GroupBox_Function.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Function.ResumeLayout(False)
        Me.HmiTableLayoutPanel_Data.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents HmiTableLayoutPanel_Mid As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_StartDate As Kochi.HMI.MainControl.UI.HMILabel
    ' Friend WithEvents HmiDateTime_Start As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiLabel_EndDate As Kochi.HMI.MainControl.UI.HMILabel
    ' Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Data_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Data_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Station As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Function As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Function_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Function_Result As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Function_Result As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTableLayoutPanel_Data As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_ResetStation As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Function_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_Result As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Result As Kochi.HMI.MainControl.UI.HMILabel
End Class
