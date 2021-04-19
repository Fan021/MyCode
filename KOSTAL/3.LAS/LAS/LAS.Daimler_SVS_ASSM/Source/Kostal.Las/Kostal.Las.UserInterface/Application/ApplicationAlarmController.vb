''' <summary>
''' AlarmController for the application
''' </summary>
Public Class ApplicationAlarmController
    Inherits Kostal.Testman.Framework.Core.Alarm.ApplicationAlarmController

    ''' <summary>
    ''' Defines an AlarmChannel item for internal usage
    ''' </summary>
    Private Class AlarmChannelItem

        ''' <summary>
        ''' The identifier
        ''' </summary>
        Public ReadOnly Id As String

        ''' <summary>
        ''' The text
        ''' </summary>
        Public ReadOnly Text As String

        ''' <summary>
        ''' The value to mark the alarm as active
        ''' </summary>
        Public ReadOnly ValueForAlarm As Boolean

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AlarmChannelItem"/> class.
        ''' </summary>
        ''' <param name="id">The identifier.</param>
        ''' <param name="text">The text.</param>
        ''' <param name="valueForAlarm">The value to indicate an active alarm.</param>
        Public Sub New(id As String, text As String, valueForAlarm As Boolean)
            Me.Id = id
            Me.Text = New Globalization.Localizer(True, 2).GetLocalizedString(text)
            Me.ValueForAlarm = valueForAlarm
        End Sub

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Overrides Function ToString() As String
            Return MyBase.ToString() + System.String.Format(" - Id = {0} - ValueForActive = {1} - Text = {2}", Id, ValueForAlarm, Text)
        End Function

    End Class

    ''' <summary>
    ''' Defines an IndicationChannel item for internal usage
    ''' </summary>
    Private Class IndicationChannelItem
        Inherits AlarmChannelItem

        ''' <summary>
        ''' The indication should also be refreshed, when no alarm channel is active
        ''' </summary>
        Public ReadOnly RefreshAlsoWhenNoAlarm As Boolean

        ''' <summary>
        ''' The polling delay time
        ''' </summary>
        Public ReadOnly PollingDelayTime As Integer

        ''' <summary>
        ''' Initializes a new instance of the <see cref="IndicationChannelItem"/> class.
        ''' </summary>
        ''' <param name="id">The identifier.</param>
        ''' <param name="text">The text.</param>
        ''' <param name="valueForIndication">The value of the channel, when the indication should be active.</param>
        ''' <param name="refreshAlsoWhenNoAlarm">The indication should also be refreshed, when no alarm channel is active.</param>
        ''' <param name="pollingDelayTime">The polling delay time.</param>
        Public Sub New(id As String, text As String, valueForIndication As Boolean, refreshAlsoWhenNoAlarm As Boolean, pollingDelayTime As Integer)
            MyBase.New(id, text, valueForIndication)
            Me.RefreshAlsoWhenNoAlarm = refreshAlsoWhenNoAlarm
            Me.PollingDelayTime = pollingDelayTime
        End Sub

        ''' <summary>
        ''' Returns a <see cref="System.String" /> that represents this instance.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="System.String" /> that represents this instance.
        ''' </returns>
        Public Overrides Function ToString() As String
            Return MyBase.ToString() + System.String.Format(" - RefreshAlsoWhenNoAlarm = {0}", RefreshAlsoWhenNoAlarm)
        End Function

    End Class

    Private ReadOnly _managerApplicationState As Head.ApplicationStateManager
    Private ReadOnly _controllerHardware As HardwareController
    Private ReadOnly _dictionaryAlarmChannels As New Dictionary(Of Kostal.Hal.Core.DigitalInput, AlarmChannelItem)
    Private ReadOnly _dictionaryIndicationChannels As New Dictionary(Of Kostal.Hal.Core.DigitalInput, IndicationChannelItem)
    Private ReadOnly _dictionaryResetChannels As New Dictionary(Of Kostal.Hal.Core.DigitalInput, Integer)

    ''' <summary>
    ''' Initializes a new instance of the <see cref="ApplicationAlarmController"/> class.
    ''' </summary>
    ''' <param name="managerApplicationState">The manager of the ApplicationState.</param>
    ''' <param name="controllerHardware">The controller for the hardware.</param>
    Friend Sub New(managerApplicationState As Head.ApplicationStateManager, controllerHardware As HardwareController)
        _managerApplicationState = managerApplicationState
        _controllerHardware = controllerHardware
    End Sub

