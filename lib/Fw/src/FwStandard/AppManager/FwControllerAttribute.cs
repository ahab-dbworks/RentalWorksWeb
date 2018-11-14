using System;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FwControllerAttribute : Attribute
    {
        public readonly string Id;
        public readonly string Editions;
        public readonly string ParentId;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwControllerAttribute(string Id, string Editions = null, string ParentId = null)
        {
            this.Id = Id;
            this.Editions = Editions;
            this.ParentId = ParentId;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
