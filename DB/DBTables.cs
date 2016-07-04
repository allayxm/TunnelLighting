using System;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
using System.Data.OleDb;

namespace NCLT.TunnelLighting.DB
{
    #region 数据基类
    public interface BaseDBI<T> : IDisposable
    {
        bool DeleteRecord(int ID);
        bool InsertRecord(T Record);
        DataTable SelectAllRecords();
        DataTable SelectRecordByID(int ID);
        DataTable SelectRecords(T Record);
        bool UpdateRecord(T Record, int ID);
        bool UpdateRecord(T Record, string Condition);
    }

    public abstract class BasicDBClass : IDisposable
    {
        protected static OleDbConnection m_DbConnection;
        protected static OleDbCommand m_DbCommand;
        protected static OleDbDataAdapter m_DbDataAdapter;
        protected string m_TableName = string.Empty;
        protected string m_TableViewName = string.Empty;

        protected bool m_IsUserView = false;
        public bool IsUserView
        {
            set
            {
                m_IsUserView = value;
            }
            get
            {
                return m_IsUserView;
            }
        }

        //"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=c:\App1\你的数据库名.mdb; User Id=admin; Password=" 
        public BasicDBClass()
        {
            if (m_DbConnection == null)
            {
                string vDBPath = string.Format("{0}Data.mdb", System.AppDomain.CurrentDomain.BaseDirectory);
                string sqlConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; User Id=admin; Password=", vDBPath);
                m_DbConnection = new OleDbConnection(sqlConn);
                m_DbCommand = new OleDbCommand();
                m_DbCommand.Connection = m_DbConnection;
                m_DbDataAdapter = new OleDbDataAdapter(m_DbCommand);
            }
        }

        //public BasicDBClass()
        //{
        //    //m_TableName = "[" + TableName + "]";
        //    //m_TableViewName = "[" + TableViewName + "]";
        //    string vDBPath = string.Format("{0}Data.mdb", System.AppDomain.CurrentDomain.BaseDirectory);
        //    string sqlConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; User Id=admin; Password=", vDBPath);
        //    m_DbConnection = new OleDbConnection(sqlConn);
        //    m_DbCommand = new OleDbCommand();
        //    m_DbCommand.Connection = m_DbConnection;
        //    m_DbDataAdapter = new OleDbDataAdapter(m_DbCommand);
        //}

        #region 事务
        public void TransactionBegin()
        {
            m_DbConnection.Open();
            OleDbTransaction transaction = m_DbConnection.BeginTransaction();
            m_DbCommand.Transaction = transaction;
        }

        public void TransactionCommit()
        {
            m_DbCommand.Transaction.Commit();
            m_DbCommand.Transaction = null;
        }

        public void TransactionRollback()
        {
            m_DbCommand.Transaction.Rollback();
            m_DbCommand.Transaction = null;
        }

        #endregion

        public string TableName
        {
            set
            {
                m_TableName = "[" + value + "]";
            }
            get
            {
                return m_TableName;
            }
        }

