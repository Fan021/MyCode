Imports System.Reflection
Imports Kochi.HMI.MainControl.Action
Imports System.Collections.Concurrent

Public Class clsActionManager
    Private lActionList As New Dictionary(Of String, clsActionCfg)
    Private lCurrentActionList As New Dictionary(Of String, clsActionCfg)
    Private cSystemElement As Dictionary(Of String, Object)
    ' Private cLocalElement As New Dictionary(Of String, Object)
    Private cIniHandler As New clsIniHandler
    Private cFileHandler As New clsFileHandler
    Private cSystemManager As clsSystemManager
    Private cDeviceLibManger As clsDeviceLibManager
    Private cActionLibManager As clsActionLibManager
    Private cMachineManager As clsMachineManager
    Private cLanguageManager As clsLanguageManager
    Private cVariantManager As clsVariantManager
    Private cVariantCfg As clsVariantCfg
    Private _Object As New Object
    Public Const Name As String = "ActionManager"

    Public ReadOnly Property IsChanged() As Boolean
        Get
            SyncLock _Object
                Return Not Equal()
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property VariantCfg As clsVariantCfg
        Get
            SyncLock _Object
                Return cVariantCfg
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ActionList As Dictionary(Of String, clsActionCfg)
        Get
            SyncLock _Object
                Return lActionList
            End SyncLock
        End Get
    End Property

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cDeviceLibManger = CType(cSystemElement(clsDeviceLibManager.Name), clsDeviceLibManager)
                cMachineManager = CType(cSystemElement(clsMachineManager.Name), clsMachineManager)
                cActionLibManager = CType(cSystemElement(clsActionLibManager.Name), clsActionLibManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                '  cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                cVariantManager = CType(cSystemElement(clsVariantManager.Name), clsVariantManager)
                cVariantCfg = New clsVariantCfg

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function GetActionListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lActionList.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetActionCfgFromKey(ByVal strKey As String) As clsActionCfg
        SyncLock _Object
            Try
                If lActionList.ContainsKey(strKey) Then
                    Return lActionList(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetSubStepCfgList(ByVal strStationID As String, ByVal strActionType As String) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lActionList.ContainsKey(strStationID) Then
                    If IsNothing(lActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                            Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                            lListKey.Add(mSubStepCfg)
                        Next
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetSubStepListCount(ByVal strStationID As String) As Integer
        SyncLock _Object
            Try
                Dim iCnt As Integer = 0
                For Each elementType As String In lActionList(strStationID).GetStepListKey
                    For Each element As String In lActionList(strStationID).GetStepCfgFromKey(elementType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lActionList(strStationID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(element)
                        For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                            Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                            iCnt = iCnt + 1
                        Next
                    Next
                Next

                Return iCnt
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetSubStepCfgListFromCurrent(ByVal strStationID As String, ByVal strActionType As String) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                            Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                            lListKey.Add(mSubStepCfg)
                        Next
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetSubStepCfgListFromIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lActionList.ContainsKey(strStationID) Then
                    If IsNothing(lActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    Dim cMainStepCfg As clsMainStepCfg = lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        lListKey.Add(mSubStepCfg)
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function



    Public Function GetSubActionStepCfgListFromIndexAndID(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iSubStepID As String, ByVal SubActionType As String) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID) = iSubStepID.ToString And mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = SubActionType Then
                            lListKey.Add(mSubStepCfg.Clone)
                        End If
                    Next

                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function GetSubActionStepCfgListFromIndexAndIDAndEnable(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iSubStepID As String, ByVal SubActionType As String) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID) = iSubStepID.ToString And mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = SubActionType And mSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = "TRUE" Then
                            lListKey.Add(mSubStepCfg.Clone)
                        End If
                    Next

                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetSubActionStepCfgListFromIndexAndID(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iSubStepID As String) As List(Of clsSubStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsSubStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID) = iSubStepID.ToString Then
                            lListKey.Add(mSubStepCfg.Clone)
                        End If
                    Next

                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetMainStepCfgList(ByVal strStationID As String, ByVal strActionType As String) As List(Of clsMainStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsMainStepCfg)
                If lActionList.ContainsKey(strStationID) Then
                    If IsNothing(lActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        lListKey.Add(cMainStepCfg)
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMainStepCfgList(ByVal strStationID As String, ByVal strActionType As String) As List(Of clsMainStepCfg)
        SyncLock _Object
            Try

                Dim lListKey As New List(Of clsMainStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        lListKey.Add(cMainStepCfg)
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainStepCfgList(ByVal strStationID As String, ByVal strID As String, ByVal strActionType As String) As clsMainStepCfg
        SyncLock _Object
            Try

                If lActionList.ContainsKey(strStationID) Then
                    If IsNothing(lActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        If cMainStepCfg.MainStepParameter(HMIMainStepKeys.ID) = strID Then
                            Return cMainStepCfg
                        End If
                    Next
                Else
                    Return Nothing
                End If
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainStepCfgListFromCurrent(ByVal strStationID As String, ByVal strActionType As String) As List(Of clsMainStepCfg)
        SyncLock _Object
            Try
                Dim lListKey As New List(Of clsMainStepCfg)
                If lCurrentActionList.ContainsKey(strStationID) Then
                    If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                        Return Nothing
                    End If
                    For Each element As String In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                        Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                        lListKey.Add(cMainStepCfg)
                    Next
                Else
                    Return Nothing
                End If
                Return lListKey
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentActionListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lCurrentActionList.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentActionCfgFromKey(ByVal strKey As String) As clsActionCfg
        SyncLock _Object
            Try
                If lCurrentActionList.ContainsKey(strKey) Then
                    Return lCurrentActionList(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddMainStep(ByVal strStationID As String, ByVal strActionType As String) As clsMainStepCfg
        SyncLock _Object
            Try
                Dim mTempMainStepCfg As New clsMainStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If
                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1
                mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1).ToString
                mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.Name) = "Action:" + iCnt.ToString
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).AddMainStep(mTempMainStepCfg)
                ChangeID(strStationID)
                Return mTempMainStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddMainStep(ByVal strStationID As String, ByVal strActionType As String, ByVal cMainStepCfg As clsMainStepCfg) As clsMainStepCfg
        SyncLock _Object
            Try
                Dim mTempMainStepCfg As New clsMainStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If
                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1
                For Each element As String In cMainStepCfg.MainStepParameter.Keys
                    mTempMainStepCfg.MainStepParameter(element) = cMainStepCfg.MainStepParameter(element).Clone
                Next
                mTempMainStepCfg = cMainStepCfg.Clone
                mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1).ToString
                '  mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.Name) = "Action:" + iCnt.ToString

                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).AddMainStep(mTempMainStepCfg)
                ChangeID(strStationID)
                Return mTempMainStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddMainStep(ByVal strStationID As String, ByVal strActionType As String, ByVal iInsertIndex As Integer) As clsMainStepCfg
        SyncLock _Object
            Try
                Dim mTempMainStepCfg As New clsMainStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If
                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                If lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count < iInsertIndex Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "3", strStationID, iInsertIndex.ToString), enumExceptionType.Crash)
                End If
                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1
                mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count + 1).ToString
                mTempMainStepCfg.MainStepParameter(HMIMainStepKeys.Name) = "Action:" + iCnt.ToString
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).AddMainStep(iInsertIndex, mTempMainStepCfg)
                ChangeID(strStationID)
                Return mTempMainStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function DelMainStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).RemoveMainStepCfgFromKey(iMainStepIndex)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpMainStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iMainStepIndex <= 0 Then
                    'Throw New clsHMIException("MainStepIndex:" + iMainStepIndex + " is Top", enumExceptionType.Crash)
                    Return False
                End If

                Dim TopTempMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex - 1).Clone

                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).ChangeMainStepCfgFromKey(iMainStepIndex - 1, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).Clone)
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).ChangeMainStepCfgFromKey(iMainStepIndex, TopTempMainStepCfg)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DownMainStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iMainStepIndex >= lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey.Count - 1 Then
                    'Throw New clsHMIException("MainStepIndex:" + iMainStepIndex + " is Bottom", enumExceptionType.Crash)
                    Return False
                End If

                Dim TopTempMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex + 1).Clone
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).ChangeMainStepCfgFromKey(iMainStepIndex + 1, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).Clone)
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).ChangeMainStepCfgFromKey(iMainStepIndex, TopTempMainStepCfg)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal strSubActionType As String) As clsSubStepCfg
        SyncLock _Object
            Try
                Dim mTempSubStepCfg As New clsSubStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) = (iMainStepIDIndex + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = strSubActionType
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = "TRUE"
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainType) = strActionType
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).AddSubStep(mTempSubStepCfg)
                ChangeID(strStationID)
                Return mTempSubStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal cSubStepCfg As clsSubStepCfg) As clsSubStepCfg
        SyncLock _Object
            Try
                Dim mTempSubStepCfg As New clsSubStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1
                For Each element As String In cSubStepCfg.SubStepParameter.Keys
                    mTempSubStepCfg.SubStepParameter(element) = cSubStepCfg.SubStepParameter(element).Clone
                Next
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) = (iMainStepIDIndex + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainType) = strActionType
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).AddSubStep(mTempSubStepCfg)
                ChangeID(strStationID)
                Return mTempSubStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iInsertIndex As Integer, ByVal strSubActionType As String, ByVal bCheck As Boolean) As clsSubStepCfg
        SyncLock _Object
            Try
                Dim mTempSubStepCfg As New clsSubStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) = (iMainStepIDIndex + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = bCheck.ToString.ToUpper
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = strSubActionType
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainType) = strActionType
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).AddSubStep(iInsertIndex, mTempSubStepCfg)
                ChangeID(strStationID)
                Return mTempSubStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iInsertIndex As Integer, ByVal cSubStepCfg As clsSubStepCfg) As clsSubStepCfg
        SyncLock _Object
            Try
                Dim mTempSubStepCfg As New clsSubStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1
                For Each element As String In cSubStepCfg.SubStepParameter.Keys
                    mTempSubStepCfg.SubStepParameter(element) = cSubStepCfg.SubStepParameter(element).Clone
                Next
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1).ToString
                '    mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) = (iMainStepIDIndex + 1).ToString

                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).AddSubStep(iInsertIndex, mTempSubStepCfg)
                ChangeID(strStationID)
                Return mTempSubStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iInsertIndex As Integer, ByVal cSubStepCfg As clsSubStepCfg, ByVal strSubActionType As String) As clsSubStepCfg
        SyncLock _Object
            Try
                Dim mTempSubStepCfg As New clsSubStepCfg(cSystemElement)
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Dim iCnt As Integer = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1
                For Each element As String In cSubStepCfg.SubStepParameter.Keys
                    mTempSubStepCfg.SubStepParameter(element) = cSubStepCfg.SubStepParameter(element).Clone
                Next
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID) = (lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).SubStepCount + 1).ToString
                '    mTempSubStepCfg.SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + mTempSubStepCfg.SubStepParameter(HMISubStepKeys.ID)
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.MainID) = (iMainStepIDIndex + 1).ToString
                mTempSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = strSubActionType
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).AddSubStep(iInsertIndex, mTempSubStepCfg)
                ChangeID(strStationID)
                Return mTempSubStepCfg
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function
    Public Function DelSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iDelIndex As Integer) As Boolean
        SyncLock _Object
            Try
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).RemoveSubStepCfgFromKey(iDelIndex)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DelAllSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIDIndex As Integer, ByVal iDelIndex As Integer) As Boolean
        SyncLock _Object
            Try
                '查找主SubAction
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).GetSubStepCfgFromKey(iDelIndex)
                Dim lListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIDIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))
                If Not IsNothing(lListSubAciton) Then
                    Dim iCnt As Integer = 0
                    For Each element As clsSubStepCfg In lListSubAciton
                        lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIDIndex).RemoveSubStepCfgFromKey(element.SubStepParameter(HMISubStepKeys.KeyID) - iCnt)
                        iCnt = iCnt + 1
                    Next
                End If
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iSubStepIndex <= 0 Then
                    'Throw New clsHMIException("SubStepIndex:" + iSubStepIndex + " is Top", enumExceptionType.Crash)
                    Return False
                End If
                Dim TopTempSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex - 1).Clone
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(iSubStepIndex - 1, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex).Clone)
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(iSubStepIndex, TopTempSubStepCfg)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function UpAllSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                '获取当前
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
                Dim iMoveCnt As Integer = 0
                Dim iPreCnt As Integer = 0
                Dim iFailCnt As Integer = 0

                '获取Pre Action数量
                Dim lPreListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.PreSubAction.ToString)
                If Not IsNothing(lPreListSubAciton) Then
                    iPreCnt = lPreListSubAciton.Count
                End If

                Dim cTopSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex - iPreCnt - 1)
                Dim lTopListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cTopSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID))
                If Not IsNothing(lTopListSubAciton) Then
                    iMoveCnt = lTopListSubAciton.Count
                End If

                Dim lListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))
                If Not IsNothing(lListSubAciton) Then
                    For k = 0 To lListSubAciton.Count - 1
                        For i = 1 To iMoveCnt
                            Dim TopTempSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) - i).Clone
                            lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) - i, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) - i + 1).Clone)
                            lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) - i + 1, TopTempSubStepCfg)
                        Next
                    Next
                End If
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeAllSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal strModify As String) As Boolean
        SyncLock _Object
            Try
                '获取当前
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
                If IsNothing(lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType)) Then
                    Return Nothing
                End If
                Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex)
                For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                    Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                    If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID) = cSubStepCfg.SubStepParameter(HMISubStepKeys.ID).ToString Then
                        mSubStepCfg.SubStepParameter(HMISubStepKeys.Enable) = strModify
                    End If
                Next
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function PastAllSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal strInsertActionType As String, ByVal iMainStepIndex As Integer, ByVal iInsertMainStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal iInsertIndex As Integer) As Boolean
        SyncLock _Object
            Try
                '获取当前
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)

                Dim lListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))
                Dim iCnt As Integer = 0
                If Not IsNothing(lListSubAciton) Then
                    For Each element As clsSubStepCfg In lListSubAciton
                        AddSubStepByIndex(strStationID, strInsertActionType, iInsertMainStepIndex, iInsertIndex + iCnt, element)
                        iCnt = iCnt + 1
                    Next
                End If
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function PastSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal strInsertActionType As String, ByVal iMainStepIndex As Integer, ByVal iMainInsertStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal iInsertIndex As Integer, ByVal SubActionType As String) As Boolean
        SyncLock _Object
            Try
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
                AddSubStepByIndex(strStationID, strInsertActionType, iMainInsertStepIndex, iInsertIndex, cSubStepCfg, SubActionType)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function PastPreSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal strInsertActionType As String, ByVal iMainStepIndex As Integer, ByVal iMainInsertStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal iInsertIndex As Integer, ByVal SubActionType As String, ByVal PastSubActionType As String) As Boolean
        SyncLock _Object
            Try
                '查找主SubAction
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
                Dim lListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID), SubActionType)
                If Not IsNothing(lListSubAciton) Then
                    Dim iCnt As Integer = 0
                    For Each element As clsSubStepCfg In lListSubAciton
                        AddSubStepByIndex(strStationID, strInsertActionType, iMainInsertStepIndex, iInsertIndex + iCnt, element, PastSubActionType)
                        ChangeID(strStationID)
                        iCnt = iCnt + 1
                    Next
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function PastAllPreSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal strInsertActionType As String, ByVal iMainStepIndex As Integer, ByVal iMainInsertStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal iInsertIndex As Integer, ByVal SubActionType As String, ByVal PastSubActionType As String) As Boolean
        SyncLock _Object
            Try
                '查找主SubAction
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)

                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If
                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If


                Dim lListAddSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID), SubActionType)

                Dim lListSubSubAction As New List(Of clsSubStepCfg)
                '获取所有类型相同的SubAction
                Dim strMainSubType As String = ""
                For Each element As String In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                    Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = SubActionType Then
                            '获取主Sub类型
                            Dim mMainSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepFromID(mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID))
                            If mMainSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType) = cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType) Then
                                lListSubSubAction.Add(mSubStepCfg)
                            End If
                        End If
                    Next
                Next

                '删除旧的
                For Each element As clsSubStepCfg In lListSubSubAction
                    lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepFromID(element.SubStepParameter(HMISubStepKeys.MainID)).RemoveSubStepCfgFromKey(element.SubStepParameter(HMISubStepKeys.KeyID))
                    ChangeID(strStationID)
                Next


                '获取需要添加的主Key
                Dim iCnt As Integer = 0
                Dim lListAddMainSubAction As New List(Of clsSubStepCfg)
                For Each element As String In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                    Dim cMainStepCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(element)
                    For Each elementsubStep As String In cMainStepCfg.GetSubStepListKey
                        Dim mSubStepCfg As clsSubStepCfg = cMainStepCfg.GetSubStepCfgFromKey(elementsubStep)
                        If mSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionType) = "SubAction" And mSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType) = cSubStepCfg.SubStepParameter(HMISubStepKeys.ActionType) Then
                            lListAddMainSubAction.Add(mSubStepCfg.Clone)

                        End If
                    Next
                Next
                '添加新的
                iCnt = 0
                Dim strOldMainId As String = ""
                For Each element As clsSubStepCfg In lListAddMainSubAction
                    If element.SubStepParameter(HMISubStepKeys.MainID) <> strOldMainId Then
                        iCnt = 0
                        strOldMainId = element.SubStepParameter(HMISubStepKeys.MainID)
                    End If
                    Dim lSubActionPass As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, element.SubStepParameter(HMISubStepKeys.MainID) - 1, element.SubStepParameter(HMISubStepKeys.SubActionID) + iCnt, enumSubActionType.SubActionPass.ToString)
                    For Each addelement As clsSubStepCfg In lListAddSubAciton
                        Select Case PastSubActionType
                            Case enumSubActionType.PreSubAction.ToString
                                AddSubStepByIndex(strStationID, strInsertActionType, element.SubStepParameter(HMISubStepKeys.MainID) - 1, element.SubStepParameter(HMISubStepKeys.KeyID) + iCnt - 1, addelement, PastSubActionType)
                            Case enumSubActionType.SubActionPass.ToString
                                AddSubStepByIndex(strStationID, strInsertActionType, element.SubStepParameter(HMISubStepKeys.MainID) - 1, element.SubStepParameter(HMISubStepKeys.KeyID) + iCnt, addelement, PastSubActionType)
                            Case enumSubActionType.SubActionFailure.ToString
                                '获取SubActionPass 数量.
                                AddSubStepByIndex(strStationID, strInsertActionType, element.SubStepParameter(HMISubStepKeys.MainID) - 1, element.SubStepParameter(HMISubStepKeys.KeyID) + iCnt + lSubActionPass.Count, addelement, PastSubActionType)
                        End Select
                        iCnt = iCnt + 1
                        ChangeID(strStationID)
                    Next
                Next

                'If Not IsNothing(lListSubAciton) Then
                '    Dim iCnt As Integer = 0
                '    For Each element As clsSubStepCfg In lListSubAciton
                '        AddSubStepByIndex(strStationID, strInsertActionType, iMainInsertStepIndex, iInsertIndex + iCnt, element, PastSubActionType)
                '        ChangeID(strStationID)
                '        iCnt = iCnt + 1
                '    Next
                'End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DownSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If iSubStepIndex >= lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepListKey.Count - 1 Then
                    '  Throw New clsHMIException("SubStepIndex:" + iSubStepIndex + " is Bottom", enumExceptionType.Crash)
                    Return False
                End If
                Dim BottomTempSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex + 1).Clone
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(iSubStepIndex + 1, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex).Clone)
                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(iSubStepIndex, BottomTempSubStepCfg)
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DownAllSubStepByIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                Dim iMoveCnt As Integer = 0
                Dim iPassCnt As Integer = 0
                Dim iFailCnt As Integer = 0
                '获取当前
                Dim cSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
                Dim lPassListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionPass.ToString)
                Dim lFailListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionFailure.ToString)

                If Not IsNothing(lPassListSubAciton) Then
                    iPassCnt = lPassListSubAciton.Count
                End If
                If Not IsNothing(lFailListSubAciton) Then
                    iFailCnt = lFailListSubAciton.Count
                End If

                Dim cDownSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex + iFailCnt + iPassCnt + 1)
                Dim lDownListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cDownSubStepCfg.SubStepParameter(HMISubStepKeys.SubActionID))
                If Not IsNothing(lDownListSubAciton) Then
                    iMoveCnt = lDownListSubAciton.Count
                End If

                Dim lListSubAciton As List(Of clsSubStepCfg) = GetSubActionStepCfgListFromIndexAndID(strStationID, strActionType, iMainStepIndex, cSubStepCfg.SubStepParameter(HMISubStepKeys.ID))
                If Not IsNothing(lListSubAciton) Then
                    For k = lListSubAciton.Count - 1 To 0 Step -1
                        For i = 1 To iMoveCnt
                            Dim BottomTempSubStepCfg As clsSubStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) + i).Clone
                            lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) + i, lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) + i - 1).Clone)
                            lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).ChangeSubStepCfgFromKey(lListSubAciton(k).SubStepParameter(HMISubStepKeys.KeyID) + i - 1, BottomTempSubStepCfg)
                        Next
                    Next
                End If
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function GetCurrentMainStepFromIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As clsMainStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                Return lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentSubStepFromID(ByVal strStationID As String, ByVal strActionType As String, ByVal strSubStepID As String) As clsSubStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                For Each elementIndex As Integer In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                    Dim element As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(elementIndex)
                    If element.HasSubStepID(strSubStepID) Then
                        Return element.GetSubStepFromID(strSubStepID)
                    End If
                Next
                Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "5", strSubStepID), enumExceptionType.Crash)
                Return Nothing
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentSubStepFromIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As clsSubStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                Return lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetSubStepFromIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer) As clsSubStepCfg
        SyncLock _Object
            Try
                If Not lActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                If Not lActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepIndex(iMainStepIndex) Then
                    Return Nothing
                End If
                If Not lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).HasSubStepIndex(iSubStepIndex) Then
                    Return Nothing
                End If
                Return lActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentMainStepFromName(ByVal strStationID As String, ByVal strActionType As String, ByVal strMainStepName As String) As clsMainStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepName(strMainStepName) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "6", strMainStepName), enumExceptionType.Crash)
                End If
                Return lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepFromName(strMainStepName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function




    Public Function GetCurrentMainStepFromID(ByVal strStationID As String, ByVal strActionType As String, ByVal strMainStepIndex As String) As clsMainStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                Return lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepFromID(strMainStepIndex)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentSubStepFromName(ByVal strStationID As String, ByVal strActionType As String, ByVal strMainStepName As String, ByVal strSubStepName As String) As clsSubStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                If Not GetCurrentMainStepFromName(strStationID, strActionType, strMainStepName).HasSubStepName(strSubStepName) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "6", strMainStepName), enumExceptionType.Crash)
                End If
                Return GetCurrentMainStepFromName(strStationID, strActionType, strMainStepName).GetSubStepFromName(strSubStepName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetCurrentSubStepFromID(ByVal strStationID As String, ByVal strActionType As String, ByVal strMainStepID As String, ByVal strSubStepID As String) As clsSubStepCfg
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                Return GetCurrentMainStepFromIndex(strStationID, strActionType, strMainStepID).GetSubStepFromID(strSubStepID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function HasMainStepIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If Not lActionList.ContainsKey(strStationID) Then
                    Return False
                End If

                If Not lActionList(strStationID).HasKey(strActionType) Then
                    Return False
                End If
                If Not lActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepIndex(iMainStepIndex) Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasCurrentMainStepIndex(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Return False
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Return False
                End If
                If Not lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepIndex(iMainStepIndex) Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasCurrentMainStepName(ByVal strStationID As String, ByVal strActionType As String, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                If lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepName(strName) Then
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

    Public Function HasCurrentMainStepName(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Alarm)
                End If
                If lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).MainStepParameter(HMIMainStepKeys.Name) = strName Then
                    Return False
                End If
                If lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).HasMainStepName(strName) Then
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

    Public Function HasCurrentSubStepName(ByVal strStationID As String, ByVal strActionType As String, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                For Each elementIndex As Integer In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                    Dim element As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(elementIndex)
                    If element.HasSubStepName(strName) Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasCurrentSubStepName(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If
                If lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex).SubStepParameter(HMISubStepKeys.Name) = strName Then
                    Return False
                End If

                For Each elementIndex As Integer In lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepListKey
                    Dim element As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(elementIndex)
                    If element.HasSubStepName(strName) Then
                        Return True
                    End If
                Next
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeCurrentMainParameter(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal strKey As String, ByVal strModifyName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).MainStepParameter(strKey) = strModifyName
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeCurrentSubParameter(ByVal strStationID As String, ByVal strActionType As String, ByVal iMainStepIndex As Integer, ByVal iSubStepIndex As Integer, ByVal strKey As String, ByVal strModifyName As String) As Boolean
        SyncLock _Object
            Try
                If Not lCurrentActionList.ContainsKey(strStationID) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "1", strStationID), enumExceptionType.Crash)
                End If

                If Not lCurrentActionList(strStationID).HasKey(strActionType) Then
                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "2", strStationID, strActionType), enumExceptionType.Crash)
                End If

                lCurrentActionList(strStationID).GetStepCfgFromKey(strActionType).GetMainStepCfgFromKey(iMainStepIndex).GetSubStepCfgFromKey(iSubStepIndex).SubStepParameter(strKey) = strModifyName
                ChangeID(strStationID)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID(ByVal strStationID As String) As Boolean
        SyncLock _Object
            Try
                Dim iIndex As Integer = 1
                Dim lListPreSub As New List(Of clsSubStepCfg)
                Dim lLastSubID As Integer = 0
                For Each element As String In lCurrentActionList(strStationID).GetStepListKey

                    Dim j As Integer = 1
                    For i = 0 To lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepListKey.Count - 1
                        lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).MainStepParameter(HMIMainStepKeys.ID) = (i + 1).ToString
                        If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).MainStepParameter(HMIMainStepKeys.Name).IndexOf("Action:") = 0 Then
                            If IsNumeric(lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).MainStepParameter(HMIMainStepKeys.Name).Replace("Action:", "")) Then
                                lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).MainStepParameter(HMIMainStepKeys.Name) = "Action:" + (i + 1).ToString
                            End If
                        End If

                        lLastSubID = 0
                        For k = 0 To lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepListKey.Count - 1
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.KeyID) = k.ToString
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.ID) = j.ToString
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.TotalID) = iIndex.ToString
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.MainID) = (i + 1).ToString
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.FailNextID) = ""
                            lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.PassNextID) = ""
                            If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.Name).IndexOf("SubAction:") = 0 Then
                                If IsNumeric(lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.Name).Replace("SubAction:", "")) Then
                                    lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.Name) = "SubAction:" + j.ToString
                                End If
                            End If

                            If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.PreSubAction.ToString Then
                                lListPreSub.Add(lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k))
                            End If

                            If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubActionFailure.ToString Or lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubActionPass.ToString Then
                                lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionID) = lLastSubID
                            End If

                            If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                lLastSubID = lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.ID)
                                lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionID) = lLastSubID
                                For Each elementlist As clsSubStepCfg In lListPreSub
                                    elementlist.SubStepParameter(HMISubStepKeys.SubActionID) = lLastSubID
                                Next
                                lListPreSub.Clear()
                            End If
                            j = j + 1
                            iIndex = iIndex + 1
                        Next
                    Next
                Next

                Dim lListPreSubtionSub As New List(Of clsSubStepCfg)
                Dim lListPassSubtionSub As New List(Of clsSubStepCfg)
                Dim lListFailSubtionSub As New List(Of clsSubStepCfg)
                For Each element As String In lCurrentActionList(strStationID).GetStepListKey
                    For i = 0 To lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepListKey.Count - 1
                        For k = 0 To lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepListKey.Count - 1
                            If lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.SubActionType) = enumSubActionType.SubAction.ToString Then
                                lListPreSubtionSub = GetSubActionStepCfgListFromIndexAndIDAndEnable(strStationID, element, i, lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.ID), enumSubActionType.PreSubAction.ToString)
                                lListPassSubtionSub = GetSubActionStepCfgListFromIndexAndIDAndEnable(strStationID, element, i, lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionPass.ToString)
                                lListFailSubtionSub = GetSubActionStepCfgListFromIndexAndIDAndEnable(strStationID, element, i, lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.ID), enumSubActionType.SubActionFailure.ToString)
                                If Not IsNothing(lListFailSubtionSub) AndAlso lListFailSubtionSub.Count > 0 Then
                                    lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.FailNextID) = lListFailSubtionSub(0).SubStepParameter(HMISubStepKeys.KeyID)
                                ElseIf Not IsNothing(lListPreSubtionSub) AndAlso lListPreSubtionSub.Count > 0 Then
                                    lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.FailNextID) = lListPreSubtionSub(0).SubStepParameter(HMISubStepKeys.KeyID)
                                Else
                                    lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.FailNextID) = lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.KeyID)
                                End If

                                If Not IsNothing(lListFailSubtionSub) AndAlso lListFailSubtionSub.Count > 0 Then
                                    If Not IsNothing(lListPreSubtionSub) AndAlso lListPreSubtionSub.Count > 0 Then
                                        lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(lListFailSubtionSub(lListFailSubtionSub.Count - 1).SubStepParameter(HMISubStepKeys.KeyID)).SubStepParameter(HMISubStepKeys.PassNextID) = lListPreSubtionSub(0).SubStepParameter(HMISubStepKeys.KeyID)
                                    Else
                                        lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(lListFailSubtionSub(lListFailSubtionSub.Count - 1).SubStepParameter(HMISubStepKeys.KeyID)).SubStepParameter(HMISubStepKeys.PassNextID) = lCurrentActionList(strStationID).GetStepCfgFromKey(element).GetMainStepCfgFromKey(i).GetSubStepCfgFromKey(k).SubStepParameter(HMISubStepKeys.KeyID)
                                    End If
                                End If
                            End If
                        Next
                    Next
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

    Public Function LoadActionCfg(ByVal strVariant As String, Optional ByVal strCopyVariant As String = "") As Boolean
        SyncLock _Object
            Try
                Dim cActionCfg As clsActionCfg
                Dim cMainStepCfg As clsMainStepCfg
                Dim cSubStepCfg As clsSubStepCfg
                Dim cLocalElement As New Dictionary(Of String, Object)
                Dim iIndex As Integer = 1
                lCurrentActionList.Clear()
                If strCopyVariant = "" Then
                    cVariantCfg.Variant = strVariant
                End If
                If strCopyVariant <> "" Then
                    strVariant = strCopyVariant
                End If
                If strCopyVariant = "" Then
                    lActionList.Clear()
                End If
                If Not cLocalElement.ContainsKey(clsActionManager.Name) Then
                    cLocalElement.Add(clsActionManager.Name, Me)
                End If
                If Not cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                    cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
                End If
                cIniHandler.OpenIniFile(cVariantManager.GetProgramPath(strVariant))
                For Each elementIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementIndex)
                    If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                        cLocalElement(clsMachineStationCfg.Name) = cMachineStationCfg.Clone
                    Else
                        cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg.Clone)
                    End If
                    cActionCfg = New clsActionCfg(strVariant, cSystemElement)
                    For Each elementType As String In cActionCfg.GetStepListKey
                        For Each elementObject As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromOpenIni(cVariantManager.GetProgramPath(strVariant), "Station" + element.Index.ToString + "_" + elementType + "_MainStep", HMIMainStepKeys.GetUserDefinedKeys.ToArray)
                            cMainStepCfg = New clsMainStepCfg(cSystemElement)
                            For Each elementKey As String In HMIMainStepKeys.GetUserDefinedKeys.ToArray
                                If CType(elementObject(elementKey), String) <> clsXmlHandler.s_DEFAULT And CType(elementObject(elementKey), String) <> clsXmlHandler.s_Null Then
                                    cMainStepCfg.MainStepParameter(elementKey) = CType(elementObject(elementKey), String)
                                Else
                                    cMainStepCfg.MainStepParameter(elementKey) = ""
                                End If
                                If elementKey = "Enable" Then
                                    If cMainStepCfg.MainStepParameter(elementKey) = "" Then
                                        cMainStepCfg.MainStepParameter(elementKey) = "TRUE"
                                    End If
                                End If
                            Next
                            cActionCfg.GetStepCfgFromKey(elementType).AddMainStep(cMainStepCfg)
                        Next
                        For Each elementObject As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromOpenIni(cVariantManager.GetProgramPath(strVariant), "Station" + element.Index.ToString + "_" + elementType + "_SubStep", HMISubStepKeys.GetUserDefinedKeys.ToArray)
                            cSubStepCfg = New clsSubStepCfg(cSystemElement)
                            For Each elementKey As String In HMISubStepKeys.GetUserDefinedKeys.ToArray
                                If CType(elementObject(elementKey), String) <> clsXmlHandler.s_DEFAULT And CType(elementObject(elementKey), String) <> clsXmlHandler.s_Null Then
                                    cSubStepCfg.SubStepParameter(elementKey) = CType(elementObject(elementKey), String)
                                Else
                                    cSubStepCfg.SubStepParameter(elementKey) = ""
                                End If

                                If elementKey = HMISubStepKeys.SubActionType Then
                                    If cSubStepCfg.SubStepParameter(elementKey) = "" Then
                                        cSubStepCfg.SubStepParameter(elementKey) = enumSubActionType.SubAction.ToString
                                    End If
                                End If

                                If elementKey = HMISubStepKeys.MainType Then
                                    If cSubStepCfg.SubStepParameter(elementKey) = "" Then
                                        cSubStepCfg.SubStepParameter(elementKey) = elementType.ToString
                                    End If
                                End If

                                If elementKey = HMISubStepKeys.TotalID Then
                                    If cSubStepCfg.SubStepParameter(elementKey) = "" Then
                                        cSubStepCfg.SubStepParameter(elementKey) = iIndex.ToString
                                    End If
                                End If
                                If elementKey = HMISubStepKeys.Enable Then
                                    If cSubStepCfg.SubStepParameter(elementKey) = "" Then
                                        cSubStepCfg.SubStepParameter(elementKey) = "TRUE"
                                    End If
                                End If
                            Next
                            cActionCfg.GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(CInt(cSubStepCfg.SubStepParameter(HMISubStepKeys.MainID)) - 1).AddSubStep(cSubStepCfg)
                            iIndex = iIndex + 1
                        Next
                    Next

                    lCurrentActionList(element.ID) = cActionCfg.Clone

                Next


                ChangeIniToParameter()
                ChangeAutoParameter()
                If strCopyVariant = "" Then
                    Clone(lCurrentActionList, lActionList)
                End If

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function



    Public Function CopyActionCfg(ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                LoadActionCfg("", strVariant)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CopyStationCfg(ByVal strID As String, ByVal strCopyName As String) As Boolean
        SyncLock _Object
            Try
                Dim strCopyID As String = ""
                strCopyID = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromName(strCopyName).ID
                lCurrentActionList(strID) = lCurrentActionList(strCopyID).Clone
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ExchangeStationCfg(ByVal strOriginName As String, ByVal strTargetName As String) As Boolean
        SyncLock _Object
            Try
                Dim strOriginID As String = ""
                Dim strTargetID As String = ""
                strOriginID = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromName(strOriginName).ID
                strTargetID = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromName(strTargetName).ID
                Dim mTemp As clsActionCfg
                mTemp = lCurrentActionList(strTargetID).Clone
                lCurrentActionList(CInt(strTargetID)) = lCurrentActionList(CInt(strOriginID)).Clone
                lCurrentActionList(CInt(strOriginID)) = mTemp.Clone
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function SaveCurrentActionCfg(ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                Dim lListValue As New List(Of String)
                ChangeParameterToIni()
                If cFileHandler.FileExist(cVariantManager.GetProgramPath(strVariant)) Then
                    'cFileHandler.DelectFile(cVariantManager.GetProgramPath(strVariant))
                End If
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            lListValue.Add("[Station" + element.Index.ToString + "_" + elementType + "_MainStep" + elementMainCfg.MainStepParameter(HMIMainStepKeys.ID) + "]")
                            Dim cMainName() As String = HMIMainStepKeys.GetUserDefinedKeys.ToArray()
                            Dim cMainValue() As String = elementMainCfg.MainStepParameter.Values.ToArray
                            For i = 0 To cMainName.Length - 1
                                lListValue.Add(cMainName(i) + "=" + cMainValue(i))
                            Next
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                elementSubCfg.SubStepParameter(HMISubStepKeys.Picture) = clsSystemPath.ToIniPath(elementSubCfg.SubStepParameter(HMISubStepKeys.Picture))
                                Dim cValue() As String = elementSubCfg.SubStepParameter.Values.ToArray
                                cValue(7) = clsSystemPath.ToIniPath(cValue(7))
                                lListValue.Add("[Station" + element.Index.ToString + "_" + elementType + "_SubStep" + elementSubCfg.SubStepParameter(HMISubStepKeys.ID) + "]")
                                Dim cName() As String = HMISubStepKeys.GetUserDefinedKeys.ToArray()
                                For i = 0 To cName.Length - 1
                                    lListValue.Add(cName(i) + "=" + cValue(i))
                                Next
                            Next
                        Next
                    Next
                Next
                cIniHandler.SaveIniFile(cVariantManager.GetProgramPath(strVariant), lListValue)
                LoadActionCfg(strVariant)
                Return True
            Catch ex As Exception
                LoadActionCfg(strVariant)
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function SaveAsCurrentActionCfg(ByVal strVariant As String) As Boolean
        SyncLock _Object
            Try
                Dim lListValue As New List(Of String)
                ChangeParameterToIni()
                If cFileHandler.FileExist(cVariantManager.GetProgramPath(strVariant)) Then
                    '  cFileHandler.DelectFile(cVariantManager.GetProgramPath(strVariant))
                End If
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            lListValue.Add("[Station" + element.Index.ToString + "_" + elementType + "_MainStep" + elementMainCfg.MainStepParameter(HMIMainStepKeys.ID) + "]")
                            Dim cMainName() As String = HMIMainStepKeys.GetUserDefinedKeys.ToArray()
                            Dim cMainValue() As String = elementMainCfg.MainStepParameter.Values.ToArray
                            For i = 0 To cMainName.Length - 1
                                lListValue.Add(cMainName(i) + "=" + cMainValue(i))
                            Next
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                elementSubCfg.SubStepParameter(HMISubStepKeys.Picture) = clsSystemPath.ToIniPath(elementSubCfg.SubStepParameter(HMISubStepKeys.Picture))
                                Dim cValue() As String = elementSubCfg.SubStepParameter.Values.ToArray
                                cValue(7) = clsSystemPath.ToIniPath(cValue(7))
                                lListValue.Add("[Station" + element.Index.ToString + "_" + elementType + "_SubStep" + elementSubCfg.SubStepParameter(HMISubStepKeys.ID) + "]")
                                Dim cName() As String = HMISubStepKeys.GetUserDefinedKeys.ToArray()
                                For i = 0 To cName.Length - 1
                                    lListValue.Add(cName(i) + "=" + cValue(i))
                                Next
                            Next
                        Next
                    Next
                Next
                cIniHandler.SaveIniFile(cVariantManager.GetProgramPath(strVariant), lListValue)
                ChangeIniToParameter()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CheckCurrentActionCfg() As Boolean
        SyncLock _Object
            Try
                Dim cLocalElement As New Dictionary(Of String, Object)
                If cLocalElement.ContainsKey(clsActionManager.Name) Then
                    cLocalElement(clsActionManager.Name) = Me
                Else
                    cLocalElement.Add(clsActionManager.Name, Me)
                End If
                If cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                    cLocalElement(clsVariantCfg.Name) = cVariantCfg
                Else
                    cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
                End If
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                        cLocalElement(clsMachineStationCfg.Name) = cMachineStationCfg.Clone
                    Else
                        cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg.Clone)
                    End If
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            If elementMainCfg.MainStepParameter(HMIMainStepKeys.Name) = "" Then
                                Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "7", element.ID, elementType, elementMainCfg.MainStepParameter(HMIMainStepKeys.ID)), enumExceptionType.Alarm)
                            End If
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                If elementSubCfg.SubStepParameter(HMISubStepKeys.Name) = "" Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "8", element.ID, elementType, elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), enumExceptionType.Alarm)
                                End If
                                'If elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Picture,cLocalElement ) = "" Then
                                '    Throw New clsHMIException("Station:" + element.ID + " Type:" + elementType + " SubID:" + elementSubCfg.SubStepParameter(HMISubStepKeys.ID) + " Picture is Null", enumExceptionType.Alarm)
                                'End If
                                'If elementSubCfg.ChangedSubStepParameter(HMISubStepKeys.Component,cLocalElement ) = "" Then
                                '    Throw New clsHMIException("Station:" + element.ID + " Type:" + elementType + " SubID:" + elementSubCfg.SubStepParameter(HMISubStepKeys.ID) + " Component is Null", enumExceptionType.Alarm)
                                'End If
                                If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "" Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "9", element.ID, elementType, elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), enumExceptionType.Alarm)
                                End If
                                If elementSubCfg.SubStepParameter(HMISubStepKeys.Repeat) = "" Then
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "10", element.ID, elementType, elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), enumExceptionType.Alarm)
                                End If

                                If IsNothing(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType))) Then
                                    elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter) = ""
                                    Continue For
                                End If
                                If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                                    CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
                                End If
                                Try
                                    If Not CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI.CheckParameter(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter))) Then
                                        Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "11", element.ID, elementType, elementSubCfg.SubStepParameter(HMISubStepKeys.ID)), enumExceptionType.Alarm)
                                    End If
                                Catch ex1 As Exception
                                    Throw New clsHMIException(cLanguageManager.GetTextLine("ActionManager", "12", element.ID, elementType, elementSubCfg.SubStepParameter(HMISubStepKeys.ID), ex1.Message), enumExceptionType.Alarm)
                                End Try
                            Next
                        Next
                    Next
                Next


                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Alarm)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeParameterToIni() As Boolean
        SyncLock _Object
            Try
                Dim cLocalElement As New Dictionary(Of String, Object)
                If cLocalElement.ContainsKey(clsActionManager.Name) Then
                    cLocalElement(clsActionManager.Name) = Me
                Else
                    cLocalElement.Add(clsActionManager.Name, Me)
                End If
                If cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                    cLocalElement(clsVariantCfg.Name) = cVariantCfg
                Else
                    cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
                End If
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                        cLocalElement(clsMachineStationCfg.Name) = cMachineStationCfg.Clone
                    Else
                        cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg.Clone)
                    End If
                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                If cLocalElement.ContainsKey(clsSubStepCfg.Name) Then
                                    cLocalElement(clsSubStepCfg.Name) = elementSubCfg
                                Else
                                    cLocalElement.Add(clsSubStepCfg.Name, elementSubCfg)
                                End If
                                If IsNothing(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType))) Then
                                    elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter) = ""
                                    Continue For
                                End If
                                If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                                    CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
                                End If
                                Dim lListParameter As New List(Of String)
                                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI.ChangeParameterToIni(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter)), lListParameter)
                                elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter) = clsParameter.ToString(lListParameter)
                            Next
                        Next
                    Next
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function HasAction(ByVal strStationID As String, ByVal strActionType As String, ByVal TotalID As Integer, ByVal iDeviceIndex As Integer, ByVal iDeviceID As String) As Boolean
        SyncLock _Object
            Try
                For Each elementType As String In lCurrentActionList(strStationID).GetStepListKey
                    For Each elementIndex As Integer In lCurrentActionList(strStationID).GetStepCfgFromKey(elementType).GetMainStepListKey
                        Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(strStationID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                        For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                            Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                            If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = strActionType Then
                                If elementSubCfg.SubStepParameter(HMISubStepKeys.TotalID) > TotalID And elementSubCfg.SubStepParameter(HMISubStepKeys.Enable).ToUpper = "TRUE" Then
                                    Dim c As List(Of String) = clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter))
                                    If c.Count >= iDeviceID + 1 Then
                                        If c(iDeviceIndex).ToString = iDeviceID Then
                                            Return True
                                        End If
                                    End If

                                End If
                            End If
                        Next
                    Next
                Next
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function ChangeIniToParameter() As Boolean
        SyncLock _Object
            Try
                Dim cLocalElement As New Dictionary(Of String, Object)
                If cLocalElement.ContainsKey(clsActionManager.Name) Then
                    cLocalElement(clsActionManager.Name) = Me
                Else
                    cLocalElement.Add(clsActionManager.Name, Me)
                End If
                If cLocalElement.ContainsKey(clsVariantCfg.Name) Then
                    cLocalElement(clsVariantCfg.Name) = cVariantCfg
                Else
                    cLocalElement.Add(clsVariantCfg.Name, cVariantCfg)
                End If
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                        cLocalElement(clsMachineStationCfg.Name) = cMachineStationCfg.Clone
                    Else
                        cLocalElement.Add(clsMachineStationCfg.Name, cMachineStationCfg.Clone)
                    End If

                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                If cLocalElement.ContainsKey(clsSubStepCfg.Name) Then
                                    cLocalElement(clsSubStepCfg.Name) = elementSubCfg
                                Else
                                    cLocalElement.Add(clsSubStepCfg.Name, elementSubCfg)
                                End If

                                elementSubCfg.SubStepParameter(HMISubStepKeys.Picture) = clsSystemPath.ToSystemPath(elementSubCfg.SubStepParameter(HMISubStepKeys.Picture))

                                If IsNothing(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType))) Then
                                    elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter) = ""
                                    Continue For
                                End If
                                If IsNothing(CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI) Then
                                    CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).CreateActionUI(cLocalElement, cSystemElement)
                                End If
                                Dim lListParameter As New List(Of String)
                                CType(cActionLibManager.GetActionLibCfgFromKey(elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType)).Source, clsHMIActionBase).ActionUI.ChangeIniToParameter(cLocalElement, cSystemElement, clsParameter.ToList(elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter)), lListParameter)
                                elementSubCfg.SubStepParameter(HMISubStepKeys.Parameter) = clsParameter.ToString(lListParameter)
                            Next
                        Next
                    Next
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function ChangeAutoParameter() As Boolean
        SyncLock _Object
            Try
                Dim bAutoMode As Boolean = False
                Dim iAutoModeSubIndex As Integer = 0
                Dim iAutoModeSubTargerIndex As Integer = 0
                Dim iAutoModeMainIndex As Integer = 0
                Dim iCnt As Integer = 0
                For Each elementCellIndex As Integer In cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationListKey
                    Dim cMachineStationCfg As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    If cMachineStationCfg.MachineStationType = enumMachineStationType.Manual Then Continue For

                    Dim element As clsMachineStationCfg = cMachineManager.MachineCellManager.MachineCellCfg.GetMachineStationCfgFromKey(elementCellIndex)
                    For Each elementType As String In lCurrentActionList(element.ID).GetStepListKey
                        For Each elementIndex As Integer In lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepListKey
                            Dim elementMainCfg As clsMainStepCfg = lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepCfgFromKey(elementIndex)
                            For Each elementSubIndex As Integer In elementMainCfg.GetSubStepListKey
                                Dim elementSubCfg As clsSubStepCfg = elementMainCfg.GetSubStepCfgFromKey(elementSubIndex)
                                If elementSubCfg.SubStepParameter(HMISubStepKeys.ActionType) = "AutoStationScrew" And elementSubCfg.SubStepParameter(HMISubStepKeys.Enable) = "TRUE" Then
                                    '小于100 步继续计算
                                    If iCnt <= 99 Then
                                        If Not bAutoMode Then
                                            '记录起始Action位置
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.AutoMode) = "TRUE"
                                            iAutoModeSubIndex = elementSubCfg.SubStepParameter(HMISubStepKeys.ID)
                                            iAutoModeMainIndex = elementMainCfg.MainStepParameter(HMIMainStepKeys.ID)
                                            iAutoModeSubTargerIndex = elementSubCfg.SubStepParameter(HMISubStepKeys.TotalID)
                                            bAutoMode = True
                                        Else
                                            iAutoModeSubTargerIndex = elementSubCfg.SubStepParameter(HMISubStepKeys.TotalID)
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.AutoMode) = "FALSE"
                                        End If
                                    Else
                                        '大约100 步继续计算
                                        If bAutoMode Then
                                            lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepFromID(iAutoModeMainIndex).GetSubStepFromID(iAutoModeSubIndex).SubStepParameter(HMISubStepKeys.TargetNumber) = iAutoModeSubTargerIndex.ToString
                                            bAutoMode = False
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.AutoMode) = "TRUE"
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.TargetNumber) = elementSubCfg.SubStepParameter(HMISubStepKeys.TotalID)
                                        Else
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.AutoMode) = "TRUE"
                                            elementSubCfg.SubStepParameter(HMISubStepKeys.TargetNumber) = elementSubCfg.SubStepParameter(HMISubStepKeys.TotalID)
                                        End If
                                    End If

                                Else
                                    '下一个回圈更新
                                    If bAutoMode Then
                                        lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepFromID(iAutoModeMainIndex).GetSubStepFromID(iAutoModeSubIndex).SubStepParameter(HMISubStepKeys.TargetNumber) = iAutoModeSubTargerIndex.ToString
                                        bAutoMode = False
                                    End If
                                    elementSubCfg.SubStepParameter(HMISubStepKeys.AutoMode) = "FALSE"
                                End If
                                iCnt = iCnt + 1
                            Next
                        Next
                        If bAutoMode Then
                            lCurrentActionList(element.ID).GetStepCfgFromKey(elementType).GetMainStepFromID(iAutoModeMainIndex).GetSubStepFromID(iAutoModeSubIndex).SubStepParameter(HMISubStepKeys.TargetNumber) = iAutoModeSubTargerIndex.ToString
                            bAutoMode = False
                        End If
                    Next
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function CancelCurrentActionCfg() As Boolean
        SyncLock _Object
            Try
                Clone(lActionList, lCurrentActionList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function Equal() As Boolean
        SyncLock _Object
            Try
                If lActionList.Count <> lCurrentActionList.Count Then
                    Return False
                End If
                For i = 0 To lCurrentActionList.Count - 1
                    If Not lActionList.ContainsKey(lCurrentActionList.Keys(i)) Then
                        Return False
                    End If
                    If lActionList(lCurrentActionList.Keys(i)) <> lCurrentActionList(lCurrentActionList.Keys(i)) Then
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

    Public Function Clone(ByRef SrcList As Dictionary(Of String, clsActionCfg), ByRef TarList As Dictionary(Of String, clsActionCfg)) As Boolean
        SyncLock _Object
            Try
                TarList.Clear()
                For i = 0 To SrcList.Count - 1
                    TarList.Add(SrcList.Keys(i), SrcList(SrcList.Keys(i)).Clone)
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class


Public Enum enumActionType
    PreAction = 0
    Action
    AfterAction
    PreActionPass
    PreActionFailure
    ActionPass
    ActionFailure
    AfterActionPass
    AfterActionFailure
    RetryAction
    PreActionEndFailure
    ActionEndFailure
    AfterActionEndFailure
End Enum

Public Enum enumSubActionType
    PreSubAction = 0
    SubAction
    SubActionPass
    SubActionFailure
End Enum

Public Class clsActionCfg
    Private lStepList As New Dictionary(Of String, clsStepCfg)
    Private _Object As New Object
    Private Shared _Object2 As New Object
    Private strVariant As String
    Private cGlobalProgramManager As clsGlobalProgramManager
    Private cSystemElement As Dictionary(Of String, Object)

    Sub New(ByVal strVariant As String, ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.cSystemElement = cSystemElement
            cGlobalProgramManager = CType(cSystemElement(clsGlobalProgramManager.Name), clsGlobalProgramManager)
            If cGlobalProgramManager.HasGlobalProgram(strVariant) Then
                Dim cTempStepCfg As New clsStepCfg
                lStepList.Add(enumActionType.PreActionPass.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.PreActionFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.ActionPass.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.ActionFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.AfterActionPass.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.AfterActionFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.PreActionEndFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.ActionEndFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.AfterActionEndFailure.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.RetryAction.ToString, cTempStepCfg)
               
            Else
                Dim cTempStepCfg As New clsStepCfg
                lStepList.Add(enumActionType.PreAction.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.Action.ToString, cTempStepCfg)
                cTempStepCfg = New clsStepCfg
                lStepList.Add(enumActionType.AfterAction.ToString, cTempStepCfg)
            End If

            Me.strVariant = strVariant
        End SyncLock
    End Sub

    Public Function GetStepListKey() As List(Of String)
        SyncLock _Object
            Try
                Return lStepList.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetStepCfgFromKey(ByVal strKey As String) As clsStepCfg
        SyncLock _Object
            Try
                If lStepList.ContainsKey(strKey) Then
                    Return lStepList(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function HasKey(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lStepList.ContainsKey(strKey) Then
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

    Public Function Clone() As clsActionCfg
        SyncLock _Object
            Dim cTempActionCfg As New clsActionCfg(strVariant, cSystemElement)
            For Each element As String In lStepList.Keys
                cTempActionCfg.lStepList(element) = lStepList(element).Clone
            Next
            Return cTempActionCfg
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsActionCfg, ByVal y As clsActionCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            If x.lStepList.Count <> y.lStepList.Count Then
                Return True
            End If
            For Each element As String In y.lStepList.Keys
                If x.lStepList(element) <> y.lStepList(element) Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsActionCfg, ByVal y As clsActionCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            If x.lStepList.Count <> y.lStepList.Count Then
                Return False
            End If
            For Each element As String In y.lStepList.Keys
                If x.lStepList(element) <> y.lStepList(element) Then
                    Return False
                End If
            Next
            Return True
        End SyncLock
    End Operator
End Class

Public Class clsStepCfg
    Private lMainStepList As New List(Of clsMainStepCfg)
    Private _Object As New Object
    Private Shared _Object2 As New Object

    Public ReadOnly Property MainStepList As List(Of clsMainStepCfg)
        Get
            SyncLock _Object
                Return lMainStepList
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property SubStepCount As Integer
        Get
            SyncLock _Object
                Return GetSubStepCount()
            End SyncLock
        End Get
    End Property

    Sub New()
        SyncLock _Object
        End SyncLock
    End Sub

    Public Function GetMainStepListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lMainStepList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveMainStepCfgFromKey(ByVal iKey As Integer) As Boolean
        SyncLock _Object
            Try
                lMainStepList.RemoveAt(iKey)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function



    Public Function HasMainStepID(ByVal strMainStepID As String) As Boolean
        SyncLock _Object
            Try
                Return lMainStepList.Any(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = strMainStepID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function HasMainStepIndex(ByVal strMainStepIndex As String) As Boolean
        SyncLock _Object
            Try
                If strMainStepIndex < lMainStepList.Count Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function HasMainStepName(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                Return lMainStepList.Any(Function(e) e.MainStepParameter(HMIMainStepKeys.Name) = strName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function GetMainStepFromID(ByVal strMainStepID As String) As clsMainStepCfg
        SyncLock _Object
            Try
                Return lMainStepList.Where(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = strMainStepID).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainStepFromName(ByVal strMainStepName As String) As clsMainStepCfg
        SyncLock _Object
            Try
                Return lMainStepList.Where(Function(e) e.MainStepParameter(HMIMainStepKeys.Name) = strMainStepName).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetMainStepCfgFromKey(ByVal iKey As Integer) As clsMainStepCfg
        SyncLock _Object
            Try
                If iKey <= lMainStepList.Count - 1 Then
                    Return lMainStepList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function ChangeMainStepCfgFromKey(ByVal iKey As Integer, ByVal TempMainStepCfg As clsMainStepCfg) As Boolean
        SyncLock _Object
            Try
                lMainStepList(iKey) = TempMainStepCfg
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddMainStep(ByVal cMainStepCfg As clsMainStepCfg) As Boolean
        SyncLock _Object
            Try
                lMainStepList.Add(cMainStepCfg)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddMainStep(ByVal iInsertID As Integer, ByVal cMainStepCfg As clsMainStepCfg) As Boolean
        SyncLock _Object
            Dim lTempMainStepList As New List(Of clsMainStepCfg)
            For i = 0 To iInsertID
                lTempMainStepList.Add(lMainStepList(i).Clone)
            Next
            lTempMainStepList.Add(cMainStepCfg)
            For i = iInsertID To lMainStepList.Count - 1
                lTempMainStepList.Add(lMainStepList(i).Clone)
            Next
            lMainStepList.Clear()
            For i = 0 To lTempMainStepList.Count - 1
                lMainStepList.Add(lTempMainStepList(i).Clone)
            Next
            Return True
        End SyncLock
    End Function

    Protected Function GetSubStepCount() As Integer
        SyncLock _Object
            Dim iCount As Integer = 0
            For Each element As clsMainStepCfg In lMainStepList
                iCount = iCount + element.GetSubStepListKey.Count
            Next
            Return iCount
        End SyncLock
    End Function

    Public Function Clone() As clsStepCfg
        SyncLock _Object
            Dim cTempStep As New clsStepCfg
            For Each element As clsMainStepCfg In lMainStepList
                cTempStep.lMainStepList.Add(element.Clone)
            Next
            Return cTempStep
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsStepCfg, ByVal y As clsStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            If x.GetMainStepListKey.Count <> y.GetMainStepListKey.Count Then
                Return True
            End If
            Dim elementMainStep As clsMainStepCfg
            For Each elementMainStep In y.lMainStepList
                If Not x.lMainStepList.Any(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) Then
                    Return True
                End If
                If x.lMainStepList.Where(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = elementMainStep.MainStepParameter(HMIMainStepKeys.ID)).First() <> elementMainStep Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsStepCfg, ByVal y As clsStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            If x.GetMainStepListKey.Count <> y.GetMainStepListKey.Count Then
                Return False
            End If
            Dim elementMainStep As clsMainStepCfg
            For Each elementMainStep In y.lMainStepList
                If Not x.lMainStepList.Any(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = elementMainStep.MainStepParameter(HMIMainStepKeys.ID)) Then
                    Return False
                End If
                If x.lMainStepList.Where(Function(e) e.MainStepParameter(HMIMainStepKeys.ID) = elementMainStep.MainStepParameter(HMIMainStepKeys.ID)).First() <> elementMainStep Then
                    Return False
                End If
            Next
            Return True
        End SyncLock
    End Operator

End Class

Public Class HMIMainStepKeys
    <MainStepAttribute()> Public Const ID As String = "ID"
    <MainStepAttribute()> Public Const Enable As String = "Enable"
    <MainStepAttribute()> Public Const Name As String = "Name"
    <MainStepAttribute()> Public Const Description As String = "Description"
    <MainStepAttribute()> Public Const Description2 As String = "Description2"
    <MainStepAttribute()> Public Const ShowDetail As String = "ShowDetail"
    Protected Shared _keylist As List(Of String)
    Private Shared _Object As New Object
    Shared Sub New()
        _keylist = New List(Of String)
        ScanList()
    End Sub

    Public Shared Function GetUserDefinedKeys() As IEnumerable(Of String)
        SyncLock _Object
            Return _keylist
        End SyncLock
    End Function

    Protected Shared Sub ScanList()
        SyncLock _Object
            Dim t As Type = GetType(HMIMainStepKeys)
            Dim sp As FieldInfo() = t.GetFields()
            For Each f As FieldInfo In sp
                If f.IsDefined(GetType(MainStepAttribute), True) Then
                    Try
                        Dim o As Object = f.GetValue(Nothing)
                        _keylist.Add(o.ToString())
                    Catch ex As Exception
                        Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
                    End Try
                End If
            Next
        End SyncLock
    End Sub

    Protected Class MainStepAttribute : Inherits Attribute
        Public Sub New()
            SyncLock _Object
            End SyncLock
        End Sub
    End Class

End Class


Public Class clsMainStepCfg
    Protected lSubStepList As New List(Of clsSubStepCfg)
    Protected lMainStepParameter As New Dictionary(Of String, String)
    Protected cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Protected cTextManager As clsTextManager
    Private _Object As New Object
    Private Shared _Object2 As New Object
    Public Const Name As String = "MainStepCfg"
    Private Shared cLocalVariant As clsVariantManager
    Private cPictureManager As clsPictureManager
    Private cGlobalProgramManager As clsGlobalProgramManager
    Public ReadOnly Property MainStepParameter As Dictionary(Of String, String)
        Get
            SyncLock _Object
                Return lMainStepParameter
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property SubStepList As List(Of clsSubStepCfg)
        Get
            SyncLock _Object
                Return lSubStepList
            End SyncLock
        End Get
    End Property


    Public Function ChangedMainStepParameter(ByVal strKey As String, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        SyncLock _Object
            Dim elementValue As String = lMainStepParameter(strKey).ToString
            Do While elementValue.IndexOf("[") >= 0 And elementValue.IndexOf("]") >= 0
                Dim strElementKey As String = elementValue.Substring(elementValue.IndexOf("[") + 1, elementValue.IndexOf("]") - elementValue.IndexOf("[") - 1)
                Dim mReplaceValue As String = strElementKey
                Dim bFind As Boolean = False
                If strElementKey.IndexOf("Variant_") = 0 Then
                    mReplaceValue = GetVariantValue(strKey, strElementKey, bFind, cLocalElement)
                ElseIf strElementKey.IndexOf("TextList_") = 0 Then
                    mReplaceValue = GetTextListValue(strElementKey, bFind, cLocalElement)
                ElseIf strElementKey.IndexOf("PictureList_") = 0 Then
                    mReplaceValue = GetPictureListValue(strElementKey, bFind, cLocalElement)
                Else
                    mReplaceValue = GetValue(strElementKey, bFind, cLocalElement)
                End If

                If bFind Then
                    elementValue = elementValue.Substring(0, elementValue.IndexOf("[")) + mReplaceValue + elementValue.Substring(elementValue.IndexOf("]") + 1)
                Else
                    elementValue = elementValue.Substring(0, elementValue.IndexOf("[")) + "$!" + mReplaceValue + "!$" + elementValue.Substring(elementValue.IndexOf("]") + 1)
                End If
            Loop
            elementValue = elementValue.Replace("$!", "[").Replace("!$", "]")
            If strKey = HMISubStepKeys.Parameter Then
                Dim cListValue As List(Of String) = clsParameter.ToList(elementValue)
                Dim cNewListValue As New List(Of String)
                For Each element As String In cListValue
                    If element.IndexOf("-") > 0 Then
                        element = element.Split("-")(0)
                    End If
                    cNewListValue.Add(element)
                Next
                elementValue = clsParameter.ToString(cNewListValue)
            End If
            Return elementValue
        End SyncLock
    End Function
    Private Function GetValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        If cLocalVariant.CurrentVariantCfg.ListElement.ContainsKey(strElementKey) Then
            elementValue = cLocalVariant.CurrentVariantCfg.ListElement(strElementKey)
            bFind = True
        ElseIf cTextManager.HasText(strElementKey) Then
            elementValue = cTextManager.GetTextCfgFromKey(strElementKey).ActiveMessage
            bFind = True
        ElseIf cPictureManager.HasPicture(strElementKey) Then
            elementValue = cPictureManager.GetPictureCfgFromName(strElementKey).Path
            bFind = True
        End If
        Return elementValue
    End Function
    Private Function GetVariantValue(ByVal strKey As String, ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim cVariantCfg As New clsVariantCfg
        If cLocalElement.ContainsKey(clsVariantManager.Name) Then
            cVariantCfg = CType(cLocalElement(clsVariantManager.Name), clsVariantManager).CurrentVariantCfg
        Else
            cVariantCfg = cLocalElement(clsVariantCfg.Name)
        End If
        If cLocalVariant.CurrentVariantCfg.Variant <> cVariantCfg.Variant Then
            If Not cGlobalProgramManager.HasGlobalProgram(cVariantCfg.Variant) Then
                If Not cLocalVariant.HasVariant(cVariantCfg.Variant) Then
                    cLocalVariant.LoadVariantCfg()
                End If
                cLocalVariant.ChangeVariant(cVariantCfg.Variant)
            End If
        End If
        If cLocalVariant.CurrentVariantCfg.Variant = "" Then
            Return lMainStepParameter(strKey).ToString
        End If
        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(8)
        If strElementKey = "Variant" Then
            elementValue = cLocalVariant.CurrentVariantCfg.Variant
            bFind = True
        ElseIf strElementKey = "SFC" Then
            If cSystemElement.ContainsKey(clsMachineStatusManager.Name) Then
                If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                    Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
                    Dim cMachineStatusManager As clsMachineStatusManager = cSystemElement(clsMachineStatusManager.Name)
                    elementValue = cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.SFC
                Else
                    elementValue = cLocalVariant.CurrentVariantCfg.SFC
                End If
            Else
                elementValue = cLocalVariant.CurrentVariantCfg.SFC
            End If
            bFind = True
        ElseIf strElementKey = "Picture" Then
            elementValue = cLocalVariant.CurrentVariantCfg.PicturePath
            bFind = True
        Else
            If cLocalVariant.CurrentVariantCfg.ListElement.ContainsKey(strElementKey) Then
                elementValue = cLocalVariant.CurrentVariantCfg.ListElement(strElementKey)
                bFind = True
            End If
        End If
        Return elementValue
    End Function

    Private Function GetTextListValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(9)
        If cTextManager.HasText(strElementKey) Then
            elementValue = cTextManager.GetTextCfgFromKey(strElementKey).ActiveMessage
            bFind = True
        End If
        Return elementValue
    End Function

    Private Function GetPictureListValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(9)
        If cPictureManager.HasPicture(strElementKey) Then
            elementValue = cPictureManager.GetPictureCfgFromName(strElementKey).Path
            bFind = True
        End If
        Return elementValue
    End Function



    Public Function ActiveDescription(ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim mTmepValue As String = ""
        If cLanguageManager.SecondLanguageActive Then
            mTmepValue = ChangedMainStepParameter(HMIMainStepKeys.Description2, cLocalElement)
            If mTmepValue = "" Then
                mTmepValue = ChangedMainStepParameter(HMIMainStepKeys.Description, cLocalElement)
            End If
        Else
            mTmepValue = ChangedMainStepParameter(HMIMainStepKeys.Description, cLocalElement)
        End If

        Do While mTmepValue.IndexOf("[") >= 0 And mTmepValue.IndexOf("]") >= 0
            Dim strKey As String = mTmepValue.Substring(mTmepValue.IndexOf("[") + 1, mTmepValue.IndexOf("]") - mTmepValue.IndexOf("[") - 1)
            If cTextManager.HasText(strKey) Then
                mTmepValue = mTmepValue.Replace("[" + strKey + "]", cTextManager.GetTextCfgFromKey(strKey).ActiveMessage)
            Else
                mTmepValue = mTmepValue.Replace("[", "$!").Replace("]", "!$")
            End If
        Loop
        mTmepValue = mTmepValue.Replace("$!", "[").Replace("!$", "]")
        Return mTmepValue

    End Function

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.cSystemElement = cSystemElement
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
            cTextManager = cSystemElement(clsTextManager.Name)
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
            cTextManager = cSystemElement(clsTextManager.Name)
            cPictureManager = cSystemElement(clsPictureManager.Name)
            cGlobalProgramManager = cSystemElement(clsGlobalProgramManager.Name)
            For Each element As String In HMIMainStepKeys.GetUserDefinedKeys
                lMainStepParameter.Add(element, "")
            Next
            If IsNothing(cLocalVariant) Then
                cLocalVariant = New clsVariantManager
                cLocalVariant.Init(cSystemElement)
                cLocalVariant.LoadVariantCfg()
            End If
        End SyncLock
    End Sub

    Public Function GetSubStepListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lSubStepList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveSubStepCfgFromKey(ByVal iKey As Integer) As Boolean
        SyncLock _Object
            Try
                lSubStepList.RemoveAt(iKey)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function HasSubStepID(ByVal strSubStepID As String) As Boolean
        SyncLock _Object
            Try
                Return lSubStepList.Any(Function(e) e.SubStepParameter(HMISubStepKeys.ID) = strSubStepID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function HasSubStepIndex(ByVal strSubStepIndex As String) As Boolean
        SyncLock _Object
            Try
                If strSubStepIndex < lSubStepList.Count Then
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function HasSubStepName(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                Return lSubStepList.Any(Function(e) e.SubStepParameter(HMISubStepKeys.Name) = strName)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return True
            End Try
        End SyncLock
    End Function

    Public Function GetSubStepFromID(ByVal strSubStepID As String) As clsSubStepCfg
        SyncLock _Object
            Try
                Return lSubStepList.Where(Function(e) e.SubStepParameter(HMISubStepKeys.ID) = strSubStepID).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetSubStepFromName(ByVal strSubStepName As String) As clsSubStepCfg
        SyncLock _Object
            Try
                Return lSubStepList.Where(Function(e) e.SubStepParameter(HMISubStepKeys.Name) = strSubStepName).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetSubStepCfgFromKey(ByVal iKey As Integer) As clsSubStepCfg
        SyncLock _Object
            Try
                If iKey <= lSubStepList.Count - 1 Then
                    Return lSubStepList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function ChangeSubStepCfgFromKey(ByVal iKey As Integer, ByVal TempSubStepCfg As clsSubStepCfg) As Boolean
        SyncLock _Object
            Try
                lSubStepList(iKey) = TempSubStepCfg
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function AddSubStep(ByVal cSubStepCfg As clsSubStepCfg) As Boolean
        SyncLock _Object
            Try
                lSubStepList.Add(cSubStepCfg)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddSubStep(ByVal iInsertID As Integer, ByVal cSubStepCfg As clsSubStepCfg) As Boolean
        SyncLock _Object
            Dim lTempSubStepList As New List(Of clsSubStepCfg)
            For i = 0 To iInsertID
                lTempSubStepList.Add(lSubStepList(i).Clone)
            Next
            lTempSubStepList.Add(cSubStepCfg)
            For i = iInsertID + 1 To lSubStepList.Count - 1
                lTempSubStepList.Add(lSubStepList(i).Clone)
            Next
            lSubStepList.Clear()
            For i = 0 To lTempSubStepList.Count - 1
                lSubStepList.Add(lTempSubStepList(i).Clone)
            Next
            Return True
        End SyncLock
    End Function

    Public Function Clone() As clsMainStepCfg
        SyncLock _Object
            Dim cTemp As New clsMainStepCfg(cSystemElement)
            For Each element As String In HMIMainStepKeys.GetUserDefinedKeys
                cTemp.MainStepParameter(element) = lMainStepParameter(element)
            Next
            For Each element As clsSubStepCfg In lSubStepList
                cTemp.AddSubStep(element.Clone)
            Next
            Return cTemp
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsMainStepCfg, ByVal y As clsMainStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False

            For Each element As String In HMIMainStepKeys.GetUserDefinedKeys
                If x.MainStepParameter(element) <> y.MainStepParameter(element) Then
                    Return True
                End If
            Next

            If x.GetSubStepListKey.Count <> y.GetSubStepListKey.Count Then
                Return True
            End If
            For Each elementSubIndex In y.GetSubStepListKey
                Dim elementSubStep As clsSubStepCfg = y.GetSubStepCfgFromKey(elementSubIndex)
                If Not x.HasSubStepID(elementSubStep.SubStepParameter(HMISubStepKeys.ID)) Then
                    Return True
                End If
                If x.GetSubStepFromID(elementSubStep.SubStepParameter(HMISubStepKeys.ID)) <> elementSubStep Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsMainStepCfg, ByVal y As clsMainStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False

            For Each element As String In HMIMainStepKeys.GetUserDefinedKeys
                If x.MainStepParameter(element) <> y.MainStepParameter(element) Then
                    Return False
                End If
            Next

            If x.GetSubStepListKey.Count <> y.GetSubStepListKey.Count Then
                Return False
            End If
            For Each elementSubIndex In y.GetSubStepListKey
                Dim elementSubStep As clsSubStepCfg = y.GetSubStepCfgFromKey(elementSubIndex)
                If x.GetSubStepFromID(elementSubStep.SubStepParameter(HMISubStepKeys.ID)) <> elementSubStep Then
                    Return False
                End If
            Next
            Return True
        End SyncLock
    End Operator

End Class


Public Class HMISubStepKeys
    <SubStepAttribute()> Public Const ID As String = "ID"
    <SubStepAttribute()> Public Const KeyID As String = "KeyID"
    <SubStepAttribute()> Public Const TotalID As String = "TotalID"
    <SubStepAttribute()> Public Const Enable As String = "Enable"
    <SubStepAttribute()> Public Const MainID As String = "MainID"
    <SubStepAttribute()> Public Const MainType As String = "MainType"
    <SubStepAttribute()> Public Const Name As String = "Name"
    <SubStepAttribute()> Public Const Description As String = "Description"
    <SubStepAttribute()> Public Const Description2 As String = "Description2"
    <SubStepAttribute()> Public Const Component As String = "Component"
    <SubStepAttribute()> Public Const Picture As String = "Picture"
    <SubStepAttribute()> Public Const Repeat As String = "Repeat"
    <SubStepAttribute()> Public Const ActionType As String = "ActionType"
    <SubStepAttribute()> Public Const Parameter As String = "Parameter"
    <SubStepAttribute()> Public Const AutoMode As String = "AutoMode"
    <SubStepAttribute()> Public Const TargetNumber As String = "TargetNumber"
    <SubStepAttribute()> Public Const SubActionType As String = "SubActionType"
    <SubStepAttribute()> Public Const SubActionID As String = "SubActionID"
    <SubStepAttribute()> Public Const FailNextID As String = "FailNextID"
    <SubStepAttribute()> Public Const PassNextID As String = "PassNextID"
    Protected Shared _keylist As List(Of String)
    Private Shared _Object As New Object

    Shared Sub New()
        SyncLock _Object
            _keylist = New List(Of String)
            ScanList()
        End SyncLock
    End Sub

    Public Shared Function GetUserDefinedKeys() As IEnumerable(Of String)
        SyncLock _Object

            Return _keylist

        End SyncLock
    End Function

    Protected Shared Sub ScanList()
        SyncLock _Object
            Dim t As Type = GetType(HMISubStepKeys)
            Dim sp As FieldInfo() = t.GetFields()
            For Each f As FieldInfo In sp
                If f.IsDefined(GetType(SubStepAttribute), True) Then
                    Try
                        Dim o As Object = f.GetValue(Nothing)
                        _keylist.Add(o.ToString())
                    Catch ex As Exception
                        Throw New clsHMIException(ex.Message, enumExceptionType.Crash)
                    End Try
                End If
            Next
        End SyncLock
    End Sub

    Protected Class SubStepAttribute : Inherits Attribute
        Public Sub New()
            SyncLock _Object
            End SyncLock
        End Sub
    End Class

End Class

Public Class clsSubStepCfg
    Protected lSubStepParameter As New Dictionary(Of String, String)
    Private _Object As New Object
    Private Shared _Object2 As New Object
    Protected cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As clsLanguageManager
    Protected cTextManager As clsTextManager
    Public Const Name As String = "SubStepCfg"
    Protected lSubStepList As New List(Of clsMainStepCfg)
    Private Shared cLocalVariant As clsVariantManager
    Private cPictureManager As clsPictureManager
    Private cGlobalProgramManager As clsGlobalProgramManager
    Public ReadOnly Property SubStepParameter As Dictionary(Of String, String)
        Get
            SyncLock _Object

                Return lSubStepParameter
            End SyncLock
        End Get
    End Property

   
    Public Function ChangedSubStepParameter(ByVal strKey As String, ByVal cLocalElement As Dictionary(Of String, Object)) As String

        Try
            SyncLock _Object
                Dim elementValue As String = lSubStepParameter(strKey).ToString
                Do While elementValue.IndexOf("[") >= 0 And elementValue.IndexOf("]") >= 0
                    Dim strElementKey As String = elementValue.Substring(elementValue.IndexOf("[") + 1, elementValue.IndexOf("]") - elementValue.IndexOf("[") - 1)
                    Dim mReplaceValue As String = strElementKey
                    Dim bFind As Boolean = False
                    If strElementKey.IndexOf("Variant_") = 0 Then
                        mReplaceValue = GetVariantValue(strKey, strElementKey, bFind, cLocalElement)
                    ElseIf strElementKey.IndexOf("TextList_") = 0 Then
                        mReplaceValue = GetTextListValue(strElementKey, bFind, cLocalElement)
                    ElseIf strElementKey.IndexOf("PictureList_") = 0 Then
                        mReplaceValue = GetPictureListValue(strElementKey, bFind, cLocalElement)
                    ElseIf strElementKey.IndexOf("Parameter_") = 0 Then
                        mReplaceValue = GetParametersListValue(strElementKey, bFind, cLocalElement)
                    Else
                        mReplaceValue = GetValue(strElementKey, bFind, cLocalElement)
                    End If

                    If bFind Then
                        elementValue = elementValue.Substring(0, elementValue.IndexOf("[")) + mReplaceValue + elementValue.Substring(elementValue.IndexOf("]") + 1)
                    Else
                        elementValue = elementValue.Substring(0, elementValue.IndexOf("[")) + "$!" + mReplaceValue + "!$" + elementValue.Substring(elementValue.IndexOf("]") + 1)
                    End If
                Loop
                elementValue = elementValue.Replace("$!", "[").Replace("!$", "]")
                If strKey = HMISubStepKeys.Parameter Then
                    Dim cListValue As List(Of String) = clsParameter.ToList(elementValue)
                    Dim cNewListValue As New List(Of String)
                    For Each element As String In cListValue
                        If element.IndexOf("-") > 0 Then
                            element = element.Split("-")(0)
                        End If
                        cNewListValue.Add(element)
                    Next
                    elementValue = clsParameter.ToString(cNewListValue)
                End If
                Return elementValue
            End SyncLock
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Private Function GetValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        If cLocalVariant.CurrentVariantCfg.ListElement.ContainsKey(strElementKey) Then
            elementValue = cLocalVariant.CurrentVariantCfg.ListElement(strElementKey)
            bFind = True
        ElseIf cTextManager.HasText(strElementKey) Then
            elementValue = cTextManager.GetTextCfgFromKey(strElementKey).ActiveMessage
            bFind = True
        ElseIf cPictureManager.HasPicture(strElementKey) Then
            elementValue = cPictureManager.GetPictureCfgFromName(strElementKey).Path
            bFind = True
        End If
        Return elementValue
    End Function
    Private Function GetVariantValue(ByVal strKey As String, ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim cVariantCfg As New clsVariantCfg
        If cLocalElement.ContainsKey(clsVariantManager.Name) Then
            cVariantCfg = CType(cLocalElement(clsVariantManager.Name), clsVariantManager).CurrentVariantCfg
        Else
            cVariantCfg = cLocalElement(clsVariantCfg.Name)
        End If
        If cLocalVariant.CurrentVariantCfg.Variant <> cVariantCfg.Variant Then
            If Not cGlobalProgramManager.HasGlobalProgram(cVariantCfg.Variant) Then
                If Not cLocalVariant.HasVariant(cVariantCfg.Variant) Then
                    cLocalVariant.LoadVariantCfg()
                End If
                cLocalVariant.ChangeVariant(cVariantCfg.Variant)
            End If
        End If
        If cLocalVariant.CurrentVariantCfg.Variant = "" Then
            Return lSubStepParameter(strKey).ToString
        End If

        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(8)
        If strElementKey = "Variant" Then
            elementValue = cLocalVariant.CurrentVariantCfg.Variant
            bFind = True
        ElseIf strElementKey = "SFC" Then
            If cSystemElement.ContainsKey(clsMachineStatusManager.Name) Then
                If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                    Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
                    Dim cMachineStatusManager As clsMachineStatusManager = cSystemElement(clsMachineStatusManager.Name)
                    elementValue = cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.SFC
                Else
                    elementValue = cLocalVariant.CurrentVariantCfg.SFC
                End If
            Else
                elementValue = cLocalVariant.CurrentVariantCfg.SFC
            End If
            bFind = True
        ElseIf strElementKey = "Jump" Then
            If cSystemElement.ContainsKey(clsMachineStatusManager.Name) Then
                If cLocalElement.ContainsKey(clsMachineStationCfg.Name) Then
                    Dim cMachineStationCfg As clsMachineStationCfg = cLocalElement(clsMachineStationCfg.Name)
                    Dim cMachineStatusManager As clsMachineStatusManager = cSystemElement(clsMachineStatusManager.Name)
                    elementValue = cMachineStatusManager.GetMachineStatusCfgFromName(cMachineStationCfg.ID).VariantCfg.Jump
                    bFind = True
                End If
            End If

        ElseIf strElementKey = "Picture" Then
            elementValue = cLocalVariant.CurrentVariantCfg.PicturePath
            bFind = True
        Else
            If cLocalVariant.CurrentVariantCfg.ListElement.ContainsKey(strElementKey) Then
                elementValue = cLocalVariant.CurrentVariantCfg.ListElement(strElementKey)
                bFind = True
            End If
        End If
        Return elementValue
    End Function

    Private Function GetTextListValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(9)
        If cTextManager.HasText(strElementKey) Then
            elementValue = cTextManager.GetTextCfgFromKey(strElementKey).ActiveMessage
            bFind = True
        End If
        Return elementValue
    End Function

    Private Function GetPictureListValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        strElementKey = strElementKey.Substring(12)
        If cPictureManager.HasPicture(strElementKey) Then
            elementValue = cPictureManager.GetPictureCfgFromName(strElementKey).Path
            bFind = True
        End If
        Return elementValue
    End Function

    Private Function GetParametersListValue(ByVal strElementKey As String, ByRef bFind As Boolean, ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim elementValue As String = strElementKey
        Dim cMachineManager As clsMachineManager
        bFind = True
        cMachineManager = cSystemElement(clsMachineManager.Name)
        strElementKey = strElementKey.Substring(10)
        Return cMachineManager.MachineGlobalParameter.GetMachineGlobalParameterFromKey(clsHMIGlobalParameter.Process).ToString
    End Function

    Public Function ActiveDescription(ByVal cLocalElement As Dictionary(Of String, Object)) As String
        Dim mTmepValue As String = ""
        If cLanguageManager.SecondLanguageActive Then
            mTmepValue = ChangedSubStepParameter(HMISubStepKeys.Description2, cLocalElement)
            If mTmepValue = "" Then
                mTmepValue = ChangedSubStepParameter(HMISubStepKeys.Description, cLocalElement)
            End If
        Else
            mTmepValue = ChangedSubStepParameter(HMISubStepKeys.Description, cLocalElement)
        End If

        Do While mTmepValue.IndexOf("[") >= 0 And mTmepValue.IndexOf("]") >= 0
            Dim strKey As String = mTmepValue.Substring(mTmepValue.IndexOf("[") + 1, mTmepValue.IndexOf("]") - mTmepValue.IndexOf("[") - 1)
            If cTextManager.HasText(strKey) Then
                mTmepValue = mTmepValue.Replace("[" + strKey + "]", cTextManager.GetTextCfgFromKey(strKey).ActiveMessage)
            Else
                mTmepValue = mTmepValue.Replace("[", "$!").Replace("]", "!$")
            End If
        Loop
        mTmepValue = mTmepValue.Replace("$!", "[").Replace("!$", "]")
        Return mTmepValue
    End Function


    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.cSystemElement = cSystemElement
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
            cTextManager = cSystemElement(clsTextManager.Name)
            cPictureManager = cSystemElement(clsPictureManager.Name)
            cGlobalProgramManager = cSystemElement(clsGlobalProgramManager.Name)
            For Each element As String In HMISubStepKeys.GetUserDefinedKeys
                lSubStepParameter.Add(element, "")
            Next
            If IsNothing(cLocalVariant) Then
                cLocalVariant = New clsVariantManager
                cLocalVariant.Init(cSystemElement)
                cLocalVariant.LoadVariantCfg()
            End If

        End SyncLock
    End Sub

    Public Function Clone() As clsSubStepCfg
        SyncLock _Object
            Dim cTemp As New clsSubStepCfg(cSystemElement)
            For Each element As String In HMISubStepKeys.GetUserDefinedKeys
                cTemp.SubStepParameter(element) = lSubStepParameter(element)
            Next
            Return cTemp
        End SyncLock
    End Function

    Public Shared Operator <>(ByVal x As clsSubStepCfg, ByVal y As clsSubStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            For Each element As String In HMISubStepKeys.GetUserDefinedKeys
                If x.SubStepParameter(element) <> y.SubStepParameter(element) Then
                    Return True
                End If
            Next
            Return False
        End SyncLock
    End Operator
    Public Shared Operator =(ByVal x As clsSubStepCfg, ByVal y As clsSubStepCfg) As Boolean
        SyncLock _Object2
            If x Is Nothing Or y Is Nothing Then Return False
            For Each element As String In HMISubStepKeys.GetUserDefinedKeys
                If x.SubStepParameter(element) <> y.SubStepParameter(element) Then
                    Return False
                End If
            Next
            Return True
        End SyncLock
    End Operator
End Class

Public Class clsActionResultCfg
    Private _Object As New Object
    Private bResult As Boolean = True
    Private dUpLimit As Double = -999999
    Private dLowLimit As Double = -999999
    Private dValue As Double = -999999
    Private strErrorType As String = String.Empty
    Private strMainErrorType As String = String.Empty
    Private strErrorCode As String = String.Empty
    Private strErrorMessage As String = String.Empty
    Private strShowErrorMessage As String = String.Empty
    Private eCompareMode As enumCompareMode
    Private iRepeatNum As Integer = 0
    Private iReWorkNum As Integer = 0
    Private cSubStepCfg As clsSubStepCfg
    Private bAbort As Boolean = False
    Private eErrorLevel As enumErrorLevel = enumErrorLevel.Alarm
    Private strLocation As String = ""
    Private strMESPosition As String = ""
    Public Const Name As String = "ActionResultCfg"
    Private bJump As Boolean = False
    Private iJumpStep As Integer = 0
    Private iSuccessNum As Integer = 0
    Private bOnlyShowMessage As Boolean = False
    Private bDisableContinue As Boolean = False
    Private bOtherStationInQueue As Boolean = False
    Private bOtherStationInQueue2 As Boolean = False
    Private bCancelAutoContinue As Boolean = False
    Private bCancelUpdate As Boolean = False
    Private bAbortProcess As Boolean = False
    Private bMaxLimite As Boolean = False
    Public Function Clone() As clsActionResultCfg
        SyncLock _Object
            Dim cTemp As New clsActionResultCfg
            cTemp.OnlyShowMessage = OnlyShowMessage
            cTemp.SuccessNum = SuccessNum
            cTemp.JumpStep = JumpStep
            cTemp.Jump = Jump
            cTemp.ErrorLevel = ErrorLevel
            If Not IsNothing(SubStepCfg) Then
                cTemp.SubStepCfg = SubStepCfg.Clone
            End If

            cTemp.RepeatNum = RepeatNum
            cTemp.Location = Location
            cTemp.MESPosition = MESPosition
            cTemp.ReWorkNum = ReWorkNum
            cTemp.CompareMode = CompareMode
            cTemp.UpLimit = UpLimit
            cTemp.LowLimit = LowLimit
            cTemp.Value = Value
            cTemp.ErrorType = ErrorType
            cTemp.ErrorCode = ErrorCode
            cTemp.MainErrorType = MainErrorType
            cTemp.ErrorMessage = ErrorMessage
            cTemp.ShowErrorMessage = ShowErrorMessage
            cTemp.Result = Result
            cTemp.Abort = Abort
            cTemp.DisableContinue = DisableContinue
            cTemp.OtherStationInQueue = OtherStationInQueue
            cTemp.OtherStationInQueue2 = OtherStationInQueue2
            cTemp.CancelAutoContinue = CancelAutoContinue
            cTemp.CancelUpdate = CancelUpdate
            cTemp.AbortProcess = AbortProcess
            cTemp.MaxLimite = MaxLimite
            Return cTemp
        End SyncLock
    End Function

    Public Property MaxLimite As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bMaxLimite = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bMaxLimite
            End SyncLock
        End Get
    End Property

    Public Property AbortProcess As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bAbortProcess = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bAbortProcess
            End SyncLock
        End Get
    End Property

    Public Property CancelUpdate As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bCancelUpdate = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bCancelUpdate
            End SyncLock
        End Get
    End Property

    Public Property CancelAutoContinue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bCancelAutoContinue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bCancelAutoContinue
            End SyncLock
        End Get
    End Property

    Public Property OtherStationInQueue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bOtherStationInQueue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bOtherStationInQueue
            End SyncLock
        End Get
    End Property

    Public Property OtherStationInQueue2 As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bOtherStationInQueue2 = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bOtherStationInQueue2
            End SyncLock
        End Get
    End Property

    Public Property DisableContinue As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bDisableContinue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bDisableContinue
            End SyncLock
        End Get
    End Property

    Public Property OnlyShowMessage As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bOnlyShowMessage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bOnlyShowMessage
            End SyncLock
        End Get
    End Property
    Public Property SuccessNum As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iSuccessNum = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iSuccessNum
            End SyncLock
        End Get
    End Property
    Public Property JumpStep As Integer
        Set(ByVal value As Integer)
            iJumpStep = value
        End Set
        Get
            Return iJumpStep
        End Get
    End Property
    Public Property Jump As Boolean
        Set(ByVal value As Boolean)
            bJump = value
        End Set
        Get
            Return bJump
        End Get
    End Property
    Public Property ErrorLevel As enumErrorLevel
        Set(ByVal value As enumErrorLevel)
            SyncLock _Object
                eErrorLevel = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eErrorLevel
            End SyncLock
        End Get
    End Property
    Public Property SubStepCfg As clsSubStepCfg
        Set(ByVal value As clsSubStepCfg)
            SyncLock _Object
                cSubStepCfg = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cSubStepCfg
            End SyncLock
        End Get
    End Property
    Public Property RepeatNum As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iRepeatNum = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iRepeatNum
            End SyncLock
        End Get
    End Property
    Public Property Location As String
        Set(ByVal value As String)
            SyncLock _Object
                strLocation = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strLocation
            End SyncLock
        End Get
    End Property
    Public Property MESPosition As String
        Set(ByVal value As String)
            SyncLock _Object
                strMESPosition = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strMESPosition
            End SyncLock
        End Get
    End Property

    Public Property ReWorkNum As Integer
        Set(ByVal value As Integer)
            SyncLock _Object
                iReWorkNum = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return iReWorkNum
            End SyncLock
        End Get
    End Property

    Public Property CompareMode As enumCompareMode
        Set(ByVal value As enumCompareMode)
            SyncLock _Object
                eCompareMode = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eCompareMode
            End SyncLock
        End Get
    End Property

    Public Property UpLimit As Double
        Set(ByVal value As Double)
            SyncLock _Object
                dUpLimit = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return dUpLimit
            End SyncLock
        End Get
    End Property

    Public Property LowLimit As Double
        Set(ByVal value As Double)
            SyncLock _Object
                dLowLimit = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return dLowLimit
            End SyncLock
        End Get
    End Property

    Public Property Value As Double
        Set(ByVal value As Double)
            SyncLock _Object
                dValue = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return dValue
            End SyncLock
        End Get
    End Property

    Public Property ErrorType As String
        Set(ByVal value As String)
            SyncLock _Object
                strErrorType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strErrorType
            End SyncLock
        End Get
    End Property

    Public Property ErrorCode As String
        Set(ByVal value As String)
            SyncLock _Object
                strErrorCode = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strErrorCode
            End SyncLock
        End Get
    End Property


    Public Property MainErrorType As String
        Set(ByVal value As String)
            SyncLock _Object
                strMainErrorType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strMainErrorType
            End SyncLock
        End Get
    End Property

    Public Property ErrorMessage As String
        Set(ByVal value As String)
            SyncLock _Object
                strErrorMessage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strErrorMessage
            End SyncLock
        End Get
    End Property

    Public Property ShowErrorMessage As String
        Set(ByVal value As String)
            SyncLock _Object
                strShowErrorMessage = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strShowErrorMessage
            End SyncLock
        End Get
    End Property


    Public Property Result As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bResult = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bResult
            End SyncLock
        End Get
    End Property

    Public Property Abort As Boolean
        Set(ByVal value As Boolean)
            SyncLock _Object
                bAbort = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return bAbort
            End SyncLock
        End Get
    End Property

End Class

Public Enum enumCompareMode
    Equal = 0
    NotEqual
    Between
End Enum

Public Enum enumErrorLevel
    Alarm = 0
    Normal
End Enum