Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Public Class clsMySqlAdapter
    Public connstr As String
    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Private _Object As New Object
    Public Function Init(ByVal strConnCmd As String)
        SyncLock _Object
            Try
                connstr = strConnCmd
                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                connection.Close()
                connection.Dispose()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function CreateDB(ByVal strCreateDatabase As String, ByVal strCheckTable As String, ByVal strCreateTable As String) As Boolean
        SyncLock _Object
            Try
                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
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
                connection.Dispose()
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function InSertData(ByVal strInserCmd As String) As Boolean
        SyncLock _Object
            Try

                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInserCmd
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                connection.Dispose()
                Return True

            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Function UpdateData(ByVal strInserCmd As String) As Boolean
        SyncLock _Object
            Try

                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInserCmd
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                connection.Dispose()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function


    Public Function DeleteData(ByVal strDelCmd As String) As Boolean
        SyncLock _Object
            Try

                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strDelCmd
                sqlCmd.CommandTimeout = 1000000
                sqlCmd.ExecuteNonQuery()
                connection.Close()
                connection.Dispose()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
            Return True
        End SyncLock
    End Function

    Public Overloads Function SelectToDataView(ByVal strInquiryCmd As String, ByRef Ds As DataSet) As Boolean
        SyncLock _Object
            Try
                Dim CommandText As String

                connection = New MySqlConnection(connstr)
                connection.Open()
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                CommandText = strInquiryCmd
                Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
                Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
                mysqlad.Fill(Ds)
                connection.Close()
                connection.Dispose()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Overloads Function GetData(ByVal strInquiryCmd As String, ByVal cTagIndex() As Integer, ByRef cTagValue() As Object) As Boolean
        SyncLock _Object
            Try
                Dim bRecorde As Boolean = False

                connection = New MySqlConnection(connstr)
                connection.Open()
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
                connection.Dispose()
                Return bRecorde
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

    Public Overloads Function GetData(ByVal strInquiryCmd As String, ByVal cTagIndex() As Integer, ByRef lValue As List(Of Object())) As Boolean
        SyncLock _Object
            Try
                Dim bRecorde As Boolean = False
                lValue.Clear()

                connection = New MySqlConnection(connstr)
                connection.Open()
                Dim cTagValue() As Object
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Connection = connection
                sqlCmd.CommandText = strInquiryCmd
                Dim dataReader As MySqlDataReader
                dataReader = sqlCmd.ExecuteReader
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        ReDim cTagValue(cTagIndex.Length - 1)

                        For i = 0 To cTagIndex.Length - 1
                            cTagValue(i) = dataReader.Item(cTagIndex(i))
                        Next
                        lValue.Add(cTagValue)
                    Loop
                End If

                dataReader.Close()
                connection.Close()
                connection.Dispose()
                Return True
            Catch ex As Exception
                Throw New clsHMIException(ex, enumExceptionType.Crash)
                Return False
            End Try
        End SyncLock
    End Function

End Class
