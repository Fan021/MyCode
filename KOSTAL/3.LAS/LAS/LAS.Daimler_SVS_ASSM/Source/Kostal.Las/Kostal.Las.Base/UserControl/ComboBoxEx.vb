Imports System.Windows.Forms
Imports System.Drawing

Public Class ComboBoxEx
    Inherits ComboBox
    Dim toolTip1 As New ToolTip
    Dim iLastX As Integer = 0
    Dim iLastY As Integer = 0
    Dim iLastIndex As Integer = -2
    Private _Object As New Object
    Public iExit As Boolean = False
    Public Overloads Property Text As String
        Set(ByVal value As String)
            SyncLock _Object
                For i = 0 To Me.Items.Count - 1
                    If Me.Items(i).ToString = value Then
                        Me.SelectedIndex = i
                    End If
                Next
            End SyncLock
        End Set
        Get
            SyncLock _Object
                If Me.SelectedIndex >= 0 Then
                    Return Me.Items(Me.SelectedIndex)
                Else
                    Return ""
                End If
            End SyncLock
        End Get
    End Property

    Public Sub New()
        MyBase.New()
        Me.FlatStyle = FlatStyle.Popup
        Me.BackColor = Drawing.Color.White
        Me.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        If m.Msg = &HF Or m.Msg = &H133 Then
            System.Windows.Forms.ControlPaint.DrawBorder(Me.CreateGraphics(), New System.Drawing.Rectangle(0, 0, Me.Width, Me.Height), System.Drawing.SystemColors.ControlDark, ButtonBorderStyle.Solid)
            System.Windows.Forms.ControlPaint.DrawBorder(Me.CreateGraphics(), New System.Drawing.Rectangle(Me.Width - 18, 0, 18, Me.Height), System.Drawing.SystemColors.ControlDark, ButtonBorderStyle.Solid)
        End If
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        If iExit Then Return
        If Me.SelectedIndex >= 0 Then
            toolTip1.Show(Me.Items(Me.SelectedIndex).ToString, Me, New Point(Me.Location.X + Me.Width, Me.Location.Y), 5000)
            toolTip1.Active = True
        End If
        MyBase.OnMouseEnter(e)
    End Sub
        
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        If iExit Then Return
        toolTip1.Active = False
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnDrawItem(ByVal e As System.Windows.Forms.DrawItemEventArgs)
        If iExit Then Return
        If e.Index >= 0 And iLastIndex <> e.Index Then
            If (e.State And DrawItemState.Focus) = DrawItemState.Focus Then
                toolTip1.Show(Me.Items(e.Index).ToString(), Me, e.Bounds.X + e.Bounds.Width, e.Bounds.Y + e.Bounds.Height, 5000)
                toolTip1.Active = True
                iLastIndex = e.Index
            End If
        End If

        If e.Index >= 0 Then
            e.DrawBackground()
            e.Graphics.DrawString(Me.Items(e.Index).ToString(), e.Font, System.Drawing.Brushes.Black, e.Bounds)
            e.DrawFocusRectangle()
        End If
        
        MyBase.OnDrawItem(e)
    End Sub
    Protected Overrides Sub OnDropDown(ByVal e As System.EventArgs)
        If Me.SelectedIndex >= 0 Then
            toolTip1.Show(Me.Items(Me.SelectedIndex).ToString, Me, New Point(Me.Location.X + Me.Width, Me.Location.Y), 5000)
            toolTip1.Active = True
        End If
        MyBase.OnDropDown(e)
    End Sub

    Protected Overrides Sub OnDropDownClosed(ByVal e As System.EventArgs)
        If iExit Then Return
        toolTip1.Active = False
        iLastIndex = -2
        MyBase.OnDropDownClosed(e)
    End Sub
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        toolTip1.Dispose()
        iExit = True
        MyBase.Dispose(disposing)
    End Sub
End Class
