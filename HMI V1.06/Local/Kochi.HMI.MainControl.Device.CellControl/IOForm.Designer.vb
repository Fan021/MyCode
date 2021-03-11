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
        Me.HmiTableLayoutPanel_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Station = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Function = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiComboBox_Function_Combox = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Function_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Add = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Del = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTableLayoutPanel_Data = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.Panel_Body.SuspendLayout()
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
        Me.Panel_Body.Controls.Add(Me.HmiTableLayoutPanel_Mid)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(498, 530)
        Me.Panel_Body.TabIndex = 0
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
        Me.HmiTableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 0)
        Me.HmiTableLayoutPanel_Mid.Name = "HmiTableLayoutPanel_Mid"
        Me.HmiTableLayoutPanel_Mid.RowCount = 2
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138.0!))
        Me.HmiTableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Mid.Size = New System.Drawing.Size(498, 530)
        Me.HmiTableLayoutPanel_Mid.TabIndex = 1
        '
        'GroupBox_Search
        '
        Me.HmiTableLayoutPanel_Mid.SetColumnSpan(Me.GroupBox_Search, 2)
        Me.GroupBox_Search.Controls.Add(Me.HmiTableLayoutPanel_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(492, 132)
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
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiTextBox_SFC, 1, 1)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_SFC, 0, 1)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiComboBox_Station, 3, 1)
        Me.HmiTableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Head.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel_Head.Name = "HmiTableLayoutPanel_Head"
        Me.HmiTableLayoutPanel_Head.RowCount = 2
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.HmiTableLayoutPanel_Head.Size = New System.Drawing.Size(486, 112)
        Me.HmiTableLayoutPanel_Head.TabIndex = 0
        '
        'HmiLabel_Station
        '
        Me.HmiLabel_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Station.Location = New System.Drawing.Point(169, 59)
        Me.HmiLabel_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Station.Name = "HmiLabel_Station"
        Me.HmiLabel_Station.Size = New System.Drawing.Size(72, 50)
        Me.HmiLabel_Station.TabIndex = 15
        '
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(72, 59)
        Me.HmiTextBox_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(97, 50)
        Me.HmiTextBox_SFC.TabIndex = 14
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(0, 59)
        Me.HmiLabel_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(72, 50)
        Me.HmiLabel_SFC.TabIndex = 13
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(411, 1)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(74, 54)
        Me.HmiButton_Cancel.TabIndex = 12
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(339, 1)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(70, 54)
        Me.HmiButton_Search.TabIndex = 11
        '
        'HmiLabel_EndDate
        '
        Me.HmiLabel_EndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_EndDate.Location = New System.Drawing.Point(169, 3)
        Me.HmiLabel_EndDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_EndDate.Name = "HmiLabel_EndDate"
        Me.HmiLabel_EndDate.Size = New System.Drawing.Size(72, 50)
        Me.HmiLabel_EndDate.TabIndex = 8
        '
        'HmiLabel_StartDate
        '
        Me.HmiLabel_StartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StartDate.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_StartDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_StartDate.Name = "HmiLabel_StartDate"
        Me.HmiLabel_StartDate.Size = New System.Drawing.Size(72, 50)
        Me.HmiLabel_StartDate.TabIndex = 1
        '
        'HmiComboBox_Station
        '
        Me.HmiComboBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Station.Location = New System.Drawing.Point(244, 59)
        Me.HmiComboBox_Station.Name = "HmiComboBox_Station"
        Me.HmiComboBox_Station.Size = New System.Drawing.Size(91, 50)
        Me.HmiComboBox_Station.TabIndex = 16
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.HmiTableLayoutPanel_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Location = New System.Drawing.Point(401, 141)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(94, 386)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "GroupBox1"
        '
        'HmiTableLayoutPanel_Function
        '
        Me.HmiTableLayoutPanel_Function.ColumnCount = 1
        Me.HmiTableLayoutPanel_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiComboBox_Function_Combox, 0, 5)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_Station, 0, 4)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_SFC, 0, 3)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_SFC, 0, 2)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_ID, 0, 1)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_ID, 0, 0)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Add, 0, 6)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Modify, 0, 7)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Del, 0, 8)
        Me.HmiTableLayoutPanel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Function.Location = New System.Drawing.Point(3, 17)
        Me.HmiTableLayoutPanel_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Function.Name = "HmiTableLayoutPanel_Function"
        Me.HmiTableLayoutPanel_Function.RowCount = 10
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.HmiTableLayoutPanel_Function.Size = New System.Drawing.Size(88, 366)
        Me.HmiTableLayoutPanel_Function.TabIndex = 0
        '
        'HmiComboBox_Function_Combox
        '
        Me.HmiComboBox_Function_Combox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_Combox.Location = New System.Drawing.Point(3, 143)
        Me.HmiComboBox_Function_Combox.Name = "HmiComboBox_Function_Combox"
        Me.HmiComboBox_Function_Combox.Size = New System.Drawing.Size(82, 22)
        Me.HmiComboBox_Function_Combox.TabIndex = 19
        '
        'HmiLabel_Function_Station
        '
        Me.HmiLabel_Function_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Station.Location = New System.Drawing.Point(0, 115)
        Me.HmiLabel_Function_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_Station.Name = "HmiLabel_Function_Station"
        Me.HmiLabel_Function_Station.Size = New System.Drawing.Size(88, 22)
        Me.HmiLabel_Function_Station.TabIndex = 18
        '
        'HmiTextBox_Function_SFC
        '
        Me.HmiTextBox_Function_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_SFC.Location = New System.Drawing.Point(0, 87)
        Me.HmiTextBox_Function_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_SFC.Name = "HmiTextBox_Function_SFC"
        Me.HmiTextBox_Function_SFC.Number = 0
        Me.HmiTextBox_Function_SFC.Size = New System.Drawing.Size(88, 22)
        Me.HmiTextBox_Function_SFC.TabIndex = 17
        Me.HmiTextBox_Function_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_Function_SFC.ValueType = GetType(String)
        '
        'HmiLabel_Function_SFC
        '
        Me.HmiLabel_Function_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_SFC.Location = New System.Drawing.Point(0, 59)
        Me.HmiLabel_Function_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_SFC.Name = "HmiLabel_Function_SFC"
        Me.HmiLabel_Function_SFC.Size = New System.Drawing.Size(88, 22)
        Me.HmiLabel_Function_SFC.TabIndex = 16
        '
        'HmiTextBox_Function_ID
        '
        Me.HmiTextBox_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ID.Location = New System.Drawing.Point(0, 31)
        Me.HmiTextBox_Function_ID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_ID.Name = "HmiTextBox_Function_ID"
        Me.HmiTextBox_Function_ID.Number = 0
        Me.HmiTextBox_Function_ID.Size = New System.Drawing.Size(88, 22)
        Me.HmiTextBox_Function_ID.TabIndex = 15
        Me.HmiTextBox_Function_ID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_ID.ValueType = GetType(String)
        '
        'HmiLabel_Function_ID
        '
        Me.HmiLabel_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_ID.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_Function_ID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_ID.Name = "HmiLabel_Function_ID"
        Me.HmiLabel_Function_ID.Size = New System.Drawing.Size(88, 22)
        Me.HmiLabel_Function_ID.TabIndex = 14
        '
        'HmiButton_Add
        '
        Me.HmiButton_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Add.Location = New System.Drawing.Point(3, 171)
        Me.HmiButton_Add.MarginHeight = 6
        Me.HmiButton_Add.Name = "HmiButton_Add"
        Me.HmiButton_Add.Size = New System.Drawing.Size(82, 22)
        Me.HmiButton_Add.TabIndex = 20
        '
        'HmiButton_Modify
        '
        Me.HmiButton_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Modify.Location = New System.Drawing.Point(3, 199)
        Me.HmiButton_Modify.MarginHeight = 6
        Me.HmiButton_Modify.Name = "HmiButton_Modify"
        Me.HmiButton_Modify.Size = New System.Drawing.Size(82, 22)
        Me.HmiButton_Modify.TabIndex = 21
        '
        'HmiButton_Del
        '
        Me.HmiButton_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Del.Location = New System.Drawing.Point(3, 227)
        Me.HmiButton_Del.MarginHeight = 6
        Me.HmiButton_Del.Name = "HmiButton_Del"
        Me.HmiButton_Del.Size = New System.Drawing.Size(82, 22)
        Me.HmiButton_Del.TabIndex = 22
        '
        'HmiTableLayoutPanel_Data
        '
        Me.HmiTableLayoutPanel_Data.ColumnCount = 1
        Me.HmiTableLayoutPanel_Data.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Data.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.HmiTableLayoutPanel_Data.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.HmiTableLayoutPanel_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Data.Location = New System.Drawing.Point(3, 141)
        Me.HmiTableLayoutPanel_Data.Name = "HmiTableLayoutPanel_Data"
        Me.HmiTableLayoutPanel_Data.RowCount = 2
        Me.HmiTableLayoutPanel_Data.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.0!))
        Me.HmiTableLayoutPanel_Data.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.HmiTableLayoutPanel_Data.Size = New System.Drawing.Size(392, 386)
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
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(373, 36)
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
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(386, 341)
        Me.HmiDataView_Data.TabIndex = 1
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IOForm"
        Me.Text = "IOForm"
        Me.Panel_Body.ResumeLayout(False)
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
    Friend WithEvents HmiTableLayoutPanel_Mid As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiLabel_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_EndDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_StartDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Station As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Function As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiComboBox_Function_Combox As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Function_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_ID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Add As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Del As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTableLayoutPanel_Data As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    ' Friend WithEvents HmiDateTime_Start As Kochi.HMI.MainControl.UI.HMIDateTime
    ' Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
End Class
