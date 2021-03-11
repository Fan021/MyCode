Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.Statistics.Vision
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("ManualConfirmInSpection", enumHMIActionType.Manual, enumHMISubActionType.SubAction)>
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
    Private OldListParmeter As New List(Of Object)

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
        Dim bTimeOut As Boolean = False
        Dim strTimeOut As String = String.Empty
        i.StepInputNumber = i.Address_Home

        Try
            Do While Not bExit
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        Dim strTempValue As String = cSubStepCfg.ActiveDescription(cLocalElement)
                        For j = 1 To 20
                            strTempValue = strTempValue.ToString.Replace("$" + j.ToString, "")
                        Next
                        cInSpection.DeletePicture()
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, strTempValue)
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetPlcMessage(cSubStepCfg, ChangeValue(cSubStepCfg, cPLCAction)), enumMainTipsManagerType.Normal))
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 1
                        If GetPlcValueChange(cPLCAction) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetPlcMessage(cSubStepCfg, ChangeValue(cSubStepCfg, cPLCAction)), enumMainTipsManagerType.NormalAndNoLog))
                        End If
                        If cPLCAction.HmiAction.bulPLCDoAction Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = 100
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = 201
                            Continue Do
                        End If

                    Case 2
                        If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 5
                        End If

                    Case 3
                        ' cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "10", GetMessage(cSubStepCfg))))
                        Try
                            cInSpection.BackUpPicture(cMachineStatusCfg.VariantCfg.Variant, cMachineStatusCfg.VariantCfg.SFC)
                            i.StepInputNumber = i.StepInputNumber + 1
                        Catch ex As Exception
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "11", GetMessage(cPLCAction, cSubStepCfg), ex.Message), enumMainTipsManagerType.Alarm))
                            cPLCAction.DoReWorkAction(cHMIPLC, cMachineStationCfg.ID, True)
                            strTimeOut = ex.Message
                            bTimeOut = True
                            i.StepInputNumber = i.StepInputNumber + 2
                        End Try


                    Case 4
                        If cInSpection.FailPicPath <> "" Then
                            cPictureShowManager.ShowCurrentPictures(cInSpection.FailPicPath, cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
                        End If
                        ' cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "12", GetMessage(cSubStepCfg))))
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 5
                        If GetPlcValueChange(cPLCAction) And Not bTimeOut Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetPlcMessage(cSubStepCfg, ChangeValue(cSubStepCfg, cPLCAction)), enumMainTipsManagerType.NormalAndNoLog))
                        End If
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = 100
                            Continue Do
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                                i.StepInputNumber = 200
                            Else
                                i.StepInputNumber = 201
                            End If
                            Continue Do
                        End If

                    Case 100
                        If Not cPictureShowManager.IsRunning Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 101
                        If cInSpection.FailPicPath <> "" Then
                            System.IO.File.Delete(cInSpection.FailPicPath)
                        End If
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "6", GetMessage(cPLCAction, cSubStepCfg))))
                        Return True

                    Case 200
                        If cInSpection.FailPicPath <> "" And Not bTimeOut Then
                            cPictureShowManager.ShowPictures(cInSpection.FailPicPath, cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Picture, cLocalElement))
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 201
                        cActionResultCfg.Result = False
                        If bTimeOut Then
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "11", GetMessage(cPLCAction, cSubStepCfg), strTimeOut)
                        Else
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "7", GetMessage(cPLCAction, cSubStepCfg))
                        End If

                        If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumInSpectionErrorType.UnKnownError.ToString
                        Else
                            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumInSpectionErrorType.UnKnownError.ToString
                        End If
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualConfirmInSpection", cActionResultCfg.ErrorType, 0)

                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualConfirmInSpection", "11", GetMessage(cPLCAction, cSubStepCfg), ex.Message)
            cActionResultCfg.ErrorType = enumInSpectionErrorType.UnKnownError.ToString
            If lListParameter(1) = enumInSpectionType.Vision.ToString Then
                cActionResultCfg.MainErrorType = enumMainInSpectionErrorType.SensorError.ToString
            Else
                cActionResultCfg.MainErrorType = enumMainInSpectionErrorType.VisionError.ToString
            End If
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualConfirmInSpection", cActionResultCfg.ErrorType, 0)
            Return False

        End Try
    End Function

    Private Function GetPlcValueChange(ByVal cPLCAction As clsPLCAction) As Boolean
        For j = 1 To cPLCAction.ListParmeter.Count - 1
            Dim strPLCValue As String = cPLCAction.ListParmeter(j - 1)
            If OldListParmeter.Count >= j Then
                If OldListParmeter(j - 1) <> strPLCValue Then
                    OldListParmeter(j - 1) = strPLCValue
                    Return True
                End If
            Else
                OldListParmeter.Add(strPLCValue)
                If strPLCValue <> "" Then Return True
            End If
        Next
        Return False
    End Function

    Private Function GetPlcMessage(ByVal cSubStepCfg As clsSubStepCfg, ByVal strDescription As String) As String
        Dim strTempValue As String = ""
        Dim strTempComponentValue As String = ""
        strTempValue = strDescription
        strTempComponentValue = cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement)

        If strTempComponentValue <> "" Then
            Return strTempComponentValue + " " + strTempValue
        Else
            Return strTempValue
        End If
    End Function

    Private Function ChangeValue(ByVal cSubStepCfg As clsSubStepCfg, ByVal cPLCAction As clsPLCAction) As String
        Dim strTempValue As String = ""
        Dim strTempComponentValue As String = ""
        strTempValue = cSubStepCfg.ActiveDescription(cLocalElement)
        If strTempValue.IndexOf("$") >= 0 Then
            For j = 1 To 20
                If cPLCAction.ListParmeter.Count >= j Then
                    strTempValue = strTempValue.Replace("$" + j.ToString, cLanguageManager.GetChangeTextLine("Change.Ini", "ManualConfirmInSpection", cPLCAction.ListParmeter(j - 1)))
                Else
                    strTempValue = strTempValue.Replace("$" + j.ToString, "")
                End If
            Next
        End If
        Return strTempValue
    End Function

    Private Sub ShowMessageAndPicture(ByVal cPictureShowManager As clsPictureShowManager, ByVal cMainStepCfg As clsMainStepCfg, ByVal cSubStepCfg As clsSubStepCfg)
        Dim lListShowAction As New List(Of clsShowActionCfg)
        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
            '  cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetMessage(cSubStepCfg)))
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

    Private Function GetMessage(ByVal cPLCAction As clsPLCAction, ByVal cSubStepCfg As clsSubStepCfg) As String
        Dim strTempValue As String = ""
        Dim strTempComponentValue As String = ""
        strTempValue = cSubStepCfg.ActiveDescription(cLocalElement)
        strTempComponentValue = cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement)
        If strTempValue.IndexOf("$") >= 0 Then
            For j = 1 To 20
                If cPLCAction.ListParmeter.Count >= j Then
                    strTempValue = strTempValue.Replace("$" + j.ToString, cLanguageManager.GetChangeTextLine("Change.Ini", "ManualConfirmInSpection", cPLCAction.ListParmeter(j - 1)))
                Else
                    strTempValue = strTempValue.Replace("$" + j.ToString, "")
                End If
            Next
        End If
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
