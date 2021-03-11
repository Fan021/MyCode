Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing

Public Class ActionUI
    Implements IActionUI

    Protected lListInitParameter As New List(Of String)
    Protected cChangePage As clsChangePage
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected iParentProgramUI As IParentProgramUI
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cHMIPKP As clsHMIPKP
    Private cMachineStationCfg As clsMachineStationCfg
    Public ReadOnly Property UI As Panel Implements IActionUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cChangePage = CType(cLocalElement(clsChangePage.Name), clsChangePage)
        iParentProgramUI = CType(cLocalElement(enumUIName.ParentProgramForm.ToString), IParentProgramUI)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        InitForm()
        InitControlText()
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Quit
        ' cChangePage.BackPage()
        If Not IsNothing(iProgramUI) Then iProgramUI.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        HmiTextBox_X.TextBox.Text = "0"
        HmiTextBox_Y.TextBox.Text = "0"
        HmiTextBox_Z.TextBox.Text = "0"
        HmiComboBox_PKP.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Screw_Repeat)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)

        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        HmiComboBox_PKP.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_PKP.ComboBox.Items.Add(element.StationIndex)
            Next
        End If

        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            For Each element As String In HmiComboBox_PKP.ComboBox.Items
                If element = lListParameter(0) Then
                    HmiComboBox_PKP.ComboBox.SelectedIndex = iCnt
                End If
                iCnt = iCnt + 1
            Next
        End If
        If lListParameter.Count >= 2 Then
            HmiTextBox_X.TextBox.Text = lListParameter(1)
        End If
        If lListParameter.Count >= 3 Then
            HmiTextBox_X.TextBox.Text = lListParameter(2)
        End If
        If lListParameter.Count >= 4 Then
            HmiTextBox_X.TextBox.Text = lListParameter(3)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        'If Not IsNothing(cDeviceManager.GetDeviceFromName(lListParameter(3))) AndAlso Not IsNothing(cDeviceManager.GetDeviceFromName(lListParameter(3)).Source) AndAlso TypeOf cDeviceManager.GetDeviceFromName(lListParameter(3)).Source Is clsHMIPKP_Z Then
        '    If lListParameter.Count < 11 Then
        '        ' Throw New clsHMIException(cLanguageManager.GetTextLine("ManualStationDoAction", "1"), enumExceptionType.Alarm)
        '    End If
        'End If

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationAutoConfirm", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetTextLine("ManualStationAutoConfirm", "1"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(0)), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationAutoConfirm", "5", cMachineStationCfg.ID, CInt(lListParameter(0))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationAutoConfirm", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetTextLine("ManualStationAutoConfirm", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationAutoConfirm", "3"), enumExceptionType.Alarm)
        End If

        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetTextLine("ManualStationAutoConfirm", "3"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            If lListParameter.Count < 4 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationAutoConfirm", "4"), enumExceptionType.Alarm)
            End If
            If lListParameter(3) = "" Then
                Throw New clsHMIException(cLanguageManager.GetTextLine("ManualStationAutoConfirm", "4"), enumExceptionType.Alarm)
            End If
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_PKP.Label.Text = cLanguageManager.GetTextLine("ManualStationAutoConfirm", "HmiLabel_PKP")
        HmiLabel_PKP.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetTextLine("ManualStationAutoConfirm", "HmiLabel_Offset_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetTextLine("ManualStationAutoConfirm", "HmiLabel_Offset_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetTextLine("ManualStationAutoConfirm", "HmiLabel_Offset_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)
        HmiTextBox_Z.ValueType = GetType(Double)

        AddHandler HmiTextBox_X.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiTextBox_X.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Y.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Z.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiComboBox_PKP.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_PKP"
                If HmiComboBox_PKP.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.Text, GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.Text, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
                    HmiLabel_Z.Show()
                    HmiTextBox_Z.Show()
                Else
                    HmiLabel_Z.Hide()
                    HmiTextBox_Z.Hide()
                End If

        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiComboBox_PKP.ComboBox.Text)
        lListInitParameter.Add(HmiTextBox_X.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Y.TextBox.Text)
        If lListInitParameter(0) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListInitParameter(0), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListInitParameter(0), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            lListInitParameter.Add(HmiTextBox_Z.TextBox.Text)
        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(0) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(0) = ""
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        lTargetListParameter(0) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class