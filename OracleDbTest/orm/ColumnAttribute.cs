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
        private string _columnName;
        private string _columnType;

        public ColumnAttribute(string fieldname, string fieldtype)
        {
            this._columnName = fieldname;
            this._columnType = fieldtype;
        }

        public string ColumnName
        {
            get { return this._columnName; }
            set { this._columnName = value; }
        }

        public string ColumnType
        {
            get { return this._columnType; }
            set { this._columnType = value; }
        }
    }
}