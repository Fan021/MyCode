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
        HmiTextBox_ResourceId.TextBox.Text = ""
        HmiTextBox_Operation.TextBox.Text = ""
        If lListInitParameter.Count >= 1 Then
            HmiTextBox_ResourceId.TextBox.Text = lListInitParameter(0)
        End If
        If lListInitParameter.Count >= 2 Then
            HmiTextBox_Operation.TextBox.Text = lListInitParameter(1)
        End If
        If lListInitParameter.Count >= 3 Then
            HmiTextBox_Address.TextBox.Text = lListInitParameter(2)
        End If

        If lListInitParameter.Count >= 4 Then
            HmiTextBox_UserName.TextBox.Text = lListInitParameter(3)
        End If

        If lListInitParameter.Count >= 5 Then
            HmiTextBox_Password.TextBox.Text = lListInitParameter(4)
        End If

        If lListInitParameter.Count >= 6 Then
            HmiTextBox_NotInqueue.TextBox.Text = lListInitParameter(5)
        End If

        If lListInitParameter.Count >= 7 Then
            HmiTextBox_NotInqueue2.TextBox.Text = lListInitParameter(6)
        End If

        If lListInitParameter.Count >= 8 Then
            HmiTextBox_NCCode.TextBox.Text = lListInitParameter(7)
        End If

        If lListInitParameter.Count >= 9 Then
            HmiTextBox_NCRetryCode.TextBox.Text = lListInitParameter(8)
        End If

        AddHandler HmiTextBox_ResourceId.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Operation.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Address.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_UserName.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Password.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_NotInqueue.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_NotInqueue2.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_NCCode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_NCRetryCode.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
        End Select
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("MES", "6"), enumExceptionType.Alarm)
        End If
        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_ResourceId.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_ResourceId")
        Me.HmiLabel_Operation.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_Operation")
        Me.HmiLabel_Address.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_Address")
        Me.HmiLabel_UserName.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_UserName")
        Me.HmiLabel_Password.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_Password")
        Me.HmiLabel_NotInqueue.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_NotInqueue")
        Me.HmiLabel_NotInqueue2.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_NotInqueue2")
        Me.HmiLabel_NCCode.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_NCCode")
        Me.HmiLabel_NCRetryCode.Label.Text = cLanguageManager.GetUserTextLine("MES", "HmiLabel_NCRetryCode")
        Me.TabPage1.Text = cLanguageManager.GetUserTextLine("MES", "TabPage1")
        Me.TabPage2.Text = cLanguageManager.GetUserTextLine("MES", "TabPage2")
        Me.TabPage1.Font = New System.Drawing.Font("Calibri", 5)
        Me.TabPage2.Font = New System.Drawing.Font("Calibri", 5)
        AddHandler HmiTextBox_ResourceId.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_ResourceId.TextBox.Height + 6 + 6
        Next
        For Each element As RowStyle In HmiTableLayoutPanel_NCCode.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_ResourceId.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_ResourceId.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Operation.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Address.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_UserName.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Password.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_NotInqueue.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_NotInqueue2.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_NCCode.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_NCRetryCode.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeIniToParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count < 7 Then
            lTargetListParameter.Add("")
        End If
        If lTargetListParameter.Count < 8 Then
            lTargetListParameter.Add("")
        End If
        If lTargetListParameter.Count < 9 Then
            lTargetListParameter.Add("")
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeParameterToIni
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function
End Class