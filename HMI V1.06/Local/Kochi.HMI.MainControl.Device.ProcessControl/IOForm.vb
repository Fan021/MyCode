Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Math
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI
Imports System.Drawing

Public Class IOForm
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private bExit As Boolean = False
    Private lListInitParameter As List(Of String)
    Private cThread As Thread
    Private mMainForm As IMainUI
    Public Const FormName As String = "ProcessControl"
    Protected cLanguageManager As clsLanguageManager
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceCfg As clsDeviceCfg
    Private _Object As New Object
    Private iFontSize As Integer = 10
    Private bReadOnly As Boolean
    Private cVariantManager As clsVariantManager
    Private bEdit As Boolean = False
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cObjectSource As clsProcessControl
    Private WithEvents HmiDateTime_Start As New Kochi.HMI.MainControl.UI.HMIDateTime
    Private WithEvents HmiDateTime_End As New Kochi.HMI.MainControl.UI.HMIDateTime '
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Public Property [ReadOnly] As Boolean
        Set(ByVal value As Boolean)
            bReadOnly = value
        End Set
        Get
            Return bReadOnly
        End Get
    End Property

    Public Property FontSize As Integer
        Set(ByVal value As Integer)
            iFontSize = value
        End Set
        Get
            Return iFontSize
        End Get
    End Property

    Public Property IO As clsProcessControl
        Set(ByVal value As clsProcessControl)
            cObjectSource = value
        End Set
        Get
            Return cObjectSource
        End Get
    End Property
    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.cLocalElement = cLocalElement
        Me.cSystemElement = cSystemElement
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        cErrorMessageManager = CType(cLocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
        cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cObjectSource.Name)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        GetPageMode()
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        cDataGridViewPage_Data = New clsDataGridViewPage
        cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
        cDataGridViewPage_Data.RowsPerPage = 12
        If Not cObjectSource.Enable Then
            Return True
        End If
        cObjectSource.cProcessSFCManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
        Return True
    End Function
    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub


    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
        End SyncLock
    End Sub

    Public Function InitControlText() As Boolean
        TabPage1.Text = cLanguageManager.GetUserTextLine("ProcessControl", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("ProcessControl", "TabPage2")
        TabPage1.Font = New System.Drawing.Font("Calibri", iFontSize)
        TabPage2.Font = New System.Drawing.Font("Calibri", iFontSize)

        MachineListView_ProcessStation.Rows.Clear()
        MachineListView_ProcessStation.Columns.Clear()
        MachineListView_ProcessStation.Font = New System.Drawing.Font("Calibri", iFontSize)
        Dim PostTest_id As New DataGridViewTextBoxColumn
        PostTest_id = New DataGridViewTextBoxColumn
        PostTest_id.HeaderText = cLanguageManager.GetUserTextLine("ProcessControl", "PostTest_id")
        PostTest_id.Name = "PostTest_id"
        PostTest_id.ReadOnly = True
        MachineListView_ProcessStation.Columns.Add(PostTest_id)

        Dim PostTest_Station As New DataGridViewTextBoxColumn
        PostTest_Station = New DataGridViewTextBoxColumn
        PostTest_Station.HeaderText = cLanguageManager.GetUserTextLine("ProcessControl", "PostTest_Station")
        PostTest_Station.Name = "PostTest_Station"
        PostTest_Station.ReadOnly = IIf(ePageMode = enumPageMode.Debug, True, False)
        MachineListView_ProcessStation.Columns.Add(PostTest_Station)

        Dim PostTest_PassStation As New DataGridViewTextBoxColumn
        PostTest_PassStation = New DataGridViewTextBoxColumn
        PostTest_PassStation.HeaderText = cLanguageManager.GetUserTextLine("ProcessControl", "PostTest_PassStation")
        PostTest_PassStation.Name = "PostTest_PassStation"
        PostTest_PassStation.ReadOnly = IIf(ePageMode = enumPageMode.Debug, True, False)
        MachineListView_ProcessStation.Columns.Add(PostTest_PassStation)

        Dim PostTest_FailStation As New DataGridViewTextBoxColumn
        PostTest_FailStation = New DataGridViewTextBoxColumn
        PostTest_FailStation.HeaderText = cLanguageManager.GetUserTextLine("ProcessControl", "PostTest_FailStation")
        PostTest_FailStation.Name = "PostTest_FailStation"
        PostTest_FailStation.ReadOnly = IIf(ePageMode = enumPageMode.Debug, True, False)
        MachineListView_ProcessStation.Columns.Add(PostTest_FailStation)
        HmiDataView_Data.ReadOnly = True
        GroupBox_Search.Text = cLanguageManager.GetUserTextLine("ProcessControl", "GroupBox_Search")
        GroupBox_Search.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_StartDate.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_StartDate")
        HmiLabel_StartDate.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_EndDate.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_EndDate")
        HmiLabel_EndDate.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiLabel_SFC.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_SFC")
        HmiLabel_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Station.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Station")
        HmiLabel_Station.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Station.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_ID.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Function_ID")
        HmiLabel_Function_ID.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Function_ID.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiLabel_Function_SFC.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Function_SFC")
        HmiLabel_Function_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Function_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_Station.Label.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiLabel_Function_Station")
        HmiLabel_Function_Station.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Station.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiButton_Search.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Search")
        HmiButton_Search.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Search.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Search")
        HmiButton_Search.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Cancel.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        GroupBox_Function.Text = cLanguageManager.GetUserTextLine("ProcessControl", "GroupBox_Function")
        GroupBox_Function.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Add.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Add")
        HmiButton_Add.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Modify.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Modify")
        HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Del.Button.Text = cLanguageManager.GetUserTextLine("ProcessControl", "HmiButton_Del")
        HmiButton_Del.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiDataView_Data.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiDataViewPage_Data.Label_TotalPage.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.Label_Total.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.HmiTextBox_Page.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiComboBox_Station.ComboBox.DropDownStyle = ComboBoxStyle.DropDown
        HmiComboBox_Function_Combox.ComboBox.DropDownStyle = ComboBoxStyle.DropDown

        Me.HmiDateTime_Start.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_Start.Location = New System.Drawing.Point(45, 3)
        Me.HmiDateTime_Start.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_Start.Name = "HmiDateTime_Start"
        Me.HmiDateTime_Start.Size = New System.Drawing.Size(91, 33)
        '
        'HmiDateTime_End
        '
        Me.HmiDateTime_End.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HmiDateTime_End.Location = New System.Drawing.Point(45, 3)
        Me.HmiDateTime_End.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.HmiDateTime_End.Name = "HmiDateTime_Start"
        Me.HmiDateTime_End.Size = New System.Drawing.Size(91, 33)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiDateTime_Start, 1, 0)
        Me.HmiTableLayoutPanel_Head.Controls.Add(Me.HmiDateTime_End, 3, 0)

        PostTest_Del.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        PostTest_Add.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiButton_Add.Button.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiButton_Del.Button.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiButton_Modify.Button.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiComboBox_Function_Combox.ComboBox.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)

        HmiTextBox_Function_ID.TextBoxReadOnly = True
        AddHandler PostTest_Add.Click, AddressOf PostTest_Add_Click
        AddHandler PostTest_Del.Click, AddressOf PostTest_Del_Click
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Add.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Del.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Modify.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiTextBox_SFC.TextBox.SizeChanged, AddressOf TextBox_SizeChanged

        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            HmiTableLayoutPanel_Mid.RowStyles(0).Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 2 + HmiTextBox_SFC.TextBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 2 + HmiTextBox_SFC.TextBox.Height + 6
            For Each element As RowStyle In HmiTableLayoutPanel_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_SFC.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Private Sub HmiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_Search"
                    Search()

                Case "HmiButton_Cancel"
                    Cancel()
                Case "HmiButton_Add"
                    Add()
                Case "HmiButton_Modify"
                    Modify()
                Case "HmiButton_Del"
                    Delete()


            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub



    Public Sub Search()
        Try
            If Not cObjectSource.Enable Then
                Return
            End If
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cObjectSource.cProcessSFCManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_Station.ComboBox.Text
                                                                           )
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            If Not cObjectSource.Enable Then
                Return
            End If
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiTextBox_SFC.TextBox.Text = ""
                                       HmiComboBox_Station.ComboBox.SelectedIndex = -1
                                       HmiTextBox_Function_ID.TextBox.Text = ""
                                       HmiTextBox_Function_SFC.TextBox.Text = ""
                                       HmiComboBox_Function_Combox.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cObjectSource.cProcessSFCManager.SelectToDataView(enumViewPageType.FirstPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_SFC.TextBox.Text,
                                                     HmiComboBox_Station.ComboBox.Text
                                         )
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub


    Public Sub Add()
        Try
            If Not cObjectSource.Enable Then
                Return
            End If
            HmiTextBox_SFC.TextBox.Text = ""
            HmiComboBox_Station.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_SFC.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "9"), enumExceptionType.Alarm, FormName))
                Return
            End If

            If cObjectSource.cProcessSFCManager.HasSFC(HmiTextBox_Function_SFC.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "7", HmiTextBox_Function_SFC.Text), enumExceptionType.Alarm, FormName))
                Return
            End If

            cObjectSource.cProcessSFCManager.InSertData(HmiTextBox_Function_SFC.Text, HmiComboBox_Function_Combox.ComboBox.Text)
            cObjectSource.cProcessSFCManager.SelectToDataView(enumViewPageType.FirstPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_SFC.TextBox.Text,
                                                     HmiComboBox_Station.ComboBox.Text
                                         )
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Public Sub Delete()
        Try
            If Not cObjectSource.Enable Then
                Return
            End If
            HmiTextBox_SFC.TextBox.Text = ""
            HmiComboBox_Station.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "6"), enumExceptionType.Alarm, FormName))
                Return
            End If

            cObjectSource.cProcessSFCManager.DeleteData(HmiTextBox_Function_ID.Text)
            cObjectSource.cProcessSFCManager.SelectToDataView(enumViewPageType.LastPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_SFC.TextBox.Text,
                                                     HmiComboBox_Station.ComboBox.Text
                                         )
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Modify()
        Try
            If Not cObjectSource.Enable Then
                Return
            End If
            HmiTextBox_SFC.TextBox.Text = ""
            HmiComboBox_Station.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "6"), enumExceptionType.Alarm, FormName))
                Return
            End If

            If Not cObjectSource.cProcessSFCManager.HasSFC(HmiTextBox_Function_SFC.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "8", HmiTextBox_Function_SFC.Text), enumExceptionType.Alarm, FormName))
                Return
            End If

            cObjectSource.cProcessSFCManager.UpdateData(HmiTextBox_Function_SFC.Text, HmiComboBox_Function_Combox.ComboBox.Text)
            cObjectSource.cProcessSFCManager.SelectToDataView(enumViewPageType.NoPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_SFC.TextBox.Text,
                                                     HmiComboBox_Station.ComboBox.Text
                                         )
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, FormName))
        End Try
    End Sub


    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_SFC.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiComboBox_Function_Combox.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
        End If
    End Sub

    Private Sub MachineListView_ProcessStation_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If Not cObjectSource.Enable Then
            Return
        End If
        SaveData()
    End Sub

    Private Sub PostTest_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not cObjectSource.Enable Then
            Return
        End If
        MachineListView_ProcessStation.Rows.Add((MachineListView_ProcessStation.Rows.Count + 1).ToString, "", "")
        SaveData()
    End Sub

    Private Sub PostTest_Del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not cObjectSource.Enable Then
            Return
        End If
        If MachineListView_ProcessStation.Rows.Count <= 0 Then Return
        MachineListView_ProcessStation.Rows.Remove(MachineListView_ProcessStation.CurrentRow)
        For Each t As DataGridViewRow In MachineListView_ProcessStation.Rows
            t.Cells(0).Value = (t.Index + 1).ToString
        Next
        SaveData()

    End Sub
    Private Sub SaveData()
        If Not cObjectSource.Enable Then
            Return
        End If
        SyncLock _Object
            cObjectSource.cProcessStationManager.lListProcessCfg.Clear()

            For i = 0 To MachineListView_ProcessStation.Rows.Count - 1
                Dim cProcessCfg As New clsProcessStationCfg
                cProcessCfg.StationName = MachineListView_ProcessStation.Rows(i).Cells(1).Value
                cProcessCfg.PassStationName = MachineListView_ProcessStation.Rows(i).Cells(2).Value
                cProcessCfg.FailureStationName = MachineListView_ProcessStation.Rows(i).Cells(3).Value
                cObjectSource.cProcessStationManager.lListProcessCfg.Add(i.ToString, cProcessCfg)
            Next
            cObjectSource.cProcessStationManager.SaveData()
        End SyncLock
    End Sub

    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Return True
    End Function

    Private Sub RefreshUI()
        Return
    End Sub


    Public Function StartRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = False
        RemoveHandler MachineListView_ProcessStation.CellValueChanged, AddressOf MachineListView_ProcessStation_CellValueChanged
        MachineListView_ProcessStation.Rows.Clear()
        If cObjectSource.Enable Then cObjectSource.cProcessStationManager.LoadData()
        HmiComboBox_Station.ComboBox.Items.Clear()
        HmiComboBox_Function_Combox.ComboBox.Items.Clear()
        If cObjectSource.Enable Then
            For Each element As clsProcessStationCfg In cObjectSource.cProcessStationManager.lListProcessCfg.Values
                MachineListView_ProcessStation.Rows.Add((MachineListView_ProcessStation.Rows.Count + 1).ToString, element.StationName, element.PassStationName, element.FailureStationName)
                HmiComboBox_Station.ComboBox.Items.Add(element.StationName)
                HmiComboBox_Function_Combox.ComboBox.Items.Add(element.StationName)
            Next
        End If
        HmiComboBox_Station.ComboBox.Items.Add("Repair")
        HmiComboBox_Function_Combox.ComboBox.Items.Add("Repair")
        AddHandler MachineListView_ProcessStation.CellValueChanged, AddressOf MachineListView_ProcessStation_CellValueChanged
        Search()
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
        RemoveHandler MachineListView_ProcessStation.CellValueChanged, AddressOf MachineListView_ProcessStation_CellValueChanged
        Dim iCnt As Integer = 100
        Do While iCnt > 0
            If IsNothing(cThread) Then
                Exit Do
            End If
            If cThread.ThreadState = ThreadState.Stopped Or cThread.ThreadState = ThreadState.Unstarted Then
                Exit Do
            End If
            iCnt = iCnt - 1
            System.Threading.Thread.Sleep(1)
        Loop
        If Not IsNothing(cThread) Then cThread.Abort()
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        Me.Dispose()
        Return True
    End Function

End Class

