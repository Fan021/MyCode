<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenStationErrorForm
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_StationID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_StationID = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Function = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_Function_ErrorCode = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Function_Reset = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Function_StationID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_Enable = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Function_StationID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Function_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Function_ExpectNumber = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_ExpectNumber = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiComboBox_Function_Enable = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Function_ErrorCode = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
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
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Mid, 0, 1)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(461, 66)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 5
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StationID, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_StationID, 1, 0)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 1
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 40)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiLabel_StationID
        '
        Me.HmiLabel_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StationID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_StationID.Name = "HmiLabel_StationID"
        Me.HmiLabel_StationID.Size = New System.Drawing.Size(85, 34)
        Me.HmiLabel_StationID.TabIndex = 0
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(230, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(62, 34)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(298, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(62, 34)
        Me.HmiButton_Cancel.TabIndex = 5
        '
        'HmiComboBox_StationID
        '
        Me.HmiComboBox_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_StationID.Location = New System.Drawing.Point(94, 3)
        Me.HmiComboBox_StationID.Name = "HmiComboBox_StationID"
        Me.HmiComboBox_StationID.Size = New System.Drawing.Size(130, 34)
        Me.HmiComboBox_StationID.TabIndex = 6
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 2
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 0)
        Me.TableLayoutPanel_Mid.Controls.Add(Me.GroupBox_Function, 1, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 72)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 458.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(467, 458)
        Me.TableLayoutPanel_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(373, 458)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 421)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(373, 37)
        Me.HmiDataViewPage_Data.TabIndex = 1
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
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
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
        DataGridViewCellStyle3.Font = New System.Drawing.Font("SimSun", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
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
        DataGridViewCellStyle4.Font = New System.Drawing.Font("SimSun", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(367, 415)
        Me.HmiDataView_Data.TabIndex = 2
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.TableLayoutPanel_Body_Left_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Function.Location = New System.Drawing.Point(376, 3)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(88, 452)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "Function"
        '
        'TableLayoutPanel_Body_Left_Function
        '
        Me.TableLayoutPanel_Body_Left_Function.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_ErrorCode, 0, 4)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Reset, 0, 9)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_StationID, 0, 1)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Enable, 0, 2)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_StationID, 0, 0)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Modify, 0, 8)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_ExpectNumber, 0, 6)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_ExpectNumber, 0, 7)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiComboBox_Function_Enable, 0, 3)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiComboBox_Function_ErrorCode, 0, 5)
        Me.TableLayoutPanel_Body_Left_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Function.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Left_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Function.Name = "TableLayoutPanel_Body_Left_Function"
        Me.TableLayoutPanel_Body_Left_Function.RowCount = 11
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.Size = New System.Drawing.Size(82, 426)
        Me.TableLayoutPanel_Body_Left_Function.TabIndex = 0
        '
        'HmiLabel_Function_ErrorCode
        '
        Me.HmiLabel_Function_ErrorCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_ErrorCode.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel_Function_ErrorCode.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_ErrorCode.Name = "HmiLabel_Function_ErrorCode"
        Me.HmiLabel_Function_ErrorCode.Size = New System.Drawing.Size(80, 28)
        Me.HmiLabel_Function_ErrorCode.TabIndex = 12
        '
        'HmiButton_Function_Reset
        '
        Me.HmiButton_Function_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Reset.Location = New System.Drawing.Point(0, 270)
        Me.HmiButton_Function_Reset.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Reset.MarginHeight = 6
        Me.HmiButton_Function_Reset.Name = "HmiButton_Function_Reset"
        Me.HmiButton_Function_Reset.Size = New System.Drawing.Size(82, 30)
        Me.HmiButton_Function_Reset.TabIndex = 8
        '
        'HmiTextBox_Function_StationID
        '
        Me.HmiTextBox_Function_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_StationID.Location = New System.Drawing.Point(0, 30)
        Me.HmiTextBox_Function_StationID.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_StationID.Name = "HmiTextBox_Function_StationID"
        Me.HmiTextBox_Function_StationID.Number = 0
        Me.HmiTextBox_Function_StationID.Size = New System.Drawing.Size(82, 30)
        Me.HmiTextBox_Function_StationID.TabIndex = 4
        Me.HmiTextBox_Function_StationID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_StationID.ValueType = GetType(String)
        '
        'HmiLabel_Function_Enable
        '
        Me.HmiLabel_Function_Enable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Enable.Location = New System.Drawing.Point(1, 61)
        Me.HmiLabel_Function_Enable.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Enable.Name = "HmiLabel_Function_Enable"
        Me.HmiLabel_Function_Enable.Size = New System.Drawing.Size(80, 28)
        Me.HmiLabel_Function_Enable.TabIndex = 2
        '
        'HmiLabel_Function_StationID
        '
        Me.HmiLabel_Function_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_StationID.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Function_StationID.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_StationID.Name = "HmiLabel_Function_StationID"
        Me.HmiLabel_Function_StationID.Size = New System.Drawing.Size(80, 28)
        Me.HmiLabel_Function_StationID.TabIndex = 1
        '
        'HmiButton_Function_Modify
        '
        Me.HmiButton_Function_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Modify.Location = New System.Drawing.Point(0, 240)
        Me.HmiButton_Function_Modify.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Modify.MarginHeight = 6
        Me.HmiButton_Function_Modify.Name = "HmiButton_Function_Modify"
        Me.HmiButton_Function_Modify.Size = New System.Drawing.Size(82, 30)
        Me.HmiButton_Function_Modify.TabIndex = 6
        '
        'HmiLabel_Function_ExpectNumber
        '
        Me.HmiLabel_Function_ExpectNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_ExpectNumber.Location = New System.Drawing.Point(1, 181)
        Me.HmiLabel_Function_ExpectNumber.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_ExpectNumber.Name = "HmiLabel_Function_ExpectNumber"
        Me.HmiLabel_Function_ExpectNumber.Size = New System.Drawing.Size(80, 28)
        Me.HmiLabel_Function_ExpectNumber.TabIndex = 10
        '
        'HmiTextBox_Function_ExpectNumber
        '
        Me.HmiTextBox_Function_ExpectNumber.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ExpectNumber.Location = New System.Drawing.Point(0, 210)
        Me.HmiTextBox_Function_ExpectNumber.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_ExpectNumber.Name = "HmiTextBox_Function_ExpectNumber"
        Me.HmiTextBox_Function_ExpectNumber.Number = 0
        Me.HmiTextBox_Function_ExpectNumber.Size = New System.Drawing.Size(82, 30)
        Me.HmiTextBox_Function_ExpectNumber.TabIndex = 11
        Me.HmiTextBox_Function_ExpectNumber.TextBoxReadOnly = False
        Me.HmiTextBox_Function_ExpectNumber.ValueType = GetType(String)
        '
        'HmiComboBox_Function_Enable
        '
        Me.HmiComboBox_Function_Enable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_Enable.Location = New System.Drawing.Point(0, 90)
        Me.HmiComboBox_Function_Enable.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiComboBox_Function_Enable.Name = "HmiComboBox_Function_Enable"
        Me.HmiComboBox_Function_Enable.Size = New System.Drawing.Size(82, 30)
        Me.HmiComboBox_Function_Enable.TabIndex = 14
        '
        'HmiComboBox_Function_ErrorCode
        '
        Me.HmiComboBox_Function_ErrorCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_ErrorCode.Location = New System.Drawing.Point(0, 150)
        Me.HmiComboBox_Function_ErrorCode.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiComboBox_Function_ErrorCode.Name = "HmiComboBox_Function_ErrorCode"
        Me.HmiComboBox_Function_ErrorCode.Size = New System.Drawing.Size(82, 30)
        Me.HmiComboBox_Function_ErrorCode.TabIndex = 15
        '
        'ChildrenStationError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenStationError"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenStationError"
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
    Friend WithEvents HmiLabel_StationID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Left_Function As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiTextBox_Function_StationID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_Enable As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Function_StationID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Function_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiLabel_Function_ExpectNumber As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_ExpectNumber As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Function_Reset As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiLabel_Function_ErrorCode As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiComboBox_StationID As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Function_Enable As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Function_ErrorCode As Kochi.HMI.MainControl.UI.HMIComboBox
End Class
