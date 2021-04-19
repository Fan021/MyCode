Imports Kostal.Testman.Plugin.Base

''' <summary>
''' Contains all available alarm adapters.
''' Singleton class.
''' </summary>
''' <remarks></remarks>
Public Class AlarmContainer

    Private _alarmGeneral As New AlarmAdapter
    'Private _pass As New AlarmAdapter
    'Private _fail As New AlarmAdapter
    Private _alarmEmergencyStop As New AlarmAdapter
    Private _alarmSafetyProtection As New AlarmAdapter
    'Private _retest As New AlarmAdapter
    'Private _release As New AlarmAdapter
    Dim _resetEnabled As Boolean = False

    ''' <summary>
    ''' Initializes a new instance of the <see cref="AlarmContainer" /> class.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Gets the first alarm channel that is turned on.
    ''' </summary>
    ''' <returns>first enabled channel as <see cref="AlarmChannelType"/>. If none is enabled <c>nothing/null</c> is returned.</returns>
    Public ReadOnly Property EnabledAlert() As AlarmChannelType
        Get
            If SafetyProtection.IsOn Then Return AlarmChannelType.SafetyProtection
            If EmergencyStop.IsOn Then Return AlarmChannelType.EmergencyStop
            'If Release.IsOn Then Return AlarmChannelType.Release
            'If Retest.IsOn Then Return AlarmChannelType.Retest
            'If Fail.IsOn Then Return AlarmChannelType.Fail
            'If Pass.IsOn Then Return AlarmChannelType.Pass
            If GeneralAlarm.IsOn Then Return AlarmChannelType.GeneralAlarm
            Return Nothing
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the adapter for general alarm.
    ''' </summary>
    ''' <value>
    ''' The general alarm adapter.
    ''' </value>
    Public Property GeneralAlarm As AlarmAdapter
        Get
            Return _alarmGeneral
        End Get
        Set(ByVal value As AlarmAdapter)
            _alarmGeneral = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the Enable flag to reset the Alarm.
    ''' </summary>
    ''' <value>
    ''' The enable flag to reset the Alarm 
    ''' </value>   
    Public Property ResetEnabled As Boolean
        Get
            Return _resetEnabled
        End Get
        Set(value As Boolean)
            '           GeneralAlarm.Reset()
            _resetEnabled = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' Gets or sets the adapter for pass alarm.
    ' ''' </summary>
    ' ''' <value>
    ' ''' The pass alarm adapter.
    ' ''' </value>
    'Public Property Pass As AlarmAdapter
    '    Get
    '        Return _pass
    '    End Get
    '    Set(ByVal value As AlarmAdapter)
    '        _pass = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Gets or sets the adapter for fail alarm.
    ' ''' </summary>
    ' ''' <value>
    ' ''' The fail alarm adapter.
    ' ''' </value>
    'Public Property Fail As AlarmAdapter
    '    Get
    '        Return _fail
    '    End Get
    '    Set(ByVal value As AlarmAdapter)
    '        _fail = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Gets or sets the adapter for emergency stop alarm.
    ''' </summary>
    ''' <value>
    ''' The emergency stop alarm adapter.
    ''' </value>
    Public Property EmergencyStop As AlarmAdapter
        Get
            Return _alarmEmergencyStop
        End Get
        Set(ByVal value As AlarmAdapter)
            _alarmEmergencyStop = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the adapter for safety protection alarm.
    ''' </summary>
    ''' <value>
    ''' The safety protection alarm adapter.
    ''' </value>
    Public Property SafetyProtection As AlarmAdapter
        Get
            Return _alarmSafetyProtection
        End Get
        Set(ByVal value As AlarmAdapter)
            _alarmSafetyProtection = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' Gets or sets the adapter for retest alarm.
    ' ''' </summary>
    ' ''' <value>
    ' ''' The retest alarm adapter.
    ' ''' </value>
    'Public Property Retest As AlarmAdapter
    '    Get
    '        Return _retest
    '    End Get
    '    Set(ByVal value As AlarmAdapter)
    '        _retest = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Gets or sets the adapter for release alarm.
    ' ''' </summary>
    ' ''' <value>
    ' ''' The release alarm adapter.
    ' ''' </value>
    'Public Property Release As AlarmAdapter
    '    Get
    '        Return _release
    '    End Get
    '    Set(ByVal value As AlarmAdapter)
    '        _release = value
    '    End Set
    'End Property

End Class