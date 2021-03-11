Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl
Imports Kochi.HMI.MainControl.UI
Imports System.Windows.Forms
Imports System.Collections.Concurrent
Imports System.Threading
Imports Kochi.HMI.MainControl.Base
Imports System.Drawing
Imports Kochi.HMI.MainControl.Action

Public Class clsStationRunner
    Inherits clsStationRunnerBase
    Implements IDisposable
    Private cErrorMessageManager As clsErrorMessageManager
    Private mChildrenMainForm As IChildrenMainUI
    Private cSystemManager As clsSystemManager
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cMainTipsManager As clsMainTipsManager
    Private cMachineManager As clsMachineManager
    Private cProductionDataManager As clsProductionDataManager
    Private cActionDataManager As clsActionDataManager
    Private cVariantManager As clsVariantManager
    Private cLocalVariantManager As clsVariantManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private TempRequestAction() As StructRequestAction
    Private cActionRequestAction(3) As StructRequestAction
    Private cPLCAutoActionParameter_Char() As StructPLCAutoActionParameter_Char
    Private cPLCAutoHmiDoAction As Boolean
    Private TempResponseAction() As StructResponseAction
    Private cHmiAction As StructHmiAction
    Private bytCurrentActionNr As Byte
    Private bytTargetActionNr As Byte
    Private cActionResponseAction(3) As StructResponseAction
    Private strInterErrorMsg As String = String.Empty
    Private cActionShowManager As clsActionShowManager

    Private cActionManager As clsActionManager
    Private cGlobalActionManager As clsActionManager
    Private cLogHandler As clsLogHandler
    Private cActionLibManager As clsActionLibManager
    Private cLanguageManager As clsLanguageManager
    Private mMainForm As IMainUI
    Private iMainUI As IMainUI
    Private cThread As Thread
    Private cRunActionCfg As New clsRunActionCfg
    Private strStartTime As String
    Private _Object As New Object
    Private iCntAction As Integer = 0
    Private iCntGlobalAction As Integer = 0
    Private iLastMainAction As Integer = 0
    Private jCntAction As Integer = 0
    Private jCntGlobalAction As Integer = 0
    Private iTargetID As Integer = 0
    Private bAutoStep As Boolean = False
    Private bAuotParameter As Boolean = False
    Private lListMainStepCfg As New List(Of clsMainStepCfg)
    Private lListGlobalMainStepCfg As New List(Of clsMainStepCfg)
    Private lListSubStepCfg As New List(Of clsSubStepCfg)
    Private lListGlobalSubStepCfg As New List(Of clsSubStepCfg)
    Private lListShowAction As New List(Of clsShowActionCfg)
    Private cStationErrorCodeManager As clsStationErrorCodeManager
    Private cSubStepCfg As clsSubStepCfg
    Private cSubGlobalStepCfg As clsSubStepCfg
    Private strTempValue As String
    Private strTempComponentValue As String
    Private strTempFilePath As String
    Private iInterRework As Integer = 0
    Private iInterGlobalRework As Integer = 0
    Private iPLCAutoIndex As Integer = 0
    Private bulPLCParameter As Boolean = False
    Private swAction As Stopwatch
    Private eActionType As enumActionType
    Private strPostion As String = ""
    Private bRecord As Boolean = False
    Private strMainMessage As String = ""
    Private cStartTime As DateTime
    Private sw As New Stopwatch
    Private bStartTest As Boolean = False
    Public Const Name As String = "StationRunner"
    Private bLastSubActionType As String = ""
    Private bLastGlobalSubActionType As String = ""
    Private lListRepeat As New Dictionary(Of String, Integer)
    Private lListSuccess As New Dictionary(Of String, Integer)
    Private cBarcodeManager As clsBarcodeManager
    Private cMainFormButtonManager As clsMainButtonManager
    Private cPreviousMachineStationCfg As clsMachineStationCfg
    Private cFailureActionManager As clsFailureActionManager
    Private cProductionMESData As New clsProductionMESData
    Private lListError As New List(Of clsActionResultCfg)
    Private strShowErrorMessage As String = ""
    Private cBackUp As New clsActionResultCfg
    Private iBackStep As Integer = 0
    Private eBackActionType As enumActionType
    Private bOtherStationInqueue As Boolean
    Private bOtherStationInqueue2 As Boolean
    Private strOtherStationInqueue As String
    Private bCancelUpdate As Boolean = False


    Public Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Boolean
        Try

            Me.cRunnerCfg = New clsRunnerCfg(MachineStationCfg.ID.ToString)
            Me.cSystemElement = cSystemElement
            Me.cRunnerElement = cRunnerElement
            mChildrenMainForm = CType(cSystemElement(enumUIName.ChildrenMainForm.ToString), IChildrenMainUI)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Init Station"))
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cProductionDataManager = New clsProductionDataManager
            cActionDataManager = New clsActionDataManager
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
            iMainUI = CType(cSystemElement(enumUIName.MainForm.ToString), IMainUI)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cStationErrorCodeManager = CType(cSystemElement(clsStationErrorCodeManager.Name), clsStationErrorCodeManager)
            cActionShowManager = New clsActionShowManager
            cActionShowManager.Init(cSystemElement)
            cPictureShowManager = New clsPictureShowManager
            cPictureShowManager.Init(cSystemElement)
            cProductionDataManager.Init(cSystemElement)
            cActionDataManager.Init(cSystemElement)
            mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
            mMainForm.InvokeAction(Sub() CreatTabpage())
            cActionManager = New clsActionManager
            cActionManager.Init(cSystemElement)
            cGlobalActionManager = New clsActionManager
            cGlobalActionManager.Init(cSystemElement)

            cFailureActionManager = New clsFailureActionManager
            cFailureActionManager.Init(cSystemElement)
            cProductionMESData = New clsProductionMESData
            cProductionMESData.Init(cSystemElement)

            cActionLibManager = New clsActionLibManager
            cActionLibManager.Init(cSystemElement)
            cHMIPLC = cDeviceManager.GetPLCDevice()

            cBarcodeManager = New clsBarcodeManager
            cBarcodeManager.Init(cSystemElement)

            cLocalVariantManager = New clsVariantManager
            cLocalVariantManager.Init(cSystemElement)
            cLocalVariantManager.LoadVariantCfg()
            If cVariantManager.CurrentVariantCfg.Variant <> "" Then
                cLocalVariantManager.ChangeVariant(cVariantManager.CurrentVariantCfg.Variant)
                SetVariant(cVariantManager.CurrentVariantCfg.Variant, enumSelectVariantType.Manual)
            End If

            cMachineStatusManager.RegisterVariantManager(cRunnerCfg.StationName, cLocalVariantManager)
            cMachineStatusManager.RegisterPictureShowManager(cRunnerCfg.StationName, cPictureShowManager)

            cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg)
            cLocalElement.Add(clsActionShowManager.Name, cActionShowManager)
            cLocalElement.Add(clsPictureShowManager.Name, cPictureShowManager)
            cLocalElement.Add(clsMachineStatusManager.Name, cMachineStatusManager)
            cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
            cLocalElement.Add(clsSubStepCfg.Name, New clsSubStepCfg(cSystemElement))
            cLocalElement.Add(clsMainStepCfg.Name, New clsMainStepCfg(cSystemElement))
            cLocalElement.Add(clsActionResultCfg.Name, New clsActionResultCfg)
            cLocalElement.Add(clsPLCAction.Name, New clsPLCAction)
            cLocalElement.Add(clsRunnerCfg.Name, cRunnerCfg)
            cLocalElement.Add(clsBarcodeManager.Name, cBarcodeManager)
            cLocalElement.Add(clsVariantManager.Name, cLocalVariantManager)
            cLocalElement.Add(clsMachineStatusCfg.Name, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName))

            If cMachineStationCfg.ID > 1 Then
                cPreviousMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(cMachineStationCfg.ID - 1)
            Else
                cPreviousMachineStationCfg = Nothing
            End If
            cMainTipsManager.CleanStationTips(cRunnerCfg.StationName)
            AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
            AddHandler cVariantManager.LoadChanged, AddressOf LoadChanged
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function
    Public Sub CreatTabpage()
        Dim SubTabPage As New TabPage
        SubTabPage.Margin = New System.Windows.Forms.Padding(0)
        SubTabPage.Padding = New System.Windows.Forms.Padding(0)
        SubTabPage.Name = cMachineStationCfg.ID.ToString
        SubTabPage.BackColor = Color.White
        SubTabPage.Text = cMachineStationCfg.StationName


        Dim TableLayoutPanel_Tab As New TableLayoutPanel
        TableLayoutPanel_Tab.ColumnCount = 1
        TableLayoutPanel_Tab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        TableLayoutPanel_Tab.Dock = System.Windows.Forms.DockStyle.Fill
        TableLayoutPanel_Tab.Margin = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab.Name = "TableLayoutPanel_Tab"
        TableLayoutPanel_Tab.Font = mChildrenMainForm.TabControl_Station.Font
        TableLayoutPanel_Tab.RowCount = 2
        TableLayoutPanel_Tab.Dock = DockStyle.Fill
        TableLayoutPanel_Tab.Font = New System.Drawing.Font("Calibri", 12.0!)
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140))
        TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100))
        ' TableLayoutPanel_Tab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30))
        SubTabPage.Controls.Add(TableLayoutPanel_Tab)

        Dim lstResults As New ListViewEx
        lstResults.Dock = System.Windows.Forms.DockStyle.Fill
        lstResults.GridLines = True
        lstResults.Location = New System.Drawing.Point(0, 0)
        lstResults.Margin = New System.Windows.Forms.Padding(0)
        lstResults.Font = mChildrenMainForm.TabControl_Station.Font
        lstResults.Name = "lstResults"
        lstResults.OwnerDraw = True
        lstResults.Size = New System.Drawing.Size(453, 140)
        lstResults.UseCompatibleStateImageBehavior = False
        lstResults.View = System.Windows.Forms.View.Details
        lstResults.FullRowSelect = True
        lstResults.MultiSelect = False
        lstResults.BeginUpdate()
        lstResults.Columns.Add("Nr", cLanguageManager.GetTextLine("StationRunner", "Nr"), 90)
        lstResults.Columns.Add("Action", cLanguageManager.GetTextLine("StationRunner", "Action"), 180)
        lstResults.Columns.Add("MaterialNr", cLanguageManager.GetTextLine("StationRunner", "MaterialNr"), 200)
        lstResults.Columns.Add("Result", cLanguageManager.GetTextLine("StationRunner", "Result"), 150)
        lstResults.Columns.Add("Cycle time", cLanguageManager.GetTextLine("StationRunner", "Cycle time"), 200)
        lstResults.Columns.Add("Value", cLanguageManager.GetTextLine("StationRunner", "Value"), 250)
        lstResults.Columns.Add("Comment", cLanguageManager.GetTextLine("StationRunner", "Comment"), 250)
        lstResults.Scrollable = True
        lstResults.LabelWrap = True
        lstResults.EndUpdate()
        TableLayoutPanel_Tab.Controls.Add(lstResults, 0, 0)
        cActionShowManager.RegisterManager(lstResults)

        Dim Panel_Results As New Panel
        Panel_Results.Dock = System.Windows.Forms.DockStyle.Fill
        Panel_Results.Location = New System.Drawing.Point(0, 0)
        Panel_Results.Margin = New System.Windows.Forms.Padding(0)
        Panel_Results.Name = "Panel_Results"
        Panel_Results.Font = mChildrenMainForm.TabControl_Station.Font
        Panel_Results.Padding = New System.Windows.Forms.Padding(0)
        TableLayoutPanel_Tab.Controls.Add(Panel_Results, 0, 1)

        'Dim Label_Results As New Label
        'Label_Results.Dock = System.Windows.Forms.DockStyle.Fill
        'Label_Results.Location = New System.Drawing.Point(0, 0)
        'Label_Results.Margin = New System.Windows.Forms.Padding(0)
        'Label_Results.Name = "Label_Results"
        'Label_Results.Font = mChildrenMainForm.TabControl_Station.Font
        'Label_Results.Padding = New System.Windows.Forms.Padding(0)
        ''Label_Results.BackColor = Color.Black
        'Label_Results.TextAlign = ContentAlignment.MiddleLeft
        'Label_Results.Text = "X:100 Y:100 "
        'TableLayoutPanel_Tab.Controls.Add(Label_Results, 0, 2)

        mChildrenMainForm.TabControl_Station.Controls.Add(SubTabPage)
        cPictureShowManager.RegisterManager(Panel_Results)
        cMachineStatusManager.SetPage()
        cPictureShowManager.ShowPicture(cMachineManager.MachineCellManager.MachineCellCfg.CellPicture)
    End Sub

    Public Overrides Function Run() As Boolean
        SyncLock _Object
            Try
                i.Toggle = i.StepOutputNumber <> i.StepInputNumber
                i.StepOutputNumber = i.StepInputNumber
                If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Return False

                If i.StepOutputNumber >= i.Address_Home Then
                    TempRequestAction = CType(cHMIPLC.GetValue(PLC_RequestAction), StructRequestAction())
                    cActionRequestAction(0) = TempRequestAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 0)
                    cActionRequestAction(1) = TempRequestAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 1)
                    cActionRequestAction(2) = TempRequestAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 2)

                    TempResponseAction = CType(cHMIPLC.GetValue(HMI_ResponseAction), StructResponseAction())
                    cActionResponseAction(0) = TempResponseAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 0)
                    cActionResponseAction(1) = TempResponseAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 1)
                    cActionResponseAction(2) = TempResponseAction((CInt(MachineStationCfg.ID.ToString) - 1) * 3 + 2)
                    cHmiAction = CType(cHMIPLC.GetValue(HMI_HmiAction), StructHmiAction())(CInt(MachineStationCfg.ID.ToString) - 1)
                    bytCurrentActionNr = CType(cHMIPLC.GetValue(HMI_bytCurrentActionNr), Byte())(CInt(MachineStationCfg.ID.ToString) - 1)
                    If cMachineManager.MachineCellManager.HasAutoStation Then
                        bytTargetActionNr = CType(cHMIPLC.GetValue(HMI_bytTargetActionNr), Byte())(CInt(MachineStationCfg.ID.ToString) - 1)
                        ' cPLCAutoActionParameter_Char = CType(cHMIPLC.GetValue(HMI_PLC_Interface.PLC_AutoActionStep + "[" + MachineStationCfg.ID.ToString + "].PLC_AutoActionParameter"), StructPLCAutoActionParameter_Char())
                        cPLCAutoActionParameter_Char = CType(cHMIPLC.GetValue(HMI_PLC_Interface.PLC_AutoActionStep + "[" + MachineStationCfg.ID.ToString + "]"), StructPLCAutoActionParameter_Char())
                    End If
                End If

                If i.Toggle Then
                    cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "[" + cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant + "][" + cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC + "]Step | Station:" + cRunnerCfg.StationName + " | Step:" + i.StepOutputNumber.ToString)
                    iMainUI.SetStationStep(cRunnerCfg.StationName, i.StepOutputNumber.ToString)
                End If
                If bStartTest Then
                    cMachineStatusManager.SetTotalTIme(cMachineStationCfg.ID.ToString, sw.ElapsedMilliseconds / 1000.0)
                End If

                Select Case i.StepOutputNumber
                    Case -100
                        cHMIPLC = cDeviceManager.GetPLCDevice()
                        If IsNothing(cHMIPLC) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "1"), enumExceptionType.Alarm))
                            Return False
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -99
                        If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("StationRunner", "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, cRunnerCfg.StationName))
                            Return False
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -98

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -97
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -96
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case -95
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "ReadVariant"
                        ' cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cLocalElement(clsSubStepCfg.Name))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case -94
                        If Not cRunActionCfg.IsRunning Then
                            If cRunActionCfg.Result Then
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Else
                                i.StepInputNumber = -96
                                Throw New clsHMIException(cRunActionCfg.Message, enumExceptionType.Alarm)
                            End If
                        End If

                    Case -93
                        cActionShowManager.CleanActionStep()
                        '  cMachineStatusManager.SetVariant(cRunnerCfg.StationName, "")
                        cMachineStatusManager.SetSFC(cRunnerCfg.StationName, "")
                        ShowInitPicture()
                        cGlobalActionManager.VariantCfg.Variant = ""
                        If cMachineStationCfg.ShowWaitingMessage = enumCheckType.True Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "Action Start")))
                        i.StepInputNumber = i.Address_Home

                    Case 0
                        '  If i.Toggle Then cMainTipsManager.CleanStationTips(cRunnerCfg.StationName)
                        If cActionRequestAction(0).bulDoPositiveAction Then
                            isRunning = True
                            i.Address_StationIndex = 0
                            eActionType = enumActionType.PreAction
                            i.Address_Pass = 1100
                            i.Address_Fail = 2100
                            i.StepInputNumber = 100
                            sw = New Stopwatch
                            sw.Start()
                            bStartTest = True
                            cMachineStatusManager.SetStationStatus(cRunnerCfg.StationName, enumStationStatus.Work)
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then
                            If cActionRequestAction(1).bulDoPositiveAction Then
                                i.Address_StationIndex = 1
                                eActionType = enumActionType.Action
                                i.Address_Pass = 1100
                                i.Address_Fail = 2100
                                i.StepInputNumber = 100
                                If Not bStartTest Then
                                    sw = New Stopwatch
                                    sw.Start()
                                    bStartTest = True
                                End If
                            End If
                        Else
                            If cActionRequestAction(1).bulDoPositiveAction Then
                                i.Address_StationIndex = 1
                                eActionType = enumActionType.Action
                                i.Address_Pass = 1100
                                i.Address_Fail = 2100
                                i.StepInputNumber = 300
                                If Not bStartTest Then
                                    sw = New Stopwatch
                                    sw.Start()
                                    bStartTest = True
                                End If
                            End If
                        End If

                        If cActionRequestAction(2).bulDoPositiveAction Then
                            i.Address_StationIndex = 2
                            eActionType = enumActionType.AfterAction
                            i.Address_Pass = 1100
                            i.Address_Fail = 2100
                            i.StepInputNumber = 100
                        End If

                        If cActionRequestAction(2).bulDoNegativeAction Then
                            i.Address_StationIndex = 2
                            eActionType = enumActionType.AfterActionFailure
                            i.Address_Pass = 1100
                            i.Address_Fail = 2100
                            i.StepInputNumber = 200
                        End If

                    Case 100
                        strInterErrorMsg = ""
                        iInterRework = 0
                        iBackStep = 0
                        bLastSubActionType = ""
                        strShowErrorMessage = ""
                        lListRepeat.Clear()
                        lListSuccess.Clear()
                        If eActionType = enumActionType.PreAction Then
                            bOtherStationInqueue = False
                            bOtherStationInqueue2 = False
                            bCancelUpdate = False
                            strOtherStationInqueue = ""
                            cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                            cActionShowManager.CleanActionStep()
                            cMachineStatusManager.SetJump(cRunnerCfg.StationName, False)
                            ' cMachineStatusManager.SetVariant(cRunnerCfg.StationName, "")
                            cMachineStatusManager.SetSFC(cRunnerCfg.StationName, "")
                            cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                            strStartTime = Now.ToString("yyyy-MM-dd HH:mm:ss")
                            cMachineStatusManager.SetStartTime(cRunnerCfg.StationName, strStartTime)
                            cMachineStatusManager.SetInsert(cRunnerCfg.StationName, False)
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, eActionType.ToString + " Start"))
                        End If

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 101
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 102
                        If CheckPLCInfo(i.Address_StationIndex) Then
                            If eActionType = enumActionType.PreAction Then
                                If cMachineStationCfg.ID = 1 Then
                                    cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC)
                                End If
                                cFailureActionManager.DeleteSFC(cLocalVariantManager.CurrentVariantCfg.SFC, cMachineStationCfg.ID)
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 2
                        Else
                            If Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 103

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber - 1
                        End If

                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            ' cLocalElement(clsActionResultCfg.Name).ErrorMessage = cMainTipsManager.GetCurrentStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text

                            i.StepInputNumber = i.Address_Fail
                            Return True
                        End If


                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cLocalElement(clsActionResultCfg.Name).ErrorMessage = cMainTipsManager.GetCurrentStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                            i.StepInputNumber = i.Address_Fail
                        End If

                    Case 104

                        If cActionManager.VariantCfg.Variant <> cLocalVariantManager.CurrentVariantCfg.Variant Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 2
                        End If

                    Case 105
                        cActionManager.LoadActionCfg(cLocalVariantManager.CurrentVariantCfg.Variant)
                        cActionManager.CheckCurrentActionCfg()
                        cGlobalActionManager.LoadActionCfg(cLocalVariantManager.GlobalProgramPath(cLocalVariantManager.CurrentVariantCfg.Variant))
                        cGlobalActionManager.CheckCurrentActionCfg()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 106
                        If CheckActionStep() Then
                            i.StepInputNumber = i.StepOutputNumber + 3
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 107
                        If WriteActionStep() Then
                            i.StepInputNumber = i.StepOutputNumber + 2
                        Else
                            If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 108
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber - 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            'If eActionType = enumActionType.PreAction Then
                            '    eActionType = enumActionType.PreActionFailuer
                            'End If
                            'If eActionType = enumActionType.Action Then
                            '    eActionType = enumActionType.ActionFailuer
                            'End If
                            'If eActionType = enumActionType.AfterAction Then
                            '    eActionType = enumActionType.AfterActionFailuer
                            'End If
                            cLocalElement(clsActionResultCfg.Name).ErrorMessage = cMainTipsManager.GetCurrentStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            i.StepInputNumber = i.Address_Fail
                        End If

                    Case 109
                        lListMainStepCfg = cActionManager.GetMainStepCfgList(cRunnerCfg.StationName, eActionType.ToString)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 110
                        iCntAction = 0
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 111
                        If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) <> True Then
                            If eActionType = enumActionType.PreAction Then
                                eActionType = enumActionType.PreActionPass
                                i.StepInputNumber = 200
                                Return True
                            End If
                        End If
                        If iCntAction > lListMainStepCfg.Count - 1 Then
                            If eActionType = enumActionType.PreAction Then
                                eActionType = enumActionType.PreActionPass
                            End If
                            If eActionType = enumActionType.Action Then
                                eActionType = enumActionType.ActionPass
                            End If
                            If eActionType = enumActionType.AfterAction Then
                                eActionType = enumActionType.AfterActionPass
                            End If
                            i.StepInputNumber = 200
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 112
                        If lListMainStepCfg(iCntAction).MainStepParameter(HMIMainStepKeys.Enable) = "FALSE" Then
                            iCntAction = iCntAction + 1
                            i.StepInputNumber = 111
                        Else
                            strTempValue = lListMainStepCfg(iCntAction).ActiveDescription(cLocalElement)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If


                    Case 113
                        cLocalElement(clsMainStepCfg.Name) = lListMainStepCfg(iCntAction)
                        strMainMessage = strTempValue
                        '    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, strTempValue))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 114
                        jCntAction = 0
                        lListSubStepCfg = cActionManager.GetSubStepCfgListFromIndex(cRunnerCfg.StationName, eActionType.ToString, iCntAction)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 115
                        lListShowAction.Clear()
                        cPictureShowManager.CleanPosition()
                        For Each elementSubCfg As clsSubStepCfg In lListSubStepCfg
                            If elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                                Continue For
                            End If
                            If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
                            End If
                            If TypeOf CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI Is IScrewActionUI Then
                                CType(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI, IScrewActionUI).GetPicturePostion(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement)), strPostion)
                                cPictureShowManager.AddPosition(CInt(elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), strPostion)
                            End If
                            'If lListMainStepCfg(iCntAction).MainStepParameter(HMIMainStepKeys.ShowDetail) = "FALSE" Then
                            '    strTempValue = elementSubCfg.ActiveDescription(cLocalElement)
                            '    Dim lListPic As New List(Of clsPictureComponentCfg)
                            '    If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "ManualStationDoAction" Then
                            '        Dim lParameter As List(Of String) = clsParameter.ToList(elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter,cLocalElement ))
                            '        If lParameter.Count > 2 Then
                            '            For j = 1 To lParameter.Count - 1 Step 2
                            '                lListPic.Add(New clsPictureComponentCfg(lParameter(j), lParameter(j + 1)))
                            '            Next
                            '        End If
                            '    End If
                            '    lListShowAction.Add(New clsShowActionCfg(elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Component,cLocalElement ), strTempValue, elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Picture,cLocalElement ), lListPic))
                            'End If

                        Next
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 116
                        If lListMainStepCfg(iCntAction).MainStepParameter(HMIMainStepKeys.ShowDetail) = "FALSE" Then cPictureShowManager.ShowActions(lListShowAction)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 117
                        If jCntAction > lListSubStepCfg.Count - 1 Then
                            iCntAction = iCntAction + 1
                            i.StepInputNumber = 111
                        Else
                            If eActionType = enumActionType.Action Then
                                'If bytCurrentActionNr > Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ID)) Then
                                '    jCntAction = jCntAction + 1
                                '    Return False
                                'Else
                                i.StepInputNumber = i.StepOutputNumber + 1
                                '  End If
                            Else

                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If

                        End If

                    Case 118
                        cSubStepCfg = lListSubStepCfg(jCntAction)
                        If Not lListRepeat.ContainsKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListRepeat.Add(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If
                        If Not lListSuccess.ContainsKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListSuccess.Add(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If
                        If cSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                            jCntAction = jCntAction + 1
                            i.StepInputNumber = 117
                        Else
                            'cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                            cLocalElement(clsSubStepCfg.Name) = cSubStepCfg

                            cLocalElement(clsPLCAction.Name) = New clsPLCAction
                            swAction = New Stopwatch
                            swAction.Start()
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If



                    Case 119
                        '成功过滤失败步骤
                        If bLastSubActionType = enumSubActionType.SubActionPass.ToString Then
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubActionFailure.ToString Then
                                jCntAction = jCntAction + 1
                                i.StepInputNumber = 117
                                Return True
                            End If
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 120
                        'strTempFilePath = lListSubStepCfg(jCntAction).ChangedSubStepParameter(HMISubStepKeys.Picture,cLocalElement )
                        'If lListMainStepCfg(iCntAction).MainStepParameter(HMIMainStepKeys.ShowDetail) <> "FALSE" Then
                        '    If lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ActionType) = "ManualStationScrew" Then
                        '        cPictureShowManager.ShowPicture(strTempFilePath, lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ActionType), lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ID), enumFlashType.Ongoing)
                        '    Else
                        '        cPictureShowManager.ShowPicture(strTempFilePath, lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ActionType))
                        '    End If
                        'Else
                        '    cPictureShowManager.ShowActions(lListShowAction)
                        'End If

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 121
                        If Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                            WriteSubStep(cSubStepCfg)
                            cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                        Else
                            cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                        End If


                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 122
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 123

                        cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cLocalElement(clsActionResultCfg.Name).SubStepCfg = cSubStepCfg
                        cLocalElement(clsActionResultCfg.Name).RepeatNum = lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cLocalElement(clsActionResultCfg.Name).SuccessNum = lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cLocalElement(clsSubStepCfg.Name))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cStartTime = Now
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 124
                        Dim iTotalID As Integer = CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) - 1
                        If iTotalID >= 100 Then
                            iTotalID = 99
                        End If
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                        cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                        If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 125
                        cActionShowManager.UpdateLastActionStepEndTime(swAction.ElapsedMilliseconds)
                        lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                        swAction.Stop()
                        If cRunActionCfg.Result Then
                            lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastSubActionType = enumSubActionType.SubActionPass.ToString
                            End If
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.PASS)
                            cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "PASS", "", cStartTime, Now)

                            If cLocalElement(clsActionResultCfg.Name).Jump Then
                                cLocalElement(clsActionResultCfg.Name).Jump = False
                                iCntAction = cLocalElement(clsActionResultCfg.Name).JumpStep
                                i.StepInputNumber = 111
                            Else
                                If cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID) <> "" Then
                                    jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID)
                                    i.StepInputNumber = 117
                                Else
                                    jCntAction = jCntAction + 1
                                    i.StepInputNumber = 117
                                End If
                            End If



                        Else
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastSubActionType = enumSubActionType.SubActionFailure.ToString
                            End If
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.FAIL)
                            If cRunActionCfg.Message <> "" Then
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", cRunActionCfg.Message, cStartTime, Now)
                            Else
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, cStartTime, Now)
                            End If

                            i.StepInputNumber = i.StepOutputNumber + 1
                            End If

                    Case 126
                        If lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) >= CInt(CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat)) Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).MaxLimite = True
                            i.StepInputNumber = 140
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 127
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 140
                            Return True
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto And cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                            If cMachineStationCfg.AutoConfirm = enumCheckType.True Then
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                                End If
                                iInterRework = 0
                                If eActionType <> enumActionType.PreAction Then
                                    cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                                    iBackStep = 129
                                    eBackActionType = eActionType
                                    eActionType = enumActionType.RetryAction
                                    i.StepInputNumber = 200
                                Else
                                    i.StepInputNumber = 129
                                End If
                                Return True
                            Else
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                                End If
                            End If
                        Else
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                            End If
                        End If

                            i.StepInputNumber = i.StepOutputNumber + 1

                    Case 128
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            iInterRework = 0
                            If eActionType <> enumActionType.PreAction Then
                                cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                                iBackStep = 129
                                eBackActionType = eActionType
                                eActionType = enumActionType.RetryAction
                                i.StepInputNumber = 200
                            Else
                                i.StepInputNumber = 129
                            End If
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            If eActionType = enumActionType.PreAction Then
                                eActionType = enumActionType.PreActionFailure
                            End If
                            If eActionType = enumActionType.Action Then
                                eActionType = enumActionType.ActionFailure
                            End If
                            If eActionType = enumActionType.AfterAction Then
                                eActionType = enumActionType.AfterActionFailure
                            End If
                            i.StepInputNumber = 140
                        End If

                    Case 129
                            If Not cRunActionCfg.IsRunning Then
                                If Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                                    WriteSubStep(cSubStepCfg)
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                Else
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                                End If
                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If

                    Case 130
                            cLocalElement(clsActionResultCfg.Name).ReWorkNum = iInterRework
                            cRunActionCfg = New clsRunActionCfg
                            cRunActionCfg.ActionName = "FailRun"
                            'cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                            cRunActionCfg.Clean()
                            cRunActionCfg.AddParameter(cSubStepCfg)
                            cRunActionCfg.IsRunning = True
                            cRunActionCfg.Result = False
                            cThread = New Thread(AddressOf RunAction)
                            cThread.IsBackground = True
                            cThread.Start()
                            i.StepInputNumber = i.StepOutputNumber + 1

                    Case 131
                            Dim iTotalID As Integer = CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) - 1
                            If iTotalID >= 100 Then
                                iTotalID = 99
                            End If
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                        If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 132
                            iInterRework = iInterRework + 1
                            If cRunActionCfg.Result Then
                                If cSubStepCfg.SubStepParameter(HMISubStepKeys.FailNextID) <> "" Then
                                    jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.FailNextID)
                                    i.StepInputNumber = 118
                                Else
                                    i.StepInputNumber = 118
                                End If
                            Else
                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If

                    Case 133
                        If iInterRework >= CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.Repeat)) Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).MaxLimite = True
                            i.StepInputNumber = 140
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 134
                            If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                                i.StepInputNumber = 140
                                Return True
                            End If
                            If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto And cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            End If

                            If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                                If cMachineStationCfg.AutoConfirm = enumCheckType.True Then
                                    If cRunActionCfg.Message <> "" Then
                                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                                    Else
                                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                                    End If
                                    iInterRework = 0
                                    i.StepInputNumber = 129
                                    Return True
                                Else
                                    If cRunActionCfg.Message <> "" Then
                                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                                    Else
                                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                                    End If
                                End If
                            Else
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                                End If
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1

                    Case 135
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            iBackStep = 129

                            If eActionType <> enumActionType.PreAction Then
                                cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                                eBackActionType = eActionType
                                eActionType = enumActionType.RetryAction
                                i.StepInputNumber = 200
                            Else
                                i.StepInputNumber = 129
                            End If
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            If eActionType = enumActionType.PreAction Then
                                eActionType = enumActionType.PreActionFailure
                            End If
                            If eActionType = enumActionType.Action Then
                                eActionType = enumActionType.ActionFailure
                            End If
                            If eActionType = enumActionType.AfterAction Then
                                eActionType = enumActionType.AfterActionFailure
                            End If
                            i.StepInputNumber = 140
                        End If

                    Case 140
                        If cRunActionCfg.Message <> "" Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                        Else
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                        End If

                        If eActionType = enumActionType.PreAction Then
                            eActionType = enumActionType.PreActionFailure
                        End If
                        If eActionType = enumActionType.Action Then
                            eActionType = enumActionType.ActionFailure
                        End If
                        If eActionType = enumActionType.AfterAction Then
                            eActionType = enumActionType.AfterActionFailure
                        End If
                        cFailureActionManager.InSertData(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID.ToString, cLocalElement(clsActionResultCfg.Name))
                        cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                        i.StepInputNumber = 200



                        'Globe Action
                    Case 200
                        iInterGlobalRework = 0
                        bLastGlobalSubActionType = ""
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 201
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 202
                        i.StepInputNumber = i.StepOutputNumber + 1


                    Case 203
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 204
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 205
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 206
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 207
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 208
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 209
                        lListGlobalMainStepCfg = cGlobalActionManager.GetMainStepCfgList(cRunnerCfg.StationName, eActionType.ToString)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 210
                        iCntGlobalAction = 0
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 211
                        If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) <> True Then
                            If eActionType <> enumActionType.RetryAction Then
                                i.StepInputNumber = i.Address_Pass
                                Return True
                            Else
                                cLocalElement(clsMainStepCfg.Name) = lListMainStepCfg(iCntAction)
                                cLocalElement(clsSubStepCfg.Name) = cSubStepCfg
                                eActionType = eBackActionType
                                i.StepInputNumber = iBackStep
                                Return True
                            End If
                        End If
                        If iCntGlobalAction > lListGlobalMainStepCfg.Count - 1 Then
                            If eActionType = enumActionType.PreActionPass Or eActionType = enumActionType.ActionPass Or eActionType = enumActionType.AfterActionPass Then
                                i.StepInputNumber = i.Address_Pass
                            ElseIf eActionType = enumActionType.RetryAction Then
                                cLocalElement(clsMainStepCfg.Name) = lListMainStepCfg(iCntAction)
                                cLocalElement(clsSubStepCfg.Name) = cSubStepCfg
                                eActionType = eBackActionType
                                i.StepInputNumber = iBackStep
                            Else
                                i.StepInputNumber = i.Address_Fail
                            End If
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 212
                        If lListGlobalMainStepCfg(iCntGlobalAction).MainStepParameter(HMIMainStepKeys.Enable) = "FALSE" Then
                            iCntGlobalAction = iCntGlobalAction + 1
                            i.StepInputNumber = 211
                        Else
                            strTempValue = lListGlobalMainStepCfg(iCntGlobalAction).ActiveDescription(cLocalElement)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 213
                        cLocalElement(clsMainStepCfg.Name) = lListGlobalMainStepCfg(iCntGlobalAction)
                        '  cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, strTempValue))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 214
                        jCntGlobalAction = 0
                        lListGlobalSubStepCfg = cGlobalActionManager.GetSubStepCfgListFromIndex(cRunnerCfg.StationName, eActionType.ToString, iCntGlobalAction)
                        iCntGlobalAction = iCntGlobalAction + 1
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 215
                        If jCntGlobalAction > lListGlobalSubStepCfg.Count - 1 Then
                            i.StepInputNumber = 211
                        Else
                            iInterGlobalRework = 0
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 216
                        '  cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cSubGlobalStepCfg = lListGlobalSubStepCfg(jCntGlobalAction)
                        If Not lListRepeat.ContainsKey(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListRepeat.Add(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If
                        If Not lListSuccess.ContainsKey(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListSuccess.Add(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If

                        '成功过滤失败步骤
                        If bLastGlobalSubActionType = enumSubActionType.SubActionPass.ToString Then
                            If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubActionFailure.ToString Then
                                jCntGlobalAction = jCntGlobalAction + 1
                                i.StepInputNumber = 215
                                Return True
                            End If
                        End If
                        If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                            jCntGlobalAction = jCntGlobalAction + 1
                            i.StepInputNumber = 215
                        Else
                            cLocalElement(clsSubStepCfg.Name) = cSubGlobalStepCfg
                            cLocalElement(clsPLCAction.Name) = New clsPLCAction
                            swAction = New Stopwatch
                            swAction.Start()
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 217
                        '  cActionShowManager.AddNewActionStep(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.Name), cSubGlobalStepCfg.ChangedSubStepParameter(HMISubStepKeys.Component,cLocalElement ), enumActionResult.Ongoing, cSubGlobalStepCfg.ActiveDescription)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 218
                        If Byte.Parse(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + Byte.Parse(cActionManager.GetSubStepListCount(cRunnerCfg.StationName)) > 100 Then
                            WriteSubStep(cSubGlobalStepCfg)
                            cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                        Else
                            cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + Byte.Parse(cActionManager.GetSubStepListCount(cRunnerCfg.StationName)))
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 219
                        CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).AbortProcess = CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 220
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 221
                        CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = False
                        cLocalElement(clsActionResultCfg.Name).RepeatNum = lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cLocalElement(clsActionResultCfg.Name).SuccessNum = lListSuccess(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cLocalElement(clsSubStepCfg.Name))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cStartTime = Now
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 222
                        Dim iTotalID As Integer = CInt(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) - 1
                        If iTotalID >= 100 Then
                            iTotalID = 99
                        End If
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                        cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                        If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 223
                        cActionShowManager.UpdateLastActionStepEndTime(swAction.ElapsedMilliseconds)
                        lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                        swAction.Stop()
                        If cRunActionCfg.Result Then
                            If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastGlobalSubActionType = enumSubActionType.SubActionPass.ToString
                            End If

                            lListSuccess(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListSuccess(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.PASS)
                            ' jCntGlobalAction = jCntGlobalAction + 1
                            cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.Name), "PASS", "", cStartTime, Now)
                            If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OtherStationInQueue Then
                                CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OtherStationInQueue = False
                                bOtherStationInqueue = True
                                strOtherStationInqueue = CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage
                            End If

                            If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OtherStationInQueue2 Then
                                bOtherStationInqueue2 = True
                            End If
                            If cLocalElement(clsActionResultCfg.Name).Jump Then
                                cLocalElement(clsActionResultCfg.Name).Jump = False
                                iCntGlobalAction = cLocalElement(clsActionResultCfg.Name).JumpStep
                                i.StepInputNumber = 211
                            Else
                                If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.PassNextID) <> "" Then
                                    jCntGlobalAction = cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.PassNextID)
                                    i.StepInputNumber = 215
                                Else
                                    jCntGlobalAction = jCntGlobalAction + 1
                                    i.StepInputNumber = 215
                                End If
                            End If
                        Else
                            If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastGlobalSubActionType = enumSubActionType.SubActionFailure.ToString
                            End If
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.FAIL)

                            If cRunActionCfg.Message <> "" Then
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", cRunActionCfg.Message, cStartTime, Now)
                            Else
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, cStartTime, Now)
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 224
                        If lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) >= CInt(CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat)) Then
                            i.StepInputNumber = 240
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 225
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 240
                            Return True
                        End If
                        Dim eMainTipsManagerType As New enumMainTipsManagerType

                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).DisableContinue Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).DisableContinue = False
                            eMainTipsManagerType = enumMainTipsManagerType.ConfirmDisableContine
                        Else
                            eMainTipsManagerType = enumMainTipsManagerType.Confirm
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto And cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                            If cMachineStationCfg.AutoConfirm = enumCheckType.True Then
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                                End If
                                If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage Then
                                    CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage = False
                                    cLocalElement(clsActionResultCfg.Name) = cBackUp.Clone
                                End If
                                i.StepInputNumber = 227
                                Return True
                            Else
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, eMainTipsManagerType))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, eMainTipsManagerType))
                                End If
                            End If
                        Else
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, eMainTipsManagerType))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, eMainTipsManagerType))
                            End If
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 226
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 240
                            Return True
                        End If
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage Then
                                CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage = False
                                cLocalElement(clsActionResultCfg.Name) = cBackUp.Clone
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 240
                        End If

                    Case 227
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 228
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "FailRun"
                        ' cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cLocalElement(clsSubStepCfg.Name))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 229
                        Dim iTotalID As Integer = CInt(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) - 1
                        If iTotalID >= 100 Then
                            iTotalID = 99
                        End If
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                        If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 230
                        iInterGlobalRework = iInterGlobalRework + 1
                        If cRunActionCfg.Result Then
                            If cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.FailNextID) <> "" Then
                                jCntGlobalAction = cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.FailNextID)
                                i.StepInputNumber = 215
                            Else
                                i.StepInputNumber = 216
                            End If
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 231
                        If iInterRework >= CInt(CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat)) Then
                            i.StepInputNumber = 240
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 232
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 240
                            Return True
                        End If
                        Dim eMainTipsManagerType As New enumMainTipsManagerType

                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).DisableContinue Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).DisableContinue = False
                            eMainTipsManagerType = enumMainTipsManagerType.ConfirmDisableContine
                        Else
                            eMainTipsManagerType = enumMainTipsManagerType.Confirm
                        End If
                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto And cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                        End If

                        If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto Then
                            If cMachineStationCfg.AutoConfirm = enumCheckType.True Then
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                                End If
                                lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                                i.StepInputNumber = 227
                                Return True
                            Else
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, eMainTipsManagerType))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, eMainTipsManagerType))
                                End If
                            End If
                        Else
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, eMainTipsManagerType))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, eMainTipsManagerType))
                            End If
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 233
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListRepeat(cSubGlobalStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                            i.StepInputNumber = 227
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 240
                        End If

                    Case 240
                        If cRunActionCfg.Message <> "" Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                        Else
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                        End If
                        '  If eActionType = enumActionType.RetryAction Then
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).OnlyShowMessage = False
                            cLocalElement(clsActionResultCfg.Name) = cBackUp.Clone
                        End If
                        cFailureActionManager.InSertData(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID.ToString, cLocalElement(clsActionResultCfg.Name))
                        '  End If
                        If eBackActionType = enumActionType.PreAction And eActionType = enumActionType.RetryAction Then
                            eActionType = enumActionType.PreActionFailure
                            i.StepInputNumber = 200
                        ElseIf eBackActionType = enumActionType.Action And eActionType = enumActionType.RetryAction Then
                            eActionType = enumActionType.ActionFailure
                            i.StepInputNumber = 200
                        ElseIf eBackActionType = enumActionType.AfterAction And eActionType = enumActionType.RetryAction Then
                            eActionType = enumActionType.AfterActionFailure
                            i.StepInputNumber = 200
                        ElseIf eActionType = enumActionType.PreActionPass Or eActionType = enumActionType.PreActionFailure Then
                            eActionType = enumActionType.PreActionEndFailure
                            i.StepInputNumber = 200
                        ElseIf eActionType = enumActionType.ActionPass Or eActionType = enumActionType.ActionFailure Then
                            eActionType = enumActionType.ActionEndFailure
                            i.StepInputNumber = 200
                        ElseIf eActionType = enumActionType.AfterActionPass Or eActionType = enumActionType.AfterActionFailure Then
                            eActionType = enumActionType.AfterActionEndFailure
                            i.StepInputNumber = 200
                        Else
                            i.StepInputNumber = i.Address_Fail
                        End If

                    Case 300
                        lListRepeat.Clear()
                        strInterErrorMsg = ""
                        iInterRework = 0
                        iTargetID = 0
                        bAutoStep = False
                        iCntAction = 0
                        jCntAction = 0
                        bLastSubActionType = ""
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, eActionType.ToString + " Start"))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 301
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 302
                        If CheckPLCInfo(i.Address_StationIndex) Then
                            i.StepInputNumber = i.StepOutputNumber + 2
                        Else
                            cLocalElement(clsActionResultCfg.Name).ErrorMessage = cMainTipsManager.GetCurrentStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 303
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber - 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            i.StepInputNumber = i.Address_Fail
                        End If

                    Case 304
                        If cActionManager.VariantCfg.Variant <> cLocalVariantManager.CurrentVariantCfg.Variant Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 2
                        End If

                    Case 305
                        cActionManager.LoadActionCfg(cLocalVariantManager.CurrentVariantCfg.Variant)
                        cActionManager.CheckCurrentActionCfg()
                        cGlobalActionManager.LoadActionCfg(cLocalVariantManager.GlobalProgramPath(cLocalVariantManager.CurrentVariantCfg.Variant))
                        cGlobalActionManager.CheckCurrentActionCfg()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 306
                        If CheckActionStep() Then
                            i.StepInputNumber = i.StepOutputNumber + 3
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 307
                        If WriteActionStep() Then
                            i.StepInputNumber = i.StepOutputNumber + 2
                        Else
                            cLocalElement(clsActionResultCfg.Name).ErrorMessage = cMainTipsManager.GetCurrentStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                            cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 308
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            i.StepInputNumber = i.StepOutputNumber - 1
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            i.StepInputNumber = i.Address_Fail
                        End If

                    Case 309
                        lListMainStepCfg = cActionManager.GetMainStepCfgList(cRunnerCfg.StationName, eActionType.ToString)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 310
                        iCntAction = 0
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 311
                        If iCntAction > lListMainStepCfg.Count - 1 Then
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then

                                cFailureActionManager.GetData(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID, lListError)
                                If lListError.Count > 0 Then
                                    cLocalElement(clsActionResultCfg.Name) = lListError(0)
                                    eActionType = enumActionType.ActionFailure
                                    i.StepInputNumber = 380
                                Else
                                    eActionType = enumActionType.ActionPass
                                    i.StepInputNumber = 380
                                End If
                            Else

                                eActionType = enumActionType.ActionPass
                                i.StepInputNumber = 380
                            End If
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 312
                        If lListMainStepCfg(iCntAction).MainStepParameter(HMIMainStepKeys.Enable) = "FALSE" Then
                            iCntAction = iCntAction + 1
                            i.StepInputNumber = 311
                        Else
                            strTempValue = lListMainStepCfg(iCntAction).ActiveDescription(cLocalElement)
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If


                    Case 313
                        cLocalElement(clsMainStepCfg.Name) = lListMainStepCfg(iCntAction)
                        strMainMessage = strTempValue
                        '  cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, strTempValue))
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 314
                        jCntAction = 0
                        lListSubStepCfg = cActionManager.GetSubStepCfgListFromIndex(cRunnerCfg.StationName, eActionType.ToString, iCntAction)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 315
                        lListShowAction.Clear()
                        cPictureShowManager.CleanPosition()
                        For Each elementSubCfg As clsSubStepCfg In lListSubStepCfg
                            If elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                                Continue For
                            End If
                            If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
                            End If
                            If TypeOf CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI Is IScrewActionUI Then
                                CType(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI, IScrewActionUI).GetPicturePostion(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement)), strPostion)
                                cPictureShowManager.AddPosition(CInt(elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), strPostion)
                            End If
                        Next
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 316
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 317
                        If jCntAction > lListSubStepCfg.Count - 1 Then
                            iCntAction = iCntAction + 1
                            i.StepInputNumber = 311
                        Else
                            cSubStepCfg = lListSubStepCfg(jCntAction)
                            iInterRework = 0
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 318
                        cSubStepCfg = lListSubStepCfg(jCntAction)
                        '成功过滤失败步骤
                        If bLastSubActionType = enumSubActionType.SubActionPass.ToString Then
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubActionFailure.ToString Then
                                jCntAction = jCntAction + 1
                                i.StepInputNumber = 317
                                Return True
                            End If
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 319
                        If Not lListRepeat.ContainsKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListRepeat.Add(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If
                        If Not lListSuccess.ContainsKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) Then
                            lListSuccess.Add(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID), 0)
                        End If
                        If cSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = "FALSE" Then
                            jCntAction = jCntAction + 1
                            i.StepInputNumber = 317
                        Else
                            cLocalElement(clsSubStepCfg.Name) = cSubStepCfg
                            cLocalElement(clsPLCAction.Name) = New clsPLCAction
                            swAction = New Stopwatch
                            swAction.Start()
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                            End If
                            If CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                                CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 320
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 321
                        If lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.AutoMode) = "TRUE" Then
                            bAutoStep = True
                            iTargetID = CInt(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TargetNumber))

                            '  cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                            If Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                                CleanAutoParament()
                                WriteSubStep(cSubStepCfg)
                                cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                            Else
                                cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)))
                                cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(iTargetID))
                            End If
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                        Else
                            '     bAutoStep = False
                            If lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationScrew" Then
                                cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                                If Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                                    WriteSubStep(cSubStepCfg)
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                Else
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)))
                                End If
                            End If
                        End If
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 322
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 323
                        cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cLocalElement(clsActionResultCfg.Name).SubStepCfg = cSubStepCfg
                        cLocalElement(clsActionResultCfg.Name).RepeatNum = lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cLocalElement(clsActionResultCfg.Name).SuccessNum = lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID))
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "Run"
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cLocalElement(clsSubStepCfg.Name))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        bAuotParameter = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cStartTime = Now
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 324
                        Dim iTotalID As Integer = CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) - 1
                        If iTotalID >= 100 Then
                            iTotalID = 99
                        End If
                        If bAutoStep Then
                            If cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsFail Or cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsPass Then
                                If Not bAuotParameter Then
                                    CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetManulPLCAutoActionParameter(iTotalID)
                                    bAuotParameter = True
                                End If
                            Else
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                            End If
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsFail
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsPass

                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAutoAction.bulPlcActionIsFail = cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsFail
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAutoAction.bulPlcActionIsPass = cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsPass
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAutoAction.bulPlcDoAction1 = cPLCAutoActionParameter_Char(iTotalID).bulPlcDoAction1
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAutoAction.bulPlcDoAction2 = cPLCAutoActionParameter_Char(iTotalID).bulPlcDoAction2

                            cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                            If Not cRunActionCfg.IsRunning Then
                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If
                        Else
                            If cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsFail Or cPLCAutoActionParameter_Char(iTotalID).bulPlcActionIsPass Then
                                If Not bAuotParameter Then
                                    CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetManulPLCAutoActionParameter(iTotalID)
                                    bAuotParameter = True
                                End If
                            Else
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).ListParmeter = GetPLCAutoActionParameter(iTotalID)
                            End If
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                            cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                            If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If
                        End If


                    Case 325
                        cActionShowManager.UpdateLastActionStepEndTime(swAction.ElapsedMilliseconds)
                        lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                        swAction.Stop()
                        If cRunActionCfg.Result Then
                            lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) = lListSuccess(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) + 1
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastSubActionType = enumSubActionType.SubActionPass.ToString
                            End If
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.PASS)
                            cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "PASS", "", cStartTime, Now)
                            i.StepInputNumber = 330
                        Else
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                bLastSubActionType = enumSubActionType.SubActionFailure.ToString
                            End If
                            cLocalElement(clsActionResultCfg.Name).Result = False
                            cActionShowManager.UpdateLastActionStepResult(enumActionResult.FAIL)
                            If cRunActionCfg.Message <> "" Then
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", cRunActionCfg.Message, cStartTime, Now)
                            Else
                                cActionDataManager.InSertData(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant, eActionType.ToString, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), cSubStepCfg.SubStepParameter(HMISubStepKeys.Name), "FAIL", CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, cStartTime, Now)
                            End If
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then cFailureActionManager.InSertData(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID.ToString, cLocalElement(clsActionResultCfg.Name))

                            i.StepInputNumber = 340
                        End If


                        '成功
                    Case 330
                        If bAutoStep Then
                            If iTargetID = cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID) Then
                                bAutoStep = False
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Else
                                If cLocalElement(clsActionResultCfg.Name).Jump Then
                                    cLocalElement(clsActionResultCfg.Name).Jump = False
                                    iCntAction = cLocalElement(clsActionResultCfg.Name).JumpStep
                                    i.StepInputNumber = 311
                                Else
                                    If cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID) <> "" Then
                                        jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID)
                                        i.StepInputNumber = 317
                                    Else
                                        jCntAction = jCntAction + 1
                                        i.StepInputNumber = 317
                                    End If
                                End If
                            End If
                        Else
                            If cLocalElement(clsActionResultCfg.Name).Jump Then
                                cLocalElement(clsActionResultCfg.Name).Jump = False
                                iCntAction = cLocalElement(clsActionResultCfg.Name).JumpStep
                                i.StepInputNumber = 311
                            Else
                                If cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID) <> "" Then
                                    jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID)
                                    i.StepInputNumber = 317 '重新开始
                                Else
                                    jCntAction = jCntAction + 1
                                    i.StepInputNumber = 317 '重新开始
                                End If
                            End If
                        End If


                    Case 331
                        If cHmiAction.bulPlcActionIsPass Or cHmiAction.bulPlcActionIsFail Then
                            bAutoStep = False
                            cLocalElement(clsPLCAction.Name).DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 332
                        If Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            If cLocalElement(clsActionResultCfg.Name).Jump Then
                                cLocalElement(clsActionResultCfg.Name).Jump = False
                                iCntAction = cLocalElement(clsActionResultCfg.Name).JumpStep
                                i.StepInputNumber = 311
                            Else
                                If cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID) <> "" Then
                                    jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.PassNextID)
                                    i.StepInputNumber = 317 '重新开始
                                Else
                                    jCntAction = jCntAction + 1
                                    i.StepInputNumber = 317 '重新开始
                                End If
                            End If
                        End If

                        '失败
                    Case 340
                        If bAutoStep Then
                            If cPLCAutoActionParameter_Char(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.ID) - 1)).bulPlcActionIsPass Then
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                                i.StepInputNumber = i.StepOutputNumber + 1 '判断后失败
                            Else
                                i.StepInputNumber = 345 '等待失败信号后失败
                            End If
                        Else
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then

                                jCntAction = jCntAction + 1
                                i.StepInputNumber = 317 '重新开始
                            Else

                                i.StepInputNumber = 350 '失败判断
                            End If
                        End If


                    Case 341
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then
                                jCntAction = jCntAction + 1

                                i.StepInputNumber = 317 '重新开始
                            Else

                                i.StepInputNumber = 360 '直接失败
                            End If

                        Else
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 342
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                            iBackStep = 322
                            eBackActionType = eActionType
                            eActionType = enumActionType.RetryAction
                            i.StepInputNumber = 200 '重新开始
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then
                                jCntAction = jCntAction + 1

                                i.StepInputNumber = 317 '重新开始
                            Else

                                i.StepInputNumber = 346
                            End If
                        End If


                    Case 345
                        If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then

                            If iTargetID = cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID) Then
                                bAutoStep = False
                                i.StepInputNumber = 346 '重新开始
                            Else
                                jCntAction = jCntAction + 1
                                i.StepInputNumber = 317 '重新开始
                            End If
                        Else

                            ' If cHmiAction.bulPlcActionIsFail Then
                            '  cLocalElement(clsPLCAction.Name).DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = i.StepInputNumber + 1 '失败
                            'End If
                        End If

                    Case 346
                        If cHmiAction.bulPlcActionIsFail Or cHmiAction.bulPlcActionIsPass Then
                            cLocalElement(clsPLCAction.Name).DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            i.StepInputNumber = i.StepInputNumber + 1
                        End If

                    Case 347
                        If Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            If cMachineStationCfg.FailAutoRun = enumCheckType.True And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelAutoContinue Then

                                jCntAction = jCntAction + 1
                                i.StepInputNumber = 317 '重新开始
                            Else

                                i.StepInputNumber = 360 '失败
                            End If
                        End If


                    Case 350
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 360
                            Return True
                        End If
                        If lListRepeat(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) >= CInt(CType(cLocalElement(clsSubStepCfg.Name), clsSubStepCfg).SubStepParameter(HMISubStepKeys.Repeat)) Then
                            i.StepInputNumber = 360
                        Else
                            If cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                                End If
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Else
                                If cRunActionCfg.Message <> "" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                                End If
                                cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                                iBackStep = 352
                                eBackActionType = eActionType
                                eActionType = enumActionType.RetryAction
                                i.StepInputNumber = 200 '重新开始
                            End If

                        End If

                    Case 351
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            iBackStep = 352
                            cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                            eBackActionType = eActionType
                            eActionType = enumActionType.RetryAction
                            i.StepInputNumber = 200 '重新开始
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 360
                        End If

                    Case 352
                        If Not cRunActionCfg.IsRunning Then
                            If lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.AutoMode) = "TRUE" Then
                                bAutoStep = True
                                iTargetID = CInt(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TargetNumber))

                                '  cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                                If Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                                    CleanAutoParament()
                                    WriteSubStep(cSubStepCfg)
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                    cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                Else
                                    cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)))
                                    cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(iTargetID))
                                End If
                                CType(cLocalElement(clsPLCAction.Name), clsPLCAction).DoAction(cHMIPLC, cMachineStationCfg.ID, True)
                            Else
                                If lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.ActionType) <> "AutoStationScrew" Then
                                    cHMIPLC.WriteAny(HMI_bytTargetActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(lListSubStepCfg(jCntAction).SubStepParameter(HMISubStepKeys.TotalID)))
                                    If Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)) > 100 Then
                                        WriteSubStep(cSubStepCfg)
                                        cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(100))
                                    Else
                                        cHMIPLC.WriteAny(HMI_bytCurrentActionNr + "[" + cRunnerCfg.StationName + "]", Byte.Parse(cSubStepCfg.SubStepParameter(HMISubStepKeys.TotalID)))
                                    End If
                                End If
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 353
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "FailRun"
                        ' cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cSubStepCfg)
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 354
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsFail = cHmiAction.bulPlcActionIsFail
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPlcActionIsPass = cHmiAction.bulPlcActionIsPass
                        CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HmiAction.bulPLCDoAction = cHmiAction.bulPLCDoAction
                        '  cActionShowManager.UpdateLastActionStepTime(swAction.ElapsedMilliseconds)
                        If Not cRunActionCfg.IsRunning And Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 355
                        iInterRework = iInterRework + 1
                        If cRunActionCfg.Result Then
                            If cSubStepCfg.SubStepParameter(HMISubStepKeys.FailNextID) <> "" Then
                                jCntAction = cSubStepCfg.SubStepParameter(HMISubStepKeys.FailNextID)
                                i.StepInputNumber = 318 '重新开始
                            Else
                                i.StepInputNumber = 318 '重新开始
                            End If
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 356
                        If iInterRework >= CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.Repeat)) Then
                            i.StepInputNumber = 360
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If


                    Case 357
                        If CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort Then
                            i.StepInputNumber = 360
                            Return True
                        End If

                        If cMachineStationCfg.AutoConfirm = enumCheckType.False Then
                            CType(cLocalElement(clsPLCAction.Name), clsPLCAction).HMIError(cHMIPLC, cMachineStationCfg.ID, cMachineStationCfg.HMIError)
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Confirm))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Confirm))
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 1
                        Else
                            If cRunActionCfg.Message <> "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                            End If
                            i.StepInputNumber = i.StepOutputNumber + 2
                        End If


                    Case 358
                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Continue Then
                            iBackStep = 352
                            cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                            eBackActionType = eActionType
                            eActionType = enumActionType.RetryAction
                            i.StepInputNumber = 200 '重新开始
                        End If

                        If cMainTipsManager.GetMainTipsConfirmTypeFromKey(cRunnerCfg.StationName) = enumMainTipsConfirmType.Abort Then
                            CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                            i.StepInputNumber = 360
                        End If

                    Case 359
                        iBackStep = 352
                        cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                        eBackActionType = eActionType
                        eActionType = enumActionType.RetryAction
                        i.StepInputNumber = 200 '重新开始


                    Case 360
                        cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                        If cRunActionCfg.Message <> "" Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                        Else
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorMessage, enumMainTipsManagerType.Alarm))
                        End If
                        cFailureActionManager.InSertData(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID.ToString, cLocalElement(clsActionResultCfg.Name))
                        eActionType = enumActionType.ActionFailure
                        i.StepInputNumber = 200


                    Case 380
                        If Not cHmiAction.bulPlcActionIsPass And Not cHmiAction.bulPlcActionIsFail And Not cHmiAction.bulPLCDoAction And Not cHmiAction.bulHmiDoReworkAction Then
                            ' cLocalElement(clsPLCAction.Name).DoPlcAction(cHMIPLC, cMachineStationCfg.ID, False)
                            cBackUp = cLocalElement(clsActionResultCfg.Name).Clone
                            i.StepInputNumber = 200
                        End If



                    Case 1100

                        If cMachineStationCfg.CompleteStep = (i.Address_StationIndex + 1).ToString Then
                            cStationErrorCodeManager.UpdateStationError(cMachineStationCfg.ID.ToString, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorType, True)
                            cStationErrorCodeManager.UpdateCarrierError(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorType, True)
                        End If
                        If eActionType = enumActionType.PreActionPass Or (i.Address_StationIndex + 1).ToString < cMachineStationCfg.CompleteStep Then
                            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC <> "" And cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant <> "" And cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.bInsert = False Then
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Else
                                i.StepInputNumber = 1200
                            End If

                        ElseIf cMachineStationCfg.CompleteStep = (i.Address_StationIndex + 1).ToString Then
                            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC <> "" And cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant <> "" Then
                                If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.bInsert = True Then
                                    i.StepInputNumber = 1104
                                Else
                                    i.StepInputNumber = i.StepOutputNumber + 1
                                End If
                            Else
                                i.StepInputNumber = 1200
                            End If
                        Else
                            i.StepInputNumber = 1200
                        End If

                    Case 1101
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 1102
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "ProductionDataManager.InSertData"
                        '  cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cMachineStationCfg.ID.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC)
                        cRunActionCfg.AddParameter(cLocalVariantManager.CurrentVariantCfg.Variant)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.strStartTime)
                        cMachineStatusManager.SetInsert(cRunnerCfg.StationName, True)
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 1103
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = 1100
                        End If

                    Case 1104
                        If Not cRunActionCfg.Result Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 1101
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 1105
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "ProductionDataManager.UpdateData"
                        '  cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cMachineStationCfg.ID.ToString.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC)
                        cRunActionCfg.AddParameter("PASS")
                        cRunActionCfg.AddParameter("")
                        cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cMachineStatusManager.SetInsert(cRunnerCfg.StationName, False)
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 1106
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 1107
                        If Not cRunActionCfg.Result Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 1105
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 1108
                        i.StepInputNumber = 1200

                    Case 1200
                        If cMachineStationCfg.CompleteStep = (i.Address_StationIndex + 1).ToString Then
                            ShowPassPicture()
                            cMachineStatusManager.AddPassCount(cRunnerCfg.StationName)
                            Dim strResult As String = "NotInqueue"
                            If bOtherStationInqueue2 Then
                                strResult = "NotInque2"
                                bOtherStationInqueue2 = False
                            End If
                            If bOtherStationInqueue And cMachineStationCfg.FailRunNextStation = enumCheckType.False Then
                                cProductionDataManager.InSertData(cMachineStationCfg.ID,
                                                                  cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID.ToString,
                                                                  cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC,
                                                                  cLocalVariantManager.CurrentVariantCfg.Variant,
                                                                  cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.strStartTime)
                                cProductionDataManager.UpdateData(
                                    cMachineStationCfg.ID.ToString.ToString,
                                    cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant,
                                    cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC,
                                   strResult,
                                    strOtherStationInqueue,
                                    Now.ToString("yyyy-MM-dd HH:mm:ss")
                                    )
                            End If
                        End If

                        WritePLCResult(i.Address_StationIndex, True)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 1201
                        If cMachineStationCfg.CompleteStep = (i.Address_StationIndex + 1).ToString Then
                            If i.Toggle Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "Action End")))
                        Else
                            If i.Toggle And (i.Address_StationIndex + 1).ToString < cMachineStationCfg.CompleteStep Then cMainTipsManager.CleanStationTips(cRunnerCfg.StationName)
                        End If

                        If CheckPLCResult(i.Address_StationIndex) Then
                            If cMachineStationCfg.CompleteStep = (i.Address_StationIndex + 1).ToString Then
                                sw.Stop()
                                cMachineStatusManager.SetTotalTIme(cMachineStationCfg.ID.ToString, sw.ElapsedMilliseconds / 1000.0)
                                cMachineStatusManager.SetStationStatus(cRunnerCfg.StationName, enumStationStatus.PASS)
                                cMachineStatusManager.SetSFCRunning(cRunnerCfg.StationName, False)

                            End If
                            If i.Address_StationIndex = 2 Then
                                bStartTest = False
                                isRunning = False
                            End If
                            If (i.Address_StationIndex + 1).ToString < cMachineStationCfg.CompleteStep Then cMachineStatusManager.SetTotalTIme(cMachineStationCfg.ID.ToString, sw.ElapsedMilliseconds / 1000.0)
                            i.StepInputNumber = i.Address_Home
                        End If


                    Case 2100

                        If cLocalElement(clsActionResultCfg.Name).ErrorLevel = enumErrorLevel.Alarm Then
                            cStationErrorCodeManager.UpdateStationError(cMachineStationCfg.ID.ToString, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorType, False)
                            cStationErrorCodeManager.UpdateCarrierError(cMachineStationCfg.ID.ToString, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID, CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorType, False)
                        End If
                        If cLocalElement(clsActionResultCfg.Name).ErrorMessage <> "" Then
                            If cLocalElement(clsActionResultCfg.Name).ErrorLevel = enumErrorLevel.Alarm Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLocalElement(clsActionResultCfg.Name).ErrorMessage, enumMainTipsManagerType.Alarm))
                            Else
                                If cMachineStationCfg.NotInqueueColor = "Red" Then
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLocalElement(clsActionResultCfg.Name).ErrorMessage, enumMainTipsManagerType.Alarm))

                                Else
                                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLocalElement(clsActionResultCfg.Name).ErrorMessage, enumMainTipsManagerType.Normal))
                                End If
                            End If
                        End If
                        Select Case eActionType
                            Case enumActionType.PreActionPass
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case enumActionType.ActionFailure
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case enumActionType.AfterActionFailure
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case enumActionType.PreAction
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case enumActionType.Action
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case enumActionType.AfterAction
                                i.StepInputNumber = i.StepOutputNumber + 1
                            Case Else
                                i.StepInputNumber = i.StepOutputNumber + 1
                        End Select

                    Case 2101
                        If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC <> "" And cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant <> "" And Not CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).CancelUpdate Then
                            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.bInsert = True Then
                                i.StepInputNumber = 2106
                            Else
                                i.StepInputNumber = i.StepOutputNumber + 1
                            End If
                        Else
                            i.StepInputNumber = 2109
                        End If

                    Case 2102
                        ' If cLocalElement(clsActionResultCfg.Name).ErrorLevel = enumErrorLevel.Normal Then
                        'i.StepInputNumber = 2106
                        'Else
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If
                        ' End If

                    Case 2103
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "ProductionDataManager.InSertData"
                        '  cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cMachineStationCfg.ID.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.CarrierID.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC)
                        cRunActionCfg.AddParameter(cLocalVariantManager.CurrentVariantCfg.Variant)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.strStartTime)
                        cMachineStatusManager.SetInsert(cRunnerCfg.StationName, True)
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()

                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 2104
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 2105
                        If Not cRunActionCfg.Result Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 2102
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 2106

                        Dim mStrErrorMessage As String = cMainTipsManager.GetStationMainTipsManagerCfgFromKey(cRunnerCfg.StationName).Text
                        cRunActionCfg = New clsRunActionCfg
                        cRunActionCfg.ActionName = "ProductionDataManager.UpdateData"
                        ' cLocalElement(clsActionResultCfg.Name) = New clsActionResultCfg
                        cRunActionCfg.Clean()
                        cRunActionCfg.AddParameter(cMachineStationCfg.ID.ToString)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.Variant)
                        cRunActionCfg.AddParameter(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC)
                        If cLocalElement(clsActionResultCfg.Name).ErrorLevel = enumErrorLevel.Alarm Then
                            If cMachineStationCfg.MachineStationType = enumMachineStationType.Auto And cMachineStationCfg.FailRunNextStation = enumCheckType.True Then
                                cRunActionCfg.AddParameter("FAILNEXT")
                            Else
                                cRunActionCfg.AddParameter("FAIL")
                            End If
                        Else
                            cRunActionCfg.AddParameter("NotInqueue")
                        End If
                        cRunActionCfg.AddParameter(mStrErrorMessage)
                        cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                        cRunActionCfg.IsRunning = True
                        cRunActionCfg.Result = False
                        cMachineStatusManager.SetInsert(cRunnerCfg.StationName, False)
                        cThread = New Thread(AddressOf RunAction)
                        cThread.IsBackground = True
                        cThread.Start()
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 2107
                        If Not cRunActionCfg.IsRunning Then
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 2108
                        If Not cRunActionCfg.Result Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cRunActionCfg.Message, enumMainTipsManagerType.Alarm))
                            i.StepInputNumber = 2106
                        Else
                            i.StepInputNumber = i.StepOutputNumber + 1
                        End If

                    Case 2109
                        If cLocalElement(clsActionResultCfg.Name).ErrorLevel = enumErrorLevel.Alarm Then cMachineStatusManager.AddFailCount(cRunnerCfg.StationName)
                        i.StepInputNumber = 2200

                    Case 2200
                        WritePLCResult(i.Address_StationIndex, False)
                        i.StepInputNumber = i.StepOutputNumber + 1

                    Case 2201
                        '  If i.Toggle Then cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, "Waiting PLC Close PreAction Signal"))
                        If CheckPLCResult(i.Address_StationIndex) Then
                            sw.Stop()
                            cMachineStatusManager.SetTotalTIme(cMachineStationCfg.ID.ToString, sw.ElapsedMilliseconds / 1000.0)
                            bStartTest = False
                            cMachineStatusManager.SetStationStatus(cRunnerCfg.StationName, enumStationStatus.FAIL)
                            cMachineStatusManager.SetSFCRunning(cRunnerCfg.StationName, False)
                            isRunning = False
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


    Public Sub RunAction()
        Try
            If cRunActionCfg.ActionName = "Run" Then
                Dim cSubStepCfg As clsSubStepCfg = CType(cRunActionCfg.GetParameter(0), clsSubStepCfg)
                If Not CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Init(cLocalElement, cSystemElement, clsParameter.ToList(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))) Then
                    cRunActionCfg.Result = False
                    cRunActionCfg.IsRunning = False
                    Return
                End If
                If Not CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Run(cLocalElement, cSystemElement, clsParameter.ToList(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))) Then
                    cRunActionCfg.Result = False
                    cRunActionCfg.IsRunning = False
                    Return
                End If
                If Not CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Quit(cLocalElement, cSystemElement) Then
                    cRunActionCfg.Result = False
                    cRunActionCfg.IsRunning = False
                    Return
                End If
            End If

            If cRunActionCfg.ActionName = "FailRun" Then
                Dim cSubStepCfg As clsSubStepCfg = CType(cRunActionCfg.GetParameter(0), clsSubStepCfg)
                If Not CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).FailRun(cLocalElement, cSystemElement, clsParameter.ToList(cSubStepCfg.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))) Then
                    cRunActionCfg.Result = False
                    cRunActionCfg.IsRunning = False
                    Return
                End If
                If Not CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Quit(cLocalElement, cSystemElement) Then
                    cRunActionCfg.Result = False
                    cRunActionCfg.IsRunning = False
                    Return
                End If
            End If

            If cRunActionCfg.ActionName = "ProductionDataManager.InSertData" Then
                cProductionDataManager.InSertData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1), cRunActionCfg.GetParameter(2), cRunActionCfg.GetParameter(3), cRunActionCfg.GetParameter(4))
                bRecord = True
            End If
            If cRunActionCfg.ActionName = "ProductionDataManager.UpdateData" Then
                cProductionDataManager.UpdateData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1), cRunActionCfg.GetParameter(2), cRunActionCfg.GetParameter(3), cRunActionCfg.GetParameter(4), cRunActionCfg.GetParameter(5))
                bRecord = False
            End If

            If cRunActionCfg.ActionName = "ReadVariant" Then
                '  

                If cLocalVariantManager.CurrentVariantCfg.Variant <> "" Then
                    cActionManager.LoadActionCfg(cLocalVariantManager.CurrentVariantCfg.Variant)
                    cActionManager.CheckCurrentActionCfg()
                    cGlobalActionManager.LoadActionCfg(cLocalVariantManager.GlobalProgramPath(cLocalVariantManager.CurrentVariantCfg.Variant))
                    cGlobalActionManager.CheckCurrentActionCfg()
                    WriteActionStep()
                End If
            End If
            cRunActionCfg.Result = True
            cRunActionCfg.IsRunning = False
        Catch ex As Exception
            If cRunActionCfg.ActionName = "Run" Or cRunActionCfg.ActionName = "FailRun" Then
                Dim cSubStepCfg As clsSubStepCfg = CType(cRunActionCfg.GetParameter(0), clsSubStepCfg)
                If IsNothing(cSubStepCfg) Then
                    cRunActionCfg.Message = ex.Message.ToString
                Else
                    cRunActionCfg.Message = cSubStepCfg.ActiveDescription(cLocalElement) + " Exception:" + ex.Message.ToString
                End If
            Else
                cRunActionCfg.Message = ex.Message.ToString
            End If
            cRunActionCfg.Result = False
            cRunActionCfg.IsRunning = False
        End Try
    End Sub

    Protected Function CheckPLCInfo(ByVal iIndex As Integer) As Boolean
        Try
            cMachineStatusManager.SetCarrierID(cRunnerCfg.StationName, cActionRequestAction(iIndex).stuActionArticleSet.intCarrierID)
            If iIndex = 0 Then
                If bytCurrentActionNr <> 0 Then
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "4"), enumMainTipsManagerType.Confirm))
                    Return False
                End If
            End If

            Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, "1", GetType(clsHMICarrierManager))
            If cMachineStationCfg.ResetCarrier = enumCheckType.True And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) = True And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.CleanSFC).ToString = cActionRequestAction(iIndex).stuActionArticleSet.strSerialNr And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.CleanSFC).ToString <> "" Then
                If Not IsNothing(cDeviceCfg) Then
                    Dim strResult As String = ""
                    Dim c As clsHMICarrierManager = cDeviceCfg.Source
                    c.ResetCarrierID(cActionRequestAction(iIndex).stuActionArticleSet.intCarrierID, strResult)
            End If
            End If


            cDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, "1", GetType(clsHMICarrierManager))
            If cMachineStationCfg.CheckCarrier = enumCheckType.True And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) = True Then
                If Not IsNothing(cDeviceCfg) Then
                    Dim strResult As String = ""
                    Dim c As clsHMICarrierManager = cDeviceCfg.Source
                    If c.CheckRepeat(cActionRequestAction(iIndex).stuActionArticleSet.intCarrierID, strResult) = -1 Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, strResult))
                        cLocalElement(clsActionResultCfg.Name).Result = False
                        cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                        cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                        cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                        CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                        CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorLevel = enumErrorLevel.Normal
                        Return False
                    End If
                End If
            End If



            If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.CleanSFC).ToString <> "" And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) = True Then
                If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.CleanSFC).ToString = cActionRequestAction(iIndex).stuActionArticleSet.strSerialNr Then
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "10", cActionRequestAction(iIndex).stuActionArticleSet.intCarrierID)))
                    cLocalElement(clsActionResultCfg.Name).Result = False
                    cLocalElement(clsActionResultCfg.Name).ErrorType = "NA"
                    cLocalElement(clsActionResultCfg.Name).MainErrorType = "NA"
                    cLocalElement(clsActionResultCfg.Name).ErrorCode = "NA"
                    CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).Abort = True
                    CType(cLocalElement(clsActionResultCfg.Name), clsActionResultCfg).ErrorLevel = enumErrorLevel.Normal
                    Return False
                End If
            End If

            '手动选择变种
            If cMachineStationCfg.VariantType = enumMachineStationType.Manual Then
                If cMachineStationCfg.CheckArticleType = enumCheckType.True And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) = True Then
                    If cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr = "" Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "5"), enumMainTipsManagerType.Confirm))
                        Return False
                    End If
                    If cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr <> cLocalVariantManager.CurrentVariantCfg.Variant Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "6", cLocalVariantManager.CurrentVariantCfg.Variant, cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr), enumMainTipsManagerType.Confirm))
                        Return False
                    End If
                    'Else
                    '    '取消变种判断
                    '    If cLocalVariantManager.CurrentVariantCfg.Variant <> cVariantManager.CurrentVariantCfg.Variant Then
                    '        If Not cVariantManager.HasVariant(cVariantManager.CurrentVariantCfg.Variant) Then
                    '            Return False
                    '        End If
                    '        cLocalVariantManager.ChangeVariant(cVariantManager.CurrentVariantCfg.Variant, enumSelectVariantType.Auto)
                    'End If
                End If
            Else
                If cMachineStationCfg.CheckArticleType = enumCheckType.True Then
                    '自动选择变种
                    If cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr = "" Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "5"), enumMainTipsManagerType.Confirm))
                        Return False
                    End If
                    If cLocalVariantManager.CurrentVariantCfg.Variant <> cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr Then
                        If Not cVariantManager.HasVariant(cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "7", cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr), enumMainTipsManagerType.Confirm))
                            Return False
                        End If
                        cLocalVariantManager.ChangeVariant(cActionRequestAction(iIndex).stuActionArticleSet.strKostalNr, enumSelectVariantType.Auto)
                    End If
                Else
                    '取消变种判断
                    If cLocalVariantManager.CurrentVariantCfg.Variant <> cVariantManager.CurrentVariantCfg.Variant Then
                        If Not cVariantManager.HasVariant(cVariantManager.CurrentVariantCfg.Variant) Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "7", cVariantManager.CurrentVariantCfg.Variant), enumMainTipsManagerType.Confirm))
                            Return False
                        End If
                        cLocalVariantManager.ChangeVariant(cVariantManager.CurrentVariantCfg.Variant, enumSelectVariantType.Auto)
                    End If

                End If
            End If

            If cMachineStationCfg.CheckSNType = enumCheckType.True And cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process) = True Then
                If cActionRequestAction(iIndex).stuActionArticleSet.strSerialNr = "" Then
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, cLanguageManager.GetTextLine("StationRunner", "8"), enumMainTipsManagerType.Confirm))
                    Return False
                End If
                cMachineStatusManager.SetSFC(cRunnerCfg.StationName, cActionRequestAction(iIndex).stuActionArticleSet.strSerialNr)
            Else
                If eActionType = enumActionType.AfterAction Then
                    cMachineStatusManager.SetSFC(cRunnerCfg.StationName, cActionRequestAction(iIndex).stuActionArticleSet.strSerialNr)
                End If
            End If

            cMachineStatusManager.SetVariant(cRunnerCfg.StationName, cLocalVariantManager.CurrentVariantCfg.Variant)
            If eActionType = enumActionType.PreAction Then
                ShowInitPicture()
                cMachineStatusManager.SetSFCRunning(cRunnerCfg.StationName, True)
            End If

            Return True
        Catch ex As Exception
            cMainTipsManager.AddTips(New clsMainTipsManagerCfg(cRunnerCfg.StationName, ex.Message, enumMainTipsManagerType.Confirm))
            Return False
        End Try
    End Function


    Protected Function WritePLCResult(ByVal iIndex As Integer, ByVal bResult As Boolean) As Boolean
        Try
            Dim dNewValueRequest = New StructRequestAction
            dNewValueRequest.bulDoPositiveAction = False
            dNewValueRequest.bulDoNegativeAction = False
            cHMIPLC.WriteAny(HMI_PLC_Interface.PLC_RequestAction + "[" + MachineStationCfg.ID.ToString + "," + (iIndex + 1).ToString + "]", dNewValueRequest)
            Dim dNewValueResponse As New StructResponseAction
            dNewValueResponse.bulActionIsPass = bResult
            dNewValueResponse.bulActionIsFail = Not bResult
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ResponseAction + "[" + MachineStationCfg.ID.ToString + "," + (iIndex + 1).ToString + "]", dNewValueResponse)
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Protected Function WriteActionStep() As Boolean
        SyncLock _Object
            Try
                Dim iIndex As Integer = 0
                Dim cActionParameter_Char(HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1) As StructActionParameter_Char
                Dim cActionParameter(HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1) As StructActionParameter_String
                For Each elementType As String In cActionManager.GetActionCfgFromKey(cRunnerCfg.StationName).GetStepListKey
                    Dim lListSubStepCfg As List(Of clsSubStepCfg) = cActionManager.GetSubStepCfgList(cRunnerCfg.StationName, elementType)
                    Dim iCnt As Integer = 0
                    For iCnt = 0 To lListSubStepCfg.Count - 1
                        If iIndex >= 100 Then Continue For
                        cActionParameter(iIndex) = New StructActionParameter_String
                        cActionParameter(iIndex).intID = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ID)
                        cActionParameter(iIndex).strType = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ActionType)
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                        End If
                        cActionParameter(iIndex).intRepeat = Int16.Parse(lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat))
                        Dim lListParameter As List(Of String) = clsParameter.ToList(lListSubStepCfg(iCnt).ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))
                        For jCnt = 0 To lListParameter.Count - 1
                            If jCnt >= 20 Then Continue For
                            cActionParameter(iIndex).strParameter(jCnt) = lListParameter(jCnt)
                        Next
                        iIndex = iIndex + 1
                    Next
                Next

                For Each elementType As String In cGlobalActionManager.GetActionCfgFromKey(cRunnerCfg.StationName).GetStepListKey
                    Dim lListSubStepCfg As List(Of clsSubStepCfg) = cGlobalActionManager.GetSubStepCfgList(cRunnerCfg.StationName, elementType)
                    Dim iCnt As Integer = 0
                    For iCnt = 0 To lListSubStepCfg.Count - 1
                        If iIndex >= 100 Then Continue For
                        cActionParameter(iIndex) = New StructActionParameter_String
                        cActionParameter(iIndex).intID = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ID)
                        cActionParameter(iIndex).strType = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ActionType)
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                        End If
                        cActionParameter(iIndex).intRepeat = Int16.Parse(lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat))
                        Dim lListParameter As List(Of String) = clsParameter.ToList(lListSubStepCfg(iCnt).ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))
                        For jCnt = 0 To lListParameter.Count - 1
                            If jCnt >= 20 Then Continue For
                            cActionParameter(iIndex).strParameter(jCnt) = lListParameter(jCnt)
                        Next
                        iIndex = iIndex + 1
                    Next
                Next
                For kCnt = iIndex To HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1
                    cActionParameter(kCnt) = New StructActionParameter_String
                Next

                For iCnt = 0 To HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1
                    cActionParameter_Char(iCnt) = New StructActionParameter_Char
                    cActionParameter_Char(iCnt).intID = cActionParameter(iCnt).intID
                    cActionParameter_Char(iCnt).strType = cActionParameter(iCnt).strType
                    cActionParameter_Char(iCnt).intRepeat = cActionParameter(iCnt).intRepeat
                    Dim jCnt As Integer = 0
                    For jCnt = 0 To 19
                        Dim kCnt As Integer = 0
                        Dim cTemp() As Char = cActionParameter(iCnt).strParameter(jCnt).ToCharArray
                        For kCnt = 0 To 29
                            If kCnt <= cTemp.Length - 1 Then
                                cActionParameter_Char(iCnt).strParameter(jCnt * 31 + kCnt) = cTemp(kCnt)
                            End If
                        Next
                    Next
                Next

                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ActionStep + "[" + cRunnerCfg.StationName + "]", cActionParameter_Char)
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
                Return False
            End Try
        End SyncLock
    End Function

    Protected Function WriteSubStep(ByVal cSubStep As clsSubStepCfg) As Boolean
        SyncLock _Object
            Try
                Dim jCnt As Integer = 0
                Dim cActionParameter As StructActionParameter_String = New StructActionParameter_String
                cActionParameter.intID = cSubStep.SubStepParameter(HMISubStepKeys.ID)
                cActionParameter.strType = cSubStep.SubStepParameter(HMISubStepKeys.ActionType)
                If cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                    cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                End If
                If cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                    cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = "1"
                End If
                If cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                    cSubStep.SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                End If
                cActionParameter.intRepeat = Int16.Parse(cSubStep.SubStepParameter(HMISubStepKeys.Repeat))
                Dim lListParameter As List(Of String) = clsParameter.ToList(cSubStep.ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))
                For jCnt = 0 To lListParameter.Count - 1
                    If jCnt >= 20 Then Continue For
                    cActionParameter.strParameter(jCnt) = lListParameter(jCnt)
                Next
                Dim cActionParameter_Char As StructActionParameter_Char = New StructActionParameter_Char
                cActionParameter_Char.intID = cActionParameter.intID
                cActionParameter_Char.strType = cActionParameter.strType
                cActionParameter_Char.intRepeat = cActionParameter.intRepeat
                For jCnt = 0 To 19
                    Dim kCnt As Integer = 0
                    Dim cTemp() As Char = cActionParameter.strParameter(jCnt).ToCharArray
                    For kCnt = 0 To 20
                        If kCnt <= cTemp.Length - 1 Then
                            cActionParameter_Char.strParameter(jCnt * 31 + kCnt) = cTemp(kCnt)
                        End If
                    Next
                Next
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ActionStep + "[" + cRunnerCfg.StationName + "].HMI_ActionParameter[100]", cActionParameter_Char)
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
                Return False
            End Try
        End SyncLock
    End Function

    Protected Function CleanAutoParament() As Boolean
        SyncLock _Object
            Try
                Dim cStructPLCAutoActionParameter_Char As New StructPLCAutoActionParameter_Char
                cHMIPLC.WriteAny(HMI_PLC_Interface.PLC_AutoActionStep + "[" + cRunnerCfg.StationName + "].PLC_AutoActionParameter[100]", cStructPLCAutoActionParameter_Char)
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
                Return False
            End Try
        End SyncLock
    End Function
    Protected Function CheckActionStep() As Boolean
        SyncLock _Object
            Try
                Dim iIndex As Integer = 0
                Dim cActionParameter_Char(HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1) As StructActionParameter_Char
                Dim cActionParameter(HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1) As StructActionParameter_String
                For Each elementType As String In cActionManager.GetActionCfgFromKey(cRunnerCfg.StationName).GetStepListKey
                    Dim lListSubStepCfg As List(Of clsSubStepCfg) = cActionManager.GetSubStepCfgList(cRunnerCfg.StationName, elementType)
                    Dim iCnt As Integer = 0
                    For iCnt = 0 To lListSubStepCfg.Count - 1
                        If iIndex >= 100 Then Continue For
                        cActionParameter(iIndex) = New StructActionParameter_String
                        cActionParameter(iIndex).intID = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ID)
                        cActionParameter(iIndex).strType = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ActionType)
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                        End If
                        cActionParameter(iIndex).intRepeat = Int16.Parse(lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat))
                        Dim lListParameter As List(Of String) = clsParameter.ToList(lListSubStepCfg(iCnt).ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))
                        For jCnt = 0 To lListParameter.Count - 1
                            If jCnt >= 20 Then Continue For
                            cActionParameter(iIndex).strParameter(jCnt) = lListParameter(jCnt)
                        Next
                        iIndex = iIndex + 1
                    Next
                Next

                For Each elementType As String In cGlobalActionManager.GetActionCfgFromKey(cRunnerCfg.StationName).GetStepListKey
                    Dim lListSubStepCfg As List(Of clsSubStepCfg) = cGlobalActionManager.GetSubStepCfgList(cRunnerCfg.StationName, elementType)
                    Dim iCnt As Integer = 0
                    For iCnt = 0 To lListSubStepCfg.Count - 1
                        If iIndex >= 100 Then Continue For
                        cActionParameter(iIndex) = New StructActionParameter_String
                        cActionParameter(iIndex).intID = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ID)
                        cActionParameter(iIndex).strType = lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.ActionType)
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[" + clsHMIGlobalParameter.Manual_Screw_Repeat + "]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = cMachineManager.MachineVariantParameter.GetGlobalParameter(cLocalVariantManager.CurrentVariantCfg.Variant, clsHMIGlobalParameter.Manual_Screw_Repeat)
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "1"
                        End If
                        If lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "[Continue]" Then
                            lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat) = "32767"
                        End If
                        cActionParameter(iIndex).intRepeat = Int16.Parse(lListSubStepCfg(iCnt).SubStepParameter(HMISubStepKeys.Repeat))
                        Dim lListParameter As List(Of String) = clsParameter.ToList(lListSubStepCfg(iCnt).ChangedSubStepParameter(HMISubStepKeys.Parameter, cLocalElement))
                        For jCnt = 0 To lListParameter.Count - 1
                            If jCnt >= 20 Then Continue For
                            cActionParameter(iIndex).strParameter(jCnt) = lListParameter(jCnt)
                        Next
                        iIndex = iIndex + 1
                    Next
                Next

                For kCnt = iIndex To HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1
                    cActionParameter(kCnt) = New StructActionParameter_String
                Next

                For iCnt = 0 To HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1
                    cActionParameter_Char(iCnt) = New StructActionParameter_Char
                    cActionParameter_Char(iCnt).intID = cActionParameter(iCnt).intID
                    cActionParameter_Char(iCnt).strType = cActionParameter(iCnt).strType
                    cActionParameter_Char(iCnt).intRepeat = cActionParameter(iCnt).intRepeat
                    Dim jCnt As Integer = 0
                    For jCnt = 0 To 19
                        Dim kCnt As Integer = 0
                        Dim cTemp() As Char = cActionParameter(iCnt).strParameter(jCnt).ToCharArray
                        For kCnt = 0 To 20
                            If kCnt <= cTemp.Length - 1 Then
                                cActionParameter_Char(iCnt).strParameter(jCnt * 31 + kCnt) = cTemp(kCnt)
                            End If
                        Next
                    Next
                Next

                Dim cReadActionParameter_Char() As StructActionParameter_Char = cHMIPLC.ReadAny(HMI_PLC_Interface.HMI_ActionStep + "[" + cRunnerCfg.StationName + "]", GetType(StructActionParameter_Char()), New Integer() {100})
                For iCnt = 0 To HMI_PLC_Interface.CON_MAXIMUM_TOTAL_ACTION - 1
                    If cActionParameter_Char(iCnt).intID <> cReadActionParameter_Char(iCnt).intID Then
                        Return False
                    End If
                    If cActionParameter_Char(iCnt).strType <> cReadActionParameter_Char(iCnt).strType Then
                        Return False
                    End If
                    If cActionParameter_Char(iCnt).intRepeat <> cReadActionParameter_Char(iCnt).intRepeat Then
                        Return False
                    End If
                    Dim jCnt As Integer = 0
                    For jCnt = 0 To 31 * 20 - 1
                        If cActionParameter_Char(iCnt).strParameter(jCnt) <> cReadActionParameter_Char(iCnt).strParameter(jCnt) Then
                            Return False
                        End If
                    Next
                Next
                Return True
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
                Return False
            End Try
        End SyncLock
    End Function

    Protected Function CheckPLCResult(ByVal iIndex As Integer) As Boolean
        Try
            If iIndex = 1 Then
                If bytCurrentActionNr = 0 And Not cActionRequestAction(iIndex).bulDoPositiveAction And Not cActionRequestAction(iIndex).bulDoNegativeAction And Not cActionResponseAction(iIndex).bulActionIsPass And Not cActionResponseAction(iIndex).bulActionIsFail Then
                    Return True
                End If
            Else
                If Not cActionRequestAction(iIndex).bulDoPositiveAction And Not cActionRequestAction(iIndex).bulDoNegativeAction And Not cActionResponseAction(iIndex).bulActionIsPass And Not cActionResponseAction(iIndex).bulActionIsFail Then
                    Return True
                End If
            End If


            Return False
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Overrides Function Reset() As Boolean
        Try
            If cRunActionCfg.IsRunning Then
                If cRunActionCfg.ActionName = "Run" Or cRunActionCfg.ActionName = "FailRun" Then
                    Dim cSubStepCfg As clsSubStepCfg = CType(cRunActionCfg.GetParameter(0), clsSubStepCfg)
                    CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Abort(cLocalElement, cSystemElement)
                    '  CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Quit(cLocalElement, cSystemElement)
                End If
            End If
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
            Dim cHmiAction As New StructHmiAction
            cHMIPLC.WriteAny(HMI_HmiAction + "[" + cRunnerCfg.StationName + "]", cHmiAction)
            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).ProductionLoggingCfg.bInsert Then
                Try
                    cProductionDataManager.UpdateData(cMachineStationCfg.ID, cLocalVariantManager.CurrentVariantCfg.Variant, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, "ESTOP", "Interrupt", Now.ToString("yyyy-MM-dd HH:mm:ss"))
                Catch
                End Try
            End If

            cMainTipsManager.CleanStationTips(cRunnerCfg.StationName)
            cActionManager.VariantCfg.Variant = ""
            i.StepInputNumber = i.Address_Origin
            iMainUI.SetStationStep(cRunnerCfg.StationName, i.StepInputNumber.ToString)
            sw.Stop()
            cMachineStatusManager.SetTotalTIme(cMachineStationCfg.ID.ToString, sw.ElapsedMilliseconds / 1000.0)
            cMachineStatusManager.SetStationStatus(cRunnerCfg.StationName, enumStationStatus.Interrupt)

            bStartTest = False
            'MES

            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).SFCRunning And cMachineStationCfg.Estop = enumCheckType.True Then
                If cMachineStationCfg.MES <> "" And cMachineStationCfg.MES <> "NONE" Then
                    Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, cMachineStationCfg.MES, GetType(clsHMIMESBase))
                    If Not IsNothing(cDeviceCfg) Then
                        Dim cHMIMES As clsHMIMESBase = cDeviceCfg.Source
                        Dim lListNcData As New List(Of clsNcDataCfg)
                        Dim cNcDataCfg As New clsNcDataCfg
                        Dim strResult As String = String.Empty
                        If cMachineManager.MachineStatus.bulEmergence Then
                            cNcDataCfg.NcComment = cLanguageManager.GetTextLine("StationRunner", "Emergency")
                        Else
                            cNcDataCfg.NcComment = cLanguageManager.GetTextLine("StationRunner", "Interrupt")
                        End If
                        lListNcData.Add(cNcDataCfg)
                        If Not cHMIMES.logNonConformance(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, lListNcData, strResult) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(strResult, enumExceptionType.Alarm, cRunnerCfg.StationName))
                        End If
                        If Not cHMIMES.Complete(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, strResult) Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(strResult, enumExceptionType.Alarm, cRunnerCfg.StationName))
                        End If
                    End If
                End If
            End If
            'ProcessControl
            If cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).SFCRunning And cMachineStationCfg.Estop = enumCheckType.True Then
                Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(cRunnerCfg.StationName, "1", GetType(clsHMIProcessControl))
                Dim strResult As String = ""
                If Not IsNothing(cDeviceCfg) Then
                    Dim cHMIHMIProcessControl As clsHMIProcessControl = cDeviceCfg.Source
                    If Not cHMIHMIProcessControl.logNonConformance(cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, strResult) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(strResult, enumExceptionType.Alarm, cRunnerCfg.StationName))
                    End If
                End If
            End If

            If cMachineStationCfg.Estop = enumCheckType.True And cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).SFCRunning Then
                If Not IsNothing(cPreviousMachineStationCfg) Then
                    cProductionMESData.InSertData(cLocalVariantManager.CurrentVariantCfg.Variant, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cPreviousMachineStationCfg.ID, "ESTOP")
                End If
                cProductionMESData.InSertData(cLocalVariantManager.CurrentVariantCfg.Variant, cMachineStatusManager.GetMachineStatusCfgFromName(cRunnerCfg.StationName).VariantCfg.SFC, cMachineStationCfg.ID, "ESTOP")
            End If

            cMachineStatusManager.SetSFCRunning(cRunnerCfg.StationName, False)
            isRunning = False
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            isRunning = False
            Return False
        End Try
    End Function
    Private Sub LoadChanged()
        If Not IsNothing(cLocalVariantManager) Then
            cLocalVariantManager.LoadVariantCfg()
        End If
    End Sub
    Private Sub VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
        SyncLock _Object
            Try
                If eSelectVariantType = enumSelectVariantType.Manual Then

                    cMachineStatusManager.SetVariant(cRunnerCfg.StationName, strVariant)
                    cLocalVariantManager.ChangeVariant(strVariant)
                    ShowInitPicture()
                    ' If cMachineStationCfg.VariantType = enumMachineStationType.Manual Then
                    If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_VariantInfo + "[" + cRunnerCfg.StationName + "].strKostalNr", cVariantManager.CurrentVariantCfg.Variant, New Integer() {cVariantManager.CurrentVariantCfg.Variant.Length})

                    'End If
                    If cActionManager.VariantCfg.Variant <> cLocalVariantManager.CurrentVariantCfg.Variant Then
                        ' cActionManager.LoadActionCfg(cLocalVariantManager.CurrentVariantCfg.Variant)
                        ' cActionManager.CheckCurrentActionCfg()
                        '  cGlobalActionManager.LoadActionCfg(cLocalVariantManager.GlobalProgramPath(cLocalVariantManager.CurrentVariantCfg.Variant))
                        '  cGlobalActionManager.CheckCurrentActionCfg()
                        '   WriteActionStep()
                    End If
                End If
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            End Try
        End SyncLock
    End Sub

    Private Sub SetVariant(ByVal strVariant As String, ByVal eSelectVariantType As enumSelectVariantType)
        SyncLock _Object
            Try
                If MachineStationCfg.VariantType = enumMachineStationType.Manual And eSelectVariantType = enumSelectVariantType.Manual Then

                    cMachineStatusManager.SetVariant(cRunnerCfg.StationName, strVariant)
                    cLocalVariantManager.ChangeVariant(strVariant)
                    ShowInitPicture()
                    If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_VariantInfo + "[" + cRunnerCfg.StationName + "].strKostalNr", cVariantManager.CurrentVariantCfg.Variant, New Integer() {cVariantManager.CurrentVariantCfg.Variant.Length})
                    If cActionManager.VariantCfg.Variant <> cLocalVariantManager.CurrentVariantCfg.Variant Then
                        ' cActionManager.LoadActionCfg(cLocalVariantManager.CurrentVariantCfg.Variant)
                        ' cActionManager.CheckCurrentActionCfg()
                        '  cGlobalActionManager.LoadActionCfg(cLocalVariantManager.GlobalProgramPath(cLocalVariantManager.CurrentVariantCfg.Variant))
                        '  cGlobalActionManager.CheckCurrentActionCfg()
                        '   WriteActionStep()
                    End If
                End If
            Catch ex As Exception
                cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            End Try
        End SyncLock
    End Sub

    Public Function ShowInitPicture() As Boolean
        Try
            If cLocalVariantManager.CurrentVariantCfg.PicturePath <> "" Then
                cPictureShowManager.ShowPicture(cLocalVariantManager.CurrentVariantCfg.PicturePath)
            Else
                cPictureShowManager.ShowPicture(cMachineManager.MachineCellManager.MachineCellCfg.CellPicture)
            End If
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function ShowPassPicture() As Boolean
        Try

            cPictureShowManager.ShowPicture(cSystemManager.Settings.ResourcesFolder + "\Pass.jpg")
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function ShowNgPicture() As Boolean
        Try

            cPictureShowManager.ShowPicture(cSystemManager.Settings.ResourcesFolder + "\NG.bmp")
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function GetManulPLCAutoActionParameter(ByVal iIndex As Integer) As List(Of Object)
        Try
            Dim lListObject As New List(Of Object)
            For ii = 1 To 20
                Dim strTemp As String = cHMIPLC.ReadAny("PLC_AutoActionStep[" + cRunnerCfg.StationName + "].PLC_AutoActionParameter[" + (iIndex + 1).ToString + "].strParameter[" + ii.ToString + "]", GetType(String), New Integer() {30})
                lListObject.Add(strTemp)
            Next
            Return lListObject
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return Nothing
        End Try
    End Function

    Public Function GetPLCAutoActionParameter(ByVal iIndex As Integer) As List(Of Object)
        Try
            Dim lListObject As New List(Of Object)
            cPLCAutoActionParameter_Char = CType(cHMIPLC.GetValue(HMI_PLC_Interface.PLC_AutoActionStep + "[" + MachineStationCfg.ID.ToString + "]"), StructPLCAutoActionParameter_Char())
            For ii = 1 To 20
                lListObject.Add(cPLCAutoActionParameter_Char(iIndex).strParameterString(ii - 1).strParameterString)
            Next
            Return lListObject
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return Nothing
        End Try
    End Function

    Public Overrides Function Quit() As Boolean
        Try
            If cRunActionCfg.IsRunning Then
                If cRunActionCfg.ActionName = "Run" Or cRunActionCfg.ActionName = "FailRun" Then
                    Dim cSubStepCfg As clsSubStepCfg = CType(cRunActionCfg.GetParameter(0), clsSubStepCfg)
                    CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Abort(cLocalElement, cSystemElement)
                    CType(cActionLibManager.GetActionLibCfgFromKey(cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).Quit(cLocalElement, cSystemElement)
                End If
            End If
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
            iMainUI.RemoveStation(cRunnerCfg.StationName)
            RemoveHandler cVariantManager.VariantChanged, AddressOf VariantChanged
            RemoveHandler cVariantManager.LoadChanged, AddressOf LoadChanged
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

End Class
