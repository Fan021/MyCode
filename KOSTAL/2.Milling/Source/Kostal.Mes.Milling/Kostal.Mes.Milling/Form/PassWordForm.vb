Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class PassWordForm

    Inherits System.Windows.Forms.Form
    Private mMasterPasswords As New Dictionary(Of Long, String)
    Private mPassWordValid As Boolean
    Private mUserPassWord As String
    Private mUserPasswordKey As String
    Private mChangeMode As Boolean
    Private Const PassWordFormHeight As Long = 327 '337  
    Private Const ChangePassWordFormHeight As Long = 446

    Private Sub PassWordForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtPassword.Text = ""
        Me.TextNewPassword.Text = ""
        Me.TextConfirmNewPassword.Text = ""
    End Sub

    Private Sub ArticleCounter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
        End If
    End Sub

    Public Property ChangeMode() As Boolean
        Get
            Return mChangeMode
        End Get
        Set(ByVal value As Boolean)
            mChangeMode = value
            ChangeHeight()
        End Set
    End Property

    Private Sub ChangeHeight()
        If mChangeMode Then
            Me.Height = ChangePassWordFormHeight
            Me.NameOldPassword.Visible = True
        Else
            Me.Height = PassWordFormHeight
            Me.NameOldPassword.Visible = False
        End If
    End Sub

    Public ReadOnly Property MasterPassword(ByVal Index As Long) As String
        Get
            Try
                Return mMasterPasswords.Item(Index)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property


    Public ReadOnly Property UserPassword() As String
        Get
            Return mUserPassWord
        End Get
    End Property

    Public ReadOnly Property PassWordValid() As Boolean
        Get
            Return mPassWordValid
        End Get
    End Property


    Public Sub Init()
        mPassWordValid = False

        mUserPassWord = Security("Restore", "0")

        Me.Height = PassWordFormHeight
    End Sub

    Private Sub CheckEntry()
        If mUserPassWord = Me.txtPassword.Text Then
            mPassWordValid = True
            Me.Hide()
        Else
            MessageBox.Show("Invalid PassWord!")
        End If

    End Sub


    Private Sub PassWordForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If Not mChangeMode Then
            Me.txtPassword.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            CheckEntry()
        ElseIf e.KeyCode = Keys.Escape Then
            MeExit()
        End If


    End Sub

    Private Sub PassWordForm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        Me.txtPassword.Focus()
    End Sub

    Private Sub Password_PressedKey(ByVal Number As Integer) Handles PasswordKeyPad.PressedKey
        Select Case Number
            Case PasswordKeyPad.KeyCode_Clear : Me.txtPassword.Clear()
            Case PasswordKeyPad.KeyCode_Enter : CheckEntry()
            Case PasswordKeyPad.KeyCode_Esc : MeExit()
            Case PasswordKeyPad.KeyCode_0 : Me.txtPassword.Text = Me.txtPassword.Text & "0"
            Case PasswordKeyPad.KeyCode_1 : Me.txtPassword.Text = Me.txtPassword.Text & "1"
            Case PasswordKeyPad.KeyCode_2 : Me.txtPassword.Text = Me.txtPassword.Text & "2"
            Case PasswordKeyPad.KeyCode_3 : Me.txtPassword.Text = Me.txtPassword.Text & "3"
            Case PasswordKeyPad.KeyCode_4 : Me.txtPassword.Text = Me.txtPassword.Text & "4"
            Case PasswordKeyPad.KeyCode_5 : Me.txtPassword.Text = Me.txtPassword.Text & "5"
            Case PasswordKeyPad.KeyCode_6 : Me.txtPassword.Text = Me.txtPassword.Text & "6"
            Case PasswordKeyPad.KeyCode_7 : Me.txtPassword.Text = Me.txtPassword.Text & "7"
            Case PasswordKeyPad.KeyCode_8 : Me.txtPassword.Text = Me.txtPassword.Text & "8"
            Case PasswordKeyPad.KeyCode_9 : Me.txtPassword.Text = Me.txtPassword.Text & "9"

        End Select
    End Sub

    Public Function RandomPassWord() As String
        Dim cResult As String, vX As Integer

        Randomize()
        RandomPassWord = ""
        For vX = 1 To 10
            RandomPassWord = RandomPassWord & Chr(CInt(127 * Rnd()))
        Next

        cResult = Security("Destroy", RandomPassWord)
    End Function


    Public Function Security(ByVal Order As String, ByVal Password As String) As String
        Dim vZ As Integer
        Security = ""
        Select Case LCase(Order)
            Case "destroy"
                For vZ = 1 To Len(Password)
                    Security = Password ' Security & Chr(Asc(Mid(Password, vZ, 1)) + 127)   'changed by wang65
                Next
            Case "restore"
                Try
                    For vZ = 1 To Len(Password)
                        Security = Password ' Security & Chr(Asc(Mid$(Password, vZ, 1)) - 127)  'changed by wang65
                    Next
                    Exit Try
                Catch
                    mUserPassWord = RandomPassWord()
                End Try

            Case Else
        End Select
    End Function


    Private Sub MeExit()
        mPassWordValid = False
        'Me.Dispose()
        ChangeHeight()
        Me.Hide()
    End Sub

    Private Sub PassWordForm_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtPassword.Focus()
    End Sub
End Class