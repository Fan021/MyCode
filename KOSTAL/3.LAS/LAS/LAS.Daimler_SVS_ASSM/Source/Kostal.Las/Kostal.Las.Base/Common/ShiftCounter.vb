Imports System.Windows.Forms
Imports System.Drawing
Public Class ShiftCounter
    Implements IDisposable
    Private IsDisposed As Boolean

    Private _Logger As Logger
    Private AppSettings As Settings
    Private _Language As Language
    Private _FileHandler As New FileHandler
    Private _FileName_Data As String

    Private _i As New Station

    Private _CounterPass As Decimal
    Private _CounterFail As Decimal

    Private WithEvents _Shift As Shift


    Public Sub New(ByVal myParent As Station, ByVal _AppSettings As Settings, ByVal Language As Language)

        Dim Result As String = ""

        AppSettings = _AppSettings
        _Language = Language

        _i.Name = "ShiftCounter"
        _i.IdString = myParent.IdString + "_" + _i.Name

        _Logger = New Logger(AppSettings)
        _Shift = New Shift(AppSettings.ConfigFolder + "lkshift.ini")

        _FileName_Data = _AppSettings.ApplicationName + "_" + _i.Name

        Result = _FileHandler.ReadIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass")
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_INIT, "PASS", Result, ""), "ShirtCounter.Init")

        If Not IsNumeric(Result) Then
            _CounterPass = 0
        Else

            Try
                _CounterPass = CDec(Result)

            Catch ex As Exception
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_INIT, "PASS", "FAIL", Result + ex.Message), "ShirtCounter.Init")

            End Try
        End If

        Result = _FileHandler.ReadIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail")
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_INIT, "FAIL", Result, ""), "ShirtCounter.Init")

        If Not IsNumeric(Result) Then
            _CounterFail = 0
        Else

            Try
                _CounterFail = CDec(Result)
            Catch ex As Exception
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_INIT, "FAIL", "FAIL", Result + ex.Message), "ShirtCounter.Init")

            End Try
        End If
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_INIT, _i.Name, "Successful"), "ShirtCounter.Init")

    End Sub


    Public Sub Dispose() Implements IDisposable.Dispose

        On Error Resume Next

        _FileHandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass", _CounterPass.ToString)
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_WRITE, "PASS", _CounterPass.ToString, ""), "ShirtCounter.Dispose")

        _FileHandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail", _CounterFail.ToString)
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_WRITE, "FAIL", _CounterFail.ToString, ""), "ShirtCounter.Dispose")
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_DISPOSE))

        _i = Nothing
        AppSettings = Nothing
        _Logger = Nothing

        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If
    End Sub


    Protected Overrides Sub Finalize()
        IsDisposed = True
        MyBase.Finalize()
    End Sub


    Private Sub _Shift_ShiftChange(ByRef CurShift As Integer) Handles _Shift.ShiftChange
        _FileHandler.WriteLogFile(AppSettings.LogFolder, AppSettings.ApplicationName + "_" + _i.IdString, "Pass;" + _CounterPass.ToString)
        _FileHandler.WriteLogFile(AppSettings.LogFolder, AppSettings.ApplicationName + _i.IdString, "Fail;" + _CounterFail.ToString)
        _CounterPass = 0
        _CounterFail = 0
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_CHANGE, CurShift.ToString), "ShiftCounter.Shift_ShiftChange")
    End Sub


    Public Sub PassCounterInc()
        Try
            _CounterPass = _CounterPass + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_ADD, "PASS", "FAIL", ex.Message), "ShiftCounter.PassCounterInc")
            _CounterPass = 0
        End Try
    End Sub


    Public Sub FailCounterInc()
        Try
            _CounterFail = _CounterFail + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_ShirtCOUNTER_ADD, "FAIL", "FAIL", ex.Message), "ShiftCounter.PassCounterInc")
            _CounterFail = 0
        End Try
    End Sub

End Class


