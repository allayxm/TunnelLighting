using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using NCLT.TunnelLighting.DB;
using NCLT.TunnelLighting.Sensor;
using NCLT.TunnelLighting.Protocol;
using NCLT.TunnelLighting.LightControl;

namespace NCLT.TunnelLighting.ControlCell
{
    public struct ReceiveDataStruct
    {
        public int? Port1;
        public int? Port2;
        public bool? CarSensorPowerDown;
        public ulong? CarNumber;
        public bool? CarCoilPowerDown;
        public double? EmergencyPowerVoltage;
        public bool? PowerDown;
        public int? PWM;
    }

    public struct OutLightenessStruct
    {
        public byte Address;
        public int PortNumber;
        public int LightenessValue;
        public DateTime ReceiveTime;
    }

    public struct CarSensorDataStruct
    {
        public byte Address;
        public ulong CarNumber;
        public DateTime ReceiveTime;
        public bool PowerDown;
    }

    public enum ModuleState  : short { Normal = 0, Timeout = 1 };
    public enum InfoLevel    : short { Error=0,Alert=1,Message=2 };
    public enum ControlState : short { Normal = 0, Instancy = 1 };
    
    public class ControlModuleManage:IDisposable
    {
        #region 私有变量
        DBClass m_DBClass = null;
        List<ControlModule> m_ControlModules = null;
        //线程运行标志(总)
        bool m_RunThreadStopFlag = true;
        //收发数据线程
        Thread m_MessageThread = null;
        //数据处理线程
        Thread m_DataTransactThread = null;
        //状态检查线程
        Thread m_StateCheckupThread = null;

        ControlState m_State = ControlState.Normal;

        bool m_DebugMode = false;
        Debug m_Debug = null;

        Modbus m_Modbus = null;
        LightControlPWM m_LightControlPWM = null;
        byte[] m_HighLevelAddressList = null;  //高优先级列表
        byte[] m_LowLevelAddressList = null;  //低优先级列表
        Queue<byte[]> m_ReceiveDataQueue = null; //接收到的数据队列
        #endregion

        #region 公共变量
        //系统配置
        const int TimeValve = 500; //任务执行时间阀
        int m_LigthTimeDelay; //灯光延时(分钟)
        DateTime m_LastCarTime; //最后一辆车经过的时间
        int m_CommunicationTimeOut; //通讯超时时间(秒)
        int m_LightFixTime; //照度调校时间(分钟)
        int m_LightAllowError; //主备照度允许误差
        //照度(入口)
        OutLightenessStruct m_RK_MainOutLighteness;
        OutLightenessStruct m_RK_SlaveOutLighteness;
        DateTime m_RK_LightFixTimeValve; //入口段照度校正时间阀
        //照度(出口)
        OutLightenessStruct m_CK_MainOutLighteness;
        OutLightenessStruct m_CK_SlaveOutLighteness;
        DateTime m_CK_LightFixTimeValve; //出口段照度校正时间阀
        //车辆传感器
        CarSensorDataStruct m_MainCarSensorData;
        CarSensorDataStruct m_SlaveCarSensorData;
        #endregion

        #region 事件
        public class ReceiveDataEventArgs : EventArgs
        {
            public readonly int ContorlID;
            public readonly ReceiveDataStruct ReceiveData;
            public ReceiveDataEventArgs(int ContorlIDValue, ReceiveDataStruct ReceiveDataValue)
            {
                ContorlID = ContorlIDValue;
                ReceiveData = ReceiveDataValue;
            }
        }
        public delegate void ReceiveDataEventArgsEventHandler(object sender, ReceiveDataEventArgs e);
        public event ReceiveDataEventArgsEventHandler OnReceiveDataEventArgsEvent;

        //Modbus活动状态
        public class ModbusActivityStateEventArgs : EventArgs
        {
            public readonly ModeActivityEnum Activity;
            public ModbusActivityStateEventArgs(ModeActivityEnum vActivity)
            {
                Activity = vActivity;
            }
        }
        public delegate void ModbusActivityStateEventHandler(object sender, ModbusActivityStateEventArgs e);
        public event ModbusActivityStateEventHandler OnModbusActivityStateEventArgsEvent;

        /// <summary>
        /// 输出信息事件
        /// </summary>
        public class OutputMessageEventArgs : EventArgs
        {
            public readonly InfoLevel Level;
            public readonly string Message;
            public readonly DateTime Time;
            public OutputMessageEventArgs(InfoLevel vLevel, string vMessage)
            {
                Level = vLevel;
                Message = vMessage;
                Time = DateTime.Now;
            }
        }
        public delegate void OutputMessageEventHandler(object sender, OutputMessageEventArgs e);
        public event OutputMessageEventHandler OnOutputMessageEvent;
        #endregion

        #region 构造
        public ControlModuleManage( string AppPath )
        {
            m_DBClass = new DBClass();
            initControlModules();
            initCommonalityVar();
            m_LightControlPWM = new LightControlPWM(AppPath);
            //调试模式
            if ( m_DebugMode )
                m_Debug = new Debug();
        }
        #endregion

