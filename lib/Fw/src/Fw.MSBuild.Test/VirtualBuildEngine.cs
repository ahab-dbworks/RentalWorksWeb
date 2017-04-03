using System;
using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace Fw.MSBuild.Test
{
    //http://stackoverflow.com/questions/260847/unit-test-msbuild-custom-task-without-task-attempted-to-log-before-it-was-initi
    public class VirtualBuildEngine : IBuildEngine
    {
        // fields
        public List<BuildErrorEventArgs> LogErrorEvents = new List<BuildErrorEventArgs>();
        public List<BuildMessageEventArgs> LogMessageEvents = new List<BuildMessageEventArgs>();
        public List<CustomBuildEventArgs> LogCustomEvents = new List<CustomBuildEventArgs>();
        public List<BuildWarningEventArgs> LogWarningEvents = new List<BuildWarningEventArgs>();
        
        // properties
        public int ColumnNumberOfTaskNode { get { return 0; } }
        public bool ContinueOnError { get { throw new NotImplementedException(); } }
        public int LineNumberOfTaskNode { get { return 0; } }
        public string ProjectFileOfTaskNode { get { return "virtual ProjectFileOfTaskNode"; } }
        
        // methods
        public bool BuildProjectFile(string projectFileName, string[] targetNames, System.Collections.IDictionary globalProperties, System.Collections.IDictionary targetOutputs) { throw new NotImplementedException(); }
        public void LogCustomEvent(CustomBuildEventArgs e) { LogCustomEvents.Add(e); }
        public void LogErrorEvent(BuildErrorEventArgs e) { LogErrorEvents.Add(e); }
        public void LogMessageEvent(BuildMessageEventArgs e) { LogMessageEvents.Add(e); }
        public void LogWarningEvent(BuildWarningEventArgs e) { LogWarningEvents.Add(e); }
    }
}
