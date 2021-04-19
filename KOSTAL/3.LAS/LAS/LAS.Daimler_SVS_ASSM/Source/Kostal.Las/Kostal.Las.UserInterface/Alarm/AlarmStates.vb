Namespace Alarm

    ''' <summary>
    ''' States of Alarm
    ''' </summary>
    Public Enum AlarmStates

        ''' <summary>
        ''' There is no alarm
        ''' </summary>
        NoAlarm = 0

        ''' <summary>
        ''' The physical alarm is not present, bu the logical alarm was not resetted
        ''' </summary>
        LogicalAlarm = 1

        ''' <summary>
        ''' The physical alarm indicates an alarm signaled by hardware
        ''' </summary>
        PhysicalAlarm = 2
    End Enum

End Namespace