Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

Public MustInherit Class clsHMIPLC
    Inherits clsHMIDeviceBase
    Public MustOverride Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
    Public MustOverride Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Overrides Function CreateProgramUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
    Public MustOverride Function AddAdsVariable(ByVal strVariableName As String) As Boolean
    Public MustOverride Function ReadAny(ByVal strName As String, ByVal Type As Type, Optional ByVal args() As Integer = Nothing) As Object
    Public MustOverride Function WriteAny(ByVal strName As String, ByVal oValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
    Public MustOverride Function AddNotificationEx(ByVal strName As String, ByVal ObjectType As Type, ByVal ObjectDefaultValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
    Public MustOverride Function RemoveNotificationEx(ByVal strName As String) As Boolean
    Public MustOverride Function GetValue(ByVal strName As String) As Object
    Public Event AdsValueChanged(ByVal sender As Object, ByVal e As AdsValueChangedEvent)

    Public Sub AdsValueChanged_Event(ByVal sender As Object, ByVal e As AdsValueChangedEvent)
        RaiseEvent AdsValueChanged(sender, e)
    End Sub
End Class

Public Class AdsValueChangedEvent
    Private strName As String
    Private oSource As Object
    Public Property Name As String
        Set(ByVal value As String)
            strName = value
        End Set
        Get
            Return strName
        End Get
    End Property
    Public Property Source As Object
        Set(ByVal value As Object)
            oSource = value
        End Set
        Get
            Return oSource
        End Get
    End Property

    Sub New(ByVal strName As String, ByVal oSource As Object)
        Me.strName = strName
        Me.oSource = oSource
    End Sub
End Class

#Region "HMI Interface V1.00"
Public Module HMI_PLC_Interface
    Public Const CON_MAXIMUM_PageNumber As Integer = 8
    Public Const HMI_DI_EL1008_AdsName As String = "HMI.PLC_DebugPage.arrDI_EL1008"
    Public Const HMI_DO_EL2008_AdsName As String = "HMI.PLC_DebugPage.arrDO_EL2008"
    Public Const HMI_DI_EP1008_AdsName As String = "HMI.PLC_DebugPage.arrDI_EP1008"
    Public Const HMI_DO_Festo_AdsName As String = "HMI.PLC_DebugPage.arrDO_Festo"
    Public Const HMI_Cylinder_AdsName As String = "HMI.PLC_DebugPage.arrCylinder"
    Public Const HMI_AutoMainFunction_AdsName As String = "HMI.HMI_AutoMainFunction"
    Public Const HMI_PLC_MachineStatus_AdsName As String = "HMI.PLC_MachineStatus"
    Public Const PLC_RequestAction As String = "PLC_RequestAction"
    Public Const HMI_ResponseAction As String = "HMI_ResponseAction"
    Public Const HMI_bytCurrentActionNr As String = "HMI_bytCurrentActionNr"
    Public Const HMI_bytTargetActionNr As String = "HMI_byTargetActionNr"
    Public Const HMI_HmiAction As String = "HMI_HmiAction"
    Public Const HMI_ActionStep As String = "HMI_ActionStep"
    Public Const HMI_VariantInfo As String = "HMI.HMI_VariantInfoPage.HMI_VariantInfo"
    Public Const PLC_AutoButton As String = "HMI.PLC_AutoButtonPage.PLC_AutoButton"
    Public Const PLC_AutoActionStep As String = "PLC_AutoActionStep"
    Public Const PLC_StepStatus As String = "PLC_StepStatus"
    Public Const HMI_DebugButton As String = "HMI_DebugButton"
    Public Const HMI_ProgramButton As String = "HMI_ProgramButton"
    Public Const HMI_ProgramCylinderButton As String = "HMI_ProgramCylinderButton"
    Public Const HMI_Station_Error As String = "HMI_Station_Error"
    Public Const HMI_Carrier_Error As String = "HMI_Carrier_Error"
    Public Const HMI_Error As String = "HMI_Error"
    Public Const CON_MAXIMUM_TOTAL_ACTION As Integer = 100
    Public Const CON_MAXIMUM_TOTAL_STATION As Integer = 100
End Module


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructHMIMain
    Public DebugPage As New StructDebugPage
    Public PLC_MachineStatus As New StructMachineStatus
    Public PLC_AutoButtonPage As New StructAutoButtonPage
    Public HMI_VariantInfo As New StructVariantInfo
    Public HMI_AutoMainFunction As New StructMainFunction
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructDebugPage
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public arrDI() As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public arrDO() As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public arrCylinder() As Boolean
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructDebugCylinder
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDOA As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDOB As Boolean = False
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructMainFunction
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPowerON As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAuto As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulManual As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCleanMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDebugMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTeachMode As Boolean = False
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructMainFunction_V1_00
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPowerON As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAuto As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulManual As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCleanMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDebugMode As Boolean = False
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructMachineStatus
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPowerON As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAuto As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulManual As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCleanMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulWork As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAlarm As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulEmergence As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoorOpend As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDebugMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulTeachMode As Boolean = False

    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intErrorID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strErrorMessage As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intMessageID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strMessage As String = ""


End Class


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructMachineStatus_V1_00
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPowerON As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAuto As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulManual As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulReset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulStepBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulCleanMode As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulWork As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulAlarm As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulEmergence As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoorOpend As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDebugMode As Boolean = False

    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intErrorID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strErrorMessage As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intMessageID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strMessage As String = ""


End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructHMIError
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHmiError As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intErrorID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strErrorMessage As String = ""
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructAutoButtonPage
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_STATION, arraysubtype:=UnmanagedType.Struct)> Public PLC_AutoActionParameter(0 To CON_MAXIMUM_TOTAL_STATION - 1) As StructAutoButton
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructAutoButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulFail As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulContinue As Boolean
End Structure


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructVariantInfoPage
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_STATION, arraysubtype:=UnmanagedType.Struct)> Public HMI_VariantInfoPage(0 To CON_MAXIMUM_TOTAL_STATION - 1) As StructVariantInfo
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructVariantInfo
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strKostalNr As String = ""
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=51)> Public strSerialNr As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intCarrierID As Int16 = 0
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructRequestAction
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoPositiveAction As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulDoNegativeAction As Boolean = False
    Public stuActionArticleSet As New StructVariantInfo
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructResponseAction
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulActionIsFail As Boolean = False
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructHmiAction
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHmiDoAction As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCDoAction As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean = False

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHmiDoReworkAction As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoReworkAction As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcReworkActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcReworkActionIsFail As Boolean = False

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHmiActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHmiActionIsFail As Boolean = False
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strHmiActionErrorMessage As String = ""
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructHmiAutoAction
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction1 As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction2 As Boolean = False
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructActionParameter_Char
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strType As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intRepeat As Int16 = 0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20 * 31)> Public strParameter(20 * 31 - 1) As Char
    'Sub New()
    '    For i = 0 To 20 * 21 - 1
    '        strParameter(i) =\
    '    Next
    'End Sub
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructActionParameter_String
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strType As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intRepeat As Int16 = 0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20 * 31)> Public strParameter(0 To 19) As String
    Sub New()
        For i = 0 To 19
            strParameter(i) = ""
        Next
    End Sub
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructActionStep
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=CON_MAXIMUM_TOTAL_ACTION)> Public HMI_ActionParameter(0 To CON_MAXIMUM_TOTAL_ACTION - 1) As StructActionParameter_Char
    Sub New()
        For i = 0 To CON_MAXIMUM_TOTAL_ACTION - 1
            HMI_ActionParameter(i) = New StructActionParameter_Char
        Next
    End Sub
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructPLCAutoActionParameter_Char
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strType As String = ""
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction1 As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction2 As Boolean = False
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20, arraysubtype:=UnmanagedType.Struct)> Public strParameterString(0 To 20 - 1) As ParameterString
    Sub New()
        'For i = 0 To 20 * 21 - 1
        '    strParameter(i) =\
        'Next
    End Sub
