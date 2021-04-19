﻿Imports Kostal.Las.ArticleProvider
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

Public Class MulitPrinter
    Inherits StationTypeBase
    Implements INotifyPropertyChanged
    Protected GetMulitSNDefine As IGetMulitSNDefine
    Protected RunMulitSNDefine As IRunMulitSNDefine
    Protected ReprintMulitDefine As IReprintMulitDefine
    Protected strStationName As String = ""
    Public Const Name As String = "MulitPrinter"
    Public Event PropertyChanged(sender As Object, ByVal e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged
    Dim StationTypeBase As IStationTypeBase
    Dim StationBase As StationTypeBase
    Private Filds() As String
    Protected _lblRefPart As Label
    Protected IMainForm As IMainForm
    Private Appsetting As Settings

    Public Sub New(ByVal SubStationCfg As SubStationCfg, ByVal Devices As Dictionary(Of String, Object), ByVal Stations As Dictionary(Of String, IStationTypeBase), GetMulitSNDefine As IGetMulitSNDefine, RunMulitSNDefine As IRunMulitSNDefine, ReprintMulitDefine As IReprintMulitDefine, ByVal lblRefPart As Label, Optional ByVal CheckTrigInfo As ICheckTrigInfo = Nothing, Optional ByVal BeforStepLine As IBeforeStepDefine = Nothing, Optional ByVal AfterStepLine As IAfterStepDefine = Nothing)
        MyBase.New(SubStationCfg, Devices, Stations, BeforStepLine, AfterStepLine)
        Try
            _UI = New MulitPrinterUI
            _CheckTrigInfo = CheckTrigInfo
            _lblRefPart = lblRefPart
            Me.GetMulitSNDefine = GetMulitSNDefine
            Me.RunMulitSNDefine = RunMulitSNDefine
            Me.ReprintMulitDefine = ReprintMulitDefine
            IMainForm = Devices("mMainForm")
            Appsetting = Devices(Settings.Name)
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
                    GetMulitSNDefine.GetAllFieldsOfMulitSN(_i, _LocalArticle, _Devices, _Stations, Filds)
                    _i.StepInputNumber = _i.StepOutputNumber + 1


                Case 4
                    ShowUI()
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 5
                    RunMulitSNDefine.RunAllFieldsOfMulitSN(_i, _LocalArticle, _Devices, _Stations, Filds)
                    _i.StepInputNumber = _i.StepOutputNumber + 1

                Case 6
                    _i.StepInputNumber = _i.Address_Pass



                '回写PLC Pass
                Case 1000
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strSerialNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_SERIAL_NUMBER).Data
                    _ReadStructDeviceInteraction.stuPlcArticleSet.strKostalNr = _LocalArticle.ArticleElements(KostalArticleKeys.KEY_ID).Data
                    UpdateStructDeviceInteractionPassStep1()

                Case 1001
                    UpdateStructDeviceInteractionPassStep2()

                Case 2000
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


    Protected Function ShowUI() As Boolean

        _lblRefPart.Tag = enumHMI_ERROR_TYPE.None
        _lblRefPart.Font = New Font("Calibri", 50.25, FontStyle.Bold)
        _lblRefPart.BringToFront()
        _lblRefPart.Controls.Clear()
        Dim cTableControl As New HMITableLayoutPanel
        cTableControl.ColumnCount = Filds.Length
        cTableControl.RowCount = 1
        cTableControl.Parent = _lblRefPart
        cTableControl.BackColor = Color.White
        _lblRefPart.Controls.Add(cTableControl)
        cTableControl.Dock = DockStyle.Fill
        For iCnt = 0 To Filds.Length - 1
            Dim cChildrenControl As New HMITableLayoutPanel
            cChildrenControl.ColumnCount = 1
            cChildrenControl.RowCount = 3
            cChildrenControl.Parent = cTableControl
            cChildrenControl.Dock = DockStyle.Fill
            cTableControl.Controls.Add(cChildrenControl, iCnt Mod 6, Math.Floor(iCnt / 6))
            Dim cLabel As New Label
            cLabel.Text = Filds(iCnt)
            cLabel.TextAlign = ContentAlignment.MiddleCenter
            cLabel.Font = New Font("Calibri", 12, FontStyle.Bold)
            cLabel.Dock = DockStyle.Fill
            cChildrenControl.Controls.Add(cLabel, 0, 0)
            Dim cButton As New Button
            cButton.Text = _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_Print_MSG2).Trim
            cButton.Dock = DockStyle.Fill
            cButton.Tag = Filds(iCnt)
            cButton.Font = New Font("Calibri", 10)
            AddHandler cButton.Click, AddressOf Button_Click
            cChildrenControl.Controls.Add(cButton, 1, 0)
            Dim cPic As New PictureBox
            cPic.Dock = DockStyle.Fill
            cPic.BackgroundImage = System.Drawing.Image.FromFile(Appsetting.PicFolder + "\\Barcode.jpg")
            cPic.BackgroundImageLayout = ImageLayout.Zoom
            cChildrenControl.Controls.Add(cPic, 2, 0)
        Next

        _lblRefPart.Show()
        _lblRefPart.BackColor = Color.White
        Return True
    End Function


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, Button).Enabled = False
        ReprintMulitDefine.ReprintFields(_i, _LocalArticle, _Devices, _Stations, CType(sender, Button).Tag)
        CType(sender, Button).Enabled = True
    End Sub
End Class