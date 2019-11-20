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

    public class FwAppImageAddAsyncRequest
    {
        public string Uniqueid1 { get; set; }
        public string Uniqueid2 { get; set; }
        public string Uniqueid3 { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public string RecType { get; set; }
        public string ImageDataUrl { get; set; }
    }

    public class FwAppImageDeleteAsyncRequest
    {
        public string AppImageId { get; set; }
    }
}
