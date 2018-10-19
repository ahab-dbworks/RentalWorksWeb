using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportRenderRequest
    {
        private List<string> AllowedRenderModes = new List<string>() { "Html", "Pdf", "Email" };
        private string _renderMode = "Html";
        public string renderMode
        {
            get
            {
                return _renderMode;
            }
            set
            {
                if (!AllowedRenderModes.Contains(value))
                {
                    throw new ArgumentException($"Invalid renderMode '{value}', allowed value:'Html', 'Pdf', or 'Email'");
                }
                _renderMode = value;
            }
        }
        public Dictionary<string, object> parameters { get; set; }
        public FwReportEmailInfo email { get; set; }
        public bool downloadPdfAsAttachment { get; set; }
    }
}
