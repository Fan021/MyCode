Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Public Class ManualReTestStation
    Inherits StationTypeBase
    Protected _UIStation As ManualReTestUI
    Protected _LineControl As LineControlStation
    Protected _FailPrinter As FailPrinterStation
    Protected _ManualReTestMsgDefine As IManualReTestMsgDefine
    Protected _lblRefPart As Label
    Protected _ManualReTestChangeScheduleDefine As IManualReTestChangeScheduleDefine
    Protected _AppSchedule As Schedule
    Protected _StartRestMode As Boolean
    Protected _ReTestMsg As String
    Protected _ScheduleManager As ScheduleManager
    Protected _MessageManager As MessageManager
    Protected _PLC_OUT_WT As WT
    Protected _Refs As References
    Protected _ReTestList As ReTestList
    Protected _PLCConfirmSignal As StructConfirmSignal
    Protected Delegate Function dGetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal PLC_OUT_WT As WT, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pGetMsg As New dGetMsg(AddressOf _GetMsg)
    Protected pGetMsgCB As AsyncCallback = New AsyncCallback(AddressOf _GetMsgCB)
    Protected Delegate Function dReTestChangeSchedule(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
    Protected pReTestChangeSchedule As New dReTestChangeSchedule(AddressOf _ReTestChangeSchedule)
    Protected pReTestChangeScheduleCB As AsyncCallback = New AsyncCallback(AddressOf _ReTestChangeScheduleCB)
    Public Const Name As String = "ManualReTestStation"

    Public Property ReTestMsg As String
        Get
            Return _ReTestMsg
        End Get
        Set(ByVal value As String)
            _ReTestMsg = value
        End Set
    End Property

    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property

    Public Property PLCConfirmSignal As StructConfirmSignal
        Set(ByVal value As StructConfirmSignal)
            _PLCConfirmSignal = value
        End Set
        Get
            Return _PLCConfirmSignal
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ManualReTestMsgDefine As IManualReTestMsgDefine, ByVal ManualReTestChangeScheduleDefine As IManualReTestChangeScheduleDefine, ByVal lblRefPart As Label, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ManualReTestUI
            _UI = _UIStation
            _lblRefPart = lblRefPart
            _PLC_OUT_WT = New WT
            _PLCConfirmSignal = New StructConfirmSignal
            _ManualReTestMsgDefine = ManualReTestMsgDefine
            _ManualReTestChangeScheduleDefine = ManualReTestChangeScheduleDefine
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
            _AppSchedule = CType(_Devices(Schedule.Name), Schedule)
            If _SubStationCfg.LineControl <> "" Then
                _LineControl = CType(_Stations(_SubStationCfg.LineControl), LineControlStation)
            End If
            If _SubStationCfg.FailPrinter <> "" Then
                _FailPrinter = CType(_Stations(_SubStationCfg.FailPrinter), FailPrinterStation)
            End If
            If _Devices.ContainsKey(ReTestList.Name) Then
                _ReTestList = CType(_Devices(ReTestList.Name), ReTestList)
            End If
            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If
            _ScheduleManager = CType(_Stations(ScheduleManager.Name), ScheduleManager)
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
            If Not UpdateMsg(ManualReTestStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
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
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _InternMsg = ""
                        _StationMode = 1
                        _InternPass = False
                        _InternFail = False
                        _StartCallBack = False
                        _Refs.RefLock = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _InternMsg = ""
                        _StationMode = 2
                        _InternPass = False
                        _InternFail = False
                        _StartCallBack = False
                        _Refs.RefLock = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()

                Case 2
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetMsg.BeginInvoke(_i, _ReTestMsg, _PLC_OUT_WT, _LocalArticle, _Devices, _Stations, pGetMsgCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            ShowMsg(ShowMsgType.ShowDefine, _ReTestMsg)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            ShowMsg(ShowMsgType.ShowFail, _ManualReTestMsgDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _ManualReTestMsgDefine.ErrorMsg)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 3
                    If _PLCConfirmSignal.bulActionIsPass Then
                        _InternMsg = ""
                        _InternPass = True
                        _InternFail = False
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If _PLCConfirmSignal.bulActionIsFail Then
                        _InternMsg = "FailStep:First Confim"
                        _InternPass = False
                        _InternFail = True
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 4
                    If Not _PLCConfirmSignal.bulActionIsPass And Not _PLCConfirmSignal.bulActionIsFail Then
                        If _InternPass Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 4
                        End If
                    End If


                Case 5
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetMsg.BeginInvoke(_i, _ReTestMsg, _PLC_OUT_WT, _LocalArticle, _Devices, _Stations, pGetMsgCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            ShowMsg(ShowMsgType.ShowDefine, _ReTestMsg)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            ShowMsg(ShowMsgType.ShowFail, _ManualReTestMsgDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _ManualReTestMsgDefine.ErrorMsg)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 6
                    If _PLCConfirmSignal.bulActionIsPass Then
                        _InternMsg = ""
                        _InternPass = True
                        _InternFail = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If _PLCConfirmSignal.bulActionIsFail Then
                        _InternMsg = "FailStep:Second Confim"
                        _InternPass = False
                        _InternFail = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 7
                    If Not _PLCConfirmSignal.bulActionIsPass And Not _PLCConfirmSignal.bulActionIsFail Then
                        _MessageManager.LockMessage = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 8
                    If _SubStationCfg.LineControl <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 9
                    If Not _LineControl.isRun Then
                        _LineControl.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 10
                    If _StationMode = 1 Then
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet = _ReadStructDeviceInteraction.stuPlcArticleSet
                    Else
                        _LineControl.ReadStructRequestAction.stuPlcArticleSet = _ManualReadStructDeviceInteraction.stuPlcArticleSet
                    End If

                    _LineControl.PLC_OUT_WT = _PLC_OUT_WT
                    _LineControl.PLC_OUT_WT.PartFailText = _InternMsg
                    _LineControl.ReadStructRequestAction.bulDoPositiveAction = _InternPass
                    _LineControl.ReadStructRequestAction.bulDoNegativeAction = _InternFail
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 11
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
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_LINECONTROL, "FAIL"))
                        ShowMsg(ShowMsgType.ShowLineControl, _LineControl.WriteStructResponseAction.strActionResultText)
                        _i.StepInputNumber = _i.Address_Fail
                    End If


                Case 12
                    If _SubStationCfg.FailPrinter <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 13
                    If Not _FailPrinter.isRun Then
                        _FailPrinter.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 14
                    _FailPrinter.PLC_OUT_WT = _PLC_OUT_WT
                    If _InternFail Then
                        _FailPrinter.PLC_OUT_WT.PartFailText = _FailPrinter.PLC_OUT_WT.PartFailText + ";" + _InternMsg
                    End If
                    _FailPrinter.ReadStructDeviceInteraction.stuPlcArticleSet = _ReadStructRequestAction.stuPlcArticleSet
                    _FailPrinter.ReadStructDeviceInteraction.bulPlcDoAction = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1


                Case 15
                    If _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsFail Or _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsPass Then
                        _FailPrinter.ReadStructDeviceInteraction.bulPlcDoAction = False
                        _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsPass = False
                        _FailPrinter.ReadStructDeviceInteraction.bulAdsActionIsFail = False
                        _FailPrinter.isRun = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 16
                    If _InternFail Then
                        _i.StepInputNumber = _i.Address_Fail
                    Else
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 17
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pReTestChangeSchedule.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, pReTestChangeScheduleCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            ShowMsg(ShowMsgType.ShowFail, _ManualReTestChangeScheduleDefine.ErrorMsg)
                            _Logger.Logger(_i, _Messager, _ManualReTestChangeScheduleDefine.ErrorMsg)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 18
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                                      _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data, _
                                      True
                     )
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    HiddenSN()
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()
                    If _i.StepInputNumber = _i.Address_Home Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 1002
                    If _ReTestList.ReTestListElement.Count = 0 Then
                        _Refs.RefLock = False
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 2000
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, _
                                      _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data, _
                                      False
                                     )

                    HiddenSN()
                    If Not _MessageManager.GetNullStatus Then
                        _MessageManager.UpdateMessage = True
                    End If
                    UpdateStructDeviceInteractionFailStep1()

                Case 2001
                    _Refs.RefLock = False
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
        _MessageManager.InsertControl(ManualReTestStation.Name)
        _MessageManager.LockMessage = True
        Dim _mTest As String
        If type = ShowMsgType.ShowDefine Then
            _mTest = _ReTestMsg
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.Text = _mTest
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_BLUE)
        End If

        If type = ShowMsgType.ShowLineControl Then
            _mTest = strMsg
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If

        If type = ShowMsgType.ShowFail Then
            _mTest = strMsg
            _lblRefPart.Text = _mTest
            _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
            _lblRefPart.BringToFront()
            _lblRefPart.Show()
            _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        End If
        Return True
    End Function

    Protected Function HiddenSN() As Boolean
        _MessageManager.RemoveControl(ManualReTestStation.Name)
        _MessageManager.LockMessage = False
        If _MessageManager.GetNullStatus Then
            _lblRefPart.SendToBack()
            _lblRefPart.Hide()
        End If
        Return True
    End Function

    Protected Function _GetMsg(ByVal _i As Station, ByRef strMsg As String, ByVal PLC_OUT_WT As WT, ByVal _LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ManualReTestMsgDefine.GetMsg(_i, strMsg, PLC_OUT_WT, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _GetMsgCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetMsg.EndInvoke(_ReTestMsg, Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _ReTestChangeSchedule(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Return _ManualReTestChangeScheduleDefine.ChangeSchedule(_i, _LocalArticle, Devices, Stations)
    End Function

    Protected Sub _ReTestChangeScheduleCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pReTestChangeSchedule.EndInvoke(Result)
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