Public Class SurfaceCounter

    Implements IDisposable

    Private IsDisposed As Boolean

    Private _i As New Station
    Private AppSettings As Settings
    Private _Language As Language

    Private _Logger As Logger
    Private _Filehandler As New FileHandler
    Private _FileName_Data As String

    Private WithEvents _btnReset As Button
    Private WithEvents _btnResetFail As Button
    Private _lblPass As Label
    Private _lblFail As Label
    Private _lblCommon As Label

    Private _decPass As Decimal
    Private _decFail As Decimal
    Private _decCommon As Decimal

    Private _WithShiftCounter As Boolean
    Private _ShiftCounter As ShiftCounter
    Private WithEvents _Shift As Shift
    Private _MyParent As Station

    Public Event CounterChanged()

    Public Sub New(ByVal MyParent As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language,
     ByVal lblPass As Label, ByVal lblFail As Label, ByVal lblCommon As Label,
     ByVal btnReset As Button, ByVal btnResetFail As Button, ByVal WithShiftCounter As Boolean)

        _i.Name = "SurfaceCounter"
        _i.IdString = MyParent.IdString + "_" + _i.Name + "_" + Format(Date.Now, "yyyyMMdd")
        AppSettings = _AppSettings
        _Language = MyLanguage
        _MyParent = MyParent
        _Logger = New Logger(AppSettings)

        _btnReset = btnReset
        _lblCommon = lblCommon
        _lblPass = lblPass
        _lblFail = lblFail
        _Shift = New Shift(AppSettings.ConfigFolder + "lkshift.ini")
        Init()
        _WithShiftCounter = WithShiftCounter
        _btnResetFail = btnResetFail
        If _WithShiftCounter Then
            _ShiftCounter = New ShiftCounter(MyParent, AppSettings, MyLanguage)
        End If
    End Sub


#Region " IDisposable Support "


    Public Sub Dispose() Implements IDisposable.Dispose

        On Error Resume Next
        _i.IdString = _MyParent.IdString + "_" + _i.Name + "_" + Format(Date.Now, "yyyyMMdd")
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Global", CStr(_decCommon))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass", CStr(_decPass))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail", CStr(_decFail))
        _Logger.Logger(_i, "Common >; " + _decCommon.ToString + " Pass > ;" + _decPass.ToString + " Fail > ;" + _decFail.ToString, "Dispose")
        If Not _ShiftCounter Is Nothing Then
            _ShiftCounter.Dispose()
            _ShiftCounter = Nothing
        End If

        _i = Nothing
        AppSettings = Nothing
        _Logger = Nothing

        If Not IsDisposed Then
            GC.SuppressFinalize(Me)
            Finalize()
        End If

    End Sub


    Protected Overrides Sub Finalize()
        IsDisposed = True
        MyBase.Finalize()
    End Sub


#End Region


#Region "Properties of Publicity"

    Public ReadOnly Property Pass As Decimal
        Get
            Return _decPass
        End Get
    End Property

    Public ReadOnly Property Fail As Decimal
        Get
            Return _decFail
        End Get
    End Property

