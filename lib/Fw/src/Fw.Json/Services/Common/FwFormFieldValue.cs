using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fw.Json.Services.Common
{
    public class FwFormFieldValue
    {
        string fieldValue, fieldOriginalValue;
        //----------------------------------------------------------------------------------------------------
        public FwFormFieldValue(string value, string originalvalue)
        {
            fieldValue = value;
            fieldOriginalValue = originalvalue;
        }
        //----------------------------------------------------------------------------------------------------
        public override String ToString()
        {
            String result;
            result = (  (!string.IsNullOrEmpty(fieldValue)) && (!fieldValue.Equals(fieldOriginalValue))  ) ? fieldValue : string.Empty;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public String ToNullableString()
        {
            String result;
            result = (  (!fieldValue.Equals(fieldOriginalValue))  ) ? fieldValue : null;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Int32 ToInt32()
        {
            Int32 result;
            result = (  (!string.IsNullOrEmpty(fieldValue)) && (!fieldValue.Equals(fieldOriginalValue))  ) ? FwConvert.ToInt32(fieldValue) : 0;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Int32? ToNullableInt32()
        {
            Int32? result;
            result = (  (!string.IsNullOrEmpty(fieldValue)) && (!fieldValue.Equals(fieldOriginalValue))  ) ? (Int32?)FwConvert.ToInt32(fieldValue) : null;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Guid ToGuid()
        {
            Guid result;
            result = (  (!string.IsNullOrEmpty(fieldValue)) && (!fieldValue.Equals(fieldOriginalValue))  ) ? new Guid(fieldValue) : Guid.Empty;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public Guid? ToNullableGuid()
        {
            Guid? result;
            result = (  (!string.IsNullOrEmpty(fieldValue)) && (!fieldValue.Equals(fieldOriginalValue))  ) ? (Guid?)new Guid(fieldValue) : null;
            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}
