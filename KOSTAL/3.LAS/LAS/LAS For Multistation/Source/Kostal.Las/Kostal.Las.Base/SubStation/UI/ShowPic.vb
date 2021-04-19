
Public Class ShowPicUI
    Implements IShowPicUI

    Private Sub ShowPic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IShowPicUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IShowPicUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IShowPicUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IShowPicUI.DataList
        Get
            Return DG_PIC
        End Get
    End Property


    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strPictures As String, ByVal bResult As Boolean) As Boolean
        If DG_PIC.Rows.Count >= 20 Then
            DG_PIC.Rows.RemoveAt(DG_PIC.Rows.Count - 1)
        End If
        DG_PIC.Rows.Add()
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("SN").Value = strSN
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Pictures").Value = strPictures
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_PIC.Rows(DG_PIC.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_PIC.Sort(DG_PIC.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_PIC.Columns.Clear()
        DG_PIC.Columns.Add("SN", "SN")
        DG_PIC.Columns.Add("Article", "Article")
        DG_PIC.Columns.Add("Customer", "Customer")
        DG_PIC.Columns.Add("ProductFamily", "ProductFamily")
        DG_PIC.Columns.Add("Pictures", "Pictures")
        DG_PIC.Columns.Add("Result", "Result")
        DG_PIC.Columns.Add("Time", "Time")
        DG_PIC.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function
End Class