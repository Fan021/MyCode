﻿Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Public Class InitUI
    Implements IInitUI

    Protected lListInitParameter As New List(Of String)
    Protected cLanguageManager As clsLanguageManager
    Protected cSystemManager As clsSystemManager
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)

    Public ReadOnly Property UI As Panel Implements IInitUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Init
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        InitForm()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Quit
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean Implements IInitUI.SetParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        HmiTextBox_Adress.TextBox.Text = ""
        HmiTextBox_MachineName.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_Adress.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_MachineName.TextBox.Text = lListInitParameter(1)
        End If
        If lListInitParameter.Count >= 3 Then
            HmiTextBox_ProcessName.TextBox.Text = lListInitParameter(2)
        End If
        If lListInitParameter.Count >= 4 Then
            HmiTextBox_Description.TextBox.Text = lListInitParameter(3)
        End If
        If lListInitParameter.Count >= 5 Then
            HmiTextBox_TraceID.TextBox.Text = lListInitParameter(4)
        End If
        If lListInitParameter.Count >= 6 Then
            RadioButton_Y.Checked = IIf(lListInitParameter(5) = "TRUE", True, False)
            RadioButton_N.Checked = Not RadioButton_Y.Checked
        End If

        AddHandler HmiTextBox_Adress.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MachineName.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ProcessName.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Description.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_TraceID.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function


    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "3"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "4"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "5"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "6"), enumExceptionType.Alarm)
        End If

        If lListParameter(4).Length <> 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 6 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KDX", "8"), enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_Adress.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_Adress")
        Me.HmiLabel_MachineName.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_MachineName")
        Me.HmiLabel_ProcessName.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_ProcessName")
        Me.HmiLabel_Description.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_Description")
        Me.HmiLabel_TraceID.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_TraceID")
        Me.HmiLabel_Enable.Label.Text = cLanguageManager.GetUserTextLine("KDX", "HmiLabel_Enable")
        HmiTextBox_TraceID.ValueType = GetType(Integer)
        HmiTextBox_TraceID.Number = 5
        RadioButton_Y.Checked = True
        AddHandler HmiTextBox_Adress.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler RadioButton_Y.Click, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.Click, AddressOf RadioButton_CheckedChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Path"
                LoadPath()
        End Select
    End Sub
    Private Sub LoadPath()
        OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.LineControlFolder
        OpenFileDialog_Path.Filter = "*.ini|*.ini"
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            HmiTextBox_Adress.TextBox.Text = clsSystemPath.ToIniPath(OpenFileDialog_Path.FileName)
        End If
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Adress.TextBox.Height + 6 + 6
        Next
    End Sub



    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_Adress.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_MachineName.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ProcessName.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Description.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_TraceID.TextBox.Text)
        If RadioButton_Y.Checked Then
            lListInitParameter.Add("TRUE")
        Else
            lListInitParameter.Add("FALSE")
        End If
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
End Class