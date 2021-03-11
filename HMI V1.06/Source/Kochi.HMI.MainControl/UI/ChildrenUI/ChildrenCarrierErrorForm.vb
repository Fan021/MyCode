Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Public Class ChildrenCarrierErrorForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private lListElement As New Dictionary(Of String, Object)
    Private cMachineManager As clsMachineManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private cUserManager As clsUserManager
    Private cStationErrorCodeManager As clsStationErrorCodeManager
    Private cErrorCodeManager As clsErrorCodeManager
    Private strButtonName As String
    Private cChildrenCarrierTotalForm As ChildrenCarrierTotalForm
    Private cChildrenCarrierDetailForm As ChildrenCarrierDetailForm

    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements UI.IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cChildrenCarrierTotalForm = New ChildrenCarrierTotalForm
            cChildrenCarrierTotalForm.Init(cLocalElement, cSystemElement)
            TabPage1.Controls.Add(cChildrenCarrierTotalForm.UI)
            TabPage1.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierErrorForm.ToString, "TabPage1")
            cChildrenCarrierDetailForm = New ChildrenCarrierDetailForm
            cChildrenCarrierDetailForm.Init(cLocalElement, cSystemElement)
            TabPage2.Controls.Add(cChildrenCarrierDetailForm.UI)
            TabPage2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierErrorForm.ToString, "TabPage2")
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenCarrierTotalForm.ToString))
            Return False
        End Try
    End Function

    Public Function Quit(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean Implements UI.IChildrenUI.Quit
        If Not IsNothing(cChildrenCarrierTotalForm) Then cChildrenCarrierTotalForm.Quit(cLocalElement, cSystemElement)
        If Not IsNothing(cChildrenCarrierDetailForm) Then cChildrenCarrierDetailForm.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Private Sub TabControl_Body_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl_Body.SelectedIndexChanged
        If TabControl_Body.SelectedIndex = 1 Then
            cChildrenCarrierDetailForm.Reflesh()
        End If
    End Sub
End Class