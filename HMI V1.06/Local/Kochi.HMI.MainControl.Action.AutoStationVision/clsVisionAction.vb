Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent


<clsHMIActionName("AutoStationVision", enumHMIActionType.Auto)>
Public Class clsVisionAction
    Inherits clsHMIActionBase

    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean
        Try
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMachineStatusManager = CType(cLocalElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cRunnerCfg = cLocalElement(clsRunnerCfg.Name)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        bExit = True
        Dispose()
        Return True
    End Function

    Public Overrides Function CreateActionUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iActionUI) Then RemoveHandler CType(iActionUI, ActionUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        iActionUI = New ActionUI
        AddHandler CType(iActionUI, ActionUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function Abort(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        bExit = True
        Return True
    End Function

    Public Overrides Function Run(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Dim cSubStepCfg As clsSubStepCfg = cLocalElement(clsSubStepCfg.Name)
        Dim cActionResultCfg As clsActionResultCfg = cLocalElement(clsActionResultCfg.Name)
        Dim cPLCAction As clsPLCAction = cLocalElement(clsPLCAction.Name)
        Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
        Dim cActionShowManager As clsActionShowManager = cLocalElement(clsActionShowManager.Name)
        Dim cPictureShowManager As clsPictureShowManager = cLocalElement(clsPictureShowManager.Name)
        Dim cMachineStatusCfg As clsMachineStatusCfg = cLocalElement(clsMachineStatusCfg.Name)

        Dim mStartTime As String = String.Empty
        Dim cAST As clsHMIAST = cDeviceManager.GetDeviceFromName(lListParameter(1)).Source
        bExit = False

        i.StepInputNumber = i.Address_Home
        Try
            Do While Not bExit
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        mStartTime = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        'cScrewDataManager.InSertData(cMachineStationCfg.StationName ,
                        '                             cMachineStatusCfg.VariantCfg .SFC ,
                        '                             cMachineStatusCfg.VariantCfg .Variant ,
                        '                             cSubStepCfg.SubStepParameter (HMISubStepKeys .Component ),
                        '                             lListParameter(1),
                        '                             lListParameter(2),
                        '                             cSubStepCfg.SubStepParameter (HMISubStepKeys .ID ),
                        '                             cAST.GetStepValue .ToString,
                        '                             cAST.GetTorqueValue ,
                        '                             cAST.GetTorqueValue ,
                        '                             cAST.GetTorqueValue ,
                        '                             cAST.GetAngleValue ,
                        '                             cAST.GetAngleValue ,
                        '                             cAST.GetAngleValue ,
                        '                             cAST.GetAngleValue 

                        i.StepInputNumber = i.StepInputNumber + 1



                    Case 1
                        If Not IsNothing(cDeviceManager.GetDeviceFromName(lListParameter(1)).Source) AndAlso TypeOf cDeviceManager.GetCurrentDeviceFromName(lListParameter(1)).Source Is clsHMIManualScanner Then

                        End If
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            i.StepInputNumber = 100
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            i.StepInputNumber = 200
                        End If

                    Case 100

                        Return True

                    Case 200
                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Overrides Function FailRun(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Return True
    End Function

    Public Overrides Function PassRun(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class
