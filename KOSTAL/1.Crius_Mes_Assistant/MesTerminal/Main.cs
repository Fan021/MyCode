using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Threading;

namespace AppDemo
{
    public partial class MainForm : Form
    {
        private CefSharp.WinForms.ChromiumWebBrowser br = null;
        ManualResetEvent _loadEvent = new ManualResetEvent(false);

        public MainForm()
        {
            //初始化主页面程序
            string url = Application.StartupPath + @"\..\html\Main.html";

            br = new CefSharp.WinForms.ChromiumWebBrowser(url);

            InitializeComponent();

            br.Parent = this;
            br.Visible = true;
            br.Dock = DockStyle.Fill;

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            //注册js对象          
            RegisterJsObjects();
            br.FrameLoadEnd += Br_FrameLoadEnd;
            //InitializeComponent();

        }


        public void RegisterJsObjects()
        {
            //注册你的js对象
            br.RegisterAsyncJsObject("form_async", this);

            //注册你的同步对象
            br.RegisterJsObject("form_syn", this);

            var items = new List<MesStationCommon.MechIOItem>() {

                new MesStationCommon.MechIOItem() { name ="入口挡停", type = MesStationCommon.MechIOItem.IOType.Output, set = setValue, get = getValue1  },
                new MesStationCommon.MechIOItem() { name ="入口电机", type = MesStationCommon.MechIOItem.IOType.Output, set = setValue, get = getValue2  },
                new MesStationCommon.MechIOItem() { name ="入口电机2", type = MesStationCommon.MechIOItem.IOType.Output, set = setValue, get = getValue3  },
                new MesStationCommon.MechIOItem() { name ="入口传感器", type = MesStationCommon.MechIOItem.IOType.Input, set = null, get = getValue4  },
                new MesStationCommon.MechIOItem() { name ="入口传感器1", type = MesStationCommon.MechIOItem.IOType.Input, set = null, get = getValue  },

            };

            br.RegisterJsObject("io", new MesFrontEnd.JsHelper(items));

        }

        public void setValue(bool value)
        {    
            return;
        }

        public bool getValue()
        {
            return true;
        }

        public bool getValue1()
        {
            return true;
        }

        public bool getValue2()
        {
            return false;
        }

        public bool getValue3()
        {
            return true;
        }

        public bool getValue4()
        {
            return false;
        }




