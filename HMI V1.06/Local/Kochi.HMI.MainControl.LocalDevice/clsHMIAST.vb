Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device

Public MustInherit Class clsHMIAST
    Inherits clsHMIDeviceBase
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function GetASTValue() As clsASTCfg
    Public MustOverride Function WriteProgram(ByVal iProgram As Integer) As Boolean
    Public MustOverride Function ChangeTorqueUnit(ByVal iIndex As Integer) As String
    Public MustOverride Function GetWebValue(ByVal iProgram As Integer) As Boolean
    Public MustOverride Function ChangeASTValue(ByVal cASTCfg As clsASTCfg) As clsASTCfg
End Class
Public Class clsASTCfg

    Public strIP As String = String.Empty
    Public strTorqueUnit As String = String.Empty
    Public SysStatus As Int16 = 0
    Public fdProg As Int16 = 0
    Public fdStatus As Int16 = 0
    Public fdTime As Single = 0
    Public fdStep As Int32 = 0


    Public fdStep1 As Int32 = 0
    Public fdTorqueLow1 As Single = 0
    Public fdTorqueTarget1 As Single = 0
    Public fdTorque1 As Single = 0
    Public fdTorqueUp1 As Single = 0
    Public fdAngleLow1 As Int32 = 0
    Public fdAngleTarget1 As Int32 = 0
    Public fdAngle1 As Int32 = 0
    Public fdAngleUp1 As Int32 = 0

    Public fdStep2 As Int32 = 0
    Public fdTorqueLow2 As Single = 0
    Public fdTorqueTarget2 As Single = 0
    Public fdTorque2 As Single = 0
    Public fdTorqueUp2 As Single = 0
    Public fdAngleLow2 As Int32 = 0
    Public fdAngleTarget2 As Int32 = 0
    Public fdAngle2 As Int32 = 0
    Public fdAngleUp2 As Int32 = 0

    Public fdStep3 As Int32 = 0
    Public fdTorqueLow3 As Single = 0
    Public fdTorqueTarget3 As Single = 0
    Public fdTorque3 As Single = 0
    Public fdTorqueUp3 As Single = 0
    Public fdAngleLow3 As Int32 = 0
    Public fdAngleTarget3 As Int32 = 0
    Public fdAngle3 As Int32 = 0
    Public fdAngleUp3 As Int32 = 0

    Public fdStepNOk As Int32 = 0
    Public fdTorqueLowNOk As Single = 0
    Public fdTorqueTargetNOk As Single = 0
    Public fdTorqueNOk As Single = 0
    Public fdTorqueUpNOk As Single = 0
    Public fdAngleLowNOk As Int32 = 0
    Public fdAngleTargetNOk As Int32 = 0
    Public fdAngleNOk As Int32 = 0
    Public fdAngleUpNOk As Int32 = 0

    Public fdTorqueUnit As Int16 = 0

    Public Function Clone() As clsASTCfg
        Dim cTempAST As New clsASTCfg
        cTempAST.strIP = strIP
        cTempAST.strTorqueUnit = strTorqueUnit
        cTempAST.SysStatus = SysStatus
        cTempAST.fdProg = fdProg
        cTempAST.fdStatus = fdStatus
        cTempAST.fdTime = fdTime
        cTempAST.fdStep = fdStep


        cTempAST.fdStep1 = fdStep1
        cTempAST.fdTorqueLow1 = fdTorqueLow1
        cTempAST.fdTorqueTarget1 = fdTorqueTarget1
        cTempAST.fdTorque1 = fdTorque1
        cTempAST.fdTorqueUp1 = fdTorqueUp1
        cTempAST.fdAngleLow1 = fdAngleLow1
        cTempAST.fdAngleTarget1 = fdAngleTarget1
        cTempAST.fdAngle1 = fdAngle1
        cTempAST.fdAngleUp1 = fdAngleUp1

        cTempAST.fdStep2 = fdStep2
        cTempAST.fdTorqueLow2 = fdTorqueLow2
        cTempAST.fdTorqueTarget2 = fdTorqueTarget2
        cTempAST.fdTorque2 = fdTorque2
        cTempAST.fdTorqueUp2 = fdTorqueUp2
        cTempAST.fdAngleLow2 = fdAngleLow2
        cTempAST.fdAngleTarget2 = fdAngleTarget2
        cTempAST.fdAngle2 = fdAngle2
        cTempAST.fdAngleUp2 = fdAngleUp2

        cTempAST.fdStep3 = fdStep3
        cTempAST.fdTorqueLow3 = fdTorqueLow3
        cTempAST.fdTorqueTarget3 = fdTorqueTarget3
        cTempAST.fdTorque3 = fdTorque3
        cTempAST.fdTorqueUp3 = fdTorqueUp3
        cTempAST.fdAngleLow3 = fdAngleLow3
        cTempAST.fdAngleTarget3 = fdAngleTarget3
        cTempAST.fdAngle3 = fdAngle3
        cTempAST.fdAngleUp3 = fdAngleUp3

        cTempAST.fdStepNOk = fdStepNOk
        cTempAST.fdTorqueLowNOk = fdTorqueLowNOk
        cTempAST.fdTorqueTargetNOk = fdTorqueTargetNOk
        cTempAST.fdTorqueNOk = fdTorqueNOk
        cTempAST.fdTorqueUpNOk = fdTorqueUpNOk
        cTempAST.fdAngleLowNOk = fdAngleLowNOk
        cTempAST.fdAngleTargetNOk = fdAngleTargetNOk
        cTempAST.fdAngleNOk = fdAngleNOk
        cTempAST.fdAngleUpNOk = fdAngleUpNOk

        cTempAST.fdTorqueUnit = fdTorqueUnit
        Return cTempAST
    End Function

End Class