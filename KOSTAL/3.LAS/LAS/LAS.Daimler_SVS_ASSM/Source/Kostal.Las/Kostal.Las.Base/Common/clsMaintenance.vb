Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Public Class clsMySqlAdapter
    Private connstr As String
    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Private _Object As New Object

    Public Function Init(ByVal strConnCmd As String) As Boolean
        SyncLock _Object
            Try
                connstr = strConnCmd
                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                connection.Close()
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function CreateDB(ByVal strCreateDatabase As String, ByVal strCheckTable As String, ByVal strCreateTable As String) As Boolean
        SyncLock _Object
            Try
                connection.Open()
                sqlCmd.CommandText = strCreateDatabase
                sqlCmd.ExecuteNonQuery()

                Try
                    sqlCmd.CommandText = strCheckTable
                    sqlCmd.ExecuteNonQuery()
                Catch ex As Exception
                    sqlCmd.CommandText = strCreateTable
                    sqlCmd.ExecuteNonQuery()
                End Try
                connection.Close()
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strInserCmd As String) As Boolean
        SyncLock _Object
            Try
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInserCmd
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                Return True

            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strInserCmd As String) As Boolean
        SyncLock _Object
            Try
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInserCmd
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function DeleteData(ByVal strDelCmd As String) As Boolean
        SyncLock _Object
            Try
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strDelCmd
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Overloads Function SelectToDataView(ByVal strInquiryCmd As String, ByRef Ds As DataSet) As Boolean
        SyncLock _Object
            Try
                Dim CommandText As String
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                CommandText = strInquiryCmd
                Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
                Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
                mysqlad.Fill(Ds)
                connection.Close()
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

    Public Overloads Function GetData(ByVal strInquiryCmd As String, ByVal cTagIndex() As Integer, ByRef cTagValue() As Object) As Boolean
        SyncLock _Object
            Try
                Dim bRecorde As Boolean = False
                If connection.State <> ConnectionState.Open Then
                    connection.Open()
                End If
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInquiryCmd
                Dim dataReader As MySqlDataReader
                dataReader = sqlCmd.ExecuteReader
                ReDim cTagValue(cTagIndex.Length - 1)
                If dataReader.HasRows Then
                    dataReader.Read()
                    For i = 0 To cTagIndex.Length - 1
                        cTagValue(i) = dataReader.Item(cTagIndex(i))
                    Next
                    bRecorde = True
                Else
                    bRecorde = False
                End If
                dataReader.Close()
                connection.Close()
                Return bRecorde
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End SyncLock
    End Function

