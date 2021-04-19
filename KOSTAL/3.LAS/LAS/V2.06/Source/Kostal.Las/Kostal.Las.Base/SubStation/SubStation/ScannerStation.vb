Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class ScannerStation
    Inherits StationTypeBase
    Implements INotifyPropertyChanged
    Protected _UIStation As ScannerUI
    Protected _Refs As References
    Protected WithEvents _Scanner As IScanner
    Protected _ScannerDefine As IScannerDefine
    Protected _LineControl As LineControlStation
    Protected _OutStructDeviceInteraction As StructDeviceInteraction
    Protected _ScannerCommandDefine As IScannerCommandDefine
    Protected _ReadStartScan As Boolean
    Protected _WriteScanEnd As Boolean
    Protected _ScanResult As String
    Protected _iRepeat As Integer = 0
    Protected _Repeat As Boolean
    Protected _NewPartMsg As ShowPicMsg
    Protected Delegate Function dCheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pCheckScannerResult As New dCheckScannerResult(AddressOf _CheckScannerResult)
    Protected pCheckScannerResultCB As AsyncCallback = New AsyncCallback(AddressOf _CheckScannerResultCB)
    Public Const Name As String = "ScannerStation"
    Protected _AutoReadStartScan As Boolean = False
    Protected _LastScannedSerialNumber As String
    Public Event PropertyChanged(sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Public AutoScanPass As Boolean = False
    Public AutoScanFail As Boolean = False
    Public strTrigOn As String = String.Empty
    Public strTrigOff As String = String.Empty
    Public iTimeOut As Integer = 3000

    Protected Delegate Function dGetCommand(ByVal _i As Station, ByRef strTrigOnCmd As String, ByRef strTrigOffCmd As String, ByRef iTimeOut As Integer, ByVal iRepeat As Integer, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pGetCommand As New dGetCommand(AddressOf _GetCommand)
    Protected pGetCommandCB As AsyncCallback = New AsyncCallback(AddressOf _GetCommandCB)

    Public Property ReadStartScan As Boolean
        Get
            Return _ReadStartScan
        End Get
        Set(ByVal value As Boolean)
            _ReadStartScan = value
        End Set
    End Property

    Public Property AutoReadStartScan As Boolean
        Get
            Return _AutoReadStartScan
        End Get
        Set(ByVal value As Boolean)
            _AutoReadStartScan = value
        End Set
    End Property

    Public Property WriteScanEnd As Boolean
        Set(ByVal value As Boolean)
            _WriteScanEnd = value
        End Set
        Get
            Return _WriteScanEnd
        End Get
    End Property

    Public ReadOnly Property ScanResult As String
        Get
            Return _ScanResult
        End Get
    End Property

    Public Property LastScannedSerialNumber As String
        Get
            Return _LastScannedSerialNumber
        End Get
        Set(ByVal value As String)

            If _LastScannedSerialNumber = value Then Return

            _LastScannedSerialNumber = value

            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("LastScannedSerialNumber"))

        End Set
    End Property

    Public ReadOnly Property Scanner As Scanner
        Get
            Return CType(_Scanner, Base.Scanner)
        End Get
    End Property

    Public ReadOnly Property OutStructDeviceInteraction As StructDeviceInteraction
        Get
            Return _OutStructDeviceInteraction
        End Get
    End Property


    Public Property NewPartMsg As ShowPicMsg
        Get
            Return _NewPartMsg
        End Get
        Set(ByVal value As ShowPicMsg)
            _NewPartMsg = value
        End Set
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal Scanner As IScanner, ByVal ScannerDefine As IScannerDefine, ByVal ScannerCommandDefine As IScannerCommandDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ScannerUI
            _ScannerCommandDefine = ScannerCommandDefine
            _Scanner = Scanner
            _OutStructDeviceInteraction = New StructDeviceInteraction
            _ScannerDefine = ScannerDefine
            _CheckTrigInfo = CheckTrigInfo
            _NewPartMsg = New ShowPicMsg
            _UI = _UIStation
            _Messager.Construct(_UIStation.Msg)
            _Devices.Add(_SubStationCfg.Name, _Scanner)
            AddHandler _UIStation.EventTrigON, AddressOf TrigON
            AddHandler _Scanner.DataReceived, AddressOf DataReceived
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
            _Language.ReadControlText(_UIStation)
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If

            ReLoadLanguage()
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
        Return True
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
            If Not UpdateMsg(ScannerStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _iRepeat = 0
                    _UIStation.Result.BackColor = Drawing.Color.White
                    _UIStation.AddColumns()
                    _Repeat = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _Scanner Is Nothing Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_SCANNER_WRONG_TYPE))
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98
                    If _SubStationCfg.Enable Then
                        If _SubStationCfg.Config.Split(CChar(",")).Length >= 3 Then
                            _SubStationCfg.Config = _SubStationCfg.Config.Split(CChar(","))(0) + "," + _SubStationCfg.Config.Split(CChar(","))(1) + "," + AppSettings.ConfigFolder + _SubStationCfg.Config.Split(CChar(","))(2)
                        End If
                        If Not _Scanner.Init(_SubStationCfg.Type, _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_SCANNER_INIT_FAIL, "FAIL", _Scanner.StatusDescription), "Scanner.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_SCANNER_INIT_PASS, "PASS"), "Scanner.Init")
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_SCANNER_INIT_PASS, "Disable", _Scanner.StatusDescription), "Scanner.Init")
                        End If
                    End If

                Case -97
                    _i.StepInputNumber = _i.Address_Home

                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        iTimeOut = 3000
                        strTrigOn = ""
                        strTrigOff = ""
                        _iRepeat = 0
                        _InternMsg = ""
                        _NewPartMsg.StrMsg = ""
                        _InternPass = False
                        _InternFail = False
                        _Repeat = False
                        _ScanResult = ""
                        _Scanner.ScanResult = ""
                        _UIStation.Result.Text = ""
                        _UIStation.Result.BackColor = Drawing.Color.White
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_START))
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        iTimeOut = 3000
                        strTrigOn = ""
                        strTrigOff = ""
                        _iRepeat = 0
                        _InternMsg = ""
                        _NewPartMsg.StrMsg = ""
                        _InternPass = False
                        _InternFail = False
                        _Repeat = False
                        _ScanResult = ""
                        _Scanner.ScanResult = ""
                        _UIStation.Result.Text = ""
                        _UIStation.Result.BackColor = Drawing.Color.White
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_START))
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ReadStartScan Then
                        iTimeOut = 3000
                        strTrigOn = ""
                        strTrigOff = ""
                        _Scanner.ScanResult = ""
                        _ScanResult = ""
                        _StationMode = 3
                        _UIStation.Result.Text = ""
                        _UIStation.Result.BackColor = Drawing.Color.White
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUALSCAN_START))
                        _i.StepInputNumber = 300
                        Exit Select
                    End If

                    If _AutoReadStartScan Then
                        AutoScanPass = False
                        AutoScanFail = False
                        _Scanner.ScanResult = ""
                        _ScanResult = ""
                        _StationMode = 3
                        _UIStation.Result.Text = ""
                        _UIStation.Result.BackColor = Drawing.Color.White
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUALSCAN_START))
                        _i.StepInputNumber = 400
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    Me.LastScannedSerialNumber = ""
                    _StartCallBack = False
                    CheckStructDeviceInteractionPLCInfo()

                Case 2  '样件模式不扫描匹配
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetCommand.BeginInvoke(_i, strTrigOn, strTrigOff, iTimeOut, _iRepeat, _LocalArticle, _Devices, _Stations, pGetCommandCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _ScannerCommandDefine.ErrorMsg
                            _Logger.Logger(_i, _Messager, _ScannerCommandDefine.ErrorMsg)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 3               '等待异步扫描结果
                    If Not _Scanner.Running Then
                        _Scanner.Scan(iTimeOut, strTrigOn, strTrigOff)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TRIGON))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
                    If Not _Scanner.Running Then
                        If _Scanner.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                            _Repeat = False
                            _ScanResult = _Scanner.ScanResult
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_RESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Repeat = True
                            _ScanResult = _Scanner.ScanResult
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_RESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 5
                    If Not _Scanner.Running Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TRIGOFF))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 6
                    If Not _Scanner.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 7
                    If _Repeat Then
                        _iRepeat = _iRepeat + 1
                        If _iRepeat >= _SubStationCfg.Repeat Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name))
                            _NewPartMsg.StrMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name)
                            _NewPartMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name)
                            _i.StepInputNumber = _i.StepOutputNumber + 2
                        Else
                            _StartCallBack = False
                            _i.StepInputNumber = 2 '重新扫描
                        End If
                    Else
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8

                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pCheckScannerResult.BeginInvoke(_i, _ScanResult, _LocalArticle, _Devices, _Stations, pCheckScannerResultCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            Me.LastScannedSerialNumber = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                            _InternPass = True
                            _InternFail = False
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternPass = False
                            _InternFail = True
                            _NewPartMsg.StrMsg = _ScannerDefine.ErrorMsg
                            _NewPartMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                            _InternMsg = _ScannerDefine.ErrorMsg
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_DEFINE, "FAIL", _ScannerDefine.ErrorMsg))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 9
                    If _SubStationCfg.LineControl <> "" Then
                        _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "Start"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "Disable"))
                        _i.StepInputNumber = 13
                    End If

                Case 10
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet.strCustomerNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet.strKostalArticleName = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data
                    _LineControl.ReadStructRequestAction.stuPlcArticleSet.strProductFamily = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data
                    _LineControl.ReadStructRequestAction.strActionScheduleName = ""
                    _LineControl.PLC_OUT_WT.SerialNumber = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _LineControl.PLC_OUT_WT.ArticleNumber = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    _LineControl.PLC_OUT_WT.Schedule = ""
                    _LineControl.PLC_OUT_WT.PartFailText = _InternMsg
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = _InternPass
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = _InternFail
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 12
                    If _LineControl.WriteStructResponseAction.bulPartReceived Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False

                        If _LineControl.WriteStructResponseAction.bulActionIsPass = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsPass = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "Successful"))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _LineControl.WriteStructResponseAction.bulActionIsFail = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsFail = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "FAIL"))
                            _InternMsg = _LineControl.WriteStructResponseAction.strActionResultText
                            _i.StepInputNumber = _i.Address_Fail  '不良
                        End If
                    End If

                Case 13
                    If _InternPass Then
                        _i.StepInputNumber = _i.Address_Pass
                    End If
                    If _InternFail Then
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                '等待异步扫描
                Case 300
                    If Not _Scanner.Running Then
                        If _Scanner.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR And _Scanner.ScanResult <> "" Then
                            _ScanResult = _Scanner.ScanResult
                            _UIStation.AddRow("", "", "", "", ScanResult, True)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If
                    If Not _ReadStartScan Then
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 301
                    If Not _Scanner.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 302
                    If _Scanner.ScanResult <> "" Then
                        _Scanner.Beep()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 303
                    _WriteScanEnd = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 304
                    If _WriteScanEnd = True And _ReadStartScan = False Then
                        _WriteScanEnd = False
                        _i.StepInputNumber = _i.Address_Home
                    End If

                    If _WriteScanEnd = False And _ReadStartScan = False Then
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 400
                    If Not _Scanner.Running Then
                        _Scanner.Scan(iTimeOut, strTrigOn, strTrigOff)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TRIGON))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 401
                    If Not _Scanner.Running Then
                        If _Scanner.Status = enumDevice_ErrorCodes.DEVICE_ERROR_NO_ERROR Then
                            _Repeat = False
                            _ScanResult = _Scanner.ScanResult
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_RESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Repeat = True
                            _ScanResult = _Scanner.ScanResult
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_RESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 402
                    If Not _Scanner.Running Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TRIGOFF))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 403
                    If Not _Scanner.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 404
                    If _Repeat Then
                        _iRepeat = _iRepeat + 1
                        If _iRepeat >= _SubStationCfg.Repeat Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name))
                            _NewPartMsg.StrMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name)
                            _NewPartMsg.TextColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_TIMEOUT, _i.Name)
                            AutoScanFail = True
                            _i.StepInputNumber = _i.StepOutputNumber + 2
                        Else
                            _i.StepInputNumber = 400
                        End If
                    Else
                        AutoScanPass = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 405
                    If Not _AutoReadStartScan Then
                        AutoScanPass = False
                        AutoScanFail = False
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 406
                    If Not _AutoReadStartScan Then
                        AutoScanPass = False
                        AutoScanFail = False
                        _i.StepInputNumber = _i.Address_Home
                    End If



                    '回写PLC Pass
                Case 1000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                        ScanResult,
                                        True)
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()

                Case 2000
                    '回写PLC FAIL
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                        _ScanResult,
                                        False)
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionFailStep1()

                Case 2001
                    UpdateStructDeviceInteractionFailStep2()

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


    Protected Sub TrigON()
        _UIStation.Result.Text = ""
        _UIStation.Result.BackColor = Drawing.Color.White
        _UIStation.TrigON.Enabled = False
        _Scanner.Scan(iTimeOut, strTrigOn, strTrigOff)
    End Sub
    Protected Sub TrigOFF()
    End Sub
    Protected Sub DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        Dim ReceiveMsg As String
        If Pass Then
            ReceiveMsg = "Barcode Receive. Result:True Data:" + Result
        Else
            ReceiveMsg = "Barcode Receive. Result:False Message:" + ErrorMsg + " Data:" + Result
        End If

        If _UIStation.Result.InvokeRequired Then
            'DataReceived（Pass， Result， ErrorMsg）
            _UIStation.Result.Invoke(Sub()
                                         If Pass Then
                                             _UIStation.Result.BackColor = Drawing.Color.White
                                         Else
                                             _UIStation.Result.BackColor = Drawing.Color.Red
                                         End If
                                         _UIStation.TrigON.Enabled = True
                                         _UIStation.Result.Text = Result
                                         _Logger.Logger(_i, _Messager, ReceiveMsg)
                                     End Sub)
        Else
            If Pass Then
                _UIStation.Result.BackColor = Drawing.Color.White
            Else
                _UIStation.Result.BackColor = Drawing.Color.Red
            End If
            _UIStation.TrigON.Enabled = True
            _UIStation.Result.Text = Result
            _Logger.Logger(_i, _Messager, ReceiveMsg)
        End If
    End Sub

    Protected Function _CheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ScannerDefine.CheckScannerResult(_i, mScannerResult, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CheckScannerResultCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pCheckScannerResult.EndInvoke(Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _GetCommand(ByVal _i As Station, ByRef strTrigOnCmd As String, ByRef strTrigOffCmd As String, ByRef iTimeOut As Integer, ByVal iRepeate As Integer, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ScannerCommandDefine.GetCommand(_i, strTrigOnCmd, strTrigOffCmd, iTimeOut, iRepeate, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _GetCommandCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetCommand.EndInvoke(strTrigOn, strTrigOff, iTimeOut, Result)
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
        If _SubStationCfg.Enable Then
            _Scanner.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

End Class
