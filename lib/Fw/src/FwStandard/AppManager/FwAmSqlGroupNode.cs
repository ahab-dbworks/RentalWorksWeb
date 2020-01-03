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
        [JsonProperty("id", Order = 1)]
        public string id { get; set; } = null;
        //---------------------------------------------------------------------------------------------
        [JsonProperty("properties", Order = 2)]
        public Dictionary<string, string> properties = new Dictionary<string, string>();
        //---------------------------------------------------------------------------------------------
    }
}
