using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{

    /// <summary>
    /// 已经执行的测试序列
    /// </summary>
    public class ExceutedSequeces
    {
        /// <summary>
        /// 产品变种号
        /// </summary>
        public string article { get; set; }

        /// <summary>
        /// sfc
        /// </summary>
        public string SFC { get; set; }

        /// <summary>
        /// 原始条形码内容
        /// </summary>
        public string barcode { get; set; }

        /// <summary>
        /// 每个函数的执行结果
        /// </summary>
        public List<MesFunctionResult> MES { get; set; }

    }
}
