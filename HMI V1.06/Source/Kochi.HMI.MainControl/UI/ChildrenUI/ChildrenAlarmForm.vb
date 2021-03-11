Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Threading
Imports System.Collections.Concurrent

Public Class ChildrenAlarmForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cAlarmDataManager As clsAlarmDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cDataGridViewPage_Analysis As clsDataGridViewPage
    Private cFormFontResize As clsFormFontResize
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
            cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cDataGridViewPage_Data = New clsDataGridViewPage
            cDataGridViewPage_Data.RegisterManager(HmiDataView_Data, HmiDataViewPage_Data)
            cDataGridViewPage_Data.RowsPerPage = 13
            cDataGridViewPage_Analysis = New clsDataGridViewPage
            cDataGridViewPage_Analysis.RegisterManager(HmiDataView_Analysis, HmiDataViewPage_Analysis)
            cDataGridViewPage_Analysis.RowsPerPage = 7
            cAlarmDataManager = New clsAlarmDataManager
            cAlarmDataManager.Init(cSystemElement)
            cAlarmDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data, cDataGridViewPage_Analysis, HmiDataView_Analysis)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenAlarmForm.ToString, Me)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Crash, enumUIName.ChildrenAlarmForm.ToString))
            Return False
        End Try
    End Function

    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiLabel_EndDate")
        HmiLabel_Code.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiLabel_Code")
        HmiButton_Export.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiButton_Export")
        HmiLabel_Message.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "HmiLabel_Message")
        TabPage_Data.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "TabPage_Data")
        TabPage_Analysis.Text = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "TabPage_Analysis")
        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiTextBox_Code.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
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
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_Code.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Code.TextBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiTextBox_Code.TextBox.Height + 6 + 6) * 2 + HmiTextBox_Code.TextBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_Code.TextBox.Height + 6 + 6
            Next

        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
        End Try
    End Sub

    Public Sub Search()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       cAlarmDataManager.SelectToDataView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                       cAlarmDataManager.SelectToAnayliseView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                       ShowChart()
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
        End Try
    End Sub


    Public Sub Cancel()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       HmiTextBox_Code.TextBox.Text = ""
                                       HmiTextBox_Message.TextBox.Text = ""
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cAlarmDataManager.SelectToDataView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                       cAlarmDataManager.SelectToAnayliseView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                       ShowChart()
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
        End Try
    End Sub


    Public Sub Delete()
        Try
            mMainForm.InvokeAction(Sub()
                                       HmiButton_Search.Button.Enabled = False
                                       HmiButton_Cancel.Button.Enabled = False
                                       If Not IsNothing(HmiDataView_Data.CurrentRow) AndAlso HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
                                           cAlarmDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cAlarmDataManager.SelectToDataView(enumViewPageType.NoPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                           cAlarmDataManager.SelectToAnayliseView(enumViewPageType.FirstPage, HmiDateTime_Start.DateTimeToString, HmiDateTime_End.DateTimeToString, HmiTextBox_Code.TextBox.Text, HmiTextBox_Message.TextBox.Text)
                                           ShowChart()
                                       End If
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
        End Try
    End Sub

    Public Sub ShowChart()
        Chart_Alarm.Series().Clear()
        Dim iCnt As Integer = 1
        Dim AlarmData As Series = New Series("AlarmData" + iCnt.ToString)
        AlarmData.Points.Clear()
        AlarmData.ChartType = SeriesChartType.Column
        AlarmData.BorderWidth = 25
        AlarmData.ShadowOffset = 15
        AlarmData.Name = "AlarmData"
         AlarmData.IsValueShownAsLabel = True
        For Each mDr As DataRow In cAlarmDataManager.Ds_Analysis.Tables(0).Rows
            AlarmData.Points.AddY(mDr.Item(1))
            AlarmData.Points(iCnt - 1).AxisLabel = mDr.Item(0)
            AlarmData.Points(iCnt - 1).XValue = iCnt - 1
            iCnt = iCnt + 1
        Next
        For i = iCnt To 10
            AlarmData.Points.AddY(0)
            AlarmData.Points(iCnt - 1).AxisLabel = i.ToString
            AlarmData.Points(iCnt - 1).XValue = i - 1
            ' AlarmData.IsValueShownAsLabel = True
        Next

        Chart_Alarm.Series.Add(AlarmData)
        
        Chart_Alarm.ChartAreas(0).AxisX.Interval = 1
        Chart_Alarm.ChartAreas(0).AxisX.Title = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "AxisX.Title")
        Chart_Alarm.ChartAreas(0).AxisY.Title = cLanguageManager.GetTextLine(enumUIName.ChildrenAlarmForm.ToString, "AxisY.Title")
        'Chart_Alarm.ChartAreas(0).Area3DStyle.Enable3D = True
        'Chart_Alarm.ChartAreas(0).Area3DStyle.Rotation = 15
        'Chart_Alarm.ChartAreas(0).Area3DStyle.Inclination = 30
        'Chart_Alarm.ChartAreas(0).Area3DStyle.LightStyle = LightStyle.Realistic
        'Chart_Alarm.ChartAreas(0).AxisX.Interval = 1
        'Chart_Alarm.ChartAreas(0).AxisX.LabelStyle.Font = New Font("宋体", 9, FontStyle.Regular)
        'Chart_Alarm.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Alarm.ChartAreas(0).AxisX.Interval = 1
        Chart_Alarm.ChartAreas(0).AxisX.MajorGrid.Enabled = False
        Chart_Alarm.ChartAreas(0).RecalculateAxesScale()
    End Sub
    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|*.csv"
        SaveFileDialogcsv.FilterIndex = 1
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cAlarmDataManager.Ds_Data)
        End If
    End Sub

    Public Function Quit(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IChildrenUI.Quit
        cLocalElement.Remove(enumUIName.ChildrenAlarmForm.ToString)
        cErrorMessageManager.Clean(enumUIName.ChildrenAlarmForm.ToString)
        Me.Dispose()
        Return True
    End Function

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
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenAlarmForm.ToString))
        End Try
    End Sub
End Class