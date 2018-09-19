using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FwStandard.AppManager
{
    public abstract class FwAppManager
    {
        [JsonIgnore]
        public List<FwAmController> Controllers { get; set; } = new List<FwAmController>();

        [JsonIgnore]
        public SortedDictionary<string, FwAmModule> ModulesById { get; set; } = new SortedDictionary<string, FwAmModule>();

        [JsonIgnore]
        public List<FwAmLogic> Logics { get; set; } = new List<FwAmLogic>();
        //public List<FwAmModule> Modules { get; set; } = new List<FwAmModule>();

        [JsonIgnore]
        public SortedDictionary<string, FwAmModule> Modules = new SortedDictionary<string, FwAmModule>();

        public SortedDictionary<string, FwAmProduct> Products = new SortedDictionary<string, FwAmProduct>();

        protected abstract string Unabbreviate(string value);
        protected abstract List<Type> GetLogicTypes();
        protected abstract List<Type> GetControllerTypes();

        protected abstract string GetDefaultEditions();

        public bool HasProductEdition(string editions, string product, string edition)
        {
            bool result = false;
            if (string.IsNullOrEmpty(editions))
            {
                editions = this.GetDefaultEditions();
            }
            string[] editionsArray = editions.Split(new char[] { ',' });
            for (int j = 0; j < editionsArray.Length; j++)
            {
                string[] rawProductParts = editionsArray[j].Split(new char[] { ',' });
                for (int k = 0; k < rawProductParts.Length; k++)
                {
                    string[] productKeyParts = rawProductParts[k].Split(new char[] { '|' });
                    string productKey = productKeyParts[0];
                    if (productKey == product)
                    {
                        List<string> editionKeys = new List<string>();
                        for (int l = 1; l <= productKeyParts.Length - 1; l++)
                        {
                            string editionKey = productKeyParts[l];
                            editionKeys.Add(editionKey);
                        }
                        if (editionKeys.Contains<string>(edition))
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public void LoadFromWebApi()
        {
            List<Type> logicTypes = GetLogicTypes()
                .Where(c => c.CustomAttributes.Where(d => d.AttributeType == typeof(FwLogicAttribute)).ToList().Count > 0).ToList();

            List<Type> controllerTypes = GetControllerTypes()
                .Where(c => c.CustomAttributes.Where(d => d.AttributeType == typeof(FwControllerAttribute)).ToList().Count > 0).ToList();

            // load the controllers
            Dictionary<string, List<Type>> namespaceControllers = new Dictionary<string, List<Type>>();
            for (int i = 0; i < controllerTypes.Count; i++)
            {
                Type controllerType = controllerTypes[i];
                if (!namespaceControllers.ContainsKey(controllerType.Namespace))
                {
                    namespaceControllers[controllerType.Namespace] = new List<Type>();
                }
                namespaceControllers[controllerType.Namespace].Add(controllerType);

                FwControllerAttribute controllerAttribute = (FwControllerAttribute)controllerType.GetCustomAttributes(typeof(FwControllerAttribute), false).First();
                if (controllerAttribute == null)
                {
                    throw new Exception($"Class: {controllerType.FullName} is missing [FwController] attribute.");
                }

                FwAmController controller = new FwAmController();
                controller.Id = controllerAttribute.Id;
                controller.ParentId = controllerAttribute.ParentId;
                string[] namespaceParts = controllerType.Namespace.Split(new char[] { '.' });
                controller.Category = controllerType.Namespace.Substring(0, controllerType.Namespace.LastIndexOf('.'));
                controller.ModuleName = namespaceParts[namespaceParts.Length - 1];

                var moduleFullName = $"{controller.Category}.{controller.ModuleName}";
                if (this.Modules.ContainsKey(moduleFullName))
                {
                    throw new Exception($"There can only be 1 controller with the namespace: {controllerType.Namespace}");
                }
                var module = new FwAmModule();
                module.Id = controller.Id;
                module.Name = controller.ModuleName;
                module.Category = controller.Category;
                module.Editions = controllerAttribute.Editions;
                module.Controller = controller;
                if (controller.ParentId == null)
                {
                    this.Modules[moduleFullName] = module;
                }
                this.ModulesById[module.Id] = module;
                this.Controllers.Add(controller);

                List<MethodInfo> methodInfos = controllerType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttribute(typeof(FwControllerMethodAttribute)) != null)
                    .OrderBy(o => o.Name)
                    .ToList<MethodInfo>();

                if (controller.ParentId == null)
                {
                    string editions = controllerAttribute.Editions;
                    if (string.IsNullOrEmpty(editions))
                    {
                        editions = this.GetDefaultEditions();
                    }
                    string[] editionsArray = editions.Split(new char[] { ',' });
                    for (int j = 0; j < editionsArray.Length; j++)
                    {
                        string[] rawProductParts = editionsArray[j].Split(new char[] { ',' });
                        for (int k = 0; k < rawProductParts.Length; k++)
                        {
                            string[] productKeyParts = rawProductParts[k].Split(new char[] { '|' });
                            string rawProductKey = productKeyParts[0];
                            string productKey = this.Unabbreviate(rawProductKey);
                            if (!this.Products.ContainsKey(productKey))
                            {
                                this.Products[productKey] = new FwAmProduct();
                            }

                            List<string> editionKeys = new List<string>();
                            for (int l = 1; l <= productKeyParts.Length - 1; l++)
                            {
                                string rawEditionKey = productKeyParts[l];
                                string editionKey = this.Unabbreviate(rawEditionKey);
                                if (!this.Products[productKey].Editions.ContainsKey(editionKey))
                                {
                                    this.Products[productKey].Editions[editionKey] = new FwAmProductEdition();
                                }
                                FwAmModule moduleCopy = JsonConvert.DeserializeObject<FwAmModule>(JsonConvert.SerializeObject(module));
                                
                                // Actions
                                
                                for (int m = 0; m < methodInfos.Count; m++)
                                {
                                    MethodInfo methodInfo = methodInfos[m];
                                    FwControllerMethodAttribute methodAttribute = (FwControllerMethodAttribute)methodInfo.GetCustomAttribute(typeof(FwControllerMethodAttribute), false);

                                    if (this.HasProductEdition(methodAttribute.Editions, rawProductKey, rawEditionKey))
                                    {
                                        FwAmControllerAction action = new FwAmControllerAction();
                                        action.Id = methodAttribute.Id;
                                        string actionName = methodInfo.Name;
                                        if (actionName.EndsWith("Async"))
                                        {
                                            actionName = actionName.Substring(0, actionName.Length - "Async".Length);
                                        }
                                        action.Name = actionName;
                                        moduleCopy.Controller.Actions.Add(action);
                                    }
                                }

                                this.Products[productKey].Editions[editionKey].Modules[$"{module.Category}.{module.Name}"] = moduleCopy;


                            }
                        }
                    }
                }

                
            }

            foreach (KeyValuePair<string, FwAmModule> item in this.ModulesById)
            {
                FwAmModule module = item.Value;
                if (module.Controller.ParentId != null)
                {
                    this.ModulesById[module.Controller.ParentId].SubModules[$"{module.Category}.{module.Name}"] = module;
                }
            }


            // load the logics
            for (int i = 0; i < logicTypes.Count; i++)
            {
                Type logicType = logicTypes[i];
                FwLogicAttribute logicAttribute = (FwLogicAttribute)logicType.GetCustomAttribute(typeof(FwLogicAttribute), false);
                if (logicAttribute == null)
                {
                    throw new Exception($"Class: {logicType.FullName} is missing [FwLogic] attribute.");
                }

                FwAmLogic logic = new FwAmLogic();
                logic.Id = logicAttribute.Id;
                string[] namespaceParts = logicType.Namespace.Split(new char[] { '.' });
                logic.Category = logicType.Namespace.Substring(0, logicType.Namespace.LastIndexOf('.'));
                logic.ModuleName = namespaceParts[namespaceParts.Length - 1];
                this.Logics.Add(logic);

                // Properties
                List<PropertyInfo> propertyInfos = logicType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).OrderBy(o => o.Name).ToList<PropertyInfo>();
                for (int j = 0; j < propertyInfos.Count; j++)
                {
                    PropertyInfo info = propertyInfos[j];
                    FwLogicPropertyAttribute propertyAttribute = (FwLogicPropertyAttribute)info.GetCustomAttribute(typeof(FwLogicPropertyAttribute), false);
                    if (propertyAttribute != null)
                    {
                        FwAmLogicProperty property = new FwAmLogicProperty();
                        property.Id = propertyAttribute.Id;
                        property.Name = propertyInfos[j].Name;
                        logic.Properties.Add(property);
                    }
                }

                var moduleFullName = $"{logic.Category}.{logic.ModuleName}";
                if (this.Modules.ContainsKey(moduleFullName))
                {
                    FwAmModule module = this.Modules[moduleFullName];
                    module.Controller.Logic = logic;
                }
            }
        }

        protected List<Type> LoadTypes(Assembly assembly, string rootNamespace, Type subClassOf)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(rootNamespace) && t.IsSubclassOf(subClassOf))
                .OrderBy(o => o.Name)
                .ToList<Type>();

            return types;
        }
    }

    public class FwAmProduct
    {
        //[JsonProperty(PropertyName ="e")]
        public SortedDictionary<string, FwAmProductEdition> Editions { get; set; } = new SortedDictionary<string, FwAmProductEdition>();
    }

    public class FwAmProductEdition
    {
        //[JsonProperty(PropertyName ="m")]
        public SortedDictionary<string, FwAmModule> Modules { get; set; } = new SortedDictionary<string, FwAmModule>();
    }

    public class FwAmController
    {
        //[JsonProperty(PropertyName ="n")]
        public string Category { get; set; }

        //[JsonProperty(PropertyName ="m")]
        public string ModuleName { get; set; }

        //[JsonProperty(PropertyName ="i")]
        public string Id { get; set; }
        
        //[JsonProperty(PropertyName ="p")]
        public string ParentId { get; set; }

        //[JsonProperty(PropertyName ="a")]
        public List<FwAmControllerAction> Actions { get; set; } = new List<FwAmControllerAction>();

        //[JsonProperty(PropertyName ="l")]
        public FwAmLogic Logic { get; set; } = null;

        [JsonIgnore]
        public string Editions { get; set; }
    }

    public class FwAmControllerAction
    {
        //[JsonProperty(PropertyName = "n")]
        public string Name { get; set; }

        //[JsonProperty(PropertyName = "i")]
        public string Id { get; set; }
    }

    public class FwAmLogic
    {
        //[JsonProperty(PropertyName ="i")]
        public string Id { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="c")]
        public string Category { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="m")]
        public string ModuleName { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="a")]
        public List<FwAmLogicProperty> Properties { get; set; } = new List<FwAmLogicProperty>();
    }

    public class FwAmLogicProperty
    {
        //[JsonProperty(PropertyName ="n")]
        public string Name { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="i")]
        public string Id { get; set; } = string.Empty;
    }

    public class FwAmModule
    {
        //[JsonProperty(PropertyName ="n")]
        public string Name { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="a")]
        public string Category { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="i")]
        public string Id { get; set; } = string.Empty;

        //[JsonProperty(PropertyName ="c")]
        public FwAmController Controller { get; set; } = new FwAmController();

        //[JsonProperty(PropertyName ="s")]
        public SortedDictionary<string, FwAmModule> SubModules { get; set; } = new SortedDictionary<string, FwAmModule>();

        [JsonIgnore]
        public string Editions { get; set; }

        //[JsonProperty(PropertyName ="P")]
        //public List<FwAmLogicProperty> Components { get; set; } = new List<FwAmLogicProperty>();
        //[JsonProperty(PropertyName ="A")]
        //public List<FwAmControllerAction> Actions { get; set; } = new List<FwAmControllerAction>();
    }

    public class FwAmModuleComponent
    {
        [JsonProperty(PropertyName = "n")]
        public string Name { get; set; }

        //public string DataType { get; set; } = string.Empty;
        //public int MaxLength { get; set; } = 0;
    }
}
