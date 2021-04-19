
'LineController Class
'Author		Frank Dümpelmann
'Version	1.0.6.0
'Build		2012_10_16


'V1.0.2.0 - Build		2010_03_02
'	DB_Cange integrated to change the database

'V1.0.3.0 - Build		2010_10_05
'	WritCurrentStamp - Set ActualTime in dll

'V1.0.4.0	Build 2011_01_05	
'	UseOnlyTable integrated (Linecontroller.dll Version 1.3.0

'V1.0.4.1	Build 2011_01_06	
'	UseOnlyTable integrated in ReadPreviousStamp too (Linecontroller.dll Version 1.3.0

'V1.0.4.2	Build 2011_01_06	
'	Integrate Instance No in New Function

'V1.0.4.3	Build 2011_01_07	
'	Different LineController IniFile and LineControllerName possible

'V1.0.4.4	Build 2011_02_15	
'	Clear Additional Infos

'V1.0.5.0	Build 2011_09_20	
'	Insert Scheduleparameter for Analysis and Warrenty

'V1.0.5.1	Build 2011_12_15	
'	Patch in Sub New

'V1.0.6.0	Build 2012_10_16
'	Default_Useonly Table patched.

'V1.0.7.0 Build 2012_10_19_00
'   _LineController.AdditionalText(3) = Schedule
Imports Kostal.Las.ArticleProvider
Imports Linecontroller

Public Class ChildElement
    Protected _mSN As String
    Protected _mArticle As String

    Public Property SN As String
        Set(ByVal value As String)
            _mSN = value
        End Set
        Get
            Return _mSN
        End Get
    End Property

    Public Property Article As String
        Set(ByVal value As String)
            _mArticle = value
        End Set
        Get
            Return _mArticle
        End Get
    End Property


    Public Sub New(ByVal mSN As String, ByVal mArticle As String)
        _mSN = mSN
        _mArticle = mArticle
    End Sub

End Class

Public Class LineControl2004


    Public Enum enumCurrentTest
        CURRENTTEST_NO_ERROR = 0
        CURRENTTEST_NO_ANY_RECORD = 1
        CURRENTTEST_RECORD_FOUND = 2
        CURRENTTEST_ERROR_OCCURED = 4
    End Enum

    Public Enum enumPreviousTest
        PREVIOUSTEST_NOTHING = 0
        PREVIOUSTEST_NONE = 1
        PREVIOUSTEST_PASS = 2
        PREVIOUSTEST_FAIL = 3
        PREVIOUSTEST_LC_FAIL = 4
    End Enum

    Public Enum enumLineControl2004Status
        WindowsError = -99
        FailWhileWriteLC = -5
        FailWhileWriteCurrentStamp = -4
        FailWhileWriteResetCurrentStamp = -3
        FailWhileReadPreviousStamp = -2
        FailWhileInit = -1
        NotInitialized = 0
        Initialized = 1
        Disabled = 2
    End Enum


    Protected _LineController As New clsDB_Linecontrolling
    Protected _Parent As String = ""
    Protected _LineControllerIniFile As String = ""
    Protected _LineControllerName As String = ""
    Protected _IsInit As Boolean
    Protected _IsDisabled As Boolean
    Protected _Status As enumLineControl2004Status
    Protected _StatusDescription As String = ""
    Protected _DefaultPreviousTest As String = ""
    Protected _LastCurrentStamp As enumCurrentTest
    Protected _LastPreviousStamp As enumPreviousTest
    Protected _TraceId As String = ""
    Protected _DefaultCurrentTest As String = ""
    Protected _DefaultUseOnlyTable As String = ""
    Protected AppSettings As Settings
    Protected _Language As Language
    Protected _i As Station
    Protected _FileHandler As New FileHandler
    Protected _Barcode As New Barcode_LK
    Protected _ParentIdString As String

    Protected Const _Suffix_Analysis As String = "_ANA"
    Protected Const _Suffix_Warranty As String = "_WAR"


    Protected _Parameters As New Dictionary(Of String, ChildElement)

    Protected Delegate Function dReadCurrentStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal UseOnlyTable As String, ByVal Schedule As String) As enumCurrentTest
    Protected pReadCurrentStamp As New dReadCurrentStamp(AddressOf _ReadCurrentStamp)
    Protected pReadCurrentStampCB As AsyncCallback = New AsyncCallback(AddressOf _ReadCurrentStampCB)
    Public Event ReadCurrentStampComplete(ByVal Status As enumCurrentTest)
    Protected _ReadCurrentStamp_RUN As Boolean

    Protected Delegate Function dReadPreviousStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal UseOnlyTable As String, ByVal Schedule As String) As enumPreviousTest
    Protected pReadPreviousStamp As New dReadPreviousStamp(AddressOf _ReadPreviousStamp)
    Protected pReadPreviousStampCB As AsyncCallback = New AsyncCallback(AddressOf _ReadPreviousStampCB)

    Public Event ReadPreviousStampComplete(ByVal Status As enumPreviousTest)
    Protected _ReadPreviousStamp_RUN As Boolean

    Protected Delegate Function dWriteCurrentStamp(ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal CurrentTest As String, ByVal UseOnlyTable As String, ByVal Schedule As String) As Boolean
    Protected pWriteCurrentStamp As New dWriteCurrentStamp(AddressOf _WriteCurrentStamp)
    Protected pWriteCurrentStampCB As AsyncCallback = New AsyncCallback(AddressOf _WriteCurrentStampCB)
    Public Event WriteCurrentStampComplete(ByVal Status As Boolean)

    Protected Delegate Function dWriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal WithChildren As Boolean, ByVal AdditionalTextPositon As Integer, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal CurrentTest As String, ByVal UseOnlyTable As String, ByVal Schedule As String) As Boolean
    Protected pWriteReTestCurrentStamp As New dWriteReTestCurrentStamp(AddressOf _WriteReTestCurrentStamp)
    Protected pWriteReTestCurrentStampCB As AsyncCallback = New AsyncCallback(AddressOf _WriteReTestCurrentStampCB)
    Public Event WriteReTestCurrentStampComplete(ByVal Status As Boolean)

    Protected _WriteCurrentStamp_RUN As Boolean
    Protected _WriteReTestCurrentStamp_RUN As Boolean
    Protected _LastWriteResult As Boolean
    Protected _PrviousStation As String
    Protected _CurrentStation As String


