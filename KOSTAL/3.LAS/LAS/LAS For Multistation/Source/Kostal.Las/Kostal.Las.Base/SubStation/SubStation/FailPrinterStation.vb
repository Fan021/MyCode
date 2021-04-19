Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class FailPrinterStation
    Inherits StationTypeBase
    Protected _UIStation As FailPrinterUI
    Protected _FailPrinter As IFailPrinter
    Protected _FailPrinterDefine As IFailPrinterDefine
    Protected _PLC_OUT_WT As New WT
    Protected Delegate Function dGetFailCollection(ByVal _i As Station, ByVal WT As WT, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Collection
    Protected pGetFailCollection As New dGetFailCollection(AddressOf _GetFailCollection)
    Protected pGetFailCollectionCB As AsyncCallback = New AsyncCallback(AddressOf _GetFailCollectionCB)
    Protected _isCallBackResultCollection As Collection
    Public Const Name As String = "FailPrinterStation"

    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal FailPrinter As IFailPrinter, ByVal FailPrinterDefine As IFailPrinterDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New FailPrinterUI
            _FailPrinter = FailPrinter
            _FailPrinterDefine = FailPrinterDefine
            _CheckTrigInfo = CheckTrigInfo
            _UI = _UIStation
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
            AddHandler _UIStation.OK.Click, AddressOf Send_Click
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
            If Not UpdateMsg(FailPrinterStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not _FailPrinter.Init(_SubStationCfg.Type, _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_FAIL, "FAIL", _FailPrinter.StatusDescription), "FailPrinter.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_PASS, "Successful"), "FailPrinter.Init")
                            _Devices.Add(_SubStationCfg.Name, _FailPrinter)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_INIT_PASS, "Disable"), "FailPrinter.Init")
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
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_START))
                        _InternMsg = ""
                        _StationMode = 1
                        _StartCallBack = False
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_START))
                        _InternMsg = ""
                        _StationMode = 2
                        _StartCallBack = False
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()

                Case 2
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetFailCollection.BeginInvoke(_i, _PLC_OUT_WT, _Devices, _Stations, pGetFailCollectionCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If Not _FailPrinter.Running Then
                            If _FailPrinter.Printer(_isCallBackResultCollection) Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _InternMsg = _FailPrinter.StatusDescription
                                _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FAILPRINTER_RUN, "FAIL", _FailPrinter.StatusDescription), "FailPrinter.Printer")
                                _i.StepInputNumber = _i.Address_Fail
                            End If

                        End If
                    End If
                Case 3
                    If Not _FailPrinter.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 4
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                      _PLC_OUT_WT.PartFailTestStep,
                                      _PLC_OUT_WT.PartFailUnit,
                                      _PLC_OUT_WT.PartFailValue,
                                      _PLC_OUT_WT.PartFailUpperLimit,
                                      _PLC_OUT_WT.PartFailLowerLimit,
                                      _PLC_OUT_WT.PartFailText
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
                                      _PLC_OUT_WT.PartFailTestStep,
                                      _PLC_OUT_WT.PartFailUnit,
                                      _PLC_OUT_WT.PartFailValue,
                                      _PLC_OUT_WT.PartFailUpperLimit,
                                      _PLC_OUT_WT.PartFailLowerLimit,
                                      _PLC_OUT_WT.PartFailText
                                     )
                    UpdateStructDeviceInteractionFailStep1()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

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


    Private Sub Send_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cmdData As New Collection
        _UIStation.OK.Enabled = False
        If Not IsNothing(_FailPrinter) Then
            If _UIStation.TextCmd.Text <> "" And Not _FailPrinter.Running Then
                cmdData.Add(_UIStation.TextCmd.Text)
                _FailPrinter.Printer(cmdData)
            End If
        End If
        _UIStation.OK.Enabled = True
    End Sub

    Protected Function _GetFailCollection(ByVal _i As Station, ByVal WT As WT, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Collection
        Return _FailPrinterDefine.GetFailCollection(_i, WT, Devices, Stations)
    End Function

    Protected Sub _GetFailCollectionCB(ByVal Result As IAsyncResult)
        _isCallBackResultCollection = pGetFailCollection.EndInvoke(Result)
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
            _FailPrinter.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
End Class
