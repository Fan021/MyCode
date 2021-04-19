Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Design
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports System.Reflection
Imports System.Runtime.InteropServices

Public Class RowMergeView
    Inherits DataGridView

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)
    End Sub
    Protected Overrides Sub OnCellPainting(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)
        Try

            If (e.RowIndex > -1 And e.ColumnIndex > -1) Then

                DrawCell(e)
            Else
                If (e.RowIndex = -1) Then

                    If (SpanRows.ContainsKey(e.ColumnIndex)) Then
                        Dim g As Graphics = e.Graphics
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background Or DataGridViewPaintParts.Border)

                        Dim left As Integer = e.CellBounds.Left
                        Dim top As Integer = e.CellBounds.Top + 2
                        Dim right As Integer = e.CellBounds.Right
                        Dim bottom As Integer = e.CellBounds.Bottom

                        Select Case SpanRows(e.ColumnIndex).Position
                            Case 1
                                left += 2
                            Case 2
                            Case 3
                                right -= 2
                        End Select

                        g.FillRectangle(New SolidBrush(Me._mergecolumnheaderbackcolor), left, top, right - left, CInt((bottom - top) / 2))
                        g.DrawLine(New Pen(Me.GridColor), left, CInt((top + bottom) / 2), right, CInt((top + bottom) / 2))

                        Dim sf As StringFormat = New StringFormat()
                        sf.Alignment = StringAlignment.Center
                        sf.LineAlignment = StringAlignment.Center

                        g.DrawString(e.Value.ToString + "", e.CellStyle.Font, Brushes.Black,
                        New Rectangle(left, CInt((top + bottom) / 2), right - left, CInt((bottom - top) / 2)), sf)
                        left = Me.GetColumnDisplayRectangle(SpanRows(e.ColumnIndex).Left, True).Left - 2

                        If left < 0 Then left = Me.GetCellDisplayRectangle(-1, -1, True).Width
                        right = Me.GetColumnDisplayRectangle(SpanRows(e.ColumnIndex).Right, True).Right - 2
                        If right < 0 Then right = Me.Width

                        g.DrawString(SpanRows(e.ColumnIndex).Text, e.CellStyle.Font, Brushes.Black,
                            New Rectangle(left, top, right - left, CInt((bottom - top) / 2)), sf)
                        e.Handled = True
                    End If
                End If
            End If
            MyBase.OnCellPainting(e)

        Catch

        End Try
        '  MyBase.OnCellPainting(e)
    End Sub

    Protected Overrides Sub OnCellClick(ByVal e As DataGridViewCellEventArgs)

        MyBase.OnCellClick(e)
    End Sub


    Private Sub DrawCell(ByVal e As DataGridViewCellPaintingEventArgs)

        If (e.CellStyle.Alignment = DataGridViewContentAlignment.NotSet) Then
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If
        Dim gridBrush As Brush = New SolidBrush(Me.GridColor)
        Dim backBrush As SolidBrush = New SolidBrush(e.CellStyle.BackColor)
        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim cellwidth As Integer
        Dim UpRows As Integer = 0
        Dim DownRows As Integer = 0
        Dim count As Integer = 0
        If (Me.MergeColumnNames.Contains(Me.Columns(e.ColumnIndex).Name) And e.RowIndex <> -1) Then

            cellwidth = e.CellBounds.Width
            Dim gridLinePen As Pen = New Pen(gridBrush)
            Dim curValue As String = CStr(IIf(IsNothing(e.Value), "", e.Value.ToString().Trim()))

            Dim curSelected As String = CStr(IIf(IsNothing(Me.CurrentRow.Cells(e.ColumnIndex).Value), "", Me.CurrentRow.Cells(e.ColumnIndex).Value.ToString().Trim()))
            If (Not String.IsNullOrEmpty(curValue)) Then

                For i = e.RowIndex To Me.Rows.Count - 1

                    If (Me.Rows(i).Cells(e.ColumnIndex).Value.ToString().Equals(curValue)) Then
                        DownRows = DownRows + 1
                        If (e.RowIndex <> i) Then
                            cellwidth = CInt(IIf(cellwidth < Me.Rows(i).Cells(e.ColumnIndex).Size.Width, cellwidth, Me.Rows(i).Cells(e.ColumnIndex).Size.Width))
                        Else

                            Exit For
                        End If
                    End If
                Next


                For i = e.RowIndex To 0 Step -1

                    If (Me.Rows(i).Cells(e.ColumnIndex).Value.ToString().Equals(curValue)) Then
                        UpRows = UpRows + 1
                        If (e.RowIndex <> i) Then
                            cellwidth = CInt(IIf(cellwidth < Me.Rows(i).Cells(e.ColumnIndex).Size.Width, cellwidth, Me.Rows(i).Cells(e.ColumnIndex).Size.Width))
                        End If
                    Else
                        Exit For
                    End If
                Next

                count = DownRows + UpRows - 1
                If (count < 2) Then
                    Return
                End If

            End If
            If (Me.Rows(e.RowIndex).Selected) Then

                backBrush.Color = e.CellStyle.SelectionBackColor
                fontBrush.Color = e.CellStyle.SelectionForeColor
            End If

            e.Graphics.FillRectangle(backBrush, e.CellBounds)

            PaintingFont(e, cellwidth, UpRows, DownRows, count)
            If (DownRows = 1) Then

                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1)
                count = 0
            End If
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom)

            e.Handled = True
        End If
    End Sub

    Private Sub PaintingFont(ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs, ByVal cellwidth As Integer, ByVal UpRows As Integer, ByVal DownRows As Integer, ByVal count As Integer)

        Dim fontBrush As SolidBrush = New SolidBrush(e.CellStyle.ForeColor)
        Dim fontheight As Integer = CInt(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height)
        Dim fontwidth As Integer = CInt(e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width)
        Dim cellheight As Integer = e.CellBounds.Height

        If (e.CellStyle.Alignment = DataGridViewContentAlignment.BottomCenter) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X + (cellwidth - fontwidth) / 2), CSng(e.CellBounds.Y + cellheight * DownRows - fontheight))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.BottomLeft) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight)

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.BottomRight) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight)

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X + (cellwidth - fontwidth) / 2), CSng(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X), CSng(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X + cellwidth - fontwidth), CSng(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.TopCenter) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X + (cellwidth - fontwidth) / 2), CSng(e.CellBounds.Y - cellheight * (UpRows - 1)))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.TopLeft) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1))

        ElseIf (e.CellStyle.Alignment = DataGridViewContentAlignment.TopRight) Then

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1))

        Else

            e.Graphics.DrawString(CStr(e.Value), e.CellStyle.Font, fontBrush, CSng(e.CellBounds.X + (cellwidth - fontwidth) / 2), CSng(e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2))

        End If
    End Sub

    Public Property MergeColumnNames As List(Of String)
        Get
            Return _mergecolumnname
        End Get
        Set(ByVal value As List(Of String))
            _mergecolumnname = value
        End Set
    End Property

    Private _mergecolumnname As New List(Of String)
    Private Structure SpanInfo

        Sub New(ByVal Text As String, ByVal Position As Integer, ByVal Left As Integer, ByVal Right As Integer)

            Me.Text = Text
            Me.Position = Position
            Me.Left = Left
            Me.Right = Right
        End Sub

        Public Text As String
        Public Position As Integer
        Public Left As Integer
        Public Right As Integer
    End Structure

    Private SpanRows As New Dictionary(Of Integer, SpanInfo)

    Public Sub AddSpanHeader(ByVal ColIndex As Integer, ByVal ColCount As Integer, ByVal Text As String)

        If (ColCount < 2) Then

            Throw New Exception("Error")

        End If
        Dim Right As Integer = ColIndex + ColCount - 1
        SpanRows(ColIndex) = New SpanInfo(Text, 1, ColIndex, Right)
        SpanRows(Right) = New SpanInfo(Text, 3, ColIndex, Right)
        For i = ColIndex + 1 To Right - 1
            SpanRows(i) = New SpanInfo(Text, 2, ColIndex, Right)
        Next

    End Sub

    Public Sub ClearSpanInfo()
        SpanRows.Clear()
    End Sub

    Protected Overrides Sub OnScroll(ByVal e As System.Windows.Forms.ScrollEventArgs)
        MyBase.OnScroll(e)
        Timer1.Enabled = False
        Timer1.Enabled = True
    End Sub

    Private Sub DataGridViewEx_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs)

        If (e.ScrollOrientation = ScrollOrientation.HorizontalScroll) Then

            Timer1.Enabled = False
            Timer1.Enabled = True
        End If
    End Sub

    Public Sub ReDrawHead()
        For Each si In SpanRows.Keys
            Me.Invalidate(Me.GetCellDisplayRectangle(si, -1, False))
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        ReDrawHead()
    End Sub

    Public Property MergeColumnHeaderBackColor As Color
        Set(ByVal value As Color)
            Me._mergecolumnheaderbackcolor = value
        End Set
        Get
            Return Me._mergecolumnheaderbackcolor
        End Get

    End Property
    Private _mergecolumnheaderbackcolor As Color = System.Drawing.SystemColors.Control

End Class
