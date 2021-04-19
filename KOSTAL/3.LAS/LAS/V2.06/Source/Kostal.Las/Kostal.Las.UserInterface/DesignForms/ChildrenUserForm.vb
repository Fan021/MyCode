Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class ChildrenUserForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cUserManager As clsUserManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As Language
    Private strButtonName As String
    Private _Settings As Settings
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
            _Settings = MySettings
            cTips = CType(Devices(clsTips.Name), clsTips)
            cLanguageManager = CType(Devices(Language.Name), Language)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cUserManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)

            InitForm()
            InitControlText()
            SetControls(Me)
            Return True
        Catch ex As Exception
            Throw ex
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
        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Operator Then
            If cUserManager.CurrentUserCfg.Name <> "" Then
                HmiTextBox_Name.TextBox.Text = cUserManager.CurrentUserCfg.Name
            Else
                HmiTextBox_Name.TextBox.Text = "NA"
            End If
            HmiComboBox_Level.ComboBox.Text = cUserManager.CurrentUserCfg.Level.ToString
            HmiTextBox_Name.TextBoxReadOnly = True
            HmiComboBox_Level.ComboBox.Enabled = False
            HmiTextBox_Function_Name.TextBoxReadOnly = False
            HmiComboBox_Function_Level.ComboBox.Enabled = True
            HmiButton_Function_Del.Button.Enabled = True
            HmiButton_Function_Add.Button.Enabled = True
        End If
        Search()
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Name.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Name")
        HmiLabel_Level.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Level")
        HmiButton_Search.Button.Text = cLanguageManager.Read("ChildrenUserForm", "HmiButton_Search")
        HmiButton_Cancel.Button.Text = cLanguageManager.Read("ChildrenUserForm", "HmiButton_Cancel")
        HmiLabel_Function_ID.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Function_ID")
        HmiLabel_Function_Name.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Function_Name")
        HmiLabel_Function_Level.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Function_Level")
        HmiLabel_Function_Password.Label.Text = cLanguageManager.Read("ChildrenUserForm", "HmiLabel_Function_Password")
        HmiButton_Function_Add.Button.Text = cLanguageManager.Read("ChildrenUserForm", "HmiButton_Function_Add")
        HmiButton_Function_Del.Button.Text = cLanguageManager.Read("ChildrenUserForm", "HmiButton_Function_Del")
        HmiButton_Function_Modify.Button.Text = cLanguageManager.Read("ChildrenUserForm", "HmiButton_Function_Modify")

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
            TableLayoutPanel_Body.RowStyles(0).SizeType = System.Windows.Forms.SizeType.Absolute
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Name.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Name.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_Name.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Name.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Name.TextBox.Height + 6 + 6
            Next
            'Dim iCnt As Integer = 0
            'For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
            '    If iCnt Mod 2 = 0 Then
            '        element.SizeType = System.Windows.Forms.SizeType.Absolute
            '        element.Height = HmiTextBox_Name.TextBox.Height - 6
            '    Else
            '        element.SizeType = System.Windows.Forms.SizeType.Absolute
            '        element.Height = HmiTextBox_Name.TextBox.Height + 6 + 6
            '    End If

            '    If iCnt >= 8 Then
            '        element.SizeType = System.Windows.Forms.SizeType.Absolute
            '        element.Height = HmiTextBox_Name.TextBox.Height + 6 + 12
            '    End If
            '    iCnt = iCnt + 1
            'Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SetControls(ByVal cons As Control)
        For Each con As Control In cons.Controls
            con.Font = New System.Drawing.Font("Calibri", 10)
            If con.Controls.Count > 0 Then
                SetControls(con)
            End If
        Next
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
            Throw ex
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_Name.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "1"))
                Return
            End If

            If HmiTextBox_Function_Password.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "2"))
                Return
            End If

            If HmiComboBox_Function_Level.ComboBox.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "3"))
                Return
            End If

            If cUserManager.HasUser(HmiTextBox_Function_Name.Text) Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "4", HmiTextBox_Function_Name.Text))
                Return
            End If

            If Not [Enum].IsDefined(GetType(enumUserLevel), HmiComboBox_Function_Level.ComboBox.Text) Then
                ' cErrorPasswordManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenUserForm.ToString, "6", HmiComboBox_Function_Level.ComboBox.Text), enumExceptionType.Alarm, enumUIName.ChildrenUserForm.ToString))
                Return
            End If

            cUserManager.InSertData((cUserManager.GetUserListKey.Count).ToString, HmiTextBox_Function_Name.Text, HmiTextBox_Function_Password.Text, HmiComboBox_Function_Level.ComboBox.Text)
            cUserManager.SelectToDataView(enumViewPageType.LastPage)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "5"))
                Return
            End If

            If HmiTextBox_Function_Name.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "6"))
                Return
            End If

            If HmiTextBox_Function_Password.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "7"))
                Return
            End If

            If HmiComboBox_Function_Level.ComboBox.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "8"))
                Return
            End If

            If cUserManager.HasUser(HmiTextBox_Function_Name.Text, HmiTextBox_Function_ID.Text) Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "9", HmiTextBox_Function_Name.Text))
                Return
            End If

            cUserManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Name.Text, HmiTextBox_Function_Password.Text, HmiComboBox_Function_Level.ComboBox.Text)
            cUserManager.SelectToDataView(enumViewPageType.NoPage)
            If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then
                cUserManager.LoginOut()
            Else
                If cUserManager.CurrentUserCfg.Name = HmiTextBox_Function_Name.Text Then
                    cUserManager.LoginOut()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Level.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenUserForm", "5"))
                Return
            End If
            cUserManager.DeleteData(HmiTextBox_Function_ID.Text)
            cUserManager.SelectToDataView(enumViewPageType.NoPage)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Name.Text = ""
            HmiTextBox_Function_Password.Text = ""
            HmiComboBox_Function_Level.ComboBox.SelectedIndex = -1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Search()
        Try
            cUserManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Name.Text, HmiComboBox_Level.ComboBox.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Name.TextBox.Text = ""
            HmiComboBox_Function_Level.ComboBox.SelectedIndex = -1
            cUserManager.SelectToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            Throw ex
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


    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.Dispose()
        Return True
    End Function

    Private Sub Timer_Show_Tick(sender As Object, e As EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub
End Class