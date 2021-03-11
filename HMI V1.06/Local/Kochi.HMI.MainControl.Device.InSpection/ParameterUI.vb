Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing

Public Class ParameterUI
    Implements IParameterUI
    Protected lListParameterParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
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
        If lListParameter.Count >= 1 Then
            Dim cValue() As String = lListParameter(0).Split(";")
            If lListParameter(0) <> "" Then
                For i = 0 To cValue.Count - 1
                    MachineListView_Vision.Rows.Add((MachineListView_Vision.Rows.Count + 1), cValue(i))
                Next
            End If
        End If
        If lListParameter.Count >= 2 Then
            Dim cValue() As String = lListParameter(1).Split(";")
            If lListParameter(1) <> "" Then
                For i = 0 To cValue.Count - 1
                    MachineListView_Sensor.Rows.Add((MachineListView_Sensor.Rows.Count + 1), cValue(i))
                Next
            End If
        End If
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IParameterUI.CheckParameter
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)

        For i = 0 To MachineListView_Vision.Rows.Count - 1
            If MachineListView_Vision.Rows(i).Cells(1).Value = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "5", MachineListView_Vision.Rows(i).Cells(0).Value), enumExceptionType.Alarm)
            End If
        Next

        For i = 0 To MachineListView_Sensor.Rows.Count - 1
            If MachineListView_Sensor.Rows(i).Cells(1).Value = "" Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine("InSpection", "6", MachineListView_Sensor.Rows(i).Cells(0).Value), enumExceptionType.Alarm)
            End If
        Next
        Return True
    End Function


    Public Function InitForm() As Boolean
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean

        MachineListView_Vision.Columns.Clear()
        MachineListView_Vision.Font = New System.Drawing.Font("Calibri", 12.0!)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Vision.Columns.Add(PostTest_id)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "Name")
        PostTest_Name.Name = "PostTest_name"
        PostTest_Name.ReadOnly = False
        MachineListView_Vision.Columns.Add(PostTest_Name)

        MachineListView_Sensor.Columns.Clear()
        MachineListView_Sensor.Font = New System.Drawing.Font("Calibri", 12.0!)
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Sensor.Columns.Add(PostTest_id)

        PostTest_Name = New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "Name")
        PostTest_Name.Name = "PostTest_name"
        PostTest_Name.ReadOnly = False
        MachineListView_Sensor.Columns.Add(PostTest_Name)

        TabControl_Parameter.Font = New System.Drawing.Font("Calibri", 12.0!)

        AddHandler MachineListView_Vision.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Sensor.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Vision.Resize, AddressOf MachineListView_Parameter_Resize
        AddHandler MachineListView_Sensor.Resize, AddressOf MachineListView_Parameter_Resize
        AddHandler PostTest_Add_Sensor.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Add_Vision.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del_Vision.Click, AddressOf PostTest_Del_Click
        AddHandler PostTest_Del_Sensor.Click, AddressOf PostTest_Del_Click
        Return True
    End Function

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "PostTest_Add_Vision"
                MachineListView_Vision.Rows.Add((MachineListView_Vision.Rows.Count + 1).ToString, "")
            Case "PostTest_Add_Sensor"
                MachineListView_Sensor.Rows.Add((MachineListView_Sensor.Rows.Count + 1).ToString, "")
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Select Case sender.name
            Case "PostTest_Del_Vision"
                If MachineListView_Vision.Rows.Count <= 0 Then Return
                MachineListView_Vision.Rows.Remove(MachineListView_Vision.CurrentRow)
                For Each t As DataGridViewRow In MachineListView_Vision.Rows
                    t.Cells(0).Value = (t.Index + 1).ToString
                Next
            Case "PostTest_Del_Sensor"
                If MachineListView_Sensor.Rows.Count <= 0 Then Return
                MachineListView_Sensor.Rows.Remove(MachineListView_Sensor.CurrentRow)
                For Each t As DataGridViewRow In MachineListView_Sensor.Rows
                    t.Cells(0).Value = (t.Index + 1).ToString
                Next
        End Select
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As DataGridViewTextBoxColumn In MachineListView_Vision.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Vision.Width / 100) * 30
                Case "PostTest_name"
                    element.Width = (MachineListView_Vision.Width / 100) * 70
            End Select
        Next

        For Each element As DataGridViewTextBoxColumn In MachineListView_Sensor.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Sensor.Width / 100) * 30
                Case "PostTest_name"
                    element.Width = (MachineListView_Sensor.Width / 100) * 70
            End Select
        Next
    End Sub

    Private Sub GetParamater()
        lListParameterParameter.Clear()
        Dim strValue As String = String.Empty
        For i = 0 To MachineListView_Vision.Rows.Count - 1
            If strValue = "" Then
                strValue = MachineListView_Vision.Rows(i).Cells(1).Value
            Else
                strValue = strValue + ";" + MachineListView_Vision.Rows(i).Cells(1).Value
            End If
        Next
        lListParameterParameter.Add(strValue)
       
        strValue = String.Empty
        For i = 0 To MachineListView_Sensor.Rows.Count - 1
            If strValue = "" Then
                strValue = MachineListView_Sensor.Rows(i).Cells(1).Value
            Else
                strValue = strValue + ";" + MachineListView_Sensor.Rows(i).Cells(1).Value
            End If
        Next
        lListParameterParameter.Add(strValue)
    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub
End Class