Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ActionUI
    Implements IScrewActionUI
    Protected lListInitParameter As New List(Of String)
    Protected cChangePage As clsChangePage
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected iParentProgramUI As IParentProgramUI
    Protected cLanguageManager As clsLanguageManager
    Protected mPicturePosition As PicturePosition
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cHMIX As clsHMIX
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
        If Not IsNothing(mPicturePosition) Then mPicturePosition.Quit()
        If Not IsNothing(iProgramUI) Then iProgramUI.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        HmiTextBox_MESPosition.TextBox.Text = ""
        HmiTextBox_PicturePosition.TextBox.Text = ""
        HmiTextBox_X.TextBox.Text = ""
        HmiTextBox_Y.TextBox.Text = ""
        HmiTextBox_Z.TextBox.Text = ""
        HmiComboBox_AST.ComboBox.SelectedIndex = -1
        HmiComboBox_Pro.ComboBox.SelectedIndex = -1
        HmiComboBox_XYZ.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Insert, 1)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIAST))
        HmiComboBox_AST.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_AST.ComboBox.Items.Add(element.Name)
            Next
        End If

        HmiComboBox_Pro.ComboBox.Items.Clear()
        For i = 1 To 16
            HmiComboBox_Pro.ComboBox.Items.Add(i.ToString)
        Next

        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIXY), GetType(clsHMIXZ))
        HmiComboBox_XYZ.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_XYZ.ComboBox.Items.Add(element.Name)
            Next
        End If


        If lListParameter.Count >= 1 Then
            HmiTextBox_PicturePosition.TextBox.Text = lListParameter(0)
        End If
        Dim iCnt As Integer = 0
        If lListParameter.Count >= 2 Then
            For iCnt = 1 To HmiComboBox_AST.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(1) Then
                    HmiComboBox_AST.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If
        If lListParameter.Count >= 3 Then
            HmiComboBox_Pro.ComboBox.Text = lListParameter(2)
        End If

        If lListParameter.Count >= 4 Then
            For iCnt = 1 To HmiComboBox_XYZ.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(3) Then
                    HmiComboBox_XYZ.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next

        End If

        If lListParameter.Count >= 5 Then
            HmiTextBox_X.TextBox.Text = lListParameter(4)
        End If
        If lListParameter.Count >= 6 Then
            HmiTextBox_Y.TextBox.Text = lListParameter(5)
        End If
        If lListParameter.Count >= 7 Then
            HmiTextBox_Z.TextBox.Text = lListParameter(6)
        End If
        If lListParameter.Count >= 9 Then
            HmiTextBox_MESPosition.TextBox.Text = lListParameter(8)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        ' If lListParameter.Count < 6 Then
        '  Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "1"), enumExceptionType.Alarm)
        ' End If
        'If Not IsNothing(cDeviceManager.GetDeviceFromName(lListParameter(3))) AndAlso Not IsNothing(cDeviceManager.GetDeviceFromName(lListParameter(3)).Source) AndAlso TypeOf cDeviceManager.GetDeviceFromName(lListParameter(3)).Source Is clsHMIXZ Then
        '    If lListParameter.Count < 7 Then
        '        Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "1"), enumExceptionType.Alarm)
        '    End If
        'End If
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "3"), enumExceptionType.Alarm)
        End If

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(1)), GetType(clsHMIAST))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "9", cMachineStationCfg.ID, CInt(lListParameter(1))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "5"), enumExceptionType.Alarm)
        End If

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(3)), GetType(clsHMIXY), GetType(clsHMIXZ))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "10", cMachineStationCfg.ID, CInt(lListParameter(3))), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "6"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 6 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(5) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(3), GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(3), GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXZ Then
            If lListParameter.Count < 7 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "8"), enumExceptionType.Alarm)
            End If
            If lListParameter(6) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationScrew", "8"), enumExceptionType.Alarm)
            End If
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_MESPosition.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_MESPosition")
        HmiLabel_MESPosition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_PicturePosition.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_PicturePosition")
        HmiLabel_PicturePosition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_XYZ.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_XYZ")
        HmiLabel_XYZ.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Pic.Button.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiButton_Pic")
        HmiButton_Pic.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_XYZ.Button.Text = cLanguageManager.GetUserTextLine("AutoStationScrew", "HmiButton_XYZ")
        HmiButton_XYZ.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_AST.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_XYZ.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_X.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Y.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Z.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MESPosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBoxReadOnly = True
        If iParentProgramUI.TextBox_Picture.TextBox.Text <> "" Then
            HmiButton_Pic.Button.Enabled = True
        Else
            HmiButton_Pic.Button.Enabled = False
        End If

        HmiButton_XYZ.Button.Enabled = False

        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)
        HmiTextBox_Z.ValueType = GetType(Double)


        AddHandler HmiTextBox_PicturePosition.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged

        AddHandler HmiButton_Pic.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_XYZ.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_PicturePosition.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MESPosition.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_X.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Y.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Z.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_XYZ.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler iParentProgramUI.TextBox_Picture.TextBox.TextChanged, AddressOf TextBox_TextChanged

        Return True
    End Function

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiTextBox_PicturePosition.TextBox.Height + 6 + 6
        Next
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Picture"
                If sender.text = "" Then
                    HmiButton_Pic.Button.Enabled = False
                Else
                    HmiButton_Pic.Button.Enabled = True
                End If
            Case Else
                GetParamater()
                RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
        End Select
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiComboBox_XYZ"
                If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXZ Then
                    HmiLabel_Z.Show()
                    HmiTextBox_Z.Show()
                    TableLayoutPanel_Body.RowStyles(6).Height = HmiTextBox_PicturePosition.TextBox.Height + 6 + 6
                    HmiLabel_Y.Hide()
                    HmiTextBox_Y.Hide()
                    TableLayoutPanel_Body.RowStyles(5).Height = 0
                ElseIf HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXY Then
                    HmiLabel_Z.Hide()
                    HmiTextBox_Z.Hide()
                    TableLayoutPanel_Body.RowStyles(6).Height = 0
                    HmiLabel_Y.Show()
                    HmiTextBox_Y.Show()
                    TableLayoutPanel_Body.RowStyles(5).Height = HmiTextBox_PicturePosition.TextBox.Height + 6 + 6

                End If
                HmiButton_XYZ.Button.Enabled = True

        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Pic"
                mPicturePosition = New PicturePosition
                mPicturePosition.Init(cLocalElement, cSystemElement)
                cChangePage.ChangePage(mPicturePosition.UI, "HmiButton_Pic")
                mPicturePosition.SetXYR(HmiTextBox_PicturePosition.TextBox.Text)
            Case "HmiButton_XYZ"
                cHMIX = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source
                cHMIX.CreateProgramUI(cLocalElement, cSystemElement)
                iProgramUI = cHMIX.ProgramUI
                iProgramUI.Init(cLocalElement, cSystemElement)
                Dim lListParameter As New List(Of String)
                lListParameter.Clear()
                lListParameter.Add(HmiComboBox_AST.ComboBox.SelectedIndex + 1)
                lListParameter.Add(HmiComboBox_Pro.ComboBox.Text)
                If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXZ Then
                    lListParameter.Add(HmiTextBox_X.TextBox.Text)
                    lListParameter.Add(HmiTextBox_Z.TextBox.Text)
                End If
                If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXY Then
                    lListParameter.Add(HmiTextBox_X.TextBox.Text)
                    lListParameter.Add(HmiTextBox_Y.TextBox.Text)
                End If
                cChangePage.ChangePage(iProgramUI.UI, "HmiButton_XYZ")
                iProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).InitParameter), clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).ControlParameter), lListParameter)
        End Select
    End Sub

    Public Sub BackPageChanged(ByVal strUIName As String)
        Select Case strUIName
            Case "HmiButton_Pic"
                If Not mPicturePosition.Cancel Then
                    HmiTextBox_PicturePosition.TextBox.Text = mPicturePosition.HmiTextBox_X.TextBox.Text + "," + mPicturePosition.HmiTextBox_Y.TextBox.Text + "," + mPicturePosition.HmiTextBox_Radius.TextBox.Text
                End If
            Case "HmiButton_XYZ"
                If Not cHMIX.ProgramUI.Cancel Then
                    Dim lListParameter As New List(Of String)
                    cHMIX.ProgramUI.GetParameter(cLocalElement, cSystemElement, Nothing, Nothing, lListParameter)

                    If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXZ Then
                        HmiTextBox_X.TextBox.Text = lListParameter(0)
                        HmiTextBox_Z.TextBox.Text = lListParameter(1)
                        HmiTextBox_Y.TextBox.Text = "0"
                    End If
                    If HmiComboBox_XYZ.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_XYZ.ComboBox.SelectedIndex + 1, GetType(clsHMIXY), GetType(clsHMIXZ)).Source Is clsHMIXY Then
                        HmiTextBox_X.TextBox.Text = lListParameter(0)
                        HmiTextBox_Y.TextBox.Text = lListParameter(1)
                        HmiTextBox_Z.TextBox.Text = "0"
                    End If

                End If
                cHMIX.ProgramUI.Quit(cLocalElement, cSystemElement)
        End Select
    End Sub

    Private Sub GetParamater()
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_PicturePosition.TextBox.Text)
        If HmiComboBox_AST.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_AST.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiComboBox_Pro.ComboBox.Text)
        If HmiComboBox_XYZ.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_XYZ.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_X.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Y.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Z.TextBox.Text)


        'Next
        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 3, (HmiComboBox_XYZ.ComboBox.SelectedIndex + 1).ToString) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If
        If lListInitParameter.Count >= 8 Then
            lListInitParameter(7) = bHasAction
        Else
            lListInitParameter.Add(bHasAction)
        End If
        lListInitParameter.Add(HmiTextBox_MESPosition.TextBox.Text)
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)

        Dim iCnt As Integer = 0
        lTargetListParameter = New List(Of String)
        For Each element As String In lListParameter
            lTargetListParameter.Add(element)
            iCnt = iCnt + 1
        Next

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(1), GetType(clsHMIAST))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(1) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(1) = ""
        End If

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(3), GetType(clsHMIXY), GetType(clsHMIXZ))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(3) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(3) = ""
        End If

        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 3, lTargetListParameter(3)) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If

        'Next
        If lTargetListParameter.Count >= 8 Then
            lTargetListParameter(7) = bHasAction
        Else
            lTargetListParameter.Add(bHasAction)
        End If
        If lTargetListParameter.Count < 9 Then
            lTargetListParameter.Add("")
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)


        lTargetListParameter = lListParameter
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(1)), GetType(clsHMIAST))
        lTargetListParameter(1) = cDeviceCfg.StationIndex
        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(3)), GetType(clsHMIXY), GetType(clsHMIXZ))
        lTargetListParameter(3) = cDeviceCfg.StationIndex


        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 3, lTargetListParameter(3)) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If

        'Next
        If lTargetListParameter.Count >= 8 Then
            lTargetListParameter(7) = bHasAction
        Else
            lTargetListParameter.Add(bHasAction)
        End If
        Return True
    End Function

    Public Function GetPicturePostion(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef strPosition As String) As Boolean Implements IScrewActionUI.GetPicturePostion
        If lListParameter.Count >= 1 Then
            strPosition = lListParameter(0)
        Else
            strPosition = ""
        End If

        Return True
    End Function
End Class