Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.IO

Public Class ChildrenVariantForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private lListElement As New Dictionary(Of String, Object)
    Private cMachineManager As clsMachineManager
    Private cVariantManager As clsVariantManager
    Private cGlobalProgramManager As clsGlobalProgramManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private cSystemManager As clsSystemManager
    Private strButtonName As String
    Private _Object As New Object
    Private lColumnListValue As New Dictionary(Of String, clsListValueCfg)
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
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cGlobalProgramManager = CType(cSystemElement(clsGlobalProgramManager.Name), clsGlobalProgramManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cVariantManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenVariantForm.ToString, Me)
            Timer_Show.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenVariantForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        cVariantManager.SelectToDataView(enumViewPageType.FirstPage)
        '   HmiDataView_Data.strFolder = cSystemManager.Settings.PictureFolder
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_ID.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_ID")
        HmiLabel_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Variant")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_ID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_ID")
        HmiLabel_Function_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Variant")
        HmiLabel_Function_GlobalProgram.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_GlobalProgram")

        HmiLabel_Function_Pic.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Pic")
        HmiButton_Function_Pic.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Function_Pic")
        HmiLabel_Function_Program.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Program")
        HmiButton_Function_Program.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Function_Program")

        HmiButton_Function_Add.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Function_Add")
        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Del.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiButton_Function_Del")

        HmiTextBox_Function_Description.Multiline = True
        HmiTextBox_Function_Description.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Description.WordWrap = False
        HmiTextBox_Function_Description2.Multiline = True
        HmiTextBox_Function_Description2.ScrollBars = ScrollBars.Both
        HmiTextBox_Function_Description2.WordWrap = False
        HmiTextBox_Function_ID.TextBoxReadOnly = True
        HmiTextBox_Function_Pic.TextBoxReadOnly = True
        HmiTextBox_Function_Program.TextBoxReadOnly = True
        HmiTextBox_Function_Description.Font = New System.Drawing.Font("Calibri", 12.0!)
        HmiTextBox_Function_Description2.Font = New System.Drawing.Font("Calibri", 12.0!)

        HmiLabel_Function_ID.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_ID.TextBox.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Variant.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_Variant.TextBox.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Description.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Description.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Description2.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Description2.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Pic.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_Pic.TextBox.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiLabel_Function_Program.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_Program.TextBox.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiButton_Function_Program.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiButton_Function_Add.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiButton_Function_Modify.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiButton_Function_Del.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiButton_Function_Pic.Button.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_Description.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiTextBox_Function_Description2.Font = New System.Drawing.Font("Calibri", 9.0!)

        HmiLabel_Function_GlobalProgram.Label.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiComboBox_Function_GlobalProgram.ComboBox.Font = New System.Drawing.Font("Calibri", 9.0!)
        HmiComboBox_Function_GlobalProgram.ComboBox.Font = New System.Drawing.Font("Calibri", 9.0!)

        If cLanguageManager.SecondLanguageEnable Then
            HmiLabel_Function_Description.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Description", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage))
            HmiLabel_Function_Description2.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Description2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage))
            HmiLabel_Function_Description.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiLabel_Function_Description2.Label.TextAlign = ContentAlignment.MiddleLeft
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Else
            HmiLabel_Function_Description.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "HmiLabel_Function_Description3")
            HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End If

        HmiComboBox_Function_GlobalProgram.ComboBox.Items.Clear()
        For Each element In cGlobalProgramManager.GetGlobalProgramListKey
            Dim cGlobalProgramCfg As clsGlobalProgramCfg = cGlobalProgramManager.GetGlobalProgramCfgFromKey(element)
            HmiComboBox_Function_GlobalProgram.ComboBox.Items.Add(cGlobalProgramCfg.GlobalProgram)
        Next
        Dim cListValueCfg As New clsListValueCfg
        Dim tempListObject As New List(Of Object)
        lColumnListValue.Clear()

        tempListObject = New List(Of Object)
        cListValueCfg = New clsListValueCfg
        tempListObject.Add("TRUE")
        tempListObject.Add("FALSE")
        cListValueCfg.ListValue = tempListObject
        lColumnListValue.Add("Single Header", cListValueCfg)

        tempListObject = New List(Of Object)
        cListValueCfg = New clsListValueCfg
        tempListObject.Add("TRUE")
        tempListObject.Add("FALSE")
        cListValueCfg.ListValue = tempListObject
        lColumnListValue.Add("Double Header", cListValueCfg)
        HmiDataView_Data.lColumnListValue = lColumnListValue


        HmiDataView_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        AddHandler HmiTextBox_ID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_Function_ID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged2
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Pic.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Program.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Add.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Del.Button.Click, AddressOf HmiButton_Function_Click
        'AddHandler HmiTextBox_Function_Description.Enter, AddressOf HmiTextBox_Function_Enter
        ' AddHandler HmiTextBox_Function_Description2.Enter, AddressOf HmiTextBox_Function_Enter
        'AddHandler HmiTextBox_Function_Description.Leave, AddressOf HmiTextBox_Function_Leave
        'AddHandler HmiTextBox_Function_Description2.Leave, AddressOf HmiTextBox_Function_Leave
        AddHandler HmiDataView_Data.CellValueChanged, AddressOf HmiDataView_Data_CellValueChanged
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_ID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_ID.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_ID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_ID.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_ID.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub

    Private Sub TextBox_SizeChanged2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim iCnt As Integer = 0
            For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                If iCnt Mod 2 = 0 And iCnt < 10 Then
                    element.Height = HmiTextBox_Function_ID.TextBox.Height
                ElseIf iCnt < 10 Then
                    element.Height = HmiTextBox_Function_ID.TextBox.Height + 1
                Else
                    element.Height = HmiTextBox_Function_ID.TextBox.Height + 3 + 2
                End If
                If cLanguageManager.SecondLanguageEnable Then
                    If iCnt = 4 Or iCnt = 6 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Function_ID.TextBox.Height + 6 + 6 + 6
                    End If
                    If iCnt = 5 Or iCnt = 7 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Function_ID.TextBox.Height + 6 + 6
                    End If
                Else
                    If iCnt = 5 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = HmiTextBox_Function_ID.TextBox.Height + 6 + 6 + 40
                    End If
                    If iCnt = 6 Or iCnt = 7 Then
                        element.SizeType = System.Windows.Forms.SizeType.Absolute
                        element.Height = 0
                    End If
                End If
                iCnt = iCnt + 1
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub

    Private Sub HmiButton_Function_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                Select Case sender.name
                    Case "HmiButton_Function_Pic"
                        Open()
                    Case "HmiButton_Function_Program"
                        Program()
                    Case "HmiButton_Function_Add"
                        Add()
                    Case "HmiButton_Function_Modify"
                        HmiButton_Function_Modify.Enabled = False
                        Modify()
                        HmiButton_Function_Modify.Enabled = True
                    Case "HmiButton_Function_Del"
                        Delete()
                    Case "HmiButton_Search"
                        Search()
                    Case "HmiButton_Cancel"
                        Cancel()
                End Select
            Catch ex As Exception
                HmiButton_Function_Modify.Enabled = True
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
            End Try
        End SyncLock
    End Sub

    Public Sub Add()
        Try
            'If HmiTextBox_Function_ID.Text = "" Then
            '    cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "1"), enumExceptionType.Alarm))
            '    Return
            'End If
            HmiTextBox_ID.TextBox.Text = ""
            HmiTextBox_Variant.TextBox.Text = ""
            If HmiTextBox_Function_Variant.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If

            If cVariantManager.HasVariant(HmiTextBox_Function_Variant.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "3", HmiTextBox_Function_Variant.Text), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If
            lListElement.Clear()
            For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                lListElement.Add(mKey, "")
            Next

            cVariantManager.InSertData((cVariantManager.GetVariantListKey.Count + 1).ToString, HmiTextBox_Function_Variant.Text, HmiTextBox_Function_Description.Text, HmiTextBox_Function_Description2.Text, HmiTextBox_Function_Pic.TextBox.Text, HmiTextBox_Function_Program.TextBox.Text, HmiComboBox_Function_GlobalProgram.ComboBox.Text, lListElement)
            cVariantManager.LoadVariantCfg()
            cVariantManager.SelectToDataView(enumViewPageType.LastPage)
            cMachineManager.LoadDeviceData()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_ID.TextBox.Text = ""
            HmiTextBox_Variant.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If

            If HmiTextBox_Function_Variant.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If

            If cVariantManager.HasVariant(HmiTextBox_Function_Variant.Text, HmiTextBox_Function_ID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "3", HmiTextBox_Function_Variant.Text), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If
            lListElement.Clear()
            For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                If Not IsNothing(HmiDataView_Data.CurrentCell) Then
                    lListElement.Add(mKey, "")
                    For i = 0 To HmiDataView_Data.Columns.Count - 1
                        If HmiDataView_Data.Columns(i).HeaderText = mKey Then
                            lListElement(mKey) = HmiDataView_Data.Rows(HmiDataView_Data.CurrentCell.RowIndex).Cells(i).Value.ToString
                        End If
                    Next
                Else
                    lListElement.Add(mKey, "")
                End If
            Next
            cVariantManager.ModifyData(HmiTextBox_Function_ID.Text, HmiTextBox_Function_Variant.Text, HmiTextBox_Function_Description.Text, HmiTextBox_Function_Description2.Text, HmiTextBox_Function_Pic.TextBox.Text, HmiTextBox_Function_Program.TextBox.Text, HmiComboBox_Function_GlobalProgram.ComboBox.Text, lListElement)
            cVariantManager.SelectToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub
    Private Sub ValueChanged(ByVal HeaderText As String)
        SyncLock _Object

        End SyncLock

    End Sub

    Private Sub HmiDataView_Data_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        SyncLock _Object
            Try

                Dim isSave As Boolean = False
                If e Is Nothing Then Return
                Dim Obj As Object = HmiDataView_Data.CurrentRow.Cells(e.ColumnIndex).Value
                lListElement.Clear()
                For Each mKey As String In cMachineManager.VaiantElememtManager.ListElement
                    If Not IsNothing(e) Then
                        lListElement.Add(mKey, "")
                        For i = 0 To HmiDataView_Data.Columns.Count - 1
                            If HmiDataView_Data.Columns(i).HeaderText = mKey Then
                                lListElement(mKey) = HmiDataView_Data.Rows(e.RowIndex).Cells(i).Value.ToString
                            End If
                        Next
                        If HmiDataView_Data.Columns(e.ColumnIndex).HeaderText = mKey Then
                            isSave = True
                        End If
                    Else
                        lListElement.Add(mKey, "")
                    End If
                Next
                If isSave Then
                    If cLanguageManager.SecondLanguageEnable Then
                        cVariantManager.ModifyData(HmiDataView_Data.Rows(e.RowIndex).Cells(0).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(1).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(2).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(3).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(4).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(5).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(6).Value, lListElement)
                    Else
                        cVariantManager.ModifyData(HmiDataView_Data.Rows(e.RowIndex).Cells(0).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(1).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(2).Value, "", HmiDataView_Data.Rows(e.RowIndex).Cells(3).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(4).Value, HmiDataView_Data.Rows(e.RowIndex).Cells(5).Value, lListElement)
                    End If

                End If
            Catch ex As Exception

                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenSystemParameterForm.ToString))
            End Try
        End SyncLock
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_ID.TextBox.Text = ""
            HmiTextBox_Variant.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenVariantForm.ToString, "1"), enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
                Return
            End If
            cVariantManager.DeleteData(HmiTextBox_Function_ID.Text)
            cVariantManager.SelectToDataView(enumViewPageType.NoPage)
            HmiTextBox_Function_ID.Text = ""
            HmiTextBox_Function_Variant.Text = ""
            HmiTextBox_Function_Description.Text = ""
            HmiTextBox_Function_Description2.Text = ""
            HmiTextBox_Function_Pic.Text = ""
            HmiTextBox_Function_Program.Text = ""
            HmiComboBox_Function_GlobalProgram.ComboBox.SelectedIndex = -1
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            cVariantManager.SelectToDataView(enumViewPageType.FirstPage, HmiTextBox_ID.Text, HmiTextBox_Variant.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiTextBox_ID.TextBox.Text = ""
            HmiTextBox_Variant.TextBox.Text = ""
            cVariantManager.SelectToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub

    Public Sub Open()
        Try
            OpenFileDialog_Path.Filter = "All Image Formats (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                             "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                              "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif"
            OpenFileDialog_Path.RestoreDirectory = True
            OpenFileDialog_Path.FilterIndex = 1
            OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.PictureFolder
            Dim cDialogResult As DialogResult = OpenFileDialog_Path.ShowDialog()
            If cDialogResult = DialogResult.OK Then
                HmiTextBox_Function_Pic.TextBox.Text = OpenFileDialog_Path.FileName
            ElseIf cDialogResult = DialogResult.Cancel Then
                HmiTextBox_Function_Pic.TextBox.Text = ""
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub


    Public Sub Program()
        Try
            OpenFileDialog_Path.Filter = "*.ini|*.ini"
            OpenFileDialog_Path.RestoreDirectory = True
            OpenFileDialog_Path.FilterIndex = 1
            OpenFileDialog_Path.InitialDirectory = cSystemManager.Settings.VariantFolder
            Dim cDialogResult As DialogResult = OpenFileDialog_Path.ShowDialog()
            If cDialogResult = DialogResult.OK Then
                HmiTextBox_Function_Program.TextBox.Text = OpenFileDialog_Path.FileName
            ElseIf cDialogResult = DialogResult.Cancel Then
                HmiTextBox_Function_Program.TextBox.Text = ""
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenVariantForm.ToString))
        End Try
    End Sub


    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_Variant.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiTextBox_Function_Description.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
            If cLanguageManager.SecondLanguageEnable Then
                HmiTextBox_Function_Description2.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
                HmiTextBox_Function_Pic.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(4).Value
                HmiTextBox_Function_Program.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(5).Value
                HmiComboBox_Function_GlobalProgram.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(6).Value
            Else
                HmiTextBox_Function_Pic.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
                HmiTextBox_Function_Program.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(4).Value
                HmiComboBox_Function_GlobalProgram.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(5).Value
            End If
        End If
    End Sub


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenVariantForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenVariantForm.ToString)
        Me.Dispose()
        Return True
    End Function

    Private Sub HmiTextBox_Function_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Function_Description"
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 60
                Else
                    TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 100
                End If
            Case "HmiTextBox_Function_Description2"
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 60
                Else
                    TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 100
                End If
        End Select

    End Sub

    Private Sub HmiTextBox_Function_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiTextBox_Function_Description"
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_ID.TextBox.Height + 6 + 6
                Else
                    TableLayoutPanel_Body_Left_Function.RowStyles(5).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 40
                End If
            Case "HmiTextBox_Function_Description2"
                If cLanguageManager.SecondLanguageEnable Then
                    TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_ID.TextBox.Height + 6 + 6
                Else
                    TableLayoutPanel_Body_Left_Function.RowStyles(7).Height = HmiTextBox_ID.TextBox.Height + 6 + 6 + 40
                End If
        End Select
    End Sub

    Private Sub Timer_Show_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Show.Tick
        Timer_Show.Enabled = False
        HmiDataView_Data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    End Sub
End Class