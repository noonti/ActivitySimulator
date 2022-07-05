using Common;
using Common.Protocol;
using DBHandler;
using DBHandler.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActivitySimulator
{

    public delegate void DisplayLogDelegate(String log);

    public partial class Form1 : Form
    {
        //List<GateWay> gateWayList = new List<GateWay>();
        
        List<TimeLineCtrl> timeLineList = new List<TimeLineCtrl>();
        DisplayLogDelegate _displayLog = null;

        System.Windows.Forms.Timer _timer = null;

        List<BaseUnit> baseUnitList;

        int sendPeriod = 300;
        //DataFrame dateFrame;

        DBOperation db = new DBOperation();

        public Form1()
        {
            InitializeComponent();
            _displayLog = new DisplayLogDelegate(DisplayData);
            Utility._addLog = new AddLogDelegate(AddLog);

            
            /*
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

            int i = 0;
            Utility.colorList[i++] = Color.Red;
            Utility.colorList[i++] = Color.Blue;
            Utility.colorList[i++] = Color.Green;
            Utility.colorList[i++] = Color.Yellow;
            Utility.colorList[i++] = Color.Silver;
            Utility.colorList[i++] = Color.Cyan;
            Utility.colorList[i++] = Color.DarkGray;
            Utility.colorList[i++] = Color.DarkMagenta;
            Utility.colorList[i++] = Color.DeepPink;
            Utility.colorList[i++] = Color.Brown;

            timeLineList.Add(timeLineCtrl1);
            timeLineList.Add(timeLineCtrl2);
            timeLineList.Add(timeLineCtrl3);
            timeLineList.Add(timeLineCtrl4);
            timeLineList.Add(timeLineCtrl5);

            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "00",
                SensorName = "roomA",
                SensorKName = "침실A"

            });

            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "10",
                SensorName = "livingA",
                SensorKName = "거실A"

            });

            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "20",
                SensorName = "bath",
                SensorKName = "화장실"

            });

            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "30",
                SensorName = "kitchen",
                SensorKName = "주방"

            });

            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "40",
                SensorName = "Door Sensor",
                SensorKName = "현관"

            });


            Utility.sensorTypeList.Add(new SensorType()
            {
                SensorCode = "FF",
                SensorName = "UNKNOWN",
                SensorKName = "미지정"

            });

            foreach (var timeLine in timeLineList)
            {
                timeLine.Initialize();
            }

            //var sensors = db.GetSensorList(out SP_RESULT spResult);

            //baseUnitList = sensors.GroupBy(s => s.bu_mac,
            //    s => new SENSOR { s_mac = s.s_mac, location = s.location, battery = s.battery, rssi = s.rssi },
            //    (key, g) => new BaseUnit { bu_mac = key, sensorList = g.ToList() }).ToList();


            for (i = 2019; i < 2025; i++)
                cbYear.Items.Add(i.ToString());
            cbYear.SelectedIndex = 2;
            //baseUnitList.Add(BaseUn


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendPeriod = int.Parse(txtPeriod.Text);
            //dateFrame = GetBBDataFrame();

            StartService();
        }

        private void StartService()
        {
            StopTimer();
            StartTimer();
            CloseAllGateway();

            StartConnectThread();

            //foreach (var gateway in baseUnitList)
            //{
            //    gateway.SetAddress(txtAddress.Text, int.Parse(txtPort.Text), CLIENT_TYPE.VDS_CLIENT, ClientConnectCallback, ClientReadCallback, SendCallback);
            //    gateway.StartConnect();
            //    Thread.Sleep(10);
            //}
        }

        private void StartConnectThread()
        {
            int i = 0;
            new Thread(() =>
            {
                try
                {
                    foreach (var gateway in baseUnitList)
                    {
                        gateway.SetAddress(txtAddress.Text, int.Parse(txtPort.Text), CLIENT_TYPE.VDS_CLIENT, ClientConnectCallback, ClientReadCallback, SendCallback);
                        gateway.StartConnect();
                        Console.WriteLine("connect count={0}", i++);
                        Thread.Sleep(100);
                    }
                }
                catch (Exception ex)
                {
                    Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
                }
                finally
                {
                }
            }
          ).Start();
        }

        private void StopService()
        {
            StopTimer();
            CloseAllGateway();

        }

        private void StartTimer()
        {
            if (_timer == null)
                _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // 1초마다 체크 
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var nowDate = DateTime.Now;
            Console.WriteLine("timer...");
            SendBBCommand();

            //if (_KictEventDisplayTime != null && nowDate > _KictEventDisplayTime)
            //{
            //    //Console.WriteLine("hide...");
            //    splitLane.Panel1.BackColor = panelOriginalColor;
            //}
            //if (bSyncTime)
            //    ProcessAutoTimeSync();


        }

        private void StopTimer()
        {
            if (_timer != null)
                _timer.Stop();
            _timer = null;
        }
        private void CloseAllGateway()
        {
            foreach (var gateway in baseUnitList)
            {
                gateway.Stop();
            }
        }

        private int ClientConnectCallback(SessionContext session, SOCKET_STATUS status)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 처리 "));
            int nResult = 0;
            String strLog;
            try
            {
                session._status = SOCKET_STATUS.CONNECTED;
                session._socket.BeginReceive(session.buffer, 0, SessionContext.BufferSize, 0,
                    new AsyncCallback(ClientReadCallback), session);

                strLog = String.Format("제어기-->영상VDS ( 접속 성공)");
                Console.WriteLine(strLog);
                Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);


                nResult = 1;
            }
            catch (Exception ex)
            {
                session._status = SOCKET_STATUS.DISCONNECTED;
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 종료 "));
            return nResult;
        }

        public void ClientReadCallback(IAsyncResult ar)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 처리 "));

            String content = String.Empty;
            String strLog;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            SessionContext session = (SessionContext)ar.AsyncState;
            Socket socket = session._socket;
            try
            {
                // Read data from the client socket.
                int bytesRead = socket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    strLog = String.Format("영상VDS --> 제어기 ReadCallback {0} 바이트 데이터 수신", bytesRead);
                    Console.WriteLine(strLog);
                    Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);

                    socket.BeginReceive(session.buffer, 0, SessionContext.BufferSize, 0,
                    new AsyncCallback(ClientReadCallback), session);

                }
                else
                {

                    Console.WriteLine("event client error");
                    //
                    //1. heartbeat 전송 쓰레드 중지 

                    CloseSessionContext(session);


                }
            }
            catch (Exception ex)
            {
                CloseSessionContext(session);
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 종료 "));
        }

        private void CloseSessionContext(SessionContext session)
        {
            if(session!=null)
            {
                try
                {
                    session._socket.Shutdown(SocketShutdown.Both);
                    session._socket.Close();
                    session._status = SOCKET_STATUS.DISCONNECTED;

                }
                catch (Exception ex)
                {
                    Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
                }
            }

        }

        private void SendCallback(IAsyncResult ar)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 처리 "));

            try
            {
                // Retrieve the socket from the state object.  
                Socket socket = (Socket)ar.AsyncState;
                // Complete sending the data to the remote device.  
                int bytesSent = socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            }
            catch (Exception ex)
            {
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 종료 "));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            byte[] buffer = Utility.StringToByte("avogadro");
            int i = 0;
            foreach(var gateway in baseUnitList)
            {
                gateway.Send(buffer);
                Thread.Sleep(50);
                Console.WriteLine("i = {0} send..", i);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataFrame frame = new DataFrame();
            frame.SetTel("010-2723-4693");
            frame.SetMac("12345");
            frame.SetDateTime(DateTime.UtcNow);

            BBCmdBody body = new BBCmdBody();
            body.SetDateTime(DateTime.UtcNow);

            body.sensorCount = 5;
            body.deviceId = 0x23;
            for(int i=0;i< body.sensorCount;i++)
            {
                SensorActivity sensorActivity = new SensorActivity();

                sensorActivity.SetMac(String.Format($"mac_{i + 1}")); 
                sensorActivity.status = 0x02;
                sensorActivity.activity[0] = 0x0F;
                sensorActivity.activity[1] = 0x01;
                sensorActivity.activity[2] = 0x02;
                sensorActivity.activity[3] = 0x03;
                sensorActivity.activity[4] = 0x04;

                //for (int j = 0; j < 5; j++)
                //    sensorActivity.activity[j] =(UInt32)(0xFF000000 + 1);
                body.sensorActivityList.Add(sensorActivity);
            }

            for (int i = 0; i < 5; i++)
            {
                OneMinuteActivity oneMinuteActivity = new OneMinuteActivity();

                oneMinuteActivity.SetMac(String.Format($"mac_{i + 1}"));
                oneMinuteActivity.place = (byte)(i + 1);
                oneMinuteActivity.status = 0x03;

                oneMinuteActivity.activity[0] = 0x0F;
                oneMinuteActivity.activity[1] = 0x01;
                oneMinuteActivity.activity[2] = 0x02;
                oneMinuteActivity.activity[3] = 0x03;

                body.oneMinuteActivityList.Add(oneMinuteActivity);
            }
            frame.body = body;
            byte [] data = frame.Serialize();

            DataFrame target = new DataFrame();
            int ret = target.Deserialize(data, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            byte[] data = { 0xe9,0x00,0x3f,0x2a,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x05,0xff,0xff,0xff,0xff,0xff,0xff,0xff,0xff,
                            0xff,0xff,0xff,0xff,0x60,0x12,0x08,0x12,0x25,0x58,0x00,0x64,0x00,0x09,0xe5,0x07,
                            0x0a,0x13,0x07,0x17,0x12,0xbb,0x04,0xe5,0x07,0x0a,0x13,0x07,0x14,0x3a,0x23,0x60,
                            0x44,0x19,0xa8,0x18,0xbf,0x01,0x00,0x00,0x00,0x00,0x02,0x80,0x00,0x00,0x00,0xb8,
                            0x00,0x00,0x00,0x00,0x00,0x18,0x00,0x00,0x00,0x00,0x60,0x22,0x22,0x22,0x22,0x01,
                            0x01,0x00,0x00,0x00,0x00,0x44,0x20,0x60,0x00,0x09,0xc0,0x00,0x00,0x00,0x00,0x00,
                            0x00,0x00,0x00,0x00,0x00,0x4b,0x00,0x17,0xec,0xc5,0xb8,0x01,0x00,0x00,0x00,0x80,
                            0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x00,0x44,0x0e,0x00,0x00,0x00,0x81,0xf0,0xb0,
                            0x17,0x21,0x96,0x31,0x30,0x33,0x0f,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                            0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x60,0x22,0x22,0x22,0x22,
                            0x01,0x00,0x01,0x00,0x00,0x00,0x44,0x60,0x22,0x22,0x22,0x22,0x01,0x00,0x01,0x08,
                            0x18,0x00,0x02,0x60,0x44,0x19,0xa8,0x18,0xbf,0x14,0x01,0x0b,0x80,0x00,0x00,0x4b,
                            0x00,0x17,0xec,0xc5,0xb8,0x1e,0x01,0x10,0x38,0x00,0x00,0x4b,0x00,0x17,0xec,0xc5,
                            0xb8,0x1e,0x01,0x00,0x81,0xf0,0xb0,0x00,0x23 };


            byte[] test = new byte[4];
            test[0] = 0xF1;
            test[1] = 0xC2;
            test[2] = 0xD3;
            test[3] = 0xA4;
            String bitString = Utility.GetBinaryString(test);// Convert.ToString(data, 2).PadLeft(8, '0');
            byte[] test2 = Utility.ConvertBinaryStringToByte(bitString);
        
            

            DataFrame target = new DataFrame();
            int ret = target.Deserialize(data, 0);
            BBCmdBody body = (BBCmdBody)target.body;
            timeLinePick.SetOneMinutePickList(body.sensorActivityList);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SP_RESULT spResult;
            //var life = db.GetLifelog48List(out SP_RESULT spResult);

            LifeLog48 data = new LifeLog48();
            int i = 0;
            data.care_recipient_id = 100;
            UInt32 u32Value = 0;
            UInt16 u16Value = 0;
            byte value;


            byte[] result;
            byte[] result2;
            result = Utility.ConvertDecimalToByte("20211028");
            result2 = Utility.ConvertDecimalToByte("20211029");
            for (i = 0;i<60*48;)
            {
                if(i<60*48/2)
                    Array.Copy(result, 0, data.lifelog_date_1_list, i, result.Length);
                else
                    Array.Copy(result2, 0, data.lifelog_date_1_list, i, result2.Length);
                i += 4;
            }
            

            

            for(i=0;i<2880;i++)
            {
                value = (byte)((i % 10) + 1);
                data.validation_1_list[i] = 0x01;
                data.place_1_list[i] = Utility.sensorTypeList[i% Utility.sensorTypeList.Count].GetSensorCode();
                data.sleep_depth_1_list[i] = (byte)(i%4);
                data.outgoing_1_list[i] = value;
            }

            int j = 0;
            int index = 0;
            for (i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        index = 0;
                        for(j=0;j<48;j++)
                        {
                            data.place_code_h_p1_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p1_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p1_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p1_list[1] = (byte)Utility.sensorTypeList[(i+1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p1_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p1_list, 2, result.Length);



                        break;
                    case 1:

                        index = 0;
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p2_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p2_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p2_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p2_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p2_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p2_list, 2, result.Length);


                        break;
                    case 2:
                        index = 0;
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p3_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p3_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p3_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p3_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p3_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p3_list, 2, result.Length);


                        break;
                    case 3:

                        index = 0;
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p4_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p4_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p4_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p4_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p4_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p4_list, 2, result.Length);

                        break;
                    case 4:

                        index = 0;
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p5_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p5_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p5_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p5_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p5_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p5_list, 2, result.Length);


                        break;
                    case 5:

                        index = 0;
                       

                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p6_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p6_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p6_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p6_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p6_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p6_list, 2, result.Length);

                        break;
                    case 6:

                        index = 0;
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p7_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p7_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p7_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p7_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p7_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p7_list, 2, result.Length);


                        break;
                    case 7:
                        index = 0;

                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p1_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p8_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p8_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p8_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p8_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p8_list, 2, result.Length);


                        break;
                    case 8:

                        index = 0;
                        
                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p9_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p9_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p9_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p9_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p9_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p9_list, 2, result.Length);

                        break;

                    case 9:

                        index = 0;

                        for (j = 0; j < 48; j++)
                        {
                            data.place_code_h_p10_list[j] = Utility.sensorTypeList[j % Utility.sensorTypeList.Count].GetSensorCode(); ;

                            u16Value = (byte)((i % 255) + 1);
                            result = BitConverter.GetBytes(u16Value);
                            Array.Copy(result, 0, data.AIX_h_p10_list, index, result.Length);
                            index += result.Length;

                        }
                        data.place_code_d_p10_list[0] = (byte)Utility.sensorTypeList[i % Utility.sensorTypeList.Count].GetSensorCode();
                        data.place_code_d_p10_list[1] = (byte)Utility.sensorTypeList[(i + 1) % Utility.sensorTypeList.Count].GetSensorCode();

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p10_list, 0, result.Length);

                        u16Value = (byte)((i % 255) + 1);
                        result = BitConverter.GetBytes(u16Value);
                        Array.Copy(result, 0, data.AIX_d_p10_list, 2, result.Length);

                        break;
                }
            }





            for (i = 0; i < 5760;)
            {
                u16Value = (byte)((i % 255) + 1);
                result = BitConverter.GetBytes(u16Value);
                Array.Copy(result, 0, data.AIX_1_list, i, result.Length);
                i += result.Length;
            }

            data.AIX_1Eq0RepeatCount = 10;
            data.AIX_1Gt0RepeatCount = 12;

            for(i=0;i<8;)
            {
                u32Value = (byte)((i % 255) + 1);
                result = BitConverter.GetBytes(u32Value);
                Array.Copy(result, 0, data.total_AIX_d_list, i, result.Length);


                
                Array.Copy(result, 0, data.lifelog_date_d_list, i, result.Length);

                Array.Copy(result, 0, data.total_AIX_d_count_List, i, result.Length);
                Array.Copy(result, 0, data.total_sleep_start_time_d_list, i, result.Length);
                Array.Copy(result, 0, data.total_sleep_start_time_d_count_list, i, result.Length);
                Array.Copy(result, 0, data.total_sleep_end_time_d_list, i, result.Length);
                Array.Copy(result, 0, data.total_sleep_end_time_d_count_list, i, result.Length);
                Array.Copy(result, 0, data.total_bath_count_d_list, i, result.Length);
                Array.Copy(result, 0, data.total_bath_count_d_count_list, i, result.Length);
                Array.Copy(result, 0, data.total_bath_time_d_list, i, result.Length);

                Array.Copy(result, 0, data.total_bath_time_d_count_list, i, result.Length);
                Array.Copy(result, 0, data.total_outgoing_count_d_list, i, result.Length);
                Array.Copy(result, 0, data.total_outgoing_count_d_count_list, i, result.Length);
                Array.Copy(result, 0, data.total_outgoing_time_d_list, i, result.Length);
                Array.Copy(result, 0, data.total_outgoing_time_d_count_list, i, result.Length);
                i += result.Length;
            }



            data.validation_d_list[0] = 0x01;
            data.validation_d_list[1] = 0x01;

            for(i=0;i<2;i++)
            {
                u16Value = (byte)((i % 255) + 1);
                result = BitConverter.GetBytes(u16Value);
                Array.Copy(result, 0, data.AIX_d_list, i, result.Length);
                i += result.Length;
            }


            index = 0;
            int index2 = 0;
            for (i = 0; i < 48; i++)
            {
                data.validation_h_list[i] = 0x01;

                u32Value = (byte)((i % 255) + 1);
                result = BitConverter.GetBytes(u32Value);
                Array.Copy(result, 0, data.lifelog_date_h_list, index, result.Length);

                Array.Copy(result, 0, data.total_AIX_h_list, index, result.Length);
                Array.Copy(result, 0, data.total_AIX_h_count_list, index, result.Length);


                index += result.Length;

                result = BitConverter.GetBytes(u16Value);
                Array.Copy(result, 0, data.AIX_h_list, index2, result.Length);


                



                index2 += result.Length;
            }
       

            //for(i=0;i<192;)
            //{
            //    u32Value = (UInt32)(i + 10);
            //    result = BitConverter.GetBytes(u32Value);
            //    Array.Copy(result, 0, data.total_AIX_h_list, j, result.Length);
            //    Array.Copy(result, 0, data.total_AIX_h_count_list, j, result.Length);
            //    j += result.Length;
            //}

            for(int cnt=0; cnt < 100; cnt++)
            {
                data.care_recipient_id = cnt + 100;
                db.AddLifelog48(data, out spResult);
                Console.WriteLine("inser count  ={0}", cnt + 1);
            }
            

        }

        private DataFrame GetBBDataFrame()
        {
            DataFrame frame = new DataFrame();
            List<SensorActivity> oneMinutePickList = new List<SensorActivity>();
            //
            foreach (var timeline in timeLineList)
            {
                timeline.GetSensorActivityTime();
            }

            for(int i = 0;i<5;i++)
            {
                var activity = GetOnePickActivity(i, 2); // 활동량 많은 것으로 Pick
                if(activity!=null)
                    oneMinutePickList.Add(activity); 
                else
                {
                    /// 활동이 모두 0인 경우 place 가 미지정으로 설정된다. 
                    SensorActivity dummy = new SensorActivity();
                    dummy.sensorType = Utility.sensorTypeList.ElementAt(Utility.sensorTypeList.Count - 1);
                    oneMinutePickList.Add(dummy);
                }
            }

            frame.SetTel("");
            frame.SetMac("");
            frame.SetDateTime(DateTime.UtcNow);

            BBCmdBody body = new BBCmdBody();
            body.SetDateTime(DateTime.UtcNow);
            body.year = (UInt16)(int.Parse(cbYear.SelectedItem.ToString()));

            body.sensorCount = (byte) timeLineList.Count;
            body.deviceId = 0x23;
            for (int i = 0; i < body.sensorCount; i++)
            {
                SensorActivity sensorActivity = new SensorActivity();

                sensorActivity.SetMac("");
                sensorActivity.status = timeLineList[i].GetActivityStatus();
                for (int j = 0; j < 20; j++)
                    sensorActivity.activity[j] = timeLineList[i].sensorActivity.activity[j] ;// (UInt32)(0xFF000000 + 1);
                Console.WriteLine($"Sensor {i} = {Utility.GetBinaryString(sensorActivity.activity)}");
                body.sensorActivityList.Add(sensorActivity);
            }

            // 1분 활동량 데이터(PICK 한것)
            for (int i = 0; i < 5; i++)
            {
                OneMinuteActivity oneMinuteActivity = new OneMinuteActivity();

                oneMinuteActivity.SetMac(String.Format($"mac_{i + 1}"));
                oneMinuteActivity.place = (byte)oneMinutePickList[i].sensorType.GetSensorCode() ;// (Utility.ConvertHexaStringToByte(oneMinutePickList[i].sensorType.SensorCode)[0]);
                oneMinuteActivity.status = GetOneMinuteActivityStatus(i, oneMinutePickList); // 
                oneMinuteActivity.activity = Utility.GetActivityData(i, oneMinutePickList[i].activity) ;
                Console.WriteLine($"pick Sensor {i} = {Utility.GetBinaryString(oneMinuteActivity.activity)}");
                body.oneMinuteActivityList.Add(oneMinuteActivity);
            }
            frame.body = body;

            timeLinePick.SetOneMinutePickList(oneMinutePickList);
            return frame;
        }

        public SensorActivity GetOnePickActivity(int minute,int mode)
        {
            SensorActivity result = null;

            switch(mode)
            {
                case 1:
                    // 최종 활동 pick
                    result = FindActivityLast(minute);
                    break;
                case 2:// 활동량이 많은 것 pick
                    result = FindActivityMost(minute);
                break;
            }
            return result;
        }

        /// <summary>
        /// 최종 활동인 Place 선택
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public SensorActivity FindActivityLast(int minute)
        {
            SensorActivity result = null;
            return result;
        }

        /// <summary>
        /// 활동량 많은 Place 선택
        /// </summary>
        /// <param name="minute"></param>
        /// <returns></returns>
        public SensorActivity FindActivityMost(int minute)
        {
            SensorActivity result = null;
            UInt32 activityCount = 0;
            foreach(var timeline in timeLineList)
            {
                if(timeline.GetActivityTotalTime(minute)> activityCount)
                {
                    result = timeline.sensorActivity;
                    activityCount = timeline.GetActivityTotalTime(minute);
                }
            }
            return result;
        }

        public int AddLog(LOG_TYPE logType, String strLog)
        {
            
            String buf = String.Format("{0}\t[{1}]\t{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logType, strLog);


            BeginInvoke(_displayLog, new object[] { buf });

            return 1;
        }
        public void DisplayData(String log)
        {
            if (lbxLog.Items.Count > 1000)
                lbxLog.Items.RemoveAt(lbxLog.Items.Count - 1);
            lbxLog.Items.Insert(0, log);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopService();
        }

        private void SendBBCommand()
        {
            DataFrame dataFrame = GetBBDataFrame(); // 실시간 데이터 반영
            //dataFrame.SetMac(txtBuMac.Text); 
            var now = DateTime.Now;
            List<BaseUnit> targetList  = (from baseUnit in baseUnitList
                            where baseUnit._status == SOCKET_STATUS.CONNECTED &&
                                    baseUnit.nextSendTime <= now &&
                                    baseUnit.bSending == false
                            select baseUnit).ToList();


            if(targetList.Count()>0)
            {
                foreach(var baseUnit in targetList)
                {
                    baseUnit.bSending = true;
                }
                new Thread(() =>
                {
                    try
                    {
                        foreach (var baseUnit in targetList)
                        {
                            dataFrame.SetMac(baseUnit.bu_mac);
                            baseUnit.SendBBCommand(dataFrame);
                            baseUnit.nextSendTime = Utility.GetNextTime(DateTime.Now, sendPeriod);
                            Thread.Sleep(10);
                        }

                    }
                    catch (Exception ex)
                    {
                        
                    }
                    finally
                    {
                        foreach (var baseUnit in targetList)
                        {
                            baseUnit.bSending = false;
                        }
                    }
                }
                ).Start();
            }
        }

        private byte GetOneMinuteActivityStatus(int minute, List<SensorActivity> pickList)
        {
            byte result = 0x00;
            bool bInactive = true;

            // 모든 센서가 데이터 미수신인 경우 0xFF 로 미수신
            for (int i = 0; i < 5; i++)
            {
                bInactive &= timeLineList[i].GetInactiveState();
            }

            if(bInactive)
                result = 0x0F;
            else
            {
                if (pickList[minute].sensorType.SensorCode == "FF") // dummy 
                    result = 0x00;
                else
                    result = 0x01;
            }
            return result;
        }
        private void DisplayFrameInfo(DataFrame frame)
        {
            String strFrame;
            Utility.AddLog(LOG_TYPE.LOG_INFO, "**************** Frame Info start **************");
            BBCmdBody body = (BBCmdBody)frame.body;

            foreach(var oneMinuteActivity in body.oneMinuteActivityList)
            {
                strFrame = String.Format($"One Minute activity mac={Utility.PrintHexaString(oneMinuteActivity.mac, oneMinuteActivity.mac.Length)} ");
                Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);


                strFrame = String.Format($"One Minute activity place={String.Format("0x{0:X2}", oneMinuteActivity.place)} ");
                Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);

                strFrame = String.Format($"One Minute activity status={String.Format("0x{0:X2}", oneMinuteActivity.status)}");
                Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);
            }

            foreach (var sensorActivity in body.sensorActivityList)
            {

                strFrame = String.Format($"Sensor activity status={String.Format("0x{0:X2}",sensorActivity.status)}");
                Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);

                strFrame = String.Format($"Sensor activity mac={Utility.PrintHexaString(sensorActivity.mac, sensorActivity.mac.Length)} ");
                Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);

            }

            strFrame = String.Format($"BB DataFrame year={body.year} month={body.month}, day={body.day}, hour={body.hour}, minute={body.minute}, second={body.second} ");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);


            strFrame = String.Format($"BB DataFrame Sensor 갯수={body.sensorCount} deviceId={body.deviceId}");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);




            strFrame = String.Format($"Frame CMD={String.Format("0x{0:X2} ", frame.cmd)}");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);

            strFrame = String.Format($"Frame year={frame.year} month={frame.month}, day={frame.day}, hour={frame.hour}, minute={frame.minute}, second={frame.second} ");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);


            strFrame = String.Format($"Frame bu_mac={Utility.PrintHexaString(frame.mac, frame.mac.Length)} ");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);


            strFrame = String.Format($"Frame version={String.Format("0x{0:X2}",frame.version)} provider={frame.provider}, transactionId={Utility.PrintHexaString(frame.transactionId, frame.transactionId.Length)}");
            Utility.AddLog(LOG_TYPE.LOG_INFO, strFrame);

            Utility.AddLog(LOG_TYPE.LOG_INFO, "**************** Frame Info end **************");

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"BaseUnit {baseUnitList.Count} 개 로드 성공"));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frame = GetBBDataFrame();
            frame.mac = Utility.ConvertHexaStringToByte(txtBuMac.Text);
            //var packet = frame.Serialize();
            //Utility.PrintHexaString(packet, packet.Length);

            DisplayFrameInfo(frame);
            BaseUnit gateway = new BaseUnit();
            gateway.bu_mac = txtBuMac.Text;
            gateway.SetAddress(txtAddress.Text, int.Parse(txtPort.Text), CLIENT_TYPE.VDS_CLIENT, SigleConnectCallback, SigleReadCallback, SendCallback);
            gateway.StartConnect();
            if (gateway._status == SOCKET_STATUS.CONNECTED)
            {
                gateway.SendBBCommand(frame);
                DisplayFrameInfo(frame);
            }
        }


        private int SigleConnectCallback(SessionContext session, SOCKET_STATUS status)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 처리 "));
            int nResult = 0;
            String strLog;
            try
            {
                session._status = SOCKET_STATUS.CONNECTED;
                session._socket.BeginReceive(session.buffer, 0, SessionContext.BufferSize, 0,
                    new AsyncCallback(SigleReadCallback), session);

                strLog = String.Format("제어기-->영상VDS ( 접속 성공)");
                Console.WriteLine(strLog);
                Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);


                nResult = 1;
            }
            catch (Exception ex)
            {
                session._status = SOCKET_STATUS.DISCONNECTED;
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 종료 "));
            return nResult;
        }

        public void SigleReadCallback(IAsyncResult ar)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 처리 "));

            String content = String.Empty;
            String strLog;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            SessionContext session = (SessionContext)ar.AsyncState;
            Socket socket = session._socket;
            try
            {
                // Read data from the client socket.
                int bytesRead = socket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    strLog = String.Format("영상VDS --> 제어기 ReadCallback {0} 바이트 데이터 수신", bytesRead);
                    Console.WriteLine(strLog);
                    Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);

                }
                CloseSessionContext(session);
            }
            catch (Exception ex)
            {
                CloseSessionContext(session);
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().ReflectedType.Name + ":" + MethodBase.GetCurrentMethod().Name} 종료 "));
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
