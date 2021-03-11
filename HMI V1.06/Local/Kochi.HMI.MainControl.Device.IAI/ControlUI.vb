Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent
Imports System.Drawing.Drawing2D
Imports System.IO
Imports Kochi.HMI.MainControl.LocalDevice
Public Class ControlUI
    Implements IControlUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Protected lListActionStep As New Dictionary(Of String, clsPointCfg)
    Private cVariantManager As clsVariantManager
    Protected cLanguageManager As clsLanguageManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Private mMainForm As IMainUI
    Private cIAI As clsIAI
    Private cPictureManager As clsPictureManager
    Private OldStructIAI As New StructIAI
    Private TempStructIAI As New StructIAI
    Public Const FormName As String = "IAIControlUI"
    Private cMachineStationCfg As clsMachineStationCfg
    Private cIniHandler As clsIniHandler
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cSystemManager As clsSystemManager
    Private lListIO As New Dictionary(Of Integer, HMIButtonWithIndicate)
    Private cDeviceCfg As clsDeviceCfg
    Private lLOldPosition As New List(Of String)
    Private lLOldName As New List(Of String)
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cMachineManager As clsMachineManager

    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cIAI = value
        End Set
        Get
            Return cIAI
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cIAI.Name)
            cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 0)
            InitForm()
            InitControlText()
            GetPageMode()
            '  CreatIO()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False

        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Variant.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_Variant")
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Move.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_Move")
        HmiButton_Move.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Save.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_Save")
        HmiButton_Save.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Text = cLanguageManager.GetUserTextLine("IAI", "HmiPassFailButton1")
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Text = cLanguageManager.GetUserTextLine("IAI", "HmiPassFailButton3")
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_STPEnable.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_STPEnable")
        HmiButton_STPEnable.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_Modify.Text = cLanguageManager.GetUserTextLine("IAI", "HmiButton_Modify")
        HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Name.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Name")
        HmiLabel_Name.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_Ready.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Ready")
        HmiLabel_Ready.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Name.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Alarm.Label.Text = cLanguageManager.GetUserTextLine("IAI", "HmiLabel_Alarm")
        HmiLabel_Alarm.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        Dim iCnt As Integer = 0
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
            If cVariantManager.CurrentVariantCfg.Variant = element.Variant Then
                HmiComboBox_Variant.ComboBox.SelectedIndex = iCnt
            End If
            iCnt = iCnt + 1
        Next

        HmiDataView_Point.Rows.Clear()
        HmiDataView_Point.Columns.Clear()
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("IAI", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("IAI", "Name")
        PostTest_Name.Name = "PostTest_Name"
        HmiDataView_Point.Columns.Add(PostTest_Name)

        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("IAI", "Point")
        PostTest_Program.Name = "PostTest_Point"
        HmiDataView_Point.Columns.Add(PostTest_Program)

        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If

       

        HmiComboBox_Pro.ComboBox.Items.Clear()
        For i = 1 To 100
            HmiComboBox_Pro.ComboBox.Items.Add(i.ToString)
        Next
        HmiComboBox_Pro.ComboBox.SelectedIndex = -1
        HmiButton_Variant.Button.Enabled = False
        HmiButton_Move.Enabled = False
        HmiButton_Modify.Button.Enabled = False
        HmiButton_Save.Button.Enabled = False

        DisableButton()
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Modify.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Save.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_MotorEnable.Click, AddressOf Button_Click
        AddHandler HmiButton_STPEnable.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton1.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton3.Click, AddressOf Button_Click
        AddHandler HmiButton_Move.Click, AddressOf Button_Click
        AddHandler HmiDataView_Point.CellClick, AddressOf HmiDataView_Point_CellClick
        Return True
    End Function


    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub



    Private Sub CreatIO()
        Try
            For i = 1 To 5
                Dim OutputIO As HMIButtonWithIndicate
                If lListIO.ContainsKey(i) Then
                    OutputIO = lListIO(i)
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_Click
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                    RemoveHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                Else
                    OutputIO = New HMIButtonWithIndicate
                    HmiTableLayoutPanel_Body_Top_Right.Controls.Add(OutputIO, i - 1, 4)
                    OutputIO.Font = New System.Drawing.Font("Calibri", 8.0!)
                    lListIO.Add(i, OutputIO)
                End If

                Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(i)
                If cDeviceProgramCfg.Type = clsProgramButton.Name Or cDeviceProgramCfg.Type = "" Then
                    Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(cDeviceProgramCfg.Index)
                    OutputIO.Dock = DockStyle.Fill
                    OutputIO.Name = i
                    OutputIO.BackColor = System.Drawing.SystemColors.Control
                    OutputIO.Text = cIOCfg.ActiveText
                    OutputIO.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
                    If ePageMode = enumPageMode.Debug Then
                        If cIOCfg.Reserve Then OutputIO.Enabled = False
                    End If
                    If TypeOf cIOCfg.IO Is InputIO Then
                        OutputIO.ControlDisable = True
                    Else
                        OutputIO.ControlDisable = False
                    End If
                    If ePageMode = enumPageMode.Edit Then
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_Click
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                        AddHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                    End If
                    If ePageMode = enumPageMode.Debug Then
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_Click
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                        AddHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                    End If
                    OutputIO.BackColor = Color.Transparent
                End If

                If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                    Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(cDeviceProgramCfg.Index)
                    OutputIO.Dock = DockStyle.Fill
                    OutputIO.Name = i
                    OutputIO.BackColor = System.Drawing.SystemColors.Control
                    Select Case cDeviceProgramCfg.CylinderType
                        Case "A"
                            OutputIO.Text = cIOCfg.ActiveButtonTextA
                        Case "B"
                            OutputIO.Text = cIOCfg.ActiveButtonTextB
                        Case Else
                            OutputIO.Text = cIOCfg.ActiveButtonTextB
                    End Select

                    OutputIO.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
                    If ePageMode = enumPageMode.Debug Then
                        Select Case cDeviceProgramCfg.CylinderType
                            Case "A"
                                If cIOCfg.ReserveA Then OutputIO.Enabled = False
                            Case "B"
                                If cIOCfg.ReserveB Then OutputIO.Enabled = False
                            Case Else
                                If cIOCfg.ReserveB Then OutputIO.Enabled = False
                        End Select
                    End If
                    If ePageMode = enumPageMode.Edit Then
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_Click
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                        AddHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                    End If
                    If ePageMode = enumPageMode.Debug Then
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_Click
                        AddHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                        AddHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                    End If
                    OutputIO.BackColor = Color.Transparent
                End If
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Edit Then Return
                Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
                Dim cParameter As New ProgramDebugForm
                cParameter.TextFont = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold)
                cParameter.CurrentIndex = cDeviceProgramCfg.Index
                cParameter.CurrentType = cDeviceProgramCfg.Type
                Select Case cDeviceProgramCfg.CylinderType
                    Case "A"
                        cParameter.RadioButton_A.Checked = True
                    Case "B"
                        cParameter.RadioButton_B.Checked = True
                    Case "AB"
                        cParameter.RadioButton_AB.Checked = True
                    Case Else
                        cParameter.RadioButton_AB.Checked = True
                End Select
                cParameter.Init(cLocalElement, cSystemElement)
                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    StopRefresh(cLocalElement, cSystemElement)
                    Dim strCyinderType As String = "AB"
                    If cParameter.RadioButton_A.Checked Then
                        strCyinderType = "A"
                    End If
                    If cParameter.RadioButton_B.Checked Then
                        strCyinderType = "B"
                    End If
                    If cParameter.RadioButton_AB.Checked Then
                        strCyinderType = "AB"
                    End If
                    cDeviceProgramButton.ChangeIO(CType(sender, Button).Name, cParameter.CurrentType, strCyinderType, cParameter.CurrentIndex)
                    CreatIO()
                    StartRefresh(cLocalElement, cSystemElement)
                End If
            End If
            If e.Button = MouseButtons.Left Then
                Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
                If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                    Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(cDeviceProgramCfg.Index)
                    If TypeOf cIOCfg.IO Is OutputIO Then
                        If cIOCfg.Reserve Then Return
                        If cIOCfg.IOTriggerType = enumIOTriggerType.Toggle Then
                            Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                            If Not bTeadMode Then
                                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, ControlUI.FormName))
                                Return
                            End If
                            Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                            If iPageNr <= 0 Then iPageNr = 1
                            Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                            Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1)
                            Dim dNewValue As Boolean = Not dOldValue
                            cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                        End If
                    End If
                End If

                If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                    Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(cDeviceProgramCfg.Index)
                    If cDeviceProgramCfg.CylinderType = "A" Then
                        If cIOCfg.ReserveA Then Return
                    Else
                        If cIOCfg.ReserveB Then Return
                    End If

                    If cIOCfg.TriggerType = enumIOTriggerType.Toggle Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        Dim iPageNr As Integer = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), New Integer() {HMI_PLC_Interface.CON_MAXIMUM_PageNumber * iPageNr})
                        Dim dOldValue As Boolean = False
                        Dim dNewValue As Boolean = False

                        Select Case cDeviceProgramCfg.CylinderType
                            Case "A"
                                dOldValue = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOA
                                dNewValue = Not dOldValue
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                            Case "B"
                                dOldValue = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOB
                                dNewValue = Not dOldValue
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                            Case "AB"
                                dOldValue = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOB
                                dNewValue = Not dOldValue
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                            Case Else
                                dOldValue = lCylinder((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1).bulDOB
                                dNewValue = Not dOldValue
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        End Select
                    End If
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
            If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(cDeviceProgramCfg.Index)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "1"), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        Dim dNewValue As Boolean = True
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                    End If
                End If
            End If

            If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(cDeviceProgramCfg.Index)
                If cDeviceProgramCfg.CylinderType = "A" Then
                    If cIOCfg.ReserveA Then Return
                Else
                    If cIOCfg.ReserveB Then Return
                End If
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "Teach"), enumExceptionType.Alarm, ControlUI.FormName))
                        Return
                    End If
                    Dim dNewValue As Boolean = True

                    Select Case cDeviceProgramCfg.CylinderType
                        Case "A"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                        Case "B"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        Case "AB"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        Case Else
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                    End Select

                End If
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(CType(sender, Button).Name)
            If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(cDeviceProgramCfg.Index)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                        Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                        If Not bTeadMode Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "Teach"), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        Dim dNewValue As Boolean = False
                        cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "]", dNewValue)
                    End If
                End If
            End If

            If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                Dim cIOCfg As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(cDeviceProgramCfg.Index)
                If cDeviceProgramCfg.CylinderType = "A" Then
                    If cIOCfg.ReserveA Then Return
                Else
                    If cIOCfg.ReserveB Then Return
                End If
                If cIOCfg.TriggerType = enumIOTriggerType.Tap Then
                    Dim bTeadMode As Boolean = cMachineManager.MachineStatus.bulTeachMode
                    If Not bTeadMode Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("Teach", "Teach"), enumExceptionType.Alarm, ControlUI.FormName))
                        Return
                    End If
                    Dim dNewValue As Boolean = False
                    Select Case cDeviceProgramCfg.CylinderType
                        Case "A"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", dNewValue)
                        Case "B"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        Case "AB"
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                        Case Else
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOA", Not dNewValue)
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex).ToString + "].bulDOB", dNewValue)
                    End Select
                End If
            End If
        End If
    End Sub
    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiComboBox_Variant"
                    HmiButton_Variant.Button.Enabled = True
                Case "HmiComboBox_Pro"
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bytHMIPosition", Byte.Parse(Byte.Parse(HmiComboBox_Pro.ComboBox.SelectedIndex) + 1))
                    HmiButton_Move.Enabled = True
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Variant"
                LoadVariant()
            Case "HmiButton_Modify"
                Modify()
            Case "HmiButton_Save"
                Save()
            Case "HmiButton_Move"
                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructIAIButton))
                Dim dNewValue As New StructIAIButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)

            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", Not dNewValue)

            Case "HmiButton_STPEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISTP", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISTP", Not dNewValue)

            Case "HmiPassFailButton1"
                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructIAIButton))
                Dim dNewValue As New StructIAIButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "HmiPassFailButton3"
                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructIAIButton))
                Dim dNewValue As New StructIAIButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
          
        End Select
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
        End Select
    End Sub

    Private Sub LoadVariant()
        Try
            LoadAction(HmiComboBox_Variant.ComboBox.Text)
            HmiButton_Variant.Button.Enabled = False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoadAction(ByVal strVariant As String)
        Try
            lLOldPosition.Clear()
            lLOldName.Clear()
            HmiDataView_Point.Rows.Clear()
            For i = 1 To 10
                Dim mTempName As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", strVariant, "Name" + i.ToString)
                Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", strVariant, "Point" + i.ToString)
                If mTempName = "" Then
                    mTempName = cLanguageManager.GetUserTextLine("IAI", "Reserved")
                End If
                If mTempValue = "" Then
                    mTempValue = i.ToString
                End If
                HmiDataView_Point.Rows.Add(i, mTempName, mTempValue)
                lLOldPosition.Add(mTempValue)
                lLOldName.Add(mTempName)
                WritePoint()
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub Modify()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value = HmiComboBox_Pro.ComboBox.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(1).Value = HmiTextBox_Name.TextBox.Text
            Dim iCnt As Integer = 0
            Dim bDisable As Boolean = False
            For Each mTemp As String In lLOldPosition
                If HmiDataView_Point.Rows(iCnt).Cells(2).Value <> mTemp Then
                    bDisable = True
                End If
            Next
            For Each mTemp As String In lLOldName
                If HmiDataView_Point.Rows(iCnt).Cells(1).Value <> mTemp Then
                    bDisable = True
                End If
            Next
            HmiButton_Save.Button.Enabled = bDisable
        End If
    End Sub
    Private Sub WritePoint()
        Dim cPoint(9) As StructIAIPoint
        Dim mTempValue As String = String.Empty
        For i = 1 To 10
            cPoint(i - 1) = New StructIAIPoint
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiComboBox_Variant.ComboBox.Text, "Name" + i.ToString)
            cPoint(i - 1).strPointName = mTempValue
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiComboBox_Variant.ComboBox.Text, "Point" + i.ToString)
            If mTempValue = "" Then mTempValue = i.ToString
            cPoint(i - 1).fdPoint = mTempValue
            If lListInitParameter.Count > 0 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)
            End If
        Next
    End Sub
    Private Sub Save()
        Try
            lLOldPosition.Clear()
            lLOldName.Clear()
            For i = 1 To HmiDataView_Point.Rows.Count
                lLOldPosition.Add(HmiDataView_Point.Rows(i - 1).Cells(2).Value)
                lLOldName.Add(HmiDataView_Point.Rows(i - 1).Cells(1).Value)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiComboBox_Variant.ComboBox.Text, "Name" + i.ToString, HmiDataView_Point.Rows(i - 1).Cells(1).Value)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiComboBox_Variant.ComboBox.Text, "Point" + i.ToString, HmiDataView_Point.Rows(i - 1).Cells(2).Value)
            Next
            WritePoint()
            HmiButton_Save.Button.Enabled = False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiComboBox_Pro.ComboBox.SelectedIndex = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value - 1
            HmiTextBox_Name.TextBox.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(1).Value
            Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
            HmiButton_Modify.Button.Enabled = True
            HmiButton_Move.Enabled = dNewValue
        End If
    End Sub


    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        Return True
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(ControlUI.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "8"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "9", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructIAI), New StructIAI)
                        iStep = iStep + 1


                    Case 4
                        TempStructIAI = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructIAI))
                        iStep = iStep + 1

                    Case 5
                        Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramButton)
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramCylinderButton)
                        Dim iCnt As Integer = 1
                        For Each element As clsDeviceProgramCfg In cDeviceProgramButton.ListIndex.Values
                            Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(iCnt)
                            If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(element.Index)
                                lListIO(iCnt).SetIndicateBackColor(lListDI1((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1))
                            End If
                            If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                                Dim cCylinderIO As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(element.Index)
                                Select Case cDeviceProgramCfg.CylinderType
                                    Case "A"
                                        If cCylinderIO.XIndex >= 1 And cCylinderIO.YIndex >= 1 Then lListIO(iCnt).SetIndicateBackColor(lCylinder((cCylinderIO.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cCylinderIO.YIndex - 1).bulDOA)
                                    Case "B"
                                        If cCylinderIO.XIndex >= 1 And cCylinderIO.YIndex >= 1 Then lListIO(iCnt).SetIndicateBackColor(lCylinder((cCylinderIO.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cCylinderIO.YIndex - 1).bulDOB)
                                    Case "AB"
                                        If cCylinderIO.XIndex >= 1 And cCylinderIO.YIndex >= 1 Then lListIO(iCnt).SetIndicateBackColor(lCylinder((cCylinderIO.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cCylinderIO.YIndex - 1).bulDOB)
                                    Case Else
                                        If cCylinderIO.XIndex >= 1 And cCylinderIO.YIndex >= 1 Then lListIO(iCnt).SetIndicateBackColor(lCylinder((cCylinderIO.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cCylinderIO.YIndex - 1).bulDOB)
                                End Select
                            End If
                            iCnt = iCnt + 1
                        Next

                        TempStructIAI = cHMIPLC.GetValue(lListInitParameter(0))

                        If TempStructIAI.bulHMIMotorEnable <> OldStructIAI.bulHMIMotorEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_MotorEnable.SetIndicateBackColor(TempStructIAI.bulHMIMotorEnable)
                                                   End Sub)
                            If TempStructIAI.bulHMIMotorEnable And TempStructIAI.bulHMISTP Then
                                mMainForm.InvokeAction(Sub()
                                                           EnableButton()
                                                       End Sub)
                            Else
                                mMainForm.InvokeAction(Sub()
                                                           DisableButton()
                                                       End Sub)
                            End If

                        End If

                        If TempStructIAI.bulHMISTP <> OldStructIAI.bulHMISTP Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_STPEnable.SetIndicateBackColor(TempStructIAI.bulHMISTP)
                                                   End Sub)
                            If TempStructIAI.bulHMIMotorEnable And TempStructIAI.bulHMISTP Then
                                mMainForm.InvokeAction(Sub()
                                                           EnableButton()
                                                       End Sub)
                            Else
                                mMainForm.InvokeAction(Sub()
                                                           DisableButton()
                                                       End Sub)
                            End If
                        End If


                        If TempStructIAI.bulPLCIAIReady <> OldStructIAI.bulPLCIAIReady Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Ready.SetIndicateBackColor(TempStructIAI.bulPLCIAIReady)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Alarm.SetIndicateErrorBackColor(TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                        End If

                        'X Home
                        If TempStructIAI.bulHMIAxisXHome.bulHMIDoAction <> OldStructIAI.bulHMIAxisXHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructIAI.bulHMIAxisXHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass <> OldStructIAI.bulHMIAxisXHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass, TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass Then
                                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructIAIButton))
                                Dim dNewValue As New StructIAIButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
                            End If
                        End If



                        'X Reset
                        If TempStructIAI.bulHMIAxisXReset.bulHMIDoAction <> OldStructIAI.bulHMIAxisXReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructIAI.bulHMIAxisXReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass <> OldStructIAI.bulHMIAxisXReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass, TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructIAIButton))
                                Dim dNewValue As New StructIAIButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
                            End If
                        End If

                        'HMI Move
                        If TempStructIAI.bulHMIMove.bulHMIDoAction <> OldStructIAI.bulHMIMove.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructIAI.bulHMIMove.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructIAI.bulHMIMove.bulPlcActionIsFail <> OldStructIAI.bulHMIMove.bulPlcActionIsFail Or TempStructIAI.bulHMIMove.bulPlcActionIsPass <> OldStructIAI.bulHMIMove.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructIAI.bulHMIMove.bulPlcActionIsPass, TempStructIAI.bulHMIMove.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructIAI.bulHMIMove.bulPlcActionIsFail Or TempStructIAI.bulHMIMove.bulPlcActionIsPass Then
                                Dim dOldValue As StructIAIButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructIAIButton))
                                Dim dNewValue As New StructIAIButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)
                            End If
                        End If

                        OldStructIAI.bulPLCIAIReady = TempStructIAI.bulPLCIAIReady
                        OldStructIAI.bulHMIMotorEnable = TempStructIAI.bulHMIMotorEnable
                        OldStructIAI.bulHMISTP = TempStructIAI.bulHMISTP

                        OldStructIAI.bulHMIAxisXHome.bulHMIDoAction = TempStructIAI.bulHMIAxisXHome.bulHMIDoAction
                        OldStructIAI.bulHMIAxisXHome.bulPlcActionIsPass = TempStructIAI.bulHMIAxisXHome.bulPlcActionIsPass
                        OldStructIAI.bulHMIAxisXHome.bulPlcActionIsFail = TempStructIAI.bulHMIAxisXHome.bulPlcActionIsFail

                        OldStructIAI.bulHMIAxisXReset.bulHMIDoAction = TempStructIAI.bulHMIAxisXReset.bulHMIDoAction
                        OldStructIAI.bulHMIAxisXReset.bulPlcActionIsPass = TempStructIAI.bulHMIAxisXReset.bulPlcActionIsPass
                        OldStructIAI.bulHMIAxisXReset.bulPlcActionIsFail = TempStructIAI.bulHMIAxisXReset.bulPlcActionIsFail


                        OldStructIAI.bulHMIMove.bulHMIDoAction = TempStructIAI.bulHMIMove.bulHMIDoAction
                        OldStructIAI.bulHMIMove.bulPlcActionIsPass = TempStructIAI.bulHMIMove.bulPlcActionIsPass
                        OldStructIAI.bulHMIMove.bulPlcActionIsFail = TempStructIAI.bulHMIMove.bulPlcActionIsFail
                        iStep = 5
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function

    Public Sub EnableButton()     
        HmiPassFailButton1.Enabled = True
        HmiPassFailButton3.Enabled = True
    End Sub

    Public Sub DisableButton()   
        HmiPassFailButton1.Enabled = False
        HmiPassFailButton3.Enabled = False
    End Sub

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StartRefresh
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StopRefresh
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cIAI.Name)
        Dim TempStructIAI As New StructIAI
        If lListInitParameter.Count >= 1 Then
            Dim cOldStructIAI As StructIAI = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructIAI))
            TempStructIAI.bytHMIPosition = cOldStructIAI.bytHMIPosition
            TempStructIAI.HMI_Point = cOldStructIAI.HMI_Point
            cHMIPLC.WriteAny(lListInitParameter(0), TempStructIAI)
        End If
        cIAI.WritePoint(cVariantManager.CurrentVariantCfg.Variant)
        Return True
    End Function
End Class

Public Class clsPointCfg
    Public Station As String
    Public Action As String
    Public MainStepIndex As Integer
    Public SubStepIndex As Integer
    Public Parameter As String
End Class