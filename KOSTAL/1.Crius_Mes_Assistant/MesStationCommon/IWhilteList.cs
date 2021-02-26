using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 白名单
    /// </summary>
    public interface IWhilteList
    {
        /// <summary>
        /// 白名单名称
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// 加载配置的白名单文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool load(string file);

        /// <summary>
        /// 是否含有变种
        /// </summary>
        /// <param name="Article"></param>
        /// <returns></returns>
        bool hasArticle(string Article);

        /// <summary>
        /// 插入一个新的变种
        /// </summary>
        /// <param name="article"></param>
        void append(WhiteItem article);

        /// <summary>
        /// 保存白名单
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool save(string file);

        /// <summary>
        /// 变种列白
        /// </summary>
        List<WhiteItem> ArticleList { get; set; }
    }
}
