Imports Kostal.Las.Base

Public Class clsCylinderManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private lListPage As New Dictionary(Of String, clsCylinderPageCfg)
    Private AppSettings As Settings
    Private mXmlHandler As New XmlHandler
    Public ReadOnly Property ListPage As Dictionary(Of String, clsCylinderPageCfg)
        Get
            Return lListPage
        End Get
    End Property


    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = Devices
                Me.AppSettings = MySettings
                LoadData()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoadData() As Boolean
        SyncLock _Object
            Try
                Dim sResult As String = ""
                Dim strFileName As String = AppSettings.ConfigFolder + "Cylinder.ini"
                lListPage.Clear()
                Dim strTempValue As String = ""
                cIniHandler.OpenIniFile(strFileName)
                sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ApplicationName, "GeneralInformation", "Cylinder_Max_Page")
                If sResult = "" Then
                    sResult = "1"
                End If
                For i = 1 To CInt(sResult)
                    Dim cIOPageCfg As New clsCylinderPageCfg(cSystemElement)
                    cIOPageCfg.ID = i.ToString
                    strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Type")
                    cIOPageCfg.Text = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Text")
                    cIOPageCfg.Text2 = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Text2")
                    If cIOPageCfg.Text = "" Then
                        cIOPageCfg.Text = "Group" + i.ToString
                    End If
                    If cIOPageCfg.Text2 = "" Then
                        cIOPageCfg.Text2 = "Group" + i.ToString
                    End If

                    For j = 1 To 8
                        Dim cIOCfg As New clsCylinderCfg(cSystemElement)
                        cIOCfg.ID = j.ToString
                        cIOCfg.TextA = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextA")
                        cIOCfg.TextA2 = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextA2")
                        cIOCfg.TextB = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextB")
                        cIOCfg.TextB2 = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TextB2")
                        If cIOCfg.TextA = "" Then cIOCfg.TextA = "Cylinder" + j.ToString + ".A"
                        If cIOCfg.TextA2 = "" Then cIOCfg.TextA2 = "Cylinder" + j.ToString + ".A"
                        If cIOCfg.TextB = "" Then cIOCfg.TextB = "Cylinder" + j.ToString + ".B"
                        If cIOCfg.TextB2 = "" Then cIOCfg.TextB2 = "Cylinder" + j.ToString + ".B"
                        cIOCfg.XIndex = i.ToString
                        cIOCfg.YIndex = j.ToString
                        cIOCfg.ReserveA = IIf(cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_ReserveA") = "True", True, False)
                        cIOCfg.ReserveB = IIf(cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_ReserveB") = "True", True, False)
                        cIOCfg.OneCylinder = IIf(cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_OneCylinder") = "False", False, True)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType")
                        If strTempValue <> "" Then cIOCfg.TriggerType = [Enum].Parse(GetType(enumIOTriggerType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAType")
                        If strTempValue <> "" Then cIOCfg.SensorAType = [Enum].Parse(GetType(enumIOType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBType")
                        If strTempValue <> "" Then cIOCfg.SensorBType = [Enum].Parse(GetType(enumIOType), strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAXIndex")
                        If strTempValue <> "" Then cIOCfg.SensorAXIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorAYIndex")
                        If strTempValue <> "" Then cIOCfg.SensorAYIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBXIndex")
                        If strTempValue <> "" Then cIOCfg.SensorBXIndex = CInt(strTempValue)
                        strTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_SensorBYIndex")
                        If strTempValue <> "" Then cIOCfg.SensorBYIndex = CInt(strTempValue)
                        cIOCfg.CylinderIO = New CylinderIO
                        For k = 1 To 100
                            Dim cIOLockCfg As New clsIOLockCfg
                            Dim mTempValue As String = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeNameA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.TypeName = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexXA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexX = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexYA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexY = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_StatusA_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.Status = mTempValue
                            cIOCfg.ListLockIOA.Add(cIOLockCfg)
                        Next

                        For k = 1 To 100
                            Dim cIOLockCfg As New clsIOLockCfg
                            Dim mTempValue As String = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeNameB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.TypeName = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexXB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexX = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexYB_" + k.ToString)
                            If mTempValue = "" Then Exit For
                            cIOLockCfg.IndexY = mTempValue
                            mTempValue = cIniHandler.ReadIniFileFromOpenIni(strFileName, "Page" + i.ToString, "Item" + "_" + j.ToString + "_StatusB_" + k.ToString)
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
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RemoveData(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                lListPage.Clear()
                Dim sResult As String = ""
                Dim strFileName As String = AppSettings.ConfigFolder + "Cylinder.ini"
                sResult = mXmlHandler.GetSectionInformation(AppSettings.ConfigFolder, AppSettings.ApplicationName, "GeneralInformation", "Cylinder_Max_Page")
                If sResult = "" Then
                    sResult = "1"
                End If
                Dim strTempValue As String = ""
                For i = CInt(sResult) + 1 To 100
                    cIniHandler.RemoveSection(strFileName, "Page" + i.ToString)
                Next
                Return True
            Catch ex As Exception
                Throw ex
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
                Throw ex
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
                lListPage(x.ToString).ListIO(y.ToString).ListLockIOA = lListLockIOA
                lListPage(x.ToString).ListIO(y.ToString).ListLockIOB = lListLockIOB
                lListPage(x.ToString).ListIO(y.ToString).OneCylinder = bOneCylinder

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
                Throw ex
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
                Throw ex
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

    Public Function GetCylinderPageCfgFromID(ByVal strID As String) As clsCylinderPageCfg
        SyncLock _Object
            Return lListPage(strID)
        End SyncLock
    End Function


    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim strFileName As String = AppSettings.ConfigFolder + "Cylinder.ini"
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
                cIniHandler.SaveIniFile(strFileName, lListValue)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
End Class

Public Class clsCylinderPageCfg
    Protected strID As String = ""
    Protected strText As String = ""
    Protected strText2 As String = ""
    Private _Object As New Object
    Private lListIO As New Dictionary(Of String, clsCylinderCfg)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As Language

    Public ReadOnly Property ListIO As Dictionary(Of String, clsCylinderCfg)
        Get
            Return lListIO
        End Get
    End Property

    Public ReadOnly Property ActiveText As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strText2
                If mTempValue = "" Then
                    mTempValue = strText
                End If
            Else
                mTempValue = strText
            End If
            Return mTempValue
        End Get
    End Property


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


    Public Property Text As String
        Set(ByVal value As String)
            SyncLock _Object
                strText = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strText
            End SyncLock
        End Get
    End Property

    Public Property Text2 As String
        Set(ByVal value As String)
            SyncLock _Object
                strText2 = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strText2
            End SyncLock
        End Get
    End Property


    Public Function Clone() As clsCylinderPageCfg
        Dim cTempCylinderPageCfg As New clsCylinderPageCfg(cSystemElement)
        cTempCylinderPageCfg.Text = Me.strText
        cTempCylinderPageCfg.Text2 = Me.strText2
        cTempCylinderPageCfg.ID = Me.ID
        For Each element As clsCylinderCfg In Me.lListIO.Values
            cTempCylinderPageCfg.lListIO.Add(element.ID, element.Clone)
        Next
        Return cTempCylinderPageCfg
    End Function

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
        cLanguageManager = CType(cSystemElement(Language.Name), Language)
        '   cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
    End Sub

End Class

Public Class clsCylinderCfg
    Protected strID As String = String.Empty
    Protected strTextA As String = String.Empty
    Protected strTextB As String = String.Empty
    Protected strTextA2 As String = String.Empty
    Protected strTextB2 As String = String.Empty
    Protected eSensorAType As enumIOType
    Protected eSensorBType As enumIOType
    Protected iSensorAXIndex As Integer = 0
    Protected iSensorAYIndex As Integer = 0
    Protected iSensorBXIndex As Integer = 0
    Protected iSensorBYIndex As Integer = 0
    Protected eCylinderCfgType As enumIOTriggerType
    Protected bReserveA As Boolean = True
    Protected bReserveB As Boolean = True
    Protected iCylinderIO As ICylinderIO
    Protected iXIndex As Integer = 0
    Protected iYIndex As Integer = 0
    Protected bOneCylinder As Boolean = True
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As Language
    Private lListLockIOA As New List(Of clsIOLockCfg)
    Private lListLockIOB As New List(Of clsIOLockCfg)

    Public Property ListLockIOA As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListLockIOA = value
        End Set
        Get
            Return lListLockIOA
        End Get
    End Property

    Public Property ListLockIOB As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListLockIOB = value
        End Set
        Get
            Return lListLockIOB
        End Get
    End Property

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
    Public Property TextA As String
        Set(ByVal value As String)
            SyncLock _Object
                strTextA = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strTextA
            End SyncLock
        End Get
    End Property
    Public Property TextA2 As String
        Set(ByVal value As String)
            SyncLock _Object
                strTextA2 = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strTextA2
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ActiveTextA As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strTextA2
                If mTempValue = "" Then
                    mTempValue = strTextA
                End If
            Else
                mTempValue = strTextA
            End If
            If bReserveA Then
                mTempValue = cLanguageManager.Read("ChildrenIOForm", "Reserve")
            End If
            Return mTempValue
        End Get
    End Property

    Public ReadOnly Property ActiveTextB As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strTextB2
                If mTempValue = "" Then
                    mTempValue = strTextB
                End If
            Else
                mTempValue = strTextB
            End If
            If bReserveB Then
                mTempValue = cLanguageManager.Read("ChildrenIOForm", "Reserve")
            End If
            Return mTempValue
        End Get
    End Property

    Public ReadOnly Property ActiveButtonTextB As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strTextB2
                If mTempValue = "" Then
                    mTempValue = strTextB
                End If
            Else
                mTempValue = strTextB
            End If
            If bReserveB Then
                mTempValue = "Reserve"
            End If
            If mTempValue.IndexOf(".") > 0 Then
                mTempValue = mTempValue.Substring(0, mTempValue.IndexOf("."))
            End If
            Return mTempValue
        End Get
    End Property

    Public ReadOnly Property ActiveButtonTextA As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strTextA2
                If mTempValue = "" Then
                    mTempValue = strTextA
                End If
            Else
                mTempValue = strTextA
            End If
            If bReserveA Then
                mTempValue = "Reserve"
            End If
            If mTempValue.IndexOf(".") > 0 Then
                mTempValue = mTempValue.Substring(0, mTempValue.IndexOf("."))
            End If
            Return mTempValue
        End Get
    End Property

    Public Property TextB As String
        Set(ByVal value As String)
            SyncLock _Object
                strTextB = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strTextB
            End SyncLock
        End Get
    End Property
    Public Property TextB2 As String
        Set(ByVal value As String)
            SyncLock _Object
                strTextB2 = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strTextB2
            End SyncLock
        End Get
    End Property


    Public Property SensorAType As enumIOType
        Set(ByVal value As enumIOType)
            SyncLock _Object
                eSensorAType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eSensorAType
            End SyncLock
        End Get
    End Property


    Public Property SensorBType As enumIOType
        Set(ByVal value As enumIOType)
            SyncLock _Object
                eSensorBType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eSensorBType
            End SyncLock
        End Get
    End Property

    Public Property TriggerType As enumIOTriggerType
        Set(ByVal value As enumIOTriggerType)
            SyncLock _Object
                eCylinderCfgType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eCylinderCfgType
            End SyncLock
        End Get
    End Property

    Public Property ReserveA As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bReserveA = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bReserveA
            End SyncLock
        End Get
    End Property

    Public Property ReserveB As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bReserveB = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bReserveB
            End SyncLock
        End Get
    End Property

    Public Property OneCylinder As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bOneCylinder = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bOneCylinder
            End SyncLock
        End Get
    End Property

    Public Property CylinderIO As ICylinderIO
        Set(ByVal value As ICylinderIO)
            SyncLock _Object
                iCylinderIO = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iCylinderIO
            End SyncLock
        End Get
    End Property

    Public Property XIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iXIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iXIndex
            End SyncLock
        End Get
    End Property

    Public Property YIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iYIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iYIndex
            End SyncLock
        End Get
    End Property

    Public Property SensorAXIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iSensorAXIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iSensorAXIndex
            End SyncLock
        End Get
    End Property


    Public Property SensorAYIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iSensorAYIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iSensorAYIndex
            End SyncLock
        End Get
    End Property

    Public Property SensorBXIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iSensorBXIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iSensorBXIndex
            End SyncLock
        End Get
    End Property


    Public Property SensorBYIndex As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iSensorBYIndex = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iSensorBYIndex
            End SyncLock
        End Get
    End Property

    Public Function Clone() As clsCylinderCfg
        Dim cTempCylinderCfg As New clsCylinderCfg(cSystemElement)
        cTempCylinderCfg.TextA = Me.strTextA
        cTempCylinderCfg.TextA2 = Me.strTextA2
        cTempCylinderCfg.TextB = Me.strTextB
        cTempCylinderCfg.TextB2 = Me.strTextB2
        cTempCylinderCfg.ID = Me.strID
        cTempCylinderCfg.eSensorAType = Me.eSensorAType
        cTempCylinderCfg.iSensorAXIndex = Me.iSensorAXIndex
        cTempCylinderCfg.iSensorAYIndex = Me.iSensorAYIndex
        cTempCylinderCfg.eSensorBType = Me.eSensorBType
        cTempCylinderCfg.iSensorBXIndex = Me.iSensorBXIndex
        cTempCylinderCfg.iSensorBYIndex = Me.iSensorBYIndex
        cTempCylinderCfg.iXIndex = Me.iXIndex
        cTempCylinderCfg.iYIndex = Me.iYIndex
        cTempCylinderCfg.ReserveA = Me.bReserveA
        cTempCylinderCfg.ReserveB = Me.bReserveB
        cTempCylinderCfg.OneCylinder = Me.bOneCylinder
        cTempCylinderCfg.TriggerType = Me.eCylinderCfgType
        cTempCylinderCfg.CylinderIO = Me.iCylinderIO
        For Each element As clsIOLockCfg In Me.lListLockIOA
            cTempCylinderCfg.ListLockIOA.Add(element.Clone)
        Next
        For Each element As clsIOLockCfg In Me.lListLockIOB
            cTempCylinderCfg.ListLockIOB.Add(element.Clone)
        Next
        Return cTempCylinderCfg
    End Function

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
        cLanguageManager = CType(cSystemElement(Language.Name), Language)
        ' cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
    End Sub

    Sub New(ByVal strID As String, ByVal strTextA As String, ByVal strTextB As String, ByVal iCylinderIO As ICylinderIO)
        Me.strID = strID
        Me.strTextA = strTextA
        Me.strTextB = strTextB
        Me.iCylinderIO = iCylinderIO
    End Sub
End Class




