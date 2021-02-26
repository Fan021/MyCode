using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 结果文件的路由规则,包含生成文件的名称和路径信息,如何根据扫到的条码信息获取结果日志(xml)文件
    /// </summary>
    public interface IResultDataRouter
    {
        bool Init(string basePath);
        string getFullFileName(string sfc);
        string getFileNameWithoutPath(string sfc);
    }
}
