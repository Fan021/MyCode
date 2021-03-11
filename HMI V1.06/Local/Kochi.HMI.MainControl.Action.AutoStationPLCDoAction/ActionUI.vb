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
    Private strOldType As String = String.Empty
    Private strOldParameter As String = String.Empty
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
        Dim iIndex As Integer = 2
        HmiComboBox_Type.ComboBox.SelectedIndex = -1
        iParentProgramUI.SetRepeat(enumProgramCounType.Manual_Continue)
        If lListParameter.Count >= 1 Then
            For i = 0 To HmiComboBox_Type.ComboBox.Items.Count - 1
                If HmiComboBox_Type.ComboBox.Items(i).ToString = lListParameter(0) Then
                    HmiComboBox_Type.ComboBox.SelectedIndex = i
                    strOldType = lListParameter(0)
                    iIndex = i + iIndex
                End If
            Next
        End If

        MachineListView_Parameter.Rows.Clear()
        If lListParameter.Count >= 2 Then
            Dim strValue As String = cMachineManager.ActionParameterManager.ListElement("AutoStationPlcDoAction")
            Dim cValue() As String = strValue.Split("|")
            If cValue.Count - 1 >= iIndex Then
                Dim cTypeValue() As String = cValue(iIndex).Split(";")
                If cValue(iIndex) <> "" Then
                    For j = 0 To cTypeValue.Count - 1
                        Dim cParameterValue() As String = cTypeValue(j).Split(",")
                        For k = 1 To lListParameter.Count - 1 Step 2
                            If lListParameter(k) = cParameterValue(0) Then
                                MachineListView_Parameter.Rows.Add((MachineListView_Parameter.Rows.Count + 1).ToString, lListParameter(k), lListParameter(k + 1))
                            End If
                        Next
                    Next
                End If
            End If
            strOldParameter = clsParameter.ToString(lListParameter)
        End If
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IActionUI.CheckParameter
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStationCfg = CType(cLocalElement(clsMachineStationCfg.Name), clsMachineStationCfg)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        If lListParameter.Count < 1 Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "2"), enumExceptionType.Alarm)
        End If

        If lListParameter(0) = "" Then
            Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "2"), enumExceptionType.Alarm)
        End If

        For i = 0 To MachineListView_Parameter.Rows.Count - 1
            If MachineListView_Parameter.Rows(i).Cells(2).Value = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "6", MachineListView_Parameter.Rows(i).Cells(1).Value), enumExceptionType.Alarm)
            End If
        Next

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Name.Label.Text = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "HmiLabel_Name")
        HmiLabel_Name.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        '  HmiLabel_Parameter.Label.Text = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "HmiLabel_Parameter")
        HmiLabel_Name.Label.Font = New System.Drawing.Font("Calibri", 10.0!)
        MachineListView_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Type.ComboBox.Font = New System.Drawing.Font("Calibri", 10.0!)
        HmiComboBox_Type.ComboBox.Items.Clear()


        MachineListView_Parameter.Columns.Clear()
        MachineListView_Parameter.Font = New System.Drawing.Font("Calibri", 10.0!)
        MachineListView_Parameter.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", 10.0!)
        MachineListView_Parameter.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", 10.0!)
        MachineListView_Parameter.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", 10.0!)

        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "PostTest_id")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Parameter.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name = New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "PostTest_Name")
        PostTest_Name.Name = "PostTest_Name"
        PostTest_Name.ReadOnly = True
        MachineListView_Parameter.Columns.Add(PostTest_Name)

        Dim PostTest_Value As New DataGridViewTextBoxColumn
        PostTest_Value = New DataGridViewTextBoxColumn
        PostTest_Value.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "PostTest_Value")
        PostTest_Value.Name = "PostTest_Value"
        PostTest_Value.ReadOnly = False
        MachineListView_Parameter.Columns.Add(PostTest_Value)

        If cMachineManager.ActionParameterManager.ListElement.ContainsKey("AutoStationPlcDoAction") Then
            Dim strValue As String = cMachineManager.ActionParameterManager.ListElement("AutoStationPlcDoAction")
            Dim cValue() As String = strValue.Split("|")
            If cValue.Count >= 2 Then
                Dim cTypeValue() As String = cValue(1).Split(";")
                For i = 0 To cTypeValue.Count - 1
                    If cTypeValue(i) = "" Then Continue For
                    HmiComboBox_Type.ComboBox.Items.Add(cTypeValue(i))
                Next
            End If

        End If

        AddHandler HmiComboBox_Type.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler HmiComboBox_Type.ComboBox.SizeChanged, AddressOf TextBoxValue_SizeChanged
        Return True
    End Function

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MachineListView_Parameter.Rows.Add((MachineListView_Parameter.Rows.Count + 1).ToString, "")
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Parameter.Rows.Count <= 0 Then Return
        MachineListView_Parameter.Rows.Remove(MachineListView_Parameter.CurrentRow)
        For Each t As DataGridViewRow In MachineListView_Parameter.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub TextBoxValue_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim iCnt As Integer = 0
        For Each element As RowStyle In TableLayoutPanel_Body.RowStyles
            If iCnt > 0 Then Continue For
            element.SizeType = System.Windows.Forms.SizeType.Absolute
            element.Height = HmiComboBox_Type.ComboBox.Height + 6 + 6
            iCnt = iCnt + 1
        Next
    End Sub
    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If HmiComboBox_Type.ComboBox.Text <> "" Then
            Dim iIndex As Integer = 2 + HmiComboBox_Type.ComboBox.SelectedIndex
            Dim lListParameter As New List(Of String)
            If strOldType = HmiComboBox_Type.ComboBox.Text Then
                lListParameter = clsParameter.ToList(strOldParameter)
            End If
            MachineListView_Parameter.Rows.Clear()
            If lListParameter.Count >= 2 Then
                Dim strValue As String = cMachineManager.ActionParameterManager.ListElement("AutoStationPlcDoAction")
                Dim cValue() As String = strValue.Split("|")
                If cValue.Count - 1 >= iIndex Then
                    Dim cTypeValue() As String = cValue(iIndex).Split(";")
                    If cValue(iIndex) <> "" Then
                        For j = 0 To cTypeValue.Count - 1
                            For k = 1 To lListParameter.Count - 1 Step 2
                                If lListParameter(k) = cTypeValue(j) Then
                                    MachineListView_Parameter.Rows.Add((MachineListView_Parameter.Rows.Count + 1).ToString, lListParameter(k), lListParameter(k + 1))
                                End If
                            Next
                        Next
                    End If
                End If
            Else
                Dim strValue As String = cMachineManager.ActionParameterManager.ListElement("AutoStationPlcDoAction")
                Dim cValue() As String = strValue.Split("|")
                If cValue.Count - 1 >= iIndex Then
                    Dim cTypeValue() As String = cValue(iIndex).Split(";")
                    If cValue(iIndex) <> "" Then
                        For j = 0 To cTypeValue.Count - 1
                            Dim cParameterValue() As String = cTypeValue(j).Split(",")
                            If cParameterValue.Length >= 2 Then
                                MachineListView_Parameter.Rows.Add((MachineListView_Parameter.Rows.Count + 1).ToString, cParameterValue(0), cParameterValue(1))
                            Else
                                MachineListView_Parameter.Rows.Add((MachineListView_Parameter.Rows.Count + 1).ToString, cTypeValue(j), "")
                            End If

                        Next
                    End If
                End If
            End If
        End If
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub MachineListView_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MachineListView_Parameter.CellValueChanged
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListInitParameter))
    End Sub

    Private Sub GetParamater()
        lListInitParameter.Clear()
        lListInitParameter.Add(HmiComboBox_Type.ComboBox.Text)
        For i = 0 To MachineListView_Parameter.Rows.Count - 1
            lListInitParameter.Add(MachineListView_Parameter.Rows(i).Cells(1).Value)
            lListInitParameter.Add(MachineListView_Parameter.Rows(i).Cells(2).Value)
        Next
        If strOldType = HmiComboBox_Type.ComboBox.Text And HmiComboBox_Type.ComboBox.Text <> "" Then
            strOldParameter = clsParameter.ToString(lListInitParameter)
        End If
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
End Class
