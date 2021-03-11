<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenVariantForm
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
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Variant = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TableLayoutPanel_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Function = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_Function_GlobalProgram = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Function_Program = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Function_Program = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_Program = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_Description2 = New System.Windows.Forms.TextBox()
        Me.HmiLabel_Function_Description2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Function_Del = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Function_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Function_Description = New System.Windows.Forms.TextBox()
        Me.HmiTextBox_Function_Variant = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_Description = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Function_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Function_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Function_Add = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Function_Pic = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Function_Pic = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_Pic = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiComboBox_Function_GlobalProgram = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.Timer_Show = New System.Windows.Forms.Timer(Me.components)
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
        Me.TableLayoutPanel_Body_Head.ColumnCount = 6
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_ID, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Variant, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_Variant, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 1
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 40)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiLabel_ID
        '
        Me.HmiLabel_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_ID.Name = "HmiLabel_ID"
        Me.HmiLabel_ID.Size = New System.Drawing.Size(62, 34)
        Me.HmiLabel_ID.TabIndex = 0
        '
        'HmiTextBox_ID
        '
        Me.HmiTextBox_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ID.Location = New System.Drawing.Point(71, 3)
        Me.HmiTextBox_ID.Name = "HmiTextBox_ID"
        Me.HmiTextBox_ID.Number = 0
        Me.HmiTextBox_ID.Size = New System.Drawing.Size(85, 34)
        Me.HmiTextBox_ID.TabIndex = 1
        Me.HmiTextBox_ID.TextBoxReadOnly = False
        Me.HmiTextBox_ID.ValueType = GetType(String)
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(162, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(62, 34)
        Me.HmiLabel_Variant.TabIndex = 2
        '
        'HmiTextBox_Variant
        '
        Me.HmiTextBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Variant.Location = New System.Drawing.Point(230, 3)
        Me.HmiTextBox_Variant.Name = "HmiTextBox_Variant"
        Me.HmiTextBox_Variant.Number = 0
        Me.HmiTextBox_Variant.Size = New System.Drawing.Size(85, 34)
        Me.HmiTextBox_Variant.TabIndex = 3
        Me.HmiTextBox_Variant.TextBoxReadOnly = False
        Me.HmiTextBox_Variant.ValueType = GetType(String)
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(321, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(62, 34)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(389, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(63, 34)
        Me.HmiButton_Cancel.TabIndex = 5
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
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
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
        'HmiDataView_Data
        '
        Me.HmiDataView_Data.AllowUserToAddRows = False
        Me.HmiDataView_Data.AllowUserToDeleteRows = False
        Me.HmiDataView_Data.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
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
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.TableLayoutPanel_Body_Left_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Function.Location = New System.Drawing.Point(376, 0)
        Me.GroupBox_Function.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.GroupBox_Function.Size = New System.Drawing.Size(88, 458)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "Function"
        '
        'TableLayoutPanel_Body_Left_Function
        '
        Me.TableLayoutPanel_Body_Left_Function.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_GlobalProgram, 0, 14)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Program, 0, 13)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Program, 0, 12)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Program, 0, 11)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Description2, 0, 7)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Description2, 0, 6)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Del, 0, 18)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Modify, 0, 17)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Description, 0, 5)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Variant, 0, 3)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Description, 0, 4)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Variant, 0, 2)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_ID, 0, 0)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_ID, 0, 1)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Add, 0, 16)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Pic, 0, 10)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_Pic, 0, 8)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_Pic, 0, 9)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiComboBox_Function_GlobalProgram, 0, 15)
        Me.TableLayoutPanel_Body_Left_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Function.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_Body_Left_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Function.Name = "TableLayoutPanel_Body_Left_Function"
        Me.TableLayoutPanel_Body_Left_Function.RowCount = 20
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.Size = New System.Drawing.Size(82, 438)
        Me.TableLayoutPanel_Body_Left_Function.TabIndex = 0
        '
        'HmiLabel_Function_GlobalProgram
        '
        Me.HmiLabel_Function_GlobalProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_GlobalProgram.Location = New System.Drawing.Point(1, 283)
        Me.HmiLabel_Function_GlobalProgram.Margin = New System.Windows.Forms.Padding(1, 3, 1, 3)
        Me.HmiLabel_Function_GlobalProgram.Name = "HmiLabel_Function_GlobalProgram"
        Me.HmiLabel_Function_GlobalProgram.Size = New System.Drawing.Size(80, 14)
        Me.HmiLabel_Function_GlobalProgram.TabIndex = 17
        '
        'HmiButton_Function_Program
        '
        Me.HmiButton_Function_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Program.Location = New System.Drawing.Point(0, 260)
        Me.HmiButton_Function_Program.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Program.MarginHeight = 6
        Me.HmiButton_Function_Program.Name = "HmiButton_Function_Program"
        Me.HmiButton_Function_Program.Size = New System.Drawing.Size(82, 20)
        Me.HmiButton_Function_Program.TabIndex = 16
        '
        'HmiTextBox_Function_Program
        '
        Me.HmiTextBox_Function_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Program.Location = New System.Drawing.Point(0, 240)
        Me.HmiTextBox_Function_Program.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Program.Name = "HmiTextBox_Function_Program"
        Me.HmiTextBox_Function_Program.Number = 0
        Me.HmiTextBox_Function_Program.Size = New System.Drawing.Size(82, 20)
        Me.HmiTextBox_Function_Program.TabIndex = 15
        Me.HmiTextBox_Function_Program.TextBoxReadOnly = False
        Me.HmiTextBox_Function_Program.ValueType = GetType(String)
        '
        'HmiLabel_Function_Program
        '
        Me.HmiLabel_Function_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Program.Location = New System.Drawing.Point(1, 222)
        Me.HmiLabel_Function_Program.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.HmiLabel_Function_Program.Name = "HmiLabel_Function_Program"
        Me.HmiLabel_Function_Program.Size = New System.Drawing.Size(80, 16)
        Me.HmiLabel_Function_Program.TabIndex = 14
        '
        'HmiTextBox_Function_Description2
        '
        Me.HmiTextBox_Function_Description2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Description2.Location = New System.Drawing.Point(0, 140)
        Me.HmiTextBox_Function_Description2.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Description2.Name = "HmiTextBox_Function_Description2"
        Me.HmiTextBox_Function_Description2.Size = New System.Drawing.Size(82, 27)
        Me.HmiTextBox_Function_Description2.TabIndex = 13
        '
        'HmiLabel_Function_Description2
        '
        Me.HmiLabel_Function_Description2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Description2.Location = New System.Drawing.Point(1, 121)
        Me.HmiLabel_Function_Description2.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Description2.Name = "HmiLabel_Function_Description2"
        Me.HmiLabel_Function_Description2.Size = New System.Drawing.Size(80, 18)
        Me.HmiLabel_Function_Description2.TabIndex = 12
        '
        'HmiButton_Function_Del
        '
        Me.HmiButton_Function_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Del.Location = New System.Drawing.Point(0, 360)
        Me.HmiButton_Function_Del.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Del.MarginHeight = 6
        Me.HmiButton_Function_Del.Name = "HmiButton_Function_Del"
        Me.HmiButton_Function_Del.Size = New System.Drawing.Size(82, 20)
        Me.HmiButton_Function_Del.TabIndex = 8
        '
        'HmiButton_Function_Modify
        '
        Me.HmiButton_Function_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Modify.Location = New System.Drawing.Point(0, 340)
        Me.HmiButton_Function_Modify.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Modify.MarginHeight = 6
        Me.HmiButton_Function_Modify.Name = "HmiButton_Function_Modify"
        Me.HmiButton_Function_Modify.Size = New System.Drawing.Size(82, 20)
        Me.HmiButton_Function_Modify.TabIndex = 7
        '
        'HmiTextBox_Function_Description
        '
        Me.HmiTextBox_Function_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Description.Location = New System.Drawing.Point(0, 100)
        Me.HmiTextBox_Function_Description.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Description.Name = "HmiTextBox_Function_Description"
        Me.HmiTextBox_Function_Description.Size = New System.Drawing.Size(82, 27)
        Me.HmiTextBox_Function_Description.TabIndex = 5
        '
        'HmiTextBox_Function_Variant
        '
        Me.HmiTextBox_Function_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Variant.Location = New System.Drawing.Point(0, 60)
        Me.HmiTextBox_Function_Variant.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Variant.Name = "HmiTextBox_Function_Variant"
        Me.HmiTextBox_Function_Variant.Number = 0
        Me.HmiTextBox_Function_Variant.Size = New System.Drawing.Size(82, 20)
        Me.HmiTextBox_Function_Variant.TabIndex = 4
        Me.HmiTextBox_Function_Variant.TextBoxReadOnly = False
        Me.HmiTextBox_Function_Variant.ValueType = GetType(String)
        '
        'HmiLabel_Function_Description
        '
        Me.HmiLabel_Function_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Description.Location = New System.Drawing.Point(1, 81)
        Me.HmiLabel_Function_Description.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Description.Name = "HmiLabel_Function_Description"
        Me.HmiLabel_Function_Description.Size = New System.Drawing.Size(80, 18)
        Me.HmiLabel_Function_Description.TabIndex = 2
        '
        'HmiLabel_Function_Variant
        '
        Me.HmiLabel_Function_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Variant.Location = New System.Drawing.Point(0, 40)
        Me.HmiLabel_Function_Variant.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiLabel_Function_Variant.Name = "HmiLabel_Function_Variant"
        Me.HmiLabel_Function_Variant.Size = New System.Drawing.Size(82, 20)
        Me.HmiLabel_Function_Variant.TabIndex = 1
        '
        'HmiLabel_Function_ID
        '
        Me.HmiLabel_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_ID.Location = New System.Drawing.Point(1, 1)
        Me.HmiLabel_Function_ID.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_ID.Name = "HmiLabel_Function_ID"
        Me.HmiLabel_Function_ID.Size = New System.Drawing.Size(80, 18)
        Me.HmiLabel_Function_ID.TabIndex = 0
        '
        'HmiTextBox_Function_ID
        '
        Me.HmiTextBox_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ID.Location = New System.Drawing.Point(0, 20)
        Me.HmiTextBox_Function_ID.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_ID.Name = "HmiTextBox_Function_ID"
        Me.HmiTextBox_Function_ID.Number = 0
        Me.HmiTextBox_Function_ID.Size = New System.Drawing.Size(82, 20)
        Me.HmiTextBox_Function_ID.TabIndex = 3
        Me.HmiTextBox_Function_ID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_ID.ValueType = GetType(String)
        '
        'HmiButton_Function_Add
        '
        Me.HmiButton_Function_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Add.Location = New System.Drawing.Point(0, 320)
        Me.HmiButton_Function_Add.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Add.MarginHeight = 6
        Me.HmiButton_Function_Add.Name = "HmiButton_Function_Add"
        Me.HmiButton_Function_Add.Size = New System.Drawing.Size(82, 20)
        Me.HmiButton_Function_Add.TabIndex = 6
        '
        'HmiButton_Function_Pic
        '
        Me.HmiButton_Function_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Pic.Location = New System.Drawing.Point(0, 200)
        Me.HmiButton_Function_Pic.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Pic.MarginHeight = 6
        Me.HmiButton_Function_Pic.Name = "HmiButton_Function_Pic"
        Me.HmiButton_Function_Pic.Size = New System.Drawing.Size(82, 20)
        Me.HmiButton_Function_Pic.TabIndex = 9
        '
        'HmiLabel_Function_Pic
        '
        Me.HmiLabel_Function_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Pic.Location = New System.Drawing.Point(1, 161)
        Me.HmiLabel_Function_Pic.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiLabel_Function_Pic.Name = "HmiLabel_Function_Pic"
        Me.HmiLabel_Function_Pic.Size = New System.Drawing.Size(80, 18)
        Me.HmiLabel_Function_Pic.TabIndex = 10
        '
        'HmiTextBox_Function_Pic
        '
        Me.HmiTextBox_Function_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_Pic.Location = New System.Drawing.Point(0, 180)
        Me.HmiTextBox_Function_Pic.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_Pic.Name = "HmiTextBox_Function_Pic"
        Me.HmiTextBox_Function_Pic.Number = 0
        Me.HmiTextBox_Function_Pic.Size = New System.Drawing.Size(82, 20)
        Me.HmiTextBox_Function_Pic.TabIndex = 11
        Me.HmiTextBox_Function_Pic.TextBoxReadOnly = False
        Me.HmiTextBox_Function_Pic.ValueType = GetType(String)
        '
        'HmiComboBox_Function_GlobalProgram
        '
        Me.HmiComboBox_Function_GlobalProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_GlobalProgram.Location = New System.Drawing.Point(1, 301)
        Me.HmiComboBox_Function_GlobalProgram.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiComboBox_Function_GlobalProgram.Name = "HmiComboBox_Function_GlobalProgram"
        Me.HmiComboBox_Function_GlobalProgram.Size = New System.Drawing.Size(80, 18)
        Me.HmiComboBox_Function_GlobalProgram.TabIndex = 18
        '
        'Timer_Show
        '
        Me.Timer_Show.Interval = 500
        '
        'ChildrenVariantForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenVariantForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VariantForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Function.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Function.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Function.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_ID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Variant As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Left_Function As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiButton_Function_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Function_Description As TextBox
    Friend WithEvents HmiTextBox_Function_Variant As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_Description As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Function_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Function_ID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Function_Add As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiButton_Function_Pic As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Function_Pic As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_Pic As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButton_Function_Del As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiTextBox_Function_Description2 As TextBox
    Friend WithEvents HmiLabel_Function_Description2 As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents HmiButton_Function_Program As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_Function_Program As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_Program As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Function_GlobalProgram As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Function_GlobalProgram As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents Timer_Show As System.Windows.Forms.Timer
End Class
