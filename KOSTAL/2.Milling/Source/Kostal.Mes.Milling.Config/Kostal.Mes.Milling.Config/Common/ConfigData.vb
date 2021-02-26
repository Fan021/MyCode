Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Public Class ConfigData
    Private connstr As String

    Private connection As MySqlConnection
    Private sqlCmd As New MySqlCommand
    Public _Setting As DataCfg
    Private _StatusDescription As String
    Protected _isRun As Boolean
    Public Const Name As String = "ConfigData"
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
            _StatusDescription = "ConfigData Init Fail. Message:" + ex.Message
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
                sqlCmd.CommandText = " select * from `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "`"
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                sqlCmd.CommandText = "CREATE TABLE `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "`" & _
                               "（ `id` INT NOT NULL AUTO_INCREMENT , " & _
                                " `Item` VARCHAR(255) NOT NULL," & _
                                " `Value` VARCHAR(255) NOT NULL," & _
                                "`timespan` VARCHAR(45) NOT NULL," & _
                                " PRIMARY KEY ( `id`  )" & _
                                " ) ENGINE=MyISAM"
                sqlCmd.ExecuteNonQuery()
            End Try
        Catch ex As Exception
            _StatusDescription = "ConfigData CreatDB Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function


    Public Function GetItemValue(ByVal strItem As String, ByRef strValue As String) As Boolean
        Try
            Dim bResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "` where Item = '" & strItem & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dataReader.Read()
                strValue = dataReader.Item(2).ToString
                bResult = True
            Else
                strValue = ""
                bResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return bResult
        Catch ex As Exception
            _StatusDescription = "ConfigData GetItemValue Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return False
    End Function


    Public Function CheckItem(ByVal strItem As String) As Boolean
        Try
            Dim bResult As Boolean = False
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "select * from `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "` where Item = '" & strItem & "'"
            Dim dataReader As MySqlDataReader
            dataReader = sqlCmd.ExecuteReader

            If dataReader.HasRows Then
                dataReader.Read()
                bResult = True
            Else
                bResult = False
            End If
            dataReader.Close()
            connection.Close()
            Return bResult
        Catch ex As Exception
            _StatusDescription = "ConfigData GetItemValue Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function InSertData(ByVal strItem As String, ByVal strValue As String) As Boolean
        If CheckItem(strItem) Then
            Return UpdateData(strItem, strValue)
        End If
        Return InSertItem(strItem, strValue)
    End Function
    Public Function InSertItem(ByVal strItem As String, ByVal strValue As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "insert into `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "` (`Item`, `Value`, `timespan`) values ('" & strItem & "', '" & strValue & "', '" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "')"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ConfigData InSertItem Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function UpdateData(ByVal strItem As String, ByVal strValue As String) As Boolean
        Try
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "update `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "` SET `Value`='" & strValue & "', `timespan`='" & Date.Now.ToString("yyyy-MM-dd HH:mm:ss") & "' where Item = '" & strItem & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ConfigData UpdateData Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function

    Public Function DelectData(ByVal strItem As String) As Boolean
        Try
            Dim iRecorde As Integer = 0
            If connection.State <> ConnectionState.Open Then
                connection.Open()
            End If
            sqlCmd.CommandType = CommandType.Text
            sqlCmd.Connection = connection
            sqlCmd.CommandText = "DELETE FROM `" + _Setting.DBName + "`.`" + _Setting.DBConfigTable + "` where Item = '" & strItem & "'"
            sqlCmd.ExecuteNonQuery()
            connection.Close()
            Return True
        Catch ex As Exception
            _StatusDescription = "ConfigData UpdateCount Fail. Message:" + ex.Message
            Throw New Exception(_StatusDescription)
            Return False
        End Try
        Return True
    End Function


   
End Class
