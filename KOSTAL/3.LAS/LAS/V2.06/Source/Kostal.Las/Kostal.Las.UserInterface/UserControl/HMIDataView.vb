Imports System.Windows.Forms
Imports System.Drawing

Public Class HMIDataView
    Inherits DataGridView

    Sub Init()
        Dim dataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.AllowUserToAddRows = False
        Me.AllowUserToDeleteRows = False
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        dataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0)
        dataGridViewCellStyle3.BackColor = SystemColors.ControlLightLight
        dataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0)
        Me.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1
        dataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0)
        Me.RowsDefaultCellStyle = dataGridViewCellStyle3
        Me.BackgroundColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(211, 223, 240)
        dataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold)
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Navy
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2
        Me.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.EnableHeadersVisualStyles = False
        Me.GridColor = System.Drawing.SystemColors.GradientInactiveCaption
        'Me.ReadOnly = True
        Me.RowHeadersVisible = False
        Me.RowTemplate.Height = 40
        '  Me.RowTemplate.ReadOnly = True
    End Sub

    Protected Overrides Sub OnCellPainting(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)
        MyBase.OnCellPainting(e)
        Dim iColumn As Integer = -1
        If e.RowIndex < 0 Then
            Return
        End If
        Dim dgr As DataGridViewRow = Me.Rows(e.RowIndex)
        For i = 0 To Me.ColumnCount - 1
            If Me.Columns(i).Name = "Result" Then
                iColumn = i
                Exit For
            End If
        Next
        If iColumn < 0 Then
            Return
        End If
        If iColumn <> e.ColumnIndex Then
            Return
        End If
        If dgr.Cells(iColumn).Value.ToString = "FAIL" Then
            e.CellStyle.BackColor = Color.Red
        ElseIf dgr.Cells(iColumn).Value.ToString = "PASS" Then
            e.CellStyle.BackColor = Color.Lime
        Else

            e.CellStyle.BackColor = Color.White
        End If

    End Sub

    Protected Overrides Sub OnSortCompare(ByVal e As System.Windows.Forms.DataGridViewSortCompareEventArgs)
        If e.Column.Index = 0 Then
            e.SortResult = IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0, 1, IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0, -1, 0))
        End If
        e.Handled = True
        MyBase.OnSortCompare(e)
    End Sub


End Class
