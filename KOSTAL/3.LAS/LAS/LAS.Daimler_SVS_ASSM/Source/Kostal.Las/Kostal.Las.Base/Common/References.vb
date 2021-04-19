Imports Kostal.Las.Base
Imports Kostal.Las.ArticleProvider
'REF Handler

''' <summary>
''' Contains all elements of one REF Part
''' </summary>
''' <remarks></remarks>
Public Class RefElements

    Private _ID As String = String.Empty
    Private _Enable As Boolean = False
    Private _SN As String = String.Empty
    Private _LkNumber As String = String.Empty
    Private _RefName As String = String.Empty
    Private _ProductFamily As String = String.Empty
    Private _ScheduleName As String = String.Empty
    Private _ScannerOK As Boolean = False
    Private _TestOK As Boolean = False

    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property LkNumber() As String
        Get
            Return _LkNumber
        End Get
        Set(ByVal value As String)
            _LkNumber = value
        End Set
    End Property

    Public Property RefName() As String
        Get
            Return _RefName
        End Get
        Set(ByVal value As String)
            _RefName = value
        End Set
    End Property

    Public Property Enable() As Boolean
        Get
            Return _Enable
        End Get
        Set(ByVal value As Boolean)
            _Enable = value
        End Set
    End Property

    Public Property SN() As String
        Get
            Return _SN
        End Get
        Set(ByVal value As String)
            _SN = value
        End Set
    End Property

    Public Property ProductFamily() As String
        Get
            Return _ProductFamily
        End Get
        Set(ByVal value As String)
            _ProductFamily = value
        End Set
    End Property

    Public Property ScheduleName() As String
        Get
            Return _ScheduleName
        End Get
        Set(ByVal value As String)
            _ScheduleName = value
        End Set
    End Property


    Public Property ScannerOK() As Boolean
        Get
            Return _ScannerOK
        End Get
        Set(ByVal value As Boolean)
            _ScannerOK = value
        End Set
    End Property


    Public Property TestOK() As Boolean
        Get
            Return _TestOK
        End Get
        Set(ByVal value As Boolean)
            _TestOK = value
        End Set
    End Property
End Class



''' <summary>
''' Handles Reference Parts
''' </summary>
''' <remarks></remarks>
Public Class References

    Implements IDisposable

    Private _i As New Station
    Private _Parent As String = String.Empty

    Private AppSettings As Settings
    Private _Language As Language

    Private _Logger As Logger

    Private _FileHandler As New FileHandler
    Private _ArticleReader As ArticleReader
    'Private _ArticleKeys As New ArticleKeys

    Private _LocalArticle As Article
    Private _LocalSchedule As Schedule
    Private _LocalChangeArticle As Article
    Private _AppArticle As Article

    Private _objRefreshList As New Object
    Private _REFs As New Dictionary(Of String, RefElements)
    Private _refCheckList As New Dictionary(Of String, RefElements)
    Private _currentRef As New RefElements
    Private _RefreshingOK As Boolean = False
    Private _RefMode As Boolean = False
    Private _RefEnable As Boolean = False
    Private _RefManual As Boolean = False
    Public Const Name As String = "_Refs"
    Private _RefLock As Boolean = False
    Public RefChange As Boolean = False
    Public WithEvents _Shift As Shift
    Public strLastScheduleName As String = String.Empty
#Region "Properties"


    Public Property RefreshingOK As Boolean
        Get
            Return _RefreshingOK
        End Get
        Set(ByVal value As Boolean)
            _RefreshingOK = value
        End Set
    End Property

    Public Property RefEnable As Boolean
        Get
            Return _RefEnable
        End Get
        Set(ByVal value As Boolean)
            _RefEnable = value
        End Set
    End Property

    Public Property RefManual As Boolean
        Get
            Return _RefManual
        End Get
        Set(ByVal value As Boolean)
            _RefManual = value
        End Set
    End Property

    Public Property RefMode As Boolean
        Get
            Return _RefMode
        End Get
        Set(ByVal value As Boolean)
            _RefMode = value
        End Set
    End Property

    Public Property RefLock As Boolean
        Get
            Return _RefLock
        End Get
        Set(ByVal value As Boolean)
            _RefLock = value
        End Set
    End Property

    Public ReadOnly Property REFs() As Dictionary(Of String, RefElements)
        Get
            Return _REFs
        End Get
    End Property

    Public Property currentRef As RefElements
        Get
            Return _currentRef
        End Get
        Set(ByVal value As RefElements)
            _currentRef = value
        End Set
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Return _refCheckList.Count
        End Get
    End Property


    Public ReadOnly Property Element(ByVal ID As String) As RefElements
        Get
            Try
                Return _REFs(ID)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property Keys As List(Of String)
        Get
            Dim lstKeys As New List(Of String)
            For Each key As String In _refCheckList.Keys
                lstKeys.Add(key)
            Next
            Return lstKeys
        End Get
    End Property

    Public ReadOnly Property ActiveIDs() As String
        Get
            Dim strKeys As String = ""
            For Each item As String In _refCheckList.Keys
                strKeys += item & ";"
            Next

            Return strKeys
        End Get
    End Property

