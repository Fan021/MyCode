Imports Kostal.Las.ArticleProvider.Csv
Imports System.Windows.Forms

#Region "csv Interface"

Public Class CsvInterface
    Protected _CsvReader As New List(Of SysCSV.clsSysCSV)
    Protected AppSettings As Settings
    Protected _IsInit As Boolean
    Protected _Section As String = String.Empty
    Protected _Folder As String = String.Empty
    Protected _ID As String = String.Empty
    Protected _IndicatedName As String = String.Empty
    Protected _FileHandler As New FileHandler
    Protected _Status As enumLK_CSV_STATUS
    Protected _StatusDescription As String
    Protected _enumCsvAppendType As enumCsvAppendType
    Protected _ArticleListElements As New Dictionary(Of String, ArticleListElement)

#Region "Properties"


    Public ReadOnly Property IsInit() As Boolean
        Get
            Return _IsInit
        End Get
    End Property

    Public ReadOnly Property Status() As enumLK_CSV_STATUS
        Get
            Return _Status
        End Get
    End Property


    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property


    Public ReadOnly Property ArticleListElements() As Dictionary(Of String, ArticleListElement)
        Get
            Dim _Empty As New Dictionary(Of String, ArticleListElement)
            Try
                Return _ArticleListElements
            Catch ex As Exception
                Return _Empty
            End Try
        End Get
    End Property

