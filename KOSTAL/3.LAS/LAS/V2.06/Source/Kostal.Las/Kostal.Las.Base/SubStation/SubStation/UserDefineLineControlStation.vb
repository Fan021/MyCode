Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class UserDefineLineControlStation
    Inherits StationTypeBase
    Protected _UIStation As LineControlUI
    Protected _Refs As References
    Protected _PLC_OUT_WT As New WT
    Protected _FailPrinter As FailPrinterStation
    Protected _Counter As CounterStation
    Protected _CaqStation As CaqStation
    Protected _SaveProductionStation As SaveProductionStation
    Protected _LineControlDefine As ILineControlDefine
    Public Const Name As String = "UserDefineLineControlStation"
    Protected UserStationDefine As IUserStationDefine
    Protected StationBase As StationTypeBase
    Property strStationName As String = String.Empty


    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal UserStationDefine As IUserStationDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New LineControlUI
            _UI = _UIStation
            Me.UserStationDefine = UserStationDefine
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
            If Not UpdateMsg(UserDefineLineControlStation.Name) Then Return
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
                    If _SubStationCfg.CAQ <> "" Then
                        _CaqStation = CType(_Stations(_SubStationCfg.CAQ), CaqStation)

                        If _CaqStation Is Nothing Or Not _CaqStation.CaqInited Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CAQ))
                            Return
                        End If
                    End If
                    If _SubStationCfg.SaveProduction <> "" Then
                        _SaveProductionStation = CType(_Stations(_SubStationCfg.SaveProduction), SaveProductionStation)
                        If _SaveProductionStation Is Nothing Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_SaveStation))
                            Return
                        End If
                    End If

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
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    UserStationDefine.GetStationName(_i, _LocalArticle, _Devices, _Stations, strStationName)
                    StationBase = CType(_Stations(strStationName), StationTypeBase)
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                Case 4
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strCustomerNr = _ReadStructRequestAction.stuPlcArticleSet.strCustomerNr
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strKostalArticleName = _ReadStructRequestAction.stuPlcArticleSet.strKostalArticleName
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strKostalNr = _ReadStructRequestAction.stuPlcArticleSet.strKostalNr
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strProductFamily = _ReadStructRequestAction.stuPlcArticleSet.strProductFamily
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strSerialNr = _ReadStructRequestAction.stuPlcArticleSet.strSerialNr
                    StationBase.ReadStructRequestAction.stuPlcArticleSet.strUserDefine = _ReadStructRequestAction.stuPlcArticleSet.strUserDefine
                    StationBase.ReadStructRequestAction.bulDoPositiveAction = _ReadStructRequestAction.bulDoPositiveAction
                    StationBase.ReadStructRequestAction.bulDoNegativeAction = _ReadStructRequestAction.bulDoNegativeAction
                    CType(StationBase, LineControlStation).PLC_OUT_WT = _PLC_OUT_WT
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    If StationBase.WriteStructResponseAction.bulActionIsPass Then
                        _InternFail = False
                        _InternPass = True
                        _InternMsg = ""
                        StationBase.WriteStructResponseAction.Clear()
                        StationBase.ReadStructRequestAction.Clear()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If StationBase.WriteStructResponseAction.bulActionIsFail Then
                        _InternFail = True
                        _InternPass = False
                        _InternMsg = StationBase.WriteStructResponseAction.strActionResultText
                        StationBase.WriteStructResponseAction.Clear()
                        StationBase.ReadStructRequestAction.Clear()
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Case 6
                    _a_Check_FailPrint()
                Case 7
                    _a_Check_FailPrintRun()
                Case 8
                    _a_FailPrint()

                Case 9
                    _a_Write_CAQ()

                Case 10
                    _a_Check_Counter()
                Case 11
                    _a_Check_CounterRun()
                Case 12
                    _a_Counter()

                Case 13
                    _a_Check_SaveProduction()
                Case 14
                    _a_Check_SaveProductionRun()
                Case 15
                    _a_Write_SaveProduction()

                Case 16
                    _a_Wait_FailPrint()
                Case 17
                    _a_Wait_For_CAQ()
                Case 18
                    _a_Wait_Counter()
                Case 19
                    _a_Wait_SaveProduction()

                Case 20
                    If _InternFail Then
                        _i.StepInputNumber = _i.Address_Fail
                    Else
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                Case 1000
                    '回写PLC
                    _UIStation.AddRow(
                       "Trigger",
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                      "",
                      "",
                      "",
                      "",
                      True,
                      "")
                    UpdateStructResponseActionPassStep1()

                Case 1001
                    UpdateStructResponseActionPassStep2()

                Case 2000
                    '回写PLC
                    _UIStation.AddRow(
                       "Trigger",
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                      "",
                      "",
                      "",
                      "",
                      False,
                      "")
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

    Protected Function _a_Check_FailPrint() As Boolean
        If _SubStationCfg.FailPrinter <> "" Then
            _FailPrinter = CType(_Stations(_SubStationCfg.FailPrinter), FailPrinterStation)
            If _ReadStructRequestAction.bulDoNegativeAction Or _InternFail Then
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_PRINT, "Start"))
                _i.StepInputNumber = _i.StepOutputNumber + 1
                Return True
            End If

            If _ReadStructRequestAction.bulDoPositiveAction And Not _InternFail Then
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_PRINT, "Skip"))
                _i.StepInputNumber = _i.StepOutputNumber + 3
            End If
        Else
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_PRINT, "Disable"))
            _i.StepInputNumber = _i.StepOutputNumber + 3
        End If
        Return True
    End Function

    Protected Function _a_Check_FailPrintRun() As Boolean
        If Not _FailPrinter.isRun Then
            _FailPrinter.isRun = True
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function

    Protected Function _a_Write_CAQ() As Boolean

        If _CaqStation IsNot Nothing Then

            If _CaqStation.isRun Then
                If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CAQ, "Error"))
                End If
            ElseIf _CaqStation.CaqDisabled Then

                If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CAQ, "Disable"))
                End If
                _i.StepInputNumber = _i.StepOutputNumber + 1

            Else
                _CaqStation.isRun = True
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CAQ, "Start"))

                _CaqStation.PLC_OUT_WT = _PLC_OUT_WT

                If _CaqStation.PLC_OUT_WT.SerialNumber = "" Then _CaqStation.PLC_OUT_WT.SerialNumber = _ReadStructRequestAction.stuPlcArticleSet.strSerialNr
                If _CaqStation.PLC_OUT_WT.ArticleNumber = "" Then _CaqStation.PLC_OUT_WT.ArticleNumber = ReadStructRequestAction.stuPlcArticleSet.strKostalNr
                If _CaqStation.PLC_OUT_WT.Schedule = "" Then _CaqStation.PLC_OUT_WT.Schedule = ReadStructRequestAction.strActionScheduleName
                If _InternFail Then
                    _CaqStation.PLC_OUT_WT.PartFailText = _CaqStation.PLC_OUT_WT.PartFailText + ";" + _InternMsg
                End If
                _CaqStation.PLC_OUT_WT.TestResult = _ReadStructRequestAction.bulDoPositiveAction And Not _ReadStructRequestAction.bulDoNegativeAction

                _CaqStation.ReadStructDeviceInteraction.stuPlcArticleSet = _ReadStructRequestAction.stuPlcArticleSet
                _CaqStation.ReadStructDeviceInteraction.bulPlcDoAction = True
            End If

            _i.StepInputNumber = _i.StepOutputNumber + 1
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If

        Return True
    End Function

    Protected Function _a_Wait_For_CAQ() As Boolean

        If _CaqStation IsNot Nothing Then

            If _CaqStation.CaqDisabled Then
                _i.StepInputNumber = _i.StepOutputNumber + 1
            Else
                If _CaqStation.ReadStructDeviceInteraction.bulAdsActionIsFail Or _CaqStation.ReadStructDeviceInteraction.bulAdsActionIsPass Then
                    _CaqStation.ReadStructDeviceInteraction.bulPlcDoAction = False
                    _CaqStation.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                    _CaqStation.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                    _CaqStation.isRun = False
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CAQ_END))
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                End If
            End If
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If

        Return True
    End Function

    Protected Function _a_Check_SaveProduction() As Boolean
        If _SubStationCfg.SaveProduction <> "" Then
            _SaveProductionStation = CType(_Stations(_SubStationCfg.SaveProduction), SaveProductionStation)
            _i.StepInputNumber = _i.StepOutputNumber + 1
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 3
        End If
        Return True
    End Function

    Protected Function _a_Check_SaveProductionRun() As Boolean
        If Not _SaveProductionStation.isRun Then
            _SaveProductionStation.isRun = True
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function
    Protected Function _a_Write_SaveProduction() As Boolean
        _SaveProductionStation.PLC_OUT_WT = _PLC_OUT_WT
        If _SaveProductionStation.PLC_OUT_WT.SerialNumber = "" Then _SaveProductionStation.PLC_OUT_WT.SerialNumber = _ReadStructRequestAction.stuPlcArticleSet.strSerialNr
        If _SaveProductionStation.PLC_OUT_WT.ArticleNumber = "" Then _SaveProductionStation.PLC_OUT_WT.ArticleNumber = ReadStructRequestAction.stuPlcArticleSet.strKostalNr
        If _SaveProductionStation.PLC_OUT_WT.Schedule = "" Then _SaveProductionStation.PLC_OUT_WT.Schedule = ReadStructRequestAction.strActionScheduleName
        _SaveProductionStation.ReadStructRequestAction.stuPlcArticleSet = _ReadStructRequestAction.stuPlcArticleSet
        _SaveProductionStation.ReadStructRequestAction.strActionScheduleName = _ReadStructRequestAction.strActionScheduleName
        If _InternFail Then
            _SaveProductionStation.ReadStructRequestAction.bulDoNegativeAction = True
            _SaveProductionStation.ReadStructRequestAction.bulDoPositiveAction = False
        Else
            _SaveProductionStation.ReadStructRequestAction.bulDoNegativeAction = _ReadStructRequestAction.bulDoNegativeAction
            _SaveProductionStation.ReadStructRequestAction.bulDoPositiveAction = _ReadStructRequestAction.bulDoPositiveAction
        End If
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Function _a_Wait_SaveProduction() As Boolean
        If IsNothing(_SaveProductionStation) Then
            _i.StepInputNumber = _i.StepOutputNumber + 1
            Return True
        End If
        If _SaveProductionStation.WriteStructResponseAction.bulPartReceived Then
            _SaveProductionStation.ReadStructRequestAction.bulDoNegativeAction = False
            _SaveProductionStation.ReadStructRequestAction.bulDoPositiveAction = False
            _SaveProductionStation.WriteStructResponseAction.bulActionIsPass = False
            _SaveProductionStation.WriteStructResponseAction.bulActionIsFail = False
            _SaveProductionStation.WriteStructResponseAction.bulPartReceived = False
            _SaveProductionStation.isRun = False
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function

    Protected Function _a_FailPrint() As Boolean
        _FailPrinter.PLC_OUT_WT = _PLC_OUT_WT

        If _FailPrinter.PLC_OUT_WT.SerialNumber = "" Then _FailPrinter.PLC_OUT_WT.SerialNumber = _ReadStructRequestAction.stuPlcArticleSet.strSerialNr
        If _FailPrinter.PLC_OUT_WT.ArticleNumber = "" Then _FailPrinter.PLC_OUT_WT.ArticleNumber = ReadStructRequestAction.stuPlcArticleSet.strKostalNr
        If _FailPrinter.PLC_OUT_WT.Schedule = "" Then _FailPrinter.PLC_OUT_WT.Schedule = ReadStructRequestAction.strActionScheduleName
        If _InternFail Then
            _FailPrinter.PLC_OUT_WT.PartFailText = _FailPrinter.PLC_OUT_WT.PartFailText + ";" + _InternMsg
        End If
        _FailPrinter.ReadStructDeviceInteraction.stuPlcArticleSet = _ReadStructRequestAction.stuPlcArticleSet
        _FailPrinter.ReadStructDeviceInteraction.bulPlcDoAction = True
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Function _a_Wait_FailPrint() As Boolean
        If IsNothing(_FailPrinter) Then
            _i.StepInputNumber = _i.StepOutputNumber + 1
            Return True
        End If
        If _ReadStructRequestAction.bulDoPositiveAction And Not _InternFail Then
            _i.StepInputNumber = _i.StepOutputNumber + 1
            Return True
        End If
        If _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsFail Or _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsPass Then
            _FailPrinter.ReadStructDeviceInteraction.bulPlcDoAction = False
            _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsPass = False
            _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsFail = False
            _FailPrinter.isRun = False
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_PRINT_END))
            _i.StepInputNumber = _i.StepOutputNumber + 1
            Return True
        End If
        Return True
        Return True
    End Function

    Protected Function _a_Check_Counter() As Boolean
        If _SubStationCfg.Counter <> "" Then
            _Counter = CType(_Stations(_SubStationCfg.Counter), CounterStation)
            _i.StepInputNumber = _i.StepOutputNumber + 1
        Else
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_COUNTER, "Disable"))
            _i.StepInputNumber = _i.StepOutputNumber + 3
        End If
        Return True
    End Function

    Protected Function _a_Check_CounterRun() As Boolean
        If Not _Counter.isRun Then
            _Counter.isRun = True
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function

    Protected Function _a_Counter() As Boolean
        _Counter.ReadStructRequestAction.stuPlcArticleSet = _ReadStructRequestAction.stuPlcArticleSet
        _Counter.ReadStructRequestAction.strActionScheduleName = _ReadStructRequestAction.strActionScheduleName
        If _InternFail Then
            _Counter.ReadStructRequestAction.bulDoNegativeAction = True
            _Counter.ReadStructRequestAction.bulDoPositiveAction = False
        Else
            _Counter.ReadStructRequestAction.bulDoNegativeAction = _ReadStructRequestAction.bulDoNegativeAction
            _Counter.ReadStructRequestAction.bulDoPositiveAction = _ReadStructRequestAction.bulDoPositiveAction
        End If
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_COUNTER, "Start"))
        _i.StepInputNumber = _i.StepOutputNumber + 1
        Return True
    End Function

    Protected Function _a_Wait_Counter() As Boolean
        If IsNothing(_Counter) Then
            _i.StepInputNumber = _i.StepOutputNumber + 1
            Return True
        End If
        If _Counter.WriteStructResponseAction.bulPartReceived Then
            _Counter.ReadStructRequestAction.bulDoNegativeAction = False
            _Counter.ReadStructRequestAction.bulDoPositiveAction = False
            _Counter.WriteStructResponseAction.bulActionIsPass = False
            _Counter.WriteStructResponseAction.bulActionIsFail = False
            _Counter.WriteStructResponseAction.bulPartReceived = False
            _Counter.isRun = False
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_COUNTER_END))
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
        Return True
    End Function

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
