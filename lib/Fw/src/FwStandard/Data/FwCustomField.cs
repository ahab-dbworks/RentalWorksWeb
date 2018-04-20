namespace FwStandard.DataLayer
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
    }
}
