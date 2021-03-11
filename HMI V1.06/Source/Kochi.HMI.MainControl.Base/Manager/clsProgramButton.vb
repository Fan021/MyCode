Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Public Class clsProgramButton
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private lListPage As New Dictionary(Of String, clsIOPageCfg)
    Public Const Name As String = "ProgramButton"
    Private lListNewIndex As New Dictionary(Of String, String)
    Public ReadOnly Property ListNewIndex As Dictionary(Of String, String)
        Get
            Return lListNewIndex
        End Get
    End Property

    Public ReadOnly Property ListPage As Dictionary(Of String, clsIOPageCfg)
        Get
            Return lListPage
        End Get
    End Property


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                lListPage.Clear()
                Dim strTempValue As String = ""
                Dim i As Integer = 1
                For i = 1 To cMachineManager.MachineGlobalParameter.GetGlobalParameter(clsHMIGlobalParameter.Program_Max_Page)
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(i - 1)
                    If IsNothing(cMachineStationCfg) Then
                        cMachineStationCfg = New clsMachineStationCfg
                        cMachineStationCfg.StationName = "Group" + i.ToString
                    End If
                    Dim cIOPageCfg As New clsIOPageCfg(cSystemElement)
                    cIOPageCfg.ID = i.ToString

                    cIOPageCfg.Text = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Text")
                    cIOPageCfg.Text2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Text2")
                    If cIOPageCfg.Text = "" Then
                        cIOPageCfg.Text = cMachineStationCfg.StationName
                    End If
                    If cIOPageCfg.Text2 = "" Then
                        cIOPageCfg.Text2 = cMachineStationCfg.StationName
                    End If
                    cIOPageCfg.iXIndex = i

                    For j = 1 To 8
                        Dim cIOCfg As New clsIOCfg(cSystemElement)
                        cIOCfg.ID = j.ToString
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_PageType")
                        If strTempValue <> "" Then
                            cIOCfg.PageType = [Enum].Parse(GetType(enumIOType), strTempValue)
                        Else
                            cIOCfg.PageType = enumIOType.EL2008
                        End If
                        cIOPageCfg.IOType = cIOCfg.PageType
                        cIOCfg.Text = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text")
                        cIOCfg.Text2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text2")
                        cIOCfg.AdsName = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_AdsName")
                        cIOCfg.XIndex = i.ToString
                        cIOCfg.YIndex = j.ToString
                        cIOCfg.Reserve = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Reserve") <> "False", True, False)
                        If cIOCfg.Text = "" Then
                            cIOCfg.Text = "Y" + j.ToString
                        End If
                        If cIOCfg.Text2 = "" Then
                            cIOCfg.Text2 = "Y" + j.ToString
                        End If
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType")
                        If strTempValue <> "" Then cIOCfg.IOTriggerType = [Enum].Parse(GetType(enumIOTriggerType), strTempValue)
                        Select Case cIOCfg.PageType
                            Case enumIOType.EL1008
                                cIOCfg.IO = New InputIO
                                cIOCfg.AdsName = HMI_PLC_Interface.HMI_ProgramButton
                            Case enumIOType.EP1008
                                cIOCfg.IO = New InputIO
                                cIOCfg.AdsName = HMI_PLC_Interface.HMI_ProgramButton
                            Case enumIOType.EL2008
                                cIOCfg.IO = New OutputIO
                                cIOCfg.AdsName = HMI_PLC_Interface.HMI_ProgramButton
                        End Select
                        For k = 1 To 100
                            Dim cIOLockCfg As New clsIOLockCfg
                            Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeName_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.TypeName = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexX_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexX = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexY_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexY = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Status_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.Status = mTempValue
                            cIOCfg.ListLockIO.Add(cIOLockCfg)
                        Next
                        cIOPageCfg.ListIO.Add(j.ToString, cIOCfg)
                    Next
                    lListPage.Add(i.ToString, cIOPageCfg)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeIO(ByVal strID As String, ByVal bInput As Boolean, ByVal strText As String, ByVal strText2 As String, ByVal bReserve As Boolean, ByVal eIOTriggerType As enumIOTriggerType, ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
        SyncLock _Object
            Try
                Dim x As Integer = 0
                Dim y As Integer = 0
                x = CInt(strID.Split("_")(0))
                y = CInt(strID.Split("_")(2))
                lListPage(x.ToString).ListIO(y.ToString).Reserve = bReserve
                lListPage(x.ToString).ListIO(y.ToString).IOTriggerType = eIOTriggerType
                lListPage(x.ToString).ListIO(y.ToString).Text = strText
                lListPage(x.ToString).ListIO(y.ToString).Text2 = strText2
                lListPage(x.ToString).ListIO(y.ToString).ListLockIO = lListLockIO
                If bInput Then
                    lListPage(x.ToString).ListIO(y.ToString).PageType = enumIOType.EL1008
                    lListPage(x.ToString).IOType = enumIOType.EL1008
                    lListPage(x.ToString).ListIO(y.ToString).AdsName = HMI_PLC_Interface.HMI_ProgramButton
                Else
                    lListPage(x.ToString).ListIO(y.ToString).PageType = enumIOType.EL2008
                    lListPage(x.ToString).IOType = enumIOType.EL2008
                    lListPage(x.ToString).ListIO(y.ToString).AdsName = HMI_PLC_Interface.HMI_ProgramButton
                End If

                Select Case lListPage(x.ToString).ListIO(y.ToString).PageType
                    Case enumIOType.EL1008
                        If lListPage(x.ToString).ListIO(y.ToString).Text = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text = "X" + y.ToString
                        End If
                        If lListPage(x.ToString).ListIO(y.ToString).Text2 = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text2 = "X" + y.ToString
                        End If
                    Case enumIOType.EP1008
                        If lListPage(x.ToString).ListIO(y.ToString).Text = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text = "X" + y.ToString
                        End If
                        If lListPage(x.ToString).ListIO(y.ToString).Text2 = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text2 = "X" + y.ToString
                        End If
                    Case enumIOType.EL2008
                        If lListPage(x.ToString).ListIO(y.ToString).Text = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text = "Y" + y.ToString
                        End If
                        If lListPage(x.ToString).ListIO(y.ToString).Text2 = "" Then
                            lListPage(x.ToString).ListIO(y.ToString).Text2 = "Y" + y.ToString
                        End If
                End Select

                Select Case lListPage(x.ToString).ListIO(y.ToString).PageType
                    Case enumIOType.EL1008
                        lListPage(x.ToString).ListIO(y.ToString).IO = New InputIO
                        lListPage(x.ToString).ListIO(y.ToString).AdsName = HMI_PLC_Interface.HMI_ProgramButton
                    Case enumIOType.EP1008
                        lListPage(x.ToString).ListIO(y.ToString).IO = New InputIO
                        lListPage(x.ToString).ListIO(y.ToString).AdsName = HMI_PLC_Interface.HMI_ProgramButton
                    Case enumIOType.EL2008
                        lListPage(x.ToString).ListIO(y.ToString).IO = New OutputIO
                        lListPage(x.ToString).ListIO(y.ToString).AdsName = HMI_PLC_Interface.HMI_ProgramButton
                End Select
                SaveData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetIOCfgFromID(ByVal strPageID As String, ByVal strID As String) As clsIOCfg
        SyncLock _Object
            If Not lListPage.ContainsKey(strPageID) Then
                Return Nothing
            End If
            If Not lListPage(strPageID).ListIO.ContainsKey(strID) Then
                Return Nothing
            End If
            Return lListPage(strPageID).ListIO(strID)
        End SyncLock
    End Function

    Public Function GetIOCfgFromID(ByVal strID As String) As clsIOCfg
        SyncLock _Object
            If strID.IndexOf("_") < 0 Then
                Return Nothing
            End If
            Dim x As Integer = 0
            Dim y As Integer = 0
            x = CInt(strID.Split("_")(0))
            y = CInt(strID.Split("_")(2))
            Return lListPage(x.ToString).ListIO(y.ToString)
        End SyncLock
    End Function

    Public Function GetIOCfgFromIndex(ByVal strIndex As String) As clsIOCfg
        SyncLock _Object
            If strIndex = "" Then Return New clsIOCfg(cSystemElement)
            Dim i As Integer = 1
            For Each element As clsIOPageCfg In lListPage.Values
                For Each subelment As clsIOCfg In element.ListIO.Values
                    If i = strIndex Then
                        Return subelment
                    End If
                    i = i + 1
                Next

            Next
            Return New clsIOCfg(cSystemElement)
        End SyncLock
    End Function

    Public Function GetIOPageCfgFromID(ByVal strID As String) As clsIOPageCfg
        SyncLock _Object
            Return lListPage(strID)
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                For Each element As clsIOPageCfg In lListPage.Values

                    If element.Text = "" Then
                        element.Text = "Group" + element.ID
                    End If
                    If element.Text2 = "" Then
                        element.Text2 = "Group" + element.ID
                    End If
                    If element.Text.IndexOf("Group") >= 0 Then
                        Dim cValue() As String = element.Text.Split("_")
                        If cValue.Length = 1 Then
                            element.Text = "Group" + element.ID
                        ElseIf cValue.Length > 1 Then
                            If IsNumeric(cValue(1)) Then
                                element.Text = "Group" + element.ID
                            End If
                        End If
                    End If
                    If element.Text2.IndexOf("Group") >= 0 Then
                        Dim cValue() As String = element.Text.Split("_")
                        If cValue.Length = 1 Then
                            element.Text2 = "Group" + element.ID
                        ElseIf cValue.Length > 1 Then
                            If IsNumeric(cValue(1)) Then
                                element.Text2 = "Group" + element.ID
                            End If
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function MovePage(ByVal strOldID As String, ByVal strNewID As String) As Boolean
        SyncLock _Object
            Try
                lListNewIndex.Clear()
                Dim lNewListPage As New Dictionary(Of String, clsIOPageCfg)
                Dim j As Integer = 1
                For i = 1 To lListPage.Count
                    If i = strOldID Then
                        Continue For
                    End If
                    If i = strNewID Then
                        lNewListPage.Add(j, lListPage(strOldID).Clone)
                        If lListNewIndex.ContainsKey(lNewListPage(j).ID) Then
                            lListNewIndex(lNewListPage(j).ID) = j
                        Else
                            lListNewIndex.Add(lNewListPage(j).ID, j)
                        End If
                        lNewListPage(j).ID = j
                        j = j + 1
                    End If
                    lNewListPage.Add(j, lListPage(i).Clone)
                    If lListNewIndex.ContainsKey(lNewListPage(j).ID) Then
                        lListNewIndex(lNewListPage(j).ID) = j
                    Else
                        lListNewIndex.Add(lNewListPage(j).ID, j)
                    End If
                    lNewListPage(j).ID = j
                    j = j + 1
                Next
                lListPage = lNewListPage
                ChangeID()
                SaveData()
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function ChangePage(ByVal strID As String, ByVal strText As String, ByVal strText2 As String) As Boolean
        SyncLock _Object
            Try
                lListPage(strID).Text = strText
                lListPage(strID).Text2 = strText2
                If lListPage(strID).Text = "" Then
                    lListPage(strID).Text = "Group" + strID.ToString
                End If
                If lListPage(strID).Text2 = "" Then
                    lListPage(strID).Text2 = "Group" + strID.ToString
                End If
                ChangeID()
                SaveData()
                LoadData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
 
    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim i As Integer = 1
                Dim lListValue As New List(Of String)
                For Each element As clsIOPageCfg In lListPage.Values
                    lListValue.Add("[Page" + i.ToString + "]")
                    lListValue.Add("Type=" + element.IOType.ToString)
                    lListValue.Add("Text=" + element.Text.ToString)
                    lListValue.Add("Text2=" + element.Text2.ToString)
                    Dim j As Integer = 1
                    For Each subelment As clsIOCfg In element.ListIO.Values
                        lListValue.Add("Item" + "_" + j.ToString + "_Text=" + subelment.Text)
                        lListValue.Add("Item" + "_" + j.ToString + "_Text2=" + subelment.Text2)
                        lListValue.Add("Item" + "_" + j.ToString + "_AdsName=" + subelment.AdsName)
                        lListValue.Add("Item" + "_" + j.ToString + "_Reserve=" + subelment.Reserve.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_IOTriggerType=" + subelment.IOTriggerType.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_PageType=" + subelment.PageType.ToString)
                        Dim k As Integer = 1
                        For Each subsubelement As clsIOLockCfg In subelment.ListLockIO
                            lListValue.Add("Item" + "_" + j.ToString + "_TypeName_" + k.ToString + "=" + subsubelement.TypeName)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexX_" + k.ToString + "=" + subsubelement.IndexX.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexY_" + k.ToString + "=" + subsubelement.IndexY.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_Status_" + k.ToString + "=" + subsubelement.Status.ToString)
                            k = k + 1
                        Next
                        j = j + 1
                    Next
                    i = i + 1
                Next
                cIniHandler.SaveIniFile(cSystemManager.Settings.ProgramDebugConfig, lListValue)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class