#Region "Properties"

    Public ReadOnly Property LastWriteResult() As Boolean
        Get
            Return _LastWriteResult
        End Get
    End Property

    Public Property PrviousStation() As String
        Get
            Return _PrviousStation
        End Get
        Set(value As String)
            _PrviousStation = value
        End Set
    End Property


    Public Property CurrentStation() As String
        Get
            Return _CurrentStation
        End Get
        Set(value As String)
            _CurrentStation = value
        End Set
    End Property

    Public ReadOnly Property ReadCurrentStamp_RUN() As Boolean
        Get
            Return _ReadCurrentStamp_RUN
        End Get
    End Property

    Public ReadOnly Property ReadPreviousStamp_RUN() As Boolean
        Get
            Return _ReadPreviousStamp_RUN
        End Get
    End Property

    Public ReadOnly Property WriteCurrentStamp_RUN() As Boolean
        Get
            Return _WriteCurrentStamp_RUN
        End Get
    End Property


    Public ReadOnly Property WriteReTestCurrentStamp_RUN() As Boolean
        Get
            Return _WriteReTestCurrentStamp_RUN
        End Get
    End Property

    Public ReadOnly Property ErrorString() As String
        Get
            Return "#ERROR#"
        End Get
    End Property

    Public ReadOnly Property LineControllerIniFile() As String
        Get
            Return _LineControllerIniFile
        End Get
    End Property

    Public ReadOnly Property LineControllerName() As String
        Get
            Return _LineControllerName
        End Get
    End Property

    Public ReadOnly Property IsDisabled() As Boolean
        Get
            Return _IsDisabled
        End Get
    End Property

    Public ReadOnly Property IsInit() As Boolean
        Get
            Return _IsInit
        End Get
    End Property

    Public ReadOnly Property Status() As enumLineControl2004Status
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            If _Status <> enumLineControl2004Status.Initialized Then
                Return _Status.ToString + ";" + _StatusDescription
            Else
                Return _StatusDescription
            End If

        End Get
    End Property

    Public ReadOnly Property DefaultPreviousTest() As String
        Get
            Return _DefaultPreviousTest
        End Get
    End Property

    Public ReadOnly Property DefaultCurrentTest() As String
        Get
            Return _DefaultCurrentTest
        End Get
    End Property

    Public ReadOnly Property DefaultUseOnlyTable() As String
        Get
            Return _DefaultUseOnlyTable
        End Get
    End Property

    Public ReadOnly Property LastCurrentStamp() As enumCurrentTest
        Get
            Return _LastCurrentStamp
        End Get
    End Property

    Public ReadOnly Property LastPreviousStamp() As enumPreviousTest
        Get
            Return _LastPreviousStamp
        End Get
    End Property

    Public ReadOnly Property TraceId() As String
        Get
            Return _TraceId
        End Get
    End Property

    Public Property AdditionalInfos(ByVal index As Integer) As String
        Get
            Return _LineController.AdditionalText(index)
        End Get
        Set(ByVal value As String)
            _LineController.AdditionalText(index) = value
        End Set
    End Property

    Public ReadOnly Property MasterDB_Name() As String
        Get
            Return _LineController.MasterDB_Name
        End Get
    End Property

    Public ReadOnly Property SlaveDB_Name() As String
        Get
            Return _LineController.SlaveDB_Name
        End Get
    End Property

#End Region

