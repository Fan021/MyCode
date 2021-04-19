Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Public Class PrinterStation
    Inherits StationTypeBase
    Protected WithEvents _UIStation As PrinterUI
    Protected _Printer As IPrinter
    Protected _Refs As References
    Protected _Fileds As String()
    Protected _PrinterDefine As IPrinterDefine
    Protected _SNStation As SNStation
    Protected _RePrint As Boolean
    Protected _TimeDelay As New TimeDelay
    Protected Delegate Function dGetAllFieldsOfPrintFile(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
    Protected pGetAllFieldsOfPrintFile As New dGetAllFieldsOfPrintFile(AddressOf _GetAllFieldsOfPrintFile)
    Protected pGetAllFieldsOfPrintFileCB As AsyncCallback = New AsyncCallback(AddressOf _GetAllFieldsOfPrintFileCB)
    Public Const Name As String = "PrinterStation"
    Public iPrintCount As Integer = 0
    Public iNowCount As Integer = 0
    Public ReadOnly Property UIStation As PrinterUI
        Get
            Return _UIStation
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal Printer As IPrinter, ByVal PrinterDefine As IPrinterDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New PrinterUI
            _Printer = Printer
            _PrinterDefine = PrinterDefine
            _CheckTrigInfo = CheckTrigInfo
            _UI = _UIStation
            _LastVariantInfo = New StructVariantInfo
            _Messager.Construct(_UIStation.Msg)
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

            If _Devices.ContainsKey(References.Name) Then
                _Refs = CType(_Devices(References.Name), References)
            End If

            If _SubStationCfg.SN <> "" Then
                _SNStation = CType(_Stations(_SubStationCfg.SN), SNStation)
            End If
            AddHandler _UIStation.RePrint, AddressOf RePrint
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
        _Language.ReadControlText(_UIStation.Panel)
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
            If Not UpdateMsg(PrinterStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _UIStation.AddColumns(20)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If _SubStationCfg.Enable Then
                        If Not _Printer.Init(_SubStationCfg.Type, AppSettings.ConfigFolder + _SubStationCfg.Config, _i, AppSettings, _Language) Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_INIT_FAIL, "FAIL", _Printer.StatusDescription), "Printer.Init")
                        Else

                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_INIT_PASS, "Successful", ""), "Printer.Init")
                            _Devices.Add(_SubStationCfg.Name, _Printer)
                            _i.StepInputNumber = _i.StepOutputNumber + 1

                        End If
                    Else
                        If _i.Toggle Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_INIT_PASS, "Disable"), "Printer.Init")
                        End If
                    End If

                Case -98
                    If _AppArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -97
                    If Not _Printer.Running Then
                        If _Printer.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF Then
                            If _Printer.GetLabelStatus Then  '有标签
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _i.StepInputNumber = _i.StepOutputNumber + 8
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 8
                        End If
                    End If

                Case -96
                    If Not _Printer.Running Then
                        If _Printer.ChangePrintModeTo(enumZebra_PrintModes.ZEBRA_PRINT_MODE_TEAR_OFF) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.ChangePrintModeTo")
                        End If
                    End If

                Case -95
                    If Not _Printer.Running Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_CALIBRATION_START))
                        If _Printer.Calibration Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.Calibration")
                        End If
                    End If

                Case -94
                    If Not _Printer.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -93
                    If Not _TimeDelay.Running Then
                        _TimeDelay.Run(6000)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_CALIBRATION_WAIT))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -92
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -91
                    If Not _Printer.Running Then
                        If _Printer.Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.Calibration")
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case -90
                    If Not _Printer.Running Then
                        If _Printer.ChangePrintModeTo(enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.ChangePrintModeTo")
                        End If
                    End If

                Case -89
                    If Not _Printer.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -88 'Calibrate End
                    _i.StepInputNumber = _i.Address_Home

                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position
                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _UIStation.btnReprint.Enabled = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_START))
                        _StationMode = 1
                        _LastVariantInfo = _ReadStructDeviceInteraction.stuPlcArticleSet
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _UIStation.btnReprint.Enabled = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_START))
                        _StationMode = 2
                        _LastVariantInfo = _ManualReadStructDeviceInteraction.stuPlcArticleSet
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                    If _RePrint Then '手动打印
                        If _UIStation.TextBox_Count.Text = "" Then
                            _UIStation.TextBox_Count.Text = "1"
                        End If
                        iNowCount = 1
                        iPrintCount = CInt(_UIStation.TextBox_Count.Text)
                        _UIStation.btnReprint.Enabled = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_START))
                        _InternMsg = ""
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _StartCheckTrigInfoDefineCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()


                Case 2  '样件模式不扫描匹配
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    If _SubStationCfg.SN <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 4
                    End If

                Case 4
                    If Not _SNStation.isRun Then
                        _SNStation.isRun = True
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 5 '产生序列号
                    _SNStation.StartCreatSN = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 6
                    If Not _SNStation.StartCreatSN Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = _SNStation.mSN
                        _SNStation.isRun = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 7
                    If Not _Printer.Running Then
                        If _Printer.PrintMode = enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF Then
                            If _Printer.GetLabelStatus Then  '有标签
                                _i.StepInputNumber = _i.StepOutputNumber + 1
                            Else
                                _i.StepInputNumber = _i.StepOutputNumber + 8
                            End If
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 8
                        End If
                    End If

                Case 8
                    If Not _Printer.Running Then
                        If _Printer.ChangePrintModeTo(enumZebra_PrintModes.ZEBRA_PRINT_MODE_TEAR_OFF) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.ChangePrintModeTo")
                            _InternMsg = _Printer.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 9
                    If Not _Printer.Running Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_CALIBRATION_START))
                        If _Printer.Calibration Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.Calibration")
                            _InternMsg = _Printer.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 10
                    If Not _Printer.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 11
                    If Not _TimeDelay.Running Then
                        _TimeDelay.Run(6000)
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_CALIBRATION_WAIT))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 12
                    If Not _TimeDelay.Running Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 13
                    If Not _Printer.Running Then
                        If _Printer.Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.Calibration")
                            _InternMsg = _Printer.StatusDescription
                            _i.StepInputNumber = _i.Address_Fail
                        Else
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        End If
                    End If

                Case 14
                    If Not _Printer.Running Then
                        If _Printer.ChangePrintModeTo(enumZebra_PrintModes.ZEBRA_PRINT_MODE_PEEL_OFF) Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _InternMsg = _Printer.StatusDescription
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.ChangePrintModeTo")
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 15
                    If Not _Printer.Running Then
                        _StartCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 16

                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        _Fileds = Nothing
                        pGetAllFieldsOfPrintFile.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _Fileds, pGetAllFieldsOfPrintFileCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                          _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                          _Fileds,
                                          True)
                            If Not IsNothing(_Fileds) Then
                                For j = 0 To _Fileds.Length - 1
                                    _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_DEFINE_VALUE, j.ToString, _Fileds(j)))
                                Next
                            End If
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                                       _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                                       _Fileds,
                                       False)
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_DEFINE, "FAIL", _PrinterDefine.ErrorMsg))
                            _InternMsg = _PrinterDefine.ErrorMsg
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If


                Case 17
                    If _UIStation.cbClearMask.Checked Then
                        _Printer.ClearMaskFile = True
                    End If
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_FILE).Data = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", KostalArticleKeys.KEY_MASK_FILE + " is Null"), "Printer.SendData")
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_NAME).Data = "" Then
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", KostalArticleKeys.KEY_MASK_NAME + " is Null"), "Printer.SendData")
                        _i.StepInputNumber = _i.Address_Fail
                    End If
                    If _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_FILE).Data <> "" And _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_NAME).Data <> "" Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 18
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_MASK, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_FILE).Data))
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 19
                    If _Printer.SendData(_Fileds, AppSettings.PrinterFolder, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_FILE).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_MASK_NAME).Data) Then
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    Else
                        _InternMsg = _Printer.StatusDescription
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription), "Printer.SendData")
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                Case 20
                    If Not _Printer.Running Then
                        If _Printer.Status <> enumZebra_ErrorCodes.ZEBRA_ERROR_NO_ERROR Then
                            _InternMsg = _Printer.StatusDescription
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_SENDDATA, "FAIL", _Printer.StatusDescription + _Printer.Status.ToString), "Printer.SendData")
                            _i.StepInputNumber = _i.Address_Fail
                            Return
                        End If
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_PRINT_END))
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case 21
                    If _RePrint Then
                        iNowCount = iNowCount + 1
                        _Logger.Logger(_i, _Messager, "Now Count:" + iNowCount.ToString)
                        If iNowCount > iPrintCount Then
                            _RePrint = False
                            _UIStation.btnReprint.Enabled = True
                            _i.StepInputNumber = _i.Address_Home
                        Else
                            _i.StepInputNumber = 1
                        End If

                    Else
                        _UIStation.btnReprint.Enabled = True
                        _i.StepInputNumber = _i.Address_Pass
                    End If


                Case 1000
                    '回写PLC
                    _UIStation.btnReprint.Enabled = True
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()

                Case 1002
                    _UIStation.btnReprint.Enabled = True

                Case 2000
                    '回写PLC
                    _UIStation.btnReprint.Enabled = True
                    If _RePrint Then
                        _RePrint = False
                        _i.StepInputNumber = _i.Address_Home
                        Return
                    End If
                    UpdateStructDeviceInteractionFailStep1()

                Case 2001
                    UpdateStructDeviceInteractionFailStep2()

                Case 2002
                    _UIStation.btnReprint.Enabled = True

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

    Public Sub RePrint()
        If _SubStationCfg.SN <> "" Then '手动模式下才有效
            If _i.StepOutputNumber <> _i.Address_Home Then
                _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STEPHOME))
            Else
                _StationMode = 3
                _RePrint = True
            End If
        End If
        If _SubStationCfg.MainDevice = "" And _SubStationCfg.SN = "" And _LastVariantInfo.strKostalNr <> "" And _LastVariantInfo.strSerialNr <> "" Then
            If _i.StepOutputNumber <> _i.Address_Home Then
                _Logger.Logger(_i, True, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_STEPHOME))
            Else
                _StationMode = 4
                _RePrint = True
            End If
        End If
    End Sub


    Protected Function _GetAllFieldsOfPrintFile(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
        Return _PrinterDefine.GetAllFieldsOfPrintFile(_i, LocalArticle, Devices, Stations, fileds)
    End Function

    Protected Sub _GetAllFieldsOfPrintFileCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetAllFieldsOfPrintFile.EndInvoke(_Fileds, Result)
        _isCallBackRunning = False
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
            _Printer.Dispose()
        End If
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

End Class
