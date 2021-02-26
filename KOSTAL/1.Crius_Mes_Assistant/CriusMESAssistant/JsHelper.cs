using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesFrontEnd
{
    public class JsHelper
    {
        List<MesStationCommon.MechIOItem> _host = new List<MesStationCommon.MechIOItem>();

        public JsHelper(List<MesStationCommon.MechIOItem> target)
        {
            _host = target;
        }

        public bool getInput(string name)
        {
            var item = _host.FirstOrDefault(x => x.name == name && x.type == MesStationCommon.MechIOItem.IOType.Input);
            var res = false;
            if(item != null)
                res = item.get.Invoke();

            return res;
        }
        public bool getOutput(string name)
        {
            var item = _host.FirstOrDefault(x => x.name == name && x.type == MesStationCommon.MechIOItem.IOType.Output);
            var res = false;
            if(item != null)
                res = item.get.Invoke();

            return res;
        }

        public void setOutput(string name, bool value)
        {
            var item = _host.FirstOrDefault(x => x.name == name && x.type == MesStationCommon.MechIOItem.IOType.Output);
            if (item != null) item.set.Invoke(value);
        }

        public string[] getInputNames()
        {
            return _host.Where(x => x.type == MesStationCommon.MechIOItem.IOType.Input).Select(x => x.name).ToArray();
        }

        public string[] getOutputNames()
        {
            return _host.Where(x => x.type == MesStationCommon.MechIOItem.IOType.Output).Select(x => x.name).ToArray();
        }

    }
}