#Region "Initialize"

    Public Sub New(ByVal ParentIdString As String, ByVal LineControllerName As String, ByVal MyStation As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language, ByVal LineControllerIniFile As String)
        _IsInit = False
        _IsDisabled = False
        _ParentIdString = ParentIdString
        _LineControllerName = LineControllerName
        _LineControllerIniFile = LineControllerIniFile
        _Status = enumLineControl2004Status.NotInitialized
        AppSettings = _AppSettings
        _Language = MyLanguage
        _i = MyStation
        _ReadPreviousStamp_RUN = False
        _WriteCurrentStamp_RUN = False
        _Parameters.Clear()
        Init()
    End Sub


    Protected Function Init() As Boolean
        Dim iResult As Integer

        If _ParentIdString Is Nothing Or _ParentIdString = String.Empty Then
            _Status = enumLineControl2004Status.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT1)
            _IsInit = False
            Return False
        End If
        _Parent = _ParentIdString


        If _LineControllerName Is Nothing Or _LineControllerName = String.Empty Then
            _Status = enumLineControl2004Status.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT2, _Parent, _LineControllerName)
            _IsInit = False
            Return False
        End If

        If _LineControllerIniFile = String.Empty Or _LineControllerIniFile = _FileHandler.ErrorString Then
            _Status = enumLineControl2004Status.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT3, _LineControllerIniFile, _Parent, _LineControllerName)
            _IsInit = False
            Return False
        End If

        _TraceId = Trim(AppSettings.MachineIdentifier.TraceId)

        If _TraceId = String.Empty Or _TraceId = _FileHandler.ErrorString Or Not IsNumeric(_TraceId) Then
            _Status = enumLineControl2004Status.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT4, _TraceId, _Parent, _LineControllerName)
            _IsInit = False
            Return False
        End If

        _LineController.INISection = _Parent + "_" + _LineControllerName + "_SETTINGS"

        If _LineControllerIniFile.Contains(".ini") Then
            _LineController.INIFile = AppSettings.LineControlFolder + _LineControllerIniFile

        Else
            _LineController.INIFile = AppSettings.LineControlFolder + _LineControllerIniFile + ".ini"
        End If

        _LineController.TraceID = _TraceId

        _LineController.MasterDB_Name = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, "MASTERDATABASE", "DBNAME"))
        If _LineController.MasterDB_Name = _FileHandler.ErrorString Then
            _LineController.MasterDB_Name = String.Empty
        End If

        _LineController.SlaveDB_Name = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, "SLAVEDATABASE", "DBNAME"))
        If _LineController.SlaveDB_Name = _FileHandler.ErrorString Then
            _LineController.SlaveDB_Name = String.Empty
        End If

        _DefaultPreviousTest = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, _LineController.INISection, "PREVIOUS_TEST"))
        If _DefaultPreviousTest = _FileHandler.ErrorString Then
            _DefaultPreviousTest = String.Empty
        End If
        _PrviousStation = _DefaultPreviousTest

        _DefaultCurrentTest = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, _LineController.INISection, "CURRENT_TEST"))
        If _DefaultCurrentTest = _FileHandler.ErrorString Then
            _Status = enumLineControl2004Status.FailWhileInit
            _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT5, _LineController.INISection)
            _DefaultCurrentTest = String.Empty
            _IsInit = False
            Return False
        End If
        _CurrentStation = _DefaultCurrentTest
        _DefaultUseOnlyTable = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, "GENERAL", "UseOnlyTable"))
        If _DefaultUseOnlyTable = _FileHandler.ErrorString Then
            _DefaultUseOnlyTable = String.Empty
        End If

        _LineController.LocalPath = Trim(_FileHandler.ReadIniFile(AppSettings.LineControlFolder, _LineControllerIniFile, "PATH", "LOCAL_PATH"))
        If _LineController.LocalPath = _FileHandler.ErrorString Then
            _LineController.LocalPath = String.Empty
        End If

        If Not System.IO.Directory.Exists(_LineController.LocalPath) And _LineController.LocalPath <> String.Empty Then
            System.IO.Directory.CreateDirectory(_LineController.LocalPath)
        End If

        _LineController.Logfile = AppSettings.LogFolder + _Parent + "_" + _LineControllerName + ".log"


        If _DefaultUseOnlyTable <> String.Empty Then
            _LineController.UseOnlyTable = _DefaultUseOnlyTable
        Else
            _DefaultUseOnlyTable = _LineController.UseOnlyTable
        End If

        _LineController.LogLevel = 255
        _LineController.LogEnabled = True
        iResult = _LineController.Init

        If iResult <> 0 Then
            _IsInit = False
            _Status = enumLineControl2004Status.FailWhileInit
            ErrorHandler_Init(iResult)
            Return False
        Else
            _IsInit = True
            _Status = enumLineControl2004Status.Initialized
            _StatusDescription = ""

            Return True
        End If
    End Function

    Public Sub AdditionalInfos_Clear()
        Dim l As Integer

        For l = 0 To 3
            _LineController.AdditionalText(l) = ""
        Next
    End Sub


    Public Function AddChild(ByVal Child As ChildElement) As Boolean
        If Not _IsInit Then Return False
        If _IsDisabled Then Return True
        Try
            _Parameters.Add(_Parameters.Count.ToString, Child)
            Return True
        Catch ex As Exception
            _StatusDescription = ex.Message & ";" & Child.SN
            _Status = enumLineControl2004Status.WindowsError
            Return False
        End Try
    End Function

    Protected Sub ErrorHandler_Init(ByVal lErrorNo As Integer)

        Select Case lErrorNo
            Case 0
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT6)
            Case -1
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT7)
            Case -2
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT8)
            Case -3
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT9)
            Case -4
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT10)
            Case -5
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT11)
            Case -6
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT12)
            Case -7
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT13)
            Case -8
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT14)
            Case -9
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT15)
            Case -10
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT16)

            Case Else
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_INIT17)
        End Select

    End Sub

