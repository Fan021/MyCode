using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// MES Sequence 
    /// </summary>
    public interface IMesFuction
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 参数列表
        /// </summary>
        Dictionary<string, object> paramters { get; }

        /// <summary>
        /// 执行
        /// </summary>
        /// <returns>返回一个动态的结果</returns>
        dynamic Execute();

        /// <summary>
        /// 执行结果，是否执行成功
        /// </summary>
        /// <returns></returns>
        bool Result();

    }
}
