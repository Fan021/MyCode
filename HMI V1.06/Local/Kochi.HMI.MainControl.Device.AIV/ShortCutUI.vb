Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

Public Class ShortCutUI
    Implements IShortcutUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Private cIO As clsAIV
    Private mIOForm As IOForm
    Protected cLanguageManager As clsLanguageManager

    Public Property ObjectSource As Object Implements IShortcutUI.ObjectSource
        Get
            Return cIO
        End Get
        Set(ByVal value As Object)
            cIO = value
        End Set
    End Property

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IDeviceUI.UI
        Get
            Return Panel_Body
        End Get
    End Property


    Public Function InitForm() As Boolean

        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean


        Return True
    End Function

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Init
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        mIOForm = New IOForm
        mIOForm.AIV = cIO
        mIOForm.FontSize = 7
        mIOForm.ReadOnly = False
        mIOForm.Init(cLocalElement, cSystemElement)
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IDeviceUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IShortcutUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        mIOForm.SetParameter(lListInitParameter, lListControlParameter)
        Panel_Body.Controls.Add(mIOForm.Panel_Body)
        Return True
    End Function

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.StartRefresh
        mIOForm.StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IShortcutUI.StopRefresh
        mIOForm.StopRefresh(cLocalElement, cSystemElement)
        Return True
    End Function


End Class