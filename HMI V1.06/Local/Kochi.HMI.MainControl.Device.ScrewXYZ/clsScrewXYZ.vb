Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("ScrewXYZ")>
Public Class clsScrewXYZ
    Inherits clsHMIXYZ
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException("PLC is Nothing, Please Add first", enumExceptionType.Crash)
            Return False
        End If
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        Return True
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try

            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iControlUI) Then RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        Return True
    End Function

    Public Overrides Function CreateInitUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iInitUI) Then RemoveHandler CType(iInitUI, InitUI).ParameterChanged, AddressOf Parameter_ParameterChanged
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

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property ProgramUI As IProgramUI
        Get
            Return Nothing
        End Get
    End Property
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructScrewXYZ
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIZForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIZBackward As Boolean = False

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveZPosition As Single = 0

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strHMIAST As String = ""
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIProg As Int16 = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIScrew As Boolean = False
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMISpeed As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIStep As Single = 0

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCYPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCZPosition As Single = 0
End Class
