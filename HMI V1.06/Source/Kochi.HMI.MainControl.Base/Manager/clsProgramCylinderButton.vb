Imports Kochi.HMI.MainControl.UI
Public Class clsProgramCylinderButton
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private lListPage As New Dictionary(Of String, clsCylinderPageCfg)
    Public Const Name As String = "ProgramCylinderButton"
    Public ReadOnly Property ListPage As Dictionary(Of String, clsCylinderPageCfg)
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
                    Dim cIOPageCfg As New clsCylinderPageCfg(cSystemElement)
                    cIOPageCfg.ID = i.ToString
                    cIOPageCfg.Text = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Text")
                    cIOPageCfg.Text2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Text2")
                    If cIOPageCfg.Text = "" Then
                        cIOPageCfg.Text = cMachineStationCfg.StationName
                    End If
                    If cIOPageCfg.Text2 = "" Then
                        cIOPageCfg.Text2 = cMachineStationCfg.StationName
                    End If

                    For j = 1 To 8
                        Dim cIOCfg As New clsCylinderCfg(cSystemElement)
                        cIOCfg.ID = j.ToString
                        cIOCfg.TextA = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextA")
                        cIOCfg.TextA2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextA2")
                        cIOCfg.TextB = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextB")
                        cIOCfg.TextB2 = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextB2")
                        If cIOCfg.TextA = "" Then cIOCfg.TextA = "Cylinder" + j.ToString + ".A"
                        If cIOCfg.TextA2 = "" Then cIOCfg.TextA2 = "Cylinder" + j.ToString + ".A"
                        If cIOCfg.TextB = "" Then cIOCfg.TextB = "Cylinder" + j.ToString + ".B"
                        If cIOCfg.TextB2 = "" Then cIOCfg.TextB2 = "Cylinder" + j.ToString + ".B"
                        cIOCfg.XIndex = i.ToString
                        cIOCfg.YIndex = j.ToString
                        cIOCfg.ReserveA = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_ReserveA") = "False", False, True)
                        cIOCfg.ReserveB = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_ReserveB") = "False", False, True)
                        cIOCfg.OneCylinder = IIf(cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_OneCylinder") = "False", False, True)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType")
                        If strTempValue <> "" Then cIOCfg.TriggerType = [Enum].Parse(GetType(enumIOTriggerType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAType")
                        If strTempValue <> "" Then cIOCfg.SensorAType = [Enum].Parse(GetType(enumIOType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBType")
                        If strTempValue <> "" Then cIOCfg.SensorBType = [Enum].Parse(GetType(enumIOType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAXIndex")
                        If strTempValue <> "" Then cIOCfg.SensorAXIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAYIndex")
                        If strTempValue <> "" Then cIOCfg.SensorAYIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBXIndex")
                        If strTempValue <> "" Then cIOCfg.SensorBXIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBYIndex")
                        If strTempValue <> "" Then cIOCfg.SensorBYIndex = CInt(strTempValue)
                        cIOCfg.CylinderIO = New CylinderIO
                        For k = 1 To 100
                            Dim cIOLockCfg As New clsIOLockCfg
                            Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeNameA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.TypeName = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexXA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexX = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexYA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexY = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_StatusA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.Status = mTempValue
                            cIOCfg.ListLockIOA.Add(cIOLockCfg)
                        Next

                        For k = 1 To 100
                            Dim cIOLockCfg As New clsIOLockCfg
                            Dim mTempValue As String = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeNameB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.TypeName = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexXB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexX = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexYB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexY = mTempValue
                            mTempValue = cIniHandler.ReadIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_StatusB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.Status = mTempValue
                            cIOCfg.ListLockIOB.Add(cIOLockCfg)
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

    Public Function RemoveData(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                lListPage.Clear()
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                Dim strTempValue As String = ""
                For i = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Cylinder_Max_Page) + 1 To 100
                    cIniHandler.RemoveSection(cSystemManager.Settings.ProgramCylinderDebugConfig, "Page" + i.ToString)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeIO(ByVal strID As String,
                             ByVal strTextA As String,
                             ByVal strTextA2 As String,
                             ByVal strTextB As String,
                             ByVal strTextB2 As String,
                             ByVal bReserveA As Boolean,
                             ByVal bReserveB As Boolean,
                             ByVal eIOTriggerType As enumIOTriggerType,
                             ByVal eSensorAType As enumIOType,
                             ByVal iSensorAXIndex As Integer,
                             ByVal iSensorAYIndex As Integer,
                             ByVal eSensorBType As enumIOType,
                             ByVal iSensorBXIndex As Integer,
                             ByVal iSensorBYIndex As Integer,
                             ByVal bOneCylinder As Boolean,
                             ByVal lListLockIOA As List(Of clsIOLockCfg),
                             ByVal lListLockIOB As List(Of clsIOLockCfg)
                             ) As Boolean
        SyncLock _Object
            Try
                Dim x As Integer = 0
                Dim y As Integer = 0
                x = CInt(strID.Split("_")(0))
                y = CInt(strID.Split("_")(1))
                lListPage(x.ToString).ListIO(y.ToString).ReserveA = bReserveA
                lListPage(x.ToString).ListIO(y.ToString).ReserveB = bReserveB
                lListPage(x.ToString).ListIO(y.ToString).TriggerType = eIOTriggerType
                lListPage(x.ToString).ListIO(y.ToString).TextA = strTextA
                lListPage(x.ToString).ListIO(y.ToString).TextA2 = strTextA2
                lListPage(x.ToString).ListIO(y.ToString).TextB = strTextB
                lListPage(x.ToString).ListIO(y.ToString).TextB2 = strTextB2
                lListPage(x.ToString).ListIO(y.ToString).SensorAType = eSensorAType
                lListPage(x.ToString).ListIO(y.ToString).SensorAXIndex = iSensorAXIndex
                lListPage(x.ToString).ListIO(y.ToString).SensorAYIndex = iSensorAYIndex
                lListPage(x.ToString).ListIO(y.ToString).SensorBType = eSensorBType
                lListPage(x.ToString).ListIO(y.ToString).SensorBXIndex = iSensorBXIndex
                lListPage(x.ToString).ListIO(y.ToString).SensorBYIndex = iSensorBYIndex
                lListPage(x.ToString).ListIO(y.ToString).OneCylinder = bOneCylinder
                lListPage(x.ToString).ListIO(y.ToString).ListLockIOA = lListLockIOA
                lListPage(x.ToString).ListIO(y.ToString).ListLockIOB = lListLockIOB
                If lListPage(x.ToString).ListIO(y.ToString).TextA = "" Then
                    lListPage(x.ToString).ListIO(y.ToString).TextA = "Cylinder" + y.ToString + ".A"
                End If
                If lListPage(x.ToString).ListIO(y.ToString).TextA2 = "" Then
                    lListPage(x.ToString).ListIO(y.ToString).TextA2 = "Cylinder" + y.ToString + ".A"
                End If

                If lListPage(x.ToString).ListIO(y.ToString).TextB = "" Then
                    lListPage(x.ToString).ListIO(y.ToString).TextB = "Cylinder" + y.ToString + ".B"
                End If
                If lListPage(x.ToString).ListIO(y.ToString).TextB2 = "" Then
                    lListPage(x.ToString).ListIO(y.ToString).TextB2 = "Cylinder" + y.ToString + ".B"
                End If
                SaveData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                For Each element As clsCylinderPageCfg In lListPage.Values

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
    Public Function GetCylinderCfgFromID(ByVal strID As String) As clsCylinderCfg
        SyncLock _Object
            Dim x As Integer = 0
            Dim y As Integer = 0
            x = CInt(strID.Split("_")(0))
            y = CInt(strID.Split("_")(1))
            Return lListPage(x.ToString).ListIO(y.ToString)
        End SyncLock
    End Function
    Public Function GetCylinderCfgFromID(ByVal strPageID As String, ByVal strID As String) As clsCylinderCfg
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
    Public Function GetCylinderCfgFromIndex(ByVal strIndex As String) As clsCylinderCfg
        SyncLock _Object
            If strIndex = "" Then Return New clsCylinderCfg(cSystemElement)
            Dim i As Integer = 1
            For Each element As clsCylinderPageCfg In lListPage.Values
                For Each subelment As clsCylinderCfg In element.ListIO.Values
                    If i = strIndex Then
                        Return subelment
                    End If
                    i = i + 1
                Next
            Next
            Return New clsCylinderCfg(cSystemElement)
        End SyncLock
    End Function

    Public Function GetCylinderPageCfgFromID(ByVal strID As String) As clsCylinderPageCfg
        SyncLock _Object
            Return lListPage(strID)
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


    Public Function MovePage(ByVal strOldID As String, ByVal strNewID As String) As Boolean
        SyncLock _Object
            Try
                Dim lNewListPage As New Dictionary(Of String, clsCylinderPageCfg)
                Dim j As Integer = 1
                For i = 1 To lListPage.Count
                    If i = strOldID Then
                        Continue For
                    End If
                    If i = strNewID Then
                        lNewListPage.Add(j, lListPage(strOldID).Clone)
                        lNewListPage(j).ID = j
                        j = j + 1
                    End If
                    lNewListPage.Add(j, lListPage(i).Clone)
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

    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim i As Integer = 1
                Dim lListValue As New List(Of String)
                For Each element As clsCylinderPageCfg In lListPage.Values
                    lListValue.Add("[Page" + i.ToString + "]")
                    lListValue.Add("Text=" + element.Text.ToString)
                    lListValue.Add("Text2=" + element.Text2.ToString)
                    Dim j As Integer = 1
                    For Each subelment As clsCylinderCfg In element.ListIO.Values
                        lListValue.Add("Item" + "_" + j.ToString + "_TextA=" + subelment.TextA)
                        lListValue.Add("Item" + "_" + j.ToString + "_TextA2=" + subelment.TextA2)
                        lListValue.Add("Item" + "_" + j.ToString + "_TextB=" + subelment.TextB)
                        lListValue.Add("Item" + "_" + j.ToString + "_TextB2=" + subelment.TextB2)
                        lListValue.Add("Item" + "_" + j.ToString + "_ReserveA=" + subelment.ReserveA.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_ReserveB=" + subelment.ReserveB.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_OneCylinder=" + subelment.OneCylinder.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_IOTriggerType=" + subelment.TriggerType.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorAType=" + subelment.SensorAType.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorAXIndex=" + subelment.SensorAXIndex.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorAYIndex=" + subelment.SensorAYIndex.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorBType=" + subelment.SensorBType.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorBXIndex=" + subelment.SensorBXIndex.ToString)
                        lListValue.Add("Item" + "_" + j.ToString + "_SensorBYIndex=" + subelment.SensorBYIndex.ToString)
                        Dim k As Integer = 1
                        For Each subsubelement As clsIOLockCfg In subelment.ListLockIOA
                            lListValue.Add("Item" + "_" + j.ToString + "_TypeNameA_" + k.ToString + "=" + subsubelement.TypeName)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexXA_" + k.ToString + "=" + subsubelement.IndexX.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexYA_" + k.ToString + "=" + subsubelement.IndexY.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_StatusA_" + k.ToString + "=" + subsubelement.Status.ToString)
                            k = k + 1
                        Next

                        k = 1
                        For Each subsubelement As clsIOLockCfg In subelment.ListLockIOB
                            lListValue.Add("Item" + "_" + j.ToString + "_TypeNameB_" + k.ToString + "=" + subsubelement.TypeName)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexXB_" + k.ToString + "=" + subsubelement.IndexX.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_IndexYB_" + k.ToString + "=" + subsubelement.IndexY.ToString)
                            lListValue.Add("Item" + "_" + j.ToString + "_StatusB_" + k.ToString + "=" + subsubelement.Status.ToString)
                            k = k + 1
                        Next
                        j = j + 1
                    Next
                    i = i + 1
                Next
                cIniHandler.SaveIniFile(cSystemManager.Settings.ProgramCylinderDebugConfig, lListValue)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class
