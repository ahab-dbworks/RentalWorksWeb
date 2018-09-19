using System;
using System.Linq;

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

    [AttributeUsage(AttributeTargets.Property)]
    public class FwLogicPropertyAttribute : Attribute
    {
        public readonly string Id;
        public readonly bool IsPrimaryKey;
        public readonly bool IsRecordTitle;
        public readonly bool IsReadOnly;
        public readonly bool IsPrimaryKeyOptional;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwLogicPropertyAttribute(string Id, bool IsPrimaryKey = false, bool IsRecordTitle = false, bool IsReadOnly = false, bool IsPrimaryKeyOptional = false)
        {
            this.Id = Id;
            this.IsPrimaryKey         = IsPrimaryKey;
            this.IsRecordTitle        = IsRecordTitle;
            this.IsReadOnly           = IsReadOnly;
            this.IsPrimaryKeyOptional = IsPrimaryKeyOptional;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
