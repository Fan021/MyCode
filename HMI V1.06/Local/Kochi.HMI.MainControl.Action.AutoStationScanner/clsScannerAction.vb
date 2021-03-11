Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UserDefine
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("AutoStationScan", enumHMIActionType.Auto, enumHMISubActionType.All)>
Public Class clsScannerAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private cScanResult As New clsScanResult
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private strErrorMsg As String = ""
    Private cLanguageManager As clsLanguageManager
    Private strScanResult As String = String.Empty
    Private cBarcodeManager As clsBarcodeManager
    Private cMESDataManager As clsMESDataManager
    Private cHMIDeviceBase As clsHMIDeviceBase
    Private lListbillOfMaterialCfg As New List(Of clsbillOfMaterialCfg)
    Private lListComponentData As New List(Of clsComponentDataCfg)
    Private cHMIScanner As clsHMIScanner
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
            If IsNothing(cMESDataManager) Then
                cMESDataManager = New clsMESDataManager
                cMESDataManager.Init(cSystemElement)
            End If
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        bExit = True
        If Not IsNothing(cHMIScanner) Then
            cHMIScanner.StopReceive()
        End If
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
        cHMIScanner = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, lListParameter(0), GetType(clsHMIManualScanner), GetType(clsHMIAutoScanner)).Source
        Dim cHMIPLC As clsHMIPLC = cDeviceManager.GetPLCDevice
        Dim cLocalVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim cMachineStatusManager As clsMachineStatusManager = cLocalElement(clsMachineStatusManager.Name)
        Dim strDevice As String = cMachineManager.ActionParameterManager.GetActionParameterDevice("AutoStationScan", cMachineStationCfg.ID, 1)
        Dim strDeviceType As String = ""
        Dim strDeviceIndex As String = ""
        Dim strResult As String = ""
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
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        i.StepInputNumber = 100

                    Case 100
                        cBarcodeManager = New clsBarcodeManager
                        cBarcodeManager.Init(cSystemElement)
                        cLocalElement(clsBarcodeManager.Name) = cBarcodeManager
                        If Not CType(cHMIScanner, clsHMIAutoScanner).Scan(3000, strScanResult, strErrorMsg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strErrorMsg)
                            cActionResultCfg.ErrorType = enumScannerErrorType.ScannerError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            cMachineStatusManager.SetJump(cRunnerCfg.StationName, True)
                            Return False
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 101
                        If Not cScanResult.Scanner(lListParameter(0), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), lListParameter(1), cLocalElement, cSystemElement, strScanResult, strErrorMsg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strErrorMsg)
                            cActionResultCfg.ErrorType = enumScannerErrorType.ScannerError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            cMachineStatusManager.SetJump(cRunnerCfg.StationName, True)
                            Return False
                        Else
                            cActionShowManager.UpdateLastActionStepValue(strScanResult)
                            i.StepInputNumber = 300
                        End If

                    Case 300
                        Select Case lListParameter(1)
                            Case enumScannerMethod.ScanHousingAndWriteSN.ToString, enumScannerMethod.ScanHousingAndWriteAndCheckSN.ToString, enumScannerMethod.ScanHousingAndCheckSN.ToString
                                i.StepInputNumber = 310
                            Case enumScannerMethod.ScanPFCPCB.ToString, enumScannerMethod.ScanDCFilterPCB.ToString
                                i.StepInputNumber = 330
                            Case enumScannerMethod.ScanDCPCB.ToString, enumScannerMethod.ScanSystemPCB.ToString, enumScannerMethod.ScanPCBA.ToString, enumScannerMethod.ScanSFC.ToString
                                i.StepInputNumber = 330
                            Case enumScannerMethod.ScanDCPCB_AID.ToString
                                i.StepInputNumber = 400
                            Case enumScannerMethod.ScanChoker.ToString, enumScannerMethod.ScanHU.ToString
                                i.StepInputNumber = 350
                            Case enumScannerMethod.ScanOther.ToString, enumScannerMethod.ScanHousing.ToString
                                i.StepInputNumber = 400
                        End Select

                    Case 310
                        cPLCAction.WriteSN(cHMIPLC, cRunnerCfg.StationName, cLocalVariantManager.CurrentVariantCfg.SFC)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 311
                        cMachineStatusManager.SetSFC(cRunnerCfg.StationName, cLocalVariantManager.CurrentVariantCfg.SFC)
                        i.StepInputNumber = 400

                        'PFC PCB
                    Case 320
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 400
                        End If

                    Case 321
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "7", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1


                    Case 322
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "8", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).validateSfc(cLocalVariantManager.CurrentVariantCfg.SFC, cBarcodeManager.SFC, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 323
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "10", GetMessage(cSubStepCfg))))
                        cMESDataManager.InSertData(cLocalVariantManager.CurrentVariantCfg.SFC, cBarcodeManager.MaterialNumber, cBarcodeManager.MaterialVersion, cBarcodeManager.SFC)
                        i.StepInputNumber = 400

                        'DC PCB
                    Case 330
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 400
                        End If

                    Case 331
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "7", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1


                    Case 332
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "8", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).validateSfc(cLocalVariantManager.CurrentVariantCfg.SFC, cBarcodeManager.SFC, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            lListComponentData.Clear()
                            Dim cComponentDataCfg As New clsComponentDataCfg
                            cComponentDataCfg.MaterialId = cBarcodeManager.MaterialNumber
                            cComponentDataCfg.MaterialRevision = cBarcodeManager.MaterialVersion
                            cComponentDataCfg.Inventory = cBarcodeManager.SFC
                            cComponentDataCfg.Quantity = 1
                            lListComponentData.Add(cComponentDataCfg)
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 333
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "9", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).Assemble(cLocalVariantManager.CurrentVariantCfg.SFC, 1, lListComponentData, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            i.StepInputNumber = 400
                        End If

                        'DC PCB-AID
                    Case 340
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 400
                        End If

                    Case 341
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "7", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1


                    Case 342
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "11", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).Start(cBarcodeManager.SFC, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), "Start:" + strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 343
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "12", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).Complete(cBarcodeManager.SFC, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), "Complete:" + strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            i.StepInputNumber = 400
                        End If


                        'Choker
                    Case 350
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 400
                        End If

                    Case 351
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "7", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        End If
                        lListbillOfMaterialCfg.Clear()
                        Dim cbillOfMaterialCfg As New clsbillOfMaterialCfg
                        cbillOfMaterialCfg.Item = cBarcodeManager.HandlingUnit
                        lListbillOfMaterialCfg.Add(cbillOfMaterialCfg)
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1


                    Case 352
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "8", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).validateBOM(cLocalVariantManager.CurrentVariantCfg.SFC, lListbillOfMaterialCfg, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            lListComponentData.Clear()
                            Dim cComponentDataCfg As New clsComponentDataCfg
                            cComponentDataCfg.MaterialId = cBarcodeManager.MaterialNumber
                            cComponentDataCfg.MaterialRevision = cBarcodeManager.MaterialVersion
                            cComponentDataCfg.Inventory = cBarcodeManager.HandlingUnit + "-" + cBarcodeManager.Vendor
                            cComponentDataCfg.Quantity = 1
                            lListComponentData.Add(cComponentDataCfg)
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 353
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "9", GetMessage(cSubStepCfg))))
                        If Not CType(cHMIDeviceBase, clsHMIMES).Assemble(cLocalVariantManager.CurrentVariantCfg.SFC, 1, lListComponentData, strResult) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("AutoStationScan", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
                            Return False
                        Else
                            i.StepInputNumber = 400
                        End If

                    Case 400
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("AutoStationScan", "5", GetMessage(cSubStepCfg))))
                        Return True
                End Select

            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumScannerErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainScannerErrorType.ScannerError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("AutoStationScan", cActionResultCfg.ErrorType, 0)
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

Public Enum enumMainScannerErrorType
    ScannerError = 1
End Enum

Public Enum enumScannerErrorType
    ScannerError = 1
    UnKnownError = 2
End Enum
