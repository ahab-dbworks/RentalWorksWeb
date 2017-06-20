using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace FwCore.ValueTypes
{
    //---------------------------------------------------------------------------------------------
    [JsonObject(MemberSerialization.OptIn)]
    public class FwGroupSecurityNode
    {
        //---------------------------------------------------------------------------------------------
        [JsonProperty("id")]
        public string Id {get;set;}
        //---------------------------------------------------------------------------------------------
        [JsonProperty("visible")]
        public string Visible {get;set;}
        //---------------------------------------------------------------------------------------------
        [JsonProperty("editable")]
        public string Editable {get;set;}
        //---------------------------------------------------------------------------------------------
        public FwGroupSecurityNode()
        {
            this.Id       = string.Empty;
            this.Visible  = "T";
            this.Editable = "T";
        }
        //---------------------------------------------------------------------------------------------
    }
}
