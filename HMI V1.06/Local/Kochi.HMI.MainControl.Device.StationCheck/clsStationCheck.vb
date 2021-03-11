Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO
Imports System.Net
Imports System.Reflection
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Xml.Serialization
Imports Kochi.HMI.MainControl.LocalDevice

<clsHMIDeviceNameAttribute("StationCheck", "LC")>
Public Class clsStationCheck
    Inherits clsHMILineControl

    Private cMySqlAdapter As New clsMySqlAdapter
    Private cMachineManager As clsMachineManager
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cSystemManager As clsSystemManager
    Public cProductionData As clsProductionDataManager
    Private isRunning As Boolean = False
    Private _Object As New Object

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cProductionData = New clsProductionDataManager
        cProductionData.Init(cSystemElement)
        Return True
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            If Not IsNothing(iShortcutUI) Then
                iShortcutUI.Quit(cLocalElement, cSystemElement)
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


    Public Overrides Property Running As Boolean
        Get
            Return isRunning
        End Get
        Set(ByVal value As Boolean)
            isRunning = value
        End Set
    End Property


    Public Function Close() As Boolean
        Try
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function


    Public Overloads Overrides Function Complete(ByVal strSN As String, ByVal strVariant As String, ByVal bResult As Boolean, ByRef strResult As String) As Boolean
        If lListInitParameter(1) = "FALSE" Then Return True
        Return True
    End Function

    Public Overloads Overrides Function Start(ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String, ByRef iResultErrorId As Integer) As Boolean
        If lListInitParameter(1) = "FALSE" Then Return True
        iResultErrorId = cProductionData.CheckStationResult(lListInitParameter(0), strSN, strVariant, strResult)

        If iResultErrorId = -1 Then
            Return False
        End If
        If iResultErrorId = -2 Then 'Not Inqueue
            strResult = cLanguageManager.GetUserTextLine("StationCheck", "4", strVariant, strSN)
            Return False
        End If

        If iResultErrorId = -3 Then 'ESTOP
            strResult = cLanguageManager.GetUserTextLine("StationCheck", "5", strVariant, strSN)
            Return False
        End If

        If iResultErrorId = -4 Then
            Dim strStationName As String = ""
            Dim c As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(lListInitParameter(0))
            strResult = cLanguageManager.GetUserTextLine("StationCheck", "6", strVariant, strSN, c.StationName)
            Return False
        End If

        If iResultErrorId = -5 Then
            Dim c As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(lListInitParameter(0))
            strResult = cLanguageManager.GetUserTextLine("StationCheck", "7", strVariant, strSN, c.StationName)
            Return False
        End If

        If iResultErrorId = -6 Then
            Return False
        End If

        If iResultErrorId = -7 Then 'Abort
            strResult = cLanguageManager.GetUserTextLine("StationCheck", "12", strVariant, strSN)
            Return False
        End If

        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function AddChild(ByVal iIndex As Integer, ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class