        #region 公有方法
        public bool AddressIsExist( int ControlID,byte Address )
        {
            foreach (ControlModule vTempControlModule in m_ControlModules)
            {
                if (vTempControlModule.Address == Address && ControlID != vTempControlModule.ID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 取得所有未使用的照度传感器
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoUsingLighteness()
        {
            DataTable vResultDataTable = new DataTable();
            vResultDataTable = m_DBClass.Select_ControlBox_DK((short)SensorTypeEnum.LightenessSensor, (short)SensorTypeEnum.COSensor);
            DataTable vNoUsingDataTable = new DataTable();
            vNoUsingDataTable.Columns.Add("ID", typeof(int));
            vNoUsingDataTable.Columns.Add("模块名称", typeof(string));
            vNoUsingDataTable.Columns.Add("模块ID", typeof(int));
            vNoUsingDataTable.Columns.Add("模块地址", typeof(byte));
            vNoUsingDataTable.Columns.Add("端口号", typeof(Int16));
            vNoUsingDataTable.Columns.Add("说明", typeof(string));
            vNoUsingDataTable.PrimaryKey = new DataColumn[] { vNoUsingDataTable.Columns["ID"] };
            vNoUsingDataTable.AcceptChanges();

            int vRowID = 0;
            foreach (DataRow vTempRow in vResultDataTable.Rows)
            {
                short vDK1 = DBConvert.ToInt16(vTempRow["端口1"]);
                short vDK2 = DBConvert.ToInt16(vTempRow["端口2"]);

                if (vDK1 == (short)SensorTypeEnum.LightenessSensor)
                {
                    vRowID++;
                    DataRow vNewRow1 = vNoUsingDataTable.NewRow();
                    vNewRow1["ID"] = vRowID;
                    vNewRow1["模块名称"]   = vTempRow["名称"];
                    vNewRow1["模块ID"]     = vTempRow["ID"];
                    vNewRow1["模块地址"]   = vTempRow["设备地址"];
                    vNewRow1["端口号"]   = 1;
                    vNewRow1["说明"] = string.Format("{0}模块 地址:{1} 端口{2}",vNewRow1["模块名称"],vNewRow1["模块地址"],vNewRow1["端口号"]);
                    vNoUsingDataTable.Rows.Add(vNewRow1);
                }

                if ( vDK2 == (short)SensorTypeEnum.LightenessSensor)
                {
                    vRowID++;
                    DataRow vNewRow2 = vNoUsingDataTable.NewRow();
                    vNewRow2["ID"] = vRowID;
                    vNewRow2["模块名称"]  = vTempRow["名称"];
                    vNewRow2["模块ID"]   = vTempRow["ID"];
                    vNewRow2["模块地址"]  = vTempRow["设备地址"];
                    vNewRow2["端口号"]   = 2;
                    vNewRow2["说明"] = string.Format("{0}模块 地址:{1} 端口{2}", vNewRow2["模块名称"], vNewRow2["模块地址"], vNewRow2["端口号"]);
                    vNoUsingDataTable.Rows.Add(vNewRow2);
                }
            }
            vNoUsingDataTable.AcceptChanges();
            return vNoUsingDataTable;
        }

        /// <summary>
        /// 取得所有未使用的
        /// </summary>
        /// <returns></returns>
        public DataTable GetNoUsingCarSensor()
        {
            DataTable vResultDataTable = new DataTable();
            vResultDataTable = m_DBClass.Select_ControlBox_DG(true);

            DataTable vNoUsingDataTable = new DataTable();
            vNoUsingDataTable.Columns.Add("ID", typeof(int));
            vNoUsingDataTable.Columns.Add("模块名称", typeof(string));
            vNoUsingDataTable.Columns.Add("模块ID", typeof(int));
            vNoUsingDataTable.Columns.Add("模块地址", typeof(byte));
            vNoUsingDataTable.Columns.Add("说明", typeof(string));
            vNoUsingDataTable.PrimaryKey = new DataColumn[] { vNoUsingDataTable.Columns["ID"] };
            vNoUsingDataTable.AcceptChanges();

            if (vResultDataTable.Rows.Count > 0)
            {
                int vRowID = 0;
                foreach (DataRow vTempRow in vResultDataTable.Rows)
                {
                    DataRow vNewRow = vNoUsingDataTable.NewRow();
                    vNewRow["ID"] 　　　= vRowID;
                    vNewRow["模块名称"] = vTempRow["名称"];
                    vNewRow["模块ID"] 　= vTempRow["ID"];
                    vNewRow["模块地址"] = vTempRow["设备地址"];
                    vNewRow["说明"] 　　= string.Format("{0}模块 地址:{1}", vNewRow["模块名称"], vNewRow["模块地址"]);
                    vNoUsingDataTable.Rows.Add(vNewRow);
                    vRowID++;
                }
            }
            vNoUsingDataTable.AcceptChanges();
            return vNoUsingDataTable;
        }

        public void Run()
        {
            if (m_RunThreadStopFlag)
            {
                m_RunThreadStopFlag = false;
                m_ReceiveDataQueue = new Queue<byte[]>();
                initModBus();
                initControlModules();
                initCommonalityVar();
                //通讯线程
                m_MessageThread = new Thread(new ThreadStart(messageThread));
                m_MessageThread.Priority = ThreadPriority.Highest;
                m_MessageThread.Name = "MessageThread";
                m_MessageThread.Start();
                //数据处理线程
                m_DataTransactThread = new Thread(new ThreadStart(dataTransactThread));
                m_DataTransactThread.Name = "DataTransactThread";
                m_DataTransactThread.Start();
                //状态检查线程
                m_StateCheckupThread = new Thread(new ThreadStart(stateCheckUpThread));
                m_StateCheckupThread.Name = "StateCheckUpThread";
                m_StateCheckupThread.Start();
            }
        }

        public void Stop()
        {
            if (!m_RunThreadStopFlag)
            {
                m_RunThreadStopFlag = true;
                m_ReceiveDataQueue.Clear();

                //通讯线程
                if (m_MessageThread != null)
                {
                    m_MessageThread.Abort();
                    m_MessageThread = null;
                }
                //数据处理线程
                if (m_DataTransactThread != null)
                {
                    m_DataTransactThread.Abort();
                    m_DataTransactThread = null;
                }

                m_Modbus.Dispose();
            }
        }

        void stateCheckUpThread()
        {
            try
            {
                while (!m_RunThreadStopFlag)
                {
                    if (!checkUpCarSensor())
                        m_State = ControlState.Instancy;

                }
            }
            catch (Exception ex)
            {
                if (m_Debug != null)
                    m_Debug.WriteDebugLog(ex.Message);
            }
        }

        /// <summary>
        /// 检查车辆传感器
        /// </summary>
        bool checkUpCarSensor()
        {
            bool vCheckUpResult = false;
            if (m_MainCarSensorData.Address != 0 )
            {
                TimeSpan vMainTime = DateTime.Now-m_MainCarSensorData.ReceiveTime;
                if ( !m_MainCarSensorData.PowerDown && vMainTime.Minutes < m_CommunicationTimeOut)
                    vCheckUpResult = true;
                    
            }
            if (m_SlaveCarSensorData.Address != 0 && !vCheckUpResult)
            {
                TimeSpan vSlaveTime = DateTime.Now - m_SlaveCarSensorData.ReceiveTime;
                if (!m_SlaveCarSensorData.PowerDown && vSlaveTime.Minutes < m_CommunicationTimeOut)
                    vCheckUpResult = true;
            }
            return vCheckUpResult;
        }

        /// <summary>
        /// 检查照度(入口)
        /// </summary>
        /// <returns></returns>
        bool checkUpLighteness_RK()
        {
            bool vCheckUpResult = false;
            if (m_RK_MainOutLighteness.Address != 0)
            {
                TimeSpan vTime = DateTime.Now - m_RK_MainOutLighteness.ReceiveTime;
                if (vTime.Milliseconds < m_CommunicationTimeOut)
                    vCheckUpResult = true;
            }
            if (m_RK_SlaveOutLighteness.Address != 0 && !vCheckUpResult)
            {
                TimeSpan vTime = DateTime.Now - m_RK_SlaveOutLighteness.ReceiveTime;
                if (vTime.Milliseconds < m_CommunicationTimeOut)
                    vCheckUpResult = true;
            }
            return vCheckUpResult;
        }

        //bool checkUpLighteness_CK()
        //{
            
        //}

        int m_LowLevelPoint = 0;
        void messageThread()
        {
            try
            {
                while (!m_RunThreadStopFlag)
                {
                    //高优先级
                    foreach (byte vHighLevelAddress in m_HighLevelAddressList)
                    {
                        ControlModule vTempControlModule = FindControlModule(vHighLevelAddress);
                        if (vTempControlModule != null)
                        {
                            int vPWM = vTempControlModule.GetPWM();
                            byte vPWM_Byte = Convert.ToByte(255 * vPWM / 100);
                            byte[] vDataPack = new byte[] { 0x00, vPWM_Byte, vPWM_Byte };
                            byte[] vReceiveDataPack = m_Modbus.SendDataPack(vTempControlModule.Address, vDataPack);
                            //调试输出
                            if (m_Debug != null)
                            {
                                string vSendInfo = string.Format("模块名:{0} 模块地址:{1} 发送数据包", vTempControlModule.Name, vTempControlModule.Address);
                                m_Debug.WriteDebugLog(vSendInfo);
                                if (vReceiveDataPack != null)
                                {
                                    string vReceiveInfo = string.Format("模块名:{0} 模块地址:{1} 接受数据包→{2}", vTempControlModule.Name, vTempControlModule.Address, vReceiveDataPack.ToArray());
                                    m_Debug.WriteDebugLog(vReceiveInfo);
                                }
                            }
                            if (vReceiveDataPack != null)
                            {
                                m_ReceiveDataQueue.Enqueue(vReceiveDataPack);
                                vTempControlModule.State = ModuleState.Normal;
                            }
                            else
                            {
                                if (m_CommunicationTimeOut != 0)
                                {
                                    DateTime vLastCommunicationTime = vTempControlModule.LastCommunicationTime;
                                    TimeSpan vTimeSpan = DateTime.Now - vLastCommunicationTime;
                                    if (vTimeSpan.Seconds >= m_CommunicationTimeOut)
                                    {
                                        vTempControlModule.State = ModuleState.Timeout;
                                    }
                                }
                            }
                        }
                    }

                    //普通级别
                    DateTime vTakeCountOfTime = DateTime.Now;
                    for (int i = m_LowLevelPoint; i < m_LowLevelAddressList.Length; i++)
                    {
                        ControlModule vTempControlModule = FindControlModule(m_LowLevelAddressList[i]);
                        if (vTempControlModule != null)
                        {
                            int vPWM = vTempControlModule.GetPWM();
                            byte vPWM_Byte = Convert.ToByte(255 * (vPWM / 100f));
                            byte[] vDataPack = new byte[] { 0x00, vPWM_Byte, vPWM_Byte };
                            byte[] vReceiveDataPack = m_Modbus.SendDataPack(vTempControlModule.Address, vDataPack);
                            //调试输出
                            if (m_Debug != null)
                            {
                                string vSendInfo = string.Format("模块名:{0} 模块地址:{1} 发送数据包", vTempControlModule.Name, vTempControlModule.Address);
                                m_Debug.WriteDebugLog(vSendInfo);
                                if (vReceiveDataPack != null)
                                {
                                    string vReceiveInfo = string.Format("模块名:{0} 模块地址:{1} 接受数据包→{2}", vTempControlModule.Name, vTempControlModule.Address,BitConverter.ToString(vReceiveDataPack) );
                                    m_Debug.WriteDebugLog(vReceiveInfo);
                                }
                            }
                            if (vReceiveDataPack != null)
                            {
                                m_ReceiveDataQueue.Enqueue(vReceiveDataPack);
                                vTempControlModule.State = ModuleState.Normal;
                            }
                            else
                            {
                                if (m_CommunicationTimeOut != 0)
                                {
                                    DateTime vLastCommunicationTime = vTempControlModule.LastCommunicationTime;
                                    TimeSpan vTimeSpan = DateTime.Now - vLastCommunicationTime;
                                    if (vTimeSpan.Seconds >= m_CommunicationTimeOut)
                                    {
                                        vTempControlModule.State = ModuleState.Timeout;
                                    }
                                }
                            }
                        }

                        //时间片调度部分
                        TimeSpan vIntervalTime = DateTime.Now - vTakeCountOfTime;

                        if (vIntervalTime.Milliseconds >= TimeValve)
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("普通队列时间片用完,耗时{0}毫秒", vIntervalTime.Milliseconds));
                            if (i == m_LowLevelAddressList.Length - 1)
                                m_LowLevelPoint = 0;
                            else
                                m_LowLevelPoint = i + 1;
                            break;
                        }
                        else
                        {
                            if (m_LowLevelPoint == (m_LowLevelAddressList.Length - 1))
                                m_LowLevelPoint = 0;
                            else
                                m_LowLevelPoint++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if ( m_Debug != null )
                    m_Debug.WriteDebugLog(ex.Message);
            }
        }

        void dataTransactThread()
        {
            try
            {
                while (!m_RunThreadStopFlag)
                {
                    if (m_ReceiveDataQueue != null && m_ReceiveDataQueue.Count != 0)
                    {
                        byte[] vReceiveDataPack = m_ReceiveDataQueue.Dequeue();
                        byte vTargetAddress = vReceiveDataPack[1]; //目标地址
                        byte vSourceAddress = byte.Parse(vReceiveDataPack[3].ToString("X"));  //源地址
                        byte[] vDataArea1 = new byte[] { vReceiveDataPack[6], vReceiveDataPack[5] }; //数据区1
                        byte[] vDataArea2 = new byte[] { vReceiveDataPack[8], vReceiveDataPack[7] }; //数据区2
                        byte vDatArea3 = vReceiveDataPack[9]; //数据区3
                        byte vDatArea4 = vReceiveDataPack[10]; //数据区4
                        byte vDatArea5 = vReceiveDataPack[11]; //数据区5
                        byte vDatArea6 = vReceiveDataPack[12]; //数据区6
                        byte[] vDatArea7 = new byte[] { vReceiveDataPack[14], vReceiveDataPack[13] }; //数据区7
                        byte vDatArea8 = vReceiveDataPack[15]; //数据区8
                        ControlModule vTempControlModule = FindControlModule(vSourceAddress);
                        if (vTempControlModule != null)
                        {
                            //处理数据
                            vTempControlModule.ReceiveData(vDataArea1, vDataArea2, vDatArea3, vDatArea4, vDatArea5, vDatArea6, vDatArea7, vDatArea8);
                            //主洞外照度传感器(入口)
                            if (vSourceAddress == m_RK_MainOutLighteness.Address)
                            {
                                if (vTempControlModule.Port1 != null && vTempControlModule.Port1 is LightenessSensor && m_RK_MainOutLighteness.PortNumber == 1)
                                {
                                    m_RK_MainOutLighteness.Address = vSourceAddress;
                                    m_RK_MainOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port1).Lighteness;
                                    m_RK_MainOutLighteness.ReceiveTime = DateTime.Now;
                                }
                                if (vTempControlModule.Port2 != null && vTempControlModule.Port2 is LightenessSensor && m_RK_MainOutLighteness.PortNumber == 2)
                                {
                                    m_RK_MainOutLighteness.Address = vSourceAddress;
                                    m_RK_MainOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port2).Lighteness;
                                    m_RK_MainOutLighteness.ReceiveTime = DateTime.Now;
                                }
                            }

                            //从洞外照度传感器(入口)
                            if (vSourceAddress == m_RK_SlaveOutLighteness.Address)
                            {
                                if (vTempControlModule.Port1 != null && vTempControlModule.Port1 is LightenessSensor && m_RK_SlaveOutLighteness.PortNumber == 1)
                                {
                                    m_RK_SlaveOutLighteness.Address = vSourceAddress;
                                    m_RK_SlaveOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port1).Lighteness;
                                    m_RK_SlaveOutLighteness.ReceiveTime = DateTime.Now;
                                }
                                if (vTempControlModule.Port2 != null && vTempControlModule.Port2 is LightenessSensor && m_RK_SlaveOutLighteness.PortNumber == 2)
                                {
                                    m_RK_SlaveOutLighteness.Address = vSourceAddress;
                                    m_RK_SlaveOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port2).Lighteness;
                                    m_RK_SlaveOutLighteness.ReceiveTime = DateTime.Now;
                                }
                            }

                            //主洞外照度传感器(出口)
                            if (vSourceAddress == m_CK_MainOutLighteness.Address)
                            {
                                if (vTempControlModule.Port1 != null && vTempControlModule.Port1 is LightenessSensor && m_CK_MainOutLighteness.PortNumber == 1)
                                {
                                    m_CK_MainOutLighteness.Address = vSourceAddress;
                                    m_CK_MainOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port1).Lighteness;
                                    m_CK_MainOutLighteness.ReceiveTime = DateTime.Now;
                                }
                                if (vTempControlModule.Port2 != null && vTempControlModule.Port2 is LightenessSensor && m_RK_MainOutLighteness.PortNumber == 2)
                                {
                                    m_CK_MainOutLighteness.Address = vSourceAddress;
                                    m_CK_MainOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port2).Lighteness;
                                    m_CK_MainOutLighteness.ReceiveTime = DateTime.Now;
                                }
                            }

                            //从洞外照度传感器(出口)
                            if (vSourceAddress == m_CK_SlaveOutLighteness.Address)
                            {
                                if (vTempControlModule.Port1 != null && vTempControlModule.Port1 is LightenessSensor && m_CK_SlaveOutLighteness.PortNumber == 1)
                                {
                                    m_CK_SlaveOutLighteness.Address = vSourceAddress;
                                    m_CK_SlaveOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port1).Lighteness;
                                    m_CK_SlaveOutLighteness.ReceiveTime = DateTime.Now;
                                }
                                if (vTempControlModule.Port2 != null && vTempControlModule.Port2 is LightenessSensor && m_RK_SlaveOutLighteness.PortNumber == 2)
                                {
                                    m_CK_SlaveOutLighteness.Address = vSourceAddress;
                                    m_CK_SlaveOutLighteness.LightenessValue = ((LightenessSensor)vTempControlModule.Port2).Lighteness;
                                    m_CK_SlaveOutLighteness.ReceiveTime = DateTime.Now;
                                }
                            }

                            //主地感线圈
                            if (vSourceAddress == m_MainCarSensorData.Address)
                            {
                                if (vTempControlModule.Car != null && vSourceAddress == vTempControlModule.Address)
                                {
                                    if (vTempControlModule.Car.CarNumber > m_MainCarSensorData.CarNumber)
                                        m_LastCarTime = DateTime.Now;
                                    m_MainCarSensorData.Address = vSourceAddress;
                                    m_MainCarSensorData.CarNumber = vTempControlModule.Car.CarNumber;
                                    m_MainCarSensorData.ReceiveTime = DateTime.Now;
                                }
                            }

                            //从地感线圈
                            if (vSourceAddress == m_SlaveCarSensorData.Address)
                            {
                                if (vTempControlModule.Car != null && vSourceAddress == vTempControlModule.Address)
                                {
                                    if (vTempControlModule.Car.CarNumber > m_SlaveCarSensorData.CarNumber)
                                        m_LastCarTime = DateTime.Now;
                                    m_SlaveCarSensorData.Address = vSourceAddress;
                                    m_SlaveCarSensorData.CarNumber = vTempControlModule.Car.CarNumber;
                                    m_SlaveCarSensorData.ReceiveTime = DateTime.Now;
                                }
                            }

                            //计算PWM
                            TimeSpan vIntervalTime = DateTime.Now - m_LastCarTime;
                            //灯光延时
                            if (m_LigthTimeDelay != 0 && vIntervalTime.Minutes < m_LigthTimeDelay)
                            {
                                if (vTempControlModule.PWMMode == PWMModeEnum.DynamicPWM)
                                {
                                    int vOutLighteness = 0;
                                    if (m_RK_MainOutLighteness.Address != 0)
                                        vOutLighteness = m_RK_MainOutLighteness.LightenessValue;
                                    else if (m_RK_SlaveOutLighteness.Address != 0)
                                        vOutLighteness = m_RK_SlaveOutLighteness.LightenessValue;

                                    bool vCar = true;
                                    if (m_MainCarSensorData.Address != 0)
                                        vCar = m_MainCarSensorData.CarNumber != 0 ? true : false;
                                    else if (m_SlaveCarSensorData.Address != 0)
                                        vCar = m_SlaveCarSensorData.CarNumber != 0 ? true : false;

                                    vTempControlModule.CalcPWM(vOutLighteness, vCar);
                                }
                            }
                            else
                                vTempControlModule.PWMSleep();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (m_Debug != null)
                    m_Debug.WriteDebugLog(ex.Message);
            }
        }

