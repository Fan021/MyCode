Imports System.Windows.Forms.DataVisualization
Imports System.Windows.Forms.DataVisualization.Charting
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Threading
Imports Kochi.HMI.MainControl.LocalDevice

<clsChildrenUINameAttribute("Vision Logging", GetType(clsHMIInSpection))>
Public Class ChildrenVisionForm
    Implements IChildrenUI
    Private cLocalElement As Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cVisionDataManager As clsVisionDataManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDataGridViewPage_Data As clsDataGridViewPage
    Private cMachineManager As clsMachineManager
    Private cVariantManager As clsVariantManager
    Private cFormFontResize As clsFormFontResize
    Private cCsvHandler As New clsCsvHandler
    Private cFileHandler As New clsFileHandler
    Private cLanguageManager As clsLanguageManager
    Private strButtonName As String
    Private cThread As Thread
    Private mMainForm As IMainUI
    Private cUserManager As clsUserManager
    Private cDeviceManager As clsDeviceManager
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
            cDataGridViewPage_Data.RowsPerPage = 11
            cVisionDataManager = New clsVisionDataManager
            cVisionDataManager.Init(cSystemElement)
            cVisionDataManager.RegisterManager(cDataGridViewPage_Data, HmiDataView_Data)
            InitForm()
            InitControlText()
            cLocalElement.Add(enumUIName.ChildrenVisionForm.ToString, Me)
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
            HmiComboBox_Station.ComboBox.Items.Add(element.ID.ToString)
        Next
        HmiComboBox_Result.ComboBox.Items.Clear()
        HmiComboBox_Result.ComboBox.Items.Add("PASS")
        HmiComboBox_Result.ComboBox.Items.Add("FAIL")

        HmiComboBox_Variant.ComboBox.Items.Clear()
        For Each elementIndex As Integer In cVariantManager.GetVariantListKey
            Dim element As clsVariantCfg = cVariantManager.GetVariantCfgFromKey(elementIndex)
            HmiComboBox_Variant.ComboBox.Items.Add(element.Variant)
        Next

        For iCnt = 1 To 32
            HmiComboBox_Program.ComboBox.Items.Add(iCnt.ToString)
        Next

        Return True
    End Function

    Public Function InitControlText() As Boolean
        HmiButton_Search.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiButton_Search")
        HmiButton_Cancel.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiButton_Cancel")
        HmiLabel_StartDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_StartDate")
        HmiLabel_EndDate.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_EndDate")
        HmiLabel_SFC.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_SFC")
        HmiLabel_Variant.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_Variant")
        HmiLabel_Station.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_Station")
        HmiLabel_Device.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_Device")
        HmiLabel_Program.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_Program")
        HmiLabel_Result.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiLabel_Result")
        HmiButton_Export.Text = cLanguageManager.GetUserTextLine(enumUIName.ChildrenVisionForm.ToString, "HmiButton_Export")
        HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
        HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"

        HmiButton_Search.MarginHeight = 0
        HmiButton_Cancel.MarginHeight = 0
        HmiButton_Export.MarginHeight = 0
        cFormFontResize.SetControlFronts(8, GroupBox_Search)
        AddHandler HmiTextBox_SFC.TextBox.SizeChanged, AddressOf TextBox_SizeChanged
        AddHandler HmiButton_Search.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Cancel.Button.Click, AddressOf HmiButton_Click
        AddHandler HmiButton_Export.Button.Click, AddressOf HmiButton_Click
        AddHandler ContextMenuStrip_Function.Click, AddressOf HmiButton_Click
        AddHandler HmiComboBox_Station.ComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        cThread = New Thread(AddressOf Search)
        cThread.IsBackground = True
        cThread.Start()

        Return True
    End Function

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
                Case "HmiComboBox_Station"

                    HmiComboBox_Device.ComboBox.Items.Clear()
                    If HmiComboBox_Station.ComboBox.SelectedIndex = -1 Then Return
                    Dim lListDeviceCfg As List(Of clsDeviceCfg)
                    lListDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationID((HmiComboBox_Station.ComboBox.Text), GetType(clsHMIInSpection))
                    HmiComboBox_Device.ComboBox.Items.Clear()
                    If Not IsNothing(lListDeviceCfg) Then
                        For Each element As clsDeviceCfg In lListDeviceCfg
                            HmiComboBox_Device.ComboBox.Items.Add(element.StationIndex)
                        Next
                    End If

            End Select
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub TextBox_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            TableLayoutPanel_Body.RowStyles(0).Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 3 + HmiTextBox_SFC.TextBox.Height + 6 + 6
            GroupBox_Search.Height = (HmiTextBox_SFC.TextBox.Height + 6 + 6) * 3 + HmiTextBox_SFC.TextBox.Height + 6
            For Each element As RowStyle In TableLayoutPanel_Body_Head.RowStyles
                element.SizeType = System.Windows.Forms.SizeType.Absolute
                element.Height = HmiTextBox_SFC.TextBox.Height + 6 + 6
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
                                       cVisionDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                           HmiDateTime_Start.DateTimeToString,
                                                                           HmiDateTime_End.DateTimeToString,
                                                                           HmiComboBox_Station.ComboBox.Text,
                                                                           HmiComboBox_Variant.ComboBox.Text,
                                                                           HmiComboBox_Result.ComboBox.Text,
                                                                           HmiTextBox_SFC.TextBox.Text,
                                                                           HmiComboBox_Device.ComboBox.Text,
                                                                           HmiComboBox_Program.ComboBox.Text
                                                                           )
                                       DeletePic()
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
                                       HmiComboBox_Program.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Variant.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Station.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Result.ComboBox.SelectedIndex = -1
                                       HmiComboBox_Device.ComboBox.SelectedIndex = -1
                                       HmiDateTime_Start.DateTimeToString = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00"
                                       HmiDateTime_End.DateTimeToString = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"
                                       cVisionDataManager.SelectToDataView(enumViewPageType.FirstPage,
                                                                           HmiDateTime_Start.DateTimeToString,
                                                                           HmiDateTime_End.DateTimeToString,
                                                                           HmiComboBox_Station.ComboBox.Text,
                                                                           HmiComboBox_Variant.ComboBox.Text,
                                                                           HmiComboBox_Result.ComboBox.Text,
                                                                           HmiTextBox_SFC.TextBox.Text,
                                                                           HmiComboBox_Device.ComboBox.Text,
                                                                           HmiComboBox_Program.ComboBox.Text
                                                                           )
                                       DeletePic()
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
                                           cVisionDataManager.DeleteData(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(0).Value)
                                           cVisionDataManager.SelectToDataView(enumViewPageType.NoPage,
                                                                               HmiDateTime_Start.DateTimeToString,
                                                                               HmiDateTime_End.DateTimeToString,
                                                                               HmiComboBox_Station.ComboBox.Text,
                                                                               HmiComboBox_Variant.ComboBox.Text,
                                                                               HmiComboBox_Result.ComboBox.Text,
                                                                               HmiTextBox_SFC.TextBox.Text,
                                                                               HmiComboBox_Device.ComboBox.Text,
                                                                               HmiComboBox_Program.ComboBox.Text
                                                                               )
                                           DeletePic()
                                       End If
                                       HmiButton_Cancel.Button.Enabled = True
                                       HmiButton_Search.Button.Enabled = True
                                   End Sub)
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Public Sub Export()
        SaveFileDialogcsv.Filter = "*.csv|.csv"
        SaveFileDialogcsv.FilterIndex = 1
        If SaveFileDialogcsv.ShowDialog() = DialogResult.OK Then
            cCsvHandler.SaveData(SaveFileDialogcsv.FileName, cVisionDataManager.Ds_Data)
        End If
    End Sub

    Private Sub HmiDataView_Data_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HmiDataView_Data.CellClick
        If IsNothing(HmiDataView_Data.CurrentRow) Then Return
        If HmiDataView_Data.CurrentRow.Index <= HmiDataView_Data.Rows.Count - 1 Then
            ShowPic(HmiDataView_Data.Rows(HmiDataView_Data.CurrentRow.Index).Cells(6).Value)
        End If
    End Sub

    Private Sub ShowPic(ByVal strFilePath As String)
        Try
            If cFileHandler.FileExist(strFilePath) Then
                Dim img As Image = Image.FromFile(strFilePath)
                Dim bmp As Image = New Bitmap(img)
                img.Dispose()
                PictureBox_Data.Image = bmp
                PictureBox_Data.SizeMode = PictureBoxSizeMode.CenterImage
            Else
                PictureBox_Data.Image = Nothing
            End If
        Catch ex As Exception
            PictureBox_Data.Image = Nothing
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub

    Private Sub DeletePic()
        Try
            PictureBox_Data.Image = Nothing
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
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
        cLocalElement.Remove(enumUIName.ChildrenVisionForm.ToString)
        Me.Dispose()
        Return True
    End Function
End Class