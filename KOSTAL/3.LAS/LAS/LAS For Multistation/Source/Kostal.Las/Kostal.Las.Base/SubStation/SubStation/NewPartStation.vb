Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class NewPartStation
    Inherits StationTypeBase
    Protected _UIStation As NewPartUI
    Protected _ReadGetNewPart As Boolean
    Protected _AppSchedule As Schedule
    Protected _variantInfo As New StructVariantInfo
    Protected _WritebulNewPartAvailable As Boolean
    Protected _WritebulArticleInfo As Boolean
    Protected _WritebulChangedArticleInfo As Boolean
    Protected _WritebulScheduleInfo As Boolean
    Protected _WritebytCurrentScheduleNr As Byte
    Protected _mSN As String = ""
    Protected _SNLable As Label
    Protected _OKArticle As Button
    Protected _NewPartType As String
    Protected _NewPartMsg As String
    Protected _Refs As References
    Protected _CheckSN As CheckSN
    Protected _ReTestList As ReTestList
    Protected _RePrint As Boolean = False
    Protected _WriteArticleOnly As Boolean
    Protected _LastScheduleMode As String
    Protected _scheduleChange As Boolean
    Protected _ChearModeChange As Boolean
    Protected _ScannedContextChanged As Boolean
    Protected _ScannerStation As UserDefineStation
    Protected _PrinterStation As PrinterStation
    Protected _LineControl As LineControlStation
    Protected _SerialNoExternalProducer As ISerialNoGeneratorDefine
    Protected _VariantInfoDefine As IVariantInfoDefine
    Protected _LineArticleCounter As IArticleCounter
    Protected _PC_bulScanPartRequest As Boolean
    Protected _PC_bulScannedPartResult As Boolean
    Protected _ScheduleManager As ScheduleManager
    Protected _MessageManager As MessageManager
    Protected Delegate Function dGetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean
    Protected pGetVariantInfo As New dGetVariantInfo(AddressOf _GetVariantInfo)
    Protected pGetVariantInfoCB As AsyncCallback = New AsyncCallback(AddressOf _GetVariantInfoCB)
    Protected Delegate Function dCreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
    Protected pCreateSerialNo As New dCreateSerialNo(AddressOf _CreateSerialNo)
    Protected pCreateSerialNoCB As AsyncCallback = New AsyncCallback(AddressOf _CreateSerialNoCB)
    Public Const Name As String = "NewPartStation"


    Public ReadOnly Property References As References
        Get
            Return _Refs
        End Get
    End Property

    Public ReadOnly Property PC_bulScanPartRequest As Boolean
        Get
            Return _PC_bulScanPartRequest
        End Get
    End Property

    Public ReadOnly Property WriteArticleOnly As Boolean
        Get
            Return _WriteArticleOnly
        End Get
    End Property

    Public ReadOnly Property PC_bulScannedPartResult As Boolean
        Get
            Return _PC_bulScannedPartResult
        End Get
    End Property

    Public ReadOnly Property AppSchedule As Schedule
        Get
            Return _AppSchedule
        End Get
    End Property

    Public ReadOnly Property LastScheduleMode As String
        Get
            Return _LastScheduleMode
        End Get
    End Property

    Public ReadOnly Property NewPartType As String
        Get
            Return _NewPartType
        End Get
    End Property


    Public ReadOnly Property NewPartMsg As String
        Get
            Return _NewPartMsg
        End Get
    End Property

    Public WriteOnly Property ReadGetNewPart As Boolean
        Set(ByVal value As Boolean)
            _ReadGetNewPart = value
        End Set
    End Property

    Public Property WritebulNewPartAvailable As Boolean
        Set(ByVal value As Boolean)
            _WritebulNewPartAvailable = value
        End Set
        Get
            Return _WritebulNewPartAvailable
        End Get
    End Property

    Public Property WritebulArticleInfo As Boolean
        Set(ByVal value As Boolean)
            _WritebulArticleInfo = value
        End Set
        Get
            Return _WritebulArticleInfo
        End Get
    End Property
    Public Property WritebulChangedArticleInfo As Boolean
        Set(ByVal value As Boolean)
            _WritebulChangedArticleInfo = value
        End Set
        Get
            Return _WritebulChangedArticleInfo
        End Get
    End Property
    Public Property WritebulScheduleInfo As Boolean
        Set(ByVal value As Boolean)
            _WritebulScheduleInfo = value
        End Set
        Get
            Return _WritebulScheduleInfo
        End Get
    End Property

    Public ReadOnly Property WritebytCurrentScheduleNr As Byte
        Get
            Return _WritebytCurrentScheduleNr
        End Get
    End Property

    Public ReadOnly Property VariantInfo As StructVariantInfo
        Get
            Return _variantInfo
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal SerialNoExternalProducer As ISerialNoGeneratorDefine, ByVal VariantInfoDefine As IVariantInfoDefine, Optional ByVal SNLable As Label = Nothing, Optional ByVal OKArticle As Button = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _AppSchedule = CType(Devices(Schedule.Name), Schedule)
            _CheckSN = New CheckSN(_i.IdString, _Devices)
            _UIStation = New NewPartUI
            _variantInfo = New StructVariantInfo
            _SerialNoExternalProducer = SerialNoExternalProducer
            _VariantInfoDefine = VariantInfoDefine
            _UI = _UIStation
            _SNLable = SNLable
            If _SNLable Is Nothing Then _SNLable = New Label
            _OKArticle = OKArticle
            _LastScheduleMode = ""
            _Messager.Construct(_UIStation.Msg)
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub


    Private Sub ScannerPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        _ScannedContextChanged = True
    End Sub

    '初始化List
    Public Overrides Function Init() As Boolean
        Try
            _i.StepInputNumber = _i.Address_Origin
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            _i.StepFromNumber = 0
            _Language.ReadControlText(_UIStation)
            If _SubStationCfg.Printer <> "" Then
                _PrinterStation = CType(_Stations(_SubStationCfg.Printer), PrinterStation)
                AddHandler _PrinterStation.UIStation.RePrint, AddressOf RePrint
            End If

            If _SubStationCfg.Scanner <> "" Then
                _ScannerStation = CType(_Stations(_SubStationCfg.Scanner), UserDefineStation)
                AddHandler _ScannerStation.PropertyChanged, AddressOf ScannerPropertyChanged
            End If

            If _SubStationCfg.LineControl <> "" Then
                _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
            End If

            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If

            If _Devices.ContainsKey(ReTestList.Name) Then
                _ReTestList = CType(_Devices(ReTestList.Name), ReTestList)
            End If
            If _SubStationCfg.Config = "" Then
                _WriteArticleOnly = True
            Else
                _WriteArticleOnly = False
            End If
            AddHandler _AppSchedule.IDChange, AddressOf Schedule_Change
            AddHandler _AppArticle.IDChange, AddressOf Article_Change
            _ScheduleManager = CType(_Stations(ScheduleManager.Name), ScheduleManager)
            _LineArticleCounter = CType(_Devices(ArticleCounter.sName), IArticleCounter)
            ReLoadLanguage()
            Return True
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
        _Language.ReadControlText(_UIStation.Panel)
        Return True
    End Function

    Public Overrides Sub Run()
        Try
            If IsNothing(_i) Then Exit Sub

            _FirstPulse = Not _FirstFlag
            _FirstFlag = True

            _ManualOffPulse = Not _ManualMode And _ManualFlag
            _ManualFlag = _ManualMode

            '==============================================================================
            'StepHeader
            '==============================================================================
            If Not CheckStepLine() Then Return
            If Not BeforeLine() Then Return
            If Not UpdateMsg(NewPartStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadGetNewPart = False
                    _WritebulNewPartAvailable = False
                    _PC_bulScanPartRequest = False
                    _PC_bulScannedPartResult = False
                    _variantInfo.Clear()
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99 'Calibrate End
                    If Not _WriteArticleOnly Then
                        If Not _CheckSN.Init(_SubStationCfg.Config) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CHECKSN_INIT_FAIL, "FAIL", _CheckSN.StatusDescription), "_CheckSN.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CHECKSN_INIT_PASS, "Successful"), "_CheckSN.Init")
                            _Devices.Add(_SubStationCfg.Name, _CheckSN)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case -98
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -97
                    _VariantInfoDefine.GetVariantInfo(_i, _AppArticle, _Devices, _Stations, _variantInfo)
                    _WritebulChangedArticleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -96
                    InitScheduleInfo()
                    _WritebulScheduleInfo = True
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If Not IsNothing(_Refs) Then
                        If _Refs.RefMode Then '样件切换完成
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "Reference"))
                            _SNLable.Text = ""
                            _PC_bulScannedPartResult = False
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                            _i.StepInputNumber = 300 '样件模式
                            Exit Select
                        End If
                    End If

                    If Not IsNothing(_ReTestList) Then
                        If _ReTestList.ReTestMode Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "ReTest"))
                            _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                            _SNLable.Text = ""
                            _PC_bulScannedPartResult = False
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                            _i.StepInputNumber = 400 'ReTest模式
                            Exit Select
                        End If
                    End If

                    If _RePrint Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "RePrint"))
                        _SNLable.Text = ""
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        _i.StepInputNumber = 500 '打印标签
                        Exit Select
                    End If
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "NewPart"))
                    _SNLable.Text = ""
                    _PC_bulScanPartRequest = False
                    _PC_bulScannedPartResult = False
                    _scheduleChange = False
                    _ChearModeChange = False
                    _mSN = ""
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 1 '等待选择变种
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_ARTICLE))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG1)
                        End If
                    End If

                Case 2
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        _ScheduleManager.RemoveIndicateName(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
                        _ScheduleManager.InsertChangeIndicatedName(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        _StartCallBack = False
                        _i.StepInputNumber = 600
                        Return
                    End If
                    If _ScheduleManager.ManualSelectSchedule Then
                        _StartCallBack = False
                        _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                        'added  AndAlso _WriteArticleOnly to fix a bug by wang65 2018.07.12
                        If _WriteArticleOnly Then
                            _i.StepInputNumber = 14
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 3
                        End If
                    Else
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 3
                    If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data <> _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data Then
                        _ScheduleManager.InsertChangeIndicatedName(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _ScheduleManager.InsertChangeIndicatedName(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
                    If _ScheduleManager.GetChangeIndicatedStatus(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data) = enumChangeResult.PASS Then
                        _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                        If _WriteArticleOnly Then
                            _i.StepInputNumber = 14
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                    If Not IsNothing(_Refs) Then
                        If _Refs.RefMode Then '样件切换完成
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                    If Not IsNothing(_ReTestList) Then
                        If _ReTestList.ReTestMode Then
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                    If _RePrint Then
                        _i.StepInputNumber = _i.Address_Home
                    End If


                Case 5
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pCreateSerialNo.BeginInvoke(_i, AppSettings, _AppArticle, _Devices, _Stations, pCreateSerialNoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _mSN <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREAT_NEW, _mSN))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If
                Case 6
                    _CheckSN.StartCheckSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 7
                    If Not _CheckSN.IsReadRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8
                    If _CheckSN.EndCheckSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 2
                    End If

                Case 9
                    _CheckSN.StartSaveSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 10
                    If Not _CheckSN.IsWriteRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    If _CheckSN.EndSaveSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        ' _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 2
                    End If

                Case 12
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREATE, _mSN))
                    _SNLable.Text = _mSN
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG2)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 13
                    If Not IsNothing(_Refs) Then
                        If _Refs.CheckingReferenceSN(_mSN) Then '样件SN
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE), "_Refs.CheckingReferenceSN")
                            _i.StepInputNumber = 2
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 14

                    _StartCallBack = False

                    If Not IsNothing(_Refs) AndAlso _Refs.RefMode Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        _i.StepInputNumber = _i.Address_Home
                    ElseIf _WriteArticleOnly Then
                        If _ScannerStation IsNot Nothing Then
                            If Not IsNothing(_Refs) Then
                                If _Refs.RefMode Then
                                    _i.StepFromNumber = _i.Address_Home '记录返回步
                                    _i.StepInputNumber = _i.Address_Home
                                    Exit Select
                                End If
                            End If
                            '切换变种需要重新开始
                            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                                _i.StepInputNumber = _i.Address_Home
                                Exit Select
                            End If

                            If Not IsNothing(_ReTestList) Then
                                If _ReTestList.ReTestMode Then
                                    _i.StepFromNumber = _i.Address_Home '记录返回步
                                    _i.StepInputNumber = _i.Address_Home 'ReTest模式
                                    Exit Select
                                End If
                            End If

                            If _ChearModeChange Then
                                _i.StepFromNumber = _i.Address_Home '记录返回步
                                _i.StepInputNumber = _i.Address_Home
                                Exit Select
                            End If
                            ''手动打印标签
                            If _RePrint Then
                                _i.StepFromNumber = 27 '记录返回步
                                _i.StepInputNumber = _i.Address_Home
                                Exit Select
                            End If
                            '等待扫描完成
                            If _ScannerStation.LastScannedSerialNumber <> "" Then
                                _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ScannerStation.LastScannedSerialNumber
                                _mSN = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                                _ScannerStation.ReScan = False
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            End If
                        Else
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _mSN
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _mSN
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 15
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        ElseIf _WriteArticleOnly Then
                            _StartCallBack = False
                            '_i.StepInputNumber = _i.StepOutputNumber - 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
                            _NewPartMsg = _VariantInfoDefine.ErrorMsg
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 16
                    If _variantInfo.strKostalNr = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, "strKostalNr is Null", "_variantInfo.strKostalNr")
                    End If
                    If _variantInfo.strSerialNr = "" And Not _WriteArticleOnly Then
                        _Logger.ThrowerNoStation(_i, _Messager, "strSerialNr is Null", "_variantInfo.strSerialNr")
                    End If

                    _SNLable.Text = _variantInfo.strSerialNr

                    _WritebulArticleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 17
                    If Not _WritebulArticleInfo Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 18
                    _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, True)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 19
                    If _SubStationCfg.Printer <> "" And _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.ClearMode.ToString) < 0 And Not _WriteArticleOnly Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_PRINT, "Start"))
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG3)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_PRINT, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 20
                    If Not _PrinterStation.isRun Then
                        _PrinterStation.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 21
                    _PrinterStation.ReadStructDeviceInteraction.stuPlcArticleSet = _variantInfo
                    _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 22
                    If _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        _PrinterStation.isRun = False
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_PRINT_END, "FAIL"), "PrinterSation.Print")
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG4)
                    End If
                    If _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass Then
                        _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        _PrinterStation.isRun = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_PRINT_END, "Successful"))
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG5)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 23
                    If _SubStationCfg.LineControl <> "" Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Start"))
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG7)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 24
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 25
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet = _variantInfo
                    _LineControl.ReadStructRequestAction.strActionScheduleName = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.PLC_OUT_WT.SerialNumber = _variantInfo.strSerialNr
                    _LineControl.PLC_OUT_WT.ArticleNumber = _variantInfo.strKostalNr
                    _LineControl.PLC_OUT_WT.Schedule = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 26
                    If _LineControl.WriteStructResponseAction.bulPartReceived Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False

                        If _LineControl.WriteStructResponseAction.bulActionIsPass = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsPass = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "Successful"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG9)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _LineControl.WriteStructResponseAction.bulActionIsFail = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsFail = False
                            _LineControl.isRun = False
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "FAIL"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG8)
                            _i.StepInputNumber = _i.Address_Home  '不良重新开始
                        End If
                    End If

                Case 27
                    If Not IsNothing(_Refs) Then
                        If _Refs.RefMode Then
                            _i.StepFromNumber = _i.Address_Home '记录返回步
                            _i.StepInputNumber = _i.Address_Home
                            Exit Select
                        End If
                    End If
                    '切换变种需要重新开始
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        _i.StepInputNumber = _i.Address_Home
                        Exit Select
                    End If

                    If Not IsNothing(_ReTestList) Then
                        If _ReTestList.ReTestMode Then
                            _i.StepFromNumber = _i.Address_Home '记录返回步
                            _i.StepInputNumber = _i.Address_Home 'ReTest模式
                            Exit Select
                        End If
                    End If

                    If _ChearModeChange Then
                        _i.StepFromNumber = _i.Address_Home '记录返回步
                        _i.StepInputNumber = _i.Address_Home
                        Exit Select
                    End If
                    ''手动打印标签
                    If _RePrint Then
                        _i.StepFromNumber = 27 '记录返回步
                        _i.StepInputNumber = _i.Address_Home
                        Exit Select
                    End If

                    'Add Scanned Event happened
                    If _ScannedContextChanged Then
                        _ScannedContextChanged = False
                        _i.StepFromNumber = _i.Address_Home '记录返回步
                        _i.StepInputNumber = _i.Address_Home
                        Exit Select
                    End If

                    If Not IsNothing(_ScannerStation) Then
                        If _ScannerStation.ReScan Then
                            _i.StepFromNumber = _i.Address_Home '记录返回步
                            _i.StepInputNumber = _i.Address_Home
                            Exit Select
                        End If
                    End If

                    'If _ScheduleManager.GetChangeIndicatedStatus(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data) = enumChangeResult.Changed Then
                    '    _i.StepFromNumber = _i.Address_Home '记录返回步
                    '    _i.StepInputNumber = _i.Address_Home
                    '    Exit Select
                    'End If

                    If _i.Toggle Or _scheduleChange Then
                        If _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.ClearMode.ToString) >= 0 Then
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG20)
                        Else
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG6)
                        End If
                        _scheduleChange = False
                    End If

                    If _ReadGetNewPart Then
                        _ScheduleManager.LockSchedule = True
                        _LineArticleCounter.Add_Record(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
                        If _SubStationCfg.Printer <> "" Then
                            _PrinterStation.UIStation.Print.Enabled = False
                        End If
                        _i.StepFromNumber = 0 '记录返回步
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 28
                    InitScheduleInfo()
                    _WritebulScheduleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 29
                    If _SubStationCfg.Printer <> "" Then
                        _PrinterStation.UIStation.Print.Enabled = True
                    End If
                    If Not _WritebulScheduleInfo Then
                        _WritebulNewPartAvailable = True
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                '样件模式
                Case 300
                    If Not _Refs.RefMode Then
                        _i.StepInputNumber = _i.Address_Home
                    End If
                    If _Refs.RefreshingOK Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 301
                    ' If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = False
                    '   InitScheduleInfo()
                    '  _WritebulScheduleInfo = True
                    _PC_bulScanPartRequest = True
                    _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                    _Refs.RefreshingOK = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 302
                    If _SubStationCfg.Printer <> "" Then
                        _PrinterStation.UIStation.Print.Enabled = False
                    End If
                    'If _i.Toggle Then
                    '    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG12, _NewPartType)
                    'End If
                    If Not _Refs.RefMode Then
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If
                    If _Refs.currentRef.ScannerOK Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_REFERENCE_SCAN))
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = False
                        _PC_bulScannedPartResult = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 303
                    If _LocalArticle.GetArticle_FromID(_Refs.currentRef.LkNumber) Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _Refs.currentRef.SN
                        _SNLable.Text = _Refs.currentRef.SN
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_REFERENCE_LK, "Successful"))
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_REFERENCE_LK, "FAIL"))
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 304
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1

                        ElseIf _WriteArticleOnly Then

                            _StartCallBack = False
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 305

                    _WritebulArticleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 306
                    If Not _WritebulArticleInfo Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 307
                    If _i.Toggle Then
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG13)
                    End If

                    If Not _Refs.RefMode Then
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If _ReadGetNewPart Then
                        _ScheduleManager.LockSchedule = True
                        InitScheduleInfo()
                        _WritebulScheduleInfo = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 308
                    If Not _WritebulScheduleInfo Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case 309
                    _WritebulNewPartAvailable = True
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG14)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 310
                    If _SubStationCfg.Printer <> "" Then
                        _PrinterStation.UIStation.Print.Enabled = True
                    End If
                    If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 311
                    If _ReadGetNewPart = False Then
                        '   _WritebulNewPartAvailable = False
                        _Refs.currentRef.ScannerOK = False
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                'ReTest模式
                Case 400
                    If _i.Toggle Then
                        ' If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = False
                        _PC_bulScanPartRequest = True
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG15)
                    End If
                    If _ReTestList.ReTestListElement.Count > 0 Then
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = False
                        _PC_bulScannedPartResult = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_RETEST_SCAN))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If Not _ReTestList.ReTestMode Then 'ReTest终止
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 401
                    If _LocalArticle.GetArticle_FromID(_ReTestList.ReTestListElement(_ReTestList.ReTestListElement.Keys(0)).LkNumber) Then
                        _SNLable.Text = _ReTestList.ReTestListElement(_ReTestList.ReTestListElement.Keys(0)).SN
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ReTestList.ReTestListElement(_ReTestList.ReTestListElement.Keys(0)).SN
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_REFERENCE_LK, "Successful"))
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_REFERENCE_LK, "FAIL"))
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 402
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 403
                    _WritebulArticleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 404
                    If Not _WritebulArticleInfo Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 405
                    If _SubStationCfg.LineControl <> "" Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Start"))
                        '  _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG7)
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 406
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 407
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet = _variantInfo
                    _LineControl.ReadStructRequestAction.strActionScheduleName = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.PLC_OUT_WT.SerialNumber = _variantInfo.strSerialNr
                    _LineControl.PLC_OUT_WT.ArticleNumber = _variantInfo.strKostalNr
                    _LineControl.PLC_OUT_WT.Schedule = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 408
                    If _LineControl.WriteStructResponseAction.bulPartReceived Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False

                        If _LineControl.WriteStructResponseAction.bulActionIsPass = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsPass = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "Successful"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG9)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _LineControl.WriteStructResponseAction.bulActionIsFail = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsFail = False
                            _LineControl.isRun = False
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "FAIL"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG8)
                            _i.StepInputNumber = _i.Address_Home  '不良重新开始
                        End If
                    End If

                Case 409
                    If _i.Toggle Then
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG16)
                    End If

                    If _ReadGetNewPart Then
                        _ScheduleManager.LockSchedule = True
                        InitScheduleInfo()
                        _WritebulScheduleInfo = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Return
                    End If

                    If Not _ReTestList.ReTestMode Then 'ReTest终止
                        If _ReTestList.ReTestListElement.Count > 0 Then
                            _ReTestList.RemoveOne(_ReTestList.ReTestListElement(_ReTestList.ReTestListElement.Keys(0)).ID)
                        End If
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                Case 410
                    If Not _WritebulScheduleInfo Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case 411
                    _WritebulNewPartAvailable = True
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG17)
                    _i.StepInputNumber = _i.StepOutputNumber + 1


                Case 412
                    '  If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                    '  _ScheduleManager.LockSchedule = False
                    If _ReadGetNewPart = False Then
                        ' _WritebulNewPartAvailable = False
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                        _ReTestList.RemoveOne(_ReTestList.ReTestListElement(_ReTestList.ReTestListElement.Keys(0)).ID)
                        _i.StepInputNumber = _i.Address_Pass
                    End If


                Case 500 '重打标签
                    If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = False
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG17)
                    _PrinterStation.UIStation.Print.Enabled = False
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_ARTICLE))
                        End If
                    End If

                Case 501
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
                    End If
                    _StartCallBack = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 502
                    If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data <> _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data Then
                        _ScheduleManager.InsertChangeIndicatedName(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 2
                    End If

                Case 503
                    If _ScheduleManager.GetChangeIndicatedStatus(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data) = enumChangeResult.PASS Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 504
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pCreateSerialNo.BeginInvoke(_i, AppSettings, _AppArticle, _Devices, _Stations, pCreateSerialNoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _mSN <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREAT_NEW, _mSN))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If
                Case 505
                    _CheckSN.StartCheckSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 506
                    If Not _CheckSN.IsReadRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 507
                    If _CheckSN.EndCheckSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 501
                    End If

                Case 508
                    _CheckSN.StartSaveSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 509
                    If Not _CheckSN.IsWriteRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 510
                    If _CheckSN.EndSaveSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 501
                    End If

                Case 511
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREATE, _mSN))
                    _SNLable.Text = _mSN
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 512
                    If Not IsNothing(_Refs) Then
                        If _Refs.CheckingReferenceSN(_mSN) Then '样件SN
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE), "_Refs.CheckingReferenceSN")
                            _i.StepInputNumber = 501
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 513
                    _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, True)
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _mSN
                    _StartCallBack = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 514
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _WritebulArticleInfo = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 515
                    If Not _PrinterStation.isRun And Not _WritebulArticleInfo Then
                        _PrinterStation.isRun = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_PRINT, "Start"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 516
                    _PrinterStation.ReadStructDeviceInteraction.stuPlcArticleSet = _variantInfo
                    _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 517
                    If _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        _PrinterStation.isRun = False
                    End If
                    If _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass Then
                        _PrinterStation.ReadStructDeviceInteraction.bulPlcDoAction = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                        _PrinterStation.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        _PrinterStation.isRun = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 518
                    _RePrint = False
                    _PrinterStation.UIStation.Print.Enabled = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 519
                    If _SubStationCfg.LineControl <> "" Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Start"))
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG7)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 520
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 521
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet = _variantInfo
                    _LineControl.ReadStructRequestAction.strActionScheduleName = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.PLC_OUT_WT.SerialNumber = _variantInfo.strSerialNr
                    _LineControl.PLC_OUT_WT.ArticleNumber = _variantInfo.strKostalNr
                    _LineControl.PLC_OUT_WT.Schedule = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 522
                    If _LineControl.WriteStructResponseAction.bulPartReceived Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False

                        If _LineControl.WriteStructResponseAction.bulActionIsPass = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsPass = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "Successful"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG9)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _LineControl.WriteStructResponseAction.bulActionIsFail = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsFail = False
                            _LineControl.isRun = False
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_LINECONTROL_END, "FAIL"))
                            _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG8)
                            _i.StepInputNumber = _i.Address_Home  '不良重新开始
                        End If
                    End If

                Case 523
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG19)
                    If _i.StepFromNumber = 27 Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "NewPart"))
                        _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                        _SNLable.Text = _mSN
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                        _i.StepInputNumber = _i.StepFromNumber
                        _i.StepFromNumber = 0
                    Else
                        If Not IsNothing(_OKArticle) Then _OKArticle.Enabled = True
                        _i.StepInputNumber = _i.Address_Home
                    End If


                Case 600
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        ElseIf _WriteArticleOnly Then
                            ' _StartCallBack = False
                            _i.StepInputNumber = _i.StepOutputNumber - 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
                            _NewPartMsg = _VariantInfoDefine.ErrorMsg
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 601
                    If _variantInfo.strKostalNr = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, "strKostalNr is Null", "_variantInfo.strKostalNr")
                    End If
                    'If _variantInfo.strSerialNr = "" And Not _WriteArticleOnly Then
                    '   _Logger.ThrowerNoStation(_i, _Messager, "strSerialNr is Null", "_variantInfo.strSerialNr")
                    'End If
                    If _variantInfo.strSerialNr = "" Then _variantInfo.strSerialNr = "InValidSN"
                    _SNLable.Text = _variantInfo.strSerialNr

                    _WritebulArticleInfo = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 602
                    If Not _WritebulArticleInfo Then
                        _i.StepInputNumber = 2
                    End If


                Case 1000
                    '回写PLC
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG10)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 1001
                    If _ReadGetNewPart = False Then
                        _WritebulNewPartAvailable = False
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTENDPASS))
                        _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MSG11)
                        ' If _WriteArticleOnly Then
                        '  
                        ' End If
                        If Not IsNothing(_ScannerStation) Then
                            _ScannerStation.LastScannedSerialNumber = ""
                        End If
                        If _i.StepFromNumber = 27 Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_MODE, "NewPart"))
                            _NewPartType = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName.Replace(" ", "")
                            _SNLable.Text = _mSN
                            _i.StepInputNumber = _i.StepFromNumber
                            _i.StepFromNumber = 0
                            _ScheduleManager.LockSchedule = False
                        Else
                            _ScheduleManager.LockSchedule = False
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 2000
                    '回写PLC
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                Case 2001
                    If _ReadGetNewPart = False Then
                        _WritebulNewPartAvailable = False
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTENDFAIL))
                        _i.StepInputNumber = _i.Address_Home
                    End If

            End Select
            '==============================EndLine=========================================
            If Not AfterLine() Then Return
            '==============================================================================
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:" + _i.StepOutputNumber.ToString + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub


    Public Sub InitScheduleInfo()
        _WritebytCurrentScheduleNr = CByte(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_INDEX).Data)
    End Sub

    Public Sub Schedule_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        InitScheduleInfo()
        _scheduleChange = True
        If _LastScheduleMode.IndexOf(ProductionMode.ClearMode.ToString) >= 0 And _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.ClearMode.ToString) < 0 Then
            _ChearModeChange = True
        End If
        _LastScheduleMode = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
        _WritebulScheduleInfo = True
    End Sub
    Public Sub Article_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)

        'added by wang65 2018.06.09
        'add condition of If (WriteArticleOnly)
        If Not _VariantInfoDefine.GetVariantInfo(_i, _AppArticle, _Devices, _Stations, _variantInfo) And Not WriteArticleOnly Then
            _Logger.Thrower(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
        End If
        _WritebulChangedArticleInfo = True
    End Sub
    Public Sub RePrint()
        _RePrint = True
    End Sub


    Protected Function _GetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean
        Return _VariantInfoDefine.GetVariantInfo(_i, LocalArticle, Devices, Stations, variantInfo)
    End Function

    Protected Sub _GetVariantInfoCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetVariantInfo.EndInvoke(_variantInfo, Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _CreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
        Return _SerialNoExternalProducer.CreateSerialNo(_i, Settings, LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CreateSerialNoCB(ByVal Result As IAsyncResult)
        _mSN = pCreateSerialNo.EndInvoke(Result)
        _isCallBackRunning = False
    End Sub

    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))
        _i = Nothing
        AppSettings = Nothing
        _Language = Nothing
        _Logger = Nothing
        _LocalArticle = Nothing
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
End Class
