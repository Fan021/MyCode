Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Public Class ArticleStore
    Private connstr As String

    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Private _Setting As Settings
    Private _StatusDescription As String
    Protected _isRun As Boolean
    Public Const Name As String = "ArticleStore"
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
            _StatusDescription = "ArticleStore Init Fail. Message:" + ex.Message
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
                sqlCmd.CommandText = " select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "`"
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                sqlCmd.CommandText = "CREATE TABLE `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "`" &
                           "（ `id` INT NOT NULL AUTO_INCREMENT , " &
                            " `Article` VARCHAR(45) NOT NULL," &
                            "`timespan` VARCHAR(45) NOT NULL," &
                            " PRIMARY KEY ( `id`  )" &
                            " ) ENGINE=MyISAM"
                sqlCmd.ExecuteNonQuery()
            End Try
        Catch ex As Exception
            _StatusDescription = "ArticleStore CreatDB Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function CheckArticle(ByVal strArticle As String) As Boolean
        Try
            Dim dResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where Article = '" & strArticle & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dResult = True
            Else
                dResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return dResult
        Catch ex As Exception
            _StatusDescription = "ArticleStore CheckArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function CheckArticle(ByVal strID As String, ByVal strArticle As String) As Boolean
        Try
            Dim dResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where id = '" & strID & "' and Article = '" & strArticle & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dResult = True
            Else
                dResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return dResult
        Catch ex As Exception
            _StatusDescription = "ArticleStore CheckArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function InSertArticle(ByVal strArticle As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "insert into `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` (`Article`, `timespan`) values ('" & strArticle & "', '" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ArticleStore InSertArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function UpdateArticle(ByVal strID As String, ByVal strArticle As String) As Boolean
        Try
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "update `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` SET  `Article`= '" & strArticle & "', `timespan`='" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where id = '" & strID & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ArticleStore UpdateArticle Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function DelectArticle(ByVal strID As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "DELETE FROM `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where id = '" & strID & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ArticleStore DelectArticle Fail. Message:" + ex.Message
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
            CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` order by timespan desc "
            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ArticleStore SelectToDataSet Fail. Message:" + ex.Message
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
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` order by timespan desc "
            End If
            If strID = "" And strArticle <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where Article = '" & strArticle & "' order by timespan desc "
            End If
            If strID <> "" And strArticle = "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where id = '" & strID & "' order by timespan desc "
            End If

            If strID <> "" And strArticle <> "" Then
                CommandText = "select * from `" + _Setting.DataStoreCfg.DBName + "`.`" + _Setting.DataStoreCfg.DBArticleTable + "` where id = '" & strID & "' and Article = '" & strArticle & "' order by timespan desc "
            End If


            Dim mysqlad As MySqlDataAdapter = New MySqlDataAdapter(CommandText, connection)
            Dim sb1 As MySqlCommandBuilder = New MySqlCommandBuilder(mysqlad)
            mysqlad.Fill(Ds)
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ArticleStore SelectToDataSet Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
    End Function

End Class
