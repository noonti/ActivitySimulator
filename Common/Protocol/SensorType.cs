using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Protocol
{
    public class SensorType
    {   /*
            LocationItem{mName='roomA', mKrname='침실A', mCode='00'}
            LocationItem{mName='roomB', mKrname='침실B', mCode='01'}
            LocationItem{mName='livingA', mKrname='거실A', mCode='10'}
            LocationItem{mName='livingB', mKrname='거실B', mCode='11'}
            LocationItem{mName='bath', mKrname='화장실', mCode='20'}
            LocationItem{mName='bathB', mKrname='화장실B', mCode='21'}
            LocationItem{mName='kitchen', mKrname='주방', mCode='30'}
            LocationItem{mName='Door Sensor', mKrname='현관', mCode='40'}
            LocationItem{mName='Refrigerator', mKrname='냉장고', mCode='41'}
            LocationItem{mName='Medicine box', mKrname='약상자', mCode='42'}
            LocationItem{mName='Bath Door', mKrname='화장실', mCode='43'}
            LocationItem{mName='BathB Door', mKrname='화장실B', mCode='44'}
        */
        public String SensorCode { get; set; }
        public String SensorName { get; set; }
        public String SensorKName { get; set; }

        public byte GetSensorCode ()
        {
            byte result = 0;
            if(!String.IsNullOrEmpty(SensorCode))
            {
                if (SensorCode.CompareTo("FF") == 0)
                    result = 0xFF;
                else
                    result = byte.Parse(SensorCode);
            }
            return result;
        }
    }
}
