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
    Private cHMIPKS As clsHMIPKS
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

        HmiTextBox_PicturePosition.TextBox.Text = ""
        HmiTextBox_X.TextBox.Text = "0.00"
        HmiTextBox_R.TextBox.Text = "0.00"
        HmiTextBox_Z.TextBox.Text = "0.00"
        HmiTextBox_ToleranceX.TextBox.Text = "0.00"
        HmiTextBox_ToleranceR.TextBox.Text = "0.00"
        HmiTextBox_ToleranceZ.TextBox.Text = "0.00"
        HmiComboBox_PKS.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)

        Dim lListDeviceCfg As List(Of clsDeviceCfg)

        lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(cMachineStationCfg.ID, GetType(clsHMIPKS))
        HmiComboBox_PKS.ComboBox.Items.Clear()
        If Not IsNothing(lListDeviceCfg) Then
            For Each element As clsDeviceCfg In lListDeviceCfg
                HmiComboBox_PKS.ComboBox.Items.Add(element.Name.ToString)
            Next
        End If

        Dim iCnt As Integer = 0
        If lListParameter.Count >= 1 Then
            HmiTextBox_PicturePosition.TextBox.Text = lListParameter(0)
        End If

        If lListParameter.Count >= 2 Then
            For iCnt = 1 To HmiComboBox_PKS.ComboBox.Items.Count
                If iCnt.ToString = lListParameter(1) Then
                    HmiComboBox_PKS.ComboBox.SelectedIndex = iCnt - 1
                End If
            Next
        End If

        If lListParameter.Count >= 3 Then
            HmiTextBox_X.TextBox.Text = lListParameter(2)
        End If
        If lListParameter.Count >= 4 Then
            HmiTextBox_ToleranceX.TextBox.Text = lListParameter(3)
        End If
        If lListParameter.Count >= 5 Then
            HmiTextBox_R.TextBox.Text = lListParameter(4)
        End If
        If lListParameter.Count >= 6 Then
            HmiTextBox_ToleranceR.TextBox.Text = lListParameter(5)
        End If
        If lListParameter.Count >= 7 Then
            HmiTextBox_Z.TextBox.Text = lListParameter(6)
        End If
        If lListParameter.Count >= 8 Then
            HmiTextBox_ToleranceZ.TextBox.Text = lListParameter(7)
        End If

        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        If lListParameter.Count < 9 Then
            ' Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "1"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "2"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 2 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "3"), enumExceptionType.Alarm)
        End If
        If lListParameter(1) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "3"), enumExceptionType.Alarm)
        End If
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lListParameter(1)), GetType(clsHMIPKS))
        If IsNothing(cDeviceCfg) Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "10", cMachineStationCfg.ID, CInt(lListParameter(1))), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 3 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter(2) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "4"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 4 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter(3) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "5"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 5 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter(4) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "6"), enumExceptionType.Alarm)
        End If
        If lListParameter.Count < 6 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "7"), enumExceptionType.Alarm)
        End If
        If lListParameter(5) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "7"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 7 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "8"), enumExceptionType.Alarm)
        End If
        If lListParameter(6) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "8"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count < 8 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "9"), enumExceptionType.Alarm)
        End If
        If lListParameter(7) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationConnect", "9"), enumExceptionType.Alarm)
        End If
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_PicturePosition.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_PicturePosition")
        HmiLabel_PicturePosition.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_PKS.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_PKS")
        HmiLabel_PKS.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_X.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_X")
        HmiLabel_X.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_R.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_R")
        HmiLabel_R.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_Z.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_Z")
        HmiLabel_Z.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceX.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_ToleranceX")
        HmiLabel_ToleranceX.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceR.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_ToleranceR")
        HmiLabel_ToleranceR.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiLabel_ToleranceZ.Label.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiLabel_ToleranceZ")
        HmiLabel_ToleranceZ.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_Pic.Button.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiButton_Pic")
        HmiButton_Pic.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiButton_PKS.Button.Text = cLanguageManager.GetUserTextLine("ManualStationConnect", "HmiButton_PKS")
        HmiButton_PKS.Button.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_PKS.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceX.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceR.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_ToleranceZ.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_X.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_R.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_Z.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiTextBox_PicturePosition.TextBox.Font = New System.Drawing.Font("Calibri", 10.0!)


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
        HmiTextBox_R.ValueType = GetType(Double)
        HmiTextBox_Z.ValueType = GetType(Double)
        HmiTextBox_ToleranceX.ValueType = GetType(Double)
        HmiTextBox_ToleranceR.ValueType = GetType(Double)
        HmiTextBox_ToleranceZ.ValueType = GetType(Double)

        HmiButton_PKS.Button.Enabled = False
        AddHandler HmiTextBox_PicturePosition.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        AddHandler HmiButton_Pic.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_PKS.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_PicturePosition.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_X.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_R.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_Z.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceX.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceR.TextBox.TextChanged, AddressOf TextBox_TextChanged
        AddHandler HmiTextBox_ToleranceZ.TextBox.TextChanged, AddressOf TextBox_TextChanged

        AddHandler HmiComboBox_PKS.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
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
            Case "HmiComboBox_PKS"
                HmiButton_PKS.Button.Enabled = True

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
            Case "HmiButton_PKS"
                cHMIPKS = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKS.ComboBox.SelectedIndex + 1, GetType(clsHMIPKS)).Source
                cHMIPKS.CreateProgramUI(cLocalElement, cSystemElement)
                iProgramUI = cHMIPKS.ProgramUI
                iProgramUI.Init(cLocalElement, cSystemElement)
                Dim lListParameter As New List(Of String)
                lListParameter.Clear()
                lListParameter.Add(HmiTextBox_X.TextBox.Text)
                lListParameter.Add(HmiTextBox_ToleranceX.TextBox.Text)
                lListParameter.Add(HmiTextBox_R.TextBox.Text)
                lListParameter.Add(HmiTextBox_ToleranceR.TextBox.Text)
                lListParameter.Add(HmiTextBox_Z.TextBox.Text)
                lListParameter.Add(HmiTextBox_ToleranceZ.TextBox.Text)
                cChangePage.ChangePage(iProgramUI.UI, "HmiButton_PKS")
                iProgramUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKS.ComboBox.SelectedIndex + 1, GetType(clsHMIPKS)).InitParameter), clsParameter.ToList(cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, HmiComboBox_PKS.ComboBox.SelectedIndex + 1, GetType(clsHMIPKS)).ControlParameter), lListParameter)

        End Select
    End Sub


    Public Sub BackPageChanged(ByVal strUIName As String)
        Select Case strUIName
            Case "HmiButton_Pic"
                If Not mPicturePosition.Cancel Then
                    HmiTextBox_PicturePosition.TextBox.Text = mPicturePosition.HmiTextBox_X.TextBox.Text + "," + mPicturePosition.HmiTextBox_Y.TextBox.Text + "," + mPicturePosition.HmiTextBox_Radius.TextBox.Text
                End If
            Case "HmiButton_PKS"
                If Not cHMIPKS.ProgramUI.Cancel Then
                    Dim lListParameter As New List(Of String)
                    cHMIPKS.ProgramUI.GetParameter(cLocalElement, cSystemElement, Nothing, Nothing, lListParameter)
                    HmiTextBox_X.TextBox.Text = lListParameter(0)
                    HmiTextBox_ToleranceX.TextBox.Text = lListParameter(1)
                    HmiTextBox_R.TextBox.Text = lListParameter(2)
                    HmiTextBox_ToleranceR.TextBox.Text = lListParameter(3)
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
                cHMIPKS.ProgramUI.Quit(cLocalElement, cSystemElement)
        End Select
    End Sub

    Private Sub GetParamater()
        Dim cActionManager As clsActionManager = CType(cLocalElement(clsActionManager.Name), clsActionManager)
        Dim cSubStepCfg As clsSubStepCfg = CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg)
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiTextBox_PicturePosition.TextBox.Text)
        If HmiComboBox_PKS.ComboBox.SelectedIndex >= 0 Then
            lListInitParameter.Add(HmiComboBox_PKS.ComboBox.SelectedIndex + 1)
        Else
            lListInitParameter.Add("")
        End If
        lListInitParameter.Add(HmiTextBox_X.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ToleranceX.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_R.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ToleranceR.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_Z.TextBox.Text)
        lListInitParameter.Add(HmiTextBox_ToleranceZ.TextBox.Text)

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
        lTargetListParameter = lListParameter

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, lTargetListParameter(1), GetType(clsHMIPKS))
        If Not IsNothing(cDeviceCfg) Then
            lTargetListParameter(1) = cDeviceCfg.StationIndex.ToString
        Else
            lTargetListParameter(1) = ""
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

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cMachineStationCfg.ID, CInt(lTargetListParameter(1)), GetType(clsHMIPKS))
        lTargetListParameter(1) = cDeviceCfg.StationIndex
        Return True
    End Function
End Class