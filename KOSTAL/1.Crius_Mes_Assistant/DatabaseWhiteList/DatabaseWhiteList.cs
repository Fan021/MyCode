using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using LM = NLog.LogManager;
using System.Linq;

namespace MesStationCommon
{
    public class DatabaseWhiteList : MesStationCommon.IWhilteList
    {
        string _connection = null;
        //List<WhiteItem> _items = new List<WhiteItem>();

        public string name { get => "DatabaseWhiteList"; set { }}
        public List<WhiteItem> ArticleList {
            get
            {
                List<WhiteItem> list = new List<WhiteItem>();
                OleDbCommand cmd = null;
                OleDbDataReader reader = null;

                using (var con = new OleDbConnection(_connection))
                {
                    try
                    {
                        con.Open();

                        cmd = new OleDbCommand(string.Format("SELECT * FROM WhiteList"), con);

                        reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            list.Add(new WhiteItem()
                            {
                                articleNo = reader.GetString(0),
                                BarcodeRule = reader.GetString(3),
                                entryside_sequence = reader.GetString(1),
                                exitside_sequence = reader.GetString(2),
                            });
                        }                       

                    }
                    catch (Exception e)
                    {
                        LM.GetCurrentClassLogger().Error(e, e.Message);
                    }
                    finally
                    {
                        reader?.Close();
                        cmd?.Dispose();                     
                        con.Close();
                    }


                    return list;
                }
            }

            set {

            }}

        public void append(WhiteItem article)
        {            
           if(ArticleList.Where(x=> x.articleNo == article.articleNo).Count() == 0)
            {               

                using (var con = new OleDbConnection(_connection))
                {
                    try
                    {
                        con.Open();

                        var cmd = new OleDbCommand(string.Format("INSERT INTO WhiteList VALUES('{0}','{1}','{2}','{3}')", article.articleNo,article.entryside_sequence,
                            article.exitside_sequence,article.BarcodeRule), con);

                        int count = (int)cmd.ExecuteNonQuery();

                    }
                    catch (Exception e)
                    {
                        LM.GetCurrentClassLogger().Error(e, e.Message);
                        
                    }
                    finally
                    {
                        con.Close();
                    }
                }

            }

        }

        public bool hasArticle(string Article)
        {
            using (var con = new OleDbConnection(_connection))
            {
                try
                {
                    con.Open();

                    var cmd = new OleDbCommand(string.Format("SELECT Count(*) FROM WhiteList WHERE articleNo = '{0}'", Article), con);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                    
                }
                catch(Exception e)
                {
                    LM.GetCurrentClassLogger().Error(e, e.Message);
                    return false;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file">target connection string of database</param>
        /// <returns></returns>
        public bool load(string file)
        {
            _connection = file;
            return true;
        }

        public bool save(string file)
        {
            //read only   
            return true;
        }
    }
}
