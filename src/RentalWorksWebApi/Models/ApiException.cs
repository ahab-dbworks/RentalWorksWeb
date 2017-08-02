using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalWorksWebApi.Models
{
    public class ApiException
    {
        public int StatusCode { get; set; } = 0;
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}