#Region "AlarmChannel"

    ''' <summary>
    ''' Adds an alarm channel.
    ''' </summary>
    ''' <param name="channelId">The if of a digital input channel for the alarm.</param>
    ''' <param name="valueForAlarm">The value of the channel, when the alarm should be active.</param>
    ''' <param name="text">The text that id displayed, if the aalrm channel is active (will be translated by inner functionality).</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddAlarmChannel(channelId As String, valueForAlarm As Boolean, text As String) As Boolean
        Return AddAlarmChannel(channelId, valueForAlarm, text, 50)
    End Function

    ''' <summary>
    ''' Adds an alarm channel.
    ''' </summary>
    ''' <param name="channelId">The if of a digital input channel for the alarm.</param>
    ''' <param name="valueForAlarm">The value of the channel, when the alarm should be active.</param>
    ''' <param name="text">The text that id displayed, if the alarm channel is active (will be translated by inner functionality).</param>
    ''' <param name="pollingDelayTime">The polling delay time.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddAlarmChannel(channelId As String, valueForAlarm As Boolean, text As String, pollingDelayTime As Integer) As Boolean
        Dim digitalinputchannelAlarm As Kostal.Hal.Core.DigitalInput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalInput)(channelId)
        If digitalinputchannelAlarm Is Nothing Then Return False
        Return AddAlarmChannel(digitalinputchannelAlarm, valueForAlarm, text, pollingDelayTime)
    End Function

    ''' <summary>
    ''' Adds an alarm channel.
    ''' </summary>
    ''' <param name="digitalinputchannelAlarm">The digital input channel for the alarm.</param>
    ''' <param name="valueForAlarm">The value of the channel, when the alarm should be active.</param>
    ''' <param name="text">The text that id displayed, if the alarm channel is active (will be translated by inner functionality).</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddAlarmChannel(digitalinputchannelAlarm As Kostal.Hal.Core.DigitalInput, valueForAlarm As Boolean, text As String) As Boolean
        Return AddAlarmChannel(digitalinputchannelAlarm, valueForAlarm, text, 50)
    End Function

    ''' <summary>
    ''' Adds an alarm channel.
    ''' </summary>
    ''' <param name="digitalinputchannelAlarm">The digital input channel for the alarm.</param>
    ''' <param name="valueForAlarm">The value of the channel, when the alarm should be active.</param>
    ''' <param name="text">The text that id displayed, if the alarm channel is active (will be translated by inner functionality).</param>
    ''' <param name="pollingDelayTime">The polling delay time.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddAlarmChannel(digitalinputchannelAlarm As Kostal.Hal.Core.DigitalInput, valueForAlarm As Boolean, text As String, pollingDelayTime As Integer) As Boolean
        If digitalinputchannelAlarm Is Nothing Then Return False
        If _dictionaryAlarmChannels.ContainsKey(digitalinputchannelAlarm) Then Return True
        _controllerHardware.Polling.AddChannel(Me, digitalinputchannelAlarm, pollingDelayTime)
        AddHandler digitalinputchannelAlarm.ValueChanged, AddressOf AlarmChannel_ValueChanged
        _dictionaryAlarmChannels.Add(digitalinputchannelAlarm, New AlarmChannelItem(digitalinputchannelAlarm.Name, text, valueForAlarm))
        AlarmChannel_ValueChanged(digitalinputchannelAlarm)
        Return True
    End Function

    ''' <summary>
    ''' Called, when the value of an alarm channel has changed.
    ''' </summary>
    ''' <param name="channel">The channel at which the value has changed.</param>
    Private Sub AlarmChannel_ValueChanged(channel As Hal.Core.Channel)
        Dim digitalinputchannelAlarm As Kostal.Hal.Core.DigitalInput = TryCast(channel, Kostal.Hal.Core.DigitalInput)
        If digitalinputchannelAlarm Is Nothing Then Return
        Dim itemAlarmChannel As AlarmChannelItem = Nothing
        If Not _dictionaryAlarmChannels.TryGetValue(digitalinputchannelAlarm, itemAlarmChannel) Then Return
        If digitalinputchannelAlarm.StoredValue = itemAlarmChannel.ValueForAlarm Then
            AddPhysicalAlarm(itemAlarmChannel.Id, itemAlarmChannel.Text)
        Else
            RemovePhysicalAlarm(itemAlarmChannel.Id)
        End If
    End Sub

