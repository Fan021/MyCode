﻿Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenTextListForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cTextManager As clsTextManager
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
            cTextManager = CType(cSystemElement(clsTextManager.Name), clsTextManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cTextManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenTextListForm.ToString, Me)
            Timer_Show.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenTextListForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        cTextManager.SelectToDataView(enumViewPageType.FirstPage)
        Return True
    End Function


    Public Function InitControlText() As Boolean
        HmiLabel_Key.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Key")
        HmiLabel_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Message")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Function_ID")
        HmiLabel_Function_Key.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Function_Key")
        HmiButton_Function_Add.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiButton_Function_Add")
        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Del.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiButton_Function_Del")
        HmiTextBox_Function_ID.TextBoxReadOnly = True

        HmiDataView_Data.AllowUserToResizeColumns = True
        HmiDataView_Data.AllowUserToResizeRows = True
        HmiTextBox_Function_Message.Multiline = True
        HmiTextBox_Function_Message.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Message.WordWrap = False
        HmiTextBox_Function_Message2.Multiline = True
        HmiTextBox_Function_Message2.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Message2.WordWrap = False
        HmiTextBox_Function_Message.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiTextBox_Function_Message2.Font = New System.Drawing.Font("Calibri", 12.0!)

        If cLanguageManager.SecondLanguageEnable Then
            HmiLabel_Function_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Function_Message", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage))
            HmiLabel_Function_Message2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Function_Message2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage))
            HmiLabel_Function_Message.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiLabel_Function_Message2.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Else
            HmiLabel_Function_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "HmiLabel_Function_Message3")
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End If

        AddHandler HmiTextBox_Key.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiTextBox_Function_Key.TextBox.KeyPress, AddressOf TextBox_KeyPress
        ' AddHandler HmiTextBox_Function_Message.Enter, AddressOf HmiTextBox_Function_Enter
        ' AddHandler HmiTextBox_Function_Message2.Enter, AddressOf HmiTextBox_Function_Enter
        ' AddHandler HmiTextBox_Function_Message.Leave, AddressOf HmiTextBox_Function_Leave
        ' AddHandler HmiTextBox_Function_Message2.Leave, AddressOf HmiTextBox_Function_Leave
        AddHandler HmiDataView_Data.CellClick, AddressOf HmiDataView_Data_CellClick
        Return True
    End Function

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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If

            'If HmiTextBox_Function_Message.Text = "" Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "2"), enumExceptionType.Alarm))
            '    Return
            'End If

            If cTextManager.HasText(HmiTextBox_Function_Key.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If

            cTextManager.InSertData((cTextManager.GetTextListKey.Count + 1).ToString, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text)
            cTextManager.SelectToDataView(enumViewPageType.LastPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If

            'If HmiTextBox_Function_Message.Text = "" Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "2"), enumExceptionType.Alarm))
            '    Return
            'End If

            If cTextManager.HasText(HmiTextBox_Function_Key.Text, HmiTextBox_Function_ID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If

            cTextManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Message.Text, HmiTextBox_Function_Message2.Text)
            cTextManager.SelectToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenTextListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
                Return
            End If
            cTextManager.DeleteData(HmiTextBox_Function_ID.Text)
            cTextManager.SelectToDataView(enumViewPageType.NoPage)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Key.Text = ""
            HmiTextBox_Function_Message.Text = ""
            HmiTextBox_Function_Message2.Text = ""
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            cTextManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Key.Text, HmiTextBox_Message.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Message.TextBox.Text = ""
            cTextManager.SelectToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenTextListForm.ToString))
        End Try
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Key.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            If cLanguageManager.SecondLanguageEnable Then
                HmiTextBox_Function_Message.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
                HmiTextBox_Function_Message2.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
            Else
                HmiTextBox_Function_Message.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
            End If

        End If
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

    'Private Sub HmiDataView_Data_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HmiDataView_Data.Resize
    '    For Each element As DataGridViewTextBoxColumn In HmiDataView_Data.Columns
    '        Select Case element.Index
    '            Case 0
    '                element.Width = (HmiDataView_Data.Width / 100) * 10
    '            Case 1
    '                element.Width = (HmiDataView_Data.Width / 100) * 30
    '            Case 2
    '                element.Width = (HmiDataView_Data.Width / 100) * 70
    '        End Select
    '    Next
    'End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenTextListForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenTextListForm.ToString)
        Me.Dispose()
        Return True
    End Function

    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub
End Class