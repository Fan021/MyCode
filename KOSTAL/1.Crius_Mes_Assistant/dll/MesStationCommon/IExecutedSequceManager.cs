using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 控制如何显示已经执行的产品序列
    /// </summary>
    public interface IExecutedSequceManager
    {
        /// <summary>
        /// 根据输入条件获取已经执行的册数序列
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <returns></returns>
        List<ExceutedSequeces> getSequences(Predicate<ExceutedSequeces> predicate);
        ExceutedSequeces getSequences(string sfc, int MaxCount = 4);
        void addExecutedMesfunction(string sfc, string article, string barcode, string function, bool? result);
        void clear(string sfc);
        void clearAll();

        /// <summary>
        /// 从文件加载状态
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool load(string file);

        /// <summary>
        /// 保存状态
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool save(string file);
        
    }
}
