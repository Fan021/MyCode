﻿Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
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
        HmiTextBox_Operation.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_Operation.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_Address.TextBox.Text = lListInitParameter(1)
        End If

        If lListInitParameter.Count >= 3 Then
            HmiTextBox_UserName.TextBox.Text = lListInitParameter(2)
        End If

        If lListInitParameter.Count >= 4 Then
            HmiTextBox_Password.TextBox.Text = lListInitParameter(3)
        End If

        If lListInitParameter.Count >= 5 Then
            HmiTextBox_Inqueue.TextBox.Text = lListInitParameter(4)
        End If

        If lListInitParameter.Count >= 6 Then
            HmiTextBox_Inqueue2.TextBox.Text = lListInitParameter(5)
        End If

        If lListInitParameter.Count >= 7 Then
            RadioButton_Y.Checked = IIf(lListInitParameter(6) = "TRUE", True, False)
            RadioButton_N.Checked = Not RadioButton_Y.Checked
        End If

        AddHandler HmiTextBox_Operation.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Address.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_UserName.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Password.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Inqueue.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Inqueue2.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
        End Select
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ProcessControl", "5"), enumExceptionType.Alarm)
        End If

        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_Operation.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Operation")
        Me.HmiLabel_Address.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Address")
        Me.HmiLabel_UserName.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_UserName")
        Me.HmiLabel_Password.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Password")
        Me.HmiLabel_Inqueue.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Inqueue")
        Me.HmiLabel_Inqueue2.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Inqueue2")
        Me.HmiLabel_Enable.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Enable")
        AddHandler HmiTextBox_Operation.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler RadioButton_Y.Click, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.Click, AddressOf RadioButton_CheckedChanged
        Return True
    End Function


    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Operation.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_Operation.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Address.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_UserName.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Password.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Inqueue.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Inqueue2.TextBox.Text)
        If RadioButton_Y.Checked Then
            lListInitParameter.Add("TRUE")
        Else
            lListInitParameter.Add("FALSE")
        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeIniToParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count < 5 Then
            lTargetListParameter.Add("")
        End If
        If lTargetListParameter.Count < 6 Then
            lTargetListParameter.Add("")
        End If
        If lTargetListParameter.Count < 7 Then
            lTargetListParameter.Add("TRUE")
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeParameterToIni
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function
End Class