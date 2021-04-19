
'History
'=======================2016-05-26 ����===============================
'Kostal.Las.ArticleProvider.Csv.dll �汾 1.0.0.1
'Kostal.Las.Base.dll �汾 1.0.0.2
'1.����Shift.class 23:59:59 ʱ����Bug.
'2.����ScheduleView CheckSum�Ƚ�.
'3.����������ʱ���Check Sum. ������������޷�����.
'4.����Linecontrol UserDefine�ӿ�. �����Զ������Child.
'5.������PLC����ʱ�ظ����ADS ����Bug.
'6.����Twincat ����ʱ����.
'7.����ManualStation �����ֹ��߶�����ɨ��.
'8.����Datalogic 210Nɨ����.
'9.����FileHander �ļ��ƶ�FileMove
'======================================================================

'=======================2016-06-06 ����===============================
'Kostal.Las.ArticleProvider.Csv.dll �汾 1.0.0.1
'Kostal.Las.Base.dll �汾 1.0.0.3
'1.�Ż�Log��ʾ
'2.Device��������
'======================================================================

'=======================2016-07-01 ����===============================
'Kostal.Las.ArticleProvider.Csv.dll �汾 1.0.0.1
'Kostal.Las.Base.dll �汾 1.0.0.3
'1.����ֶ������ӿ�
'2.����DLL�汾��ʾ
'3.�Ż���ʱ��
'4.����Picture Station����ʾBug.
'5.Define ��Ϊ�첽����.
'6.����������ӡ����ֽ.
'7.����StepLine�ӿ�.����ѡ���ڲ���֮ǰ���߲���֮������Զ��岽��
'8.����CheckTrigInfo�ӿ�.���Ը����û�����ѡ��Local Article.��APP Article����PLC Article or �Զ���Article.
'9.Visible="True"
'10.ȡ��AutoSelectʱ��Article������ʱ����.
'11.ֻ��һ��PLC���Զ�ѡ���һ��PLC
'12.����������ݲ�ͬ�ֱ�����ʾ
'13.����PLC Run�ӿ�.�û������Զ���Station����
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
        '��ʼ������

        _AppSettings = _SetAppSettings.Init(mMainForm.MainLogger)
        mLoadScreen.AppSettings = _AppSettings
        mLoadScreen.Show()
        _Devices.Add(Settings.Name, _AppSettings)
        _Logger = New Logger(_AppSettings)

        'ɾ��log
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".log")
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".txt")
        _FileHandler.DelectLogByDay(90, _AppSettings.LogFolder, ".l_1")

        'д������Ϣ
        SetInitInfoToLogger()

        '��ʼ������
        _Language = New Language(_i, _AppSettings)
        If Not _Language.Init() Then Return False
        _Devices.Add(Language.Name, _Language)

        '��ʼ��Station Config
        _StationCfg = New StationCfg(_i, _AppSettings, _Language)
        If Not _StationCfg.Init() Then Return False
        _Devices.Add(StationCfg.Name, _StationCfg)

        '��ʼ��Article
        _AppArticle = New Article(_i, _AppSettings, _Language, mMainForm.CBArticle)
        _AppArticle.Init()
        '�ֶ������Ҫ��Article Key.
        '_AppArticle.AddManualElement("Test", True)
        _Devices.Add(Article.Name, _AppArticle)

        '��ʼ��Schedule
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
