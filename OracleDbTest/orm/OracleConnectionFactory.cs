using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleDbTest.orm
{
    public class OracleConnectionFactory
    {
        #region 数据库连接操作

        public static OracleConnection OpenConn()
        {
            var conn = new OracleConnection
            {
                ConnectionString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.2.32.189)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=oracle.db.server)));Persist Security Info=True;User ID=crscd;Password=crscd123@;"
            };
            conn.Open();
            return conn;
        }

        public static void CloseConn(OracleConnection conn)
        {
            if (conn == null)
            {
                return;
            }

            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }

        #endregion
    }
}