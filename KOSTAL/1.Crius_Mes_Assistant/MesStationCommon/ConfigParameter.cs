using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 可配置的参数,可序列化尾JSON或xml对象
    /// </summary>
    /// 
    [Serializable]
    public class ConfigParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 显示的文本值
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 参数值,为null代表当前为组合参数名,subParametners代表其子参数,比如lolimit，uplimit
        /// </summary>
        public object value { get; set; }

        
        
        //public List<ConfigParameter> subParamters { get; set; }

    }
}
