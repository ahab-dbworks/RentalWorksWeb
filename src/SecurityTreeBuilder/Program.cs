using FwCore.Controllers;
using FwStandard.Models;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using WebApi.ApplicationManager;
using WebApi.Controllers;

namespace SecurityTreeBuilder
{
    class Program
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();

            app.HelpOption();
            //var optionSubject = app.Option("-s|--subject <SUBJECT>", "The subject", CommandOptionType.SingleValue);
            var optionOutputFile = app.Option("--outputfile <OUTPUTFILE>", "The output json file.", CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                //var subject = optionOutputFile.HasValue() ? optionOutputFile.Value() : "world";
                if (!optionOutputFile.HasValue())
                {
                    throw new ArgumentException("Please include --outputfile <OUTPUTFILE>");
                }

                Run(optionOutputFile);

                return 0;
            });

            return app.Execute(args);
        }

        static List<Type> LoadTypes(Assembly assembly, string rootNamespace, Type subClassOf)
        {
            List<Type> types = assembly.GetTypes()
                .Where(t => t.FullName.StartsWith(rootNamespace) && t.IsSubclassOf(subClassOf))
                .OrderBy(o => o.Name)
                .ToList<Type>();

            return types;
        }

        class SecController
        {
            public string Namespace { get; set; } = "";
            public string LogicType { get; set; } = "";
            public SecLogic Logic { get; set; } = new SecLogic();
            public List<string> Methods { get; set; } = new List<string>();
        }

        class SecLogic
        {
            public string Name { get; set; } = "";
            public List<string> Properties { get; set; } = new List<string>();
        }

        class SecProperty
        {
            public string id { get; set; } = "";
        }

        static void Run(CommandOption optionOutputFile)
        {
            Dictionary<string, SecController> controllers = new Dictionary<string, SecController>();

            // controllers
            //List<Type> appDataControllerTypes = Program.LoadTypes(typeof(WebApi.Program).Assembly, "WebApi", typeof(AppDataController));
            //List<Type> appReportControllerTypes = Program.LoadTypes(typeof(WebApi.Program).Assembly, "WebApi", typeof(AppReportController));
            List<Type> controllerTypes = Program.LoadTypes(typeof(WebApi.Program).Assembly, "WebApi", typeof(FwController));
            //List<Type> controllerTypes = new List<Type>();
            //controllerTypes.AddRange(appDataControllerTypes);
            //controllerTypes.AddRange(appReportControllerTypes);
            foreach (Type controllerType in controllerTypes)
            {
                SecController secController = new SecController();
                controllers[controllerType.Name] = secController;
                secController.Namespace = controllerType.Namespace;

                // look up the LogicType
                if (controllerType.IsSubclassOf(typeof(AppDataController)))
                {
                    FwApplicationConfig appConfig = new FwApplicationConfig();
                    var options = Options.Create<FwApplicationConfig>(appConfig);
                    AppDataController controller = (AppDataController)Activator.CreateInstance(controllerType, new object[] { options });
                    FieldInfo fieldInfo = controller.GetType().GetField("logicType", BindingFlags.NonPublic | BindingFlags.Instance);
                    Type logicType = (Type)fieldInfo.GetValue(controller);
                    if (logicType != null)
                    {
                        secController.LogicType = logicType.FullName;
                    }
                }
                
                var methodInfos = controllerType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttribute(typeof(HttpPostAttribute)) != null 
                             || m.GetCustomAttribute(typeof(HttpGetAttribute)) != null
                             || m.GetCustomAttribute(typeof(HttpPutAttribute)) != null
                             || m.GetCustomAttribute(typeof(HttpDeleteAttribute)) != null)
                    .OrderBy(o => o.Name)
                    .ToList<MethodInfo>();
                methodInfos.ForEach(methodInfo =>
                {
                    secController.Methods.Add(methodInfo.Name);
                });
            }

            List<Type> logicTypes = Program.LoadTypes(typeof(WebApi.Program).Assembly, "WebApi", typeof(WebApi.Logic.AppBusinessLogic));
            foreach (Type logicType in logicTypes)
            {
                var controllerItems = controllers
                    .Where(c => /*c.Value.Namespace == logicType.Namespace &&*/ c.Value.LogicType == logicType.FullName)
                    .ToList();
                if (controllerItems.Count > 0)
                {
                    SecController controller = controllerItems[0].Value;
                    var logic = new SecLogic();
                    logic.Name = logicType.Name;
                    controller.Logic = logic;
                    var propertyInfos = logicType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null)
                        .OrderBy(o => o.Name)
                        .ToList<PropertyInfo>();
                    propertyInfos.ForEach(propertyInfo =>
                    {
                        logic.Properties.Add(propertyInfo.Name);
                    });
                }
            }

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(controllers, jsonSettings);
            File.WriteAllText(optionOutputFile.Value(), json);
        }
    }
}
