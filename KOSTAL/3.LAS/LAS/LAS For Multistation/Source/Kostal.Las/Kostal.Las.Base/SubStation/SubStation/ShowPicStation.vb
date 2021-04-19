Imports System.Drawing

Public Class ShowPicStation
    Inherits StationTypeBase
    Protected _UIStation As ShowPicUI
    Protected _Refs As References
    Protected WithEvents _Pic As LAS
    Protected _ShowPicDefine As IShowPicDefine
    Protected _Fileds As String()
    Protected _NewPartStation As NewPartStation
    Protected _ReferenceStation As ReferenceStation
    Protected _ReTestStation As ReTestStation
    Protected _ListScanner As New Dictionary(Of String, ScannerStation)
    Protected _LastNewPartMsg As String
    Protected Delegate Function dGetAllFieldsOfFileName(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
    Protected pGetAllFieldsOfFileName As New dGetAllFieldsOfFileName(AddressOf _GetAllFieldsOfFileName)
    Protected pGetAllFieldsOfFileNameCB As AsyncCallback = New AsyncCallback(AddressOf _GetAllFieldsOfFileNameCB)
    Protected _NewPartMsg As String
    Protected _PLCErrorCode As String
    Protected _PLCLastErrorCode As String
    Public Const Name As String = "ShowPicStation"
    Protected _LastMessage As String
    Protected _LastColor As Color
    Protected _mTemp As String

    Public Property PLCErrorCode As String
        Get
            Return _PLCErrorCode
        End Get
        Set(value As String)
            _PLCErrorCode = value
        End Set
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal ShowPicDefine As IShowPicDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New ShowPicUI
            _ShowPicDefine = ShowPicDefine
            _CheckTrigInfo = CheckTrigInfo
            _Pic = New LAS
            _UI = _UIStation
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

            For Each substation In _Stations.Values
                If TypeOf substation Is NewPartStation Then
                    _NewPartStation = CType(substation, NewPartStation)
                End If
                If TypeOf substation Is ReferenceStation Then
                    _ReferenceStation = CType(substation, ReferenceStation)
                End If
                If TypeOf substation Is ReTestStation Then
                    _ReTestStation = CType(substation, ReTestStation)
                End If
            Next
            _Language.ReadControlText(_UIStation.Panel)
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
        If Not IsNothing(_Pic) Then _Pic.ReLoadLanguage()
        Return (True)
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
            DisplayMsg()
            If Not CheckStepLine() Then Return
            If Not BeforeLine() Then Return
            If Not UpdateMsg(ShowPicStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _UIStation.AddColumns()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    If Not _Pic.Init(_i, _SubStationCfg.Config.Split(CChar(","))(0), AppSettings, _Language) Then
                        _Devices.Add(_SubStationCfg.Name, _Pic)
                        _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_FAIL, "FAIL", _SubStationCfg.Config), "Pic.Init")
                    Else
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                    End If

                Case -98
                    For Each _ScannerScanner In _Pic.DisplayScanner.Values
                        If _Stations.ContainsKey(_ScannerScanner) Then
                            _ListScanner.Add(_ScannerScanner, CType(_Stations(_ScannerScanner), ScannerStation))
                        Else
                            _Logger.ThrowerNoStation(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_FAIL_ADD, "FAIL", _ScannerScanner), "Pic.Init")
                        End If
                    Next
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -97
                    _i.StepInputNumber = _i.Address_Home

                    '====================================================================================================
                    '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_START))
                        _InternMsg = ""
                        _NewPartMsg = ""
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                    If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_START))
                        _InternMsg = ""
                        _NewPartMsg = ""
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1  '判断PLC传递信息
                    CheckStructDeviceInteractionPLCInfo()
                    _StartCallBack = False

                Case 2  '样件模式不扫描匹配
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    If Not _StartCallBack Then
                        _StartCallBack = True
                        _isCallBackRunning = True
                        pGetAllFieldsOfFileName.BeginInvoke(_i, _LocalArticle, _Devices, _Stations, _Fileds, pGetAllFieldsOfFileNameCB, Nothing)
                    End If
                    If _StartCallBack And Not _isCallBackRunning Then
                        If _isCallBackResult Then
                            _i.StepInputNumber = _i.StepOutputNumber + 1
                        Else
                            _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_DEFINE, "FAIL", _ShowPicDefine.ErrorMsg))
                            _InternMsg = _ShowPicDefine.ErrorMsg
                            _i.StepInputNumber = _i.Address_Fail
                        End If
                    End If

                Case 4
                    _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_PICTURE).Data))
                    _Pic.ShowPic(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_NAME).Data, _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data, _Fileds)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    _NewPartMsg = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_MSG3)
                    _i.StepInputNumber = _i.Address_Pass

                Case 1000
                    '回写PLC Pass
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_PICTURE).Data,
                               True)
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()
                    If _i.StepInputNumber = _i.Address_Home Then
                        _NewPartMsg = ""
                        _Pic.DelectPic()
                    End If

                Case 2000
                    '回写PLC Fail
                    _UIStation.AddRow(_LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_CUSTOMER_NUMBER).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ARTICLE_FAMILY).Data,
                               _LocalArticle.ArticleElements(KostalArticleKeys.KEY_PICTURE).Data,
                               False)
                    UpdateStructDeviceInteractionFailStep1()

                Case 2001
                    UpdateStructDeviceInteractionFailStep2()
                    If _i.StepInputNumber = _i.Address_Home Then
                        _NewPartMsg = ""
                        _Pic.DelectPic()
                    End If

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

    Public Sub DisplayMsg()

        If _Pic.DisplayNewPart Then
            If _PLCErrorCode <> "" And _PLCErrorCode <> "0" Then
                If _PLCLastErrorCode <> _PLCErrorCode Then
                    _LastMessage = _Pic.Label_Msg.Text
                    _LastColor = _Pic.Label_Msg.ForeColor
                    _mTemp = ""
                    If _FileHandler.FileExist(AppSettings.HelpFolder + "\" + _SubStationCfg.Config.Split(CChar(","))(1)) Then
                        _mTemp = _FileHandler.ReadIniFile(AppSettings.HelpFolder + "\" + _SubStationCfg.Config.Split(CChar(","))(1), _Language.LanguageElement.SelectedLanguageFileName, _PLCErrorCode)
                        If _mTemp = FileHandler.s_DEFAULT Or _mTemp = FileHandler.s_Null Then
                            _mTemp = "UnKnown Error"
                        End If
                    Else
                        _mTemp = "UnKnown Error"
                    End If
                    _mTemp = _PLCErrorCode + "," + _mTemp
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
                    _Pic.Label_Msg.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SHOWPIC_MSG4, _mTemp).Trim
                    _PLCLastErrorCode = _PLCErrorCode
                End If
                Return
            End If

            If _PLCErrorCode = "" Or _PLCErrorCode = "0" Then
                _PLCLastErrorCode = ""
            End If

            If _LastMessage <> "" Then
                _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                _Pic.Label_Msg.ForeColor = _LastColor
                _Pic.Label_Msg.Text = _LastMessage
                _LastMessage = ""
            End If

            For Each _Scanner In _ListScanner.Values
                If _Scanner.NewPartMsg.StrMsg <> "" Then
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.ForeColor = _Scanner.NewPartMsg.TextColor
                    _Pic.Label_Msg.Text = _Scanner.NewPartMsg.StrMsg
                    _Scanner.NewPartMsg.StrMsg = ""
                End If
            Next

            If Not IsNothing(_ReferenceStation) Then
                If _ReferenceStation.ReferenceMsg.StrMsg <> "" Then
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.Text = _ReferenceStation.ReferenceMsg.StrMsg
                    _Pic.Label_Msg.ForeColor = _ReferenceStation.ReferenceMsg.TextColor
                    _ReferenceStation.ReferenceMsg.StrMsg = ""
                End If
            End If

            If Not IsNothing(_ReTestStation) Then
                If _ReTestStation.ReTestMsg.StrMsg <> "" Then
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.Text = _ReTestStation.ReTestMsg.StrMsg
                    _Pic.Label_Msg.ForeColor = _ReTestStation.ReTestMsg.TextColor
                    _ReTestStation.ReTestMsg.StrMsg = ""
                End If
            End If

            If _NewPartMsg = "" Then
                If _LastNewPartMsg <> _NewPartStation.NewPartMsg Then
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.Text = _NewPartStation.NewPartMsg
                    _Pic.Label_Msg.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_NOMALBLUE)
                    _LastNewPartMsg = _NewPartStation.NewPartMsg
                End If
            Else
                If _LastNewPartMsg <> _NewPartMsg Then
                    _Pic.Label_Msg.Font = New Font("Calibri", 32, FontStyle.Bold)
                    _Pic.Label_Msg.Text = _NewPartMsg
                    _Pic.Label_Msg.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_NOMALBLUE)
                    _LastNewPartMsg = _NewPartMsg
                End If
            End If
        End If
    End Sub

    Protected Function _GetAllFieldsOfFileName(ByVal _i As Station, ByVal LocalArticle As Article, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByRef fileds As String()) As Boolean
        Return _ShowPicDefine.GetAllFieldsOfFileName(_i, LocalArticle, Devices, Stations, fileds)
    End Function

    Protected Sub _GetAllFieldsOfFileNameCB(ByVal Result As IAsyncResult)
        _isCallBackResult = pGetAllFieldsOfFileName.EndInvoke(_Fileds, Result)
        _isCallBackRunning = False
    End Sub

    Public Overrides Sub Dispose()
        On Error Resume Next
        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))

        If _Pic IsNot Nothing Then
            _Pic.Dispose()
        End If
        _i = Nothing
        AppSettings = Nothing
        _Language = Nothing
        _Logger = Nothing
        _LocalArticle = Nothing
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub
End Class
