Imports System.Windows.Forms
Imports System.Drawing

Public Class ShowPicMsg
    Protected mStrMsg As String = String.Empty
    Protected cTextColor As Color
    Public Property StrMsg As String
        Set(value As String)
            mStrMsg = value
        End Set
        Get
            Return mStrMsg
        End Get
    End Property
    Public Property TextColor As Color
        Set(value As Color)
            cTextColor = value
        End Set
        Get
            Return cTextColor
        End Get
    End Property

End Class
Public Class ReferenceStation
    Inherits StationTypeBase
    Protected _UIStation As ReferencesUI
    Protected _Refs As References
    Protected _lblRefPart As Label
    Private WithEvents _Shift As Shift
    Protected _listShift As New Dictionary(Of String, String)
    Protected _ScannerSation As ScannerStation
    Protected _ScanResult As String
    Protected _SettedRefID As String
    Protected _AppSchedule As Schedule
    Protected _ScannerDefine As IScannerDefine
    Protected _ScannerDeviceDefine As IScannerDeviceDefine
    Protected _ReferenceMsg As ShowPicMsg
    Protected _CurrentShift As Integer
    Protected _ArticleChange As Boolean
    Protected _sResult As String
    Protected _ShiftChange As Boolean
    ' Protected _ManualRef As Boolean
    Protected _MessageManager As MessageManager
    Protected _ScheduleManager As ScheduleManager
    Protected Delegate Function dCheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pCheckScannerResult As New dCheckScannerResult(AddressOf _CheckScannerResult)
    Protected pCheckScannerResultCB As AsyncCallback = New AsyncCallback(AddressOf _CheckScannerResultCB)
    Public Const Name As String = "ReferenceStation"
    Protected _LastScheduleName As String
    Protected _PLC_Reference_Sensor As Boolean = False
    Protected eScannerDeviceType As enumScannerDeviceType
    Protected _LAS_Reference_Fail As Boolean = False
    Protected mMain As IMainForm
    Private _Settings As Settings
    Private iNowRef As Integer = 0
    Private iMaxRef As Integer = 0
    Public Property ReferenceMsg As ShowPicMsg
        Get
            Return _ReferenceMsg
        End Get
        Set(ByVal value As ShowPicMsg)
            _ReferenceMsg = value
        End Set
    End Property

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

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ScannerDefine As IScannerDefine, ByVal ScannerDeviceDefine As IScannerDeviceDefine, ByVal lblRefPart As Label, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ReferencesUI
            _Refs = New References(_i.IdString, Devices)
            _ReferenceMsg = New ShowPicMsg
            _AppSchedule = CType(Devices(Schedule.Name), Schedule)
            _Settings = CType(Devices(Settings.Name), Settings)
            Devices.Add(References.Name, _Refs)
            _Shift = New Shift(_Settings.ConfigFolder + "lkshift.ini")
            Devices.Add(Shift.Name, _Shift)
            _ScannerDefine = ScannerDefine
            _ScannerDeviceDefine = ScannerDeviceDefine
            _UI = _UIStation
            _lblRefPart = lblRefPart
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
            If _SubStationCfg.Scanner <> "" Then
                _ScannerSation = CType(_Stations(_SubStationCfg.Scanner), ScannerStation)
            End If
            AddHandler _AppSchedule.IDChange, AddressOf Schedule_Change
            AddHandler _AppArticle.IDChange, AddressOf Article_Change
            ReLoadLanguage()
            If _SubStationCfg.Enable Then
                _Refs.RefMode = True
            Else
                _Refs.RefMode = False
            End If
            _Refs.RefEnable = _SubStationCfg.Enable
            _ScheduleManager = CType(_Stations(ScheduleManager.Name), ScheduleManager)
            _ShiftChange = False

            _CurrentShift = _Shift.GetCurrentShift
            _Refs._Shift = _Shift

            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
            mMain = CType(_Devices("mMainForm"), IMainForm)
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
            If Not UpdateMsg(ReferenceStation.Name) Then Return
            '==============================================================================
            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ScanResult = ""
                    _SettedRefID = ""
                    _ArticleChange = False
                    _InternFail = False
                    _LAS_Reference_Fail = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not InitREF() Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_INIT_FAIL, "FAIL", ""), "InitREF")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_INIT_PASS, "Successful"), "InitREF")
                            _Devices.Add(_SubStationCfg.Name, _Refs)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_INIT_PASS, "Disable"), "InitREF")
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case -98
                    _UIStation.AddColumns()
                    If _SubStationCfg.Enable Then UpdateRefList()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -97
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -96
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        '   HiddenREF()
                        _LAS_Reference_Fail = False
                        _ManualRefresh = False
                        _ArticleChange = False
                        _ShiftChange = True
                        _Refs.RefreshingOK = False
                        _Refs.currentRef.ScannerOK = False
                        _Refs.RefManual = False
                        '    _Refs.RefMode = False
                    End If

                    If _SubStationCfg.Enable Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 1
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        If Not _LocalArticle.GetArticle_FromID(AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data) Then
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 2 '加载样件时间
                    If _Refs.RefLock Then
                        Return
                    End If
                    If Not _listShift.ContainsKey(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString) Then
                        _listShift.Add(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString, _FileHandler.ReadIniFile(_Settings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "Shift_" & _CurrentShift.ToString))
                    Else
                        If _ShiftChange Then
                            _ShiftChange = False
                            If Not _Shift.CheckRefWithNowTime(_listShift(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString), _CurrentShift) Then
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_START, _CurrentShift.ToString))
                                UpdateList()
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _Refs.RefMode = False
                            End If
                        End If
                    End If
                    If _Refs.RefChange Then
                        _Refs.RefMode = True
                        _Refs.RefChange = False
                        _i.StepInputNumber = 4
                        Return
                    End If

                    If _ArticleChange Then
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 3
                        If Not _Refs.RefreshingCheckList(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data) Then
                            _UIStation.CleanRow()
                            _i.StepInputNumber = _i.Address_Fail
                        Else
                            UpdateList()
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 4 '查询样件完成状态
                    If _Refs.Count > 0 Then
                        _InternFail = False
                        _SettedRefID = _Refs.Keys(0)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SN, _Refs.Element(_SettedRefID).SN, _Refs.Element(_SettedRefID).LkNumber))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                Case 5
                        _sResult = _FileHandler.ReadIniFile(_Settings.LogFolder, "REF", AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _Refs.Element(_SettedRefID).SN + "_Shift_" & _CurrentShift.ToString)
                        If Not _Shift.CheckRefWithNowTime(_sResult, _CurrentShift) Or _Refs.RefManual Then
                            _Refs.RefMode = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Refs.Element(_SettedRefID).TestOK = True
                            _UIStation.UpdateRow(_Refs.Element(_SettedRefID).SN, _Refs.Element(_SettedRefID).TestOK)
                            _Refs.RemoveOne(_SettedRefID)
                            _i.StepInputNumber = 4
                        End If

                Case 6
                        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _Refs.Element(_SettedRefID).LkNumber Then
                            If _LocalArticle.GetArticle_FromID(_Refs.Element(_SettedRefID).LkNumber) Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ERROR, _Refs.Element(_SettedRefID).RefName, _Refs.Element(_SettedRefID).LkNumber))
                                _i.StepInputNumber = _i.Address_Fail
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 7
                        If _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data <> _Refs.Element(_SettedRefID).ScheduleName Then
                            '  _AppSchedule.GetArticle_FromIndicatedName(_Refs.Element(_SettedRefID).ScheduleName)
                            _LastScheduleName = _Refs.Element(_SettedRefID).ScheduleName
                            _ScheduleManager.InsertChangeIndicatedName(_Refs.Element(_SettedRefID).ScheduleName)
                            '  _Refs.RefreshingOK = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _LastScheduleName = _Refs.Element(_SettedRefID).ScheduleName
                            _ScheduleManager.InsertChangeIndicatedName(_Refs.Element(_SettedRefID).ScheduleName)
                            ' _Refs.RefreshingOK = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 8
                        If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.PASS Then
                            _Refs.RefreshingOK = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 9
                    If Not _InternFail Then
                        ShowMsg(ShowMsgType.ShowSN, _SettedRefID)
                    End If
                    iNowRef = 1
                    iMaxRef = _Refs.Element(_SettedRefID).SN.Split(";").Length
                    _i.StepInputNumber = 200

                Case 10 'Scan
                        If _SubStationCfg.Scanner <> "" Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN))
                            _i.StepInputNumber = _i.Address_Fail
                        End If

                Case 11
                        If Not _ScannerSation.isRun And Not _ScannerSation.WriteScanEnd Then
                            _ScannerSation.isRun = True
                            _ScannerSation.ReadStartScan = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 12
                        If _ScannerSation.WriteScanEnd Then
                            _ScanResult = _ScannerSation.ScanResult
                            If _ScanResult <> "" Then
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN_RESULT, _ScanResult))
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            End If
                        End If

                        If _MessageManager.UpdateMessage Then
                            _MessageManager.UpdateMessage = False
                            ShowMsg(ShowMsgType.ShowSN, _SettedRefID)
                        End If

                        If _ShiftChange Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _ArticleChange Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _Refs.RefMode = False Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 13
                        _ScannerSation.ReadStartScan = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 14
                        If _ScannerSation.WriteScanEnd = False Then
                            _ScannerSation.isRun = False
                            _StartCallBack = False
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 15
                    If _ShiftChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If _ArticleChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If _Refs.RefMode = False Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
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
                            ShowMsg(ShowMsgType.ShowDefine, _SettedRefID, _ScannerDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN_DEFINE, "FAIL", _ScannerDefine.ErrorMsg))
                            If _SubStationCfg.ScannerType = enumScanType.Manual Then
                                _i.StepInputNumber = 9 '重新開始
                            Else
                                _i.StepInputNumber = 240 '重新開始
                            End If
                        End If
                    End If

                Case 16
                    '修改的步骤
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _Refs.Element(_SettedRefID).SN Then
                        If _SubStationCfg.ScannerType = enumScanType.Manual Then ShowMsg(ShowMsgType.ShowWaiting, _SettedRefID)
                        _Refs.currentRef = _Refs.Element(_SettedRefID)
                        _Refs.currentRef.ScannerOK = True
                        '  _ScheduleManager.LockSchedule = True
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN_PASS, _SettedRefID, "PASS"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _InternFail = True
                        ShowMsg(ShowMsgType.ShowSNFail, _SettedRefID, "", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN_FAIL, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _Refs.Element(_SettedRefID).SN))
                        If _SubStationCfg.ScannerType = enumScanType.Manual Then
                            _i.StepInputNumber = 9 '重新開始
                        Else
                            _i.StepInputNumber = 240 '重新開始
                        End If
                    End If

                Case 17
                    If _Refs.RefMode = False Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If

                    If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = _i.Address_Home
                    End If

                    If Not _Refs.currentRef.ScannerOK Then
                        HiddenREF()
                        _i.StepInputNumber = _i.StepInputNumber + 1
                    End If

                Case 18
                    If eScannerDeviceType = enumScannerDeviceType.ManualSelect Then
                        If Not _PLC_Reference_Sensor Then
                            _Refs.Element(_SettedRefID).TestOK = True
                            _UIStation.UpdateRow(_Refs.Element(_SettedRefID).SN, _Refs.Element(_SettedRefID).TestOK)
                            _Refs.RemoveOne(_SettedRefID)
                            _FileHandler.WriteIniFile(_Settings.LogFolder, "REF", AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _Refs.Element(_SettedRefID).SN + "_Shift_" & _CurrentShift.ToString, Date.Now.ToString("yyyy.MM.dd HH:mm:ss"))
                            If _Refs.Count > 0 Then
                                _ScheduleManager.InsertChangeIndicatedName(_Refs.Element(_Refs.Keys(0)).ScheduleName)
                            End If
                            _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                            _i.StepInputNumber = 4
                        End If
                    Else
                        _Refs.Element(_SettedRefID).TestOK = True
                        _UIStation.UpdateRow(_Refs.Element(_SettedRefID).SN, _Refs.Element(_SettedRefID).TestOK)
                        _Refs.RemoveOne(_SettedRefID)
                        _FileHandler.WriteIniFile(_Settings.LogFolder, "REF", AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _Refs.Element(_SettedRefID).SN + "_Shift_" & _CurrentShift.ToString, Date.Now.ToString("yyyy.MM.dd HH:mm:ss"))
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        If _Refs.Count > 0 Then
                            _ScheduleManager.InsertChangeIndicatedName(_Refs.Element(_Refs.Keys(0)).ScheduleName)
                        End If

                        _i.StepInputNumber = 4
                    End If


                Case 200
                    Dim strDeviceName As String = ""
                    Dim eScanType As enumScanType = enumScanType.Manual
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_USER_DEFINED).Data = "DoQuery" + iNowRef.ToString
                    eScannerDeviceType = _ScannerDeviceDefine.GetScannerName(_i, LocalArticle, _Devices, _Stations, strDeviceName, eScanType)
                    If eScannerDeviceType = enumScannerDeviceType.AutoSelect Then
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
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_SCAN))
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 202
                    If _SubStationCfg.ScannerType = enumScanType.Manual Then
                        _i.StepInputNumber = 11
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 203
                    ShowMsg(ShowMsgType.ShowWaiting, _SettedRefID)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 204
                    If _PLC_Reference_Sensor Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                    If _MessageManager.UpdateMessage Then
                        ShowMsg(ShowMsgType.ShowWaiting, _SettedRefID)
                    End If


                    If _ShiftChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ArticleChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _Refs.RefMode = False Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                Case 205
                    _ScannerSation.AutoReadStartScan = True
                    ShowMsg(ShowMsgType.Scaner, _SettedRefID)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 206
                    If _ScannerSation.AutoScanPass Then

                        _ScannerSation.AutoReadStartScan = False
                        _StartCallBack = False
                        HiddenREF()
                        If iNowRef = 1 Then
                            _ScanResult = _ScannerSation.ScanResult
                        Else
                            _ScanResult = _ScanResult + ";" + _ScannerSation.ScanResult
                        End If

                        iNowRef = iNowRef + 1
                        If iNowRef > iMaxRef Then
                            _i.StepInputNumber = 15
                        Else
                            _i.StepInputNumber = 200
                        End If

                    End If

                    If _ScannerSation.AutoScanFail Then
                        If iNowRef = 1 Then
                            _ScanResult = _ScannerSation.ScanResult
                        Else
                            _ScanResult = _ScanResult + ";" + _ScannerSation.ScanResult
                        End If
                        _ScannerSation.AutoReadStartScan = False
                        _LAS_Reference_Fail = True
                        _StartCallBack = False
                        _i.StepInputNumber = 220
                    End If

                Case 220
                    _LAS_Reference_Fail = True
                    ShowMsg(ShowMsgType.TakeFail, _SettedRefID)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 221
                    If Not _PLC_Reference_Sensor Then
                        _LAS_Reference_Fail = False
                        iNowRef = 1
                        _i.StepInputNumber = 200
                    End If

                    If _ShiftChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ArticleChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _Refs.RefMode = False Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                Case 230
                    _ScannerSation.AutoReadStartScan = False
                    _i.StepInputNumber = _i.Address_Home

                Case 240
                    _LAS_Reference_Fail = True
                    ShowMsg(ShowMsgType.TakeDefineFail, _SettedRefID)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 241
                    If Not _PLC_Reference_Sensor Then
                        _LAS_Reference_Fail = False
                        iNowRef = 1
                        _i.StepInputNumber = 200
                    End If

                    If _ShiftChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ArticleChange Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _Refs.RefMode = False Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                    If _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Changed Or _ScheduleManager.GetChangeIndicatedStatus(_Refs.Element(_SettedRefID).ScheduleName) = enumChangeResult.Null Then
                        _ScheduleManager.RemoveIndicateName(_Refs.Element(_SettedRefID).ScheduleName)
                        HiddenREF()
                        _Refs.RefMode = False
                        _InternFail = False
                        _i.StepInputNumber = 230
                    End If

                Case 1000
                    '回写PLC Pass
                    HiddenREF()
                    If _listShift.ContainsKey(AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString) Then
                        _listShift(AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString) = Date.Now.ToString("yyyy.MM.dd HH:mm:ss")
                        _FileHandler.WriteIniFile(_Settings.LogFolder, "REF", AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "Shift_" & _CurrentShift.ToString, Date.Now.ToString("yyyy.MM.dd HH:mm:ss"))
                    End If
                    If _Settings.LineType = enumLineType.MultiLine Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 3
                    End If


                Case 1001
                    _ScheduleManager.InsertChangeIndicatedName(LAS_ScheduleMode.ClearMode.ToString, enumChangeType.Manual)
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                Case 1002
                    If _ScheduleManager.GetChangeIndicatedStatus(LAS_ScheduleMode.ClearMode.ToString) = enumChangeResult.PASS Then
                        mMain.MainForm_AddClear()
                        ' mMain.MainForm_btnClear.Enabled = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case 1003
                    _Refs.RefMode = False
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCEENDPASS))
                    _i.StepInputNumber = _i.Address_Home

                Case 2000
                        '回写PLC Fail
                        _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 2001
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCEENDFAIL))
                        _i.StepInputNumber = _i.Address_Home
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

    Protected Function InitREF() As Boolean
        Dim _RefElements As RefElements
        For Each element As Dictionary(Of String, Object) In _xmlHandler.GetAnyListFromXml(_Settings.ConfigFolder, _Settings.ConfigName, "References", "Reference", New String() {"Name", "Enable", "Article", "SN", "ReferenceName", "ProductFamily", "ScheduleName"})
            _RefElements = New RefElements
            _RefElements.ID = CStr(element("Name"))
            _RefElements.Enable = CBool(IIf(element("Enable").ToString.ToUpper = "TRUE", True, False))
            _RefElements.LkNumber = CStr(element("Article"))
            _RefElements.SN = CStr(element("SN"))
            _RefElements.RefName = CStr(element("ReferenceName"))
            _RefElements.ProductFamily = CStr(element("ProductFamily"))
            _RefElements.ScheduleName = CStr(element("ScheduleName"))
            If Not _Refs.AddOne(_RefElements) Then
                Return False
            End If
        Next
        Return True
    End Function

    Protected Function ShowMsg(ByVal type As ShowMsgType, ByVal RefID As String, Optional ByVal strMsg As String = "", Optional ByVal strSN As String = "") As Boolean
        Dim _mTest, _Result As String
        _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
        _MessageManager.InsertControl(ReferenceStation.Name)
        If _MessageManager.LockMessage Then Return True
        If type = ShowMsgType.ShowSN Then
            _Result = _Refs.Element(RefID).ScheduleName
            If _Result = FileHandler.s_DEFAULT Or _Result = FileHandler.s_Null Then
                _Result = _Refs.Element(RefID).ScheduleName
            Else
                _Result = Trim(_Result)
            End If
            If Not _InternFail Then

                _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG9).Trim + _Refs.Element(RefID).RefName + vbCrLf _
                       + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + _Refs.Element(RefID).SN
                _lblRefPart.Font = New Font("Arial", 50.25, FontStyle.Bold)
                _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
                _lblRefPart.Text = _mTest
                _lblRefPart.BringToFront()
                _lblRefPart.Show()
                _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)
                _ReferenceMsg.StrMsg = _mTest
                _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_NOMALBLUE)
            End If

        End If

        If type = ShowMsgType.ShowWaiting Then

            _Result = _Refs.Element(RefID).RefName
            If _Result = FileHandler.s_DEFAULT Or _Result = FileHandler.s_Null Then
                _Result = _Refs.Element(RefID).ScheduleName
            Else
                _Result = Trim(_Result)
            End If

            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG10, _Result).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + _Refs.Element(RefID).SN
            _lblRefPart.Font = New Font("Arial", 50.25, FontStyle.Bold)
            _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
        End If

        If type = ShowMsgType.ShowDefine Then

            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG11).Trim + vbCrLf + strMsg
            _lblRefPart.Font = New Font("Arial", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReferenceMsg.StrMsg = _mTest
            _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowSNFail Then

            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG11).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG12).Trim + _Refs.Element(RefID).SN + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG13).Trim + strSN
            _lblRefPart.Font = New Font("Arial", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReferenceMsg.StrMsg = _mTest
            _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.TakeFail Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG24).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG12).Trim + _Refs.Element(RefID).SN + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG13).Trim + strSN
            _lblRefPart.Font = New Font("Arial", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReferenceMsg.StrMsg = _mTest
            _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.Scaner Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG26).Trim
            _lblRefPart.Font = New Font("Arial", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReferenceMsg.StrMsg = _mTest
            _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If
        If type = ShowMsgType.TakeDefineFail Then
            _mTest = _lblRefPart.Text + vbCrLf + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG25).Trim
            _lblRefPart.Font = New Font("Arial", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            _ReferenceMsg.StrMsg = _mTest
            _ReferenceMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)

        End If
        Return True

    End Function

    Protected Function HiddenREF() As Boolean
        _MessageManager.RemoveControl(ReferenceStation.Name)
        If _MessageManager.GetNullStatus Then
            _lblRefPart.SendToBack()
            _lblRefPart.Tag = enumHMI_ERROR_TYPE.None
        End If
        Return True
    End Function


    Protected Sub Schedule_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        If ChangeType = enumChangeType.Auto Then
            Return
        End If

        If _SubStationCfg.Enable Then
            If _Refs.CheckingReferenceName(_AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data) Then
                _Refs.RefreshingCheckList(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                If _Refs.Count = 0 Then
                    _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCEENDCHANGEFAIL, _CurrentShift.ToString, _AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "FAIL"))
                    '样件自动回切
                    _ScheduleManager.RemoveIndicateName(_AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                    '  _AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                Else
                    If _i.StepOutputNumber = 2 Then
                        _listShift.Clear()
                        ' _listShift(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data & "_Shift_" & _CurrentShift.ToString) = ""
                        UpdateList()
                        _Refs.RefMode = True
                        _Refs.RefManual = True
                        _i.StepInputNumber = 4
                    Else
                        _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCEENDCHANGESTEPFAIL, _i.StepOutputNumber.ToString))

                        '样件自动回切
                        _ScheduleManager.RemoveIndicateName(_AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                        '_AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                    End If
                End If
            End If
        Else
            '样件自动回切
            If _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.SelfResistance.ToString) >= 0 Or _AppSchedule.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data.IndexOf(ProductionMode.MasterPart.ToString) >= 0 Then
                _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCEENDCHANGEFAILENABLE, _CurrentShift.ToString, _AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "FAIL"))
                _ScheduleManager.InsertChangeIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
                ' _AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
            End If
        End If
    End Sub

    Protected Sub Article_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)

        _ArticleChange = True
        _listShift.Clear()
        ' _i.StepInputNumber = _i.Address_Home
    End Sub


    Private Sub _Shift_ShiftChange(ByRef CurShift As Integer) Handles _Shift.ShiftChange
        _CurrentShift = CurShift
        _Logger.Logger(_i, _Messager, "Shift Change:" + CurShift.ToString, "Shift_ShiftChange ")
        _ShiftChange = True
    End Sub

    Protected Sub UpdateList()
        _UIStation.CleanRow()
        For i = 0 To _Refs.Count - 1
            _UIStation.AddRow(_Refs.Element(_Refs.Keys(i)).SN, _Refs.Element(_Refs.Keys(i)).LkNumber, _Refs.Element(_Refs.Keys(i)).RefName, _Refs.Element(_Refs.Keys(i)).ProductFamily, _Refs.Element(_Refs.Keys(i)).ScheduleName, _CurrentShift.ToString, _Refs.Element(_Refs.Keys(i)).TestOK)
        Next
    End Sub

    Protected Sub UpdateRefList()
        For i = 0 To _Refs.REFs.Count - 1
            If _Refs.REFs(_Refs.REFs.Keys(i)).ProductFamily = _AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data Then
                If _Refs.REFs(_Refs.REFs.Keys(i)).Enable Then
                    _UIStation.AddRow(_Refs.REFs(_Refs.REFs.Keys(i)).SN, _Refs.REFs(_Refs.REFs.Keys(i)).LkNumber, _Refs.REFs(_Refs.REFs.Keys(i)).RefName, _Refs.REFs(_Refs.REFs.Keys(i)).ProductFamily, _Refs.REFs(_Refs.REFs.Keys(i)).ScheduleName, _CurrentShift.ToString, True)
                    _Refs.strLastScheduleName = _Refs.REFs(_Refs.REFs.Keys(i)).ScheduleName
                End If
            End If
        Next
    End Sub

    Protected Function _CheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ScannerDefine.CheckScannerResult(_i, mScannerResult, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CheckScannerResultCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pCheckScannerResult.EndInvoke(Result)
        _isCallBackRunning = False
    End Sub

    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))
        _i = Nothing
        _Settings = Nothing
        _Language = Nothing
        _Logger = Nothing
        _LocalArticle = Nothing
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
End Class
