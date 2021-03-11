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
Imports Kochi.HMI.MainControl.UI

<clsHMIDeviceNameAttribute("CellControl", "CellControl")>
Public Class clsCellControl
    Inherits clsHMIDeviceBase
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private isRunning As Boolean = False
    Private strResourceId As String = ""
    Private strOperationId As String = ""
    Private strIP As String = ""
    Private strUserName As String = ""
    Private strPassWord As String = ""
    Public cProcessSFCManager As clsCellSFCManager


    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        strOperationId = lListInitParameter(0)
        ' Close()

        GetIP()
        cProcessSFCManager = New clsCellSFCManager
        cProcessSFCManager.strIP = strIP
        cProcessSFCManager.strUserName = strUserName
        cProcessSFCManager.strPassWord = strPassWord
        cProcessSFCManager.Init(cSystemElement)

        Return True
    End Function
    Private Function GetIP() As Boolean
        Try
            strIP = lListInitParameter(1)
            strOperationId = lListInitParameter(0)
            strUserName = lListInitParameter(2)
            strPassWord = lListInitParameter(3)
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
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

    Public Function Start(ByVal strSFC As String, ByRef strResult As String) As Integer
        Try
            Dim strStation As String = ""
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                Dim cValue() As String = lListInitParameter(4).Split(",")
                For i = 0 To cValue.Length - 1
                    If strStation = cValue(i) And cValue(i) <> "" Then
                        Return -1
                    End If
                Next
            End If
            Return 0
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return 0

        End Try
    End Function

    Public Function CreateSFC(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            If cProcessSFCManager.HasSFC(strSFC) Then
                cProcessSFCManager.DeleteSFC(strSFC)
            End If
            If Not cProcessSFCManager.InSertData(strSFC, "") Then
                strResult = cLanguageManager.GetUserTextLine("CellControl", "11")
                Return False
            End If
            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False
        End Try
    End Function

    Public Function Complete(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try

            Return True
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False

        End Try
    End Function

    Public Function logNonConformance(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            Dim strStation As String = ""
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                If Not cProcessSFCManager.UpdateData(strSFC, strOperationId) Then
                    strResult = cLanguageManager.GetUserTextLine("CellControl", "13")
                    Return False
                End If
            Else
                If Not cProcessSFCManager.InSertData(strSFC, strOperationId) Then
                    strResult = cLanguageManager.GetUserTextLine("CellControl", "11")
                    Return False
                End If
            End If
            System.Threading.Thread.Sleep(200)
            Return True

        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return False

        End Try
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

End Class


Public Class clsProcessStationCfg
    Public StationName As String = ""
    Public PassStationName As String = ""
    Public FailureStationName As String = ""
End Class





