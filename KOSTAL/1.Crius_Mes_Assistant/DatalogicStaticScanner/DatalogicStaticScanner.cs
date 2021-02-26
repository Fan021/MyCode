using MesStationCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogicStaticScanner
{
    public class DatalogicStaticScanner : IScanner
    {
        private SerialPort _serial;
        Parity _Parity = Parity.None;

        private const Byte STX = 0x02;
        private const Byte ETX = 0x0d;

        public void Clear()
        {
            _serial.DiscardInBuffer();
        }
        //
        public string getBarcode(int nTimeout)
        {
            Stopwatch wh = new Stopwatch();
            string strBarcode = "";

            try
            {
            	Clear();
            	
                wh.Start();

                //wait for CR
                Byte byCurr;
                do
                {
                    if(_serial.BytesToRead > 0)
                    {
                        byCurr = (Byte)_serial.ReadByte();
                        if(byCurr == STX)
                        {
                            continue;
                        }else if(byCurr == ETX){
                        	break;
                        }
                        strBarcode = strBarcode + (Char)byCurr;
                    }
                    if(wh.ElapsedMilliseconds > nTimeout)
                    {
                        strBarcode = "";
                        break;
                    }
                } while (wh.ElapsedMilliseconds <= nTimeout);

                if(strBarcode.Length > 13)
                {
                    if (strBarcode.Contains("/P"))
                    {
                        strBarcode = strBarcode.Substring(0, strBarcode.Length - 1) + "0";
                    }
                }
                   
            }catch(Exception ex)
            {
                throw ex;
            }
            return strBarcode;
        }

        public bool Init(string paramters)
        {
            bool res = false;
            string nPort;
            int nBaudRate;

            try
            {
                string[] setting = paramters.Split(',');

                nPort = setting[0];
                nBaudRate = Convert.ToInt32(setting[1]);

                _serial = new SerialPort(nPort, nBaudRate);
                //_serial.Parity = _Parity;

                _serial.Open();

                res = true;

            }catch(Exception ex)
            {
                throw ex;
            }

            return res;
        }
        public void Quit()
        {
            try
            {
                if(_serial != null)
                {
                    if(_serial.IsOpen)
                    {
                        _serial.Close();
                    }
                    _serial = null;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool isBarcodeReady(Func<bool> sensor = null)
        {
        	if(sensor.Invoke()){
                _serial.Write("T");    	
                return true;
        	}else{
        		return false;
        	}  
        }
    }
}
