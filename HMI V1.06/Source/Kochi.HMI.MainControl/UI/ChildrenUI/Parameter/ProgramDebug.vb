Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Threading

Public Class ProgramDebug
    Implements IChildrenUI
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private mMainForm As MainForm
    Private cHMIPLC As clsHMIPLC
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cThread As Thread
    Private bExit As Boolean
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDeviceManager As clsDeviceManager
    Private strButtonName As String = String.Empty
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIOManager As clsIOManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cSystemManager As clsSystemManager
    Public Event ChangedPage()
    Private cIOLockManager As clsIOLockManager

    Public Property ButtonName As String Implements UI.IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements UI.IChildrenUI.Init
        Me.cLocalElement = cLocalElement
        Me.cSystemElement = cSystemElement
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
        cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
        cIOManager = New clsIOManager
        cIOManager.Init(cSystemElement)

        cIOLockManager = New clsIOLockManager
        cIOLockManager.IOManager = cIOManager
        cIOLockManager.ProgramButton = cProgramButton
        cIOLockManager.ProgramCylinderButton = cProgramCylinderButton
        cIOLockManager.Init(cSystemElement)


        GetPageMode()
        InitForm()
        InitControlText()
        CreateIO(0)
        Return True
    End Function

    Public Function InitForm() As Boolean
        Panel_UI.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        If ePageMode = enumPageMode.Edit Then AddHandler TabControl_IO.MouseClick, AddressOf TabControl_MouseClick
        Return True
    End Function

    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements UI.IChildrenUI.Quit
        StopRefreshUI()
        Me.Dispose()
        Return True
    End Function

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements UI.IChildrenUI.UI
        Get
            Return Panel_UI
        End Get
    End Property

    Public Sub CreateIO(ByVal iIndex As Integer)
        TabControl_IO.Controls.Clear()
        For Each element As clsIOPageCfg In cProgramButton.ListPage.Values
            Dim cTabPage As New TabPage
            cTabPage.Name = element.ID
            cTabPage.Text = element.ActiveText
            cTabPage.Font = TabControl_IO.Font
            cTabPage.BackColor = Color.White
            Dim TableLayoutPanel_Tab_Main As New TableLayoutPanel
            CreateIO(TableLayoutPanel_Tab_Main, cTabPage, element)
            CreateCylinder(TableLayoutPanel_Tab_Main, cTabPage, cProgramCylinderButton.ListPage(element.ID))
            TabControl_IO.Controls.Add(cTabPage)
        Next
        TabControl_IO.SelectedIndex = iIndex
    End Sub

    Private Sub CreateIO(ByVal TableLayoutPanel_Tab_Main As TableLayoutPanel, ByVal SubTabPage As TabPage, ByVal cIOPageCfg As clsIOPageCfg)
        SubTabPage.Controls.Clear()
        TableLayoutPanel_Tab_Main.ColumnCount = 4
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.0!))
        TableLayoutPanel_Tab_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
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
        For Each element As clsIOCfg In cIOPageCfg.ListIO.Values
            If element.PageType = enumIOType.EL1008 Or element.PageType = enumIOType.EP1008 Then
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
        SubTabPage.Controls.Add(TableLayoutPanel_Tab_Main)
    End Sub

    Private Sub CreateCylinder(ByVal TableLayoutPanel_Tab_Main As TableLayoutPanel, ByVal SubTabPage As TabPage, ByVal cIOPageCfg As clsCylinderPageCfg)
        For Each element As clsCylinderCfg In cIOPageCfg.ListIO.Values
            Dim CylinderIO As New CylinderIO
            element.CylinderIO = CylinderIO
            CylinderIO.Dock = DockStyle.Fill
            CylinderIO.MainButtonA.Font = SubTabPage.Font
            CylinderIO.MainButtonB.Font = SubTabPage.Font
            CylinderIO.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
            CylinderIO.MainButtonA.Name = element.XIndex.ToString + "_" + element.YIndex.ToString + ".A"
            CylinderIO.MainButtonA.Text = element.ActiveTextA
            CylinderIO.MainButtonB.Name = element.XIndex.ToString + "_" + element.YIndex.ToString + ".B"
            CylinderIO.MainButtonB.Text = element.ActiveTextB
            TableLayoutPanel_Tab_Main.Controls.Add(CylinderIO, 3, (element.YIndex - 1) * 2 + 1)
            If ePageMode = enumPageMode.Edit Then
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Cylinder_Click
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Cylinder_MouseDown
                AddHandler CylinderIO.MainButtonA.MouseUp, AddressOf MainButton_Cylinder_MouseUp
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Cylinder_Click
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Cylinder_MouseDown
                AddHandler CylinderIO.MainButtonB.MouseUp, AddressOf MainButton_Cylinder_MouseUp
            End If

            If ePageMode = enumPageMode.Debug Then
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Cylinder_Click
                AddHandler CylinderIO.MainButtonA.MouseDown, AddressOf MainButton_Cylinder_MouseDown
                AddHandler CylinderIO.MainButtonA.MouseUp, AddressOf MainButton_Cylinder_MouseUp
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Cylinder_Click
                AddHandler CylinderIO.MainButtonB.MouseDown, AddressOf MainButton_Cylinder_MouseDown
                AddHandler CylinderIO.MainButtonB.MouseUp, AddressOf MainButton_Cylinder_MouseUp
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
    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cParameter As New IOParameter
                If cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).PageType <> enumIOType.EL2008 Then
                    cParameter.DisableTriger = True
                    cParameter.ShowType = True
                    cParameter.RadioButton_Input.Checked = True
                    cParameter.RadioButton_Output.Checked = False
                Else
                    cParameter.DisableTriger = False
                    cParameter.ShowType = True
                    cParameter.RadioButton_Input.Checked = False
                    cParameter.RadioButton_Output.Checked = True
                End If
                cParameter.IOManager = cIOManager
                cParameter.ProgramButton = cProgramButton
                cParameter.ProgramCylinderButton = cProgramCylinderButton
                cParameter.DebugMode = False
                cParameter.ListIO = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).ListLockIO
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.Init(cLocalElement, cSystemElement)
                cParameter.TextBox_ID.Text = CType(sender, Button).Name
                cParameter.TextBox_NameA.Text = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).Text
                cParameter.TextBox_NameA2.Text = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).Text2
                cParameter.RadioButton_Toggle.Checked = IIf(cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).IOTriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_Y.Checked = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).Reserve
                cParameter.RadioButton_N.Checked = Not cParameter.RadioButton_Y.Checked
                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cParameter.TextBox_NameA.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If
                    cIOLockManager.CheckIO(cParameter.ListIO)
                    StopRefreshUI()
                    cProgramButton.ChangeIO(cParameter.TextBox_ID.Text, cParameter.RadioButton_Input.Checked, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text, cParameter.RadioButton_Y.Checked, IIf(cParameter.RadioButton_Toggle.Checked, enumIOTriggerType.Toggle, enumIOTriggerType.Tap), cParameter.ListIO)
                    CType(sender, Button).Text = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name).ActiveText
                    CreateIO(TabControl_IO.SelectedIndex)
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Toggle Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Return
                        End If
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1)
                        Dim dNewValue As Boolean = Not dOldValue
                        cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                    End If
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
            If TypeOf cIOCfg.IO Is OutputIO Then
                If cIOCfg.Reserve Then Return
                If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = True
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                    cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                End If
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
            If TypeOf cIOCfg.IO Is OutputIO Then
                If cIOCfg.Reserve Then Return
                If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = False
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIO)
                    cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                End If
            End If
        End If
    End Sub

    Private Sub MainButton_Cylinder_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cParameter As New CylinderParameter
                Dim strTempName As String = CType(sender, Button).Name
                strTempName = strTempName.Split(CChar("."))(0)
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.DebugMode = False
                cParameter.IOManager = cIOManager
                cParameter.ProgramButton = cProgramButton
                cParameter.ProgramCylinderButton = cProgramCylinderButton
                cParameter.ListIOA = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ListLockIOA
                cParameter.ListIOB = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ListLockIOB

                cParameter.TextBox_ID.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).XIndex.ToString + "_" + cProgramCylinderButton.GetCylinderCfgFromID(strTempName).YIndex.ToString
                cParameter.TextBox_NameA.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).TextA
                cParameter.TextBox_NameA2.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).TextA2
                cParameter.TextBox_NameB.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).TextB
                cParameter.TextBox_NameB2.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).TextB2
                cParameter.RadioButton_Toggle.Checked = IIf(cProgramCylinderButton.GetCylinderCfgFromID(strTempName).TriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_YA.Checked = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ReserveA
                cParameter.RadioButton_NA.Checked = Not cParameter.RadioButton_YA.Checked

                cParameter.RadioButton_YOne.Checked = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).OneCylinder
                cParameter.RadioButton_NOne.Checked = Not cParameter.RadioButton_YOne.Checked

                cParameter.RadioButton_YB.Checked = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ReserveB
                cParameter.RadioButton_NB.Checked = Not cParameter.RadioButton_YB.Checked

                cParameter.SensorAType = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorAType
                cParameter.SensorAXIndex = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorAXIndex
                cParameter.SensorAYIndex = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorAYIndex
                cParameter.SensorBType = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorBType
                cParameter.SensorBXIndex = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorBXIndex
                cParameter.SensorBYIndex = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).SensorBYIndex
                cParameter.Init(cLocalElement, cSystemElement)

                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                    If cParameter.TextBox_NameA.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCylinderForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If
                    If cParameter.TextBox_NameB.Text = "" Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCylinderForm.ToString, "1", cParameter.Label_NameB.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If

                    If cParameter.ComboBoxEx_SensorAType.SelectedIndex > 0 And cParameter.ComboBoxEx1_SensorA.SelectedIndex < 0 Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCylinderForm.ToString, "5"), enumExceptionType.Alarm, enumUIName.ChildrenCylinderForm.ToString))
                        Return
                    End If

                    If cParameter.ComboBoxEx_SensorBType.SelectedIndex > 0 And cParameter.ComboBoxEx1_SensorB.SelectedIndex < 0 Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCylinderForm.ToString, "6"), enumExceptionType.Alarm, enumUIName.ChildrenCylinderForm.ToString))
                        Return
                    End If
                    cIOLockManager.CheckIO(cParameter.ListIOA)
                    cIOLockManager.CheckIO(cParameter.ListIOB)
                    StopRefreshUI()
                    cProgramCylinderButton.ChangeIO(cParameter.TextBox_ID.Text,
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
                    CType(cProgramCylinderButton.GetCylinderCfgFromID(strTempName).CylinderIO, CylinderIO).MainButtonA.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ActiveTextA
                    CType(cProgramCylinderButton.GetCylinderCfgFromID(strTempName).CylinderIO, CylinderIO).MainButtonB.Text = cProgramCylinderButton.GetCylinderCfgFromID(strTempName).ActiveTextB
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                Dim strTempName As String = CType(sender, Button).Name
                If strTempName.Split(CChar("."))(1) = "A" Then
                    strTempName = strTempName.Split(CChar("."))(0)
                    Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                    If cIOCfg.ReserveA Then Return
                    If cIOCfg.TriggerType = enumIOTriggerType.Toggle Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Return
                        End If
                        Dim iPageNr As Integer = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), New Integer() {HMI_PLC_Interface.CON_MAXIMUM_PageNumber * iPageNr})
                        Dim dOldValue As Boolean = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOA
                        Dim dNewValue As Boolean = Not dOldValue
                        cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA)
                        cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                        If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", False)
                    End If

                Else
                    strTempName = strTempName.Split(CChar("."))(0)
                    Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                    If cIOCfg.ReserveB Then Return
                    If cIOCfg.TriggerType = enumIOTriggerType.Toggle Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Return
                        End If
                        Dim iPageNr As Integer = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), New Integer() {HMI_PLC_Interface.CON_MAXIMUM_PageNumber * iPageNr})
                        Dim dOldValue As Boolean = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOB
                        Dim dNewValue As Boolean = Not dOldValue
                        cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB)
                        cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", False)
                    End If
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCylinderForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_Cylinder_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim strTempName As String = CType(sender, Button).Name
            If strTempName.Split(CChar("."))(1) = "A" Then
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveA Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = True
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA)
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                    If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", False)
                End If
            Else
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveB Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = True
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB)
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                    If cIOCfg.OneCylinder Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", False)
                End If
            End If
        End If
    End Sub

    Private Sub MainButton_Cylinder_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim strTempName As String = CType(sender, Button).Name
            If strTempName.Split(CChar("."))(1) = "A" Then
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveA Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = False
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIOA)
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                End If
            Else
                strTempName = strTempName.Split(CChar("."))(0)
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromID(strTempName)
                If cIOCfg.ReserveB Then Return
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                        Return
                    End If
                    Dim dNewValue As Boolean = False
                    cIOLockManager.CheckLockIO(cIOCfg.ListLockIOB)
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                End If
            End If
        End If
    End Sub


    Private Sub TabControl_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Right Then
            If ePageMode <> enumPageMode.Edit Then Return
            Dim cParameter As New CylinderGroupParameter
            Dim iOldIndex As Integer = 0
            cParameter.DisableMove = False
            cParameter.CurrentIndex = TabControl_IO.SelectedIndex + 1
            cParameter.MaxIndex = TabControl_IO.TabPages.Count
            cParameter.TextFont = CType(sender, TabControl).Font
            cParameter.Init(cLocalElement, cSystemElement)
            cParameter.TextBox_ID.Text = TabControl_IO.SelectedTab.Name
            cParameter.TextBox_NameA.Text = cProgramCylinderButton.GetCylinderPageCfgFromID(TabControl_IO.SelectedTab.Name).Text.ToString
            cParameter.TextBox_NameA2.Text = cProgramCylinderButton.GetCylinderPageCfgFromID(TabControl_IO.SelectedTab.Name).Text2.ToString

            If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If cParameter.TextBox_NameA.Text = "" Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCylinderForm.ToString, "8", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                    Return
                End If
                StopRefreshUI()
                cProgramCylinderButton.ChangePage(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text)
                cProgramButton.ChangePage(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text)
                TabControl_IO.SelectedTab.Text = cProgramCylinderButton.GetCylinderPageCfgFromID(TabControl_IO.SelectedTab.Name).ActiveText.ToString

                If cParameter.ComboBoxEx_Position.Text <> "" Then
                    iOldIndex = CInt(cParameter.ComboBoxEx_Position.Text) - 1
                    cProgramButton.MovePage(TabControl_IO.SelectedIndex + 1, cParameter.ComboBoxEx_Position.Text)
                    cProgramCylinderButton.MovePage(TabControl_IO.SelectedIndex + 1, cParameter.ComboBoxEx_Position.Text)
                    For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
                        cDeviceProgramButton = New clsDeviceProgramButton
                        cDeviceProgramButton.Init(cSystemElement)
                        cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 16)
                        cDeviceProgramButton.ChangPage(cProgramButton.ListNewIndex)
                        RaiseEvent ChangedPage()
                    Next
                Else
                    iOldIndex = TabControl_IO.SelectedIndex
                End If
                CreateIO(iOldIndex)
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentDevicesForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentDevicesForm.ToString, "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EL1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DI_EL1008_AdsName, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cIOManager.ListTypeNumber(enumIOType.EP1008)
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_DI_EP1008_AdsName, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iStep = iStep + 1

                    Case 4
                        Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramButton)
                        For Each element As clsIOPageCfg In cProgramButton.ListPage.Values
                            For Each subelement As clsIOCfg In element.ListIO.Values
                                subelement.IO.SetIndicateBackColor(lListDI1((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1))
                            Next
                        Next

                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramCylinderButton)
                        Dim lListDI2() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DI_EL1008_AdsName)
                        Dim lListDI3() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_DI_EP1008_AdsName)

                        For Each element As clsCylinderPageCfg In cProgramCylinderButton.ListPage.Values
                            For Each subelement As clsCylinderCfg In element.ListIO.Values
                                subelement.CylinderIO.SetButtonBackColorA(lCylinder((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1).bulDOA)
                                subelement.CylinderIO.SetButtonBackColorB(lCylinder((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1).bulDOB)
                                If subelement.SensorAType = enumIOType.EL1008 Then
                                    If ((subelement.SensorAXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorAYIndex - 1) > lListDI2.Count - 1 Then
                                        subelement.SensorAType = enumIOType.NONE
                                    End If
                                End If
                                If subelement.SensorAType = enumIOType.EP1008 Then
                                    If ((subelement.SensorAXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorAYIndex - 1) > lListDI3.Count - 1 Then
                                        subelement.SensorAType = enumIOType.NONE
                                    End If
                                End If

                                If subelement.SensorBType = enumIOType.EL1008 Then
                                    If ((subelement.SensorBXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorBYIndex - 1) > lListDI2.Count - 1 Then
                                        subelement.SensorBType = enumIOType.NONE
                                    End If
                                End If

                                If subelement.SensorBType = enumIOType.EP1008 Then
                                    If ((subelement.SensorBXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorBYIndex - 1) > lListDI3.Count - 1 Then
                                        subelement.SensorBType = enumIOType.NONE
                                    End If
                                End If
                                If subelement.SensorAType = enumIOType.EL1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lListDI2((subelement.SensorAXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorAYIndex - 1))
                                End If
                                If subelement.SensorAType = enumIOType.EP1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lListDI3((subelement.SensorAXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorAYIndex - 1))
                                End If
                                If subelement.SensorAType = enumIOType.NONE Then
                                    subelement.CylinderIO.SetIndicateBackColorA(lCylinder((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1).bulDOA)
                                End If

                                If subelement.SensorBType = enumIOType.EL1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lListDI2((subelement.SensorBXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorBYIndex - 1))
                                End If
                                If subelement.SensorBType = enumIOType.EP1008 Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lListDI3((subelement.SensorBXIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.SensorBYIndex - 1))
                                End If
                                If subelement.SensorBType = enumIOType.NONE Then
                                    subelement.CylinderIO.SetIndicateBackColorB(lCylinder((subelement.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + subelement.YIndex - 1).bulDOB)
                                End If
                            Next
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
        System.Threading.Thread.Sleep(10)
        If Not IsNothing(cThread) Then cThread.Abort()
       
        Return True
    End Function

    Public Function StartRefreshUI() As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()
        Return True
    End Function
End Class