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
    Public Const FormName As String = "CarrierManager"
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
    Private cCarrierManager As clsCarrierManager
    Private WithEvents HmiDateTime_Start As New Kochi.HMI.MainControl.UI.HMIDateTime
    Private WithEvents HmiDateTime_End As New Kochi.HMI.MainControl.UI.HMIDateTime '
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private OldStructCarrierManager As New StructCarrierManager
    Private TempStructCarrierManager As New StructCarrierManager
    Protected lListControlParameter As New List(Of String)
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

    Public Property IO As clsCarrierManager
        Set(ByVal value As clsCarrierManager)
            cCarrierManager = value
        End Set
        Get
            Return cCarrierManager
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
        cDeviceCfg = cDeviceManager.GetDeviceFromName(cCarrierManager.Name)
        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        GetPageMode()
        InitForm()
        InitControlText()
        Return True
    End Function

    Public Function InitForm() As Boolean
        TopLevel = False
        OldStructCarrierManager.intCarrierID = 99
        cDataGridViewPage_Data = New clsDataGridViewPage
        cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
        cDataGridViewPage_Data.RowsPerPage = 12
        cCarrierManager.cCarrierDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
        AddHandler cCarrierManager.ValueChanged, AddressOf ValueChanged
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
        TabPage1.Text = cLanguageManager.GetUserTextLine("CarrierManager", "TabPage1")
        TabPage2.Text = cLanguageManager.GetUserTextLine("CarrierManager", "TabPage2")
        TabPage1.Font = New System.Drawing.Font("Calibri", iFontSize)
        TabPage2.Font = New System.Drawing.Font("Calibri", iFontSize)


        GroupBox_Search.Text = cLanguageManager.GetUserTextLine("CarrierManager", "GroupBox_Search")
        GroupBox_Search.Font = New System.Drawing.Font("Calibri", iFontSize)

        GroupBox_Function.Text = cLanguageManager.GetUserTextLine("CarrierManager", "GroupBox_Function")
        GroupBox_Function.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Data_CarrierID.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Data_CarrierID")
        HmiLabel_Data_CarrierID.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Data_CarrierID.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Data_Station.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Data_Station")
        HmiLabel_Data_Station.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Data_Station.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiButton_Search.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_Search")
        HmiButton_Search.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Cancel.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiLabel_Function_ID.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Function_ID")
        HmiLabel_Function_ID.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Function_ID.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_CarrierID.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Function_CarrierID")
        HmiLabel_Function_CarrierID.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiTextBox_Function_CarrierID.TextBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Function_Station.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Function_Station")
        HmiLabel_Function_Station.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiComboBox_Function_Combox.ComboBox.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Add.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_Add")
        HmiButton_Add.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Modify.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_Modify")
        HmiButton_Modify.Button.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_Del.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_Del")
        HmiButton_Del.Button.Font = New System.Drawing.Font("Calibri", iFontSize)


        HmiDataView_Data.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.RowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.AlternatingRowsDefaultCellStyle.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataView_Data.ReadOnly = True

        HmiDataViewPage_Data.Label_TotalPage.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.Label_Total.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiDataViewPage_Data.HmiTextBox_Page.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiComboBox_Function_Combox.ComboBox.Items.Clear()
        HmiComboBox_Function_Combox.ComboBox.Items.Add("Abort")
        HmiComboBox_Function_Combox.ComboBox.DropDownStyle = ComboBoxStyle.DropDown

        HmiLabel_CarrierID.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_CarrierID")
        HmiLabel_CarrierID.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Variant.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_SFC.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_SFC")
        HmiLabel_SFC.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Carrier.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Carrier")
        HmiLabel_Carrier.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButtonWithIndicate_Write.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButtonWithIndicate_Write")
        HmiButtonWithIndicate_Write.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiButtonWithIndicate_Read.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButtonWithIndicate_Read")
        HmiButtonWithIndicate_Read.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButtonWithIndicate_Reset.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButtonWithIndicate_Reset")
        HmiButtonWithIndicate_Reset.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_Present.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Present")
        HmiLabel_Present.Label.Font = New System.Drawing.Font("Calibri", iFontSize)
        HmiLabel_Error.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_Error")
        HmiLabel_Error.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiLabel_CarrierStation.Label.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiLabel_CarrierStation")
        HmiLabel_CarrierStation.Label.Font = New System.Drawing.Font("Calibri", iFontSize)

        HmiButton_ResetCarrierStation.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_ResetCarrierStation")
        HmiButton_ResetCarrierStation.Button.Font = New System.Drawing.Font("Calibri", iFontSize - 4)

        HmiButton_AbortCarrierStation.Button.Text = cLanguageManager.GetUserTextLine("CarrierManager", "HmiButton_AbortCarrierStation")
        HmiButton_AbortCarrierStation.Button.Font = New System.Drawing.Font("Calibri", iFontSize - 4)

        HmiTextBox_CarrierStation.TextBoxReadOnly = True

        If cUserManager.CurrentUserCfg.Level < enumUserLevel.Supplier Then
            HmiButtonWithIndicate_Write.Enabled = False
        Else
            HmiButtonWithIndicate_Write.Enabled = True
        End If

        HmiComboBox_CarrierID.ComboBox.Items.Clear()
        For i = 1 To 100
            HmiComboBox_CarrierID.ComboBox.Items.Add(i.ToString)
        Next
        HmiButton_AbortCarrierStation.Button.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_ResetCarrierStation.Button.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Add.Button.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Del.Button.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButton_Modify.Button.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiComboBox_Function_Combox.ComboBox.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButtonWithIndicate_Read.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        ' HmiButtonWithIndicate_Write.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiButtonWithIndicate_Reset.Enabled = IIf(cUserManager.CurrentUserCfg.Level >= enumUserLevel.Engineer, True, False)
        HmiTextBox_SFC.TextBoxReadOnly = True
        HmiTextBox_Variant.TextBoxReadOnly = True
        HmiTextBox_CarrierID.TextBoxReadOnly = True
        AddHandler HmiButtonWithIndicate_Write.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_Read.Click, AddressOf Button_Click
        AddHandler HmiButtonWithIndicate_Reset.Click, AddressOf Button_Click
        AddHandler HmiButton_ResetCarrierStation.Button.Click, AddressOf Button_Click
        AddHandler HmiButton_AbortCarrierStation.Button.Click, AddressOf Button_Click
        AddHandler HmiTextBox_Data_CarrierID.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Add.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Del.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Modify.Button.Click, AddressOf HmiButton_Click
        Search()
        Return True
    End Function

    Private Sub ValueChanged()
        If bExit Then Return
        mMainForm.InvokeAction(Sub()
                                   HmiTextBox_CarrierStation.TextBox.Text = cCarrierManager.GetCarrierStation(HmiTextBox_CarrierID.TextBox.Text)

                               End Sub)
    End Sub



    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            HmiTableLayoutPanel_Mid.RowStyles(0).Height = (HmiTextBox_Data_CarrierID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Data_CarrierID.TextBox.Height + 6 + 6
            HmiLabel_Data_Station.Dock = DockStyle.None
            GroupBox_Search.Height = (HmiTextBox_Data_CarrierID.TextBox.Height + 6 + 6) * 1 + HmiTextBox_Data_CarrierID.TextBox.Height + 6
            For Each element As RowStyle In HmiTableLayoutPanel_Function.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_SFC.TextBox.Height + 6 + 6
            Next
            HmiLabel_Data_Station.Dock = DockStyle.Fill
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Private Function CheckCarrierID() As Boolean
        Try
            If HmiComboBox_CarrierID.ComboBox.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("CarrierManager", "3"), enumExceptionType.Alarm, IOForm.FormName))
                Return False
            End If
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex.Message, enumExceptionType.Alarm, IOForm.FormName))
            Return False
        End Try
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case "HmiButtonWithIndicate_Write"
                If Not CheckCarrierID() Then
                    Return
                End If
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIWrite", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".intHMICarrierID", Byte.Parse((HmiComboBox_CarrierID.ComboBox.SelectedIndex + 1)))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIWrite", dNewValue)

            Case "HmiButtonWithIndicate_Read"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRead", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False

                cHMIPLC.WriteAny(lListInitParameter(0) + ".strKostalNr", "", New Integer() {0})
                cHMIPLC.WriteAny(lListInitParameter(0) + ".strSerialNr", "", New Integer() {0})
                cHMIPLC.WriteAny(lListInitParameter(0) + ".intCarrierID", Byte.Parse(0))
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRead", dNewValue)

            Case "HmiButtonWithIndicate_Reset"
                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIReset", GetType(StructGapFillerButton))
                Dim dNewValue As New StructGapFillerButton
                dNewValue.bulHMIDoAction = Not dOldValue.bulHMIDoAction
                dNewValue.bulPlcActionIsFail = False
                dNewValue.bulPlcActionIsPass = False
                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIReset", dNewValue)

            Case "HmiButton_ResetCarrierStation"
                Dim cResuslt As String = ""
                cCarrierManager.ResetCarrierID(HmiTextBox_CarrierID.TextBox.Text, cResuslt)
                Search()
            Case "HmiButton_AbortCarrierStation"
                Dim cResuslt As String = ""
                cCarrierManager.UpdateCarrier(HmiTextBox_CarrierID.TextBox.Text, "Abort", cResuslt)
                Search()
        End Select
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
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cCarrierManager.cCarrierDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               HmiTextBox_Data_CarrierID.TextBox.Text,
                                                                                 HmiTextBox_Data_Station.TextBox.Text
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
                                       HmiTextBox_Data_CarrierID.TextBox.Text = ""
                                       HmiTextBox_Data_Station.TextBox.Text = ""
                                       cCarrierManager.cCarrierDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                  HmiTextBox_Data_CarrierID.TextBox.Text,
                                                    HmiTextBox_Data_Station.TextBox.Text
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
            HmiTextBox_Data_CarrierID.TextBox.Text = ""
            HmiTextBox_Data_Station.TextBox.Text = ""
            If HmiTextBox_Function_CarrierID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "10"), enumExceptionType.Alarm, FormName))
                Return
            End If

            If cCarrierManager.cCarrierDataManager.HasCarrierID(HmiTextBox_Function_CarrierID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "11", HmiTextBox_Function_CarrierID.Text), enumExceptionType.Alarm, FormName))
                Return
            End If

            cCarrierManager.cCarrierDataManager.InSertData(HmiTextBox_Function_CarrierID.Text, HmiComboBox_Function_Combox.ComboBox.Text)
            cCarrierManager.cCarrierDataManager.SelectToDataView(enumViewPageType.LastPage,
                         HmiTextBox_Data_CarrierID.TextBox.Text,
                           HmiTextBox_Data_Station.TextBox.Text)

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, FormName))
        End Try
    End Sub

    Public Sub Delete()
        Try
            HmiTextBox_Data_CarrierID.TextBox.Text = ""
            HmiTextBox_Data_Station.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "12"), enumExceptionType.Alarm, FormName))
                Return
            End If

            cCarrierManager.cCarrierDataManager.DeleteData(HmiTextBox_Function_ID.Text)
            cCarrierManager.cCarrierDataManager.SelectToDataView(enumViewPageType.NoPage,
                         HmiTextBox_Data_CarrierID.TextBox.Text,
                           HmiTextBox_Data_Station.TextBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Modify()
        Try
            HmiTextBox_Data_CarrierID.TextBox.Text = ""
            HmiTextBox_Data_Station.TextBox.Text = ""
            If HmiTextBox_Function_ID.Text = "" Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "12"), enumExceptionType.Alarm, FormName))
                Return
            End If

            If Not cCarrierManager.cCarrierDataManager.HasCarrierID(HmiTextBox_Function_CarrierID.Text) Then
                cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine(FormName, "13", HmiTextBox_Function_CarrierID.Text), enumExceptionType.Alarm, FormName))
                Return
            End If

            cCarrierManager.cCarrierDataManager.UpdateData(HmiTextBox_Function_CarrierID.Text, HmiComboBox_Function_Combox.ComboBox.Text)
            cCarrierManager.cCarrierDataManager.SelectToDataView(enumViewPageType.NoPage,
                         HmiTextBox_Data_CarrierID.TextBox.Text,
                           HmiTextBox_Data_Station.TextBox.Text)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, FormName))
        End Try
    End Sub


    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            HmiTextBox_Function_ID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value
            HmiTextBox_Function_CarrierID.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(1).Value
            HmiComboBox_Function_Combox.ComboBox.Text = HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(2).Value
        End If
    End Sub

    

    Public Function SetParameter(ByVal lListInitParameter As List(Of String), ByVal lListControlParameter As List(Of String)) As Boolean
        Me.lListInitParameter = lListInitParameter
        Me.lListControlParameter = lListControlParameter
        Return True
    End Function

    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                System.Threading.Thread.Sleep(10)
                If cErrorMessageManager.GetStationManagerStateFromKey(IOForm.FormName) = enumErrorMessageManagerState.Alarm Then Continue While
                Select Case iStep
                    Case 1
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("CarrierManager", "1"), enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1
                    Case 2
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetUserTextLine("CarrierManager", "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, IOForm.FormName))
                            Continue While
                        End If
                        iStep = iStep + 1

                    Case 3
                        cHMIPLC.AddNotificationEx(lListInitParameter(0), GetType(StructCarrierManager), New StructCarrierManager)
                        iStep = iStep + 1

                    Case 4
                        TempStructCarrierManager = cHMIPLC.GetValue(lListInitParameter(0))
                        If TempStructCarrierManager.strSerialNr <> OldStructCarrierManager.strSerialNr Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_SFC.TextBox.Text = TempStructCarrierManager.strSerialNr
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.strKostalNr <> OldStructCarrierManager.strKostalNr Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_Variant.TextBox.Text = TempStructCarrierManager.strKostalNr
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.intCarrierID <> OldStructCarrierManager.intCarrierID Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiTextBox_CarrierID.TextBox.Text = TempStructCarrierManager.intCarrierID
                                                       HmiTextBox_CarrierStation.TextBox.Text = cCarrierManager.GetCarrierStation(HmiTextBox_CarrierID.TextBox.Text)
                                                   End Sub)
                        End If


                        If TempStructCarrierManager.bulCarrierPresent <> OldStructCarrierManager.bulCarrierPresent Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Present.SetIndicateBackColor(TempStructCarrierManager.bulCarrierPresent)
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.bulCarrierError <> OldStructCarrierManager.bulCarrierError Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiSensor_Error.SetIndicateErrorBackColor(TempStructCarrierManager.bulCarrierError)
                                                   End Sub)

                            If TempStructCarrierManager.bulCarrierError Then
                                Dim dNewValue As New StructGapFillerButton
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRead", dNewValue)
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIWrite", dNewValue)
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIReset", dNewValue)
                            End If

                        End If

                        'HmiButtonWithIndicate_Write
                        If TempStructCarrierManager.bulHMIWrite.bulHMIDoAction <> OldStructCarrierManager.bulHMIWrite.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Write.SetIndicateColor(TempStructCarrierManager.bulHMIWrite.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.bulHMIWrite.bulPlcActionIsFail <> OldStructCarrierManager.bulHMIWrite.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIWrite.bulPlcActionIsPass <> OldStructCarrierManager.bulHMIWrite.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Write.SetIndicateColor(TempStructCarrierManager.bulHMIWrite.bulPlcActionIsPass, TempStructCarrierManager.bulHMIWrite.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructCarrierManager.bulHMIWrite.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIWrite.bulPlcActionIsPass Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIWrite", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIWrite", dNewValue)
                            End If
                        End If

                        'HmiButtonWithIndicate_Read
                        If TempStructCarrierManager.bulHMIRead.bulHMIDoAction <> OldStructCarrierManager.bulHMIRead.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Read.SetIndicateColor(TempStructCarrierManager.bulHMIRead.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.bulHMIRead.bulPlcActionIsFail <> OldStructCarrierManager.bulHMIRead.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIRead.bulPlcActionIsPass <> OldStructCarrierManager.bulHMIRead.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Read.SetIndicateColor(TempStructCarrierManager.bulHMIRead.bulPlcActionIsPass, TempStructCarrierManager.bulHMIRead.bulPlcActionIsFail)
                                                   End Sub)

                            If TempStructCarrierManager.bulHMIRead.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIRead.bulPlcActionIsPass Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIRead", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIRead", dNewValue)
                            End If
                        End If


                        'HmiButtonWithIndicate_Reset
                        If TempStructCarrierManager.bulHMIReset.bulHMIDoAction <> OldStructCarrierManager.bulHMIReset.bulHMIDoAction Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Reset.SetIndicateColor(TempStructCarrierManager.bulHMIReset.bulHMIDoAction)
                                                   End Sub)
                        End If

                        If TempStructCarrierManager.bulHMIReset.bulPlcActionIsFail <> OldStructCarrierManager.bulHMIReset.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIReset.bulPlcActionIsPass <> OldStructCarrierManager.bulHMIReset.bulPlcActionIsPass Then
                            mMainForm.InvokeAction(Sub()
                                                       HmiButtonWithIndicate_Reset.SetIndicateColor(TempStructCarrierManager.bulHMIReset.bulPlcActionIsPass, TempStructCarrierManager.bulHMIReset.bulPlcActionIsFail)
                                                   End Sub)
                            If TempStructCarrierManager.bulHMIReset.bulPlcActionIsFail Or TempStructCarrierManager.bulHMIReset.bulPlcActionIsPass Then
                                Dim dOldValue As StructGapFillerButton = cHMIPLC.ReadAny(lListInitParameter(0) + ".bulHMIReset", GetType(StructGapFillerButton))
                                Dim dNewValue As New StructGapFillerButton
                                dNewValue.bulHMIDoAction = False
                                dNewValue.bulPlcActionIsFail = dOldValue.bulPlcActionIsFail
                                dNewValue.bulPlcActionIsPass = dOldValue.bulPlcActionIsPass
                                cHMIPLC.WriteAny(lListInitParameter(0) + ".bulHMIReset", dNewValue)
                            End If
                        End If

                        OldStructCarrierManager.bulCarrierError = TempStructCarrierManager.bulCarrierError
                        OldStructCarrierManager.bulCarrierPresent = TempStructCarrierManager.bulCarrierPresent
                        OldStructCarrierManager.strSerialNr = TempStructCarrierManager.strSerialNr
                        OldStructCarrierManager.intCarrierID = TempStructCarrierManager.intCarrierID
                        OldStructCarrierManager.strKostalNr = TempStructCarrierManager.strKostalNr

                        OldStructCarrierManager.bulHMIWrite.bulHMIDoAction = TempStructCarrierManager.bulHMIWrite.bulHMIDoAction
                        OldStructCarrierManager.bulHMIWrite.bulPlcActionIsFail = TempStructCarrierManager.bulHMIWrite.bulPlcActionIsFail
                        OldStructCarrierManager.bulHMIWrite.bulPlcActionIsPass = TempStructCarrierManager.bulHMIWrite.bulPlcActionIsPass

                        OldStructCarrierManager.bulHMIRead.bulHMIDoAction = TempStructCarrierManager.bulHMIRead.bulHMIDoAction
                        OldStructCarrierManager.bulHMIRead.bulPlcActionIsFail = TempStructCarrierManager.bulHMIRead.bulPlcActionIsFail
                        OldStructCarrierManager.bulHMIRead.bulPlcActionIsPass = TempStructCarrierManager.bulHMIRead.bulPlcActionIsPass

                        OldStructCarrierManager.bulHMIReset.bulHMIDoAction = TempStructCarrierManager.bulHMIReset.bulHMIDoAction
                        OldStructCarrierManager.bulHMIReset.bulPlcActionIsFail = TempStructCarrierManager.bulHMIReset.bulPlcActionIsFail
                        OldStructCarrierManager.bulHMIReset.bulPlcActionIsPass = TempStructCarrierManager.bulHMIReset.bulPlcActionIsPass

                        iStep = 4


                End Select
            Catch ex As Exception
                If Not bExit Then cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, IOForm.FormName))
            End Try


        End While
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
        If Not IsNothing(lListInitParameter) AndAlso lListInitParameter.Count > 0 Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(lListInitParameter(0))
        End If
        Return True
        Return True
    End Function
    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        StopRefresh(cLocalElement, cSystemElement)
        RemoveHandler cCarrierManager.ValueChanged, AddressOf ValueChanged
        Me.Dispose()
        Return True
    End Function

End Class

