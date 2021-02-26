Imports System.Threading
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports System.Drawing
Public Class MainForm
    Dim _Screen As Screen
    Private X As Double
    Private Y As Double
    Private newx As Double
    Private newy As Double
    Private _LastStatusHeight As Integer
    Private _LastStatusTop As Integer
    Public Event IamClosing()
    Public Event LanguageChangedTo(ByVal Name As String)
    Private _PassWordForm As New PassWordForm
    Private _Login As Login
    Public CycleCounter As Long
    Public sw As New Stopwatch
    Private _Count As Count
    Private _ArticleCount As NoArticleCount
    Private _LinecontrolConfig As LinecontrolConfig
    Private _SMTNumber As SMTNumber
    Private _Bit As Bit
    Private _PLC As PLC
    Private mSetting As Settings
    Private mLanguage As Language
    Public TC As TwinCatAds
    Private strLastStateInfo As String
    Private _Devices As Dictionary(Of String, Object)
    Private Sub MainForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        _PassWordForm.ChangeMode = False
        _PassWordForm.ShowDialog()
        If _PassWordForm.PassWordValid Then
            RaiseEvent IamClosing()
        End If
    End Sub

    Public Function Init(ByVal Devices As Dictionary(Of String, Object)) As Boolean
        mLanguage = CType(Devices(Language.Name), Language)
        mSetting = CType(Devices(Settings.Name), Settings)
        _PassWordForm.Init()
        ChangeFormSize()
        SetStatusStrip()
        SetMenu()
        ReLoadLanguage()
        timCycle.Enabled = True
        _Devices = Devices
        _Login = New Login
        _Login.setting = mSetting
        If mSetting.WebServiceCfg.Enable Then
            If Not mSetting.WebServiceCfg.PassiveMode Then
                StatusForm.Items("MES").Image = My.Resources.green
            Else
                StatusForm.Items("MES").Image = My.Resources.gray
            End If
        Else
            StatusForm.Items("MES").Image = My.Resources.gray
        End If
        Return True
    End Function

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub


    Public Sub SetStatusStrip()

        Dim mToolStripStatusLabel As ToolStripStatusLabel
        Dim MyVersion As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim MyFileVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion

        StatusForm.Items.Clear()

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "CycleTime"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = ""
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "Left"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = ""
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "Right"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = ""
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "PLC"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = "PLC"
        mToolStripStatusLabel.Image = My.Resources.gray
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "MES"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = "MES"
        mToolStripStatusLabel.Image = My.Resources.gray
        StatusForm.Items.Add(mToolStripStatusLabel)

    End Sub

    Private Function SetMenu() As Boolean

        'Dim l As Integer, NewMenuItem As ToolStripMenuItem

        'For l = 1 To mLanguage.LanguageElement.LanguageFileName_Count
        '    NewMenuItem = New ToolStripMenuItem
        '    NewMenuItem.Name = Me.MenuLanguage.Name & "_" & mLanguage.LanguageElement.LanguageFileName(l)
        '    NewMenuItem.Text = mLanguage.LanguageElement.LanguageFileName(l)
        '    NewMenuItem.Tag = mLanguage.LanguageElement.LanguageFileName(l)
        '    Me.MenuLanguage.DropDownItems.Add(NewMenuItem)
        '    AddHandler NewMenuItem.Click, AddressOf Language_Change
        '    If mLanguage.LanguageElement.LanguageFileName(l) = mLanguage.LanguageElement.SelectedLanguageFileName Then NewMenuItem.Checked = True 'added by wang65 2015.06.12
        '    NewMenuItem = Nothing
        'Next

        Return True

    End Function

    Private Sub Language_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim Item As ToolStripMenuItem
        'mXmlHandler.SetGeneralInformation(mSetting.ConfigFolder, mSetting.ConfigName, mLanguage.LanguageElement.Section_LanguageFileNames, mLanguage.LanguageElement.KeyWord_SelectedLanguage, CType(sender, ToolStripMenuItem).Tag.ToString)
        'mLanguage.SetAppLanguage.ReloadLanguage()
        'For Each Item In Me.MenuLanguage.DropDownItems
        '    Item.Checked = False
        'Next
        'ReLoadLanguage()
        'RaiseEvent LanguageChangedTo(CType(sender, ToolStripMenuItem).Tag.ToString)
        'CType(sender, ToolStripMenuItem).Checked = True
    End Sub

    Public Sub ReLoadLanguage()
        mLanguage.ReadControlText(Me)
    End Sub
    Public Sub ChangeFormSize()
        _Screen = Screen.AllScreens(0)
        X = 1292
        Y = 973
        _LastStatusHeight = 0
        _LastStatusTop = 0
        setTag(Me)
        Me.Left = _Screen.WorkingArea.Left
        Me.Width = _Screen.WorkingArea.Width
        Me.Height = _Screen.WorkingArea.Height
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = 0
        newx = (Me.Width) / X
        newy = (Me.Height - 10) / Y
        ' If _Screen.WorkingArea.Width <> 1280 Or _Screen.WorkingArea.Height <> 1024 Then
        setControls(newx, newy, Me)
        ' End If
    End Sub

    Private Sub setControls(ByVal newx As Double, ByVal newy As Double, ByVal cons As Control)
        For Each con As Control In cons.Controls
            If IsNothing(con.Tag) Then Continue For
            Dim mytag() As String = con.Tag.ToString().Split(CChar(":"))
            Dim a As Double = Convert.ToSingle(mytag(0)) * newx

            If cons.Name.IndexOf("grpStatus") >= 0 Then
                If _LastStatusHeight <> 0 Then
                    con.Width = CInt(a)
                    a = Convert.ToSingle(mytag(1)) * newy
                    con.Height = CInt(a)
                    a = Convert.ToSingle(mytag(2)) * newx
                    con.Left = CInt(a)
                    a = Convert.ToSingle(mytag(3)) * newy
                    con.Top = _LastStatusTop - _LastStatusHeight + 1
                    _LastStatusHeight = con.Height
                    _LastStatusTop = con.Top
                Else
                    con.Width = CInt(a)
                    a = Convert.ToSingle(mytag(1)) * newy
                    con.Height = CInt(a)
                    _LastStatusHeight = con.Height
                    a = Convert.ToSingle(mytag(2)) * newx
                    con.Left = CInt(a)
                    a = Convert.ToSingle(mytag(3)) * newy
                    con.Top = CInt(a)
                    _LastStatusTop = con.Top
                End If
            Else
                con.Width = CInt(a)
                a = Convert.ToSingle(mytag(1)) * newy
                con.Height = CInt(a)
                a = Convert.ToSingle(mytag(2)) * newx
                con.Left = CInt(a)
                a = Convert.ToSingle(mytag(3)) * newy
                con.Top = CInt(a)
            End If
            Dim currentSize As Single = CSng((Convert.ToSingle(mytag(4))) * newy)
            If cons.Name.IndexOf("DG_Article") >= 0 Then
                con.Font = New Font("SimSun", currentSize, con.Font.Style, con.Font.Unit)
            Else
                con.Font = New Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit)
            End If
            If con.Controls.Count > 0 Then
                setControls(newx, newy, con)
            End If
        Next

    End Sub


    Private Sub setTag(ByVal cons As Control)
        For Each con As Control In cons.Controls
            con.Tag = con.Width.ToString + ":" + con.Height.ToString + ":" + con.Left.ToString + ":" + con.Top.ToString + ":" + con.Font.Size.ToString
            setTag(con)
        Next
    End Sub


    Private Sub timCycle_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timCycle.Tick
        Dim Cycle As Double, mCycle As Double
        Dim swTime As Double = 0
        Try
            timCycle.Enabled = False
            sw.Stop()
            swTime = sw.ElapsedMilliseconds
            If CycleCounter > 0 Then
                Cycle = CType(swTime, Double) / CType(CycleCounter, Double)
            Else
                Cycle = CType(swTime, Double)
            End If
            mCycle = CType(Cycle, Double)
            If StatusForm.Items("CycleTime").Text <> "CycleTime:" & mCycle.ToString("0.000") & " ms" Then StatusForm.Items("CycleTime").Text = "CycleTime:" & mCycle.ToString("0.000") & " ms"
            '-------------------------------------------------------------------
            If TC IsNot Nothing AndAlso TC.StateInfo <> strLastStateInfo Then
                If TC.StateInfo.ToUpper.Contains("RUN") Then
                    StatusForm.Items("PLC").Image = My.Resources.green
                ElseIf TC.StateInfo.ToUpper.Contains("STOP") Then
                    StatusForm.Items("PLC").Image = My.Resources.red
                Else
                    StatusForm.Items("PLC").Image = My.Resources.gray
                End If
                strLastStateInfo = TC.StateInfo.ToString
            End If
            '-------------------------------------------------------------------
            sw.Reset()
            sw.Start()
            CycleCounter = 0
            timCycle.Enabled = True
        Catch ex As Exception
            timCycle.Enabled = True
        End Try
    End Sub

    Private Sub ShowDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowDataToolStripMenuItem.Click
        _Login.Level = 1
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _Count = New Count
            _Count.Init(_Devices)
            _Count.ShowDialog()
            _Count.Dispose()
        End If
    End Sub

    Private Sub MainForm_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Ui_Left.TextBox_ErrorMsg.Focus()
    End Sub

    Private Sub ShowArticleDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowArticleDataToolStripMenuItem.Click
        _Login.Level = 2
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _ArticleCount = New NoArticleCount
            _ArticleCount.Init(_Devices)
            _ArticleCount.ShowDialog()
            _ArticleCount.Dispose()
        End If
    End Sub

    Private Sub ShowLineControlConfigToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowLineControlConfigToolStripMenuItem.Click
        _Login.Level = 2
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _LinecontrolConfig = New LinecontrolConfig
            _LinecontrolConfig.Init(_Devices)
            _LinecontrolConfig.ShowDialog()
            _LinecontrolConfig.Dispose()
        End If
    End Sub

    Private Sub SMTNumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SMTNumberToolStripMenuItem.Click
        _Login.Level = 2
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _SMTNumber = New SMTNumber
            _SMTNumber.Init(_Devices)
            _SMTNumber.ShowDialog()
            _SMTNumber.Dispose()
        End If
    End Sub

    Private Sub BitNumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BitNumberToolStripMenuItem.Click
        _Login.Level = 2
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _Bit = New Bit
            _Bit.Init(_Devices)
            _Bit.ShowDialog()
            _Bit.Dispose()
        End If
    End Sub

    Private Sub PLCNumberToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PLCNumberToolStripMenuItem.Click
        _Login.Level = 2
        _Login.PassWordValid = False
        _Login.ShowDialog()
        If _Login.PassWordValid Then
            _PLC = New PLC
            _PLC.Init(_Devices)
            _PLC.ShowDialog()
            _PLC.Dispose()
        End If
    End Sub
End Class