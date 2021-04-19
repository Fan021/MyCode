Public Class MesUI
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
            Return DG_Mes
        End Get
    End Property

    Public Function AddRow(ByVal strType As String, ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal bPositiveAction As String, ByVal bNegativeAction As String, ByVal bResult As Boolean, ByVal strMessage As String) As Boolean
        If DG_Mes.Rows.Count >= 20 Then
            DG_Mes.Rows.RemoveAt(DG_Mes.Rows.Count - 1)
        End If
        DG_Mes.Rows.Add()
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Type").Value = strType
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily

        If bPositiveAction.ToString.ToUpper = "FALSE" Then
            bPositiveAction = ""
        End If

        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("PositiveAction").Value = bPositiveAction.ToString
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("PositiveAction").Style.BackColor = CType((IIf(bPositiveAction = "True", System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.White)), Drawing.Color)

        If bPositiveAction.ToString.ToUpper = "TRUE" Then
            bNegativeAction = ""
        End If

        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("NegativeAction").Value = bNegativeAction.ToString
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("NegativeAction").Style.BackColor = CType((IIf(bNegativeAction = "False", System.Drawing.Color.Red,
                                          System.Drawing.Color.White)), Drawing.Color)

        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Message").Value = strMessage
        DG_Mes.Rows(DG_Mes.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Mes.Sort(DG_Mes.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Mes.Columns.Clear()
        DG_Mes.Columns.Add("Type", "Type")
        DG_Mes.Columns.Add("SN", "SN")
        DG_Mes.Columns.Add("Article", "Article")
        DG_Mes.Columns.Add("Customer", "Customer")
        DG_Mes.Columns.Add("ProductFamily", "ProductFamily")
        DG_Mes.Columns.Add("PositiveAction", "PositiveAction")
        DG_Mes.Columns.Add("NegativeAction", "NegativeAction")
        DG_Mes.Columns.Add("Result", "Result")
        DG_Mes.Columns.Add("Message", "Message")
        DG_Mes.Columns.Add("Time", "Time")
        Return True
    End Function

End Class