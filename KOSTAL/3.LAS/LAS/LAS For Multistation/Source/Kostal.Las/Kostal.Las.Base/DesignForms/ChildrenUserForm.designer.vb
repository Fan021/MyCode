Imports System.Windows.Forms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChildrenUserForm
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_Name = New Kostal.Las.Base.HMILabel()
        Me.HmiTextBox_Name = New Kostal.Las.Base.HMITextBox()
        Me.HmiLabel_Level = New Kostal.Las.Base.HMILabel()
        Me.HmiButton_Search = New Kostal.Las.Base.HMIButton()
        Me.HmiButton_Cancel = New Kostal.Las.Base.HMIButton()
        Me.HmiComboBox_Level = New Kostal.Las.Base.HMIComboBox()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kostal.Las.Base.HMIDataView()
        Me.HmiDataViewPage_Data = New Kostal.Las.Base.HMIDataViewPage()
        Me.Timer_Show = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Function = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiComboBox_Function_Level = New Kostal.Las.Base.HMIComboBox()
        Me.HmiLabel_Function_Level = New Kostal.Las.Base.HMILabel()
        Me.HmiButton_Function_Del = New Kostal.Las.Base.HMIButton()
        Me.HmiButton_Function_Modify = New Kostal.Las.Base.HMIButton()
        Me.HmiTextBox_Function_Password = New Kostal.Las.Base.HMITextBox()
        Me.HmiTextBox_Function_Name = New Kostal.Las.Base.HMITextBox()
        Me.HmiLabel_Function_Password = New Kostal.Las.Base.HMILabel()
        Me.HmiLabel_Function_Name = New Kostal.Las.Base.HMILabel()
        Me.HmiLabel_Function_ID = New Kostal.Las.Base.HMILabel()
        Me.HmiTextBox_Function_ID = New Kostal.Las.Base.HMITextBox()
        Me.HmiButton_Function_Add = New Kostal.Las.Base.HMIButton()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Search.SuspendLayout()
        Me.TableLayoutPanel_Body_Head.SuspendLayout()
        Me.TableLayoutPanel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Function.SuspendLayout()
        Me.TableLayoutPanel_Body_Left_Function.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 430)
        Me.Panel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Function, 1, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Mid, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.88889!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 430)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(367, 44)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 6
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Name, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_Name, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Level, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Level, 3, 0)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 1
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(361, 18)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiLabel_Name
        '
        Me.HmiLabel_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Name.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Name.Name = "HmiLabel_Name"
        Me.HmiLabel_Name.Size = New System.Drawing.Size(48, 12)
        Me.HmiLabel_Name.TabIndex = 0
        '
        'HmiTextBox_Name
        '
        Me.HmiTextBox_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Name.Location = New System.Drawing.Point(57, 3)
        Me.HmiTextBox_Name.Name = "HmiTextBox_Name"
        Me.HmiTextBox_Name.Number = 0
        Me.HmiTextBox_Name.Size = New System.Drawing.Size(66, 12)
        Me.HmiTextBox_Name.TabIndex = 1
        Me.HmiTextBox_Name.TextBoxReadOnly = False
        Me.HmiTextBox_Name.ValueType = GetType(String)
        '
        'HmiLabel_Level
        '
        Me.HmiLabel_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Level.Location = New System.Drawing.Point(129, 3)
        Me.HmiLabel_Level.Name = "HmiLabel_Level"
        Me.HmiLabel_Level.Size = New System.Drawing.Size(48, 12)
        Me.HmiLabel_Level.TabIndex = 2
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(255, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(48, 12)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(309, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(49, 12)
        Me.HmiButton_Cancel.TabIndex = 5
        '
        'HmiComboBox_Level
        '
        Me.HmiComboBox_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Level.Location = New System.Drawing.Point(183, 3)
        Me.HmiComboBox_Level.Name = "HmiComboBox_Level"
        Me.HmiComboBox_Level.Size = New System.Drawing.Size(66, 12)
        Me.HmiComboBox_Level.TabIndex = 6
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 50)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(373, 380)
        Me.TableLayoutPanel_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(373, 380)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'HmiDataView_Data
        '
        Me.HmiDataView_Data.AllowUserToAddRows = False
        Me.HmiDataView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.HmiDataView_Data.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.HmiDataView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HmiDataView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Data.EnableHeadersVisualStyles = False
        Me.HmiDataView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Data.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataView_Data.Name = "HmiDataView_Data"
        Me.HmiDataView_Data.ReadOnly = True
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.HmiDataView_Data.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.HmiDataView_Data.RowHeadersVisible = False
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.HmiDataView_Data.RowTemplate.Height = 40
        Me.HmiDataView_Data.RowTemplate.ReadOnly = True
        Me.HmiDataView_Data.Size = New System.Drawing.Size(373, 349)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 349)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(373, 31)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'Timer_Show
        '
        Me.Timer_Show.Interval = 1000
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.TableLayoutPanel_Body_Left_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Function.Location = New System.Drawing.Point(376, 3)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.TableLayoutPanel_Body.SetRowSpan(Me.GroupBox_Function, 2)
        Me.GroupBox_Function.Size = New System.Drawing.Size(88, 424)
        Me.GroupBox_Function.TabIndex = 4
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "Function"
        '
        'TableLayoutPanel_Body_Left_Function
        '
        Me.TableLayoutPanel_Body_Left_Function.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiComboBox_Function_Level, 0, 7)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Level, 0, 6)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Del, 0, 10)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Modify, 0, 9)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Password, 0, 5)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Name, 0, 3)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Password, 0, 4)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Name, 0, 2)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_ID, 0, 0)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_ID, 0, 1)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Add, 0, 8)
        Me.TableLayoutPanel_Body_Left_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Function.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Left_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Function.Name = "TableLayoutPanel_Body_Left_Function"
        Me.TableLayoutPanel_Body_Left_Function.RowCount = 12
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Left_Function.Size = New System.Drawing.Size(82, 398)
        Me.TableLayoutPanel_Body_Left_Function.TabIndex = 0
        '
        'HmiComboBox_Function_Level
        '
        Me.HmiComboBox_Function_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_Level.Location = New System.Drawing.Point(0, 252)
        Me.HmiComboBox_Function_Level.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiComboBox_Function_Level.Name = "HmiComboBox_Function_Level"
        Me.HmiComboBox_Function_Level.Size = New System.Drawing.Size(82, 36)
        Me.HmiComboBox_Function_Level.TabIndex = 10
        '
        'HmiLabel_Function_Level
        '
        Me.HmiLabel_Function_Level.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Level.Location = New System.Drawing.Point(1, 217)
        Me.HmiLabel_Function_Level.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Level.Name = "HmiLabel_Function_Level"
        Me.HmiLabel_Function_Level.Size = New System.Drawing.Size(80, 34)
        Me.HmiLabel_Function_Level.TabIndex = 9
        '
        'HmiButton_Function_Del
        '
        Me.HmiButton_Function_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Del.Location = New System.Drawing.Point(0, 360)
        Me.HmiButton_Function_Del.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Del.MarginHeight = 6
        Me.HmiButton_Function_Del.Name = "HmiButton_Function_Del"
        Me.HmiButton_Function_Del.Size = New System.Drawing.Size(82, 36)
        Me.HmiButton_Function_Del.TabIndex = 8
        '
        'HmiButton_Function_Modify
        '
        Me.HmiButton_Function_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Modify.Location = New System.Drawing.Point(0, 324)
        Me.HmiButton_Function_Modify.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Modify.MarginHeight = 6
        Me.HmiButton_Function_Modify.Name = "HmiButton_Function_Modify"
        Me.HmiButton_Function_Modify.Size = New System.Drawing.Size(82, 36)
        Me.HmiButton_Function_Modify.TabIndex = 7
        '
        'HmiTextBox_Function_Password
        '
        Me.HmiTextBox_Function_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Password.Location = New System.Drawing.Point(0, 180)
        Me.HmiTextBox_Function_Password.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Password.Name = "HmiTextBox_Function_Password"
        Me.HmiTextBox_Function_Password.Number = 0
        Me.HmiTextBox_Function_Password.Size = New System.Drawing.Size(82, 36)
        Me.HmiTextBox_Function_Password.TabIndex = 5
        Me.HmiTextBox_Function_Password.TextBoxReadOnly = False
        Me.HmiTextBox_Function_Password.ValueType = GetType(String)
        '
        'HmiTextBox_Function_Name
        '
        Me.HmiTextBox_Function_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Name.Location = New System.Drawing.Point(0, 108)
        Me.HmiTextBox_Function_Name.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Name.Name = "HmiTextBox_Function_Name"
        Me.HmiTextBox_Function_Name.Number = 0
        Me.HmiTextBox_Function_Name.Size = New System.Drawing.Size(82, 36)
        Me.HmiTextBox_Function_Name.TabIndex = 4
        Me.HmiTextBox_Function_Name.TextBoxReadOnly = False
        Me.HmiTextBox_Function_Name.ValueType = GetType(String)
        '
        'HmiLabel_Function_Password
        '
        Me.HmiLabel_Function_Password.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Password.Location = New System.Drawing.Point(1, 145)
        Me.HmiLabel_Function_Password.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Password.Name = "HmiLabel_Function_Password"
        Me.HmiLabel_Function_Password.Size = New System.Drawing.Size(80, 34)
        Me.HmiLabel_Function_Password.TabIndex = 2
        '
        'HmiLabel_Function_Name
        '
        Me.HmiLabel_Function_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Name.Location = New System.Drawing.Point(1, 73)
        Me.HmiLabel_Function_Name.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Name.Name = "HmiLabel_Function_Name"
        Me.HmiLabel_Function_Name.Size = New System.Drawing.Size(80, 34)
        Me.HmiLabel_Function_Name.TabIndex = 1
        '
        'HmiLabel_Function_ID
        '
        Me.HmiLabel_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_ID.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Function_ID.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_ID.Name = "HmiLabel_Function_ID"
        Me.HmiLabel_Function_ID.Size = New System.Drawing.Size(80, 34)
        Me.HmiLabel_Function_ID.TabIndex = 0
        '
        'HmiTextBox_Function_ID
        '
        Me.HmiTextBox_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ID.Location = New System.Drawing.Point(0, 36)
        Me.HmiTextBox_Function_ID.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_ID.Name = "HmiTextBox_Function_ID"
        Me.HmiTextBox_Function_ID.Number = 0
        Me.HmiTextBox_Function_ID.Size = New System.Drawing.Size(82, 36)
        Me.HmiTextBox_Function_ID.TabIndex = 3
        Me.HmiTextBox_Function_ID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_ID.ValueType = GetType(String)
        '
        'HmiButton_Function_Add
        '
        Me.HmiButton_Function_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Add.Location = New System.Drawing.Point(0, 288)
        Me.HmiButton_Function_Add.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Add.MarginHeight = 6
        Me.HmiButton_Function_Add.Name = "HmiButton_Function_Add"
        Me.HmiButton_Function_Add.Size = New System.Drawing.Size(82, 36)
        Me.HmiButton_Function_Add.TabIndex = 6
        '
        'ChildrenUserForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 430)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenUserForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TextForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Function.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Function.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_Name As HMILabel
    Friend WithEvents HmiTextBox_Name As HMITextBox
    Friend WithEvents HmiLabel_Level As HMILabel
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As HMIDataView
    Friend WithEvents HmiButton_Search As HMIButton
    Friend WithEvents HmiButton_Cancel As HMIButton
    Friend WithEvents HmiDataViewPage_Data As HMIDataViewPage
    Friend WithEvents HmiComboBox_Level As HMIComboBox
    Friend WithEvents Timer_Show As Timer
    Friend WithEvents GroupBox_Function As GroupBox
    Friend WithEvents TableLayoutPanel_Body_Left_Function As TableLayoutPanel
    Friend WithEvents HmiComboBox_Function_Level As HMIComboBox
    Friend WithEvents HmiLabel_Function_Level As HMILabel
    Friend WithEvents HmiButton_Function_Del As HMIButton
    Friend WithEvents HmiButton_Function_Modify As HMIButton
    Friend WithEvents HmiTextBox_Function_Password As HMITextBox
    Friend WithEvents HmiTextBox_Function_Name As HMITextBox
    Friend WithEvents HmiLabel_Function_Password As HMILabel
    Friend WithEvents HmiLabel_Function_Name As HMILabel
    Friend WithEvents HmiLabel_Function_ID As HMILabel
    Friend WithEvents HmiTextBox_Function_ID As HMITextBox
    Friend WithEvents HmiButton_Function_Add As HMIButton
End Class
