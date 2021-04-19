
Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.Windows.Forms
Imports Kostal.Las.Base

Public Class ChildrenPlcMessageListForm
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cPlcMessageManager As clsPlcMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As Language
    Private strButtonName As String
    Private _FileHandler As New FileHandler
    Private AppSettings As Settings
    Private cTips As clsTips
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            Me.cSystemElement = Devices
            Me.cLocalElement = cLocalElement
            AppSettings = _AppSettings
            cTips = CType(Devices(clsTips.Name), clsTips)
            cLanguageManager = CType(Devices(Language.Name), Language)
            cPlcMessageManager = CType(cSystemElement(clsPlcMessageManager.Name), clsPlcMessageManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cPlcMessageManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            SetControls(Me)
            Timer_Show.Enabled = True
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        AddHandler PopupVariant.Click, AddressOf Variant_Click
        cPlcMessageManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Function_Key.TextBox.Text)
        Return True
    End Function


    Public Function InitControlText() As Boolean
        HmiLabel_Key.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Key")
        HmiLabel_Message.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Message")
        HmiButton_Search.Button.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiButton_Search")
        HmiButton_Cancel.Button.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiButton_Cancel")
        HmiLabel_Function_ID.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Function_ID")
        HmiLabel_Function_Key.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Function_Key")
        HmiLabel_Function_Message.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Function_Message")
        HmiLabel_Function_Message2.Label.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiLabel_Function_Message2")
        HmiButton_Function_Add.Button.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiButton_Function_Add")
        HmiButton_Function_Del.Button.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiButton_Function_Del")
        HmiButton_Function_Modify.Button.Text = cLanguageManager.Read("ChildrenPlcMessageListForm", "HmiButton_Function_Modify")
        HmiTextBox_Function_ID.TextBoxReadOnly = True

        'HmiTextBox_Function_Message.Multiline = True
        'HmiTextBox_Function_Message.ScrollBars = ScrollBars.Both
        'HmiTextBox_Function_Message.WordWrap = False
        'HmiTextBox_Function_Message2.Multiline = True
        'HmiTextBox_Function_Message2.ScrollBars = ScrollBars.Both
        'HmiTextBox_Function_Message2.WordWrap = False
        HmiTextBox_Function_Message.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiTextBox_Function_Message2.Font = New System.Drawing.Font("Calibri", 12.0!)


        ' HmiLabel_Function_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Message", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage))
        ' HmiLabel_Function_Message2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Message2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage))
        HmiLabel_Function_Message.Label.TextAlign = ContentAlignment.MiddleLeft
        HmiLabel_Function_Message2.Label.TextAlign = ContentAlignment.MiddleLeft
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells


        HmiTextBox_Function_Key.ValueType = GetType(Integer)
        HmiTextBox_Key.ValueType = GetType(Integer)

        AddHandler HmiTextBox_Key.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiTextBox_Function_Key.TextBox.KeyPress, AddressOf TextBox_KeyPress
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).SizeType = System.Windows.Forms.SizeType.Absolute
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Key.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Key.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_Key.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Key.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6
            Next


            'Dim iCnt As Integer = 0
            'For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
            '    element.SizeType = System.Windows.Forms.SizeType.Percent
            '    ' element.Height = 10

            '    iCnt = iCnt + 1
            'Next
        Catch ex As Exception
            Throw ex
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
            Throw ex
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_Key.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "1"))
                Return
            End If

            If cPlcMessageManager.HasPlcMessage(HmiTextBox_Function_Key.Text) Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "2", HmiTextBox_Function_Key.Text))
                Return
            End If

            cPlcMessageManager.InSertData((cPlcMessageManager.GetPlcMessageListKey.Count + 1).ToString, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.DefinePage, HmiTextBox_Function_Key.TextBox.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Modify()
        Try

            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "3"))
                Return
            End If

            If HmiTextBox_Function_Key.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "4"))
                Return
            End If


            If cPlcMessageManager.HasPlcMessage(HmiTextBox_Function_Key.Text, HmiTextBox_Function_ID.Text) Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "5", HmiTextBox_Function_Key.Text))
                Return
            End If

            cPlcMessageManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.DefinePage, HmiTextBox_Function_Key.TextBox.Text)
            '   HmiTextBox_Function_Key.TextBox.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cTips.AddTips(cLanguageManager.LanguageElement.GetText("ChildrenPlcMessageListForm", "3"))
                Return
            End If
            cPlcMessageManager.DeleteData(HmiTextBox_Function_ID.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.NoPage, HmiTextBox_Function_Key.TextBox.Text)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Key.Text = ""
            HmiTextBox_Function_Message.Text = ""
            HmiTextBox_Function_Message2.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Search()
        Try
            cPlcMessageManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Function_Key.TextBox.Text, HmiTextBox_Key.Text, HmiTextBox_Message.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            cPlcMessageManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Function_Key.TextBox.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Key.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value

            HmiTextBox_Function_Message.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
            HmiTextBox_Function_Message2.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value


        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
    End Sub

    Private Sub HmiTextBox_Function_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Function_Message"
                TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_Function_Key.TextBox.Height + 6 + 6 + 100

            Case "HmiTextBox_Function_Message2"
                TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_Function_Key.TextBox.Height + 6 + 6 + 100

        End Select

    End Sub

    Private Sub HmiTextBox_Function_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Function_Message"
                TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_Function_Key.TextBox.Height + 6 + 6 + 40
            Case "HmiTextBox_Function_Message2"
                TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_Function_Key.TextBox.Height + 6 + 6 + 40

        End Select
    End Sub

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.Dispose()
        Return True
    End Function

    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub


    Public Sub SetControls(ByVal cons As Control)
        For Each con As Control In cons.Controls
            con.Font = New System.Drawing.Font("Calibri", 10)
            If con.Controls.Count > 0 Then
                SetControls(con)
            End If
        Next
    End Sub

    Private Sub HmiTextBox_Function_Message_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip_Menu.Show(HmiTextBox_Function_Message, e.Location)
            ContextMenuStrip_Menu.Tag = HmiTextBox_Function_Message
        End If

    End Sub
    Private Sub Variant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim HmiTextBox_Message As TextBox = ContextMenuStrip_Menu.Tag
        Dim idx As Integer = HmiTextBox_Message.SelectionStart
        Dim s As String = "[Variant]"
        HmiTextBox_Message.Text = HmiTextBox_Message.Text.Insert(idx, s)
        HmiTextBox_Message.SelectionStart = idx + s.Length
        HmiTextBox_Message.Focus()
    End Sub
End Class