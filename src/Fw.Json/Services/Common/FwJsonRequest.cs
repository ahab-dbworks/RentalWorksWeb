using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fw.Json.Services.Common
{
    public class FwJsonRequest
    {
        string fieldValue;
        //----------------------------------------------------------------------------------------------------
        public FwJsonRequest(string value)
        {
            fieldValue = value;
        }
        //----------------------------------------------------------------------------------------------------
        public static FwJsonRequest GetField(string CONTEXT, IDictionary<String, object> fields, string fieldname)
        {
            FwJsonRequest fieldValue;
            String value;

            FwValidate.TestPropertyDefined(CONTEXT, fields, fieldname);
            value = (string)fields[fieldname];
            fieldValue = new FwJsonRequest(value);
            
            return fieldValue;
        }
        //----------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            String result;
            result = (!string.IsNullOrEmpty(fieldValue)) ? fieldValue : string.Empty;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public String ToNullableString()
        {
            String result;
            result = fieldValue;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Int32 ToInt32()
        {
            Int32 result;
            result = (!string.IsNullOrEmpty(fieldValue)) ? FwConvert.ToInt32(fieldValue) : 0;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Int32? ToNullableInt32()
        {
            Int32? result;
            result = (!string.IsNullOrEmpty(fieldValue)) ? (Int32?)FwConvert.ToInt32(fieldValue) : null;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Guid ToGuid()
        {
            Guid result;
            result = (!string.IsNullOrEmpty(fieldValue)) ? new Guid(fieldValue) : Guid.Empty;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Guid? ToNullableGuid()
        {
            Guid? result;
            result = (!string.IsNullOrEmpty(fieldValue)) ? (Guid?)new Guid(fieldValue) : null;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}
