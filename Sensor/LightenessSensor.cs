using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.Sensor
{
    /// <summary>
    /// 亮度传感器
    /// </summary>
    public class LightenessSensor : SensorObject
    {
        #region 掉电
        ///// <summary>
        ///// 掉电
        ///// </summary>
        //public class LightenessSensorPowerDownEventArgs : EventArgs
        //{
        //    public readonly bool m_PowerDown;
        //    public LightenessSensorPowerDownEventArgs(bool vPowerDown)
        //    {
        //        m_PowerDown = vPowerDown;
        //    }
        //}
        //public delegate void LightenessSensorPowerDownEventHandler(object sender, LightenessSensorPowerDownEventArgs e);
        //public event LightenessSensorPowerDownEventHandler OnLightenessSensorPowerDownEvent;
        #endregion

        #region
        /// <summary>
        /// 亮度改变
        /// </summary>
        public class LightenessSensorCDChangeEventArgs : EventArgs
        {
            public readonly int CDValue;
            public LightenessSensorCDChangeEventArgs(int vCDValue)
            {
                CDValue = vCDValue;
            }
        }
        public delegate void LightenessSensorCDChangeEventHandler(object sender, LightenessSensorCDChangeEventArgs e);
        public event LightenessSensorCDChangeEventHandler OnLightenessCDChangeDownEvent;
        #endregion

        #region 构造
        public LightenessSensor(int vControlModuleID)
            : base( vControlModuleID, SensorTypeEnum.LightenessSensor)
        {
            m_Lighteness = 0;
            m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.LightenessSensor;
        }
        #endregion

        #region 亮度
        int m_Lighteness = 0;
        public int Lighteness
        {
            get
            {
                return m_Lighteness;
            }
        }

        public void SetLightenessValue( byte[] lightenessValue )
        {
            int vLighteness = convertLightenessValue( lightenessValue );
            if ( vLighteness !=  m_Lighteness )
            {
                m_Lighteness = vLighteness;
                if (OnLightenessCDChangeDownEvent != null)
                {
                    if ( m_Lighteness == -3500 )
                        OnLightenessCDChangeDownEvent(this, new LightenessSensorCDChangeEventArgs(0));
                    else
                        OnLightenessCDChangeDownEvent(this, new LightenessSensorCDChangeEventArgs(m_Lighteness));
                }
            }
        }

        /// <summary>
        /// 转换照度值
        /// </summary>
        /// <param name="LightenessValue"></param>
        /// <returns></returns>
        int convertLightenessValue(byte[] lightenessValue)
        {
            int vResultValue = 0;

            UInt16 vADC = BitConverter.ToUInt16(lightenessValue,0);
            double vResultValue_D = (vADC * 3) / (65535 * 0.15);
            vResultValue_D = vResultValue_D <= 4 ? 0 : vResultValue_D;
            double vLUX = (vResultValue_D-4) * ( 200000 / (20 - 4) );
            double vCD = vLUX * 0.07;
            vResultValue = Convert.ToInt32(vCD);
            return vResultValue;
        }
        #endregion
      
    }
}
