


#Region "ChangeRequests"

#Region "E-Mail from 2014_02_19_17_17"

'Duempelmann, Frank
'Von: Schaefer, Andreas
'Gesendet: Mittwoch, 19. Februar 2014 17:17
'An: Duempelmann, Frank
'Cc: Becker, Wolfgang; Rittinghaus, Holger; Richter, Uwe; Heine, Dr. Ingo
'Betreff: CAQ Reporting Montagelinie DBE
'Hallo Herr Dümpelmann,
'wie soeben besprochen bitte das CAQ Reporting bei den Montagelinien DBE (LK + KOSPA) wie folgt ändern:
'• Zukünftig nur noch den vollständigen Serienablauf „Full“ (Montage + Prüfen) an das CAQ reporten => echter
'FPY
'• Zusätzliche Betriebsmodi „alternate shadow“ (Gewährleistung, Retest, …) nicht mehr an das CAQ System
'reporten
'Bitte den Zeitpunkt der Umstellung per Mail dokumentieren.
'Mit freundlichen Grüßen / Kind regards
'Leopold Kostal GmbH & Co. KG
'Automobil Elektrik / Automotive Electrical Systems
'Dipl.-Ing., Dipl-Wirt.Ing.
'Andreas Schäfer
'APP9 Leitung Montage- und Prüfequipment
'Senior Manager Assembly- and Testequipment
'An der Bellmerei 10, 58513 Lüdenscheid
'Deutschland / Germany
'Telefon: +49 2351 16 - 2221
'Telefax: +49 2351 16 - 2444
'Mobil: +49 163 9160 121
'E-Mail: an.schaefer@kostal.com
'Internet: http://

#End Region

#End Region



#Region "CAQ Interface"


'Version 2.0.0.0 Build 2011_02_09_00
'Add Test Status
'
'Version 2.0.1.0 Build 2011_03_25_00
'Add Class CaqInfo

'Version 2.0.2.0 Build 2013_02_20_00
'Release CAQ/FPY Station Names from real Stations

Public Class CaqInterface

	Public Enum enumCaqInterfaceStatus
		WindowsError = -99
		FailWhileWrite = -2
		FailIniFileDoNotExit = -1
		NotInitialized = 0
		Initialized = 1
		Disabled = 2
		InWriteMode = 3
	End Enum

	Public Enum enumCaqFirstTestStatus
		Normal = 1
		ProcessSupervision = 2
		WeeklyReport = 3
		Analysis = 4
	End Enum

	Public Enum enumCaqSecondTestStatus
		[Nothing] = 1
		ReTest = 2
		TestError = 3
	End Enum

    Protected _Caq As Global.CAQ.CAQ2002
    Protected Const CAQ_FILENAME As String = "_CAQSERV.INI"
    Protected _i As Station
    Protected AppSettings As New Settings
    Protected _FileHandler As New FileHandler
    Protected _SubStationCfg As SubStationCfg
    Protected _IsInit As Boolean
    Protected _IsDisabled As Boolean
    Protected _Status As enumCaqInterfaceStatus
    Protected _StatusDescription As String
    Protected _CaqIniFile As String
    Protected _ArticleNumber As String
    Protected _LastArticleNumber As String
    Protected _LastTestResult As Boolean
    Protected _TimerOut As System.Threading.Timer
    Protected _TimeOutCallback As System.Threading.TimerCallback
    Protected _Pass As Boolean
    Protected _Write_RUN As Boolean

    Protected _FailNo As String
    Protected _FailItemNo As String
    Protected _FailValue As String
    Protected _FailText As String
    Protected _FailValInfo As String

    Protected _TestStatus As String

    Protected Delegate Function dWrite() As Boolean
    Protected pWrite As New dWrite(AddressOf _Write)
    Protected pWriteCB As AsyncCallback = New AsyncCallback(AddressOf _WriteCB)

    Public Event WriteComplete(ByVal Pass As Boolean)
    Public Event ArticleChangedTo(ByVal NewArticle As String)
    '

