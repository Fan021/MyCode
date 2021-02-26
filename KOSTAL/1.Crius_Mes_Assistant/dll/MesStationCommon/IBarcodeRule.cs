using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 条码编码规则，定义如何获取变种号和序列号
    /// </summary>
    public interface IBarcodeRule
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        string name { get;}

        /// <summary>
        /// 获取sfc
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="mesFunctions"></param>
        /// <returns></returns>
        string getSfc(string barcode, Dictionary<string, IMesFuction> mesFunctions);

        /// <summary>
        /// 获取变种号
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        string getArticle(string barcode, Dictionary<string, IMesFuction> mesFunctions);
    }
}
