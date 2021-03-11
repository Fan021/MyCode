Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("ScrewXY", "XYZ")>
Public Class clsScrewXY
    Inherits clsHMIXY
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceCfg As clsDeviceCfg
    Private cSystemManager As clsSystemManager
    Private cIniHandler As clsIniHandler
    Private lListPoint As New Dictionary(Of String, clsScrewPointCfg)

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ScrewXY", "3"), enumExceptionType.Crash)
            Return False
        End If
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))

        Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_AutoSpeed")
        If mTempValue = "" Then
            mTempValue = "10"
        End If
        Dim fNewValue As Int16 = CInt(mTempValue)
        If fNewValue <= 0 Then fNewValue = 10
        If fNewValue > 100 Then fNewValue = 100
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMIAutoSpeed", fNewValue)
        mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Configure", "HmiTextBox_Speed")
        If mTempValue = "" Then
            mTempValue = "10"
        End If
        fNewValue = CInt(mTempValue)
        If fNewValue <= 0 Then fNewValue = 10
        If fNewValue > 100 Then fNewValue = 100
        cHMIPLC.WriteAny(lListInitParameter(0) + ".fdHMISpeed", fNewValue)

        lListPoint.Clear()
        lListPoint.Add("Waste Box Position", New clsScrewPointCfg)
        lListPoint.Add("Waiting Position", New clsScrewPointCfg)
        Dim cPoint(lListPoint.Count - 1) As StructPoint
        For i = 0 To lListPoint.Count - 1
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "X")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).X = 0
            Else
                lListPoint(lListPoint.Keys(i)).X = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Y")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Y = 0
            Else
                lListPoint(lListPoint.Keys(i)).Y = Single.Parse(mTempValue)
            End If
            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "ScrewXY" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", lListPoint.Keys(i), "Z")
            If mTempValue = "" Then
                lListPoint(lListPoint.Keys(i)).Z = 0
            Else
                lListPoint(lListPoint.Keys(i)).Z = Single.Parse(mTempValue)
            End If
            cPoint(i) = New StructPoint
            cPoint(i).strHMIName = lListPoint.Keys(i)
            cPoint(i).fdXPosition = lListPoint(lListPoint.Keys(i)).X
            cPoint(i).fdYPosition = lListPoint(lListPoint.Keys(i)).Y
            cPoint(i).fdZPosition = lListPoint(lListPoint.Keys(i)).Z
        Next
        cHMIPLC.WriteAny(lListInitParameter(0) + ".HMI_Point", cPoint)

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


    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(iProgramUI) Then
            iProgramUI.Quit(cLocalElement, cSystemElement)
        End If
        iProgramUI = New ProgramUI
        iProgramUI.ObjectSource = Me
        Return True
    End Function


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Class StructScrewXY
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIXBackward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYForward As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIYBackward As Boolean = False

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIMoveYPosition As Single = 0

    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strHMIAST As String = "0"
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIProg As Int16 = 0
    Public bulHMIScrew As StructScrewXYButton
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMISpeed As Int16 = 0
    <MarshalAs(UnmanagedType.I2, SizeConst:=1)> Public fdHMIAutoSpeed As Int16 = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdHMIStep As Single = 0
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIContinueEnable As Boolean = True
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIMotorEnable As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMISDUEnable As Boolean = False
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMISDUCalibrate As Boolean = False
    Public bulHMIMove As StructScrewXYButton
    Public bulHMIAxisXHome As StructScrewXYButton
    Public bulHMIAxisYHome As StructScrewXYButton
    Public bulHMIAxisXReset As StructScrewXYButton
    Public bulHMIAxisYReset As StructScrewXYButton

    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCXPosition As Single = 0
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdPLCYPosition As Single = 0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10, arraysubtype:=UnmanagedType.Struct)> Public cPoint(0 To 9) As StructPoint
End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructScrewXYButton
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulHMIDoAction As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsPass As Boolean
    <MarshalAs(UnmanagedType.I1, SizeConst:=1)> Public bulPlcActionIsFail As Boolean
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructPoint
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=21)> Public strHMIName As String
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdXPosition As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdYPosition As Single
    <MarshalAs(UnmanagedType.R4, SizeConst:=1)> Public fdZPosition As Single
End Structure
