
Imports System.Reflection

Public Class DebugBackup


    'Public Key_Station01 As String = enum_STATION_KEY.Station01.ToString
    'Public Key_Station02 As String = enum_STATION_KEY.Station02.ToString
    'Public Key_Station03 As String = enum_STATION_KEY.Station03.ToString
    'Public Key_Table As String = enum_STATION_KEY.Table.ToString
    'Public Key_MainControl As String = enum_STATION_KEY.MainControl.ToString
    'Public Key_System As String = enum_STATION_KEY.System.ToString


    'Dim t As Type = GetType(StationInformation)

    'Dim sp As FieldInfo() = t.GetFields()

    'For Each f As FieldInfo In sp

    '    If f.IsDefined(GetType(ColumnAttribute), True) Then
    '        Try

    '            Dim o As Object = f.GetValue(Nothing)
    '            _columnKeyList.Add(o.ToString())

    '        Catch ex As Exception
    '            Dim strErr As String = ex.Message

    '        End Try
    '    End If
    'Next


End Class



'Public NotInheritable Class EnumeratorMember

'    Private _columnKeyList As New List(Of String)

'    Private _instance As StationInformation
'    Public Sub New(ByVal instance As StationInformation)

'        _instance = instance

'        ScanVariablesOnce()

'    End Sub

'    Public Function GetUserDefinedKeys() As IEnumerable(Of String)

'        Return _columnKeyList

'    End Function


'    Private Sub ScanVariablesOnce()

'        'Dim t As Type = GetType(StationInformation)

'        Try


'            'Dim t As System.Type = _instance.GetType()
'            Dim t As Type = GetType(StationInformation)

'            Dim sp As PropertyInfo() = t.GetProperties(BindingFlags.Instance Or BindingFlags.Public)

'            For Each p As PropertyInfo In sp
'                For Each arrtri As Object In p.GetCustomAttributes(True)
'                    If TypeOf arrtri Is Attribute Then
'                        _columnKeyList.Add(p.Name)
'                    End If
'                Next
'            Next

'        Catch ex As Exception

'        End Try

'        'For Each f As FieldInfo In sp

'        '    If f.IsDefined(GetType(ColumnAttribute), True) Then
'        '        Try

'        '            Dim o As Object = f.GetValue(Nothing)
'        '            _columnKeyList.Add(o.ToString())

'        '        Catch ex As Exception
'        '            Dim strErr As String = ex.Message

'        '        End Try
'        '    End If
'        'Next

'    End Sub



'    'Public Function AddRow(ByVal strSN As String, ByVal strArticle As String, ByVal strCustomer As String, ByVal strProductFamily As String, ByVal bResult As Boolean) As Boolean

'    '    dgView.Rows.Add()
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("SN").Value = strSN
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("Article").Value = strArticle
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("Customer").Value = strCustomer
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("ProductFamily").Value = strProductFamily
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("Result").Value = bResult.ToString
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("Result").Style.BackColor = CType((IIf(bResult, System.Drawing.Color.LightGreen,
'    '                                      System.Drawing.Color.Red)), Drawing.Color)
'    '    dgView.Rows(dgView.Rows.Count - 1).Cells("Time").Value = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
'    '    dgView.Sort(dgView.Columns("Time"), ComponentModel.ListSortDirection.Descending)
'    '    Return True
'    'End Function

'    'Private Function Readfield(channel As Channel, field As String) As Object
'    '    Dim res As Object = Nothing
'    '    Dim currName As String = ""
'    '    Dim o As Object = channel.StoredValue

'    '    Dim strArr As String() = o.ToString.Split(CChar(";"))
'    '    Dim str As String = ""

'    '    Dim fields As New List(Of String)
'    '    Try


'    '        If channel IsNot Nothing Then
'    '            For Each str In strArr
'    '                If str.Contains(field) Then
'    '                    strArr = str.Split(CChar("="))
'    '                    str = strArr(1)
'    '                    Return str
'    '                End If
'    '            Next

'    '            fields.AddRange(field.Split(CChar(".")))
'    '        End If

'    '        If fields.Count = 1 Then

'    '            If o.ToString.Contains(field) Then

'    '            End If

'    '            Return o.GetType.GetField(fields(0)).GetValue(o)
'    '        Else
'    '            'Dim currObj As Object = o.GetType.GetField(fields(0)).GetValue(o)
'    '            fields.RemoveAt(0)
'    '            Return Readfield(channel, field)
'    '        End If

'    '        Return Nothing
'    '    Catch ex As Exception
'    '        Throw ex
'    '    End Try
'    'End Function

'    'Private Sub Writefield(channel As Channel, field As String, value As Object)
'    '    Dim res As Object = Nothing
'    '    Dim currName As String = ""
'    '    Dim o As Object = channel.Value

'    '    Dim fields As New List(Of String)

'    '    If channel IsNot Nothing Then

'    '        fields.AddRange(field.Split(CChar(".")))

'    '        'Get field type
'    '        Dim fi As FieldInfo = o.GetType.GetField(fields(0))

'    '        'convert data to target type
'    '        Dim p1 = System.Convert.ChangeType(value, fi.FieldType)

'    '        'write value to filed
'    '        Dim v As ValueType = CType(o, ValueType)

'    '        fi.SetValue(v, p1)

'    '        channel.StoredValue = v

'    '    Else
'    '        Dim f As FieldInfo = o.GetType.GetField(fields(0))

'    '        Dim curr As ValueType = CType(f.GetValue(o), ValueType)

'    '        fields.RemoveAt(0)

'    '        Writefield(channel, field, value)

'    '        'set o back
'    '        Dim obj As ValueType = CType(o, ValueType)
'    '        f.SetValue(obj, curr)

'    '    End If

'    '    channel.Value = channel.StoredValue

'    'End Sub

'    Private Structure DG_STATION_CONSTANTS

'        Public Const Number As String = "Number"
'        Public Const StationName As String = "StationName"
'        Public Const StationEnable As String = "StationEnable"
'        Public Const PlcStatus As String = "PlcStatus"
'        Public Const StepNumber As String = "StepNumber"
'        Public Const AutoManual As String = "AutoManual"
'        Public Const TestmanStatus As String = "TestmanStatus"
'        Public Const TestmanPercent As String = "TestmanPercent"
'        Public Const Result As String = "Result"

'    End Structure

'    '    _dgViewColumnList.Add(DG_STATION_CONSTANTS.Number)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.StationName)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.StationEnable)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.PlcStatus)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.StepNumber)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.AutoManual)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.TestmanStatus)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.TestmanPercent)
'    '_dgViewColumnList.Add(DG_STATION_CONSTANTS.Result)

'End Class


'Dim stInfo As New StationInformation

'Dim enmtor As New EnumeratorMember(New StationInformation(enum_STATION_KEY.System))

'enmtor.GetUserDefinedKeys()


'    Private Shared _columnKeyList As New List(Of String)

'    Public Sub New()

'        ScanVariablesOnce()

'    End Sub

'    Public Shared Function GetUserDefinedKeys() As IEnumerable(Of String)

'        Return _columnKeyList

'    End Function


'    Private Shared Sub ScanVariablesOnce()

'        Dim t As Type = GetType(T)

'        Dim sp As FieldInfo() = t.GetFields()

'        For Each f As FieldInfo In sp

'            If f.IsDefined(GetType(ColumnAttribute), True) Then
'                Try

'                    Dim o As Object = f.GetValue(Nothing)
'                    _columnKeyList.Add(o.ToString())

'                Catch ex As Exception
'                    Dim strErr As String = ex.Message

'                End Try
'            End If
'        Next

'    End Sub