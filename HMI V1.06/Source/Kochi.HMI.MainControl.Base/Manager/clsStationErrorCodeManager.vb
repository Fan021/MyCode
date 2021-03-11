Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Public Class clsStationErrorCodeManager
    Public Const Name As String = "StationErrorCodeManager"
    Private cSystemManager As clsSystemManager
    Private cLanguageManager As clsLanguageManager
    Private cMachineManager As clsMachineManager
    Private cDeviceManager As clsDeviceManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private lListStationError As New Dictionary(Of String, clsStationErrorCfg)
    Private lListStationCarrierError As New Dictionary(Of String, clsStationCarrierErrorCfg)
    Private lListCarrierError As New Dictionary(Of String, Dictionary(Of Integer, clsCarrierErrorCfg))
    Private cIniHandler As New clsIniHandler
    Private cDataGridViewPage_Station As clsDataGridViewPage
    Private cHMIDataView_Station As HMIDataView
    Private cDataGridViewPage_Carrier As clsDataGridViewPage
    Private cHMIDataView_Carrier As HMIDataView
    Private cDataGridViewPage_StationCarrier As clsDataGridViewPage
    Private cHMIDataView_StationCarrier As HMIDataView
    Private cHMIPLC As clsHMIPLC

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
                LoadIni()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager_Station(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean

        SyncLock _Object
            Me.cDataGridViewPage_Station = cDataGridViewPage
            Me.cHMIDataView_Station = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function RegisterManager_Carrier(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean

        SyncLock _Object
            Me.cDataGridViewPage_Carrier = cDataGridViewPage
            Me.cHMIDataView_Carrier = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function RegisterManager_StationCarrier(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean

        SyncLock _Object
            Me.cDataGridViewPage_StationCarrier = cDataGridViewPage
            Me.cHMIDataView_StationCarrier = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function LoadIni() As Boolean
        SyncLock _Object
            Try
                Dim strTempValue As String = String.Empty
                Me.cSystemElement = cSystemElement
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                lListStationError.Clear()
                lListStationCarrierError.Clear()
                lListCarrierError.Clear()
                For Each element In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(element)
                    Dim cStationErrorCfg As New clsStationErrorCfg
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + cMachineStationCfg.ID.ToString, "Enable")
                    cStationErrorCfg.Enable = IIf(strTempValue = "TRUE", True, False)

                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + cMachineStationCfg.ID.ToString, "ExpectNum")
                    If strTempValue <> "" Then
                        cStationErrorCfg.ExpectNum = CInt(strTempValue)
                    Else
                        cStationErrorCfg.ExpectNum = 0
                    End If

                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + cMachineStationCfg.ID.ToString, "ErrorCode")
                    cStationErrorCfg.ErrorCode = strTempValue
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + cMachineStationCfg.ID.ToString, "CurrentErrorType")
                    cStationErrorCfg.CurrentErrorType = strTempValue

                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + cMachineStationCfg.ID.ToString, "CurrentNum")
                    If strTempValue <> "" Then
                        cStationErrorCfg.CurrentNum = CInt(strTempValue)
                    Else
                        cStationErrorCfg.CurrentNum = 0
                    End If
                    lListStationError.Add(cMachineStationCfg.ID.ToString, cStationErrorCfg)

                    Dim cStationCarrierErrorCfg As New clsStationCarrierErrorCfg
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "StationCarrier" + cMachineStationCfg.ID.ToString, "Enable")
                    cStationCarrierErrorCfg.Enable = IIf(strTempValue = "TRUE", True, False)

                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "StationCarrier" + cMachineStationCfg.ID.ToString, "ExpectNum")
                    If strTempValue <> "" Then
                        cStationCarrierErrorCfg.ExpectNum = CInt(strTempValue)
                    Else
                        cStationCarrierErrorCfg.ExpectNum = 0
                    End If
                    strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "StationCarrier" + cMachineStationCfg.ID.ToString, "ErrorCode")
                    cStationCarrierErrorCfg.ErrorCode = strTempValue

                    lListStationCarrierError.Add(cMachineStationCfg.ID.ToString, cStationCarrierErrorCfg)
                Next

                For i = 1 To cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count
                    Dim lListCarrier As New Dictionary(Of Integer, clsCarrierErrorCfg)
                    For j = 0 To 100
                        Dim cCarrierErrorCfg As New clsCarrierErrorCfg
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + i.ToString + "Carrier" + j.ToString, "CurrentNum")
                        If strTempValue = "" Then Continue For
                        cCarrierErrorCfg.Enable = lListStationCarrierError(i.ToString).Enable
                        cCarrierErrorCfg.ExpectNum = lListStationCarrierError(i.ToString).ExpectNum
                        cCarrierErrorCfg.ErrorCode = lListStationCarrierError(i.ToString).ErrorCode
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + i.ToString + "Carrier" + j.ToString, "CurrentErrorType")
                        cCarrierErrorCfg.CurrentErrorType = strTempValue

                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + i.ToString + "Carrier" + j.ToString, "CurrentNum")
                        If strTempValue <> "" Then
                            cCarrierErrorCfg.CurrentNum = CInt(strTempValue)
                        Else
                            cCarrierErrorCfg.CurrentNum = 0
                        End If

                        lListCarrier.Add(j, cCarrierErrorCfg)
                    Next
                    lListCarrierError.Add(i.ToString, lListCarrier)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SelectStationErrorToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("StationErrorTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "StationID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Enable"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ExpectCount"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ErrorCode"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "CurrentErrorType"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "CurrentErrorCount"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                For Each element As String In lListStationError.Keys
                    Dim cErrorCfg As clsStationErrorCfg = lListStationError(element)
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element <> cListSearchContion(0) Then
                                Continue For
                            End If
                        End If
                    End If

                    dt.Rows.Add(element, cErrorCfg.Enable.ToString.ToUpper, cErrorCfg.ExpectNum.ToString, cErrorCfg.ErrorCode.ToString, cErrorCfg.CurrentErrorType.ToString, cErrorCfg.CurrentNum.ToString)
                Next
                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage_Station) Then
                    cDataGridViewPage_Station.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage_Station.Paging(cViewPageType)
                Else
                    cHMIDataView_Station.DataSource = Ds.Tables(0)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SelectCarrierErrorToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("StationCarrierErrorTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "StationID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "CarrierID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Enable"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ExpectCount"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ErrorCode"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "CurrentErrorType"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "CurrentErrorCount"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True

                For Each elementStationKey As String In lListCarrierError.Keys
                    For Each elementCarrier As Integer In lListCarrierError(elementStationKey).Keys
                        Dim cCarrierErrorCfg As clsCarrierErrorCfg = lListCarrierError(elementStationKey)(elementCarrier)
                        If cListSearchContion.Count >= 1 Then
                            If cListSearchContion(0) <> "" Then
                                If elementStationKey <> cListSearchContion(0) Then
                                    Continue For
                                End If
                            End If
                        End If
                        If cListSearchContion.Count >= 2 Then
                            If cListSearchContion(1) <> "" Then
                                If elementCarrier <> cListSearchContion(1) Then
                                    Continue For
                                End If
                            End If
                        End If
                        dt.Rows.Add(elementStationKey, elementCarrier, cCarrierErrorCfg.Enable.ToString.ToUpper, cCarrierErrorCfg.ExpectNum.ToString, cCarrierErrorCfg.ErrorCode.ToString, cCarrierErrorCfg.CurrentErrorType.ToString, cCarrierErrorCfg.CurrentNum.ToString)
                    Next
                Next


                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage_Carrier) Then
                    cDataGridViewPage_Carrier.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage_Carrier.Paging(cViewPageType)
                Else
                    cHMIDataView_Carrier.DataSource = Ds.Tables(0)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function SelectStationCarrierErrorToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("CarrierErrorTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "StationID"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "Enable"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ExpectCount"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                dt.Columns.Add(cLanguageManager.GetTextLine(clsVariantManager.Name, "ErrorCode"))
                dt.Columns(dt.Columns.Count - 1).ReadOnly = True
                For Each element As String In lListStationError.Keys
                    Dim cErrorCfg As clsStationCarrierErrorCfg = lListStationCarrierError(element)
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element <> cListSearchContion(0) Then
                                Continue For
                            End If
                        End If
                    End If

                    dt.Rows.Add(element, cErrorCfg.Enable.ToString.ToUpper, cErrorCfg.ExpectNum.ToString, cErrorCfg.ErrorCode.ToString)
                Next
                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage_StationCarrier) Then
                    cDataGridViewPage_StationCarrier.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage_StationCarrier.Paging(cViewPageType)
                Else
                    cHMIDataView_StationCarrier.DataSource = Ds.Tables(0)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ModifyStationErrorData(ByVal strStationID As String, ByVal strEnable As String, ByVal strErrorCode As String, ByVal strExpectNumber As String) As Boolean
        SyncLock _Object
            Try
                If lListStationError.ContainsKey(strStationID) Then
                    lListStationError(strStationID).Enable = IIf(strEnable = "TRUE", True, False)
                    lListStationError(strStationID).ErrorCode = strErrorCode
                    If strExpectNumber = "" Then
                        lListStationError(strStationID).ExpectNum = 0
                    Else
                        lListStationError(strStationID).ExpectNum = CInt(strExpectNumber)
                    End If
                    SaveStationErrorData(strStationID)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ResetStationErrorData(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                If lListStationError.ContainsKey(strStationID) Then
                    lListStationError(strStationID).CurrentNum = 0
                    lListStationError(strStationID).CurrentErrorType = ""
                    SaveStationErrorData(strStationID)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ModifyCarrierStationErrorData(ByVal strStationID As String, ByVal strEnable As String, ByVal strErrorCode As String, ByVal strExpectNumber As String) As Boolean
        SyncLock _Object
            Try
                If lListStationCarrierError.ContainsKey(strStationID) Then
                    lListStationCarrierError(strStationID).Enable = IIf(strEnable = "TRUE", True, False)
                    lListStationCarrierError(strStationID).ErrorCode = strErrorCode
                    If strExpectNumber = "" Then
                        lListStationCarrierError(strStationID).ExpectNum = 0
                    Else
                        lListStationCarrierError(strStationID).ExpectNum = CInt(strExpectNumber)
                    End If

                End If

                For Each element As clsCarrierErrorCfg In lListCarrierError(strStationID).Values
                    element.Enable = lListStationCarrierError(strStationID).Enable
                    element.ExpectNum = lListStationCarrierError(strStationID).ExpectNum
                    element.ErrorCode = lListStationCarrierError(strStationID).ErrorCode
                Next
                SaveCarrierStationErrorData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ResetCarrierStationErrorData(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                For Each element As clsCarrierErrorCfg In lListCarrierError(strStationID).Values
                    element.CurrentNum = 0
                    element.CurrentErrorType = ""
                Next
                SaveCarrierStationErrorData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ResetCarrierErrorData(ByVal strStationID As String, ByVal strCarrierID As String) As Boolean
        SyncLock _Object
            Try
                Dim iCarrierID As Integer = 0
                If strCarrierID <> "" Then
                    iCarrierID = CInt(strCarrierID)
                End If
                If lListCarrierError.ContainsKey(strStationID) Then

                    If lListCarrierError(strStationID).ContainsKey(iCarrierID) Then
                        lListCarrierError(strStationID)(iCarrierID).CurrentNum = 0
                        lListCarrierError(strStationID)(iCarrierID).CurrentErrorType = ""
                    End If
                End If
                SaveCarrierErrorData(strStationID, iCarrierID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Sub SaveStationErrorData(ByVal strStationID As String)
        SyncLock _Object
            Try
                If lListStationError.ContainsKey(strStationID) Then
                    cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID, "Enable", lListStationError(strStationID).Enable.ToString.ToUpper)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID, "ExpectNum", lListStationError(strStationID).ExpectNum.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID, "ErrorCode", lListStationError(strStationID).ErrorCode.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID, "CurrentErrorType", lListStationError(strStationID).CurrentErrorType.ToString)
                    cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID, "CurrentNum", lListStationError(strStationID).CurrentNum.ToString)
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Sub SaveCarrierErrorData(ByVal strStationID As String, ByVal iCarrier As Integer)
        SyncLock _Object
            Try
                If lListCarrierError.ContainsKey(strStationID) Then
                    If lListCarrierError(strStationID).ContainsKey(iCarrier) Then
                        cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID.ToString + "Carrier" + iCarrier.ToString, "CurrentErrorType", lListCarrierError(strStationID)(iCarrier).CurrentErrorType)
                        cIniHandler.WriteIniFile(cSystemManager.Settings.StationErrorConfig, "Station" + strStationID.ToString + "Carrier" + iCarrier.ToString, "CurrentNum", lListCarrierError(strStationID)(iCarrier).CurrentNum.ToString)
                    End If
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Sub SaveCarrierStationErrorData()
        SyncLock _Object
            Try

                Dim lListValue As New List(Of String)
                For Each element In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(element)
                    Dim cStationErrorCfg As clsStationErrorCfg = lListStationError(cMachineStationCfg.ID)
                    lListValue.Add("[Station" + cMachineStationCfg.ID.ToString + "]")
                    lListValue.Add("Enable=" + cStationErrorCfg.Enable.ToString.ToUpper)
                    lListValue.Add("ExpectNum=" + cStationErrorCfg.ExpectNum.ToString.ToUpper)
                    lListValue.Add("ErrorCode=" + cStationErrorCfg.ErrorCode.ToString.ToUpper)
                    lListValue.Add("CurrentErrorType=" + cStationErrorCfg.CurrentErrorType.ToString.ToUpper)
                    lListValue.Add("CurrentNum=" + cStationErrorCfg.CurrentNum.ToString.ToUpper)

                    Dim cStationCarrierErrorCfg As clsStationCarrierErrorCfg = lListStationCarrierError(cMachineStationCfg.ID)
                    lListValue.Add("[StationCarrier" + cMachineStationCfg.ID.ToString + "]")
                    lListValue.Add("Enable=" + cStationCarrierErrorCfg.Enable.ToString.ToUpper)
                    lListValue.Add("ExpectNum=" + cStationCarrierErrorCfg.ExpectNum.ToString.ToUpper)
                    lListValue.Add("ErrorCode=" + cStationCarrierErrorCfg.ErrorCode.ToString.ToUpper)
                Next
                For i = 1 To cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count
                    Dim lListCarrier As Dictionary(Of Integer, clsCarrierErrorCfg) = lListCarrierError(i.ToString)
                    For Each element In lListCarrier.Keys
                        Dim cCarrierErrorCfg As clsCarrierErrorCfg = lListCarrier(element)
                        lListValue.Add("[Station" + i.ToString + "Carrier" + element.ToString + "]")
                        lListValue.Add("CurrentErrorType=" + cCarrierErrorCfg.CurrentErrorType.ToString.ToUpper)
                        lListValue.Add("CurrentNum=" + cCarrierErrorCfg.CurrentNum.ToString.ToUpper)
                    Next
                Next
                cIniHandler.SaveIniFile(cSystemManager.Settings.StationErrorConfig, lListValue)

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Sub

    Public Function UpdateStationError(ByVal strID As String, ByVal strErrorType As String, ByVal bResult As Boolean) As Boolean
        If strErrorType = "" Then Return True
        If lListStationError.ContainsKey(strID) Then
            If lListStationError(strID).Enable = False Then Return True
            If bResult Then
                lListStationError(strID).CurrentNum = 0
                lListStationError(strID).CurrentErrorType = ""
            Else
                If lListStationError(strID).CurrentErrorType = strErrorType Then
                    lListStationError(strID).CurrentNum = lListStationError(strID).CurrentNum + 1
                Else
                    lListStationError(strID).CurrentNum = 1
                    lListStationError(strID).CurrentErrorType = strErrorType
                End If
            End If

            If lListStationError(strID).CurrentNum >= lListStationError(strID).ExpectNum Then
                cHMIPLC = cDeviceManager.GetPLCDevice
                If Not IsNothing(cHMIPLC) Then
                    Dim cHMIError As New StructHMIError
                    If IsNumeric(lListStationError(strID).ErrorCode) Then
                        cHMIError.intErrorID = CInt(lListStationError(strID).ErrorCode)
                    Else
                        cHMIError.intErrorID = 0
                    End If
                    cHMIError.strErrorMessage = "StationError;" + strID + ";" + lListStationError(strID).CurrentErrorType
                    cHMIError.bulHmiError = True
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_Station_Error + "[" + strID + "]", cHMIError)
                End If
                lListStationError(strID).CurrentNum = 0
            End If
        End If
        SaveStationErrorData(strID)
        Return True
    End Function

    Public Function UpdateCarrierError(ByVal strID As String, ByVal iCarrierID As Integer, ByVal strErrorType As String, ByVal bResult As Boolean) As Boolean
        If strErrorType = "" Then Return True
        If lListCarrierError.ContainsKey(strID) Then
            If lListCarrierError(strID).ContainsKey(iCarrierID) Then
                If lListCarrierError(strID)(iCarrierID).Enable = False Then Return True
                If bResult Then
                    lListCarrierError(strID)(iCarrierID).CurrentNum = 0
                    lListCarrierError(strID)(iCarrierID).CurrentErrorType = ""
                Else
                    If lListCarrierError(strID)(iCarrierID).CurrentErrorType = strErrorType Then
                        lListCarrierError(strID)(iCarrierID).CurrentNum = lListCarrierError(strID)(iCarrierID).CurrentNum + 1
                    Else
                        lListCarrierError(strID)(iCarrierID).CurrentNum = 1
                        lListCarrierError(strID)(iCarrierID).CurrentErrorType = strErrorType
                    End If
                End If
            Else
                Dim cCarrierErrorCfg As New clsCarrierErrorCfg
                cCarrierErrorCfg.Enable = lListStationCarrierError(strID).Enable
                cCarrierErrorCfg.ErrorCode = lListStationCarrierError(strID).ErrorCode
                cCarrierErrorCfg.ExpectNum = lListStationCarrierError(strID).ExpectNum
                cCarrierErrorCfg.CurrentErrorType = strErrorType
                cCarrierErrorCfg.CurrentNum = 1
                lListCarrierError(strID).Add(iCarrierID, cCarrierErrorCfg)
            End If
            If lListCarrierError(strID)(iCarrierID).Enable = False Then Return True

            If lListCarrierError(strID)(iCarrierID).CurrentNum >= lListCarrierError(strID)(iCarrierID).ExpectNum Then
                cHMIPLC = cDeviceManager.GetPLCDevice
                If Not IsNothing(cHMIPLC) Then
                    Dim cHMIError As New StructHMIError
                    If IsNumeric(lListCarrierError(strID)(iCarrierID).ErrorCode) Then
                        cHMIError.intErrorID = CInt(lListCarrierError(strID)(iCarrierID).ErrorCode)
                    Else
                        cHMIError.intErrorID = 0
                    End If
                    cHMIError.strErrorMessage = "CarrierError;" + strID + ";" + iCarrierID.ToString + ";" + lListCarrierError(strID)(iCarrierID).CurrentErrorType
                    cHMIError.bulHmiError = True
                    cHMIPLC.WriteAny(HMI_PLC_Interface.HMI_Carrier_Error + "[" + strID + "]", cHMIError)
                End If
                lListCarrierError(strID)(iCarrierID).CurrentNum = 0
            End If
        End If
        SaveCarrierErrorData(strID, iCarrierID)
        Return True
    End Function

End Class

Public Class clsStationErrorCfg
    Public Enable As Boolean = False
    Public ExpectNum As Integer = 0
    Public ErrorCode As String = String.Empty
    Public CurrentErrorType As String = String.Empty
    Public CurrentNum As Integer = 0
End Class

Public Class clsStationCarrierErrorCfg
    Public Enable As Boolean = False
    Public ExpectNum As Integer = 0
    Public ErrorCode As String = String.Empty
End Class

Public Class clsCarrierErrorCfg
    Public Enable As Boolean = False
    Public ExpectNum As Integer = 0
    Public ErrorCode As String = String.Empty
    Public CurrentErrorType As String = String.Empty
    Public CurrentNum As Integer = 0
End Class