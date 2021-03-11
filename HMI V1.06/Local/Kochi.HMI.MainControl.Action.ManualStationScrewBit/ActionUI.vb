Imports System.Windows.Forms
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
    Private cLocalVariant As clsVariantManager
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
        cLocalVariant = CType(cLocalElement(clsVariantManager.Name), clsVariantManager)
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

        HmiTextBox_Pro.TextBox.Text = ""
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIScrewBit))
        HmiComboBox_ScrewBit.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_ScrewBit.ComboBox.Items.Add(element.Name)
            Next
        End If
        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_ScrewBit.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_ScrewBit.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If


        iCnt = 0

        If lListParameter.Count >= 2 Then
            HmiTextBox_Pro.TextBox.Text = lListParameter(1)
        End If
        Return True
    End Function

    Private Sub LoadData()
        Dim lListKey As New Dictionary(Of String, Dictionary(Of String, String))
        Dim lListValue As New Dictionary(Of String, String)


        For iCnt = 0 To 32
            lListValue.Add(iCnt.ToString, iCnt.ToString)
        Next
        lListKey.Add("Program", lListValue)

        lListValue = New Dictionary(Of String, String)
        For Each ElementKey As String In cLocalVariant.CurrentVariantCfg.ListElement.Keys
            lListValue.Add(ElementKey, cLocalVariant.CurrentVariantCfg.ListElement(ElementKey))
        Next
        lListKey.Add("Variant", lListValue)
        Dim lListLine As New List(Of Integer)
        Dim lListNameWitheTextLine As New List(Of Integer)
        lListLine.Add(1)
        lListNameWitheTextLine.Add(1)
        HmiTextBox_Pro.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Replace, True)
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)


        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrewBit", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrewBit", "7"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrewBit", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrewBit", "2"), enumExceptionType.Alarm)
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        LoadData()
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_ScrewBit.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrewBit", "HmiLabel_ScrewBit")
        HmiLabel_Program.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrewBit", "HmiLabel_Program")
        HmiLabel_Program.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ScrewBit.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Pro.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        AddHandler HmiTextBox_Pro.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Pro.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiComboBox_ScrewBit.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_Pro.TextBox.Height + 6 + 6
        Next
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_Pro"
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()

        lListInitParameter.Clear()
        If HmiComboBox_ScrewBit.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_ScrewBit.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_Pro.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIScrewBit))
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

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIScrewBit))
        lTargetListParameter(0) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class
