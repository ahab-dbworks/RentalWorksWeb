using System;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FwLogicAttribute : Attribute
    {
        public readonly string Id;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwLogicAttribute(string Id)
        {
            this.Id = Id;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
