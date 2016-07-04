using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace NCLT.TunnelLighting.DB
{
    public class DBConvert
    {
        public static SqlString ToSqlString(object value)
        {
            return value == DBNull.Value || value == null ? SqlString.Null : (string)value;
        }

        public static string ToString(object value)
        {
            return value == DBNull.Value ? null : (string)value;
        }

        public static SqlDecimal ToSqlDecimal(object value)
        {

            return value == DBNull.Value || value == null ? SqlDecimal.Null : (decimal)value;
        }

        public static decimal ToDecimal(object value)
        {
            return value == DBNull.Value ? 0 : (decimal)value;
        }
        public static decimal ToDecimal(object value, int Decimals)
        {
            decimal resultValue = value == DBNull.Value ? 0 : (decimal)value;
            return Math.Round(resultValue, Decimals);
        }

        public static SqlSingle ToSqlSingle(object value)
        {
            return value == DBNull.Value || value == null ? SqlSingle.Null : (Single)value;
        }

        public static Single ToSingle(object value)
        {
            return value == DBNull.Value ? 0 : (Single)value;
        }

        public static SqlInt16 ToSqlInt16(object value)
        {
            return value == DBNull.Value || value == null ? SqlInt16.Null : (Int16)value;
        }

        public static Int16 ToInt16(object value)
        {
            return value == DBNull.Value ? (Int16)0 : (Int16)value;
        }

        public static SqlInt32 ToSqlInt32(object value)
        {
            return value == DBNull.Value || value == null ? SqlInt32.Null : (Int32)value;
        }

        public static Int32 ToInt32(object value)
        {
            return value == DBNull.Value ? 0 : (Int32)value;
        }

        public static SqlDateTime ToSqlDateTime(object value)
        {
            return value == DBNull.Value || value == null ? SqlDateTime.Null : (DateTime)value;
        }

        public static DateTime ToDateTime(object value)
        {
            return value == DBNull.Value ? DateTime.MinValue : (DateTime)value;
        }

        public static SqlByte ToSqlByte(object value)
        {
            return value == DBNull.Value || value == null ? SqlByte.Null : (byte)value;
        }

        public static SqlBytes ToSqlBytes(object value)
        {
            SqlBytes vSqlBytes = null;
            if (value == DBNull.Value || value == null)
                vSqlBytes = null;
            else
            {
                byte[] vBytes = (byte[])value;
                vSqlBytes = new SqlBytes(vBytes);
            }

            return vSqlBytes;
        }

        public static byte[] ToBytes(object value)
        {
            return value == DBNull.Value ? null : (byte[])value;
        }

        public static byte ToByte(object value)
        {
            return value == DBNull.Value ? (byte)0 : (byte)value;
        }

        public static SqlBoolean ToSqlBoolean(object value)
        {
            return value == DBNull.Value || value == null ? SqlBoolean.Null : (Boolean)value;
        }

        public static bool ToBoolean(object value)
        {
            return value == DBNull.Value ? false : (Boolean)value;
        }

        public static double ToDouble(object value)
        {
            return value == DBNull.Value ? 0 : (double)value;
        }

        public static SqlDouble ToSqlDouble(object value)
        {
            return value == DBNull.Value || value == null ? SqlDouble.Null : (double)value;
        }
    }
}
