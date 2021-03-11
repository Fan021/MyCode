Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class ChildrenShortCutForm
    Implements IChildrenUI
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As New Dictionary(Of String, Object)
    Private cFormFontResize As clsFormFontResize
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDeviceManager As clsDeviceManager
    Private cHmiDevice As clsHMIDeviceBase
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cOldMachineStatus As New StructMachineStatus
    Private strButtonName As String
    Private strLastDeviceName As String = ""
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private iMax As Integer = 0
    Private cHMIPLC As clsHMIPLC
    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As MainForm
    Private cUserManager As clsUserManager
    Private lListOldPLCStepStatus() As StructPLCStepStatus
    Private cDebugButtonManager As clsDebugButtonManager
    Private ePageMode As enumPageMode
    Private lListStationTap As New Dictionary(Of String, TabControl)
    Private strLastStationName As String = ""
    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        cDebugButtonManager = New clsDebugButtonManager
        cDebugButtonManager.Init(cSystemElement)
        GetPageMode()
        InitForm()
        InitControlText()
        cLocalElement.Add(enumUIName.ChildrenShortCutForm.ToString, Me)
        Return True
    End Function


    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        AddHandler TabControl_Devices.SelectedIndexChanged, AddressOf TabControl_Station_SelectedIndexChanged
        Return True
    End Function

    Public Function InitControlText() As Boolean

        ListView_Data.Columns.Clear()
        ListView_Data.Font = New System.Drawing.Font("Calibri", 10.0!)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        ListView_Data.Columns.Add(PostTest_id)

        Dim PostTest_step As New DataGridViewTextBoxColumn
        PostTest_step.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "Step")
        PostTest_step.Name = "PostTest_step"
        PostTest_step.ReadOnly = True
        ListView_Data.Columns.Add(PostTest_step)

        Dim PostTest_CarrierNr As New DataGridViewTextBoxColumn
        PostTest_CarrierNr.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "CarrierNr")
        PostTest_CarrierNr.Name = "PostTest_CarrierNr"
        PostTest_CarrierNr.ReadOnly = True
        ListView_Data.Columns.Add(PostTest_CarrierNr)

        Dim PostTest_Description As New DataGridViewTextBoxColumn
        PostTest_Description.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "Description")
        PostTest_Description.Name = "PostTest_Description"
        PostTest_Description.ReadOnly = True
        ListView_Data.Columns.Add(PostTest_Description)

        ListView_Info.Columns.Clear()
        ListView_Info.Font = New System.Drawing.Font("Calibri", 10.0!)
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        ListView_Info.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "Name")
        PostTest_Name.Name = "PostTest_Name"
        If ePageMode <> enumPageMode.Edit Then
            PostTest_Name.ReadOnly = True
        End If
        ListView_Info.Columns.Add(PostTest_Name)

        Dim PostTest_Value As New DataGridViewTextBoxColumn
        PostTest_Value.HeaderText = cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "Value")
        PostTest_Value.Name = "PostTest_Value"
        If ePageMode <> enumPageMode.Edit Then
            PostTest_Value.ReadOnly = True
        End If
        ListView_Info.Columns.Add(PostTest_Value)

        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Config", "Max")
        If mTempValue <> "" AndAlso IsNumeric(mTempValue) Then
            For i = 1 To CInt(mTempValue)
                ListView_Data.Rows.Add((ListView_Data.Rows.Count + 1).ToString, "", "", "")
            Next
            iMax = ListView_Data.Rows.Count
        End If
        If ePageMode <> enumPageMode.Edit Then
            PostTest_Add.Enabled = False
            PostTest_Del.Enabled = False
            PostTest_Add_Info.Enabled = False
            PostTest_Del_Info.Enabled = False
            PostTest_Up_Info.Enabled = False
            PostTest_Down_Info.Enabled = False
        End If
        LoadInfoData()
        CreateIO()
        CreateTabPage()
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        AddHandler PostTest_Add_Info.Click, AddressOf PostTest_Add_Infor_Click
        AddHandler PostTest_Del_Info.Click, AddressOf PostTest_Del_Infor_Click
        AddHandler PostTest_Up_Info.Click, AddressOf PostTest_Up_Info_Click
        AddHandler PostTest_Down_Info.Click, AddressOf PostTest_Down_Info_Click
        AddHandler ListView_Info.CellValueChanged, AddressOf ListView_Info_CellValueChanged
        Return True
    End Function

    Private Sub LoadInfoData()
        ListView_Info.Rows.Clear()
        For i = 1 To 1024
            Dim mTempName As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Info", "Name:" + i.ToString)
            If mTempName = "" Then Exit For
            Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ShortCutConfig, "Info", "Value:" + i.ToString)
            ListView_Info.Rows.Add(i.ToString, mTempName, mTempValue)
        Next
    End Sub
    Private Sub SaveInfoData()
        For i = 0 To ListView_Info.Rows.Count - 1
            cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Info", "Name:" + ListView_Info.Rows(i).Cells(0).Value, ListView_Info.Rows(i).Cells(1).Value)
            cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Info", "Value:" + ListView_Info.Rows(i).Cells(0).Value, ListView_Info.Rows(i).Cells(2).Value)
        Next

    End Sub
    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        StopRefreshUI()
        StartRefreshUI()
        ListView_Data.Rows.Add((ListView_Data.Rows.Count + 1).ToString, "", "", "")
        iMax = ListView_Data.Rows.Count
        cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Config", "Max", ListView_Data.Rows.Count.ToString)
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        StopRefreshUI()
        StartRefreshUI()
        If ListView_Data.CurrentRow Is Nothing Then Return
        If ListView_Data.CurrentRow.Index = -1 Then Return
        ListView_Data.Rows.Remove(ListView_Data.CurrentRow)
        For i = 0 To ListView_Data.Rows.Count - 1
            ListView_Data.Rows(i).Cells(0).Value = (i + 1).ToString
        Next
        iMax = ListView_Data.Rows.Count
        cIniHandler.WriteIniFile(cSystemManager.Settings.ShortCutConfig, "Config", "Max", ListView_Data.Rows.Count.ToString)
    End Sub

    Private Sub PostTest_Up_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iID As Integer = ListView_Info.CurrentRow.Index + 1
        UpRow(iID, ListView_Info)
        SaveInfoData()
    End Sub

    Private Sub UpRow(ByVal id As Integer, ByRef v As DataGridView)
        If id <= 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id - 1).ToString
        v.Rows(id - 2).Cells(0).Value = id.ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id - 2, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub


    Private Sub PostTest_Down_Info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iID As Integer = ListView_Info.CurrentRow.Index + 1
        DownRow(iID, ListView_Info)
        SaveInfoData()
        SaveInfoData()
    End Sub

    Private Sub DownRow(ByVal id As Integer, ByRef v As DataGridView)
        If id > v.Rows.Count - 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id + 1).ToString
        v.Rows(id).Cells(0).Value = (id).ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub

    Private Sub PostTest_Add_Infor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ListView_Info.Rows.Add((ListView_Info.Rows.Count + 1).ToString, "", "")
        For Each t As DataGridViewRow In ListView_Info.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        SaveInfoData()
    End Sub

    Private Sub PostTest_Del_Infor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ListView_Info.CurrentRow Is Nothing Then Return
        If ListView_Info.CurrentRow.Index = -1 Then Return
        ListView_Info.Rows.Remove(ListView_Info.CurrentRow)
        For Each t As DataGridViewRow In ListView_Info.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        SaveInfoData()
    End Sub

    Private Sub ListView_Info_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        SaveInfoData()
    End Sub

    Private Sub ListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView_Data.Resize
        For Each element As DataGridViewTextBoxColumn In ListView_Data.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (ListView_Data.Width / 100) * 10
                Case "PostTest_step"
                    element.Width = (ListView_Data.Width / 100) * 20
                Case "PostTest_CarrierNr"
                    element.Width = (ListView_Data.Width / 100) * 30
            End Select
        Next
    End Sub


    Private Sub ListView_Info_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView_Info.Resize
        For Each element As DataGridViewTextBoxColumn In ListView_Info.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (ListView_Info.Width / 100) * 10
                Case "PostTest_Name"
                    element.Width = (ListView_Info.Width / 100) * 45
                Case "PostTest_Value"
                    element.Width = (ListView_Info.Width / 100) * 45
            End Select
        Next
    End Sub

    Private Sub CreateIO()
        Dim strTempValue As String = ""
        TableLayoutPanel_Right.ColumnCount = 1
        TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Right.RowCount = 11
        TableLayoutPanel_Right.Dock = DockStyle.Fill
        Dim j As Integer = 0
        For j = 1 To 11
            TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0))
        Next
        For j = 0 To 10
            TableLayoutPanel_Right.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0)
        Next

        j = 0
        For Each element As clsIOCfg In cDebugButtonManager.ListIO.Values
            Dim OutputIO As New OutputIO
            element.IO = OutputIO
            OutputIO.Dock = DockStyle.Fill
            OutputIO.MainButton.Font = New Font(ShortCut.Font.Name, ShortCut.Font.Size, FontStyle.Regular)
            OutputIO.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
            TableLayoutPanel_Right.Controls.Add(OutputIO, 0, j)
            OutputIO.RegisterButton(element.ActiveText, element.ID)
            OutputIO.ControlDisable = element.Reserve
            If cUserManager.CurrentUserCfg.Level < element.Level Then
                OutputIO.Level = False
            Else
                OutputIO.Level = True
            End If

            If cFormFontResize.CurrentRate < 1 And cFormFontResize.CurrentRate > 0 Then
                OutputIO.MainButton.Font = New Font(OutputIO.MainButton.Font.Name, OutputIO.MainButton.Font.Size * cFormFontResize.CurrentRate)
            End If
            If ePageMode = enumPageMode.Edit Then
                AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_Click
                AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_MouseDown
                AddHandler OutputIO.MainButton.MouseUp, AddressOf MainButton_MouseUp
            End If
            If ePageMode = enumPageMode.Debug Then
                AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_Click
                AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_MouseDown
                AddHandler OutputIO.MainButton.MouseUp, AddressOf MainButton_MouseUp
            End If
            If ePageMode <> enumPageMode.Edit Then
                If element.Reserve Then OutputIO.ControlDisable = True
            End If
            j = j + 1
        Next

    End Sub
    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cParameter As New IOParameter
                cParameter.DebugLock = False
                cParameter.ShowLevel = True
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.Init(cLocalElement, cSystemElement)
                cParameter.TextBox_ID.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).ID.ToString
                cParameter.TextBox_NameA.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Text
                cParameter.TextBox_NameA2.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Text2
                cParameter.RadioButton_Toggle.Checked = IIf(cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).IOTriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_Y.Checked = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Reserve
                cParameter.RadioButton_N.Checked = Not cParameter.RadioButton_Y.Checked
                cParameter.HmiComboBox_Level.ComboBox.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Level.ToString
                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cParameter.TextBox_NameA.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If
                    StopRefreshUI()
                    cDebugButtonManager.ChangeIO(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text, cParameter.RadioButton_Y.Checked, IIf(cParameter.RadioButton_Toggle.Checked, enumIOTriggerType.Toggle, enumIOTriggerType.Tap), [Enum].Parse(GetType(enumUserLevel), cParameter.HmiComboBox_Level.ComboBox.Text))
                    CType(sender, Button).Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).ActiveText
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Toggle Then
                        Dim iPageNr As Integer = 10
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1)
                        Dim dNewValue As Boolean = Not dOldValue
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
                    End If
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
            End If
        End If
    End Sub


    Public Function CreateTabPage() As Boolean
        Try
            Dim SubTabPage As New TabPage
            lListStationTap.Clear()

            For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                SubTabPage = New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Font = TabControl_Devices.Font
                SubTabPage.Name = element.ID
                SubTabPage.Text = element.StationName
                SubTabPage.BackColor = Color.White

                Dim cTabControl As New TabControl
                cTabControl.Margin = New System.Windows.Forms.Padding(0)
                cTabControl.Font = TabControl_Devices.Font
                cTabControl.Name = element.ID
                cTabControl.Text = element.StationName
                cTabControl.BackColor = Color.White
                cTabControl.Dock = DockStyle.Fill
                lListStationTap.Add(element.ID, cTabControl)
                SubTabPage.Controls.Add(cTabControl)
                TabControl_Devices.Controls.Add(SubTabPage)
                AddHandler cTabControl.SelectedIndexChanged, AddressOf TabControl_Devices_SelectedIndexChanged
            Next

            For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
                Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
                SubTabPage = New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Font = TabControl_Devices.Font
                SubTabPage.Name = element.Name
                SubTabPage.Text = element.Name
                SubTabPage.BackColor = Color.White
                cHmiDevice = element.Source
                If IsNothing(cHmiDevice) Then Continue For
                cHmiDevice.CreateShortcutUI(cLocalElement, cSystemElement)
                If IsNothing(cHmiDevice.ShortcutUI) Then Continue For
                cHmiDevice.ShortcutUI.Init(cLocalElement, cSystemElement)
                cHmiDevice.ShortcutUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter))
                SubTabPage.Controls.Add(cHmiDevice.ShortcutUI.UI)
                If lListStationTap.ContainsKey(element.StationID) Then lListStationTap(element.StationID).Controls.Add(SubTabPage)
            Next
            StartRefreshUI()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
            Return False
        End Try
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                '   If Not IsNothing(cErrorMessageManager) Then If cErrorMessageManager.ErrorMessageManagerState = enumErrorMessageManagerState.Alarm Then Continue While

                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenShortCutForm.ToString, "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 3
                        If iMax <= 0 Then iMax = 1
                        Dim cDefaultPLCStepStatus() As StructPLCStepStatus = Enumerable.Repeat(New StructPLCStepStatus, iMax).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_StepStatus, GetType(StructPLCStepStatus()), cDefaultPLCStepStatus, New Integer() {iMax})
                        lListOldPLCStepStatus = Enumerable.Repeat(New StructPLCStepStatus, iMax).ToArray()
                        Dim i As Integer = 0
                        For i = 0 To iMax - 1
                            If i <= ListView_Data.Rows.Count - 1 Then
                                mMainForm.Invoke(Sub()
                                                     ListView_Data.Rows(i).Cells(1).Value = lListOldPLCStepStatus(i).intStepID.ToString
                                                     ListView_Data.Rows(i).Cells(2).Value = lListOldPLCStepStatus(i).intCarrierID.ToString
                                                     ListView_Data.Rows(i).Cells(3).Value = lListOldPLCStepStatus(i).strDescription.ToString
                                                 End Sub)
                            End If
                        Next
                        Dim iPageNr As Integer = 10
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DebugButton, GetType(Boolean()), New Boolean(iPageNr) {}, New Integer() {iPageNr})
                        iStep = iStep + 1

                    Case 4
                        Dim lListPLCStepStatus() As StructPLCStepStatus = cHMIPLC.GetValue(HMI_PLC_Interface.PLC_StepStatus)
                        If IsNothing(lListPLCStepStatus) Then
                            Continue While
                        End If
                        If lListPLCStepStatus.Length <> iMax Then
                            Continue While
                        End If
                        Dim i As Integer = 0
                        For i = 0 To iMax - 1
                            If i <= ListView_Data.Rows.Count - 1 And (lListPLCStepStatus(i).intStepID <> lListOldPLCStepStatus(i).intStepID Or lListPLCStepStatus(i).intCarrierID <> lListOldPLCStepStatus(i).intCarrierID Or lListPLCStepStatus(i).strDescription <> lListOldPLCStepStatus(i).strDescription) Then
                                mMainForm.Invoke(Sub()
                                                     ListView_Data.Rows(i).Cells(1).Value = lListPLCStepStatus(i).intStepID.ToString
                                                     ListView_Data.Rows(i).Cells(2).Value = lListPLCStepStatus(i).intCarrierID.ToString
                                                     ListView_Data.Rows(i).Cells(3).Value = lListPLCStepStatus(i).strDescription.ToString
                                                 End Sub)
                            End If
                            lListOldPLCStepStatus(i).intStepID = lListPLCStepStatus(i).intStepID
                            lListOldPLCStepStatus(i).intCarrierID = lListPLCStepStatus(i).intCarrierID
                            lListOldPLCStepStatus(i).strDescription = lListPLCStepStatus(i).strDescription
                        Next

                        Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DebugButton)
                        For Each element As clsIOCfg In cDebugButtonManager.ListIO.Values
                            element.IO.SetIndicateBackColor(lListDI1((element.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + element.YIndex - 1))
                        Next

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
            End Try
            System.Threading.Thread.Sleep(10)
        End While

    End Sub

    Public Function StopRefreshUI() As Boolean
        bExit = True
        Dim iCnt As Integer = 100
        Do While iCnt > 0
            If IsNothing(cThread) Then
                Exit Do
            End If
            If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                Exit Do
            End If
            iCnt = iCnt - 1
            System.Threading.Thread.Sleep(1)
        Loop
        If Not IsNothing(cThread) Then cThread.Abort()
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.PLC_StepStatus)
        Return True
    End Function

    Public Function StartRefreshUI() As Boolean
        strLastStationName = "ShortCut"
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        Try
            StopRefreshUI()
            For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
                Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
                cHmiDevice = element.Source
                If IsNothing(cHmiDevice) Then Continue For
                If IsNothing(cHmiDevice.ShortcutUI) Then Continue For
                cHmiDevice.ShortcutUI.Quit(cLocalElement, cSystemElement)
            Next
            If cLocalElement.ContainsKey(enumUIName.ChildrenShortCutForm.ToString) Then
                cLocalElement.Remove(enumUIName.ChildrenShortCutForm.ToString)
            End If
            cErrorMessageManager.Clean(enumUIName.ChildrenShortCutForm.ToString)
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
            Return False
        End Try
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub


    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Private Sub TabControl_Station_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If strLastStationName = "ShortCut" Then
            StopRefreshUI()
        Else
            If Not IsNothing(lListStationTap(strLastStationName).SelectedTab) Then
                strLastDeviceName = lListStationTap(strLastStationName).SelectedTab.Name
                CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ShortcutUI.StopRefresh(cLocalElement, cSystemElement)
            End If
        End If
        strLastStationName = TabControl_Devices.SelectedTab.Name
        If strLastStationName = "ShortCut" Then
            StartRefreshUI()
        Else
            If Not IsNothing(lListStationTap(strLastStationName).SelectedTab) Then
                strLastDeviceName = lListStationTap(strLastStationName).SelectedTab.Name
                CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ShortcutUI.StartRefresh(cLocalElement, cSystemElement)
            Else
                strLastDeviceName = ""
            End If
        End If
    End Sub

    Private Sub TabControl_Devices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If strLastDeviceName <> "" Then CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ShortcutUI.StopRefresh(cLocalElement, cSystemElement)
        strLastDeviceName = sender.SelectedTab.Name
        CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ShortcutUI.StartRefresh(cLocalElement, cSystemElement)
    End Sub

End Class