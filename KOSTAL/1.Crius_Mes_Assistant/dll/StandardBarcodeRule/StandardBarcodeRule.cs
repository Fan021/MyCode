using System;
using System.Collections.Generic;

namespace MesStationCommon
{
    /// <summary>
    /// 标准barcode条码规则
    /// BARCODE: _P10333257_SNYB560SHR06550
    /// </summary>
    public class StandardBarcodeRule : IBarcodeRule
    {
        public string name
        {
            get { return "StandardBarcodeRule"; }
        }

        public string getArticle(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract article from barcode {0}", barcode));
            if (string.IsNullOrEmpty(barcode))
            {
                return "";
            }

            string[] items = barcode.Split(new string[]{"/P","/SN"}, StringSplitOptions.RemoveEmptyEntries);
            if(items.Length >= 2)
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("article is {0}", items[0]));
                return items[0];
            }

            //not a standard barcode of pcb
            return "";
        }

        public string getSfc(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract sfc from barcode {0}", barcode));

            if (string.IsNullOrEmpty(barcode))
            {
                return "";
            }else{
            	NLog.LogManager.GetCurrentClassLogger().Info(string.Format("sfc is {0}", barcode));
            	return barcode;
            }
        }
    }
}
