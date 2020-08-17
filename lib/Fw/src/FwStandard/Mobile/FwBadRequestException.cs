using System;
using System.Collections.Generic;
using System.Text;

namespace FwStandard.Mobile
{
    public class FwBadRequestException : Exception
    {
        public FwBadRequestException(string message) : base(message)
        {

        }
    }
}
