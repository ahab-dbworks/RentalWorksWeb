using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FwStandard.Models
{
    public class ValidateDuplicateRequest
    {
        [Required]
        Dictionary<string, ValidateDuplicateRequestField> fields { get; set; }
    }

    public class ValidateDuplicateRequestField
    {
        [Required]
        public string datafield { get; set; }
        [Required]
        public object value { get; set; }
        [Required]
        public string type { get; set; }
    }
}
