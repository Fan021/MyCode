Imports System.Threading
Imports Kostal.Las.ArticleProvider
Imports Kostal.Las.Base
Imports System.Collections.Generic
Imports System.Linq

Public Class MainForm
    Inherits System.Windows.Forms.Form

    Public Event IamClosing()
    Public Event LanguageChangedTo(ByVal Name As String)

    Private mPassword As New PassWordForm
    Private mFileHandler As New FileHandler
    Private mXmlHandler As New XmlHandler

	Private i As New Station
    Private Log As Logger
    Private _mLanguage As Language
    Private mSettings As New Settings
    Public CAQ_Label As ToolStripStatusLabel
    Public LineControl_Label As ToolStripStatusLabel
    Private _HelpReader As New ToolStripMenuItemReader
    Private _LocalSchedule As Schedule

    Private Delegate Sub dGroupHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.BackColor = Color.FromArgb(30, 70, 125)
        Me.timCycle.Enabled = True

        AddHandler lblStatusName_01.Click, AddressOf GroupHandler
        AddHandler lblStatusName_02.Click, AddressOf GroupHandler
        AddHandler lblStatusName_03.Click, AddressOf GroupHandler
        AddHandler lblStatusName_04.Click, AddressOf GroupHandler
        AddHandler lblStatusName_05.Click, AddressOf GroupHandler
        AddHandler lblStatusName_06.Click, AddressOf GroupHandler
        AddHandler lblStatusName_07.Click, AddressOf GroupHandler
        AddHandler lblStatusName_08.Click, AddressOf GroupHandler
        AddHandler lblStatusName_09.Click, AddressOf GroupHandler
        AddHandler lblStatusName_10.Click, AddressOf GroupHandler
        AddHandler lblStatusName_11.Click, AddressOf GroupHandler
        AddHandler lblStatusName_12.Click, AddressOf GroupHandler
        AddHandler lblStatusName_13.Click, AddressOf GroupHandler
        AddHandler lblStatusName_14.Click, AddressOf GroupHandler
        AddHandler lblStatusName_15.Click, AddressOf GroupHandler
        AddHandler lblStatusName_16.Click, AddressOf GroupHandler

        picArticle.Left = CInt(grpPicture.Width / 2 - picArticle.Width / 2)
        picArticle.Top = CInt(grpPicture.Height / 2 - picArticle.Height / 2)
        picArticle.SendToBack()
    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then MeExit()
    End Sub

    Private Sub MeExit()
        Log.Logger(i, "sucessfull", "Exit")
        RaiseEvent IamClosing()
        'Me.Dispose()
    End Sub

    'Public Overloads Sub Dispose()
    '    RaiseEvent IamClosing()
    '    MyBase.Dispose()
    'End Sub

    Public Function Init(ByVal FormSettings As Settings, ByVal mLanguage As Language) As Boolean
        Dim sResult As String
        Dim _Screen As Screen

        mSettings = FormSettings
        _mLanguage = mLanguage
        Log = New Logger(mSettings)
        i.Name = Me.Name
        Log.Logger(i, "Init Run", "MainForm")
        If mSettings.BoschLine Then
            _LocalSchedule = New Schedule(i, mSettings, _mLanguage)
            _LocalSchedule.Init()
        End If

        SetStatusStrip()
        SetMenu()

        ReLoadLanguage()

        mPassword.Init(i, AppSettings, "UserPassWord")

        sResult = mXmlHandler.GetSectionInformation(mSettings.ApplicationFolder, mSettings.RootIniName, "Environment", "Screen")

        Try
            _Screen = Screen.AllScreens(CInt(sResult))
        Catch ex As Exception
            _Screen = Screen.AllScreens(0)
        End Try


        If Not mFileHandler.FileExist(mSettings.PicFolder + "layout.bmp") Then
            Throw New Exception("No Find " + mSettings.PicFolder + "layout.bmp")
            Return False
        End If
        picArticle.Image = New Bitmap(mSettings.PicFolder + "layout.bmp")

        Me.Left = _Screen.WorkingArea.Left
        Me.Width = _Screen.WorkingArea.Width
        Me.Height = _Screen.WorkingArea.Height
        Me.StartPosition = FormStartPosition.Manual
        Me.Top = 0
        Me.SkinEngine1.SkinFile = mSettings.SkinFolder + "Skin1.ssk"
        'DG_Article.Rows.Clear()

        Log.Logger(i, "Init sucessfull", "MainForm")


        Dim t As Graphics = grpPicture.CreateGraphics

        Dim t1 As Point
        grpPicture.PointToClient(t1)
        t.DrawLine(Pens.Black, t1, New Point(t1.X + 100, t1.Y + btnArticle.Height))
        If Not mSettings.BoschLine Then
            ShowWtDataToolStripMenuItem.Visible = False
            ShowScheduleToolStripMenuItem.Visible = False
        End If
        Return True
    End Function

    Public Sub SetStatusStrip()

        Dim mToolStripStatusLabel As ToolStripStatusLabel
        Dim MyVersion As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim MyFileVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion

        StatusForm.Items.Clear()

        'added by wang65 2015.06.13
        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "tssKostal"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = "KOSTAL Co."
        mToolStripStatusLabel.Image = My.Resources.logo_screen_145px2
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "ApplicationFolder"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = mSettings.ApplicationFolder & mSettings.ApplicationName
        StatusForm.Items.Add(mToolStripStatusLabel)

        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "Version"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All

        mToolStripStatusLabel.Text = _
         " = Version " + Format(MyVersion.Major, "00") + "." + Format(MyVersion.Minor, "00") + "." + Format(MyVersion.Build, "00") + "." + Format(MyVersion.Revision, "00") + _
         " = Build " + MyFileVersion + " ="
        StatusForm.Items.Add(mToolStripStatusLabel)

        For Each value In mSettings.PLCConfig.Keys
            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = value
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = value
            mToolStripStatusLabel.Image = My.Resources.gray
            StatusForm.Items.Add(mToolStripStatusLabel)
        Next


        mToolStripStatusLabel = Nothing
        mToolStripStatusLabel = New ToolStripStatusLabel
        mToolStripStatusLabel.Name = "CycleTime"
        mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
        mToolStripStatusLabel.Text = mSettings.ApplicationFolder & mSettings.ApplicationName
        StatusForm.Items.Add(mToolStripStatusLabel)

    End Sub

	Private Function SetMenu() As Boolean

        Dim l As Integer, NewMenuItem As ToolStripMenuItem

        For l = 1 To _mLanguage.LanguageElement.LanguageFileName_Count
            NewMenuItem = New ToolStripMenuItem
            NewMenuItem.Name = Me.MenuLanguage.Name & "_" & _mLanguage.LanguageElement.LanguageFileName(l)
            NewMenuItem.Text = _mLanguage.LanguageElement.LanguageFileName(l)
            NewMenuItem.Tag = _mLanguage.LanguageElement.LanguageFileName(l)
            Me.MenuLanguage.DropDownItems.Add(NewMenuItem)
            AddHandler NewMenuItem.Click, AddressOf Language_Change
            If _mLanguage.LanguageElement.LanguageFileName(l) = _mLanguage.LanguageElement.SelectedLanguageFileName Then NewMenuItem.Checked = True 'added by wang65 2015.06.12
            NewMenuItem = Nothing
        Next

        If IsNothing(AppSettings.HelpFiles) Then Return False
        If AppSettings.HelpFiles.Count = 0 Then Return False

        For l = 1 To AppSettings.HelpFiles.Count
            NewMenuItem = New ToolStripMenuItem
            NewMenuItem.Name = "HelpFile_" + l.ToString
            NewMenuItem.Text = NewMenuItem.Name
            NewMenuItem.Tag = l
            Me.HelpToolStripMenuItem.DropDownItems.Add(NewMenuItem)
            AddHandler NewMenuItem.Click, AddressOf CallHelp
            NewMenuItem = Nothing
        Next

        Return True

	End Function

	Private Sub CallHelp(ByVal sender As System.Object, ByVal e As System.EventArgs)

		If IsNothing(AppSettings.HelpFiles) Then Return
		If AppSettings.HelpFiles.Count = 0 Then Return

        Dim Item As New ToolStripMenuItem

        Try
            Item = CType(sender, ToolStripMenuItem)
            Shell(AppSettings.HelpApplication(CInt(Item.Tag)) & " " & AppSettings.HelpFolder & AppSettings.HelpFiles(CInt(Item.Tag)), AppWinStyle.NormalFocus)

        Catch ex As Exception
            i.StepTextLine = "CallHelp"

            If Not IsNothing(Item) Then
                i.Text = Item.Name
            Else
                i.Text = "No Item defined"
            End If

            Log.Logger(i)

        End Try

	End Sub

    Public Function ReLoadLanguage() As Boolean
        _mLanguage.ReadControlText(Me)
        If mSettings.BoschLine Then
            ShowScheduleReLoadLanguage()
        End If
        Return True
    End Function

    Private Sub Language_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim Item As ToolStripMenuItem

        mXmlHandler.SetGeneralInformation(AppSettings.IniFolder, AppSettings.ApplicationActive, _mLanguage.LanguageElement.Section_LanguageFileNames, _mLanguage.LanguageElement.KeyWord_SelectedLanguage, CType(sender, ToolStripMenuItem).Tag.ToString)
        RaiseEvent LanguageChangedTo(sender.ToString)

        For Each Item In Me.HelpToolStripMenuItem.DropDownItems
            Try
                _HelpReader.ReadToolStripMenuItem(mSettings.LngFolder, _mLanguage.LanguageElement.SelectedLanguageFileName & mSettings.Extension_LanguageFile, Item)
            Catch ex As Exception
                If Not IsNothing(Item) Then

                End If
            End Try
        Next

        'added by wang65 2015.06.12
        For Each Item In Me.MenuLanguage.DropDownItems
            Item.Checked = False
        Next
        CType(sender, ToolStripMenuItem).Checked = True
    End Sub

	Private Sub MenuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		AboutBox.ShowDialog()
	End Sub

	Private Sub MenuMinimized_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.WindowState = FormWindowState.Minimized
	End Sub

    Private Sub MenuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuExit.Click
        mPassword.ChangeMode = False
        mPassword.ShowDialog()
        If mPassword.PassWordValid Then MeExit()
    End Sub

    Private Sub MenuChangePassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuChangePassword.Click
        mPassword.ChangeMode = True
        mPassword.ShowDialog()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox.ShowDialog()
    End Sub

    Private Sub ShowWtDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowWtDataToolStripMenuItem.Click
        Main._WatchWT.ShowWtData = Not Main._WatchWT.ShowWtData
    End Sub

    Private Sub timCycle_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timCycle.Tick
        Dim Cycle As Double, mCycle As Long
        Dim swTime As Double = 0
        Static strLastStateInfo As New Dictionary(Of String, String)
        Try
            timCycle.Enabled = False
            Main.sw.Stop()
            swTime = sw.ElapsedMilliseconds
            Cycle = CType(swTime, Double) / CType(Main.CycleCounter, Double)
            mCycle = CType(Cycle, Long)
            If StatusForm.Items("CycleTime").Text <> "CycleTime:" & CStr(mCycle) & " ms" Then StatusForm.Items("CycleTime").Text = "CycleTime:" & CStr(mCycle) & " ms"
            '-------------------------------------------------------------------


            For Each value In MyTwinCat.Keys
                If Not strLastStateInfo.ContainsKey(value) Then
                    strLastStateInfo.Add(value, "")
                End If
                If MyTwinCat(value) IsNot Nothing Then
                    Try
                        If IsNothing(MyTwinCat(value).StateInfo) Then
                            timCycle.Enabled = False
                        End If
                    Catch ex As Exception
                        _PLCdisconnect = True
                        timCycle.Enabled = False
                    End Try

                End If

                If MyTwinCat(value) IsNot Nothing AndAlso MyTwinCat(value).StateInfo <> strLastStateInfo(value) Then
                    If MyTwinCat(value).StateInfo.ToUpper.Contains("RUN") Then
                        StatusForm.Items(value).Image = My.Resources.green
                    ElseIf MyTwinCat(value).StateInfo.ToUpper.Contains("STOP") Then
                        StatusForm.Items(value).Image = My.Resources.red
                    Else
                        StatusForm.Items(value).Image = My.Resources.gray
                    End If
                    strLastStateInfo(value) = MyTwinCat(value).StateInfo
                End If

            Next
            '-------------------------------------------------------------------
            Main.sw.Reset()
            Main.sw.Start()
            Main.CycleCounter = 0
            timCycle.Enabled = True
        Catch ex As Exception
            timCycle.Enabled = Not _PLCdisconnect
        End Try
    End Sub

	Private Sub GroupHandler(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim Tag As String, l As Integer, Tab As New TabPage

		TabControlStations.Visible = True

		Try
			Tag = CType(sender, Label).Tag.ToString

			For l = 0 To TabControlStations.TabCount - 1
				If TabControlStations.TabPages(l).Tag.ToString = Tag Then
					TabControlStations.SelectTab(l)
					Return
				End If
			Next
		Catch ex As Exception

		End Try

	End Sub

    Private Sub grpStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.TabControlStations.Visible = False
    End Sub

    Private Sub ScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim _Label As New Label

        _Label.Tag = 99
        GroupHandler(_Label, Nothing)

    End Sub

	Private Sub ShowCounterToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowCounterToolStripMenuItem1.Click
        _LineArticleCounter.ShowCounter = Not _LineArticleCounter.ShowCounter
	End Sub

    Private Sub picArticle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles picArticle.Click
        Call ScheduleToolStripMenuItem_Click(Me, Nothing)
    End Sub

    Private Sub MainBox_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MainBox.Enter

    End Sub

    Private Sub MainForm_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
    
    End Sub

    Private Sub InitScheduleData()
        Dim sResult As String = String.Empty
        Dim ID As String = String.Empty
        Dim sDescription As String = String.Empty
        Dim elementDictionary As New ScheduleDataElement
        Dim ManualCheckSum As Integer = 0
        Dim strKey As String = String.Empty

        _ScheduleView.ScheduleName.Clear()
        _ScheduleView.ScheduleData.Clear()

        '循环遍历添加ScheduleName
        For Each _scheElement In _LocalSchedule.ArticleElements.Values
            If _scheElement.Key <> KostalScheduleKeys.KEY_USER_VERIFICATION Then

                '根据关键字选择是否添加Description子键
                If _scheElement.Key.IndexOf("PassST") >= 0 Then
                    sDescription = "Description" + _scheElement.Key.Substring(_scheElement.Key.IndexOf("PassST") + 4)
                    sResult = _mLanguage.Read("Schedule", sDescription)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    _ScheduleView.ScheduleName.Add(sDescription, New ScheduleNameElement(sDescription, sResult, ScheduleNameType.ini))

                    '读取语言
                    sResult = _mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    _ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
                    Continue For
                End If

                '添加CheckSum
                If _scheElement.Key.IndexOf(BaseScheduleDataElement.SecurityChecksum.ToString) >= 0 Then
                    sResult = _mLanguage.Read("Schedule", _scheElement.Key)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = _scheElement.Key
                    End If
                    _ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))

                    '添加CheckSum
                    strKey = BaseScheduleDataElement.ManualChecksum.ToString
                    sResult = _mLanguage.Read("Schedule", strKey)
                    If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                        sResult = strKey
                    End If
                    _ScheduleView.ScheduleName.Add(strKey, New ScheduleNameElement(strKey, sResult, ScheduleNameType.Manual))
                    Continue For
                End If

                sResult = _mLanguage.Read("Schedule", _scheElement.Key)
                If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                    sResult = _scheElement.Key
                End If
                _ScheduleView.ScheduleName.Add(_scheElement.Key, New ScheduleNameElement(_scheElement.Key, sResult, ScheduleNameType.csv))
            End If
        Next

        '循环遍历添加ScheduleData
        For Each _schedulelistElement As ArticleListElement In _LocalSchedule.ArticleListElement.Values
            ID = _schedulelistElement.ID
            _LocalSchedule.GetArticle_FromID(ID)
            elementDictionary = New ScheduleDataElement
            elementDictionary.Hide = False
            ManualCheckSum = 0

            '计算CheckSum
            For Each _ArticleElements As ArticleElement In _LocalSchedule.ArticleElements.Values
                If _ArticleElements.Key.IndexOf("PassST") >= 0 Or _ArticleElements.Key.IndexOf("FailST") >= 0 Then
                    If IsNumeric(_ArticleElements.Data) Then
                        ManualCheckSum = ManualCheckSum + CInt(_ArticleElements.Data)
                    End If
                End If
            Next

            '循环遍历添加ScheduleData
            For Each _scheduleElement As ScheduleNameElement In _ScheduleView.ScheduleName.Values
                Select Case _scheduleElement.ValueFrom
                    Case ScheduleNameType.csv
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, _LocalSchedule.ArticleElements(_scheduleElement.Key).Data))
                    Case ScheduleNameType.Manual
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, ManualCheckSum.ToString))
                    Case ScheduleNameType.ini
                        sResult = _mLanguage.Read("Schedule", _scheduleElement.Key)
                        If sResult = FileHandler.s_DEFAULT Or sResult = FileHandler.s_Null Then
                            sResult = ""
                        End If
                        elementDictionary.ScheduleElement.Add(_scheduleElement.Key, New ScheduleElement(_scheduleElement.Key, sResult))
                End Select
            Next
            _ScheduleView.ScheduleData.Add(ID, elementDictionary)
        Next

    End Sub

    Private Sub CheckScheduleChecksum()
        For Each scheduleDataelement As ScheduleDataElement In _ScheduleView.ScheduleData.Values

            For Each TypeElemet As BaseScheduleDataElement In [Enum].GetValues(GetType(BaseScheduleDataElement))
                If Not scheduleDataelement.ScheduleElement.ContainsKey([Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet)) Then
                    Throw New Exception("Please Add Element Name: " + [Enum].GetName(GetType(BaseScheduleDataElement), TypeElemet))
                End If
            Next

            If _ScheduleView.CheckScheduleMode(scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ScheduleName.ToString).Value) Then
                If scheduleDataelement.ScheduleElement(BaseScheduleDataElement.SecurityChecksum.ToString).Value <> scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ManualChecksum.ToString).Value Then
                    Throw New Exception("Schedule: " + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ScheduleName.ToString).Value + " " & _
                                        BaseScheduleDataElement.SecurityChecksum.ToString + ":" + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.SecurityChecksum.ToString).Value + " " & _
                                        "Not equal " & _
                                        BaseScheduleDataElement.ManualChecksum.ToString + ":" + scheduleDataelement.ScheduleElement(BaseScheduleDataElement.ManualChecksum.ToString).Value + " " & _
                                        "Please Check schedule csv!"
                                        )
                End If
            End If
        Next
    End Sub
    Protected Function CheckScheduleMode(ByVal strName As String) As Boolean
        For Each TypeElemet As ScheduleMode In [Enum].GetValues(GetType(ScheduleMode))
            If strName.IndexOf([Enum].GetName(GetType(ScheduleMode), TypeElemet)) >= 0 Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub ShowScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowScheduleToolStripMenuItem.Click
        If _ScheduleView.ShowSchedule Then
            _ScheduleView.ShowSchedule = Not _ScheduleView.ShowSchedule
        Else
            Dim UserVerification As New StructUserVerification
            UserVerification.VerificationType = enumUserVerificationType.PASSWORD_USERDEFINED
            UserVerification.Password = "dotnet"
            mPassword.ChangeMode = False
            mPassword.UserVerification = UserVerification
            mPassword.ShowDialog()
            If mPassword.PassWordValid Then
                _ScheduleView.ShowSchedule = Not _ScheduleView.ShowSchedule
            End If
            UserVerification.VerificationType = enumUserVerificationType.PASSWORD_APPLICATION
            mPassword.UserVerification = UserVerification
        End If
       
    End Sub

    Private Sub ShowScheduleReLoadLanguage()
        _mLanguage.ReadContextMenuStrip(_ScheduleView.ContextMenuStrip_Schedule)
        InitScheduleData()
        CheckScheduleChecksum()
        If _ScheduleView.ShowSchedule Then
            _ScheduleView.ShowData()
        End If
    End Sub
End Class





