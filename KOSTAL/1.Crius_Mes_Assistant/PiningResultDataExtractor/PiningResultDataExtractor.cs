using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LM = NLog.LogManager;

namespace MesStationCommon
{
    public class PiningResultDataExtractor : IResultDataExtractor
    {
        XDocument _doc = null;

        public bool isPartOk {
            get {
                if (_doc == null) return false;

                var n = (from x in _doc.Descendants()
                        where x.Name == "STATE"
                        select x).FirstOrDefault();

                if(n != null)
                {
                    return n.Value == "PASS";
                }

                return false;
            }
        }
        public string sfc {
            get
            {
                if (_doc == null) return "";

                var n = (from x in _doc.Descendants()
                         where x.Name == "Barcode"
                         select x).FirstOrDefault();

                if (n != null)
                {
                    return n.Value;
                }

                return "";
            } 
        }

        /// <summary>
        /// object；数据格式name, value
        /// </summary>
        public List<KeyValue> parameters {
            get
            {
                List<KeyValue> p = new List<KeyValue>();

                // < Values >
                //< Value type = "PCB" val = "1" tval = "" ulim = "" llim = "" unit = "" alarm = "" desc = "PCB location in panel" />        
                //< Value type = "Pin" val = "2" tval = " " ulim = " " llim = " " unit = " " alarm = " " pindesc = "" desc = "Pin No in PCB" />
                //< Value type = "Force" val = "189.955" ulim = "320" llim = "125" unit = "N" alarm = "1" desc = "max force" />                            
                //</ Values >

                //add pining parameters
                foreach (var values in _doc.Descendants().Where(x=> x.Name == "Values") )
                {
                    var currentValue = new KeyValue();
                    
                    //each pin
                    foreach(var value in values.Elements("Value"))
                    {
                        //check PCB - number
                        var obj = (from x in value.Attributes() where x.Name.LocalName == "type" && x.Value == "PCB" select x).FirstOrDefault();
                        if(obj != null)
                        {
                            currentValue.name = "PCB" + value.Attribute("val").Value;
                            continue;
                        }

                        //get pin number
                        obj = (from x in value.Attributes() where x.Name.LocalName == "type" && x.Value == "Pin" select x).FirstOrDefault();
                        if(obj != null)
                        {
                            currentValue.name += ("-" + value.Attribute("val").Value);
                            continue;
                        }

                        //get value;
                        obj = (from x in value.Attributes() where x.Name.LocalName == "type" && x.Value == "Force" select x).FirstOrDefault();
                        if(obj != null)
                        {
                            currentValue.value = value.Attribute("val").Value;
                            currentValue.lolimit = value.Attribute("llim").Value;
                            currentValue.uplimit = value.Attribute("ulim").Value;
                            currentValue.compare_mode = "BETWEEN";
                        }
                       
                    }

                    p.Add(currentValue);
                }

                return p;
            }
        }
        public List<IResultDataExtractor> subPanels {
            get
            {
                List<IResultDataExtractor> panels = new List<IResultDataExtractor>();

                foreach(var panel in _doc.Root.Elements("values"))
                {
                    //to-do:how to handle the different
                }

                return panels;
            }
        }

        public bool Load(string file)
        {
            try
            {
                LM.GetCurrentClassLogger().Info(string.Format("load pinning result data file {0}", file));
                _doc = XDocument.Load(file);
                return true;
            }
            catch(Exception e)
            {
                LM.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }
        }
    }
}
