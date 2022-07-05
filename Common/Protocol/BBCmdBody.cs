using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Common.Protocol
{
    public class BBCmdBody : IDataFrame
    {
        public byte sensorCount;
        public UInt16 year { get; set; }
        public byte month { get; set; }
        public byte day { get; set; }
        public byte hour { get; set; }
        public byte minute { get; set; }
        public byte second { get; set; }

        public byte deviceId = 0x23;

        public List<SensorActivity> sensorActivityList = new List<SensorActivity>();
        public List<OneMinuteActivity> oneMinuteActivityList = new List<OneMinuteActivity>();


        public int Deserialize(byte[] packet, int startIdx)
        {
            int index = 0;
            index = startIdx;

            byte[] value = new byte[10];

            // sensor count 
            sensorCount = packet[index++] ;

            // year
            Array.Copy(packet, index, value, 0, 2);
            index += 2;
            year = BitConverter.ToUInt16(value, 0);

            // month
            month = packet[index++];

            // day
            day = packet[index++];

            // hour
            hour = packet[index++];

            // minute
            minute = packet[index++];

            // second
            second = packet[index++];

            // deviceId 
            deviceId = packet[index++];

            for(int i=0;i<sensorCount;i++)
            {
                SensorActivity sensorActivity = new SensorActivity();
                index = sensorActivity.Deserialize(packet, index);
                sensorActivityList.Add(sensorActivity);
            }


            for (int i = 0; i < 5; i++)
            {
                OneMinuteActivity oneMinuteActivity = new OneMinuteActivity();
                index = oneMinuteActivity.Deserialize(packet, index);
                oneMinuteActivityList.Add(oneMinuteActivity);
            }


            return index;
        }

        public byte[] Serialize()
        {
            byte[] result;
            int totalSize = 0;
            int index = 0;
            try
            {
                totalSize += sensorActivityList.Count * 27;
                totalSize += oneMinuteActivityList.Count * 12;
                totalSize += 9; // sensorCount(1) + year(2) + month(1) + day(1) + hour(1) + minute(1) + second(1) +  deviceId(1)

                result = new byte[totalSize];

                result[index++] = sensorCount;
                
                // year 
                Array.Copy(Utility.ToLittleEndian(year, 2), 0, result, index, 2);
                index += 2;

                result[index++] = month;
                result[index++] = day;
                result[index++] = hour;
                result[index++] = minute;
                result[index++] = second;
                result[index++] = deviceId;

                foreach (var sensorActivity in sensorActivityList)
                {
                    byte[] temp = sensorActivity.Serialize();
                    Array.Copy(temp, 0, result, index, temp.Length);
                    index += temp.Length;
                }


                foreach (var oneActivity in oneMinuteActivityList)
                {
                    byte[] temp = oneActivity.Serialize();
                    Array.Copy(temp, 0, result, index, temp.Length);
                    index += temp.Length;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public void SetDateTime(DateTime now)
        {
            year = (UInt16)now.Year;
            month = (byte)now.Month;
            day = (byte)now.Day;
            hour = (byte)now.Hour;
            minute = (byte)now.Minute;
            second = (byte)now.Second;

        }

        public void SetDataFrame(IDataFrame dataFrame)
        {
            BBCmdBody frame = (BBCmdBody)dataFrame;
            sensorCount = frame.sensorCount;
            sensorCount = frame.sensorCount;
            year = frame.year;
            month = frame.month;
            day = frame.day;
            hour = frame.hour;
            minute = frame.minute;
            second = frame.second;
            deviceId = frame.deviceId;


            foreach( var sensorActivity in frame.sensorActivityList)
            {
                SensorActivity activity = new SensorActivity();
                activity.SetDataFrame(sensorActivity);
                sensorActivityList.Add(activity);
            }


            foreach (var oneMinuteActivity in frame.oneMinuteActivityList)
            {
                OneMinuteActivity activity = new OneMinuteActivity();
                activity.SetDataFrame(oneMinuteActivity);
                oneMinuteActivityList.Add(activity);
            }

        }
    }
}
