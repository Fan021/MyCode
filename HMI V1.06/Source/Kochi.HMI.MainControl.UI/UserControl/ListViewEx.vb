Imports System.Windows.Forms
Imports System.Drawing
Imports System.Windows.Forms.ListViewItem

Public Class ListViewEx
    Inherits ListView
    Dim toolTip1 As New ToolTip
    Dim lastlistviewsub As New ListViewSubItem
    Dim iLastX As Integer = 0
    Dim iLastY As Integer = 0
    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()
    End Sub

    Protected Overrides Sub OnDrawColumnHeader(ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs)
        e.DrawBackground()
        e.DrawText()
    End Sub

    Protected Overrides Sub OnDrawSubItem(ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs)
        If e.Header.Text = "Result" Then
            If e.SubItem.Text = "PASS" Then
                e.SubItem.BackColor = Color.LightGreen
            ElseIf e.SubItem.Text = "FAIL" Then
                e.SubItem.BackColor = Color.Red
            Else
                e.SubItem.BackColor = Color.Yellow
            End If
        End If
        e.DrawBackground()
        e.DrawText()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim item As ListViewItem = Me.GetItemAt(e.X, e.Y)
        If Not IsNothing(item) Then
            If iLastX <> e.X Or iLastY <> e.Y Then
                toolTip1.Show(item.GetSubItemAt(e.X, e.Y).Text, Me, New Point(e.X + 15, e.Y + 15), 5000)
                toolTip1.Active = True
                iLastX = e.X
                iLastY = e.Y
            End If
        Else
            toolTip1.Active = False
        End If
        MyBase.OnMouseMove(e)
    End Sub
    Protected Overrides Sub OnPaintBackground(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        'MyBase.OnPaintBackground(pevent)
    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ListViewEx
        '
        Me.ResumeLayout(False)

    End Sub
End Class
