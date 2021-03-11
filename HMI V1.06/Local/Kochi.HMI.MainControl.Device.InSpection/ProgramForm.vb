Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Kochi.HMI.MainControl.LocalDevice

Public Class ProgramForm
    Protected lListParameterParameter As New List(Of String)
    Public Event ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected cLocalElement As Dictionary(Of String, Object)
    Protected cDeviceManager As clsDeviceManager
    Protected cLanguageManager As clsLanguageManager
    Protected cMachineManager As clsMachineManager
    Private iProgramUI As IProgramUI
    Private cInSpection As clsHMIInSpection
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Public Event ValueChanged()
    Public ReadOnly Property UI As Panel
        Get
            Return Panel_UI
        End Get
    End Property

    Public Property ObjectSource As Object
        Set(ByVal value As Object)
            cInSpection = value
        End Set
        Get
            Return cInSpection
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.cSystemElement = cSystemElement
        Me.cLocalElement = cLocalElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
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

    Public Function InitControlText() As Boolean
        MachineListView_Program.Columns.Clear()
        MachineListView_Program.Font = New System.Drawing.Font("Calibri", 12.0!)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Program.Columns.Add(PostTest_id)

        Dim PostTest_Program As New DataGridViewTextBoxColumn
        PostTest_Program.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "Program")
        PostTest_Program.Name = "PostTest_Program"
        PostTest_Program.ReadOnly = True
        MachineListView_Program.Columns.Add(PostTest_Program)

        Dim PostTest_ProgramName As New DataGridViewTextBoxColumn
        PostTest_ProgramName.HeaderText = cLanguageManager.GetUserTextLine("InSpection", "ProgramName")
        PostTest_ProgramName.Name = "PostTest_ProgramName"
        MachineListView_Program.Columns.Add(PostTest_ProgramName)
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cInSpection.Name)
        For i = 0 To 32
            Dim strTemp As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Program" + i.ToString, "Name")
            MachineListView_Program.Rows.Add(MachineListView_Program.Rows.Count + 1, i.ToString, strTemp)
        Next
        AddHandler MachineListView_Program.CellValueChanged, AddressOf MachineListView_Value_CellValueChanged
        AddHandler MachineListView_Program.Resize, AddressOf MachineListView_Parameter_Resize
        Return True
    End Function

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each element As DataGridViewTextBoxColumn In MachineListView_Program.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Program.Width / 100) * 20
                Case "PostTest_Program"
                    element.Width = (MachineListView_Program.Width / 100) * 30
                Case "PostTest_ProgramName"
                    element.Width = (MachineListView_Program.Width / 100) * 50
            End Select
        Next

    End Sub

    Private Sub MachineListView_Value_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cInSpection.Name)
        For i = 0 To MachineListView_Program.Rows.Count - 1
            cIniHandler.WriteIniFile(cSystemManager.Settings.ConfigFolder + "\" + cDeviceCfg.DeviceType + "_" + cDeviceCfg.StationID.ToString + "_" + cDeviceCfg.StationIndex.ToString + ".ini", "Program" + i.ToString, "Name", MachineListView_Program.Rows(i).Cells(2).Value)
        Next
        RaiseEvent ValueChanged()
    End Sub
End Class