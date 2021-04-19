Imports System.Windows.Forms
Imports System.ComponentModel
Public Class ScannerUI
    Implements IScannerUI
    Public Event EventTrigON()
    Public Event EventTrigOFF()
    Public ReadOnly Property Panel As Panel Implements IScannerUI.Panel
        Get
            Return DockPanel
        End Get
    End Property


    Public ReadOnly Property TrigON As Button Implements IScannerUI.TrigON
        Get
            Return _btnON
        End Get
    End Property

    Public ReadOnly Property TrigOFF As Button Implements IScannerUI.TrigOFF
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Result As TextBox Implements IScannerUI.Result
        Get
            Return _lblScanResult
        End Get
    End Property

    Public ReadOnly Property StepID As Label Implements IScannerUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Msg As Label Implements IScannerUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IScannerUI.DataList
        Get
            Return DG_SCAN
        End Get
    End Property

    Private Sub _btnON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _btnON.Click
        RaiseEvent EventTrigON()
        '  _btnON.Enabled = False
    End Sub

    Private Sub _btnOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        RaiseEvent EventTrigOFF()
        ' _btnON.Enabled = True
    End Sub


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