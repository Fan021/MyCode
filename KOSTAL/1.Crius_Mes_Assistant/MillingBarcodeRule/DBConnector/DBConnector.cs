using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public class DBConnector
    {
        private MySqlConnection _connection = null;
        private MySqlCommand sqlCmd = new MySqlCommand();
        private string _host = "localhost";
        private uint _port = 3306;
        private string _user = "root";
        private string _pwr = "apb34eol";
        private string tableSfc = "sfc_article";
        private string tableBoards = "article_boards";
        private string _dbName = "";

        public string DBName
        {
            set
            {
                _dbName = value;
            }
        }

        public bool Init()
        {
            bool bRet = true;
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = _host;
                builder.Password = _pwr;
                builder.Port = _port;
                builder.UserID = _user;

                _connection = new MySqlConnection(builder.ConnectionString);
                _connection.Open();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                if(!CreateDB())  return false;
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = false;
            }
            return bRet;
        }

        public bool CreateDB()
        {
            bool bRet = true;
            try
            {
                sqlCmd.CommandText = "create schema if not exists " + _dbName;
                sqlCmd.ExecuteNonQuery();

                try
                {
                    sqlCmd.CommandText = "select * from `" + _dbName + "`.`" + tableSfc + "`";
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.CommandText = "select * from `" + _dbName + "`.`" + tableBoards + "`";
                    sqlCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    sqlCmd.CommandText = "CREATE TABLE `" + _dbName + "`.`" + tableSfc + "`" +
                                " ( `id` INT NOT NULL AUTO_INCREMENT , " +
                                " `Article` VARCHAR(45) NOT NULL," +
                                " `sfc` VARCHAR(45) NOT NULL," +
                                " PRIMARY KEY ( `id`  )" +
                                " )";
                    sqlCmd.ExecuteNonQuery();
                    sqlCmd.CommandText = "CREATE TABLE `" + _dbName + "`.`" + tableBoards + "`" +
                                " ( `id` INT NOT NULL AUTO_INCREMENT , " +
                                " `Article` VARCHAR(45) NOT NULL," +
                                " `amountOfBoards` INT NOT NULL," +
                                "`timespan` VARCHAR(45) NOT NULL," +
                                " PRIMARY KEY ( `id`  )" +
                                " )";
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = false;
            }
            return bRet;
        }
        public bool InSertSfc(string article, string sfc)
        {
            bool bRet = true;
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                sqlCmd.CommandText = "insert into `" + _dbName + "`.`" + tableSfc + "` (`Article`, `sfc`) values ('" + article + "', '" + sfc + "')";
                sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = false;
            }
            return bRet;
        }

        public bool DeleteSfc()
        {
            bool bRet = true;
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                sqlCmd.CommandText = "DELETE FROM `" + _dbName + "`.`" + tableSfc + "`";
                sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = false;
            }
            return bRet;
        }
        public string GetArticle(string sfc)
        {
            string bRet = "";
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                sqlCmd.CommandText = "select * from `" + _dbName + "`.`" + tableSfc + "` where sfc = '" + sfc + "'";
                MySqlDataReader dataReader = null;
                dataReader = sqlCmd.ExecuteReader();

                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        bRet = dataReader.GetString(1);
                    }
                }
                else
                {
                    bRet = "";
                }
                dataReader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = "";
            }
            return bRet;
        }
        public string GetBoards(string article)
        {
            string bRet = "";
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                sqlCmd.CommandText = "select * from `" + _dbName + "`.`" + tableBoards + "` where Article = '" + article + "'";
                MySqlDataReader dataReader = null;
                dataReader = sqlCmd.ExecuteReader();

                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        bRet = dataReader.GetString(2);
                    }
                }else
                {
                    bRet = "";
                }
                dataReader.Close();
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = "-1";
            }

            return bRet;
        }
        public string GetAmountOfBoards(string sfc)
        {
            string bRet = "";
            try
            {
                string article = GetArticle(sfc);
                if(article != "")
                {
                    bRet = GetBoards(article);
                }
            }catch(Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = "";
            }
            return bRet;
        }

        public bool InSertBoards(string article, string boards)
        {
            bool bRet = true;
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }

                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Connection = _connection;
                sqlCmd.CommandText = "insert into `" + _dbName + "`.`" + tableBoards + "` (`Article`, `amountOfBoards`, `timespan`) values ('" + article + "', '" + boards + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                bRet = false;
            }
            return bRet;
        }
    }
}
