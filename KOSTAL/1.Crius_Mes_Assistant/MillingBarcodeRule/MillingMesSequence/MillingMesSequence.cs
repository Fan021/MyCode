using MesStationCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MillingMesSequence
{
    public class Basic : IMesFuction
    {
        public static string xmlPath;
        public static string resourceID;
        public static string operation;
        public HttpWebRequest webRequests;
        private Dictionary<string, object> parameters = new Dictionary<string, object>();
        public bool result;

        public virtual string Name
        {
            get
            {
                return "Basic";
            }
        }

        public virtual Dictionary<string, object> paramters
        {
            get
            {
                return parameters;
            }

            set
            {
                parameters = value;
            }
        }

        //[return: Dynamic]
        public virtual dynamic Execute()
        {
            return true;
        }

        public virtual bool Result()
        {
            return result;
        }

        protected void Init()
        {
            xmlPath = parameters["EcoRequestFilePath"].ToString();
            resourceID = parameters["ResourceId"].ToString();
            operation = parameters["Operation"].ToString();
        }

        protected void CreateHttpWebRequest()
        {
            try
            {
                webRequests = null;
                webRequests = (HttpWebRequest)WebRequest.Create(parameters["ECoEndpoint"].ToString());
                webRequests.ContentType = "text/xml"; // or application/soap+xml for SOAP 1.2
                webRequests.Method = "POST";
                webRequests.KeepAlive = false;
                webRequests.Credentials = new NetworkCredential(parameters["ECOUser"].ToString(), parameters["ECOPassword"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected static XmlDocument ReadXmlResponse(WebResponse response)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retXml = sr.ReadToEnd();
                sr.Close();
                doc.LoadXml(retXml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doc;
        }
    }

    public class GetProcessLotDetails : Basic
    {
        public override string Name
        {
            get
            {
                return "GetProcessLotDetails";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//getProcessLotDetails.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("processLot");
                noVersion.InnerText = paramters["processLot"].ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);

                if (resultCode == "0")
                {
                    resParameters.Add("sfc", repon.SelectNodes("memberList").Item(0).InnerText);
                    result = true;
                }
                else
                {
                    resParameters.Add("sfc", "");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }

    public class GetSfcStatus : Basic
    {
        public override string Name
        {
            get
            {
                return "GetSfcStatus";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();

            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//getSfcStatus.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);

                if (resultCode == "0")
                {
                    resParameters.Add("materialId", repon.SelectSingleNode("material").InnerText);
                    result = true;
                }
                else
                {
                    resParameters.Add("materialId", "");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }
    
        public class ValidateBom : Basic
    {
        public override string Name
        {
            get
            {
                return "ValidateBom";
            }
        }

        //[return: Dynamic]
        public override dynamic Execute()
        {
            XDocument obj = new XDocument();
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//validateBOM.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                //get amountofboards
                DBConnector connect = new DBConnector();
                string amountofboards = "";
                connect.DBName = System.Configuration.ConfigurationManager.AppSettings["AmountOfBoardsDB"];
                if (connect.Init())
                {
                	string nSFC = paramters["sfc"].ToString();
                	nSFC = nSFC.Substring(nSFC.Length - 13,13);
                    amountofboards = connect.GetAmountOfBoards(nSFC);                  
                }
                noVersion = requestNode.SelectSingleNode("amountOfBoards");
                noVersion.InnerText = amountofboards.ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                var resultText = repon.SelectSingleNode("resultText").InnerText;
                resParameters.Add("resultText", resultText);

                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("MES ValidateBom ResultCode: {0}, ResultText: {1}", resultCode, resultText));
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }

    public class Start : Basic
    {
        public override string Name
        {
            get
            {
                return "Start";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//start.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                var resultText = repon.SelectSingleNode("resultText").InnerText;
                resParameters.Add("resultText", resultText);

                resultCode = "0";

                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("MES Start ResultCode: {0}, ResultText: {1}", resultCode, resultText));
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }


    public class LogParameters : Basic
    {
        public override string Name
        {
            get
            {
                return "LogParameters";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();

            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//logParameters.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                if (paramters["parameters"] is IList)
                {
                    XmlNode compareModeNode = null;
                    XmlNode dataTypeNode = null;
                    XmlNode identifierNode = null;
                    XmlNode lowerLimitNode = null;
                    XmlNode upperLimitNode = null;
                    XmlNode valueNode = null;
                    foreach (var n in (IList)paramters["parameters"])
                    {
                        Type t = n.GetType();
                        var curParentNode = xmlDocs.CreateNode("element", "logParameters", "");
                        compareModeNode = xmlDocs.CreateNode("element", "compareMode", "");
                        compareModeNode.InnerText = n.GetType().GetProperty("compare_mode").GetValue(n).ToString();
                        dataTypeNode = xmlDocs.CreateNode("element", "dataType", "");
                        dataTypeNode.InnerText = "TEXT";
                        identifierNode = xmlDocs.CreateNode("element", "identifier", "");
                        identifierNode.InnerText = n.GetType().GetProperty("name").GetValue(n).ToString();
                        lowerLimitNode = xmlDocs.CreateNode("element", "lowerLimit", "");
                        lowerLimitNode.InnerText = n.GetType().GetProperty("lolimit").GetValue(n).ToString();
                        upperLimitNode = xmlDocs.CreateNode("element", "upperLimit", "");
                        upperLimitNode.InnerText = n.GetType().GetProperty("uplimit").GetValue(n).ToString();
                        valueNode = xmlDocs.CreateNode("element", "value", "");
                        valueNode.InnerText = n.GetType().GetProperty("value").GetValue(n).ToString();
                        curParentNode.AppendChild(compareModeNode);
                        curParentNode.AppendChild(dataTypeNode);
                        curParentNode.AppendChild(identifierNode);
                        curParentNode.AppendChild(lowerLimitNode);
                        curParentNode.AppendChild(upperLimitNode);
                        curParentNode.AppendChild(valueNode);

                        noVersion.ParentNode.InsertAfter(curParentNode, noVersion.ParentNode.LastChild);
                    }
                }

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);
                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }

    public class AssembleEmptyComp : Basic
    {
        public override string Name
        {
            get
            {
                return "AssembleEmptyComp";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//assembleEmptyComp.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);
                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }

    public class LogNonConformance : Basic
    {
        public override string Name
        {
            get
            {
                return "LogNonConformance";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//logNonConformance.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                if (paramters["results"] is IList)
                {
                    XmlNode identifierNode = null;
                    XmlNode valueNode = null;
                    foreach (var n in (IList)paramters["results"])
                    {
                        Type t = n.GetType();
                        var curParentNode = xmlDocs.CreateNode("element", "ncDatas", "");
                        identifierNode = xmlDocs.CreateNode("element", "identifier", "");
                        identifierNode.InnerText = n.GetType().GetProperty("name").GetValue(n).ToString();
                        valueNode = xmlDocs.CreateNode("element", "value", "");
                        valueNode.InnerText = n.GetType().GetProperty("value").GetValue(n).ToString();
                        curParentNode.AppendChild(identifierNode);
                        curParentNode.AppendChild(valueNode);

                        noVersion.ParentNode.InsertAfter(curParentNode, noVersion.ParentNode.LastChild);
                    }
                }

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);

                //resultCode = "0";

                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }

    public class Complete : Basic
    {
        public override string Name
        {
            get
            {
                return "Complete";
            }
        }
        //[return: Dynamic]
        public override dynamic Execute()
        {
            Dictionary<string, object> resParameters = new Dictionary<string, object>();
            try
            {
                Init();
                CreateHttpWebRequest();

                XmlDocument xmlDocs = new XmlDocument();
                xmlDocs.Load(xmlPath + "//complete.xml");
                XmlNamespaceManager mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

                var requestNode = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var noVersion = requestNode.SelectSingleNode("resourceId");
                noVersion.InnerText = resourceID;
                noVersion = requestNode.SelectSingleNode("operation");
                noVersion.InnerText = operation;
                noVersion = requestNode.SelectSingleNode("sfc");
                noVersion.InnerText = paramters["sfc"].ToString();

                string payload = xmlDocs.InnerXml.ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(payload);
                webRequests.ContentLength = byteArray.Length;
                Stream requestStream = webRequests.GetRequestStream();
                requestStream.Write(byteArray, 0, byteArray.Length);
                requestStream.Close();
                xmlDocs = ReadXmlResponse(webRequests.GetResponse());
                mgr = new XmlNamespaceManager(xmlDocs.NameTable);
                mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
                var repon = xmlDocs.SelectSingleNode("//soap:Body/*/*", mgr);
                var resultCode = repon.SelectSingleNode("resultCode").InnerText;
                resParameters.Add("resultText", repon.SelectSingleNode("resultText").InnerText);

                //resultCode = "0";

                if (resultCode == "0")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.Message;
            }
            finally
            {
                //释放资源
                if (webRequests != null)
                {
                    webRequests.Abort();
                }
            }
            return resParameters;
        }
    }
}
