Public Class clsHMIException
    Inherits ApplicationException
    Private strStationID As String = String.Empty
    Private eExceptionType As enumExceptionType
    Private _p1 As Object
    Private _enumExceptionType As enumExceptionType


    Public ReadOnly Property Name As String
        Get
            Return strStationID
        End Get
    End Property

    Public ReadOnly Property ExceptionType As enumExceptionType
        Get
            Return eExceptionType
        End Get
    End Property

    Sub New(ByVal strMessage As String, Optional ByVal eExceptionType As enumExceptionType = enumExceptionType.Alarm, Optional ByVal strStationID As String = "")
        MyBase.New(strMessage)
        Me.strStationID = strStationID
        Me.eExceptionType = eExceptionType
    End Sub

    Sub New(ByVal strMessage As String, ByVal cException As Exception, Optional ByVal eExceptionType As enumExceptionType = enumExceptionType.Alarm, Optional ByVal strStationID As String = "")
        MyBase.New(strMessage, cException)
        Me.strStationID = strStationID
        If TypeOf cException Is clsHMIException Then
            Me.eExceptionType = CType(cException, clsHMIException).ExceptionType
        Else
            Me.eExceptionType = eExceptionType
        End If
    End Sub

    Sub New(ByVal cException As Exception, Optional ByVal eExceptionType As enumExceptionType = enumExceptionType.Alarm, Optional ByVal strStationID As String = "")
        MyBase.New(cException.Message, cException)
        Me.strStationID = strStationID
        If TypeOf cException Is clsHMIException Then
            Me.eExceptionType = CType(cException, clsHMIException).ExceptionType
        Else
            Me.eExceptionType = eExceptionType
        End If
    End Sub
End Class

Public Enum enumExceptionType
    Normal = 0
    Confirm
    Warning
    Alarm
    Crash
    PLC
    UnKown
End Enum
