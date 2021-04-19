Imports System.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Concurrent
Public Class HMIDataViewWithButton
    Inherits System.Windows.Forms.DataGridView
    Protected cButton As New Button  'for select List value
    Protected cEditor As Control = cButton
    Protected m_nCurrColIndex As Integer = 0
    Protected Olde As System.Windows.Forms.DataGridViewCellEventArgs
    Public ListIndex As New Dictionary(Of String, Integer)
    Protected OpenFileDialog_Path As New OpenFileDialog
    Public strFolder As String
    Public Sub New()
        MyBase.New()
        InitializeComponent()
        AddHandler Me.cButton.MouseLeave, AddressOf ListView_MouseLeave
    End Sub

    ReadOnly Property ComboBox() As Button
        Get
            Return cButton
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

        cButton = New Button
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        AddHandler cButton.Click, AddressOf Button_Click
    End Sub

    Private Sub ListView_AddRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Me.RowsAdded
        Me.Rows(e.RowIndex).Height = Me.ComboBox.Height + 6
    End Sub
    Private Sub ListView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Me.ComboBox.Font = Me.RowHeadersDefaultCellStyle.Font
        Dim rc As Rectangle, i As Int32 = 0
        Me.cButton.Top = (rc.Bottom + rc.Top) / 2 - (Me.cButton.Bottom - Me.cButton.Top) / 2
        If cButton.Visible Then
            cButton.Visible = False
            '  Me.MoveEditorControlPos(Me.cButton, Olde)
        End If
    End Sub


    Private Sub TextBoxValue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim cell As DataGridViewCell = Me.CurrentCell
        If cell.IsInEditMode Then
        End If
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                           "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                            "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
        OpenFileDialog_Path.InitialDirectory = strFolder
        OpenFileDialog_Path.RestoreDirectory = True
        OpenFileDialog_Path.FilterIndex = 1
        Dim cDialogResult As New System.Windows.Forms.DialogResult
        cDialogResult = OpenFileDialog_Path.ShowDialog()
        If cDialogResult = DialogResult.OK Then
            Me(Olde.ColumnIndex, Olde.RowIndex).Value = OpenFileDialog_Path.FileName
        End If
    End Sub

    Private Sub ListView_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        If e.Control.GetType.BaseType.Name = "TextBox" Then
            Dim control As Control = New TextBox()
            control = CType(e.Control, TextBox)
            AddHandler control.KeyPress, AddressOf TextBoxValue_KeyPress
        End If
    End Sub

    Private Sub ListView_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEnter

        If Me.Rows(e.RowIndex).IsNewRow Then Return
        If Me.Columns(e.ColumnIndex).ReadOnly Then Return
        If Not Me.Focused Then Return
        'get column name
        Olde = e
        Me.SetEditorControlPos(Me.cButton, e)

        cButton.Font = Me.RowsDefaultCellStyle.Font
        cButton.Text = "+"
        cButton.Visible = True
        cButton.Focus()
        Me.m_nCurrColIndex = e.ColumnIndex
        Me.cEditor = cButton
    End Sub

    Private Sub ListView_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        If cButton.Visible Then
            cButton.Visible = False
            '  Me.MoveEditorControlPos(Me.cButton, Olde)
        End If
    End Sub
    Private Sub ListView_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles Me.Scroll
        If cButton.Visible Then
            cButton.Visible = False
            '  Me.MoveEditorControlPos(Me.cButton, Olde)
        End If
    End Sub
    Private Sub LimitListView_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellLeave


        If Me.cButton.Visible Then
            Me.Focus()
            Me.cButton.Visible = False
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
            Case "Button"
                rc.Y += Me.ColumnHeadersHeight
                rc.X -= Me.HorizontalScrollingOffset
                rc.Y -= Me.VerticalScrollingOffset
                Me.ComboCenterCell(rc)

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
            Case "Button"
                rc.Y += Me.ColumnHeadersHeight
                rc.X -= Me.HorizontalScrollingOffset
                rc.Y -= Me.VerticalScrollingOffset
                Me.ComboCenterCell(rc)

        End Select
    End Sub
    Public Sub ComboCenterCell(ByVal rc As Rectangle)
        Me.cButton.Left = rc.Left + rc.Width * 3 / 4
        Me.cButton.Width = rc.Width / 4
        Me.cButton.Top = (rc.Bottom + rc.Top) / 2 - (Me.cButton.Bottom - Me.cButton.Top) / 2
    End Sub
    Protected Overrides Sub OnSortCompare(ByVal e As System.Windows.Forms.DataGridViewSortCompareEventArgs)
        If e.Column.Index = 0 Then
            e.SortResult = IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) > 0, 1, IIf(Convert.ToDouble(e.CellValue1) - Convert.ToDouble(e.CellValue2) < 0, -1, 0))
        End If
        e.Handled = True
        MyBase.OnSortCompare(e)
    End Sub
End Class
