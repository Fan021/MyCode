using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using CefSharp;
using System.IO;
using System.Reflection;
using MesStationCommon;
using System.Dynamic;

namespace CriusMESTerminal
{
    public partial class Main : Form
    {
        private Thread _TaskScanner = null;
        private Thread _TaskSequence = null;
        private bool _bStopTask = false;

        private static NLog.Logger nlogger;

        private string entrySide;
        private string exitSide;
        private string topSide;
        private string bottomSide;
        private IScanner _serialEntryTop = null;
        private IScanner _serialEntryBottom = null;
        private IScanner _serialExitTop = null;
        private IScanner _serialExitBottom = null;
        private ITransmit _transmitEntryTop = null;
        private ITransmit _transmitEntryBottom = null;
        private ITransmit _transmitExitTop = null;
        private ITransmit _transmitExitBottom = null;

        private string sfc = "";
        private string article = "";
        private string barcode = "";

        private IBarcodeRule _barCodeRule = null;
        private IWhilteList _whiteList = null;
        private IResultDataRouter _resultDataRouter = null;
        private IResultDataExtractor _resultDataExtractor = null;
        private IConfigParamterManager _configParameters = null;
        private ISequenceAdapter _sequenceAdapter = null;
        private IExecutedSequceManager _executedSequceManager = null;
        private IMechControl _mechControl = null;

        private static int stationNum;
        private static string resourceID;
        private static string operation;
        private static string eCoEndpoint;
        private static string eCOUser;
        private static string eCOPassword;
        private static string ecoRequestFilePath;
        Dictionary<string, IMesFuction> _mesFunctions = new Dictionary<string, IMesFuction>();

        private CefSharp.WinForms.ChromiumWebBrowser br = null;

        private bool _manualTreatResult;

        private static object _lock = new object();

        ManualResetEvent _loadEvent = new ManualResetEvent(false);

        public Main()
        {
            try
            {
                NLog.Config.XmlLoggingConfiguration cXMLConfiguration;

                string configPath = @".\NLog.config";
                if (System.IO.File.Exists(configPath))
                {
                    cXMLConfiguration = new NLog.Config.XmlLoggingConfiguration(configPath);
                    if ((cXMLConfiguration != null))
                    {
                        NLog.LogManager.Configuration = cXMLConfiguration;
                    }
                }
                nlogger = NLog.LogManager.GetCurrentClassLogger();

                //初始化扫描枪、传递条码串口
                nlogger.Info("Initialize Scanner.");
                if (!InitScanner())
                {
                    nlogger.Info("Initialize Scanner Failed.");
                    throw new Exception("could not create instance for each lib!");
                }

                //初始化其他接口对象
                if (!CreateInstance())
                {
                    nlogger.Info("Initialize Instance Failed.");
                    throw new Exception("could not create instance for each lib!");
                }

                //初始化工位个数
                int.TryParse( ConfigurationManager.AppSettings["ResourceId"].ToString(), out stationNum);

                //初始化mes functions
                resourceID = ConfigurationManager.AppSettings["ResourceId"].ToString();
                operation = ConfigurationManager.AppSettings["Operation"].ToString();
                eCoEndpoint = ConfigurationManager.AppSettings["ECoEndpoint"].ToString();
                eCOUser = ConfigurationManager.AppSettings["ECOUser"].ToString();
                eCOPassword = ConfigurationManager.AppSettings["ECOPassword"].ToString();
                ecoRequestFilePath = ConfigurationManager.AppSettings["EcoRequestFilePath"].ToString();

                string nameMes = ConfigurationManager.AppSettings["IMesFunction"].ToString();
                var types = Assembly.LoadFile(Application.StartupPath + nameMes).GetTypes();           
                foreach(var t in types.Where(x=> x.GetInterface(typeof(IMesFuction).FullName) != null))
                {
                    if(!t.Name.Contains("<"))
                    {
                        if (ConfigurationManager.AppSettings.AllKeys.Contains(t.Name))
                        {
                            if (ConfigurationManager.AppSettings[t.Name].ToString() == "1" | ConfigurationManager.AppSettings[t.Name].ToString() == "2") {
                                var o = t.InvokeMember("", BindingFlags.CreateInstance, null, null, new object[] { }) as IMesFuction;
                                if (o == null)
                                {
                                    nlogger.Info("Initialize mes functions Failed.");
                                    throw new Exception("could not create instance for " + o.Name + " mes functions!");
                                }
                                _mesFunctions.Add(o.Name, o);
                                //初始化基本的mes参数
                                o.paramters.Add("ResourceId", resourceID);
                                o.paramters.Add("Operation", operation);
                                o.paramters.Add("ECoEndpoint", eCoEndpoint);
                                o.paramters.Add("ECOUser", eCOUser);
                                o.paramters.Add("ECOPassword", eCOPassword);
                                o.paramters.Add("EcoRequestFilePath", ecoRequestFilePath);
                            }
                        }

                    }
                }

                //加载界面
                nlogger.Info("Initialize Interface.");
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
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Init", MessageBoxButtons.OK);
                nlogger.Error(ex.Message);
            }
        }

