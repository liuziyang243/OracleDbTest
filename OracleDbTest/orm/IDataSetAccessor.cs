using System.Collections.Generic;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 对象操作接口，对orm操作进行了接口封装
 */
namespace OracleDbTest.orm
{
    public interface IDataSetAccessor
    {
        /// <summary>
        /// 查询对应实体，返回单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        T Select<T>(string condition, params object[] paramList) where T : class;

        /// <summary>
        /// 查询实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        List<T> SelectList<T>(string condition, params object[] paramList) where T : class;

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        bool Insert<T>(T t) where T : class;

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        bool Update<T>(T t, string condition, params object[] paramList) where T : class;

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        bool Del<T>(string condition, params object[] paramList) where T : class;

        /// <summary>
        /// 查询实体数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        long GetCount<T>(string condition, params object[] paramList) where T : class;

        /// <summary>
        /// 查询单列数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        List<T> GetColumnList<T>(string table, string column, string condition, params object[] paramList);

        /// <summary>
        /// 向指定表格插入指定列数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnDatMap"></param>
        /// <returns></returns>
        bool InsertColumnData(string table, Dictionary<string, object> columnDatMap);

        /// <summary>
        /// 向指定表格更新指定列的数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnDatMap"></param>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        bool UpdateColumnData(string table, Dictionary<string, object> columnDatMap, string condition, params object[] paramList);

        /// <summary>
        /// 在指定表格中删除满足条件的数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        bool DelData(string table, string condition, params object[] paramList);
    }
}