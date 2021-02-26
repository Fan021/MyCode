using System;
using System.Collections.Generic;

namespace MesStationCommon
{
    [Serializable]
    public class ExecutedItem
    {
        public string Name { get; set; }

        public string sfc { get; set; }
    }

    [Serializable]
    public class ExecutedItemCollection
    {
        public List<ExecutedItem> list { get; set; }
    }
}
