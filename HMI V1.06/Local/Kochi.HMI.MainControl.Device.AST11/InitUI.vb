Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class InitUI
    Implements IInitUI

    Protected lListInitParameter As New List(Of String)
    Protected cLanguageManager As clsLanguageManager
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)

    Public ReadOnly Property UI As Panel Implements IInitUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Init
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        InitForm()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Quit
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean Implements IInitUI.SetParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        HmiTextBox_ASTIP.TextBox.Text = ""
        HmiTextBox_PLCAds.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_ASTIP.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_PLCAds.TextBox.Text = lListInitParameter(1)
        End If
        AddHandler HmiTextBox_ASTIP.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_PLCAds.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Public Function CheckInitParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 2 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 1 Then
            '   Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            '   Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AST11", "4"), enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_ASTIP.Label.Text = cLanguageManager.GetUserTextLine("AST11", "HmiLabel_ASTIP")
        Me.HmiLabel_PLCAds.Label.Text = cLanguageManager.GetUserTextLine("AST11", "HmiLabel_PLCAds")
        Me.HmiTextBox_ASTIP.ValueType = GetType(clsIP)
        AddHandler HmiTextBox_ASTIP.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_ASTIP.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_ASTIP.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_PLCAds.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeIniToParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeParameterToIni
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub
End Class