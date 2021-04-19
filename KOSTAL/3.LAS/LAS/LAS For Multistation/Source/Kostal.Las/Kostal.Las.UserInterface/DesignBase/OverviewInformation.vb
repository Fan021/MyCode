Imports System.Reflection
Imports System.Collections.Generic
Imports Kostal.Las.UserInterface
Imports Kostal.Las.Base
Imports System.Xml

Public Class OverviewInformation
    Implements IEnumerable(Of StationInformation)

    Private _OverviewInfosDictionary As Dictionary(Of String, StationInformation)


    Public Sub New(ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal MySettings As Settings)

        _OverviewInfosDictionary = New Dictionary(Of String, StationInformation)


        Dim _FileHander As New FileHandler
        Dim s_FileName As String = MySettings.ConfigFolder + "\Las.xml"
        Dim _doc As New XmlDocument
        Dim _rootElem As XmlElement
        Dim _nodes As XmlNodeList
        Dim _subNodes As XmlNodeList
        _OverviewInfosDictionary.Clear()
        If Not _FileHander.FileExist(s_FileName) Then
            Dim msg As String = String.Format("Error loading {0}. The document exists but it might be not-well-formed. Error Message: {1}", s_FileName, "Open Fail")
            Throw New Exception(msg)
        End If
        _doc.Load(s_FileName)
        _rootElem = _doc.DocumentElement
        _nodes = _rootElem.GetElementsByTagName("StationStatusViews")
        For Each _node As XmlNode In _nodes
            _subNodes = CType(_node, XmlElement).GetElementsByTagName("StationStatusView")
            For Each _nodeList As XmlNode In _subNodes
                _OverviewInfosDictionary.Add(CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText, New StationInformation(CType(_nodeList, XmlElement).GetElementsByTagName("Name")(0).InnerText))

            Next
        Next
    End Sub

    Public Function GetStationInfo(ByVal key As String) As StationInformation

        Try
            Return _OverviewInfosDictionary.Item(key)
        Catch ex As ArgumentNullException
            ex.PreserveStackTrace()
        Catch ex As KeyNotFoundException
            ex.PreserveStackTrace()
        End Try
        Return Nothing

    End Function

    Public Function SetStationInfo(ByVal key As String, ByVal value As StationInformation) As Boolean

        Try
            If _OverviewInfosDictionary.Keys.Contains(key) Then
                _OverviewInfosDictionary.Item(key) = value
                Return True
            End If
        Catch ex As ArgumentNullException
            ex.PreserveStackTrace()
        Catch ex As KeyNotFoundException
            ex.PreserveStackTrace()
        End Try
        Return False

    End Function

    Public Function GetStationInfos() As IEnumerable(Of StationInformation)

        Return _OverviewInfosDictionary.Values.ToArray

    End Function

    Public Function VGetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator

        Return GetEnumerator()

    End Function

    Public Function GetEnumerator() As IEnumerator(Of StationInformation) Implements IEnumerable(Of StationInformation).GetEnumerator

        Return _OverviewInfosDictionary.Values.GetEnumerator

    End Function

    Public ReadOnly Property GetOverviewDictionary As Dictionary(Of String, StationInformation)

        Get
            Return _OverviewInfosDictionary
        End Get

    End Property

End Class
