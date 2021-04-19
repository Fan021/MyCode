Namespace Alarm

    ''' <summary>
    ''' Defines a physical alarm
    ''' </summary>
    Public Interface IAlarmIndication

        ''' <summary>
        ''' Gets the identifier.
        ''' </summary>
        ''' <value>
        ''' The identifier.
        ''' </value>
        ReadOnly Property Id As String

        ''' <summary>
        ''' Gets the type of the alarm.
        ''' </summary>
        ''' <value>
        ''' The type.
        ''' </value>
        ReadOnly Property TypeId As String

        ''' <summary>
        ''' Gets the description.
        ''' </summary>
        ''' <value>
        ''' The description.
        ''' </value>
        ReadOnly Property Description As String

        ''' <summary>
        ''' Gets the date time of the raising of this alarm.
        ''' </summary>
        ''' <value>
        ''' The raised date time.
        ''' </value>
        ReadOnly Property RaisedDateTime As System.DateTime

    End Interface

End Namespace