#End Region

#Region "ReadCurrentStamp"

    '=============================================================================
    'ReadCurrentStamp
    '=============================================================================

    Public Overloads Sub ReadCurrentStamp(ByVal Master_DB As String, ByVal Slave_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, Master_DB, Slave_DB, "", Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStamp(ByVal Master_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, Master_DB, "", "", Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, "", "", "", Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, "", "", "", "", Schedule, pReadCurrentStampCB, Nothing)
    End Sub


    '=============================================================================
    'ReadCurrentStamp UseOnlyTable
    '=============================================================================

    Public Overloads Sub ReadCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, Master_DB, Slave_DB, Table, Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, Master_DB, "", Table, Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStampOnlyTable(ByVal Table As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal CurrentTest As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, CurrentTest, "", "", Table, Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadCurrentStampOnlyTable(ByVal Table As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal Schedule As String)
        _ReadCurrentStamp_RUN = True
        pReadCurrentStamp.BeginInvoke(sSerialNumber, sArticleNo, "", "", "", Table, Schedule, pReadCurrentStampCB, Nothing)
    End Sub

    '=============================================================================
    '=============================================================================

    Protected Sub _ReadCurrentStampCB(ByVal Result As IAsyncResult)
        _LastCurrentStamp = pReadCurrentStamp.EndInvoke(Result)
        _ReadCurrentStamp_RUN = False
        RaiseEvent ReadCurrentStampComplete(_LastCurrentStamp)
    End Sub

    Protected Function _ReadCurrentStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, Optional ByVal CurrentTest As String = "", Optional ByVal Master_DB As String = "", Optional ByVal Slave_DB As String = "", Optional ByVal UseOnlyTable As String = "", Optional ByVal Schedule As String = "") As enumCurrentTest
        Dim iResult As Integer, TempTest As String = String.Empty
        'Dim _LastResult As enumCurrentTest
        If _DefaultCurrentTest = "NONE" Then ' if _DefaultPreviousTest is null, don't search Linecontrol
            '_Status = enumLineControl2004Status.CURRENTTEST_NO_ERROR
            _StatusDescription = ""
            Return enumCurrentTest.CURRENTTEST_NO_ERROR
        End If

        _LastCurrentStamp = enumCurrentTest.CURRENTTEST_NO_ERROR

        If Not _IsInit Then Return _LastCurrentStamp

        If Master_DB <> "" And _LineController.MasterDB_Name <> Master_DB Then
            _LineController.MasterDB_Name = Master_DB
            If Not Init() Then
                _LineController.MasterDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT1, Master_DB)
                Return _LastCurrentStamp
            End If
        End If

        If Slave_DB <> "" And _LineController.SlaveDB_Name <> Slave_DB Then
            _LineController.SlaveDB_Name = Slave_DB
            If Not Init() Then
                _LineController.SlaveDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT2, Slave_DB)
                Return _LastCurrentStamp
            End If
        End If

        If UseOnlyTable <> "" Then
            _LineController.UseOnlyTable = UseOnlyTable
        Else
            _LineController.UseOnlyTable = _DefaultUseOnlyTable
        End If

        If CurrentTest <> "" Then
            TempTest = CurrentTest
        Else
            TempTest = _DefaultCurrentTest
        End If

        'If Schedule.Contains(ScheduleInterface.AlternateScheduleString) Then
        '    If Schedule.Contains(ScheduleInterface.AnalysisString) Then
        '        TempTest = TempTest + _Suffix_Analysis
        '    ElseIf Schedule.Contains(ScheduleInterface.WarrantyString) Then
        '        TempTest = TempTest + _Suffix_Warranty
        '    End If
        'End If
        Dim cSN() As String = sSerialNumber.Split(CChar(";"))
        Dim cArticle() As String = sArticleNo.Split(CChar(";"))
        For i = 0 To cSN.Length - 1
            _LineController.CurrentTest = TempTest
            _LineController.serialNo = cSN(i)
            If cArticle.Length > 1 Then
                _LineController.articleNo = cArticle(i)
            Else
                _LineController.articleNo = sArticleNo
            End If

            iResult = _LineController.ReadCurrentStamp
            If iResult <> 0 Then
                Exit For
            End If
        Next

        Select Case iResult
                Case Is > 0
                    'Entry found - Current test 
                    _LastCurrentStamp = enumCurrentTest.CURRENTTEST_RECORD_FOUND
                    ErrorHandler_ReadCurrentStamp(iResult)
                    Return _LastCurrentStamp
                Case 0
                    'No Error
                    _LastCurrentStamp = enumCurrentTest.CURRENTTEST_NO_ERROR
                    Return _LastCurrentStamp

                Case -1, -5
                    'No Entry found - New part
                    _LastCurrentStamp = enumCurrentTest.CURRENTTEST_NO_ANY_RECORD
                    ErrorHandler_ReadCurrentStamp(iResult)
                    Return _LastCurrentStamp

                Case Else
                    _LastCurrentStamp = enumCurrentTest.CURRENTTEST_ERROR_OCCURED
                    ErrorHandler_ReadCurrentStamp(iResult)
                    Return _LastCurrentStamp
            End Select

    End Function


    Protected Sub ErrorHandler_ReadCurrentStamp(ByVal iErrorNo As Integer)
        Select Case iErrorNo
            Case 0
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT3)
            Case -1
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT4)
            Case -2
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT5)
            Case -3
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT6)
            Case -4
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT7)
            Case -5
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT8)
            Case Else
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_CURRENT9)
        End Select

    End Sub

