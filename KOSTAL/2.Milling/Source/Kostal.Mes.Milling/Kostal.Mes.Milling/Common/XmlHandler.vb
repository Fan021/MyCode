Imports System.Xml
Public Class HelpElement
    Protected _FileName As String
    Protected _Application As String
    Public Sub New(ByVal FileName As String, ByVal Application As String)
        _FileName = FileName
        _Application = Application
    End Sub
    Public Property FileName As String
        Set(ByVal value As String)
            _FileName = value
        End Set
        Get
            Return _FileName
        End Get
    End Property
    Public Property Application As String
        Set(ByVal value As String)
            _Application = value
        End Set
        Get
            Return _Application
        End Get
    End Property

End Class

Public Class CsvMapperElement
    Protected _CsvFileName As String
    Protected _MappingFileName As String
    Public Sub New(ByVal CsvFileName As String, ByVal MappingFileName As String)
        _CsvFileName = CsvFileName
        _MappingFileName = MappingFileName
    End Sub
    Public Property CsvFileName As String
        Set(ByVal value As String)
            _CsvFileName = value
        End Set
        Get
            Return _CsvFileName
        End Get
    End Property
    Public Property MappingFileName As String
        Set(ByVal value As String)
            _MappingFileName = value
        End Set
        Get
            Return _MappingFileName
        End Get
    End Property

End Class

Public Class CsvScheduleElement
    Protected _CsvFileName As String
    Public Sub New(ByVal CsvFileName As String)
        _CsvFileName = CsvFileName
    End Sub
    Public Property CsvFileName As String
        Set(ByVal value As String)
            _CsvFileName = value
        End Set
        Get
            Return _CsvFileName
        End Get
    End Property
End Class

Public Class LanguageFileElement
    Protected _Language As String
    Public Sub New(ByVal Language As String)
        _Language = Language
    End Sub
    Public Property Language As String
        Set(ByVal value As String)
            _Language = value
        End Set
        Get
            Return _Language
        End Get
    End Property


End Class

Public Class XmlHandler1
    Public Const s_DEFAULT As String = "#ERROR#"
    Public Const s_Null As String = ""
    Protected _FileHander As New FileHandler
    Protected _doc As New XmlDocument
    Protected _rootElem As XmlElement
    Protected _nodes As XmlNodeList
    Protected _subNodes As XmlNodeList
    Protected _subNextNodes As XmlNodeList
    Protected _ExtFile As String = ".xml"

    Public Function GetSectionInformation(ByVal s_Path As String, ByVal s_FileName As String, ByVal s_Section As String, ByVal s_KeyWord As String) As String
        Try
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If
            If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If
            _doc.Load(s_Path + s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_Section)
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_KeyWord)
                If _subNodes.Count = 1 Then
                    Return _subNodes(0).InnerText
                End If
            Next
            Return s_DEFAULT
        Catch ex As Exception
            Dim msg As String = String.Format("GetGeneralInformation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return s_DEFAULT
        End Try
    End Function

    Public Function GetSectionInformation(ByVal s_FileName As String, ByVal s_Section As String, ByVal s_KeyWord As String) As String
        Try
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If
            If Not _FileHander.FileExist(s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If
            _doc.Load(s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_Section)
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_KeyWord)
                If _subNodes.Count = 1 Then
                    Return _subNodes(0).InnerText
                End If
            Next
            Return s_DEFAULT
        Catch ex As Exception
            Dim msg As String = String.Format("GetGeneralInformation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return s_DEFAULT
        End Try
    End Function

    Public Function SetGeneralInformation(ByVal s_Path As String, ByVal s_FileName As String, ByVal s_Section As String, ByVal s_KeyWord As String, ByVal s_Value As String) As Boolean
        Try
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If
            If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If
            _doc.Load(s_Path + s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_Section)
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_KeyWord)
                If _subNodes.Count = 1 Then
                    _subNodes(0).InnerText = s_Value
                End If
            Next
            _doc.Save(s_Path + s_FileName + _ExtFile)
            Return True
        Catch ex As Exception
            Dim msg As String = String.Format("SetGeneralInformation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return False
        End Try
    End Function


    Public Function GetAnyListFromXml(ByVal s_Path As String, ByVal s_FileName As String, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String) As List(Of Object)
        Try
            Dim _ListDictionary As New List(Of Object)
            Dim _TagValue As Dictionary(Of String, Object)
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If

            If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_Path + s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_NodeName)
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
            Return _ListDictionary
        Catch ex As Exception
            Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function

    Public Function GetAnyListFromXml(ByVal s_FileName As String, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String) As List(Of Object)
        Try
            Dim _ListDictionary As New List(Of Object)
            Dim _TagValue As Dictionary(Of String, Object)
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If

            If Not _FileHander.FileExist(s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_NodeName)
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
            Return _ListDictionary
        Catch ex As Exception
            Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function


    Public Function SetAnyListFromXml(ByVal s_Path As String, ByVal s_FileName As String, ByVal iIndex As Integer, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String, ByVal s_TagValue() As String) As Boolean
        Try
            Dim _ListDictionary As New List(Of Object)
            Dim iNowIndex As Integer = 0
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If

            If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            If s_TagName.Length <> s_TagValue.Length Then
                Dim msg As String = String.Format("Error File {0}. s_TagName.Length: {1}. s_TagValue.Length: {2}", s_FileName, s_TagName.Length.ToString, s_TagValue.Length.ToString)
                Throw New Exception(msg)
            End If

            _doc.Load(s_Path + s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_NodeName)
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_SubNodeName)

                For Each _nodeList As XmlNode In _subNodes
                    If iNowIndex = iIndex Then
                        For i = 0 To s_TagName.Length - 1
                            CType(_nodeList, XmlElement).GetElementsByTagName(s_TagName.GetValue(i).ToString)(0).InnerText = s_TagValue.GetValue(i).ToString
                        Next
                    End If
                    iNowIndex = iNowIndex + 1
                Next
            Next
            _doc.Save(s_Path + s_FileName + _ExtFile)
            Return True
        Catch ex As Exception
            Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function

    Public Function SetAnyListFromXml(ByVal s_FileName As String, ByVal iIndex As Integer, ByVal s_NodeName As String, ByVal s_SubNodeName As String, ByVal s_TagName() As String, ByVal s_TagValue() As String) As Boolean
        Try
            Dim _ListDictionary As New List(Of Object)
            Dim iNowIndex As Integer = 0
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If

            If Not _FileHander.FileExist(s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            If s_TagName.Length <> s_TagValue.Length Then
                Dim msg As String = String.Format("Error File {0}. s_TagName.Length: {1}. s_TagValue.Length: {2}", s_FileName, s_TagName.Length.ToString, s_TagValue.Length.ToString)
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName(s_NodeName)
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName(s_SubNodeName)
                If iNowIndex = iIndex Then
                    For Each _nodeList As XmlNode In _subNodes
                        For i = 0 To s_TagName.Length - 1
                            CType(_nodeList, XmlElement).GetElementsByTagName(s_TagName.GetValue(i).ToString)(0).InnerText = s_TagValue.GetValue(i).ToString
                        Next
                    Next
                End If

                iNowIndex = iNowIndex + 1

            Next
            Return True
        Catch ex As Exception
            Dim msg As String = String.Format("GetCsvs GetAnyListFromXml. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function
End Class
