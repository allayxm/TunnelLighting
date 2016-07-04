using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCLT.TunnelLighting.DB;

namespace NCLT.TunnelLighting.Sensor
{
    /// <summary>
    /// 车辆传感器
    /// </summary>
    public class CarSensor : SensorObject
    {
        #region 电源掉电
        /// <summary>
        /// 车辆传感器(电源掉电)
        /// </summary>
        public class CarSensorPowerDownEventArgs : EventArgs
        {
            public readonly bool PowerDown;
            public CarSensorPowerDownEventArgs(bool vPowerDown)
            {
                PowerDown = vPowerDown;
            }
        }
        public delegate void CarSensorPowerDownEventHandler(object sender, CarSensorPowerDownEventArgs e);
        public event CarSensorPowerDownEventHandler OnCarSensorPowerDownEvent;
        #endregion

        #region 传感线圈掉电
        public class CarSensingCoilPowerDownEventArgs : EventArgs
        {
            public readonly bool SensingCoilPowerDown;
            public CarSensingCoilPowerDownEventArgs(bool vSensingCoilPowerDown)
            {
                SensingCoilPowerDown = vSensingCoilPowerDown;
            }
        }
        public delegate void CarSensingCoilPowerDownEventHandler(object sender, CarSensingCoilPowerDownEventArgs e);
        public event CarSensingCoilPowerDownEventHandler OnCarSensingCoilPowerDownEvent;
        #endregion

        #region 车辆信号
        public class CarSensingCarNumberEventArgs : EventArgs
        {
            public readonly ulong CarNumber;
            public CarSensingCarNumberEventArgs(ulong vCarNumber)
            {
                CarNumber = vCarNumber;
            }
        }
        public delegate void CarSensingCarNumberEventHandler(object sender, CarSensingCarNumberEventArgs e);
        public event CarSensingCarNumberEventHandler OnCarSensingCarNumberEvent;
        #endregion

        #region 私有变量
        bool m_PowerDown;
        ulong m_CarNumber; //车辆计数
        bool m_LoopPowerDown; //传感线圈掉电
        #endregion
        
        #region 属性
        
        public bool LoopPowerDown
        {
            get
            {
                return m_LoopPowerDown;
            }
         
        }

        public void SetLoopPowerDown(byte LoopPowerDownValue)
        {
            bool vLoopPowerDown = false;
            if (LoopPowerDownValue == 0x00)
            {
                m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.CarSensor_LoopPowerDown;
                vLoopPowerDown = true;
            }
            if (LoopPowerDownValue == 0x01)
            {
                m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.CarSensor;
                vLoopPowerDown = false;
            }
            
            if (vLoopPowerDown != m_LoopPowerDown)
            {
                m_LoopPowerDown = vLoopPowerDown;
                if ( OnCarSensorPowerDownEvent != null )
                    OnCarSensorPowerDownEvent(this, new CarSensorPowerDownEventArgs(m_LoopPowerDown));
            }
        }

        public ulong CarNumber
        {
            get
            {
                return m_CarNumber;
            }
     
        }

        public void SetCarNumber( byte vCarNumber )
        {
            if (vCarNumber == 0xFF)
            {
                if (m_LoopPowerDown != true)
                {
                    OnCarSensingCoilPowerDownEvent(this, new CarSensingCoilPowerDownEventArgs(true));
                    m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.CarSensor_PowerDown;
                    m_LoopPowerDown = true;
                }
            }
            else
            {
                if (m_LoopPowerDown)
                {
                    OnCarSensingCoilPowerDownEvent(this, new CarSensingCoilPowerDownEventArgs(false));
                    m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.CarSensor;
                    m_LoopPowerDown = false;
                }
                m_CarNumber += Convert.ToUInt64( vCarNumber );
                if (OnCarSensingCarNumberEvent != null && vCarNumber!= 0 )
                    OnCarSensingCarNumberEvent(this, new CarSensingCarNumberEventArgs(m_CarNumber));
            }
        }

        #endregion

        #region 构造
        public CarSensor(int vControlModuleID)
            : base(vControlModuleID,SensorTypeEnum.CarSensor)
        {
            m_LoopPowerDown = false;
            m_PowerDown = false;
            m_CarNumber = 0;
            m_StateImage = NCLT.TunnelLighting.Sensor.Properties.Resources.CarSensor_PowerDown;
        }
        #endregion
    }
}