#End Region

#Region "ReadPreviousStamp"

    '=============================================================================
    'ReadPreviousStamp
    '=============================================================================

    Public Overloads Sub ReadPreviousStamp(ByVal Master_DB As String, ByVal Slave_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, Master_DB, Slave_DB, "", Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStamp(ByVal Master_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, Master_DB, "", "", Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, "", "", "", Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, "", "", "", "", Schedule, pReadPreviousStampCB, Nothing)
    End Sub


    '=============================================================================
    'ReadPreviousStamp UseOnlyTable
    '=============================================================================

    Public Overloads Sub ReadPreviousStampOnlyTable(ByVal Table As String, ByVal Master_DB As String, ByVal Slave_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, Master_DB, Slave_DB, Table, Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStampOnlyTable(ByVal Table As String, ByVal Master_DB As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, Master_DB, "", Table, Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStampOnlyTable(ByVal Table As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal PreviousTest As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, PreviousTest, "", "", Table, Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    Public Overloads Sub ReadPreviousStampOnlyTable(ByVal Table As String, ByVal sSerialNumber As String, ByVal sArticleNo As String, ByVal Schedule As String)
        _ReadPreviousStamp_RUN = True
        pReadPreviousStamp.BeginInvoke(sSerialNumber, sArticleNo, "", "", "", Table, Schedule, pReadPreviousStampCB, Nothing)
    End Sub

    '=============================================================================
    '=============================================================================

    Protected Sub _ReadPreviousStampCB(ByVal Result As IAsyncResult)
        _LastPreviousStamp = pReadPreviousStamp.EndInvoke(Result)
        _ReadPreviousStamp_RUN = False
        RaiseEvent ReadPreviousStampComplete(_LastPreviousStamp)
    End Sub

    Protected Function _ReadPreviousStamp(ByVal sSerialNumber As String, ByVal sArticleNo As String, Optional ByVal PreviousTest As String = "", Optional ByVal Master_DB As String = "", Optional ByVal Slave_DB As String = "", Optional ByVal UseOnlyTable As String = "", Optional ByVal Schedule As String = "") As enumPreviousTest
        Dim iResult As Integer, TempTest As String = String.Empty
        'Dim _LastResult As enumPreviousTest
        If PreviousTest <> "" Then

            TempTest = PreviousTest
        Else
            TempTest = _DefaultPreviousTest
        End If

        If TempTest = "NONE" Then ' if _DefaultPreviousTest is null, don't search Linecontrol
            _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_PASS
            Return _LastPreviousStamp
        End If

        _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_NOTHING

        If Not _IsInit Then Return _LastPreviousStamp

        If Master_DB <> "" And _LineController.MasterDB_Name <> Master_DB Then
            _LineController.MasterDB_Name = Master_DB
            If Not Init() Then
                _LineController.MasterDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS1, Master_DB)
                Return _LastPreviousStamp
            End If
        End If

        If Slave_DB <> "" And _LineController.SlaveDB_Name <> Slave_DB Then
            _LineController.SlaveDB_Name = Slave_DB
            If Not Init() Then
                _LineController.SlaveDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS1, Slave_DB)
                Return _LastPreviousStamp
            End If
        End If

        If UseOnlyTable <> "" Then
            _LineController.UseOnlyTable = UseOnlyTable
        Else
            _LineController.UseOnlyTable = _DefaultUseOnlyTable
        End If


        _PrviousStation = TempTest
        'If Schedule.Contains(ScheduleInterface.AlternateScheduleString) Then
        '	If Schedule.Contains(ScheduleInterface.AnalysisString) Then
        '		TempTest = TempTest + _Suffix_Analysis
        '	ElseIf Schedule.Contains(ScheduleInterface.WarrantyString) Then
        '		TempTest = TempTest + _Suffix_Warranty
        '	End If
        'End If
        Dim cSN() As String = sSerialNumber.Split(CChar(";"))
        Dim cArticle() As String = sArticleNo.Split(CChar(";"))
        For i = 0 To cSN.Length - 1
            _LineController.PreviousTest(0) = TempTest
            _LineController.serialNo = cSN(i)
            If cArticle.Length > 1 Then
                _LineController.articleNo = cArticle(i)
            Else
                _LineController.articleNo = sArticleNo
            End If

            iResult = _LineController.ReadPreviousStamp
            If iResult <> 0 Then
                Exit For
            End If
        Next


        Select Case iResult
            Case 0
                'Entry found - Previous test good
                _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_PASS
                Return _LastPreviousStamp
            Case -2
                'Entry found - Previous test good
                _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_LC_FAIL
                ErrorHandler_ReadPreviousStamp(iResult)
                Return _LastPreviousStamp
            Case -4
                'Entry found - Previous test bad
                _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_FAIL
                ErrorHandler_ReadPreviousStamp(iResult)
                Return _LastPreviousStamp
            Case -1, -6
                'No Entry found - New part
                _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_FAIL
                ErrorHandler_ReadPreviousStamp(iResult)
                Return _LastPreviousStamp

            Case Else
                _LastPreviousStamp = enumPreviousTest.PREVIOUSTEST_NOTHING
                ErrorHandler_ReadPreviousStamp(iResult)

                Return _LastPreviousStamp
        End Select
    End Function


    Protected Sub ErrorHandler_ReadPreviousStamp(ByVal iErrorNo As Integer)
        Select Case iErrorNo
            Case 0
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS3)
            Case -1
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS4)
            Case -2
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS5)
            Case -3
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS6)
            Case -4
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS7)
            Case -5
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS8)
            Case -6
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS9)
            Case -69
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS10)
            Case Else
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_PREVIOUS11, iErrorNo.ToString)
        End Select

    End Sub

