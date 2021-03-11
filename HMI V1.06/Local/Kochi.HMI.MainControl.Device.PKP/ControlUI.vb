Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent
Imports System.IO
Imports System.Drawing.Drawing2D
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ControlUI
    Implements IControlUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Protected lListActionStep As New Dictionary(Of String, clsPointCfg)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cVariantManager As clsVariantManager
    Private cPictureManager As clsPictureManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Protected cLanguageManager As clsLanguageManager
    Private mMainForm As IMainUI
    Private cPKP As clsPKP
    Private OldStructPKP As New StructPKP
    Private TempStructPKP As New StructPKP
    Public Const FormName As String = "PKPControlUI"
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private lListIO As New Dictionary(Of Integer, HMIButtonWithIndicate)

    Private g As Graphics
    Private bmp As Bitmap
    Private img As Image
    Private scaleX As Single = 0
    Private scaleY As Single = 0
    Private rectF As RectangleF
    Protected lListPosition As New Dictionary(Of Integer, String)
    Private strPostion As String = String.Empty
    Private strOldID As String = ""
    Private strOldFilePath As String = String.Empty

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cPKP = value
        End Set
        Get
            Return cPKP
        End Get
    End Property
    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cPKP.Name)
            cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 8)

            InitForm()
            InitControlText()
            GetPageMode()
            CreatIO()
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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_ToleranceY")
        HmiLabel_ToleranceY.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Function.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_Function")
        HmiLabel_Function.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Variant.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_Variant")
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Teach.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_Teach")
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        '  HmiButton_Modify.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_Modify")
        '  HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Save.Text = cLanguageManager.GetUserTextLine("PKP", "HmiButton_Save")
        HmiButton_Save.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_AST.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Pro.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_SensorX.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_SensorX")
        HmiLabel_SensorX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_SensorY.Label.Text = cLanguageManager.GetUserTextLine("PKP", "HmiLabel_SensorY")
        HmiLabel_SensorY.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Variant.ComboBox.Items.Clear()
        HmiTextBox_AST.TextBoxReadOnly = True
        HmiTextBox_Pro.TextBoxReadOnly = True


        HmiTextBox_MoveX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MoveY.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceY.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        Label_X.Text = OldStructPKP.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructPKP.fdPLCYPosition.ToString("0.00")

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
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("PKP", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = cLanguageManager.GetUserTextLine("PKP", "Action")
        PostTest_Action.Name = "PostTest_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)


        Dim PostTest_X As New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = cLanguageManager.GetUserTextLine("PKP", "X")
        PostTest_X.Name = "PostTest_X"
        HmiDataView_Point.Columns.Add(PostTest_X)

        Dim PostTest_Y As New DataGridViewTextBoxColumn
        PostTest_Y.HeaderText = cLanguageManager.GetUserTextLine("PKP", "Y")
        PostTest_Y.Name = "PostTest_Y"
        HmiDataView_Point.Columns.Add(PostTest_Y)


        Dim PostTest_TX As New DataGridViewTextBoxColumn
        PostTest_TX.HeaderText = cLanguageManager.GetUserTextLine("PKP", "ToleranceX")
        PostTest_TX.Name = "PostTest_TX"
        HmiDataView_Point.Columns.Add(PostTest_TX)

        Dim PostTest_TY As New DataGridViewTextBoxColumn
        PostTest_TY.HeaderText = cLanguageManager.GetUserTextLine("PKP", "ToleranceY")
        PostTest_TY.Name = "PostTest_TY"
        HmiDataView_Point.Columns.Add(PostTest_TY)

        Dim PostTest_AST As New DataGridViewTextBoxColumn
        PostTest_AST.HeaderText = cLanguageManager.GetUserTextLine("PKP", "AST")
        PostTest_AST.Name = "PostTest_AST"
        HmiDataView_Point.Columns.Add(PostTest_AST)

        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("PKP", "Program")
        PostTest_Program.Name = "PostTest_Program"
        HmiDataView_Point.Columns.Add(PostTest_Program)

        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If

        HmiTextBox_MoveX.ValueType = GetType(Double)
        HmiTextBox_MoveY.ValueType = GetType(Double)
        HmiTextBox_ToleranceX.ValueType = GetType(Double)
        HmiTextBox_ToleranceY.ValueType = GetType(Double)
        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_ToleranceX.TextBoxReadOnly = True
        HmiTextBox_ToleranceY.TextBoxReadOnly = True

        HmiButton_Variant.Button.Enabled = False
        HmiButton_Teach.Button.Enabled = False
        'HmiButton_Modify.Button.Enabled = False
        HmiButton_Save.Button.Enabled = False
        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        ' AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        '  AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Teach.Button.Click, AddressOf Button_Click
        '  AddHandler HmiButton_Modify.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Save.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_AST.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Pro.TextBox.TextChanged, AddressOf TextBox_TextChanged
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
            For i = 1 To 8
                Dim OutputIO As HMIButtonWithIndicate
                If lListIO.ContainsKey(i) Then
                    OutputIO = lListIO(i)
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_Click
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                    RemoveHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                Else
                    OutputIO = New HMIButtonWithIndicate
                    HmiTableLayoutPanel_Body_Top_Right.Controls.Add(OutputIO, 4, i - 1)
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
                    '   Case "HmiComboBox_AST"
                    '     cHMIPLC.WriteAny(lListInitParameter(0) + ".strHMIAST", HmiComboBox_AST.ComboBox.Text, New Integer() {HmiComboBox_AST.ComboBox.Text.Length})
                    '  Case "HmiComboBox_Pro"
                    '   cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(HmiComboBox_Pro.ComboBox.Text))
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub




    Private Sub LoadAction(ByVal strVariant As String)
        Try
            HmiTextBox_MoveX.Text = ""
            HmiTextBox_ToleranceX.Text = ""
            HmiTextBox_MoveY.Text = ""
            HmiTextBox_ToleranceY.Text = ""
            HmiTextBox_AST.Text = ""
            HmiTextBox_Pro.Text = ""
            HmiButton_Teach.Button.Enabled = False

            Dim iCnt As Integer = 1
            Dim i As Integer = 0
            Dim j As Integer = 0
            lListActionStep.Clear()
            lListPosition.Clear()
            HmiDataView_Point.Rows.Clear()
            cActionManager.LoadActionCfg(strVariant)
            For Each element As String In cActionManager.GetActionListKey()
                For Each elementAction In cActionManager.GetActionCfgFromKey(element).GetStepListKey
                    i = 0
                    Dim lListMainStepCfg As List(Of clsMainStepCfg) = cActionManager.GetMainStepCfgList(element, elementAction)
                    For Each elementMainStepCfg As clsMainStepCfg In lListMainStepCfg
                        j = 0
                        For Each elementSubKey As String In elementMainStepCfg.GetSubStepListKey
                            Dim lListParameter() As String = clsParameter.ToList(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter)).ToArray
                            If lListParameter.Length < 9 Then
                                j = j + 1
                                Continue For
                            End If
                            If elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "ManualStationScrew" Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cPKP.Name).StationIndex.ToString <> lListParameter(4) Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cPKP.Name).StationID.ToString <> element Then
                                j = j + 1
                                Continue For
                            End If
                            Dim cPointCfg As New clsPointCfg
                            cPointCfg.Station = element
                            cPointCfg.Action = elementAction
                            cPointCfg.MainStepIndex = i
                            cPointCfg.SubStepIndex = j
                            cPointCfg.Parameter = elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Parameter)
                            lListActionStep.Add(iCnt.ToString, cPointCfg)
                            HmiDataView_Point.Rows.Add(iCnt.ToString,
                                                       elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.Name),
                                                       lListParameter(5),
                                                       lListParameter(7),
                                                       lListParameter(6),
                                                       lListParameter(8),
                                                       lListParameter(1),
                                                       lListParameter(4)
                                                       )
                            lListPosition.Add(elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.TotalID), lListParameter(0))
                            iCnt = iCnt + 1
                            j = j + 1
                        Next
                        i = i + 1

                    Next


                Next
            Next
            Showdefault()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Public Sub Showdefault()
        If HmiDataView_Point.Rows.Count > 0 Then HmiDataView_Point.CurrentCell = HmiDataView_Point.Rows(0).Cells(0)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value
            HmiTextBox_ToleranceX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value
            HmiTextBox_MoveY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value
            HmiTextBox_ToleranceY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value
            HmiTextBox_AST.TextBox.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(6).Value
            HmiTextBox_Pro.TextBox.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(7).Value
            Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex
                                                    )
            Dim lListParameter As List(Of String) = clsParameter.ToList(cSubStepCfg.SubStepParameter(HMISubStepKeys.Parameter))
            SetXYZ(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
            HmiButton_Teach.Button.Enabled = True
        End If

    End Sub

    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Point.CellClick
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value
            HmiTextBox_ToleranceX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value
            HmiTextBox_MoveY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value
            HmiTextBox_ToleranceY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value
            HmiTextBox_AST.TextBox.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(6).Value
            HmiTextBox_Pro.TextBox.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(7).Value
            Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                    lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex
                                                    )
            Dim lListParameter As List(Of String) = clsParameter.ToList(cSubStepCfg.SubStepParameter(HMISubStepKeys.Parameter))
            SetXYZ(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
            HmiButton_Teach.Button.Enabled = True
        End If
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

    Private Sub SetXYZ(ByVal strID As String, ByVal strFilePath As String)
        strOldID = strID
        strOldFilePath = strFilePath
        If strOldID = "" Then Return
        If File.Exists(ChangeKeyToPath(strFilePath)) Then
            img = Image.FromFile(ChangeKeyToPath(strFilePath))
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
        SetPositon(strID)

    End Sub

    Private Sub SetPositon(ByVal strID As String)
        Dim iR As Integer
        Dim iX As Integer
        Dim iY As Integer
        Dim iPointX As Integer
        Dim iPointY As Integer
        Dim strPosition As String = ""
        Dim iIndex As Integer = strID
        Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                              lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                              lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                              lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex
                              )
        lListPosition.Clear()

        Dim cMainStepCfg As clsMainStepCfg = cActionManager.GetMainStepCfgList(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station, cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID), lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action)
        For Each element As clsSubStepCfg In cMainStepCfg.SubStepList
            Dim lListParameter() As String = clsParameter.ToList(element.SubStepParameter(HMISubStepKeys.Parameter)).ToArray
            If element.SubStepParameter(HMISubStepKeys.ActionType) <> "ManualStationScrew" Then
                Continue For
            End If
            lListPosition.Add(element.SubStepParameter(HMISubStepKeys.TotalID), lListParameter(0))
        Next

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
                If scaleX < 1 Then iR = iR * scaleX * 1.5
            Else
                black_left_width = IIf(img.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img.Width * scaleY) / 2)
                black_top_height = IIf(img.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img.Height * scaleY) / 2)
                iPointX = iX * scaleY + black_left_width
                iPointY = iY * scaleY + black_top_height
                If scaleY < 1 Then iR = iR * scaleY * 1.5
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

    Private Sub ShowDefaultPicture()
        img = Nothing
        scaleX = 1
        scaleY = 1
        rectF.Width = PictureBox_Pic.Width
        rectF.Height = PictureBox_Pic.Height
        rectF.X = 0
        rectF.Y = 0
        bmp = New Bitmap(PictureBox_Pic.Width, PictureBox_Pic.Height)
        Dim graphics2 As Graphics = PictureBox_Pic.CreateGraphics()
        graphics2.DrawImage(bmp, New Point(0, 0))
        graphics2.Dispose()
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_MoveX"
                CheckMovePostion()
            Case "HmiTextBox_MoveY"
                CheckMovePostion()
            Case "HmiTextBox_ToleranceX"
                CheckMovePostion()
            Case "HmiTextBox_ToleranceY"
                CheckMovePostion()
            Case "HmiTextBox_AST"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".strHMIAST", HmiTextBox_AST.TextBox.Text, New Integer() {HmiTextBox_AST.TextBox.Text.Length})

            Case "HmiTextBox_Pro"
                If HmiTextBox_AST.TextBox.Text <> "" Then
                    If Not IsNothing(HmiDataView_Point.CurrentRow) Then
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station, HmiTextBox_AST.TextBox.Text, GetType(clsHMIAST))
                        If Not IsNothing(cDeviceCfg) Then
                            CType(cDeviceCfg.Source, clsHMIAST).WriteProgram(HmiTextBox_Pro.TextBox.Text)
                        End If
                    End If
                End If
                If HmiTextBox_Pro.TextBox.Text = "" Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(0))
                Else
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(HmiTextBox_Pro.TextBox.Text))
                End If


        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Variant"
                LoadVariant()
            Case "HmiButton_Teach"
                Teach()
            Case "HmiButton_Modify"
                Modify()
            Case "HmiButton_Save"
                Save()
        End Select
    End Sub

    Private Sub Teach()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value = Label_X.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value = Label_Y.Text
            Dim lListParameter As List(Of String) = clsParameter.ToList(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Parameter)
            lListParameter(5) = Label_X.Text
            lListParameter(7) = Label_Y.Text
            cActionManager.ChangeCurrentSubParameter(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex,
                                                     HMISubStepKeys.Parameter,
                                                     clsParameter.ToString(lListParameter))
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged
        End If
    End Sub

    Private Sub Modify()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value = HmiTextBox_MoveX.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value = HmiTextBox_ToleranceX.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(6).Value = HmiTextBox_MoveY.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(7).Value = HmiTextBox_ToleranceY.Text
            Dim lListParameter As List(Of String) = clsParameter.ToList(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Parameter)
            lListParameter(5) = HmiTextBox_MoveX.Text
            lListParameter(6) = HmiTextBox_ToleranceX.Text
            lListParameter(7) = HmiTextBox_MoveY.Text
            lListParameter(8) = HmiTextBox_ToleranceY.Text

            cActionManager.ChangeCurrentSubParameter(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex,
                                                     HMISubStepKeys.Parameter,
                                                     clsParameter.ToString(lListParameter))
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged
            '  HmiButton_Modify.Button.Enabled = False
        End If
    End Sub

    Private Sub Save()
        Try
            If cActionManager.CheckCurrentActionCfg() Then
                If cActionManager.SaveCurrentActionCfg(HmiComboBox_Variant.ComboBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "4"), enumExceptionType.Normal, ControlUI.FormName))
                Else
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "5"), enumExceptionType.Normal, ControlUI.FormName))
                End If
                HmiButton_Save.Button.Enabled = cActionManager.IsChanged
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub CheckMovePostion()
        Try
            If HmiTextBox_MoveX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "6"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(HmiTextBox_MoveX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(HmiTextBox_MoveY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(0))
            End If

            If HmiTextBox_ToleranceX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_ToleranceX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "9"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXTolerance", Single.Parse(HmiTextBox_ToleranceX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXTolerance", Single.Parse(0))
            End If
            If HmiTextBox_ToleranceY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_ToleranceY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "10"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYTolerance", Single.Parse(HmiTextBox_ToleranceY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYTolerance", Single.Parse(0))
            End If
            ' HmiButton_Modify.Button.Enabled = IIf(HmiTextBox_MoveX.TextBox.Text = "" Or HmiTextBox_MoveY.TextBox.Text = "", False, True)

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub LoadVariant()
        Try
            If HmiComboBox_Variant.ComboBox.Text = "" Then Return
            cActionManager.LoadActionCfg(HmiComboBox_Variant.ComboBox.Text)
            LoadAction(HmiComboBox_Variant.ComboBox.Text)
            HmiButton_Variant.Button.Enabled = False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("PKP", "14", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructPKP), New StructPKP)
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

                        TempStructPKP = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructPKP.fdPLCXPosition <> OldStructPKP.fdPLCXPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructPKP.fdPLCXPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        OldStructPKP.fdPLCXPosition = TempStructPKP.fdPLCXPosition
                        iStep = iStep + 1

                    Case 5
                        If TempStructPKP.fdPLCYPosition <> OldStructPKP.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_Y.Text = TempStructPKP.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If
                        If TempStructPKP.fdPLCXOriginDone <> OldStructPKP.fdPLCXOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_X.SetIndicateBackColor(TempStructPKP.fdPLCXOriginDone)
                                                   End Sub)
                        End If
                        If TempStructPKP.fdPLCYOriginDone <> OldStructPKP.fdPLCYOriginDone Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Y.SetIndicateBackColor(TempStructPKP.fdPLCYOriginDone)
                                                   End Sub)
                        End If
                        OldStructPKP.fdPLCYPosition = TempStructPKP.fdPLCYPosition
                        OldStructPKP.fdPLCXOriginDone = TempStructPKP.fdPLCXOriginDone
                        OldStructPKP.fdPLCYOriginDone = TempStructPKP.fdPLCYOriginDone
                        iStep = 4

                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        If Not IsNothing(cActionManager) AndAlso cActionManager.IsChanged Then
            cErrorMessageManager.Clean(ControlUI.FormName)
            cErrorMessageManager.RegisterSaveFunction(AddressOf SaveFunction)
            cErrorMessageManager.RegisterAbortFunction(AddressOf AbortFunction)
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ParentProgramForm.ToString, "5"), enumExceptionType.Confirm, ControlUI.FormName))
            Return False
        End If
        StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(cErrorMessageManager) Then cErrorMessageManager.DisposeAbortFunction()
        If Not IsNothing(cErrorMessageManager) Then cErrorMessageManager.DisposeSaveFunction()
        Me.Dispose()
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function


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

    Private Sub SaveFunction()
        Save()
        HmiButton_Save.Button.Enabled = cActionManager.IsChanged
    End Sub

    Private Sub AbortFunction()
        Try
            LoadVariant()
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
        End Try
    End Sub

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempStructPKP As New StructPKP
        If lListInitParameter.Count >= 1 Then cHMIPLC.WriteAny(lListInitParameter(0), TempStructPKP)
        Return True
    End Function

    Private Sub PictureBox_Pic_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox_Pic.SizeChanged
        SetXYZ(strOldID, strOldFilePath)
    End Sub
End Class

Public Class clsPointCfg
    Public Station As String
    Public Action As String
    Public MainStepIndex As Integer
    Public SubStepIndex As Integer
    Public Parameter As String
End Class