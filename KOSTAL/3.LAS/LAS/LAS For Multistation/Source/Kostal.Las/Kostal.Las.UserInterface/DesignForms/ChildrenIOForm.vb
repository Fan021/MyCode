Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Enum enumPageMode
    [ReadOnly] = 6
    Debug = 7
    Edit = 8
End Enum
Public Class ChildrenIOForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As MainForm_Bosh
    Private strButtonName As String
    Private ePageMode As enumPageMode
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private bWaiting As Boolean = False
    Private cIOLockManager As clsIOLockManager
    Private cHMIPLC As TwinCatAds
    Private _xmlHandler As New XmlHandler
    Private cLanguageManager As Language
    Private cTips As clsTips
    Public cMainForm As MainForm_Mul
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        Try
            Me.cSystemElement = Devices
            Me.cLocalElement = cLocalElement
            cLanguageManager = CType(Devices(Language.Name), Language)
            cTips = CType(Devices(clsTips.Name), clsTips)

            cIOManager = New clsIOManager
            cIOManager.Init(Devices, Stations, MySettings)
            cIOManager.LoadData()

            cCylinderManager = New clsCylinderManager
            cCylinderManager.Init(Devices, Stations, MySettings)
            cCylinderManager.LoadData()

            Dim sResult As String = ""
            sResult = _xmlHandler.GetSectionInformation(MySettings.ConfigFolder, MySettings.ApplicationName, "GeneralInformation", "WtPLCName")
            cHMIPLC = CType(Devices(sResult), TwinCatAds)

            cIOLockManager = New clsIOLockManager
            cIOLockManager.IOManager = cIOManager
            cIOLockManager.CylinderManager = cCylinderManager
            cIOLockManager.cHMIPLC = cHMIPLC
            cIOLockManager.Init(Devices)



            GetPageMode()
            Timer1.Enabled = True
            Return True
        Catch ex As Exception
            Throw ex
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
            TabControl_IO.Enabled = False
            TabControl_IO.Controls.Clear()
            For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                Dim SubTabPage As New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Name = element.ID
                SubTabPage.BackColor = Color.White
                SubTabPage.Text = element.ActiveText
                SubTabPage.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Bold)
                TabControl_IO.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Bold)
                CreateIO(SubTabPage, element)
                ' Application.DoEvents()
                TabControl_IO.Controls.Add(SubTabPage)
            Next
            TabControl_IO.Enabled = True
            TabControl_IO.SelectedIndex = iSelectIndex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateIO(ByVal SubTabPage As TabPage, ByVal cIOPageCfg As clsIOPageCfg)
        SubTabPage.Controls.Clear()
        Dim TableLayoutPanel_Tab_Main As New TableLayoutPanel
        TableLayoutPanel_Tab_Main.ColumnCount = 3
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        TableLayoutPanel_Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab_Main.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab_Main.Name = "TableLayoutPanel_Tab_Main"
        TableLayoutPanel_Tab_Main.RowCount = 8 * 2 + 1
        TableLayoutPanel_Tab_Main.Dock = DockStyle.Fill

        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        For j = 1 To 8 * 2 - 1
            TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Next
        TableLayoutPanel_Tab_Main.RowStyles(0) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14)
        For j = 1 To 8 * 2 - 1 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20)
        Next

        For j = 2 To 8 * 2 - 1 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6)
        Next
        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        If cIOPageCfg.IOType = enumIOType.EL1008 Or cIOPageCfg.IOType = enumIOType.EP1008 Then
            Dim Label_Input As New Label
            Label_Input.Dock = System.Windows.Forms.DockStyle.Fill
            Label_Input.Font = SubTabPage.Font
            Label_Input.Name = "Input"
            Label_Input.Size = New System.Drawing.Size(223, 32)
            Label_Input.Text = cLanguageManager.Read("ChildrenIOForm", "Label_Input")
            Label_Input.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            TableLayoutPanel_Tab_Main.Controls.Add(Label_Input, 1, 0)
        Else
            Dim Label_Output As New Label
            Label_Output.Dock = System.Windows.Forms.DockStyle.Fill
            Label_Output.Font = SubTabPage.Font
            Label_Output.Name = "Output"
            Label_Output.Size = New System.Drawing.Size(223, 32)
            Label_Output.Text = cLanguageManager.Read("ChildrenIOForm", "Label_Output")
            Label_Output.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
            TableLayoutPanel_Tab_Main.Controls.Add(Label_Output, 1, 0)
        End If

        Dim Label_Input2 As New Label
        Label_Input2.Dock = System.Windows.Forms.DockStyle.Fill
        Label_Input2.Name = "Mode"
        Label_Input2.Size = New System.Drawing.Size(223, 32)
        Label_Input2.Font = New System.Drawing.Font("Calibri", SubTabPage.Font.Size)
        Label_Input2.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Label_Input2.TextAlign = ContentAlignment.MiddleRight
        Label_Input2.Text = cLanguageManager.Read("ChildrenIOForm", ePageMode.ToString）
        TableLayoutPanel_Tab_Main.Controls.Add(Label_Input2, 2, 0)

        For Each element As clsIOCfg In cIOPageCfg.ListIO.Values
            If cIOPageCfg.IOType = enumIOType.EL1008 Or cIOPageCfg.IOType = enumIOType.EP1008 Then
                Dim InputIO As New InputIO
                element.IO = InputIO
                InputIO.Dock = DockStyle.Fill
                InputIO.MainButton.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Regular)
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
                OutputIO.MainButton.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Regular)
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
        ' cFormFontResize.SetControls(cFormFontResize.CurrentRate, SubTabPage)
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
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenIOForm", "1"))
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
                        Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * 8})
                        Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * 8 + cIOCfg.YIndex - 1)
                        Dim dNewValue As Boolean = Not dOldValue
                        cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", dNewValue)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub TabControl_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl_IO.Click
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
                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenIOForm", "1"))
                    Return
                End If
                If cParameter.ComboBox_Model.Text = "" Then
                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenIOForm", "2"))
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

                Select Case iStep
                    Case 1
                        If ePageMode = enumPageMode.Debug Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_DebugMode, True)
                        ResetIO()
                        iStep = iStep + 1

                    Case 2
                        Dim iPageNr As Integer = cIOManager.ListTypeNumber(enumIOType.EL1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDI1() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_EL1008, GetType(Boolean()), New Integer() {iPageNr * 8})
                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EP1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDI2() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_EP1008, GetType(Boolean()), New Integer() {iPageNr * 8})
                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EL2008)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDI3() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_EL2008, GetType(Boolean()), New Integer() {iPageNr * 8})


                        For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                            For Each subelement As clsIOCfg In element.ListIO.Values
                                Select Case subelement.PageType
                                    Case enumIOType.EL1008
                                        subelement.IO.SetIndicateBackColor(lListDI1((subelement.XIndex - 1) * 8 + subelement.YIndex - 1))
                                    Case enumIOType.EP1008
                                        subelement.IO.SetIndicateBackColor(lListDI2((subelement.XIndex - 1) * 8 + subelement.YIndex - 1))
                                    Case enumIOType.EL2008
                                        subelement.IO.SetIndicateBackColor(lListDI3((subelement.XIndex - 1) * 8 + subelement.YIndex - 1))
                                End Select
                            Next
                        Next
                End Select
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            System.Threading.Thread.Sleep(10)
        End While

    End Sub

    Public Sub GetPageMode()
        'Dim cPLCStepStatus As StructPLCStepStatus = cHMIPLC.ReadAny(HMI_PLC_Interface.PLC_StepStatus + "[1]", GetType(StructPLCStepStatus))
        'If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
        '    If Not cMachineManager.MachineStatus.bulManual And Not cMachineManager.MachineStatus.bulAuto Then
        '        ePageMode = enumPageMode.Edit
        '    ElseIf cMachineManager.MachineStatus.bulManual Then
        '        If cPLCStepStatus.intStepID = 0 Or cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode Then
        '            ePageMode = enumPageMode.Debug
        '        Else
        '            ePageMode = enumPageMode.ReadOnly
        '        End If
        '    Else
        '        ePageMode = enumPageMode.ReadOnly
        '    End If
        'Else
        '    If Not cMachineManager.MachineStatus.bulManual And Not cMachineManager.MachineStatus.bulAuto Then
        '        ePageMode = enumPageMode.ReadOnly
        '    ElseIf cMachineManager.MachineStatus.bulManual Then
        '        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer Then
        '            If cPLCStepStatus.intStepID = 0 Or cMachineManager.MachineStatus.bulTeachMode Or cMachineManager.MachineStatus.bulDebugMode Then
        '                ePageMode = enumPageMode.Debug
        '            Else
        '                ePageMode = enumPageMode.ReadOnly
        '            End If
        '        Else
        '            ePageMode = enumPageMode.ReadOnly
        '        End If
        '    Else
        '        ePageMode = enumPageMode.ReadOnly
        '    End If
        'End If

        If cMainForm._PlcIsPoweredOn And cMainForm._PlcAutoManual = enumPLC_AUTO_MANUAL.Manual Then
            ePageMode = enumPageMode.Debug
        Else
            ePageMode = enumPageMode.Edit
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
        ResetIO()
        Return True
    End Function

    Private Sub ResetIO()
        Dim iPageNr As Integer = cIOManager.ListTypeNumber(enumIOType.EL1008)
        If iPageNr <= 0 Then iPageNr = 1
        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_EL1008, New Boolean(iPageNr * 8) {})

        iPageNr = cIOManager.ListTypeNumber(enumIOType.EP1008)
        If iPageNr <= 0 Then iPageNr = 1
        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_EP1008, New Boolean(iPageNr * 8) {})

        iPageNr = cIOManager.ListTypeNumber(enumIOType.EL2008)
        If iPageNr <= 0 Then iPageNr = 1
        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_EL2008, New Boolean(iPageNr * 8) {})

    End Sub
    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Timer1.Enabled = False
        Do While bWaiting
            System.Threading.Thread.Sleep(5)
        Loop
        StopRefreshUI()
        ' cLocalElement.Remove(enumUIName.ChildrenIOForm.ToString)
        ' cErrorMessageManager.Clean(enumUIName.ChildrenIOForm.ToString)
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



