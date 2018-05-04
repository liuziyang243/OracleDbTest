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
        // 查询对应实体，返回单条记录
        T Select<T>(string condition, params object[] paramList) where T : class;

        // 查询实体列表
        List<T> SelectList<T>(string condition, params object[] paramList) where T : class;

        // 插入实体
        bool Insert<T>(T t) where T : class;

        // 更新实体
        bool Update<T>(T t, string condition, params object[] paramList) where T : class;

        // 删除实体
        bool Del<T>(string condition, params object[] paramList) where T : class;

        // 查询实体数量
        long GetCount<T>(string condition, params object[] paramList) where T : class;

        // 查询单列数据
        List<T> GetColumnList<T>(string table, string column, string condition, params object[] paramList);

        // 向指定表格插入指定列数据
        bool InsertColumnData(string table, Dictionary<string, object> columnDatMap);

        // 向指定表格更新指定列的数据
        bool UpdateColumnData(string table, Dictionary<string, object> columnDatMap, string condition, params object[] paramList);

        // 在指定表格中删除满足条件的数据
        bool DelData(string table, string condition, params object[] paramList);
    }
}