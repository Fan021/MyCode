using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesStationCommon
{
    public class EmptyMechControl : IMechControl
    {
        public void entryside_go()
        {
            
        }

        public void exitside_go()
        {
            
        }

        public List<MechIOItem> getIOPair()
        {
            return new List<MechIOItem>();
        }

        public bool get_entryside_sensor()
        {
            return false;
        }

        public bool get_exitside_sensor()
        {
            return false;
        }

        public bool init(string parameter)
        {
            return true;
        }

        public void set_entryside_hold(bool isHold)
        {
            
        }

        public void set_entryside_motor(bool isRunning)
        {
            
        }

        public void set_exitside_hold(bool isHold)
        {
            
        }

        public void set_exitside_motor(bool isRunning)
        {
            
        }
    }
}
