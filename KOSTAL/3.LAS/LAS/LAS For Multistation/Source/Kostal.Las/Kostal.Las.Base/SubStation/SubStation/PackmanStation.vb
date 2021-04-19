Imports System.Windows.Forms

Public Class PackmanStation
    Inherits StationTypeBase

    Protected _UIStation As PackmanUI
    Protected _packman As IPackmanDevice
    Protected Const Name As String = "PackmanStation"
    Protected _mainForm As IMainForm


    '**************For packman start*****************
    'HuConfig
    Private _huConfig As EOLPackman.HuXmlConfig
    Private _huConfigFile As String = ""

    'DB
    Private _db As EOLPackman.DbConnect
    Private _sever As String = ""
    Private _port As String = "3306"
    Private _user As String = ""
    Private _password As String = ""
    Private _dbName As String = "eolpackman"
    Private _tableName As String = "scatteredpacked"

    'LC
    Private _lcConfig As EOLPackman.ReadLCFile
    Private _lcConfigFileName As String = ""
    Private _enable As Boolean = False
    'Main class
    Private WithEvents _eolPackmanDll As EOLPackman.EolPackman
    Private _currentArticleNo As String = ""
    Private _lastArticleNo As String = ""
    '**************For packman end*****************

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _eolPackmanDll = New EOLPackman.EolPackman
            _UIStation = New PackmanUI(_eolPackmanDll.EOLPackmanForm.PanelEOLPackman)
            _UI = _UIStation

            _CheckTrigInfo = CheckTrigInfo
            _mainForm = _Devices("mMainForm")
            'InitPackam(_SubStationCfg.Config, _Stations(_SubStationCfg.MainDevice).SubStationCfg.Config)
            '_Logger.Logger(_i, _Messager, "Packman init Successful", "Packman.Init")

            '_Messager.Construct(_UIStation.Msg)
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub

    '初始化List
    Public Overrides Function Init() As Boolean
        Try
            _i.StepInputNumber = _i.Address_Origin
            _i.Address_Pass = 1000
            _i.Address_Fail = 2000
            AddHandler _AppArticle.IDChange, AddressOf ArticleChanged
            AddHandler _mainForm.MainForm_IamClosing, AddressOf FormClosed
            ReLoadLanguage()
            Return True
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
        '_Language.ReadControlText(_UIStation.Panel)
        Return True
    End Function

    Public Overrides Sub Run()
        Try
            If IsNothing(_i) Then Exit Sub

            _FirstPulse = Not _FirstFlag
            _FirstFlag = True

            _ManualOffPulse = Not _ManualMode And _ManualFlag
            _ManualFlag = _ManualMode

            '==============================================================================
            'StepHeader
            '==============================================================================
            If Not CheckStepLine() Then Return
            If Not BeforeLine() Then Return
            If Not UpdateMsg(PackmanStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not Me.InitPackam(_SubStationCfg.Config, _Stations(_SubStationCfg.MainDevice).SubStationCfg.Config) Then
                            _Logger.ThrowerNoStation(_i, _Messager, "Packman init error check HuConfig.xml or LC config file.", "Packman.Init")
                        Else
                            _Logger.Logger(_i, _Messager, "Packman init Successful", "Packman.Init")
                            '_Devices.Add(_SubStationCfg.Name, _packman)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, "Packman Disabled", "Packman.Init")
                        End If
                    End If

                Case -98 '
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        ArticleChanged(_AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data, enumChangeType.Auto)
                        _i.StepInputNumber = _i.Address_Home
                    End If


                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If
                    If _ReadStructRequestAction.bulDoPositiveAction Or _ReadStructRequestAction.bulDoNegativeAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCOUNTER))
                        _StationMode = 1 'Auto Mode
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal.Add("_ReadStructRequestAction", _ReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ReadStructRequestAction") Then _TrigSignal("_ReadStructRequestAction") = _ReadStructRequestAction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructRequestAction.bulDoPositiveAction Or _ManualReadStructRequestAction.bulDoNegativeAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STARTCOUNTER))
                        _StationMode = 2 ' Manual Auto Mode
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal.Add("_ManualReadStructRequestAction", _ManualReadStructRequestAction)
                        If _TrigSignal.ContainsKey("_ManualReadStructRequestAction") Then _TrigSignal("_ManualReadStructRequestAction") = _ManualReadStructRequestAction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                Case 1  '判断PLC传递信息
                    CheckStructRequestActionPLCInfo()

                Case 2
                    If _ReadStructRequestAction.bulDoPositiveAction Then
                        IncreaseCount(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data)
                    End If

                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    _i.StepInputNumber = _i.Address_Pass

                Case 1000
                    '回写PLC
                    UpdateStructResponseActionPassStep1()

                Case 1001

                    UpdateStructDeviceInteractionPassStep2()

                Case 2000
                    '回写PLC
                    UpdateStructResponseActionFailStep1()

                Case 2001
                    UpdateStructDeviceInteractionFailStep2()

            End Select
            '==============================EndLine=========================================
            If Not AfterLine() Then Return
            '==============================================================================
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:" + _i.StepOutputNumber.ToString + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub

    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))

        _i = Nothing
        AppSettings = Nothing
        _Language = Nothing
        _Logger = Nothing
        _LocalArticle = Nothing
        If _SubStationCfg.Enable Then
            _packman.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

