
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
Imports Kostal.Las.Base
Imports System.Reflection

<Assembly: AssemblyVersion("1.1.0.0")>
<Assembly: AssemblyFileVersion("1.1.0.0")>


Module Main
    Private WithEvents mMainForm As MainForm
    Private mLoadScreen As New LoadScreen
    Private _Logger As Logger
    Private _SetAppSettings As New SetSettings
    Private _AppSettings As Settings
    Private _Language As Language
    Private _AppArticle As Article
    Private _AppSchedule As Schedule
    Private _i As Station
    Private _Devices As New Dictionary(Of String, Object)
    Private _Stations As New Dictionary(Of String, IStationTypeBase)
    Private _StationCfg As StationCfg
    Private mMainFormIsClosing As Boolean
    '  Private mAppArticle As Kostal.Las.Common.
    Private _FileHandler As New FileHandler
    Private _XmlHandler As New XmlHandler
    Private mPassword As New PassWordForm
    Public MyTwinCat As New Dictionary(Of String, TwinCatAds)
    '======================================================
    Public Const Key_Main As String = "Main"


    Public Sub Main()
        Try
            Dim MyProcess As New ProcessControl
            If Not MyProcess.IsMyProcessRunning Then
                If Not MainInit() Then
                    DisposeMe()
                Else
                    mMainForm.sw.Start()
                    Do
                        Application.DoEvents()
                        If Not mMainFormIsClosing Then
                            GlobalSignalHandler()
                            If mMainForm.CycleCounter < Long.MaxValue Then mMainForm.CycleCounter = mMainForm.CycleCounter + 1
                        Else
                            Exit Do
                        End If
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
        Next

        mMainForm.Run()
        If mMainForm.PLCdisconnect Then
            _Logger.Thrower(_i, "PLC Disconnect", "MyTwinCat.Run")
            mMainFormIsClosing = True
        End If
    End Sub

   

    Private Function MainInit() As Boolean
        mMainForm = New MainForm
        _Devices.Add(MainForm.sName, mMainForm)
        _i = New Station(Key_Main)
        '初始化设置

        _AppSettings = _SetAppSettings.Init(mMainForm.MainLogger)
        mLoadScreen.AppSettings = _AppSettings
        mLoadScreen.Show()
        _Devices.Add(Settings.Name, _AppSettings)
        _Logger = New Logger(_AppSettings)

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
        _AppArticle = New Article(_i, _AppSettings, _Language, mMainForm.CBArticle)
        _AppArticle.Init()
        '手动添加需要的Article Key.
        '_AppArticle.AddManualElement("Test", True)
        _Devices.Add(Article.Name, _AppArticle)

        '初始化Schedule
        If _AppSettings.BoschLine Then
            _AppSchedule = New Schedule(_i, _AppSettings, _Language)
            _AppSchedule.Init()
            _Devices.Add(Schedule.Name, _AppSchedule)
        End If

        If Not mMainForm.Init(_Devices, _Stations) Then Return False
        mMainForm.Show()
        mPassword.Init(_i, _AppSettings, "UserPassWord")
        If Not InitStations() Then Return False
        If Not TwinCatInit() Then Return False
        mLoadScreen.Dispose()
        Return True
    End Function

    Private Function InitStations() As Boolean
        Dim mTempException As String = ""
        Dim _Tab As TabControl
        Dim _Panel As Panel
        mMainForm.TabControlStations.Location = New Point(0, 0)
        mMainForm.TabControlStations.Height = mMainForm.grpPicture.Height
        For Each _StationElement As StationElement In _StationCfg.StationListElement.Values
            _Panel = New Panel
            _Tab = New TabControl()
            _Panel.Controls.Add(_Tab)
            mMainForm.TabControlStations.TabPages.Add(_StationElement.Name, _StationElement.Name)
            mMainForm.TabControlStations.Padding = New Point(0, 0)
            _Tab.Width = mMainForm.TabControlStations.TabPages(_StationElement.Name).Width + 4
            _Tab.Height = mMainForm.TabControlStations.TabPages(_StationElement.Name).Height + 2
            _Panel.Width = mMainForm.TabControlStations.TabPages(_StationElement.Name).Width + 4
            _Panel.Height = mMainForm.TabControlStations.TabPages(_StationElement.Name).Height + 2
            _Panel.Location = New Point(-1, 0)
            _Tab.Location = New Point(-1, 0)
            _Panel.Controls.Add(_Tab)
            _Panel.Parent = mMainForm.TabControlStations.TabPages(_StationElement.Name)


            For Each _SubStationElement As SubStationCfg In _StationElement.SubStation.Values
                Dim _Station As IStationTypeBase = Nothing
                mTempException = _SubStationElement.Name
                Select Case _SubStationElement.Inteface
                    Case StationType.Schedule
                        '_Station = New ScheduleStation(_SubStationElement, _Devices, _Stations, mMainForm., New BeforeStepLine, New AfterStepLine)
                    Case StationType.Article
                        _Station = New ArticleStation(_SubStationElement, _Devices, _Stations, New VariantInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Scanner
                        _Station = New ScannerStation(_SubStationElement, _Devices, _Stations, New Scanner, New ScannerDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.ManualScanner
                        _Station = New ManualScannerStation(_SubStationElement, _Devices, _Stations, New ManualScanDefine, mMainForm.lblRefPart, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Printer
                        _Station = New PrinterStation(_SubStationElement, _Devices, _Stations, New Printer, New PrinterDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.NewPart
                        _Station = New NewPartStation(_SubStationElement, _Devices, _Stations, New SerialNoGeneratorDefine, New VariantInfo, mMainForm.lblActualSerialNumber_01, mMainForm.btnArticle, New BeforeStepLine, New AfterStepLine)
                    Case StationType.SN
                        _Station = New SNStation(_SubStationElement, _Devices, _Stations, New SerialNoGeneratorDefine, mMainForm.lblActualSerialNumber_01, New BeforeStepLine, New AfterStepLine)
                    Case StationType.QGW, StationType.QGW_ASSM, StationType.QGW_Finish
                        _Station = New LineControlStation(_SubStationElement, _Devices, _Stations, New LineControlDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.FailPrinter
                        _Station = New FailPrinterStation(_SubStationElement, _Devices, _Stations, New FailPrinter, New FailPrinterDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Laser
                        _Station = New LaserStation(_SubStationElement, _Devices, _Stations, New Laser, New LaserDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Flash
                        _Station = New FlashStation(_SubStationElement, _Devices, _Stations, New Flash, New FlashDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Manual
                        _Station = New ManualStation(_SubStationElement, _Devices, _Stations, New ManualScanDefine, mMainForm.lblRefPart, New BeforeStepLine, New AfterStepLine)
                    Case StationType.ReTest
                        _Station = New ReTestStation(_SubStationElement, _Devices, _Stations, New ManualScanDefine, New ReTestDefine, mMainForm.lblRefPart, New BeforeStepLine, New AfterStepLine)
                    Case StationType.ManualReTest
                        _Station = New ManualReTestStation(_SubStationElement, _Devices, _Stations, New ManualReTestMsgDefine, New ManualReTestChangeScheduleDefine, mMainForm.lblRefPart, New BeforeStepLine, New AfterStepLine)
                    Case StationType.ShowPic
                        _Station = New ShowPicStation(_SubStationElement, _Devices, _Stations, New ShowPicDefine, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Reference
                        _Station = New ReferenceStation(_SubStationElement, _Devices, _Stations, New ManualScanDefine, mMainForm.lblRefPart, New BeforeStepLine, New AfterStepLine)
                    Case StationType.Counter
                        _Station = New CounterStation(_SubStationElement, _Devices, _Stations, mMainForm.lblMessage, mMainForm.lblPass, mMainForm.lblfail, mMainForm.lbltotal, mMainForm.Button_Reset, New CheckTrigInfo, New BeforeStepLine, New AfterStepLine)
                    Case StationType.UserDefine
                    Case Else
                        Return False
                End Select
                If _Station Is Nothing Then Continue For
                _Tab.TabPages.Add(_SubStationElement.Name, _SubStationElement.Name)
                _Language.ReadControlText(_Tab)
                mMainForm.ChangeControlSize(_Station.UI())
                _Station.UI().Dock = DockStyle.Fill
                _Station.UI().Parent = _Tab.TabPages(_SubStationElement.Name)
                _Stations.Add(_SubStationElement.Name, _Station)
            Next
        Next

        For Each _Station As IStationTypeBase In _Stations.Values
            mTempException = _Station.SubStationCfg.Name
            _Station.Init()
        Next
        mMainForm.Text += " " + _SetAppSettings.Settings.MachineIdentifier.ProjectId
        Return True

    End Function

    Private Function TwinCatInit() As Boolean
        Dim tc As TwinCatAds
        For Each element As PLCConfig In _AppSettings.PLCConfig.Values
            tc = New TwinCatAds(element, _Devices, _Stations, New TwicatRun)
            If Not tc.Init Then
                _Logger.Thrower(_i, element.Name + "TC;" + tc.StatusDescription, "Start TwinCat")
                Return False
            End If
            _Devices.Add(element.Name, tc)
            MyTwinCat.Add(element.Name, tc)
        Next
        mMainForm.MyTwinCat = MyTwinCat
        _i.StepTextLine = "Start TwinCat"
        _Logger.Logger(_i)
        Return True
    End Function

    Private Sub SetInitInfoToLogger()
        _Logger.Logger(_i, "**************************************************************************************", "Init")
        _i.Text = "System Version: V" & My.Application.Info.Version.ToString _
          & " Build: " & _
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
        If Not IsNothing(mMainForm) Then
            mMainForm.Dispose()
        End If
        StationsTerminate()
        _Logger.Logger(_i, "System Stop", "Exit")
        _Logger.Logger(_i, "**************************************************************************************")
    End Sub

    Private Sub mMainForm_IamClosing() Handles mMainForm.IamClosing
        mMainFormIsClosing = True
    End Sub

End Module
