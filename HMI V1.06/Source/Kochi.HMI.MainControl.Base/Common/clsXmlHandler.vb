Imports System.Xml
Imports System.Collections.Concurrent

Public Class clsXmlHandler
    Public Const s_DEFAULT As String = "#ERROR#"
    Public Const s_Null As String = ""
    Protected _FileHander As New clsFileHandler
    Protected _doc As New XmlDocument
    Protected _rootElement As XmlElement
    Protected _nodes As XmlNodeList
    Protected _root As XmlNode
    Protected _subNodes As XmlNodeList
    Protected _subNextNodes As XmlNodeList
    Protected _Object As New Object
    Protected _rootElementName As String = "HMIConfiguration/"

    Public Function GetAnyListFromXml(ByVal s_FileName As String, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String) As List(Of Object)
        SyncLock _Object
            Try
                Dim _ListDictionary As New List(Of Object)
                Dim _TagValue As Dictionary(Of String, Object)
                If Not _FileHander.FileExist(s_FileName) Then
                    Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                    Throw New Exception(msg)
                End If

                _doc.Load(s_FileName)
                _rootElement = _doc.DocumentElement
                _nodes = _rootElement.GetElementsByTagName(s_NodeName)
                If Not IsNothing(_nodes) Then
                    For Each _node As XmlNode In _nodes
                        _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_SubNodeName)
                        For Each _nodeList As XmlNode In _subNodes
                            _TagValue = New Dictionary(Of String, Object)
                            For i = 0 To s_TagName.Length - 1
                                _TagValue.Add(s_TagName.GetValue(i).ToString, CType(_nodeList, XmlElement).GetElementsByTagName(s_TagName.GetValue(i).ToString)(0).InnerText)
                            Next
                            _ListDictionary.Add(_TagValue)
                        Next

                    Next
                End If
                Return _ListDictionary
            Catch ex As Exception
                Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
                Throw New Exception(msg)
                Return Nothing
            End Try
        End SyncLock
    End Function


    Public Function SetAnyListToXml(ByVal s_Path As String, ByVal s_FileName As String, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String, ByVal s_TagValue() As String) As Boolean
        'Try
        '    Dim _ListDictionary As New List(Of Object)
        '    Dim iNowIndex As Integer = 0
        '    If s_FileName.IndexOf(".xml") >= 0 Then
        '        _ExtFile = ""
        '    Else
        '        _ExtFile = ".xml"
        '    End If

        '    If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
        '        Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
        '        Throw New Exception(msg)
        '    End If

        '    If s_TagName.Length <> s_TagValue.Length Then
        '        Dim msg As String = String.Format("Error File {0}. s_TagName.Length: {1}. s_TagValue.Length: {2}", s_FileName, s_TagName.Length.ToString, s_TagValue.Length.ToString)
        '        Throw New Exception(msg)
        '    End If

        '    _doc.Load(s_Path + s_FileName + _ExtFile)
        '    _rootElem = _doc.DocumentElement
        '    _nodes = _rootElem.GetElementByTagName(s_NodeName)
        '    For Each _node As XmlNode In _nodes
        '        _subNodes = CType(_node, XmlElement).GetElementByTagName(s_SubNodeName)

        '        For Each _nodeList As XmlNode In _subNodes
        '            For i = 0 To s_TagName.Length - 1
        '                CType(_nodeList, XmlElement).GetElementByTagName(s_TagName.GetValue(i).ToString)(0).InnerText = s_TagValue.GetValue(i).ToString
        '            Next
        '        Next
        '    Next
        '    _doc.Save(s_Path + s_FileName + _ExtFile)
        '    Return True
        'Catch ex As Exception
        '    Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
        '    Throw New Exception(msg)
        '    Return Nothing
        'End Try
        Return True
    End Function


    Public Function InsertAnyListToXml(ByVal s_FileName As String, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String, ByVal s_TagValue() As String) As Boolean
        SyncLock _Object
            Try
                Dim _ListDictionary As New List(Of Object)
                Dim xe As XmlElement
                Dim xesub As XmlElement

                If Not _FileHander.FileExist(s_FileName) Then
                    Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                    Throw New Exception(msg)
                End If

                If s_TagName.Length <> s_TagValue.Length Then
                    Dim msg As String = String.Format("Error File {0}. s_TagName.Length: {1}. s_TagValue.Length: {2}", s_FileName, s_TagName.Length.ToString, s_TagValue.Length.ToString)
                    Throw New Exception(msg)
                End If

                _doc.Load(s_FileName)
                _root = _doc.SelectSingleNode(_rootElementName + s_NodeName)
                If IsNothing(_root) Then
                    _root = _doc.CreateElement(s_NodeName)
                End If
                xe = _doc.CreateElement(s_SubNodeName)

                For i = 0 To s_TagName.Length - 1
                    xesub = _doc.CreateElement(s_TagName.GetValue(i).ToString)
                    xesub.InnerText = s_TagValue.GetValue(i).ToString
                    xe.AppendChild(xesub)
                Next
                _root.AppendChild(xe)
                _doc.Save(s_FileName)
                Return True
            Catch ex As Exception
                Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
                Throw New Exception(msg)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveNode(ByVal s_FileName As String, ByVal s_NodeName As String) As Boolean
        SyncLock _Object
            Try
                Dim _ListDictionary As New List(Of Object)

                If Not _FileHander.FileExist(s_FileName) Then
                    Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                    Throw New Exception(msg)
                End If
                _doc.Load(s_FileName)
                _rootElement = _doc.DocumentElement
                _root = _rootElement.SelectSingleNode(s_NodeName)
                If Not IsNothing(_root) Then
                    _root.RemoveAll()
                End If
                _doc.Save(s_FileName)
                Return True
            Catch ex As Exception
                Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
                Throw New Exception(msg)
                Return Nothing
            End Try
        End SyncLock
    End Function

    Public Function RemoveItem(ByVal s_FileName As String, ByVal s_NodeName As String) As Boolean
        SyncLock _Object
            Try
                Dim _ListDictionary As New List(Of Object)

                If Not _FileHander.FileExist(s_FileName) Then
                    Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                    Throw New Exception(msg)
                End If
                _doc.Load(s_FileName)
                _root = _doc.SelectSingleNode(_rootElementName + s_NodeName)
                If _root Is Nothing Then Return True
                _root.ParentNode.RemoveChild(_root)
                _doc.Save(s_FileName)
                Return True
            Catch ex As Exception
                Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
                Throw New Exception(msg)
                Return Nothing
            End Try
        End SyncLock
    End Function
End Class
