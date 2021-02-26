Public Class Login
    Private fileHander As New FileHandler
    Private dataStoreCfg As New DataCfg
    Private UserData As New UserData
    Private SessionData As New SessionData
    Private Frm As FormConfig
    Private MyProcess As New ProcessControl
    Private Sub Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            MyProcess.FileName = "Kostal.Mes.Milling.exe"
            MyProcess.ExeCount = 1
            If MyProcess.IsMyProcessRunning Then
                MessageBox.Show("请关闭Kostal.Mes.Milling.exe程序")
                Application.Exit()
            End If

            MyProcess.FileName = "Kostal.Mes.Milling.Config.exe"
            MyProcess.ExeCount = 2
            If MyProcess.IsMyProcessRunning Then
                MessageBox.Show("Kostal.Mes.Milling.Config.exe程序已存在")
                Application.Exit()
            End If

            dataStoreCfg.DBServer = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "server")
            dataStoreCfg.DBPort = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "Port")
            dataStoreCfg.DBUserName = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "UserName")
            dataStoreCfg.DBPassWord = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "PassWord")
            dataStoreCfg.DBName = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "Name")
            dataStoreCfg.DBUserTable = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "UserTable")
            dataStoreCfg.DBConfigTable = fileHander.Read(My.Application.Info.DirectoryPath + "\Config", "SqlConfig.ini", "Config", "ConfigTable")
            'UserData.Init(dataStoreCfg)
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
            Return
        End If
        If TextBox_Password.Text = "" Then
            MessageBox.Show("密码不能为空")
            Return
        End If

        If Not UserData.CheckLogin(TextBox_Name.Text, TextBox_Password.Text) Then
            MessageBox.Show("用户名或密码错误")
            Return
        End If
        Me.Hide()
        SessionData.UserName = TextBox_Name.Text
        SessionData.PassWord = TextBox_Password.Text
        SessionData.Level = UserData.GetLevel(TextBox_Name.Text, TextBox_Password.Text)
        Frm = New FormConfig
        Frm.SessionData = SessionData
        Frm.dataStoreCfg = dataStoreCfg
        Frm.ShowDialog()

    End Sub

    Private Sub Button_Reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Reset.Click
        TextBox_Name.Text = ""
        TextBox_Password.Text = ""
    End Sub
End Class


