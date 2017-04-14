using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fw.Json.Services.Common
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class FwJsonServiceMethodAttribute : System.Attribute
    {
        public string RequiredParameters { get;set; } = string.Empty;
        public string OptionalParameters { get;set; } = string.Empty;
    }
}
