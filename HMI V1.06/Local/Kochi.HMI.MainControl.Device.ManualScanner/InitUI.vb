Imports System.Windows.Forms
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
        HmiTextBox_Port.TextBox.Text = ""
        HmiTextBox_BaudRate.TextBox.Text = ""
        HmiTextBox_Parity.TextBox.Text = ""
        HmiTextBox_DataBits.TextBox.Text = ""
        HmiTextBox_StopBits.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_Port.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_BaudRate.TextBox.Text = lListInitParameter(1)
        End If
        If lListInitParameter.Count >= 3 Then
            HmiTextBox_Parity.TextBox.Text = lListInitParameter(2)
        End If
        If lListInitParameter.Count >= 4 Then
            HmiTextBox_DataBits.TextBox.Text = lListInitParameter(3)
        End If
        If lListInitParameter.Count >= 5 Then
            HmiTextBox_StopBits.TextBox.Text = lListInitParameter(4)
        End If
        AddHandler HmiTextBox_Port.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_BaudRate.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Parity.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_DataBits.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_StopBits.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualScanner", "7"), enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_Port.Label.Text = cLanguageManager.GetUserTextLine("ManualScanner", "HmiLabel_Port")
        Me.HmiLabel_BaudRate.Label.Text = cLanguageManager.GetUserTextLine("ManualScanner", "HmiLabel_BaudRate")
        Me.HmiLabel_Parity.Label.Text = cLanguageManager.GetUserTextLine("ManualScanner", "HmiLabel_Parity")
        Me.HmiLabel_DataBits.Label.Text = cLanguageManager.GetUserTextLine("ManualScanner", "HmiLabel_DataBits")
        Me.HmiLabel_StopBits.Label.Text = cLanguageManager.GetUserTextLine("ManualScanner", "HmiLabel_StopBits")
        HmiTextBox_BaudRate.ValueType = GetType(Integer)
        HmiTextBox_StopBits.ValueType = GetType(Integer)
        HmiTextBox_StopBits.Number = 1
        AddHandler HmiTextBox_Port.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Port.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_Port.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_BaudRate.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Parity.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_DataBits.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_StopBits.TextBox.Text)
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