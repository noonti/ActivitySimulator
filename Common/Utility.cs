using Common.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public delegate int AddLogDelegate(LOG_TYPE type, String _log);
    public delegate int ConnectCallback(SessionContext session, SOCKET_STATUS status);


    public enum LOG_TYPE
    {
        LOG_INFO = 1,
        LOG_WARN = 2,
        LOG_ERROR = 3
    }


    public enum CLIENT_TYPE
    {
        NONE = 0,
        VDS_CLIENT = 1,
        KICT_EVNT_CLIENT = 2, // KICT event   제어기->센터(10000)
        KICT_CTRL_CLIENT = 3, // Control Client 센터->제어기(12000)
        KICT_CLIB_CLIENT = 4, // Calibration Client  센터->제어기(12000)
        VIDEO_VDS_CLIENT = 5

    }


    public enum SOCKET_STATUS
    {
        DISCONNECTED = 1,
        CONNECTING = 2,
        CONNECTED = 3,
        AUTHORIZED = 4,
        UNAUTHORIZE = 5
    };

    public static class Utility
    {
        public const int MAX_GATEWAY_COUNT = 100;

        public static AddLogDelegate _addLog = null;

        public static List<SensorType> sensorTypeList = new List<SensorType>();

        public static Color[] colorList = new Color[10];

        public static int AddLog(LOG_TYPE logType, String strLog)
        {
            int nResult = 0;
            if (_addLog != null)
            {
                _addLog(logType, strLog);
                nResult = 1;
            }
            return nResult;
        }


        public static String ByteToString(byte[] strByte)
        {
            String result = Encoding.Default.GetString(strByte);
            return result;
        }

        public static byte[] StringToByte(String str)
        {
            byte[] result = Encoding.UTF8.GetBytes(str);
            return result;
        }


        // Big Endian을 little엔디안으로 return UInt32
        public static UInt16 toLittleEndianInt16(byte[] bigNumber)
        {
            byte[] temp = new byte[2];
            bigNumber.CopyTo(temp, 0);
            Array.Reverse(temp);
            return BitConverter.ToUInt16(temp, 0);
        }




        // big엔디안으로 return Byte[]
        public static Byte[] toBigEndianInt16(UInt16 number)
        {
            byte[] temp = new byte[2];
            temp = BitConverter.GetBytes(number);
            Array.Reverse(temp);
            return temp;
        }


        // Big Endian을 little엔디안으로 return UInt32
        public static UInt32 toLittleEndianInt32(byte[] bigNumber)
        {
            byte[] temp = new byte[4];
            bigNumber.CopyTo(temp, 0);
            Array.Reverse(temp);
            return BitConverter.ToUInt32(temp, 0);
        }


        // Big Endian을 little엔디안으로 return UInt32
        public static UInt64 toLittleEndianInt64(byte[] bigNumber)
        {
            byte[] temp = new byte[8];
            bigNumber.CopyTo(temp, 0);
            Array.Reverse(temp);
            return BitConverter.ToUInt64(temp, 0);
        }

        // big엔디안으로 return Byte[]
        public static Byte[] toBigEndianInt32(UInt32 number)
        {
            byte[] temp = new byte[4];
            temp = BitConverter.GetBytes(number);
            Array.Reverse(temp);
            return temp;
        }


        public static byte[] GetDateTime(DateTime time)
        {
            byte[] result = new byte[8];
            int idx = 0;

            //0 1 year
            toBigEndianInt16((UInt16)time.Year).CopyTo(result, idx);
            idx += 2;
            //2 month
            result[idx++] = (byte)time.Month;
            //3 day
            result[idx++] = (byte)time.Day;
            //4 hour
            result[idx++] = (byte)time.Hour;
            result[idx++] = (byte)time.Minute;
            result[idx++] = (byte)time.Second;
            result[idx++] = (byte)(time.Millisecond / 10);
            return result;
        }


        public static bool Connect(ref Socket socket, String address, int port, int timeout)
        {
            //IPAddress ipAddress = IPAddress.Parse(address);// ipHostInfo.AddressList[0];
            //IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
            bool bResult = false;
            try
            {
                IPAddress ipAddress = IPAddress.Parse(address);// ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                socket.Blocking = false;
                socket.Connect(remoteEP);
                bResult = true;

            }
            catch (SocketException ex)
            {

                if (ex.SocketErrorCode == SocketError.WouldBlock)
                {
                    ArrayList socketList = new ArrayList();
                    socketList.Add(socket);
                    Socket.Select(null, socketList, null, timeout * 1000 * 1000);
                    if (socketList.Count == 0)
                    {
                        //Trace.WriteLine(ex.ToString());
                        AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
                        return false;
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
                return false;
            }
            return bResult;
        }


        public static byte ByteToBCD(byte value)
        {
            byte Result = 0;
            try
            {
                Result = (byte)(((value / 10) << 4) + value % 10);

            }
            catch (Exception ex)
            {
                Result = 0;
                AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            return Result;
        }

        public static byte BCDToByte(byte value)
        {
            byte Result = 0;
            try
            {
                Result = (byte)((value & 15) + (value >> 4) * 10);
            }
            catch (Exception ex)
            {
                Result = 0;
                AddLog(LOG_TYPE.LOG_ERROR, ex.Message.ToString() + "\n" + ex.StackTrace.ToString());
            }
            return Result;
        }


        public static String PrintHexaString(byte[] data, int length)
        {
            String Result = String.Empty;
            for (int i = 0; i < length; i++)
            {
                if (i % 16 == 0)
                    Console.WriteLine("");
                Result += String.Format("0x{0:X2} ", data[i]);

            }
            Console.WriteLine(Result);
            return Result;
        }

        public static String GetTransactionId()
        {
            String result = DateTime.Now.ToString("MMddHHmmss");
            return result;
        }

        public static byte[] ToLittleEndian(UInt32 number, int bytes)
        {
            byte[] temp = new byte[bytes];
            temp = BitConverter.GetBytes(number);
            return temp;
        }

        public static byte[] GetActivity(UInt32 number)
        {
            UInt32 value = number & 0x3FFFFFFF;
            byte[] temp = new byte[4];
            temp = BitConverter.GetBytes(value);
            return temp;
        }


        public static String GetBinaryString(byte[] data)
        {
            String result = String.Empty;

            for (int i = 0; i < data.Length; i++)
            {
                result += Convert.ToString(data[i], 2).PadLeft(8, '0');
            }
            return result;
        }


        public static byte[] ConvertBinaryStringToByte(String data)
        {
            byte[] result = new byte[data.Length / 8];
            String value;
            int i = 0;
            int j = 0;
            while (i < data.Length)
            {
                value = data.Substring(i, 8);
                result[j++] = Convert.ToByte(value, 2);
                i += 8;
            }

            return result;
        }

        public static byte[] GetActivityData(int minute, byte[] activityData)
        {
            byte[] result;
            // activityData 20 byte 중에서 minute 에 해당하는 4바이트 
            String bitString = Utility.GetBinaryString(activityData);
            // 10bit 이후 부터...데이터 시작.
            int startIndex = 10 + minute * 30;
            String value = "00" + bitString.Substring(startIndex, 30);
            result = Utility.ConvertBinaryStringToByte(value);
            return result;

        }

        public static DateTime GetNextTime(DateTime date, int second)
        {
            DateTime result = date.AddSeconds(second);
            return result;
        }

        //public static byte[] GetStringToHexa(String hexaString)
        //{
        //    byte[] convertArr = new byte[hexaString.Length / 2];

        //    for (int i = 0; i < convertArr.Length; i++)
        //    {
        //        convertArr[i] = Convert.ToByte(hexaString.Substring(i * 2, 2), 16);
        //    }
        //    return convertArr;
        //}


        public static byte[] ConvertHexaStringToByte(String hexa)
        {
            byte[] convertArr = new byte[hexa.Length / 2];
            for (int i = 0; i < convertArr.Length; i++)
            {
                convertArr[i] = Convert.ToByte(hexa.Substring(i * 2, 2), 16);
                //convertArr[i] = Convert.ToByte(, 16);
            }
            return convertArr;
        }


        public static byte[] ConvertDecimalToByte(String str)
        {
            byte[] convertArr = new byte[str.Length / 2];
            for (int i = 0; i < convertArr.Length; i++)
            {
                convertArr[i] = (byte)int.Parse(str.Substring(i * 2, 2));
                //convertArr[i] = Convert.ToByte(, 16);
            }
            return convertArr;

        }

    }
}
