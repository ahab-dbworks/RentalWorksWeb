using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace FwStandard.Utilities
{
    public static class FwValidate
    {
        //----------------------------------------------------------------------------------
        public static bool IsPropertyDefined(ExpandoObject obj, string key)
        {
            bool isPropertyDefined;
            isPropertyDefined = true;
            if (isPropertyDefined)
            {
                isPropertyDefined = (obj != null);
            }
            if (isPropertyDefined)
            {
                isPropertyDefined = (((IDictionary<String, object>)obj).ContainsKey(key));
            }
            return isPropertyDefined;
        }
        //----------------------------------------------------------------------------------
        public static bool IsPropertyDefined(JObject obj, string key)
        {
            bool isPropertyDefined;
            isPropertyDefined = true;
            if (isPropertyDefined)
            {
                isPropertyDefined = (obj != null);
            }
            if (isPropertyDefined)
            {
                isPropertyDefined = obj.ContainsKey(key);
            }
            return isPropertyDefined;
        }
        //----------------------------------------------------------------------------------
        public static bool IsPropertyDefined(IDictionary<String, object> obj, string key)
        {
            bool isPropertyDefined;
            isPropertyDefined = true;
            if (isPropertyDefined)
            {
                isPropertyDefined = (obj != null);
            }
            if (isPropertyDefined)
            {
                isPropertyDefined = obj.ContainsKey(key);
            }
            return isPropertyDefined;
        }
        //----------------------------------------------------------------------------------
        public static void TestPropertyDefined(string contextDescription, ExpandoObject obj, string key)
        {
            bool isPropertyDefined;
            isPropertyDefined = FwValidate.IsPropertyDefined(obj, key);
            if (!FwValidate.IsPropertyDefined(obj, key))
            {
                throw new Exception(key + " is undefined. [" + contextDescription + "]");
            }
        }
        //----------------------------------------------------------------------------------
        public static void TestPropertyDefined(string contextDescription, IDictionary<String, object> obj, string key)
        {
            bool isPropertyDefined;
            isPropertyDefined = FwValidate.IsPropertyDefined(obj, key);
            if (!FwValidate.IsPropertyDefined(obj, key))
            {
                throw new Exception(key + " is undefined. [" + contextDescription + "]");
            }
        }
        //----------------------------------------------------------------------------------
        public static void TestIsNullOrEmpty(string contextDescription, string key, string value)
        {
            bool isNullOrEmpty;
            isNullOrEmpty = string.IsNullOrEmpty(value);
            if (isNullOrEmpty)
            {
                throw new Exception(key + " is required. [" + contextDescription + "]");
            }
        }
        //----------------------------------------------------------------------------------
        public static void TestIsNullOrWhitespace(string contextDescription, string key, string value)
        {
            bool isNullOrWhiteSpace;
            isNullOrWhiteSpace = string.IsNullOrWhiteSpace(value);
            if (isNullOrWhiteSpace)
            {
                throw new Exception(key + " is required. [" + contextDescription + "]");
            }
        }
        //----------------------------------------------------------------------------------
        public static void TestIsNumeric(string contextDescription, string key, object value)
        {
            bool isNumeric = false;
            if (value is Int16 || value is Int32 || value is Int64 || value is Decimal || value is Single || value is Double)
            {
                isNumeric = true;
            }
            if (!isNumeric)
            {
                throw new Exception(key + " is not a number. [" + contextDescription + "]");
            }
        }
        //----------------------------------------------------------------------------------
    }
}
