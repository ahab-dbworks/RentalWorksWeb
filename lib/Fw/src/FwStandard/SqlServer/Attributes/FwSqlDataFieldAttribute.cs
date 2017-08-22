using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwSqlDataFieldAttribute : Attribute
    {
        public readonly string ColumnName;
        public readonly FwDataTypes ModelType;
        public readonly string SqlType;
        public readonly int MaxLength;
        public readonly int Precision;
        public readonly int Scale;
        public readonly bool IsPrimaryKey;
        public readonly bool IsVisible;
        public readonly bool Required;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwSqlDataFieldAttribute(string column = "", FwDataTypes modeltype = FwDataTypes.Text, string sqltype = "", int maxlength = 0, int precision = 0, int scale = 0, bool isPrimaryKey = false, bool isVisible = false, bool required = false)
        {
            ColumnName = column;
            ModelType = modeltype;
            SqlType = sqltype;
            MaxLength = maxlength;
            Precision = precision;
            Scale = scale;
            IsPrimaryKey = isPrimaryKey;
            IsVisible = isVisible;
            Required = required;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
