Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenUserForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cUserManager As clsUserManager
    Private cErrorPasswordManager As clsErrorMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private strButtonName As String
    Private cMainButtonManager As clsMainButtonManager
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
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cErrorPasswordManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMainButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cUserManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenUserForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenUserForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        For Each eType As enumUserLevel In [Enum].GetValues(GetType(enumUserLevel))
            If eType = enumUserLevel.Normal Then Continue For
            HmiComboBox_Level.ComboBox.Items.Add(eType.ToString)
            HmiComboBox_Function_Level.ComboBox.Items.Add(eType.ToString)
        Next
        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then
            If cUserManager.CurrentUserCfg.Name <> "" Then
                HmiTextBox_Name.TextBox.Text = cUserManager.CurrentUserCfg.Name
            Else
                HmiTextBox_Name.TextBox.Text = "NA"
            End If
            HmiComboBox_Level.ComboBox.Text = cUserManager.CurrentUserCfg.Level.ToString
            HmiTextBox_Name.TextBoxReadOnly = True
            HmiComboBox_Level.ComboBox.Enabled = False
            HmiTextBox_Function_Name.TextBoxReadOnly = True
            HmiComboBox_Function_Level.ComboBox.Enabled = False
            HmiButton_Function_Del.Button.Enabled = False
            HmiButton_Function_Add.Button.Enabled = False
        End If
        Search()
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Name.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Name")
        HmiLabel_Level.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Level")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Function_ID")
        HmiLabel_Function_Name.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Function_Name")
        HmiLabel_Function_Password.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Function_Password")
        HmiLabel_Function_Level.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiLabel_Function_Level")
        HmiButton_Function_Add.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiButton_Function_Add")
        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Del.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "HmiButton_Function_Del")
        HmiTextBox_Function_ID.TextBoxReadOnly = True
        AddHandler HmiTextBox_Name.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Name.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Name.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_Name.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Name.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Name.TextBox.Height + 6 + 6
            Next
            For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Name.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub

    Private Sub HmiButton_Function_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_Function_Add"
                    Add()
                Case "HmiButton_Function_Modify"
                    Modify()
                Case "HmiButton_Function_Del"
                    Delete()
                Case "HmiButton_Search"
                    Search()
                Case "HmiButton_Cancel"
                    Cancel()
            End Select
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_Name.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Password.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If HmiComboBox_Function_Level.ComboBox.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If cUserManager.HasUser(HmiTextBox_Function_Name.Text) Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "4", HmiTextBox_Function_Name.Text), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If Not [Enum].IsDefined(GetType(enumUserLevel), HmiComboBox_Function_Level.ComboBox.Text) Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "6", HmiComboBox_Function_Level.ComboBox.Text), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            cUserManager.InSertData((cUserManager.GetUserListKey.Count + 1).ToString, HmiTextBox_Function_Name.Text, HmiTextBox_Function_Password.Text, HmiComboBox_Function_Level.ComboBox.Text)
            cUserManager.SelectToDataView(enumViewPageType.LastPage)
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "5"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Name.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Password.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If HmiComboBox_Function_Level.ComboBox.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            If cUserManager.HasUser(HmiTextBox_Function_Name.Text, HmiTextBox_Function_ID.Text) Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "4", HmiTextBox_Function_Name.Text), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            cUserManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Name.Text, HmiTextBox_Function_Password.Text, HmiComboBox_Function_Level.ComboBox.Text)
            cUserManager.SelectToDataView(enumViewPageType.NoPage)
            If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then
                cMainButtonManager.BackHome()
                cUserManager.LoginOut()
            Else
                If cUserManager.CurrentUserCfg.Name = HmiTextBox_Function_Name.Text Then
                    cMainButtonManager.BackHome()
                    cUserManager.LoginOut()
                End If
            End If

        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "5"), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If
            cUserManager.DeleteData(HmiTextBox_Function_ID.Text)
            cUserManager.SelectToDataView(enumViewPageType.NoPage)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Name.Text = ""
            HmiTextBox_Function_Password.Text = ""
            HmiComboBox_Function_Level.ComboBox.SelectedIndex = -1
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            cUserManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Name.Text, HmiComboBox_Level.ComboBox.Text)
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Function_Level.ComboBox.SelectedIndex = -1
            cUserManager.SelectToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorPasswordManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
        End Try
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Name.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiTextBox_Function_Password.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
            HmiComboBox_Function_Level.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
        End If
    End Sub


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenUserForm.ToString)
        cErrorPasswordManager.Clean(enumUIName.ChildrenUserForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class