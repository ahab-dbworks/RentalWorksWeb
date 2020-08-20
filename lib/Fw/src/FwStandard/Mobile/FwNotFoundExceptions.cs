using System;

namespace FwStandard.Mobile
{
    public class FwNotFoundException: Exception 
    {
        public FwNotFoundException() : base()
        {
            
        }
        
        public FwNotFoundException(string message) : base(message)
        {

        }
    }
}