#End Region

#Region "WriteCurrentStamp"

    '=============================================================================
    'WriteCurrentStamp
    '=============================================================================


    Public Overloads Sub WriteCurrentStamp(ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, "", "", "", "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub


    Public Overloads Sub WriteCurrentStamp(ByVal Master_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, "", "", "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, SlaveDB_Name, "", "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStamp(ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, "", "", CurrentTest, "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStamp(ByVal Master_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, "", CurrentTest, "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, SlaveDB_Name, CurrentTest, "", Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    '=============================================================================
    'WriteCurrentStamp UseOnlyTable
    '=============================================================================

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, "", "", "", Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, "", "", Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, SlaveDB_Name, "", Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, "", "", CurrentTest, Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, "", CurrentTest, Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteCurrentStampOnlyTable(ByVal Table As String, ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteCurrentStamp_RUN = True
        pWriteCurrentStamp.BeginInvoke(SerialNo, ArticleNo, TestResult, Master_DB_Name, SlaveDB_Name, CurrentTest, Table, Schedule, pWriteCurrentStampCB, Nothing)
    End Sub

    '=============================================================================
    '=============================================================================

    Protected Sub _WriteCurrentStampCB(ByVal Result As IAsyncResult)
        _LastWriteResult = pWriteCurrentStamp.EndInvoke(Result)
        _WriteCurrentStamp_RUN = False
        RaiseEvent WriteCurrentStampComplete(_LastWriteResult)
    End Sub


    Protected Function _WriteCurrentStamp(ByVal SerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, Optional ByVal Master_DB As String = "", Optional ByVal Slave_DB As String = "", Optional ByVal _CurrentTest As String = "", Optional ByVal UseOnlyTable As String = "", Optional ByVal Schedule As String = "") As Boolean
        Dim iResult As Integer, xWithChildren As Boolean, TempTest As String = String.Empty

        If Not _IsInit Then Return True
        If _CurrentTest = "" Then
            TempTest = _DefaultCurrentTest
        Else
            TempTest = _CurrentTest
        End If
        If _CurrentTest = "NONE" Then ' if _DefaultPreviousTest is null, don't search Linecontrol
            _Status = enumLineControl2004Status.Initialized
            _StatusDescription = ""
            Return True
        End If

        If Master_DB <> "" And _LineController.MasterDB_Name <> Master_DB Then
            _LineController.MasterDB_Name = Master_DB
            If Not Init() Then
                _LineController.MasterDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE1, Master_DB)
                Return False
            End If
        End If

        If Slave_DB <> "" And _LineController.SlaveDB_Name <> Slave_DB Then
            _LineController.SlaveDB_Name = Slave_DB
            If Not Init() Then
                _LineController.SlaveDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE2, Slave_DB)
                Return False
            End If
        End If

        If UseOnlyTable <> "" Then
            _LineController.UseOnlyTable = UseOnlyTable
        Else
            _LineController.UseOnlyTable = _DefaultUseOnlyTable
        End If
        Dim cSN() As String = SerialNo.Split(CChar(";"))
        Dim cArticle() As String = ArticleNo.Split(CChar(";"))
        For i = 0 To cSN.Length - 1

            xWithChildren = (_Parameters.Count <> 0)
            If xWithChildren Then
                For Each ChildElement In _Parameters.Values
                    _LineController.articleNo = ChildElement.Article
                    _LineController.serialNo = ChildElement.SN
                    _LineController.PrepareForChild()
                Next
            End If
            _LineController.TraceID = _TraceId
            If cArticle.Length > 1 Then
                _LineController.articleNo = cArticle(i)
            Else
                _LineController.articleNo = ArticleNo
            End If

            _LineController.serialNo = cSN(i)



            _CurrentStation = TempTest
            '  _LineController.AdditionalText(0) = FailStr
            '  _LineController.AdditionalText(3) = Schedule

            'If Schedule.Contains(ScheduleInterface.AnalysisString) Then
            '	TempTest = TempTest + _Suffix_Analysis
            'ElseIf Schedule.Contains(ScheduleInterface.WarrantyString) Then
            '	TempTest = TempTest + _Suffix_Warranty
            'End If

            _LineController.CurrentTest = TempTest

            _LineController.TestResult = TestResult
            _LineController.Timestamp = Format(Now, "yyyy-MM-dd HH:mm:ss")

            iResult = _LineController.WriteCurrentStamp
            If xWithChildren And iResult = 0 Then
                iResult = _LineController.MarryAllChildren()
            End If
            If iResult <> 0 Then
                Exit For
            End If
        Next

        _Parameters.Clear()

        If iResult = -3 Then
            _Status = enumLineControl2004Status.FailWhileWriteLC
            ErrorHandler_WriteCurrentStamp(iResult)
            Return False
        ElseIf iResult <> 0 Then
            _Status = enumLineControl2004Status.FailWhileWriteCurrentStamp
            ErrorHandler_WriteCurrentStamp(iResult)
            Return False
        Else
            _Status = enumLineControl2004Status.Initialized
            _StatusDescription = ""
            Return True
        End If

    End Function


    Protected Sub ErrorHandler_WriteCurrentStamp(ByVal iErrorNo As Integer)
        Select Case iErrorNo
            Case 0
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE3)
            Case -1
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE4)
            Case -2
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE5)
            Case -3
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE6)
            Case -4
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE7)
            Case Else
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE8)
        End Select

    End Sub

