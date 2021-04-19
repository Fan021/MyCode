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

Public Class XmlHandler
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


    Public Function GetSubStationCfgs(ByVal s_Path As String, ByVal s_FileName As String) As Dictionary(Of String, StationElement)
        Try

            Dim _TempCfg As New StationElement
            Dim _SubStationCfg As New SubStationCfg
            Dim _ListDictionary As New Dictionary(Of String, StationElement)
            Dim _subAdsInputsNodes As XmlNodeList
            Dim _subAdsInputNodes As XmlNodeList
            Dim _subAdsOutputsNodes As XmlNodeList
            Dim _subAdsOutputNodes As XmlNodeList
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
            _nodes = _rootElem.GetElementsByTagName("Stations")
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("Station")
                For Each _nodeList As XmlNode In _subNodes
                    _TempCfg = New StationElement
                    If CType(_nodeList, XmlElement).GetAttribute("Visible").ToUpper <> "TRUE" Then Continue For
                    _TempCfg.Name = CType(_nodeList, XmlElement).GetAttribute("Name")
                    If _TempCfg.Name = "" Then
                        Dim msg As String = String.Format("Staion Name is Null")
                        Throw New Exception(msg)
                    End If

                    _subNextNodes = CType(_nodeList, XmlElement).GetElementsByTagName("Substation")
                    For Each _nodeNextList As XmlNode In _subNextNodes
                        _SubStationCfg = New SubStationCfg
                        _SubStationCfg.Station = _TempCfg.Name
                        _SubStationCfg.Name = CType(_nodeNextList, XmlElement).GetAttribute("Name")
                        If _SubStationCfg.Name = "" Then
                            Dim msg As String = String.Format("Staion:{0} The SubStation name is Null", _TempCfg.Name)
                            Throw New Exception(msg)
                        End If
                        _SubStationCfg.TypeName = CType(_nodeNextList, XmlElement).GetElementsByTagName("Type")(0).InnerText
                        _SubStationCfg.Type = StationCfg.ChangeStringToLasType(_SubStationCfg.TypeName)
                        _SubStationCfg.Inteface = StationCfg.ChangeTypeToInface(_SubStationCfg.Type)
                        _SubStationCfg.Enable = CBool(IIf(CType(_nodeNextList, XmlElement).GetElementsByTagName("Enable")(0).InnerText.ToUpper = "TRUE", True, False))
                        _SubStationCfg.Config = CType(_nodeNextList, XmlElement).GetElementsByTagName("Config")(0).InnerText
                        If CType(_nodeNextList, XmlElement).GetElementsByTagName("Repeat")(0).InnerText <> XmlHandler.s_DEFAULT And CType(_nodeNextList, XmlElement).GetElementsByTagName("Repeat")(0).InnerText <> XmlHandler.s_Null Then
                            _SubStationCfg.Repeat = Integer.Parse(CType(_nodeNextList, XmlElement).GetElementsByTagName("Repeat")(0).InnerText)
                        Else
                            _SubStationCfg.Repeat = 0
                        End If
                        Try
                            _SubStationCfg.MainDevice = CType(_nodeNextList, XmlElement).GetElementsByTagName("MainDevice")(0).InnerText
                        Catch
                            _SubStationCfg.MainDevice = CType(_nodeNextList, XmlElement).GetElementsByTagName("Owner")(0).InnerText
                        End Try

                        _SubStationCfg.PLCName = CType(_nodeNextList, XmlElement).GetElementsByTagName("PLCName")(0).InnerText

                        _subAdsInputsNodes = CType(_nodeNextList, XmlElement).GetElementsByTagName("AdsInputs")
                        For Each _nodeAdsInputsList As XmlNode In _subAdsInputsNodes
                            _subAdsInputNodes = CType(_nodeAdsInputsList, XmlElement).GetElementsByTagName("AdsInput")
                            For Each _nodeAdsInputList As XmlNode In _subAdsInputNodes
                                If CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText <> XmlHandler.s_DEFAULT And CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText <> XmlHandler.s_Null Then
                                    If _ListDictionary.ContainsKey(CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText) Then
                                        Dim msg As String = String.Format("The station:{0} SubStation:{1} have add the same AdsInput key:{3}", _TempCfg.Name, _SubStationCfg.Name, CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                        Throw New Exception(msg)
                                    End If
                                    If CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText.IndexOf(".") < 0 Then
                                        _SubStationCfg.AdsInput.Add("." + CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                    Else
                                        _SubStationCfg.AdsInput.Add(CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                    End If

                                End If
                            Next
                        Next
                        _subAdsOutputsNodes = CType(_nodeNextList, XmlElement).GetElementsByTagName("AdsOutputs")
                        For Each _nodeAdsOutputsList As XmlNode In _subAdsOutputsNodes
                            _subAdsOutputNodes = CType(_nodeAdsOutputsList, XmlElement).GetElementsByTagName("AdsOutput")
                            For Each _nodeAdsOutputList As XmlNode In _subAdsOutputNodes
                                If CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText <> XmlHandler.s_DEFAULT And CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText <> XmlHandler.s_Null Then
                                    If _ListDictionary.ContainsKey(CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText) Then
                                        Dim msg As String = String.Format("The station:{0} SubStation:{1} have add the same AdsOutput key:{3}", _TempCfg.Name, _SubStationCfg.Name, CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                        Throw New Exception(msg)
                                    End If

                                    If CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText.IndexOf(".") < 0 Then
                                        _SubStationCfg.AdsOutput.Add("." + CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                    Else
                                        _SubStationCfg.AdsOutput.Add(CType(_nodeAdsOutputList, XmlElement).GetElementsByTagName("Name")(0).InnerText)
                                    End If
                                End If
                            Next
                        Next
                        If _TempCfg.SubStation.ContainsKey(_SubStationCfg.Name) Then
                            Dim msg As String = String.Format("The Station:{0} have add the same Substation:{1}", _TempCfg.Name, _SubStationCfg.Name)
                            Throw New Exception(msg)
                        End If
                        _TempCfg.SubStation.Add(_SubStationCfg.Name, _SubStationCfg)
                    Next
                    If _ListDictionary.ContainsKey(_TempCfg.Name) Then
                        Dim msg As String = String.Format("The  same station:{0} have exist", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If
                    _ListDictionary.Add(_TempCfg.Name, _TempCfg)
                Next

            Next
            Return _ListDictionary
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function


    Public Function GetParameterCfgs(ByVal s_Path As String, ByVal s_FileName As String) As Dictionary(Of String, clsParameterCfg)
        Try
            Dim _TempCfg As New clsParameterCfg
            Dim _ListDictionary As New Dictionary(Of String, clsParameterCfg)
            Dim _subAdsInputsNodes As XmlNodeList
            Dim _subAdsInputNodes As XmlNodeList
            If s_FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If
            _ListDictionary.Clear()
            If Not _FileHander.FileExist(s_Path + s_FileName + _ExtFile) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If
            _doc.Load(s_Path + s_FileName + _ExtFile)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("Parameters")
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("Parameter")

                For Each _nodeList As XmlNode In _subNodes
                    _TempCfg = New clsParameterCfg
                    Dim mTemp As String = String.Empty
                    _TempCfg.Name = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    If _TempCfg.Name = "" Then
                        Dim msg As String = String.Format("name is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("Type")(0).InnerText
                    If mTemp = "" Then
                        Dim msg As String = String.Format("Name:" + _TempCfg.Name + " Type is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("Enable")(0).InnerText
                    _TempCfg.Enable = CType(IIf(mTemp.ToUpper = "TRUE", True, False), Boolean)

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("Type")(0).InnerText
                    _TempCfg.Type = clsParameterCfg.ChangeStringToLasType(mTemp)
                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("Value")(0).InnerText
                    _TempCfg.Value = mTemp

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("AdsName")(0).InnerText
                    If mTemp = "" Then
                        Dim msg As String = String.Format("Name:" + _TempCfg.Name + " AdsName is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If
                    _TempCfg.AdsName = mTemp

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("VariantChange")(0).InnerText
                    If mTemp = "" Then
                        Dim msg As String = String.Format("Name:" + _TempCfg.Name + " VariantChange is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If
                    _TempCfg.VariantChange = CType(IIf(mTemp.ToUpper = "TRUE", True, False), Boolean)

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("VariantElement")(0).InnerText
                    If mTemp = "" Then
                        Dim msg As String = String.Format("Name:" + _TempCfg.Name + " VariantElement is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If
                    _TempCfg.VariantElement = mTemp

                    mTemp = CType(_nodeList, XmlElement).GetElementsByTagName("PLC")(0).InnerText
                    If mTemp = "" Then
                        Dim msg As String = String.Format("Name:" + _TempCfg.Name + " PLC is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If
                    _TempCfg.PLC = mTemp

                    _subAdsInputsNodes = CType(_nodeList, XmlElement).GetElementsByTagName("Lists")
                    For Each _nodeAdsInputsList As XmlNode In _subAdsInputsNodes
                        _subAdsInputNodes = CType(_nodeAdsInputsList, XmlElement).GetElementsByTagName("List")
                        For Each _nodeAdsInputList As XmlNode In _subAdsInputNodes
                            mTemp = CType(_nodeAdsInputList, XmlElement).GetElementsByTagName("ListValue")(0).InnerText
                            If mTemp = "" Then
                                Dim msg As String = String.Format("Name:" + _TempCfg.Name + " List is Null", _TempCfg.Name)
                                Throw New Exception(msg)
                            End If
                            _TempCfg.ListValue.Add(mTemp)
                        Next
                    Next
                    If Not _ListDictionary.ContainsKey(_TempCfg.Name) And _TempCfg.Enable Then
                        _ListDictionary.Add(_TempCfg.Name, _TempCfg)
                    End If
                Next

            Next
            Return _ListDictionary
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
        End Try
    End Function

    Public Function SaveParameterCfgs(ByVal s_Path As String, ByVal s_FileName As String, ByVal ListValue As Dictionary(Of String, clsParameterCfg)) As Boolean
        Try
            Dim _TempCfg As New clsParameterCfg
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
            _nodes = _rootElem.GetElementsByTagName("Parameters")
            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("Parameter")
                _TempCfg = New clsParameterCfg
                For Each _nodeList As XmlNode In _subNodes
                    Dim mTemp As String = String.Empty
                    _TempCfg.Name = CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText
                    If _TempCfg.Name = "" Then
                        Dim msg As String = String.Format("name is Null", _TempCfg.Name)
                        Throw New Exception(msg)
                    End If

                    If ListValue.ContainsKey(_TempCfg.Name) Then CType(_nodeList, XmlElement).GetElementsByTagName("Value")(0).InnerText = ListValue(_TempCfg.Name).Value
                Next

            Next
                _doc.Save(s_Path + s_FileName + _ExtFile)
            Return True
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
            Return Nothing
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




    Public Function HasOverView(ByVal Path As String, ByVal FileName As String) As Boolean

        Try
            Dim _FileHander As New FileHandler
            If FileName.IndexOf(".xml") >= 0 Then
                _ExtFile = ""
            Else
                _ExtFile = ".xml"
            End If
            Dim s_FileName As String = Path + FileName + _ExtFile


            Dim _doc As New XmlDocument
            Dim _rootElem As XmlElement
            Dim _nodes As XmlNodeList
            Dim _subNodes As XmlNodeList
            If Not _FileHander.FileExist(s_FileName) Then
                Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
                Throw New Exception(msg)
            End If

            _doc.Load(s_FileName)
            _rootElem = _doc.DocumentElement
            _nodes = _rootElem.GetElementsByTagName("StationStatusViews")

            For Each _node As XmlNode In _nodes
                _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationStatusView")
                If _subNodes.Count > 0 Then
                    Return True
                End If

            Next
            Return False
        Catch ex As Exception
            Dim msg As String = String.Format("Get SubStation Fail. Error Message: {0}", ex.Message)
            Throw New Exception(msg)
        End Try
        Return False
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
