Imports System.Windows.Forms

Imports System.Runtime.InteropServices
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.Statistics.Screw
Imports System.Collections.Concurrent
Imports System.Threading
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIActionName("ManualStationScrew", enumHMIActionType.Manual, enumHMISubActionType.SubAction)>
Public Class clsScrewAction
    Inherits clsHMIActionBase
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cScrewDataManager As clsScrewDataManager
    Private i As New clsStep
    Private bExit As Boolean = False
    Private cRunnerCfg As clsRunnerCfg
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cASTCfg As clsASTCfg
    Private cTempASTCfg As clsASTCfg
    Private strComment As String = String.Empty
    Private strErrorType As String = String.Empty
    Private cHMIDeviceBase As clsHMIDeviceBase
    Private cRunActionCfg As New clsRunActionCfg
    Private cThread As Thread
    Private cAST As clsHMIAST
    Private cPKP As clsHMIPKP
    Private interPass As Boolean = False
    Private interFail As Boolean = False
    Private cActionDataManager As clsActionDataManager
    Private lListlogParameter As New List(Of clslogParameterCfg)
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
            If IsNothing(cScrewDataManager) Then
                cScrewDataManager = New clsScrewDataManager
                cScrewDataManager.Init(cSystemElement)
            End If
            If IsNothing(cActionDataManager) Then
                cActionDataManager = New clsActionDataManager
                cActionDataManager.Init(cSystemElement)
            End If
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
        cAST = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(1), GetType(clsHMIAST)).Source
        Dim strDevice As String = cMachineManager.ActionParameterManager.GetActionParameterDevice("ManualStationScrew", cMachineStationCfg.ID, 1)
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
                        interPass = False
                        interFail = False

                        If Not cRunActionCfg.IsRunning Then
                            cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cSubStepCfg.ActiveDescription(cLocalElement))
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If
                    Case 1
                        ShowMessageAndPicture(cPictureShowManager, cMainStepCfg, cSubStepCfg)
                        cPLCAction.DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "57", GetMessage(cSubStepCfg))))
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(lListParameter(2))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 2
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Waiting)
                        If cPLCAction.HmiAction.bulPLCDoAction Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "33", GetMessage(cSubStepCfg))))
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
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "34", GetMessage(cSubStepCfg))))
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
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 6
                        If cRunActionCfg.Result Then
                            i.StepInputNumber = 10
                        Else
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 7
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "60", GetMessage(cSubStepCfg), cRunActionCfg.Message), enumMainTipsManagerType.Confirm))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 8
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "60", GetMessage(cSubStepCfg), cRunActionCfg.Message)
                            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
                            cActionResultCfg.MESPosition = lListParameter(12)
                            cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                            Return False
                        End If

                    Case 9
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(lListParameter(2))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = 5

                    Case 10
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "42", GetMessage(cSubStepCfg))))
                        cTempASTCfg = cAST.GetASTValue()
                        cTempASTCfg.fdProg = Short.Parse(lListParameter(2))
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 11
                        cASTCfg = cAST.ChangeASTValue(cTempASTCfg)
                        If cASTCfg.fdStatus = 0 Then
                            strComment = cLanguageManager.GetUserTextLine("ManualStationScrew", "OK")
                        Else
                            strComment = CType([Enum].ToObject(GetType(enumScrewErrorType), Integer.Parse(cASTCfg.fdStatus)), enumScrewErrorType).ToString
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 12
                        cScrewDataManager.InSertData(cMachineManager.MachineCellManager.CurrentMachineCfg.CellName,
                                                     cMachineStationCfg.ID.ToString,
                                                     cMachineStatusCfg.VariantCfg.Variant,
                                                     cMachineStatusCfg.VariantCfg.SFC,
                                                     cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement),
                                                     lListParameter(4),
                                                     lListParameter(1),
                                                     cSubStepCfg.SubStepParameter(HMISubStepKeys.ID),
                                                     lListParameter(2),
                                                     cASTCfg.fdStatus.ToString,
                                                     Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                     cASTCfg.fdStep.ToString,
                                                     cASTCfg.fdTime,
                                                     cASTCfg.fdStep1.ToString,
                                                     cASTCfg.fdTorqueLow1,
                                                     cASTCfg.fdTorqueTarget1,
                                                     cASTCfg.fdTorque1,
                                                     cASTCfg.fdTorqueUp1,
                                                     cASTCfg.fdAngleLow1,
                                                     cASTCfg.fdAngleTarget1,
                                                     cASTCfg.fdAngle1,
                                                     cASTCfg.fdAngleUp1,
                                                     cASTCfg.fdStep2.ToString,
                                                     cASTCfg.fdTorqueLow2,
                                                     cASTCfg.fdTorqueTarget2,
                                                     cASTCfg.fdTorque2,
                                                     cASTCfg.fdTorqueUp2,
                                                     cASTCfg.fdAngleLow2,
                                                     cASTCfg.fdAngleTarget2,
                                                     cASTCfg.fdAngle2,
                                                     cASTCfg.fdAngleUp2,
                                                     cASTCfg.fdStep3.ToString,
                                                     cASTCfg.fdTorqueLow3,
                                                     cASTCfg.fdTorqueTarget3,
                                                     cASTCfg.fdTorque3,
                                                     cASTCfg.fdTorqueUp3,
                                                     cASTCfg.fdAngleLow3,
                                                     cASTCfg.fdAngleTarget3,
                                                     cASTCfg.fdAngle3,
                                                     cASTCfg.fdAngleUp3,
                                                     strComment)

                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 13
                        If strDeviceType = "KDX" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 20
                        End If

                    Case 14
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIKDX))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "48", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
                            cActionResultCfg.MESPosition = lListParameter(12)
                            cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                            Return False
                        End If
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 15
                        Dim strVariant As String = String.Empty
                        Dim iIndex As Integer = 0
                        Dim cValue() As String = cMachineStatusCfg.VariantCfg.Variant.Split("-")
                        If cValue.Count = 2 Then
                            strVariant = cValue(0)
                            If IsNumeric(cValue(1)) Then
                                iIndex = CInt(cValue(1))
                            End If
                        Else
                            strVariant = cMachineStatusCfg.VariantCfg.Variant
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).SetInfo(strVariant, iIndex, cMachineStatusCfg.VariantCfg.SFC)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 16
                        Dim strStepname As String = String.Empty
                        Dim strTestStep As String = String.Empty
                        Dim bResult As Boolean = False
                        Dim strUnit As String = String.Empty
                        Dim strStep As Integer = 0

                        'step1
                        strStep = 1
                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep1.ToString("00") + "_" + "A"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cLanguageManager.GetUserTextLine("ManualStationScrew", "AngleUnit")
                        strTestStep += "." + cASTCfg.fdStep1.ToString("00") + "1" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdAngle1 >= cASTCfg.fdAngleLow1 And cASTCfg.fdAngle1 <= cASTCfg.fdAngleUp1 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdAngleLow1, cASTCfg.fdAngleUp1, cASTCfg.fdAngle1, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)


                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep1.ToString("00") + "_" + "T"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cASTCfg.strTorqueUnit
                        strTestStep += "." + cASTCfg.fdStep1.ToString("00") + "2" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdTorque1 >= cASTCfg.fdTorqueLow1 And cASTCfg.fdTorque1 < cASTCfg.fdTorqueUp1 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdTorqueLow1, cASTCfg.fdTorqueUp1, cASTCfg.fdTorque1, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)

                        'step2
                        strStep = 2
                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep2.ToString("00") + "_" + "A"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cLanguageManager.GetUserTextLine("ManualStationScrew", "AngleUnit")
                        strTestStep += "." + cASTCfg.fdStep2.ToString("00") + "1" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdAngle2 >= cASTCfg.fdAngleLow2 And cASTCfg.fdAngle2 < cASTCfg.fdAngleUp2 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdAngleLow2, cASTCfg.fdAngleUp2, cASTCfg.fdAngle2, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)


                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep2.ToString("00") + "_" + "T"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cASTCfg.strTorqueUnit
                        strTestStep += "." + cASTCfg.fdStep2.ToString("00") + "2" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdTorque2 >= cASTCfg.fdTorqueLow2 And cASTCfg.fdTorque2 <= cASTCfg.fdTorqueUp2 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdTorqueLow2, cASTCfg.fdTorqueUp2, cASTCfg.fdTorque2, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)

                        'step3
                        strStep = 3
                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep3.ToString("00") + "_" + "A"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cLanguageManager.GetUserTextLine("ManualStationScrew", "AngleUnit")
                        strTestStep += "." + cASTCfg.fdStep3.ToString("00") + "1" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdAngle3 >= cASTCfg.fdAngleLow3 And cASTCfg.fdAngle3 < cASTCfg.fdAngleUp3 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdAngleLow3, cASTCfg.fdAngleUp3, cASTCfg.fdAngle3, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)

                        strStepname = cASTCfg.strIP + "_" + cASTCfg.fdProg.ToString + "_" + cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) + "_" + cActionResultCfg.RepeatNum.ToString("00") + "_" + cASTCfg.fdStep3.ToString("00") + "_" + "T"
                        strTestStep = cASTCfg.fdProg.ToString("00") + CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)).ToString("00")
                        strUnit = cASTCfg.strTorqueUnit
                        strTestStep += "." + cASTCfg.fdStep3.ToString("00") + "2" + cActionResultCfg.RepeatNum.ToString("00")
                        If cASTCfg.fdTorque3 >= cASTCfg.fdTorqueLow3 And cASTCfg.fdTorque3 <= cASTCfg.fdTorqueUp3 Then
                            bResult = True
                        Else
                            bResult = False
                        End If
                        CType(cHMIDeviceBase, clsHMIKDX).AddTeststep(strTestStep, strStepname, strUnit, cASTCfg.fdTorqueLow3, cASTCfg.fdTorqueUp3, cASTCfg.fdTorque3, bResult, cASTCfg.fdTime, cActionResultCfg.RepeatNum, cASTCfg.fdStatus)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 17
                        CType(cHMIDeviceBase, clsHMIKDX).DataSave()
                        i.StepInputNumber = 30

                    Case 20
                        If strDeviceType = "MES" Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        Else
                            i.StepInputNumber = 30
                        End If

                    Case 21
                        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, strDeviceIndex, GetType(clsHMIMES))
                        If IsNothing(cDeviceCfg) Then
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "49", GetMessage(cSubStepCfg), strDeviceIndex)
                            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
                            cActionResultCfg.MESPosition = lListParameter(12)
                            cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                            Return False
                        End If
                        cHMIDeviceBase = cDeviceCfg.Source
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 22
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "59", GetMessage(cSubStepCfg))))
                        lListlogParameter.Clear()
                        Dim clogParameterCfg As New clslogParameterCfg
                        'ID
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.UniqueID.ToString), 2)
                        Dim mCellName As String = cMachineManager.MachineCellManager.MachineCellCfg.CellName
                        If mCellName.Length > 3 Then
                            mCellName = mCellName.Substring(mCellName.Length - 3)
                        End If
                        Dim mSpearter As String = "-"
                        Dim mStationName As String = cMachineStationCfg.ID.ToString
                        Dim mASTNumber As String = lListParameter(1)
                        Dim mActionNumber As String = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                        If mActionNumber.Length > 2 Then
                            mActionNumber = mActionNumber.Substring(mActionNumber.Length - 2)
                        End If
                        Dim strScrewVariant As String = cSubStepCfg.SubStepParameter(HMISubStepKeys.Component)
                        If strScrewVariant.Length > 8 Then
                            strScrewVariant = strScrewVariant.Substring(0, 8)
                        End If
                        Dim mASTProNumber As String = lListParameter(2).ToString
                        If mASTProNumber.Length > 2 Then
                            mASTProNumber = mASTProNumber.Substring(mASTProNumber.Length - 2)
                        End If

                        Dim mResult As String = cASTCfg.fdStatus.ToString
                        If mResult.Length > 2 Then
                            mResult = mResult.Substring(mResult.Length - 2)
                        End If

                        Dim mNewValue As String = mCellName & mSpearter & mStationName & mSpearter & mASTNumber & mSpearter & mActionNumber & mSpearter & strScrewVariant & mSpearter & mASTProNumber & mSpearter & mResult

                        clogParameterCfg.Value = mNewValue
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)

                        'MATERIAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.MATERIAL.ToString), 2)
                        clogParameterCfg.Value = cMachineStatusCfg.VariantCfg.Variant
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'SFC
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.SFC.ToString), 2)
                        clogParameterCfg.Value = cMachineStatusCfg.VariantCfg.SFC
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'Material_HU
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.Material_HU.ToString), 2)
                        clogParameterCfg.Value = cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement)
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'Control_HU
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.Control_HU.ToString), 2)
                        clogParameterCfg.Value = lListParameter(1)
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'TS
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.TS.ToString), 2)
                        clogParameterCfg.Value = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'RECIPE
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.RECIPE.ToString), 2)
                        clogParameterCfg.Value = lListParameter(2)
                        clogParameterCfg.DataType = "TEXT"
                        lListlogParameter.Add(clogParameterCfg)
                        'STATUS
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STATUS.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdStatus.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'TIMESTAMP
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.TIMESTAMP.ToString), 2)
                        clogParameterCfg.Value = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEPID
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEPID.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdStep.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_TORQUE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_TORQUE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueLow1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_TORQUE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_TORQUE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueUp1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_TORQUE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_TORQUE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueTarget1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_TORQUE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_TORQUE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorque1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_ANGLE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_ANGLE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleLow1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_ANGLE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_ANGLE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleUp1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_ANGLE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_ANGLE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleTarget1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP1_ANGLE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP1_ANGLE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngle1.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)

                        'STEP2_TORQUE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_TORQUE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueLow2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_TORQUE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_TORQUE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueUp2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_TORQUE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_TORQUE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueTarget2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_TORQUE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_TORQUE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorque2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_ANGLE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_ANGLE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleLow2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_ANGLE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_ANGLE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleUp2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_ANGLE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_ANGLE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleTarget2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP2_ANGLE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP2_ANGLE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngle2.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)

                        'STEP3_TORQUE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_TORQUE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueLow3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_TORQUE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_TORQUE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueUp3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_TORQUE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_TORQUE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorqueTarget3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_TORQUE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_TORQUE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTorque3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_ANGLE_LL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_ANGLE_LL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleLow3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_ANGLE_UL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_ANGLE_UL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleUp3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_ANGLE_TAR_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_ANGLE_TAR_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngleTarget3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'STEP3_ANGLE_MEA_VAL
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.STEP3_ANGLE_MEA_VAL.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdAngle3.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)
                        'RUN_TIME
                        clogParameterCfg = New clslogParameterCfg
                        clogParameterCfg.Identifier = cMachineManager.ActionParameterManager.GetActionParameterParameterValue("ManualStationScrew", cLanguageManager.GetUserTextLine("ManualStationScrew", enumMESParameter.RUN_TIME.ToString), 2)
                        clogParameterCfg.Value = cASTCfg.fdTime.ToString
                        clogParameterCfg.DataType = "NUMERIC"
                        lListlogParameter.Add(clogParameterCfg)

                        If Not CType(cHMIDeviceBase, clsHMIMES).logParameters(cMachineStatusCfg.VariantCfg.SFC, lListlogParameter, strResult) Then
                            '   CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            i.StepInputNumber = 30
                        End If

                    Case 23
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "60", GetMessage(cSubStepCfg), strResult), enumMainTipsManagerType.Confirm))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 24
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = 22
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "60", GetMessage(cSubStepCfg), strResult)
                            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
                            cActionResultCfg.MESPosition = lListParameter(12)
                            cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                            Return False
                        End If

                    Case 30
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
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "35", GetMessage(cSubStepCfg))))
                        Return True

                    Case 200
                        cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 201
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 202
                        strErrorType = CType([Enum].ToObject(GetType(enumScrewErrorType), Integer.Parse(cASTCfg.fdStatus)), enumScrewErrorType).ToString
                        If cASTCfg.fdStatus = 2 Or cASTCfg.fdStatus = 3 Then
                            cActionResultCfg.UpLimit = cASTCfg.fdAngleUpNOk.ToString
                            cActionResultCfg.LowLimit = cASTCfg.fdAngleLowNOk.ToString
                            cActionResultCfg.Value = cASTCfg.fdAngleNOk.ToString
                            cActionResultCfg.Location = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "44", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType), cASTCfg.fdAngleNOk.ToString, cASTCfg.fdAngleUpNOk.ToString, cASTCfg.fdAngleLowNOk.ToString)
                        ElseIf cASTCfg.fdStatus = 4 Or cASTCfg.fdStatus = 5 Then
                            cActionResultCfg.UpLimit = cASTCfg.fdTorqueUpNOk.ToString
                            cActionResultCfg.LowLimit = cASTCfg.fdTorqueLowNOk.ToString
                            cActionResultCfg.Value = cASTCfg.fdTorqueNOk.ToString
                            cActionResultCfg.Location = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "44", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType), cASTCfg.fdTorqueNOk.ToString, cASTCfg.fdTorqueUpNOk.ToString, cASTCfg.fdTorqueLowNOk.ToString)
                        Else
                            cActionResultCfg.Location = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "45", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType))
                        End If
                        cActionResultCfg.MESPosition = lListParameter(12)
                        cActionResultCfg.ErrorType = strErrorType
                        cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", strErrorType, 0)
                        cActionResultCfg.Result = False
                        Return False
                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
            Return False
        End Try
    End Function

    Public Sub RunAction()
        Try
            cAST.GetWebValue(cRunActionCfg.GetParameter(0))
            cRunActionCfg.Result = True
            cRunActionCfg.IsRunning = False
        Catch ex As Exception
            cRunActionCfg.Message = ex.Message.ToString
            cRunActionCfg.Result = False
            cRunActionCfg.IsRunning = False
        End Try
    End Sub

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

    Private Sub ShowScrewMessageAndPicture(ByVal cPictureShowManager As clsPictureShowManager, ByVal cMainStepCfg As clsMainStepCfg, ByVal cSubStepCfg As clsSubStepCfg, ByVal lListParameter As System.Collections.Generic.List(Of String))
        Dim lListShowAction As New List(Of clsShowActionCfg)
        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
            If lListParameter(13) = "0" Then
                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "50", GetMessage(cSubStepCfg))))
            Else
                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "51", GetMessage(cSubStepCfg), lListParameter(13))))
            End If

            cPictureShowManager.ShowPicture(lListParameter(14), cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType))
        Else
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cMainStepCfg.ActiveDescription(cLocalElement)))
            cPictureShowManager.ShowActions(GetListAction(cMainStepCfg))
        End If

    End Sub

    Private Sub ShowDiscardMessageAndPicture(ByVal cPictureShowManager As clsPictureShowManager, ByVal cMainStepCfg As clsMainStepCfg, ByVal cSubStepCfg As clsSubStepCfg, ByVal lListParameter As System.Collections.Generic.List(Of String))
        Dim lListShowAction As New List(Of clsShowActionCfg)
        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "54", GetMessage(cSubStepCfg))))
            cPictureShowManager.ShowPicture(lListParameter(17), cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType))
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
        cAST = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(1), GetType(clsHMIAST)).Source
        Dim strDevice As String = cMachineManager.ActionParameterManager.GetActionParameterDevice("ManualStationScrew", cMachineStationCfg.ID, 1)
        Dim strDeviceType As String = ""
        Dim strDeviceIndex As String = ""
        Dim cStartTime As DateTime
        Dim swAction As Stopwatch = Nothing
        Dim bStart As Boolean = False
        cStartTime = Now
        If strDevice <> "" Then
            strDeviceType = strDevice.Split("-")(0)
            strDeviceIndex = strDevice.Split("-")(1)
        End If

        cPKP = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(4), GetType(clsHMIPKP)).Source
        If Not cPKP.ReWorkEnable Then Return True
        bExit = False
        i.StepInputNumber = i.Address_Home
        Try
            Do While Not bExit
                If Not IsNothing(swAction) AndAlso bStart Then cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Continue Do
                Select Case i.StepOutputNumber
                    Case 0
                        interPass = False
                        interFail = False
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If
                    Case 1
                        swAction = New Stopwatch
                        swAction.Start()
                        cActionShowManager.AddNewActionStep(cSubStepCfg.SubStepParameter(HMISubStepKeys.Name) + "-" + cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork"), cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement), enumActionResult.Ongoing, cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork"))
                        bStart = True
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "37", GetMessage(cSubStepCfg))))
                        cPLCAction.DoReWorkAction(cHMIPLC, cMachineStationCfg.ID, True)
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "58", GetMessage(cSubStepCfg))))
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(lListParameter(3))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 2
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Waiting)
                        If cPLCAction.HmiAction.bulPLCDoAction Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "38", GetMessage(cSubStepCfg))))
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 3
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Ongoing)
                        If Not cPLCAction.HmiAction.bulPLCDoAction Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "39", GetMessage(cSubStepCfg))))
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 4
                        cPictureShowManager.FlashIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Ongoing)
                        If cPLCAction.HmiAction.bulPlcActionIsPass Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            interPass = True
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                        If cPLCAction.HmiAction.bulPlcActionIsFail Then
                            cPLCAction.DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            interFail = True
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 5
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 6
                        If cRunActionCfg.Result Then
                            i.StepInputNumber = 10
                        Else
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 7
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 8
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cActionResultCfg.Abort = True
                            cActionResultCfg.Result = False
                            cActionResultCfg.ErrorMessage = cRunActionCfg.Message
                            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
                            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
                            cActionResultCfg.MESPosition = lListParameter(12)
                            cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, enumActionType.Action.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name) + "-" + cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork"), "FAIL", cActionResultCfg.ErrorMessage, cStartTime, Now)
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.FAIL)
                            cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                            swAction.Stop()
                            bStart = False
                            Return False
                        End If

                    Case 9
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(lListParameter(3))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = 5

                    Case 10
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "42", GetMessage(cSubStepCfg))))
                        cTempASTCfg = cAST.GetASTValue()
                        cTempASTCfg.fdProg = Short.Parse(lListParameter(3))
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 11
                        cASTCfg = cAST.ChangeASTValue(cTempASTCfg)
                        If cASTCfg.fdStatus = 0 Then
                            strComment = cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork")
                        Else
                            strComment = cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork")
                            strComment = strComment + " " + CType([Enum].ToObject(GetType(enumScrewErrorType), Integer.Parse(cASTCfg.fdStatus)), enumScrewErrorType).ToString
                        End If
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 12
                        cScrewDataManager.InSertData(cMachineManager.MachineCellManager.CurrentMachineCfg.CellName,
                                                     cMachineStationCfg.ID.ToString,
                                                     cMachineStatusCfg.VariantCfg.Variant,
                                                     cMachineStatusCfg.VariantCfg.SFC,
                                                     cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component, cLocalElement),
                                                     lListParameter(4),
                                                     lListParameter(1),
                                                     cSubStepCfg.SubStepParameter(HMISubStepKeys.ID),
                                                     lListParameter(3),
                                                     cASTCfg.fdStatus.ToString,
                                                     Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                     cASTCfg.fdStep.ToString,
                                                     cASTCfg.fdTime,
                                                     cASTCfg.fdStep1.ToString,
                                                     cASTCfg.fdTorqueLow1,
                                                     cASTCfg.fdTorqueTarget1,
                                                     cASTCfg.fdTorque1,
                                                     cASTCfg.fdTorqueUp1,
                                                     cASTCfg.fdAngleLow1,
                                                     cASTCfg.fdAngleTarget1,
                                                     cASTCfg.fdAngle1,
                                                     cASTCfg.fdAngleUp1,
                                                     cASTCfg.fdStep2.ToString,
                                                     cASTCfg.fdTorqueLow2,
                                                     cASTCfg.fdTorqueTarget2,
                                                     cASTCfg.fdTorque2,
                                                     cASTCfg.fdTorqueUp2,
                                                     cASTCfg.fdAngleLow2,
                                                     cASTCfg.fdAngleTarget2,
                                                     cASTCfg.fdAngle2,
                                                     cASTCfg.fdAngleUp2,
                                                     cASTCfg.fdStep3.ToString,
                                                     cASTCfg.fdTorqueLow3,
                                                     cASTCfg.fdTorqueTarget3,
                                                     cASTCfg.fdTorque3,
                                                     cASTCfg.fdTorqueUp3,
                                                     cASTCfg.fdAngleLow3,
                                                     cASTCfg.fdAngleTarget3,
                                                     cASTCfg.fdAngle3,
                                                     cASTCfg.fdAngleUp3,
                                                     strComment)

                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 13
                        i.StepInputNumber = 30


                    Case 30
                        If interPass Then
                            i.StepInputNumber = 100
                        End If

                        If interFail Then
                            i.StepInputNumber = 200
                        End If


                    Case 100
                        cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 101
                        i.StepInputNumber = i.StepInputNumber + 1
                    Case 102
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetUserTextLine("ManualStationScrew", "40", GetMessage(cSubStepCfg))))
                        cActionShowManager.UpdateLastActionStepResult(enumActionResult.PASS)
                        cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, enumActionType.Action.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name) + "-" + cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork"), "PASS", "", cStartTime, Now)
                        swAction.Stop()
                        bStart = False
                        Return True



                    Case 200
                        cPictureShowManager.ShowIndicate(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID)), enumFlashType.Fail)
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 201
                        i.StepInputNumber = i.StepInputNumber + 1

                    Case 202
                        strErrorType = CType([Enum].ToObject(GetType(enumScrewErrorType), Integer.Parse(cASTCfg.fdStatus)), enumScrewErrorType).ToString
                        If cASTCfg.fdStatus = 2 Or cASTCfg.fdStatus = 3 Then
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "46", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType), cASTCfg.fdAngleNOk, cASTCfg.fdAngleLowNOk, cASTCfg.fdAngleUpNOk)
                        ElseIf cASTCfg.fdStatus = 4 Or cASTCfg.fdStatus = 5 Then
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "46", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType), cASTCfg.fdTorqueNOk, cASTCfg.fdTorqueLowNOk, cASTCfg.fdTorqueUpNOk)
                        Else
                            cActionResultCfg.ErrorMessage = cLanguageManager.GetUserTextLine("ManualStationScrew", "47", GetMessage(cSubStepCfg), cASTCfg.fdStep.ToString, cLanguageManager.GetUserTextLine("ManualStationScrew", strErrorType))
                        End If
                        cActionResultCfg.ErrorType = strErrorType
                        cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
                        cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", strErrorType, 0)
                        cActionResultCfg.MESPosition = lListParameter(12)
                        cActionResultCfg.Result = False
                        cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, enumActionType.Action.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name) + "-" + cLanguageManager.GetUserTextLine("ManualStationScrew", "ReWork"), "FAIL", cActionResultCfg.ErrorMessage, cStartTime, Now)
                        cActionShowManager.UpdateLastActionStepResult(enumActionResult.FAIL)
                        swAction.Stop()
                        bStart = False
                        Return False

                End Select
            Loop
            Return True
        Catch ex As Exception
            cActionResultCfg.Result = False
            cActionResultCfg.ErrorMessage = ex.Message
            cActionResultCfg.ErrorType = enumScrewErrorType.UnKnownError.ToString
            cActionResultCfg.MainErrorType = enumMainScrewErrorType.ScrewError.ToString
            cActionResultCfg.ErrorCode = cMachineManager.ActionParameterManager.GetActionParameterErrorCode("ManualStationScrew", cActionResultCfg.ErrorType, 0)
            Return False
        End Try
    End Function

    Public Overrides Function PassRun(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean
        Return True
    End Function

End Class


Public Enum enumMainScrewErrorType
    ScrewError = 1
End Enum
Public Enum enumScrewErrorType
    TIME_LIMIT = 1
    ANGLE_LOW = 2
    ANGLE_HIGH = 3
    TORQUE_LOW = 4
    TORQUE_HIGH = 5
    PU_PROG = 6
    DRIVER_IO = 7
    START = 8
    INVALID_PROG = 9
    INTERRUPT = 10
    OVERCURRENT = 11
    ERROR_TH = 12
    ERROR_CRC = 13
    SLINIT = 14
    ERROR_POSH = 15
    FRICT_LOW = 16
    FRICT_HIGH = 17
    IIT_ERROR = 18
    PG_NUM = 19
    TOTAL_TIME = 22
    GEN_FAULT = 23
    UnKnownError = 24
End Enum

Public Enum enumMESParameter
    UniqueID = 0
    MATERIAL
    SFC
    Material_HU
    Control_HU
    TS
    RECIPE
    STATUS
    TIMESTAMP
    STEPID
    STEP1_TORQUE_LL
    STEP1_TORQUE_UL
    STEP1_TORQUE_TAR_VAL
    STEP1_TORQUE_MEA_VAL
    STEP1_ANGLE_LL
    STEP1_ANGLE_UL
    STEP1_ANGLE_TAR_VAL
    STEP1_ANGLE_MEA_VAL
    STEP2_TORQUE_LL
    STEP2_TORQUE_UL
    STEP2_TORQUE_TAR_VAL
    STEP2_TORQUE_MEA_VAL
    STEP2_ANGLE_LL
    STEP2_ANGLE_UL
    STEP2_ANGLE_TAR_VAL
    STEP2_ANGLE_MEA_VAL
    STEP3_TORQUE_LL
    STEP3_TORQUE_UL
    STEP3_TORQUE_TAR_VAL
    STEP3_TORQUE_MEA_VAL
    STEP3_ANGLE_LL
    STEP3_ANGLE_UL
    STEP3_ANGLE_TAR_VAL
    STEP3_ANGLE_MEA_VAL
    RUN_TIME
End Enum