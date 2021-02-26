using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MesStationCommon
{
    public class HostSerialControlScanner : IScanner
    {
        private SerialPort _serial = new SerialPort();

        private const string TRIG_ON = "+";
        private const string TRIG_OFF = "";
        private const Byte SUFFIX = 0X0D;
        private int _delay_before_scan = 500; //单位ms

        private System.Diagnostics.Stopwatch _delay = new System.Diagnostics.Stopwatch();

        public void Clear()
        {
            if (_serial == null) return;

            _serial.DiscardInBuffer();
            _serial.DiscardOutBuffer();
        }

        public string getBarcode(int nTimeout)
        {
            if (_serial == null) return "";

            //wait for delay
//            while(_delay.ElapsedMilliseconds < _delay_before_scan)
//            {
//                System.Threading.Thread.Sleep(1);
//            }

            _delay.Stop();

            _serial.DiscardInBuffer();
            //trig
            _serial.Write(TRIG_ON);
//            System.Threading.Thread.Sleep(200);
//            _serial.Write(TRIG_ON);

            //Get response
            System.Diagnostics.Stopwatch w = new System.Diagnostics.Stopwatch();

            List<byte> response = new List<byte>();

            byte curr = 0;
            bool isSuffixReceived = false;

            w.Start();
            do
            {
                if (_serial.BytesToRead > 0)
                {
                    curr = (byte)_serial.ReadByte();
                    
                    if (curr == SUFFIX)
                    {
                        isSuffixReceived = true;
                        break;
                    }
					response.Add(curr);
                    
                }
                else
                {
                    System.Threading.Thread.Sleep(10);
                }

            } while (w.ElapsedMilliseconds < nTimeout);

            if (isSuffixReceived)
            {
                if(response.Count() > 0 && response[0] == 0x02)
                {
                    response.RemoveAt(0);
                }

                return System.Text.ASCIIEncoding.ASCII.GetString(response.ToArray());
            }
            else
            {
                //trig off
                //_serial.Write(TRIG_OFF);
                return "No Read";
            }
        }

        public bool Init(string parameter)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Initialize the serial port with parameters {0}", parameter));
            string[] parameters = parameter.ToUpper().Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            _serial = new SerialPort()
            {
                PortName = parameters[0],
                BaudRate = int.Parse(parameters[1]),
                DataBits = int.Parse(parameters[3]),
                StopBits = (StopBits)int.Parse(parameters[4])
            };

            if(parameters.Count() > 5)
                _delay_before_scan = int.Parse(parameters[5]);

            //get Parity
            string[] names = Enum.GetNames(typeof(Parity));

            string p = names.Where(x => x.StartsWith(parameters[2])).FirstOrDefault();
            if (string.IsNullOrEmpty(p))
            {
                //log: throw new Exception("wrong parameter Parity!");
                return false;
            }

            _serial.Parity = (Parity)Enum.Parse(typeof(Parity), p);

            try
            {
                _serial.Open();
            }
            catch (Exception e)
            {
                //log: ...
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }

            return _serial.IsOpen;
        }

        public bool isBarcodeReady(Func<bool> sensor = null)
        {
            if (sensor == null) return false;

            if(sensor.Invoke())
            {
//                _delay.Restart();
                return true;
            }

            return false;
        }

        public void Quit()
        {
            if(_serial != null)
            {
                _serial.Close();
            }
        }
    }
}
