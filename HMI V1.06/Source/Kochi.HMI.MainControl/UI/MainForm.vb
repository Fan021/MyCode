Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.IO
Imports System.Threading

Public Class MainForm
    Implements IMainUI

    Private iUI As IParentUI
    Private cFormFontResize As New clsFormFontResize
    Private cDeviceManager As clsDeviceManager
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cMainButtonManager As clsMainButtonManager
    Private cMachineManager As clsMachineManager
    Private cErrorMessageManager As clsErrorMessageManager
    Private cUserManager As clsUserManager
    Private cSystemManager As clsSystemManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cLanguageManager As clsLanguageManager
    Private lListFunctionButton As New Dictionary(Of String, MainFunctionButton)
    Private strOldCycleTime As String = String.Empty
    Private isInit As Boolean = False
    Public Event IamClosing()
    Private bClosing As Boolean = False
    Private _Object As New Object
    Public iRunTime As Integer = 2
    Private cProcessStart As clsProcessStart
    Public ReadOnly Property RunTime As Integer
        Get
            Return iRunTime
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try

                Me.cSystemElement = cSystemElement
                cSystemElement.Add(enumUIName.MainForm.ToString, Me)
                cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cSystemElement.Add(clsFormFontResize.Name, cFormFontResize)
                cMainButtonManager = New clsMainButtonManager
                cMainButtonManager.Init(cLocalElement, cSystemElement)
                cSystemElement.Add(clsMainButtonManager.Name, cMainButtonManager)
                CreateMainButton()
                InitForm()
                isInit = True
                cFormFontResize.oldH = 676
                cFormFontResize.cons = Me
                DeleteFile()
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
                Return True
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InitForm() As Boolean
        SyncLock _Object
            Panel_Head.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormHead)
            Panel_Left.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLeft)
            Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormBody)
            SetStatusStrip()
            ShowCell()
            ShowUser()
            ShowTitle()
            ShowMes()
            DisableMainLeftButton()
            AddHandler cUserManager.UserChanged, AddressOf UserChanged
            AddHandler cUserManager.LoginOutChanged, AddressOf LoginOutChanged
            AddHandler Label_UserLoginOut.Click, AddressOf Label_Click
            Return True
        End SyncLock
    End Function
    Public Sub ShowTitle()
        Label_Titile.Text = cMachineManager.MachineCellManager.MachineCellCfg.ProjectName
    End Sub
    Public Sub ShowCell()
        SyncLock _Object
            Label_Cell.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Label_Cell")
            Label_CellValue.Text = cMachineManager.MachineCellManager.MachineCellCfg.CellName
            Label_Titile.Text = cMachineManager.MachineCellManager.MachineCellCfg.ProjectName
        End SyncLock
    End Sub
    Public Sub ShowMes()
        If cMachineManager.MachineGlobalParameter.GetGlobalParameter((clsHMIGlobalParameter.MES)) = True Then
            SetMESStatus(enumDeviceStatus.Open)
        Else
            SetMESStatus(enumDeviceStatus.Close)
        End If
        If cMachineManager.MachineGlobalParameter.GetGlobalParameter((clsHMIGlobalParameter.Process)) = True Then
            SetProcessStatus(enumDeviceStatus.Open)
        Else
            SetProcessStatus(enumDeviceStatus.Close)
        End If
    End Sub

    Public Sub ShowUser()
        SyncLock _Object
            Label_UserName.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Label_UserName")
            Label_UserNameValue.Text = cUserManager.CurrentUserCfg.Name
            Label_UserLevel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Label_UserLevel")
            If cUserManager.CurrentUserCfg.Level <> enumUserLevel.Normal Then
                Label_UserLevelValue.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, cUserManager.CurrentUserCfg.Level.ToString)
            Else
                Label_UserLevelValue.Text = ""
            End If
            Label_UserLoginOut.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Label_UserLoginOut")
        End SyncLock
    End Sub

    Public Sub SetStatusStrip()
        SyncLock _Object
            Dim mToolStripStatusLabel As ToolStripStatusLabel
            Dim MyVersion As System.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
            Dim MyFileVersion As String = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly.Location).FileVersion

            StatusForm.Items.Clear()
            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "tssKostal"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "KOSTAL Co.")
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "tssKochi"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Support By APB4")
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "Version"
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "HMI Version") & System.Diagnostics.FileVersionInfo.GetVersionInfo(cSystemManager.Settings.ApplicationFolder + "\Kochi.HMI.MainControl.exe").FileVersion
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "Base"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = "Kochi.HMI.MainControl.Base.dll Version: " & System.Diagnostics.FileVersionInfo.GetVersionInfo(cSystemManager.Settings.LibFolder + "\Kochi.HMI.MainControl.Base.dll").FileVersion
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "PLC"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "PLC")
            mToolStripStatusLabel.Image = My.Resources.gray
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "MES"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "MES")
            mToolStripStatusLabel.Image = My.Resources.gray
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "Process"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "Process")
            mToolStripStatusLabel.Image = My.Resources.gray
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "CycleTime"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = "0.00 ms"
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)

            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = "TouchKeyBoard"
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, "TouchKeyBoard")
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)
            AddHandler mToolStripStatusLabel.Click, AddressOf Button_Click
        End SyncLock
    End Sub
    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        System.Threading.Thread.Sleep(50)
        cProcessStart = New clsProcessStart
        System.Threading.Thread.Sleep(50)
        cProcessStart.Start("C:\Windows\System32", "osk.exe")
    End Sub

    Public Function CreateMainButton() As Boolean
        SyncLock _Object
            Try
                cMainButtonManager.RegisterManager(Panel_Body,
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.Program.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.Program.ToString), GetType(ParentProgramForm), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.IO.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.IO.ToString), GetType(ParentIOFrom), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.Statistics.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.Statistics.ToString), GetType(ParentSystemForm), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.Devices.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.Devices.ToString), GetType(ParentDevicesForm), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.System.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.System.ToString), GetType(ParentSystemForm), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left, enumHMI_LEFT_ITEM.Home.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_ITEM.Home.ToString), GetType(ParentMainForm), enumMainButtonType.LeftMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Manual.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Manual.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Forward.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Forward.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Backward.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Backward.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString), Nothing, enumMainButtonType.FunctionMainButton),
                                     New clsMainButtonManagerCfg(TableLayoutPanel_Left_Head, enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString, cLanguageManager.GetTextLine(enumUIName.MainForm.ToString, enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString), Nothing, enumMainButtonType.FunctionMainButton)
                                     )
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub Panel_Head_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Head.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 1, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel_Left_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Left.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 1, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                                ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid)
    End Sub

    Private Sub Panel_Body_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel_Body.Paint
        ControlPaint.DrawBorder(e.Graphics, CType(sender, Panel).ClientRectangle,
                             ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 0, ButtonBorderStyle.Solid,
                             ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 1, ButtonBorderStyle.Solid,
                             ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid,
                             ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormLine), 2, ButtonBorderStyle.Solid)

    End Sub

    Private Sub Main_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If Not isInit Then Return
        cFormFontResize.WinFromH = System.Windows.Forms.Screen.GetWorkingArea(Me).Height
        cFormFontResize.newH = Me.Height

    End Sub

    Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not cMachineStatusManager.MachineStatus.bulPowerON Then
            e.Cancel = True
            bClosing = True
            RaiseEvent IamClosing()
        Else
            e.Cancel = True
        End If

    End Sub
    Public Sub LoginOutChanged()
        SyncLock _Object
            ShowUser()
            DisableMainLeftButton()
        End SyncLock
    End Sub
    Public Sub UserChanged(ByVal strVariant As String, ByVal cUserCfg As clsUserCfg)
        SyncLock _Object
            ShowUser()
            DisableMainLeftButton()
        End SyncLock
    End Sub

    Public Function Quit() As Boolean
        If Not IsNothing(cMainButtonManager) Then If Not IsNothing(cMainButtonManager.HomeUI) Then cMainButtonManager.HomeUI.Quit(cSystemElement)
        If Not IsNothing(cMainButtonManager) Then If Not IsNothing(cMainButtonManager.ParentUI) Then cMainButtonManager.ParentUI.Quit(cSystemElement)
        Me.Dispose()
        Return True
    End Function

    Public Function RaiseHMIExceptionEvent(ByVal eException As System.Exception) As Object Implements UI.IMainUI.RaiseHMIExceptionEvent
        SyncLock _Object
            Dim cExceptionForm As ExceptionForm = New ExceptionForm
            cExceptionForm.TextBox_Msg.Text = eException.Message
            cExceptionForm.TextBox_Stack.Text = eException.ToString
            cExceptionForm.ShowDialog()
            RaiseEvent IamClosing()
            Return True
        End SyncLock
    End Function

    Public Function SetCycleTime(ByVal dTime As Double) As Boolean Implements UI.IMainUI.SetCycleTime
        SyncLock _Object
            If strOldCycleTime <> "CycleTime:" & dTime.ToString("0.00") & " ms" Then
                StatusForm.Items("CycleTime").Text = "CycleTime:" & dTime.ToString("0.00") & " ms"
                strOldCycleTime = "CycleTime:" & dTime.ToString("0.00") & " ms"
            End If
            Return True
        End SyncLock
    End Function

    Public Function SetMESStatus(ByVal eDeviceStatus As UI.enumDeviceStatus) As Boolean Implements UI.IMainUI.SetMESStatus
        SyncLock _Object
            Select Case eDeviceStatus
                Case enumDeviceStatus.Close Or enumDeviceStatus.Normal
                    StatusForm.Items("MES").Image = My.Resources.gray
                Case enumDeviceStatus.NG
                    StatusForm.Items("MES").Image = My.Resources.red
                Case enumDeviceStatus.Open
                    StatusForm.Items("MES").Image = My.Resources.green
            End Select
            Return True
        End SyncLock
    End Function

    Public Function SetProcessStatus(ByVal eDeviceStatus As UI.enumDeviceStatus) As Boolean Implements UI.IMainUI.SetProcessSStatus
        SyncLock _Object
            Select Case eDeviceStatus
                Case enumDeviceStatus.Close Or enumDeviceStatus.Normal
                    StatusForm.Items("Process").Image = My.Resources.gray
                Case enumDeviceStatus.NG
                    StatusForm.Items("Process").Image = My.Resources.red
                Case enumDeviceStatus.Open
                    StatusForm.Items("Process").Image = My.Resources.green
            End Select
            Return True
        End SyncLock
    End Function

    Public Function SetPLCStatus(ByVal eDeviceStatus As UI.enumDeviceStatus) As Boolean Implements UI.IMainUI.SetPLCStatus
        SyncLock _Object
            Try
                Select Case eDeviceStatus
                    Case enumDeviceStatus.Close Or enumDeviceStatus.Normal
                        StatusForm.Items("PLC").Image = My.Resources.gray
                    Case enumDeviceStatus.NG
                        StatusForm.Items("PLC").Image = My.Resources.red
                    Case enumDeviceStatus.Open
                        StatusForm.Items("PLC").Image = My.Resources.green
                End Select
            Catch
            End Try
            Return True
        End SyncLock
    End Function

    Public Function SetStationStep(ByVal strStationID As String, ByVal iStep As Integer) As Boolean Implements UI.IMainUI.SetStationStep
        SyncLock _Object
            StatusForm.Items(strStationID).Text = "Step:" + iStep.ToString
            Return True
        End SyncLock
    End Function

    Public Function AddStation(ByVal strStationID As String) As Boolean Implements UI.IMainUI.AddStation
        SyncLock _Object
            Dim mToolStripStatusLabel As ToolStripStatusLabel
            mToolStripStatusLabel = Nothing
            mToolStripStatusLabel = New ToolStripStatusLabel
            mToolStripStatusLabel.Name = strStationID
            mToolStripStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.All
            mToolStripStatusLabel.Text = "Step" + strStationID + ":0"
            mToolStripStatusLabel.Font = New System.Drawing.Font("Calibri", 12.0!)
            StatusForm.Items.Add(mToolStripStatusLabel)
            Return True
        End SyncLock
    End Function

    Public Function RemoveStation(ByVal strStationID As String) As Boolean Implements UI.IMainUI.RemoveStation
        SyncLock _Object
            StatusForm.Items.RemoveByKey(strStationID)
            Return True
        End SyncLock
    End Function

    Public Sub DisableMainLeftButton() Implements UI.IMainUI.DisableMainLeftButton
        Label_UserLoginOut.Enabled = False
    End Sub

    Public Sub EnableMainLeftButton() Implements UI.IMainUI.EnableMainLeftButton
        cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
        Label_UserLoginOut.Enabled = Not cMachineStatusManager.MachineStatus.bulPowerON And cUserManager.CurrentUserCfg.Level >= 1 And cMainButtonManager.CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString
    End Sub

    Private Sub Label_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cUserManager.LoginOut()
        ShowUser()
        DisableMainLeftButton()
    End Sub

    Private Sub Timer_Logo_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Logo.Tick
        Label_Time.Text = Now.ToString("yyyy-MM-dd HH:mm:ss")
        Timer_Logo.Enabled = True
    End Sub

    Public Sub AutoClose() Implements UI.IMainUI.AutoClose
        bClosing = True
        RaiseEvent IamClosing()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        Select Case m.Msg
            Case &HA3
                m.WParam = IntPtr.Zero
                Exit Select
        End Select
        MyBase.WndProc(m)
    End Sub
    Private Sub DeleteFile()
        If Directory.Exists(cSystemManager.Settings.ApplicationFolder) Then
            Dim directroyInfo As DirectoryInfo = New DirectoryInfo(cSystemManager.Settings.ApplicationFolder)
            For Each tempFile As FileInfo In directroyInfo.GetFiles
                If tempFile.Extension = ".xml" Then
                    File.Delete(tempFile.FullName)
                End If
            Next
        End If
    End Sub


    Public Function InvokeAction(ByVal method As System.Delegate, ByVal ParamArray args() As Object) As Object Implements UI.IMainUI.InvokeAction
        SyncLock _Object
            Try
                If bClosing Then Return True
                Me.BeginInvoke(method, args)
                Return True
            Catch
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub MainForm_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
        If Me.Visible = False Then
            iRunTime = 20
        Else
            iRunTime = 2
        End If
    End Sub
End Class
