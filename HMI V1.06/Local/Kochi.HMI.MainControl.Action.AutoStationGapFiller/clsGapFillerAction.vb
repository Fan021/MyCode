Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("AutoStationGapFiller", enumHMIActionType.Auto, enumHMISubActionType.SubAction)>
Public Class clsGapFillerAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private strErrorMsg As String = ""
    Private cLanguageManager As clsLanguageManager
    Private cHMIDeviceBase As clsHMIDeviceBase
    Private lListbillOfMaterialCfg As New List(Of clsbillOfMaterialCfg)
    Private lListComponentData As New List(Of clsComponentDataCfg)

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean
        Try
            Me.cLocalElement = cLocalElement
            Me.cSystemElement = cSystemElement
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMachineStatusManager = CType(cLocalElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
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
        Dim cLocalVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim strLastProgram As String = String.Empty
        bExit = False
        Dim cGapFiller As clsHMIGapFiller = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIGapFiller)).Source
        Dim strDevice As String = cMachineManager.ActionParameterManager.GetActionParameterDevice("AutoStationGapFiller", cMachineStationCfg.ID, 1)
        Dim strDeviceType As String = ""
        Dim strDeviceIndex As String = ""
        Dim strResult As String = ""
        If strDevice <> "" Then
            strDeviceType = strDevice.Split("-")(0)
            strDeviceIndex = strDevice.Split("-")(1)
        End If

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
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 20
                        End If

                    Case 2
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            cHMIDeviceBase = cDeviceCfg.Source
                            i.StepInputNumber = i.StepInputNumber + 3
                        End If

                    Case 3
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "7", GetMessage(cSubStepCfg), strDeviceIndex), enumMainTipsManagerType.Confirm))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 4
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "7", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                        'Pail A
                    Case 5
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "8", GetMessage(cSubStepCfg))))
                        lListbillOfMaterialCfg.Clear()
                        Dim cbillOfMaterialCfg As New clsbillOfMaterialCfg
                        cbillOfMaterialCfg.Item = cGapFiller.PPSstrPackagingNoA
                        lListbillOfMaterialCfg.Add(cbillOfMaterialCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 6
                        If Not CType(cHMIDeviceBase, clsHMIMES).validateBOM(cLocalVariantManager.CurrentVariantCfg.SFC, lListbillOfMaterialCfg, strResult) Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult), enumMainTipsManagerType.Confirm))
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = i.StepInputNumber + 2
                        End If

                    Case 7
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 5
                        End If
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                        'Pail B
                    Case 8
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "9", GetMessage(cSubStepCfg))))
                        lListbillOfMaterialCfg.Clear()
                        Dim cbillOfMaterialCfg As New clsbillOfMaterialCfg
                        cbillOfMaterialCfg.Item = cGapFiller.PPSstrPackagingNoB
                        lListbillOfMaterialCfg.Add(cbillOfMaterialCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 9
                        If Not CType(cHMIDeviceBase, clsHMIMES).validateBOM(cLocalVariantManager.CurrentVariantCfg.SFC, lListbillOfMaterialCfg, strResult) Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult), enumMainTipsManagerType.Confirm))
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = i.StepInputNumber + 2
                        End If

                    Case 10
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 8
                        End If
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                        'Pail A
                    Case 11
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "10", GetMessage(cSubStepCfg))))
                        lListComponentData.Clear()
                        Dim mTemp As String = cGapFiller.PPSstrPartNoA
                        If mTemp.IndexOf("-") < 0 Then
                            mTemp = mTemp + "-"
                        End If
                        Dim cComponentDataCfg As New clsComponentDataCfg
                        cComponentDataCfg.MaterialId = mTemp.Split("-")(0)
                        cComponentDataCfg.MaterialRevision = mTemp.Split("-")(1)
                        cComponentDataCfg.Inventory = cGapFiller.PPSstrPackagingNoA + "-" + cGapFiller.PPSstrSupplierNoA
                        cComponentDataCfg.Quantity = 1
                        lListComponentData.Add(cComponentDataCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 12
                        If Not CType(cHMIDeviceBase, clsHMIMES).Assemble(cLocalVariantManager.CurrentVariantCfg.SFC, 1, lListComponentData, strResult) Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult), enumMainTipsManagerType.Confirm))
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = i.StepInputNumber + 2
                        End If

                    Case 13
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 11
                        End If
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                        'Pail B
                    Case 14
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "11", GetMessage(cSubStepCfg))))
                        lListComponentData.Clear()
                        Dim mTemp As String = cGapFiller.PPSstrPartNoB
                        If mTemp.IndexOf("-") < 0 Then
                            mTemp = mTemp + "-"
                        End If
                        Dim cComponentDataCfg As New clsComponentDataCfg
                        cComponentDataCfg.MaterialId = mTemp.Split("-")(0)
                        cComponentDataCfg.MaterialRevision = mTemp.Split("-")(1)
                        cComponentDataCfg.Inventory = cGapFiller.PPSstrPackagingNoB + "-" + cGapFiller.PPSstrSupplierNoB
                        cComponentDataCfg.Quantity = 1
                        lListComponentData.Add(cComponentDataCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 15
                        If Not CType(cHMIDeviceBase, clsHMIMES).Assemble(cLocalVariantManager.CurrentVariantCfg.SFC, 1, lListComponentData, strResult) Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult), enumMainTipsManagerType.Confirm))
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 20
                        End If

                    Case 16
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 14
                        End If
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "12", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If

                    Case 20
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 21
                        If cGapFiller.LastProgram <> strLastProgram Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cGapFiller.LastProgram, enumMainTipsManagerType.Normal, 20))
                            strLastProgram = cGapFiller.LastProgram
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
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "5", GetMessage(cSubStepCfg))))
                        Return True

                    Case 200
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationGapFiller", "6", GetMessage(cSubStepCfg))))
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 201
                        cActionResultCfg.Result = False
                        cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationGapFiller", "6", GetMessage(cSubStepCfg))
                        cActionResultCfg.ErrorType = enumGapFillerErrorType.GapFillerError.ToString
                        cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumGapFillerErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainGapFillerErrorType.GapFillerError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationGapFiller", cActionResultCfg.ErrorType, 0)
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

Public Enum enumMainGapFillerErrorType
    GapFillerError = 1
End Enum

Public Enum enumGapFillerErrorType
    GapFillerError = 1
    UnKnownError = 2
End Enum