        private void Br_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.Url.EndsWith("Main.html"))
                _loadEvent.Set();
            
        }
        MesStationCommon.XmlConfigParamteterManage config = new MesStationCommon.XmlConfigParamteterManage();
        public string getXmlConfigParameters()
        {
            config.parameters.Clear();
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text="湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature",text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit",text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Humidity", text = "湿度", value = "60" });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Temperature", text = "温度", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "100" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "0" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "40" } } });
            config.parameters.Add(new MesStationCommon.ConfigParameter { Name = "Torque", text = "扭矩", value = new List<MesStationCommon.ConfigParameter>() { new MesStationCommon.ConfigParameter { Name = "uplimit", text = "上限", value = "125" }, new MesStationCommon.ConfigParameter { Name = "lolimit", text = "下限", value = "-30" }, new MesStationCommon.ConfigParameter { Name = "configvalue", text = "设定值", value = "80" } } });


            string param = Newtonsoft.Json.JsonConvert.SerializeObject(config.parameters);

            return param;
        }

        public bool saveXmlConfigFile(string str)
        {
            var para = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MesStationCommon.ConfigParameter>>(str);

            foreach (var p in para)
            {
                DeserilizValue(p);
            }

            config.parameters = para;
            config.save(@"D:\Projects1\2019\OCCMes\sss.xml");
            return true;
        }

        public void DeserilizValue(MesStationCommon.ConfigParameter p)
        {
            if (p.value == null) return;

            if (p.value.ToString().StartsWith("["))
            {
                var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MesStationCommon.ConfigParameter>>(p.value.ToString());

                p.value = temp;
                foreach (var p1 in temp)
                {
                    DeserilizValue(p1);
                }
            }
            else
            {
                return;
            }
        }
        bool isFault = false;
        MesStationCommon.XmlExecutedSequenceManager mana = new MesStationCommon.XmlExecutedSequenceManager();
        public void manualTreatResult(bool obj)
        {
            isFault = obj;

            //Thread.Sleep(3000);

            //var str = "[{\"article\": \"10346918\",\"SFC\": \"91283RLO00003\",\"MES\": [{\"func\": \"ValidateBOM\",\"result\": \"true\"},{\"func\": \"Start\",\"result\": \"true\"},{\"func\": \"Logparameters\",\"result\": \"false\"},{\"func\": \"Complete\",\"result\": \"true\"}]}]";

            //var val= Newtonsoft.Json.JsonConvert.DeserializeObject(str);
            //br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));

            //Thread.Sleep(3000);

            //str = "[{\"article\": \"10346918\",\"SFC\": \"91283RLO00004\",\"MES\": [{\"func\": \"Start\",\"result\": \"true\"},{\"func\": \"Logparameters\",\"result\": null},{\"func\": \"Complete\",\"result\": null}]}]";

            //val = Newtonsoft.Json.JsonConvert.DeserializeObject(str);
            //br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));

            Thread.Sleep(1000);
            mana.addExecutedMesfunction("98114147EP/91283RLO00004", "10346918", "98114147EP/91283RLO00004", "Start", null);
            mana.addExecutedMesfunction("98114147EP/91283RLO00004", "10346918", "98114147EP/91283RLO00004", "Logparameters", null);
            mana.addExecutedMesfunction("98114147EP/91283RLO00004", "10346918", "98114147EP/91283RLO00004", "LogNonConfermance", null);
            mana.addExecutedMesfunction("98114147EP/91283RLO00004", "10346918", "98114147EP/91283RLO00004", "Complete", null);

            mana.addExecutedMesfunction("98114147EP/91283RLO00003", "10346918", "98114147EP/91283RLO00003", "ValidateBOM", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00003", "10346918", "98114147EP/91283RLO00003", "Start", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00003", "10346918", "98114147EP/91283RLO00003", "Logparameters", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00003", "10346918", "98114147EP/91283RLO00003", "Complete", true);
                                                                                                  
            mana.addExecutedMesfunction("98114147EP/91283RLO00002", "10346918", "98114147EP/91283RLO00002", "ValidateBOM", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00002", "10346918", "98114147EP/91283RLO00002", "Start", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00002", "10346918", "98114147EP/91283RLO00002", "Logparameters", false);
            mana.addExecutedMesfunction("98114147EP/91283RLO00002", "10346918", "98114147EP/91283RLO00002", "Complete", null);
                                                                                                 
            mana.addExecutedMesfunction("98114147EP/91283RLO00001", "10346918", "98114147EP/91283RLO00001", "Start", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00001", "10346918", "98114147EP/91283RLO00001", "Logparameters", true);
            mana.addExecutedMesfunction("98114147EP/91283RLO00001", "10346918", "98114147EP/91283RLO00001", "LogNonConfermance", null);
            mana.addExecutedMesfunction("98114147EP/91283RLO00001", "10346918", "98114147EP/91283RLO00001", "Complete", true);

            mana.addExecutedMesfunction("/3OS10351616-03/SN92033RK900100", "10346918", "/3OS10351616-03/SN92033RK900100", "Start", null);
            mana.addExecutedMesfunction("/3OS10351616-03/SN92033RK900100", "10346918", "/3OS10351616-03/SN92033RK900100", "Logparameters", null);
            mana.addExecutedMesfunction("/3OS10351616-03/SN92033RK900100", "10346918", "/3OS10351616-03/SN92033RK900100", "LogNonConfermance", null);
            mana.addExecutedMesfunction("/3OS10351616-03/SN92033RK900100", "10346918", "/3OS10351616-03/SN92033RK900100", "Complete", null);

            var res = Newtonsoft.Json.JsonConvert.SerializeObject(mana.getSequences(x => x.SFC != ""));

            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", res));
        }

        //调试代码用
        public void showDebugTools()
        {
            br.ShowDevTools();

        }

        public void logout()
        {
            try
            {
                br.CloseDevTools();
                br.GetBrowser().CloseBrowser(true);
                this.Invoke((EventHandler)delegate { this.Close(); });
            }
            catch (Exception ex)
            {
                //nlogger.Error(ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var w = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

            this.Top = w.Top;
            this.Left = w.Left;
            this.Height = w.Height;
            this.Width = w.Width;
        }

        public bool manualTreatEnable()
        {
            return true;
        }

        public string getStationName()
        {
            return "FSBCJ_hdw";
        }

        public void changeThreadAction(bool res)
        {
            var a = res;
        }

        public void mesEnable(bool val)
        {
            var a = val;
        }
    }
}
