using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace MesStationCommon
{
    public class SerialPortTransmit : ITransmit
    {
        private SerialPort _comm = null;
        private string _trigString = "T";
        private string _currentReceivedString = "";
        private string _prefix = "";
        private string _suffix = "";
		private int _disableReceiveTimeAfterTrigged = 5000;
		private bool _isTrigged = false;
		
		private System.Diagnostics.Stopwatch _timeoutWatcher = null; 
        
        private Byte[] GetBytes(string data)
        {
            string[] l = data.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            List<Byte> ret = new List<byte>();
            foreach(string c in l)
            {
                ret.Add(byte.Parse(c, System.Globalization.NumberStyles.HexNumber));
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="parameter">初始化参数,比如:COM1,19200,N,8,1,+,02,0D 0A,5000;</param>
        /// <returns></returns>
        public bool Init(string parameter)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Initialize the serial port with parameters {0}", parameter));
            string[] parameters = parameter.ToUpper().Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            _comm = new SerialPort()
            {
                PortName = parameters[0],
                BaudRate = int.Parse(parameters[1]),
                DataBits = int.Parse(parameters[3]),
                StopBits = (StopBits)int.Parse(parameters[4])
            };

            if(parameters.Count() > 5)
            {
                _trigString = parameters[5];
            }

            if(parameters.Count() > 6)
            {
                _prefix = System.Text.ASCIIEncoding.ASCII.GetString(GetBytes(parameters[6]));
            }

            if(parameters.Count() > 7)
            {
                _suffix = System.Text.ASCIIEncoding.ASCII.GetString(GetBytes(parameters[7]));
            }
            
            if(parameters.Count() > 8)
            {
            	_disableReceiveTimeAfterTrigged = int.Parse(parameters[8]);
            }


            //get Parity
            string[] names = Enum.GetNames(typeof(Parity));

            string p = names.Where(x => x.StartsWith(parameters[2])).FirstOrDefault();
            if(string.IsNullOrEmpty(p))
            {                
                //log: throw new Exception("wrong parameter Parity!");
                return false;
            }

            _comm.Parity = (Parity)Enum.Parse(typeof(Parity), p);

            try
            {
            	//create watcher
            	_timeoutWatcher = new System.Diagnostics.Stopwatch();
            	           	
                _comm.Open();
                _currentReceivedString = "";
                _comm.DataReceived += _comm_DataReceived;
                
            }
            catch(Exception e)
            {
                //log: ...
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }

            return _comm.IsOpen;
        }

        private void _comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message = _comm.ReadExisting();
            
            if(_timeoutWatcher.ElapsedMilliseconds > _disableReceiveTimeAfterTrigged)
            {
            	_isTrigged = false;
            	_timeoutWatcher.Stop();
            }
            
            if(!_isTrigged)
            {
            	_currentReceivedString += message;
            }

            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("Receive message: {0}", message));
        }

        public bool isHostTrigged()
        {
            bool bTrigger = false;

            if (_currentReceivedString.Contains(_trigString))
            {
                bTrigger = true;
                _isTrigged = true;
                _timeoutWatcher.Restart();
                _currentReceivedString = "";

            }

            return bTrigger;
        }

        public void Quit()
        {
            if((_comm != null) && _comm.IsOpen)
            {
                NLog.LogManager.GetCurrentClassLogger().Info("close port");
                _comm.Close();
            }
        }

        public bool send(string content)
        {
            if (_comm == null) return false;
            if (!_comm.IsOpen) return false;

            try
            { 
            	string textToSend = _prefix + content + _suffix;
                NLog.LogManager.GetCurrentClassLogger().Info("transmit message: " + textToSend);
                
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(textToSend);
                _comm.Write(data, 0, data.Length);
                return true;
            }
            catch(Exception e)
            {
                //log..
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }
        }
    }
}
