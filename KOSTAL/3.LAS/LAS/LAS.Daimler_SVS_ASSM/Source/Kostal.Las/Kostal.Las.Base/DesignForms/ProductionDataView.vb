Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kostal.Las.Base
Public Class ProductionDataView
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cDataGridViewPage As clsDataGridViewPage_Bosh
    Private cLanguageManager As Language
    Private strButtonName As String
    Private _FileHandler As New FileHandler
    Private _XmlHandler As New XmlHandler
    Private AppSettings As Settings
    Private cProductionManager As clsProductionManager
    Private cHMIDate_Start As HMIDateTime
    Private cHMIDate_End As HMIDateTime
    Private cThread As Thread
    Private mMainForm As Form
    Private AppArticle As Article
    Private lListDate As New List(Of clsDataCfg)
    Private lListWTDate As New List(Of clsDataCfg)
    Private lListProductionData As New Dictionary(Of String, clsProductionCfg)
    Private lListWTData As New Dictionary(Of String, clsProductionCfg)
    Private _Shift As Shift
    Private iMaxCarrier As Integer = 0
    Public isShow As Boolean = True
    Private cCsvHandler As New clsCsvHandler
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property

    Private Sub ProductionDataView_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        Try
            Me.cSystemElement = Devices
            Me.cLocalElement = cLocalElement
            AppSettings = _AppSettings
            cLanguageManager = CType(Devices(Language.Name), Language)
            mMainForm = Devices("mMainForm")
            AppArticle = CType(Devices(Article.Name), Article)
            cProductionManager = New clsProductionManager()
            cProductionManager.Init(Devices, Stations, AppSettings)
            _Shift = New Shift(AppSettings.ConfigFolder + "lkshift.ini")

            cDataGridViewPage = New clsDataGridViewPage_Bosh
            cDataGridViewPage.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage.RowsPerPage = 15
            cProductionManager.RegisterManager(cDataGridViewPage, HmiDataView_Data)
            cHMIDate_Start = New HMIDateTime
            cHMIDate_End = New HMIDateTime
            TableLayoutPanel_Body_Head.Controls.Add(cHMIDate_Start, 1, 0)
            cHMIDate_Start.Dock = DockStyle.Fill
            cHMIDate_Start.Margin = New Padding(2)
            TableLayoutPanel_Body_Head.Controls.Add(cHMIDate_End, 3, 0)
            cHMIDate_End.Dock = DockStyle.Fill
            cHMIDate_End.Margin = New Padding(2)
            InitForm()
            InitControlText()
            Me.Height = My.Computer.Screen.WorkingArea.Height * 0.8
            Me.Width = My.Computer.Screen.WorkingArea.Width * 0.8
            Me.Left = CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
            Me.Top = CInt((My.Computer.Screen.WorkingArea.Height / 2) - (Me.Height / 2))
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        If AppSettings.LineType = enumLineType.MultiLine Then TopLevel = False

        HmiComboBox_Variant.ComboBox.Items.Clear()
        HmiComboBox_Variant.ComboBox.Items.Add("")
        For Each element As ArticleListElement In AppArticle.ArticleListElement.Values
            HmiComboBox_Variant.ComboBox.Items.Add(element.ID)
        Next
        Dim sResult As String = _XmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "WtStatus")
        HmiComboBox_CarrierId.ComboBox.Items.Clear()
        If sResult = "" Then sResult = "0"
        HmiComboBox_CarrierId.ComboBox.Items.Add("")
        For i = 1 To CInt(sResult)
            HmiComboBox_CarrierId.ComboBox.Items.Add(i.ToString)
        Next
        iMaxCarrier = CInt(sResult)
        HmiComboBox_Result.ComboBox.Items.Clear()
        HmiComboBox_Result.ComboBox.Items.Add("")
        HmiComboBox_Result.ComboBox.Items.Add("PASS")
        HmiComboBox_Result.ComboBox.Items.Add("FAIL")


        sResult = _XmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ConfigName, "GeneralInformation", "MaxStation")
        HmiComboBox_ErrorStation.ComboBox.Items.Clear()
        If sResult = "" Then sResult = "0"
        HmiComboBox_ErrorStation.ComboBox.Items.Add("")
        For i = 1 To CInt(sResult)
            HmiComboBox_ErrorStation.ComboBox.Items.Add(i.ToString)
        Next

        Timer1.Enabled = True
        Return True
    End Function


    Public Function InitControlText() As Boolean
        HmiLabel_StartDate.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_StartDate")
        HmiLabel_StartDate.Label.Font = New System.Drawing.Font("Calibri", 8.0!)

        HmiLabel_ErrorStation.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_ErrorStation")
        HmiLabel_ErrorStation.Label.Font = New System.Drawing.Font("Calibri", 8.0!)

        HmiLabel_EndDate.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_EndDate")
        HmiLabel_EndDate.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiButton_Search.Button.Text = cLanguageManager.Read("ProductionDataView", "HmiButton_Search")
        HmiButton_Search.Button.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiButton_Cancel.Button.Text = cLanguageManager.Read("ProductionDataView", "HmiButton_Cancel")
        HmiButton_Cancel.Button.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiButton_Export.Button.Text = cLanguageManager.Read("ProductionDataView", "HmiButton_Export")
        HmiButton_Export.Button.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiLabel_Variant.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_Variant")
        HmiLabel_Variant.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiComboBox_Variant.ComboBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiLabel_CarrierId.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_CarrierId")
        HmiLabel_CarrierId.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiComboBox_CarrierId.ComboBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiLabel_Result.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_Result")
        HmiLabel_Result.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiComboBox_Result.ComboBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiComboBox_ErrorStation.ComboBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiLabel_SFC.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_SFC")
        HmiLabel_SFC.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiTextBox_SFC.TextBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiLabel_TestStep.Label.Text = cLanguageManager.Read("ProductionDataView", "HmiLabel_TestStep")
        HmiLabel_TestStep.Label.Font = New System.Drawing.Font("Calibri", 8.0!)
        HmiTextBox_TestStep.TextBox.Font = New System.Drawing.Font("Calibri", 8.0!)
        TabControl_Data.Font = New System.Drawing.Font("Calibri", 8.0!)
        TabPage1.Text = cLanguageManager.Read("ProductionDataView", "TabPage1")
        TabPage2.Text = cLanguageManager.Read("ProductionDataView", "TabPage2")
        TabPage3.Text = cLanguageManager.Read("ProductionDataView", "TabPage3")
        cHMIDate_Start.DateTimeToString = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd") + " 00:00:00"
        cHMIDate_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
        RadioButton_Statistics_ByDay.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_Statistics_ByShift.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_WT_ByDay.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_WT_ByShift.Font = New System.Drawing.Font("Calibri", 10.0!)
        RadioButton_Statistics_ByDay.Checked = True
        RadioButton_WT_ByDay.Checked = True
        AddHandler HmiTextBox_SFC.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Export.Button.Click, AddressOf HmiButton_Click
        AddHandler RadioButton_Statistics_ByDay.Click, AddressOf RadioButton_Click
        AddHandler RadioButton_Statistics_ByShift.Click, AddressOf RadioButton_Click
        AddHandler RadioButton_WT_ByDay.Click, AddressOf RadioButton_Click
        AddHandler RadioButton_WT_ByShift.Click, AddressOf RadioButton_Click
        Return True
    End Function


    Private Sub HmiButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiButton_Search"
                    cThread = New Thread(AddressOf Search)
                    cThread.IsBackground = True
                    cThread.Start()

                Case "HmiButton_Cancel"
                    cHMIDate_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                    cHMIDate_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                    HmiComboBox_Variant.ComboBox.SelectedIndex = -1
                    HmiComboBox_CarrierId.ComboBox.SelectedIndex = -1
                    HmiComboBox_Result.ComboBox.SelectedIndex = -1
                    HmiComboBox_ErrorStation.ComboBox.SelectedIndex = -1
                    HmiTextBox_SFC.TextBox.Text = ""
                    HmiTextBox_TestStep.TextBox.Text = ""
                    cThread = New Thread(AddressOf Search)
                    cThread.IsBackground = True
                    cThread.Start()

                Case "HmiButton_Export"
                    Export()


            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.Invoke(Sub()
                                 HmiButton_Search.Button.Enabled = False
                                 HmiButton_Cancel.Button.Enabled = False
                                 HmiButton_Export.Button.Enabled = False
                                 cProductionManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                               cHMIDate_Start.DateTimeToString,
                                                                               cHMIDate_End.DateTimeToString,
                                                                               HmiComboBox_Variant.ComboBox.Text,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_CarrierId.ComboBox.Text,
                                                                               HmiComboBox_Result.ComboBox.Text,
                                                                               HmiTextBox_TestStep.TextBox.Text,
                                                                               HmiComboBox_ErrorStation.ComboBox.Text)
                                 GetDataList()
                                 ShowData()
                                 ChartData()
                                 GetWTDataList()
                                 ShowWTData()
                                 ChartWTData()
                                 HmiButton_Search.Button.Enabled = True
                                 HmiButton_Cancel.Button.Enabled = True
                                 HmiButton_Export.Button.Enabled = True
                             End Sub)
        Catch ex As Exception
            Return
        End Try
    End Sub

    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|*.csv"
        SaveFileDialogcsv.FilterIndex = 1
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cProductionManager.cDs_Data)
        End If
    End Sub
    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).SizeType = System.Windows.Forms.SizeType.Absolute
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_SFC.TextBox.Height + 6 + 10) * 3 + HmiTextBox_SFC.TextBox.Height + 12
            GroupBox_Search.Height = (HmiTextBox_SFC.TextBox.Height + 6 + 10) * 3 + HmiTextBox_SFC.TextBox.Height
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_SFC.TextBox.Height + 6 + 10
            Next
            If AppSettings.LineType = enumLineType.MultiLine Then
                RowMergeView_Statistics.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                RowMergeView_Statistics.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                RowMergeView_Statistics.RowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                RowMergeView_WT.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                RowMergeView_WT.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                RowMergeView_WT.RowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                HmiDataView_Data.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                HmiDataView_Data.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
                HmiDataView_Data.RowsDefaultCellStyle.Font = New Font("Calibri", HmiTextBox_SFC.TextBox.Font.Size)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetDataList()
        If RadioButton_Statistics_ByDay.Checked Then
            lListDate.Clear()
            For i = 0 To Integer.MaxValue
                Dim cDataCfg As New clsDataCfg
                If DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i) >= DateTime.Parse(cHMIDate_End.DateTimeToString) Then
                    Exit For
                Else
                    cDataCfg.strStartDate = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 00:00:00"
                    cDataCfg.strEndDate = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 23:59:59"
                    cDataCfg.ActiveTitle = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd")
                End If
                lListDate.Add(cDataCfg)
            Next
        Else
            lListDate.Clear()
            For i = -1 To Integer.MaxValue
                If DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i) >= DateTime.Parse(cHMIDate_End.DateTimeToString) Then
                    Exit For
                Else
                    For j = 1 To _Shift._ListOfShiftElementData.Count
                        Dim cDataCfg As New clsDataCfg
                        Dim NowDay As DateTime = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 00:00:00"

                        If j = 1 Then
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData((j + 1).ToString).ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListDate.Add(cDataCfg)
                        ElseIf j = _Shift._ListOfShiftElementData.Count Then
                            cDataCfg = New clsDataCfg
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.AddDays(1).ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData("1").ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListDate.Add(cDataCfg)
                        Else
                            cDataCfg = New clsDataCfg
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData((j + 1).ToString).ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListDate.Add(cDataCfg)

                        End If


                    Next

                End If
            Next
        End If

        lListProductionData.Clear()
        For Each element As ArticleListElement In AppArticle.ArticleListElement.Values
            If HmiComboBox_Variant.ComboBox.Text <> "" Then
                If element.ID <> HmiComboBox_Variant.ComboBox.Text Then
                    Continue For
                End If
            End If
            Dim cProductionCfg As New clsProductionCfg
            For Each elementDataCfg As clsDataCfg In lListDate
                Dim cProductionElementCfg As New clsProductionElementCfg
                cProductionElementCfg.cDataCfg = elementDataCfg
                cProductionCfg.ListData.Add(elementDataCfg.ActiveTitle(), cProductionElementCfg)
            Next
            lListProductionData.Add(element.ID, cProductionCfg)
        Next

        For i = 0 To cProductionManager.cDs_Data.Tables(0).Rows.Count - 1
            Dim strTitle As String = GetTitle(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(12).ToString)
            If strTitle <> "" AndAlso lListProductionData.ContainsKey(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString) Then
                If cProductionManager.cDs_Data.Tables(0).Rows(i).Item(4).ToString = "PASS" Then
                    lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).ListData(strTitle).iPassCount = lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).ListData(strTitle).iPassCount + 1
                    lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).cTotalCfg.iPassCount = lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).cTotalCfg.iPassCount + 1
                Else
                    lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).ListData(strTitle).iFailCount = lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).ListData(strTitle).iFailCount + 1
                    lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).cTotalCfg.iFailCount = lListProductionData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(1).ToString).cTotalCfg.iFailCount + 1
                End If
            End If
        Next

    End Sub
    Public Sub GetWTDataList()
        If RadioButton_WT_ByDay.Checked Then
            lListWTDate.Clear()
            For i = 0 To Integer.MaxValue
                Dim cDataCfg As New clsDataCfg
                If DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i) >= DateTime.Parse(cHMIDate_End.DateTimeToString) Then
                    Exit For
                Else
                    cDataCfg.strStartDate = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 00:00:00"
                    cDataCfg.strEndDate = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 23:59:59"
                    cDataCfg.ActiveTitle = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd")
                End If
                lListWTDate.Add(cDataCfg)
            Next
        Else
            lListWTDate.Clear()
            For i = -1 To Integer.MaxValue
                If DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i) >= DateTime.Parse(cHMIDate_End.DateTimeToString) Then
                    Exit For
                Else
                    For j = 1 To _Shift._ListOfShiftElementData.Count
                        Dim cDataCfg As New clsDataCfg
                        Dim NowDay As DateTime = DateTime.Parse(cHMIDate_Start.DateTimeToString).AddDays(i).ToString("yyyy-MM-dd") + " 00:00:00"

                        If j = 1 Then
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData((j + 1).ToString).ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListWTDate.Add(cDataCfg)
                        ElseIf j = _Shift._ListOfShiftElementData.Count Then
                            cDataCfg = New clsDataCfg
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.AddDays(1).ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData("1").ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListWTDate.Add(cDataCfg)
                        Else
                            cDataCfg = New clsDataCfg
                            cDataCfg.strStartDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData(j.ToString).ShiftData
                            cDataCfg.strEndDate = NowDay.ToString("yyyy-MM-dd") + " " + _Shift._ListOfShiftElementData((j + 1).ToString).ShiftData
                            cDataCfg.ActiveTitle = NowDay.ToString("yyyy-MM-dd") + " Shift" + j.ToString
                            lListWTDate.Add(cDataCfg)

                        End If


                    Next

                End If
            Next
        End If

        lListWTData.Clear()
        For i = 1 To iMaxCarrier
            If HmiComboBox_CarrierId.ComboBox.Text <> "" Then
                If i <> HmiComboBox_CarrierId.ComboBox.Text Then
                    Continue For
                End If
            End If
            Dim cProductionCfg As New clsProductionCfg
            For Each elementDataCfg As clsDataCfg In lListWTDate
                Dim cProductionElementCfg As New clsProductionElementCfg
                cProductionElementCfg.cDataCfg = elementDataCfg
                cProductionCfg.ListData.Add(elementDataCfg.ActiveTitle(), cProductionElementCfg)
            Next
            lListWTData.Add(i, cProductionCfg)
        Next

        For i = 0 To cProductionManager.cDs_Data.Tables(0).Rows.Count - 1
            Dim strTitle As String = GetWTTitle(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(12).ToString)
            If strTitle <> "" AndAlso lListWTData.ContainsKey(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString) Then
                If cProductionManager.cDs_Data.Tables(0).Rows(i).Item(4).ToString = "PASS" Then
                    lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).ListData(strTitle).iPassCount = lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).ListData(strTitle).iPassCount + 1
                    lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).cTotalCfg.iPassCount = lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).cTotalCfg.iPassCount + 1
                Else
                    lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).ListData(strTitle).iFailCount = lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).ListData(strTitle).iFailCount + 1
                    lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).cTotalCfg.iFailCount = lListWTData(cProductionManager.cDs_Data.Tables(0).Rows(i).Item(3).ToString).cTotalCfg.iFailCount + 1
                End If
            End If
        Next

    End Sub

    Public Function GetTitle(ByVal strTime As String) As String
        For Each elementDataCfg As clsDataCfg In lListDate
            If strTime >= elementDataCfg.strStartDate And strTime < elementDataCfg.strEndDate Then
                Return elementDataCfg.ActiveTitle()
            End If
        Next
        Return ""
    End Function

    Public Function GetWTTitle(ByVal strTime As String) As String
        For Each elementDataCfg As clsDataCfg In lListWTDate
            If strTime >= elementDataCfg.strStartDate And strTime < elementDataCfg.strEndDate Then
                Return elementDataCfg.ActiveTitle()
            End If
        Next
        Return ""
    End Function

    Public Sub ShowData()
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim iStartCheckSum As Integer = 0
            Dim iStopCheckSum As Integer = 0
            Dim iNameNum As Integer = 0
            Dim bRemove As Boolean = False
            Dim RowsDescription As New List(Of String)
            Dim RowsDescriptionData As New Dictionary(Of String, String)
            RowMergeView_Statistics.Columns.Clear()
            RowMergeView_Statistics.Rows.Clear()
            RowsDescription.Clear()
            RowsDescriptionData.Clear()
            RowMergeView_Statistics.ClearSpanInfo()
            RowMergeView_Statistics.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_Statistics.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_Statistics.RowsDefaultCellStyle.Font = New Font("Calibri", 10)



            '添加表头
            RowMergeView_Statistics.Columns.Add("ID", "ID")
            RowMergeView_Statistics.Columns.Add("Variant", "Variant")
            RowMergeView_Statistics.Columns.Add("Total", "Total")
            RowMergeView_Statistics.Columns.Add("Pass", "Pass")
            RowMergeView_Statistics.Columns.Add("Fail", "Fail")
            RowMergeView_Statistics.Columns.Add("Percent", "Percent")
            RowMergeView_Statistics.Columns.Add("PPM", "PPM")
            For Each element As clsDataCfg In lListDate
                RowMergeView_Statistics.Columns.Add(element.ActiveTitle() + "Total", "Total")
                RowMergeView_Statistics.Columns.Add(element.ActiveTitle() + "Pass", "Pass")
                RowMergeView_Statistics.Columns.Add(element.ActiveTitle() + "Fail", "Fail")
                RowMergeView_Statistics.Columns.Add(element.ActiveTitle() + "Percent", "Percent")
                RowMergeView_Statistics.Columns.Add(element.ActiveTitle() + "PPM", "PPM")
                RowsDescriptionData.Add(element.ActiveTitle() + "Total", element.ActiveTitle())
            Next


            For i = 0 To RowMergeView_Statistics.Columns.Count - 1
                RowMergeView_Statistics.Columns(i).HeaderText = cLanguageManager.Read("ProductionDataView", RowMergeView_Statistics.Columns(i).HeaderText)
                RowMergeView_Statistics.Columns(i).DataPropertyName = i.ToString
            Next

            '合并表头
            RowMergeView_Statistics.ColumnHeadersHeight = 40
            RowMergeView_Statistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            For i = 0 To RowMergeView_Statistics.Columns.Count - 1
                If RowMergeView_Statistics.Columns(i).Name.IndexOf("Total") > 0 Then
                    RowMergeView_Statistics.AddSpanHeader(CInt(RowMergeView_Statistics.Columns(i).DataPropertyName), 5, RowsDescriptionData(RowMergeView_Statistics.Columns(i).Name))
                End If
            Next

            Dim iCnt As Integer = 1
            For Each element As String In lListProductionData.Keys
                lListProductionData(element).cTotalCfg.iTotalCount = lListProductionData(element).cTotalCfg.iPassCount + lListProductionData(element).cTotalCfg.iFailCount
                If lListProductionData(element).cTotalCfg.iTotalCount = 0 Then
                    lListProductionData(element).cTotalCfg.dPercent = 0
                    lListProductionData(element).cTotalCfg.dPpm = 0
                Else
                    lListProductionData(element).cTotalCfg.dPercent = (lListProductionData(element).cTotalCfg.iFailCount / lListProductionData(element).cTotalCfg.iTotalCount * 1.0) * 100
                    lListProductionData(element).cTotalCfg.dPpm = (lListProductionData(element).cTotalCfg.iFailCount / lListProductionData(element).cTotalCfg.iTotalCount * 1.0) * 1000000
                End If

                Dim lListRow As New List(Of String)
                lListRow.Add(iCnt.ToString)
                lListRow.Add(element)
                lListRow.Add(lListProductionData(element).cTotalCfg.iTotalCount.ToString)
                lListRow.Add(lListProductionData(element).cTotalCfg.iPassCount.ToString)
                lListRow.Add(lListProductionData(element).cTotalCfg.iFailCount.ToString)
                lListRow.Add(lListProductionData(element).cTotalCfg.dPercent.ToString("0.00"))
                lListRow.Add(lListProductionData(element).cTotalCfg.dPpm.ToString("0"))
                For Each Subelement As clsProductionElementCfg In lListProductionData(element).ListData.Values
                    Subelement.iTotalCount = Subelement.iPassCount + Subelement.iFailCount
                    If Subelement.iTotalCount = 0 Then
                        Subelement.dPercent = 0
                        Subelement.dPpm = 0
                    Else
                        Subelement.dPercent = (Subelement.iFailCount / Subelement.iTotalCount * 1.0) * 100
                        Subelement.dPpm = (Subelement.iFailCount / Subelement.iTotalCount * 1.0) * 1000000
                    End If

                    lListRow.Add(Subelement.iTotalCount.ToString)
                    lListRow.Add(Subelement.iPassCount.ToString)
                    lListRow.Add(Subelement.iFailCount.ToString)
                    lListRow.Add(Subelement.dPercent.ToString("0.00"))
                    lListRow.Add(Subelement.dPpm.ToString("0"))
                Next
                RowMergeView_Statistics.Rows.Add(lListRow.ToArray)
            Next

            RowMergeView_Statistics.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_Statistics.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_Statistics.RowsDefaultCellStyle.Font = New Font("Calibri", 10)
            iCnt = iCnt + 1
        Catch ex As Exception
            Throw New Exception("Show Data Fail. Error Message:" + ex.Message.ToString)
        End Try
    End Sub


    Public Sub ShowWTData()
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim iStartCheckSum As Integer = 0
            Dim iStopCheckSum As Integer = 0
            Dim iNameNum As Integer = 0
            Dim bRemove As Boolean = False
            Dim RowsDescription As New List(Of String)
            Dim RowsDescriptionData As New Dictionary(Of String, String)
            RowMergeView_WT.Columns.Clear()
            RowMergeView_WT.Rows.Clear()
            RowsDescription.Clear()
            RowsDescriptionData.Clear()
            RowMergeView_WT.ClearSpanInfo()
            RowMergeView_WT.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_WT.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_WT.RowsDefaultCellStyle.Font = New Font("Calibri", 10)



            '添加表头
            RowMergeView_WT.Columns.Add("Carrier", "Carrier")
            RowMergeView_WT.Columns.Add("Total", "Total")
            RowMergeView_WT.Columns.Add("Pass", "Pass")
            RowMergeView_WT.Columns.Add("Fail", "Fail")
            RowMergeView_WT.Columns.Add("Percent", "Percent")
            RowMergeView_WT.Columns.Add("PPM", "PPM")
            For Each element As clsDataCfg In lListWTDate
                RowMergeView_WT.Columns.Add(element.ActiveTitle() + "Total", "Total")
                RowMergeView_WT.Columns.Add(element.ActiveTitle() + "Pass", "Pass")
                RowMergeView_WT.Columns.Add(element.ActiveTitle() + "Fail", "Fail")
                RowMergeView_WT.Columns.Add(element.ActiveTitle() + "Percent", "Percent")
                RowMergeView_WT.Columns.Add(element.ActiveTitle() + "PPM", "PPM")
                RowsDescriptionData.Add(element.ActiveTitle() + "Total", element.ActiveTitle())
            Next


            For i = 0 To RowMergeView_WT.Columns.Count - 1
                RowMergeView_WT.Columns(i).HeaderText = cLanguageManager.Read("ProductionDataView", RowMergeView_WT.Columns(i).HeaderText)
                RowMergeView_WT.Columns(i).DataPropertyName = i.ToString
            Next

            '合并表头
            RowMergeView_WT.ColumnHeadersHeight = 40
            RowMergeView_WT.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

            For i = 0 To RowMergeView_WT.Columns.Count - 1
                If RowMergeView_WT.Columns(i).Name.IndexOf("Total") > 0 Then
                    RowMergeView_WT.AddSpanHeader(CInt(RowMergeView_WT.Columns(i).DataPropertyName), 5, RowsDescriptionData(RowMergeView_WT.Columns(i).Name))
                End If
            Next

            For Each element As String In lListWTData.Keys
                lListWTData(element).cTotalCfg.iTotalCount = lListWTData(element).cTotalCfg.iPassCount + lListWTData(element).cTotalCfg.iFailCount
                If lListWTData(element).cTotalCfg.iTotalCount = 0 Then
                    lListWTData(element).cTotalCfg.dPercent = 0
                    lListWTData(element).cTotalCfg.dPpm = 0
                Else
                    lListWTData(element).cTotalCfg.dPercent = (lListWTData(element).cTotalCfg.iFailCount / lListWTData(element).cTotalCfg.iTotalCount * 1.0) * 100
                    lListWTData(element).cTotalCfg.dPpm = (lListWTData(element).cTotalCfg.iFailCount / lListWTData(element).cTotalCfg.iTotalCount * 1.0) * 1000000
                End If

                Dim lListRow As New List(Of String)
                lListRow.Add(element)
                lListRow.Add(lListWTData(element).cTotalCfg.iTotalCount.ToString)
                lListRow.Add(lListWTData(element).cTotalCfg.iPassCount.ToString)
                lListRow.Add(lListWTData(element).cTotalCfg.iFailCount.ToString)
                lListRow.Add(lListWTData(element).cTotalCfg.dPercent.ToString("0.00"))
                lListRow.Add(lListWTData(element).cTotalCfg.dPpm.ToString("0"))
                For Each Subelement As clsProductionElementCfg In lListWTData(element).ListData.Values
                    Subelement.iTotalCount = Subelement.iPassCount + Subelement.iFailCount
                    If Subelement.iTotalCount = 0 Then
                        Subelement.dPercent = 0
                        Subelement.dPpm = 0
                    Else
                        Subelement.dPercent = (Subelement.iFailCount / Subelement.iTotalCount * 1.0) * 100
                        Subelement.dPpm = (Subelement.iFailCount / Subelement.iTotalCount * 1.0) * 1000000
                    End If

                    lListRow.Add(Subelement.iTotalCount.ToString)
                    lListRow.Add(Subelement.iPassCount.ToString)
                    lListRow.Add(Subelement.iFailCount.ToString)
                    lListRow.Add(Subelement.dPercent.ToString("0.00"))
                    lListRow.Add(Subelement.dPpm.ToString("0"))
                Next
                RowMergeView_WT.Rows.Add(lListRow.ToArray)
            Next

            RowMergeView_WT.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_WT.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
            RowMergeView_WT.RowsDefaultCellStyle.Font = New Font("Calibri", 10)
        Catch ex As Exception
            Throw New Exception("Show Data Fail. Error Message:" + ex.Message.ToString)
        End Try
    End Sub
    Private Sub ChartData()
        Try
            Dim iShot As Integer = 0
            Dim dMin As Double = 0
            Dim dMax As Double = 0

            dMin = 0
            dMax = 100
            If lListProductionData.Count > 0 Then
                iShot = lListProductionData(lListProductionData.Keys(0)).ListData.Count
            Else
                iShot = 7
            End If

            Chart_Products_Value.Series().Clear()
            For Each element As String In lListProductionData.Keys
                Dim PercentValue As Series = New Series(element)
                PercentValue.ChartType = SeriesChartType.Line
                PercentValue.BorderWidth = 1
                PercentValue.Name = element
                PercentValue.MarkerBorderColor = Color.Blue
                PercentValue.MarkerBorderWidth = 3
                PercentValue.MarkerColor = Color.White
                PercentValue.MarkerSize = 8
                PercentValue.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
                PercentValue.Points.Clear()
                Dim i As Integer = 1
                For Each subelement As clsProductionElementCfg In lListProductionData(element).ListData.Values
                    PercentValue.Points.AddXY(i, subelement.dPercent.ToString("0.00"))
                    i = i + 1
                Next
                PercentValue.ToolTip = "Variant:" + element + " Percent:" + "#VAL"
                Chart_Products_Value.Series.Add(PercentValue)

            Next
            Chart_Products_Value.ChartAreas(0).AxisX.Interval = 1
            Chart_Products_Value.ChartAreas(0).AxisY.Interval = 10
            Chart_Products_Value.ChartAreas(0).AxisY.LabelStyle.Format = "N2"
            Chart_Products_Value.ChartAreas(0).AxisY.Maximum = dMax
            Chart_Products_Value.ChartAreas(0).AxisY.Minimum = dMin
            Chart_Products_Value.ChartAreas(0).AxisX.Minimum = 1
            Chart_Products_Value.ChartAreas(0).AxisX.Maximum = CInt(iShot)
            Chart_Products_Value.ChartAreas(0).AxisX.Title = "Day"
            Chart_Products_Value.ChartAreas(0).AxisY.Title = "Percent(%)"
            Chart_Products_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
            Chart_Products_Value.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ChartWTData()
        Try
            Dim iShot As Integer = 0
            Dim dMin As Double = 0
            Dim dMax As Double = 0

            dMin = 0
            dMax = 100
            If lListWTData.Count > 0 Then
                iShot = lListWTData(lListWTData.Keys(0)).ListData.Count
            Else
                iShot = 7
            End If

            Chart_WT_Value.Series().Clear()
            For Each element As String In lListWTData.Keys
                Dim PercentValue As Series = New Series(element)
                PercentValue.ChartType = SeriesChartType.Line
                PercentValue.BorderWidth = 1
                PercentValue.Name = element
                PercentValue.MarkerBorderColor = Color.Blue
                PercentValue.MarkerBorderWidth = 3
                PercentValue.MarkerColor = Color.White
                PercentValue.MarkerSize = 8
                PercentValue.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle
                PercentValue.Points.Clear()
                Dim i As Integer = 1
                For Each subelement As clsProductionElementCfg In lListWTData(element).ListData.Values
                    PercentValue.Points.AddXY(i, subelement.dPercent.ToString("0.00"))
                    i = i + 1
                Next
                PercentValue.ToolTip = "Carrier:" + element + " Percent:" + "#VAL"
                Chart_WT_Value.Series.Add(PercentValue)

            Next
            Chart_WT_Value.ChartAreas(0).AxisX.Interval = 1
            Chart_WT_Value.ChartAreas(0).AxisY.Interval = 10
            Chart_WT_Value.ChartAreas(0).AxisY.LabelStyle.Format = "N2"
            Chart_WT_Value.ChartAreas(0).AxisY.Maximum = dMax
            Chart_WT_Value.ChartAreas(0).AxisY.Minimum = dMin
            Chart_WT_Value.ChartAreas(0).AxisX.Minimum = 1
            Chart_WT_Value.ChartAreas(0).AxisX.Maximum = CInt(iShot)
            Chart_WT_Value.ChartAreas(0).AxisX.Title = "Day"
            Chart_WT_Value.ChartAreas(0).AxisY.Title = "Percent(%)"
            Chart_WT_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
            Chart_WT_Value.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub RadioButton_Click(sender As Object, e As EventArgs)
        cThread = New Thread(AddressOf Search)
        cThread.IsBackground = True
        cThread.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        cThread = New Thread(AddressOf Search)
        cThread.IsBackground = True
        cThread.Start()
        Timer1.Enabled = False
    End Sub

    Private Sub ProductionDataView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        RowMergeView_Statistics.Columns.Clear()
        RowMergeView_Statistics.Rows.Clear()
        Me.Dispose()
        isShow = False
    End Sub

    Public Function Quit(ByVal Devices As Dictionary(Of String, Object)) As Boolean
        RowMergeView_Statistics.Columns.Clear()
        RowMergeView_Statistics.Rows.Clear()
        Me.Dispose()
        isShow = False
        Return True
    End Function
End Class

Public Class clsDataCfg
    Public strStartDate As String = String.Empty
    Public strEndDate As String = String.Empty
    Private _ActiveTitle As String = String.Empty
    Public Property ActiveTitle As String
        Set(value As String)
            _ActiveTitle = value
        End Set
        Get
            Return _ActiveTitle
        End Get
    End Property
End Class

Public Class clsProductionCfg
    Public cTotalCfg As New clsProductionElementCfg
    Public ListData As New Dictionary(Of String, clsProductionElementCfg)
End Class
Public Class clsProductionElementCfg
    Public cDataCfg As clsDataCfg
    Public iTotalCount As Integer = 0
    Public iPassCount As Integer = 0
    Public iFailCount As Integer = 0
    Public dPercent As Double = 0
    Public dPpm As Double = 0
End Class