#Region "Properties"

    Public ReadOnly Property TestStatus() As String
        Get
            Return _TestStatus
        End Get
    End Property

    Public ReadOnly Property IsInit() As Boolean
        Get
            Return _IsInit
        End Get
    End Property

    Public ReadOnly Property IsDisabled() As Boolean
        Get
            Return _IsDisabled
        End Get
    End Property

    Public ReadOnly Property Pass() As Boolean
        Get
            Return _Pass
        End Get
    End Property

    Public ReadOnly Property Write_RUN() As Boolean
        Get
            Return _Write_RUN
        End Get
    End Property

    Public ReadOnly Property Status() As enumCaqInterfaceStatus
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            If _Status < enumCaqInterfaceStatus.NotInitialized Then
                Return _Status.ToString & ";" & _StatusDescription
            Else
                Return _Status.ToString
            End If
        End Get
    End Property

    Public Property ArticleNumber() As String
        Get
            Return _ArticleNumber
        End Get
        Set(ByVal value As String)
            _ArticleNumber = value
        End Set
    End Property

    Public Property FailItemNo() As String
        Get
            Return _FailItemNo
        End Get
        Set(ByVal value As String)
            _FailItemNo = value
        End Set
    End Property

    Public Property FailNo() As String
        Get
            Return _FailNo
        End Get
        Set(ByVal value As String)
            _FailNo = value
        End Set
    End Property

    Public Property FailText() As String
        Get
            Return _FailText
        End Get
        Set(ByVal value As String)
            _FailText = value
        End Set
    End Property

    Public Property FailValInfo() As String
        Get
            Return _FailValInfo
        End Get
        Set(ByVal value As String)
            _FailValInfo = value
        End Set
    End Property

    Public Property FailValue() As String
        Get
            Return _FailValue
        End Get
        Set(ByVal value As String)
            _FailValue = value
        End Set
    End Property

#End Region

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal i As Station, ByVal mSettings As Settings)

        _i = i
        _SubStationCfg = SubStationCfg
        AppSettings = mSettings
        _IsInit = False
        _IsDisabled = False
        _Status = enumCaqInterfaceStatus.NotInitialized
        _StatusDescription = ""
        _ArticleNumber = ""
        _CaqIniFile = AppSettings.ConfigFolder & _SubStationCfg.Config
        If _SubStationCfg.Config = "" Or Not _FileHandler.FileExist(_CaqIniFile) Then
            _Status = enumCaqInterfaceStatus.FailIniFileDoNotExit
            _StatusDescription = _CaqIniFile
            Return
        End If
        'sResult = _FileHandler.ReadIniFile(AppSettings.IniFolder, AppSettings.ApplicationName, "CAQ", _i.IdString & "_CAQ_Disabled")
        If Not _SubStationCfg.Enable Then
            _IsDisabled = True
            _IsInit = True
            _Status = enumCaqInterfaceStatus.Disabled
            Return
        End If

        _TimeOutCallback = New System.Threading.TimerCallback(AddressOf TimeReached)
        _TimerOut = New System.Threading.Timer(_TimeOutCallback)
        _TimerOut.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)

        _Caq = New Global.CAQ.CAQ2002
        SetTestStatus()
        _IsInit = True
        _Status = enumCaqInterfaceStatus.Initialized
    End Sub


    Protected Overrides Sub Finalize()
        On Error Resume Next
        _Caq.CloseDLL()
        _Caq = Nothing
        MyBase.Finalize()
    End Sub

    Public Sub SetTestStatus(Optional ByVal FirstCode As enumCaqFirstTestStatus = enumCaqFirstTestStatus.Normal, Optional ByVal SecondCode As enumCaqSecondTestStatus = enumCaqSecondTestStatus.Nothing)
        Dim mFirstCode As String, mSecondCode As String

        Select Case FirstCode
            Case enumCaqFirstTestStatus.Normal
                mFirstCode = "N"
            Case enumCaqFirstTestStatus.ProcessSupervision
                mFirstCode = "P"
            Case enumCaqFirstTestStatus.WeeklyReport
                mFirstCode = "W"
            Case enumCaqFirstTestStatus.Analysis
                mFirstCode = "A"
            Case Else
                mFirstCode = "N"
        End Select

        Select Case SecondCode
            Case enumCaqSecondTestStatus.Nothing
                mSecondCode = ""
            Case enumCaqSecondTestStatus.ReTest
                mSecondCode = "R"
            Case enumCaqSecondTestStatus.TestError
                mSecondCode = "T"
            Case Else
                mSecondCode = ""
        End Select

        If Not _IsInit Or _IsDisabled Then Return
        _Caq.SETPAR("TEST_STATUS", mFirstCode & mSecondCode)

    End Sub

    Public Overloads Function Write(ByVal TestResult As Boolean) As Boolean
        _Pass = False
        _LastTestResult = TestResult
        If Not _IsInit Then Return False
        If _IsDisabled Then
            _Pass = True
            Return True
        End If
        If _ArticleNumber = "" Then Return False
        _TimerOut.Change(5000, System.Threading.Timeout.Infinite)
        _Write_RUN = True
        pWrite.BeginInvoke(pWriteCB, Nothing)
        Return True
    End Function

    Public Overloads Function Write(ByVal TestResult As Boolean, ByVal Article_Number As String) As Boolean
        _Pass = False
        _LastTestResult = TestResult
        If Not _IsInit Then Return False
        If _IsDisabled Then
            _Pass = True
            Return True
        End If
        If Article_Number = "" Then Return False
        _ArticleNumber = Article_Number
        _TimerOut.Change(5000, System.Threading.Timeout.Infinite)
        _Write_RUN = True
        pWrite.BeginInvoke(pWriteCB, Nothing)
        Return True
    End Function

    Private Function _Write() As Boolean
        If _ArticleNumber = "" Then Return False
        If _LastArticleNumber <> _ArticleNumber Then
            _LastArticleNumber = _ArticleNumber
            _Caq.Init(_LastArticleNumber, _CaqIniFile)
            RaiseEvent ArticleChangedTo(_LastArticleNumber)
        End If
        Try
            If _LastTestResult Then
                _Caq.FailItemNo = ""
                _Caq.FailNo = ""
                _Caq.FailText = ""
                _Caq.FailValInfo = _FailValInfo
                _Caq.FailValue = ""
                _Caq.CountRec(-1S, 0.0R)
            Else
                _Caq.FailItemNo = _FailItemNo
                _Caq.FailNo = _FailNo
                _Caq.FailText = _FailText
                _Caq.FailValInfo = _FailValInfo
                _Caq.FailValue = _FailValue
                _Caq.CountRec(0S, 0.0R)
            End If
            Return True
        Catch ex As Exception
            _Status = enumCaqInterfaceStatus.WindowsError
            _StatusDescription = ex.Message
            Return False
        End Try
    End Function

    Private Sub _WriteCB(ByVal Result As IAsyncResult)
        _TimerOut.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        _Pass = pWrite.EndInvoke(Result)
        _Write_RUN = False
        RaiseEvent WriteComplete(_Pass)
    End Sub

    Private Sub TimeReached(ByVal state As Object)
        _TimerOut.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite)
        _Status = enumCaqInterfaceStatus.FailWhileWrite
        _StatusDescription = "TimeOut"
        _Pass = False
        _Write_RUN = False
        RaiseEvent WriteComplete(_Pass)
    End Sub
