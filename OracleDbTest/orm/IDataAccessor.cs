using System;
using System.Collections.Generic;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 实体操作接口，对一些低层次操作进行封装，主要完成执行sql语句并返回
 * 相关结果
 */
namespace OracleDbTest.orm
{
    public interface IDataAccessor
    {
        // 查询对应实体，返回单条记录
        T QueryEntity<T>(string sql, Dictionary<string, object> parms) where T : class;
        // 查询对应实体，返回多条记录
        List<T> QueryEntityList<T>(string sql, Dictionary<string, object> parms) where T : class;
        // 查询对应的数据，返回单条记录（列名 => 数据）
        Dictionary<string, object> QueryMap(string sql, Type type, Dictionary<string, object> parms);
        // 查询对应的数据，返回多条记录（列名 => 数据）
        List<Dictionary<string, object>> QueryMapList(string sql, Type type, Dictionary<string, object> parms);
        // 查询单列数据，返回一条单列数据（列名 => 数据）
        T QueryColumn<T>(string sql, Dictionary<string, object> parms);
        // 查询单列数据，返回多条单列数据（列名 => 数据）
        List<T> QueryColumnList<T>(string sql, Dictionary<string, object> parms);
        // 查询记录条数，返回总记录数
        long QueryCount(string sql, Dictionary<string, object> parms);
        // 执行更新操作（包括：update、insert、delete），返回所更新的记录数
        int Update(string sql, Dictionary<string, object> parms);
    }
}