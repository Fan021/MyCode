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
    Public Const FormName As String = "StationCheck"
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
    Private cObjectSource As clsStationCheck
    Private WithEvents HmiDateTime_Start As New Kochi.HMI.MainControl.UI.HMIDateTime
    Private WithEvents HmiDateTime_End As New Kochi.HMI.MainControl.UI.HMIDateTime '
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cMachineManager As clsMachineManager
    Private cMachineStatusManager As clsMachineStatusManager
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

    Public Property IO As clsStationCheck
        Set(ByVal value As clsStationCheck)
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
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
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
        cObjectSource.cProductionData.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
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
        TabPage1.Text = cLanguageManager.GetUserTextLine("StationCheck", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("StationCheck", "TabPage2")
        TabPage1.Font = New System.Drawing.Font("Calibri", iFontSize)
        TabPage2.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_SFC.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_SFC")
        HmiLabel_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiLabel_Result.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Result")
        HmiLabel_Result.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Result.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_ResetStation.Button.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiButton_ResetStation")
        HmiButton_ResetStation.Button.Font = New System.Drawing.Font("Calibri", iFontSize)



        GroupBox_Search.Text = cLanguageManager.GetUserTextLine("StationCheck", "GroupBox_Search")
        GroupBox_Search.Font = New System.Drawing.Font("Calibri", iFontSize)

        GroupBox_Function.Text = cLanguageManager.GetUserTextLine("StationCheck", "GroupBox_Function")
        GroupBox_Function.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_StartDate.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_StartDate")
        HmiLabel_StartDate.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_EndDate.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_EndDate")
        HmiLabel_EndDate.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Data_SFC.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Data_SFC")
        HmiLabel_Data_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiLabel_Station.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Station")
        HmiLabel_Station.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Station.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_SFC.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Function_SFC")
        HmiLabel_Function_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Function_SFC.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_Result.Label.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiLabel_Function_Result")
        HmiLabel_Function_Result.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Function_Result.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)



        HmiButton_Modify.Button.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiButton_Modify")
        HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Search.Button.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiButton_Search")
        HmiButton_Search.Button.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiButton_Cancel.Button.Text = cLanguageManager.GetUserTextLine("StationCheck", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiDataView_Data.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiDataViewPage_Data.Label_TotalPage.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.Label_Total.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.HmiTextBox_Page.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiComboBox_Station.ComboBox.DropDownStyle = ComboBoxStyle.DropDown
        '  HmiComboBox_Function_Result.ComboBox.DropDownStyle = ComboBoxStyle.DropDown

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


        HmiButton_Modify.Button.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiComboBox_Function_Result.ComboBox.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)
        HmiButton_ResetStation.Button.Enabled = IIf(ePageMode = enumPageMode.Edit, True, False)

        HmiComboBox_Station.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            HmiComboBox_Station.ComboBox.Items.Add(element.ID.ToString)
        Next

        HmiComboBox_Function_Result.ComboBox.Items.Clear()
        HmiComboBox_Function_Result.ComboBox.Items.Add("PASS")
        HmiComboBox_Function_Result.ComboBox.Items.Add("FAIL")

        Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromName(cObjectSource.Name)
        If Not IsNothing(cDeviceCfg) Then
            Dim cMachineStatusCfg As clsMachineStatusCfg = cMachineStatusManager.GetMachineStatusCfgFromName(cDeviceCfg.StationID)
            If Not IsNothing(cDeviceCfg) And Not IsNothing(cMachineStatusCfg) Then
                HmiTextBox_SFC.TextBox.Text = cMachineStatusCfg.VariantCfg.SFC
                If HmiTextBox_SFC.TextBox.Text <> "" Then
                    Dim strResult As String = ""
                    cObjectSource.cProductionData.GetControlDataSFC(HmiTextBox_SFC.TextBox.Text, strResult)
                    HmiTextBox_Result.TextBox.Text = strResult
                End If

            End If
        End If

        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiTextBox_Result.TextBoxReadOnly = True
        HmiTextBox_SFC.TextBoxReadOnly = True
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Modify.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_ResetStation.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiTextBox_Data_SFC.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        Search()
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            HmiTableLayoutPanel_Mid.RowStyles(0).Height = (HmiTextBox_Data_SFC.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Data_SFC.TextBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiTextBox_Data_SFC.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Data_SFC.TextBox.Height + 6
            For Each element As RowStyle In HmiTableLayoutPanel_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Data_SFC.TextBox.Height + 6 + 6
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
                Case "HmiButton_Modify"
                    Modify()

                Case "HmiButton_ResetStation"

                    Reset()
                    Search()
            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Public Sub Reset()
        Try
            If HmiTextBox_SFC.TextBox.Text <> "" Then
                cObjectSource.cProductionData.UpdateControlDataSFC(HmiTextBox_SFC.TextBox.Text, "PASS")
                Dim strResult As String = ""
                cObjectSource.cProductionData.GetControlDataSFC(HmiTextBox_SFC.TextBox.Text, strResult)
                HmiTextBox_Result.TextBox.Text = strResult
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cObjectSource.cProductionData.SelectControlDataToDataView(enumViewPageType.FirstPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiTextBox_Data_SFC.TextBox.Text,
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
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiTextBox_Data_SFC.TextBox.Text = ""
                                       HmiComboBox_Station.ComboBox.SelectedIndex = -1
                                       HmiTextBox_Function_SFC.TextBox.Text = ""
                                       HmiComboBox_Function_Result.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cObjectSource.cProductionData.SelectControlDataToDataView(enumViewPageType.FirstPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_Data_SFC.TextBox.Text,
                                                     HmiComboBox_Station.ComboBox.Text
                                         )

                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub



    Public Sub Modify()
        Try
            HmiTextBox_Data_SFC.TextBox.Text = ""
            HmiComboBox_Station.ComboBox.SelectedIndex = -1
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "10"), enumExceptionType.Alarm, FormName))
                Return
            End If

            If Not cObjectSource.cProductionData.HasControlDataSFC(HmiTextBox_Function_SFC.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "11", HmiTextBox_Function_SFC.Text), enumExceptionType.Alarm, FormName))
                Return
            End If

            cObjectSource.cProductionData.UpdateControlDataSFC(HmiTextBox_Function_SFC.Text, HmiComboBox_Function_Result.ComboBox.Text)
            cObjectSource.cProductionData.SelectControlDataToDataView(enumViewPageType.NoPage,
                                                     HmiDateTime_Start.DateTimeToString,
                                                     HmiDateTime_End.DateTimeToString,
                                                     HmiTextBox_Data_SFC.TextBox.Text,
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
            HmiTextBox_Function_SFC.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
            HmiComboBox_Function_Result.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(4).Value
        End If
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
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Public Function StopRefresh(ByVal cLocalElement As System.Collections.Generic.Dictionary(Of String, Object), ByVal cSystemElement As System.Collections.Generic.Dictionary(Of String, Object)) As Boolean
        bExit = True
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

