Imports System.Threading
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Drawing
Imports Kostal.Las.Base
Imports System.Timers
Imports System.Xml
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class MainForm_Bosh
    Inherits System.Windows.Forms.Form
    Implements IMainForm

    Public Event LanguageChangedTo(ByVal Name As String)
    Public Event IamClosing As IMainForm.MainForm_IamClosingEventHandler Implements IMainForm.MainForm_IamClosing
    Public mPassword As New PassWordForm
    Public mFileHandler As New FileHandler
    Public mXmlHandler As New XmlHandler
    Public FileHandler As New FileHandler
    Public AppArticleUsed As Boolean
    Public i As New Station
    Public Log As Logger
    Public mLanguage As Language
    Public CAQ_Label As ToolStripStatusLabel
    Public LineControl_Label As ToolStripStatusLabel
    Public HelpReader As New ToolStripMenuItemReader
    Public LocalSchedule As Base.Schedule
    Public AppSettings As Settings
    Public AppArticle As Article
    Public LineArticleCounter As ArticleCounter
    Public LineMaintenance As Maintenance
    Public AboutBox As New AboutBox
    Public WatchWT As New WtStatusForm
    Public CycleCounter As Long
    Public sw As New Stopwatch
    Public PLCdisconnect As Boolean = False
    Public MyTwinCat As New Dictionary(Of String, TwinCatAds)
    Public ScheduleView As New ScheduleViewFrom
    Public ChangeArtcile As Boolean
    Public Const sName As String = "mMainForm"
    Public Stations As Dictionary(Of String, IStationTypeBase)
    Public Devices As Dictionary(Of String, Object)
    Private X As Double
    Private Y As Double
    Private newx As Double
    Private newy As Double
    Private _LastStatusHeight As Integer
    Private _LastStatusTop As Integer
    Dim _Screen As Screen
    Public cFormFontResize As New clsFormFontResize
    Private strStationPicture As String = ""
    Private lListStationView As New Dictionary(Of String, clsPictureComponentCfg)
    Protected g1 As Graphics
    Protected bmp1 As Bitmap
    Protected img1 As Image
    Protected scaleX1 As Single
    Protected scaleY1 As Single
    Protected rectF1 As RectangleF
    Private iMaxX As Integer = 0
    Private iMaxY As Integer = 0
    Private iRatex As Double = 1
    Private iRateY As Double = 1
    Private cRectangle As Rectangle
    Private bLastIndex As Integer = 0
    Private OverviewInfoForm As OverviewInfoForm
    Private bEditMode As Boolean = False
    Private cProductionDataView As ProductionDataView
    Protected xmlHandler As New XmlHandler
    Private cErrorCodeListForm As ChildrenErrorCodeListForm
    Private _StationCfg As StationCfg
    Private _Object As New Object
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        cFormFontResize.cons = Me
        Me.WindowState = FormWindowState.Normal
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.Top = 0
        Me.Left = 0
        Me.Width = Screen.PrimaryScreen.WorkingArea.Width
        Me.Height = Screen.PrimaryScreen.WorkingArea.Height
    End Sub
    Public Property MainForm_Timer As System.Windows.Forms.Timer Implements IMainForm.MainForm_Timer
        Get
            Return timCycle
        End Get
        Set(value As System.Windows.Forms.Timer)
            timCycle = value
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

    Public Property MainForm_TabControlStations As TabControl Implements IMainForm.MainForm_TabControlStations
        Get
            Return TabControlStations
        End Get
        Set(value As TabControl)
            TabControlStations = value
        End Set
    End Property

    Public Property MainForm_ScheduleSelectView As IScheduleUI Implements IMainForm.MainForm_ScheduleSelectView
        Get
            Return Nothing
        End Get
        Set(value As IScheduleUI)
        End Set
    End Property

    Public Property MainForm_lblCurrentSchedule As Label Implements IMainForm.MainForm_lblCurrentSchedule
        Get
            Return Nothing
        End Get
        Set(value As Label)
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
            Return lblRefPart
        End Get
        Set(value As Label)
            lblRefPart = value
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
            Return Button_Reset
        End Get
        Set(value As Button)
            Button_Reset = value
        End Set
    End Property

    Public Property MainForm_CaqIndicator As enumINDICATOR_STATRUS Implements IMainForm.MainForm_CaqIndicator
        Get
            Return Nothing
        End Get
        Set(value As enumINDICATOR_STATRUS)

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

    Private Property IMainForm_MainForm_StationOverviewInfo As Dictionary(Of Integer, structStationOverviewInfo) Implements IMainForm.MainForm_StationOverviewInfo
        Get
            Return Nothing
        End Get
        Set(value As Dictionary(Of Integer, structStationOverviewInfo))
        End Set
    End Property

    Private Property IMainForm_MainForm_ErrorMessageSet As structErrorMessageSet Implements IMainForm.MainForm_ErrorMessageSet
        Get
            Return Nothing
        End Get
        Set(value As structErrorMessageSet)
        End Set
    End Property

    Public Property MainForm_MainLogger As ListBox Implements IMainForm.MainForm_MainLogger
        Get
            Return MainLogger
        End Get
        Set(value As ListBox)
            MainLogger = value
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

    Public Property MainForm_NewPartStartion As NewPartStation Implements IMainForm.MainForm_NewPartStartion
        Get
            Return Nothing
        End Get
        Set(value As NewPartStation)
        End Set
    End Property

    Public Property MainForm_LinecotrolIndicator As enumINDICATOR_STATRUS Implements IMainForm.MainForm_LinecotrolIndicator
        Get
            Return Nothing
        End Get
        Set(value As enumINDICATOR_STATRUS)
        End Set
    End Property

    Public Property MainForm_stationView As Object Implements IMainForm.MainForm_stationView
        Get
            Return Nothing
        End Get
        Set(value As Object)
        End Set
    End Property

    Public Property MainForm_btnResetFail As Button Implements IMainForm.MainForm_btnResetFail
        Get
            Return New Button
        End Get
        Set(value As Button)

        End Set
    End Property

    Public Property MainForm_btnClear As Button Implements IMainForm.MainForm_btnClear
        Get
            Return New Button
        End Get
        Set(value As Button)
            'Throw New NotImplementedException()
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
            Throw New NotImplementedException()
        End Get
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PlcAutoManual As enumPLC_AUTO_MANUAL Implements IMainForm.PlcAutoManual
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As enumPLC_AUTO_MANUAL)
            Throw New NotImplementedException()
        End Set
    End Property

    Private Delegate Sub dGroupHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(30, 70, 125)
        Me.timCycle.Enabled = True
        picBoxMain.Left = CInt(grpPicture.Width / 2 - picBoxMain.Width / 2)
        picBoxMain.Top = CInt(grpPicture.Height / 2 - picBoxMain.Height / 2)
    End Sub
    Private Sub AddStationData()

        ListView_StationData.View = View.Details
        ListView_StationData.BeginUpdate()
        ListView_StationData.GridLines = True
        For Each element As clsPictureComponentCfg In lListStationView.Values
            Dim lvi As New ListViewItem
            lvi.Text = element.strName
            ListView_StationData.Items.Add(lvi)
        Next
        ListView_StationData.EndUpdate()
    End Sub
    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then MeExit()
    End Sub

    Private Sub MeExit()
        picBoxMain.BringToFront()
        '  g1.Dispose()
        '  bmp1.Dispose()
        '  img1.Dispose()
        '  picBoxMain.Image = Nothing
        'System.Threading.Thread.Sleep(500)
        'Me.Dispose()
        SkinEngine1.RemoveForm(Me, True)
        SkinEngine1.Dispose()
        RaiseEvent IamClosing()
        Log.Logger(i, "sucessfull", "Exit")
    End Sub

    'Public Overloads Sub Dispose()
    '    RaiseEvent IamClosing()
    '    MyBase.Dispose()
    'End Sub

    Private Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Me.Stations = Stations
        Me.Devices = Devices

        i = New Station(Me.Name)
        mLanguage = CType(Devices(Language.Name), Language)
        AppSettings = CType(Devices(Settings.Name), Settings)
        AppArticle = CType(Devices(Article.Name), Article)
        LineMaintenance = Devices(Maintenance.sName)
        Log = New Logger(AppSettings)
        Log.Logger(i, "Init Run", "MainForm")
        If AppSettings.LineType > 0 Then
            LocalSchedule = New Base.Schedule(i, AppSettings, mLanguage)
            LocalSchedule.Init()
        End If
        _StationCfg = Devices(StationCfg.Name)

        OverviewInfoForm = New OverviewInfoForm
        OverviewInfoForm.Init(Devices, Stations)

        LineArticleCounter = Devices(ArticleCounter.sName)

        WatchWT.Init(Devices, Stations)
        SetStatusStrip()
        SetMenu()

        ReLoadLanguage()
        InitCurrentArticle()
        mPassword.Init(i, AppSettings, "UserPassWord")

        ' picBoxMain.Image = New Bitmap(AppSettings.PicFolder + "layout.bmp")
        'DG_Article.Rows.Clear()
        Log.Logger(i, "Init sucessfull", "MainForm")
        ChangeFormSize()
        If Not AppSettings.LineType > 0 Then
            ShowWtDataToolStripMenuItem.Visible = False
            ShowScheduleToolStripMenuItem.Visible = False
            ShowMaintainToolStripMenuItem.Visible = False
        End If
        If Not xmlHandler.HasOverView(AppSettings.ConfigFolder, AppSettings.ConfigName) Then
            ShowStationOverViewToolStripMenuItem.Visible = False
        End If

        If Not _StationCfg.HasStation(StationType.SaveProduction) Then
            ShowProductionViewToolStripMenuItem.Visible = False
        End If

        If Not _StationCfg.HasStation(StationType.PLCAlarm) Then
            ShowErrorCodeViewToolStripMenuItem.Visible = False
        End If

        CreateStation()
        ShowStationView(picBoxMain.Width, picBoxMain.Height)
        AddStationData()
        lblRefPart.Hide()
        AddHandler picBoxMain.Resize, AddressOf picBoxMain_Resize
        Return True
    End Function

    Public Sub SetStatusStrip()

        Dim mToolStripStatusLabel As ToolStripStatusLabel
        Dim MyVersion As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim MyFileVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion

        StatusForm.Items.Clear()

        'added by wang65 2015.06.13
        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "tssKostal"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = "KOSTAL Co."
        mToolStripStatusLabel.Image = My.Resources.logo_screen_145px2
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "ApplicationFolder"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = AppSettings.ApplicationFolder
        ' StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "Version"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All

        mToolStripStatusLabel.Text = "Main Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(AppSettings.ApplicationFolder + "Kostal.Las.Launcher.exe").FileVersion
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "Base"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All

        mToolStripStatusLabel.Text = "Kostal.Las.Base.dll Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(AppSettings.LibFolder + "Kostal.Las.Base.dll").FileVersion
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "ArticleProvider"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All

        mToolStripStatusLabel.Text = "Kostal.Las.ArticleProvider.dll Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(AppSettings.LibFolder + "Kostal.Las.ArticleProvider.dll").FileVersion
        StatusForm.Items.Add(mToolStripStatusLabel)

        For Each value In AppSettings.PLCConfig.Keys
            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = value
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = value
            mToolStripStatusLabel.Image = My.Resources.gray
            StatusForm.Items.Add(mToolStripStatusLabel)
        Next

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "CycleTime"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = AppSettings.ApplicationFolder & AppSettings.ConfigName
        StatusForm.Items.Add(mToolStripStatusLabel)
    End Sub

    Private Function SetMenu() As Boolean

        Dim l As Integer, NewMenuItem As ToolStripMenuItem

        For l = 1 To mLanguage.LanguageElement.LanguageFileName_Count
            NewMenuItem = New ToolStripMenuItem
            NewMenuItem.Name = Me.MenuLanguage.Name & "_" & mLanguage.LanguageElement.LanguageFileName(l)
            NewMenuItem.Text = mLanguage.LanguageElement.LanguageFileName(l)
            NewMenuItem.Tag = mLanguage.LanguageElement.LanguageFileName(l)
            Me.MenuLanguage.DropDownItems.Add(NewMenuItem)
            AddHandler NewMenuItem.Click, AddressOf Language_Change
            If mLanguage.LanguageElement.LanguageFileName(l) = mLanguage.LanguageElement.SelectedLanguageFileName Then NewMenuItem.Checked = True 'added by wang65 2015.06.12
            NewMenuItem = Nothing
        Next

        If IsNothing(AppSettings.HelpFiles) Then Return False
        If AppSettings.HelpFiles.Count = 0 Then Return False

        For l = 1 To AppSettings.HelpFiles.Count
            NewMenuItem = New ToolStripMenuItem
            NewMenuItem.Name = "HelpFile_" + l.ToString
            NewMenuItem.Text = NewMenuItem.Name
            NewMenuItem.Tag = l
            Me.HelpToolStripMenuItem.DropDownItems.Add(NewMenuItem)
            AddHandler NewMenuItem.Click, AddressOf CallHelp
            NewMenuItem = Nothing
        Next

        Return True

    End Function

    Private Sub CallHelp(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If IsNothing(AppSettings.HelpFiles) Then Return
        If AppSettings.HelpFiles.Count = 0 Then Return

        Dim Item As New ToolStripMenuItem

        Try
            Item = CType(sender, ToolStripMenuItem)
            Shell(AppSettings.HelpApplication(CInt(Item.Tag)) & " " & Chr(34) & AppSettings.HelpFolder & AppSettings.HelpFiles(CInt(Item.Tag)) & Chr(34), AppWinStyle.NormalFocus)
        Catch ex As Exception
            i.StepTextLine = "CallHelp"

            If Not IsNothing(Item) Then
                i.Text = Item.Name
            Else
                i.Text = "No Item defined"
            End If

            Log.Logger(i)

        End Try

    End Sub

    Public Sub ReLoadLanguage()
        mLanguage.ReadControlText(Me)
        If AppSettings.LineType > 0 Then
            ShowScheduleReLoadLanguage()
        End If
        WatchWT.LanguageInit()
        LineArticleCounter.LanguageInit()
        StationColumnHeader.Text = mLanguage.Read("Controls", "StationColumnHeader")
        '   Me.Text = mLanguage.Read("Controls", "MainForm")
        Me.Text = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder + AppSettings.ConfigName, "GeneralInformation", "Title")
    End Sub

    Private Sub Language_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim Item As ToolStripMenuItem

        mXmlHandler.SetGeneralInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, mLanguage.LanguageElement.Section_LanguageFileNames, mLanguage.LanguageElement.KeyWord_SelectedLanguage, CType(sender, ToolStripMenuItem).Tag.ToString)

        mLanguage.SetAppLanguage.ReloadLanguage()
        For Each _Station As IStationTypeBase In Stations.Values
            _Station.ReLoadLanguage()
        Next

        ReLoadLanguage()
        InitCurrentArticle()

        For Each Item In Me.HelpToolStripMenuItem.DropDownItems
            Try
                HelpReader.ReadToolStripMenuItem(AppSettings.LngFolder, mLanguage.LanguageElement.SelectedLanguageFileName & AppSettings.Extension_LanguageFile, Item)
            Catch ex As Exception
                If Not IsNothing(Item) Then

                End If
            End Try
        Next

        'added by wang65 2015.06.12
        For Each Item In Me.MenuLanguage.DropDownItems
            Item.Checked = False
        Next
        CType(sender, ToolStripMenuItem).Checked = True
    End Sub

    Private Sub MenuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox.ShowDialog()
    End Sub

    Private Sub MenuMinimized_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub MenuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExit.Click
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then MeExit()
    End Sub

    Private Sub MenuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuChangePassword.Click
        mPassword.ChangeMode = True
        mPassword.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AboutBox.ShowDialog()
    End Sub

    Private Sub ShowWtDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowWtDataToolStripMenuItem.Click
        WatchWT.ShowWtData = Not WatchWT.ShowWtData
    End Sub
    Public Sub Run()
        WatchWT.Run()
        ScheduleView.Run()
        LineArticleCounter.Run()
        LineMaintenance.Run()
        OverviewInfoForm.Run()
    End Sub

    Private Sub timCycle_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timCycle.Tick
        Dim Cycle As Double, mCycle As Double
        Dim swTime As Double = 0
        Static strLastStateInfo As New Dictionary(Of String, String)
        Try
            timCycle.Enabled = False
            sw.Stop()
            swTime = sw.ElapsedMilliseconds
            If CycleCounter > 100 Then
                Cycle = CType(swTime, Double) / CType(CycleCounter, Double)
                CycleCounter = 0
                mCycle = CType(Cycle, Double)
                If StatusForm.Items("CycleTime").Text <> "CycleTime:" & mCycle.ToString("0.000") & " ms" Then StatusForm.Items("CycleTime").Text = "CycleTime:" & mCycle.ToString("0.000") & " ms"
            End If
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
                        StatusForm.Items(value).Image = My.Resources.green
                    ElseIf MyTwinCat(value).StateInfo.ToUpper.Contains("STOP") Then
                        StatusForm.Items(value).Image = My.Resources.red
                    Else
                        StatusForm.Items(value).Image = My.Resources.gray
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

    Private Sub GroupHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Tag As String, Tab As New TabPage

        TabControlStations.Visible = True

        Try
            Tag = CType(sender, Label).Name.ToString.Substring(CType(sender, Label).Name.ToString.Length - 2)

            ' For l = 1 To TabControlStations.TabCount - 1
            ' If TabControlStations.TabPages(l).Tag.ToString = Tag Then
            TabControlStations.SelectTab(CInt(Tag) - 1)
            'Return
            ' End If
            ' Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub grpStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.TabControlStations.Visible = False
    End Sub

    Private Sub ScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _Label As New Label

        _Label.Tag = 99
        GroupHandler(_Label, Nothing)

    End Sub

    Private Sub ShowCounterToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowCounterToolStripMenuItem.Click
        LineArticleCounter.ShowCounter = Not LineArticleCounter.ShowCounter
    End Sub
    Private Sub ShowMaintainToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowMaintainToolStripMenuItem.Click
        LineMaintenance.ShowMaintain = Not LineMaintenance.ShowMaintain
    End Sub

    Private Sub MainBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub MainForm_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

    End Sub

    Private Sub InitScheduleData()
        Dim sResult As String = String.Empty
        Dim ID As String = String.Empty
        Dim sDescription As String = String.Empty
        Dim elementDictionary As New ScheduleDataElement
        Dim ManualCheckSum As Integer = 0
        Dim strKey As String = String.Empty

        ScheduleView.ScheduleName.Clear()
        ScheduleView.ScheduleData.Clear()

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
                    ScheduleView.ScheduleName.Add(sDescription, New ScheduleNameElement(sDescription, sResult, ScheduleNameType.ini))

                    '读取语言
                    sResult = mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
                    Continue For
                End If

                '添加CheckSum
                If _scheElement.Key.IndexOf(BaseScheduleDataElement.SecurityChecksum.ToString) >= 0 Then
                    sResult = mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))

                    '添加CheckSum
                    strKey = BaseScheduleDataElement.ManualChecksum.ToString
                    sResult = mLanguage.Read("Schedule", strKey)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = strKey
                    End If
                    ScheduleView.ScheduleName.Add(strKey, New ScheduleNameElement(strKey, sResult, ScheduleNameType.Manual))
                    Continue For
                End If

                sResult = mLanguage.Read("Schedule", _scheElement.Key)
                If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                    sResult = _scheElement.Key
                End If
                ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
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
                    If IsNumeric(_ArticleElements.Data) Then
                        ManualCheckSum = ManualCheckSum + CInt(_ArticleElements.Data)
                    End If
                End If
            Next

            '循环遍历添加ScheduleData
            For Each _scheduleElement As ScheduleNameElement In ScheduleView.ScheduleName.Values
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
            ScheduleView.ScheduleData.Add(ID, elementDictionary)
        Next

    End Sub

    Private Sub CheckScheduleChecksum()
        For Each scheduleDataelement As ScheduleDataElement In ScheduleView.ScheduleData.Values

            For Each TypeElemet As BaseScheduleDataElement In [Enum].GetValues(GetType(BaseScheduleDataElement))
                If Not scheduleDataelement.ScheduleElement.ContainsKey([Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet)) Then
                    Throw New Exception("Please Add Element Name: " + [Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet))
                End If
            Next

            If ScheduleView.CheckScheduleMode(scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ScheduleName.ToString).Value) Then
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

    Private Sub ShowScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowScheduleToolStripMenuItem.Click
        If ScheduleView.ShowSchedule Then
            ScheduleView.ShowSchedule = Not ScheduleView.ShowSchedule
        Else
            Dim UserVerification As New StructUserVerification
            UserVerification.VerificationType = enumUserVerificationType.PASSWORD_USERDEFINED
            UserVerification.Password = "dotnet"
            mPassword.ChangeMode = False
            mPassword.UserVerification = UserVerification
            mPassword.ShowDialog()
            If mPassword.PassWordValid Then
                ScheduleView.ShowSchedule = Not ScheduleView.ShowSchedule
            End If
            UserVerification.VerificationType = enumUserVerificationType.PASSWORD_APPLICATION
            mPassword.UserVerification = UserVerification
        End If

    End Sub

    Private Sub ShowScheduleReLoadLanguage()
        mLanguage.ReadContextMenuStrip(ScheduleView.ContextMenuStrip_Schedule)
        InitScheduleData()
        CheckScheduleChecksum()
        If ScheduleView.ShowSchedule Then
            ScheduleView.ShowData()
        End If
    End Sub

    Public Function InitCurrentArticle() As Boolean
        Dim sResult As String, _Element As New ArticleListElement, _ArticleElement As New ArticleElement
        sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "AppArticleNotUsed")
        AppArticleUsed = Trim(sResult.ToUpper) = "TRUE"
        DG_Article.Rows.Clear()
        If Not AppArticleUsed Then
            Log.Logger(i, "Application Article not in use", "InitCurrentArticle")
            gbArticle.Visible = True
            _CBArticle.Text = ""
            Return True
        End If
        gbArticle.Visible = True
        sResult = mXmlHandler.GetSectionInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT)
        If Not AppArticle.ArticleListElement.ContainsKey(sResult.Trim) And AppArticle.ArticleListElement.Count >= 1 Then
            sResult = AppArticle.ArticleListElement(AppArticle.ArticleListElement.Keys(0)).ID
        End If
        If AppArticle.ArticleListElement.ContainsKey(sResult) Then
            AppArticle.GetArticle_FromID(sResult)
            InitArticleElement(sResult)
            _CBArticle.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
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
        DG_Article.Rows.Clear()
        For Each _ArticleElement As ArticleElement In AppArticle.ArticleElements.Values
            If _ArticleElement.Visible Then
                DG_Article.Rows.Add(_ArticleElement.Name, _ArticleElement.Data)
            End If
        Next
        Return True
    End Function



    Private Sub CBArticle_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles CBArticle.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call btnArticle_Click(sender, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then
            _CBArticle.Text = AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
            _lstMatchBox.Visible = False
        Else
            ChangeArtcile = True
        End If
    End Sub

    Private Sub btnArticle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArticle.Click
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then
            AppArticle.GetArticle_FromID(_CBArticle.Text)
            InitArticleElement(_CBArticle.Text)
            mXmlHandler.SetGeneralInformation(AppSettings.LogFolder, AppSettings.ApplicationActive, "Article", CON_KEYWORD_SELVARIANT, _CBArticle.Text)
            Log.Logger(i, False, mLanguage.LanguageElement.GetTextLine(i, enumLK_TEXT.LK_TEXT_CHANGEARTICLE, AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data), "_btnArticle_Click")
            MsgBox(i.Text, MsgBoxStyle.Information, i.IdString & " - " & i.StepTextLine)
        End If
    End Sub

    Private Sub CBArticle_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBArticle.TextChanged
        If ChangeArtcile Then _lstMatchBoxRefresh()
    End Sub

    Private Sub _lstMatchBoxRefresh()
        _lstMatchBox.Visible = True
        ChangeArtcile = False
        _lstMatchBox.Items.Clear()
        _lstMatchBox.BringToFront()
        If Not IsNothing(AppArticle) Then
            For Each Element As ArticleListElement In AppArticle.ArticleListElement.Values
                If Element.ID.IndexOf(_CBArticle.Text) >= 0 Then
                    _lstMatchBox.Items.Add(Element.ID)
                End If
            Next
        End If
    End Sub

    Private Sub lstMatchBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMatchBox.Click
        On Error Resume Next
        _CBArticle.Text = _lstMatchBox.Items.Item(_lstMatchBox.SelectedIndex).ToString
        _lstMatchBox.Visible = False
    End Sub

    Public Sub ChangeFormSize()
        Dim sResult As String
        sResult = mXmlHandler.GetSectionInformation(AppSettings.ApplicationFolder, AppSettings.RootIniName, "Environment", "Screen")
        Try
            _Screen = Screen.AllScreens(CInt(sResult))
        Catch ex As Exception
            _Screen = Screen.AllScreens(0)
        End Try
        X = 1292
        Y = 973
        _LastStatusHeight = 0
        _LastStatusTop = 0
        setTag(Me)
        Me.Left = _Screen.WorkingArea.Left
        Me.Width = _Screen.WorkingArea.Width
        Me.Height = _Screen.WorkingArea.Height
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = 0
        Me.SkinEngine1.SkinFile = AppSettings.SkinFolder + "Skin1.ssk"
        'AddHandler Me.Resize, AddressOf Form1_Resize
        newx = (Me.Width) / X
        newy = (Me.Height - 10) / Y
        If _Screen.WorkingArea.Width <> 1280 Then
            setControls(newx, newy, Me)
        End If
        Dim t1 As Point
        Dim t As Graphics = grpPicture.CreateGraphics
        grpPicture.PointToClient(t1)
        t.DrawLine(Pens.Black, t1, New Point(t1.X + 100, t1.Y + btnArticle.Height))
    End Sub

    Public Sub ChangeControlSize(ByVal cons As Control)
        newy = (Me.Height - 40) / Y
        setTag(cons)
        If _Screen.WorkingArea.Width <> 1280 Or _Screen.WorkingArea.Height <> 1024 Then
            setControls(newx, newy, cons)
        End If
    End Sub

    Private Sub setControls(ByVal newx As Double, ByVal newy As Double, ByVal cons As Control)
        For Each con As Control In cons.Controls
            If IsNothing(con.Tag) Then Continue For
            Dim mytag() As String = con.Tag.ToString().Split(CChar(":"))
            Dim a As Double = Convert.ToSingle(mytag(0)) * newx

            If cons.Name.IndexOf("grpStatus") >= 0 Then
                If _LastStatusHeight <> 0 Then
                    con.Width = CInt(a)
                    a = Convert.ToSingle(mytag(1)) * newy
                    con.Height = CInt(a)
                    a = Convert.ToSingle(mytag(2)) * newx
                    con.Left = CInt(a)
                    a = Convert.ToSingle(mytag(3)) * newy
                    con.Top = _LastStatusTop - _LastStatusHeight + 1
                    _LastStatusHeight = con.Height
                    _LastStatusTop = con.Top
                Else
                    con.Width = CInt(a)
                    a = Convert.ToSingle(mytag(1)) * newy
                    con.Height = CInt(a)
                    _LastStatusHeight = con.Height
                    a = Convert.ToSingle(mytag(2)) * newx
                    con.Left = CInt(a)
                    a = Convert.ToSingle(mytag(3)) * newy
                    con.Top = CInt(a)
                    _LastStatusTop = con.Top
                End If
            Else
                con.Width = CInt(a)
                a = Convert.ToSingle(mytag(1)) * newy
                con.Height = CInt(a)
                a = Convert.ToSingle(mytag(2)) * newx
                con.Left = CInt(a)
                a = Convert.ToSingle(mytag(3)) * newy
                con.Top = CInt(a)
            End If
            Dim currentSize As Single = CSng((Convert.ToSingle(mytag(4))) * newy)
            If cons.Name.IndexOf("DG_Article") >= 0 Then
                con.Font = New Font("Calibri", currentSize, con.Font.Style, con.Font.Unit)
            Else
                con.Font = New Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit)
            End If
            If con.Controls.Count > 0 Then
                setControls(newx, newy, con)
            End If
        Next

    End Sub


    Private Sub setTag(ByVal cons As Control)
        For Each con As Control In cons.Controls
            con.Tag = con.Width.ToString + ":" + con.Height.ToString + ":" + con.Left.ToString + ":" + con.Top.ToString + ":" + con.Font.Size.ToString
            setTag(con)
        Next
    End Sub

    Private Sub MainUI_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        cFormFontResize.WinFromH = System.Windows.Forms.Screen.GetWorkingArea(Me).Height
        If Me.Height = cFormFontResize.WinFromH Then
            ' cFormFontResize.newH = Me.Height
        End If
    End Sub


    Public Sub DisposeMe()
        If Not IsNothing(WatchWT) Then
            WatchWT.Dispose()
        End If
        If Not IsNothing(ScheduleView) Then
            ScheduleView.Dispose()
        End If
    End Sub

    Public Sub MainForm_Run() Implements IMainForm.MainForm_Run
        Me.Run()
    End Sub

    Public Sub MainForm_ReadLanguage() Implements IMainForm.MainForm_ReadLanguage
        Me.ReLoadLanguage()
    End Sub

    Public Sub MainForm_Show() Implements IMainForm.MainForm_Show
        Me.Show()
    End Sub

    Public Sub MainForm_Quit() Implements IMainForm.MainForm_Quit
        MeExit()
    End Sub

    Public Sub MainForm_Dispose() Implements IMainForm.MainForm_Dispose
        Me.Dispose()
    End Sub

    Public Function MainForm_Init(Devices As Dictionary(Of String, Object), Stations As Dictionary(Of String, IStationTypeBase), AppSettings As Settings) As Boolean Implements IMainForm.MainForm_Init
        Return Init(Devices, Stations)
    End Function

    Public Function MainForm_InitCounterView(CounterControl As CounterController) As Boolean Implements IMainForm.MainForm_InitCounterView
        Return True
    End Function

    Public Sub MainForm_ResetClear() Implements IMainForm.MainForm_ResetClear

    End Sub
    Public Sub MainForm_AddClear() Implements IMainForm.MainForm_AddClear

    End Sub

    Private Sub ListView_StationData_Resize(sender As Object, e As EventArgs) Handles ListView_StationData.Resize
        StationColumnHeader.Width = ListView_StationData.Width - 19
    End Sub

    Private Sub DG_ArticleResize(sender As Object, e As EventArgs) Handles DG_Article.Resize
        lblRefPart.Width = gbArticle.Width - 3
        lblRefPart.Height = DG_Article.Height + 10 - 3
    End Sub


    Private Sub ListView_StationData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView_StationData.SelectedIndexChanged
        TabControlStations.Visible = False
        If ListView_StationData.SelectedItems.Count > 0 Then
            bEditMode = False
            If bLastIndex > 0 Then lListStationView(bLastIndex).bEdit = False
            lListStationView(ListView_StationData.SelectedItems(0).Index + 1).bEdit = True
            bLastIndex = ListView_StationData.SelectedItems(0).Index + 1
        Else
            bEditMode = False
            If bLastIndex > 0 Then lListStationView(bLastIndex).bEdit = False
        End If
        ShowStationView(picBoxMain.Width, picBoxMain.Height)
    End Sub

    Private Sub CreateStation()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            lListStationView.Clear()
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationViews")
            strStationPicture = CType(_nodes(0), XmlElement).GetAttribute("Picture")
            Dim iCnt As Integer = 1
            For Each _node As XmlNode In _nodes

                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationView")
                For Each _nodeList As XmlNode In _subNodes
                    Dim cPictureComponent As New clsPictureComponentCfg
                    cPictureComponent.strName = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    cPictureComponent.strX = CType(_nodeList, XmlElement).GetElementsByTagName("Position_X")(0).InnerText
                    cPictureComponent.strY = CType(_nodeList, XmlElement).GetElementsByTagName("Position_Y")(0).InnerText
                    cPictureComponent.strR = CType(_nodeList, XmlElement).GetElementsByTagName("Position_R")(0).InnerText
                    cPictureComponent.iFontSize = CType(_nodeList, XmlElement).GetElementsByTagName("FontSize")(0).InnerText
                    cPictureComponent.strColor = CType(_nodeList, XmlElement).GetElementsByTagName("FontColor")(0).InnerText
                    lListStationView.Add(iCnt.ToString, cPictureComponent)
                    iCnt = iCnt + 1
                Next

            Next
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try
    End Sub

    Private Sub ShowStationView(ByVal W As Integer, ByVal H As Integer)
        Try
            Dim iR As Double
            Dim iX As Integer
            Dim iY As Integer
            Dim iPointX As Integer
            Dim iPointY As Integer
            Dim iIndex As Integer = 0
            Dim cFileHandler As New FileHandler
            Dim strBackImage As String = AppSettings.ApplicationFolder + strStationPicture

            If Not cFileHandler.FileExist(strBackImage) Then
                Return
            End If
            If Not IsNothing(picBoxMain.Image) Then
                picBoxMain.Image.Dispose()
                picBoxMain.Image = Nothing
            End If
            img1 = Nothing
            Using file1 As New FileStream(strBackImage, FileMode.Open, FileAccess.Read)
                Dim img = FileCompress.CompressionImage(file1, 50)
                img1 = FileCompress.BytesToImage(img)
                file1.Close()
            End Using

            scaleX1 = 1
            scaleY1 = 1
            rectF1 = New RectangleF()
            rectF1.Width = W
            rectF1.Height = H
            rectF1.X = 0
            rectF1.Y = 0
            bmp1 = New Bitmap(W, H)


            g1 = Graphics.FromImage(bmp1)
            g1.Clear(Color.White)
            g1.SmoothingMode = SmoothingMode.AntiAlias
            If Not IsNothing(bmp1) Then g1.DrawImage(img1, rectF1)



            For Each cPictureCfg As clsPictureComponentCfg In lListStationView.Values
                Dim strStationBackImage As String = AppSettings.ApplicationFolder + "Picture\green.png"
                If Not IsNumeric(cPictureCfg.strR) Or cPictureCfg.strR = "" Then
                    iR = 1.0
                Else
                    iR = cPictureCfg.strR
                End If

                If Not IsNumeric(cPictureCfg.strX) Or cPictureCfg.strX = "" Then
                    iX = 1.0
                Else
                    iX = cPictureCfg.strX
                End If

                If Not IsNumeric(cPictureCfg.strY) Or cPictureCfg.strY = "" Then
                    iY = 1.0
                Else
                    iY = cPictureCfg.strY
                End If
                iX = iX * iRatex
                iY = iY * iRateY
                iR = iR * iRateY
                iPointX = iX
                iPointY = iY

                If File.Exists(strStationBackImage) Then
                    Dim imgCompontent As Image
                    Using file2 As New FileStream(strStationBackImage, FileMode.Open, FileAccess.Read)
                        Dim img2 = FileCompress.CompressionImage(file2, 50)
                        imgCompontent = FileCompress.BytesToImage(img2)
                        file2.Close()
                    End Using

                    Dim rectCompontent As RectangleF = New RectangleF()
                    rectCompontent.Width = imgCompontent.Width * iR
                    rectCompontent.Height = imgCompontent.Height * iR
                    rectCompontent.X = iPointX - rectCompontent.Width / 2
                    rectCompontent.Y = iPointY - rectCompontent.Height / 2
                    g1.DrawImage(imgCompontent, rectCompontent)


                    cRectangle = New Rectangle(New Point(rectCompontent.X, rectCompontent.Y), New Size(rectCompontent.Width, rectCompontent.Height))
                    cPictureCfg.cRectangle = cRectangle

                    Dim iH As Integer = g1.MeasureString(cPictureCfg.strName, New System.Drawing.Font("Calibri", CInt(cPictureCfg.iFontSize), FontStyle.Bold)).Height
                    Dim iW As Integer = g1.MeasureString(cPictureCfg.strName, New System.Drawing.Font("Calibri", CInt(cPictureCfg.iFontSize), FontStyle.Bold)).Width
                    cRectangle = New Rectangle(New Point(rectCompontent.X + rectCompontent.Width, iPointY - iW / 5), New Size(iW, iH))

                    g1.DrawString(cPictureCfg.strName, New System.Drawing.Font("Calibri", CInt(cPictureCfg.iFontSize), FontStyle.Bold), New SolidBrush(ColorTranslator.FromHtml(cPictureCfg.strColor)), New Point(rectCompontent.X + rectCompontent.Width, iPointY - iW / 5))

                    If cPictureCfg.bEdit Then
                        If bEditMode Then
                            g1.DrawRectangle(New Pen(Color.Orange, 2), cRectangle)
                        Else
                            g1.DrawRectangle(New Pen(Color.Red, 2), cRectangle)
                        End If
                    Else
                        g1.DrawRectangle(New Pen(ColorTranslator.FromHtml(cPictureCfg.strColor), 1), cRectangle)
                    End If
                End If
            Next




            picBoxMain.Image = bmp1
            Dim graphics1 As Graphics = picBoxMain.CreateGraphics()
            graphics1.DrawImage(bmp1, New Point(0, 0))
            graphics1.Dispose()

        Catch ex As Exception
            Return
        End Try
    End Sub
    Private Sub SaveStation()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationViews")
            strStationPicture = CType(_nodes(0), XmlElement).GetAttribute("Picture")
            Dim iCnt As Integer = 1
            For Each _node As XmlNode In _nodes

                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationView")
                For Each _nodeList As XmlNode In _subNodes
                    Dim cPictureComponent As clsPictureComponentCfg = lListStationView(iCnt.ToString)
                    CType(_nodeList, XmlElement).GetElementsByTagName("Position_X")(0).InnerText = cPictureComponent.strX
                    CType(_nodeList, XmlElement).GetElementsByTagName("Position_Y")(0).InnerText = cPictureComponent.strY
                    iCnt = iCnt + 1
                Next

            Next
            _doc.Save(s_FileName)
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try
    End Sub
    Private Sub PictureBox_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picBoxMain.MouseClick
        If e.Button = MouseButtons.Left Or Not bEditMode Then
            Call ScheduleToolStripMenuItem_Click(Me, Nothing)
        End If
        If e.Button = MouseButtons.Right And bEditMode Then
            For Each cPictureCfg As clsPictureComponentCfg In lListStationView.Values
                If cPictureCfg.bEdit Then
                    cPictureCfg.strX = e.Location.X
                    cPictureCfg.strY = e.Location.Y
                    ShowStationView(picBoxMain.Width, picBoxMain.Height)
                    SaveStation()
                End If
            Next
        End If
    End Sub

    Private Sub picBoxMain_DoubleClick(sender As Object, e As MouseEventArgs) Handles picBoxMain.DoubleClick

        'If e.Button = MouseButtons.Left Then

        'Else
        'Call ScheduleToolStripMenuItem_Click(Me, Nothing)
        'End If


    End Sub
    Private Sub picBoxMain_Resize(sender As Object, e As EventArgs)
        Try
            If picBoxMain.Width > iMaxX Then
                iMaxX = picBoxMain.Width
            End If
            If picBoxMain.Width < iMaxX Then
                iRatex = picBoxMain.Width / iMaxX
                ShowStationView(picBoxMain.Width, picBoxMain.Height)
            End If

            If picBoxMain.Height > iMaxY Then
                iMaxY = picBoxMain.Height
            End If
            If picBoxMain.Height < iMaxY Then
                iRateY = picBoxMain.Height / iMaxY
                ShowStationView(picBoxMain.Width, picBoxMain.Height)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowStationOverViewToolStripMenuItem.Click
        OverviewInfoForm.ShowOverview = Not OverviewInfoForm.ShowOverview
    End Sub

    Private Sub ShowProductionViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowProductionViewToolStripMenuItem.Click
        If IsNothing(cProductionDataView) Then
            cProductionDataView = New ProductionDataView
            cProductionDataView.Init(Devices, Stations, AppSettings)
            cProductionDataView.Show()
        ElseIf cProductionDataView.isShow Then
            cProductionDataView.Quit(Devices)
        Else
            cProductionDataView = New ProductionDataView
            cProductionDataView.Init(Devices, Stations, AppSettings)
            cProductionDataView.Show()
        End If


    End Sub

    Private Sub ListView_StationData_DoubleClick(sender As Object, e As EventArgs) Handles ListView_StationData.DoubleClick
        If ListView_StationData.SelectedItems.Count > 0 Then
            bEditMode = True
            If bLastIndex > 0 Then lListStationView(bLastIndex).bEdit = False
            lListStationView(ListView_StationData.SelectedItems(0).Index + 1).bEdit = True
            bLastIndex = ListView_StationData.SelectedItems(0).Index + 1
        Else
            bEditMode = False
            If bLastIndex > 0 Then lListStationView(bLastIndex).bEdit = False
        End If
        ShowStationView(picBoxMain.Width, picBoxMain.Height)
    End Sub

    Private Sub ShowErrorCodeViewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowErrorCodeViewToolStripMenuItem.Click
        If IsNothing(cErrorCodeListForm) Then
            cErrorCodeListForm = New ChildrenErrorCodeListForm
            cErrorCodeListForm.Init(Devices, Stations, AppSettings)
            cErrorCodeListForm.Show()
        ElseIf cErrorCodeListForm.isShow Then
            cErrorCodeListForm.Quit(Devices)
        Else
            cErrorCodeListForm = New ChildrenErrorCodeListForm
            cErrorCodeListForm.Init(Devices, Stations, AppSettings)
            cErrorCodeListForm.Show()
        End If

    End Sub

    Private Sub picBoxMain_DoubleClick(sender As Object, e As EventArgs) Handles picBoxMain.DoubleClick

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
End Class






