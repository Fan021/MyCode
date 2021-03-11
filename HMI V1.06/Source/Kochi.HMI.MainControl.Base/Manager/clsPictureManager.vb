Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsPictureManager
    Public Const Name As String = "PictureManager"
    Private lPictureList As New List(Of clsPictureCfg)
    Private cPictureData As New clsPictureData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cHMIDataView As HMIDataView
    Private cLanguageManager As clsLanguageManager
    Private _Object As New Object

    Public Function RegisterManager(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage = cDataGridViewPage
            Me.cHMIDataView = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cPictureData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function GetPictureListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lPictureList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetPictureCfgFromKey(ByVal iKey As Integer) As clsPictureCfg
        SyncLock _Object
            Try
                If iKey <= lPictureList.Count - 1 Then
                    Return lPictureList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetPictureCfgFromName(ByVal strKey As String) As clsPictureCfg
        SyncLock _Object
            Try
                If Not HasPicture(strKey) Then
                    Return Nothing
                End If
                Return lPictureList.Where(Function(e) e.Key = strKey).First
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strPath As String) As Boolean
        SyncLock _Object
            Try
                    cPictureData.InSertData(strID, strKey, strPath)
                    lPictureList.Add(New clsPictureCfg(strID, strKey, strPath))
                    Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strKey As String, ByVal strPath As String) As Boolean
        SyncLock _Object
            Try
                    cPictureData.ModifyData(strID, strKey, strPath)
                    For Each element As clsPictureCfg In lPictureList
                        If element.ID = strID Then
                            element.Key = strKey
                            element.Path = strPath
                        End If
                    Next
                    ChangeID()
                    Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasPicture(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lPictureList.Any(Function(e) e.Key = strKey) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasPicture(ByVal strKey As String, ByVal strID As String) As Boolean
        SyncLock _Object
            Try

                If lPictureList.Any(Function(e) e.Key = strKey And e.ID <> strID) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
            Throw New clsHMIException(ex, enumExceptionType.Crash)
            Return False
        End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                lPictureList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cPictureData.SavePicture(lPictureList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadPictureCfg() As Boolean
        SyncLock _Object
            Try
                cPictureData.LoadPicture(lPictureList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("PictureTable")
                dt.Columns.Add(cLanguageManager.GetTextLine("PictureManager", "ID"))
                dt.Columns.Add(cLanguageManager.GetTextLine("PictureManager", "Key"))
                dt.Columns.Add(cLanguageManager.GetTextLine("PictureManager", "Path"))
                For Each element As clsPictureCfg In lPictureList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.Key.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cListSearchContion(1) <> "" Then
                            If element.Path.IndexOf(cListSearchContion(1)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    dt.Rows.Add(New String() {element.ID, element.Key, element.Path})
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
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                Dim j As Integer = 1
                For i = 0 To lPictureList.Count - 1
                    lPictureList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

End Class

Public Class clsPictureCfg
    Private strID As String = String.Empty
    Private strKey As String = String.Empty
    Private strPath As String = String.Empty
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
    Public Property Key As String
        Set(ByVal value As String)
            SyncLock _Object
                strKey = value
            End SyncLock
        End Set
        Get
            SyncLock _Object
                Return strKey
            End SyncLock
        End Get
    End Property



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

    Sub New()
        SyncLock _Object

        End SyncLock
    End Sub

    Sub New(ByVal strID As String, ByVal strKey As String, ByVal strPath As String)
        SyncLock _Object
            Me.strID = strID
            Me.strKey = strKey
            Me.strPath = strPath
        End SyncLock
    End Sub
End Class

Public Class clsPictureData
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private _Object As New Object

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                cSystemManager = CType(cSystemElement(clsSystemManager.Name), clsSystemManager)
                cIniHandler = CType(cSystemElement(clsIniHandler.Name), clsIniHandler)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strPath As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.PictureListConfig, "Picture" + strID, New String() {"ID", "KeyName", "Path"},
                                      New String() {strID, strKey, clsSystemPath.ToIniPath(strPath)})
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strKey As String, ByVal strPath As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.PictureListConfig, "Picture" + strID, New String() {"ID", "KeyName", "Path"},
                                      New String() {strID, strKey, clsSystemPath.ToIniPath(strPath)})
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.RemoveSection(cSystemManager.Settings.PictureListConfig, "Picture" + strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadPicture(ByRef lPictureList As List(Of clsPictureCfg)) As Boolean
        SyncLock _Object
            Try
                lPictureList.Clear()
                Dim cPictureElement As clsPictureCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.PictureListConfig, "Picture", New String() {"ID", "KeyName", "Path"})
                    cPictureElement = New clsPictureCfg
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cPictureElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("KeyName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("KeyName"), String) <> clsXmlHandler.s_Null Then
                        cPictureElement.Key = CType(element("KeyName"), String)
                    End If
                    If CType(element("Path"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Path"), String) <> clsXmlHandler.s_Null Then
                        cPictureElement.Path = clsSystemPath.ToSystemPath(CType(element("Path"), String))
                    End If
                    lPictureList.Add(cPictureElement)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function SavePicture(ByVal lPictureList As List(Of clsPictureCfg)) As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(cSystemManager.Settings.PictureListConfig, "Picture")
                For Each element As clsPictureCfg In lPictureList
                    InSertData(element.ID, element.Key, clsSystemPath.ToIniPath(element.Path))
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class
