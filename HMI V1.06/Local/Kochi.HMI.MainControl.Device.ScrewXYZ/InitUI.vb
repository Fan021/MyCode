﻿Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class InitUI
    Implements IInitUI
    Protected lListInitParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)

    Public ReadOnly Property UI As Panel Implements IInitUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Init
        InitForm()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Quit
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean Implements IInitUI.SetParameter
        HmiTextBox_PLCAds.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_PLCAds.TextBox.Text = lListInitParameter(0)
        End If
        AddHandler HmiTextBox_PLCAds.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        If lListParameter.Count < 1 Then
            Throw New clsHMIException("Paramater Length is Failure", enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException("Ads Name is Null", enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_PLCAds.Label.Text = "Ads Name:"

        AddHandler HmiTextBox_PLCAds.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_PLCAds.TextBox.Text)
    End Sub
End Class