Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class LineControlStation
    Inherits StationTypeBase
    Protected _UIStation As LineControlUI
    Protected _Refs As References
    Protected _PLC_OUT_WT As New WT
    Protected _FailPrinter As FailPrinterStation
    Protected _Counter As CounterStation
    Protected _CaqStation As CaqStation
    Protected _SaveProductionStation As SaveProductionStation
    Protected _LineControlDefine As ILineControlDefine
    Protected WithEvents _LineControl As LineControl2004
    Protected _LineControlStationDefine As ILineControlStationDefine
    Protected _Listchild As New Dictionary(Of String, ChildElement)
    Protected Delegate Function dLineControlDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef _Listchild As Dictionary(Of String, ChildElement)) As Boolean
    Protected pLineControlDefine As New dLineControlDefine(AddressOf _dLineControlDefine)
    Protected pLineControlDefineCB As AsyncCallback = New AsyncCallback(AddressOf _LineControlDefineCB)
    Public Const Name As String = "LineControlStation"
    Protected Delegate Function dStation(ByVal _i As Station, ByRef strPreviousStation As String, ByRef strCurrentStation As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pGetStation As New dStation(AddressOf _GetStation)
    Protected pGetStationCB As AsyncCallback = New AsyncCallback(AddressOf _GetStationCB)
    Public strPreviousStation As String = String.Empty
    Public strCurrentStation As String = String.Empty
    Private _showMaintenance As ShowMaintenance
    Public Property Listchild As Dictionary(Of String, ChildElement)
        Set(ByVal value As Dictionary(Of String, ChildElement))
            _Listchild = value
        End Set
        Get
            Return _Listchild
        End Get
    End Property

    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal LineControlStationDefine As ILineControlStationDefine, ByVal LineControlDefine As ILineControlDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New LineControlUI
            _UI = _UIStation
            _LineControlDefine = LineControlDefine
            _LineControlStationDefine = LineControlStationDefine
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
            If Not UpdateMsg(LineControlStation.Name) Then Return
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
                        _a_Init_LineController(_SubStationCfg.Config)
                    Else
                        If _i.Toggle Then
                            _Devices.Add(_SubStationCfg.Name, _LineControl)
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
                        strPreviousStation = ""
                        strCurrentStation = ""
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
                        strPreviousStation = ""
                        strCurrentStation = ""
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
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetStation.BeginInvoke(_i, strPreviousStation, strCurrentStation, _LocalArticle, _Devices, _Stations, pGetStationCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _a_CheckRef()
                        Else
                            _InternPass = False
                            _InternFail = True
                            _InternMsg = _LineControlStationDefine.ErrorMsg
                            _Logger.Logger(_i, _Messager, _LineControlStationDefine.ErrorMsg)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If
                Case 3
                    _a_bulDoNegativeAction()
                Case 4
                    _a_ReadPrevious_LineControl()
                Case 5
                    _a_Check_LineControl_ReadPreviousResult()
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
                    _a_ClearLineControlDefine()
                Case 21
                    _a_LineControlDefine()
                Case 22
                    _a_Write_LineControl()
                Case 23
                    _a_Check_LineControl_WriteCurrentResult()
                Case 24
                    If _InternFail Then
                        _i.StepInputNumber = _i.Address_Fail
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

    Protected Sub _a_Init_LineController(ByVal Name As String)

        If Not LineControlInit(Name) Then
            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_INIT_FAIL, "FAIL", _LineControl.StatusDescription), "LineControl.Init")
        Else
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_INIT_PASS, "Successful"), "LineControl.Init")
            _i.StepInputNumber = _i.StepOutputNumber + 1

        End If
    End Sub


    Protected Function LineControlInit(ByVal Name As String) As Boolean

        _LineControl = New LineControl2004(SubStationCfg.Station, SubStationCfg.Name, _i, AppSettings, _Language, Name)

        Return _LineControl.IsInit

    End Function

    Protected Sub _a_bulDoNegativeAction()
        If _ReadStructRequestAction.bulDoNegativeAction Then
            _i.StepInputNumber = _i.StepOutputNumber + 3
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 1
        End If
    End Sub

    Protected Sub _a_ReadPrevious_LineControl()
        If strPreviousStation <> "" Then
            _LineControl.PrviousStation = strPreviousStation
        Else
            _LineControl.PrviousStation = _LineControl.DefaultPreviousTest
        End If

        If _LineControl.PrviousStation <> "NONE" Then
            _LineControl.AdditionalInfos_Clear()
            _LineControl.ReadPreviousStamp(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, strPreviousStation, _PLC_OUT_WT.Schedule)
            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_READ, "Start"))
            _i.StepInputNumber = _i.StepOutputNumber + 1
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 2
        End If

    End Sub


    Protected Sub _a_Check_LineControl_ReadPreviousResult()
        If Not _LineControl.ReadPreviousStamp_RUN Then
            If _LineControl.LastPreviousStamp = LineControl2004.enumPreviousTest.PREVIOUSTEST_PASS Then
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_READRESULTPASS))
                _UIStation.AddRow(
                       "Read",
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                      _LineControl.PrviousStation,
                      "",
                      "",
                      "",
                      True,
                      "")
                _i.StepInputNumber = _i.StepOutputNumber + 1
            Else
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_READRESULTFAIL, "FAIL", _LineControl.StatusDescription))
                _InternFail = True
                _InternPass = False
                _InternMsg = _LineControl.StatusDescription
                _UIStation.AddRow(
                               "Read",
                              _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                              _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                              _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                              _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                              _LineControl.PrviousStation,
                              "",
                              "",
                              "",
                              False,
                              _InternMsg)
                If _LineControl.LastPreviousStamp = LineControl2004.enumPreviousTest.PREVIOUSTEST_LC_FAIL Then
                    _showMaintenance = New ShowMaintenance
                    _showMaintenance.Init(_i, AppSettings, _Language)
                    _showMaintenance.TextBox_Msg.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_LAN)
                    _showMaintenance.TextBox_Msg.Select(0, 0)
                    _showMaintenance.Button_Confirm.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_Button)
                    _showMaintenance.Text = "LC"
                    If _showMaintenance.ShowDialog = DialogResult.OK Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If
                Else
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                End If
            End If
        End If
    End Sub

    Protected Sub _a_ClearLineControlDefine()
        _Listchild.Clear()
        _StartCallBack = False
        _i.StepInputNumber = _i.StepOutputNumber + 1
    End Sub

    Protected Sub _a_LineControlDefine()

        If Not _StartCallBack Then
            _StartCallBack = True
            _isCallBackRunning = True
            pLineControlDefine.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _Listchild, pLineControlDefineCB, Nothing)
        End If
        If _StartCallBack And Not _isCallBackRunning Then
            If _isCallBackResult Then
                _i.StepInputNumber = _i.StepOutputNumber + 1
            Else
                _InternPass = False
                _InternFail = True
                _InternMsg = _LineControlDefine.ErrorMsg
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_DEFINE, "FAIL", _LineControlDefine.ErrorMsg))
                _i.StepInputNumber = _i.StepOutputNumber + 1
            End If
        End If

    End Sub
    Protected Sub _a_Write_LineControl()
        If strCurrentStation <> "" Then
            _LineControl.CurrentStation = strCurrentStation
        Else
            _LineControl.CurrentStation = _LineControl.DefaultCurrentTest
        End If
        If _LineControl.CurrentStation <> "NONE" Then

            If _ReadStructRequestAction.bulDoPositiveAction Then
                If _InternFail Then
                    _Listchild.Clear()
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITE, "NegativeAction", _LineControl.StatusDescription))
                    _LineControl.AdditionalInfos_Clear()
                    _LineControl.AdditionalInfos(0) = _InternMsg
                    If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then _LineControl.AdditionalInfos(1) = "WT " + _PLC_OUT_WT.Number.ToString
                    If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then
                        If _ReadStructRequestAction.strActionScheduleName = "" Then
                            _LineControl.AdditionalInfos(2) = _PLC_OUT_WT.Schedule
                        Else
                            _LineControl.AdditionalInfos(2) = _ReadStructRequestAction.strActionScheduleName
                        End If
                    End If

                    _LineControl.WriteCurrentStamp(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, False, strCurrentStation, _PLC_OUT_WT.Schedule)
                    _UIStation.AddRow(
                           "Write",
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                            _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                            "",
                            _LineControl.CurrentStation,
                            "",
                            "False",
                            True,
                            _InternMsg)

                Else
                    For Each element As ChildElement In _Listchild.Values
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_CHILD, element.Article, element.SN))
                        _LineControl.AddChild(element)
                    Next

                    _Listchild.Clear()
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITE, "PositiveAction", _LineControl.StatusDescription))
                    _LineControl.AdditionalInfos_Clear()
                    _LineControl.AdditionalInfos(0) = _InternMsg
                    If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then _LineControl.AdditionalInfos(1) = "WT " + _PLC_OUT_WT.Number.ToString
                    If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then
                        If _ReadStructRequestAction.strActionScheduleName = "" Then
                            _LineControl.AdditionalInfos(2) = _PLC_OUT_WT.Schedule
                        Else
                            _LineControl.AdditionalInfos(2) = _ReadStructRequestAction.strActionScheduleName
                        End If
                    End If
                    _LineControl.WriteCurrentStamp(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, True, strCurrentStation, _PLC_OUT_WT.Schedule)
                    _UIStation.AddRow(
                             "Write",
                             _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                             _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                             _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                             _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                             "",
                             _LineControl.CurrentStation,
                             "True",
                             "",
                             True,
                             "")
                End If
            End If
            If _ReadStructRequestAction.bulDoNegativeAction Then
                _Listchild.Clear()
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITE, "NegativeAction", _LineControl.StatusDescription))
                _LineControl.AdditionalInfos_Clear()
                _LineControl.AdditionalInfos(0) = _PLC_OUT_WT.PartFailText
                If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then _LineControl.AdditionalInfos(1) = "WT " + _PLC_OUT_WT.Number.ToString
                If AppSettings.PLCConfig(_SubStationCfg.PLCName).LineType > 0 Then
                    If _ReadStructRequestAction.strActionScheduleName = "" Then
                        _LineControl.AdditionalInfos(2) = _PLC_OUT_WT.Schedule
                    Else
                        _LineControl.AdditionalInfos(2) = _ReadStructRequestAction.strActionScheduleName
                    End If
                End If
                _LineControl.WriteCurrentStamp(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, False, strCurrentStation, _PLC_OUT_WT.Schedule)
                _UIStation.AddRow(
                           "Write",
                           _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                           _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                           _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                           _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                           "",
                           _LineControl.CurrentStation,
                           "",
                           "False",
                           True,
                           _PLC_OUT_WT.PartFailText)

            End If
            _i.StepInputNumber = _i.StepOutputNumber + 1
        Else
            _i.StepInputNumber = _i.StepOutputNumber + 2
        End If
    End Sub

    Protected Sub _a_Check_LineControl_WriteCurrentResult()
        If Not _LineControl.WriteCurrentStamp_RUN Then
            If _LineControl.LastWriteResult Then
                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITERESULTPASS))
                _i.StepInputNumber = _i.StepOutputNumber + 1
            Else
                If _LineControl.Status = LineControl2004.enumLineControl2004Status.FailWhileWriteLC Then
                    _showMaintenance = New ShowMaintenance
                    _showMaintenance.Init(_i, AppSettings, _Language)
                    _showMaintenance.TextBox_Msg.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_LAN)
                    _showMaintenance.TextBox_Msg.Select(0, 0)
                    _showMaintenance.Button_Confirm.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_Button)
                    _showMaintenance.Text = "LC"
                    If _showMaintenance.ShowDialog = DialogResult.OK Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITERESULTFAIL, "FAIL", _LineControl.StatusDescription))
                    End If
                Else
                    _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_LINECONTROL_WRITERESULTFAIL, "FAIL", _LineControl.StatusDescription))
                End If
                '_i.StepInputNumber = _i.StepOutputNumber + 1
            End If
        End If

    End Sub

    Protected Function _dLineControlDefine(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef _Listchild As Dictionary(Of String, ChildElement)) As Boolean
        Return _LineControlDefine.LineControlDefine(_i, LocalArticle, Devices, Stations, _Listchild)
    End Function

    Protected Sub _LineControlDefineCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pLineControlDefine.EndInvoke(_Listchild, Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _GetStation(ByVal _i As Station, ByRef strPreviousStation As String, ByRef strCurrentStation As String, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _LineControlStationDefine.GetStation(_i, strPreviousStation, strCurrentStation, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _GetStationCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetStation.EndInvoke(strPreviousStation, strCurrentStation, Result)
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
