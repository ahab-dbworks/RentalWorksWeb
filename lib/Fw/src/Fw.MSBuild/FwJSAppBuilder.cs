using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Net;
using System.Xml;
using Fw.Json.ValueTypes;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Threading;
using Microsoft.Win32;
using Fw.MSBuild;

namespace Fw.MSBuildTasks
{
    public class FwJSAppBuilder : Task
    {
        [Required]
        public string ConfigFilePath { get;set;} = string.Empty;
        //[Required]
        public string SolutionDir { get;set;} = string.Empty;
        public string Version { get;set;} = string.Empty;
        public bool Publish { get;set;} = false;
        public bool AttachDebugger { get;set;} = false;
        //---------------------------------------------------------------------------------------------
        public FwJSAppBuilder() : base()
        {
 
        }
        //---------------------------------------------------------------------------------------------
        public override bool Execute()
        {
            return Build(ConfigFilePath, SolutionDir);
        }
        //---------------------------------------------------------------------------------------------
        public bool Build(string configFilePath, string solutionDir)
        {
            bool success;
            JSAppBuilderConfig config;
            string applicationConfigPath, 
                mergeFile_inputFileUri, mergeFile_inputFileText, mergeFile_mergeFragment,
                sourceFile_inputFileUri, sourceFile_inputFileText,
                sourceFiles_outputFile, sourceFiles_minifiedFile, sourceFiles_combinedOutputPath,
                javaPath, template, tableName, datafield, columnName;
            string[] datafieldFragments;
            List<string> html_outputFilePath = new List<string>(), html_inputFilePath = new List<string>();
            List<StringBuilder> html_outputFileText = new List<StringBuilder>();
            StringBuilder sourceFiles, sourceFiles_combinedText, mergeTemplate, mergeFiles;
            CommandLineBuilder commandLine;
            MergeFile mergeFile;
            XmlDocument doc;
            XmlNodeList scrollerNodes, scrollerFieldNodes, formfieldNodes;
            FwApplicationSchema.Form form;
            FwApplicationSchema.Browse browse;
            FwApplicationSchema.FormTable table;
            FwApplicationSchema.Column column;
            Dictionary<string, DatabaseConnection> databaseConnections = null;

            // page and controls
            StringBuilder sbModules;
            string fileBrowseTemplate=null, fileFormTemplate=null, pathBrowseTemplate, pathFormTemplate, pathSite, 
                pathModules, nameModule, pathBrowse, pathForm;
            string[] pathModuleArray, pathModuleSubDirectoryArray, pathModuleSubDirectoryArray2;
            List<string> pathModuleList;

            try
            {
                success = true;
                if (AttachDebugger)
                {
                    System.Diagnostics.Debugger.Launch();
                }
                if (configFilePath.Length > 0)
                {
                    string[] versionFragments = this.Version.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                    //if ((versionFragments.Length == 4) && (versionFragments[3] == "0"))
                    //{
                    //    Version = versionFragments[0] + "." + versionFragments[1] + "." + versionFragments[2];
                    //}
                    
                    config = (JSAppBuilderConfig)FwFuncMSBuild.LoadObject(typeof(JSAppBuilderConfig), configFilePath, solutionDir, this.Log);
                    pathSite     = Path.GetDirectoryName(configFilePath);

                    for (int i = 0; i < config.Targets.Count; i++)
                    {
                        if (this.Publish == config.Targets[i].Publish)
                        {
                            if (!this.Publish)
                            {
                                //BuildFwReferencesJsFile(solutionDir);
                                //BuildAppReferencesJsFile(config);
                            }

                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                TargetFile file = config.Targets[i].Files[targetfileno];
                                html_outputFilePath.Add(GetAbsolutePath(Path.Combine(config.Targets[i].OutputDirectory, config.Targets[i].Files[targetfileno].OutputFile)));
                                html_inputFilePath.Add(GetAbsolutePath(config.Targets[i].Files[targetfileno].InputFile));
                                html_outputFileText.Add(new StringBuilder(File.ReadAllText(html_inputFilePath[targetfileno])));
                            }

                            bool buildmodules = false;
                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                if (html_outputFileText[targetfileno].ToString().Contains("{{{Modules}}}"))
                                {
                                    buildmodules = true;
                                }
                            }
                                
                            #region merge the <MergeFiles> into the target output file
                            // Merge Files
                            for (int j = 0; j < config.MergeSections.Count; j++)
                            {
                                mergeFiles = new StringBuilder();
                                for (int k = 0; k < config.MergeSections[j].MergeFiles.Count; k++)
                                {
                                    mergeFile               = config.MergeSections[j].MergeFiles[k];
                                    mergeFile_inputFileUri  = ApplyFields(mergeFile.Uri, config, config.Targets[i].Publish);
                                    mergeFile_inputFileText = UriToString(mergeFile_inputFileUri, this);
                                    mergeFile_mergeFragment = mergeFile.Template.Replace("{{{File}}}", mergeFile_inputFileText)
                                                                                .Replace("{{Version}}", Version);
                                    mergeFiles.AppendLine(mergeFile_mergeFragment);
                                }
                                for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                {
                                    html_outputFileText[targetfileno].Replace(config.MergeSections[j].ReplaceField, mergeFiles.ToString());
                                }
                            }
                            #endregion

                            #region replace: {{{Pages}}}
                            // Pages & Controls
                            bool buildpages = false;
                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                if (html_outputFileText[targetfileno].ToString().Contains("{{{Pages}}}"))
                                {
                                    buildpages = true;
                                }
                            }
                            if (buildpages)
                            {
                                sbModules = new StringBuilder();
                                // Fw Controls
                                pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Controls");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories);
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate=null;
                                        nameModule = Path.GetFileNameWithoutExtension(pathModule);
                                        if (File.Exists(pathModule)) filePageTemplate = File.ReadAllText(pathModule);
                                        if (filePageTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-controls-" + nameModule + "\" type=\"text/html\">");
                                            sbModules.AppendLine(filePageTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Fw Pages
                                pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Pages");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories);
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate=null;
                                        nameModule = Path.GetFileNameWithoutExtension(pathModule);
                                        if (File.Exists(pathModule)) filePageTemplate = File.ReadAllText(pathModule);
                                        if (filePageTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-pages-" + nameModule + "\" type=\"text/html\">");
                                            sbModules.AppendLine(filePageTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Application Controls
                                pathModules  = Path.Combine(pathSite, @"source\Controls");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories);
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate=null;
                                        nameModule = Path.GetFileNameWithoutExtension(pathModule);
                                        if (File.Exists(pathModule)) filePageTemplate = File.ReadAllText(pathModule);
                                        if (filePageTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-controls-" + nameModule + "\" type=\"text/html\">");
                                            sbModules.AppendLine(filePageTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Application Pages
                                pathModules  = Path.Combine(pathSite, @"source\Pages");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories);
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate=null;
                                        nameModule = Path.GetFileNameWithoutExtension(pathModule);
                                        if (File.Exists(pathModule)) filePageTemplate = File.ReadAllText(pathModule);
                                        if (filePageTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-pages-" + nameModule + "\" type=\"text/html\">");
                                            sbModules.AppendLine(filePageTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                {
                                    html_outputFileText[targetfileno].Replace("{{{Pages}}}", sbModules.ToString());
                                }
                            }
                            #endregion

                            #region replace: {{{Modules}}}
                            // Modules
                            if (buildmodules)
                            {
                                // Fw Modules
                                sbModules = new StringBuilder();
                                pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Modules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate   = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate))   fileFormTemplate   = File.ReadAllText(pathFormTemplate);
                                        if (fileBrowseTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\">");
                                            sbModules.AppendLine(fileBrowseTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                        if (fileFormTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\">");
                                            sbModules.AppendLine(fileFormTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Fw SubModules
                                pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\SubModules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate   = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate))   fileFormTemplate   = File.ReadAllText(pathFormTemplate);
                                        if (fileBrowseTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\">");
                                            sbModules.AppendLine(fileBrowseTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                        if (fileFormTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\">");
                                            sbModules.AppendLine(fileFormTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Application Modules
                                pathModules  = Path.Combine(pathSite, @"source\Modules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        DirectoryInfo moduleDirectoryInfo = new DirectoryInfo(pathModule);
                                        nameModule = moduleDirectoryInfo.Name;
                                        fileBrowseTemplate = null;
                                        fileFormTemplate = null;
                                        FileInfo[] fileInfos = moduleDirectoryInfo.GetFiles("*.htm");
                                        foreach (FileInfo fileInfo in fileInfos)
                                        {
                                            if (fileInfo.Name.EndsWith("Browse.htm") /*|| fileInfo.Name.EndsWith("Browse.html")*/)
                                            {
                                                fileBrowseTemplate = File.ReadAllText(fileInfo.FullName);
                                            }
                                            else if (fileInfo.Name.EndsWith("Form.htm") /*|| fileInfo.Name.EndsWith("Form.html")*/)
                                            {
                                                fileFormTemplate   = File.ReadAllText(fileInfo.FullName);
                                            }
                                            else
                                            {
                                                if (Publish)
                                                {
                                                    string htmlTemplate = File.ReadAllText(fileInfo.FullName);
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + fileInfo.Name.Replace(".htm", string.Empty) + "\" type=\"text/html\">");
                                                    sbModules.AppendLine(htmlTemplate);
                                                    sbModules.AppendLine("</script>");
                                                }
                                                else
                                                {
                                                    string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + fileInfo.Name;
                                                    urlHtml = ApplyFields(urlHtml, config, Publish);
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + fileInfo.Name.Replace(".htm", string.Empty) + "\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                                }
                                            }
                                        }
                                        if (fileBrowseTemplate != null)
                                        {
                                            if (Publish)
                                            {
                                                sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\">");
                                                sbModules.AppendLine(fileBrowseTemplate);
                                                sbModules.AppendLine("</script>");
                                            }
                                            else
                                            {
                                                string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                                urlHtml = ApplyFields(urlHtml, config, Publish);
                                                sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                            }
                                        }
                                        if (fileFormTemplate != null)
                                        {
                                            if (Publish)
                                            {
                                                sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\">");
                                                sbModules.AppendLine(fileFormTemplate);
                                                sbModules.AppendLine("</script>");
                                            }
                                            else
                                            {
                                                string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Form.htm";
                                                urlHtml = ApplyFields(urlHtml, config, Publish);
                                                sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                            }
                                        }
                                    }
                                }
                                // Application SubModules
                                pathModules = Path.Combine(pathSite, @"source\SubModules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate   = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate))   fileFormTemplate   = File.ReadAllText(pathFormTemplate);
                                        if (fileBrowseTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\">");
                                            sbModules.AppendLine(fileBrowseTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                        if (fileFormTemplate != null)
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\">");
                                            sbModules.AppendLine(fileFormTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                {
                                    html_outputFileText[targetfileno].Replace("{{{Modules}}}", sbModules.ToString());
                                }
                            }
                            #endregion

                            #region replace: {{{Grids}}}
                            // Grids
                            sbModules = new StringBuilder();
                            // Fw Grids
                            pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Grids");
                            if (Directory.Exists(pathModules))
                            {
                                pathModuleList = getModuleDirectories(pathModules);
                                foreach (string pathModule in pathModuleList)
                                {
                                    nameModule = new DirectoryInfo(pathModule).Name;
                                    pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                    fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                    sbModules.AppendLine("<script id=\"tmpl-grids-" + nameModule + "Browse\" type=\"text/html\">");
                                    sbModules.AppendLine(fileBrowseTemplate);
                                    sbModules.AppendLine("</script>");
                                }
                            }
                            // Application Grids
                            pathModules  = Path.Combine(pathSite, @"source\Grids");
                            if (Directory.Exists(pathModules))
                            {
                                pathModuleList = getModuleDirectories(pathModules);
                                foreach (string pathModule in pathModuleList)
                                {
                                    nameModule = new DirectoryInfo(pathModule).Name;
                                    pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                    fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                    if (Publish)
                                    {
                                        sbModules.AppendLine("<script id=\"tmpl-grids-" + nameModule + "Browse\" type=\"text/html\">");
                                        sbModules.AppendLine(fileBrowseTemplate);
                                        sbModules.AppendLine("</script>");
                                    }
                                    else
                                    {
                                        string urlHtml = "{{AppUri}}Source/Grids/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                        urlHtml = ApplyFields(urlHtml, config, Publish);
                                        sbModules.AppendLine("<script id=\"tmpl-grids-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                    }
                                }
                            }
                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                html_outputFileText[targetfileno].Replace("{{{Grids}}}", sbModules.ToString());
                            }
                            #endregion

                            #region replace: {{{Validations}}}
                            // Validations
                            sbModules = new StringBuilder();
                            // Fw Validations
                            pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Validations");
                            if (Directory.Exists(pathModules))
                            {
                                pathModuleList = getModuleDirectories(pathModules);
                                foreach (string pathModule in pathModuleList)
                                {
                                    nameModule = new DirectoryInfo(pathModule).Name;
                                    pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                    fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                    sbModules.AppendLine("<script id=\"tmpl-validations-" + nameModule + "Browse\" type=\"text/html\">");
                                    sbModules.AppendLine(fileBrowseTemplate);
                                    sbModules.AppendLine("</script>");
                                }
                            }
                            // Application Validations
                            pathModules  = Path.Combine(pathSite, @"source\Validations");
                            if (Directory.Exists(pathModules))
                            {
                                pathModuleList = getModuleDirectories(pathModules);
                                foreach (string pathModule in pathModuleList)
                                {
                                    nameModule = new DirectoryInfo(pathModule).Name;
                                    pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                    fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                    if (Publish)
                                    {
                                        sbModules.AppendLine("<script id=\"tmpl-validations-" + nameModule + "Browse\" type=\"text/html\">");
                                        sbModules.AppendLine(fileBrowseTemplate);
                                        sbModules.AppendLine("</script>");
                                    }
                                    else
                                    {
                                        string urlHtml = "{{AppUri}}Source/Validations/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                        urlHtml = ApplyFields(urlHtml, config, Publish);
                                        sbModules.AppendLine("<script id=\"tmpl-validations-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                    }
                                }
                            }
                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                html_outputFileText[targetfileno].Replace("{{{Validations}}}", sbModules.ToString());
                            }
                            #endregion

                            #region replace: {{{Reports}}}
                            // Reports
                            bool buildreports = false;
                            for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                            {
                                if (html_outputFileText[targetfileno].ToString().Contains("{{{Reports}}}"))
                                {
                                    buildreports = true;
                                }
                            }
                            if (buildreports)
                            {
                                sbModules = new StringBuilder();
                                // Fw Reports
                                pathModules  = Path.Combine(pathSite, @"libraries\Fw\source\Reports");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        string reportFrontEndPath, reportFrontEndText, reportStyleSheetPath, reportStyleSheetText, reportHeaderPath, reportHeaderText,
                                            reportBodyPath, reportBodyText, reportFooterPath, reportFooterText;

                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        if (nameModule != "Application")
                                        {
                                            reportFrontEndPath = Path.Combine(pathModule, "FrontEnd.htm");
                                            reportFrontEndText = File.ReadAllText(reportFrontEndPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "FrontEnd\" type=\"text/html\">");
                                            sbModules.AppendLine(reportFrontEndText);
                                            sbModules.AppendLine("</script>");

                                            reportStyleSheetPath = Path.Combine(pathModule, "StyleSheet.css");
                                            reportStyleSheetText = File.ReadAllText(reportStyleSheetPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "StyleSheet\" type=\"text/css\">");
                                            sbModules.AppendLine(reportStyleSheetText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportHeaderPath = Path.Combine(pathModule, "Header.htm");
                                            reportHeaderText = File.ReadAllText(reportHeaderPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Header\" type=\"text/html\">");
                                            sbModules.AppendLine(reportHeaderText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportBodyPath = Path.Combine(pathModule, "Body.htm");
                                            reportBodyText = File.ReadAllText(reportBodyPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Body\" type=\"text/html\">");
                                            sbModules.AppendLine(reportBodyText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportFooterPath = Path.Combine(pathModule, "Footer.htm");
                                            reportFooterText = File.ReadAllText(reportFooterPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Footer\" type=\"text/html\">");
                                            sbModules.AppendLine(reportFooterText);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                // Application Reports
                                pathModules  = Path.Combine(pathSite, @"source\Reports");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        string reportFrontEndPath, reportFrontEndText, reportStyleSheetPath, reportStyleSheetText, reportHeaderPath, reportHeaderText,
                                            reportBodyPath, reportBodyText, reportFooterPath, reportFooterText;

                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        if (nameModule != "Application")
                                        {
                                            reportFrontEndPath = Path.Combine(pathModule, "FrontEnd.htm");
                                            reportFrontEndText = File.ReadAllText(reportFrontEndPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "FrontEnd\" type=\"text/html\">");
                                            sbModules.AppendLine(reportFrontEndText);
                                            sbModules.AppendLine("</script>");

                                            reportStyleSheetPath = Path.Combine(pathModule, "StyleSheet.css");
                                            reportStyleSheetText = File.ReadAllText(reportStyleSheetPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "StyleSheet\" type=\"text/css\">");
                                            sbModules.AppendLine(reportStyleSheetText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportHeaderPath = Path.Combine(pathModule, "Header.htm");
                                            reportHeaderText = File.ReadAllText(reportHeaderPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Header\" type=\"text/html\">");
                                            sbModules.AppendLine(reportHeaderText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportBodyPath = Path.Combine(pathModule, "Body.htm");
                                            reportBodyText = File.ReadAllText(reportBodyPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Body\" type=\"text/html\">");
                                            sbModules.AppendLine(reportBodyText);
                                            sbModules.AppendLine("</script>");
                                    
                                            reportFooterPath = Path.Combine(pathModule, "Footer.htm");
                                            reportFooterText = File.ReadAllText(reportFooterPath);
                                            sbModules.AppendLine("<script id=\"tmpl-reports-" + nameModule + "Footer\" type=\"text/html\">");
                                            sbModules.AppendLine(reportFooterText);
                                            sbModules.AppendLine("</script>");
                                        }
                                    }
                                }
                                for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                {
                                    html_outputFileText[targetfileno].Replace("{{{Reports}}}", sbModules.ToString());
                                }
                            }
                            #endregion
                                
                            // Source Files
                            #region replace: {{{javascripts}}}
                            foreach (SourceFile sourceFile in config.SourceFiles)
                            {
                                if (sourceFile.ReplaceField == "{{{javascripts}}}")
                                {
                                    List<string> jsFiles;
                                    string[] newInputFiles;
                                    string fieldKey, jspath;
                                    string fwpath = Path.Combine(pathSite, @"libraries\Fw\");

                                    jsFiles = new List<string>();

                                    jspath = Path.Combine(pathSite, @"libraries\Fw\source");
                                    if (Directory.Exists(jspath))
                                    {
                                        newInputFiles = Directory.GetFiles(jspath, "*.js", SearchOption.AllDirectories);
                                        for (int jsfileno = 0; jsfileno < newInputFiles.Length; jsfileno++)
                                        {
                                            newInputFiles[jsfileno] = "{{FwFrontEndLibraryUri}}" + newInputFiles[jsfileno].Substring(fwpath.Length + 1);
                                        }
                                        jsFiles.AddRange(newInputFiles);
                                    }
                                    if (Directory.Exists(Path.Combine(pathSite, @"source")))
                                    {
                                        newInputFiles = Directory.GetFiles(Path.Combine(pathSite, @"source"), "*.js", SearchOption.AllDirectories);
                                        for (int jsfileno = 0; jsfileno < newInputFiles.Length; jsfileno++)
                                        {
                                            newInputFiles[jsfileno] = "{{AppUri}}" + newInputFiles[jsfileno].Substring(pathSite.Length + 1);
                                        }
                                        jsFiles.AddRange(newInputFiles);
                                    }
                                    if (jsFiles.Count > 0)
                                    {
                                        for (int uriNo = 0; uriNo < jsFiles.Count; uriNo++)
                                        {
                                            foreach (Field field in config.Fields)
                                            {
                                                if (jsFiles[uriNo].StartsWith(field.Value))
                                                {
                                                    if (this.Publish == field.Publish)
                                                    {
                                                        fieldKey = field.Key.Replace("{{FwFrontEndLibraryPath}}", "{{FwFrontEndLibraryUri}}")
                                                                                .Replace("{{AppPath}}", "{{AppUri}}");
                                                        jsFiles[uriNo] = jsFiles[uriNo].Replace(field.Value + "\\", fieldKey); 
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    for (int inputFileNo = 0; inputFileNo < sourceFile.InputFiles.Count; inputFileNo++)
                                    {
                                        string inputFile = sourceFile.InputFiles[inputFileNo];
                                        if (inputFile == "[source_js]")
                                        {
                                            sourceFile.InputFiles.RemoveAt(inputFileNo);
                                            sourceFile.InputFiles.InsertRange(inputFileNo, jsFiles);
                                            break;
                                        }
                                    }
                                }

                                if (sourceFile.ReplaceField == "{{{stylesheets}}}")
                                {
                                    List<string> cssFiles;
                                    string[] newInputFiles;
                                    string fieldKey, csspath;

                                    cssFiles = new List<string>();
                                    
                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\Grids");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\Modules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\Pages");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\Reports");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "FrontEnd.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\SubModules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries\Fw\source\Controls\Modules\Validations");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Controls");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Grids");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Modules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Pages");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Reports");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "FrontEnd.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\SubModules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source\Validations");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories);    
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    for (int cssfileno = 0; cssfileno < cssFiles.Count; cssfileno++)
                                    {
                                        cssFiles[cssfileno] = "{{AppUri}}" + cssFiles[cssfileno].Substring(pathSite.Length + 1);
                                    }

                                    if (cssFiles.Count > 0)
                                    {
                                        for (int uriNo = 0; uriNo < cssFiles.Count; uriNo++)
                                        {
                                            foreach (Field field in config.Fields)
                                            {
                                                if (cssFiles[uriNo].StartsWith(field.Value))
                                                {
                                                    if (this.Publish == field.Publish)
                                                    {
                                                        fieldKey = field.Key.Replace("{{FwFrontEndLibraryPath}}", "{{FwFrontEndLibraryUri}}")
                                                                                .Replace("{{AppPath}}", "{{AppUri}}");
                                                        cssFiles[uriNo] = cssFiles[uriNo].Replace(field.Value + "\\", fieldKey); 
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    for (int inputFileNo = 0; inputFileNo < sourceFile.InputFiles.Count; inputFileNo++)
                                    {
                                        string inputFile = sourceFile.InputFiles[inputFileNo];
                                        if (inputFile == "[source_css]")
                                        {
                                            sourceFile.InputFiles.RemoveAt(inputFileNo);
                                            sourceFile.InputFiles.InsertRange(inputFileNo, cssFiles);
                                            break;
                                        }
                                    }
                                }
                            }
                            #endregion


                            // write app_scripts.json
                            bool writeAppScriptsJson = false;
                            StringBuilder appScriptsJson = new StringBuilder();
                            appScriptsJson.AppendLine("{");
                            appScriptsJson.AppendLine("    \"scripts\":  [");
                            for (int j = 0; j < config.SourceFiles.Count; j++)
                            {
                                #region create file: app_scripts.json
                                // create app_scripts.json
                                if (!this.Publish)
                                {
                                    sourceFiles_outputFile = config.SourceFiles[j].OutputFile;
                                    if (sourceFiles_outputFile.EndsWith(".js") && config.SourceFiles[j].InputFiles.Count > 0)
                                    {
                                        for (int k = 0; k < config.SourceFiles[j].InputFiles.Count; k++)
                                        {
                                            sourceFile_inputFileUri = config.SourceFiles[j].InputFiles[k]
                                                .Replace(@"\", @"/")
                                                .Replace("{{Version}}", Version)
                                                .Replace("{{FwFrontEndLibraryUri}}", "[appbaseurl][fwvirtualdirectory]")
                                                .Replace("{{AppUri}}", "[appbaseurl][appvirtualdirectory]");
                                            sourceFile_inputFileUri = ApplyFields(sourceFile_inputFileUri, config, config.Targets[i].Publish);
                                            if (k > 0 || writeAppScriptsJson) appScriptsJson.Append(",");
                                            appScriptsJson.AppendLine("{\"src\": \"" + sourceFile_inputFileUri + "\"}");
                                        }
                                        writeAppScriptsJson = true;
                                    }
                                }
                                else
                                {
                                    // create app_scripts.json
                                    sourceFiles_outputFile = config.SourceFiles[j].OutputFile;
                                    if (sourceFiles_outputFile.EndsWith(".js"))
                                    {
                                        string scriptFilename = config.SourceFiles[j].OutputFile.Replace("{{Version}}", Version);
                                        if (config.SourceFiles[j].Minify)
                                        {
                                            scriptFilename = config.SourceFiles[j].MinifiedFile.Replace("{{Version}}", Version);
                                        }
                                        if (writeAppScriptsJson) appScriptsJson.Append(",");
                                        appScriptsJson.AppendLine("{\"src\": \"[appbaseurl][appvirtualdirectory]" + scriptFilename + "\"}");
                                        writeAppScriptsJson = true;
                                    }
                                }
                                #endregion

                                sourceFiles = new StringBuilder();
                                if (!config.Targets[i].Publish)
                                {
                                    #region create file: debug version of source file
                                    string replacefield = config.SourceFiles[j].ReplaceField; // for conditional breakpoints like: replacefield == "{{{stylesheets}}}"
                                    for (int k = 0; k < config.SourceFiles[j].InputFiles.Count; k++)
                                    {
                                        string inputFileUri         = ApplyFields(config.SourceFiles[j].InputFiles[k], config, config.Targets[i].Publish);
                                        string inputFileTagTemplate = ApplyFields(config.SourceFiles[j].Template,      config, config.Targets[i].Publish)
                                            .Replace("{{File}}", inputFileUri)
                                            .Replace("{{Version}}", Version);
                                        sourceFiles.AppendLine(inputFileTagTemplate);
                                        sourceFiles.Replace(@"\", @"/");
                                    }
                                    for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                    {
                                        html_outputFileText[targetfileno].Replace(config.SourceFiles[j].ReplaceField, sourceFiles.ToString());
                                        if (File.Exists(html_outputFilePath[targetfileno]))
                                        {
                                            File.Delete(html_outputFilePath[targetfileno]);
                                        }
                                        File.WriteAllText(html_outputFilePath[targetfileno], NormalizeLineEndings(html_outputFileText[targetfileno].ToString()));
                                    }
                                    #endregion

                                    #region create file: intellisense.js
                                    // create a js file for intellisense.js
                                    //sourceFiles_outputFile   = build.SourceFiles[j].OutputFile.Replace("-{{Version}}.debug", string.Empty);
                                    //if (sourceFiles_outputFile.Equals("script.js"))
                                    //{
                                    //    sourceFiles_outputFile = "intellisense.js";
                                    //    sourceFiles_outputFile = Path.Combine(build.Targets[i].OutputDirectory, sourceFiles_outputFile);
                                    //    sourceFiles_combinedOutputPath = GetAbsolutePath(sourceFiles_outputFile);
                                    //    sourceFiles_combinedText = new StringBuilder();
                                    //    for (int k = 0; k < build.SourceFiles[j].InputFiles.Count; k++)
                                    //    {
                                    //        sourceFile_inputFileUri = ApplyFields(build.SourceFiles[j].InputFiles[k], build, true);
                                    //        sourceFile_inputFileText = UriToString(sourceFile_inputFileUri);
                                    //        sourceFiles_combinedText.AppendLine(sourceFile_inputFileText);
                                    //    }
                                    //    if (File.Exists(sourceFiles_combinedOutputPath))
                                    //    {
                                    //        File.Delete(sourceFiles_combinedOutputPath);
                                    //    }
                                    //    File.WriteAllText(sourceFiles_combinedOutputPath, NormalizeLineEndings(sourceFiles_combinedText.ToString()));
                                    //}
                                    #endregion
                                }
                                else  // Publish is true
                                {
                                    #region create file: release verson of source file
                                    sourceFiles_outputFile   = Path.Combine(config.Targets[i].OutputDirectory, config.SourceFiles[j].OutputFile.Replace("{{Version}}", Version));
                                    sourceFiles_minifiedFile = Path.Combine(config.Targets[i].OutputDirectory, config.SourceFiles[j].MinifiedFile.Replace("{{Version}}", Version));
                                    sourceFiles_combinedOutputPath = GetAbsolutePath(sourceFiles_outputFile);
                                    sourceFiles_combinedText = new StringBuilder();
                                    for (int k = 0; k < config.SourceFiles[j].InputFiles.Count; k++)
                                    {
                                        sourceFile_inputFileUri = ApplyFields(config.SourceFiles[j].InputFiles[k], config, config.Targets[i].Publish);
                                        sourceFile_inputFileText = UriToString(sourceFile_inputFileUri, this);
                                        sourceFiles_combinedText.AppendLine(sourceFile_inputFileText);
                                    }
                                    if (File.Exists(sourceFiles_combinedOutputPath))
                                    {
                                        File.Delete(sourceFiles_combinedOutputPath);
                                    }
                                    new FileInfo(sourceFiles_combinedOutputPath).Directory.Create();
                                    File.WriteAllText(sourceFiles_combinedOutputPath, NormalizeLineEndings(sourceFiles_combinedText.ToString()));
                                    if ((config.Targets[i].Minify) && (config.SourceFiles[j].Minify))
                                    {
                                        string sourceFileInclude = string.Empty;
                                        if (config.Targets[i].AddBaseUrlToSourceFiles)
                                        {
                                            sourceFileInclude += "[appbaseurl][appvirtualdirectory]";
                                        }
                                        sourceFileInclude += config.SourceFiles[j].MinifiedFile.Replace("{{Version}}", Version);
                                        sourceFileInclude = sourceFileInclude.Replace("{{Version}}", Version);
                                        sourceFileInclude = sourceFileInclude.Replace(@"\", @"/");
                                        sourceFiles.AppendLine(config.SourceFiles[j].Template.Replace("{{File}}", sourceFileInclude));
                                        if (File.Exists(sourceFiles_minifiedFile))
                                        {
                                            File.Delete(sourceFiles_minifiedFile);
                                        }
                                            
                                        if (Path.GetExtension(sourceFiles_minifiedFile) == ".js")
                                        {
                                            commandLine = new CommandLineBuilder();
                                            //commandLine.AppendSwitchIfNotNull("-jar ", Path.Combine(fwPath, @"lib\Fw\Build\Fw.MsBuild\yuicompressor-2.4.7.jar"));
                                            //commandLine.AppendSwitchIfNotNull("-o ", sourceFiles_minifiedFile);
                                            //commandLine.AppendFileNameIfNotNull(sourceFiles_combinedOutputPath);
                                            commandLine.AppendSwitchIfNotNull("-jar ", Path.Combine(solutionDir, @"lib\Fw\Build\Fw.MsBuild\closure-compiler\compiler.jar"));
                                            commandLine.AppendSwitchIfNotNull("--js_output_file=", sourceFiles_minifiedFile);
                                            commandLine.AppendSwitchIfNotNull("--js ", sourceFiles_combinedOutputPath);
                                            //commandLine.AppendSwitchIfNotNull("--compilation_level ", "ADVANCED_OPTIMIZATIONS");
                                            javaPath = GetJavaInstallationPath();
                                            if (string.IsNullOrEmpty(javaPath))
                                            {
                                                throw new Exception("Can't find java.exe");
                                            }
                                            else
                                            {
                                                javaPath += @"\bin\java.exe";
                                            }

                                            using (Process process = new Process())
                                            {
                                                int timeout = 5 * 60 * 1000; // 5 minutes
                                                process.StartInfo.FileName               = javaPath;
                                                process.StartInfo.Arguments              = commandLine.ToString();
                                                process.StartInfo.UseShellExecute        = false;
                                                process.StartInfo.CreateNoWindow         = true;
                                                process.StartInfo.RedirectStandardError  = true;
                                                process.StartInfo.RedirectStandardOutput = true;
                                                
                                                StringBuilder output = new StringBuilder();
                                                StringBuilder error = new StringBuilder();

                                                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                                                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                                                {
                                                    process.OutputDataReceived += (sender, e) => {
                                                        try
                                                        {
                                                            if (e.Data == null)
                                                            {
                                                                outputWaitHandle.Set();
                                                            }
                                                            else
                                                            {
                                                                output.AppendLine(e.Data);
                                                            }
                                                        }
                                                        catch(ObjectDisposedException)
                                                        {

                                                        }
                                                    };
                                                    process.ErrorDataReceived += (sender, e) =>
                                                    {
                                                        try
                                                        {
                                                            if (e.Data == null)
                                                            {
                                                                errorWaitHandle.Set();
                                                            }
                                                            else
                                                            {
                                                                error.AppendLine(e.Data);
                                                            }
                                                        }
                                                        catch (ObjectDisposedException)
                                                        {

                                                        }
                                                    };

                                                    process.Start();

                                                    process.BeginOutputReadLine();
                                                    process.BeginErrorReadLine();

                                                    if (process.WaitForExit(timeout) &&
                                                        outputWaitHandle.WaitOne(timeout) &&
                                                        errorWaitHandle.WaitOne(timeout))
                                                    {
                                                        // Process completed. Check process.ExitCode here.
                                                        if (this.BuildEngine.GetType() != typeof(VirtualBuildEngine))
                                                        {
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                            this.Log.LogMessage(MessageImportance.High, "closer compiler standard output stream:");
                                                            this.Log.LogMessage(MessageImportance.High, output.ToString());
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                            this.Log.LogMessage(MessageImportance.High, "closure compiler standard err stream:");
                                                            this.Log.LogWarning(error.ToString());
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                        }
                                                        else
                                                        {
                                                            if (output.Length > 0)
                                                            {
                                                                Console.Out.WriteLine();
                                                                Console.Out.WriteLine("--------");
                                                                Console.Out.WriteLine("closer compiler standard output stream:");
                                                                Console.Out.WriteLine(output.ToString());
                                                                Console.Out.WriteLine("--------");
                                                                Console.Out.WriteLine();

                                                            }
                                                            if (error.Length > 0)
                                                            {
                                                                if (error.ToString().Contains("0 error(s)"))
                                                                {
                                                                    Console.Out.WriteLine();
                                                                    Console.Out.WriteLine("--------");
                                                                    Console.Out.WriteLine("closure compiler standard err stream:");
                                                                    Console.Out.WriteLine(error.ToString());
                                                                    Console.Out.WriteLine("--------");
                                                                    Console.Out.WriteLine();
                                                                }
                                                                else
                                                                {
                                                                    Environment.ExitCode = -1;
                                                                    Console.Error.WriteLine();
                                                                    Console.Error.WriteLine("--------");
                                                                    Console.Error.WriteLine("closure compiler standard err stream:");
                                                                    Console.Error.WriteLine(error.ToString());
                                                                    Console.Error.WriteLine("--------");
                                                                    Console.Error.WriteLine();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Timed out.
                                                        if (this.BuildEngine.GetType() != typeof(VirtualBuildEngine))
                                                        {
                                                            this.Log.LogError("Timed out trying to run closure compiler.");

                                                        }
                                                        else
                                                        {
                                                            Console.Error.WriteLine("Timed out trying to run closure compiler.");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (Path.GetExtension(sourceFiles_minifiedFile) == ".css")
                                        {
                                            commandLine = new CommandLineBuilder();
                                            commandLine.AppendSwitchIfNotNull("-jar ", Path.Combine(solutionDir, @"lib\Fw\Build\Fw.MsBuild\yuicompressor-2.4.7.jar"));
                                            commandLine.AppendSwitchIfNotNull("-o ", sourceFiles_minifiedFile);
                                            commandLine.AppendFileNameIfNotNull(sourceFiles_combinedOutputPath);
                                            javaPath = GetJavaInstallationPath();
                                            if (string.IsNullOrEmpty(javaPath))
                                            {
                                                throw new Exception("Can't find java.exe");
                                            }
                                            else
                                            {
                                                javaPath += @"\bin\java.exe";
                                            }
                                            
                                            using (Process process = new Process())
                                            {
                                                int timeout = 30 * 1000; // 30 seconds
                                                process.StartInfo.FileName               = javaPath;
                                                process.StartInfo.Arguments              = commandLine.ToString();
                                                process.StartInfo.UseShellExecute        = false;
                                                process.StartInfo.CreateNoWindow         = true;
                                                process.StartInfo.RedirectStandardError  = true;
                                                process.StartInfo.RedirectStandardOutput = true;
                                                process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                {
                                                    Console.Out.WriteLine(e.Data);
                                                };
                                                process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
                                                {
                                                    Console.Error.WriteLine(e.Data);
                                                };

                                                StringBuilder output = new StringBuilder();
                                                StringBuilder error = new StringBuilder();

                                                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                                                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                                                {
                                                    process.OutputDataReceived += (sender, e) => {
                                                        if (e.Data == null)
                                                        {
                                                            outputWaitHandle.Set();
                                                        }
                                                        else
                                                        {
                                                            output.AppendLine(e.Data);
                                                        }
                                                    };
                                                    process.ErrorDataReceived += (sender, e) =>
                                                    {
                                                        if (e.Data == null)
                                                        {
                                                            errorWaitHandle.Set();
                                                        }
                                                        else
                                                        {
                                                            error.AppendLine(e.Data);
                                                        }
                                                    };

                                                    process.Start();

                                                    process.BeginOutputReadLine();
                                                    process.BeginErrorReadLine();

                                                    if (process.WaitForExit(timeout) &&
                                                        outputWaitHandle.WaitOne(timeout) &&
                                                        errorWaitHandle.WaitOne(timeout))
                                                    {
                                                        // Process completed. Check process.ExitCode here.
                                                        if (this.BuildEngine.GetType() != typeof(VirtualBuildEngine))
                                                        {
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                            this.Log.LogMessage(MessageImportance.High, "yui standard output stream:");
                                                            this.Log.LogMessage(MessageImportance.High, output.ToString());
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                            this.Log.LogMessage(MessageImportance.High, "yui standard err stream:");
                                                            this.Log.LogMessage(MessageImportance.High, error.ToString());
                                                            this.Log.LogMessage(MessageImportance.High, "--------");
                                                        } 
                                                        else
                                                        {
                                                            if (output.Length > 0)
                                                            {
                                                                Console.Out.WriteLine();
                                                                Console.Out.WriteLine("--------");
                                                                Console.Out.WriteLine("yui standard output stream:");
                                                                Console.Out.WriteLine(output.ToString());
                                                                Console.Out.WriteLine("--------");
                                                                Console.Out.WriteLine();

                                                            }
                                                            if (error.Length > 0)
                                                            {
                                                                if (error.ToString().Contains("0 error(s)"))
                                                                {
                                                                    Console.Out.WriteLine();
                                                                    Console.Out.WriteLine("--------");
                                                                    Console.Out.WriteLine("yui standard err stream:");
                                                                    Console.Out.WriteLine(error.ToString());
                                                                    Console.Out.WriteLine("--------");
                                                                    Console.Out.WriteLine();
                                                                }
                                                                else
                                                                {
                                                                    Environment.ExitCode = -1;
                                                                    Console.Error.WriteLine();
                                                                    Console.Error.WriteLine("--------");
                                                                    Console.Error.WriteLine("yui standard err stream:");
                                                                    Console.Error.WriteLine(error.ToString());
                                                                    Console.Error.WriteLine("--------");
                                                                    Console.Error.WriteLine();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Timed out.
                                                        if (this.BuildEngine.GetType() != typeof(VirtualBuildEngine))
                                                        {
                                                            this.Log.LogError("Timed out trying to run closure compiler.");
                                                        }
                                                        else
                                                        {
                                                            Console.Error.WriteLine("Timed out trying to run closure compiler.");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else // if the source file is not being minified
                                    {
                                        sourceFiles.AppendLine(config.SourceFiles[j].Template.
                                            Replace("{{File}}", "[appbaseurl][appvirtualdirectory]" + config.SourceFiles[j].OutputFile.
                                            Replace("{{Version}}", Version)));
                                    }
                                    for (int targetfileno = 0; targetfileno < config.Targets[i].Files.Count; targetfileno++)
                                    {
                                        html_outputFileText[targetfileno].Replace(config.SourceFiles[j].ReplaceField, sourceFiles.ToString());
                                        if (File.Exists(html_outputFilePath[targetfileno]))
                                        {
                                            File.Delete(html_outputFilePath[targetfileno]);
                                        }
                                        File.WriteAllText(html_outputFilePath[targetfileno], NormalizeLineEndings(html_outputFileText[targetfileno].ToString()));
                                    }
                                    #endregion
                                }
                            }
                            if (writeAppScriptsJson)
                            {
                                appScriptsJson.AppendLine("    ]");
                                appScriptsJson.AppendLine("}");
                                string appScripsJsonPath = GetAbsolutePath(Path.Combine(config.Targets[i].OutputDirectory, "app_scripts.json"));
                                if (File.Exists(appScripsJsonPath))
                                {
                                    File.Delete(appScripsJsonPath);
                                }
                                File.WriteAllText(appScripsJsonPath, NormalizeLineEndings(appScriptsJson.ToString()));
                            }
                        }
                    }
                }
            }
            //catch (MinifiedException ex)
            //{
            //    success = false;
            //}
            //catch (Exception ex)
            //{
            //    success = false;
            //    if (this.BuildEngine != null)
            //    {
            //        this.Log.LogErrorFromException(ex, true);
            //        while(ex.InnerException != null)
            //        {
            //            ex = ex.InnerException;
            //            this.Log.LogErrorFromException(ex, true);
            //        }
            //    }
            //    else
            //    {
            //        throw;
            //        //System.Diagnostics.Debugger.Launch();
            //    }
            //}
            finally
            {
            }

            return success;
        }

        //---------------------------------------------------------------------------------------------
        static string GetJavaInstallationPath()
        {
            string environmentPath = Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            const string JAVA_KEY = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";

            var localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);
            using (var rk = localKey.OpenSubKey(JAVA_KEY))
            {
                if (rk != null)
                {
                    string currentVersion = rk.GetValue("CurrentVersion").ToString();
                    using (var key = rk.OpenSubKey(currentVersion))
                    {
                        return key.GetValue("JavaHome").ToString();
                    }
                }
            }

            localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            using (var rk = localKey.OpenSubKey(JAVA_KEY))
            {
                if (rk != null)
                {
                    string currentVersion = rk.GetValue("CurrentVersion").ToString();
                    using (var key = rk.OpenSubKey(currentVersion))
                    {
                        return key.GetValue("JavaHome").ToString();
                    }
                }
            }

            return null;
        }
        //---------------------------------------------------------------------------------------------
        private void BuildFwReferencesJsFile(string solutionDir, string pathSite)
        {
            // create the Fw.Json _references.js file
            // when you open a fw script, it will intellisense against all the fw scripts
            string fwjsoncontentpath = Path.Combine(pathSite, @"libraries\Fw\");
            if (Directory.Exists(fwjsoncontentpath))
            {
                string[] fwscripts = Directory.GetFiles(fwjsoncontentpath, "*.js", SearchOption.AllDirectories);
                string[] fwexcludescripts = File.ReadAllLines(Path.Combine(solutionDir, @"lib\Fw\src\Fw.Json\scripts\_referencesToExclude.txt"));
                StringBuilder fwreferencesfile = new StringBuilder();
                for (int refno = 0; refno < fwscripts.Length; refno++)
                {
                    bool include = true;
                    for (int exclno = 0; exclno < fwexcludescripts.Length; exclno++)
                    {
                        if (fwexcludescripts[exclno] == fwscripts[refno])
                        {
                            include = false;
                        }
                    }
                    if (include)
                    {
                        fwreferencesfile.AppendLine("/// <reference path=\"" + fwscripts[refno] + "\" />");
                    }
                }
                string fwreferencespath = Path.Combine(solutionDir, @"lib\Fw\src\Fw.Json\scripts\_references.js");
                bool writefwreferencesfile = true;
                if (File.Exists(fwreferencespath))
                {
                    string existingfwreferencesfile = File.ReadAllText(fwreferencespath);
                    if (existingfwreferencesfile == fwreferencesfile.ToString())
                    {
                        writefwreferencesfile = false;
                    }
                }
                if (writefwreferencesfile)
                {
                    File.WriteAllText(fwreferencespath, fwreferencesfile.ToString());
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void BuildAppReferencesJsFile(JSAppBuilderConfig config)
        {
            // create the App _references.js file
            // when you open an app script it will intellisense against the scripts in JSAppBuilder.config 
            string fwpath  = string.Empty;
            string apppath = string.Empty;
            List<string> jsfiles = null;
            for (int fieldno = 0; fieldno < config.Fields.Count; fieldno++)
            {
                if (config.Fields[fieldno].Key == "{{FwFrontEndLibraryPath}}")
                {
                    fwpath = config.Fields[fieldno].Value + @"\";
                }
                if (config.Fields[fieldno].Key == "{{AppPath}}")
                {
                    apppath = config.Fields[fieldno].Value + @"\";
                }
            }
            if (!string.IsNullOrEmpty(fwpath) && !string.IsNullOrEmpty(apppath))
            {
                StringBuilder appreferencesfile = new StringBuilder();
                for (int sourcefileno = 0; sourcefileno < config.SourceFiles.Count; sourcefileno++)
                {
                    // find the first output file that ends with .js and we will create the references against the input files for that
                    if (config.SourceFiles[sourcefileno].OutputFile.EndsWith(".js"))
                    {
                        jsfiles = new List<string>(config.SourceFiles[sourcefileno].InputFiles);
                        break;
                    }
                }
                if (jsfiles != null)
                {
                    string appsourcepath = apppath + @"Source\";
                    if (Directory.Exists(appsourcepath))
                    {
                        string[] jssourcefiles = Directory.GetFiles(appsourcepath, "*.js", SearchOption.AllDirectories);
                        int sourcefilesindex = jsfiles.IndexOf("[source_js]");
                        if (sourcefilesindex >= 0)
                        {
                            jsfiles.RemoveAt(sourcefilesindex);
                            jsfiles.InsertRange(sourcefilesindex, jssourcefiles);
                        }
                    }
                    for (int jsfileno = 0; jsfileno < jsfiles.Count; jsfileno++)
                    {
                        appreferencesfile.AppendLine("/// <reference path=\"" + jsfiles[jsfileno].Replace(@"/", @"\") + "\" />" );
                        appreferencesfile.Replace("{{FwFrontEndLibraryUri}}", fwpath);
                        appreferencesfile.Replace("{{AppUri}}", apppath);
                    }
                    string appreferencespath = apppath + @"scripts\_references.js";
                    bool writeappreferencesfile = true;
                    if (File.Exists(appreferencespath))
                    {
                        string existingappreferencesfile = File.ReadAllText(appreferencespath);
                        if (existingappreferencesfile == appreferencesfile.ToString())
                        {
                            writeappreferencesfile = false;
                        }
                    }
                    if (writeappreferencesfile)
                    {
                        File.WriteAllText(appreferencespath, appreferencesfile.ToString());
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public class MinifiedException : Exception
        {

        }
        //---------------------------------------------------------------------------------------------
        private List<string> getModuleDirectories(string pathModules)
        {
            string[] pathModuleFileArray;
            List<string> modules;

            modules = new List<string>();
            pathModuleFileArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories);
            foreach (string pathModuleFile in pathModuleFileArray)
            {
                string pathModule = Path.GetDirectoryName(pathModuleFile);
                if (!modules.Contains(pathModule))
                {
                    modules.Add(pathModule);
                }
            }

            return modules;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Module GetModuleSchema(string moduleName, string browseTemplate, string formTemplate, DatabaseConnection databaseConnection)
        {
            XmlDocument xmlDoc;
            FwApplicationSchema.Module moduleSchema = null;
            FwApplicationSchema.Browse browseSchema = null;
            FwApplicationSchema.Form formSchema = null;

            try
            {
                if (browseTemplate != null)
                {
                    browseSchema = GetBrowseSchema(databaseConnection, "Module", moduleName, browseTemplate);
                }
                if (formTemplate != null)
                {
                    formSchema   = GetFormSchema(databaseConnection, "Module", moduleName, formTemplate);
                }
                moduleSchema = new FwApplicationSchema.Module(moduleName, browseSchema, formSchema);
            }
            catch (Exception ex)
            {
                Exception newException;
                newException = new Exception("Exception occured when loading schema for Module: " + moduleName + ".  Check the template for errors.", ex);
                this.Log.LogErrorFromException(newException, true);
            }

            return moduleSchema;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Grid GetGridSchema(string gridName, string browseTemplate, DatabaseConnection databaseConnection)
        {
            XmlDocument xmlDoc;
            FwApplicationSchema.Grid gridSchema=null;
            FwApplicationSchema.Browse browseSchema;
            FwApplicationSchema.Form formSchema;

            try
            {
                browseSchema = GetBrowseSchema(databaseConnection, "Grid", gridName, browseTemplate);
                formSchema   = GetGridFormSchema(databaseConnection, "Grid", gridName, browseTemplate);
                gridSchema   = new FwApplicationSchema.Grid(gridName, browseSchema, formSchema);
            }
            catch (Exception ex)
            {
                Exception newException;
                newException = new Exception("Exception occured when loading schema for Grid: " + gridName + ".  Check the template for errors.", ex);
                this.Log.LogErrorFromException(newException, true);
            }

            return gridSchema;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Validation GetValidationSchema(string validationName, string browseTemplate, DatabaseConnection databaseConnection)
        {
            XmlDocument xmlDoc;
            FwApplicationSchema.Validation validationSchema = null;
            FwApplicationSchema.Browse browseSchema = null;

            try
            {
                browseSchema     = GetBrowseSchema(databaseConnection, "Validation", validationName, browseTemplate);
                validationSchema = new FwApplicationSchema.Validation(validationName, browseSchema);
            }
            catch (Exception ex)
            {
                Exception newException;
                newException = new Exception("Exception occured when loading schema for Validation: " + validationName + ".  Check the template for errors.", ex);
                this.Log.LogErrorFromException(newException, true);
            }

            return validationSchema;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Form GetFormSchema(DatabaseConnection databaseConnection, string componentType, string componentName, string formTemplate)
        {
            XmlDocument xmlDoc;
            XmlNodeList xmlForm, xmlUniqueIds, xmlColumns, xmlChargeField, xmlTabs, xmlTabPageFields, xmlTabPageGrids, xmlTabPages, xmlGrid, xmlTabPageFwCharges;
            FwApplicationSchema.Form formSchema=null;
            FwApplicationSchema.FormGrid gridSchema;
            FwApplicationSchema.Tab tabSchema;
            FwApplicationSchema.FormTable tableSchema;
            FwApplicationSchema.Column columnSchema;
            Dictionary<string, string> tabsByField, tabCaptionByTabPageId, tabsByGrid, tabsByFwCharge;
            bool isUniqueId, hasAudit;

            try
            {
                tabsByField           = new Dictionary<string,string>();
                tabCaptionByTabPageId = new Dictionary<string,string>();
                tabsByGrid            = new Dictionary<string,string>();
                tabsByFwCharge        = new Dictionary<string, string>();
                xmlDoc                = new XmlDocument();
                xmlDoc.LoadXml(formTemplate);
                xmlForm        = xmlDoc.SelectNodes("//div[@data-control='FwContainer' and @data-type='form']"); //xpath query
                xmlUniqueIds   = xmlDoc.SelectNodes("//div[@data-control='FwFormField' and @data-isuniqueid='true']"); //xpath query
                xmlColumns     = xmlDoc.SelectNodes("//div[@data-control='FwFormField' and (not(@data-isuniqueid) or @data-isuniqueid!='true')]"); //xpath query
                xmlChargeField = xmlDoc.SelectNodes("//div[@data-control='FwCharge']"); //xpath query
                xmlGrid        = xmlDoc.SelectNodes("//div[@data-control='FwGrid']"); //xpath query
                xmlTabs        = xmlDoc.SelectNodes("//div[@data-type='tab']"); //xpath query
                xmlTabPages    = xmlDoc.SelectNodes("//div[@data-type='tabpage']"); //xpath query
                
                if (xmlForm.Count == 0)
                {
                    throw new Exception("FwJSAppBuilder.GetFormSchema: No forms were found in " + componentType + ": '" + componentName + "'");
                }
                hasAudit = false;
                foreach(XmlNode formNode in xmlForm)
                {
                    hasAudit = (formNode.Attributes["data-hasaudit"] != null) ? formNode.Attributes["data-hasaudit"].Value.Equals("true") : false;
                    break;
                }

                formSchema   = new FwApplicationSchema.Form(databaseConnection.Name, new List<FwApplicationSchema.Tab>(),  new List<FwApplicationSchema.FormGrid>(), new Dictionary<string, FwApplicationSchema.FormTable>(), hasAudit);
                
                // load the form tab nodes
                foreach(XmlNode formTab in xmlTabs)
                {
                    string caption;

                    // add the tab to the schema
                    caption = (formTab.Attributes["data-caption"] != null) ? formTab.Attributes["data-caption"].Value : string.Empty;
                    tabSchema = new FwApplicationSchema.Tab(caption);
                    formSchema.Tabs.Add(tabSchema);
                    tabCaptionByTabPageId[formTab.Attributes["data-tabpageid"].Value] = caption;
                }

                if (formSchema.HasAudit)
                {
                    tabSchema = new FwApplicationSchema.Tab("Audit");
                    formSchema.Tabs.Add(tabSchema);
                }
                
                // index the tabs by datafield, so we can lookup what tab a field belongs to
                foreach(XmlNode tabPage in xmlTabPages)
                {
                    string caption, tabpageid;

                    tabpageid = (tabPage.Attributes["id"] != null) ? tabPage.Attributes["id"].Value : string.Empty;
                    caption = (tabCaptionByTabPageId.ContainsKey(tabpageid) ? tabCaptionByTabPageId[tabpageid] : string.Empty);
                    
                    xmlTabPageFields = xmlDoc.SelectNodes("//div[@data-type='tabpage'][@id='" + tabpageid + "']//div[@data-control='FwFormField' and (not(@data-isuniqueid) or @data-isuniqueid!='true')]"); //xpath query
                    foreach(XmlNode tabPageField in xmlTabPageFields)
                    {
                        string datafield;
                        datafield = (tabPageField.Attributes["data-datafield"] != null) ? tabPageField.Attributes["data-datafield"].Value : string.Empty;
                        tabsByField[datafield] = caption;
                    }
                }

                // index the tabs by FwCharge, so we can lookup what tab a FwCharge belongs to
                foreach(XmlNode tabPage in xmlTabPages)
                {
                    string caption, tabpageid;

                    tabpageid = (tabPage.Attributes["id"] != null) ? tabPage.Attributes["id"].Value : string.Empty;
                    caption = (tabCaptionByTabPageId.ContainsKey(tabpageid) ? tabCaptionByTabPageId[tabpageid] : string.Empty);
                    
                    xmlTabPageFwCharges = xmlDoc.SelectNodes("//div[@data-type='tabpage'][@id='" + tabpageid + "']//div[@data-control='FwCharge']"); //xpath query
                    foreach(XmlNode tabPageFwCharge in xmlTabPageFwCharges)
                    {
                        string datafield;
                        datafield = (tabPageFwCharge.Attributes["data-datafield"] != null) ? tabPageFwCharge.Attributes["data-datafield"].Value : string.Empty;
                        tabsByFwCharge[datafield] = caption;
                    }
                }

                // index the tabs by grid, so we can lookup what tab a grid belongs to
                foreach(XmlNode tabPage in xmlTabPages)
                {
                    string caption, tabpageid;

                    tabpageid = (tabPage.Attributes["id"] != null) ? tabPage.Attributes["id"].Value : string.Empty;
                    caption = (tabCaptionByTabPageId.ContainsKey(tabpageid) ? tabCaptionByTabPageId[tabpageid] : string.Empty);
                    
                    xmlTabPageGrids = xmlDoc.SelectNodes("//div[@data-type='tabpage'][@id='" + tabpageid + "']//div[@data-control='FwGrid']"); //xpath query
                    foreach(XmlNode tabPageGrid in xmlTabPageGrids)
                    {
                        string grid;
                        grid = (tabPageGrid.Attributes["data-grid"] != null) ? tabPageGrid.Attributes["data-grid"].Value : string.Empty;
                        tabsByGrid[grid] = caption;
                    }
                }

                // load the form Grid nodes
                foreach (XmlNode formGrid in xmlGrid) {
                    string grid, securityCaption, tabCaption;

                    // add the tab to the schema
                    grid            = (formGrid.Attributes["data-grid"]            != null) ? formGrid.Attributes["data-grid"].Value            : string.Empty;
                    securityCaption = (formGrid.Attributes["data-securitycaption"] != null) ? formGrid.Attributes["data-securitycaption"].Value : string.Empty;
                    tabCaption      = (tabsByGrid.ContainsKey(grid) ? tabsByGrid[grid] : string.Empty);
                    gridSchema = new FwApplicationSchema.FormGrid(grid, securityCaption, tabCaption);
                    formSchema.Grids.Add(gridSchema);
                }

                // load the form uniqueid nodes
                foreach (XmlNode formfieldNode in xmlUniqueIds)
                {
                    if ((formfieldNode.Attributes["data-datafield"] != null) && (!string.IsNullOrEmpty(formfieldNode.Attributes["data-datafield"].Value)))
                    {
                        string datafield, tableName=string.Empty, caption, columnName=string.Empty, validationName, validationDisplayField, dataType, tabCaption;
                        string[] datafieldFragments;
                        int saveOrder;
                        bool required, isIdentity, readOnly, noDuplicate, exportToExcel;

                        datafield              = (formfieldNode.Attributes["data-datafield"] != null) ? formfieldNode.Attributes["data-datafield"].Value : string.Empty;
                        caption                = (formfieldNode.Attributes["data-caption"] != null)   ? formfieldNode.Attributes["data-caption"].Value   : datafield;
                        dataType               = (formfieldNode.Attributes["data-type"]      != null) ? formfieldNode.Attributes["data-type"].Value      : string.Empty;
                        readOnly               = (formfieldNode.Attributes["data-readonly"]  != null) ? formfieldNode.Attributes["data-readonly"].Value.Equals("true") : false;
                        required               = true;
                        isIdentity             = false;
                        datafieldFragments     = datafield.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                        if (datafieldFragments.Length == 2)
                        {
                            tableName              = datafieldFragments[0];
                            columnName             = datafieldFragments[1];
                        }
                        else if (datafieldFragments.Length == 1)
                        {
                            tableName              = string.Empty;
                            columnName             = datafieldFragments[0];
                        }
                        saveOrder              = (formfieldNode.Attributes["data-saveorder"] != null) ? Convert.ToInt32(formfieldNode.Attributes["data-saveorder"].Value) : 0;
                        validationName         = (formfieldNode.Attributes["data-validationname"] != null) ? formfieldNode.Attributes["data-validationname"].Value : string.Empty;
                        validationDisplayField = (formfieldNode.Attributes["data-validationdisplayfield"] != null) ? formfieldNode.Attributes["data-validationdisplayfield"].Value : string.Empty;
                        noDuplicate            = true;
                        exportToExcel          = false;
                        tabCaption             = (tabsByField.ContainsKey(datafield) ? tabsByField[datafield] : string.Empty);
                        if (!formSchema.Tables.ContainsKey(tableName))
                        {
                            formSchema.Tables[tableName] = new FwApplicationSchema.FormTable(tableName, saveOrder, new Dictionary<string,FwApplicationSchema.Column>(), new Dictionary<string,FwApplicationSchema.Column>());
                        }
                        columnSchema = new FwApplicationSchema.Column(caption, columnName, dataType, string.Empty, false, 0, 0, 0, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                        formSchema.Tables[tableName].UniqueIds[columnName] = columnSchema;
                    }
                }
                
                // load the form column nodes
                foreach(XmlNode formfieldNode in xmlColumns)
                {
                    if ((formfieldNode.Attributes["data-datafield"] != null) && (!string.IsNullOrEmpty(formfieldNode.Attributes["data-datafield"].Value)))
                    {
                        string datafield, tableName=string.Empty, columnName=string.Empty, caption, validationName, validationDisplayField, dataType, tabCaption;
                        string[] datafieldFragments;
                        int saveOrder;
                        bool required, isIdentity, readOnly, noDuplicate, exportToExcel;

                        datafield              = (formfieldNode.Attributes["data-datafield"] != null) ? formfieldNode.Attributes["data-datafield"].Value : string.Empty;
                        caption                = (formfieldNode.Attributes["data-caption"]   != null) ? formfieldNode.Attributes["data-caption"].Value   : datafield;
                        dataType               = (formfieldNode.Attributes["data-type"]      != null) ? formfieldNode.Attributes["data-type"].Value      : string.Empty;
                        datafieldFragments     = datafield.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                        if (datafieldFragments.Length == 2)
                        {
                            tableName              = datafieldFragments[0];
                            columnName             = datafieldFragments[1];
                        }
                        else if (datafieldFragments.Length == 1)
                        {
                            tableName              = string.Empty;
                            columnName             = datafieldFragments[0];
                        }
                        readOnly               = (formfieldNode.Attributes["data-readonly"] != null) ? formfieldNode.Attributes["data-readonly"].Value.Equals("true") : false;
                        required               = (formfieldNode.Attributes["data-required"] != null) ? formfieldNode.Attributes["data-required"].Value.ToLower().Equals("true") : false;
                        isIdentity             = false;
                        validationName         = (formfieldNode.Attributes["data-validationname"] != null) ? formfieldNode.Attributes["data-validationname"].Value : string.Empty;
                        validationDisplayField = (formfieldNode.Attributes["data-validationdisplayfield"] != null) ? formfieldNode.Attributes["data-validationdisplayfield"].Value : string.Empty;
                        noDuplicate            = (formfieldNode.Attributes["data-noduplicate"] != null) ? formfieldNode.Attributes["data-noduplicate"].Value.Equals("true") : false;
                        exportToExcel          = false;
                        tabCaption             = (tabsByField.ContainsKey(datafield) ? tabsByField[datafield] : string.Empty);
                        if (formSchema.Tables.ContainsKey(tableName))
                        {
                            columnSchema = new FwApplicationSchema.Column(caption, columnName, dataType, string.Empty, false, 0, 0, 0, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                            formSchema.Tables[tableName].Columns[columnName] = columnSchema;
                        }
                    }
                }

                // load the form charge column nodes
                foreach(XmlNode chargefieldNode in xmlChargeField)
                {
                    if ((chargefieldNode.Attributes["data-datafield"] != null) && (!string.IsNullOrEmpty(chargefieldNode.Attributes["data-datafield"].Value)))
                    {
                        string datafield, tableName, columnName, modifiedColumnName, caption, tabCaption;
                        string[] datafieldFragments;
                        int saveOrder;
                        bool required, isIdentity, readOnly, noDuplicate, exportToExcel;

                        datafield              = (chargefieldNode.Attributes["data-datafield"] != null) ? chargefieldNode.Attributes["data-datafield"].Value : string.Empty;
                        datafieldFragments     = datafield.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                        tableName              = datafieldFragments[0];
                        columnName             = datafieldFragments[1];
                        caption                = (chargefieldNode.Attributes["data-caption"] != null) ? chargefieldNode.Attributes["data-caption"].Value : columnName;
                        readOnly               = (chargefieldNode.Attributes["data-readonly"]  != null) ? chargefieldNode.Attributes["data-readonly"].Value.Equals("true") : false;
                        required               = (chargefieldNode.Attributes["data-required"] != null) ? chargefieldNode.Attributes["data-required"].Value.ToLower().Equals("true") : false;
                        noDuplicate            = (chargefieldNode.Attributes["data-noduplicate"] != null) ? chargefieldNode.Attributes["data-noduplicate"].Value.Equals("true") : false;
                        isIdentity             = false;
                        exportToExcel          = false;
                        //tabCaption             = (chargefieldNode.Attributes["data-tabcaption"] != null) ? chargefieldNode.Attributes["data-tabcaption"].Value : string.Empty;
                        tabCaption             = tabsByFwCharge[datafield];
                        if (formSchema.Tables.ContainsKey(tableName))
                        {
                            for (int i = 1; i <= 13; i++)
                            {
                                modifiedColumnName = columnName + i.ToString();
                                columnSchema = new FwApplicationSchema.Column(caption, modifiedColumnName, string.Empty, string.Empty, false, 0, 0, 0, isIdentity, readOnly, required, string.Empty, string.Empty, noDuplicate, exportToExcel, tabCaption);
                                formSchema.Tables[tableName].Columns[modifiedColumnName] = columnSchema;
                            }
                        }
                    }
                }
                
                if (formSchema != null)
                {
                    foreach(var item in formSchema.Tables)
                    {
                        SqlConnection sqlConnection;
                        SqlCommand sqlCommand;
                        SqlDataAdapter sqlAdapter;
                        DataTable dt;
                        
                        // load schema from the database
                        tableSchema = item.Value;
                        dt = new DataTable();
                        sqlConnection = new SqlConnection("Server=" + databaseConnection.Server + ";Database=" + databaseConnection.Database + ";User Id=dbworks;Password=db2424;");
                        sqlCommand    = new SqlCommand(GetTableSchemaQuery(tableSchema.TableName), sqlConnection);
                        sqlAdapter    = new SqlDataAdapter(sqlCommand);
                        sqlAdapter.Fill(dt);
                        foreach(DataRow row in dt.Rows)
                        {
                            string caption, colname, dataType, sqlDataType, validationName, validationDisplayField, tabCaption;
                            bool sqlIsNullable;
                            int sqlCharacterMaxLength, sqlNumericPrecision, sqlNumericScale;
                            bool required, sqlIsIdentity, readOnly, noDuplicate, exportToExcel;

                            colname = row["column"].ToString();
                            caption = colname;
                            if (tableSchema.Columns.ContainsKey(colname))
                            {
                                caption = tableSchema.Columns[colname].Caption;
                            }
                            // automatically include datestamp column if it exists in the info schema view
                            if ((colname == "datestamp") && (!tableSchema.Columns.ContainsKey(colname)))
                            {
                                tableSchema.Columns[colname] = new FwApplicationSchema.Column(caption, colname, "datetime", string.Empty, false, 0, 0, 0, false, false, true, string.Empty, string.Empty, true, false, string.Empty);
                            }
                            if ( (tableSchema.UniqueIds.ContainsKey(colname)) || (tableSchema.Columns.ContainsKey(colname)) )
                            {
                                sqlDataType              = row["datatype"].ToString();
                                sqlIsNullable            = (row["isnullable"].ToString() == "YES");
                                sqlCharacterMaxLength    = ((row["maxlength"] != DBNull.Value) && (sqlDataType != "text")) ? Convert.ToInt32(row["maxlength"]) : 0;
                                sqlNumericPrecision      = (row["precision"]  != DBNull.Value) ? Convert.ToInt32(row["precision"])            : 0;
                                sqlNumericScale          = (row["scale"]      != DBNull.Value) ? Convert.ToInt32(row["scale"])                : 0;
                                sqlIsIdentity            = (row["isidentity"] != DBNull.Value) ? Convert.ToInt32(row["isidentity"]).Equals(1) : false;
                                if (tableSchema.UniqueIds.ContainsKey(colname))
                                {
                                    dataType               = tableSchema.UniqueIds[colname].DataType;
                                    readOnly               = tableSchema.UniqueIds[colname].ReadOnly;
                                    required               = tableSchema.UniqueIds[colname].Required;
                                    validationName         = tableSchema.UniqueIds[colname].ValidationName;
                                    validationDisplayField = tableSchema.UniqueIds[colname].ValidationDisplayField;
                                    noDuplicate            = tableSchema.UniqueIds[colname].NoDuplicate;
                                    exportToExcel          = tableSchema.UniqueIds[colname].ExportToExcel;
                                    tabCaption             = tableSchema.UniqueIds[colname].TabCaption;
                                    columnSchema = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, sqlIsNullable, sqlCharacterMaxLength, sqlNumericPrecision, sqlNumericScale, sqlIsIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                    tableSchema.UniqueIds[colname] = columnSchema;
                                }
                                else if (tableSchema.Columns.ContainsKey(colname))
                                {
                                    dataType               = tableSchema.Columns[colname].DataType;
                                    readOnly               = tableSchema.Columns[colname].ReadOnly;
                                    required               = tableSchema.Columns[colname].Required;
                                    validationName         = tableSchema.Columns[colname].ValidationName;
                                    validationDisplayField = tableSchema.Columns[colname].ValidationDisplayField;
                                    noDuplicate            = tableSchema.Columns[colname].NoDuplicate;
                                    exportToExcel          = tableSchema.Columns[colname].ExportToExcel;
                                    tabCaption             = tableSchema.Columns[colname].TabCaption;
                                    columnSchema = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, sqlIsNullable, sqlCharacterMaxLength, sqlNumericPrecision, sqlNumericScale, sqlIsIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                    tableSchema.Columns[colname] = columnSchema;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception newException;
                newException = new Exception("Exception occured when loading schema for " + componentType + ": " + componentName + "Form.htm.  Make sure there is no error in the template.", ex);
                this.Log.LogErrorFromException(newException, true);
            }

            return formSchema;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Browse GetBrowseSchema(DatabaseConnection databaseConnection, string componentType, string componentName, string browseTemplate)
        {
            XmlDocument xmlDoc;
            XmlNodeList scrollerNodes, scrollerFieldNodes, uniqueidNodes;
            FwApplicationSchema.Browse schemaBrowse=null;
            FwApplicationSchema.Column schemaColumn;
            string tableName, columnName, connectionString, infoSchemaColumnQuery;
            string[] datafieldFragments;
            SqlConnection sqlConnection;
            SqlCommand sqlCommand;
            SqlDataAdapter sqlAdapter;
            DataTable dt;

            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(browseTemplate);
                scrollerNodes = xmlDoc.SelectNodes("//div[@data-control='FwBrowse']"); //xpath query for: <div data-control="FwScroller"></div>
                foreach(XmlNode scrollerNode in scrollerNodes)
                {
                    string browseDatabaseConnection, datatable;
                    Dictionary<string,FwApplicationSchema.Column> browseUniqueIds, browseColumns;
                    
                    uniqueidNodes            = scrollerNode.SelectNodes("//input[@class='uniqueid']");
                    browseDatabaseConnection = databaseConnection.Name;
                    datatable                = (scrollerNode.Attributes["data-datatable"] != null) ? scrollerNode.Attributes["data-datatable"].Value : string.Empty;
                    browseUniqueIds          = new Dictionary<string,FwApplicationSchema.Column>();
                    browseColumns            = new Dictionary<string,FwApplicationSchema.Column>();
                    schemaBrowse             = new FwApplicationSchema.Browse(browseDatabaseConnection, datatable, browseUniqueIds, browseColumns);
                    scrollerFieldNodes       = scrollerNode.SelectNodes("//div[@class='column']/div[@class='field']"); //xpath query for: <div class="field"></div>
                    foreach(XmlNode fieldNode in scrollerFieldNodes)
                    {
                        string caption, datafield, dataType, displayfield, displayFieldDataType, validationName, validationDisplayField, tabCaption;
                        bool isUniqueId, required, readOnly, noDuplicate, exportToExcel;

                        datafield              = (fieldNode.Attributes["data-browsedatafield"]            != null) ? fieldNode.Attributes["data-browsedatafield"].Value : string.Empty;
                        caption                = (fieldNode.Attributes["data-caption"]                    != null) ? fieldNode.Attributes["data-caption"].Value : datafield;
                        dataType               = (fieldNode.Attributes["data-browsedatatype"]             != null) ? fieldNode.Attributes["data-browsedatatype"].Value : string.Empty;
                        displayfield           = (fieldNode.Attributes["data-browsedisplayfield"]         != null) ? fieldNode.Attributes["data-browsedisplayfield"].Value : string.Empty;
                        displayFieldDataType   = (fieldNode.Attributes["data-browsedisplayfielddatatype"] != null) ? fieldNode.Attributes["data-browsedisplayfielddatatype"].Value : string.Empty;
                        isUniqueId             = (fieldNode.Attributes["data-isuniqueid"]                 != null) ? fieldNode.Attributes["data-isuniqueid"].Value.ToLower().Equals("true") : false;
                        readOnly               = true;
                        required               = false;
                        validationName         = (fieldNode.Attributes["data-formvalidationname"]         != null) ? fieldNode.Attributes["data-formvalidationname"].Value : string.Empty;
                        validationDisplayField = (fieldNode.Attributes["data-validationdisplayfield"]     != null) ? fieldNode.Attributes["data-validationdisplayfield"].Value : string.Empty;
                        noDuplicate            = (fieldNode.Attributes["data-noduplicate"]                != null) ? fieldNode.Attributes["data-noduplicate"].Value.Equals("true") : false;
                        exportToExcel          = (fieldNode.Attributes["data-exporttoexcel"]              == null) ? true : fieldNode.Attributes["data-exporttoexcel"].Value.Equals("true");
                        tabCaption             = (fieldNode.Attributes["data-tabcaption"]            != null) ? fieldNode.Attributes["data-tabcaption"].Value : string.Empty;
                        if (isUniqueId)
                        {
                            if (!string.IsNullOrEmpty(datafield))
                            {
                                schemaColumn = new FwApplicationSchema.Column(caption, datafield, dataType, string.Empty, false, 0, 0, 0, false, readOnly, required, validationName, validationDisplayField, true, exportToExcel, tabCaption);
                                schemaBrowse.UniqueIds[schemaColumn.ColumnName] = schemaColumn;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(datafield))
                            {
                                schemaColumn = new FwApplicationSchema.Column(caption, datafield, dataType, string.Empty, false, 0, 0, 0, false, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                schemaBrowse.Columns[schemaColumn.ColumnName] = schemaColumn;
                            }
                        }
                        if (!string.IsNullOrEmpty(displayfield))
                        {
                            schemaColumn = new FwApplicationSchema.Column(caption, displayfield, displayFieldDataType, string.Empty, false, 0, 0, 0, false, true, false, string.Empty, string.Empty, false, exportToExcel, tabCaption);
                            schemaBrowse.Columns[schemaColumn.ColumnName] = schemaColumn;
                        }
                    }
                    // load schema from the database
                    dt                    = new DataTable();
                    connectionString      = "Server=" + databaseConnection.Server + ";Database=" + databaseConnection.Database + ";User Id=dbworks;Password=db2424;";
                    infoSchemaColumnQuery = GetTableSchemaQuery(datatable);
                    sqlConnection         = new SqlConnection(connectionString);
                    sqlCommand            = new SqlCommand(infoSchemaColumnQuery, sqlConnection);
                    sqlAdapter            = new SqlDataAdapter(sqlCommand);
                    sqlAdapter.Fill(dt);
                    foreach(DataRow row in dt.Rows)
                    {
                        string caption, colname, dataType, sqlDataType, validationName, validationDisplayField, tabCaption;
                        bool isNullable, required, isIdentity, readOnly, noDuplicate, exportToExcel;
                        int characterMaxLength, numericPrecision, numericScale;
                        
                        colname = row["column"].ToString();
                        caption = colname;
                        if (schemaBrowse.Columns.ContainsKey(colname))
                        {
                            caption = schemaBrowse.Columns[colname].Caption;
                        }
                        // automatically include inactive column if it exists in the info schema view
                        if ((colname == "inactive") && (!schemaBrowse.Columns.ContainsKey(colname)))
                        {
                            schemaBrowse.Columns[colname] = new FwApplicationSchema.Column(caption, colname, "checkbox", string.Empty, false, 0, 0, 0, false, false, true, string.Empty, string.Empty, false, false, string.Empty);
                        }
                        if ( (schemaBrowse.UniqueIds.ContainsKey(colname)) || (schemaBrowse.Columns.ContainsKey(colname)) )
                        {
                            sqlDataType           = row["datatype"].ToString();
                            isNullable            = (row["isnullable"].ToString() == "YES");
                            characterMaxLength    = ((row["maxlength"] != DBNull.Value) && (sqlDataType != "text")) ? Convert.ToInt32(row["maxlength"]) : 0;
                            numericPrecision      = (row["precision"]  != DBNull.Value) ? Convert.ToInt32(row["precision"])        : 0;
                            numericScale          = (row["scale"]      != DBNull.Value) ? Convert.ToInt32(row["scale"])            : 0;
                            isIdentity            = (row["isidentity"] != DBNull.Value) ? Convert.ToInt32(row["isidentity"]).Equals(1)   : false;
                            if (schemaBrowse.UniqueIds.ContainsKey(colname))
                            {
                                dataType               = schemaBrowse.UniqueIds[colname].DataType;
                                readOnly               = schemaBrowse.UniqueIds[colname].ReadOnly;
                                required               = schemaBrowse.UniqueIds[colname].Required;
                                validationName         = schemaBrowse.UniqueIds[colname].ValidationName;
                                validationDisplayField = schemaBrowse.UniqueIds[colname].ValidationDisplayField;
                                noDuplicate            = schemaBrowse.UniqueIds[colname].NoDuplicate;
                                exportToExcel          = schemaBrowse.UniqueIds[colname].ExportToExcel;
                                tabCaption             = schemaBrowse.UniqueIds[colname].TabCaption;
                                schemaColumn = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, isNullable, characterMaxLength, numericPrecision, numericScale, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                schemaBrowse.UniqueIds[colname] = schemaColumn;
                            }
                            if (schemaBrowse.Columns.ContainsKey(colname))
                            {
                                dataType               = schemaBrowse.Columns[colname].DataType;
                                readOnly               = schemaBrowse.Columns[colname].ReadOnly;
                                required               = schemaBrowse.Columns[colname].Required;
                                validationName         = schemaBrowse.Columns[colname].ValidationName;
                                validationDisplayField = schemaBrowse.Columns[colname].ValidationDisplayField;
                                noDuplicate            = schemaBrowse.Columns[colname].NoDuplicate;
                                exportToExcel          = schemaBrowse.Columns[colname].ExportToExcel;
                                tabCaption             = schemaBrowse.Columns[colname].TabCaption;
                                schemaColumn   = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, isNullable, characterMaxLength, numericPrecision, numericScale, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                schemaBrowse.Columns[colname] = schemaColumn;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception newException;
                newException = new Exception("Exception occured when loading schema for " + componentType + ": " + componentName + "Browse.htm.  Make sure there is no error in the template.", ex);
                this.Log.LogErrorFromException(newException, true);
            }

            return schemaBrowse;
        }
        //---------------------------------------------------------------------------------------------
        public FwApplicationSchema.Form GetGridFormSchema(DatabaseConnection databaseConnection, string componentType, string componentName, string browseTemplate)
        {
            XmlDocument xmlDoc;
            XmlNodeList scrollerNodes, scrollerFieldNodes;
            FwApplicationSchema.Form formSchema=null;
            FwApplicationSchema.FormTable tableSchema;
            FwApplicationSchema.Column columnSchema;
            string connectionString, infoSchemaColumnQuery;
            SqlConnection sqlConnection;
            SqlCommand sqlCommand;
            SqlDataAdapter sqlAdapter;
            DataTable dt;
            bool isUniqueId, hasAudit;

            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(browseTemplate);
                scrollerNodes = xmlDoc.SelectNodes("//div[@data-control='FwBrowse']"); //xpath query for: <div data-control="FwScroller"></div>
                foreach(XmlNode scrollerNode in scrollerNodes)
                {
                    string formDatabaseConnection, datatable;
                    List<FwApplicationSchema.Tab> tabs;
                    List<FwApplicationSchema.FormGrid> grids;
                    Dictionary<string,FwApplicationSchema.FormTable> tables;
                    
                    formDatabaseConnection = databaseConnection.Name;
                    tabs                   = new List<FwApplicationSchema.Tab>();
                    grids                  = new List<FwApplicationSchema.FormGrid>();
                    tables                 = new Dictionary<string,FwApplicationSchema.FormTable>();
                    hasAudit               = false;
                    formSchema             = new FwApplicationSchema.Form(formDatabaseConnection, tabs, grids, tables, hasAudit);
                    scrollerFieldNodes     = scrollerNode.SelectNodes("//div[@class='column']/div[@class='field']"); //xpath query for the field in: <div class="column"><div class="field"></div></div>
                    
                    // add columns to the form schema from the xml template
                    foreach(XmlNode fieldNode in scrollerFieldNodes)
                    {
                        string caption, datafield, dataType, tableName, columnName, validationName, validationDisplayField, tabCaption;
                        int saveOrder = 1;
                        string[] datafieldFragments;
                        bool required, isIdentity, readOnly, noDuplicate, exportToExcel;

                        if (fieldNode.Attributes["data-formdatafield"] == null)
                        {
                            throw new Exception("A field is missing attribute data-formdatafield in " + componentType + ": " + componentName + ".");
                        }
                        //if (fieldNode.Attributes["data-isuniqueid"] == null)
                        //{
                        //    throw new Exception("A field is missing attribute data-isuniqueid in " + componentType + ": " + componentName + ".");
                        //}
                        datafield    = fieldNode.Attributes["data-formdatafield"].Value;
                        caption      = (fieldNode.Attributes["data-caption"] != null) ? fieldNode.Attributes["data-caption"].Value : datafield;
                        if (!string.IsNullOrEmpty(datafield))
                        {
                            datafieldFragments = datafield.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                            if (datafieldFragments.Length == 1)
                            {
                                //throw new Exception("Invalid datafield: '" + datafield + "' in " + componentType + ": " + componentName + ".");
                                tableName = componentName;
                                columnName = datafieldFragments[0];
                            }
                            else if (datafieldFragments.Length == 2)
                            {
                                tableName  = datafieldFragments[0];
                                columnName = datafieldFragments[1];
                            }
                            else
                            {
                                throw new Exception("Invalid datafield: '" + datafield + "' in " + componentType + ": " + componentName + ".");
                            }
                            dataType               = (fieldNode.Attributes["data-formdatatype"] != null) ? fieldNode.Attributes["data-formdatatype"].Value : string.Empty;
                            isUniqueId             = (fieldNode.Attributes["data-isuniqueid"] != null) ? fieldNode.Attributes["data-isuniqueid"].Value.Equals("true") : false;
                            //if ((fieldNode.Attributes["data-isuniqueid"] != null) && 
                            //    (fieldNode.Attributes["data-isuniqueid"].Value.ToLower() == "true") && 
                            //    (fieldNode.Attributes["data-formsaveorder"] == null))
                            //{
                            //    //throw new Exception("A uniqueid field is missing attribute data-formsaveorder in " + componentType + ": " + componentName + ".");
                            //    saveOrder = 1;
                            //}
                            if (fieldNode.Attributes["data-formsaveorder"] != null)
                            {
                                saveOrder = Convert.ToInt32(fieldNode.Attributes["data-formsaveorder"].Value);
                            }
                            isIdentity             = false;
                            readOnly               = (fieldNode.Attributes["data-formreadonly"]           != null) ? fieldNode.Attributes["data-formreadonly"].Value.Equals("true") : false;
                            required               = (fieldNode.Attributes["data-formrequired"]           != null) ? fieldNode.Attributes["data-formrequired"].Value.Equals("true") : false;
                            validationName         = (fieldNode.Attributes["data-formvalidationname"]     != null) ? fieldNode.Attributes["data-formvalidationname"].Value : string.Empty;
                            validationDisplayField = (fieldNode.Attributes["data-validationdisplayfield"] != null) ? fieldNode.Attributes["data-validationdisplayfield"].Value : string.Empty;
                            noDuplicate            = (fieldNode.Attributes["data-noduplicate"]            != null) ? fieldNode.Attributes["data-noduplicate"].Value.Equals("true") : false;
                            exportToExcel          = (fieldNode.Attributes["data-exporttoexcel"]          == null) ? true : fieldNode.Attributes["data-exporttoexcel"].Value.Equals("true");
                            tabCaption             = (fieldNode.Attributes["data-tabcaption"] != null) ? fieldNode.Attributes["data-tabcaption"].Value : string.Empty;
                            if (!formSchema.Tables.ContainsKey(tableName))
                            {
                                tableSchema = new FwApplicationSchema.FormTable(tableName, saveOrder, new Dictionary<string,FwApplicationSchema.Column>(), new Dictionary<string,FwApplicationSchema.Column>());
                                formSchema.Tables[tableName] = tableSchema;
                            }
                            columnSchema = new FwApplicationSchema.Column(caption, columnName, dataType, string.Empty, false, 0, 0, 0, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                            if (isUniqueId)
                            {
                                formSchema.Tables[tableName].UniqueIds[columnName] = columnSchema;
                            }
                            else
                            {
                                formSchema.Tables[tableName].Columns[columnName] = columnSchema;
                            }
                        }
                    }
                }
                if (formSchema != null)
                {
                    foreach(var item in formSchema.Tables)
                    {
                        // load schema from the database
                        tableSchema = item.Value;
                        dt = new DataTable();
                        sqlConnection = new SqlConnection("Server=" + databaseConnection.Server + ";Database=" + databaseConnection.Database + ";User Id=dbworks;Password=db2424;");
                        sqlCommand    = new SqlCommand(GetTableSchemaQuery(tableSchema.TableName), sqlConnection);
                        sqlAdapter    = new SqlDataAdapter(sqlCommand);
                        sqlAdapter.Fill(dt);
                        foreach(DataRow row in dt.Rows)
                        {
                            string caption, colname, dataType, sqlDataType, validationName, validationDisplayField, tabCaption;
                            bool isNullable, required, isIdentity, readOnly, noDuplicate, exportToExcel;
                            int characterMaxLength, numericPrecision, numericScale;
                            
                            colname = row["column"].ToString();
                            caption = colname;
                            if (tableSchema.Columns.ContainsKey(colname))
                            {
                                caption = tableSchema.Columns[colname].Caption;
                            }
                            // automatically include datestamp if it exists in the info schema view
                            if ((colname == "datestamp") && (!tableSchema.Columns.ContainsKey(colname)))
                            {
                                isIdentity = false;
                                tableSchema.Columns[colname] = new FwApplicationSchema.Column(caption, colname, "datetime", string.Empty, false, 0, 0, 0, isIdentity, false, true, string.Empty, string.Empty, false, false, string.Empty);
                            }
                            if ( (tableSchema.UniqueIds.ContainsKey(colname)) || (tableSchema.Columns.ContainsKey(colname)) )
                            {
                                sqlDataType           = row["datatype"].ToString();
                                isNullable            = (row["isnullable"].ToString() == "YES");
                                characterMaxLength    = ((row["maxlength"]  != DBNull.Value) && (sqlDataType != "text")) ? Convert.ToInt32(row["maxlength"]) : 0;
                                numericPrecision      = (row["precision"]  != DBNull.Value) ? Convert.ToInt32(row["precision"])        : 0;
                                numericScale          = (row["scale"]      != DBNull.Value) ? Convert.ToInt32(row["scale"])            : 0;
                                isIdentity            = (row["isidentity"] != DBNull.Value) ? Convert.ToInt32(row["isidentity"]).Equals(1)   : false;
                                if (tableSchema.UniqueIds.ContainsKey(colname))
                                {
                                    dataType               = tableSchema.UniqueIds[colname].DataType;
                                    readOnly               = tableSchema.UniqueIds[colname].ReadOnly;
                                    required               = tableSchema.UniqueIds[colname].Required;
                                    validationName         = tableSchema.UniqueIds[colname].ValidationName;
                                    validationDisplayField = tableSchema.UniqueIds[colname].ValidationDisplayField;
                                    noDuplicate            = tableSchema.UniqueIds[colname].NoDuplicate;
                                    exportToExcel          = tableSchema.UniqueIds[colname].ExportToExcel;
                                    tabCaption             = tableSchema.UniqueIds[colname].TabCaption;
                                    columnSchema   = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, isNullable, characterMaxLength, numericPrecision, numericScale, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                    tableSchema.UniqueIds[colname] = columnSchema;
                                }
                                else if (tableSchema.Columns.ContainsKey(colname))
                                {
                                    dataType               = tableSchema.Columns[colname].DataType;
                                    readOnly               = tableSchema.Columns[colname].ReadOnly;
                                    required               = tableSchema.Columns[colname].Required;
                                    validationName         = tableSchema.Columns[colname].ValidationName;
                                    validationDisplayField = tableSchema.Columns[colname].ValidationDisplayField;
                                    noDuplicate            = tableSchema.Columns[colname].NoDuplicate;
                                    exportToExcel          = tableSchema.Columns[colname].ExportToExcel;
                                    tabCaption             = tableSchema.Columns[colname].TabCaption;
                                    columnSchema   = new FwApplicationSchema.Column(caption, colname, dataType, sqlDataType, isNullable, characterMaxLength, numericPrecision, numericScale, isIdentity, readOnly, required, validationName, validationDisplayField, noDuplicate, exportToExcel, tabCaption);
                                    tableSchema.Columns[colname] = columnSchema;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Exception newException;
                //newException = new Exception("Exception occured when loading the FORM schema for " + componentType + ": " + componentName + "Browse.htm.  Make sure there is no error in the template.", ex);
                //this.Log.LogErrorFromException(newException, true);
                this.Log.LogErrorFromException(ex, true);
                this.Log.LogError("Exception occured when loading the FORM schema for " + componentType + ": " + componentName + "Browse.htm.  Make sure there is no error in the template.");
            }

            return formSchema;
        }
        //---------------------------------------------------------------------------------------------
        static string NormalizeLineEndings(string text)
        {
            string result = Regex.Replace(text, @"\r\n|\n\r|\n|\r", "\r\n");
            return result;
        }
        //---------------------------------------------------------------------------------------------
        static Dictionary<string, DatabaseConnection> GetDatabaseConnections(string applicationConfigPath)
        {
            string applicationConfigXml, databaseConnectionName;
            XmlDocument applicationConfigDoc;
            XmlNodeList nodesDatabaseConnection, nodesServer, nodesDatabase;
            Dictionary<string, DatabaseConnection> databaseConnections;
            DatabaseConnection connection;

            applicationConfigXml = File.ReadAllText(applicationConfigPath);
            applicationConfigDoc = new XmlDocument();
            applicationConfigDoc.LoadXml(applicationConfigXml);
            nodesDatabaseConnection = applicationConfigDoc.SelectNodes("//DatabaseConnection");
            databaseConnections     = new Dictionary<string,DatabaseConnection>();
            foreach(XmlNode node in nodesDatabaseConnection)
            {
                connection          = new DatabaseConnection();
                connection.Name     = node.Attributes["Name"].Value;
                nodesServer         = node.SelectNodes("//Server");
                connection.Server   = nodesServer[0].InnerText;
                nodesDatabase       = node.SelectNodes("//Database");
                connection.Database = nodesDatabase[0].InnerText;
                databaseConnections[connection.Name] = connection;
            }

            return databaseConnections;
        }
        //---------------------------------------------------------------------------------------------
        static string GetAbsolutePath(string path)
        {
            path = path.Replace('/',  Path.DirectorySeparatorChar)
                       .Replace('\\', Path.DirectorySeparatorChar);
            string absolutePath = string.Empty;
            if (Path.IsPathRooted(path))
            {
                absolutePath = path;
            }
            else
            {
                absolutePath = Path.Combine(Environment.CurrentDirectory, path);
            }
            return absolutePath;
        }
        //---------------------------------------------------------------------------------------------
        string ApplyFields(string text, JSAppBuilderConfig build, bool publish)
        {
            string result = text;
            string value;

            foreach (Field field in build.Fields)
            {
                if ((publish == field.Publish) && (!string.IsNullOrEmpty(field.Key)))
                {
                    value = field.Value;
                    if (((field.Key == "{{FwFrontEndLibraryUri}}") && (field.Value != "[appbaseurl][fwvirtualdirectory]")) || ((field.Key == "{{AppUri}}") && (field.Value != "[appbaseurl][appvirtualdirectory]")))
                    {
                        //if (!value.EndsWith("/"))
                        //{
                        //    value += "/";
                        //}
                    }
                    result = result.Replace(field.Key, value);
                }
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        string UriToString(string uri, Task task)
        {
            WebClient client;
            Stream stream;
            StreamReader reader;
            string result;

            Uri uri1 = new Uri(uri);
            if (uri1.IsFile)
            {
                result = File.ReadAllText(uri1.LocalPath);
            }
            else
            {
                try
                {
                    client = new WebClient();
                    stream = client.OpenRead(uri);
                    reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
                catch (Exception ex)
                {
                    //System.Diagnostics.Debugger.Launch();
                    task.Log.LogError("Unable to load uri: " + uri);
                    throw new Exception("Unable to load uri: " + uri, ex);
                }
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }
            return sb.ToString();
        }
        //---------------------------------------------------------------------------------------------
        string GetAddSchemaDataToModuleBrowseTemplate(string formTemplate, FwApplicationSchema schema, string moduleName)
        {
            XmlDocument xmlForm;
            XmlNodeList xmlBrowses;
            string result;

            xmlForm = new XmlDocument();
            try
            {
                xmlForm.LoadXml(formTemplate);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("***The file " + moduleName + "Browse.htm is not valid XML.***");
                Console.Error.WriteLine(ex.Message);
                //Console.Error.WriteLine(ex.StackTrace);
            }
            xmlBrowses = xmlForm.SelectNodes("//div[@data-control='FwBrowse']"); //xpath query
            foreach(XmlNode xmlBrowse in xmlBrowses)
            {
                if ((schema.Modules.ContainsKey(moduleName)) && (schema.Modules[moduleName].Browse != null) && (schema.Modules[moduleName].Browse.Columns.ContainsKey("inactive")))
                {
                    if (xmlBrowse.Attributes["data-hasinactive"] == null)
                    {
                        XmlAttribute attrHasInactive;

                        attrHasInactive       = xmlForm.CreateAttribute("data-hasinactive");
                        attrHasInactive.Value = "true";
                        xmlBrowse.Attributes.Append(attrHasInactive); 
                    }
                    if (xmlBrowse.Attributes["data-activeinactiveview"] == null)
                    {
                        XmlAttribute attrHasInactive;

                        attrHasInactive       = xmlForm.CreateAttribute("data-activeinactiveview");
                        attrHasInactive.Value = "active";
                        xmlBrowse.Attributes.Append(attrHasInactive); 
                    }
                }
                
            }
            result = GetFormattedXml(xmlForm.OuterXml);
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        string GetAddSchemaDataToModuleFormTemplate(string formTemplate, FwApplicationSchema schema, string moduleName)
        {
            XmlDocument xmlForm;
            XmlNodeList xmlUniqueIds, xmlColumns;
            string result;

            xmlForm = new XmlDocument();
            try
            {
                xmlForm.LoadXml(formTemplate);
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine("***The file " + moduleName + "Form.htm is not valid XML.***");
                Console.Error.WriteLine(ex.Message);
                //Console.Error.WriteLine(ex.StackTrace);
            }
            xmlUniqueIds = xmlForm.SelectNodes("//div[@data-control='FwFormField' and @data-isuniqueid='true']"); //xpath query
            xmlColumns   = xmlForm.SelectNodes("//div[@data-control='FwFormField' and (not(@data-isuniqueid) or @data-isuniqueid!='true')]"); //xpath query
            foreach(XmlNode xmlUniqueId in xmlUniqueIds)
            {
                string tableName, columnName;
                string[] dataFieldFragments;

                if ( ((xmlUniqueId.Attributes["data-datafield"] != null) && (xmlUniqueId.Attributes["data-datafield"].Value.Contains('.'))) &&
                        ((xmlUniqueId.Attributes["data-maxlength"] == null) || (string.IsNullOrEmpty(xmlUniqueId.Attributes["data-maxlength"].Value))) )
                {
                    dataFieldFragments = xmlUniqueId.Attributes["data-datafield"].Value.Split(new char[]{'.'});
                    tableName          = dataFieldFragments[0];
                    columnName         = dataFieldFragments[1];
                    if (schema.Modules.ContainsKey(moduleName) && schema.Modules[moduleName].Form.Tables.ContainsKey(tableName) && schema.Modules[moduleName].Form.Tables[tableName].UniqueIds.ContainsKey(columnName))
                    {
                        XmlAttribute attrMaxLength;

                        attrMaxLength = xmlUniqueId.OwnerDocument.CreateAttribute("data-maxlength");
                        attrMaxLength.Value = schema.Modules[moduleName].Form.Tables[tableName].UniqueIds[columnName].SqlCharacterMaximumLength.ToString();
                        xmlUniqueId.Attributes.Append(attrMaxLength);
                    }
                }
            }
            foreach(XmlNode xmlColumn in xmlColumns)
            {
                string tableName, columnName;
                string[] dataFieldFragments;

                if ((xmlColumn.Attributes["data-datafield"] != null) && (xmlColumn.Attributes["data-datafield"].Value.Contains('.')))
                {
                    dataFieldFragments = xmlColumn.Attributes["data-datafield"].Value.Split(new char[]{'.'});
                    tableName          = dataFieldFragments[0];
                    columnName         = dataFieldFragments[1];
                    if (schema.Modules.ContainsKey(moduleName) && schema.Modules[moduleName].Form.Tables.ContainsKey(tableName) && schema.Modules[moduleName].Form.Tables[tableName].Columns.ContainsKey(columnName))
                    {
                        string attrDataType, attrSqlDataType;
                        XmlAttribute attrMaxLength, attrMinValue, attrMaxValue;

                        attrDataType    = schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].DataType.ToString();
                        attrSqlDataType = schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlDataType.ToString();
                        
                        switch (attrDataType) {
                            case "text":
                            case "textarea":
                            case "email":
                            case "password":
                            case "encrypt":
                                if ((xmlColumn.Attributes["data-maxlength"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-maxlength"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-maxlength"].Value) > schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlCharacterMaximumLength))
                                {
                                    attrMaxLength = xmlColumn.OwnerDocument.CreateAttribute("data-maxlength");
                                    attrMaxLength.Value = schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlCharacterMaximumLength.ToString();
                                    xmlColumn.Attributes.Append(attrMaxLength);
                                }
                                break;
                            case "number":
                                switch (attrSqlDataType)
                                {
                                    case "int":
                                        if ((xmlColumn.Attributes["data-minvalue"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-minvalue"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-minvalue"].Value) < Int32.Parse("-2147483647")))
                                        {
                                            attrMinValue = xmlColumn.OwnerDocument.CreateAttribute("data-minvalue");
                                            attrMinValue.Value = "-2147483647";
                                            xmlColumn.Attributes.Append(attrMinValue);
                                        }
                                        if ((xmlColumn.Attributes["data-maxvalue"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-maxvalue"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-maxvalue"].Value) > Int32.Parse("2147483647")))
                                        {
                                            attrMaxValue = xmlColumn.OwnerDocument.CreateAttribute("data-maxvalue");
                                            attrMaxValue.Value = "2147483647";
                                            xmlColumn.Attributes.Append(attrMaxValue);
                                        }
                                        break;
                                    case "smallint":
                                        if ((xmlColumn.Attributes["data-minvalue"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-minvalue"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-minvalue"].Value) < Int32.Parse("-32767")))
                                        {
                                            attrMinValue = xmlColumn.OwnerDocument.CreateAttribute("data-minvalue");
                                            attrMinValue.Value = "-32767";
                                            xmlColumn.Attributes.Append(attrMinValue);
                                        }
                                        if ((xmlColumn.Attributes["data-maxvalue"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-maxvalue"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-maxvalue"].Value) > Int32.Parse("32767")))
                                        {
                                            attrMaxValue = xmlColumn.OwnerDocument.CreateAttribute("data-maxvalue");
                                            attrMaxValue.Value = "32767";
                                            xmlColumn.Attributes.Append(attrMaxValue);
                                        }
                                        break;
                                    case "numeric":
                                    case "decimal":
                                        if ((xmlColumn.Attributes["data-maxlength"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-maxlength"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-maxlength"].Value) > schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlNumericPrecision))
                                        {
                                            attrMaxValue = xmlColumn.OwnerDocument.CreateAttribute("data-maxlength");
                                            attrMaxValue.Value = schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlNumericPrecision.ToString();
                                            xmlColumn.Attributes.Append(attrMaxValue);
                                        }
                                        if ((xmlColumn.Attributes["data-digits"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-digits"].Value)) || (Int32.Parse(xmlColumn.Attributes["data-digits"].Value) > schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlNumericScale))
                                        {
                                            attrMaxValue = xmlColumn.OwnerDocument.CreateAttribute("data-digits");
                                            attrMaxValue.Value = schema.Modules[moduleName].Form.Tables[tableName].Columns[columnName].SqlNumericScale.ToString();
                                            xmlColumn.Attributes.Append(attrMaxValue);
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            result = GetFormattedXml(xmlForm.OuterXml);
            
            return result;
        }
        //---------------------------------------------------------------------------------------------
        //string GetAddSchemaDataToGridTemplate(string formTemplate, FwApplicationSchema schema, string gridName)
        //{
        //    XmlDocument xmlForm;
        //    XmlNodeList xmlBrowses, xmlUniqueIds, xmlColumns;
        //    string result;
            
        //    xmlForm = new XmlDocument();
        //    try
        //    {
        //        xmlForm.LoadXml(formTemplate);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.Error.WriteLine("***The file " + gridName + "Browse.htm is not valid XML.***");
        //        Console.Error.WriteLine(ex.Message);
        //        //Console.Error.WriteLine(ex.StackTrace);
        //    }
        //    xmlBrowses = xmlForm.SelectNodes("//div[@data-control='FwBrowse']"); //xpath query
        //    foreach(XmlNode xmlBrowse in xmlBrowses)
        //    {
        //        if (schema.Grids[gridName].Browse.Columns.ContainsKey("inactive"))
        //        {
        //            if (xmlBrowse.Attributes["data-hasinactive"] == null)
        //            {
        //                XmlAttribute attrHasInactive;

        //                attrHasInactive       = xmlForm.CreateAttribute("data-hasinactive");
        //                attrHasInactive.Value = "true";
        //                xmlBrowse.Attributes.Append(attrHasInactive); 
        //            }
        //            if (xmlBrowse.Attributes["data-activeinactiveview"] == null)
        //            {
        //                XmlAttribute attrHasInactive;

        //                attrHasInactive       = xmlForm.CreateAttribute("data-activeinactiveview");
        //                attrHasInactive.Value = "active";
        //                xmlBrowse.Attributes.Append(attrHasInactive); 
        //            }
        //        }
                
        //    }
        //    xmlUniqueIds = xmlForm.SelectNodes("//div[@class='field' and @data-isuniqueid='true']"); //xpath query
        //    xmlColumns   = xmlForm.SelectNodes("//div[@class='field' and (not(@data-isuniqueid) or @data-isuniqueid!='true')]"); //xpath query
        //    foreach(XmlNode xmlUniqueId in xmlUniqueIds)
        //    {
        //        string datafield, tableName, columnName;
        //        string[] dataFieldFragments;

        //        if ( ((xmlUniqueId.Attributes["data-formdatafield"] != null) && (xmlUniqueId.Attributes["data-formdatafield"].Value.Contains('.'))) &&
        //                ((xmlUniqueId.Attributes["data-formmaxlength"] == null) || (string.IsNullOrEmpty(xmlUniqueId.Attributes["data-formmaxlength"].Value))) )
        //        {
        //            dataFieldFragments = xmlUniqueId.Attributes["data-formdatafield"].Value.Split(new char[]{'.'});
        //            tableName          = dataFieldFragments[0];
        //            columnName         = dataFieldFragments[1];
        //            if (schema.Grids.ContainsKey(gridName) && schema.Grids[gridName].Form.Tables.ContainsKey(tableName) && schema.Grids[gridName].Form.Tables[tableName].UniqueIds.ContainsKey(columnName))
        //            {
        //                XmlAttribute attrMaxLength;

        //                attrMaxLength = xmlUniqueId.OwnerDocument.CreateAttribute("data-formmaxlength");
        //                attrMaxLength.Value = schema.Grids[gridName].Form.Tables[tableName].UniqueIds[columnName].SqlCharacterMaximumLength.ToString();
        //                xmlUniqueId.Attributes.Append(attrMaxLength);
        //            }
        //        }
        //    }
        //    foreach(XmlNode xmlColumn in xmlColumns)
        //    {
        //        string datafield, tableName, columnName;
        //        string[] dataFieldFragments;

        //        if ( ((xmlColumn.Attributes["data-formdatafield"] != null) && (xmlColumn.Attributes["data-formdatafield"].Value.Contains('.'))) &&
        //                ((xmlColumn.Attributes["data-formmaxlength"] == null) || (string.IsNullOrEmpty(xmlColumn.Attributes["data-formmaxlength"].Value))) )
        //        {
        //            dataFieldFragments = xmlColumn.Attributes["data-formdatafield"].Value.Split(new char[]{'.'});
        //            tableName          = dataFieldFragments[0];
        //            columnName         = dataFieldFragments[1];
        //            if (schema.Grids.ContainsKey(gridName) && schema.Grids[gridName].Form.Tables.ContainsKey(tableName) && schema.Grids[gridName].Form.Tables[tableName].Columns.ContainsKey(columnName))
        //            {
        //                XmlAttribute attrMaxLength;

        //                attrMaxLength = xmlColumn.OwnerDocument.CreateAttribute("data-formmaxlength");
        //                attrMaxLength.Value = schema.Grids[gridName].Form.Tables[tableName].Columns[columnName].SqlCharacterMaximumLength.ToString();
        //                xmlColumn.Attributes.Append(attrMaxLength);
        //            }
        //        }
        //    }
        //    result = GetFormattedXml(xmlForm.OuterXml);

        //    return result;
        //}
        //---------------------------------------------------------------------------------------------
        //string GetAddSchemaDataToValidationBrowseTemplate(string formTemplate, FwApplicationSchema schema, string validationName)
        //{
        //    XmlDocument xmlForm;
        //    XmlNodeList xmlBrowses;
        //    string result;

        //    xmlForm = new XmlDocument();
        //    try
        //    {
        //        xmlForm.LoadXml(formTemplate);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.Error.WriteLine("***The file " + validationName + "Browse.htm is not valid XML.***");
        //        Console.Error.WriteLine(ex.Message);
        //        //Console.Error.WriteLine(ex.StackTrace);
        //    }
        //    xmlBrowses = xmlForm.SelectNodes("//div[@data-control='FwBrowse']"); //xpath query
        //    foreach(XmlNode xmlBrowse in xmlBrowses)
        //    {
        //        if (schema.Validations[validationName].Browse.Columns.ContainsKey("inactive"))
        //        {
        //            if (xmlBrowse.Attributes["data-hasinactive"] == null)
        //            {
        //                XmlAttribute attrHasInactive;

        //                attrHasInactive       = xmlForm.CreateAttribute("data-hasinactive");
        //                attrHasInactive.Value = "true";
        //                xmlBrowse.Attributes.Append(attrHasInactive); 
        //            }
        //            if (xmlBrowse.Attributes["data-activeinactiveview"] == null)
        //            {
        //                XmlAttribute attrHasInactive;

        //                attrHasInactive       = xmlForm.CreateAttribute("data-activeinactiveview");
        //                attrHasInactive.Value = "active";
        //                xmlBrowse.Attributes.Append(attrHasInactive); 
        //            }
        //        }
                
        //    }
        //    result = GetFormattedXml(xmlForm.OuterXml);
            
        //    return result;
        //}
        //---------------------------------------------------------------------------------------------
        string GetFormattedXml(string xml)
        {
            StringBuilder stringBuilder;
            XElement element;
            XmlWriterSettings settings;

            stringBuilder = new StringBuilder();
            element       = XElement.Parse(xml);
            settings      = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = false;
            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }

        string GetTableSchemaQuery(string tablename)
        {
            StringBuilder sb;
            string result, functionname;
            int rightParenIndex;

            rightParenIndex = tablename.IndexOf("(");
            functionname = (rightParenIndex > 0) ? tablename.Substring(0, rightParenIndex).Replace("dbo.", string.Empty) : tablename.Replace("dbo.", string.Empty);
            sb = new StringBuilder();
            sb.AppendLine("select");
            sb.AppendLine("  [table]      = t.name,");
            sb.AppendLine("  [column]     = c.name,");
            sb.AppendLine("  [datatype]   = ty.name,");
            sb.AppendLine("  [maxlength]  = c.max_length,");
            sb.AppendLine("  [precision]  = c.precision,");
            sb.AppendLine("  [scale]      = c.scale,");
            sb.AppendLine("  [isidentity] = c.is_identity,");
            sb.AppendLine("  [isnullable] = c.is_nullable");
            sb.AppendLine("from sys.tables t join sys.columns c on (t.[object_id]  = c.[object_id])");
            sb.AppendLine("                  join sys.types ty  on (c.user_type_id = ty.user_type_id)");
            sb.AppendLine("where t.name = '" + tablename + "'");
            sb.AppendLine("union all");
            sb.AppendLine("select");
            sb.AppendLine("  [table]      = v.name,");
            sb.AppendLine("  [column]     = c.name,");
            sb.AppendLine("  [datatype]   = ty.name,");
            sb.AppendLine("  [maxlength]  = c.max_length,");
            sb.AppendLine("  [precision]  = c.precision,");
            sb.AppendLine("  [scale]      = c.scale,");
            sb.AppendLine("  [isidentity] = c.is_identity,");
            sb.AppendLine("  [isnullable] = c.is_nullable");
            sb.AppendLine("from sys.views v join sys.columns c on (v.[object_id]  = c.[object_id])");
            sb.AppendLine("                 join sys.types ty  on (c.user_type_id = ty.user_type_id)");
            sb.AppendLine("where v.name = '" + tablename + "'");
            sb.AppendLine("union all");
            sb.AppendLine("select");
            sb.AppendLine("  [table]      = o.name,");
            sb.AppendLine("  [column]     = c.name,");
            sb.AppendLine("  [datatype]   = ty.name,");
            sb.AppendLine("  [maxlength]  = c.max_length,");
            sb.AppendLine("  [precision]  = c.precision,");
            sb.AppendLine("  [scale]      = c.scale,");
            sb.AppendLine("  [isidentity] = c.is_identity,");
            sb.AppendLine("  [isnullable] = c.is_nullable");
            sb.AppendLine("from sys.objects o join sys.columns c on (o.[object_id]  = c.[object_id])");
            sb.AppendLine("                   join sys.types ty  on (c.user_type_id = ty.user_type_id)");
            sb.AppendLine("where o.name = '" + functionname + "'");
            sb.AppendLine("  and type IN ('FN', 'IF', 'TF')");
            result = sb.ToString();

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }

    public class BuildData
    {

    }

    [XmlRoot("JSAppBuilderConfig")]
    public class JSAppBuilderConfig
    {
        public string DatabaseConnection {get;set;}
        
        [XmlArray("Fields")]
        [XmlArrayItem("Field")]
        public List<Field> Fields {get;set;}
        
        [XmlArray("MergeSections")]
        [XmlArrayItem("MergeSection")]
        public List<MergeSection> MergeSections {get;set;}
        
        [XmlArray("SourceFiles")]
        [XmlArrayItem("SourceFile")]
        public List<SourceFile> SourceFiles {get;set;}
        
        [XmlArray("Targets")]
        [XmlArrayItem("Target")]
        public List<Target> Targets {get;set;}
    }

    [XmlRoot("Field")]
    public class Field
    {
        [XmlAttribute("Publish")]
        public bool Publish {get;set;}
        
        [XmlAttribute("Key")]
        public string Key {get;set;}
        
        [XmlAttribute("Value")]
        public string Value {get;set;}
    }

    [XmlRoot("MergeSection")]
    public class MergeSection
    {        
        [XmlElement("ReplaceField")]
        public string ReplaceField {get;set;}

        [XmlElement("Template")]
        public string Template {get;set;}
        
        [XmlArray("MergeFiles")]
        [XmlArrayItem("MergeFile")]
        public List<MergeFile> MergeFiles {get;set;}
    }

    [XmlRoot("MergeFile")]
    public class MergeFile
    {        
        [XmlAttribute("Module")]
        public string Module {get;set;}

        [XmlAttribute("TemplateType")]
        public string TemplateType {get;set;}

        [XmlAttribute("DatabaseConnection")]
        public string DatabaseConnection {get;set;}
        
        [XmlElement("Template")]
        public string Template {get;set;}
        
        [XmlElement("Uri")]
        public string Uri {get;set;}
    }

    [XmlRoot("SourceFile")]
    public class SourceFile
    {        
        [XmlElement("ReplaceField")]
        public string ReplaceField {get;set;}
        
        [XmlElement("Template")]
        public string Template {get;set;}
        
        [XmlArray("InputFiles")]
        [XmlArrayItem("Uri")]
        public List<String> InputFiles {get;set;}
        
        [XmlElement("Minify")]
        public bool Minify {get;set;}
        
        [XmlElement("OutputFile")]
        public string OutputFile {get;set;}
        
        [XmlElement("MinifiedFile")]
        public string MinifiedFile {get;set;}
    }

    [XmlRoot("Target")]
    public class Target
    {
        [XmlElement("Publish")]
        public bool Publish { get; set; } = false;

        [XmlElement("Minify")]
        public bool Minify { get; set; } = false;

        //[XmlElement("AddBaseUrlToSourceFiles")]
        public bool AddBaseUrlToSourceFiles { get; set; } = false;

        [XmlElement("OutputDirectory")]
        public string OutputDirectory { get; set; } = string.Empty;

        [XmlArray("Files")]
        [XmlArrayItem("File")]
        public List<TargetFile> Files { get; set; } = new List<TargetFile>();
    }

    [XmlRoot("File")]
    public class TargetFile
    {
        [XmlAttribute("AddBaseUrl")]
        public bool AddBaseUrl { get; set; } = false;

        [XmlElement("InputFile")]
        public string InputFile { get; set; } = string.Empty;

        [XmlElement("OutputFile")]
        public string OutputFile { get; set; } = string.Empty;
    }

    [XmlRoot("DatabaseConnection")]
    public class DatabaseConnection
    {
        [XmlAttribute("Name")]
        public string Name {get;set;}
        
        [XmlElement("Server")]
        public string Server {get;set;}

        [XmlElement("Database")]
        public string Database {get;set;}
    }
}

