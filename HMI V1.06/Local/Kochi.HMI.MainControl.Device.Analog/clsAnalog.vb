Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

<clsHMIDeviceNameAttribute("Analog", "Analog")>
Public Class clsAnalog
    Inherits clsHMIDeviceBase
    Private cHMIPLC As clsHMIPLC
    Private _Object As New Object
    Private cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Private cVariantManager As clsVariantManager
    Private lListIO As New Dictionary(Of String, clsParameterCfg)
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Analog", "1"), enumExceptionType.Crash)
            Return False
        End If
        Me.lListInitParameter = lListInitParameter
        CreateControlUI(cLocalElement, cSystemElement)
        CreateInitUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cHMIPLC.AddAdsVariable(lListInitParameter(0))
        AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
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

    Public Sub VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        If Not IsNothing(cHMIPLC) Then
            lListIO.Clear()
            Dim mTempValue As String = ""
            For j = 0 To 19
                Dim cIOCfg As New clsParameterCfg
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Text")
                cIOCfg.strText = mTempValue
                If cIOCfg.strText = "" Then
                    cIOCfg.strText = "Parameter:" + (j + 1).ToString
                End If

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Index")
                If mTempValue = "" Then
                    cIOCfg.iIndex = j
                Else
                    cIOCfg.iIndex = CInt(mTempValue)
                End If

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "ReadOnly")
                cIOCfg.bReadOnly = IIf(mTempValue = "True", True, False)

                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "VariantValue")
                cIOCfg.bVariantValue = IIf(mTempValue = "True", True, False)
                cIOCfg.ID = "Parameter" + j.ToString
                lListIO.Add("Parameter" + j.ToString, cIOCfg)
            Next
            WriteValue()
        End If
    End Sub

    Private Sub WriteValue()
        For Each cIOCfg As clsParameterCfg In lListIO.Values
            Dim strValue As String = ""
            If Not cIOCfg.bReadOnly Then
                If cIOCfg.bVariantValue Then
                    If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                        strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cVariantManager.CurrentVariantCfg.Variant, cIOCfg.iIndex.ToString)
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                    Else
                        cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                    End If
                Else
                    strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "Analog" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Value")
                    cHMIPLC.WriteAny(lListInitParameter(0) + ".strParameter[" + (cIOCfg.iIndex + 1).ToString + "]", strValue, New Integer() {strValue.Length})
                End If
            End If


        Next
    End Sub


End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructAnalogParameter
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=31)> Public strParameter As String
End Structure

Public Class clsParameterCfg
    Public cLabel As Label
    Public cText As HMITextBox
    Public iIndex As Integer
    Public strText As String = String.Empty
    Public ID As String = String.Empty
    Public bReadOnly As Boolean = False
    Public bVariantValue As Boolean = False
End Class
