Imports Kostal.Las.ArticleProvider.Base
Imports Kostal.Las.ArticleProvider.Csv
Imports System.Windows.Forms

Public Enum enumUserVerificationType
    NULL_VERIFICATION = 0
    ARTICLE_OCCUPIED = 1
    PLC_OCCUPIED = 2
    PASSWORD_APPLICATION = 3
    PASSWORD_USERDEFINED = 4
End Enum

Public Enum enumSchedulePriority
    NULL = 0
    Article
    Manual
    REF
    PLC
End Enum

Public Structure StructUserVerification
    Public VerificationType As enumUserVerificationType
    Public Password As String
End Structure


Public Enum enumCsvAppendType
    Append = 0
    Merge
    NONE
End Enum
Public Enum enumARTICLE_SOURCE
    ARTICLE_SOURCE_INTERNAL = -2
    ARTICLE_SOURCE_NONE = -1
    ARTICLE_SOURCE_MANUAL = 0
    ARTICLE_SOURCE_CSVorXML = 2
    ARTICLE_SOURCE_CSV_STATION = 2
    ARTICLE_SOURCE_INI = 3
    ARTICLE_SOURCE_XML = 4
End Enum

Public Enum enumLK_ARTICLE_STATUS
    LK_ARTICLE_STATUS_WINDOWS_ERROR = -99
    LK_ARTICLE_STATUS_NOT_EXISTING_COMPONENT = -2
    LK_ARTICLE_STATUS_ELEMENT_FAIL = -1
    LK_ARTICLE_STATUS_NO_ERROR = 0
End Enum

Public Class ArticleBase
    Protected _ArticleElements As New Dictionary(Of String, ArticleElement)
    Protected _ArticleListElements As New Dictionary(Of String, ArticleListElement)
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _ComboBox As ComboBox
    Protected _AritcleReader As IReader
    Protected _Logger As Logger
    Protected _i As New Station
    Protected _mCurrentID As String
    Protected _ClassName As String
    Protected _FileHande As FileHandler
    Protected _xmlHande As XmlHandler
    Public Event IDChange(ByVal mID As String, ByVal ChangeType As enumChangeType)
#Region "Properties"

    ReadOnly Property ArticleListElement() As Dictionary(Of String, ArticleListElement)
        Get
            Dim _Empty As New Dictionary(Of String, ArticleListElement)
            Try
                Return _ArticleListElements
            Catch ex As Exception
                Return _Empty
            End Try

        End Get
    End Property

    ReadOnly Property ArticleElements() As Dictionary(Of String, ArticleElement)
        Get
            Dim _Empty As New Dictionary(Of String, ArticleElement)
            Try
                Return _ArticleElements
            Catch ex As Exception
                Return _Empty
            End Try

        End Get
    End Property

