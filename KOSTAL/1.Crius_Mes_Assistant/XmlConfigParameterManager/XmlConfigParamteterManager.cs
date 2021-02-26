using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace MesStationCommon
{
    public class XmlConfigParamteterManage : IConfigParamterManager
    {
        private string _name = "";
        private List<ConfigParameter> _parameters = new List<ConfigParameter>();
        public string name { get { return _name; } set { _name = value; } }

        public List<ConfigParameter> parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }

        }

        public void append(ConfigParameter article)
        {
            _parameters.Add(article);
        }

        public bool load(string file)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("load parameters from configuration file {0}", file));

            XmlSerializer serialer = new XmlSerializer(typeof(XmlConfigParamteterManage));

            try
            {
                if (File.Exists(file))
                {
                    using (var stream = new StreamReader(file))
                    {
                        var obj = (serialer.Deserialize(stream) as XmlConfigParamteterManage);

                        this._parameters = obj.parameters;
                        this.name = obj.name;
                    }

                    return true;
                }
            }
            catch(Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
            }

            return false;
        }

        public bool save(string file)
        {
            NLog.LogManager.GetCurrentClassLogger().Info(string.Format("save parameters to configuration file {0}", file));
            XmlSerializer serializer = new XmlSerializer(typeof(XmlConfigParamteterManage));

            try
            {
                using (var stream = new StreamWriter(file))
                {
                    serializer.Serialize(stream, this);
                }

                return true;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }
        }

        public List<KeyValue> toKeyValuePairs(bool isgnoreEmptyValue)
        {
            List<KeyValue> values = new List<KeyValue>();
            foreach(var p in _parameters)
            {
                toKeyValuePairs(isgnoreEmptyValue, p, values);
            }
            return values;
            
        }

        //查找参数树
        private List<KeyValue> toKeyValuePairs(bool isgnoreEmptyValue, ConfigParameter p, List<KeyValue> valueSet)
        {
            var o = new List<ConfigParameter>();
            //List<KeyValue> values = new List<KeyValue>();

            if (p == null) return valueSet;
              
            if (p.Name != null && p.value != null && p.value.GetType().FullName == typeof(List<ConfigParameter>).FullName)
            {
                //p is name
                if(valueSet.Find(x=> x.name == p.Name) == null)
                {
                    valueSet.Add(new KeyValue() { name = p.Name,compare_mode = "BETWEEN" });

                    foreach (var item in p.value as List<ConfigParameter>)
                    {
                        toKeyValuePairs(isgnoreEmptyValue, item, valueSet);
                    }
                }
            }
            else //p.value=null or basic value
            {
               if(p.Name == "lolimit")
                {
                    //fond last parameter
                    valueSet.FindLast(x => true).lolimit = p.value.ToString();
                }

                if (p.Name == "uplimit")
                {
                    //fond last parameter
                    valueSet.FindLast(x => true).uplimit = p.value.ToString();
                }

                if(p.Name == "configvalue")
                {
                    valueSet.FindLast(x => true).value = p.value.ToString();
                }               
                
            }

            return valueSet;
        }
    }
}
