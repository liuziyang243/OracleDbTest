using System;
using System.Collections.Generic;
using System.Reflection;

namespace OracleDbTest.orm
{
    /**
     * 使用该类可以获取表名和属性-列名映射列表
     */
    public class EntityHelper
    {
        private static readonly Dictionary<Type, Dictionary<string, string>> EntityMap = new Dictionary<Type, Dictionary<string, string>>();

        private static readonly Dictionary<Type, string> TableMap = new Dictionary<Type, string>();

        //获取表名
        public static string GetTableName(Type type)
        {
            if (TableMap.ContainsKey(type))
            {
                return TableMap[type];
            }
            else
            {
                return GetTargetTableMap(type);
            }
        }

        // 获取对象指定名称的属性值
        public static object GetObjectPropertyValue(object obj, string propertyname)
        {
            var type = obj.GetType();
            return type.GetProperty(propertyname)?.GetValue(obj);
        }

        // 设置对象指定名称的属性值
        public static bool SetObjectPropertyValue(string fieldName, object value, object obj)
        {
            try
            {
                var type = obj.GetType();
                type.GetProperty(fieldName)?.SetValue(obj, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //获取属性值和列名对应关系表：key-属性名 value-列名
        public static Dictionary<string, string> GetAttributeColumnMap(Type type)
        {
            if (EntityMap.ContainsKey(type))
            {
                return EntityMap[type];
            }
            else
            {
                return GetTargetAttributeColumnMap(type);
            }
        }

        //获取数据库列名和属性名对应关系map，key-列名，value-属性名
        public static Dictionary<string, string> GetColumnAttributeMap(Type type)
        {
            Dictionary<string, string> temp;
            Dictionary<string, string> result = new Dictionary<string, string>();
            temp = EntityMap.ContainsKey(type) ? EntityMap[type] : GetTargetAttributeColumnMap(type);
            // 翻转key-value
            foreach (var entry in temp)
            {
                result.Add(entry.Value, entry.Key);
            }

            return result;
        }

        // 使用反射的方式获取表名，在首次从缓存查询的时候使用
        private static string GetTargetTableMap(Type type)
        {
            string tableName;
            // 判断是否有注解
            object[] objTableAttribute = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (objTableAttribute != null && objTableAttribute.Length != 0)
            {
                tableName = ((TableAttribute)objTableAttribute[0]).TableName;
            }
            else
            {
                // 没有注解则默认认为表名与类名相同
                tableName = type.Name.ToLower();
            }
            // 将反射后的结果缓存
            TableMap.Add(type, tableName);
            return tableName;
        }

        // 使用反射的方式获取属性-列名列表，在首次从缓存查询的时候使用
        private static Dictionary<string, string> GetTargetAttributeColumnMap(Type type)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, PropertyInfo> infoMap = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] infos = type.GetProperties();
            foreach (var info in infos)
            {
                infoMap.Add(info.Name, info);
                // 判断属性上是否有注解，如果有注解，则直接使用注解中的Column作为列名
                object[] objDataFieldAttribute = info.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (objDataFieldAttribute != null && objDataFieldAttribute.Length != 0)
                {
                    result.Add(info.Name, ((ColumnAttribute)objDataFieldAttribute[0]).ColumnName);
                }
                else
                {
                    //否则默认认为列名和属性名相同,且都转换为小写
                    result.Add(info.Name, info.Name.ToLower());
                }
            }
            // 将反射后的结果缓存起来
            EntityMap.Add(type, result);
            return result;
        }
    }
}