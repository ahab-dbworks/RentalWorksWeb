using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class UpdateRequestDto
    {
        public Dictionary<string, object> uniqueIds { get; set; }
        public Dictionary<string, object> fields { get; set; }

        public UpdateRequestDto()
        {

        }
    }
}
