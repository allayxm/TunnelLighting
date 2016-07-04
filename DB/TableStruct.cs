using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace NCLT.TunnelLighting.DB
{
    
    #region ControlBox表
    [TableAttrib("ControlBox")]
    public struct ControlBoxTableStruct
    {
        [ColumnAttrib("ID")]
        public SqlInt32 ID;
        [ColumnAttrib("名称")]
        public SqlString MC;
        [ColumnAttrib("设备地址")]
        public SqlByte SBDZ;
        [ColumnAttrib("端口1")]
        public SqlInt16 DK1;
        [ColumnAttrib("端口2")]
        public SqlInt16 DK2;
        [ColumnAttrib("地感")]
        public SqlBoolean DG;
        [ColumnAttrib("备用电源")]
        public SqlBoolean BYDY;
        [ColumnAttrib("电源")]
        public SqlBoolean DY;
        [ColumnAttrib("高优先级")]
        public SqlBoolean GYXJ;
        [ColumnAttrib("PWM模式")]
        public SqlByte PWMMS;
        [ColumnAttrib("PWM固定值")]
        public SqlInt16 PWMGDZ;
        [ColumnAttrib("PWM标定值")]
        public SqlInt32 PWMBDZ;
        [ColumnAttrib("区段")]
        public SqlInt16 QD;
    }
    #endregion

    public struct DataPackStruct
    {
        public int Lighteness;
        public int COStrength;
        public bool LoopPowerDown;  //地感线圈掉电
        public bool CarSensorPowerDown; //地感测试仪掉电
        public ulong CarNumber;//车辆计数
        public double EmergencyPowerVoltage;//备用电池电压
        public bool PowerDown;//主电源
    }

    #region SystemInfo表
    [TableAttrib("SystemInfo")]
    public struct SystemInfoStruct
    {
        [ColumnAttrib("项目名")]
        public SqlString XMM;
        [ColumnAttrib("项目值I")]
        public SqlInt32 XMZ_I;
        [ColumnAttrib("项目值D")]
        public SqlDouble XMZ_D;
        [ColumnAttrib("项目值S")]
        public SqlString XMZ_S;
        [ColumnAttrib("项目值B")]
        public SqlBoolean XMZ_B;
    }
    #endregion
}
