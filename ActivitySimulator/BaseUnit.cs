using Common;
using Common.Protocol;
using DBHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySimulator
{
    public class BaseUnit :GateWay
    {
        public bool bSending = false;
        public String bu_mac { get; set; }
        public List<SENSOR> sensorList = new List<SENSOR>();

        //public GateWay gateway { get; set; }

        public void SendBBCommand(DataFrame frame)
        {
            DataFrame data = new DataFrame();
            data.SetDataFrame(frame);

            if(bu_mac!=null)
                data.mac = Utility.ConvertHexaStringToByte(bu_mac);
            else
                data.mac = Utility.ConvertHexaStringToByte("010203040506");
            BBCmdBody body = (BBCmdBody)data.body;

            foreach(var sensorActivity in body.sensorActivityList)
            {
                sensorActivity.mac = Utility.ConvertHexaStringToByte("010203040506"); ;// Utility.ConvertHexaStringToByte(sensorList[0].s_mac);
            }

            foreach (var oneMinute in body.oneMinuteActivityList)
            {
                oneMinute.mac = Utility.ConvertHexaStringToByte("010203040506"); // Utility.ConvertHexaStringToByte(sensorList[0].s_mac);

            }

            byte[] packet = data.Serialize();
            Send(packet);
            Console.WriteLine("SendBBCommand ...packet length={0}", packet.Length);
        }
    }
}
