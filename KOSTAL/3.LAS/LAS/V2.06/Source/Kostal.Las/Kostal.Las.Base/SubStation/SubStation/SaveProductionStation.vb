Imports System.Windows.Forms
Imports System.Drawing
Public Class SaveProductionStation
    Inherits StationTypeBase
    Protected _UIStation As SaveProductionUI
    Protected _sResult As String
    Property _lblMessage As Label
    Public Const Name As String = "SaveProductionStation"
    Protected _PLC_OUT_WT As New WT
    Protected cProductionInterface As New ProductionInterface
    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property
    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal lblMessage As Label = Nothing, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New SaveProductionUI
            _UI = _UIStation
            _CheckTrigInfo = CheckTrigInfo
            _Messager.Construct(_UIStation.Msg)
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + ";Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try

    End Sub

    Public Overrides Function Init() As Boolean
        Try

            _i.StepInputNumber = _i.Address_Origin
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            _Language.ReadControlText(_UIStation)
            ReLoadLanguage()
            Return True
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + ";Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
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
            If Not UpdateMsg(SaveProductionStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructRequestAction.Clear()
                    _WriteStructResponseAction.Clear()
                    _ManualReadStructRequestAction.Clear()
                    _ManualWriteStructResponseAction.Clear()
                    _StationMode = 0
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                Case -99
                    If _SubStationCfg.Enable Then
                        If Not cProductionInterface.Init(_SubStationCfg.Type, _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, cProductionInterface.StatusDescription)
                        End If
                        _i.StepInputNumber = _i.Address_Home
                    End If


                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position
                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        '_isHome = True
                        'IncreasePass()
                        ' _CounterController.ClearResultIndication()
                        _ManualRefresh = False
                    End If

                    If _ReadStructRequestAction.bulDoPositiveAction Or _ReadStructRequestAction.bulDoNegativeAction Then

                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCOUNTER))
                        _StationMode = 1 'Auto Mode
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal.Add("_ReadStructRequestAction", _ReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal("_ReadStructRequestAction") = _ReadStructRequestAction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructRequestAction.bulDoPositiveAction Or _ManualReadStructRequestAction.bulDoNegativeAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCOUNTER))
                        _StationMode = 2 ' Manual Auto Mode
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal.Add("_ManualReadStructRequestAction", _ManualReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal("_ManualReadStructRequestAction") = _ManualReadStructRequestAction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    '_isHome = False
                    CheckStructRequestActionPLCInfo()

                Case 2 '计数
                    If Not cProductionInterface.Write_RUN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 3
                    cProductionInterface.InSertData(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                                    _PLC_OUT_WT.Number.ToString,
                                                    IIf(_ReadStructRequestAction.bulDoPositiveAction, "PASS", "FAIL").ToString,
                                                    _PLC_OUT_WT.PartFailCode.ToString,
                                                    _PLC_OUT_WT.PartFailLowerLimit.ToString,
                                                    _PLC_OUT_WT.PartFailValue.ToString,
                                                    _PLC_OUT_WT.PartFailUpperLimit.ToString,
                                                    _PLC_OUT_WT.PartFailLocation.ToString,
                                                    _PLC_OUT_WT.PartFailTestStep.ToString,
                                                    _PLC_OUT_WT.PartFailText.ToString)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 4
                    If Not cProductionInterface.Write_RUN Then
                        If cProductionInterface.Status = ProductionInterface.enumProductionInterfaceStatus.NO_ERROR Then
                            _i.StepInputNumber = _i.Address_Pass
                        Else
                            _Logger.LoggerNoStepTextLine(_i, _Messager, cProductionInterface.StatusDescription)
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                      _PLC_OUT_WT.Number.ToString,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                     _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                     _ReadStructRequestAction.bulDoPositiveAction
                                      )
                    UpdateStructResponseActionPassStep1()

                Case 1001
                    UpdateStructResponseActionPassStep2()

                Case 2000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                      _PLC_OUT_WT.Number.ToString,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                      False
                                      )
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


    Protected Function IncreasePass() As Boolean
        Return True
    End Function

    Protected Function IncreaseFail() As Boolean
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
