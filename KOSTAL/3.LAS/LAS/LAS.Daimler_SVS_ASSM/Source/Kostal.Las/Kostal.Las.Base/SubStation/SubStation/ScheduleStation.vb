Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms

Public Enum LAS_ScheduleMode
    Unknown = 0
    ProductionMode = 1 ' 正常生产
    RetestMode ' 复测模式
    SelfResistanceTest ' 短路样件
    MasterPartTest '标准样件
    AssemblyOnly  '装配模式
    ClearMode  '清盘模式
    UserDefined  '定制模式
    StandAlone '脱机模式
End Enum

Public Class ScheduleStation
    Inherits StationTypeBase

    Protected _AppSchedule As Schedule
    Protected _LocalSchedule As Schedule
    Protected _UISchedule As IScheduleUI
    Protected _mPassword As PassWordForm
    Protected _Refs As References
    Protected _ScheduleManager As ScheduleManager
    Protected _WriteSchedule As Boolean
    Protected _LastScheduleName As String
    Private _OldSchedule As String = String.Empty
    Private _LabelScheduleMode As Label
    Public Const Name As String = "ScheduleStation"

    Public ReadOnly Property AppSchedule As Schedule
        Get
            Return _AppSchedule
        End Get
    End Property
    Public ReadOnly Property LocalSchedule As Schedule
        Get
            Return _LocalSchedule
        End Get
    End Property

    Public Property WriteSchedule As Boolean
        Set(ByVal value As Boolean)
            _WriteSchedule = value
        End Set
        Get
            Return _WriteSchedule
        End Get
    End Property

    Public ReadOnly Property LastScheduleName As String
        Get
            Return _LastScheduleName
        End Get
    End Property

    Public Sub New(ByVal StationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ScheduleUI As IScheduleUI, Optional ByVal LblScheduleMode As Label = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(StationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            '初始化Shedule
            _LocalSchedule = New Schedule(_i, AppSettings, _Language)
            _LocalSchedule.Init()
            _AppSchedule = CType(Devices(Schedule.Name), Schedule)
            _UISchedule = ScheduleUI
            _UI = _UISchedule
            _mPassword = New PassWordForm
            _mPassword.Init(_i, AppSettings, "UserPassWord")
            _ScheduleManager = New ScheduleManager(New SubStationCfg, _Devices, _Stations)
            _ScheduleManager.Init()
            _Stations.Add(ScheduleManager.Name, _ScheduleManager)

            _LabelScheduleMode = CType(IIf(LblScheduleMode Is Nothing, New Label, LblScheduleMode), Label)

            AddHandler _AppArticle.IDChange, AddressOf Article_Change
            AddHandler _AppSchedule.IDChange, AddressOf Schedule_Change
            AddHandler _AppSchedule.IDManualChange, AddressOf IDManualChange
            AddHandler _UISchedule.ScheduleChangeTo, AddressOf Schedule_TextChanged
            AddHandler _UISchedule.AbortScheduleChange, AddressOf Reset_Change
            AddHandler _UISchedule.ComboxScheduleChangeTo, AddressOf cmbSchedules_SelectedIndexChanged
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

            InitDataList()
            _Language.ReadControlText(_UISchedule.Panel)
            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If
            Return True
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Function

    Protected Function InitDataList() As Boolean
        If _AppSchedule Is Nothing Then Return False
        If Not IsNothing(_UISchedule.ScheduleList) Then
            _UISchedule.ScheduleList.Items.Clear()
            If _AppSchedule.ArticleListElement.Count <> 0 Then
                For Each _Element In _AppSchedule.ArticleListElement
                    _UISchedule.ScheduleList.Items.Add(_Element.Value.IndicatedNativeName.Replace(" ", "") + " (" + _Element.Value.IndicatedName + ")")
                Next '
            End If
        End If
        Return True
    End Function

    Protected Function InitSchedule() As Boolean
        If _AppSchedule Is Nothing Then Return False
        If _AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data = "" Then
            Throw New Exception("Key:" + _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data + " Element:" + KostalArticleKeys.KEY_SCHEDULE_NAME + " is Null")
        End If

        ' _ScheduleManager.InsertChangeIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
        ' _LastScheduleName = _AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data
        ' _AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
        _WriteSchedule = True
        Return True
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
        _AppSchedule.ReadArticleLanguage()
        InitDataList()
        _Language.ReadControlText(_UISchedule.Panel)
        Return True
    End Function

    Public Overrides Sub Run()
        Try
            If IsNothing(_i) Then Exit Sub

            _FirstPulse = Not _FirstFlag
            _FirstFlag = True

            _ManualOffPulse = Not _ManualMode And _ManualFlag
            _ManualFlag = _ManualMode

            '    _ScheduleManager.Run()
            '==============================================================================
            'StepHeader
            '==============================================================================
            If Not CheckStepLine() Then Return
            If Not BeforeLine() Then Return
            If Not UpdateMsg(ScheduleStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_INIT, "Start"))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _UISchedule.OKButton.Enabled = False
                        _UISchedule.ResetButton.Enabled = False
                    End If

                Case -98
                    InitSchedule()
                    If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -97
                    Schedule_Change(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data, enumChangeType.Auto)
                    _i.StepInputNumber = _i.Address_Home
                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position
                    If Not IsNothing(_Refs) Then
                        If _Refs.RefMode And Not _Refs.RefManual Then
                            _UISchedule.OKButton.Enabled = False
                            _UISchedule.ResetButton.Enabled = False
                            Return
                        End If
                    End If

                    If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_USER_VERIFICATION).Data.IndexOf("PLC") >= 0 Then
                        _UISchedule.OKButton.Enabled = False
                        _UISchedule.ResetButton.Enabled = False
                        Return
                    End If

                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data = _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data Then
                        _UISchedule.ResetButton.Enabled = False
                    Else
                        _UISchedule.ResetButton.Enabled = True
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

    'Schedule List更改
    Protected Sub cmbSchedules_SelectedIndexChanged(ByVal IndicatedName As String)

        If _AppSchedule.ArticleListElement.Count <> 0 Then
            For Each _Element In _AppSchedule.ArticleListElement
                If _Element.Value.IndicatedName = ReadScheduleNameByFullText(IndicatedName) Then
                    If _Element.Value.UserVerification.VerificationType = enumUserVerificationType.ARTICLE_OCCUPIED Or _Element.Value.UserVerification.VerificationType = enumUserVerificationType.PLC_OCCUPIED Or (Not _Refs.RefEnable And _Element.Value.IndicatedName.IndexOf(ProductionMode.SelfResistance.ToString) >= 0) Or (Not _Refs.RefEnable And _Element.Value.IndicatedName.IndexOf(ProductionMode.MasterPart.ToString) >= 0) Then
                        _UISchedule.OKButton.Enabled = False
                        Return
                    End If
                End If
            Next
        End If

        If ReadScheduleNameByFullText(_UISchedule.ScheduleList.Text) = _AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data Or _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data = ReadScheduleNameByFullText(_UISchedule.ScheduleList.Text) Then
            _UISchedule.OKButton.Enabled = False
        Else
            _UISchedule.OKButton.Enabled = True
        End If

    End Sub

    '手动更改模式
    Protected Sub Schedule_TextChanged(ByVal IndicatedName As String, Optional ByVal IgnorePassword As Boolean = False)
        If _AppSchedule.ArticleListElement.Count <> 0 Then
            For Each _Element In _AppSchedule.ArticleListElement
                If _Element.Value.IndicatedName = ReadScheduleNameByFullText(IndicatedName) Then
                    If Not IgnorePassword Then
                        _mPassword.ChangeMode = False
                        _mPassword.UserVerification = _Element.Value.UserVerification
                        _mPassword.ShowDialog()
                    End If
                    If _mPassword.PassWordValid Or IgnorePassword Then
                        '   _AppSchedule.GetArticle_FromIndicatedName(ReadScheduleNameByFullText(IndicatedName))
                        If _LastScheduleName <> "" Then _ScheduleManager.RemoveIndicateName(_LastScheduleName)
                        _LastScheduleName = ReadScheduleNameByFullText(IndicatedName)
                        _ScheduleManager.InsertChangeIndicatedName(_LastScheduleName, enumChangeType.Manual)
                    End If
                End If
            Next
        End If
    End Sub

    '初始化List Data
    Protected Sub Schedule_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        _Logger.Logger(_i, _Messager, "Schedule Changed. ID:" + mID + " Name:" + _LocalSchedule.ArticleListElement(mID).IndicatedName + " Type:" + ChangeType.ToString)
        If ChangeType = enumChangeType.Manual Then
            If _LocalSchedule.ArticleListElement(mID).SchedulePriority = enumSchedulePriority.Manual Then
                _ScheduleManager.ManualSelectSchedule = True
            End If
        End If

        If Not IsNothing(_UISchedule.ScheduleData) Then
            For i = 0 To _UISchedule.ScheduleList.Items.Count - 1
                If ReadScheduleNameByFullText(_UISchedule.ScheduleList.Items(i).ToString) = _AppSchedule.ArticleListElement(mID).IndicatedName Then
                    _UISchedule.ScheduleList.SelectedIndex = i
                    _OldSchedule = _UISchedule.ScheduleList.Text
                End If
            Next
        End If
        If IsNothing(_UISchedule.ScheduleData) Then
            Return
        End If
        _UISchedule.ScheduleData.Columns.Clear()

        _UISchedule.ScheduleName.Text = _AppSchedule.ArticleListElement(_AppSchedule.ArticleElements(KostalScheduleKeys.KEY_ID).Data).IndicatedNativeName
        If _LabelScheduleMode.Text <> _UISchedule.ScheduleName.Text Then _LabelScheduleMode.Text = _UISchedule.ScheduleName.Text

        If Not IsNothing(_UISchedule.ScheduleData) Then
            For Each _scheElement In _AppSchedule.ArticleElements.Values
                _UISchedule.ScheduleData.Columns.Add(_scheElement.Key, _scheElement.Key)
            Next
            _UISchedule.ScheduleData.Rows.Add()
        End If

        For Each _scheduleElement In _AppSchedule.ArticleElements.Values
            _UISchedule.ScheduleData.Rows(0).Cells(_scheduleElement.Key).Value = _AppSchedule.ArticleElements(_scheduleElement.Key).Data
            If CStr(_UISchedule.ScheduleData.Rows(0).Cells(_scheduleElement.Key).Value) = "" Or
                CStr(_UISchedule.ScheduleData.Rows(0).Cells(_scheduleElement.Key).Value) = "0" Or
                _scheduleElement.Key = KostalScheduleKeys.KEY_USER_VERIFICATION Then
                _UISchedule.ScheduleData.Columns(_scheduleElement.Key).Visible = False
            Else
                _UISchedule.ScheduleData.Columns(_scheduleElement.Key).Visible = True
            End If

        Next
    End Sub

    '更改Schedule
    Protected Sub Article_Change(ByVal mID As String, ByVal ChangeType As enumChangeType)
        '   _ScheduleManager.InsertChangeIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
        '  _AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
    End Sub

    Protected Sub Reset_Change()
        If _LastScheduleName = "" Then Return
        _ScheduleManager.ManualSelectSchedule = False
        _ScheduleManager.RemoveIndicateName(_LastScheduleName)
        _ScheduleManager.InsertChangeIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
        _LastScheduleName = ""

        If _LabelScheduleMode.Text <> _LastScheduleName Then _LabelScheduleMode.Text = _LastScheduleName
        _AppSchedule.GetArticle_FromIndicatedName(_AppArticle.ArticleElements(KostalArticleKeys.KEY_SCHEDULE_NAME).Data)
    End Sub
    '获取IndicateName
    Protected Function ReadScheduleNameByFullText(ByVal strScheduleModeText As String) As String
        Dim strRes As String = ""
        Dim sItems() As String
        If strScheduleModeText.IndexOf("(") < 0 Then Return strScheduleModeText
        sItems = strScheduleModeText.Trim.Split("("c)
        If sItems.Length > 1 Then
            strRes = sItems(1).Replace(")", "").Trim
        End If

        Return strRes
    End Function
    Public Sub IDManualChange(ByVal mID As String)
        _LastScheduleName = mID
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
