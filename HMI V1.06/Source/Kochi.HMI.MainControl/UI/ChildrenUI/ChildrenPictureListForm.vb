Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenPictureListForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cPictureManager As clsPictureManager
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
            cPictureManager = CType(cSystemElement(clsPictureManager.Name), clsPictureManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cPictureManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenPictureListForm.ToString, Me)
            Timer_Show.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        cPictureManager.SelectToDataView(enumViewPageType.FirstPage)
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_Key.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiLabel_Key")
        HmiLabel_Path.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiLabel_Path")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiLabel_Function_ID")
        HmiLabel_Function_Key.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiLabel_Function_Key")
        HmiLabel_Function_Path.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiLabel_Function_Path")
        HmiButton_Function_File.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Function_File")
        HmiButton_Function_Add.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Function_Add")
        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Del.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "HmiButton_Function_Del")
        HmiTextBox_Function_ID.TextBoxReadOnly = True
        HmiTextBox_Function_Path.TextBoxReadOnly = True
        AddHandler HmiTextBox_Key.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_File.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiTextBox_Function_Key.TextBox.KeyPress, AddressOf TextBox_KeyPress
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

            For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Key.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
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
                Case "HmiButton_Function_File"
                    Open()
                Case "HmiButton_Search"
                    Search()
                Case "HmiButton_Cancel"
                    Cancel()

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub

    Public Sub Add()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Path.TextBox.Text = ""
            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Path.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            If cPictureManager.HasPicture(HmiTextBox_Function_Key.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            cPictureManager.InSertData((cPictureManager.GetPictureListKey.Count + 1).ToString, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Path.Text)
            cPictureManager.SelectToDataView(enumViewPageType.LastPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Path.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Key.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Path.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            If cPictureManager.HasPicture(HmiTextBox_Function_Key.Text, HmiTextBox_Function_ID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "3", HmiTextBox_Function_Key.Text), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If

            cPictureManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Key.Text, HmiTextBox_Function_Path.Text)
            cPictureManager.SelectToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Path.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenPictureListForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenPictureListForm.ToString))
                Return
            End If
            cPictureManager.DeleteData(HmiTextBox_Function_ID.Text)
            cPictureManager.SelectToDataView(enumViewPageType.NoPage)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Key.Text = ""
            HmiTextBox_Function_Path.Text = ""
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            cPictureManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_Key.Text, HmiTextBox_Path.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_Key.TextBox.Text = ""
            HmiTextBox_Function_Path.TextBox.Text = ""
            cPictureManager.SelectToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub


    Public Sub Open()
        Try
            OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                     "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                      "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
            OpenFileDialog_Path.RestoreDirectory = True
            OpenFileDialog_Path.FilterIndex = 1
            If OpenFileDialog_Path.ShowDialog() = DialogResult.OK Then
                HmiTextBox_Function_Path.TextBox.Text = OpenFileDialog_Path.FileName
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenPictureListForm.ToString))
        End Try
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Key.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiTextBox_Function_Path.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper())
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenPictureListForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenPictureListForm.ToString)
        Me.Dispose()
        Return True
    End Function
    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub
End Class