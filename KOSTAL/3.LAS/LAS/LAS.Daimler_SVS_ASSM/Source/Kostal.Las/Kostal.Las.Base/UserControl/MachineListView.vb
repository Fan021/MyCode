Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Concurrent

Public Class MachineListView
    Inherits System.Windows.Forms.DataGridView
    Protected cComboBox As New ComboBox 'for select List value
    Protected cEditor As Control = cComboBox
    Protected m_nCurrColIndex As Integer = 0
    Public lRowListValue As New Dictionary(Of String, clsListValueCfg)
    Public lColumnListValue As New Dictionary(Of String, clsListValueCfg)
    Protected Olde As System.Windows.Forms.DataGridViewCellEventArgs
    Public ListIndex As New Dictionary(Of String, Integer)
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        AddHandler Me.cComboBox.MouseLeave, AddressOf ListView_MouseLeave
    End Sub

    ReadOnly Property ComboBox() As ComboBox
        Get
            Return cComboBox
        End Get
    End Property


    Private Sub InitializeComponent()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LimitListView
        '
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
        Me.ReadOnly = False
        Me.RowHeadersVisible = False
        Me.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        Me.RowTemplate.Height = 40
        Me.RowTemplate.ReadOnly = False

        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private Sub ListView_AddRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Me.RowsAdded
        Me.Rows(e.RowIndex).Height = Me.ComboBox.Height + 6
    End Sub
    Private Sub ListView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Me.ComboBox.Font = Me.RowHeadersDefaultCellStyle.Font
        Dim rc As Rectangle, i As Int32 = 0
        Me.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cComboBox.Top = (rc.Bottom + rc.Top) / 2 - (Me.cComboBox.Bottom - Me.cComboBox.Top) / 2
        If cComboBox.Visible Then
            cComboBox.Visible = False
            '  Me.MoveEditorControlPos(Me.cComboBox, Olde)
        End If
    End Sub


    Private Sub TextBoxValue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim cell As DataGridViewCell = Me.CurrentCell
        If cell.IsInEditMode Then
            If lRowListValue.ContainsKey(Me.CurrentRow.Cells(1).Value.ToString) And cell.ColumnIndex = 2 Then
                Select Case lRowListValue(Me.CurrentRow.Cells(1).Value.ToString).ListType
                    Case GetType(Double), GetType(Single)
                        If Not Char.IsNumber(e.KeyChar) And Not Char.IsPunctuation(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If

                        If Char.IsPunctuation(e.KeyChar) Then
                            If e.KeyChar <> "." And CType(sender, TextBox).TextLength = 0 Then
                                e.Handled = True
                            End If
                            If CType(sender, TextBox).Text.LastIndexOf(".") <> -1 Then
                                e.Handled = True
                            End If

                        End If
                    Case GetType(Integer)
                        If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If
                End Select
            End If

            If lColumnListValue.ContainsKey(Columns(cell.ColumnIndex).Name) Then
                Select Case lColumnListValue(Columns(cell.ColumnIndex).Name).ListType
                    Case GetType(Double), GetType(Single)
                        If Not Char.IsNumber(e.KeyChar) And Not Char.IsPunctuation(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If

                        If Char.IsPunctuation(e.KeyChar) Then
                            If e.KeyChar <> "." And CType(sender, TextBox).TextLength = 0 Then
                                e.Handled = True
                            End If
                            If CType(sender, TextBox).Text.LastIndexOf(".") <> -1 Then
                                e.Handled = True
                            End If

                        End If
                    Case GetType(Integer)
                        If Not Char.IsNumber(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
                            e.Handled = True
                        End If
                End Select
            End If
        End If


    End Sub

    Private Sub ListView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        If e.Control.GetType.BaseType.Name = "TextBox" Then
            Dim control As Control = New TextBox()
            control = CType(e.Control, TextBox)
            AddHandler control.KeyPress, AddressOf TextBoxValue_KeyPress
        End If
    End Sub
    Public Sub Cell_Enter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If Me.Rows(e.RowIndex).IsNewRow Then Return
        If Not Me.Focused Then Return
        'get column name
        Olde = e
        If lColumnListValue.ContainsKey(Columns(e.ColumnIndex).Name) Then
            If lColumnListValue(Columns(e.ColumnIndex).Name).ListValue.Count > 0 Then
                Me.SetEditorControlPos(Me.cComboBox, e)
                cComboBox.Items.Clear()
                For Each element As Object In lColumnListValue(Columns(e.ColumnIndex).Name).ListValue
                    cComboBox.Items.Add(element.ToString)
                Next
                cComboBox.Font = Me.RowsDefaultCellStyle.Font
                cComboBox.Text = Me.CurrentCell.Value
                cComboBox.Visible = True
                cComboBox.Focus()
                Me.m_nCurrColIndex = e.ColumnIndex
                Me.cEditor = cComboBox
            End If
        End If

        If lRowListValue.ContainsKey(Me.CurrentRow.Cells(1).Value.ToString) And (e.ColumnIndex = 2 Or e.ColumnIndex = 3) Then
            If lRowListValue(Me.CurrentRow.Cells(1).Value.ToString).ListValue.Count > 0 Then
                cComboBox.Items.Clear()
                Me.SetEditorControlPos(Me.cComboBox, e)
                For Each element As Object In lRowListValue(Me.CurrentRow.Cells(1).Value.ToString).ListValue
                    cComboBox.Items.Add(element.ToString)
                Next
                cComboBox.Font = Me.RowsDefaultCellStyle.Font
                cComboBox.Text = Me.CurrentCell.Value
                cComboBox.Visible = True
                cComboBox.Focus()
                Me.m_nCurrColIndex = e.ColumnIndex
                Me.cEditor = cComboBox
            End If
        End If
    End Sub
    Private Sub ListView_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If cComboBox.Visible Then
            cComboBox.Visible = False
            '  Me.MoveEditorControlPos(Me.cComboBox, Olde)
        End If
    End Sub
    Private Sub ListView_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles Me.Scroll
        If cComboBox.Visible Then
            cComboBox.Visible = False
            '  Me.MoveEditorControlPos(Me.cComboBox, Olde)
        End If
    End Sub
    Private Sub LimitListView_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellLeave

        If lColumnListValue.ContainsKey(Me.Columns(e.ColumnIndex).Name) Then
            If Me.cComboBox.Visible Then
                If Me.cComboBox.SelectedIndex >= 0 Then
                    Me(e.ColumnIndex, e.RowIndex).Value = Me.cComboBox.Text
                    Me(e.ColumnIndex, e.RowIndex).Tag = Me.cComboBox.SelectedIndex + 1
                    Me.Focus()
                    Me.cComboBox.Visible = False
                End If
            End If
        End If
        If lRowListValue.ContainsKey(Me.CurrentRow.Cells(1).Value.ToString) Then
            If Me.cComboBox.Visible Then
                If Me.cComboBox.SelectedIndex >= 0 Then
                    Me(e.ColumnIndex, e.RowIndex).Value = Me.cComboBox.Text
                    Me(e.ColumnIndex, e.RowIndex).Tag = Me.cComboBox.SelectedIndex + 1
                    Me.Focus()
                    Me.cComboBox.Visible = False
                End If
            End If
        End If
    End Sub


    Public Sub SetEditorControlPos(ByRef Editor As Control, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs, Optional ByVal ClearItems As Boolean = True)

        'Get the Rectangle of the current cell
        Dim rc As Rectangle, i As Int32 = 0

        For i = 0 To e.ColumnIndex - 1
            rc.X += Me.Columns(i).Width
        Next
        rc.Width = Me.Columns(i).Width

        For i = 0 To e.RowIndex - 1
            rc.Y += Me.Rows(i).Height
        Next
        rc.Height = Me.Rows(i).Height

        If Editor Is Nothing Then Return
        Editor.Visible = False
        Editor.Parent = Me

        Select Case Editor.GetType.Name
            Case "ComboBox"
                Me.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                rc.Y += Me.ColumnHeadersHeight
                rc.X -= Me.HorizontalScrollingOffset
                rc.Y -= Me.VerticalScrollingOffset
                Me.ComboCenterCell(rc)
                If ClearItems Then cComboBox.Items.Clear()

        End Select
    End Sub

    Public Sub MoveEditorControlPos(ByRef Editor As Control, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs, Optional ByVal ClearItems As Boolean = True)

        'Get the Rectangle of the current cell
        Dim rc As Rectangle, i As Int32 = 0

        For i = 0 To e.ColumnIndex - 1
            rc.X += Me.Columns(i).Width
        Next
        rc.Width = Me.Columns(i).Width

        For i = 0 To e.RowIndex - 1
            rc.Y += Me.Rows(i).Height
        Next
        rc.Height = Me.Rows(i).Height


        Select Case Editor.GetType.Name
            Case "ComboBox"
                Me.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                rc.Y += Me.ColumnHeadersHeight
                rc.X -= Me.HorizontalScrollingOffset
                rc.Y -= Me.VerticalScrollingOffset
                Me.ComboCenterCell(rc)

        End Select
    End Sub
    Public Sub ComboCenterCell(ByVal rc As Rectangle)
        Me.cComboBox.Left = rc.Left
        Me.cComboBox.Width = rc.Width
        Me.cComboBox.Top = (rc.Bottom + rc.Top) / 2 - (Me.cComboBox.Bottom - Me.cComboBox.Top) / 2
    End Sub
    Protected Overrides Sub OnSortCompare(ByVal e As System.Windows.Forms.DataGridViewSortCompareEventArgs)
        If e.Column.Index = 0 Then
            e.SortResult = IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0, 1, IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0, -1, 0))
        End If
        e.Handled = True
        MyBase.OnSortCompare(e)
    End Sub

End Class

Public Class clsListValueCfg
    Public ListValue As New List(Of String)
    Public ListType As Type = GetType(String)
End Class