<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenDevicesManagerForm
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
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Left = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Devices = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Devices = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Left_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Down = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.Button_Del = New System.Windows.Forms.Button()
        Me.Button_Up = New System.Windows.Forms.Button()
        Me.TreeView_Devices = New Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown(Me.components)
        Me.GroupBox_Parameter = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Property = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Mid_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_Name = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Name = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Type = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Type = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.GroupBox_Init = New System.Windows.Forms.GroupBox()
        Me.Panel_Init = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body_Mid_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.HmiLabel_StationID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_StationID = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Index = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Index = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Left.SuspendLayout()
        Me.GroupBox_Devices.SuspendLayout()
        Me.TableLayoutPanel_Body_Left_Devices.SuspendLayout()
        Me.TableLayoutPanel_Body_Left_Bottom.SuspendLayout()
        Me.GroupBox_Parameter.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.GroupBox_Property.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Head.SuspendLayout()
        Me.GroupBox_Init.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Bottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.Controls.Add(Me.TableLayoutPanel_Body_Left)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(467, 530)
        Me.Panel_Body.TabIndex = 1
        '
        'TableLayoutPanel_Body_Left
        '
        Me.TableLayoutPanel_Body_Left.ColumnCount = 2
        Me.TableLayoutPanel_Body_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Left.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.GroupBox_Devices, 0, 0)
        Me.TableLayoutPanel_Body_Left.Controls.Add(Me.GroupBox_Parameter, 1, 0)
        Me.TableLayoutPanel_Body_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left.Name = "TableLayoutPanel_Body_Left"
        Me.TableLayoutPanel_Body_Left.RowCount = 1
        Me.TableLayoutPanel_Body_Left.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body_Left.TabIndex = 1
        '
        'GroupBox_Devices
        '
        Me.GroupBox_Devices.Controls.Add(Me.TableLayoutPanel_Body_Left_Devices)
        Me.GroupBox_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Devices.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Devices.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Devices.Name = "GroupBox_Devices"
        Me.GroupBox_Devices.Size = New System.Drawing.Size(134, 524)
        Me.GroupBox_Devices.TabIndex = 0
        Me.GroupBox_Devices.TabStop = False
        Me.GroupBox_Devices.Text = "Select a Device"
        '
        'TableLayoutPanel_Body_Left_Devices
        '
        Me.TableLayoutPanel_Body_Left_Devices.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Devices.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Devices.Controls.Add(Me.TableLayoutPanel_Body_Left_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Left_Devices.Controls.Add(Me.TreeView_Devices, 0, 0)
        Me.TableLayoutPanel_Body_Left_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Devices.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Left_Devices.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Devices.Name = "TableLayoutPanel_Body_Left_Devices"
        Me.TableLayoutPanel_Body_Left_Devices.RowCount = 2
        Me.TableLayoutPanel_Body_Left_Devices.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.0!))
        Me.TableLayoutPanel_Body_Left_Devices.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Me.TableLayoutPanel_Body_Left_Devices.Size = New System.Drawing.Size(128, 498)
        Me.TableLayoutPanel_Body_Left_Devices.TabIndex = 0
        '
        'TableLayoutPanel_Body_Left_Bottom
        '
        Me.TableLayoutPanel_Body_Left_Bottom.ColumnCount = 2
        Me.TableLayoutPanel_Body_Left_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.Controls.Add(Me.Button_Down, 1, 0)
        Me.TableLayoutPanel_Body_Left_Bottom.Controls.Add(Me.Button_Add, 0, 1)
        Me.TableLayoutPanel_Body_Left_Bottom.Controls.Add(Me.Button_Del, 1, 1)
        Me.TableLayoutPanel_Body_Left_Bottom.Controls.Add(Me.Button_Up, 0, 0)
        Me.TableLayoutPanel_Body_Left_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Bottom.Location = New System.Drawing.Point(0, 423)
        Me.TableLayoutPanel_Body_Left_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Bottom.Name = "TableLayoutPanel_Body_Left_Bottom"
        Me.TableLayoutPanel_Body_Left_Bottom.RowCount = 2
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.Size = New System.Drawing.Size(128, 75)
        Me.TableLayoutPanel_Body_Left_Bottom.TabIndex = 2
        '
        'Button_Down
        '
        Me.Button_Down.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Down.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Down.Location = New System.Drawing.Point(67, 3)
        Me.Button_Down.Name = "Button_Down"
        Me.Button_Down.Size = New System.Drawing.Size(58, 31)
        Me.Button_Down.TabIndex = 3
        Me.Button_Down.Text = "↓"
        Me.Button_Down.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Add.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Add.Location = New System.Drawing.Point(3, 40)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(58, 32)
        Me.Button_Add.TabIndex = 0
        Me.Button_Add.Text = "+"
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'Button_Del
        '
        Me.Button_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Del.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Del.Location = New System.Drawing.Point(67, 40)
        Me.Button_Del.Name = "Button_Del"
        Me.Button_Del.Size = New System.Drawing.Size(58, 32)
        Me.Button_Del.TabIndex = 1
        Me.Button_Del.Text = "-"
        Me.Button_Del.UseVisualStyleBackColor = True
        '
        'Button_Up
        '
        Me.Button_Up.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Up.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Up.Location = New System.Drawing.Point(3, 3)
        Me.Button_Up.Name = "Button_Up"
        Me.Button_Up.Size = New System.Drawing.Size(58, 31)
        Me.Button_Up.TabIndex = 2
        Me.Button_Up.Text = "↑"
        Me.Button_Up.UseVisualStyleBackColor = True
        '
        'TreeView_Devices
        '
        Me.TreeView_Devices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView_Devices.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText
        Me.TreeView_Devices.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TreeView_Devices.HideSelection = False
        Me.TreeView_Devices.Location = New System.Drawing.Point(3, 3)
        Me.TreeView_Devices.Name = "TreeView_Devices"
        Me.TreeView_Devices.Size = New System.Drawing.Size(122, 417)
        Me.TreeView_Devices.TabIndex = 0
        '
        'GroupBox_Parameter
        '
        Me.GroupBox_Parameter.AutoSize = True
        Me.GroupBox_Parameter.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.GroupBox_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Parameter.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Parameter.Location = New System.Drawing.Point(143, 3)
        Me.GroupBox_Parameter.Name = "GroupBox_Parameter"
        Me.GroupBox_Parameter.Size = New System.Drawing.Size(321, 524)
        Me.GroupBox_Parameter.TabIndex = 1
        Me.GroupBox_Parameter.TabStop = False
        Me.GroupBox_Parameter.Text = "Parameter"
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.AutoSize = True
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.GroupBox_Property, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.GroupBox_Init, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_Bottom, 0, 2)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 3
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 228.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(315, 498)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'GroupBox_Property
        '
        Me.GroupBox_Property.AutoSize = True
        Me.GroupBox_Property.Controls.Add(Me.TableLayoutPanel_Body_Mid_Head)
        Me.GroupBox_Property.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Property.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Property.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Property.Name = "GroupBox_Property"
        Me.GroupBox_Property.Size = New System.Drawing.Size(309, 222)
        Me.GroupBox_Property.TabIndex = 0
        Me.GroupBox_Property.TabStop = False
        Me.GroupBox_Property.Text = "Property"
        '
        'TableLayoutPanel_Body_Mid_Head
        '
        Me.TableLayoutPanel_Body_Mid_Head.AutoSize = True
        Me.TableLayoutPanel_Body_Mid_Head.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Index, 1, 1)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Index, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiComboBox_StationID, 1, 3)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_StationID, 0, 3)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_ID, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Name, 1, 2)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Name, 0, 2)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Type, 0, 4)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiComboBox_Type, 1, 4)
        Me.TableLayoutPanel_Body_Mid_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Mid_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Head.Name = "TableLayoutPanel_Body_Mid_Head"
        Me.TableLayoutPanel_Body_Mid_Head.RowCount = 5
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.Size = New System.Drawing.Size(303, 196)
        Me.TableLayoutPanel_Body_Mid_Head.TabIndex = 0
        '
        'HmiLabel_ID
        '
        Me.HmiLabel_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_ID.Name = "HmiLabel_ID"
        Me.HmiLabel_ID.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_ID.TabIndex = 6
        '
        'HmiTextBox_ID
        '
        Me.HmiTextBox_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ID.Location = New System.Drawing.Point(78, 3)
        Me.HmiTextBox_ID.Name = "HmiTextBox_ID"
        Me.HmiTextBox_ID.Number = 0
        Me.HmiTextBox_ID.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_ID.TabIndex = 7
        Me.HmiTextBox_ID.TextBoxReadOnly = False
        Me.HmiTextBox_ID.ValueType = GetType(String)
        '
        'HmiTextBox_Name
        '
        Me.HmiTextBox_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Name.Location = New System.Drawing.Point(79, 83)
        Me.HmiTextBox_Name.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_Name.Name = "HmiTextBox_Name"
        Me.HmiTextBox_Name.Number = 0
        Me.HmiTextBox_Name.Size = New System.Drawing.Size(143, 29)
        Me.HmiTextBox_Name.TabIndex = 8
        Me.HmiTextBox_Name.TextBoxReadOnly = False
        Me.HmiTextBox_Name.ValueType = GetType(String)
        '
        'HmiLabel_Name
        '
        Me.HmiLabel_Name.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Name.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Name.Name = "HmiLabel_Name"
        Me.HmiLabel_Name.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Name.TabIndex = 10
        '
        'HmiLabel_Type
        '
        Me.HmiLabel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Type.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_Type.Name = "HmiLabel_Type"
        Me.HmiLabel_Type.Size = New System.Drawing.Size(69, 34)
        Me.HmiLabel_Type.TabIndex = 11
        '
        'HmiComboBox_Type
        '
        Me.HmiComboBox_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Type.Location = New System.Drawing.Point(78, 159)
        Me.HmiComboBox_Type.Name = "HmiComboBox_Type"
        Me.HmiComboBox_Type.Size = New System.Drawing.Size(145, 34)
        Me.HmiComboBox_Type.TabIndex = 12
        '
        'GroupBox_Init
        '
        Me.GroupBox_Init.Controls.Add(Me.Panel_Init)
        Me.GroupBox_Init.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Init.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Init.Location = New System.Drawing.Point(3, 231)
        Me.GroupBox_Init.Name = "GroupBox_Init"
        Me.GroupBox_Init.Size = New System.Drawing.Size(309, 210)
        Me.GroupBox_Init.TabIndex = 1
        Me.GroupBox_Init.TabStop = False
        Me.GroupBox_Init.Text = "Init"
        '
        'Panel_Init
        '
        Me.Panel_Init.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Init.Location = New System.Drawing.Point(3, 23)
        Me.Panel_Init.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Init.Name = "Panel_Init"
        Me.Panel_Init.Size = New System.Drawing.Size(303, 184)
        Me.Panel_Init.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid_Bottom
        '
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnCount = 5
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.Controls.Add(Me.Button_Save, 1, 0)
        Me.TableLayoutPanel_Body_Mid_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Bottom.Location = New System.Drawing.Point(0, 444)
        Me.TableLayoutPanel_Body_Mid_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Bottom.Name = "TableLayoutPanel_Body_Mid_Bottom"
        Me.TableLayoutPanel_Body_Mid_Bottom.RowCount = 1
        Me.TableLayoutPanel_Body_Mid_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid_Bottom.Size = New System.Drawing.Size(315, 54)
        Me.TableLayoutPanel_Body_Mid_Bottom.TabIndex = 2
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Button_Save.Location = New System.Drawing.Point(66, 3)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(57, 48)
        Me.Button_Save.TabIndex = 0
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'HmiLabel_StationID
        '
        Me.HmiLabel_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StationID.Location = New System.Drawing.Point(3, 120)
        Me.HmiLabel_StationID.Name = "HmiLabel_StationID"
        Me.HmiLabel_StationID.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_StationID.TabIndex = 13
        '
        'HmiComboBox_StationID
        '
        Me.HmiComboBox_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_StationID.Location = New System.Drawing.Point(78, 120)
        Me.HmiComboBox_StationID.Name = "HmiComboBox_StationID"
        Me.HmiComboBox_StationID.Size = New System.Drawing.Size(145, 33)
        Me.HmiComboBox_StationID.TabIndex = 14
        '
        'HmiLabel_Index
        '
        Me.HmiLabel_Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Index.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Index.Name = "HmiLabel_Index"
        Me.HmiLabel_Index.Size = New System.Drawing.Size(69, 33)
        Me.HmiLabel_Index.TabIndex = 15
        '
        'HmiTextBox_Index
        '
        Me.HmiTextBox_Index.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Index.Location = New System.Drawing.Point(78, 42)
        Me.HmiTextBox_Index.Name = "HmiTextBox_Index"
        Me.HmiTextBox_Index.Number = 0
        Me.HmiTextBox_Index.Size = New System.Drawing.Size(145, 33)
        Me.HmiTextBox_Index.TabIndex = 16
        Me.HmiTextBox_Index.TextBoxReadOnly = False
        Me.HmiTextBox_Index.ValueType = GetType(String)
        '
        'ChildrenDevicesManagerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenDevicesManagerForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DevicesManagerForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left.PerformLayout()
        Me.GroupBox_Devices.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Devices.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Bottom.ResumeLayout(False)
        Me.GroupBox_Parameter.ResumeLayout(False)
        Me.GroupBox_Parameter.PerformLayout()
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.PerformLayout()
        Me.GroupBox_Property.ResumeLayout(False)
        Me.GroupBox_Property.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_Head.ResumeLayout(False)
        Me.GroupBox_Init.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid_Bottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Left As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Devices As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Left_Devices As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TreeView_Devices As Kochi.HMI.MainControl.UI.TreeViewWidthKeyDown
    Friend WithEvents GroupBox_Parameter As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Property As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Mid_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Init As System.Windows.Forms.GroupBox
    Friend WithEvents Panel_Init As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body_Mid_Bottom As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button_Save As System.Windows.Forms.Button
    Friend WithEvents HmiLabel_ID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Name As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Type As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Type As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_Name As Kochi.HMI.MainControl.UI.HMITextBox
    Public WithEvents TableLayoutPanel_Body_Left_Bottom As System.Windows.Forms.TableLayoutPanel
    Public WithEvents Button_Down As System.Windows.Forms.Button
    Public WithEvents Button_Add As System.Windows.Forms.Button
    Public WithEvents Button_Del As System.Windows.Forms.Button
    Public WithEvents Button_Up As System.Windows.Forms.Button
    Friend WithEvents HmiTextBox_Index As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Index As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_StationID As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_StationID As Kochi.HMI.MainControl.UI.HMILabel
End Class
