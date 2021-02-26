using System;
using System.IO;
using LM = NLog.LogManager;

namespace MesStationCommon
{
    /// <summary>
    /// Pinning插针设备数据查找器
    /// </summary>
    public class PinningDataRouter : IResultDataRouter
    {
        string _basePath = "";

        public string getFileNameWithoutPath(string sfc)
        {
            string file = getFullFileName(sfc);
            

            if (File.Exists(file))
            {                
                return Path.GetFileName(file);
            }
            return "";

        }

        /// <summary>
        /// 根据条码信息获取日志文件的完整目录
        /// </summary>
        /// <param name="sfc"></param>
        /// <returns></returns>
        public string getFullFileName(string sfc)
        {
            LM.GetCurrentClassLogger().Info(string.Format("Get pining result file for sfc {0}", sfc));
       
            string temp = sfc.Replace("/", "_");
            string format = string.Format("*{0}*.xml", temp);

            //search file, current time
            string[] f = Directory.GetFiles(_basePath, format);
            if(f.Length == 1)
            {
                LM.GetCurrentClassLogger().Info(string.Format("found result file {0}", f[0]));
                return f[0];
            }
 
            return "";
        }

        /// <summary>
        /// 初始化;
        /// </summary>
        /// <param name="basePath">需要监控的文件目录: 比如d:\data</param>
        /// <returns></returns>
        public bool Init(string basePath)
        {
            try
            {
                try
                {
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }
                }
                catch
                {
                    return false;
                }

                _basePath = basePath;

                return true;
            }
            catch(Exception e)
            {
                LM.GetCurrentClassLogger().Error(e,e.Message);
                return false;
            }         
            
        }
    }
}
