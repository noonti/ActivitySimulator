using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NAServer
{
    public partial class Form1 : Form
    {

        ServerSocket _server = new ServerSocket();

        public Form1()
        {
            InitializeComponent();
        }



        public void AcceptCtrlCallback(IAsyncResult ar)
        {
            // Get the socket that handles the client request.  
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().Name} 처리 "));
            String strLog;
            try
            {
                // Create the state object.  
                Socket serverSocket = (Socket)ar.AsyncState;
                Socket socket = serverSocket.EndAccept(ar);
                //if (socket.Connected)
                {
                    SessionContext sessionContext = new SessionContext();
                    sessionContext._socket = socket;
                    socket.BeginReceive(sessionContext.buffer, 0, SessionContext.BufferSize, 0,
                        new AsyncCallback(GatewayReadCallback), sessionContext);

                    strLog = String.Format("Remote Client accepted");
                    Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);
                    Console.WriteLine(strLog);
                }

            }
            catch (Exception ex)
            {
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            finally
            {
                _server.SetAcceptProcessEvent();
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().Name} 종료 "));
        }


        public void GatewayReadCallback(IAsyncResult ar)
        {
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().Name} 처리 "));

            String content = String.Empty;
            String strLog;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            SessionContext sessionContext = (SessionContext)ar.AsyncState;
            Socket socket = sessionContext._socket;
            try
            {
                // Read data from the client socket.
                int bytesRead = socket.EndReceive(ar);
                Console.WriteLine("ReadCallback {0} byte", bytesRead);
                if (bytesRead > 0)
                {
                    strLog = String.Format("ReadCallback {0} 바이트 데이터 수신", bytesRead);
                    Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);

                    byte[] packet = new byte[bytesRead];
                    Array.Copy(sessionContext.buffer, 0, packet, 0, bytesRead);

                    Console.WriteLine($"received packet size = {bytesRead}");

                    strLog = Utility.PrintHexaString(packet, bytesRead);
                    Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"PrintHexa:{strLog}"));

                    Send(sessionContext, packet);
                    // Not all data received. Get more.  
                    socket.BeginReceive(sessionContext.buffer, 0, SessionContext.BufferSize, 0,
                    new AsyncCallback(GatewayReadCallback), sessionContext);

                }
                else
                {
                    if (socket != null )
                    {
                        strLog = String.Format("원격 클라이언트 연결 종료111");
                        Utility.AddLog(LOG_TYPE.LOG_INFO, strLog);
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            Utility.AddLog(LOG_TYPE.LOG_INFO, String.Format($"{MethodBase.GetCurrentMethod().Name} 종료 "));
        }

        public void Send(SessionContext session,byte[] byteData)
        {
            try
            {

                // Convert the string data to byte data using ASCII encoding.  
                //byte[] byteData = Encoding.ASCII.GetBytes(data);

                // Begin sending the data to the remote device.  
                session._socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), session._socket);
            }
            catch (Exception ex)
            {
                Utility.AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
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


        private void button1_Click(object sender, EventArgs e)
        {
            _server.SetAddress(txtAddress.Text, int.Parse(txtPort.Text), CLIENT_TYPE.VDS_CLIENT, AcceptCtrlCallback);
            _server.StartManager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _server.StopManager();
        }
    }



    
}
