using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;

/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: 这个类用来处理从oracle数据库查询出来的数据进行处理，通过一系列变换，
 * 将OracleDataReader读取出来的列表信息转换为实例对象或者Map数据列表
 */
namespace OracleDbTest.orm
{
    public class ResultHandler
    {
        // 将oracleReader转换为一组对象列表
        public static List<T> GenerateObjFromTable<T>(OracleDataReader reader)
        {
            var type = typeof(T);
            List<T> resultList = new List<T>();
            Dictionary<string, string> fieldMap = EntityHelper.GetColumnAttributeMap(type);
            Dictionary<string, int> columnNameIndexMap = GetNameIndexMapFromDb(reader);
            //遍历reader，从reader中获取指定列的属性值，并将属性值赋值给对象
            while (reader.Read())
            {
                //利用反射创建对象
                var instance = (T) Activator.CreateInstance(type);
                //读取列值并赋值给属性
                foreach (var entry in fieldMap)
                {
                    try
                    {
                        var index = columnNameIndexMap[entry.Key.ToLower()];
                        var value = GetValueByType(entry.Value, index, type, reader);
                        EntityHelper.SetObjectPropertyValue(entry.Value, value, instance);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                resultList.Add(instance);
            }

            return resultList;
        }

        // 将从数据库读取出的数据每一行转换为一个map，key为列名，value为值，将读取结果封装成一个List
        public static List<Dictionary<string, object>> GenerateResultMapFromTable(OracleDataReader reader)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, int> columnNameIndexMap = GetNameIndexMapFromDb(reader);
            while (reader.Read())
            {
                Dictionary<string, object> temp = new Dictionary<string, object>();
                foreach (var key in columnNameIndexMap.Keys)
                {
                    temp.Add(key.Trim().ToLower(), reader[key]);
                }

                result.Add(temp);
            }

            return result;
        }

        // 读取一列数据，将数据转换为一个List返回
        public static List<T> GenerateColumnObjFromTable<T>(OracleDataReader reader)
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                result.Add((T) reader[0]);
            }

            return result;
        }


        // 获取数据库列名和序号的对应关系
        private static Dictionary<string, int> GetNameIndexMapFromDb(OracleDataReader reader)
        {
            var result = new Dictionary<string, int>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i).Trim().ToLower();
                result.Add(columnName, i);
            }

            return result;
        }

        // 根据属性类型获取对应的值
        private static object GetValueByType(string propertyName, int index, Type type, OracleDataReader reader)
        {
            // 根据对象属性类型读取相应数据库列中的数据
            var propertyType = type.GetProperty(propertyName)?.PropertyType;
            object value;
            if (propertyType == typeof(short))
            {
                value = reader.GetInt16(index);
            }
            else if (propertyType == typeof(int))
            {
                value = reader.GetInt32(index);
            }
            else if (propertyType == typeof(long))
            {
                value = reader.GetInt64(index);
            }
            else if (propertyType == typeof(float))
            {
                value = reader.GetFloat(index);
            }
            else if (propertyType == typeof(double))
            {
                value = reader.GetDouble(index);
            }
            else if (propertyType == typeof(bool))
            {
                // oracle没有bool类型的值，默认采用number(1)存储，1代表true，0代表false
                value = reader.GetInt32(index) == 1;
            }
            else if (propertyType == typeof(decimal))
            {
                value = reader.GetDecimal(index);
            }
            else if (propertyType == typeof(char))
            {
                // 直接使用GetChar会报错，提示无此方法，需要使用GetString获取
                value = reader.GetString(index)[0];
            }
            else if (propertyType == typeof(DateTime))
            {
                value = reader.GetDateTime(index);
            }
            else if (propertyType == typeof(string))
            {
                value = reader.GetString(index);
            }
            else
            {
                value = reader[index];
            }

            return value;
        }
    }
}