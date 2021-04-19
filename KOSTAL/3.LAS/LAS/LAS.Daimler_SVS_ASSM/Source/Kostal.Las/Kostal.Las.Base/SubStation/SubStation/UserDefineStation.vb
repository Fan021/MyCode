Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel

Public Class UserDefineStation
    Inherits StationTypeBase
    Implements INotifyPropertyChanged
    Protected UserStationDefine As IUserStationDefine
    Protected _OutStructDeviceInteraction As StructDeviceInteraction
    Protected strStationName As String = ""
    Public Const Name As String = "UserDefineStation"
    Public Event PropertyChanged(sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Dim StationTypeBase As IStationTypeBase
    Dim StationBase As StationTypeBase
    Dim mReadSN As String = ""
    Dim bReScan As Boolean = False
    Public Shared DoQuery As String = "Doquery"
    Public ReadOnly Property OutStructDeviceInteraction As StructDeviceInteraction
        Get
            Return _OutStructDeviceInteraction
        End Get
    End Property
    Public Property LastScannedSerialNumber As String
        Get
            Return mReadSN
        End Get
        Set(ByVal value As String)
            mReadSN = ""
            bReScan = False
            '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("LastScannedSerialNumber"))
        End Set
    End Property

    Public Property ReScan As Boolean
        Get
            Return bReScan
        End Get
        Set(ByVal value As Boolean)
            bReScan = value
            '    RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("LastScannedSerialNumber"))
        End Set
    End Property

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), ByVal UserStationDefine As IUserStationDefine, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UI = New LineControlUI
            _CheckTrigInfo = CheckTrigInfo
            Me.UserStationDefine = UserStationDefine
            _OutStructDeviceInteraction = New StructDeviceInteraction
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
            ReLoadLanguage()
        Catch ex As Exception
            If IsNothing(_i) Then
                Throw New Exception("Station:Nothing" + vbCrLf + "Message:" + ex.Message.ToString)
            Else
                Throw New Exception("Station:" + _i.Name + vbCrLf + "Step:Init" + vbCrLf + "Message:" + ex.Message.ToString)
            End If
        End Try
        Return True
    End Function

    Public Overrides Function ReLoadLanguage() As Boolean
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
            If Not UpdateMsg(ScannerStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -99
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case -98
                    _i.StepInputNumber = _i.Address_Home

                '====================================================================================================
                '====================================================================================================
                Case 0  'Home Position

                    If _i.Toggle Or _ManualOffPulse Or _ManualRefresh Then
                        _ManualRefresh = False
                    End If

                    If _ReadStructDeviceInteraction.bulPlcDoAction Then
                        _InternMsg = ""
                        mReadSN = ""
                        If _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine.IndexOf("DoQuery") >= 0 Then
                            bReScan = True
                        End If
                        _InternPass = False
                        _InternFail = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_START))
                        _StationMode = 1
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal.Add("_ReadStructDeviceInteraction", _ReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ReadStructDeviceInteraction") Then _TrigSignal("_ReadStructDeviceInteraction") = _ReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                        If _ManualReadStructDeviceInteraction.bulPlcDoAction Then
                        _InternMsg = ""
                        mReadSN = ""
                        If _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine.IndexOf("DoQuery") >= 0 Then
                            bReScan = True
                        End If
                        _InternPass = False
                        _InternFail = False
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SCAN_START))
                        _StationMode = 2
                        _StartCheckTrigInfoDefineCallBack = False
                        If Not _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal.Add("_ManualReadStructDeviceInteraction", _ManualReadStructDeviceInteraction)
                        If _TrigSignal.ContainsKey("_ManualReadStructDeviceInteraction") Then _TrigSignal("_ManualReadStructDeviceInteraction") = _ManualReadStructDeviceInteraction
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If

                Case 1
                    CheckStructDeviceInteractionPLCInfo()
                Case 2
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    UserStationDefine.GetStationName(_i, _LocalArticle, _Devices, _Stations, strStationName)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 4
                    StationBase = CType(_Stations(strStationName), StationTypeBase)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strCustomerNr = _ReadStructDeviceInteraction.stuPlcArticleSet.strCustomerNr
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalArticleName = _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalArticleName
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strProductFamily = _ReadStructDeviceInteraction.stuPlcArticleSet.strProductFamily
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                    StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine = _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine
                    StationBase.ReadStructDeviceInteraction.bulPlcDoAction = True
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 6
                    If StationBase.ReadStructDeviceInteraction.bulAdsActionIsPass Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                        StationBase.ReadStructDeviceInteraction.Clear()
                        _i.StepInputNumber = _i.Address_Pass
                    End If

                    If StationBase.ReadStructDeviceInteraction.bulAdsActionIsFail Then
                        _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data = StationBase.ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr
                        _InternMsg = StationBase.ReadStructDeviceInteraction.strAdsActionValue
                        StationBase.ReadStructDeviceInteraction.Clear()
                        _i.StepInputNumber = _i.Address_Fail
                    End If

                '回写PLC Pass
                Case 1000
                    If _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine.IndexOf("DoQuery") >= 0 Then
                        mReadSN = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    Else
                        mReadSN = ""
                    End If
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()

                Case 2000
                    If _ReadStructDeviceInteraction.stuPlcArticleSet.strUserDefine.IndexOf("DoQuery") >= 0 Then
                        mReadSN = ""
                    Else
                        mReadSN = ""
                    End If
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionFailStep1()

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
        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub

End Class