        /// <summary>
        /// 添加新控制模块
        /// </summary>
        /// <returns></returns>
        public ControlModule NewControlModule()
        {
            int vControlModuleID = 0;
            byte vNewAddress = getNewAddress();
            ControlBoxTableStruct vControlBoxRecord = new ControlBoxTableStruct();
            vControlBoxRecord.SBDZ = vNewAddress;
            vControlBoxRecord.MC   = "新模块";
            m_DBClass.InsertRecord(vControlBoxRecord, ref vControlModuleID);
            ControlModule vNewControlModule = new ControlModule(vControlModuleID,m_LightControlPWM);
            return vNewControlModule;
        }

        byte getNewAddress()
        {
            for (byte i = 1; i < 255; i++)
            {
                if (FindControlModule(i) == null)
                {
                    return i;
                }
            }
            return 0;
        }

        public ControlModule FindControlModule(int ControlModuleID)
        {
            foreach (ControlModule vTempControlModule in m_ControlModules)
            {
                if (vTempControlModule.ID == ControlModuleID)
                {
                    return vTempControlModule;
                }
            }
            return null;
        }

        public ControlModule FindControlModule(byte ControlModuleAddress)
        {
            foreach (ControlModule vTempControlModule in m_ControlModules)
            {
                if (vTempControlModule.Address == ControlModuleAddress)
                {
                    return vTempControlModule;
                }
            }
            return null;
        }

