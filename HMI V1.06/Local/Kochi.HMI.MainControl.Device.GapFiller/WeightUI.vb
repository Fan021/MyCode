Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Statistics.GapFiller
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent
Imports System.Windows.Forms.DataVisualization.Charting

Public Class WeightUI
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
    Private cVariantManager As clsVariantManager
    Protected cLanguageManager As clsLanguageManager
    Private bExit As Boolean = False
    Private cThread As Thread
    Private mMainForm As IMainUI
    Private cIniHandler As clsIniHandler
    Private cGapFiller As clsGapFiller
    Private cSystemManager As clsSystemManager
    Private OldStructGapFiller As New StructGapFiller
    Private TempStructGapFiller As New StructGapFiller
    Private ePageType As enumPageType
    Private iIndex As Integer = 0
    Private iCnt As Integer = 1
    Private WeightValue As Series = New Series("Value")
    Private WeightLowValue As Series = New Series("LowValue")
    Private WeightUpValue As Series = New Series("UpValue")
    Private iStep As Integer = 1
    Private lListWeightValue As New List(Of Double)
    Private cChangePage As clsChangePage
    Private cChildrenGapFillerForm As ChildrenGapFillerForm
    Private iLastCurrentIndex As Integer = 1
    Private cGapFillerDataManager As clsGapFillerDataManager
    Private cMainTipsManager As clsMainTipsManager
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private cDeviceCfg As clsDeviceCfg
    Private bReset As Boolean = False
    Private cUserManager As clsUserManager
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

    Public Property ObjectSource As clsGapFiller
        Set(ByVal value As clsGapFiller)
            cGapFiller = value
        End Set
        Get
            Return cGapFiller
        End Get
    End Property
    Public Property PageType As enumPageType
        Set(ByVal value As enumPageType)
            ePageType = value
        End Set
        Get
            Return ePageType
        End Get
    End Property
    Public Property Index As Integer
        Set(ByVal value As Integer)
            iIndex = value
        End Set
        Get
            Return iIndex
        End Get
    End Property
    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cHMIPLC = cDeviceManager.GetPLCDevice()
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cGapFiller.Name)
            cChangePage = New clsChangePage
            cChangePage.Init(cLocalElement, cSystemElement)
            cChangePage.RegisterManager(Panel_Top, Panel_UI)
            cGapFillerDataManager = New clsGapFillerDataManager
            cGapFillerDataManager.Init(cSystemElement)
            InitForm()
            InitControlText()
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
        RemoveHandler HmiTextBox_Max.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_Min.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_PreShot.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_PreShotTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_Pause.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_PerCycle.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_DispensingTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_DispensingPause.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_AutoWeightNo.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler CheckBox_AutoWeight.CheckedChanged, AddressOf CheckBox_AutoWeight_CheckedChanged
        RemoveHandler Button_Data.Click, AddressOf Button_Click
        RemoveHandler Button_Back.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_Start.Click, AddressOf Button_Click

        Label_Title.Font = New System.Drawing.Font("Calibri", iFontSize, FontStyle.Bold)
        Label_Name.Font = New System.Drawing.Font("Calibri", iFontSize, FontStyle.Bold)

        GroupBox_Cup.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_Cup" + "_" + ePageType.ToString)
        GroupBox_Cup.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Cup.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Cup" + "_" + ePageType.ToString)
        Label_Cup.Font = New System.Drawing.Font("Calibri", iFontSize)

        Label_Active.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Active" + "_" + ePageType.ToString)
        Label_Active.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButtonWithIndicate_Start.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_Start" + "_" + ePageType.ToString)
        HmiButtonWithIndicate_Start.Font = New System.Drawing.Font("Calibri", iFontSize)

        GroupBox_Weight.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_Weight" + "_" + ePageType.ToString)
        GroupBox_Weight.Font = New System.Drawing.Font("Calibri", iFontSize)
        GroupBox_AutoWeight.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_AutoWeight" + "_" + ePageType.ToString)
        GroupBox_AutoWeight.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label3_AutoWeight.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label3_AutoWeight" + "_" + ePageType.ToString)
        Label3_AutoWeight.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_AutoWeightNo.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_AutoWeightNo" + "_" + ePageType.ToString)
        Label_AutoWeightNo.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Max.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Max" + "_" + ePageType.ToString)
        Label_Max.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Min.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Min" + "_" + ePageType.ToString)
        Label_Min.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_PreShot.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_PreShot" + "_" + ePageType.ToString)
        Label_PreShot.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_PreShotTime.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_PreShotTime" + "_" + ePageType.ToString)
        Label_PreShotTime.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Pause.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Pause" + "_" + ePageType.ToString)
        Label_Pause.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_PerCycle.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_PerCycle" + "_" + ePageType.ToString)
        Label_PerCycle.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_DispensingTime.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_DispensingTime" + "_" + ePageType.ToString)
        Label_DispensingTime.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_DispensingPause.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_DispensingPause" + "_" + ePageType.ToString)
        Label_DispensingPause.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_LastWeight.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_LastWeight" + "_" + ePageType.ToString)
        Label_LastWeight.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_PerCycle2.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_PerCycle2" + "_" + ePageType.ToString)
        Label_PerCycle2.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_InCycle.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_InCycle" + "_" + ePageType.ToString)
        Label_InCycle.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_MaxWeight.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_MaxWeight" + "_" + ePageType.ToString)
        Label_MaxWeight.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Weight2.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Weight2" + "_" + ePageType.ToString)
        Label_Weight2.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Weight.Font = New System.Drawing.Font("Calibri", iFontSize + 2, FontStyle.Bold)
        Label_MinWeight.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_MinWeight" + "_" + ePageType.ToString)
        Label_MinWeight.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Std.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Std" + "_" + ePageType.ToString)
        Label_Std.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_CP.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_CP" + "_" + ePageType.ToString)
        Label_CP.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_CPK.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_CPK" + "_" + ePageType.ToString)
        Label_CPK.Font = New System.Drawing.Font("Calibri", iFontSize)
        Label_Result.Text = cLanguageManager.GetUserTextLine("GapFiller", "Label_Result" + "_" + ePageType.ToString)
        Label_Result.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_AutoWeightNo.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Max.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Min.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Max.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_PreShot.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_PreShotTime.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Pause.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_PerCycle.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_DispensingTime.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_DispensingPause.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_LastWeight.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_PerCycle2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_InCycle.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_MaxWeight.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Weight2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_MinWeight.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Std.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_CP.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_CPK.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiTextBox_LastWeight.TextBoxReadOnly = True
        HmiTextBox_PerCycle2.TextBoxReadOnly = True
        HmiTextBox_InCycle.TextBoxReadOnly = True
        HmiTextBox_MaxWeight.TextBoxReadOnly = True
        HmiTextBox_Weight2.TextBoxReadOnly = True
        HmiTextBox_MinWeight.TextBoxReadOnly = True
        HmiTextBox_Std.TextBoxReadOnly = True
        HmiTextBox_CP.TextBoxReadOnly = True
        HmiTextBox_CPK.TextBoxReadOnly = True

        HmiTextBox_Max.ValueType = GetType(Double)
        HmiTextBox_Min.ValueType = GetType(Double)
        HmiTextBox_PreShot.ValueType = GetType(Integer)
        HmiTextBox_PreShotTime.ValueType = GetType(Double)
        HmiTextBox_Pause.ValueType = GetType(Double)
        HmiTextBox_PerCycle.ValueType = GetType(Integer)
        HmiTextBox_DispensingTime.ValueType = GetType(Double)
        HmiTextBox_DispensingPause.ValueType = GetType(Double)

        If Not bReset Then
            HmiTextBox_AutoWeightNo.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMAutoWeightNr.ToString
            HmiTextBox_LastWeight.TextBox.Text = "00"
            HmiTextBox_PerCycle2.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle.ToString
            HmiTextBox_InCycle.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle.ToString
            HmiTextBox_MaxWeight.TextBox.Text = "0.00"
            HmiTextBox_MinWeight.TextBox.Text = "0.00"
            HmiTextBox_Weight2.TextBox.Text = "0.00"
            HmiTextBox_Std.TextBox.Text = "0.00"
            HmiTextBox_CP.TextBox.Text = "0.00"
            HmiTextBox_CPK.TextBox.Text = "0.00"
            SetTitle()
            iLastCurrentIndex = 0

            OldStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint = Enumerable.Repeat(New StructWeightPoint, 100).ToArray()
        End If

        If Not bReset Then
            HmiTextBox_Max.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax.ToString
            HmiTextBox_Min.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin.ToString
            HmiTextBox_PreShot.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepShots.ToString
            HmiTextBox_PreShotTime.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepShot.ToString
            HmiTextBox_Pause.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepPause.ToString
            HmiTextBox_PerCycle.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIShotsPreCycle.ToString
            HmiTextBox_DispensingTime.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIDispensing.ToString
            HmiTextBox_DispensingPause.TextBox.Text = OldStructGapFiller.PLC_Weight(Index - 1).fdHMIDispensingPause.ToString


            If OldStructGapFiller.PLC_Weight(Index - 1).fdHMAutoWeight Then
                CheckBox_AutoWeight.Checked = True
                HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
            Else
                CheckBox_AutoWeight.Checked = False
                HmiTextBox_AutoWeightNo.TextBoxReadOnly = True
            End If

            bReset = True
        End If

        If bReadOnly Then
            CheckBox_AutoWeight.Enabled = False
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = True
            HmiTextBox_Max.TextBoxReadOnly = True
            HmiTextBox_Min.TextBoxReadOnly = True
            HmiTextBox_Max.TextBoxReadOnly = True
            HmiTextBox_PreShot.TextBoxReadOnly = True
            HmiTextBox_PreShotTime.TextBoxReadOnly = True
            HmiTextBox_Pause.TextBoxReadOnly = True
            HmiTextBox_PerCycle.TextBoxReadOnly = True
            HmiTextBox_DispensingTime.TextBoxReadOnly = True
            HmiTextBox_DispensingPause.TextBoxReadOnly = True
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = True
            HmiButtonWithIndicate_Start.Enabled = True
        Else
            CheckBox_AutoWeight.Enabled = True
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
            HmiTextBox_Max.TextBoxReadOnly = False
            HmiTextBox_Min.TextBoxReadOnly = False
            HmiTextBox_Max.TextBoxReadOnly = False
            HmiTextBox_PreShot.TextBoxReadOnly = False
            HmiTextBox_PreShotTime.TextBoxReadOnly = False
            HmiTextBox_Pause.TextBoxReadOnly = False
            HmiTextBox_PerCycle.TextBoxReadOnly = False
            HmiTextBox_DispensingTime.TextBoxReadOnly = False
            HmiTextBox_DispensingPause.TextBoxReadOnly = False
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
            HmiButtonWithIndicate_Start.Enabled = True
        End If

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Engineer Then
            HmiButtonWithIndicate_Start.Enabled = False
        End If
       

        AddHandler HmiTextBox_Max.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Min.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_PreShot.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_PreShotTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Pause.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_PerCycle.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_DispensingTime.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_DispensingPause.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_AutoWeightNo.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler CheckBox_AutoWeight.CheckedChanged, AddressOf CheckBox_AutoWeight_CheckedChanged
        AddHandler Button_Data.Click, AddressOf Button_Click
        AddHandler Button_Back.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_Start.Click, AddressOf Button_Click
        Return True
    End Function


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Button_Data"
                If Not IsNothing(cChildrenGapFillerForm) Then cChildrenGapFillerForm.Quit(cLocalElement, cSystemElement)
                cChildrenGapFillerForm = New ChildrenGapFillerForm
                cChildrenGapFillerForm.SelectComponent = ePageType
                cChildrenGapFillerForm.Init(cLocalElement, cSystemElement)
                cChangePage.ChangePage(cChildrenGapFillerForm.UI)
            Case "Button_Back"
                If Not IsNothing(cChildrenGapFillerForm) Then cChildrenGapFillerForm.Quit(cLocalElement, cSystemElement)
                cChangePage.BackPage()
            Case "HmiButtonWithIndicate_Start"
                Dim oldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIStartWeight", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIStartWeight", Not oldValue)
        End Select
    End Sub
    Public Sub SetTitle()
        Dim iShot As Integer = 0
        Dim dMin As Double = 0
        Dim dMax As Double = 0
        If HmiTextBox_Min.TextBox.Text = "" Then
            dMin = 0
        Else
            If Not IsNumeric(HmiTextBox_Min.TextBox.Text) Then
                dMin = 0
            Else
                dMin = Double.Parse(HmiTextBox_Min.TextBox.Text)
            End If

        End If

        If HmiTextBox_Max.TextBox.Text = "" Then
            dMax = 0
        Else
            If Not IsNumeric(HmiTextBox_Max.TextBox.Text) Then
                dMax = 0
            Else
                dMax = Double.Parse(HmiTextBox_Max.TextBox.Text)
            End If

        End If

        If HmiTextBox_PerCycle.TextBox.Text = "" Then
            iShot = 50
        Else
            iShot = Integer.Parse(HmiTextBox_PerCycle.TextBox.Text)
        End If

        If dMax < dMin Then
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "26", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
            Return
        End If

        Dim dAvg As Single = (Single.Parse(dMin) + Single.Parse(dMax)) / 2
        If ePageType = enumPageType.A Then
            Label_Title.Text = cLanguageManager.GetUserTextLine("GapFiller", "A", dAvg.ToString("0.00"))
            Label_Name.Text = cLanguageManager.GetUserTextLine("GapFiller", "Component", ePageType.ToString)
        End If
        If ePageType = enumPageType.B Then
            Label_Title.Text = cLanguageManager.GetUserTextLine("GapFiller", "B", dAvg.ToString("0.00"))
            Label_Name.Text = cLanguageManager.GetUserTextLine("GapFiller", "Component", ePageType.ToString)
        End If
        If ePageType = enumPageType.AB Then
            Label_Title.Text = cLanguageManager.GetUserTextLine("GapFiller", "AB", (dAvg / 2).ToString("0.00"), (dAvg / 2).ToString("0.00"), dAvg.ToString("0.00"))
            Label_Name.Text = cLanguageManager.GetUserTextLine("GapFiller", "Mixture")
        End If
        ChartDefaultWeight(iShot, dMin, dMax)
    End Sub

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter

        Return True
    End Function

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Max"
                If HmiTextBox_Max.TextBox.Text = "" Then HmiTextBox_Max.TextBox.Text = "0.00"
                CheckMax()
                SetTitle()
            Case "HmiTextBox_Min"
                If HmiTextBox_Min.TextBox.Text = "" Then HmiTextBox_Min.TextBox.Text = "0.00"
                CheckMin()
                SetTitle()
            Case "HmiTextBox_PreShot"
                If HmiTextBox_PreShot.TextBox.Text = "" Then HmiTextBox_PreShot.TextBox.Text = "0"
                CheckPreShots()
            Case "HmiTextBox_PreShotTime"
                If HmiTextBox_PreShotTime.TextBox.Text = "" Then HmiTextBox_PreShotTime.TextBox.Text = "0.00"
                CheckPreShotTime()
            Case "HmiTextBox_Pause"
                If HmiTextBox_Pause.TextBox.Text = "" Then HmiTextBox_Pause.TextBox.Text = "0.00"
                CheckPause()
            Case "HmiTextBox_PerCycle"
                If HmiTextBox_PerCycle.TextBox.Text = "" Then HmiTextBox_PerCycle.TextBox.Text = "0"
                CheckPerCycle()
                SetTitle()
            Case "HmiTextBox_DispensingTime"
                If HmiTextBox_DispensingTime.TextBox.Text = "" Then HmiTextBox_DispensingTime.TextBox.Text = "0.00"
                CheckDispensingTime()
            Case "HmiTextBox_DispensingPause"
                If HmiTextBox_DispensingPause.TextBox.Text = "" Then HmiTextBox_DispensingPause.TextBox.Text = "0.00"
                CheckDispensingPause()
            Case "HmiTextBox_AutoWeightNo"
                If HmiTextBox_AutoWeightNo.TextBox.Text = "" Then HmiTextBox_AutoWeightNo.TextBox.Text = "0"
                CheckAutoWeightNo()
        End Select
        cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name + Index.ToString, sender.text)

    End Sub

    Private Sub CheckMax()
        Try
            If HmiTextBox_Max.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Max.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "18", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIWeightMax", Single.Parse(HmiTextBox_Max.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIWeightMax", Single.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckMin()
        Try
            If HmiTextBox_Min.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Min.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "19", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIWeightMin", Single.Parse(HmiTextBox_Min.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIWeightMin", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckPreShots()
        Try
            If HmiTextBox_PreShot.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_PreShot.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "20", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepShots", Int16.Parse(HmiTextBox_PreShot.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepShots", Int16.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckPreShotTime()
        Try
            If HmiTextBox_PreShotTime.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_PreShotTime.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "21", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepShot", Single.Parse(HmiTextBox_PreShotTime.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepShot", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckPause()
        Try
            If HmiTextBox_Pause.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_Pause.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "22", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepPause", Single.Parse(HmiTextBox_Pause.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIPrepPause", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckPerCycle()
        Try
            If HmiTextBox_PerCycle.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_PerCycle.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "23", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIShotsPreCycle", Int16.Parse(HmiTextBox_PerCycle.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIShotsPreCycle", Int16.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckDispensingTime()
        Try
            If HmiTextBox_DispensingTime.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_DispensingTime.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "24", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIDispensing", Single.Parse(HmiTextBox_DispensingTime.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIDispensing", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckDispensingPause()
        Try
            If HmiTextBox_DispensingPause.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_DispensingPause.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "25", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIDispensingPause", Single.Parse(HmiTextBox_DispensingPause.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMIDispensingPause", Single.Parse(0))
            End If

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub

    Private Sub CheckAutoWeightNo()
        Try
            If HmiTextBox_AutoWeightNo.TextBox.Text <> "" Then
                If Not IsNumeric(HmiTextBox_AutoWeightNo.TextBox.Text) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "26", ePageType.ToString), enumExceptionType.Alarm, ControlUI.FormName))
                End If
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMAutoWeightNr", Int16.Parse(HmiTextBox_AutoWeightNo.TextBox.Text))
            Else
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMAutoWeightNr", Int16.Parse(0))
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, ControlUI.FormName))
        End Try
    End Sub


    Private Sub ChartDefaultWeight(ByVal iShot As Integer, ByVal dLowLimtValue As Double, ByVal dUpLimtValue As Double)
        Try
            If iShot <= 0 Then
                iShot = 50
            End If
            If dLowLimtValue <= 0 Then
                dLowLimtValue = 2.85
            End If

            If dUpLimtValue <= 0 Then
                dUpLimtValue = 3.15
            End If

            If dUpLimtValue <= dLowLimtValue Then
                dLowLimtValue = 0
            End If

            Chart_Weight_Value.Series().Clear()
            WeightValue.ChartType = SeriesChartType.Line
            WeightValue.BorderWidth = 1
            WeightValue.Name = "WeightValue"


            WeightLowValue.ChartType = SeriesChartType.Line
            WeightLowValue.BorderWidth = 2
            WeightLowValue.BorderColor = Color.Red

            WeightUpValue.ChartType = SeriesChartType.Line
            WeightUpValue.BorderWidth = 2
            WeightUpValue.BorderColor = Color.Maroon
            WeightValue.Points.Clear()
            WeightLowValue.Points.Clear()
            WeightUpValue.Points.Clear()
            For i = 1 To iShot
                WeightValue.Points.AddXY(i, 0)
                WeightLowValue.Points.AddXY(i, dLowLimtValue)
                WeightUpValue.Points.AddXY(i, dUpLimtValue)
            Next
            Chart_Weight_Value.Series.Add(WeightLowValue)
            Chart_Weight_Value.Series.Add(WeightValue)
            Chart_Weight_Value.Series.Add(WeightUpValue)
            Chart_Weight_Value.Series(1).ToolTip = "#VAL"
            Chart_Weight_Value.Series(0).ToolTip = "Low Limit:#VAL"
            Chart_Weight_Value.Series(2).ToolTip = "Up Limit:#VAL"

            Chart_Weight_Value.ChartAreas(0).AxisX.Interval = CInt(iShot / 10)
            Chart_Weight_Value.ChartAreas(0).AxisY.Interval = Double.Parse(((dUpLimtValue - dLowLimtValue) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisY.LabelStyle.Format = "N2"
            Chart_Weight_Value.ChartAreas(0).AxisY.Maximum = dUpLimtValue + Double.Parse(((dUpLimtValue - dLowLimtValue) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisY.Minimum = dLowLimtValue - Double.Parse(((dUpLimtValue - dLowLimtValue) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisX.Minimum = 1
            Chart_Weight_Value.ChartAreas(0).AxisX.Maximum = CInt(iShot)
            Chart_Weight_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine("GapFiller", "Shots")
            Chart_Weight_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine("GapFiller", "Weight")
            Chart_Weight_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
            Chart_Weight_Value.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub ChartWeight()
        Try
            Dim iShot As Integer = 0
            Dim dMin As Double = 0
            Dim dMax As Double = 0
            If HmiTextBox_Min.TextBox.Text = "" Then
                dMin = 0
            Else
                If Not IsNumeric(HmiTextBox_Min.TextBox.Text) Then
                    dMin = 0
                Else
                    dMin = Double.Parse(HmiTextBox_Min.TextBox.Text)
                End If

            End If

            If HmiTextBox_Max.TextBox.Text = "" Then
                dMax = 0
            Else
                If Not IsNumeric(HmiTextBox_Max.TextBox.Text) Then
                    dMax = 0
                Else
                    dMax = Double.Parse(HmiTextBox_Max.TextBox.Text)
                End If

            End If

            If dMin >= dMax Then
                dMax = dMin + 1
            End If

            If HmiTextBox_PerCycle.TextBox.Text = "" Then
                iShot = 50
            Else
                iShot = Integer.Parse(HmiTextBox_PerCycle.TextBox.Text)
            End If
            If iShot <= 0 Then
                iShot = 50
            End If

            Dim cCPK As clsCPK = New clsCPK(lListWeightValue, dMin, dMax)
            If cCPK.FindValue() Then
                HmiTextBox_MaxWeight.TextBox.Text = cCPK.Max.ToString("0.00")
                HmiTextBox_MinWeight.TextBox.Text = cCPK.Min.ToString("0.00")
                HmiTextBox_Weight2.TextBox.Text = cCPK.Mean.ToString("0.00")
                HmiTextBox_Std.TextBox.Text = cCPK.Std.ToString
                HmiTextBox_CP.TextBox.Text = cCPK.Cp.ToString
                HmiTextBox_CPK.TextBox.Text = cCPK.Cpk.ToString
            End If

            Chart_Weight_Value.Series().Clear()
            WeightValue.ChartType = SeriesChartType.Line
            WeightValue.BorderWidth = 1
            WeightValue.Name = "WeightValue"
            WeightValue.MarkerBorderColor = Color.Blue
            WeightValue.MarkerBorderWidth = 3
            WeightValue.MarkerColor = Color.White
            WeightValue.MarkerSize = 8
            WeightValue.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle

            WeightLowValue.ChartType = SeriesChartType.Line
            WeightLowValue.BorderWidth = 2
            WeightLowValue.BorderColor = Color.Red

            WeightUpValue.ChartType = SeriesChartType.Line
            WeightUpValue.BorderWidth = 2
            WeightUpValue.BorderColor = Color.Maroon
            WeightValue.Points.Clear()
            WeightLowValue.Points.Clear()
            WeightUpValue.Points.Clear()
            For i = 1 To iShot
                If i <= lListWeightValue.Count Then
                    If lListWeightValue(i - 1) < dMin - Double.Parse(((dMax - dMin) / 6.5).ToString("0.00")) Then
                        WeightValue.Points.AddXY(i, dMin - Double.Parse(((dMax - dMin) / 6.5).ToString("0.00")))
                    ElseIf lListWeightValue(i - 1) > dMax + Double.Parse(((dMax - dMin) / 6.5).ToString("0.00")) Then
                        WeightValue.Points.AddXY(i, dMax + Double.Parse(((dMax - dMin) / 6.5).ToString("0.00")))
                    Else
                        WeightValue.Points.AddXY(i, lListWeightValue(i - 1))
                    End If
                    ' Else
                    ' WeightValue.Points.AddXY(i, 0)
                End If
                WeightLowValue.Points.AddXY(i, dMin)
                WeightUpValue.Points.AddXY(i, dMax)
            Next
            Chart_Weight_Value.Series.Add(WeightLowValue)
            Chart_Weight_Value.Series.Add(WeightValue)
            Chart_Weight_Value.Series.Add(WeightUpValue)
            Chart_Weight_Value.Series(1).ToolTip = "#VAL"
            Chart_Weight_Value.Series(0).ToolTip = "Low Limit:#VAL"
            Chart_Weight_Value.Series(2).ToolTip = "Up Limit:#VAL"

            Chart_Weight_Value.ChartAreas(0).AxisX.Interval = CInt(iShot / 10)
            Chart_Weight_Value.ChartAreas(0).AxisY.Interval = Double.Parse(((dMax - dMin) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisY.LabelStyle.Format = "N2"
            Chart_Weight_Value.ChartAreas(0).AxisY.Maximum = dMax + Double.Parse(((dMax - dMin) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisY.Minimum = dMin - Double.Parse(((dMax - dMin) / 6.5).ToString("0.00"))
            Chart_Weight_Value.ChartAreas(0).AxisX.Minimum = 1
            Chart_Weight_Value.ChartAreas(0).AxisX.Maximum = CInt(iShot)
            Chart_Weight_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine("GapFiller", "Shots")
            Chart_Weight_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine("GapFiller", "Weight")
            Chart_Weight_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
            Chart_Weight_Value.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub CheckBox_AutoWeight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CheckBox_AutoWeight.Checked Then
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMAutoWeight", True)
        Else
            HmiTextBox_AutoWeightNo.TextBoxReadOnly = True
            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].fdHMAutoWeight", False)
        End If
        cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + "GapFiller" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", sender.name + Index.ToString, CheckBox_AutoWeight.Checked.ToString)
    End Sub


    Public Sub RefreshUI()
        Select Case iStep
            Case 1
                TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                iStep = iStep + 1
            Case 2
                mMainForm.InvokeAction(Sub()
                                           HmiTextBox_Max.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax.ToString
                                           HmiTextBox_Min.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin.ToString
                                           HmiTextBox_PreShot.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepShots.ToString
                                           HmiTextBox_PreShotTime.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepShot.ToString
                                           HmiTextBox_Pause.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIPrepPause.ToString
                                           HmiTextBox_PerCycle.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIShotsPreCycle.ToString
                                           HmiTextBox_DispensingTime.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIDispensing.ToString
                                           HmiTextBox_DispensingPause.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIDispensingPause.ToString
                                           If TempStructGapFiller.PLC_Weight(Index - 1).fdHMAutoWeight Then
                                               CheckBox_AutoWeight.Checked = True
                                               HmiTextBox_AutoWeightNo.TextBoxReadOnly = False
                                           Else
                                               CheckBox_AutoWeight.Checked = False
                                               HmiTextBox_AutoWeightNo.TextBoxReadOnly = True
                                           End If
                                           HmiTextBox_AutoWeightNo.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdHMAutoWeightNr.ToString
                                       End Sub)
                iStep = iStep + 1

            Case 3
                iStep = iStep + 1

            Case 4
                TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))
                If TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle <> OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle Or TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle <> OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle Then
                    mMainForm.InvokeAction(Sub()
                                               HmiTextBox_PerCycle2.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle.ToString
                                               HmiTextBox_InCycle.TextBox.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle.ToString
                                           End Sub)

                End If

                If TempStructGapFiller.PLC_Weight(Index - 1).bulPLCCupPresent <> OldStructGapFiller.PLC_Weight(Index - 1).bulPLCCupPresent Then
                    mMainForm.InvokeAction(Sub()
                                               Panel_Indicate_Cup.SetIndicateBackColor(TempStructGapFiller.PLC_Weight(Index - 1).bulPLCCupPresent)
                                           End Sub)

                End If

                If TempStructGapFiller.PLC_Weight(Index - 1).bulPLCWeightResult <> OldStructGapFiller.PLC_Weight(Index - 1).bulPLCWeightResult Then
                    mMainForm.InvokeAction(Sub()
                                               Panel_Result.SetIndicateBackColor(TempStructGapFiller.PLC_Weight(Index - 1).bulPLCWeightResult)
                                           End Sub)
                End If

                If TempStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight <> OldStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight Then
                    mMainForm.InvokeAction(Sub()
                                               HmiSensor_Active.SetIndicateBackColor(TempStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight)
                                           End Sub)
                End If

                If TempStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentWeight <> OldStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentWeight Then
                    mMainForm.InvokeAction(Sub()
                                               Label_Weight.Text = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentWeight.ToString
                                           End Sub)
                End If

                If TempStructGapFiller.PLC_Weight(Index - 1).fdHMIStartWeight <> OldStructGapFiller.PLC_Weight(Index - 1).fdHMIStartWeight Then
                    mMainForm.InvokeAction(Sub()
                                               HmiButtonWithIndicate_Start.SetIndicateBackColor(TempStructGapFiller.PLC_Weight(Index - 1).fdHMIStartWeight)
                                           End Sub)
                End If

                '称重
                If TempStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight Then
                    If TempStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight And Not OldStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight Then
                        lListWeightValue.Clear()
                        mMainForm.InvokeAction(Sub()
                                                   SetTitle()
                                               End Sub)

                    End If
                    For i = 1 To TempStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentIndex
                        If OldStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsPass <> TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsPass And TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsPass And Not TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulHMIDoaction Then
                            Dim strResult As String = ""
                            strResult = "PASS"
                            Panel_Result.SetIndicateBackColor(True)
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].PLC_WeightPoint[" + i.ToString + "].bulHMIDoaction", True)
                            If Not TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulHMIDoaction Then cGapFillerDataManager.InSertData(ePageType.ToString, i.ToString, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax, strResult, Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            iCnt = i
                            If bReadOnly Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg("1", cLanguageManager.GetUserTextLine("GapFiller", "36", ePageType.ToString, iCnt, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax), enumMainTipsManagerType.Normal))
                            End If

                            lListWeightValue.Add(TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).fdPLCCurrentWeight)
                            mMainForm.InvokeAction(Sub()
                                                       ChartWeight()
                                                       If lListWeightValue.Count >= 1 Then
                                                           HmiTextBox_LastWeight.TextBox.Text = lListWeightValue(lListWeightValue.Count - 1).ToString("0.00")
                                                       Else
                                                           HmiTextBox_LastWeight.TextBox.Text = "0.00"
                                                       End If
                                                   End Sub)
                        End If

                        OldStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsPass = TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsPass
                        If OldStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsFail <> TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsFail And TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsFail And Not TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulHMIDoaction Then
                            Dim strResult As String = ""
                            strResult = "FAIL"
                            Panel_Result.SetIndicateBackColor(True)
                            cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].PLC_WeightPoint[" + i.ToString + "].bulHMIDoaction", True)
                            If Not TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulHMIDoaction Then cGapFillerDataManager.InSertData(ePageType.ToString, i.ToString, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax, strResult, Now.ToString("yyyy-MM-dd HH:mm:ss"))
                            iCnt = i
                            If bReadOnly Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg("1", cLanguageManager.GetUserTextLine("GapFiller", "36", ePageType.ToString, iCnt, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax), enumMainTipsManagerType.Alarm))
                            End If

                            lListWeightValue.Add(TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).fdPLCCurrentWeight)
                            mMainForm.InvokeAction(Sub()
                                                       ChartWeight()
                                                       If lListWeightValue.Count >= 1 Then
                                                           HmiTextBox_LastWeight.TextBox.Text = lListWeightValue(lListWeightValue.Count - 1).ToString("0.00")
                                                       Else
                                                           HmiTextBox_LastWeight.TextBox.Text = "0.00"
                                                       End If
                                                   End Sub)
                        End If
                        OldStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsFail = TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(i - 1).bulPLCIsFail
                    Next
                End If
                OldStructGapFiller.PLC_Weight(Index - 1).fdHMIStartWeight = TempStructGapFiller.PLC_Weight(Index - 1).fdHMIStartWeight
                OldStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCStartWeight
                OldStructGapFiller.PLC_Weight(Index - 1).bulPLCCupPresent = TempStructGapFiller.PLC_Weight(Index - 1).bulPLCCupPresent
                OldStructGapFiller.PLC_Weight(Index - 1).bulPLCWeightResult = TempStructGapFiller.PLC_Weight(Index - 1).bulPLCWeightResult
                OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsPreCycle
                OldStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCShotsInCycle.ToString
                OldStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentWeight = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentWeight
                OldStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentIndex = TempStructGapFiller.PLC_Weight(Index - 1).fdPLCCurrentIndex

            Case 10
                iStep = iStep + 1

            Case 11
                If bReadOnly Then
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("1", cLanguageManager.GetUserTextLine("GapFiller", "31", ePageType.ToString, iCnt, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax), enumMainTipsManagerType.Alarm))
                Else
                    ' cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "31", ePageType.ToString, iCnt, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax), enumExceptionType.Alarm, ControlUI.FormName))
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("1", cLanguageManager.GetUserTextLine("GapFiller", "31", ePageType.ToString, iCnt, TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).fdPLCCurrentWeight, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMin, TempStructGapFiller.PLC_Weight(Index - 1).fdHMIWeightMax), enumMainTipsManagerType.Alarm))
                End If

                iStep = iStep + 1

            Case 12
                cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].PLC_WeightPoint[" + (iCnt).ToString + "].bulHMIDoaction", True)
                iStep = iStep + 1

            Case 13
                iStep = iStep + 1

            Case 14
                TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                If Not TempStructGapFiller.PLC_Weight(Index - 1).PLC_WeightPoint(iCnt - 1).bulHMIDoaction Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].PLC_WeightPoint[" + (iCnt).ToString + "].bulPLCIsFail", False)
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".PLC_Weight[" + iIndex.ToString + "].PLC_WeightPoint[" + (iCnt).ToString + "].bulPLCIsPass", False)
                    lListWeightValue.RemoveAt(lListWeightValue.Count - 1)
                    iStep = 4
                End If

        End Select
    End Sub

    Public Sub StopReflesh()
        'iStep = 1
        cChangePage.BackPage()
    End Sub
    Public Sub StartReflesh()
        'iStep = 1
    End Sub
End Class

Public Enum enumPageType
    A = 0
    B
    AB
End Enum