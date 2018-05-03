using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;

namespace OracleDbTest.orm
{
    /**
     * 处理sql中的参数
     */
    public class ParameterHandler
    {
        // 设置sql语句参数, parms中key为sql语句参数占位符，value为实际的参数值
        public static void SetParameters(OracleCommand cmd, Dictionary<string, object> parms)
        {
            foreach (var o in parms)
            {
                cmd.Parameters.Add(new OracleParameter(o.Key, o.Value));
            }
        }

        // 将实体类属性转换为占位符和参数值配对的形式
        public static Dictionary<string, object> GetConditionParams(object obj)
        {
            Type type = obj.GetType();
            Dictionary<string, object> result = new Dictionary<string, object>();
            Dictionary<string, string> fieldMap = EntityHelper.GetAttributeColumnMap(type);
            foreach (var field in fieldMap)
            {
                var propertyValue = EntityHelper.GetObjectPropertyValue(obj, field.Key);
                result.Add(":" + field.Value, propertyValue);
            }

            return result;
        }

        // 将condition中的？转换为匹配的占位符并与实际参数配对
        public static Dictionary<string, object> GetConditionParams(string condition, params object[] parmObjects)
        {
            var result = new Dictionary<string, object>();
            var placeholders = new List<string>();
            if (!string.IsNullOrEmpty(condition) && condition.Contains("?"))
            {
                // 判断占位符和参数的个数是否相符，如果不符则直接抛出异常
                var number = condition.Count(c => c == '?');
                if (number != parmObjects.Length)
                {
                    throw new Exception("Placeholder ? can't match parameter! The number is not equal.");
                }

                // 首先处理是否包含多个用and连接的独立条件
                if (condition.ToLower().Contains("and"))
                {
                    var temp = condition.ToLower();
                    temp = temp.Replace("and", "$");
                    var conditions = temp.Split('$');
                    foreach (var s in conditions)
                    {
                        var con = s.Split('=');
                        placeholders.Add(":" + con[0].Trim() + "_");
                    }
                }
                // 处理单个条件的情况
                else
                {
                    var con = condition.Split('=');
                    placeholders.Add(":" + con[0].Trim() + "_");
                }

                // 将占位符与参数进行配对
                var placeholderArray = placeholders.ToArray();
                for (var i = 0; i < parmObjects.Length; i++)
                {
                    result.Add(placeholderArray[i], parmObjects[i]);
                }
            }
            else
            {
                //todo:log this case
            }

            return result;
        }
    }
}