End Class


Public Class CaqInfo

    Private _StationNames As New Dictionary(Of String, String)


    Public ReadOnly Property StationNames() As Dictionary(Of String, String)
        Get
            Return _StationNames
        End Get
    End Property


    Public Function Clear() As Boolean
        Try
            _StationNames.Clear()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function Add(ByVal Key As String, ByVal Name As String) As Boolean
        Try
            _StationNames.Add(Key, Name)
            Return True

        Catch ex As Exception
            Return False

        End Try

    End Function

    Public Function GetName(ByVal Key As String) As String

        Try

            Return _StationNames.Item(Key)
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class


#End Region



#Region "FirstPartYield"



'**********************************************************************************************************************
'**********************************************************************************************************************
'FPY - FirstPartYield
'**********************************************************************************************************************
'**********************************************************************************************************************


'**********************************************************************************************************************
'Definition Schedule:
'**********************************************************************************************************************

'Only Schedules with Prefix "ALTERNATE_SCHEDULE_" are write to WT.Schedule.

'The WT.Schedule contains the Key without the Prefix "ALTERNATE_SCHEDULE_".
'For example > Schedule ID = "ALTERNATE_SCHEDULE_Warranty" > Content WT.Schedule > "Warranty"
'For example > Schedule ID = "ALTERNATE_SCHEDULE_Analysis" > Content WT.Schedule > "Analysis"


'**********************************************************************************************************************
'Version 1.0.0.0 Build 2011_08_26_00
'**********************************************************************************************************************

'Definition der Felder in der CAQ

'Fehlerort()	= Split(PLC_OUT_WtInfo.PartFailLocation, "/")
'_CAQ.FailNo	= Fehlerort(0) (SPS Kennung)	z.B. "St.5"
'_CAQ.FailText	= Fehlerort(1) Klartext			z.B. "Handarbeitsplatz 5"

'_CAQ.FailValue = _PLC_OUT_WtInfo.PartFailText
'_CAQ.FailValInfo = _PLC_OUT_WtInfo.PartFailText
'_CAQ.FailItemNo = _PLC_OUT_WtInfo.PartFailTestStep

'If _PLC_OUT_WtInfo.Schedule = "" Then
'	_CAQ.SetTestStatus(CaqInterface.enumCaqFirstTestStatus.Normal)
'Else
'	_CAQ.SetTestStatus(CaqInterface.enumCaqFirstTestStatus.Analysis)
'End If

