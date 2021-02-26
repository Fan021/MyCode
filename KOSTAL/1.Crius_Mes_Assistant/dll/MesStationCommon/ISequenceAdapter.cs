using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 测试序列适配器
    /// </summary>
    public interface ISequenceAdapter
    {
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool load(string file);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool save(string file);

        /// <summary>
        /// 确定某个产品是否已经执行某个MES函数
        /// </summary>
        /// <param name="sfc"></param>
        /// <param name="fuction"></param>
        /// <returns></returns>
        bool isSequeceExecuted(string sfc, string fuctionName);


        bool addSequence(string sfc, string functionName);

        bool deleteSequence(string sfc, string functionName);


        void clear();

        /// <summary>
        /// 获取某个产品已经执行的测试序列
        /// </summary>
        /// <param name="sfc">产品</param>
        /// <returns></returns>
        List<string> ExecutedSequences(string sfc);

    }
}
