Public Class CheckSN
    Private _i As New Station
    Private _Logger As Logger
    Private _Parent As String = String.Empty
    Private AppSettings As Settings
    Private _Language As Language
    Protected _StatusDescription As String = ""
    Protected WithEvents _LineControl As LineControl2004

    Public Sub New(ByVal My_Parent As String, ByVal Devices As Dictionary(Of String, Object))
        _Parent = My_Parent
        _i.IdString = _Parent + "_CheckSN"
        _i.Name = _i.IdString
        AppSettings = CType(Devices(Settings.Name), Settings)
        _Language = CType(Devices(Language.Name), Language)
        _Logger = New Logger(AppSettings)
    End Sub
    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public ReadOnly Property IsReadRun() As Boolean
        Get
            Return _LineControl.ReadPreviousStamp_RUN
        End Get
    End Property

    Public ReadOnly Property IsWriteRun() As Boolean
        Get
            Return _LineControl.WriteCurrentStamp_RUN
        End Get
    End Property

    Public Function Init(ByVal mFileName As String) As Boolean
        If Not LineControlInit(mFileName) Then
            _StatusDescription = "LineControl;Init;" & _LineControl.StatusDescription
            Return False
        End If
        Return True
    End Function

    Protected Function LineControlInit(ByVal mFileName As String) As Boolean
        _LineControl = New LineControl2004("SN", "CheckSN", _i, AppSettings, _Language, mFileName)
        Return _LineControl.IsInit
    End Function

    Public Function StartCheckSN(ByVal mSN As String) As Boolean
        _LineControl.AdditionalInfos_Clear()
        _LineControl.AdditionalInfos(2) = ""
        _LineControl.AdditionalInfos(3) = ""
        _LineControl.ReadPreviousStamp(mSN, "00000000", "")
        Return True
    End Function

    Public Function EndCheckSN() As Boolean
        If Not _LineControl.ReadPreviousStamp_RUN Then
            If _LineControl.LastPreviousStamp = LineControl2004.enumPreviousTest.PREVIOUSTEST_PASS Then
                Return False
            Else
                _StatusDescription = "ReadPreviousStamp;" & _LineControl.StatusDescription
                Return True
            End If
        End If
        Return False
    End Function

    Public Function StartSaveSN(ByVal mSN As String) As Boolean
        _LineControl.AdditionalInfos_Clear()
        _LineControl.AdditionalInfos(2) = ""
        _LineControl.AdditionalInfos(3) = ""
        _LineControl.WriteCurrentStamp(mSN, "00000000", True, "")
        Return True
    End Function

    Public Function EndSaveSN() As Boolean

        If Not _LineControl.WriteCurrentStamp_RUN Then
            If _LineControl.LastWriteResult Then
                Return True
            Else
                _StatusDescription = "WriteCurrentStamp;" & _LineControl.StatusDescription
                Return False
            End If
        End If
        Return False
    End Function
End Class
