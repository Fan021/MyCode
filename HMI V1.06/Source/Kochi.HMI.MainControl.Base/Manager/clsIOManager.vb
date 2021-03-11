Imports Kochi.HMI.MainControl.UI
Imports Kochi.HMI.MainControl.Device
Public Class clsIOManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cSystemManager As clsSystemManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private lListPage As New Dictionary(Of String, clsIOPageCfg)
    Private lListTypeNumber As New Dictionary(Of enumIOType, Integer)


    Public ReadOnly Property ListPage As Dictionary(Of String, clsIOPageCfg)
        Get
            Return lListPage
        End Get
    End Property

    Public ReadOnly Property ListTypeNumber As Dictionary(Of enumIOType, Integer)
        Get
            Return lListTypeNumber
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
                lListTypeNumber.Clear()
                lListPage.Clear()
                Dim strTempValue As String = ""

                For Each element In [Enum].GetValues(GetType(enumIOType))
                    lListTypeNumber.Add(element, 0)
                Next
                cIniHandler.OpenIniFile(cSystemManager.Settings.IOListConfig)
                For i = 1 To cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.IO_Max_Page)
                    Dim cIOPageCfg As New clsIOPageCfg(cSystemElement)
                    cIOPageCfg.ID = i.ToString
                    strTempValue = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Type")
                    If strTempValue <> "" Then cIOPageCfg.IOType = [Enum].Parse(GetType(enumIOType), strTempValue)
                    If lListTypeNumber.ContainsKey(cIOPageCfg.IOType) Then
                        lListTypeNumber(cIOPageCfg.IOType) = lListTypeNumber(cIOPageCfg.IOType) + 1
                    End If
                    cIOPageCfg.Text = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Text")
                    cIOPageCfg.Text2 = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Text2")
                    If cIOPageCfg.Text = "" Then
                        cIOPageCfg.Text = "Group" + i.ToString
                    End If
                    If cIOPageCfg.Text2 = "" Then
                        cIOPageCfg.Text2 = "Group" + i.ToString
                    End If
                    cIOPageCfg.iXIndex = i
                    If cIOPageCfg.IOType <> enumIOType.NONE Then
                        For j = 1 To 8
                            Dim cIOCfg As New clsIOCfg(cSystemElement)
                            cIOCfg.ID = j.ToString
                            cIOCfg.PageType = cIOPageCfg.IOType
                            cIOCfg.Text = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text")
                            cIOCfg.Text2 = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Text2")
                            cIOCfg.AdsName = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_AdsName")
                            cIOCfg.XIndex = lListTypeNumber(cIOPageCfg.IOType)
                            cIOCfg.YIndex = j.ToString
                            cIOCfg.Reserve = IIf(cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Reserve") = "True", True, False)
                            strTempValue = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IOTriggerType")
                            If strTempValue <> "" Then cIOCfg.IOTriggerType = [Enum].Parse(GetType(enumIOTriggerType), strTempValue)
                            Select Case cIOCfg.PageType
                                Case enumIOType.EL1008
                                    cIOCfg.IO = New InputIO
                                    cIOCfg.AdsName = HMI_PLC_Interface.HMI_DI_EL1008_AdsName
                                Case enumIOType.EP1008
                                    cIOCfg.IO = New InputIO
                                    cIOCfg.AdsName = HMI_PLC_Interface.HMI_DI_EP1008_AdsName
                                Case enumIOType.EL2008
                                    cIOCfg.IO = New OutputIO
                                    cIOCfg.AdsName = HMI_PLC_Interface.HMI_DO_EL2008_AdsName
                            End Select
                            For k = 1 To 100
                                Dim cIOLockCfg As New clsIOLockCfg
                                Dim mTempValue As String = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_TypeName_" + k.ToString)
                                If mTempValue = "" Then Exit For
                                cIOLockCfg.TypeName = mTempValue
                                mTempValue = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexX_" + k.ToString)
                                If mTempValue = "" Then Exit For
                                cIOLockCfg.IndexX = mTempValue
                                mTempValue = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_IndexY_" + k.ToString)
                                If mTempValue = "" Then Exit For
                                cIOLockCfg.IndexY = mTempValue
                                mTempValue = cIniHandler.ReadIniFileFromOpenIni(cSystemManager.Settings.IOListConfig, "Page" + i.ToString, "Item" + "_" + j.ToString + "_Status_" + k.ToString)
                                If mTempValue = "" Then Exit For
                                cIOLockCfg.Status = mTempValue
                                cIOCfg.ListLockIO.Add(cIOLockCfg)
                            Next
                            cIOPageCfg.ListIO.Add(j.ToString, cIOCfg)
                        Next
                    End If
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
                lListTypeNumber.Clear()
                lListPage.Clear()
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                Dim strTempValue As String = ""
                For i = cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.IO_Max_Page) + 1 To 100
                    cIniHandler.RemoveSection(cSystemManager.Settings.IOListConfig, "Page" + i.ToString)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangePage(ByVal strID As String, ByVal eIOType As enumIOType, ByVal strText As String, ByVal strText2 As String) As Boolean
        SyncLock _Object
            Try
                If lListPage(strID).IOType = eIOType Then
                    lListPage(strID).Text = strText
                    lListPage(strID).Text2 = strText2
                    If lListPage(strID).Text = "" Then
                        lListPage(strID).Text = "Group" + strID.ToString
                    End If
                    If lListPage(strID).Text2 = "" Then
                        lListPage(strID).Text2 = "Group" + strID.ToString
                    End If
                Else
                    lListPage(strID).Text = strText
                    lListPage(strID).Text2 = strText2
                    If lListPage(strID).Text = "" Then
                        lListPage(strID).Text = "Group" + strID.ToString
                    End If
                    If lListPage(strID).Text2 = "" Then
                        lListPage(strID).Text2 = "Group" + strID.ToString
                    End If
                    lListPage(strID).IOType = eIOType
                    lListPage(strID).ListIO.Clear()
                    Dim j As Integer = 1
                    For i = 1 To 8
                        lListPage(strID).ListIO.Add(i.ToString, New clsIOCfg(cSystemElement))
                    Next
                    For Each element As clsIOCfg In lListPage(strID).ListIO.Values
                        element.ID = j.ToString
                        element.PageType = lListPage(strID).IOType
                        element.IOTriggerType = enumIOTriggerType.Toggle
                        Select Case element.PageType
                            Case enumIOType.EL1008
                                element.Text = "X" + (j - 1).ToString
                                element.Text2 = "X" + (j - 1).ToString
                                element.AdsName = HMI_PLC_Interface.HMI_DI_EL1008_AdsName
                                element.IO = New InputIO
                            Case enumIOType.EP1008
                                element.Text = "X" + (j - 1).ToString
                                element.Text2 = "X" + (j - 1).ToString
                                element.AdsName = HMI_PLC_Interface.HMI_DI_EP1008_AdsName
                                element.IO = New InputIO
                            Case enumIOType.EL2008
                                element.Text = "Y" + (j - 1).ToString
                                element.Text2 = "Y" + (j - 1).ToString
                                element.AdsName = HMI_PLC_Interface.HMI_DO_EL2008_AdsName
                                element.IO = New OutputIO
                        End Select
                        element.Reserve = False
                        j = j + 1
                    Next

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

    Public Function ChangeIO(ByVal strID As String, ByVal strText As String, ByVal strText2 As String, ByVal bReserve As Boolean, ByVal eIOTriggerType As enumIOTriggerType, ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
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
                SaveData()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetIOCfgFromID(ByVal strID As String) As clsIOCfg
        SyncLock _Object
            Dim x As Integer = 0
            Dim y As Integer = 0
            x = CInt(strID.Split("_")(0))
            y = CInt(strID.Split("_")(2))
            Return lListPage(x.ToString).ListIO(y.ToString)
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


    Public Function GetIOPageCfgFromID(ByVal strID As String) As clsIOPageCfg
        SyncLock _Object
            Return lListPage(strID)
        End SyncLock
    End Function

    Public Function MovePage(ByVal strOldID As String, ByVal strNewID As String) As Boolean
        SyncLock _Object
            Try

                Dim lNewListPage As New Dictionary(Of String, clsIOPageCfg)
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

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                lListTypeNumber.Clear()
                Dim iCnt As Integer = 1
                For Each element As clsIOPageCfg In lListPage.Values
                    If lListTypeNumber.ContainsKey(element.IOType) Then
                        lListTypeNumber(element.IOType) = lListTypeNumber(element.IOType) + 1
                    Else
                        lListTypeNumber.Add(element.IOType, 1)
                    End If
                    If element.IOType <> enumIOType.NONE Then
                        If element.Text = "" Then
                            element.Text = element.IOType.ToString + "_" + iCnt.ToString
                        End If
                        If element.Text2 = "" Then
                            element.Text2 = element.IOType.ToString + "_" + iCnt.ToString
                        End If
                        If element.Text.IndexOf(element.IOType.ToString + "_") >= 0 Then
                            Dim cValue() As String = element.Text.Split("_")
                            If cValue.Length = 1 Then
                                element.Text = element.IOType.ToString + "_" + iCnt.ToString
                            ElseIf cValue.Length > 1 Then
                                If IsNumeric(cValue(1)) Then
                                    element.Text = element.IOType.ToString + "_" + iCnt.ToString
                                End If
                            End If
                        End If
                        If element.Text2.IndexOf(element.IOType.ToString + "_") >= 0 Then
                            Dim cValue() As String = element.Text.Split("_")
                            If cValue.Length = 1 Then
                                element.Text2 = element.IOType.ToString + "_" + iCnt.ToString
                            ElseIf cValue.Length > 1 Then
                                If IsNumeric(cValue(1)) Then
                                    element.Text2 = element.IOType.ToString + "_" + iCnt.ToString
                                End If
                            End If
                        End If
                    Else
                        element.Text = "Group_" + iCnt.ToString
                        element.Text2 = "Group_" + iCnt.ToString
                    End If
                    iCnt = iCnt + 1
                Next

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
                cIniHandler.SaveIniFile(cSystemManager.Settings.IOListConfig, lListValue)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class

Public Enum enumIOTriggerType
    Toggle = 0
    Tap
End Enum

Public Enum enumIOType
    NONE = 0
    EL1008
    EL2008
    EP1008
End Enum


Public Class clsIOPageCfg
    Protected strID As String = ""
    Protected eIOType As enumIOType
    Protected strText As String = ""
    Protected strText2 As String = ""
    Property iXIndex As Integer = 0
    Private _Object As New Object
    Private lListIO As New Dictionary(Of String, clsIOCfg)
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager


    Public ReadOnly Property ActiveText As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SecondLanguageActive Then
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
    Public ReadOnly Property ListIO As Dictionary(Of String, clsIOCfg)
        Get
            Return lListIO
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
    Public Property IOType As enumIOType
        Set(ByVal value As enumIOType)
            SyncLock _Object
                eIOType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eIOType
            End SyncLock
        End Get
    End Property

    Public Function Clone() As clsIOPageCfg
        Dim cTempIOPageCfg As New clsIOPageCfg(cSystemElement)
        cTempIOPageCfg.Text = Me.strText
        cTempIOPageCfg.Text2 = Me.strText2
        cTempIOPageCfg.IOType = Me.eIOType
        cTempIOPageCfg.ID = Me.ID

        For Each element As clsIOCfg In Me.lListIO.Values
            cTempIOPageCfg.ListIO.Add(element.ID, element.Clone)
        Next
    
        Return cTempIOPageCfg
    End Function

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
    End Sub

End Class
Public Class clsIOCfg
    Protected strID As String = ""
    Protected strText As String = "Reserve"
    Protected strText2 As String = "Reserve"
    Protected eIOCfgType As enumIOTriggerType
    Protected bReserve As Boolean = True
    Protected iIO As IIO
    Protected iXIndex As Integer = 0
    Protected iYIndex As Integer = 0
    Protected strAdsName As String = ""
    Protected eIOType As enumIOType
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Private lListLockIO As New List(Of clsIOLockCfg)
    Private eLevel As enumUserLevel = enumUserLevel.Operator
    Public Property ListLockIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListLockIO = value
        End Set
        Get
            Return lListLockIO
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

    Public Property Level As enumUserLevel
        Set(ByVal value As enumUserLevel)
            SyncLock _Object
                eLevel = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eLevel
            End SyncLock
        End Get
    End Property

    Public Property AdsName As String
        Set(ByVal value As String)
            SyncLock _Object
                strAdsName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strAdsName
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ActiveText As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SecondLanguageActive Then
                mTempValue = strText2
                If mTempValue = "" Then
                    mTempValue = strText
                End If
            Else
                mTempValue = strText
            End If
            'If bReserve Then
            '    mTempValue = cLanguageManager.GetTextLine("ChildrenIOForm", "Reserve")
            'End If
            Return mTempValue
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
    Public Property IOTriggerType As enumIOTriggerType
        Set(ByVal value As enumIOTriggerType)
            SyncLock _Object
                eIOCfgType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eIOCfgType
            End SyncLock
        End Get
    End Property

    Public Property PageType As enumIOType
        Set(ByVal value As enumIOType)
            SyncLock _Object
                eIOType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eIOType
            End SyncLock
        End Get
    End Property

    Public Property Reserve As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bReserve = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bReserve
            End SyncLock
        End Get
    End Property

    Public Property IO As IIO
        Set(ByVal value As IIO)
            SyncLock _Object
                iIO = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iIO
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


    Public Function Clone() As clsIOCfg
        Dim cTempIOCfg As New clsIOCfg(cSystemElement)
        cTempIOCfg.Text = Me.strText
        cTempIOCfg.Text2 = Me.strText2
        cTempIOCfg.ID = Me.strID
        cTempIOCfg.iXIndex = Me.iXIndex
        cTempIOCfg.iYIndex = Me.iYIndex
        cTempIOCfg.Reserve = Me.bReserve
        cTempIOCfg.IOTriggerType = Me.eIOCfgType
        cTempIOCfg.PageType = Me.eIOType
        cTempIOCfg.AdsName = Me.strAdsName
        cTempIOCfg.IO = Me.iIO
        cTempIOCfg.Level = Me.eLevel
        For Each element As clsIOLockCfg In Me.lListLockIO
            cTempIOCfg.ListLockIO.Add(element.Clone)
        Next
        Return cTempIOCfg
    End Function

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        Me.cSystemElement = cSystemElement
        cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
    End Sub
    Sub New(ByVal strID As String, ByVal strText As String, ByVal iIO As IIO)
        Me.strID = strID
        Me.strText = strText
        Me.iIO = iIO
    End Sub
End Class