        public bool RemoveControlModule(int ControlModuleID)
        {
            bool vResult = m_DBClass.DeleteRecord<ControlBoxTableStruct>(ControlModuleID);
            if (vResult)
            {
                if (m_ControlModules != null)
                {
                    for (int i = 0; i < m_ControlModules.Count; i++)
                    {
                        if (m_ControlModules[i].ID == ControlModuleID)
                            m_ControlModules.Remove(m_ControlModules[i]);
                    }
                }
            }
            return vResult;
        }
        #endregion

        #region 属性
        public List<ControlModule> ControlModules
        {
            get
            {
                return m_ControlModules;
            }
        }

        public bool IsRun
        {
            get
            {
                return !m_RunThreadStopFlag; 
            }
        }
        #endregion

        #region 私有方法

        //应急照明
        void meetanemergency()
        {
            foreach (ControlModule vTempControlModule in m_ControlModules)
            {
                vTempControlModule.PWMMax();
            }
        }
        

        void initModBus()
        {
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                int vBaudRate = vSystemConfig.BaudRate;
                string vPortName = vSystemConfig.PortName;
                m_Modbus = new Modbus(vPortName, vBaudRate);
                m_Modbus.OnModbusState_RXEvent += new Modbus.ModbusState_RXEventHandler(m_Modbus_OnModbusState_RXEvent);
                m_Modbus.OnModbusState_TXEvent += new Modbus.ModbusState_TXEventHandler(m_Modbus_OnModbusState_TXEvent);
            }
        }

