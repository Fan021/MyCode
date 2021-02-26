Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Public Class DataCfg
    Public DBServer As String = String.Empty
    Public DBPort As String = String.Empty
    Public DBUserName As String = String.Empty
    Public DBPassWord As String = String.Empty
    Public DBName As String = String.Empty
    Public DBUserTable As String = String.Empty
    Public DBConfigTable As String = String.Empty
End Class
Public Class SessionData
    Public UserName As String = String.Empty
    Public PassWord As String = String.Empty
    Public Level As Integer = 0
End Class



Public Class UserData
    Private connstr As String

    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Public _Setting As DataCfg
    Private _StatusDescription As String
    Protected _isRun As Boolean
    Public Const Name As String = "UserData"
    Public Property isRun As Boolean
        Set(ByVal value As Boolean)
            _isRun = value
        End Set
        Get
            Return _isRun
        End Get

    End Property

    Public ReadOnly Property StatusDescription As String
        Get
            Return _StatusDescription
        End Get
    End Property
    Public Function Init(ByVal Setting As DataCfg)
        Try
            _Setting = Setting
            '  connstr = "Persist Security Info=False;database=" + Config.DBName + ";server=" + Config.DBServer + ";user id=" + Config.DBUserName + "; pwd=" + Config.DBPassWord
            connstr = "Persist Security Info=False;server=" + _Setting.DBServer + ";user id=" + _Setting.DBUserName + "; pwd=" + _Setting.DBPassWord
            connection = New MySqlConnection(connstr)
            connection.Open()
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            If Not CreatDB() Then Return False
            connection.Close()
        Catch ex As Exception
            _StatusDescription = "UserData Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Private Function CreatDB() As Boolean
        Try
            sqlCmd.CommandText = "create schema if not exists " + _Setting.DBName
            sqlCmd.ExecuteNonQuery()

            Try
                sqlCmd.CommandText = " select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "`"
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                sqlCmd.CommandText = "CREATE TABLE `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "`" & _
                               "（ `id` INT NOT NULL AUTO_INCREMENT , " & _
                                " `UserName` VARCHAR(45) NOT NULL," & _
                                " `Password` VARCHAR(45) NOT NULL," & _
                                " `Level` INT NOT NULL," & _
                                "`timespan` VARCHAR(45) NOT NULL," & _
                                " PRIMARY KEY ( `id`  )" & _
                                " ) ENGINE=MyISAM"
                sqlCmd.ExecuteNonQuery()
                InSertData("ADMIN", "apb34eol", 3)
            End Try
        Catch ex As Exception
            _StatusDescription = "UserData CreatDB Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function CheckLogin(ByVal strUserName As String, ByVal strPassword As String) As Boolean
        '#####
        Return True
        Try
            Dim bResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` where UserName = '" & strUserName & "' and Password = '" & strPassword & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                bResult = True
            Else
                bResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return bResult
        Catch ex As Exception
            _StatusDescription = "UserData CheckLogin Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        'Return True
    End Function

    Public Function GetLevel(ByVal strUserName As String, ByVal strPassword As String) As Integer
        '#####
        Return 3
        Try
            Dim iRecorde As Integer = 0
            Dim iLevel As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` where UserName = '" & strUserName & "' and Password = '" & strPassword & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dataReader.Read()
                iLevel = CInt(dataReader.Item(3))
            Else
                iLevel = 0
            End If
            dataReader.Close()
            connection.Close()
            Return iLevel
        Catch ex As Exception
            _StatusDescription = "UserData GetLevel Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return 0
        End Try
        Return 0
    End Function

    Public Function GetUserName(ByVal strUserName As String) As Boolean
        Try
            Dim bResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` where UserName = '" & strUserName & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                bResult = True
            Else
                bResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return bResult
        Catch ex As Exception
            _StatusDescription = "UserData GetUserName Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return 0
    End Function


    Public Function InSertData(ByVal strUserName As String, ByVal strPassword As String, ByVal iLevel As Integer) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "insert into `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` (`UserName`, `Password`,`Level`, `timespan`) values ('" & strUserName & "', '" & strPassword & "'," & iLevel & ", '" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "UserData InSertData Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function UpdateData(ByVal strID As String, ByVal strUserName As String, ByVal strPassword As String, ByVal iLevel As Integer) As Boolean
        Try
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "update `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` SET `Level`= " & iLevel.ToString & ", `Password`='" & strPassword & "', `timespan`='" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where id = '" & strID & "' and UserName = '" & strUserName & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "UserData UpdateData Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function DelectData(ByVal strID As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "DELETE FROM `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` where id = '" & strID & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "DelectData UpdateCount Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function


    Public Overloads Function SelectToDataSet(ByRef Ds As DataSet) As Boolean
        Try
            Dim iRecorde As Integer = 0
            Dim CommandText As String
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` order by timespan desc "
            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "UserData SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function

    Public Overloads Function SelectToDataSet(ByVal strUser As String, ByRef Ds As DataSet) As Boolean
        Try
            Dim iRecorde As Integer = 0
            Dim CommandText As String = ""
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            If strUser = "" Then
                CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` order by timespan desc "
            End If
            If strUser <> "" Then
                CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBUserTable + "` where UserName = '" & strUser & "' order by timespan desc "
            End If

            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "UserData SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function
End Class
