Imports Kochi.HMI.MainControl.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenSystemParameterForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChildrenSystemParameterForm))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.TabControl_Body = New System.Windows.Forms.TabControl()
        Me.TabPage_Config = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Config = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Station = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Config_Head_Station = New System.Windows.Forms.TableLayoutPanel()
        Me.PostToolBar = New System.Windows.Forms.ToolStrip()
        Me.PostTest_Add = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Del = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Up = New System.Windows.Forms.ToolStripButton()
        Me.PostTest_Down = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator_Station = New System.Windows.Forms.ToolStripSeparator()
        Me.MachineListView_Data = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.GroupBox_Cell = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Config_Head_Cell = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_Cell_Picture = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Cell_Picture = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Cell_Name = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Cell_Name = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Cell_Description = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Cell_Description = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiButton_Cell_Picture = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.GroupBox_Project = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Config_Head_Project = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiLabel_Project = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Project = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TabPage_Parameter = New System.Windows.Forms.TabPage()
        Me.MachineListView_Parameter = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.TabPage_VariantParameter = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_VariantParameter = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton_AddVariantParameter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton_DelVariantParamete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.GroupBox_Variant = New System.Windows.Forms.GroupBox()
        Me.TreeViewWidthKeyDown_Variant = New Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown(Me.components)
        Me.TabPage_MachineStatus = New System.Windows.Forms.TabPage()
        Me.MachineListView_MachineStatus = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.TabPage_Variant = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel_Body_Variant = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Variant_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.MachineListView_Variant = New Kochi.HMI.MainControl.UI.MachineListView()
        Me.ToolStrip_Variant = New System.Windows.Forms.ToolStrip()
        Me.ListView_Data_Variant_Add = New System.Windows.Forms.ToolStripButton()
        Me.ListView_Data_Variant_Del = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ListView_Data_Variant_Up = New System.Windows.Forms.ToolStripButton()
        Me.ListView_Data_Variant_Down = New System.Windows.Forms.ToolStripButton()
        Me.TabPage_ActionParameter = New System.Windows.Forms.TabPage()
        Me.Panel_ActionParameter = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Paramater = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Parameter = New System.Windows.Forms.GroupBox()
        Me.TreeViewWidthKeyDown_Parameter = New Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown(Me.components)
        Me.Panel_Parameter = New System.Windows.Forms.Panel()
        Me.TabPage_DeviceParameter = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Devices = New System.Windows.Forms.GroupBox()
        Me.TreeViewWidthKeyDown_Devices = New Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown(Me.components)
        Me.Panel_Devices = New System.Windows.Forms.Panel()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Bottom.SuspendLayout()
        Me.TabControl_Body.SuspendLayout()
        Me.TabPage_Config.SuspendLayout()
        Me.TableLayoutPanel_Body_Config.SuspendLayout()
        Me.GroupBox_Station.SuspendLayout()
        Me.TableLayoutPanel_Body_Config_Head_Station.SuspendLayout()
        Me.PostToolBar.SuspendLayout()
        CType(Me.MachineListView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Cell.SuspendLayout()
        Me.TableLayoutPanel_Body_Config_Head_Cell.SuspendLayout()
        Me.GroupBox_Project.SuspendLayout()
        Me.TableLayoutPanel_Body_Config_Head_Project.SuspendLayout()
        Me.TabPage_Parameter.SuspendLayout()
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_VariantParameter.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.MachineListView_VariantParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox_Variant.SuspendLayout()
        Me.TabPage_MachineStatus.SuspendLayout()
        CType(Me.MachineListView_MachineStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Variant.SuspendLayout()
        Me.TableLayoutPanel_Body_Variant.SuspendLayout()
        Me.TableLayoutPanel_Body_Variant_Mid.SuspendLayout()
        CType(Me.MachineListView_Variant, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip_Variant.SuspendLayout()
        Me.TabPage_ActionParameter.SuspendLayout()
        Me.Panel_ActionParameter.SuspendLayout()
        Me.TableLayoutPanel_Paramater.SuspendLayout()
        Me.GroupBox_Parameter.SuspendLayout()
        Me.TabPage_DeviceParameter.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox_Devices.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 1
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Bottom, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.TabControl_Body, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(461, 524)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Bottom
        '
        Me.TableLayoutPanel_Body_Bottom.ColumnCount = 5
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.TableLayoutPanel_Body_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Bottom.Controls.Add(Me.Button_Save, 2, 0)
        Me.TableLayoutPanel_Body_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Bottom.Location = New System.Drawing.Point(0, 445)
        Me.TableLayoutPanel_Body_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Bottom.Name = "TableLayoutPanel_Body_Bottom"
        Me.TableLayoutPanel_Body_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Bottom.Size = New System.Drawing.Size(461, 79)
        Me.TableLayoutPanel_Body_Bottom.TabIndex = 4
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Button_Save.Location = New System.Drawing.Point(196, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(67, 73)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'TabControl_Body
        '
        Me.TabControl_Body.Controls.Add(Me.TabPage_Config)
        Me.TabControl_Body.Controls.Add(Me.TabPage_Parameter)
        Me.TabControl_Body.Controls.Add(Me.TabPage_VariantParameter)
        Me.TabControl_Body.Controls.Add(Me.TabPage_MachineStatus)
        Me.TabControl_Body.Controls.Add(Me.TabPage_Variant)
        Me.TabControl_Body.Controls.Add(Me.TabPage_ActionParameter)
        Me.TabControl_Body.Controls.Add(Me.TabPage_DeviceParameter)
        Me.TabControl_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Body.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabControl_Body.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TabControl_Body.Name = "TabControl_Body"
        Me.TabControl_Body.SelectedIndex = 0
        Me.TabControl_Body.Size = New System.Drawing.Size(461, 445)
        Me.TabControl_Body.TabIndex = 0
        '
        'TabPage_Config
        '
        Me.TabPage_Config.Controls.Add(Me.TableLayoutPanel_Body_Config)
        Me.TabPage_Config.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Config.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Config.Name = "TabPage_Config"
        Me.TabPage_Config.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Config.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_Config.TabIndex = 0
        Me.TabPage_Config.Text = "Configuration"
        Me.TabPage_Config.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Config
        '
        Me.TableLayoutPanel_Body_Config.ColumnCount = 1
        Me.TableLayoutPanel_Body_Config.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Config.Controls.Add(Me.GroupBox_Station, 0, 2)
        Me.TableLayoutPanel_Body_Config.Controls.Add(Me.GroupBox_Cell, 0, 1)
        Me.TableLayoutPanel_Body_Config.Controls.Add(Me.GroupBox_Project, 0, 0)
        Me.TableLayoutPanel_Body_Config.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Config.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Config.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Config.Name = "TableLayoutPanel_Body_Config"
        Me.TableLayoutPanel_Body_Config.RowCount = 3
        Me.TableLayoutPanel_Body_Config.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TableLayoutPanel_Body_Config.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111.0!))
        Me.TableLayoutPanel_Body_Config.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Config.Size = New System.Drawing.Size(447, 407)
        Me.TableLayoutPanel_Body_Config.TabIndex = 0
        '
        'GroupBox_Station
        '
        Me.GroupBox_Station.Controls.Add(Me.TableLayoutPanel_Body_Config_Head_Station)
        Me.GroupBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Station.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Station.Location = New System.Drawing.Point(3, 186)
        Me.GroupBox_Station.Name = "GroupBox_Station"
        Me.GroupBox_Station.Size = New System.Drawing.Size(441, 218)
        Me.GroupBox_Station.TabIndex = 4
        Me.GroupBox_Station.TabStop = False
        Me.GroupBox_Station.Text = "Station"
        '
        'TableLayoutPanel_Body_Config_Head_Station
        '
        Me.TableLayoutPanel_Body_Config_Head_Station.ColumnCount = 1
        Me.TableLayoutPanel_Body_Config_Head_Station.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Config_Head_Station.Controls.Add(Me.PostToolBar, 0, 0)
        Me.TableLayoutPanel_Body_Config_Head_Station.Controls.Add(Me.MachineListView_Data, 0, 1)
        Me.TableLayoutPanel_Body_Config_Head_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Config_Head_Station.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Config_Head_Station.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Config_Head_Station.Name = "TableLayoutPanel_Body_Config_Head_Station"
        Me.TableLayoutPanel_Body_Config_Head_Station.RowCount = 2
        Me.TableLayoutPanel_Body_Config_Head_Station.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Config_Head_Station.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Config_Head_Station.Size = New System.Drawing.Size(435, 192)
        Me.TableLayoutPanel_Body_Config_Head_Station.TabIndex = 0
        '
        'PostToolBar
        '
        Me.PostToolBar.BackColor = System.Drawing.SystemColors.Control
        Me.PostToolBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PostToolBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PostTest_Add, Me.PostTest_Del, Me.PostTest_Up, Me.PostTest_Down, Me.ToolStripSeparator_Station})
        Me.PostToolBar.Location = New System.Drawing.Point(0, 0)
        Me.PostToolBar.Name = "PostToolBar"
        Me.PostToolBar.Size = New System.Drawing.Size(435, 38)
        Me.PostToolBar.TabIndex = 9
        '
        'PostTest_Add
        '
        Me.PostTest_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Add.Image = CType(resources.GetObject("PostTest_Add.Image"), System.Drawing.Image)
        Me.PostTest_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Add.Name = "PostTest_Add"
        Me.PostTest_Add.Size = New System.Drawing.Size(23, 35)
        Me.PostTest_Add.ToolTipText = "add a new command"
        '
        'PostTest_Del
        '
        Me.PostTest_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Del.Image = CType(resources.GetObject("PostTest_Del.Image"), System.Drawing.Image)
        Me.PostTest_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Del.Name = "PostTest_Del"
        Me.PostTest_Del.Size = New System.Drawing.Size(23, 35)
        Me.PostTest_Del.ToolTipText = "delete selected command"
        '
        'PostTest_Up
        '
        Me.PostTest_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Up.Image = CType(resources.GetObject("PostTest_Up.Image"), System.Drawing.Image)
        Me.PostTest_Up.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Up.Name = "PostTest_Up"
        Me.PostTest_Up.Size = New System.Drawing.Size(23, 35)
        Me.PostTest_Up.ToolTipText = "move up"
        '
        'PostTest_Down
        '
        Me.PostTest_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PostTest_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PostTest_Down.Image = CType(resources.GetObject("PostTest_Down.Image"), System.Drawing.Image)
        Me.PostTest_Down.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.PostTest_Down.Name = "PostTest_Down"
        Me.PostTest_Down.Size = New System.Drawing.Size(23, 35)
        Me.PostTest_Down.ToolTipText = "move down"
        '
        'ToolStripSeparator_Station
        '
        Me.ToolStripSeparator_Station.Name = "ToolStripSeparator_Station"
        Me.ToolStripSeparator_Station.Size = New System.Drawing.Size(6, 38)
        '
        'MachineListView_Data
        '
        Me.MachineListView_Data.AllowUserToAddRows = False
        Me.MachineListView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.MachineListView_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Data.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Data.EnableHeadersVisualStyles = False
        Me.MachineListView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Data.Location = New System.Drawing.Point(0, 38)
        Me.MachineListView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Data.Name = "MachineListView_Data"
        Me.MachineListView_Data.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Data.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Data.RowTemplate.Height = 23
        Me.MachineListView_Data.Size = New System.Drawing.Size(435, 154)
        Me.MachineListView_Data.TabIndex = 10
        '
        'GroupBox_Cell
        '
        Me.GroupBox_Cell.Controls.Add(Me.TableLayoutPanel_Body_Config_Head_Cell)
        Me.GroupBox_Cell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Cell.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Cell.Location = New System.Drawing.Point(3, 75)
        Me.GroupBox_Cell.Name = "GroupBox_Cell"
        Me.GroupBox_Cell.Size = New System.Drawing.Size(441, 105)
        Me.GroupBox_Cell.TabIndex = 3
        Me.GroupBox_Cell.TabStop = False
        Me.GroupBox_Cell.Text = "Cell"
        '
        'TableLayoutPanel_Body_Config_Head_Cell
        '
        Me.TableLayoutPanel_Body_Config_Head_Cell.ColumnCount = 4
        Me.TableLayoutPanel_Body_Config_Head_Cell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiTextBox_Cell_Picture, 1, 1)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiLabel_Cell_Picture, 0, 1)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiLabel_Cell_Name, 0, 0)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiTextBox_Cell_Name, 1, 0)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiLabel_Cell_Description, 2, 0)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiTextBox_Cell_Description, 3, 0)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Controls.Add(Me.HmiButton_Cell_Picture, 2, 1)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Config_Head_Cell.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Config_Head_Cell.Name = "TableLayoutPanel_Body_Config_Head_Cell"
        Me.TableLayoutPanel_Body_Config_Head_Cell.RowCount = 2
        Me.TableLayoutPanel_Body_Config_Head_Cell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Config_Head_Cell.Size = New System.Drawing.Size(435, 79)
        Me.TableLayoutPanel_Body_Config_Head_Cell.TabIndex = 0
        '
        'HmiTextBox_Cell_Picture
        '
        Me.HmiTextBox_Cell_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Cell_Picture.Location = New System.Drawing.Point(91, 44)
        Me.HmiTextBox_Cell_Picture.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Cell_Picture.Name = "HmiTextBox_Cell_Picture"
        Me.HmiTextBox_Cell_Picture.Number = 0
        Me.HmiTextBox_Cell_Picture.Size = New System.Drawing.Size(122, 30)
        Me.HmiTextBox_Cell_Picture.TabIndex = 5
        Me.HmiTextBox_Cell_Picture.TextBoxReadOnly = False
        Me.HmiTextBox_Cell_Picture.ValueType = GetType(String)
        '
        'HmiLabel_Cell_Picture
        '
        Me.HmiLabel_Cell_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Cell_Picture.Location = New System.Drawing.Point(4, 44)
        Me.HmiLabel_Cell_Picture.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiLabel_Cell_Picture.Name = "HmiLabel_Cell_Picture"
        Me.HmiLabel_Cell_Picture.Size = New System.Drawing.Size(79, 30)
        Me.HmiLabel_Cell_Picture.TabIndex = 4
        '
        'HmiLabel_Cell_Name
        '
        Me.HmiLabel_Cell_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Cell_Name.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Cell_Name.Name = "HmiLabel_Cell_Name"
        Me.HmiLabel_Cell_Name.Size = New System.Drawing.Size(81, 33)
        Me.HmiLabel_Cell_Name.TabIndex = 0
        '
        'HmiTextBox_Cell_Name
        '
        Me.HmiTextBox_Cell_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Cell_Name.Location = New System.Drawing.Point(90, 3)
        Me.HmiTextBox_Cell_Name.Name = "HmiTextBox_Cell_Name"
        Me.HmiTextBox_Cell_Name.Number = 0
        Me.HmiTextBox_Cell_Name.Size = New System.Drawing.Size(124, 33)
        Me.HmiTextBox_Cell_Name.TabIndex = 1
        Me.HmiTextBox_Cell_Name.TextBoxReadOnly = False
        Me.HmiTextBox_Cell_Name.ValueType = GetType(String)
        '
        'HmiLabel_Cell_Description
        '
        Me.HmiLabel_Cell_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Cell_Description.Location = New System.Drawing.Point(220, 3)
        Me.HmiLabel_Cell_Description.Name = "HmiLabel_Cell_Description"
        Me.HmiLabel_Cell_Description.Size = New System.Drawing.Size(81, 33)
        Me.HmiLabel_Cell_Description.TabIndex = 2
        '
        'HmiTextBox_Cell_Description
        '
        Me.HmiTextBox_Cell_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Cell_Description.Location = New System.Drawing.Point(307, 3)
        Me.HmiTextBox_Cell_Description.Name = "HmiTextBox_Cell_Description"
        Me.HmiTextBox_Cell_Description.Number = 0
        Me.HmiTextBox_Cell_Description.Size = New System.Drawing.Size(125, 33)
        Me.HmiTextBox_Cell_Description.TabIndex = 3
        Me.HmiTextBox_Cell_Description.TextBoxReadOnly = False
        Me.HmiTextBox_Cell_Description.ValueType = GetType(String)
        '
        'HmiButton_Cell_Picture
        '
        Me.HmiButton_Cell_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cell_Picture.Location = New System.Drawing.Point(220, 42)
        Me.HmiButton_Cell_Picture.MarginHeight = 6
        Me.HmiButton_Cell_Picture.Name = "HmiButton_Cell_Picture"
        Me.HmiButton_Cell_Picture.Size = New System.Drawing.Size(81, 34)
        Me.HmiButton_Cell_Picture.TabIndex = 6
        '
        'GroupBox_Project
        '
        Me.GroupBox_Project.Controls.Add(Me.TableLayoutPanel_Body_Config_Head_Project)
        Me.GroupBox_Project.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Project.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Project.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Project.Name = "GroupBox_Project"
        Me.GroupBox_Project.Size = New System.Drawing.Size(441, 66)
        Me.GroupBox_Project.TabIndex = 2
        Me.GroupBox_Project.TabStop = False
        Me.GroupBox_Project.Text = "Project"
        '
        'TableLayoutPanel_Body_Config_Head_Project
        '
        Me.TableLayoutPanel_Body_Config_Head_Project.ColumnCount = 4
        Me.TableLayoutPanel_Body_Config_Head_Project.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Config_Head_Project.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Config_Head_Project.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Config_Head_Project.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Config_Head_Project.Controls.Add(Me.HmiLabel_Project, 0, 0)
        Me.TableLayoutPanel_Body_Config_Head_Project.Controls.Add(Me.HmiTextBox_Project, 1, 0)
        Me.TableLayoutPanel_Body_Config_Head_Project.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Config_Head_Project.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Config_Head_Project.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Config_Head_Project.Name = "TableLayoutPanel_Body_Config_Head_Project"
        Me.TableLayoutPanel_Body_Config_Head_Project.RowCount = 1
        Me.TableLayoutPanel_Body_Config_Head_Project.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Config_Head_Project.Size = New System.Drawing.Size(435, 40)
        Me.TableLayoutPanel_Body_Config_Head_Project.TabIndex = 0
        '
        'HmiLabel_Project
        '
        Me.HmiLabel_Project.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Project.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Project.Name = "HmiLabel_Project"
        Me.HmiLabel_Project.Size = New System.Drawing.Size(81, 34)
        Me.HmiLabel_Project.TabIndex = 0
        '
        'HmiTextBox_Project
        '
        Me.TableLayoutPanel_Body_Config_Head_Project.SetColumnSpan(Me.HmiTextBox_Project, 2)
        Me.HmiTextBox_Project.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Project.Location = New System.Drawing.Point(90, 3)
        Me.HmiTextBox_Project.Name = "HmiTextBox_Project"
        Me.HmiTextBox_Project.Number = 0
        Me.HmiTextBox_Project.Size = New System.Drawing.Size(211, 34)
        Me.HmiTextBox_Project.TabIndex = 1
        Me.HmiTextBox_Project.TextBoxReadOnly = False
        Me.HmiTextBox_Project.ValueType = GetType(String)
        '
        'TabPage_Parameter
        '
        Me.TabPage_Parameter.Controls.Add(Me.MachineListView_Parameter)
        Me.TabPage_Parameter.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.TabPage_Parameter.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Parameter.Name = "TabPage_Parameter"
        Me.TabPage_Parameter.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Parameter.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_Parameter.TabIndex = 1
        Me.TabPage_Parameter.Text = "Parameter"
        Me.TabPage_Parameter.UseVisualStyleBackColor = True
        '
        'MachineListView_Parameter
        '
        Me.MachineListView_Parameter.AllowUserToAddRows = False
        Me.MachineListView_Parameter.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.MachineListView_Parameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Parameter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Parameter.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Parameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Parameter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Parameter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.MachineListView_Parameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Parameter.EnableHeadersVisualStyles = False
        Me.MachineListView_Parameter.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Parameter.Location = New System.Drawing.Point(3, 3)
        Me.MachineListView_Parameter.Name = "MachineListView_Parameter"
        Me.MachineListView_Parameter.RowHeadersVisible = False
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Parameter.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.MachineListView_Parameter.RowTemplate.Height = 80
        Me.MachineListView_Parameter.Size = New System.Drawing.Size(447, 407)
        Me.MachineListView_Parameter.TabIndex = 0
        '
        'TabPage_VariantParameter
        '
        Me.TabPage_VariantParameter.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage_VariantParameter.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_VariantParameter.Name = "TabPage_VariantParameter"
        Me.TabPage_VariantParameter.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_VariantParameter.TabIndex = 6
        Me.TabPage_VariantParameter.Text = "VariantParameter"
        Me.TabPage_VariantParameter.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox_Variant, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 413.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(453, 413)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel4, 0, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(138, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(312, 407)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.MachineListView_VariantParameter, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.ToolStrip1, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(312, 407)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'MachineListView_VariantParameter
        '
        Me.MachineListView_VariantParameter.AllowUserToAddRows = False
        Me.MachineListView_VariantParameter.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_VariantParameter.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.MachineListView_VariantParameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_VariantParameter.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_VariantParameter.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_VariantParameter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_VariantParameter.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_VariantParameter.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.MachineListView_VariantParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_VariantParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_VariantParameter.EnableHeadersVisualStyles = False
        Me.MachineListView_VariantParameter.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_VariantParameter.Location = New System.Drawing.Point(0, 24)
        Me.MachineListView_VariantParameter.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_VariantParameter.Name = "MachineListView_VariantParameter"
        Me.MachineListView_VariantParameter.RowHeadersVisible = False
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_VariantParameter.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.MachineListView_VariantParameter.RowTemplate.Height = 23
        Me.MachineListView_VariantParameter.Size = New System.Drawing.Size(312, 383)
        Me.MachineListView_VariantParameter.TabIndex = 12
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_AddVariantParameter, Me.ToolStripButton_DelVariantParamete, Me.ToolStripSeparator2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(312, 24)
        Me.ToolStrip1.TabIndex = 11
        '
        'ToolStripButton_AddVariantParameter
        '
        Me.ToolStripButton_AddVariantParameter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ToolStripButton_AddVariantParameter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_AddVariantParameter.Image = CType(resources.GetObject("ToolStripButton_AddVariantParameter.Image"), System.Drawing.Image)
        Me.ToolStripButton_AddVariantParameter.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ToolStripButton_AddVariantParameter.Name = "ToolStripButton_AddVariantParameter"
        Me.ToolStripButton_AddVariantParameter.Size = New System.Drawing.Size(23, 21)
        Me.ToolStripButton_AddVariantParameter.ToolTipText = "add a new command"
        '
        'ToolStripButton_DelVariantParamete
        '
        Me.ToolStripButton_DelVariantParamete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ToolStripButton_DelVariantParamete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton_DelVariantParamete.Image = CType(resources.GetObject("ToolStripButton_DelVariantParamete.Image"), System.Drawing.Image)
        Me.ToolStripButton_DelVariantParamete.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ToolStripButton_DelVariantParamete.Name = "ToolStripButton_DelVariantParamete"
        Me.ToolStripButton_DelVariantParamete.Size = New System.Drawing.Size(23, 21)
        Me.ToolStripButton_DelVariantParamete.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 24)
        '
        'GroupBox_Variant
        '
        Me.GroupBox_Variant.Controls.Add(Me.TreeViewWidthKeyDown_Variant)
        Me.GroupBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Variant.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Variant.Name = "GroupBox_Variant"
        Me.GroupBox_Variant.Size = New System.Drawing.Size(129, 407)
        Me.GroupBox_Variant.TabIndex = 0
        Me.GroupBox_Variant.TabStop = False
        Me.GroupBox_Variant.Text = "GroupBox1"
        '
        'TreeViewWidthKeyDown_Variant
        '
        Me.TreeViewWidthKeyDown_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewWidthKeyDown_Variant.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.TreeViewWidthKeyDown_Variant.HideSelection = False
        Me.TreeViewWidthKeyDown_Variant.Location = New System.Drawing.Point(3, 23)
        Me.TreeViewWidthKeyDown_Variant.Name = "TreeViewWidthKeyDown_Variant"
        Me.TreeViewWidthKeyDown_Variant.Size = New System.Drawing.Size(123, 381)
        Me.TreeViewWidthKeyDown_Variant.TabIndex = 0
        '
        'TabPage_MachineStatus
        '
        Me.TabPage_MachineStatus.Controls.Add(Me.MachineListView_MachineStatus)
        Me.TabPage_MachineStatus.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_MachineStatus.Name = "TabPage_MachineStatus"
        Me.TabPage_MachineStatus.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_MachineStatus.TabIndex = 5
        Me.TabPage_MachineStatus.Text = "MachineStatus"
        Me.TabPage_MachineStatus.UseVisualStyleBackColor = True
        '
        'MachineListView_MachineStatus
        '
        Me.MachineListView_MachineStatus.AllowUserToAddRows = False
        Me.MachineListView_MachineStatus.AllowUserToDeleteRows = False
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_MachineStatus.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.MachineListView_MachineStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_MachineStatus.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_MachineStatus.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_MachineStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_MachineStatus.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_MachineStatus.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.MachineListView_MachineStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_MachineStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_MachineStatus.EnableHeadersVisualStyles = False
        Me.MachineListView_MachineStatus.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_MachineStatus.Location = New System.Drawing.Point(0, 0)
        Me.MachineListView_MachineStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_MachineStatus.Name = "MachineListView_MachineStatus"
        Me.MachineListView_MachineStatus.RowHeadersVisible = False
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_MachineStatus.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.MachineListView_MachineStatus.RowTemplate.Height = 23
        Me.MachineListView_MachineStatus.Size = New System.Drawing.Size(453, 413)
        Me.MachineListView_MachineStatus.TabIndex = 11
        '
        'TabPage_Variant
        '
        Me.TabPage_Variant.Controls.Add(Me.TableLayoutPanel_Body_Variant)
        Me.TabPage_Variant.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_Variant.Name = "TabPage_Variant"
        Me.TabPage_Variant.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Variant.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_Variant.TabIndex = 2
        Me.TabPage_Variant.Text = "Variant Element"
        Me.TabPage_Variant.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel_Body_Variant
        '
        Me.TableLayoutPanel_Body_Variant.ColumnCount = 2
        Me.TableLayoutPanel_Body_Variant.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.32892!))
        Me.TableLayoutPanel_Body_Variant.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.67108!))
        Me.TableLayoutPanel_Body_Variant.Controls.Add(Me.TableLayoutPanel_Body_Variant_Mid, 0, 0)
        Me.TableLayoutPanel_Body_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Variant.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel_Body_Variant.Name = "TableLayoutPanel_Body_Variant"
        Me.TableLayoutPanel_Body_Variant.RowCount = 1
        Me.TableLayoutPanel_Body_Variant.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Variant.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 407.0!))
        Me.TableLayoutPanel_Body_Variant.Size = New System.Drawing.Size(447, 407)
        Me.TableLayoutPanel_Body_Variant.TabIndex = 1
        '
        'TableLayoutPanel_Body_Variant_Mid
        '
        Me.TableLayoutPanel_Body_Variant_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Variant_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.Controls.Add(Me.MachineListView_Variant, 0, 1)
        Me.TableLayoutPanel_Body_Variant_Mid.Controls.Add(Me.ToolStrip_Variant, 0, 0)
        Me.TableLayoutPanel_Body_Variant_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Variant_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Variant_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Variant_Mid.Name = "TableLayoutPanel_Body_Variant_Mid"
        Me.TableLayoutPanel_Body_Variant_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Variant_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.0!))
        Me.TableLayoutPanel_Body_Variant_Mid.Size = New System.Drawing.Size(300, 407)
        Me.TableLayoutPanel_Body_Variant_Mid.TabIndex = 0
        '
        'MachineListView_Variant
        '
        Me.MachineListView_Variant.AllowUserToAddRows = False
        Me.MachineListView_Variant.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Variant.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.MachineListView_Variant.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.MachineListView_Variant.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.MachineListView_Variant.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Variant.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Variant.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Variant.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.MachineListView_Variant.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Variant.EnableHeadersVisualStyles = False
        Me.MachineListView_Variant.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Variant.Location = New System.Drawing.Point(0, 24)
        Me.MachineListView_Variant.Margin = New System.Windows.Forms.Padding(0)
        Me.MachineListView_Variant.Name = "MachineListView_Variant"
        Me.MachineListView_Variant.RowHeadersVisible = False
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Variant.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.MachineListView_Variant.RowTemplate.Height = 23
        Me.MachineListView_Variant.Size = New System.Drawing.Size(300, 383)
        Me.MachineListView_Variant.TabIndex = 12
        '
        'ToolStrip_Variant
        '
        Me.ToolStrip_Variant.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip_Variant.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ListView_Data_Variant_Add, Me.ListView_Data_Variant_Del, Me.ToolStripSeparator1, Me.ListView_Data_Variant_Up, Me.ListView_Data_Variant_Down})
        Me.ToolStrip_Variant.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip_Variant.Name = "ToolStrip_Variant"
        Me.ToolStrip_Variant.Size = New System.Drawing.Size(300, 24)
        Me.ToolStrip_Variant.TabIndex = 11
        '
        'ListView_Data_Variant_Add
        '
        Me.ListView_Data_Variant_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Data_Variant_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Data_Variant_Add.Image = CType(resources.GetObject("ListView_Data_Variant_Add.Image"), System.Drawing.Image)
        Me.ListView_Data_Variant_Add.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Data_Variant_Add.Name = "ListView_Data_Variant_Add"
        Me.ListView_Data_Variant_Add.Size = New System.Drawing.Size(23, 21)
        Me.ListView_Data_Variant_Add.ToolTipText = "add a new command"
        '
        'ListView_Data_Variant_Del
        '
        Me.ListView_Data_Variant_Del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Data_Variant_Del.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Data_Variant_Del.Image = CType(resources.GetObject("ListView_Data_Variant_Del.Image"), System.Drawing.Image)
        Me.ListView_Data_Variant_Del.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Data_Variant_Del.Name = "ListView_Data_Variant_Del"
        Me.ListView_Data_Variant_Del.Size = New System.Drawing.Size(23, 21)
        Me.ListView_Data_Variant_Del.ToolTipText = "delete selected command"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 24)
        '
        'ListView_Data_Variant_Up
        '
        Me.ListView_Data_Variant_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Data_Variant_Up.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Data_Variant_Up.Image = CType(resources.GetObject("ListView_Data_Variant_Up.Image"), System.Drawing.Image)
        Me.ListView_Data_Variant_Up.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Data_Variant_Up.Name = "ListView_Data_Variant_Up"
        Me.ListView_Data_Variant_Up.Size = New System.Drawing.Size(23, 21)
        Me.ListView_Data_Variant_Up.ToolTipText = "move up"
        '
        'ListView_Data_Variant_Down
        '
        Me.ListView_Data_Variant_Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ListView_Data_Variant_Down.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ListView_Data_Variant_Down.Image = CType(resources.GetObject("ListView_Data_Variant_Down.Image"), System.Drawing.Image)
        Me.ListView_Data_Variant_Down.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.ListView_Data_Variant_Down.Name = "ListView_Data_Variant_Down"
        Me.ListView_Data_Variant_Down.Size = New System.Drawing.Size(23, 21)
        Me.ListView_Data_Variant_Down.ToolTipText = "move down"
        '
        'TabPage_ActionParameter
        '
        Me.TabPage_ActionParameter.Controls.Add(Me.Panel_ActionParameter)
        Me.TabPage_ActionParameter.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_ActionParameter.Name = "TabPage_ActionParameter"
        Me.TabPage_ActionParameter.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_ActionParameter.TabIndex = 3
        Me.TabPage_ActionParameter.Text = "ActionParameter"
        Me.TabPage_ActionParameter.UseVisualStyleBackColor = True
        '
        'Panel_ActionParameter
        '
        Me.Panel_ActionParameter.Controls.Add(Me.TableLayoutPanel_Paramater)
        Me.Panel_ActionParameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_ActionParameter.Location = New System.Drawing.Point(0, 0)
        Me.Panel_ActionParameter.Name = "Panel_ActionParameter"
        Me.Panel_ActionParameter.Size = New System.Drawing.Size(453, 413)
        Me.Panel_ActionParameter.TabIndex = 0
        '
        'TableLayoutPanel_Paramater
        '
        Me.TableLayoutPanel_Paramater.ColumnCount = 2
        Me.TableLayoutPanel_Paramater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Paramater.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Paramater.Controls.Add(Me.GroupBox_Parameter, 0, 0)
        Me.TableLayoutPanel_Paramater.Controls.Add(Me.Panel_Parameter, 1, 0)
        Me.TableLayoutPanel_Paramater.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Paramater.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Paramater.Name = "TableLayoutPanel_Paramater"
        Me.TableLayoutPanel_Paramater.RowCount = 1
        Me.TableLayoutPanel_Paramater.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Paramater.Size = New System.Drawing.Size(453, 413)
        Me.TableLayoutPanel_Paramater.TabIndex = 0
        '
        'GroupBox_Parameter
        '
        Me.GroupBox_Parameter.Controls.Add(Me.TreeViewWidthKeyDown_Parameter)
        Me.GroupBox_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Parameter.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Parameter.Name = "GroupBox_Parameter"
        Me.GroupBox_Parameter.Size = New System.Drawing.Size(129, 407)
        Me.GroupBox_Parameter.TabIndex = 0
        Me.GroupBox_Parameter.TabStop = False
        Me.GroupBox_Parameter.Text = "GroupBox1"
        '
        'TreeViewWidthKeyDown_Parameter
        '
        Me.TreeViewWidthKeyDown_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewWidthKeyDown_Parameter.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.TreeViewWidthKeyDown_Parameter.HideSelection = False
        Me.TreeViewWidthKeyDown_Parameter.Location = New System.Drawing.Point(3, 23)
        Me.TreeViewWidthKeyDown_Parameter.Name = "TreeViewWidthKeyDown_Parameter"
        Me.TreeViewWidthKeyDown_Parameter.Size = New System.Drawing.Size(123, 381)
        Me.TreeViewWidthKeyDown_Parameter.TabIndex = 0
        '
        'Panel_Parameter
        '
        Me.Panel_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Parameter.Location = New System.Drawing.Point(138, 3)
        Me.Panel_Parameter.Name = "Panel_Parameter"
        Me.Panel_Parameter.Size = New System.Drawing.Size(312, 407)
        Me.Panel_Parameter.TabIndex = 1
        '
        'TabPage_DeviceParameter
        '
        Me.TabPage_DeviceParameter.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage_DeviceParameter.Location = New System.Drawing.Point(4, 28)
        Me.TabPage_DeviceParameter.Name = "TabPage_DeviceParameter"
        Me.TabPage_DeviceParameter.Size = New System.Drawing.Size(453, 413)
        Me.TabPage_DeviceParameter.TabIndex = 4
        Me.TabPage_DeviceParameter.Text = "DeviceParameter"
        Me.TabPage_DeviceParameter.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox_Devices, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel_Devices, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(453, 413)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'GroupBox_Devices
        '
        Me.GroupBox_Devices.Controls.Add(Me.TreeViewWidthKeyDown_Devices)
        Me.GroupBox_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Devices.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Devices.Name = "GroupBox_Devices"
        Me.GroupBox_Devices.Size = New System.Drawing.Size(129, 407)
        Me.GroupBox_Devices.TabIndex = 0
        Me.GroupBox_Devices.TabStop = False
        Me.GroupBox_Devices.Text = "GroupBox1"
        '
        'TreeViewWidthKeyDown_Devices
        '
        Me.TreeViewWidthKeyDown_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewWidthKeyDown_Devices.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.TreeViewWidthKeyDown_Devices.HideSelection = False
        Me.TreeViewWidthKeyDown_Devices.Location = New System.Drawing.Point(3, 23)
        Me.TreeViewWidthKeyDown_Devices.Name = "TreeViewWidthKeyDown_Devices"
        Me.TreeViewWidthKeyDown_Devices.Size = New System.Drawing.Size(123, 381)
        Me.TreeViewWidthKeyDown_Devices.TabIndex = 0
        '
        'Panel_Devices
        '
        Me.Panel_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Devices.Location = New System.Drawing.Point(138, 3)
        Me.Panel_Devices.Name = "Panel_Devices"
        Me.Panel_Devices.Size = New System.Drawing.Size(312, 407)
        Me.Panel_Devices.TabIndex = 1
        '
        'ChildrenSystemParameterForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenSystemParameterForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SubSystemParameterForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Bottom.ResumeLayout(False)
        Me.TabControl_Body.ResumeLayout(False)
        Me.TabPage_Config.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Config.ResumeLayout(False)
        Me.GroupBox_Station.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Config_Head_Station.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Config_Head_Station.PerformLayout()
        Me.PostToolBar.ResumeLayout(False)
        Me.PostToolBar.PerformLayout()
        CType(Me.MachineListView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Cell.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Config_Head_Cell.ResumeLayout(False)
        Me.GroupBox_Project.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Config_Head_Project.ResumeLayout(False)
        Me.TabPage_Parameter.ResumeLayout(False)
        CType(Me.MachineListView_Parameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_VariantParameter.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        CType(Me.MachineListView_VariantParameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox_Variant.ResumeLayout(False)
        Me.TabPage_MachineStatus.ResumeLayout(False)
        CType(Me.MachineListView_MachineStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Variant.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Variant_Mid.PerformLayout()
        CType(Me.MachineListView_Variant, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip_Variant.ResumeLayout(False)
        Me.ToolStrip_Variant.PerformLayout()
        Me.TabPage_ActionParameter.ResumeLayout(False)
        Me.Panel_ActionParameter.ResumeLayout(False)
        Me.TableLayoutPanel_Paramater.ResumeLayout(False)
        Me.GroupBox_Parameter.ResumeLayout(False)
        Me.TabPage_DeviceParameter.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox_Devices.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl_Body As System.Windows.Forms.TabControl
    Friend WithEvents TabPage_Config As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_Parameter As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Config As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Project As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Config_Head_Project As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_Project As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Project As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents GroupBox_Cell As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Config_Head_Cell As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_Cell_Name As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Cell_Name As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Cell_Description As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Cell_Description As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents GroupBox_Station As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Config_Head_Station As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PostToolBar As System.Windows.Forms.ToolStrip
    Friend WithEvents PostTest_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Up As System.Windows.Forms.ToolStripButton
    Friend WithEvents PostTest_Down As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator_Station As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MachineListView_Data As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents TableLayoutPanel_Body_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents MachineListView_Parameter As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents TabPage_Variant As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_ActionParameter As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel_Body_Variant As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel_Body_Variant_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_Variant As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents ToolStrip_Variant As System.Windows.Forms.ToolStrip
    Friend WithEvents ListView_Data_Variant_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView_Data_Variant_Del As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ListView_Data_Variant_Up As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView_Data_Variant_Down As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel_ActionParameter As System.Windows.Forms.Panel
    Friend WithEvents HmiTextBox_Cell_Picture As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Cell_Picture As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Cell_Picture As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabPage_DeviceParameter As System.Windows.Forms.TabPage
    Friend WithEvents TabPage_MachineStatus As System.Windows.Forms.TabPage
    Friend WithEvents MachineListView_MachineStatus As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents TableLayoutPanel_Paramater As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Parameter As System.Windows.Forms.GroupBox
    Friend WithEvents TreeViewWidthKeyDown_Parameter As Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown
    Friend WithEvents Panel_Parameter As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Devices As System.Windows.Forms.GroupBox
    Friend WithEvents TreeViewWidthKeyDown_Devices As Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown
    Friend WithEvents Panel_Devices As System.Windows.Forms.Panel
    Friend WithEvents TabPage_VariantParameter As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Variant As System.Windows.Forms.GroupBox
    Friend WithEvents TreeViewWidthKeyDown_Variant As Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MachineListView_VariantParameter As Kochi.HMI.MainControl.UI.MachineListView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton_AddVariantParameter As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton_DelVariantParamete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
