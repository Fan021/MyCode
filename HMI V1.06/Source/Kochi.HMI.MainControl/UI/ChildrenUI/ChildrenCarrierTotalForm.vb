Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class ChildrenCarrierTotalForm
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
            cDataGridViewPage.RegisterManager(MachineListView_Data_Carrier, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cStationErrorCodeManager.RegisterManager_StationCarrier(cDataGridViewPage, MachineListView_Data_Carrier)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenCarrierTotalForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenCarrierTotalForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        cStationErrorCodeManager.SelectStationCarrierErrorToDataView(enumViewPageType.FirstPage)
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiLabel_StationID.Label.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiLabel_StationID")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Cancel")
        HmiLabel_Function_StationID.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiLabel_Function_StationID")
        HmiLabel_Function_Enable.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiLabel_Function_Enable")
        HmiLabel_Function_ErrorCode.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiLabel_Function_ErrorCode")
        HmiLabel_Function_ExpectNumber.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiLabel_Function_ExpectNumber")


        HmiButton_Function_Modify.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Function_Modify")
        HmiButton_Function_Reset.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Function_Reset")
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "HmiButton_Cancel")

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
        AddHandler MachineListView_Data_Carrier.CellClick, AddressOf HmiDataView_Data_CellClick
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1

            If HmiTextBox_Function_StationID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
                Return
            End If
            If HmiComboBox_Function_ErrorCode.ComboBox.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "4"), enumExceptionType.Alarm, enumUIName.ChildrenStationErrorForm.ToString))
                Return
            End If
            If HmiComboBox_Function_Enable.ComboBox.Text = "TRUE" And HmiTextBox_Function_ExpectNumber.TextBox.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "3"), enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
                Return
            End If

            cStationErrorCodeManager.ModifyCarrierStationErrorData(HmiTextBox_Function_StationID.Text, HmiComboBox_Function_Enable.ComboBox.Text, HmiComboBox_Function_ErrorCode.ComboBox.Text, HmiTextBox_Function_ExpectNumber.TextBox.Text)
            cStationErrorCodeManager.SelectStationCarrierErrorToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
        End Try
    End Sub

    Public Sub ResetNumber()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1

            If HmiTextBox_Function_StationID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenCarrierTotalForm.ToString, "2"), enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
                Return
            End If

            cStationErrorCodeManager.ResetCarrierStationErrorData(HmiTextBox_Function_StationID.Text)
            cStationErrorCodeManager.SelectStationCarrierErrorToDataView(enumViewPageType.NoPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
        End Try
    End Sub



    Public Sub Search()
        Try
            cStationErrorCodeManager.SelectStationCarrierErrorToDataView(enumViewPageType.FirstPage, HmiComboBox_StationID.ComboBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            HmiComboBox_StationID.ComboBox.SelectedIndex = -1
            cStationErrorCodeManager.SelectStationCarrierErrorToDataView(enumViewPageType.FirstPage)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenCarrierTotalForm.ToString))
        End Try
    End Sub


    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If IsNothing(MachineListView_Data_Carrier.CurrentRow) Then Return
        If MachineListView_Data_Carrier.CurrentRow.Index <= MachineListView_Data_Carrier.Rows.Count - 1 Then
            HmiTextBox_Function_StationID.Text = MachineListView_Data_Carrier.Rows(MachineListView_Data_Carrier.CurrentRow.Index).Cells(0).Value
            HmiComboBox_Function_Enable.ComboBox.Text = MachineListView_Data_Carrier.Rows(MachineListView_Data_Carrier.CurrentRow.Index).Cells(1).Value
            HmiComboBox_Function_ErrorCode.ComboBox.Text = MachineListView_Data_Carrier.Rows(MachineListView_Data_Carrier.CurrentRow.Index).Cells(3).Value
            HmiTextBox_Function_ExpectNumber.TextBox.Text = MachineListView_Data_Carrier.Rows(MachineListView_Data_Carrier.CurrentRow.Index).Cells(2).Value
        End If
    End Sub


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenCarrierTotalForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenCarrierTotalForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class