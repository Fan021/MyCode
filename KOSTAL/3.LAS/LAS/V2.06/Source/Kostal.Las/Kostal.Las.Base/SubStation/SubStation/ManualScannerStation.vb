Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Public Class ManualScannerStation
    Inherits StationTypeBase
    Protected _UIStation As ManualScannerUI
    Protected _LineControl As LineControlStation
    Protected _ScannerSation As ScannerStation
    Protected _ScannerDefine As IScannerDefine
    Protected _MessageManager As MessageManager
    Protected _ManualScannerMsgDefine As IManualScannerMsgDefine
    Protected _lblRefPart As Label
    Protected _ScanResult As String
    Protected _Refs As References
    Protected _variantInfo As New StructVariantInfo
    Protected _OutStructDeviceInteraction As StructDeviceInteraction
    Protected Delegate Function dGetBarcode(ByVal _i As Station, ByVal mScannerResult As String, ByRef _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean

    Protected Delegate Function dCheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pCheckScannerResult As New dCheckScannerResult(AddressOf _CheckScannerResult)
    Protected pCheckScannerResultCB As AsyncCallback = New AsyncCallback(AddressOf _CheckScannerResultCB)

    Protected Delegate Function dGetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pGetMsg As New dGetMsg(AddressOf _GetMsg)
    Protected pGetMsgCB As AsyncCallback = New AsyncCallback(AddressOf _GetMsgCB)
    Public Const Name As String = "ManualScannerStation"
    Public _ScannerTestMsg As String = ""
    Public ReadOnly Property OutStructDeviceInteraction As StructDeviceInteraction
        Get
            Return _OutStructDeviceInteraction
        End Get
    End Property

    Public ReadOnly Property ScanResult As String
        Get
            Return _ScanResult
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ScannerDefine As IScannerDefine, ByVal ManualScannerMsgDefine As IManualScannerMsgDefine, ByVal lblRefPart As Label, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ManualScannerUI
            _ManualScannerMsgDefine = ManualScannerMsgDefine
            _UI = _UIStation
            _lblRefPart = lblRefPart
            _ScannerDefine = ScannerDefine
            _CheckTrigInfo = CheckTrigInfo
            _Messager.Construct(_UIStation.Msg)
            _ReadStructDeviceInteraction = New StructDeviceInteraction
            _OutStructDeviceInteraction = New StructDeviceInteraction
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
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If
            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
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
            If Not UpdateMsg(ManualScannerStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _UIStation.AddColumns()
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _OutStructDeviceInteraction.Clear()
                    _ScanResult = ""
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _ScannerTestMsg = ""
                        _StartCallBack = False
                        _InternMsg = ""
                        _InternPass = False
                        _InternFail = False
                        _ScanResult = ""
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_START))
                        ' ShowMsg(ShowMsgType.ShowSN)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _ScannerTestMsg = ""
                        _StartCallBack = False
                        _InternMsg = ""
                        _InternPass = False
                        _InternFail = False
                        _ScanResult = ""
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_START))
                        ' ShowMsg(ShowMsgType.ShowSN)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()

                Case 2  '样件模式不扫描匹配
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetMsg.BeginInvoke(_i, _ScannerTestMsg, _LocalArticle, _Devices, _Stations, pGetMsgCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            ShowMsg(ShowMsgType.ShowSN)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _ManualScannerMsgDefine.ErrorMsg
                            ShowMsg(ShowMsgType.ShowDefine, _InternMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_DEFINE, "FAIL", _ManualScannerMsgDefine.ErrorMsg))
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 3
                    If _SubStationCfg.Scanner <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_SCAN), "ManualStation.Scan")
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 4
                    If Not _ScannerSation.isRun And Not _ScannerSation.WriteScanEnd Then
                        _ScannerSation.isRun = True
                        _ScannerSation.ReadStartScan = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 5
                    If Not _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                    If _ScannerSation.WriteScanEnd Then
                        _ScanResult = _ScannerSation.ScanResult
                        If _ScanResult <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_SCANRESULT, _ScanResult))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                    If _MessageManager.UpdateMessage Then
                        _MessageManager.UpdateMessage = False
                        ShowMsg(ShowMsgType.ShowSN)
                    End If

                Case 6
                    _ScannerSation.ReadStartScan = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 7
                    If _ScannerSation.WriteScanEnd = False Then
                        _ScannerSation.isRun = False
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8
                    If Not _ReadStructDeviceInteraction.bulPlcDoAction Then
                        HiddenSN()
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
                            _InternPass = True
                            _InternFail = False
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _ScannerDefine.ErrorMsg
                            ShowMsg(ShowMsgType.ShowDefine, _InternMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_DEFINE, "FAIL", _ScannerDefine.ErrorMsg))
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 9
                    If _SubStationCfg.LineControl <> "" Then
                        _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "Start"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_LINECONTROL, "Disable"))
                        _i.StepInputNumber = _i.StepOutputNumber + 4
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
                            ShowMsg(ShowMsgType.ShowLineControl, _InternMsg)
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

                    '回写PLC Pass
                Case 1000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                                        ScanResult, _
                                        True)
                    ShowMsg(ShowMsgType.ShowPass, , _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()
                    HiddenSN()

                Case 2000
                    '回写PLC FAIL
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                                        _ScanResult, _
                                        False)
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalArticleName = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
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


    Protected Function ShowMsg(ByVal type As ShowMsgType, Optional ByVal strMsg As String = "", Optional ByVal strSN As String = "") As Boolean
        Dim _mTest As String
        _MessageManager.InsertControl(ManualScannerStation.Name)
        _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
        If type = ShowMsgType.ShowSN Then
            If _ScannerTestMsg <> "" Then
                _mTest = _ScannerTestMsg
            Else
                _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG23).Trim
            End If

            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)

        End If

        If type = ShowMsgType.ShowDefine Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG16).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG17).Trim + strMsg
            _lblRefPart.Font = New Font("Calibri", 40, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowLineControl Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG18).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG17).Trim + strMsg
            _lblRefPart.Font = New Font("Calibri", 35, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowPass Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG19).Trim + vbCrLf _
                   + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + strSN
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
        End If

        If type = ShowMsgType.ShowFail Then
            _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG20).Trim + vbCrLf _
                  + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG3).Trim + strSN
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        Return True

    End Function

    Protected Function HiddenSN() As Boolean
        _MessageManager.RemoveControl(ManualScannerStation.Name)
        If _MessageManager.GetNullStatus Then
            _lblRefPart.SendToBack()
            _lblRefPart.Hide()
            _lblRefPart.Tag = enumHMI_ERROR_TYPE.None
        End If
        Return True
    End Function

    Protected Function _CheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ScannerDefine.CheckScannerResult(_i, mScannerResult, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CheckScannerResultCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pCheckScannerResult.EndInvoke(Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _GetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ManualScannerMsgDefine.GetMsg(_i, strMsg, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _GetMsgCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetMsg.EndInvoke(_ScannerTestMsg, Result)
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
