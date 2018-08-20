using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportEmailInfo
    {
        public string from { get; set; }
        public string to { get; set; }
        public string cc { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