End Class
Public Class clsMaintenance
    Private cIniHandler As New clsIniHandler
    Private _Object As New Object
    Private lListData As New Dictionary(Of String, clsMaintenanceCfg)
    Private AppSettings As Settings
    Public Function Init(ByVal MyParent As Station, ByVal _AppSettings As Settings, ByVal MyLanguage As Language) As Boolean
        SyncLock _Object
            Try
                AppSettings = _AppSettings
                lListData.Clear()
                Dim cMaintenanceCfg As clsMaintenanceCfg
                For Each element As Dictionary(Of String, Object) In cIniHandler.GetAnyListFromIni(_AppSettings.ConfigFolder + "Maintenance.ini", "Maintenance", New String() {"ID", "Station", "Component", "AlarmUpLimit", "AlarmMessageChinese", "AlarmMessageEnglish","CurrentCount"})
                    cMaintenanceCfg = New clsMaintenanceCfg
                    If CType(element("ID"), String) <> "" Then
                        cMaintenanceCfg.strID = CType(element("ID"), String)
                    End If
                    If CType(element("Station"), String) <> "" Then
                        cMaintenanceCfg.strStation = CType(element("Station"), String)
                    End If
                    If CType(element("Component"), String) <> "" Then
                        cMaintenanceCfg.strComponent = CType(element("Component"), String)
                    End If
                    If CType(element("AlarmUpLimit"), String) <> "" Then
                        cMaintenanceCfg.strAlarmUpLimit = CType(element("AlarmUpLimit"), String)
                    End If
                    If CType(element("AlarmMessageChinese"), String) <> "" Then
                        cMaintenanceCfg.strAlarmMessageChinese = CType(element("AlarmMessageChinese"), String)
                    End If
                    If CType(element("AlarmMessageEnglish"), String) <> "" Then
                        cMaintenanceCfg.strAlarmMessageEnglish = CType(element("AlarmMessageEnglish"), String)
                    End If
                    If CType(element("CurrentCount"), String) <> "" Then
                        cMaintenanceCfg.CurrentCount = CType(element("CurrentCount"), Integer)
                    End If
                    lListData.Add(cMaintenanceCfg.strID, cMaintenanceCfg)
                Next
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function ModifyData(ByVal strID As String,
                               ByVal strStation As String,
                               ByVal strComponent As String,
                               ByVal strAlarmUpLimit As String,
                               ByVal strAlarmMessageChinese As String,
                               ByVal strAlarmMessageEnglish As String,
                               ByVal strCurrentCount As String) As Boolean
        SyncLock _Object
            Try
                If lListData.ContainsKey(strID) Then
                    lListData(strID).strStation = strStation
                    lListData(strID).strComponent = strComponent
                    lListData(strID).strAlarmUpLimit = strAlarmUpLimit
                    lListData(strID).strAlarmMessageChinese = strAlarmMessageChinese
                    lListData(strID).strAlarmMessageEnglish = strAlarmMessageEnglish
                    lListData(strID).CurrentCount = strCurrentCount
                Else
                    Dim cMaintenanceCfg As New clsMaintenanceCfg
                    cMaintenanceCfg.strID = strID
                    cMaintenanceCfg.strStation = strStation
                    cMaintenanceCfg.strComponent = strComponent
                    cMaintenanceCfg.strAlarmUpLimit = strAlarmUpLimit
                    cMaintenanceCfg.strAlarmMessageChinese = strAlarmMessageChinese
                    cMaintenanceCfg.strAlarmMessageEnglish = strAlarmMessageEnglish
                    cMaintenanceCfg.CurrentCount = strCurrentCount
                    lListData.Add(cMaintenanceCfg.strID, cMaintenanceCfg)
                End If
                Return True
            Catch ex As Exception
                'Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function SaveData() As Boolean
        SyncLock _Object
            Try
                Dim i As Integer = 1
                Dim lListValue As New List(Of String)
                For Each element As clsMaintenanceCfg In lListData.Values
                    lListValue.Add("[Maintenance" + i.ToString + "]")
                    lListValue.Add("ID=" + element.strID.ToString)
                    lListValue.Add("Station=" + element.strStation.ToString)
                    lListValue.Add("Component=" + element.strComponent.ToString)
                    lListValue.Add("AlarmUpLimit=" + element.strAlarmUpLimit.ToString)
                    lListValue.Add("AlarmMessageChinese=" + element.strAlarmMessageChinese.ToString)
                    lListValue.Add("AlarmMessageEnglish=" + element.strAlarmMessageEnglish.ToString)
                    lListValue.Add("CurrentCount=" + element.CurrentCount.ToString)
                    i = i + 1
                Next
                cIniHandler.SaveIniFile(AppSettings.ConfigFolder + "Maintenance.ini", lListValue)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
            Return True
        End SyncLock
    End Function
    Public Function DeleteData() As Boolean
        SyncLock _Object
            Try
                lListData.Clear()
                Return True
            Catch ex As Exception
                'Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Overloads Function SelectToDataView(ByRef Ds As DataSet) As Boolean
        Dim dt As DataTable = New DataTable("PlcMessageTable")
        dt.Columns.Add("Station")
        dt.Columns.Add("Component")
        dt.Columns.Add("AlarmUpLimit")
        dt.Columns.Add("AlarmMessageChinese")
        dt.Columns.Add("AlarmMessageEnglish")
        dt.Columns.Add("CurrentCount")
        For Each element As clsMaintenanceCfg In lListData.Values
            dt.Rows.Add(New String() {element.strStation, element.strComponent, element.strAlarmUpLimit, element.strAlarmMessageChinese, element.strAlarmMessageEnglish, element.CurrentCount.ToString})
        Next
        Ds.Tables.Add(dt)
        Return True
    End Function
End Class


Public Class clsMaintenanceCfg
    Public strID As String = String.Empty
    Public strStation As String = String.Empty
    Public strComponent As String = String.Empty
    Public strAlarmUpLimit As String = String.Empty
    Public strAlarmMessageChinese As String = String.Empty
    Public strAlarmMessageEnglish As String = String.Empty
    Public CurrentCount As Integer = 0
End Class


