Imports Kochi.HMI.MainControl.UI
Imports System.Collections.Concurrent

Public Class clsTextManager
    Public Const Name As String = "TextManager"
    Private lTextList As New List(Of clsTextCfg)
    Private cTextData As New clsTextData
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
                cTextData.Init(cSystemElement)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function GetTextListKey() As List(Of Integer)
        SyncLock _Object
            Try
                Dim lList As New List(Of Integer)
                For i = 0 To lTextList.Count - 1
                    lList.Add(i)
                Next
                Return lList
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetTextCfgFromKey(ByVal iKey As Integer) As clsTextCfg
        SyncLock _Object
            Try
                If iKey <= lTextList.Count - 1 Then
                    Return lTextList(iKey)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function GetTextCfgFromKey(ByVal strKey As String) As clsTextCfg
        SyncLock _Object
            Try
                If Not lTextList.Any(Function(e) e.Key = strKey) Then
                    Return Nothing
                End If
                Return lTextList.Where(Function(e) e.Key = strKey).First()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                cTextData.InSertData(strID, strKey, strMessage, strMessage2)
                lTextList.Add(New clsTextCfg(strID, strKey, strMessage, strMessage2, cSystemElement))
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
                cTextData.ModifyData(strID, strKey, strMessage, strMessage2)
                For Each element As clsTextCfg In lTextList
                    If element.ID = strID Then
                        element.Key = strKey
                        element.Message = strMessage
                        element.Message2 = strMessage2
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

    Public Function HasText(ByVal strKey As String) As Boolean
        SyncLock _Object
            Try
                If lTextList.Any(Function(e) e.Key = strKey) Then
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

    Public Function HasText(ByVal strKey As String, ByVal strID As String) As Boolean
        SyncLock _Object
            Try

                If lTextList.Any(Function(e) e.Key = strKey And e.ID <> strID) Then
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
                lTextList.RemoveAt(CInt(strID) - 1)
                ChangeID()
                cTextData.SaveText(lTextList)
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function LoadTextCfg() As Boolean
        SyncLock _Object
            Try
                cTextData.LoadText(lTextList)
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
                Dim dt As DataTable = New DataTable("TextTable")
                dt.Columns.Add(cLanguageManager.GetTextLine(clsTextManager.Name, "ID"))
                dt.Columns.Add(cLanguageManager.GetTextLine(clsTextManager.Name, "KEY"))
                If cLanguageManager.SecondLanguageEnable Then
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsTextManager.Name, "Message", cLanguageManager.GetTextLine("Language", cLanguageManager.FirtLanguage)))
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsTextManager.Name, "Message2", cLanguageManager.GetTextLine("Language", cLanguageManager.SecondLanguage)))
                Else
                    dt.Columns.Add(cLanguageManager.GetTextLine(clsTextManager.Name, "Message3"))
                End If
                For Each element As clsTextCfg In lTextList
                    If cListSearchContion.Count >= 1 Then
                        If cListSearchContion(0) <> "" Then
                            If element.Key.IndexOf(cListSearchContion(0)) < 0 Then
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
                    If cLanguageManager.SecondLanguageEnable Then
                        dt.Rows.Add(New String() {element.ID, element.Key, element.Message, element.Message2})
                    Else

                        dt.Rows.Add(New String() {element.ID, element.Key, element.Message})
                    End If
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
                For i = 0 To lTextList.Count - 1
                    lTextList(i).ID = j.ToString
                    j = j + 1
                Next
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
            End Try
        End SyncLock
    End Function

End Class

Public Class clsTextCfg
    Private strID As String = String.Empty
    Private strKey As String = String.Empty
    Private strMessage As String = String.Empty
    Private strMessage2 As String = String.Empty
    Private cLanguageManager As clsLanguageManager
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

    Sub New(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String, ByVal cSystemElement As Dictionary(Of String, Object))
        SyncLock _Object
            Me.strID = strID
            Me.strKey = strKey
            Me.strMessage = strMessage
            Me.strMessage2 = strMessage2
            cLanguageManager = cSystemElement(clsLanguageManager.Name)
        End SyncLock
    End Sub
End Class

Public Class clsTextData
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


    Public Function InSertData(ByVal strID As String, ByVal strKey As String, ByVal strMessage As String, ByVal strMessage2 As String) As Boolean
        SyncLock _Object
            Try
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.TextListConfig, "Text" + strID, New String() {"ID", "KeyName", "Message", "Message2"},
                                      New String() {strID, strKey, strMessage, strMessage2})
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
                Return cIniHandler.SetAnyListToIni(cSystemManager.Settings.TextListConfig, "Text" + strID, New String() {"ID", "KeyName", "Message", "Message2"},
                                      New String() {strID, strKey, strMessage, strMessage2})
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
                Return cIniHandler.RemoveSection(cSystemManager.Settings.TextListConfig, "Text" + strID)
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function LoadText(ByRef lTextList As List(Of clsTextCfg)) As Boolean
        SyncLock _Object
            Try
                lTextList.Clear()
                Dim cTextElement As clsTextCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(cSystemManager.Settings.TextListConfig, "Text", New String() {"ID", "KeyName", "Message", "Message2"})
                    cTextElement = New clsTextCfg(cSystemElement)
                    If CType(element("ID"), String) <> clsXmlHandler.s_DEFAULT And CType(element("ID"), String) <> clsXmlHandler.s_Null Then
                        cTextElement.ID = CType(element("ID"), String)
                    End If
                    If CType(element("KeyName"), String) <> clsXmlHandler.s_DEFAULT And CType(element("KeyName"), String) <> clsXmlHandler.s_Null Then
                        cTextElement.Key = CType(element("KeyName"), String)
                    End If
                    If CType(element("Message"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Message"), String) <> clsXmlHandler.s_Null Then
                        cTextElement.Message = CType(element("Message"), String)
                    End If
                    If CType(element("Message2"), String) <> clsXmlHandler.s_DEFAULT And CType(element("Message2"), String) <> clsXmlHandler.s_Null Then
                        cTextElement.Message2 = CType(element("Message2"), String)
                    End If
                    lTextList.Add(cTextElement)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function



    Public Function SaveText(ByVal lTextList As List(Of clsTextCfg)) As Boolean
        SyncLock _Object
            Try
                cIniHandler.RemoveAllSection(cSystemManager.Settings.TextListConfig, "Text")
                For Each element As clsTextCfg In lTextList
                    InSertData(element.ID, element.Key, element.Message, element.Message2)
                Next
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

End Class
