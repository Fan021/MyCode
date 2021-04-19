Imports Kostal.Las.Base
Imports Kostal.Las.UserInterface

Public Class WtStatusView
    Implements IViewDefine

    Private _xmlHandler As New XmlHandler
    Private _FileHandler As New FileHandler
    Private _Settings As New Settings
    Private _Language As Language
    Private _Logger As Logger
    Private mPassword As New PassWordForm
    Private _FormHandler As New FormControls

    Private _i As New Station
    Private _Log As Logger
    Private _ShowWtData As Boolean
    Private _WtNumberRequest As Byte
    Private _SourceData_Station As Boolean
    Private _DoReset_WT As Boolean
    Private _DoAbort_WT As Boolean
    Private _IsReset_WT As Boolean
    Private _Devices As Dictionary(Of String, Object)
    Private _WtInfo As New WT
    Private _TC As TwinCatAds
    Private _PLCName As String
    Private _IsInit As Boolean
    Private Const _PASS As String = "PASS"
    Private Const _FAIL As String = "FAIL"
    Private cUserManager As clsUserManager
#Region "Properties"

    Public ReadOnly Property DoReset_WT As Boolean
        Get
            Return _DoReset_WT
        End Get
    End Property


    Public Property IsReset_WT As Boolean
        Get
            Return _IsReset_WT
        End Get
        Set(ByVal value As Boolean)
            _IsReset_WT = value
        End Set
    End Property

    Public Property ShowWtData As Boolean
        Get
            Return _ShowWtData
        End Get
        Set(ByVal value As Boolean)
            _ShowWtData = value
        End Set
    End Property


    Public ReadOnly Property WtNumberRequest As Byte
        Get
            Return _WtNumberRequest
        End Get
    End Property


    Public ReadOnly Property WtInfo As WT
        Get
            Return _WtInfo
        End Get
    End Property


    Public ReadOnly Property SourceData_Station As Boolean
        Get
            Return _SourceData_Station
        End Get
    End Property

    Public ReadOnly Property GetPannel As Panel Implements IViewDefine.GetPannel
        Get
            Return Me.DesignPanel
        End Get
    End Property

