Imports System.Windows.Forms
Imports System.Drawing

Public Class CounterStation
    Inherits StationTypeBase
    Protected _UIStation As CounterUI
    Protected _iPass As Integer
    Protected _iFail As Integer
    Protected _iTotal As Integer
    Protected _sResult As String
    Property _PassLabel As Label
    Property _FailLabel As Label
    Property _TotalLabel As Label

    Property _lblMessage As Label
    Property _Reset As Button
    Property _ResetFail As Button
    Protected _LineCounter As SurfaceCounter
    Protected _LineArticleCounter As IArticleCounter
    Protected _LineMaintenance As IMaintenance
    Private _mPassword As PassWordForm
    Public Const Name As String = "CounterStation"
    Protected _MessageManager As MessageManager
    Private _CounterController As CounterController

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal lblMessage As Label = Nothing, Optional ByVal PassLabel As Label = Nothing, Optional ByVal FailLable As Label = Nothing, Optional ByVal TotalLable As Label = Nothing, Optional ByVal Reset As Button = Nothing, Optional ByVal ResetFail As Button = Nothing, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _CounterController = New CounterController
            _UIStation = New CounterUI
            _PassLabel = PassLabel
            _FailLabel = FailLable
            _TotalLabel = TotalLable
            _lblMessage = lblMessage
            _CheckTrigInfo = CheckTrigInfo
            _Reset = Reset
            _ResetFail = ResetFail
            _UI = _UIStation
            _LineCounter = New SurfaceCounter(_i, AppSettings, _Language, _PassLabel, _FailLabel, _TotalLabel, _Reset, _ResetFail, False)
            AddHandler _LineCounter.CounterChanged, AddressOf CounterChangedHandler

            _Messager.Construct(_UIStation.Msg)
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + ";Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try

    End Sub

    Private Sub CounterChangedHandler()

        RestoreCounter()

    End Sub

    Public ReadOnly Property CounterController As CounterController
        Get
            Return _CounterController
        End Get
    End Property

    Public Sub RestoreCounter()

        _CounterController.Restore(_LineCounter)

    End Sub

    '初始化List
    Public Overrides Function Init() As Boolean
        Try
            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
            _i.StepInputNumber = _i.Address_Origin
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            _Language.ReadControlText(_UIStation)
            _LineArticleCounter = CType(_Devices(ArticleCounter.sName), IArticleCounter)
            _LineMaintenance = CType(_Devices(Maintenance.sName), Maintenance)
            _mPassword = New PassWordForm
            _mPassword.Init(_i, AppSettings, "UserPassWord")
            ReLoadLanguage()
            _LineMaintenance.ShowTips = _lblMessage
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
            If Not UpdateMsg(CounterStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructRequestAction.Clear()
                    _WriteStructResponseAction.Clear()
                    _ManualReadStructRequestAction.Clear()
                    _ManualWriteStructResponseAction.Clear()
                    _iPass = 0
                    _iFail = 0
                    _iTotal = 0
                    _StationMode = 0
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1
                Case -99
                    _i.StepInputNumber = _i.Address_Home

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
                        _CounterController.ClearResultIndication()
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCOUNTER))
                        _StationMode = 1 'Auto Mode
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal.Add("_ReadStructRequestAction", _ReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal("_ReadStructRequestAction") = _ReadStructRequestAction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructRequestAction.bulDoPositiveAction Or _ManualReadStructRequestAction.bulDoNegativeAction Then
                        _CounterController.ClearResultIndication()
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
                    If _ReadStructRequestAction.bulDoPositiveAction Then
                        IncreasePass()
                    End If

                    If _ReadStructRequestAction.bulDoNegativeAction Then
                        IncreaseFail()
                    End If
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                     _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                     True
                                      )
                    UpdateStructResponseActionPassStep1()
                Case 1001
                    UpdateStructResponseActionPassStep2()

                Case 2000
                    '回写PLC
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                      _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
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
        _LineArticleCounter.Add_Pass(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
        _LineCounter.PassCounterInc()
        _CounterController.Success.Increase()
        Dim strMsg As String = _LineMaintenance.Inc_Count()
        If strMsg <> "" Then ShowMsg(strMsg)
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_COUNTERADDPASS))
        Return True
    End Function

    Protected Function IncreaseFail() As Boolean
        _LineArticleCounter.Add_Fail(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data)
        _LineCounter.FailCounterInc()
        _CounterController.Fail.Increase()
        Dim strMsg As String = _LineMaintenance.Inc_Count()
        If strMsg <> "" Then ShowMsg(strMsg)
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_COUNTERADDFAIL))
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
        _LineCounter.Dispose()
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
    Protected Function ShowMsg(ByVal Msg As String) As Boolean
        _lblMessage.Tag = enumHMI_ERROR_TYPE.MasterMessage
        _MessageManager.InsertControl(CounterStation.Name)
        If _MessageManager.LockMessage Then Return True
        _lblMessage.Font = New Font("Calibri", 40, FontStyle.Bold)
        _lblMessage.Text = Msg
        _lblMessage.BringToFront()
        _lblMessage.Show()
        _lblMessage.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        _LineMaintenance.ShowAlarm = True
        _lblMessage.Tag = enumHMI_ERROR_TYPE.MasterMessage
        Return True
    End Function

    Protected Function HiddenREF() As Boolean
        _MessageManager.RemoveControl(CounterStation.Name)
        If _MessageManager.GetNullStatus Then
            _lblMessage.SendToBack()
            _lblMessage.Hide()
            _lblMessage.Tag = enumHMI_ERROR_TYPE.None
        End If
        Return True
    End Function
End Class
