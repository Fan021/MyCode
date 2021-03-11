Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Device
Imports System.Threading
Imports System.Windows.Forms
Imports System.Drawing
Imports Kochi.HMI.MainControl.LocalDevice

<clsChildrenUINameAttribute("GapFiller Logging", GetType(clsHMIGapFiller))>
Public Class ChildrenGapFillerForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cGapFillerDataManager As clsGapFillerDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cMachineManager As clsMachineManager
    Private cVariantManager As clsVariantManager
    Private cCsvHandler As New clsCsvHandler
    Private cFormFontResize As clsFormFontResize
    Private cLanguageManager As clsLanguageManager
    Protected Const strGapfillerUnit As String = " g"
    Protected Const strPercent As String = " %"
    Private strButtonName As String
    Private cThread As Thread
    Private mMainForm As IMainUI
    Private iSelectComponent As Integer = 0
    Private cUserManager As clsUserManager
    Public Property SelectComponent As Integer
        Set(ByVal value As Integer)
            iSelectComponent = value
        End Set
        Get
            Return iSelectComponent
        End Get
    End Property
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
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDataGridViewPage_Data = New clsDataGridViewPage
            cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage_Data.RowsPerPage = 13
            cGapFillerDataManager = New clsGapFillerDataManager
            cGapFillerDataManager.Init(cSystemElement)
            cGapFillerDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
            InitForm()
            InitControlText()
            If Not cLocalElement.ContainsKey(enumUIName.ChildrenGapFillerForm.ToString) Then
                cLocalElement.Add(enumUIName.ChildrenGapFillerForm.ToString, Me)
            End If
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False

        HmiComboBox_Component.ComboBox.Items.Clear()
        HmiComboBox_Component.ComboBox.Items.Add("A")
        HmiComboBox_Component.ComboBox.Items.Add("B")
        HmiComboBox_Component.ComboBox.Items.Add("AB")
        HmiComboBox_Component.ComboBox.SelectedIndex = iSelectComponent
        HmiComboBox_Result.ComboBox.Items.Clear()
        HmiComboBox_Result.ComboBox.Items.Add("PASS")
        HmiComboBox_Result.ComboBox.Items.Add("FAIL")

        HmiComboBox_Shot.ComboBox.Items.Clear()
        For i = 1 To 100
            HmiComboBox_Shot.ComboBox.Items.Add(i.ToString)
        Next
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_EndDate")
        HmiLabel_Component.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Component")
        HmiButton_Export.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiButton_Export")
        HmiLabel_Component.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Component")
        HmiLabel_Shot.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Shot")
        HmiLabel_Result.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Result")

        HmiLabel_Gapfiller_MaxValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_MaxValue")
        HmiLabel_Gapfiller_MaxValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_MinValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_MinValue")
        HmiLabel_Gapfiller_MinValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_AvgValue.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_AvgValue")
        HmiLabel_Gapfiller_AvgValue.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_LowLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_LowLimit")
        HmiLabel_Gapfiller_LowLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_UpLimit.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_UpLimit")
        HmiLabel_Gapfiller_UpLimit.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Std.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Std")
        HmiLabel_Gapfiller_Std.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Cp.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Cp")
        HmiLabel_Gapfiller_Cp.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Cpk.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Cpk")
        HmiLabel_Gapfiller_Cpk.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Total.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Total")
        HmiLabel_Gapfiller_Total.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Pass.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Pass")
        HmiLabel_Gapfiller_Pass.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Fail.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Fail")
        HmiLabel_Gapfiller_Fail.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_Gapfiller_Rate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "HmiLabel_Gapfiller_Rate")
        HmiLabel_Gapfiller_Rate.Label.TextAlign = ContentAlignment.MiddleRight

        HmiTextBox_Gapfiller_MaxValue.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_MinValue.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_AvgValue.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Std.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Cp.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Cpk.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Total.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Pass.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Fail.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_Rate.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_LowLimit.TextBoxReadOnly = True
        HmiTextBox_Gapfiller_UpLimit.TextBoxReadOnly = True



        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiComboBox_Component.ComboBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Export.Button.Click, AddressOf HmiButton_Click
        AddHandler ContextMenuStrip_Function.Click, AddressOf HmiButton_Click
        cThread = New Thread(AddressOf Search)
        cThread.Start()
        Return True
    End Function

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiComboBox_Component.ComboBox.Height + 6 + 6) * 2 + HmiComboBox_Component.ComboBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiComboBox_Component.ComboBox.Height + 6 + 6) * 2 + HmiComboBox_Component.ComboBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiComboBox_Component.ComboBox.Height + 6 + 6
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
                                       cGapFillerDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                          HmiDateTime_Start.DateTimeToString,
                                                                          HmiDateTime_End.DateTimeToString,
                                                                          HmiComboBox_Component.ComboBox.Text,
                                                                          HmiComboBox_Shot.ComboBox.Text,
                                                                          HmiComboBox_Result.ComboBox.Text
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
                                       HmiComboBox_Shot.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Component.ComboBox.SelectedIndex = iSelectComponent
                                       HmiComboBox_Result.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cGapFillerDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                          HmiDateTime_Start.DateTimeToString,
                                                                          HmiDateTime_End.DateTimeToString,
                                                                          HmiComboBox_Component.ComboBox.Text,
                                                                          HmiComboBox_Shot.ComboBox.Text,
                                                                          HmiComboBox_Result.ComboBox.Text
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
                                       If Not IsNothing(HmiDataView_Data.CurrentRow.Index) AndAlso HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
                                           cGapFillerDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cGapFillerDataManager.SelectToDataView(enumViewPageType.NoPage,
                                                                             HmiDateTime_Start.DateTimeToString,
                                                                             HmiDateTime_End.DateTimeToString,
                                                                             HmiComboBox_Component.ComboBox.Text,
                                                                             HmiComboBox_Shot.ComboBox.Text,
                                                                             HmiComboBox_Result.ComboBox.Text
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

    Public Sub ShowChart()
        ChartGapfillerDefaultValue()
        ChartGapfiller(0, 0)
    End Sub
    Private Sub ChartGapfiller(ByVal dLowLimtValue As Double, ByVal dUpLimtValue As Double)
        Try
            Dim x As New List(Of Double)
            Dim dLowValue As Double = Double.MaxValue
            Dim dUpValue As Double = Double.MinValue
            For Each mDr As DataRow In cGapFillerDataManager.Ds_Data.Tables(0).Rows
                If CDbl(mDr(3).ToString()) < dLowValue Then
                    dLowValue = CDbl(mDr(3).ToString())
                End If
                If CDbl(mDr(5).ToString()) > dUpValue Then
                    dUpValue = CDbl(mDr(5).ToString())
                End If
                x.Add(CDbl(mDr(4).ToString()))
            Next
            If x.Count <= 0 Then
                Return
            End If
            If dLowLimtValue <> 0 Then dLowValue = dLowLimtValue
            If dUpLimtValue <> 0 Then dUpValue = dUpLimtValue
            If dUpValue < dLowValue Then
                Throw New clsHMIException(cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "1"))
            End If

            If x.Count >= 0 Then
                Dim cCPK As clsCPK = New clsCPK(x, dLowValue, dUpValue)
                If cCPK.FindValue() Then
                    HmiTextBox_Gapfiller_MaxValue.TextBox.Text = cCPK.Max.ToString + strGapfillerUnit
                    HmiTextBox_Gapfiller_MinValue.TextBox.Text = cCPK.Min.ToString + strGapfillerUnit
                    HmiTextBox_Gapfiller_AvgValue.TextBox.Text = cCPK.Mean.ToString + strGapfillerUnit
                    HmiTextBox_Gapfiller_UpLimit.TextBox.Text = dUpValue.ToString + strGapfillerUnit
                    HmiTextBox_Gapfiller_LowLimit.TextBox.Text = dLowValue.ToString + strGapfillerUnit
                    HmiTextBox_Gapfiller_Std.TextBox.Text = cCPK.Std.ToString
                    HmiTextBox_Gapfiller_Cp.TextBox.Text = cCPK.Cp.ToString
                    HmiTextBox_Gapfiller_Cpk.TextBox.Text = cCPK.Cpk.ToString
                    HmiTextBox_Gapfiller_Total.TextBox.Text = cCPK.TotalCount.ToString
                    HmiTextBox_Gapfiller_Pass.TextBox.Text = cCPK.PassCount.ToString
                    HmiTextBox_Gapfiller_Fail.TextBox.Text = cCPK.FailCount.ToString
                    HmiTextBox_Gapfiller_Rate.TextBox.Text = cCPK.FailRate.ToString + strPercent

                    Chart_Gapfiller_Value.Series().Clear()
                    Dim GapfillerValue As Series = New Series("GapfillerValue")
                    GapfillerValue.ChartType = SeriesChartType.Line
                    GapfillerValue.BorderWidth = 1
                    GapfillerValue.Name = "GapfillerValue"
                    GapfillerValue.MarkerBorderColor = Color.Blue
                    GapfillerValue.MarkerBorderWidth = 3
                    GapfillerValue.MarkerColor = Color.White
                    GapfillerValue.MarkerSize = 5
                    GapfillerValue.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle

                    Dim GapfillerLowValue As Series = New Series("GapfillerLowValue")
                    GapfillerLowValue.ChartType = SeriesChartType.Line
                    GapfillerLowValue.BorderWidth = 2
                    GapfillerLowValue.BorderColor = Color.Red
                    GapfillerLowValue.ToolTip = dLowValue.ToString

                    Dim GapfillerUpValue As Series = New Series("GapfillerUpValue")
                    GapfillerUpValue.ChartType = SeriesChartType.Line
                    GapfillerUpValue.BorderWidth = 2
                    GapfillerUpValue.BorderColor = Color.Maroon
                    GapfillerUpValue.ToolTip = dUpValue.ToString



                    For i = cGapFillerDataManager.Ds_Data.Tables(0).Rows.Count - 1 To 0 Step -1
                        GapfillerValue.Points.AddXY(i, cGapFillerDataManager.Ds_Data.Tables(0).Rows(i)(4))
                        GapfillerLowValue.Points.AddXY(i, dLowValue)
                        GapfillerUpValue.Points.AddXY(i, dUpValue)
                    Next
                    Chart_Gapfiller_Value.Series.Add(GapfillerLowValue)

                    Chart_Gapfiller_Value.Series.Add(GapfillerValue)
                    Chart_Gapfiller_Value.Series.Add(GapfillerUpValue)

                    Chart_Gapfiller_Value.Series(1).ToolTip = "#VAL"
                    Chart_Gapfiller_Value.Series(0).ToolTip = "Low Limit:#VAL"
                    Chart_Gapfiller_Value.Series(2).ToolTip = "Up Limit:#VAL"

                    '  Chart_Gapfiller_Value.ChartAreas(0).AxisX.Interval = 1
                    Chart_Gapfiller_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "Gapfiller Distribution")
                    Chart_Gapfiller_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "Value")
                    Chart_Gapfiller_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
                    Chart_Gapfiller_Value.ChartAreas(0).RecalculateAxesScale()

                End If
            End If
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub
    Private Sub ChartGapfillerDefaultValue()
        HmiTextBox_Gapfiller_MaxValue.TextBox.Text = "0.00" + strGapfillerUnit
        HmiTextBox_Gapfiller_MinValue.TextBox.Text = "0.00" + strGapfillerUnit
        HmiTextBox_Gapfiller_AvgValue.TextBox.Text = "0.00" + strGapfillerUnit
        HmiTextBox_Gapfiller_UpLimit.TextBox.Text = "0.00" + strGapfillerUnit
        HmiTextBox_Gapfiller_LowLimit.TextBox.Text = "0.00" + strGapfillerUnit
        HmiTextBox_Gapfiller_Std.TextBox.Text = "0.00"
        HmiTextBox_Gapfiller_Cp.TextBox.Text = "0.00"
        HmiTextBox_Gapfiller_Cpk.TextBox.Text = "0.00"
        HmiTextBox_Gapfiller_Total.TextBox.Text = "0"
        HmiTextBox_Gapfiller_Pass.TextBox.Text = "0"
        HmiTextBox_Gapfiller_Fail.TextBox.Text = "0"
        HmiTextBox_Gapfiller_Rate.TextBox.Text = "0.00" + strPercent

        Chart_Gapfiller_Value.Series().Clear()
        Dim GapfillerValue As Series = New Series("GapfillerValue")
        GapfillerValue.ChartType = SeriesChartType.Line
        GapfillerValue.BorderWidth = 1
        GapfillerValue.Name = "GapfillerValue"

        Dim GapfillerLowValue As Series = New Series("GapfillerLowValue")
        GapfillerLowValue.ChartType = SeriesChartType.Line
        GapfillerLowValue.BorderWidth = 2
        GapfillerLowValue.BorderColor = Color.Red
        GapfillerLowValue.ToolTip = "0.00"

        Dim GapfillerUpValue As Series = New Series("GapfillerUpValue")
        GapfillerUpValue.ChartType = SeriesChartType.Line
        GapfillerUpValue.BorderWidth = 2
        GapfillerUpValue.BorderColor = Color.Maroon
        GapfillerUpValue.ToolTip = "0.00"

        For i = 50 To 0 Step -1
            GapfillerValue.Points.AddXY(i, 0)
            GapfillerLowValue.Points.AddXY(i, 0)
            GapfillerUpValue.Points.AddXY(i, 0)
        Next
        Chart_Gapfiller_Value.Series.Add(GapfillerLowValue)

        Chart_Gapfiller_Value.Series.Add(GapfillerValue)
        Chart_Gapfiller_Value.Series.Add(GapfillerUpValue)

        Chart_Gapfiller_Value.Series(1).ToolTip = "#VAL"
        Chart_Gapfiller_Value.Series(0).ToolTip = "Low Limit:#VAL"
        Chart_Gapfiller_Value.Series(2).ToolTip = "Up Limit:#VAL"

        '  Chart_Gapfiller_Value.ChartAreas(0).AxisX.Interval = 1
        Chart_Gapfiller_Value.ChartAreas(0).AxisX.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "Gapfiller Distribution")
        Chart_Gapfiller_Value.ChartAreas(0).AxisY.Title = cLanguageManager.GetUserTextLine(enumUIName.ChildrenGapFillerForm.ToString, "Value")
        Chart_Gapfiller_Value.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Gapfiller_Value.ChartAreas(0).RecalculateAxesScale()
    End Sub

    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|*.csv"
        SaveFileDialogcsv.FilterIndex = 1
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cGapFillerDataManager.Ds_Data)
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
        cLocalElement.Remove(enumUIName.ChildrenGapFillerForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class