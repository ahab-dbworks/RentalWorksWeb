using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.AppManager
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FwAmSqlGroupNode
    {
        //---------------------------------------------------------------------------------------------
        [JsonProperty("id")]
        public string id { get; set; } = null;
        //---------------------------------------------------------------------------------------------
        [JsonProperty("properties")]
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        //---------------------------------------------------------------------------------------------
    }
}
