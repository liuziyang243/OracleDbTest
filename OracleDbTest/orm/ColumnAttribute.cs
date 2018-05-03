using System;

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