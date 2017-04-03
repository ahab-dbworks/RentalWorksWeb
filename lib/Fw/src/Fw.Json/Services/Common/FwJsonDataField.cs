using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fw.Json.Services
{
    public class FwJsonDataField
    {
        //---------------------------------------------------------------------------------------------
        public string       dataField    {get;set;}
        public dynamic      value        {get;set;}
        public dynamic      text         {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwJsonDataField()
        {
            this.dataField   = string.Empty;
            this.value       = string.Empty;
            this.text        = string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        public FwJsonDataField(string dataField, dynamic value, dynamic text)
        {
            this.dataField   = dataField;
            this.value       = value;
            this.text        = text;
        }
        //---------------------------------------------------------------------------------------------
    }
}
