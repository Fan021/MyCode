Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("PKP_Z", "PKP")>
Public Class clsPKP_Z
    Inherits clsHMIPKP_Z


    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager


    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("PKP_Z", "1"), enumExceptionType.Crash)
            Return False
        End If
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        Return True
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iProgramUI) Then
                iProgramUI.Quit(cLocalElement, cSystemElement)
            End If
            If Not IsNothing(iControlUI) Then
                iControlUI.Quit(cLocalElement, cSystemElement)
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

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iProgramUI) Then
            iProgramUI.Quit(cLocalElement, cSystemElement)
        End If
        iProgramUI = New ProgramUI
        iProgramUI.ObjectSource = Me
        Return True
    End Function

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
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

    Public Overrides Function GetXValue() As Double
        SyncLock _Object
            Return cHMIPLC.ReadAny(lListInitParameter(0) + ".fdPLCXPosition", GetType(Single))
        End SyncLock
    End Function

    Public Overrides Function GetYValue() As Double
        SyncLock _Object
            Return cHMIPLC.ReadAny(lListInitParameter(0) + ".fdPLCYPosition", GetType(Single))
        End SyncLock
    End Function

    Public Overrides Function GetZValue() As Double
        SyncLock _Object
            Return cHMIPLC.ReadAny(lListInitParameter(0) + ".fdPLCZPosition", GetType(Single))
        End SyncLock
    End Function


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property ReWorkEnable As Boolean
        Get
            Return True
        End Get
    End Property
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructPKP_Z
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveZPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveXTolerance As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveYTolerance As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveZTolerance As Single = 0

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strHMIAST As String = "0"
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIProg As Int16 = 0

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdPLCXOriginDone As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdPLCYOriginDone As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public fdPLCZOriginDone As Boolean = False
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCZPosition As Single = 0
End Class