#End Region

#Region "IndicationChannel"

    ''' <summary>
    ''' Adds an indication channel.
    ''' </summary>
    ''' <param name="channelId">The if of a digital input channel for the indication.</param>
    ''' <param name="valueForIndication">The value of the channel, when the indication should be active.</param>
    ''' <param name="text">The text that id displayed, if the indication channel is active (will be translated by inner functionality).</param>
    ''' <param name="refreshAlsoWhenNoAlarm">If true, the indication channel is polled all the time, otherwise only, when an alarm channel is active.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddIndicationChannel(channelId As String, valueForIndication As Boolean, text As String, refreshAlsoWhenNoAlarm As Boolean) As Boolean
        Return AddIndicationChannel(channelId, valueForIndication, text, refreshAlsoWhenNoAlarm, 200)
    End Function

    ''' <summary>
    ''' Adds an indication channel.
    ''' </summary>
    ''' <param name="channelId">The if of a digital input channel for the indication.</param>
    ''' <param name="valueForIndication">The value of the channel, when the indication should be active.</param>
    ''' <param name="text">The text that id displayed, if the indication channel is active (will be translated by inner functionality).</param>
    ''' <param name="refreshAlsoWhenNoAlarm">If true, the indication channel is polled all the time, otherwise only, when an alarm channel is active.</param>
    ''' <param name="pollingDelayTime">The polling delay time.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddIndicationChannel(channelId As String, valueForIndication As Boolean, text As String, refreshAlsoWhenNoAlarm As Boolean, pollingDelayTime As Integer) As Boolean
        Dim digitalinputchannelIndication As Kostal.Hal.Core.DigitalInput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalInput)(channelId)
        If digitalinputchannelIndication Is Nothing Then Return False
        Return AddIndicationChannel(digitalinputchannelIndication, valueForIndication, text, refreshAlsoWhenNoAlarm, pollingDelayTime)
    End Function

    ''' <summary>
    ''' Adds an indication channel.
    ''' </summary>
    ''' <param name="digitalinputchannelIndication">The digital input channel for the indication.</param>
    ''' <param name="valueForIndication">The value of the channel, when the indication should be active.</param>
    ''' <param name="text">The text that id displayed, if the indication channel is active (will be translated by inner functionality).</param>
    ''' <param name="refreshAlsoWhenNoAlarm">If true, the indication channel is polled all the time, otherwise only, when an alarm channel is active.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddIndicationChannel(digitalinputchannelIndication As Kostal.Hal.Core.DigitalInput, valueForIndication As Boolean, text As String, refreshAlsoWhenNoAlarm As Boolean) As Boolean
        Return AddIndicationChannel(digitalinputchannelIndication, valueForIndication, text, refreshAlsoWhenNoAlarm, 200)
    End Function

    ''' <summary>
    ''' Adds an indication channel.
    ''' </summary>
    ''' <param name="digitalinputchannelIndication">The digital input channel for the indication.</param>
    ''' <param name="valueForIndication">The value of the channel, when the indication should be active.</param>
    ''' <param name="text">The text that id displayed, if the indication channel is active (will be translated by inner functionality).</param>
    ''' <param name="refreshAlsoWhenNoAlarm">If true, the indication channel is polled all the time, otherwise only, when an alarm channel is active.</param>
    ''' <param name="pollingDelayTime">The polling delay time.</param>
    ''' <returns>Returns true, if seccessful otherwise false.</returns>
    Public Function AddIndicationChannel(digitalinputchannelIndication As Kostal.Hal.Core.DigitalInput, valueForIndication As Boolean, text As String, refreshAlsoWhenNoAlarm As Boolean, pollingDelayTime As Integer) As Boolean
        If digitalinputchannelIndication Is Nothing Then Return False
        If _dictionaryIndicationChannels.ContainsKey(digitalinputchannelIndication) Then Return True
        Dim itemIndicationChannel As New IndicationChannelItem(digitalinputchannelIndication.Name, text, valueForIndication, refreshAlsoWhenNoAlarm, pollingDelayTime)
        _dictionaryIndicationChannels.Add(digitalinputchannelIndication, itemIndicationChannel)
        If refreshAlsoWhenNoAlarm OrElse State = Base.Alarm.AlarmStates.LogicalAlarm Then ActivateIndicationChannel(digitalinputchannelIndication, itemIndicationChannel)
        Return True
    End Function

    ''' <summary>
    ''' Activates the indication channels.
    ''' </summary>
    Private Sub ActivateIndicationChannels()
        For Each kvpIndicationChannel As KeyValuePair(Of Kostal.Hal.Core.DigitalInput, IndicationChannelItem) In _dictionaryIndicationChannels
            If Not kvpIndicationChannel.Value.RefreshAlsoWhenNoAlarm Then ActivateIndicationChannel(kvpIndicationChannel.Key, kvpIndicationChannel.Value)
        Next
    End Sub

    ''' <summary>
    ''' Activates an indication channel.
    ''' </summary>
    ''' <param name="digitalinputchannelIndication">The digitalinputchannel for the indication.</param>
    ''' <param name="itemIndicationChannel">The corresponding item for the indication channel.</param>
    Private Sub ActivateIndicationChannel(digitalinputchannelIndication As Kostal.Hal.Core.DigitalInput, itemIndicationChannel As IndicationChannelItem)
        Dim channelIndication As Kostal.Hal.Core.Channel = _controllerHardware.Polling.AddChannel(Me, digitalinputchannelIndication, itemIndicationChannel.PollingDelayTime)
        AddHandler channelIndication.ValueChanged, AddressOf IndicationChannel_ValueChanged
        IndicationChannel_ValueChanged(digitalinputchannelIndication)
    End Sub

    ''' <summary>
    ''' Deactivates the indication channels.
    ''' </summary>
    Private Sub DeactivateIndicationChannels()
        For Each kvpIndicationChannel As KeyValuePair(Of Kostal.Hal.Core.DigitalInput, IndicationChannelItem) In _dictionaryIndicationChannels
            If Not kvpIndicationChannel.Value.RefreshAlsoWhenNoAlarm Then
                _controllerHardware.Polling.RemoveChannel(Me, kvpIndicationChannel.Key)
                RemoveHandler kvpIndicationChannel.Key.ValueChanged, AddressOf IndicationChannel_ValueChanged
            End If
        Next
    End Sub

    ''' <summary>
    ''' Called, when the value of an indication channel has changed.
    ''' </summary>
    ''' <param name="channel">The channel at which the value has changed.</param>
    Private Sub IndicationChannel_ValueChanged(channel As Hal.Core.Channel)
        Dim digitalinputchannelIndication As Kostal.Hal.Core.DigitalInput = TryCast(channel, Kostal.Hal.Core.DigitalInput)
        If digitalinputchannelIndication Is Nothing Then Return
        Dim itemIndicationChannel As IndicationChannelItem = Nothing
        If Not _dictionaryIndicationChannels.TryGetValue(digitalinputchannelIndication, itemIndicationChannel) Then Return
        If digitalinputchannelIndication.StoredValue = itemIndicationChannel.ValueForAlarm Then
            AddIndication(itemIndicationChannel.Id, itemIndicationChannel.Text)
        Else
            RemoveIndication(itemIndicationChannel.Id)
        End If
    End Sub

