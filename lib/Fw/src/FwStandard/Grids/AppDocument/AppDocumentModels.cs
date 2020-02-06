using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Grids.AppDocument
{
    public class AppDocumentGetRequest
    {
        public string DocumentTypeId { get; set; }
        public string Description { get; set; }
    }


    public class AppDocumentPostRequest
    {
        public string DocumentTypeId { get; set; }
        public string Description { get; set; }
        public bool? AttachToEmail { get; set; }
        public bool? Inactive { get; set; }
        public bool? FileIsModified { get; set; }
        public string FileDataUrl { get; set; }
        public string FilePath { get; set; }
    }

    public class AppDocumentPutRequest : AppDocumentPostRequest
    {
        public string AppDocumentId { get; set; }
    }
}
