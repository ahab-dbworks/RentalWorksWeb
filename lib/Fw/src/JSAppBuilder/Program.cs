using Fw.MSBuild;
using Fw.MSBuildTasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSAppBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathJSAppBuilderConfig = string.Empty;
            string solutionPath = string.Empty;
            bool publish = false;
            string version = args[2];
            bool updateSchema = false;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-c":
                        i++;
                        pathJSAppBuilderConfig = args[i];
                        break;
                    case "-s":
                        i++;
                        solutionPath = args[i];
                        break;
                    case "-v":
                        i++;
                        version = args[i];
                        break;
                    case "-u":
                        i++;
                        updateSchema = args[i].ToLower().Equals("true");
                        break;
                    case "-p":
                        i++;
                        publish = args[i].ToLower().Equals("true");
                        break;
                }
            }

            FwJSAppBuilder config = new FwJSAppBuilder();
            config.BuildEngine = new VirtualBuildEngine();
            config.Publish = publish;
            config.Version = version;
            config.UpdateSchema = updateSchema;
            config.Build(pathJSAppBuilderConfig, solutionPath);
        }
    }
}
