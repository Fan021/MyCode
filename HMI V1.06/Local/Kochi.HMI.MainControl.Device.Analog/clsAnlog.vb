Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("Analog", "AST")>
Public Class clsAnlog
    Inherits clsHMIDeviceBase
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Analog", "1"), enumExceptionType.Crash)
            Return False
        End If
        Me.lListInitParameter = lListInitParameter
        CreateControlUI(cLocalElement, cSystemElement)
        CreateInitUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        Return True
    End Function

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



    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
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
