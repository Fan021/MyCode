Imports System.Windows.Forms
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
        HmiTextBox_StationID.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_StationID.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            RadioButton_Y.Checked = IIf(lListInitParameter(1) = "TRUE", True, False)
            RadioButton_N.Checked = Not RadioButton_Y.Checked
        End If
        AddHandler HmiTextBox_StationID.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function


    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)


        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("StationCheck", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("StationCheck", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("StationCheck", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("StationCheck", "3"), enumExceptionType.Alarm)
        End If
       
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_StationID.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_StationID")
        Me.HmiLabel_Enable.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Enable")
        HmiTextBox_StationID.ValueType = GetType(Integer)
        HmiTextBox_StationID.Number = 5
        RadioButton_Y.Checked = True
        RadioButton_N.Checked = False
        AddHandler HmiTextBox_StationID.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler RadioButton_Y.Click, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.Click, AddressOf RadioButton_CheckedChanged
        Return True
    End Function



    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_StationID.TextBox.Height + 6 + 6
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
        lListInitParameter.Add(HmiTextBox_StationID.TextBox.Text)
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