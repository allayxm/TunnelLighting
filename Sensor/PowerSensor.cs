using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.Sensor
{
    /// <summary>
    /// 电源传感器
    /// </summary>
    public class PowerSensor : SensorObject
    {
        #region CO传感器(电源掉电)
        /// <summary>
        /// CO传感器(电源掉电)
        /// </summary>
        public class PowerSensorPowerDownEventArgs : EventArgs
        {
            public readonly bool PowerDown;
            public PowerSensorPowerDownEventArgs(bool vPowerDown)
            {
                PowerDown = vPowerDown;
            }
        }
        public delegate void PowerSensorPowerDownEventHandler(object sender, PowerSensorPowerDownEventArgs e);
        public event PowerSensorPowerDownEventHandler OnPowerSensorPowerDownEvent;
        #endregion

        #region 构造
        public PowerSensor( int vControlModuleID)
            : base( vControlModuleID, SensorTypeEnum.PowerSensor)
        {
            m_PowerDown = true;
            m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.PowerSensor_PowerDown;
        }
        #endregion

        #region 电源管理
        bool m_PowerDown;
        public bool PowerDown
        {
            get
            {
                return m_PowerDown;
            }
        }
        public void SetPowerDownValue( byte PowerDown )
        {
            bool vPowerDown = false;
            if (PowerDown == 0x00)
                vPowerDown = true;
            if (PowerDown == 0x01)
                vPowerDown = false;
            if (vPowerDown != m_PowerDown)
            {
                if (vPowerDown)
                {
                    m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.PowerSensor_PowerDown;
                    
                }
                else
                    m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.PowerSensor;
                m_PowerDown = vPowerDown;
                if ( OnPowerSensorPowerDownEvent != null )
                    OnPowerSensorPowerDownEvent(this, new PowerSensorPowerDownEventArgs(m_PowerDown));
            }

        }
        #endregion
    }
}
  