        void m_Modbus_OnModbusState_RXEvent(object sender, Modbus.ModbusState_RXEventArgs e)
        {
            if ( OnModbusActivityStateEventArgsEvent != null )
                OnModbusActivityStateEventArgsEvent(this, new ModbusActivityStateEventArgs(ModeActivityEnum.RX));
        }

        void m_Modbus_OnModbusState_TXEvent(object sender, Modbus.ModbusState_TXEventArgs e)
        {
            if (OnModbusActivityStateEventArgsEvent != null)
                OnModbusActivityStateEventArgsEvent(this, new ModbusActivityStateEventArgs(ModeActivityEnum.TX));
        }
        
        void initControlModules()
        {
            if (m_ControlModules != null)
                m_ControlModules.Clear();
            m_ControlModules = new List<ControlModule>();
            DataTable vControlBoxDataTable =  m_DBClass.SelectAllRecords<ControlBoxTableStruct>();
            if (vControlBoxDataTable.Rows.Count > 0)
            {
                List<byte> vHighLevelList = new List<byte>();
                List<byte> vLowLevelList = new List<byte>();

                ControlBoxTableStruct vControlBoxRecord;
                foreach( DataRow vTempRow in vControlBoxDataTable.Rows )
                {
                    vControlBoxRecord = new ControlBoxTableStruct();
                    CommClass.ConvertDataRowToStruct(ref vControlBoxRecord, vTempRow);
                    ControlModule vControlModule = new ControlModule(vControlBoxRecord, m_LightControlPWM);

                    if (vControlModule.Port1 != null)
                    {
                        if (vControlModule.Port1.SensorType == SensorTypeEnum.LightenessSensor)
                            ((LightenessSensor)vControlModule.Port1).OnLightenessCDChangeDownEvent += new LightenessSensor.LightenessSensorCDChangeEventHandler(ControlModuleManage_OnPort1LightenessCDChangeDownEvent);
                        if (vControlModule.Port1.SensorType == SensorTypeEnum.COSensor)
                            ((COSensor)vControlModule.Port1).OnCOSensorStrengthChangeEvent += new COSensor.COSensorStrengthChangeEventHandler(ControlModuleManage_OnPort1COSensorStrengthChangeEvent);
                    }

                    if (vControlModule.Port2 != null)
                    {
                        if ( vControlModule.Port2.SensorType == SensorTypeEnum.LightenessSensor )
                            ((LightenessSensor)vControlModule.Port2).OnLightenessCDChangeDownEvent += new LightenessSensor.LightenessSensorCDChangeEventHandler(ControlModuleManage_OnPort2LightenessCDChangeDownEvent);
                        if (vControlModule.Port2.SensorType == SensorTypeEnum.COSensor)
                            ((COSensor)vControlModule.Port2).OnCOSensorStrengthChangeEvent += new COSensor.COSensorStrengthChangeEventHandler(ControlModuleManage_OnPort2COSensorStrengthChangeEvent);
                    }

                    if ( vControlModule.Power!= null )
                        vControlModule.Power.OnPowerSensorPowerDownEvent += new PowerSensor.PowerSensorPowerDownEventHandler(Power_OnPowerSensorPowerDownEvent);
                    if (vControlModule.Car != null)
                    {
                        vControlModule.Car.OnCarSensingCarNumberEvent += new CarSensor.CarSensingCarNumberEventHandler(Car_OnCarSensingCarNumberEvent);
                        vControlModule.Car.OnCarSensingCoilPowerDownEvent += new CarSensor.CarSensingCoilPowerDownEventHandler(Car_OnCarSensingCoilPowerDownEvent);
                        vControlModule.Car.OnCarSensorPowerDownEvent += new CarSensor.CarSensorPowerDownEventHandler(Car_OnCarSensorPowerDownEvent);
                    }
                    if (vControlModule.EmergencyPower != null)
                    {
                        vControlModule.EmergencyPower.OnEmergencyPowerSensorVoltageEvent += new EmergencyPowerSensor.EmergencyPowerSensorVoltageEventHandler(EmergencyPower_OnEmergencyPowerSensorVoltageEvent);
                    }

                    vControlModule.OnPWMChangedEvent += new ControlModule.PWMChangedEventHandler(vControlModule_OnPWMChangedEvent);

                    m_ControlModules.Add(vControlModule);
                    if (vControlModule.HighLevel)
                        vHighLevelList.Add(vControlModule.Address);
                    else
                        vLowLevelList.Add(vControlModule.Address);
                }
                m_HighLevelAddressList = vHighLevelList.ToArray();
                m_LowLevelAddressList = vLowLevelList.ToArray();
            }
        }

