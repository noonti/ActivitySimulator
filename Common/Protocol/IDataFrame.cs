using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol
{
    public interface IDataFrame
    {
        int Deserialize(byte[] packet, int startIdx);
        byte[] Serialize();
        void SetDataFrame(IDataFrame dataFrame);
        
    }
}
