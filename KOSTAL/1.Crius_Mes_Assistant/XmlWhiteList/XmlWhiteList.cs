using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace MesStationCommon
{
    /// <summary>
    /// 白名单功能
    /// </summary>
    public class XmlWhiteList : IWhilteList
    {
        private string _name = "";
        private List<WhiteItem> _articles = new List<WhiteItem>(); 

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public List<WhiteItem> ArticleList {
            get {
                return _articles;
            }
            set {
                _articles = value; 
            }
        }

        public bool hasArticle(string Article)
        {
            return _articles.Where(x => x.articleNo.Trim() == Article.Trim()).Count() == 1;
        }

        public void append(WhiteItem article)
        {
            _articles.Add(article);
        }

        public bool load(string file)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("load white list from configuration file {0}", file));
            XmlSerializer serialer = new XmlSerializer(typeof(XmlWhiteList));

            try
            {
                if (File.Exists(file))
                {
                    using (var stream = new StreamReader(file))
                    {
                        var obj = (serialer.Deserialize(stream) as XmlWhiteList);

                        this._articles = obj.ArticleList;
                        this.name = obj.name;
                    }

                    return true;
                }
            }
            catch(Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
            }

            return false;
        }

        public bool save(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XmlWhiteList));

            try
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("save white list to configuration file {0}", file));
                using (var stream = new StreamWriter(file))
                {
                      serializer.Serialize(stream, this);
                }

                return true;
            }
            catch(Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }         
        }
    }
}
