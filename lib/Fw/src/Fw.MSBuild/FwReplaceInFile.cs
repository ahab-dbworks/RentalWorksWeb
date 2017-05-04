using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Fw.MSBuildTasks
{
    public class FwReplaceInFile : Task
    {
        [Required]
        public string Path{get;set;}
        public string Pattern{get;set;}
        public string Replacement{get;set;}
        //---------------------------------------------------------------------------------------------
        public FwReplaceInFile()
        {
            Path        = string.Empty;
            Pattern     = string.Empty;
            Replacement = string.Empty;
        }
        //---------------------------------------------------------------------------------------------
        void ReplaceInFile(string path, string pattern, string replacement, RegexOptions options)
        {
            string text = File.ReadAllText(path);
            text = Regex.Replace(text, pattern, replacement, options); 
            File.WriteAllText(path, text);
        }
        //---------------------------------------------------------------------------------------------
        public override bool Execute()
        {                             
            ReplaceInFile(Path, Pattern, Replacement, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return true;    
        }
        //---------------------------------------------------------------------------------------------
    }
}
