using System;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.04.27
 * @document: 属性类，用来标记POJO类的属性，提供额外的映射支持
 */
namespace OracleDbTest.orm
{
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string fieldname, string fieldtype)
        {
            this.ColumnName = fieldname;
            this.ColumnType = fieldtype;
        }

        public string ColumnName { get; }

        public string ColumnType { get; }
    }
}