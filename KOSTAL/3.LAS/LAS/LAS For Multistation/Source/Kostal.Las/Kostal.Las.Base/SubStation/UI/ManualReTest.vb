Public Class ManualReTestUI
    Implements IManualReTestUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IManualReTestUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IManualReTestUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IManualReTestUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IManualReTestUI.DataList
        Get
            Return DG_ReTest
        End Get
    End Property

    Public Function AddColumns() As Boolean
        DG_ReTest.Columns.Add("SN", "SN")
        DG_ReTest.Columns.Add("Article", "Article")
        DG_ReTest.Columns.Add("Customer", "Customer")
        DG_ReTest.Columns.Add("ProductFamily", "ProductFamily")
        DG_ReTest.Columns.Add("ScheduleName", "ScheduleName")
        DG_ReTest.Columns.Add("Result", "Result")
        DG_ReTest.Columns.Add("Time", "Time")
        DG_ReTest.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strScheduleName As String, ByVal bResult As Boolean) As Boolean
        If DG_ReTest.Rows.Count >= 20 Then
            DG_ReTest.Rows.RemoveAt(DG_ReTest.Rows.Count - 1)
        End If
        DG_ReTest.Rows.Add()
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("SN").Value = strSN
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("ScheduleName").Value = strScheduleName
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                           System.Drawing.Color.Red)), Drawing.Color)
        DG_ReTest.Rows(DG_ReTest.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_ReTest.Sort(DG_ReTest.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function CleanRow() As Boolean
        DG_ReTest.Rows.Clear()
        Return True
    End Function

End Class