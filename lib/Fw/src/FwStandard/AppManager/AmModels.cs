using Newtonsoft.Json;
using System.Collections.Generic;

namespace FwStandard.AppManager
{
    public class FwAmProduct
    {
        public SortedDictionary<string, FwAmProductEdition> Editions { get; set; } = new SortedDictionary<string, FwAmProductEdition>();
    }

    public class FwAmProductEdition
    {
        public SortedDictionary<string, FwAmModule> Modules { get; set; } = new SortedDictionary<string, FwAmModule>();
    }

    public class FwAmController
    {
        public string Category { get; set; }
        public string ModuleName { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public List<FwAmControllerAction> Actions { get; set; } = new List<FwAmControllerAction>();
        public FwAmLogic Logic { get; set; } = null;

        [JsonIgnore]
        public string Editions { get; set; }
    }

    public class FwAmControllerAction
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public FwControllerActionTypes ActionType { get; set; }
        public string ParentId { get; set; }
        public string OptionName { get; set; }
    }

    public class FwAmLogic
    {
        public string Id { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ModuleName { get; set; } = string.Empty;
        public List<FwAmLogicProperty> Properties { get; set; } = new List<FwAmLogicProperty>();
    }

    public class FwAmLogicProperty
    {
        public string Name { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }

    public class FwAmModule
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public FwAmController Controller { get; set; } = new FwAmController();
        public SortedDictionary<string, FwAmModule> SubModules { get; set; } = new SortedDictionary<string, FwAmModule>();
        public Dictionary<string, FwControlAttribute> Controls { get; set; } = new Dictionary<string, FwControlAttribute>();
        public Dictionary<string, FwOptionsGroupAttribute> OptionsGroups { get; set; } = new Dictionary<string, FwOptionsGroupAttribute>();

        [JsonIgnore]
        public string Editions { get; set; }

    }

    public class FwAmModuleComponent
    {
        public string Name { get; set; }
    }
}
