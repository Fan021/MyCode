Imports System.ComponentModel
Imports System.Timers
Imports System.Windows.Forms.Integration
Imports Kostal.Las.Base

Public Class MainForm_Mul
    Implements IMainForm

#Region "Const"
    Public Const LAS As String = "LAS"

    Public Const sName As String = "mMainForm"

    Public Const conARTICLE_VIEW As String = "ArticleSelectView"

    Public Const conSCHEDULE_VIEW As String = "ScheduleSelectView"

    Public Const conLoging_VIEW As String = "UserLoginView"

    Public Const conOVERVIEW As String = "Overview"

    Public Const conSTATION_VIEW As String = "StationView"
#End Region


#Region "View"
    Private WithEvents articleView As New ArticleSelectView

    Private WithEvents scheduleView As New ScheduleSelectView

    Private WithEvents LoginView As New ChildrenLoginForm

    Private WithEvents overView As New OverallView

    Public WithEvents stationView As New StationView

    Private WithEvents _LasCounterView As CounterView
    Private WithEvents _LasIOView As ChildrenIOForm
    Private WithEvents _LasCylinderForm As ChildrenCylinderForm
    Private WithEvents _LasErrorCodeListForm As ChildrenErrorCodeListForm
    Private WithEvents _LasPlcMessageListForm As ChildrenPlcMessageListForm
    Private WithEvents _LasProductionDataView As ProductionDataView
    Private WithEvents _LasPlcParameterForm As ChildrenParameterForm

    Private WithEvents _LasUserForm As ChildrenUserForm
    Public WithEvents _LasShortCutView As New ChildrenShortCutForm

    Private SystemLog As New SystemLog

    Private WatchWT As New WtStatusView

    Private LineArticleCounter As ArticleCounter

    Private LineMaintenance As Maintenance

    Private SchedulePanel As ScheduleViewFrom

    Private WithEvents LanguageView As New LanguageSelectView
    Private ListResie As New Dictionary(Of String, Boolean)
    Private mPassword As New PassWordForm

    Private overviewInfo As OverviewInformation
    Private _Object As New Object
    Private cCurrentStationPage As enumStationView = enumStationView.OverView
#End Region

#Region "System Forms"
    Public WithEvents lblRefPart As New System.Windows.Forms.Label

    Public WithEvents CBArticle As System.Windows.Forms.ComboBox

    Public WithEvents CBLanguage As New System.Windows.Forms.ComboBox

    Public WithEvents lblPass As New System.Windows.Forms.Label

    Public WithEvents lblfail As New System.Windows.Forms.Label

    Public WithEvents lblMessage As New System.Windows.Forms.Label

    Public WithEvents lbltotal As New System.Windows.Forms.Label

    Public WithEvents btnArticle As New System.Windows.Forms.Button
#End Region

#Region "PLC"
    Public MyTwinCat As New Dictionary(Of String, TwinCatAds)

    Public PLCdisconnect As Boolean = False

    Private _PLC_bulRedboxStatus As Boolean

    Private _PC_bulRedboxLock As Boolean

    Private _PC_bulSwitchOnOff As Boolean

    Public _PlcIsPoweredOn As Boolean = False

    Public _PlcAutoManual As enumPLC_AUTO_MANUAL = enumPLC_AUTO_MANUAL.None
#End Region

#Region "Prompt"
    Private _plcErrorMessageSet As structErrorMessageSet

    Private _lasErrorMessageSet As structErrorMessageSet

    Private _EnableToShowLasPrompt As Boolean = False

    Private PromptsController As New Prompts.StationPromptsController(New Prompts.TestStationParameters)

    Private myPrompt As UserPrompt

    Private _LastErrorType As enumHMI_ERROR_TYPE = enumHMI_ERROR_TYPE.None

    Protected _ReferenceIndicator As enumINDICATOR_STATRUS = enumINDICATOR_STATRUS.Unknown

    Protected _MainPlcIndicator As enumINDICATOR_STATRUS = enumINDICATOR_STATRUS.Unknown

    Protected _LinecotrolIndicator As enumINDICATOR_STATRUS = enumINDICATOR_STATRUS.Unknown

    Protected _CaqIndicator As enumINDICATOR_STATRUS = enumINDICATOR_STATRUS.Unknown

    Protected _RunningInClearMode As Boolean = False

    Private _NewPartStation As NewPartStation

    Private _dicOverviewInfo As Dictionary(Of Integer, structStationOverviewInfo)
#End Region

#Region "System"
    Public mFileHandler As New FileHandler

    Public mXmlHandler As New XmlHandler

    Public FileHandler As New FileHandler

    Public LocalSchedule As Base.Schedule

    Public AppSettings As Settings

    Public AppArticle As Article

    Public mLanguage As Language

    Public Log As Logger

    Public i As New Station

    Public Stations As Dictionary(Of String, IStationTypeBase)
    Public Devices As Dictionary(Of String, Object)
    Public AppArticleUsed As Boolean
    Private cTips As clsTips
#End Region

#Region "System tick"
    Public sw As New Stopwatch

    Public CycleCounter As Long