        public void RegisterJsObjects()
        {
            //注册你的js对象
            br.RegisterAsyncJsObject("form_async", this);

            //注册你的同步对象
            br.RegisterJsObject("form_syn", this);
            List<MechIOItem> a = new List<MechIOItem>();
            a = _mechControl.getIOPair();
            br.RegisterJsObject("io", new MesFrontEnd.JsHelper(_mechControl.getIOPair()));
        }

        private void Br_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.Url.EndsWith("Main.html"))
                _loadEvent.Set();

            if (e.Frame.Url.EndsWith("Produce.html"))
            {
                //加载mes sequence
                List<MesStationCommon.ExceutedSequeces> executedSequence = new List<ExceutedSequeces>();
                executedSequence = _executedSequceManager.getSequences(x => true);
                if (executedSequence.Count > 0)
                {
                    var val = Newtonsoft.Json.JsonConvert.SerializeObject(executedSequence);
                    br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));
                }               

                //页面加载完成后，启动扫描和mes sequence线程
                StartThread();
            }
        }
        //调试代码用
        public void showDebugTools()
        {
            br.ShowDevTools();

        }

        /// <summary>
        /// 扫描条码进程
        /// </summary>
        private void TaskScanner()
        {
            try
            {
    			//判断pcb板是否已经到位，到位的话移动板子重新到位
                if(entrySide =="1"){
                	while(_mechControl.get_entryside_sensor() && _bStopTask){
                		//提示移动pcb板
                        br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请移动PCB板"));   
                    	Thread.Sleep(1);
                	}                        
            	}else if(exitSide == "1"){
            		while(_mechControl.get_exitside_sensor() && _bStopTask){
                		//提示移动pcb板
                        br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请移动PCB板"));   
                    	Thread.Sleep(1);
                	} 
            	}
    			br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请扫描标签"));
                string nBarcode = "";
                while (_bStopTask)
                {
                    lock (_lock)
                    {            
                        if(entrySide == "1")
                        {
                            //设置IO初始状态
                            if (!_mechControl.get_entryside_sensor())
                            {
                                _mechControl.set_entryside_hold(true);
                                _mechControl.set_entryside_motor(false);
                            }else{
                            	br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请扫描标签"));
                            }
                            if(_serialEntryTop != null)
                            {
                            	if (_serialEntryTop.isBarcodeReady(()=>_mechControl.get_entryside_sensor() || _transmitEntryTop.isHostTrigged()))
                                {
                                    nBarcode = _serialEntryTop.getBarcode(1000);
                                }
                            }
                            if (_serialEntryBottom != null)
                            {
                            	if (_serialEntryBottom.isBarcodeReady(()=>_mechControl.get_entryside_sensor() || _transmitEntryBottom.isHostTrigged()))
                                {
                                    nBarcode = _serialEntryBottom.getBarcode(1000);
                                }
                            }
                        }else if(exitSide == "1")
                        {
                            //设置IO初始状态
                            if (!_mechControl.get_exitside_sensor())
                            {
                                _mechControl.set_exitside_hold(true);
                                _mechControl.set_exitside_motor(false);
                            }else{
                            	br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请扫描标签"));
                            }
                            if (_serialExitTop != null)
                            {
                            	if (_serialExitTop.isBarcodeReady(()=>_mechControl.get_exitside_sensor() || _transmitExitTop.isHostTrigged()))
                                {
                                    nBarcode = _serialExitTop.getBarcode(1000);
                                }
                            }
                            if (_serialEntryTop != null)
                            {
                            	if (_serialEntryTop.isBarcodeReady(()=>_mechControl.get_exitside_sensor() || _transmitExitBottom.isHostTrigged()))
                                {
                                    nBarcode = _serialEntryTop.getBarcode(1000);
                                }
                            }
                        }
                        //获取sfc
                        if (nBarcode != "")
                        {
                            //清除异常信息
                            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, nBarcode));
                            sfc = _barCodeRule.getSfc(nBarcode, _mesFunctions);
                            article = _barCodeRule.getArticle(nBarcode, _mesFunctions);
                            if(article =="" || sfc == ""){
                            	//IO重新重新触发扫描枪
                            	if (entrySide == "1")
	                            {
	                            	while(_mechControl.get_entryside_sensor() && _bStopTask){
	                            		br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "获取变种失败，请取走产品"));
	                            		Thread.Sleep(1);
	                            	}	                            	
	                            }else if(exitSide == "1")
	                            {
	                            	while(_mechControl.get_exitside_sensor() && _bStopTask){
	                            		br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "获取变种失败，请取走产品"));
	                            		Thread.Sleep(1);
	                            	}	
	                            }
                            	br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请扫描标签"));
                            	nBarcode = "";
                            	continue;
                            }
                            //加载mes sequence
                            List<MesStationCommon.ExceutedSequeces> executedSequence = new List<ExceutedSequeces>();
                            executedSequence = _executedSequceManager.getSequences(x => true);
       
							if(_sequenceAdapter.ExecutedSequences(sfc).Count == _mesFunctions.Count | _sequenceAdapter.ExecutedSequences(sfc).Count == 0){
	                          	//判断是否已经做过mes
	                            foreach (var o in executedSequence)
	                            {
	                                if(o.SFC == sfc)
	                                {
	                                    _executedSequceManager.clear(sfc);
	                                    _executedSequceManager.save(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString());
	                                    //清除sequenceAdapter
	                                    foreach (var n in _mesFunctions)
	                                    {
	                                        if (_sequenceAdapter.isSequeceExecuted(sfc, n.Key))
	                                        {
	                                            if (!_sequenceAdapter.deleteSequence(sfc, n.Key))
	                                            {
	                                                nlogger.Info("Delete Sequence Failed.");
	                                                throw new Exception("could not selete mes sequence!");
	                                            }
	                                        }
	                                    }
	                                }
	                            }	
						        foreach(var o in _mesFunctions)
	                            {
	                                _executedSequceManager.addExecutedMesfunction(sfc, article, nBarcode, o.Key, null);
	                            }
	                            _executedSequceManager.save(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString());
	                            executedSequence = _executedSequceManager.getSequences(x => true); 		
}
                            if (executedSequence.Count > int.Parse(ConfigurationManager.AppSettings["StationNum"].ToString()))
                            {
                                _executedSequceManager.clear(executedSequence[0].SFC);
                            }
                            _executedSequceManager.save(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString());
                            executedSequence = _executedSequceManager.getSequences(x => true);
                            executedSequence.Reverse();
                            var val = Newtonsoft.Json.JsonConvert.SerializeObject(executedSequence);
                            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));
                            barcode = nBarcode;
                            nBarcode = "";
                        }

                        Thread.Sleep(1);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Scanner", MessageBoxButtons.OK);
                nlogger.Error(ex.Message);
            }
        }
        /// <summary>
        /// 执行sequence进程
        /// </summary>
        private void TaskSequence()
        {
            try
            {
                while (_bStopTask)
                {          
                    lock(_lock)
                    {
                        bool isbreak = false;

                        if (article != "")
                        {
                            //判断白名单
                            if(_whiteList.hasArticle(article))
                            {
                                //产品结果
                                bool EndResult = false;
                                dynamic mesResults = null;                             
                                if (_sequenceAdapter.ExecutedSequences(sfc).Count == 0)
                                {
                                    foreach (var key in _mesFunctions.Keys)
                                    {
                                        if (ConfigurationManager.AppSettings[key].ToString() == "1")
                                        {
                                            //判断function是否已经做过
                                            if (_sequenceAdapter.isSequeceExecuted(sfc, key)) continue;

                                            nlogger.Info("Execute " + _mesFunctions[key].Name + " for sfc:" + sfc);
                                            if (_mesFunctions[key].paramters.ContainsKey("sfc"))
                                            {
                                                _mesFunctions[key].paramters["sfc"] = sfc;
                                            }
                                            else
                                            {
                                                _mesFunctions[key].paramters.Add("sfc", sfc);
                                            }

                                            switch (key)
                                            {                                       
                                                case "AssembleEmptyComp":
                                                case "Complete":
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;              //先跳出if, switch，执行界面提示，然后跳出sequence
                                                    }
                                                    else
                                                    {    
                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                    }
                                                    break;
                                                case "ValidateBom":    
                                                case "Start":
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;      //跳出mes sequence
                                                    }
                                                    else
                                                    {
                                                    	if(key == "ValidateBom"){
	                                                        if (!TransmitBarcode())
	                                                        {
	                                                            nlogger.Info("Transmit Barcode error. ");
	                                                        }           
	                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());                                                       	
                                                    	}else{
	                                                    	if(ConfigurationManager.AppSettings["ValidateBom"].ToString() != "1"){
		                                                 		if (!TransmitBarcode())
		                                                        {
		                                                            nlogger.Info("Transmit Barcode error. ");
		                                                        }           
		                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString()); 
	                                                    	}                                                    	
                                                    	}
                                                    }
                                                    break;
                                                case "LogParameters":
                                                    //获取测试结果参数，传给mes，包括：xmlConfig和ResultData
                                                    List<object> logParameters = new List<object>();
                                                    foreach (var l in _configParameters.toKeyValuePairs(true))
                                                    {
                                                        logParameters.Add(l);
                                                    }
                                                    string filepath = _resultDataRouter.getFullFileName(sfc);

                                                    if(_resultDataExtractor.Load(filepath)){
	                                                    foreach (var l in _resultDataExtractor.parameters)
	                                                    {
	                                                        logParameters.Add(l);
	                                                    }                                                   
                                                    }else{
                                                    	nlogger.Info("Load Result File Failed.");
                                                    }

                                                    if (_mesFunctions[key].paramters.ContainsKey("parameters"))
                                                    {
                                                        _mesFunctions[key].paramters["parameters"] = logParameters;
                                                    }
                                                    else
                                                    {
                                                        _mesFunctions[key].paramters.Add("parameters", logParameters);
                                                    }
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;      //跳出mes sequence
                                                    }
                                                    else
                                                    {    
                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                    }
                                                    break;
                                                case "LogNonConformance":
                                                    List<object> results = new List<object>();
                                                    //从测试结果文件中取测试结果
                                                    EndResult = _resultDataExtractor.isPartOk && !_manualTreatResult;
                                                    //从界面获取测试结果
                                                    if (EndResult)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        results.Add(new
                                                        {
                                                            name = ConfigurationManager.AppSettings["NcCode"].ToString(),
                                                            value = EndResult
                                                        });

                                                        if (_mesFunctions[key].paramters.ContainsKey("results"))
                                                        {
                                                            _mesFunctions[key].paramters["results"] = results;
                                                        }
                                                        else
                                                        {
                                                            _mesFunctions[key].paramters.Add("results", results);
                                                        }

                                                        mesResults = _mesFunctions[key].Execute();
                                                        if (!_mesFunctions[key].Result())
                                                        {
                                                            if (mesResults is object)
                                                            {
                                                                nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                            }
                                                            else
                                                            {
                                                                nlogger.Info("Result: " + mesResults.ToString());
                                                            }
                                                            isbreak = true;
                                                            break;      //跳出mes sequence
                                                        }
                                                        else
                                                        {                                                            
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            //将做过的finction存起来
                                            if (!_sequenceAdapter.addSequence(sfc, key))
                                            {
                                                nlogger.Info(string.Format("Add Mes Sequence Error. sfc: {0}", sfc));
                                                throw new Exception("could not add mes sequence!!");
                                            }
                                            if (!_sequenceAdapter.save(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString()))
                                            {
                                                nlogger.Info("Save Sequence Failed.");
                                                throw new Exception("could not save mes sequence!!");
                                            }
                                            //界面提示mes function结果
                                            List<MesStationCommon.ExceutedSequeces> executedSequence = new List<ExceutedSequeces>();
                                            if (!EndResult)
                                            {
                                                _executedSequceManager.addExecutedMesfunction(sfc, article, barcode, key, _mesFunctions[key].Result());
                                                _executedSequceManager.save(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString());
                                            }
                                            else
                                            {
                                                EndResult = false;
                                            }
                                            executedSequence = _executedSequceManager.getSequences(x => true);
                                            executedSequence.Reverse();
                                            var val = Newtonsoft.Json.JsonConvert.SerializeObject(executedSequence);
                                            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));
                                        }
	                                    if (isbreak)
	                                    {
		                                    foreach (var n in _mesFunctions)
		                                    {
		                                        if (_sequenceAdapter.isSequeceExecuted(sfc, n.Key))
		                                        {
		                                            if (!_sequenceAdapter.deleteSequence(sfc, n.Key))
		                                            {
		                                                nlogger.Info("Delete Sequence Failed.");
		                                                throw new Exception("could not selete mes sequence!");
		                                            }
		                                        }
		                                    }
		                                    _sequenceAdapter.save(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString());	                                    	
	                                        //界面提示mes失败信息
	                                        string str = mesResults["resultText"].ToString().Replace("'", "");
	                                        br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 1, str));
	                                        Thread.Sleep(3000);
	                                        break;
	                                    }
                                    }
                                }
                                else
                                {
                                    foreach (var key in _mesFunctions.Keys)
                                    {
                                        if (ConfigurationManager.AppSettings[key].ToString() == "2")
                                        {
                                            //判断function是否已经做过
                                            if (_sequenceAdapter.isSequeceExecuted(sfc, key)) continue;
                                            nlogger.Info("Execute " + _mesFunctions[key].Name + " for sfc:" + sfc);
                                            if (_mesFunctions[key].paramters.ContainsKey("sfc"))
                                            {
                                                _mesFunctions[key].paramters["sfc"] = sfc;
                                            }
                                            else
                                            {
                                                _mesFunctions[key].paramters.Add("sfc", sfc);
                                            }

                                            switch (key)
                                            {
                                                case "AssembleEmptyComp":
                                                case "Complete":
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;              //先跳出if, switch，执行界面提示，然后跳出sequence
                                                    }
                                                    else
                                                    {     
                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                    }
                                                    break;
                                                case "ValidateBom":                                                    
                                                case "Start":
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;      //跳出mes sequence
                                                    }
                                                    else
                                                    {
                                                    	if(key == "ValidateBom"){
	                                                        if (!TransmitBarcode())
	                                                        {
	                                                            nlogger.Info("Transmit Barcode error. ");
	                                                        }           
	                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());                                                       	
                                                    	}else{
	                                                    	if(ConfigurationManager.AppSettings["ValidateBom"].ToString() != "1"){
		                                                 		if (!TransmitBarcode())
		                                                        {
		                                                            nlogger.Info("Transmit Barcode error. ");
		                                                        }           
		                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString()); 
	                                                    	}                                                    	
                                                    	}
                                                    }
                                                    break;
                                                case "LogParameters":
                                                    //获取测试结果参数，传给mes，包括：xmlConfig和ResultData
                                                    List<object> logParameters = new List<object>();
                                                    foreach (var l in _configParameters.toKeyValuePairs(true))
                                                    {
                                                        logParameters.Add(l);
                                                    }
                                                    string filepath = _resultDataRouter.getFullFileName(sfc);
                                                    if (_resultDataExtractor.Load(filepath))
                                                    {
                                                        foreach (var l in _resultDataExtractor.parameters)
                                                        {
                                                            logParameters.Add(l);
                                                        }
                                                    }else{
                                                    	nlogger.Info("Load Result File Failed.");
                                                    }
                                                    if (_mesFunctions[key].paramters.ContainsKey("parameters"))
                                                    {
                                                        _mesFunctions[key].paramters["parameters"] = logParameters;
                                                    }
                                                    else
                                                    {
                                                        _mesFunctions[key].paramters.Add("parameters", logParameters);
                                                    }
                                                    mesResults = _mesFunctions[key].Execute();
                                                    if (!_mesFunctions[key].Result())
                                                    {
                                                        if (mesResults is object)
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults.ToString());
                                                        }
                                                        isbreak = true;
                                                        break;      //跳出mes sequence
                                                    }
                                                    else
                                                    {                             
                                                        nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                    }
                                                    break;
                                                case "LogNonConformance":
                                                    List<object> results = new List<object>();
                                                    //从测试结果文件中取测试结果
                                                    EndResult = _resultDataExtractor.isPartOk && !_manualTreatResult;
                                                    //从界面获取测试结果
                                                    if (EndResult)
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        results.Add(new
                                                        {
                                                            name = ConfigurationManager.AppSettings["NcCode"].ToString(),
                                                            value = EndResult
                                                        });

                                                        if (_mesFunctions[key].paramters.ContainsKey("results"))
                                                        {
                                                            _mesFunctions[key].paramters["results"] = results;
                                                        }
                                                        else
                                                        {
                                                            _mesFunctions[key].paramters.Add("results", results);
                                                        }

                                                        mesResults = _mesFunctions[key].Execute();
                                                        if (!_mesFunctions[key].Result())
                                                        {
                                                            if (mesResults is object)
                                                            {
                                                                nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                            }
                                                            else
                                                            {
                                                                nlogger.Info("Result: " + mesResults.ToString());
                                                            }
                                                            isbreak = true;
                                                            break;      //跳出mes sequence
                                                        }
                                                        else
                                                        {
                                                            nlogger.Info("Result: " + mesResults["resultText"].ToString());
                                                        }
                                                        break;
                                                    }
                                                default:
                                                    break;
                                            }
                                            //将做过的finction存起来
                                            if (!_sequenceAdapter.addSequence(sfc, key))
                                            {
                                                nlogger.Info(string.Format("Add Mes Sequence Error. sfc: {0}", sfc));
                                                throw new Exception("could not add mes sequence!!");
                                            }
                                            if (!_sequenceAdapter.save(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString()))
                                            {
                                                nlogger.Info("Save Sequence Failed.");
                                                throw new Exception("could not save mes sequence!!");
                                            }
                                            //界面提示mes function结果
                                            List<MesStationCommon.ExceutedSequeces> executedSequence = new List<ExceutedSequeces>();
                                            if (!EndResult)
                                            {
                                                _executedSequceManager.addExecutedMesfunction(sfc, article, barcode, key, _mesFunctions[key].Result());
                                                _executedSequceManager.save(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString());
                                            }
                                            else
                                            {
                                                EndResult = false;
                                            }
                                            executedSequence = _executedSequceManager.getSequences(x => true);
                                            executedSequence.Reverse();
                                            var val = Newtonsoft.Json.JsonConvert.SerializeObject(executedSequence);
                                            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", val));
                                        }
	                                    if (isbreak)
	                                    {
			                                foreach (var n in _mesFunctions)
		                                    {
		                                        if (_sequenceAdapter.isSequeceExecuted(sfc, n.Key))
		                                        {
		                                            if (!_sequenceAdapter.deleteSequence(sfc, n.Key))
		                                            {
		                                                nlogger.Info("Delete Sequence Failed.");
		                                                throw new Exception("could not selete mes sequence!");
		                                            }
		                                        }
		                                    }
		                                    _sequenceAdapter.save(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString());
	                                        //界面提示mes失败信息
	                                        string str = mesResults["resultText"].ToString().Replace("'", "");
	                                        br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 1, str));
	                                        Thread.Sleep(3000);
	                                        break;
	                                    }
                                    }
                                }
                                //执行完所有的sequence，清除sequenceAdapter
                                if(_sequenceAdapter.ExecutedSequences(sfc).Count == _mesFunctions.Count)
                                {
                                    foreach (var n in _mesFunctions)
                                    {
                                        if (_sequenceAdapter.isSequeceExecuted(sfc, n.Key))
                                        {
                                            if (!_sequenceAdapter.deleteSequence(sfc, n.Key))
                                            {
                                                nlogger.Info("Delete Sequence Failed.");
                                                throw new Exception("could not selete mes sequence!");
                                            }
                                        }
                                    }
                                    _sequenceAdapter.save(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString());
                                }
                                //故障件按钮重置
                                br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync("restoreBtn()");
                                ClearBarcodeVar();
                            }
                            else
                            {
                                //界面显示inactive模式
                                br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "InActive Mode"));
                                //清除界面的sequence
                                br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMesSequence({0})", ""));
                                if (!TransmitBarcode())
                                {
                                    nlogger.Info(string.Format("Transmit Barcode error.barcode: {0}"), barcode);
                                    throw new Exception("could not transmit barcode!");
                                }
                                ClearBarcodeVar();                                
                            }
                            //走板                           
                            if(!isbreak){
	                            if (entrySide == "1")
	                            {
	                                _mechControl.entryside_go();
	                            }else if(exitSide == "1")
	                            {
	                                _mechControl.exitside_go();
	                            }                            
                            }else{
	                            if (entrySide == "1")
	                            {
	                            	while(_mechControl.get_entryside_sensor() && _bStopTask){
	                            		br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "MES执行失败，请取走产品"));
	                            		Thread.Sleep(1);
	                            	}	                            	
	                            }else if(exitSide == "1")
	                            {
	                            	while(_mechControl.get_exitside_sensor() && _bStopTask){
	                            		br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "MES执行失败，请取走产品"));
	                            		Thread.Sleep(1);
	                            	}	
	                            }
                            }
                            br.GetBrowser().GetFrame("ifm_produce").ExecuteJavaScriptAsync(string.Format("showMsg({0},'{1}')", 0, "请扫描标签"));	
                        }
                        Thread.Sleep(10);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error MesSequence", MessageBoxButtons.OK);
                nlogger.Error(ex.Message);
            }

        }

        private Dictionary<string,object> MesBasicParameters()
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                //初始化mes参数
                resourceID = ConfigurationManager.AppSettings["ResourceId"].ToString();
                operation = ConfigurationManager.AppSettings["Operation"].ToString();
                eCoEndpoint = ConfigurationManager.AppSettings["ECoEndpoint"].ToString();
                eCOUser = ConfigurationManager.AppSettings["ECOUser"].ToString();
                eCOPassword = ConfigurationManager.AppSettings["ECOPassword"].ToString();
                ecoRequestFilePath = ConfigurationManager.AppSettings["EcoRequestFilePath"].ToString();

                parameters.Add("ResourceId", resourceID);
                parameters.Add("Operation", operation);
                parameters.Add("ECoEndpoint", eCoEndpoint);
                parameters.Add("ECOUser", eCOUser);
                parameters.Add("ECOPassword", eCOPassword);
                parameters.Add("EcoRequestFilePath", ecoRequestFilePath);

                return parameters;
            }catch (Exception ex)
            {
                nlogger.Error(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 实例化接口对象
        /// </summary>
        /// <param name="t">接口类型</param>
        /// <param name="file">类文件路径</param>
        /// <returns></returns>
        private object CreatTargetIntance(Type t, string file)
        {
            if (File.Exists(Application.StartupPath + file))
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(Application.StartupPath + file);
                    var type = asm.GetTypes().Where(x => x.GetInterface(t.FullName) != null).FirstOrDefault();
                    if(type != null)
                    {
                        var obj = type.InvokeMember("", BindingFlags.CreateInstance, null, null, new object[] { });
                        return obj;
                    }
                    return null;
                }catch(Exception ex)
                {
                    nlogger.Error(ex.Message);
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// 初始化扫描枪
        /// </summary>
        /// <returns></returns>
        private bool InitScanner()
        {
            try
            {
                entrySide = ConfigurationManager.AppSettings["EntrySide"].ToString();
                exitSide = ConfigurationManager.AppSettings["ExitSide"].ToString();
                topSide = ConfigurationManager.AppSettings["TopSide"].ToString();
                bottomSide = ConfigurationManager.AppSettings["BottomSide"].ToString();

                string nameScanner = ConfigurationManager.AppSettings["IScanner"].ToString();
                string nameTransmit = ConfigurationManager.AppSettings["ITransmit"].ToString();
                if (entrySide == "1")
                {
                    if (topSide == "1")
                    {
                        if (_serialEntryTop == null)
                        {
                            _serialEntryTop = CreatTargetIntance(typeof(IScanner), nameScanner) as IScanner;
                            _serialEntryTop.Init(ConfigurationManager.AppSettings["ScannerEntryTop"].ToString());
                        }
                        if (_transmitEntryTop == null)
                        {
                            _transmitEntryTop = CreatTargetIntance(typeof(ITransmit), nameTransmit) as ITransmit;
                            _transmitEntryTop.Init(ConfigurationManager.AppSettings["TransmitEntryTop"].ToString());
                        }
                    }
                    else if (bottomSide == "1")
                    {
                        if (_serialEntryBottom == null)
                        {
                            _serialEntryBottom = CreatTargetIntance(typeof(IScanner), nameScanner) as IScanner;
                            _serialEntryBottom.Init(ConfigurationManager.AppSettings["ScannerEntryBottom"].ToString());
                        }
                        if (_transmitEntryBottom == null)
                        {
                            _transmitEntryBottom = CreatTargetIntance(typeof(ITransmit), nameTransmit) as ITransmit;
                            _transmitEntryBottom.Init(ConfigurationManager.AppSettings["TransmitEntryBottom"].ToString());
                        }
                    }
                }
                if (exitSide == "1")
                {
                    if (topSide == "1")
                    {
                        if (_serialExitTop == null)
                        {
                            _serialExitTop = CreatTargetIntance(typeof(IScanner), nameScanner) as IScanner;
                            _serialExitTop.Init(ConfigurationManager.AppSettings["ScannerExitTop"].ToString());
                        }
                        if (_transmitExitTop == null)
                        {
                            _transmitExitTop = CreatTargetIntance(typeof(ITransmit), nameTransmit) as ITransmit;
                            _transmitExitTop.Init(ConfigurationManager.AppSettings["TransmitExitTop"].ToString());
                        }
                    }
                    else if (bottomSide == "1")
                    {
                        if (_serialExitBottom == null)
                        {
                            _serialExitBottom = CreatTargetIntance(typeof(IScanner), nameScanner) as IScanner;
                            _serialExitBottom.Init(ConfigurationManager.AppSettings["ScannerExitBottom"].ToString());
                        }
                        if (_transmitExitBottom == null)
                        {
                            _transmitExitBottom = CreatTargetIntance(typeof(ITransmit), nameTransmit) as ITransmit;
                            _transmitExitBottom.Init(ConfigurationManager.AppSettings["TransmitExitBottom"].ToString());
                        }
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                nlogger.Error(ex.Message);
                return false;
            }
        }

        private bool CreateInstance()
        {
            try
            {
                _barCodeRule = CreatTargetIntance(typeof(IBarcodeRule),
                            ConfigurationManager.AppSettings["IBarcodeRule"].ToString()) as IBarcodeRule;
                _whiteList = CreatTargetIntance(typeof(IWhilteList),
                            ConfigurationManager.AppSettings["IWhilteList"].ToString()) as IWhilteList;
                if(_whiteList != null)
                {
                    if (!_whiteList.load(ConfigurationManager.AppSettings["WhiteListFilePath"].ToString())) return false;
                }
                _resultDataRouter = CreatTargetIntance(typeof(IResultDataRouter),
                            ConfigurationManager.AppSettings["IResultDataRouter"].ToString()) as IResultDataRouter;
                if(_resultDataRouter != null)
                {
                    if (!_resultDataRouter.Init(ConfigurationManager.AppSettings["ResultDataRouterPath"].ToString())) return false;
                }
                _resultDataExtractor = CreatTargetIntance(typeof(IResultDataExtractor),
                            ConfigurationManager.AppSettings["IResultDataExtractor"].ToString()) as IResultDataExtractor;             
                _configParameters = CreatTargetIntance(typeof(IConfigParamterManager),
                            ConfigurationManager.AppSettings["IConfigParamterManager"].ToString()) as IConfigParamterManager;
                if(_configParameters != null)
                {
                    if (!_configParameters.load(ConfigurationManager.AppSettings["ConfigParamterManagerPath"].ToString())) return false;
                }
                _sequenceAdapter = CreatTargetIntance(typeof(ISequenceAdapter),
                            ConfigurationManager.AppSettings["ISequenceAdapter"].ToString()) as ISequenceAdapter;
                if(_sequenceAdapter != null)
                {
                    if (File.Exists(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString()))
                    {
                        if (!_sequenceAdapter.load(ConfigurationManager.AppSettings["SequenceAdapterPath"].ToString())) return false;
                    }
                }
                _executedSequceManager = CreatTargetIntance(typeof(IExecutedSequceManager),
                            ConfigurationManager.AppSettings["IExecutedSequceManager"].ToString()) as IExecutedSequceManager;
                if(_executedSequceManager != null)
                {
                    if (File.Exists(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString()))
                    {
                        if (!_executedSequceManager.load(ConfigurationManager.AppSettings["ExecutedSequenceManagerPath"].ToString())) return false;
                    }
                }
                _mechControl = CreatTargetIntance(typeof(IMechControl),
                            ConfigurationManager.AppSettings["IMechControl"].ToString()) as IMechControl;
                if(_mechControl != null)
                {
                    if (!_mechControl.init(ConfigurationManager.AppSettings["MechControl"].ToString())) return false;
                }

                return _barCodeRule != null
                    && _whiteList != null
                    && _resultDataRouter != null
                    && _resultDataExtractor != null
                    && _configParameters != null
                    && _sequenceAdapter != null
                    && _executedSequceManager != null
                    && _mechControl != null;

            }
            catch (Exception ex)
            {
                nlogger.Error(ex.Message);
                return false;
            }
        }

        private bool TransmitBarcode()
        {
            try
            {
                if (entrySide == "1")
                {
                    if (topSide == "1")
                    {
                        _transmitEntryTop.send(barcode);
                    }
                    else if (bottomSide == "1")
                    {
                        _transmitEntryBottom.send(barcode);
                    }
                }
                if (exitSide == "1")
                {
                    if (topSide == "1")
                    {
                        _transmitExitTop.send(barcode);
                    }
                    else if (bottomSide == "1")
                    {
                        _transmitExitBottom.send(barcode);
                    }
                }
            }
            catch(Exception ex)
            {
                nlogger.Error(ex.Message);
                return false;
            }
            return true;
        }

        private void ClearBarcodeVar()
        {
            barcode = "";
            sfc = "";
            article = "";
        }

        public void manualTreatResult(bool result)
        {
            _manualTreatResult = result;
        }

        public bool manualTreatEnable()
        {
            string res = ConfigurationManager.AppSettings["ManualTreatEnable"].ToString();
            if(res == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getXmlConfigParameters()
        {
            if(_configParameters != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(_configParameters.parameters);
            }else
            {
                return "";
            }
        }

        public bool saveXmlConfigFile(string str)
        {
            try
            {
                if (_configParameters != null)
                {
                    var para = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MesStationCommon.ConfigParameter>>(str);

                    foreach (var p in para)
                    {
                        DeserilizValue(p);
                    }

                    _configParameters.parameters = para;

                    _configParameters.save(ConfigurationManager.AppSettings["ConfigParamterManagerPath"].ToString());
                }
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public string getStationName()
        {
            return ConfigurationManager.AppSettings["StationName"].ToString();
        }
        private void DeserilizValue(MesStationCommon.ConfigParameter p)
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

        private void Main_Load(object sender, EventArgs e)
        {
            var w = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

            this.Top = w.Top;
            this.Left = w.Left;
            this.Height = w.Height;
            this.Width = w.Width;
        }
        public void logout()
        {
            try
            { 
            	this.BeginInvoke((EventHandler)delegate { this.Close(); });
            }
            catch (Exception ex)
            {
                nlogger.Error(ex.Message);
            }
        }
        private void StartThread(){
    	    _bStopTask = true;
            _TaskScanner = new Thread(new ThreadStart(TaskScanner));
            _TaskSequence = new Thread(new ThreadStart(TaskSequence));
            _TaskScanner.Start();
            _TaskSequence.Start();
        }       
        private void QuitThread()
        {
        	_bStopTask = false;
            if(_TaskScanner != null && _TaskScanner.ThreadState != ThreadState.Stopped)
            {
                _bStopTask = false;
                while( _TaskScanner.ThreadState != ThreadState.Stopped){
                	Application.DoEvents();
                	Thread.Sleep(1);
                }
            }

            if(_TaskSequence !=null && _TaskSequence.ThreadState != ThreadState.Stopped)
            {
                _bStopTask = false;
                while( _TaskScanner.ThreadState != ThreadState.Stopped){
                	Application.DoEvents();
                	Thread.Sleep(1);
                }
            }
        }
        
        public void changeThreadAction(bool action){
        	//清除扫描枪的buffer
            if (_serialEntryTop != null)
            {
            	_serialEntryTop.Clear();
            }        	
        	if (_serialEntryBottom != null)
            {
            	_serialEntryBottom.Clear();
            }
            if (_serialExitTop != null)
            {
            	_serialExitTop.Clear();
            }
            if (_serialExitBottom != null)
            {
            	_serialExitBottom.Clear();
            }            
        	if(action){
        		StartThread();
        	}else{
        		QuitThread();
        	}
        }
        public void mesEnable(bool action){
        	changeThreadAction(action);
        	//关闭mes后，设置IO状态
        	bool entryEnable =System.Configuration.ConfigurationManager.AppSettings["EntrySide"] == "1";
        	bool exitEnable = System.Configuration.ConfigurationManager.AppSettings["ExitSide"] == "1";
        	     		
    		if(entryEnable)
    		{
            	_mechControl.set_entryside_hold(action);
            	_mechControl.set_entryside_motor(!action);
    		}
    		if(exitEnable)
    		{
    			_mechControl.set_exitside_hold(action);
    			_mechControl.set_exitside_motor(!action);
    		}
        }
		void MainFormClosed(object sender, FormClosedEventArgs e)
		{
			    QuitThread();
            System.Diagnostics.Process[] process= System.Diagnostics.Process.GetProcessesByName("_TaskScanner");
            foreach (System.Diagnostics.Process p in process) {
                p.Kill();
            };
       
            	if(_serialEntryTop != null) _serialEntryTop.Quit();
            	if(_serialEntryBottom != null) _serialEntryBottom.Quit();
            	if(_serialExitTop != null) _serialExitTop.Quit();
            	if(_serialExitBottom != null) _serialExitBottom.Quit();
            	
            	if(_transmitEntryTop != null) _transmitEntryTop.Quit();
            	if(_transmitEntryBottom != null) _transmitEntryBottom.Quit();
            	if(_transmitExitTop != null) _transmitExitTop.Quit();
            	if(_transmitExitBottom != null) _transmitExitBottom.Quit();           	
            		    
                br.CloseDevTools();
                br.GetBrowser().CloseBrowser(true);
		}
    }
}
