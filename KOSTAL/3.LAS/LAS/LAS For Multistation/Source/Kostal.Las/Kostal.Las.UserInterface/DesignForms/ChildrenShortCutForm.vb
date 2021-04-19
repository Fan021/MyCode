Imports System.Threading
Imports System.Collections.Concurrent
Imports Kostal.Las.Base
Public Class ChildrenShortCutForm
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As New Dictionary(Of String, Object)
    Private strButtonName As String
    Private strLastDeviceName As String = ""
    Private cIniHandler As clsIniHandler
    Private iMax As Integer = 0
    Private cHMIPLC As TwinCatAds
    Private cThread As Thread
    Private bExit As Boolean
    Private mMainForm As MainForm_Bosh
    Private cUserManager As clsUserManager
    Private cDebugButtonManager As clsDebugButtonManager
    Private ePageMode As enumPageMode
    Private _xmlHandler As New XmlHandler
    Private cLanguageManager As Language
    Public cMain As MainForm_Mul
    Public ReadOnly Property GetPannel As Panel
        Get
            Return Me.Panel_Body
        End Get
    End Property



    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        Me.cSystemElement = Devices
        Me.cLocalElement = cLocalElement
        cLanguageManager = CType(Devices(Language.Name), Language)
        cUserManager = CType(Devices(clsUserManager.Name), clsUserManager)
        cDebugButtonManager = New clsDebugButtonManager
        cDebugButtonManager.Init(Devices, Stations, MySettings)
        GetPageMode()
        InitForm()
        InitControlText()
        Dim sResult As String = ""
        sResult = _xmlHandler.GetSectionInformation(MySettings.ConfigFolder, MySettings.ApplicationName, "GeneralInformation", "WtPLCName")
        cHMIPLC = CType(Devices(sResult), TwinCatAds)
        StartRefreshUI()
        Panel_Body.BackColor = Color.White
        Return True
    End Function


    Public Function InitForm() As Boolean
        btnReset.Text = cLanguageManager.Read("ChildrenShortCutForm", "btnReset")
        btnResetFail.Text = cLanguageManager.Read("ChildrenShortCutForm", "btnResetFail")
        btnResetReference.Text = cLanguageManager.Read("ChildrenShortCutForm", "btnResetReference")
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        btnReset.Font = New System.Drawing.Font("Calibri", 15)
        btnResetFail.Font = New System.Drawing.Font("Calibri", 15)
        btnResetReference.Font = New System.Drawing.Font("Calibri", 15)
        TopLevel = False
        Return True
    End Function

    Public Function InitControlText() As Boolean
        CreateIO()
        Return True
    End Function

    Private Sub CreateIO()
        Dim strTempValue As String = ""
        TableLayoutPanel_Right.Controls.Clear()
        TableLayoutPanel_Right.ColumnCount = 1
        TableLayoutPanel_Right.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Right.RowCount = 11
        TableLayoutPanel_Right.Dock = DockStyle.Fill
        Dim j As Integer = 0
        For j = 1 To 10
            TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0))
        Next
        TableLayoutPanel_Right.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize, 0))
        For j = 0 To 9
            TableLayoutPanel_Right.RowStyles(j) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0)
        Next
        TableLayoutPanel_Right.RowStyles(10) = New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize, 0)
        j = 0
        For Each element As clsIOCfg In cDebugButtonManager.ListIO.Values
            Dim OutputIO As New OutputIO
            element.IO = OutputIO
            OutputIO.Dock = DockStyle.Fill
            '  OutputIO.ControlDisable = IIf(cUserManager.CurrentUserCfg.Level = enumUserLevel.Administrator, False, True)
            OutputIO.MainButton.Font = New Font("Calibri", 10 * cMain.cFormFontResize.CurrentRate, FontStyle.Regular)
            OutputIO.Margin = New System.Windows.Forms.Padding(3, 3, 3, 3)
            If cUserManager.CurrentUserCfg.Level >= element.Level Then
                OutputIO.ControlDisable = False
            Else
                OutputIO.ControlDisable = True
            End If
            TableLayoutPanel_Right.Controls.Add(OutputIO, 0, j)
            OutputIO.RegisterButton(element.ActiveText, element.ID)
            '  If ePageMode = enumPageMode.Debug Then
            AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_Click
            AddHandler OutputIO.MainButton.MouseDown, AddressOf MainButton_MouseDown
            AddHandler OutputIO.MainButton.MouseUp, AddressOf MainButton_MouseUp
            '  End If
            'If ePageMode <> enumPageMode.Debug Then
            '    If element.Reserve Then OutputIO.ControlDisable = True
            'End If
            j = j + 1
        Next

    End Sub
    Private Sub MainButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Right Then
                If ePageMode <> enumPageMode.Debug Then Return
                Dim cParameter As New IOParameter
                cParameter.DebugLock = False
                cParameter.UserLevel = True
                cParameter.TextFont = CType(sender, Button).Font
                cParameter.Init(cLocalElement, cSystemElement)
                cParameter.TextBox_ID.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).ID.ToString
                cParameter.TextBox_NameA.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Text
                cParameter.TextBox_NameA2.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Text2
                cParameter.RadioButton_Toggle.Checked = IIf(cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).IOTriggerType = enumIOTriggerType.Toggle, True, False)
                cParameter.RadioButton_Tap.Checked = Not cParameter.RadioButton_Toggle.Checked
                cParameter.RadioButton_Y.Checked = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Reserve
                cParameter.RadioButton_N.Checked = Not cParameter.RadioButton_Y.Checked
                cParameter.HmiComboBox_Level.ComboBox.Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).Level.ToString
                If cParameter.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    If cParameter.TextBox_NameA.Text = "" Then
                        'cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine(enumUIName.ChildrenIOForm.ToString, "1", cParameter.Label_NameA.Text.Replace(":", "")), enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
                        Return
                    End If
                    StopRefreshUI()
                    cDebugButtonManager.ChangeIO(cParameter.TextBox_ID.Text, cParameter.TextBox_NameA.Text, cParameter.TextBox_NameA2.Text, cParameter.RadioButton_Y.Checked, IIf(cParameter.RadioButton_Toggle.Checked, enumIOTriggerType.Toggle, enumIOTriggerType.Tap), IIf(cParameter.HmiComboBox_Level.ComboBox.Text = enumUserLevel.Administrator.ToString, enumUserLevel.Administrator, enumUserLevel.Operator))
                    CType(sender, Button).Text = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name).ActiveText
                    StartRefreshUI()
                End If
            End If
            If e.Button = MouseButtons.Left Then
                Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
                If TypeOf cIOCfg.IO Is OutputIO Then
                    If cIOCfg.Reserve Then Return
                    If cUserManager.CurrentUserCfg.Level < cIOCfg.Level Then Return
                    If cIOCfg.IOTriggerType = enumIOTriggerType.Toggle Then
                            Dim iPageNr As Integer = 10
                            If iPageNr <= 0 Then iPageNr = 1
                            Dim lListDO() As Boolean = cHMIPLC.ReadAny(cIOCfg.AdsName, GetType(Boolean()), New Integer() {iPageNr * 8})
                            Dim dOldValue As Boolean = lListDO((cIOCfg.XIndex - 1) * 8 + cIOCfg.YIndex - 1)
                            Dim dNewValue As Boolean = Not dOldValue
                            cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
                        End If
                    End If
                End If
        Catch ex As Exception
            ' cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenIOForm.ToString))
        End Try
    End Sub

    Private Sub MainButton_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cUserManager.CurrentUserCfg.Level < cIOCfg.Level Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = True
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
            End If
        End If
    End Sub

    Private Sub MainButton_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            Dim cIOCfg As clsIOCfg = cDebugButtonManager.GetIOCfgFromID(CType(sender, Button).Name)
            If cIOCfg.Reserve Then Return
            If cUserManager.CurrentUserCfg.Level < cIOCfg.Level Then Return
            If cIOCfg.IOTriggerType = enumIOTriggerType.Tap Then
                Dim dNewValue As Boolean = False
                cHMIPLC.WriteAny(cIOCfg.AdsName + "[" + cIOCfg.YIndex.ToString + "]", dNewValue)
            End If
        End If
    End Sub
    Private Sub RefreshUI()
        Dim iStep As Integer = 1
        While Not bExit
            Try
                Application.DoEvents()
                '   If Not IsNothing(cErrorMessageManager) Then If cErrorMessageManager.ErrorMessageManagerState = enumErrorMessageManagerState.Alarm Then Continue While

                Select Case iStep
                    Case 1

                        Dim lListDI1() As Boolean = cHMIPLC.ReadAny(KostalAdsVariables.HMI_ShortcutButton, GetType(Boolean()), New Integer() {1 * 8})
                        For Each element As clsIOCfg In cDebugButtonManager.ListIO.Values
                            element.IO.SetIndicateBackColor(lListDI1((element.XIndex - 1) * 8 + element.YIndex - 1))
                        Next

                End Select
            Catch ex As Exception

            End Try
            System.Threading.Thread.Sleep(10)
        End While

    End Sub

    Public Function StopRefreshUI() As Boolean
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

    Public Function StartRefreshUI() As Boolean
        bExit = False
        cThread = New Thread(AddressOf RefreshUI)
        cThread.IsBackground = True
        cThread.Start()
        Return True
    End Function

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try

            StopRefreshUI()
            Me.Dispose()
            Return True
        Catch ex As Exception
            ' cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ChildrenShortCutForm.ToString))
            Return False
        End Try
    End Function

    Private Sub Panel_Right_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                     ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid)
    End Sub


    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level >= enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Debug
        Else
            ePageMode = enumPageMode.ReadOnly
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

    End Sub


End Class