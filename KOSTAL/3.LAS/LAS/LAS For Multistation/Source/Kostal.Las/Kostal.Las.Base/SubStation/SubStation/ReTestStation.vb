Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Public Class ReTestStation
    Inherits StationTypeBase
    Protected _UIStation As ReTestUI
    Protected _LineControl As LineControlStation
    Protected _ScannerSation As ScannerStation
    Protected _ScannerDefine As IScannerDefine
    Protected _lblRefPart As Label
    Protected _ScanResult As String
    Protected _variantInfo As New StructVariantInfo
    Protected _ReTestList As ReTestList
    Protected _ReTestDefine As IReTestDefine
    Protected _AppSchedule As Schedule
    Protected _LocalSchedule As Schedule
    Protected _StartRestMode As Boolean
    Protected _ChangeType As enumChangeType
    Protected _ChangeScheduleName As String
    Protected _ReTestMsg As ShowPicMsg
    Protected _ManualVariantInfo As StructVariantInfo
    Protected _ScheduleManager As ScheduleManager
    Protected _MessageManager As MessageManager
    Protected Delegate Function dCheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pCheckScannerResult As New dCheckScannerResult(AddressOf _CheckScannerResult)
    Protected pCheckScannerResultCB As AsyncCallback = New AsyncCallback(AddressOf _CheckScannerResultCB)
    Protected Delegate Function dReTest(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pReTest As New dReTest(AddressOf _ReTest)
    Protected pReTestCB As AsyncCallback = New AsyncCallback(AddressOf _ReTestCB)
    Public Const Name As String = "ReTestStation"
    Protected _PLC_Reference_Sensor As Boolean = False
    Protected _ScannerDeviceDefine As IScannerDeviceDefine
    Protected _LAS_Reference_Fail As Boolean = False
    Public Property PLC_Reference_Sensor As Boolean
        Get
            Return _PLC_Reference_Sensor
        End Get
        Set(ByVal value As Boolean)
            _PLC_Reference_Sensor = value
        End Set
    End Property

    Public Property LAS_Reference_Fail As Boolean
        Get
            Return _LAS_Reference_Fail
        End Get
        Set(ByVal value As Boolean)
            _LAS_Reference_Fail = value
        End Set
    End Property
    Public Property ManualVariantInfo As StructVariantInfo
        Get
            Return _ManualVariantInfo
        End Get
        Set(ByVal value As StructVariantInfo)
            _ManualVariantInfo.strSerialNr = value.strSerialNr
            _ManualVariantInfo.strKostalNr = value.strKostalNr
            _ManualVariantInfo.strCustomerNr = value.strCustomerNr
            _ManualVariantInfo.strKostalArticleName = value.strKostalArticleName
            _ManualVariantInfo.strProductFamily = value.strProductFamily
        End Set
    End Property

    Public Property ReTestMsg As ShowPicMsg
        Get
            Return _ReTestMsg
        End Get
        Set(ByVal value As ShowPicMsg)
            _ReTestMsg = value
        End Set
    End Property

    Public ReadOnly Property ScanResult As String
        Get
            Return _ScanResult
        End Get
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ScannerDefine As IScannerDefine, ByVal ReTestDefine As IReTestDefine, ByVal ScannerDeviceDefine As IScannerDeviceDefine, ByVal lblRefPart As Label, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ReTestUI
            _UI = _UIStation
            _lblRefPart = lblRefPart
            _LocalSchedule = New Schedule(_i, AppSettings, _Language)
            _LocalSchedule.Init()
            _AppSchedule = CType(Devices(Schedule.Name), Schedule)
            _ManualVariantInfo = New StructVariantInfo
            _ScannerDeviceDefine = ScannerDeviceDefine
            _ReTestMsg = New ShowPicMsg
            _ReTestList = New ReTestList(_i.IdString, _Devices)
            Devices.Add(ReTestList.Name, _ReTestList)
            _ScannerDefine = ScannerDefine
            _ReTestDefine = ReTestDefine
            _Messager.Construct(_UIStation.Msg)
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub

    '初始化List
    Public Overrides Function Init() As Boolean
        Try
            _i.StepInputNumber = _i.Address_Origin
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            If _SubStationCfg.LineControl <> "" Then
                _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
            End If
            If _SubStationCfg.Scanner <> "" Then
                _ScannerSation = CType(_Stations(_SubStationCfg.Scanner), ScannerStation)
            End If
            _ScheduleManager = CType(_Stations(ScheduleManager.Name), ScheduleManager)
            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
            AddHandler _AppSchedule.IDChange, AddressOf Schedule_Change
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
            If Not UpdateMsg(ReTestStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _StartRestMode = False
                    _InternFail = False
                    _ScanResult = ""
                    _LAS_Reference_Fail = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -98
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -97
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _LAS_Reference_Fail = False
                        _ManualRefresh = False
                    End If

                    If _StartRestMode Then
                        If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                            _LocalSchedule.GetArticle_FromID(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data)
                        End If
                        ShowMsg(ShowMsgType.ShowSN)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 1
                    If _ManualVariantInfo.strKostalNr <> "" Then
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _ManualVariantInfo.strKostalNr Then
                            If Not _LocalArticle.GetArticle_FromID(_ManualVariantInfo.strKostalNr) Then
                                _i.StepInputNumber = _i.Address_Fail
                            End If
                        Else
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _ManualVariantInfo.strSerialNr
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                            If Not _LocalArticle.GetArticle_FromID(AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data) Then
                                _i.StepInputNumber = _i.Address_Fail
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If


                Case 2
                    If _ManualVariantInfo.strKostalNr <> "" Then
                        _variantInfo.strSerialNr = _ManualVariantInfo.strSerialNr
                        _variantInfo.strKostalNr = _ManualVariantInfo.strKostalNr
                        _variantInfo.strCustomerNr = _ManualVariantInfo.strCustomerNr
                        _variantInfo.strKostalArticleName = _ManualVariantInfo.strKostalArticleName
                        _variantInfo.strProductFamily = _ManualVariantInfo.strProductFamily
                        _ManualVariantInfo.strKostalNr = ""
                        _ManualVariantInfo.strSerialNr = ""
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 11
                    Else
                        _i.StepInputNumber = 200
                    End If

                Case 3
                    If _SubStationCfg.Scanner = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_SCAN))
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                    If Not _ScannerSation.isRun And Not _ScannerSation.WriteScanEnd Then
                        _ScannerSation.isRun = True
                        _ScannerSation.ReadStartScan = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
                    If _ScannerSation.WriteScanEnd Then
                        _ScanResult = _ScannerSation.ScanResult
                        If _ScanResult <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_SCAN_RESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                    If _MessageManager.UpdateMessage Then
                        _MessageManager.UpdateMessage = False
                        ShowMsg(ShowMsgType.ShowSN)
                    End If

                    If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Return
                    End If

                    If Not _StartRestMode Then  '手动结束
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Return
                    End If

                Case 5
                    _ScannerSation.ReadStartScan = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 6
                    If _ScannerSation.WriteScanEnd = False Then
                        _ScannerSation.isRun = False
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 7

                    If Not _StartRestMode Then  '手动结束
                        HiddenSN()
                        _i.StepInputNumber = _i.Address_Home
                        _InternFail = False
                        Return
                    End If

                    If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pCheckScannerResult.BeginInvoke(_i, _ScanResult, _LocalArticle, _Devices, _Stations, pCheckScannerResultCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternFail = True
                            ShowMsg(ShowMsgType.ShowDefine, _ScannerDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_SCAN_DEFINE, "FAIL", _ScannerDefine.ErrorMsg))
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 8
                    InitvariantInfo()
                    _StartCallBack = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 9
                    If _SubStationCfg.LineControl <> "" Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_LINECONTROL, "Start"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_LINECONTROL, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 10
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet = _variantInfo
                    _LineControl.ReadStructRequestAction.strActionScheduleName = ""
                    _LineControl.PLC_OUT_WT.SerialNumber = _variantInfo.strSerialNr
                    _LineControl.PLC_OUT_WT.ArticleNumber = _variantInfo.strKostalNr
                    _LineControl.PLC_OUT_WT.PartFailText = ""
                    _LineControl.PLC_OUT_WT.Schedule = _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = True
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 12
                    If _LineControl.WriteStructResponseAction.bulPartReceived And _LineControl.WriteStructResponseAction.bulActionIsPass Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulActionIsFail = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False
                        _LineControl.WriteStructResponseAction.bulActionIsPass = False
                        _LineControl.isRun = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_LINECONTROL, "Successful"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If _LineControl.WriteStructResponseAction.bulPartReceived And _LineControl.WriteStructResponseAction.bulActionIsFail Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulActionIsFail = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False
                        _LineControl.WriteStructResponseAction.bulActionIsPass = False
                        _LineControl.isRun = False
                        _InternFail = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_LINECONTROL, "FAIL"))
                        ShowMsg(ShowMsgType.ShowLineControl, _LineControl.WriteStructResponseAction.strActionResultText)
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 13
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pReTest.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, pReTestCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternFail = True
                            ShowMsg(ShowMsgType.ReTestDefine, _ReTestDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_RETEST_DEFINE, "FAIL", _ReTestDefine.ErrorMsg))
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 14
                    _ScanResult = ""
                    _InternFail = False
                    ShowMsg(ShowMsgType.ShowWaiting, , _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    _ReTestList.AddOne(New ReTestElement(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data))
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_NEWPART, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 15
                    If Not _StartRestMode Then
                        _InternFail = False
                        _ScannerSation.ReadStartScan = False
                        _ScannerSation.isRun = False
                        HiddenSN()
                        _i.StepInputNumber = _i.Address_Home
                    End If

                    If _ReTestList.ReTestListElement.Count = 0 Then
                        _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                          _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data,
                                          True
                                          )
                        If _ChangeType = enumChangeType.Auto Then
                            HiddenSN()
                            _ScheduleManager.RemoveIndicateName(_ChangeScheduleName)
                            _InternFail = False
                            _StartRestMode = False
                        End If
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                Case 200
                    Dim strDeviceName As String = ""
                    Dim eScanType As enumScanType = enumScanType.Manual
                    If _ScannerDeviceDefine.GetScannerName(_i, LocalArticle, _Devices, _Stations, strDeviceName, eScanType) = enumScannerDeviceType.AutoSelect Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _SubStationCfg.Scanner = strDeviceName
                        _SubStationCfg.ScannerType = eScanType
                        _ScannerSation = CType(_Stations(_SubStationCfg.Scanner), ScannerStation)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 201 'Scan
                    If _SubStationCfg.Scanner <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_SCAN))
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 202
                    If _SubStationCfg.ScannerType = enumScanType.Manual Then
                        _i.StepInputNumber = 3
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 203
                    ShowMsg(ShowMsgType.ShowWaiting2)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 204
                    If _PLC_Reference_Sensor Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                    If _MessageManager.UpdateMessage Then
                        ShowMsg(ShowMsgType.ShowWaiting)
                    End If

                    If Not _StartRestMode Then  '手动结束
                        HiddenSN()
                        _i.StepInputNumber = 230
                        _InternFail = False
                        Return
                    End If

                    If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                        _InternFail = False
                        _i.StepInputNumber = 230
                        Return
                    End If

                Case 205
                    _ScannerSation.AutoReadStartScan = True
                    ShowMsg(ShowMsgType.Scaner)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 206
                    If _ScannerSation.AutoScanPass Then
                        _ScanResult = _ScannerSation.ScanResult
                        _ScannerSation.AutoReadStartScan = False
                        _StartCallBack = False
                        _i.StepInputNumber = 7
                    End If

                    If _ScannerSation.AutoScanFail Then
                        _ScanResult = _ScannerSation.ScanResult
                        _ScannerSation.AutoReadStartScan = False
                        _i.StepInputNumber = 220
                    End If

                Case 220
                    _LAS_Reference_Fail = True
                    ShowMsg(ShowMsgType.TakeFail)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 221
                    If Not _PLC_Reference_Sensor Then
                        _LAS_Reference_Fail = False
                        _i.StepInputNumber = 200
                    End If

                    If Not _StartRestMode Then  '手动结束
                        HiddenSN()
                        _i.StepInputNumber = 230
                        _InternFail = False
                        Return
                    End If

                    If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                        _InternFail = False
                        _i.StepInputNumber = 230
                        Return
                    End If

                Case 230
                    _ScannerSation.AutoReadStartScan = False
                    _i.StepInputNumber = _i.Address_Home

                Case 240
                    _LAS_Reference_Fail = True
                    ShowMsg(ShowMsgType.TakeDefineFail)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 241
                    If Not _PLC_Reference_Sensor Then
                        _LAS_Reference_Fail = False
                        _i.StepInputNumber = 200
                    End If

                    If Not _StartRestMode Then  '手动结束
                        HiddenSN()
                        _i.StepInputNumber = 230
                        _InternFail = False
                        Return
                    End If

                    If _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data Then
                        _InternFail = False
                        _i.StepInputNumber = 230
                        Return
                    End If


                Case 1000
                    If _ChangeType <> enumChangeType.Auto Then
                        ShowMsg(ShowMsgType.ShowPass, , _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    End If
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_RESULT, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, "PASS"))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 1001
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETESTENDPASS))
                    _i.StepInputNumber = _i.Address_Home

                Case 2000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                      _LocalSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data,
                                      False
                                     )
                    '   ShowMsg(ShowMsgType.ShowFail, , _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_RESULT, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, "FAIL"))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 2001
                    If _ChangeType = enumChangeType.Auto Then
                        HiddenSN()
                        _ScheduleManager.RemoveIndicateName(_ChangeScheduleName)
                        _InternFail = False
                        _StartRestMode = False
                    End If
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETESTENDFAIL))
                    If _SubStationCfg.ScannerType = enumScanType.Manual Then
                        _i.StepInputNumber = _i.Address_Home
                    Else
                        _i.StepInputNumber = 240 '重新開始
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

    Public Sub InitvariantInfo()
        _ScanResult = ""
        _variantInfo.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
        _variantInfo.strKostalArticleName = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
        _variantInfo.strCustomerNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
        _variantInfo.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
        _variantInfo.strProductFamily = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data
        _variantInfo.strUserDefine = ""
    End Sub

    Protected Function ShowMsg(ByVal type As ShowMsgType, Optional ByVal strMsg As String = "", Optional ByVal strSN As String = "") As Boolean
        Dim _mTest As String
        _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
        _MessageManager.InsertControl(ReTestStation.Name)
        If type = ShowMsgType.ShowSN Then
            If Not _InternFail Then
                _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG1).Trim
                _lblRefPart.Text = _mTest
                _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
                _lblRefPart.BringToFront()
                _lblRefPart.Show()
                _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)
            End If
        End If

        If type = ShowMsgType.ShowWaiting Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG2).Trim + vbCrLf + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + strSN
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_ORANGE)
        End If

        If type = ShowMsgType.ShowDefine Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG4).Trim + vbCrLf + strMsg
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If


        If type = ShowMsgType.ReTestDefine Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG21).Trim + vbCrLf + strMsg
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowLineControl Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG6).Trim + vbCrLf + strMsg
            _lblRefPart.Font = New Font("Calibri", 35, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowPass Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG7).Trim + vbCrLf + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + strSN
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
        End If

        If type = ShowMsgType.ShowFail Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG8).Trim + vbCrLf + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + strSN
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.TakeFail Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG27).Trim
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.Scaner Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG28).Trim
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If
        If type = ShowMsgType.TakeDefineFail Then
            _mTest = _lblRefPart.Text + vbCrLf + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG29).Trim
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReTestMsg.StrMsg = _mTest
            _ReTestMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)

        End If

        If type = ShowMsgType.ShowWaiting2 Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG2).Trim
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_ORANGE)
        End If

        Return True

    End Function

    Protected Function HiddenSN() As Boolean
        _MessageManager.RemoveControl(ReTestStation.Name)
        If _MessageManager.GetNullStatus Then
            _lblRefPart.SendToBack()
            _lblRefPart.Hide()
            _lblRefPart.Tag = enumHMI_ERROR_TYPE.None
        End If
        Return True
    End Function


    '初始化List Data
    Protected Sub Schedule_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        If _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.RetestMode.ToString) >= 0 Then
            _ChangeScheduleName = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
            _ChangeType = ChangeType
            _ReTestList.ReTestMode = True
            _StartRestMode = True
        Else
            _ChangeScheduleName = _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
            _ChangeType = ChangeType
            _ReTestList.ReTestMode = False
            _StartRestMode = False
        End If
    End Sub

    Protected Function _CheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ScannerDefine.CheckScannerResult(_i, mScannerResult, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CheckScannerResultCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pCheckScannerResult.EndInvoke(Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _ReTest(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ReTestDefine.ReTest(_i, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _ReTestCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pReTest.EndInvoke(Result)
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
