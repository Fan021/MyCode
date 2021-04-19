Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class clsUserManager
    Public Const Name As String = "UserManager"
    Private lUserList As New List(Of clsUserCfg)
    Private cUserData As New clsUserData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cHMIDataView As HMIDataView
    Private _Object As New Object
    Private cCurrentUserCfg As New clsUserCfg
    Private cLanguageManager As Language
    Public Event UserChanged(ByVal strVariant As String, ByVal cUserCfg As clsUserCfg)
    Public Event LoginOutChanged()
    Public Event LoginChanged(ByVal strVariant As String, ByVal cUserCfg As clsUserCfg)
    Private Const strAdminUser As String = "Admin"
    Private Const strAdminPassword As String = "Admin"
    Public ReadOnly Property CurrentUserCfg As clsUserCfg
        Get
            SyncLock _Object
                Return cCurrentUserCfg
            End SyncLock
        End Get
    End Property

    Public Function RegisterManager(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage = cDataGridViewPage
            Me.cHMIDataView = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal _AppSettings As Settings) As Boolean
        SyncLock _Object
            Try
                cUserData.Init(Devices)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetUserListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lUserList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetUserCfgFromKey(ByVal iKey As Integer) As clsUserCfg
        SyncLock _Object
            Try
                If iKey <= lUserList.Count - 1 Then
                    Return lUserList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetUserCfgFromName(ByVal strName As String) As clsUserCfg
        SyncLock _Object
            Try
                If Not lUserList.Any(Function(e) e.Name = strName) Then
                    Return Nothing
                End If
                Return lUserList.Where(Function(e) e.Name = strName).First()
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strName As String, ByVal strPassword As String, ByVal strLevel As String) As Boolean
        SyncLock _Object
            Try
                cUserData.InSertData(strID, strName, strPassword, strLevel)
                lUserList.Add(New clsUserCfg(strID, strName, strPassword, [Enum].Parse(GetType(enumUserLevel), strLevel)))
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strName As String, ByVal strPassword As String, ByVal strLevel As String) As Boolean
        SyncLock _Object
            Try
                cUserData.ModifyData(strID, strName, strPassword, strLevel)
                For Each element As clsUserCfg In lUserList
                    If element.ID = strID Then
                        element.Name = strName
                        element.Password = strPassword
                        element.Level = [Enum].Parse(GetType(enumUserLevel), strLevel)
                    End If
                Next
                ChangeID()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasUser(ByVal strName As String) As Boolean
        SyncLock _Object
            Try
                If lUserList.Any(Function(e) e.Name = strName) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasUser(ByVal strName As String, ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                If lUserList.Any(Function(e) e.Name = strName And e.ID <> strID) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function HasUserAndPassWord(ByVal strName As String, ByVal strPassWord As String) As Boolean
        SyncLock _Object
            Try
                If lUserList.Any(Function(e) e.Name = strName And e.Password.ToUpper = strPassWord.ToUpper) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ChangeUser(ByVal strName As String) As Boolean

        SyncLock _Object
            Try
                If Not HasUser(strName) Then
                    Return False
                End If
                cCurrentUserCfg = GetUserCfgFromName(strName)
                RaiseEvent UserChanged(strName, cCurrentUserCfg)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function AutoLogin() As Boolean

        SyncLock _Object
            Try
                cCurrentUserCfg.Level = enumUserLevel.Operator
                RaiseEvent UserChanged("", cCurrentUserCfg)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function LoginOut() As Boolean

        SyncLock _Object
            Try
                cCurrentUserCfg = New clsUserCfg
                RaiseEvent LoginOutChanged()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                If strID = 0 Then Return True
                lUserList.RemoveAt(CInt(strID))
                ChangeID()
                cUserData.SaveUser(lUserList)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadUserCfg() As Boolean
        SyncLock _Object
            Try
                lUserList.Clear()
                lUserList.Add(New clsUserCfg(0, strAdminUser, strAdminPassword, enumUserLevel.Administrator))
                cUserData.LoadUser(lUserList)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("UserTable")
                dt.Columns.Add("ID")
                dt.Columns.Add("Name")
                dt.Columns.Add("Password")
                dt.Columns.Add("Level")
                For Each element As clsUserCfg In lUserList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.Name.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cListSearchContion(1) <> "" Then
                            If element.Level.ToString.IndexOf(cListSearchContion(1)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    dt.Rows.Add(New String() {element.ID, element.Name, element.Password, element.Level.ToString})
                Next
                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage) Then
                    cDataGridViewPage.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage.Paging(cViewPageType)
                Else
                    cHMIDataView.DataSource = Ds.Tables(0)
                End If
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
                Dim j As Integer = 0
                For i = 0 To lUserList.Count - 1
                    lUserList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End SyncLock
    End Function

End Class

Public Class clsUserCfg
    Private strID As String = String.Empty
    Private strName As String = String.Empty
    Private strPassword As String = String.Empty
    Private eLevel As enumUserLevel = enumUserLevel.Normal
    Private _Object As New Object
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
    Public Property Name As String
        Set(ByVal value As String)
            SyncLock _Object
                strName = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strName
            End SyncLock
        End Get
    End Property



    Public Property Password As String
        Set(ByVal value As String)
            SyncLock _Object
                strPassword = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strPassword
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

    Sub New()
        SyncLock _Object

        End SyncLock
    End Sub

    Sub New(ByVal strID As String, ByVal strName As String, ByVal strPassword As String, ByVal eLevel As enumUserLevel)
        SyncLock _Object
            Me.strID = strID
            Me.strName = strName
            Me.strPassword = strPassword
            Me.eLevel = eLevel
        End SyncLock
    End Sub
End Class

Public Class clsUserData
    Private cIniHandler As New clsIniHandler
    Private cLanguageManager As Language
    Private _Object As New Object
    Private _AppSettings As Settings
    Private cSystemElement As Dictionary(Of String, Object)
    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                _AppSettings = cSystemElement(Settings.Name)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strName As String, ByVal strPassword As String, ByVal strLevel As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.SetAnyListToIni(_AppSettings.ConfigFolder + "UserData.ini", "User" + strID, New String() {"ID", "Name", "Password", "Level"},
                                      New String() {strID, strName, strPassword, strLevel})
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strName As String, ByVal strPassword As String, ByVal strLevel As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.SetAnyListToIni(_AppSettings.ConfigFolder + "UserData.ini", "User" + strID, New String() {"ID", "Name", "Password", "Level"},
                                      New String() {strID, strName, strPassword, strLevel})
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.RemoveSection(_AppSettings.ConfigFolder + "UserData.ini", "User" + strID)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadUser(ByRef lUserList As List(Of clsUserCfg)) As Boolean
        SyncLock _Object
            Try
                Dim cUserElement As clsUserCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(_AppSettings.ConfigFolder + "UserData.ini", "User", New String() {"ID", "Name", "Password", "Level"})
                    cUserElement = New clsUserCfg
                    If CType(element("ID"), String) <> "" Then
                        cUserElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("Name"), String) <> "" Then
                        cUserElement.Name = CType(element("Name"), String)
                    End If
                    If CType(element("Password"), String) <> "" Then
                        cUserElement.Password = CType(element("Password"), String)
                    End If
                    If CType(element("Level"), String) <> "" Then
                        cUserElement.Level = [Enum].Parse(GetType(enumUserLevel), CType(element("Level"), String))
                    End If

                    lUserList.Add(cUserElement)
                Next
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function SaveUser(ByVal lUserList As List(Of clsUserCfg)) As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(_AppSettings.ConfigFolder + "UserData.ini", "User")
                For Each element As clsUserCfg In lUserList
                    InSertData(element.ID, element.Name, element.Password, element.Level)
                Next
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class

Public Enum enumUserLevel
    Normal = 0
    [Operator]
    Administrator
End Enum