#End Region

    Private Sub Init()
        Dim sResult As String

        _FileName_Data = AppSettings.ApplicationName + "_" + _i.Name

        sResult = _Filehandler.ReadIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Global")
        If Not IsNumeric(sResult) Then
            _decCommon = 0
        Else
            Try
                _decCommon = CType(sResult, Decimal)
            Catch ex As Exception
                _decCommon = 0
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_INIT, "Total", "FAIL", sResult + ex.Message), "SurfaceCounter.Init")
            End Try
        End If

        sResult = _Filehandler.ReadIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass")

        If Not IsNumeric(sResult) Then
            _decPass = 0
        Else
            Try
                _decPass = CType(sResult, Decimal)
            Catch ex As Exception
                _decPass = 0
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_INIT, "PASS", "FAIL", sResult + ex.Message), "SurfaceCounter.Init")
            End Try
        End If

        sResult = _Filehandler.ReadIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail")

        If Not IsNumeric(sResult) Then
            _decPass = 0
        Else
            Try
                _decFail = CType(sResult, Decimal)
            Catch ex As Exception
                _decFail = 0
                _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_INIT, "FAIL", "FAIL", sResult + ex.Message), "SurfaceCounter.Init")
            End Try
        End If
        FormControl()
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_INIT_PASS, _decCommon.ToString, _decPass.ToString, _decFail.ToString), "SurfaceCounter.Init")
    End Sub


    Public Sub PassCounterInc()
        Try
            _decCommon = _decCommon + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_ADD, "Total", "FAIL", ex.Message), "SurfaceCounter.PassCounterInc")
            _decCommon = 0
        End Try

        Try
            _decPass = _decPass + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_ADD, "PASS", "FAIL", ex.Message), "SurfaceCounter.PassCounterInc")
            _decPass = 0
        End Try
        FormControl()
    End Sub


    Public Sub FailCounterInc()
        Try
            _decCommon = _decCommon + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_ADD, "Total", "FAIL", ex.Message), "SurfaceCounter.PassCounterInc")
            _decCommon = 0
        End Try

        Try
            _decFail = _decFail + 1
        Catch ex As Exception
            _Logger.ThrowerNoStation(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_ADD, "FAIL", "FAIL", ex.Message), "SurfaceCounter.PassCounterInc")
            _decFail = 0
        End Try
        FormControl()
    End Sub


    Private Sub _Shift_ShiftChange(ByRef CurShift As Integer) Handles _Shift.ShiftChange
        _i.IdString = _MyParent.IdString + "_" + _i.Name + "_" + Format(Date.Now, "yyyyMMdd")
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Global", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail", CStr(0))
        _decCommon = 0
        _decFail = 0
        _decPass = 0
        RaiseEvent CounterChanged()

        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_RESET, _decPass.ToString, _decFail.ToString), "SurfaceCounter.Shift_ShiftChange")
        FormControl()
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_CHANGE, CurShift.ToString), "SurfaceCounter.Shift_ShiftChange")
    End Sub

    Private Sub FormControl()
        If Not IsNothing(_lblCommon) Then
            _lblCommon.BackColor = Color.White
            _lblCommon.Text = CStr(_decCommon)
        End If

        If Not IsNothing(_lblPass) Then
            _lblPass.BackColor = Color.White
            _lblPass.Text = CStr(_decPass)
        End If

        If Not IsNothing(_lblFail) Then
            _lblFail.BackColor = Color.White
            _lblFail.Text = CStr(_decFail)
            _lblFail.ForeColor = Color.Red
        End If

    End Sub


    Private Sub _btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnReset.Click
        _i.IdString = _MyParent.IdString + "_" + _i.Name + "_" + Format(Date.Now, "yyyyMMdd")
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_RESET, _decPass.ToString, _decFail.ToString), "SurfaceCounter._btnReset_Click")
        _decCommon = 0
        _decPass = 0
        _decFail = 0
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Global", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail", CStr(0))
        FormControl()

        RaiseEvent CounterChanged()
    End Sub

    Private Sub _btnResetFail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnResetFail.Click
        _i.IdString = _MyParent.IdString + "_" + _i.Name + "_" + Format(Date.Now, "yyyyMMdd")
        _Logger.Logger(_i, _Language.LanguageElement.GetTextLine(_i, enumLK_TEXT.LK_TEXT_SURFACECOUNTER_RESET, _decPass.ToString, _decFail.ToString), "SurfaceCounter._btnReset_Click")
        '_decCommon = 0
        '_decPass = 0
        _decFail = 0
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Global", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Pass", CStr(0))
        _Filehandler.WriteIniFile(AppSettings.CounterFolder, _FileName_Data, _i.IdString, "Fail", CStr(0))
        FormControl()

        RaiseEvent CounterChanged()
    End Sub

End Class

