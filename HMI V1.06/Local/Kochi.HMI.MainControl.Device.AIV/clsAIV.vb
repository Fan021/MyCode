Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("AIV", "AIV")>
Public Class clsAIV
    Inherits clsHMIDeviceBase
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
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AIV", "3"), enumExceptionType.Crash)
            Return False
        End If
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))

        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "AIV" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "SysMode")
        If mTempValue = "" Then
            mTempValue = "1"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".SysMode", Int16.Parse(mTempValue))


        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "AIV" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "BeltDelalyTime")
        If mTempValue = "" Then
            mTempValue = "1000"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".BeltDelalyTime", Int16.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "AIV" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "AIVWaitTime")
        If mTempValue = "" Then
            mTempValue = "1000"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".AIVWaitTime", Int16.Parse(mTempValue))


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
Public Class StructAIV
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public SysMode As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public BeltDelayTime As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public AIVWaitTime As Int16 = 0
End Class
