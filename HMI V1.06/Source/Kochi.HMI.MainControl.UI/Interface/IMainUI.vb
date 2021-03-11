Public Interface IMainUI
    Function RaiseHMIExceptionEvent(ByVal eException As Exception)
    Function SetCycleTime(ByVal dTime As Double) As Boolean
    Function SetPLCStatus(ByVal eDeviceStatus As enumDeviceStatus) As Boolean
    Function SetMESStatus(ByVal eDeviceStatus As enumDeviceStatus) As Boolean
    Function SetProcessSStatus(ByVal eDeviceStatus As enumDeviceStatus) As Boolean
    Function SetStationStep(ByVal strStationID As String, ByVal iStep As Integer) As Boolean
    Function AddStation(ByVal strStationID As String) As Boolean
    Function RemoveStation(ByVal strStationID As String) As Boolean
    Sub EnableMainLeftButton()
    Sub DisableMainLeftButton()
    Sub AutoClose()
    Function InvokeAction(ByVal method As System.Delegate, ByVal ParamArray args() As Object) As Object

End Interface

Public Enum enumDeviceStatus
    Normal
    Open
    Close
    NG
End Enum

Public Enum enumPageMode
    [ReadOnly] = 6
    Debug = 7
    Edit = 8
End Enum
