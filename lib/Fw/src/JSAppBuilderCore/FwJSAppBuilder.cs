using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace JSAppBuilderCore
{
    public class FwJSAppBuilder
    {
        [Required]
        public string ConfigFilePath { get; set; } = string.Empty;
        //[Required]
        public string SolutionDir { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public bool Publish { get; set; } = false;
        public bool AttachDebugger { get; set; } = false;
        public bool AjaxLoadHtml { get; set; } = false;
        //---------------------------------------------------------------------------------------------
        public FwJSAppBuilder() : base()
        {

        }
        //---------------------------------------------------------------------------------------------
        public bool Build(string configFilePath, string solutionDir)
        {
            bool success;
            JSAppBuilderConfig config;
            string mergeFile_inputFileUri, mergeFile_inputFileText, mergeFile_mergeFragment,
                sourceFile_inputFileUri, sourceFile_inputFileText,
                sourceFiles_outputFile, sourceFiles_combinedOutputPath;
            List<string> html_outputFilePath = new List<string>(), html_inputFilePath = new List<string>();
            List<StringBuilder> html_outputFileText = new List<StringBuilder>();
            StringBuilder sourceFiles, sourceFiles_combinedText, mergeFiles;
            MergeFile mergeFile;

            // page and controls
            StringBuilder sbModules;
            string fileBrowseTemplate = null, fileFormTemplate = null, pathBrowseTemplate, pathFormTemplate, pathSite,
                pathModules, nameModule;
            string[] pathModuleArray;
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
                    string[] versionFragments = this.Version.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

                    config = LoadObject<JSAppBuilderConfig>(configFilePath, solutionDir);
                    pathSite = Path.GetDirectoryName(configFilePath);

                    for (int i = 0; i < config.Targets.Count; i++)
                    {
                        if (this.Publish == config.Targets[i].Publish)
                        {
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
                                    mergeFile = config.MergeSections[j].MergeFiles[k];
                                    mergeFile_inputFileUri = ApplyFields(mergeFile.Uri, config, config.Targets[i].Publish);
                                    mergeFile_inputFileText = UriToString(mergeFile_inputFileUri);
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
                                pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Controls");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate = null;
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
                                pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Pages");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate = null;
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
                                pathModules = Path.Combine(pathSite, @"source/Controls");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate = null;
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
                                pathModules = Path.Combine(pathSite, @"source/Pages");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                    foreach (string pathModule in pathModuleArray)
                                    {
                                        string filePageTemplate = null;
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
                                pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Modules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate)) fileFormTemplate = File.ReadAllText(pathFormTemplate);
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
                                pathModules = Path.Combine(pathSite, @"libraries/Fw/source/SubModules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate)) fileFormTemplate = File.ReadAllText(pathFormTemplate);
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
                                pathModules = Path.Combine(pathSite, @"source/Modules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        DirectoryInfo moduleDirectoryInfo = new DirectoryInfo(pathModule);
                                        nameModule = moduleDirectoryInfo.Name;
                                        fileBrowseTemplate = null;
                                        fileFormTemplate = null;
                                        FileInfo[] fileInfos = moduleDirectoryInfo.GetFiles("*.htm").OrderBy(path => path.FullName).ToArray();
                                        foreach (FileInfo fileInfo in fileInfos)
                                        {
                                            if (fileInfo.Name.EndsWith("Browse.htm") /*|| fileInfo.Name.EndsWith("Browse.html")*/)
                                            {
                                                fileBrowseTemplate = File.ReadAllText(fileInfo.FullName);
                                            }
                                            else if (fileInfo.Name.EndsWith("Form.htm") /*|| fileInfo.Name.EndsWith("Form.html")*/)
                                            {
                                                fileFormTemplate = File.ReadAllText(fileInfo.FullName);
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
                                                    if (this.AjaxLoadHtml)
                                                    {
                                                        string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + fileInfo.Name;
                                                        urlHtml = ApplyFields(urlHtml, config, Publish);
                                                        sbModules.AppendLine("<script id=\"tmpl-modules-" + fileInfo.Name.Replace(".htm", string.Empty) + "\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                                    }
                                                    else
                                                    {
                                                        string htmlTemplate = File.ReadAllText(fileInfo.FullName);
                                                        sbModules.AppendLine("<script id=\"tmpl-modules-" + fileInfo.Name.Replace(".htm", string.Empty) + "\" type=\"text/html\">");
                                                        sbModules.AppendLine(htmlTemplate);
                                                        sbModules.AppendLine("</script>");
                                                    }
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
                                                if (this.AjaxLoadHtml)
                                                {
                                                    string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                                    urlHtml = ApplyFields(urlHtml, config, Publish);
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                                }
                                                else
                                                {
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Browse\" type=\"text/html\">");
                                                    sbModules.AppendLine(fileBrowseTemplate);
                                                    sbModules.AppendLine("</script>");
                                                }
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
                                                if (this.AjaxLoadHtml)
                                                {
                                                    string urlHtml = "{{AppUri}}Source/Modules/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Form.htm";
                                                    urlHtml = ApplyFields(urlHtml, config, Publish);
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                                }
                                                else
                                                {
                                                    sbModules.AppendLine("<script id=\"tmpl-modules-" + nameModule + "Form\" type=\"text/html\">");
                                                    sbModules.AppendLine(fileFormTemplate);
                                                    sbModules.AppendLine("</script>");
                                                }
                                            }
                                        }
                                    }
                                }
                                // Application SubModules
                                pathModules = Path.Combine(pathSite, @"source/SubModules");
                                if (Directory.Exists(pathModules))
                                {
                                    pathModuleList = getModuleDirectories(pathModules);
                                    foreach (string pathModule in pathModuleList)
                                    {
                                        nameModule = new DirectoryInfo(pathModule).Name;
                                        pathBrowseTemplate = Path.Combine(pathModule, nameModule + "Browse.htm");
                                        pathFormTemplate = Path.Combine(pathModule, nameModule + "Form.htm");
                                        if (File.Exists(pathBrowseTemplate)) fileBrowseTemplate = File.ReadAllText(pathBrowseTemplate);
                                        if (File.Exists(pathFormTemplate)) fileFormTemplate = File.ReadAllText(pathFormTemplate);
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
                            pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Grids");
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
                            pathModules = Path.Combine(pathSite, @"source/Grids");
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
                                        if (this.AjaxLoadHtml)
                                        {
                                            string urlHtml = "{{AppUri}}Source/Grids/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                            urlHtml = ApplyFields(urlHtml, config, Publish);
                                            sbModules.AppendLine("<script id=\"tmpl-grids-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                        }
                                        else
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-grids-" + nameModule + "Browse\" type=\"text/html\">");
                                            sbModules.AppendLine(fileBrowseTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
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
                            pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Validations");
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
                            pathModules = Path.Combine(pathSite, @"source/Validations");
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
                                        if (this.AjaxLoadHtml)
                                        {
                                            string urlHtml = "{{AppUri}}Source/Validations/" + pathModule.Substring(pathModules.Length + 1).Replace("\\", "/") + "/" + nameModule + "Browse.htm";
                                            urlHtml = ApplyFields(urlHtml, config, Publish);
                                            sbModules.AppendLine("<script id=\"tmpl-validations-" + nameModule + "Browse\" type=\"text/html\" src=\"" + urlHtml + "\" data-ajaxload=\"true\"></script>");
                                        }
                                        else
                                        {
                                            sbModules.AppendLine("<script id=\"tmpl-validations-" + nameModule + "Browse\" type=\"text/html\">");
                                            sbModules.AppendLine(fileBrowseTemplate);
                                            sbModules.AppendLine("</script>");
                                        }
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
                                pathModules = Path.Combine(pathSite, @"libraries/Fw/source/Reports");
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
                                pathModules = Path.Combine(pathSite, @"source/Reports");
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
                                    string fwpath = Path.Combine(pathSite, @"libraries/Fw/");

                                    jsFiles = new List<string>();

                                    jspath = Path.Combine(pathSite, @"libraries/Fw/source");
                                    if (Directory.Exists(jspath))
                                    {
                                        newInputFiles = Directory.GetFiles(jspath, "*.js", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        for (int jsfileno = 0; jsfileno < newInputFiles.Length; jsfileno++)
                                        {
                                            newInputFiles[jsfileno] = "{{FwFrontEndLibraryUri}}" + newInputFiles[jsfileno].Substring(fwpath.Length + 1);
                                        }
                                        jsFiles.AddRange(newInputFiles);
                                    }
                                    if (Directory.Exists(Path.Combine(pathSite, @"source")))
                                    {
                                        newInputFiles = Directory.GetFiles(Path.Combine(pathSite, @"source"), "*.js", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
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
                                                        jsFiles[uriNo] = jsFiles[uriNo].Replace(field.Value + "/", fieldKey);
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

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/Grids");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/Modules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/Pages");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/Reports");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "FrontEnd.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/SubModules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"libraries/Fw/source/Controls/Modules/Validations");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/Controls");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/Grids");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/Modules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/Pages");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, $"source/Reports");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "FrontEnd.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/SubModules");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
                                        cssFiles.AddRange(newInputFiles);
                                    }

                                    csspath = Path.Combine(pathSite, @"source/Validations");
                                    if (Directory.Exists(csspath))
                                    {
                                        newInputFiles = Directory.GetFiles(csspath, "*.css", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
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
                                                        cssFiles[uriNo] = cssFiles[uriNo].Replace(field.Value + "/", fieldKey);
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
                                                .Replace("{{FwFrontEndLibraryUri}}", "[appbaseurl]")
                                                .Replace("{{AppUri}}", "[appbaseurl]");
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
                                        string inputFileUri = ApplyFields(config.SourceFiles[j].InputFiles[k], config, config.Targets[i].Publish);
                                        string inputFileTagTemplate = ApplyFields(config.SourceFiles[j].Template, config, config.Targets[i].Publish)
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
                                }
                                else  // Publish is true
                                {
                                    #region create file: release verson of source file
                                    sourceFiles_outputFile = Path.Combine(config.Targets[i].OutputDirectory, config.SourceFiles[j].OutputFile.Replace("{{Version}}", Version));
                                    sourceFiles_combinedOutputPath = GetAbsolutePath(sourceFiles_outputFile);
                                    sourceFiles_combinedText = new StringBuilder();
                                    for (int k = 0; k < config.SourceFiles[j].InputFiles.Count; k++)
                                    {
                                        sourceFile_inputFileUri = ApplyFields(config.SourceFiles[j].InputFiles[k], config, config.Targets[i].Publish);
                                        sourceFile_inputFileText = UriToString(sourceFile_inputFileUri);
                                        sourceFiles_combinedText.AppendLine(sourceFile_inputFileText);
                                    }
                                    if (File.Exists(sourceFiles_combinedOutputPath))
                                    {
                                        File.Delete(sourceFiles_combinedOutputPath);
                                    }
                                    new FileInfo(sourceFiles_combinedOutputPath).Directory.Create();
                                    File.WriteAllText(sourceFiles_combinedOutputPath, NormalizeLineEndings(sourceFiles_combinedText.ToString()));

                                    sourceFiles.AppendLine(config.SourceFiles[j].Template.
                                            Replace("{{File}}", "[appbaseurl][appvirtualdirectory]" + config.SourceFiles[j].OutputFile.
                                            Replace("{{Version}}", Version)));

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
            finally
            {
            }

            return success;
        }
        //---------------------------------------------------------------------------------------------
        public static T LoadObject<T>(string path, string solutionDir)
        {
            T o = default(T);

            try
            {
                StringBuilder sb = new StringBuilder(File.ReadAllText(path));
                sb.Replace("$(SolutionDir)", solutionDir);
                string xml = sb.ToString();
                using (StringReader reader = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    o = (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return o;
        }
        //---------------------------------------------------------------------------------------------
        private List<string> getModuleDirectories(string pathModules)
        {
            string[] pathModuleFileArray;
            List<string> modules;

            modules = new List<string>();
            pathModuleFileArray = Directory.GetFiles(pathModules, "*.htm", SearchOption.AllDirectories).OrderBy(path => path).ToArray();
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
        static string NormalizeLineEndings(string text)
        {
            string result = Regex.Replace(text, @"\r\n|\n\r|\n|\r", "\r\n");
            return result;
        }
        //---------------------------------------------------------------------------------------------
        static string GetAbsolutePath(string path)
        {
            path = path.Replace('/', Path.DirectorySeparatorChar)
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
                    result = result.Replace(field.Key, value);
                }
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        string UriToString(string uri)
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
                    throw new Exception("Unable to load uri: " + uri, ex);
                }
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
    }

    [XmlRoot("JSAppBuilderConfig")]
    public class JSAppBuilderConfig
    {
        public string DatabaseConnection { get; set; }

        [XmlArray("Fields")]
        [XmlArrayItem("Field")]
        public List<Field> Fields { get; set; }

        [XmlArray("MergeSections")]
        [XmlArrayItem("MergeSection")]
        public List<MergeSection> MergeSections { get; set; }

        [XmlArray("SourceFiles")]
        [XmlArrayItem("SourceFile")]
        public List<SourceFile> SourceFiles { get; set; }

        [XmlArray("Targets")]
        [XmlArrayItem("Target")]
        public List<Target> Targets { get; set; }
    }

    [XmlRoot("Field")]
    public class Field
    {
        [XmlAttribute("Publish")]
        public bool Publish { get; set; }

        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }

    [XmlRoot("MergeSection")]
    public class MergeSection
    {
        [XmlElement("ReplaceField")]
        public string ReplaceField { get; set; }

        [XmlElement("Template")]
        public string Template { get; set; }

        [XmlArray("MergeFiles")]
        [XmlArrayItem("MergeFile")]
        public List<MergeFile> MergeFiles { get; set; }
    }

    [XmlRoot("MergeFile")]
    public class MergeFile
    {
        [XmlAttribute("Module")]
        public string Module { get; set; }

        [XmlAttribute("TemplateType")]
        public string TemplateType { get; set; }

        [XmlAttribute("DatabaseConnection")]
        public string DatabaseConnection { get; set; }

        [XmlElement("Template")]
        public string Template { get; set; }

        [XmlElement("Uri")]
        public string Uri { get; set; }
    }

    [XmlRoot("SourceFile")]
    public class SourceFile
    {
        [XmlElement("ReplaceField")]
        public string ReplaceField { get; set; }

        [XmlElement("Template")]
        public string Template { get; set; }

        [XmlArray("InputFiles")]
        [XmlArrayItem("Uri")]
        public List<String> InputFiles { get; set; }

        //[XmlElement("Minify")]
        //public bool Minify { get; set; }

        [XmlElement("OutputFile")]
        public string OutputFile { get; set; }

        //[XmlElement("MinifiedFile")]
        //public string MinifiedFile { get; set; }
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
        public string Name { get; set; }

        [XmlElement("Server")]
        public string Server { get; set; }

        [XmlElement("Database")]
        public string Database { get; set; }
    }
}

