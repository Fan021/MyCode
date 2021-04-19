
'Barcode Definitionen

'Author		Frank Dümpelmann
'Version	2.0.0.0
'Build		2010_02_11


Public Class Barcode_LK

    Protected _Elements As New Dictionary(Of String, String)
    Protected Const _DELIMITER As Char = "/"c

    Protected Const _PREFIX_LK_NUMBER As String = "/3OS"
    Protected Const _PREFIX_SERIAL_NUMBER As String = "/SN"
    Protected Const _PREFIX_CUSTOMER_NUMBER As String = "/P"
    Protected Const _PREFIX_HW_INDEX As String = "/HW"                  'Harware Index
    Protected Const _PREFIX_SW_INDEX As String = "/SW"                  'Software Index
    Protected Const _PREFIX_AS_INDEX As String = "/AS"
    Protected Const _PREFIX_AI_INDEX As String = "/AI"
    Protected Const _PREFIX_ZI_INDEX As String = "/ZI"                  'Drawing Index
    Protected Const _PREFIX_QIE As String = "/KS"                           'Quality Index
    Protected Const _PREFIX_ME_INDEX As String = "/ME"                  'Mechanic Index
    Protected Const _PREFIX_CUSTOMER_COLOR_CODE As String = "/CC"
    Protected Const _PREFIX_CUSTOMER_SERIAL_NUMBER As String = "/CSN"
    Protected Const _PREFIX_NGS As String = "/Y"

#Region "Properties"

    Public ReadOnly Property PREFIX_LK_NUMBER() As String
        Get
            Return _PREFIX_LK_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_SERIAL_NUMBER() As String
        Get
            Return _PREFIX_SERIAL_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_NUMBER() As String
        Get
            Return _PREFIX_CUSTOMER_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_HW_INDEX() As String
        Get
            Return _PREFIX_HW_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_SW_INDEX() As String
        Get
            Return _PREFIX_SW_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_ME_INDEX() As String
        Get
            Return _PREFIX_ME_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_AS_INDEX() As String
        Get
            Return _PREFIX_AS_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_AI_INDEX() As String
        Get
            Return _PREFIX_AI_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_ZI_INDEX() As String
        Get
            Return _PREFIX_ZI_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_QIE() As String
        Get
            Return _PREFIX_QIE
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_COLOR_CODE() As String
        Get
            Return _PREFIX_CUSTOMER_COLOR_CODE
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_SERIAL_NUMBER() As String
        Get
            Return _PREFIX_CUSTOMER_SERIAL_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_NGS() As String
        Get
            Return _PREFIX_NGS
        End Get
    End Property

#End Region

    Public Sub Clear()
        _Elements.Clear()
        _Elements.Add(_PREFIX_LK_NUMBER, "")
        _Elements.Add(_PREFIX_SERIAL_NUMBER, "")
        _Elements.Add(_PREFIX_CUSTOMER_NUMBER, "")
        _Elements.Add(_PREFIX_HW_INDEX, "")
        _Elements.Add(_PREFIX_SW_INDEX, "")
        _Elements.Add(_PREFIX_ME_INDEX, "")
        _Elements.Add(_PREFIX_AS_INDEX, "")
        _Elements.Add(_PREFIX_AI_INDEX, "")
        _Elements.Add(_PREFIX_ZI_INDEX, "")
        _Elements.Add(_PREFIX_QIE, "")
        _Elements.Add(_PREFIX_CUSTOMER_COLOR_CODE, "")
        _Elements.Add(_PREFIX_CUSTOMER_SERIAL_NUMBER, "")
        _Elements.Add(_PREFIX_NGS, "")
    End Sub

    Public Sub New()
        Clear()
    End Sub
    Protected Overrides Sub Finalize()
        _Elements.Clear()
        _Elements = Nothing
        MyBase.Finalize()
    End Sub

#Region "Properties"

    Public ReadOnly Property LkNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_LK_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property SerialNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_SERIAL_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property CustomerNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_CUSTOMER_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property HW_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_HW_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property SW_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_SW_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property AI_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_AI_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property AS_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_AS_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property ZI_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_ZI_INDEX, Value)
            Return Value
        End Get
    End Property

    Public ReadOnly Property QIE() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_QIE, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property CustomerColorCode() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_CUSTOMER_COLOR_CODE, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property NGS() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_NGS, Value)
            Return Value
        End Get
    End Property

