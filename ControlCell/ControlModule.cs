using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using NCLT.TunnelLighting.DB;
using NCLT.TunnelLighting.Sensor;
using NCLT.TunnelLighting.LightControl;

namespace NCLT.TunnelLighting.ControlCell
{
    public class ControlModule:IDisposable
    {
        #region PWM值输出
        /// <summary>
        /// 浓度改变
        /// </summary>
        public class PWMChangedEventArgs : EventArgs
        {
            public readonly int PWM;
            public PWMChangedEventArgs(int vPWM)
            {
                PWM = vPWM;
            }
        }
        public delegate void PWMChangedEventHandler(object sender, PWMChangedEventArgs e);
        public event PWMChangedEventHandler OnPWMChangedEvent;
        #endregion

        #region 私有变量
        int m_ControlModuleID;
        byte m_ControlAddress = 0;
        string m_Name = "";
        bool m_HighLevel = false;
        LightControlPWM m_LightControlPWM = null;

        #endregion
        
        #region 属性
        public int ID
        {
            get
            {
                return m_ControlModuleID;
            }
        }
        
        public byte Address
        {
            get
            {
                return m_ControlAddress;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        /// <summary>
        /// 端口1
        /// </summary>
        SensorObject m_Port1 = null;
        public SensorObject Port1
        {
            get
            {
                return m_Port1;
            }
        }

        public bool HighLevel
        {
            get
            {
                return m_HighLevel;
            }
        }

        /// <summary>
        /// CO传感器
        /// </summary>
        SensorObject m_Port2 = null;
        public SensorObject Port2
        {
            get
            {
                return m_Port2;
            }
        }

        /// <summary>
        /// 车辆传感器(地感线圈)
        /// </summary>
        CarSensor m_CarSensor = null;
        public CarSensor Car
        {
            get
            {
                return m_CarSensor;
            }
        }

        /// <summary>
        /// 备用电源传感器
        /// </summary>
        EmergencyPowerSensor m_EmergencyPowerSensor = null;
        public EmergencyPowerSensor EmergencyPower
        {
            get
            {
                return m_EmergencyPowerSensor;
            }
        }

        /// <summary>
        /// 220V电源传感器
        /// </summary>
        PowerSensor m_PowerSensor = null;
        public PowerSensor Power
        {
            get
            {
                return m_PowerSensor;
            }
        }

        /// <summary>
        /// PWM输出1
        /// </summary>
        PWMModeEnum m_PWMMode;
        public PWMModeEnum PWMMode
        {
            get
            {
                return m_PWMMode;
            }
        }

        /// <summary>
        /// PWM输出
        /// </summary>
        int m_PWMFixupValue;
        public int PWMFixupValue
        {
            get
            {
                return m_PWMFixupValue;
            }
        }

        int m_PWMDynamicValue;

        int m_PWMDemarcate;
        /// <summary>
        /// PWM标定值
        /// </summary>
        public int PWMDemarcate
        {
            get
            {
                return m_PWMDemarcate;
            }
            set
            {
                m_PWMDemarcate = value;
            }
        }

        public UInt16 SensorNumber
        {
            get
            {
                UInt16 vSensorNumber = 0;
                if (m_Port1 != null)
                    vSensorNumber++;
                if (m_Port2 != null)
                    vSensorNumber++;
                if ( m_CarSensor != null )
                    vSensorNumber++;
                if ( m_EmergencyPowerSensor!=null )
                    vSensorNumber++;
                if ( m_PowerSensor != null )
                    vSensorNumber++;
                return vSensorNumber;
            }
        }

        Int16 m_Area;
        public Int16 Area
        {
            get
            {
                return m_Area;
            }
        }

        //最后通讯时间
        DateTime m_LastCommunicationTime;
        public DateTime LastCommunicationTime
        {
            get
            {
                return m_LastCommunicationTime;
            }
        }

        ModuleState m_State;
        public ModuleState State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }
        #endregion

        #region 构造
        public ControlModule(int ControlModuleID,LightControlPWM vLightControlPWM)
        {
            m_ControlModuleID = ControlModuleID;
            m_LightControlPWM = vLightControlPWM;
            DataTable vControlBoxDataTable = new DataTable();
            using (DBClass vDBClass = new DBClass())
            {
                vControlBoxDataTable = vDBClass.SelectRecordByID<ControlBoxTableStruct>(m_ControlModuleID);
            }
            if (vControlBoxDataTable.Rows.Count > 0)
            {
                ControlBoxTableStruct vControlBoxRecord = new ControlBoxTableStruct();
                CommClass.ConvertDataRowToStruct(ref vControlBoxRecord, vControlBoxDataTable.Rows[0]);
                init(vControlBoxRecord);
            }
            vControlBoxDataTable.Clear();
            vControlBoxDataTable.Dispose();
            vControlBoxDataTable = null;

            m_LastCommunicationTime = DateTime.Now;
            m_State = ModuleState.Timeout;
        }

        public ControlModule(ControlBoxTableStruct vControlBoxRecord, LightControlPWM vLightControlPWM)
        {
            m_ControlModuleID     = vControlBoxRecord.ID.Value;
            m_LightControlPWM     = vLightControlPWM;
            init(vControlBoxRecord);
            m_LastCommunicationTime = DateTime.Now;
            m_State = ModuleState.Timeout;
        }

        void init(ControlBoxTableStruct vControlBoxRecord)
        {
            m_ControlAddress = vControlBoxRecord.SBDZ.IsNull ? (byte)0 : vControlBoxRecord.SBDZ.Value;

            m_Name = vControlBoxRecord.MC.IsNull ? "" : vControlBoxRecord.MC.Value;
            m_HighLevel = vControlBoxRecord.GYXJ.Value;
            //端口1
            if (vControlBoxRecord.DK1.Value != 0)
            {
                if (vControlBoxRecord.DK1 == 1)
                    m_Port1 = new LightenessSensor(m_ControlModuleID);
                if (vControlBoxRecord.DK1 == 2)
                    m_Port1 = new COSensor(m_ControlModuleID);
            }
            else
                m_Port1 = null;
            //端口2
            if (vControlBoxRecord.DK2.Value != 0)
            {
                if (vControlBoxRecord.DK2 == 1)
                    m_Port2 = new LightenessSensor(m_ControlModuleID);
                if (vControlBoxRecord.DK2 == 2)
                    m_Port2 = new COSensor(m_ControlModuleID);
            }
            else
                m_Port2 = null;
            //地感线圈
            if (vControlBoxRecord.DG.Value)
                m_CarSensor = new CarSensor(m_ControlModuleID);
            else
                m_CarSensor = null;
            //备用电源
            if (vControlBoxRecord.BYDY.Value)
                m_EmergencyPowerSensor = new EmergencyPowerSensor(m_ControlModuleID);
            else
                m_EmergencyPowerSensor = null;
            //电源
            if (vControlBoxRecord.DY.Value)
            {
                m_PowerSensor = new PowerSensor(m_ControlModuleID);
            }
            else
                m_PowerSensor = null;
            //高优先级
            m_HighLevel = vControlBoxRecord.GYXJ.Value;

            //PWM模式和PWM输出
            if (vControlBoxRecord.PWMMS.IsNull)
            {
                m_PWMMode = PWMModeEnum.NoPWM;
            }
            else if (vControlBoxRecord.PWMMS.Value == 0)
            {
                m_PWMFixupValue = 0;
                m_PWMDynamicValue = 0;
            }
            else if (vControlBoxRecord.PWMMS.Value == 1)
            {
                m_PWMMode = PWMModeEnum.FixupPWM;
                if (vControlBoxRecord.PWMGDZ.IsNull)
                {
                    m_PWMFixupValue = 0;
                    m_PWMDynamicValue = 0;
                }
                else
                {
                    short vPWMGDZ = vControlBoxRecord.PWMGDZ.Value;
                    m_PWMFixupValue = vPWMGDZ;
                }
            }
            else if (vControlBoxRecord.PWMMS.Value == 2)
                m_PWMMode = PWMModeEnum.DynamicPWM;

            m_PWMDemarcate = vControlBoxRecord.PWMBDZ.IsNull ? 0 : vControlBoxRecord.PWMBDZ.Value;
            m_Area = vControlBoxRecord.QD.IsNull ? (short)0 : vControlBoxRecord.QD.Value;
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 睡眠
        /// </summary>
        public void PWMSleep()
        {
            if (m_PWMDynamicValue != 50)
            {
                m_PWMDynamicValue = 50;
                OnPWMChangedEvent(this, new PWMChangedEventArgs(m_PWMDynamicValue));
            }
        }

        /// <summary>
        /// PWM最大化输出
        /// </summary>
        public void PWMMax()
        {
            if (m_PWMDynamicValue != 100)
            {
                m_PWMDynamicValue = 100;
                OnPWMChangedEvent(this, new PWMChangedEventArgs(m_PWMDynamicValue));
            }
        }

        public int GetPWM()
        {
            int vResultPWMValue = 0;
            if (Power != null)
            {
                if (Power.PowerDown)
                    vResultPWMValue = 0;
                else
                {
                    switch (m_PWMMode)
                    {
                        case PWMModeEnum.NoPWM:
                            vResultPWMValue = 0;
                            break;
                        case PWMModeEnum.FixupPWM:
                            vResultPWMValue = m_PWMFixupValue;
                            break;
                        case PWMModeEnum.DynamicPWM:
                            vResultPWMValue = m_PWMDynamicValue;
                            break;
                    }
                }
            }
            else
            {
                switch (m_PWMMode)
                {
                    case PWMModeEnum.NoPWM:
                        vResultPWMValue = 0;
                        break;
                    case PWMModeEnum.FixupPWM:
                        vResultPWMValue = m_PWMFixupValue;
                        break;
                    case PWMModeEnum.DynamicPWM:
                        vResultPWMValue = m_PWMDynamicValue;
                        break;
                }
            }
            return vResultPWMValue;
        }

        public void CalcPWM(int OutLighteness, bool Car)
        {

            if (m_LightControlPWM != null)
            {
                m_LightControlPWM.out_illumination = OutLighteness;
                int vLightenessValue1 = 0;
                int vLightenessValue2 = 0;

                if (m_Port1 != null && m_Port1.SensorType == SensorTypeEnum.LightenessSensor)
                    vLightenessValue1 = ((LightenessSensor)m_Port1).Lighteness;

                if (m_Port2 != null && m_Port2.SensorType == SensorTypeEnum.LightenessSensor)
                    vLightenessValue2 = ((LightenessSensor)m_Port2).Lighteness;

                if (vLightenessValue1 >=0  && vLightenessValue2 >=0 )
                    m_LightControlPWM.in_illumination = (vLightenessValue1 + vLightenessValue2) / 2;
                else if (vLightenessValue1 >= 0)
                    m_LightControlPWM.in_illumination = vLightenessValue1;
                else if (vLightenessValue2 >= 0)
                    m_LightControlPWM.in_illumination = vLightenessValue2;

                m_LightControlPWM.car_Idle = true;
                m_LightControlPWM.id = m_Area;
                int vPWMValue = m_LightControlPWM.Judge()[1];

                //if (vPWMValue != m_PWMDynamicValue && m_PWMDemarcate != 0)
                if ( m_PWMDemarcate != 0)
                {
                    //计算补偿值
                    double vPWMRepairValue = 0;//PWM补偿值
                    //int vInPWMValue = Convert.ToInt32(m_PWMDemarcate * (m_PWMDynamicValue / 100f)); //洞内照明实际应达到PWM值
                    int vInPWMValue = Convert.ToInt32(m_PWMDemarcate * (vPWMValue / 100f)); //洞内照明实际应达到PWM值
                    if (m_LightControlPWM.in_illumination < vInPWMValue)
                    {
                        vPWMRepairValue = (vInPWMValue - m_LightControlPWM.in_illumination);
                        vPWMRepairValue = vPWMRepairValue / m_PWMDemarcate * 100;
                    }

                    m_PWMDynamicValue = vPWMValue + Convert.ToInt32(vPWMRepairValue);
                    if (m_PWMDynamicValue > 100)
                        m_PWMDynamicValue = 100;
                    OnPWMChangedEvent(this, new PWMChangedEventArgs(m_PWMDynamicValue));
                }
                else
                {
                    if (m_PWMDynamicValue != vPWMValue)
                    {
                        m_PWMDynamicValue = vPWMValue;
                        OnPWMChangedEvent(this, new PWMChangedEventArgs(m_PWMDynamicValue));
                    }
                }
            }
        }
       
        public void ReceiveData(byte[] DataArea1, byte[] DataArea2, byte DataArea3, byte DataArea4,
            byte DataArea5, byte DataArea6, byte[] DataArea7, byte DataArea8)
        {
            //端口1
            if (m_Port1 != null)
            {
                if (m_Port1.SensorType == SensorTypeEnum.LightenessSensor)
                    ((LightenessSensor)m_Port1).SetLightenessValue(DataArea1);
                if (m_Port1.SensorType == SensorTypeEnum.COSensor)
                    ((COSensor)m_Port1).SetStrengthValue(DataArea1);
            }
            //端口2
            if (m_Port2 != null)
            {
                if (m_Port2.SensorType == SensorTypeEnum.LightenessSensor)
                    ((LightenessSensor)m_Port2).SetLightenessValue(DataArea2);
                if (m_Port2.SensorType == SensorTypeEnum.COSensor)
                    ((COSensor)m_Port2).SetStrengthValue(DataArea2);
            }
            //地感线圈
            if (m_CarSensor != null)
            {
                m_CarSensor.SetLoopPowerDown(DataArea3);
                m_CarSensor.SetCarNumber(DataArea5);
            }
            //蓄电池电压
            if ( m_EmergencyPowerSensor != null )
                m_EmergencyPowerSensor.SetVoltageValue(DataArea7);
            //主电源状态
            if ( m_PowerSensor != null )
                m_PowerSensor.SetPowerDownValue(DataArea8);
            m_LastCommunicationTime = DateTime.Now;

            if (m_PowerSensor != null && m_PowerSensor.PowerDown) //通讯超时
                m_State = ModuleState.Timeout;
        }

        public void SaveToDB(byte Address, string Name, Int16 Port1, Int16 Port2,
            bool DG,bool BYDY,bool DY,bool HighLevel,byte PWMMode,Int16 PWMGDZ,int PWMBDZ,Int16 QD )
        {
            ControlBoxTableStruct vControlBox = new ControlBoxTableStruct();
            vControlBox.SBDZ = Address;
            vControlBox.MC = Name;
            vControlBox.DK1 = Port1;
            vControlBox.DK2 = Port2;
            vControlBox.DG = DG;
            vControlBox.BYDY = BYDY;
            vControlBox.DY = DY;
            vControlBox.GYXJ = HighLevel;
            vControlBox.PWMMS = PWMMode;
            vControlBox.PWMGDZ = PWMGDZ;
            vControlBox.PWMBDZ = PWMBDZ;
            vControlBox.QD = QD;
            using (DBClass vDBClass = new DBClass())
            {
                if (vDBClass.UpdateRecord(vControlBox, m_ControlModuleID))
                    init(vControlBox);
            }
        }
        #endregion

        public void Dispose()
        {
            
        }
    }
}
