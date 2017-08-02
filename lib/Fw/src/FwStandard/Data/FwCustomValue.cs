namespace FwStandard.DataLayer
{

    public class FwCustomValue
    {
        public string FieldName;
        public string FieldValue;

        //------------------------------------------------------------------------------------
        public FwCustomValue(string fieldName, string fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
        }
        //------------------------------------------------------------------------------------
    }
}
