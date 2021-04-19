
Public Class StateColorHelper

    Friend Shared Function GetApplicationStateColor(applicationState As Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates) As System.Windows.Media.Brush
        Select Case applicationState
            Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Down
            Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Off]
                Return ColorHelper.MachineStateOffColor
            Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.On
                Return ColorHelper.MachineStateOnColor
            Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Run
                Return ColorHelper.MachineStateRunColor
            Case Kostal.Testman.Framework.Base.ISystemStateManager.SystemStates.Test
                Return ColorHelper.MachineStateTestColor
        End Select
        Return ColorHelper.MachineStateOffColor
        Return Nothing
    End Function

    Friend Shared Function GetStationStateColor(stationState As Kostal.Testman.Framework.Base.IStationStateManager.StationStates) As System.Windows.Media.Brush
        Select Case stationState
            Case Kostal.Testman.Framework.Base.IStationStateManager.StationStates.Off
                Return ColorHelper.MachineStateOffColor
            Case Kostal.Testman.Framework.Base.IStationStateManager.StationStates.On
                Return ColorHelper.MachineStateOnColor
            Case Kostal.Testman.Framework.Base.IStationStateManager.StationStates.Run
                Return ColorHelper.MachineStateRunColor
        End Select
        Return ColorHelper.MachineStateOffColor
        Return Nothing
    End Function

End Class