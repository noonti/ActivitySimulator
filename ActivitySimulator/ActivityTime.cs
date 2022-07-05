using Common.Protocol;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivitySimulator
{
    public class ActivityTime
    {
        public bool IsSelected { get; set; }
        public RectangleF rect { get; set; }
        public int index { get; set; }
        public int timePerCell { get; set; }  // 1개가 나타내는 단위 시간(초) - 기본 2초
        public SensorType sensorType { get; set; }
        public Color color { get; set; }

        public ActivityTime()
        {
            IsSelected = true;
            index = 0;
            timePerCell = 2; // 기본 2초 
            color = Color.Red;
        }

        public bool PointInRect(Point pt)
        {
            bool bResult = false;
            bResult = rect.Contains(pt);
            return bResult;
        }
    }
}
