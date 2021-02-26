using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MesStationCommon
{
    /// <summary>
    /// 空结果文件路由,需配合EmptyResultDataExtractor使用
    /// </summary>
    public class EmptyDataRouter : IResultDataRouter
    {
  
        public string getFileNameWithoutPath(string sfc)
        {
            return string.Format("{0}.xml",sfc);
        }

        public string getFullFileName(string sfc)
        {
            //get system path
            return string.Format("C:\\temp\\{0}.xml", sfc);
        }

        public bool Init(string basePath)
        {
            return true;
        }
    }
}
