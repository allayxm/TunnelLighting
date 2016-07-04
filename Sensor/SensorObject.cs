using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using NCLT.TunnelLighting.DB;


namespace NCLT.TunnelLighting.Sensor
{
    public class SensorObject
    {
        /// <summary>
        /// 控制盒ID
        /// </summary>
        int m_ControlModuleID = 0;
        public int ControlModuleID
        {
            get
            {
                return m_ControlModuleID;
            }
        }

        SensorTypeEnum m_SensorType;
        public SensorTypeEnum SensorType
        {
            get
            {
                return m_SensorType;
            }
        }

        protected Bitmap m_StateImage;
        public Bitmap StateImage
        {
            get
            {
                return m_StateImage;
            }
        }

        #region 构造
        public SensorObject(int vControlModuleID, SensorTypeEnum vSensorType)
        {
            m_ControlModuleID = vControlModuleID;
            m_SensorType = vSensorType;
        }
        #endregion
    }
}
