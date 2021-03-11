Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UserDefine
Imports Kochi.HMI.MainControl.LocalDevice


<clsHMIDeviceNameAttribute("Printer", "Printer")>
Public Class clsZebraPrinter
    Inherits clsHMIPrinter


    Private _Object As New Object
    Protected cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Private cPrinter As New Printer.Zebra
    Private isRunning As Boolean
    Private strLastFormatFile As String = String.Empty
    Private strLastPrintFile As String = String.Empty
    Public Overrides Property Running As Boolean
        Get
            Return isRunning
        End Get
        Set(ByVal value As Boolean)
            isRunning = value
        End Set
    End Property

    Public Overrides Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        CreateInitUI(cLocalElement, cSystemElement)
        CreateControlUI(cLocalElement, cSystemElement)
        iInitUI.CheckParameter(cLocalElement, cSystemElement, lListInitParameter)
        cPrinter.InitNetPrinter(lListInitParameter(1), lListInitParameter(0))
        Return (True)
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
        MyBase.CreateShortcutUI(cLocalElement, cSystemElement)
        If Not IsNothing(iShortcutUI) Then
            iShortcutUI.Quit(cLocalElement, cSystemElement)
        End If
        iShortcutUI = New ShortCutUI
        iShortcutUI.ObjectSource = Me
        Return True
    End Function



    Public Overrides Function LoadFormatFile(ByVal strFilePath As String) As Boolean
        If strLastFormatFile = strFilePath Then
            Return True
        End If
        cPrinter.LoadFormatFile(strFilePath)
        strLastFormatFile = strFilePath
        Return True
    End Function

    Public Overrides Function LoadPrintFile(ByVal strFilePath As String) As Boolean
        If strLastPrintFile = strFilePath Then
            Return True
        End If
        cPrinter.LoadPrintFile(strFilePath)
        strLastPrintFile = strFilePath
        Return True
    End Function

    Public Overrides Function PrintLabel(ByVal strName As String) As Boolean
        Dim cName() As String = strName.Split("\")
        Return cPrinter.PrintLabel(cName(cName.Count - 1))
    End Function

    Public Overrides Function SetField(ByVal strName As String, ByVal strFildStart As String, ByVal strFildEnd As String, ByVal strValue As String) As Boolean
        Dim cName() As String = strName.Split("\")
        Return cPrinter.SetField(cName(cName.Count - 1), strFildStart, strFildEnd, strValue)
    End Function

    Public Overrides Function CreateParameterUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Overrides Function CreateProgramUI(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

End Class

