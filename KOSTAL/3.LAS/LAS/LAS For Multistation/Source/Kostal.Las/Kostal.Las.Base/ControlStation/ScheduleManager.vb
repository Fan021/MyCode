Imports Kostal.Las.ArticleProvider

Public Enum enumChangeResult
    Init = 0
    Waiting
    PASS
    FAIL
    Changed
    Null
End Enum

Public Enum enumChangeType
    Auto = 0
    Manual
End Enum

Public Class ScheduleChangeElement
    Protected _IndicatedName As String
    Protected _ChangeResult As enumChangeResult
    Protected _ChangeType As enumChangeType

    Public Sub New(ByVal IndicatedName As String, ByVal ChangeResult As enumChangeResult, ByVal ChangeType As enumChangeType)
        _IndicatedName = IndicatedName
        _ChangeResult = ChangeResult
        _ChangeType = ChangeType
    End Sub
    Public Property IndicatedName As String
        Set(ByVal value As String)
            _IndicatedName = value
        End Set
        Get
            Return _IndicatedName
        End Get
    End Property

    Public Property ChangeResult As enumChangeResult
        Set(ByVal value As enumChangeResult)
            _ChangeResult = value
        End Set
        Get
            Return _ChangeResult
        End Get
    End Property

    Public Property ChangeType As enumChangeType
        Set(ByVal value As enumChangeType)
            _ChangeType = value
        End Set
        Get
            Return _ChangeType
        End Get
    End Property
End Class



