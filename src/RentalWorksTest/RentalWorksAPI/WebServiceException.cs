using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksTest.RentalWorksAPI
{
    public class WebServiceException
    {
        public string Message { get; set; } = string.Empty;
        public string ExceptionMessage { get; set; } = string.Empty;
        public string ExceptionType { get; set; } = string.Empty;
        public string StackTrace { get; set; }
    }
}
