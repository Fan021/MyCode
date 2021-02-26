using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    /// <summary>
    /// 空转发器;适用于不需要转发消息的情况
    /// </summary>
    public class EmptyTransmit : ITransmit
    {
        public bool Init(string parameter)
        {
            return true;
        }

        public bool isHostTrigged()
        {
            return false;
        }

        public void Quit()
        {
           
        }

        public bool send(string content)
        {
            return true;
        }
    }
}
