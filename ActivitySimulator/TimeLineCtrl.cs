using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Protocol;
using Common;

namespace ActivitySimulator
{
    public partial class TimeLineCtrl : UserControl
    {
        Bitmap canvasImage;
        List<ActivityTime> activityTimeList = new List<ActivityTime>();

        public int timePerCell; // 셀당 단위 시간. 기본 2초. 

        //센서 2초 활동현황 리스트 (150 bit)
        //public List<SensorActivity> sensorActivityList = new List<SensorActivity>();
        public SensorActivity sensorActivity;

        public SensorType sensorType;
        public Color color;
        public int index;

        bool bDrag = false;
        Point start;
        public TimeLineCtrl()
        {
            InitializeComponent();
            pbImage.MouseDown += pbImage_MouseDown;
            pbImage.MouseMove += pbImag_MouseMove;
            pbImage.MouseUp += pbImag_MouseUp;

            timePerCell = 2;// 2초

            for (int i = 0; i < 150; i++)
            {
                ActivityTime activityTime = new ActivityTime();
                activityTime.IsSelected = false;
                activityTime.timePerCell = timePerCell;
                activityTime.index = i;
                activityTime.color = color;
                activityTimeList.Add(activityTime);
            }

        }

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bDrag = true;
                start = new Point(e.Location.X, e.Location.Y);
                this.Cursor = Cursors.Hand;

