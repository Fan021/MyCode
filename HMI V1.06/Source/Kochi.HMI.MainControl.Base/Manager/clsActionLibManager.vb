Imports System.Reflection
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Action
Imports System.Collections.Concurrent

Public Class clsActionLibManager
    Public Const Name As String = "ActionLibManager"
    Private cSystemManager As clsSystemManager
    Private cFileHandler As New clsFileHandler
    Private lListDll As New Dictionary(Of String, clsActionLibCfg)
    Private _Object As New Object
  
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                LoadDll()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function GetListDllKey() As List(Of String)
        SyncLock _Object
            Try
                Return lListDll.Keys.ToList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetActionLibCfgFromKey(ByVal strKey As String) As clsActionLibCfg
        SyncLock _Object
            Try
                If lListDll.ContainsKey(strKey) Then
                    Return lListDll(strKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Private Function LoadDll() As Boolean
        SyncLock _Object
            Try
                Dim Asm As Assembly, Obj As Object

                For Each element As String In cFileHandler.ListDll(cSystemManager.Settings.ActionFolder)
                    Asm = Assembly.LoadFile(element)
                    If Asm Is Nothing Then Continue For
                    Try
                        For Each t As Type In Asm.GetTypes
                            If Not t.IsClass Or t.IsAbstract Then Continue For
                            If Not t.GetInterface("IHMIActionBase") Is Nothing Then
                                Obj = System.Activator.CreateInstance(t, True)
                                Dim typeinfo As MemberInfo = Obj.GetType
                                Dim bookarr As New Object
                                Try
                                    bookarr = typeinfo.GetCustomAttributes(GetType(clsHMIActionNameAttribute), False)
                                Catch ex As Exception
                                    Continue For
                                End Try

                                If Not bookarr Is Nothing And bookarr.Length = 1 Then
                                    Dim cActionLibCfg As New clsActionLibCfg
                                    cActionLibCfg.Path = element
                                    cActionLibCfg.ClassName = t.FullName
                                    cActionLibCfg.ActionName = CType(bookarr(0), clsHMIActionNameAttribute).Name
                                    cActionLibCfg.HMIActionType = CType(bookarr(0), clsHMIActionNameAttribute).HMIActionType
                                    cActionLibCfg.HMISubActionType = CType(bookarr(0), clsHMIActionNameAttribute).HMISubActionType
                                    cActionLibCfg.Source = Obj
                                    If Not lListDll.ContainsKey(cActionLibCfg.ActionName) Then
                                        lListDll.Add(cActionLibCfg.ActionName, cActionLibCfg)
                                    End If
                                End If
                            End If
                        Next
                    Catch
                        Continue For
                    End Try

                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
End Class


Public Class clsActionLibCfg
    Private strPath As String = String.Empty
    Private strActionName As String = String.Empty
    Private strClassName As String = String.Empty
    Private cHMIActionType As enumHMIActionType
    Private cHMISubActionType As enumHMISubActionType
    Private cSource As Object
    Private _Object As New Object

    Public Property Path As String
        Set(ByVal value As String)
            SyncLock _Object
                strPath = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strPath
            End SyncLock
        End Get
    End Property

    Public Property ActionName As String
        Set(ByVal value As String)
            SyncLock _Object
                strActionName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strActionName
            End SyncLock
        End Get
    End Property

    Public Property HMISubActionType As enumHMIActionType
        Set(ByVal value As enumHMIActionType)
            SyncLock _Object
                cHMISubActionType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cHMISubActionType
            End SyncLock
        End Get
    End Property

    Public Property HMIActionType As enumHMISubActionType
        Set(ByVal value As enumHMISubActionType)
            SyncLock _Object
                cHMIActionType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cHMIActionType
            End SyncLock
        End Get
    End Property

    Public Property ClassName As String
        Set(ByVal value As String)
            SyncLock _Object
                strClassName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strClassName
            End SyncLock
        End Get
    End Property

    Public Property Source As Object
        Set(ByVal value As Object)
            SyncLock _Object
                cSource = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return cSource
            End SyncLock
        End Get
    End Property

    Public Function CreateInstance() As Object
        SyncLock _Object
            Try
                Dim cls As Type = Nothing, Obj As Object
                cls = Assembly.LoadFrom(strPath).GetType(strClassName)
                Obj = Activator.CreateInstance(cls)
                Return Obj
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

End Class


