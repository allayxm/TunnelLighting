using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace NCLT.TunnelLighting.Protocol
{
    /// <summary>
    /// MoudBus接收发送状态
    /// </summary>
    public enum ModeActivityEnum : short { TX=0,RX=1 }

    public class Modbus:IDisposable
    {
        #region 发送数据事件
        /// <summary>
        /// 发送数据
        /// </summary>
        public class ModbusState_TXEventArgs : EventArgs
        {
            public ModbusState_TXEventArgs()
            {
               
            }
        }
        public delegate void ModbusState_TXEventHandler(object sender, ModbusState_TXEventArgs e);
        public event ModbusState_TXEventHandler OnModbusState_TXEvent;
        #endregion

        #region 接收数据事件
        /// <summary>
        /// 接收数据
        /// </summary>
        public class ModbusState_RXEventArgs : EventArgs
        {
            public ModbusState_RXEventArgs()
            {

            }
        }
        public delegate void ModbusState_RXEventHandler(object sender, ModbusState_RXEventArgs e);
        public event ModbusState_RXEventHandler OnModbusState_RXEvent;
        #endregion

        #region 私有变量
        SerialPort m_SerialPort = null;
        #endregion

        #region 构造
        public Modbus( string PortName,int BaudRate)
        {
            m_SerialPort = new SerialPort(PortName, BaudRate);
            m_SerialPort.DataBits      = 8;
            m_SerialPort.StopBits      = StopBits.One;
            m_SerialPort.ParityReplace = 0x00;
            m_SerialPort.Parity        = Parity.Even;
            m_SerialPort.DtrEnable     = true;
            m_SerialPort.RtsEnable     = true;
            
            m_SerialPort.Open();
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="DataArea"></param>
        public byte[] SendDataPack(byte TargetAddress, byte[] DataArea)
        {
            byte[] vReceiveBytes = null;
            if (m_SerialPort.IsOpen)
            {
                string vDataPack = string.Format(":{0:00}060001", TargetAddress);
                string vDataAreaPack = "";
                foreach (byte vTempDataArea in DataArea)
                {
                    vDataAreaPack += string.Format("{0:X2}", vTempDataArea);
                }
                vDataPack += vDataAreaPack;
                byte vCRC = CalcCRC(vDataPack.Substring(5));
                vDataPack += string.Format("{0:X2}", vCRC);
                m_SerialPort.Write(vDataPack);
                if (OnModbusState_TXEvent != null)
                    OnModbusState_TXEvent(this, new ModbusState_TXEventArgs());
                Thread.Sleep(100);
                System.Diagnostics.Debug.WriteLine("->目标地址{0} 发送的数据包→{1}", TargetAddress, vDataPack);

                bool vStopFlag = false;
                DateTime vStartTime = DateTime.Now;
                do
                {
                    int vMilliseconds = (DateTime.Now - vStartTime).Milliseconds;
                    if (vMilliseconds > 280) //接受数据超时
                    {
                        vStopFlag = true;
                        System.Diagnostics.Debug.WriteLine("接收数据超时{0}毫秒 目标地址→{1} ", vMilliseconds, TargetAddress);
                        break;
                    }
                    int vBytesToRead = m_SerialPort.BytesToRead;
                    if (vBytesToRead >= 35)
                    {
                        if (m_SerialPort.ReadByte() == 58)
                        {
                            vStopFlag = true;
                            byte[] vTempReceiveBytes = new byte[35];
                            m_SerialPort.Read(vTempReceiveBytes, 1, 34);
                            vTempReceiveBytes[0] = 58;
                            vReceiveBytes = new byte[17];
                            vReceiveBytes[0] = vTempReceiveBytes[0];
                            int vPoint = 1;
                            for (int i = 1; i < vTempReceiveBytes.Length; i += 2)
                            {
                                if (vPoint < vReceiveBytes.Length)
                                {
                                    try
                                    {
                                        vReceiveBytes[vPoint] = Convert.ToByte(string.Format("0x{0}{1}", (char)vTempReceiveBytes[i], (char)vTempReceiveBytes[i + 1]), 16);
                                    }
                                    catch
                                    {
                                        System.Diagnostics.Debug.WriteLine("错误的数据包:{0}:{1}", (char)vTempReceiveBytes[i], (char)vTempReceiveBytes[i + 1]);
                                        vReceiveBytes = null;
                                        Thread.Sleep(150);
                                        break;
                                    }
                                    vPoint++;
                                }
                            }
                            //计算CRC校验
                            if (vReceiveBytes != null)
                            {
                                byte vCRCCode = CalcCRC(getCRCDataPack_Receive(vReceiveBytes));
                                if (vCRCCode != vReceiveBytes[vReceiveBytes.Length - 1])
                                    vReceiveBytes = null;
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("->收到的数据包{0},耗时{1}", BitConverter.ToString(vReceiveBytes), (DateTime.Now - vStartTime).Milliseconds);
                                    if (OnModbusState_RXEvent != null)
                                        OnModbusState_RXEvent(this, new ModbusState_RXEventArgs());
                                    Thread.Sleep(60);
                                }
                            }
                        }
                    }
                } while (!vStopFlag);
            }
            return vReceiveBytes;
        }

        /// <summary>
        /// 取得需要校验的数据包段
        /// </summary>
        /// <param name="DataPack"></param>
        /// <returns></returns>
        byte[] getCRCDataPack_Receive(byte[] DataPack)
        {
            byte[] vCRCDataPack = new byte[13];
            int vPoint = 0;
            for (int i = 3; i < 16; i++)
            {
                vCRCDataPack[vPoint] = DataPack[i];
                vPoint++;
            }
            return vCRCDataPack;
        }

        #endregion

        #region 计算CRC值
        public static byte CalcCRC( string vCRCDataPack)
        {
            byte vCRC = 0x00;
            for (int i = 0; i < vCRCDataPack.Length; i = i + 2)
            {
                vCRC += Convert.ToByte(vCRCDataPack.Substring(i, 2), 16);
            }
            return vCRC;
        }

        public static byte CalcCRC(byte[] vCRCDataPack)
        {
            byte vCRC = 0x00;
            for (int i = 0; i < vCRCDataPack.Length; i++)
            {
                vCRC += vCRCDataPack[i];
            }
            return vCRC;
        }
        #endregion

        #region 析构
        public void Dispose()
        {
            if (m_SerialPort != null)
            {
                m_SerialPort.Close();
                m_SerialPort.Dispose();
                m_SerialPort = null;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