'**********************************************************************************************************************
'V1.1.0.0 Build 2014_02_20_00 - see > #Region "ChangeRequests" > #Region "E-Mail from 2014_02_19_17_17"
'**********************************************************************************************************************


''' <summary>
''' V1.1.0.0 Build 2014_02_20_00
''' </summary>
''' <remarks></remarks>
Public Class CAQ

    Inherits CaqInterface

    Implements IDisposable
    Private IsDisposed As Boolean

    Protected _Language As Language

    Private _CaqStationNames As New CaqInfo

    Private _WtData As New WT



#Region "Properties"

    Public ReadOnly Property WtData() As WT
        Get
            Return _WtData
        End Get
    End Property


#End Region



    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal myLanguage As Language, ByVal Allstations As Dictionary(Of String, IStationTypeBase))


        MyBase.New(SubStationCfg, MyStation, _AppSettings)

        'Dim Index As Integer

        Dim StationKey As String = ""

        _Language = myLanguage

        _CaqStationNames.Clear()

        'Try

        '    Index = 1

        '    Do

        '        StationKey = _FileHandler.ReadIniFile(AppSettings.IniFolder, AppSettings.ApplicationName, "CAQ", _i.IdString + FpyName + Index.ToString)

        '        If StationKey <> _FileHandler.ErrorString Then
        '            _CaqStationNames.Add(StationKey, "")
        '        End If

        '        Index += 1

        '    Loop Until StationKey = _FileHandler.ErrorString

        '    LanguageInit()

        'Catch ex As Exception
        '    _IsInit = False
        'End Try

    End Sub

    Public Overridable Function Init() As Boolean

        Return IsInit

    End Function

    Public Sub Dispose() Implements IDisposable.Dispose

        On Error Resume Next

        _i = Nothing
        AppSettings = Nothing

        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If

    End Sub



    Protected Overrides Sub Finalize()
        IsDisposed = True
        MyBase.Finalize()
    End Sub



    'Public Sub LanguageInit()

    '    Dim StationKey As Dictionary(Of String, String).KeyCollection, mKeys(0) As String
    '    Dim StationName As String = ""
    '    Dim mLoop As Integer

    '    Try

    '        StationKey = _CaqStationNames.StationNames.Keys

    '        ReDim mKeys(StationKey.Count - 1)

    '        StationKey.CopyTo(mKeys, 0)

    '        For mLoop = 0 To mKeys.GetUpperBound(0)
    '            StationName = _FileHandler.ReadLanguageFile(AppSettings.LngFolder, _Language.SelectedLanguageFileName, "CaqInfo", mKeys(mLoop))
    '            _CaqStationNames.StationNames.Item(mKeys(mLoop)) = StationName
    '        Next

    '    Catch ex As Exception

    '    End Try

    'End Sub



    Public Shadows Function Write(ByVal WtData As WT, ByVal Pass As Boolean, ByVal Article_Number As String) As Boolean

        If WtData Is Nothing Then Return False

        _WtData = WtData

        '===============================================
        'Ignore all alternate schedules and return PASS
        '===============================================
        If Not _WtData.Schedule.Contains(LAS_ScheduleMode.ProductionMode.ToString) Then
            _Pass = True
            Return True
        End If
        '===============================================

        GenerateData()

        Return MyBase.Write(Pass, Article_Number)

    End Function



    Private Sub GenerateData()
        'Dim FailLocation() As String

        'FailLocation = Split(_WtData.PartFailLocation, "/")

        'If FailLocation.GetLength(0) >= 1 Then
        '    _FailNo = FailLocation(0)
        '    _FailText = _CaqStationNames.GetName(FailLocation(0))
        'Else
        '    _FailNo = ""
        '    _FailText = ""
        '        'End If
        '# The following is from VET
        '#                CAQ.FailItemNo = ErrorInfo.ts;
        '#                CAQ.FailNo = ErrorInfo.ts;
        '#                CAQ.FailValInfo = ErrorInfo.infor;
        '#                CAQ.FailValue = ErrorInfo.MeasuredValue;
        '#                CAQ.FailText = ErrorInfo.FailText;

        _FailValue = _WtData.PartFailValue

        _FailText = _WtData.PartFailText
        _FailValInfo = _WtData.Number.ToString

        _FailNo = _WtData.PartFailCode
        _FailItemNo = _WtData.PartFailTestStep

        If _WtData.Schedule.Contains(LAS_ScheduleMode.ProductionMode.ToString) Then
            SetTestStatus(CaqInterface.enumCaqFirstTestStatus.Normal)
        Else
            SetTestStatus(CaqInterface.enumCaqFirstTestStatus.Analysis)
        End If

    End Sub



End Class



#End Region
