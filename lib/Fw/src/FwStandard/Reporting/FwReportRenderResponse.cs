using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportRenderResponse
    {
        public string renderMode { get; set; }
        public string htmlReportUrl { get; set; }
        public string pdfReportUrl { get; set; }
        public string consoleOutput { get; set; }
    }
}
