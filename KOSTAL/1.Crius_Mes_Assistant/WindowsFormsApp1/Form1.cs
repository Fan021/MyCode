using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        	MesStationCommon.HostSerialControlScanner scanner = new MesStationCommon.HostSerialControlScanner();
        	MesStationCommon.BeckhoffMechControl m = new MesStationCommon.BeckhoffMechControl();
        	
        	
        	scanner.Init("COM4,19200,N,8,1,1000");
        	
        	m.init("amsaddress:,amsport:801,entryside_hold:Main.entry_hold,entryside_sensor:Main.entry_sensor,entryside_motor:Main.entry_motor");
        	
        	while(true)
        	{        		        		
        		if(scanner.isBarcodeReady(m.get_entryside_sensor))
        		{
        			var text = scanner.getBarcode(500);
        		}
        		
        	}
        	
        	
        	
            //MesStationCommon.XmlExecutedSequenceManager m = new MesStationCommon.XmlExecutedSequenceManager();

            //m.addExecutedMesfunction("909", "1212121", "start", true);
            //m.addExecutedMesfunction("909", "1212121", "complete", true);
            //m.addExecutedMesfunction("909", "1212121", "lognc", null);
            //m.addExecutedMesfunction("909", "1212121", "loparamter", true);
            //m.addExecutedMesfunction("91", "1212121", "start", false);
            //m.addExecutedMesfunction("91", "1212121", "complete", true);
            //m.addExecutedMesfunction("92", "1212121", "start", null);
            //m.addExecutedMesfunction("92", "1212122", "lognc", false);
            //m.addExecutedMesfunction("93", "1212121", "start", false);
            //m.addExecutedMesfunction("94", "1212121", "start", false);

            //var r = m.getSequences(x => true);

            //var ss = m.getSequences(x => x.SFC == "909");

            //m.clear("909");
            //ss = m.getSequences(x => x.SFC == "909");

            //m.save(".\\10.xml");


            

            //MesStationCommon.XmlExecutedSequenceManager mm = new MesStationCommon.XmlExecutedSequenceManager();
            //var o = mm.load(@"d:\10.xml");

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(r);

            //try configparamters ------------------------
            //MesStationCommon.ConfigParameter p = new MesStationCommon.ConfigParameter() { Name = "dd", value = null, subParamters = new List<MesStationCommon.ConfigParameter>() };

            //p.subParamters.Add(new MesStationCommon.ConfigParameter() { Name = "kk", value = "12" });
            //p.subParamters.Add(new MesStationCommon.ConfigParameter() { Name = "ll", value = "13" });

            //JsonSerializer s = new JsonSerializer();

            //System.IO.TextWriter w = new System.IO.StreamWriter(@"d:\2.json");

            //s.Serialize(w, p);

            //w.Close();

            //try ------------------------------------- whitelist 

            MesStationCommon.XmlWhiteList list = new MesStationCommon.XmlWhiteList() { name = "OCC" };

            list.ArticleList.Add(new MesStationCommon.WhiteItem { articleNo = "10103625", entryside_sequence = "sedn-read", exitside_sequence = "red-green", BarcodeRule = "StandardBarcodeRule" });
            list.ArticleList.Add(new MesStationCommon.WhiteItem { articleNo = "10103256", entryside_sequence = "sedn-rea1d", exitside_sequence = "red-gree3n", BarcodeRule = "StandardBarcodeRule" });
            list.ArticleList.Add(new MesStationCommon.WhiteItem { articleNo = "10203654", entryside_sequence = "sedn-read2", exitside_sequence = "red-gree1n", BarcodeRule = "StandardBarcodeRule" });

            list.save(@"d:\whitelist.xml");

            MesStationCommon.XmlWhiteList A = new MesStationCommon.XmlWhiteList();

            A.load(@"d:\whitelist.xml");

            //return;

            //MesStationCommon.XmlWhiteList ll = new MesStationCommon.XmlWhiteList();

            //ll.load(@"d:\1.xml");

            //try configparamters
            //MesStationCommon.XmlConfigParamteterManage m = new MesStationCommon.XmlConfigParamteterManage() { name = "tt" };


            //m.append(new MesStationCommon.ConfigParameter()
            //{
            //    Name = "temperature",
            //    subParamters = new List<MesStationCommon.ConfigParameter>() {
            //        new MesStationCommon.ConfigParameter(){ Name ="Lolimit", value = "12"},
            //        new MesStationCommon.ConfigParameter(){Name = "Uplimit", value = "30"}
            //    },

            //    value = null
            //});

            //m.append(new MesStationCommon.ConfigParameter()
            //{
            //    Name = "humid",
            //    value = "22"
            //});

            //m.save(@"d:\3.xml");

            MesStationCommon.XmlConfigParamteterManage t = new MesStationCommon.XmlConfigParamteterManage();

            t.parameters.Add(new MesStationCommon.ConfigParameter()
            {
                Name = "温度",

                //Name = "121",
                value = new List<MesStationCommon.ConfigParameter>(){ new MesStationCommon.ConfigParameter()
                {
                    Name = "lolimit",
                    text = "下限",
                    value = 12
                },
                new MesStationCommon.ConfigParameter()
                {
                    Name = "uplimit",
                    text = "上限",
                    value = 20
                },
                new MesStationCommon.ConfigParameter()
                {
                    Name = "configvalue",
                    text = "值",
                    value = 15
                }
                
                }

            });

            t.parameters.Add(new MesStationCommon.ConfigParameter()
            {
                Name = "亮度度",

                //Name = "121",
                value = new List<MesStationCommon.ConfigParameter>(){ new MesStationCommon.ConfigParameter()
                {
                    Name = "lolimit",
                    text = "下限",
                    value = 120
                },
                new MesStationCommon.ConfigParameter()
                {
                    Name = "uplimit",
                    text = "上限",
                    value = 200
                },
                new MesStationCommon.ConfigParameter()
                {
                    Name = "configvalue",
                    text = "值",
                    value = 155
                }

                }

            });

            t.save(@"d:\4.xml");

            var ob = t.toKeyValuePairs(true);

            MesStationCommon.XmlConfigParamteterManage l = new MesStationCommon.XmlConfigParamteterManage();

            l.load(@"d:\4.xml");

            var s = t.toKeyValuePairs(true);


            //MesStationCommon.StandardBarcodeRule r = new MesStationCommon.StandardBarcodeRule();

            //var sfc = r.getSfc("_P10333257_SNYB560SHR06550", null);
            //var art = r.getArticle("_P10333257_SNYB560SHR06550",null);

            //MesStationCommon.PinningDataRouter r = new MesStationCommon.PinningDataRouter();

            //r.Init(@"C:\Users\yu12\source\repos\WindowsFormsApp1\Crius_Mes_Assistant\datafilefrompining");

            //string l = r.getFullFileName("YB560SHR07100");

            //MesStationCommon.PiningResultDataExtractor ex = new MesStationCommon.PiningResultDataExtractor();

            //ex.Load(l);
            //string sfc = ex.sfc;
            //bool res = ex.isPartOk;
            //var p = ex.parameters;

            MesStationCommon.QueneSequenceAdapter ad = new MesStationCommon.QueneSequenceAdapter();

            ad.load("d:\\1.xml");

            ad.addSequence("06236580", "start");
            ad.addSequence("06236580", "complete");

            var f = ad.isSequeceExecuted("06236580", "start");

            f = ad.isSequeceExecuted("06236580", "com");

            ad.addSequence("0623", "complete");

            ad.deleteSequence("06236580", "start");

            ad.save("d:\\1.xml");

            //MesStationCommon.EmptyDataRouter router = new MesStationCommon.EmptyDataRouter();

            //string f = router.getFullFileName("Y091600123");

            //MesStationCommon.EmptyResultDataExtractor extractor = new MesStationCommon.EmptyResultDataExtractor();

            //extractor.Load(f);

            //string ex = extractor.sfc;
            //var r = extractor.isPartOk;


            return;

        }
    }
}
