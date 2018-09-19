using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using System;
using System.IO;
using WebApi.ApplicationManager;

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

                run(optionOutputFile);

                return 0;
            });

            return app.Execute(args);
        }

        static void run(CommandOption optionOutputFile)
        {
            AppManager tree = new AppManager();
            tree.LoadFromWebApi();

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(tree, jsonSettings);
            File.WriteAllText(optionOutputFile.Value(), json);

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