#End Region

    Public Sub New _
                (
                ByVal _AppSettings As Settings,
                ByVal MySection As String,
                ByVal MyFolder As String,
                ByVal MyID As String,
                ByVal MyIndicatedName As String,
                ByVal MyenumCsvAppendType As enumCsvAppendType
                )
        Try
            _Status = enumLK_CSV_STATUS.LK_CSV_NOT_INITIALIZED
            _IsInit = False
            AppSettings = _AppSettings
            _Section = MySection
            _Folder = MyFolder
            _ID = MyID
            _IndicatedName = MyIndicatedName
            _enumCsvAppendType = MyenumCsvAppendType
        Catch ex As Exception
            _StatusDescription = "CsvInterface.Init. Msg:" & ex.Message
            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
            _IsInit = False
        End Try
    End Sub

    Public Function Init() As Boolean
        _IsInit = InitDo(_ID = _IndicatedName)
        Return _IsInit
    End Function



    Protected Overrides Sub Finalize()
        _CsvReader = Nothing
        _IsInit = False
        MyBase.Finalize()
    End Sub



    Protected Function InitDo(Optional ByVal UseIDAsIndicatedName As Boolean = False) As Boolean
        Dim sResult As String, lResult As Long
        Try
            sResult = AppSettings.ScheduleFile
            If sResult = "" Or sResult = _FileHandler.ErrorString Then
                _Status = enumLK_CSV_STATUS.LK_CSV_FILE_NOT_AVAILABLE
                _StatusDescription = "CsvInterface.InitDo. " + _Section + " Not Find. "
                Return False
            End If
            _CsvReader.Add(New SysCSV.clsSysCSV)
            lResult = _CsvReader(0).TeachCSV(_Folder & sResult)
            If lResult <> 0 Then
                _Status = enumLK_CSV_STATUS.LK_CSV_FILE_NOT_AVAILABLE
                _StatusDescription = "CsvInterface.InitDo. " + " Error Code:" + lResult.ToString + " File:" + _Folder & sResult
                Return False
            End If

            Return Read_IDs(UseIDAsIndicatedName)
        Catch ex As Exception
            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
            _StatusDescription = "CsvInterface.InitDo. Msg:" + ex.Message
            Return False
        End Try
    End Function



    Protected Function Read_IDs(Optional ByVal UseIDAsIndicatedName As Boolean = False) As Boolean
        Dim l As Long, sResult As String, TempId As String
        Dim mKey As Array
        sResult = ""
        Try
            _ArticleListElements = Nothing
            _ArticleListElements = New Dictionary(Of String, ArticleListElement)

            '追加模式List信息合并
            If _enumCsvAppendType = enumCsvAppendType.Append Then
                For i = 0 To _CsvReader.Count - 1
                    For l = 0 To _CsvReader(i).MySum.Count - 1
                        _CsvReader(i).sAliasKey = ""
                        _CsvReader(i).sCSVKey = _IndicatedName
                        mKey = CType(_CsvReader(i).MySum.Keys, Array)
                        TempId = CStr(mKey.GetValue(l))
                        If UseIDAsIndicatedName Then
                            sResult = TempId
                        Else
                            _CsvReader(i).sMatNoKey = CStr(mKey.GetValue(l))
                            _CsvReader(i).GetEntry(sResult)
                        End If
                        If _ArticleListElements.ContainsKey(TempId) Then
                            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
                            _StatusDescription = "CsvInterface.Read_IDs. The same Key:" + TempId + " have exist"
                            _ArticleListElements.Clear()
                            Return False
                        End If
                        _ArticleListElements.Add(TempId, New ArticleListElement(TempId, sResult, l))
                    Next
                Next
            End If

            '合并模式List只取第一个CSV信息
            If _enumCsvAppendType = enumCsvAppendType.Merge Or _enumCsvAppendType = enumCsvAppendType.NONE Then
                For l = 0 To _CsvReader(0).MySum.Count - 1
                    _CsvReader(0).sAliasKey = ""
                    _CsvReader(0).sCSVKey = _IndicatedName
                    mKey = CType(_CsvReader(0).MySum.Keys, Array)
                    TempId = CStr(mKey.GetValue(l))
                    If UseIDAsIndicatedName Then
                        sResult = TempId
                    Else
                        _CsvReader(0).sMatNoKey = CStr(mKey.GetValue(l))
                        _CsvReader(0).GetEntry(sResult)
                    End If
                    If _ArticleListElements.ContainsKey(TempId) Then
                        _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
                        _StatusDescription = "CsvInterface.Read_IDs. The same Key:" + TempId + " have exist"
                        _ArticleListElements.Clear()
                        Return False
                    End If
                    _ArticleListElements.Add(TempId, New ArticleListElement(TempId, sResult, l))
                Next
            End If
            Return True
        Catch ex As Exception
            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
            _StatusDescription = "CsvInterface.Read_IDs. Msg:" + ex.Message
            _ArticleListElements.Clear()
            Return False
        End Try

    End Function


    Public Function Get_ID_FromIndicatedName(ByVal IndicatedName As String) As String
        Dim _Element As ArticleListElement
        If Not _IsInit Then Return String.Empty
        For Each _Element In _ArticleListElements.Values
            If _Element.IndicatedName = IndicatedName Then
                Return _Element.ID
            End If
        Next
        Return String.Empty
    End Function

    'important change here executed by wang65 2015.06.23
    Public Overloads Function ReadElement(ByVal ID As String, ByRef Element As ArticleElement) As Boolean
        Dim lResult As Long, sResult As String
        Try
            If Not _IsInit Then Return False
            For i = 0 To _CsvReader.Count - 1
                sResult = ""
                _CsvReader(i).sMatNoKey = ID
                _CsvReader(i).sCSVKey = CType(Element.Mapper, Mapping.CsvColumnMapping).ColumnHeader
                _CsvReader(i).sAliasKey = ""
                lResult = _CsvReader(i).GetEntry(sResult)
                If lResult = 0 Then
                    Element.Data = sResult
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
            _StatusDescription = "CsvInterface.ReadElement. Msg:" + ex.Message
            Return False
        End Try
    End Function

    'added by wang65 20150413
    Public Overloads Function ReadElement(ByVal ID As String, ByVal strKey As String, ByRef strResult As String) As Boolean
        Dim lResult As Long, sResult As String
        Try
            strResult = ""
            If Not _IsInit Then Return False
            For i = 0 To _CsvReader.Count - 1
                sResult = ""
                _CsvReader(i).sMatNoKey = ID
                _CsvReader(i).sCSVKey = strKey
                _CsvReader(i).sAliasKey = ""
                lResult = _CsvReader(i).GetEntry(sResult)
                If lResult = 0 Then
                    strResult = sResult
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            _Status = enumLK_CSV_STATUS.LK_CSV_WINDOWS_ERROR
            _StatusDescription = "CsvInterface.ReadElement. Msg:" + ex.Message
            Return False
        End Try
    End Function
End Class

#End Region


