Imports System.Windows.Forms

Public Structure ProductionCount
    Public TotalCount As Integer
    Public PassCount As Integer
    Public FailCount As Integer
End Structure
Public Class MESStation
    Inherits StationTypeBase
    Protected WithEvents _Scanner As IScanner
    Protected _InterArticleResult As Boolean
    Protected _InterResult As String
    Protected _InterMesResult As Boolean
    Protected _InterCount As Integer
    Protected _InterSave As Boolean
    Protected _InterLinecontrolenable As Boolean
    Protected _InterConfigName As String
    Protected _InterIndex As String
    Protected _InterArticle As String
    Protected _InterList As New Dictionary(Of String, ScapLocation)
    Protected _InterWebServiceResult As String
    Protected _ScanResult As String
    Protected _LK As New Barcode_LK
    Protected _DataStore As DataStore
    Protected _ArticleStore As ArticleStore
    Protected _SMTStore As SMTStore
    Protected _LinecontrolStore As LinecontrolStore
    Protected _ArticleCount As ArticleCount
    Protected _isCallBackRunning As Boolean
    Protected _StartCallBack As Boolean
    Protected _WebService As WebService
    Protected _WebService2 As WebService2
    Protected _Devices As Dictionary(Of String, Object)
    Protected _Read As Boolean
    Protected _NeadRead As Boolean
    Protected _WriteTrue As Boolean
    Protected _WriteFalse As Boolean
    Protected _TimeDelay As TimeDelay
    Protected _ToolStripStatusLabel As ToolStripStatusLabel
    Protected _Shift As Shift
    Protected _CleanShift As Shift
    Protected _ProductionCount As New ProductionCount
    Protected _CurrentShift As Integer
    Protected _BarcodeMsg As String
    Protected _Language As Language
    Protected _Lc As LineControl
    Protected _LastArticle As String
    Protected _BitLastArticle As String
    Protected _LcResult As Boolean
    Protected _LcMessage As String
    Protected _listScrapSN As New List(Of String)
    Protected _ia As Integer = 0
    Protected _isCallBackResult As Boolean
    Protected _strCallBackMsg As String
    Protected Delegate Function dcompleteBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
    Protected pcompleteBoard As New dcompleteBoard(AddressOf _completeBoard)
    Protected pcompleteBoardCB As AsyncCallback = New AsyncCallback(AddressOf _completeBoardCB)
   
    Protected Delegate Function dstartBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
    Protected pstartBoard As New dstartBoard(AddressOf _startBoard)
    Protected pstartBoardCB As AsyncCallback = New AsyncCallback(AddressOf _startBoardCB)
    Protected _BitSql As BitStore
    Protected _PlcSql As PLCStore
    Protected _strBitNumber As String = String.Empty
    Public _strPLcAddress As String = String.Empty
    Protected cAlarm As clsAlarm
    Public WritePLCAddress As Boolean = False
    Public Property Read As Boolean
        Set(ByVal value As Boolean)
            _Read = value
        End Set
        Get
            Return _Read
        End Get
    End Property

    Public ReadOnly Property NeadRead As Boolean
        Get
            Return _NeadRead
        End Get
    End Property

    Public Property WriteTrue As Boolean
        Set(ByVal value As Boolean)
            _WriteTrue = value
        End Set
        Get
            Return _WriteTrue
        End Get
    End Property

    Public Property WriteFalse As Boolean
        Set(ByVal value As Boolean)
            _WriteFalse = value
        End Set
        Get
            Return _WriteFalse
        End Get
    End Property

    Public Sub New(ByVal StationCfg As StationCfg, ByVal Settings As Settings, ByVal UI As UI, ByVal Devices As Dictionary(Of String, Object), ByVal ToolStripStatusLabel As ToolStripStatusLabel)
        MyBase.New(StationCfg, Settings, UI)
        Try
            _Scanner = New Scanner
            _TimeDelay = New TimeDelay
            _Devices = Devices
            _Messager.Construct(UI.ListBox_Msg)
            _ToolStripStatusLabel = ToolStripStatusLabel
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:New" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub


    Public Overrides Function Init() As Boolean
        _i.Address_Debug = 1000
        _i.StepInputNumber = _i.Address_Origin
        _Lc = New LineControl
        If _Settings.WebServiceCfg.Enable Then
            _WebService = CType(_Devices(_i.Name + WebService.Name), WebService)
            _WebService2 = CType(_Devices(_i.Name + WebService2.Name), WebService2)
        End If
        _DataStore = CType(_Devices(DataStore.Name), DataStore)
        _ArticleStore = CType(_Devices(ArticleStore.Name), ArticleStore)
        _SMTStore = CType(_Devices(SMTStore.Name), SMTStore)
        _ArticleCount = CType(_Devices(ArticleCount.sName), ArticleCount)
        _LinecontrolStore = CType(_Devices(LinecontrolStore.Name), LinecontrolStore)
        _Language = CType(_Devices(Language.Name), Language)
        cAlarm = CType(_Devices(clsAlarm.Name), clsAlarm)
        _Shift = New Shift(_Settings.ConfigFolder + "lkshift.ini")
        _CleanShift = New Shift(_Settings.ConfigFolder + "Cleanshift.ini")
        _CurrentShift = _Shift.GetCurrentShift
        _BitSql = New BitStore()
        _BitSql.Init(_Settings)

        _PlcSql = New PLCStore()
        _PlcSql.Init(_Settings)

        InitProductionData()
        AddHandler _Scanner.DataReceived, AddressOf DataReceived
        AddHandler _Shift.ShiftChange, AddressOf _Shift_ShiftChange
        AddHandler _CleanShift.ShiftChange, AddressOf _Shift_ShiftChange2

        AddHandler _UI.Button_Clean.Click, AddressOf Button_Clean_Click
        AddHandler _UI.Button_Abort.Click, AddressOf Button_Abort_Click
        Return True
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
        SetStepText(_Language.LanguageElement.GetTextLine(_i, _i.StepOutputNumber.ToString))
        Return True
    End Function

    Public Overrides Sub Run()
        Try
            If IsNothing(_i) Then Exit Sub
            _FirstPulse = Not _FirstFlag
            _FirstFlag = True

            _ManualOffPulse = Not _ManualMode And _ManualFlag
            _ManualFlag = _ManualMode
            _i.Toggle = _i.StepOutputNumber <> _i.StepInputNumber
            _i.StepOutputNumber = _i.StepInputNumber

            If _i.Toggle Then SetStepText(_Language.LanguageElement.GetTextLine(_i, _i.StepOutputNumber.ToString))
            _i.StepTextLine = "Step:" + _i.StepOutputNumber.ToString
            _ToolStripStatusLabel.Text = _i.Name + " " + _i.StepTextLine
            If _BarcodeMsg <> "" Then
                Throw New Exception("Barcode Receive. Result:False Message:" + _BarcodeMsg)
            End If

            Select Case _i.StepOutputNumber
                Case -100  'Init
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "System Initial")
                    _InterCount = 0
                    _InterResult = ""
                    _ScanResult = ""
                    _InterArticleResult = False
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Initial Scanner")
                    If _StationCfg.DeviceConfig.Enable Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _i.Toggle Then SetStepText(_Language.LanguageElement.GetTextLine(_i, "1001"))
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Scanner is Disable")
                    End If

                Case -98
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Init Scanner")
                    If _Scanner.Init(_StationCfg.DeviceConfig, _Settings) Then
                        _Logger.Logger(_i, _Messager, "Init Scanner Pass")
                    Else
                        _Logger.ThrowerNoStation(_i, enmLogType.ErrorLog, False, _Messager, "Init Scanner Fail. Message:" + _Scanner.StatusDescription)
                    End If
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -97
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Initial LineControl")
                    If _Lc.Init(_Settings.LineControlFolder + _Settings.LineControlCfg.strDefaultLR, _Settings.LineControlCfg.strDefalutSection, _Settings.LineControlCfg.strTraceId) Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _Logger.ThrowerNoStation(_i, enmLogType.ErrorLog, False, _Messager, "Init LineControl Fail. Message:" + _Lc.StatusDescription)
                    End If

                Case -96
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Start Receive Message")
                    _Scanner.ContinueScan()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -95
                    '   _ScanResult = "12345679/SNYA016QEQ26530"
                    _i.StepInputNumber = _i.Address_Home

                Case 0
                    If _i.Toggle Then _UI.Button_Abort.Enabled = False
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Home Position")
                    If _i.Toggle Then _UI.Label_Msg.BackColor = Drawing.Color.LightGray
                    If _CleanShift.GetCurrentShift <> cAlarm.strLastTime Then
                        cAlarm.strLastTime = _CleanShift.GetCurrentShift
                        cAlarm.Save()
                        Dim pAlarm As New AlarmMessage
                        pAlarm.Init(_Devices)
                        pAlarm.Message = _Language.LanguageElement.GetTextLine(_i, "1030")
                        pAlarm.ShowTime = CInt(_Settings.ShowTime) * 60
                        pAlarm.ShowDialog()

                    End If


                    isHome = True
                    If _ScanResult <> "" Then
                        _InterResult = _ScanResult
                        _ScanResult = ""
                        _InterCount = 0
                        _InterConfigName = ""
                        _InterIndex = ""
                        _InterSave = False
                        _InterMesResult = False
                        _isCallBackResult = False
                        _isCallBackRunning = False
                        _strCallBackMsg = False
                        _LcResult = False
                        _InterLinecontrolenable = False
                        _listScrapSN.Clear()
                        _LcMessage = ""
                        _InterArticleResult = False
                        _UI.Label_Msg.Text = ""
                        _UI.TextBox_ErrorMsg.Text = ""
                        _UI.TextBox_Scap.Text = ""
                        _UI.TextBox_SN.Text = ""
                        _UI.Button_Abort.Enabled = True
                        SetSNText(_InterResult)
                        _Logger.Logger(_i, _Messager, "Scan Result:" + _InterResult)
                        isHome = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 1
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check Scan Result Length")
                    If _InterResult.Length = _StationCfg.BarLength Then
                        _Logger.Logger(_i, _Messager, "Scan Result:" + _InterResult)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    ElseIf _InterResult.Length = _StationCfg.SMTBarLength Then
                        _Logger.Logger(_i, _Messager, "Scan SMT Result:" + _InterResult)
                        _i.StepInputNumber = 100
                    Else
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1002"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Scan Result Length Fail. Scan Length :" + _InterResult.Length.ToString)
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 2
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Analyze Scan Result")
                    _LK.SetNewLine("/P" + _InterResult)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check SN and Article Length")
                    If _LK.LkNumber.Length <> 8 Then
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1003"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Article Length Fail. Scan Length :" + _LK.LkNumber.Length.ToString)
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If
                    If _LK.SerialNumber.Length <> 13 Then
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1004"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "SerialNumber Length Fail. Scan Length :" + _LK.SerialNumber.Length.ToString)
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If
                    If _LK.SerialNumber.Substring(12) <> "0" Then
                        _LK.SerialNumber = _LK.SerialNumber.Substring(0, 12) & "0"
                    End If
                    _Logger.Logger(_i, _Messager, "Article：" + _LK.LkNumber + " SerialNumber:" + _LK.SerialNumber)
                    If _BitLastArticle <> _LK.LkNumber Then
                        _i.StepInputNumber = 140
                    Else
                        _i.StepInputNumber = 150
                    End If



                Case 4

                    If _LK.LkNumber <> cAlarm.strLastArticle Then
                        cAlarm.strLastArticle = _LK.LkNumber
                        cAlarm.Save2()
                        Dim pAlarm As New AlarmMessage
                        pAlarm.Init(_Devices)
                        pAlarm.Message = _Language.LanguageElement.GetTextLine(_i, "1040")
                        pAlarm.ShowTime = 10
                        pAlarm.ShowDialog()
                    End If

                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _ArticleStore not Run")
                    If Not _ArticleStore.isRun Then
                        _ArticleStore.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 5
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check if need MES")
                    _InterArticleResult = _ArticleStore.CheckArticle(_LK.LkNumber)
                    _ArticleStore.isRun = False
                    If _InterArticleResult Or Not _Settings.WebServiceCfg.Enable Then
                        _i.StepInputNumber = 17
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 6
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _DataStore  and  _ArticleCount not Run")
                    If Not _DataStore.isRun And Not _ArticleCount.isRun Then
                        _DataStore.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 7
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Get Article Count")
                    If _DataStore.GetCount(_LK.LkNumber, _InterCount) Then
                        _DataStore.isRun = False
                        _Logger.Logger(_i, _Messager, "Article：" + _LK.LkNumber + " amountOfBoards:" + _InterCount.ToString)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _DataStore.isRun = False
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1005"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Get Article Count Fail. Error Message :" + _DataStore.StatusDescription)
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 8
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check Article Count")
                    If _InterCount > 0 Then
                        _InterSave = False
                        If _InterCount = 1 Then
                            _i.StepInputNumber = 120
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 4
                        End If

                    Else
                        _InterSave = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 9
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _ArticleCount not Run")
                    If Not _ArticleCount.isRun Then
                        _ArticleCount.isRun = True
                        _ArticleCount.DisplayEnd = False
                        _ArticleCount.isDisplay = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = 4
                    End If

                Case 10
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting Input Artice Count")
                    If _ArticleCount.DisplayEnd Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check Input Count")
                    If _ArticleCount.Count > 0 Then
                        _InterCount = _ArticleCount.Count
                        _ArticleCount.Count = 0
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Input Count:" + _InterCount.ToString)
                        If _InterCount = 1 Then
                            _i.StepInputNumber = 120
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                    Else
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1006"))
                        If _InterSave Then _ArticleCount.isRun = False
                        If _i.Toggle Then _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Invalid Article Count:" + _InterCount.ToString)
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 12
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting WebService not Run")
                    If Not _WebService.isRun Then
                        _WebService.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 13
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService.ValidateMilling")
                    If _WebService.ValidateMilling(_LK.SerialNumber, _InterCount, _InterWebServiceResult, _InterList) Then
                        _InterMesResult = False
                        _WebService.isRun = False
                        If _Settings.WebServiceCfg.PassiveMode Then If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.ValidateMilling Response:" + _InterWebServiceResult)
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService.ValidateMilling Pass")
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _Settings.WebServiceCfg.PassiveMode Then
                            _InterMesResult = True
                            _WebService.isRun = False
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.ValidateMilling Response:" + _InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService.ValidateMilling Pass")
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InterMesResult = True
                            AddFailProductionData()
                            _WebService.isRun = False
                            If _InterSave Then _ArticleCount.isRun = False
                            SetFailText(_InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "WebService.ValidateMilling Fail. Message:" + _InterWebServiceResult)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 14
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Display Scrap Location")
                    For Each element As ScapLocation In _InterList.Values
                        If element.Result <> "GOOD" Then
                            SetScapText(element.Location)
                            _listScrapSN.Add(element.Location)
                        End If

                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Location:" + element.Location + " Result:" + element.Result)
                    Next
                    If _InterSave Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 3
                    End If

                Case 15
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _DataStore not Run")
                    If Not _DataStore.isRun Then
                        _DataStore.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 16
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "DataStore.InSertCount. Article:" + _LK.LkNumber + " Count:" + _InterCount.ToString)
                    If _DataStore.InSertCount(_LK.LkNumber, _InterCount.ToString) Then
                        _DataStore.isRun = False
                        If _InterSave Then _ArticleCount.isRun = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _InterSave Then _ArticleCount.isRun = False
                        _DataStore.isRun = False
                        _i.StepInputNumber = _i.Address_Home
                    End If


                Case 17
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check if need Linecontrol")
                    If Not _Settings.LineControlCfg.bEnable Then
                        _i.StepInputNumber = 27
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If


                Case 18
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _LinecontrolStore not Run")
                    If Not _LinecontrolStore.isRun Then
                        _LinecontrolStore.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 19
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Get Linecontrol ini")
                    If _LinecontrolStore.GetConfig(_LK.LkNumber, _InterConfigName, _InterIndex) Then
                        _InterLinecontrolenable = False
                        _LinecontrolStore.isRun = False
                        _Logger.Logger(_i, _Messager, "Article：" + _LK.LkNumber + " Linecontrol ini:" + _InterConfigName + " Index:" + _InterIndex)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _InterLinecontrolenable = True
                        _LinecontrolStore.isRun = False
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Linecontrol Disable.")
                        _i.StepInputNumber = 27
                    End If

                Case 20
                    If _InterCount > 0 Then
                        _i.StepInputNumber = 22
                    Else
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _DataStore  and  _ArticleCount not Run")
                        If Not _DataStore.isRun And Not _ArticleCount.isRun Then
                            _DataStore.isRun = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 21
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Get Article Count")
                    If _DataStore.GetCount(_LK.LkNumber, _InterCount) Then
                        _DataStore.isRun = False
                        If _InterCount > 0 Then
                            _Logger.Logger(_i, _Messager, "Article：" + _LK.LkNumber + " amountOfBoards:" + _InterCount.ToString)
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            SetFailText(_Language.LanguageElement.GetTextLine(_i, "1005"))
                            _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Get Article Count Fail. Error Message :" + _DataStore.StatusDescription)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    Else
                        _DataStore.isRun = False
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1005"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Get Article Count Fail. Error Message :" + _DataStore.StatusDescription)
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 22
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "ChangeSNtoPreviousSNList")
                    If ChangeSNtoPreviousSNList(_LK.SerialNumber, _LK.LkNumber, _InterIndex, _InterCount) Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        SetFailText(_Language.LanguageElement.GetTextLine(_i, "1008"))
                        _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "ChangeSNtoPreviousSNList Fail")
                        _i.StepInputNumber = _i.Address_Home
                    End If

                Case 23
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Linecontrol Init")
                    If _LastArticle <> _LK.LkNumber Then

                        If _Lc.Init(_Settings.LineControlFolder + _InterConfigName, _Settings.LineControlCfg.strDefalutSection, _Settings.LineControlCfg.strTraceId) Then
                            _LastArticle = _LK.LkNumber
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            SetFailText(_Language.LanguageElement.GetTextLine(_i, "1009"))
                            _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Init LineControl Fail. Message:" + _Lc.StatusDescription)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 24
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _Lc not Run")
                    If Not _Lc.isRunning Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 25
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Linecontrol ReadPreviousStamp")
                    _Lc.ReadPreviousStamp(_ListLcSn)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 26
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _Lc not Run")
                    If Not _Lc.isRunning Then
                        If _Lc.isResult Then
                            _LcResult = True
                            _LcMessage = ""
                            For Each element As LineControlElement In _ListLcSn
                                _Logger.Logger(_i, _Messager, "Linecontrol ReadPreviousStamp:" + element.strSN + " " + element.strErrorMsg)
                                If element.bTestResult = False Then
                                    _LcResult = False
                                    If _LcMessage = "" Then
                                        _LcMessage = element.strSN + " " + element.strErrorMsg
                                    Else
                                        _LcMessage = _LcMessage + vbCrLf + element.strSN + " " + element.strErrorMsg
                                    End If
                                End If
                            Next
                            If _LcResult Then
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                AddFailProductionData()
                                SetFailText(_LcMessage)
                                _i.StepInputNumber = _i.Address_Home
                            End If
                        Else
                            SetFailText(_Language.LanguageElement.GetTextLine(_i, "1010"))
                            _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "Linecontrol ReadPreviousStamp Fail. Message:" + _Lc.StatusDescription)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If


                Case 27
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting Press OK Button")
                    If _i.Toggle Then _UI.Label_Msg.BackColor = Drawing.Color.LightGreen
                    _NeadRead = True
                    If _Read Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 28
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Open Output")
                    If _i.Toggle Then _UI.Label_Msg.BackColor = Drawing.Color.LightGray
                    _WriteTrue = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 29
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Open Output")
                    If Not _WriteTrue Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 30
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _TimeDelay not Run")
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 31
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Delay 1s")
                    _TimeDelay.Run(1000)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 32
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _TimeDelay not Run")
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 33
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting Release OK Button")
                    If Not _Read Then
                        _NeadRead = True
                        Threading.Thread.Sleep(_StationCfg.DelayTime)
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 34
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Off output")
                    _WriteFalse = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 35
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting PLC off output")
                    If Not _WriteFalse Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 36
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check if need MES")
                    If _InterArticleResult Or _InterMesResult Or Not _Settings.WebServiceCfg.Enable Then
                        _i.StepInputNumber = 44
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 37
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _WebService not Run")
                    If Not _WebService.isRun Then
                        _WebService.isRun = True
                        _isCallBackResult = False
                        _isCallBackRunning = False
                        _StartCallBack = False
                        _strCallBackMsg = ""
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 38
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService.startBoard")
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pstartBoard.BeginInvoke(_LK.SerialNumber, _strCallBackMsg, pstartBoardCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _WebService.isRun = False
                            If _Settings.WebServiceCfg.PassiveMode Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.startBoard Response:" + _strCallBackMsg)
                            _Logger.Logger(_i, _Messager, "WebService.startBoard Pass")
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            If _Settings.WebServiceCfg.PassiveMode Then
                                _WebService.isRun = False
                                _InterMesResult = True
                                _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.startBoard Response:" + _strCallBackMsg)
                                _Logger.Logger(_i, _Messager, "WebService.startBoard Pass")
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                AddFailProductionData()
                                _InterMesResult = True
                                _WebService.isRun = False
                                SetFailText(_InterWebServiceResult)
                                _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "WebService.startBoard Fail. Message:" + _strCallBackMsg)
                                _i.StepInputNumber = _i.Address_Home
                            End If
                        End If
                    End If

                Case 39
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _TimeDelay not Run")
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 40
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Delay 1s")
                    _TimeDelay.Run(1000)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 41
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _TimeDelay not Run")
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 42
                    If _InterMesResult Or Not _Settings.WebServiceCfg.Enable Then
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Inter Mes Fail")
                        _i.StepInputNumber = 44
                        Return
                    End If
                        _i.StepInputNumber = 44

                        'If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting _WebService not Run")
                        'If Not _WebService.isRun Then
                        '    _WebService.isRun = True
                        '_isCallBackResult = False
                        '_isCallBackRunning = False
                        '_StartCallBack = False
                        '    _i.StepInputNumber = _i.StepOutputNumber + 1
                        'End If

                Case 43
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService.completeBoard")
                        If Not _StartCallBack Then
                            _StartCallBack = True
                            _isCallBackRunning = True
                            pcompleteBoard.BeginInvoke(_LK.SerialNumber, _strCallBackMsg, pcompleteBoardCB, Nothing)
                        End If
                        If _StartCallBack And Not _isCallBackRunning Then
                            If _isCallBackResult Then
                                _WebService.isRun = False
                                If _Settings.WebServiceCfg.PassiveMode Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.completeBoard Response:" + _strCallBackMsg)
                                _Logger.Logger(_i, _Messager, "WebService.completeBoard Pass")
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                If _Settings.WebServiceCfg.PassiveMode Then
                                    _WebService.isRun = False
                                _Logger.Logger(_i, _Messager, "[PassiveMode]WebService.completeBoard Response:" + _strCallBackMsg)
                                    _Logger.Logger(_i, _Messager, "WebService.completeBoard Pass")
                                    _i.StepInputNumber = _i.StepOutputNumber + 1
                                Else
                                    AddFailProductionData()
                                    _WebService.isRun = False
                                    SetFailText(_strCallBackMsg)
                                    _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "WebService.completeBoard Fail. Message:" + _strCallBackMsg)
                                    _i.StepInputNumber = _i.Address_Home
                                End If
                            End If
                        End If

                Case 44
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Check if need Linecontrol")
                        If Not _Settings.LineControlCfg.bEnable Or _InterLinecontrolenable Then
                            _i.StepInputNumber = 49
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 45
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "ChangeSNtoCurrentSNList")
                        If ChangeSNtoCurrentSNList(_LK.SerialNumber, _LK.LkNumber, _InterIndex, _InterCount) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            SetFailText(_Language.LanguageElement.GetTextLine(_i, "1008"))
                            _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "ChangeSNtoCurrentSNList Fail")
                            _i.StepInputNumber = _i.Address_Home
                        End If

                Case 46
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _Lc not Run")
                        If Not _Lc.isRunning Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 47
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Linecontrol WriteCurrentStamp")
                        _Lc.WriteCurrentStamp(_ListLcSn)
                        _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 48
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _Lc not Run")
                        If Not _Lc.isRunning Then
                            If _Lc.isResult Then
                                _LcResult = True
                                _LcMessage = ""
                                For Each element As LineControlElement In _ListLcSn
                                    _Logger.Logger(_i, _Messager, "Linecontrol WriteCurrentStamp:" + element.strSN + " " + element.strErrorMsg)
                                    If element.bTestResult = False Then
                                        _LcResult = False
                                        If _LcMessage = "" Then
                                            _LcMessage = element.strSN + " " + element.strErrorMsg
                                        Else
                                            _LcMessage = vbCrLf + element.strSN + " " + element.strErrorMsg
                                        End If
                                    End If
                                Next
                                If _LcResult Then
                                    _i.StepInputNumber = _i.StepOutputNumber + 1
                                Else
                                    AddFailProductionData()
                                    SetFailText(_LcMessage)
                                    _i.StepInputNumber = _i.Address_Home
                                End If
                            Else
                                SetFailText(_Language.LanguageElement.GetTextLine(_i, "1010"))
                                _Logger.ThrowerNoStation(_i, enmLogType.ErrorLog, False, _Messager, "WriteCurrentStamp Fail. Message:" + _Lc.StatusDescription)
                                _i.StepInputNumber = _i.Address_Home
                            End If
                        End If

                Case 49
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Clean")
                        _UI.Label_Msg.Text = ""
                        _UI.TextBox_ErrorMsg.Text = ""
                        '  _UI.TextBox_Scap.Text = ""
                        _UI.TextBox_SN.Text = ""
                        AddPassProductionData()
                        _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 50
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "End")
                        _i.StepInputNumber = _i.Address_Home


                Case 100
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting for _SMTStore not Run")
                        If Not _SMTStore.isRun Then
                            _SMTStore.isRun = True
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If

                Case 101
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "Change SMT Number To Article")
                        _LK.SetNewLine(_InterResult)
                        If _SMTStore.GetArticle(_LK.LkNumber, _InterArticle) Then
                            _SMTStore.isRun = False
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _SMTStore.isRun = False
                            SetFailText(_Language.LanguageElement.GetTextLine(_i, "1011"))
                            _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "SMTStore GetArticle Fail.")
                            _i.StepInputNumber = _i.Address_Home
                        End If

                Case 102
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Create New SN")
                        _InterResult = _InterArticle + "/SN" + _LK.SerialNumber
                    _i.StepInputNumber = 2


                Case 120
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting WebService2 not Run")
                    If Not _WebService2.isRun Then
                        _WebService2.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 121
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.Start")
                    If _WebService2.Start(_LK.SerialNumber, _InterWebServiceResult) Then
                        _InterMesResult = False
                        _WebService2.isRun = False
                        If _Settings.WebServiceCfg.PassiveMode Then If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService2.Start Response:" + _InterWebServiceResult)
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.Start Pass")
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        If _Settings.WebServiceCfg.PassiveMode Then
                            _InterMesResult = True
                            _WebService2.isRun = False
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService2.Start Response:" + _InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.Start Pass")
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InterMesResult = True
                            AddFailProductionData()
                            _WebService2.isRun = False
                            If _InterSave Then _ArticleCount.isRun = False
                            SetFailText(_InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "WebService2.Start Fail. Message:" + _InterWebServiceResult)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If

                Case 122
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "Waiting WebService2 not Run")
                    If Not _WebService2.isRun Then
                        _WebService2.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 123
                    If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.signOff")
                    If _WebService2.signOff(_LK.SerialNumber, _InterWebServiceResult) Then
                        _InterMesResult = False
                        _WebService2.isRun = False
                        If _Settings.WebServiceCfg.PassiveMode Then If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService2.signOff Response:" + _InterWebServiceResult)
                        If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.signOff Pass")
                        _i.StepInputNumber = 12
                    Else
                        If _Settings.WebServiceCfg.PassiveMode Then
                            _InterMesResult = True
                            _WebService2.isRun = False
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "[PassiveMode]WebService2.signOff Response:" + _InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, _Messager, "WebService2.signOff Pass")
                            _i.StepInputNumber = 12
                        Else
                            _InterMesResult = True
                            AddFailProductionData()
                            _WebService2.isRun = False
                            If _InterSave Then _ArticleCount.isRun = False
                            SetFailText(_InterWebServiceResult)
                            If _i.Toggle Then _Logger.Logger(_i, enmLogType.ErrorLog, _Messager, "WebService2.signOff Fail. Message:" + _InterWebServiceResult)
                            _i.StepInputNumber = _i.Address_Home
                        End If
                    End If


                Case 140
                    _BitSql.GetBit(_LK.LkNumber, _strBitNumber)
                    If _strBitNumber = "" Then
                        _i.StepInputNumber = 150
                    Else
                        _i.StepInputNumber = _i.StepInputNumber + 1
                    End If

                Case 141
                    Dim pAlarm As New AlarmMessage
                    pAlarm.Init(_Devices)
                    pAlarm.Message = _Language.LanguageElement.GetTextLine(_i, "1020") + vbCrLf + _Language.LanguageElement.GetTextLine(_i, "1021") + _strBitNumber
                    pAlarm.ShowTime = 1
                    pAlarm.ShowDialog()
                    _BitLastArticle = _LK.LkNumber
                    _FileHandler.WriteIniFile(_Settings.LogFolder + _i.Name + "_Article.ini", "Article", "ID", _BitLastArticle)
                    _i.StepInputNumber = 150



                Case 150
                    _PlcSql.GetPLC(_LK.LkNumber, _strPLcAddress)
                    _i.StepInputNumber = _i.StepInputNumber + 1

                Case 151
                    WritePLCAddress = True
                    _i.StepInputNumber = _i.StepInputNumber + 1

                Case 152
                    If Not WritePLCAddress Then
                        _i.StepInputNumber = 4
                    End If

            End Select


        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:" + _i.StepOutputNumber.ToString + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
    End Sub


    Protected Sub DataReceived(ByVal Pass As Boolean, ByVal Result As String, ByVal ErrorMsg As String)
        Dim ReceiveMsg As String
        If ErrorMsg <> "" Then
            _BarcodeMsg = ErrorMsg
            Return
        End If
        If Pass Then
            ReceiveMsg = "Barcode Receive. Result:True Data:" + Result
            If _i.StepOutputNumber = _i.Address_Home Then _ScanResult = Result
        Else
            ReceiveMsg = "Barcode Receive. Result:False Message:" + ErrorMsg + " Data:" + Result
        End If
        _Logger.Logger(_i, _Messager, ReceiveMsg, "DataReceived")
        Application.DoEvents()
    End Sub



    Protected Function _startBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
        Return _WebService.startBoard(strSN, strResult)
    End Function

    Protected Sub _startBoardCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pstartBoard.EndInvoke(_strCallBackMsg, Result)
        _isCallBackRunning = False
    End Sub

    Protected Function _completeBoard(ByVal strSN As String, ByRef strResult As String) As Boolean
        Return _WebService.completeBoard(strSN, strResult)
    End Function

    Protected Sub _completeBoardCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pcompleteBoard.EndInvoke(_strCallBackMsg, Result)
        _isCallBackRunning = False
    End Sub

    Private Sub InitProductionData()
        Dim strResult As String
        strResult = _FileHandler.ReadIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Total")
        If strResult <> FileHandler.s_DEFAULT And strResult <> FileHandler.s_Null Then
            _ProductionCount.TotalCount = CInt(strResult)
        Else
            _ProductionCount.TotalCount = 0
        End If
        strResult = _FileHandler.ReadIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Pass")
        If strResult <> FileHandler.s_DEFAULT And strResult <> FileHandler.s_Null Then
            _ProductionCount.PassCount = CInt(strResult)
        Else
            _ProductionCount.PassCount = 0
        End If
        strResult = _FileHandler.ReadIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Fail")
        If strResult <> FileHandler.s_DEFAULT And strResult <> FileHandler.s_Null Then
            _ProductionCount.FailCount = CInt(strResult)
        Else
            _ProductionCount.FailCount = 0
        End If


        _BitLastArticle = _FileHandler.ReadIniFile(_Settings.LogFolder + _i.Name + "_Article.ini", "Article", "ID")

        _UI.lbltotal.Text = _ProductionCount.TotalCount
        _UI.lblPass.Text = _ProductionCount.PassCount
        _UI.lblfail.Text = _ProductionCount.FailCount
    End Sub

    Private Sub AddPassProductionData()
        _ProductionCount.TotalCount = _ProductionCount.TotalCount + 1
        _ProductionCount.PassCount = _ProductionCount.PassCount + 1
        _UI.lbltotal.Text = _ProductionCount.TotalCount
        _UI.lblPass.Text = _ProductionCount.PassCount
        _UI.lblfail.Text = _ProductionCount.FailCount
        WriteIni()
    End Sub

    Private Sub AddFailProductionData()
        _ProductionCount.TotalCount = _ProductionCount.TotalCount + 1
        _ProductionCount.FailCount = _ProductionCount.FailCount + 1
        _UI.lbltotal.Text = _ProductionCount.TotalCount
        _UI.lblPass.Text = _ProductionCount.PassCount
        _UI.lblfail.Text = _ProductionCount.FailCount
        WriteIni()
    End Sub


    Private Sub _Shift_ShiftChange2(ByRef CurShift As Integer)

    End Sub

    Private Sub _Shift_ShiftChange(ByRef CurShift As Integer)
        _CurrentShift = CurShift
        _ProductionCount.TotalCount = 0
        _ProductionCount.PassCount = 0
        _ProductionCount.FailCount = 0
        _UI.lbltotal.Text = _ProductionCount.TotalCount
        _UI.lblPass.Text = _ProductionCount.PassCount
        _UI.lblfail.Text = _ProductionCount.FailCount
        WriteIni()
        _Logger.Logger(_i, _Messager, "Shift Change:" + CurShift.ToString, "Shift_ShiftChange ")
    End Sub

    Private Sub Button_Clean_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        _ProductionCount.TotalCount = 0
        _ProductionCount.PassCount = 0
        _ProductionCount.FailCount = 0
        _UI.lbltotal.Text = _ProductionCount.TotalCount
        _UI.lblPass.Text = _ProductionCount.PassCount
        _UI.lblfail.Text = _ProductionCount.FailCount
        WriteIni()
        _Logger.Logger(_i, _Messager, "Button_Clean_Click", "Button_Clean_Click ")
    End Sub


    Private Sub WriteIni()
        _FileHandler.WriteIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Total", _ProductionCount.TotalCount.ToString)
        _FileHandler.WriteIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Pass", _ProductionCount.PassCount.ToString)
        _FileHandler.WriteIniFile(_Settings.LogFolder + _i.Name + "_Count.ini", _Shift.GetCurrentDate + "_" + _CurrentShift.ToString, "Fail", _ProductionCount.FailCount.ToString)
    End Sub
    Private Sub Button_Abort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If _i.StepOutputNumber = 27 Then
            _UI.Label_Msg.Text = ""
            _UI.TextBox_ErrorMsg.Text = ""
            _UI.TextBox_Scap.Text = ""
            _UI.TextBox_SN.Text = ""
            _i.StepInputNumber = _i.Address_Home
        End If
    End Sub

    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, "Dispose Pass", "Dispose")
        _i = Nothing
        _Settings = Nothing
        If Not IsNothing(_Scanner) Then
            _Scanner.Dispose()
        End If
    End Sub

    Private Function ChangeSNtoPreviousSNList(ByVal strSN As String, ByVal strArticle As String, ByVal strIndex As String, ByVal iCount As Integer) As Boolean
        Dim strTempSN As String = ""
        _ListLcSn.Clear()
        If iCount > 35 Then
            Return False
        End If
        strTempSN = strSN.Substring(0, 12)
        For i As Integer = 1 To iCount
            If i <= 9 Then
                If _listScrapSN.Contains(i.ToString) Then
                    _ListLcSn.Add(New LineControlElement(strTempSN + i.ToString, strArticle, strIndex, _Settings.LineControlCfg.strPreviousTest, True))
                Else
                    _ListLcSn.Add(New LineControlElement(strTempSN + i.ToString, strArticle, strIndex, _Settings.LineControlCfg.strPreviousTest, False))
                End If
            End If
            If i > 9 Then
                If _listScrapSN.Contains(i.ToString) Then
                    _ListLcSn.Add(New LineControlElement(strTempSN + Chr(65 + i - 10), strArticle, strIndex, _Settings.LineControlCfg.strPreviousTest, True))
                Else
                    _ListLcSn.Add(New LineControlElement(strTempSN + Chr(65 + i - 10), strArticle, strIndex, _Settings.LineControlCfg.strPreviousTest, False))
                End If
            End If
        Next
        Return True
    End Function

    Private Function ChangeSNtoCurrentSNList(ByVal strSN As String, ByVal strArticle As String, ByVal strIndex As String, ByVal iCount As Integer) As Boolean
        Dim strTempSN As String = ""
        _ListLcSn.Clear()
        If iCount > 35 Then
            Return False
        End If
        strTempSN = strSN.Substring(0, 12)
        For i As Integer = 1 To iCount
            If i <= 9 Then
                If _listScrapSN.Contains(i.ToString) Then
                    _ListLcSn.Add(New LineControlElement(strTempSN + i.ToString, strArticle, strIndex, _Settings.LineControlCfg.strCurrentTest, True, True))
                Else
                    _ListLcSn.Add(New LineControlElement(strTempSN + i.ToString, strArticle, strIndex, _Settings.LineControlCfg.strCurrentTest, True, False))
                End If
            End If
            If i > 9 Then
                If _listScrapSN.Contains(i.ToString) Then
                    _ListLcSn.Add(New LineControlElement(strTempSN + Chr(65 + i - 10), strArticle, strIndex, _Settings.LineControlCfg.strCurrentTest, True, True))
                Else
                    _ListLcSn.Add(New LineControlElement(strTempSN + Chr(65 + i - 10), strArticle, strIndex, _Settings.LineControlCfg.strCurrentTest, True, False))
                End If
            End If
        Next
        Return True
    End Function
End Class
