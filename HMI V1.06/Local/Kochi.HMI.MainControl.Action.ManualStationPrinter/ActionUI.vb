
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
    Private cSystemManager As clsSystemManager
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
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        InitForm()
        InitControlText()
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Quit
        Me.Dispose()
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        HmiComboBox_Printer.ComboBox.SelectedIndex = -1
        HmiComboBox_LKSN.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIPrinter))
        HmiComboBox_Printer.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_Printer.ComboBox.Items.Add(element.Name)
            Next
        End If

        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMILKSN))
        HmiComboBox_LKSN.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_LKSN.ComboBox.Items.Add(element.Name)
            Next
        End If

        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_Printer.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_Printer.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If

        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_LKSN.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(1) Then
                    HmiComboBox_LKSN.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If


        If lListParameter.Count >= 3 Then
            HmiTextBox_FormatFile.TextBox.Text = lListParameter(2)
        End If

        If lListParameter.Count >= 4 Then
            HmiTextBox_PrintFile.TextBox.Text = lListParameter(3)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 4 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "1"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "2"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(0)), GetType(clsHMIPrinter))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "6", cMachineStationCfg.ID, CInt(lListParameter(0))), enumExceptionType.Alarm)
        End If

        'If lListParameter(1) = "" Then
        '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "3"), enumExceptionType.Alarm)
        'End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "4"), enumExceptionType.Alarm)
        End If

        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "4"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "5"), enumExceptionType.Alarm)
        End If

        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPrint", "5"), enumExceptionType.Alarm)
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Printer.Label.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiLabel_Printer")
        HmiLabel_Printer.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_LKSN.Label.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiLabel_LKSN")
        HmiLabel_LKSN.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_FormatFile.Label.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiLabel_FormatFile")
        HmiLabel_FormatFile.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_PrintFile.Label.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiLabel_PrintFile")
        HmiLabel_PrintFile.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_FormatFile.Button.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiButton_FormatFile")
        HmiButton_FormatFile.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_PrintFile.Button.Text = cLanguageManager.GetUserTextLine("ManualStationPrint", "HmiButton_PrintFile")
        HmiButton_PrintFile.Button.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiComboBox_Printer.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_LKSN.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_FormatFile.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PrintFile.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_FormatFile.TextBoxReadOnly = True
        HmiTextBox_PrintFile.TextBoxReadOnly = True


        AddHandler HmiComboBox_Printer.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_LKSN.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiTextBox_FormatFile.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiButton_FormatFile.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_PrintFile.Button.Click, AddressOf Button_Click
        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiComboBox_Printer.ComboBox.Height + 6 + 6
        Next
    End Sub


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_FormatFile"
                OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.PrinterFolder
                OpenFileDialog_Path.Filter = "*.txt|*.txt"
                If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
                    HmiTextBox_FormatFile.TextBox.Text = OpenFileDialog_Path.FileName
                End If
            Case "HmiButton_PrintFile"
                OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.PrinterFolder
                OpenFileDialog_Path.Filter = "*.txt|*.txt"
                If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
                    HmiTextBox_PrintFile.TextBox.Text = OpenFileDialog_Path.FileName
                End If
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub



    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_Printer"
            Case "HmiComboBox_LKSN"
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub
    Private Sub ListView_Data_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub
    Private Sub GetParamater()
        lListInitParameter.Clear()
        If HmiComboBox_Printer.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_Printer.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        If HmiComboBox_LKSN.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_LKSN.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_FormatFile.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_PrintFile.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        lTargetListParameter = lListParameter
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIPrinter))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(0) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(0) = ""
        End If

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(1), GetType(clsHMILKSN))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(1) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(1) = ""
        End If
        lTargetListParameter(2) = clsSystemPath.ToSystemPath(lTargetListParameter(2))
        lTargetListParameter(3) = clsSystemPath.ToSystemPath(lTargetListParameter(3))
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIPrinter))
        lTargetListParameter(0) = cDeviceCfg.StationIndex

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(1)), GetType(clsHMILKSN))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(1) = cDeviceCfg.StationIndex
        End If
        lTargetListParameter(2) = clsSystemPath.ToIniPath(lTargetListParameter(2))
        lTargetListParameter(3) = clsSystemPath.ToIniPath(lTargetListParameter(3))
        Return True
    End Function
End Class
