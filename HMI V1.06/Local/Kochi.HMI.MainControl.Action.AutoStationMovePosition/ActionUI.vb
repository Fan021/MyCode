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
    Private cHMIXY As clsHMIXY
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
        AddHandler cChangePage.BackPageChanged, AddressOf BackPageChanged
        InitForm()
        InitControlText()
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Quit
        ' cChangePage.BackPage()
        RemoveHandler cChangePage.BackPageChanged, AddressOf BackPageChanged
        If Not IsNothing(iProgramUI) Then iProgramUI.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        HmiTextBox_X.TextBox.Text = ""
        HmiTextBox_Y.TextBox.Text = ""
        HmiTextBox_Z.TextBox.Text = ""
        HmiComboBox_XYZ.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Insert, 1)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIXY), GetType(clsHMIXYZ))
        HmiComboBox_XYZ.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_XYZ.ComboBox.Items.Add(element.Name)
            Next
        End If

        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            For iCnt = 1 To HmiComboBox_XYZ.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(0) Then
                    HmiComboBox_XYZ.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If
        If lListParameter.Count >= 2 Then
            HmiTextBox_X.TextBox.Text = lListParameter(1)
        End If
        If lListParameter.Count >= 3 Then
            HmiTextBox_Y.TextBox.Text = lListParameter(2)
        End If
        If lListParameter.Count >= 4 Then
            HmiTextBox_Z.TextBox.Text = lListParameter(3)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "1"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(0)), GetType(clsHMIXY), GetType(clsHMIXYZ))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "5", cMachineStationCfg.ID, CInt(lListParameter(0))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "3"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIXY), GetType(clsHMIXYZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(0), GetType(clsHMIXY), GetType(clsHMIXYZ)).Source Is clsHMIXYZ Then
            If lListParameter.Count < 4 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "4"), enumExceptionType.Alarm)
            End If
            If lListParameter(3) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationMovePosition", "4"), enumExceptionType.Alarm)
            End If
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_XYZ.Label.Text = cLanguageManager.GetUserTextLine("AutoStationMovePosition", "HmiLabel_XYZ")
        HmiLabel_XYZ.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("AutoStationMovePosition", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("AutoStationMovePosition", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("AutoStationMovePosition", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_XYZ.Button.Text = cLanguageManager.GetUserTextLine("AutoStationMovePosition", "HmiButton_XYZ")
        HmiButton_XYZ.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_XYZ.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_X.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Y.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Z.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_XYZ.Button.Enabled = False

        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)
        HmiTextBox_Z.ValueType = GetType(Double)

        AddHandler HmiTextBox_X.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiButton_XYZ.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_X.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Y.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Z.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiComboBox_XYZ.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler iParentProgramUI.TextBox_Picture.TextBox.TextChanged, AddressOf TextBox_TextChanged

        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_X.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case Else
                GetParamater()
                RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
        End Select
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_XYZ"
                If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).Source Is clsHMIXYZ Then
                    HmiLabel_Z.Show()
                    HmiTextBox_Z.Show()
                Else
                    HmiLabel_Z.Hide()
                    HmiTextBox_Z.Hide()
                End If
                HmiButton_XYZ.Button.Enabled = True

        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_XYZ"
                cHMIXY = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).Source
                cHMIXY.CreateProgramUI(cLocalElement, cSystemElement)
                iProgramUI = cHMIXY.ProgramUI
                iProgramUI.Init(cLocalElement, cSystemElement)
                Dim lListParameter As New List(Of String)
                lListParameter.Clear()
                lListParameter.Add("")
                lListParameter.Add("")
                lListParameter.Add(HmiTextBox_X.TextBox.Text)
                lListParameter.Add(HmiTextBox_Y.TextBox.Text)
                If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).Source Is clsHMIXYZ Then
                    lListParameter.Add(HmiTextBox_Z.TextBox.Text)
                End If
                cChangePage.ChangePage(iProgramUI.UI, "HmiButton_XYZ")
                iProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).InitParameter), clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).ControlParameter), lListParameter)
        End Select
    End Sub

    Public Sub BackPageChanged(ByVal strUIName As String)
        Select Case strUIName
            Case "HmiButton_XYZ"
                If Not cHMIXY.ProgramUI.Cancel Then
                    Dim lListParameter As New List(Of String)
                    cHMIXY.ProgramUI.GetParameter(cLocalElement, cSystemElement, Nothing, Nothing, lListParameter)
                    HmiTextBox_X.TextBox.Text = lListParameter(0)
                    HmiTextBox_Y.TextBox.Text = lListParameter(1)
                    If lListParameter.Count >= 3 Then
                        HmiTextBox_Z.TextBox.Text = lListParameter(2)
                    Else
                        HmiTextBox_Z.TextBox.Text = ""
                    End If

                End If
                cHMIXY.ProgramUI.Quit(cLocalElement, cSystemElement)
        End Select
    End Sub

    Private Sub GetParamater()
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        lListInitParameter.Clear()
        If HmiComboBox_XYZ.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_XYZ.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_X.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Y.TextBox.Text)
        If lListInitParameter(0) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXYZ)).Source Is clsHMIXYZ Then
            lListInitParameter.Add(HmiTextBox_Z.TextBox.Text)
        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(0), GetType(clsHMIXY), GetType(clsHMIXYZ))
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
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(0)), GetType(clsHMIXY), GetType(clsHMIXYZ))
        lTargetListParameter(0) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class