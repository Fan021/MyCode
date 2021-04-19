
'History
'=======================2016-05-26 更新===============================
'Kostal.Las.ArticleProvider.Csv.dll 版本 1.0.0.1
'Kostal.Las.Base.dll 版本 1.0.0.2
'1.修正Shift.class 23:59:59 时报错Bug.
'2.新增ScheduleView CheckSum比较.
'3.新增程序开启时检查Check Sum. 若不相符程序无法开启.
'4.新增Linecontrol UserDefine接口. 可以自定义添加Child.
'5.修正多PLC工作时重复添加ADS 变量Bug.
'6.新增Twincat 断线时报错.
'7.新增ManualStation 满足手工线多条码扫描.
'8.新增Datalogic 210N扫描仪.
'9.新增FileHander 文件移动FileMove
'======================================================================

'=======================2016-06-06 更新===============================
'Kostal.Las.ArticleProvider.Csv.dll 版本 1.0.0.1
'Kostal.Las.Base.dll 版本 1.0.0.3
'1.优化Log显示
'2.Device新增语言
'======================================================================

'=======================2016-07-01 更新===============================
'Kostal.Las.ArticleProvider.Csv.dll 版本 1.0.0.1
'Kostal.Las.Base.dll 版本 1.0.0.3
'1.添加手动触发接口
'2.新增DLL版本显示
'3.优化计时间
'4.修正Picture Station不显示Bug.
'5.Define 改为异步调用.
'6.新增不良打印机切纸.
'7.新增StepLine接口.可以选择在步骤之前或者步骤之后添加自定义步骤
'8.新增CheckTrigInfo接口.可以根据用户需求选择Local Article.是APP Article还是PLC Article or 自定义Article.
'9.Visible="True"
'10.取消AutoSelect时若Article不存在时报错.
'11.只有一个PLC是自动选择第一个PLC
'12.新增窗体根据不同分辨率显示
'13.新增PLC Run接口.用户可以自定义Station触发
'======================================================================

Imports System.Threading
Imports System.Windows.Forms
Imports Kostal.Las.ArticleProvider
Imports System.Reflection
Imports Kostal.Las.Base
Imports Kostal.Las.UserInterface
Imports System.IO

