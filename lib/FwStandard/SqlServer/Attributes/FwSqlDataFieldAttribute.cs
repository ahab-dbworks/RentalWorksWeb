using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwSqlDataFieldAttribute : Attribute
    {
        public readonly string ColumnName;
        public readonly FwDataTypes DataType;
        public readonly bool IsPrimaryKey;
        public readonly bool IsVisible;
        public FwSqlDataFieldAttribute(string columnName, FwDataTypes dataType, bool isPrimaryKey = false, bool isVisible = true)
        {
            ColumnName = columnName;
            DataType = dataType;
            IsPrimaryKey = isPrimaryKey;
            IsVisible = isVisible;
        }   
    }
}
