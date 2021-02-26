using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 白名单条目
    /// </summary>
    /// 
    [Serializable]
    public class WhiteItem
    {
        /// <summary>
        /// 变种号
        /// </summary>
        public string articleNo { get; set; }

        /// <summary>
        /// 入口端执行序列;针对该变种,如果为空,则执行全局的Sequence
        /// </summary>
        public string entryside_sequence { get; set; }

        /// <summary>
        /// 出口端执行序列,如果为空,则执行全局的Sequence
        /// </summary>
        public string exitside_sequence { get; set; }

        /// <summary>
        /// 条形码处理规则
        /// </summary>
        public string BarcodeRule { get; set; }


    }
}
