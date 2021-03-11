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
    Private cLocalActionManager As clsActionManager
    Private cMainStepCfg As clsMainStepCfg
    Private cSubStepCfg As clsSubStepCfg
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
        cLocalActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cMainStepCfg = CType(cLocalElement(clsMainStepCfg.Name), clsMainStepCfg)
        cSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
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
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)
        HmiComboBox_Action.ComboBox.SelectedIndex = -1
        If lListParameter.Count >= 1 Then
            HmiTextBoxWithButton_A.TextBox.Text = lListParameter(0)
        End If
        If lListParameter.Count >= 2 Then
            HmiComboBox_Condition.ComboBox.Text = lListParameter(1)
        End If
        If lListParameter.Count >= 3 Then
            HmiTextBoxWithButton_B.TextBox.Text = lListParameter(2)
        End If
        Dim iCnt As Integer = 0
        If lListParameter.Count >= 4 Then
            For iCnt = 0 To HmiComboBox_Action.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(3) Then
                    HmiComboBox_Action.ComboBox.SelectedIndex = iCnt
                End If
            Next
        End If
        Return True
    End Function

    Private Sub LoadData()
        Dim lListKey As New Dictionary(Of String, Dictionary(Of String, String))
        Dim lListValue As New Dictionary(Of String, String)
        lListValue.Add("Variant", cLocalVariant.CurrentVariantCfg.Variant)
        lListValue.Add("SFC", cLocalVariant.CurrentVariantCfg.SFC)
        lListValue.Add("Jump", cLocalVariant.CurrentVariantCfg.Jump.ToString)
        For Each ElementKey As String In cLocalVariant.CurrentVariantCfg.ListElement.Keys
            lListValue.Add(ElementKey, cLocalVariant.CurrentVariantCfg.ListElement(ElementKey))
        Next
        lListKey.Add("Variant", lListValue)

        lListValue = New Dictionary(Of String, String)
        lListValue.Add("Process", cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process).ToString)
        lListKey.Add("Parameter", lListValue)

        Dim lListLine As New List(Of Integer)
        Dim lListNameWitheTextLine As New List(Of Integer)
        HmiTextBoxWithButton_A.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Replace, False)
        HmiTextBoxWithButton_B.RegisterButton(lListKey, lListLine, lListNameWitheTextLine, enumInsertType.Replace, False)
    End Sub

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cLocalActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)

        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "3"), enumExceptionType.Alarm)
        End If
        'If lListParameter(0) = "" Then
        '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "3"), enumExceptionType.Alarm)
        'End If

        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "4"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "5"), enumExceptionType.Alarm)
        End If
        'If lListParameter(2) = "" Then
        '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "5"), enumExceptionType.Alarm)
        'End If

        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) <> "@END" Then
            If Not cLocalActionManager.HasCurrentMainStepIndex(cMachineStationCfg.ID, lListParameter(4), lListParameter(3)) Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "7", lListParameter(3)), enumExceptionType.Alarm)
            End If
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        HmiComboBox_Action.ComboBox.Items.Clear()
        For Each element As clsMainStepCfg In cLocalActionManager.GetCurrentMainStepCfgList(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType))
            HmiComboBox_Action.ComboBox.Items.Add(element.MainStepParameter(HMIMainStepKeys.Name))
        Next
        HmiComboBox_Action.ComboBox.Items.Add("@END")

        HmiComboBox_Condition.ComboBox.Items.Clear()
        For Each eType As enumConditionMethod In [Enum].GetValues(GetType(enumConditionMethod))
            HmiComboBox_Condition.ComboBox.Items.Add(eType.ToString)
        Next
        LoadData()
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_A.Label.Text = cLanguageManager.GetUserTextLine("Jump", "HmiLabel_A")
        HmiLabel_A.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_B.Label.Text = cLanguageManager.GetUserTextLine("Jump", "HmiLabel_B")
        HmiLabel_B.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Condition.Label.Text = cLanguageManager.GetUserTextLine("Jump", "HmiLabel_Condition")
        HmiLabel_Condition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Action.Label.Text = cLanguageManager.GetUserTextLine("Jump", "HmiLabel_Action")
        HmiLabel_Action.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBoxWithButton_A.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBoxWithButton_B.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Condition.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Action.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        AddHandler HmiTextBoxWithButton_A.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBoxWithButton_B.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBoxWithButton_A.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiComboBox_Condition.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Action.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name

        End Select
    End Sub


    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBoxWithButton_A.TextBox.Height + 6 + 6
        Next
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub


    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBoxWithButton_A.TextBox.Text)
        lListInitParameter.Add(HmiComboBox_Condition.ComboBox.Text)
        lListInitParameter.Add(HmiTextBoxWithButton_B.TextBox.Text)
        If HmiComboBox_Action.ComboBox.SelectedIndex = -1 Then
            lListInitParameter.Add("")
            lListInitParameter.Add("")
        Else
            lListInitParameter.Add(HmiComboBox_Action.ComboBox.SelectedIndex)
            If HmiComboBox_Action.ComboBox.SelectedIndex = HmiComboBox_Action.ComboBox.Items.Count - 1 Then
                lListInitParameter.Add("@END")
            Else
                lListInitParameter.Add(cSubStepCfg.SubStepParameter(HMISubStepKeys.MainType))
            End If

        End If
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cLocalActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count >= 5 Then
            If lListParameter(4) <> "@END" Then
                'If Not cLocalActionManager.HasMainStepIndex(cMachineStationCfg.ID, lListParameter(4), lListParameter(3)) Then
                '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "7", lListParameter(3)), enumExceptionType.Alarm)
                'End If
            End If
        End If

        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cLocalActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        lTargetListParameter = lListParameter
        lTargetListParameter = lListParameter
        If lTargetListParameter.Count >= 5 Then
            If lListParameter(4) <> "@END" Then
                If Not cLocalActionManager.HasCurrentMainStepIndex(cMachineStationCfg.ID, lListParameter(4), lListParameter(3)) Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("Jump", "7", lListParameter(3)), enumExceptionType.Alarm)
                End If
            End If
        End If
        Return True
    End Function
End Class

