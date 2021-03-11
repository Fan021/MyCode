Imports TwinCAT.Ads
Imports System.Windows.Forms
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports Kochi.HMI.MainControl.UI
Imports System.Runtime.InteropServices
Imports System.Collections.Concurrent
Imports System.Threading

Public Class clsTwincatInterface
    Protected WithEvents TcAds As TcAdsClient
    Protected lPLCVairablesHandles As New Dictionary(Of String, Integer)
    Protected notificationHandles As New Dictionary(Of String, Integer)
    Protected lDeviceNotificationEx As New Dictionary(Of String, Object)
    Protected lAdsCfg As New Dictionary(Of String, clsAdsCfg)
    Public Event AdsValueChanged(ByVal sender As Object, ByVal e As AdsValueChangedEvent)
    Private _Object As New Object
    Private _Object2 As New Object
    Private _Object3 As New Object
    Private cThread As Thread
    Private bExit As Boolean = False
    Private cLogHandler As clsLogHandler
    Private cSystemManager As clsSystemManager
    Private ePLCVersion As enumPLCVersion = enumPLCVersion.V1_01
    Private bHasHMI_ProgramButton As Boolean = True
    Private bHasHMI_ProgramCylinderButton As Boolean = True
    Public Function Init(ByVal cLocalElement As Dictionary(Of String, Object), ByVal cSystemElement As Dictionary(Of String, Object), ByVal strAmsID As String, ByVal strAmsPort As String) As Boolean
        Try
            cLogHandler = CType(cSystemElement(clsLogHandler.Name), clsLogHandler)
            cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
            TcAds = New TcAdsClient
            TcAds.Connect(strAmsID, CInt(strAmsPort))
            Dim cStateInfo As StateInfo = TcAds.ReadState
            AddAdsVariable(HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName + ".bulPowerON")
            Try
                AddAdsVariable(HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName + ".bulTeachMode")
            Catch ex As Exception
                ePLCVersion = enumPLCVersion.V1_00
            End Try

            If Not AddHMIAdsVariable(HMI_PLC_Interface.HMI_ProgramButton) Then
                bHasHMI_ProgramButton = False
            End If
            If Not AddHMIAdsVariable(HMI_PLC_Interface.HMI_ProgramCylinderButton) Then
                bHasHMI_ProgramCylinderButton = False
            End If

            AddNotificationEx(HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName, GetType(StructMachineStatus), New StructMachineStatus)
            cThread = New Thread(AddressOf Refresh)
            cThread.IsBackground = True
            cThread.Start()
            Return TcAds.IsConnected
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            Return False
        End Try
    End Function
    Public Function Quit(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        Try
            'For Each element As String In lDeviceNotificationEx.Keys
            '    If notificationHandles.ContainsKey(element) Then
            '        _TcAds.DeleteDeviceNotification(notificationHandles(element))
            '        notificationHandles.Remove(element)
            '    End If
            'Next
            bExit = True
            Dim iCnt As Integer = 50000
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
            TcAds.Disconnect()
            TcAds.Dispose()
            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            Return False
        End Try
    End Function


    Public Function AddAdsVariable(ByVal strVariableName As String) As Boolean
        SyncLock _Object
            Try
                Dim hVariable As Integer = -1
                If strVariableName.Trim.IndexOf(".") <> 0 Then
                    strVariableName = "." + strVariableName.Trim
                End If
                If lPLCVairablesHandles.ContainsKey(strVariableName.Trim) Then
                    '   Throw New clsHMIException(strVariableName.Trim + " have existed")
                    Return True
                End If
                hVariable = TcAds.CreateVariableHandle(strVariableName.Trim)
                lPLCVairablesHandles.Add(strVariableName.Trim, hVariable)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message + "--" + strVariableName, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Function AddHMIAdsVariable(ByVal strVariableName As String) As Boolean
        SyncLock _Object
            Try
                Dim hVariable As Integer = -1
                If strVariableName.Trim.IndexOf(".") <> 0 Then
                    strVariableName = "." + strVariableName.Trim
                End If
                If lPLCVairablesHandles.ContainsKey(strVariableName.Trim) Then
                    '   Throw New clsHMIException(strVariableName.Trim + " have existed")
                    Return True
                End If
                hVariable = TcAds.CreateVariableHandle(strVariableName.Trim)
                lPLCVairablesHandles.Add(strVariableName.Trim, hVariable)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ReadAny(ByVal strName As String, ByVal Type As Type, Optional ByVal args() As Integer = Nothing) As Object

        SyncLock _Object
            Try
                Dim oObject As Object

                If strName.Trim.IndexOf(".") <> 0 Then
                    strName = "." + strName.Trim
                End If

                If TcAds Is Nothing Then
                    Throw New clsHMIException("TcAds is Nothing")
                End If
                If bExit Then
                    If lDeviceNotificationEx.ContainsKey(strName) Then
                        Return lDeviceNotificationEx(strName)
                    End If
                End If
                If Not TcAds.IsConnected Then
                    If lDeviceNotificationEx.ContainsKey(strName) Then
                        Return lDeviceNotificationEx(strName)
                    End If
                End If

                If ePLCVersion = enumPLCVersion.V1_00 Then
                    If Type = GetType(StructMachineStatus) Then
                        Type = GetType(StructMachineStatus_V1_00)
                    End If
                    If strName = "." + HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName + ".bulTeachMode" Then
                        strName = "." + HMI_PLC_Interface.HMI_PLC_MachineStatus_AdsName + ".bulDebugMode"
                    End If
                End If

                If strName = ".HMI_ProgramButton" And Not bHasHMI_ProgramButton Then
                    Return lAdsCfg(strName).cObjectValue
                ElseIf strName.IndexOf(".HMI_ProgramButton") >= 0 And Not bHasHMI_ProgramButton Then

                End If

                If strName = ".HMI_ProgramCylinderButton" And Not bHasHMI_ProgramCylinderButton Then
                    Return lAdsCfg(strName).cObjectValue
                ElseIf strName.IndexOf(".HMI_ProgramCylinderButton") >= 0 And Not bHasHMI_ProgramCylinderButton Then
                    Return True
                End If

                If Not lPLCVairablesHandles.ContainsKey(strName) Then
                    AddAdsVariable(strName)
                End If

                If strName.IndexOf(HMI_PLC_Interface.PLC_AutoActionStep) >= 0 Then
                    Dim a As String = ""
                End If

                oObject = TcAds.ReadAny(lPLCVairablesHandles(strName), Type, args)

                If ePLCVersion = enumPLCVersion.V1_00 Then
                    Dim cMachineStatus As New StructMachineStatus
                    If oObject.GetType = GetType(StructMachineStatus_V1_00) Then
                        cMachineStatus.bulAlarm = CType(oObject, StructMachineStatus_V1_00).bulAlarm
                        cMachineStatus.bulAuto = CType(oObject, StructMachineStatus_V1_00).bulAuto
                        cMachineStatus.bulCleanMode = CType(oObject, StructMachineStatus_V1_00).bulCleanMode
                        cMachineStatus.bulDebugMode = CType(oObject, StructMachineStatus_V1_00).bulDebugMode
                        cMachineStatus.bulDoorOpend = CType(oObject, StructMachineStatus_V1_00).bulDoorOpend
                        cMachineStatus.bulEmergence = CType(oObject, StructMachineStatus_V1_00).bulEmergence
                        cMachineStatus.bulManual = CType(oObject, StructMachineStatus_V1_00).bulManual
                        cMachineStatus.bulPowerON = CType(oObject, StructMachineStatus_V1_00).bulPowerON
                        cMachineStatus.bulReset = CType(oObject, StructMachineStatus_V1_00).bulReset
                        cMachineStatus.bulStepBackward = CType(oObject, StructMachineStatus_V1_00).bulStepBackward
                        cMachineStatus.bulStepForward = CType(oObject, StructMachineStatus_V1_00).bulStepForward
                        cMachineStatus.bulTeachMode = False
                        cMachineStatus.bulWork = CType(oObject, StructMachineStatus_V1_00).bulWork
                        cMachineStatus.intErrorID = CType(oObject, StructMachineStatus_V1_00).intErrorID
                        cMachineStatus.intMessageID = CType(oObject, StructMachineStatus_V1_00).intMessageID
                        cMachineStatus.strErrorMessage = CType(oObject, StructMachineStatus_V1_00).strErrorMessage
                        cMachineStatus.strMessage = CType(oObject, StructMachineStatus_V1_00).strMessage
                        Return cMachineStatus
                    End If
                End If
                Return oObject
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            End Try
        End SyncLock
    End Function

    Public Function WriteAny(ByVal strName As String, ByVal oValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
        SyncLock _Object
            Try
                If TcAds Is Nothing Then
                    Throw New clsHMIException("TcAds is Nothing")
                End If

                If Not TcAds.IsConnected Then
                    Throw New clsHMIException("TcAds is Disconnect")
                End If

                If strName.Trim.IndexOf(".") <> 0 Then
                    strName = "." + strName.Trim
                End If

                If ePLCVersion = enumPLCVersion.V1_00 Then

                    If strName = "." + HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulTeachMode" Then
                        strName = "." + HMI_PLC_Interface.HMI_AutoMainFunction_AdsName + ".bulDebugMode"
                    End If

                End If

                If strName.IndexOf(".HMI_ProgramButton") >= 0 And Not bHasHMI_ProgramButton Then
                    Return True
                End If

                If strName.IndexOf(".HMI_ProgramCylinderButton") >= 0 And Not bHasHMI_ProgramCylinderButton Then
                    Return True
                End If

                If Not lPLCVairablesHandles.ContainsKey(strName) Then
                    AddAdsVariable(strName)
                End If
                TcAds.WriteAny(lPLCVairablesHandles(strName), oValue, args)
                cLogHandler.SaveLogger(cSystemManager.Settings.LogFolder, "WriteAny:" + strName + " Value:" + oValue.ToString)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            End Try
        End SyncLock
    End Function

    Public Function AddNotificationEx(ByVal strName As String, ByVal ObjectType As Type, ByVal ObjectDefauleValue As Object, Optional ByVal args() As Integer = Nothing) As Boolean
        SyncLock _Object3
            Try

                ' Dim iHandel As Integer
                If strName = "" Then
                    Return True
                End If
                If strName.Trim.IndexOf(".") <> 0 Then
                    strName = "." + strName.Trim
                End If
                'If Not notificationHandles.ContainsKey(strName) Then
                '    iHandel = TcAds.AddDeviceNotificationEx(strName, AdsTransMode.OnChange, 100, 10, strName, ObjectType, args)
                '    notificationHandles.Add(strName, iHandel)
                '    lDeviceNotificationEx.Add(strName, ObjectDefauleValue)
                'End If
                If Not lAdsCfg.ContainsKey(strName) Then
                    If IsNothing(args) Then
                        lAdsCfg.Add(strName, New clsAdsCfg(strName, ObjectType, 0, ObjectDefauleValue))
                    Else
                        lAdsCfg.Add(strName, New clsAdsCfg(strName, ObjectType, args(0), ObjectDefauleValue))
                    End If
                    lDeviceNotificationEx.Add(strName, ObjectDefauleValue)
                End If
                Return True

            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Alarm)
            End Try
        End SyncLock
    End Function

    Public Function RemoveNotificationEx(ByVal strName As String) As Boolean
        SyncLock _Object3
            Try
                If strName.Trim.IndexOf(".") <> 0 Then
                    strName = "." + strName.Trim
                End If
                'If notificationHandles.ContainsKey(strName) Then
                '    _TcAds.DeleteDeviceNotification(notificationHandles(strName))
                '    notificationHandles.Remove(strName)
                '    lDeviceNotificationEx.Remove(strName)
                'End If
                If Not lAdsCfg.ContainsKey(strName) Then
                    Return True
                End If

                If lAdsCfg.ContainsKey(strName) Then
                    lAdsCfg.Remove(strName)
                    lDeviceNotificationEx.Remove(strName)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Private Sub Refresh()
        While Not bExit
            Try

                SyncLock _Object3
                    For Each element As clsAdsCfg In lAdsCfg.Values
                        If bExit Then
                            Return
                        End If
                        If IsNothing(element.args) Then
                            Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType)
                            SyncLock _Object2
                                lDeviceNotificationEx(element.Name) = ObjectValue
                            End SyncLock
                        End If

                        If Not IsNothing(element.args) Then
                            If element.args = 0 Then
                                Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType)
                                SyncLock _Object2
                                    lDeviceNotificationEx(element.Name) = ObjectValue
                                End SyncLock
                            Else

                                Dim ObjectValue As Object = ReadAny(element.Name, element.ObjectType, New Integer() {element.args})
                                SyncLock _Object2
                                    lDeviceNotificationEx(element.Name) = ObjectValue
                                End SyncLock
                            End If
                        End If
                    Next
                End SyncLock
                Thread.Sleep(20)
            Catch ex As Exception
                If bExit Then
                    Return
                End If
                Thread.Sleep(20)
            End Try
        End While

    End Sub

    Private Sub adsClient_AdsNotificationEx(ByVal sender As Object, ByVal e As AdsNotificationExEventArgs) Handles TcAds.AdsNotificationEx
        SyncLock _Object
            Try
                Dim ObjectValue As New Object
                If lDeviceNotificationEx.ContainsKey(CType(e.UserData, String)) Then
                    lDeviceNotificationEx(CType(e.UserData, String)) = e.Value
                End If
                RaiseEvent AdsValueChanged(sender, New AdsValueChangedEvent(e.UserData, e.Value))

            Catch ex As Exception
                Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Function GetValue(ByVal strName As String) As Object
        SyncLock _Object2
            If strName.Trim.IndexOf(".") <> 0 Then
                strName = "." + strName.Trim
            End If
            If lDeviceNotificationEx.ContainsKey(strName) Then
                Return lDeviceNotificationEx(strName)
            Else
                Throw New clsHMIException("lDeviceNotificationEx not ContainsKey:" + strName, enumExceptionType.Crash)
            End If
            Return Nothing
        End SyncLock
    End Function
End Class

Public Class clsAdsCfg
    Private strName As String
    Private cObjectType As Type
    Private cargs As Integer
    Public cObjectValue As Object

    Public Property Name As String
        Set(ByVal value As String)
            strName = value
        End Set
        Get
            Return strName
        End Get
    End Property
    Public Property ObjectType As Type
        Set(ByVal value As Type)
            cObjectType = value
        End Set
        Get
            Return cObjectType
        End Get
    End Property
    Public Property args As Integer
        Set(ByVal value As Integer)
            cargs = value
        End Set
        Get
            Return cargs
        End Get
    End Property
    Sub New(ByVal strName As String, ByVal cObjectType As Type, ByVal cargs As Integer, ByVal cObjectValue As Object)
        Me.strName = strName
        Me.cObjectType = cObjectType
        Me.cargs = cargs
        Me.cObjectValue = cObjectValue
    End Sub
End Class

Public Enum enumPLCVersion
    V1_00 = 1
    V1_01
End Enum