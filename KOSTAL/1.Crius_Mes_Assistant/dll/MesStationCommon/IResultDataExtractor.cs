using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 测试结果解析,需要输入文件名称作为
    /// </summary>
    public interface IResultDataExtractor
    {        
        /// <summary>
        /// 整个产品是否测试OK
        /// </summary>
        bool isPartOk { get;}

        /// <summary>
        /// 序列号
        /// </summary>
        string sfc { get; }

        /// <summary>
        /// 加载结果文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool Load(string file);

        /// <summary>
        /// 参数信息
        /// </summary>
        List<KeyValue> parameters { get; }

        List<IResultDataExtractor> subPanels { get; }
    }
}
