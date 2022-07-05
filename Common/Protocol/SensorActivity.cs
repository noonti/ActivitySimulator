using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol
{
    public class SensorActivity : IDataFrame
    {
        public byte[] mac = new byte[6]; // sensor mac
        public byte status;
        
        public byte[] activity = new byte[20];// 5분간 activity data 
        public UInt32[] totalActivity = new UInt32[5]; // 1분 활동량합
        public SensorType sensorType;
        public SensorActivity()
        {
            //index = 0;
            
        }

        public int Deserialize(byte[] packet, int startIdx)
        {
            int index;
            index = startIdx;
            byte[] value = new byte[10];


            // mac 
            Array.Copy(packet, index, mac, 0, 6);
            index += 6;
            
            // status 
            status = packet[index++];

            Array.Copy(packet, index, activity, 0, activity.Length);
            index += activity.Length;

            //for(int i=0;i<5;i++)
            //{
            //    Array.Copy(packet, index, value, 0, 4);
            //    index += 4;
            //    activity[i] = BitConverter.ToUInt32(value,0);
            //}
            return index;
        }

        public byte[] Serialize()
        {
            byte[] result = new byte[27];
            int index = 0;
            try
            {
                Array.Copy(mac, 0, result, index, mac.Length);
                index += mac.Length;
                result[index++] = status;

                Array.Copy(activity, 0, result, index, activity.Length);
                index += activity.Length;

                //for(int i=0;i<5;i++)
                //{
                //    byte[] amount = Utility.GetActivity(activity[i]);
                //    Array.Copy(amount, 0, result, index, amount.Length);
                //    index += amount.Length;
                //}
            }
            catch(Exception ex)
            {

            }

            return result;
        }

        public void SetDataFrame(IDataFrame dataFrame)
        {

            SensorActivity frame = (SensorActivity)dataFrame;
            Array.Copy(frame.mac, 0, mac, 0, frame.mac.Length);
            status = frame.status;
            Array.Copy(frame.activity, 0, activity, 0, frame.activity.Length);
            Array.Copy(frame.totalActivity, 0, totalActivity, 0, frame.totalActivity.Length);
            sensorType = frame.sensorType;
        }

        public void SetMac(String macAddress)
        {
            var temp = Utility.StringToByte(macAddress);
            Array.Copy(temp, 0, mac, 0, temp.Length);
        }
    }
}
