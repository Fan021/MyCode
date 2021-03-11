Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenPlcMessageListForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cPlcMessageManager As clsPlcMessageManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private strButtonName As String

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
            cPlcMessageManager = CType(cSystemElement(clsPlcMessageManager.Name), clsPlcMessageManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cPlcMessageManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenPlcMessageListForm.ToString, Me)
            Timer_Show.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
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
        HmiLabel_Key.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Key")
        HmiLabel_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Message")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_ID")
        HmiLabel_Function_Key.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Key")
        HmiButton_Function_Add.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Function_Add")
        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Del.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Function_Del")
        HmiTextBox_Function_ID.TextBoxReadOnly = True
        HmiLabel_Choose.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Choose")
        HmiButton_Choose.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiButton_Choose")

        HmiTextBox_Function_Message.Multiline = True
        HmiTextBox_Function_Message.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Message.WordWrap = False
        HmiTextBox_Function_Message2.Multiline = True
        HmiTextBox_Function_Message2.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Message2.WordWrap = False
        HmiTextBox_Function_Message.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiTextBox_Function_Message2.Font = New System.Drawing.Font("Calibri", 12.0!)

        If cLanguageManager.SecondLanguageEnable Then
            HmiLabel_Function_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Message", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage))
            HmiLabel_Function_Message2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Message2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage))
            HmiLabel_Function_Message.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiLabel_Function_Message2.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Else
            HmiLabel_Function_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "HmiLabel_Function_Message3")
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End If

        HmiTextBox_Function_Key.ValueType = GetType(Integer)
        HmiTextBox_Key.ValueType = GetType(Integer)

        AddHandler HmiTextBox_Key.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Choose.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiTextBox_Function_Key.TextBox.KeyPress, AddressOf TextBox_KeyPress
        '  AddHandler HmiTextBox_Function_Message.Enter, AddressOf HmiTextBox_Function_Enter
        ' AddHandler HmiTextBox_Function_Message2.Enter, AddressOf HmiTextBox_Function_Enter
        ' AddHandler HmiTextBox_Function_Message.Leave, AddressOf HmiTextBox_Function_Leave
        ' AddHandler HmiTextBox_Function_Message2.Leave, AddressOf HmiTextBox_Function_Leave
        Return True
    End Function



    Public Sub Open()
        Try
            OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                     "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                      "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
            OpenFileDialog_Path.RestoreDirectory = True
            OpenFileDialog_Path.FilterIndex = 1
            If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
                HmiTextBox_Path.TextBox.Text = OpenFileDialog_Path.FileName
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Key.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Key.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_Key.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Key.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6
            Next


            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6
                If cLanguageManager.SecondLanguageEnable Then
                    If iCnt = 4 Or iCnt = 6 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6 + 12
                    End If
                    If iCnt = 5 Or iCnt = 7 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6 + 40
                    End If
                Else
                    If iCnt = 5 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6 + 40
                    End If
                    If iCnt = 6 Or iCnt = 7 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = 0
                    End If
                End If
                iCnt = iCnt + 1
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
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
                Case "HmiButton_Choose"
                    Open()
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If

            'If HmiTextBox_Function_Message.Text = "" Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "2"), enumExceptionType.Alarm))
            '    Return
            'End If

            If cPlcMessageManager.HasPlcMessage(HmiTextBox_Function_Key.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If

            cPlcMessageManager.InSertData((cPlcMessageManager.GetPlcMessageListKey.Count + 1).ToString, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text, HmiTextBox_Path.TextBox.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.DefinePage, HmiTextBox_Function_Key.TextBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try

            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If

            'If HmiTextBox_Function_Message.Text = "" Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "2"), enumExceptionType.Alarm))
            '    Return
            'End If

            If cPlcMessageManager.HasPlcMessage(HmiTextBox_Function_Key.Text, HmiTextBox_Function_ID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If

            cPlcMessageManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text, HmiTextBox_Path.TextBox.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.DefinePage, HmiTextBox_Function_Key.TextBox.Text)
            '   HmiTextBox_Function_Key.TextBox.Text = ""
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPlcMessageListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenPlcMessageListForm.ToString))
                Return
            End If
            cPlcMessageManager.DeleteData(HmiTextBox_Function_ID.Text)
            cPlcMessageManager.SelectToDataView(enumViewPageType.NoPage, HmiTextBox_Function_Key.TextBox.Text)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Key.Text = ""
            HmiTextBox_Function_Message.Text = ""
            HmiTextBox_Function_Message2.Text = ""
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            cPlcMessageManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Function_Key.TextBox.Text, HmiTextBox_Key.Text, HmiTextBox_Message.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            cPlcMessageManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Function_Key.TextBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPlcMessageListForm.ToString))
        End Try
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Key.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            If cLanguageManager.SecondLanguageEnable Then
                HmiTextBox_Function_Message.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
                HmiTextBox_Function_Message2.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
                HmiTextBox_Path.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(4).Value
            Else
                HmiTextBox_Function_Message.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
                HmiTextBox_Path.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
            End If

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

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenPlcMessageListForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenPlcMessageListForm.ToString)
        Me.Dispose()
        Return True
    End Function

    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub


    Private Su

    Private Sub HmiTextBox_Function_Message_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles HmiTextBox_Function_Message.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
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