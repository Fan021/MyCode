using System.Collections.Generic;

namespace MesStationCommon
{
    /// <summary>
    /// 空序列适配器;适用于程序配置为entry-side/exit-side模式;无需计入当前已经执行MES功能的情况
    /// </summary>
    public class EmtpySequenceAdapter : ISequenceAdapter
    {
       
        public bool addSequence(string sfc, string functionName)
        {
            return true;
        }

        public void clear()
        {
            
        }

        public bool deleteSequence(string sfc, string functionName)
        {
            return true;
        }

        public List<string> ExecutedSequences(string sfc)
        {
            return new List<string>();
        }

        public bool isSequeceExecuted(string sfc, string fuctionName)
        {
            return false;
        }

        public bool load(string file)
        {
            return true;
        }

        public bool save(string file)
        {
            return true;
        }
    }
}
