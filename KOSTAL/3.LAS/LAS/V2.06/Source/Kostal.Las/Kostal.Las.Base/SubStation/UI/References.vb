Public Class ReferencesUI
    Implements IReferencesUI


    Public ReadOnly Property Msg As System.Windows.Forms.Label Implements IReferencesUI.Msg
        Get
            Return _Msg
        End Get
    End Property

    Public ReadOnly Property StepID As System.Windows.Forms.Label Implements IReferencesUI.StepID
        Get
            Return _StepID
        End Get
    End Property

    Public ReadOnly Property Panel As System.Windows.Forms.Panel Implements IReferencesUI.Panel
        Get
            Return DockPanel
        End Get
    End Property

    Public ReadOnly Property DataList As System.Windows.Forms.DataGridView Implements IReferencesUI.DataList
        Get
            Return DG_Reference
        End Get
    End Property

    Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strReferenceName As String, ByVal strProductFamily As String, ByVal strScheduleName As String, ByVal strShirt As String, ByVal bResult As Boolean) As Boolean
        DG_Reference.Rows.Add()
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("SN").Value = strSN
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Article").Value = strArticle
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("ReferenceName").Value = strReferenceName
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("ScheduleName").Value = strScheduleName
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Shift").Value = strShirt
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Result").Value = bResult.ToString
        DG_Reference.Rows(DG_Reference.Rows.Count - 1).Cells("Result").Style.BackColor = CType(IIf(bResult, System.Drawing.Color.LightGreen, System.Drawing.Color.Red), Drawing.Color)
        Return True
    End Function

    Public Function UpdateRow(ByVal strSN As String, ByVal bResult As Boolean) As Boolean
        For i = 0 To DG_Reference.Rows.Count - 1
            If DG_Reference.Rows(i).Cells("SN").Value.ToString = strSN Then
                DG_Reference.Rows(i).Cells("Result").Value = bResult.ToString
                DG_Reference.Rows(i).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen, System.Drawing.Color.Red)), Drawing.Color)
            End If
        Next
        Return True
    End Function

    Public Function CleanRow() As Boolean
        DG_Reference.Rows.Clear()
        Return True
    End Function

    Public Function AddColumns() As Boolean
        DG_Reference.Columns.Add("SN", "SN")
        DG_Reference.Columns.Add("Article", "Article")
        DG_Reference.Columns.Add("ReferenceName", "ReferenceName")
        DG_Reference.Columns.Add("ProductFamily", "ProductFamily")
        DG_Reference.Columns.Add("ScheduleName", "ScheduleName")
        DG_Reference.Columns.Add("Shift", "Shift")
        DG_Reference.Columns.Add("Result", "Result")
        DG_Reference.Columns("Result").AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Return True
    End Function
End Class