End Class

Public Structure ParameterString
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strParameterString As String
End Structure


<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructPLCAutoActionParameter_String
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strType As String = ""
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction1 As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcDoAction2 As Boolean = False
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20 * 31)> Public strParameter(0 To 19) As String
    Sub New()
        For i = 0 To 19
            strParameter(i) = ""
        Next
    End Sub
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructPLCAutoActionStep
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=CON_MAXIMUM_TOTAL_ACTION, arraysubtype:=UnmanagedType.Struct)> Public PLC_AutoActionParameter(0 To CON_MAXIMUM_TOTAL_ACTION - 1) As StructPLCAutoActionParameter_Char
    Sub New()
        For i = 0 To CON_MAXIMUM_TOTAL_ACTION - 1
            PLC_AutoActionParameter(i) = New StructPLCAutoActionParameter_Char
        Next
    End Sub
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructPLCStepStatus
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intStepID As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public intCarrierID As Int16 = 0
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=256)> Public strDescription As String = ""
End Class

Public Class clsPLCAction
    Public Const Name As String = "PLCAction"
    Public HmiAction As New StructHmiAction
    Public HmiAutoAction As New StructHmiAutoAction
    Public ListParmeter As New List(Of Object)
    Public Function DoAction(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal cResult As Boolean)
        Try
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulHmiDoAction", cResult)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DoReWorkAction(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal cResult As Boolean)
        Try
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulHmiDoReworkAction", cResult)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DoPlcAction(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal cResult As Boolean)
        Try
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulHmiDoAction", cResult)
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulHmiDoReworkAction", cResult)
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulPLCDoAction", cResult)
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulPlcActionIsPass", cResult)
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + strStationName + "].bulPlcActionIsFail", cResult)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function WriteSN(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal strSN As String)
        Try
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_VariantInfo + "[" + strStationName + "].strSerialNr", strSN, New Integer() {strSN.Length})
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function WriteAction(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal strAction As String)
        Try
            Dim iIndex As Integer = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_bytCurrentActionNr + "[" + strStationName + "]", GetType(Byte))
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ActionStep + "[" + strStationName + "].HMI_ActionParameter[" + iIndex.ToString + "].strType", strAction, New Integer() {strAction.Length})
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function HMIError(ByVal cHMIPLC As clsHMIPLC, ByVal strStationName As String, ByVal strErrorCode As String) As Boolean
        Try
            If strErrorCode = "" Or strErrorCode = "NONE" Then Return True
            Dim cHMIError As New StructHMIError
            cHMIError.intErrorID = CInt(strErrorCode)
            cHMIError.strErrorMessage = "Station;" + strStationName
            cHMIError.bulHmiError = True
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_Error + "[" + strStationName + "]", cHMIError)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
#End Region