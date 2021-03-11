Imports System.Math
Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class clsMachineStatusManager
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private lListStationStatus As New Dictionary(Of String, clsMachineStatusCfg)
    Public lListVariantManager As New Dictionary(Of String, clsVariantManager)
    Public lListPictureShowManager As New Dictionary(Of String, clsPictureShowManager)
    Private cIniHandler As New clsIniHandler
    Private cTabControl As TabControl
    Private cVariantLabel As Label
    Private cSFCLabel As Label
    Private cTotalLabel As Label
    Private cPassLabel As Label
    Private cFailLabel As Label
    Private cTotalTimeLabel As Label
    Private cFailRateLabel As Label
    Private _Object As New Object
    Private mMainForm As IMainUI
    Private cMachineStatus As New StructMachineStatus
    Private cLanguageManager As clsLanguageManager
    Private iActivePage As Integer = 1
    Private iActiveNumber As Integer = 0
    Private lListDisableChangePage As New Dictionary(Of String, Boolean)
    Public Const Name As String = "MachineStatusManager"
    Private cDeviceManager As clsDeviceManager
    Private cHMIPLC As clsHMIPLC
    Public Event LocalVariantChanged(ByVal strStationID As String, ByVal cLocalVariant As clsVariantManager)

    Public ReadOnly Property ListDisableChangePage As Dictionary(Of String, Boolean)
        Get
            Return lListDisableChangePage
        End Get
    End Property

    Public ReadOnly Property ActivePage As Integer
        Get
            Return iActivePage
        End Get
    End Property



    Public Property MachineStatus As StructMachineStatus
        Set(ByVal value As StructMachineStatus)
            SyncLock _Object
                cMachineStatus = value
            End SyncLock
        End Set
        Get
            Return cMachineStatus
        End Get
    End Property

    Public Function RegisterVariantManager(ByVal strStationID As String, ByVal cVariantManager As clsVariantManager) As Boolean
        SyncLock _Object
            If lListVariantManager.ContainsKey(strStationID) Then
                lListVariantManager(strStationID) = cVariantManager
            Else
                lListVariantManager.Add(strStationID, cVariantManager)
            End If
            RaiseEvent LocalVariantChanged(strStationID, cVariantManager)
            Return True
        End SyncLock
    End Function

    Public Function RegisterPictureShowManager(ByVal strStationID As String, ByVal cPictureShowManager As clsPictureShowManager) As Boolean
        SyncLock _Object
            If lListVariantManager.ContainsKey(strStationID) Then
                lListPictureShowManager(strStationID) = cPictureShowManager
            Else
                lListPictureShowManager.Add(strStationID, cPictureShowManager)
            End If
            Return True
        End SyncLock
    End Function
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cDeviceManager = CType(cSystemElement(clsDeviceManager.Name), clsDeviceManager)
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetMachineStatusCfgFromName(ByVal strStationID As String) As clsMachineStatusCfg
        SyncLock _Object
            Try
                If Not lListStationStatus.ContainsKey(strStationID) Then
                    ' Throw New clsHMIException(cLanguageManager.GetTextLine("MachineStatusManager", "1", strStationID), enumExceptionType.Crash)
                    Return Nothing
                End If
                Return lListStationStatus(strStationID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMachineStatusKey() As List(Of String)
        SyncLock _Object
            Try
                Return lListStationStatus.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(
                                    ByVal cTabControl As TabControl,
                                    ByVal cVariantLabel As Label,
                                    ByVal cSFCLabel As Label,
                                    ByVal cTotalLabel As Label,
                                    ByVal cPassLabel As Label,
                                    ByVal cFailLabel As Label,
                                    ByVal cFailRateLabel As Label,
                                    ByVal cTotalTimeLabel As Label
                                    ) As Boolean
        Try
            Me.cTabControl = cTabControl
            Me.cVariantLabel = cVariantLabel
            Me.cSFCLabel = cSFCLabel
            Me.cTotalLabel = cTotalLabel
            Me.cPassLabel = cPassLabel
            Me.cFailLabel = cFailLabel
            Me.cFailRateLabel = cFailRateLabel
            Me.cTotalTimeLabel = cTotalTimeLabel

            Return True
        Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
    End Function

    Public Function AddPassCount(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).StationCounterCfg.PassCount = lListStationStatus(strStationID).StationCounterCfg.PassCount + 1
                lListStationStatus(strStationID).StationCounterCfg.TotalCount = lListStationStatus(strStationID).StationCounterCfg.TotalCount + 1
                lListStationStatus(strStationID).StationCounterCfg.FailRate = Round((lListStationStatus(strStationID).StationCounterCfg.PassCount * 100.0 / lListStationStatus(strStationID).StationCounterCfg.TotalCount * 1.0), 2)
                ShowMessage(strStationID)
                SaveData(strStationID)
                If lListDisableChangePage.ContainsKey(strStationID) Then
                    If Not lListDisableChangePage(strStationID) Then
                        AddPage(strStationID)
                    End If
                Else
                    AddPage(strStationID)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function AddDisablePage(ByVal strStationID As String, ByVal bValue As Boolean) As Boolean
        If lListDisableChangePage.ContainsKey(strStationID) Then
            lListDisableChangePage(strStationID) = bValue
        Else
            lListDisableChangePage.Add(strStationID, bValue)
        End If
        Return True
    End Function
    Public Function AddFailCount(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).StationCounterCfg.FailCount = lListStationStatus(strStationID).StationCounterCfg.FailCount + 1
                lListStationStatus(strStationID).StationCounterCfg.TotalCount = lListStationStatus(strStationID).StationCounterCfg.TotalCount + 1
                lListStationStatus(strStationID).StationCounterCfg.FailRate = Round((lListStationStatus(strStationID).StationCounterCfg.PassCount * 100.0 / lListStationStatus(strStationID).StationCounterCfg.TotalCount * 1.0), 2)
                ShowMessage(strStationID)
                SaveData(strStationID)
                If lListDisableChangePage.ContainsKey(strStationID) Then
                    If Not lListDisableChangePage(strStationID) Then
                        AddPage(strStationID)
                    End If
                Else
                    AddPage(strStationID)
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function GetWork() As Boolean
        SyncLock _Object
            Try
                For Each element As clsMachineStatusCfg In lListStationStatus.Values
                    If element.StationStatus = enumStationStatus.Work Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SaveData(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try

                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "Variant", lListStationStatus(strStationID).VariantCfg.Variant.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "SFC", lListStationStatus(strStationID).VariantCfg.SFC.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "TotalCount", lListStationStatus(strStationID).StationCounterCfg.TotalCount.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "PassCount", lListStationStatus(strStationID).StationCounterCfg.PassCount.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "FailCount", lListStationStatus(strStationID).StationCounterCfg.FailCount.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "Status", lListStationStatus(strStationID).StationStatus.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "Active", lListStationStatus(strStationID).Active.ToString)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadConfig() As Boolean
        SyncLock _Object
            Try
                lListStationStatus.Clear()
                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    lListStationStatus.Add(element.ID, New clsMachineStatusCfg(element.ID))

                Next
                ReadConfig()

                If Not IsNothing(cTabControl) Then
                    If cTabControl.TabPages.Count > 0 AndAlso Not IsNothing(cTabControl.SelectedTab) Then
                        If Not IsNothing(cTabControl.SelectedTab.Name) Then
                            ShowMessage(cTabControl.SelectedTab.Name)
                        End If
                    End If
                End If
                Return True

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ReadConfig() As Boolean
        SyncLock _Object
            Try

                Dim mTempValue As String = String.Empty

                For Each element As clsMachineStatusCfg In lListStationStatus.Values
                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "TotalCount")
                    If mTempValue <> "" AndAlso IsNumeric(mTempValue) Then
                        element.StationCounterCfg.TotalCount = CInt(mTempValue)
                    Else
                        element.StationCounterCfg.TotalCount = 0
                    End If
                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "PassCount")
                    If mTempValue <> "" AndAlso IsNumeric(mTempValue) Then
                        element.StationCounterCfg.PassCount = CInt(mTempValue)
                    Else
                        element.StationCounterCfg.PassCount = 0
                    End If
                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "FailCount")
                    If mTempValue <> "" AndAlso IsNumeric(mTempValue) Then
                        element.StationCounterCfg.FailCount = CInt(mTempValue)
                    Else
                        element.StationCounterCfg.FailCount = 0
                    End If
                    If element.StationCounterCfg.TotalCount <= 0 Then
                        element.StationCounterCfg.FailRate = 0.0
                    Else
                        element.StationCounterCfg.FailRate = Round((element.StationCounterCfg.PassCount * 100.0 / element.StationCounterCfg.TotalCount * 1.0), 2)
                    End If

                    mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "Active")
                    element.Active = IIf(mTempValue.ToUpper = "TRUE", True, False)


                    element.ProductionLoggingCfg.strStartTime = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "StartTime")
                    element.ProductionLoggingCfg.bInsert = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", element.StationID, "Insert") = "TRUE", True, False)
                Next
                Return True

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock

    End Function
    Public Function ChangeNextPage() As Boolean
        SyncLock _Object
            Try
                FindPage()
                SetPage()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function FindPage() As Boolean
        SyncLock _Object
            Try
                If cMachineManager.MachineCellManager.HasManualStation Then
                    Dim lListStationCfg As New List(Of clsMachineStationCfg)
                    For i = iActivePage To cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey.Count - 1
                        lListStationCfg.Add(cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(i))
                    Next
                    If iActivePage >= 1 Then
                        For i = 0 To iActivePage - 1
                            lListStationCfg.Add(cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(i))
                        Next
                    End If

                    For Each element As clsMachineStationCfg In lListStationCfg
                        If element.MachineStationType = enumMachineStationType.Auto Then
                            Continue For
                        End If
                        If element.MachineStationType = enumMachineStationType.Manual Then
                            If element.AutoChangePage > 0 Then
                                iActivePage = element.ID
                                iActiveNumber = 0
                                Exit For
                            ElseIf element.AutoChangePage = 0 Then
                                If cMachineManager.MachineCellManager.HasOneManualStation() Then
                                    iActivePage = element.ID
                                    iActiveNumber = 0
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Else
                    iActivePage = 1
                    iActiveNumber = 0
                    For Each element In lListStationStatus.Values
                        If element.Active = True Then
                            iActivePage = element.StationID
                            iActiveNumber = 0
                        End If
                    Next
                End If
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActivePage", iActivePage.ToString)
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActiveNumber", iActiveNumber.ToString)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End SyncLock
    End Function


    Public Function LoadPage() As Boolean
        SyncLock _Object
            Try
                iActivePage = 1
                iActiveNumber = 0
                Dim mTempValue As String = String.Empty
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActivePage")
                If mTempValue <> "" Then
                    iActivePage = CInt(mTempValue)
                End If
                mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActiveNumber")
                If mTempValue <> "" Then
                    iActiveNumber = CInt(mTempValue)
                End If

                If cMachineManager.MachineCellManager.HasManualStation Then
                    If cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(iActivePage - 1).MachineStationType = enumMachineStationType.Auto Then
                        FindPage()
                    Else
                        If iActiveNumber >= cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(iActivePage - 1).AutoChangePage Then
                            FindPage()
                        End If
                    End If
                Else
                    FindPage()
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddPage(ByVal strStationID As String) As Boolean
        SyncLock _Object
            If strStationID = iActivePage Then
                iActiveNumber = iActiveNumber + 1
                Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(strStationID)
                If Not IsNothing(cMachineStationCfg) Then
                    If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then
                        If iActiveNumber >= cMachineStationCfg.AutoChangePage Then
                            FindPage()
                            SetPage()
                        End If
                    Else
                        SetPage()
                    End If
                End If
            End If
            Return True
        End SyncLock
    End Function

    Public Sub Add()
        AddHandler cTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
    End Sub

    Public Sub Remove()
        RemoveHandler cTabControl.SelectedIndexChanged, AddressOf TabControl_SelectedIndexChanged
    End Sub

    Public Function SetPage() As Boolean
        SyncLock _Object
            Try
                mMainForm.InvokeAction(Sub()
                                           If cTabControl.TabPages.Count >= iActivePage And iActivePage > 0 Then
                                               If cTabControl.SelectedIndex <> iActivePage - 1 Then
                                                   cTabControl.SelectedIndex = iActivePage - 1
                                               End If
                                           End If
                                       End Sub)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ResetPage(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromID(strStationID)
            If Not IsNothing(cMachineStationCfg) Then
                If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then
                    iActivePage = strStationID
                    iActiveNumber = 0
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActivePage", "1")
                    cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", "Active", "ActiveNumber", "0")
                    SetPage()
                End If
            End If
            For Each element In lListStationStatus.Values
                SaveData(element.StationID)
            Next

            Return True
        End SyncLock
    End Function

    Public Function ResetCount() As Boolean
        SyncLock _Object
            Try
                Dim mTempValue As String = String.Empty
                For Each element As clsMachineStatusCfg In lListStationStatus.Values
                    element.StationCounterCfg.TotalCount = 0
                    element.StationCounterCfg.PassCount = 0
                    element.StationCounterCfg.FailCount = 0
                    element.StationCounterCfg.FailRate = 0
                    SaveData(element.StationID)

                    ShowMessage(element.StationID)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock

    End Function


    Public Function ShowMessage(ByVal strStationID As String) As String
        SyncLock _Object
            Try

                mMainForm.InvokeAction(Sub()
                                           If Not IsNothing(cTabControl.SelectedTab) Then
                                               If strStationID <> cTabControl.SelectedTab.Name Then
                                                   Return
                                               End If
                                           End If


                                           If lListStationStatus.ContainsKey(strStationID) Then
                                               cVariantLabel.Text = lListStationStatus(strStationID).VariantCfg.Variant
                                               cSFCLabel.Text = lListStationStatus(strStationID).VariantCfg.SFC
                                               cTotalLabel.Text = lListStationStatus(strStationID).StationCounterCfg.TotalCount.ToString
                                               cPassLabel.Text = lListStationStatus(strStationID).StationCounterCfg.PassCount.ToString
                                               cFailLabel.Text = lListStationStatus(strStationID).StationCounterCfg.FailCount.ToString
                                               cFailRateLabel.Text = lListStationStatus(strStationID).StationCounterCfg.FailRate.ToString("0.00") + " %"
                                               cTotalTimeLabel.Text = lListStationStatus(strStationID).StationCounterCfg.TotalTime.ToString("0.00") + " s"
                                           Else
                                               cVariantLabel.Text = ""
                                               cSFCLabel.Text = ""
                                               cTotalLabel.Text = "0"
                                               cPassLabel.Text = "0"
                                               cFailLabel.Text = "0"
                                               cFailRateLabel.Text = "0.00 %"
                                               cTotalTimeLabel.Text = "0.00 s"
                                           End If
                                           If lListStationStatus.ContainsKey(strStationID) AndAlso lListStationStatus(strStationID).StationStatus = enumStationStatus.PASS Then
                                               cPassLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Pass)
                                           Else
                                               cPassLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Total)
                                           End If
                                           If lListStationStatus.ContainsKey(strStationID) AndAlso lListStationStatus(strStationID).StationStatus = enumStationStatus.FAIL Then
                                               cFailLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Fail)
                                           Else
                                               cFailLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Total)
                                           End If

                                           If lListStationStatus.ContainsKey(strStationID) AndAlso (lListStationStatus(strStationID).StationStatus = enumStationStatus.Work Or lListStationStatus(strStationID).StationStatus = enumStationStatus.UnKnow) Then
                                               cPassLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Total)
                                               cFailLabel.BackColor = ColorTranslator.FromWin32(enumHMI_COLOR.HMI_COLOR_Label_Back_Total)
                                           End If

                                       End Sub)

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SetSFCRunning(ByVal strStationID As String, ByVal bResult As Boolean) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).SFCRunning = bResult
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function




    Public Function SetJump(ByVal strStationID As String, ByVal bJump As Boolean) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                If lListVariantManager.ContainsKey(strStationID) Then
                    lListVariantManager(strStationID).CurrentVariantCfg.Jump = bJump
                End If
                lListStationStatus(strStationID).VariantCfg.Jump = bJump
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try

        End SyncLock
    End Function

    Public Function SetSFC(ByVal strStationID As String, ByVal strSFC As String) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                If lListVariantManager.ContainsKey(strStationID) Then
                    lListVariantManager(strStationID).CurrentVariantCfg.SFC = strSFC
                End If
                lListStationStatus(strStationID).VariantCfg.SFC = strSFC
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try

        End SyncLock
    End Function


    Public Function SetTotalTIme(ByVal strStationID As String, ByVal dTotalTime As Double) As Boolean
        SyncLock _Object
            Try
                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).StationCounterCfg.TotalTime = dTotalTime
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try

        End SyncLock
    End Function

    Public Function SetCarrierID(ByVal strStationID As String, ByVal iCarrierID As Integer) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                If lListVariantManager.ContainsKey(strStationID) Then
                    lListVariantManager(strStationID).CurrentVariantCfg.CarrierID = iCarrierID
                End If
                lListStationStatus(strStationID).VariantCfg.CarrierID = iCarrierID
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try

        End SyncLock
    End Function

    Public Function SetStartTime(ByVal strStationID As String, ByVal strStartTime As String) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).ProductionLoggingCfg.strStartTime = strStartTime
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "StartTime", strStartTime)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SetInsert(ByVal strStationID As String, ByVal bInsert As Boolean) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).ProductionLoggingCfg.bInsert = bInsert
                cIniHandler.WriteIniFile(cSystemManager.Settings.LogFolder + "\MachineStatus.ini", strStationID, "Insert", bInsert.ToString)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SetVariant(ByVal strStationID As String, ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                lListStationStatus(strStationID).VariantCfg.Variant = strVariant
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SetStationStatus(ByVal strStationID As String, ByVal eStationStatus As enumStationStatus) As Boolean
        SyncLock _Object
            Try

                If Not lListStationStatus.ContainsKey(strStationID) Then
                    Return False
                End If
                If eStationStatus = enumStationStatus.Interrupt Then
                    If lListStationStatus(strStationID).StationStatus = enumStationStatus.Work Then
                        lListStationStatus(strStationID).StationStatus = enumStationStatus.FAIL
                    End If
                Else
                    lListStationStatus(strStationID).StationStatus = eStationStatus
                End If
                ShowMessage(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Dim mStationID As String = cTabControl.SelectedTab.Name
            For Each element In lListStationStatus.Values
                If element.StationID = mStationID Then
                    element.Active = True
                Else
                    element.Active = False
                End If
            Next
            '  lListPictureShowManager(mStationID).TapChanged()
            ShowMessage(mStationID)
            System.Threading.Thread.Sleep(10)
            ResetPage(mStationID)
            cHMIPLC = cDeviceManager.GetPLCDevice
            If Not IsNothing(cHMIPLC) Then
                If cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.WrtieCurrentStation).ToString.ToUpper = "TRUE" Then
                    cHMIPLC.WriteAny(".HMI_CurrentStation", Int16.Parse(mStationID))
                End If
            End If
        End SyncLock
    End Sub
End Class

Public Class clsMachineStatusCfg
    Public Const Name As String = "MachineStatusCfg"
    Private cStationCounterCfg As New clsStationCounterCfg
    Private cVariantCfg As New clsVariantCfg
    Private eStationStatus As enumStationStatus = enumStationStatus.UnKnow
    Private strStationID As String = String.Empty
    Private cProductionLoggingCfg As New clsProductionLoggingCfg
    Protected _Object As New Object
    Private bSFCRunning As Boolean = False
    Private bActive As Boolean = False

    Public Property Active As Boolean
        Set(ByVal value As Boolean)
            bActive = value
        End Set
        Get
            Return bActive
        End Get
    End Property

    Public Property SFCRunning As Boolean
        Set(ByVal value As Boolean)
            bSFCRunning = value
        End Set
        Get
            Return bSFCRunning
        End Get
    End Property
    Public Property StationID As String
        Set(ByVal value As String)
            SyncLock _Object
                strStationID = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strStationID
            End SyncLock
        End Get
    End Property
    Public Property StationCounterCfg As clsStationCounterCfg
        Set(ByVal value As clsStationCounterCfg)
            SyncLock _Object
                cStationCounterCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cStationCounterCfg
            End SyncLock
        End Get
    End Property

    Public Property ProductionLoggingCfg As clsProductionLoggingCfg
        Set(ByVal value As clsProductionLoggingCfg)
            SyncLock _Object
                cProductionLoggingCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cProductionLoggingCfg
            End SyncLock
        End Get
    End Property

    Public Property VariantCfg As clsVariantCfg
        Set(ByVal value As clsVariantCfg)
            SyncLock _Object
                cVariantCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cVariantCfg
            End SyncLock
        End Get
    End Property
    Public Property StationStatus As enumStationStatus
        Set(ByVal value As enumStationStatus)
            SyncLock _Object
                eStationStatus = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eStationStatus
            End SyncLock
        End Get
    End Property

    Sub New(ByVal strStationID As String)
        Me.strStationID = strStationID
    End Sub
End Class


Public Enum enumStationStatus
    UnKnow = 1
    Work
    PASS
    FAIL
    Interrupt
End Enum

Public Class clsProductionLoggingCfg
    Public strStartTime As String = String.Empty
    Public bInsert As Boolean = False
End Class

Public Class clsStationCounterCfg
    Private iTotalCount As Integer = 0
    Private iPassCount As Integer = 0
    Private iFailCount As Integer = 0
    Private dFailRate As Double = 0
    Private dTotalTime As Double = 0
    Protected _Object As New Object

    Public Property TotalCount As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iTotalCount = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iTotalCount
            End SyncLock
        End Get
    End Property

    Public Property PassCount As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iPassCount = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iPassCount
            End SyncLock
        End Get
    End Property

    Public Property FailCount As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iFailCount = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iFailCount
            End SyncLock
        End Get
    End Property

    Public Property FailRate As Double
        Set(ByVal value As Double)
            SyncLock _Object
                dFailRate = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return dFailRate
            End SyncLock
        End Get
    End Property

    Public Property TotalTime As Double
        Set(ByVal value As Double)
            SyncLock _Object
                dTotalTime = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return dTotalTime
            End SyncLock
        End Get
    End Property

    Sub New()
    End Sub


End Class
