using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: IDataAccessor的默认实现类
 */
namespace OracleDbTest.orm
{
    public class DefaultDataAccessor : IDataAccessor
    {
        private const bool PrintSqlFlag = true;
        #region 接口实现

        public T queryEntity<T>(string sql, Dictionary<string, object> parms) where T : class
        {
            List<T> list = queryEntityList<T>(sql, parms);
            return list.Any() ? list[0] : null;
        }

        public List<T> queryEntityList<T>(string sql, Dictionary<string, object> parms) where T : class
        {
            PrintSQL(sql);
            OracleConnection conn = null;
            List<T> result = new List<T>();
            try
            {
                conn = OracleConnectionFactory.OpenConn();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    ParameterHandler.SetParameters(cmd, parms);
                    var reader = cmd.ExecuteReader();
                    result = ResultHandler.GenerateObjFromTable<T>(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleConnectionFactory.CloseConn(conn);
            }

            return result;
        }

        public Dictionary<string, object> queryMap(string sql, Type type, Dictionary<string, object> parms)
        {
            List<Dictionary<string, object>> result = queryMapList(sql, type, parms);
            if (result.Any())
            {
                return result[0];
            }

            return new Dictionary<string, object>();
        }

        public List<Dictionary<string, object>> queryMapList(string sql, Type type, Dictionary<string, object> parms)
        {
            PrintSQL(sql);
            OracleConnection conn = null;
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            try
            {
                conn = OracleConnectionFactory.OpenConn();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    ParameterHandler.SetParameters(cmd, parms);
                    var reader = cmd.ExecuteReader();
                    result = ResultHandler.GenerateResultMapFromTable(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleConnectionFactory.CloseConn(conn);
            }

            return result;
        }

        public T queryColumn<T>(string sql, Dictionary<string, object> parms)
        {
            List<T> list = queryColumnList<T>(sql, parms);
            return list.Any() ? list[0] : default(T);
        }

        public List<T> queryColumnList<T>(string sql, Dictionary<string, object> parms)
        {
            PrintSQL(sql);
            OracleConnection conn = null;
            List<T> result = new List<T>();
            try
            {
                conn = OracleConnectionFactory.OpenConn();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    ParameterHandler.SetParameters(cmd, parms);
                    var reader = cmd.ExecuteReader();
                    result = ResultHandler.GenerateColumnObjFromTable<T>(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleConnectionFactory.CloseConn(conn);
            }

            return result;
        }

        public long queryCount(string sql, Dictionary<string, object> parms)
        {
            PrintSQL(sql);
            OracleConnection conn = null;
            long result = 0;
            try
            {
                conn = OracleConnectionFactory.OpenConn();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    ParameterHandler.SetParameters(cmd, parms);
                    var count = cmd.ExecuteScalar();
                    result = Convert.ToInt64(count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleConnectionFactory.CloseConn(conn);
            }

            return result;
        }

        public int update(string sql, Dictionary<string, object> parms)
        {
            PrintSQL(sql);
            OracleConnection conn = null;
            try
            {
                conn = OracleConnectionFactory.OpenConn();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    ParameterHandler.SetParameters(cmd, parms);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                OracleConnectionFactory.CloseConn(conn);
            }

            return -1;
        }

        #endregion

        #region 打印sql语句

        private void PrintSQL(string sql)
        {
            if (PrintSqlFlag)
            {
                Console.WriteLine("[SQL]:{0}", sql);
            }
        }

        #endregion
    }
}