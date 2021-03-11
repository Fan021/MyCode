Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Threading

Public Class ChildrenActionForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cActionDataManager As clsActionDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cFormFontResize As clsFormFontResize
    Private cMachineManager As clsMachineManager
    Private cVariantManager As clsVariantManager
    Private cCsvHandler As New clsCsvHandler
    Private cLanguageManager As clsLanguageManager
    Private strButtonName As String
    Private cThread As Thread
    Private mMainForm As MainForm
    Private cUserManager As clsUserManager
    Public Property ButtonName As String Implements IChildrenUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property
    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IChildrenUI.UI
        Get
            Return Panel_Body
        End Get
    End Property

    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Init
        Try
            Me.cSystemElement = cSystemElement
            Me.cLocalElement = cLocalElement
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDataGridViewPage_Data = New clsDataGridViewPage
            cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage_Data.RowsPerPage = 12
            cActionDataManager = New clsActionDataManager
            cActionDataManager.Init(cSystemElement)
            cActionDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenActionForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        HmiComboBox_Station.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            HmiComboBox_Station.ComboBox.Items.Add(element.ID.ToString)
        Next
        HmiComboBox_Result.ComboBox.Items.Clear()
        HmiComboBox_Result.ComboBox.Items.Add("PASS")
        HmiComboBox_Result.ComboBox.Items.Add("FAIL")

        HmiComboBox_Variant.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
        Next

        HmiComboBox_ActionId.ComboBox.Items.Clear()
        For i = 1 To 100
            HmiComboBox_ActionId.ComboBox.Items.Add(i.ToString)
        Next
        HmiComboBox_Stage.ComboBox.Items.Clear()
        For Each eType As enumActionType In [Enum].GetValues(GetType(enumActionType))
            If eType = enumUserLevel.Normal Then Continue For
            HmiComboBox_Stage.ComboBox.Items.Add(eType.ToString)
        Next
        Return True

    End Function
    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_EndDate")
        HmiLabel_SFC.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_SFC")
        HmiLabel_Variant.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_Variant")
        HmiLabel_Station.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_Station")
        HmiLabel_Stage.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_Stage")
        HmiLabel_ActionId.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_ActionId")
        HmiLabel_ActionName.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_ActionName")
        HmiLabel_Result.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiLabel_Result")
        HmiButton_Export.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenActionForm.ToString, "HmiButton_Export")
        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"


        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiComboBox_Station.ComboBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Export.Button.Click, AddressOf HmiButton_Click
        AddHandler ContextMenuStrip_Function.Click, AddressOf HmiButton_Click
        cThread = New Thread(AddressOf Search)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiComboBox_Station.ComboBox.Height + 6 + 6) * 4 + HmiComboBox_Station.ComboBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiComboBox_Station.ComboBox.Height + 6 + 6) * 4 + HmiComboBox_Station.ComboBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiComboBox_Station.ComboBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub

    Private Sub HmiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_Search"
                    cThread = New Thread(AddressOf Search)
                    cThread.IsBackground = True
                    cThread.Start()

                Case "HmiButton_Cancel"
                    cThread = New Thread(AddressOf Cancel)
                    cThread.IsBackground = True
                    cThread.Start()

                Case "HmiButton_Export"
                    cThread = New Thread(AddressOf Export)
                    cThread.IsBackground = True
                    cThread.Start()

                Case "ContextMenuStrip_Function"
                    cThread = New Thread(AddressOf Delete)
                    cThread.IsBackground = True
                    cThread.Start()

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cActionDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiComboBox_Station.ComboBox.Text,
                                                                               HmiComboBox_Variant.ComboBox.Text,
                                                                               HmiComboBox_Result.ComboBox.Text,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_Stage.ComboBox.Text,
                                                                               HmiComboBox_ActionId.ComboBox.Text,
                                                                               HmiTextBox_ActionName.TextBox.Text
                                                                               )
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiTextBox_SFC.TextBox.Text = ""
                                       HmiTextBox_ActionName.TextBox.Text = ""
                                       HmiComboBox_Variant.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Station.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Result.ComboBox.SelectedIndex = -1
                                       HmiComboBox_ActionId.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Stage.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cActionDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiComboBox_Station.ComboBox.Text,
                                                                               HmiComboBox_Variant.ComboBox.Text,
                                                                               HmiComboBox_Result.ComboBox.Text,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_Stage.ComboBox.Text,
                                                                               HmiComboBox_ActionId.ComboBox.Text,
                                                                               HmiTextBox_ActionName.TextBox.Text
                                                                               )
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub


    Public Sub Delete()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       If Not IsNothing(HmiDataView_Data.CurrentRow) AndAlso HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
                                           cActionDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cActionDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiComboBox_Station.ComboBox.Text,
                                                                               HmiComboBox_Variant.ComboBox.Text,
                                                                               HmiComboBox_Result.ComboBox.Text,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_Stage.ComboBox.Text,
                                                                               HmiComboBox_ActionId.ComboBox.Text,
                                                                               HmiTextBox_ActionName.TextBox.Text
                                                                               )
                                       End If
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub


    Public Sub Export()
        mMainForm.InvokeAction(Sub()
                                   SaveFileDialogcsv.Filter = "*.csv|*.csv"
                                   If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
                                       cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cActionDataManager.Ds_Data)
                                   End If
                               End Sub)

    End Sub


    Private Sub HmiDataView_Data_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        Try
            If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then
                Return
            End If
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim rc As Rectangle, i As Int32 = 0

                For i = 0 To e.ColumnIndex - 1
                    rc.X += HmiDataView_Data.Columns(i).Width
                Next
                rc.Width = HmiDataView_Data.Columns(0).Width

                For i = 0 To e.RowIndex
                    rc.Y += HmiDataView_Data.Rows(i).Height
                Next
                rc.Height = HmiDataView_Data.Rows(0).Height
                ContextMenuStrip_Function.Show(HmiDataView_Data, New Point(rc.X, rc.Y + rc.Height))

            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenActionForm.ToString))
        End Try
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenActionForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenActionForm.ToString)
        Me.Dispose()
        Return True
    End Function

End Class