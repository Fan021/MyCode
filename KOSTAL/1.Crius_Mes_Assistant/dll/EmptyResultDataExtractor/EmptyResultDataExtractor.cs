using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MesStationCommon
{
    public class EmptyResultDataExtractor : IResultDataExtractor
    {
        private string _sfc = "";

        //所有结果都是PASS
        public bool isPartOk 
        {	
        	get{
        		return true;
        	}
        }

        public string sfc 
        {
        	get
        	{
        		return _sfc;
        	}
        }

        public List<KeyValue> parameters 
        {
        	get
        	{
        	 	return new List<KeyValue>();
        	}
        }

        public List<IResultDataExtractor> subPanels 
        { 
        	get{ return new List<IResultDataExtractor>();}
        }

        public bool Load(string file)
        {
            //根据输入的文件名返回sfc
            _sfc = Path.GetFileNameWithoutExtension(file);
            return true;
        }
    }
}
