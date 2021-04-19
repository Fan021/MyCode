
Public Class MulitPrinterUI
    Implements IMulitPrinterUI

    Private Sub ShowPic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IMulitPrinterUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IMulitPrinterUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IMulitPrinterUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IMulitPrinterUI.DataList
        Get
            Return DG_DATA
        End Get
    End Property


    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal bResult As Boolean) As Boolean
        If DG_DATA.Rows.Count >= 20 Then
            DG_DATA.Rows.RemoveAt(DG_DATA.Rows.Count - 1)
        End If
        DG_DATA.Rows.Add()
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("SN").Value = strSN
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_DATA.Sort(DG_DATA.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_DATA.Columns.Clear()
        DG_DATA.Columns.Add("SN", "SN")
        DG_DATA.Columns.Add("Article", "Article")
        DG_DATA.Columns.Add("Customer", "Customer")
        DG_DATA.Columns.Add("ProductFamily", "ProductFamily")
        DG_DATA.Columns.Add("Result", "Result")
        DG_DATA.Columns.Add("Time", "Time")
        DG_DATA.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function
End Class