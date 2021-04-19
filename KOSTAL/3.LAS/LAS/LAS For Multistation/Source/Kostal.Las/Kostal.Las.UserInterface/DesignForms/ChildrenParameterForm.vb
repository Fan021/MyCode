Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base
Public Class ChildrenParameterForm
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As New Dictionary(Of String, Object)
    Private strButtonName As String
    Private strLastDeviceName As String = ""
    Private cIniHandler As clsIniHandler
    Private iMax As Integer = 0
    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As MainForm_Bosh
    Private cUserManager As clsUserManager
    Private _xmlHandler As New XmlHandler
    Private cLanguageManager As Language
    Public cMain As MainForm_Mul
    Public cGlobalParameter As clsGlobalParameter
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property



    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        Me.cSystemElement = Devices
        Me.cLocalElement = cLocalElement
        cLanguageManager = CType(Devices(Language.Name), Language)
        cUserManager = CType(Devices(clsUserManager.Name), clsUserManager)
        cGlobalParameter = CType(Devices(clsGlobalParameter.Name), clsGlobalParameter)
        InitForm()
        InitControlText()
        Panel_Body.BackColor = Color.White
        Return True
    End Function


    Public Function InitForm() As Boolean

        MachineListView_Parameter.Rows.Clear()
        MachineListView_Parameter.Columns.Clear()
        Dim Parameter_id As New DataGridViewTextBoxColumn
        Parameter_id.HeaderText = cLanguageManager.Read("ChildrenParameterForm", "ID")
        Parameter_id.Name = "Parameter_id"
        Parameter_id.ReadOnly = True
        Parameter_id.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_id)

        Dim Parameter_Name As New DataGridViewTextBoxColumn
        Parameter_Name.HeaderText = cLanguageManager.Read("ChildrenParameterForm", "Name")
        Parameter_Name.Name = "Parameter_Name"
        Parameter_Name.ReadOnly = True
        Parameter_Name.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_Name)

        Dim Parameter_Value As New DataGridViewTextBoxColumn
        Parameter_Value.HeaderText = cLanguageManager.Read("ChildrenParameterForm", "Value")
        Parameter_Value.Name = "Parameter_Value"
        Parameter_Value.SortMode = DataGridViewColumnSortMode.NotSortable
        MachineListView_Parameter.Columns.Add(Parameter_Value)

        HmiButton_Save.Button.Text = cLanguageManager.Read("ChildrenParameterForm", "Save")
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        AddHandler HmiButton_Save.Button.Click, AddressOf Button_Save_Click
        Return True
    End Function

    Public Function InitControlText() As Boolean
        Dim cListValueCfg As New clsListValueCfg
        Dim tempListObject As New List(Of Object)
        Dim lRowListValue As New Dictionary(Of String, clsListValueCfg)
        tempListObject = New List(Of Object)
        For Each element As clsParameterCfg In cGlobalParameter.lMachineGlobalParameter.Values
            If element.ListValue.Count > 0 Then
                cListValueCfg = New clsListValueCfg
                cListValueCfg.ListValue = element.ListValue
                cListValueCfg.ListType = element.Type
            Else
                cListValueCfg = New clsListValueCfg
                cListValueCfg.ListValue = New List(Of String)
                cListValueCfg.ListType = element.Type

            End If
            lRowListValue.Add(element.Name, cListValueCfg)
        Next
        Dim iCnt As Integer = 1
        MachineListView_Parameter.lRowListValue = lRowListValue
        For Each element As clsParameterCfg In cGlobalParameter.lMachineGlobalParameter.Values
            MachineListView_Parameter.Rows.Add(iCnt.ToString, element.Name, element.Value)
            iCnt = iCnt + 1
        Next
        HmiButton_Save.Button.Enabled = cGlobalParameter.IsChanged
        Return True
    End Function


    Private Sub MachineListView_Data_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MachineListView_Parameter.CellValueChanged
        Try
            If MachineListView_Parameter.CurrentCell Is Nothing Then Return
            Dim Obj As Object = MachineListView_Parameter.CurrentRow.Cells(e.ColumnIndex).Value
            If IsNothing(Obj) Then
                Obj = ""
            End If
            Select Case MachineListView_Parameter.Columns(e.ColumnIndex).Name
                Case "Parameter_id"
                    Return
                Case "Parameter_Name"
                    Return
                Case "Parameter_Value"
                    cGlobalParameter.SetCurrentGlobalParameter(MachineListView_Parameter.CurrentRow.Cells(1).Value.ToString, Obj.ToString)
            End Select


            HmiButton_Save.Button.Enabled = cGlobalParameter.IsChanged
        Catch ex As Exception

            Throw ex
        End Try
    End Sub

    Private Sub Button_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            cGlobalParameter.SaveCurrentGlobalParameter()
            cGlobalParameter.LoadMachineGlobalParameter()
            HmiButton_Save.Button.Enabled = cGlobalParameter.IsChanged
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try

            Me.Dispose()
            Return True
        Catch ex As Exception
            ' cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
            Return False
        End Try
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub


End Class