Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Public Class ManualStation
    Inherits StationTypeBase
    Protected _UIStation As ManualUI
    Protected _LineControl As LineControlStation
    Protected _ScannerSation As ScannerStation
    Protected _ScannerDefine As IScannerDefine
    Protected _lblRefPart As Label
    Protected _ScanResult As String
    Protected _variantInfo As New StructVariantInfo
    Protected _MessageManager As MessageManager
    Protected Delegate Function dCheckScannerResult(ByVal _i As Station, ByVal mScannerResult As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pCheckScannerResult As New dCheckScannerResult(AddressOf _CheckScannerResult)
    Protected pCheckScannerResultCB As AsyncCallback = New AsyncCallback(AddressOf _CheckScannerResultCB)
    Public Const Name As String = "ManualStation"
    Public ReadOnly Property ScanResult As String
        Get
            Return _ScanResult
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ScannerDefine As IScannerDefine, ByVal lblRefPart As Label, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ManualUI
            _UI = _UIStation
            _lblRefPart = lblRefPart
            _ScannerDefine = ScannerDefine
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
            If Not UpdateMsg(ManualStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _UIStation.AddColumns()
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

                    If Not _UIStation.TrigON.Enabled Then
                        ShowMsg(ShowMsgType.ShowSN)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_START))
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

                Case 2
                    If _SubStationCfg.Scanner <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_SCAN), "ManualStation.Scan")
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 3
                    If Not _ScannerSation.isRun And Not _ScannerSation.WriteScanEnd Then
                        _ScannerSation.isRun = True
                        _ScannerSation.ReadStartScan = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
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

                    If _UIStation.TrigON.Enabled Then  '手动结束
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
                    If _UIStation.TrigON.Enabled Then  '手动结束
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
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            ShowMsg(ShowMsgType.ShowDefine, _ScannerDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_DEFINE, "FAIL", _ScannerDefine.ErrorMsg))
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 8
                    InitvariantInfo()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 9
                    If _SubStationCfg.LineControl <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_LINECONTROL), "ManualStation.Scan")
                        _i.StepInputNumber = _i.Address_Fail
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
                    _LineControl.PLC_OUT_WT.PartFailText = "Manual Fail."
                    _LineControl.PLC_OUT_WT.Schedule = "Manual Fail"
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 12
                    If _LineControl.WriteStructResponseAction.bulPartReceived And _LineControl.WriteStructResponseAction.bulActionIsPass Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulActionIsFail = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False
                        _LineControl.WriteStructResponseAction.bulActionIsPass = False
                        _LineControl.isRun = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_LINECONTROLEND, _ScanResult))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If _LineControl.WriteStructResponseAction.bulPartReceived And _LineControl.WriteStructResponseAction.bulActionIsFail Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulActionIsFail = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False
                        _LineControl.WriteStructResponseAction.bulActionIsPass = False
                        _LineControl.isRun = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUAL_LINECONTROLEND, _ScanResult))
                        ShowMsg(ShowMsgType.ShowLineControl, _LineControl.WriteStructResponseAction.strActionResultText)
                        _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                                          False)
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 13
                    _ScanResult = ""
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                               True)
                    ShowMsg(ShowMsgType.ShowPass, , _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUALENDPASS, _ScanResult))
                    _i.StepInputNumber = _i.Address_Home


                Case 2000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                               False)
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = ""
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_MANUALENDFAIL, _ScanResult))
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
        _MessageManager.InsertControl(ManualStation.Name)
        If type = ShowMsgType.ShowSN Then
            If _lblRefPart.Text = "" Then

                _mTest = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REF_MSG15).Trim
                _lblRefPart.Text = _mTest
                _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
                _lblRefPart.BringToFront()
                _lblRefPart.Show()
                _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)
            End If
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
        _MessageManager.RemoveControl(ManualStation.Name)
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

