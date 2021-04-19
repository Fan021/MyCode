Public Class FailPrinterUI
    Implements IFailPrinterUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IFailPrinterUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IFailPrinterUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IFailPrinterUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IFailPrinterUI.DataList
        Get
            Return DG_FAIL
        End Get
    End Property

    Public ReadOnly Property TextCmd As System.Windows.Forms.TextBox Implements IFailPrinterUI.TextCmd
        Get
            Return TextBox_Send
        End Get
    End Property

    Public ReadOnly Property OK As System.Windows.Forms.Button Implements IFailPrinterUI.OK
        Get
            Return Button_Send
        End Get
    End Property

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strPartFailTestStep As String, ByVal strPartFailUnit As String, ByVal strPartFailValue As String, ByVal strPartFailUpperLimit As String, ByVal strPartFailLowerLimit As String, ByVal strPartFailText As String) As Boolean
        If DG_FAIL.Rows.Count >= 20 Then
            DG_FAIL.Rows.RemoveAt(DG_FAIL.Rows.Count - 1)
        End If
        DG_FAIL.Rows.Add()
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("SN").Value = strSN
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailTestStep").Value = strPartFailTestStep
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailUnit").Value = strPartFailUnit
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailValue").Value = strPartFailValue
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailUpperLimit").Value = strPartFailUpperLimit
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailLowerLimit").Value = strPartFailLowerLimit
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("PartFailText").Value = strPartFailText
        DG_FAIL.Rows(DG_FAIL.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_FAIL.Sort(DG_FAIL.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_FAIL.Columns.Clear()
        DG_FAIL.Columns.Add("SN", "SN")
        DG_FAIL.Columns.Add("Article", "Article")
        DG_FAIL.Columns.Add("Customer", "Customer")
        DG_FAIL.Columns.Add("ProductFamily", "ProductFamily")
        DG_FAIL.Columns.Add("PartFailTestStep", "PartFailTestStep")
        DG_FAIL.Columns.Add("PartFailUnit", "PartFailUnit")
        DG_FAIL.Columns.Add("PartFailValue", "PartFailValue")
        DG_FAIL.Columns.Add("PartFailUpperLimit", "PartFailUpperLimit")
        DG_FAIL.Columns.Add("PartFailLowerLimit", "PartFailLowerLimit")
        DG_FAIL.Columns.Add("PartFailText", "PartFailText")
        DG_FAIL.Columns.Add("Time", "Time")
        Return True
    End Function


End Class