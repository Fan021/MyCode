Namespace Alarm

    Public Class AlarmIndication
        Implements Alarm.IAlarmIndication

        Private ReadOnly _id As String
        Private ReadOnly _description As String
        Private ReadOnly _raised As System.DateTime = System.DateTime.Now

        Public Sub New(id As String, description As String)
            _id = id
            _description = description
        End Sub

        Public ReadOnly Property Id As String Implements Alarm.IAlarmIndication.Id
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Description As String Implements Alarm.IAlarmIndication.Description
            Get
                Return _description
            End Get
        End Property

        Public ReadOnly Property Raised As Date Implements Alarm.IAlarmIndication.RaisedDateTime
            Get
                Return _raised
            End Get
        End Property

        Public ReadOnly Property TypeId As String Implements Alarm.IAlarmIndication.TypeId
            Get
                Return Alarm.PredefinedAlarmTypes.EmergencyStop.ToString()
            End Get
        End Property

    End Class

End Namespace