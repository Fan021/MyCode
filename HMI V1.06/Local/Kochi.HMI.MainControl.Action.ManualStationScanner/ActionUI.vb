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

        HmiComboBox_Scanner.ComboBox.SelectedIndex = -1
        HmiComboBox_Method.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIAutoScanner), GetType(clsHMIManualScanner))
        HmiComboBox_Scanner.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_Scanner.ComboBox.Items.Add(element.Name)
            Next
        End If


        If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Company) = "KOI" Then
            HmiComboBox_Method.ComboBox.Items.Clear()
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanHousingAndWriteSN.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanHousingAndCheckSN.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanPFCPCB.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanDCPCB.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanDCPCB_AID.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanDCFilterPCB.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanSystemPCB.ToString)
            HmiComboBox_Method.ComboBox.Items.Add(enumScannerMethod.ScanChoker.ToString)
        Else
            HmiComboBox_Method.ComboBox.Items.Clear()
            For Each eType As enumScannerMethod In [Enum].GetValues(GetType(enumScannerMethod))
                HmiComboBox_Method.ComboBox.Items.Add(eType.ToString)
            Next
        End If

        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_Scanner.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_Scanner.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If

        If lListParameter.Count >= 2 Then
            HmiComboBox_Method.ComboBox.Text = lListParameter(1)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 2 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "2"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(0)), GetType(clsHMIAutoScanner), GetType(clsHMIManualScanner))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "4", cMachineStationCfg.ID, CInt(lListParameter(0))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScan", "3"), enumExceptionType.Alarm)
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Scanner.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScan", "HmiLabel_Scanner")
        HmiLabel_Scanner.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Method.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScan", "HmiLabel_Method")
        HmiLabel_Method.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Scanner.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Method.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        AddHandler HmiComboBox_Scanner.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Method.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiComboBox_Scanner.ComboBox.Height + 6 + 6
        Next
    End Sub


    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiLabel_Scanner"
            Case "HmiLabel_Method"
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        If HmiComboBox_Scanner.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_Scanner.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiComboBox_Method.ComboBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIAutoScanner), GetType(clsHMIManualScanner))
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
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIAutoScanner), GetType(clsHMIManualScanner))
        lTargetListParameter(0) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class

