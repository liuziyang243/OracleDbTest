using System.Collections.Generic;

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
    }
}