Public Class ManualUI
    Implements IManualUI
    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IManualUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IManualUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IManualUI.Panel
        Get
            Return DockPanel
        End Get
    End Property
    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IManualUI.DataList
        Get
            Return DG_Manual
        End Get
    End Property

    Private Sub TrigON_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrigON.Click
        TrigON.Enabled = False
        TrigOFF.Enabled = True
    End Sub

    Private Sub TrigOFF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrigOFF.Click
        TrigON.Enabled = True
        TrigOFF.Enabled = False
    End Sub

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal bResult As Boolean) As Boolean
        If DG_Manual.Rows.Count >= 20 Then
            DG_Manual.Rows.RemoveAt(DG_Manual.Rows.Count - 1)
        End If
        DG_Manual.Rows.Add()
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("Customer").Value = strCustomer
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_Manual.Rows(DG_Manual.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_Manual.Sort(DG_Manual.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_Manual.Columns.Clear()
        DG_Manual.Columns.Add("SN", "SN")
        DG_Manual.Columns.Add("Article", "Article")
        DG_Manual.Columns.Add("Customer", "Customer")
        DG_Manual.Columns.Add("ProductFamily", "ProductFamily")
        DG_Manual.Columns.Add("Result", "Result")
        DG_Manual.Columns.Add("Time", "Time")
        DG_Manual.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill

        Return True
    End Function

    Private Sub ManualUI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class