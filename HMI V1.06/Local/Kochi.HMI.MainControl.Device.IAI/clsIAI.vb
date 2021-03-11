Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice
<clsHMIDeviceNameAttribute("IAI", "IAI")>
Public Class clsIAI
    Inherits clsHMIIAI
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceCfg As clsDeviceCfg
    Private cSystemManager As clsSystemManager
    Private cIniHandler As clsIniHandler
    Private cVariantManager As clsVariantManager

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        Me.lListInitParameter = lListInitParameter
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("IAI", "3"), enumExceptionType.Crash)
            Return False
        End If
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
        Return True
    End Function

    Public Sub VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
        WritePoint(cVariantManager.CurrentVariantCfg.Variant)
    End Sub

    Public Sub WritePoint(ByVal strVariant As String)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        Dim cPoint(9) As StructIAIPoint
        Dim mTempValue As String = String.Empty
        For i = 1 To 10
            cPoint(i - 1) = New StructIAIPoint
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", strVariant, "Name" + i.ToString)
            cPoint(i - 1).strPointName = mTempValue
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "IAI" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", strVariant, "Point" + i.ToString)
            If mTempValue = "" Then mTempValue = i.ToString
            cPoint(i - 1).fdPoint = mTempValue
            If lListInitParameter.Count > 0 Then
                cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)
            End If
        Next
    End Sub


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
        RemoveHandler cVariantManager.VariantChanged, AddressOf VariantChanged
        If Not IsNothing(iControlUI) Then
            RemoveHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
            iControlUI.Quit(cLocalElement, cSystemElement)
        End If
        iControlUI = New ControlUI
        iControlUI.ObjectSource = Me
        AddHandler CType(iControlUI, ControlUI).ParameterChanged, AddressOf Parameter_ParameterChanged
        AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
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

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructIAI
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIMotorEnable As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMISTP As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bytHMIPosition As Byte = 0
    Public bulHMIMove As StructIAIButton
    Public bulHMIAxisXHome As StructIAIButton
    Public bulHMIAxisXReset As StructIAIButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPLCIAIReady As Boolean = False
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10, arraysubtype:=UnmanagedType.Struct)> Public HMI_Point() As StructIAIPoint
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructIAIButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoAction As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructIAIPoint
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=51)> Public strPointName As String
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdPoint As Int16
End Structure