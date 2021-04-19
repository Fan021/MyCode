Option Explicit On
'StringFactory.vb
'Author : Space Wang
'Version 2015.07.02

Public Enum enumTarget_GetValue
    InternalArticle = 1
    CustomerDefinition
    CustomerRegex
End Enum

Public Enum enumString_Comparision
    Contains = 1
    ContainsAnyway    'True if A contains B or B contains A.
    Equals
    'Matchs
End Enum

Public Enum enumString_CutMethod
    PrefixSplitter = 1
    PrefixSuffix
    PrefixLength
    StartLength
    WholeString
End Enum

Public Class StringFactory

    Protected _prefix As String = ""
    Protected _suffix As String = ""
    Protected _splitter As Char = "/"c
    Protected _startIndex As Integer = 0
    Protected _length As Integer = 0
    Protected _sourceCutWay As enumString_CutMethod = enumString_CutMethod.WholeString
    Protected _stringsCompareMode As enumString_Comparision = enumString_Comparision.Equals

    Public Sub New(ByVal prefix As String, ByVal splitter As Char, Optional ByVal compareMode As enumString_Comparision = enumString_Comparision.Equals)

        _sourceCutWay = enumString_CutMethod.PrefixSplitter
        _stringsCompareMode = compareMode
        _prefix = prefix
        _splitter = splitter

    End Sub

    Public Sub New(ByVal prefix As String, ByVal suffix As String, Optional ByVal compareMode As enumString_Comparision = enumString_Comparision.Equals)

        _sourceCutWay = enumString_CutMethod.PrefixSuffix
        _stringsCompareMode = compareMode
        _prefix = prefix
        _suffix = suffix

    End Sub

    Public Sub New(ByVal prefix As String, ByVal lengthWithoutPrefix As Integer, Optional ByVal compareMode As enumString_Comparision = enumString_Comparision.Equals)

        _sourceCutWay = enumString_CutMethod.PrefixLength
        _stringsCompareMode = compareMode
        _prefix = prefix
        _length = lengthWithoutPrefix

    End Sub

    Public Sub New(ByVal startIndex As Integer, ByVal length As Integer, Optional ByVal compareMode As enumString_Comparision = enumString_Comparision.Equals)

        _sourceCutWay = enumString_CutMethod.StartLength
        _stringsCompareMode = compareMode
        If startIndex < 0 Or length < 0 Then Exit Sub
        _startIndex = startIndex
        _length = length

    End Sub

    Public Sub New(Optional ByVal compareMode As enumString_Comparision = enumString_Comparision.Contains)

        _sourceCutWay = enumString_CutMethod.WholeString
        _stringsCompareMode = compareMode

    End Sub

#Region "Properties"

    Public ReadOnly Property StringsCompareMode As enumString_Comparision
        Get
            Return _stringsCompareMode
        End Get
    End Property

    Public ReadOnly Property StringsCutMethod As enumString_CutMethod
        Get
            Return _sourceCutWay
        End Get
    End Property

#End Region

#Region "Functions"

    Public Shared Function CompareStrings(ByVal source As String, ByVal copareMode As enumString_Comparision, ByVal expected As String) As Boolean
        Dim bulRes As Boolean
        Dim strRes As String

        strRes = source
        Select Case copareMode
            Case enumString_Comparision.Contains
                bulRes = strRes.Contains(expected)
            Case enumString_Comparision.ContainsAnyway
                bulRes = strRes.Contains(expected) Or expected.Contains(strRes)
            Case enumString_Comparision.Equals
                bulRes = (strRes = expected)
        End Select

        Return bulRes

    End Function

    Public Function ProduceString(ByVal Line As String) As String
        Dim strRes As String = ""

        Select Case _sourceCutWay
            Case enumString_CutMethod.PrefixSplitter
                strRes = SetLineAndGetValue(Line, _prefix, _splitter)
            Case enumString_CutMethod.PrefixSuffix
                strRes = SetLineAndGetValue(Line, _prefix, _suffix)
            Case enumString_CutMethod.PrefixLength
                strRes = SetLineAndGetValue(Line, _prefix, _length)
            Case enumString_CutMethod.StartLength
                If Line.Length < _length Then
                    strRes = Line.Substring(_startIndex, Line.Length - _startIndex)
                Else
                    strRes = Line.Substring(_startIndex, _length)
                End If
            Case enumString_CutMethod.WholeString
                strRes = Line
        End Select

        Return strRes

    End Function

    Protected Function SetLineAndGetValue(ByVal Line As String, ByVal prefix As String, ByVal length As Integer) As String
        Dim prePos As Integer, strRes As String = ""

        prePos = InStr(Line, prefix)
        If prePos > 0 Then
            strRes = Line.Substring(prePos + prefix.Length - 1, length)
        End If

        Return strRes

    End Function

    Protected Function SetLineAndGetValue(ByVal Line As String, ByVal prefix As String, ByVal suffix As String) As String
        Dim prePos As Integer, postPos As Integer, strRes As String = ""

        prePos = InStr(Line, prefix)
        postPos = prePos + InStr(Line.Substring(prePos + prefix.Length - 1), suffix) - 1
        If postPos > prePos AndAlso postPos >= (prePos + prefix.Length) Then
            strRes = Line.Substring(prePos + prefix.Length - 1, postPos - prePos)
        ElseIf postPos <= prePos Then
            strRes = Line.Substring(prePos + prefix.Length - 1)
        End If

        Return strRes

    End Function

    Protected Function SetLineAndGetValue(ByVal Line As String, ByVal prefix As String, ByVal delimiter As Char) As String
        Dim _Data() As String, l As Integer, strRes As String = ""

        _Data = Line.Split(delimiter)
        If _Data.Length = 0 Then Return strRes
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            _Data(l) = delimiter & _Data(l)
        Next
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            If _Data(l).Length > prefix.Length Then
                If _Data(l).Substring(0, prefix.Length) = prefix Then
                    strRes = _Data(l).Substring(prefix.Length)
                    _Data(l) = ""
                    Exit For
                End If
            End If
        Next

        Return strRes

    End Function

#End Region

End Class