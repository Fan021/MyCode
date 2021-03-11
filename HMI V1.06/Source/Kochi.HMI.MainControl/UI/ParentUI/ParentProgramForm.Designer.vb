<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ParentProgramForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ParentProgramForm))
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TabControl_Program = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_UI_Bottom = New System.Windows.Forms.Panel()
        Me.Panel_UI = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel_Mid = New System.Windows.Forms.Panel()
        Me.Panel_Left = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox_Parameter = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiLabel_Detail = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Description2 = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.HmiLabel_Description2 = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_ActionType = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiTextBox_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Type = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Component = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Number = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Number = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Description = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Picture = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Description = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.HmiTextBox_Picture = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.HmiLabel_Repeat = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Repeat = New Kochi.HMI.MainControl.UI.HMITextBoxWithButton()
        Me.TableLayoutPanel_Head_Detail = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_N = New System.Windows.Forms.RadioButton()
        Me.RadioButton_Y = New System.Windows.Forms.RadioButton()
        Me.HmiButton_Choose = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Component = New Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer()
        Me.GroupBox_Action = New System.Windows.Forms.GroupBox()
        Me.Panel_Action = New System.Windows.Forms.Panel()
        Me.GroupBox_Ts = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Action = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Left_Bottom = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Down = New System.Windows.Forms.Button()
        Me.Button_Add = New System.Windows.Forms.Button()
        Me.Button_Del = New System.Windows.Forms.Button()
        Me.Button_Up = New System.Windows.Forms.Button()
        Me.TabControl_TS = New Kochi.HMI.MainControl.UI.HMITabControl(Me.components)
        Me.Panel_Right = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Right_Item = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.Button_SaveAs = New System.Windows.Forms.Button()
        Me.HmiButton_ExchangeCopy = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_StationCopy = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StationCopy = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Exchange = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_ExchangeTarget = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_ExchangeOrigin = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_StationCopy = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.Button_Save = New System.Windows.Forms.Button()
        Me.HmiButton_CopyConfirm = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiComboBox_CopyVariant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_CopyVariant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiButton_Confirm = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.ContextMenuStrip_Menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PopupCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopupPaste = New System.Windows.Forms.ToolStripMenuItem()
        Me.PopupPasteAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogIni = New System.Windows.Forms.SaveFileDialog()
        Me.Panel_Body.SuspendLayout()
        Me.TabControl_Program.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel.SuspendLayout()
        Me.Panel_UI_Bottom.SuspendLayout()
        Me.Panel_UI.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.Panel_Mid.SuspendLayout()
        Me.Panel_Left.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox_Parameter.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid_Head.SuspendLayout()
        Me.TableLayoutPanel_Head_Detail.SuspendLayout()
        Me.GroupBox_Action.SuspendLayout()
        Me.GroupBox_Ts.SuspendLayout()
        Me.TableLayoutPanel_Body_Left_Action.SuspendLayout()
        Me.TableLayoutPanel_Body_Left_Bottom.SuspendLayout()
        Me.Panel_Right.SuspendLayout()
        Me.TableLayoutPanel_Right_Item.SuspendLayout()
        Me.ContextMenuStrip_Menu.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Body
        '
        Me.Panel_Body.BackColor = System.Drawing.Color.White
        Me.Panel_Body.Controls.Add(Me.TabControl_Program)
        Me.Panel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body.Name = "Panel_Body"
        Me.Panel_Body.Size = New System.Drawing.Size(623, 530)
        Me.Panel_Body.TabIndex = 0
        '
        'TabControl_Program
        '
        Me.TabControl_Program.Controls.Add(Me.TabPage1)
        Me.TabControl_Program.Controls.Add(Me.TabPage2)
        Me.TabControl_Program.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_Program.Location = New System.Drawing.Point(0, 0)
        Me.TabControl_Program.Name = "TabControl_Program"
        Me.TabControl_Program.SelectedIndex = 0
        Me.TabControl_Program.Size = New System.Drawing.Size(623, 530)
        Me.TabControl_Program.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(615, 504)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.ColumnCount = 1
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel.Controls.Add(Me.Panel_UI_Bottom, 0, 0)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 1
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(609, 498)
        Me.TableLayoutPanel.TabIndex = 1
        '
        'Panel_UI_Bottom
        '
        Me.Panel_UI_Bottom.BackColor = System.Drawing.Color.Transparent
        Me.Panel_UI_Bottom.Controls.Add(Me.Panel_UI)
        Me.Panel_UI_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI_Bottom.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI_Bottom.Name = "Panel_UI_Bottom"
        Me.Panel_UI_Bottom.Size = New System.Drawing.Size(609, 498)
        Me.Panel_UI_Bottom.TabIndex = 0
        '
        'Panel_UI
        '
        Me.Panel_UI.BackColor = System.Drawing.Color.Transparent
        Me.Panel_UI.Controls.Add(Me.TableLayoutPanel_Body)
        Me.Panel_UI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_UI.Location = New System.Drawing.Point(0, 0)
        Me.Panel_UI.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_UI.Name = "Panel_UI"
        Me.Panel_UI.Size = New System.Drawing.Size(609, 498)
        Me.Panel_UI.TabIndex = 0
        '
        'TableLayoutPanel_Body
        '
        Me.TableLayoutPanel_Body.ColumnCount = 2
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.0!))
        Me.TableLayoutPanel_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Mid, 0, 0)
        Me.TableLayoutPanel_Body.Controls.Add(Me.Panel_Right, 1, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 1
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(609, 498)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'Panel_Mid
        '
        Me.Panel_Mid.BackColor = System.Drawing.Color.White
        Me.Panel_Mid.Controls.Add(Me.Panel_Left)
        Me.Panel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Mid.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Mid.Name = "Panel_Mid"
        Me.Panel_Mid.Size = New System.Drawing.Size(487, 498)
        Me.Panel_Mid.TabIndex = 0
        '
        'Panel_Left
        '
        Me.Panel_Left.BackColor = System.Drawing.Color.White
        Me.Panel_Left.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel_Left.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Left.Location = New System.Drawing.Point(0, 0)
        Me.Panel_Left.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Left.Name = "Panel_Left"
        Me.Panel_Left.Size = New System.Drawing.Size(487, 498)
        Me.Panel_Left.TabIndex = 2
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox_Parameter, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox_Ts, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(487, 498)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox_Parameter
        '
        Me.GroupBox_Parameter.AutoSize = True
        Me.GroupBox_Parameter.Controls.Add(Me.TableLayoutPanel_Body_Mid)
        Me.GroupBox_Parameter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Parameter.Location = New System.Drawing.Point(173, 3)
        Me.GroupBox_Parameter.Name = "GroupBox_Parameter"
        Me.GroupBox_Parameter.Size = New System.Drawing.Size(311, 492)
        Me.GroupBox_Parameter.TabIndex = 2
        Me.GroupBox_Parameter.TabStop = False
        Me.GroupBox_Parameter.Text = "Parameter"
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.AutoSize = True
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid_Head, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.GroupBox_Action, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 3
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 391.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(305, 469)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid_Head
        '
        Me.TableLayoutPanel_Body_Mid_Head.AutoSize = True
        Me.TableLayoutPanel_Body_Mid_Head.ColumnCount = 3
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Mid_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Detail, 0, 8)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_ID, 0, 0)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Description2, 1, 3)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Description2, 0, 3)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiComboBox_ActionType, 1, 7)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_ID, 1, 0)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Type, 0, 7)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Component, 0, 5)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Number, 0, 1)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Number, 1, 1)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Description, 0, 2)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Picture, 0, 4)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Description, 1, 2)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Picture, 1, 4)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiLabel_Repeat, 0, 6)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Repeat, 1, 6)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.TableLayoutPanel_Head_Detail, 1, 8)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiButton_Choose, 2, 4)
        Me.TableLayoutPanel_Body_Mid_Head.Controls.Add(Me.HmiTextBox_Component, 1, 5)
        Me.TableLayoutPanel_Body_Mid_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid_Head.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Mid_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid_Head.Name = "TableLayoutPanel_Body_Mid_Head"
        Me.TableLayoutPanel_Body_Mid_Head.RowCount = 10
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Mid_Head.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Mid_Head.Size = New System.Drawing.Size(305, 391)
        Me.TableLayoutPanel_Body_Mid_Head.TabIndex = 2
        '
        'HmiLabel_Detail
        '
        Me.HmiLabel_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Detail.Location = New System.Drawing.Point(3, 315)
        Me.HmiLabel_Detail.Name = "HmiLabel_Detail"
        Me.HmiLabel_Detail.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Detail.TabIndex = 26
        '
        'HmiLabel_ID
        '
        Me.HmiLabel_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_ID.Name = "HmiLabel_ID"
        Me.HmiLabel_ID.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_ID.TabIndex = 25
        '
        'HmiTextBox_Description2
        '
        Me.HmiTextBox_Description2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Description2.Location = New System.Drawing.Point(94, 120)
        Me.HmiTextBox_Description2.Name = "HmiTextBox_Description2"
        Me.HmiTextBox_Description2.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_Description2.TabIndex = 24
        '
        'HmiLabel_Description2
        '
        Me.HmiLabel_Description2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Description2.Location = New System.Drawing.Point(3, 119)
        Me.HmiLabel_Description2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.HmiLabel_Description2.Name = "HmiLabel_Description2"
        Me.HmiLabel_Description2.Size = New System.Drawing.Size(85, 35)
        Me.HmiLabel_Description2.TabIndex = 23
        '
        'HmiComboBox_ActionType
        '
        Me.HmiComboBox_ActionType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ActionType.Location = New System.Drawing.Point(94, 276)
        Me.HmiComboBox_ActionType.Name = "HmiComboBox_ActionType"
        Me.HmiComboBox_ActionType.Size = New System.Drawing.Size(146, 33)
        Me.HmiComboBox_ActionType.TabIndex = 16
        '
        'HmiTextBox_ID
        '
        Me.HmiTextBox_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ID.Location = New System.Drawing.Point(94, 3)
        Me.HmiTextBox_ID.Name = "HmiTextBox_ID"
        Me.HmiTextBox_ID.Number = 0
        Me.HmiTextBox_ID.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_ID.TabIndex = 17
        Me.HmiTextBox_ID.TextBoxReadOnly = False
        Me.HmiTextBox_ID.ValueType = GetType(String)
        '
        'HmiLabel_Type
        '
        Me.HmiLabel_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Type.Location = New System.Drawing.Point(3, 276)
        Me.HmiLabel_Type.Name = "HmiLabel_Type"
        Me.HmiLabel_Type.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Type.TabIndex = 13
        '
        'HmiLabel_Component
        '
        Me.HmiLabel_Component.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Component.Location = New System.Drawing.Point(3, 198)
        Me.HmiLabel_Component.Name = "HmiLabel_Component"
        Me.HmiLabel_Component.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Component.TabIndex = 12
        '
        'HmiLabel_Number
        '
        Me.HmiLabel_Number.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Number.Location = New System.Drawing.Point(3, 42)
        Me.HmiLabel_Number.Name = "HmiLabel_Number"
        Me.HmiLabel_Number.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Number.TabIndex = 6
        '
        'HmiTextBox_Number
        '
        Me.HmiTextBox_Number.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Number.Location = New System.Drawing.Point(94, 42)
        Me.HmiTextBox_Number.Name = "HmiTextBox_Number"
        Me.HmiTextBox_Number.Number = 0
        Me.HmiTextBox_Number.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_Number.TabIndex = 7
        Me.HmiTextBox_Number.TextBoxReadOnly = False
        Me.HmiTextBox_Number.ValueType = GetType(String)
        '
        'HmiLabel_Description
        '
        Me.HmiLabel_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Description.Location = New System.Drawing.Point(3, 81)
        Me.HmiLabel_Description.Name = "HmiLabel_Description"
        Me.HmiLabel_Description.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Description.TabIndex = 10
        '
        'HmiLabel_Picture
        '
        Me.HmiLabel_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Picture.Location = New System.Drawing.Point(3, 159)
        Me.HmiLabel_Picture.Name = "HmiLabel_Picture"
        Me.HmiLabel_Picture.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Picture.TabIndex = 11
        '
        'HmiTextBox_Description
        '
        Me.HmiTextBox_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Description.Location = New System.Drawing.Point(94, 81)
        Me.HmiTextBox_Description.Name = "HmiTextBox_Description"
        Me.HmiTextBox_Description.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_Description.TabIndex = 18
        '
        'HmiTextBox_Picture
        '
        Me.HmiTextBox_Picture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Picture.Location = New System.Drawing.Point(94, 159)
        Me.HmiTextBox_Picture.Name = "HmiTextBox_Picture"
        Me.HmiTextBox_Picture.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_Picture.TabIndex = 19
        '
        'HmiLabel_Repeat
        '
        Me.HmiLabel_Repeat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Repeat.Location = New System.Drawing.Point(3, 237)
        Me.HmiLabel_Repeat.Name = "HmiLabel_Repeat"
        Me.HmiLabel_Repeat.Size = New System.Drawing.Size(85, 33)
        Me.HmiLabel_Repeat.TabIndex = 21
        '
        'HmiTextBox_Repeat
        '
        Me.HmiTextBox_Repeat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Repeat.Location = New System.Drawing.Point(94, 237)
        Me.HmiTextBox_Repeat.Name = "HmiTextBox_Repeat"
        Me.HmiTextBox_Repeat.Size = New System.Drawing.Size(146, 33)
        Me.HmiTextBox_Repeat.TabIndex = 22
        '
        'TableLayoutPanel_Head_Detail
        '
        Me.TableLayoutPanel_Head_Detail.ColumnCount = 2
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_N, 1, 0)
        Me.TableLayoutPanel_Head_Detail.Controls.Add(Me.RadioButton_Y, 0, 0)
        Me.TableLayoutPanel_Head_Detail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Head_Detail.Location = New System.Drawing.Point(94, 315)
        Me.TableLayoutPanel_Head_Detail.Name = "TableLayoutPanel_Head_Detail"
        Me.TableLayoutPanel_Head_Detail.RowCount = 1
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Head_Detail.Size = New System.Drawing.Size(146, 33)
        Me.TableLayoutPanel_Head_Detail.TabIndex = 27
        '
        'RadioButton_N
        '
        Me.RadioButton_N.AutoSize = True
        Me.RadioButton_N.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_N.Location = New System.Drawing.Point(76, 3)
        Me.RadioButton_N.Name = "RadioButton_N"
        Me.RadioButton_N.Size = New System.Drawing.Size(67, 27)
        Me.RadioButton_N.TabIndex = 1
        Me.RadioButton_N.Text = "N"
        Me.RadioButton_N.UseVisualStyleBackColor = True
        '
        'RadioButton_Y
        '
        Me.RadioButton_Y.AutoSize = True
        Me.RadioButton_Y.Checked = True
        Me.RadioButton_Y.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Y.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Y.Name = "RadioButton_Y"
        Me.RadioButton_Y.Size = New System.Drawing.Size(67, 27)
        Me.RadioButton_Y.TabIndex = 0
        Me.RadioButton_Y.TabStop = True
        Me.RadioButton_Y.Text = "Y"
        Me.RadioButton_Y.UseVisualStyleBackColor = True
        '
        'HmiButton_Choose
        '
        Me.HmiButton_Choose.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Choose.Location = New System.Drawing.Point(246, 159)
        Me.HmiButton_Choose.MarginHeight = 6
        Me.HmiButton_Choose.Name = "HmiButton_Choose"
        Me.HmiButton_Choose.Size = New System.Drawing.Size(56, 33)
        Me.HmiButton_Choose.TabIndex = 28
        '
        'HmiTextBox_Component
        '
        Me.HmiTextBox_Component.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Component.Location = New System.Drawing.Point(94, 199)
        Me.HmiTextBox_Component.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.HmiTextBox_Component.Name = "HmiTextBox_Component"
        Me.HmiTextBox_Component.Size = New System.Drawing.Size(146, 31)
        Me.HmiTextBox_Component.TabIndex = 29
        '
        'GroupBox_Action
        '
        Me.GroupBox_Action.Controls.Add(Me.Panel_Action)
        Me.GroupBox_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Action.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Action.Location = New System.Drawing.Point(3, 394)
        Me.GroupBox_Action.Name = "GroupBox_Action"
        Me.GroupBox_Action.Size = New System.Drawing.Size(299, 72)
        Me.GroupBox_Action.TabIndex = 1
        Me.GroupBox_Action.TabStop = False
        Me.GroupBox_Action.Text = "Action"
        '
        'Panel_Action
        '
        Me.Panel_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Action.Location = New System.Drawing.Point(3, 20)
        Me.Panel_Action.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Action.Name = "Panel_Action"
        Me.Panel_Action.Size = New System.Drawing.Size(293, 49)
        Me.Panel_Action.TabIndex = 0
        '
        'GroupBox_Ts
        '
        Me.GroupBox_Ts.Controls.Add(Me.TableLayoutPanel_Body_Left_Action)
        Me.GroupBox_Ts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Ts.Font = New System.Drawing.Font("Calibri", 10.0!)
        Me.GroupBox_Ts.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Ts.Name = "GroupBox_Ts"
        Me.GroupBox_Ts.Size = New System.Drawing.Size(164, 492)
        Me.GroupBox_Ts.TabIndex = 0
        Me.GroupBox_Ts.TabStop = False
        Me.GroupBox_Ts.Text = "Select a Action"
        '
        'TableLayoutPanel_Body_Left_Action
        '
        Me.TableLayoutPanel_Body_Left_Action.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Action.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Action.Controls.Add(Me.TableLayoutPanel_Body_Left_Bottom, 0, 1)
        Me.TableLayoutPanel_Body_Left_Action.Controls.Add(Me.TabControl_TS, 0, 0)
        Me.TableLayoutPanel_Body_Left_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Action.Location = New System.Drawing.Point(3, 20)
        Me.TableLayoutPanel_Body_Left_Action.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Action.Name = "TableLayoutPanel_Body_Left_Action"
        Me.TableLayoutPanel_Body_Left_Action.RowCount = 2
        Me.TableLayoutPanel_Body_Left_Action.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.0!))
        Me.TableLayoutPanel_Body_Left_Action.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.0!))
        Me.TableLayoutPanel_Body_Left_Action.Size = New System.Drawing.Size(158, 469)
        Me.TableLayoutPanel_Body_Left_Action.TabIndex = 0
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
        Me.TableLayoutPanel_Body_Left_Bottom.Location = New System.Drawing.Point(0, 408)
        Me.TableLayoutPanel_Body_Left_Bottom.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Bottom.Name = "TableLayoutPanel_Body_Left_Bottom"
        Me.TableLayoutPanel_Body_Left_Bottom.RowCount = 2
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Bottom.Size = New System.Drawing.Size(158, 61)
        Me.TableLayoutPanel_Body_Left_Bottom.TabIndex = 1
        '
        'Button_Down
        '
        Me.Button_Down.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Down.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Down.Location = New System.Drawing.Point(82, 3)
        Me.Button_Down.Name = "Button_Down"
        Me.Button_Down.Size = New System.Drawing.Size(73, 24)
        Me.Button_Down.TabIndex = 3
        Me.Button_Down.Text = "↓"
        Me.Button_Down.UseVisualStyleBackColor = True
        '
        'Button_Add
        '
        Me.Button_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Add.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Add.Location = New System.Drawing.Point(3, 33)
        Me.Button_Add.Name = "Button_Add"
        Me.Button_Add.Size = New System.Drawing.Size(73, 25)
        Me.Button_Add.TabIndex = 0
        Me.Button_Add.Text = "+"
        Me.Button_Add.UseVisualStyleBackColor = True
        '
        'Button_Del
        '
        Me.Button_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Del.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Button_Del.Location = New System.Drawing.Point(82, 33)
        Me.Button_Del.Name = "Button_Del"
        Me.Button_Del.Size = New System.Drawing.Size(73, 25)
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
        Me.Button_Up.Size = New System.Drawing.Size(73, 24)
        Me.Button_Up.TabIndex = 2
        Me.Button_Up.Text = "↑"
        Me.Button_Up.UseVisualStyleBackColor = True
        '
        'TabControl_TS
        '
        Me.TabControl_TS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_TS.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl_TS.Location = New System.Drawing.Point(3, 3)
        Me.TabControl_TS.Name = "TabControl_TS"
        Me.TabControl_TS.SelectedIndex = 0
        Me.TabControl_TS.Size = New System.Drawing.Size(152, 402)
        Me.TabControl_TS.TabIndex = 2
        '
        'Panel_Right
        '
        Me.Panel_Right.BackColor = System.Drawing.Color.Transparent
        Me.Panel_Right.Controls.Add(Me.TableLayoutPanel_Right_Item)
        Me.Panel_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Right.Location = New System.Drawing.Point(487, 0)
        Me.Panel_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Right.Name = "Panel_Right"
        Me.Panel_Right.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.Panel_Right.Size = New System.Drawing.Size(122, 498)
        Me.Panel_Right.TabIndex = 1
        '
        'TableLayoutPanel_Right_Item
        '
        Me.TableLayoutPanel_Right_Item.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel_Right_Item.ColumnCount = 1
        Me.TableLayoutPanel_Right_Item.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.Button_SaveAs, 0, 14)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiButton_ExchangeCopy, 0, 12)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiButton_StationCopy, 0, 8)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiLabel_StationCopy, 0, 6)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiLabel_Exchange, 0, 9)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiComboBox_ExchangeTarget, 0, 11)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiComboBox_ExchangeOrigin, 0, 10)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiComboBox_StationCopy, 0, 7)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.Button_Save, 0, 13)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiButton_CopyConfirm, 0, 5)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiComboBox_CopyVariant, 0, 4)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiLabel_CopyVariant, 0, 3)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiLabel_Variant, 0, 0)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiComboBox_Variant, 0, 1)
        Me.TableLayoutPanel_Right_Item.Controls.Add(Me.HmiButton_Confirm, 0, 2)
        Me.TableLayoutPanel_Right_Item.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Right_Item.Location = New System.Drawing.Point(2, 0)
        Me.TableLayoutPanel_Right_Item.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Right_Item.Name = "TableLayoutPanel_Right_Item"
        Me.TableLayoutPanel_Right_Item.RowCount = 16
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Right_Item.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Right_Item.Size = New System.Drawing.Size(120, 498)
        Me.TableLayoutPanel_Right_Item.TabIndex = 1
        '
        'Button_SaveAs
        '
        Me.Button_SaveAs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_SaveAs.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Button_SaveAs.Location = New System.Drawing.Point(3, 283)
        Me.Button_SaveAs.Name = "Button_SaveAs"
        Me.Button_SaveAs.Size = New System.Drawing.Size(114, 14)
        Me.Button_SaveAs.TabIndex = 16
        Me.Button_SaveAs.Text = "Save"
        Me.Button_SaveAs.UseVisualStyleBackColor = True
        '
        'HmiButton_ExchangeCopy
        '
        Me.HmiButton_ExchangeCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_ExchangeCopy.Location = New System.Drawing.Point(1, 241)
        Me.HmiButton_ExchangeCopy.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_ExchangeCopy.MarginHeight = 6
        Me.HmiButton_ExchangeCopy.Name = "HmiButton_ExchangeCopy"
        Me.HmiButton_ExchangeCopy.Size = New System.Drawing.Size(118, 18)
        Me.HmiButton_ExchangeCopy.TabIndex = 15
        '
        'HmiButton_StationCopy
        '
        Me.HmiButton_StationCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_StationCopy.Location = New System.Drawing.Point(1, 161)
        Me.HmiButton_StationCopy.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_StationCopy.MarginHeight = 6
        Me.HmiButton_StationCopy.Name = "HmiButton_StationCopy"
        Me.HmiButton_StationCopy.Size = New System.Drawing.Size(118, 18)
        Me.HmiButton_StationCopy.TabIndex = 14
        '
        'HmiLabel_StationCopy
        '
        Me.HmiLabel_StationCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StationCopy.Location = New System.Drawing.Point(3, 123)
        Me.HmiLabel_StationCopy.Name = "HmiLabel_StationCopy"
        Me.HmiLabel_StationCopy.Size = New System.Drawing.Size(114, 14)
        Me.HmiLabel_StationCopy.TabIndex = 13
        '
        'HmiLabel_Exchange
        '
        Me.HmiLabel_Exchange.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Exchange.Location = New System.Drawing.Point(3, 183)
        Me.HmiLabel_Exchange.Name = "HmiLabel_Exchange"
        Me.HmiLabel_Exchange.Size = New System.Drawing.Size(114, 14)
        Me.HmiLabel_Exchange.TabIndex = 12
        '
        'HmiComboBox_ExchangeTarget
        '
        Me.HmiComboBox_ExchangeTarget.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ExchangeTarget.Location = New System.Drawing.Point(3, 223)
        Me.HmiComboBox_ExchangeTarget.Name = "HmiComboBox_ExchangeTarget"
        Me.HmiComboBox_ExchangeTarget.Size = New System.Drawing.Size(114, 14)
        Me.HmiComboBox_ExchangeTarget.TabIndex = 10
        '
        'HmiComboBox_ExchangeOrigin
        '
        Me.HmiComboBox_ExchangeOrigin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ExchangeOrigin.Location = New System.Drawing.Point(3, 203)
        Me.HmiComboBox_ExchangeOrigin.Name = "HmiComboBox_ExchangeOrigin"
        Me.HmiComboBox_ExchangeOrigin.Size = New System.Drawing.Size(114, 14)
        Me.HmiComboBox_ExchangeOrigin.TabIndex = 9
        '
        'HmiComboBox_StationCopy
        '
        Me.HmiComboBox_StationCopy.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_StationCopy.Location = New System.Drawing.Point(3, 143)
        Me.HmiComboBox_StationCopy.Name = "HmiComboBox_StationCopy"
        Me.HmiComboBox_StationCopy.Size = New System.Drawing.Size(114, 14)
        Me.HmiComboBox_StationCopy.TabIndex = 7
        '
        'Button_Save
        '
        Me.Button_Save.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Save.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.Button_Save.Location = New System.Drawing.Point(3, 263)
        Me.Button_Save.Name = "Button_Save"
        Me.Button_Save.Size = New System.Drawing.Size(114, 14)
        Me.Button_Save.TabIndex = 6
        Me.Button_Save.Text = "Save"
        Me.Button_Save.UseVisualStyleBackColor = True
        '
        'HmiButton_CopyConfirm
        '
        Me.HmiButton_CopyConfirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_CopyConfirm.Location = New System.Drawing.Point(1, 101)
        Me.HmiButton_CopyConfirm.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_CopyConfirm.MarginHeight = 6
        Me.HmiButton_CopyConfirm.Name = "HmiButton_CopyConfirm"
        Me.HmiButton_CopyConfirm.Size = New System.Drawing.Size(118, 18)
        Me.HmiButton_CopyConfirm.TabIndex = 5
        '
        'HmiComboBox_CopyVariant
        '
        Me.HmiComboBox_CopyVariant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CopyVariant.Location = New System.Drawing.Point(3, 83)
        Me.HmiComboBox_CopyVariant.Name = "HmiComboBox_CopyVariant"
        Me.HmiComboBox_CopyVariant.Size = New System.Drawing.Size(114, 14)
        Me.HmiComboBox_CopyVariant.TabIndex = 3
        '
        'HmiLabel_CopyVariant
        '
        Me.HmiLabel_CopyVariant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CopyVariant.Location = New System.Drawing.Point(3, 63)
        Me.HmiLabel_CopyVariant.Name = "HmiLabel_CopyVariant"
        Me.HmiLabel_CopyVariant.Size = New System.Drawing.Size(114, 14)
        Me.HmiLabel_CopyVariant.TabIndex = 2
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(114, 14)
        Me.HmiLabel_Variant.TabIndex = 0
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(3, 23)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(114, 14)
        Me.HmiComboBox_Variant.TabIndex = 1
        '
        'HmiButton_Confirm
        '
        Me.HmiButton_Confirm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Confirm.Location = New System.Drawing.Point(1, 41)
        Me.HmiButton_Confirm.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Confirm.MarginHeight = 6
        Me.HmiButton_Confirm.Name = "HmiButton_Confirm"
        Me.HmiButton_Confirm.Size = New System.Drawing.Size(118, 18)
        Me.HmiButton_Confirm.TabIndex = 4
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(615, 504)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip_Menu
        '
        Me.ContextMenuStrip_Menu.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.ContextMenuStrip_Menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PopupCopy, Me.PopupPaste, Me.PopupPasteAll})
        Me.ContextMenuStrip_Menu.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip_Menu.Size = New System.Drawing.Size(153, 98)
        '
        'PopupCopy
        '
        Me.PopupCopy.Image = CType(resources.GetObject("PopupCopy.Image"), System.Drawing.Image)
        Me.PopupCopy.Name = "PopupCopy"
        Me.PopupCopy.Size = New System.Drawing.Size(152, 24)
        Me.PopupCopy.Text = "Copy "
        '
        'PopupPaste
        '
        Me.PopupPaste.Image = CType(resources.GetObject("PopupPaste.Image"), System.Drawing.Image)
        Me.PopupPaste.Name = "PopupPaste"
        Me.PopupPaste.Size = New System.Drawing.Size(131, 24)
        Me.PopupPaste.Text = "Paste"
        '
        'PopupPasteAll
        '
        Me.PopupPasteAll.Enabled = False
        Me.PopupPasteAll.Name = "PopupPasteAll"
        Me.PopupPasteAll.Size = New System.Drawing.Size(152, 24)
        Me.PopupPasteAll.Text = "PasteAll"
        '
        'ParentProgramForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ParentProgramForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ProgramForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TabControl_Program.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.Panel_UI_Bottom.ResumeLayout(False)
        Me.Panel_UI.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.Panel_Mid.ResumeLayout(False)
        Me.Panel_Left.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox_Parameter.ResumeLayout(False)
        Me.GroupBox_Parameter.PerformLayout()
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.PerformLayout()
        Me.TableLayoutPanel_Body_Mid_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.ResumeLayout(False)
        Me.TableLayoutPanel_Head_Detail.PerformLayout()
        Me.GroupBox_Action.ResumeLayout(False)
        Me.GroupBox_Ts.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Action.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Left_Bottom.ResumeLayout(False)
        Me.Panel_Right.ResumeLayout(False)
        Me.TableLayoutPanel_Right_Item.ResumeLayout(False)
        Me.ContextMenuStrip_Menu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel_Body As System.Windows.Forms.Panel
    Public WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Public WithEvents ContextMenuStrip_Menu As System.Windows.Forms.ContextMenuStrip
    Public WithEvents PopupCopy As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents PopupPaste As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogIni As System.Windows.Forms.SaveFileDialog
    Friend WithEvents TabControl_Program As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Public WithEvents Panel_UI_Bottom As System.Windows.Forms.Panel
    Public WithEvents Panel_UI As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Public WithEvents Panel_Mid As System.Windows.Forms.Panel
    Public WithEvents Panel_Left As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Public WithEvents GroupBox_Parameter As System.Windows.Forms.GroupBox
    Public WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Public WithEvents GroupBox_Action As System.Windows.Forms.GroupBox
    Public WithEvents Panel_Action As System.Windows.Forms.Panel
    Public WithEvents GroupBox_Ts As System.Windows.Forms.GroupBox
    Public WithEvents TableLayoutPanel_Body_Left_Action As System.Windows.Forms.TableLayoutPanel
    Public WithEvents TableLayoutPanel_Body_Left_Bottom As System.Windows.Forms.TableLayoutPanel
    Public WithEvents Button_Down As System.Windows.Forms.Button
    Public WithEvents Button_Add As System.Windows.Forms.Button
    Public WithEvents Button_Del As System.Windows.Forms.Button
    Public WithEvents Button_Up As System.Windows.Forms.Button
    Public WithEvents Panel_Right As System.Windows.Forms.Panel
    Public WithEvents TableLayoutPanel_Right_Item As HMI.MainControl.UI.HMITableLayoutPanel
    Public WithEvents Button_SaveAs As System.Windows.Forms.Button
    Public WithEvents HmiButton_ExchangeCopy As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents HmiButton_StationCopy As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents HmiLabel_StationCopy As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_Exchange As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiComboBox_ExchangeTarget As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents HmiComboBox_ExchangeOrigin As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents HmiComboBox_StationCopy As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents Button_Save As System.Windows.Forms.Button
    Public WithEvents HmiButton_CopyConfirm As Kochi.HMI.MainControl.UI.HMIButton
    Public WithEvents HmiComboBox_CopyVariant As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents HmiLabel_CopyVariant As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents HmiButton_Confirm As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents TableLayoutPanel_Body_Mid_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Public WithEvents HmiLabel_Detail As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_ID As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiTextBox_Description2 As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Public WithEvents HmiLabel_Description2 As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiComboBox_ActionType As Kochi.HMI.MainControl.UI.HMIComboBox
    Public WithEvents HmiTextBox_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Public WithEvents HmiLabel_Type As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_Component As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_Number As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiTextBox_Number As Kochi.HMI.MainControl.UI.HMITextBox
    Public WithEvents HmiLabel_Description As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiLabel_Picture As Kochi.HMI.MainControl.UI.HMILabel
    Public WithEvents HmiTextBox_Description As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Public WithEvents HmiTextBox_Picture As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
    Friend WithEvents HmiLabel_Repeat As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Repeat As Kochi.HMI.MainControl.UI.HMITextBoxWithButton
    Friend WithEvents TableLayoutPanel_Head_Detail As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RadioButton_N As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton_Y As System.Windows.Forms.RadioButton
    Friend WithEvents HmiButton_Choose As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents TabControl_TS As Kochi.HMI.MainControl.UI.HMITabControl
    Friend WithEvents PopupPasteAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HmiTextBox_Component As Kochi.HMI.MainControl.UI.HMITextBoxWithButtonAnd2Layer
End Class
