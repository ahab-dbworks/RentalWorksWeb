using FwStandard.SqlServer;

namespace FwStandard.Data
{
    public class FwCustomField
    {
        public string ModuleName;
        public string FieldName;
        public string CustomTableName;
        public string CustomFieldName;
        public string FieldType;

        public FwCustomField() { }
        //------------------------------------------------------------------------------------
        public FwCustomField(string moduleName, string fieldName, string customTableName, string customFieldName, string fieldType)
        {
            this.ModuleName = moduleName;
            this.FieldName = fieldName;
            this.CustomTableName = customTableName;
            this.CustomFieldName = customFieldName;
            this.FieldType = fieldType;
        }
        //------------------------------------------------------------------------------------
        public FwDataTypes FwDataType {
            get
            {
                FwDataTypes t = FwDataTypes.Text;
                if (FieldType.Equals("Text"))
                {
                    t = FwDataTypes.Text;
                }
                else if (FieldType.Equals("Integer"))
                {
                    t = FwDataTypes.Integer;
                }
                else if (FieldType.Equals("Float"))
                {
                    t = FwDataTypes.Decimal;
                }
                else if (FieldType.Equals("Date"))
                {
                    t = FwDataTypes.Date;
                }
                else if (FieldType.Equals("True/False"))
                {
                    t = FwDataTypes.Boolean;
                }
                return t;
            }
        }
        //------------------------------------------------------------------------------------
    }
}
