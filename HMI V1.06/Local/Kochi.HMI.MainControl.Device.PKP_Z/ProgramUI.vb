Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing
Imports Kochi.HMI.MainControl.Action
Imports System.IO
Imports System.Drawing.Drawing2D
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ProgramUI
    Implements IProgramUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cVariantManager As clsVariantManager
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Protected cChangePage As clsChangePage
    Private mMainForm As IMainUI
    Private cPKP_Z As clsPKP_Z
    Private bExit As Boolean = False
    Private cThread As Thread
    Private OldStructPKP_Z As New StructPKP_Z
    Private TempStructPKP_Z As New StructPKP_Z
    Private isCancal As Boolean = True
    Private cMachineStationCfg As clsMachineStationCfg
    Private cPictureManager As clsPictureManager
    Private cMainStepCfg As clsMainStepCfg
    Private cSubStepCfg As clsSubStepCfg
    Private cActionLibManager As clsActionLibManager
    Private g As Graphics
    Private bmp As Bitmap
    Private img As Image
    Private scaleX As Single = 0
    Private scaleY As Single = 0
    Private rectF As RectangleF
    Protected lListPosition As New Dictionary(Of Integer, String)
    Private strPostion As String = String.Empty
    Protected iParentProgramUI As IParentProgramUI
    Public Const FormName As String = "PKP_ZProgramUI"
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cSystemManager As clsSystemManager
    Private lListIO As New Dictionary(Of Integer, HMIButtonWithIndicate)
    Private iASTProgramUI As IProgramUI
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton

    Public ReadOnly Property Cancel As Boolean Implements IProgramUI.Cancel
        Get
            Return isCancal
        End Get
    End Property

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IProgramUI.ObjectSource
        Get
            Return cPKP_Z
        End Get
        Set(ByVal value As Object)
            cPKP_Z = value
        End Set
    End Property

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
            cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)

            cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)

            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cPKP_Z.Name)
            cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 0)
            InitForm()
            InitControlText()
            GetPageMode()
            '   CreatIO()

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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_MoveZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_MoveZ")
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceY")
        HmiLabel_ToleranceY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_ToleranceZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_ToleranceZ")
        HmiLabel_ToleranceZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Teach.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiButton_Teach")
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiButton_Cancel.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorX.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorX")
        HmiLabel_SensorX.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorY.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorY")
        HmiLabel_SensorY.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_SensorZ.Label.Text = cLanguageManager.GetUserTextLine("PKP_Z", "HmiLabel_SensorZ")
        HmiLabel_SensorZ.Label.Font = New System.Drawing.Font("Calibri", 12.0!)

        Label_X.Text = OldStructPKP_Z.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructPKP_Z.fdPLCYPosition.ToString("0.00")
        Label_Z.Text = OldStructPKP_Z.fdPLCZPosition.ToString("0.00")

        TabControl1.Font = New System.Drawing.Font("Calibri", 12.0!)
        TabPage1.Text = cLanguageManager.GetUserTextLine("PKP_Z", "TabPage1")
        TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!)

        TabPage2.Text = cLanguageManager.GetUserTextLine("PKP_Z", "TabPage2")
        TabPage2.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiTextBox_AST.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBoxReadOnly = True
        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_MoveZ.TextBoxReadOnly = True
        HmiTextBox_ToleranceX.TextBoxReadOnly = True
        HmiTextBox_ToleranceY.TextBoxReadOnly = True
        HmiTextBox_ToleranceZ.TextBoxReadOnly = True

        AddHandler HmiButton_Teach.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_AST.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Pro.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function


    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Debug
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub


    Private Sub CreatIO()
        Try
            For i = 1 To 6
                Dim OutputIO As HMIButtonWithIndicate
                If lListIO.ContainsKey(i) Then
                    OutputIO = lListIO(i)
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_Click
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                    RemoveHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                Else
                    OutputIO = New HMIButtonWithIndicate
                    HmiTableLayoutPanel_Body_Top_Right.Controls.Add(OutputIO, 6, i - 1)
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

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiTextBox_MoveX"
                    CheckMovePostion()

                Case "HmiTextBox_MoveY"
                    CheckMovePostion()

                Case "HmiTextBox_MoveZ"
                    CheckMovePostion()

                Case "HmiTextBox_ToleranceX"
                    CheckMovePostion()

                Case "HmiTextBox_ToleranceY"
                    CheckMovePostion()

                Case "HmiTextBox_ToleranceZ"
                    CheckMovePostion()

                Case "HmiTextBox_AST"
                    CheckMovePostion()

                Case "HmiTextBox_Pro"
                    CheckMovePostion()

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckMovePostion()
        Try
            If HmiTextBox_MoveX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "6"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(HmiTextBox_MoveX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(HmiTextBox_MoveY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveZ.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveZ.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "8"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZPosition", Single.Parse(HmiTextBox_MoveZ.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZPosition", Single.Parse(0))
            End If
            If HmiTextBox_ToleranceX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_ToleranceX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "9"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXTolerance", Single.Parse(HmiTextBox_ToleranceX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXTolerance", Single.Parse(0))
            End If
            If HmiTextBox_ToleranceY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_ToleranceY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "10"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYTolerance", Single.Parse(HmiTextBox_ToleranceY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYTolerance", Single.Parse(0))
            End If
            If HmiTextBox_ToleranceZ.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_ToleranceZ.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "11"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZTolerance", Single.Parse(HmiTextBox_ToleranceZ.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZTolerance", Single.Parse(0))
            End If

            If HmiTextBox_Pro.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Pro.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "12"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                If HmiTextBox_AST.TextBox.Text <> "" Then
                    Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiTextBox_AST.TextBox.Text, GetType(clsHMIAST))
                    If Not IsNothing(cDeviceCfg) Then
                        CType(cDeviceCfg.Source, clsHMIAST).WriteProgram(HmiTextBox_Pro.TextBox.Text)
                    End If
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(HmiTextBox_Pro.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(0))
            End If

            cHMIPLC.WriteAny(lListInitParameter(0) + ".strHMIAST", HmiTextBox_AST.TextBox.Text, New Integer() {HmiTextBox_AST.TextBox.Text.Length})
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Teach"
                isCancal = False
                cChangePage.BackPage()
            Case "HmiButton_Cancel"
                isCancal = True
                cChangePage.BackPage()
        End Select
    End Sub

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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructPKP_Z), New StructPKP_Z)
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        iStep = iStep + 1

                    Case 4
                        Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramButton)
                        Dim lCylinder() As StructDebugCylinder = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramCylinderButton)
                        Dim iCnt As Integer = 1
                        For Each element As clsDeviceProgramCfg In cDeviceProgramButton.ListIndex.Values
                            Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(iCnt)
                            If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                                Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(element.Index)
                                If cIOCfg.XIndex >= 1 And cIOCfg.YIndex >= 1 Then lListIO(iCnt).SetIndicateBackColor(lListDI1((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1))
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

                        TempStructPKP_Z = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructPKP_Z.fdPLCXPosition <> OldStructPKP_Z.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructPKP_Z.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        OldStructPKP_Z.fdPLCXPosition = TempStructPKP_Z.fdPLCXPosition
                        iStep = iStep + 1

                    Case 5
                        If TempStructPKP_Z.fdPLCYPosition <> OldStructPKP_Z.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Y.Text = TempStructPKP_Z.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        OldStructPKP_Z.fdPLCYPosition = TempStructPKP_Z.fdPLCYPosition
                        iStep = iStep + 1

                    Case 6
                        If TempStructPKP_Z.fdPLCZPosition <> OldStructPKP_Z.fdPLCZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Z.Text = TempStructPKP_Z.fdPLCZPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCXOriginDone <> OldStructPKP_Z.fdPLCXOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_X.SetIndicateBackColor(TempStructPKP_Z.fdPLCXOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCYOriginDone <> OldStructPKP_Z.fdPLCYOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Y.SetIndicateBackColor(TempStructPKP_Z.fdPLCYOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP_Z.fdPLCZOriginDone <> OldStructPKP_Z.fdPLCZOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Z.SetIndicateBackColor(TempStructPKP_Z.fdPLCZOriginDone)
                                                   End Sub)
                        End If
                        OldStructPKP_Z.fdPLCZPosition = TempStructPKP_Z.fdPLCZPosition
                        OldStructPKP_Z.fdPLCXOriginDone = TempStructPKP_Z.fdPLCXOriginDone
                        OldStructPKP_Z.fdPLCYOriginDone = TempStructPKP_Z.fdPLCYOriginDone
                        OldStructPKP_Z.fdPLCZOriginDone = TempStructPKP_Z.fdPLCZOriginDone
                        iStep = 4

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
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

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(iASTProgramUI) Then iASTProgramUI.Quit(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Private Sub SetXYZ()
        lListPosition.Clear()
        cMainStepCfg = CType(cLocalElement(clsMainStepCfg.Name), clsMainStepCfg)
        cSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        iParentProgramUI = CType(cLocalElement(enumUIName.ParentProgramForm.ToString), IParentProgramUI)

        For Each elementSubCfg As clsSubStepCfg In cMainStepCfg.SubStepList
            If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "" Then Continue For
            '  If elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then Continue For
            If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
            End If
            If TypeOf CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI Is IScrewActionUI Then
                CType(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI, IScrewActionUI).GetPicturePostion(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter)), strPostion)
                lListPosition.Add(CInt(elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), strPostion)
            End If
        Next
        Dim cPositon() As String = lListPosition(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).Split(",")


        If File.Exists(ChangeKeyToPath(iParentProgramUI.TextBox_Picture.TextBox.Text)) Then
            img = Image.FromFile(ChangeKeyToPath(iParentProgramUI.TextBox_Picture.TextBox.Text))
            scaleX = PictureBox_Pic.Width * 1.0F / img.Width
            scaleY = PictureBox_Pic.Height * 1.0F / img.Height

            rectF = New RectangleF()
            If (scaleX < scaleY) Then
                rectF.Width = img.Width * scaleX
                rectF.Height = img.Height * scaleX
                If scaleX < 1 Then
                    rectF.Width = img.Width * scaleX
                    rectF.Height = img.Height * scaleX
                Else
                    scaleX = 1
                    rectF.Width = img.Width * scaleX
                    rectF.Height = img.Height * scaleX
                End If
            Else
                If scaleY < 1 Then
                    rectF.Width = img.Width * scaleY
                    rectF.Height = img.Height * scaleY
                Else
                    scaleY = 1
                    rectF.Width = img.Width
                    rectF.Height = img.Height
                End If
            End If
            rectF.X = (PictureBox_Pic.Width - rectF.Width) / 2.0F
            rectF.Y = (PictureBox_Pic.Height - rectF.Height) / 2.0F
        Else
            img = New Bitmap(PictureBox_Pic.Width, PictureBox_Pic.Height)
            scaleX = 1
            scaleY = 1
            rectF.Width = PictureBox_Pic.Width
            rectF.Height = PictureBox_Pic.Height
            rectF.X = 0
            rectF.Y = 0
        End If
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.UpdateStyles()
        bmp = New Bitmap(PictureBox_Pic.Width, PictureBox_Pic.Height)
        g = Graphics.FromImage(bmp)
        g.Clear(Color.White)
        PictureBox_Pic.Image = bmp
        g.SmoothingMode = SmoothingMode.AntiAlias
        SetPositon()

    End Sub

    Private Sub SetPositon()
        Dim iR As Integer
        Dim iX As Integer
        Dim iY As Integer
        Dim iPointX As Integer
        Dim iPointY As Integer
        Dim strPosition As String = ""
        Dim iIndex As Integer = CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))


        g.Clear(Color.White)
        g.SmoothingMode = SmoothingMode.AntiAlias
        If Not IsNothing(bmp) Then g.DrawImage(img, rectF)

        For i = 0 To lListPosition.Count - 1
            strPosition = lListPosition(lListPosition.Keys(i))
            If strPosition = "" Then
                Continue For
            End If
            Dim cPosition() As String = strPosition.Split(",")
            If cPosition.Length < 2 Then
                iX = 0
                iY = 0
                iR = 20
            Else
                If Not IsNumeric(cPosition(2)) Or cPosition(2) = "" Then
                    iR = 20
                Else
                    iR = cPosition(2)
                End If

                If Not IsNumeric(cPosition(0)) Or cPosition(0) = "" Then
                    iX = 0
                Else
                    iX = cPosition(0)
                End If

                If Not IsNumeric(cPosition(1)) Or cPosition(1) = "" Then
                    iY = 0
                Else
                    iY = cPosition(1)
                End If
            End If

            Dim black_left_width As Integer
            Dim black_top_height As Integer

            If (scaleX < scaleY) Then
                black_left_width = IIf(img.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img.Width * scaleX) / 2)
                black_top_height = IIf(img.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img.Height * scaleX) / 2)
                iPointX = iX * scaleX + black_left_width
                iPointY = iY * scaleX + black_top_height
            Else
                black_left_width = IIf(img.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img.Width * scaleY) / 2)
                black_top_height = IIf(img.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img.Height * scaleY) / 2)
                iPointX = iX * scaleY + black_left_width
                iPointY = iY * scaleY + black_top_height
            End If

            If lListPosition.Keys(i) < iIndex Then
                g.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
            End If

            If lListPosition.Keys(i) = iIndex Then
                g.FillEllipse(Brushes.Yellow, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
            End If

            If lListPosition.Keys(i) > iIndex Then
                g.FillEllipse(Brushes.White, iPointX - iR, iPointY - iR, 2 * iR, 2 * iR)
            End If
            Dim iSize As Integer = ChangeSize(iR, (i + 1).ToString)
            Dim iH As Integer = g.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height
            Dim iW As Integer = g.MeasureString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width
            '  g.DrawString((i + 1).ToString, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold), New SolidBrush(Color.Black), New Point(iPointX - iW / 2, iPointY - iH / 2))
        Next

        Dim graphics As Graphics = PictureBox_Pic.CreateGraphics()
        graphics.DrawImage(bmp, New Point(0, 0))
        graphics.Dispose()

    End Sub

    Public Function ChangeKeyToPath(ByVal strFilePath As String) As String
        If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
            If strFilePath.IndexOf("[") >= 0 And strFilePath.IndexOf("]") >= 0 Then
                Dim strKey As String = strFilePath.Replace("[", "").Replace("]", "")
                If cPictureManager.HasPicture(strKey) Then
                    strFilePath = cPictureManager.GetPictureCfgFromName(strKey).Path
                End If
            End If
        End If
        Return strFilePath
    End Function

    Private Function ChangeSize(ByVal iR As Integer, ByVal strValue As String) As Integer
        Dim iH As Integer = 2 * iR
        Dim iW As Integer = 2 * iR
        Dim iSize As Integer = 1
        Do While g.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Width < iW And g.MeasureString(strValue, New System.Drawing.Font("Calibri", iSize, FontStyle.Bold)).Height < iH
            iSize = iSize + 1
        Loop
        iSize = iSize - 2
        If iSize < 1 Then iSize = 1
        Return iSize
    End Function


    Public Function GetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByRef lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.GetParameter
        lListParameter.Clear()
        lListParameter.Add(Label_X.Text)
        lListParameter.Add(HmiTextBox_ToleranceX.TextBox.Text)
        lListParameter.Add(Label_Y.Text)
        lListParameter.Add(HmiTextBox_ToleranceY.TextBox.Text)
        lListParameter.Add(Label_Z.Text)
        lListParameter.Add(HmiTextBox_ToleranceZ.TextBox.Text)
        Return True
    End Function


    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.SetParameter
        If lListParameter.Count < 8 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "2"), enumExceptionType.Alarm)
        End If
        Me.lListInitParameter = lListInitParameter
        HmiTextBox_AST.TextBox.Text = lListParameter(0)
        HmiTextBox_Pro.TextBox.Text = lListParameter(1)
        HmiTextBox_MoveX.TextBox.Text = lListParameter(2)
        If lListParameter(3) = "" Then
            HmiTextBox_ToleranceX.TextBox.Text = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Manual_Screw_ToleranceX)
        Else
            HmiTextBox_ToleranceX.TextBox.Text = lListParameter(3)
        End If
        HmiTextBox_MoveY.TextBox.Text = lListParameter(4)

        If lListParameter(5) = "" Then
            HmiTextBox_ToleranceY.TextBox.Text = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Manual_Screw_ToleranceY)
        Else
            HmiTextBox_ToleranceY.TextBox.Text = lListParameter(5)
        End If
        HmiTextBox_MoveZ.TextBox.Text = lListParameter(6)
        If lListParameter(7) = "" Then
            HmiTextBox_ToleranceZ.TextBox.Text = cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Manual_Screw_ToleranceZ)
        Else
            HmiTextBox_ToleranceZ.TextBox.Text = lListParameter(7)
        End If

        TabPage2.Controls.Clear()
        If HmiTextBox_AST.TextBox.Text <> "" Then
            Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiTextBox_AST.TextBox.Text, GetType(clsHMIAST))
            If Not IsNothing(cDeviceCfg) Then
                If Not IsNothing(iASTProgramUI) Then iASTProgramUI.Quit(cLocalElement, cSystemElement)
                CType(cDeviceCfg.Source, clsHMIAST).CreateProgramUI(cLocalElement, cSystemElement)
                iASTProgramUI = CType(cDeviceCfg.Source, clsHMIAST).ProgramUI
                iASTProgramUI.Init(cLocalElement, cSystemElement)
                iASTProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceCfg.InitParameter), clsParameter.ToList(cDeviceCfg.ControlParameter), clsParameter.ToList(""))
                TabPage2.Controls.Add(iASTProgramUI.UI)
            End If
        End If
        SetXYZ()
        StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempStructPKP_Z As New StructPKP_Z
        If lListInitParameter.Count >= 1 Then cHMIPLC.WriteAny(lListInitParameter(0), TempStructPKP_Z)
        Return True
    End Function
End Class