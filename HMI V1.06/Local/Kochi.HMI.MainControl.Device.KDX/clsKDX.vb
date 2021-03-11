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

<clsHMIDeviceNameAttribute("KDX", "KDX")>
Public Class clsKDX
    Inherits clsHMIKDX
    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cSystemManager As clsSystemManager
    Private isRunning As Boolean = False
    Private strPreviousTest As String = String.Empty
    Private cIniHandler As New clsIniHandler
    Private m_VisuEL As KDX_VisuEL

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        Me.lListInitParameter = lListInitParameter
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        m_VisuEL = New KDX_VisuEL
        If lListInitParameter(5).ToUpper <> "FALSE" Then
            m_VisuEL.Init(lListInitParameter(0), lListInitParameter(1), lListInitParameter(2), lListInitParameter(3), lListInitParameter(4))
        End If
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

    Public Overrides Sub AddTeststep(ByVal TestStepNumber As String, ByVal StepName As String, ByVal Unit As String, ByVal LowLimit As Double, ByVal UpLimit As Double, ByVal value As Double, ByVal io As Boolean, ByVal StepDuration As Double, ByVal RetryCounter As Integer, ByVal FailureText As String)
        If lListInitParameter(5).ToUpper = "FALSE" Then Return
        m_VisuEL.AddTeststep(TestStepNumber, StepName, Unit, LowLimit, UpLimit, value, io, StepDuration, RetryCounter, FailureText)
    End Sub

    Public Overrides Sub DataSave()
        If lListInitParameter(5).ToUpper = "FALSE" Then Return
        m_VisuEL.DataSave()
    End Sub

    Public Overrides Sub SetInfo(ByVal ArticleNumber As String, ByVal ArticleIndex As Integer, ByVal SerialNum As String)
        If lListInitParameter(5).ToUpper = "FALSE" Then Return
        m_VisuEL.ArticleNum = ArticleNumber
        m_VisuEL.ArticleIndex = ArticleIndex

        m_VisuEL.SaveStart()

        m_VisuEL.SetArticleInfo()
        m_VisuEL.SetDUTInfo(SerialNum)
        m_VisuEL.SetTestInfo(0, False)
        m_VisuEL.AddSaveData()
    End Sub

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

End Class


