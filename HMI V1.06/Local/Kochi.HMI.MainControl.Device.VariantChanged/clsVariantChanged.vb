Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

<clsHMIDeviceNameAttribute("VariantChanged", "VariantChanged")>
Public Class clsVariantChanged
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
    Private cMachineStatusManager As clsMachineStatusManager

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(Me.Name)
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("VariantChanged", "1"), enumExceptionType.Crash)
            Return False
        End If
        Me.lListInitParameter = lListInitParameter
        CreateControlUI(cLocalElement, cSystemElement)
        CreateInitUI(cLocalElement, cSystemElement)
        cVariantManager = cSystemElement(clsVariantManager.Name)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        AddHandler cMachineStatusManager.LocalVariantChanged, AddressOf LocalVariantChanged
        AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
        Return True
    End Function

    Private Sub LocalVariantChanged(ByVal strStationID As String, ByVal cLocalVariant As Kochi.HMI.MainControl.Base.clsVariantManager)
    End Sub



    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try

            RemoveHandler cVariantManager.VariantChanged, AddressOf VariantChanged
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
        Try
            cHMIPLC = cDeviceManager.GetPLCDevice()
            If Not IsNothing(cHMIPLC) Then
                lListIO.Clear()
                Dim mTempValue As String = ""
                For j = 0 To 19
                    Dim cIOCfg As New clsParameterCfg
                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Text")
                    cIOCfg.strText = mTempValue
                    If cIOCfg.strText = "" Then
                        cIOCfg.strText = "Parameter:" + (j + 1).ToString
                    End If

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "Index")
                    If mTempValue = "" Then
                        cIOCfg.iIndex = j
                    Else
                        cIOCfg.iIndex = CInt(mTempValue)
                    End If

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "ReadOnly")
                    cIOCfg.bReadOnly = IIf(mTempValue = "True", True, False)


                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "AdsName")
                    cIOCfg.strAdsName = mTempValue

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "ValueType")
                    cIOCfg.strValueType = mTempValue

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "VariantName")
                    cIOCfg.strVariantName = mTempValue

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "AdsLength")
                    If mTempValue = "" Then mTempValue = "0"
                    cIOCfg.iLength = mTempValue

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", j.ToString, "VariantValue")
                    cIOCfg.bVariantValue = IIf(mTempValue = "True", True, False)
                    cIOCfg.ID = "Parameter" + j.ToString
                    lListIO.Add("Parameter" + j.ToString, cIOCfg)
                Next
                WriteValue()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub WriteValue()
        SyncLock _Object
            For Each cIOCfg As clsParameterCfg In lListIO.Values
                Dim strValue As String = ""
                WriteIO(cIOCfg)
            Next
        End SyncLock
    End Sub

    Private Sub WriteIO(ByVal cIOCfg As clsParameterCfg)
        Dim strValue As String = ""
        If Not cIOCfg.bReadOnly And cIOCfg.strAdsName <> "" Then
            If cIOCfg.bVariantValue Then
                If cVariantManager.CurrentVariantCfg.ListElement.ContainsKey(cIOCfg.strVariantName) Then
                    strValue = cVariantManager.CurrentVariantCfg.ListElement(cIOCfg.strVariantName)
                    WriteAdsValue(cIOCfg.strAdsName, cIOCfg.strValueType, strValue)
                Else
                    WriteAdsValue(cIOCfg.strAdsName, cIOCfg.strValueType, "")
                End If
            Else
                strValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + "VariantChanged" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", cIOCfg.iIndex.ToString, "Value")
                WriteAdsValue(cIOCfg.strAdsName, cIOCfg.strValueType, strValue)
            End If
        End If
    End Sub

    Private Sub WriteAdsValue(ByVal strAdsName As String, ByVal strValueType As String, ByVal strValue As String)
        If strValue = "" Then strValue = "0"
        Select Case strValueType
            Case "Boolean"
                cHMIPLC.WriteAny(strAdsName, IIf(strValue.ToUpper = "TRUE", True, False))
            Case "Integer"
                cHMIPLC.WriteAny(strAdsName, Int16.Parse(strValue))
            Case "Single"
                cHMIPLC.WriteAny(strAdsName, Single.Parse(strValue))
            Case "Double"
                cHMIPLC.WriteAny(strAdsName, Double.Parse(strValue))
            Case "String"
                cHMIPLC.WriteAny(strAdsName, strValue, New Integer() {strValue.Length})
        End Select
    End Sub

    Private Function ReadAdsValue(ByVal strAdsName As String, ByVal strValueType As String, ByVal iLength As Integer) As String
        If strAdsName = "" Then Return ""
        Select Case strValueType
            Case "Boolean"
                Return cHMIPLC.ReadAny(strAdsName, GetType(Boolean)).ToString
            Case "Integer"
                Return cHMIPLC.ReadAny(strAdsName, GetType(Int16)).ToString
            Case "Single"
                Return cHMIPLC.ReadAny(strAdsName, GetType(Single)).ToString
            Case "Double"
                Return cHMIPLC.ReadAny(strAdsName, GetType(Double)).ToString
            Case "String"
                Return cHMIPLC.ReadAny(strAdsName, GetType(String), New Integer() {iLength}).ToString
        End Select
        Return ""
    End Function



End Class

<StructLayout(LayoutKind.Sequential, Pack:=1)>
Public Structure StructVariantChangedParameter
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
    Public strValueType As String = String.Empty
    Public strAdsName As String = String.Empty
    Public strVariantName As String = String.Empty
    Public iLength As Integer
End Class
