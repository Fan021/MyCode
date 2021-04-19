Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class LaserStation
    Inherits StationTypeBase
    Protected _UIStation As LaserUI
    Protected _Laser As ILaser
    Protected _mCmd As String
    Protected _Refs As References
    Protected _LaserDefine As ILaserDefine
    Protected _SNStation As SNStation
    Protected _StartCmdstr As String
    Protected _StartCmd As Boolean
    Protected _StartMode As Boolean
    Protected Delegate Function dGetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef mCmd As String) As Boolean
    Protected pGetSeqentialCommands As New dGetSeqentialCommands(AddressOf _GetSeqentialCommands)
    Protected pGetSeqentialCommandsCB As AsyncCallback = New AsyncCallback(AddressOf _GetSeqentialCommandsCB)
    Public Const Name As String = "LaserStation"

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal Laser As ILaser, ByVal LaserDefine As ILaserDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New LaserUI
            _Laser = Laser
            _LaserDefine = LaserDefine
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
            If _SubStationCfg.SN <> "" Then
                _SNStation = CType(_Stations(_SubStationCfg.SN), SNStation)
            Else
                _UIStation.Start.Enabled = False
            End If

            AddHandler _UIStation.OK.Click, AddressOf btnChangeTemplate_Click
            AddHandler _UIStation.Start.Click, AddressOf btnStart_Click

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
            If Not UpdateMsg(LaserStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _StartCmd = False
                    _StartMode = False
                    _StartCmdstr = ""
                    _mCmd = ""
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not _Laser.Init(_SubStationCfg.Type, _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_FAIL, "FAIL", _Laser.StatusDescription), "Laser.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_PASS, "Successful"), "Laser.Init")
                            _Devices.Add(_SubStationCfg.Name, _Laser)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_PASS, "Disable"), "Laser.Init")

                        End If
                    End If

                Case -98 'Calibrate End
                    _i.StepInputNumber = _i.Address_Home

                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _mCmd = ""
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START))
                        _InternMsg = ""
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _mCmd = ""
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START))
                        _InternMsg = ""
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                    If _StartMode Then
                        _mCmd = ""
                        _UIStation.Start.Enabled = False
                        _StationMode = 3
                        _StartCheckTrigInfoDefineCallBack = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _StartCmd Then
                        _StartCmd = False
                        _i.StepInputNumber = 300
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    _StartCallBack = False
                    CheckStructDeviceInteractionPLCInfo()

                Case 2
                    If _SubStationCfg.SN <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 3
                    If Not _SNStation.isRun Then
                        _SNStation.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4 '产生序列号
                    _SNStation.StartCreatSN = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    If Not _SNStation.StartCreatSN Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SNStation.mSN
                        _SNStation.isRun = False
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 6
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetSeqentialCommands.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _mCmd, pGetSeqentialCommandsCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_DEFINE, "FAIL", _LaserDefine.ErrorMsg))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 7
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUSREADY, "FAIL", KostalArticleKeys.KEY_LASER_TEMPLATE + " is Null"), "LocalArticle.KEY_LASER_TEMPLATE")
                        _i.StepInputNumber = _i.Address_Fail
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8
                    If _Laser.ReadyToWrite Then
                        _Laser.ResetLastResponse()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 9
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetStatus() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUS, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 10
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetStatusReady(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUSREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 11
                    If _Laser.ReadyToWrite Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_TEMPLATE, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data))
                        If _Laser.SetAndGetTemplate(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_TEMPLATE, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 12
                    If _Laser.ReadyToWrite Then
                        If _Laser.SetTemplateReady(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data) Then

                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_TEMPLATEREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 13
                    If _Laser.ReadyToWrite Then
                        If _mCmd <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_CMD, _mCmd))
                            If _Laser.SetAnyCommand(_mCmd) Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_COMMANDREADY, "FAIL", _Laser.StatusDescription))
                                _InternMsg = _Laser.StatusDescription
                                _i.StepInputNumber = _i.Address_Fail
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 14
                    If _Laser.ReadyToWrite Then
                        If _Laser.SetAnyCommandReady("") Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_COMMANDREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 15
                    If _StartMode Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 5
                    End If

                Case 16
                    If _Laser.ReadyToWrite Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_MARK, _mCmd))
                        If _Laser.Start() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_START_MARKREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 17
                    If _Laser.ReadyToWrite Then
                        If _Laser.StartReady() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_START_MARKREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 18
                    If _Laser.ReadyToWrite Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_GETSTATUS))
                        If _Laser.GetStatus() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUS, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 19
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetStatusReady(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUSREADY, "PASS", _Laser.LastResponse))
                            _i.StepInputNumber = _i.StepOutputNumber - 1
                        End If
                    End If

                Case 20
                    If _StartMode Then
                        _StartMode = False
                        _UIStation.Start.Enabled = True
                        _i.StepInputNumber = _i.Address_Home
                    Else
                        _i.StepInputNumber = _i.Address_Pass
                    End If


                Case 300
                    If _StartCmdstr = "GetStatus" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 3
                    End If

                Case 301
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetStatus() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUS, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If


                Case 302
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetStatusReady(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data) Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUSREADY, "PASS", _Laser.LastResponse))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_GETSTATUSREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If


                Case 303
                    If _StartCmdstr = "GetVar" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 3
                    End If

                Case 304
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetVar() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_GETVAR, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If


                Case 305
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetVarReady() Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_GETVARREADY, "PASS", _Laser.LastResponse))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_GETVARREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 306
                    If _StartCmdstr = "GetTemplate" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 3
                    End If

                Case 307
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetGetTemplate() Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_START_TEMPLATE, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 308
                    If _Laser.ReadyToWrite Then
                        If _Laser.GetGetTemplateReady() Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_TEMPLATEREADY, "PASS", _Laser.LastResponse))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LASER_SET_TEMPLATEREADY, "FAIL", _Laser.StatusDescription))
                            _InternMsg = _Laser.StatusDescription
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 309
                    _StartCmdstr = ""
                    _i.StepInputNumber = _i.Address_Home


                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data,
                          _mCmd,
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
                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_LASER_TEMPLATE).Data,
                          _mCmd,
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


    Private Sub btnChangeTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If _i.StepOutputNumber <> _i.Address_Home Then
            _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STEPHOME))
        Else
            _StartMode = True
        End If

    End Sub


    Protected Function _GetSeqentialCommands(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef mCmd As String) As Boolean
        Return _LaserDefine.GetSeqentialCommands(_i, LocalArticle, Devices, Stations, mCmd)
    End Function

    Protected Sub _GetSeqentialCommandsCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetSeqentialCommands.EndInvoke(_mCmd, Result)
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
            _Laser.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

End Class

