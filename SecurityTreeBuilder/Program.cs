using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SecurityTreeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Type> appBusinessLogicTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules", typeof(WebApi.Logic.AppBusinessLogic));
            List<Type> appDataControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules", typeof(WebApi.Controllers.AppDataController));

            Dictionary<string, List<Type>> namespaceControllers = new Dictionary<string, List<Type>>();
            for (int i = 0; i < appDataControllerTypes.Count; i++)
            {
                Type controllerType = appDataControllerTypes[i];
                if (!namespaceControllers.ContainsKey(controllerType.Namespace))
                {
                    namespaceControllers[controllerType.Namespace] = new List<Type>();
                }
                namespaceControllers[controllerType.Namespace].Add(controllerType);
            }

            SecurityTree tree = new SecurityTree();
            for (int i = 0; i < appBusinessLogicTypes.Count; i++)
            {
                Type blType = appBusinessLogicTypes[i];

                // Module
                SecurityTreeModule module = new SecurityTreeModule();
                tree.RentalWorks.Modules.Add(module);
                tree.TrakItWorks.Modules.Add(module);
                tree.QuikScan.Modules.Add(module);
                if (blType.Name.EndsWith("Logic"))
                {
                    module.Name = blType.Name.Substring(0, blType.Name.Length - 6);
                }
                module.Name = blType.Name;
                string[] namespaceParts = blType.Namespace.Split(new char[] { '.' });
                if (blType.Namespace.StartsWith("WebApi.Modules.") && namespaceParts.Length >= 3)
                {
                    module.Category = namespaceParts[2];
                }

                // Components
                List<PropertyInfo> propertyInfos = blType.GetProperties().OrderBy(o => o.Name).ToList<PropertyInfo>();
                for (int j = 0; j < propertyInfos.Count; j++)
                {
                    SecurityTreeModuleComponents component = new SecurityTreeModuleComponents();
                    component.Name = propertyInfos[j].Name;
                    module.Components.Add(component);
                }

                // Actions - this is a temporary hacked together way of doing the actions without Attributes
                SecurityTreeModuleAction newAction = new SecurityTreeModuleAction();
                newAction.Name = "New";
                module.Actions.Add(newAction);

                SecurityTreeModuleAction editAction = new SecurityTreeModuleAction();
                editAction.Name = "Edit";
                module.Actions.Add(editAction);

                SecurityTreeModuleAction deleteAction = new SecurityTreeModuleAction();
                deleteAction.Name = "Delete";
                module.Actions.Add(deleteAction);

                if (namespaceControllers.ContainsKey(blType.Namespace))
                {
                    var controllerTypes = namespaceControllers[blType.Namespace];
                    for (int j = 0; j < controllerTypes.Count; j++)
                    {
                        var methodInfos = controllerTypes[j].GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                        for (int k = 0; k < methodInfos.Length; k++)
                        {
                            SecurityTreeModuleAction action = new SecurityTreeModuleAction();
                            action.Name = controllerTypes[j].Name + "." + methodInfos[k].Name;
                            module.Actions.Add(action);
                        }
                    }
                }
            }

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;
            string json = JsonConvert.SerializeObject(tree, jsonSettings);
            string path = Path.Combine(Environment.CurrentDirectory, "SecurityTree.json");
            File.WriteAllText(path, json);

            //List<Type> homeModelTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Home", typeof(WebApi.Logic.AppBusinessLogic));
            //List<Type> homeControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Home", typeof(WebApi.Controllers.AppDataController));
          
            //List<Type> administratorModelTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Administrator", typeof(WebApi.Logic.AppBusinessLogic));
            //List<Type> administratorControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Administrator", typeof(WebApi.Controllers.AppDataController));

            //List<Type> reportControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Reports", typeof(WebApi.Controllers.AppDataController));

            //List<Type> settingsModelTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Settings", typeof(WebApi.Logic.AppBusinessLogic));
            //List<Type> settingsControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Settings", typeof(WebApi.Controllers.AppDataController));

            //List<Type> utilitiesModelTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Utilities", typeof(WebApi.Logic.AppBusinessLogic));
            //List<Type> utilitiesControllerTypes = SecurityTreeLoader.LoadTypes("WebApi.Modules.Utilities", typeof(WebApi.Controllers.AppDataController));


            //for (int i = 0; i < homeModuleTypes.Count; i++)
            //{
            //    Console.WriteLine(homeModuleTypes[i].Name);
            //}
            //Console.WriteLine("Hello World!");
        }
    }
}
