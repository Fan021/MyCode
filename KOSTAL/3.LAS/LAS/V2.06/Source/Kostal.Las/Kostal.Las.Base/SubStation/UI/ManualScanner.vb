Imports System.Windows.Forms
Imports System.ComponentModel
Public Class ManualScannerUI
    Implements IManualScannerUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IManualScannerUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IManualScannerUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IManualScannerUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IManualScannerUI.DataList
        Get
            Return DG_SCAN
        End Get
    End Property

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal strScannMsg As String, ByVal bResult As Boolean) As Boolean
        If DG_SCAN.Rows.Count >= 20 Then
            DG_SCAN.Rows.RemoveAt(DG_SCAN.Rows.Count - 1)
        End If
        DG_SCAN.Rows.Add()
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("SN").Value = strSN
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("ScannerMsg").Value = strScannMsg
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_SCAN.Rows(DG_SCAN.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_SCAN.Sort(DG_SCAN.Columns("Time"), ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_SCAN.Columns.Clear()
        DG_SCAN.Columns.Add("SN", "SN        ")
        DG_SCAN.Columns.Add("Article", "Article")
        DG_SCAN.Columns.Add("Customer", "Customer")
        DG_SCAN.Columns.Add("ProductFamily", "ProductFamily  ")
        DG_SCAN.Columns.Add("ScannerMsg", "ScannerMsg    ")
        DG_SCAN.Columns.Add("Result", "Result    ")
        DG_SCAN.Columns.Add("Time", "Time    ")
        Return True
    End Function

End Class