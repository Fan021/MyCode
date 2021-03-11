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
            Dim cValue() As String = lListParameter(0).Split(";")
            For i = 0 To cValue.Count - 1
                If i <= MachineListView_Device_Value.Rows.Count - 1 Then
                    Dim cTempValue() As String = cValue(i).Split(",")
                    If cTempValue.Count <> 2 Then Continue For
                    If cTempValue(0).ToString = MachineListView_Device_Value.Rows(i).Cells(0).Value Then
                        MachineListView_Device_Value.Rows(i).Cells(2).Value = cTempValue(1)
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


        MachineListView_Device_Value.Columns.Clear()
        MachineListView_Device_Value.Font = New System.Drawing.Font("Calibri", 12.0!)
        Dim PostTest_id As DataGridViewTextBoxColumn = New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Device_Value.Columns.Add(PostTest_id)

        Dim PostTest_name As New DataGridViewTextBoxColumn
        PostTest_name.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Name")
        PostTest_name.Name = "PostTest_name"
        PostTest_name.ReadOnly = True
        MachineListView_Device_Value.Columns.Add(PostTest_name)

        Dim PostTest_value As New DataGridViewTextBoxColumn
        PostTest_value.HeaderText = cLanguageManager.GetUserTextLine("GapFiller", "Device")
        PostTest_value.Name = "PostTest_value"
        MachineListView_Device_Value.Columns.Add(PostTest_value)

        TabPage1.Text = cLanguageManager.GetUserTextLine("GapFiller", "TabPage")
        TabControl_Parameter.Font = New System.Drawing.Font("Calibri", 12.0!)
        Dim lRowListValue As New Dictionary(Of String, clsListValueCfg)
        Dim cListValueCfg As New clsListValueCfg
        Dim tempListObject As New List(Of Object)
        Dim lListDeviceCfg As List(Of clsDeviceCfg)
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            tempListObject = New List(Of Object)
            cListValueCfg = New clsListValueCfg
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            MachineListView_Device_Value.Rows.Add(element.ID, element.StationName, "")
            cListValueCfg = New clsListValueCfg
            tempListObject.Add("")

            lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID(element.ID, GetType(clsHMIMES))
            If Not IsNothing(lListDeviceCfg) Then
                For Each elementDeviceCfg As clsDeviceCfg In lListDeviceCfg
                    tempListObject.Add(elementDeviceCfg.DeviceType + "-" + elementDeviceCfg.StationIndex.ToString)
                Next
            End If
            cListValueCfg.ListValue = tempListObject
            lRowListValue.Add(element.StationName, cListValueCfg)
        Next
        MachineListView_Device_Value.lRowListValue = lRowListValue

        AddHandler MachineListView_Device_Value.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Device_Value.Resize, AddressOf MachineListView_Parameter_Resize
        Return True
    End Function

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As DataGridViewTextBoxColumn In MachineListView_Device_Value.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Device_Value.Width / 100) * 20
                Case "PostTest_name"
                    element.Width = (MachineListView_Device_Value.Width / 100) * 30
                Case "PostTest_value"
                    element.Width = (MachineListView_Device_Value.Width / 100) * 50
            End Select
        Next
    End Sub

    Private Sub GetParamater()
        lListParameterParameter.Clear()
        Dim strDevice As String = String.Empty
        For i = 0 To MachineListView_Device_Value.Rows.Count - 1
            If strDevice = "" Then
                strDevice = MachineListView_Device_Value.Rows(i).Cells(0).Value.ToString + "," + MachineListView_Device_Value.Rows(i).Cells(2).Value
            Else
                strDevice = strDevice + ";" + MachineListView_Device_Value.Rows(i).Cells(0).Value.ToString + "," + MachineListView_Device_Value.Rows(i).Cells(2).Value
            End If
        Next
        lListParameterParameter.Add(strDevice)
    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        GetParamater()
        RaiseEvent ParameterChanged(Me, New ParameterEvent(lListParameterParameter))
    End Sub
End Class