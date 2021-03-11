Imports System.Windows.Forms
Imports System.Drawing

Public Class HMIButtonWithIndicate
    Inherits Button
    Implements IIO
    Private bOldIndicateValue As Boolean = False
    Dim toolTip1 As New ToolTip
    Dim iLastX As Integer = 0
    Dim iLastY As Integer = 0
    Dim iExit As Integer = 0
    Dim Timer1 As New Timer
    Dim strLastMessage As String = ""
    Private _Object As New Object
    Public ControlDisable As Boolean = False
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        If bOldIndicateValue Then
            BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            BackColor = System.Drawing.SystemColors.Control
        End If
        If ControlDisable And BackColor <> ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter) Then
            BackColor = System.Drawing.SystemColors.Control
        End If
        FlatAppearance.MouseOverBackColor = BackColor
        FlatAppearance.MouseDownBackColor = BackColor
        MyBase.OnMouseEnter(e)
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        SyncLock _Object
            If bOldIndicateValue Then
                BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
            Else
                BackColor = Color.Transparent
            End If
            If ControlDisable And BackColor <> ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter) Then
                BackColor = System.Drawing.SystemColors.Control
            End If
            FlatAppearance.MouseOverBackColor = BackColor
            FlatAppearance.MouseDownBackColor = BackColor
            If iExit Then Return
            Timer1.Enabled = False
            toolTip1.Active = False
            MyBase.OnMouseLeave(e)
        End SyncLock
    End Sub


    Public ReadOnly Property MainButton As System.Windows.Forms.Button Implements IIO.MainButton
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property MainIndicate As System.Windows.Forms.Panel Implements IIO.MainIndicate
        Get
            Return Nothing
        End Get
    End Property

    Public Function RegisterButton(ByVal strText As String, Optional ByVal strName As String = "") As Boolean Implements IIO.RegisterButton
        Return Nothing
    End Function

    Public Function SetIndicateBackColor(ByVal bValue As Boolean) As Boolean Implements IIO.SetIndicateBackColor
        If bOldIndicateValue = bValue Then Return True
        If bValue Then
            BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_MainButton_Button_Enter)
        Else
            BackColor = System.Drawing.SystemColors.Control
        End If
        FlatAppearance.MouseOverBackColor = BackColor
        FlatAppearance.MouseDownBackColor = BackColor
        bOldIndicateValue = bValue
        Return True
    End Function
    Protected Overrides Sub OnMouseMove(ByVal mevent As System.Windows.Forms.MouseEventArgs)
        SyncLock _Object
            If iExit Then Return
            If iLastX <> mevent.X Or iLastY <> mevent.Y Then
                strLastMessage = Me.Text
                Timer1.Enabled = True
                iLastX = mevent.X
                iLastY = mevent.Y
            End If
            MyBase.OnMouseMove(mevent)
        End SyncLock
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        toolTip1.Dispose()
        Timer1.Enabled = False
        Timer1.Dispose()
        iExit = True
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Timer1.Enabled = False
            toolTip1.Show(Me.Text, Me, New Point(iLastX + 20, iLastY + 20), 5000)
            toolTip1.Active = True
        End SyncLock
    End Sub
End Class
