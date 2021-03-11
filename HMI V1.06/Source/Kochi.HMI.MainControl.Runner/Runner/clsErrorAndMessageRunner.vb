Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent
Imports System.Threading
Imports Kochi.HMI.MainControl.Base

Public Class clsErrorAndMessageRunner
    Inherits clsRunnerBase
    Implements IDisposable

    Private cErrorMessageManager As clsErrorMessageManager
    Private cHMIPLC As clsHMIPLC
    Private cDeviceManager As clsDeviceManager
    Private cAlarmDataManager As clsAlarmDataManager
    Private cErrorCodeManager As clsErrorCodeManager
    Private cPlcMessageManager As clsPlcMessageManager
    Private TempMachineStatus As StructMachineStatus
    Private cMainTipsManager As clsMainTipsManager
    Private strTempValue As String = String.Empty
    Private strStationName As String = String.Empty
    Private strTempAlarmValue As String = String.Empty
    Private strTempStationValue As String = String.Empty
    Private cTempErrorCodeCfg As clsErrorCodeCfg
    Private cTempPLCMessageCfg As clsPlcMessageCfg
    Private cRunActionCfg As New clsRunActionCfg
    Private cLanguageManager As clsLanguageManager
    Private iLastErrorCode As Integer
    Private iLastMessage As Integer
    Private cThread As Thread
    Public Const Name As String = "ErrorAndMessageRunner"
    Private bStationErrorMessage As Boolean = False
    Private strStationErrorID As String = ""
    Private bHMIError As Boolean = False
    Private strHMIErrorStation As String = String.Empty
    Private cMachineManager As clsMachineManager
    Public Overrides Function Init(ByVal cSystemElement As Dictionary(Of String, Object), ByVal cRunnerElement As Dictionary(Of String, Object)) As Object
        Try
            Me.cRunnerCfg = New clsRunnerCfg(Name)
            Me.cSystemElement = cSystemElement
            Me.cRunnerElement = cRunnerElement
            cErrorMessageManager = CType(cSystemElement(clsErrorMessageManager.Name), clsErrorMessageManager)
            cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
            cAlarmDataManager = New clsAlarmDataManager
            cErrorCodeManager = CType(cSystemElement(clsErrorCodeManager.Name), clsErrorCodeManager)
            cMainTipsManager = CType(cSystemElement(clsMainTipsManager.Name), clsMainTipsManager)
            cPlcMessageManager = CType(cSystemElement(clsPlcMessageManager.Name), clsPlcMessageManager)
            cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
            cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
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
                        cErrorMessageManager.AddHMIException(New clsHMIException(cRunnerCfg.StationName, cLanguageManager.GetTextLine("ErrorAndMessageRunner", "1"), enumExceptionType.Alarm))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -99
                    If cHMIPLC.DeviceState <> enumDeviceState.OPEN Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("ErrorAndMessageRunner", "2", cHMIPLC.Name, cHMIPLC.DeviceState.ToString), enumExceptionType.Alarm, cRunnerCfg.StationName))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -98
                    If Not cAlarmDataManager.Init(cSystemElement) Then
                        cErrorMessageManager.AddHMIException(New clsHMIException(cLanguageManager.GetTextLine("ErrorAndMessageRunner", "3"), cRunnerCfg.StationName))
                        Return False
                    End If
                    i.StepInputNumber = i.StepOutputNumber + 1

                Case -97
                    i.StepInputNumber = i.Address_Home

                Case 0
                    TempMachineStatus = CType(cHMIPLC.GetValue(HMI_PLC_MachineStatus_AdsName), StructMachineStatus)
                    i.StepInputNumber = i.StepOutputNumber + 1
                Case 1
                    If TempMachineStatus.intErrorID <> 0 Then
                        strTempValue = ""
                        strTempStationValue = ""
                        bHMIError = False
                        strHMIErrorStation = ""
                        cTempErrorCodeCfg = cErrorCodeManager.GetErrorCfgFromCode(TempMachineStatus.intErrorID.ToString)
                        If Not IsNothing(cTempErrorCodeCfg) Then   
                                strTempValue = cTempErrorCodeCfg.ActiveMessage
                        End If

                        If TempMachineStatus.strErrorMessage <> "" Then
                            If TempMachineStatus.strErrorMessage.IndexOf("StationError;") >= 0 Then
                                Dim cValue() As String = TempMachineStatus.strErrorMessage.Split(";")
                                If cValue.Length = 3 Then
                                    strTempStationValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "4", strTempValue, cValue(1), cValue(2))
                                End If
                            End If
                            If TempMachineStatus.strErrorMessage.IndexOf("CarrierError;") >= 0 Then
                                Dim cValue() As String = TempMachineStatus.strErrorMessage.Split(";")
                                If cValue.Length = 4 Then
                                    strTempStationValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "5", strTempValue, cValue(1), cValue(2), cValue(3))
                                End If
                            End If
                            If TempMachineStatus.strErrorMessage.IndexOf("Station;") >= 0 Then
                                Dim cValue() As String = TempMachineStatus.strErrorMessage.Split(";")
                                If cValue.Length = 2 Then
                                    bHMIError = True
                                    strHMIErrorStation = cValue(1)
                                End If
                                Dim cMainTipsManagerCfg As clsMainTipsManagerCfg = cMainTipsManager.GetMainTipsCfgTypeFromKey(strHMIErrorStation)
                                If Not IsNothing(cMainTipsManagerCfg) Then
                                    strTempValue = strTempValue + "\r\n" + cMainTipsManagerCfg.Text
                                End If
                            End If
                        End If


                        If strTempValue = "" Then
                            strTempAlarmValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "Code") + TempMachineStatus.intErrorID.ToString + " " + TempMachineStatus.strErrorMessage
                            strTempValue = TempMachineStatus.strErrorMessage
                        Else
                            If strTempStationValue <> "" Then
                                strTempAlarmValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "Code") + TempMachineStatus.intErrorID.ToString + " " + strTempStationValue
                                strTempValue = strTempStationValue
                            Else
                                strTempAlarmValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "Code") + TempMachineStatus.intErrorID.ToString + " " + strTempValue
                                strTempValue = strTempValue
                            End If

                        End If

                        If TempMachineStatus.intErrorID <> iLastErrorCode And iLastErrorCode = 0 Then
                            cErrorMessageManager.AddHMIException(New clsHMIException(strTempAlarmValue, enumExceptionType.PLC, "PLC"))
                            iLastErrorCode = TempMachineStatus.intErrorID
                            i.StepInputNumber = 100
                            Return False
                        End If
                    End If

                    If TempMachineStatus.intErrorID = 0 And iLastErrorCode <> 0 Then
                        ' If bHMIError = True Then
                        '  cMainTipsManager.Auto_Continue(strHMIErrorStation)
                        ' End If
                        cErrorMessageManager.Clean("PLC")
                    End If

                    If TempMachineStatus.intErrorID <> iLastErrorCode Then
                        i.StepInputNumber = 110
                        Return False
                    End If



                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 2
                    If TempMachineStatus.intMessageID <> 0 And TempMachineStatus.intMessageID <> iLastMessage Then
                        strTempValue = ""
                        strStationName = "PLC"
                        cMainTipsManager.CleanPLCTips(strStationName)
                        For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                            cMainTipsManager.CleanStationTips("PLC" + element.ID.ToString)
                        Next
                        If TempMachineStatus.strMessage.IndexOf("StationMessage:") >= 0 Then
                            Dim cValue() As String = TempMachineStatus.strMessage.Split(":")
                            If cValue.Length = 2 Then
                                strStationName = "PLC" + cValue(1)
                                cMainTipsManager.CleanStationTips(cValue(1))
                            End If
                        End If

                        If TempMachineStatus.strMessage.IndexOf("StationErrorMessage:") >= 0 Then
                            Dim cValue() As String = TempMachineStatus.strMessage.Split(":")
                            If cValue.Length = 2 Then
                                strStationName = "PLC" + cValue(1)
                            End If
                            strStationErrorID = cValue(1)
                            bStationErrorMessage = True
                        Else
                            strStationErrorID = ""
                            bStationErrorMessage = False
                        End If

                        cTempPLCMessageCfg = cPlcMessageManager.GetPlcMessageCfgFromKey(TempMachineStatus.intMessageID.ToString)
                        If Not IsNothing(cTempPLCMessageCfg) Then
                            If cTempPLCMessageCfg.Picture <> "" Then
                                Dim cMainRunner As clsMainRunner = cSystemElement(clsMainRunner.Name)
                                For Each element As clsStationRunnerBase In cMainRunner.lStationRunnerElement.Values
                                    element.PictureShowManager.ShowPicture(cTempPLCMessageCfg.Picture)
                                Next
                            End If

                            strTempValue = cTempPLCMessageCfg.ActiveMessage
                        End If
                        If bStationErrorMessage Then
                            Dim cMainTipsManagerCfg As clsMainTipsManagerCfg = cMainTipsManager.GetMainTipsCfgTypeFromKey(strStationErrorID)
                            If Not IsNothing(cMainTipsManagerCfg) Then
                                ' cMainTipsManager.CleanStationTips(strStationErrorID)
                                If strTempValue = "" Then
                                    strTempValue = cLanguageManager.GetTextLine("ErrorAndMessageRunner", "Key", TempMachineStatus.intMessageID.ToString)
                                End If
                                strTempValue = cMainTipsManagerCfg.Text + vbCrLf + strTempValue
                            End If
                        End If

                        If strTempValue = "" Then
                            If TempMachineStatus.strMessage = "" Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(strStationName, cLanguageManager.GetTextLine("ErrorAndMessageRunner", "Key", TempMachineStatus.intMessageID.ToString), enumMainTipsManagerType.PLC))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(strStationName, TempMachineStatus.strMessage, enumMainTipsManagerType.PLC))
                            End If
                        Else
                            If bStationErrorMessage Then
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(strStationName, strTempValue, enumMainTipsManagerType.PLCAlarm))
                            Else
                                cMainTipsManager.AddTips(New clsMainTipsManagerCfg(strStationName, strTempValue, enumMainTipsManagerType.PLC))
                            End If

                        End If
                        iLastMessage = TempMachineStatus.intMessageID
                    End If
                    If TempMachineStatus.intMessageID = 0 And iLastMessage <> 0 Then
                        ' If cMainTipsManager.CurrentMainTipsManagerCfg.MainTipsManagerType = enumMainTipsManagerType.PLC Then
                        cMainTipsManager.CleanPLCTips(strStationName)
                        'End If
                        iLastMessage = 0
                    End If
                    i.StepInputNumber = i.Address_Home

                Case 100
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.StepOutputNumber + 1
                    End If

                Case 101
                    cRunActionCfg.ActionName = "AlarmDataManager.InSertData"
                    cRunActionCfg.Clean()
                    cRunActionCfg.AddParameter(TempMachineStatus.intErrorID.ToString)
                    cRunActionCfg.AddParameter(strTempValue)
                    cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cRunActionCfg.IsRunning = True
                    cRunActionCfg.Result = False
                    cThread = New Thread(AddressOf RunAction)
                    cThread.IsBackground = True
                    cThread.Start()

                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 102
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.Address_Home
                    End If

                Case 110
                    If Not cRunActionCfg.IsRunning Then
                        i.StepInputNumber = i.StepOutputNumber + 1
                    End If

                Case 111
                    cRunActionCfg.ActionName = "AlarmDataManager.UpdateData"
                    cRunActionCfg.Clean()
                    cRunActionCfg.AddParameter(iLastErrorCode.ToString)
                    cRunActionCfg.AddParameter(Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    cRunActionCfg.IsRunning = True
                    cRunActionCfg.Result = False
                    cThread = New Thread(AddressOf RunAction)
                    cThread.IsBackground = True
                    cThread.Start()

                    i.StepInputNumber = i.StepOutputNumber + 1

                Case 112
                    If Not cRunActionCfg.IsRunning Then
                        iLastErrorCode = 0
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
        If cRunActionCfg.ActionName = "AlarmDataManager.InSertData" Then
            cAlarmDataManager.InSertData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1), cRunActionCfg.GetParameter(2))
        End If
        If cRunActionCfg.ActionName = "AlarmDataManager.UpdateData" Then
            cAlarmDataManager.UpdateData(cRunActionCfg.GetParameter(0), cRunActionCfg.GetParameter(1))
        End If
        cRunActionCfg.Result = True
        cRunActionCfg.IsRunning = False
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
