
Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.Drawing

Public Class PLCAlarmStation
    Inherits StationTypeBase
    Protected _UIStation As PLCAlarmUI
    Public Const Name As String = "PLCAlarmStation"
    Protected iOldErrorCode As Integer = 0
    Protected iErrorCode As Integer = 0
    Protected cErrorCodeManager As clsErrorCodeManager
    Protected _lblRefPart As Label
    Protected _MessageManager As MessageManager
    Protected strErrorMsg As String = ""
    Public Property OldErrorCode As Integer
        Set(value As Integer)
            iOldErrorCode = value
        End Set
        Get
            Return iOldErrorCode
        End Get
    End Property

    Public Property ErrorCode As Integer
        Set(value As Integer)
            iErrorCode = value
        End Set
        Get
            Return iErrorCode
        End Get
    End Property


    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal lblRefPart As Label = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)

        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UIStation = New PLCAlarmUI
            _UI = _UIStation
            _CheckTrigInfo = CheckTrigInfo
            _lblRefPart = lblRefPart
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
            'AddHandler _UIStation.OK.Click, AddressOf Send_Click
            If _Devices.ContainsKey(clsErrorCodeManager.Name) Then
                cErrorCodeManager = CType(_Devices(clsErrorCodeManager.Name), clsErrorCodeManager)
            Else
                cErrorCodeManager = New clsErrorCodeManager
                cErrorCodeManager.Init(_Devices, _Stations, AppSettings)
                cErrorCodeManager.LoadErrorCodeCfg()
                _Devices.Add(clsErrorCodeManager.Name, cErrorCodeManager)
            End If

            If _Devices.ContainsKey(MessageManager.Name) Then
                _MessageManager = CType(_Devices(MessageManager.Name), MessageManager)
            Else
                _MessageManager = New MessageManager(_Devices, _Stations)
                _Devices.Add(MessageManager.Name, _MessageManager)
            End If
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
            If Not UpdateMsg(PLCAlarmStation.Name) Then Return
            '==============================================================================

            Select Case _i.StepOutputNumber

                Case -100  'Init
                    _ReadStructDeviceInteraction.Clear()
                    _ManualReadStructDeviceInteraction.Clear()
                    _UIStation.AddColumns()
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

                    If iErrorCode <> 0 And iOldErrorCode <> iErrorCode Then
                        _Logger.Logger(_i, _Messager, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_CAQ_START))
                        _InternMsg = ""
                        _StationMode = 1
                        strErrorMsg = ""
                        _StartCallBack = False
                        _StartCheckTrigInfoDefineCallBack = False
                        _i.StepInputNumber = _i.StepOutputNumber + 1
                        Exit Select
                    End If


                Case 1  '判断PLC传递信息
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 2
                    iOldErrorCode = iErrorCode
                    If cErrorCodeManager.HasErrorCode(iOldErrorCode.ToString) Then
                        strErrorMsg = cErrorCodeManager.GetErrorCfgFromCode(iOldErrorCode.ToString).ActiveMessage
                    End If
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 3
                    ShowMsg(iOldErrorCode.ToString, strErrorMsg)
                    _UIStation.AddRow(iOldErrorCode.ToString, strErrorMsg)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 4
                    _i.StepInputNumber = _i.Address_Pass


                Case 1000
                    If iErrorCode = 0 Then
                        HiddenREF()
                        iOldErrorCode = 0
                        _i.StepInputNumber = _i.Address_Home
                    End If

                    If iOldErrorCode <> iErrorCode Then
                        _i.StepInputNumber = _i.Address_Home
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


    Protected Function ShowMsg(ByVal strErrorCode As String, ByVal strMsg As String) As Boolean
        _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
        _MessageManager.InsertControl(PLCAlarmStation.Name)
        If _MessageManager.LockMessage Then Return True

        Dim _mTest As String = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Alrm_MSG1, strErrorCode).Trim + vbCrLf _
                       + _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Alrm_MSG2, strMsg).Trim
        _lblRefPart.Font = New Font("Calibri", 30, FontStyle.Bold)
        _lblRefPart.Tag = enumHMI_ERROR_TYPE.MasterMessage
        _lblRefPart.Text = _mTest
        _lblRefPart.BringToFront()
        _lblRefPart.Show()
        _lblRefPart.ForeColor = ColorTranslator.FromWin32(enumLK_COLOR.LK_COLOR_RED)
        Return True
    End Function

    Protected Function HiddenREF() As Boolean
        _MessageManager.RemoveControl(PLCAlarmStation.Name)
        If _MessageManager.GetNullStatus Then
            _lblRefPart.SendToBack()
            _lblRefPart.Hide()
            _lblRefPart.Tag = enumHMI_ERROR_TYPE.None
        End If
        Return True
    End Function


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

