Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base
Imports System.Windows.Forms
Imports System.Drawing

Public Class ChildrenCylinderForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As clsIniHandler
    Private cHMIPLC As TwinCatAds

    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As Form
    Private strButtonName As String
    Private ePageMode As enumPageMode
    Private cCylinderManager As clsCylinderManager
    Private cIOManager As clsIOManager
    Private cIOLockManager As clsIOLockManager
    Private _xmlHandler As New XmlHandler
    Private Devices As Dictionary(Of String, Object)
    Private Stations As Dictionary(Of String, IStationTypeBase)
    Private AppSettings As Settings
    Private mXmlHandler As New XmlHandler
    Private cLanguageManager As Language
    Private cTips As clsTips
    Public cMainForm As IMainForm

    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            Me.Devices = Devices
            Me.cSystemElement = Devices
            Me.Stations = Stations
            Me.AppSettings = _AppSettings
            Dim sResult As String = ""
            sResult = _xmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "WtPLCName")
            cHMIPLC = CType(Devices(sResult), TwinCatAds)

            cLanguageManager = CType(Devices(Language.Name), Language)
            cTips = CType(Devices(clsTips.Name), clsTips)
            cIOManager = New clsIOManager
            cIOManager.Init(Devices, Stations, AppSettings)
            cIOManager.LoadData()

            cCylinderManager = New clsCylinderManager
            cCylinderManager.Init(Devices, Stations, AppSettings)
            cCylinderManager.LoadData()

            cIOLockManager = New clsIOLockManager
            cIOLockManager.IOManager = cIOManager
            cIOLockManager.CylinderManager = cCylinderManager
            cIOLockManager.cHMIPLC = cHMIPLC
            cIOLockManager.Init(Devices)

            GetPageMode()
            CreateTable()
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
        CreateTable()
        If ePageMode = enumPageMode.Edit Then AddHandler TabControl_Cylinder.MouseClick, AddressOf TabControl_MouseClick
        Return True
    End Function

    Private Sub CreateTable()
        Try
            TabControl_Cylinder.Controls.Clear()
            For Each element As clsCylinderPageCfg In cCylinderManager.ListPage.Values
                Dim SubTabPage As New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Name = element.ID
                SubTabPage.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Bold)
                TabControl_Cylinder.Font = New Font("Calibri", 12 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Bold)

                SubTabPage.BackColor = Color.White
                SubTabPage.Text = element.ActiveText
                CreateIO(SubTabPage, element)
                Application.DoEvents()
                TabControl_Cylinder.Controls.Add(SubTabPage)
                System.Threading.Thread.Sleep(10)
            Next
            StartRefreshUI()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateIO(ByVal SubTabPage As TabPage, ByVal cIOPageCfg As clsCylinderPageCfg)
        SubTabPage.Controls.Clear()
        Dim TableLayoutPanel_Tab_Main As New TableLayoutPanel
        TableLayoutPanel_Tab_Main.ColumnCount = 3
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.0!))
        TableLayoutPanel_Tab_Main.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab_Main.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab_Main.Name = "TableLayoutPanel_Tab_Main"
        TableLayoutPanel_Tab_Main.RowCount = 8 * 2 + 1
        TableLayoutPanel_Tab_Main.Dock = DockStyle.Fill
        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))

        For j = 1 To 8 * 2 - 1
            TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.0!))
        Next
        TableLayoutPanel_Tab_Main.RowStyles(0) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12)
        For j = 1 To 8 * 2 - 1 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20)
        Next

        For j = 2 To 8 * 2 - 1 Step 2
            TableLayoutPanel_Tab_Main.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6)
        Next

        TableLayoutPanel_Tab_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))

        Dim Label_Input2 As New Label
        Label_Input2.Dock = System.Windows.Forms.DockStyle.Fill
        Label_Input2.Font = New System.Drawing.Font("Calibri", SubTabPage.Font.Size)
        Label_Input2.Name = "Mode"
        Label_Input2.Size = New System.Drawing.Size(223, 32)

        Label_Input2.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
        Label_Input2.TextAlign = ContentAlignment.MiddleRight
        Label_Input2.Text = cLanguageManager.Read("ChildrenIOForm", ePageMode.ToString)
        TableLayoutPanel_Tab_Main.Controls.Add(Label_Input2, 2, 0)

        For Each element As clsCylinderCfg In cIOPageCfg.ListIO.Values
            Dim CylinderIO As New CylinderIO
            element.CylinderIO = CylinderIO
            CylinderIO.Dock = DockStyle.Fill
            CylinderIO.MainButtonA.Font = New Font("Calibri", 11 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Regular)
            CylinderIO.MainButtonB.Font = New Font("Calibri", 11 * cMainForm.cFormFontResize.CurrentRate, FontStyle.Regular)
            CylinderIO.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
            CylinderIO.MainButtonA.Name = element.XIndex.ToString + "_" + element.YIndex.ToString + ".A"
            CylinderIO.MainButtonA.Text = element.ActiveTextA
            CylinderIO.MainButtonB.Name = element.XIndex.ToString + "_" + element.YIndex.ToString + ".B"
            CylinderIO.MainButtonB.Text = element.ActiveTextB
            TableLayoutPanel_Tab_Main.Controls.Add(CylinderIO, 1, (element.YIndex - 1) * 2 + 1)
            If ePageMode = enumPageMode.Edit Then
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Click
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Click
            End If

            If ePageMode = enumPageMode.Debug Then
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Click
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_MouseDown
                AddHandler CylinderIO.MainButtonA.MouseUp, AddressOf MainButton_MouseUp
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Click
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_MouseDown
                AddHandler CylinderIO.MainButtonB.MouseUp, AddressOf MainButton_MouseUp
            End If

            If ePageMode = enumPageMode.ReadOnly Then
                CylinderIO.DisableA = True
                CylinderIO.DisableB = True
            ElseIf ePageMode = enumPageMode.Edit Then
                CylinderIO.DisableA = False
                CylinderIO.DisableB = False
            Else
                If element.ReserveA Then
                    CylinderIO.DisableA = True
                Else
                    CylinderIO.DisableA = False
                End If

                If element.ReserveB Then
                    CylinderIO.DisableB = True
                Else
                    CylinderIO.DisableB = False
                End If
            End If

        Next
        SubTabPage.Controls.Add(TableLayoutPanel_Tab_Main)
    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cParameter As New CylinderParameter
                Dim strTempName As String = CType(sender, Button).Name
                strTempName = strTempName.Split(CChar("."))(0)
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.DebugMode = True
                cParameter.IOManager = cIOManager
                cParameter.CylinderManager = cCylinderManager
                cParameter.ListIOA = cCylinderManager.GetCylinderCfgFromID(strTempName).ListLockIOA
                cParameter.ListIOB = cCylinderManager.GetCylinderCfgFromID(strTempName).ListLockIOB

                cParameter.TextBox_ID.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).XIndex.ToString + "_" + cCylinderManager.GetCylinderCfgFromID(strTempName).YIndex.ToString
                cParameter.TextBox_NameA.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).TextA
                cParameter.TextBox_NameA2.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).TextA2
                cParameter.TextBox_NameB.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).TextB
                cParameter.TextBox_NameB2.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).TextB2
                cParameter.RadioButton_Toggle.Checked = IIf(cCylinderManager.GetCylinderCfgFromID(strTempName).TriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_YA.Checked = cCylinderManager.GetCylinderCfgFromID(strTempName).ReserveA
                cParameter.RadioButton_NA.Checked = Not cParameter.RadioButton_YA.Checked

                cParameter.RadioButton_YB.Checked = cCylinderManager.GetCylinderCfgFromID(strTempName).ReserveB
                cParameter.RadioButton_NB.Checked = Not cParameter.RadioButton_YB.Checked
                cParameter.RadioButton_YOne.Checked = cCylinderManager.GetCylinderCfgFromID(strTempName).OneCylinder
                cParameter.RadioButton_NOne.Checked = Not cParameter.RadioButton_YOne.Checked

                cParameter.SensorAType = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorAType
                cParameter.SensorAXIndex = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorAXIndex
                cParameter.SensorAYIndex = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorAYIndex
                cParameter.SensorBType = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorBType
                cParameter.SensorBXIndex = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorBXIndex
                cParameter.SensorBYIndex = cCylinderManager.GetCylinderCfgFromID(strTempName).SensorBYIndex
                cParameter.Init(Devices, Stations, AppSettings)

                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                    If cParameter.TextBox_NameA.Text = "" Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenCylinderForm", "1"))
                        Return
                    End If
                    If cParameter.TextBox_NameB.Text = "" Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenCylinderForm", "2"))
                        Return
                    End If

                    If cParameter.ComboBoxEx_SensorAType.SelectedIndex > 0 And cParameter.ComboBoxEx1_SensorA.SelectedIndex < 0 Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenCylinderForm", "3"))
                        Return
                    End If

                    If cParameter.ComboBoxEx_SensorBType.SelectedIndex > 0 And cParameter.ComboBoxEx1_SensorB.SelectedIndex < 0 Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenCylinderForm", "4"))
                        Return
                    End If
                    cIOLockManager.CheckIO(cParameter.ListIOA)
                    cIOLockManager.CheckIO(cParameter.ListIOB)
                    StopRefreshUI()
                    cCylinderManager.ChangeIO(cParameter.TextBox_ID.Text,
                                               cParameter.TextBox_NameA.Text,
                                               cParameter.TextBox_NameA2.Text,
                                               cParameter.TextBox_NameB.Text,
                                               cParameter.TextBox_NameB2.Text,
                                               cParameter.RadioButton_YA.Checked,
                                               cParameter.RadioButton_YB.Checked,
                                               IIf(cParameter.RadioButton_Toggle.Checked, enumIOTriggerType.Toggle, enumIOTriggerType.Tap),
                                               cParameter.SensorAType,
                                               cParameter.SensorAXIndex,
                                               cParameter.SensorAYIndex,
                                               cParameter.SensorBType,
                                               cParameter.SensorBXIndex,
                                               cParameter.SensorBYIndex,
                                               cParameter.RadioButton_YOne.Checked,
                                               cParameter.ListIOA,
                                               cParameter.ListIOB
                                              )
                    CType(cCylinderManager.GetCylinderCfgFromID(strTempName).CylinderIO, CylinderIO).MainButtonA.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).ActiveTextA
                    CType(cCylinderManager.GetCylinderCfgFromID(strTempName).CylinderIO, CylinderIO).MainButtonB.Text = cCylinderManager.GetCylinderCfgFromID(strTempName).ActiveTextB
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                If ePageMode <> enumPageMode.Debug Then Return
                Dim strTempName As String = CType(sender, Button).Name
                If strTempName.Split(CChar("."))(1) = "A" Then
                    strTempName = strTempName.Split(CChar("."))(0)
                    Dim sResult As String = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "Cylinder_Max_Page")
                    If sResult = "" Then
                        sResult = "1"
                    End If
                    Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                    If cIOCfg.ReserveA Then Return
                    If cIOCfg.TriggerType = enumIOTriggerType.Toggle Then
                        Dim iPageNr As Integer = sResult
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(KostalAdsVariables.HMI_Cylinder, GetType(StructDebugCylinder()), New Integer() {8 * iPageNr})
                        Dim dOldValue As Boolean = lCylinder((cIOCfg.XIndex - 1) * 8 + cIOCfg.YIndex - 1).bulDOA
                        Dim dNewValue As Boolean = Not dOldValue
                        If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA) Then Return
                        cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", dNewValue)
                        If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", False)
                    End If

                Else
                    strTempName = strTempName.Split(CChar("."))(0)
                    Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                    If cIOCfg.ReserveB Then Return
                    Dim sResult As String = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "Cylinder_Max_Page")
                    If sResult = "" Then
                        sResult = "1"
                    End If
                    If cIOCfg.TriggerType = enumIOTriggerType.Toggle Then
                        Dim iPageNr As Integer = sResult
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(KostalAdsVariables.HMI_Cylinder, GetType(StructDebugCylinder()), New Integer() {8 * iPageNr})
                        Dim dOldValue As Boolean = lCylinder((cIOCfg.XIndex - 1) * 8 + cIOCfg.YIndex - 1).bulDOB
                        Dim dNewValue As Boolean = Not dOldValue
                        If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB) Then Return
                        cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", dNewValue)
                        If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", False)
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
            Dim strTempName As String = CType(sender, Button).Name
            If strTempName.Split(CChar("."))(1) = "A" Then
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveA Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim dNewValue As Boolean = True
                    If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA) Then Return
                    cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", dNewValue)
                    If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", False)
                End If
            Else
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveB Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim dNewValue As Boolean = True
                    If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB) Then Return
                    cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", dNewValue)
                    If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", False)
                End If
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            If ePageMode <> enumPageMode.Debug Then Return
            Dim strTempName As String = CType(sender, Button).Name
            If strTempName.Split(CChar("."))(1) = "A" Then
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveA Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim dNewValue As Boolean = False
                    If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA) Then Return
                    cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", dNewValue)
                End If
            Else
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveB Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim dNewValue As Boolean = False
                    If Not cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB) Then Return
                    cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", dNewValue)
                End If
            End If
        End If
    End Sub

    Private Sub TabControl_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabControl_Cylinder.MouseClick
        If e.Button = MouseButtons.Right Then
            If ePageMode <> enumPageMode.Edit Then Return
            Dim cParameter As New CylinderGroupParameter
            cParameter.TextFont = CType(sender, TabControl).Font
            cParameter.Init(cLocalElement, cSystemElement)
            cParameter.TextBox_ID.Text = TabControl_Cylinder.SelectedTab.Name
            cParameter.TextBox_NameA.Text = cCylinderManager.GetCylinderPageCfgFromID(TabControl_Cylinder.SelectedTab.Name).Text.ToString
            cParameter.TextBox_NameA2.Text = cCylinderManager.GetCylinderPageCfgFromID(TabControl_Cylinder.SelectedTab.Name).Text2.ToString

            If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If cParameter.TextBox_NameA.Text = "" Then
                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenCylinderForm", "5"))
                    Return
                End If
                StopRefreshUI()
                cCylinderManager.ChangePage(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text)
                TabControl_Cylinder.SelectedTab.Text = cCylinderManager.GetCylinderPageCfgFromID(TabControl_Cylinder.SelectedTab.Name).ActiveText.ToString
                StartRefreshUI()
            End If
        End If
    End Sub

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        Dim sResult As String = 1
        While Not bExit
            Try
                Application.DoEvents()
                Select Case iStep
                    Case 1
                        sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "Cylinder_Max_Page")
                        If sResult = "" Then
                            sResult = "1"
                        End If
                        ResetIO(True)
                        iStep = iStep + 1
                    Case 2
                        Dim iPageNr As Integer = cIOManager.ListTypeNumber(enumIOType.EL1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDI1() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_EL1008, GetType(Boolean()), New Integer() {iPageNr * 8})
                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EP1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDI2() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_EP1008, GetType(Boolean()), New Integer() {iPageNr * 8})

                        iPageNr = sResult
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(KostalAdsVariables.HMI_Cylinder, GetType(StructDebugCylinder()), New Integer() {iPageNr * 8})

                        For Each element As clsCylinderPageCfg In cCylinderManager.ListPage.Values
                            For Each subelement As clsCylinderCfg In element.ListIO.Values
                                subelement.CylinderIO.SetButtonBackColorA(lCylinder((subelement.XIndex - 1) * 8 + subelement.YIndex - 1).bulDOA)
                                subelement.CylinderIO.SetButtonBackColorB(lCylinder((subelement.XIndex - 1) * 8 + subelement.YIndex - 1).bulDOB)
                                If subelement.SensorAType = enumIOType.EL1008 Then
                                    If ((subelement.SensorAXIndex - 1) * 8 + subelement.SensorAYIndex - 1) > lListDI1.Count - 1 Then
                                        subelement.SensorAType = enumIOType.NONE
                                    End If
                                End If
                                If subelement.SensorAType = enumIOType.EP1008 Then
                                    If ((subelement.SensorAXIndex - 1) * 8 + subelement.SensorAYIndex - 1) > lListDI2.Count - 1 Then
                                        subelement.SensorAType = enumIOType.NONE
                                    End If
                                End If

                                If subelement.SensorBType = enumIOType.EL1008 Then
                                    If ((subelement.SensorBXIndex - 1) * 8 + subelement.SensorBYIndex - 1) > lListDI1.Count - 1 Then
                                        subelement.SensorBType = enumIOType.NONE
                                    End If
                                End If

                                If subelement.SensorBType = enumIOType.EP1008 Then
                                    If ((subelement.SensorBXIndex - 1) * 8 + subelement.SensorBYIndex - 1) > lListDI2.Count - 1 Then
                                        subelement.SensorBType = enumIOType.NONE
                                    End If
                                End If

                                If subelement.SensorAType = enumIOType.EL1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lListDI1((subelement.SensorAXIndex - 1) * 8 + subelement.SensorAYIndex - 1))
                                End If
                                If subelement.SensorAType = enumIOType.EP1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lListDI2((subelement.SensorAXIndex - 1) * 8 + subelement.SensorAYIndex - 1))
                                End If
                                If subelement.SensorAType = enumIOType.NONE Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lCylinder((subelement.XIndex - 1) * 8 + subelement.YIndex - 1).bulDOA)
                                End If

                                If subelement.SensorBType = enumIOType.EL1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lListDI1((subelement.SensorBXIndex - 1) * 8 + subelement.SensorBYIndex - 1))
                                End If
                                If subelement.SensorBType = enumIOType.EP1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lListDI2((subelement.SensorBXIndex - 1) * 8 + subelement.SensorBYIndex - 1))
                                End If
                                If subelement.SensorBType = enumIOType.NONE Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lCylinder((subelement.XIndex - 1) * 8 + subelement.YIndex - 1).bulDOB)
                                End If
                            Next
                        Next

                End Select
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                '  If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCylinderForm.ToString))
            End Try
            System.Threading.Thread.Sleep(10)
        End While


    End Sub

    Public Sub GetPageMode()
        Dim cPLCStepStatus As structStationOverviewInfo = cHMIPLC.ReadAny(KostalAdsVariables.PLC_arrOverviewInfo + "[2]", GetType(structStationOverviewInfo))
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
        Dim sResult As String = String.Empty
        Dim bAnytimeDebug As Boolean = True
        Try
            sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "AnytimeDebug")
            If sResult.ToUpper = "FALSE" Then
                bAnytimeDebug = False
            End If
        Catch ex As Exception

        End Try

        If cMainForm.PlcIsPoweredOn And cMainForm.PlcAutoManual = enumPLC_AUTO_MANUAL.Manual And (cPLCStepStatus.iActualStepNumber <= 1 Or bAnytimeDebug) Then
            ePageMode = enumPageMode.Debug
        ElseIf cMainForm.PlcIsPoweredOn And cMainForm.PlcAutoManual = enumPLC_AUTO_MANUAL.Manual Then
            ePageMode = enumPageMode.ReadOnly
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
        ResetIO(False)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_DebugMode, False)
        Return True
    End Function

    Public Sub ResetIO(ByVal bResult As Boolean)
        Dim sResult As String = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "Cylinder_Max_Page")
        If sResult = "" Then
            sResult = "1"
        End If
        Dim iPageNr As Integer = sResult
        If iPageNr <= 0 Then iPageNr = 0
        Dim bAutoReset As Boolean = True
        Try
            sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "DebugAutoReset")
            If sResult.ToUpper = "FALSE" Then
                bAutoReset = False
            End If
        Catch ex As Exception
        End Try
        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * 8).ToArray()
        If Not IsNothing(cHMIPLC) And bAutoReset Then cHMIPLC.WriteAny(KostalAdsVariables.HMI_Cylinder, cDefaultValue)

    End Sub

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefreshUI()
        For Each cIOPageCfg As clsCylinderPageCfg In cCylinderManager.ListPage.Values
            For Each element As clsCylinderCfg In cIOPageCfg.ListIO.Values
                RemoveHandler element.CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Click
                RemoveHandler element.CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_MouseDown
                RemoveHandler element.CylinderIO.MainButtonA.MouseUp, AddressOf MainButton_MouseUp
                RemoveHandler element.CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Click
                RemoveHandler element.CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_MouseDown
                RemoveHandler element.CylinderIO.MainButtonB.MouseUp, AddressOf MainButton_MouseUp
            Next
        Next
        ' cLocalElement.Remove(enumUIName.ChildrenCylinderForm.ToString)
        ' cErrorMessageManager.Clean(enumUIName.ChildrenCylinderForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class