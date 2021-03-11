Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Runner

Public Class ChildrenIOForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cMachineManager As clsMachineManager
    Private cOldMachineStatus As New StructMachineStatus
    Private cUserManager As clsUserManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cLanguageManager As clsLanguageManager
    Private cSystemManager As clsSystemManager
    Private cIniHandler As clsIniHandler
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As MainForm
    Private strButtonName As String
    Private ePageMode As enumPageMode
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private cMainButtonRunner As clsMainButtonRunner
    Private cFormFontResize As clsFormFontResize
    Private cMainButtonManager As clsMainButtonManager
    Private bWaiting As Boolean = False
    Private cIOLockManager As clsIOLockManager
    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property

    Public ReadOnly Property UI As Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMainButtonRunner = CType(cSystemElement(clsMainButtonRunner.Name), clsMainButtonRunner)
            cMainButtonManager = CType(cLocalElement(clsMainButtonManager.Name), clsMainButtonManager)
            cIOManager = New clsIOManager
            cIOManager.Init(cSystemElement)
            cCylinderManager = New clsCylinderManager
            cCylinderManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cIOLockManager = New clsIOLockManager
            cIOLockManager.IOManager = cIOManager
            cIOLockManager.CylinderManager = cCylinderManager
            cIOLockManager.Init(cSystemElement)


            cIniHandler = New clsIniHandler
            GetPageMode()
            InitForm()
            InitControlText()
            Timer1.Enabled = True

            cLocalElement.Add(enumUIName.ChildrenIOForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenIOForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean

        If ePageMode = enumPageMode.Edit Then AddHandler TabControl_IO.MouseClick, AddressOf TabControl_MouseClick
        Return True
    End Function

    Private Sub CreateTable(ByVal iSelectIndex As Integer)
        Try
            CType(cMainButtonManager.GetMainButtonManagerCfgFromName("Cylinder").Source, MainRightButton).Enabled = False
            CType(cMainButtonManager.GetMainButtonManagerCfgFromName("IO").Source, MainRightButton).Enabled = False
            TabControl_IO.Enabled = False
            TabControl_IO.Controls.Clear()
            For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                Dim SubTabPage As New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Name = element.ID
                SubTabPage.BackColor = Color.White
                SubTabPage.Text = element.ActiveText
                SubTabPage.Font = TabControl_IO.Font
                CreateIO(SubTabPage, element)
                ' Application.DoEvents()
                TabControl_IO.Controls.Add(SubTabPage)
            Next
            TabControl_IO.Enabled = True
            TabControl_IO.SelectedIndex = iSelectIndex
            CType(cMainButtonManager.GetMainButtonManagerCfgFromName("Cylinder").Source, MainRightButton).Enabled = True
            CType(cMainButtonManager.GetMainButtonManagerCfgFromName("IO").Source, MainRightButton).Enabled = True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub CreateIO(ByVal SubTabPage As TabPage, ByVal cIOPageCfg As clsIOPageCfg)
        SubTabPage.Controls.Clear()
        Dim TableLayoutPanel_Tab_Main As New TableLayoutPanel
        TableLayoutPanel_Tab_Main.ColumnCount = 3
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        TableLayoutPanel_Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab_Main.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab_Main.Name = "TableLayoutPanel_Tab_Main"
        TableLayoutPanel_Tab_Main.RowCount = HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2 + 2
        TableLayoutPanel_Tab_Main.Dock = DockStyle.Fill
        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        For j = 1 To HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2
            TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Next
        For j = 1 To HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (100.0 - 15) / HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2 + 4)
        Next

        For j = 2 To HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, (100.0 - 15) / HMI_PLC_Interface.CON_MAXIMUM_PageNumber * 2 - 4)
        Next

        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        If cIOPageCfg.IOType = enumIOType.EL1008 Or cIOPageCfg.IOType = enumIOType.EP1008 Then
            Dim Label_Input As New Label
            Label_Input.Dock = System.Windows.Forms.DockStyle.Fill
            Label_Input.Font = SubTabPage.Font
            Label_Input.Name = "Input"
            Label_Input.Size = New System.Drawing.Size(223, 32)
            Label_Input.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "Input")
            Label_Input.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            TableLayoutPanel_Tab_Main.Controls.Add(Label_Input, 1, 0)
        Else
            Dim Label_Output As New Label
            Label_Output.Dock = System.Windows.Forms.DockStyle.Fill
            Label_Output.Font = SubTabPage.Font
            Label_Output.Name = "Output"
            Label_Output.Size = New System.Drawing.Size(223, 32)
            Label_Output.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "Output")
            Label_Output.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            TableLayoutPanel_Tab_Main.Controls.Add(Label_Output, 1, 0)
        End If

        Dim Label_Input2 As New Label
        Label_Input2.Dock = System.Windows.Forms.DockStyle.Fill
        Label_Input2.Name = "Mode"
        Label_Input2.Size = New System.Drawing.Size(223, 32)
        Label_Input2.Font = New System.Drawing.Font("Calibri", SubTabPage.Font.Size * 0.5)
        Label_Input2.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Label_Input2.TextAlign = ContentAlignment.MiddleRight
        Label_Input2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, ePageMode)
        TableLayoutPanel_Tab_Main.Controls.Add(Label_Input2, 2, 0)

        For Each element As clsIOCfg In cIOPageCfg.ListIO.Values
            If cIOPageCfg.IOType = enumIOType.EL1008 Or cIOPageCfg.IOType = enumIOType.EP1008 Then
                Dim InputIO As New InputIO
                element.IO = InputIO
                InputIO.Dock = DockStyle.Fill
                InputIO.MainButton.Font = SubTabPage.Font
                InputIO.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
                InputIO.MainButton.Name = cIOPageCfg.XIndex.ToString + "_" + element.XIndex.ToString + "_" + element.YIndex.ToString
                InputIO.MainButton.Text = element.ActiveText
                TableLayoutPanel_Tab_Main.Controls.Add(InputIO, 1, (element.YIndex - 1) * 2 + 1)
                If ePageMode = enumPageMode.Edit Then AddHandler InputIO.MainButton.MouseDown, AddressOf MainButton_Click

                If ePageMode = enumPageMode.ReadOnly Then
                    InputIO.ControlDisable = True
                Else
                    InputIO.ControlDisable = False
                End If

            Else
                Dim OutputIO As New OutputIO
                element.IO = OutputIO
                OutputIO.Dock = DockStyle.Fill
                OutputIO.MainButton.Font = SubTabPage.Font
                OutputIO.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
                OutputIO.MainButton.Name = cIOPageCfg.XIndex.ToString + "_" + element.XIndex.ToString + "_" + element.YIndex.ToString
                OutputIO.MainButton.Text = element.ActiveText
                TableLayoutPanel_Tab_Main.Controls.Add(OutputIO, 1, (element.YIndex - 1) * 2 + 1)
                If ePageMode = enumPageMode.Edit Then AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_Click
                If ePageMode = enumPageMode.Debug Then
                    AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_Click
                    AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_MouseDown
                    AddHandler OutputIO.MainButton.MouseUp, AddressOf MainButton_MouseUp
                End If

                If ePageMode = enumPageMode.ReadOnly Then
                    OutputIO.ControlDisable = True
                ElseIf ePageMode = enumPageMode.Edit Then
                    OutputIO.ControlDisable = False
                Else
                    If element.Reserve Then
                        OutputIO.ControlDisable = True
                    Else
                        OutputIO.ControlDisable = False
                    End If
                End If

            End If
        Next
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, SubTabPage)
        SubTabPage.Controls.Add(TableLayoutPanel_Tab_Main)
    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cParameter As New IOParameter
                If cIOManager.GetIOCfgFromID(CType(sender, Button).Name).PageType <> enumIOType.EL2008 Then
                    cParameter.DisableTriger = True
                End If
                cParameter.IOManager = cIOManager
                cParameter.CylinderManager = cCylinderManager
                cParameter.DebugMode = True
                cParameter.ListIO = cIOManager.GetIOCfgFromID(CType(sender, Button).Name).ListLockIO
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.Init(cLocalElement, cSystemElement)
                cParameter.TextBox_ID.Text = CType(sender, Button).Name
                cParameter.TextBox_NameA.Text = cIOManager.GetIOCfgFromID(CType(sender, Button).Name).Text
                cParameter.TextBox_NameA2.Text = cIOManager.GetIOCfgFromID(CType(sender, Button).Name).Text2
                cParameter.RadioButton_Toggle.Checked = IIf(cIOManager.GetIOCfgFromID(CType(sender, Button).Name).IOTriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_Y.Checked = cIOManager.GetIOCfgFromID(CType(sender, Button).Name).Reserve
                cParameter.RadioButton_N.Checked = Not cParameter.RadioButton_Y.Checked
                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cParameter.TextBox_NameA.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If
                    cIOLockManager.CheckIO(cParameter.ListIO)
                    StopRefreshUI()
                    cIOManager.ChangeIO(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text, cParameter.RadioButton_Y.Checked, IIf(cParameter.RadioButton_Toggle.Checked, enumIOTriggerType.Toggle, enumIOTriggerType.Tap), cParameter.ListIO)
                    CType(sender, Button).Text = cIOManager.GetIOCfgFromID(CType(sender, Button).Name).ActiveText
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                If ePageMode <> enumPageMode.Debug Then Return
                Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(CType(sender, Button).Name)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Toggle Then
                        Dim iPageNr As Integer = cIOManager.ListTypeNumber(cIOManager.GetIOCfgFromID(CType(sender, Button).Name).PageType)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1)
                        Dim dNewValue As Boolean = Not dOldValue
                        cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", dNewValue)
                    End If
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If ePageMode <> enumPageMode.Debug Then Return
            Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = True
                cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", dNewValue)
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If ePageMode <> enumPageMode.Debug Then Return
            Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = False
                cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", dNewValue)
            End If
        End If
    End Sub

    Private Sub TabControl_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            If ePageMode <> enumPageMode.Edit Then Return
            Dim cParameter As New IOGroupParameter
            Dim strOldType As String = String.Empty
            Dim iOldIndex As Integer = 0
            cParameter.TextFont = CType(sender, TabControl).Font
            cParameter.CurrentIndex = TabControl_IO.SelectedIndex + 1
            cParameter.MaxIndex = TabControl_IO.TabPages.Count
            cParameter.Init(cLocalElement, cSystemElement)
            cParameter.TextBox_ID.Text = TabControl_IO.SelectedTab.Name
            cParameter.ComboBoxEx_Position.Text = ""
            cParameter.ComboBox_Model.Text = cIOManager.GetIOPageCfgFromID(TabControl_IO.SelectedTab.Name).IOType.ToString
            strOldType = cIOManager.GetIOPageCfgFromID(TabControl_IO.SelectedTab.Name).IOType.ToString
            cParameter.TextBox_NameA.Text = cIOManager.GetIOPageCfgFromID(TabControl_IO.SelectedTab.Name).Text
            cParameter.TextBox_NameA2.Text = cIOManager.GetIOPageCfgFromID(TabControl_IO.SelectedTab.Name).Text2
            If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If cParameter.TextBox_NameA.Text = "" Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                    Return
                End If
                If cParameter.ComboBox_Model.Text = "" Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                    Return
                End If
                StopRefreshUI()
                cIOManager.ChangePage(TabControl_IO.SelectedTab.Name, [Enum].Parse(GetType(enumIOType), cParameter.ComboBox_Model.Text), cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text)
                If cParameter.ComboBoxEx_Position.Text <> "" Then
                    iOldIndex = CInt(cParameter.ComboBoxEx_Position.Text) - 1
                    cIOManager.MovePage(TabControl_IO.SelectedIndex + 1, cParameter.ComboBoxEx_Position.Text)
                Else
                    iOldIndex = TabControl_IO.SelectedIndex
                End If
                TabControl_IO.SelectedTab.Text = cIOManager.GetIOPageCfgFromID(TabControl_IO.SelectedTab.Name).ActiveText

                CreateTable(iOldIndex)
                StartRefreshUI()
            End If
        End If
    End Sub

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                If Not IsNothing(cErrorMessageManager) Then If cErrorMessageManager.ErrorMessageManagerState = enumErrorMessageManagerState.Alarm Then Continue While

                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "4", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                            Continue While
                        End If
                        cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", False)
                        If cMainButtonRunner.TeachMode Then cMainButtonRunner.IOCloeseTeachMode = True
                        iStep = iStep + 1

                    Case 3
                        If Not cMainButtonRunner.TeachMode Then
                            iStep = iStep + 1
                        End If

                    Case 4
                        If ePageMode = enumPageMode.Debug And cMachineManager.MachineStatus.bulPowerON Then
                            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", False)
                            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode", True)
                            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn1).ToString.IndexOf("EL2008") >= 0 Then
                                Dim i As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn1).ToString.Replace("EL2008-", "").Split(".")(0)
                                Dim j As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn1).ToString.Replace("EL2008-", "").Split(".")(1)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_DO_EL2008_AdsName + "[" + i.ToString + "," + j.ToString + "]", True)
                            End If
                            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn2).ToString.IndexOf("EL2008") >= 0 Then
                                Dim i As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn2).ToString.Replace("EL2008-", "").Split(".")(0)
                                Dim j As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn2).ToString.Replace("EL2008-", "").Split(".")(1)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_DO_EL2008_AdsName + "[" + i.ToString + "," + j.ToString + "]", True)
                            End If
                            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn3).ToString.IndexOf("EL2008") >= 0 Then
                                Dim i As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn3).ToString.Replace("EL2008-", "").Split(".")(0)
                                Dim j As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn3).ToString.Replace("EL2008-", "").Split(".")(1)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_DO_EL2008_AdsName + "[" + i.ToString + "," + j.ToString + "]", True)
                            End If
                            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn4).ToString.IndexOf("EL2008") >= 0 Then
                                Dim i As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn4).ToString.Replace("EL2008-", "").Split(".")(0)
                                Dim j As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn4).ToString.Replace("EL2008-", "").Split(".")(1)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_DO_EL2008_AdsName + "[" + i.ToString + "," + j.ToString + "]", True)
                            End If
                            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn5).ToString.IndexOf("EL2008") >= 0 Then
                                Dim i As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn5).ToString.Replace("EL2008-", "").Split(".")(0)
                                Dim j As Integer = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.DebugAutoOn5).ToString.Replace("EL2008-", "").Split(".")(1)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_DO_EL2008_AdsName + "[" + i.ToString + "," + j.ToString + "]", True)
                            End If
                        End If
                        iStep = iStep + 1

                    Case 5
                        Dim iPageNr As Integer = cIOManager.ListTypeNumber(enumIOType.EL1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DI_EL1008_AdsName, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EP1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DI_EP1008_AdsName, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EL2008)
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DO_EL2008_AdsName, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iStep = iStep + 1
                    Case 6
                        Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DI_EL1008_AdsName)
                        Dim lListDI2() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DI_EP1008_AdsName)
                        Dim lListDI3() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DO_EL2008_AdsName)


                        For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                            For Each subelement As clsIOCfg In element.ListIO.Values
                                Select Case subelement.PageType
                                    Case enumIOType.EL1008
                                        subelement.IO.SetIndicateBackColor(lListDI1((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1))
                                    Case enumIOType.EP1008
                                        subelement.IO.SetIndicateBackColor(lListDI2((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1))
                                    Case enumIOType.EL2008
                                        subelement.IO.SetIndicateBackColor(lListDI3((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1))
                                End Select
                            Next
                        Next


                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(ex, enumExceptionType.Alarm)
            End Try
            System.Threading.Thread.Sleep(10)
        End While

    End Sub

    Public Sub GetPageMode()
        Dim cPLCStepStatus As StructPLCStepStatus = cHMIPLC.ReadAny(HMI_PLC_Interface.PLC_StepStatus + "[1]", GetType(StructPLCStepStatus))
        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
            If Not cMachineManager.MachineStatus.bulManual And Not cMachineManager.MachineStatus.bulAuto And Not cMachineManager.MachineStatus.bulPowerON Then
                ePageMode = enumPageMode.Edit
            ElseIf cMachineManager.MachineStatus.bulManual Then
                If cPLCStepStatus.intStepID = 0 Or cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode Then
                    ePageMode = enumPageMode.Debug
                Else
                    ePageMode = enumPageMode.ReadOnly
                End If
            Else
                ePageMode = enumPageMode.ReadOnly
            End If
        Else
            If Not cMachineManager.MachineStatus.bulManual And Not cMachineManager.MachineStatus.bulAuto And Not cMachineManager.MachineStatus.bulPowerON Then
                ePageMode = enumPageMode.ReadOnly
            ElseIf cMachineManager.MachineStatus.bulManual Then
                If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer Then
                    If cPLCStepStatus.intStepID = 0 Or cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode Then
                        ePageMode = enumPageMode.Debug
                    Else
                        ePageMode = enumPageMode.ReadOnly
                    End If
                Else
                    ePageMode = enumPageMode.ReadOnly
                End If
            Else
                ePageMode = enumPageMode.ReadOnly
            End If
        End If
    End Sub

    Public Function StartRefreshUI() As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function
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
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DI_EL1008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DI_EP1008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DO_EL2008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DO_Festo_AdsName)


        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        Timer1.Enabled = False
        Do While bWaiting
            System.Threading.Thread.Sleep(5)
        Loop
        StopRefreshUI()
        cLocalElement.Remove(enumUIName.ChildrenIOForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenIOForm.ToString)
        Me.Dispose()
        Return True
    End Function


    Private Sub TabControl_IO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        bWaiting = True
        Timer1.Enabled = False
        CreateTable(0)
        StartRefreshUI()
        bWaiting = False
    End Sub
End Class



