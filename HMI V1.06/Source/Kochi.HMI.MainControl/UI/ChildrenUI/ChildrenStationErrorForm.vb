Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenStationErrorForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private lListElement As New Dictionary(Of String, Object)
    Private cMachineManager As clsMachineManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
    Private cUserManager As clsUserManager
    Private cStationErrorCodeManager As clsStationErrorCodeManager
    Private cErrorCodeManager As clsErrorCodeManager
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
            cStationErrorCodeManager = CType(cSystemElement(clsStationErrorCodeManager.Name), clsStationErrorCodeManager)
            cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cErrorCodeManager = CType(cSystemElement(clsErrorCodeManager.Name), clsErrorCodeManager)
            cDataGridViewPage = New clsDataGridViewPage
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cStationErrorCodeManager.RegisterManager_Station(cDataGridViewPage, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenStationErrorForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenStationErrorForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        cStationErrorCodeManager.SelectStationErrorToDataView(enumViewPageType.FirstPage)
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_StationID.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiLabel_StationID")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_StationID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiLabel_Function_StationID")
        HmiLabel_Function_Enable.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiLabel_Function_Enable")
        HmiLabel_Function_ErrorCode.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiLabel_Function_ErrorCode")
        HmiLabel_Function_ExpectNumber.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiLabel_Function_ExpectNumber")


        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Reset.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Function_Reset")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "HmiButton_Cancel")

        HmiTextBox_Function_ExpectNumber.ValueType = GetType(Integer)
        HmiTextBox_Function_StationID.TextBoxReadOnly = True

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Administrator Then
            HmiButton_Function_Modify.Enabled = False
        Else
            HmiButton_Function_Modify.Enabled = True
        End If

        HmiComboBox_StationID.ComboBox.Items.Clear()
        For Each element In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(element)
            HmiComboBox_StationID.ComboBox.Items.Add(cMachineStationCfg.ID.ToString)
        Next
        HmiComboBox_Function_Enable.ComboBox.Items.Clear()
        HmiComboBox_Function_Enable.ComboBox.Items.Add("TRUE")
        HmiComboBox_Function_Enable.ComboBox.Items.Add("FALSE")

        HmiComboBox_Function_ErrorCode.ComboBox.Items.Clear()
        For Each element In cErrorCodeManager.GetErrorCodeListKey
            Dim cErrorCodeCfg As clsErrorCodeCfg = cErrorCodeManager.GetErrorCodeCfgFromKey(element)
            HmiComboBox_Function_ErrorCode.ComboBox.Items.Add(cErrorCodeCfg.Key.ToString)
        Next

        AddHandler HmiTextBox_Function_StationID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Modify.Button.Click, AddressOf HmiButton_Function_Click
        AddHandler HmiButton_Function_Reset.Button.Click, AddressOf HmiButton_Function_Click
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Function_StationID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Function_StationID.TextBox.Height + 6
            GroupBox_Search.Height = (HmiTextBox_Function_StationID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Function_StationID.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Function_StationID.TextBox.Height + 6 + 6
            Next
            For Each element As RowStyle In TableLayoutPanel_Body_Left_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Function_StationID.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub

    Private Sub HmiButton_Function_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_Function_Reset"
                    ResetNumber()
                Case "HmiButton_Function_Modify"
                    Modify()
                Case "HmiButton_Search"
                    Search()
                Case "HmiButton_Cancel"
                    Cancel()
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1

            If HmiTextBox_Function_StationID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
                Return
            End If
            If HmiComboBox_Function_ErrorCode.ComboBox.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
                Return
            End If
            If HmiComboBox_Function_Enable.ComboBox.Text = "TRUE" And HmiTextBox_Function_ExpectNumber.TextBox.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
                Return
            End If

            cStationErrorCodeManager.ModifyStationErrorData(HmiTextBox_Function_StationID.Text, HmiComboBox_Function_Enable.ComboBox.Text, HmiComboBox_Function_ErrorCode.ComboBox.Text, HmiTextBox_Function_ExpectNumber.TextBox.Text)
            cStationErrorCodeManager.SelectStationErrorToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub

    Public Sub ResetNumber()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1

            If HmiTextBox_Function_StationID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenStationErrorForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
                Return
            End If

            cStationErrorCodeManager.ResetStationErrorData(HmiTextBox_Function_StationID.Text)
            cStationErrorCodeManager.SelectStationErrorToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub



    Public Sub Search()
        Try
            cStationErrorCodeManager.SelectStationErrorToDataView(enumViewPageType.FirstPage, HmiComboBox_StationID.ComboBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1
            cStationErrorCodeManager.SelectStationErrorToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
        End Try
    End Sub


    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_StationID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiComboBox_Function_Enable.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiComboBox_Function_ErrorCode.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(3).Value
            HmiTextBox_Function_ExpectNumber.TextBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
        End If
    End Sub


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenStationErrorForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenStationErrorForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class