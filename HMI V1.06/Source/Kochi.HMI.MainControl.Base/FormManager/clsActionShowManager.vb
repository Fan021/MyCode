Imports System.Reflection
Imports System.Math
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsActionShowManager
    Implements IDisposable
    Protected cSystemElement As Dictionary(Of String, Object)
    Protected lstResults As ListViewEx
    Protected mMainForm As IMainUI
    Protected _Object As New Object
    Protected cTextManager As clsTextManager
    Protected iCnt As Integer = 0
    Protected bInsertData As Boolean = False
    Public Const Name As String = "ActionShowManager"

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                mMainForm = CType(cSystemElement(enumUIName.MainForm.ToString), Form)
                cTextManager = CType(cSystemElement(clsTextManager.Name), clsTextManager)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function RegisterManager(ByVal lstResults As ListViewEx) As Boolean
        SyncLock _Object
            Try
                Me.lstResults = lstResults
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function CleanActionStep() As Boolean
        SyncLock _Object
            Try
                mMainForm.InvokeAction(Sub()
                                           lstResults.Items.Clear()
                                       End Sub)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AddNewActionStep(ByVal strAction As String, ByVal strMaterialNr As String, ByVal eActionResult As enumActionResult, ByVal strComment As String) As Boolean
        SyncLock _Object
            Try
                bInsertData = True
                If strComment.IndexOf("[") >= 0 And strComment.IndexOf("]") >= 0 Then
                    Dim strKey As String = strComment.Replace("[", "").Replace("]", "")
                    If cTextManager.HasText(strKey) Then
                        strComment = cTextManager.GetTextCfgFromKey(strKey).Message
                    End If
                End If
                iCnt = 0
                mMainForm.InvokeAction(Sub()
                                           Dim Item As ListViewItem = Nothing
                                           Item = New ListViewItem
                                           Item.SubItems.Add(strAction)
                                           Item.SubItems.Add(strMaterialNr)
                                           Item.SubItems.Add(eActionResult.ToString)
                                           Item.SubItems.Add("0.00 s")
                                           Item.SubItems.Add("")
                                           Item.SubItems.Add(strComment)
                                           Item.Name = (lstResults.Items.Count + 1).ToString
                                           Item.Text = (lstResults.Items.Count + 1).ToString

                                           lstResults.Items.Add(Item)
                                           lstResults.EnsureVisible(lstResults.Items.Count - 1)
                                       End Sub)

                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateLastActionStepTime(ByVal dTime As Double) As Boolean
        SyncLock _Object
            Try
                If Not bInsertData Then
                    Return True
                End If
                If iCnt > 5 Then
                    mMainForm.InvokeAction(Sub()
                                               If lstResults.Items.Count >= 1 Then
                                                   lstResults.BeginUpdate()
                                                   lstResults.Items(lstResults.Items.Count - 1).SubItems(4).Text = (dTime / 1000.0).ToString("0.00") + " s"
                                                   lstResults.EndUpdate()
                                               End If
                                           End Sub)
                    iCnt = 0
                End If
                iCnt = iCnt + 1
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateLastActionValue(ByVal strValue As String) As Boolean
        SyncLock _Object
            Try
                If iCnt > 20 Then
                    mMainForm.InvokeAction(Sub()
                                               If lstResults.Items.Count >= 1 Then
                                                   lstResults.BeginUpdate()
                                                   lstResults.Items(lstResults.Items.Count - 1).SubItems(5).Text = strValue
                                                   lstResults.EndUpdate()
                                               End If
                                           End Sub)
                    iCnt = 0
                End If
                iCnt = iCnt + 1
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateLastActionStepEndTime(ByVal dTime As Double) As Boolean
        SyncLock _Object
            Try
                If Not bInsertData Then
                    Return True
                End If
                mMainForm.InvokeAction(Sub()
                                           If lstResults.Items.Count >= 1 Then
                                               lstResults.BeginUpdate()
                                               lstResults.Items(lstResults.Items.Count - 1).SubItems(4).Text = (dTime / 1000.0).ToString("0.00") + " s"
                                               lstResults.EndUpdate()
                                           End If
                                       End Sub)
                iCnt = 0
                iCnt = iCnt + 1
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function


    Public Function UpdateLastActionStepResult(ByVal eActionResult As enumActionResult) As Boolean
        SyncLock _Object
            Try
                If Not bInsertData Then
                    Return True
                End If
                mMainForm.InvokeAction(Sub()
                                           If lstResults.Items.Count >= 1 Then
                                               lstResults.Items(lstResults.Items.Count - 1).SubItems(3).Text = eActionResult.ToString
                                           End If
                                           bInsertData = False
                                       End Sub)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function UpdateLastActionStepValue(ByVal strValue As String) As Boolean
        SyncLock _Object
            Try
                If Not bInsertData Then
                    Return True
                End If
                mMainForm.InvokeAction(Sub()
                                           If lstResults.Items.Count >= 1 Then
                                               lstResults.Items(lstResults.Items.Count - 1).SubItems(5).Text = strValue
                                           End If
                                       End Sub)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetActionList() As List(Of clsActionShowManagerCfg)
        SyncLock _Object
            Try
                Dim iListActionList As New List(Of clsActionShowManagerCfg)
                mMainForm.InvokeAction(Sub()
                                           Dim TempActionShowManagerCfg As New clsActionShowManagerCfg
                                           For i = 0 To lstResults.Items.Count - 1
                                               TempActionShowManagerCfg.Action = lstResults.Items(i).SubItems(0).Text
                                               TempActionShowManagerCfg.MaterialNr = lstResults.Items(i).SubItems(1).Text
                                               TempActionShowManagerCfg.ActionResult = [Enum].Parse(GetType(enumActionResult), lstResults.Items(i).SubItems(2).Text)
                                               TempActionShowManagerCfg.Time = lstResults.Items(i).SubItems(3).Text
                                               TempActionShowManagerCfg.Comment = lstResults.Items(i).SubItems(4).Text
                                               iListActionList.Add(TempActionShowManagerCfg)
                                           Next
                                       End Sub)
                Return iListActionList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
        Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        On Error Resume Next
        MyBase.Finalize()
    End Sub
#End Region
End Class

Public Enum enumActionResult
    Ongoing
    PASS
    FAIL
End Enum

Public Class clsActionShowManagerCfg
    Private strAction As String
    Private strMaterialNr As String
    Private eActionResult As enumActionResult
    Private strTime As String
    Private strComment As String
    Private _Object As New Object

    Public Property Action As String
        Set(ByVal value As String)
            SyncLock _Object
                strAction = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strAction
            End SyncLock
        End Get
    End Property
    Public Property MaterialNr As String
        Set(ByVal value As String)
            SyncLock _Object
                strMaterialNr = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strMaterialNr
            End SyncLock
        End Get
    End Property
    Public Property Comment As String
        Set(ByVal value As String)
            SyncLock _Object
                strComment = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strComment
            End SyncLock
        End Get
    End Property
    Public Property ActionResult As enumActionResult
        Set(ByVal value As enumActionResult)
            SyncLock _Object
                eActionResult = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return eActionResult
            End SyncLock
        End Get
    End Property
    Public Property Time As String
        Set(ByVal value As String)
            SyncLock _Object
                strTime = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strTime
            End SyncLock
        End Get
    End Property
End Class
