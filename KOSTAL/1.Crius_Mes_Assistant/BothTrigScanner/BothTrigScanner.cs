using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;

namespace MesStationCommon
{
    public class BothTrigScanner : IScanner
    {
        private SerialPort _serial1 = new SerialPort();
        private SerialPort _serial2 = new SerialPort();

        private string SCANNER1_TRIG_ON = "+";
        private string SCANNER2_TRIG_ON = "+";

        private string SCANNER1_TRIG_OFF = "-";
        private string SCANNER2_TRIG_OFF = "-";

        private Byte SCANNER1_SUFFIX = 0X0D;
        private Byte SCANNER2_SUFFIX = 0X0D;

        public void Clear()
        {
            _serial1.DiscardInBuffer();
            _serial2.DiscardInBuffer();
        }

        public string getBarcode(int nTimeout)
        {
            List<byte> data1 = new List<byte>();
            List<byte> data2 = new List<byte>();

            Clear();

            //trigg on
            _serial1.Write(SCANNER1_TRIG_ON);
            _serial2.Write(SCANNER2_TRIG_ON);

            //get barcode
            bool isBarcodeReceived = false;
            Stopwatch w = new Stopwatch();
            w.Start();
            
           // nTimeout = nTimeout > 3000? nTimeout : 3000;

            while(w.ElapsedMilliseconds < nTimeout)
            {
                byte curr = 0;

                while(_serial1.BytesToRead > 0)
                {
                    curr =(byte)_serial1.ReadByte();

                    if (curr == SCANNER1_SUFFIX)
                    {
                        isBarcodeReceived = true;
                        data2.Clear();
                        break;
                    };

                    data1.Add(curr);
                }

                if (isBarcodeReceived) break;

                while (_serial2.BytesToRead > 0)
                {
                    curr = (byte)_serial2.ReadByte();

                    if (curr == SCANNER2_SUFFIX)
                    {
                        isBarcodeReceived = true;
                        data1.Clear();
                        break;
                    };

                    data2.Add(curr);
                }

                if (isBarcodeReceived) break;
            }

            w.Stop();
            //trig off
            _serial1.Write(SCANNER1_TRIG_OFF);
            _serial2.Write(SCANNER2_TRIG_OFF);

            if(isBarcodeReceived)
            {
                string barcode = System.Text.ASCIIEncoding.ASCII.GetString(data1.Concat(data2).ToArray());
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("got barcode {0}", barcode));
                return barcode;
            }
            else
            {
                return "";
            }
        }


        private bool InitComm(string parameter, SerialPort p, int index)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Initialize the serial port with parameters {0}", parameter));
            string[] parameters = parameter.ToUpper().Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                      
            p.PortName = parameters[0];
            p.BaudRate = int.Parse(parameters[1]);
            p.DataBits = int.Parse(parameters[3]);
            p.StopBits = (StopBits)int.Parse(parameters[4]);

            //get Parity
            string[] names = Enum.GetNames(typeof(Parity));

            string parity = names.Where(x => x.StartsWith(parameters[2])).FirstOrDefault();
            if (string.IsNullOrEmpty(parity))
            {
                //log: throw new Exception("wrong parameter Parity!");
                return false;
            }

            p.Parity = (Parity)Enum.Parse(typeof(Parity), parity);

            //set trig on
            if(parameters.Length > 5)
            {
                if(index == 0)
                {
                    SCANNER1_TRIG_ON = parameters[5];
                }
                else if(index == 1)
                {
                    SCANNER2_TRIG_ON = parameters[5];
                }
            }

            if (parameters.Length > 6)
            {
                if (index == 0)
                {
                    SCANNER1_TRIG_OFF = parameters[6];
                }
                else if (index == 1)
                {
                    SCANNER2_TRIG_OFF = parameters[6];
                }
            }


            try
            {
                //set suffix
                if (parameters.Length > 7)
                {
                    if (index == 0)
                    {
                        SCANNER1_SUFFIX = byte.Parse(parameters[7], System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (index == 1)
                    {
                        SCANNER2_SUFFIX = byte.Parse(parameters[7], System.Globalization.NumberStyles.HexNumber);
                    }
                }


                p.Open();
            }
            catch (Exception e)
            {
                //log: ...
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }

            return p.IsOpen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">COM4,9600,N,8,1,+,-,0D;COM5,9600,N,8,1,+,-,0D</param>
        /// <returns></returns>
        public bool Init(string parameters)
        {
            string[] settings = parameters.Split(";".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (settings.Length < 2) return false;

            bool ret = InitComm(settings[0], _serial1,0);

            if (!ret) return false;

            ret = InitComm(settings[1], _serial2,1);
            if(!ret)
            {
                _serial1.Close();
                return false;
            }

            return _serial2.IsOpen && _serial1.IsOpen;
        }

        public bool isBarcodeReady(Func<bool> sensor = null)
        {
            if(sensor != null)
            {
                return sensor.Invoke();
            }
            else
            {
                return false;
            }
        }

        public void Quit()
        {
            if(_serial1.IsOpen)
            {
                _serial1.Close();
            }

            if (_serial2.IsOpen)
            {
                _serial2.Close();
            }

        }
    }
}
