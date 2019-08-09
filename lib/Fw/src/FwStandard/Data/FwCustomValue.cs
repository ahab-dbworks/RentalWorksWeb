namespace FwStandard.Data
{

    public class FwCustomValue
    {
        public string FieldName;
        public string FieldValue;
        public string FieldType;

        //------------------------------------------------------------------------------------
        public FwCustomValue(string fieldName, string fieldValue, string fieldType)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
            this.FieldType = fieldType;
        }
        //------------------------------------------------------------------------------------
    }
}
