using MesStationCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //DBConnector connect = new DBConnector();
            //connect.DBName = System.Configuration.ConfigurationManager.AppSettings["AmountOfBoardsDB"];
            //if (connect.Init())
            //{
            //    string a = "";
            //}
            MillingBarcodeRule boards = new MillingBarcodeRule();

            boards.getArticle("/P12074349/SNYBZHC11111190", null);

            //MillingMesSequence.ValidateBom validate = new MillingMesSequence.ValidateBom();

            //validate.paramters["ECoEndpoint"] = "http://cnshmes001.cn.kostal.int:1050/MillingIntegrationR1_5/MillingIntegrationBean5?wsdl";
            //validate.paramters["ECOUser"] = "ECOSYS";
            //validate.paramters["ECOPassword"] = "m7atbQ0nn3Ae";
            //validate.paramters["EcoRequestFilePath"] = Application.StartupPath;
            //validate.paramters["ResourceId"] = "100084";
            //validate.paramters["Operation"] = "MILL";
            //validate.paramters["sfc"] = "/SNYBZHC11111190";
            ////validate.paramters["amountOfBoards"] = "8";

            //validate.Execute();
            //bool ret = validate.Result();

            MillingMesSequence.Start start = new MillingMesSequence.Start();

            start.paramters["ECoEndpoint"] = "http://cnshmes001.cn.kostal.int:1050/MillingIntegrationR1_5/MillingIntegrationBean5?wsdl";
            start.paramters["ECOUser"] = "ECOSYS";
            start.paramters["ECOPassword"] = "m7atbQ0nn3Ae";
            start.paramters["EcoRequestFilePath"] = Application.StartupPath;
            start.paramters["ResourceId"] = "100084";
            start.paramters["Operation"] = "MILL";
            start.paramters["sfc"] = "/SNYBZHC11111190";
            //validate.paramters["amountOfBoards"] = "8";

            start.Execute();
            bool ret1 = start.Result();
        }
    }
}
