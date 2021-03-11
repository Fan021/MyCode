Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent

Public Class NCI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private lListPoint As New Dictionary(Of String, clsPointCfg)
    Private lListIO As New Dictionary(Of String, clsMFunctionCfg)
    Private lListWeightUI As New List(Of WeightUI)
    Private cVariantManager As clsVariantManager
    Protected cLanguageManager As clsLanguageManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private cActionManager As clsActionManager
    Private mMainForm As IMainUI
    Private cIniHandler As clsIniHandler
    Private cGapFiller As clsGapFiller
    Private cSystemManager As clsSystemManager
    Private OldStructGapFiller As New StructGapFiller
    Private TempStructGapFiller As New StructGapFiller
    Public Const FormName As String = "GapFillerControlUI"
    Private iStep As Integer = 1
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private cDeviceCfg As clsDeviceCfg
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cDeviceProgramButton As clsDeviceProgramButton
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private lListProgramIO As New Dictionary(Of Integer, HMIButtonWithIndicate)
    Private cMachineManager As clsMachineManager
    Public Property [ReadOnly] As Boolean
        Set(ByVal value As Boolean)
            bReadOnly = value
        End Set
        Get
            Return bReadOnly
        End Get
    End Property

    Public Property FontSize As Integer
        Set(ByVal value As Integer)
            iFontSize = value
        End Set
        Get
            Return iFontSize
        End Get
    End Property

    Public Property ObjectSource As Object
        Set(ByVal value As Object)
            cGapFiller = value
        End Set
        Get
            Return cGapFiller
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDeviceProgramButton = New clsDeviceProgramButton
            cDeviceProgramButton.Init(cSystemElement)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cGapFiller.Name)
            cDeviceProgramButton.LoadData(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", 0)
            InitForm()
            InitControlText()
            GetPageMode()
            '    CreatIO()
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
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Step.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Step")
        HmiLabel_Step.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Speed.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_Speed")
        HmiLabel_Speed.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_MoveX.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_MoveX")
        HmiLabel_MoveX.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_MoveY.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_MoveY")
        HmiLabel_MoveY.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_MoveZ.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel_MoveZ")
        HmiLabel_MoveZ.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Teach.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Teach")
        HmiButton_Teach.Button.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButton_Save.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Save")
        HmiButton_Save.Button.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButton_Move.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Move")
        HmiButton_Move.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Speed.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_MotorEnable.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_MotorEnable")
        HmiButton_MotorEnable.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButton_Needle.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Needle")
        HmiButton_Needle.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButton_AutoRefer.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_AutoRefer")
        HmiButton_AutoRefer.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButton_Filling.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Filling")
        HmiButton_Filling.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButton_Release.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButton_Release")
        HmiButton_Release.Font = New System.Drawing.Font("Calibri", iFontSize - 2)


        InputIO1.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO1")
        InputIO2.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO2")
        InputIO3.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO3")
        InputIO4.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO4")
        InputIO5.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO5")
        InputIO6.Text = cLanguageManager.GetUserTextLine("GapFiller", "InputIO6")

        InputIO1.Font = New System.Drawing.Font("Calibri", iFontSize)
        InputIO2.Font = New System.Drawing.Font("Calibri", iFontSize)
        InputIO3.Font = New System.Drawing.Font("Calibri", iFontSize)
        InputIO4.Font = New System.Drawing.Font("Calibri", iFontSize)
        InputIO5.Font = New System.Drawing.Font("Calibri", iFontSize)
        InputIO6.Font = New System.Drawing.Font("Calibri", iFontSize)

        RadioButton1.Font = New System.Drawing.Font("Calibri", iFontSize)
        RadioButton2.Font = New System.Drawing.Font("Calibri", iFontSize)
        RadioButton3.Font = New System.Drawing.Font("Calibri", iFontSize)
        RadioButton4.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_MoveX.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_MoveY.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_MoveZ.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_X.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        Label_Y.Font = New System.Drawing.Font("Calibri", iFontSize - 1)
        Label_Z.Font = New System.Drawing.Font("Calibri", iFontSize - 1)



        HmiDataView_Point.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Point.Rows.Clear()
        HmiDataView_Point.Columns.Clear()
        HmiDataView_Point.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Point.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Point.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        HmiDataView_Point.Columns.Add(PostTest_id)

        Dim PostTest_Action As New DataGridViewTextBoxColumn
        PostTest_Action.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Name")
        PostTest_Action.Name = "PostTest_Action"
        HmiDataView_Point.Columns.Add(PostTest_Action)

        Dim PostTest_X As New DataGridViewTextBoxColumn
        PostTest_X.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "X")
        PostTest_X.Name = "PostTest_X"
        HmiDataView_Point.Columns.Add(PostTest_X)

        Dim PostTest_Y As New DataGridViewTextBoxColumn
        PostTest_Y.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Y")
        PostTest_Y.Name = "PostTest_Y"
        HmiDataView_Point.Columns.Add(PostTest_Y)

        Dim PostTest_Z As New DataGridViewTextBoxColumn
        PostTest_Z.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Z")
        PostTest_Z.Name = "PostTest_Z"
        HmiDataView_Point.Columns.Add(PostTest_Z)

        Label_Needle.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Needle")
        Label_Needle.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        Label_MAX.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_MAX")
        Label_MAX.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        Label_Automatic.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Automatic")
        Label_Automatic.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        Label_ActualX.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_ActualX")
        Label_ActualX.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        Label_ActualY.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_ActualY")
        Label_ActualY.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        Label_ActualZ.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_ActualZ")
        Label_ActualZ.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_Needle.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_MAX.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_Automatic.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_ActualX.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_ActualX.TextBoxReadOnly = True
        HmiTextBox_ActualY.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_ActualY.TextBoxReadOnly = True
        HmiTextBox_ActualZ.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 3)
        HmiTextBox_ActualZ.TextBoxReadOnly = True

        HmiButton_Teach.Button.Enabled = False
        HmiButton_Move.Enabled = False


        lListPoint.Add("Service Position", New clsPointCfg)
        lListPoint.Add("Blindshot Position", New clsPointCfg)
        lListPoint.Add("Purging Position", New clsPointCfg)
        lListPoint.Add("Weighing Position", New clsPointCfg)
        lListPoint.Add("TwinSafe Position", New clsPointCfg)
        lListPoint.Add("NeedleCheck Position", New clsPointCfg)
        Dim mTempValue As String = String.Empty
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            HmiDataView_Point.Rows.Add((i + 1).ToString, lListPoint.Keys(i), lListPoint(lListPoint.Keys(i)).X.ToString("0.00"), lListPoint(lListPoint.Keys(i)).Y.ToString("0.00"), lListPoint(lListPoint.Keys(i)).Z.ToString("0.00"))
        Next

        HmiTextBox_MoveX.TextBoxReadOnly = True
        HmiTextBox_MoveY.TextBoxReadOnly = True
        HmiTextBox_MoveZ.TextBoxReadOnly = True

        Label_X.Text = OldStructGapFiller.fdPLCXPosition.ToString("0.00")
        Label_Y.Text = OldStructGapFiller.fdPLCYPosition.ToString("0.00")
        Label_Z.Text = OldStructGapFiller.fdPLCZPosition.ToString("0.00")
        HmiTextBox_MoveX.TextBox.Text = OldStructGapFiller.fdHMIMoveXPosition.ToString("0.00")
        HmiTextBox_MoveY.TextBox.Text = OldStructGapFiller.fdHMIMoveYPosition.ToString("0.00")
        HmiTextBox_MoveZ.TextBox.Text = OldStructGapFiller.fdHMIMoveZPosition.ToString("0.00")
        HmiTextBox_Needle.TextBox.Text = OldStructGapFiller.fdHMINeedleDiameter.ToString("0.00")
        HmiTextBox_MAX.TextBox.Text = OldStructGapFiller.fdHMIMAXOffsetXY.ToString("0.00")
        HmiTextBox_Automatic.TextBox.Text = OldStructGapFiller.fdHMIAutomaticCheck.ToString("0")
        HmiTextBox_ActualX.TextBox.Text = OldStructGapFiller.fdPLCActualOffsetX.ToString("0.00")
        HmiTextBox_ActualY.TextBox.Text = OldStructGapFiller.fdPLCActualOffsetY.ToString("0.00")
        HmiTextBox_ActualZ.TextBox.Text = OldStructGapFiller.fdPLCActualOffsetZ.ToString("0.00")

        RadioButton4.Checked = True
        HmiTextBox_Speed.TextBox.Text = 100
        TrackBar_Speed.Value = 100

        HmiButton_Save.Button.Enabled = isChanged()

        If bReadOnly Then
            ButtonXAdd.Enabled = False
            ButtonXDec.Enabled = False
            ButtonYAdd.Enabled = False
            ButtonYDec.Enabled = False
            ButtonZAdd.Enabled = False
            ButtonZDec.Enabled = False
            HmiButton_MotorEnable.Enabled = False
            RadioButton1.Enabled = False
            RadioButton2.Enabled = False
            RadioButton3.Enabled = False
            RadioButton4.Enabled = False
            TrackBar_Speed.Enabled = False
            HmiTextBox_Speed.TextBoxReadOnly = True
            HmiButton_Teach.Enabled = False
            HmiButton_Move.Enabled = False
            HmiButton_Save.Enabled = False
            HmiButton_Needle.Enabled = False
            HmiButton_AutoRefer.Enabled = False
            HmiButton_Filling.Enabled = False
            HmiTextBox_Needle.TextBoxReadOnly = True
            HmiTextBox_MAX.TextBoxReadOnly = True
            HmiTextBox_Automatic.TextBoxReadOnly = True
            HmiButton_Release.Enabled = False
            InputIO1.Enabled = False
            InputIO2.Enabled = False
            InputIO3.Enabled = False
            InputIO4.Enabled = False
            InputIO5.Enabled = False
            InputIO6.Enabled = False
        End If



        AddHandler HmiButton_Teach.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Save.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Speed.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MoveZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Needle.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MAX.TextBox.TextChanged, AddressOf TextBox_TextChanged

        AddHandler HmiTextBox_Automatic.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler RadioButton1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton4.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler ButtonXAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZAdd.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonYDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonZDec.MouseDown, AddressOf Button_MouseDown
        AddHandler ButtonXAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZAdd.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonXDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonYDec.MouseUp, AddressOf Button_MouseUp
        AddHandler ButtonZDec.MouseUp, AddressOf Button_MouseUp


        AddHandler TrackBar_Speed.Scroll, AddressOf TrackBar_Speed_Scroll
        AddHandler HmiButton_MotorEnable.Click, AddressOf Button_Click
        AddHandler HmiButton_Move.Click, AddressOf Button_Click
        AddHandler HmiButton_Needle.Click, AddressOf Button_Click
        AddHandler HmiButton_AutoRefer.Click, AddressOf Button_Click
        AddHandler HmiButton_Filling.Click, AddressOf Button_Click
        AddHandler HmiButton_Release.Click, AddressOf Button_Click
        AddHandler InputIO1.Click, AddressOf Button_Click
        AddHandler InputIO2.Click, AddressOf Button_Click
        AddHandler InputIO3.Click, AddressOf Button_Click
        AddHandler InputIO4.Click, AddressOf Button_Click
        AddHandler InputIO5.Click, AddressOf Button_Click
        AddHandler InputIO6.Click, AddressOf Button_Click
        If Not bReadOnly Then AddHandler HmiDataView_Point.CellClick, AddressOf HmiDataView_Point_CellClick
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
            For i = 1 To 6
                Dim OutputIO As HMIButtonWithIndicate
                If lListProgramIO.ContainsKey(i) Then
                    OutputIO = lListProgramIO(i)
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_Click
                    RemoveHandler OutputIO.MouseDown, AddressOf MainButton_MouseDown
                    RemoveHandler OutputIO.MouseUp, AddressOf MainButton_MouseUp
                Else
                    OutputIO = New HMIButtonWithIndicate
                    HmiTableLayoutPanel_Body_Top_Right.Controls.Add(OutputIO, i - 1, 9)
                    OutputIO.Font = New System.Drawing.Font("Calibri", 8.0!)
                    lListProgramIO.Add(i, OutputIO)
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

    Private Function isChanged() As Boolean
        For i = 0 To HmiDataView_Point.RowCount - 1
            If lListPoint(HmiDataView_Point.Rows(i).Cells(1).Value).X <> Single.Parse(HmiDataView_Point.Rows(i).Cells(2).Value) Then
                Return True
            End If
            If lListPoint(HmiDataView_Point.Rows(i).Cells(1).Value).Y <> Single.Parse(HmiDataView_Point.Rows(i).Cells(3).Value) Then
                Return True
            End If
            If lListPoint(HmiDataView_Point.Rows(i).Cells(1).Value).Z <> Single.Parse(HmiDataView_Point.Rows(i).Cells(4).Value) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub



    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_MoveX", "HmiTextBox_MoveY", "HmiTextBox_MoveZ"
                CheckMovePostion()
            Case "HmiTextBox_Speed"
                If HmiTextBox_Speed.TextBox.Text = "" Then HmiTextBox_Speed.TextBox.Text = "0"
                CheckSpeed()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_Needle"
                If HmiTextBox_Needle.TextBox.Text = "" Then HmiTextBox_Needle.TextBox.Text = "0.00"
                CheckNeedle()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_MAX"
                If HmiTextBox_MAX.TextBox.Text = "" Then HmiTextBox_MAX.TextBox.Text = "0.00"
                CheckMax()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)
            Case "HmiTextBox_Automatic"
                If HmiTextBox_Automatic.TextBox.Text = "" Then HmiTextBox_Automatic.TextBox.Text = "0"
                CheckAutomatic()
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name, sender.text)

        End Select
    End Sub


    Private Sub CheckNeedle()
        Try
            If HmiTextBox_Needle.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Needle.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "11"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMINeedleDiameter", Single.Parse(HmiTextBox_Needle.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMINeedleDiameter", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckMax()
        Try
            If HmiTextBox_MAX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MAX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "12"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMAXOffsetXY", Single.Parse(HmiTextBox_MAX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMAXOffsetXY", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckAutomatic()
        Try
            If HmiTextBox_Automatic.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Automatic.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "13"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutomaticCheck", Int16.Parse(HmiTextBox_Automatic.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutomaticCheck", Int16.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckMovePostion()
        Try
            If HmiTextBox_MoveX.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveX.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "4"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(HmiTextBox_MoveX.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveXPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveY.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveY.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "5"), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(HmiTextBox_MoveY.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMoveYPosition", Single.Parse(0))
            End If
            If HmiTextBox_MoveZ.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_MoveZ.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "6"), enumExceptionType.Alarm, ControlUI.FormName))
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
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "7"), enumExceptionType.Alarm, ControlUI.FormName))
                    Return
                End If
                If CInt(HmiTextBox_Speed.TextBox.Text) < 0 Or CInt(HmiTextBox_Speed.TextBox.Text) > 100 Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "8"), enumExceptionType.Alarm, ControlUI.FormName))
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


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Teach"
                Teach()
            Case "HmiButton_Modify"
                Modify()
            Case "HmiButton_Save"
                Save()
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIMotorEnable", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", Not dNewValue)
            Case "HmiButton_Move"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".fdHMIMove", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMove", dNewValue)

            Case "HmiButton_Needle"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMICheckNeedle", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMICheckNeedle", dNewValue)
            Case "HmiButton_AutoRefer"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAutoRefer", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAutoRefer", dNewValue)
            Case "HmiButton_Filling"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIFilling", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIFilling", dNewValue)

            Case "HmiButton_Release"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRelease3D", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)

            Case "InputIO1"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "InputIO2"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYHome", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)

            Case "InputIO3"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZHome", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
            Case "InputIO4"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
            Case "InputIO5"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYReset", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)
            Case "InputIO6"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZReset", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
        End Select
    End Sub

    Private Sub Button_Down(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)

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
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", dNewValue)
            Case "HmiButton_Needle"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMICheckNeedle", dNewValue)
            Case "HmiButton_AutoRefer"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAutoRefer", dNewValue)
            Case "HmiButton_Filling"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIFilling", dNewValue)
            Case "InputIO1"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "InputIO3"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)
            Case "InputIO5"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
            Case "InputIO2"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
            Case "InputIO4"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)
            Case "InputIO6"
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
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
            Case "ButtonZAdd"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZForward", dNewValue)
            Case "ButtonZDec"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIZBackward", dNewValue)
            Case "HmiButton_MotorEnable"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIMotorEnable", dNewValue)
            Case "HmiButton_Needle"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMICheckNeedle", dNewValue)
            Case "HmiButton_AutoRefer"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAutoRefer", dNewValue)
            Case "HmiButton_Filling"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIFilling", dNewValue)
            Case "InputIO1"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
            Case "InputIO3"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)
            Case "InputIO5"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
            Case "InputIO2"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
            Case "InputIO4"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)
            Case "InputIO6"
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
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


    Private Sub Teach()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value = Label_X.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value = Label_Y.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value = Label_Z.Text
            HmiButton_Save.Button.Enabled = isChanged()
        End If
    End Sub

    Private Sub Modify()
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value = HmiTextBox_MoveX.TextBox.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value = HmiTextBox_MoveY.TextBox.Text
            HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value = HmiTextBox_MoveZ.TextBox.Text
            HmiButton_Save.Button.Enabled = isChanged()
        End If
    End Sub

    Private Sub Save()
        Try
            For i = 0 To HmiDataView_Point.RowCount - 1
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiDataView_Point.Rows(i).Cells(1).Value, "X", HmiDataView_Point.Rows(i).Cells(2).Value)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiDataView_Point.Rows(i).Cells(1).Value, "Y", HmiDataView_Point.Rows(i).Cells(3).Value)
                cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", HmiDataView_Point.Rows(i).Cells(1).Value, "Z", HmiDataView_Point.Rows(i).Cells(4).Value)
            Next
            Dim mTempValue As String = String.Empty
            For i = 0 To lListPoint.Count - 1
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).X = 0
                Else
                    lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Y = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Z = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
                End If
            Next
            Dim cPoint(9) As StructPoint
            For i = 0 To lListPoint.Count - 1
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).X = 0
                Else
                    lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Y = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
                If mTempValue = "" Then
                    lListPoint(lListPoint.Keys(i)).Z = 0
                Else
                    lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
                End If
                If i <= lListPoint.Count - 1 Then
                    cPoint(i) = New StructPoint
                    cPoint(i).strHMIName = lListPoint.Keys(i)
                    cPoint(i).fdXPosition = lListPoint(lListPoint.Keys(i)).X
                    cPoint(i).fdYPosition = lListPoint(lListPoint.Keys(i)).Y
                    cPoint(i).fdZPosition = lListPoint(lListPoint.Keys(i)).Z
                End If
            Next
            cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)
            HmiButton_Save.Button.Enabled = isChanged()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Private Sub HmiDataView_Point_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_Point.CurrentRow) Then Return
        If HmiDataView_Point.CurrentRow.Index <= HmiDataView_Point.Rows.Count - 1 Then
            HmiTextBox_MoveX.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(2).Value
            HmiTextBox_MoveY.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(3).Value
            HmiTextBox_MoveZ.Text = HmiDataView_Point.Rows(HmiDataView_Point.CurrentRow.Index).Cells(4).Value
            HmiButton_Teach.Button.Enabled = True
            HmiButton_Move.Enabled = True
        End If
    End Sub

    Public Sub RefreshUI()
        Try
            Select Case iStep

                Case 1
                    Dim fNewValue As Single = 0.0
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIStep", fNewValue)
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIContinueEnable", True)
                    iStep = iStep + 1

                Case 2
                    Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
                    If iPageNr <= 0 Then iPageNr = 1
                    cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramButton, GetType(Boolean()), New Boolean(iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber) {}, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                    iPageNr = cProgramCylinderButton.ListPage.Keys.Count
                    If iPageNr <= 0 Then iPageNr = 1
                    Dim cDefaultValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
                    cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton, GetType(StructDebugCylinder()), cDefaultValue, New Integer() {iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber})

                    TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                    iStep = iStep + 1

                Case 3
                    If TempStructGapFiller.fdHMISpeed = "0.1" Then
                        mMainForm.InvokeAction(Sub()
                                                   RadioButton1.Checked = True
                                               End Sub)
                    End If
                    If TempStructGapFiller.fdHMISpeed = "1" Then
                        mMainForm.InvokeAction(Sub()
                                                   RadioButton2.Checked = True
                                               End Sub)
                    End If
                    If TempStructGapFiller.fdHMISpeed = "10" Then
                        mMainForm.InvokeAction(Sub()
                                                   RadioButton3.Checked = True
                                               End Sub)
                    End If
                    If TempStructGapFiller.bulHMIContinueEnable Then
                        mMainForm.InvokeAction(Sub()
                                                   RadioButton4.Checked = True
                                               End Sub)
                    End If

                    mMainForm.InvokeAction(Sub()
                                               HmiTextBox_Speed.TextBox.Text = TempStructGapFiller.fdHMISpeed.ToString("0")
                                               HmiTextBox_Needle.TextBox.Text = TempStructGapFiller.fdHMINeedleDiameter.ToString("0.00")
                                               HmiTextBox_MAX.TextBox.Text = TempStructGapFiller.fdHMIMAXOffsetXY.ToString("0.00")
                                               HmiTextBox_Automatic.TextBox.Text = TempStructGapFiller.fdHMIAutomaticCheck.ToString("0")
                                           End Sub)
                    iStep = iStep + 1

                Case 4

                    Dim lListDI1() As Boolean = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramButton)
                    Dim lCylinder() As StructDebugCylinder = cHMIPLC.GetValue(HMI_PLC_Interface.HMI_ProgramCylinderButton)
                    Dim iCnt As Integer = 1
                    For Each element As clsDeviceProgramCfg In cDeviceProgramButton.ListIndex.Values
                        Dim cDeviceProgramCfg As clsDeviceProgramCfg = cDeviceProgramButton.GetIOCfgFromID(iCnt)
                        If cDeviceProgramCfg.Type = clsProgramButton.Name Then
                            Dim cIOCfg As clsIOCfg = cProgramButton.GetIOCfgFromIndex(element.Index)
                            lListProgramIO(iCnt).SetIndicateBackColor(lListDI1((cIOCfg.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cIOCfg.YIndex - 1))
                        End If
                        If cDeviceProgramCfg.Type = clsProgramCylinderButton.Name Then
                            Dim cCylinderIO As clsCylinderCfg = cProgramCylinderButton.GetCylinderCfgFromIndex(element.Index)
                            lListProgramIO(iCnt).SetIndicateBackColor(lCylinder((cCylinderIO.XIndex - 1) * HMI_PLC_Interface.CON_MAXIMUM_PageNumber + cCylinderIO.YIndex - 1).bulDOB)
                        End If
                        iCnt = iCnt + 1
                    Next

                    TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))
                    If TempStructGapFiller.fdPLCXPosition <> OldStructGapFiller.fdPLCXPosition Or TempStructGapFiller.fdPLCYPosition <> OldStructGapFiller.fdPLCYPosition Or TempStructGapFiller.fdPLCZPosition <> OldStructGapFiller.fdPLCZPosition Then
                        mMainForm.InvokeAction(Sub()
                                                   Label_X.Text = TempStructGapFiller.fdPLCXPosition.ToString("0.00")
                                                   Label_Y.Text = TempStructGapFiller.fdPLCYPosition.ToString("0.00")
                                                   Label_Z.Text = TempStructGapFiller.fdPLCZPosition.ToString("0.00")
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdPLCActualOffsetX <> OldStructGapFiller.fdPLCActualOffsetX Or TempStructGapFiller.fdPLCActualOffsetY <> OldStructGapFiller.fdPLCActualOffsetY Or TempStructGapFiller.fdPLCActualOffsetZ <> OldStructGapFiller.fdPLCActualOffsetZ Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_ActualX.Text = TempStructGapFiller.fdPLCActualOffsetX.ToString("0.00")
                                                   HmiTextBox_ActualY.Text = TempStructGapFiller.fdPLCActualOffsetY.ToString("0.00")
                                                   HmiTextBox_ActualZ.Text = TempStructGapFiller.fdPLCActualOffsetZ.ToString("0.00")
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIXForward <> OldStructGapFiller.bulHMIXForward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonXAdd.SetIndicateBackColor(TempStructGapFiller.bulHMIXForward)
                                               End Sub)
                    End If
                    If TempStructGapFiller.bulHMIXBackward <> OldStructGapFiller.bulHMIXBackward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonXDec.SetIndicateBackColor(TempStructGapFiller.bulHMIXBackward)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIYForward <> OldStructGapFiller.bulHMIYForward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonYAdd.SetIndicateBackColor(TempStructGapFiller.bulHMIYForward)
                                               End Sub)
                    End If
                    If TempStructGapFiller.bulHMIYBackward <> OldStructGapFiller.bulHMIYBackward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonYDec.SetIndicateBackColor(TempStructGapFiller.bulHMIYBackward)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIZForward <> OldStructGapFiller.bulHMIZForward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonZAdd.SetIndicateBackColor(TempStructGapFiller.bulHMIZForward)
                                               End Sub)
                    End If
                    If TempStructGapFiller.bulHMIZBackward <> OldStructGapFiller.bulHMIZBackward Then
                        mMainForm.InvokeAction(Sub()
                                                   ButtonZDec.SetIndicateBackColor(TempStructGapFiller.bulHMIZBackward)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIMotorEnable <> OldStructGapFiller.bulHMIMotorEnable Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_MotorEnable.SetIndicateBackColor(TempStructGapFiller.bulHMIMotorEnable)
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdHMIMove.bulHMIDoAction <> OldStructGapFiller.fdHMIMove.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Move.SetIndicateColor(TempStructGapFiller.fdHMIMove.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.fdHMIMove.bulPlcActionIsFail <> OldStructGapFiller.fdHMIMove.bulPlcActionIsFail Or TempStructGapFiller.fdHMIMove.bulPlcActionIsPass <> OldStructGapFiller.fdHMIMove.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Move.SetIndicateColor(TempStructGapFiller.fdHMIMove.bulPlcActionIsPass, TempStructGapFiller.fdHMIMove.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.fdHMIMove.bulPlcActionIsFail Or TempStructGapFiller.fdHMIMove.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".fdHMIMove", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIMove", dNewValue)
                            End If
                        End If
                    End If

                    'HmiButton_Needle
                    If TempStructGapFiller.bulHMICheckNeedle.bulHMIDoAction <> OldStructGapFiller.bulHMICheckNeedle.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Needle.SetIndicateColor(TempStructGapFiller.bulHMICheckNeedle.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail <> OldStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail Or TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass <> OldStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Needle.SetIndicateColor(TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass, TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail Or TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMICheckNeedle", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMICheckNeedle", dNewValue)
                            End If
                        End If
                    End If

                    'HmiButton_AutoRefer
                    If TempStructGapFiller.bulHMIAutoRefer.bulHMIDoAction <> OldStructGapFiller.bulHMIAutoRefer.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_AutoRefer.SetIndicateColor(TempStructGapFiller.bulHMIAutoRefer.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_AutoRefer.SetIndicateColor(TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass, TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail)
                                               End Sub)

                        If TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAutoRefer", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAutoRefer", dNewValue)
                            End If
                        End If
                    End If

                    'HmiButton_Filling
                    If TempStructGapFiller.bulHMIFilling.bulHMIDoAction <> OldStructGapFiller.bulHMIFilling.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Filling.SetIndicateColor(TempStructGapFiller.bulHMIFilling.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIFilling.bulPlcActionIsFail <> OldStructGapFiller.bulHMIFilling.bulPlcActionIsFail Or TempStructGapFiller.bulHMIFilling.bulPlcActionIsPass <> OldStructGapFiller.bulHMIFilling.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Filling.SetIndicateColor(TempStructGapFiller.bulHMIFilling.bulPlcActionIsPass, TempStructGapFiller.bulHMIFilling.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIFilling.bulPlcActionIsFail Or TempStructGapFiller.bulHMIFilling.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIFilling", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIFilling", dNewValue)
                            End If
                        End If
                    End If

                    'bulHMIRelease3D
                    If TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction <> OldStructGapFiller.bulHMIRelease3D.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Release.SetIndicateColor(TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail <> OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass <> OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButton_Release.SetIndicateColor(TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass, TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail Or TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRelease3D", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRelease3D", dNewValue)
                            End If
                        End If
                    End If


                    'InputIO1
                    If TempStructGapFiller.bulHMIAxisXHome.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisXHome.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO1.SetIndicateColor(TempStructGapFiller.bulHMIAxisXHome.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO1.SetIndicateColor(TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXHome", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXHome", dNewValue)
                            End If
                        End If
                    End If

                    'InputIO2
                    If TempStructGapFiller.bulHMIAxisYHome.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisYHome.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO2.SetIndicateColor(TempStructGapFiller.bulHMIAxisYHome.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO2.SetIndicateColor(TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYHome", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYHome", dNewValue)
                            End If
                        End If
                    End If

                    'InputIO3
                    If TempStructGapFiller.bulHMIAxisZHome.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisZHome.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO3.SetIndicateColor(TempStructGapFiller.bulHMIAxisZHome.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO3.SetIndicateColor(TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZHome", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZHome", dNewValue)
                            End If
                        End If
                    End If


                    'InputIO4
                    If TempStructGapFiller.bulHMIAxisXReset.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisXReset.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO4.SetIndicateColor(TempStructGapFiller.bulHMIAxisXReset.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO4.SetIndicateColor(TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisXReset", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisXReset", dNewValue)
                            End If
                        End If
                    End If

                    'InputIO5
                    If TempStructGapFiller.bulHMIAxisYReset.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisYReset.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO5.SetIndicateColor(TempStructGapFiller.bulHMIAxisYReset.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO5.SetIndicateColor(TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisYReset", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisYReset", dNewValue)
                            End If
                        End If
                    End If

                    'InputIO6
                    If TempStructGapFiller.bulHMIAxisZReset.bulHMIDoAction <> OldStructGapFiller.bulHMIAxisZReset.bulHMIDoAction Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO6.SetIndicateColor(TempStructGapFiller.bulHMIAxisZReset.bulHMIDoAction)
                                               End Sub)
                    End If

                    If TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail <> OldStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass <> OldStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass Then
                        mMainForm.InvokeAction(Sub()
                                                   InputIO6.SetIndicateColor(TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass, TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail)
                                               End Sub)
                        If TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail Or TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass Then
                            If Not bReadOnly Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIAxisZReset", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIAxisZReset", dNewValue)
                            End If
                        End If
                    End If

                    OldStructGapFiller.fdPLCActualOffsetX = TempStructGapFiller.fdPLCActualOffsetX
                    OldStructGapFiller.fdPLCActualOffsetY = TempStructGapFiller.fdPLCActualOffsetY
                    OldStructGapFiller.fdPLCActualOffsetZ = TempStructGapFiller.fdPLCActualOffsetZ
                    OldStructGapFiller.fdPLCXPosition = TempStructGapFiller.fdPLCXPosition
                    OldStructGapFiller.fdPLCYPosition = TempStructGapFiller.fdPLCYPosition
                    OldStructGapFiller.fdPLCZPosition = TempStructGapFiller.fdPLCZPosition
                    OldStructGapFiller.bulPLCAxisXHome = TempStructGapFiller.bulPLCAxisXHome
                    OldStructGapFiller.bulPLCAxisYHome = TempStructGapFiller.bulPLCAxisYHome
                    OldStructGapFiller.bulPLCAxisZHome = TempStructGapFiller.bulPLCAxisZHome
                    OldStructGapFiller.bulPLCAxisXReset = TempStructGapFiller.bulPLCAxisXReset
                    OldStructGapFiller.bulPLCAxisYReset = TempStructGapFiller.bulPLCAxisYReset
                    OldStructGapFiller.bulPLCAxisZReset = TempStructGapFiller.bulPLCAxisZReset
                    OldStructGapFiller.bulHMIXForward = TempStructGapFiller.bulHMIXForward
                    OldStructGapFiller.bulHMIXBackward = TempStructGapFiller.bulHMIXBackward
                    OldStructGapFiller.bulHMIYForward = TempStructGapFiller.bulHMIYForward
                    OldStructGapFiller.bulHMIYBackward = TempStructGapFiller.bulHMIYBackward
                    OldStructGapFiller.bulHMIZForward = TempStructGapFiller.bulHMIZForward
                    OldStructGapFiller.bulHMIZBackward = TempStructGapFiller.bulHMIZBackward
                    OldStructGapFiller.bulHMIMotorEnable = TempStructGapFiller.bulHMIMotorEnable

                    OldStructGapFiller.fdHMIMove.bulHMIDoAction = TempStructGapFiller.fdHMIMove.bulHMIDoAction
                    OldStructGapFiller.fdHMIMove.bulPlcActionIsPass = TempStructGapFiller.fdHMIMove.bulPlcActionIsPass
                    OldStructGapFiller.fdHMIMove.bulPlcActionIsFail = TempStructGapFiller.fdHMIMove.bulPlcActionIsFail

                    OldStructGapFiller.bulHMICheckNeedle.bulHMIDoAction = TempStructGapFiller.bulHMICheckNeedle.bulHMIDoAction
                    OldStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass = TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsPass
                    OldStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail = TempStructGapFiller.bulHMICheckNeedle.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAutoRefer.bulHMIDoAction = TempStructGapFiller.bulHMIAutoRefer.bulHMIDoAction
                    OldStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass = TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail = TempStructGapFiller.bulHMIAutoRefer.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIFilling.bulHMIDoAction = TempStructGapFiller.bulHMIFilling.bulHMIDoAction
                    OldStructGapFiller.bulHMIFilling.bulPlcActionIsPass = TempStructGapFiller.bulHMIFilling.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIFilling.bulPlcActionIsFail = TempStructGapFiller.bulHMIFilling.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIRelease3D.bulHMIDoAction = TempStructGapFiller.bulHMIRelease3D.bulHMIDoAction
                    OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass = TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail = TempStructGapFiller.bulHMIRelease3D.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisXHome.bulHMIDoAction = TempStructGapFiller.bulHMIAxisXHome.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisXHome.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisYHome.bulHMIDoAction = TempStructGapFiller.bulHMIAxisYHome.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisYHome.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisZHome.bulHMIDoAction = TempStructGapFiller.bulHMIAxisZHome.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisZHome.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisXReset.bulHMIDoAction = TempStructGapFiller.bulHMIAxisXReset.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisXReset.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisYReset.bulHMIDoAction = TempStructGapFiller.bulHMIAxisYReset.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisYReset.bulPlcActionIsFail

                    OldStructGapFiller.bulHMIAxisZReset.bulHMIDoAction = TempStructGapFiller.bulHMIAxisZReset.bulHMIDoAction
                    OldStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass = TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsPass
                    OldStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail = TempStructGapFiller.bulHMIAxisZReset.bulPlcActionIsFail
            End Select
        Catch ex As Exception
            If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try


    End Sub

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        iStep = 1
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
        iStep = 1
        Return True
    End Function
End Class