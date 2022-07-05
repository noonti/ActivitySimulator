using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataFrame : IDataFrame
    {
        // Command header
        public UInt16 totalSize { get; set; } // 
        public byte version = 0x30;
        public byte[] tel = new byte[20];

        // Message header
        public byte provider = 0x05;
        public byte[] transactionId = new byte[12];
        public byte[] mac = new byte[6]; // bu mac
        public byte power { get; set; }
        public byte battery { get; set; }
        public byte rssi { get; set; }
        public byte timezone { get; set; }
        public UInt16 year { get; set; }
        public byte month { get; set; }
        public byte day { get; set; }
        public byte hour { get; set; }
        public byte minute { get; set; }
        public byte second { get; set; }
        public byte cmd { get; set; }

        // body 
        public IDataFrame body;

        // tail 
        public byte retry { get; set; }
        public byte eof = 0x23 ;// # 

        public DataFrame()
        {
            totalSize = 0;
            
            /*
             서비스 제공자(Global LivOn Version = 0x10)
             - SRV_PROVIDER_U_CARE     0x00
             - SRV_PROVIDER_H_CARE     0x01
             - SRV_PROVIDER_HYODREAM   0x02
             - SRV_PROVIDER_HD_30      0x03       // TSP30 국내 m2m
             - SRV_PROVIDER_HD_30_KT   0x04      // TSP30 국내 KT
             - General : 0x10
             - Turkey : 0x11
             - German : 0x12
             - XCARE20: 0x05
             - XCARE20-TEST: 0x06
            */
            provider = 0x05;
            SetTransactionId(Utility.GetTransactionId());

            mac = new byte[6];

            // 전원상태
            power = 0x00;
            //배터리 레벨
            battery = 0x38;
            // rssi 
            rssi = 0x74;
            // timezone
            timezone = 0x09;
            cmd = 0xBB; // bb command 

            body = new BBCmdBody();


        }


        

        public int Deserialize(byte[] packet, int startIdx)
        {
            int index;
            index = startIdx;
            byte[] value = new byte[10];

            // total size 
            Array.Copy(packet, index, value,0, 2);
            index += 2;
            totalSize = BitConverter.ToUInt16(value,0);// Utility.toLittleEndianInt16(value);

            // version
            version = packet[index++];

            // tel 
            Array.Copy(packet, index, tel, 0, 20);
            index += 20;

            // provider
            provider = packet[index++];


            // transactionId
            Array.Copy(packet, index, transactionId, 0, 12);
            index += 12;

            // mac 
            Array.Copy(packet, index, mac, 0, 6);
            index += 6;

            // power
            power = packet[index++];

            // battery
            battery = packet[index++];

            // rssi 
            rssi = packet[index++];

            // timezone
            timezone = packet[index++];

            // year
            Array.Copy(packet, index, value,0, 2);
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

            // cmd
            cmd = packet[index++];

            if(cmd == 0xBB)
            {
                body = new BBCmdBody();
                index = body.Deserialize(packet, index);
            }

            retry = packet[index++];
            eof = packet[index++];

            return index;
        }

        public byte[] Serialize()
        {
            byte[] result;
            int index = 0;
            byte[] bodyPacket;

            bodyPacket = body.Serialize();
            totalSize = (UInt16)(bodyPacket.Length + 56);
            result = new byte[totalSize];

            // total size 
            Array.Copy(Utility.ToLittleEndian(totalSize, 2),0, result, index, 2);
            index += 2;

            // version
            result[index++] = version;

            // tel 
            Array.Copy(tel, 0, result, index, tel.Length);
            index += tel.Length;

            // provider
            result[index++] = provider;

            // transactionId
            Array.Copy(transactionId, 0, result, index, transactionId.Length);
            index += transactionId.Length;

            // mac 
            Array.Copy(mac, 0, result, index, mac.Length);
            index += mac.Length;

            // power
            result[index++] = power;

            // battery
            result[index++] = battery;

            // rssi 
            result[index++] = rssi;

            // timezone
            result[index++] = timezone;

            // year 
            Array.Copy(Utility.ToLittleEndian(year, 2), 0, result, index, 2);
            index += 2;

            result[index++] = month;
            result[index++] = day;
            result[index++] = hour;
            result[index++] = minute;
            result[index++] = second;


            // cmd 
            result[index++] = cmd;

            // body 
            Array.Copy(bodyPacket, 0, result, index, bodyPacket.Length);
            index += bodyPacket.Length;


            // retry
            result[index++] = 0x0;

            // eof (#)
            result[index++] = eof;

            return result;
            
        }

        public void SetTransactionId(String id)
        {
            byte[] value = Utility.StringToByte(id);
            Array.Copy(value, 0, transactionId, 0, value.Length);

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

        public void SetTel(String telNo)
        {
            var temp = Utility.StringToByte(telNo);
            Array.Copy(temp, 0, tel, 0, temp.Length);
        }

        public void SetMac(String macAddress)
        {
            //var temp = Utility.StringToByte(macAddress);
            //Array.Copy(temp, 0, mac, 0, temp.Length);
            mac = Utility.ConvertHexaStringToByte(macAddress);
        }


        public void SetDataFrame(IDataFrame dataFrame)
        {
            DataFrame frame = (DataFrame)dataFrame;
            totalSize = frame.totalSize;
            version = frame.version;
            Array.Copy(frame.tel, 0, tel, 0, frame.tel.Length);
            provider = frame.provider;
            Array.Copy(frame.transactionId, 0, transactionId, 0, frame.transactionId.Length);
            Array.Copy(frame.mac, 0, mac, 0, frame.mac.Length);
            power = frame.power;
            battery = frame.battery;
            rssi = frame.rssi;
            timezone = frame.timezone;
            year = frame.year;
            month = frame.month;
            day = frame.day;
            hour = frame.hour;
            minute = frame.minute;
            second = frame.second;
            cmd = frame.cmd;
            body.SetDataFrame(frame.body);
            retry = frame.retry;
            eof = frame.eof;

        }

    }
}
