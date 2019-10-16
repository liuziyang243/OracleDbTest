using System;
using System.Collections.Generic;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 数据库操作接口，对一些低层次操作进行封装，主要完成执行sql语句并返回
 * 相关结果
 */
namespace OracleDbTest.orm
{
    public interface IDataAccessor
    {
        /// <summary>
        /// 查询对应实体，返回单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        T QueryEntity<T>(string sql, Dictionary<string, object> parms) where T : class;
        /// <summary>
        /// 查询对应实体，返回多条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        List<T> QueryEntityList<T>(string sql, Dictionary<string, object> parms) where T : class;
        /// <summary>
        /// 查询对应的数据，返回单条记录（列名 => 数据）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">数据类型</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        Dictionary<string, object> QueryMap(string sql, Type type, Dictionary<string, object> parms);
        /// <summary>
        /// 查询对应的数据，返回多条记录（列名 => 数据）
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">数据类型</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        List<Dictionary<string, object>> QueryMapList(string sql, Type type, Dictionary<string, object> parms);
        /// <summary>
        /// 查询单列数据，返回一条单列数据（列名 => 数据）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        T QueryColumn<T>(string sql, Dictionary<string, object> parms);
        /// <summary>
        /// 查询单列数据，返回多条单列数据（列名 => 数据）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        List<T> QueryColumnList<T>(string sql, Dictionary<string, object> parms);
        /// <summary>
        /// 查询记录条数，返回总记录数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        long QueryCount(string sql, Dictionary<string, object> parms);
        /// <summary>
        /// 执行更新操作（包括：update、insert、delete），返回所更新的记录数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parms">参数列表，key：具名参数，value：参数值</param>
        /// <returns></returns>
        int Update(string sql, Dictionary<string, object> parms);
    }
}