#End Region

    Public Overloads Function SetNewLine(ByVal Line As String) As Boolean
        Dim _Data() As String, l As Integer, _Key As String

        Clear()
        _Data = Line.Split(_DELIMITER)
        If _Data.Length = 0 Then Return False
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            _Data(l) = _DELIMITER & _Data(l)
        Next
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            For Each _Key In _Elements.Keys
                If _Data(l).Length > _Key.Length Then
                    If _Data(l).Substring(0, _Key.Length) = _Key Then
                        _Elements(_Key) = _Data(l).Substring(_Key.Length).Replace(";", "")
                        _Data(l) = ""
                        Exit For
                    End If
                End If
            Next
        Next
        Return True

    End Function

    Public Overloads Function SetNewLine(ByVal Line As String, ByVal Element As String) As String
        If Not SetNewLine(Line) Then Return ""
        Return GetElement(Element)
    End Function

    Public Function GetElement(ByVal Element As String) As String
        Dim Value As String = ""
        _Elements.TryGetValue(Element, Value)
        Return Value
    End Function

    Public Shared Function GetBarcode(ByVal lkNr As String, ByVal sn As String) As String

        Return _PREFIX_LK_NUMBER & lkNr.Trim & _PREFIX_SERIAL_NUMBER & sn.Trim

    End Function

End Class

Public Class Barcode_Splitter

    Protected _Elements As New Dictionary(Of String, String)
    Protected _Delimiter As Char = "/"c
    Protected Const _PREFIX_LK_NUMBER As String = "/3OS"
    Protected Const _PREFIX_SERIAL_NUMBER As String = "/SN"
    Protected Const _PREFIX_CUSTOMER_NUMBER As String = "/P"
    Protected Const _PREFIX_HW_INDEX As String = "/HW"                    'Harware Index
    Protected Const _PREFIX_SW_INDEX As String = "/SW"                    'Software Index
    Protected Const _PREFIX_AS_INDEX As String = "/AS"
    Protected Const _PREFIX_AI_INDEX As String = "/AI"
    Protected Const _PREFIX_ZI_INDEX As String = "/ZI"                    'Drawing Index
    Protected Const _PREFIX_QIE As String = "/KS"                         'Quality Index
    Protected Const _PREFIX_ME_INDEX As String = "/ME"                    'Mechanic Index
    Protected Const _PREFIX_CUSTOMER_COLOR_CODE As String = "/CC"
    Protected Const _PREFIX_CUSTOMER_SERIAL_NUMBER As String = "/CSN"
    Protected Const _PREFIX_NGS As String = "/Y"

#Region "Properties"

    Public ReadOnly Property PREFIX_LK_NUMBER() As String
        Get
            Return _PREFIX_LK_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_SERIAL_NUMBER() As String
        Get
            Return _PREFIX_SERIAL_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_NUMBER() As String
        Get
            Return _PREFIX_CUSTOMER_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_HW_INDEX() As String
        Get
            Return _PREFIX_HW_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_SW_INDEX() As String
        Get
            Return _PREFIX_SW_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_ME_INDEX() As String
        Get
            Return _PREFIX_ME_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_AS_INDEX() As String
        Get
            Return _PREFIX_AS_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_AI_INDEX() As String
        Get
            Return _PREFIX_AI_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_ZI_INDEX() As String
        Get
            Return _PREFIX_ZI_INDEX
        End Get
    End Property
    Public ReadOnly Property PREFIX_QIE() As String
        Get
            Return _PREFIX_QIE
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_COLOR_CODE() As String
        Get
            Return _PREFIX_CUSTOMER_COLOR_CODE
        End Get
    End Property
    Public ReadOnly Property PREFIX_CUSTOMER_SERIAL_NUMBER() As String
        Get
            Return _PREFIX_CUSTOMER_SERIAL_NUMBER
        End Get
    End Property
    Public ReadOnly Property PREFIX_NGS() As String
        Get
            Return _PREFIX_NGS
        End Get
    End Property

#End Region

    Protected Sub InitAsLkStandardFormat()
        _Elements.Clear()
        _Elements.Add(_PREFIX_LK_NUMBER, "")
        _Elements.Add(_PREFIX_SERIAL_NUMBER, "")
        _Elements.Add(_PREFIX_CUSTOMER_NUMBER, "")
        _Elements.Add(_PREFIX_HW_INDEX, "")
        _Elements.Add(_PREFIX_SW_INDEX, "")
        _Elements.Add(_PREFIX_ME_INDEX, "")
        _Elements.Add(_PREFIX_AS_INDEX, "")
        _Elements.Add(_PREFIX_AI_INDEX, "")
        _Elements.Add(_PREFIX_ZI_INDEX, "")
        _Elements.Add(_PREFIX_QIE, "")
        _Elements.Add(_PREFIX_CUSTOMER_COLOR_CODE, "")
        _Elements.Add(_PREFIX_CUSTOMER_SERIAL_NUMBER, "")
        _Elements.Add(_PREFIX_NGS, "")
    End Sub

    Protected Sub ClearValues()

        For Each key As String In _Elements.Keys
            _Elements(key) = ""
        Next

    End Sub

    Public Sub New(ByVal usingLkStandardFormat As Boolean, ByVal dicCheckItems As Dictionary(Of String, String), Optional ByVal delimiter As Char = ";"c)
        If usingLkStandardFormat Then
            _Delimiter = "/"c
            InitAsLkStandardFormat()
        Else
            _Delimiter = delimiter
            _Elements.Clear()
            For Each item As String In dicCheckItems.Values
                If item = "" Then Continue For
                If _Elements.ContainsKey(item) Then Continue For
                _Elements.Add(item, "")
            Next
        End If

    End Sub
    Protected Overrides Sub Finalize()
        _Elements.Clear()
        _Elements = Nothing
        MyBase.Finalize()
    End Sub

