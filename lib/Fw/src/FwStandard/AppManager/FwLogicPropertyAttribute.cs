using System;

namespace FwStandard.AppManager
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FwLogicPropertyAttribute : Attribute
    {
        public readonly string Id;
        public readonly bool IsPrimaryKey;
        public readonly bool IsRecordTitle;
        public readonly bool IsReadOnly;
        public readonly bool IsPrimaryKeyOptional;
        public readonly bool IsNotAudited;
        public readonly bool IsAuditMasked;
        public readonly bool DisableDirectAssign;  // for New 
        public readonly bool DisableDirectModify;  // for Editing 
        //---------------------------------------------------------------------------------------------------------------------------
        public FwLogicPropertyAttribute(string Id, bool IsPrimaryKey = false, bool IsRecordTitle = false, bool IsReadOnly = false, bool IsPrimaryKeyOptional = false, bool IsNotAudited = false, bool IsAuditMasked = false, bool DisableDirectAssign = false, bool DisableDirectModify = false)
        {
            this.Id = Id;
            this.IsPrimaryKey         = IsPrimaryKey;
            this.IsRecordTitle        = IsRecordTitle;
            this.IsReadOnly           = IsReadOnly;
            this.IsPrimaryKeyOptional = IsPrimaryKeyOptional;
            this.IsNotAudited         = IsNotAudited;
            this.IsAuditMasked        = IsAuditMasked;
            this.DisableDirectAssign  = DisableDirectAssign;
            this.DisableDirectModify  = DisableDirectModify;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
