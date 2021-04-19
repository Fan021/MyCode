Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Xml
Imports Kostal.Las.Base

Public Class OverviewInfoForm
    Inherits System.Windows.Forms.Form
    Private _Language As Language
    Private _FileHandler As New FileHandler
    Private _ShowOverview As Boolean
    Private _mPassword As PassWordForm
    Private _i As New Station
    Private AppSettings As New Settings
    Private _xmlHandler As New XmlHandler
    Public Const sName As String = "OverviewInfoForm"
    Public iCnt As Integer = 0
    Private _TC As TwinCatAds
    Private _Devices As Dictionary(Of String, Object)
    'Private _PLCName As String
    Private listItem As New List(Of String)
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property
    Public Property ShowOverview As Boolean
        Set(value As Boolean)
            _ShowOverview = value
        End Set
        Get
            Return _ShowOverview
        End Get
    End Property


    Public Sub Run()
        If Not _ShowOverview Then
            If Me.Visible Then
                Me.Visible = False

                For Each element As PLCConfig In AppSettings.PLCConfig.Values
                    _TC = _Devices(element.Name)
                Next
                _TC.RemoveNotificationEx(KostalAdsVariables.PLC_arrOverviewInfo)
            End If
        Else
            If Not Me.Visible Then
                Me.Visible = True
                Me.Width = My.Computer.Screen.WorkingArea.Width - 50
                Me.Left = CInt((My.Computer.Screen.WorkingArea.Width / 2) - (Me.Width / 2))
                dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
                For i = 0 To dgView.Rows.Count - 1
                    Dim mPLCName As String = dgView.Rows(i).Cells(0).Tag
                    _TC = _Devices(mPLCName)
                    Dim cDefaultStationOverviewInfo() As structStationOverviewInfo_V1_00 = Enumerable.Repeat(New structStationOverviewInfo_V1_00, 50).ToArray()
                    _TC.AddAdsVariable(KostalAdsVariables.PLC_arrOverviewInfo)
                    _TC.AddNotificationEx(KostalAdsVariables.PLC_arrOverviewInfo, cDefaultStationOverviewInfo, New Integer() {50})
                Next
            End If

            If _ShowOverview Then
                UpdateRow()
            End If
        End If

    End Sub

    Public Function UpdateRow() As Boolean

        Try

            For i = 0 To dgView.Rows.Count - 1
                Dim j As Integer = 0
                Dim mPLCName As String = dgView.Rows(i).Cells(0).Tag
                Dim iPLCIndex As Integer = dgView.Rows(i).Cells(1).Tag

                _TC = _Devices(mPLCName)
                Dim stationInfo() As structStationOverviewInfo_V1_00 = _TC.GetDeviceNotificationEx(KostalAdsVariables.PLC_arrOverviewInfo)
                For Each item As String In listItem
                    If j = 0 Then
                        j = j + 1
                        Continue For
                    End If

                    Dim value As String = ""
                    If item = "bulAuto" Then
                        value = "--"
                        If stationInfo(iPLCIndex).bulAuto Then
                            value = "Auto"
                        End If
                        If stationInfo(iPLCIndex).bulManual Then
                            value = "Manual"
                        End If
                    ElseIf item = "iTotalNumber" Then
                        value = (stationInfo(iPLCIndex).iPassNumber + stationInfo(iPLCIndex).iFailNumber).ToString
                    ElseIf item = "iPercent" Then
                        Dim Ppm As Double = 0
                        If stationInfo(iPLCIndex).iPassNumber + stationInfo(iPLCIndex).iFailNumber = 0 Then
                            Ppm = 0
                        Else
                            Ppm = stationInfo(iPLCIndex).iFailNumber / (stationInfo(iPLCIndex).iPassNumber + stationInfo(iPLCIndex).iFailNumber)
                        End If
                        value = String.Format("{0:P2}", Ppm)

                    ElseIf item = "iPPM" Then
                        Dim Ppm As Double = 0
                        If stationInfo(iPLCIndex).iPassNumber + stationInfo(iPLCIndex).iFailNumber = 0 Then
                            Ppm = 0
                        Else
                            Ppm = stationInfo(iPLCIndex).iFailNumber / (stationInfo(iPLCIndex).iPassNumber + stationInfo(iPLCIndex).iFailNumber)
                        End If
                        Ppm = Ppm * 1000000
                        value = CInt(Ppm).ToString + " ppm"
                    ElseIf item = "strProcessTime" Then
                        Dim strProcess As String = stationInfo(iPLCIndex).strProcessTime
                        If strProcess = "" Then strProcess = "0"
                        value = (Double.Parse(strProcess) / 1000.0).ToString("0.00") + " s"
                    Else
                        value = _TC.Readfield(stationInfo(iPLCIndex), item).ToString
                    End If

                    If IsNothing(dgView.Rows(i).Cells(j).Value) Then
                        dgView.Rows(i).Cells(j).Value = ""
                    End If
                    If dgView.Rows(i).Cells(j).Value.ToString <> value Then
                        dgView.Rows(i).Cells(j).Value = value
                    End If
                    j = j + 1
                Next
            Next
            Return True

        Catch ex As Exception

            Dim strErr As String = ex.Message

        End Try


        Return False

    End Function
    Private Sub Maintenance_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
            _ShowOverview = False
        End If
    End Sub

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        AppSettings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)
        _Devices = Devices
        LanguageInit()
        CreateStationStatus()
        ' Dim sResult As String = _xmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ApplicationName, "GeneralInformation", "WtPLCName")
        '   _PLCName = sResult
        listItem.Add("strStation")
        listItem.Add("strProcessTime")
        listItem.Add("strArticleNumber")
        listItem.Add("strSerialNumber")
        listItem.Add("strScheduleName")
        listItem.Add("iCarrierNumber")
        listItem.Add("iDestinationStation")
        listItem.Add("iStepNumber")
        listItem.Add("bulAuto")
        listItem.Add("bulAlarm")
        listItem.Add("iAlarmNumber")
        listItem.Add("iMessageNumber")
        listItem.Add("iTotalNumber")
        listItem.Add("iPassNumber")
        listItem.Add("iFailNumber")
        listItem.Add("iPercent")
        listItem.Add("iPPM")
        Return True
    End Function


    Public Sub LanguageInit()
        dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        _Language.ReadControlText(Me)
        dgView.ColumnHeadersDefaultCellStyle.Font = New Font("Calibri", 10)
        dgView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgView.AlternatingRowsDefaultCellStyle.Font = New Font("Calibri", 10)
        dgView.RowsDefaultCellStyle.Font = New Font("Calibri", 10)

        For i = 0 To dgView.Columns.Count - 2
            dgView.Columns(i).HeaderCell.Value = _FileHandler.ReadLanguageFile(AppSettings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, OverviewInfoForm.sName, "dgView.Columns(" + i.ToString + ").HeaderCell")

        Next
        dgView.Columns(dgView.Columns.Count - 1).HeaderCell.Value = "   " + _FileHandler.ReadLanguageFile(AppSettings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, OverviewInfoForm.sName, "dgView.Columns(" + (dgView.Columns.Count - 1).ToString + ").HeaderCell") + "  "
        dgView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
    End Sub

    Private Sub CreateStationStatus()
        Try
            Dim _FileHander As New FileHandler
            Dim s_FileName As String = AppSettings.ConfigFolder + AppSettings.ConfigName
            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationStatusViews")

            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationStatusView")
                For Each _nodeList As XmlNode In _subNodes

                    dgView.Rows.Add(1)
                    dgView.Rows(dgView.Rows.Count - 1).Cells(0).Value = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    dgView.Rows(dgView.Rows.Count - 1).Cells(0).Tag = CType(_nodeList, XmlElement).GetElementsByTagName("PLC")(0).InnerText
                    dgView.Rows(dgView.Rows.Count - 1).Cells(1).Tag = CType(_nodeList, XmlElement).GetElementsByTagName("PLCIndex")(0).InnerText
                    dgView.Rows(dgView.Rows.Count - 1).HeaderCell.Value = (iCnt + 1).ToString
                    iCnt = iCnt + 1
                Next

            Next
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try

    End Sub

    Private Sub dgView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgView.CellValueChanged


        If e.ColumnIndex < 0 Then Return
        If e.RowIndex < 0 Then Return

        Select Case e.ColumnIndex
            Case 8
                Select Case dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
                    Case "Auto"
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN) 'Color.Green
                    Case "Manual"
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.Red
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                End Select
            Case 9
                Select Case dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
                    Case True.ToString
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED) 'Color.Red
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                End Select

            Case 10
                Select Case dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
                    Case "0"
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE) 'Color.White
                    Case Else
                        dgView.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_LIGHTRED) 'Color.Red
                End Select

            Case Else

        End Select


    End Sub
End Class