Public Class ScheduleManager
    Inherits StationTypeBase
    Protected _AppSchedule As Schedule
    Protected _synLock As New Object
    Protected _strIndicatedName As String
    Protected _WaitingChangeIndicatedName As New Dictionary(Of String, ScheduleChangeElement)
    Protected _LocalWaitingChangeIndicatedName As New Dictionary(Of String, ScheduleChangeElement)
    Protected _LastIndicatedName As String
    Protected _LockSchedule As Boolean
    Protected _LastMessage As String
    Protected _isManualSelectSchedule As Boolean
    Public Const Name As String = "ScheduleManage"


    Public Property ManualSelectSchedule As Boolean
        Set(ByVal value As Boolean)
            _isManualSelectSchedule = value
        End Set
        Get
            Return _isManualSelectSchedule
        End Get
    End Property

    Public Property LockSchedule As Boolean
        Set(ByVal value As Boolean)
            _LockSchedule = value
        End Set
        Get
            Return _LockSchedule
        End Get
    End Property


    Public Sub New(ByVal StationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(StationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
    End Sub

    Public Overrides Function Init() As Boolean
        Try
            _AppSchedule = CType(_Devices(Schedule.Name), Schedule)
            _i.StepInputNumber = _i.Address_Origin
            _i.StepOutputNumber = -100
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            Return True
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Function

    Public Function InsertChangeIndicatedName(ByVal strIndicatedName As String, Optional ByVal ChangeType As enumChangeType = enumChangeType.Auto) As Boolean
        SyncLock _synLock
            If Not _WaitingChangeIndicatedName.ContainsKey(strIndicatedName) Then
                _WaitingChangeIndicatedName.Add(strIndicatedName, New ScheduleChangeElement(strIndicatedName, enumChangeResult.Init, ChangeType))
            End If
        End SyncLock
        Return True
    End Function

    Public Function GetChangeIndicatedStatus(ByVal strIndicatedName As String) As enumChangeResult
        SyncLock _synLock
            If _WaitingChangeIndicatedName.ContainsKey(strIndicatedName) Then
                Return _WaitingChangeIndicatedName(strIndicatedName).ChangeResult
            Else
                Return enumChangeResult.Null
            End If
        End SyncLock
        Return enumChangeResult.FAIL
    End Function

    Public Function RemoveIndicateName(ByVal strIndicatedName As String) As Boolean
        SyncLock _synLock
            If _WaitingChangeIndicatedName.ContainsKey(strIndicatedName) Then
                _WaitingChangeIndicatedName.Remove(strIndicatedName)
            End If
        End SyncLock
        Return True
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
        Return True
    End Function

    Public Overrides Sub Run()
        Try
            If IsNothing(_i) Then Exit Sub

            _FirstPulse = Not _FirstFlag
            _FirstFlag = True
            _i.Toggle = _i.StepOutputNumber <> _i.StepInputNumber
            _i.StepOutputNumber = _i.StepInputNumber

            _ManualOffPulse = Not _ManualMode And _ManualFlag
            _ManualFlag = _ManualMode
            '==============================================================================
            'StepHeader
            '==============================================================================
            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _isManualSelectSchedule = False
                    _LockSchedule = False
                    _LastIndicatedName = ""
                    _strIndicatedName = ""
                    _i.StepInputNumber = _i.Address_Home

                Case 0
                    SyncLock _object
                        _LocalWaitingChangeIndicatedName.Clear()
                        For Each element As String In _WaitingChangeIndicatedName.Keys
                            _LocalWaitingChangeIndicatedName.Add(element, _WaitingChangeIndicatedName(element))
                        Next
                        If _LocalWaitingChangeIndicatedName.Count > 0 And Not _LockSchedule Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End SyncLock

                Case 1
                    If GetIndicatedNameFromList(_strIndicatedName) Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 2
                    If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data <> _strIndicatedName Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _WaitingChangeIndicatedName.ContainsKey(_strIndicatedName) Then
                            _WaitingChangeIndicatedName(_strIndicatedName).ChangeResult = enumChangeResult.PASS
                        End If
                        _i.StepInputNumber = _i.Address_Home
                    End If
                Case 3
                    _AppSchedule.GetArticle_FromIndicatedName(_strIndicatedName, _LocalWaitingChangeIndicatedName(_strIndicatedName).ChangeType)
                    If _WaitingChangeIndicatedName.ContainsKey(_strIndicatedName) Then
                        If _WaitingChangeIndicatedName(_strIndicatedName).ChangeType = enumChangeType.Manual Then
                            _AppSchedule.ManualChange(_strIndicatedName)
                        End If
                        _WaitingChangeIndicatedName(_strIndicatedName).ChangeResult = enumChangeResult.PASS
                    End If
                    If _LocalWaitingChangeIndicatedName.Count > 1 Then
                        If _AppSchedule.ArticleElements(KostalScheduleKeys.KEY_SCHEDULE_NAME).Data <> _LastIndicatedName Then
                            If _WaitingChangeIndicatedName.ContainsKey(_LastIndicatedName) Then _WaitingChangeIndicatedName(_LastIndicatedName).ChangeResult = enumChangeResult.Changed
                        End If
                    End If
                    _LastIndicatedName = _strIndicatedName
                    _i.StepInputNumber = _i.Address_Home

            End Select
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:" + _i.StepOutputNumber.ToString + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try


    End Sub

    Protected Function GetIndicatedNameFromList(ByRef strIndicatedName As String) As Boolean
        SyncLock _synLock
            Dim strNowIndicatedName As String = ""
            Dim SchedulePriority As enumSchedulePriority = enumSchedulePriority.NULL
            Dim strKeyID As String = ""
            For Each element In _LocalWaitingChangeIndicatedName.Values
                For Each KeyElement As String In _AppSchedule.ArticleListElement.Keys
                    If _AppSchedule.ArticleListElement(KeyElement).IndicatedName = element.IndicatedName Then
                        strKeyID = KeyElement
                    End If
                Next
                If _AppSchedule.ArticleListElement(strKeyID).SchedulePriority > SchedulePriority Then
                    SchedulePriority = _AppSchedule.ArticleListElement(strKeyID).SchedulePriority
                    strNowIndicatedName = _AppSchedule.ArticleListElement(strKeyID).IndicatedName
                End If
            Next

            If SchedulePriority = enumSchedulePriority.NULL Then
                Return False
            Else
                strIndicatedName = strNowIndicatedName
                Return True
            End If
        End SyncLock
    End Function


    Public Overrides Sub Dispose()

    End Sub
End Class
