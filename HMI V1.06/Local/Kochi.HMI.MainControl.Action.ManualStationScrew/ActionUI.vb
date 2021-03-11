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
    Private cHMIPKP As clsHMIPKP
    Private cMachineStationCfg As clsMachineStationCfg
    Private cPictureManager As clsPictureManager
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
        cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
        AddHandler cChangePage.BackPageChanged, AddressOf BackPageChanged
        InitForm()
        InitControlText()
        Return True
    End Function


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IActionUI.Quit
        ' cChangePage.BackPage()
        If Not IsNothing(cChangePage) Then RemoveHandler cChangePage.BackPageChanged, AddressOf BackPageChanged
        If Not IsNothing(mPicturePosition) Then mPicturePosition.Quit()
        If Not IsNothing(iProgramUI) Then iProgramUI.Quit(cLocalElement, cSystemElement)
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Dim cVariantCfg As clsVariantCfg = CType(cLocalElement(clsVariantCfg.Name), clsVariantCfg)
        HmiTextBox_MESPosition.TextBox.Text = ""
        HmiTextBox_PicturePosition.TextBox.Text = ""
        HmiTextBox_X.TextBox.Text = "0.00"
        HmiTextBox_Y.TextBox.Text = "0.00"
        HmiTextBox_Z.TextBox.Text = "0.00"
        HmiTextBox_ToleranceX.TextBox.Text = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceX)
        HmiTextBox_ToleranceY.TextBox.Text = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceY)
        HmiTextBox_ToleranceZ.TextBox.Text = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceZ)
        HmiComboBox_AST.ComboBox.SelectedIndex = -1
        HmiComboBox_Pro.ComboBox.SelectedIndex = -1
        HmiComboBox_PKP.ComboBox.SelectedIndex = -1

        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        iParentProgramUI.ShowButton(enumProgramButtonType.HmiTextBox_Repeat)
        If cSubStepCfg.SubStepParameter(HMISubStepKeys.Repeat) = "" Then
            iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Screw_Repeat)
        End If
        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIAST))
        HmiComboBox_AST.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_AST.ComboBox.Items.Add(element.Name.ToString)
            Next
        End If

        HmiComboBox_Pro.ComboBox.Items.Clear()
        For i = 1 To 16
            HmiComboBox_Pro.ComboBox.Items.Add(i.ToString)
            HmiComboBox_ReworkPro.ComboBox.Items.Add(i.ToString)
        Next



        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        HmiComboBox_PKP.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_PKP.ComboBox.Items.Add(element.Name.ToString)
            Next
        End If

        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            HmiTextBox_PicturePosition.TextBox.Text = lListParameter(0)
        End If
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
            HmiComboBox_ReworkPro.ComboBox.Text = lListParameter(3)
        End If

        If lListParameter.Count >= 5 Then
            For iCnt = 1 To HmiComboBox_PKP.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(4) Then
                    HmiComboBox_PKP.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If

        If lListParameter.Count >= 6 Then
            HmiTextBox_X.TextBox.Text = lListParameter(5)
        End If
        If lListParameter.Count >= 7 Then
            HmiTextBox_ToleranceX.TextBox.Text = lListParameter(6)
        End If
        If lListParameter.Count >= 8 Then
            HmiTextBox_Y.TextBox.Text = lListParameter(7)
        End If
        If lListParameter.Count >= 9 Then
            HmiTextBox_ToleranceY.TextBox.Text = lListParameter(8)
        End If
        If lListParameter.Count >= 10 Then
            HmiTextBox_Z.TextBox.Text = lListParameter(9)
        End If
        If lListParameter.Count >= 11 Then
            HmiTextBox_ToleranceZ.TextBox.Text = lListParameter(10)
        End If
        If lListParameter.Count >= 13 Then
            HmiTextBox_MESPosition.TextBox.Text = lListParameter(12)
        End If

        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 9 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            If lListParameter.Count < 11 Then
                ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "1"), enumExceptionType.Alarm)
            End If
        End If
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "3"), enumExceptionType.Alarm)
        End If
        Dim cListInitParameter As New List(Of String)
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(1)), GetType(clsHMIAST))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "13", cMachineStationCfg.ID, CInt(lListParameter(1))), enumExceptionType.Alarm)
        End If
        cListInitParameter = clsParameter.ToList(cDeviceCfg.InitParameter)
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "4"), enumExceptionType.Alarm)
        End If
        'If lListParameter.Count < 4 Then
        '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "5"), enumExceptionType.Alarm)
        'End If
        'If lListParameter(3) = "" Then
        '    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "5"), enumExceptionType.Alarm)
        'End If
        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "6"), enumExceptionType.Alarm)
        End If
        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(4)), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "14", cMachineStationCfg.ID, CInt(lListParameter(4))), enumExceptionType.Alarm)
        End If

        Dim cPKP As clsHMIPKP = cDeviceCfg.Source
        If cPKP.ReWorkEnable Then
            If lListParameter.Count < 4 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "5"), enumExceptionType.Alarm)
            End If
            If lListParameter(3) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "5"), enumExceptionType.Alarm)
            End If
        End If
        If lListParameter.Count < 6 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(5) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 7 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "8"), enumExceptionType.Alarm)
        End If
        If lListParameter(6) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "8"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 8 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "9"), enumExceptionType.Alarm)
        End If
        If lListParameter(7) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "9"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 9 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "10"), enumExceptionType.Alarm)
        End If
        If lListParameter(8) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "10"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            If lListParameter.Count < 10 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "11"), enumExceptionType.Alarm)
            End If
            If lListParameter(9) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "11"), enumExceptionType.Alarm)
            End If
            If lListParameter.Count < 11 Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "12"), enumExceptionType.Alarm)
            End If
            If lListParameter(10) = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationScrew", "12"), enumExceptionType.Alarm)
            End If
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean

        HmiLabel_MESPosition.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_MESPosition")
        HmiLabel_MESPosition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_PicturePosition.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_PicturePosition")
        HmiLabel_PicturePosition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_AST.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_AST")
        HmiLabel_AST.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Pro.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_Pro")
        HmiLabel_Pro.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        Label_ReworkPro.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "Label_ReworkPro")
        Label_ReworkPro.TextAlign = ContentAlignment.MiddleRight
        Label_ReworkPro.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_PKP.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_PKP")
        HmiLabel_PKP.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Y.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_Y")
        HmiLabel_Y.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceY.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_ToleranceY")
        HmiLabel_ToleranceY.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceZ.Label.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiLabel_ToleranceZ")
        HmiLabel_ToleranceZ.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Pic.Button.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiButton_Pic")
        HmiButton_Pic.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_PKP.Button.Text = cLanguageManager.GetUserTextLine("ManualStationScrew", "HmiButton_PKP")
        HmiButton_PKP.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_AST.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_PKP.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Pro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_ReworkPro.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceY.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceZ.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_X.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Y.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Z.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_MESPosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiTextBox_PicturePosition.TextBoxReadOnly = True

        If iParentProgramUI.TextBox_Picture.TextBox.Text <> "" Then
            HmiButton_Pic.Button.Enabled = True
        Else
            HmiButton_Pic.Button.Enabled = False
        End If

        Dim lListKey As New Dictionary(Of String, String)
        lListKey.Clear()
        For Each ElementIndex As Integer In cPictureManager.GetPictureListKey
            Dim Element As clsPictureCfg = cPictureManager.GetPictureCfgFromKey(ElementIndex)
            lListKey.Add(Element.Key, Element.Path.Split("\").Last)
        Next


        HmiTextBox_X.ValueType = GetType(Double)
        HmiTextBox_Y.ValueType = GetType(Double)
        HmiTextBox_Z.ValueType = GetType(Double)
        HmiTextBox_ToleranceX.ValueType = GetType(Double)
        HmiTextBox_ToleranceY.ValueType = GetType(Double)
        HmiTextBox_ToleranceZ.ValueType = GetType(Double)

        HmiButton_PKP.Button.Enabled = False
        AddHandler HmiTextBox_PicturePosition.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiButton_Pic.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_PKP.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_PicturePosition.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_X.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Y.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Z.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceY.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceZ.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_MESPosition.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiComboBox_AST.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_PKP.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Pro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_ReworkPro.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
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
            Case "HmiComboBox_PKP"
                If HmiComboBox_PKP.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
                    HmiLabel_Z.Show()
                    HmiLabel_ToleranceZ.Show()
                    HmiTextBox_ToleranceZ.Show()
                    HmiTextBox_Z.Show()
                    TableLayoutPanel_Body.RowStyles(7).Height = HmiTextBox_PicturePosition.TextBox.Height + 6 + 6
                Else
                    HmiLabel_Z.Hide()
                    HmiLabel_ToleranceZ.Hide()
                    HmiTextBox_ToleranceZ.Hide()
                    HmiTextBox_Z.Hide()
                    TableLayoutPanel_Body.RowStyles(7).Height = 0
                End If

                If HmiComboBox_AST.ComboBox.SelectedIndex <> HmiComboBox_PKP.ComboBox.SelectedIndex And HmiButton_PKP.Button.Enabled Then
                    If HmiComboBox_AST.ComboBox.Items.Count - 1 >= HmiComboBox_PKP.ComboBox.SelectedIndex Then
                        HmiComboBox_AST.ComboBox.SelectedIndex = HmiComboBox_PKP.ComboBox.SelectedIndex
                    Else
                        HmiComboBox_AST.ComboBox.SelectedIndex = -1
                    End If
                End If
                HmiButton_PKP.Button.Enabled = True

            Case "HmiComboBox_AST"
                If HmiComboBox_AST.ComboBox.SelectedIndex <> HmiComboBox_PKP.ComboBox.SelectedIndex Then
                    If HmiComboBox_PKP.ComboBox.Items.Count - 1 >= HmiComboBox_AST.ComboBox.SelectedIndex Then
                        HmiButton_PKP.Button.Enabled = True
                        HmiComboBox_PKP.ComboBox.SelectedIndex = HmiComboBox_AST.ComboBox.SelectedIndex
                    Else
                        HmiButton_PKP.Button.Enabled = False
                        HmiComboBox_PKP.ComboBox.SelectedIndex = -1
                    End If
                End If

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
            Case "HmiButton_PKP"
                cHMIPKP = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source
                cHMIPKP.CreateProgramUI(cLocalElement, cSystemElement)
                iProgramUI = cHMIPKP.ProgramUI
                iProgramUI.Init(cLocalElement, cSystemElement)
                Dim lListParameter As New List(Of String)
                lListParameter.Clear()
                lListParameter.Add(HmiComboBox_AST.ComboBox.SelectedIndex + 1)
                lListParameter.Add(HmiComboBox_Pro.ComboBox.Text)
                lListParameter.Add(HmiTextBox_X.TextBox.Text)
                lListParameter.Add(HmiTextBox_ToleranceX.TextBox.Text)
                lListParameter.Add(HmiTextBox_Y.TextBox.Text)
                lListParameter.Add(HmiTextBox_ToleranceY.TextBox.Text)
                If HmiComboBox_PKP.ComboBox.Text <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
                    lListParameter.Add(HmiTextBox_Z.TextBox.Text)
                    lListParameter.Add(HmiTextBox_ToleranceZ.TextBox.Text)
                End If
                cChangePage.ChangePage(iProgramUI.UI, "HmiButton_PKP")
                iProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).InitParameter), clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKP.ComboBox.SelectedIndex + 1, GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).ControlParameter), lListParameter)

        End Select
    End Sub


    Public Sub BackPageChanged(ByVal strUIName As String)
        Select Case strUIName
            Case "HmiButton_Pic"
                If Not mPicturePosition.Cancel Then
                    HmiTextBox_PicturePosition.TextBox.Text = mPicturePosition.HmiTextBox_X.TextBox.Text + "," + mPicturePosition.HmiTextBox_Y.TextBox.Text + "," + mPicturePosition.HmiTextBox_Radius.TextBox.Text
                End If
            Case "HmiButton_PKP"
                If Not cHMIPKP.ProgramUI.Cancel Then
                    Dim lListParameter As New List(Of String)
                    cHMIPKP.ProgramUI.GetParameter(cLocalElement, cSystemElement, Nothing, Nothing, lListParameter)
                    HmiTextBox_X.TextBox.Text = lListParameter(0)
                    HmiTextBox_ToleranceX.TextBox.Text = lListParameter(1)
                    HmiTextBox_Y.TextBox.Text = lListParameter(2)
                    HmiTextBox_ToleranceY.TextBox.Text = lListParameter(3)
                    If lListParameter.Count >= 5 Then
                        HmiTextBox_Z.TextBox.Text = lListParameter(4)
                    Else
                        HmiTextBox_Z.TextBox.Text = ""
                    End If

                    If lListParameter.Count >= 6 Then
                        HmiTextBox_ToleranceZ.TextBox.Text = lListParameter(5)
                    Else
                        HmiTextBox_ToleranceZ.TextBox.Text = ""
                    End If

                End If
                cHMIPKP.ProgramUI.Quit(cLocalElement, cSystemElement)
        End Select
    End Sub

    Private Sub GetParamater()
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_PicturePosition.TextBox.Text)
        If HmiComboBox_AST.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_AST.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiComboBox_Pro.ComboBox.Text)
        lListInitParameter.Add(HmiComboBox_ReworkPro.ComboBox.Text)
        If HmiComboBox_PKP.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_PKP.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_X.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ToleranceX.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Y.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ToleranceY.TextBox.Text)
        If lListInitParameter(4) <> "" AndAlso Not IsNothing(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListInitParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))) AndAlso TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lListInitParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            lListInitParameter.Add(HmiTextBox_Z.TextBox.Text)
            lListInitParameter.Add(HmiTextBox_ToleranceZ.TextBox.Text)
        Else
            lListInitParameter.Add("0")
            lListInitParameter.Add("0")
        End If


        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 4, (HmiComboBox_PKP.ComboBox.SelectedIndex + 1).ToString) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If

        'Next
        If lListInitParameter.Count >= 12 Then
            lListInitParameter(11) = bHasAction
        Else
            lListInitParameter.Add(bHasAction)
        End If
        lListInitParameter.Add(HmiTextBox_MESPosition.TextBox.Text)
    End Sub

    Public Function GetPicturePostion(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef strPosition As String) As Boolean Implements IScrewActionUI.GetPicturePostion
        If lListParameter.Count >= 1 Then
            strPosition = lListParameter(0)
        Else
            strPosition = ""
        End If

        Return True
    End Function

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Dim cVariantCfg As clsVariantCfg = CType(cLocalElement(clsVariantCfg.Name), clsVariantCfg)
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        Dim iCnt As Integer = 0
        lTargetListParameter = New List(Of String)
        For Each element As String In lListParameter
            lTargetListParameter.Add(element)
        Next

        If lTargetListParameter.Count > 6 AndAlso lTargetListParameter(6) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceX + "]" Then
            lTargetListParameter(6) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceX)
        End If
        If lTargetListParameter.Count > 8 AndAlso lTargetListParameter(8) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceY + "]" Then
            lTargetListParameter(8) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceY)
        End If

        If lTargetListParameter.Count > 10 AndAlso lTargetListParameter(10) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceZ + "]" Then
            lTargetListParameter(10) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceZ)
        End If
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(1), GetType(clsHMIAST))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(1) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(1) = ""
        End If

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(4), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(4) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(4) = ""
        End If


        'Next
        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 4, lTargetListParameter(4)) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If
        If lTargetListParameter.Count >= 12 Then
            lTargetListParameter(11) = bHasAction
        Else
            lTargetListParameter.Add(bHasAction)
        End If

        If lTargetListParameter.Count < 13 Then
            lTargetListParameter.Add("")
        End If
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        Dim cVariantCfg As clsVariantCfg = CType(cLocalElement(clsVariantCfg.Name), clsVariantCfg)
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)

        lTargetListParameter = lListParameter
        If lTargetListParameter(6) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceX) Then
            lTargetListParameter(6) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceX + "]"
        End If
        If lTargetListParameter(8) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceY) Then
            lTargetListParameter(8) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceY + "]"
        End If
        If TypeOf cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(4)), GetType(clsHMIPKP), GetType(clsHMIPKP_Z)).Source Is clsHMIPKP_Z Then
            If lTargetListParameter(10) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_ToleranceZ) Then
                lTargetListParameter(10) = "[" + clsHMIGlobalParameter.Manual_Screw_ToleranceZ + "]"
            End If
        End If
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(1)), GetType(clsHMIAST))
        lTargetListParameter(1) = cDeviceCfg.StationIndex

        cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(4)), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
        lTargetListParameter(4) = cDeviceCfg.StationIndex

        'Next
        Dim bHasAction As String = String.Empty
        If cActionManager.HasAction(cMachineStationCfg.ID, cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType), cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 4, lTargetListParameter(4)) Then
            bHasAction = "TRUE"
        Else
            bHasAction = "FALSE"
        End If
        If lTargetListParameter.Count >= 12 Then
            lTargetListParameter(11) = bHasAction
        Else
            lTargetListParameter.Add(bHasAction)
        End If
        Return True
    End Function
End Class