#Region "Properties"

    Public ReadOnly Property LkNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_LK_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property SerialNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_SERIAL_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property CustomerNumber() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_CUSTOMER_NUMBER, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property HW_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_HW_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property SW_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_SW_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property AI_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_AI_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property AS_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_AS_INDEX, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property ZI_Index() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_ZI_INDEX, Value)
            Return Value
        End Get
    End Property

    Public ReadOnly Property QIE() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_QIE, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property CustomerColorCode() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_CUSTOMER_COLOR_CODE, Value)
            Return Value
        End Get
    End Property
    Public ReadOnly Property NGS() As String
        Get
            Dim Value As String = ""
            _Elements.TryGetValue(_PREFIX_NGS, Value)
            Return Value
        End Get
    End Property

#End Region

    Public Overloads Function SetNewLine(ByVal Line As String) As Boolean
        Dim _Data() As String, l As Integer, _Key As String

        ClearValues()
        _Data = Line.Split(_Delimiter)
        If _Data.Length = 0 Then Return False
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            _Data(l) = _Delimiter & _Data(l)
        Next
        For l = _Data.GetLowerBound(0) To _Data.GetUpperBound(0)
            For Each _Key In _Elements.Keys
                If _Data(l).Length > _Key.Length Then
                    If _Data(l).Substring(0, _Key.Length) = _Key Then
                        _Elements(_Key) = _Data(l).Substring(_Key.Length).Replace(";", "")
                        _Data(l) = ""
                        Exit For
                    End If
                End If
            Next
        Next
        Return True

    End Function

    Public Overloads Function SetNewLine(ByVal Line As String, ByVal Element As String) As String
        If Not SetNewLine(Line) Then Return ""
        Return GetElement(Element)
    End Function

    Public Function GetElement(ByVal Element As String) As String
        Dim Value As String = ""
        _Elements.TryGetValue(Element, Value)
        Return Value
    End Function

    Public Shared Function GetBarcode(ByVal lkNr As String, ByVal customerNr As String, ByVal sn As String) As String

        Return _PREFIX_LK_NUMBER & lkNr.Trim & _PREFIX_CUSTOMER_NUMBER & customerNr & _PREFIX_SERIAL_NUMBER & sn.Trim

    End Function

End Class

Public Class Barcode_Porsche

    Public Enum Section
        Start = 0
        SapArticleNumber = 1
        LkSerialNumber = 2
        BitsOfFeatures = 3
        LkProductionUnitNumber = 4
        LkProductionIndex = 5
        LkProduktionNumber = 6
        PorscheSequenceNumber = 7
    End Enum

    Protected Const _SEQUENCE_START As String = "KIT"
    Protected Const _DELIMITER As Char = "#"c
    Protected Const _MaxSection As Integer = 7
    Protected _Data() As String

#Region "Properties"

    Public ReadOnly Property SapArticleNumber() As String
        Get
            Try
                Return _Data(Section.SapArticleNumber)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property LkSerialNumber() As String
        Get
            Try
                Return _Data(Section.LkSerialNumber)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property BitsOfFeatures() As String
        Get
            Try
                Return _Data(Section.BitsOfFeatures)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property LkProductionUnitNumber() As String
        Get
            Try
                Return _Data(Section.LkProductionUnitNumber)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property LkProductionIndex() As String
        Get
            Try
                Return _Data(Section.LkProductionIndex)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property LkProduktionNumber() As String
        Get
            Try
                Return _Data(Section.LkProduktionNumber)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Public ReadOnly Property PorscheSequenceNumber() As String
        Get
            Try
                Return _Data(Section.PorscheSequenceNumber)
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
#End Region

    Public Sub New()
        Clear()
    End Sub

    Public Overloads Function NewLine(ByVal Line As String) As Boolean
        Clear()
        _Data = Line.Split(_DELIMITER)
        If _Data.Length <> _MaxSection Then Return False
        If _Data(0) <> _SEQUENCE_START Then Return False
        Return True
    End Function

    Public Overloads Function NewLine(ByVal Line As String, ByVal GetElement As Barcode_Porsche.Section) As String
        NewLine(Line)
        Try
            Return _Data(GetElement)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub Clear()
        If _Data.Length > 0 Then
            Array.Clear(_Data, 0, _Data.Length)
        End If
    End Sub

End Class




