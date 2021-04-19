Imports System.ComponentModel
Public Class NewPartUI
    Implements INewPartUI

    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements INewPartUI.Msg
        Get
            Return _Msg
        End Get
    End Property


    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements INewPartUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements INewPartUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements INewPartUI.DataList
        Get
            Return Nothing
        End Get
    End Property


    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strTraceID As String, ByVal bResult As Boolean) As Boolean
        If DG_SN.Rows.Count >= 20 Then
            DG_SN.Rows.RemoveAt(DG_SN.Rows.Count - 1)
        End If
        DG_SN.Rows.Add()
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("SN").Value = strSN
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("TraceID").Value = strTraceID
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
                                          System.Drawing.Color.Red)), Drawing.Color)
        DG_SN.Rows(DG_SN.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
        DG_SN.Sort(DG_SN.Columns("Time"), ListSortDirection.Descending)
        Return True
    End Function

    Public Function AddColumns() As Boolean
        DG_SN.Columns.Add("SN", "SN")
        DG_SN.Columns.Add("Article", "Article")
        DG_SN.Columns.Add("TraceID", "TraceID")
        DG_SN.Columns.Add("Result", "Result")
        DG_SN.Columns.Add("Time", "Time")
        DG_SN.Columns("SN").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DG_SN.Columns("Result").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DG_SN.Columns("Time").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function

End Class