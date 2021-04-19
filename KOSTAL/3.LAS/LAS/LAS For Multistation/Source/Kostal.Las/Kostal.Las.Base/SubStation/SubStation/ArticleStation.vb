Public Class ArticleStation
    Inherits StationTypeBase
    Protected _UIStation As ArticleUI
    Protected _WriteArticle As Boolean
    Protected _StartWriteArticle As Boolean
    Protected _variantInfo As New StructVariantInfo
    Protected _VariantInfoDefine As IVariantInfoDefine
    Protected Delegate Function dGetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean
    Protected pGetVariantInfo As New dGetVariantInfo(AddressOf _GetVariantInfo)
    Protected pGetVariantInfoCB As AsyncCallback = New AsyncCallback(AddressOf _GetVariantInfoCB)
    Public Const Name As String = "ArticleStation"

    Public Property WriteArticle As Boolean
        Get
            Return _WriteArticle
        End Get
        Set(ByVal value As Boolean)
            _WriteArticle = value
        End Set
    End Property

    Public Property StartWriteArticle As Boolean
        Get
            Return _StartWriteArticle
        End Get
        Set(ByVal value As Boolean)
            _StartWriteArticle = value
        End Set
    End Property

    Public ReadOnly Property VariantInfo As StructVariantInfo
        Get
            Return _variantInfo
        End Get
    End Property

    Public Sub New(ByVal StationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal VariantInfoDefine As IVariantInfoDefine, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(StationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            '初始化Shedule
            _UIStation = New ArticleUI
            _UI = _UIStation
            _VariantInfoDefine = VariantInfoDefine
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
            _StartWriteArticle = True
            AddHandler _AppArticle.IDChange, AddressOf Article_Change
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
            If Not UpdateMsg(ArticleStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98 'Calibrate End
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position
                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_HOME))
                    End If

                    If _StartWriteArticle Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTARTICLE))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 1
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 2
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 3
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetVariantInfo.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _variantInfo, pGetVariantInfoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _WriteArticle = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPARTVARIANT, "FAIL", _VariantInfoDefine.ErrorMsg), "_VariantInfoDefine.GetVariantInfo")
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 4
                    If Not _WriteArticle Then
                        _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data)
                        _StartWriteArticle = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ENDARTICLE))
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



    '更改Schedule
    Protected Sub Article_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        _StartWriteArticle = True
    End Sub

    Protected Function _GetVariantInfo(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef variantInfo As StructVariantInfo) As Boolean
        Return _VariantInfoDefine.GetVariantInfo(_i, LocalArticle, Devices, Stations, variantInfo)
    End Function

    Protected Sub _GetVariantInfoCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetVariantInfo.EndInvoke(_variantInfo, Result)
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
