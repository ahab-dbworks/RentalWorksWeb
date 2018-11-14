using System;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FwControllerMethodAttribute : Attribute
    {
        public readonly string Id;
        public readonly string Editions;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwControllerMethodAttribute(string Id, string Editions = null)
        {
            this.Id = Id;
            this.Editions = Editions;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