#End Region

    Public Sub New(ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language, ByVal AritcleReader As IReader, Optional ByVal ComboBox As ComboBox = Nothing)
        _i = MyStation
        AppSettings = _AppSettings
        _Language = MyLanguage
        _AritcleReader = AritcleReader
        _ComboBox = ComboBox
        _FileHande = New FileHandler()
        _Logger = New Logger(AppSettings)

    End Sub

    Public Overridable Function Init() As Boolean
        If Not _AritcleReader.Init() Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_INIT, "FAIL"), _ClassName + ".Init")
            Return False
        End If
        _ArticleListElements = _AritcleReader.ArticleListElements
        _ArticleElements = _AritcleReader.ArticleElements

        ReadArticleLanguage()

        If Not IsNothing(_ComboBox) Then
            _ComboBox.Items.Clear()
            If _ArticleListElements.Count <> 0 Then
                For Each _Element In _ArticleListElements
                    _ComboBox.Items.Add(_Element.Value.IndicatedName)
                Next '
            End If
        End If
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_INIT, "Successful"), _ClassName + ".Init")
        Return True
    End Function

    Public Function GetArticle_FromID(ByVal mID As String, Optional ByVal ChangeType As enumChangeType = enumChangeType.Auto) As Boolean
        If mID = "" Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_GETARTICLE_FROMID_FAIL, "FAIL", "ID is NULL"), _ClassName + ".GetArticle_FromID")
            Return False
        End If

        If Not _ArticleListElements.ContainsKey(mID) Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_GETARTICLE_FROMID_FAIL, "FAIL", "Article List Not ContainsKey:" + mID), _ClassName + ".GetArticle_FromID")
            Return False
        End If

        _mCurrentID = mID
        _ArticleElements = _AritcleReader.Get_Elements(_mCurrentID)
        If _ArticleElements Is Nothing Then
            Return False
        End If
        ReadArticleLanguage()
        RaiseEvent IDChange(mID, ChangeType)
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_GETARTICLE_FROMID, "Successful", ""), _ClassName + ".GetArticle_FromID")
        Return True
    End Function

    Public Function GetArticle_FromIndicatedName(ByVal IndicatedName As String, Optional ByVal ChangeType As enumChangeType = enumChangeType.Auto) As Boolean
        If Not GetArticle_FromID(_AritcleReader.Get_Article_ID_FromIndicatedName(IndicatedName), ChangeType) Then
            Return False
        End If
        ReadArticleLanguage()
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_INDICATE, "Successful", ""), _ClassName + ".GetArticle_FromIndicatedName")
        Return True
    End Function



    Public Function AddManualElement(ByVal mKeyName As String, Optional ByVal DisplayAtDataList As Boolean = False) As Boolean
        Return _AritcleReader.AddManualElement(mKeyName, DisplayAtDataList)
    End Function

    Protected Overrides Sub Finalize()
        Try
            _ArticleElements.Clear()
            _ArticleElements = Nothing
        Catch ex As Exception
            '
        End Try
        MyBase.Finalize()

    End Sub

    Public Sub Clear()
        _ArticleElements.Clear()
    End Sub

    Public Sub ClearEntriesOnly()
        Dim mElement As ArticleElement
        For Each mElement In _ArticleElements.Values
            mElement.Clear()
        Next
    End Sub

    Public Sub ClearDataOnly()
        Dim mElement As ArticleElement
        For Each mElement In _ArticleElements.Values
            mElement.ClearData()
        Next
    End Sub

    Public Sub ClearManualSourceData()
        Dim mElement As ArticleElement
        For Each mElement In _ArticleElements.Values
            If mElement.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL Then
                mElement.ClearData()
            End If
        Next
    End Sub

    Public Overridable Function ReadArticleLanguage() As Boolean
        Dim mElement As KeyValuePair(Of String, ArticleElement)

        For Each mElement In _ArticleElements
            _ArticleElements.Item(mElement.Key).Name = _Language.Read(_AritcleReader.mSelection, mElement.Key)   '_FileHandler.ReadLanguageFile(AppSettings.LngFolder, _Language.SelectedLanguageFileName, CON_SECTION_ARTICLE, mElement.Key)
            If _ArticleElements.Item(mElement.Key).Name.ToUpper.Contains("ERROR") Then
                _ArticleElements.Item(mElement.Key).Name = mElement.Key
            End If
        Next
        Return True
    End Function

    Protected Function CheckValidElmentNames(ByVal checkNames As IEnumerable(Of String)) As Boolean

        For Each name As String In checkNames

            If Not _ArticleElements.ContainsKey(name) Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ARTICLE_CHECKELMENT, name), _ClassName + ".CheckValidElmentNames")
                Return False
            End If
        Next

        Return True
    End Function
End Class

Public Class ArticleElement
    Protected _Key As String
    Protected _Index As Long
    Protected _Source As enumARTICLE_SOURCE

    Protected _Mapper As Mapping.Mapping
    Protected _Name As String
    Protected _Data As String
    Protected _Quantity As String
    Protected _Location As String
    Protected _Revision As String
    Protected _Status As String
    Protected _MustLocated As Boolean
    Protected _Visible As Boolean

    Public Property Key() As String
        Get
            Return _Key
        End Get
        Set(ByVal value As String)
            _Key = value
        End Set
    End Property

    Public Property Index() As Long
        Get
            Return _Index
        End Get
        Set(ByVal value As Long)
            _Index = value
        End Set
    End Property

    Public Property Source() As enumARTICLE_SOURCE
        Get
            Return _Source
        End Get
        Set(ByVal value As enumARTICLE_SOURCE)
            _Source = value
        End Set
    End Property

    Public Property Mapper() As Mapping.Mapping
        Get
            Return _Mapper
        End Get
        Set(ByVal value As Mapping.Mapping)
            _Mapper = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property Data() As String
        Get
            Return _Data
        End Get
        Set(ByVal value As String)
            _Data = value
        End Set
    End Property

    Public Property Quantity() As String
        Get
            Return _Quantity
        End Get
        Set(ByVal value As String)
            _Quantity = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Property Revision() As String
        Get
            Return _Revision
        End Get
        Set(ByVal value As String)
            _Revision = value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    Public Property MustLocated() As Boolean
        Get
            Return _MustLocated
        End Get
        Set(ByVal value As Boolean)
            _MustLocated = value
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal value As Boolean)
            _Visible = value
        End Set
    End Property

    Public Sub Clear()
        _Key = ""
        _Index = 0
        _Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE
        _Mapper = Nothing
        _Name = ""
        _Data = ""
        _Quantity = ""
        _Location = ""
        _Revision = ""
        _Status = ""
        _MustLocated = False
        _Visible = False
    End Sub

    Public Sub ClearData()
        _Data = ""
        _Location = ""
        _Status = ""
    End Sub

