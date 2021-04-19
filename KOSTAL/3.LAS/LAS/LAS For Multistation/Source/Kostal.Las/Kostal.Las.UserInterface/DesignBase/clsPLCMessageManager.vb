Imports System.Collections.Concurrent
Imports Kostal.Las.Base

Public Class clsPlcMessageManager
    Public Const Name As String = "PlcMessageManager"
    Private lPlcMessageList As New List(Of clsPlcMessageCfg)
    Private cPlcMessageData As New clsPlcMessageData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cHMIDataView As HMIDataView
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)


    Public Function RegisterManager(ByVal cDataGridViewPage As clsDataGridViewPage, ByVal cHMIDataView As HMIDataView) As Boolean
        SyncLock _Object
            Me.cDataGridViewPage = cDataGridViewPage
            Me.cHMIDataView = cHMIDataView
            Return True
        End SyncLock
    End Function

    Public Function Init(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = Devices
                cPlcMessageData.Init(cSystemElement)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetPlcMessageListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lPlcMessageList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetPlcMessageCfgFromKey(ByVal strKey As String) As clsPlcMessageCfg
        SyncLock _Object
            Try
                If Not HasPlcMessage(strKey) Then
                    Return Nothing
                End If
                Return lPlcMessageList.Where(Function(e) e.Key = strKey).FirstOrDefault
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetPlcMessageCfgFromKey(ByVal iKey As Integer) As clsPlcMessageCfg
        SyncLock _Object
            Try
                If iKey <= lPlcMessageList.Count - 1 Then
                    Return lPlcMessageList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw ex
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                lPlcMessageList.Add(New clsPlcMessageCfg(strID, strKey, strMessage, strMessage2, cSystemElement))
                ChangeID()
                cPlcMessageData.SavePlcMessage(lPlcMessageList)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                For Each element As clsPlcMessageCfg In lPlcMessageList
                    If element.ID = strID Then
                        element.Key = strKey
                        element.Message = strMessage
                        element.Message2 = strMessage2
                    End If
                Next
                ChangeID()
                cPlcMessageData.SavePlcMessage(lPlcMessageList)
                Return True

            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasPlcMessage(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lPlcMessageList.Any(Function(e) e.Key = strKey) Then
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

    Public Function HasPlcMessage(ByVal strKey As String, ByVal strID As String) As Boolean
        SyncLock _Object
            Try

                If lPlcMessageList.Any(Function(e) e.Key = strKey And e.ID <> strID) Then
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

    Public Function DeleteData(ByVal strID As String) As Boolean
        SyncLock _Object
            Try
                lPlcMessageList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cPlcMessageData.SavePlcMessage(lPlcMessageList)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadPlcMessageCfg() As Boolean
        SyncLock _Object
            Try
                cPlcMessageData.LoadPlcMessage(lPlcMessageList)
                ChangeID()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal strKey As String, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim iCnt As Integer = 0
                Dim iKeyCnt As Integer = 0
                Dim dt As DataTable = New DataTable("PlcMessageTable")
                dt.Columns.Add("ID")
                dt.Columns.Add("KEY")
                dt.Columns.Add("Message(Chinese)")
                dt.Columns.Add("Message(English)")
                For Each element As clsPlcMessageCfg In lPlcMessageList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.Key.ToString.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cListSearchContion(1) <> "" Then
                            If element.Message.IndexOf(cListSearchContion(1)) < 0 And element.Message2.IndexOf(cListSearchContion(1)) < 0 Then
                                Continue For
                            End If
                        End If

                    End If
                    iCnt = iCnt + 1
                    If element.Key.ToString = strKey Then
                        iKeyCnt = iCnt \ cDataGridViewPage.RowsPerPage
                    End If
                    dt.Rows.Add(New String() {element.ID, element.Key, element.Message, element.Message2})
                Next
                Ds.Tables.Add(dt)
                If Not IsNothing(cDataGridViewPage) Then
                    cDataGridViewPage.SetDataView = Ds.Tables(0).DefaultView
                    cDataGridViewPage.Paging(cViewPageType, iKeyCnt)
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
                lPlcMessageList.Sort(Function(x As clsPlcMessageCfg, y As clsPlcMessageCfg) x.Key.CompareTo(y.Key))
                Dim j As Integer = 1
                For i = 0 To lPlcMessageList.Count - 1
                    lPlcMessageList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End SyncLock
    End Function

End Class

Public Class clsPlcMessageCfg
    Private strID As String = String.Empty
    Private iKey As Integer = 0
    Private strMessage As String = String.Empty
    Private strMessage2 As String = String.Empty
    Private cLanguageManager As Language
    Private cSystemElement As Dictionary(Of String, Object)
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
    Public Property Key As Integer

        Set(ByVal value As Integer)
            SyncLock _Object
                iKey = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return iKey
            End SyncLock
        End Get
    End Property

    Public ReadOnly Property ActiveMessage As String
        Get
            Dim mTempValue As String = ""
            If cLanguageManager.SetAppLanguage.Language.SelectedLanguageFileName = LanguageElement.Con_English Then
                mTempValue = strMessage2
                If mTempValue = "" Then
                    mTempValue = strMessage
                End If
            Else
                mTempValue = strMessage
            End If
            Return mTempValue
        End Get
    End Property

    Public Property Message As String

        Set(ByVal value As String)
            SyncLock _Object
                strMessage = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strMessage
            End SyncLock
        End Get
    End Property

    Public Property Message2 As String

        Set(ByVal value As String)
            SyncLock _Object
                strMessage2 = value
            End SyncLock
        End Set

        Get
            SyncLock _Object
                Return strMessage2
            End SyncLock
        End Get
    End Property

    Sub New(ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.cSystemElement = cSystemElement
            cLanguageManager = CType(cSystemElement(Language.Name), Language)
        End SyncLock
    End Sub


    Sub New(ByVal strID As String, ByVal iKey As Integer, ByVal strMessage As String, ByVal strMessage2 As String, ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.strID = strID
            Me.iKey = iKey
            Me.strMessage = strMessage
            Me.strMessage2 = strMessage2
            Me.cSystemElement = cSystemElement
            cLanguageManager = CType(cSystemElement(Language.Name), Language)
        End SyncLock
    End Sub
End Class

Public Class clsPlcMessageData
    Private cIniHandler As New clsIniHandler
    Private cSystemElement As Dictionary(Of String, Object)
    Private cLanguageManager As Language
    Private _Object As New Object
    Private _AppSettings As Settings


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



    Public Function LoadPlcMessage(ByRef lPlcMessageList As List(Of clsPlcMessageCfg)) As Boolean
        SyncLock _Object
            Try
                lPlcMessageList.Clear()
                Dim cPlcMessageElement As clsPlcMessageCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(_AppSettings.ConfigFolder + "PlcMessage.ini", "PlcMessage", New String() {"ID", "KeyName", "Message", "Message2"})
                    cPlcMessageElement = New clsPlcMessageCfg(cSystemElement)
                    If CType(element("ID"), String) <> "" Then
                        cPlcMessageElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("KeyName"), String) <> "" Then
                        cPlcMessageElement.Key = CType(element("KeyName"), String)
                    End If
                    If CType(element("Message"), String) <> "" Then
                        cPlcMessageElement.Message = CType(element("Message"), String)
                    End If
                    If CType(element("Message2"), String) <> "" Then
                        cPlcMessageElement.Message2 = CType(element("Message2"), String)
                    End If
                    lPlcMessageList.Add(cPlcMessageElement)
                Next
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function



    Public Function SavePlcMessage(ByVal lPlcMessageList As List(Of clsPlcMessageCfg)) As Boolean
        SyncLock _Object
            Try
                Dim i As Integer = 1
                Dim lListValue As New List(Of String)
                For Each element As clsPlcMessageCfg In lPlcMessageList
                    lListValue.Add("[PlcMessage" + i.ToString + "]")
                    lListValue.Add("ID=" + element.ID.ToString)
                    lListValue.Add("KeyName=" + element.Key.ToString)
                    lListValue.Add("Message=" + element.Message.ToString)
                    lListValue.Add("Message2=" + element.Message2.ToString)
                    i = i + 1
                Next
                cIniHandler.SaveIniFile(_AppSettings.ConfigFolder + "PlcMessage.ini", lListValue)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class
