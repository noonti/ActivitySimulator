using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler.Model
{
    public class SENSOR
    {
        //b.mac bu_mac, s.mac s_mac, s.location, s.battery, s.rssi
        public String bu_mac { get; set; }
        public String s_mac { get; set; }
        public String location { get; set; }
        public UInt16 battery { get; set; }
        public UInt16 rssi { get; set; }
    }
}