#Region "Packman"

    Public ReadOnly Property PackmanUI As Panel
        Get
            Return _eolPackmanDll.EOLPackmanForm.PanelEOLPackman
        End Get
    End Property

    Public Function InitPackam(huConfigFile As String, lcConfigFile As String) As Boolean
        Dim res As Boolean = True
        _lcConfigFileName = lcConfigFile
        _huConfigFile = huConfigFile

        Try
            _db = New EOLPackman.DbConnect()
            _huConfig = New EOLPackman.HuXmlConfig()
            _lcConfig = New EOLPackman.ReadLCFile()

            If _huConfigFile <> "" Then
                _huConfigFile = IO.Path.Combine(AppSettings.ConfigFolder, _huConfigFile)
            Else
                _Logger.Logger(_i, _Messager, "HuConfigFile is empty, Please set it!")
                res = False
            End If

            If _lcConfigFileName <> "" Then
                _lcConfigFileName = IO.Path.Combine(AppSettings.LineControlFolder, _lcConfigFileName)
                If _lcConfig.ReadLCFile(_lcConfigFileName) Then
                    If Not _db.InitDatabase(_lcConfig.Sever, _port, _lcConfig.User, _lcConfig.Password, _lcConfig.DbName, _tableName) Then
                        _Logger.Logger(_i, _Messager, "Init data base error, check config is right or sever is accessable.")
                        res = False
                    End If
                Else
                    _Logger.Logger(_i, _Messager, "Read LC file is error.")
                    res = False
                End If
            Else
                If Not _db.InitDatabase(_sever, _port, _user, _password, _dbName, _tableName) Then
                    _Logger.Logger(_i, _Messager, "Init data base error, check config is right.")
                    res = False
                End If
            End If

            If Not _huConfig.Init(_huConfigFile) Then
                _Logger.Logger(_i, _Messager, "Read Hu config file error.")
                res = False
            End If

            '_eolPackmanDll = New EOLPackman.EolPackman(_db, _huConfig) With {.LcTableName = _lcConfig.TableName, .MachineId = AppSettings.MachineIdentifier.TraceId}
            '_UIStation = New PackmanUI(_eolPackmanDll.EOLPackmanForm.PanelEOLPackman)
            '_UI = _UIStation

            _eolPackmanDll.DB = _db
            _eolPackmanDll.HuConfig = _huConfig
            _eolPackmanDll.LcTableName = _lcConfig.TableName
            _eolPackmanDll.MachineId = AppSettings.MachineIdentifier.TraceId

        Catch ex As Exception
            _Logger.Logger(_i, _Messager, "StartUp EOLpackman error, Info:" + ex.Message)
            res = False
        End Try

        If res Then
            _Logger.Logger(_i, _Messager, "EOLPackman initialize successful.")
        Else
            _Logger.Logger(_i, _Messager, "EOLPackman initialize fail.")
        End If

        Return res

    End Function

    Public Sub ArticleChanged(ByVal articleNo As String, ByVal ChangeType As enumChangeType)
        Try
            _currentArticleNo = articleNo
            _eolPackmanDll.CurrentArticleNo = _currentArticleNo

            _eolPackmanDll.EOLPackmanForm.CurrentArticleNo = _currentArticleNo

            If _lastArticleNo <> _currentArticleNo Then
                _eolPackmanDll.SaveData("", _lastArticleNo, _eolPackmanDll.PackagingNum)
                _lastArticleNo = _currentArticleNo

                _eolPackmanDll.PackedBoxNum = 0
                _eolPackmanDll.PackagingNum = 0

                _eolPackmanDll.GetPackageCount()
                _eolPackmanDll.SetZeroboxNum()

                'Update EOLPackmanpanel
                _eolPackmanDll.EOLPackmanForm.BoxCount = _eolPackmanDll.PackedBoxNum
                _eolPackmanDll.EOLPackmanForm.PartCount = _eolPackmanDll.PackagingNum

                If _eolPackmanDll.EOLPackmanForm.InvokeRequired Then
                    _eolPackmanDll.EOLPackmanForm.Invoke(Sub()
                                                             _eolPackmanDll.EOLPackmanForm.SetControls("")
                                                         End Sub)
                Else
                    _eolPackmanDll.EOLPackmanForm.SetControls("")
                End If
            End If

        Catch ex As Exception
            _Logger.Logger(_i, _Messager, "Change article error, Info:" + ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Incerase count and check HU
    ''' </summary>
    ''' <param name="sn">Show sn in packman station</param>
    ''' <returns></returns>
    Private Function IncreaseCount(sn As String) As Boolean

        Dim res As Boolean = True
        Try

            If _eolPackmanDll.EOLPackmanForm.InvokeRequired Then
                _eolPackmanDll.EOLPackmanForm.Invoke(Sub()
                                                         _eolPackmanDll.IncreaseCount(sn)
                                                     End Sub)
            Else
                _eolPackmanDll.IncreaseCount(sn)
            End If

            If _eolPackmanDll.PackagingNum = _eolPackmanDll.ExpectNum Then
                If _eolPackmanDll.CheckHu() Then
                    If _lcConfigFileName <> "" Then
                        If Not _eolPackmanDll.WriteHuNoToLcTable() Then
                            _Logger.Logger(_i, _Messager, "Write HuNo to LC sever fail !")
                            res = False
                        End If
                    End If
                Else
                    res = False
                End If
            End If

        Catch ex As Exception
            _Logger.Logger(_i, _Messager, "TestCompleted check HU error, Info:" + ex.Message)
        End Try

        Return res

    End Function

    Public Function SaveData() As Boolean
        Return _eolPackmanDll.SaveData("", _currentArticleNo, _eolPackmanDll.PackagingNum)
    End Function

    Private Sub FormClosed()
        If _SubStationCfg.Enable Then
            SaveData()
        End If
    End Sub

#End Region

End Class
