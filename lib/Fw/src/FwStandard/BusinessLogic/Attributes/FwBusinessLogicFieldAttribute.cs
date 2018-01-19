using System;

namespace FwStandard.BusinessLogic.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FwBusinessLogicFieldAttribute : Attribute
    {
        public readonly bool IsPrimaryKey;
        public readonly bool IsRecordTitle;
        public readonly bool IsReadOnly;
        public readonly bool IsPrimaryKeyOptional;
        public readonly bool IsCustomPrimaryKey;
        //---------------------------------------------------------------------------------------------------------------------------
        public FwBusinessLogicFieldAttribute(bool isPrimaryKey = false, bool isRecordTitle = false, bool isReadOnly = false, bool isPrimaryKeyOptional = false, bool isCustomPrimaryKey = false)
        {
            this.IsPrimaryKey         = isPrimaryKey;
            this.IsRecordTitle        = isRecordTitle;
            this.IsReadOnly           = isReadOnly;
            this.IsPrimaryKeyOptional = isPrimaryKeyOptional;
            this.IsCustomPrimaryKey   = isCustomPrimaryKey;
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
