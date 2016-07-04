using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.Sensor
{
    /// <summary>
    /// CO浓度传感器
    /// </summary>
    public class COSensor : SensorObject
    {
        #region CO传感器(电源掉电)
        /// <summary>
        /// 浓度改变
        /// </summary>
        public class COSensorStrengthChangeEventArgs : EventArgs
        {
            public readonly int Strength;
            public COSensorStrengthChangeEventArgs(int vStrength)
            {
                Strength = vStrength;
            }
        }
        public delegate void COSensorStrengthChangeEventHandler(object sender, COSensorStrengthChangeEventArgs e);
        public event COSensorStrengthChangeEventHandler OnCOSensorStrengthChangeEvent;
        #endregion

    

        #region 属性
        /// <summary>
        /// 掉电
        /// </summary>
        bool m_PowerDown = false; 
        public bool PowerDown
        {
            get
            {
                return m_PowerDown;
            }
        }

        /// <summary>
        /// 浓度
        /// </summary>
        int m_Strength;
        public  int Strength
        {
            get
            {
                return m_Strength;
            }
        }

        public void SetStrengthValue(byte[] StrengthValue)
        {
            int vStrength = convertStrengthValue(StrengthValue);
            if (vStrength != m_Strength)
            {
                m_Strength = vStrength;
                if ( OnCOSensorStrengthChangeEvent != null )
                    OnCOSensorStrengthChangeEvent(this, new COSensorStrengthChangeEventArgs(m_Strength));
            }
        }

        int convertStrengthValue(byte[] vStrength )
        {
            return 0;
        }
        #endregion

        #region 构造
        public COSensor(int vControlModuleID)
            : base( vControlModuleID,SensorTypeEnum.COSensor)
        {
            m_Strength = 0;
            m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.COSensor_PowerDown;
        }
        #endregion
    }
}
