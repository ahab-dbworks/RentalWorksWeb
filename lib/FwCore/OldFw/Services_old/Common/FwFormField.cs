using System;
using System.Collections.Generic;

namespace FwCore.Services.Common
{
    public class FwFormField
    {
        //----------------------------------------------------------------------------------------------------
        public static FwFormFieldValue GetValue(string CONTEXT, IDictionary<String, object> fields, string fieldname)
        {
            FwFormFieldValue fieldValue;
            String originalvalue, value;
            IDictionary<String, object> field;

            field = (IDictionary<String, object>)fields[fieldname];
            if (field.ContainsKey("originalvalue"))
            {
                originalvalue = (string)field["originalvalue"];
            }
            else
            {
                originalvalue = null;
            }
            value            = (string)field["value"];
            fieldValue = new FwFormFieldValue(value, originalvalue);
            
            return fieldValue;
        }
        //----------------------------------------------------------------------------------------------------
    }
}
