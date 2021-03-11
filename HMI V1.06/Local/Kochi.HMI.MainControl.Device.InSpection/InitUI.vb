Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.LocalDevice
Public Class InitUI
    Implements IInitUI

    Protected lListInitParameter As New List(Of String)
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)


    Public ReadOnly Property UI As Panel Implements IInitUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Init
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        InitForm()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IInitUI.Quit
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListInitParameter As List(Of String)) As Boolean Implements IInitUI.SetParameter

        HmiTextBox_PLCAds.TextBox.Text = ""
        HmiTextBox_Path.TextBox.Text = ""
        HmiTextBox_Path2.TextBox.Text = ""
        HmiTextBox_Include.TextBox.Text = ""
        HmiTextBox_TimeOut.TextBox.Text = ""
        HmiComboBox_Type.ComboBox.SelectedIndex = -1
        HmiComboBox_Type2.ComboBox.SelectedIndex = -1

        If lListInitParameter.Count >= 1 Then
            HmiTextBox_PLCAds.TextBox.Text = lListInitParameter(0)
        End If
        Dim iCnt As Integer = 0
        If lListInitParameter.Count >= 2 Then
            For iCnt = 0 To HmiComboBox_Type.ComboBox.Items.Count - 1
                If HmiComboBox_Type.ComboBox.Items(iCnt) = lListInitParameter(1) Then
                    HmiComboBox_Type.ComboBox.SelectedIndex = iCnt
                End If
            Next
        End If
        If lListInitParameter.Count >= 3 Then
            For iCnt = 0 To HmiComboBox_Type2.ComboBox.Items.Count - 1
                If HmiComboBox_Type2.ComboBox.Items(iCnt) = lListInitParameter(2) Then
                    HmiComboBox_Type2.ComboBox.SelectedIndex = iCnt
                End If
            Next
        End If

        If lListInitParameter.Count >= 4 Then
            HmiTextBox_Path.TextBox.Text = lListInitParameter(3)
        End If

        If lListInitParameter.Count >= 5 Then
            HmiTextBox_Path2.TextBox.Text = lListInitParameter(4)
        End If

        If lListInitParameter.Count >= 6 Then
            HmiTextBox_Include.TextBox.Text = lListInitParameter(5)
        End If

        If lListInitParameter.Count >= 7 Then
            HmiTextBox_TimeOut.TextBox.Text = lListInitParameter(6)
        End If


        If lListInitParameter.Count >= 8 Then
            If lListInitParameter(7) = "TRUE" Then
                RadioButtonProgram_Y.Checked = True
                RadioButtonProgram_N.Checked = False
            Else
                RadioButtonProgram_Y.Checked = False
                RadioButtonProgram_N.Checked = True
            End If
        End If

        HmiTextBox_Path.TextBoxReadOnly = True
        HmiTextBox_Path2.TextBoxReadOnly = True
        AddHandler HmiTextBox_PLCAds.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Path.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Path2.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Include.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_TimeOut.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiButton_Path.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Path2.Button.Click, AddressOf Button_Click
        AddHandler RadioButtonProgram_Y.CheckedChanged, AddressOf RadioButtonProgram_CheckedChanged
        AddHandler RadioButtonProgram_N.CheckedChanged, AddressOf RadioButtonProgram_CheckedChanged
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Path"
                If FolderBrowserDialog_Path.ShowDialog() = DialogResult.OK Then
                    HmiTextBox_Path.TextBox.Text = FolderBrowserDialog_Path.SelectedPath
                Else
                    HmiTextBox_Path.TextBox.Text = ""
                End If
            Case "HmiButton_Path2"
                If FolderBrowserDialog_Path.ShowDialog() = DialogResult.OK Then
                    HmiTextBox_Path2.TextBox.Text = FolderBrowserDialog_Path.SelectedPath
                Else
                    HmiTextBox_Path2.TextBox.Text = ""
                End If
        End Select
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_Type"
                If HmiComboBox_Type.ComboBox.Text = enumInSpectionType.Vision.ToString Then
                    Me.HmiLabel_Type2.Show()
                    Me.HmiComboBox_Type2.Show()

                    Me.HmiLabel_Path.Label.Show()
                    Me.HmiTextBox_Path.TextBox.Show()
                    Me.HmiButton_Path.Button.Show()

                    Me.HmiLabel_Path2.Label.Show()
                    Me.HmiTextBox_Path2.TextBox.Show()
                    Me.HmiButton_Path2.Button.Show()

                    Me.HmiLabel_TimeOut.Show()
                    Me.HmiTextBox_TimeOut.Show()

                    Me.HmiLabel_Include.Show()
                    Me.HmiTextBox_Include.Show()
                    HmiComboBox_Type2.ComboBox.Items.Clear()
                    Dim strTempValue As String = cMachineManager.DeviceParameterManager.ListElement("Inspection")
                    Dim cValue() As String = strTempValue.Split("|")
                    Dim cValue2() As String = cValue(0).Split(";")
                    For i = 0 To cValue2.Count - 1
                        HmiComboBox_Type2.ComboBox.Items.Add(cValue2(i))
                    Next

                    TableLayoutPanel_Body.RowStyles(3).Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6
                    TableLayoutPanel_Body.RowStyles(4).Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6
                    TableLayoutPanel_Body.RowStyles(5).Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6
                    TableLayoutPanel_Body.RowStyles(6).Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6

                Else
                    Me.HmiLabel_Type2.Show()
                    Me.HmiComboBox_Type2.Show()

                    Me.HmiLabel_Path.Label.Hide()
                    Me.HmiTextBox_Path.TextBox.Hide()
                    Me.HmiButton_Path.Button.Hide()

                    Me.HmiLabel_Path2.Label.Hide()
                    Me.HmiTextBox_Path2.TextBox.Hide()
                    Me.HmiButton_Path2.Button.Hide()

                    Me.HmiLabel_Include.Hide()
                    Me.HmiTextBox_Include.Hide()

                    Me.HmiLabel_TimeOut.Hide()
                    Me.HmiTextBox_TimeOut.Hide()

                    Me.HmiLabel_ProgramCheck.Show()
                    RadioButtonProgram_Y.Show()
                    RadioButtonProgram_N.Show()

                    TableLayoutPanel_Body.RowStyles(3).SizeType = System.Windows.Forms.SizeType.Absolute
                    TableLayoutPanel_Body.RowStyles(4).SizeType = System.Windows.Forms.SizeType.Absolute
                    TableLayoutPanel_Body.RowStyles(5).SizeType = System.Windows.Forms.SizeType.Absolute
                    TableLayoutPanel_Body.RowStyles(6).SizeType = System.Windows.Forms.SizeType.Absolute

                    TableLayoutPanel_Body.RowStyles(3).Height = 0
                    TableLayoutPanel_Body.RowStyles(4).Height = 0
                    TableLayoutPanel_Body.RowStyles(5).Height = 0
                    TableLayoutPanel_Body.RowStyles(6).Height = 0
                    HmiComboBox_Type2.ComboBox.Items.Clear()
                    Dim strTempValue As String = cMachineManager.DeviceParameterManager.ListElement("Inspection")
                    Dim cValue() As String = strTempValue.Split("|")
                    If cValue.Count >= 2 Then
                        Dim cValue2() As String = cValue(1).Split(";")
                        For i = 0 To cValue2.Count - 1
                            HmiComboBox_Type2.ComboBox.Items.Add(cValue2(i))
                        Next
                    End If
                End If
        End Select

        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IInitUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "4"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "7"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count > 4 Then
            If lListParameter(3) <> "" And lListParameter(4) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "8"), enumExceptionType.Alarm)
            End If
            If lListParameter(3) <> "" And lListParameter(6) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "10"), enumExceptionType.Alarm)
            End If
        End If

        Return True
    End Function

    Public Function InitForm() As Boolean
        Me.TopLevel = False
        Me.HmiLabel_PLCAds.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_PLCAds")
        Me.HmiLabel_Type.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Type")
        Me.HmiLabel_Type2.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Type2")
        Me.HmiLabel_Path.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Path")
        Me.HmiLabel_Path2.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Path2")
        Me.HmiLabel_Include.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_Include")
        Me.HmiLabel_TimeOut.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_TimeOut")
        Me.HmiButton_Path.Button.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiButton_Path")
        Me.HmiButton_Path2.Button.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiButton_Path2")
        Me.HmiLabel_ProgramCheck.Label.Text = cLanguageManager.GetUserTextLine("InSpection", "HmiLabel_ProgramCheck")
        Me.RadioButtonProgram_Y.Text = cLanguageManager.GetUserTextLine("InSpection", "RadioButtonProgram_Y")
        Me.RadioButtonProgram_N.Text = cLanguageManager.GetUserTextLine("InSpection", "RadioButtonProgram_N")

        RadioButtonProgram_Y.Checked = True
        Me.HmiLabel_TimeOut.Show()
        Me.HmiTextBox_TimeOut.Show()

        Me.HmiLabel_Path.Label.Hide()
        Me.HmiTextBox_Path.TextBox.Hide()
        Me.HmiButton_Path.Button.Hide()

        Me.HmiLabel_Path2.Label.Hide()
        Me.HmiTextBox_Path2.TextBox.Hide()
        Me.HmiButton_Path2.Button.Hide()

        Me.HmiLabel_Include.Hide()
        Me.HmiTextBox_Include.Hide()
        Me.HmiLabel_TimeOut.Hide()
        Me.HmiTextBox_TimeOut.Hide()
        Me.HmiLabel_ProgramCheck.Hide()
        RadioButtonProgram_Y.Hide()
        RadioButtonProgram_N.Hide()

        Me.HmiTextBox_TimeOut.ValueType = GetType(Integer)

        HmiComboBox_Type.ComboBox.Items.Clear()
        For Each eType As enumInSpectionType In [Enum].GetValues(GetType(enumInSpectionType))
            HmiComboBox_Type.ComboBox.Items.Add(eType.ToString)
        Next
        AddHandler HmiComboBox_Type.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Type2.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged

        AddHandler HmiTextBox_PLCAds.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iCnt As Integer = 0
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_PLCAds.TextBox.Height + 6 + 6
            If HmiComboBox_Type.ComboBox.Text = enumInSpectionType.Sensor.ToString Then
                If iCnt >= 3 And iCnt <= 6 Then
                    element.SizeType = System.Windows.Forms.SizeType.Absolute
                    element.Height = 0
                End If
            End If
            iCnt = iCnt + 1
        Next
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_PLCAds.TextBox.Text)
        lListInitParameter.Add(HmiComboBox_Type.ComboBox.Text)
        lListInitParameter.Add(HmiComboBox_Type2.ComboBox.Text)
        lListInitParameter.Add(HmiTextBox_Path.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Path2.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Include.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_TimeOut.TextBox.Text)
        If RadioButtonProgram_Y.Checked Then
            lListInitParameter.Add("TRUE")
        Else
            lListInitParameter.Add("FALSE")
        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeIniToParameter
        lTargetListParameter = lListParameter
        If lListParameter.Count > 4 Then
            lTargetListParameter(3) = clsSystemPath.ToSystemPath(lListParameter(3))
        End If
        If lListParameter.Count > 5 Then
            lTargetListParameter(4) = clsSystemPath.ToSystemPath(lListParameter(4))
        End If
        If lListParameter.Count < 8 Then
            lListParameter.Add("TRUE")
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IInitUI.ChangeParameterToIni
        lTargetListParameter = lListParameter
        If lListParameter.Count > 4 Then
            lTargetListParameter(3) = clsSystemPath.ToIniPath(lListParameter(3))
        End If
        If lListParameter.Count > 5 Then
            lTargetListParameter(4) = clsSystemPath.ToIniPath(lListParameter(4))
        End If
        Return True
    End Function

    Private Sub RadioButtonProgram_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

End Class