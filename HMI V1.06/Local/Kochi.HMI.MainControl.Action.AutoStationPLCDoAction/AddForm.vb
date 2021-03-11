Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing

Public Class AddForm
    Protected lListParameterParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cLanguageManager As clsLanguageManager
    Protected cFont As Font
    Protected strName As String = String.Empty


    Public Property ControlName As String
        Set(ByVal value As String)
            strName = value
        End Set
        Get
            Return strName
        End Get
    End Property

    Public Property [FontSize] As Font
        Set(ByVal value As Font)
            cFont = value
        End Set
        Get
            Return cFont
        End Get
    End Property

    Public ReadOnly Property UI As Panel
        Get
            Return Panel_UI
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function SetParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal strParameter As String) As Boolean
        If strParameter <> "" Then
            Dim cValueType() As String = strParameter.Split(";")
            For i = 0 To cValueType.Count - 1
                Dim cValue() As String = cValueType(i).Split(",")
                If cValue.Length >= 2 Then
                    MachineListView_Type_Value.Rows.Add(MachineListView_Type_Value.Rows.Count + 1, cValue(0), cValue(1))
                Else
                    MachineListView_Type_Value.Rows.Add(MachineListView_Type_Value.Rows.Count + 1, cValueType(i), "")
                End If

            Next
        End If
        Return True
    End Function
    Public Function InitControlText() As Boolean
        MachineListView_Type_Value.Name = strName
        MachineListView_Type_Value.Columns.Clear()
        Dim PostTest_id As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Type_Value.Columns.Add(PostTest_id)

        MachineListView_Type_Value.Font = cFont
        MachineListView_Type_Value.ColumnHeadersDefaultCellStyle.Font = cFont
        MachineListView_Type_Value.AlternatingRowsDefaultCellStyle.Font = cFont
        MachineListView_Type_Value.RowsDefaultCellStyle.Font = cFont

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "Parameter")
        PostTest_Name.Name = "PostTest_name"
        PostTest_Name.ReadOnly = False
        MachineListView_Type_Value.Columns.Add(PostTest_Name)

        Dim PostTest_Value As New DataGridViewTextBoxColumn
        PostTest_Value.HeaderText = cLanguageManager.GetUserTextLine("AutoStationPlcDoAction", "Value")
        PostTest_Value.Name = "PostTest_Value"
        PostTest_Value.ReadOnly = False
        MachineListView_Type_Value.Columns.Add(PostTest_Value)

        AddHandler MachineListView_Type_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Type_Value.Resize, AddressOf MachineListView_Parameter_Resize
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        Return True
    End Function

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MachineListView_Type_Value.Rows.Add((MachineListView_Type_Value.Rows.Count + 1).ToString, "", "")
        RaiseEvent ParameterChanged(Me)
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Type_Value.Rows.Count <= 0 Then Return
        MachineListView_Type_Value.Rows.Remove(MachineListView_Type_Value.CurrentRow)
        For Each t As DataGridViewRow In MachineListView_Type_Value.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        RaiseEvent ParameterChanged(Me)
    End Sub

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)

        For Each element As DataGridViewTextBoxColumn In MachineListView_Type_Value.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Type_Value.Width / 100) * 20
                Case "PostTest_name"
                    element.Width = (MachineListView_Type_Value.Width / 100) * 40
                Case "PostTest_Value"
                    element.Width = (MachineListView_Type_Value.Width / 100) * 40
            End Select
        Next

    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    End Sub
End Class