<Assembly: AssemblyVersion("1.1.0.0")>
<Assembly: AssemblyFileVersion("1.1.0.0")>
Module Main
    Private WithEvents mMainForm As IMainForm
    Private mLoadScreen As New UserInterface.LoadScreen
    Private _Logger As Logger
    Private _SetAppSettings As New SetSettings
    Private _AppSettings As Settings
    Private _Language As Language
    Private _AppArticle As Article
    Private _AppSchedule As Base.Schedule
    Private _i As Station
    Private _Devices As New Dictionary(Of String, Object)
    Private _Stations As New Dictionary(Of String, IStationTypeBase)
    Private _StationCfg As StationCfg
    Private mMainFormIsClosing As Boolean
    '  Private mAppArticle As Kostal.Las.Common.
    Private _FileHandler As New FileHandler
    Private _XmlHandler As New XmlHandler
    Private mPassword As New PassWordForm
    Private cErrorCodeManager As clsErrorCodeManager
    Private cPlcMessageManager As clsPlcMessageManager
    Private cUserManager As clsUserManager
    Private cTips As clsTips
    Private LineArticleCounter As ArticleCounter
    Private LineMaintenance As Base.Maintenance
    Public MyTwinCat As New Dictionary(Of String, TwinCatAds)
    Private cGlobalParameter As clsGlobalParameter
    '======================================================
    Public Const Key_Main As String = "Main"
    Private strConfigName As String = ""

    Public Sub Main(ByVal args() As String)
        Try

            Application.EnableVisualStyles()
            Dim MyProcess As New ProcessControl
            If Not MyProcess.IsMyProcessRunning Then
                If args.Length >= 1 Then
                    strConfigName = args(0)
                End If
                If Not MainInit() Then
                    DisposeMe()
                Else
                    mMainForm.MainForm_Timer.Start()
                    Do
                        Application.DoEvents()
                        If Not mMainFormIsClosing Then
                            GlobalSignalHandler()
                            If mMainForm.MainForm_CycleCounter < Long.MaxValue Then mMainForm.MainForm_CycleCounter = mMainForm.MainForm_CycleCounter + 1
                        Else
                            Exit Do
                        End If
                        System.Threading.Thread.Sleep(2)
                    Loop
                    DisposeMe()
                End If
            End If
        Catch ex As Exception
            If IsNothing(_Logger) Then
                Dim _ExceptionMsg As ExceptionMsg = New ExceptionMsg()
                _ExceptionMsg.TextBox_Msg.Text = ex.Message
                _ExceptionMsg.TextBox_Stack.Text = ex.StackTrace
                _ExceptionMsg.ShowDialog()
                _ExceptionMsg.Dispose()
            Else
                _i.Text = ex.Message
                _i.Stack = ex.StackTrace
                _Logger.StackError(_i)
            End If
            DisposeMe()
        End Try

    End Sub

    Private Sub GlobalSignalHandler()
        For Each _Station As IStationTypeBase In _Stations.Values
            _Station.Run()
            If _Station.IsMasterError Then
                mMainFormIsClosing = True
            End If
        Next

        For Each elementTwincat As TwinCatAds In MyTwinCat.Values
            If Not elementTwincat.Run() Then
                _Logger.Thrower(_i, elementTwincat.AmsNetId + elementTwincat.StatusDescription, "MyTwinCat.Run")
                mMainFormIsClosing = True
            End If

            ' elementTwincat.WriteAny(KostalAdsVariables.PC_bulSwitchOnOff, mMainForm.PC_bulSwitchOnOff)
            ' elementTwincat.WriteAny(KostalAdsVariables.PC_bulResetError, mMainForm.PC_bulResetError)
            ' elementTwincat.WriteAny(KostalAdsVariables.ADS_bulRedboxLock, mMainForm.PC_bulRedboxLock)
            If _AppSettings.LineType = enumLineType.MultiLine Then mMainForm.MainForm_StationOverviewInfo = elementTwincat.StationOverviewInfo
            If _AppSettings.LineType = enumLineType.MultiLine Then mMainForm.MainForm_ErrorMessageSet = CType(elementTwincat.GetDeviceNotificationEx(KostalAdsVariables.PLC_stuErrorMessage), structErrorMessageSet)

        Next

        mMainForm.MainForm_Run()

        If mMainForm.MainForm_PLCdisconnect Then
            _Logger.Thrower(_i, "PLC Disconnect", "MyTwinCat.Run")
            mMainFormIsClosing = True
        End If
    End Sub



    Private Function MainInit() As Boolean

        _i = New Station(Key_Main)
        '初始化设置
        _AppSettings = _SetAppSettings.Settings
        If strConfigName <> "" Then
            _AppSettings.ConfigName = strConfigName
        End If
        _AppSettings = _SetAppSettings.Init()


        _Devices.Add(Settings.Name, _AppSettings)
        mLoadScreen.AppSettings = _AppSettings
        mLoadScreen.Show()

        If _AppSettings.LineType = enumLineType.MultiLine Then
            mMainForm = New MainForm_Mul
        Else
            mMainForm = New MainForm_Bosh
        End If

        _Devices.Add(MainForm_Mul.sName, mMainForm)
        _SetAppSettings.InitLogger(mMainForm.MainForm_MainLogger, mMainForm)

        _Logger = New Logger(_AppSettings)
        CreateActive()
        '_FileHandler.WriteIniFile(_AppSettings.LogFolder, "SN.ini", "UserDefined", "SN", "")

        '删除log
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".log")
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".txt")
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".l_1")


        '写更新信息

        SetInitInfoToLogger()

        '初始化语言
        _Language = New Language(_i, _AppSettings)
        If Not _Language.Init() Then Return False
        _Devices.Add(Language.Name, _Language)

        '初始化Station Config
        _StationCfg = New StationCfg(_i, _AppSettings, _Language)
        If Not _StationCfg.Init() Then Return False
        _Devices.Add(StationCfg.Name, _StationCfg)

        '初始化Article
        _AppArticle = New Article(_i, _AppSettings, _Language, mMainForm.MainForm_CBArticle)
        _AppArticle.Init()
        '手动添加需要的Article Key.
        '_AppArticle.AddManualElement("Test", True)
        _Devices.Add(Article.Name, _AppArticle)


        If _AppSettings.LineType = enumLineType.MultiLine Then
            cErrorCodeManager = New clsErrorCodeManager()
            cErrorCodeManager.Init(_Devices, _Stations, _AppSettings)
            cErrorCodeManager.LoadErrorCodeCfg()
            _Devices.Add(clsErrorCodeManager.Name, cErrorCodeManager)

            cPlcMessageManager = New clsPlcMessageManager()
            cPlcMessageManager.Init(_Devices, _Stations, _AppSettings)
            cPlcMessageManager.LoadPlcMessageCfg()
            _Devices.Add(clsPlcMessageManager.Name, cPlcMessageManager)


            cUserManager = New clsUserManager()
            cUserManager.Init(_Devices, _Stations, _AppSettings)
            cUserManager.LoadUserCfg()
            _Devices.Add(clsUserManager.Name, cUserManager)

            cTips = New clsTips
            cTips.Init(_Devices, _Stations, _AppSettings)
            _Devices.Add(clsTips.Name, cTips)
        End If

        ''Line Article Counter
        LineArticleCounter = New ArticleCounter
        LineArticleCounter.Init(_i, _AppSettings, _Language)
        _Devices.Add(ArticleCounter.sName, LineArticleCounter)

        LineMaintenance = New Maintenance
        LineMaintenance.Init(_i, _AppSettings, _Language)
        _Devices.Add(Maintenance.sName, LineMaintenance)

        '初始化Schedule
        If _AppSettings.LineType > 0 Then
            _AppSchedule = New Base.Schedule(_i, _AppSettings, _Language)
            _AppSchedule.Init()
            _Devices.Add(Base.Schedule.Name, _AppSchedule)
            cGlobalParameter = New clsGlobalParameter
            cGlobalParameter.Init(_Devices, _Stations, _AppSettings)
            cGlobalParameter.LoadMachineGlobalParameter()
            _Devices.Add(clsGlobalParameter.Name, cGlobalParameter)
        End If


        If Not mMainForm.MainForm_Init(_Devices, _Stations, _AppSettings) Then Return False
        mPassword.Init(_i, _AppSettings, "UserPassWord")

        If Not InitStations() Then Return False
        If Not TwinCatInit() Then Return False
        mMainForm.MainForm_ReadLanguage()
        mMainForm.MainForm_Show()
        mLoadScreen.Dispose()

        If _AppSettings.LineType >= enumLineType.MultiLine Then
            cGlobalParameter.WriteValue()
        End If
        AddHandler _AppArticle.IDChange, AddressOf AppArticle_IDChange
        Return True
    End Function

    Public Sub AppArticle_IDChange(ByVal mID As String, ByVal ChangeType As enumChangeType)
        If _AppSettings.LineType >= enumLineType.MultiLine Then
            cGlobalParameter.WriteValue()
        End If
    End Sub


    Private Sub CreateActive()
        If Not _FileHandler.FileExist(_AppSettings.LogFolder + "LAS_Active.xml") Then
            Dim File As StreamWriter = New StreamWriter(_AppSettings.LogFolder + "LAS_Active.xml")

            File.WriteLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            File.WriteLine("<LasConfiguration xmlns=""http: //www.kostal.com/Testman/config"">")

            File.WriteLine(" <LanguageFiles>")
            File.WriteLine("  <!-- Select Language  -->")
            File.WriteLine("  <SelLanguage>English</SelLanguage>")
            File.WriteLine(" </LanguageFiles>")
            File.WriteLine(" <Article>")
            File.WriteLine("  <!-- Select Article  -->")
            File.WriteLine("  <SelVariant></SelVariant>")
            File.WriteLine(" </Article>")
            File.WriteLine("</LasConfiguration>")
            File.Close()
        End If
    End Sub
    Private Function InitStations() As Boolean
        Dim mTempException As String = ""
        Dim _Tab As TabControl
        Dim _Panel As Panel
        mMainForm.MainForm_TabControlStations.Location = New Point(0, 0)
        'mMainForm.TabControlStations.Height = mMainForm.grpPicture.Height
        Try
            For Each _StationElement As StationElement In _StationCfg.StationListElement.Values

                mMainForm.MainForm_TabControlStations.TabPages.Add(_StationElement.Name, _StationElement.Name)
                mMainForm.MainForm_TabControlStations.Padding = New Point(0, 0)

                _Tab = New TabControl()
                _Tab.Width = mMainForm.MainForm_TabControlStations.TabPages(_StationElement.Name).Width + 4
                _Tab.Height = mMainForm.MainForm_TabControlStations.TabPages(_StationElement.Name).Height + 2
                _Tab.Location = New Point(-1, 0)
                _Tab.Dock = DockStyle.Fill
                _Tab.Name = _StationElement.Name

                _Panel = New Panel
                _Panel.Controls.Add(_Tab)
                _Panel.Width = _Tab.Width
                _Panel.Height = _Tab.Height
                _Panel.Location = _Tab.Location
                _Panel.Dock = DockStyle.Fill
                _Panel.BackColor = Color.White
                _Panel.Parent = mMainForm.MainForm_TabControlStations.TabPages(_StationElement.Name)


                For Each _SubStationElement As SubStationCfg In _StationElement.SubStation.Values
                    Dim _Station As IStationTypeBase = Nothing
                    mTempException = _SubStationElement.Name
                    Select Case _SubStationElement.Inteface
                        Case StationType.Schedule
                            _Station = New ScheduleStation(_SubStationElement, _Devices, _Stations, CType(IIf(_AppSettings.LineType = enumLineType.MultiLine, mMainForm.MainForm_ScheduleSelectView, New ScheduleUI), IScheduleUI), mMainForm.MainForm_lblCurrentSchedule, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Article
                            _Station = New ArticleStation(_SubStationElement, _Devices, _Stations, New VariantInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Scanner
                            _Station = New ScannerStation(_SubStationElement, _Devices, _Stations, New Scanner, New ScannerDefine, New ScannerCommandDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.ManualScanner
                            _Station = New ManualScannerStation(_SubStationElement, _Devices, _Stations, New ScannerDefine, New ManualScannerMsgDefine, mMainForm.MainForm_lblRefPart, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Printer
                            _Station = New PrinterStation(_SubStationElement, _Devices, _Stations, New Printer, New PrinterDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.NewPart
                            _Station = New NewPartStation(_SubStationElement, _Devices, _Stations, New SerialNoGeneratorDefine, New VariantInfo, mMainForm.MainForm_lblActualSerialNumber, mMainForm.MainForm_btnArticle, New BeforeStepLine, New AfterStepLine)
                            mMainForm.MainForm_NewPartStartion = CType(_Station, NewPartStation)
                        Case StationType.SN
                            _Station = New SNStation(_SubStationElement, _Devices, _Stations, New SerialNoGeneratorDefine, mMainForm.MainForm_lblActualSerialNumber, New BeforeStepLine, New AfterStepLine)
                        Case StationType.PLCAlarm
                            _Station = New PLCAlarmStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, mMainForm.MainForm_lblRefPart, New BeforeStepLine, New AfterStepLine)
                        Case StationType.QGW, StationType.QGW_ASSM, StationType.QGW_Finish
                            _Station = New LineControlStation(_SubStationElement, _Devices, _Stations, New LineControlStationDefine, New LineControlDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                            If CType(_Station, LineControlStation).Init Then
                                mMainForm.MainForm_LinecotrolIndicator = enumINDICATOR_STATRUS.Green
                            End If
                        Case StationType.MES
                            _Station = New MesStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)

                        Case StationType.UserDefineQGW
                            _Station = New UserDefineLineControlStation(_SubStationElement, _Devices, _Stations, New UserStationDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)

                        Case StationType.UpdateReference
                            _Station = New UpdateReferenceStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)

                        Case StationType.FailPrinter
                            _Station = New FailPrinterStation(_SubStationElement, _Devices, _Stations, New FailPrinter, New FailPrinterDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Laser
                            _Station = New LaserStation(_SubStationElement, _Devices, _Stations, New Laser, New LaserDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Flash
                            _Station = New FlashStation(_SubStationElement, _Devices, _Stations, New Flash, New FlashDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Manual
                            _Station = New ManualStation(_SubStationElement, _Devices, _Stations, New ScannerDefine, mMainForm.MainForm_lblRefPart, New BeforeStepLine, New AfterStepLine)
                        Case StationType.ReTest
                            _Station = New ReTestStation(_SubStationElement, _Devices, _Stations, New ScannerDefine, New ReTestDefine, New ScannerDeviceDefine, mMainForm.MainForm_lblRefPart, New BeforeStepLine, New AfterStepLine)
                        Case StationType.ManualReTest
                            _Station = New ManualReTestStation(_SubStationElement, _Devices, _Stations, New ManualReTestMsgDefine, New ManualReTestChangeScheduleDefine, mMainForm.MainForm_lblRefPart, New BeforeStepLine, New AfterStepLine)
                        Case StationType.ShowPic
                            _Station = New ShowPicStation(_SubStationElement, _Devices, _Stations, New ShowPicDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Reference
                            _Station = New ReferenceStation(_SubStationElement, _Devices, _Stations, New ScannerDefine, New ScannerDeviceDefine, mMainForm.MainForm_lblRefPart, New BeforeStepLine, New AfterStepLine)
                        Case StationType.SaveProduction
                            _Station = New SaveProductionStation(_SubStationElement, _Devices, _Stations, mMainForm.MainForm_lblRefPart, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.MulitPrinter
                            _Station = New MulitPrinter(_SubStationElement, _Devices, _Stations, New GetMulitSNDefine, New RunMulitSNDefine, New ReprintMulitDefine, mMainForm.MainForm_lblRefPart, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.ForCam
                            _Station = New ForcamStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)

                        Case StationType.Counter
                            Dim counterStation As CounterStation = New CounterStation(_SubStationElement, _Devices, _Stations, mMainForm.MainForm_lblMessage, mMainForm.MainForm_lblPass, mMainForm.MainForm_lblfail, mMainForm.MainForm_lbltotal, mMainForm.MainForm_btnReset, mMainForm.MainForm_btnResetFail, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                            mMainForm.MainForm_InitCounterView(counterStation.CounterController)
                            counterStation.RestoreCounter()
                            _Station = counterStation
                        Case StationType.CAQ
                            Dim caqStation As CaqStation = New CaqStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                            If caqStation.CaqDisabled Then
                                mMainForm.MainForm_CaqIndicator = enumINDICATOR_STATRUS.Gray
                            ElseIf caqStation.CaqInited Then
                                mMainForm.MainForm_CaqIndicator = enumINDICATOR_STATRUS.Green
                            Else
                                mMainForm.MainForm_CaqIndicator = enumINDICATOR_STATRUS.Red
                            End If
                            _Station = caqStation
                        Case StationType.UserDefine
                            _Station = New UserDefineStation(_SubStationElement, _Devices, _Stations, New UserStationDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case StationType.Packman
                            _Station = New PackmanStation(_SubStationElement, _Devices, _Stations, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                        Case Else
                            Return False
                    End Select
                    If _Station Is Nothing Then Continue For
                    If Not _SubStationElement.Inteface = StationType.Schedule Or _AppSettings.LineType <> enumLineType.MultiLine Then
                        _Tab.TabPages.Add(_SubStationElement.Name, _SubStationElement.Name)
                        _Language.ReadControlText(_Tab)
                        '  mMainForm.cFormFontResize.SetControls(mMainForm.cFormFontResize.CurrentRate, _Station.UI())
                        ' _Station.UI().BackColor = Color.White
                        _Station.UI().Dock = DockStyle.Fill
                        _Station.UI().Parent = _Tab.TabPages(_SubStationElement.Name)
                        mMainForm.MainForm_cFormFontResize.SetControls(mMainForm.MainForm_cFormFontResize.CurrentRate, _Station.UI())
                        If _AppSettings.LineType = enumLineType.MultiLine Then AddHandler _Tab.Click, AddressOf CType(mMainForm.MainForm_stationView, StationView).ChildTabControl_Click
                    End If
                    _Stations.Add(_SubStationElement.Name, _Station)
                Next
            Next

            For Each _Station As IStationTypeBase In _Stations.Values
                mTempException = _Station.SubStationCfg.Name
                _Station.Init()
            Next
            mMainForm.MainForm_Text += " " + _SetAppSettings.Settings.MachineIdentifier.ProjectId
        Catch ex As Exception
            Throw ex
        End Try

        Return True

    End Function

    Private Function TwinCatInit() As Boolean
        Dim tc As TwinCatAds
        For Each element As PLCConfig In _AppSettings.PLCConfig.Values
            tc = New TwinCatAds(element, _Devices, _Stations, New TwicatRun)
            If Not tc.Init(element.LineType > 0) Then
                _Logger.Thrower(_i, element.Name + "TC;" + tc.StatusDescription, "Start TwinCat")
                Return False
            End If
            _Devices.Add(element.Name, tc)
            MyTwinCat.Add(element.Name, tc)
        Next
        mMainForm.MainForm_MyTwinCat = MyTwinCat
        _i.StepTextLine = "Start TwinCat"
        _Logger.Logger(_i)
        Return True
    End Function

    Private Sub SetInitInfoToLogger()
        _Logger.Logger(_i, "**************************************************************************************", "Init")
        _i.Text = "System Version: V" & My.Application.Info.Version.ToString _
          & " Build: " &
         System.Diagnostics.FileVersionInfo.GetVersionInfo _
         (System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
        _Logger.Logger(_i)
        _Logger.Logger(_i, "Kostal.Las.ArticleProvider.dll Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(_AppSettings.LibFolder + "Kostal.Las.ArticleProvider.dll").FileVersion)
        _Logger.Logger(_i, "Kostal.Las.Base.dll                Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(_AppSettings.LibFolder + "Kostal.Las.Base.dll").FileVersion)

    End Sub


    Private Sub StationsTerminate()
        For Each Station As IStationTypeBase In _Stations.Values
            Station.Dispose()
        Next
        For Each elementTwincat As TwinCatAds In MyTwinCat.Values
            elementTwincat.Dispose()
        Next
    End Sub


    Private Sub DisposeMe()
        On Error Resume Next
        LineArticleCounter.Dispose()
        If Not IsNothing(mMainForm) Then
            mMainForm.MainForm_Quit()
            mMainForm.MainForm_Dispose()
        End If

        StationsTerminate()
        _Logger.Logger(_i, "System Stop", "Exit")
        _Logger.Logger(_i, "**************************************************************************************")
    End Sub

    Private Sub mMainForm_IamClosing() Handles mMainForm.MainForm_IamClosing
        mMainFormIsClosing = True
    End Sub

End Module
