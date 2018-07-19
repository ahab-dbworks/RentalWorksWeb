using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityTreeBuilder
{
    public class SecurityTree
    {
        public SecurityTreeApplication RentalWorks { get; set; } = new SecurityTreeApplication();
        public SecurityTreeApplication TrakItWorks { get; set; } = new SecurityTreeApplication();
        public SecurityTreeApplication QuikScan { get; set; } = new SecurityTreeApplication();
    }

    public class SecurityTreeApplication
    {
        [JsonProperty(PropertyName ="M")]
        public List<SecurityTreeModule> Modules { get; set; } = new List<SecurityTreeModule>();
    }

    public class SecurityTreeModule
    {
        [JsonProperty(PropertyName ="N")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty(PropertyName ="C")]
        public string Category { get; set; } = string.Empty;
        [JsonProperty(PropertyName ="G")]
        public string Guid { get; set; } = string.Empty;
        [JsonProperty(PropertyName ="p")]
        public List<SecurityTreeModuleComponents> Components { get; set; } = new List<SecurityTreeModuleComponents>();
        [JsonProperty(PropertyName ="A")]
        public List<SecurityTreeModuleAction> Actions { get; set; } = new List<SecurityTreeModuleAction>();
    }

    public class SecurityTreeModuleComponents
    {
        [JsonProperty(PropertyName = "N")]
        public string Name { get; set; }
        //public string DataType { get; set; } = string.Empty;
        //public int MaxLength { get; set; } = 0;
    }

    public class SecurityTreeModuleAction
    {
        [JsonProperty(PropertyName = "N")]
        public string Name { get; set; }
    }

}
