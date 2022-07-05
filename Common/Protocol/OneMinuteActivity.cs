using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol
{
    public class OneMinuteActivity : IDataFrame
    {
        public byte[] mac = new byte[6]; // sensor mac 
        public byte place;
        public byte status;
        public byte[] activity = new byte[4] ;// 5분간 activity data 

        public int Deserialize(byte[] packet, int startIdx)
        {
            int index;
            index = startIdx;
            byte[] value = new byte[10];


            // mac 
            Array.Copy(packet, index, mac, 0, 6);
            index += 6;

            // place
            place = packet[index++];

            // status 
            status = packet[index++];

            Array.Copy(packet, index, activity, 0, activity.Length);
            index += activity.Length;

            //Array.Copy(packet, index, value, 0, 4);
            //activity = BitConverter.ToUInt32(value, 0);
            //index += 4;

            return index;
        }

        public  byte[] Serialize()
        {
            byte[] result = new byte[12];
            int index = 0;
            try
            {
                Array.Copy(mac, 0, result, index, mac.Length);
                index += mac.Length;
                result[index++] = place;
                result[index++] = status;

                Array.Copy(activity, 0, result, index, activity.Length);
                index += activity.Length;

                //var temp = Utility.ToLittleEndian(activity, 4);
                //Array.Copy(temp, 0, result, index, temp.Length);
                //index += temp.Length;

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public void SetDataFrame(IDataFrame dataFrame)
        {
            OneMinuteActivity frame = (OneMinuteActivity)dataFrame;
            Array.Copy(frame.mac, 0, mac, 0, frame.mac.Length);
            place = frame.place;
            status = frame.status;
            Array.Copy(frame.activity, 0, activity, 0, frame.activity.Length);
        }

        public void SetMac(String macAddress)
        {
            var temp = Utility.StringToByte(macAddress);
            Array.Copy(temp, 0, mac, 0, temp.Length);
        }
    }
}
