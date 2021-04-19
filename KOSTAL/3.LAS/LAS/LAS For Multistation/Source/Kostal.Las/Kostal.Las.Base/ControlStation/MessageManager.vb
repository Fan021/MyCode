Public Class MessageManager
    Protected _object As New Object
    Protected _listControl As New List(Of String)
    Public Const Name As String = "MessageManager"
    Protected _UpdateMessage As Boolean
    Protected _LockMessage As Boolean
    Public Sub New(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase))
    End Sub


    Public Property UpdateMessage As Boolean
        Set(ByVal value As Boolean)
            _UpdateMessage = value
        End Set
        Get
            Return _UpdateMessage
        End Get
    End Property

    Public Property LockMessage As Boolean
        Set(ByVal value As Boolean)
            _LockMessage = value
        End Set
        Get
            Return _LockMessage
        End Get
    End Property

    Public Function InsertControl(ByVal strMessage As String) As Boolean
        SyncLock _object
            If Not _listControl.Contains(strMessage) Then
                _listControl.Add(strMessage)
            End If
        End SyncLock
        Return True
    End Function
    Public Function RemoveControl(ByVal strMessage As String) As Boolean
        SyncLock _object
            If _listControl.Contains(strMessage) Then
                _listControl.Remove(strMessage)
            End If
        End SyncLock
        Return True
    End Function

    Public Function GetNullStatus() As Boolean
        SyncLock _object
            If _listControl.Count <= 0 Then
                Return True
            Else
                Return False
            End If
        End SyncLock
        Return True
    End Function

End Class
