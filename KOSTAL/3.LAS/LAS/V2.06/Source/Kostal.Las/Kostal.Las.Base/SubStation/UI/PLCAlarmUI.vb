Public Class PLCAlarmUI
    Implements IPLCAlarmUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IPLCAlarmUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IPLCAlarmUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IPLCAlarmUI.Panel
        Get
            Return PanelUI
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IPLCAlarmUI.DataList
        Get
            Return DG_Fail
        End Get
    End Property


    Public Function AddRow(ByVal strErrorCode As String, ByVal strDescription As String) As Boolean
        If DG_Fail.Rows.Count >= 20 Then
            DG_Fail.Rows.RemoveAt(DG_Fail.Rows.Count - 1)
        End If
        DG_Fail.Rows.Add()
        DG_Fail.Rows(DG_Fail.Rows.Count - 1).Cells("ErrorCode").Value = strErrorCode
        DG_Fail.Rows(DG_Fail.Rows.Count - 1).Cells("Description").Value = strDescription
        DG_Fail.Rows(DG_Fail.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Fail.Sort(DG_Fail.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Fail.Columns.Clear()
        DG_Fail.Columns.Add("ErrorCode", "ErrorCode")
        DG_Fail.Columns.Add("Description", "Description")
        DG_Fail.Columns.Add("Time", "Time")
        Return True
    End Function


End Class