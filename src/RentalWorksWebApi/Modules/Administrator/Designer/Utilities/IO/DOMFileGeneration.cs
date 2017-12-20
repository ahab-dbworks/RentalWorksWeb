using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBWorksDesigner.Utilities.IO
{
    public class DOMFileManipulation
    {
        public string path { get; set; }
        public string folder { get; set; }
        private string path_and_folder { get; set; }

        public DOMFileManipulation(string path = "C:\\", string folder = "MyFolder")
        {
            this.path = path;
            if (!this.path.EndsWith("\\"))
            {
                this.path = this.path + "\\";
            }
            this.folder = folder.Trim();
            this.path_and_folder = this.path + this.folder;
                
        }

        public bool createFolder()
        {
            var hasCreated = false;
            try
            {
                DirectoryInfo di = Directory.CreateDirectory(this.path_and_folder);
                if (di.Exists)
                {
                    hasCreated = true;
                }
            }
            catch (Exception e)
            {
                Security.Logging.RecordError(e);
            }
            return hasCreated;
        }

        public bool createDOMFiles(string fileName, Enums.DOM domType, string defaultContent = "Hello World.")
        {
            var hasCreated = false;
            var fullFileName = fileName + "." + domType.ToString().ToLower();
            var fullPath = this.path_and_folder + "\\" + fullFileName;

            try
            {
                using (FileStream fs = File.Create(fullPath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(defaultContent);
                    fs.Write(info, 0, info.Length);
                    hasCreated = true;
                }
            }

            catch (Exception ex)
            {
                Security.Logging.RecordError(ex);
            }

            return hasCreated;
        }        

        public void updateDOMFile(string fileName, string fileContent, string path)
        {
            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }

            var fullPath = path + fileName;

            File.WriteAllText(fullPath, fileContent, Encoding.UTF8);
        }

        public string readDOMContents(string fileName, Enums.DOM domType)
        {
            List<string> list = new List<string>();
            var file = fileName + "." + domType.ToString();
            using (var reader = new StreamReader(new FileStream(this.path_and_folder + "\\" + file, FileMode.Open)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line + Environment.NewLine);
                }
            }
            return string.Join("", list);
        }

        public static string readFileContents(string filePathAndName)
        {
            List<string> list = new List<string>();
            using (var reader = new StreamReader(new FileStream(filePathAndName, FileMode.Open)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line + Environment.NewLine);
                }
            }
            return string.Join("", list);
        }

    }
}
