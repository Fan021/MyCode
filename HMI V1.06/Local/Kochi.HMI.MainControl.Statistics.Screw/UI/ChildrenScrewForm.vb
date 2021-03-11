Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports Kochi.HMI.MainControl.LocalDevice

<clsChildrenUINameAttribute("Screw Logging", GetType(clsHMIAST))>
Public Class ChildrenScrewForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cScrewDataManager As clsScrewDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cMachineManager As clsMachineManager
    Private cVariantManager As clsVariantManager
    Private cCsvHandler As New clsCsvHandler
    Private cFormFontResize As clsFormFontResize
    Private cLanguageManager As clsLanguageManager
    Private cDeviceManager As clsDeviceManager
    Protected Const strTorqueUnit As String = " N.m"
    Protected Const strAngleUnit As String = " °"
    Protected Const strPercent As String = " %"
    Private strButtonName As String
    Private cThread As Thread
    Private mMainForm As IMainUI
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
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cDataGridViewPage_Data = New clsDataGridViewPage
            cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage_Data.RowsPerPage = 13
            cScrewDataManager = New clsScrewDataManager
            cScrewDataManager.Init(cSystemElement)
            cScrewDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenScrewForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False

        HmiComboBox_Station.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            HmiComboBox_Station.ComboBox.Items.Add(element.ID)
        Next
        HmiComboBox_Result.ComboBox.Items.Clear()
        For i = 0 To 23
            HmiComboBox_Result.ComboBox.Items.Add(i.ToString)
        Next

        HmiComboBox_Program.ComboBox.Items.Clear()
        For i = 1 To 16
            HmiComboBox_Program.ComboBox.Items.Add(i.ToString)
        Next

        HmiComboBox_Seq.ComboBox.Items.Clear()
        For i = 1 To 100
            HmiComboBox_Seq.ComboBox.Items.Add(i.ToString)
        Next

        HmiComboBox_Variant.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
        Next
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_EndDate")
        HmiLabel_Station.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Station")
        HmiButton_Export.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiButton_Export")
        HmiLabel_Station.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Station")
        HmiLabel_Variant.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Variant")
        HmiLabel_Result.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Result")
        HmiLabel_SFC.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_SFC")
        HmiLabel_PartNumber.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_PartNumber")
        HmiLabel_Seq.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Seq")
        HmiLabel_Device.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Device")
        HmiLabel_AST.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_AST")
        HmiLabel_Program.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Program")

        HmiLabel_Torque_MaxValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_MaxValue")
        HmiLabel_Torque_MaxValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_MinValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_MinValue")
        HmiLabel_Torque_MinValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_AvgValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_AvgValue")
        HmiLabel_Torque_AvgValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_LowLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_LowLimit")
        HmiLabel_Torque_LowLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_UpLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_UpLimit")
        HmiLabel_Torque_UpLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Std.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Std")
        HmiLabel_Torque_Std.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Cp.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Cp")
        HmiLabel_Torque_Cp.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Cpk.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Cpk")
        HmiLabel_Torque_Cpk.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Total.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Total")
        HmiLabel_Torque_Total.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Pass.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Pass")
        HmiLabel_Torque_Pass.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Fail.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Fail")
        HmiLabel_Torque_Fail.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Rate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Rate")
        HmiLabel_Torque_Rate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Torque_Step.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Torque_Step")
        HmiLabel_Torque_Step.Label.TextAlign = ContentAlignment.MiddleRight
        RadioButton_Torque_Step1.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Torque_Step1")
        RadioButton_Torque_Step2.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Torque_Step2")
        RadioButton_Torque_Step3.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Torque_Step3")

        HmiTextBox_Torque_MaxValue.TextBoxReadOnly = True
        HmiTextBox_Torque_MinValue.TextBoxReadOnly = True
        HmiTextBox_Torque_AvgValue.TextBoxReadOnly = True
        HmiTextBox_Torque_Std.TextBoxReadOnly = True
        HmiTextBox_Torque_Cp.TextBoxReadOnly = True
        HmiTextBox_Torque_Cpk.TextBoxReadOnly = True
        HmiTextBox_Torque_Total.TextBoxReadOnly = True
        HmiTextBox_Torque_Pass.TextBoxReadOnly = True
        HmiTextBox_Torque_Fail.TextBoxReadOnly = True
        HmiTextBox_Torque_Rate.TextBoxReadOnly = True
        HmiTextBox_Torque_LowLimit.TextBoxReadOnly = True
        HmiTextBox_Torque_UpLimit.TextBoxReadOnly = True

        HmiLabel_Angle_MaxValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_MaxValue")
        HmiLabel_Angle_MaxValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_MinValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_MinValue")
        HmiLabel_Angle_MinValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_AvgValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_AvgValue")
        HmiLabel_Angle_AvgValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_LowLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_LowLimit")
        HmiLabel_Angle_LowLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_UpLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_UpLimit")
        HmiLabel_Angle_UpLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Std.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Std")
        HmiLabel_Angle_Std.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Cp.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Cp")
        HmiLabel_Angle_Cp.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Cpk.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Cpk")
        HmiLabel_Angle_Cpk.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Total.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Total")
        HmiLabel_Angle_Total.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Pass.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Pass")
        HmiLabel_Angle_Pass.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Fail.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Fail")
        HmiLabel_Angle_Fail.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Rate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Rate")
        HmiLabel_Angle_Rate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Angle_Step.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "HmiLabel_Angle_Step")
        HmiLabel_Angle_Step.Label.TextAlign = ContentAlignment.MiddleRight
        RadioButton_Angle_Step1.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Angle_Step1")
        RadioButton_Angle_Step2.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Angle_Step2")
        RadioButton_Angle_Step3.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "RadioButton_Angle_Step3")

        HmiTextBox_Angle_MaxValue.TextBoxReadOnly = True
        HmiTextBox_Angle_MinValue.TextBoxReadOnly = True
        HmiTextBox_Angle_AvgValue.TextBoxReadOnly = True
        HmiTextBox_Angle_Std.TextBoxReadOnly = True
        HmiTextBox_Angle_Cp.TextBoxReadOnly = True
        HmiTextBox_Angle_Cpk.TextBoxReadOnly = True
        HmiTextBox_Angle_Total.TextBoxReadOnly = True
        HmiTextBox_Angle_Pass.TextBoxReadOnly = True
        HmiTextBox_Angle_Fail.TextBoxReadOnly = True
        HmiTextBox_Angle_Rate.TextBoxReadOnly = True
        HmiTextBox_Angle_LowLimit.TextBoxReadOnly = True
        HmiTextBox_Angle_UpLimit.TextBoxReadOnly = True

        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiTextBox_SFC.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiTextBox_Torque_MaxValue.TextBox.SizeChanged, AddressOf MaxValueTextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Export.Button.Click, AddressOf HmiButton_Click
        AddHandler ContextMenuStrip_Function.Click, AddressOf HmiButton_Click
        AddHandler HmiTextBox_Torque_LowLimit.TextBox.KeyDown, AddressOf TextBox_KeyDown
        AddHandler HmiTextBox_Torque_UpLimit.TextBox.KeyDown, AddressOf TextBox_KeyDown
        AddHandler HmiTextBox_Angle_LowLimit.TextBox.KeyDown, AddressOf TextBox_KeyDown
        AddHandler HmiTextBox_Angle_UpLimit.TextBox.KeyDown, AddressOf TextBox_KeyDown
        AddHandler HmiComboBox_Station.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        AddHandler RadioButton_Torque_Step1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_Torque_Step2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_Torque_Step3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_Angle_Step1.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_Angle_Step2.CheckedChanged, AddressOf RadioButton_CheckedChanged
        AddHandler RadioButton_Angle_Step3.CheckedChanged, AddressOf RadioButton_CheckedChanged
        cThread = New Thread(AddressOf Search)
        cThread.Start()
        Return True
    End Function

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiComboBox_Station"
                    HmiComboBox_AST.ComboBox.Items.Clear()
                    HmiComboBox_Device.ComboBox.Items.Clear()
                    If HmiComboBox_Station.ComboBox.SelectedIndex = -1 Then Return
                    Dim lListDeviceCfg As List(Of clsDeviceCfg)
                    lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID((HmiComboBox_Station.ComboBox.Text), GetType(clsHMIPKP), GetType(clsHMIPKP_Z))
                    HmiComboBox_Device.ComboBox.Items.Clear()
                    If Not IsNothing(lListDeviceCfg) Then
                        For Each element As clsDeviceCfg In lListDeviceCfg
                            HmiComboBox_Device.ComboBox.Items.Add(element.StationIndex)
                        Next
                    End If

                    lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID((HmiComboBox_Station.ComboBox.Text), GetType(clsHMIAST))
                    HmiComboBox_AST.ComboBox.Items.Clear()
                    If Not IsNothing(lListDeviceCfg) Then
                        For Each element As clsDeviceCfg In lListDeviceCfg
                            HmiComboBox_AST.ComboBox.Items.Add(element.StationIndex)
                        Next
                    End If

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 4 + HmiTextBox_SFC.TextBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 4 + HmiTextBox_SFC.TextBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_SFC.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub MaxValueTextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body_Mid_Torque.RowStyles(0).Height = (HmiTextBox_Torque_MaxValue.TextBox.Height + 6 + 6) * 8
            For Each element As RowStyle In TableLayoutPanel_Body_Torque_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Torque_MaxValue.TextBox.Height + 6 + 6
            Next

            TableLayoutPanel_Body_Mid_Angle.RowStyles(0).Height = (HmiTextBox_Torque_MaxValue.TextBox.Height + 6 + 6) * 8
            For Each element As RowStyle In TableLayoutPanel_Body_Angle_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Torque_MaxValue.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
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
                    Export()
                Case "ContextMenuStrip_Function"
                    cThread = New Thread(AddressOf Delete)
                    cThread.IsBackground = True
                    cThread.Start()

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cScrewDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                          HmiDateTime_Start.DateTimeToString,
                                                                          HmiDateTime_End.DateTimeToString,
                                                                          HmiComboBox_Station.ComboBox.Text,
                                                                          HmiComboBox_Variant.ComboBox.Text,
                                                                          HmiComboBox_Result.ComboBox.Text,
                                                                          HmiTextBox_SFC.TextBox.Text,
                                                                          HmiTextBox_PartNumber.TextBox.Text,
                                                                          HmiComboBox_Seq.ComboBox.Text,
                                                                          HmiComboBox_Device.ComboBox.Text,
                                                                          HmiComboBox_AST.ComboBox.Text,
                                                                          HmiComboBox_Program.ComboBox.Text
                                                                          )
                                       ShowChart()
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Cancel()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiTextBox_SFC.TextBox.Text = ""
                                       HmiTextBox_PartNumber.TextBox.Text = ""
                                       HmiComboBox_Seq.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Program.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Device.ComboBox.SelectedIndex = -1
                                       HmiComboBox_AST.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Variant.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Station.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Result.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cScrewDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                          HmiDateTime_Start.DateTimeToString,
                                                                          HmiDateTime_End.DateTimeToString,
                                                                          HmiComboBox_Station.ComboBox.Text,
                                                                          HmiComboBox_Variant.ComboBox.Text,
                                                                          HmiComboBox_Result.ComboBox.Text,
                                                                          HmiTextBox_SFC.TextBox.Text,
                                                                          HmiTextBox_PartNumber.TextBox.Text,
                                                                          HmiComboBox_Seq.ComboBox.Text,
                                                                          HmiComboBox_Device.ComboBox.Text,
                                                                          HmiComboBox_AST.ComboBox.Text,
                                                                          HmiComboBox_Program.ComboBox.Text
                                                                          )
                                       ShowChart()
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Delete()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       If Not IsNothing(HmiDataView_Data.CurrentRow) AndAlso HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
                                           cScrewDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cScrewDataManager.SelectToDataView(enumViewPageType.NoPage,
                                                                             HmiDateTime_Start.DateTimeToString,
                                                                             HmiDateTime_End.DateTimeToString,
                                                                             HmiComboBox_Station.ComboBox.Text,
                                                                             HmiComboBox_Variant.ComboBox.Text,
                                                                             HmiComboBox_Result.ComboBox.Text,
                                                                             HmiTextBox_SFC.TextBox.Text,
                                                                             HmiTextBox_PartNumber.TextBox.Text,
                                                                             HmiComboBox_Seq.ComboBox.Text,
                                                                             HmiComboBox_Device.ComboBox.Text,
                                                                             HmiComboBox_AST.ComboBox.Text,
                                                                             HmiComboBox_Program.ComboBox.Text
                                                                             )
                                           ShowChart()

                                       End If
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub TextBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            Dim dLowLimtValue As Double = 0
            Dim dUpLimtValue As Double = 0
            If e.KeyCode = Keys.Enter Then
                Select Case sender.name
                    Case "HmiTextBox_Torque_LowLimit", "HmiTextBox_Torque_UpLimit"
                        If Not IsNumeric(HmiTextBox_Torque_UpLimit.TextBox.Text.Replace(" ", "").Replace(strTorqueUnit.Replace(" ", ""), "")) Then
                            Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "1"))
                        End If
                        If Not IsNumeric(HmiTextBox_Torque_LowLimit.TextBox.Text.Replace(" ", "").Replace(strTorqueUnit.Replace(" ", ""), "")) Then
                            Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "2"))
                        End If
                        dLowLimtValue = CDbl(HmiTextBox_Torque_LowLimit.TextBox.Text.Replace(" ", "").Replace(strTorqueUnit.Replace(" ", ""), ""))
                        dUpLimtValue = CDbl(HmiTextBox_Torque_UpLimit.TextBox.Text.Replace(" ", "").Replace(strTorqueUnit.Replace(" ", ""), ""))
                        ChartTorqueDefaultValue()
                        ChartTorque(dLowLimtValue, dUpLimtValue)
                    Case "HmiTextBox_Angle_LowLimit", "HmiTextBox_Angle_UpLimit"
                        If Not IsNumeric(HmiTextBox_Angle_UpLimit.TextBox.Text.Replace(" ", "").Replace(strAngleUnit.Replace(" ", ""), "")) Then
                            Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "3"))
                        End If
                        If Not IsNumeric(HmiTextBox_Angle_LowLimit.TextBox.Text.Replace(" ", "").Replace(strAngleUnit.Replace(" ", ""), "")) Then
                            Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "4"))
                        End If
                        dLowLimtValue = CDbl(HmiTextBox_Angle_LowLimit.TextBox.Text.Replace(" ", "").Replace(strAngleUnit.Replace(" ", ""), ""))
                        dUpLimtValue = CDbl(HmiTextBox_Angle_UpLimit.TextBox.Text.Replace(" ", "").Replace(strAngleUnit.Replace(" ", ""), ""))
                        ChartAngleDefaultValue()
                        ChartAngle(dLowLimtValue, dUpLimtValue)

                End Select
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Public Sub ShowChart()
        ChartTorqueDefaultValue()
        ChartAngleDefaultValue()
        ChartTorque(0, 0)
        ChartAngle(0, 0)
    End Sub
    Private Sub ChartTorque(ByVal dLowLimtValue As Double, ByVal dUpLimtValue As Double)
        Try
            If HmiComboBox_Station.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_Device.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_AST.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_Program.ComboBox.SelectedIndex = -1 Then Return
            Dim x As New List(Of Double)
            Dim dLowValue As Double = Double.MaxValue
            Dim dUpValue As Double = Double.MinValue
            Dim iLowIndex As Integer = 0
            Dim iUpIndex As Integer = 0
            Dim iValueIndex As Integer = 0
            If RadioButton_Torque_Step1.Checked Then
                iLowIndex = 15
                iUpIndex = 18
                iValueIndex = 17
            End If

            If RadioButton_Torque_Step2.Checked Then
                iLowIndex = 24
                iUpIndex = 27
                iValueIndex = 26
            End If

            If RadioButton_Torque_Step3.Checked Then
                iLowIndex = 33
                iUpIndex = 36
                iValueIndex = 35
            End If

            For Each mDr As DataRow In cScrewDataManager.Ds_Data.Tables(0).Rows
                If CDbl(mDr(iLowIndex).ToString()) < dLowValue Then
                    dLowValue = CDbl(mDr(iLowIndex).ToString())
                End If
                If CDbl(mDr(iUpIndex).ToString()) > dUpValue Then
                    dUpValue = CDbl(mDr(iUpIndex).ToString())
                End If
                x.Add(CDbl(mDr(iValueIndex).ToString()))
            Next
            If x.Count <= 0 Then
                Return
            End If
            If dLowLimtValue <> 0 Then dLowValue = dLowLimtValue
            If dUpLimtValue <> 0 Then dUpValue = dUpLimtValue
            If dUpValue < dLowValue Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "5"))
            End If

            If x.Count >= 0 Then
                Dim cCPK As clsCPK = New clsCPK(x, dLowValue, dUpValue)
                If cCPK.FindValue() Then
                    HmiTextBox_Torque_MaxValue.TextBox.Text = cCPK.Max.ToString + strTorqueUnit
                    HmiTextBox_Torque_MinValue.TextBox.Text = cCPK.Min.ToString + strTorqueUnit
                    HmiTextBox_Torque_AvgValue.TextBox.Text = cCPK.Mean.ToString + strTorqueUnit
                    HmiTextBox_Torque_UpLimit.TextBox.Text = dUpValue.ToString + strTorqueUnit
                    HmiTextBox_Torque_LowLimit.TextBox.Text = dLowValue.ToString + strTorqueUnit
                    HmiTextBox_Torque_Std.TextBox.Text = cCPK.Std.ToString
                    HmiTextBox_Torque_Cp.TextBox.Text = cCPK.Cp.ToString
                    HmiTextBox_Torque_Cpk.TextBox.Text = cCPK.Cpk.ToString
                    HmiTextBox_Torque_Total.TextBox.Text = cCPK.TotalCount.ToString
                    HmiTextBox_Torque_Pass.TextBox.Text = cCPK.PassCount.ToString
                    HmiTextBox_Torque_Fail.TextBox.Text = cCPK.FailCount.ToString
                    HmiTextBox_Torque_Rate.TextBox.Text = cCPK.FailRate.ToString + strPercent

                    Chart_Torque_Value.Series().Clear()
                    Dim TorqueValue As Series = New Series("TorqueValue")
                    TorqueValue.ChartType = SeriesChartType.Line
                    TorqueValue.BorderWidth = 1
                    TorqueValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueValue")

                    Dim TorqueLowValue As Series = New Series("TorqueLowValue")
                    TorqueLowValue.ChartType = SeriesChartType.Line
                    TorqueLowValue.BorderWidth = 2
                    TorqueLowValue.BorderColor = Color.Red
                    TorqueLowValue.ToolTip = dLowValue.ToString
                    TorqueLowValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueLowValue")

                    Dim TorqueUpValue As Series = New Series("TorqueUpValue")
                    TorqueUpValue.ChartType = SeriesChartType.Line
                    TorqueUpValue.BorderWidth = 2
                    TorqueUpValue.BorderColor = Color.Maroon
                    TorqueUpValue.ToolTip = dUpValue.ToString
                    TorqueUpValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueUpValue")

                    For i = cScrewDataManager.Ds_Data.Tables(0).Rows.Count - 1 To 0 Step -1
                        TorqueValue.Points.AddXY(i, cScrewDataManager.Ds_Data.Tables(0).Rows(i)(iValueIndex))
                        TorqueLowValue.Points.AddXY(i, dLowValue)
                        TorqueUpValue.Points.AddXY(i, dUpValue)
                    Next
                    Chart_Torque_Value.Series.Add(TorqueLowValue)

                    Chart_Torque_Value.Series.Add(TorqueValue)
                    Chart_Torque_Value.Series.Add(TorqueUpValue)
                    '  Chart_Torque_Value.ChartAreas(0).AxisX.Interval = 1
                    Chart_Torque_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Torque Distribution")
                    Chart_Torque_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Value")
                    Chart_Torque_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    Chart_Torque_Value.ChartAreas(0).RecalculateAxesScale()

                    Chart_Torque_Rate.Series().Clear()
                    Dim mTempListRateGroupCfg As List(Of clsRateGroupCfg) = cCPK.FindRateGroup(7)
                    Dim iCnt As Integer = 0
                    Dim TorqueData As Series = New Series("TorqueCount")
                    TorqueData.ChartType = SeriesChartType.Column
                    TorqueData.BorderWidth = 25
                    TorqueData.ShadowOffset = 0
                    TorqueData.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueCount")
                    Chart_Torque_Rate.Series.Add(TorqueData)

                    For Each element As clsRateGroupCfg In mTempListRateGroupCfg
                        TorqueData.Points.AddY(element.Count)
                        TorqueData.Points(iCnt).AxisLabel = element.LowValue.ToString + "--" + element.UpValue.ToString
                        TorqueData.IsValueShownAsLabel = True
                        iCnt = iCnt + 1
                    Next

                    Chart_Torque_Rate.ChartAreas(0).AxisX.Interval = 1
                    Chart_Torque_Rate.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Torque Normal Distribution")
                    Chart_Torque_Rate.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Count")
                    Chart_Torque_Rate.ChartAreas(0).AxisX.Interval = 1
                    Chart_Torque_Rate.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    Chart_Torque_Rate.ChartAreas(0).RecalculateAxesScale()
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Private Sub ChartTorqueDefaultValue()
        HmiTextBox_Torque_MaxValue.TextBox.Text = "0.00" + strTorqueUnit
        HmiTextBox_Torque_MinValue.TextBox.Text = "0.00" + strTorqueUnit
        HmiTextBox_Torque_AvgValue.TextBox.Text = "0.00" + strTorqueUnit
        HmiTextBox_Torque_UpLimit.TextBox.Text = "0.00" + strTorqueUnit
        HmiTextBox_Torque_LowLimit.TextBox.Text = "0.00" + strTorqueUnit
        HmiTextBox_Torque_Std.TextBox.Text = "0.00"
        HmiTextBox_Torque_Cp.TextBox.Text = "0.00"
        HmiTextBox_Torque_Cpk.TextBox.Text = "0.00"
        HmiTextBox_Torque_Total.TextBox.Text = "0"
        HmiTextBox_Torque_Pass.TextBox.Text = "0"
        HmiTextBox_Torque_Fail.TextBox.Text = "0"
        HmiTextBox_Torque_Rate.TextBox.Text = "0.00" + strPercent
        Chart_Torque_Value.Series().Clear()
        Dim TorqueValue As Series = New Series("TorqueValue")
        TorqueValue.ChartType = SeriesChartType.Line
        TorqueValue.BorderWidth = 1
        TorqueValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueValue")

        Dim TorqueLowValue As Series = New Series("TorqueLowValue")
        TorqueLowValue.ChartType = SeriesChartType.Line
        TorqueLowValue.BorderWidth = 2
        TorqueLowValue.BorderColor = Color.Red
        TorqueLowValue.ToolTip = "0.00"
        TorqueLowValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueLowValue")

        Dim TorqueUpValue As Series = New Series("TorqueUpValue")
        TorqueUpValue.ChartType = SeriesChartType.Line
        TorqueUpValue.BorderWidth = 2
        TorqueUpValue.BorderColor = Color.Maroon
        TorqueUpValue.ToolTip = "0.00"
        TorqueUpValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueUpValue")

        For i = 50 To 0 Step -1
            TorqueValue.Points.AddXY(i, 0)
            TorqueLowValue.Points.AddXY(i, 0)
            TorqueUpValue.Points.AddXY(i, 0)
        Next
        Chart_Torque_Value.Series.Add(TorqueLowValue)

        Chart_Torque_Value.Series.Add(TorqueValue)
        Chart_Torque_Value.Series.Add(TorqueUpValue)
        '  Chart_Torque_Value.ChartAreas(0).AxisX.Interval = 1
        Chart_Torque_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Torque Distribution")
        Chart_Torque_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Value")
        Chart_Torque_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Torque_Value.ChartAreas(0).RecalculateAxesScale()

        Chart_Torque_Rate.Series().Clear()
        Dim iCnt As Integer = 0
        Dim TorqueData As Series = New Series("TorqueCount")
        TorqueData.ChartType = SeriesChartType.Column
        TorqueData.BorderWidth = 25
        TorqueData.ShadowOffset = 0
        TorqueData.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "TorqueCount")
        Chart_Torque_Rate.Series.Add(TorqueData)

        For i = 1 To 7
            TorqueData.Points.AddY(0)
            TorqueData.Points(i - 1).AxisLabel = (i - 1).ToString + "--" + i.ToString
            TorqueData.IsValueShownAsLabel = True
        Next

        Chart_Torque_Rate.ChartAreas(0).AxisX.Interval = 1
        Chart_Torque_Rate.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Torque Normal Distribution")
        Chart_Torque_Rate.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Count")
        Chart_Torque_Rate.ChartAreas(0).AxisX.Interval = 1
        Chart_Torque_Rate.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Torque_Rate.ChartAreas(0).RecalculateAxesScale()
    End Sub

    Private Sub ChartAngle(ByVal dLowLimtValue As Double, ByVal dUpLimtValue As Double)
        Try
            If HmiComboBox_Station.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_Device.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_AST.ComboBox.SelectedIndex = -1 Then Return
            If HmiComboBox_Program.ComboBox.SelectedIndex = -1 Then Return
            Dim x As New List(Of Double)
            Dim dLowValue As Double = Double.MaxValue
            Dim dUpValue As Double = Double.MinValue
            Dim iLowIndex As Integer = 0
            Dim iUpIndex As Integer = 0
            Dim iValueIndex As Integer = 0
            If RadioButton_Angle_Step1.Checked Then
                iLowIndex = 19
                iUpIndex = 22
                iValueIndex = 21
            End If

            If RadioButton_Angle_Step2.Checked Then
                iLowIndex = 28
                iUpIndex = 31
                iValueIndex = 30
            End If

            If RadioButton_Angle_Step3.Checked Then
                iLowIndex = 37
                iUpIndex = 40
                iValueIndex = 39
            End If

            For Each mDr As DataRow In cScrewDataManager.Ds_Data.Tables(0).Rows
                If CDbl(mDr(iLowIndex).ToString()) < dLowValue Then
                    dLowValue = CDbl(mDr(iLowIndex).ToString())
                End If
                If CDbl(mDr(iUpIndex).ToString()) > dUpValue Then
                    dUpValue = CDbl(mDr(iUpIndex).ToString())
                End If
                x.Add(CDbl(mDr(iValueIndex).ToString()))
            Next
            If x.Count <= 0 Then
                Return
            End If
            If dLowLimtValue <> 0 Then dLowValue = dLowLimtValue
            If dUpLimtValue <> 0 Then dUpValue = dUpLimtValue
            If dUpValue < dLowValue Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "5"))
            End If

            If x.Count >= 0 Then
                Dim cCPK As clsCPK = New clsCPK(x, dLowValue, dUpValue)
                If cCPK.FindValue() Then
                    HmiTextBox_Angle_MaxValue.TextBox.Text = cCPK.Max.ToString + strAngleUnit
                    HmiTextBox_Angle_MinValue.TextBox.Text = cCPK.Min.ToString + strAngleUnit
                    HmiTextBox_Angle_AvgValue.TextBox.Text = cCPK.Mean.ToString + strAngleUnit
                    HmiTextBox_Angle_UpLimit.TextBox.Text = dUpValue.ToString + strAngleUnit
                    HmiTextBox_Angle_LowLimit.TextBox.Text = dLowValue.ToString + strAngleUnit
                    HmiTextBox_Angle_Std.TextBox.Text = cCPK.Std.ToString
                    HmiTextBox_Angle_Cp.TextBox.Text = cCPK.Cp.ToString
                    HmiTextBox_Angle_Cpk.TextBox.Text = cCPK.Cpk.ToString
                    HmiTextBox_Angle_Total.TextBox.Text = cCPK.TotalCount.ToString
                    HmiTextBox_Angle_Pass.TextBox.Text = cCPK.PassCount.ToString
                    HmiTextBox_Angle_Fail.TextBox.Text = cCPK.FailCount.ToString
                    HmiTextBox_Angle_Rate.TextBox.Text = cCPK.FailRate.ToString + strPercent

                    Chart_Angle_Value.Series().Clear()
                    Dim AngleValue As Series = New Series("AngleValue")
                    AngleValue.ChartType = SeriesChartType.Line
                    AngleValue.BorderWidth = 1
                    AngleValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleValue")

                    Dim AngleLowValue As Series = New Series("AngleLowValue")
                    AngleLowValue.ChartType = SeriesChartType.Line
                    AngleLowValue.BorderWidth = 2
                    AngleLowValue.BorderColor = Color.Red
                    AngleLowValue.ToolTip = dLowValue.ToString
                    AngleLowValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleLowValue")

                    Dim AngleUpValue As Series = New Series("AngleUpValue")
                    AngleUpValue.ChartType = SeriesChartType.Line
                    AngleUpValue.BorderWidth = 2
                    AngleUpValue.BorderColor = Color.Maroon
                    AngleUpValue.ToolTip = dUpValue.ToString
                    AngleUpValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleUpValue")

                    For i = cScrewDataManager.Ds_Data.Tables(0).Rows.Count - 1 To 0 Step -1
                        AngleValue.Points.AddXY(i, cScrewDataManager.Ds_Data.Tables(0).Rows(i)(iValueIndex))
                        AngleLowValue.Points.AddXY(i, dLowValue)
                        AngleUpValue.Points.AddXY(i, dUpValue)
                    Next
                    Chart_Angle_Value.Series.Add(AngleLowValue)

                    Chart_Angle_Value.Series.Add(AngleValue)
                    Chart_Angle_Value.Series.Add(AngleUpValue)
                    '  Chart_Angle_Value.ChartAreas(0).AxisX.Interval = 1
                    Chart_Angle_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Angle Distribution")
                    Chart_Angle_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Value")
                    Chart_Angle_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    Chart_Angle_Value.ChartAreas(0).RecalculateAxesScale()

                    Chart_Angle_Rate.Series().Clear()
                    Dim mTempListRateGroupCfg As List(Of clsRateGroupCfg) = cCPK.FindRateGroup(7)
                    Dim iCnt As Integer = 0
                    Dim AngleData As Series = New Series("AngleCount")
                    AngleData.ChartType = SeriesChartType.Column
                    AngleData.BorderWidth = 25
                    AngleData.ShadowOffset = 0
                    AngleData.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleData")
                    Chart_Angle_Rate.Series.Add(AngleData)


                    For Each element As clsRateGroupCfg In mTempListRateGroupCfg
                        AngleData.Points.AddY(element.Count)
                        AngleData.Points(iCnt).AxisLabel = element.LowValue.ToString + "--" + element.UpValue.ToString
                        AngleData.IsValueShownAsLabel = True
                        iCnt = iCnt + 1
                    Next

                    Chart_Angle_Rate.ChartAreas(0).AxisX.Interval = 1
                    Chart_Angle_Rate.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Angle Normal Distribution")
                    Chart_Angle_Rate.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Count")
                    Chart_Angle_Rate.ChartAreas(0).AxisX.Interval = 1
                    Chart_Angle_Rate.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    Chart_Angle_Rate.ChartAreas(0).RecalculateAxesScale()
                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Private Sub ChartAngleDefaultValue()
        HmiTextBox_Angle_MaxValue.TextBox.Text = "0.00" + strAngleUnit
        HmiTextBox_Angle_MinValue.TextBox.Text = "0.00" + strAngleUnit
        HmiTextBox_Angle_AvgValue.TextBox.Text = "0.00" + strAngleUnit
        HmiTextBox_Angle_UpLimit.TextBox.Text = "0.00" + strAngleUnit
        HmiTextBox_Angle_LowLimit.TextBox.Text = "0.00" + strAngleUnit
        HmiTextBox_Angle_Std.TextBox.Text = "0.00"
        HmiTextBox_Angle_Cp.TextBox.Text = "0.00"
        HmiTextBox_Angle_Cpk.TextBox.Text = "0.00"
        HmiTextBox_Angle_Total.TextBox.Text = "0"
        HmiTextBox_Angle_Pass.TextBox.Text = "0"
        HmiTextBox_Angle_Fail.TextBox.Text = "0"
        HmiTextBox_Angle_Rate.TextBox.Text = "0.00" + strPercent
        Chart_Angle_Value.Series().Clear()
        Dim AngleValue As Series = New Series("AngleValue")
        AngleValue.ChartType = SeriesChartType.Line
        AngleValue.BorderWidth = 1
        AngleValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleValue")

        Dim AngleLowValue As Series = New Series("AngleLowValue")
        AngleLowValue.ChartType = SeriesChartType.Line
        AngleLowValue.BorderWidth = 2
        AngleLowValue.BorderColor = Color.Red
        AngleLowValue.ToolTip = "0.00"
        AngleLowValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleLowValue")

        Dim AngleUpValue As Series = New Series("AngleUpValue")
        AngleUpValue.ChartType = SeriesChartType.Line
        AngleUpValue.BorderWidth = 2
        AngleUpValue.BorderColor = Color.Maroon
        AngleUpValue.ToolTip = "0.00"
        AngleUpValue.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleUpValue")

        For i = 50 To 0 Step -1
            AngleValue.Points.AddXY(i, 0)
            AngleLowValue.Points.AddXY(i, 0)
            AngleUpValue.Points.AddXY(i, 0)
        Next
        Chart_Angle_Value.Series.Add(AngleLowValue)

        Chart_Angle_Value.Series.Add(AngleValue)
        Chart_Angle_Value.Series.Add(AngleUpValue)
        '  Chart_Angle_Value.ChartAreas(0).AxisX.Interval = 1
        Chart_Angle_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Angle Distribution")
        Chart_Angle_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Value")
        Chart_Angle_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Angle_Value.ChartAreas(0).RecalculateAxesScale()

        Chart_Angle_Rate.Series().Clear()
        Dim iCnt As Integer = 0
        Dim AngleData As Series = New Series("AngleCount")
        AngleData.ChartType = SeriesChartType.Column
        AngleData.BorderWidth = 25
        AngleData.ShadowOffset = 0
        AngleData.Name = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "AngleData")
        Chart_Angle_Rate.Series.Add(AngleData)

        For i = 1 To 7
            AngleData.Points.AddY(0)
            AngleData.Points(i - 1).AxisLabel = (i - 1).ToString + "--" + i.ToString
            AngleData.IsValueShownAsLabel = True
        Next


        Chart_Angle_Rate.ChartAreas(0).AxisX.Interval = 1
        Chart_Angle_Rate.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Angle Normal Distribution")
        Chart_Angle_Rate.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenScrewForm.ToString, "Count")
        Chart_Angle_Rate.ChartAreas(0).AxisX.Interval = 1
        Chart_Angle_Rate.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Angle_Rate.ChartAreas(0).RecalculateAxesScale()
    End Sub
    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|*.csv"
        SaveFileDialogcsv.FilterIndex = 1
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cScrewDataManager.Ds_Data)
        End If
    End Sub


    Private Sub HmiDataView_Data_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles HmiDataView_Data.CellMouseClick
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
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenScrewForm.ToString)
        Me.Dispose()
        Return True
    End Function

    Private Sub RadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mMainForm.InvokeAction(Sub()
                                   HmiButton_Search.Button.Enabled = False
                                   HmiButton_Cancel.Button.Enabled = False
                                   cScrewDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                      HmiDateTime_Start.DateTimeToString,
                                                                      HmiDateTime_End.DateTimeToString,
                                                                      HmiComboBox_Station.ComboBox.Text,
                                                                      HmiComboBox_Variant.ComboBox.Text,
                                                                      HmiComboBox_Result.ComboBox.Text,
                                                                      HmiTextBox_SFC.TextBox.Text,
                                                                      HmiTextBox_PartNumber.TextBox.Text,
                                                                      HmiComboBox_Seq.ComboBox.Text,
                                                                      HmiComboBox_Device.ComboBox.Text,
                                                                      HmiComboBox_AST.ComboBox.Text,
                                                                      HmiComboBox_Program.ComboBox.Text
                                                                      )
                                   ShowChart()
                                   HmiButton_Cancel.Button.Enabled = True
                                   HmiButton_Search.Button.Enabled = True
                               End Sub)
    End Sub

End Class