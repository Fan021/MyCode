Imports System.Reflection
Imports Kochi.HMI.MainControl.Base
Imports Kochi.HMI.MainControl.Device
Imports System.Collections.Concurrent

Public Class clsDeviceLibManager
    Public Const Name As String = "DeviceLibManager"
    Private cSystemManager As clsSystemManager
    Private cFileHandler As New clsFileHandler
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager
    Private lListDll As New Dictionary(Of String, clsDeviceLibCfg)


    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
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

    Public Function GetDeviceLibCfgFromKey(ByVal strKey As String) As clsDeviceLibCfg
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

                For Each element As String In cFileHandler.ListDll(cSystemManager.Settings.DeviceFolder)
                    Try
                        Asm = Assembly.LoadFile(element)
                    
                    If Asm Is Nothing Then Continue For
                    For Each t As Type In Asm.GetTypes
                        If Not t.IsClass Or t.IsAbstract Then Continue For
                        If Not t.GetInterface("IHMIDeviceBase") Is Nothing Then
                            Obj = System.Activator.CreateInstance(t, True)
                            Dim typeinfo As MemberInfo = Obj.GetType
                            Dim bookarr As Object
                            Try
                                bookarr = typeinfo.GetCustomAttributes(GetType(clsHMIDeviceNameAttribute), False)
                            Catch
                                Continue For
                            End Try
                            If Not bookarr Is Nothing And bookarr.Length = 1 Then
                                Dim cDeviceLibCfg As New clsDeviceLibCfg
                                cDeviceLibCfg.Path = element
                                cDeviceLibCfg.ClassName = t.FullName
                                If TypeOf bookarr(0) Is clsHMIDeviceNameAttribute Then
                                    cDeviceLibCfg.DeviceName = CType(bookarr(0), clsHMIDeviceNameAttribute).Name
                                    cDeviceLibCfg.DeviceType = CType(bookarr(0), clsHMIDeviceNameAttribute).Type
                                    cDeviceLibCfg.Source = Obj
                                    If Not lListDll.ContainsKey(cDeviceLibCfg.DeviceName) Then
                                        lListDll.Add(cDeviceLibCfg.DeviceName, cDeviceLibCfg)
                                    End If
                                End If
                            End If
                        End If
                        Next
                    Catch ex As Exception
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


Public Class clsDeviceLibCfg
    Private strPath As String = String.Empty
    Private strDeviceName As String = String.Empty
    Private strClassName As String = String.Empty
    Private strDeviceType As String = String.Empty
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

    Public Property DeviceName As String
        Set(ByVal value As String)
            SyncLock _Object
                strDeviceName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strDeviceName
            End SyncLock
        End Get
    End Property

    Public Property DeviceType As String
        Set(ByVal value As String)
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
