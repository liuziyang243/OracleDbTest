using System;

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