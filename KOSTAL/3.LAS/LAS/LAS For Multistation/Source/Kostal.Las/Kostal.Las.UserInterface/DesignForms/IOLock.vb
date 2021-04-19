Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class IOLock
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cTextFont As Font
    Private cDebugMode As Boolean
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Private tempListObject As New List(Of String)
    Private lColumnListValue As New Dictionary(Of String, clsListValueCfg)
    Private cListValueCfg As New clsListValueCfg
    Private lListSourceIO As New List(Of clsIOLockCfg)
    Private lListIO As New List(Of clsIOLockCfg)
    Private cLanguageManager As Language
    Private cTips As clsTips
    Public Property ListSourceIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListSourceIO = value
        End Set
        Get
            Return lListSourceIO
        End Get
    End Property

    Public Property ListIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIO = value
        End Set
        Get
            Return lListIO
        End Get
    End Property

    Public Property IOManager As clsIOManager
        Set(ByVal value As clsIOManager)
            cIOManager = value
        End Set
        Get
            Return cIOManager
        End Get
    End Property
    Public Property CylinderManager As clsCylinderManager
        Set(ByVal value As clsCylinderManager)
            cCylinderManager = value
        End Set
        Get
            Return cCylinderManager
        End Get
    End Property

    Public Property TextFont As Font
        Set(ByVal value As Font)
            cTextFont = value
        End Set
        Get
            Return cTextFont
        End Get
    End Property

    Public Property DebugMode As Boolean
        Set(ByVal value As Boolean)
            cDebugMode = value
        End Set
        Get
            Return cDebugMode
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean

        MachineListView_Lock.RowsDefaultCellStyle.Font = cTextFont
        MachineListView_Lock.AlternatingRowsDefaultCellStyle.Font = cTextFont
        MachineListView_Lock.ColumnHeadersDefaultCellStyle.Font = cTextFont
        cLanguageManager = CType(cSystemElement(Language.Name), Language)
        cTips = CType(cSystemElement(clsTips.Name), clsTips)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.Read("IOParameter", "ID")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_Lock.Columns.Add(PostTest_id)

        Dim PostTest_Type As New DataGridViewTextBoxColumn
        PostTest_Type.HeaderText = cLanguageManager.Read("IOParameter", "Type")
        PostTest_Type.Name = "PostTest_Type"
        MachineListView_Lock.Columns.Add(PostTest_Type)

        Dim PostTest_Name As New DataGridViewTextBoxColumn
        PostTest_Name.HeaderText = cLanguageManager.Read("IOParameter", "Name")
        PostTest_Name.Name = "PostTest_Name"
        MachineListView_Lock.Columns.Add(PostTest_Name)


        Dim PostTest_Value As New DataGridViewTextBoxColumn
        PostTest_Value.HeaderText = cLanguageManager.Read("IOParameter", "Value")
        PostTest_Value.Name = "PostTest_Value"
        MachineListView_Lock.Columns.Add(PostTest_Value)

        tempListObject = New List(Of String)
        cListValueCfg = New clsListValueCfg
        If cDebugMode Then
            For Each element As clsIOPageCfg In cIOManager.ListPage.Values
                tempListObject.Add(element.ActiveText)
            Next
            For Each element As clsCylinderPageCfg In cCylinderManager.ListPage.Values
                tempListObject.Add(element.ActiveText)
            Next
        End If
        cListValueCfg.ListValue = tempListObject
        lColumnListValue.Add("PostTest_Type", cListValueCfg)

        tempListObject = New List(Of String)
        cListValueCfg = New clsListValueCfg
        tempListObject.Add("ON")
        tempListObject.Add("OFF")
        cListValueCfg.ListValue = tempListObject
        lColumnListValue.Add("PostTest_Value", cListValueCfg)
        MachineListView_Lock.lColumnListValue = lColumnListValue
        lListIO.Clear()
        For Each element As clsIOLockCfg In lListSourceIO
            lListIO.Add(element.Clone)
        Next
        For Each element As clsIOLockCfg In lListIO
            Select Case element.TypeName
                Case "EL1008", "EP1008", "EL2008"

                    Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(element.IndexX, element.IndexY)
                    If IsNothing(cIOCfg) Then
                        MachineListView_Lock.Rows.Add(MachineListView_Lock.Rows.Count + 1, "", "", "")
                        MachineListView_Lock.Rows(MachineListView_Lock.Rows.Count - 1).Cells(1).Tag = 0
                        element.TypeName = ""
                        element.IndexX = 0
                        element.IndexY = 0
                        element.Status = ""
                    Else
                        MachineListView_Lock.Rows.Add(MachineListView_Lock.Rows.Count + 1, cIOManager.GetIOPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveText, element.Status)
                        MachineListView_Lock.Rows(MachineListView_Lock.Rows.Count - 1).Cells(1).Tag = element.IndexX
                    End If


                Case "Cylinder"
                    Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                    If IsNothing(cIOCfg) Then
                        MachineListView_Lock.Rows.Add(MachineListView_Lock.Rows.Count + 1, "", "", "")
                        MachineListView_Lock.Rows(MachineListView_Lock.Rows.Count - 1).Cells(1).Tag = 0
                        element.TypeName = ""
                        element.IndexX = 0
                        element.IndexY = 0
                        element.Status = ""
                    Else
                        If element.IndexY Mod 2 = 0 Then
                            MachineListView_Lock.Rows.Add(MachineListView_Lock.Rows.Count + 1, cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextB, element.Status)
                        Else
                            MachineListView_Lock.Rows.Add(MachineListView_Lock.Rows.Count + 1, cCylinderManager.GetCylinderPageCfgFromID(element.IndexX).ActiveText, cIOCfg.ActiveTextA, element.Status)
                        End If
                        MachineListView_Lock.Rows(MachineListView_Lock.Rows.Count - 1).Cells(1).Tag = element.IndexX + cIOManager.ListPage.Count
                    End If

            End Select


        Next
        AddHandler ListView_Add.Click, AddressOf ListView_Add_Click
        AddHandler ListView_Del.Click, AddressOf ListView_Del_Click
        AddHandler ListView_Up.Click, AddressOf ListView_Up_Click
        AddHandler ListView_Down.Click, AddressOf ListView_Down_Click
        AddHandler MachineListView_Lock.CellValueChanged, AddressOf MachineListView_Data_CellValueChanged
        AddHandler MachineListView_Lock.CellClick, AddressOf MachineListView_Data_CellClick
        Return True
    End Function

    Private Sub MachineListView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Select Case MachineListView_Lock.Columns(e.ColumnIndex).Name
            Case "PostTest_Name"
                Dim iCnt As Integer = MachineListView_Lock.CurrentRow.Cells(e.ColumnIndex - 1).Tag
                tempListObject = New List(Of String)
                cListValueCfg = New clsListValueCfg
                If cDebugMode Then
                    If iCnt > 0 And iCnt <= cIOManager.ListPage.Count Then
                        For Each element As clsIOCfg In cIOManager.ListPage(iCnt).ListIO.Values
                            tempListObject.Add(element.ActiveText)
                        Next
                    End If

                    If iCnt > cIOManager.ListPage.Count Then
                        For Each element As clsCylinderCfg In cCylinderManager.ListPage(iCnt - cIOManager.ListPage.Count).ListIO.Values
                            tempListObject.Add(element.ActiveTextA)
                            tempListObject.Add(element.ActiveTextB)
                        Next
                    End If
                    cListValueCfg.ListValue = tempListObject
                End If
                If lColumnListValue.ContainsKey("PostTest_Name") Then
                    lColumnListValue("PostTest_Name") = cListValueCfg
                Else
                    lColumnListValue.Add("PostTest_Name", cListValueCfg)
                End If
                MachineListView_Lock.lColumnListValue = lColumnListValue

        End Select
        MachineListView_Lock.Cell_Enter(sender, e)
    End Sub

    Private Sub MachineListView_Data_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            Select Case MachineListView_Lock.Columns(e.ColumnIndex).Name
                Case "PostTest_Type"
                    Dim iCnt As Integer = MachineListView_Lock.ComboBox.SelectedIndex + 1
                    tempListObject = New List(Of String)
                    cListValueCfg = New clsListValueCfg
                    If cDebugMode Then
                        If iCnt > 0 And iCnt <= cIOManager.ListPage.Count Then
                            For Each element As clsIOCfg In cIOManager.ListPage(iCnt).ListIO.Values
                                tempListObject.Add(element.ActiveText)
                            Next
                            lListIO(e.RowIndex).TypeName = cIOManager.ListPage(iCnt).IOType.ToString
                            lListIO(e.RowIndex).IndexX = iCnt
                        End If

                        If iCnt > cIOManager.ListPage.Count Then
                            For Each element As clsCylinderCfg In cCylinderManager.ListPage(iCnt - cIOManager.ListPage.Count).ListIO.Values
                                tempListObject.Add(element.ActiveTextA)
                                tempListObject.Add(element.ActiveTextB)
                            Next
                            lListIO(e.RowIndex).TypeName = "Cylinder"
                            lListIO(e.RowIndex).IndexX = iCnt - cIOManager.ListPage.Count
                        End If
                        cListValueCfg.ListValue = tempListObject
                    End If
                    If lColumnListValue.ContainsKey("PostTest_Name") Then
                        lColumnListValue("PostTest_Name") = cListValueCfg
                    Else
                        lColumnListValue.Add("PostTest_Name", cListValueCfg)
                    End If
                    MachineListView_Lock.lColumnListValue = lColumnListValue
                    MachineListView_Lock.CurrentRow.Cells(e.ColumnIndex + 1).Value = ""
                    MachineListView_Lock.CurrentRow.Cells(e.ColumnIndex + 2).Value = ""

                Case "PostTest_Name"
                    lListIO(e.RowIndex).IndexY = MachineListView_Lock.ComboBox.SelectedIndex + 1

                Case "PostTest_Value"
                    lListIO(e.RowIndex).Status = MachineListView_Lock.CurrentRow.Cells(e.ColumnIndex).Value
                    Return

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MachineListView_Parameter_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MachineListView_Lock.Resize
        For Each element As DataGridViewTextBoxColumn In MachineListView_Lock.Columns
            Select Case element.Name
                Case "PostTest_id"
                    element.Width = (MachineListView_Lock.Width / 100) * 10
                Case "PostTest_Type"
                    element.Width = (MachineListView_Lock.Width / 100) * 30
                Case "PostTest_Name"
                    element.Width = (MachineListView_Lock.Width / 100) * 50
                Case "PostTest_Value"
                    element.Width = (MachineListView_Lock.Width / 100) * 10
            End Select
        Next
    End Sub

    Private Sub ListView_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        lListIO.Add(New clsIOLockCfg)
        MachineListView_Lock.Rows.Add((MachineListView_Lock.Rows.Count + 1).ToString, "", "")
    End Sub

    Private Sub ListView_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MachineListView_Lock.CurrentRow Is Nothing Then Return
        lListIO.RemoveAt(MachineListView_Lock.CurrentRow.Index)
        MachineListView_Lock.Rows.Remove(MachineListView_Lock.CurrentRow)
        For i = 0 To MachineListView_Lock.Rows.Count - 1
            MachineListView_Lock.Rows(i).Cells(0).Value = (i + 1).ToString
        Next
    End Sub

    Private Sub ListView_Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Lock.CurrentRow Is Nothing Then Return
            If MachineListView_Lock.CurrentRow.Index = 0 Then Return
            Dim cTempCfg As clsIOLockCfg = lListIO(MachineListView_Lock.CurrentRow.Index - 1).Clone
            lListIO(MachineListView_Lock.CurrentRow.Index - 1) = lListIO(MachineListView_Lock.CurrentRow.Index).Clone
            lListIO(MachineListView_Lock.CurrentRow.Index) = cTempCfg.Clone
            Dim iID As Integer = MachineListView_Lock.CurrentRow.Index + 1
            UpRow(iID, MachineListView_Lock)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub ListView_Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MachineListView_Lock.CurrentRow Is Nothing Then Return
            If MachineListView_Lock.CurrentRow.Index = MachineListView_Lock.Rows.Count - 1 Then Return
            Dim cTempCfg As clsIOLockCfg = lListIO(MachineListView_Lock.CurrentRow.Index + 1).Clone
            lListIO(MachineListView_Lock.CurrentRow.Index + 1) = lListIO(MachineListView_Lock.CurrentRow.Index).Clone
            lListIO(MachineListView_Lock.CurrentRow.Index) = cTempCfg.Clone
            Dim iID As Integer = MachineListView_Lock.CurrentRow.Index + 1
            DownRow(iID, MachineListView_Lock)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub DownRow(ByVal id As Integer, ByRef v As DataGridView)
        If id > v.Rows.Count - 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id + 1).ToString
        v.Rows(id).Cells(0).Value = (id).ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub

    Private Sub UpRow(ByVal id As Integer, ByRef v As DataGridView)
        If id <= 1 Or v Is Nothing Then Return
        v.Rows(id - 1).Cells(0).Value = (id - 1).ToString
        v.Rows(id - 2).Cells(0).Value = id.ToString
        Dim CurrRow As DataGridViewRow = v.Rows(id - 1)
        v.Rows.Remove(CurrRow)
        v.Rows.Insert(id - 2, CurrRow)
        v.CurrentCell = CurrRow.Cells(0)
    End Sub
End Class