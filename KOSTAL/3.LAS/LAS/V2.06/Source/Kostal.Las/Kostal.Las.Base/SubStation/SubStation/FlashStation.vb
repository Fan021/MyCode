Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class FlashStation
    Inherits StationTypeBase
    Protected _UIStation As FlashUI
    Protected _flash As IFlash
    Protected _Fileds As String()
    Protected _Refs As References
    Protected _FlashDefine As IFlashDefine
    Protected _StartCmdstr As String
    Protected _StartCmd As Boolean
    Protected _StartMode As Boolean
    Protected _Repeat As Boolean
    Protected _iRepeat As Integer = 0
    Protected _LineControl As LineControlStation
    Protected Delegate Function dGetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fileds As String()) As Boolean
    Protected pGetSeqentialCommands As New dGetSeqentialCommands(AddressOf _GetSeqentialCommands)
    Protected pGetSeqentialCommandsCB As AsyncCallback = New AsyncCallback(AddressOf _GetSeqentialCommandsCB)
    Public Const Name As String = "FlashStation"

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal flash As IFlash, ByVal FlashDefine As IFlashDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New FlashUI
            _flash = flash
            _FlashDefine = FlashDefine
            _CheckTrigInfo = CheckTrigInfo
            _UI = _UIStation
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If
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

            AddHandler _UIStation.OK.Click, AddressOf btnCmd_Click

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
            If Not UpdateMsg(FlashStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _StartCmd = False
                    _StartMode = False
                    _Repeat = False
                    _StartCmdstr = ""
                    _Fileds = Nothing
                    _UIStation.AddColumns(20)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not _flash.Init(_SubStationCfg.Type, _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_INIT_FAIL, "FAIL", _flash.StatusDescription), "Flash.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_INIT_PASS, "Successful"), "Flash.Init")
                            _Devices.Add(_SubStationCfg.Name, _flash)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_INIT_PASS, "Disable"), "Flash.Init")
                        End If
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
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START))
                        _InternFail = False
                        _InternPass = False
                        _InternMsg = ""
                        _StationMode = 1
                        _iRepeat = 0
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START))
                        _InternFail = False
                        _InternPass = False
                        _InternMsg = ""
                        _StationMode = 2
                        _iRepeat = 0
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _StartCmd Then
                        _StartCmd = False
                        _i.StepInputNumber = 300
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()

                Case 2  '样件模式不扫描匹配
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    If Not _flash.IsBusy Then
                        _StartCallBack = False
                        _Repeat = True
                        _flash.ResetLastResponse()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        _Fileds = Nothing
                        pGetSeqentialCommands.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _Fileds, pGetSeqentialCommandsCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternFail = True
                            _InternPass = False
                            _Repeat = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_DEFINE, "FAIL", _FlashDefine.ErrorMsg))
                            _InternMsg = _FlashDefine.ErrorMsg
                            _i.StepInputNumber = 7
                        End If
                    End If

                Case 5
                    If Not _flash.IsBusy Then
                        If Not IsNothing(_Fileds) Then
                            For i = 0 To _Fileds.Count - 1
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_START_CMD, _Fileds(i)))
                            Next

                            If _flash.SendRead(_Fileds) Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _Repeat = False
                                _InternFail = True
                                _InternPass = False
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY1, "FAIL", _flash.StatusDescription))
                                _InternMsg = _flash.StatusDescription
                                _i.StepInputNumber = 7
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 6
                    If Not _flash.IsBusy Then
                        If _flash.IsError Then
                            _Repeat = False
                            _InternFail = True
                            _InternPass = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY1, "FAIL", _flash.StatusDescription))
                            _InternMsg = _flash.StatusDescription
                            _i.StepInputNumber = 7
                            Return
                        End If

                        Dim str1 As String = _flash.LastResponse
                        Dim str2 As String = _flash.LastResponse.Replace(SMH_FlashRunner.ACK, "")


                        If _Fileds.Count > str1.Length - str2.Length Then
                            _Repeat = True
                            _InternFail = True
                            _InternPass = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY2, "FAIL", _flash.LastResponse))
                            _InternMsg = _flash.LastResponse
                            _i.StepInputNumber = 7
                            Return
                        End If
                        If Not _flash.IsError And _Fileds.Count <= str1.Length - str2.Length Then
                            _InternFail = False
                            _InternPass = True
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY2, "PASS", _flash.LastResponse))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 7
                    If _InternFail = True Then
                        If _Repeat Then
                            _iRepeat = _iRepeat + 1
                            If _iRepeat >= _SubStationCfg.Repeat Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _i.StepInputNumber = 3
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If
                    If _InternPass = True Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8
                    If _SubStationCfg.LineControl <> "" Then
                        _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_LINECONTROL, "Start"))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_LINECONTROL, "Disable"))
                        _i.StepInputNumber = 12
                    End If

                Case 9
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 10
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

                Case 11
                    If _LineControl.WriteStructResponseAction.bulPartReceived Then
                        _LineControl.ReadStructRequestAction.bulDoNegativeAction = False
                        _LineControl.ReadStructRequestAction.bulDoPositiveAction = False
                        _LineControl.WriteStructResponseAction.bulPartReceived = False

                        If _LineControl.WriteStructResponseAction.bulActionIsPass = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsPass = False
                            _LineControl.isRun = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_LINECONTROL, "Successful"))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                        If _LineControl.WriteStructResponseAction.bulActionIsFail = True Then
                            _LineControl.WriteStructResponseAction.bulActionIsFail = False
                            _LineControl.isRun = False
                            _InternFail = True
                            _InternPass = False
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_LINECONTROL, "FAIL"))
                            _InternMsg = _LineControl.WriteStructResponseAction.strActionResultText
                            _i.StepInputNumber = _i.Address_Fail  '不良
                        End If
                    End If

                Case 12
                    If _InternPass Then
                        _i.StepInputNumber = _i.Address_Pass
                    End If
                    If _InternFail Then
                        _i.StepInputNumber = _i.Address_Fail
                    End If


                Case 300
                    If Not _flash.IsBusy Then
                        _flash.ResetLastResponse()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 301
                    If Not _flash.IsBusy Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_START_CMD, _StartCmdstr))
                        ReDim _Fileds(0)
                        _Fileds(0) = _StartCmdstr
                        If _flash.SendRead(_Fileds) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY1, "FAIL", _flash.StatusDescription))
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 302
                    If Not _flash.IsBusy Then
                        If _flash.IsError Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_SET_COMMANDREADY1, "FAIL", _flash.StatusDescription))
                            _i.StepInputNumber = _i.Address_Home
                        End If
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Flash_Response, _flash.LastResponse))
                        _i.StepInputNumber = _i.Address_Home
                    End If


                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                          _Fileds,
                          True
                          )
                    UpdateStructDeviceInteractionPassStep1()


                Case 1001
                    UpdateStructDeviceInteractionPassStep2()


                Case 2000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                          _Fileds,
                          False
                          )
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


    Private Sub btnCmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If _i.StepOutputNumber <> _i.Address_Home Then
            _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STEPHOME))
        Else
            If _UIStation.Cmd.Text = "" Then
                _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STEPHOME_CMD))
            Else
                _StartCmd = True
                _StartCmdstr = _UIStation.Cmd.Text
            End If
        End If

    End Sub


    Protected Function _GetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef Fileds As String()) As Boolean
        Return _FlashDefine.GetSeqentialCommands(_i, LocalArticle, Devices, Stations, Fileds)
    End Function

    Protected Sub _GetSeqentialCommandsCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetSeqentialCommands.EndInvoke(_Fileds, Result)
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
            _flash.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
End Class
