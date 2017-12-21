using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace DBWorksDesigner.Logic.Administrative
{
    public class Folder
    {
        public string folderName { get; set; }
        public string parentPath { get; set; }
        public List<Administrative.File> files { get; set; }

        public List<string> GetPathFolderAndFileStructure(string path)
        {
            var files = new List<string>();

            if (DisregardInvalidPath(path))
            {
                var filter = "*";
                var originalFiles = Directory.GetFiles(path, filter, SearchOption.AllDirectories);

                // for efficiency sake, lets remove part of the path we already know on every result. this will reduce the payload size when sending it back out.
                for(int i = 0; i < originalFiles.Length; i++)
                {
                    originalFiles[i] = originalFiles[i].Replace(path + "\\", "");
                }

                files = originalFiles.ToList<string>();

            }
            else
            {
                throw new Exception("invalid path");
            }

            return files;
        }

        private bool DisregardInvalidPath(string path)
        {
            var isValid = true;
            var inValidPaths = new List<string>() { "Program Files", "Program Files (x86)", "ProgramData", "Windows" };

            foreach(var invalidPath in inValidPaths)
            {
                if (path.Contains(invalidPath))
                {
                    isValid = false;
                }

                if (path == "C:\\") {

                    isValid = false;

                }

            }

            return isValid;
        }
    }
}
