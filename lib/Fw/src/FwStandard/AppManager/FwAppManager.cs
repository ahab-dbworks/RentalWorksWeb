using FwStandard.Models;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FwStandard.AppManager
{
    public abstract class FwSecurityTree
    {

    }

    public abstract class FwAppManager : FwSecurityTree
    {
        private SqlServerConfig _sqlServerOptions;
        //--------------------------------------------------------------------------------------------- 
        public Dictionary<string, FwSqlData.ApplicationOption> ApplicationOptions { get; set; }
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        public static FwAppManager Tree { get; set; } = null;
        //--------------------------------------------------------------------------------------------- 
        public Dictionary<string, FwAmGroupTree> GroupTrees { get; private set; } = new Dictionary<string, FwAmGroupTree>();
        //--------------------------------------------------------------------------------------------- 
        [JsonProperty("applications")]
        public FwAmSecurityTreeNode System { get; set; }
        //--------------------------------------------------------------------------------------------- 
        public Dictionary<string, FwAmSecurityTreeNode> Nodes { get; set; } = new Dictionary<string, FwAmSecurityTreeNode>();
        public Dictionary<string, FwAmSecurityTreeNode> ExcludedNodes { get; set; } = new Dictionary<string, FwAmSecurityTreeNode>();
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        public static string CurrentProduct { get; set; } = null;
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        public static string CurrentProductEdition { get; set; } = null;
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        private List<FwAmController> Controllers { get; set; } = new List<FwAmController>();
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        private SortedDictionary<string, FwAmModule> ModulesById { get; set; } = new SortedDictionary<string, FwAmModule>();
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        private List<FwAmLogic> Logics { get; set; } = new List<FwAmLogic>();
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        private SortedDictionary<string, FwAmModule> Modules = new SortedDictionary<string, FwAmModule>();
        //--------------------------------------------------------------------------------------------- 
        [JsonIgnore]
        private SortedDictionary<string, FwAmProduct> Products = new SortedDictionary<string, FwAmProduct>();
        //--------------------------------------------------------------------------------------------- 
        //[JsonIgnore]
        //public SortedDictionary<string, FwAmControllerAction> ControllerActionPolicies = new SortedDictionary<string, FwAmControllerAction>();
        ////--------------------------------------------------------------------------------------------- 
        //[JsonIgnore]
        //public SortedDictionary<string, FwAmLogicProperty> LogicPropertyPolicies = new SortedDictionary<string, FwAmLogicProperty>();
        //[JsonIgnore]
        //private Dictionary<string, Type> BranchTypes { get; set; }
        //--------------------------------------------------------------------------------------------- 
        public FwAppManager(SqlServerConfig sqlServerOptions)
        {
            _sqlServerOptions = sqlServerOptions;
            Task.Run(async () =>
            {
                await this.InitAsync();
            }).Wait();
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task InitAsync()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_sqlServerOptions.ConnectionString))
            {
                this.ApplicationOptions = await FwSqlData.GetApplicationOptions2Async(conn, _sqlServerOptions);
            }
        }
        //--------------------------------------------------------------------------------------------- 
        protected abstract string Unabbreviate(string value);
        //--------------------------------------------------------------------------------------------- 
        protected abstract List<Type> GetLogicTypes();
        //--------------------------------------------------------------------------------------------- 
        protected abstract List<Type> GetControllerTypes();
        //--------------------------------------------------------------------------------------------- 
        protected abstract List<Type> GetBranchTypes();
        //--------------------------------------------------------------------------------------------- 
        /// <summary>
        /// This supplies a default edition for security tree nodes.  This was intended to be used for development purposes before the actual editions have
        /// been programmed throughout the system.
        /// </summary>
        /// <returns></returns>
        public abstract string GetDefaultEditions();
        //--------------------------------------------------------------------------------------------- 
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
        //--------------------------------------------------------------------------------------------- 
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
                var controllerAttributes = controllerType.GetCustomAttributes();
                var controllerTemplate = string.Empty;
                foreach (var attr in controllerAttributes)
                {
                    if (attr.GetType().FullName == "Microsoft.AspNetCore.Mvc.RouteAttribute")
                    {
                        var propertyInfoTemplate = attr.GetType().GetProperty("Template");
                        controllerTemplate = (string)propertyInfoTemplate.GetValue(attr);
                        break;
                    }
                }

                FwAmController controller = new FwAmController();
                controller.Id = controllerAttribute.Id;
                controller.ParentId = controllerAttribute.ParentId;
                string[] namespaceParts = controllerType.Namespace.Split(new char[] { '.' });
                for (int namespaceIndex = 2; namespaceIndex < namespaceParts.Length - 1; namespaceIndex++)
                {
                    if (namespaceIndex > 2)
                    {
                        controller.Category += ".";
                    }
                    controller.Category += namespaceParts[namespaceIndex];
                }
                controller.ModuleName = namespaceParts[namespaceParts.Length - 1];

                controllerTemplate = controllerTemplate.Replace("[controller]", controller.ModuleName.ToLower());

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

                var controlAttributes = controllerType.GetCustomAttributes<FwControlAttribute>(false);
                Dictionary<string, FwControlAttribute> dictionaryGridAttributes = new Dictionary<string, FwControlAttribute>();
                foreach (var item in controlAttributes)
                {
                    module.Controls[item.SecurityId] = item;
                }

                var optionsGroupAttributes = controllerType.GetCustomAttributes<FwOptionsGroupAttribute>(false);
                Dictionary<string, FwOptionsGroupAttribute> dictionaryOptionsGroupAttributes = new Dictionary<string, FwOptionsGroupAttribute>();
                foreach (var item in optionsGroupAttributes)
                {
                    module.OptionsGroups[item.SecurityId] = item;
                }

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
                                    FwControllerMethodAttribute controllerMethodAttribute = (FwControllerMethodAttribute)methodInfo.GetCustomAttribute(typeof(FwControllerMethodAttribute), false);
                                    var methodAttributes = methodInfo.GetCustomAttributes();
                                    string method = string.Empty;
                                    string methodTemplate = string.Empty;
                                    foreach (var methodAttribute in methodAttributes)
                                    {
                                        if (methodAttribute.GetType().FullName == "Microsoft.AspNetCore.Mvc.HttpGetAttribute")
                                        {
                                            method = "GET";
                                        }
                                        if (methodAttribute.GetType().FullName == "Microsoft.AspNetCore.Mvc.HttpPostAttribute")
                                        {
                                            method = "POST";
                                        }
                                        if (methodAttribute.GetType().FullName == "Microsoft.AspNetCore.Mvc.HttpPutAttribute")
                                        {
                                            method = "PUT";
                                        }
                                        if (methodAttribute.GetType().FullName == "Microsoft.AspNetCore.Mvc.HttpDeleteAttribute")
                                        {
                                            method = "DELETE";
                                        }
                                        if (method.Length > 0)
                                        {
                                            var propertyInfoTemplate = methodAttribute.GetType().GetProperty("Template");
                                            methodTemplate = "/" + (string)propertyInfoTemplate.GetValue(methodAttribute);
                                            break;
                                        }
                                    }
                                    if (this.HasProductEdition(controllerMethodAttribute.Editions, rawProductKey, rawEditionKey))
                                    {
                                        FwAmControllerAction action = new FwAmControllerAction();
                                        action.Id = controllerMethodAttribute.Id;

                                        //string actionName = methodInfo.Name;
                                        //if (actionName.EndsWith("Async"))
                                        //{
                                        //    actionName = actionName.Substring(0, actionName.Length - "Async".Length);
                                        //}
                                        //action.Name = actionName;
                                        action.Name = $"{method} {controllerTemplate}{methodTemplate}";
                                        if (!string.IsNullOrEmpty(controllerMethodAttribute.Caption))
                                        {
                                            action.OptionName = controllerMethodAttribute.Caption;
                                            //action.Name = controllerMethodAttribute.Caption + " - " + action.Name;
                                            action.Name = action.Name;
                                        }
                                        action.ActionType = controllerMethodAttribute.ActionType;
                                        action.ParentId = controllerMethodAttribute.ParentId;
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
                logic.Category = namespaceParts[namespaceParts.Length - 2];
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
            // copy the Logic to the product editions
            foreach (var productItem in this.Products)
            {
                foreach (var editionItem in productItem.Value.Editions)
                {
                    foreach (var moduleItem in editionItem.Value.Modules.ToList())
                    {
                        this.Products[productItem.Key].Editions[editionItem.Key].Modules[moduleItem.Key].Controller.Logic = this.Modules[moduleItem.Key].Controller.Logic;
                    }
                }
            }

            FwAmSecurityTreeNode nodeSystem = new FwAmSecurityTreeNode("System", "Security Tree", FwAmSecurityTreeNodeTypes.System);
            //this.Nodes[nodeSystem.Id] = nodeSystem;
            //nodeSystem.Properties["visible"] = "T";
            this.AddNode(null, nodeSystem);
            FwAppManager.Tree.System = nodeSystem;
            var productEditionTree = this.Products[this.Unabbreviate(FwAppManager.CurrentProduct)].Editions[this.Unabbreviate(FwAppManager.CurrentProductEdition)];
            foreach (var moduleItem in productEditionTree.Modules)
            {
                var module = moduleItem.Value;
                var categories = module.Category.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < categories.Length; i++)
                {
                    var currentCategory = categories[i];
                    var parentId = new StringBuilder();
                    var categoryId = new StringBuilder();
                    for (int j = 0; j <= i; j++)
                    {
                        if (j > 0)
                        {
                            categoryId.Append(".");
                        }
                        if (j < i)
                        {
                            if (j < 0)
                            {
                                parentId.Append(".");
                            }
                            parentId.Append(categories[j]);
                        }
                        categoryId.Append(categories[j]);
                    }

                    var categoryNode = nodeSystem.FindById(categoryId.ToString());
                    if (categoryNode == null)
                    {
                        categoryNode = new FwAmSecurityTreeNode(categoryId.ToString(), currentCategory, FwAmSecurityTreeNodeTypes.Category);
                        categoryNode.Properties["visible"] = "T";
                        //this.Nodes[module.Category] = categoryNode;
                        if (i == 0)
                        {
                            this.AddNode(nodeSystem, categoryNode);
                            //nodeSystem.Children.Add(categoryNode);
                        }
                        else
                        {
                            var parentNode = nodeSystem.FindById(parentId.ToString());
                            this.AddNode(parentNode, categoryNode);
                            //parentNode.Children.Add(categoryNode);
                        }
                    }
                }
            }
            foreach (var moduleItem in productEditionTree.Modules)
            {
                var module = moduleItem.Value;
                var moduleNode = new FwAmSecurityTreeNode(module.Id, module.Name, FwAmSecurityTreeNodeTypes.Module);
                moduleNode.Properties["visible"] = "T";
                //this.Nodes[moduleNode.Id] = moduleNode;
                var categoryNode = nodeSystem.FindById(module.Category);
                this.AddNode(categoryNode, moduleNode);
                //categoryNode.Children.Add(moduleNode);
                //nodeSystem.Children.Add(moduleNode);
                var moduleActionNodes = new Dictionary<FwControllerActionTypes, FwAmSecurityTreeNode>();
                FwAmSecurityTreeNode moduleActionsNode = null;
                FwAmSecurityTreeNode moduleOptionsNode = null;
                FwAmSecurityTreeNode moduleApiMethodsNode = null;
                FwAmSecurityTreeNode moduleControlsNode = null;
                Dictionary<string, FwAmSecurityTreeNode> moduleOptionsGroupsNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlActionNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlBrowseNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlNewNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlEditNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlSaveNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlDeleteNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                Dictionary<string, FwAmSecurityTreeNode> controlOptionNodes = new Dictionary<string, FwAmSecurityTreeNode>();
                foreach (var action in module.Controller.Actions)
                {
                    var controllerMethodNode = new FwAmSecurityTreeNode(action.Id, action.Name, FwAmSecurityTreeNodeTypes.ControllerMethod);
                    //this.Nodes[controllerMethodNode.Id] = controllerMethodNode;
                    switch (action.ActionType)
                    {
                        case FwControllerActionTypes.Browse:
                        case FwControllerActionTypes.View:
                        case FwControllerActionTypes.New:
                        case FwControllerActionTypes.Edit:
                        case FwControllerActionTypes.Save:
                        case FwControllerActionTypes.Delete:
                            if (moduleActionNodes.Count == 0)
                            {
                                moduleActionsNode = new FwAmSecurityTreeNode(module.Id + "-Actions", "Actions", FwAmSecurityTreeNodeTypes.ModuleActions);
                                //moduleActionsNode.Properties["visible"] = "T";
                                //this.Nodes[moduleActionsNode.Id] = moduleActionsNode;
                                this.AddNode(moduleNode, moduleActionsNode);
                                //moduleNode.Children.Add(moduleActionsNode);
                            }
                            if (!moduleActionNodes.ContainsKey(action.ActionType))
                            {
                                var moduleActionNode = new FwAmSecurityTreeNode(module.Id + "-" + action.ActionType.ToString(), action.ActionType.ToString(), FwAmSecurityTreeNodeTypes.ModuleAction);
                                moduleActionNode.Properties["action"] = action.ActionType.ToString();
                                moduleActionNode.Properties["visible"] = "T";
                                //this.Nodes[moduleActionNode.Id] = moduleActionNode;
                                moduleActionNodes[action.ActionType] = moduleActionNode;
                                this.AddNode(moduleActionsNode, moduleActionNode);
                                //moduleActionsNode.Children.Add(moduleActionNode);
                            }
                            this.AddNode(moduleActionNodes[action.ActionType], controllerMethodNode);
                            //moduleActionNodes[action.ActionType].Children.Add(controllerMethodNode);

                            moduleActionsNode.Children.Sort((x, y) =>
                            {
                                if (x.Properties["action"] == "Browse")
                                {
                                    return -1;
                                }
                                else if (x.Properties["action"] == "View")
                                {
                                    if (y.Properties["action"] == "Browse")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "View")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "New")
                                {
                                    if (y.Properties["action"] == "Browse" || y.Properties["action"] == "View")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "New")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "Edit")
                                {
                                    if (y.Properties["action"] == "Browse" || y.Properties["action"] == "View" || y.Properties["action"] == "New")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "Edit")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "Save")
                                {
                                    if (y.Properties["action"] == "Browse" || y.Properties["action"] == "View" || y.Properties["action"] == "New" || y.Properties["action"] == "Edit")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "Save")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "Delete")
                                {
                                    if (y.Properties["action"] == "Browse" || y.Properties["action"] == "View" || y.Properties["action"] == "New" || y.Properties["action"] == "Edit" || y.Properties["action"] == "Save")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "Delete")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else
                                {
                                    return 1;
                                }
                            });
                            break;
                        case FwControllerActionTypes.Option:
                            if (moduleOptionsNode == null)
                            {
                                moduleOptionsNode = new FwAmSecurityTreeNode(module.Id + "-Options", "Options", FwAmSecurityTreeNodeTypes.ModuleOptions);
                                //moduleOptionsNode.Properties["visible"] = "T";
                                //this.Nodes[moduleOptionsNode.Id] = moduleOptionsNode;
                                this.AddNode(moduleNode, moduleOptionsNode);
                                //moduleNode.Children.Add(moduleOptionsNode);
                            }
                            if (!string.IsNullOrEmpty(action.ParentId))
                            {
                                if (!moduleOptionsGroupsNodes.ContainsKey(action.ParentId))
                                {
                                    moduleOptionsGroupsNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId, module.OptionsGroups[action.ParentId].Caption, FwAmSecurityTreeNodeTypes.ModuleOption);
                                    this.AddNode(moduleOptionsNode, moduleOptionsGroupsNodes[action.ParentId]);
                                    //moduleOptionsNode.Children.Add(moduleOptionsGroupsNodes[action.ParentId]);
                                }
                                this.AddNode(moduleOptionsGroupsNodes[action.ParentId], controllerMethodNode);
                                //moduleOptionsGroupsNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            else
                            {
                                var optionGroup = new FwAmSecurityTreeNode(controllerMethodNode.Id, action.OptionName, FwAmSecurityTreeNodeTypes.ModuleOption);
                                controllerMethodNode.Id += "-Method";
                                this.AddNode(optionGroup, controllerMethodNode);
                                //optionGroup.Children.Add(controllerMethodNode);
                                this.AddNode(moduleOptionsNode, optionGroup);
                                //moduleOptionsNode.Children.Add(optionGroup);
                            }
                            break;
                        case FwControllerActionTypes.ApiMethod:
                            if (moduleApiMethodsNode == null)
                            {
                                moduleApiMethodsNode = new FwAmSecurityTreeNode(module.Id + "-ApiMethods", "API Methods", FwAmSecurityTreeNodeTypes.ModuleApiMethods);
                                //moduleApiMethodsNode.Properties["visible"] = "T";
                                //this.Nodes[moduleApiMethodsNode.Id] = moduleApiMethodsNode;
                                this.AddNode(moduleNode, moduleApiMethodsNode);
                                //moduleNode.Children.Add(moduleApiMethodsNode);
                            }
                            //controllerMethodNode.Properties["visible"] = "T";
                            this.AddNode(moduleApiMethodsNode, controllerMethodNode);
                            //moduleApiMethodsNode.Children.Add(controllerMethodNode);
                            break;
                        case FwControllerActionTypes.ControlBrowse:
                        case FwControllerActionTypes.ControlNew:
                        case FwControllerActionTypes.ControlEdit:
                        case FwControllerActionTypes.ControlSave:
                        case FwControllerActionTypes.ControlDelete:
                        case FwControllerActionTypes.ControlOption:
                            if (moduleControlsNode == null)
                            {
                                moduleControlsNode = new FwAmSecurityTreeNode(module.Id + "-Controls", "Controls", FwAmSecurityTreeNodeTypes.Controls);
                                //moduleComponentsNode.Properties["visible"] = "T";
                                //this.Nodes[moduleControlsNode.Id] = moduleControlsNode;
                                this.AddNode(moduleNode, moduleControlsNode);
                                //moduleNode.Children.Add(moduleControlsNode);
                            }
                            if (!controlNodes.ContainsKey(action.ParentId))
                            {
                                controlNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId, module.Controls[action.ParentId].Caption, FwAmSecurityTreeNodeTypes.Control);
                                controlNodes[action.ParentId].Properties["visible"] = "T";
                                if (module.Controls.ContainsKey(action.ParentId))
                                {
                                    controlNodes[action.ParentId].Properties["controltype"] = module.Controls[action.ParentId].ControlType.ToString();
                                }
                                //this.Nodes[controlNodes[action.ParentId].Id] = controlNodes[action.ParentId];
                                this.AddNode(moduleControlsNode, controlNodes[action.ParentId]);
                                //moduleControlsNode.Children.Add(controlNodes[action.ParentId]);
                            }
                            if ((action.ActionType == FwControllerActionTypes.ControlBrowse ||
                                 action.ActionType == FwControllerActionTypes.ControlNew ||
                                 action.ActionType == FwControllerActionTypes.ControlEdit ||
                                 action.ActionType == FwControllerActionTypes.ControlSave ||
                                 action.ActionType == FwControllerActionTypes.ControlDelete) && !controlActionNodes.ContainsKey(action.ParentId))
                            {
                                controlActionNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions", "Actions", FwAmSecurityTreeNodeTypes.ControlActions);
                                //this.Nodes[controlActionNodes[action.ParentId].Id] = controlActionNodes[action.ParentId];
                                this.AddNode(controlNodes[action.ParentId], controlActionNodes[action.ParentId]);
                                //controlNodes[action.ParentId].Children.Add(controlActionNodes[action.ParentId]);
                            }
                            if ((action.ActionType == FwControllerActionTypes.ControlOption) && !controlOptionNodes.ContainsKey(action.ParentId))
                            {
                                controlOptionNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Options", "Options", FwAmSecurityTreeNodeTypes.ControlOptions);
                                //this.Nodes[controlOptionNodes[action.ParentId].Id] = controlOptionNodes[action.ParentId];
                                this.AddNode(controlNodes[action.ParentId], controlOptionNodes[action.ParentId]);
                                //controlNodes[action.ParentId].Children.Add(controlOptionNodes[action.ParentId]);
                            }
                            //controllerMethodNode.Properties["visible"] = "T";
                            if (action.ActionType == FwControllerActionTypes.ControlBrowse)
                            {
                                if (!controlBrowseNodes.ContainsKey(action.ParentId))
                                {
                                    controlBrowseNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions-Browse", "Browse", FwAmSecurityTreeNodeTypes.ControlAction);
                                    controlBrowseNodes[action.ParentId].Properties["action"] = action.ActionType.ToString();
                                    this.AddNode(controlActionNodes[action.ParentId], controlBrowseNodes[action.ParentId]);
                                    //controlActionNodes[action.ParentId].Children.Add(controlBrowseNodes[action.ParentId]);
                                }
                                this.AddNode(controlBrowseNodes[action.ParentId], controllerMethodNode);
                                //controlBrowseNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            if (action.ActionType == FwControllerActionTypes.ControlNew)
                            {
                                if (!controlNewNodes.ContainsKey(action.ParentId))
                                {
                                    controlNewNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions-New", "New", FwAmSecurityTreeNodeTypes.ControlAction);
                                    controlNewNodes[action.ParentId].Properties["action"] = action.ActionType.ToString();
                                    this.AddNode(controlActionNodes[action.ParentId], controlNewNodes[action.ParentId]);
                                    //controlActionNodes[action.ParentId].Children.Add(controlNewNodes[action.ParentId]);
                                }
                                this.AddNode(controlNewNodes[action.ParentId], controllerMethodNode);
                                //controlNewNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            if (action.ActionType == FwControllerActionTypes.ControlEdit)
                            {
                                if (!controlEditNodes.ContainsKey(action.ParentId))
                                {
                                    controlEditNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions-Edit", "Edit", FwAmSecurityTreeNodeTypes.ControlAction);
                                    controlEditNodes[action.ParentId].Properties["action"] = action.ActionType.ToString();
                                    this.AddNode(controlActionNodes[action.ParentId], controlEditNodes[action.ParentId]);
                                    //controlActionNodes[action.ParentId].Children.Add(controlEditNodes[action.ParentId]);
                                }
                                this.AddNode(controlEditNodes[action.ParentId], controllerMethodNode);
                                //controlEditNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            if (action.ActionType == FwControllerActionTypes.ControlSave)
                            {
                                if (!controlSaveNodes.ContainsKey(action.ParentId))
                                {
                                    controlSaveNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions-Save", "Save", FwAmSecurityTreeNodeTypes.ControlAction);
                                    controlSaveNodes[action.ParentId].Properties["action"] = action.ActionType.ToString();
                                    this.AddNode(controlActionNodes[action.ParentId], controlSaveNodes[action.ParentId]);
                                    //controlActionNodes[action.ParentId].Children.Add(controlSaveNodes[action.ParentId]);
                                }
                                this.AddNode(controlSaveNodes[action.ParentId], controllerMethodNode);
                                //controlSaveNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            if (action.ActionType == FwControllerActionTypes.ControlDelete)
                            {
                                if (!controlDeleteNodes.ContainsKey(action.ParentId))
                                {
                                    controlDeleteNodes[action.ParentId] = new FwAmSecurityTreeNode(action.ParentId + "-Actions-Delete", "Delete", FwAmSecurityTreeNodeTypes.ControlAction);
                                    controlDeleteNodes[action.ParentId].Properties["action"] = action.ActionType.ToString();
                                    this.AddNode(controlActionNodes[action.ParentId], controlDeleteNodes[action.ParentId]);
                                    //controlActionNodes[action.ParentId].Children.Add(controlDeleteNodes[action.ParentId]);
                                }
                                this.AddNode(controlDeleteNodes[action.ParentId], controllerMethodNode);
                                //controlDeleteNodes[action.ParentId].Children.Add(controllerMethodNode);
                            }
                            if (action.ActionType == FwControllerActionTypes.ControlOption)
                            {
                                var optionGroup = new FwAmSecurityTreeNode(controllerMethodNode.Id, action.OptionName, FwAmSecurityTreeNodeTypes.ControlOption);
                                controllerMethodNode.Id += "-Method";
                                this.AddNode(optionGroup, controllerMethodNode);
                                this.AddNode(controlOptionNodes[action.ParentId], optionGroup);
                                //optionGroup.Children.Add(controllerMethodNode);
                                //controlOptionNodes[action.ParentId].Children.Add(optionGroup);
                            }

                            controlActionNodes[action.ParentId].Children.Sort((x, y) =>
                            {
                                if (x.Properties["action"] == "ControlBrowse")
                                {
                                    return -1;
                                }
                                else if (x.Properties["action"] == "ControlNew")
                                {
                                    if (y.Properties["action"] == "ControlBrowse")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "ControlNew")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "ControlEdit")
                                {
                                    if (y.Properties["action"] == "ControlBrowse" || y.Properties["action"] == "ControlNew")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "ControlEdit")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "ControlSave")
                                {
                                    if (y.Properties["action"] == "ControlBrowse" || y.Properties["action"] == "ControlNew" || y.Properties["action"] == "ControlEdit")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "ControlSave")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else if (x.Properties["action"] == "ControlDelete")
                                {
                                    if (y.Properties["action"] == "ControlBrowse" || y.Properties["action"] == "ControlNew" || y.Properties["action"] == "ControlEdit" || y.Properties["action"] == "ControlSave")
                                    {
                                        return 1;
                                    }
                                    else if (y.Properties["action"] == "ControlDelete")
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        return -1;
                                    }
                                }
                                else
                                {
                                    return 1;
                                }
                            });
                            break;
                    }
                }
            }

            List<Type> branchTypes = this.GetBranchTypes();
            branchTypes.ForEach((Type type) =>
            {
                // Call the BuildBranch method of the SecurityTree branch via Reflection
                var methodInfoBuildBranch = type.GetMethod("BuildBranch");
                object instance = Activator.CreateInstance(type);
                methodInfoBuildBranch.Invoke(instance, new object[] { this });
            });
        }
        //--------------------------------------------------------------------------------------------- 
        protected void AddNode(FwAmSecurityTreeNode parent, FwAmSecurityTreeNode node)
        {
            if (string.IsNullOrEmpty(node.Id))
            {
                throw new Exception($"Security Tree node cannot be blank. NodeType={node.NodeType}, NodeCaption={node.Caption}");
            }
            if (this.Nodes.ContainsKey(node.Id))
            {
                throw new Exception($"Security Tree already contains a node with the id: {node.Id}. NodeType={node.NodeType}, NodeCaption={node.Caption}");
            }
            this.Nodes[node.Id] = node;
            if (parent != null)
            {
                parent.Children.Add(node);
            }
        }
        //--------------------------------------------------------------------------------------------- 
        protected List<Type> LoadTypesBySubclass(Assembly assembly, string rootNamespace, Type subClassOf)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(rootNamespace) && t.IsSubclassOf(subClassOf))
                .OrderBy(o => o.Name)
                .ToList<Type>();

            return types;
        }
        //--------------------------------------------------------------------------------------------- 
        protected List<Type> LoadSpecificType(Assembly assembly, Type type)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => t == type)
                .OrderBy(o => o.Name)
                .ToList<Type>();

            return types;
        }
        //--------------------------------------------------------------------------------------------- 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupsid">the group id to fetch</param>
        /// <param name="applyParentVisibility">when true, if a parent is set to visible 'F', then all children will get set to visible 'F'.  This allows efficient checks on node visibility.</param>
        /// <returns></returns>
        public async Task<FwAmGroupTree> GetGroupsTreeAsync(string groupsid, bool applyParentVisibility)
        {
            bool hidenewmenuoptionsbydefault;
            string jsonApplicationTree, appmanagerJson;
            FwAmGroupTree groupTree = null;
            FwAmSecurityTreeNode groupTreeNode;
            List<FwAmSqlGroupNode> securityNodes;
            DateTime datestamp = DateTime.MinValue;

            // use the datestamp to determine whether or not to use the cached group tree
            using (FwSqlConnection conn = new FwSqlConnection(_sqlServerOptions.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _sqlServerOptions.QueryTimeout);
                qry.Add("select top 1 datestamp, hidenewmenuoptionsbydefault, security");
                qry.Add("from groups with (nolock)");
                qry.Add("where groupsid = @groupsid");
                qry.AddParameter("@groupsid", groupsid);
                await qry.ExecuteAsync();
                if (qry.RowCount > 0)
                {
                    datestamp = qry.GetField("datestamp").ToDateTime();
                    hidenewmenuoptionsbydefault = qry.GetField("hidenewmenuoptionsbydefault").ToBoolean();
                    appmanagerJson = qry.GetField("security").ToString().TrimEnd();

                    if (GroupTrees.ContainsKey(groupsid))
                    {
                        groupTree = GroupTrees[groupsid + applyParentVisibility.ToString()];
                        if (groupTree.DateStamp != datestamp)
                        {
                            groupTree = null;
                        }
                    }

                    if (groupTree == null)
                    {
                        // do a deep copy of jsonApplicationTree by serialization, so groupTree doesn't share reference types with jsonApplicationTree
                        jsonApplicationTree = JsonConvert.SerializeObject(FwAppManager.Tree.System);
                        groupTree = new FwAmGroupTree();
                        groupTree.GroupsId = groupsid;
                        groupTree.DateStamp = datestamp;
                        groupTree.RootNode = JsonConvert.DeserializeObject<FwAmSecurityTreeNode>(jsonApplicationTree);
                        groupTree.RootNode.Id = "System";
                        groupTree.RootNode.NodeType = "System";
                        groupTree.RootNode.Caption = "Security Tree";
                        //groupTree.RootNode.Properties["visible"] = "T";
                        GroupTrees[groupsid + applyParentVisibility.ToString()] = groupTree;

                        groupTree.RootNode.InitGroupSecurityTree(hidenewmenuoptionsbydefault);
                        if (!string.IsNullOrEmpty(appmanagerJson))
                        {
                            securityNodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FwAmSqlGroupNode>>(appmanagerJson);
                            for (int secnodeNo = 0; secnodeNo < securityNodes.Count; secnodeNo++)
                            {
                                FwAmSqlGroupNode secnode = securityNodes[secnodeNo];
                                groupTreeNode = groupTree.RootNode.FindById(secnode.id);
                                if (groupTreeNode != null)
                                {
                                    // deep copy the sql security node properties to the new groupTreeNode properties
                                    var secnodePropertiesJson = JsonConvert.SerializeObject(secnode.properties);
                                    var secnodeProperties = JsonConvert.DeserializeObject<Dictionary<string, string>>(secnodePropertiesJson);
                                    foreach (var item in secnodeProperties)
                                    {
                                        groupTreeNode.Properties[item.Key] = item.Value;
                                    }
                                    if (FwAppManager.Tree.ModulesById.ContainsKey(groupTreeNode.Id))
                                    {
                                        foreach (var subModuleNodeItem in FwAppManager.Tree.ModulesById[groupTreeNode.Id].SubModules)
                                        {
                                            var subModuleNode = subModuleNodeItem.Value;
                                        }
                                    }
                                }
                            }
                        }
                        groupTree.RootNode.Children = groupTree.RootNode.Children.OrderBy(c => c.Caption).ToList();

                        // fix the visible properties on the nodes based on the parent visibility
                        if (applyParentVisibility)
                        {
                            await ApplyParentVisbilityToGroupTree(groupTree);
                        }


                    }
                }
            }

            return groupTree;
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task ApplyParentVisbilityToGroupTree(FwAmGroupTree groupTree)
        {
            foreach (var childNode in groupTree.RootNode.Children)
            {
                await ApplyParentVisbilityToGroupTreeNodesRecursive(childNode);
            }
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task ApplyParentVisbilityToGroupTreeNodesRecursive(FwAmSecurityTreeNode groupTreeNode)
        {
            var groupTreeNodeParent = groupTreeNode.Parent;
            while (groupTreeNodeParent != null)
            {
                if (!groupTreeNodeParent.Properties.ContainsKey("visible"))
                {
                    groupTreeNodeParent.Properties["visible"] = "T";
                }
                if (groupTreeNodeParent.Properties["visible"] == "F")
                {
                    groupTreeNode.Properties["visible"] = "F";
                    break;
                }
                groupTreeNodeParent = groupTreeNodeParent.Parent;
            }
            foreach (var childNode in groupTreeNode.Children)
            {
                await ApplyParentVisbilityToGroupTreeNodesRecursive(childNode);
            }

            // if edit permission is turned off for a module, then make sure it's turned off for any child grids
            if (groupTreeNode.NodeType == "Module" && groupTreeNode.Properties["visible"] == "T")
            {
                var moduleNode = groupTreeNode;
                var moduleActionsNode = moduleNode.Children.FirstOrDefault(x => x.NodeType == "ModuleActions");
                if (moduleActionsNode != null)
                {
                    var moduleEditNode = moduleActionsNode.Children.FirstOrDefault(x => x.NodeType == "ModuleAction" && x.Properties["action"] == "Edit");
                    var moduleSaveNode = moduleActionsNode.Children.FirstOrDefault(x => x.NodeType == "ModuleAction" && x.Properties["action"] == "Save");
                    var hasEdit = (moduleEditNode != null && moduleEditNode.Properties["visible"] == "T") || (moduleSaveNode != null && moduleSaveNode.Properties["visible"] == "T");
                    if (!hasEdit)
                    {
                        var controlsNode = moduleNode.Children.FirstOrDefault(x => x.NodeType == "Controls");
                        if (controlsNode != null)
                        {
                            foreach (var controlNode in controlsNode.Children)
                            {
                                var controlActionsNode = controlNode.Children.FirstOrDefault(x => x.NodeType == "ControlActions");
                                var controlEditNode = controlActionsNode.Children.FirstOrDefault(x => x.NodeType == "ControlAction" && x.Properties["action"] == "ControlEdit");
                                if (controlEditNode != null)
                                {
                                    controlEditNode.Properties["visible"] = "F";
                                }
                                else
                                {
                                    var controlSaveNode = controlActionsNode.Children.FirstOrDefault(x => x.NodeType == "ControlAction" && x.Properties["action"] == "ControlSave");
                                    if (controlSaveNode != null)
                                    {
                                        controlSaveNode.Properties["visible"] = "F";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        //--------------------------------------------------------------------------------------------- 
        public async Task LoadAllGroupTrees()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_sqlServerOptions.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _sqlServerOptions.QueryTimeout);
                qry.Add("select groupsid");
                qry.Add("from groups with (nolock)");
                List<dynamic> groupResults = await qry.QueryToDynamicList2Async();
                for (var rowno = 0; rowno < groupResults.Count; rowno++)
                {
                    string groupsid = groupResults[rowno].groupsid;
                    await FwAppManager.Tree.GetGroupsTreeAsync(groupsid, false);
                }
            }
        }
        //--------------------------------------------------------------------------------------------- 
        static string StripId(string id)
        {
            string result = id;
            if ((id != null) && (id.Length == 38))
            {
                result = id.Substring(1, 36);
            }
            return result;
        }
        //--------------------------------------------------------------------------------------------- 
        private FwAmSecurityTreeNode Add(string parentid, string id, string caption, FwAmSecurityTreeNodeTypes nodeType, bool allowonnewform = false)
        {
            FwAmSecurityTreeNode parentNode = null, node;
            id = StripId(id);
            parentid = StripId(parentid);
            if (this.Nodes.ContainsKey(id)) throw new Exception("Application Tree already contains node id: " + id);
            if ((parentid != null) && (!this.Nodes.ContainsKey(parentid) && !this.ExcludedNodes.ContainsKey(parentid))) throw new Exception("Application Tree does not contain parent node id: " + parentid);
            node = new FwAmSecurityTreeNode(id, caption, nodeType);
            this.Nodes[id] = node;
            if (parentid != null)
            {
                if (this.Nodes.ContainsKey(parentid))
                {
                    parentNode = this.Nodes[parentid];
                }
                else if (this.ExcludedNodes.ContainsKey(parentid))
                {
                    parentNode = this.ExcludedNodes[parentid];
                }
                node.Parent = parentNode;
                if (parentNode.Children == null)
                {
                    parentNode.Children = new List<FwAmSecurityTreeNode>();
                }
                parentNode.Children.Add(node);
            }

            return node;
        }
    }
}
