using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FwStandard.Reporting
{
    public class FwReportEmailInfo
    {
        [Required]
        [MaxLength(255)]
        public string from { get; set; }

        [Required]
        [MaxLength(255)]
        public string to { get; set; }

        [MaxLength(255)]
        public string cc { get; set; }

        [MaxLength(255)]
        public string subject { get; set; }

        // max length could be longer here, this is just an arbitrary value
        [MaxLength(8000)]
        public string body { get; set; }
    }
}