#End Region

    Public Sub New(ByVal My_Parent As String, ByVal Devices As Dictionary(Of String, Object))

        _Parent = My_Parent
        _i.IdString = _Parent + "_REF"

        _i.Name = _i.IdString

        AppSettings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)

        _Logger = New Logger(AppSettings)

        _LocalArticle = New Article(_i, AppSettings, _Language)
        _LocalArticle.Init()

        _AppArticle = CType(Devices(Article.Name), Article)

        _LocalChangeArticle = New Article(_i, AppSettings, _Language)
        _LocalChangeArticle.Init()

        _LocalSchedule = New Schedule(_i, AppSettings, _Language)
        _LocalSchedule.Init()
        _REFs.Clear()
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_INIT, "Successful"), "References.Init")

    End Sub



    Public Function GetElementsFromID(ByVal mID As String) As RefElements
        For Each Element As RefElements In _REFs.Values
            If Element.SN = mID Then
                Return Element
            End If
        Next
        Return Nothing
    End Function
    '''<summary>
    '''Checking whether the ScheduleName has been used By Reference settings or not
    '''</summary>
    ''' 
    Public Function CheckingScheduleNameBeingUsedByRefs(ByVal strScheduleName As String) As Boolean

        If strScheduleName = "" Then Return False

        For Each item As RefElements In _REFs.Values

            If item.ScheduleName = strScheduleName Then Return True

        Next

        Return False

    End Function

    Public Function CheckingLKNumber(ByVal mID As String) As Boolean
        If Not _LocalArticle.ArticleListElement.ContainsKey(mID) Then
            Return False
        End If
        Return True
    End Function

    Public Function CheckingScheduleName(ByVal mScheduleName As String) As Boolean
        For Each _ArticleListElement As ArticleListElement In _LocalSchedule.ArticleListElement.Values
            If _ArticleListElement.IndicatedName = mScheduleName Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CheckingReferenceName(ByVal mScheduleName As String) As Boolean
        For Each _Element As RefElements In _REFs.Values
            If _Element.ScheduleName = mScheduleName Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function CheckingReferenceSN(ByVal mSN As String) As Boolean
        Dim Element As RefElements
        Element = GetElementsFromID(mSN)
        If IsNothing(Element) Then
            Return False
        End If
        Return True
    End Function

    Public Function GetLastScheduleFromIni(ByVal strArticleID As String) As Boolean
        If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data Then
            If Not _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data) Then
                Return False
            End If
        End If
        Dim mLastSchedule As String = _FileHandler.ReadIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "LastSchedule")
        If mLastSchedule <> "" AndAlso mLastSchedule <> FileHandler.s_DEFAULT Then
            strLastScheduleName = mLastSchedule
        End If
        Return True
    End Function
    '''<summary>
    '''Refreshing CheckList which contains reference info 
    '''</summary>
    Public Function RefreshingCheckList(ByVal strArticleID As String, Optional ByVal strScheduleName As String = "") As Boolean
        Dim sResult As String = ""
        SyncLock _objRefreshList
            _refCheckList.Clear()
            If Not _LocalArticle.GetArticle_FromID(strArticleID) Then
                Return False
            End If

            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data = String.Empty Then
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_FAMILY), "References.RefreshingCheckList")
                Return False
            End If


            For Each item As RefElements In _REFs.Values
                If item.Enable Then
                    If item.ProductFamily.ToUpper = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data.ToUpper Then
                        item.TestOK = False
                        If strScheduleName <> "" Then
                            If item.ScheduleName = strScheduleName Then
                                _refCheckList.Add(item.ID, item)
                                strLastScheduleName = item.ScheduleName
                            End If
                        Else
                            Dim _sResult As String = _FileHandler.ReadIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, item.SN + "_Shift_" & _Shift.ReturnCurrentShift.ToString)
                            If Not _Shift.CheckRefWithNowTime(_sResult, _Shift.ReturnCurrentShift) Then
                                strLastScheduleName = item.ScheduleName
                            End If
                            _refCheckList.Add(item.ID, item)
                        End If


                    End If
                End If
            Next
            Return True

        End SyncLock

    End Function

    Public Function GetLastScheduleName(ByVal strArticleID As String) As String
        SyncLock _objRefreshList
            Return strLastScheduleName
        End SyncLock
    End Function
    Public Function RefreshingSchedule(ByVal strArticleID As String, ByVal strScheduleName As String) As Boolean
        SyncLock _objRefreshList
            If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data <> _AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data Then
                If Not _LocalArticle.GetArticle_FromID(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NUMBER).Data) Then
                    Return False
                End If
            End If

            For Each item As RefElements In _REFs.Values
                If item.Enable Then
                    If item.ScheduleName = strScheduleName And item.ProductFamily.ToUpper = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data.ToUpper Then
                        item.TestOK = False
                        If item.ScheduleName = strScheduleName Then
                            strLastScheduleName = strScheduleName
                            If _refCheckList.ContainsKey(item.ID) Then
                                _refCheckList.Remove(item.ID)
                            End If
                            _refCheckList.Add(item.ID, item)
                            _FileHandler.WriteIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "LastSchedule", item.ScheduleName)
                            _FileHandler.WriteIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, item.SN + "_Shift_" & _Shift.ReturnCurrentShift.ToString, "")
                            _FileHandler.WriteIniFile(AppSettings.LogFolder, "REF", _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data, "Shift_" & _Shift.ReturnCurrentShift.ToString, "")
                        End If
                        RefChange = True
                    End If
                End If
            Next
            Return True
        End SyncLock
    End Function


    '''<summary>
    '''Modifing CheckList which contains reference info 
    '''</summary>
    Public Function ModifingCheckList(ByVal context As String) As Boolean

        Dim key As String
        Dim bulFound As Boolean
        Dim lstKeys As New List(Of String)
        Dim items As String() = context.Split(";"c)

        lstKeys.Clear()
        For Each key In _refCheckList.Keys
            bulFound = False
            For i As Integer = 0 To items.Length - 1 Step 1
                If items(i).Trim = key Then bulFound = True
            Next i
            If Not bulFound Then lstKeys.Add(key)
        Next key

        For Each key In lstKeys
            _refCheckList.Remove(key)
        Next key

        Return True

    End Function

    '''<summary>
    '''Romove some items with same schedule name in Checklist 
    '''</summary>
    Public Function RemoveItems(ByVal strScheduleName As String) As Boolean

        Try
            For Each item As RefElements In _refCheckList.Values
                If item.ScheduleName = strScheduleName Then
                    _refCheckList.Remove(item.ID)
                    Return True
                End If
            Next

            Return False
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_REMOVE_SCHEDULE, strScheduleName, "FAIL", ex.Message), "References.RemoveItems")
            Return False
        End Try
    End Function

    '''<summary>
    '''Romove one of items of Checklist 
    '''</summary>
    Public Function RemoveOne(ByVal ID As String) As Boolean
        Try
            _refCheckList.Remove(ID)
            Return True
        Catch ex As Exception
            _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_REMOVE, ID, "FAIL", ex.Message))
            Return False
        End Try
    End Function

    '''<summary>
    '''Use to add one spezified REF part with additional LK Name
    '''</summary>
    Public Function AddOne(ByVal locRef As RefElements) As Boolean

        'Nr	= Enable;LK Nr;SN;SampleName;ProductFamily;
        'Part_1  = 1;10118155;653210123456;SRT;ALL;
        'Part_2  = 1;10118154;653210123454;MASTERPART;ALL;
        'items(0)   ->Enable
        'items(1)   ->LK Nr
        'items(2)   ->SN
        'items(3)   ->SampleName
        'items(4)   ->ProductFamily
        'items(5)   ->ScheduleName

        If Not CheckingLKNumber(locRef.LkNumber) Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "FAIL", "LKNumber: " + locRef.LkNumber + " is invalid"), "References.AddOne")
            locRef = Nothing
            Return False
        ElseIf Not CheckingScheduleName(locRef.ScheduleName) Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "FAIL", "ScheduleName: " + locRef.ScheduleName + " is invalid"), "References.AddOne")
            locRef = Nothing
            Return False
        ElseIf locRef.ID = "" Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "FAIL", "SN is invalid"), "References.AddOne")
            locRef = Nothing
            Return False
        ElseIf locRef.RefName = "" Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "FAIL", "Reference Name not available"), "References.AddOne")
            locRef = Nothing
            Return False
        ElseIf locRef.ProductFamily = "" Then
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "FAIL", "Product Family is not available"), "References.AddOne")
            locRef = Nothing

            Return False

        ElseIf locRef.ScheduleName = "" Then
            Return False
        End If

        _REFs.Add(locRef.ID, locRef)
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_REFERENCE_ADD, locRef.ID, "Successful", ""), "References.AddOne")
        Return True

    End Function



#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' So ermitteln Sie überflüssige Aufrufe

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)

        On Error Resume Next

        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: Anderen Zustand freigeben (verwaltete Objekte).
            End If

            _ArticleReader = Nothing
            _LocalArticle = Nothing
            _FileHandler = Nothing
            _Logger = Nothing

            _Language = Nothing
            AppSettings = Nothing

        End If

        Me.disposedValue = True

    End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie oben in Dispose(ByVal disposing As Boolean) Bereinigungscode ein.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region


End Class