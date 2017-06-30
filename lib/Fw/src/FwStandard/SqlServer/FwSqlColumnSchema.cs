using System;

namespace FwStandard.SqlServer
{
    public class FwSqlColumnSchema
    {
        public string Caption {get;set;}
        public string Column {get;set;}
        public string DataType {get;set;}
        public bool   IsNullable {get;set;}
        public int    CharacterMaximumLength {get;set;}
        public int    NumericPrecision {get;set;}
        public int    NumericScale {get;set;}
        public bool   IsIdentity {get;set;}
        public bool   ReadOnly {get;set;}
        //-------------------------------------------------------------------------
        public FwSqlColumnSchema()
        {
            Caption                = string.Empty;
            Column                 = string.Empty;
            IsNullable             = false;
            CharacterMaximumLength = 0;
            NumericPrecision       = 0;
            NumericScale           = 0;
            IsIdentity             = false;
            DataType               = string.Empty;
            ReadOnly               = false;
        }
        //-------------------------------------------------------------------------
        public FwSqlColumnSchema(FwApplicationSchema.Column appcolumnschema)
        {
            Caption                = appcolumnschema.Caption;
            Column                 = appcolumnschema.ColumnName;
            IsNullable             = appcolumnschema.SqlIsNullable;
            CharacterMaximumLength = appcolumnschema.SqlCharacterMaximumLength;
            NumericPrecision       = appcolumnschema.SqlNumericPrecision;
            NumericScale           = appcolumnschema.SqlNumericScale;
            DataType               = appcolumnschema.SqlDataType;
            IsIdentity             = appcolumnschema.SqlIsIdentity;
            ReadOnly               = appcolumnschema.ReadOnly;
        }
        //-------------------------------------------------------------------------
        public object SQLFormat(string value)
        {
            object columnValue = value;

            switch (DataType)
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                    columnValue = value.Trim();
                    break;
                case "uniqueidentifier":
                case "tinyint":
                case "smallint":
                case "int":
                case "real":
                case "money":
                case "float":
                case "bit":
                case "decimal":
                case "numeric":
                case "smallmoney":
                case "bigint":
                    if (value.Trim().Replace(".", "") == string.Empty)
                    {
                    columnValue = "0";
                    }
                    break;
                case "timestamp":
                case "smalldatetime":
                case "datetime":
                    if (FwFunc.IsEmptyDate(value.Trim()))
                    {
                    columnValue = DBNull.Value;
                    }
                    break;
                case "image":   //not complete - need to stream data
                case "text":
                case "sql_Variant":
                case "ntext":
                case "varbinary": 
                case "binary":
                case "xml":
                case "sysname":
                case "unknown":
                default:
                    columnValue = value.Trim();
                    break;

            }
            return columnValue;
        }
        //---------------------------------------------------------------------------------------------
        public string SetValue(FwSqlCommand qry)
        {
            string columnValue = string.Empty;

            switch (DataType)
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "uniqueidentifier":
                    columnValue = qry.GetField(Column).ToString().Trim();
                    break;
                case "tinyint":
                case "smallint":
                case "int":
                case "real":
                case "money":
                    columnValue = Math.Round(qry.GetField(Column).ToDecimal(), 2).ToString();
                    break;
                case "float":
                case "bit":
                case "decimal":
                case "numeric":
                case "smallmoney":
                case "bigint":
                    columnValue = qry.GetField(Column).ToString().Trim();
                    break;
                case "timestamp":
                case "smalldatetime":
                case "datetime":
                    columnValue = qry.GetField(Column).ToString().Trim();
                    break;
                case "image":   //not complete - need to stream data
                case "text":
                case "sql_Variant":
                case "ntext":
                case "varbinary":
                case "binary":
                case "xml":
                case "sysname":
                case "unknown":
                    columnValue = qry.GetField(Column).ToString().Trim();
                    break;
            }
            return columnValue;
        }
        //-------------------------------------------------------------------------
    }
}
