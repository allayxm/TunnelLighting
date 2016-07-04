using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.ControlCell
{
    public class SystemConfig:IDisposable
    {
        #region 私有变量
        DBClass m_DBClass;
        const string TunnelName_Item  = "隧道名";
        const string PortName_Item    = "通讯端口号";
        const string BaudRate_Item    = "通讯波特率";
        const string Lighteness_RK_Main_Item  = "主洞外照度(入口)";
        const string Lighteness_RK_Slave_Item = "从洞外照度(入口)";
        const string Lighteness_CK_Main_Item  = "主洞外照度(出口)";
        const string Lighteness_CK_Slave_Item = "从洞外照度(出口)";
        const string Car_Main_Item  = "主地感线圈";
        const string Car_Slave_Item = "从地感线圈";
        const string LigthTimeDelay_Item = "灯光延时";
        const string CommunicationTimeOut_Item = "模块通讯超时";
        const string LightFixTime_Item = "照度调校时间";
        const string LightAllowError_Item = "主备照度允许误差";
        const string DebugMode_Item = "调试模式";
        #endregion

        #region 构造
        public SystemConfig()
        {
            m_DBClass = new DBClass();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 通讯超时
        /// </summary>
        public int CommunicationTimeOut
        {
            get
            {
                int vCommunicationTimeOut = 0;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = CommunicationTimeOut_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vCommunicationTimeOut = DBConvert.ToInt32(vSystemInfoDataTable.Rows[0]["项目值I"]);
                }
                return vCommunicationTimeOut;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZI(CommunicationTimeOut_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = CommunicationTimeOut_Item;
                    vSystemInfoStruct.XMZ_I = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get
            {
                int vBaudRate = 9600;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = BaudRate_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vBaudRate = DBConvert.ToInt32(vSystemInfoDataTable.Rows[0]["项目值I"]);
                }
                return vBaudRate;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZI(PortName_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = PortName_Item;
                    vSystemInfoStruct.XMZ_I = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 端口名
        /// </summary>
        public string PortName
        {
            get
            {
                string vPortName = "COM1";
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = PortName_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vPortName = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                }
                return vPortName;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZS(PortName_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = PortName_Item;
                    vSystemInfoStruct.XMZ_S = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 隧道名
        /// </summary>
        public string TunnelName
        {
            get
            {
                string vTunnelName = "";
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = TunnelName_Item;
                DataTable vSystemInfoDataTable =  m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vTunnelName = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                }
                else
                {
                    vTunnelName = "新隧道";
                    vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = TunnelName_Item;
                    vSystemInfoStruct.XMZ_S = vTunnelName;
                    m_DBClass.InsertRecord(vSystemInfoStruct);

                }
                return vTunnelName;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZS(TunnelName_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = TunnelName_Item;
                    vSystemInfoStruct.XMZ_S = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 灯光延时
        /// </summary>
        public int LigthTimeDelay
        {
            get
            {
                int vLigthTimeDelay = 0;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = LigthTimeDelay_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vLigthTimeDelay = DBConvert.ToInt32(vSystemInfoDataTable.Rows[0]["项目值I"]);
                }
                else
                {
                    vLigthTimeDelay = 0;
                    vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LigthTimeDelay_Item;
                    vSystemInfoStruct.XMZ_I = vLigthTimeDelay;
                    m_DBClass.InsertRecord(vSystemInfoStruct);

                }
                return vLigthTimeDelay;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZI(LigthTimeDelay_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LigthTimeDelay_Item;
                    vSystemInfoStruct.XMZ_I = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 灯光调校时间
        /// </summary>
        public int LightFixTime
        {
            get
            {
                int vLightFixTime = 0;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = LightFixTime_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vLightFixTime = DBConvert.ToInt32(vSystemInfoDataTable.Rows[0]["项目值I"]);
                }
                else
                {
                    vLightFixTime = 0;
                    vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LightFixTime_Item;
                    vSystemInfoStruct.XMZ_I = vLightFixTime;
                    m_DBClass.InsertRecord(vSystemInfoStruct);

                }
                return vLightFixTime;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZI(LightFixTime_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LightFixTime_Item;
                    vSystemInfoStruct.XMZ_I = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 主备照度允许误差
        /// </summary>
        public int LightAllowError
        {
            get
            {
                int vLightAllowError = 0;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = LightAllowError_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vLightAllowError = DBConvert.ToInt32(vSystemInfoDataTable.Rows[0]["项目值I"]);
                }
                else
                {
                    vLightAllowError = 0;
                    vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LightAllowError_Item;
                    vSystemInfoStruct.XMZ_I = vLightAllowError;
                    m_DBClass.InsertRecord(vSystemInfoStruct);

                }
                return vLightAllowError;
            }
            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZI(LightAllowError_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = LightAllowError_Item;
                    vSystemInfoStruct.XMZ_I = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        /// <summary>
        /// 调试模式
        /// </summary>
        public bool DebugMode
        {
            get
            {
                bool vDebugMode = false;
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = DebugMode_Item;
                DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
                if (vSystemInfoDataTable.Rows.Count > 0)
                {
                    vDebugMode = DBConvert.ToBoolean(vSystemInfoDataTable.Rows[0]["项目值B"]);
                }
                else
                {
                    vDebugMode = false;
                    vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = DebugMode_Item;
                    vSystemInfoStruct.XMZ_B = vDebugMode;
                    m_DBClass.InsertRecord(vSystemInfoStruct);

                }
                vSystemInfoDataTable.Clear();
                vSystemInfoDataTable.Dispose();
                vSystemInfoDataTable = null;
                return vDebugMode;
            }

            set
            {
                if (!m_DBClass.Update_SystemInfo_XMZB(DebugMode_Item, value))
                {
                    SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                    vSystemInfoStruct.XMM = DebugMode_Item;
                    vSystemInfoStruct.XMZ_B = value;
                    m_DBClass.InsertRecord(vSystemInfoStruct);
                }
            }
        }

        #region 洞外照明(入口)
        public void SetMainLighteness_RK( int ControlID,int PortNumber )
        {
            string vMainLighteness = "";
            vMainLighteness = string.Format("{0}|{1}", ControlID, PortNumber);
            if (!m_DBClass.Update_SystemInfo_XMZS(Lighteness_RK_Main_Item, vMainLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Lighteness_RK_Main_Item;
                vSystemInfoStruct.XMZ_S = vMainLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetMainLighteness_RK(ref string Name, ref byte Address, ref int ControlID, ref int PortNumber)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Lighteness_RK_Main_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    if (vValueList.Length == 2)
                    {
                        ControlID = int.Parse(vValueList[0]);
                        PortNumber = int.Parse(vValueList[1]);
                    }
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;
            
        }

        public void SetSlaveLighteness_RK(int ControlID, int PortNumber)
        {
            string vSlaveLighteness = "";
            vSlaveLighteness = string.Format("{0}|{1}", ControlID, PortNumber);
            if (!m_DBClass.Update_SystemInfo_XMZS(Lighteness_RK_Slave_Item, vSlaveLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Lighteness_RK_Slave_Item;
                vSystemInfoStruct.XMZ_S = vSlaveLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetSlaveLighteness_RK(ref string Name, ref byte Address, ref int ControlID, ref int PortNumber)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Lighteness_RK_Slave_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    if (vValueList.Length == 2)
                    {
                        ControlID  = int.Parse(vValueList[0]);
                        PortNumber = int.Parse(vValueList[1]);
                    }
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;

        }
        #endregion

        #region 洞外照明(出口)
        public void SetMainLighteness_CK(int ControlID, int PortNumber)
        {
            string vMainLighteness = "";
            vMainLighteness = string.Format("{0}|{1}", ControlID, PortNumber);
            if (!m_DBClass.Update_SystemInfo_XMZS(Lighteness_CK_Main_Item, vMainLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Lighteness_CK_Main_Item;
                vSystemInfoStruct.XMZ_S = vMainLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetMainLighteness_CK(ref string Name, ref byte Address, ref int ControlID, ref int PortNumber)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Lighteness_CK_Main_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    if (vValueList.Length == 2)
                    {
                        ControlID = int.Parse(vValueList[0]);
                        PortNumber = int.Parse(vValueList[1]);
                    }
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;

        }

        public void SetSlaveLighteness_CK(int ControlID, int PortNumber)
        {
            string vSlaveLighteness = "";
            vSlaveLighteness = string.Format("{0}|{1}", ControlID, PortNumber);
            if (!m_DBClass.Update_SystemInfo_XMZS(Lighteness_CK_Slave_Item, vSlaveLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Lighteness_CK_Slave_Item;
                vSystemInfoStruct.XMZ_S = vSlaveLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetSlaveLighteness_CK(ref string Name, ref byte Address, ref int ControlID, ref int PortNumber)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Lighteness_CK_Slave_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    if (vValueList.Length == 2)
                    {
                        ControlID = int.Parse(vValueList[0]);
                        PortNumber = int.Parse(vValueList[1]);
                    }
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;

        }
        #endregion

        #region 地感线圈
        public void SetMainCarSensor( int ControlID )
        {
            string vMainCarLighteness = "";
            vMainCarLighteness = string.Format("{0}", ControlID);
            if (!m_DBClass.Update_SystemInfo_XMZS(Car_Main_Item, vMainCarLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Car_Main_Item;
                vSystemInfoStruct.XMZ_S = vMainCarLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetMainCarSensor(ref string Name, ref byte Address, ref int ControlID)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Car_Main_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    ControlID = int.Parse(vValueList[0]);
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;
        }

        public void SetSlaveCarSensor(int ControlID )
        {
            string vSlaveCarLighteness = "";
            vSlaveCarLighteness = string.Format("{0}", ControlID );
            if (!m_DBClass.Update_SystemInfo_XMZS(Car_Main_Item, vSlaveCarLighteness))
            {
                SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
                vSystemInfoStruct.XMM = Car_Slave_Item;
                vSystemInfoStruct.XMZ_S = vSlaveCarLighteness;
                m_DBClass.InsertRecord(vSystemInfoStruct);
            }
        }

        public void GetSlaveCarSensor(ref string Name, ref byte Address, ref int ControlID)
        {
            SystemInfoStruct vSystemInfoStruct = new SystemInfoStruct();
            vSystemInfoStruct.XMM = Car_Slave_Item;
            DataTable vSystemInfoDataTable = m_DBClass.SelectRecords(vSystemInfoStruct);
            if (vSystemInfoDataTable.Rows.Count > 0)
            {
                string vValues = DBConvert.ToString(vSystemInfoDataTable.Rows[0]["项目值S"]);
                if (vValues != null)
                {
                    string[] vValueList = vValues.Split('|');
                    ControlID = int.Parse(vValueList[0]);
                }

                DataTable vControlBoxDataTable = m_DBClass.SelectRecordByID<ControlBoxTableStruct>(ControlID);
                if (vControlBoxDataTable.Rows.Count > 0)
                {
                    Name = DBConvert.ToString(vControlBoxDataTable.Rows[0]["名称"]);
                    Address = DBConvert.ToByte(vControlBoxDataTable.Rows[0]["设备地址"]);
                }
                vControlBoxDataTable.Dispose();
                vControlBoxDataTable.Clear();
                vControlBoxDataTable = null;
            }
            vSystemInfoDataTable.Dispose();
            vSystemInfoDataTable.Clear();
            vSystemInfoDataTable = null;
        }
        #endregion
        #endregion

        #region 析构
        public void Dispose()
        {
            if (m_DBClass != null)
            {
                m_DBClass.Dispose();
                m_DBClass = null;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
