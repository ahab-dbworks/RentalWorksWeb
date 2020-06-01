using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportRenderRequest
    {
        private List<string> AllowedRenderModes = new List<string>() { "Html", "Pdf", "Email", "EmailImage" };
        private string _renderMode = "Html";
        /// <summary>
        /// Valid values: Html, Pdf, Email, EmailImage
        /// </summary>
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
                    throw new ArgumentException($"Invalid renderMode '{value}', allowed values:'Html', 'Pdf', 'Email', 'EmailImage'");
                }
                _renderMode = value;
            }
        }
        public Dictionary<string, object> parameters { get; set; }
        public FwReportEmailInfo email { get; set; }
        public bool downloadPdfAsAttachment { get; set; }

        /// <summary>
        /// Options for render mode 'EmailImage'
        /// </summary>
        public FwReportRenderRequestEmailImageOptions emailImageOptions { get; set; }
    }

    public class FwReportRenderRequestEmailImageOptions
    {
        public int Width = 640;
        public int Height = 480;
    }

}
