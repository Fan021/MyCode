
Public Class ForcamUI
    Implements IForcamUI

    Private Sub ShowPic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IForcamUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IForcamUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IForcamUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IForcamUI.DataList
        Get
            Return DG_DATA
        End Get
    End Property


    Public Function AddRow(ByVal strArticle As String, ByVal strType As String, ByVal strADS_Step As String, ByVal bResult As Boolean) As Boolean
        If DG_DATA.Rows.Count >= 20 Then
            DG_DATA.Rows.RemoveAt(DG_DATA.Rows.Count - 1)
        End If
        DG_DATA.Rows.Add()
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("ADS_Step").Value = strADS_Step
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Type").Value = strType
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_DATA.Rows(DG_DATA.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_DATA.Sort(DG_DATA.Columns("Time"), ComponentModel.ListSortDirection.Descending)
        Return True
    End Function


    Public Function AddColumns() As Boolean
        DG_DATA.Columns.Clear()
        DG_DATA.Columns.Add("Article", "Article")
        DG_DATA.Columns.Add("ADS_Step", "ADS_Step")
        DG_DATA.Columns.Add("Type", "Type")
        DG_DATA.Columns.Add("Result", "Result")
        DG_DATA.Columns.Add("Time", "Time")
        DG_DATA.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function
End Class