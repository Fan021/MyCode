Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIActionName("MES", enumHMIActionType.ManualAuto, enumHMISubActionType.SubAction)>
Public Class clsMESAction
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
    Private lListlogParameter As New List(Of clslogParameterCfg)
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
        Dim cHMIMES As clsHMIMES = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, lListParameter(0), GetType(clsHMIMES)).Source
        Dim cLocalVariantManager As clsVariantManager = cLocalElement(clsVariantManager.Name)
        Dim cLanguageManager As clsLanguageManager = cSystemElement(clsLanguageManager.Name)
        Dim lListNcData As New List(Of clsNcDataCfg)
        Dim cNcDataCfg As New clsNcDataCfg
        Dim cMESDataCfg As New clsMESDataCfg
        bExit = False
        Dim strResult As String = ""
        i.StepInputNumber = i.Address_Home
        If cHMIMES.Enable = False Then Return True
        Try
            Do While Not bExit
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        If lListParameter(1) <> enumMESMethod.Complete.ToString Or cMachineStationCfg.CompleteStep <> "2" Then cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                        If lListParameter(1) <> enumMESMethod.Complete.ToString Or cMachineStationCfg.CompleteStep <> "2" Then ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        i.StepInputNumber = i.StepInputNumber + 1
                    Case 1
                        Select Case lListParameter(1)
                            Case enumMESMethod.Start.ToString
                                i.StepInputNumber = 100
                            Case enumMESMethod.logNonConformance.ToString
                                i.StepInputNumber = 200
                            Case enumMESMethod.Complete.ToString
                                i.StepInputNumber = 300
                            Case enumMESMethod.Assemble.ToString
                                i.StepInputNumber = 400
                            Case enumMESMethod.logListNonConformance.ToString
                                i.StepInputNumber = 500
                            Case enumMESMethod.AssembleOrlogNonConformance.ToString
                                i.StepInputNumber = 600
                            Case enumMESMethod.logRetryNonConformance.ToString
                                i.StepInputNumber = 700
                            Case enumMESMethod.CreateSFC.ToString
                                i.StepInputNumber = 800
                            Case enumMESMethod.StartWithConfirm.ToString
                                i.StepInputNumber = 800
                            Case enumMESMethod.logData_Carrier.ToString
                                i.StepInputNumber = 1000

                        End Select

                    Case 100
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 101
                        If cHMIMES.Start(cLocalVariantManager.CurrentVariantCfg.SFC, strResult, cLocalVariantManager.CurrentVariantCfg.Program) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            Dim mTemp As String = ""
                            Dim iCnt As Integer = cHMIMES.getSFCOperation(cLocalVariantManager.CurrentVariantCfg.SFC, mTemp)
                            If iCnt = -1 Then
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.Abort = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            ElseIf iCnt = -2 Then
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.Abort = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            ElseIf iCnt = -3 Then
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                                cHMIMES.Running = False
                                Return True
                            ElseIf iCnt = -4 Then
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.OtherStationInQueue2 = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                                cHMIMES.Running = False
                                Return True
                            Else
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            End If
                            Return False
                        End If


                    Case 200
                        cFailureActionManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, lListError)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 201
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 202
                        lListNcData = New List(Of clsNcDataCfg)
                        If lListError.Count = 0 Then
                            cNcDataCfg = New clsNcDataCfg
                            cNcDataCfg.Identifier = cActionResultCfg.ErrorType
                            cNcDataCfg.NcComment = cActionResultCfg.ErrorMessage
                            If cActionResultCfg.Location <> "" Then cNcDataCfg.Location = cActionResultCfg.Location
                            If cActionResultCfg.MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = cActionResultCfg.MESPosition
                            If cActionResultCfg.Value <> -999999 Then cNcDataCfg.Value = cActionResultCfg.Value
                            If cActionResultCfg.LowLimit <> -999999 Then cNcDataCfg.LowerLimit = cActionResultCfg.LowLimit
                            If cActionResultCfg.UpLimit <> -999999 Then cNcDataCfg.UpperLimit = cActionResultCfg.UpLimit
                            lListNcData.Add(cNcDataCfg)
                        Else
                            For kk = 0 To lListError.Count - 1
                                cNcDataCfg = New clsNcDataCfg
                                cNcDataCfg.Identifier = lListError(kk).ErrorType
                                cNcDataCfg.NcComment = lListError(kk).ErrorMessage
                                If lListError(kk).Location <> "" Then cNcDataCfg.Location = lListError(kk).Location
                                If lListError(kk).MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = lListError(kk).MESPosition
                                If lListError(kk).Value <> -999999 Then cNcDataCfg.Value = lListError(kk).Value
                                If lListError(kk).LowLimit <> -999999 Then cNcDataCfg.LowerLimit = lListError(kk).LowLimit
                                If lListError(kk).UpLimit <> -999999 Then cNcDataCfg.UpperLimit = lListError(kk).UpLimit
                                lListNcData.Add(cNcDataCfg)
                            Next
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1


                    Case 203
                        If cHMIMES.logNonConformance(cLocalVariantManager.CurrentVariantCfg.SFC, lListNcData, strResult) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC, cRunnerCfg.StationName)
                            Return True
                        Else
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If


                    Case 300
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 301
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 302
                        If cHMIMES.Complete(cLocalVariantManager.CurrentVariantCfg.SFC, strResult) Then
                            cHMIMES.Running = False
                            If lListParameter(1) <> enumMESMethod.Complete.ToString Or cMachineStationCfg.CompleteStep <> "2" Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If

                    Case 400
                        If cMESDataManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, cMESDataCfg) Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If



                    Case 401
                        lListComponentData.Clear()
                        Dim cComponentDataCfg As New clsComponentDataCfg
                        cComponentDataCfg.MaterialId = cMESDataCfg.MaterialNumber
                        cComponentDataCfg.MaterialRevision = cMESDataCfg.MaterialVersion
                        cComponentDataCfg.Inventory = cMESDataCfg.MaterialSFC
                        cComponentDataCfg.Quantity = 1
                        lListComponentData.Add(cComponentDataCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 402
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If
                    Case 403
                        If cHMIMES.Assemble(cLocalVariantManager.CurrentVariantCfg.SFC, 1, lListComponentData, strResult) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else

                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If


                    Case 500
                        cFailureActionManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, lListError)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 501
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 502
                        lListNcData = New List(Of clsNcDataCfg)
                        If lListError.Count = 0 Then
                            cNcDataCfg = New clsNcDataCfg
                            cNcDataCfg.Identifier = cActionResultCfg.ErrorType
                            cNcDataCfg.NcComment = cActionResultCfg.ErrorMessage
                            If cActionResultCfg.Location <> "" Then cNcDataCfg.Location = cActionResultCfg.Location
                            If cActionResultCfg.MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = cActionResultCfg.MESPosition
                            If cActionResultCfg.Value <> -999999 Then cNcDataCfg.Value = cActionResultCfg.Value
                            If cActionResultCfg.LowLimit <> -999999 Then cNcDataCfg.LowerLimit = cActionResultCfg.LowLimit
                            If cActionResultCfg.UpLimit <> -999999 Then cNcDataCfg.UpperLimit = cActionResultCfg.UpLimit
                            lListNcData.Add(cNcDataCfg)
                        Else
                            For kk = 0 To lListError.Count - 1
                                cNcDataCfg = New clsNcDataCfg
                                cNcDataCfg.Identifier = lListError(kk).ErrorType
                                cNcDataCfg.NcComment = lListError(kk).ErrorMessage
                                If lListError(kk).Location <> "" Then cNcDataCfg.Location = lListError(kk).Location
                                If lListError(kk).MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = lListError(kk).MESPosition
                                If lListError(kk).Value <> -999999 Then cNcDataCfg.Value = lListError(kk).Value
                                If lListError(kk).LowLimit <> -999999 Then cNcDataCfg.LowerLimit = lListError(kk).LowLimit
                                If lListError(kk).UpLimit <> -999999 Then cNcDataCfg.UpperLimit = lListError(kk).UpLimit
                                lListNcData.Add(cNcDataCfg)
                            Next
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 503
                        If cHMIMES.logNonConformance(cLocalVariantManager.CurrentVariantCfg.SFC, lListNcData, strResult) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC)
                            Return True
                        Else
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If


                    Case 600
                        cFailureActionManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, lListError)
                        If lListError.Count > 0 Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            If Not cHMIMES.Running Then
                                cHMIMES.Running = True
                                i.StepInputNumber = 605
                            End If
                        End If

                    Case 601
                        cFailureActionManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, lListError)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 602
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 603
                        lListNcData = New List(Of clsNcDataCfg)
                        If lListError.Count = 0 Then
                            cNcDataCfg = New clsNcDataCfg
                            cNcDataCfg.Identifier = cActionResultCfg.ErrorType
                            cNcDataCfg.NcComment = cActionResultCfg.ErrorMessage
                            If cActionResultCfg.Location <> "" Then cNcDataCfg.Location = cActionResultCfg.Location
                            If cActionResultCfg.MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = cActionResultCfg.MESPosition
                            If cActionResultCfg.Value <> -999999 Then cNcDataCfg.Value = cActionResultCfg.Value
                            If cActionResultCfg.LowLimit <> -999999 Then cNcDataCfg.LowerLimit = cActionResultCfg.LowLimit
                            If cActionResultCfg.UpLimit <> -999999 Then cNcDataCfg.UpperLimit = cActionResultCfg.UpLimit
                            lListNcData.Add(cNcDataCfg)
                        Else
                            For kk = 0 To lListError.Count - 1
                                cNcDataCfg = New clsNcDataCfg
                                cNcDataCfg.Identifier = lListError(kk).ErrorType
                                cNcDataCfg.NcComment = lListError(kk).ErrorMessage
                                If lListError(kk).Location <> "" Then cNcDataCfg.Location = lListError(kk).Location
                                If lListError(kk).MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = lListError(kk).MESPosition
                                If lListError(kk).Value <> -999999 Then cNcDataCfg.Value = lListError(kk).Value
                                If lListError(kk).LowLimit <> -999999 Then cNcDataCfg.LowerLimit = lListError(kk).LowLimit
                                If lListError(kk).UpLimit <> -999999 Then cNcDataCfg.UpperLimit = lListError(kk).UpLimit
                                lListNcData.Add(cNcDataCfg)
                            Next
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 604
                        If cHMIMES.logNonConformance(cLocalVariantManager.CurrentVariantCfg.SFC, lListNcData, strResult) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If


                    Case 605
                        If cHMIMES.Complete(cLocalVariantManager.CurrentVariantCfg.SFC, strResult) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If

                    Case 700
                        cFailureActionManager.GetData(cLocalVariantManager.CurrentVariantCfg.SFC, lListError)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 701
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 702
                        lListNcData = New List(Of clsNcDataCfg)
                        cNcDataCfg = New clsNcDataCfg
                        cNcDataCfg.Identifier = cActionResultCfg.ErrorType
                        cNcDataCfg.NcComment = cActionResultCfg.ErrorMessage
                        If cActionResultCfg.Location <> "" Then cNcDataCfg.Location = cActionResultCfg.Location
                        If cActionResultCfg.MESPosition <> "" Then cNcDataCfg.ReferenceDesignator = cActionResultCfg.MESPosition
                        If cActionResultCfg.Value <> -999999 Then cNcDataCfg.Value = cActionResultCfg.Value
                        If cActionResultCfg.LowLimit <> -999999 Then cNcDataCfg.LowerLimit = cActionResultCfg.LowLimit
                        If cActionResultCfg.UpLimit <> -999999 Then cNcDataCfg.UpperLimit = cActionResultCfg.UpLimit
                        lListNcData.Add(cNcDataCfg)
                        i.StepInputNumber = i.StepOutputNumber + 1


                    Case 703
                        If cHMIMES.logNonConformance(cLocalVariantManager.CurrentVariantCfg.SFC, lListNcData, strResult, True) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC, cRunnerCfg.StationName)
                            Return True
                        Else
                            cActionResultCfg.DisableContinue = True
                            cActionResultCfg.OnlyShowMessage = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cActionResultCfg.ErrorMessage + vbCrLf + cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If

                    Case 800
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 801
                        If cHMIMES.Start(cLocalVariantManager.CurrentVariantCfg.SFC, strResult, cLocalVariantManager.CurrentVariantCfg.Program) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            Dim mTemp As String = ""
                            Dim iCnt As Integer = cHMIMES.getSFCOperation(cLocalVariantManager.CurrentVariantCfg.SFC, mTemp)
                            If iCnt = -1 Then
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            ElseIf iCnt = -2 Then
                                cActionResultCfg.ErrorLevel = enumErrorLevel.Normal
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            ElseIf iCnt = -3 Then
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                                cHMIMES.Running = False
                                Return True
                            ElseIf iCnt = -4 Then
                                cActionResultCfg.OtherStationInQueue = True
                                cActionResultCfg.OtherStationInQueue2 = True
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                                cHMIMES.Running = False
                                Return True
                            Else
                                cActionResultCfg.Result = False
                                cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                                cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                                cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                                cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                                cHMIMES.Running = False
                            End If
                            Return False
                        End If


                    Case 900
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 901
                        If cHMIMES.Start(cLocalVariantManager.CurrentVariantCfg.SFC, strResult, cLocalVariantManager.CurrentVariantCfg.Program) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If


                    Case 1000
                        If Not cHMIMES.Running Then
                            cHMIMES.Running = True
                            lListlogParameter.Clear()
                            Dim clogParameterCfg As New clslogParameterCfg
                            clogParameterCfg = New clslogParameterCfg
                            clogParameterCfg.Identifier = lListParameter(2)
                            clogParameterCfg.Value = cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID.ToString
                            clogParameterCfg.DataType = "TEXT"
                            lListlogParameter.Add(clogParameterCfg)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 1001
                        If cHMIMES.logParameters(cLocalVariantManager.CurrentVariantCfg.SFC, lListlogParameter, strResult) Then
                            cHMIMES.Running = False
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("GlobalStationMES", "5", GetMessage(cSubStepCfg))))
                            Return True
                        Else
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("GlobalStationMES", "6", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumMesErrorType.MesError.ToString
                            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
                            cHMIMES.Running = False
                            Return False
                        End If

                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumMesErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainMesErrorType.MesError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("GlobalStationMES", cActionResultCfg.ErrorType, 0)
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

Public Enum enumMainMesErrorType
    MesError = 1
End Enum

Public Enum enumMesErrorType
    MesError = 1
    UnKnownError = 2
End Enum
