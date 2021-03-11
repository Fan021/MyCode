Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Threading

Public Class ChildrenMachineForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cMachineDataManager As clsMachineDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cFormFontResize As clsFormFontResize
    Private cCsvHandler As New clsCsvHandler
    Protected Const strUnit As String = " h"
    Protected Const strPercent As String = " %"
    Private strButtonName As String
    Private cThread As Thread
    Private mMainForm As MainForm
    Private cLanguageManager As clsLanguageManager
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
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDataGridViewPage_Data = New clsDataGridViewPage
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage_Data.RowsPerPage = 13
            cMachineDataManager = New clsMachineDataManager
            cMachineDataManager.Init(cSystemElement)
            cMachineDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenMachineForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenMachineForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        HmiComboBox_Action.ComboBox.Items.Clear()
        HmiComboBox_Action.ComboBox.Items.Add(enumManchineActionType.PowerOn.ToString)
        HmiComboBox_Action.ComboBox.Items.Add(enumManchineActionType.Auto.ToString)
        HmiComboBox_Action.ComboBox.Items.Add(enumManchineActionType.Work.ToString)
        HmiComboBox_Action.ComboBox.Items.Add(enumManchineActionType.Waiting.ToString)
        HmiComboBox_Action.ComboBox.Items.Add(enumManchineActionType.Alarm.ToString)
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_EndDate")
        HmiLabel_Action.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_Action")
        HmiButton_Export.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiButton_Export")
        HmiLabel_TotalTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_TotalTime")
        HmiLabel_TotalTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_PowerOn.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_PowerOn")
        HmiLabel_PowerOn.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_PowerOnRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_PowerOnRate")
        HmiLabel_PowerOnRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_AutoTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_AutoTime")
        HmiLabel_AutoTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_AutoRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_AutoRate")
        HmiLabel_AutoRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_ManualTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_ManualTime")
        HmiLabel_ManualTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_ManualRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_ManualRate")
        HmiLabel_ManualRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WorkTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WorkTime")
        HmiLabel_WorkTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WorkTotalRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WorkTotalRate")
        HmiLabel_WorkTotalRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WorkRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WorkRate")
        HmiLabel_WorkRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WaitingTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WaitingTime")
        HmiLabel_WaitingTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WaitingTotalRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WaitingTotalRate")
        HmiLabel_WaitingTotalRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_WaitingRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_WaitingRate")
        HmiLabel_WaitingRate.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_AlarmTime.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_AlarmTime")
        HmiLabel_AlarmTime.Label.TextAlign = ContentAlignment.MiddleRight
        HmiLabel_AlarmRate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "HmiLabel_AlarmRate")
        HmiLabel_AlarmRate.Label.TextAlign = ContentAlignment.MiddleRight

        HmiTextBox_TotalTime.TextBoxReadOnly = True
        HmiTextBox_PowerOn.TextBoxReadOnly = True
        HmiTextBox_PowerOnRate.TextBoxReadOnly = True
        HmiTextBox_AutoTime.TextBoxReadOnly = True
        HmiTextBox_AutoRate.TextBoxReadOnly = True
        HmiTextBox_ManualTime.TextBoxReadOnly = True
        HmiTextBox_ManualRate.TextBoxReadOnly = True
        HmiTextBox_WorkTime.TextBoxReadOnly = True
        HmiTextBox_WorkTotalRate.TextBoxReadOnly = True
        HmiTextBox_WorkRate.TextBoxReadOnly = True
        HmiTextBox_WaitingTime.TextBoxReadOnly = True
        HmiTextBox_WaitingTotalRate.TextBoxReadOnly = True
        HmiTextBox_WaitingRate.TextBoxReadOnly = True
        HmiTextBox_AlarmTime.TextBoxReadOnly = True
        HmiTextBox_AlarmRate.TextBoxReadOnly = True

        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"


        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiComboBox_Action.ComboBox.SizeChanged, AddressOf ComboBox_SizeChanged
        AddHandler HmiTextBox_TotalTime.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
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

            TableLayoutPanel_Body_Mid_Analysis.RowStyles(0).Height = (HmiTextBox_TotalTime.TextBox.Height + 6 + 6) * 9
            For Each element As RowStyle In TableLayoutPanel_Body_Analysis_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_TotalTime.TextBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub

    Private Sub ComboBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            TableLayoutPanel_Body.RowStyles(0).Height = (HmiComboBox_Action.ComboBox.Height + 6 + 6) * 2 + HmiComboBox_Action.ComboBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiComboBox_Action.ComboBox.Height + 6 + 6) * 2 + HmiComboBox_Action.ComboBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiComboBox_Action.ComboBox.Height + 6 + 6
            Next
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cMachineDataManager.SelectToDataView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                       cMachineDataManager.SelectToAnayliseView(HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                       ShowChart()
                                       HmiButton_Search.Button.Enabled = True
                                       HmiButton_Cancel.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiComboBox_Action.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cMachineDataManager.SelectToDataView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                       cMachineDataManager.SelectToAnayliseView(HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                       ShowChart()
                                       HmiButton_Search.Button.Enabled = True
                                       HmiButton_Cancel.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub


    Public Sub Delete()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       If Not IsNothing(HmiDataView_Data.CurrentRow) AndAlso HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
                                           cMachineDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cMachineDataManager.SelectToDataView(enumViewPageType.NoPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                           cMachineDataManager.SelectToAnayliseView(HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiComboBox_Action.ComboBox.Text)
                                           ShowChart()
                                       End If
                                       HmiButton_Search.Button.Enabled = True
                                       HmiButton_Cancel.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub

    Public Sub ShowChart()
        Try
            Dim dData(7) As Double
            If HmiDateTime_Start.DateTimeToString > HmiDateTime_End.DateTimeToString Then
                dData(0) = 0
                dData(1) = 0
                dData(2) = 0
                dData(3) = 0
                dData(4) = 0
                dData(5) = 0
                dData(6) = 0
            Else
                Dim ts1 As TimeSpan = New TimeSpan(DateTime.Parse(HmiDateTime_Start.DateTimeToString).Ticks)
                Dim ts2 As TimeSpan = New TimeSpan(DateTime.Parse(HmiDateTime_End.DateTimeToString).Ticks)
                Dim ts As TimeSpan = ts2.Subtract(ts1).Duration()
                dData(0) = ts.TotalHours
                dData(1) = 0
                dData(2) = 0
                dData(3) = 0
                dData(4) = 0
                dData(5) = 0
                dData(6) = 0
            End If

            For Each mDr As DataRow In cMachineDataManager.Ds_Analysis.Tables(0).Rows
                dData(CInt([Enum].Parse(GetType(enumManchineActionType), mDr.Item(0)))) = CDbl(mDr.Item(2)) / 60.0
                If dData(CInt([Enum].Parse(GetType(enumManchineActionType), mDr.Item(0)))) <= 0 Then dData(CInt([Enum].Parse(GetType(enumManchineActionType), mDr.Item(0)))) = 0
            Next
            dData(3) = dData(1) - dData(2)
            dData(5) = dData(2) - dData(4)
            If dData(3) <= 0 Then dData(3) = 0
            If dData(5) <= 0 Then dData(5) = 0

            HmiTextBox_TotalTime.TextBox.Text = dData(0).ToString("F2") + " " + strUnit
            HmiTextBox_PowerOn.TextBox.Text = dData(1).ToString("F2") + " " + strUnit

            If dData(0) <= 0 Then
                HmiTextBox_PowerOnRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_AutoRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_ManualRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_WorkTotalRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_WaitingTotalRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_AlarmRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
            Else
                HmiTextBox_PowerOnRate.TextBox.Text = (dData(1) * 100 / dData(0)).ToString("F2") + " " + strPercent
                HmiTextBox_AutoRate.TextBox.Text = (dData(2) * 100 / dData(0)).ToString("F2") + " " + strPercent
                HmiTextBox_ManualRate.TextBox.Text = (dData(3) * 100 / dData(0)).ToString("F2") + " " + strPercent
                HmiTextBox_WorkTotalRate.TextBox.Text = (dData(4) * 100 / dData(0)).ToString("F2") + " " + strPercent
                HmiTextBox_WaitingTotalRate.TextBox.Text = (dData(5) * 100 / dData(0)).ToString("F2") + " " + strPercent
                HmiTextBox_AlarmRate.TextBox.Text = (dData(6) * 100 / dData(0)).ToString("F2") + " " + strPercent
            End If

            HmiTextBox_AutoTime.TextBox.Text = dData(2).ToString("F2") + " " + strUnit
            HmiTextBox_ManualTime.TextBox.Text = dData(3).ToString("F2") + " " + strUnit
            HmiTextBox_WorkTime.TextBox.Text = dData(4).ToString("F2") + " " + strUnit

            If dData(2) <= 0 Then
                HmiTextBox_WorkRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
                HmiTextBox_WaitingRate.TextBox.Text = (0.0).ToString("F2") + " " + strPercent
            Else
                HmiTextBox_WorkRate.TextBox.Text = (dData(4) * 100 / dData(2)).ToString("F2") + " " + strPercent
                HmiTextBox_WaitingRate.TextBox.Text = (dData(5) * 100 / dData(2)).ToString("F2") + " " + strPercent
            End If

            HmiTextBox_WaitingTime.TextBox.Text = dData(5).ToString("F2") + " " + strUnit
            HmiTextBox_AlarmTime.TextBox.Text = dData(6).ToString("F2") + " " + strUnit

            Dim yValues() As Double = {dData(1), dData(0) - dData(1)}
            Dim xValues() As String = {cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "PowerOn"), cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "PowerOff")}
            Dim Series_PowerOn As Series = New Series()
            Chart_PowerOn.Series().Clear()
            Series_PowerOn.Points.DataBindXY(xValues, yValues)
            Chart_PowerOn.Series.Add(Series_PowerOn)
            Chart_PowerOn.Series(0).ChartType = SeriesChartType.Pie
            Chart_PowerOn.Legends(0).Enabled = True
            Chart_PowerOn.Series(0).LegendText = "#VALX"
            Chart_PowerOn.Series(0).Label = "#PERCENT"
            Chart_PowerOn.Series(0).IsXValueIndexed = False
            Chart_PowerOn.Series(0).IsValueShownAsLabel = False
            Chart_PowerOn.Series(0)("PieLineColor") = "Black"
            Chart_PowerOn.Series(0)("PieLabelStyle") = "Inside"
            Chart_PowerOn.Series(0).ToolTip = "#VALX"
            Chart_PowerOn.ChartAreas(0).RecalculateAxesScale()

            Dim title As Title = New Title(cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "Power On /Total Time Rate(%)"))
            Chart_PowerOn.Titles().Clear()
            Chart_PowerOn.Titles.Add(title)
            Chart_PowerOn.ChartAreas(0).Area3DStyle.Enable3D = True
            Chart_PowerOn.ChartAreas(0).Area3DStyle.Rotation = 15
            Chart_PowerOn.ChartAreas(0).Area3DStyle.Inclination = 45
            Chart_PowerOn.ChartAreas(0).Area3DStyle.LightStyle = LightStyle.Realistic

            Dim yValues2() As Double = {dData(4), dData(5)}
            Dim xValues2() As String = {"Working", "Waiting"}
            Dim Series_Work As Series = New Series()
            Chart_Work.Series().Clear()
            Series_Work.Points.DataBindXY(xValues2, yValues2)
            Chart_Work.Series.Add(Series_Work)
            Chart_Work.Series(0).ChartType = SeriesChartType.Pie
            Chart_Work.Legends(0).Enabled = True
            Chart_Work.Series(0).LegendText = "#VALX"
            Chart_Work.Series(0).Label = "#PERCENT"
            Chart_Work.Series(0).IsXValueIndexed = False
            Chart_Work.Series(0).IsValueShownAsLabel = False
            Chart_Work.Series(0)("PieLineColor") = "Black"
            Chart_Work.Series(0)("PieLabelStyle") = "Inside"
            Chart_Work.Series(0).ToolTip = "#VALX"

            Dim title2 As Title = New Title(cLanguageManager.GetTextLine(enumUIName.ChildrenMachineForm.ToString, "Working / Auto  Rate(%)"))
            Chart_Work.Titles().Clear()
            Chart_Work.Titles.Add(title2)
            Chart_Work.ChartAreas(0).Area3DStyle.Enable3D = True
            Chart_Work.ChartAreas(0).Area3DStyle.Rotation = 15
            Chart_Work.ChartAreas(0).Area3DStyle.Inclination = 45
            Chart_Work.ChartAreas(0).Area3DStyle.LightStyle = LightStyle.Realistic
            Chart_Work.ChartAreas(0).RecalculateAxesScale()
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub
    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|*.csv"
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cMachineDataManager.Ds_Data)
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenMachineForm.ToString))
        End Try
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenMachineForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenMachineForm.ToString)
        Me.Dispose()
        Return True
    End Function

End Class