        protected bool OpenConnection()
        {
            try
            {
                if (m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    m_DbConnection.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        void MarkInsertSql<T>(T Record, out string Sql, out OleDbParameter[] SqlParameters)
        {
            List<OleDbParameter> sqlParas = new List<OleDbParameter>();
            string fieldsSql = "", valuesSql = "";
            SqlParameters = null;

            Type recordType = Record.GetType();
            FieldInfo[] fieldInfos = recordType.GetFields();

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && !columnAttrib.IsViewColumn)
                    {
                        object fieldValue = fieldInfo.GetValue(Record);
                        if (!((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            fieldsSql += string.Format("[{0}],", columnAttrib.ColumnName);
                            valuesSql += string.Format("@{0},", columnAttrib.ColumnName);
                            OleDbParameter newSqlParam = new OleDbParameter();
                            newSqlParam.OleDbType = ConvertTypeToDbType(fieldValue);
                            newSqlParam.Value = ConvertDbTypeValue(fieldValue);
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }
                    }
                }
            }

            //去除最后一个","号
            if (fieldsSql != "")
                fieldsSql = fieldsSql.Remove(fieldsSql.Length - 1);
            if (valuesSql != "")
                valuesSql = valuesSql.Remove(valuesSql.Length - 1);

            Sql = string.Format("Insert Into {0} ( {1} ) Values ( {2} )", m_TableName, fieldsSql, valuesSql);
            SqlParameters = sqlParas.ToArray();
            sqlParas.Clear();
        }

        void MarkSelectSql<T>(T Record, out string Sql, out OleDbParameter[] SqlParameters, string Sort, string[] Columns)
        {
            List<OleDbParameter> sqlParas = new List<OleDbParameter>();
            string selectSql = string.Empty;
            string OutColumnsSql = string.Empty;
            SqlParameters = null;

            Type recordType = Record.GetType();
            FieldInfo[] fieldInfos = recordType.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if ((columnAttrib != null && !columnAttrib.IsViewColumn) || (columnAttrib != null && (columnAttrib.IsViewColumn && m_TableViewName != string.Empty)))
                    {
                        object fieldValue = fieldInfo.GetValue(Record);
                        if (!((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            selectSql += string.Format("([{0}]=@{0}) and ", columnAttrib.ColumnName);
                            OleDbParameter newSqlParam = new OleDbParameter();
                            newSqlParam.OleDbType = ConvertTypeToDbType(fieldValue);
                            newSqlParam.Value = ConvertDbTypeValue(fieldValue);
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }

                        if (Columns != null)
                        {
                            bool is_Out = false;
                            foreach (string column in Columns)
                            {
                                if (column == columnAttrib.ColumnName)
                                {
                                    is_Out = true;
                                    break;
                                }
                            }
                            if (is_Out)
                                OutColumnsSql += string.Format("[{0}],", columnAttrib.ColumnName);
                        }
                    }
                }
            }

            if (selectSql != string.Empty)
                selectSql = selectSql.Remove(selectSql.Length - 5);
            if (OutColumnsSql != string.Empty)
                OutColumnsSql = OutColumnsSql.Remove(OutColumnsSql.Length - 1);
            else
                OutColumnsSql = "*";

            if (selectSql != string.Empty)
            {
                if (Sort == "")
                    Sql = string.Format("Select {0} From  {1} Where {2}", OutColumnsSql, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName, selectSql);
                else
                    Sql = string.Format("Select {0} From  {1} Where {2} Order By {3}", OutColumnsSql, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName, selectSql, Sort);
                SqlParameters = sqlParas.ToArray();
            }
            else
            {
                if (Sort == "")
                    Sql = string.Format("Select {0} From  {1}", OutColumnsSql, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName);
                else
                    Sql = string.Format("Select {0} From  {1} Order By {2}", OutColumnsSql, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName, Sort);
            }

        }


        protected void MarkUpdateSql<T>(T Record, string Condition, out string Sql, out OleDbParameter[] SqlParameters)
        {
            List<OleDbParameter> sqlParas = new List<OleDbParameter>();
            string setSql = string.Empty;
            SqlParameters = null;

            Type recordType = Record.GetType();
            FieldInfo[] fieldInfos = recordType.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                object[] attributes = fieldInfo.GetCustomAttributes(true);
                foreach (Attribute tempAttr in attributes)
                {
                    ColumnAttrib columnAttrib = tempAttr as ColumnAttrib;
                    if (columnAttrib != null && !columnAttrib.IsViewColumn)
                    {
                        object fieldValue = fieldInfo.GetValue(Record);
                        if (!((INullable)fieldValue).IsNull && columnAttrib.ColumnName.ToUpper() != "ID")
                        {
                            setSql += string.Format("[{0}]=@{0},", columnAttrib.ColumnName);
                            OleDbParameter newSqlParam = new OleDbParameter();
                            newSqlParam.OleDbType = ConvertTypeToDbType(fieldValue);
                            newSqlParam.Value = ConvertDbTypeValue(fieldValue);
                            newSqlParam.ParameterName = string.Format("@{0}", columnAttrib.ColumnName);
                            sqlParas.Add(newSqlParam);
                        }
                    }
                }
            }

            if (setSql != string.Empty)
            {
                setSql = setSql.Remove(setSql.Length - 1);
                Sql = string.Format("Update {0} Set {1} Where {2}", m_TableName, setSql, Condition);
                SqlParameters = sqlParas.ToArray();
            }
            else
                Sql = string.Empty;
        }

        object ConvertDbTypeValue(Object ObjectType)
        {
            if (ObjectType is SqlBinary)
                return ((INullable)ObjectType).IsNull ? null : ((SqlBytes)ObjectType).Value;
            else if (ObjectType is SqlBoolean)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlBoolean)ObjectType).Value;
            else if (ObjectType is SqlByte)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlByte)ObjectType).Value;
            else if (ObjectType is SqlDateTime)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlDateTime)ObjectType).Value;
            else if (ObjectType is SqlDecimal)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlDecimal)ObjectType).Value;
            else if (ObjectType is SqlDouble)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlDouble)ObjectType).Value;
            else if (ObjectType is SqlGuid)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlGuid)ObjectType).Value;
            else if (ObjectType is SqlInt16)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlInt16)ObjectType).Value;
            else if (ObjectType is SqlInt32)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlInt32)ObjectType).Value;
            else if (ObjectType is SqlInt64)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlInt64)ObjectType).Value;
            else if (ObjectType is SqlMoney)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlMoney)ObjectType).Value;
            else if (ObjectType is SqlSingle)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlDouble)ObjectType).Value;
            else if (ObjectType is SqlString)
                return ((INullable)ObjectType).IsNull ? (object)null : ((SqlString)ObjectType).Value;
            else
                return ((INullable)ObjectType).IsNull ? (object)null : ( (SqlString)ObjectType ).Value;
        }

        OleDbType ConvertTypeToDbType(Object ObjectType)
        {
            
            if (ObjectType is SqlBinary)
                return OleDbType.Binary;
            else if (ObjectType is SqlBoolean)
                return OleDbType.Boolean;
            else if (ObjectType is SqlByte)
                return OleDbType.UnsignedTinyInt;
            else if (ObjectType is SqlDateTime)
                return OleDbType.Date;
            else if (ObjectType is SqlDecimal)
                return OleDbType.Decimal;
            else if (ObjectType is SqlDouble)
                return OleDbType.Single;
            else if (ObjectType is SqlFileStream)
                return OleDbType.VarBinary;
            else if (ObjectType is SqlGuid)
                return OleDbType.Guid;
            else if (ObjectType is SqlInt16)
                return OleDbType.SmallInt;
            else if (ObjectType is SqlInt32)
                return OleDbType.Integer;
            else if (ObjectType is SqlInt64)
                return OleDbType.BigInt;
            else if (ObjectType is SqlMoney)
                return OleDbType.Numeric;
            else if (ObjectType is SqlSingle)
                return OleDbType.Double;
            else if (ObjectType is SqlString)
                return OleDbType.BSTR;
            else
                return OleDbType.BSTR;
        }


        public virtual DataTable SelectRecords<T>(T Record, string Sort, string Columns)
        {
            initTableName(Record);
            DataTable resultTable = new DataTable(m_TableName);
            string OleDbCommand;

            OleDbParameter[] OleDbParameters = null;
            if (Columns == "")
                MarkSelectSql(Record, out OleDbCommand, out OleDbParameters, Sort, null);
            else
            {
                string[] ColumnsArray = Columns.Split(',');
                MarkSelectSql(Record, out OleDbCommand, out OleDbParameters, Sort, ColumnsArray);
            }

            if (OleDbCommand != string.Empty)
            {
                m_DbCommand.CommandText = OleDbCommand;
                m_DbCommand.Parameters.Clear();
                if (OleDbParameters != null)
                    m_DbCommand.Parameters.AddRange(OleDbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        public virtual DataTable SelectRecords<T>(T Record)
        {
            initTableName(Record);
            DataTable resultTable = new DataTable(m_TableName);
            string OleDbCommand;
            OleDbParameter[] OleDbParameters = null;
            MarkSelectSql(Record, out OleDbCommand, out OleDbParameters, "", null);
            if (OleDbCommand != string.Empty)
            {
                m_DbCommand.CommandText = OleDbCommand;
                m_DbCommand.Parameters.Clear();
                if (OleDbParameters != null)
                    m_DbCommand.Parameters.AddRange(OleDbParameters);
                if (OpenConnection())
                {
                    m_DbDataAdapter.FillSchema(resultTable, SchemaType.Mapped);
                    m_DbDataAdapter.Fill(resultTable);
                }
            }
            return resultTable;
        }

        public virtual bool InsertRecord<T>(T Record)
        {
            initTableName(Record);
            string OleDbCommand;
            OleDbParameter[] OleDbParameters = null;
            MarkInsertSql(Record, out OleDbCommand, out OleDbParameters);
            if (OleDbCommand != string.Empty && OleDbParameters != null)
            {
                m_DbCommand.CommandText = OleDbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(OleDbParameters);

                if (OpenConnection())
                {
                    if (m_DbCommand.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public virtual bool InsertRecord<T>(T Record, ref int ReturnID)
        {
            initTableName(Record);
            string OleDbCommand;
            OleDbParameter[] OleDbParameters = null;
            MarkInsertSql(Record, out OleDbCommand, out OleDbParameters);
            if (OleDbCommand != string.Empty && OleDbParameters != null)
            {
                m_DbCommand.CommandText = OleDbCommand;// +";SELECT SCOPE_IDENTITY() as NewID"; ;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(OleDbParameters);

                if (OpenConnection())
                {
                    if (m_DbCommand.ExecuteNonQuery() > 0)
                    {
                        m_DbCommand.Parameters.Clear();
                        m_DbCommand.CommandText = String.Format("Select Max(ID) From {0}", m_TableName);
                        ReturnID = Convert.ToInt32(m_DbCommand.ExecuteScalar());
                        if (ReturnID != 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                  
                }
                else
                    return false;
            }
            else
                return false;
        }

        public virtual bool UpdateRecord<T>(T Record, int ID)
        {
            initTableName(Record);
            string OleDbCommand;
            OleDbParameter[] OleDbParameters = null;
            MarkUpdateSql(Record, string.Format("ID={0}", ID), out OleDbCommand, out OleDbParameters);
            if (OleDbCommand != string.Empty && OleDbParameters != null)
            {
                m_DbCommand.CommandText = OleDbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(OleDbParameters);

                if (OpenConnection())
                {
                    if (m_DbCommand.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }

        //protected bool Base_UpdateRecord<T>(T Record, string Condition)
        //{
        //    string OleDbCommand;
        //    OleDbParameter[] OleDbParameters = null;
        //    MarkUpdateSql(Record, Condition, out OleDbCommand, out OleDbParameters);
        //    if (OleDbCommand != string.Empty && OleDbParameters != null)
        //    {
        //        m_DbCommand.CommandText = OleDbCommand;
        //        m_DbCommand.Parameters.Clear();
        //        m_DbCommand.Parameters.AddRange(OleDbParameters);

        //        OpenConnection();
        //        if (m_DbCommand.ExecuteNonQuery() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //    else
        //        return false;
        //}

        protected void initTableName<T>( T TableStruct)
        {
            Type vTableStructType = TableStruct.GetType();
            object[] vTableAttribs = vTableStructType.GetCustomAttributes( typeof( TableAttrib),true );
            if (vTableAttribs.Length > 0)
            {
                TableAttrib vTableAttrib = vTableAttribs[0] as TableAttrib;
                m_TableName = vTableAttrib.TableName;
                m_TableViewName = vTableAttrib.TableViewName;
            }
        }

        public virtual DataTable SelectAllRecords<T>() where T : new()
        {
            initTableName(new T());
            DataTable records = new DataTable(m_TableName);
            m_DbCommand.CommandText = string.Format("Select *From {0}", m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName);
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(records, SchemaType.Mapped);
                m_DbDataAdapter.Fill(records);
            }
            return records;
        }

        public virtual DataTable SelectAllRecords<T>(string Sort, string Columns) where T : new()
        {
            initTableName(new T());
            DataTable records = new DataTable(m_TableName);
            string vSqlColumns = "*";
            if (Columns != null && Columns != "")
                vSqlColumns = Columns;
            if (Sort != null && Sort != "")
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Order By {2}", Columns, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName, Sort);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1}", Columns, m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName);
            m_DbCommand.Parameters.Clear();

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(records, SchemaType.Mapped);
                m_DbDataAdapter.Fill(records);
            }
            return records;
        }

        public virtual bool DeleteRecord<T>(int ID) where T : new()
        {
            initTableName(new T());
            m_DbCommand.CommandText = string.Format("Delete From {0} Where ID=@ID", m_TableName);
            m_DbCommand.Parameters.Clear();

            m_DbCommand.Parameters.Add("@ID",OleDbType.Integer);
            m_DbCommand.Parameters["@ID"].Value = ID;

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public virtual DataTable SelectRecordByID<T>(int ID) where T : new()
        {
            initTableName(new T());
            DataTable table = new DataTable();
            m_DbCommand.CommandText = string.Format("Select *From {0} Where ID=@ID", m_TableViewName == string.Empty && !m_IsUserView ? m_TableName : m_TableViewName);
            m_DbCommand.Parameters.Clear();

            m_DbCommand.Parameters.Add("@ID", OleDbType.Integer);
            m_DbCommand.Parameters["@ID"].Value = ID;

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(table, SchemaType.Mapped);
                m_DbDataAdapter.Fill(table);
            }

            return table;
        }

        public virtual DataTable SelectRecordByID<T>(int ID, string Columns) where T : new()
        {
            initTableName(new T());
            DataTable table = new DataTable();
            if (Columns == "")
                m_DbCommand.CommandText = string.Format("Select *From {0} Where ID=@ID", m_TableViewName == string.Empty && m_IsUserView ? m_TableName : m_TableViewName);
            else
                m_DbCommand.CommandText = string.Format("Select {0} From {1} Where ID=@ID", Columns, m_TableViewName == string.Empty ? m_TableName : m_TableViewName);
            m_DbCommand.Parameters.Clear();

            m_DbCommand.Parameters.Add("@ID", OleDbType.Integer );
            m_DbCommand.Parameters["@ID"].Value = ID;

            if (OpenConnection())
            {
                m_DbDataAdapter.FillSchema(table, SchemaType.Mapped);
                m_DbDataAdapter.Fill(table);
            }

            return table;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            //if (m_DbConnection != null)
            //{
            //    m_DbConnection.Close();
            //    m_DbConnection.Dispose();
            //    m_DbConnection = null;
            //}

            //if (m_DbCommand != null)
            //{
            //    m_DbCommand.Dispose();
            //    m_DbCommand = null;
            //}

            //if (m_DbDataAdapter != null)
            //{
            //    m_DbDataAdapter.Dispose();
            //    m_DbDataAdapter = null;
            //}
            //GC.SuppressFinalize(this);
        }
        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            //if (m_DbConnection != null)
            //{
            //    m_DbConnection.Close();
            //    m_DbConnection.Dispose();
            //    m_DbConnection = null;
            //}

            //if (m_DbCommand != null)
            //{
            //    m_DbCommand.Dispose();
            //    m_DbCommand = null;
            //}

            //if (m_DbDataAdapter != null)
            //{
            //    m_DbDataAdapter.Dispose();
            //    m_DbDataAdapter = null;
            //}
            //GC.SuppressFinalize(this);
        }

        #endregion

       
        public bool UpdateRecord<T>(T Record, string Condition)
        {
            initTableName(Record);
            string OleDbCommand;
            OleDbParameter[] OleDbParameters = null;
            MarkUpdateSql(Record, Condition, out OleDbCommand, out OleDbParameters);
            if (OleDbCommand != string.Empty && OleDbParameters != null)
            {
                m_DbCommand.CommandText = OleDbCommand;
                m_DbCommand.Parameters.Clear();
                m_DbCommand.Parameters.AddRange(OleDbParameters);

                OpenConnection();
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }

    public class DBClass : BasicDBClass
    {
        public int Comput_ControlBox_RK( bool RKValue )
        {
            int vResultValue = -1;
            m_DbCommand.CommandText = "Select Count(*) From {0} Where 入口=@RKValue";
            m_DbCommand.Parameters.Clear();

            m_DbCommand.Parameters.Add("@RKValue", OleDbType.Boolean);
            m_DbCommand.Parameters["@RKValue"].Value = RKValue;

            if (OpenConnection())
            {
                object vSqlValue =  m_DbCommand.ExecuteScalar();
                if (vSqlValue == DBNull.Value)
                    vResultValue = 0;
                else
                    vResultValue = (int)vSqlValue;
            }
            return vResultValue;
        }
        /// <summary>
        /// 更新SysteInfo表
        /// </summary>
        /// <param name="XMM">需要更新的项目名称</param>
        /// <param name="XMZS">需要更新的项目值S</param>
        /// <returns></returns>
        public bool Update_SystemInfo_XMZS( string XMM,string XMZS  )
        {
            initTableName(new SystemInfoStruct());
            m_DbCommand.Parameters.Clear();
            
            m_DbCommand.CommandText = string.Format( "Update {0} Set [项目值S]=@XMZS Where [项目名]=@XMM",m_TableName );

            m_DbCommand.Parameters.Add("@XMZS", OleDbType.VarChar);
            m_DbCommand.Parameters["@XMZS"].Value = XMZS;

            m_DbCommand.Parameters.Add("@XMM", OleDbType.VarChar);
            m_DbCommand.Parameters["@XMM"].Value = XMM;

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public DataTable Select_ControlBox_DG( bool DG )
        {
            DataTable vResultDataTable = new DataTable();
            m_DbCommand.Parameters.Clear();
            m_DbCommand.CommandText = "Select *From ControlBox Where 地感=@DG";

            m_DbCommand.Parameters.Add("@DG", OleDbType.Boolean);
            m_DbCommand.Parameters["@DG"].Value = DG;

            if (OpenConnection())
            {
                m_DbDataAdapter.Fill(vResultDataTable);
            }

            return vResultDataTable;
        }

        public DataTable Select_ControlBox_DK( short DK1,short DK2 )
        {
            DataTable vResultDataTable = new DataTable();
            m_DbCommand.Parameters.Clear();
            m_DbCommand.CommandText = "Select *From ControlBox Where 端口1=@DK1 or 端口2=@DK2";

            m_DbCommand.Parameters.Add("@DK1", OleDbType.SmallInt);
            m_DbCommand.Parameters["@DK1"].Value = DK1;

            m_DbCommand.Parameters.Add("@DK2", OleDbType.SmallInt);
            m_DbCommand.Parameters["@DK2"].Value = DK2;

            if (OpenConnection())
            {
                m_DbDataAdapter.Fill(vResultDataTable); 
            }

            return vResultDataTable;
        }
        
        public bool Update_SystemInfo_XMZI(string XMM, int XMZI)
        {
            initTableName(new SystemInfoStruct());
            m_DbCommand.Parameters.Clear();

            m_DbCommand.CommandText = string.Format("Update {0} Set [项目值I]=@XMZI Where [项目名]=@XMM", m_TableName);

            m_DbCommand.Parameters.Add("@XMZI", OleDbType.Integer);
            m_DbCommand.Parameters["@XMZI"].Value = XMZI;

            m_DbCommand.Parameters.Add("@XMM", OleDbType.VarChar);
            m_DbCommand.Parameters["@XMM"].Value = XMM;

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        public bool Update_SystemInfo_XMZB(string XMM, bool XMZB)
        {
            initTableName(new SystemInfoStruct());
            m_DbCommand.Parameters.Clear();

            m_DbCommand.CommandText = string.Format("Update {0} Set [项目值B]=@XMZB Where [项目名]=@XMM", m_TableName);

            m_DbCommand.Parameters.Add("@XMZB", OleDbType.Boolean);
            m_DbCommand.Parameters["@XMZB"].Value = XMZB;

            m_DbCommand.Parameters.Add("@XMM", OleDbType.VarChar);
            m_DbCommand.Parameters["@XMM"].Value = XMM;

            if (OpenConnection())
            {
                if (m_DbCommand.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
    }
    #endregion

    /// <summary>
    /// 传感器类型
    /// LightenessSensor[1]：照度传感器  COSensor[２]：ＣＯ传感器　CarSensor[3]:车辆传感器　EmergencyPowerSensor[4]：备用电源传感器　PowerSensor[５]：电源传感器
    /// </summary>
    public enum SensorTypeEnum : short
    {
        LightenessSensor = 1, COSensor = 2, CarSensor = 3, EmergencyPowerSensor = 4, PowerSensor = 5
    }

    /// <summary>
    /// NoPWM[0]:无PWM输出 FixupPWM[1]:固定PWM输出  DynamicPWM[2]:动态PWM输出
    /// </summary>
    public enum PWMModeEnum : byte
    {
        NoPWM=0,FixupPWM=1,DynamicPWM=2
    }

    #region 自定义表属性
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TableAttrib : Attribute
    {
        string m_TableName = string.Empty;
        string m_TableViewName = string.Empty;
        public TableAttrib( string TableName )
        {
            m_TableName = string.Format("[{0}]", TableName);
        }

        public TableAttrib(string TableName, string TableViewName)
        {
            m_TableName = string.Format("[{0}]", TableName);
            m_TableViewName = string.Format("[{0}]", TableViewName);
        }

        public string TableName
        {
            get
            {
                return m_TableName;
            }
        }

        public string TableViewName
        {
            get
            {
                return m_TableViewName;
            }
        }
    }

    /// <summary>
    /// 自定义属性(列)
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttrib : Attribute
    {
        /// <summary>
        /// 是否为视图行
        /// </summary>
        bool m_IsViewColumn = false;
        string m_ColumnName = string.Empty;

        public ColumnAttrib(string ColumnName)
        {
            m_ColumnName = ColumnName;
        }

        public ColumnAttrib(string ColumnName, bool IsViewColumn)
        {
            m_ColumnName = ColumnName;
            m_IsViewColumn = IsViewColumn;

        }

        public bool IsViewColumn
        {
            get
            {
                return m_IsViewColumn;
            }
        }

        public string ColumnName
        {
            get
            {
                return m_ColumnName;
            }
        }
    }
    #endregion

}
