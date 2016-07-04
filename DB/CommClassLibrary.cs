using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlTypes;

namespace NCLT.TunnelLighting.DB
{
    /// <summary>
    /// 公用方法类
    /// </summary>
    public class CommClass
    {
        /// <summary>
        /// 将DataRow中的数据转换为Struct
        /// </summary>
        /// <typeparam name="StructT"></typeparam>
        /// <param name="record"></param>
        /// <param name="row"></param>
        public static void ConvertDataRowToStruct<StructT>(ref StructT record, DataRow row)
        {
            Type recordType = record.GetType();
            FieldInfo[] fieldInfos = recordType.GetFields();
            object recordOBJ = (object)record;
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null)
                    {
                        if (columnAttrib.IsViewColumn)
                        {
                            try
                            {
                                object tempObj = row[columnAttrib.ColumnName];
                            }
                            catch
                            {
                                break;
                            }
                        }

                        object fieldValue = null;
                        switch (fieldInfo.FieldType.ToString())
                        {
                            case "System.Data.SqlTypes.SqlBoolean":
                                fieldValue = DBConvert.ToSqlBoolean(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Data.SqlTypes.SqlInt16":
                                fieldValue = DBConvert.ToSqlInt16(row[columnAttrib.ColumnName]);
                                if (fieldValue == null)
                                    fieldValue = DBNull.Value;
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Data.SqlTypes.SqlInt32":
                                fieldValue = DBConvert.ToSqlInt32(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Data.SqlTypes.SqlString":
                                fieldValue = DBConvert.ToSqlString(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Data.SqlTypes.SqlDateTime":
                                fieldValue = DBConvert.ToSqlDateTime(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //float
                            case "System.Data.SqlTypes.SqlSingle":
                                fieldValue = DBConvert.ToSqlSingle(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //Decimal
                            case "System.Data.SqlTypes.SqlDecimal":
                                fieldValue = DBConvert.ToSqlDecimal(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //byte[]
                            case "System.Data.SqlTypes.SqlBytes":
                                fieldValue = DBConvert.ToSqlBytes(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            //double
                            case "System.Data.SqlTypes.SqlDouble":
                                fieldValue = DBConvert.ToSqlDouble(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                            case "System.Data.SqlTypes.SqlByte":
                                fieldValue = DBConvert.ToSqlByte(row[columnAttrib.ColumnName]);
                                fieldInfo.SetValue(recordOBJ, fieldValue);
                                break;
                        }
                    }

                }

            }
            record = (StructT)recordOBJ;
        }


        public static void ConvertStructToDataRow(object Record, DataRow Row)
        {
            Type recordType = Record.GetType();
            FieldInfo[] fieldInfos = recordType.GetFields();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null)
                    {
                        if (!columnAttrib.IsViewColumn && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            object vObjectValue = fieldInfo.GetValue(Record);
                            switch (fieldInfo.FieldType.ToString())
                            {
                                case "System.Data.SqlTypes.SqlBoolean":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlBoolean)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlInt16":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlInt16)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlInt32":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlInt32)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlString":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlString)vObjectValue).Value;
                                    break;
                                case "System.Data.SqlTypes.SqlDateTime":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDateTime)vObjectValue).Value;
                                    break;
                                //float
                                case "System.Data.SqlTypes.SqlSingle":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlSingle)vObjectValue).Value;
                                    break;
                                //Decimal
                                case "System.Data.SqlTypes.SqlDecimal":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDecimal)vObjectValue).Value;
                                    break;
                                //byte[]
                                case "System.Data.SqlTypes.SqlBytes":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlBytes)vObjectValue).Value;
                                    break;
                                //double
                                case "System.Data.SqlTypes.SqlDouble":
                                    Row[columnAttrib.ColumnName] = ((INullable)vObjectValue).IsNull ? (object)DBNull.Value : ((SqlDouble)vObjectValue).Value;
                                    break;
                            }
                        }
                    }

                }
            }
        }

    }
}
