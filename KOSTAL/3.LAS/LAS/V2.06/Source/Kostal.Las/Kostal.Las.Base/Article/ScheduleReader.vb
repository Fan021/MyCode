Imports Kostal.Las.ArticleProvider.Csv
Imports System.Windows.Forms

Public Class ScheduleReader
    Inherits ReaderBase
    Protected enumCsvAppendType As enumCsvAppendType = enumCsvAppendType.NONE

    Public Sub New _
      (ByVal MyParent As Station,
       ByVal _AppSettings As Settings,
       ByVal MyLanguage As Language
      )
        MyBase.New(MyParent, _AppSettings, MyLanguage)
        _mSelection = "Schedule"
        _ClassName = "ScheduleReader"
    End Sub

    Public Overrides Function Init() As Boolean
        If Not Init_Elements() Then Return False
        _ArticleReader = New CsvInterface(AppSettings, CON_SECTION_SCHEDULE, AppSettings.ScheduleFolder, _HeaderOfCsvKeyColumn, _HeaderOfIndicatedName, enumCsvAppendType)
        If Not _ArticleReader.Init() Then
            _Logger.Thrower(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCHEDULEREAD_INIT, "FAIL"), "ScheduleReader.Init")
            Return False
        End If
        _ArticleListElement = _ArticleReader.ArticleListElements
        Return True
    End Function

    Protected Overrides Function Init_Elements() As Boolean
        Dim i As UInteger
        _HeaderOfCsvKeyColumn = KostalScheduleKeys.KEY_ID
        _HeaderOfIndicatedName = KostalScheduleKeys.KEY_SCHEDULE_NAME

        AddKey(KostalScheduleKeys.KEY_ID, enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_ID))
        AddKey(KostalScheduleKeys.KEY_SCHEDULE_INDEX, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_SCHEDULE_INDEX))
        AddKey(KostalScheduleKeys.KEY_SCHEDULE_NAME, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_SCHEDULE_NAME))
        AddKey(KostalScheduleKeys.KEY_SCHEDULE_DESCRIPTION, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_SCHEDULE_DESCRIPTION))
        AddKey(KostalScheduleKeys.KEY_USER_VERIFICATION, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_USER_VERIFICATION))
        AddKey(KostalScheduleKeys.KEY_REFERENCE_SCHEDULE, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_REFERENCE_SCHEDULE))
        AddKey(KostalScheduleKeys.KEY_SECURITY_CHECKSUM, enumARTICLE_SOURCE.ARTICLE_SOURCE_CSVorXML, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_SECURITY_CHECKSUM))

        For i = 1 To CON_MAXIMUM_TOTAL_STATIONS Step 1
            AddKey(KostalScheduleKeys.KEY_PASS_STATION(i), enumARTICLE_SOURCE.ARTICLE_SOURCE_CSV_STATION, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_PASS_STATION(i)))
            AddKey(KostalScheduleKeys.KEY_FAIL_STATION(i), enumARTICLE_SOURCE.ARTICLE_SOURCE_CSV_STATION, New Mapping.CsvColumnMapping(KostalScheduleKeys.KEY_FAIL_STATION(i)))
        Next i
        Return True
    End Function

    Protected Overrides Function GetFullArticleAttributteList() As List(Of String)
        Dim basicArticleKeys As New List(Of String)
        For Each name As String In KostalScheduleKeys.GetUserDefinedKeys
            basicArticleKeys.Add(name)
        Next
        Return basicArticleKeys
    End Function

End Class
