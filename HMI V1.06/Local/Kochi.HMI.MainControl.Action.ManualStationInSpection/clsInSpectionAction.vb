﻿Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.Statistics.Vision
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIActionName("ManualStationInSpection", enumHMIActionType.Manual, enumHMISubActionType.SubAction)>
Public Class clsInSpectionAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cLanguageManager As clsLanguageManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private cVisionDataManager As clsVisionDataManager
    Private cHMIPLC As clsHMIPLC
    Private cMachineManager As clsMachineManager
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean
        Try
            Me.cLocalElement = cLocalElement
            Me.cSystemElement = cSystemElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMachineStatusManager = CType(cLocalElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cRunnerCfg = cLocalElement(clsRunnerCfg.Name)
            If IsNothing(cVisionDataManager) Then
                cVisionDataManager = New clsVisionDataManager
                cVisionDataManager.Init(cSystemElement)
            End If
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
        Dim cMainStepCfg As clsMainStepCfg = cLocalElement(clsMainStepCfg.Name)
        Dim cActionResultCfg As clsActionResultCfg = cLocalElement(clsActionResultCfg.Name)
        Dim cPLCAction As clsPLCAction = cLocalElement(clsPLCAction.Name)
        Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
        Dim cActionShowManager As clsActionShowManager = cLocalElement(clsActionShowManager.Name)
        Dim cPictureShowManager As clsPictureShowManager = cLocalElement(clsPictureShowManager.Name)
        Dim cMachineStatusCfg As clsMachineStatusCfg = cLocalElement(clsMachineStatusCfg.Name)
        cHMIPLC = cDeviceManager.GetPLCDevice
        bExit = False
        Dim cInSpection As clsHMIInSpection = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIInSpection)).Source

        i.StepInputNumber = i.Address_Home
        Try
            Do While Not bExit
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        cInSpection.DeletePicture()
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 1
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = 100
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = 200
                            Continue Do
                        End If

                    Case 100
                        cInSpection.DeletePicture()
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationInSpection", "6", GetMessage(cSubStepCfg))))
                        Return True

                    Case 200
                        If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 204
                        End If

                    Case 201
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationInSpection", "10", GetMessage(cSubStepCfg))))
                        cInSpection.BackUpPicture(cMachineStatusCfg.VariantCfg.Variant, cMachineStatusCfg.VariantCfg.SFC)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 202
                        If cInSpection.FailPicPath <> "" Then
                            cVisionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusCfg.VariantCfg.Variant, cMachineStatusCfg.VariantCfg.SFC, lListParameter(0), lListParameter(3), cInSpection.FailPicPath, "FAIL", Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 203
                        If cInSpection.FailPicPath <> "" Then
                            cPictureShowManager.ShowPictures(cInSpection.FailPicPath, cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 204
                        cActionResultCfg.Result = False
                        cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationInSpection", "7", GetMessage(cSubStepCfg))
                        If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumInSpectionErrorType.UnKnownError.ToString
                        Else
                            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumInSpectionErrorType.UnKnownError.ToString
                        End If
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationInSpection", cActionResultCfg.ErrorType, 0)

                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationInSpection", "11", GetMessage(cSubStepCfg), ex.Message)
            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
            If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                cActionResultCfg.MainErrorType = enumMainInSpectionErrorType.SensorError.ToString
            Else
                cActionResultCfg.MainErrorType = enumMainInSpectionErrorType.VisionError.ToString
            End If
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationInSpection", cActionResultCfg.ErrorType, 0)
            Return False

        End Try
    End Function

    Private Sub ShowMessageAndPicture(ByVal cPictureShowManager As clsPictureShowManager, ByVal cMainStepCfg As clsMainStepCfg, ByVal cSubStepCfg As clsSubStepCfg)
        Dim lListShowAction As New List(Of clsShowActionCfg)
        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetMessage(cSubStepCfg)))
            cPictureShowManager.ShowPicture(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement), cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType))
        Else
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cMainStepCfg.ActiveDescription(cLocalElement)))
            cPictureShowManager.ShowActions(GetListAction(cMainStepCfg))
        End If

    End Sub

    Private Function GetListAction(ByVal cMainStepCfg As clsMainStepCfg) As List(Of clsShowActionCfg)
        Dim lListShowAction As New List(Of clsShowActionCfg)
        For Each element In cMainStepCfg.GetSubStepListKey
            Dim elementSubCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(element)
            If elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                Continue For
            End If
            If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) = "FALSE" Then
                Dim strTempValue As String = elementSubCfg.ActiveDescription(cLocalElement)
                Dim lListPic As New List(Of clsPictureComponentCfg)
                If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "ManualStationDoAction" Then
                    Dim lParameter As List(Of String) = clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter))
                    If lParameter.Count > 2 Then
                        For j = 1 To lParameter.Count - 1 Step 2
                            lListPic.Add(New clsPictureComponentCfg(lParameter(j), lParameter(j + 1)))
                        Next
                    End If
                End If
                lListShowAction.Add(New clsShowActionCfg(elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), strTempValue, elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement), lListPic))
            End If
        Next
        Return lListShowAction
    End Function

    Private Function GetMessage(ByVal cSubStepCfg As clsSubStepCfg) As String
        Dim strTempValue As String = ""
        Dim strTempComponentValue As String = ""
        strTempValue = cSubStepCfg.ActiveDescription(cLocalElement)
        strTempComponentValue = cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement)

        If strTempComponentValue <> "" Then
            Return strTempComponentValue + " " + strTempValue
        Else
            Return strTempValue
        End If
    End Function

    Public Overrides Function FailRun(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Return True
    End Function

    Public Overrides Function PassRun(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iParameterUI) Then RemoveHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        iParameterUI = New ParameterUI
        AddHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

End Class
Public Enum enumMainInSpectionErrorType
    VisionError = 1
    SensorError = 2
End Enum

Public Enum enumInSpectionErrorType
    VisionError = 1
    SensorError = 2
    UnKnownError = 3
End Enum