End Class


Public Class ArticleListElement

    Protected _ID As String
    Protected _IndicatedName As String
    Protected _Position As Long
    Protected _IndicatedNativeName As String
    Protected _UserVerification As StructUserVerification
    Protected _SchedulePriority As enumSchedulePriority
    Protected _strUserVerification As String

    Sub New(Optional ByVal ID As String = "", Optional ByVal IndicatedName As String = "", Optional ByVal Position As Long = 0, Optional ByVal UserVerification As StructUserVerification = Nothing)
        _ID = ID
        _IndicatedName = IndicatedName
        _Position = Position
        _UserVerification = UserVerification
    End Sub

    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property


    Public Property IndicatedName() As String
        Get
            Return _IndicatedName
        End Get
        Set(ByVal value As String)
            _IndicatedName = value
        End Set
    End Property


    Public Property Position() As Long
        Get
            Return _Position
        End Get
        Set(ByVal value As Long)
            _Position = value
        End Set
    End Property


    Public Property IndicatedNativeName() As String
        Get
            Return _IndicatedNativeName
        End Get
        Set(ByVal value As String)
            _IndicatedNativeName = value
        End Set
    End Property

    Public Property UserVerification() As StructUserVerification
        Get
            Return _UserVerification
        End Get
        Set(ByVal value As StructUserVerification)
            _UserVerification = value
        End Set
    End Property

    Public Property SchedulePriority() As enumSchedulePriority
        Get
            Return _SchedulePriority
        End Get
        Set(ByVal value As enumSchedulePriority)
            _SchedulePriority = value
        End Set
    End Property

    Public Sub Clear()
        _ID = ""
        _IndicatedName = ""
        _Position = 0

        _IndicatedNativeName = ""
        _UserVerification.VerificationType = enumUserVerificationType.NULL_VERIFICATION
        _UserVerification.Password = ""

    End Sub


End Class

Public Interface IReader

    ReadOnly Property mSelection As String
    ReadOnly Property ArticleElements() As Dictionary(Of String, ArticleElement)
    ReadOnly Property ArticleListElements() As Dictionary(Of String, ArticleListElement)

    Function Init() As Boolean
    Function AddManualElement(ByVal mKeyName As String, Optional ByVal DisplayAtDataList As Boolean = False) As Boolean
    Function Get_Elements(ByVal ID As String) As Dictionary(Of String, ArticleElement)
    Function Get_Article_ID_FromIndicatedName(ByVal IndicatedName As String) As String

End Interface

Public Class ReaderBase
    Implements IReader
    Protected _i As Station
    Protected AppSettings As Settings
    Protected _Elements As New Dictionary(Of String, ArticleElement)
    Protected _ArticleListElement As New Dictionary(Of String, ArticleListElement)
    Protected _ArticleReader As CsvInterface
    Protected _Logger As Logger
    Protected _FileHandler As New FileHandler
    Protected _Language As Language
    Protected _ArticleAndMappingFile As List(Of ArticleAndMappingFile)
    Protected _HeaderOfCsvKeyColumn As String
    Protected _HeaderOfIndicatedName As String
    Protected _mSelection As String
    Protected _ClassName As String

#Region "Properties"

    ReadOnly Property mSelection As String Implements IReader.mSelection
        Get
            Try
                Return _mSelection
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    ReadOnly Property ArticleListElement() As Dictionary(Of String, ArticleListElement) Implements IReader.ArticleListElements
        Get
            Dim _Empty As New Dictionary(Of String, ArticleListElement)
            Try
                Return _ArticleListElement
            Catch ex As Exception
                Return _Empty
            End Try

        End Get
    End Property

    ReadOnly Property ArticleElements() As Dictionary(Of String, ArticleElement) Implements IReader.ArticleElements
        Get
            Dim _Empty As New Dictionary(Of String, ArticleElement)
            Try
                Return _Elements
            Catch ex As Exception
                Return _Empty
            End Try

        End Get
    End Property