        void initCommonalityVar()
        {
            using (SystemConfig vSystemConfig = new SystemConfig())
            {
                m_LastCarTime = DateTime.Now;
                m_RK_MainOutLighteness = new OutLightenessStruct();  //主洞外照度
                m_LigthTimeDelay = vSystemConfig.LigthTimeDelay;  //延时关灯
                m_CommunicationTimeOut = vSystemConfig.CommunicationTimeOut; //通讯超时

                //主洞外照度传感器(入口)
                string vMainOutLightenessName_RK = "";
                int vMainOutLightenessID_RK = 0;
                vSystemConfig.GetMainLighteness_RK(ref vMainOutLightenessName_RK, ref m_RK_MainOutLighteness.Address,
                    ref vMainOutLightenessID_RK, ref m_RK_MainOutLighteness.PortNumber);
                //从洞外照度传感器(出口)
                string vSlaveOutLightenessName_RK = "";
                int vSlaveOutLightenessID_RK = 0;
                vSystemConfig.GetSlaveLighteness_RK(ref vSlaveOutLightenessName_RK, ref m_RK_SlaveOutLighteness.Address,
                    ref vSlaveOutLightenessID_RK, ref m_RK_SlaveOutLighteness.PortNumber);

                //主洞外照度传感器(出口)
                string vMainOutLightenessName_CK = "";
                int vMainOutLightenessID_CK = 0;
                vSystemConfig.GetMainLighteness_CK(ref vMainOutLightenessName_CK, ref m_CK_MainOutLighteness.Address,
                    ref vMainOutLightenessID_CK, ref m_CK_MainOutLighteness.PortNumber);
                //从洞外照度传感器(出口)
                string vSlaveOutLightenessName_CK = "";
                int vSlaveOutLightenessID_CK = 0;
                vSystemConfig.GetSlaveLighteness_CK(ref vSlaveOutLightenessName_CK, ref m_CK_SlaveOutLighteness.Address,
                    ref vSlaveOutLightenessID_CK, ref m_CK_SlaveOutLighteness.PortNumber);

                //主地感线圈
                string vMainCarSensorName = "";
                int vMainCarSensorID = 0;
                vSystemConfig.GetMainCarSensor(ref vMainCarSensorName, ref m_MainCarSensorData.Address, ref vMainCarSensorID);
                //从地感线圈
                string vSlaveCarSensorName = "";
                int vSlaveCarSensorID = 0;
                vSystemConfig.GetSlaveCarSensor(ref vSlaveCarSensorName, ref m_SlaveCarSensorData.Address, ref vSlaveCarSensorID);
                //调试模式
                m_DebugMode = vSystemConfig.DebugMode;
            }
        }
        #endregion

