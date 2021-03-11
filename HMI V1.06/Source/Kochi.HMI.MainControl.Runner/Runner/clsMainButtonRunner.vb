Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.Base
Imports System.Windows.Forms

Public Class clsMainButtonRunner
    Inherits clsRunnerBase
    Implements IDisposable
    Private cErrorMessageManager As clsErrorMessageManager
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cMainFormButtonManager As clsMainButtonManager
    Private lListFunctionButton As New Dictionary(Of String, MainFunctionButton)
    Private TempMachineStatus As New StructMachineStatus
    Private TempOldMachineStatus As New StructMachineStatus
    Private TempAutoButton() As StructAutoButton
    Private TempOldAutoButton() As StructAutoButton
    Private cMainTipsManager As clsMainTipsManager
    Private cUserManager As clsUserManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private bLastContinueStatus As Boolean = False
    Private bLastAbortStatus As Boolean = False
    Private iParentMainUI As IParentMainUI
    Private cVariantManager As clsVariantManager
    Private iMainUI As IMainUI
    Protected iTime As New Timer
    Protected bulResetDisable As Boolean
    Private bPLCReset(100) As Boolean
    Public Const Name As String = "MachineStatusRunner"
    Private cProgramButton As clsProgramButton
    Private cProgramCylinderButton As clsProgramCylinderButton
    Private bTeachMode As Boolean = False
    Private bCloseTeadMode As Boolean = False
    Private bIOCloeseTeachMode As Boolean = False

    Public ReadOnly Property TeachMode As Boolean
        Get
            Return bTeachMode
        End Get
    End Property

    Public Property IOCloeseTeachMode As Boolean
        Set(ByVal value As Boolean)
            bIOCloeseTeachMode = value
        End Set
        Get
            Return bIOCloeseTeachMode
        End Get
    End Property

    Public Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Object
        Try
            Me.cRunnerCfg = New clsRunnerCfg(Name)
            Me.cSystemElement = cSystemElement
            Me.cRunnerElement = cRunnerElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cUserManager = CType(cSystemElement(clsUserManager.Name), clsUserManager)
            cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            iParentMainUI = CType(cSystemElement(enumUIName.ParentMainForm.ToString), IParentMainUI)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
            cProgramButton = CType(cSystemElement(clsProgramButton.Name), clsProgramButton)
            cProgramCylinderButton = CType(cSystemElement(clsProgramCylinderButton.Name), clsProgramCylinderButton)
            iMainUI = CType(cSystemElement(enumUIName.MainForm.ToString), IMainUI)
            iTime.Interval = 100
            AddHandler iTime.Tick, AddressOf Time_Tick
            AddHandler CType(cMainFormButtonManager.GetMainButtonManagerCfgFromName(enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString).Source, MainFunctionButton).MainButton.Click, AddressOf Button_Click
            AddHandler CType(cMainFormButtonManager.GetMainButtonManagerCfgFromName(enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString).Source, MainFunctionButton).MainButton.Click, AddressOf Button_Click
            AddHandler cMainFormButtonManager.PageChanged, AddressOf PageChanged
            AddHandler cUserManager.UserChanged, AddressOf UserChanged
            AddHandler cUserManager.LoginOutChanged, AddressOf LoginOutChanged
            AddHandler cVariantManager.VariantChanged, AddressOf VariantChanged
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Overrides Function Run() As Boolean
        Try
            i.Toggle = i.StepOutputNumber <> i.StepInputNumber
            i.StepOutputNumber = i.StepInputNumber
            If cErrorMessageManager.GetStationManagerStateFromKey(cRunnerCfg.StationName) = enumErrorMessageManagerState.Alarm Then Return False
            Select Case i.StepOutputNumber


                Case -100
                    cHMIPLC = cDeviceManager.GetPLCDevice()
                    If IsNothing(cHMIPLC) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cRunnerCfg.StationName, cLanguageManager.GetTextLine("MachineStatusRunner", "1"), enumExceptionType.Alarm))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -99
                    If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("MachineStatusRunner", "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, cRunnerCfg.StationName))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -98
                    lListFunctionButton.Clear()
                    For Each element In [Enum].GetValues(GetType(enumHMI_LEFT_FUNCTION_ITEM))
                        lListFunctionButton.Add(element.ToString, cMainFormButtonManager.GetMainButtonManagerCfgFromName(element.ToString).Source)
                    Next
                    For Each element As MainFunctionButton In lListFunctionButton.Values
                        AddHandler element.MainButton.MouseUp, AddressOf Button_MouseUp
                        AddHandler element.MainButton.MouseDown, AddressOf Button_MouseDown
                    Next
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -97
                    TempMachineStatus = CType(cHMIPLC.GetValue(HMI_PLC_MachineStatus_AdsName), StructMachineStatus)
                    cMachineStatusManager.MachineStatus = TempMachineStatus
                    UserChanged("", Nothing)
                    i.StepInputNumber = i.Address_Home

                Case 0
                    TempMachineStatus = CType(cHMIPLC.GetValue(HMI_PLC_MachineStatus_AdsName), StructMachineStatus)
                    TempAutoButton = CType(cHMIPLC.GetValue(HMI_PLC_Interface.PLC_AutoButton), StructAutoButton())
                    cMachineManager.MachineStatus = TempMachineStatus
                    If IsNothing(TempOldAutoButton) Then
                        ReDim TempOldAutoButton(TempAutoButton.Length - 1)
                    End If
                    If TempAutoButton.Length <> TempOldAutoButton.Length Then
                        ReDim TempOldAutoButton(TempAutoButton.Length - 1)
                    End If
                    cMachineStatusManager.MachineStatus = TempMachineStatus
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 1
                    For Each element As String In lListFunctionButton.Keys
                        Select Case element
                            Case enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulPowerON)
                            Case enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulAuto)
                            Case enumHMI_LEFT_FUNCTION_ITEM.Manual.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulManual)
                            Case enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulReset)
                            Case enumHMI_LEFT_FUNCTION_ITEM.Forward.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulStepForward)
                            Case enumHMI_LEFT_FUNCTION_ITEM.Backward.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulStepBackward)
                            Case enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString
                                lListFunctionButton(element).SetIndicateBackColor(TempMachineStatus.bulCleanMode)
                        End Select
                    Next
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 2
                    If TempMachineStatus.bulPowerON And Not TempOldMachineStatus.bulPowerON Then
                        PowerChanged("bulPowerON")
                    End If
                    If Not TempMachineStatus.bulPowerON And TempOldMachineStatus.bulPowerON Then
                        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", False)
                        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode", False)
                        PowerChanged("bulPowerON")
                    End If
                    If TempMachineStatus.bulManual And Not TempOldMachineStatus.bulManual Then
                        PowerChanged("bulManual")
                    End If
                    If Not TempMachineStatus.bulManual And TempOldMachineStatus.bulManual Then
                        PowerChanged("bulManual")
                    End If

                    If Not TempMachineStatus.bulTeachMode And TempOldMachineStatus.bulTeachMode Then
                        bCloseTeadMode = True
                        PowerChanged("bulTeachMode")
                    End If

                    If bIOCloeseTeachMode And bTeachMode Then
                        bCloseTeadMode = True
                    End If
                    If bCloseTeadMode Then
                        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name = enumHMI_LEFT_ITEM.Home.ToString Or bIOCloeseTeachMode Then
                            iParentMainUI.BackUI()
                            CloseTeachModeIO()
                            bCloseTeadMode = False
                            bIOCloeseTeachMode = False
                        End If
                    End If

                    If TempMachineStatus.bulTeachMode And Not TempOldMachineStatus.bulTeachMode Then
                        PowerChanged("bulTeachMode")
                        bTeachMode = True
                    End If

                    TempOldMachineStatus = TempMachineStatus
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 3
                    For j = 0 To TempAutoButton.Length - 1
                        If IsNothing(TempOldAutoButton(j)) Then TempOldAutoButton(j) = New StructAutoButton
                        If TempAutoButton(j).bulContinue And TempAutoButton(j).bulContinue <> TempOldAutoButton(j).bulContinue Then
                            cMainTipsManager.Auto_Continue((j + 1).ToString)
                            ResetError()
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulReset", True)
                            bPLCReset(j) = True
                        End If
                        If bPLCReset(j) And Not TempAutoButton(j).bulContinue Then
                            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulReset", False)
                            bPLCReset(j) = False
                        End If
                        TempOldAutoButton(j).bulContinue = TempAutoButton(j).bulContinue
                    Next

                    i.StepInputNumber = i.StepOutputNumber + 1
                Case 4
                    For j = 0 To TempAutoButton.Length - 1
                        If TempAutoButton(j).bulFail And TempAutoButton(j).bulFail <> TempOldAutoButton(j).bulFail Then
                            cMainTipsManager.Auto_Abort((j + 1).ToString)
                        End If
                        TempOldAutoButton(j).bulFail = TempAutoButton(j).bulFail
                    Next
                    i.StepInputNumber = i.Address_Home

            End Select
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Private Sub Button_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name <> enumHMI_LEFT_ITEM.Home.ToString Then Return
        ' If IsNothing(cErrorMessageManager.CurrentHMIException) Then Return
        ' If cErrorMessageManager.CurrentHMIException.ExceptionType <> enumExceptionType.PLC Then Return
        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name <> enumHMI_LEFT_ITEM.Home.ToString Then Return
        Select Case sender.name
            Case enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulPowerON", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulAuto", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.Manual.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulManual", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulReset", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.Forward.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulStepForward", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.Backward.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulStepBackward", True)
            Case enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulCleanMode", True)
        End Select
    End Sub

    Private Sub CloseTeachMode()
        cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", False)
        cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode", False)
    End Sub
    Private Sub CloseTeachModeIO()
        Dim cHmiDevice As clsHMIDeviceBase
        ' If Not cLocalElement.ContainsKey(clsFormFontResize.Name) Then cLocalElement.Add(clsFormFontResize.Name, Nothing)
        ' If Not cLocalElement.ContainsKey(enumUIName.ParentDevicesForm.ToString) Then cLocalElement.Add(enumUIName.ParentDevicesForm.ToString, Nothing)
        '  If Not cLocalElement.ContainsKey(clsErrorMessageManager.Name) Then cLocalElement.Add(clsErrorMessageManager.Name, cErrorMessageManager)
        '  If Not cLocalElement.ContainsKey(clsChangePage.Name) Then cLocalElement.Add(clsChangePage.Name, Nothing)
        ' If Not cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then cLocalElement.Add(clsMachineStationCfg.Name, Nothing)
        ' If Not cLocalElement.ContainsKey(clsMainStepCfg.Name) Then cLocalElement.Add(clsMainStepCfg.Name, Nothing)
        ' If Not cLocalElement.ContainsKey(clsSubStepCfg.Name) Then cLocalElement.Add(clsSubStepCfg.Name, Nothing)

        For Each elementIndex As Integer In cDeviceManager.GetDevicesListKey
            Dim element As clsDeviceCfg = cDeviceManager.GetDeviceCfgFromKey(elementIndex)
            cHmiDevice = element.Source
            If IsNothing(cHmiDevice) Then Continue For

            cHmiDevice.CreateProgramUI(cLocalElement, cSystemElement)
            If Not IsNothing(cHmiDevice.ProgramUI) Then
                cHmiDevice.ProgramUI.CloseIO(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter))
                cHmiDevice.ProgramUI.Quit(cLocalElement, cSystemElement)
            End If
          

            cHmiDevice.CreateControlUI(cLocalElement, cSystemElement)
            If Not IsNothing(cHmiDevice.ControlUI) Then
                cHmiDevice.ControlUI.CloseIO(cLocalElement, cSystemElement, clsParameter.ToList(element.InitParameter), clsParameter.ToList(element.ControlParameter))
                cHmiDevice.ControlUI.Quit(cLocalElement, cSystemElement)
            End If
           
        Next

        If Not IsNothing(cHMIPLC) Then
            Dim iPageNr As Integer = cProgramButton.ListPage.Keys.Count
            Dim cDefaultValue() As Boolean = Enumerable.Repeat(New Boolean, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramButton, cDefaultValue)
            Dim cDefaultCylinderValue() As StructDebugCylinder = Enumerable.Repeat(New StructDebugCylinder, iPageNr * HMI_PLC_Interface.CON_MAXIMUM_PageNumber).ToArray()
            cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_ProgramCylinderButton, cDefaultCylinderValue)
        End If

        If Not IsNothing(cHMIPLC) Then cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode", False)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DI_EL1008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DI_EP1008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DO_EL2008_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_DO_Festo_AdsName)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_ProgramButton)
        If Not IsNothing(cHMIPLC) Then cHMIPLC.RemoveNotificationEx(HMI_PLC_Interface.HMI_ProgramCylinderButton)
        bTeachMode = False
    End Sub

    Private Sub Button_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name <> enumHMI_LEFT_ITEM.Home.ToString Then Return
        '  If IsNothing(cErrorMessageManager.CurrentHMIException) Then Return
        ' If cErrorMessageManager.CurrentHMIException.ExceptionType <> enumExceptionType.PLC Then Return
        If cMainFormButtonManager.CurrentMainButtonManagerCfg.Name <> enumHMI_LEFT_ITEM.Home.ToString Then Return
        Select Case sender.name
            Case enumHMI_LEFT_FUNCTION_ITEM.PowerOn.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulPowerON", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.Auto.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulAuto", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.Manual.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulManual", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulReset", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.Forward.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulStepForward", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.Backward.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulStepBackward", False)
            Case enumHMI_LEFT_FUNCTION_ITEM.CleanMode.ToString
                cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulCleanMode", False)
        End Select
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Select Case sender.name
            Case enumHMI_LEFT_FUNCTION_ITEM.Reset.ToString
                ResetError()
            Case enumHMI_LEFT_FUNCTION_ITEM.Exit.ToString
                iMainUI.AutoClose()
        End Select
    End Sub

    Private Sub ResetError()
        If bulResetDisable Then
            Return
        End If
        bulResetDisable = True
        iTime.Start()
        Dim cTempErrorMessageManager As clsErrorMessageManager

        cTempErrorMessageManager = CType(CType(cMainFormButtonManager.CurrentMainButtonManagerCfg.UISource, IParentUI).LocalElement(clsErrorMessageManager.Name), clsErrorMessageManager)
        cTempErrorMessageManager.Reset()
    End Sub

    Public Sub PageChanged(ByVal strName As String, ByVal strCurrentName As String, ByVal eMainButtonType As enumMainButtonType)
        ' cMainFormButtonManager.EnableFunctionMainButton(cMachineStatusManager.MachineStatus, cUserManager.CurrentUserCfg.Level)
    End Sub

    Private Sub LoginOutChanged()
        cMainFormButtonManager.EnableMainLeftButton()
        cMainFormButtonManager.EnableFunctionMainButton()
        iParentMainUI.EnableMainLeftButton()
        iMainUI.EnableMainLeftButton()
    End Sub

    Private Sub VariantChanged(ByVal strVariant As String, ByVal cVariantCfg As clsVariantCfg, ByVal eSelectVariantType As enumSelectVariantType)
        cMainFormButtonManager.EnableMainLeftButton()
        cMainFormButtonManager.EnableFunctionMainButton()
        iParentMainUI.EnableMainLeftButton()
        iMainUI.EnableMainLeftButton()
    End Sub
    Public Sub UserChanged(ByVal strUserName As String, ByVal cUserCfg As clsUserCfg)
        cMainFormButtonManager.EnableMainLeftButton()
        cMainFormButtonManager.EnableFunctionMainButton()
        iParentMainUI.EnableMainLeftButton()
        iMainUI.EnableMainLeftButton()
    End Sub

    Public Sub PowerChanged(ByVal strName As String)
        cMainFormButtonManager.EnableMainLeftButton(strName)
        cMainFormButtonManager.EnableFunctionMainButton()
        iParentMainUI.EnableMainLeftButton()
        iMainUI.EnableMainLeftButton()
    End Sub

    Public Overrides Function Reset() As Boolean
        Try
            For Each element As MainFunctionButton In lListFunctionButton.Values
                RemoveHandler element.MainButton.MouseUp, AddressOf Button_MouseUp
                RemoveHandler element.MainButton.MouseDown, AddressOf Button_MouseDown
            Next
            i.StepInputNumber = i.Address_Origin
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function


    Public Sub Time_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        iTime.Enabled = False
        bulResetDisable = False
    End Sub

    Public Overrides Function Quit() As Boolean
        Try
            If bTeachMode Then
                CloseTeachModeIO()
            End If
            RemoveHandler cMainFormButtonManager.PageChanged, AddressOf PageChanged
            RemoveHandler cUserManager.UserChanged, AddressOf UserChanged
            RemoveHandler cUserManager.LoginOutChanged, AddressOf LoginOutChanged
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

End Class