#End Region

    Private Sub WtStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _IsInit = False
    End Sub
    Private Sub ArticleCounter_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not e.CloseReason = CloseReason.FormOwnerClosing Then
            e.Cancel = True
            _ShowWtData = False
        End If
    End Sub
    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase)) As Boolean
        Dim sResult As String, x As Integer, y As Integer, WT As Long
        cUserManager = CType(Devices(clsUserManager.Name), clsUserManager)
        _IsInit = False
        _Settings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)
        _Devices = Devices
        sResult = _xmlHandler.GetSectionInformation(_Settings.ConfigFolder, _Settings.ApplicationName, "GeneralInformation", "WtPLCName")
        _PLCName = sResult
        _Log = New Logger(_Settings)

        _i.Name = Me.Name.ToString

        mPassword.Init(_i, _Settings, "UserPassWord")

        _Language.ReadControlText(Me)


        rbSource_Station.Checked = True
        _SourceData_Station = True
        btnWtReset.Visible = False
        btnWtAbort.Visible = False

        rbSource_WT.Enabled = IIf（cUserManager.CurrentUserCfg.Level > enumUserLevel.Operator, True, False)

        _DoAbort_WT = False
        _DoReset_WT = False

        Me.StartPosition = FormStartPosition.Manual
        Me.Top = CInt((CDbl(My.Computer.Screen.WorkingArea.Height) / CDbl(2)) - (CDbl(Me.Height) / CDbl(2)))
        Me.Left = CInt((CDbl(My.Computer.Screen.WorkingArea.Width) / CDbl(2)) - (CDbl(Me.Width) / CDbl(2)))
        sResult = _xmlHandler.GetSectionInformation(_Settings.ApplicationFolder, _Settings.RootIniName, "Environment", "Screen")
        If IsNumeric(sResult) Then
            Me.Left = Me.Left + CInt(sResult) * My.Computer.Screen.WorkingArea.Width
        Else
            Me.Left = 0
        End If

        DG_WT_Data.Rows.Clear()
        DG_WT_Data.Rows.Add(10)
        For x = 0 To DG_WT_Data.Rows.Count - 1
            For y = 0 To 1
                DG_WT_Data.Rows(x).MinimumHeight = 30
                DG_WT_Data.Rows(x).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                DG_WT_Data.Rows(x).DefaultCellStyle.Font = New Font("Calibri", 18, FontStyle.Bold)
                DG_WT_Data.Rows(x).Cells(y).Value = ""
            Next
        Next

        sResult = _xmlHandler.GetSectionInformation(_Settings.ConfigFolder, _Settings.ApplicationName, "GeneralInformation", "WtStatus")
        If IsNumeric(sResult.Trim) Then
            For WT = 1 To CLng(sResult)

                cmbWT.Items.Add(WT.ToString)
            Next
            cmbWT.SelectedIndex = 0
        Else
            _Log.Logger(_i, "Invalid Key in [WtStatus] > MaxWT")
            Return False
        End If

        LanguageInit()

        '  Me.Show()
        _IsInit = True
        _Log.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_INIT, "Successful"), "WtStatusForm.Init")
        Return True
    End Function

    Public Sub Run()

        If Not _ShowWtData Then
            'If Me.Visible Then Me.Visible = False
            _DoReset_WT = False

        Else
            If IsNothing(_TC) Then
                _TC = CType(_Devices(_PLCName), TwinCatAds)
            End If
            'If Not Me.Visible Then Me.Visible = True
            _WtInfo = _TC.ReadCarrierInfo(_WtNumberRequest)

            'If _WtInfo.Status.ToUpper = _PASS Then
            '    If lblWtNumber.BackColor <> ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN) Then
            '        lblWtNumber.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)
            '    End If
            'ElseIf _WtInfo.Status.ToUpper = _FAIL Then
            '    If lblWtNumber.BackColor <> ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED) Then
            '        lblWtNumber.BackColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
            '    End If
            'Else
            '    If lblWtNumber.BackColor <> Color.Transparent Then lblWtNumber.BackColor = Color.Transparent
            'End If
            If lblWtName.BackColor <> lblWtNumber.BackColor Then lblWtName.BackColor = lblWtNumber.BackColor

            ' If lblWtNumber.Text <> CStr(_WtInfo.Number) Then lblWtNumber.Text = CStr(_WtInfo.Number)
            If DG_WT_Data.Rows(0).Cells(1).Value.ToString <> _WtInfo.ArticleNumber Then DG_WT_Data.Rows(0).Cells(1).Value = _WtInfo.ArticleNumber
            If DG_WT_Data.Rows(1).Cells(1).Value.ToString <> _WtInfo.SerialNumber Then DG_WT_Data.Rows(1).Cells(1).Value = _WtInfo.SerialNumber
            If DG_WT_Data.Rows(2).Cells(1).Value.ToString <> _WtInfo.Status Then DG_WT_Data.Rows(2).Cells(1).Value = _WtInfo.Status
            If DG_WT_Data.Rows(3).Cells(1).Value.ToString <> _WtInfo.Target Then DG_WT_Data.Rows(3).Cells(1).Value = _WtInfo.Target
            If DG_WT_Data.Rows(4).Cells(1).Value.ToString <> _WtInfo.PartFailLocation Then DG_WT_Data.Rows(4).Cells(1).Value = _WtInfo.PartFailLocation
            If DG_WT_Data.Rows(5).Cells(1).Value.ToString <> _WtInfo.PartFailTestStep Then DG_WT_Data.Rows(5).Cells(1).Value = _WtInfo.PartFailTestStep
            If DG_WT_Data.Rows(6).Cells(1).Value.ToString <> _WtInfo.PartFailCode Then DG_WT_Data.Rows(6).Cells(1).Value = _WtInfo.PartFailCode
            If DG_WT_Data.Rows(7).Cells(1).Value.ToString <> _WtInfo.PartFailText Then DG_WT_Data.Rows(7).Cells(1).Value = _WtInfo.PartFailText
            If DG_WT_Data.Rows(8).Cells(1).Value.ToString <> _WtInfo.PartFailValue Then DG_WT_Data.Rows(8).Cells(1).Value = _WtInfo.PartFailValue
            If DG_WT_Data.Rows(9).Cells(1).Value.ToString <> _WtInfo.PartFailLowerLimit Then DG_WT_Data.Rows(9).Cells(1).Value = _WtInfo.PartFailLowerLimit
            If DG_WT_Data.Rows(10).Cells(1).Value.ToString <> _WtInfo.PartFailUpperLimit Then DG_WT_Data.Rows(10).Cells(1).Value = _WtInfo.PartFailUpperLimit
            If _DoReset_WT Then
                _TC.ResetCarrierInfo(_WtNumberRequest)
                _DoReset_WT = False
            End If
            If _DoAbort_WT Then
                _TC.AbortCarrierInfo(_WtNumberRequest)
                _DoAbort_WT = False
            End If
            FormControl()

        End If
    End Sub

    Public Sub LanguageInit()

        ' _Language.ReadControlText(Me)
        Dim strText As String = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Station:")
        If strText = FileHandler.s_DEFAULT Then
            Me.lblWtName.Text = "Station:"
        Else
            Me.lblWtName.Text = strText
        End If

        Me.btnWtReset.Text = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "btnWtReset")
        Me.btnWtAbort.Text = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "btnWtAbort")
        Me.rbSource_WT.Text = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "rbSource_WT")
        Me.rbSource_Station.Text = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "rbSource_Station")

        Me.DG_WT_Data.Rows(0).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Article")
        Me.DG_WT_Data.Rows(1).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "SerialNumber")
        Me.DG_WT_Data.Rows(2).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Status")
        Me.DG_WT_Data.Rows(3).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "TargetAddress")
        Me.DG_WT_Data.Rows(4).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Station")
        Me.DG_WT_Data.Rows(5).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "TestStepNumber")
        Me.DG_WT_Data.Rows(6).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Code")
        Me.DG_WT_Data.Rows(7).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Text")
        Me.DG_WT_Data.Rows(8).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "Value")
        Me.DG_WT_Data.Rows(9).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "LowerLimit")
        Me.DG_WT_Data.Rows(10).Cells(0).Value = _FileHandler.ReadLanguageFile(_Settings.LngFolder, _Language.LanguageElement.SelectedLanguageFileName, "WtData", "UpperLimit")

    End Sub


    Private Sub FormControl()

        Dim mColor As Color

        On Error Resume Next

        If _IsReset_WT Then
            mColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_GREEN)

        ElseIf _DoReset_WT Then
            mColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_YELLOW)

        Else
            mColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_WHITE)

        End If

        _FormHandler.FormControl(btnWtReset, mColor, btnWtReset.Text, False, mColor, btnWtReset.Text, btnWtReset.Visible)
        _FormHandler.FormControl(btnWtAbort, mColor, btnWtAbort.Text, False, mColor, btnWtAbort.Text, btnWtAbort.Visible)
        On Error GoTo 0

    End Sub

    Private Sub cmbWT_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWT.SelectedIndexChanged

        Try
            _WtNumberRequest = CByte(cmbWT.SelectedItem)
            lblWtNumber.Text = _WtNumberRequest.ToString("D02")
        Catch ex As Exception
            _WtNumberRequest = 0

        End Try

        _DoReset_WT = False

    End Sub

    Private Sub rbSource_Station_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSource_Station.Click

        _SourceData_Station = True
        btnWtReset.Visible = False
        _DoReset_WT = False
        btnWtAbort.Visible = False
        _DoAbort_WT = False
    End Sub

    Private Sub rbSource_WT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSource_WT.Click

        _SourceData_Station = False
        btnWtReset.Visible = True
        btnWtAbort.Visible = True
    End Sub

    Private Sub btnWtReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWtReset.Click

        mPassword.ChangeMode = False
        mPassword.ShowDialog()

        If mPassword.PassWordValid Then
            _Log.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_WT_RESET), "WtStatusForm.Reset")
            _DoReset_WT = True
        End If

    End Sub
    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Me.Dispose()
        Return True
    End Function


    Private Sub cmbWT_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWT.SizeChanged
        Try
            TableLayoutPanel_Body.RowStyles(0).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.RowStyles(0).Height = cmbWT.Height + 20

            Dim g As Graphics = lblWtName.CreateGraphics()
            Dim length As SizeF = g.MeasureString(lblWtName.Text, lblWtName.Font)
            TableLayoutPanel_Body.ColumnStyles(0).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(0).Width = length.Width * 1.2
            TableLayoutPanel_Body.ColumnStyles(1).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(1).Width = length.Width
            TableLayoutPanel_Body.ColumnStyles(2).SizeType = SizeType.Percent
            TableLayoutPanel_Body.ColumnStyles(2).Width = 100
            TableLayoutPanel_Body.ColumnStyles(3).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(3).Width = length.Width
            TableLayoutPanel_Body.ColumnStyles(4).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(4).Width = length.Width
            TableLayoutPanel_Body.ColumnStyles(5).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(5).Width = length.Width
            TableLayoutPanel_Body.ColumnStyles(6).SizeType = SizeType.Absolute
            TableLayoutPanel_Body.ColumnStyles(6).Width = length.Width
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnWtAbort_Click(sender As Object, e As EventArgs) Handles btnWtAbort.Click
        mPassword.ChangeMode = False
        mPassword.ShowDialog()

        If mPassword.PassWordValid Then
            _Log.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_WT_RESET), "WtStatusForm.Reset")
            _DoAbort_WT = True
        End If
    End Sub
End Class