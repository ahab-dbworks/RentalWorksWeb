using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Build.Utilities;

namespace Fw.MSBuildTasks
{
    public class FwFuncMSBuild
    {
        //------------------------------------------------------------------------------------------------
        public static string FileToString(string path, TaskLoggingHelper log)
        {
            StreamReader reader = null;;
            string str = string.Empty;
            try
            {
                reader = new StreamReader(path);
                str = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                log.LogErrorFromException(ex, true);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return str;
        }
        //------------------------------------------------------------------------------------------------
        public static void SaveObject(object o, string path, TaskLoggingHelper log)
        {
            StreamWriter writer = null;
            XmlSerializer serializer = null;
            try
            {
                writer = new StreamWriter(path);            
                serializer = new XmlSerializer(o.GetType());
                serializer.Serialize(writer, o);
            }
            catch (Exception ex)
            {
                log.LogErrorFromException(ex, true);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        //------------------------------------------------------------------------------------------------
        public static object LoadObject(Type type, string path, string solutionDir, TaskLoggingHelper log)
        {
            object o = null;

            try
            {
                StringBuilder sb = new StringBuilder(File.ReadAllText(path));
                sb.Replace("$(SolutionDir)", solutionDir);
                string xml = sb.ToString();
                using (StringReader reader = new StringReader(xml))
                {
                    XmlSerializer serializer = new XmlSerializer(type);
                    o = serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                log.LogErrorFromException(ex, true);
            }
            return o;
        }
        //------------------------------------------------------------------------------------------------
        public static string AddBackslashToDirectory(string path)
        {
            if(path[path.Length - 1] != '\\')
            {
                path += @"\";   
            }
            return path;
        }
        //------------------------------------------------------------------------------------------------
    }
}
