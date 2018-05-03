using System;
using System.Collections.Generic;
using System.Text;

namespace OracleDbTest.orm
{
    /**
     * 实现sql语句拼写:
     * 对于插入和生成语句,自动生成a=:a或者(:a,:b)
     * 对于条件语句，使用a=?或者a=? AND b=?,后续会使用参数处理类将?替换为:a_
     * 将列名对应的参数与条件参数区别对待的原因是防止出现重名参数占位的情况
     */
    public class SqlHelper
    {
        // 生成选择语句
        public static string GenSelectSql(Type type)
        {
            return GenSelectSql(type, null);
        }

        // 生成带有条件语句的选择语句
        public static string GenSelectSql(Type type, string condition)
        {
            condition = ConvertCondition(condition);
            var tableName = EntityHelper.GetTableName(type);
            Dictionary<string, string> fieldMap = EntityHelper.GetColumnAttributeMap(type);
            StringBuilder builder = new StringBuilder();
            StringBuilder columns = new StringBuilder();
            foreach (var field in fieldMap)
            {
                columns.Append(field.Key).Append(",");
            }

            builder.Append("SELECT ").Append(columns.ToString(0, columns.Length - 1)).Append(" FROM ")
                .Append(tableName);
            if (condition != null)
            {
                builder.Append(" WHERE ").Append(condition);
            }

            return builder.ToString();
        }

        // 生成带有条件语句的单查询语句
        public static string GenSelectSingleSql(Type type, string condition)
        {
            var sql = GenSelectSql(type, condition);
            sql += " LIMIT 1";
            return sql;
        }

        // 生成插入语句
        public static string GenInsertSql(Type type)
        {
            var tableName = EntityHelper.GetTableName(type);
            Dictionary<string, string> fieldMap = EntityHelper.GetColumnAttributeMap(type);
            var builder = new StringBuilder();
            var columns = new StringBuilder();
            var attributes = new StringBuilder();
            foreach (var entry in fieldMap)
            {
                columns.Append(entry.Key).Append(",");
                attributes.Append(":").Append(entry.Key).Append(",");
            }

            builder.Append("INSERT INTO ").Append(tableName).Append("(").Append(columns.ToString(0, columns.Length - 1))
                .Append(") VALUES (").Append(attributes.ToString(0, attributes.Length - 1)).Append(")");
            return builder.ToString();
        }

        // 生成更新语句
        public static string GenUpdateSql(Type type, string condition)
        {
            condition = ConvertCondition(condition);
            var tableName = EntityHelper.GetTableName(type);
            Dictionary<string, string> fieldMap = EntityHelper.GetColumnAttributeMap(type);
            var builder = new StringBuilder();
            var columns = new StringBuilder();
            foreach (var entry in fieldMap)
            {
                columns.Append(entry.Key).Append("=:").Append(entry.Key).Append(",");
            }

            builder.Append("UPDATE ").Append(tableName).Append(" SET ").Append(columns.ToString(0, columns.Length - 1))
                .Append(" WHERE ").Append(condition);
            return builder.ToString();
        }

        // 生成删除语句
        public static string GenDelSql(Type type, string condition)
        {
            condition = ConvertCondition(condition);
            var tableName = EntityHelper.GetTableName(type);
            var builder = new StringBuilder();
            builder.Append("DELETE FROM ").Append(tableName).Append(" WHERE ").Append(condition);
            return builder.ToString();
        }

        // 生成查询行数语句
        public static string GenCountSql(Type type, string condition)
        {
            condition = ConvertCondition(condition);
            var tableName = EntityHelper.GetTableName(type);
            var builder = new StringBuilder();
            builder.Append("DELETE COUNT(*) ").Append(tableName).Append(" WHERE ").Append(condition);
            return builder.ToString();
        }

        // 生成查询表结构的语句
        public static string GenQueryTableStructureSql(Type type)
        {
            var tableName = EntityHelper.GetTableName(type);
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM ").Append(tableName)
                .Append(" WHERE 1<>1");
            return builder.ToString();
        }

        // 处理condition，将a=?转换为a=:a_类型, 防止出现在更新过程中与:a重复的情况
        private static string ConvertCondition(string condition)
        {
            if (!string.IsNullOrEmpty(condition) && condition.Contains("?"))
            {
                if (condition.ToLower().Contains("and"))
                {
                    var temp = condition.ToLower();
                    temp = temp.Replace("and", "$");
                    var conditions = temp.Split('$');
                    var builder = new StringBuilder();
                    foreach (var s in conditions)
                    {
                        var con = s.Split('=');
                        builder.Append(con[0].Trim()).Append("=").Append(":").Append(con[0].Trim()).Append("_").Append(" AND ");
                    }

                    int len = builder.Length;
                    condition = builder.Remove(len - 5, 5).ToString();
                }
                else
                {
                    var con = condition.Split('=');
                    condition = condition.Replace("?", ":" + con[0].Trim()+"_");
                }
            }
            else
            {
                condition = "1=1";
            }
            return condition;
        }
    }
}