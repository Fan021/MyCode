<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChildrenActionForm
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
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel_Body = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Body = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel_Body_Mid = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiDataView_Data = New Kochi.HMI.MainControl.UI.HMIDataView(Me.components)
        Me.HmiDataViewPage_Data = New Kochi.HMI.MainControl.UI.HMIDataViewPage()
        Me.GroupBox_Search = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel_Body_Head = New System.Windows.Forms.TableLayoutPanel()
        Me.HmiTextBox_ActionName = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.HmiLabel_ActionName = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Stage = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Stage = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_ActionId = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_ActionId = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_Station = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_SFC = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Cancel = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_StartDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiLabel_EndDate = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiButton_Search = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiDateTime_Start = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiDateTime_End = New Kochi.HMI.MainControl.UI.HMIDateTime()
        Me.HmiButton_Export = New Kochi.HMI.MainControl.UI.HMIButton()
        Me.HmiLabel_Variant = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Station = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiLabel_Result = New Kochi.HMI.MainControl.UI.HMILabel()
        Me.HmiComboBox_Result = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.HmiComboBox_Variant = New Kochi.HMI.MainControl.UI.HMIComboBox()
        Me.ContextMenuStrip_Function = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogcsv = New System.Windows.Forms.SaveFileDialog()
        Me.HmiTextBox_SFC = New Kochi.HMI.MainControl.UI.HMITextBox()
        Me.Panel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body.SuspendLayout()
        Me.TableLayoutPanel_Body_Mid.SuspendLayout()
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox_Search.SuspendLayout()
        Me.TableLayoutPanel_Body_Head.SuspendLayout()
        Me.ContextMenuStrip_Function.SuspendLayout()
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
        Me.TableLayoutPanel_Body.Controls.Add(Me.TableLayoutPanel_Body_Mid, 0, 1)
        Me.TableLayoutPanel_Body.Controls.Add(Me.GroupBox_Search, 0, 0)
        Me.TableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Body.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body.Name = "TableLayoutPanel_Body"
        Me.TableLayoutPanel_Body.RowCount = 2
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel_Body.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel_Body.Size = New System.Drawing.Size(467, 530)
        Me.TableLayoutPanel_Body.TabIndex = 0
        '
        'TableLayoutPanel_Body_Mid
        '
        Me.TableLayoutPanel_Body_Mid.ColumnCount = 1
        Me.TableLayoutPanel_Body_Mid.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataView_Data, 0, 0)
        Me.TableLayoutPanel_Body_Mid.Controls.Add(Me.HmiDataViewPage_Data, 0, 1)
        Me.TableLayoutPanel_Body_Mid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Mid.Location = New System.Drawing.Point(0, 200)
        Me.TableLayoutPanel_Body_Mid.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Mid.Name = "TableLayoutPanel_Body_Mid"
        Me.TableLayoutPanel_Body_Mid.RowCount = 2
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.0!))
        Me.TableLayoutPanel_Body_Mid.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.0!))
        Me.TableLayoutPanel_Body_Mid.Size = New System.Drawing.Size(467, 341)
        Me.TableLayoutPanel_Body_Mid.TabIndex = 4
        '
        'HmiDataView_Data
        '
        Me.HmiDataView_Data.AllowUserToAddRows = False
        Me.HmiDataView_Data.AllowUserToDeleteRows = False
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle13
        Me.HmiDataView_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.HmiDataView_Data.BackgroundColor = System.Drawing.Color.White
        Me.HmiDataView_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.HmiDataView_Data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Calibri", 12.0!)
        DataGridViewCellStyle14.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.HmiDataView_Data.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle14
        Me.HmiDataView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.HmiDataView_Data.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDataView_Data.EnableHeadersVisualStyles = False
        Me.HmiDataView_Data.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.HmiDataView_Data.Location = New System.Drawing.Point(0, 0)
        Me.HmiDataView_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataView_Data.Name = "HmiDataView_Data"
        Me.HmiDataView_Data.ReadOnly = True
        Me.HmiDataView_Data.RowHeadersVisible = False
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.HmiDataView_Data.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.HmiDataView_Data.RowTemplate.Height = 40
        Me.HmiDataView_Data.RowTemplate.ReadOnly = True
        Me.HmiDataView_Data.Size = New System.Drawing.Size(467, 313)
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
        Me.HmiDataViewPage_Data.Location = New System.Drawing.Point(0, 313)
        Me.HmiDataViewPage_Data.Margin = New System.Windows.Forms.Padding(0)
        Me.HmiDataViewPage_Data.Name = "HmiDataViewPage_Data"
        Me.HmiDataViewPage_Data.Size = New System.Drawing.Size(467, 28)
        Me.HmiDataViewPage_Data.TabIndex = 1
        Me.HmiDataViewPage_Data.TotallPage = 0
        Me.HmiDataViewPage_Data.TotalRecord = 0
        '
        'GroupBox_Search
        '
        Me.GroupBox_Search.Controls.Add(Me.TableLayoutPanel_Body_Head)
        Me.GroupBox_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox_Search.Font = New System.Drawing.Font("Calibri", 12.0!)
        Me.GroupBox_Search.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox_Search.Name = "GroupBox_Search"
        Me.GroupBox_Search.Size = New System.Drawing.Size(461, 194)
        Me.GroupBox_Search.TabIndex = 3
        Me.GroupBox_Search.TabStop = False
        Me.GroupBox_Search.Text = "Search"
        '
        'TableLayoutPanel_Body_Head
        '
        Me.TableLayoutPanel_Body_Head.ColumnCount = 7
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00115!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0023!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00115!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0023!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33154!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33154!))
        Me.TableLayoutPanel_Body_Head.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33002!))
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_ActionName, 5, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_ActionName, 4, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Stage, 1, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Stage, 0, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_ActionId, 3, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_ActionId, 2, 2)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Station, 0, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_SFC, 0, 3)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Cancel, 5, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_StartDate, 0, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_EndDate, 2, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Search, 4, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiButton_Export, 6, 0)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Variant, 2, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Station, 1, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiLabel_Result, 4, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Result, 5, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiComboBox_Variant, 3, 1)
        Me.TableLayoutPanel_Body_Head.Controls.Add(Me.HmiTextBox_SFC, 1, 3)
        Me.TableLayoutPanel_Body_Head.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Body_Head.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanel_Body_Head.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_Body_Head.Name = "TableLayoutPanel_Body_Head"
        Me.TableLayoutPanel_Body_Head.RowCount = 4
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Body_Head.Size = New System.Drawing.Size(455, 168)
        Me.TableLayoutPanel_Body_Head.TabIndex = 0
        '
        'HmiTextBox_ActionName
        '
        Me.HmiTextBox_ActionName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_ActionName.Location = New System.Drawing.Point(332, 81)
        Me.HmiTextBox_ActionName.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiTextBox_ActionName.Name = "HmiTextBox_ActionName"
        Me.HmiTextBox_ActionName.Number = 0
        Me.HmiTextBox_ActionName.Size = New System.Drawing.Size(60, 33)
        Me.HmiTextBox_ActionName.TabIndex = 23
        Me.HmiTextBox_ActionName.TextBoxReadOnly = False
        Me.HmiTextBox_ActionName.ValueType = GetType(String)
        '
        'HmiLabel_ActionName
        '
        Me.HmiLabel_ActionName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ActionName.Location = New System.Drawing.Point(272, 81)
        Me.HmiLabel_ActionName.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_ActionName.Name = "HmiLabel_ActionName"
        Me.HmiLabel_ActionName.Size = New System.Drawing.Size(60, 33)
        Me.HmiLabel_ActionName.TabIndex = 22
        '
        'HmiComboBox_Stage
        '
        Me.HmiComboBox_Stage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Stage.Location = New System.Drawing.Point(45, 81)
        Me.HmiComboBox_Stage.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Stage.Name = "HmiComboBox_Stage"
        Me.HmiComboBox_Stage.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Stage.TabIndex = 21
        '
        'HmiLabel_Stage
        '
        Me.HmiLabel_Stage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Stage.Location = New System.Drawing.Point(0, 81)
        Me.HmiLabel_Stage.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Stage.Name = "HmiLabel_Stage"
        Me.HmiLabel_Stage.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Stage.TabIndex = 20
        '
        'HmiComboBox_ActionId
        '
        Me.HmiComboBox_ActionId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_ActionId.Location = New System.Drawing.Point(181, 81)
        Me.HmiComboBox_ActionId.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_ActionId.Name = "HmiComboBox_ActionId"
        Me.HmiComboBox_ActionId.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_ActionId.TabIndex = 19
        '
        'HmiLabel_ActionId
        '
        Me.HmiLabel_ActionId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_ActionId.Location = New System.Drawing.Point(136, 81)
        Me.HmiLabel_ActionId.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_ActionId.Name = "HmiLabel_ActionId"
        Me.HmiLabel_ActionId.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_ActionId.TabIndex = 18
        '
        'HmiLabel_Station
        '
        Me.HmiLabel_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Station.Location = New System.Drawing.Point(0, 42)
        Me.HmiLabel_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Station.Name = "HmiLabel_Station"
        Me.HmiLabel_Station.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Station.TabIndex = 13
        '
        'HmiLabel_SFC
        '
        Me.HmiLabel_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_SFC.Location = New System.Drawing.Point(0, 120)
        Me.HmiLabel_SFC.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_SFC.Name = "HmiLabel_SFC"
        Me.HmiLabel_SFC.Size = New System.Drawing.Size(45, 45)
        Me.HmiLabel_SFC.TabIndex = 8
        '
        'HmiButton_Cancel
        '
        Me.HmiButton_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Cancel.Location = New System.Drawing.Point(332, 3)
        Me.HmiButton_Cancel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Cancel.MarginHeight = 6
        Me.HmiButton_Cancel.Name = "HmiButton_Cancel"
        Me.HmiButton_Cancel.Size = New System.Drawing.Size(60, 33)
        Me.HmiButton_Cancel.TabIndex = 7
        '
        'HmiLabel_StartDate
        '
        Me.HmiLabel_StartDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_StartDate.Location = New System.Drawing.Point(0, 3)
        Me.HmiLabel_StartDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_StartDate.Name = "HmiLabel_StartDate"
        Me.HmiLabel_StartDate.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_StartDate.TabIndex = 0
        '
        'HmiLabel_EndDate
        '
        Me.HmiLabel_EndDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_EndDate.Location = New System.Drawing.Point(136, 3)
        Me.HmiLabel_EndDate.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_EndDate.Name = "HmiLabel_EndDate"
        Me.HmiLabel_EndDate.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_EndDate.TabIndex = 2
        '
        'HmiButton_Search
        '
        Me.HmiButton_Search.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Search.Location = New System.Drawing.Point(272, 3)
        Me.HmiButton_Search.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Search.MarginHeight = 6
        Me.HmiButton_Search.Name = "HmiButton_Search"
        Me.HmiButton_Search.Size = New System.Drawing.Size(60, 33)
        Me.HmiButton_Search.TabIndex = 4
        '
        'HmiDateTime_Start
        '
        Me.HmiDateTime_Start.DateTimeToString = ""
        Me.HmiDateTime_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_Start.Location = New System.Drawing.Point(45, 3)
        Me.HmiDateTime_Start.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_Start.Name = "HmiDateTime_Start"
        Me.HmiDateTime_Start.Size = New System.Drawing.Size(91, 33)
        Me.HmiDateTime_Start.TabIndex = 6
        '
        'HmiDateTime_End
        '
        Me.HmiDateTime_End.DateTimeToString = ""
        Me.HmiDateTime_End.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_End.Location = New System.Drawing.Point(181, 3)
        Me.HmiDateTime_End.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_End.Name = "HmiDateTime_End"
        Me.HmiDateTime_End.Size = New System.Drawing.Size(91, 33)
        Me.HmiDateTime_End.TabIndex = 9
        '
        'HmiButton_Export
        '
        Me.HmiButton_Export.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiButton_Export.Location = New System.Drawing.Point(392, 3)
        Me.HmiButton_Export.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiButton_Export.MarginHeight = 6
        Me.HmiButton_Export.Name = "HmiButton_Export"
        Me.HmiButton_Export.Size = New System.Drawing.Size(63, 33)
        Me.HmiButton_Export.TabIndex = 10
        '
        'HmiLabel_Variant
        '
        Me.HmiLabel_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Variant.Location = New System.Drawing.Point(136, 42)
        Me.HmiLabel_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Variant.Name = "HmiLabel_Variant"
        Me.HmiLabel_Variant.Size = New System.Drawing.Size(45, 33)
        Me.HmiLabel_Variant.TabIndex = 11
        '
        'HmiComboBox_Station
        '
        Me.HmiComboBox_Station.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Station.Location = New System.Drawing.Point(45, 42)
        Me.HmiComboBox_Station.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Station.Name = "HmiComboBox_Station"
        Me.HmiComboBox_Station.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Station.TabIndex = 14
        '
        'HmiLabel_Result
        '
        Me.HmiLabel_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiLabel_Result.Location = New System.Drawing.Point(272, 42)
        Me.HmiLabel_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiLabel_Result.Name = "HmiLabel_Result"
        Me.HmiLabel_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiLabel_Result.TabIndex = 15
        '
        'HmiComboBox_Result
        '
        Me.HmiComboBox_Result.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Result.Location = New System.Drawing.Point(332, 42)
        Me.HmiComboBox_Result.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Result.Name = "HmiComboBox_Result"
        Me.HmiComboBox_Result.Size = New System.Drawing.Size(60, 33)
        Me.HmiComboBox_Result.TabIndex = 16
        '
        'HmiComboBox_Variant
        '
        Me.HmiComboBox_Variant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiComboBox_Variant.Location = New System.Drawing.Point(181, 42)
        Me.HmiComboBox_Variant.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiComboBox_Variant.Name = "HmiComboBox_Variant"
        Me.HmiComboBox_Variant.Size = New System.Drawing.Size(91, 33)
        Me.HmiComboBox_Variant.TabIndex = 17
        '
        'ContextMenuStrip_Function
        '
        Me.ContextMenuStrip_Function.Font = New System.Drawing.Font("Calibri", 18.0!)
        Me.ContextMenuStrip_Function.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem_Delete})
        Me.ContextMenuStrip_Function.Name = "ContextMenuStrip_Function"
        Me.ContextMenuStrip_Function.Size = New System.Drawing.Size(152, 38)
        '
        'ToolStripMenuItem_Delete
        '
        Me.ToolStripMenuItem_Delete.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripMenuItem_Delete.Name = "ToolStripMenuItem_Delete"
        Me.ToolStripMenuItem_Delete.Size = New System.Drawing.Size(151, 34)
        Me.ToolStripMenuItem_Delete.Text = "Delete"
        '
        'HmiTextBox_SFC
        '
        Me.HmiTextBox_SFC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiTextBox_SFC.Location = New System.Drawing.Point(49, 122)
        Me.HmiTextBox_SFC.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.HmiTextBox_SFC.Name = "HmiTextBox_SFC"
        Me.HmiTextBox_SFC.Number = 0
        Me.HmiTextBox_SFC.Size = New System.Drawing.Size(83, 41)
        Me.HmiTextBox_SFC.TabIndex = 24
        Me.HmiTextBox_SFC.TextBoxReadOnly = False
        Me.HmiTextBox_SFC.ValueType = GetType(String)
        '
        'ChildrenActionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 530)
        Me.Controls.Add(Me.Panel_Body)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "ChildrenActionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ProductionForm"
        Me.Panel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Mid.ResumeLayout(False)
        CType(Me.HmiDataView_Data, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox_Search.ResumeLayout(False)
        Me.TableLayoutPanel_Body_Head.ResumeLayout(False)
        Me.ContextMenuStrip_Function.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel_Body As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel_Body As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GroupBox_Search As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel_Body_Head As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiLabel_StartDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_EndDate As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiButton_Search As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiDateTime_Start As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Cancel As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_SFC As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiDateTime_End As Kochi.HMI.MainControl.UI.HMIDateTime
    Friend WithEvents HmiButton_Export As Kochi.HMI.MainControl.UI.HMIButton
    Friend WithEvents HmiLabel_Variant As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents ContextMenuStrip_Function As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogcsv As System.Windows.Forms.SaveFileDialog
    Friend WithEvents HmiLabel_Station As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Station As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_Result As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Result As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_Variant As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiComboBox_ActionId As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiLabel_ActionId As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiLabel_Stage As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents HmiComboBox_Stage As Kochi.HMI.MainControl.UI.HMIComboBox
    Friend WithEvents HmiTextBox_ActionName As Kochi.HMI.MainControl.UI.HMITextBox
    Friend WithEvents HmiLabel_ActionName As Kochi.HMI.MainControl.UI.HMILabel
    Friend WithEvents TableLayoutPanel_Body_Mid As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HmiDataView_Data As Kochi.HMI.MainControl.UI.HMIDataView
    Friend WithEvents HmiDataViewPage_Data As Kochi.HMI.MainControl.UI.HMIDataViewPage
    Friend WithEvents HmiTextBox_SFC As Kochi.HMI.MainControl.UI.HMITextBox
End Class
