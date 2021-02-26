Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class SMTStore
    Private connstr As String

    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Private _Setting As Settings
    Private _StatusDescription As String
    Protected _isRun As Boolean
    Public Const Name As String = "SMTStore"
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
    Public Function Init(ByVal Setting As Settings)
        Try
            _Setting = Setting
            '  connstr = "Persist Security Info=False;database=" + Config.DBName + ";server=" + Config.DBServer + ";user id=" + Config.DBUserName + "; pwd=" + Config.DBPassWord
            connstr = "Persist Security Info=False;server=" + _Setting.DataStoreCfg.DBServer + ";user id=" + _Setting.DataStoreCfg.DBUserName + "; pwd=" + _Setting.DataStoreCfg.DBPassWord
            connection = New MySqlConnection(connstr)
            connection.Open()
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            If Not CreatDB() Then Return False
            connection.Close()
        Catch ex As Exception
            _StatusDescription = "SMTStore Init Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Private Function CreatDB() As Boolean
        Try
            sqlCmd.CommandText = "create schema if not exists " + _Setting.DataStoreCfg.DBName
            sqlCmd.ExecuteNonQuery()

            Try
                sqlCmd.CommandText = " select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "`"
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                sqlCmd.CommandText = "CREATE TABLE `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "`" & _
                               "（ `id` INT NOT NULL AUTO_INCREMENT , " & _
                                " `SMTNumber` VARCHAR(45) NOT NULL," & _
                                " `Article` VARCHAR(45) NOT NULL," & _
                                "`timespan` VARCHAR(45) NOT NULL," & _
                                " PRIMARY KEY ( `id`  )" & _
                                " ) ENGINE=MyISAM"
                sqlCmd.ExecuteNonQuery()
            End Try
        Catch ex As Exception
            _StatusDescription = "SMTStore CreatDB Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetArticle(ByVal strSMTNumber As String, ByRef strArticle As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where SMTNumber = '" & strSMTNumber & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                Do While dataReader.Read
                    strArticle = dataReader.Item(2)
                    iRecorde = iRecorde + 1
                Loop
            Else
                strArticle = ""
            End If
            dataReader.Close()
            connection.Close()
            If strArticle = "" Then
                Return False
            End If
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetArticle(ByVal strSMTNumber As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where SMTNumber = '" & strSMTNumber & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dataReader.Close()
                connection.Close()
                Return False
            End If
            dataReader.Close()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetArticle2(ByVal strID As String, ByVal strSMTNumber As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where id = '" & strID & "' and SMTNumber = '" & strSMTNumber & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dataReader.Close()
                connection.Close()
                Return False
            End If
            dataReader.Close()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function
    Public Function InsertSMTNumber(ByVal strSMTNumber As String, ByVal strArticle As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "insert into `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` (`SMTNumber`, `Article`, `timespan`) values ('" & strSMTNumber & "', '" & strArticle & "', '" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore InsertSMTNumber Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function UpdateSMTNumber(ByVal strID As String, ByVal strSMTNumber As String, ByVal strArticle As String) As Boolean
        Try
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "update `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` SET `Article`= '" & strArticle & "', `timespan`='" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where id = '" & strID & "' and SMTNumber = '" & strSMTNumber & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore UpdateSMTNumber Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function DelectSMTNumber(ByVal strID As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "DELETE FROM `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where id = '" & strID & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore DelectSMTNumber Fail. Message:" + ex.Message
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
            CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` order by timespan desc "
            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function

    Public Overloads Function SelectToDataSet(ByVal strID As String, ByVal strSMTNumber As String, ByRef Ds As DataSet) As Boolean
        Try
            Dim iRecorde As Integer = 0
            Dim CommandText As String = ""
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            If strID = "" And strSMTNumber = "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` order by timespan desc "
            End If
            If strID = "" And strSMTNumber <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where SMTNumber = '" & strSMTNumber & "' order by timespan desc "
            End If
            If strID <> "" And strSMTNumber = "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where id = '" & strID & "' order by timespan desc "
            End If

            If strID <> "" And strSMTNumber <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBSMTTable + "` where id = '" & strID & "' and SMTNumber = '" & strSMTNumber & "' order by timespan desc "
            End If


            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "SMTStore SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function
End Class
