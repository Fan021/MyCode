Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms



Public Class ForcamStation
    Inherits StationTypeBase
    Protected _UIStation As ForcamUI
    Public Const Name As String = "ForcamStation"
    Public _ScannerTestMsg As String = ""
    Protected _AdsValue As Integer = 0
    Protected _LastAdsValue As Integer = 0
    Protected _Forcam As Forcam
    Public Property AdsValue As Integer
        Get
            Return _AdsValue
        End Get
        Set(value As Integer)
            _AdsValue = value
        End Set
    End Property



    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ForcamUI
            _UI = _UIStation
            _CheckTrigInfo = CheckTrigInfo
            _Messager.Construct(_UIStation.Msg)
            _ReadStructDeviceInteraction = New StructDeviceInteraction
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
            If Not UpdateMsg(ForcamStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _UIStation.AddColumns()
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _AdsValue = 0
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98
                    If _SubStationCfg.Enable Then
                        _Forcam = New Forcam(_SubStationCfg, _i, AppSettings, _SubStationCfg.Config)
                        _Devices.Add(_SubStationCfg.Name, _Forcam)
                        If _Forcam.Status <> enumForcamStatus.Initialized Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_INIT_PASS, "FAIL", _Forcam.StatusDescription), "FormCam.Init")
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_INIT_PASS, "Disable"), "FormCam.Init")
                        End If
                    End If

                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _AdsValue > 0 And _AdsValue <> _LastAdsValue Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_START, _AdsValue.ToString()))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If



                Case 1
                    If _AdsValue = 2 Then
                        _i.StepInputNumber = 10
                    ElseIf _AdsValue = 102 Then
                        _i.StepInputNumber = 30
                    Else
                        _i.StepInputNumber = 20
                    End If



                Case 10
                    If _i.Toggle Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_WAITING))
                    End If
                    If Not _Forcam.Start_RUN Then
                        _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Start", True)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    _Forcam.Start()
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_START1, _AdsValue.ToString()))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 12
                    If _i.Toggle Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_WAITING))
                    End If
                    If Not _Forcam.Start_RUN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 13
                    If _Forcam.Status <> enumForcamStatus.Initialized Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_Start_PASS, "FAIL", _Forcam.StatusDescription), "FormCam.Start")
                    Else
                        _LastAdsValue = _AdsValue
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_Start_PASS, "PASS"), "FormCam.Start")
                        _i.StepInputNumber = _i.Address_Home
                    End If


                Case 20
                    _LastAdsValue = _AdsValue
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_DONE_PASS, "PASS"), "FormCam.DONE")
                    _i.StepInputNumber = _i.Address_Home


                Case 30
                    If _i.Toggle Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_WAITING))
                    End If
                    If Not _Forcam.Complete_RUN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 31
                    Select Case _LastAdsValue
                        Case 32
                            _Forcam.Complete(1, 0)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", True)
                        Case 52
                            _Forcam.Complete(0, 1)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", False)
                        Case 212
                            _Forcam.Complete(2, 0)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", True)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", True)
                        Case 222
                            _Forcam.Complete(1, 1)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", True)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", False)
                        Case 232
                            _Forcam.Complete(0, 2)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", False)
                            _UIStation.AddRow(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _AdsValue, "Complete", False)
                    End Select
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_COMPLETE, _AdsValue.ToString()))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 32
                    If _i.Toggle Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_WAITING))
                    End If
                    If Not _Forcam.Start_RUN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 33
                    If _Forcam.Status <> enumForcamStatus.Initialized Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_Start_PASS, "FAIL", _Forcam.StatusDescription), "FormCam.Complete")
                    Else
                        _LastAdsValue = _AdsValue
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_FORCAM_Start_PASS, "PASS"), "FormCam.Complete")
                        _i.StepInputNumber = _i.Address_Home
                    End If

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


    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))
        If _SubStationCfg.Enable Then
            _Forcam.Quit()
        End If
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
