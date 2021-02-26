using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public class MillingBarcodeRule : IBarcodeRule
    {
        public DBConnector _dbConnect = null;
        public AmountOfBoards _amountOfBoards = null;
        public MillingBarcodeRule()
        {
            try
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Init Database {0}", System.Configuration.ConfigurationManager.AppSettings["AmountOfBoardsDB"]));
                _dbConnect = new DBConnector();
                _dbConnect.DBName = System.Configuration.ConfigurationManager.AppSettings["AmountOfBoardsDB"];
                if (_dbConnect.Init())
                {
                    _dbConnect.DeleteSfc();
                }
            }catch(Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, ex.Message);
                return;
            }
        }

        public string name
        {
            get
            {
                return "MillingBarcodeRule";
            }
        }

        public string getArticle(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract article from barcode {0}", barcode));
            if (string.IsNullOrEmpty(barcode))
            {
                return "";
            }

            string[] items = barcode.Split(new string[] { "/P", "/SN" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length >= 2)
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("article is {0}", items[0]));
                //set sfc
                 items[1] = items[1].Substring(0,items[1].Length-1) +"0";
                if (_dbConnect.InSertSfc(items[0], items[1]))
                {
                    //get amountofboards
                    string boards = _dbConnect.GetBoards(items[0]);
                    if(boards == "")
                    {
                        _amountOfBoards = new AmountOfBoards();
                        do
                        {
                            _amountOfBoards.ShowDialog();
                            Thread.Sleep(1);
                        } while (_amountOfBoards.DialogResult != System.Windows.Forms.DialogResult.OK);
                        if (_amountOfBoards.boards != "")
                        {
                            if (!_dbConnect.InSertBoards(items[0], _amountOfBoards.boards))
                            {
                                return "";
                            }
                        }
                    }
                    else if(boards =="-1")
                    {
                        return "";
                    }
                }

                return items[0];
            }
          
            return "";
        }

        public string getSfc(string barcode, Dictionary<string, IMesFuction> mesFunctions)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Extract sfc from barcode {0}", barcode));

            if (string.IsNullOrEmpty(barcode))
            {
                return "";
            }
            else
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("sfc is {0}", barcode));
                barcode = barcode.Substring(0,barcode.Length-1) +"0";
                return barcode;
            }
        }
    }

}
