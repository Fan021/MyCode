<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenCarrierDetailForm
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
        Me.MachineListView_Data_Carrier = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.GroupBox_Function = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Left_Function = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiButton_Function_Reset = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiTextBox_Function_StationID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_Function_StationID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Function_CarrierID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiTextBox_Function_CarrierID = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.OpenFileDialog_Path = New System.Windows.Forms.OpenFileDialog()
        Me.HmiLabel_CarrierID = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_CarrierID = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.GroupBox_Search.SuspendLayout()
        Me.TableLayoutPanel_Body_Head.SuspendLayout()
        Me.TableLayoutPanel_Mid.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.MachineListView_Data_Carrier, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.18182!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_CarrierID, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_CarrierID, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StationID, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_StationID, 1, 0)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 1
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 40)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiLabel_StationID
        '
        Me.HmiLabel_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StationID.Location = New System.Drawing.Point(3, 3)
        Me.HmiLabel_StationID.Name = "HmiLabel_StationID"
        Me.HmiLabel_StationID.Size = New System.Drawing.Size(76, 34)
        Me.HmiLabel_StationID.TabIndex = 0
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(331, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(56, 34)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(393, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(59, 34)
        Me.HmiButton_Cancel.TabIndex = 5
        '
        'HmiComboBox_StationID
        '
        Me.HmiComboBox_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_StationID.Location = New System.Drawing.Point(85, 3)
        Me.HmiComboBox_StationID.Name = "HmiComboBox_StationID"
        Me.HmiComboBox_StationID.Size = New System.Drawing.Size(76, 34)
        Me.HmiComboBox_StationID.TabIndex = 6
        '
        'TableLayoutPanel_Mid
        '
        Me.TableLayoutPanel_Mid.ColumnCount = 2
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.0!))
        Me.TableLayoutPanel_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.0!))
        Me.TableLayoutPanel_Mid.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 0)
        Me.TableLayoutPanel_Mid.Controls.Add(Me.GroupBox_Function, 1, 0)
        Me.TableLayoutPanel_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Mid.Location = New System.Drawing.Point(0, 72)
        Me.TableLayoutPanel_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Mid.Name = "TableLayoutPanel_Mid"
        Me.TableLayoutPanel_Mid.RowCount = 1
        Me.TableLayoutPanel_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Mid.Size = New System.Drawing.Size(467, 458)
        Me.TableLayoutPanel_Mid.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.MachineListView_Data_Carrier, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(364, 458)
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
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(364, 37)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'MachineListView_Data_Carrier
        '
        Me.MachineListView_Data_Carrier.AllowUserToAddRows = False
        Me.MachineListView_Data_Carrier.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Data_Carrier.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.MachineListView_Data_Carrier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.MachineListView_Data_Carrier.BackgroundColor = System.Drawing.Color.White
        Me.MachineListView_Data_Carrier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.MachineListView_Data_Carrier.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.MachineListView_Data_Carrier.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.MachineListView_Data_Carrier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MachineListView_Data_Carrier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MachineListView_Data_Carrier.EnableHeadersVisualStyles = False
        Me.MachineListView_Data_Carrier.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.MachineListView_Data_Carrier.Location = New System.Drawing.Point(3, 3)
        Me.MachineListView_Data_Carrier.Name = "MachineListView_Data_Carrier"
        Me.MachineListView_Data_Carrier.RowHeadersVisible = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.MachineListView_Data_Carrier.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.MachineListView_Data_Carrier.RowTemplate.Height = 40
        Me.MachineListView_Data_Carrier.Size = New System.Drawing.Size(358, 415)
        Me.MachineListView_Data_Carrier.TabIndex = 2
        '
        'GroupBox_Function
        '
        Me.GroupBox_Function.Controls.Add(Me.TableLayoutPanel_Body_Left_Function)
        Me.GroupBox_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Function.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Function.Location = New System.Drawing.Point(367, 3)
        Me.GroupBox_Function.Name = "GroupBox_Function"
        Me.GroupBox_Function.Size = New System.Drawing.Size(97, 452)
        Me.GroupBox_Function.TabIndex = 1
        Me.GroupBox_Function.TabStop = False
        Me.GroupBox_Function.Text = "Function"
        '
        'TableLayoutPanel_Body_Left_Function
        '
        Me.TableLayoutPanel_Body_Left_Function.ColumnCount = 1
        Me.TableLayoutPanel_Body_Left_Function.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiButton_Function_Reset, 0, 4)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_StationID, 0, 1)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_StationID, 0, 0)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiLabel_Function_CarrierID, 0, 2)
        Me.TableLayoutPanel_Body_Left_Function.Controls.Add(Me.HmiTextBox_Function_CarrierID, 0, 3)
        Me.TableLayoutPanel_Body_Left_Function.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Left_Function.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Left_Function.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Left_Function.Name = "TableLayoutPanel_Body_Left_Function"
        Me.TableLayoutPanel_Body_Left_Function.RowCount = 6
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body_Left_Function.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel_Body_Left_Function.Size = New System.Drawing.Size(91, 426)
        Me.TableLayoutPanel_Body_Left_Function.TabIndex = 0
        '
        'HmiButton_Function_Reset
        '
        Me.HmiButton_Function_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Function_Reset.Location = New System.Drawing.Point(0, 120)
        Me.HmiButton_Function_Reset.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiButton_Function_Reset.MarginHeight = 6
        Me.HmiButton_Function_Reset.Name = "HmiButton_Function_Reset"
        Me.HmiButton_Function_Reset.Size = New System.Drawing.Size(91, 30)
        Me.HmiButton_Function_Reset.TabIndex = 8
        '
        'HmiTextBox_Function_StationID
        '
        Me.HmiTextBox_Function_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_StationID.Location = New System.Drawing.Point(0, 30)
        Me.HmiTextBox_Function_StationID.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_StationID.Name = "HmiTextBox_Function_StationID"
        Me.HmiTextBox_Function_StationID.Number = 0
        Me.HmiTextBox_Function_StationID.Size = New System.Drawing.Size(91, 30)
        Me.HmiTextBox_Function_StationID.TabIndex = 4
        Me.HmiTextBox_Function_StationID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_StationID.ValueType = GetType(String)
        '
        'HmiLabel_Function_StationID
        '
        Me.HmiLabel_Function_StationID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_StationID.Location = New System.Drawing.Point(1, 2)
        Me.HmiLabel_Function_StationID.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.HmiLabel_Function_StationID.Name = "HmiLabel_Function_StationID"
        Me.HmiLabel_Function_StationID.Size = New System.Drawing.Size(89, 26)
        Me.HmiLabel_Function_StationID.TabIndex = 1
        '
        'HmiLabel_Function_CarrierID
        '
        Me.HmiLabel_Function_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Function_CarrierID.Location = New System.Drawing.Point(1, 62)
        Me.HmiLabel_Function_CarrierID.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.HmiLabel_Function_CarrierID.Name = "HmiLabel_Function_CarrierID"
        Me.HmiLabel_Function_CarrierID.Size = New System.Drawing.Size(89, 26)
        Me.HmiLabel_Function_CarrierID.TabIndex = 10
        '
        'HmiTextBox_Function_CarrierID
        '
        Me.HmiTextBox_Function_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_Function_CarrierID.Location = New System.Drawing.Point(0, 90)
        Me.HmiTextBox_Function_CarrierID.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiTextBox_Function_CarrierID.Name = "HmiTextBox_Function_CarrierID"
        Me.HmiTextBox_Function_CarrierID.Number = 0
        Me.HmiTextBox_Function_CarrierID.Size = New System.Drawing.Size(91, 30)
        Me.HmiTextBox_Function_CarrierID.TabIndex = 11
        Me.HmiTextBox_Function_CarrierID.TextBoxReadOnly = False
        Me.HmiTextBox_Function_CarrierID.ValueType = GetType(String)
        '
        'HmiLabel_CarrierID
        '
        Me.HmiLabel_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_CarrierID.Location = New System.Drawing.Point(167, 3)
        Me.HmiLabel_CarrierID.Name = "HmiLabel_CarrierID"
        Me.HmiLabel_CarrierID.Size = New System.Drawing.Size(76, 34)
        Me.HmiLabel_CarrierID.TabIndex = 7
        '
        'HmiComboBox_CarrierID
        '
        Me.HmiComboBox_CarrierID.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_CarrierID.Location = New System.Drawing.Point(249, 3)
        Me.HmiComboBox_CarrierID.Name = "HmiComboBox_CarrierID"
        Me.HmiComboBox_CarrierID.Size = New System.Drawing.Size(76, 34)
        Me.HmiComboBox_CarrierID.TabIndex = 8
        '
        'ChildrenCarrierDetailForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenCarrierDetailForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChildrenStationError"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.TableLayoutPanel_Mid.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.MachineListView_Data_Carrier, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents HmiLabel_Function_StationID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiLabel_Function_CarrierID As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Function_Reset As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents OpenFileDialog_Path As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HmiComboBox_StationID As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents MachineListView_Data_Carrier As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiTextBox_Function_CarrierID As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiComboBox_CarrierID As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_CarrierID As Kochi.HMI.MainControl.UI.HMILabel

End Class
