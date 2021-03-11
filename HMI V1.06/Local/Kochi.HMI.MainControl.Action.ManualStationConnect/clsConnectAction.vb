Imports System.Windows.Forms

Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports System.Threading
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("ManualStationConnect", enumHMIActionType.Manual, enumHMISubActionType.SubAction)>
Public Class clsConnectAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private strComment As String = String.Empty
    Private strErrorType As String = String.Empty
    Private cHMIDeviceBase As clsHMIDeviceBase
    Private cRunActionCfg As New clsRunActionCfg
    Private cThread As Thread
    Private cPKS As clsHMIPKS
    Private interPass As Boolean = False
    Private interFail As Boolean = False
    Private cActionDataManager As clsActionDataManager

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
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Dispose()
        bExit = True
        Return True
    End Function

    Public Overrides Function CreateActionUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iActionUI) Then
            RemoveHandler CType(iActionUI, ActionUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iActionUI.Quit(cLocalElement, cSystemElement)
        End If
        iActionUI = New ActionUI
        AddHandler CType(iActionUI, ActionUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iParameterUI) Then RemoveHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        iParameterUI = New ParameterUI
        AddHandler CType(iParameterUI, ParameterUI).ParameterChanged, AddressOf Parameter_ParameterChanged
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
        Dim cVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim cHMIPLC As clsHMIPLC = cDeviceManager.GetPLCDevice
        cPKS = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(1), GetType(clsHMIPKS)).Source
        Dim strDevice As String = cMachineManager.ActionParameterManager.GetActionParameterDevice("ManualStationConnect", cMachineStationCfg.ID, 1)
        Dim strDeviceType As String = ""
        Dim strDeviceIndex As String = ""

        If strDevice <> "" Then
            strDeviceType = strDevice.Split("-")(0)
            strDeviceIndex = strDevice.Split("-")(1)
        End If
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
                        interPass = False
                        interFail = False
                        If Not cRunActionCfg.IsRunning Then
                            cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If
                    Case 1
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationConnect", "30", GetMessage(cSubStepCfg))))
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 2
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Waiting)
                        If cPLCAction.HmiAction.bulPLCDoAction Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationConnect", "31", GetMessage(cSubStepCfg))))
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                    Case 3
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Ongoing)
                        If Not cPLCAction.HmiAction.bulPLCDoAction Then
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                    Case 4
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Ongoing)
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            interPass = True
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            interFail = True
                            i.StepInputNumber = i.StepInputNumber + 1
                            Continue Do
                        End If

                    Case 5
                        If interPass Then
                            i.StepInputNumber = 100
                        End If

                        If interFail Then
                            i.StepInputNumber = 200
                        End If


                    Case 100
                        cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Pass)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 101
                        i.StepInputNumber = i.StepInputNumber + 1
                    Case 102
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationConnect", "32", GetMessage(cSubStepCfg))))
                        Return True

                    Case 200
                        cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 201
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 202
                        cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationConnect", "34", GetMessage(cSubStepCfg), (Single.Parse(lListParameter(6)) - Single.Parse(lListParameter(7))).ToString, cPKS.GetZValue.ToString, (Single.Parse(lListParameter(6)) + Single.Parse(lListParameter(7))).ToString)
                        cActionResultCfg.ErrorType = enumConnectErrorType.ConnectInspectionError
                        cActionResultCfg.MainErrorType = enumMainConnectErrorType.ConnectInspectionError.ToString
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationConnect", strErrorType, 0)
                        cActionResultCfg.Result = False
                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumConnectErrorType.UnKnownError
            cActionResultCfg.MainErrorType = enumMainConnectErrorType.ConnectInspectionError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationConnect", cActionResultCfg.ErrorType, 0)
            Return False
        End Try
    End Function


    Private Sub ShowMessageAndPicture(ByVal cPictureShowManager As clsPictureShowManager, ByVal cMainStepCfg As clsMainStepCfg, ByVal cSubStepCfg As clsSubStepCfg)
        Dim lListShowAction As New List(Of clsShowActionCfg)
        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetMessage(cSubStepCfg)))
            cPictureShowManager.ShowPicture(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement), cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), True, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))
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

End Class



Public Enum enumMainConnectErrorType
    ConnectInspectionError = 1
End Enum

Public Enum enumConnectErrorType
    ConnectInspectionError = 1
    UnKnownError = 2
End Enum
