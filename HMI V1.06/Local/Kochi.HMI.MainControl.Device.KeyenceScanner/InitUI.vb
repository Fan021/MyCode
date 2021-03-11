Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

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
        HmiTextBox_IP.TextBox.Text = ""
        HmiTextBox_Port.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_IP.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_Port.TextBox.Text = lListInitParameter(1)
        End If
        If lListInitParameter.Count >= 3 Then
            HmiTextBox_TimeOut.TextBox.Text = lListInitParameter(2)
        End If
        If lListInitParameter.Count >= 4 Then
            RadioButton_Y.Checked = IIf(lListInitParameter(3) = "TRUE", True, False)
            RadioButton_N.Checked = Not RadioButton_Y.Checked
        End If
        AddHandler HmiTextBox_IP.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Port.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_TimeOut.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "8"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("KeyenceScanner", "8"), enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        HmiTextBox_Port.ValueType = GetType(Integer)
        Me.HmiLabel_IP.Label.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "HmiLabel_IP")
        Me.HmiLabel_Port.Label.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "HmiLabel_Port")
        Me.HmiLabel_TimeOut.Label.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "HmiLabel_TimeOut")
        Me.HmiLabel_Enable.Label.Text = cLanguageManager.GetUserTextLine("KeyenceScanner", "Enable")
        Me.HmiTextBox_IP.ValueType = GetType(clsIP)
        Me.HmiTextBox_Port.ValueType = GetType(Integer)
        Me.HmiTextBox_TimeOut.ValueType = GetType(Integer)
        AddHandler HmiTextBox_IP.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler RadioButton_Y.Click, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.Click, AddressOf RadioButton_CheckedChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_IP.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub
    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_IP.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Port.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_TimeOut.TextBox.Text)
        If RadioButton_Y.Checked Then
            lListInitParameter.Add("TRUE")
        Else
            lListInitParameter.Add("FALSE")
        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeIniToParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count < 4 Then
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