#End Region


    Public Sub New _
      (ByVal MyParent As Station,
       ByVal _AppSettings As Settings,
       ByVal MyLanguage As Language
      )
        _i = MyParent
        AppSettings = _AppSettings
        _Language = MyLanguage
        _ArticleAndMappingFile = _AppSettings.ArticleAndMappingFile
        _Logger = New Logger(AppSettings)
    End Sub

    Public Overridable Function Init() As Boolean Implements IReader.Init

        Return True
    End Function

    Protected Overridable Function Init_Elements() As Boolean
        Return True
    End Function

    Public Function AddManualElement(ByVal mKeyName As String, Optional ByVal DisplayAtDataList As Boolean = False) As Boolean Implements IReader.AddManualElement
        Return AddKey(mKeyName, enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL, , , DisplayAtDataList)
    End Function


    Protected Function AddKey(ByVal Key As String, Optional ByVal Source As enumARTICLE_SOURCE = enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE, Optional ByVal Mapper As Mapping.Mapping = Nothing, Optional ByVal MustLocated As Boolean = False, Optional ByVal Visible As Boolean = True) As Boolean
        Add(Key, "", "", "", "", "", Source, Mapper, MustLocated, Visible)
        Return True
    End Function


    Protected Function Add(ByVal Key As String, ByVal Data As String, ByVal Name As String, ByVal Location As String, ByVal Quantity As String, ByVal Revision As String, ByVal Source As enumARTICLE_SOURCE, ByVal Mapper As Mapping.Mapping, Optional ByVal MustLocated As Boolean = False, Optional ByVal IsVisible As Boolean = True) As Boolean
        Dim objNewMember As ArticleElement
        objNewMember = New ArticleElement
        objNewMember.Key = Key
        objNewMember.Index = _Elements.Count + 1
        objNewMember.Name = Name
        objNewMember.Data = Data
        objNewMember.Quantity = Quantity
        objNewMember.Location = Location
        objNewMember.Revision = Revision
        objNewMember.Source = Source
        objNewMember.Mapper = Mapper
        objNewMember.Status = ""
        objNewMember.Visible = IsVisible
        objNewMember.MustLocated = MustLocated
        If _Elements.ContainsKey(objNewMember.Key) Then
            If objNewMember.Key <> ArticleAttribute.ID.ToString Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_ADD, "FAIL", objNewMember.Key), _ClassName + ".AddKey")
                Return False
            Else
                objNewMember = Nothing
                Return True
            End If
        Else
            _Elements.Add(objNewMember.Key, objNewMember)
        End If
        objNewMember = Nothing
        Return True
    End Function


    Protected Overridable Function GetFullArticleAttributteList() As List(Of String)
        Dim basicArticleKeys As New List(Of String)
        Return basicArticleKeys
    End Function

    Public Overridable Function Get_Elements(ByVal ID As String) As Dictionary(Of String, ArticleElement) Implements IReader.Get_Elements
        Dim _Element As ArticleElement, Found As Boolean
        _Elements(KostalArticleKeys.KEY_ID).Data = ID

        For Each _Element In _Elements.Values
            Found = False
            If _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_NONE _
            Or _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_INTERNAL Then
                Found = True

            ElseIf _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_MANUAL Then
                _Element.Data = ""
                Found = True

            ElseIf _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_INI Then
                Found = Source_INI(_Element)

            ElseIf _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_XML Then
                Found = Source_XML(_Element)
            ElseIf _Element.Source = enumARTICLE_SOURCE.ARTICLE_SOURCE_CSV_STATION Then
                Found = _ArticleReader.ReadElement(ID, _Element)
                If Not Found Then
                    _Element.Data = ""
                    If _mSelection = "Schedule" Then Found = True
                End If
            Else
                If Not Found Then
                    Found = _ArticleReader.ReadElement(ID, _Element)
                End If
            End If

            If Not Found Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_GETELEMENT, "FAIL", _Element.Key, _Element.Source.ToString), _ClassName + ".Get_Elements")
                _Element.Data = ""
                Return Nothing
            End If
        Next
        Return _Elements
    End Function


    Public Overridable Function Get_Article_ID_FromIndicatedName(ByVal IndicatedName As String) As String Implements IReader.Get_Article_ID_FromIndicatedName
        Dim mTemp As String
        mTemp = _ArticleReader.Get_ID_FromIndicatedName(IndicatedName)

        If mTemp = "" Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_GETINDICATE, "FAIL"), _ClassName + ".Get_Article_ID_FromIndicatedName")
        End If
        Return mTemp
    End Function


    Protected Function Source_XML(ByRef Element As ArticleElement) As Boolean
        Dim sResult As String = ""

        'Try
        '    'Don't need any value here for parameter of GetValue(x)
        '    Element.Data = Element.Mapper.GetValue(sResult).Value
        '    Return True
        'Catch ex As Exception
        '    _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_SOURCE, "FAIL", Element.Key, "XML", ex.Message), _ClassName + ".Source_XML")
        '    Return False
        'End Try
        Return True
    End Function

    Protected Function Source_INI(ByRef Element As ArticleElement) As Boolean
        Dim sResult As String
        Try
            sResult = _FileHandler.ReadIniFile(AppSettings.ConfigFolder, AppSettings.ConfigName, CON_SECTION_ARTICLE, Element.Key)
            If (sResult <> _FileHandler.ErrorString) Then
                Element.Data = sResult
                Return True
            End If
            Return False
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_READBASE_SOURCE, "FAIL", Element.Key, "INI", ex.Message), _ClassName + ".Source_INI")
            Return False
        End Try
    End Function
End Class



