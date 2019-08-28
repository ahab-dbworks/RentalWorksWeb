using System;

namespace FwStandard.SqlServer.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwSqlDataFieldAttribute : Attribute
    {
        public readonly string ColumnName;
        public readonly FwDataTypes ModelType;
        public readonly string SqlType;
        public readonly bool Identity;
        public readonly int MaxLength;
        public readonly int Precision;
        public readonly int Scale;
        public readonly bool IsPrimaryKey;
        public readonly bool IsPrimaryKeyOptional;
        public readonly bool IsVisible;
        public readonly bool Required;
        public readonly string CalculatedColumnSql;
        public readonly bool HideInSummary;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwSqlDataFieldAttribute(string column = "", FwDataTypes modeltype = FwDataTypes.Text, string sqltype = "", bool identity = false, int maxlength = 0, int precision = 0, int scale = 0, bool isPrimaryKey = false, bool isPrimaryKeyOptional = false, bool isVisible = true, bool required = false, string calculatedColumnSql = "", bool hideInSummary = false)
        {
            ColumnName           = column;
            ModelType            = modeltype;
            SqlType              = sqltype;
            Identity             = identity;
            MaxLength            = maxlength;
            Precision            = precision;
            Scale                = scale;
            IsPrimaryKey         = isPrimaryKey;
            IsPrimaryKeyOptional = isPrimaryKeyOptional;
            IsVisible            = isVisible;
            Required             = required;
            CalculatedColumnSql  = calculatedColumnSql;
            HideInSummary        = hideInSummary;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
