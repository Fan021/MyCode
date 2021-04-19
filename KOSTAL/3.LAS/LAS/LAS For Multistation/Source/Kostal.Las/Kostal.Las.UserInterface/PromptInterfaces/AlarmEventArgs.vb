Public Class AlarmEventArgs
    Inherits EventArgs

    Private _isGeneralAlarmOn As Boolean
    ''' <summary>
    ''' Initializes a new instance of the <see cref="AlarmEventArgs" /> class.
    ''' </summary>
    Public Sub New(isGeneralAlarmOn As Boolean)
        _isGeneralAlarmOn = isGeneralAlarmOn
    End Sub

    public ReadOnly Property IsGeneralAlarmOn As Boolean
        Get
            Return _isGeneralAlarmOn
        End Get
    End Property

End Class