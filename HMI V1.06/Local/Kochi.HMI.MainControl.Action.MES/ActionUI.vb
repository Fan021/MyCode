﻿Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ActionUI
    Implements IActionUI

    Protected lListInitParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected iParentProgramUI As IParentProgramUI
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cMachineStationCfg As clsMachineStationCfg
    Public ReadOnly Property UI As Panel Implements IActionUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
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
        Me.Dispose()
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        HmiComboBox_MES.ComboBox.SelectedIndex = -1
        HmiComboBox_Method.ComboBox.SelectedIndex = -1
        ' iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)
        iParentProgramUI.ShowButton(enumProgramButtonType.HmiTextBox_Repeat)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIMES))
        HmiComboBox_MES.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_MES.ComboBox.Items.Add(element.Name.ToString)
            Next
        End If

        HmiComboBox_Method.ComboBox.Items.Clear()
        For Each eType As enumMESMethod In [Enum].GetValues(GetType(enumMESMethod))
            HmiComboBox_Method.ComboBox.Items.Add(eType.ToString)
        Next

        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_MES.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_MES.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If

        If lListParameter.Count >= 2 Then
            HmiComboBox_Method.ComboBox.Text = lListParameter(1)
        End If
        If lListParameter.Count >= 3 Then
            HmiTextBox_Carrier.TextBox.Text = lListParameter(2)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 2 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "1"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "2"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(0)), GetType(clsHMIMES))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "4", cMachineStationCfg.ID, CInt(lListParameter(0))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("GlobalStationMES", "3"), enumExceptionType.Alarm)
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_MES.Label.Text = cLanguageManager.GetUserTextLine("GlobalStationMES", "HmiLabel_MES")
        HmiLabel_MES.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Method.Label.Text = cLanguageManager.GetUserTextLine("GlobalStationMES", "HmiLabel_Method")
        HmiLabel_Carrier.Label.Text = cLanguageManager.GetUserTextLine("GlobalStationMES", "HmiLabel_Carrier")
        HmiLabel_Method.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Carrier.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_MES.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Method.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Method.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Carrier.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        AddHandler HmiComboBox_MES.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Method.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiTextBox_Carrier.TextBox.TextChanged, AddressOf TextBox_TextChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiComboBox_MES.ComboBox.Height + 6 + 6
        Next
    End Sub


    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If HmiComboBox_Method.ComboBox.Text = enumMESMethod.logData_Carrier.ToString Then
            HmiLabel_Carrier.Show()
            HmiTextBox_Carrier.Show()
        Else
            HmiLabel_Carrier.Hide()
            HmiTextBox_Carrier.Hide()
        End If
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        If HmiComboBox_MES.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_MES.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiComboBox_Method.ComboBox.Text)
        lListInitParameter.Add(HmiTextBox_Carrier.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIMES))
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

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIMES))
        lTargetListParameter(0) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class

Public Enum enumMESMethod
    Start = 1
    StartWithConfirm
    CreateSFC
    logNonConformance
    Assemble
    Complete
    logRetryNonConformance
    logListNonConformance
    AssembleOrlogNonConformance
    logData_Carrier
End Enum