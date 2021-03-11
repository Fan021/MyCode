Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ParameterUI
    Implements IParameterUI
    Protected lListParameterParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cHMIPKP As clsHMIPKP
    Private lListPage As New Dictionary(Of String, TabPage)
    Private lListForm As New Dictionary(Of String, AddForm)
    Private lListParameter As New List(Of String)
    Public ReadOnly Property UI As Panel Implements IParameterUI.UI
        Get
            Return Pandel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParameterUI.Init
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParameterUI.Quit

        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IParameterUI.SetParameter
        RemoveHandler MachineListView_ErrorCode_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        RemoveHandler MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged

        Me.lListParameter = lListParameter
        Dim iIndex As Integer = 2
        If lListParameter.Count >= 1 Then
            Dim cValueErrorCode() As String = lListParameter(0).Split(";")
            For i = 0 To cValueErrorCode.Count - 1
                If i <= MachineListView_ErrorCode_Value.Rows.Count - 1 Then
                    Dim cTempValue() As String = cValueErrorCode(i).Split(",")
                    If cTempValue.Count <> 2 Then Continue For
                    If cTempValue(0) = MachineListView_ErrorCode_Value.Rows(i).Cells(1).Value Then
                        MachineListView_ErrorCode_Value.Rows(i).Cells(2).Value = cTempValue(1)
                    End If
                End If
            Next
        End If
        If lListParameter.Count >= 2 AndAlso lListParameter(1) <> "" Then
            Dim cValueType() As String = lListParameter(1).Split(";")
            For i = 0 To cValueType.Count - 1
                MachineListView_Type_Value.Rows.Add(MachineListView_Type_Value.Rows.Count + 1, cValueType(i))
                Dim cTapPage As New TabPage
                cTapPage.Name = cValueType(i)
                cTapPage.Text = cValueType(i)
                cTapPage.Font = TabPage1.Font
                Dim cAddForm As New AddForm
                cAddForm.ControlName = (i + 3).ToString
                cAddForm.FontSize = TabControl_Parameter.Font
                cAddForm.Init(cLocalElement, cSystemElement)
                Dim strParameter As String = String.Empty
                If lListParameter.Count > iIndex Then
                    strParameter = lListParameter(iIndex)
                Else
                    strParameter = ""
                End If
                cAddForm.SetParameter(cLocalElement, cSystemElement, strParameter)
                cTapPage.Controls.Add(cAddForm.UI)
                TabControl_Parameter.Controls.Add(cTapPage)
                AddHandler cAddForm.MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Parameter_CellValueChanged
                AddHandler cAddForm.ParameterChanged, AddressOf AddForm_ParameterChanged
                iIndex = iIndex + 1
                lListPage.Add(iIndex.ToString, cTapPage)
                lListForm.Add(iIndex.ToString, cAddForm)
            Next
        End If
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
        AddHandler MachineListView_ErrorCode_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IParameterUI.CheckParameter
        If lListParameter.Count >= 2 AndAlso lListParameter(1) <> "" Then
            Dim cValueType() As String = lListParameter(1).Split(";")
            For i = 0 To cValueType.Count - 1
                Dim cValue() As String = cValueType(i).Split(",")
                If cValue(0) = "" Then
                    Throw New clsHMIException(cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "5"), enumExceptionType.Alarm)
                End If
            Next
        End If

        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        TabControl_Parameter.Font = New System.Drawing.Font("Calibri", 12.0!)
        MachineListView_ErrorCode_Value.Columns.Clear()
        MachineListView_ErrorCode_Value.Font = New System.Drawing.Font("Calibri", 12.0!)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_id)

        Dim PostTest_ErrorType As New DataGridViewTextBoxColumn
        PostTest_ErrorType.HeaderText = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "ErrorType")
        PostTest_ErrorType.Name = "PostTest_ErrorType"
        PostTest_ErrorType.ReadOnly = True
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_ErrorType)

        Dim PostTest_ErrorCode As New DataGridViewTextBoxColumn
        PostTest_ErrorCode.HeaderText = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "ErrorCode")
        PostTest_ErrorCode.Name = "PostTest_ErrorCode"
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_ErrorCode)


        MachineListView_Type_Value.Columns.Clear()
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Type_Value.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "Name")
        PostTest_Name.Name = "PostTest_name"
        PostTest_Name.ReadOnly = False
        MachineListView_Type_Value.Columns.Add(PostTest_Name)

        TabPage1.Text = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("ManualStationPlcDoAction", "TabPage2")

        For Each eType As enumPlcDoActionErrorType In [Enum].GetValues(GetType(enumPlcDoActionErrorType))
            MachineListView_ErrorCode_Value.Rows.Add(MachineListView_ErrorCode_Value.Rows.Count + 1, eType.ToString, "")
        Next

        AddHandler MachineListView_ErrorCode_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_ErrorCode_Value.Resize, AddressOf MachineListView_Parameter_Resize
        AddHandler MachineListView_Type_Value.Resize, AddressOf MachineListView_Parameter_Resize
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        Return True
    End Function

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "MachineListView_ErrorCode_Value"
                For Each element As DataGridViewTextBoxColumn In MachineListView_ErrorCode_Value.Columns
                    Select Case element.Name
                        Case "PostTest_id"
                            element.Width = (MachineListView_ErrorCode_Value.Width / 100) * 20
                        Case "PostTest_name"
                            element.Width = (MachineListView_ErrorCode_Value.Width / 100) * 30
                        Case "PostTest_value"
                            element.Width = (MachineListView_ErrorCode_Value.Width / 100) * 50
                    End Select
                Next
            Case "MachineListView_Type_Value"
                For Each element As DataGridViewTextBoxColumn In MachineListView_Type_Value.Columns
                    Select Case element.Name
                        Case "PostTest_id"
                            element.Width = (MachineListView_Type_Value.Width / 100) * 20
                        Case "PostTest_name"
                            element.Width = (MachineListView_Type_Value.Width / 100) * 80
                    End Select
                Next
        End Select

    End Sub

    Private Sub GetParamater()
        lListParameterParameter.Clear()
        Dim strErrorCode As String = String.Empty
        For i = 0 To MachineListView_ErrorCode_Value.Rows.Count - 1
            If strErrorCode = "" Then
                strErrorCode = MachineListView_ErrorCode_Value.Rows(i).Cells(1).Value + "," + MachineListView_ErrorCode_Value.Rows(i).Cells(2).Value
            Else
                strErrorCode = strErrorCode + ";" + MachineListView_ErrorCode_Value.Rows(i).Cells(1).Value + "," + MachineListView_ErrorCode_Value.Rows(i).Cells(2).Value
            End If
        Next
        lListParameterParameter.Add(strErrorCode)
        Dim strType As String = String.Empty
        For i = 0 To MachineListView_Type_Value.Rows.Count - 1
            If strType = "" Then
                strType = MachineListView_Type_Value.Rows(i).Cells(1).Value
            Else
                strType = strType + ";" + MachineListView_Type_Value.Rows(i).Cells(1).Value
            End If
        Next
        lListParameterParameter.Add(strType)


        For i = 0 To MachineListView_Type_Value.Rows.Count - 1
            Dim strParameter As String = String.Empty
            For j = 0 To lListForm((i + 3).ToString).MachineListView_Type_Value.Rows.Count - 1
                If strParameter = "" Then
                    strParameter = lListForm((i + 3).ToString).MachineListView_Type_Value.Rows(j).Cells(1).Value + "," + lListForm((i + 3).ToString).MachineListView_Type_Value.Rows(j).Cells(2).Value
                Else
                    strParameter = strParameter + ";" + lListForm((i + 3).ToString).MachineListView_Type_Value.Rows(j).Cells(1).Value + "," + lListForm((i + 3).ToString).MachineListView_Type_Value.Rows(j).Cells(2).Value
                End If
            Next
            lListParameterParameter.Add(strParameter)
        Next

    End Sub

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MachineListView_Type_Value.Rows.Add((MachineListView_Type_Value.Rows.Count + 1).ToString, "")

        CreatTapPage()
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Type_Value.Rows.Count <= 0 Then Return
        MachineListView_Type_Value.Rows.Remove(MachineListView_Type_Value.CurrentRow)
        For Each t As DataGridViewRow In MachineListView_Type_Value.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        CreatTapPage()

        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        CreatTapPage()
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub
    Private Sub AddForm_ParameterChanged()
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub

    Private Sub MachineListView_Parameter_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub
    Private Sub CreatTapPage()
        Dim iIndex As Integer = 2
        TabControl_Parameter.Controls.Clear()
        TabControl_Parameter.Controls.Add(TabPage1)
        TabControl_Parameter.Controls.Add(TabPage2)
        ' lListPage.Clear()

        For i = 0 To MachineListView_Type_Value.Rows.Count - 1
            Dim strType As String = MachineListView_Type_Value.Rows(i).Cells(1).Value
            Dim cTapPage As TabPage
            Dim cAddForm As AddForm
            If lListPage.ContainsKey((i + 3)) Then
                cTapPage = lListPage((i + 3))
                cTapPage.Name = strType
                cTapPage.Text = strType
                cAddForm = lListForm((i + 3))
                TabControl_Parameter.Controls.Add(cTapPage)
            Else
                cAddForm = New AddForm
                cTapPage = New TabPage
                cTapPage.Name = strType
                cTapPage.Text = strType
                cTapPage.Font = TabPage1.Font
                cAddForm.FontSize = TabControl_Parameter.Font
                cAddForm.ControlName = (i + 3).ToString
                cAddForm.Init(cLocalElement, cSystemElement)
                Dim strParameter As String = String.Empty
                If lListParameter.Count > (i + 2) Then
                    strParameter = lListParameter((i + 2))
                Else
                    strParameter = ""
                End If
                cAddForm.SetParameter(cLocalElement, cSystemElement, strParameter)
                cTapPage.Controls.Add(cAddForm.UI)
                TabControl_Parameter.Controls.Add(cTapPage)
                AddHandler cAddForm.MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Parameter_CellValueChanged
                AddHandler cAddForm.ParameterChanged, AddressOf AddForm_ParameterChanged
            End If

            If lListPage.ContainsKey((i + 3).ToString) Then
                lListPage((i + 3).ToString) = cTapPage
            Else
                lListPage.Add((i + 3).ToString, cTapPage)
                lListForm.Add((i + 3).ToString, cAddForm)
            End If
        Next
        TabControl_Parameter.SelectedIndex = 1
    End Sub
End Class