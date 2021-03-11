Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent
Imports System.Threading
Imports Kochi.HMI.MainControl.Base

Public Class clsMachineDataRunner
    Inherits clsRunnerBase
    Implements IDisposable
    Private cErrorMessageManager As clsErrorMessageManager
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cMainFormButtonManager As clsMainButtonManager
    Private cMachineManager As clsMachineManager
    Private cMachineDataManager As clsMachineDataManager
    Private cMachineStatusManager As clsMachineStatusManager
    Private lListFunctionButton As New Dictionary(Of String, MainFunctionButton)
    Private TempMachineStatus As StructMachineStatus
    Private LastMachineStatus As New StructMachineStatus
    Private LastMESStatus As New StructMachineStatus
    Private cThread As Thread
    Private cRunActionCfg As New clsRunActionCfg
    Private cLanguageManager As clsLanguageManager
    Private strTempAction As String
    Private strTempDescription As String
    Private cHMIMES As clsHMIMESBase
    Public Const Name As String = "MachineDataRunner"
    Private strLastMachineState As String = String.Empty
    Private cMainTipsManager As clsMainTipsManager
    Public bChangeResouse As Boolean = False
    Public Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Object
        Try
            Me.cRunnerCfg = New clsRunnerCfg(Name)
            Me.cSystemElement = cSystemElement
            Me.cRunnerElement = cRunnerElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cMachineDataManager = New clsMachineDataManager
            cMainFormButtonManager = CType(cSystemElement(clsMainButtonManager.Name), clsMainButtonManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
            cMachineStatusManager = CType(cSystemElement(clsMachineStatusManager.Name), clsMachineStatusManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
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
                        cErrorMessageManager.AddHMIException(New clsHMIException(cRunnerCfg.StationName, cLanguageManager.GetTextLine("MachineDataRunner", "1"), enumExceptionType.Alarm))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -99
                    If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("MachineDataRunner", "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, cRunnerCfg.StationName))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -98
                    If Not cMachineDataManager.Init(cSystemElement) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("MachineDataRunner", "3"), cRunnerCfg.StationName))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -97
                    i.StepInputNumber = i.Address_Home

                Case 0
                    TempMachineStatus = CType(cHMIPLC.GetValue(HMI_PLC_MachineStatus_AdsName), StructMachineStatus)
                    TempMachineStatus.bulWork = cMachineStatusManager.GetWork
                    TempMachineStatus.bulAlarm = TempMachineStatus.intErrorID <> 0
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 1
                    If cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).LastStatusValue <> TempMachineStatus.bulPowerON Then
                        If TempMachineStatus.bulPowerON And Not cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).InsertValue Then
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).ClickValue = False
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).InsertValue = True
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).UpdateValue = False
                            strTempAction = enumManchineActionType.PowerOn.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "PowerOn")
                            i.StepInputNumber = 110
                            Return False
                        End If
                        If Not TempMachineStatus.bulPowerON And cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).InsertValue Then

                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).InsertValue = False
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).UpdateValue = True
                            strTempAction = enumManchineActionType.PowerOn.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "PowerOn")
                            i.StepInputNumber = 120
                            Return False
                        End If
                    End If
                    cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.PowerOn.ToString).LastStatusValue = TempMachineStatus.bulPowerON
                    If TempMachineStatus.bulPowerON And Not LastMESStatus.bulPowerON Then
                        strTempAction = enumManchineActionType.PowerOn.ToString
                        i.StepInputNumber = 130
                        LastMESStatus.bulPowerON = TempMachineStatus.bulPowerON
                        Return False
                    End If
                    If Not TempMachineStatus.bulPowerON And LastMESStatus.bulPowerON Then
                        bChangeResouse = False
                        strTempAction = enumManchineActionType.PowerOff.ToString
                        LastMESStatus.bulPowerON = TempMachineStatus.bulPowerON
                        i.StepInputNumber = 130
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 2
                    If cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).LastStatusValue <> TempMachineStatus.bulAuto And TempMachineStatus.bulPowerON Then
                        If TempMachineStatus.bulAuto And Not cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).InsertValue Then
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).ClickValue = False
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).InsertValue = True
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).UpdateValue = False
                            strTempAction = enumManchineActionType.Auto.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Auto")
                            i.StepInputNumber = 110
                            Return False
                        End If
                    End If
                    If Not TempMachineStatus.bulAuto And cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).InsertValue Then
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).InsertValue = False
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).UpdateValue = True
                        strTempAction = enumManchineActionType.Auto.ToString
                        strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Auto")
                        i.StepInputNumber = 120
                        Return False
                    End If


                    cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Auto.ToString).LastStatusValue = TempMachineStatus.bulAuto
                    If TempMachineStatus.bulAuto And Not LastMESStatus.bulAuto Then
                        strTempAction = enumManchineActionType.Auto.ToString
                        LastMESStatus.bulAuto = TempMachineStatus.bulAuto
                        i.StepInputNumber = 130
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 3
                    If cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).LastStatusValue <> TempMachineStatus.bulAlarm And TempMachineStatus.bulPowerON Then
                        If TempMachineStatus.bulAlarm And Not cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).InsertValue Then

                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).InsertValue = True
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).UpdateValue = False
                            strTempAction = enumManchineActionType.Alarm.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Alarm")
                            i.StepInputNumber = 110
                            Return False
                        End If
                    End If
                    If Not TempMachineStatus.bulAlarm And cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).InsertValue Then
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).InsertValue = False
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).UpdateValue = True
                        strTempAction = enumManchineActionType.Alarm.ToString
                        strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Alarm")
                        i.StepInputNumber = 120
                        Return False
                    End If


                    cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Alarm.ToString).LastStatusValue = TempMachineStatus.bulAlarm
                    If TempMachineStatus.bulAlarm And Not LastMESStatus.bulAlarm Then
                        bChangeResouse = False
                        strTempAction = enumManchineActionType.Alarm.ToString
                        LastMESStatus.bulAlarm = TempMachineStatus.bulAlarm
                        i.StepInputNumber = 130
                        Return False
                    End If
                    If Not TempMachineStatus.bulAlarm And LastMESStatus.bulAlarm Then
                        strTempAction = enumManchineActionType.PowerOn.ToString
                        LastMESStatus.bulAlarm = TempMachineStatus.bulAlarm
                        i.StepInputNumber = 130
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 4
                    If cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).LastStatusValue <> TempMachineStatus.bulWork And TempMachineStatus.bulPowerON And TempMachineStatus.bulAuto Then
                        If TempMachineStatus.bulWork And Not cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).InsertValue Then
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).InsertValue = True
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).UpdateValue = False
                            strTempAction = enumManchineActionType.Work.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Work")
                            i.StepInputNumber = 110
                            Return False
                        End If
                    End If
                    If Not TempMachineStatus.bulWork And cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).InsertValue Then
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).InsertValue = False
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).UpdateValue = True
                        strTempAction = enumManchineActionType.Work.ToString
                        strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Work")
                        i.StepInputNumber = 120
                        Return False
                    End If

                    cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Work.ToString).LastStatusValue = TempMachineStatus.bulWork
                    If TempMachineStatus.bulWork And Not LastMESStatus.bulWork Then
                        strTempAction = enumManchineActionType.Work.ToString
                        LastMESStatus.bulWork = TempMachineStatus.bulWork
                        i.StepInputNumber = 130
                        Return False
                    End If
                    If Not TempMachineStatus.bulWork And LastMESStatus.bulWork And TempMachineStatus.bulPowerON Then
                        strTempAction = enumManchineActionType.Waiting.ToString
                        LastMESStatus.bulWork = TempMachineStatus.bulWork
                        i.StepInputNumber = 130
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 5
                    If cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).LastStatusValue <> TempMachineStatus.bulWork And TempMachineStatus.bulPowerON And TempMachineStatus.bulAuto Then
                        If Not TempMachineStatus.bulWork And Not cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).InsertValue Then
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).InsertValue = True
                            cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).UpdateValue = False
                            strTempAction = enumManchineActionType.Waiting.ToString
                            strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Waiting")
                            i.StepInputNumber = 110
                            Return False
                        End If
                    End If

                    If (TempMachineStatus.bulWork Or Not TempMachineStatus.bulPowerON) And cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).InsertValue Then
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).InsertValue = False
                        cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).UpdateValue = True
                        strTempAction = enumManchineActionType.Waiting.ToString
                        strTempDescription = cLanguageManager.GetTextLine("MachineDataRunner", "Waiting")
                        i.StepInputNumber = 120
                        Return False
                    End If

                    cMachineDataManager.GetManchineActionCfgFromName(enumManchineActionType.Waiting.ToString).LastStatusValue = Not TempMachineStatus.bulWork
                    i.StepInputNumber = i.StepOutputNumber + 1


                Case 6

                    If TempMachineStatus.bulManual And Not LastMachineStatus.bulManual And Not TempMachineStatus.bulCleanMode Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Manual"), enumMainTipsManagerType.Mode))
                    End If

                    'If TempMachineStatus.bulDebugMode And Not LastMachineStatus.bulDebugMode Then
                    '    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Debug"), enumMainTipsManagerType.Mode))
                    'End If

                    'If TempMachineStatus.bulTeachMode And Not LastMachineStatus.bulTeachMode Then
                    '    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Debug"), enumMainTipsManagerType.Mode))
                    'End If

                    If TempMachineStatus.bulCleanMode And Not LastMachineStatus.bulCleanMode Then
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Clean"), enumMainTipsManagerType.Mode))
                    End If

                    If (Not TempMachineStatus.bulCleanMode And LastMachineStatus.bulCleanMode) Or _
                         (Not TempMachineStatus.bulManual And LastMachineStatus.bulManual) Then
                        cMainTipsManager.CleanPLCTips("Mode")
                        If TempMachineStatus.bulCleanMode Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Clean"), enumMainTipsManagerType.Mode))
                        ElseIf TempMachineStatus.bulManual Then
                            cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Manual"), enumMainTipsManagerType.Mode))
                        End If
                    End If

                    LastMachineStatus.bulManual = TempMachineStatus.bulManual
                    LastMachineStatus.bulCleanMode = TempMachineStatus.bulCleanMode
                    i.StepInputNumber = i.Address_Home

                Case 110
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.StepOutputNumber + 1
                    End If

                Case 111
                    cRunActionCfg.ActionName = "MachineDataManager.InSertData"
                    cRunActionCfg.Clean()
                    cRunActionCfg.AddParameter(strTempAction)
                    cRunActionCfg.AddParameter(strTempDescription)
                    cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cRunActionCfg.IsRunning = True
                    cRunActionCfg.Result = False
                    cThread = New Thread(AddressOf RunAction)
                    cThread.IsBackground = True
                    cThread.Start()

                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 112
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.Address_Home
                    End If

                Case 120
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.StepOutputNumber + 1
                    End If

                Case 121
                    cRunActionCfg.ActionName = "MachineDataManager.UpdateData"
                    cRunActionCfg.Clean()
                    cRunActionCfg.AddParameter(strTempAction)
                    cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cRunActionCfg.IsRunning = True
                    cRunActionCfg.Result = False
                    cThread = New Thread(AddressOf RunAction)
                    cThread.IsBackground = True
                    cThread.Start()

                    i.StepInputNumber = i.StepOutputNumber + 1
                Case 122
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.Address_Home
                    End If

                Case 130
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.StepOutputNumber + 1
                    End If

                Case 131
                    cRunActionCfg.ActionName = "MachineDataManager.UpdateMES"
                    cRunActionCfg.Clean()
                    cRunActionCfg.AddParameter(strTempAction)
                    cRunActionCfg.IsRunning = True
                    cRunActionCfg.Result = False
                    cThread = New Thread(AddressOf RunAction)
                    cThread.IsBackground = True
                    cThread.Start()

                    i.StepInputNumber = i.StepOutputNumber + 1
                Case 132
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.Address_Home
                    End If

            End Select
            Return True
        Catch ex As Exception
            cErrorMessageManager.AddHMIException(New clsHMIException(ex, enumExceptionType.Alarm, cRunnerCfg.StationName))
            Return False
        End Try
    End Function

    Public Sub RunAction()
        If cRunActionCfg.ActionName = "MachineDataManager.InSertData" Then
            cMachineDataManager.InSertData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1), cRunActionCfg.GetParameter(2))
            If cRunActionCfg.GetParameter(0) <> enumManchineActionType.Alarm.ToString Then
                strLastMachineState = cRunActionCfg.GetParameter(0)
            End If
        End If
        If cRunActionCfg.ActionName = "MachineDataManager.UpdateData" Then
            cMachineDataManager.UpdateData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1))
        End If

        If cRunActionCfg.ActionName = "MachineDataManager.UpdateMES" Then
            UpdateMesStatus(cRunActionCfg.GetParameter(0))
        End If


        cRunActionCfg.Result = True
        cRunActionCfg.IsRunning = False
    End Sub

    Private Sub UpdateChange(ByVal strManchineActionType As String)
        If strManchineActionType <> enumManchineActionType.PowerOff.ToString Then
            bChangeResouse = True
        End If
    End Sub


    Public Sub UpdateMesStatus(ByVal strManchineActionType As String)
        Try
            ' If strLastMachineState = "" Then Return
            Dim strResult As String = String.Empty
            If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.MachineStatusType) = "TRUE" Then
                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If element.MES = "" Or element.MES = "NONE" Then Continue For
                    Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(element.ID, element.MES, GetType(clsHMIMESBase))
                    If IsNothing(cDeviceCfg) Then
                        Continue For
                    End If
                    cHMIMES = cDeviceCfg.Source
                    Dim strNewstate As String = cMachineManager.MachineStatusParameterManager.GetMachineStatusParameter(strManchineActionType)
                    If strNewstate = "" Then
                        UpdateChange(strManchineActionType)
                        Return
                    End If

                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", "changeResourceState:" + strNewstate, enumMainTipsManagerType.Mode))
                    If Not cHMIMES.changeResourceState(strNewstate, strResult) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(strResult, enumExceptionType.Alarm, cRunnerCfg.StationName))
                    End If
                    If Not TempMachineStatus.bulCleanMode Then
                        cMainTipsManager.CleanPLCTips("Mode")
                    Else
                        cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Clean"), enumMainTipsManagerType.Mode))
                    End If
                Next
            Else
                If cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.MachineStatus) = "" Or cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.MachineStatus) = "NONE" Then
                    UpdateChange(strManchineActionType)
                    Return
                End If
                Dim cDeviceCfg As clsDeviceCfg = cDeviceManager.GetDeviceFromTypeAndStationIndex(0, cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.MachineStatus), GetType(clsHMIMESBase))
                If IsNothing(cDeviceCfg) Then
                    UpdateChange(strManchineActionType)
                    Return
                End If
                cHMIMES = cDeviceCfg.Source

                Dim strNewstate As String = cMachineManager.MachineStatusParameterManager.GetMachineStatusParameter(strManchineActionType)
                If strNewstate = "" Then
                    UpdateChange(strManchineActionType)
                    Return
                End If

                cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", "changeResourceState:" + strNewstate, enumMainTipsManagerType.Mode))

                If Not cHMIMES.changeResourceState(strNewstate, strResult) Then
                    cErrorMessageManager.AddHMIException(New clsHMIException(strResult, enumExceptionType.Alarm, cRunnerCfg.StationName))
                End If
                If Not TempMachineStatus.bulCleanMode Then
                    cMainTipsManager.CleanPLCTips("Mode")
                Else
                    cMainTipsManager.AddTips(New clsMainTipsManagerCfg("Mode", cLanguageManager.GetTextLine("MachineDataRunner", "Clean"), enumMainTipsManagerType.Mode))
                End If
            End If
            UpdateChange(strManchineActionType)
        Catch ex As Exception
            bChangeResouse = False
            cErrorMessageManager.AddHMIException(New clsHMIException("changeResourceState:" + ex.Message, enumExceptionType.Alarm, cRunnerCfg.StationName))
        End Try
    End Sub

    Public Overrides Function Reset() As Boolean
        Try
            i.StepInputNumber = i.Address_Origin
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Overrides Function Quit() As Boolean
        Try
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