#End Region

#Region "ResetChannel"

    Public Function AddResetChannel(channelId As String) As Boolean
        Return AddResetChannel(channelId, 50)
    End Function

    Public Function AddResetChannel(channelId As String, pollingDelayTime As Integer) As Boolean
        Dim digitalinputchannelReset As Kostal.Hal.Core.DigitalInput = _controllerHardware.TryFindChannel(Of Kostal.Hal.Core.DigitalInput)(channelId)
        If digitalinputchannelReset Is Nothing Then Return False
        Return AddResetChannel(digitalinputchannelReset, pollingDelayTime)
    End Function

    Public Function AddResetChannel(digitalinputchannelReset As Kostal.Hal.Core.DigitalInput) As Boolean
        Return AddResetChannel(digitalinputchannelReset, 200)
    End Function

    Public Function AddResetChannel(digitalinputchannelReset As Kostal.Hal.Core.DigitalInput, pollingDelayTime As Integer) As Boolean
        If digitalinputchannelReset Is Nothing Then Return False
        If _dictionaryResetChannels.ContainsKey(digitalinputchannelReset) Then Return True
        _dictionaryResetChannels.Add(digitalinputchannelReset, pollingDelayTime)
        If State = Base.Alarm.AlarmStates.LogicalAlarm Then ActivateResetChannel(digitalinputchannelReset, pollingDelayTime)
        Return True
    End Function

    Private Sub ActivateResetChannels()
        For Each kvpResetChannel As KeyValuePair(Of Kostal.Hal.Core.DigitalInput, Integer) In _dictionaryResetChannels
            ActivateResetChannel(kvpResetChannel.Key, kvpResetChannel.Value)
        Next
    End Sub

    Private Sub ActivateResetChannel(digitalinputchannelReset As Kostal.Hal.Core.DigitalInput, pollingDelayTime As Integer)
        Dim channelReset As Kostal.Hal.Core.Channel = _controllerHardware.Polling.AddChannel(Me, digitalinputchannelReset, pollingDelayTime)
        AddHandler channelReset.ValueChanged, AddressOf ResetChannel_ValueChanged
        ResetChannel_ValueChanged(digitalinputchannelReset)
    End Sub

    Private Sub DeactivateResetChannels()
        For Each kvpResetChannel As KeyValuePair(Of Kostal.Hal.Core.DigitalInput, Integer) In _dictionaryResetChannels
            _controllerHardware.Polling.RemoveChannel(Me, kvpResetChannel.Key)
            RemoveHandler kvpResetChannel.Key.ValueChanged, AddressOf ResetChannel_ValueChanged
        Next
    End Sub

    ''' <summary>
    ''' Called, when the value of a reset channel has changed.
    ''' </summary>
    ''' <param name="channel">The channel at which the value has changed.</param>
    Private Sub ResetChannel_ValueChanged(channel As Hal.Core.Channel)
        Dim digitalinputchannelReset As Kostal.Hal.Core.DigitalInput = TryCast(channel, Kostal.Hal.Core.DigitalInput)
        If digitalinputchannelReset Is Nothing Then Return
        If Not digitalinputchannelReset.StoredValue Then Return
        MyBase.Reset()
    End Sub

#End Region

    Public Sub SetFuncForReset(funcForReset As Func(Of Boolean))
        _funcForReset = funcForReset
    End Sub

    Public Shadows Function ResetInternal() As Boolean
        Return MyBase.ResetInternal()
    End Function

    Protected Overrides Sub ChangeAlarm(newState As Base.Alarm.AlarmStates)
        MyBase.ChangeAlarm(newState)
        Select Case newState
            Case Base.Alarm.AlarmStates.PhysicalAlarm
                _managerApplicationState.SetAlarmInAllStations()
                DeactivateResetChannels()
                ActivateIndicationChannels()
            Case Base.Alarm.AlarmStates.LogicalAlarm
                ActivateResetChannels()
                DeactivateIndicationChannels()
            Case Base.Alarm.AlarmStates.NoAlarm
                DeactivateResetChannels()
                DeactivateIndicationChannels()
                _managerApplicationState.DisableAllStations()
                _managerApplicationState.EnableAllStations()
        End Select
    End Sub

End Class