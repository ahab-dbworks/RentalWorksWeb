using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class EmailPdfRequest
    {
        public string from { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public string cc { get; set; } = string.Empty;
        public string subject { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
}
