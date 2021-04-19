Imports Kostal.Las.Base

Public Class clsIOLockManager
    Private cDebugMode As Boolean
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cIOManager As clsIOManager
    Private cCylinderManager As clsCylinderManager
    Public Const Name As String = "IOLockManager"
    Private lListIO As New List(Of clsIOLockCfg)
    Public cHMIPLC As TwinCatAds
    Private cLanguageManager As Language
    Private cTips As clsTips
    Public Property ListIO As List(Of clsIOLockCfg)
        Set(ByVal value As List(Of clsIOLockCfg))
            lListIO = value
        End Set
        Get
            Return lListIO
        End Get
    End Property

    Public Property IOManager As clsIOManager
        Set(ByVal value As clsIOManager)
            cIOManager = value
        End Set
        Get
            Return cIOManager
        End Get
    End Property
    Public Property CylinderManager As clsCylinderManager
        Set(ByVal value As clsCylinderManager)
            cCylinderManager = value
        End Set
        Get
            Return cCylinderManager
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cTips = CType(cSystemElement(clsTips.Name), clsTips)
                cLanguageManager = CType(cSystemElement(Language.Name), Language)

                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckIO(ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                For Each element As clsIOLockCfg In lListLockIO
                    If element.TypeName = "" Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "1"))
                        Return False
                    End If
                    If element.IndexX <= 0 Then
                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "2"))
                        Return False
                    End If
                    If element.IndexY <= 0 Then
                        Select Case element.TypeName
                            Case "EL1008", "EP1008", "EL2008"
                                Dim cIOPageCfg As clsIOPageCfg = cIOManager.GetIOPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "3", iCnt.ToString))
                                    Return False
                                Else
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "4", iCnt.ToString, cIOPageCfg.ActiveText))
                                    Return False
                                End If

                            Case "Cylinder"
                                Dim cIOPageCfg As clsCylinderPageCfg = cCylinderManager.GetCylinderPageCfgFromID(element.IndexX)
                                If IsNothing(cIOPageCfg) Then
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "3", iCnt.ToString))
                                    Return False
                                Else
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "4", iCnt.ToString, cIOPageCfg.ActiveText))
                                    Return False
                                End If

                        End Select
                    End If

                    If element.Status = "" Then
                        Select Case element.TypeName
                            Case "EL1008", "EP1008", "EL2008"
                                Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(element.IndexX, element.IndexY)
                                If IsNothing(cIOCfg) Then
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "3", iCnt.ToString))
                                    Return False
                                Else
                                    cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "4", iCnt.ToString))
                                    Return False
                                End If


                            Case "Cylinder"
                                Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                                If IsNothing(cIOCfg) Then
                                    ' Throw New clsHMIException(cLanguageManager.GetTextLine("IOLockManager", "1", iCnt), enumExceptionType.Alarm)
                                Else
                                    If element.IndexY Mod 2 = 0 Then
                                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "3", iCnt.ToString))
                                        Return False
                                    Else
                                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "4", iCnt.ToString))
                                        Return False
                                    End If
                                End If
                        End Select
                    End If

                    iCnt = iCnt + 1
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckLockIO(ByVal lListLockIO As List(Of clsIOLockCfg)) As Boolean
        SyncLock _Object
            Try
                Dim iCnt As Integer = 1
                Dim bReadResult As Boolean = False
                For Each element As clsIOLockCfg In lListLockIO
                    Select Case element.TypeName
                        Case "EL1008", "EP1008", "EL2008"
                            Dim cIOCfg As clsIOCfg = cIOManager.GetIOCfgFromID(element.IndexX, element.IndexY)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                bReadResult = CType(cHMIPLC.ReadAny(cIOCfg.AdsName + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "]", GetType(Boolean)), Boolean)
                                If element.Status = "ON" Then
                                    If Not bReadResult Then
                                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveText, element.Status))
                                        Return False
                                    End If
                                End If
                                If element.Status = "OFF" Then
                                    If bReadResult Then
                                        cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveText, element.Status))
                                        Return False
                                    End If
                                End If
                            End If


                        Case "Cylinder"
                            Dim cIOCfg As clsCylinderCfg = cCylinderManager.GetCylinderCfgFromID(element.IndexX, Math.Ceiling(element.IndexY / 2).ToString)
                            If IsNothing(cIOCfg) Then
                                Continue For
                            Else
                                If element.IndexY Mod 2 = 0 Then
                                    bReadResult = CType(cHMIPLC.ReadAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOB", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveTextB, element.Status))
                                            Return False
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveTextB, element.Status))
                                            Return False
                                        End If
                                    End If
                                Else
                                    bReadResult = CType(cHMIPLC.ReadAny(KostalAdsVariables.HMI_Cylinder + "[" + cIOCfg.XIndex.ToString + "," + (cIOCfg.YIndex - 1).ToString + "].bulDOA", GetType(Boolean)), Boolean)
                                    If element.Status = "ON" Then
                                        If Not bReadResult Then
                                            cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveTextA, element.Status))
                                            Return False
                                        End If
                                    End If
                                    If element.Status = "OFF" Then
                                        If bReadResult Then
                                            cTips.AddTips(cLanguageManager.LanguageElement.GetText("IOLockManager", "5", cIOCfg.ActiveTextA, element.Status))
                                            Return False
                                        End If
                                    End If
                                End If

                            End If


                    End Select


                    iCnt = iCnt + 1
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
End Class

Public Class clsIOLockCfg
    Public TypeName As String = String.Empty
    Public IndexX As Integer = -1
    Public IndexY As Integer = -1
    Public Status As String = String.Empty
    Public Function Clone() As clsIOLockCfg
        Dim cTempIOLockCfg As New clsIOLockCfg
        cTempIOLockCfg.TypeName = TypeName
        cTempIOLockCfg.IndexX = IndexX
        cTempIOLockCfg.IndexY = IndexY
        cTempIOLockCfg.Status = Status
        Return cTempIOLockCfg
    End Function

End Class