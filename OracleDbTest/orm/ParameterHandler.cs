using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: 参数处理类，将插入、更新属性和条件中的参数进行封装，形成OracleParameter
 * 属性的参数占位符使用 :[属性名] 的形式，为了避免条件中出现相同的占位符，条件中的参数
 * 占位符使用 :[属性名]_ 的形式。
 */
namespace OracleDbTest.orm
{
    /**
     * 处理sql中的参数
     */
    public static class ParameterHandler
    {
        // 设置sql语句参数, parms中key为sql语句参数占位符，value为实际的参数值
        public static void SetParameters(OracleCommand cmd, Dictionary<string, object> paramDictionary)
        {
            if (paramDictionary!=null && paramDictionary.Any())
            {
                foreach (var o in paramDictionary)
                {
                    cmd.Parameters.Add(new OracleParameter(o.Key, o.Value));
                }
            }
        }

        // 将实体类属性转换为占位符和参数值配对的形式
        public static Dictionary<string, object> GetConditionParams(object obj)
        {
            var type = obj.GetType();
            var result = new Dictionary<string, object>();
            var fieldMap = EntityHelper.GetAttributeColumnMap(type);
            foreach (var field in fieldMap)
            {
                var propertyValue = EntityHelper.GetObjectPropertyValue(obj, field.Key);
                // oracle没有bool类型的值，默认采用number(1)存储，1代表true，0代表false
                if (propertyValue is bool)
                {
                    var value = (bool) propertyValue;
                    result.Add(":" + field.Value, value ? 1 : 0);
                }
                else
                {
                    result.Add(":" + field.Value, propertyValue);
                }
            }

            return result;
        }

        // 将map对象转换为占位符和参数值配对的形式
        public static Dictionary<string, object> GetConditionParams(Dictionary<string, object> columnDatMap)
        {
            var result = new Dictionary<string, object>();
            foreach (var o in columnDatMap)
            {
                var propertyValue = o.Value;
                // oracle没有bool类型的值，默认采用number(1)存储，1代表true，0代表false
                if (propertyValue is bool)
                {
                    var value = (bool)propertyValue;
                    result.Add(":" + o.Key, value ? 1 : 0);
                }
                else
                {
                    result.Add(":" + o.Key, propertyValue);
                }
            }

            return result;
        }

        // 将condition中的？转换为匹配的占位符并与实际参数配对
        public static Dictionary<string, object> GetConditionParams(string condition, params object[] paramObjects)
        {
            if (string.IsNullOrEmpty(condition))
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (paramObjects == null || paramObjects.Length<1)
            {
                throw new ArgumentNullException(nameof(paramObjects));
            }

            // 判断占位符和参数的个数是否相符，如果不符则直接抛出异常
            var number = condition.Count(c => c == '?');
            if (number != paramObjects.Length)
            {
                throw new ArgumentException("Placeholder ? can't match parameter! The number is not equal.");
            }

            var result = new Dictionary<string, object>();
            var placeholders = new List<string>();

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
            for (var i = 0; i < paramObjects.Length; i++)
            {
                // oracle没有bool类型的值，默认采用number(1)存储，1代表true，0代表false
                if (paramObjects[i] is bool)
                {
                    var value = (bool) paramObjects[i];
                    result.Add(placeholderArray[i], value ? 1 : 0);
                }
                else
                {
                    result.Add(placeholderArray[i], paramObjects[i]);
                }
            }

            return result;
        }
    }
}