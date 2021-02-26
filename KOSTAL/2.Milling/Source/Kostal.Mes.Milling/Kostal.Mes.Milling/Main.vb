Imports System.Windows.Forms

Module Main
    Private WithEvents mMainForm As MainForm
    Private mLoadScreen As New LoadScreen
    Private _SetAppSettings As New SetSettings
    Private _AppSettings As Settings
    Private _Language As Language
    Private _MessagerLeft As Messager
    Private _MessagerRight As Messager
    Private _FileHandler As New FileHandler
    Private mMainFormIsClosing As Boolean
    Private _Logger As Logger
    Private _iLeft As Station
    Private _iRight As Station
    Private _Stations As New Dictionary(Of String, IStationTypeBase)
    Private _Devices As New Dictionary(Of String, Object)
    Private _MesStation As MESStation
    Private _WebServiceL As WebService
    Private _WebServiceR As WebService
    Private _WebService2L As WebService2
    Private _WebService2R As WebService2
    Private _DataStore As DataStore
    Private _LinecontrolStore As LinecontrolStore
    Private _ArticleStore As ArticleStore
    Private _TC As TwinCatAds
    Private _ArticleCount As ArticleCount
    Private _SMTStore As SMTStore
    Private clsAlarm As clsAlarm


    Private Function MainInit() As Boolean
        _iLeft = New Station("MainLeft")
        _iRight = New Station("MainRight")
        mMainForm = New MainForm

        _AppSettings = _SetAppSettings.Init()
        _Devices.Add(Settings.Name, _AppSettings)
        mLoadScreen.AppSettings = _AppSettings
        _Language = New Language(_iLeft, _AppSettings)
        If Not _Language.Init() Then Return False
        _Devices.Add(Language.Name, _Language)
        mLoadScreen.Show()
        _MessagerLeft = New Messager
        _MessagerLeft.Construct(mMainForm.Ui_Left.ListBox_Msg)
        _MessagerRight = New Messager
        _MessagerRight.Construct(mMainForm.Ui_Right.ListBox_Msg)
        _FileHandler.DelectLogByDay(360, _AppSettings.LogFolder, ".log")
        _Logger = New Logger(_AppSettings)

        _ArticleCount = New ArticleCount
        _ArticleCount.Init(_Devices)
        _Devices.Add(ArticleCount.sName, _ArticleCount)



        If _AppSettings.WebServiceCfg.Enable Then
            _WebServiceL = New WebService
            _WebServiceL.Init(_AppSettings)
            _WebServiceL.CreateDll()
            _WebServiceL.LoadDll()
            _Devices.Add("Left" + WebService.Name, _WebServiceL)

            _WebService2L = New WebService2
            _WebService2L.Init(_AppSettings)
            _WebService2L.CreateDll()
            _WebService2L.LoadDll()
            _Devices.Add("Left" + WebService2.Name, _WebService2L)


            _WebServiceR = New WebService
            _WebServiceR.Init(_AppSettings)
            _WebServiceR.LoadDll()
            _Devices.Add("Right" + WebService.Name, _WebServiceR)

            _WebService2R = New WebService2
            _WebService2R.Init(_AppSettings)
            _WebService2R.LoadDll()
            _Devices.Add("Right" + WebService2.Name, _WebService2R)
        End If

        _DataStore = New DataStore
        _DataStore.Init(_AppSettings)
        _Devices.Add(DataStore.Name, _DataStore)

        clsAlarm = New clsAlarm
        clsAlarm.Init(_AppSettings)
        _Devices.Add(clsAlarm.Name, clsAlarm)

        _LinecontrolStore = New LinecontrolStore
        _LinecontrolStore.Init(_AppSettings)
        _Devices.Add(LinecontrolStore.Name, _LinecontrolStore)

        _SMTStore = New SMTStore
        _SMTStore.Init(_AppSettings)
        _Devices.Add(SMTStore.Name, _SMTStore)

        _ArticleStore = New ArticleStore
        _ArticleStore.Init(_AppSettings)
        _Devices.Add(ArticleStore.Name, _ArticleStore)
        _ArticleCount.Show()
        _ArticleCount.Hide()
        If Not mMainForm.Init(_Devices) Then Return False
        AddHandler mMainForm.LanguageChangedTo, AddressOf LanguageChangedTo
        mMainForm.Show()


        SetInitInfoToLogger()
        If Not InitStations() Then Return False
        _TC = New TwinCatAds
        If Not _TC.Init(_AppSettings, _Devices, _Stations) Then Return False
        mMainForm.TC = _TC
        mLoadScreen.Dispose()
        Return True
    End Function
    Private Function InitStations() As Boolean
        For Each element As StationCfg In _AppSettings.StaionCfg.Values
            _MesStation = New MESStation(element, _AppSettings, IIf(element.Name = "Left", mMainForm.Ui_Left, mMainForm.Ui_Right), _Devices, mMainForm.StatusForm.Items(element.Name))
            If Not _MesStation.Init() Then Return False
            _Stations.Add(element.Name, _MesStation)
        Next
        Return True
    End Function

    Private Sub GlobalSignalHandler()
        For Each _Station As IStationTypeBase In _Stations.Values
            _Station.Run()
            If _Station.IsMasterError Then
                mMainFormIsClosing = True
            End If
        Next
        If Not _TC.Run() Then
            mMainFormIsClosing = True
        End If
        _ArticleCount.Run()
    End Sub

    Sub LanguageChangedTo(ByVal Name As String)
        For Each _Station As IStationTypeBase In _Stations.Values
            _Station.ReLoadLanguage()
        Next
        _ArticleCount.ReLoadLanguage()
    End Sub

    Sub Main()
        Try

            Dim MyProcess As New ProcessControl
            If Not MyProcess.IsMyProcessRunning Then
                MainInit()
                mMainForm.sw.Start()
                Do While Not mMainFormIsClosing
                    Application.DoEvents()
                    GlobalSignalHandler()
                    If mMainForm.CycleCounter < Long.MaxValue Then mMainForm.CycleCounter = mMainForm.CycleCounter + 1
                Loop
            Else
                MessageBox.Show("The Process:" + MyProcess.FileName + " have Exist.")
            End If
            DisposeMe()
        Catch ex As Exception
            If IsNothing(_Logger) Then
                Dim _ExceptionMsg As ExceptionMsg = New ExceptionMsg()
                _ExceptionMsg.TextBox_Msg.Text = ex.Message
                _ExceptionMsg.TextBox_Stack.Text = ex.StackTrace
                _ExceptionMsg.ShowDialog()
                _ExceptionMsg.Dispose()
            Else
                _iLeft.Text = ex.Message
                _iLeft.Stack = ex.StackTrace
                _Logger.StackError(_iLeft, enmLogType.ErrorLog, , _MessagerLeft)
            End If
            DisposeMe()
        End Try

    End Sub

    Private Sub DisposeMe()
        On Error Resume Next
        _WebServiceL.Close()
        _WebServiceR.Close()
        If Not IsNothing(mMainForm) Then
            mMainForm.Dispose()
        End If

        If Not IsNothing(_ArticleCount) Then
            _ArticleCount.Dispose()
        End If

        If Not IsNothing(_TC) Then
            _TC.Dispose()
        End If
        For Each _Station As IStationTypeBase In _Stations.Values
            _Station.Dispose()
        Next
        _Logger.Logger(_iLeft, _MessagerLeft, "**************************************************************************************", "Dispose")
        _Logger.Logger(_iRight, _MessagerRight, "**************************************************************************************", "Dispose")
    End Sub

    Private Sub SetInitInfoToLogger()
        _Logger.Logger(_iLeft, _MessagerLeft, "**************************************************************************************", "Init")
        _iLeft.Text = "System Version: V" & My.Application.Info.Version.ToString _
          & " Build: " & _
         System.Diagnostics.FileVersionInfo.GetVersionInfo _
         (System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
        _Logger.Logger(_iLeft, _MessagerLeft)
        _Logger.Logger(_iRight, _MessagerRight, "**************************************************************************************", "Init")
        _iRight.Text = "System Version: V" & My.Application.Info.Version.ToString _
          & " Build: " & _
         System.Diagnostics.FileVersionInfo.GetVersionInfo _
         (System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion
        _Logger.Logger(_iRight, _MessagerRight)
    End Sub

    Private Sub mMainForm_IamClosing() Handles mMainForm.IamClosing
        mMainFormIsClosing = True
    End Sub

End Module
