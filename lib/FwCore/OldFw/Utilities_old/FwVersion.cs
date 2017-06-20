using System.IO;

namespace FwCore.Utilities
{
    public class FwVersion
    {
        public int Major    {get;private set;}
        public int Minor    {get;private set;}
        public int Build    {get;private set;}
        public int Revision {get;private set;}
        public string DotNetFramework {get;private set;}
        public string ShortVersion {get;private set;}
        public string FullVersion  {get;private set;}
        //---------------------------------------------------------------------------------------------
        public static FwVersion Current
        {
            get
            {
                if (FwVersion.FwCurrent == null)
                {
                    using (FileStream fsVersion = new FileStream(HttpContext.Current.Server.MapPath("~/version.txt"), FileMode.Open, FileAccess.Read))
                    {
                        byte[] buffer;
                        string version;
                
                        buffer = new byte[fsVersion.Length];
                        fsVersion.Read(buffer, 0, buffer.Length);
                        version = System.Text.Encoding.ASCII.GetString(buffer);
                        FwVersion.SetVersion(version);
                    }
                }
                return FwCurrent;
            }
            private set
            {
                FwCurrent = value;
            }
        }
        private static FwVersion FwCurrent;
        //---------------------------------------------------------------------------------------------
        public static void SetVersion(string version)
        {
            int major, minor, build, revision;
            string[] versionFragments;
            
            FwVersion.Current = new FwVersion();
            versionFragments = version.Split(new char[] {'.'});
            major    = FwConvert.ToInt32(versionFragments[0]);
            minor    = FwConvert.ToInt32(versionFragments[1]);
            build    = FwConvert.ToInt32(versionFragments[2]);
            revision = FwConvert.ToInt32(versionFragments[3]);
            FwVersion.Current.Major           = major;
            FwVersion.Current.Minor           = minor;
            FwVersion.Current.Build           = build;
            FwVersion.Current.Revision        = revision;
            FwVersion.Current.ShortVersion    = FwVersion.Current.Major + "." + FwVersion.Current.Minor.ToString();
            FwVersion.Current.FullVersion     = FwVersion.Current.Major + "." + FwVersion.Current.Minor.ToString() + "." + FwVersion.Current.Build.ToString() + "." + FwVersion.Current.Revision.ToString();
            //FwVersion.Current.DotNetFramework = assembly.ImageRuntimeVersion;
        }
        //---------------------------------------------------------------------------------------------
    }
}
