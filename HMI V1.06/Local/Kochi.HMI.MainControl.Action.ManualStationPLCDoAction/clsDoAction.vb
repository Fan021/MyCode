Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent


<clsHMIActionName("ManualStationPlcDoAction", enumHMIActionType.Manual, enumHMISubActionType.All)>
Public Class clsDoAction
    Inherits clsHMIActionBase
    Private cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
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
        Dim cHMIPLC As clsHMIPLC = cDeviceManager.GetPLCDevice
        OldListParmeter.Clear()
        bExit = False

        i.StepInputNumber = i.Address_Home
        Try
            Do While Not bExit
                If lListParameter(lListParameter.Count - 1).ToUpper = "TRUE" Then
                    If cActionResultCfg.SuccessNum >= 1 Then
                        Return True
                    End If
                End If
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
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, strTempValue)
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        i.StepInputNumber = i.StepInputNumber + 1
                    Case 1
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetPlcMessage(cSubStepCfg, ChangeValue(cSubStepCfg, cPLCAction)), enumMainTipsManagerType.Normal))
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 2
                        If GetPlcValueChange(cPLCAction) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, GetPlcMessage(cSubStepCfg, ChangeValue(cSubStepCfg, cPLCAction)), enumMainTipsManagerType.NormalAndNoLog))
                        End If
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
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "3", GetMessage(cPLCAction, cSubStepCfg))))
                        Return True

                    Case 200
                        cActionResultCfg.Result = False
                        cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "4", GetMessage(cPLCAction, cSubStepCfg))
                        cActionResultCfg.ErrorType = enumPlcDoActionErrorType.PlcDoActionError.ToString
                        cActionResultCfg.MainErrorType = enumMainPlcDoActionErrorType.PlcDoActionError.ToString
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationPlcDoAction", cActionResultCfg.ErrorType, 0)
                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumPlcDoActionErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainPlcDoActionErrorType.PlcDoActionError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationPlcDoAction", cActionResultCfg.ErrorType, 0)
            Return False
        End Try
        Return True
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

    Private Function ChangeValue(ByVal cSubStepCfg As clsSubStepCfg, ByVal cPLCAction As clsPLCAction) As String
        Dim strTempValue As String = ""
        Dim strTempComponentValue As String = ""
        strTempValue = cSubStepCfg.ActiveDescription(cLocalElement)
        If strTempValue.IndexOf("$") >= 0 Then
            For j = 1 To 20
                If cPLCAction.ListParmeter.Count >= j Then
                    strTempValue = strTempValue.Replace("$" + j.ToString, cLanguageManager.GetChangeTextLine("Change.Ini", "ManualStationPlcDoAction", cPLCAction.ListParmeter(j - 1)))
                Else
                    strTempValue = strTempValue.Replace("$" + j.ToString, "")
                End If
            Next
        End If
        Return strTempValue
    End Function

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
                    strTempValue = strTempValue.Replace("$" + j.ToString, cLanguageManager.GetChangeTextLine("Change.Ini", "ManualStationPlcDoAction", cPLCAction.ListParmeter(j - 1)))
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

Public Enum enumMainPlcDoActionErrorType
    PlcDoActionError = 1
End Enum

Public Enum enumPlcDoActionErrorType
    PlcDoActionError = 1
    UnKnownError = 2
End Enum