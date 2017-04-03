using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Fw.Json.Services.Common
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
