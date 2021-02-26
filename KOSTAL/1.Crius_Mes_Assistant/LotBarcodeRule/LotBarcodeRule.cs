using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MesStationCommon
{
    /// <summary>
    /// 从托盘的tag获取产品的变种号的sfc
    /// </summary>
    public class LotBarcodeRule : MesStationCommon.IBarcodeRule
    {
        const string SLOT_FUCTION_NAME = "GetProcessLotDetails";
        const string ARTICLE_FUNCTION_NAME = "GetSfcStatus";

        const string PROCESS_LOT = "processLot";
        const string SFC = "sfc";

        public string name {get{return "LotBarcodeRule"; }}
        

        /// <summary>
        /// 条用Barcode的函数获取
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="mesFunctions"></param>
        /// <returns></returns>
        public string getArticle(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract article from barcode {0}", barcode));

            string dutSfc = getDUTsfc(getSfc(barcode, mesFunctions), mesFunctions);

            if (mesFunctions.ContainsKey(ARTICLE_FUNCTION_NAME))
            {
                //execute
                IMesFuction instance = mesFunctions[ARTICLE_FUNCTION_NAME];

                if(instance.paramters.ContainsKey(SFC))
                {
                    instance.paramters[SFC] = dutSfc;
                }
                else
                {
                    instance.paramters.Add(SFC, dutSfc);
                }

                try
                {
                    var response = instance.Execute();

                    if (instance.Result())
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info(string.Format("article is {0}", response["materialId"]));
                        return response["materialId"];
                    }
                    else
                    {
                        return "";
                    }
                }
                catch(Exception e)
                {
                    //log...
                    NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                    return "";
                }
                finally
                {
                    
                }

            }
            else
            {
                return "";
            }
        }

        private string getDUTsfc(string carrier, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract sfc from barcode {0}", carrier));

            if (string.IsNullOrEmpty(carrier)) return "";

            if (mesFunctions.ContainsKey(SLOT_FUCTION_NAME))
            {
                //execute
                IMesFuction instance = mesFunctions[SLOT_FUCTION_NAME];

                if (instance.paramters.ContainsKey(PROCESS_LOT))
                {
                    instance.paramters[PROCESS_LOT] = carrier;
                }
                else
                {
                    instance.paramters.Add(PROCESS_LOT, carrier);
                }

                try
                {
                    var response = instance.Execute();

                    if (instance.Result())
                    {
                        var SFC = response["sfc"];
                        NLog.LogManager.GetCurrentClassLogger().Info(string.Format("sfc is {0}", SFC));
                        return SFC;
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                    //log...
                    return "";
                }

            }
            else
            {
                return "";
            }
        }

        public string getSfc(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            //直接返回原始sfc

            int index = barcode.IndexOf("/KPL");

            if(index >=0)
            {
                barcode = barcode.Substring(index + "/KPL".Length);
            }

            return barcode;
        }
    }
}
