Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

<clsHMIActionName("CarrierManger", enumHMIActionType.ManualAuto, enumHMISubActionType.SubAction)>
Public Class clsCarrierMangerAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private cMESDataManager As clsMESDataManager
    Private cFailureActionManager As clsFailureActionManager
    Private lListComponentData As New List(Of clsComponentDataCfg)
    Private lListError As New List(Of clsActionResultCfg)
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
            If IsNothing(cMESDataManager) Then
                cMESDataManager = New clsMESDataManager
                cMESDataManager.Init(cSystemElement)
            End If
            If IsNothing(cFailureActionManager) Then
                cFailureActionManager = New clsFailureActionManager
                cFailureActionManager.Init(cSystemElement)
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
        Dim cMachineStatusManager As clsMachineStatusManager = cLocalElement(clsMachineStatusManager.Name)
        Dim cHMICarrierManger As Kochi.HMI.MainControl.Device.CarrierManager.clsCarrierManager = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, lListParameter(0), GetType(Kochi.HMI.MainControl.Device.CarrierManager.clsCarrierManager)).Source
        Dim cLocalVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim cLanguageManager As clsLanguageManager = cSystemElement(clsLanguageManager.Name)
        Dim lListNcData As New List(Of clsNcDataCfg)
        Dim cNcDataCfg As New clsNcDataCfg
        Dim cMESDataCfg As New clsMESDataCfg
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
                        If lListParameter(1) <> enumProcessMethod.UpdateCarrier.ToString Or cMachineStationCfg.CompleteStep <> "2" Then cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                        If lListParameter(1) <> enumProcessMethod.UpdateCarrier.ToString Or cMachineStationCfg.CompleteStep <> "2" Then ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        i.StepInputNumber = i.StepInputNumber + 1
                    Case 1
                        Select Case lListParameter(1)
                            Case enumProcessMethod.ResetCarrier.ToString
                                i.StepInputNumber = 100
                            Case enumProcessMethod.CheckRepeat.ToString
                                i.StepInputNumber = 200
                            Case enumProcessMethod.UpdateCarrier.ToString
                                i.StepInputNumber = 300
                        End Select


                    Case 100
                        If cHMICarrierManger.ResetCarrierID(cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.CarrierID, strResult) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumCarrierMangerErrorType.CarrierMangerError.ToString
                            cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                    Case 200
                        Dim iCnt As Integer = cHMICarrierManger.CheckRepeat(cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.CarrierID, strResult)
                        Select Case iCnt
                            Case 0
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "5", GetMessage(cSubStepCfg))))
                                Return True
                            Case -1
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.Abort = True

                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
                                Return False
                            Case -2
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.Abort = True
                                cActionResultCfg.CancelUpdate = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
                                Return False
                            Case Else
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
                                Return False
                        End Select


                    Case 300
                        If cHMICarrierManger.UpdateCarrier(cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.CarrierID, strResult) Then
                            If lListParameter(1) <> enumProcessMethod.UpdateCarrier.ToString Or cMachineStationCfg.CompleteStep <> "2" Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationCarrierManger", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumCarrierMangerErrorType.CarrierMangerError.ToString
                            cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If


                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumCarrierMangerErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainCarrierMangerErrorType.CarrierMangerError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationCarrierManger", cActionResultCfg.ErrorType, 0)
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

Public Enum enumMainCarrierMangerErrorType
    CarrierMangerError = 1
End Enum

Public Enum enumCarrierMangerErrorType
    CarrierMangerError = 1
    UnKnownError = 2
End Enum
