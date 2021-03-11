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

<clsHMIDeviceNameAttribute("LineControl", "LC")>
Public Class clsLineControl
    Inherits clsHMILineControl

    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cSystemManager As clsSystemManager
    Private isRunning As Boolean = False
    Private LR As New Linecontroller.clsDB_Linecontrolling
    Private strPreviousTest As String = String.Empty
    Private strCurrentTest As String = String.Empty
    Private cIniHandler As New clsIniHandler

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        Open()
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

    Public Overrides Function CreateShortcutUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
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


    Public Function Open() As Boolean
        Try
            If lListInitParameter(3) = "FALSE" Then Return True
            LR.INIFile = lListInitParameter(0)
            LR.INISection = lListInitParameter(1)
            LR.TraceID = lListInitParameter(2)
            Dim lResult As Integer = LR.Init
            If lResult <> 0 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("LineControl", "5", cLanguageManager.GetUserTextLine("LineControl_Init", lResult), lResult), enumExceptionType.Alarm)
            End If
            strPreviousTest = cIniHandler.ReadIniFile(lListInitParameter(0), lListInitParameter(1), "PREVIOUS_TEST").Split(" ")(0).Split(vbTab)(0)
            strCurrentTest = cIniHandler.ReadIniFile(lListInitParameter(0), lListInitParameter(1), "CURRENT_TEST").Split(" ")(0).Split(vbTab)(0)
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
    Public Overrides Function AddChild(ByVal iIndex As Integer, ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String) As Boolean
        cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\" + Name + ".ini", "Child" + iIndex.ToString, "Variant", strVariant)
        cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\" + Name + ".ini", "Child" + iIndex.ToString, "SN", strSN)
        Return True
    End Function

    Public Overloads Overrides Function Complete(ByVal strSN As String, ByVal strVariant As String, ByVal bResult As Boolean, ByRef strResult As String) As Boolean
        If lListInitParameter(3) = "FALSE" Then Return True
        Dim bFindChild As Boolean = False
        For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.LogFolder + "\" + Name + ".ini", "Child", New String() {"Variant", "SN"})
            Dim mStrVariant As String = ""
            Dim mStrSN As String = ""
            If CType(element("Variant"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Variant"), String) <> clsXmlHandler.s_Null Then
                mStrVariant = CType(element("Variant"), String)
            End If
            If CType(element("SN"), String) <> clsXmlHandler.s_DEFAULT And CType(element("SN"), String) <> clsXmlHandler.s_Null Then
                mStrSN = CType(element("SN"), String)
            End If
            LR.articleNo = mStrVariant
            LR.serialNo = mStrSN
            LR.PrepareForChild()
            bFindChild = True
        Next

        LR.articleNo = strVariant
        LR.serialNo = strSN
        LR.CurrentTest = strCurrentTest
        LR.TraceID = lListInitParameter(2)
        LR.TestResult = bResult
        Dim lResult As Integer = LR.WriteCurrentStamp()
        If bFindChild And lResult = 0 Then
            lResult = LR.MarryAllChildren()
        End If
        If lResult <> 0 Then
            strResult = cLanguageManager.GetUserTextLine("LineControl", "5", cLanguageManager.GetUserTextLine("LineControl_Complete", lResult), lResult)
            Return False
        End If
        Return True
    End Function

    Public Overloads Overrides Function Start(ByVal strSN As String, ByVal strVariant As String, ByRef strResult As String, ByRef iResultErrorId As Integer) As Boolean
        If lListInitParameter(3) = "FALSE" Then Return True
        LR.articleNo = strVariant
        LR.serialNo = strSN
        LR.TraceID = lListInitParameter(2)
        LR.PreviousTest(0) = strPreviousTest
        Dim lResult As Integer = LR.ReadPreviousStamp()
        cIniHandler.RemoveAllSection(cSystemManager.Settings.LogFolder + "\" + Name + ".ini", "Child")

        If lResult <> 0 Then
            iResultErrorId = -1
            strResult = cLanguageManager.GetUserTextLine("LineControl", "5", cLanguageManager.GetUserTextLine("LineControl_Start", lResult), lResult)
            Return False
        End If
        iResultErrorId = 0
        Return True
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function
End Class