#End Region
    Public Event IamClosing As IMainForm.MainForm_IamClosingEventHandler Implements IMainForm.MainForm_IamClosing
    Protected _FileHandler As New FileHandler
    Public bUpdateLasMessage As Boolean = False
    Public cFormFontResize As New clsFormFontResize
    Private cUserManager As clsUserManager
    Private cLanguageManager As Language
    Private _xmlHandler As New XmlHandler
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.LinecotrolIndicator = enumINDICATOR_STATRUS.Gray

        CBArticle = articleView.CBArticle
        CBLanguage = LanguageView.CBLanguage
        cFormFontResize.cons = Me
        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.Top = 0
        Me.Left = 0
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height


    End Sub

    Public Property ReferenceIndicator As enumINDICATOR_STATRUS
        Get

            Return _ReferenceIndicator

        End Get

        Set(value As enumINDICATOR_STATRUS)

            If _ReferenceIndicator = value Then Return

            Const IMAGE_NAME As String = "RefStatus"

            _ReferenceIndicator = value

            UpdateStatusImage(IMAGE_NAME, _ReferenceIndicator)

        End Set

    End Property

    Public Property MainPlcIndicator As enumINDICATOR_STATRUS
        Get

            Return _MainPlcIndicator

        End Get

        Set(value As enumINDICATOR_STATRUS)

            If _MainPlcIndicator = value Then Return

            Const IMAGE_NAME As String = "PLC"

            _MainPlcIndicator = value

            UpdateStatusImage(IMAGE_NAME, _MainPlcIndicator)

        End Set

    End Property

    Public Property LinecotrolIndicator As enumINDICATOR_STATRUS Implements IMainForm.MainForm_LinecotrolIndicator
        Get

            Return _LinecotrolIndicator

        End Get

        Set(value As enumINDICATOR_STATRUS)

            If _LinecotrolIndicator = value Then Return

            Const IMAGE_NAME As String = "LCStatus"

            _LinecotrolIndicator = value

            UpdateStatusImage(IMAGE_NAME, _LinecotrolIndicator)

        End Set

    End Property



    Public Property MainLogger As ListBox Implements IMainForm.MainForm_MainLogger
        Get
            Return Me.SystemLog.SystemMainLogger
        End Get
        Set(value As ListBox)
            ' Me.SystemLog.SystemMainLogger = value
        End Set
    End Property



    Public ReadOnly Property PC_bulRedboxLock As Boolean
        Get
            Return _PC_bulRedboxLock
        End Get
    End Property

    Public ReadOnly Property PC_bulSwitchOnOff As Boolean
        Get
            Return _PC_bulSwitchOnOff
        End Get
    End Property

    Public ReadOnly Property PC_bulResetError As Boolean
        Get
            If cTips.Active Then
                If PromptsController.PcResetError Then
                    cTips.Active = False
                End If
            End If
            Return PromptsController.PcResetError
        End Get
    End Property

    Public Property TabControlStations As TabControl Implements IMainForm.MainForm_TabControlStations
        Get
            Return Me.stationView.TabControlStations
        End Get
        Set(value As TabControl)
            Me.stationView.TabControlStations = value
        End Set
    End Property


    Public Property NewPartStartion As NewPartStation Implements IMainForm.MainForm_NewPartStartion
        Get
            Return _NewPartStation
        End Get
        Set(value As NewPartStation)
            If _NewPartStation IsNot Nothing Then _NewPartStation = Nothing
            _NewPartStation = value
        End Set
    End Property

    Public Property StationOverviewInfo As Dictionary(Of Integer, structStationOverviewInfo) Implements IMainForm.MainForm_StationOverviewInfo
        Get
            Return _dicOverviewInfo
        End Get
        Set(value As Dictionary(Of Integer, structStationOverviewInfo))
            _dicOverviewInfo = value
        End Set
    End Property

    Public Property ErrorMessageSet As structErrorMessageSet Implements IMainForm.MainForm_ErrorMessageSet
        Get
            If cTips.Active Then
                Return cTips._lasErrorMessageSet
            ElseIf _EnableToShowLasPrompt Then
                Return _lasErrorMessageSet
            Else
                Return _plcErrorMessageSet
            End If
        End Get
        Set(value As structErrorMessageSet)
            _plcErrorMessageSet = value
        End Set
    End Property

    Public Property MainForm_Timer As System.Windows.Forms.Timer Implements IMainForm.MainForm_Timer
        Get
            Return swTimer
        End Get
        Set(value As System.Windows.Forms.Timer)
            swTimer = value
        End Set
    End Property

    Public Property MainForm_CycleCounter As Long Implements IMainForm.MainForm_CycleCounter
        Get
            Return CycleCounter
        End Get
        Set(value As Long)
            CycleCounter = value
        End Set
    End Property



    Public Property MainForm_PLCdisconnect As Boolean Implements IMainForm.MainForm_PLCdisconnect
        Get
            Return PLCdisconnect
        End Get
        Set(value As Boolean)
            PLCdisconnect = value
        End Set
    End Property

    Public Property MainForm_ScheduleSelectView As IScheduleUI Implements IMainForm.MainForm_ScheduleSelectView
        Get
            Return Me.scheduleView
        End Get
        Set(value As IScheduleUI)
            scheduleView = value
        End Set
    End Property

    Public Property MainForm_lblCurrentSchedule As Label Implements IMainForm.MainForm_lblCurrentSchedule
        Get
            Return lblCurrentSchedule
        End Get
        Set(value As Label)
            lblCurrentSchedule = value
        End Set
    End Property

    Public Property MainForm_lblActualSerialNumber As Label Implements IMainForm.MainForm_lblActualSerialNumber
        Get
            Return lblActualSerialNumber_01
        End Get
        Set(value As Label)
            lblActualSerialNumber_01 = value
        End Set
    End Property

    Public Property MainForm_btnArticle As Button Implements IMainForm.MainForm_btnArticle
        Get
            Return btnArticle
        End Get
        Set(value As Button)
            btnArticle = value
        End Set
    End Property

    Public Property MainForm_lblRefPart As Label Implements IMainForm.MainForm_lblRefPart
        Get
            Return lblRefPart
        End Get
        Set(value As Label)
            lblRefPart = value
        End Set
    End Property

    Public Property MainForm_lblMessage As Label Implements IMainForm.MainForm_lblMessage
        Get
            Return lblMessage
        End Get
        Set(value As Label)
            lblMessage = value
        End Set
    End Property

    Public Property MainForm_lblPass As Label Implements IMainForm.MainForm_lblPass
        Get
            Return lblPass
        End Get
        Set(value As Label)
            lblPass = value
        End Set
    End Property

    Public Property MainForm_lblfail As Label Implements IMainForm.MainForm_lblfail
        Get
            Return lblfail
        End Get
        Set(value As Label)
            lblfail = value
        End Set
    End Property

    Public Property MainForm_lbltotal As Label Implements IMainForm.MainForm_lbltotal
        Get
            Return lbltotal
        End Get
        Set(value As Label)
            lbltotal = value
        End Set
    End Property

    Public Property MainForm_btnReset As Button Implements IMainForm.MainForm_btnReset
        Get
            Return Me._LasShortCutView.btnReset
        End Get
        Set(value As Button)
            Me._LasShortCutView.btnReset = value
        End Set
    End Property

    Public Property MainForm_CaqIndicator As enumINDICATOR_STATRUS Implements IMainForm.MainForm_CaqIndicator
        Get

            Return _CaqIndicator

        End Get

        Set(value As enumINDICATOR_STATRUS)

            If _CaqIndicator = value Then Return

            Const IMAGE_NAME As String = "CAQ"

            _CaqIndicator = value

            UpdateStatusImage(IMAGE_NAME, _CaqIndicator)

        End Set
    End Property

    Public Property MainForm_cFormFontResize As clsFormFontResize Implements IMainForm.MainForm_cFormFontResize
        Get
            Return cFormFontResize
        End Get
        Set(value As clsFormFontResize)
            cFormFontResize = value
        End Set
    End Property

    Public Property MainForm_MyTwinCat As Dictionary(Of String, TwinCatAds) Implements IMainForm.MainForm_MyTwinCat
        Get
            Return MyTwinCat
        End Get
        Set(value As Dictionary(Of String, TwinCatAds))
            MyTwinCat = value
        End Set
    End Property

    Public Property MainForm_Text As String Implements IMainForm.MainForm_Text
        Get
            Return Me.Text
        End Get
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    Public Property MainForm_CBArticle As ComboBox Implements IMainForm.MainForm_CBArticle
        Get
            Return CBArticle
        End Get
        Set(value As ComboBox)
            CBArticle = value
        End Set
    End Property

    Public Property MainForm_stationView As Object Implements IMainForm.MainForm_stationView
        Get
            Return stationView
        End Get
        Set(value As Object)
            stationView = value
        End Set
    End Property

    Public Property MainForm_btnResetFail As Button Implements IMainForm.MainForm_btnResetFail
        Get
            Return _LasShortCutView.btnResetFail
        End Get
        Set(value As Button)
            _LasShortCutView.btnResetFail = value
        End Set
    End Property

    Public Property MainForm_btnClear As Button Implements IMainForm.MainForm_btnClear
        Get
            Return btnClear
        End Get
        Set(value As Button)
            btnClear = value
        End Set
    End Property

    Private Property IMainForm_cFormFontResize As clsFormFontResize Implements IMainForm.cFormFontResize
        Get
            Return cFormFontResize
        End Get
        Set(value As clsFormFontResize)
            cFormFontResize = value
        End Set
    End Property

    Public Property PlcIsPoweredOn As Boolean Implements IMainForm.PlcIsPoweredOn
        Get
            Return _PlcIsPoweredOn
        End Get
        Set(value As Boolean)
            _PlcIsPoweredOn = value
        End Set
    End Property

    Public Property PlcAutoManual As enumPLC_AUTO_MANUAL Implements IMainForm.PlcAutoManual
        Get
            Return _PlcAutoManual
        End Get
        Set(value As enumPLC_AUTO_MANUAL)
            _PlcAutoManual = value
        End Set
    End Property

    Private Sub scheduleModeView_Click(ByVal sender As System.Object, ByVal e As LasViewEventArgs) Handles scheduleView.ScheduleChanging

        If Not e.IsMakeSure Then

            'Me.panMain.Controls.Item(conSCHEDULE_VIEW).Visible = False
            'Me.panMain.Controls.Item(conOVERVIEW).Visible = True
            'Me.panMain.Controls.Item(conOVERVIEW).BringToFront()
            ShowPanel(conOVERVIEW)
            cCurrentStationPage = enumStationView.OverView
            Return

        Else

            If e.ConText IsNot Nothing AndAlso e.ConText.Contains(LAS_ScheduleMode.ProductionMode.ToString) Then

                'Me.panMain.Controls.Item(conSCHEDULE_VIEW).Visible = False
                'Me.panMain.Controls.Item(conOVERVIEW).BringToFront()
                'Me.panMain.Controls.Item(conOVERVIEW).Visible = True
                ShowPanel(conOVERVIEW)
                cCurrentStationPage = enumStationView.OverView
            End If

        End If

    End Sub

    Private Sub btnArticle_Click(ByVal sender As System.Object, ByVal e As LasViewEventArgs) Handles articleView.ArticleChanging

        '_CBArticle.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data

        If Not e.IsMakeSure Then
            ShowPanel(conOVERVIEW)
            cCurrentStationPage = enumStationView.OverView
            'Me.panMain.Controls.Item(conARTICLE_VIEW).Visible = False
            'Me.panMain.Controls.Item(conOVERVIEW).Visible = True
            'Me.panMain.Controls.Item(conOVERVIEW).BringToFront()
            Return
        End If

        mPassword.ChangeMode = False
        mPassword.ShowDialog()

        If mPassword.PassWordValid Then
            Dim sResult As String = "False"
            Try
                sResult = _xmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "ArticleChangeSchedule")
            Catch ex As Exception

            End Try
            If sResult.ToUpper = "TRUE" Then _FileHandler.DelectFile(AppSettings.LogFolder + "REF.Ini")

            If AppArticle.GetArticle_FromID(_CBArticle.Text) AndAlso InitArticleElement(_CBArticle.Text) Then

                lblCurrentArticle.Text = _CBArticle.Text
                ShowPanel(conOVERVIEW)
                cCurrentStationPage = enumStationView.OverView
                'Me.panMain.Controls.Item(conARTICLE_VIEW).Visible = False

                'Me.panMain.Controls.Item(conOVERVIEW).Visible = True
                'Me.panMain.Controls.Item(conOVERVIEW).BringToFront()


            Else
                lblCurrentArticle.Text = ""
            End If
            mXmlHandler.SetGeneralInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT, _CBArticle.Text)
            Log.Logger(i, False, mLanguage.LanguageElement.GetTextLine(i, enumLK_TEXT.LK_TEXT_CHANGEARTICLE, AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data), "_btnArticle_Click")
            'MsgBox(i.Text, MsgBoxStyle.Information, i.IdString & " - " & i.StepTextLine)

        End If
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As LasViewEventArgs) Handles LoginView.UserChanging
        If Not e.IsMakeSure Then
            ShowPanel(conOVERVIEW)
            cCurrentStationPage = enumStationView.OverView
            Return
        End If
    End Sub

    Private Sub btnLanguage_Click(ByVal sender As System.Object, ByVal e As LasViewEventArgs) Handles LanguageView.LanguageChanging

        '_CBArticle.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data

        If Not e.IsMakeSure Then
            'Me.panMain.Controls.Item(conARTICLE_VIEW).Visible = False
            'Me.panMain.Controls.Item(conOVERVIEW).Visible = True
            'Me.panMain.Controls.Item(conOVERVIEW).BringToFront()
            ShowPanel(conOVERVIEW)
            cCurrentStationPage = enumStationView.OverView
            Return
        End If

        mPassword.ChangeMode = False
        mPassword.ShowDialog()

        If mPassword.PassWordValid And _CBLanguage.Text <> "" Then
            mXmlHandler.SetGeneralInformation(AppSettings.LogFolder,
                                              AppSettings.ApplicationActive,
                                              mLanguage.LanguageElement.Section_LanguageFileNames,
                                              mLanguage.LanguageElement.KeyWord_SelectedLanguage,
                                              _CBLanguage.Text)
            mLanguage.SetAppLanguage.ReloadLanguage()
            ReadLanguage()
            articleView.InitLanugage()
            scheduleView.InitLanugage()
            LineArticleCounter.LanguageInit()
            LineMaintenance.LanguageInit()
            overView.InitLanguage()
            ' mLanguage.ReadControlText(Me.TableLayoutPanel1)
            ' mLanguage.ReadControlText(Me.TableLayoutPanel2)
            'mLanguage.ReadControlText(Me.stationView.TabControlStations)
            For Each _Station As IStationTypeBase In Stations.Values
                _Station.ReLoadLanguage()
            Next
            'ReLoadLanguage
            InitCurrentArticle()

            'If AppArticle.GetArticle_FromID(_CBArticle.Text) AndAlso InitArticleElement(_CBArticle.Text) Then

            '    lblCurrentArticle.Text = _CBArticle.Text

            '    Me.panMain.Controls.Item(conARTICLE_VIEW).Visible = False

            '    Me.panMain.Controls.Item(conOVERVIEW).Visible = True
            '    Me.panMain.Controls.Item(conOVERVIEW).BringToFront()


            'Else
            '    lblCurrentArticle.Text = ""
            'End If
            'mXmlHandler.SetGeneralInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT, _CBArticle.Text)
            'Log.Logger(i, False, mLanguage.LanguageElement.GetTextLine(i, enumLK_TEXT.LK_TEXT_CHANGEARTICLE, AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data), "_btnArticle_Click")
            ''MsgBox(i.Text, MsgBoxStyle.Information, i.IdString & " - " & i.StepTextLine)

        End If
        QuitForm(cCurrentStationPage)
    End Sub
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean Implements IMainForm.MainForm_Init
        Me.Stations = Stations
        Me.Devices = Devices
        'Me.Devices = Devices
        cTips = CType(Devices(clsTips.Name), clsTips)

        i = New Station(Me.Name)
        mLanguage = CType(Devices(Language.Name), Language)
        'mLanguage.ReadControlText(Me.TableLayoutPanel1)
        'mLanguage.ReadControlText(Me.TableLayoutPanel2)
        Me.LoadLanguage()

        AppSettings = CType(Devices(Settings.Name), Settings)
        AppArticle = CType(Devices(Article.Name), Article)
        cUserManager = CType(Devices(clsUserManager.Name), clsUserManager)

        articleView.Init(Devices, Stations, AppSettings)
        scheduleView.Init(Devices, Stations, AppSettings)
        AddHandler cUserManager.UserChanged, AddressOf UserChanged
        cUserManager.AutoLogin()



        AddHandler AppArticle.IDChange, AddressOf Article_Change
        LineArticleCounter = Devices(ArticleCounter.sName)

        LineMaintenance = CType(Devices(Maintenance.sName), Maintenance)

        SchedulePanel = New ScheduleViewFrom
        Devices.Add(ScheduleViewFrom.sName, SchedulePanel)

        LocalSchedule = CType(Devices(Schedule.Name), Schedule)
        ShowScheduleReLoadLanguage()

        Log = New Logger(AppSettings)
        Log.Logger(i, "Init Run", "MainForm")

        overviewInfo = New OverviewInformation(Devices, Stations, AppSettings)
        'If AppSettings.BoschLine Then
        '    LocalSchedule = New Schedule(i, AppSettings, mLanguage)
        '    LocalSchedule.Init()
        'End If

        'WatchWT.Init(Devices, Stations)
        'SetStatusStrip()
        'SetMenu()

        'ReLoadLanguage()
        InitCurrentArticle()
        mPassword.Init(i, AppSettings, "UserPassWord")
        'If Not mFileHandler.FileExist(AppSettings.PicFolder + "layout.bmp") Then
        '    Throw New Exception("No Find " + AppSettings.PicFolder + "layout.bmp")
        '    Return False
        'End If
        'picArticle.Image = New Bitmap(AppSettings.PicFolder + "layout.bmp")
        ''DG_Article.Rows.Clear()
        'Log.Logger(i, "Init sucessfull", "MainForm")
        'ChangeFormSize()
        'If Not AppSettings.BoschLine Then
        '    ShowWtDataToolStripMenuItem.Visible = False
        '    ShowScheduleToolStripMenuItem.Visible = False
        'End If


        lblSnMessage.ForeColor = KostalLasColors.KOSTALBLUE

        AddHandler stationView.UserCancelled, AddressOf UserViewChanged
        ReadLanguage()
        InitInterface()

        StatusForm.Items("ToolVersion").Text = " Version:" + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
        InitSystemPages(enumStationView.System)
        Me.Show()
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, articleView.DesignPanel)
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, scheduleView.DesignPanel)
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, overView.DesignPanel)
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, LoginView.Panel_Body)
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, _LasShortCutView.Panel_Body)
        '  AddHandler _LasShortCutView.btnResetReference.Click, AddressOf btnResetReference_Click
        sw.Start()
        Return True
    End Function
    Private Sub btnResetReference_Click(sender As Object, e As EventArgs)
        _FileHandler.DelectFile(AppSettings.LogFolder + "REF.Ini")
        If Devices.ContainsKey(Shift.Name) Then
            Dim _Shift As Shift = Devices(Shift.Name)
            _Shift.UpdateShift()
        End If
    End Sub
    Private Sub UserViewChanged(sender As Object, e As LasViewEventArgs)
        QuitForm(cCurrentStationPage)
    End Sub
    Protected Sub Article_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        Label_Article_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data
        Label_Name_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
        Label_Index_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_INDEX).Data
        Label_HW_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_HARDWARE_VERSION).Data
        Label_SW_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_SOFTWARE_VERSION).Data
        Label_Custorm_Infor.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data


    End Sub

    Protected Sub UserChanged(ByVal strUser As String, ByVal cUserCfg As clsUserCfg)
        Label_User_Infor.Text = cUserCfg.Name.ToString
        Label_Level_Infor.Text = cUserCfg.Level.ToString
    End Sub


    Public Function InitCounterView(ByVal CounterControl As CounterController) As Boolean Implements IMainForm.MainForm_InitCounterView

        If CounterControl Is Nothing Then Return False

        _LasCounterView = New CounterView(CounterControl)

        Dim counterPan As Panel = _LasCounterView.GetPannel
        counterPan.Dock = DockStyle.Fill
        counterPan.Name = "LasCounter"
        cFormFontResize.SetControls(cFormFontResize.CurrentRate, counterPan)
        panCounter.Controls.Add(counterPan)
        panCounter.Dock = DockStyle.Fill
        Return True

    End Function


    Public Function InitInterface() As Boolean

        'SplitContainerHeader.SplitterDistance = SplitContainerHeader.Width * 0.75

        MainUIAddPage(conSTATION_VIEW, stationView)
        MainUIAddPage(conARTICLE_VIEW, articleView)
        MainUIAddPage(conSCHEDULE_VIEW, scheduleView)
        MainUIAddPage(conOVERVIEW, overView)
        MainUIAddPage(conLoging_VIEW, LoginView)


        'Dim singleControl = New SinglePromptControl()

        'Dim alarmControl = New AlarmPromptControl

        Dim promptControl = New PromptsView


        'Dim listResponses As New List(Of Prompts.UserResponse)

        'listResponses.Add(New Prompts.UserResponse("R", "Reset"))

        'listResponses.Add(New Prompts.UserResponse("N", "No"))

        'listResponses.Add(New Prompts.UserResponse("C", "Cancel"))

        'listResponses.Add(New Prompts.UserResponse("A", "Abort"))

        'myPrompt = PromptsController.Set(Prompts.PromptTypes.Alarm, Prompts.DisplayOptions.None, "The Machine State is on, are you ready?", listResponses)

        'myPrompt = PromptsController.Set("", PromptTypes.Information, DisplayOptions.None, "My first test")

        'myPrompt = PromptsController.Set("Station1", PromptTypes.Alarm, DisplayOptions.Local, "Error")

        'PromptsController.Set(Prompts.PromptTypes.Warning, Prompts.DisplayOptions.Local, "Error222")

        'PromptsController.Set(Prompts.PromptTypes.Problem, Prompts.DisplayOptions.Local, "Error Prompt kostal first test project gem src eolt")


        Dim appPromptsMode As New ApplicationPromptsModel(PromptsController)

        Dim promptsModel As New PromptsModel(appPromptsMode)


        promptControl.DataContext = promptsModel


        panMessage.Controls.Add(New ElementHost() _
                                With {
                                      .Child = promptControl,
                                       .Dock = DockStyle.Fill,
                                      .BackColor = System.Drawing.Color.WhiteSmoke,
                                      .ForeColor = System.Drawing.Color.White
                                    }
                                    )

        ''Dim testStationPara As Testman.Framework.Runtime.Components.TestStationRuntimeParameters

        ''testStationPara = New Testman.Framework.Runtime.Components.TestStationRuntimeParameters(New Testman.Framework.Runtime.Components.TestSystemRuntimeParameters()
        'PromptsController = New Prompts.StationPromptsController(New Testman.Framework.Runtime.Components.TestStationRuntimeParameters)


        'Dim singleModel As SinglePromptModel = New SinglePromptModel(myPrompt)

        'singleControl.DataContext = singleModel


        'Dim alarmModel As AlarmPromptModel = New AlarmPromptModel("stsf", "Problem", Nothing, Now)

        'alarmControl.DataContext = alarmModel

        'myPrompt.PromptText
        'Me.Refresh()

        'Dim responeid As String = myPrompt.WaitForResponse(5000, True)

        'If responeid = "Y" Then

        '       cTips.AddTips("You have choos Yes")

        'Else

        '       cTips.AddTips("You have choos No")

        'End If

        Me.btnStart.BackColor = SystemColors.ButtonFace 'ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
        Me.btnClear.BackColor = SystemColors.ButtonFace 'ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
        Me.btnAuto.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)

        Me.panMain.Controls.Item(conOVERVIEW).Visible = True
        Me.panMain.Controls.Item(conOVERVIEW).BringToFront()

        Me.timCycle.Enabled = True

        Return True
    End Function

    Public Function InitCurrentArticle() As Boolean
        Dim sResult As String, _Element As New ArticleListElement, _ArticleElement As New ArticleElement
        sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "AppArticleNotUsed")
        AppArticleUsed = Trim(sResult.ToUpper) = "TRUE"

        Dim dvArticle = articleView.DG_Article 'CType(Me.panMain.Controls("DG_Article"), DataGridView)
        dvArticle.Rows.Clear()

        'DG_Article.Rows.Clear()
        If Not AppArticleUsed Then
            Log.Logger(i, "Application Article not in use", "InitCurrentArticle")
            dvArticle.Visible = True
            _CBArticle.Text = ""
            Return True
        End If
        'gbArticle.Visible = True
        sResult = mXmlHandler.GetSectionInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT)
        If Not AppArticle.ArticleListElement.ContainsKey(sResult.Trim) And AppArticle.ArticleListElement.Count >= 1 Then
            sResult = AppArticle.ArticleListElement(AppArticle.ArticleListElement.Keys(0)).ID
        End If
        If AppArticle.ArticleListElement.ContainsKey(sResult.Trim) Then

            AppArticle.GetArticle_FromID(sResult)

            InitArticleElement(sResult)

            _CBArticle.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data

            If lblCurrentArticle.Text <> _CBArticle.Text Then lblCurrentArticle.Text = _CBArticle.Text

            Log.Logger(i, "Successful", "InitCurrentArticle :" + sResult)
        Else
            Log.Logger(i, True, "InitCurrentArticle " + sResult + " Fail")
            _CBArticle.SelectedIndex = -1
            AppArticleUsed = False
            mXmlHandler.SetGeneralInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT, "")
        End If
        Return True
    End Function

    Public Function InitArticleElement(ByVal mID As String) As Boolean
        Dim dv = articleView.DG_Article
        dv.Rows.Clear()

        For Each _ArticleElement As ArticleElement In AppArticle.ArticleElements.Values
            If _ArticleElement.Visible Then
                dv.Rows.Add(_ArticleElement.Name, _ArticleElement.Data)
            End If
        Next
        Return True
    End Function

    Private Sub UpdateStatusImage(ByVal ImageName As String, ByVal IndicatorStatus As enumINDICATOR_STATRUS)

        If StatusForm.Items(ImageName) Is Nothing Then Return

        Select Case IndicatorStatus
            Case enumINDICATOR_STATRUS.Gray

                StatusForm.Items(ImageName).BackgroundImage = My.Resources.gray

            Case enumINDICATOR_STATRUS.Red

                StatusForm.Items(ImageName).BackgroundImage = My.Resources.red

            Case enumINDICATOR_STATRUS.Green

                StatusForm.Items(ImageName).BackgroundImage = My.Resources.green

            Case Else

                'do nothing

        End Select

    End Sub

    Private Sub Run()

        RefreshIndicatorWithNewPartStation()

        RefreshInterface()

        RefreshStationInformation()

        RefreshErrorMessagePrompt()

        WatchWT.Run()

        LineArticleCounter.Run()
        LineMaintenance.Run()


    End Sub

    Public Sub RefreshIndicatorWithNewPartStation()


        If _NewPartStation Is Nothing Then Return

        If _NewPartStation.References Is Nothing Then Return

        If _NewPartStation.LastScheduleMode = LAS_ScheduleMode.ClearMode.ToString Then

            _RunningInClearMode = True

            If btnClear.BackColor <> KostalLasColors.GREEN Then btnClear.BackColor = KostalLasColors.GREEN

        Else
            _RunningInClearMode = False

            If btnClear.BackColor <> KostalLasColors.BUTTONFACE Then btnClear.BackColor = KostalLasColors.BUTTONFACE

        End If

        If Not _NewPartStation.References.RefEnable Then

            Me.ReferenceIndicator = enumINDICATOR_STATRUS.Gray

        ElseIf _NewPartStation.PC_bulScanPartRequest And Not _NewPartStation.PC_bulScannedPartResult Then

            Me.ReferenceIndicator = enumINDICATOR_STATRUS.Red

        Else

            Me.ReferenceIndicator = enumINDICATOR_STATRUS.Green

        End If

    End Sub

    Private Sub RefreshErrorMessagePrompt()

        If cTips.Active Then
            If PromptsController.PcResetError Then
                cTips.Active = False
            End If
        End If

        Dim listResponses As New List(Of Prompts.UserResponse)
        Dim cErrorCodeManager As clsErrorCodeManager = Devices(clsErrorCodeManager.Name)
        Dim cPlcMessageManager As clsPlcMessageManager = Devices(clsPlcMessageManager.Name)
        'myPrompt = PromptsController.Set(ErrorMessageSet, listResponses)
        Dim strMessage As String = ""
        Select Case ErrorMessageSet.strErrorType
            Case enumHMI_ERROR_TYPE.MasterError.ToString, enumHMI_ERROR_TYPE.Error.ToString
                Dim cErrorCodeCfg As clsErrorCodeCfg = cErrorCodeManager.GetErrorCfgFromCode(ErrorMessageSet.iErrorCode.ToString)
                If Not IsNothing(cErrorCodeCfg) Then
                    Dim mMessage As String = cErrorCodeCfg.ActiveMessage
                    If mMessage.IndexOf("$1") >= 0 Then
                        mMessage = mMessage.Replace("$1", ErrorMessageSet.strErrorMessage)
                    End If
                    strMessage = ErrorMessageSet.strErrorMessage
                    ErrorMessageSet.strErrorMessage = mMessage
                End If

                listResponses.Add(New Prompts.UserResponse("R", "Reset"))
                myPrompt = PromptsController.Set(ErrorMessageSet, listResponses, _LastErrorType <> enumHMI_ERROR_TYPE.Error)
                _LastErrorType = enumHMI_ERROR_TYPE.Error
                If strMessage <> "" Then ErrorMessageSet.strErrorMessage = strMessage

            Case enumHMI_ERROR_TYPE.Message.ToString, enumHMI_ERROR_TYPE.MasterMessage.ToString, enumHMI_ERROR_TYPE.Tips.ToString
                Dim cPlcMessageCfg As clsPlcMessageCfg = cPlcMessageManager.GetPlcMessageCfgFromKey(ErrorMessageSet.iErrorCode.ToString)
                If Not IsNothing(cPlcMessageCfg) Then
                    Dim mMessage As String = cPlcMessageCfg.ActiveMessage
                    If mMessage.IndexOf("$1") >= 0 Then
                        mMessage = mMessage.Replace("$1", ErrorMessageSet.strErrorMessage)
                    End If
                    strMessage = ErrorMessageSet.strErrorMessage
                    ErrorMessageSet.strErrorMessage = mMessage
                End If
                listResponses.Clear()
                myPrompt = PromptsController.Set(ErrorMessageSet, listResponses, _LastErrorType <> enumHMI_ERROR_TYPE.Message)
                _LastErrorType = enumHMI_ERROR_TYPE.Message
                If strMessage <> "" Then ErrorMessageSet.strErrorMessage = strMessage
            Case Else

                myPrompt = PromptsController.Set(ErrorMessageSet, listResponses, _LastErrorType <> enumHMI_ERROR_TYPE.None)
                _LastErrorType = enumHMI_ERROR_TYPE.None

        End Select


    End Sub

    Private Sub RefreshStationInformation() '

        Dim stationType As String = ""

        For Each item As structStationOverviewInfo In _dicOverviewInfo.Values

            stationType = item.strStationName
            If stationType = "System" Then
                Select Case item.strAutoManual
                    Case enumPLC_AUTO_MANUAL.Auto.ToString
                        _PlcAutoManual = enumPLC_AUTO_MANUAL.Auto
                    Case enumPLC_AUTO_MANUAL.Manual.ToString
                        _PlcAutoManual = enumPLC_AUTO_MANUAL.Manual
                    Case Else
                        _PlcAutoManual = enumPLC_AUTO_MANUAL.None
                End Select

                Select Case item.strStationStatus
                    Case enumPLC_Status.Off.ToString
                        _PlcIsPoweredOn = False
                    Case Else
                        _PlcIsPoweredOn = True
                End Select

                _EnableToShowLasPrompt = False
                ' If item.strStationStatus = enumPLC_Status.Run.ToString And bUpdateLasMessage Then
                ' If item.strStationStatus = enumPLC_Status.Run.ToString AndAlso ErrorMessageSet.iErrorCode = 666 Then
                ' If item.strStationStatus = enumPLC_Status.Run.ToString AndAlso _plcErrorMessageSet.strErrorType <> enumHMI_ERROR_TYPE.MasterError.ToString AndAlso _plcErrorMessageSet.strErrorType <> enumHMI_ERROR_TYPE.MasterMessage.ToString AndAlso lblRefPart.Visible Then
                If item.strStationStatus <> enumPLC_Status.Off.ToString AndAlso lblRefPart.Visible Then
                    If _plcErrorMessageSet.strErrorType <> enumHMI_ERROR_TYPE.Error.ToString AndAlso _plcErrorMessageSet.strErrorType <> enumHMI_ERROR_TYPE.MasterError.ToString AndAlso _plcErrorMessageSet.strErrorType <> enumHMI_ERROR_TYPE.MasterMessage.ToString Then
                        If CType(lblRefPart.Tag, enumHMI_ERROR_TYPE) > 0 Then
                            _EnableToShowLasPrompt = True
                            bUpdateLasMessage = False
                        Else
                            _EnableToShowLasPrompt = False
                        End If
                    End If
                End If

            End If

            For Each value As StationInformation In overviewInfo.GetStationInfos

                If stationType = value.StationKey Then

                    value.Enable = True
                    value.PlcStatus = item.strStationStatus
                    value.StepNr = item.iActualStepNumber
                    value.ArticleNr = item.strArticleNumber
                    value.SerialNumber = item.strSerialNumber
                    value.WT = item.iCarrierNumber
                    value.DS = item.iDestinationStation
                    value.TestmanPercent = item.strTestmanPercent
                    value.TestmanStatus = item.strTestmanStatus
                    value.AutoManual = item.strAutoManual
                    value.ScheduleName = item.strScheduleName
                    value.TestTime = item.strProcessTime
                    value.Result = item.xTestResult

                    Exit For
                End If

            Next

        Next

        'overviewInfo.GetStationInfo(enumSTATION_KEY.Station01).Result = True
        'overviewInfo.GetStationInfo(enumSTATION_KEY.Station02).Result = True
        'overviewInfo.GetStationInfo(enumSTATION_KEY.Station03).Result = True

        For Each item As StationInformation In overviewInfo.GetOverviewDictionary.Values

            overView.UpdateRow(item.StationKey, item)

        Next

    End Sub

    Private Sub RefreshInterface()

        'RefreshBtnColor(Me.btnCurrentSchedule, conSCHEDULE_VIEW)
        ' RefreshBtnColor(Me.btnCurrentArticle, conARTICLE_VIEW)
        RefreshBtnColor()

        WatchWT.ShowWtData = Me.stationView.TabControlSystem.Visible
        LineArticleCounter.ShowCounter = Me.stationView.TabControlSystem.Visible
        LineMaintenance.ShowMaintain = Me.stationView.TabControlSystem.Visible
        SchedulePanel.ShowSchedule = Me.stationView.TabControlSystem.Visible

        Dim Result As String = ""
        Select Case _PlcAutoManual
            Case enumPLC_AUTO_MANUAL.Auto
                'btnAuto.Text = "自 动"
                'btnRedbox.Enabled = False
                ' If btnRedbox.BackColor <> KostalLasColors.BUTTONFACE Then btnRedbox.BackColor = KostalLasColors.BUTTONFACE
                Result = mLanguage.Read("MainUI", _PlcAutoManual.ToString)
                If Result = "" Then Result = "Auto"
                If btnAuto.Text <> Result Then btnAuto.Text = Result
                btnAuto.BackColor = KostalLasColors.GREEN 'ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)

            Case enumPLC_AUTO_MANUAL.Manual
                'btnAuto.Text = "手 动"
                Result = mLanguage.Read("MainUI", _PlcAutoManual.ToString)
                If Result = "" Then Result = "Manual"
                If btnAuto.Text <> Result Then btnAuto.Text = Result

                If _PC_bulRedboxLock Then
                    '   btnRedbox.BackColor = KostalLasColors.GREEN
                Else
                    '   btnRedbox.BackColor = KostalLasColors.RED
                End If

                'btnRedbox.Enabled = True
                btnAuto.BackColor = KostalLasColors.YELLOW 'ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED)
            Case Else
                btnAuto.Text = "---"
                btnAuto.BackColor = KostalLasColors.BUTTONFACE
        End Select

        'If Result <> "" And btnAuto.Text <> Result Then
        '    btnAuto.Text = Result
        'Else
        '    btnAuto.Text = "---"
        '    btnAuto.BackColor = KostalLasColors.BUTTONFACE
        'End If

        If _PlcIsPoweredOn Then
            'btnStart.Text = "运行中"
            btnStart.BackColor = KostalLasColors.GREEN
        Else
            btnStart.BackColor = KostalLasColors.BUTTONFACE
            'btnStart.Text = "开 始"
        End If

    End Sub

    Private Sub MainUI_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        e.Cancel = True
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then MeExit()

    End Sub

    Private Sub MeExit()
        Log.Logger(i, "sucessfull", "Exit")
        RaiseEvent IamClosing()
        'Me.Dispose()
    End Sub

    Private Sub MainUI_Load(sender As Object, e As EventArgs) Handles Me.Load



    End Sub

    Private Sub InitSystemPages(ByVal strName As enumStationView)

        If strName = enumStationView.System Then
            Me.stationView.TabControlSystem.TabPages.Clear()
            Const SYSTEM_LOG As String = "Log"
            Const WT_STATUS As String = "WtStatus"
            Const COUNTERS As String = "Counters"
            Const MAINTENANCE As String = "Maintenance"
            Const SCHEDULE_PANEL As String = "Schedule"
            Const LANGUAGE_SELECT As String = "Language"
            Const ErrorCode_SELECT As String = "ErrorCode"
            Const PlcMessage_SELECT As String = "PlcMessage"
            Const ProductionView_SELECT As String = "ProductionView"
            Const Parameter_SELECT As String = "Parameter"
            Const User_SELECT As String = "User"

            WatchWT = New WtStatusView
            WatchWT.Init(Devices, Stations)

            TabControlSystemAddPage(SYSTEM_LOG, Me.SystemLog, False)
            TabControlSystemAddPage(WT_STATUS, Me.WatchWT, True)
            TabControlSystemAddPage(COUNTERS, Me.LineArticleCounter, False)
            TabControlSystemAddPage(MAINTENANCE, Me.LineMaintenance, False)
            TabControlSystemAddPage(SCHEDULE_PANEL, Me.SchedulePanel, False)
            If cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator Then TabControlSystemAddPage(LANGUAGE_SELECT, Me.LanguageView, True)
            _LasErrorCodeListForm = New ChildrenErrorCodeListForm
            _LasErrorCodeListForm.Init(Devices, Stations, AppSettings)

            If cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator Then TabControlSystemAddPage(ErrorCode_SELECT, Me._LasErrorCodeListForm, True)

            _LasPlcMessageListForm = New ChildrenPlcMessageListForm
            _LasPlcMessageListForm.Init(Devices, Stations, AppSettings)
            If cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator Then TabControlSystemAddPage(PlcMessage_SELECT, Me._LasPlcMessageListForm, True)

            _LasPlcParameterForm = New ChildrenParameterForm
            _LasPlcParameterForm.Init(Devices, Stations, AppSettings)
            If cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator Then TabControlSystemAddPage(Parameter_SELECT, Me._LasPlcParameterForm, True)


            _LasUserForm = New ChildrenUserForm
            _LasUserForm.Init(Devices, Stations, AppSettings)
            If cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator Then TabControlSystemAddPage(User_SELECT, Me._LasUserForm, True)


            _LasProductionDataView = New ProductionDataView
            _LasProductionDataView.Init(Devices, Stations, AppSettings)
            TabControlSystemAddPage(ProductionView_SELECT, Me._LasProductionDataView, True)

            Me.stationView.TabControlSystem.Visible = True
        End If


        If strName = enumStationView.Debug Then
            Const IO_SELECT As String = "IO"
            Const Cylinder_SELECT As String = "Cylinder"
            Me.stationView.TabControlSystem.TabPages.Clear()
            _LasIOView = New ChildrenIOForm
            _LasIOView.cMainForm = Me
            _LasIOView.Init(Devices, Stations, AppSettings)
            _LasCylinderForm = New ChildrenCylinderForm
            _LasCylinderForm.cMainForm = Me
            _LasCylinderForm.Init(Devices, Stations, AppSettings)
            TabControlSystemAddPage(IO_SELECT, Me._LasIOView, True)
            TabControlSystemAddPage(Cylinder_SELECT, Me._LasCylinderForm, True)
            Me.stationView.TabControlSystem.Visible = True
            Me.stationView.ShowStation(False)
        End If

        If strName = enumStationView.ShortCut Then
            Const ShortCut_SELECT As String = "ShortCut"
            Me.stationView.TabControlSystem.TabPages.Clear()
            '  _LasShortCutView = New ChildrenShortCutForm
            _LasShortCutView.cMain = Me
            _LasShortCutView.Init(Devices, Stations, AppSettings)
            TabControlSystemAddPage(ShortCut_SELECT, Me._LasShortCutView, False)
            Me.stationView.TabControlSystem.Visible = True
            Me.stationView.ShowStation(False)
        End If
        stationView.Init(Devices, Stations, AppSettings)
    End Sub
    Private Sub ReadLanguage()
        btnStart.Text = mLanguage.Read("MainUI", "btnStart")
        btnClear.Text = mLanguage.Read("MainUI", "btnClear")
        btnCurrentArticle.Text = mLanguage.Read("MainUI", "btnCurrentArticle")
        btnCurrentSchedule.Text = mLanguage.Read("MainUI", "btnCurrentSchedule")
        lblSnMessage.Text = mLanguage.Read("MainUI", "lblSnMessage")
        btnSystem.Text = mLanguage.Read("MainUI", "btnSystem")
        btnStation.Text = mLanguage.Read("MainUI", "btnStation")
        btnDebug.Text = mLanguage.Read("MainUI", "btnDebug")
        btnLogin.Text = mLanguage.Read("MainUI", "btnLogin")
        btnShortCut.Text = mLanguage.Read("MainUI", "btnShortCut")

        Label_Article.Text = mLanguage.Read("MainUI", "Label_Article")
        Label_Name.Text = mLanguage.Read("MainUI", "Label_Name")
        Label_Index.Text = mLanguage.Read("MainUI", "Label_Index")
        Label_HW.Text = mLanguage.Read("MainUI", "Label_HW")
        Label_SW.Text = mLanguage.Read("MainUI", "Label_SW")
        Label_Customer.Text = mLanguage.Read("MainUI", "Label_Customer")
        Label_User.Text = mLanguage.Read("MainUI", "Label_User")
        Label_Level.Text = mLanguage.Read("MainUI", "Label_Level")
        ' Me.Text = mLanguage.Read("Controls", "MainForm")

        SetControls(Me.stationView.TabControlStations)
        SetControls(Me.stationView.TabControlSystem)
        Me.Text = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder + AppSettings.ConfigName, "GeneralInformation", "Title")

    End Sub

    Public Sub SetControls(ByVal cons As Control)
        For Each con As Control In cons.Controls
            con.Text = mLanguage.Read("StationView", con.Text)
            If con.Controls.Count > 0 Then
                SetControls(con)
            End If
        Next
    End Sub

    Private Sub btnCurrentArticle_Click(sender As Object, e As EventArgs) Handles btnCurrentArticle.Click
        cCurrentStationPage = enumStationView.Article
        ShowPanel(conARTICLE_VIEW)

    End Sub


    'Private Sub btnCurrentArticle_MouseMove(sender As Object, e As MouseEventArgs) Handles btnCurrentArticle.MouseMove
    '    Me.btnCurrentArticle.Text = "变种选择"
    'End Sub

    'Private Sub btnCurrentArticle_MouseLeave(sender As Object, e As EventArgs) Handles btnCurrentArticle.MouseLeave
    '    Me.btnCurrentArticle.Text = "当前变种"
    'End Sub

    Private Sub btnCurrentSchedule_Click(sender As Object, e As EventArgs) Handles btnCurrentSchedule.Click
        cCurrentStationPage = enumStationView.Schedule
        ShowPanel(conSCHEDULE_VIEW)

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Dim iCnt As Integer = 100
        btnClear.Enabled = False

        If _RunningInClearMode Then

            Me.scheduleView.btnAlternateScheduleAbort_Click(Me, Nothing)
            Do While iCnt > 0
                If Not _RunningInClearMode Then
                    Exit Do
                End If
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                iCnt = iCnt - 1
            Loop
        Else

            Me.scheduleView.ShortToRaiseClearMode()
            Do While iCnt > 0
                If _RunningInClearMode Then
                    Exit Do
                End If
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                iCnt = iCnt - 1
            Loop
        End If

        btnClear.Enabled = True

        btnClear.Focus()

    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click


        'If _PC_bulSwitchOnOff Then
        '    Me.btnStart.BackColor = Color.LightGreen
        'Else
        '    Me.btnStart.BackColor = SystemColors.ButtonFace
        'End If

    End Sub

    Private Sub btnStart_MouseDown(sender As Object, e As MouseEventArgs)

        _PC_bulSwitchOnOff = True
        'btnStart.Text = "正在关闭中"

    End Sub

    Private Sub btnStart_MouseUp(sender As Object, e As MouseEventArgs)

        _PC_bulSwitchOnOff = False
        'btnStart.Text = "开始"

    End Sub

    Private Sub btnStation_Click(sender As Object, e As EventArgs) Handles btnStation.Click
        sender.Enabled = False
        cCurrentStationPage = enumStationView.Station

        Me.stationView.TabControlSystem.Visible = False
        Me.stationView.TabControlStations.Visible = True
        Me.stationView.ShowStation(True)
        Me.stationView.lblMessage.Text = StationView.TabControlStationsName
        '  InitSystemPages("System")
        ShowPanel(conSTATION_VIEW)
        sender.Enabled = True
    End Sub

    Private Sub btnDebug_Click(sender As Object, e As EventArgs) Handles btnDebug.Click
        sender.Enabled = False
        cCurrentStationPage = enumStationView.Debug

        Me.stationView.TabControlSystem.Visible = False
        Me.stationView.TabControlStations.Visible = True
        Me.stationView.ShowStation(False)
        Me.stationView.lblMessage.Text = StationView.TabControlStationsName
        InitSystemPages(enumStationView.Debug)
        ShowPanel(conSTATION_VIEW)
        sender.Enabled = True
    End Sub

    Private Sub btnSystem_Click(sender As Object, e As EventArgs) Handles btnSystem.Click
        cCurrentStationPage = enumStationView.System
        Me.stationView.TabControlStations.Visible = False
        Me.stationView.TabControlSystem.Visible = True
        Me.stationView.ShowStation(False)
        Me.stationView.lblMessage.Text = StationView.TabControlSystemName
        InitSystemPages(enumStationView.System)
        ShowPanel(conSTATION_VIEW)

    End Sub

    Private Sub timCycle_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timCycle.Tick
        Dim Cycle As Double
        Dim swTime As Double = 0
        Static strLastStateInfo As New Dictionary(Of String, String)
        Try

            If Me.lblDate.Text <> Now.Date.ToLongDateString Then
                Me.lblDate.Text = Now.Date.ToLongDateString()
                Me.lblDate.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)
            End If

            Me.lblTime.Text = Now.DayOfWeek.ToString + " " + Now.ToLongTimeString
            Me.lblTime.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)

            timCycle.Enabled = False
            sw.Stop()
            swTime = sw.ElapsedMilliseconds
            If CycleCounter > 100 Then
                Cycle = CType(swTime, Double) / CType(CycleCounter, Double)
                CycleCounter = 0
                _LasCounterView.UpdateCycleTime(Cycle)
            End If

            'If StatusForm.Items("CycleTime").Text <> "CycleTime:" & mCycle.ToString("0.000") & " ms" Then StatusForm.Items("CycleTime").Text = "CycleTime:" & mCycle.ToString("0.000") & " ms"
            '-------------------------------------------------------------------

            For Each value In MyTwinCat.Keys
                If Not strLastStateInfo.ContainsKey(value) Then
                    strLastStateInfo.Add(value, "")
                End If
                If MyTwinCat(value) IsNot Nothing Then
                    Try
                        If IsNothing(MyTwinCat(value).StateInfo) Then
                            timCycle.Enabled = False
                        End If
                    Catch ex As Exception
                        PLCdisconnect = True
                        timCycle.Enabled = False
                    End Try

                End If

                If MyTwinCat(value) IsNot Nothing AndAlso MyTwinCat(value).StateInfo <> strLastStateInfo(value) Then
                    If MyTwinCat(value).StateInfo.ToUpper.Contains("RUN") Then
                        'StatusForm.Items(value).BackgroundImage = My.Resources.green
                        Me.MainPlcIndicator = enumINDICATOR_STATRUS.Green
                    ElseIf MyTwinCat(value).StateInfo.ToUpper.Contains("STOP") Then
                        'StatusForm.Items(value).BackgroundImage = My.Resources.red
                        Me.MainPlcIndicator = enumINDICATOR_STATRUS.Red
                    Else
                        'StatusForm.Items(value).BackgroundImage = My.Resources.gray
                        Me.MainPlcIndicator = enumINDICATOR_STATRUS.Gray
                    End If
                    strLastStateInfo(value) = MyTwinCat(value).StateInfo
                End If

            Next
            '-------------------------------------------------------------------
            sw.Reset()
            sw.Start()
            ' CycleCounter = 0
            timCycle.Enabled = True
        Catch ex As Exception
            timCycle.Enabled = Not PLCdisconnect
        End Try
    End Sub

    Private Sub lblRefPart_TextChanged(sender As Object, e As EventArgs) Handles lblRefPart.TextChanged

        If _lasErrorMessageSet Is Nothing Then
            _lasErrorMessageSet = New structErrorMessageSet
        End If

        _lasErrorMessageSet.Clear()
        _lasErrorMessageSet.iKeyUser = 0
        _lasErrorMessageSet.iErrorCode = 999
        _lasErrorMessageSet.strErrorSource = "LAS"
        _lasErrorMessageSet.strErrorType = CType(lblRefPart.Tag, enumHMI_ERROR_TYPE).ToString
        _lasErrorMessageSet.strErrorValue = ""
        _lasErrorMessageSet.strErrorTitle = ""
        _lasErrorMessageSet.strErrorMessage = lblRefPart.Text
        _lasErrorMessageSet.strRaisedTime = Date.Now.ToString("HH:mm:ss")

        bUpdateLasMessage = True
    End Sub

    Private Sub btnRedbox_Click(sender As Object, e As EventArgs)

        If _PC_bulRedboxLock Then

            mPassword.ChangeMode = False
            mPassword.ShowDialog()
            'mPassword.UserVerification = New StructUserVerification With {.VerificationType = enumUserVerificationType.PASSWORD_USERDEFINED, .Password = "3333"}
            If mPassword.PassWordValid Then
                _PC_bulRedboxLock = Not _PC_bulRedboxLock
            End If

        Else
            _PC_bulRedboxLock = Not _PC_bulRedboxLock
        End If

    End Sub

    Private Sub ShowScheduleReLoadLanguage()
        mLanguage.ReadContextMenuStrip(SchedulePanel.ContextMenuStrip_Schedule)
        InitScheduleData()
        CheckScheduleChecksum()
        SchedulePanel.ShowData()
    End Sub

    Private Sub InitScheduleData()
        Dim sResult As String = String.Empty
        Dim ID As String = String.Empty
        Dim sDescription As String = String.Empty
        Dim elementDictionary As New ScheduleDataElement
        Dim ManualCheckSum As Integer = 0
        Dim strKey As String = String.Empty

        SchedulePanel.ScheduleName.Clear()
        SchedulePanel.ScheduleData.Clear()

        '循环遍历添加ScheduleName
        For Each _scheElement In LocalSchedule.ArticleElements.Values
            If _scheElement.Key <> KostalScheduleKeys.KEY_USER_VERIFICATION Then

                '根据关键字选择是否添加Description子键
                If _scheElement.Key.IndexOf("PassST") >= 0 Then
                    sDescription = "Description" + _scheElement.Key.Substring(_scheElement.Key.IndexOf("PassST") + 4)
                    sResult = mLanguage.Read("Schedule", sDescription)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    SchedulePanel.ScheduleName.Add(sDescription, New ScheduleNameElement(sDescription, sResult, ScheduleNameType.ini))

                    '读取语言
                    sResult = mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    SchedulePanel.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
                    Continue For
                End If

                '添加CheckSum
                If _scheElement.Key.IndexOf(BaseScheduleDataElement.SecurityChecksum.ToString) >= 0 Then
                    sResult = mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    SchedulePanel.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))

                    '添加CheckSum
                    strKey = BaseScheduleDataElement.ManualChecksum.ToString
                    sResult = mLanguage.Read("Schedule", strKey)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = strKey
                    End If
                    SchedulePanel.ScheduleName.Add(strKey, New ScheduleNameElement(strKey, sResult, ScheduleNameType.Manual))
                    Continue For
                End If

                sResult = mLanguage.Read("Schedule", _scheElement.Key)
                If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                    sResult = _scheElement.Key
                End If
                SchedulePanel.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
            End If
        Next

        '循环遍历添加ScheduleData
        For Each _schedulelistElement As ArticleListElement In LocalSchedule.ArticleListElement.Values
            ID = _schedulelistElement.ID
            LocalSchedule.GetArticle_FromID(ID)
            elementDictionary = New ScheduleDataElement
            elementDictionary.Hide = False
            ManualCheckSum = 0

            '计算CheckSum
            For Each _ArticleElements As ArticleElement In LocalSchedule.ArticleElements.Values
                If _ArticleElements.Key.IndexOf("PassST") >= 0 Or _ArticleElements.Key.IndexOf("FailST") >= 0 Then

                    Dim cValue() As String = _ArticleElements.Data.Split(CChar(","))
                    For k = 0 To cValue.Length - 1
                        If IsNumeric(cValue(k)) Then
                            ManualCheckSum = ManualCheckSum + CInt(cValue(k))
                        End If
                    Next
                End If

            Next

            '循环遍历添加ScheduleData
            For Each _scheduleElement As ScheduleNameElement In SchedulePanel.ScheduleName.Values
                Select Case _scheduleElement.ValueFrom
                    Case ScheduleNameType.csv
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, LocalSchedule.ArticleElements(_scheduleElement.Key).Data))
                    Case ScheduleNameType.Manual
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, ManualCheckSum.ToString))
                    Case ScheduleNameType.ini
                        sResult = mLanguage.Read("Schedule", _scheduleElement.Key)
                        If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                            sResult = ""
                        End If
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, sResult))
                End Select
            Next
            SchedulePanel.ScheduleData.Add(ID, elementDictionary)
        Next

    End Sub

    Private Sub CheckScheduleChecksum()
        For Each scheduleDataelement As ScheduleDataElement In SchedulePanel.ScheduleData.Values

            For Each TypeElemet As BaseScheduleDataElement In [Enum].GetValues(GetType(BaseScheduleDataElement))
                If Not scheduleDataelement.ScheduleElement.ContainsKey([Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet)) Then
                    Throw New Exception("Please Add Element Name: " + [Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet))
                End If
            Next

            If SchedulePanel.CheckScheduleMode(scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ScheduleName.ToString).Value) Then
                If scheduleDataelement.ScheduleElement(BaseScheduleDataElement.SecurityChecksum.ToString).Value <> scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ManualChecksum.ToString).Value Then
                    Throw New Exception("Schedule: " + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ScheduleName.ToString).Value + " " &
                                        BaseScheduleDataElement.SecurityChecksum.ToString + ":" + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.SecurityChecksum.ToString).Value + " " &
                                        "Not equal " &
                                        BaseScheduleDataElement.ManualChecksum.ToString + ":" + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ManualChecksum.ToString).Value + " " &
                                        "Please Check schedule csv!"
                                        )
                End If
            End If
        Next
    End Sub

    Protected Function CheckScheduleMode(ByVal strName As String) As Boolean
        For Each TypeElemet As ScheduleMode In [Enum].GetValues(GetType(ScheduleMode))
            If strName.IndexOf([Enum].GetName(GetType(ScheduleMode), TypeElemet)) >= 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub ShowPanel(ByVal Name As String)
        Dim i As Integer
        For i = 0 To Me.panMain.Controls.Count - 1
            Me.panMain.Controls.Item(i).Visible = False
        Next

        Me.panMain.Controls.Item(Name).Visible = True
        Me.panMain.Controls.Item(Name).BringToFront()
    End Sub


    Private Sub RefreshBtnColor()
        SetBackColor(btnSystem, IIf(cCurrentStationPage = enumStationView.System, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnSystem, IIf(cCurrentStationPage = enumStationView.System, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnSystem, IIf(cCurrentStationPage = enumStationView.OverView, True, False))
        SetBackColor(btnStation, IIf(cCurrentStationPage = enumStationView.Station, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnStation, IIf(cCurrentStationPage = enumStationView.Station, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnStation, IIf(cCurrentStationPage = enumStationView.OverView, True, False))
        SetBackColor(btnDebug, IIf(cCurrentStationPage = enumStationView.Debug, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnDebug, IIf(cCurrentStationPage = enumStationView.Debug, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnDebug, IIf(cCurrentStationPage = enumStationView.OverView And cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator And _PlcAutoManual = enumPLC_AUTO_MANUAL.Manual, True, False))
        SetBackColor(btnShortCut, IIf(cCurrentStationPage = enumStationView.ShortCut, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnShortCut, IIf(cCurrentStationPage = enumStationView.ShortCut, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnShortCut, IIf(cCurrentStationPage = enumStationView.OverView, True, False))
        SetBackColor(btnLogin, IIf(cCurrentStationPage = enumStationView.Login, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnLogin, IIf(cCurrentStationPage = enumStationView.Login, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnLogin, IIf(cCurrentStationPage = enumStationView.OverView, True, False))
        SetBackColor(btnCurrentArticle, IIf(cCurrentStationPage = enumStationView.Article, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnCurrentArticle, IIf(cCurrentStationPage = enumStationView.Article, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnCurrentArticle, IIf(cCurrentStationPage = enumStationView.OverView, True, False))
        SetBackColor(btnCurrentSchedule, IIf(cCurrentStationPage = enumStationView.Schedule, KostalLasColors.GREEN, KostalLasColors.BUTTONFACE))
        SetForeColor(btnCurrentSchedule, IIf(cCurrentStationPage = enumStationView.Schedule, KostalLasColors.WHITE, KostalLasColors.KOSTALBLUE))
        SetEnable(btnCurrentSchedule, IIf(cCurrentStationPage = enumStationView.OverView, True, False))

    End Sub

    Private Sub SetBackColor(ByVal cControl As Control, ByVal cColor As Color)
        If cControl.BackColor <> cColor Then
            cControl.BackColor = cColor
        End If

    End Sub

    Private Sub SetForeColor(ByVal cControl As Control, ByVal cColor As Color)
        If cControl.ForeColor <> cColor Then
            cControl.ForeColor = cColor
        End If
    End Sub

    Private Sub SetEnable(ByVal cControl As Control, ByVal bResult As Boolean)
        If cControl.Enabled <> bResult Then
            cControl.Enabled = bResult
        End If
    End Sub

    Private Sub MainUIAddPage(ByVal ViewName As String, ByVal Panel As Object)
        Dim _Panel As Panel

        _Panel = Panel.GetPannel
        _Panel.Dock = DockStyle.Fill
        _Panel.Name = ViewName
        _Panel.Visible = False
        Me.panMain.Controls.Add(_Panel)
    End Sub

    Private Sub TabControlSystemAddPage(ByVal TagName As String, ByVal Panel As Object, ByVal bResize As Boolean)
        Dim _Panel As Panel

        Me.stationView.TabControlSystem.TabPages.Add(TagName, mLanguage.Read("StationView", TagName))

        _Panel = Panel.GetPannel
        _Panel.Width = Me.stationView.TabControlSystem.TabPages(TagName).Width + 4
        _Panel.Height = Me.stationView.TabControlSystem.TabPages(TagName).Height + 2
        _Panel.Location = New Point(-1, 0)
        _Panel.Dock = DockStyle.Fill
        If TagName <> "ShortCut" Then _Panel.BackColor = System.Drawing.SystemColors.ControlLight
        _Panel.BackColor = Color.White
        _Panel.Parent = Me.stationView.TabControlSystem.TabPages(TagName)
        Me.stationView.TabControlSystem.Refresh()
        _Panel.Refresh()
        If bResize Then
            cFormFontResize.SetControls(cFormFontResize.CurrentRate, _Panel)
        Else
            If Not ListResie.ContainsKey(TagName) Then
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, _Panel)
                ListResie.Add(TagName, True)
            End If
        End If
    End Sub

    Public Sub LoadLanguage（）
        Dim l As Integer

        For l = 1 To mLanguage.LanguageElement.LanguageFileName_Count
            CBLanguage.Items.Add(mLanguage.LanguageElement.LanguageFileName(l))
        Next

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        sender.Enabled = False
        cCurrentStationPage = enumStationView.Login
        LoginView.Init(Devices, Stations, AppSettings)
        ShowPanel(conLoging_VIEW)
        sender.Enabled = True
    End Sub

    Private Sub btnShortCut_Click(sender As Object, e As EventArgs) Handles btnShortCut.Click
        sender.Enabled = False
        cCurrentStationPage = enumStationView.ShortCut
        Me.stationView.TabControlSystem.Visible = False
        Me.stationView.TabControlStations.Visible = True
        Me.stationView.ShowStation(False)
        Me.stationView.lblMessage.Text = StationView.TabControlStationsName
        InitSystemPages(enumStationView.ShortCut)
        ShowPanel(conSTATION_VIEW)
        sender.Enabled = True
    End Sub
    Private Sub Quit()
        QuitForm(cCurrentStationPage)
    End Sub
    Private Sub QuitForm(ByRef cStationView As enumStationView)
        Select Case cStationView
            Case enumStationView.Station
                Me.stationView.TabControlStations.Visible = False
                Me.stationView.TabControlSystem.Visible = False
                'Me.stationView.ShowStation(Not Me.stationView.TabControlSystem.Visible)
                ShowPanel(conOVERVIEW)
            Case enumStationView.System
                WatchWT.Quit(Devices)
                _LasErrorCodeListForm.Quit(Devices)
                _LasPlcMessageListForm.Quit(Devices)
                _LasUserForm.Quit(Devices)
                _LasPlcParameterForm.Quit(Devices)
                _LasProductionDataView.Quit(Devices)
                Me.stationView.TabControlStations.Visible = False
                Me.stationView.TabControlSystem.Visible = False
                '  Me.stationView.ShowStation(Not Me.stationView.TabControlSystem.Visible)
                ShowPanel(conOVERVIEW)



            Case enumStationView.Debug
                _LasIOView.Quit(Devices)
                _LasCylinderForm.Quit(Devices)
                Me.stationView.TabControlStations.Visible = False
                Me.stationView.TabControlSystem.Visible = False
                ' Me.stationView.ShowStation(Not Me.stationView.TabControlSystem.Visible)
                ShowPanel(conOVERVIEW)
            Case enumStationView.ShortCut
                _LasShortCutView.Quit(Devices)
                Me.stationView.TabControlStations.Visible = False
                Me.stationView.TabControlSystem.Visible = False
                ' Me.stationView.ShowStation(Not Me.stationView.TabControlSystem.Visible)
                ShowPanel(conOVERVIEW)
        End Select
        cStationView = enumStationView.OverView

    End Sub

    Private Sub MainUI_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        overView.Init(Devices, Stations, AppSettings)
        overView.InitLanguage()
    End Sub

    Private Sub MainUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        cFormFontResize.WinFromH = System.Windows.Forms.Screen.GetWorkingArea(Me).Height
        If Me.Height = cFormFontResize.WinFromH Then
            cFormFontResize.newH = Me.Height
        End If
    End Sub

    Public Sub MainForm_Run() Implements IMainForm.MainForm_Run
        Run()
    End Sub

    Public Sub MainForm_ReadLanguage() Implements IMainForm.MainForm_ReadLanguage
        ReadLanguage()
    End Sub

    Public Sub MainForm_Show() Implements IMainForm.MainForm_Show
        Me.Show()
    End Sub

    Public Sub MainForm_Quit() Implements IMainForm.MainForm_Quit
        Me.Quit()
    End Sub

    Public Sub MainForm_Dispose() Implements IMainForm.MainForm_Dispose
        Me.Dispose()
    End Sub

    Public Sub MainForm_ResetClear() Implements IMainForm.MainForm_ResetClear

        If _RunningInClearMode Then
            Me.scheduleView.btnAlternateScheduleAbort_Click(Me, Nothing)
        End If
        btnClear.Focus()
    End Sub

    Public Sub MainForm_AddClear() Implements IMainForm.MainForm_AddClear

        If _RunningInClearMode Then
        Else
            Me.scheduleView.ShortToRaiseClearMode()
        End If
    End Sub

    Public Function InvokeAction(method As [Delegate], ParamArray args() As Object) As Object Implements IMainForm.InvokeAction
        SyncLock _Object
            Try
                'If bClosing Then Return True
                Me.BeginInvoke(method, args)
                Return True
            Catch
                Return False
            End Try
        End SyncLock
    End Function



    'Protected Overrides Function TurningFromOffToOn() As Boolean

    '    Dim listtResponse As New List(Of Prompts.UserResponse)

    '    listtResponse.Add(New Prompts.UserResponse("Y", "Yes"))

    '    listtResponse.Add(New Prompts.UserResponse("N", "No"))

    '    Dim myPrompt = Me.MyStation.Prompts.Set(Prompts.IPrompt.PromptType.Question, Prompts.IPrompt.DisplayOptions.None, "The Machine State is on, are you ready?", listtResponse.ToArray)


    '    Dim responeid As String = myPrompt.WaitForResponse

    '    If responeid = "Y" Then

    '        Return True

    '    End If

    '    Return False
    '    Dim buttonText As String
    '    buttonText = Me.MyStation.Localizer.GetLocalizedString("Abort")


    '    Me.MyStation.Runner.Configuration.Abort.ResponseText = "Exit"

    '    'Dim lstDIs As New List(Of String)
    '    'lstDIs.Add("LABJACK.DI1")
    '    'lstDIs.Add("LABJACK.DI2")
    '    'Me.MyStation.Runner.Configuration.Abort.ResponseChannelIds = lstDIs.ToArray
    '    'Me.MyStation.Runner.Configuration.BlinkTimeOn = 500
    '    'Me.MyStation.Runner.Configuration.BlinkTimeOff = 250

    '    Return True

    'End Function


End Class

Public Enum enumStationView
    NONE = 1
    OverView
    System
    Station
    Debug
    ShortCut
    Article
    Schedule
    Login
End Enum