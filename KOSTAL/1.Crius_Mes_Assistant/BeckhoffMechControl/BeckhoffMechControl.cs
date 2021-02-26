using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MesStationCommon
{
    /// <summary>
    /// Beck模块的方案
    /// </summary>
    public class BeckhoffMechControl : IMechControl
    {

    	private List<MechIOItem> _instances = new List<MechIOItem>();
        private TwinCAT.Ads.TcAdsClient _client = new TwinCAT.Ads.TcAdsClient();

        private Dictionary<string, string> _variables = new Dictionary<string, string>();
        private Dictionary<string, int> _handles = new Dictionary<string, int>();

        /// <summary>
        /// 初始化参数,包含所有需要用到的变量的名称：类似Json字符串 按照以下顺序.按照变量名称访问
        /// 
        ///     'amsaddress' : '',
        ///     'amsport' : '',
        ///     'entryside_hold' : '',
        ///     'exitside_hold' : '',
        ///     'entryside_sensor' : '',
        ///     'exitside_sensor' : '',
        ///     'entryside_go' : '',
        ///     'exitside_go' : '',
        /// 
        /// </summary>
        /// <param name="parameter">
        /// 输入的参数列表
        /// 'amsaddress' : '169.254.157.10',
        /// 'amsport' : '801',
        /// 'entryside_hold' : 'entryside_hold',
        /// 'exitside_hold' : 'exitside_hold',
        /// 'entryside_sensor' : 'entryside_sensor',
        /// 'exitside_sensor' : 'exitside_sensor',
        /// 'entryside_motor' : 'entryside_busy',
        /// 'exitside_motor' : 'exitside_busy',  
		///         
        /// </param> 
        /// <returns></returns>
        public bool init(string parameter)
        {
            string[] parameters = parameter.Replace("'","").Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries); 

            foreach(var p in parameters)
            {
                string[] curr = p.Split(":".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                
                if(curr.Length >= 2 )
                {
                    _variables.Add(curr[0], curr[1]);
                }
                else
                {
                	_variables.Add(curr[0],"");
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
                	if(string.IsNullOrEmpty(k.Value)) continue;
                	   
                    int h = _client.CreateVariableHandle(k.Value);
                    _handles.Add(k.Key, h);   
                    if(k.Key == "entryside_hold")
                    {
                    	_instances.Add(new MechIOItem(){
                    	               	name = "入口挡停",
                    	               	type = MechIOItem.IOType.Output,
                    	               	get = new Func<bool>(get_entryside_hold),
                    	               	set = new Action<bool>(set_entryside_hold)
                    	               }                    	              
                    	              );
                    }
                    else if(k.Key == "exitside_hold")
                    {
                    	_instances.Add(new MechIOItem(){
                    	               	name = "出口挡停",
                    	               	type = MechIOItem.IOType.Output,
                    	               	get = new Func<bool>(get_exitside_hold),
                    	               	set = new Action<bool>(set_exitside_hold)
                    	               }                    	              
                    	              );
                    }
                    else if(k.Key == "entryside_sensor")
                    {
                    	_instances.Add(new MechIOItem(){
                    	               	name = "入口传感器",
                    	               	type = MechIOItem.IOType.Input,
                    	               	get = new Func<bool>(get_entryside_sensor),
                    	               	set = null
                    	               }                    	              
                    	              );
                    }
                    else if(k.Key == "exitside_sensor")
                    {
                    	_instances.Add(new MechIOItem(){
                    	               	name = "出口传感器",
                    	               	type = MechIOItem.IOType.Input,
                    	               	get = new Func<bool>(get_exitside_sensor),
                    	               	set = null
                    	               }                    	              
                    	              );
                    }
                    else if (k.Key == "entryside_motor")
                    {
                        _instances.Add(new MechIOItem()
                        {
                            name = "入口电机",
                            type = MechIOItem.IOType.Output,
                            get = new Func<bool>(get_entryside_motor),
                            set = new Action<bool>(set_entryside_motor),
                        }
                                      );
                    }
                    else if (k.Key == "exitside_motor")
                    {
                        _instances.Add(new MechIOItem()
                        {
                            name = "出口电机",
                            type = MechIOItem.IOType.Output,
                            get = new Func<bool>(get_exitside_motor),
                            set = new Action<bool>(set_exitside_motor),
                        }
                                      );
                    }
                }
                
                 return true;
            }
            catch(Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e,e.Message);
                return false;
            }
        }

        public bool get_entryside_sensor()
        {
            if (_handles.ContainsKey("entryside_sensor"))
                return (bool)_client.ReadAny(_handles["entryside_sensor"], typeof(bool));
            else
                return false;
        }

        public bool get_exitside_sensor()
        {
            if (_handles.ContainsKey("exitside_sensor"))
                return (bool)_client.ReadAny(_handles["exitside_sensor"], typeof(bool));
            else
                return false;
        }

        public void set_entryside_hold(bool isHold)
        {
            if (_handles.ContainsKey("entryside_hold"))
                _client.WriteAny(_handles["entryside_hold"], isHold);   
        }

        public void set_exitside_hold(bool isHold)
        {
            if (_handles.ContainsKey("exitside_hold"))
                _client.WriteAny(_handles["exitside_hold"], isHold);
        }

        public void set_entryside_motor(bool isRunning)
        {
            if(_handles.ContainsKey("entryside_motor"))
            {
            	_client.WriteAny(_handles["entryside_motor"], isRunning); 
            }
        }

        public void set_exitside_motor(bool isRunning)
        {
            if (_handles.ContainsKey("exitside_motor"))
            {
                _client.WriteAny(_handles["exitside_motor"], isRunning);
            }
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

        public bool get_exitside_hold()
        {
            if (_handles.ContainsKey("exitside_hold"))
            {
                return (bool)_client.ReadAny(_handles["exitside_hold"], typeof(bool));
            }
            else
            {
                return false;
            }
        }

        public bool get_entryside_motor()
        {
            if (_handles.ContainsKey("entryside_motor"))
            {
                return (bool)_client.ReadAny(_handles["entryside_motor"], typeof(bool));
            }
            else
            {
                return false;
            }
        }

        public bool get_exitside_motor()
        {
            if (_handles.ContainsKey("exitside_motor"))
            {
                return (bool)_client.ReadAny(_handles["exitside_motor"], typeof(bool));
            }
            else
            {
                return false;
            }
        }

        public void entryside_go()
        {
            //release
            set_entryside_hold(false);

            Thread.Sleep(200);

            set_entryside_motor(true);
            
            while(get_entryside_sensor())
            {
                Thread.Sleep(50);
            }

            //get config 
            string delay = System.Configuration.ConfigurationManager.AppSettings["PanelReleaseSleep"];
            int value = 0;
            if(int.TryParse(delay, out value))
            {
                Thread.Sleep(value);
            }
            else
            {
                //delay 500 ms
                Thread.Sleep(500);
            }

            set_entryside_motor(false);
            set_entryside_hold(true);
        }

        public void exitside_go()
        {
            //release
            set_exitside_hold(false);
            Thread.Sleep(200);

            set_exitside_motor(true);

            while (get_exitside_sensor())
            {
                Thread.Sleep(50);
            }

            //delay 500 ms
            Thread.Sleep(500);

            set_exitside_motor(false);
            set_exitside_hold(true);

        }
     

        List<MechIOItem> IMechControl.getIOPair()
        {
        	return _instances;
        }

    
    }
}
