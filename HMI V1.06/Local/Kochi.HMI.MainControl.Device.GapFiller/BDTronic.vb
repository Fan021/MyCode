Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing
Imports System.Threading
Imports System.IO
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice

Public Class BDTronic
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
    Private cMachineManager As clsMachineManager
    Private cMainTipsManager As clsMainTipsManager
    Private iStep As Integer = 1
    Public Const FormName As String = "GapFillerControlUI"
    Private strDeviceType As String = ""
    Private strDeviceIndex As String = ""
    Private cDeviceCfg As clsDeviceCfg
    Private cDeviceCfgA As clsDeviceCfg
    Private cDeviceCfgB As clsDeviceCfg
    Private strResult As String = ""
    Private cPLCAction As clsPLCAction
    Private strTriggerType As String = ""
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private bReset As Boolean = False
    Protected cLogHandler As clsLogHandler
    Private iStepA As Integer = 1
    Private iStepB As Integer = 1
    Private bLock As Boolean = False
    Private strHUA As String = ""
    Private strHUB As String = ""
    Private strErrorMessage As String = ""
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
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDeviceCfg = cDeviceManager.GetDeviceFromName(cGapFiller.Name)
            cPLCAction = New clsPLCAction
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()
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

        RemoveHandler HmiTextBox_PPSOP_Mode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_B2000Position.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_B2000OP_Mode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        RemoveHandler HmiTextBox_B2000RecipeNumber.TextBox.TextChanged, AddressOf TextBox_TextChanged

        RemoveHandler HmiTextBox_PPSFillingLevelP1.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        RemoveHandler HmiButtonWithIndicate_PPSHS_MESokA.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_PPSHS_MESokB.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000Start.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000Filling.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000Cleaning.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000Material_OK.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_PPSHS_OPMode1.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_PPSHS_OPMode2.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_PPSHS_OPMode3.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000HS_OPMode1.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000HS_OPMode2.Click, AddressOf Button_Click
        RemoveHandler HmiButtonWithIndicate_B2000HS_OPMode3.Click, AddressOf Button_Click

        GroupBox_PPS.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_PPS")
        GroupBox_PPS.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        GroupBox_B2000.Text = cLanguageManager.GetUserTextLine("GapFiller", "GroupBox_B2000")
        GroupBox_B2000.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiLabel1.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel1")
        HmiLabel1.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel1.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel3.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel3")
        HmiLabel3.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSactOP_Mode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSactOP_Mode.TextBoxReadOnly = True
        HmiLabel3.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel4.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel4")
        HmiLabel4.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSactUser_Level.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSactUser_Level.TextBoxReadOnly = True
        HmiLabel4.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel8.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel8")
        HmiLabel8.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSFillingLevelP1.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSFillingLevelP1.TextBoxReadOnly = True
        HmiLabel8.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel2.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel2")
        HmiLabel2.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSFillingLevelP2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSFillingLevelP2.TextBoxReadOnly = True
        HmiLabel2.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel5.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel5")
        HmiLabel5.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSSupplyPressureP1.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSSupplyPressureP1.TextBoxReadOnly = True
        HmiLabel5.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel6.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel6")
        HmiLabel6.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSSupplyPressureP2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSSupplyPressureP2.TextBoxReadOnly = True
        HmiLabel6.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel7.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel7")
        HmiLabel7.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSPressureP1Outlet.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSPressureP1Outlet.TextBoxReadOnly = True
        HmiLabel7.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel9.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel9")
        HmiLabel9.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSPressureP2Outlet.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSPressureP2Outlet.TextBoxReadOnly = True
        HmiLabel9.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel10.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel10")
        HmiLabel10.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSOP_Mode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel10.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel11.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel11")
        HmiLabel11.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel11.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel12.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel12")
        HmiLabel12.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel12.Label.TextAlign = ContentAlignment.MiddleRight

        HmiButtonWithIndicate_PPSHS_MESokA.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_PPSHS_MESokA")
        HmiButtonWithIndicate_PPSHS_MESokA.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_PPSHS_MESokB.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_PPSHS_MESokB")
        HmiButtonWithIndicate_PPSHS_MESokB.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_PPSHS_OPMode1.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_PPSHS_OPMode1")
        HmiButtonWithIndicate_PPSHS_OPMode1.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_PPSHS_OPMode2.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_PPSHS_OPMode2")
        HmiButtonWithIndicate_PPSHS_OPMode2.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_PPSHS_OPMode3.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_PPSHS_OPMode3")
        HmiButtonWithIndicate_PPSHS_OPMode3.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiLabel13.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel13")
        HmiLabel13.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel13.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrPartNoA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrPartNoA.TextBoxReadOnly = True

        HmiLabel14.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel14")
        HmiLabel14.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel14.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrVolumeA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrVolumeA.TextBoxReadOnly = True

        HmiLabel15.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel15")
        HmiLabel15.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel15.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrExpiryDateA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrExpiryDateA.TextBoxReadOnly = True

        HmiLabel16.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel16")
        HmiLabel16.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel16.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrBatchNoA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrBatchNoA.TextBoxReadOnly = True

        HmiLabel17.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel17")
        HmiLabel17.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel17.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrSupplierNoA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrSupplierNoA.TextBoxReadOnly = True

        HmiLabel18.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel18")
        HmiLabel18.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel18.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrPackagingNoA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrPackagingNoA.TextBoxReadOnly = True

        HmiLabel19.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel19")
        HmiLabel19.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel19.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrPartNoB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrPartNoB.TextBoxReadOnly = True

        HmiLabel20.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel20")
        HmiLabel20.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel20.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrVolumeB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrVolumeB.TextBoxReadOnly = True

        HmiLabel21.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel21")
        HmiLabel21.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel21.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrExpiryDateB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrExpiryDateB.TextBoxReadOnly = True

        HmiLabel22.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel22")
        HmiLabel22.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel22.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrBatchNoB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrBatchNoB.TextBoxReadOnly = True

        HmiLabel23.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel23")
        HmiLabel23.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel23.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrSupplierNoB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrSupplierNoB.TextBoxReadOnly = True

        HmiLabel24.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel24")
        HmiLabel24.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel24.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_PPSstrPackagingNoB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_PPSstrPackagingNoB.TextBoxReadOnly = True


        HmiLabel28.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel28")
        HmiLabel28.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel28.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel27.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel27")
        HmiLabel27.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel27.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel26.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel26")
        HmiLabel26.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel26.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel25.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel25")
        HmiLabel25.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel25.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel29.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel29")
        HmiLabel29.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel29.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel30.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel30")
        HmiLabel30.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel30.Label.TextAlign = ContentAlignment.MiddleRight

        HmiLabel32.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel32")
        HmiLabel32.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel32.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000actOP_Mode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000actOP_Mode.TextBoxReadOnly = True

        HmiLabel33.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel33")
        HmiLabel33.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel33.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000requestPostiton.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000requestPostiton.TextBoxReadOnly = True

        HmiLabel34.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel34")
        HmiLabel34.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel34.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000actRecipe.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000actRecipe.TextBoxReadOnly = True

        HmiLabel31.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel31")
        HmiLabel31.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel31.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000actRecipe.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000actRecipe.TextBoxReadOnly = True

        HmiLabel49.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel49")
        HmiLabel49.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel49.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000actUserLevel.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000actUserLevel.TextBoxReadOnly = True

        HmiLabel34.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel34")
        HmiLabel34.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel34.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000FillingLevel1.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000FillingLevel1.TextBoxReadOnly = True

        HmiLabel50.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel50")
        HmiLabel50.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel50.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000FillingLevel2.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_B2000FillingLevel2.TextBoxReadOnly = True

        HmiTextBox_MES_StatusA.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_MES_StatusA.TextBoxReadOnly = True


        HmiTextBox_MES_StatusB.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiTextBox_MES_StatusB.TextBoxReadOnly = True

        HmiButtonWithIndicate_B2000HS_OPMode1.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000HS_OPMode1")
        HmiButtonWithIndicate_B2000HS_OPMode1.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_B2000HS_OPMode2.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000HS_OPMode2")
        HmiButtonWithIndicate_B2000HS_OPMode2.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_B2000HS_OPMode3.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000HS_OPMode3")
        HmiButtonWithIndicate_B2000HS_OPMode3.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiButtonWithIndicate_B2000Start.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000Start")
        HmiButtonWithIndicate_B2000Start.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_B2000Filling.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000Filling")
        HmiButtonWithIndicate_B2000Filling.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_B2000Cleaning.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000Cleaning")
        HmiButtonWithIndicate_B2000Cleaning.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiButtonWithIndicate_B2000Material_OK.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiButtonWithIndicate_B2000Material_OK")
        HmiButtonWithIndicate_B2000Material_OK.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiLabel41.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel41")
        HmiLabel41.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel41.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000Position.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiLabel37.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel37")
        HmiLabel37.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel37.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000OP_Mode.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        HmiLabel38.Label.Text = cLanguageManager.GetUserTextLine("GapFiller", "HmiLabel38")
        HmiLabel38.Label.Font = New System.Drawing.Font("Calibri", iFontSize - 2)
        HmiLabel38.Label.TextAlign = ContentAlignment.MiddleRight
        HmiTextBox_B2000RecipeNumber.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize - 2)

        If Not bReset Then
            HmiTextBox_PPSactOP_Mode.TextBox.Text = OldStructGapFiller.bulPLCPPSactOP_Mode.ToString
            HmiTextBox_PPSactUser_Level.TextBox.Text = OldStructGapFiller.bulPLCPPSactUser_Level.ToString
            HmiTextBox_PPSFillingLevelP1.TextBox.Text = OldStructGapFiller.bulPLCPPSFillingLevelP1.ToString
            HmiTextBox_PPSFillingLevelP2.TextBox.Text = OldStructGapFiller.bulPLCPPSFillingLevelP2.ToString
            HmiTextBox_PPSSupplyPressureP1.TextBox.Text = OldStructGapFiller.bulPLCPPSSupplyPressureP1.ToString
            HmiTextBox_PPSSupplyPressureP2.TextBox.Text = OldStructGapFiller.bulPLCPPSSupplyPressureP2.ToString
            HmiTextBox_PPSPressureP1Outlet.TextBox.Text = OldStructGapFiller.bulPLCPPSPressureP1Outlet.ToString
            HmiTextBox_PPSPressureP2Outlet.TextBox.Text = OldStructGapFiller.bulPLCPPSPressureP2Outlet.ToString
            HmiTextBox_PPSOP_Mode.TextBox.Text = OldStructGapFiller.bulHMIPPSOP_Mode.ToString
            HmiTextBox_B2000actOP_Mode.TextBox.Text = OldStructGapFiller.bulPLCB2000actOP_Mode.ToString
            HmiTextBox_B2000requestPostiton.TextBox.Text = OldStructGapFiller.bulPLCB2000requestPostiton.ToString
            HmiTextBox_B2000actRecipe.TextBox.Text = OldStructGapFiller.bulPLCB2000actRecipe.ToString

            HmiTextBox_B2000Position.TextBox.Text = OldStructGapFiller.bulHMIB2000Position.ToString
            HmiTextBox_B2000OP_Mode.TextBox.Text = OldStructGapFiller.bulHMIB2000OP_Mode.ToString
            HmiTextBox_B2000RecipeNumber.TextBox.Text = OldStructGapFiller.bulHMIB2000RecipeNumber.ToString
            bLock = True
            UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), "A")
            UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), "B")
            bLock = False
            bReset = True
        End If

        'If bReadOnly Then


        '    HmiTextBox_PPSOP_Mode.TextBoxReadOnly = True
        '    HmiButtonWithIndicate_PPSHS_MESokA.Enabled = False
        '    HmiButtonWithIndicate_PPSHS_MESokB.Enabled = False
        '    HmiButtonWithIndicate_B2000Start.Enabled = False
        '    HmiButtonWithIndicate_B2000Filling.Enabled = False
        '    HmiButtonWithIndicate_B2000Cleaning.Enabled = False
        '    HmiButtonWithIndicate_B2000Material_OK.Enabled = False
        '    HmiTextBox_B2000Position.TextBoxReadOnly = True
        '    HmiTextBox_B2000OP_Mode.TextBoxReadOnly = True
        '    HmiTextBox_B2000RecipeNumber.TextBoxReadOnly = True
        '    HmiButtonWithIndicate_PPSHS_OPMode1.Enabled = False
        '    HmiButtonWithIndicate_PPSHS_OPMode2.Enabled = False
        '    HmiButtonWithIndicate_PPSHS_OPMode3.Enabled = False
        '    HmiButtonWithIndicate_B2000HS_OPMode1.Enabled = False
        '    HmiButtonWithIndicate_B2000HS_OPMode2.Enabled = False
        '    HmiButtonWithIndicate_B2000HS_OPMode3.Enabled = False
        'Else
        HmiTextBox_PPSOP_Mode.TextBoxReadOnly = False
        HmiButtonWithIndicate_PPSHS_MESokA.Enabled = True
        HmiButtonWithIndicate_PPSHS_MESokB.Enabled = True
        HmiButtonWithIndicate_B2000Start.Enabled = True
        HmiButtonWithIndicate_B2000Filling.Enabled = True
        HmiButtonWithIndicate_B2000Cleaning.Enabled = True
        HmiButtonWithIndicate_B2000Material_OK.Enabled = True
        HmiTextBox_B2000Position.TextBoxReadOnly = False
        HmiTextBox_B2000OP_Mode.TextBoxReadOnly = False
        HmiTextBox_B2000RecipeNumber.TextBoxReadOnly = False
        HmiButtonWithIndicate_PPSHS_OPMode1.Enabled = True
        HmiButtonWithIndicate_PPSHS_OPMode2.Enabled = True
        HmiButtonWithIndicate_PPSHS_OPMode3.Enabled = True
        HmiButtonWithIndicate_B2000HS_OPMode1.Enabled = True
        HmiButtonWithIndicate_B2000HS_OPMode2.Enabled = True
        HmiButtonWithIndicate_B2000HS_OPMode3.Enabled = True

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Engineer Then
            HmiButtonWithIndicate_PPSHS_OPMode1.Enabled = False
            HmiButtonWithIndicate_PPSHS_OPMode2.Enabled = False
            HmiButtonWithIndicate_PPSHS_OPMode3.Enabled = False
            HmiButtonWithIndicate_B2000HS_OPMode1.Enabled = False
            HmiButtonWithIndicate_B2000HS_OPMode2.Enabled = False
            HmiButtonWithIndicate_B2000HS_OPMode3.Enabled = False
            HmiButtonWithIndicate_PPSHS_MESokA.Enabled = False
            HmiButtonWithIndicate_PPSHS_MESokB.Enabled = False
            HmiButtonWithIndicate_B2000Start.Enabled = False
            HmiButtonWithIndicate_B2000Filling.Enabled = False
            HmiButtonWithIndicate_B2000Cleaning.Enabled = False
        End If
        ' End If

        HmiTextBox_PPSOP_Mode.ValueType = GetType(Integer)
        HmiTextBox_B2000Position.ValueType = GetType(Integer)
        HmiTextBox_B2000OP_Mode.ValueType = GetType(Integer)
        HmiTextBox_B2000RecipeNumber.ValueType = GetType(Integer)

        Dim strDevice As String = cMachineManager.DeviceParameterManager.GetParameterDevice("GapFiller", cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationID, 0)
        If strDevice <> "" Then
            strDeviceType = strDevice.Split("-")(0)
            strDeviceIndex = strDevice.Split("-")(1)
        End If

        AddHandler HmiTextBox_PPSOP_Mode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_B2000Position.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_B2000OP_Mode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_B2000RecipeNumber.TextBox.TextChanged, AddressOf TextBox_TextChanged

        AddHandler HmiTextBox_PPSFillingLevelP1.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiButtonWithIndicate_PPSHS_MESokA.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_PPSHS_MESokB.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000Start.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000Filling.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000Cleaning.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000Material_OK.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_PPSHS_OPMode1.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_PPSHS_OPMode2.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_PPSHS_OPMode3.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000HS_OPMode1.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000HS_OPMode2.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_B2000HS_OPMode3.Click, AddressOf Button_Click

        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButtonWithIndicate_PPSHS_MESokA"
                Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                If dOldValue = 0 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
                End If
                If dOldValue = 1 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
                End If
                If dOldValue = 2 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                End If
                If dOldValue = 3 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
                End If

            Case "HmiButtonWithIndicate_PPSHS_MESokB"
                Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                If dOldValue = 0 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
                End If
                If dOldValue = 1 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                End If
                If dOldValue = 2 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
                End If
                If dOldValue = 3 Then
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
                End If

            Case "HmiButtonWithIndicate_B2000Start"
                Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIB2000Start", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000Start", Not dOldValue)

            Case "HmiButtonWithIndicate_B2000Filling"
                Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIB2000Filling", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000Filling", Not dOldValue)

            Case "HmiButtonWithIndicate_B2000Cleaning"
                Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIB2000Cleaning", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000Cleaning", Not dOldValue)

            Case "HmiButtonWithIndicate_B2000Material_OK"
                Dim dOldValue As Boolean = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIB2000Material_OK", GetType(Boolean))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000Material_OK", Not dOldValue)
            Case "HmiButtonWithIndicate_PPSHS_OPMode1"
                HmiTextBox_PPSOP_Mode.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSOP_Mode", Byte.Parse(HmiTextBox_PPSOP_Mode.TextBox.Text))
            Case "HmiButtonWithIndicate_PPSHS_OPMode2"
                HmiTextBox_PPSOP_Mode.TextBox.Text = "1"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSOP_Mode", Byte.Parse(HmiTextBox_PPSOP_Mode.TextBox.Text))
            Case "HmiButtonWithIndicate_PPSHS_OPMode3"
                HmiTextBox_PPSOP_Mode.TextBox.Text = "3"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSOP_Mode", Byte.Parse(HmiTextBox_PPSOP_Mode.TextBox.Text))
            Case "HmiButtonWithIndicate_B2000HS_OPMode1"
                HmiTextBox_B2000OP_Mode.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000OP_Mode", Byte.Parse(HmiTextBox_B2000OP_Mode.TextBox.Text))
            Case "HmiButtonWithIndicate_B2000HS_OPMode2"
                HmiTextBox_B2000OP_Mode.TextBox.Text = "2"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000OP_Mode", Byte.Parse(HmiTextBox_B2000OP_Mode.TextBox.Text))
            Case "HmiButtonWithIndicate_B2000HS_OPMode3"
                HmiTextBox_B2000OP_Mode.TextBox.Text = "3"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000OP_Mode", Byte.Parse(HmiTextBox_B2000OP_Mode.TextBox.Text))

        End Select
    End Sub

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub


    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In HmiTableLayoutPanel1.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_PPSFillingLevelP1.TextBox.Height + 6
        Next
        For Each element As RowStyle In HmiTableLayoutPanel2.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_PPSFillingLevelP1.TextBox.Height + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_PPSOP_Mode"
                If HmiTextBox_PPSOP_Mode.TextBox.Text = "" Then HmiTextBox_PPSOP_Mode.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSOP_Mode", Byte.Parse(HmiTextBox_PPSOP_Mode.TextBox.Text))
            Case "HmiTextBox_B2000Position"
                If HmiTextBox_B2000Position.TextBox.Text = "" Then HmiTextBox_B2000Position.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000Position", Byte.Parse(HmiTextBox_B2000Position.TextBox.Text))
            Case "HmiTextBox_B2000OP_Mode"
                If HmiTextBox_B2000OP_Mode.TextBox.Text = "" Then HmiTextBox_B2000OP_Mode.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000OP_Mode", Byte.Parse(HmiTextBox_B2000OP_Mode.TextBox.Text))
            Case "HmiTextBox_B2000RecipeNumber"
                If HmiTextBox_B2000RecipeNumber.TextBox.Text = "" Then HmiTextBox_B2000RecipeNumber.TextBox.Text = "0"
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIB2000RecipeNumber", Byte.Parse(HmiTextBox_B2000RecipeNumber.TextBox.Text))
        End Select
    End Sub


    Public Sub RefreshUI()
        Try
            Select Case iStep

                Case 1
                    iStep = iStep + 1

                Case 2
                    TempStructGapFiller = cHMIPLC.ReadAny(lListInitParameter(0), GetType(StructGapFiller))
                    iStep = iStep + 1

                Case 3
                    HmiTextBox_PPSOP_Mode.TextBox.Text = TempStructGapFiller.bulHMIPPSOP_Mode
                    HmiTextBox_B2000Position.TextBox.Text = TempStructGapFiller.bulHMIB2000Position
                    HmiTextBox_B2000OP_Mode.TextBox.Text = TempStructGapFiller.bulHMIB2000OP_Mode
                    HmiTextBox_B2000RecipeNumber.TextBox.Text = TempStructGapFiller.bulHMIB2000RecipeNumber
                    HmiButtonWithIndicate_PPSHS_OPMode1.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 0, True, False))
                    HmiButtonWithIndicate_PPSHS_OPMode2.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 1, True, False))
                    HmiButtonWithIndicate_PPSHS_OPMode3.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 3, True, False))
                    HmiButtonWithIndicate_B2000HS_OPMode1.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 0, True, False))
                    HmiButtonWithIndicate_B2000HS_OPMode2.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 2, True, False))
                    HmiButtonWithIndicate_B2000HS_OPMode3.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 3, True, False))
                    System.Threading.Thread.Sleep(500)
                    iStep = iStep + 1

                Case 4
                    TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))


                    If TempStructGapFiller.bulPLCPPSHandShake_active <> OldStructGapFiller.bulPLCPPSHandShake_active Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_PPS_Active.SetIndicateBackColor(TempStructGapFiller.bulPLCPPSHandShake_active)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSHandShake_active = TempStructGapFiller.bulPLCPPSHandShake_active

                    '2
                    If TempStructGapFiller.bulPLCPPSactOP_Mode <> OldStructGapFiller.bulPLCPPSactOP_Mode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSactOP_Mode.TextBox.Text = TempStructGapFiller.bulPLCPPSactOP_Mode.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSactOP_Mode = TempStructGapFiller.bulPLCPPSactOP_Mode

                    '3
                    If TempStructGapFiller.bulPLCPPSactUser_Level <> OldStructGapFiller.bulPLCPPSactUser_Level Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSactUser_Level.TextBox.Text = TempStructGapFiller.bulPLCPPSactUser_Level.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSactUser_Level = TempStructGapFiller.bulPLCPPSactUser_Level

                    '4
                    If TempStructGapFiller.bulPLCPPSFillingLevelP1 <> OldStructGapFiller.bulPLCPPSFillingLevelP1 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSFillingLevelP1.TextBox.Text = TempStructGapFiller.bulPLCPPSFillingLevelP1.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSFillingLevelP1 = TempStructGapFiller.bulPLCPPSFillingLevelP1

                    '5
                    If TempStructGapFiller.bulPLCPPSFillingLevelP2 <> OldStructGapFiller.bulPLCPPSFillingLevelP2 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSFillingLevelP2.TextBox.Text = TempStructGapFiller.bulPLCPPSFillingLevelP2.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSFillingLevelP2 = TempStructGapFiller.bulPLCPPSFillingLevelP2

                    '6
                    If TempStructGapFiller.bulPLCPPSSupplyPressureP1 <> OldStructGapFiller.bulPLCPPSSupplyPressureP1 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSSupplyPressureP1.TextBox.Text = TempStructGapFiller.bulPLCPPSSupplyPressureP1.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSSupplyPressureP1 = TempStructGapFiller.bulPLCPPSSupplyPressureP1

                    '7
                    If TempStructGapFiller.bulPLCPPSSupplyPressureP2 <> OldStructGapFiller.bulPLCPPSSupplyPressureP2 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSSupplyPressureP2.TextBox.Text = TempStructGapFiller.bulPLCPPSSupplyPressureP2.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSSupplyPressureP2 = TempStructGapFiller.bulPLCPPSSupplyPressureP2

                    '8
                    If TempStructGapFiller.bulPLCPPSPressureP1Outlet <> OldStructGapFiller.bulPLCPPSPressureP1Outlet Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSPressureP1Outlet.TextBox.Text = TempStructGapFiller.bulPLCPPSPressureP1Outlet.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSPressureP1Outlet = TempStructGapFiller.bulPLCPPSPressureP1Outlet


                    '9
                    If TempStructGapFiller.bulPLCPPSPressureP2Outlet <> OldStructGapFiller.bulPLCPPSPressureP2Outlet Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSPressureP2Outlet.TextBox.Text = TempStructGapFiller.bulPLCPPSPressureP2Outlet.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSPressureP2Outlet = TempStructGapFiller.bulPLCPPSPressureP2Outlet

                    '10
                    If TempStructGapFiller.bulPLCPPSscanProcessReadyMES <> OldStructGapFiller.bulPLCPPSscanProcessReadyMES Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_PPSscanProcessReadyMESA.SetIndicateBackColor(IIf(TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 Or TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3, True, False))
                                                   HmiSensor_PPSscanProcessReadyMESB.SetIndicateBackColor(IIf(TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 Or TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3, True, False))
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSscanProcessReadyMES = TempStructGapFiller.bulPLCPPSscanProcessReadyMES

                    '11
                    If TempStructGapFiller.bulHMIPPSHS_MESok <> OldStructGapFiller.bulHMIPPSHS_MESok Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_PPSHS_MESokA.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSHS_MESok = 1 Or TempStructGapFiller.bulHMIPPSHS_MESok = 3, True, False))
                                                   HmiButtonWithIndicate_PPSHS_MESokB.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSHS_MESok = 2 Or TempStructGapFiller.bulHMIPPSHS_MESok = 3, True, False))
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIPPSHS_MESok = TempStructGapFiller.bulHMIPPSHS_MESok

                    '12
                    If TempStructGapFiller.bulPLCPPSstrPartNoA <> OldStructGapFiller.bulPLCPPSstrPartNoA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrPartNoA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrPartNoA.ToString
                                                   cGapFiller.PPSstrPartNoA = TempStructGapFiller.bulPLCPPSstrPartNoA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrPartNoA = TempStructGapFiller.bulPLCPPSstrPartNoA


                    '13
                    If TempStructGapFiller.bulPLCPPSstrVolumeA <> OldStructGapFiller.bulPLCPPSstrVolumeA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrVolumeA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrVolumeA.ToString
                                                   cGapFiller.PPSstrVolumeA = TempStructGapFiller.bulPLCPPSstrVolumeA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrVolumeA = TempStructGapFiller.bulPLCPPSstrVolumeA

                    '14
                    If TempStructGapFiller.bulPLCPPSstrExpiryDateA <> OldStructGapFiller.bulPLCPPSstrExpiryDateA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrExpiryDateA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrExpiryDateA.ToString
                                                   cGapFiller.PPSstrExpiryDateA = TempStructGapFiller.bulPLCPPSstrExpiryDateA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrExpiryDateA = TempStructGapFiller.bulPLCPPSstrExpiryDateA

                    '15
                    If TempStructGapFiller.bulPLCPPSstrBatchNoA <> OldStructGapFiller.bulPLCPPSstrBatchNoA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrBatchNoA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrBatchNoA.ToString
                                                   cGapFiller.PPSstrBatchNoA = TempStructGapFiller.bulPLCPPSstrBatchNoA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrBatchNoA = TempStructGapFiller.bulPLCPPSstrBatchNoA

                    '16
                    If TempStructGapFiller.bulPLCPPSstrSupplierNoA <> OldStructGapFiller.bulPLCPPSstrSupplierNoA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrSupplierNoA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrSupplierNoA.ToString
                                                   cGapFiller.PPSstrSupplierNoA = TempStructGapFiller.bulPLCPPSstrSupplierNoA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrSupplierNoA = TempStructGapFiller.bulPLCPPSstrSupplierNoA

                    '17
                    If TempStructGapFiller.bulPLCPPSstrPackagingNoA <> OldStructGapFiller.bulPLCPPSstrPackagingNoA Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrPackagingNoA.TextBox.Text = TempStructGapFiller.bulPLCPPSstrPackagingNoA.ToString
                                                   cGapFiller.PPSstrPackagingNoA = TempStructGapFiller.bulPLCPPSstrPackagingNoA
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrPackagingNoA = TempStructGapFiller.bulPLCPPSstrPackagingNoA


                    '12
                    If TempStructGapFiller.bulPLCPPSstrPartNoB <> OldStructGapFiller.bulPLCPPSstrPartNoB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrPartNoB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrPartNoB.ToString
                                                   cGapFiller.PPSstrPartNoB = TempStructGapFiller.bulPLCPPSstrPartNoB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrPartNoB = TempStructGapFiller.bulPLCPPSstrPartNoB


                    '13
                    If TempStructGapFiller.bulPLCPPSstrVolumeB <> OldStructGapFiller.bulPLCPPSstrVolumeB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrVolumeB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrVolumeB.ToString
                                                   cGapFiller.PPSstrVolumeB = TempStructGapFiller.bulPLCPPSstrVolumeB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrVolumeB = TempStructGapFiller.bulPLCPPSstrVolumeB

                    '14
                    If TempStructGapFiller.bulPLCPPSstrExpiryDateB <> OldStructGapFiller.bulPLCPPSstrExpiryDateB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrExpiryDateB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrExpiryDateB.ToString
                                                   cGapFiller.PPSstrExpiryDateB = TempStructGapFiller.bulPLCPPSstrExpiryDateB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrExpiryDateB = TempStructGapFiller.bulPLCPPSstrExpiryDateB

                    '15
                    If TempStructGapFiller.bulPLCPPSstrBatchNoB <> OldStructGapFiller.bulPLCPPSstrBatchNoB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrBatchNoB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrBatchNoB.ToString
                                                   cGapFiller.PPSstrBatchNoB = TempStructGapFiller.bulPLCPPSstrBatchNoB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrBatchNoB = TempStructGapFiller.bulPLCPPSstrBatchNoB

                    '16
                    If TempStructGapFiller.bulPLCPPSstrSupplierNoB <> OldStructGapFiller.bulPLCPPSstrSupplierNoB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrSupplierNoB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrSupplierNoB.ToString
                                                   cGapFiller.PPSstrSupplierNoB = TempStructGapFiller.bulPLCPPSstrSupplierNoB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrSupplierNoB = TempStructGapFiller.bulPLCPPSstrSupplierNoB

                    '17
                    If TempStructGapFiller.bulPLCPPSstrPackagingNoB <> OldStructGapFiller.bulPLCPPSstrPackagingNoB Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_PPSstrPackagingNoB.TextBox.Text = TempStructGapFiller.bulPLCPPSstrPackagingNoB.ToString
                                                   cGapFiller.PPSstrPackagingNoB = TempStructGapFiller.bulPLCPPSstrPackagingNoB
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCPPSstrPackagingNoB = TempStructGapFiller.bulPLCPPSstrPackagingNoB

                    '18
                    If TempStructGapFiller.bulPLCB2000Ready <> OldStructGapFiller.bulPLCB2000Ready Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000Ready.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000Ready)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000Ready = TempStructGapFiller.bulPLCB2000Ready

                    '19
                    If TempStructGapFiller.bulPLCB2000Busy <> OldStructGapFiller.bulPLCB2000Busy Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000Busy.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000Busy)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000Busy = TempStructGapFiller.bulPLCB2000Busy

                    '20
                    If TempStructGapFiller.bulPLCB2000System_OK <> OldStructGapFiller.bulPLCB2000System_OK Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000System_OK.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000System_OK)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000System_OK = TempStructGapFiller.bulPLCB2000System_OK

                    '21
                    If TempStructGapFiller.bulPLCB2000ProcessCycle_OK <> OldStructGapFiller.bulPLCB2000ProcessCycle_OK Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000ProcessCycle_OK.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000ProcessCycle_OK)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000ProcessCycle_OK = TempStructGapFiller.bulPLCB2000ProcessCycle_OK

                    '22
                    If TempStructGapFiller.bulPLCB2000ProcessCycle_NOK <> OldStructGapFiller.bulPLCB2000ProcessCycle_NOK Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000ProcessCycle_NOK.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000ProcessCycle_NOK)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000ProcessCycle_NOK = TempStructGapFiller.bulPLCB2000ProcessCycle_NOK

                    '23
                    If TempStructGapFiller.bulPLCB2000Handshake_active <> OldStructGapFiller.bulPLCB2000Handshake_active Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiSensor_B2000Handshake_active.SetIndicateBackColor(TempStructGapFiller.bulPLCB2000Handshake_active)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000Handshake_active = TempStructGapFiller.bulPLCB2000Handshake_active

                    '24
                    If TempStructGapFiller.bulPLCB2000actOP_Mode <> OldStructGapFiller.bulPLCB2000actOP_Mode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000actOP_Mode.TextBox.Text = TempStructGapFiller.bulPLCB2000actOP_Mode.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000actOP_Mode = TempStructGapFiller.bulPLCB2000actOP_Mode
                    '25
                    If TempStructGapFiller.bulPLCB2000requestPostiton <> OldStructGapFiller.bulPLCB2000requestPostiton Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000requestPostiton.TextBox.Text = TempStructGapFiller.bulPLCB2000requestPostiton.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000requestPostiton = TempStructGapFiller.bulPLCB2000requestPostiton

                    '26
                    If TempStructGapFiller.bulPLCB2000actRecipe <> OldStructGapFiller.bulPLCB2000actRecipe Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000actRecipe.TextBox.Text = TempStructGapFiller.bulPLCB2000actRecipe.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000actRecipe = TempStructGapFiller.bulPLCB2000actRecipe

                    '27
                    If TempStructGapFiller.bulPLCB2000actUserLevel <> OldStructGapFiller.bulPLCB2000actUserLevel Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000actUserLevel.TextBox.Text = TempStructGapFiller.bulPLCB2000actUserLevel.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000actUserLevel = TempStructGapFiller.bulPLCB2000actUserLevel

                    '28
                    If TempStructGapFiller.bulPLCB2000FillingLevel1 <> OldStructGapFiller.bulPLCB2000FillingLevel1 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000FillingLevel1.TextBox.Text = TempStructGapFiller.bulPLCB2000FillingLevel1.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000FillingLevel1 = TempStructGapFiller.bulPLCB2000FillingLevel1

                    '29
                    If TempStructGapFiller.bulPLCB2000FillingLevel2 <> OldStructGapFiller.bulPLCB2000FillingLevel2 Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiTextBox_B2000FillingLevel2.TextBox.Text = TempStructGapFiller.bulPLCB2000FillingLevel2.ToString
                                               End Sub)
                    End If
                    OldStructGapFiller.bulPLCB2000FillingLevel2 = TempStructGapFiller.bulPLCB2000FillingLevel2

                    '30
                    If TempStructGapFiller.bulHMIB2000Start <> OldStructGapFiller.bulHMIB2000Start Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_B2000Start.SetIndicateBackColor(TempStructGapFiller.bulHMIB2000Start)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIB2000Start = TempStructGapFiller.bulHMIB2000Start

                    '31
                    If TempStructGapFiller.bulHMIB2000Filling <> OldStructGapFiller.bulHMIB2000Filling Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_B2000Filling.SetIndicateBackColor(TempStructGapFiller.bulHMIB2000Filling)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIB2000Filling = TempStructGapFiller.bulHMIB2000Filling

                    '32
                    If TempStructGapFiller.bulHMIB2000Cleaning <> OldStructGapFiller.bulHMIB2000Cleaning Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_B2000Cleaning.SetIndicateBackColor(TempStructGapFiller.bulHMIB2000Cleaning)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIB2000Cleaning = TempStructGapFiller.bulHMIB2000Cleaning

                    '33
                    If TempStructGapFiller.bulHMIB2000Material_OK <> OldStructGapFiller.bulHMIB2000Material_OK Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_B2000Material_OK.SetIndicateBackColor(TempStructGapFiller.bulHMIB2000Material_OK)
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIB2000Material_OK = TempStructGapFiller.bulHMIB2000Material_OK

                    '34
                    If TempStructGapFiller.bulHMIPPSOP_Mode <> OldStructGapFiller.bulHMIPPSOP_Mode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_PPSHS_OPMode1.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 0, True, False))
                                                   HmiButtonWithIndicate_PPSHS_OPMode2.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 1, True, False))
                                                   HmiButtonWithIndicate_PPSHS_OPMode3.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIPPSOP_Mode = 3, True, False))
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIPPSOP_Mode = TempStructGapFiller.bulHMIPPSOP_Mode

                    '35
                    If TempStructGapFiller.bulHMIB2000OP_Mode <> OldStructGapFiller.bulHMIB2000OP_Mode Then
                        mMainForm.InvokeAction(Sub()
                                                   HmiButtonWithIndicate_B2000HS_OPMode1.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 0, True, False))
                                                   HmiButtonWithIndicate_B2000HS_OPMode2.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 2, True, False))
                                                   HmiButtonWithIndicate_B2000HS_OPMode3.SetIndicateBackColor(IIf(TempStructGapFiller.bulHMIB2000OP_Mode = 3, True, False))
                                               End Sub)
                    End If
                    OldStructGapFiller.bulHMIB2000OP_Mode = TempStructGapFiller.bulHMIB2000OP_Mode


                    RefreshA()
                    RefreshB()

            End Select
        Catch ex As Exception
            If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, ControlUI.FormName))
        End Try

    End Sub

    Private Sub RefreshA()
        Dim strTriggerType As String = "A"

        Select Case iStepA
            Case 1
                If TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 Or TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                    If bReadOnly Then iStepA = iStepA + 1
                    Return
                End If

            Case 2
                UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "32"), strTriggerType)
                iStepA = iStepA + 1

            Case 3
                If strDeviceType = "" Then
                    iStepA = 10
                Else
                    iStepA = iStepA + 1
                End If

            Case 4
                cDeviceCfgA = cDeviceManager.GetDeviceFromTypeAndStationIndex(cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationID, strDeviceIndex, GetType(clsHMIMES))
                If IsNothing(cDeviceCfgA) Then
                    If bReadOnly Then
                        strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "33")
                        iStepA = iStepA + 2
                        Return
                    Else
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "33"), enumExceptionType.Alarm, ControlUI.FormName))
                        Return
                    End If
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "33"), strTriggerType)
                End If
                iStepA = iStepA + 1

            Case 5
                If strTriggerType = "A" Then
                    strHUA = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    UpdateMESMessage(strHUA, strTriggerType)
                    If Not CType(cDeviceCfgA.Source, clsHMIMES).SetupComponent(strHUA, strResult) Then
                        If bReadOnly Then
                            strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "35", strResult) + " " + strHUA
                            iStepA = iStepA + 1
                            Return
                        Else
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        ' UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), strTriggerType)
                    End If
                Else
                    strHUB = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    UpdateMESMessage(strHUB, strTriggerType)
                    If Not CType(cDeviceCfgB.Source, clsHMIMES).SetupComponent(strHUB, strResult) Then
                        If bReadOnly Then
                            strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "35", strResult) + " " + strHUB
                            iStepA = iStepA + 1
                            Return
                        Else
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        '   UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), strTriggerType)
                    End If
                End If

                iStepA = iStepA + 4

            Case 6
                If Not bLock Then
                    bLock = True
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("User", strTriggerType + ": " + strErrorMessage, enumMainTipsManagerType.Confirm))
                    UpdateMESMessage(strErrorMessage, strTriggerType)
                    iStepA = iStepA + 1
                End If

            Case 7
                If IsNothing(cDeviceCfg) Then
                    cPLCAction.HMIError(cHMIPLC, 1, cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromID(1).HMIError)
                Else
                    cPLCAction.HMIError(cHMIPLC, cDeviceCfg.StationID, cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromID(cDeviceCfg.StationID).HMIError)
                End If
                iStepA = iStepA + 1

            Case 8

                If strTriggerType = "A" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

                If strTriggerType = "B" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

                Dim strNowHU As String = ""
                If strTriggerType = "A" Then
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    If strNowHU <> strHUA Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                Else
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    If strNowHU <> strHUB Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

                If cMainTipsManager.GetMainTipsConfirmTypeFromKey("User") = enumMainTipsConfirmType.Continue Then
                    bLock = False
                    If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                    ResetHSMESOK(strTriggerType)
                    iStepA = 1
                End If

                If cMainTipsManager.GetMainTipsConfirmTypeFromKey("User") = enumMainTipsConfirmType.Abort Then
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "34"), strTriggerType)
                    ResetHSMESOK(strTriggerType)
                    bLock = False
                    iStepA = 10
                End If

            Case 9
                If strTriggerType = "A" Then
                    Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                    If dOldValue = 0 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
                    End If
                    If dOldValue = 2 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                    End If
                Else
                    Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                    If dOldValue = 0 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
                    End If
                    If dOldValue = 1 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                    End If
                End If
                UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "34"), strTriggerType)
                iStepA = iStepA + 1

            Case 10
                TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))

                Dim strNowHU As String = ""
                If strTriggerType = "A" Then
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    If strNowHU <> strHUA Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                Else
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    If strNowHU <> strHUB Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

                If strTriggerType = "A" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

                If strTriggerType = "B" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepA = 1
                    End If
                End If

        End Select
    End Sub


    Private Sub RefreshB()
        Dim strTriggerType As String = "B"
        Select Case iStepB
            Case 1
                If TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 Or TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                    If bReadOnly Then iStepB = iStepB + 1
                    Return
                End If


            Case 2
                UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "32"), strTriggerType)
                iStepB = iStepB + 1

            Case 3
                If strDeviceType = "" Then
                    iStepB = 10
                Else
                    iStepB = iStepB + 1
                End If

            Case 4
                cDeviceCfgB = cDeviceManager.GetDeviceFromTypeAndStationIndex(cDeviceManager.GetDeviceFromName(cGapFiller.Name).StationID, strDeviceIndex, GetType(clsHMIMES))
                If IsNothing(cDeviceCfgB) Then
                    If bReadOnly Then
                        strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "33")
                        iStepB = iStepB + 2
                        Return
                    Else
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "33"), enumExceptionType.Alarm, ControlUI.FormName))
                        Return
                    End If
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "33"), strTriggerType)
                End If
                iStepB = iStepB + 1

            Case 5
                If strTriggerType = "A" Then
                    strHUA = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    UpdateMESMessage(strHUA, strTriggerType)
                    If Not CType(cDeviceCfgA.Source, clsHMIMES).SetupComponent(strHUA, strResult) Then
                        If bReadOnly Then
                            strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "35", strResult) + " " + strHUA
                            iStepB = iStepB + 1
                            Return
                        Else
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        ' UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), strTriggerType)
                    End If
                Else
                    strHUB = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    UpdateMESMessage(strHUB, strTriggerType)
                    If Not CType(cDeviceCfgB.Source, clsHMIMES).SetupComponent(strHUB, strResult) Then
                        If bReadOnly Then
                            strErrorMessage = cLanguageManager.GetUserTextLine("GapFiller", "35", strResult) + " " + strHUB
                            iStepB = iStepB + 1
                            Return
                        Else
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), enumExceptionType.Alarm, ControlUI.FormName))
                            Return
                        End If
                        '  UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "35", strResult), strTriggerType)
                    End If
                End If

                iStepB = iStepB + 4

            Case 6
                If Not bLock Then
                    bLock = True
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("User", strTriggerType + ": " + strErrorMessage, enumMainTipsManagerType.Confirm))
                    UpdateMESMessage(strErrorMessage, strTriggerType)
                    iStepB = iStepB + 1
                End If

            Case 7
                If IsNothing(cDeviceCfg) Then
                    cPLCAction.HMIError(cHMIPLC, 1, cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromID(1).HMIError)
                Else
                    cPLCAction.HMIError(cHMIPLC, cDeviceCfg.StationID, cMachineManager.MachineCellManager.CurrentMachineCfg.GetMachineStationCfgFromID(cDeviceCfg.StationID).HMIError)
                End If
                iStepB = iStepB + 1

            Case 8

                If strTriggerType = "A" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1

                    End If
                End If

                If strTriggerType = "B" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                End If

                Dim strNowHU As String = ""
                If strTriggerType = "A" Then
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    If strNowHU <> strHUA Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                Else
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    If strNowHU <> strHUB Then
                        bLock = False
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                End If

                If cMainTipsManager.GetMainTipsConfirmTypeFromKey("User") = enumMainTipsConfirmType.Continue Then
                    bLock = False
                    If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                    ResetHSMESOK(strTriggerType)
                    iStepB = 1
                End If

                If cMainTipsManager.GetMainTipsConfirmTypeFromKey("User") = enumMainTipsConfirmType.Abort Then
                    UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "34"), strTriggerType)
                    bLock = False
                    iStepB = 10
                End If

            Case 9
                If strTriggerType = "A" Then
                    Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                    If dOldValue = 0 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
                    End If
                    If dOldValue = 2 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                    End If
                Else
                    Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
                    If dOldValue = 0 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
                    End If
                    If dOldValue = 1 Then
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(3))
                    End If
                End If
                UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "34"), strTriggerType)
                iStepB = iStepB + 1

            Case 10
                TempStructGapFiller = cHMIPLC.GetValue(lListInitParameter(0))

                Dim strNowHU As String = ""
                If strTriggerType = "A" Then
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoA + "/Q" + cGapFiller.PPSstrVolumeA + "/14D" + cGapFiller.PPSstrExpiryDateA + "/V" + cGapFiller.PPSstrSupplierNoA + "/S" + cGapFiller.PPSstrPackagingNoA
                    If strNowHU <> strHUA Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                Else
                    strNowHU = "/P" + cGapFiller.PPSstrPartNoB + "/Q" + cGapFiller.PPSstrVolumeB + "/14D" + cGapFiller.PPSstrExpiryDateB + "/V" + cGapFiller.PPSstrSupplierNoB + "/S" + cGapFiller.PPSstrPackagingNoB
                    If strNowHU <> strHUB Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                End If

                If strTriggerType = "A" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 1 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                End If

                If strTriggerType = "B" Then
                    If Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 2 And Not TempStructGapFiller.bulPLCPPSscanProcessReadyMES = 3 Then
                        If bReadOnly Then cMainTipsManager.CleanStationTips("User")
                        UpdateMESMessage(cLanguageManager.GetUserTextLine("GapFiller", "37"), strTriggerType)
                        ResetHSMESOK(strTriggerType)
                        iStepB = 1
                    End If
                End If

        End Select
    End Sub

    Private Sub ResetHSMESOK(ByVal strTriggerType As String)
        If strTriggerType = "A" Then
            Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
            If dOldValue = 0 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
            End If
            If dOldValue = 1 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
            End If
            If dOldValue = 2 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
            End If
            If dOldValue = 3 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(2))
            End If
        Else
            Dim dOldValue As Byte = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", GetType(Byte))
            If dOldValue = 0 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
            End If
            If dOldValue = 1 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
            End If
            If dOldValue = 2 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(0))
            End If
            If dOldValue = 3 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIPPSHS_MESok", Byte.Parse(1))
            End If
        End If

        If bReadOnly And Not bLock Then cMainTipsManager.CleanStationTips("User")

    End Sub
    Private Sub UpdateMESMessage(ByVal strMessage As String, ByVal strType As String)
        strMessage = strType + ":" + strMessage
        mMainForm.InvokeAction(Sub()
                                   If strType = "A" Then
                                       HmiTextBox_MES_StatusA.TextBox.Text = strMessage
                                   Else
                                       HmiTextBox_MES_StatusB.TextBox.Text = strMessage
                                   End If

                               End Sub)
        If bReadOnly And Not bLock Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg("User", strMessage))
        cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, strMessage)
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
        '  iStepA = 1
        ' iStepB = 1
        '  iStep = 1
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
        '  iStepA = 1
        '  iStepB = 1
        '   iStep = 1
        Return True
    End Function
End Class
