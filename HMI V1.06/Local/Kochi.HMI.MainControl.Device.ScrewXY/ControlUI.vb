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
    Private cScrewXY As clsScrewXY
    Private cPictureManager As clsPictureManager
    Private OldStructScrewXY As New StructScrewXY
    Private TempStructScrewXY As New StructScrewXY
    Public Const FormName As String = "ScrewXYControlUI"
    Private cMachineStationCfg As clsMachineStationCfg
    Private cIniHandler As clsIniHandler
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cSystemManager As clsSystemManager
    Private lListIO As New Dictionary(Of Integer, HMIButtonWithIndicate)
    Private cDeviceCfg As clsDeviceCfg
    Private lListPoint As New Dictionary(Of String, clsScrewPointCfg)
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private cMachineManager As clsMachineManager
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

    Public ReadOnly Property UI As Panel Implements IDeviceUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cScrewXY = value
        End Set
        Get
            Return cScrewXY
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
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cScrewXY.Name)
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
        HmiDataView_ScrewPoint.Rows.Clear()
        Dim mTempValue As String = String.Empty
        lListPoint.Clear()
        lListPoint.Add("Waste Box Position", New clsScrewPointCfg)
        lListPoint.Add("Waiting Position", New clsScrewPointCfg)
        Dim cPoint(lListPoint.Count - 1) As StructPoint
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
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
        If cVariantManager.CurrentVariantCfg.Variant <> "" Then
            LoadAction(cVariantManager.CurrentVariantCfg.Variant)
        End If
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Step.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Step")
        HmiLabel_Step.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Speed.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Speed")
        HmiLabel_Speed.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_AutoSpeed.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_AutoSpeed")
        HmiLabel_AutoSpeed.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Function.Label.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiLabel_Function")
        HmiLabel_Function.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Variant.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_Variant")
        HmiButton_Variant.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Teach.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_Teach")
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Move.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_Move")
        HmiButton_Move.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Screw.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_Screw")
        HmiButton_Screw.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Save.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_Save")
        HmiButton_Save.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_AST.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiPassFailButton1")
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton2.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiPassFailButton2")
        HmiPassFailButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiPassFailButton3")
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton4.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiPassFailButton4")
        HmiPassFailButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label_X.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label_Y.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label2.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton1.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton2.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton3.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiPassFailButton4.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MoveX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MoveY.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_SDUEnable.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_SDUEnable")
        HmiButton_SDUEnable.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_SDUCalibrate.Text = cLanguageManager.GetUserTextLine("ScrewXY", "HmiButton_SDUCalibrate")
        HmiButton_SDUCalibrate.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        Label_X.Text = OldStructScrewXY.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructScrewXY.fdPLCYPosition.ToString("0.00")
        RadioButton4.Checked = True
        HmiTextBox_Speed.TextBox.Text = 100
        TrackBar_Speed.Value = 100

        TabControl_Point.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage_ProPoint.Text = cLanguageManager.GetUserTextLine("ScrewXY", "TabPage_ProPoint")
        TabPage_ProPoint.Font = New System.Drawing.Font("Calibri", 10.0!)
        TabPage_Point.Text = cLanguageManager.GetUserTextLine("ScrewXY", "TabPage_Point")
        TabPage_Point.Font = New System.Drawing.Font("Calibri", 10.0!)

        'TabPage_PTP.Text = cLanguageManager.GetUserTextLine("ScrewXY", "TabPage_PTP")
        '  TabPage_PTP.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBox_AutoSpeed.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_AutoSpeed.ValueType = GetType(Integer)
        HmiTextBox_Speed.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Speed.ValueType = GetType(Integer)
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
        HmiDataView_ScrewPoint.Rows.Clear()
        HmiDataView_ScrewPoint.Columns.Clear()

        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_ScrewPoint.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "Name")
        PostTest_Name.Name = "PostTest_Name"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_Name)

        Dim PostTest_X As New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "X")
        PostTest_X.Name = "PostTest_X"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_X)

        Dim PostTest_Y As New DataGridViewTextBoxColumn
        PostTest_Y.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "Y")
        PostTest_Y.Name = "PostTest_Y"
        HmiDataView_ScrewPoint.Columns.Add(PostTest_Y)

        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)


        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "Action")
        PostTest_Action.Name = "PostTest_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)


        PostTest_X = New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "X")
        PostTest_X.Name = "PostTest_X"
        HmiDataView_Point.Columns.Add(PostTest_X)

        PostTest_Y = New DataGridViewTextBoxColumn
        PostTest_Y.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "Y")
        PostTest_Y.Name = "PostTest_Y"
        HmiDataView_Point.Columns.Add(PostTest_Y)

        Dim PostTest_AST As New DataGridViewTextBoxColumn
        PostTest_AST.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "AST")
        PostTest_AST.Name = "PostTest_AST"
        HmiDataView_Point.Columns.Add(PostTest_AST)

        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("ScrewXY", "Program")
        PostTest_Program.Name = "PostTest_Program"
        HmiDataView_Point.Columns.Add(PostTest_Program)

        HmiTextBox_MoveX.ValueType = GetType(Double)
        HmiTextBox_MoveY.ValueType = GetType(Double)



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

        HmiButton_Variant.Button.Enabled = False
        HmiButton_Teach.Button.Enabled = True
        HmiButton_Move.Enabled = False
        HmiButton_Screw.Enabled = False
        HmiButton_Save.Button.Enabled = False
        HmiButton_SDUCalibrate.Enabled = False
        HmiButton_Screw.Hide()

        DisableButton()

        AddHandler HmiComboBox_Variant.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiButton_Variant.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Teach.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Save.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Speed.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_AutoSpeed.TextBox.TextChanged, AddressOf TextBox_TextChanged

        AddHandler RadioButton1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton4.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler ButtonXAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonXDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYDec.MouseUp, AddressOf Button_MouseUp
        AddHandler HmiButton_MotorEnable.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton1.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton2.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton3.Click, AddressOf Button_Click
        AddHandler HmiPassFailButton4.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUEnable.Click, AddressOf Button_Click
        AddHandler HmiButton_SDUCalibrate.Click, AddressOf Button_Click
        AddHandler HmiButton_Move.Click, AddressOf Button_Click
        AddHandler HmiButton_Screw.Click, AddressOf Button_Click
        AddHandler TrackBar_Speed.Scroll, AddressOf TrackBar_Speed_Scroll
        AddHandler HmiDataView_Point.CellClick, AddressOf HmiDataView_Point_CellClick
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
                Case "HmiComboBox_Variant"
                    HmiButton_Variant.Button.Enabled = True
                Case "HmiComboBox_AST"
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".strHMIAST", HmiComboBox_AST.ComboBox.Text, New Integer() {HmiComboBox_AST.ComboBox.Text.Length})
                    'HmiButton_Screw.Button.Enabled = IIf(HmiComboBox_AST.ComboBox.SelectedIndex < 0 Or HmiComboBox_Pro.ComboBox.SelectedIndex < 0, False, True)
                Case "HmiComboBox_Pro"
                    Dim mTemp As String = HmiComboBox_Pro.ComboBox.Text
                    If mTemp = "" Then
                        mTemp = "0"
                    End If
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIProg", Int16.Parse(mTemp))
                    '  HmiButton_Screw.Button.Enabled = IIf(HmiComboBox_AST.ComboBox.SelectedIndex < 0 Or HmiComboBox_Pro.ComboBox.SelectedIndex < 0, False, True)
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
            Case "HmiTextBox_MoveX", "HmiTextBox_MoveY"
                CheckMovePostion()
            Case "HmiTextBox_Speed"
                CheckSpeed()
                '  cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_AutoSpeed"
                CheckAutoSpeed()
                '  cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Variant"
                LoadVariant()
            Case "HmiButton_Teach"
                If TabControl_Point.SelectedTab.Name = "TabPage_ProPoint" Then
                    Teach()
                Else
                    TeachScrew()
                End If

            Case "HmiButton_Modify"
                Modify()
            Case "HmiButton_Save"
                Save()

            Case "HmiButton_Move"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)

            Case "HmiButton_Screw"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)

            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", Not dNewValue)
            Case "HmiPassFailButton1"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "HmiPassFailButton2"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYHome", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)
            Case "HmiPassFailButton3"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
            Case "HmiPassFailButton4"
                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYReset", GetType(StructScrewXYButton))
                Dim dNewValue As New StructScrewXYButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)

            Case "HmiButton_SDUEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate", False)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUEnable", Not dNewValue)

            Case "HmiButton_SDUCalibrate"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                Dim dSDUValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMISDUCalibrate", GetType(Boolean))
                HmiButton_Move.Enabled = dNewValue And dSDUValue
                ButtonXAdd.Enabled = dNewValue And dSDUValue
                ButtonXDec.Enabled = dNewValue And dSDUValue
                ButtonYAdd.Enabled = dNewValue And dSDUValue
                ButtonYDec.Enabled = dNewValue And dSDUValue
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMISDUCalibrate", Not dSDUValue)
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
            Case "ButtonYAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYForward", dNewValue)
            Case "ButtonYDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYBackward", dNewValue)
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
            Case "ButtonYAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYForward", dNewValue)
            Case "ButtonYDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIYBackward", dNewValue)
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
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "4"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(HmiTextBox_MoveX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "5"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(HmiTextBox_MoveY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckSpeed()
        Try
            If HmiTextBox_Speed.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Speed.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "6"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                If CInt(HmiTextBox_Speed.TextBox.Text) < 0 Or CInt(HmiTextBox_Speed.TextBox.Text) > 100 Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                TrackBar_Speed.Value = CInt(HmiTextBox_Speed.TextBox.Text)
                Dim fNewValue As Int16 = CInt(HmiTextBox_Speed.TextBox.Text)
                If fNewValue > 100 Then
                    HmiTextBox_Speed.TextBox.Text = "100"
                End If
                fNewValue = CInt(HmiTextBox_Speed.TextBox.Text)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", fNewValue)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed", fNewValue.ToString)
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", Int16.Parse(0))
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed", "0")
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckAutoSpeed()
        Try
            If HmiTextBox_AutoSpeed.TextBox.Text = "" Then
                HmiTextBox_AutoSpeed.TextBox.Text = "0"
            End If
            If HmiTextBox_AutoSpeed.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_AutoSpeed.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "6"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                If CInt(HmiTextBox_AutoSpeed.TextBox.Text) < 0 Or CInt(HmiTextBox_AutoSpeed.TextBox.Text) > 100 Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                Dim fNewValue As Int16 = CInt(HmiTextBox_AutoSpeed.TextBox.Text)
                If fNewValue > 100 Then
                    HmiTextBox_AutoSpeed.TextBox.Text = "100"
                End If
                fNewValue = CInt(HmiTextBox_AutoSpeed.TextBox.Text)
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutoSpeed", fNewValue)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_AutoSpeed", fNewValue.ToString)
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutoSpeed", Int16.Parse(0))
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_AutoSpeed", "0")
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub LoadVariant()
        Try
            cActionManager.LoadActionCfg(HmiComboBox_Variant.ComboBox.Text)
            LoadAction(HmiComboBox_Variant.ComboBox.Text)

            HmiButton_Variant.Button.Enabled = False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub
    Private Sub LoadAction(ByVal strVariant As String)
        Try
            HmiTextBox_MoveX.Text = ""
            HmiTextBox_MoveY.Text = ""
            HmiComboBox_AST.ComboBox.SelectedIndex = -1
            HmiComboBox_Pro.ComboBox.SelectedIndex = -1
            HmiButton_Teach.Button.Enabled = True
            HmiButton_Screw.Enabled = False
            HmiButton_Move.Enabled = False

            lListPosition.Clear()
            Dim iCnt As Integer = 1
            Dim i As Integer = 0
            Dim j As Integer = 0
            lListActionStep.Clear()
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
                            If lListParameter.Length < 6 Then
                                j = j + 1
                                Continue For
                            End If
                            If elementMainStepCfg.GetSubStepCfgFromKey(elementSubKey).SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationScrew" Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cScrewXY.Name).StationIndex.ToString <> lListParameter(3) Then
                                j = j + 1
                                Continue For
                            End If
                            If cDeviceManager.GetDeviceFromName(cScrewXY.Name).StationID.ToString <> element Then
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
                                                       lListParameter(4),
                                                       lListParameter(5),
                                                       lListParameter(1),
                                                       lListParameter(2))
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

    Private Sub Teach()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value = Label_X.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value = Label_Y.Text
            Dim lListParameter As List(Of String) = clsParameter.ToList(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Parameter)
            lListParameter(4) = Label_X.Text
            lListParameter(5) = Label_Y.Text
            HmiTextBox_MoveX.TextBox.Text = Label_X.Text
            HmiTextBox_MoveY.TextBox.Text = Label_Y.Text
            cActionManager.ChangeCurrentSubParameter(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex,
                                                     HMISubStepKeys.Parameter,
                                                     clsParameter.ToString(lListParameter))
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged Or isChanged()
        End If
    End Sub

    Private Sub TeachScrew()
        If IsNothing(HmiDataView_ScrewPoint.CurrentRow) Then Return
        If HmiDataView_ScrewPoint.CurrentRow.Index <= HmiDataView_ScrewPoint.Rows.Count - 1 Then
            HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(2).Value = Label_X.Text
            HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(3).Value = Label_Y.Text
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged Or isChanged()
        End If
    End Sub

    Private Function isChanged() As Boolean
        For i = 0 To HmiDataView_ScrewPoint.RowCount - 1
            If lListPoint(HmiDataView_ScrewPoint.Rows(i).Cells(1).Value).X <> Single.Parse(HmiDataView_ScrewPoint.Rows(i).Cells(2).Value) Then
                Return True
            End If
            If lListPoint(HmiDataView_ScrewPoint.Rows(i).Cells(1).Value).Y <> Single.Parse(HmiDataView_ScrewPoint.Rows(i).Cells(3).Value) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Modify()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value = HmiTextBox_MoveX.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value = HmiTextBox_MoveY.Text
            Dim lListParameter As List(Of String) = clsParameter.ToList(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Parameter)
            lListParameter(4) = HmiTextBox_MoveX.Text
            lListParameter(5) = HmiTextBox_MoveY.Text
            cActionManager.ChangeCurrentSubParameter(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex,
                                                     HMISubStepKeys.Parameter,
                                                     clsParameter.ToString(lListParameter))
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged
        End If
    End Sub

    Private Sub Save()
        Try
            If cActionManager.CheckCurrentActionCfg() Then
                If cActionManager.SaveCurrentActionCfg(HmiComboBox_Variant.ComboBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "11"), enumExceptionType.Normal, ControlUI.FormName))
                Else
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "12"), enumExceptionType.Normal, ControlUI.FormName))
                End If
            End If

            For i = 0 To HmiDataView_ScrewPoint.RowCount - 1
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiDataView_ScrewPoint.Rows(i).Cells(1).Value, "X", HmiDataView_ScrewPoint.Rows(i).Cells(2).Value)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiDataView_ScrewPoint.Rows(i).Cells(1).Value, "Y", HmiDataView_ScrewPoint.Rows(i).Cells(3).Value)
            Next
            Dim mTempValue As String = String.Empty
            For i = 0 To lListPoint.Count - 1
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).X = 0
                Else
                    lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Y = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Z = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
                End If
            Next
            Dim cPoint(lListPoint.Count - 1) As StructPoint
            For i = 0 To lListPoint.Count - 1
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).X = 0
                Else
                    lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Y = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
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
            Next
            cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged Or isChanged()

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub HmiDataView_ScrewPoint_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_ScrewPoint.CurrentRow) Then Return
        If HmiDataView_ScrewPoint.CurrentRow.Index <= HmiDataView_ScrewPoint.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(2).Value
            HmiTextBox_MoveY.Text = HmiDataView_ScrewPoint.Rows(HmiDataView_ScrewPoint.CurrentRow.Index).Cells(3).Value
            ShowDefaultPicture()
            Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
            HmiButton_Teach.Button.Enabled = True
            HmiButton_Move.Enabled = dNewValue
        End If
    End Sub

    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value
            HmiTextBox_MoveY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value
            If HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value <> "" Then
                HmiComboBox_AST.ComboBox.SelectedIndex = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value - 1
            Else
                HmiComboBox_AST.ComboBox.SelectedIndex = -1
            End If

            If HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value <> "" Then
                HmiComboBox_Pro.ComboBox.SelectedIndex = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value - 1
            Else
                HmiComboBox_Pro.ComboBox.SelectedIndex = -1
            End If

            Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex
                                     )
            Dim lListParameter As List(Of String) = clsParameter.ToList(cSubStepCfg.SubStepParameter(HMISubStepKeys.Parameter))
            SetXYZ(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
            Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
            HmiButton_Teach.Button.Enabled = True
            HmiButton_Move.Enabled = dNewValue
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
            If element.SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationScrew" Then
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

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        LoadData()
        TempStructScrewXY = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructScrewXY))
        HmiTextBox_Speed.TextBox.Text = TempStructScrewXY.fdHMISpeed
        HmiTextBox_AutoSpeed.TextBox.Text = TempStructScrewXY.fdHMIAutoSpeed
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
                        Dim fNewValue As Single = 0.0
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", True)
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructScrewXY), New StructScrewXY)
                        Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                        iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                        If iPageNr <= 0 Then iPageNr = 1
                        Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                        cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})
                        iStep = iStep + 1


                    Case 4
                        TempStructScrewXY = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructScrewXY))
                        If TempStructScrewXY.fdHMISpeed = "0.1" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton1.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMISpeed = "1" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton2.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXY.fdHMISpeed = "10" Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton3.Checked = True
                                                   End Sub)
                        End If
                        If TempStructScrewXY.bulHMIContinueEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       RadioButton4.Checked = True
                                                   End Sub)
                        End If

                        '  mMainForm.InvokeAction(Sub()
                        '  HmiTextBox_Speed.TextBox.Text = TempStructScrewXY.fdHMISpeed.ToString("0")
                        '    HmiTextBox_AutoSpeed.TextBox.Text = TempStructScrewXY.fdHMIAutoSpeed.ToString("0")
                        '  End Sub)
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

                        TempStructScrewXY = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructScrewXY.fdPLCXPosition <> OldStructScrewXY.fdPLCXPosition Or TempStructScrewXY.fdPLCYPosition <> OldStructScrewXY.fdPLCYPosition Then
                            mMainForm.InvokeAction(Sub()
                                                       Label_X.Text = TempStructScrewXY.fdPLCXPosition.ToString("0.00")
                                                       Label_Y.Text = TempStructScrewXY.fdPLCYPosition.ToString("0.00")
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIXForward <> OldStructScrewXY.bulHMIXForward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonXAdd.SetIndicateBackColor(TempStructScrewXY.bulHMIXForward)
                                                   End Sub)
                        End If
                        If TempStructScrewXY.bulHMIXBackward <> OldStructScrewXY.bulHMIXBackward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonXDec.SetIndicateBackColor(TempStructScrewXY.bulHMIXBackward)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIYForward <> OldStructScrewXY.bulHMIYForward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonYAdd.SetIndicateBackColor(TempStructScrewXY.bulHMIYForward)
                                                   End Sub)
                        End If
                        If TempStructScrewXY.bulHMIYBackward <> OldStructScrewXY.bulHMIYBackward Then
                            mMainForm.InvokeAction(Sub()
                                                       ButtonYDec.SetIndicateBackColor(TempStructScrewXY.bulHMIYBackward)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIMotorEnable <> OldStructScrewXY.bulHMIMotorEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_MotorEnable.SetIndicateBackColor(TempStructScrewXY.bulHMIMotorEnable)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIMotorEnable Then
                                mMainForm.InvokeAction(Sub()
                                                           EnableButton()
                                                       End Sub)
                            End If
                            If Not TempStructScrewXY.bulHMIMotorEnable Then
                                mMainForm.InvokeAction(Sub()
                                                           DisableButton()
                                                       End Sub)
                            End If
                        End If


                        If TempStructScrewXY.bulHMIScrew.bulHMIDoAction <> OldStructScrewXY.bulHMIScrew.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXY.bulHMIScrew.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail <> OldStructScrewXY.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass <> OldStructScrewXY.bulHMIScrew.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass, TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)
                            End If
                        End If

                        'X Home
                        If TempStructScrewXY.bulHMIAxisXHome.bulHMIDoAction <> OldStructScrewXY.bulHMIAxisXHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructScrewXY.bulHMIAxisXHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail <> OldStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass <> OldStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton1.SetIndicateColor(TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass, TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
                            End If
                        End If


                        'Y Home
                        If TempStructScrewXY.bulHMIAxisYHome.bulHMIDoAction <> OldStructScrewXY.bulHMIAxisYHome.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton2.SetIndicateColor(TempStructScrewXY.bulHMIAxisYHome.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail <> OldStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass <> OldStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton2.SetIndicateColor(TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass, TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYHome", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)
                            End If
                        End If

                        'X Reset
                        If TempStructScrewXY.bulHMIAxisXReset.bulHMIDoAction <> OldStructScrewXY.bulHMIAxisXReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructScrewXY.bulHMIAxisXReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass <> OldStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton3.SetIndicateColor(TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass, TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
                            End If
                        End If

                        'Y Reset
                        If TempStructScrewXY.bulHMIAxisYReset.bulHMIDoAction <> OldStructScrewXY.bulHMIAxisYReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton4.SetIndicateColor(TempStructScrewXY.bulHMIAxisYReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail <> OldStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass <> OldStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiPassFailButton4.SetIndicateColor(TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass, TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail Or TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYReset", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)
                            End If
                        End If

                        'HMI Move
                        If TempStructScrewXY.bulHMIMove.bulHMIDoAction <> OldStructScrewXY.bulHMIMove.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructScrewXY.bulHMIMove.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIMove.bulPlcActionIsFail <> OldStructScrewXY.bulHMIMove.bulPlcActionIsFail Or TempStructScrewXY.bulHMIMove.bulPlcActionIsPass <> OldStructScrewXY.bulHMIMove.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Move.SetIndicateColor(TempStructScrewXY.bulHMIMove.bulPlcActionIsPass, TempStructScrewXY.bulHMIMove.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIMove.bulPlcActionIsFail Or TempStructScrewXY.bulHMIMove.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMove", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMove", dNewValue)
                            End If
                        End If


                        'HMI Screw
                        If TempStructScrewXY.bulHMIScrew.bulHMIDoAction <> OldStructScrewXY.bulHMIScrew.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXY.bulHMIScrew.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail <> OldStructScrewXY.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass <> OldStructScrewXY.bulHMIScrew.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_Screw.SetIndicateColor(TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass, TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail Or TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass Then
                                Dim dOldValue As StructScrewXYButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIScrew", GetType(StructScrewXYButton))
                                Dim dNewValue As New StructScrewXYButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIScrew", dNewValue)
                            End If
                        End If


                        If TempStructScrewXY.bulHMISDUEnable <> OldStructScrewXY.bulHMISDUEnable Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate.Enabled = TempStructScrewXY.bulHMISDUEnable
                                                       HmiButton_SDUEnable.SetIndicateBackColor(TempStructScrewXY.bulHMISDUEnable)
                                                   End Sub)

                        End If

                        If TempStructScrewXY.bulHMISDUCalibrate <> OldStructScrewXY.bulHMISDUCalibrate Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButton_SDUCalibrate.SetIndicateBackColor(TempStructScrewXY.bulHMISDUCalibrate)
                                                   End Sub)

                        End If

                        OldStructScrewXY.bulHMIXForward = TempStructScrewXY.bulHMIXForward
                        OldStructScrewXY.bulHMIXBackward = TempStructScrewXY.bulHMIXBackward
                        OldStructScrewXY.bulHMIYForward = TempStructScrewXY.bulHMIYForward
                        OldStructScrewXY.bulHMIYBackward = TempStructScrewXY.bulHMIYBackward
                        OldStructScrewXY.bulHMIMotorEnable = TempStructScrewXY.bulHMIMotorEnable

                        OldStructScrewXY.bulHMIScrew.bulHMIDoAction = TempStructScrewXY.bulHMIScrew.bulHMIDoAction
                        OldStructScrewXY.bulHMIScrew.bulPlcActionIsPass = TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIScrew.bulPlcActionIsFail = TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIAxisXHome.bulHMIDoAction = TempStructScrewXY.bulHMIAxisXHome.bulHMIDoAction
                        OldStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass = TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail = TempStructScrewXY.bulHMIAxisXHome.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIAxisXReset.bulHMIDoAction = TempStructScrewXY.bulHMIAxisXReset.bulHMIDoAction
                        OldStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass = TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail = TempStructScrewXY.bulHMIAxisXReset.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIAxisYHome.bulHMIDoAction = TempStructScrewXY.bulHMIAxisYHome.bulHMIDoAction
                        OldStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass = TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail = TempStructScrewXY.bulHMIAxisYHome.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIAxisYReset.bulHMIDoAction = TempStructScrewXY.bulHMIAxisYReset.bulHMIDoAction
                        OldStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass = TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail = TempStructScrewXY.bulHMIAxisYReset.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIMove.bulHMIDoAction = TempStructScrewXY.bulHMIMove.bulHMIDoAction
                        OldStructScrewXY.bulHMIMove.bulPlcActionIsPass = TempStructScrewXY.bulHMIMove.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIMove.bulPlcActionIsFail = TempStructScrewXY.bulHMIMove.bulPlcActionIsFail

                        OldStructScrewXY.bulHMIScrew.bulHMIDoAction = TempStructScrewXY.bulHMIScrew.bulHMIDoAction
                        OldStructScrewXY.bulHMIScrew.bulPlcActionIsPass = TempStructScrewXY.bulHMIScrew.bulPlcActionIsPass
                        OldStructScrewXY.bulHMIScrew.bulPlcActionIsFail = TempStructScrewXY.bulHMIScrew.bulPlcActionIsFail

                        OldStructScrewXY.fdPLCXPosition = TempStructScrewXY.fdPLCXPosition
                        OldStructScrewXY.fdPLCYPosition = TempStructScrewXY.fdPLCYPosition
                        OldStructScrewXY.bulHMISDUEnable = TempStructScrewXY.bulHMISDUEnable
                        OldStructScrewXY.bulHMISDUCalibrate = TempStructScrewXY.bulHMISDUCalibrate
                        iStep = 5
                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
            End Try


        End While

    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        If Not IsNothing(cActionManager) AndAlso cActionManager.IsChanged Or isChanged() Then
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

    Private Sub SaveFunction()
        Save()
        HmiButton_Save.Button.Enabled = cActionManager.IsChanged
    End Sub

    Private Sub AbortFunction()
        Try
            LoadData()
            HmiButton_Save.Button.Enabled = cActionManager.IsChanged
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentProgramForm.ToString))
        End Try
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function

    Public Sub EnableButton()
        ButtonXDec.Enabled = True
        ButtonXAdd.Enabled = True
        ButtonYAdd.Enabled = True
        ButtonYDec.Enabled = True
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
        If Not IsNothing(HmiDataView_Point.CurrentRow) AndAlso HmiDataView_Point.CurrentRow.Index >= 0 Then HmiButton_Move.Enabled = dNewValue
    End Sub

    Public Sub Showdefault()
        If HmiDataView_Point.Rows.Count > 0 Then HmiDataView_Point.CurrentCell = HmiDataView_Point.Rows(0).Cells(0)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value
            HmiTextBox_MoveY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value
            If HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value <> "" Then
                HmiComboBox_AST.ComboBox.SelectedIndex = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value - 1
            Else
                HmiComboBox_AST.ComboBox.SelectedIndex = -1
            End If

            If HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value <> "" Then
                HmiComboBox_Pro.ComboBox.SelectedIndex = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(5).Value - 1
            Else
                HmiComboBox_Pro.ComboBox.SelectedIndex = -1
            End If

            Dim cSubStepCfg As clsSubStepCfg = cActionManager.GetCurrentSubStepFromIndex(lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Station,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).Action,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).MainStepIndex,
                                     lListActionStep(HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(0).Value).SubStepIndex
                                     )
            Dim lListParameter As List(Of String) = clsParameter.ToList(cSubStepCfg.SubStepParameter(HMISubStepKeys.Parameter))
            SetXYZ(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
        End If
    End Sub
    Public Sub DisableButton()
        ButtonXDec.Enabled = False
        ButtonXAdd.Enabled = False
        ButtonYAdd.Enabled = False
        ButtonYDec.Enabled = False
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
        cHMIPLC = cDeviceManager.GetPLCDevice
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cScrewXY.Name)

        Dim lListPoint As New Dictionary(Of String, clsScrewPointCfg)
        Dim mTempValue As String = String.Empty
        lListPoint.Clear()
        lListPoint.Add("Waste Box Position", New clsScrewPointCfg)
        lListPoint.Add("Waiting Position", New clsScrewPointCfg)
        Dim cPoint(lListPoint.Count - 1) As StructPoint
        Dim TempStructScrewXY As New StructScrewXY
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
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
            TempStructScrewXY.cPoint(i) = cPoint(i)
        Next
        If lListInitParameter.Count >= 1 Then
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_AutoSpeed")
            If mTempValue = "" Then
                mTempValue = "10"
            End If
            Dim fNewValue As Int16 = CInt(mTempValue)
            If fNewValue <= 0 Then fNewValue = 10
            If fNewValue > 100 Then fNewValue = 100
            TempStructScrewXY.fdHMIAutoSpeed = fNewValue

            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed")
            If mTempValue = "" Then
                mTempValue = "10"
            End If
            fNewValue = CInt(mTempValue)
            If fNewValue <= 0 Then fNewValue = 10
            If fNewValue > 100 Then fNewValue = 100
            TempStructScrewXY.fdHMISpeed = fNewValue
            TempStructScrewXY.bulHMIContinueEnable = True
            cHMIPLC.WriteAny(lListInitParameter(0), TempStructScrewXY)
        End If

        Return True
    End Function

    Private Sub HmiDataView_Point_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles HmiDataView_Point.ColumnHeaderMouseClick
        HmiDataView_Point.Sort(HmiDataView_Point.Columns(0), System.ComponentModel.ListSortDirection.Descending)
    End Sub

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


Public Class clsScrewPointCfg
    Public X As Single
    Public Y As Single
    Public Z As Single
End Class