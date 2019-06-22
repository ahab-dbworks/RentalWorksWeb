using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class FwAppImageModel
    {
        public string AppImageId { get; set; }
        public string DateStamp { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string RecType { get; set; }
        public int OrderBy { get; set; }
        public byte[] Image { get; set; }
    }
}
