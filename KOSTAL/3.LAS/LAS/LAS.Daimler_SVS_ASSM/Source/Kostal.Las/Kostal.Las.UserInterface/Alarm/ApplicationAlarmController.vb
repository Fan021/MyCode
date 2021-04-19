Namespace Alarm

    ''' <summary>
    ''' AlarmController for the application
    ''' </summary>
    Public MustInherit Class ApplicationAlarmController
        Inherits NotifyingObject
        Implements Alarm.IApplicationAlarmController

        Private _state As Alarm.AlarmStates = Alarm.AlarmStates.NoAlarm
        Private ReadOnly _dictionaryAlarms As New System.Collections.Generic.Dictionary(Of String, AlarmIndication)()
        Private ReadOnly _collectionAlarms As New ObjectModel.ObservableCollection(Of Alarm.IAlarmIndication)()
        Private ReadOnly _readonlycollectionAlarms As ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication)

        Private ReadOnly _dictionaryIndications As New System.Collections.Generic.Dictionary(Of String, AlarmIndication)()
        Private ReadOnly _collectionIndications As New ObjectModel.ObservableCollection(Of Alarm.IAlarmIndication)()
        Private ReadOnly _readonlycollectionIndications As ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication)

        Protected _funcForReset As Func(Of Boolean) = AddressOf ResetInternal

        Protected Sub New()
            _readonlycollectionAlarms = New ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication)(_collectionAlarms)
            _readonlycollectionIndications = New ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication)(_collectionIndications)
        End Sub

        ''' <summary>
        ''' Gets the summary state of all AlarmChannels.
        ''' </summary>
        ''' <value>
        ''' The state.
        ''' </value>
        Public ReadOnly Property State As Alarm.AlarmStates Implements Alarm.IApplicationAlarmController.State
            Get
                Return _state
            End Get
        End Property

        ''' <summary>
        ''' Resets this alarm status.
        ''' </summary>
        ''' <returns>True, if successful, otherwise false.</returns>
        Public Function Reset() As Boolean Implements Alarm.IApplicationAlarmController.Reset
            Return _funcForReset.Invoke()
        End Function

        Protected Function ResetInternal() As Boolean
            If _state = Alarm.AlarmStates.PhysicalAlarm Then Return False
            If _state = Alarm.AlarmStates.NoAlarm Then Return True
            ChangeAlarm(Alarm.AlarmStates.NoAlarm)
            Return True
        End Function

        ''' <summary>
        ''' Gets the descriptions of the active alarm channels.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property PhysicalAlarms As ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication) Implements Alarm.IApplicationAlarmController.PhysicalAlarms
            Get
                Return _readonlycollectionAlarms
            End Get
        End Property

        Friend Sub AddPhysicalAlarm(id As String, description As String)
            SyncLock _collectionAlarms
                If _dictionaryAlarms.ContainsKey(id) Then Return
                Dim alarmToAdd As New AlarmIndication(id, description)
                _dictionaryAlarms.Add(id, alarmToAdd)
                _collectionAlarms.Add(alarmToAdd)
            End SyncLock
            If _state = Alarm.AlarmStates.PhysicalAlarm Then Return
            ChangeAlarm(Alarm.AlarmStates.PhysicalAlarm)
        End Sub

        Friend Sub RemovePhysicalAlarm(id As String)
            SyncLock _collectionAlarms
                Dim alarmToRemove As AlarmIndication = Nothing
                If Not _dictionaryAlarms.TryGetValue(id, alarmToRemove) Then Return
                _dictionaryAlarms.Remove(id)
                _collectionAlarms.Remove(alarmToRemove)
            End SyncLock
            If _collectionAlarms.Count <> 0 Then Return
            ChangeAlarm(Alarm.AlarmStates.LogicalAlarm)
        End Sub

        Protected Overridable Sub ChangeAlarm(newState As Alarm.AlarmStates)
            If _state = newState Then Return
            _state = newState
            OnPropertyChanged(Member.Of(Function() Me.State))
        End Sub

        Friend Sub AddIndication(id As String, description As String)
            SyncLock _collectionIndications
                If _dictionaryIndications.ContainsKey(id) Then Return
                Dim indicationToAdd As New AlarmIndication(id, description)
                _dictionaryIndications.Add(id, indicationToAdd)
                _collectionIndications.Add(indicationToAdd)
            End SyncLock
        End Sub

        Friend Sub RemoveIndication(id As String)
            SyncLock _collectionIndications
                Dim indicationToRemove As AlarmIndication = Nothing
                If Not _dictionaryIndications.TryGetValue(id, indicationToRemove) Then Return
                _dictionaryIndications.Remove(id)
                _collectionIndications.Remove(indicationToRemove)
            End SyncLock
        End Sub

        ''' <summary>
        ''' Gets the descriptions of the active alarm channels.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Indications As ObjectModel.ReadOnlyObservableCollection(Of Alarm.IAlarmIndication) Implements Alarm.IApplicationAlarmController.Indications
            Get
                Return _readonlycollectionIndications
            End Get
        End Property

    End Class

End Namespace