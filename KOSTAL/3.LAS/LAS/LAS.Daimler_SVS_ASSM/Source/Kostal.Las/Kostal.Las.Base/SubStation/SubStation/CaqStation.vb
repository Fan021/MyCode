
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

''' <summary>
''' Class:      CaqStation 
''' Author:     Wang Yumin
''' Version:    1.0.0.0
''' Date:       2018.07.14
''' Description:SubStation which is to write Caq onto Db for FPY parts. 
''' </summary>
''' <remarks> FPY:FirstPartYield </remarks>
Public Class CaqStation
    Inherits StationTypeBase
    Protected _UIStation As CAQUI
    Protected _Caq As CAQ
    Protected _PLC_OUT_WT As New WT
    Public Const Name As String = "CaqStation"


    Public Property PLC_OUT_WT As WT
        Set(ByVal value As WT)
            _PLC_OUT_WT = value
        End Set
        Get
            Return _PLC_OUT_WT
        End Get
    End Property

    Public ReadOnly Property CaqDisabled As Boolean
        Get
            Return _Caq.IsDisabled
        End Get
    End Property

    Public ReadOnly Property CaqInited As Boolean
        Get
            Return _Caq.IsInit
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)

        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New CAQUI
            _UI = _UIStation

            _Caq = New CAQ(_SubStationCfg, _i, AppSettings, _Language, _Stations)
            '_CaqFpyDefine = CaqFpyDefine
            _CheckTrigInfo = CheckTrigInfo

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
            'AddHandler _UIStation.OK.Click, AddressOf Send_Click
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
            If Not UpdateMsg(CaqStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not _Caq.Init Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_INIT_FAIL, "FAIL", _Caq.StatusDescription), "CAQ.Init")
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_INIT_PASS, "Successful"), "CAQ.Init")
                            _Devices.Add(_SubStationCfg.Name, _Caq)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_INIT_PASS, "Disable"), "CAQ.Init")
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
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_START))
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
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_START))
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
                    If Not _Caq.Write_RUN Then
                        If _Caq.Write(_PLC_OUT_WT, _PLC_OUT_WT.TestResult, _PLC_OUT_WT.ArticleNumber) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternMsg = _Caq.StatusDescription
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_RUN, "FAIL", _Caq.StatusDescription), "CAQ.Write")
                            _i.StepInputNumber = _i.Address_Fail
                        End If

                    End If

                Case 3
                    If Not _Caq.Write_RUN Then

                        If Not _Caq.Pass Then

                            _InternMsg = _Caq.StatusDescription
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_RUN, "FAIL", _Caq.StatusDescription), "CAQ.Write")

                            If Not Caq_ReCall() Then
                                _i.StepInputNumber = _i.Address_Fail
                            Else
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            End If

                        End If

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
                                      _PLC_OUT_WT.TestResult,
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
                                      False,
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

    Protected Function Caq_ReCall() As Boolean

        Try
            _Caq.Dispose()
        Catch ex As Exception

        End Try

        If CaqInit() Then
            _Caq.Write(_PLC_OUT_WT, _PLC_OUT_WT.TestResult, _PLC_OUT_WT.ArticleNumber)

        Else
            _i.Text = _Caq.StatusDescription
            _Logger.PcMasterError(_i)
            Return False
        End If

        Return True


    End Function

    Protected Function CaqInit() As Boolean

        _i.StepTextLine = "CaqInit"
        _Caq = New CAQ(_SubStationCfg, _i, AppSettings, _Language, _Stations)
        If Not _Caq.IsInit Then
            _i.Text = _Caq.StatusDescription
            _Logger.PcMasterError(_i)
            Return False
        End If

        If _Caq.IsDisabled Then
            _i.Text = "_CAQ " & _Caq.StatusDescription
            _Logger.Logger(_i)
            Return True
        End If

        _i.Text = _Caq.StatusDescription
        _Logger.Logger(_i)

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
        If _SubStationCfg.Enable Then
            _Caq.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

End Class
