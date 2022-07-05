using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BodyData : IDataFrame
    {

        public int GetBodySize()
        {
            return 10;
        }



        public int Deserialize(byte[] packet, int startIdx)
        {
            return 1;
        }

        public byte[] Serialize()
        {
            return null;
        }
    }
}
