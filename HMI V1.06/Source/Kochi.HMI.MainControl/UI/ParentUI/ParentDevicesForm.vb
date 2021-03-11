Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Threading

Public Class ParentDevicesForm
    Implements IParentUI
    Private cLocalElement As New Dictionary(Of String, Object)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cFormFontResize As clsFormFontResize
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDeviceManager As clsDeviceManager
    Private cHmiDevice As clsHMIDeviceBase
    Private strButtonName As String
    Private strLastDeviceName As String = ""
    Private strLastStationName As String = ""
    Private cLocalFormFontResize As clsFormFontResize
    Private mMainForm As MainForm
    Private cHMIPLC As clsHMIPLC
    Private ePageMode As enumPageMode
    Private cUserManager As clsUserManager
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cThread As Thread
    Private bExit As Boolean
    Private lListStationTap As New Dictionary(Of String, TabControl)
    Private cProgramDebug As ProgramDebug
    Private bWaiting As Boolean = False
    Public Property ButtonName As String Implements IParentUI.ButtonName
        Get
            Return strButtonName
        End Get
        Set(ByVal value As String)
            strButtonName = value
        End Set
    End Property

    Public ReadOnly Property UI As System.Windows.Forms.Panel Implements IParentUI.UI
        Get
            Return Panel_Body
        End Get
    End Property
    Public ReadOnly Property LocalElement As Dictionary(Of String, Object) Implements IParentUI.LocalElement
        Get
            Return cLocalElement
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Init
        Me.cSystemElement = cSystemElement
        cFormFontResize = CType(cSystemElement(clsFormFontResize.Name), clsFormFontResize)
        cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
        mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), MainForm)

        cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
        cErrorMessageManager = New clsErrorMessageManager
        cLocalFormFontResize = New clsFormFontResize
        cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
        cLocalElement.Add(clsFormFontResize.Name, cLocalFormFontResize)
        cErrorMessageManager.Init(cSystemElement)
        cErrorMessageManager.RegisterManager(TableLayoutPanel_Body)
        cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
        cHMIPLC = cDeviceManager.GetPLCDevice()
        cProgramDebug = New ProgramDebug
        cProgramDebug.Init(cLocalElement, cSystemElement)
        GetPageMode()
        InitForm()
        InitControlText()
        Timer1.Enabled = True
        cLocalElement.Add(enumUIName.ParentDevicesForm.ToString, Me)
        Return True
    End Function


    Public Function InitForm() As Boolean
        Panel_Body.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_FormMid)
        TopLevel = False
        AddHandler TabControl_Devices.SelectedIndexChanged, AddressOf TabControl_Station_SelectedIndexChanged
        Return True
    End Function

    Public Function InitControlText() As Boolean
        If cMachineManager.MachineStatus.bulPowerON Then
            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode", False)
            If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", True)
        End If
        Return True
    End Function

    Public Sub GetPageMode()
        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Administrator Then
            ePageMode = enumPageMode.Edit
        Else
            ePageMode = enumPageMode.Debug
        End If
    End Sub

    Public Function CreateTabPage() As Boolean
        Try
            Dim SubTabPage As New TabPage
            TabControl_Devices.Enabled = False
            lListStationTap.Clear()
            SubTabPage = New TabPage
            SubTabPage.Margin = New System.Windows.Forms.Padding(0)
            SubTabPage.Padding = New System.Windows.Forms.Padding(0)
            SubTabPage.Font = TabControl_Devices.Font
            SubTabPage.Name = "Debug"
            SubTabPage.Text = cLanguageManager.GetTextLine(enumUIName.ParentDevicesForm.ToString, "Debug")
            SubTabPage.BackColor = Color.White
            cFormFontResize.SetControls(cFormFontResize.CurrentRate, cProgramDebug.UI)
            SubTabPage.Controls.Add(cProgramDebug.UI)
            TabControl_Devices.Controls.Add(SubTabPage)
            '  AddHandler cProgramDebug.ChangedPage, AddressOf ChangedPage

            For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                SubTabPage = New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Font = TabControl_Devices.Font
                SubTabPage.Name = element.ID
                SubTabPage.Text = element.StationName
                SubTabPage.BackColor = Color.White

                Dim cTabControl As New TabControl
                cTabControl.Margin = New System.Windows.Forms.Padding(0)
                cTabControl.Font = TabControl_Devices.Font
                cTabControl.Name = element.ID
                cTabControl.Text = element.StationName
                cTabControl.BackColor = Color.White
                cTabControl.Dock = DockStyle.Fill
                lListStationTap.Add(element.ID, cTabControl)
                SubTabPage.Controls.Add(cTabControl)
                TabControl_Devices.Controls.Add(SubTabPage)
                AddHandler cTabControl.SelectedIndexChanged, AddressOf TabControl_Devices_SelectedIndexChanged
                Application.DoEvents()
                ' System.Threading.Thread.Sleep(10)
                If bExit Then Return True
            Next

            For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
                Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
                SubTabPage = New TabPage
                SubTabPage.Margin = New System.Windows.Forms.Padding(0)
                SubTabPage.Padding = New System.Windows.Forms.Padding(0)
                SubTabPage.Font = TabControl_Devices.Font
                SubTabPage.Name = element.Name
                SubTabPage.Text = element.Name
                SubTabPage.BackColor = Color.White
                cHmiDevice = element.Source
                If IsNothing(cHmiDevice) Then Continue For
                AddHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
                cHmiDevice.CreateControlUI(cLocalElement, cSystemElement)
                If IsNothing(cHmiDevice.ControlUI) Then Continue For
                cHmiDevice.ControlUI.Init(cLocalElement, cSystemElement)
                cHmiDevice.ControlUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter))
                cFormFontResize.SetControls(cFormFontResize.CurrentRate, cHmiDevice.ControlUI.UI)
                SubTabPage.Controls.Add(cHmiDevice.ControlUI.UI)
                If lListStationTap.ContainsKey(element.StationID) Then lListStationTap(element.StationID).Controls.Add(SubTabPage)
                '  Application.DoEvents()
                '  System.Threading.Thread.Sleep(10)
                If bExit Then Return True
            Next
            StartRefreshUI()
            TabControl_Devices.Enabled = True
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, enumUIName.ParentDevicesForm.ToString))
            Return False
        End Try
    End Function

    Public Function StopRefreshUI() As Boolean
        cProgramDebug.StopRefreshUI()
        Return True
    End Function

    Public Function StartRefreshUI() As Boolean
        strLastStationName = "Debug"
        cProgramDebug.StartRefreshUI()
        Return True
    End Function

    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean Implements IParentUI.Quit
        Try
            Timer1.Enabled = False
            Do While bWaiting
                System.Threading.Thread.Sleep(5)
            Loop
            StopRefreshUI()
            If Not IsNothing(cProgramDebug) Then cProgramDebug.Quit(cLocalElement, cSystemElement)
            For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
                Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
                cHmiDevice = element.Source
                If IsNothing(cHmiDevice) Then Continue For
                If IsNothing(cHmiDevice.ControlUI) Then Continue For
                RemoveHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
                If Not cHmiDevice.ControlUI.Quit(cLocalElement, cSystemElement) Then
                    Return False
                End If
            Next

            '     If Not IsNothing(cProgramDebug) Then RemoveHandler cProgramDebug.ChangedPage, AddressOf ChangedPage
            cErrorMessageManager.Clean(enumUIName.ParentDevicesForm.ToString)
            cErrorMessageManager.Dispose()
            cLocalElement.Remove(enumUIName.ParentDevicesForm.ToString)
            Me.Dispose()
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex, enumExceptionType.Crash)
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


    Public Sub ParameterChanged(ByVal sender As Object, ByVal e As ParameterEvent)
        Try
            cDeviceManager.SaveControlParameter(lListStationTap(TabControl_Devices.SelectedTab.Name).SelectedTab.Name, clsParameter.ToString(e.ListParameter))
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(ex)
        End Try
    End Sub


    Private Sub TabControl_Station_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If strLastStationName = "Debug" Then
            StopRefreshUI()
        Else
            If Not IsNothing(lListStationTap(strLastStationName).SelectedTab) Then
                strLastDeviceName = lListStationTap(strLastStationName).SelectedTab.Name
                CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ControlUI.StopRefresh(cLocalElement, cSystemElement)
            End If
        End If
        strLastStationName = TabControl_Devices.SelectedTab.Name
        If strLastStationName = "Debug" Then
            StartRefreshUI()
        Else
            If Not IsNothing(lListStationTap(strLastStationName).SelectedTab) Then
                strLastDeviceName = lListStationTap(strLastStationName).SelectedTab.Name
                CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ControlUI.StartRefresh(cLocalElement, cSystemElement)
            Else
                strLastDeviceName = ""
            End If
        End If

    End Sub

    Private Sub TabControl_Devices_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If strLastDeviceName <> "" Then CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ControlUI.StopRefresh(cLocalElement, cSystemElement)
        strLastDeviceName = sender.SelectedTab.Name
        CType(cDeviceManager.GetDeviceFromName(strLastDeviceName).Source, clsHMIDeviceBase).ControlUI.StartRefresh(cLocalElement, cSystemElement)
    End Sub

    Private Sub Panel_UI_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel_UI.Resize
        If Not IsNothing(cLocalFormFontResize) Then
            cLocalFormFontResize.WinFromH = mMainForm.Panel_Body.Height
            cLocalFormFontResize.newExH = Panel_UI.Height
            If cFormFontResize.Resized And cLocalFormFontResize.Resized Then
                cLocalFormFontResize.cons = Panel_UI
                cLocalFormFontResize.ChangeFontSize()
            End If
            cLocalFormFontResize.OldExH = Panel_UI.Height
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        bWaiting = True
        Timer1.Enabled = False
        CreateTabPage()
        bWaiting = False
    End Sub
    Private Sub ChangedPage()
        StopRefreshUI()
        Dim SubTabPage As New TabPage
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            RemoveHandler lListStationTap(element.ID).SelectedIndexChanged, AddressOf TabControl_Devices_SelectedIndexChanged
            lListStationTap(element.ID).Controls.Clear()
        Next


        For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
            Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
            SubTabPage = New TabPage
            SubTabPage.Margin = New System.Windows.Forms.Padding(0)
            SubTabPage.Padding = New System.Windows.Forms.Padding(0)
            SubTabPage.Font = TabControl_Devices.Font
            SubTabPage.Name = element.Name
            SubTabPage.Text = element.Name
            SubTabPage.BackColor = Color.White
            If Not IsNothing(cHmiDevice) Then
                RemoveHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
            End If

            cHmiDevice = element.Source
            If IsNothing(cHmiDevice) Then Continue For
            AddHandler cHmiDevice.ParameterChanged, AddressOf ParameterChanged
            cHmiDevice.CreateControlUI(cLocalElement, cSystemElement)
            If IsNothing(cHmiDevice.ControlUI) Then Continue For
            cHmiDevice.ControlUI.Init(cLocalElement, cSystemElement)
            cHmiDevice.ControlUI.SetParameter(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter))
            cFormFontResize.SetControls(cFormFontResize.CurrentRate, cHmiDevice.ControlUI.UI)
            SubTabPage.Controls.Add(cHmiDevice.ControlUI.UI)

            lListStationTap(element.StationID).Controls.Add(SubTabPage)
            '  Application.DoEvents()
            '  System.Threading.Thread.Sleep(10)
            If bExit Then Return
        Next
        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
            AddHandler lListStationTap(element.ID).SelectedIndexChanged, AddressOf TabControl_Devices_SelectedIndexChanged
        Next
        StartRefreshUI()
    End Sub
End Class