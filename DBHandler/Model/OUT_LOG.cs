using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHandler.Model
{
    public class OUT_LOG
    {
        public int id { get; set; }
        public int userId { get; set; }
        public String gwMac { get; set; }
        public String gwTime { get; set; }
        public String data1 { get; set; }

        public String startDate { get; set; }
        public String endDate { get; set; }

    }
}
