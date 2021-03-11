Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsErrorCodeManager
    Public Const Name As String = "ErrorCodeManager"
    Private lErrorCodeList As New List(Of clsErrorCodeCfg)
    Private cErrorCodeData As New clsErrorCodeData
    Private cDataGridViewPage As clsDataGridViewPage
    Private cLanguageManager As clsLanguageManager
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

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
                cLanguageManager = CType(cSystemElement(clsLanguageManager.Name), clsLanguageManager)
                cErrorCodeData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetErrorCodeListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lErrorCodeList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function GetErrorCfgFromCode(ByVal strCode As String) As clsErrorCodeCfg
        SyncLock _Object
            Try
                If Not HasErrorCode(strCode) Then
                    Return Nothing
                End If
                Return lErrorCodeList.Where(Function(e) e.Key = strCode).FirstOrDefault
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetErrorCodeCfgFromKey(ByVal iKey As Integer) As clsErrorCodeCfg
        SyncLock _Object
            Try
                If iKey <= lErrorCodeList.Count - 1 Then
                    Return lErrorCodeList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                lErrorCodeList.Add(New clsErrorCodeCfg(strID, strKey, strMessage, strMessage2, cSystemElement))
                ChangeID()
                cErrorCodeData.SaveErrorCode(lErrorCodeList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function ModifyData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                For Each element As clsErrorCodeCfg In lErrorCodeList
                    If element.ID = strID Then
                        element.Key = strKey
                        element.Message = strMessage
                        element.Message2 = strMessage2
                    End If
                Next
                ChangeID()
                cErrorCodeData.SaveErrorCode(lErrorCodeList)
                Return True

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function HasErrorCode(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lErrorCodeList.Any(Function(e) e.Key = strKey) Then
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

    Public Function HasErrorCode(ByVal strKey As String, ByVal strID As String) As Boolean
        SyncLock _Object
            Try

                If lErrorCodeList.Any(Function(e) e.Key = strKey And e.ID <> strID) Then
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
                lErrorCodeList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cErrorCodeData.SaveErrorCode(lErrorCodeList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadErrorCodeCfg() As Boolean
        SyncLock _Object
            Try
                cErrorCodeData.LoadErrorCode(lErrorCodeList)
                ChangeID()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function
    Public Function SelectToDataView(ByVal cViewPageType As enumViewPageType, ByVal strKey As String, ByVal ParamArray cListSearchContion() As String) As Boolean
        SyncLock _Object
            Try
                Dim Ds = New DataSet
                Dim dt As DataTable = New DataTable("ErrorCodeTable")
                Dim iCnt As Integer = 0
                Dim iKeyCnt As Integer = 0
                dt.Columns.Add(cLanguageManager.GetTextLine(clsErrorCodeManager.Name, "ID"))
                dt.Columns.Add(cLanguageManager.GetTextLine(clsErrorCodeManager.Name, "KEY"))
                If cLanguageManager.SecondLanguageEnable Then
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsErrorCodeManager.Name, "Message", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage)))
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsErrorCodeManager.Name, "Message2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage)))
                Else
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsErrorCodeManager.Name, "Message3"))
                End If
                For Each element As clsErrorCodeCfg In lErrorCodeList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.Key.ToString.IndexOf(cListSearchContion(0)) < 0 Then
                                Continue For
                            End If
                        End If
                    End If
                    If cListSearchContion.Count >= 2 Then
                        If cLanguageManager.SecondLanguageEnable Then
                            If cListSearchContion(1) <> "" Then
                                If element.Message.IndexOf(cListSearchContion(1)) < 0 And element.Message2.IndexOf(cListSearchContion(1)) < 0 Then
                                    Continue For
                                End If
                            End If
                        Else
                            If cListSearchContion(1) <> "" Then
                                If element.Message.IndexOf(cListSearchContion(1)) < 0 Then
                                    Continue For
                                End If
                            End If
                        End If
                    End If
                    iCnt = iCnt + 1
                    If element.Key.ToString = strKey Then
                        iKeyCnt = iCnt \ cDataGridViewPage.RowsPerPage
                    End If
                    If cLanguageManager.SecondLanguageEnable Then
                        dt.Rows.Add(New String() {element.ID, element.Key, element.Message, element.Message2})
                    Else

                        dt.Rows.Add(New String() {element.ID, element.Key, element.Message})
                    End If
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
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Function ChangeID() As Boolean
        SyncLock _Object
            Try
                lErrorCodeList.Sort(Function(x As clsErrorCodeCfg, y As clsErrorCodeCfg) x.Key.CompareTo(y.Key))
                Dim j As Integer = 1
                For i = 0 To lErrorCodeList.Count - 1
                    lErrorCodeList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

End Class

Public Class clsErrorCodeCfg
    Private strID As String = String.Empty
    Private iKey As Integer = 0
    Private strMessage As String = String.Empty
    Private strMessage2 As String = String.Empty
    Private _Object As New Object
    Private cLanguageManager As clsLanguageManager

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
            If cLanguageManager.SecondLanguageActive Then
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
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
        End SyncLock
    End Sub


    Sub New(ByVal strID As String, ByVal iKey As Integer, ByVal strMessage As String, ByVal strMessage2 As String, ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.strID = strID
            Me.iKey = iKey
            Me.strMessage = strMessage
            Me.strMessage2 = strMessage2
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
        End SyncLock
    End Sub
End Class

Public Class clsErrorCodeData
    Private cIniHandler As clsIniHandler
    Private cSystemManager As clsSystemManager
    Private _Object As New Object
    Private cSystemElement As Dictionary(Of String, Object)

    Public Function Init(ByVal cSystemElement As Dictionary(Of String, Object)) As Boolean
        SyncLock _Object
            Try
                Me.cSystemElement = cSystemElement
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


    Public Function LoadErrorCode(ByRef lErrorCodeList As List(Of clsErrorCodeCfg)) As Boolean
        SyncLock _Object
            Try
                lErrorCodeList.Clear()
                Dim cErrorCodeElement As clsErrorCodeCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.ErrorCodeListConfig, "ErrorCode", New String() {"ID", "KeyName", "Message", "Message2"})
                    cErrorCodeElement = New clsErrorCodeCfg(cSystemElement)
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cErrorCodeElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("KeyName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("KeyName"), String) <> clsXmlHandler.s_Null Then
                        cErrorCodeElement.Key = CType(element("KeyName"), String)
                    End If
                    If CType(element("Message"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Message"), String) <> clsXmlHandler.s_Null Then
                        cErrorCodeElement.Message = CType(element("Message"), String)
                    End If
                    If CType(element("Message2"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Message2"), String) <> clsXmlHandler.s_Null Then
                        cErrorCodeElement.Message2 = CType(element("Message2"), String)
                    End If
                    lErrorCodeList.Add(cErrorCodeElement)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function



    Public Function SaveErrorCode(ByVal lErrorCodeList As List(Of clsErrorCodeCfg)) As Boolean
        SyncLock _Object
            Try
                Dim i As Integer = 1
                Dim lListValue As New List(Of String)
                For Each element As clsErrorCodeCfg In lErrorCodeList
                    lListValue.Add("[ErrorCode" + i.ToString + "]")
                    lListValue.Add("ID=" + element.ID.ToString)
                    lListValue.Add("KeyName=" + element.Key.ToString)
                    lListValue.Add("Message=" + element.Message.ToString)
                    lListValue.Add("Message2=" + element.Message2.ToString)
                    i = i + 1
                Next
                cIniHandler.SaveIniFile(cSystemManager.Settings.ErrorCodeListConfig, lListValue)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class
