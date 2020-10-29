using System;

namespace JSAppBuilderCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string pathJSAppBuilderConfig = string.Empty;
                string solutionPath = string.Empty;
                bool publish = false;
                bool attachDebugger = false;
                string version = args[2];
                bool ajaxLoadHtml = false;
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-ConfigFilePath":
                            i++;
                            pathJSAppBuilderConfig = args[i];
                            break;
                        case "-SolutionDir":
                            i++;
                            solutionPath = args[i];
                            break;
                        case "-Version":
                            i++;
                            version = args[i];
                            break;
                        case "-Publish":
                            i++;
                            publish = args[i].ToLower().Equals("true");
                            break;
                        case "-AttachDebugger":
                            i++;
                            attachDebugger = args[i].ToLower().Equals("true");
                            break;
                        case "-AjaxLoadHtml":
                            i++;
                            ajaxLoadHtml = args[i].ToLower().Equals("true");
                            break;
                    }
                }

                FwJSAppBuilder config = new FwJSAppBuilder();
                config.Publish = publish;
                config.Version = version;
                config.AttachDebugger = attachDebugger;
                config.AjaxLoadHtml = ajaxLoadHtml;
                config.Build(pathJSAppBuilderConfig, solutionPath);
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message + ex.StackTrace);
                Environment.ExitCode = -1;
            }
        }
    }
}
