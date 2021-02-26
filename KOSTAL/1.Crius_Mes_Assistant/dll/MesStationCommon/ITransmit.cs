using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 转发接口,上海
    /// </summary>
    public interface ITransmit
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool Init(string parameter);

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        bool send(string content);

        /// <summary>
        /// 主机是否触发
        /// </summary>
        /// <returns></returns>
        bool isHostTrigged();

        /// <summary>
        /// 释放资源
        /// </summary>
        void Quit();
    }
}
