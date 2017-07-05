using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwSqlDataFieldAttribute : Attribute
    {
        public readonly string ColumnName;
        public readonly FwDataTypes DataType;  // need to distinguish between "char" and "varchar", etc.
        public readonly int Length;
        public readonly int Precision;
        public readonly int Scale;
        public readonly bool IsPrimaryKey;
        public readonly bool IsVisible;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwSqlDataFieldAttribute(string columnName = "", FwDataTypes dataType = FwDataTypes.Text, int length = 0, int precision = 0, int scale = 0, bool isPrimaryKey = false, bool isVisible = false)
        {
            ColumnName = columnName;
            DataType = dataType;
            Length = length;
            Precision = precision;
            Scale = scale;
            IsPrimaryKey = isPrimaryKey;
            IsVisible = isVisible;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
