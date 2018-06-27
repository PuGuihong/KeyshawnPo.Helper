using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace KeyshawnPo.Helper.Lib.Applay.LED
{
    /// <summary>
    /// LED控制帮助类
    /// </summary>
    public class LEDHelper
    {
        private static SerialPort serialPort = new SerialPort();
        static object synchLock = new object();
        /// <summary>
        /// 发送信息到LED
        /// </summary>
        /// <param name="comport">串口名：COM5</param>
        /// <param name="bps">波特率，默认9600</param>
        /// <param name="address">控制卡地址</param>
        /// <param name="msg">发送的消息</param>
        /// <param name="showmode">显示效果00:立即显示 01：右左滚动 07：闪动</param>
        /// <returns></returns>
        public static bool Send(string comport, int bps, int address, string msg, string showmode)
        {
            //检测串口是否开启
            #region
            if (!serialPort.IsOpen)
            {
                serialPort.PortName = comport;
                serialPort.BaudRate = bps;
                try
                {
                    serialPort.Open();
                }
                catch (Exception e)
                {
                    serialPort = new SerialPort();
                    return false;
                }
            }
            #endregion
            //中文编码 请：C7 EB 号：BA C5  窗口：B4 B0 BF DA
            lock (synchLock)
            {
                string addrHex = address.ToString("X2");
                int lenHex = System.Text.Encoding.ASCII.GetBytes(msg).Length;
                String st1 = "AA 55 00 " + addrHex + "";
                String st2 = ("04 00 00 " + addrHex + " 11 " + (lenHex - 1).ToString("X2") + " 01");
                String st3 = ("00 01 00 " + (lenHex + 1).ToString("X2") + " 11 " + getHexByStr(msg) + "");
                st3 = getEorSum(st3);

                String st5 = ("AA 55 00 " + addrHex + "");
                String st6 = ("04 00 00 " + addrHex + " 02 00 01");
                String st7 = ("00 01 00 04 00 " + showmode + " 06 02 00");
                st7 = getEorSum(st7);

                if (!SendToLED(st1))
                    return false;
                if (!SendToLED(st2))
                    return false;
                if (!SendToLED(st3))
                    return false;
                if (!SendToLED(st5))
                    return false;
                if (!SendToLED(st6))
                    return false;
                if (!SendToLED(st7))
                    return false;
                return true;
            }
        }
        /// <summary>
        /// 获取16进制码
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string getHexByStr(string str)
        {
            StringBuilder builder = new StringBuilder();
            byte[] buf = System.Text.Encoding.ASCII.GetBytes(str);
            //依次的拼接出16进制字符串
            foreach (byte b in buf)
            {
                builder.Append(b.ToString("X2") + " ");
            }
            return builder.ToString();
        }

        private static string getEorSum(string str)
        {
            //我们不管规则了。如果写错了一些，我们允许的，只用正则得到有效的十六进制数
            MatchCollection mc = Regex.Matches(str, @"(?i)[\da-f]{2}");
            List<byte> buf = new List<byte>();//填充到这个临时列表中
            //依次添加到列表中
            foreach (Match m in mc)
            {
                buf.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));
            }
            byte[] array = buf.ToArray();
            byte VerifyByte = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                VerifyByte = (byte)(VerifyByte ^ array[i]);
            }
            str = str + " " + VerifyByte.ToString("X2");
            return str;
        }
        private static bool SendToLED(string str)
        {

            //我们不管规则了。如果写错了一些，我们允许的，只用正则得到有效的十六进制数
            MatchCollection mc = Regex.Matches(str, @"(?i)[\da-f]{2}");
            List<byte> buf = new List<byte>();//填充到这个临时列表中
            //依次添加到列表中
            foreach (Match m in mc)
            {
                buf.Add(byte.Parse(m.Value, System.Globalization.NumberStyles.HexNumber));
            }
            if (!SendPacket(buf.ToArray()))
            {
                return false;
            }
            byte[] data = null;
            bool b = RecvPacket(ref data);
            return true;
        }

        private static bool SendPacket(byte[] WriteBuffer)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                foreach (byte b in WriteBuffer)
                {
                    if (builder.Length == 0)
                    {
                        builder.Append(b.ToString("X2"));
                    }
                    else
                    {
                        builder.Append("-" + b.ToString("X2"));
                    }
                }

                serialPort.Write(WriteBuffer, 0, WriteBuffer.Length);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private static bool RecvPacket(ref byte[] data)
        {
            List<byte> recvData = new List<byte>();
            try
            {
                while (true)
                {
                    StringBuilder builder = new StringBuilder();

                    //接收数据
                    while (serialPort.BytesToRead > 0 && recvData.Count < 5)
                    {
                        int d = serialPort.ReadByte();
                        recvData.Add(byte.Parse(d.ToString()));

                        if (builder.Length == 0)
                        {
                            builder.Append(d.ToString("X2"));
                        }
                        else
                        {
                            builder.Append("-" + d.ToString("X2"));
                        }

                    }

                    if (recvData.Count != 0)
                    {
                        data = recvData.ToArray();
                        return true;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
