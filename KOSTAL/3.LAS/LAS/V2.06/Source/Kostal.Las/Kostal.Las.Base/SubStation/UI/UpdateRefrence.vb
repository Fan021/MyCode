Public Class UpdateRefrenceUI
    Implements IUpdateReferenceUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IUpdateReferenceUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IUpdateReferenceUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IUpdateReferenceUI.Panel
        Get
            Return PanelUI
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IUpdateReferenceUI.DataList
        Get
            Return DG_Reference
        End Get
    End Property

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strSchedule As String, ByVal bResult As Boolean) As Boolean
        If DG_Reference.Rows.Count >= 20 Then
            DG_Reference.Rows.RemoveAt(DG_Reference.Rows.Count - 1)
        End If
        DG_Reference.Rows.Add()
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Schedule").Value = strSchedule

        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
       System.Drawing.Color.Red)), Drawing.Color)

        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Reference.Sort(DG_Reference.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Reference.Columns.Clear()
        DG_Reference.Columns.Add("SN", "SN")
        DG_Reference.Columns.Add("Article", "Article")
        DG_Reference.Columns.Add("Customer", "Customer")
        DG_Reference.Columns.Add("ProductFamily", "ProductFamily")
        DG_Reference.Columns.Add("Schedule", "Schedule")
        DG_Reference.Columns.Add("Result", "Result")
        DG_Reference.Columns.Add("Time", "Time")
        Return True
    End Function

End Class