Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("LineControl", enumHMIActionType.ManualAuto, enumHMISubActionType.SubAction)>
Public Class clsLineControlAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean
        Try
            Me.cLocalElement = cLocalElement
            Me.cSystemElement = cSystemElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMachineStatusManager = CType(cLocalElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
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
        Dim cMainStepCfg As clsMainStepCfg = cLocalElement(clsMainStepCfg.Name)
        Dim cActionResultCfg As clsActionResultCfg = cLocalElement(clsActionResultCfg.Name)
        Dim cPLCAction As clsPLCAction = cLocalElement(clsPLCAction.Name)
        Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
        Dim cActionShowManager As clsActionShowManager = cLocalElement(clsActionShowManager.Name)
        Dim cPictureShowManager As clsPictureShowManager = cLocalElement(clsPictureShowManager.Name)
        Dim cMachineStatusCfg As clsMachineStatusCfg = cLocalElement(clsMachineStatusCfg.Name)
        Dim cLocalVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim cHMILineControl As clsHMILineControl = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, lListParameter(0), GetType(clsHMILineControl)).Source
        Dim cLanguageManager As clsLanguageManager = cSystemElement(clsLanguageManager.Name)
        bExit = False
        Dim strResult As String = ""

        i.StepInputNumber = i.Address_Home
        Try
            Do While Not bExit
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 1
                        Select Case lListParameter(1)
                            Case enumLineControlMethod.Start.ToString
                                i.StepInputNumber = 100
                            Case enumLineControlMethod.Complete.ToString
                                i.StepInputNumber = 200
                            Case enumLineControlMethod.StartWithConfirm.ToString
                                i.StepInputNumber = 300
                        End Select

                    Case 100
                        If Not cHMILineControl.Running Then
                            cHMILineControl.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 101
                        Dim iResultErrorId As Integer = 0
                        If cHMILineControl.Start(cLocalVariantManager.CurrentVariantCfg.SFC, cLocalVariantManager.CurrentVariantCfg.Variant, strResult, iResultErrorId) Then
                            cHMILineControl.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationLineControl", "6", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            If iResultErrorId = -6 Then
                                cMachineStatusManager.SetJump(cRunnerCfg.StationName, True)
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.ErrorMessage = strResult
                                cHMILineControl.Running = False
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationLineControl", "6", GetMessage(cSubStepCfg))))
                                Return True
                            End If
                            If TypeOf cHMILineControl Is Kochi.HMI.MainControl.Device.StationCheck.clsStationCheck Then
                                cActionResultCfg.Abort = True
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = strResult
                                cActionResultCfg.ErrorType = enumLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
                                cHMILineControl.Running = False
                                Return False
                            Else
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationLineControl", "4", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
                                cHMILineControl.Running = False
                                Return False
                            End If

                        End If


                    Case 200
                        If Not cHMILineControl.Running Then
                            cHMILineControl.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 201
                        If cHMILineControl.Complete(cLocalVariantManager.CurrentVariantCfg.SFC, cLocalVariantManager.CurrentVariantCfg.Variant, cActionResultCfg.Result, strResult) Then
                            cHMILineControl.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationLineControl", "7", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationLineControl", "5", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumLineControlErrorType.LineControlError.ToString
                            cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
                            cHMILineControl.Running = False
                            Return False
                        End If


                    Case 300
                        If Not cHMILineControl.Running Then
                            cHMILineControl.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 301
                        Dim iResultErrorId As Integer = 0
                        If cHMILineControl.Start(cLocalVariantManager.CurrentVariantCfg.SFC, cLocalVariantManager.CurrentVariantCfg.Variant, strResult, iResultErrorId) Then
                            cHMILineControl.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationLineControl", "6", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            If iResultErrorId = -6 Then
                                cMachineStatusManager.SetJump(cRunnerCfg.StationName, True)
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.ErrorMessage = strResult
                                cHMILineControl.Running = False
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationLineControl", "6", GetMessage(cSubStepCfg))))
                                Return True
                            End If
                            If TypeOf cHMILineControl Is Kochi.HMI.MainControl.Device.StationCheck.clsStationCheck Then
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = strResult
                                cActionResultCfg.ErrorType = enumLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
                                cHMILineControl.Running = False
                                Return False
                            Else
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationLineControl", "4", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
                                cHMILineControl.Running = False
                                Return False
                            End If

                        End If

                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumLineControlErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainLineControlErrorType.LineControlError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationLineControl", cActionResultCfg.ErrorType, 0)
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

Public Enum enumMainLineControlErrorType
    LineControlError = 1
End Enum

Public Enum enumLineControlErrorType
    LineControlError = 1
    UnKnownError = 2
End Enum

