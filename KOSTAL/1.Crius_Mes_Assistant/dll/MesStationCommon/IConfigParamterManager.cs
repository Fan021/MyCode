using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public interface IConfigParamterManager
    {
        /// <summary>
        /// 白名单名称
        /// </summary>
        string name { get; }

        /// <summary>
        /// 加载配置的白名单文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool load(string file);


        /// <summary>
        /// 插入一个新的变种
        /// </summary>
        /// <param name="article"></param>
        void append(ConfigParameter article);

        /// <summary>
        /// 保存白名单
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool save(string file);

        /// <summary>
        /// 参数列表
        /// </summary>
        List<ConfigParameter> parameters { get; set; }

        /// <summary>
        /// 生成名称和值的对
        /// </summary>
        /// <returns></returns>
        List<KeyValue> toKeyValuePairs(bool isgnoreEmptyValue);
    }
}
