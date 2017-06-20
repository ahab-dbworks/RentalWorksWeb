using System.Collections.Generic;
using System.Dynamic;

namespace FwCore.Services.Common
{
    public class FwJsonFormTable
    {
        public dynamic fields {get;set;}
        public dynamic misc {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwJsonFormTable()
        {
            this.fields = new ExpandoObject();
            this.misc = new ExpandoObject();
        }
        //---------------------------------------------------------------------------------------------
        public void AddField(FwJsonDataField field)
        {
            IDictionary<string,object> fieldsHash;

            fieldsHash = (IDictionary<string, object>)fields;
            fieldsHash.Add(field.dataField, field);
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataField AddField(string dataField, dynamic value, dynamic text)
        {
            FwJsonDataField field;
            IDictionary<string,object> fieldsHash;

            field = new FwJsonDataField(dataField, value, text);
            fieldsHash = (IDictionary<string, object>)fields;
            fieldsHash.Add(field.dataField, field);

            return field;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataField AddField(string dataField, dynamic value)
        {
            return AddField(dataField, value, string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataField GetField(string dataField)
        {
            FwJsonDataField field;
            IDictionary<string,object> fieldsHash;

            fieldsHash = (IDictionary<string, object>)fields;
            field = (FwJsonDataField)fieldsHash[dataField];

            return field;
        }
        //---------------------------------------------------------------------------------------------
        public List<FwJsonDataField> GetFieldList()
        {
            List<FwJsonDataField> fieldList;
            IDictionary<string,object> fieldsHash;

            fieldsHash = (IDictionary<string, object>)fields;
            fieldList = new List<FwJsonDataField>((ICollection<FwJsonDataField>)fieldsHash.Values);

            return fieldList;
        }
        //---------------------------------------------------------------------------------------------
    }
}
