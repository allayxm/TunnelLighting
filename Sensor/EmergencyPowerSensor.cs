using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.Sensor
{
    /// <summary>
    /// 备用电源传感器
    /// </summary>
    public class EmergencyPowerSensor : SensorObject
    {
        #region 电压
        /// <summary>
        /// 电压
        /// </summary>
        public class EmergencyPowerSensorVoltageEventArgs : EventArgs
        {
            public readonly double Voltage;
            public EmergencyPowerSensorVoltageEventArgs(double vVoltage)
            {
                Voltage = vVoltage;
            }
        }
        public delegate void EmergencyPowerSensorVoltageEventHandler(object sender, EmergencyPowerSensorVoltageEventArgs e);
        public event EmergencyPowerSensorVoltageEventHandler OnEmergencyPowerSensorVoltageEvent;
        #endregion

        #region 属性
        /// <summary>
        /// 电压
        /// </summary>
        double m_Voltage ;
        public double Voltage
        {
            get
            {
                return m_Voltage;
            }
        
        }

        public void SetVoltageValue(byte[] VoltageValue)
        {
            if (VoltageValue.Length == 2)
            {
                double vVoltageValue = convertEmergencyPowerVoltageValue( VoltageValue );
                if (vVoltageValue != m_Voltage)
                {
                    m_Voltage = vVoltageValue;
                    if ( OnEmergencyPowerSensorVoltageEvent != null )
                        OnEmergencyPowerSensorVoltageEvent(this, new EmergencyPowerSensorVoltageEventArgs(m_Voltage));
                }
            }
        }

        double convertEmergencyPowerVoltageValue(byte[] voltageValue)
        {
            int vADC = BitConverter.ToInt16(voltageValue, 0);
            double vVoltageValue_D = (vADC * 5.0 * 11) / 1023;
            return vVoltageValue_D;
        }
        #endregion

        #region 构造
        public EmergencyPowerSensor(int vControlModuleID)
            : base( vControlModuleID, SensorTypeEnum.EmergencyPowerSensor)
        {
            m_Voltage = 0;
            m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.EmergencyPowerSensor;
        }
        #endregion

    }
}
