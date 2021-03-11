Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports TwinCAT.Ads
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent

<clsHMIDeviceNameAttribute("Twincat", "PLC")>
Public Class clsTwincat
    Inherits clsHMIPLC


    Private _Object As New Object
    Protected cTwincatInterface As clsTwincatInterface
    Protected cLanguageManager As clsLanguageManager
    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Try
            Me.cSystemElement = cSystemElement
            CreateInitUI(cLocalElement, cSystemElement)
            CreateControlUI(cLocalElement, cSystemElement)
            iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cTwincatInterface = New clsTwincatInterface
            AddHandler cTwincatInterface.AdsValueChanged, AddressOf AdsValueChanged_Event
            Return (cTwincatInterface.Init(cLocalElement, cSystemElement, lListInitParameter(0), lListInitParameter(1)))
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function

    Public Overrides Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(cTwincatInterface) Then cTwincatInterface.Quit(cSystemElement)
        If Not IsNothing(iInitUI) Then iInitUI.Quit(cLocalElement, cSystemElement)
        If Not IsNothing(iControlUI) Then iControlUI.Quit(cLocalElement, cSystemElement)
        Dispose()
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

    Public Overrides Function CreateControlUI(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function AddAdsVariable(ByVal strVariableName As String) As Boolean
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.AddAdsVariable(strVariableName)
        End SyncLock
    End Function

    Public Overrides Function ReadAny(ByVal strName As String, ByVal Type As System.Type, Optional ByVal args() As Integer = Nothing) As Object
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.ReadAny(strName, Type, args)
        End SyncLock
    End Function

    Public Overrides Function WriteAny(ByVal strName As String, ByVal oValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.WriteAny(strName, oValue, args)
        End SyncLock
    End Function

    Public Overrides Function AddNotificationEx(ByVal strName As String, ByVal ObjectType As Type, ByVal ObjectDefauleValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.AddNotificationEx(strName, ObjectType, ObjectDefauleValue, args)
        End SyncLock
    End Function

    Public Overrides Function RemoveNotificationEx(ByVal strName As String) As Boolean
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.RemoveNotificationEx(strName)
        End SyncLock
    End Function

    Public Overrides Function GetValue(ByVal strName As String) As Object
        SyncLock _Object
            If IsNothing(cTwincatInterface) Then Throw New clsHMIException(cLanguageManager.GetUserTextLine("Twincat", "1"))
            Return cTwincatInterface.GetValue(strName)
        End SyncLock
    End Function


    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

End Class


