using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBWorksDesigner.Logic.Administrative
{
    public class File
    {
        public string fileName { get; set; }
        public string fileContents { get; set; }
        public string ext { get; set; }
        public string path { get; set; }
        public bool isEditable { get; set; }
        public bool hasChanged { get; set; }       

    }
}
