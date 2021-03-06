using System;
using Fw.Json.Utilities;

namespace Fw.Json.SqlServer
{
    public class FwSqlColumn
    {
        private string fValue = string.Empty;
        private string fOriginalValue = string.Empty;
        //---------------------------------------------------------------------------------------------
        public string Column {get;set;}
        public string Value { get { return fValue; } set { fValue = value.TrimEnd(); } }
        public string OriginalValue { get { return fOriginalValue; } set { fOriginalValue = value.TrimEnd(); } }
        public bool   UniqueIdentifier = false;
        public FwSqlColumnSchema ColumnSchema {get; internal set;}
        public FwSqlTable Table;
        public bool EnableValidation {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwSqlColumn(string column)
        {
            this.EnableValidation = true;
            this.Column           = column;
            this.ColumnSchema     = new FwSqlColumnSchema();
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlColumn(FwSqlTable table, string column) : this(column)
        {
            this.Table = table;
        }
        //---------------------------------------------------------------------------------------------
        public FwSqlColumn(string column, bool isUniqueIdentifier) : this(column)
        {
            this.UniqueIdentifier = isUniqueIdentifier;
        }
        //---------------------------------------------------------------------------------------------
        //public void Clear()
        //{
        //    Value         = string.Empty;
        //    OriginalValue = string.Empty;
        //}
        //---------------------------------------------------------------------------------------------
        public object SQLFormat()
        {
            return this.ColumnSchema.SQLFormat(Value);
        }
        //---------------------------------------------------------------------------------------------
        public string SetValue(FwSqlCommand qry)
        {
            this.Value         = this.ColumnSchema.SetValue(qry);
            this.OriginalValue = this.Value;
            return Value;
        }
        //---------------------------------------------------------------------------------------------
        public bool Modified
        {
            get
            {
                return true;
            }
        }
        //public bool Modified
        //{
        //    get 
        //    {
        //        bool modified = true;

        //        if ((ColumnSchema.DataType == "datetime") ||
        //            (ColumnSchema.DataType == "smalldatetime"))
        //        {
        //            modified = (FwConvert.ToDateTime(Value) != FwConvert.ToDateTime(OriginalValue));
        //        }
        //        else
        //        {
        //            modified = (Value.Trim() != OriginalValue.Trim());
        //        }
        //        return modified;
        //    }
        //}
        //---------------------------------------------------------------------------------------------
        //public bool ToBoolean()
        //{
        //    return FwConvert.ToBoolean(Value);
        //}
        ////---------------------------------------------------------------------------------------------
        //public int ToInt32()
        //{
        //    return FwConvert.ToInt32(Value);
        //}
        ////---------------------------------------------------------------------------------------------
        //public override string ToString()
        //{
        //    return Value;
        //}
        //---------------------------------------------------------------------------------------------
        public FwDatabaseField FwDatabaseField
        {
            get 
            {
                return new FwDatabaseField(this.Value);
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
