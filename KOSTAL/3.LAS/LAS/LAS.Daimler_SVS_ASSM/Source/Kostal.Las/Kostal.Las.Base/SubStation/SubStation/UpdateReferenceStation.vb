Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class UpdateReferenceStation
    Inherits StationTypeBase
    Protected _UIStation As UpdateRefrenceUI
    Protected _Refs As References
    Public Shared Name As String = "UpdateReference"
    Protected mMain As IMainForm
    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New UpdateRefrenceUI
            _UI = _UIStation
            _CheckTrigInfo = CheckTrigInfo
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
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If
            mMain = CType(_Devices("mMainForm"), IMainForm)
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
            If Not UpdateMsg(UpdateReferenceStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructRequestAction.Clear()
                    _WriteStructResponseAction.Clear()
                    _ManualReadStructRequestAction.Clear()
                    _ManualWriteStructResponseAction.Clear()
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_INIT_PASS, "Disable"), "LinControl.Init")
                        End If
                    End If
                Case -98
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -97
                    _i.StepInputNumber = _i.Address_Home
                    '====================================================================================================
                    '====================================================================================================

                Case 0  'Home Position
                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructRequestAction.bulDoPositiveAction Or _ReadStructRequestAction.bulDoNegativeAction Then
                        _InternFail = False
                        _InternPass = False
                        _InternMsg = ""
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal.Add("_ReadStructRequestAction", _ReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal("_ReadStructRequestAction") = _ReadStructRequestAction
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructRequestAction.bulDoPositiveAction Or _ManualReadStructRequestAction.bulDoNegativeAction Then
                        _InternFail = False
                        _InternPass = False
                        _InternMsg = ""
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal.Add("_ManualReadStructRequestAction", _ManualReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal("_ManualReadStructRequestAction") = _ManualReadStructRequestAction
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_START))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                Case 1
                    _a_CheckPLCInfo()
                Case 2
                    If _ReadStructRequestAction.bulDoPositiveAction Then
                        _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                        _ReadStructRequestAction.strActionScheduleName,
                                         True)
                        _i.StepInputNumber = 20
                    Else
                        _i.StepInputNumber = _i.StepInputNumber + 1
                    End If
                Case 3
                    _Refs.RefreshingSchedule(_ReadStructRequestAction.stuPlcArticleSet.strKostalNr, _ReadStructRequestAction.strActionScheduleName)
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                        _ReadStructRequestAction.strActionScheduleName,
                                         False)
                    _i.StepInputNumber = _i.Address_Pass

                Case 20
                    If _ReadStructRequestAction.strActionScheduleName = _Refs.GetLastScheduleName(_ReadStructRequestAction.stuPlcArticleSet.strKostalNr) Then
                        mMain.MainForm_ResetClear()
                        mMain.MainForm_btnClear.Enabled = True
                        _FileHandler.WriteIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "LastSchedule", "")
                        _i.StepInputNumber = _i.Address_Pass
                    Else
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                Case 1000
                    '回写PLC
                    UpdateStructResponseActionPassStep1()

                Case 1001
                    UpdateStructResponseActionPassStep2()

                Case 2000
                    '回写PLC

                    UpdateStructResponseActionFailStep1()

                Case 2001
                    UpdateStructResponseActionFailStep2()

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

    Protected Function _a_CheckPLCInfo() As Boolean
        CheckStructRequestActionPLCInfo()
        Return True
    End Function

    Protected Function _a_CheckRef() As Boolean
        If _ReadStructRequestAction.strActionScheduleName.Contains(LAS_ScheduleMode.MasterPartTest.ToString) Or
            _ReadStructRequestAction.strActionScheduleName.Contains(LAS_ScheduleMode.SelfResistanceTest.ToString) Then
            _i.StepInputNumber = _i.Address_Pass
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function

    Protected Function _a_CheckRefEnd() As Boolean
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Sub _a_bulDoNegativeAction()
        If _ReadStructRequestAction.bulDoNegativeAction Then
            _i.StepInputNumber = _i.StepOutputNumber + 3
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
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