#End Region

#Region "WriteReTestCurrentStamp"
    '=============================================================================
    'WriteReTestCurrentStamp
    '=============================================================================


    Public Overloads Sub WriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, "", "", "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String, ByVal WithChildren As Boolean, ByVal AdditionalTextPositon As Integer)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, AdditionalTextPositon, "", "", "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String, ByVal WithChildren As Boolean)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, 0, "", "", "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, Master_DB_Name, "", "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, Master_DB_Name, Slave_DB_Name, "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String, ByVal WithChildren As Boolean)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, 0, Master_DB_Name, Slave_DB_Name, "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal Schedule As String, ByVal WithChildren As Boolean, ByVal AdditionalTextPositon As Integer)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, AdditionalTextPositon, Master_DB_Name, Slave_DB_Name, "", "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, "", "", CurrentTest, "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, Master_DB_Name, "", CurrentTest, "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, True, 0, Master_DB_Name, Slave_DB_Name, CurrentTest, "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String, ByVal WithChildren As Boolean)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, 0, Master_DB_Name, Slave_DB_Name, CurrentTest, "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Public Overloads Sub WriteReTestCurrentStamp(ByVal Master_DB_Name As String, ByVal Slave_DB_Name As String, ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, ByVal CurrentTest As String, ByVal Schedule As String, ByVal WithChildren As Boolean, ByVal AdditionalTextPositon As Integer)
        _WriteReTestCurrentStamp_RUN = True
        pWriteReTestCurrentStamp.BeginInvoke(OldSerialNo, NewSerialNo, ArticleNo, TestResult, WithChildren, AdditionalTextPositon, Master_DB_Name, Slave_DB_Name, CurrentTest, "", Schedule, pWriteReTestCurrentStampCB, Nothing)
    End Sub

    Protected Sub _WriteReTestCurrentStampCB(ByVal Result As IAsyncResult)
        _LastWriteResult = pWriteReTestCurrentStamp.EndInvoke(Result)
        _WriteReTestCurrentStamp_RUN = False
        RaiseEvent WriteReTestCurrentStampComplete(_LastWriteResult)
    End Sub

    Protected Function _WriteReTestCurrentStamp(ByVal OldSerialNo As String, ByVal NewSerialNo As String, ByVal ArticleNo As String, ByVal TestResult As Boolean, Optional ByVal WithChildren As Boolean = True, Optional ByVal AdditionalTextPositon As Integer = 0, Optional ByVal Master_DB As String = "", Optional ByVal Slave_DB As String = "", Optional ByVal _CurrentTest As String = "", Optional ByVal UseOnlyTable As String = "", Optional ByVal Schedule As String = "") As Boolean
        Dim iResult As Integer, xWithChildren As Boolean, TempTest As String = String.Empty
        Dim iRecodeResult As Integer = 0
        Dim childs As Array = New String(,) {}
        If Not _IsInit Then Return True

        If _DefaultCurrentTest = "NONE" Then ' if _DefaultPreviousTest is null, don't search Linecontrol
            _Status = enumLineControl2004Status.Initialized
            _StatusDescription = ""
            Return True
        End If

        If Master_DB <> "" And _LineController.MasterDB_Name <> Master_DB Then
            _LineController.MasterDB_Name = Master_DB
            If Not Init() Then
                _LineController.MasterDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE1, Master_DB)
                Return False
            End If
        End If

        If Slave_DB <> "" And _LineController.SlaveDB_Name <> Slave_DB Then
            _LineController.SlaveDB_Name = Slave_DB
            If Not Init() Then
                _LineController.SlaveDB_Name = ""
                _StatusDescription = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_LC_WRITE2, Slave_DB)
                Return False
            End If
        End If

        If UseOnlyTable <> "" Then
            _LineController.UseOnlyTable = UseOnlyTable
        Else
            _LineController.UseOnlyTable = _DefaultUseOnlyTable
        End If
        '查询是否有记录
        _LineController.TraceID = _TraceId
        _LineController.articleNo = ArticleNo
        _LineController.serialNo = OldSerialNo
        If _CurrentTest = "" Then
            TempTest = _DefaultCurrentTest
        Else
            TempTest = _CurrentTest
        End If
        _LineController.PreviousTest(0) = TempTest
        _LineController.CurrentTest = TempTest
        iResult = _LineController.ReadPreviousStamp
        If iResult <> 0 Then
            _Status = enumLineControl2004Status.FailWhileReadPreviousStamp
            ErrorHandler_WriteCurrentStamp(iResult)
            Return False
        End If

        '添加Old Children SN
        iRecodeResult = _LineController.GetChildren(ArticleNo, OldSerialNo, childs)
        If iRecodeResult <0 Or (WithChildren And iRecodeResult = 0) Then
            _Status = enumLineControl2004Status.FailWhileWriteResetCurrentStamp
            ErrorHandler_WriteCurrentStamp(iResult)
            Return False
        End If
        Dim _childs(,) As String = CType(childs, String(,))
        For i As Integer = 0 To iRecodeResult - 1
            If _childs.GetUpperBound(i) >= 2 Then
                If IsNothing(_childs(i, 0)) Or IsNothing(_childs(i, 1)) Then Continue For
                _LineController.articleNo = _childs(i, 0)
                _LineController.serialNo = _childs(i, 1)
                _LineController.PrepareForChild()
            End If
        Next

        '根据需要选择添加
        xWithChildren = (_Parameters.Count <> 0)
        If xWithChildren Then
            For Each ChildElement In _Parameters.Values
                _LineController.articleNo = ChildElement.Article
                _LineController.serialNo = ChildElement.SN
                _LineController.PrepareForChild()
            Next
        End If

        _LineController.TraceID = _TraceId
        _LineController.articleNo = ArticleNo
        _LineController.serialNo = NewSerialNo
        _CurrentStation = TempTest
        _LineController.CurrentTest = TempTest
        _LineController.TestResult = TestResult
        _LineController.Timestamp = Format(Now, "yyyy-MM-dd HH:mm:ss")
        AdditionalInfos_Clear()
        _LineController.AdditionalText(AdditionalTextPositon) = OldSerialNo

        iResult = _LineController.WriteCurrentStamp
        If (xWithChildren Or iRecodeResult > 0) And iResult = 0 Then
            iResult = _LineController.MarryAllChildren()
        End If
        _Parameters.Clear()

        If iResult <> 0 Then
            _Status = enumLineControl2004Status.FailWhileWriteCurrentStamp
            ErrorHandler_WriteCurrentStamp(iResult)
            Return False
        Else
            _Status = enumLineControl2004Status.Initialized
            _StatusDescription = ""
            Return True
        End If

    End Function
#End Region
End Class

