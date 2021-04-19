Public Class SaveProductionUI
    Implements ISaveProductionUI


    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements ISaveProductionUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements ISaveProductionUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements ISaveProductionUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements ISaveProductionUI.DataList
        Get
            Return DG_Counter
        End Get
    End Property

    Public Function AddRow(ByVal strSN As String, ByVal strCarrierNumber As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal bResult As Boolean) As Boolean
        If DG_Counter.Rows.Count >= 20 Then
            DG_Counter.Rows.RemoveAt(DG_Counter.Rows.Count - 1)
        End If
        DG_Counter.Rows.Add()
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("CarrierNumber").Value = strCarrierNumber
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_Counter.Rows(DG_Counter.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Counter.Sort(DG_Counter.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Counter.Columns.Clear()
        DG_Counter.Columns.Add("SN", "SN")
        DG_Counter.Columns.Add("Article", "Article")
        DG_Counter.Columns.Add("CarrierNumber", "CarrierNumber")
        DG_Counter.Columns.Add("Customer", "Customer")
        DG_Counter.Columns.Add("ProductFamily", "ProductFamily")
        DG_Counter.Columns.Add("Result", "Result")
        DG_Counter.Columns.Add("Time", "Time")
        DG_Counter.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function

End Class