        #region 响应事件
        void EmergencyPower_OnEmergencyPowerSensorVoltageEvent(object sender, EmergencyPowerSensor.EmergencyPowerSensorVoltageEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("备用电源电压改变为{0}", e.Voltage));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.EmergencyPowerVoltage = e.Voltage;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((EmergencyPowerSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((EmergencyPowerSensor)sender).ControlModuleID).Name;
                OnOutputMessageEvent(sender, new OutputMessageEventArgs( InfoLevel.Message, string.Format("{0}:备用电源电压改变为{1}",vModuleName, e.Voltage)));
            }
        }


        /// <summary>
        /// 车辆传感器电源掉电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Car_OnCarSensorPowerDownEvent(object sender, CarSensor.CarSensorPowerDownEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("车辆传感器电源状态改变为{0}", e.PowerDown));
                int vModuleAddress = FindControlModule( ( (CarSensor)sender ).ControlModuleID ).Address;
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.CarSensorPowerDown = e.PowerDown;
                if (m_MainCarSensorData.Address == vModuleAddress)
                    m_MainCarSensorData.PowerDown = e.PowerDown;
                else
                    m_MainCarSensorData.PowerDown = e.PowerDown;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((CarSensor)sender).ControlModuleID, vReceiveData));

               
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((CarSensor)sender).ControlModuleID).Name;
                if ( e.PowerDown )
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:车辆传感器电源恢复正常", vModuleName )));
                else
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Error, string.Format("{0}:车辆传感器电源掉电",vModuleName )));
            }
        }

        /// <summary>
        /// 车辆传感器地感线圈掉电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Car_OnCarSensingCoilPowerDownEvent(object sender, CarSensor.CarSensingCoilPowerDownEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("车辆传感器电圈电源状态改变为{0}", e.SensingCoilPowerDown));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.CarCoilPowerDown = e.SensingCoilPowerDown;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((CarSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((CarSensor)sender).ControlModuleID).Name;
                if (e.SensingCoilPowerDown)
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Error, string.Format("{0}:车辆传感器电圈电源掉电", vModuleName)));
                else
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:车辆传感器电圈电源恢复正常", vModuleName)));
            }
        }

        void Car_OnCarSensingCarNumberEvent(object sender, CarSensor.CarSensingCarNumberEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("车辆传感器车辆数为{0}", e.CarNumber));
                int vModuleAddress = FindControlModule(((CarSensor)sender).ControlModuleID).Address;
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.CarNumber = e.CarNumber;
                m_LastCarTime = DateTime.Now;
                
                if (m_MainCarSensorData.Address == vModuleAddress)
                {
                    m_MainCarSensorData.CarNumber = e.CarNumber;
                    m_MainCarSensorData.PowerDown = false;
                    m_MainCarSensorData.ReceiveTime = DateTime.Now;
                }

                if (m_SlaveCarSensorData.Address == vModuleAddress)
                {
                    m_SlaveCarSensorData.CarNumber = e.CarNumber;
                    m_SlaveCarSensorData.PowerDown = false;
                    m_SlaveCarSensorData.ReceiveTime = DateTime.Now;
                }

                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((CarSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((CarSensor)sender).ControlModuleID).Name;
                OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:车辆传感器车辆数为{0}",vModuleName, e.CarNumber)));
            }
        }

        void ControlModuleManage_OnPort1COSensorStrengthChangeEvent(object sender, COSensor.COSensorStrengthChangeEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("ＣＯ浓度为{0}", e.Strength));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.Port1 = e.Strength;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((COSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((COSensor)sender).ControlModuleID).Name;
                OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0},ＣＯ浓度为{0}",vModuleName, e.Strength)));
            }
        }

        void ControlModuleManage_OnPort2COSensorStrengthChangeEvent(object sender, COSensor.COSensorStrengthChangeEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("ＣＯ浓度为{0}", e.Strength));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.Port2 = e.Strength;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((COSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((COSensor)sender).ControlModuleID).Name;
                OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:ＣＯ浓度为{1}", vModuleName,e.Strength)));
            }
        }

        void Power_OnPowerSensorPowerDownEvent(object sender, PowerSensor.PowerSensorPowerDownEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("主电源传感器状态改变为{0}", e.PowerDown));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.PowerDown = e.PowerDown;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((PowerSensor)sender).ControlModuleID, vReceiveData));
            }

            if (OnOutputMessageEvent != null)
            {
                string vModuleName = FindControlModule(((PowerSensor)sender).ControlModuleID).Name;
                if ( e.PowerDown )
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Error, string.Format("{0}:主电源掉电",vModuleName) ));
                else
                    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:主电源恢复正常",vModuleName)));
            }
        }

        void ControlModuleManage_OnPort1LightenessCDChangeDownEvent(object sender, LightenessSensor.LightenessSensorCDChangeEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("照度传感器照度值改变为{0}", e.CDValue));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.Port1 = e.CDValue;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((LightenessSensor)sender).ControlModuleID, vReceiveData));
            }
            
            //if (OnOutputMessageEvent != null)
            //{
            //    string vModuleName = FindControlModule(((LightenessSensor)sender).ControlModuleID).Name;
            //    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:照度传感器照度值改变为{1}",vModuleName,  e.CDValue)));
            //}
        }

        void ControlModuleManage_OnPort2LightenessCDChangeDownEvent(object sender, LightenessSensor.LightenessSensorCDChangeEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("照度传感器照度值改变为{0}", e.CDValue));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.Port2 = e.CDValue;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((LightenessSensor)sender).ControlModuleID, vReceiveData));
            }

            //if (OnOutputMessageEvent != null)
            //{
            //    string vModuleName = FindControlModule(((LightenessSensor)sender).ControlModuleID).Name;
            //    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:照度传感器照度值改变为{1}",vModuleName, e.CDValue)));
            //}
        }

        void vControlModule_OnPWMChangedEvent(object sender, ControlModule.PWMChangedEventArgs e)
        {
            if (OnReceiveDataEventArgsEvent != null)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("PWM值改变为{0}", e.PWM));
                ReceiveDataStruct vReceiveData = new ReceiveDataStruct();
                vReceiveData.PWM = e.PWM;
                OnReceiveDataEventArgsEvent(sender, new ReceiveDataEventArgs(((ControlModule)sender).ID, vReceiveData));
            }

            //if (OnOutputMessageEvent != null)
            //{
            //    string vModuleName = ((ControlModule)sender).Name;
            //    OnOutputMessageEvent(sender, new OutputMessageEventArgs(InfoLevel.Message, string.Format("{0}:PWM值改变为{1}", vModuleName, e.PWM)));
            //}
        }
        #endregion

        #region 析构
        public void Dispose()
        {
            m_RunThreadStopFlag = true;
            if (m_DBClass != null)
            {
                m_DBClass.Dispose();
                m_DBClass = null;
            }
            
            if (m_Modbus != null)
            {
                m_Modbus.Dispose();
                m_Modbus = null;
            }

            if (m_MessageThread != null)
            {
                m_MessageThread.Abort();
                m_MessageThread = null;
            }

            if (m_DataTransactThread != null)
            {
                m_DataTransactThread.Abort();
                m_DataTransactThread = null;
            }

            if (m_StateCheckupThread != null)
            {
                m_StateCheckupThread.Abort();
                m_StateCheckupThread = null;
            }

            if (m_Debug != null)
            {
                m_Debug.Dispose();
                m_Debug = null;
            }
        }
        #endregion
    }
}
