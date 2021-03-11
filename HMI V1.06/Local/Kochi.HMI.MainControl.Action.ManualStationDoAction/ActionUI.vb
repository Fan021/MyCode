Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing

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
    Protected cChangePage As clsChangePage
    Protected mPicturePosition As PicturePosition
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
        If Not IsNothing(cChangePage) Then RemoveHandler cChangePage.BackPageChanged, AddressOf BackPageChanged
        If Not IsNothing(mPicturePosition) Then mPicturePosition.Quit()
        Me.Dispose()
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.SetParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)
        
        If lListParameter.Count > 2 Then
            For i = 1 To lListParameter.Count - 1 Step 2
                MachineListView_Picture.Rows.Add((MachineListView_Picture.Rows.Count + 1).ToString, lListParameter(i), lListParameter(i + 1))
            Next
        End If
        RadioButton_Y.Checked = True
        If lListParameter.Count >= 1 Then
            RadioButton_Y.Checked = IIf(lListParameter(0).ToUpper = "TRUE", True, False)
            RadioButton_N.Checked = IIf(lListParameter(0).ToUpper = "TRUE", False, True)
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationDoAction", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationDoAction", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter.Count > 2 Then
            For i = 1 To lListParameter.Count - 1 Step 2
                If lListParameter(i) = "" Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationDoAction", "5", i.ToString), enumExceptionType.Alarm)
                End If
                If lListParameter(i + 1) = "" Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationDoAction", "6", (i + 1).ToString), enumExceptionType.Alarm)
                End If
            Next
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        AddHandler HmiTextBox_Picture.TextBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Name.Label.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiLabel_Name")
        HmiLabel_Name.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_Picture.Label.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiLabel_Picture")
        HmiLabel_Picture.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiLabel_Position.Label.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiLabel_Position")
        HmiLabel_Position.Label.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_Picture.Button.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiButton_Picture")
        HmiButton_Picture.Button.Font = New System.Drawing.Font("Calibri", 10.0!)

        HmiButton_Position.Button.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "HmiButton_Position")
        HmiButton_Position.Button.Font = New System.Drawing.Font("Calibri", 10.0!)

        RadioButton_Y.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "RadioButton_Y")
        RadioButton_Y.Font = New System.Drawing.Font("Calibri", 10.0!)

        RadioButton_N.Text = cLanguageManager.GetUserTextLine("ManualStationDoAction", "RadioButton_N")
        RadioButton_N.Font = New System.Drawing.Font("Calibri", 10.0!)

        MachineListView_Picture.Columns.Clear()
        MachineListView_Picture.Font = New System.Drawing.Font("Calibri", 10.0!)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ManualStationDoAction", "PostTest_id")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Picture.Columns.Add(PostTest_id)

        Dim PostTest_Pic As New DataGridViewTextBoxColumn
        PostTest_Pic = New DataGridViewTextBoxColumn
        PostTest_Pic.HeaderText = cLanguageManager.GetUserTextLine("ManualStationDoAction", "PostTest_Pic")
        PostTest_Pic.Name = "PostTest_Pic"
        PostTest_Pic.ReadOnly = True
        MachineListView_Picture.Columns.Add(PostTest_Pic)

        Dim PostTest_Position As New DataGridViewTextBoxColumn
        PostTest_Position = New DataGridViewTextBoxColumn
        PostTest_Position.HeaderText = cLanguageManager.GetUserTextLine("ManualStationDoAction", "PostTest_Position")
        PostTest_Position.Name = "PostTest_Position"
        PostTest_Position.ReadOnly = True
        MachineListView_Picture.Columns.Add(PostTest_Position)

        HmiButton_Picture.Button.Enabled = False
        HmiButton_Position.Button.Enabled = False
        HmiTextBox_Picture.TextBoxReadOnly = True
        HmiTextBox_Position.TextBoxReadOnly = True
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        AddHandler HmiButton_Position.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_Picture.Button.Click, AddressOf Button_Click
        AddHandler RadioButton_Y.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_N.CheckedChanged, AddressOf RadioButton_CheckedChanged
        Return True
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButton_Picture"
                Open()
            Case "HmiButton_Position"
                SetPosition()
        End Select
    End Sub

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iCnt As Integer = 0
        For Each element As RowStyle In TableLayoutPanel_Body_Mid_Right.RowStyles
            If iCnt > 0 Then
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Picture.TextBox.Height + 6 + 6
            End If
            iCnt = iCnt + 1
        Next
    End Sub


    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_Picture.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_Picture.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Picture.Width / 100) * 20
                Case "PostTest_Pic"
                    element.Width = (MachineListView_Picture.Width / 100) * 40
                Case "PostTest_Position"
                    element.Width = (MachineListView_Picture.Width / 100) * 40
            End Select
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

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        HmiButton_Picture.Button.Enabled = False
        HmiButton_Position.Button.Enabled = False
        MachineListView_Picture.Rows.Add((MachineListView_Picture.Rows.Count + 1).ToString, "", "")
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Picture.Rows.Count <= 0 Then Return
        HmiButton_Picture.Button.Enabled = False
        HmiButton_Position.Button.Enabled = False
        MachineListView_Picture.Rows.Remove(MachineListView_Picture.CurrentRow)
        For Each t As DataGridViewRow In MachineListView_Picture.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        If RadioButton_Y.Checked Then
            lListInitParameter.Add("TRUE")
        Else
            lListInitParameter.Add("FALSE")
        End If

        For i = 0 To MachineListView_Picture.Rows.Count - 1
            lListInitParameter.Add(MachineListView_Picture.Rows(i).Cells(1).Value)
            lListInitParameter.Add(MachineListView_Picture.Rows(i).Cells(2).Value)
        Next
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Public Sub Open()
        OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                         "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                          "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
        OpenFileDialog_Path.RestoreDirectory = True
        OpenFileDialog_Path.FilterIndex = 1
        If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
            HmiTextBox_Picture.TextBox.Text = OpenFileDialog_Path.FileName
            MachineListView_Picture.CurrentRow.Cells(1).Value = OpenFileDialog_Path.FileName
            If HmiTextBox_Picture.TextBox.Text <> "" Then HmiButton_Position.Button.Enabled = True
        End If

    End Sub

    Public Sub SetPosition()
        mPicturePosition = New PicturePosition
        mPicturePosition.Init(cLocalElement, cSystemElement)
        cChangePage.ChangePage(mPicturePosition.UI, "HmiButton_Pic")
        Dim ListPosition As New List(Of clsPictureComponentCfg)
        For i = 0 To MachineListView_Picture.Rows.Count - 1
            ListPosition.Add(New clsPictureComponentCfg(MachineListView_Picture.Rows(i).Cells(1).Value, MachineListView_Picture.Rows(i).Cells(2).Value))
        Next
        mPicturePosition.SetXYR(MachineListView_Picture.CurrentRow.Index, ListPosition)
    End Sub

    Public Sub BackPageChanged(ByVal strUIName As String)
        Select Case strUIName
            Case "HmiButton_Pic"
                If Not mPicturePosition.Cancel Then
                    HmiTextBox_Position.TextBox.Text = mPicturePosition.HmiTextBox_X.TextBox.Text + "," + mPicturePosition.HmiTextBox_Y.TextBox.Text + "," + mPicturePosition.HmiTextBox_Zoom.TextBox.Text
                    MachineListView_Picture.CurrentRow.Cells(2).Value = HmiTextBox_Position.TextBox.Text
                End If
        End Select
    End Sub

    Public Function ChangeIniToParameter(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeIniToParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal lListParameter As System.Collections.Generic.List(Of String), ByRef lTargetListParameter As System.Collections.Generic.List(Of String)) As Boolean Implements IActionUI.ChangeParameterToIni
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        lTargetListParameter = lListParameter
        Return True
    End Function

    Private Sub MachineListView_Picture_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MachineListView_Picture.CellClick
        If IsNothing(MachineListView_Picture.CurrentRow) Then Return
        If MachineListView_Picture.CurrentRow.Index <= MachineListView_Picture.Rows.Count - 1 Then
            HmiTextBox_Picture.Text = MachineListView_Picture.Rows(MachineListView_Picture.CurrentRow.Index).Cells(1).Value
            HmiTextBox_Position.Text = MachineListView_Picture.Rows(MachineListView_Picture.CurrentRow.Index).Cells(2).Value
            HmiButton_Picture.Button.Enabled = True
            If HmiTextBox_Picture.TextBox.Text <> "" Then HmiButton_Position.Button.Enabled = True
        End If
    End Sub
    Private Sub MachineListView_Picture_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MachineListView_Picture.CellValueChanged
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

End Class
