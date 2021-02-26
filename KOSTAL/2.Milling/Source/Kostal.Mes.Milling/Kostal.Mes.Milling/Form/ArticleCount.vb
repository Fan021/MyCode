Imports System.Collections.Generic
Imports System.Windows.Forms
Public Class ArticleCount
    Private _Count As Integer = 0
    Private _isRun As Boolean
    Public Const sName As String = "ArticleCount"
    Private _isDisplay As Boolean
    Private _DisplayEnd As Boolean
    Private mLanguage As Language
 
    Public Property isDisplay As Boolean
        Set(ByVal value As Boolean)
            _isDisplay = value
        End Set
        Get
            Return _isDisplay
        End Get
    End Property

    Public Property DisplayEnd As Boolean
        Set(ByVal value As Boolean)
            _DisplayEnd = value
        End Set
        Get
            Return _DisplayEnd
        End Get
    End Property


    Public Property Count As Integer
        Set(ByVal value As Integer)
            _Count = value
        End Set
        Get
            Return _Count
        End Get
    End Property

    Public Property isRun As Boolean
        Set(ByVal value As Boolean)
            _isRun = value
        End Set
        Get
            Return _isRun
        End Get
    End Property

    Private Sub Button_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_OK.Click
        CheckEntry()
    End Sub
    Private Sub TextBox_Count_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox_Count.KeyDown
        If e.KeyCode = Keys.Enter Then
            CheckEntry()
        ElseIf e.KeyCode = Keys.Escape Then
            MeExit()
        End If
    End Sub
    Private Sub CheckEntry()
        If Not IsNumeric(TextBox_Count.Text) Then
            MessageBox.Show("Invalid Count")
            Return
        End If
        If CInt(TextBox_Count.Text) < 0 Then
            MessageBox.Show("Invalid Count")
            Return
        End If
        _Count = CInt(TextBox_Count.Text)
        MeExit()
    End Sub
    Private Sub MeExit()
        _isDisplay = False
        _DisplayEnd = True
        TextBox_Count.Text = ""
        Me.Hide()
    End Sub


    Public Sub Run()
        If _isDisplay Then
            Me.BringToFront()
            '   Me.TopLevel = True
            '  Me.TopMost = True
            Me.Show()
            TextBox_Count.Focus()
            _isDisplay = False
        End If
    End Sub
    Public Function Init(ByVal Devices As Dictionary(Of String, Object)) As Boolean
        mLanguage = CType(Devices(Language.Name), Language)
        Return True
    End Function

    Public Function ReLoadLanguage() As Boolean
        mLanguage.ReadControlText(Me)
        Return True
    End Function

    Private Sub ArticleCount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sResult As String
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = CInt((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2))
        sResult = 0

        If IsNumeric(sResult) Then
            Me.Left = (CInt(sResult) * My.Computer.Screen.WorkingArea.Width) + CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
        Else
            Me.Left = CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
        End If

    End Sub

    Private Sub ArticleCount_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        ReLoadLanguage()
    End Sub
End Class