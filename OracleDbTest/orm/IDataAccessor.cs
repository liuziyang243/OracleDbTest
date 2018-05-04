using System;
using System.Collections.Generic;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 实体操作接口，对一些低层次操作进行封装
 */
namespace OracleDbTest.orm
{
    public interface IDataAccessor
    {
        // 查询对应实体，返回单条记录
        T queryEntity<T>(string sql, Dictionary<string, object> parms) where T : class;
        // 查询对应实体，返回多条记录
        List<T> queryEntityList<T>(string sql, Dictionary<string, object> parms) where T : class;
        // 查询对应的数据，返回单条记录（列名 => 数据）
        Dictionary<string, object> queryMap(string sql, Type type, Dictionary<string, object> parms);
        // 查询对应的数据，返回多条记录（列名 => 数据）
        List<Dictionary<string, object>> queryMapList(string sql, Type type, Dictionary<string, object> parms);
        // 查询单列数据，返回一条单列数据（列名 => 数据）
        T queryColumn<T>(string sql, Dictionary<string, object> parms);
        // 查询单列数据，返回多条单列数据（列名 => 数据）
        List<T> queryColumnList<T>(string sql, Dictionary<string, object> parms);
        // 查询记录条数，返回总记录数
        long queryCount(string sql, Dictionary<string, object> parms);
        // 执行更新操作（包括：update、insert、delete），返回所更新的记录数
        int update(string sql, Dictionary<string, object> parms);
    }
}