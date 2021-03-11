Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Statistics.Screw
Imports System.Drawing
Imports System.Collections.Concurrent

Public Class ProgramUI
    Implements IProgramUI
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected cLanguageManager As clsLanguageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private mIOForm As IOForm
    Private cChangePage As clsChangePage
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cObjectSource As clsAST11
    Public ReadOnly Property UI As Panel Implements IProgramUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IProgramUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        mIOForm = New IOForm
        mIOForm.AST = cObjectSource
        mIOForm.Init(cLocalElement, cSystemElement)
        cChangePage = New clsChangePage
        cChangePage.Init(cLocalElement, cSystemElement)
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        Panel_Boby_Left.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IProgramUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(mIOForm) Then mIOForm.Quit(cLocalElement, cSystemElement)
        Return True
    End Function


    Public Property ObjectSource As Object Implements IProgramUI.ObjectSource
        Set(ByVal value As Object)
            cObjectSource = value
        End Set
        Get
            Return cObjectSource
        End Get
    End Property

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(mIOForm) Then mIOForm.StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        If Not IsNothing(mIOForm) Then mIOForm.StopRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public ReadOnly Property Cancel As Boolean Implements IProgramUI.Cancel
        Get
            Return False
        End Get
    End Property

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempStructAST As New StructAST
        If lListInitParameter.Count >= 2 Then cHMIPLC.WriteAny(lListInitParameter(1), TempStructAST)
        Return True
    End Function

    Public Function GetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByRef lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.GetParameter
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String), ByVal lListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IProgramUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter

        mIOForm.SetParameter(lListInitParameter, lListControlParameter)
        Panel_Boby_Left.Controls.Add(mIOForm.Panel_Body)
        cChangePage.RegisterManager(Panel_Boby_Left, mIOForm.Panel_Body)
        StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function
End Class