Imports System.Windows.Forms
Public Class SNStation
    Inherits StationTypeBase
    Protected _UIStation As SNUI
    Protected _StartCreatSN As Boolean
    Protected _MachineIdentifier As MachineIdentifier
    Protected _SerialNoExternalProducer As ISerialNoGeneratorDefine
    Protected _CheckSN As CheckSN
    Protected _mSN As String = ""
    Protected _SNLable As Label
    Protected Delegate Function dCreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
    Protected pCreateSerialNo As New dCreateSerialNo(AddressOf _CreateSerialNo)
    Protected pCreateSerialNoCB As AsyncCallback = New AsyncCallback(AddressOf _CreateSerialNoCB)
    Public Const Name As String = "SNStation"
    Public Property StartCreatSN As Boolean
        Get
            Return _StartCreatSN
        End Get
        Set(ByVal value As Boolean)
            _StartCreatSN = value
        End Set
    End Property

    Public ReadOnly Property mSN As String
        Get
            Return _mSN
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal SerialNoExternalProducer As ISerialNoGeneratorDefine, Optional ByVal SNLable As Label = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            '初始化Shedule
            _UIStation = New SNUI
            _UI = _UIStation
            _CheckSN = New CheckSN(_i.IdString, _Devices)
            _MachineIdentifier = AppSettings.MachineIdentifier
            _SerialNoExternalProducer = SerialNoExternalProducer
            _SNLable = SNLable
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
            _Language.ReadControlText(_UIStation)
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
            If Not UpdateMsg(SNStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    StartCreatSN = False
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99 'Calibrate End
                    If Not _CheckSN.Init(_SubStationCfg.Config) Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CHECKSN_INIT_FAIL, "FAIL", _CheckSN.StatusDescription), "_CheckSN.Init")
                    Else
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CHECKSN_INIT_PASS, "Successful"), "_CheckSN.Init")
                        _Devices.Add(_SubStationCfg.Name, _CheckSN)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -97
                    _i.StepInputNumber = _i.Address_Home
                    '====================================================================================================

                Case 0  'Home Position
                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _StartCreatSN Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCREATSN))
                        _mSN = ""
                        _StationMode = 0
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_START))
                        _StationMode = 1
                        _LastVariantInfo = _ReadStructDeviceInteraction.stuPlcArticleSet
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_START))
                        _StationMode = 2
                        _LastVariantInfo = _ManualReadStructDeviceInteraction.stuPlcArticleSet
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1 '等待选择变种
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_NEWPART_ARTICLE))
                        End If
                    End If
                Case 2
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data Then
                        _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
                    End If
                    _StartCallBack = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pCreateSerialNo.BeginInvoke(_i, AppSettings, _AppArticle, _Devices, _Stations, pCreateSerialNoCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _mSN <> "" Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREAT_NEW, _mSN))
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If
                Case 4
                    _CheckSN.StartCheckSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    If Not _CheckSN.IsReadRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 6
                    If _CheckSN.EndCheckSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UIStation.AddRow(_mSN, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, AppSettings.MachineIdentifier.TraceId, False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 2
                    End If

                Case 7
                    _CheckSN.StartSaveSN(_mSN)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 8
                    If Not _CheckSN.IsWriteRun Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 9
                    If _CheckSN.EndSaveSN Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UIStation.AddRow(_mSN,
                         _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                         _MachineIdentifier.TraceId,
                         False)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_EXIST, _mSN))
                        _i.StepInputNumber = 2
                    End If

                Case 10
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SN_CREATE, _mSN))
                    If _SNLable IsNot Nothing Then _SNLable.Text = _mSN
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_mSN,
                                         _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                         _MachineIdentifier.TraceId,
                                         True)
                    _StartCreatSN = False
                    _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _mSN
                    If _StationMode = 0 Then
                        _i.StepInputNumber = _i.Address_Home
                    Else
                        UpdateStructDeviceInteractionPassStep1()
                    End If


                Case 1001
                    UpdateStructDeviceInteractionPassStep2()

                Case 2000
                    _StartCreatSN = False
                    If _StationMode = 0 Then
                        _i.StepInputNumber = _i.Address_Home
                    Else
                        UpdateStructDeviceInteractionFailStep1()
                    End If
                    _i.StepInputNumber = _i.Address_Home

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

    Protected Function _CreateSerialNo(ByVal _i As Station, ByVal Settings As Settings, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As String
        Return _SerialNoExternalProducer.CreateSerialNo(_i, Settings, LocalArticle, Devices, Stations)
    End Function

    Protected Sub _CreateSerialNoCB(ByVal Result As IAsyncResult)
        _mSN = pCreateSerialNo.EndInvoke(Result)
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
