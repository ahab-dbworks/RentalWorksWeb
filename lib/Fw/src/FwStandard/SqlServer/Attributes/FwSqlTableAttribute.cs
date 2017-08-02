using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class FwSqlTableAttribute : Attribute
    {
        public readonly string TableName;
        //public readonly string DataType;
        public readonly bool HasInsert;
        public readonly bool HasUpdate;
        public readonly bool HasDelete;

        public FwSqlTableAttribute(string tableName, bool hasInsert = true, bool hasUpdate = true, bool hasDelete = true)
        {
            TableName = tableName;
            HasInsert = hasInsert;
            HasUpdate = hasUpdate;
            HasDelete = hasDelete;
        }
    }
}
