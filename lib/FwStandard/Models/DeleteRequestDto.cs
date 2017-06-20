using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Models
{
    public class DeleteRequestDto
    {
        public Dictionary<string, object> uniqueIds { get; set; }

        public DeleteRequestDto()
        {

        }
    }
}
