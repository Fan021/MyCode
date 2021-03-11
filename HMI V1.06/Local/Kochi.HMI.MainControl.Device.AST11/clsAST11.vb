Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice
Imports System.Threading

<clsHMIDeviceNameAttribute("AST11", "AST")>
Public Class clsAST11
    Inherits clsHMIAST
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Protected cDataProcess As DEPRAG_DataProcess
    Private ListProgram As New Dictionary(Of Integer, Boolean)
    Protected TimerCB As New TimerCallback(AddressOf _TimerCB)
    Protected _Timer As New System.Threading.Timer(TimerCB, Nothing, Timeout.Infinite, Timeout.Infinite)
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "1"), enumExceptionType.Crash)
            Return False
        End If
        Me.lListInitParameter = lListInitParameter
        CreateControlUI(cLocalElement, cSystemElement)
        CreateInitUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(1))
        If lListInitParameter(0) <> "" Then
            cDataProcess = New DEPRAG_DataProcess
            If Not cDataProcess.InitController(lListInitParameter(0)) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "5", lListInitParameter(0)), enumExceptionType.Alarm)
            End If
        End If

        For i = 1 To 16
            ListProgram.Add(i, False)
            GetWebValue(i)
        Next
        _Timer.Change(1000, Timeout.Infinite)
        Return True
    End Function

    Protected Sub _TimerCB(ByVal state As Object)
        SyncLock _Object
            _Timer.Change(Timeout.Infinite, Timeout.Infinite)
            If CheckIE() Then
                For i = 1 To 16
                    ListProgram(i) = False
                Next
            End If

            _Timer.Change(1000, Timeout.Infinite)
        End SyncLock
    End Sub

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iProgramUI) Then
                iProgramUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iInitUI) Then
                iInitUI.Quit(cLocalElement, cSystemElement)
            End If
            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then
            RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        MyBase.CreateShortcutUI(cLocalElement, cSystemElement)
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then
            RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iInitUI.Quit(cLocalElement, cSystemElement)
        End If
        iInitUI = New InitUI
        AddHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function GetASTValue() As clsASTCfg
        Dim cStructAST As StructAST = cHMIPLC.ReadAny(lListInitParameter(1), GetType(StructAST))
        Dim cASTCfg As New clsASTCfg
        cASTCfg.strIP = lListInitParameter(0)
        cASTCfg.fdAngle1 = cStructAST.fdAngle1
        cASTCfg.fdAngle2 = cStructAST.fdAngle2
        cASTCfg.fdAngle3 = cStructAST.fdAngle3
        cASTCfg.fdAngleNOk = cStructAST.fdAngleNOk
        cASTCfg.fdProg = cStructAST.fdProg
        cASTCfg.fdStatus = cStructAST.fdStatus
        cASTCfg.fdStep1 = cStructAST.fdStep1
        cASTCfg.fdStep2 = cStructAST.fdStep2
        cASTCfg.fdStep3 = cStructAST.fdStep3
        cASTCfg.fdStepNOk = cStructAST.fdStepNOk
        cASTCfg.fdTime = cStructAST.fdTime
        cASTCfg.fdTorque1 = cStructAST.fdTorque1
        cASTCfg.fdTorque2 = cStructAST.fdTorque2
        cASTCfg.fdTorque3 = cStructAST.fdTorque3
        cASTCfg.fdTorqueNOk = cStructAST.fdTorqueNOk
        cASTCfg.fdTorqueUnit = cStructAST.fdTorqueUnit
        cASTCfg.strTorqueUnit = ChangeTorqueUnit(cASTCfg.fdTorqueUnit)

        Return cASTCfg
    End Function
    Public Overrides Function ChangeASTValue(ByVal cASTCfg As clsASTCfg) As clsASTCfg
        Dim cTempASTCfg As New clsASTCfg
        cTempASTCfg = cASTCfg.Clone
        If cASTCfg.fdProg = 0 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "8", enumExceptionType.Alarm))
        End If

        If lListInitParameter(0) <> "" Then
            If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo) Then
                'step1
                If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info) Then
                    If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Type = ClsDataProcess.StepType.LA Or cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Type = ClsDataProcess.StepType.FA Then
                        cTempASTCfg.fdAngleLow1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Shut_off_angle
                        cTempASTCfg.fdTorqueLow1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget1 = 0
                    Else
                        cTempASTCfg.fdAngleLow1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget1 = 0
                        cTempASTCfg.fdTorqueLow1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget1 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).Info.Shut_off_torque
                    End If
                End If

                'step2
                If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info) Then
                    If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Type = ClsDataProcess.StepType.LA Or cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Type = ClsDataProcess.StepType.FA Then
                        cTempASTCfg.fdAngleLow2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Shut_off_angle
                        cTempASTCfg.fdTorqueLow2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget2 = 0
                    Else
                        cTempASTCfg.fdAngleLow2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget2 = 0
                        cTempASTCfg.fdTorqueLow2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget2 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).Info.Shut_off_torque
                    End If
                End If

                'step3
                If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info) Then
                    If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Type = ClsDataProcess.StepType.LA Or cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Type = ClsDataProcess.StepType.FA Then
                        cTempASTCfg.fdAngleLow3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Shut_off_angle
                        cTempASTCfg.fdTorqueLow3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget3 = 0
                    Else
                        cTempASTCfg.fdAngleLow3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Minimum_angle
                        cTempASTCfg.fdAngleUp3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Maximum_angle
                        cTempASTCfg.fdAngleTarget3 = 0
                        cTempASTCfg.fdTorqueLow3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Minimum_torque
                        cTempASTCfg.fdTorqueUp3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Maximum_torque
                        cTempASTCfg.fdTorqueTarget3 = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).Info.Shut_off_torque
                    End If
                End If

                'stepNok
                If cTempASTCfg.fdStepNOk > 0 Then
                    Dim iCnt As Integer = 0
                    If cTempASTCfg.fdStepNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).index Then
                        iCnt = 0
                        If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info) Then
                            cTempASTCfg.fdAngleLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_angle
                            cTempASTCfg.fdAngleUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_angle
                            If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Type = ClsDataProcess.StepType.FT Then
                                cTempASTCfg.fdAngleTargetNOk = 0
                                cTempASTCfg.fdTorqueTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_torque
                            Else
                                cTempASTCfg.fdAngleTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_angle
                                cTempASTCfg.fdTorqueTargetNOk = 0
                            End If

                            cTempASTCfg.fdTorqueLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_torque
                            cTempASTCfg.fdTorqueUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_torque

                        End If
                    ElseIf cTempASTCfg.fdStepNOk > cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).index And cTempASTCfg.fdStepNOk < cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).index Then
                        iCnt = 0
                        If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info) Then
                            cTempASTCfg.fdAngleLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_angle
                            cTempASTCfg.fdAngleUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_angle
                            If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Type = ClsDataProcess.StepType.FT Then
                                cTempASTCfg.fdAngleTargetNOk = 0
                                cTempASTCfg.fdTorqueTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_torque
                            Else
                                cTempASTCfg.fdAngleTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_angle
                                cTempASTCfg.fdTorqueTargetNOk = 0
                            End If
                            cTempASTCfg.fdTorqueLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_torque
                            cTempASTCfg.fdTorqueUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_torque

                        End If
                    ElseIf cTempASTCfg.fdStepNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).index Then
                        iCnt = 1
                        If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info) Then
                            cTempASTCfg.fdAngleLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_angle
                            cTempASTCfg.fdAngleUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_angle
                            If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Type = ClsDataProcess.StepType.FT Then
                                cTempASTCfg.fdAngleTargetNOk = 0
                                cTempASTCfg.fdTorqueTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_torque
                            Else
                                cTempASTCfg.fdAngleTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_angle
                                cTempASTCfg.fdTorqueTargetNOk = 0
                            End If
                            cTempASTCfg.fdTorqueLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_torque
                            cTempASTCfg.fdTorqueUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_torque

                        End If
                    ElseIf cTempASTCfg.fdStepNOk > cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(1).index And cTempASTCfg.fdStepNOk < cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).index Then
                        iCnt = 1
                        If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info) Then
                            cTempASTCfg.fdAngleLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_angle
                            cTempASTCfg.fdAngleUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_angle
                            If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Type = ClsDataProcess.StepType.FT Then
                                cTempASTCfg.fdAngleTargetNOk = 0
                                cTempASTCfg.fdTorqueTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_torque
                            Else
                                cTempASTCfg.fdAngleTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_angle
                                cTempASTCfg.fdTorqueTargetNOk = 0
                            End If
                            cTempASTCfg.fdTorqueLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_torque
                            cTempASTCfg.fdTorqueUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_torque

                        End If
                    ElseIf cTempASTCfg.fdStepNOk >= cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(2).index Then
                        iCnt = 2
                        If Not IsNothing(cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info) Then
                            cTempASTCfg.fdAngleLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_angle
                            cTempASTCfg.fdAngleUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_angle
                            If cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Type = ClsDataProcess.StepType.FT Then
                                cTempASTCfg.fdAngleTargetNOk = 0
                                cTempASTCfg.fdTorqueTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_torque
                            Else
                                cTempASTCfg.fdAngleTargetNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Shut_off_angle
                                cTempASTCfg.fdTorqueTargetNOk = 0
                            End If
                            cTempASTCfg.fdTorqueLowNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Minimum_torque
                            cTempASTCfg.fdTorqueUpNOk = cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(iCnt).Info.Maximum_torque

                        End If
                    End If
                End If

            End If

        End If



        If cTempASTCfg.fdStep1 = 0 Then
            cTempASTCfg.fdStep1 = cTempASTCfg.fdStep2
            cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngle2
            cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorque2
            cTempASTCfg.fdStep2 = cTempASTCfg.fdStep3
            cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngle3
            cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorque3
            cTempASTCfg.fdStep3 = 0
            cTempASTCfg.fdAngle3 = 0
            cTempASTCfg.fdTorque3 = 0
        End If
        If cTempASTCfg.fdStep1 = 0 Then
            cTempASTCfg.fdStep1 = cTempASTCfg.fdStep2
            cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngle2
            cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorque2
            cTempASTCfg.fdStep2 = cTempASTCfg.fdStep3
            cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngle3
            cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorque3
            cTempASTCfg.fdStep3 = 0
            cTempASTCfg.fdAngle3 = 0
            cTempASTCfg.fdTorque3 = 0
        End If

        If lListInitParameter(0) <> "" Then
            If cTempASTCfg.fdStep1 < cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).index Then
                cTempASTCfg.fdStep1 = cTempASTCfg.fdStep2
                cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngle2
                cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorque2
                cTempASTCfg.fdStep2 = cTempASTCfg.fdStep3
                cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngle3
                cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorque3
                cTempASTCfg.fdStep3 = 0
                cTempASTCfg.fdAngle3 = 0
                cTempASTCfg.fdTorque3 = 0
            End If
            If cTempASTCfg.fdStep1 < cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).index Then
                cTempASTCfg.fdStep1 = cTempASTCfg.fdStep2
                cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngle2
                cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorque2
                cTempASTCfg.fdStep2 = cTempASTCfg.fdStep3
                cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngle3
                cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorque3
                cTempASTCfg.fdStep3 = 0
                cTempASTCfg.fdAngle3 = 0
                cTempASTCfg.fdTorque3 = 0
            End If

            If cTempASTCfg.fdStep1 < cDataProcess.m_DataProcess.m_ProgrammeStepInfo(cTempASTCfg.fdProg - 1).m_stepInfo(0).index Then
                cTempASTCfg.fdStep1 = cTempASTCfg.fdStep2
                cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngle2
                cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorque2
                cTempASTCfg.fdStep2 = cTempASTCfg.fdStep3
                cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngle3
                cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorque3
                cTempASTCfg.fdStep3 = 0
                cTempASTCfg.fdAngle3 = 0
                cTempASTCfg.fdTorque3 = 0
            End If
        End If


        If cTempASTCfg.fdStatus = 0 Then
            If cTempASTCfg.fdStep3 <> 0 Then
                cTempASTCfg.fdStepNOk = cTempASTCfg.fdStep3
                cTempASTCfg.fdAngleNOk = cTempASTCfg.fdAngle3
                cTempASTCfg.fdTorqueNOk = cTempASTCfg.fdTorque3
                cTempASTCfg.fdAngleLowNOk = cTempASTCfg.fdAngleLow3
                cTempASTCfg.fdAngleUpNOk = cTempASTCfg.fdAngleUp3
                cTempASTCfg.fdAngleTargetNOk = cTempASTCfg.fdAngleTarget3
                cTempASTCfg.fdTorqueLowNOk = cTempASTCfg.fdTorqueLow3
                cTempASTCfg.fdTorqueUpNOk = cTempASTCfg.fdTorqueUp3
                cTempASTCfg.fdTorqueTargetNOk = cTempASTCfg.fdTorqueTarget3
                cTempASTCfg.fdStep = cTempASTCfg.fdStep3
            End If

            If cTempASTCfg.fdStep3 = 0 And cTempASTCfg.fdStep2 <> 0 Then
                cTempASTCfg.fdStepNOk = cTempASTCfg.fdStep2
                cTempASTCfg.fdAngleNOk = cTempASTCfg.fdAngle2
                cTempASTCfg.fdTorqueNOk = cTempASTCfg.fdTorque2

                cTempASTCfg.fdAngleLowNOk = cTempASTCfg.fdAngleLow2
                cTempASTCfg.fdAngleUpNOk = cTempASTCfg.fdAngleUp2
                cTempASTCfg.fdAngleTargetNOk = cTempASTCfg.fdAngleTarget2
                cTempASTCfg.fdTorqueLowNOk = cTempASTCfg.fdTorqueLow2
                cTempASTCfg.fdTorqueUpNOk = cTempASTCfg.fdTorqueUp2
                cTempASTCfg.fdTorqueTargetNOk = cTempASTCfg.fdTorqueTarget2
                cTempASTCfg.fdStep = cTempASTCfg.fdStep2
            End If

            If cTempASTCfg.fdStep3 = 0 And cTempASTCfg.fdStep2 = 0 And cTempASTCfg.fdStep1 <> 0 Then
                cTempASTCfg.fdStepNOk = cTempASTCfg.fdStep1
                cTempASTCfg.fdAngleNOk = cTempASTCfg.fdAngle1
                cTempASTCfg.fdTorqueNOk = cTempASTCfg.fdTorque1

                cTempASTCfg.fdAngleLowNOk = cTempASTCfg.fdAngleLow1
                cTempASTCfg.fdAngleUpNOk = cTempASTCfg.fdAngleUp1
                cTempASTCfg.fdAngleTargetNOk = cTempASTCfg.fdAngleTarget1
                cTempASTCfg.fdTorqueLowNOk = cTempASTCfg.fdTorqueLow1
                cTempASTCfg.fdTorqueUpNOk = cTempASTCfg.fdTorqueUp1
                cTempASTCfg.fdTorqueTargetNOk = cTempASTCfg.fdTorqueTarget1
                cTempASTCfg.fdStep = cTempASTCfg.fdStep1
            End If

        Else
            If cTempASTCfg.fdStep1 = 0 Or cTempASTCfg.fdStep1 = cTempASTCfg.fdStepNOk Then
                cTempASTCfg.fdStep1 = cTempASTCfg.fdStepNOk
                cTempASTCfg.fdAngle1 = cTempASTCfg.fdAngleNOk
                cTempASTCfg.fdTorque1 = cTempASTCfg.fdTorqueNOk
            ElseIf cTempASTCfg.fdStep1 <> 0 And (cTempASTCfg.fdStep2 = 0 Or cTempASTCfg.fdStep2 = cTempASTCfg.fdStepNOk) Then
                cTempASTCfg.fdStep2 = cTempASTCfg.fdStepNOk
                cTempASTCfg.fdAngle2 = cTempASTCfg.fdAngleNOk
                cTempASTCfg.fdTorque2 = cTempASTCfg.fdTorqueNOk
            ElseIf cTempASTCfg.fdStep1 <> 0 And cTempASTCfg.fdStep2 <> 0 And (cTempASTCfg.fdStep3 = 0 Or cTempASTCfg.fdStep3 = cTempASTCfg.fdStepNOk) Then
                cTempASTCfg.fdStep3 = cTempASTCfg.fdStepNOk
                cTempASTCfg.fdAngle3 = cTempASTCfg.fdAngleNOk
                cTempASTCfg.fdTorque3 = cTempASTCfg.fdTorqueNOk
            End If
            cTempASTCfg.fdStep = cTempASTCfg.fdStepNOk
        End If


        Return cTempASTCfg
    End Function

    Public Overrides Function ChangeTorqueUnit(ByVal iIndex As Integer) As String

        Select Case iIndex
            Case 0
                Return "Nm"
            Case 1
                Return "Ncm"
            Case 2
                Return "ft-lb"
            Case 3
                Return "in-lb"
            Case 4
                Return "kg-m"
            Case 5
                Return "kg-cm"
            Case Else
                Return "Nm"
        End Select
    End Function

    Public Overrides Function WriteProgram(ByVal iProgram As Integer) As Boolean
        cHMIPLC.WriteAny(lListInitParameter(1) + ".ProgramNo", Int16.Parse(iProgram))
        Return True
    End Function

    Public Overrides Function GetWebValue(ByVal iProgram As Integer) As Boolean
        SyncLock _Object
            If lListInitParameter(0) <> "" And (Not ListProgram(iProgram)) Then
                Dim iCnt As Integer = 0
                iCnt = cDataProcess.LoadProgram(iProgram)
                If iCnt = -2 Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "9", lListInitParameter(0)), enumExceptionType.Alarm)
                End If
                If iCnt <> 0 Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "7", lListInitParameter(0)), enumExceptionType.Alarm)
                End If
                If Not cDataProcess.ChangeParameter(iProgram) Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "7", lListInitParameter(0)), enumExceptionType.Alarm)
                End If
                ListProgram(iProgram) = True
            End If
            Return True
        End SyncLock
    End Function


    Public Function CheckIE() As Boolean
        Return False
        'Dim _ProcessList As Process() = Process.GetProcesses
        'Dim _OneProcess As Process
        'Dim _FileString As String = String.Empty
        'Dim _FolderString As String = String.Empty
        'Dim _FileCount As Integer = 0
        'Dim mName As String = ""

        ''For Each _OneProcess In _ProcessList
        ''    Try
        ''        mName = _OneProcess.MainModule.ModuleName.ToUpper
        ''    Catch ex As Exception
        ''        mName = ""
        ''    End Try

        ''    If mName.ToLower = "iexplore.exe" Then
        ''        Return True
        ''    End If
        ''Next

        'Return False

    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iProgramUI) Then
            iProgramUI.Quit(cLocalElement, cSystemElement)
        End If
        iProgramUI = New ProgramUI
        iProgramUI.ObjectSource = Me
        Return True
    End Function

End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructAST
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public Start As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public [Stop] As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public AutoStop As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public Reload As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public StartRelease As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public ProgIn2 As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public DataSaved As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public ProgramNo As Int16 = 0

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public SystemOK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public Ready As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public OK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public NOK As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public ProgOut8 As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public Heartbeat As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public DataAvailable As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public SysStatus As Int16 = 0

    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdProg As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdStatus As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdTime As Single = 0

    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdStep1 As Int32 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdTorque1 As Single = 0
    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdAngle1 As Int32 = 0

    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdStep2 As Int32 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdTorque2 As Single = 0
    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdAngle2 As Int32 = 0

    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdStep3 As Int32 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdTorque3 As Single = 0
    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdAngle3 As Int32 = 0

    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdStepNOk As Int32 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdTorqueNOk As Single = 0
    <MarshalAs(UnmanagedType.I4, SizeConst:=1)> Public fdAngleNOk As Int32 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdTorqueUnit As Int16 = 0

End Class
