using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 获取oracle数据库连接，并提供关闭接口的功能。
 * 每次请求都会获取到一个新的数据库连接
 * 使用Oracle.ManagedDataAccess方式进行数据库连接，需要使用Oracle.ManagedDataAccess.dll
 * 该方法时oracle提供的官方.net连接数据库方法，并且也是推荐方案
 */
namespace OracleDbTest.orm
{
    public static class OracleConnectionFactory
    {
        #region 数据库连接操作

        public static OracleConnection OpenConn()
        {
            var conn = new OracleConnection
            {
                ConnectionString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=oracle.db.server)));Persist Security Info=True;User ID=crscd;Password=crscd123@;"
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