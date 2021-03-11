Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("TapeFeeder", "TapeFeeder")>
Public Class clsTapeFeeder
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
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("TapeFeeder", "3"), enumExceptionType.Crash)
            Return False
        End If
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))

        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "TapeFeeder" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_OffDelay")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_TOR_OffDelay", Single.Parse(mTempValue))

        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "TapeFeeder" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_indexDelay")
        If mTempValue = "" Then
            mTempValue = "0"
        End If
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_TOR_indexDelay", Single.Parse(mTempValue))

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
Public Class StructTOR
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public HMI_TOR_Enable As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public HMI_TOR_Swap As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public HMI_TOR_Start As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public HMI_TOR_Reset As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public HMI_TOR_Ionizer As Boolean = False

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public HMI_TOR_OffDelay As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public HMI_TOR_indexDelay As Single

    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Tape As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_TapePickOff As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Slide_HP_forward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Slide_WP_back As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Downholder_WP_R As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Downholder_HP_R As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Downholder_WP_L As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_Downholder_HP_L As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_IdxNextPart_L As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_i_IdxNextPart_R As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Slide_HP_forward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Slide_WP_back As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Downholder_HP_R As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Downholder_WP_R As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Downholder_HP_L As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_Downholder_WP_L As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_TapeMotorFeeder As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_ReelingMotorUp As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_ReelingMotorDown As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public PLC_o_MotorRelease As Boolean = False
End Class
