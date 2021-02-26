Imports System.Data.OleDb
Imports MySql.Data.MySqlClient

Public Class PLCStore
    Private connstr As String

    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Private _Setting As Settings
    Private _StatusDescription As String
    Protected _isRun As Boolean
    Public Const Name As String = "PLCStore"
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
            _StatusDescription = "PLCStore Init Fail. Message:" + ex.Message
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
                sqlCmd.CommandText = " select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "`"
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                sqlCmd.CommandText = "CREATE TABLE `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "`" & _
                               "（ `id` INT NOT NULL AUTO_INCREMENT , " & _
                                " `Article` VARCHAR(45) NOT NULL," & _
                                " `PLC` VARCHAR(45) NOT NULL," & _
                                "`timespan` VARCHAR(45) NOT NULL," & _
                                " PRIMARY KEY ( `id`  )" & _
                                " ) ENGINE=MyISAM"
                sqlCmd.ExecuteNonQuery()
            End Try
        Catch ex As Exception
            _StatusDescription = "PLCStore CreatDB Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetPLC(ByVal strArticle As String, ByRef strPLCNumber As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where Article = '" & strArticle & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                Do While dataReader.Read
                    strPLCNumber = dataReader.Item(2)
                    iRecorde = iRecorde + 1
                Loop
            Else
                strPLCNumber = ""
            End If
            dataReader.Close()
            connection.Close()
            If strArticle = "" Then
                Return False
            End If
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetPLC(ByVal strArticle As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where Article = '" & strArticle & "'"
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
            _StatusDescription = "PLCStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function GetPLC2(ByVal strID As String, ByVal strArticle As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where id = '" & strID & "' and Article = '" & strArticle & "'"
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
            _StatusDescription = "PLCStore GetArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function
    Public Function InsertPLC(ByVal strArticle As String, ByVal strPLC As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "insert into `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` (`Article`, `PLC`, `timespan`) values ('" & strArticle & "', '" & strPLC & "', '" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore InsertSMTNumber Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function UpdatePLC(ByVal strID As String, ByVal strArticle As String, ByVal strPLC As String) As Boolean
        Try
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "update `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` SET `PLC`= '" & strPLC & "', `timespan`='" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where id = '" & strID & "' and Article = '" & strArticle & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore UpdateSMTNumber Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function DelectPLC(ByVal strID As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "DELETE FROM `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where id = '" & strID & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore DelectSMTNumber Fail. Message:" + ex.Message
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
            CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` order by timespan desc "
            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function

    Public Overloads Function SelectToDataSet(ByVal strID As String, ByVal strArticle As String, ByRef Ds As DataSet) As Boolean
        Try
            Dim iRecorde As Integer = 0
            Dim CommandText As String = ""
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            If strID = "" And strArticle = "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` order by timespan desc "
            End If
            If strID = "" And strArticle <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where Article = '" & strArticle & "' order by timespan desc "
            End If
            If strID <> "" And strArticle = "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where id = '" & strID & "' order by timespan desc "
            End If

            If strID <> "" And strArticle <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBPLCTable + "` where id = '" & strID & "' and Article = '" & strArticle & "' order by timespan desc "
            End If


            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "PLCStore SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function
End Class
