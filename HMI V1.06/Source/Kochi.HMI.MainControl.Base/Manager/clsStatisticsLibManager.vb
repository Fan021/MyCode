Imports System.Reflection
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Action
Imports System.Collections.Concurrent
Imports Kochi.HMI.MainControl.UI

Public Class clsStatisticsLibManager
    Public Const Name As String = "StatisticsLibManager"
    Private cSystemManager As clsSystemManager
    Private cFileHandler As New clsFileHandler
    Private lListDll As New Dictionary(Of String, clsStatisticsLibCfg)
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

    Public Function HasStatisticsLibCfgFromKey(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lListDll.ContainsKey(strKey) Then
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

    Public Function GetStatisticsLibCfgFromKey(ByVal strKey As String) As clsStatisticsLibCfg
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

                For Each element As String In cFileHandler.ListDll(cSystemManager.Settings.StatisticsFolder)
                    Asm = Assembly.LoadFile(element)
                    If Asm Is Nothing Then Continue For
                    For Each t As Type In Asm.GetTypes
                        If Not t.IsClass Or t.IsAbstract Then Continue For
                        If Not t.GetInterface("IChildrenUI") Is Nothing Then
                            Obj = System.Activator.CreateInstance(t, True)
                            Dim typeinfo As MemberInfo = Obj.GetType
                            Dim bookarr = typeinfo.GetCustomAttributes(GetType(clsChildrenUINameAttribute), False)
                            If Not bookarr Is Nothing And bookarr.Length = 1 Then
                                Dim cStatisticsLibCfg As New clsStatisticsLibCfg
                                cStatisticsLibCfg.Path = element
                                cStatisticsLibCfg.ClassName = t.FullName
                                cStatisticsLibCfg.DeviceType = CType(bookarr(0), clsChildrenUINameAttribute).DeviceType
                                cStatisticsLibCfg.UIName = CType(bookarr(0), clsChildrenUINameAttribute).Name
                                cStatisticsLibCfg.Source = Obj
                                If Not lListDll.ContainsKey(cStatisticsLibCfg.DeviceType.ToString) Then
                                    lListDll.Add(cStatisticsLibCfg.DeviceType.ToString, cStatisticsLibCfg)
                                End If
                            End If
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
End Class

Public Class clsStatisticsLibCfg
    Private strPath As String = String.Empty
    Private strUIName As String = String.Empty
    Private strDeviceType As Type
    Private strClassName As String = String.Empty
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

    Public Property UIName As String
        Set(ByVal value As String)
            SyncLock _Object
                strUIName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strUIName
            End SyncLock
        End Get
    End Property

    Public Property DeviceType As Type
        Set(ByVal value As Type)
            SyncLock _Object
                strDeviceType = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strDeviceType
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
