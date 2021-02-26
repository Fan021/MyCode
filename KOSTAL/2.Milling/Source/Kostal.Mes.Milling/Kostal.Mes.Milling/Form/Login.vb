Imports System.Collections.Generic
Imports System.Windows.Forms
Public Class Login
    Private fileHander As New FileHandler
    Public setting As Settings
    Public Level As String
    Public PassWordValid As Boolean
    Private UserData As New UserData
    Private SessionData As New SessionData
    Private MyProcess As New ProcessControl

    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            UserData.Init(setting)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            Application.Exit()
        End Try
    End Sub

    Private Sub Button_Login_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Login.Click
        Login()
    End Sub

    Private Sub TextBox_Password_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox_Password.KeyDown
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub

    Private Sub Login()
        If TextBox_Name.Text = "" Then
            MessageBox.Show("用户名不能为空")
            PassWordValid = False
            Return
        End If
        If TextBox_Password.Text = "" Then
            MessageBox.Show("密码不能为空")
            PassWordValid = False
            Return
        End If

        If Not UserData.CheckLogin(TextBox_Name.Text, TextBox_Password.Text) Then
            MessageBox.Show("用户名或密码错误")
            PassWordValid = False
            Return
        End If


        SessionData.UserName = TextBox_Name.Text
        SessionData.PassWord = TextBox_Password.Text
        SessionData.Level = UserData.GetLevel(TextBox_Name.Text, TextBox_Password.Text)
        If SessionData.Level < Level Then
            MessageBox.Show("用户等级错误")
            PassWordValid = False
            Return
        End If
        PassWordValid = True
        TextBox_Name.Text = ""
        TextBox_Password.Text = ""
        Me.Hide()
    End Sub

    Private Sub Button_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Reset.Click
        TextBox_Name.Text = ""
        TextBox_Password.Text = ""
    End Sub
End Class


