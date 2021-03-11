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
        Return True
    End Function

    Public Function CheckParameter(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal lListParameter As List(Of String)) As Boolean Implements IParameterUI.CheckParameter
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
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ManualStationHandScrewRework", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_id)

        Dim PostTest_ErrorType As New DataGridViewTextBoxColumn
        PostTest_ErrorType.HeaderText = cLanguageManager.GetUserTextLine("ManualStationHandScrewRework", "ErrorType")
        PostTest_ErrorType.Name = "PostTest_ErrorType"
        PostTest_ErrorType.ReadOnly = True
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_ErrorType)

        Dim PostTest_ErrorCode As New DataGridViewTextBoxColumn
        PostTest_ErrorCode.HeaderText = cLanguageManager.GetUserTextLine("ManualStationHandScrewRework", "ErrorCode")
        PostTest_ErrorCode.Name = "PostTest_ErrorCode"
        MachineListView_ErrorCode_Value.Columns.Add(PostTest_ErrorCode)

        TabPage1.Text = cLanguageManager.GetUserTextLine("ManualStationHandScrewRework", "TabPage1")

        For Each eType As enumHandScrewReworkErrorType In [Enum].GetValues(GetType(enumHandScrewReworkErrorType))
            MachineListView_ErrorCode_Value.Rows.Add(MachineListView_ErrorCode_Value.Rows.Count + 1, eType.ToString, "")
        Next

        AddHandler MachineListView_ErrorCode_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_ErrorCode_Value.Resize, AddressOf MachineListView_Parameter_Resize
        Return True
    End Function

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
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
    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub
End Class