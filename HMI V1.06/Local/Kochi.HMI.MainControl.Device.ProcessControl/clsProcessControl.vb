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

<clsHMIDeviceNameAttribute("ProcessControl", "ProcessControl")>
Public Class clsProcessControl
    Inherits clsHMIProcessControl


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
    Public cProcessStationManager As clsProcessStationManager
    Public cProcessSFCManager As clsProcessSFCManager


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
        If Enable Then
            GetIP()
            cProcessStationManager = New clsProcessStationManager
            cProcessStationManager.strIP = strIP
            cProcessStationManager.strUserName = strUserName
            cProcessStationManager.strPassWord = strPassWord
            cProcessStationManager.Init(cSystemElement)
            cProcessStationManager.LoadData()

            cProcessSFCManager = New clsProcessSFCManager
            cProcessSFCManager.strIP = strIP
            cProcessSFCManager.strUserName = strUserName
            cProcessSFCManager.strPassWord = strPassWord
            cProcessSFCManager.Init(cSystemElement)
        End If
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




    Public Function Open() As Boolean
        Try

            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function
    Public Function Close() As Boolean
        Try

            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Overrides Function Start(ByVal strSFC As String, ByRef strResult As String) As Integer
        Try
            If Not Enable Then
                Return 0
            End If
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "TRUE" Then
                Return 0
            End If
            Dim strStation As String = ""
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                If strStation = strOperationId Then
                    Return 0
                Else

                    Dim cValue() As String = lListInitParameter(5).Split(",")
                    For i = 0 To cValue.Length - 1
                        If strStation = cValue(i) Then
                            strResult = cLanguageManager.GetUserTextLine("ProcessControl", "10", strSFC, strOperationId, strStation)
                            Return -5
                        End If
                    Next

                    If lListInitParameter(4) = "" Then
                        strResult = cLanguageManager.GetUserTextLine("ProcessControl", "10", strSFC, strOperationId, strStation)
                        Return -1
                    End If

                    cValue = lListInitParameter(4).Split(",")
                    For i = 0 To cValue.Length - 1
                        If strStation = cValue(i) Then
                            strResult = cLanguageManager.GetUserTextLine("ProcessControl", "10", strSFC, strOperationId, strStation)
                            Return -4
                        End If
                    Next
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "10", strSFC, strOperationId, strStation)
                    Return -1
                End If
            Else
                strResult = cLanguageManager.GetUserTextLine("ProcessControl", "12", strSFC, strOperationId, strStation)
                Return -2
            End If
            Return 0
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return -3

        End Try
    End Function

    Public Overrides Function CreateSFC(ByVal strSFC As String, ByRef strResult As String) As Integer
        Try
            If Not Enable Then
                Return 0
            End If
            Dim strStation As String = ""
            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.MES).ToString.ToUpper = "TRUE" Then
                Return 0
            End If
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                If strStation = strOperationId Then
                    Return 0
                Else
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "10", strSFC, strOperationId, strStation)
                    Return -1
                End If
            End If
            If Not cProcessSFCManager.InSertData(strSFC, strOperationId) Then
                strResult = cLanguageManager.GetUserTextLine("ProcessControl", "11")
                Return -2
            End If
            Return 0
        Catch ex As Exception
            If TypeOf ex Is System.Reflection.TargetInvocationException Then
                strResult = ex.InnerException.Message
            Else
                strResult = ex.Message
            End If
            Return -3

        End Try
    End Function

    Public Overrides Function Complete(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try
            If Not Enable Then
                Return True
            End If
            Dim strStation As String = ""
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                If Not cProcessSFCManager.UpdateData(strSFC, cProcessStationManager.GetNextStation(strOperationId, True)) Then
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "13")
                    Return False
                End If
            Else
                If Not cProcessSFCManager.InSertData(strSFC, cProcessStationManager.GetNextStation(strOperationId, True)) Then
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "11")
                    Return False
                End If
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

    Public Overrides Function logNonConformance(ByVal strSFC As String, ByRef strResult As String) As Boolean
        Try '
            If Not Enable Then
                Return True
            End If
            Dim strStation As String = ""
            If cProcessSFCManager.HasSFC(strSFC, strStation) Then
                If Not cProcessSFCManager.UpdateData(strSFC, cProcessStationManager.GetNextStation(strOperationId, False)) Then
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "13")
                    Return False
                End If
            Else
                If Not cProcessSFCManager.InSertData(strSFC, cProcessStationManager.GetNextStation(strOperationId, False)) Then
                    strResult = cLanguageManager.GetUserTextLine("ProcessControl", "11")
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

    Public Overrides Property Running As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    Public Overrides ReadOnly Property Enable As Boolean
        Get
            Return IIf(lListInitParameter(6) = "TRUE", True, False)
        End Get
    End Property
End Class


Public Class clsProcessStationCfg
    Public StationName As String = ""
    Public PassStationName As String = ""
    Public FailureStationName As String = ""
End Class