                Point pt = new Point(e.X, e.Y);
                DrawActivityTimeSelection(pt);

            }
        }

        private void pbImag_MouseMove(object sender, MouseEventArgs e)
        {
            if (bDrag && (ModifierKeys & Keys.Control) == Keys.Control)
            {
                Point pt = new Point(e.X, e.Y);
                DrawActivityTimeSelection(pt, true);
            }

        }

        private void pbImag_MouseUp(object sender, MouseEventArgs e)
        {
            if (bDrag)
            {

            }
            this.Cursor = Cursors.Default;
            bDrag = false;
        }

        public void DrawTimeLine()
        {
            float width = 0;
            float height = 0;
            float startX = 10;
            float startY = 20;

            if (canvasImage != null)
                canvasImage.Dispose();
            canvasImage = new Bitmap(pbImage.Width, pbImage.Height);
            Graphics bmpDC = Graphics.FromImage(canvasImage);

            width = (pbImage.Width - 10 * 2) / 150;
            height = (pbImage.Height - 10 * 3);

            foreach (var activityTime in activityTimeList)
            {
                activityTime.rect = new RectangleF(startX, startY, width, height);
                DrawActivityTime(bmpDC, activityTime);
                startX += width;
            }

            startX = 10;
            startY = 10;

            for (int i = 1; i <= 5; i++)
            {
                startX = 30 * width * (i) + width;
                bmpDC.DrawLine(new Pen(Color.Black), startX, startY, startX, startY + 5);


            }
            pbImage.Image = canvasImage;
        }

        public void DrawActivityTime(Graphics dc, ActivityTime activityTime)
        {
            dc.DrawRectangle(new Pen(Color.Red, 1), activityTime.rect.Left, activityTime.rect.Top, activityTime.rect.Width, activityTime.rect.Height);
            if (activityTime.IsSelected)
            {
                SolidBrush blueBrush = new SolidBrush(activityTime.color);
                dc.FillRectangle(blueBrush, activityTime.rect);
            }
        }

        private void TimeLineCtrl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DrawActivityTimeSelection(Point pt, bool isDrag = false)
        {
            bool reDraw = false;
            foreach (var activityTime in activityTimeList)
            {
                if (activityTime.PointInRect(pt))
                {
                    Console.WriteLine($"selected index={activityTime.index}");
                    if (isDrag)
                        activityTime.IsSelected = true;
                    else
                        activityTime.IsSelected = !activityTime.IsSelected;
                    reDraw = true;
                }

            }
            if (reDraw)
            {
                DrawTimeLine();
            }
        }

        private void TimeLineCtrl_Resize(object sender, EventArgs e)
        {
            DrawTimeLine();
        }


        public void SetColor(Color color)
        {
            this.color = color;
            foreach (var activityTime in activityTimeList)
            {
                activityTime.color = color;
            }
        }

        public void SetSensorType(SensorType sensorType)
        {
            this.sensorType = sensorType;

            foreach (var activityTime in activityTimeList)
            {
                activityTime.sensorType = sensorType;
            }
        }

        public void SetTimePerCell(int second)
        {
            timePerCell = second;
            for (int i = 0; i < 150; i++)
            {
                activityTimeList[i].timePerCell = timePerCell;
            }
        }

        public void GetSensorActivityTime()
        {
            int i = 0;
            int index = 0;
            //         UInt32 sensorActivityTime = 0;
            UInt32 sensorActivityTotalTime = 0;

            sensorActivity = new SensorActivity();
            sensorActivity.sensorType = sensorType;
            String bitString = String.Format("0000000000");

            foreach (var activityTime in activityTimeList)
            {
                if (activityTime.IsSelected)
                {
                    // bit 연산 sensorActivityTime & i )
                    bitString += "1";
                    //                    sensorActivityTime += (UInt32)(0x01 << (30 - i - 1));
                    sensorActivityTotalTime++;
                }
                else
                    bitString += "0";


                if (activityTime.index != 0 && activityTime.index % 30 == 29) // 1분 완성
                {
                    // 1분간 활동정보 저장(bit)
                    //sensorActivity.activity[index] = sensorActivityTime;
                    sensorActivity.totalActivity[index] = sensorActivityTotalTime;
                    index++;

                    //                    sensorActivityTime = 0;
                    sensorActivityTotalTime = 0;
                    i = 0;
                }
                else
                    i++;
            }
            byte[] bitData = Utility.ConvertBinaryStringToByte(bitString);
            Array.Copy(bitData, 0, sensorActivity.activity, 0, bitData.Length);

        }

        public void SetSensorTypeCombo()
        {
            cbPlace.Items.Clear();
            foreach (var sensorType in Utility.sensorTypeList)
            {
                cbPlace.Items.Add(sensorType.SensorKName);
            }
        }

        public void Initialize()
        {
            SetSensorTypeCombo();
            color = Color.Red;
        }

        private void cbPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPlace.SelectedIndex >= 0)
            {
                sensorType = Utility.sensorTypeList.ElementAt(cbPlace.SelectedIndex);
                SetSensorType(sensorType);
                SetColor(Utility.colorList[cbPlace.SelectedIndex]);
            }
        }

        public UInt32 GetActivityTime(int minute)
        {
            return sensorActivity.activity[minute];

        }

        public UInt32 GetActivityTotalTime(int minute)
        {
            return sensorActivity.totalActivity[minute];

        }

        public void SetOneMinutePickList(List<SensorActivity> pickList)
        {
            int i = 0;
            foreach (var sensorActivity in pickList)
            {
                SetActivityTime(i, sensorActivity);
                // 1분 데이터 세팅
                //sensorActivity.activity[i] 

                i++;
            }
        }
        public void SetActivityTime(int minute, SensorActivity sensorActivity)
        {
            int starIndex = minute * 30;
            UInt32 activity = sensorActivity.activity[minute];
            String bitString = Utility.GetBinaryString(Utility.GetActivityData(minute, sensorActivity.activity));
            int colorIndex = Utility.sensorTypeList.IndexOf(sensorActivity.sensorType);


            int i;
            for (i = 2; i < bitString.Length; i++)
            {
                activityTimeList[starIndex].color = Utility.colorList[colorIndex >= 0 ? colorIndex : 0];
                if (bitString.Substring(i, 1).CompareTo("1") == 0)
                {
                    activityTimeList[starIndex].IsSelected = true;
                }
                else
                {
                    activityTimeList[starIndex].IsSelected = false;
                }
                starIndex++;
            }
            DrawTimeLine();
        }

        private void chkInactive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInactive.Checked) // 미감지 수신일 경우 모든 선택된 정보 reset 한다.
            {
                ClearAllActivityTime();
            }
        }

        private void ClearAllActivityTime()
        {
            foreach (var activityTime in activityTimeList)
            {
                activityTime.IsSelected = false;
            }
            DrawTimeLine();
        }

        public byte GetActivityStatus()
        {
            byte result = 0x00; // 기본 활동 미감지 
            int totalActivity = 0;

            if (chkInactive.Checked)
                result = 0x0F; // 데이터 미수신 
            else
            {
                for(int i=0;i<5;i++)
                {
                    totalActivity += (int)sensorActivity.totalActivity[i];
                }
                if (totalActivity > 0)
                    result = 0x01; // 활동 감지
            }
            return result;
        }
        public bool GetInactiveState()
        {
            return chkInactive.Checked;
        }
    }
}
