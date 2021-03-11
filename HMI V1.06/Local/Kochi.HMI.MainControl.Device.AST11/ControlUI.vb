Imports System.Windows.Forms
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Statistics.Screw
Imports System.Drawing
Imports System.Collections.Concurrent

Public Class ControlUI
    Implements IControlUI

    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Protected cLanguageManager As clsLanguageManager
    Protected lListInitParameter As New List(Of String)
    Protected lListControlParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Private mIOForm As IOForm
    Private iChildrenUI As IChildrenUI
    Private cChangePage As clsChangePage
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As Dictionary(Of String, Object)
    Private cObjectSource As clsAST11
    Public ReadOnly Property UI As Panel Implements IControlUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IControlUI.Init
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
        Panel_Boby_Right.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormRight)
        Panel_Boby_Left.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        MainRightButton_Web.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Web"), "Web")
        MainRightButton_Data.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Data"), "Data")
        MainRightButton_Back.RegisterButton(cLanguageManager.GetUserTextLine("AST11", "Back"), "Back")
        AddHandler MainRightButton_Web.MainButton.Click, AddressOf MainButton_Click
        AddHandler MainRightButton_Data.MainButton.Click, AddressOf MainButton_Click
        AddHandler MainRightButton_Back.MainButton.Click, AddressOf MainButton_Click
        Return True
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Boby_Right.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)

    End Sub

    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Web"
                System.Diagnostics.Process.Start("explorer.exe", "http://" + lListInitParameter(0))
            Case "Data"
                If Not IsNothing(iChildrenUI) Then iChildrenUI.Quit(cLocalElement, cSystemElement)
                iChildrenUI = New ChildrenScrewForm
                iChildrenUI.Init(cLocalElement, cSystemElement)
                cChangePage.ChangePage(iChildrenUI.UI)
            Case "Back"
                If Not IsNothing(iChildrenUI) Then iChildrenUI.Quit(cLocalElement, cSystemElement)
                cChangePage.BackPage()
        End Select
        MainRightButton_Web.SetIndicateBackColor(sender.name)
        MainRightButton_Data.SetIndicateBackColor(sender.name)
        MainRightButton_Back.SetIndicateBackColor(sender.name)
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IControlUI.Quit
        StopRefresh(cLocalElement, cSystemElement)
        If Not IsNothing(mIOForm) Then mIOForm.Quit(cLocalElement, cSystemElement)
        If Not IsNothing(iChildrenUI) Then iChildrenUI.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean Implements IControlUI.SetParameter
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter

        mIOForm.SetParameter(lListInitParameter, lListControlParameter)
        Panel_Boby_Left.Controls.Add(mIOForm.Panel_Body)
        cChangePage.RegisterManager(Panel_Boby_Left, mIOForm.Panel_Body)
        Return True
    End Function

    Public Function CheckInitParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IControlUI.CheckParameter
        Return True
    End Function

    Public Property ObjectSource As Object Implements IControlUI.ObjectSource
        Set(ByVal value As Object)
            cObjectSource = value
        End Set
        Get
            Return cObjectSource
        End Get
    End Property

    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StartRefresh
        If Not IsNothing(mIOForm) Then mIOForm.StartRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements IControlUI.StopRefresh
        If Not IsNothing(mIOForm) Then mIOForm.StopRefresh(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function CloseIO(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListInitParameter As System.Collections.Generic.List(Of String), ByVal lListControlParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IControlUI.CloseIO
        Dim cHMIPLC As clsHMIPLC
        Dim cDeviceManager As clsDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cHMIPLC = cDeviceManager.GetPLCDevice
        Dim TempStructAST As New StructAST
        If lListInitParameter.Count >= 2 Then cHMIPLC.WriteAny(lListInitParameter(1), TempStructAST)
        Return True
    End Function
End Class