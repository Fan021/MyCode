<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IOForm
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel_Body_Bottom_Right = New System.Windows.Forms.Panel()
        Me.HmiTableLayoutPanel_Body_Top_Right = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiButton_AbortCarrierStation = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_CarrierStation = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiSensor_Error = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiLabel_Error = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Present = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_CarrierID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Carrier = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_CarrierID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_CarrierID = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiTextBox_Variant = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiSensor_Present = New Kochi.HMI.MainControl.UI.HMISensor()
        Me.HmiButton_ResetCarrierStation = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_CarrierStation = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.HmiTableLayoutPanel_Mid = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Head = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiTextBox_Data_CarrierID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Data_CarrierID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Data_Station = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Data_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.HmiTableLayoutPanel_Function = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiComboBox_Function_Combox = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Function_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_CarrierID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_CarrierID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_ID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_ID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Add = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Modify = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiButton_Del = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTableLayoutPanel_Data = New Kochi.HMI.MainControl.UI.HMITableLayoutPanel(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiButtonWithIndicate_Reset = New Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton()
        Me.HmiButtonWithIndicate_Write = New Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton()
        Me.HmiButtonWithIndicate_Read = New Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton()
        Me.Panel_Body.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel_Body_Bottom_Right.SuspendLayout()
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
        Me.Panel_Body.Size = New System.Drawing.Size(587, 501)
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
        Me.TabControl1.Size = New System.Drawing.Size(587, 501)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel_Body_Bottom_Right)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(579, 475)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel_Body_Bottom_Right
        '
        Me.Panel_Body_Bottom_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Body_Bottom_Right.Controls.Add(Me.HmiTableLayoutPanel_Body_Top_Right)
        Me.Panel_Body_Bottom_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Body_Bottom_Right.Location = New System.Drawing.Point(3, 3)
        Me.Panel_Body_Bottom_Right.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel_Body_Bottom_Right.Name = "Panel_Body_Bottom_Right"
        Me.Panel_Body_Bottom_Right.Size = New System.Drawing.Size(573, 469)
        Me.Panel_Body_Bottom_Right.TabIndex = 4
        '
        'HmiTableLayoutPanel_Body_Top_Right
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnCount = 5
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_AbortCarrierStation, 3, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_CarrierStation, 0, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Error, 1, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Error, 0, 5)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Present, 0, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButtonWithIndicate_Reset, 3, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_CarrierID, 1, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_SFC, 1, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Carrier, 0, 3)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_SFC, 0, 2)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_Variant, 0, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiLabel_CarrierID, 0, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiComboBox_CarrierID, 1, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_Variant, 1, 1)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButtonWithIndicate_Write, 2, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButtonWithIndicate_Read, 3, 0)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiSensor_Present, 1, 4)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiButton_ResetCarrierStation, 2, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Controls.Add(Me.HmiTextBox_CarrierStation, 1, 6)
        Me.HmiTableLayoutPanel_Body_Top_Right.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Body_Top_Right.Location = New System.Drawing.Point(0, 0)
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
        Me.HmiTableLayoutPanel_Body_Top_Right.Size = New System.Drawing.Size(571, 467)
        Me.HmiTableLayoutPanel_Body_Top_Right.TabIndex = 0
        '
        'HmiButton_AbortCarrierStation
        '
        Me.HmiButton_AbortCarrierStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_AbortCarrierStation.Location = New System.Drawing.Point(345, 225)
        Me.HmiButton_AbortCarrierStation.MarginHeight = 6
        Me.HmiButton_AbortCarrierStation.Name = "HmiButton_AbortCarrierStation"
        Me.HmiButton_AbortCarrierStation.Size = New System.Drawing.Size(108, 31)
        Me.HmiButton_AbortCarrierStation.TabIndex = 31
        '
        'HmiLabel_CarrierStation
        '
        Me.HmiLabel_CarrierStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CarrierStation.Location = New System.Drawing.Point(3, 225)
        Me.HmiLabel_CarrierStation.Name = "HmiLabel_CarrierStation"
        Me.HmiLabel_CarrierStation.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_CarrierStation.TabIndex = 28
        '
        'HmiSensor_Error
        '
        Me.HmiSensor_Error.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Error.Location = New System.Drawing.Point(117, 196)
        Me.HmiSensor_Error.Margin = New System.Windows.Forms.Padding(3, 11, 50, 11)
        Me.HmiSensor_Error.Name = "HmiSensor_Error"
        Me.HmiSensor_Error.Size = New System.Drawing.Size(61, 15)
        Me.HmiSensor_Error.TabIndex = 27
        '
        'HmiLabel_Error
        '
        Me.HmiLabel_Error.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Error.Location = New System.Drawing.Point(3, 188)
        Me.HmiLabel_Error.Name = "HmiLabel_Error"
        Me.HmiLabel_Error.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_Error.TabIndex = 26
        '
        'HmiLabel_Present
        '
        Me.HmiLabel_Present.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Present.Location = New System.Drawing.Point(3, 151)
        Me.HmiLabel_Present.Name = "HmiLabel_Present"
        Me.HmiLabel_Present.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_Present.TabIndex = 24
        '
        'HmiTextBox_CarrierID
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_CarrierID, 2)
        Me.HmiTextBox_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_CarrierID.Location = New System.Drawing.Point(117, 114)
        Me.HmiTextBox_CarrierID.Name = "HmiTextBox_CarrierID"
        Me.HmiTextBox_CarrierID.Number = 0
        Me.HmiTextBox_CarrierID.Size = New System.Drawing.Size(222, 31)
        Me.HmiTextBox_CarrierID.TabIndex = 20
        Me.HmiTextBox_CarrierID.TextBoxReadOnly = False
        Me.HmiTextBox_CarrierID.ValueType = GetType(String)
        '
        'HmiTextBox_SFC
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_SFC, 2)
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(117, 77)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(222, 31)
        Me.HmiTextBox_SFC.TabIndex = 19
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'HmiLabel_Carrier
        '
        Me.HmiLabel_Carrier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Carrier.Location = New System.Drawing.Point(3, 114)
        Me.HmiLabel_Carrier.Name = "HmiLabel_Carrier"
        Me.HmiLabel_Carrier.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_Carrier.TabIndex = 17
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(3, 77)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_SFC.TabIndex = 16
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(3, 40)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_Variant.TabIndex = 15
        '
        'HmiLabel_CarrierID
        '
        Me.HmiLabel_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CarrierID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_CarrierID.Name = "HmiLabel_CarrierID"
        Me.HmiLabel_CarrierID.Size = New System.Drawing.Size(108, 31)
        Me.HmiLabel_CarrierID.TabIndex = 11
        '
        'HmiComboBox_CarrierID
        '
        Me.HmiComboBox_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CarrierID.Location = New System.Drawing.Point(117, 1)
        Me.HmiComboBox_CarrierID.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
        Me.HmiComboBox_CarrierID.Name = "HmiComboBox_CarrierID"
        Me.HmiComboBox_CarrierID.Size = New System.Drawing.Size(108, 35)
        Me.HmiComboBox_CarrierID.TabIndex = 12
        '
        'HmiTextBox_Variant
        '
        Me.HmiTableLayoutPanel_Body_Top_Right.SetColumnSpan(Me.HmiTextBox_Variant, 2)
        Me.HmiTextBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Variant.Location = New System.Drawing.Point(117, 40)
        Me.HmiTextBox_Variant.Name = "HmiTextBox_Variant"
        Me.HmiTextBox_Variant.Number = 0
        Me.HmiTextBox_Variant.Size = New System.Drawing.Size(222, 31)
        Me.HmiTextBox_Variant.TabIndex = 18
        Me.HmiTextBox_Variant.TextBoxReadOnly = False
        Me.HmiTextBox_Variant.ValueType = GetType(String)
        '
        'HmiSensor_Present
        '
        Me.HmiSensor_Present.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiSensor_Present.Location = New System.Drawing.Point(117, 159)
        Me.HmiSensor_Present.Margin = New System.Windows.Forms.Padding(3, 11, 50, 11)
        Me.HmiSensor_Present.Name = "HmiSensor_Present"
        Me.HmiSensor_Present.Size = New System.Drawing.Size(61, 15)
        Me.HmiSensor_Present.TabIndex = 25
        '
        'HmiButton_ResetCarrierStation
        '
        Me.HmiButton_ResetCarrierStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_ResetCarrierStation.Location = New System.Drawing.Point(231, 225)
        Me.HmiButton_ResetCarrierStation.MarginHeight = 6
        Me.HmiButton_ResetCarrierStation.Name = "HmiButton_ResetCarrierStation"
        Me.HmiButton_ResetCarrierStation.Size = New System.Drawing.Size(108, 31)
        Me.HmiButton_ResetCarrierStation.TabIndex = 29
        '
        'HmiTextBox_CarrierStation
        '
        Me.HmiTextBox_CarrierStation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_CarrierStation.Location = New System.Drawing.Point(117, 225)
        Me.HmiTextBox_CarrierStation.Name = "HmiTextBox_CarrierStation"
        Me.HmiTextBox_CarrierStation.Number = 0
        Me.HmiTextBox_CarrierStation.Size = New System.Drawing.Size(108, 31)
        Me.HmiTextBox_CarrierStation.TabIndex = 30
        Me.HmiTextBox_CarrierStation.TextBoxReadOnly = False
        Me.HmiTextBox_CarrierStation.ValueType = GetType(String)
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.HmiTableLayoutPanel_Mid)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(579, 475)
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
        Me.HmiTableLayoutPanel_Mid.Size = New System.Drawing.Size(573, 469)
        Me.HmiTableLayoutPanel_Mid.TabIndex = 1
        '
        'GroupBox_Search
        '
        Me.HmiTableLayoutPanel_Mid.SetColumnSpan(Me.GroupBox_Search, 2)
        Me.GroupBox_Search.Controls.Add(Me.HmiTableLayoutPanel_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(567, 144)
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
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiTextBox_Data_CarrierID, 1, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_Data_CarrierID, 0, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiTextBox_Data_Station, 3, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiLabel_Data_Station, 2, 0)
        Me.HmiTableLayoutPanel_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Head.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Head.Name = "HmiTableLayoutPanel_Head"
        Me.HmiTableLayoutPanel_Head.RowCount = 1
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.HmiTableLayoutPanel_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125.0!))
        Me.HmiTableLayoutPanel_Head.Size = New System.Drawing.Size(561, 125)
        Me.HmiTableLayoutPanel_Head.TabIndex = 0
        '
        'HmiTextBox_Data_CarrierID
        '
        Me.HmiTextBox_Data_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Data_CarrierID.Location = New System.Drawing.Point(84, 3)
        Me.HmiTextBox_Data_CarrierID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Data_CarrierID.Name = "HmiTextBox_Data_CarrierID"
        Me.HmiTextBox_Data_CarrierID.Number = 0
        Me.HmiTextBox_Data_CarrierID.Size = New System.Drawing.Size(112, 119)
        Me.HmiTextBox_Data_CarrierID.TabIndex = 14
        Me.HmiTextBox_Data_CarrierID.TextBoxReadOnly = False
        Me.HmiTextBox_Data_CarrierID.ValueType = GetType(String)
        '
        'HmiLabel_Data_CarrierID
        '
        Me.HmiLabel_Data_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Data_CarrierID.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_Data_CarrierID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Data_CarrierID.Name = "HmiLabel_Data_CarrierID"
        Me.HmiLabel_Data_CarrierID.Size = New System.Drawing.Size(84, 119)
        Me.HmiLabel_Data_CarrierID.TabIndex = 13
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(477, 1)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(83, 123)
        Me.HmiButton_Cancel.TabIndex = 12
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(393, 1)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(1)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(82, 123)
        Me.HmiButton_Search.TabIndex = 11
        '
        'HmiTextBox_Data_Station
        '
        Me.HmiTextBox_Data_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Data_Station.Location = New System.Drawing.Point(283, 3)
        Me.HmiTextBox_Data_Station.Name = "HmiTextBox_Data_Station"
        Me.HmiTextBox_Data_Station.Number = 0
        Me.HmiTextBox_Data_Station.Size = New System.Drawing.Size(106, 119)
        Me.HmiTextBox_Data_Station.TabIndex = 15
        Me.HmiTextBox_Data_Station.TextBoxReadOnly = False
        Me.HmiTextBox_Data_Station.ValueType = GetType(String)
        '
        'HmiLabel_Data_Station
        '
        Me.HmiLabel_Data_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Data_Station.Location = New System.Drawing.Point(199, 3)
        Me.HmiLabel_Data_Station.Name = "HmiLabel_Data_Station"
        Me.HmiLabel_Data_Station.Size = New System.Drawing.Size(78, 119)
        Me.HmiLabel_Data_Station.TabIndex = 16
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.HmiTableLayoutPanel_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Location = New System.Drawing.Point(461, 153)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(109, 313)
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
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_CarrierID, 0, 3)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_CarrierID, 0, 2)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiTextBox_Function_ID, 0, 1)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiLabel_Function_ID, 0, 0)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Add, 0, 6)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Modify, 0, 7)
        Me.HmiTableLayoutPanel_Function.Controls.Add(Me.HmiButton_Del, 0, 8)
        Me.HmiTableLayoutPanel_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTableLayoutPanel_Function.Location = New System.Drawing.Point(3, 16)
        Me.HmiTableLayoutPanel_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTableLayoutPanel_Function.Name = "HmiTableLayoutPanel_Function"
        Me.HmiTableLayoutPanel_Function.RowCount = 10
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.HmiTableLayoutPanel_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.HmiTableLayoutPanel_Function.Size = New System.Drawing.Size(103, 294)
        Me.HmiTableLayoutPanel_Function.TabIndex = 0
        '
        'HmiComboBox_Function_Combox
        '
        Me.HmiComboBox_Function_Combox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Function_Combox.Location = New System.Drawing.Point(3, 153)
        Me.HmiComboBox_Function_Combox.Name = "HmiComboBox_Function_Combox"
        Me.HmiComboBox_Function_Combox.Size = New System.Drawing.Size(97, 24)
        Me.HmiComboBox_Function_Combox.TabIndex = 19
        '
        'HmiLabel_Function_Station
        '
        Me.HmiLabel_Function_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_Station.Location = New System.Drawing.Point(0, 123)
        Me.HmiLabel_Function_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_Station.Name = "HmiLabel_Function_Station"
        Me.HmiLabel_Function_Station.Size = New System.Drawing.Size(103, 24)
        Me.HmiLabel_Function_Station.TabIndex = 18
        '
        'HmiTextBox_Function_CarrierID
        '
        Me.HmiTextBox_Function_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_CarrierID.Location = New System.Drawing.Point(0, 93)
        Me.HmiTextBox_Function_CarrierID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_CarrierID.Name = "HmiTextBox_Function_CarrierID"
        Me.HmiTextBox_Function_CarrierID.Number = 0
        Me.HmiTextBox_Function_CarrierID.Size = New System.Drawing.Size(103, 24)
        Me.HmiTextBox_Function_CarrierID.TabIndex = 17
        Me.HmiTextBox_Function_CarrierID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_CarrierID.ValueType = GetType(String)
        '
        'HmiLabel_Function_CarrierID
        '
        Me.HmiLabel_Function_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_CarrierID.Location = New System.Drawing.Point(0, 63)
        Me.HmiLabel_Function_CarrierID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Function_CarrierID.Name = "HmiLabel_Function_CarrierID"
        Me.HmiLabel_Function_CarrierID.Size = New System.Drawing.Size(103, 24)
        Me.HmiLabel_Function_CarrierID.TabIndex = 16
        '
        'HmiTextBox_Function_ID
        '
        Me.HmiTextBox_Function_ID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_ID.Location = New System.Drawing.Point(0, 33)
        Me.HmiTextBox_Function_ID.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_Function_ID.Name = "HmiTextBox_Function_ID"
        Me.HmiTextBox_Function_ID.Number = 0
        Me.HmiTextBox_Function_ID.Size = New System.Drawing.Size(103, 24)
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
        Me.HmiLabel_Function_ID.Size = New System.Drawing.Size(103, 24)
        Me.HmiLabel_Function_ID.TabIndex = 14
        '
        'HmiButton_Add
        '
        Me.HmiButton_Add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Add.Location = New System.Drawing.Point(3, 183)
        Me.HmiButton_Add.MarginHeight = 6
        Me.HmiButton_Add.Name = "HmiButton_Add"
        Me.HmiButton_Add.Size = New System.Drawing.Size(97, 24)
        Me.HmiButton_Add.TabIndex = 20
        '
        'HmiButton_Modify
        '
        Me.HmiButton_Modify.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Modify.Location = New System.Drawing.Point(3, 213)
        Me.HmiButton_Modify.MarginHeight = 6
        Me.HmiButton_Modify.Name = "HmiButton_Modify"
        Me.HmiButton_Modify.Size = New System.Drawing.Size(97, 24)
        Me.HmiButton_Modify.TabIndex = 21
        '
        'HmiButton_Del
        '
        Me.HmiButton_Del.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Del.Location = New System.Drawing.Point(3, 243)
        Me.HmiButton_Del.MarginHeight = 6
        Me.HmiButton_Del.Name = "HmiButton_Del"
        Me.HmiButton_Del.Size = New System.Drawing.Size(97, 24)
        Me.HmiButton_Del.TabIndex = 22
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
        Me.HmiTableLayoutPanel_Data.Size = New System.Drawing.Size(452, 313)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 281)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(373, 32)
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
        Me.HmiDataView_Data.Size = New System.Drawing.Size(446, 275)
        Me.HmiDataView_Data.TabIndex = 1
        '
        'HmiButtonWithIndicate_Reset
        '
        Me.HmiButtonWithIndicate_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_Reset.Location = New System.Drawing.Point(345, 40)
        Me.HmiButtonWithIndicate_Reset.Name = "HmiButtonWithIndicate_Reset"
        Me.HmiButtonWithIndicate_Reset.Size = New System.Drawing.Size(108, 31)
        Me.HmiButtonWithIndicate_Reset.TabIndex = 23
        Me.HmiButtonWithIndicate_Reset.Text = "CarrierManagerButton1"
        Me.HmiButtonWithIndicate_Reset.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_Write
        '
        Me.HmiButtonWithIndicate_Write.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_Write.Location = New System.Drawing.Point(231, 3)
        Me.HmiButtonWithIndicate_Write.Name = "HmiButtonWithIndicate_Write"
        Me.HmiButtonWithIndicate_Write.Size = New System.Drawing.Size(108, 31)
        Me.HmiButtonWithIndicate_Write.TabIndex = 21
        Me.HmiButtonWithIndicate_Write.Text = "CarrierManagerButton1"
        Me.HmiButtonWithIndicate_Write.UseVisualStyleBackColor = True
        '
        'HmiButtonWithIndicate_Read
        '
        Me.HmiButtonWithIndicate_Read.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButtonWithIndicate_Read.Location = New System.Drawing.Point(345, 3)
        Me.HmiButtonWithIndicate_Read.Name = "HmiButtonWithIndicate_Read"
        Me.HmiButtonWithIndicate_Read.Size = New System.Drawing.Size(108, 31)
        Me.HmiButtonWithIndicate_Read.TabIndex = 22
        Me.HmiButtonWithIndicate_Read.Text = "CarrierManagerButton1"
        Me.HmiButtonWithIndicate_Read.UseVisualStyleBackColor = True
        '
        'IOForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 501)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "IOForm"
        Me.Text = "IOForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel_Body_Bottom_Right.ResumeLayout(False)
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
    Friend WithEvents Panel_Body_Bottom_Right As System.Windows.Forms.Panel
    Friend WithEvents HmiTableLayoutPanel_Body_Top_Right As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiButton_AbortCarrierStation As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_CarrierStation As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiSensor_Error As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiLabel_Error As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Present As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButtonWithIndicate_Reset As Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton
    Friend WithEvents HmiTextBox_CarrierID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Carrier As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_CarrierID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_CarrierID As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_Variant As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiButtonWithIndicate_Write As Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton
    Friend WithEvents HmiButtonWithIndicate_Read As Kochi.HMI.MainControl.Device.CarrierManager.CarrierManagerButton
    Friend WithEvents HmiSensor_Present As Kochi.HMI.MainControl.UI.HMISensor
    Friend WithEvents HmiButton_ResetCarrierStation As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTextBox_CarrierStation As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents HmiTableLayoutPanel_Mid As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Head As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiTextBox_Data_CarrierID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Data_CarrierID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents GroupBox_Function As System.Windows.Forms.GroupBox
    Friend WithEvents HmiTableLayoutPanel_Function As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiComboBox_Function_Combox As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Function_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_CarrierID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_CarrierID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiTextBox_Function_ID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Function_ID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Add As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Modify As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiButton_Del As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiTableLayoutPanel_Data As Kochi.HMI.MainControl.UI.HMITableLayoutPanel
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiTextBox_Data_Station As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_Data_Station As Kochi.HMI.MainControl.UI.HMILabel
End Class
