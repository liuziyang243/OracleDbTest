using System;
using System.Collections.Generic;
using System.Linq;

namespace OracleDbTest.orm
{
    public class DefaultDataSetAccessor : IDataSetAccessor
    {
        // 通过工厂获取dataAccessor
        private readonly IDataAccessor _dataAccessor = OrmEntryFactory.GetDataAccessor();

        public T Select<T>(string condition, params object[] paramList) where T : class
        {
            Type type = typeof(T);
            string sql = SqlHelper.GenSelectSql(type, condition);
            Dictionary<string, object> oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.queryEntity<T>(sql, oracleParams);
        }

        public List<T> SelectList<T>(string condition, params object[] paramList) where T : class
        {
            Type type = typeof(T);
            string sql = SqlHelper.GenSelectSql(type, condition);
            Dictionary<string, object> oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.queryEntityList<T>(sql, oracleParams);
        }

        public bool Insert<T>(T t) where T : class
        {
            Type type = typeof(T);
            string sql = SqlHelper.GenInsertSql(type);
            Dictionary<string, object> oracleParams = ParameterHandler.GetConditionParams(t);
            return _dataAccessor.update(sql, oracleParams) > 0;
        }

        public bool Update<T>(T t, string condition, params object[] paramList) where T : class
        {
            Type type = typeof(T);
            string sql = SqlHelper.GenUpdateSql(type, condition);
            Dictionary<string, object> oracleParams1 = ParameterHandler.GetConditionParams(t);
            Dictionary<string, object> oracleParams2 = ParameterHandler.GetConditionParams(condition, paramList);
            Dictionary<string, object> oracleParams =
                oracleParams1.Concat(oracleParams2).ToDictionary(k => k.Key, v => v.Value);
            return _dataAccessor.update(sql, oracleParams) > 0;
        }

        public bool Del<T>(string condition, params object[] paramList) where T : class
        {
            Type type = typeof(T);
            string sql = SqlHelper.GenDelSql(type, condition);
            Dictionary<string, object> oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.update(sql, oracleParams) > 0;
        }
    }
}