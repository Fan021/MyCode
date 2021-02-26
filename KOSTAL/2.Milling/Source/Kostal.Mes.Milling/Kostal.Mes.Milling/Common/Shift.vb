Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Linq

Public Enum enum_ErrorCodes
    SHIFT_ERROR_FILE_ERROR = -99
    SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
    SHIFT_ERROR_INVALID_SHIFTDATACOUNT_ERROR
    SHIFT_ERROR_INVALID_SHIFTDATASORT_ERROR
    SHIFT_ERROR_INVALID_SHIFTTIME_ERROR
    SHIFT_ERROR_INVALID_SHIFTNOWTIME_ERROR
    SHIFT_ERROR_WINDOWS_ERROR
    SHIFT_ERROR_WINDOWS_TIMEER
    SHIFT_ERROR_INVALID_GETCURRENT_ERROR
End Enum
Public Class Shift
    Protected _FileHandler As New ReadIni
    Protected _ListOfShiftElementData As New Dictionary(Of String, ShiftElementData)
    Protected _ListOfShiftElement As New Dictionary(Of String, ShiftElement)
    Protected _FilePathName As String = String.Empty
    Protected _Status As enum_ErrorCodes
    Protected _StatusDescription As String = String.Empty
    Protected Const MAXSHIFT As Integer = 3
    Protected _sResult As String = String.Empty
    Protected _Timer As New System.Windows.Forms.Timer
    Protected _GetCurrentShift As Integer
    Protected _GetCurrentDate As String
    Protected _GetNowShift As Integer
    Protected _Object As New Object
    Protected Const DayStart As String = "00:00:00"
    Protected Const DayStop As String = "23:59:59"
    Public Event ShiftChange(ByRef CurShift As Integer)


    Public ReadOnly Property GetCurrentDate As String
        Get
            Return _GetCurrentDate
        End Get
    End Property

    Public ReadOnly Property GetCurrentShift As Integer
        Get
            Return _GetCurrentShift
        End Get
    End Property
    Public ReadOnly Property Status() As enum_ErrorCodes
        Get
            Return _Status
        End Get
    End Property

    Public ReadOnly Property StatusDescription() As String
        Get
            Return _StatusDescription
        End Get
    End Property

    Public Sub New(ByVal FilePathName As String)
        _FilePathName = FilePathName
        CheckAndReadShiftElementData()
        ChangeShiftElementDataToShiftElement()
        _GetCurrentShift = ReturnCurrentShift()
        _GetNowShift = _GetCurrentShift
        _Timer.Interval = 100
        AddHandler _Timer.Tick, AddressOf _TimerCB
        _Timer.Enabled = True
    End Sub

    Protected Function CheckAndReadShiftElementData() As Boolean
        Try
            _ListOfShiftElementData.Clear()
            If Not File.Exists(_FilePathName) Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_FILE_ERROR
                ThrowErrorCode(_Status, _FilePathName)
                Return False
            End If
            For i As Integer = 1 To MAXSHIFT
                _sResult = _FileHandler.Read(_FilePathName, "Start", i.ToString)
                If _sResult = ReadIni.s_DEFAULT Or _sResult = ReadIni.s_Null Then Continue For
                If CheckDateFrame(_sResult) Then
                    AddDataFrame(i.ToString, _sResult)
                Else
                    Return False
                End If
            Next
            If _ListOfShiftElementData.Count <= 0 Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATACOUNT_ERROR
                ThrowErrorCode(_Status)
                Return False
            End If

        Catch ex As Exception
            _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
            ThrowErrorCode(_Status, "CheckAndReadShiftElementData", ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Protected Function ChangeShiftElementDataToShiftElement() As Boolean
        Try
            Dim _ShiftName As String = String.Empty
            For i As Integer = 0 To _ListOfShiftElementData.Keys.Count - 1
                If i = 0 Then
                    _ShiftName = _ListOfShiftElementData(_ListOfShiftElementData.Keys(_ListOfShiftElementData.Keys.Count - 1)).ShiftName
                    _ListOfShiftElement.Add(_ListOfShiftElement.Count.ToString, New ShiftElement(_ShiftName, Shift.DayStart, _ListOfShiftElementData(_ListOfShiftElementData.Keys(i)).ShiftData))
                End If
                If i > 0 And i <= _ListOfShiftElementData.Keys.Count - 1 Then
                    _ShiftName = _ListOfShiftElementData(_ListOfShiftElementData.Keys(i - 1)).ShiftName
                    _ListOfShiftElement.Add(_ListOfShiftElement.Count.ToString, New ShiftElement(_ShiftName, _ListOfShiftElementData(_ListOfShiftElementData.Keys(i - 1)).ShiftData, _ListOfShiftElementData(_ListOfShiftElementData.Keys(i)).ShiftData))
                End If
                If i = _ListOfShiftElementData.Keys.Count - 1 Then
                    _ShiftName = _ListOfShiftElementData(_ListOfShiftElementData.Keys(i)).ShiftName
                    _ListOfShiftElement.Add(_ListOfShiftElement.Count.ToString.ToString, New ShiftElement(_ShiftName, _ListOfShiftElementData(_ListOfShiftElementData.Keys(i)).ShiftData, Shift.DayStop))
                End If
            Next
        Catch ex As Exception
            _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
            ThrowErrorCode(_Status, "ChangeShiftElementDataToShiftElement", ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Protected Function ReturnCurrentShift() As Integer
        Try
            For Each Element As ShiftElement In _ListOfShiftElement.Values
                Element.NowDay = DateTime.Parse(DateTime.Now.ToString)
                If Element.ShiftTo = Shift.DayStop Then
                    If Element.NowDay >= Element.FromWithNowday And Element.NowDay <= Element.ToWithNowday Then
                        _GetCurrentDate = DateTime.Now.ToString("yyyyMMdd")
                        Return CInt(Element.ShiftName)
                    End If
                Else
                    If Element.NowDay >= Element.FromWithNowday And Element.NowDay < Element.ToWithNowday Then
                        If Element.ShiftFrom = Shift.DayStart Then
                            _GetCurrentDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd")
                        Else
                            _GetCurrentDate = DateTime.Now.ToString("yyyyMMdd")
                        End If
                        Return CInt(Element.ShiftName)
                    End If
                End If
            Next
        Catch ex As Exception
            _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
            ThrowErrorCode(_Status, "ReturnCurrentShift", ex.Message.ToString)
            Return 0
        End Try
        _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_GETCURRENT_ERROR
        ThrowErrorCode(_Status)
        Return 0
    End Function

    Protected Function CheckDateFrame(ByVal ElementData As String) As Boolean
        Try
            Dim cData() As String = ElementData.Split(CChar(":"))
            If cData.Length <> 3 Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                ThrowErrorCode(_Status, ElementData)
                Return False
            End If

            If Not IsNumeric(cData(0)) Or Not IsNumeric(cData(1)) Or Not IsNumeric(cData(2)) Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                ThrowErrorCode(_Status, ElementData)
                Return False
            End If

            If CInt(cData(0)) < 0 Or CInt(cData(0)) >= 24 Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                ThrowErrorCode(_Status, ElementData)
                Return False
            End If

            If CInt(cData(1)) < 0 Or CInt(cData(1)) > 59 Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                ThrowErrorCode(_Status, ElementData)
                Return False
            End If

            If CInt(cData(2)) < 0 Or CInt(cData(2)) > 59 Then
                _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                ThrowErrorCode(_Status, ElementData)
                Return False
            End If
            For Each Elemment As ShiftElementData In _ListOfShiftElementData.Values
                If TimeSpan.Parse(ElementData) < TimeSpan.Parse(Elemment.ShiftData) Then
                    _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATASORT_ERROR
                    ThrowErrorCode(_Status, ElementData)
                    Return False
                End If
            Next
        Catch ex As Exception
            _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
            ThrowErrorCode(_Status, "CheckDataFrame", ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Protected Function AddDataFrame(ByVal ElementName As String, ByVal ElementData As String) As Boolean
        Try
            If Not _ListOfShiftElementData.ContainsKey(ElementName) Then
                For Each Elemment As ShiftElementData In _ListOfShiftElementData.Values
                    If Elemment.ShiftData = ElementData Then
                        Return True
                    End If
                Next
                _ListOfShiftElementData.Add(ElementName, New ShiftElementData(ElementName, ElementData))
            End If
        Catch ex As Exception
            _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
            ThrowErrorCode(_Status, "AddDataFrame", ex.Message.ToString)
            Return False
        End Try
        Return True
    End Function

    Protected Sub _TimerCB(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SyncLock _Object
            Try
                _Timer.Enabled = False
                _GetNowShift = ReturnCurrentShift()
                If _GetNowShift <> _GetCurrentShift Then
                    _GetCurrentShift = _GetNowShift
                    RaiseEvent ShiftChange(_GetCurrentShift)
                End If
                _Timer.Enabled = True
            Catch ex As Exception
                _Status = enum_ErrorCodes.SHIFT_ERROR_WINDOWS_TIMEER
                ThrowErrorCode(_Status, "_TimerCB", ex.Message.ToString)
                _Timer.Enabled = False
            End Try
        End SyncLock
    End Sub

    Protected Sub ThrowErrorCode(ByVal Status As enum_ErrorCodes, Optional ByVal AppendMsg As String = "", Optional ByVal AppendMsg1 As String = "")
        Dim _ErrorMsg As String = String.Empty
        Select Case Status
            Case enum_ErrorCodes.SHIFT_ERROR_FILE_ERROR
                _ErrorMsg = String.Format("Invalid File Name:{0}", AppendMsg)
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                _ErrorMsg = String.Format("Invalid Shift Data:{0}", AppendMsg)
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATACOUNT_ERROR
                _ErrorMsg = "Shift Data is Null.Please Add"
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATASORT_ERROR
                _ErrorMsg = String.Format("Invalid Shift Data:{0}. Error in sort", AppendMsg)
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTTIME_ERROR
                _ErrorMsg = String.Format("Invalid ShiftTime:{0}", AppendMsg)
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTNOWTIME_ERROR
                _ErrorMsg = String.Format("Invalid NowTime:{0}", AppendMsg)
            Case enum_ErrorCodes.SHIFT_ERROR_WINDOWS_ERROR
                _ErrorMsg = String.Format("Function:{0}. ErrorMsg:{1}", AppendMsg, AppendMsg1)
            Case enum_ErrorCodes.SHIFT_ERROR_WINDOWS_TIMEER
                _ErrorMsg = String.Format("Function:{0}. ErrorMsg:{1}", AppendMsg, AppendMsg1)
            Case enum_ErrorCodes.SHIFT_ERROR_INVALID_GETCURRENT_ERROR
                _ErrorMsg = String.Format("Invalid GetCurrentShift")
            Case Else
                _ErrorMsg = "UnKnow Error"
        End Select
        _StatusDescription = _ErrorMsg
        Throw New Exception(_StatusDescription)
    End Sub

    Public Overloads Function CheckRefWithNowTime(ByVal strRefTime As String, ByVal iCurrentShift As Integer) As Boolean
        SyncLock _Object
            Try
                If Not IsDate(strRefTime) Then
                    _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                    ThrowErrorCode(_Status, strRefTime)
                    Return False
                End If
                For Each Element As ShiftElement In _ListOfShiftElement.Values
                    Element.NowDay = DateTime.Parse(Date.Now.ToString)
                    If Element.ShiftTo = Shift.DayStop Then
                        If DateTime.Parse(strRefTime) >= Element.FromWithNowday And DateTime.Parse(strRefTime) <= Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    Else
                        If DateTime.Parse(strRefTime) >= Element.FromWithNowday And DateTime.Parse(strRefTime) < Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    End If
                Next
            Catch ex As Exception
                Return False
            End Try
            Return False
        End SyncLock
    End Function

    Public Overloads Function CheckRefWithNowTime(ByVal iRefTime As DateTime, ByVal iCurrentShift As Integer) As Boolean
        SyncLock _Object
            Try
                For Each Element As ShiftElement In _ListOfShiftElement.Values
                    Element.NowDay = DateTime.Parse(Date.Now.ToString)
                    iRefTime = DateTime.Parse(iRefTime.ToString)
                    If Element.ShiftTo = Shift.DayStop Then
                        If iRefTime >= Element.FromWithNowday And iRefTime <= Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    Else
                        If iRefTime >= Element.FromWithNowday And iRefTime < Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    End If
                Next
            Catch ex As Exception
                Return False
            End Try
            Return False
        End SyncLock
    End Function

    Public Overloads Function CheckRefWithDayTime(ByVal strRefTime As String, ByVal strDayTime As String, ByVal iCurrentShift As Integer) As Boolean
        SyncLock _Object
            Try
                If Not IsDate(strRefTime) Then
                    _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                    ThrowErrorCode(_Status, strRefTime)
                    Return False
                End If

                If Not IsDate(strDayTime) Then
                    _Status = enum_ErrorCodes.SHIFT_ERROR_INVALID_SHIFTDATA_ERROR
                    ThrowErrorCode(_Status, strRefTime)
                    Return False
                End If

                For Each Element As ShiftElement In _ListOfShiftElement.Values
                    Element.NowDay = DateTime.Parse(strDayTime)
                    If Element.ShiftTo = Shift.DayStop Then
                        If DateTime.Parse(strRefTime) >= Element.FromWithNowday And DateTime.Parse(strRefTime) <= Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    Else
                        If DateTime.Parse(strRefTime) >= Element.FromWithNowday And DateTime.Parse(strRefTime) < Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    End If
                Next
            Catch
                Return False
            End Try
            Return False
        End SyncLock
    End Function

    Public Overloads Function CheckRefWithDayTime(ByVal iRefTime As DateTime, ByVal iDayTime As DateTime, ByVal iCurrentShift As Integer) As Boolean
        SyncLock _Object
            Try
                For Each Element As ShiftElement In _ListOfShiftElement.Values
                    Element.NowDay = DateTime.Parse(iDayTime.ToString)
                    iRefTime = DateTime.Parse(iRefTime.ToString)
                    If Element.ShiftTo = Shift.DayStop Then
                        If iRefTime >= Element.FromWithNowday And iRefTime <= Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    Else
                        If iRefTime >= Element.FromWithNowday And iRefTime < Element.ToWithNowday And CInt(Element.ShiftName) = iCurrentShift Then
                            Return True
                        End If
                    End If
                Next
            Catch
                Return False
            End Try
            Return False
        End SyncLock
    End Function
End Class


Public Class ShiftElementData
    Protected _ShiftData As String
    Protected _ShiftName As String

    Public Property ShiftData As String
        Set(ByVal value As String)
            _ShiftData = value
        End Set
        Get
            Return _ShiftData
        End Get
    End Property

    Public Property ShiftName As String
        Set(ByVal value As String)
            _ShiftName = value
        End Set
        Get
            Return _ShiftName
        End Get
    End Property

    Sub New(ByVal ShiftName As String, ByVal ShiftData As String)
        _ShiftData = ShiftData
        _ShiftName = ShiftName
    End Sub

End Class

Public Class ShiftElement
    Protected _ShiftFrom As String
    Protected _ShiftTo As String
    Protected _ShiftName As String
    Protected _NowDay As New DateTime

    Public Property ShiftFrom As String
        Set(ByVal value As String)
            _ShiftFrom = value
        End Set
        Get
            Return _ShiftFrom
        End Get
    End Property

    Public Property ShiftTo As String
        Set(ByVal value As String)
            _ShiftTo = value
        End Set
        Get
            Return _ShiftTo
        End Get
    End Property

    Public Property ShiftName As String
        Set(ByVal value As String)
            _ShiftName = value
        End Set
        Get
            Return _ShiftName
        End Get
    End Property

    Public Property NowDay As DateTime
        Set(ByVal value As DateTime)
            _NowDay = value
        End Set
        Get
            Return _NowDay
        End Get
    End Property

    Public ReadOnly Property FromWithNowday As DateTime
        Get
            Return DateTime.Parse(_NowDay.ToString("yyyy.MM.dd") + " " + _ShiftFrom)
        End Get
    End Property
    Public ReadOnly Property ToWithNowday As DateTime
        Get
            Return DateTime.Parse(_NowDay.ToString("yyyy.MM.dd") + " " + _ShiftTo)
        End Get
    End Property


    Sub New()
    End Sub
    Sub New(ByVal ShiftName As String, ByVal ShiftFrom As String, ByVal ShiftTo As String)
        _ShiftName = ShiftName
        _ShiftFrom = ShiftFrom
        _ShiftTo = ShiftTo
    End Sub

    Public Shared Operator <>(ByVal x As ShiftElement, ByVal y As ShiftElement) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        Return x.ShiftName <> y.ShiftName
    End Operator
    Public Shared Operator =(ByVal x As ShiftElement, ByVal y As ShiftElement) As Boolean
        If x Is Nothing Or y Is Nothing Then Return False
        Return x.ShiftName = y.ShiftName
    End Operator
    Public Function Clone() As ShiftElement
        If Me Is Nothing Then Return Nothing
        Dim t As New ShiftElement
        t.ShiftFrom = Me._ShiftFrom
        t.ShiftTo = Me._ShiftTo
        t.ShiftName = Me._ShiftName
        Return t
    End Function
End Class

Public Class ReadIni
    Protected Shared objGetPrivateProfileString As New Object
    Protected Const i_MAX_CHAR As Integer = 256
    Public Const s_DEFAULT As String = "#ERROR#"
    Public Const s_Null As String = ""

    <DllImport("kernel32", EntryPoint:="GetPrivateProfileStringA", CharSet:=CharSet.Ansi)> _
    Protected Shared Function GetPrivateProfileStringKey( _
                                                        ByVal s_Section As String, _
                                                        ByVal s_KeyWord As String, _
                                                        ByVal s_Default As String, _
                                                        ByVal sb_Result As StringBuilder, _
                                                        ByVal l_MaxChar As Int32, _
                                                        ByVal s_FileName As String _
                                                        ) As Int32

    End Function

    <DllImport("kernel32", EntryPoint:="WritePrivateProfileStringA", CharSet:=CharSet.Ansi)> _
    Protected Shared Function WritePrivateProfileString( _
                                                        ByVal s_Section As String, _
                                                        ByVal s_KeyWord As String, _
                                                        ByVal s_Entry As String, _
                                                        ByVal s_FileName As String _
                                                        ) As Int32

    End Function

    Public Function Read(ByVal CompleteFileName As String, ByVal s_Section As String, ByVal s_KeyWord As String) As String
        Dim i_StringLenght As Integer, l_Pos As Integer, sb_Result As New StringBuilder(256), s_Result As String

        SyncLock objGetPrivateProfileString
            i_StringLenght = GetPrivateProfileStringKey(s_Section, s_KeyWord, s_DEFAULT, sb_Result, i_MAX_CHAR, CompleteFileName)
        End SyncLock

        s_Result = LSet(sb_Result.ToString, i_StringLenght)
        l_Pos = InStr(s_Result, ",")

        If l_Pos = 0 Then
            'No Comment found
            Return s_Result
        Else
            Return Trim(LSet(s_Result, l_Pos - 1))
        End If

    End Function

End Class