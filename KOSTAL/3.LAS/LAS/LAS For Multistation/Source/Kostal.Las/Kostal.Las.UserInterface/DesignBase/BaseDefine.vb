
Public Enum enumPLC_AUTO_MANUAL
    None = 0
    Auto
    Manual
End Enum


Public Enum enumPLC_Status
    Off = 0
    [On]
    Calibrated
    [Stop]
    Start
    Run
    Ready
    LastCycle
End Enum


'Public Enum enumHMI_ERROR_TYPE
'    None = 0
'    Message
'    MasterMessage
'    [Error]
'    MasterError
'End Enum

Public Interface IUserInterface

    Event UserCancelled(sender As Object, e As LasViewEventArgs)

End Interface

Public Interface IViewDefine

    ReadOnly Property GetPannel As Panel

End Interface


Public Class BaseDefine

End Class
