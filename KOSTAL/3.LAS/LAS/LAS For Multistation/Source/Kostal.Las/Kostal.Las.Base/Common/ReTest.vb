Public Class ReTestElement

    Private _ID As String = String.Empty
    Private _SN As String = String.Empty
    Private _LkNumber As String = String.Empty
    Private _ProductFamily As String = String.Empty
    Private _ScheduleName As String = String.Empty

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
    Public Sub New(ByVal ID As String, ByVal SN As String, ByVal LkNumber As String, ByVal ProductFamily As String, ByVal ScheduleName As String)
        _ID = ID
        _SN = SN
        _LkNumber = LkNumber
        _ProductFamily = ProductFamily
        _ScheduleName = ScheduleName
    End Sub



End Class
Public Class ReTestList

    Private _i As New Station
    Private _Parent As String = String.Empty

    Private AppSettings As Settings
    Private _Language As Language
    Private _Logger As Logger
    Private _ReTestMode As Boolean = False
    Private _ReTestListElement As New Dictionary(Of String, ReTestElement)
    Public Const Name As String = "_ReTestList"

    Public Property ReTestMode() As Boolean
        Get
            Return _ReTestMode
        End Get
        Set(ByVal value As Boolean)
            _ReTestMode = value
        End Set
    End Property

    Public ReadOnly Property ReTestListElement() As Dictionary(Of String, ReTestElement)
        Get
            Return _ReTestListElement
        End Get
    End Property

    Public Sub New(ByVal My_Parent As String, ByVal Devices As Dictionary(Of String, Object))

        _Parent = My_Parent
        _i.IdString = _Parent + "_ReTest"

        _i.Name = _i.IdString

        AppSettings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)

        _Logger = New Logger(AppSettings)

        _ReTestListElement.Clear()
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_INIT, "Successful"), "ReTestList.Init")
    End Sub

    Public Function AddOne(ByVal id As ReTestElement) As Boolean
        Try
            _ReTestListElement.Add(id.ID, id)
            Return True
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_ADD, id.ToString, "FAIL", ex.Message), "ReTestList.AddOne")
            Return False
        End Try

    End Function

    Public Function RemoveOne(ByVal id As String) As Boolean
        Try
            If _ReTestListElement.ContainsKey(id) Then
                _ReTestListElement.Remove(id)
            End If
            Return True
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_RETEST_REMOVE, id, "FAIL", ex.Message), "ReTestList.RemoveOne")
            Return False
        End Try

    End Function
End Class
