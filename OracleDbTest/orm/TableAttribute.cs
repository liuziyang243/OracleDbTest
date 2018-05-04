using System;
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: 属性类，用来标记POJO类，提供额外的映射支持
 */
namespace OracleDbTest.orm
{
    public class TableAttribute:Attribute
    {
        private string _tableName;

        public TableAttribute(string tableName)
        {
            this._tableName = tableName;
        }

        public string TableName
        {
            set { _tableName = value; }
            get { return _tableName; }
        }
    }
}