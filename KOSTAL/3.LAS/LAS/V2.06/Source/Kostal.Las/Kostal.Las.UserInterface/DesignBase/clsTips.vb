Imports Kostal.Las.Base
Public Class clsTips
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Public Const Name As String = "Tips"
    Public Active As Boolean
    Public _lasErrorMessageSet As structErrorMessageSet
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = Devices
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
    Public Function AddTips(ByVal strMessage As String) As Boolean
        If _lasErrorMessageSet Is Nothing Then
            _lasErrorMessageSet = New structErrorMessageSet
        End If

        _lasErrorMessageSet.Clear()
        _lasErrorMessageSet.iKeyUser = 0
        _lasErrorMessageSet.iErrorCode = 999
        _lasErrorMessageSet.strErrorSource = "LAS"
        _lasErrorMessageSet.strErrorType = enumHMI_ERROR_TYPE.MasterError.ToString
        _lasErrorMessageSet.strErrorValue = ""
        _lasErrorMessageSet.strErrorTitle = ""
        _lasErrorMessageSet.strErrorMessage = strMessage
        _lasErrorMessageSet.strRaisedTime = Date.Now.ToString("HH:mm:ss")
        Active = True
        Return True
    End Function
End Class
