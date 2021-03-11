Imports Kochi.HMI.MainControl.Device
Imports System.Windows.Forms
Imports System.Drawing
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base

Public Class clsMainRunner
    Inherits clsRunnerBase
    Implements IDisposable
    Private cErrorMessageManager As clsErrorMessageManager
    Private cDeviceManager As clsDeviceManager
    Private cDeviceLibManger As clsDeviceLibManager
    Private cMainTipsManager As clsMainTipsManager
    Private cMainFormButtonManager As clsMainButtonManager
    Private cResetMainFunctionButton As MainFunctionButton
    Private cMainButtonRunner As clsMainButtonRunner
    Private cMachineDataRunner As clsMachineDataRunner
    Private cErrorAndMessageRunner As clsErrorAndMessageRunner
    Private cMachineManager As clsMachineManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cVariantManager As clsVariantManager
    Private TempMachineStatus As New StructMachineStatus
    Private cSystemManager As clsSystemManager
    Public lStationRunnerElement As New Dictionary(Of String, Object)
    Private cHMIPLC As clsHMIPLC
    Private mMainForm As IMainUI
    Private iMainUI As IMainUI
    Private mChildrenMainForm As IChildrenMainUI
    Private mParentMainForm As IParentMainUI
    Private cLanguageManager As clsLanguageManager
    Private cUserManager As clsUserManager
    Private bReset As Boolean = False
    Private _Object As New Object
    Private bDeviceChange As Boolean = False
    Public Const Name As String = "MainRunner"
    Private bAllRunning As Boolean = False

    Public Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Object
        Try
            Me.cRunnerCfg = New clsRunnerCfg(Name)
            Me.cSystemElement = cSystemElement
            Me.cRunnerElement = cRunnerElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cDeviceLibManger = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMainButtonRunner = CType(cRunnerElement(clsMainButtonRunner.Name), clsMainButtonRunner)
            cMachineDataRunner = CType(cRunnerElement(clsMachineDataRunner.Name), clsMachineDataRunner)
            cErrorAndMessageRunner = CType(cRunnerElement(clsErrorAndMessageRunner.Name), clsErrorAndMessageRunner)
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            iMainUI = CType(cSystemElement(enumUIName.MainForm.ToString), IMainUI)
            mChildrenMainForm = CType(cSystemElement(enumUIName.ChildrenMainForm.ToString), IChildrenMainUI)
            mParentMainForm = CType(cSystemElement(enumUIName.ParentMainForm.ToString), IParentMainUI)
            cResetMainFunctionButton = cMainFormButtonManager.GetMainButtonManagerCfgFromName(enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString).Source()
            cSystemElement.Add(clsMainRunner.Name, Me)
            AddHandler cResetMainFunctionButton.MainButton.Click, AddressOf Button_Click
            AddHandler mParentMainForm.Button_Clean.Click, AddressOf Button_Clean_Click
            AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        bReset = True
    End Sub
    Private Sub Button_Clean_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        cMachineStatusManager.ResetCount()
    End Sub
    Public Sub CreatTabpage()
        Dim SubTabPage As New TabPage
        SubTabPage.Margin = New System.Windows.Forms.Padding(0)
        SubTabPage.Padding = New System.Windows.Forms.Padding(0)
        SubTabPage.Name = "NA"
        SubTabPage.BackColor = Color.White
        SubTabPage.Text = "NA"

        Dim TableLayoutPanel_Tab As New TableLayoutPanel
        TableLayoutPanel_Tab.ColumnCount = 1
        TableLayoutPanel_Tab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Tab.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab.Name = "TableLayoutPanel_Tab"
        TableLayoutPanel_Tab.Font = mChildrenMainForm.TabControl_Station.Font
        TableLayoutPanel_Tab.RowCount = 2
        TableLayoutPanel_Tab.Dock = DockStyle.Fill
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140))
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        SubTabPage.Controls.Add(TableLayoutPanel_Tab)

        Dim lstResults As New ListViewEx
        lstResults.Dock = System.Windows.Forms.DockStyle.Fill
        lstResults.Font = mChildrenMainForm.TabControl_Station.Font
        lstResults.GridLines = True
        lstResults.Location = New System.Drawing.Point(0, 0)
        lstResults.Margin = New System.Windows.Forms.Padding(0)
        lstResults.Name = "lstResults"
        lstResults.OwnerDraw = True
        lstResults.Size = New System.Drawing.Size(453, 140)
        lstResults.UseCompatibleStateImageBehavior = False
        lstResults.View = System.Windows.Forms.View.Details
        lstResults.BeginUpdate()
        lstResults.Columns.Add("Nr", cLanguageManager.GetTextLine("MainRunner", "Nr"), 80)
        lstResults.Columns.Add("Action", cLanguageManager.GetTextLine("MainRunner", "Action"), 150)
        lstResults.Columns.Add("MaterialNr", cLanguageManager.GetTextLine("MainRunner", "MaterialNr"), 200)
        lstResults.Columns.Add("Result", cLanguageManager.GetTextLine("MainRunner", "Result"), 150)
        lstResults.Columns.Add("Cycle time", cLanguageManager.GetTextLine("MainRunner", "Cycle time"), 200)
        lstResults.Columns.Add("Value", cLanguageManager.GetTextLine("MainRunner", "Value"), 250)
        lstResults.Columns.Add("Comment", cLanguageManager.GetTextLine("MainRunner", "Comment"), 250)
        lstResults.EndUpdate()
        TableLayoutPanel_Tab.Controls.Add(lstResults, 0, 0)

        Dim Panel_Results As New Panel
        Panel_Results.Dock = System.Windows.Forms.DockStyle.Fill
        Panel_Results.Location = New System.Drawing.Point(0, 0)
        Panel_Results.Margin = New System.Windows.Forms.Padding(0)
        Panel_Results.Name = "Panel_Results"
        Panel_Results.Padding = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab.Controls.Add(Panel_Results, 0, 1)

        Dim TableLayoutPanel_Tab_Body As New TableLayoutPanel
        TableLayoutPanel_Tab_Body.ColumnCount = 1
        TableLayoutPanel_Tab_Body.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Tab_Body.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab_Body.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab_Body.Name = "TableLayoutPanel_Tab_Body"
        TableLayoutPanel_Tab_Body.Font = mChildrenMainForm.TabControl_Station.Font
        TableLayoutPanel_Tab_Body.RowCount = 1
        TableLayoutPanel_Tab_Body.Dock = DockStyle.Fill
        TableLayoutPanel_Tab_Body.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize))
        Panel_Results.Controls.Add(TableLayoutPanel_Tab_Body)

        Dim PictureBox_Material As New PictureBox
        PictureBox_Material.Dock = System.Windows.Forms.DockStyle.Fill
        PictureBox_Material.Location = New System.Drawing.Point(0, 0)
        PictureBox_Material.Margin = New System.Windows.Forms.Padding(0)
        PictureBox_Material.Name = "PictureBox_Material"
        PictureBox_Material.Padding = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab_Body.Controls.Add(PictureBox_Material, 0, 0)
        '
        PictureBox_Material.Image = Image.FromFile(cMachineManager.MachineCellManager.MachineCellCfg.CellPicture)
        PictureBox_Material.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom

        mChildrenMainForm.TabControl_Station.Controls.Add(SubTabPage)
    End Sub

    Public Overrides Function Run() As Boolean
        SyncLock _Object
            Try
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Return False
                Select Case i.StepOutputNumber

                    Case -100
                        mParentMainForm.EnableMainLeftButton()
                        If Not bDeviceChange Then cMainButtonRunner.PowerChanged("bulPowerON")
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -99
                        If Not CheckPLC() Then
                            i.StepInputNumber = i.Address_Home
                            Return False
                        End If

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -98
                        If Not InitPLC() Then
                            i.StepInputNumber = i.Address_Home
                            Return False
                        End If

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -97
                        If cDeviceManager.DeviceChange Then
                            i.StepInputNumber = i.Address_Home
                            Return False
                        End If
                        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                            If Not InitDevices() Then
                                i.StepInputNumber = i.Address_Home
                                Return False
                            End If
                            cDeviceManager.DeviceChange = False
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case -96
                        If Not AddAdsVariable() Then
                            i.StepInputNumber = i.Address_Home
                            Return False
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -95
                        'If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                        InitStation()
                        i.StepInputNumber = i.StepOutputNumber + 1
                        ' End If

                    Case -94
                        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString Then
                            mParentMainForm.AutoLogin()
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case -93
                        If cUserManager.CurrentUserCfg.Level > enumUserLevel.Normal Then
                            cMainFormButtonManager.EnableMainLeftButton()
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case -92
                        Dim iStationNr As Integer = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count
                        If iStationNr <= 0 Then iStationNr = 0
                        If iStationNr > 0 Then
                            Dim cVariantInfo() As StructVariantInfo = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_VariantInfo, GetType(StructVariantInfo()), New Integer() {iStationNr})
                            If cVariantInfo.Length >= 1 Then
                                If cVariantInfo(0).strKostalNr <> "" AndAlso cVariantManager.HasVariant(cVariantInfo(0).strKostalNr) Then
                                    If cVariantManager.CurrentVariantCfg.Variant <> cVariantInfo(0).strKostalNr Then cVariantManager.ChangeVariant(cVariantInfo(0).strKostalNr)
                                Else
                                    mParentMainForm.ChangePageToVariant()
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cLanguageManager.GetTextLine("MainRunner", "8", cHMIPLC.Name)))
                                End If
                            End If
                        Else
                            mParentMainForm.ChangePageToVariant()
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cLanguageManager.GetTextLine("MainRunner", "8", cHMIPLC.Name)))
                        End If
                        bDeviceChange = False


                        i.StepInputNumber = i.Address_Home

                    Case 0

                        If cDeviceManager.DeviceChange Then
                            cDeviceManager.DeviceChange = False
                            bDeviceChange = True
                            i.StepInputNumber = 100
                            Return True
                        End If

                        If cMachineManager.MachineCellManager.CellChanged Then
                            i.StepInputNumber = 200
                            Return True
                        End If

                        If bReset And cDeviceManager.PLCDeviceError Then
                            bReset = False
                            i.StepInputNumber = -100
                            Return True
                        End If

                        If Not cDeviceManager.PLCDeviceError Then
                            cMainButtonRunner.Run()
                            cMachineDataRunner.Run()
                            cErrorAndMessageRunner.Run()
                        End If

                        If bReset And cDeviceManager.DeviceError Then
                            bReset = False
                            i.StepInputNumber = -98
                            Return True
                        End If
                        bReset = False


                        bAllRunning = False
                        If cMachineStatusManager.MachineStatus.bulPowerON And Not cMachineStatusManager.MachineStatus.bulDebugMode And Not cDeviceManager.PLCDeviceError And Not cDeviceManager.DeviceError And cMachineDataRunner.bChangeResouse Then
                            For Each element As clsStationRunnerBase In lStationRunnerElement.Values
                                element.Run()
                                If element.Running Then
                                    bAllRunning = True
                                End If
                            Next
                            CType(cMainFormButtonManager.GetMainButtonManagerCfgFromName(enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString).Source, MainFunctionButton).FunctionEnable = Not bAllRunning
                        End If

                        If TempMachineStatus.bulPowerON And Not cMachineStatusManager.MachineStatus.bulPowerON And Not cDeviceManager.PLCDeviceError And Not cDeviceManager.DeviceError And cMachineDataRunner.bChangeResouse Then
                            TempMachineStatus.bulPowerON = cMachineStatusManager.MachineStatus.bulPowerON
                            i.StepInputNumber = 300
                            Return True
                        End If
                        TempMachineStatus.bulPowerON = cMachineStatusManager.MachineStatus.bulPowerON

                        ' If Not TempMachineStatus.bulDebugMode And cMachineStatusManager.MachineStatus.bulDebugMode And Not cDeviceManager.PLCDeviceError And Not cDeviceManager.DeviceError Then
                        ' TempMachineStatus.bulDebugMode = cMachineStatusManager.MachineStatus.bulDebugMode
                        '   i.StepInputNumber = 300
                        '     Return True
                        '   End If
                        TempMachineStatus.bulDebugMode = cMachineStatusManager.MachineStatus.bulDebugMode

                        ' If Not TempMachineStatus.bulTeachMode And cMachineStatusManager.MachineStatus.bulTeachMode And Not cDeviceManager.PLCDeviceError And Not cDeviceManager.DeviceError Then
                        '   TempMachineStatus.bulTeachMode = cMachineStatusManager.MachineStatus.bulTeachMode
                        '  i.StepInputNumber = 300
                        '   Return True
                        '  End If
                        TempMachineStatus.bulTeachMode = cMachineStatusManager.MachineStatus.bulTeachMode

                    Case 100
                        For Each element As clsStationRunnerBase In lStationRunnerElement.Values
                            element.Quit()
                        Next

                        cMainButtonRunner.Reset()
                        cMachineDataRunner.Reset()
                        cErrorAndMessageRunner.Reset()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 101
                        If Not RemoveAdsVariable() Then Return False
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 102
                        ' If Not CloseDeives() Then Return False
                        i.StepInputNumber = i.Address_Origin

                    Case 200
                        For Each element As clsStationRunnerBase In lStationRunnerElement.Values
                            element.Quit()
                        Next
                        cMainButtonRunner.Reset()
                        cMachineDataRunner.Reset()
                        cErrorAndMessageRunner.Reset()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 201
                        InitStation()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 202
                        cMachineManager.MachineCellManager.CellChanged = False
                        i.StepInputNumber = i.Address_Home

                    Case 300
                        For Each element As clsStationRunnerBase In lStationRunnerElement.Values
                            element.Reset()
                        Next
                        CType(cMainFormButtonManager.GetMainButtonManagerCfgFromName(enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString).Source, MainFunctionButton).FunctionEnable = True
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 301
                        cMainButtonRunner.Run()
                        cMachineDataRunner.Run()
                        cErrorAndMessageRunner.Run()
                        If Not cMachineStatusManager.MachineStatus.bulTeachMode And Not cMachineStatusManager.MachineStatus.bulDebugMode Then
                            i.StepInputNumber = i.Address_Home
                        End If


                End Select
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckPLC() As Boolean
        cDeviceManager.PLCDeviceError = False
        cHMIPLC = cDeviceManager.GetPLCDevice()
        iMainUI.SetPLCStatus(enumDeviceStatus.Normal)
        If IsNothing(cHMIPLC) Then
            Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "1"))
            cDeviceManager.PLCDeviceError = True
            i.StepInputNumber = i.Address_Home
            Return False
        End If
        Return True
    End Function

    Public Function InitPLC() As Boolean
        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cLanguageManager.GetTextLine("MainRunner", "2", cHMIPLC.Name)))
        Try
            If IsNothing(cHMIPLC) Then
                Return True
            End If
            If cHMIPLC.DeviceState = enumDeviceState.OPEN Then
                Return True
            End If
            If Not cHMIPLC.Init(cLocalElement, cSystemElement, clsParameter.ToList(cDeviceManager.GetDeviceFromName(cHMIPLC.Name).InitParameter), clsParameter.ToList(cDeviceManager.GetDeviceFromName(cHMIPLC.Name).ControlParameter)) Then
                cHMIPLC.DeviceState = enumDeviceState.ERROR
                Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "3", cHMIPLC.Name))
                cDeviceManager.PLCDeviceError = True
                cDeviceManager.DeviceError = True
                iMainUI.SetPLCStatus(enumDeviceStatus.NG)
                i.StepInputNumber = i.Address_Home
                Return False
            End If
            cHMIPLC.DeviceState = enumDeviceState.OPEN
        Catch ex As Exception
            cHMIPLC.DeviceState = enumDeviceState.ERROR
            Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "4", cHMIPLC.Name, ex.Message))
            cDeviceManager.PLCDeviceError = True
            cDeviceManager.DeviceError = True
            iMainUI.SetPLCStatus(enumDeviceStatus.NG)
            i.StepInputNumber = i.Address_Home
            Return False
        End Try
        iMainUI.SetPLCStatus(enumDeviceStatus.Open)
        cHMIPLC.DeviceState = enumDeviceState.OPEN
        Return True
    End Function


    Public Function ClosePLC() As Boolean

        Try
            If IsNothing(cHMIPLC) Then
                Return True
            End If
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cLanguageManager.GetTextLine("MainRunner", "7", cHMIPLC.Name)))
            If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                Return True
            End If
            If Not cHMIPLC.Quit(cLocalElement, cSystemElement) Then
                cHMIPLC.DeviceState = enumDeviceState.ERROR
                Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "3", cHMIPLC.Name))
                cDeviceManager.PLCDeviceError = True
                cDeviceManager.DeviceError = True
                iMainUI.SetPLCStatus(enumDeviceStatus.NG)
                i.StepInputNumber = i.Address_Home
                Return False
            End If
        Catch ex As Exception
            cHMIPLC.DeviceState = enumDeviceState.ERROR
            Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "4", cHMIPLC.Name, ex.Message))
            cDeviceManager.PLCDeviceError = True
            cDeviceManager.DeviceError = True
            iMainUI.SetPLCStatus(enumDeviceStatus.NG)
            i.StepInputNumber = i.Address_Home
            Return False
        End Try
        iMainUI.SetPLCStatus(enumDeviceStatus.Close)
        cHMIPLC.DeviceState = enumDeviceState.CLOSE
        Return True
    End Function

    Public Function InitDevices() As Boolean
        cDeviceManager.DeviceError = False
        For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
            Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
            Try
                If TypeOf element.Source Is clsHMIPLC Then
                Else
                    If element.Source.DeviceState <> enumDeviceState.OPEN Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cLanguageManager.GetTextLine("MainRunner", "2", element.Name)))

                        If Not CType(element.Source, IHMIDeviceBase).Init(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter)) Then
                            element.Source.DeviceState = enumDeviceState.ERROR
                            Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "3", element.Name))
                            cDeviceManager.DeviceError = True
                        End If
                    End If
                    element.Source.DeviceState = enumDeviceState.OPEN
                End If
            Catch ex As Exception
                element.Source.DeviceState = enumDeviceState.ERROR
                Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "4", element.Name, ex.Message))
                cDeviceManager.DeviceError = True
            End Try
        Next
        cMainTipsManager.CleanStationTips("MainRunner")
        Return True
    End Function

    Public Function CloseDeives() As Boolean
        For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
            Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
            Try
                If TypeOf element.Source Is clsHMIPLC Then
                Else
                    If Not IsNothing(element.Source) Then
                        If element.Source.DeviceState = enumDeviceState.OPEN Then
                            If Not CType(element.Source, IHMIDeviceBase).Quit(cLocalElement, cSystemElement) Then
                                element.Source.DeviceState = enumDeviceState.ERROR
                                Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "5", element.Name))
                            End If
                            element.Source.DeviceState = enumDeviceState.CLOSE
                        End If
                    End If
                End If
            Catch ex As Exception
                element.Source.DeviceState = enumDeviceState.ERROR
                Throw New clsHMIException(cLanguageManager.GetTextLine("MainRunner", "6", element.Name, ex.Message))
            End Try
        Next
        Return True
    End Function

    Public Function InitStation() As Boolean
        Try
            cMachineStatusManager.Remove()
            cMachineStatusManager.LoadConfig()
            lStationRunnerElement.Clear()
            Try
                mChildrenMainForm.TabControl_Station.Controls.Clear()
            Catch ex As Exception
                mChildrenMainForm.TabControl_Station.Controls.Clear()
            End Try
            If cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count = 0 Then
                CreatTabpage()
            Else
                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    Dim TempStationRunner As New clsStationRunner
                    TempStationRunner.MachineStationCfg = element
                    TempStationRunner.Init(cSystemElement, cRunnerElement)
                    lStationRunnerElement.Add(element.ID, TempStationRunner)
                    iMainUI.AddStation(element.ID)
                Next
            End If
            cMachineStatusManager.Add()

            cMachineStatusManager.LoadPage()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function


    Public Function AddAdsVariable() As Boolean
        Try
            Dim iStationNr As Integer = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count
            If iStationNr <= 0 Then iStationNr = 1
            If iStationNr = 0 Then Return True

            Dim cDefaultRequestValue() As StructRequestAction = Enumerable.Repeat(New StructRequestAction, iStationNr * 3).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_RequestAction, GetType(StructRequestAction()), cDefaultRequestValue, New Integer() {iStationNr * 3})

            Dim cDefaultResponseValue() As StructResponseAction = Enumerable.Repeat(New StructResponseAction, iStationNr * 3).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ResponseAction, GetType(StructResponseAction()), cDefaultResponseValue, New Integer() {iStationNr * 3})

            Dim cDefaultActionValue() As StructHmiAction = Enumerable.Repeat(New StructHmiAction, iStationNr).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_HmiAction, GetType(StructHmiAction()), cDefaultActionValue, New Integer() {iStationNr})


            Dim cDefaultActionNrValue() As Byte = Enumerable.Repeat(New Byte, iStationNr).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_bytCurrentActionNr, GetType(Byte()), cDefaultActionNrValue, New Integer() {iStationNr})

            Dim cDefaultVariantInfo() As StructVariantInfo = Enumerable.Repeat(New StructVariantInfo, iStationNr).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_VariantInfo, GetType(StructVariantInfo()), cDefaultVariantInfo, New Integer() {iStationNr})

            Dim cDefaultStructAutoButton() As StructAutoButton = Enumerable.Repeat(New StructAutoButton, iStationNr).ToArray()
            cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_AutoButton, GetType(StructAutoButton()), cDefaultStructAutoButton, New Integer() {iStationNr})

            Dim iCnt As Integer = 0
            For iCnt = 1 To iStationNr
                Dim cDefaultStructActionParameterValue() As StructActionParameter_Char = Enumerable.Repeat(New StructActionParameter_Char, 100).ToArray()
                cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_ActionStep + "[" + iCnt.ToString + "]", GetType(StructActionParameter_Char()), cDefaultStructActionParameterValue, New Integer() {100})
            Next

            For iCnt = 1 To iStationNr
                Dim cDefaultStructActionParameterValue() As StructPLCAutoActionParameter_Char = Enumerable.Repeat(New StructPLCAutoActionParameter_Char, 100).ToArray()
                cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_AutoActionStep + "[" + iCnt.ToString + "]", GetType(StructPLCAutoActionParameter_Char()), cDefaultStructActionParameterValue, New Integer() {100})
            Next

            If cMachineManager.MachineCellManager.HasAutoStation Then

                Dim cDefaultbyTargetActionNr() As Byte = Enumerable.Repeat(New Byte, iStationNr).ToArray()
                cHMIPLC.AddNotificationEx(HMI_PLC_Interface.HMI_bytTargetActionNr, GetType(Byte()), cDefaultbyTargetActionNr, New Integer() {iStationNr})

                'For iCnt = 1 To iStationNr
                '    Dim cDefaultStructPLCActionParameterValue() As StructPLCAutoActionParameter_Char = Enumerable.Repeat(New StructPLCAutoActionParameter_Char, 100).ToArray()
                '    cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_AutoActionStep + "[" + iCnt.ToString + "].PLC_AutoActionParameter", GetType(StructPLCAutoActionParameter_Char()), cDefaultStructPLCActionParameterValue, New Integer() {100})
                'Next
            End If
            'mMainForm.InvokeAction(Sub() cHMIPLC.AddNotificationEx(HMI_PLC_Interface.PLC_AutoActionStep + "[" + iCnt.ToString + "]", GetType(StructPLCAutoActionStep), New StructPLCAutoActionStep))
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function RemoveAdsVariable() As Boolean
        Try
            Dim iStationNr As Integer = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count
            If iStationNr <= 0 Then iStationNr = 0
            If iStationNr = 0 Then Return True
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.PLC_RequestAction)
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_ResponseAction)
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_HmiAction)
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_bytCurrentActionNr)
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_VariantInfo)
            cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.PLC_AutoButton)
            Dim iCnt As Integer = 0
            For iCnt = 1 To iStationNr
                mMainForm.InvokeAction(Sub() cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_ActionStep + "[" + iCnt.ToString + "]"))
                mMainForm.InvokeAction(Sub() cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.PLC_AutoActionStep + "[" + iCnt.ToString + "]"))
            Next
            For iCnt = 1 To iStationNr
                'mMainForm.InvokeAction(Sub() cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.PLC_AutoActionStep + "[" + iCnt.ToString + "].bulHmiDoAction"))
            Next
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Overrides Function Reset() As Boolean
        Try
            i.StepInputNumber = i.Address_Origin
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Private Sub VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
        cMainTipsManager.CleanStationTips(cHMIPLC.Name)
    End Sub

    Public Overrides Function Quit() As Boolean
        Try
            For Each element As clsStationRunnerBase In lStationRunnerElement.Values
                element.Quit()
            Next
            If cSystemElement.ContainsKey(clsMainRunner.Name) Then
                cSystemElement.Remove(clsMainRunner.Name)
            End If
            cMainButtonRunner.Quit()
            cMachineDataRunner.Quit()
            cErrorAndMessageRunner.Quit()
            CloseDeives()
            ClosePLC()
            RemoveHandler cVariantManager.VariantChanged, AddressOf VariantChanged
            Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
