using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public class MechIOItem
    {
        public enum IOType
        {
            Input,
            Output
        }

        public string name { get; set; }

        public IOType type { get; set; }

        public Action<bool> set;

        public Func<bool> get;
    }
}
