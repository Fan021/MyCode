Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class ChildrenLoginForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cUserManager As clsUserManager
    Private strButtonName As String
    Public Event UserChanging(sender As Object, e As LasViewEventArgs)
    Private cLanguageManager As Language
    Private cTips As clsTips
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        Try
            Me.cSystemElement = Devices
            Me.cLocalElement = cLocalElement

            cLanguageManager = CType(Devices(Language.Name), Language)
            cTips = CType(Devices(clsTips.Name), clsTips)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            InitForm()
            InitControlText()
            Timer1.Enabled = True
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function InitControlText() As Boolean
        Label_Title.Text = cLanguageManager.Read("ChildrenLoginForm", "Label_Title")
        Button_Login.Text = cLanguageManager.Read("ChildrenLoginForm", "Button_Login")
        Button_Cancel.Text = cLanguageManager.Read("ChildrenLoginForm", "Button_Cancel")
        Comobox_UserName.Text = cLanguageManager.Read("ChildrenLoginForm", "TextBox_UserName")
        Comobox_UserName.ForeColor = System.Drawing.SystemColors.ControlDark
        TextBox_PassWord.Text = cLanguageManager.Read("ChildrenLoginForm", "TextBox_PassWord")

        TextBox_PassWord.ForeColor = System.Drawing.SystemColors.ControlDark
        Comobox_UserName.Items.Clear()
        For Each i As Integer In cUserManager.GetUserListKey
            Comobox_UserName.Items.Add(cUserManager.GetUserCfgFromKey(i).Name)
        Next

        AddHandler Comobox_UserName.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler Panel_UserNane.Resize, AddressOf Panel_Resize
        AddHandler Comobox_UserName.Click, AddressOf TextBox_Click
        AddHandler TextBox_PassWord.Click, AddressOf TextBox_Click
        AddHandler Comobox_UserName.Leave, AddressOf TextBox_Leave
        AddHandler TextBox_PassWord.Leave, AddressOf TextBox_Leave
        AddHandler Button_Login.Click, AddressOf Button_Click
        AddHandler Button_Cancel.Click, AddressOf Button_Click
        AddHandler Comobox_UserName.DropDown, AddressOf Comobox_DropDown
        Return True
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TableLayoutPanel_Body_Mid.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormRight)
        Button_UserName.FlatAppearance.MouseOverBackColor = Color.White
        Button_UserName.FlatAppearance.MouseDownBackColor = Color.White
        Button_PassWord.FlatAppearance.MouseOverBackColor = Color.White
        Button_PassWord.FlatAppearance.MouseDownBackColor = Color.White
        TopLevel = False
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body_Mid.RowStyles(0).Height = Comobox_UserName.Height + 36
            TableLayoutPanel_Body_Mid.RowStyles(1).Height = Comobox_UserName.Height + 36
            TableLayoutPanel_Body_Mid.RowStyles(2).Height = Comobox_UserName.Height + 36
            TableLayoutPanel_Body_Mid.RowStyles(3).Height = Comobox_UserName.Height + 36
            Comobox_UserName.Location = New Point(3, (Button_UserName.Height + 6 - Comobox_UserName.Height) / 2)
            TextBox_PassWord.Location = New Point(3, (Button_PassWord.Height + 6 - TextBox_PassWord.Height) / 2)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Panel_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Panel_UserNane"
                Comobox_UserName.Width = Panel_UserNane.Width
                Comobox_UserName.Location = New Point(3, (Button_UserName.Height + 6 - Comobox_UserName.Height) / 2)
            Case "Panel_PassWord"
                TextBox_PassWord.Width = Panel_PassWord.Width
                TextBox_PassWord.Location = New Point(3, (Button_PassWord.Height + 6 - TextBox_PassWord.Height) / 2)
        End Select
    End Sub

    Private Sub TextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Comobox_UserName"
                If Comobox_UserName.Text.ToUpper = "USERNAME" Then
                    Comobox_UserName.Text = ""
                    Comobox_UserName.ForeColor = System.Drawing.SystemColors.WindowText
                End If
            Case "TextBox_PassWord"
                If TextBox_PassWord.Text.ToUpper = "PASSWORD" Then
                    TextBox_PassWord.Text = ""
                    TextBox_PassWord.ForeColor = System.Drawing.SystemColors.WindowText
                End If

        End Select
    End Sub

    Private Sub TextBox_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Comobox_UserName"
                If Comobox_UserName.Text = "" Then
                    Comobox_UserName.Text = "UserName"
                    Comobox_UserName.ForeColor = System.Drawing.SystemColors.ControlDark
                End If
            Case "TextBox_PassWord"
                If TextBox_PassWord.Text = "" Then
                    TextBox_PassWord.Text = "PassWord"
                    TextBox_PassWord.ForeColor = System.Drawing.SystemColors.ControlDark
                End If

        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "Button_Login"
                Login()

            Case "Button_Cancel"
                Cancel()
        End Select
    End Sub

    Private Sub Login()
        If Comobox_UserName.Text = "" Then
            cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenLoginForm", "1"))
            Return
        End If
        If TextBox_PassWord.Text = "" Then
            cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenLoginForm", "2"))
            Return
        End If

        If Not cUserManager.HasUser(Comobox_UserName.Text) Then
            cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenLoginForm", "3", Comobox_UserName.Text))
            Return
        End If

        If Not cUserManager.HasUserAndPassWord(Comobox_UserName.Text, TextBox_PassWord.Text) Then
            cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenLoginForm", "4", Comobox_UserName.Text))
            Return
        End If
        cUserManager.ChangeUser(Comobox_UserName.Text)
        RaiseEvent UserChanging(Me, New LasViewEventArgs With {.IsMakeSure = False})
        ' cChangePage.BackPage()
        Quit(cLocalElement, cSystemElement)
    End Sub

    Private Sub Cancel()
        'If cUserManager.CurrentUserCfg.Level = enumUserLevel.Normal Then Return
        ' cChangePage.BackPage()
        RaiseEvent UserChanging(Me, New LasViewEventArgs With {.IsMakeSure = False})
        Quit(cLocalElement, cSystemElement)
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        '  Me.Dispose()

        Return True
    End Function



    Private Sub TextBox_PassWord_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox_PassWord.KeyDown
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub

    Private Sub Comobox_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Comobox_UserName.ForeColor = System.Drawing.SystemColors.WindowText
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Comobox_UserName.Focus()
    End Sub
End Class