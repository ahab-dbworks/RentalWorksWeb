using System;

namespace WebApi.Security
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class SecurityApplicationNodeAttribute : Attribute
    {
        public readonly string   Id;
        public readonly string[] Application;
        public readonly string   ApplicationType;
        //---------------------------------------------------------------------------------------------------------------------------
        public SecurityApplicationNodeAttribute(string Id = "", string[] Application = null, string ApplicationType = "")
        {
            this.Id              = Id;
            this.Application     = Application ?? new string[0];
            this.ApplicationType = ApplicationType;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class SecurityModuleAttribute : Attribute
    {
        public readonly string   Id;
        public readonly string[] ParentIds;
        //---------------------------------------------------------------------------------------------------------------------------
        public SecurityModuleAttribute(string Id = "", string[] ParentIds = null)
        {
            this.Id        = Id;
            this.ParentIds = ParentIds ?? new string[0];
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class SecurityModuleComponentAttribute : Attribute
    {
        public readonly string Id;
        public readonly string ParentId;
        //---------------------------------------------------------------------------------------------------------------------------
        public SecurityModuleComponentAttribute(string Id = "", string ParentId = "")
        {
            this.Id       = Id;
            this.ParentId = ParentId;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class SecurityModuleActionAttribute : Attribute
    {
        public readonly string Id;
        public readonly string ParentId;
        //---------------------------------------------------------------------------------------------------------------------------
        public SecurityModuleActionAttribute(string Id = "", string ParentId = "")
        {
            this.Id       = Id;
            this.ParentId = ParentId;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
