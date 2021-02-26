using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public class BeckhoffInOutMechControl : IMechControl
    {
        private List<MechIOItem> _instances = new List<MechIOItem>();
        private TwinCAT.Ads.TcAdsClient _client = new TwinCAT.Ads.TcAdsClient();

        private Dictionary<string, string> _variables = new Dictionary<string, string>();
        private Dictionary<string, int> _handles = new Dictionary<string, int>();


        public void entryside_go()
        {
            //release
            set_entryside_hold(false);

            Thread.Sleep(200);

            set_entryside_motor(true);

            //waiting for output sensor....
            while (!get_entryside_sensor_out())
            {
                Thread.Sleep(50);
            }

            set_entryside_motor(false);
            set_entryside_hold(true);
        }

        public void exitside_go()
        {
            //do nothing
        }




        public List<MechIOItem> getIOPair()
        {
            return _instances;
        }

        public bool get_entryside_sensor()
        {
            if (_handles.ContainsKey("entryside_sensor_in"))
                return (bool)_client.ReadAny(_handles["entryside_sensor_in"], typeof(bool));
            else
                return false;
        }

        public bool get_entryside_out_sensor()
        {
            if (_handles.ContainsKey("entryside_sensor_out"))
                return (bool)_client.ReadAny(_handles["entryside_sensor_out"], typeof(bool));
            else
                return false;
        }

        public bool get_entryside_hold()
        {
            if (_handles.ContainsKey("entryside_hold"))
            {
                return (bool)_client.ReadAny(_handles["entryside_hold"], typeof(bool));
            }
            else
            {
                return false;
            }
        }


        public  bool init(string parameter)
        {
            string[] parameters = parameter.Replace("'", "").Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var p in parameters)
            {
                string[] curr = p.Split(":".ToArray(), StringSplitOptions.RemoveEmptyEntries);

                if (curr.Length >= 2)
                {
                    _variables.Add(curr[0], curr[1]);
                }
                else
                {
                    _variables.Add(curr[0], "");
                }
            }

            if ((!_variables.ContainsKey("amsaddress")) && (!_variables.ContainsKey("amsport"))) return false;

            _client.Connect(_variables["amsaddress"], int.Parse(_variables["amsport"]));

            _variables.Remove("amsaddress");
            _variables.Remove("amsport");

            //create handle list
            try
            {
                foreach (var k in _variables)
                {
                    if (string.IsNullOrEmpty(k.Value)) continue;

                    int h = _client.CreateVariableHandle(k.Value);
                    _handles.Add(k.Key, h);
                    if (k.Key == "entryside_hold")
                    {
                        _instances.Add(new MechIOItem()
                        {
                            name = "入口挡停",
                            type = MechIOItem.IOType.Output,
                            get = new Func<bool>(get_entryside_hold),
                            set = new Action<bool>(set_entryside_hold)
                        }
                                      );
                    }
                  
                    else if (k.Key == "entryside_sensor_in")
                    {
                        _instances.Add(new MechIOItem()
                        {
                            name = "进入传感器",
                            type = MechIOItem.IOType.Input,
                            get = new Func<bool>(get_entryside_sensor),
                            set = null
                        }
                                      );
                    }
                  
                    else if (k.Key == "entryside_sensor_out")
                    {
                        _instances.Add(new MechIOItem()
                        {
                            name = "送出传感器",
                            type = MechIOItem.IOType.Input,
                            get = new Func<bool>(get_entryside_sensor_out),
                            set = null
                        }
                                      );
                    }
                  
                }

                return true;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }
        }

        public void set_entryside_hold(bool isHold)
        {
            if (_handles.ContainsKey("entryside_hold"))
                _client.WriteAny(_handles["entryside_hold"], isHold);
        }


        public bool get_entryside_sensor_out()
        {
            if (_handles.ContainsKey("entryside_sensor_out"))
                return (bool)_client.ReadAny(_handles["entryside_sensor_out"], typeof(bool));
            else
                return false;
        }

        public void set_exitside_hold(bool isHold)
        {
            
        }

        public bool get_exitside_sensor()
        {
            return false;
        }

        public void set_entryside_motor(bool isRunning)
        {
           
        }

        public void set_exitside_motor(bool isRunning)
        {
            
        }
    }
}
