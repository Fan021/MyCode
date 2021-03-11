﻿Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.Collections.Concurrent
Imports System.Drawing.Drawing2D
Imports System.IO
Imports Kochi.HMI.MainControl.Action
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ProgramUI
    Implements IProgramUI
    Private lListPoint As New Dictionary(Of String, clsScrewPointCfg)
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
    Private cScrewXZ As clsScrewXZ
    Private cPictureManager As clsPictureManager
    Private OldStructScrewXZ As New StructScrewXZ
    Private TempStructScrewXZ As New StructScrewXZ
    Public Const FormName As String = "ScrewXZControlUI"
    Private cMachineStationCfg As clsMachineStationCfg
    Private cIniHandler As clsIniHandler
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cSystemManager As clsSystemManager
    Private lListIO As New Dictionary(Of Integer, HMIButtonWithIndicate)
    Private cDeviceCfg As clsDeviceCfg
    Private iASTProgramUI As IProgramUI
    Private isCancal As Boolean = True
    Protected cChangePage As clsChangePage
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cMachineManager As clsMachineManager
    Private g As Graphics
    Private bmp As Bitmap
    Private img As Image
    Private scaleX As Single = 0
    Private scaleY As Single = 0
    Private rectF As RectangleF
    Private cMainStepCfg As clsMainStepCfg
    Private cSubStepCfg As clsSubStepCfg
    Protected lListPosition As New Dictionary(Of Integer, String)
    Private strPostion As String = String.Empty
    Protected iParentProgramUI As IParentProgramUI
    Private cActionLibManager As clsActionLibManager

    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IProgramUI.ObjectSource
        Set(ByVal value As Object)
            cScrewXZ = value
        End Set
        Get
            Return cScrewXZ
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cScrewXZ.Name)
            cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 5)
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

    Public Function LoadData() As Boolean
        Dim mTempValue As String = String.Empty
        lListPoint.Clear()
        lListPoint.Add("Waste Box Position", New clsScrewPointCfg)
        lListPoint.Add("Waiting Position", New clsScrewPointCfg)
        Dim cPoint(lListPoint.Count - 1) As StructPoint
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            cPoint(i) = New StructPoint
            cPoint(i).strHMIName = lListPoint.Keys(i)
            cPoint(i).fdXPosition = lListPoint(lListPoint.Keys(i)).X
            cPoint(i).fdYPosition = lListPoint(lListPoint.Keys(i)).Y
            cPoint(i).fdZPosition = lListPoint(lListPoint.Keys(i)).Z
            HmiDataView_ScrewPoint.Rows.Add((i + 1).ToString, lListPoint.Keys(i), lListPoint(lListPoint.Keys(i)).X.ToString("0.00"), lListPoint(lListPoint.Keys(i)).Y.ToString("0.00"))
        Next
        Return True
    End Function
    Public Function InitControlText() As Boolean
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Step.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Step")
        HmiLabel_Step.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Speed.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Speed")
        HmiLabel_Speed.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        ' HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Variant")
        ' HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveZ.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_MoveZ")
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        ' HmiLabel_Function.Label.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiLabel_Function")
        ' HmiLabel_Function.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Teach.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_Teach")
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Move.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_Move")
        HmiButton_Move.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Screw.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_Screw")
        HmiButton_Screw.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_AST.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiPassFailButton1")
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton2.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiPassFailButton2")
        HmiPassFailButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiPassFailButton3")
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton4.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiPassFailButton4")
        HmiPassFailButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label_X.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label_Z.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabControl1.Font = New System.Drawing.Font("Calibri", 12.0!)
        TabPage1.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "TabPage1")
        TabPage1.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiButton_Cancel.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_SDUEnable1.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_SDUEnable1")
        HmiButton_SDUEnable1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_SDUEnable2.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_SDUEnable2")
        HmiButton_SDUEnable2.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_SDUCalibrate1.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_SDUCalibrate1")
        HmiButton_SDUCalibrate1.Font = New System.Drawing.Font("Calibri", 10.0!)


        HmiButton_SDUCalibrate2.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "HmiButton_SDUCalibrate2")
        HmiButton_SDUCalibrate2.Font = New System.Drawing.Font("Calibri", 10.0!)

        TabPage2.Text = cLanguageManager.GetUserTextLine("ScrewXZ", "TabPage2")
        TabPage2.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiTextBox_MoveX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MoveZ.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveZ.TextBoxReadOnly = True
        Label_X.Text = OldStructScrewXZ.fdPLCXPosition.ToString("0.00")
        Label_Z.Text = OldStructScrewXZ.fdPLCZPosition.ToString("0.00")
        RadioButton4.Checked = True
        HmiTextBox_Speed.TextBox.Text = 100
        TrackBar_Speed.Value = 100

        HmiDataView_ScrewPoint.Rows.Clear()
        HmiDataView_ScrewPoint.Columns.Clear()

        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ScrewXZ", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_ScrewPoint.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("ScrewXZ", "Name")
        PostTest_Name.Name = "PostTest_Name"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_Name)

        Dim PostTest_X As New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = cLanguageManager.GetUserTextLine("ScrewXZ", "X")
        PostTest_X.Name = "PostTest_X"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_X)

        Dim PostTest_Z As New DataGridViewTextBoxColumn
        PostTest_Z.HeaderText = cLanguageManager.GetUserTextLine("ScrewXZ", "Y")
        PostTest_Z.Name = "PostTest_Z"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_Z)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cDeviceCfg.StationID, GetType(clsHMIAST))
        HmiComboBox_AST.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_AST.ComboBox.Items.Add(element.Name.ToString)
            Next
        End If

        HmiComboBox_Pro.ComboBox.Items.Clear()
        For i = 1 To 16
            HmiComboBox_Pro.ComboBox.Items.Add(i.ToString)
        Next

        HmiButton_Teach.Button.Enabled = True
        HmiButton_Move.Enabled = False
        HmiButton_Screw.Enabled = False
        HmiComboBox_AST.ComboBox.Enabled = False
        HmiComboBox_Pro.ComboBox.Enabled = False
        HmiButton_SDUCalibrate1.Enabled = False
        HmiButton_Screw.Hide()

        DisableButton()
        LoadData()

        AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Teach.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Speed.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler RadioButton1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton4.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler ButtonXAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonXDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZDec.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_MotorEnable.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUEnable1.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUEnable2.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUCalibrate1.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUCalibrate2.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton1.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton2.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton3.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton4.Click, AddressOf Button_Click
        AddHandler HmiButton_Move.Click, AddressOf Button_Click
        AddHandler HmiButton_Screw.Click, AddressOf Button_Click
        AddHandler TrackBar_Speed.Scroll, AddressOf TrackBar_Speed_Scroll
        AddHandler HmiDataView_ScrewPoint.CellClick, AddressOf HmiDataView_ScrewPoint_CellClick
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
                    HmiTableLayoutPanel_Body_Top_Right.Controls.Add(OutputIO, i - 1, 7)
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
                Case "HmiComboBox_AST"
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".strHMIAST", HmiComboBox_AST.ComboBox.SelectedIndex.ToString, New Integer() {HmiComboBox_AST.ComboBox.SelectedIndex.ToString.Length})
                    If HmiComboBox_AST.ComboBox.SelectedIndex >= 0 And HmiComboBox_Pro.ComboBox.SelectedIndex >= 0 Then
                        HmiButton_Screw.Enabled = True
                    Else
                        HmiButton_Screw.Enabled = False
                    End If
                Case "HmiComboBox_Pro"
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(HmiComboBox_Pro.ComboBox.SelectedIndex))
                    If HmiComboBox_AST.ComboBox.SelectedIndex >= 0 And HmiComboBox_Pro.ComboBox.SelectedIndex >= 0 Then
                        HmiButton_Screw.Enabled = True
                    Else
                        HmiButton_Screw.Enabled = False
                    End If
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_MoveX", "HmiTextBox_MoveZ"
                CheckMovePostion()
            Case "HmiTextBox_Speed"
                CheckSpeed()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Teach"
                isCancal = False
                cChangePage.BackPage()

            Case "HmiButton_Cancel"
                isCancal = True
                cChangePage.BackPage()

            Case "HmiButton_Move"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)

            Case "HmiButton_Screw"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)

            Case "HmiButton_MotorEnable"
                Dim bTeadMode As Boolean = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName + ".bulTeachMode", GetType(Boolean))
                If Not bTeadMode Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "10"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", Not dNewValue)
            Case "HmiPassFailButton1"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "HmiPassFailButton2"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZHome", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
            Case "HmiPassFailButton3"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
            Case "HmiPassFailButton4"
                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZReset", GetType(StructScrewXZButton))
                Dim dNewValue As New StructScrewXZButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
            Case "HmiButton_SDUEnable1"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUEnable1", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate1", False)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUEnable1", Not dNewValue)

            Case "HmiButton_SDUEnable2"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUEnable2", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate2", False)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUEnable2", Not dNewValue)

            Case "HmiButton_SDUCalibrate1"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                Dim dSDUValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUCalibrate1", GetType(Boolean))
                HmiButton_Move.Enabled = dNewValue And dSDUValue
                ButtonXAdd.Enabled = dNewValue And dSDUValue
                ButtonXDec.Enabled = dNewValue And dSDUValue
                ButtonZAdd.Enabled = dNewValue And dSDUValue
                ButtonZDec.Enabled = dNewValue And dSDUValue
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate1", Not dSDUValue)

            Case "HmiButton_SDUCalibrate2"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                Dim dSDUValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUCalibrate2", GetType(Boolean))
                HmiButton_Move.Enabled = dNewValue And dSDUValue
                ButtonXAdd.Enabled = dNewValue And dSDUValue
                ButtonXDec.Enabled = dNewValue And dSDUValue
                ButtonZAdd.Enabled = dNewValue And dSDUValue
                ButtonZDec.Enabled = dNewValue And dSDUValue
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate2", Not dSDUValue)


        End Select
    End Sub

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "ButtonXAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXForward", dNewValue)
            Case "ButtonXDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXBackward", dNewValue)
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
        End Select
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case sender.name
            Case "ButtonXAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXForward", dNewValue)
            Case "ButtonXDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIXBackward", dNewValue)
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
        End Select
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If RadioButton1.Checked Then
            Dim fNewValue As Single = 0.1
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
            cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", False)
        End If
        If RadioButton2.Checked Then
            Dim fNewValue As Single = 1.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
            cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", False)
        End If
        If RadioButton3.Checked Then
            Dim fNewValue As Single = 10.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
            cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", False)
        End If
        If RadioButton4.Checked Then
            Dim fNewValue As Single = 0.0
            cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
            cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", True)
        End If

    End Sub

    Private Sub TrackBar_Speed_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HmiTextBox_Speed.TextBox.Text = TrackBar_Speed.Value.ToString
    End Sub

    Private Sub CheckMovePostion()
        Try
            If HmiTextBox_MoveX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "4"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(HmiTextBox_MoveX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveZ.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveZ.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "5"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZPosition", Single.Parse(HmiTextBox_MoveZ.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveZPosition", Single.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckSpeed()
        Try
            If HmiTextBox_Speed.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Speed.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "6"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                If CInt(HmiTextBox_Speed.TextBox.Text) < 0 Or CInt(HmiTextBox_Speed.TextBox.Text) > 100 Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                TrackBar_Speed.Value = CInt(HmiTextBox_Speed.TextBox.Text)
                Dim fNewValue As Int16 = CInt(HmiTextBox_Speed.TextBox.Text)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", fNewValue)
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", Int16.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub Teach()
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
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "8"), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "9", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        Dim fNewValue As Single = 0.0
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", True)
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewXZ), New StructScrewXZ)
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        iStep = iStep + 1

                    Case 4

                        TempStructScrewXZ = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructScrewXZ))
                        If TempStructScrewXZ.fdHMISpeed = "0.1" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton1.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXZ.fdHMISpeed = "1" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton2.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXZ.fdHMISpeed = "10" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton3.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXZ.bulHMIContinueEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton4.Checked = True
                                                   End Sub)
                        End If

                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_Speed.TextBox.Text = TempStructScrewXZ.fdHMISpeed.ToString("0")
                                               End Sub)
                        iStep = iStep + 1

                    Case 5
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

                        TempStructScrewXZ = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructScrewXZ.fdPLCXPosition <> OldStructScrewXZ.fdPLCXPosition Or TempStructScrewXZ.fdPLCZPosition <> OldStructScrewXZ.fdPLCZPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructScrewXZ.fdPLCXPosition.ToString("0.00")
                                                       Label_Z.Text = TempStructScrewXZ.fdPLCZPosition.ToString("0.00")
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIXForward <> OldStructScrewXZ.bulHMIXForward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonXAdd.SetIndicateBackColor(TempStructScrewXZ.bulHMIXForward)
                                                   End Sub)
                        End If
                        If TempStructScrewXZ.bulHMIXBackward <> OldStructScrewXZ.bulHMIXBackward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonXDec.SetIndicateBackColor(TempStructScrewXZ.bulHMIXBackward)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIZForward <> OldStructScrewXZ.bulHMIZForward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonZAdd.SetIndicateBackColor(TempStructScrewXZ.bulHMIZForward)
                                                   End Sub)
                        End If
                        If TempStructScrewXZ.bulHMIZBackward <> OldStructScrewXZ.bulHMIZBackward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonZDec.SetIndicateBackColor(TempStructScrewXZ.bulHMIZBackward)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIMotorEnable <> OldStructScrewXZ.bulHMIMotorEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_MotorEnable.SetIndicateBackColor(TempStructScrewXZ.bulHMIMotorEnable)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIMotorEnable Then
                                mMainForm.InvokeAction(Sub()
                                                           EnableButton()
                                                       End Sub)
                            End If
                            If Not TempStructScrewXZ.bulHMIMotorEnable Then
                                mMainForm.InvokeAction(Sub()
                                                           DisableButton()
                                                       End Sub)
                            End If
                        End If


                        If TempStructScrewXZ.bulHMIScrew.bulHMIDoAction <> OldStructScrewXZ.bulHMIScrew.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXZ.bulHMIScrew.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIScrew.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass, TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)
                            End If
                        End If

                        'X Home
                        If TempStructScrewXZ.bulHMIAxisXHome.bulHMIDoAction <> OldStructScrewXZ.bulHMIAxisXHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructScrewXZ.bulHMIAxisXHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass, TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
                            End If
                        End If


                        'Y Home
                        If TempStructScrewXZ.bulHMIAxisZHome.bulHMIDoAction <> OldStructScrewXZ.bulHMIAxisZHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton2.SetIndicateColor(TempStructScrewXZ.bulHMIAxisZHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton2.SetIndicateColor(TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass, TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZHome", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
                            End If
                        End If

                        'X Reset
                        If TempStructScrewXZ.bulHMIAxisXReset.bulHMIDoAction <> OldStructScrewXZ.bulHMIAxisXReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructScrewXZ.bulHMIAxisXReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass, TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
                            End If
                        End If

                        'Y Reset
                        If TempStructScrewXZ.bulHMIAxisZReset.bulHMIDoAction <> OldStructScrewXZ.bulHMIAxisZReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton4.SetIndicateColor(TempStructScrewXZ.bulHMIAxisZReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton4.SetIndicateColor(TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass, TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZReset", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
                            End If
                        End If

                        'HMI Move
                        If TempStructScrewXZ.bulHMIMove.bulHMIDoAction <> OldStructScrewXZ.bulHMIMove.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructScrewXZ.bulHMIMove.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIMove.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIMove.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIMove.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIMove.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructScrewXZ.bulHMIMove.bulPlcActionIsPass, TempStructScrewXZ.bulHMIMove.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIMove.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIMove.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)
                            End If
                        End If


                        'HMI Screw
                        If TempStructScrewXZ.bulHMIScrew.bulHMIDoAction <> OldStructScrewXZ.bulHMIScrew.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXZ.bulHMIScrew.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail <> OldStructScrewXZ.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass <> OldStructScrewXZ.bulHMIScrew.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass, TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXZButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXZButton))
                                Dim dNewValue As New StructScrewXZButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)
                            End If
                        End If

                        If TempStructScrewXZ.bulHMISDUEnable1 <> OldStructScrewXZ.bulHMISDUEnable1 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate1.Enabled = TempStructScrewXZ.bulHMISDUEnable1
                                                       HmiButton_SDUEnable1.SetIndicateBackColor(TempStructScrewXZ.bulHMISDUEnable1)
                                                   End Sub)

                        End If


                        If TempStructScrewXZ.bulHMISDUEnable2 <> OldStructScrewXZ.bulHMISDUEnable2 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate2.Enabled = TempStructScrewXZ.bulHMISDUEnable2
                                                       HmiButton_SDUEnable2.SetIndicateBackColor(TempStructScrewXZ.bulHMISDUEnable2)
                                                   End Sub)

                        End If

                        If TempStructScrewXZ.bulHMISDUCalibrate1 <> OldStructScrewXZ.bulHMISDUCalibrate1 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate1.SetIndicateBackColor(TempStructScrewXZ.bulHMISDUCalibrate1)
                                                   End Sub)

                        End If

                        If TempStructScrewXZ.bulHMISDUCalibrate2 <> OldStructScrewXZ.bulHMISDUCalibrate2 Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate2.SetIndicateBackColor(TempStructScrewXZ.bulHMISDUCalibrate2)
                                                   End Sub)

                        End If

                        OldStructScrewXZ.bulHMIXForward = TempStructScrewXZ.bulHMIXForward
                        OldStructScrewXZ.bulHMIXBackward = TempStructScrewXZ.bulHMIXBackward
                        OldStructScrewXZ.bulHMIZForward = TempStructScrewXZ.bulHMIZForward
                        OldStructScrewXZ.bulHMIZBackward = TempStructScrewXZ.bulHMIZBackward
                        OldStructScrewXZ.bulHMIMotorEnable = TempStructScrewXZ.bulHMIMotorEnable

                        OldStructScrewXZ.bulHMIScrew.bulHMIDoAction = TempStructScrewXZ.bulHMIScrew.bulHMIDoAction
                        OldStructScrewXZ.bulHMIScrew.bulPlcActionIsPass = TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIScrew.bulPlcActionIsFail = TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIAxisXHome.bulHMIDoAction = TempStructScrewXZ.bulHMIAxisXHome.bulHMIDoAction
                        OldStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass = TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail = TempStructScrewXZ.bulHMIAxisXHome.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIAxisXReset.bulHMIDoAction = TempStructScrewXZ.bulHMIAxisXReset.bulHMIDoAction
                        OldStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass = TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail = TempStructScrewXZ.bulHMIAxisXReset.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIAxisZHome.bulHMIDoAction = TempStructScrewXZ.bulHMIAxisZHome.bulHMIDoAction
                        OldStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass = TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail = TempStructScrewXZ.bulHMIAxisZHome.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIAxisZReset.bulHMIDoAction = TempStructScrewXZ.bulHMIAxisZReset.bulHMIDoAction
                        OldStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass = TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail = TempStructScrewXZ.bulHMIAxisZReset.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIMove.bulHMIDoAction = TempStructScrewXZ.bulHMIMove.bulHMIDoAction
                        OldStructScrewXZ.bulHMIMove.bulPlcActionIsPass = TempStructScrewXZ.bulHMIMove.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIMove.bulPlcActionIsFail = TempStructScrewXZ.bulHMIMove.bulPlcActionIsFail

                        OldStructScrewXZ.bulHMIScrew.bulHMIDoAction = TempStructScrewXZ.bulHMIScrew.bulHMIDoAction
                        OldStructScrewXZ.bulHMIScrew.bulPlcActionIsPass = TempStructScrewXZ.bulHMIScrew.bulPlcActionIsPass
                        OldStructScrewXZ.bulHMIScrew.bulPlcActionIsFail = TempStructScrewXZ.bulHMIScrew.bulPlcActionIsFail

                        OldStructScrewXZ.fdPLCXPosition = TempStructScrewXZ.fdPLCXPosition
                        OldStructScrewXZ.fdPLCZPosition = TempStructScrewXZ.fdPLCZPosition
                        OldStructScrewXZ.bulHMISDUEnable1 = TempStructScrewXZ.bulHMISDUEnable1
                        OldStructScrewXZ.bulHMISDUCalibrate1 = TempStructScrewXZ.bulHMISDUCalibrate1
                        OldStructScrewXZ.bulHMISDUEnable2 = TempStructScrewXZ.bulHMISDUEnable2
                        OldStructScrewXZ.bulHMISDUCalibrate2 = TempStructScrewXZ.bulHMISDUCalibrate2
                        iStep = 5
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(iASTProgramUI) Then iASTProgramUI.Quit(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function


    Public Sub EnableButton()
        ButtonXDec.Enabled = True
        ButtonXAdd.Enabled = True
        ButtonZAdd.Enabled = True
        ButtonZDec.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        RadioButton3.Enabled = True
        RadioButton4.Enabled = True
        TrackBar_Speed.Enabled = True
        HmiTextBox_Speed.Enabled = True
        HmiPassFailButton1.Enabled = True
        HmiPassFailButton2.Enabled = True
        HmiPassFailButton3.Enabled = True
        HmiPassFailButton4.Enabled = True
        Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
        Dim dSDUValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUCalibrate1", GetType(Boolean))
        HmiButton_Move.Enabled = dNewValue And Not dSDUValue
    End Sub

    Public Sub DisableButton()
        ButtonXDec.Enabled = False
        ButtonXAdd.Enabled = False
        ButtonZAdd.Enabled = False
        ButtonZDec.Enabled = False
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        RadioButton3.Enabled = False
        RadioButton4.Enabled = False
        TrackBar_Speed.Enabled = False
        HmiTextBox_Speed.Enabled = False
        HmiPassFailButton1.Enabled = False
        HmiPassFailButton2.Enabled = False
        HmiPassFailButton3.Enabled = False
        HmiPassFailButton4.Enabled = False
        HmiButton_Move.Enabled = False
    End Sub

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Private Sub HmiDataView_ScrewPoint_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_ScrewPoint.CurrentRow) Then Return
        If HmiDataView_ScrewPoint.CurrentRow.Index <= HmiDataView_ScrewPoint.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(2).Value
            HmiTextBox_MoveZ.Text = HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(3).Value
            Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
            HmiButton_Teach.Button.Enabled = True
            HmiButton_Move.Enabled = dNewValue
        End If
    End Sub

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

    Public ReadOnly Property Cancel As Boolean Implements IProgramUI.Cancel
        Get
            Return isCancal
        End Get
    End Property

    Public Function GetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByRef lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.GetParameter
        lListParameter.Clear()
        lListParameter.Add(Label_X.Text)
        lListParameter.Add(Label_Z.Text)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXZ", "1"), enumExceptionType.Alarm)
        End If
        Me.lListInitParameter = lListInitParameter
        HmiTextBox_MoveX.TextBox.Text = lListParameter(2)
        HmiTextBox_MoveZ.TextBox.Text = lListParameter(3)

        If lListParameter(0) <> "" Then
            HmiComboBox_AST.ComboBox.SelectedIndex = lListParameter(0) - 1
        Else
            HmiComboBox_AST.ComboBox.SelectedIndex = -1
        End If

        If lListParameter(1) <> "" Then
            HmiComboBox_Pro.ComboBox.SelectedIndex = lListParameter(1) - 1
        Else
            HmiComboBox_Pro.ComboBox.SelectedIndex = -1
        End If

        TabPage2.Controls.Clear()
        If HmiComboBox_AST.ComboBox.Text <> "" Then
            Dim cAstDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cDeviceCfg.StationID, HmiComboBox_AST.ComboBox.SelectedIndex + 1, GetType(clsHMIAST))
            If Not IsNothing(cAstDeviceCfg) Then
                If Not IsNothing(iASTProgramUI) Then iASTProgramUI.Quit(cLocalElement, cSystemElement)
                CType(cAstDeviceCfg.Source, clsHMIAST).CreateProgramUI(cLocalElement, cSystemElement)
                iASTProgramUI = CType(cAstDeviceCfg.Source, clsHMIAST).ProgramUI
                iASTProgramUI.Init(cLocalElement, cSystemElement)
                iASTProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cAstDeviceCfg.InitParameter), clsParameter.ToList(cAstDeviceCfg.ControlParameter), clsParameter.ToList(""))
                TabPage2.Controls.Add(iASTProgramUI.UI)
            End If
        End If
        SetXYZ()
        StartRefresh(cLocalElement, cSystemElement)
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
                iR = iR * scaleX * 1.5
            Else
                black_left_width = IIf(img.Width = PictureBox_Pic.Width, 0, (PictureBox_Pic.Width - img.Width * scaleY) / 2)
                black_top_height = IIf(img.Height = PictureBox_Pic.Height, 0, (PictureBox_Pic.Height - img.Height * scaleY) / 2)
                iPointX = iX * scaleY + black_left_width
                iPointY = iY * scaleY + black_top_height
                iR = iR * scaleY * 1.5
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

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cHMIPLC = cDeviceManager.GetPLCDevice
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cScrewXZ.Name)
        Dim lListPoint As New Dictionary(Of String, clsScrewPointCfg)
        Dim mTempValue As String = String.Empty
        lListPoint.Clear()
        lListPoint.Add("Waste Box Position", New clsScrewPointCfg)
        lListPoint.Add("Waiting Position", New clsScrewPointCfg)
        Dim cPoint(lListPoint.Count - 1) As StructPoint
        Dim TempStructScrewXZ As New StructScrewXZ
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            cPoint(i) = New StructPoint
            cPoint(i).strHMIName = lListPoint.Keys(i)
            cPoint(i).fdXPosition = lListPoint(lListPoint.Keys(i)).X
            cPoint(i).fdYPosition = lListPoint(lListPoint.Keys(i)).Y
            cPoint(i).fdZPosition = lListPoint(lListPoint.Keys(i)).Z
            TempStructScrewXZ.cPoint(i) = cPoint(i)
        Next

        If lListInitParameter.Count >= 1 Then
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXZ" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed")
            If mTempValue = "" Then
                mTempValue = "10"
            End If
            Dim fNewValue As Int16 = CInt(mTempValue)
            If fNewValue <= 0 Then fNewValue = 10
            TempStructScrewXZ.fdHMISpeed = fNewValue
            TempStructScrewXZ.bulHMIContinueEnable = True
            cHMIPLC.WriteAny(lListInitParameter(0), TempStructScrewXZ)
        End If


        Return True
    End Function
End Class
