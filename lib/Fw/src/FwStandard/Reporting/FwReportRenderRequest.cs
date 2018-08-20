using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportRenderRequest
    {
        public FwReportRenderModes renderMode { get; set; }
        public Dictionary<string, object> parameters { get; set; }
        public FwReportEmailInfo email { get; set; }
        public bool downloadPdfAsAttachment { get; set; }
    }
}
