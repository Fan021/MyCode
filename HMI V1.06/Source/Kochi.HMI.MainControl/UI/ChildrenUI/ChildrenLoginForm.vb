Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenLoginForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cErrorMessageManager As clsErrorMessageManager
    Private cMainTipsManager As clsMainTipsManager
    Private cChangePage As clsChangePage
    Private cUserManager As clsUserManager
    Private strButtonName As String
    Private cLanguageManager As clsLanguageManager
    Private cProcessStart As clsProcessStart
    Private cMachineManager As clsMachineManager
    Private cSystemManager As clsSystemManager

    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cChangePage = CType(cSystemElement(clsChangePage.Name), clsChangePage)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            InitForm()
            InitControlText()
            If Not cLocalElement.ContainsKey(enumUIName.ChildrenLoginForm.ToString) Then cLocalElement.Add(enumUIName.ChildrenLoginForm.ToString, Me)
            If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.TouchKeyBoard) = "TRUE" Then
                System.Threading.Thread.Sleep(50)
                cProcessStart = New clsProcessStart
                System.Threading.Thread.Sleep(50)
                cProcessStart.Start("C:\Windows\System32", "osk.exe")
            End If
            Timer1.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenLoginForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitControlText() As Boolean
        Label_Title.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "Label_Title")
        Button_Login.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "Button_Login")
        Button_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "Button_Cancel")
        Comobox_UserName.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "TextBox_UserName")
        Comobox_UserName.ForeColor = System.Drawing.SystemColors.ControlDark
        TextBox_PassWord.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "TextBox_PassWord")
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenLoginForm.ToString))
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
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenLoginForm.ToString))
            Return
        End If
        If TextBox_PassWord.Text = "" Then
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenLoginForm.ToString))
            Return
        End If

        If Not cUserManager.HasUser(Comobox_UserName.Text) Then
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenLoginForm.ToString))
            Return
        End If

        If Not cUserManager.HasUserAndPassWord(Comobox_UserName.Text, TextBox_PassWord.Text) Then
            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenLoginForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenLoginForm.ToString))
            Return
        End If
        cUserManager.ChangeUser(Comobox_UserName.Text)

        cChangePage.BackPage()
        Quit(cLocalElement, cSystemElement)
    End Sub

    Private Sub Cancel()
        If cUserManager.CurrentUserCfg.Level = enumUserLevel.Normal Then Return
        cChangePage.BackPage()
        Quit(cLocalElement, cSystemElement)
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenLoginForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenLoginForm.ToString)

        If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.TouchKeyBoard) = "TRUE" Then
            If Not IsNothing(cProcessStart) Then
                cProcessStart.Stop()
                System.Threading.Thread.Sleep(10)
                cProcessStart.Start(cSystemManager.Settings.ExeFolder, "KillProcess.bat", False)
            End If
        End If
        Me.Dispose()

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