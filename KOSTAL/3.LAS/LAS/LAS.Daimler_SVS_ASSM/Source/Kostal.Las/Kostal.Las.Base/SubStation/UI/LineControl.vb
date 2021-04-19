Public Class LineControlUI
    Implements ILineControlUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements ILineControlUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements ILineControlUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements ILineControlUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements ILineControlUI.DataList
        Get
            Return DG_LineControl
        End Get
    End Property

    Public Function AddRow(ByVal strType As String, ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strPrviousStation As String, ByVal strCurrentStation As String, ByVal bPositiveAction As String, ByVal bNegativeAction As String, ByVal bResult As Boolean, ByVal strMessage As String) As Boolean
        If DG_LineControl.Rows.Count >= 20 Then
            DG_LineControl.Rows.RemoveAt(DG_LineControl.Rows.Count - 1)
        End If
        DG_LineControl.Rows.Add()
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Type").Value = strType
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("SN").Value = strSN
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("PrviousStation").Value = strPrviousStation
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("CurrentStation").Value = strCurrentStation


        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("PositiveAction").Value = bPositiveAction.ToString
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("PositiveAction").Style.BackColor = CType((IIf(bPositiveAction = "True", System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.White)), Drawing.Color)

        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("NegativeAction").Value = bNegativeAction.ToString
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("NegativeAction").Style.BackColor = CType((IIf(bNegativeAction = "False", System.Drawing.Color.Red,
                                          System.Drawing.Color.White)), Drawing.Color)

        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Message").Value = strMessage
        DG_LineControl.Rows(DG_LineControl.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_LineControl.Sort(DG_LineControl.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_LineControl.Columns.Clear()
        DG_LineControl.Columns.Add("Type", "Type")
        DG_LineControl.Columns.Add("SN", "SN")
        DG_LineControl.Columns.Add("Article", "Article")
        DG_LineControl.Columns.Add("Customer", "Customer")
        DG_LineControl.Columns.Add("ProductFamily", "ProductFamily")
        DG_LineControl.Columns.Add("PrviousStation", "PrviousStation")
        DG_LineControl.Columns.Add("CurrentStation", "CurrentStation")
        DG_LineControl.Columns.Add("PositiveAction", "PositiveAction")
        DG_LineControl.Columns.Add("NegativeAction", "NegativeAction")
        DG_LineControl.Columns.Add("Result", "Result")
        DG_LineControl.Columns.Add("Message", "Message")
        DG_LineControl.Columns.Add("Time", "Time")
        Return True
    End Function

End Class