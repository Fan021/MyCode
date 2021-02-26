using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using LM = NLog.LogManager;

namespace MesStationCommon
{
  
    public class QueneSequenceAdapter : ISequenceAdapter
    {
        private ExecutedItemCollection _executedItems = new ExecutedItemCollection() { list = new List<ExecutedItem>() };

        public bool addSequence(string sfc, string functionName)
        {
            LM.GetCurrentClassLogger().Info(string.Format("add new sequence [{0},{1}]", sfc, functionName));
            if(_executedItems != null)
            {
                if(_executedItems.list.Where(x=>x.sfc == sfc.Trim() && x.Name == functionName.Trim()).Count() > 0)
                {
                    //exist
                    return true;
                }

                _executedItems.list.Add(new ExecutedItem() { sfc = sfc, Name = functionName });
                return true;
            }

            return false;
        }


        public void clear()
        {
            LM.GetCurrentClassLogger().Info("clear all items");
            if (_executedItems != null)
            {
            	if(_executedItems.list != null) _executedItems.list.Clear();
            }
        }

        public bool deleteSequence(string sfc, string functionName)
        {
            if(_executedItems != null)
            {
                int n = _executedItems.list.RemoveAll(x => x.Name == functionName.Trim() && x.sfc == sfc.Trim());

                LM.GetCurrentClassLogger().Info(string.Format("delete sequence [{0},{1}], {2} items is deleted", sfc, functionName, n));

                return n > 0;
            }

            return false;
        }


        public List<string> ExecutedSequences(string sfc)
        {
            List<string> returns = new List<string>();

            if (_executedItems != null)
            {
                var items = _executedItems.list.Where(x => x.sfc == sfc.Trim());
                foreach(var item in items)
                {
                    returns.Add(item.Name);
                }
            }

            return returns;
        }


        public bool isSequeceExecuted(string sfc, string fuctionName)
        {
        	if(_executedItems != null)
        	{
        		return _executedItems.list.FirstOrDefault(x => x.sfc == sfc.Trim() && x.Name == fuctionName.Trim()) != null;
        	}
        	else
        	{
        		return false;
        	}
        	
        }

        public bool load(string file)
        {
            XmlSerializer s = new XmlSerializer(typeof(ExecutedItemCollection));

            if (File.Exists(file))
            {
                try
                {
                    using (var r = new StreamReader(file))
                    {
                        _executedItems = s.Deserialize(r) as ExecutedItemCollection;
                    }

                    return true;
                }
                catch(Exception e)
                {
                    return false;
                }
            }

            return false;
        }

        public bool save(string file)
        {
            XmlSerializer s = new XmlSerializer(typeof(ExecutedItemCollection));

            try
            {
                using (var w = new StreamWriter(file))
                {
                    s.Serialize(w, _executedItems);
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }
    }
}
