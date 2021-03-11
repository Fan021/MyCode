Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class clsDeviceManager
    Private lDevicesList As New List(Of clsDeviceCfg)
    Private lCurrentDevicesList As New List(Of clsDeviceCfg)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceLibManger As clsDeviceLibManager
    Private cMachineManager As clsMachineManager
    Private bDeviceError As Boolean
    Private bPLCDeviceError As Boolean
    Private bDeviceChange As Boolean
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager
    Public Const Name As String = "DeviceManager"
    Private lListStation As New Dictionary(Of String, List(Of clsDeviceCfg))

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal(lDevicesList, lCurrentDevicesList)
            End SyncLock
        End Get
    End Property

    Public Property DeviceChange As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bDeviceChange = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bDeviceChange
            End SyncLock
        End Get
    End Property

    Public Property DeviceError As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bDeviceError = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bDeviceError
            End SyncLock
        End Get
    End Property

    Public Property PLCDeviceError As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bPLCDeviceError = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bPLCDeviceError
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cDeviceLibManger = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetDevicesListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lDevicesList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetDeviceCfgFromKey(ByVal iKey As Integer) As clsDeviceCfg
        SyncLock _Object
            Try
                If iKey <= lDevicesList.Count - 1 Then
                    Return lDevicesList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentDevicesListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lCurrentDevicesList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentDeviceCfgFromKey(ByVal iKey As Integer) As clsDeviceCfg
        SyncLock _Object
            Try
                If iKey <= lCurrentDevicesList.Count - 1 Then
                    Return lCurrentDevicesList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddCurrentDevice() As clsDeviceCfg
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                Dim mTempName As String
                mTempName = "Device" + iCnt.ToString
                Do While lCurrentDevicesList.Any(Function(e) e.Name = mTempName)
                    iCnt = iCnt + 1
                    mTempName = "Device" + iCnt.ToString
                Loop
                Dim cDeviceCfg As New clsDeviceCfg((lCurrentDevicesList.Count + 1).ToString, mTempName)
                lCurrentDevicesList.Add(cDeviceCfg)
                ChangeID()
                Return cDeviceCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentDeviceFromName(ByVal strName As String) As clsDeviceCfg
        SyncLock _Object
            Try
                Return lCurrentDevicesList.Where(Function(e) e.Name = strName).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetDeviceFromName(ByVal lDevicesList As List(Of clsDeviceCfg), ByVal strName As String) As clsDeviceCfg
        SyncLock _Object
            Try
                If Not lDevicesList.Any(Function(e) e.Name = strName) Then
                    Return Nothing
                End If
                Return lDevicesList.Where(Function(e) e.Name = strName).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetDeviceFromName(ByVal strName As String) As clsDeviceCfg
        SyncLock _Object
            Try
                If Not lDevicesList.Any(Function(e) e.Name = strName) Then
                    Return Nothing
                End If

                Return lDevicesList.Where(Function(e) e.Name = strName).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetDeviceFromType(ByVal ParamArray DeviceType() As Type) As List(Of clsDeviceCfg)
        SyncLock _Object
            Try
                Dim lListDeviceCfg As New List(Of clsDeviceCfg)
                For Each element As clsDeviceCfg In lDevicesList
                    For i = 0 To DeviceType.Count - 1
                        If element.Source.GetType = DeviceType(i) Or element.Source.GetType.IsSubclassOf(DeviceType(i)) Then
                            If Not lListDeviceCfg.Contains(element) Then
                                lListDeviceCfg.Add(element)
                            End If
                        End If
                    Next
                Next
                Return lListDeviceCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetDeviceFromTypeAndStationID(ByVal iStationID As Integer, ByVal ParamArray DeviceType() As Type) As List(Of clsDeviceCfg)
        SyncLock _Object
            Try
                Dim lListDeviceCfg As New List(Of clsDeviceCfg)

                For Each element As clsDeviceCfg In lDevicesList
                    For i = 0 To DeviceType.Count - 1
                        If element.StationID = iStationID And (element.Source.GetType = DeviceType(i) Or element.Source.GetType.IsSubclassOf(DeviceType(i))) Then
                            If Not lListDeviceCfg.Contains(element) Then
                                lListDeviceCfg.Add(element)
                            End If
                        End If
                    Next
                Next
                Return lListDeviceCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetDeviceFromTypeAndStationIndex(ByVal strStationID As String, ByVal strIndex As String, ByVal ParamArray DeviceType() As Type) As clsDeviceCfg
        SyncLock _Object
            Try
                Dim iStationID As Integer = 0
                If strStationID = "" Then
                    Return Nothing
                End If

                If Not IsNumeric(strStationID) Then
                    Return Nothing
                End If
                iStationID = CInt(strStationID)
                Dim iIndex As Integer = 0
                If strIndex = "" Then
                    Return Nothing
                End If

                If Not IsNumeric(strIndex) Then
                    Return Nothing
                End If
                iIndex = CInt(strIndex)

                For Each element As clsDeviceCfg In lDevicesList
                    For i = 0 To DeviceType.Count - 1
                        If element.StationIndex = iIndex And element.StationID = iStationID And (element.Source.GetType = DeviceType(i) Or element.Source.GetType.IsSubclassOf(DeviceType(i))) Then
                            Return element
                        End If
                    Next
                Next
                Return Nothing



            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function HasDeviceFromType(ByVal strDeviceType As Type) As Boolean
        SyncLock _Object
            Try
                Dim lListDeviceCfg As New List(Of clsDeviceCfg)
                For Each element As clsDeviceCfg In lDevicesList
                    If element.Source.GetType = strDeviceType Or element.Source.GetType.IsSubclassOf(strDeviceType) Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetPLCDevice() As clsHMIPLC
        SyncLock _Object
            Try
                If Not lDevicesList.Any(Function(e) TypeOf e.Source Is clsHMIPLC) Then
                    '  Throw New clsHMIException("Please Add PLC Device First.", enumExceptionType.Alarm)
                    Return Nothing
                End If
                Return lDevicesList.Where(Function(e) TypeOf e.Source Is clsHMIPLC).First().Source
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetCurrentDeviceFromID(ByVal strID As String) As clsDeviceCfg
        SyncLock _Object
            Try
                Return lCurrentDevicesList.Where(Function(e) e.ID = strID).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function HasCurrentDevice(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If lCurrentDevicesList.Any(Function(e) e.Name = strName) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasCurrentDevice(ByVal strName As String, ByVal iIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If lCurrentDevicesList.Any(Function(e) e.Name = strName And e.ID <> iIndex + 1) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentDeviceName(ByVal strID As String, ByVal strModifyValue As String) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.ID = strID Then
                        DeviceElement.Name = strModifyValue
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentStationID(ByVal strID As String, ByVal iModifyValue As Integer) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.ID = strID Then
                        DeviceElement.StationID = iModifyValue
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeCurrentDeviceType(ByVal strID As String, ByVal strDeviceType As String) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.ID = strID Then
                        DeviceElement.DeviceType = strDeviceType
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentSource(ByVal strID As String, ByVal oSource As Object) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.ID = strID Then
                        DeviceElement.Source = oSource
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentParameter(ByVal strID As String, ByVal strParameter As String) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.ID = strID Then
                        DeviceElement.InitParameter = strParameter
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveControlParameter(ByVal strName As String, ByVal strParameter As String) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.Name = strName Then
                        DeviceElement.ControlParameter = strParameter
                    End If
                Next
                For Each DeviceElement As clsDeviceCfg In lDevicesList
                    If DeviceElement.Name = strName Then
                        DeviceElement.ControlParameter = strParameter
                        cIniHandler.WriteIniFile(cSystemManager.Settings.DevicesConfig, "Device" + DeviceElement.ID, "ControlParameter", DeviceElement.ControlParameter)
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DeleteCurrentDevice(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                lCurrentDevicesList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpDeviceByIndex(ByVal iIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iIndex <= 0 Then Return True

                Dim TopTempDeviceCfg As clsDeviceCfg = lCurrentDevicesList(iIndex - 1).Clone
                lCurrentDevicesList(iIndex - 1) = lCurrentDevicesList(iIndex).Clone
                lCurrentDevicesList(iIndex) = TopTempDeviceCfg
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DownDeviceByIndex(ByVal iIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iIndex >= lCurrentDevicesList.Count - 1 Then Return True
                Dim TopTempDeviceCfg As clsDeviceCfg = lCurrentDevicesList(iIndex + 1).Clone
                lCurrentDevicesList(iIndex + 1) = lCurrentDevicesList(iIndex).Clone
                lCurrentDevicesList(iIndex) = TopTempDeviceCfg
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetStationUpMaxIndex(ByVal strStationID As String) As Integer
        Try
            Dim iCnt As Integer = 0
            For Each element As String In lListStation.Keys
                If element = strStationID Then
                    Return iCnt
                End If
                iCnt = iCnt + lListStation(element).Count
            Next
            Return iCnt
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetStationDownMaxIndex(ByVal strStationID As String) As Integer
        Try
            Dim iCnt As Integer = 0
            For Each element As String In lListStation.Keys
                iCnt = iCnt + lListStation(element).Count
                If element = strStationID Then
                    Return iCnt
                End If
            Next
            Return iCnt
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                Dim j As Integer = 1
                Dim lListIndex As New Dictionary(Of String, Integer)
                Dim lListStationIndex As New Dictionary(Of String, Integer)
                lListStation.Clear()
                Dim lListDevice As New List(Of clsDeviceCfg)

                For Each elementindex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementindex)
                    lListDevice = New List(Of clsDeviceCfg)
                    lListStation.Add(element.ID, lListDevice)
                Next
                lListDevice = New List(Of clsDeviceCfg)
                lListStation.Add("0", lListDevice)
                Dim strDeviceType As String = ""
                For i = 0 To lCurrentDevicesList.Count - 1
                    If Not lListStation.ContainsKey(lCurrentDevicesList(i).StationID.ToString) Then Continue For
                    lListStation(lCurrentDevicesList(i).StationID.ToString).Add(lCurrentDevicesList(i))
                Next
                'lCurrentDevicesList.Clear()
                'For Each element As List(Of clsDeviceCfg) In lListStation.Values
                '    For Each sudelement As clsDeviceCfg In element
                '        lCurrentDevicesList.Add(sudelement.Clone)
                '    Next
                'Next
                j = 1
                For i = 0 To lCurrentDevicesList.Count - 1
                    lCurrentDevicesList(i).ID = j.ToString
                    strDeviceType = lCurrentDevicesList(i).DeviceType
                    If strDeviceType <> "" Then
                        strDeviceType = cDeviceLibManger.GetDeviceLibCfgFromKey(strDeviceType).DeviceType
                        If lListIndex.ContainsKey(strDeviceType) Then
                            lListIndex(strDeviceType) = lListIndex(strDeviceType) + 1
                        Else
                            lListIndex.Add(strDeviceType, 1)
                        End If
                        ' lCurrentDevicesList(i).Index = lListIndex(strDeviceType)
                        If lListStationIndex.ContainsKey(lCurrentDevicesList(i).StationID.ToString + "_" + strDeviceType) Then
                            lListStationIndex(lCurrentDevicesList(i).StationID.ToString + "_" + strDeviceType) = lListStationIndex(lCurrentDevicesList(i).StationID.ToString + "_" + strDeviceType) + 1
                        Else
                            lListStationIndex.Add(lCurrentDevicesList(i).StationID.ToString + "_" + strDeviceType, 1)
                        End If
                        lCurrentDevicesList(i).StationIndex = lListStationIndex(lCurrentDevicesList(i).StationID.ToString + "_" + strDeviceType)
                    End If
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Public Function LoadDeviceCfg() As Boolean
        SyncLock _Object
            Try
                Dim cDeviceElement As clsDeviceCfg
                lDevicesList.Clear()
                lCurrentDevicesList.Clear()
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.DevicesConfig, "Device", New String() {"ID", "Name", "Type", "StationID", "Index", "StationIndex", "InitParameter", "ControlParameter"})

                    cDeviceElement = New clsDeviceCfg
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("Name"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Name"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.Name = CType(element("Name"), String)
                    End If
                    If CType(element("Type"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Type"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.DeviceType = CType(element("Type"), String)
                    End If
                    If CType(element("StationID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("StationID"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.StationID = CType(element("StationID"), Integer)
                    Else
                        cDeviceElement.StationID = 1
                    End If
                    cDeviceElement.StationID = ChangeStationIndexToID(cDeviceElement.StationID)
                    'If CType(element("Index"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Index"), String) <> clsXmlHandler.s_Null Then
                    '    cDeviceElement.Index = CType(element("Index"), String)
                    'End If
                    If CType(element("StationIndex"), String) <> clsXmlHandler.s_DEFAULT And CType(element("StationIndex"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.StationIndex = CType(element("StationIndex"), String)
                    End If
                    If CType(element("InitParameter"), String) <> clsXmlHandler.s_DEFAULT And CType(element("InitParameter"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.InitParameter = CType(element("InitParameter"), String)
                    End If
                    If CType(element("ControlParameter"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ControlParameter"), String) <> clsXmlHandler.s_Null Then
                        cDeviceElement.ControlParameter = CType(element("ControlParameter"), String)
                    End If
                    cDeviceElement.Source = cDeviceLibManger.GetDeviceLibCfgFromKey(cDeviceElement.DeviceType).CreateInstance
                    cDeviceElement.Source.Name = cDeviceElement.Name
                    lCurrentDevicesList.Add(cDeviceElement)
                Next
                ChangeID()
                ChangeIniToParameter(Nothing)
                For Each element As clsDeviceCfg In lCurrentDevicesList
                    lDevicesList.Add(element.Clone)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Function ChangeStationIndexToID(ByVal iIndex As Integer) As String
        If iIndex = 0 Then Return iIndex.ToString
        If cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count = 0 Then Return 0

        For Each elementindex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementindex)   
            If element.Index = iIndex Then
                Return element.ID
            End If
        Next
        Return iIndex.ToString
    End Function


    Private Function ChangeStationIDToIndex(ByVal iIndex As Integer) As String
        If iIndex = 0 Then Return iIndex.ToString
        For Each elementindex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
            Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementindex)
            If element.ID = iIndex Then
                Return element.Index
            End If
        Next
        Return iIndex.ToString
    End Function

    Public Function SaveCurrentDeviceCfg() As Boolean
        SyncLock _Object
            Try
                Dim cMainRunner As Object = cSystemElement("MainRunner")
                If Not cMainRunner.CloseDeives() Then
                    Return False
                End If
                If Not cMainRunner.ClosePLC() Then
                    Return False
                End If
                ChangeParameterToIni(Nothing)
                cIniHandler.RemoveAllSection(cSystemManager.Settings.DevicesConfig, "Device")
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    cIniHandler.SetAnyListToIni(cSystemManager.Settings.DevicesConfig, "Device" + DeviceElement.ID, New String() {"ID", "Name", "Type", "StationID", "StationIndex", "InitParameter", "ControlParameter"},
                                          New String() {DeviceElement.ID, DeviceElement.Name, DeviceElement.DeviceType, ChangeStationIDToIndex(DeviceElement.StationID.ToString), DeviceElement.StationIndex.ToString, DeviceElement.InitParameter, DeviceElement.ControlParameter})
                Next
                LoadDeviceCfg()

                bDeviceChange = True
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeParameterToIni(ByVal cLocalElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If IsNothing(CType(DeviceElement.Source, clsHMIDeviceBase).InitUI) Then
                        CType(DeviceElement.Source, clsHMIDeviceBase).CreateInitUI(cLocalElement, cSystemElement)
                    End If
                    Dim lListParameter As New List(Of String)
                    If Not CType(DeviceElement.Source, clsHMIDeviceBase).InitUI.ChangeParameterToIni(cLocalElement, cSystemElement, clsParameter.ToList(DeviceElement.InitParameter), lListParameter) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "8", DeviceElement.ID), enumExceptionType.Crash)
                    End If
                    DeviceElement.InitParameter = clsParameter.ToString(lListParameter)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeIniToParameter(ByVal cLocalElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If IsNothing(CType(DeviceElement.Source, clsHMIDeviceBase).InitUI) Then
                        CType(DeviceElement.Source, clsHMIDeviceBase).CreateInitUI(cLocalElement, cSystemElement)
                    End If
                    Dim lListParameter As New List(Of String)
                    If Not CType(DeviceElement.Source, clsHMIDeviceBase).InitUI.ChangeIniToParameter(cLocalElement, cSystemElement, clsParameter.ToList(DeviceElement.InitParameter), lListParameter) Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "9", DeviceElement.ID), enumExceptionType.Crash)
                    End If
                    DeviceElement.InitParameter = clsParameter.ToString(lListParameter)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentDeviceCfg(ByVal cLocalElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                For Each DeviceElement As clsDeviceCfg In lCurrentDevicesList
                    If DeviceElement.Name = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "1", DeviceElement.ID), enumExceptionType.Alarm)
                    End If
                    If DeviceElement.DeviceType = "" Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "2", DeviceElement.ID), enumExceptionType.Alarm)
                    End If
                    If DeviceElement.StationID < 0 Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "7", DeviceElement.ID), enumExceptionType.Alarm)
                    End If

                    'If DeviceElement.Index <= 0 Then
                    '    Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "5", DeviceElement.ID), enumExceptionType.Alarm)
                    'End If

                    If DeviceElement.StationIndex <= 0 Then
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "6", DeviceElement.ID), enumExceptionType.Alarm)
                    End If
                    If IsNothing(CType(DeviceElement.Source, clsHMIDeviceBase).InitUI) Then
                        CType(DeviceElement.Source, clsHMIDeviceBase).CreateInitUI(cLocalElement, cSystemElement)
                    End If
                    Try
                        If Not CType(DeviceElement.Source, clsHMIDeviceBase).InitUI.CheckParameter(cLocalElement, cSystemElement, clsParameter.ToList(DeviceElement.InitParameter)) Then
                            Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "3", DeviceElement.ID), enumExceptionType.Alarm)
                        End If
                    Catch ex1 As Exception
                        Throw New clsHMIException(cLanguageManager.GetTextLine("DeviceManager", "4", DeviceElement.ID, ex1.Message), enumExceptionType.Alarm)
                    End Try
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CancelCurrentDeviceCfg() As Boolean
        SyncLock _Object
            Try
                Clone(lDevicesList, lCurrentDevicesList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function Equal(ByVal SrcList As List(Of clsDeviceCfg), ByVal TarList As List(Of clsDeviceCfg)) As Boolean
        SyncLock _Object
            Try
                Dim element As clsDeviceCfg
                If SrcList.Count <> TarList.Count Then
                    Return False
                End If
                For Each element In TarList
                    If Not SrcList.Any(Function(e) e.Name = element.Name) Then
                        Return False
                    End If

                    If GetDeviceFromName(SrcList, element.Name) <> element Then
                        Return False
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function Clone(ByRef SrcList As List(Of clsDeviceCfg), ByRef TarList As List(Of clsDeviceCfg)) As Boolean
        SyncLock _Object
            Try
                TarList.Clear()
                For Each element In SrcList
                    TarList.Add(element.Clone)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


End Class


Public Class clsDeviceCfg
    Private strID As String = String.Empty
    Private strName As String = String.Empty
    Private iStationID As Integer = 0
    Private strStationIndex As Integer = 0
    Private strInitParameter As String = String.Empty
    Private strControlParameter As String = String.Empty
    Private strDeviceType As String = String.Empty
    Private oSource As Object = Nothing
    Private _Object As New Object
    Private Shared _Object2 As New Object

    Public Property ID As String
        Set(ByVal value As String)
            SyncLock _Object
                strID = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strID
            End SyncLock
        End Get
    End Property

    Public Property StationID As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iStationID = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iStationID
            End SyncLock
        End Get
    End Property


    Public Property StationIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                strStationIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strStationIndex
            End SyncLock
        End Get
    End Property

    Public Property Name As String
        Set(ByVal value As String)
            SyncLock _Object
                strName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strName
            End SyncLock
        End Get
    End Property

    Public Property InitParameter As String
        Set(ByVal value As String)
            SyncLock _Object
                strInitParameter = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strInitParameter
            End SyncLock
        End Get
    End Property

    Public Property ControlParameter As String
        Set(ByVal value As String)
            SyncLock _Object
                strControlParameter = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strControlParameter
            End SyncLock
        End Get
    End Property

    Public Property DeviceType As String
        Set(ByVal value As String)
            SyncLock _Object
                strDeviceType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strDeviceType
            End SyncLock
        End Get
    End Property


    Public Property Source As Object
        Set(ByVal value As Object)
            SyncLock _Object
                oSource = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return oSource
            End SyncLock
        End Get
    End Property

    Sub New()
        SyncLock _Object

        End SyncLock
    End Sub
    Sub New(ByVal strName As String)
        SyncLock _Object
            Me.strName = strName
        End SyncLock
    End Sub
    Sub New(ByVal strID As String, ByVal strName As String)
        SyncLock _Object
            Me.strID = strID
            Me.strName = strName
        End SyncLock
    End Sub

    Public Shared Operator <>(ByVal x As clsDeviceCfg, ByVal y As clsDeviceCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ID <> y.ID Or x.Name <> y.Name Or x.DeviceType <> y.DeviceType Or x.InitParameter <> y.InitParameter Or x.ControlParameter <> y.ControlParameter Or x.StationIndex <> y.StationIndex Or x.StationID <> y.StationID
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsDeviceCfg, ByVal y As clsDeviceCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            Return x.ID = y.ID And x.Name = y.Name And x.DeviceType = y.DeviceType And x.InitParameter = y.InitParameter And x.ControlParameter = y.ControlParameter And x.StationIndex = y.StationIndex And x.StationID = y.StationID
        End SyncLock
    End Operator

    Public Function Clone() As clsDeviceCfg
        SyncLock _Object
            Dim cTemp As New clsDeviceCfg
            cTemp.ID = ID
            cTemp.DeviceType = DeviceType
            cTemp.Name = Name
            cTemp.InitParameter = InitParameter
            cTemp.ControlParameter = ControlParameter
            cTemp.Source = Source
            cTemp.StationIndex = StationIndex
            cTemp.StationID = StationID
            Return cTemp
        End SyncLock
    End Function

End Class


