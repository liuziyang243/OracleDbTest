using System.Collections.Generic;
using System.Linq;

/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: IDataSetAccessor的默认实现类
 */
namespace OracleDbTest.orm
{
    public class DefaultDataSetAccessor : IDataSetAccessor
    {
        // 通过工厂获取dataAccessor
        private readonly IDataAccessor _dataAccessor = OrmEntryFactory.GetDataAccessor();

        public T Select<T>(string condition, params object[] paramList) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenSelectSql(type, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.QueryEntity<T>(sql, oracleParams);
        }

        public List<T> SelectList<T>(string condition, params object[] paramList) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenSelectSql(type, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.QueryEntityList<T>(sql, oracleParams);
        }

        public bool Insert<T>(T t) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenInsertSql(type);
            var oracleParams = ParameterHandler.GetConditionParams(t);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }

        public bool Update<T>(T t, string condition, params object[] paramList) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenUpdateSql(type, condition);
            var oracleParams1 = ParameterHandler.GetConditionParams(t);
            var oracleParams2 = ParameterHandler.GetConditionParams(condition, paramList);
            var oracleParams =
                oracleParams1.Concat(oracleParams2).ToDictionary(k => k.Key, v => v.Value);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }

        public bool Del<T>(string condition, params object[] paramList) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenDelSql(type, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }

        public long GetCount<T>(string condition, params object[] paramList) where T : class
        {
            var type = typeof(T);
            var sql = SqlHelper.GenCountSql(type, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.QueryCount(sql, oracleParams);
        }

        public List<T> GetColumnList<T>(string table, string column, string condition, params object[] paramList)
        {
            var sql = SqlHelper.GenSelectSql(table, column, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.QueryColumnList<T>(sql, oracleParams);
        }

        public bool InsertColumnData(string table, Dictionary<string, object> columnDatMap)
        {
            var sql = SqlHelper.GenInsertSql(table, columnDatMap);
            var oracleParams = ParameterHandler.GetConditionParams(columnDatMap);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }

        public bool UpdateColumnData(string table, Dictionary<string, object> columnDatMap, string condition,
            params object[] paramList)
        {
            var sql = SqlHelper.GenUpdateSql(table, columnDatMap, condition);
            var oracleParams1 = ParameterHandler.GetConditionParams(columnDatMap);
            var oracleParams2 = ParameterHandler.GetConditionParams(condition, paramList);
            var oracleParams =
                oracleParams1.Concat(oracleParams2).ToDictionary(k => k.Key, v => v.Value);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }

        public bool DelData(string table, string condition, params object[] paramList)
        {
            var sql = SqlHelper.GenDelSql(table, condition);
            var oracleParams = ParameterHandler.GetConditionParams(condition, paramList);
            return _dataAccessor.Update(sql, oracleParams) > 0;
        }
    }
}